﻿using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;

namespace UnitTest5;

[TestClass]
public class Brine
{
    //Test MixPropyleneGlycolAQ
    [TestMethod]
    public void MixPropyleneGlycolAQ()
    {
        //Arrange
        var brine = new Fluid(FluidList.MixPropyleneGlycolAQ);
        brine.SetFraction(50);

        //Act
        brine.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(30));

        //Assert
        Assert.IsFalse(brine.FailState);
        Assert.AreEqual(0.364454027507565, brine.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(3568.61888590578, brine.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(3568.61888590578, brine.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(36187.9355749803, brine.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(40.877565694763696, brine.Prandtl, 0.0001);
        Assert.AreEqual(10, brine.Pressure.Bar, 0.0001);
        Assert.IsNull(brine.CriticalPressure);
        Assert.IsNull(brine.LimitPressureMax);
        Assert.IsNull(brine.LimitPressureMin);
        Assert.AreEqual(1032.40586458968, brine.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.028315636591452198, brine.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(brine.SurfaceTension);
        Assert.AreEqual(30, brine.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(30, brine.Tsat.DegreeCelsius, 0.0001);
        Assert.IsNull(brine.CriticalTemperature);
        Assert.AreEqual(100, brine.LimitTemperatureMax.DegreeCelsius, 0.0001);
        Assert.AreEqual(-100, brine.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(0.00417472247064576, brine.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0, brine.Quality, 0.0001);
        Assert.IsNull(brine.SoundSpeed);
        Assert.IsNull(brine.MolarMass);
        Assert.AreEqual(0, brine.Compressibility, 0.0001);
        Assert.AreEqual(35219.3242620278, brine.InternalEnergy.JoulePerKilogram, 0.001);
        Assert.AreEqual(-32.193468794403, brine.T_freeze.DegreeCelsius, 0.001);

    }
}
