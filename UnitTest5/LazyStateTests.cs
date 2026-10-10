using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitsTests;

// A Fluid creates its CoolProp state the first time it is updated, and reads its constants (limits, critical point,
// freeze temperature) from a per-fluid cache. The expected values below come from SharpFluids 3.0.518, before that change.
[TestClass]
public class LazyStateTests
{
    private static void AreClose(double expected, double actual, string what) =>
        Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-9, what);

    [TestMethod]
    public void NewFluid_HasNoCoolPropState_UntilItIsUpdated()
    {
        _ = new Fluid(FluidList.Water);   // the first one fills the constants cache
        var water = new Fluid(FluidList.Water);

        Assert.IsNull(water.CoolPropStateIfCreated);
        AreClose(647.095999999987, water.CriticalTemperature.Kelvins, "critical temperature");

        water.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreesCelsius(20));

        Assert.IsNotNull(water.CoolPropStateIfCreated);
        Assert.IsFalse(water.FailState);
        AreClose(998.206543497662, water.Density.KilogramPerCubicMeter, "density");
    }

    [DataTestMethod]
    [DataRow(FluidList.Water, 2000.0, 273.16, 647.095999999987, 22063999.9999977, 611.654800896869, 1000000000.0)]
    [DataRow(FluidList.Ammonia, 725.0, 195.495, 405.559999973263, 11363391.1574146, 6055.81357453296, 1000000000.0)]
    [DataRow(FluidList.R134a, 455.0, 169.85, 374.211966584951, 4059276.37379106, 389.56378856199, 70000000.0)]
    public void Constants_AreTheSameAsBefore(FluidList type, double tmax, double tmin, double tcrit, double pcrit, double pmin, double pmax)
    {
        for (int i = 0; i < 2; i++)   // the second one takes them from the cache
        {
            var fluid = new Fluid(type);

            AreClose(tmax, fluid.LimitTemperatureMax.Kelvins, "Tmax");
            AreClose(tmin, fluid.LimitTemperatureMin.Kelvins, "Tmin");
            AreClose(tmin, fluid.T_freeze.Kelvins, "T_freeze");
            AreClose(tcrit, fluid.CriticalTemperature.Kelvins, "critical temperature");
            AreClose(pcrit, fluid.CriticalPressure.Pascal, "critical pressure");
            AreClose(pmin, fluid.LimitPressureMin.Pascal, "pressure min");
            AreClose(pmax, fluid.LimitPressureMax.Pascal, "pressure max");
        }
    }

    [DataTestMethod]
    [DataRow(FluidList.Water, 1.0, 20.0, 998.206543497662, 84006.0539427943, 4184.0550670546, 0.00100159655130398)]
    [DataRow(FluidList.Ammonia, 10.0, 40.0, 7.21133271301473, 1673903.5521582, 2842.4226656027, 1.0447062879466E-05)]
    [DataRow(FluidList.R134a, 5.0, 30.0, 22.5538170125167, 421156.891871864, 949.382792536692, 1.19303893898693E-05)]
    public void Update_GivesTheSameValuesAsBefore(FluidList type, double bar, double celsius, double density, double enthalpy, double cp, double viscosity)
    {
        var fluid = new Fluid(type);
        fluid.UpdatePT(Pressure.FromBar(bar), Temperature.FromDegreesCelsius(celsius));

        Assert.IsFalse(fluid.FailState);
        AreClose(density, fluid.Density.KilogramPerCubicMeter, "density");
        AreClose(enthalpy, fluid.Enthalpy.JoulePerKilogram, "enthalpy");
        AreClose(cp, fluid.Cp.JoulePerKilogramKelvin, "cp");
        AreClose(viscosity, fluid.DynamicViscosity.PascalSeconds, "viscosity");
    }

    [TestMethod]
    public void Brine_GetsItsFractionLimitsFreezeTemperatureAndValues()
    {
        for (int i = 0; i < 2; i++)   // the second one takes the constants and freeze temperature from the cache
        {
            var brine = new Fluid(FluidList.MixPropyleneGlycolAQ);
            brine.SetFraction(30);

            AreClose(0.6, brine.FractionMax, "fraction max");
            AreClose(260.36089594153, brine.T_freeze.Kelvins, "freeze temperature");

            brine.UpdatePT(Pressure.FromBar(2), Temperature.FromDegreesCelsius(-5));

            Assert.IsFalse(brine.FailState);
            AreClose(1033.0243872948, brine.Density.KilogramPerCubicMeter, "density");
            AreClose(-95482.6401154879, brine.Enthalpy.JoulePerKilogram, "enthalpy");
        }
    }

    [TestMethod]
    public void Copy_CarriesTheValues_WithoutCreatingAState()
    {
        var water = new Fluid(FluidList.Water);
        water.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreesCelsius(20));

        var copy = new Fluid(FluidList.Ammonia);
        copy.Copy(water);

        Assert.IsNull(copy.CoolPropStateIfCreated);
        Assert.AreEqual(water.Density.KilogramPerCubicMeter, copy.Density.KilogramPerCubicMeter);
        Assert.AreEqual(water.Enthalpy.JoulePerKilogram, copy.Enthalpy.JoulePerKilogram);

        // ...and the copy can still be updated, like a new water
        copy.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreesCelsius(80));
        var fresh = new Fluid(FluidList.Water);
        fresh.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreesCelsius(80));

        Assert.IsFalse(copy.FailState);
        Assert.AreEqual(fresh.Density.KilogramPerCubicMeter, copy.Density.KilogramPerCubicMeter);
        Assert.AreEqual(fresh.Enthalpy.JoulePerKilogram, copy.Enthalpy.JoulePerKilogram);
    }

    [TestMethod]
    public void SetNewMedia_KeepsTheStateForTheSameMedia_AndStartsAgainForAnother()
    {
        var fluid = new Fluid(FluidList.Water);
        fluid.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreesCelsius(20));
        var state = fluid.CoolPropStateIfCreated;

        fluid.SetNewMedia(fluid.Media);
        Assert.AreSame(state, fluid.CoolPropStateIfCreated);

        fluid.SetNewMedia(FluidList.Ammonia);
        Assert.IsNull(fluid.CoolPropStateIfCreated);

        fluid.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreesCelsius(40));
        Assert.IsFalse(fluid.FailState);
        AreClose(7.21133271301473, fluid.Density.KilogramPerCubicMeter, "ammonia density");
    }

    [TestMethod]
    public void Dispose_CreatesNoState_AndTheFluidCanStillBeUpdated()
    {
        var water = new Fluid(FluidList.Water);
        water.Dispose();
        Assert.IsNull(water.CoolPropStateIfCreated);

        water.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreesCelsius(20));
        Assert.IsFalse(water.FailState);
        AreClose(998.206543497662, water.Density.KilogramPerCubicMeter, "density");
    }

    [TestMethod]
    public void ReadExtendedProperties_Off_LeavesThemUnread_AndTheRestUnchanged()
    {
        try
        {
            Fluid.ReadExtendedProperties = false;
            var off = new Fluid(FluidList.Water);
            off.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreesCelsius(20));

            Fluid.ReadExtendedProperties = true;
            var on = new Fluid(FluidList.Water);
            on.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreesCelsius(20));

            // Of the extended properties only rhomolar reaches CoolProp today; AbstractState returns 0 for the others
            Assert.AreEqual(0.0, off.rhomolar);
            AreClose(55408.92000594504, on.rhomolar, "rhomolar");
            Assert.AreEqual(on.Density.KilogramPerCubicMeter, off.Density.KilogramPerCubicMeter);
            Assert.AreEqual(on.Enthalpy.JoulePerKilogram, off.Enthalpy.JoulePerKilogram);
        }
        finally
        {
            Fluid.ReadExtendedProperties = true;
        }
    }

    [TestMethod]
    public void SeparateFluidsOnManyThreads_GiveTheSameResultsAsOneThread()
    {
        // Single-phase states only: water liquid, ammonia and R134a superheated
        var cases = Enumerable.Range(0, 600).Select(i => (i % 3) switch
        {
            0 => (Type: FluidList.Water, Bar: 1.0 + i % 10, Celsius: 20.0 + i % 60),
            1 => (Type: FluidList.Ammonia, Bar: 1.0 + i % 5, Celsius: 30.0 + i % 30),
            _ => (Type: FluidList.R134a, Bar: 1.0 + i % 3, Celsius: 30.0 + i % 30),
        }).ToList();

        double[] Compute((FluidList Type, double Bar, double Celsius) c)
        {
            var fluid = new Fluid(c.Type);
            fluid.UpdatePT(Pressure.FromBar(c.Bar), Temperature.FromDegreesCelsius(c.Celsius));
            Assert.IsFalse(fluid.FailState, $"{c}");
            return [fluid.Density.KilogramPerCubicMeter, fluid.Enthalpy.JoulePerKilogram, fluid.DynamicViscosity.PascalSeconds];
        }

        List<double[]> oneThread = cases.Select(Compute).ToList();
        var manyThreads = new double[cases.Count][];
        Parallel.For(0, cases.Count, new ParallelOptions { MaxDegreeOfParallelism = 8 }, i => manyThreads[i] = Compute(cases[i]));

        for (int i = 0; i < cases.Count; i++)
            CollectionAssert.AreEqual(oneThread[i], manyThreads[i], $"{cases[i]}");
    }
}
