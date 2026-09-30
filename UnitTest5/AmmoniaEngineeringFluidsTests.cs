#if NET10_0_OR_GREATER
using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Fast = EngineeringUnits.Fast;

namespace UnitsTests;

/// <summary>
/// For Ammonia the Update overloads taking EngineeringUnits.Fast quantities use EngineeringFluids instead of CoolProp.
/// They must give the same state as the EngineeringUnits overloads (CoolProp) from the same inputs.
/// </summary>
[TestClass]
public class AmmoniaEngineeringFluidsTests
{
    private const double RelTol = 1e-7;

    // Reference states, set through CoolProp
    private static readonly (string Name, Action<Fluid> Set)[] States =
    [
        ("gas 5 bar 40 °C", f => f.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreeCelsius(40))),
        ("liquid 20 bar 0 °C", f => f.UpdatePT(Pressure.FromBar(20), Temperature.FromDegreeCelsius(0))),
        ("supercritical 150 bar 150 °C", f => f.UpdatePT(Pressure.FromBar(150), Temperature.FromDegreeCelsius(150))),
        ("two-phase 5 bar q=0.3", f => f.UpdatePX(Pressure.FromBar(5), 0.3)),
        ("two-phase -30 °C q=0.9", f => f.UpdateXT(0.9, Temperature.FromDegreeCelsius(-30))),
    ];

    private static Fast.Pressure P(Fluid f) => Fast.Pressure.FromSI(f.Pressure.SI);
    private static Fast.Temperature T(Fluid f) => Fast.Temperature.FromSI(f.Temperature.SI);
    private static Fast.SpecificEnergy H(Fluid f) => Fast.SpecificEnergy.FromSI(f.Enthalpy.SI);
    private static Fast.SpecificEntropy S(Fluid f) => Fast.SpecificEntropy.FromSI(f.Entropy.SI);
    private static Fast.Density D(Fluid f) => Fast.Density.FromSI(f.Density.SI);

    // Each update, once through CoolProp (EngineeringUnits) and once through EngineeringFluids (EngineeringUnits.Fast)
    private static readonly (string Name, Func<Fluid, bool> AppliesTo, Action<Fluid, Fluid> Classic, Action<Fluid, Fluid> Fast)[] Updates =
    [
        ("PT", r => r.Phase is not Phases.Twophase, (f, r) => f.UpdatePT(r.Pressure, r.Temperature), (f, r) => f.UpdatePT(P(r), T(r))),
        ("PH", _ => true, (f, r) => f.UpdatePH(r.Pressure, r.Enthalpy), (f, r) => f.UpdatePH(P(r), H(r))),
        ("PS", _ => true, (f, r) => f.UpdatePS(r.Pressure, r.Entropy), (f, r) => f.UpdatePS(P(r), S(r))),
        ("PX", r => r.Phase is Phases.Twophase, (f, r) => f.UpdatePX(r.Pressure, r.Quality), (f, r) => f.UpdatePX(P(r), r.Quality)),
        ("XT", r => r.Phase is Phases.Twophase, (f, r) => f.UpdateXT(r.Quality, r.Temperature), (f, r) => f.UpdateXT(r.Quality, T(r))),
        ("DT", _ => true, (f, r) => f.UpdateDT(r.Density, r.Temperature), (f, r) => f.UpdateDT(D(r), T(r))),
        ("DP", _ => true, (f, r) => f.UpdateDP(r.Density, r.Pressure), (f, r) => f.UpdateDP(D(r), P(r))),
        ("DH", _ => true, (f, r) => f.UpdateDH(r.Density, r.Enthalpy), (f, r) => f.UpdateDH(D(r), H(r))),
        ("DS", _ => true, (f, r) => f.UpdateDS(r.Density, r.Entropy), (f, r) => f.UpdateDS(D(r), S(r))),
        ("TS", _ => true, (f, r) => f.UpdateTS(r.Temperature, r.Entropy), (f, r) => f.UpdateTS(T(r), S(r))),
        ("HS", _ => true, (f, r) => f.UpdateHS(r.Enthalpy, r.Entropy), (f, r) => f.UpdateHS(H(r), S(r))),
    ];

    private static readonly (string Name, Func<Fluid, double> Get)[] Properties =
    [
        ("T", f => f.Temperature?.SI ?? double.NaN),
        ("P", f => f.Pressure?.SI ?? double.NaN),
        ("D", f => f.Density?.SI ?? double.NaN),
        ("H", f => f.Enthalpy?.SI ?? double.NaN),
        ("S", f => f.Entropy?.SI ?? double.NaN),
        ("U", f => f.InternalEnergy?.SI ?? double.NaN),
        ("Cp", f => f.Cp?.SI ?? double.NaN),
        ("Cv", f => f.Cv?.SI ?? double.NaN),
        ("Mu", f => f.DynamicViscosity?.SI ?? double.NaN),
        ("K", f => f.Conductivity?.SI ?? double.NaN),
        ("Pr", f => f.Prandtl),
        ("W", f => f.SoundSpeed?.SI ?? double.NaN),
        ("Z", f => f.Compressibility),
        ("Tsat", f => f.Tsat?.SI ?? double.NaN),
        ("GasD", f => f.GasDensity?.SI ?? double.NaN),
        ("LiqD", f => f.LiquidDensity?.SI ?? double.NaN),
    ];

    private static bool Close(double expected, double actual) =>
        expected == actual || (double.IsNaN(expected) && double.IsNaN(actual)) || Math.Abs(expected - actual) <= RelTol * Math.Max(Math.Abs(expected), 1e-12);

    [TestMethod]
    public void SameStateAsCoolProp()
    {
        var problems = new List<string>();

        foreach (var (stateName, set) in States)
        {
            var reference = new Fluid(FluidList.Ammonia);
            set(reference);
            Assert.IsFalse(reference.FailState, stateName);

            foreach (var update in Updates.Where(u => u.AppliesTo(reference)))
            {
                var coolProp = new Fluid(FluidList.Ammonia);
                var engineeringFluids = new Fluid(FluidList.Ammonia);
                update.Classic(coolProp, reference);
                update.Fast(engineeringFluids, reference);

                string where = $"{update.Name} at {stateName}";

                // CoolProp can't do DH and DS for ammonia in two-phase or above the critical point, EngineeringFluids can:
                // then it must land on the state the inputs were taken from
                if (coolProp.FailState && update.Name is "DH" or "DS")
                    coolProp = reference;

                if (engineeringFluids.EngineeringFluidsUpdateCount != 1)
                    problems.Add($"{where}: fell back to CoolProp");

                if (coolProp.FailState != engineeringFluids.FailState)
                    problems.Add($"{where}: FailState CoolProp={coolProp.FailState} EngineeringFluids={engineeringFluids.FailState}");
                if (coolProp.Phase != engineeringFluids.Phase)
                    problems.Add($"{where}: Phase CoolProp={coolProp.Phase} EngineeringFluids={engineeringFluids.Phase}");

                bool twoPhase = coolProp.Phase is Phases.Twophase;
                if (twoPhase && !Close(coolProp.Quality, engineeringFluids.Quality))
                    problems.Add($"{where}: Quality CoolProp={coolProp.Quality} EngineeringFluids={engineeringFluids.Quality}");

                foreach (var (name, get) in Properties)
                {
                    // CoolProp's SoundSpeed is 0 everywhere in two-phase
                    if (twoPhase && name == "W")
                        continue;

                    double expected = get(coolProp), actual = get(engineeringFluids);
                    if (!Close(expected, actual))
                        problems.Add(string.Create(CultureInfo.InvariantCulture,
                            $"{where}: {name} CoolProp={expected:G12} EngineeringFluids={actual:G12} (rel {Math.Abs(expected - actual) / Math.Abs(expected):E1})"));
                }
            }
        }

        Assert.AreEqual(0, problems.Count, Environment.NewLine + string.Join(Environment.NewLine, problems));
    }

    [TestMethod]
    public void R717IsAmmonia()
    {
        var fluid = new Fluid(FluidList.R717);
        fluid.UpdatePT(Fast.Pressure.FromBar(5), Fast.Temperature.FromDegreeCelsius(40));

        Assert.IsFalse(fluid.FailState);
        Assert.AreEqual(1, fluid.EngineeringFluidsUpdateCount, "R717 should go through EngineeringFluids");
    }

    [TestMethod]
    public void EngineeringUnitsOverloadsStillUseCoolProp()
    {
        var fluid = new Fluid(FluidList.Ammonia);
        fluid.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreeCelsius(40));

        Assert.IsFalse(fluid.FailState);
        Assert.AreEqual(0, fluid.EngineeringFluidsUpdateCount, "Only the EngineeringUnits.Fast overloads use EngineeringFluids");
    }

    [TestMethod]
    public void CanBeSwitchedOff()
    {
        try
        {
            Fluid.UseEngineeringFluidsForAmmonia = false;

            var fluid = new Fluid(FluidList.Ammonia);
            fluid.UpdatePT(Fast.Pressure.FromBar(5), Fast.Temperature.FromDegreeCelsius(40));

            Assert.IsFalse(fluid.FailState);
            Assert.AreEqual(0, fluid.EngineeringFluidsUpdateCount, "Switched off, CoolProp should do the update");
        }
        finally
        {
            Fluid.UseEngineeringFluidsForAmmonia = true;
        }
    }

    [TestMethod]
    public void AboveCriticalPointSameAsCoolProp()
    {
        var coolProp = new Fluid(FluidList.Ammonia);
        var engineeringFluids = new Fluid(FluidList.Ammonia);

        coolProp.UpdatePX(Pressure.FromBar(150), 0.5);
        engineeringFluids.UpdatePX(Fast.Pressure.FromBar(150), 0.5);
        Assert.IsTrue(engineeringFluids.FailState);
        Assert.AreEqual(coolProp.Enthalpy.SI, engineeringFluids.Enthalpy.SI, Math.Abs(coolProp.Enthalpy.SI) * RelTol);

        coolProp.UpdateXT(0.5, Temperature.FromDegreeCelsius(140));
        engineeringFluids.UpdateXT(0.5, Fast.Temperature.FromDegreeCelsius(140));
        Assert.IsTrue(engineeringFluids.FailState);
        Assert.AreEqual(coolProp.Enthalpy.SI, engineeringFluids.Enthalpy.SI, Math.Abs(coolProp.Enthalpy.SI) * RelTol);
    }

    [TestMethod]
    public void OutsideTheRangeFailsLikeCoolProp()
    {
        // Below the triple point: EngineeringFluids throws, the update falls back to CoolProp, which fails too
        var coolProp = new Fluid(FluidList.Ammonia);
        var engineeringFluids = new Fluid(FluidList.Ammonia);

        coolProp.UpdatePX(Pressure.FromPascal(1000), 0.5);
        engineeringFluids.UpdatePX(Fast.Pressure.FromPascal(1000), 0.5);

        Assert.AreEqual(coolProp.FailState, engineeringFluids.FailState);
    }
}
#endif
