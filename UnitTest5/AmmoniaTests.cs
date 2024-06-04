//using EngineeringUnits;
using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;

namespace UnitsTests;

[TestClass]
public class AmmoniaTests
{

    [TestMethod]
    public void UpdateDH()
    {


        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        R717.MassFlow = setMassFlow;

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
    }
    [TestMethod]
    public void UpdateDHFailedTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(-5);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(-100);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdateDHFailedTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(1000000);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(10000000);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDH(setDensity, setEnthalpy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }

    [TestMethod]
    public void UpdateDP()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(15.362783819911829);
        var setPressure = Pressure.FromBar(10);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDP(setDensity, setPressure);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(4946.6414603438907, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2789.2425141052067, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
        Assert.AreEqual(10, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(15.362783819911829, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(24.912702090002085, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(24.912702089993616, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0.5, R717.Quality, 0.0001);
        Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
        Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }
    [TestMethod]
    public void UpdateDPTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(-1);
        var setPressure = Pressure.FromBar(-10);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDP(setDensity, setPressure);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdateDPTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(100000000);
        var setPressure = Pressure.FromBar(100000);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDP(setDensity, setPressure);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }

    [TestMethod]
    public void UpdateDS()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(50);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(6000);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDS(setDensity, setEntropy);
        R717.MassFlow = setMassFlow;

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
        Assert.AreEqual(0.938889018776402, R717.Prandtl, 0.0001);
        Assert.AreEqual(117.968517492664, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(50, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(1.434034416826004, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        //Assert.AreEqual(0, R717.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(278.39210805142, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(2.08697088998702E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(10000, R717.Quality, 0.0001);
        Assert.AreEqual(544.366706172041, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.87621627068822361, R717.Compressibility, 0.0001);
        Assert.AreEqual(1912412.68517865, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.04, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }
    [TestMethod]
    public void UpdateDSTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(-15.362783819911829);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(-3846.6363350450893);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDS(setDensity, setEntropy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdateDSTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(1000000);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(1000000000);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDS(setDensity, setEntropy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }

    [TestMethod]
    public void UpdateDT()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(15.362783819911829);
        var setTemperature = Temperature.FromDegreeCelsius(24.912702090002085);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDT(setDensity, setTemperature);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(4946.6414603438907, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2789.2425141052067, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
        Assert.AreEqual(10, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(15.362783819911829, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(24.912702090002085, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(24.912702089993616, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0.5, R717.Quality, 0.0001);
        Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
        Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }
    [TestMethod]
    public void UpdateDTTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(-15.362783819911829);
        var setTemperature = Temperature.FromDegreeCelsius(-24.912702090002085);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDT(setDensity, setTemperature);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdateDTTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setDensity = Density.FromKilogramPerCubicMeter(90000);
        var setTemperature = Temperature.FromDegreeCelsius(90000);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateDT(setDensity, setTemperature);
        R717.MassFlow = setMassFlow;

        //Assert
        //Nothing is too high...
    }

    [TestMethod]
    public void UpdateHS()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(1045846.5098055181);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(3846.6363350450893);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateHS(setEnthalpy, setEntropy);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(4946.6414603438907, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2789.2425141052067, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
        Assert.AreEqual(10, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(15.362783819911829, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(24.912702090002085, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(24.912702089993616, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0.5, R717.Quality, 0.0001);
        Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
        Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }
    [TestMethod]
    public void UpdateHSTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(-1045000000846.5098055181);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(-38000000046.6363350450893);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateHS(setEnthalpy, setEntropy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdateHSTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(104500000000846.5098055181);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(3800000000046.6363350450893);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateHS(setEnthalpy, setEntropy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }

    [TestMethod]
    public void UpdatePH()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(10);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(1045846.5098055181);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePH(setPressure, setEnthalpy);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(4946.6414603438907, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2789.2425141052067, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
        Assert.AreEqual(10, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(15.362783819911829, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(24.912702090002085, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(24.912702089993616, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0.5, R717.Quality, 0.0001);
        Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
        Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }
    [TestMethod]
    public void UpdatePHTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(-10);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(-1045846.5098055181);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePH(setPressure, setEnthalpy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdatePHTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(100000);
        var setEnthalpy = SpecificEnergy.FromJoulePerKilogram(104580046.5098055181);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePH(setPressure, setEnthalpy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }

    [TestMethod]
    public void UpdatePS()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(10);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(3846.6363350450893);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePS(setPressure, setEntropy);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(4946.6414603438907, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2789.2425141052067, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
        Assert.AreEqual(10, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(15.362783819911829, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(24.912702090002085, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(24.912702089993616, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0.5, R717.Quality, 0.0001);
        Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
        Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }
    [TestMethod]
    public void UpdatePSTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(-10);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(-3846.6363350450893);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePS(setPressure, setEntropy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdatePSTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(1000000000);
        var setEntropy = SpecificEntropy.FromJoulePerKilogramKelvin(38400000006.6363350450893);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePS(setPressure, setEntropy);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }

    [TestMethod]
    public void UpdatePT()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(10);
        var setTemperature = Temperature.FromDegreeCelsius(100);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePT(setPressure, setTemperature);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.034349411318899653, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(2464.8830983211074, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1843.882651806066, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1829344.4765594436, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(0.92250631570868957, R717.Prandtl, 0.0001);
        Assert.AreEqual(10, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(5.7513489580705048, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(1.5308777308424815, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(100, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(24.912702090001176, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(1.2855599076541866E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, R717.Quality, 0.0001);
        Assert.AreEqual(470.62060167332942, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.95442258123496115, R717.Compressibility, 0.0001);
        Assert.AreEqual(1655472.2237557317, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.34774450560742387, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }
    [TestMethod]
    public void UpdatePTTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(-10);
        var setTemperature = Temperature.FromDegreeCelsius(-1000);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePT(setPressure, setTemperature);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdatePTTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(9000000000000000000);
        var setTemperature = Temperature.FromDegreeCelsius(9000000000000000000);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePT(setPressure, setTemperature);
        R717.MassFlow = setMassFlow;

        //Assert
        //Nothing is too high....
    }

    [TestMethod]
    public void UpdatePTCrit()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePT(R717.CriticalPressure, R717.CriticalTemperature + Temperature.FromKelvins(0.1));
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
    }

    [TestMethod]
    public void UpdatePX()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(10);
        var X = 0.5;
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePX(setPressure, X);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(4946.6414603438907, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2789.2425141052067, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
        Assert.AreEqual(10, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(15.362783819911829, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(24.912702090002085, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(24.912702089993616, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0.5, R717.Quality, 0.0001);
        Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
        Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }

    [TestMethod]
    public void UpdatePXTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(-10);
        var X = -0.5;
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePX(setPressure, X);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }
    [TestMethod]
    public void UpdatePXTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setPressure = Pressure.FromBar(10000000);
        var X = 0.5;
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePX(setPressure, X);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsTrue(R717.FailState);

        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        //Assert.AreEqual(0, R717.Conductivity.WattPerMeterKelvin);
        //Assert.AreEqual(0, R717.Cp.JoulePerKilogramKelvin);
        //Assert.AreEqual(0, R717.Cv.JoulePerKilogramKelvin);
        //Assert.AreEqual(0, R717.Enthalpy.JoulePerKilogram);
        //Assert.AreEqual(0, R717.Prandtl);
        //Assert.AreEqual(0, R717.Pressure.Bar);
        //Assert.AreEqual(0, R717.Density.KilogramPerCubicMeter);
        //Assert.AreEqual(0, R717.Entropy.JoulePerKilogramKelvin);
        //Assert.AreEqual(0, R717.SurfaceTension.NewtonPerMeter);
        //Assert.AreEqual(0, R717.Temperature.Kelvins);
        //Assert.AreEqual(0, R717.Tsat.Kelvins);
        //Assert.AreEqual(0, R717.DynamicViscosity.NewtonSecondPerMeterSquared);
        //Assert.AreEqual(0, R717.Quality);
        //Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond);

        //Assert.AreEqual(0, R717.MolarMass.GramPerMole);
        //Assert.AreEqual(0, R717.Compressibility);
        //Assert.AreEqual(0, R717.InternalEnergy.JoulePerKilogram);

        //Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        //Assert.AreEqual(0, R717.VolumeFlow.CubicMeterPerSecond);
    }

    [TestMethod]
    public void UpdatePXCrit()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var R717X0 = new Fluid(FluidList.Ammonia);
        Pressure setPressure = R717.CriticalPressure;
        double X = 1;
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdatePX(setPressure, X);
        R717X0.UpdatePX(setPressure, 0);
        R717.MassFlow = setMassFlow;
        R717X0.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        //Assert.AreEqual(0, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(73158204704795.4, R717.Cp.JoulePerKilogramKelvin, 1);
        Assert.AreEqual(6467.97163351429, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1247802.1607166, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(2513829873.6623368, R717.Prandtl, 0.0001);
        Assert.AreEqual(R717.CriticalPressure.Bar, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(233.25000192, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.95486829094462955, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(R717.SurfaceTension);
        Assert.AreEqual(R717.CriticalTemperature.DegreeCelsius, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(R717.CriticalTemperature.DegreeCelsius, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(2.7906634676727E-05, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(1, R717.Quality, 0.0001);
        Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.24605095619157291, R717.Compressibility, 0.0001);
        Assert.AreEqual(1199084.51411734, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.008574490819022413, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);

        //Compare X=0 and X=1
        Assert.AreEqual(R717X0.Conductivity.WattPerMeterKelvin, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(R717X0.Cp.JoulePerKilogramKelvin, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(R717X0.Cv.JoulePerKilogramKelvin, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(R717X0.Enthalpy.JoulePerKilogram, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(R717X0.Prandtl, R717.Prandtl, 0.0001);
        Assert.AreEqual(R717X0.Pressure.Bar, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(R717X0.Density.KilogramPerCubicMeter, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(R717X0.Entropy.CaloriePerGramKelvin, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(R717X0.SurfaceTension);
        Assert.AreEqual(R717X0.Temperature.DegreeCelsius, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(R717X0.Tsat.DegreeCelsius, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(R717X0.DynamicViscosity.NewtonSecondPerMeterSquared, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(R717X0.SoundSpeed.MeterPerSecond, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(R717X0.MolarMass.GramPerMole, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(R717X0.Compressibility, R717.Compressibility, 0.0001);
        Assert.AreEqual(R717X0.InternalEnergy.JoulePerKilogram, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(R717X0.MassFlow.KilogramPerSecond, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(R717X0.VolumeFlow.CubicMeterPerSecond, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);

    }

    [TestMethod]
    public void UpdateXT()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setTemperature = Temperature.FromDegreeCelsius(24.912702090002085);
        var X = 0.5;
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateXT(X, setTemperature);
        R717.MassFlow = setMassFlow;

        //Assert
        Assert.IsFalse(R717.FailState);
        Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(10000, R717.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(113.634, R717.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(132.41, R717.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(4946.6414603438907, R717.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(2789.2425141052067, R717.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
        Assert.AreEqual(10, R717.Pressure.Bar, 0.0001);
        Assert.AreEqual(15.362783819911829, R717.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(24.912702090002085, R717.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(24.912702089993616, R717.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0.5, R717.Quality, 0.0001);
        Assert.AreEqual(0, R717.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(17.03052, R717.MolarMass.GramPerMole, 0.001);
        Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
        Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond, 0.0001);
        Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMeterPerSecond, 0.0001);
    }
    [TestMethod]
    public void UpdateXTTooLow()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setTemperature = Temperature.FromDegreeCelsius(-204.912702090002085);
        var X = 0.5;
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateXT(X, setTemperature);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }

    [TestMethod]
    public void UpdateXTTooHigh()
    {

        //Arrange
        var R717 = new Fluid(FluidList.Ammonia);
        var setTemperature = Temperature.FromDegreeCelsius(20000000000004);
        double X = 100;
        var setMassFlow = MassFlow.FromKilogramPerSecond(2);

        //Act
        R717.UpdateXT(X, setTemperature);
        R717.MassFlow = setMassFlow;

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

        Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
        Assert.AreEqual(null, R717.VolumeFlow);
    }

    [TestMethod]
    public void Phase()
    {
        var amonia = new Fluid(FluidList.Ammonia);

        amonia.UpdatePT(Pressure.FromPascal(582999.74), Temperature.FromKelvins(282));
        Assert.AreEqual(SharpFluids.Phases.Gas, amonia.Phase, "Should be in gas phase");

        amonia.UpdatePT(Pressure.FromPascal(582999.74), Temperature.FromKelvins(281));
        Assert.AreEqual(SharpFluids.Phases.Liquid, amonia.Phase, "Should be in liquid phase");

        amonia.UpdatePX(Pressure.FromPascal(582999.74), 0.5);
        Assert.AreEqual(SharpFluids.Phases.Twophase, amonia.Phase, "Should be in twophase");

        amonia.UpdatePT(Pressure.FromPascal(11433000.0), Temperature.FromKelvins(410.4));
        Assert.AreEqual(SharpFluids.Phases.Supercritical, amonia.Phase, "Should be in supercritical phase");

        amonia.UpdatePT(Pressure.FromPascal(11233000.0), Temperature.FromKelvins(410.4));
        Assert.AreEqual(SharpFluids.Phases.SupercriticalGas, amonia.Phase, "Should be in supercritical gas phase");

        amonia.UpdatePT(Pressure.FromPascal(11433000.0), Temperature.FromKelvins(400.4));
        Assert.AreEqual(SharpFluids.Phases.SupercriticalLiquid, amonia.Phase, "Should be in supercritical liquid phase");
    }
}