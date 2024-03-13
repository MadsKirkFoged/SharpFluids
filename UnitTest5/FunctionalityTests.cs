//using EngineeringUnits;
using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UnitsTests;

[TestClass]
public class FunctionalityTests
{

    [TestMethod]
    public void IntoJsonAndBackAgainMassFlow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        R717.MassFlow = setMassFlow;

        //Save as JSON
        var json = R717.SaveAsJSON();

        //Start new fluid and load as json
        Fluid R717JSON = R717.LoadFromJSON(json);

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.0722711120542153, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(3251.34163624504, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2270.36197650328, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2148349.72016398, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(0.93888901877652886, R717.Prandtl, 0.0001);
        Assert.AreEqual(117.968517492664, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(50, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(6000, R717.Entropy.JoulePerKilogramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(278.392108051419, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(2.0869708899873E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(10000, R717.Quality, 0.0001);
        Assert.AreEqual(544.366706172041, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.87621627068822328, R717.Compressibility, 0.0001);
        Assert.AreEqual(1912412.68517865, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.04, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);

        //Assert JSON
        Assert.IsFalse(R717JSON.FailState);
        Assert.AreEqual(R717JSON.LimitPressureMin.Bar, R717.LimitPressureMin.Bar);
        Assert.AreEqual(R717JSON.LimitPressureMax.Bar, R717.LimitPressureMax.Bar);
        Assert.AreEqual(R717JSON.CriticalPressure.Bar, R717.CriticalPressure.Bar);
        Assert.AreEqual(R717JSON.LimitTemperatureMin.DegreeCelsius, R717.LimitTemperatureMin.DegreeCelsius);
        Assert.AreEqual(R717JSON.CriticalTemperature.DegreeCelsius, R717.CriticalTemperature.DegreeCelsius);
        Assert.AreEqual(R717JSON.LimitTemperatureMax.DegreeCelsius, R717.LimitTemperatureMax.DegreeCelsius);

        Assert.AreEqual(R717JSON.Conductivity.WattPerMeterKelvin, R717.Conductivity.WattPerMeterKelvin);
        Assert.AreEqual(R717JSON.Cp.JoulePerKilogramKelvin, R717.Cp.JoulePerKilogramKelvin);
        Assert.AreEqual(R717JSON.Cv.JoulePerKilogramKelvin, R717.Cv.JoulePerKilogramKelvin);
        Assert.AreEqual(R717JSON.Enthalpy.JoulePerKilogram, R717.Enthalpy.JoulePerKilogram);
        Assert.AreEqual(R717JSON.Prandtl, R717.Prandtl);
        Assert.AreEqual(R717JSON.Pressure.Bar, R717.Pressure.Bar);
        Assert.AreEqual(R717JSON.Density.KilogramPerCubicMeter, R717.Density.KilogramPerCubicMeter);
        Assert.AreEqual(R717JSON.Entropy.KilocaloriePerGramKelvin, R717.Entropy.KilocaloriePerGramKelvin);
        Assert.IsNull(R717JSON.SurfaceTension);
        Assert.AreEqual(R717JSON.Temperature.DegreeCelsius, R717.Temperature.DegreeCelsius);
        Assert.AreEqual(R717JSON.Tsat.DegreeCelsius, R717.Tsat.DegreeCelsius, 0.00001);
        Assert.AreEqual(R717JSON.DynamicViscosity.NewtonSecondPerMeterSquared, R717.DynamicViscosity.NewtonSecondPerMeterSquared);
        Assert.AreEqual(R717JSON.Quality, R717.Quality);
        Assert.AreEqual(R717JSON.SoundSpeed.MeterPerSecond, R717.SoundSpeed.MeterPerSecond);

        Assert.AreEqual(R717JSON.MolarMass.GramPerMole, R717.MolarMass.GramPerMole);
        Assert.AreEqual(R717JSON.Compressibility, R717.Compressibility);
        Assert.AreEqual(R717JSON.InternalEnergy.JoulePerKilogram, R717.InternalEnergy.JoulePerKilogram);

        Assert.AreEqual(R717JSON.MassFlow.KilogramPerSecond, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(R717JSON.VolumeFlow.CubicMeterPerSecond, R717.VolumeFlow.CubicMeterPerSecond);

        Assert.AreEqual(R717JSON.Media, R717.Media);
    }

    [TestMethod]
    public void IntoJsonAndBackAgainMass()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        var setMass = Mass.FromKilogram(43);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        R717.Mass = setMass;

        //Save as JSON
        var json = R717.SaveAsJSON();

        //Start new fluid and load as json
        Fluid R717JSON = R717.LoadFromJSON(json);

        Assert.AreEqual(43, R717.Mass.Kilogram, 0.0001);
        Assert.AreEqual(0.86, R717.Volume.CubicMeter, 0.0001);

        //Assert JSON          
        Assert.AreEqual(R717JSON.MassFlow, R717.MassFlow);
        Assert.AreEqual(R717JSON.VolumeFlow, R717.VolumeFlow);
    }

    [TestMethod]
    public void LoadValuesThenJSON()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Save as JSON
        var json = R717.SaveAsJSON();

        //Start new fluid and load as json
        Fluid R717JSON = R717.LoadFromJSON(json);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        R717.MassFlow = setMassFlow;
        R717JSON.UpdateDH(setDensity, setEnthalpy);
        R717JSON.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.0722711120542153, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(3251.34163624504, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2270.36197650328, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2148349.72016398, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(0.93888901877652886, R717.Prandtl, 0.0001);
        Assert.AreEqual(117.968517492664, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(50, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(6000, R717.Entropy.JoulePerKilogramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(278.392108051419, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(2.0869708899873E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(10000, R717.Quality, 0.0001);
        Assert.AreEqual(544.366706172041, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.87621627068822328, R717.Compressibility, 0.0001);
        Assert.AreEqual(1912412.68517865, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.04, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);

        //Assert JSON
        Assert.IsFalse(R717JSON.FailState);
        Assert.AreEqual(R717JSON.LimitPressureMin.Bar, R717.LimitPressureMin.Bar);
        Assert.AreEqual(R717JSON.LimitPressureMax.Bar, R717.LimitPressureMax.Bar);
        Assert.AreEqual(R717JSON.CriticalPressure.Bar, R717.CriticalPressure.Bar);
        Assert.AreEqual(R717JSON.LimitTemperatureMin.DegreeCelsius, R717.LimitTemperatureMin.DegreeCelsius);
        Assert.AreEqual(R717JSON.CriticalTemperature.DegreeCelsius, R717.CriticalTemperature.DegreeCelsius);
        Assert.AreEqual(R717JSON.LimitTemperatureMax.DegreeCelsius, R717.LimitTemperatureMax.DegreeCelsius);

        Assert.AreEqual(R717JSON.Conductivity.WattPerMeterKelvin, R717.Conductivity.WattPerMeterKelvin);
        Assert.AreEqual(R717JSON.Cp.JoulePerKilogramKelvin, R717.Cp.JoulePerKilogramKelvin);
        Assert.AreEqual(R717JSON.Cv.JoulePerKilogramKelvin, R717.Cv.JoulePerKilogramKelvin);
        Assert.AreEqual(R717JSON.Enthalpy.JoulePerKilogram, R717.Enthalpy.JoulePerKilogram);
        Assert.AreEqual(R717JSON.Prandtl, R717.Prandtl);
        Assert.AreEqual(R717JSON.Pressure.Bar, R717.Pressure.Bar);
        Assert.AreEqual(R717JSON.Density.KilogramPerCubicMeter, R717.Density.KilogramPerCubicMeter);
        Assert.AreEqual(R717JSON.Entropy.KilocaloriePerGramKelvin, R717.Entropy.KilocaloriePerGramKelvin);
        Assert.IsNull(R717JSON.SurfaceTension);
        Assert.AreEqual(R717JSON.Temperature.DegreeCelsius, R717.Temperature.DegreeCelsius);
        Assert.AreEqual(R717JSON.Tsat.DegreeCelsius, R717.Tsat.DegreeCelsius);
        Assert.AreEqual(R717JSON.DynamicViscosity.NewtonSecondPerMeterSquared, R717.DynamicViscosity.NewtonSecondPerMeterSquared);
        Assert.AreEqual(R717JSON.Quality, R717.Quality);
        Assert.AreEqual(R717JSON.SoundSpeed.MeterPerSecond, R717.SoundSpeed.MeterPerSecond);

        Assert.AreEqual(R717JSON.MolarMass.GramPerMole, R717.MolarMass.GramPerMole);
        Assert.AreEqual(R717JSON.Compressibility, R717.Compressibility);
        Assert.AreEqual(R717JSON.InternalEnergy.JoulePerKilogram, R717.InternalEnergy.JoulePerKilogram);

        Assert.AreEqual(R717JSON.MassFlow.KilogramPerSecond, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(R717JSON.VolumeFlow.CubicMeterPerSecond, R717.VolumeFlow.CubicMeterPerSecond);

        Assert.AreEqual(R717JSON.Media, R717.Media);
    }

    [TestMethod]
    public void FailStateAfterJSON()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        _ = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        var json = R717.SaveAsJSON();
        Fluid R717JSON = R717.LoadFromJSON(json);

        //Asking for Tsat
        _ = R717JSON.Tsat;

        Assert.IsFalse(R717JSON.FailState);

    }

    [TestMethod]
    public void SavingJSONWithoutPointersInTheDLL()
    {

        //This test was made after we found out that the CoolProp DLL was saving pointers in JSON
        //..which made things works fine on your local computer but crash when using DBs and servers!

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var R7172 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(15.36622602626586);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(1043420.2106074861);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        R717.MassFlow = setMassFlow;
        var R717JSON = R717.SaveAsJSON();

        R7172.UpdateDH(setDensity, setEnthalpy);
        R7172.MassFlow = setMassFlow;
        var R7172JSON = R7172.SaveAsJSON();

        //Assert JSON          
        Assert.AreEqual(R717JSON, R7172JSON);

    }

    [TestMethod]
    public void CopyFuntional()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);

        //Start new fluid and load as json
        var R717JSON = new Fluid();
        R717JSON.Copy(R717);

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.0722711120542153, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(3251.34163624504, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2270.36197650328, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2148349.72016398, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(0.93888901877652886, R717.Prandtl, 0.0001);
        Assert.AreEqual(117.968517492664, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(50, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(6000, R717.Entropy.JoulePerKilogramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(278.392108051419, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(2.0869708899873E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(10000, R717.Quality, 0.0001);
        Assert.AreEqual(544.366706172041, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.87621627068822328, R717.Compressibility, 0.0001);
        Assert.AreEqual(1912412.68517865, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        //Assert JSON
        Assert.IsFalse(R717JSON.FailState);
        Assert.AreEqual(R717JSON.LimitPressureMin.Bar, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(R717JSON.LimitPressureMax.Bar, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(R717JSON.CriticalPressure.Bar, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(R717JSON.LimitTemperatureMin.DegreeCelsius, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(R717JSON.CriticalTemperature.DegreeCelsius, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(R717JSON.LimitTemperatureMax.DegreeCelsius, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(R717JSON.Conductivity.WattPerMeterKelvin, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(R717JSON.Cp.JoulePerKilogramKelvin, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(R717JSON.Cv.JoulePerKilogramKelvin, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(R717JSON.Enthalpy.JoulePerKilogram, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(R717JSON.Prandtl, R717.Prandtl, 0.0001);
        Assert.AreEqual(R717JSON.Pressure.Bar, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(R717JSON.Density.KilogramPerCubicMeter, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(R717JSON.Entropy.KilocaloriePerGramKelvin, R717.Entropy.KilocaloriePerGramKelvin, 0.0001);
        Assert.IsNull(R717JSON.SurfaceTension);
        Assert.AreEqual(R717JSON.Temperature.DegreeCelsius, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(R717JSON.Tsat.DegreeCelsius, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(R717JSON.DynamicViscosity.NewtonSecondPerMeterSquared, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(R717JSON.Quality, R717.Quality, 0.0001);
        Assert.AreEqual(R717JSON.SoundSpeed.MeterPerSecond, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(R717.MolarMass.GramPerMole, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(R717.Compressibility, R717.Compressibility, 0.0001);
        Assert.AreEqual(R717.InternalEnergy.JoulePerKilogram, R717.InternalEnergy.JoulePerKilogram, 0.0001);
    }

    [TestMethod]
    public void UpdateStartValues()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);

        //Assert
        Assert.IsTrue(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(null, R717.Conductivity);
        Assert.AreEqual(null, R717.Cp);
        Assert.AreEqual(null, R717.Cv);
        Assert.AreEqual(null, R717.Enthalpy);
        Assert.AreEqual(0, R717.Prandtl);
        Assert.AreEqual(null, R717.Pressure);
        Assert.AreEqual(null, R717.Density);
        Assert.AreEqual(null, R717.Entropy);
        Assert.AreEqual(null, R717.SurfaceTension);
        Assert.AreEqual(null, R717.Temperature);
        Assert.AreEqual(null, R717.Tsat);
        Assert.AreEqual(null, R717.DynamicViscosity);
        Assert.AreEqual(0, R717.Quality);
        Assert.AreEqual(null, R717.SoundSpeed);

        Assert.AreEqual(null, R717.MolarMass);
        Assert.AreEqual(0, R717.Compressibility);
        Assert.AreEqual(null, R717.InternalEnergy);

    }

    [TestMethod]
    public void ZeroValues()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(15.36622602626586);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(1043420.2106074861);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);

        R717.SetValuesToNull();

        //Assert
        Assert.IsTrue(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(null, R717.Conductivity);
        Assert.AreEqual(null, R717.Cp);
        Assert.AreEqual(null, R717.Cv);
        Assert.AreEqual(null, R717.Enthalpy);
        Assert.AreEqual(0, R717.Prandtl);
        Assert.AreEqual(null, R717.Pressure);
        Assert.AreEqual(null, R717.Density);
        Assert.AreEqual(null, R717.Entropy);
        Assert.AreEqual(null, R717.SurfaceTension);
        Assert.AreEqual(null, R717.Temperature);
        Assert.AreEqual(null, R717.Tsat);
        Assert.AreEqual(null, R717.DynamicViscosity);
        Assert.AreEqual(0, R717.Quality);
        Assert.AreEqual(null, R717.SoundSpeed);

        Assert.AreEqual(null, R717.MolarMass);
        Assert.AreEqual(0, R717.Compressibility);
        Assert.AreEqual(null, R717.InternalEnergy);

    }

    [TestMethod]
    public void CopyTypeWithEmptyStartingFluid()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        R717.UpdateDH(setDensity, setEnthalpy);

        //Start new fluid
        var CO2 = new Fluid();
        CO2.CopyType(R717);

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(null, CO2.Conductivity);
        Assert.AreEqual(null, CO2.Cp);
        Assert.AreEqual(null, CO2.Cv);
        Assert.AreEqual(null, CO2.Enthalpy);
        Assert.AreEqual(0, CO2.Prandtl);
        Assert.AreEqual(null, CO2.Pressure);
        Assert.AreEqual(null, CO2.Density);
        Assert.AreEqual(null, CO2.Entropy);
        Assert.AreEqual(null, CO2.SurfaceTension);
        Assert.AreEqual(null, CO2.Temperature);
        Assert.AreEqual(null, CO2.Tsat);
        Assert.AreEqual(null, CO2.DynamicViscosity);
        Assert.AreEqual(0, CO2.Quality);
        Assert.AreEqual(null, CO2.SoundSpeed);

        Assert.AreEqual(null, CO2.MolarMass);
        Assert.AreEqual(0, CO2.Compressibility);
        Assert.AreEqual(null, CO2.InternalEnergy);
    }

    [TestMethod]
    public void CopyTypeWithNonEmptyStartingFluid()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        R717.UpdateDH(setDensity, setEnthalpy);

        //Start new fluid
        var CO2 = new Fluid(FluidList.CO2);
        CO2.UpdatePT(Pressure.FromBar(30), Temperature.FromDegreeCelsius(60));
        CO2.CopyType(R717);

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, CO2.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, CO2.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, CO2.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, CO2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, CO2.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, CO2.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(null, CO2.Conductivity);
        Assert.AreEqual(null, CO2.Cp);
        Assert.AreEqual(null, CO2.Cv);
        Assert.AreEqual(null, CO2.Enthalpy);
        Assert.AreEqual(0, CO2.Prandtl);
        Assert.AreEqual(null, CO2.Pressure);
        Assert.AreEqual(null, CO2.Density);
        Assert.AreEqual(null, CO2.Entropy);
        Assert.AreEqual(null, CO2.SurfaceTension);
        Assert.AreEqual(null, CO2.Temperature);
        Assert.AreEqual(null, CO2.Tsat);
        Assert.AreEqual(null, CO2.DynamicViscosity);
        Assert.AreEqual(0, CO2.Quality);
        Assert.AreEqual(null, CO2.SoundSpeed);

        Assert.AreEqual(null, CO2.MolarMass);
        Assert.AreEqual(0, CO2.Compressibility);
        Assert.AreEqual(null, CO2.InternalEnergy);
    }

    [TestMethod]
    public void ShouldFailWhenTryingToLoadWithOutAFluid()
    {

        //Arrange
        var R717 = new Fluid();
        var setDensity = Density.FromKilogramPerCubicMeter(15.36622602626586);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(1043420.2106074861);

        _=Assert.ThrowsException<InvalidOperationException>(() => R717.UpdateDH(setDensity, setEnthalpy));

    }

    [TestMethod]
    public void AddToUsingMassFlow()
    {

        //Arrange
        var Input1 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        Input1.MassFlow = MassFlow.FromKilogramPerSecond(2);
        Input1.UpdateDH(setDensity, setEnthalpy);

        var Input2 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(12);
        var setTemperature = Temperature.FromDegreeCelsius(63);
        Input2.MassFlow = MassFlow.FromKilogramPerSecond(1.4);
        Input2.UpdatePT(setPressure, setTemperature);

        //Act
        _=Input1.AddTo(Input2);

        //Assert
        //Assert.IsFalse(Input1.FailState);
        //Assert.AreEqual(0.060912231081315084, Input1.LimitPressureMin.Bar, 0.0001);
        //Assert.AreEqual(10000, Input1.LimitPressureMax.Bar, 0.0001);
        //Assert.AreEqual(113.634, Input1.CriticalPressure.Bar, 0.0001);
        //Assert.AreEqual(-77.655, Input1.LimitTemperatureMin.DegreeCelsius, 0.0001);
        //Assert.AreEqual(132.41, Input1.CriticalTemperature.DegreeCelsius, 0.0001);
        //Assert.AreEqual(451.85, Input1.LimitTemperatureMax.DegreeCelsius, 0.0001);

        //Assert.AreEqual(0.0722711120542153, Input1.Conductivity.WattPerMeterKelvin, 0.0001);
        //Assert.AreEqual(3251.34163624504, Input1.Cp.JoulePerKilogramKelvin, 0.0001);
        //Assert.AreEqual(2270.36197650328, Input1.Cv.JoulePerKilogramKelvin, 0.0001);
        //Assert.AreEqual(1974756.3106341641, Input1.Enthalpy.JoulePerKilogram, 0.0001);
        //Assert.AreEqual(0.93888901877652886, Input1.Prandtl, 0.0001);
        //Assert.AreEqual(74.334422054350568, Input1.Pressure.Bar, 0.0001);
        //Assert.AreEqual(50, Input1.Density.KilogramPerCubicMeter, 0.0001);
        //Assert.AreEqual(1.4371720270740429, Input1.Entropy.CaloriePerGramKelvin, 0.0001);
        //Assert.AreEqual(0, Input1.SurfaceTension.NewtonPerMeter, 0.0001);
        //Assert.AreEqual(189.701240030246, Input1.Temperature.DegreeCelsius, 0.0001);
        //Assert.AreEqual(108.979422334718, Input1.Tsat.DegreeCelsius, 0.0001);
        //Assert.AreEqual(2.0869708899873E-05, Input1.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        //Assert.AreEqual(10000, Input1.Quality, 0.0001);
        //Assert.AreEqual(544.366706172041, Input1.SoundSpeed.MeterPerSecond, 0.0001);

        //Assert.AreEqual(17.03052, Input1.MolarMass.GramPerMole, 0.001);
        //Assert.AreEqual(0.87621627068822328, Input1.Compressibility, 0.0001);
        //Assert.AreEqual(1912412.68517865, Input1.InternalEnergy.JoulePerKilogram, 0.0001);

        //Assert.AreEqual(3.4, Input1.MassFlow.KilogramPerSecond, 0.0001);
        //Assert.AreEqual(0.068, Input1.VolumeFlow.CubicMeterPerSecond, 0.0001);

        ////Assert
        //Assert.IsFalse(Input2.FailState);
        //Assert.AreEqual(0.060912231081315084, Input2.LimitPressureMin.Bar, 0.0001);
        //Assert.AreEqual(10000, Input2.LimitPressureMax.Bar, 0.0001);
        //Assert.AreEqual(113.634, Input2.CriticalPressure.Bar, 0.0001);
        //Assert.AreEqual(-77.655, Input2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        //Assert.AreEqual(132.41, Input2.CriticalTemperature.DegreeCelsius, 0.0001);
        //Assert.AreEqual(451.85, Input2.LimitTemperatureMax.DegreeCelsius, 0.0001);

        //Assert.AreEqual(0.0301054359321657, Input2.Conductivity.WattPerMeterKelvin, 0.0001);
        //Assert.AreEqual(2718.6666345713, Input2.Cp.JoulePerKilogramKelvin, 0.0001);
        //Assert.AreEqual(1941.4305998537359, Input2.Cv.JoulePerKilogramKelvin, 0.0001);
        //Assert.AreEqual(1726765.7255915655, Input2.Enthalpy.JoulePerKilogram, 0.0001);
        //Assert.AreEqual(1.0245751602823272, Input2.Prandtl, 0.0001);
        //Assert.AreEqual(11.999999999617073, Input2.Pressure.Bar, 0.0001);
        //Assert.AreEqual(7.9795686221167559, Input2.Density.KilogramPerCubicMeter, 0.0001);
        //Assert.AreEqual(1.4416543274283831, Input2.Entropy.CaloriePerGramKelvin, 0.0001);
        //Assert.AreEqual(0, Input2.SurfaceTension.NewtonPerMeter, 0.0001);
        //Assert.AreEqual(63, Input2.Temperature.DegreeCelsius, 0.0001);
        //Assert.AreEqual(30.9545445068116, Input2.Tsat.DegreeCelsius, 0.0001);
        //Assert.AreEqual(1.1345738919708291E-05, Input2.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        //Assert.AreEqual(-1, Input2.Quality, 0.0001);
        //Assert.AreEqual(437.77319868672936, Input2.SoundSpeed.MeterPerSecond, 0.0001);

        //Assert.AreEqual(17.03052, Input2.MolarMass.GramPerMole, 0.001);
        //Assert.AreEqual(0.91635253866863609, Input2.Compressibility, 0.0001);
        //Assert.AreEqual(1576381.6563733453, Input2.InternalEnergy.JoulePerKilogram, 0.0001);

        //Assert.AreEqual(1.4, Input2.MassFlow.KilogramPerSecond, 0.0001);
        //Assert.AreEqual(0.17544808075459839, Input2.VolumeFlow.CubicMeterPerSecond, 0.0001);

    }

    [TestMethod]
    public void AddToUsingMassFlowStartingWithZero()
    {

        //Arrange
        var Input1 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        Input1.MassFlow = MassFlow.FromKilogramPerSecond(2);
        Input1.UpdateDH(setDensity, setEnthalpy);

        var Input2 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(12);
        var setTemperature = Temperature.FromDegreeCelsius(63);
        Input2.MassFlow = MassFlow.FromKilogramPerSecond(1.4);
        Input2.UpdatePT(setPressure, setTemperature);

        var Mixer = new Fluid(FluidList.Ammonia);

        //Act
        Mixer.SetValuesToNull();
        _=Mixer.AddTo(Input1);
        _=Mixer.AddTo(Input2);

        //Assert
        //Assert.IsFalse(Mixer.FailState);
        //Assert.AreEqual(0.060912231081315084, Input1.LimitPressureMin.Bar, 0.0001);
        //Assert.AreEqual(10000, Input1.LimitPressureMax.Bar, 0.0001);
        //Assert.AreEqual(113.634, Input1.CriticalPressure.Bar, 0.0001);
        //Assert.AreEqual(-77.655, Input1.LimitTemperatureMin.DegreeCelsius, 0.0001);
        //Assert.AreEqual(132.41, Input1.CriticalTemperature.DegreeCelsius, 0.0001);
        //Assert.AreEqual(451.85, Input1.LimitTemperatureMax.DegreeCelsius, 0.0001);

        //Assert.AreEqual(0.0722711120542153, Mixer.Conductivity.WattPerMeterKelvin, 0.0001);
        //Assert.AreEqual(3251.34163624504, Mixer.Cp.JoulePerKilogramKelvin, 0.0001);
        //Assert.AreEqual(2270.36197650328, Mixer.Cv.JoulePerKilogramKelvin, 0.0001);
        //Assert.AreEqual(1974756.3106341641, Mixer.Enthalpy.JoulePerKilogram, 0.0001);
        //Assert.AreEqual(0.93888901877652886, Mixer.Prandtl, 0.0001);
        //Assert.AreEqual(74.334422054350568, Mixer.Pressure.Bar, 0.0001);
        //Assert.AreEqual(50, Mixer.Density.KilogramPerCubicMeter, 0.0001);
        //Assert.AreEqual(1.4371720270740429, Mixer.Entropy.CaloriePerGramKelvin, 0.0001);
        //Assert.AreEqual(0, Mixer.SurfaceTension.NewtonPerMeter, 0.0001);
        //Assert.AreEqual(189.701240030246, Mixer.Temperature.DegreeCelsius, 0.0001);
        //Assert.AreEqual(108.979422334718, Mixer.Tsat.DegreeCelsius, 0.0001);
        //Assert.AreEqual(2.0869708899873E-05, Mixer.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        //Assert.AreEqual(10000, Mixer.Quality, 0.0001);
        //Assert.AreEqual(544.366706172041, Mixer.SoundSpeed.MeterPerSecond, 0.0001);

        //Assert.AreEqual(17.03052, Mixer.MolarMass.GramPerMole, 0.001);
        //Assert.AreEqual(0.87621627068822328, Mixer.Compressibility, 0.0001);
        //Assert.AreEqual(1912412.68517865, Mixer.InternalEnergy.JoulePerKilogram, 0.0001);

        //Assert.AreEqual(3.4, Mixer.MassFlow.KilogramPerSecond, 0.0001);
        //Assert.AreEqual(0.068, Mixer.VolumeFlow.CubicMeterPerSecond, 0.0001);

        ////Assert
        //Assert.AreEqual(0.0609122310813151, Input2.LimitPressureMin.Bar, 0.0001);
        //Assert.AreEqual(10000, Input2.LimitPressureMax.Bar, 0.0001);
        //Assert.AreEqual(113.634, Input2.CriticalPressure.Bar, 0.0001);
        //Assert.AreEqual(-77.655, Input2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        //Assert.AreEqual(132.41, Input2.CriticalTemperature.DegreeCelsius, 0.0001);
        //Assert.AreEqual(451.85, Input2.LimitTemperatureMax.DegreeCelsius, 0.0001);

        //Assert.AreEqual(0.030105435932165674, Input2.Conductivity.WattPerMeterKelvin, 0.0001);
        //Assert.AreEqual(2718.666634571302, Input2.Cp.JoulePerKilogramKelvin, 0.0001);
        //Assert.AreEqual(1941.4305998537359, Input2.Cv.JoulePerKilogramKelvin, 0.0001);
        //Assert.AreEqual(1726765.7255915655, Input2.Enthalpy.JoulePerKilogram, 0.0001);
        //Assert.AreEqual(1.0245751602823272, Input2.Prandtl, 0.0001);
        //Assert.AreEqual(11.999999999617073, Input2.Pressure.Bar, 0.0001);
        //Assert.AreEqual(7.9795686221167559, Input2.Density.KilogramPerCubicMeter, 0.0001);
        //Assert.AreEqual(1.4416543274283831, Input2.Entropy.CaloriePerGramKelvin, 0.0001);
        //Assert.AreEqual(0, Input2.SurfaceTension.NewtonPerMeter, 0.0001);
        //Assert.AreEqual(63, Input2.Temperature.DegreeCelsius, 0.0001);
        //Assert.AreEqual(30.9545445068116, Input2.Tsat.DegreeCelsius, 0.0001);
        //Assert.AreEqual(1.1345738919708291E-05, Input2.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        //Assert.AreEqual(-1, Input2.Quality, 0.0001);
        //Assert.AreEqual(437.77319868672936, Input2.SoundSpeed.MeterPerSecond, 0.0001);

        //Assert.AreEqual(17.03052, Input2.MolarMass.GramPerMole, 0.001);
        //Assert.AreEqual(0.91635253866863609, Input2.Compressibility, 0.0001);
        //Assert.AreEqual(1576381.6563733453, Input2.InternalEnergy.JoulePerKilogram, 0.0001);

        //Assert.AreEqual(1.4, Input2.MassFlow.KilogramPerSecond, 0.0001);
        //Assert.AreEqual(0.17544808075459839, Input2.VolumeFlow.CubicMeterPerSecond, 0.0001);

    }

    [TestMethod]
    public void AddPowerMassFlow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        R717.MassFlow = MassFlow.FromKilogramPerSecond(2);
        R717.UpdateDH(setDensity, setEnthalpy);

        //Act
        _=R717.AddPower(Power.FromKilowatt(100));

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.0740824508482401, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(3196.92765925771, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2275.77928863739, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2198349.72016398, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(0.92442608105668511, R717.Prandtl, 0.0001);
        Assert.AreEqual(117.968517498023, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(47.8585478000212, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(1.4554031513314867, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(293.906571452989, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(2.14217389356299E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, R717.Quality, 0.0001);
        Assert.AreEqual(555.853464477809, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.89037731875072978, R717.Compressibility, 0.0001);
        Assert.AreEqual(1951855.57664972, R717.InternalEnergy.JoulePerKilogram, 0.0001);

    }
    [TestMethod]
    public void AddPowerMassFlowMAX()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        R717.MassFlow = MassFlow.FromKilogramPerSecond(2);
        R717.UpdateDH(setDensity, setEnthalpy);

        //Act
        _=R717.AddPower(Power.FromKilowatt(99999999));

        Debug.Print((R717.LimitTemperatureMax == R717.Temperature).ToString());

        //Assert
        Assert.IsTrue(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.0859782428759609, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(3097.85594384625, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2441.80870244021, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2688514.39365347, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(0.97573511970514171, R717.Prandtl, 0.0001);
        Assert.AreEqual(117.968517498023, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(34.477070684444, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(6852.38893489242, R717.Entropy.JoulePerKilogramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(451.85, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(2.70806624405054E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, R717.Quality, 0.0001);
        Assert.AreEqual(648.988154438777, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.96669979536621986, R717.Compressibility, 0.0001);
        Assert.AreEqual(2346349.25273296, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.058009568687121579, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);

    }

    [TestMethod]
    public void RemovePowerMassFlow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        R717.MassFlow = MassFlow.FromKilogramPerSecond(2);
        R717.UpdateDH(setDensity, setEnthalpy);

        //Act
        _=R717.RemovePower(Power.FromKilowatt(100));

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.07053387608846, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(3322.84286345408, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2269.0067689874, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2098349.72016398, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(0.95791851419327612, R717.Prandtl, 0.0001);
        Assert.AreEqual(117.968517492702, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(52.3793974219741, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(5908.06618790933, R717.Entropy.JoulePerKilogramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(263.17307543127, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(2.03337047701124E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, R717.Quality, 0.0001);
        Assert.AreEqual(532.403853045307, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.86014765870726462, R717.Compressibility, 0.0001);
        Assert.AreEqual(1873130.41009377, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.038182951664903347, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);

    }

    [TestMethod]
    public void RemovePowerMassFlowMin()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        R717.MassFlow = MassFlow.FromKilogramPerSecond(2);
        R717.UpdateDH(setDensity, setEnthalpy);

        //Act
        _=R717.RemovePower(Power.FromKilowatt(99999999));

        //Assert
        Assert.IsTrue(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.836238283804608, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(4291.45072618964, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2977.0257935309, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(11216.0474289283, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(3.0501801705032769, R717.Prandtl, 0.0001);
        Assert.AreEqual(117.968517492702, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(737.991789248866, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(-0.00588517138163891, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(R717.LimitTemperatureMin, R717.Temperature);
        Assert.AreEqual(132.41, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(0.000594362511378813, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, R717.Quality, 0.0001);
        Assert.AreEqual(2045.83348665104, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.16748380087109294, R717.Compressibility, 0.0001);
        Assert.AreEqual(-4769.02438505104, R717.InternalEnergy.JoulePerKilogram, 0.0001);

    }

    [TestMethod]
    public void TsatBelowCrit()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var pressure = Pressure.FromBar(20);
        R717.UpdatePX(pressure, 0.5);

        //Act
        Temperature Tsat = R717.Tsat;

        //Assert
        Assert.AreEqual(Tsat.DegreeCelsius, 49.371451247030961, 0.001);

    }

    [TestMethod]
    public void TsatOnCrit()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        Pressure pressure = R717.CriticalPressure;
        R717.UpdatePX(pressure, 0.5);

        //Act
        Temperature Tsat = R717.Tsat;

        //Assert
        Assert.AreEqual(Tsat, R717.CriticalTemperature);

    }

    [TestMethod]
    public void TsatAboveCrit()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        R717.UpdatePT(R717.LimitPressureMax, R717.LimitTemperatureMax);

        //Act
        Temperature Tsat = R717.Tsat;

        //Assert
        Assert.AreEqual(Tsat, R717.CriticalTemperature);

    }

    [TestMethod]
    public void GetSatTemperature()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var pressure = Pressure.FromBar(20);

        //Act
        Temperature temperature = R717.GetSatTemperature(pressure);

        //Assert
        Assert.AreEqual(temperature.DegreeCelsius, 49.371451247030279, 0.001);

    }

    [TestMethod]
    public void GetSatTemperatureAboveCrit()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        Pressure pressure = R717.LimitPressureMax;

        //Act
        Temperature temperature = R717.GetSatTemperature(pressure);

        //Assert
        Assert.AreEqual(temperature, R717.CriticalTemperature);

    }

    [TestMethod]
    public void GetSatPressure()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var temperature = Temperature.FromDegreeCelsius(30);

        //Act
        Pressure pressure = R717.GetSatPressure(temperature);

        //Assert
        Assert.AreEqual(pressure.Bar, 11.665360629887157, 0.001);

    }

    [TestMethod]
    public void GetSatPressureAboveCrit()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        Temperature temperature = R717.LimitTemperatureMax;

        //Act
        Pressure pressure = R717.GetSatPressure(temperature);

        //Assert
        Assert.AreEqual(pressure.Bar, R717.CriticalPressure.Bar, 0.001);

    }

    [TestMethod]
    public void MinMaxFractionLimit()
    {

        var mixref = new Fluid(FluidList.MixAmmoniaAQ);

        //Assert
        Assert.AreEqual(0, mixref.FractionMin);
        Assert.AreEqual(0.3, mixref.FractionMax);

    }

    [TestMethod]
    public void UpdateTSFunctionality()
    {
        var water = new Fluid(FluidList.Water);
        var temperature = Temperature.FromKelvins(286.15);
        var entropy = SpecificEntropy.FromJoulePerKilogramKelvin(195.27);
        water.UpdateTS(temperature, entropy);

        Assert.AreEqual(0.082063, water.Pressure.Megapascal, 0.001);

        var ammonia = new Fluid(FluidList.Ammonia);
        temperature = Temperature.FromKelvins(286.15);
        entropy = SpecificEntropy.FromJoulePerKilogramKelvin(1697.7);
        ammonia.UpdateTS(temperature, entropy);

        Assert.AreEqual(0.93555, ammonia.Pressure.Megapascal, 0.001);

        var co2 = new Fluid(FluidList.CO2);
        temperature = Temperature.FromKelvins(286.15);
        entropy = SpecificEntropy.FromJoulePerKilogramKelvin(2220.2);
        co2.UpdateTS(temperature, entropy);

        Assert.AreEqual(1.1240, co2.Pressure.Megapascal, 0.001);

    }

    [TestMethod]// (Skip = "race condition demonstration")]
    public void ParallelAccess2()
    {

        List<Task> tasks = [];
        for (var i = 1; i < 400; ++i)
        {
            tasks.Add(Task.Run(dividedUnit));
        }

        Task.WaitAll(tasks.ToArray());

        //Local function
        static void dividedUnit()
        {
            var water = new Fluid(FluidList.Water);
            var temperature = Temperature.FromKelvins(286.15);
            var entropy = SpecificEntropy.FromJoulePerKilogramKelvin(195.27);
            water.UpdateTS(temperature, entropy);
        }
    }

    [TestMethod]
    public void GasDensity()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        R717.UpdatePX(Pressure.FromBar(10), 0.5);

        //Act
        Density Density = R717.Density;
        Density Gasdensity = R717.GasDensity;
        Density Liqdensity = R717.LiquidDensity;

        //Assert
        Assert.IsTrue(Gasdensity < Density);
        Assert.IsTrue(Density < Liqdensity);

    }
    [TestMethod]
    public void GasDensityOutSide()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        R717.UpdatePX(Pressure.FromBar(10), 1);
        R717.UpdatePT(R717.Pressure, R717.Temperature + Temperature.FromKelvins(10));

        //Act
        Density Density = R717.Density;
        Density Gasdensity = R717.GasDensity;
        Density Liqdensity = R717.LiquidDensity;

        //Assert
        Assert.IsTrue(Gasdensity == Density);
        Assert.IsTrue(Density == Liqdensity);

    }

    [TestMethod]
    public void GasDensityOutSide2()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        R717.UpdatePX(Pressure.FromBar(10), 0);
        R717.UpdatePT(R717.Pressure, R717.Temperature - Temperature.FromKelvins(10));

        //Act
        Density Density = R717.Density;
        Density Gasdensity = R717.GasDensity;
        Density Liqdensity = R717.LiquidDensity;

        //Assert
        Assert.IsTrue(Gasdensity == Density);
        Assert.IsTrue(Density == Liqdensity);

    }

    [TestMethod]
    public void GasDensityAbove()
    {

        Log.Logger = new LoggerConfiguration()
           .WriteTo.Debug()
           .CreateLogger();

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);

        R717.UpdatePT(R717.CriticalPressure + Pressure.FromBar(10), R717.CriticalTemperature + Temperature.FromKelvins(25));

        //Act
        Density Density = R717.Density;
        Density Gasdensity = R717.GasDensity;
        Density Liqdensity = R717.LiquidDensity;

        //Assert
        Assert.IsTrue(Gasdensity == Density);
        Assert.IsTrue(Density == Liqdensity);
        Assert.IsTrue(Density != Density.Zero);

    }
}