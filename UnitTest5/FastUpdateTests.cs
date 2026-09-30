#if NET10_0_OR_GREATER
using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using System;
using Fast = EngineeringUnits.Fast;

namespace UnitsTests;

/// <summary>
/// The Update* overloads taking EngineeringUnits.Fast quantities must give exactly the same state as the EngineeringUnits ones.
/// </summary>
[TestClass]
public class FastUpdateTests
{
    // Superheated water at 3 bar and 237 °C (the state used in WaterTests.UpdatePT)
    private static Fluid Reference()
    {
        var water = new Fluid(FluidList.Water);
        water.UpdatePT(Pressure.FromBar(3), Temperature.FromDegreeCelsius(237));
        Assert.IsFalse(water.FailState);
        return water;
    }

    private static Fast.Pressure P(Fluid f) => Fast.Pressure.FromSI(f.Pressure.SI);
    private static Fast.Temperature T(Fluid f) => Fast.Temperature.FromSI(f.Temperature.SI);
    private static Fast.SpecificEnergy H(Fluid f) => Fast.SpecificEnergy.FromSI(f.Enthalpy.SI);
    private static Fast.SpecificEntropy S(Fluid f) => Fast.SpecificEntropy.FromSI(f.Entropy.SI);
    private static Fast.Density D(Fluid f) => Fast.Density.FromSI(f.Density.SI);

    private static void AssertSameState(Fluid expected, Fluid actual)
    {
        Assert.AreEqual(expected.FailState, actual.FailState);
        Assert.AreEqual(expected.Phase, actual.Phase);
        Assert.AreEqual(expected.Quality, actual.Quality, 1e-12);
        AssertClose(expected.Pressure.SI, actual.Pressure.SI);
        AssertClose(expected.Temperature.SI, actual.Temperature.SI);
        AssertClose(expected.Enthalpy.SI, actual.Enthalpy.SI);
        AssertClose(expected.Entropy.SI, actual.Entropy.SI);
        AssertClose(expected.Density.SI, actual.Density.SI);
        AssertClose(expected.Cp.SI, actual.Cp.SI);
        AssertClose(expected.DynamicViscosity.SI, actual.DynamicViscosity.SI);
    }

    private static void AssertClose(double expected, double actual) =>
        Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-12);

    private static void AssertSameUpdate(Action<Fluid> classic, Action<Fluid> fast, FluidList type = FluidList.Water)
    {
        var expected = new Fluid(type);
        var actual = new Fluid(type);

        classic(expected);
        fast(actual);

        AssertSameState(expected, actual);
    }

    [TestMethod]
    public void UpdatePT()
    {
        AssertSameUpdate(
            f => f.UpdatePT(Pressure.FromBar(3), Temperature.FromDegreeCelsius(237)),
            f => f.UpdatePT(Fast.Pressure.FromBar(3), Fast.Temperature.FromDegreeCelsius(237)));
    }

    [TestMethod]
    public void UpdatePH()
    {
        var r = Reference();
        AssertSameUpdate(f => f.UpdatePH(r.Pressure, r.Enthalpy), f => f.UpdatePH(P(r), H(r)));
    }

    [TestMethod]
    public void UpdatePS()
    {
        var r = Reference();
        AssertSameUpdate(f => f.UpdatePS(r.Pressure, r.Entropy), f => f.UpdatePS(P(r), S(r)));
    }

    [TestMethod]
    public void UpdateDS()
    {
        var r = Reference();
        AssertSameUpdate(f => f.UpdateDS(r.Density, r.Entropy), f => f.UpdateDS(D(r), S(r)));
    }

    [TestMethod]
    public void UpdateDP()
    {
        var r = Reference();
        AssertSameUpdate(f => f.UpdateDP(r.Density, r.Pressure), f => f.UpdateDP(D(r), P(r)));
    }

    [TestMethod]
    public void UpdateDT()
    {
        var r = Reference();
        AssertSameUpdate(f => f.UpdateDT(r.Density, r.Temperature), f => f.UpdateDT(D(r), T(r)));
    }

    [TestMethod]
    public void UpdateDH()
    {
        var r = Reference();
        AssertSameUpdate(f => f.UpdateDH(r.Density, r.Enthalpy), f => f.UpdateDH(D(r), H(r)));
    }

    [TestMethod]
    public void UpdateHS()
    {
        var r = Reference();
        AssertSameUpdate(f => f.UpdateHS(r.Enthalpy, r.Entropy), f => f.UpdateHS(H(r), S(r)));
    }

    [TestMethod]
    public void UpdateTS()
    {
        var r = Reference();
        AssertSameUpdate(f => f.UpdateTS(r.Temperature, r.Entropy), f => f.UpdateTS(T(r), S(r)));
    }

    [TestMethod]
    public void UpdatePX()
    {
        AssertSameUpdate(
            f => f.UpdatePX(Pressure.FromBar(25), 0.7),
            f => f.UpdatePX(Fast.Pressure.FromBar(25), 0.7),
            FluidList.CO2);
    }

    [TestMethod]
    public void UpdateXT()
    {
        AssertSameUpdate(
            f => f.UpdateXT(0.7, Temperature.FromDegreeCelsius(13)),
            f => f.UpdateXT(0.7, Fast.Temperature.FromDegreeCelsius(13)),
            FluidList.CO2);
    }

    [TestMethod]
    public void UpdatePXAboveCriticalPressure()
    {
        AssertSameUpdate(
            f => f.UpdatePX(Pressure.FromBar(100), 0.5),
            f => f.UpdatePX(Fast.Pressure.FromBar(100), 0.5),
            FluidList.CO2);
    }

    [TestMethod]
    public void UpdateXTAboveCriticalTemperature()
    {
        AssertSameUpdate(
            f => f.UpdateXT(0.5, Temperature.FromDegreeCelsius(40)),
            f => f.UpdateXT(0.5, Fast.Temperature.FromDegreeCelsius(40)),
            FluidList.CO2);
    }

    [TestMethod]
    public void UpdatePXOnMixture()
    {
        // A mixture has no CriticalPressure - both versions must skip the above-critical path
        AssertSameUpdate(
            f => f.UpdatePX(Pressure.FromBar(1), 0.5),
            f => f.UpdatePX(Fast.Pressure.FromBar(1), 0.5),
            FluidList.R404A_mix);
    }

    [TestMethod]
    public void UpdateCustomFluid()
    {
        var expected = new Fluid(FluidList.Custom_SHC228);
        var actual = new Fluid(FluidList.Custom_SHC228);

        expected.UpdateCustomFluid(Pressure.FromBar(1), Temperature.FromDegreeCelsius(40));
        actual.UpdateCustomFluid(Fast.Pressure.FromBar(1), Fast.Temperature.FromDegreeCelsius(40));

        Assert.AreEqual(expected.FailState, actual.FailState);
        AssertClose(expected.Density.SI, actual.Density.SI);
        AssertClose(expected.Cp.SI, actual.Cp.SI);
        AssertClose(expected.Conductivity.SI, actual.Conductivity.SI);
        AssertClose(expected.DynamicViscosity.SI, actual.DynamicViscosity.SI);
    }

    [TestMethod]
    public void GuardsStillApply()
    {
        var oil = new Fluid(FluidList.Custom_SHC228);
        Assert.ThrowsException<NotImplementedException>(() => oil.UpdatePH(Fast.Pressure.FromBar(1), Fast.SpecificEnergy.FromJoulePerKilogram(1000)));

        var mix = new Fluid(FluidList.R404A_mix);
        Assert.ThrowsException<NotImplementedException>(() => mix.UpdatePH(Fast.Pressure.FromBar(1), Fast.SpecificEnergy.FromJoulePerKilogram(1000)));

        var water = new Fluid(FluidList.Water);
        Assert.ThrowsException<NotImplementedException>(() => water.UpdateHT(Fast.SpecificEnergy.FromJoulePerKilogram(1000), Fast.Temperature.FromDegreeCelsius(20)));
    }

    [TestMethod]
    public void NullStillPicksTheEngineeringUnitsOverload()
    {
        // Must compile (not be ambiguous) and still be a no-op, exactly as before the Fast overloads were added
        var water = Reference();
        var before = water.Enthalpy.SI;

        water.UpdatePT(null, null);
        water.UpdatePH(null, null);
        water.UpdatePX(null, 0.5);
        water.UpdateXT(0.5, null);

        Assert.AreEqual(before, water.Enthalpy.SI);
    }
}
#endif
