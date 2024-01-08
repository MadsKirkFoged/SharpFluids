using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SharpFluids;
//using EngineeringUnits;
using EngineeringUnits;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace UnitsTests
{
    [TestClass]
    public class FunctionalityTests
    {

        [TestMethod]
        public void IntoJsonAndBackAgainMassFlow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);

            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;

            //Save as JSON
            string json = R717.SaveAsJSON();


            //Start new fluid and load as json
            Fluid R717JSON = R717.LoadFromJSON(json);




            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.0722711120542153, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(3251.34163624504, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2270.36197650328, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2148349.72016398, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.93888901877652886, R717.Prandtl, 0.0001);
            Assert.AreEqual(117.968517492664, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(50, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(6000, R717.Entropy.JoulesPerKilogramKelvin, 0.0001);
            Assert.IsNull(R717.SurfaceTension);
            Assert.AreEqual(278.392108051419, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.0869708899873E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(10000, R717.Quality, 0.0001);
            Assert.AreEqual(544.366706172041, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.87621627068822328, R717.Compressibility, 0.0001);
            Assert.AreEqual(1912412.68517865, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.04, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert JSON
            Assert.IsFalse(R717JSON.FailState);
            Assert.AreEqual(R717JSON.LimitPressureMin.Bars, R717.LimitPressureMin.Bars);
            Assert.AreEqual(R717JSON.LimitPressureMax.Bars, R717.LimitPressureMax.Bars);
            Assert.AreEqual(R717JSON.CriticalPressure.Bars, R717.CriticalPressure.Bars);
            Assert.AreEqual(R717JSON.LimitTemperatureMin.DegreesCelsius, R717.LimitTemperatureMin.DegreesCelsius);
            Assert.AreEqual(R717JSON.CriticalTemperature.DegreesCelsius, R717.CriticalTemperature.DegreesCelsius);
            Assert.AreEqual(R717JSON.LimitTemperatureMax.DegreesCelsius, R717.LimitTemperatureMax.DegreesCelsius);


            Assert.AreEqual(R717JSON.Conductivity.WattsPerMeterKelvin, R717.Conductivity.WattsPerMeterKelvin);
            Assert.AreEqual(R717JSON.Cp.JoulesPerKilogramKelvin, R717.Cp.JoulesPerKilogramKelvin);
            Assert.AreEqual(R717JSON.Cv.JoulesPerKilogramKelvin, R717.Cv.JoulesPerKilogramKelvin);
            Assert.AreEqual(R717JSON.Enthalpy.JoulesPerKilogram, R717.Enthalpy.JoulesPerKilogram);
            Assert.AreEqual(R717JSON.Prandtl, R717.Prandtl);
            Assert.AreEqual(R717JSON.Pressure.Bars, R717.Pressure.Bars);
            Assert.AreEqual(R717JSON.Density.KilogramsPerCubicMeter, R717.Density.KilogramsPerCubicMeter);
            Assert.AreEqual(R717JSON.Entropy.KilocaloriesPerGramKelvin, R717.Entropy.KilocaloriesPerGramKelvin);
            Assert.IsNull(R717JSON.SurfaceTension);
            Assert.AreEqual(R717JSON.Temperature.DegreesCelsius, R717.Temperature.DegreesCelsius);
            Assert.AreEqual(R717JSON.Tsat.DegreesCelsius, R717.Tsat.DegreesCelsius, 0.00001);
            Assert.AreEqual(R717JSON.DynamicViscosity.NewtonSecondsPerMeterSquared, R717.DynamicViscosity.NewtonSecondsPerMeterSquared);
            Assert.AreEqual(R717JSON.Quality, R717.Quality);
            Assert.AreEqual(R717JSON.SoundSpeed.MetersPerSecond, R717.SoundSpeed.MetersPerSecond);

            Assert.AreEqual(R717JSON.MolarMass.GramsPerMole, R717.MolarMass.GramsPerMole);
            Assert.AreEqual(R717JSON.Compressibility, R717.Compressibility);
            Assert.AreEqual(R717JSON.InternalEnergy.JoulesPerKilogram, R717.InternalEnergy.JoulesPerKilogram);

            Assert.AreEqual(R717JSON.MassFlow.KilogramsPerSecond, R717.MassFlow.KilogramsPerSecond);
            Assert.AreEqual(R717JSON.VolumeFlow.CubicMetersPerSecond, R717.VolumeFlow.CubicMetersPerSecond);

            Assert.AreEqual(R717JSON.Media, R717.Media);
        }

        [TestMethod]
        public void IntoJsonAndBackAgainMass()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            Mass setMass = Mass.FromKilograms(43);

            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.Mass = setMass;

            //Save as JSON
            string json = R717.SaveAsJSON();


            //Start new fluid and load as json
            Fluid R717JSON = R717.LoadFromJSON(json);


            Assert.AreEqual(43, R717.Mass.Kilograms, 0.0001);
            Assert.AreEqual(0.86, R717.Volume.CubicMeters, 0.0001);

            //Assert JSON          
            Assert.AreEqual(R717JSON.MassFlow, R717.MassFlow);
            Assert.AreEqual(R717JSON.VolumeFlow, R717.VolumeFlow);
        }

        [TestMethod]
        public void LoadValuesThenJSON()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Save as JSON
            string json = R717.SaveAsJSON();

            //Start new fluid and load as json
            Fluid R717JSON = R717.LoadFromJSON(json);


            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;
            R717JSON.UpdateDH(setDensity, setEnthalpy);
            R717JSON.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.0722711120542153, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(3251.34163624504, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2270.36197650328, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2148349.72016398, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.93888901877652886, R717.Prandtl, 0.0001);
            Assert.AreEqual(117.968517492664, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(50, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(6000, R717.Entropy.JoulesPerKilogramKelvin, 0.0001);
            Assert.IsNull(R717.SurfaceTension);
            Assert.AreEqual(278.392108051419, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.0869708899873E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(10000, R717.Quality, 0.0001);
            Assert.AreEqual(544.366706172041, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.87621627068822328, R717.Compressibility, 0.0001);
            Assert.AreEqual(1912412.68517865, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.04, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert JSON
            Assert.IsFalse(R717JSON.FailState);
            Assert.AreEqual(R717JSON.LimitPressureMin.Bars, R717.LimitPressureMin.Bars);
            Assert.AreEqual(R717JSON.LimitPressureMax.Bars, R717.LimitPressureMax.Bars);
            Assert.AreEqual(R717JSON.CriticalPressure.Bars, R717.CriticalPressure.Bars);
            Assert.AreEqual(R717JSON.LimitTemperatureMin.DegreesCelsius, R717.LimitTemperatureMin.DegreesCelsius);
            Assert.AreEqual(R717JSON.CriticalTemperature.DegreesCelsius, R717.CriticalTemperature.DegreesCelsius);
            Assert.AreEqual(R717JSON.LimitTemperatureMax.DegreesCelsius, R717.LimitTemperatureMax.DegreesCelsius);


            Assert.AreEqual(R717JSON.Conductivity.WattsPerMeterKelvin, R717.Conductivity.WattsPerMeterKelvin);
            Assert.AreEqual(R717JSON.Cp.JoulesPerKilogramKelvin, R717.Cp.JoulesPerKilogramKelvin);
            Assert.AreEqual(R717JSON.Cv.JoulesPerKilogramKelvin, R717.Cv.JoulesPerKilogramKelvin);
            Assert.AreEqual(R717JSON.Enthalpy.JoulesPerKilogram, R717.Enthalpy.JoulesPerKilogram);
            Assert.AreEqual(R717JSON.Prandtl, R717.Prandtl);
            Assert.AreEqual(R717JSON.Pressure.Bars, R717.Pressure.Bars);
            Assert.AreEqual(R717JSON.Density.KilogramsPerCubicMeter, R717.Density.KilogramsPerCubicMeter);
            Assert.AreEqual(R717JSON.Entropy.KilocaloriesPerGramKelvin, R717.Entropy.KilocaloriesPerGramKelvin);
            Assert.IsNull(R717JSON.SurfaceTension);
            Assert.AreEqual(R717JSON.Temperature.DegreesCelsius, R717.Temperature.DegreesCelsius);
            Assert.AreEqual(R717JSON.Tsat.DegreesCelsius, R717.Tsat.DegreesCelsius);
            Assert.AreEqual(R717JSON.DynamicViscosity.NewtonSecondsPerMeterSquared, R717.DynamicViscosity.NewtonSecondsPerMeterSquared);
            Assert.AreEqual(R717JSON.Quality, R717.Quality);
            Assert.AreEqual(R717JSON.SoundSpeed.MetersPerSecond, R717.SoundSpeed.MetersPerSecond);

            Assert.AreEqual(R717JSON.MolarMass.GramsPerMole, R717.MolarMass.GramsPerMole);
            Assert.AreEqual(R717JSON.Compressibility, R717.Compressibility);
            Assert.AreEqual(R717JSON.InternalEnergy.JoulesPerKilogram, R717.InternalEnergy.JoulesPerKilogram);

            Assert.AreEqual(R717JSON.MassFlow.KilogramsPerSecond, R717.MassFlow.KilogramsPerSecond);
            Assert.AreEqual(R717JSON.VolumeFlow.CubicMetersPerSecond, R717.VolumeFlow.CubicMetersPerSecond);

            Assert.AreEqual(R717JSON.Media, R717.Media);
        }

        [TestMethod]
        public void FailStateAfterJSON()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            string json = R717.SaveAsJSON();
            Fluid R717JSON = R717.LoadFromJSON(json);

            //Asking for Tsat
            Temperature dummy = R717JSON.Tsat;


            Assert.IsFalse(R717JSON.FailState);


        }


        [TestMethod]
        public void SavingJSONWithoutPointersInTheDLL()
        {

            //This test was made after we found out that the CoolProp DLL was saving pointers in JSON
            //..which made things works fine on your local computer but crash when using DBs and servers!


            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Fluid R7172 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);

            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;
            string R717JSON = R717.SaveAsJSON();

            R7172.UpdateDH(setDensity, setEnthalpy);
            R7172.MassFlow = setMassFlow;
            string R7172JSON = R7172.SaveAsJSON();




            //Assert JSON          
            Assert.AreEqual(R717JSON, R7172JSON);

        }




        [TestMethod]
        public void CopyFuntional()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);

            //Act
            R717.UpdateDH(setDensity, setEnthalpy);


            //Start new fluid and load as json
            Fluid R717JSON = new Fluid();
            R717JSON.Copy(R717);



            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.0722711120542153, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(3251.34163624504, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2270.36197650328, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2148349.72016398, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.93888901877652886, R717.Prandtl, 0.0001);
            Assert.AreEqual(117.968517492664, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(50, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(6000, R717.Entropy.JoulesPerKilogramKelvin, 0.0001);
            Assert.IsNull(R717.SurfaceTension);
            Assert.AreEqual(278.392108051419, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.0869708899873E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(10000, R717.Quality, 0.0001);
            Assert.AreEqual(544.366706172041, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.87621627068822328, R717.Compressibility, 0.0001);
            Assert.AreEqual(1912412.68517865, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            //Assert JSON
            Assert.IsFalse(R717JSON.FailState);
            Assert.AreEqual(R717JSON.LimitPressureMin.Bars, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(R717JSON.LimitPressureMax.Bars, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(R717JSON.CriticalPressure.Bars, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(R717JSON.LimitTemperatureMin.DegreesCelsius, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.CriticalTemperature.DegreesCelsius, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.LimitTemperatureMax.DegreesCelsius, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(R717JSON.Conductivity.WattsPerMeterKelvin, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(R717JSON.Cp.JoulesPerKilogramKelvin, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(R717JSON.Cv.JoulesPerKilogramKelvin, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(R717JSON.Enthalpy.JoulesPerKilogram, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(R717JSON.Prandtl, R717.Prandtl, 0.0001);
            Assert.AreEqual(R717JSON.Pressure.Bars, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(R717JSON.Density.KilogramsPerCubicMeter, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(R717JSON.Entropy.KilocaloriesPerGramKelvin, R717.Entropy.KilocaloriesPerGramKelvin, 0.0001);
            Assert.IsNull(R717JSON.SurfaceTension);
            Assert.AreEqual(R717JSON.Temperature.DegreesCelsius, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.Tsat.DegreesCelsius, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.DynamicViscosity.NewtonSecondsPerMeterSquared, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(R717JSON.Quality, R717.Quality, 0.0001);
            Assert.AreEqual(R717JSON.SoundSpeed.MetersPerSecond, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(R717.MolarMass.GramsPerMole, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(R717.Compressibility, R717.Compressibility, 0.0001);
            Assert.AreEqual(R717.InternalEnergy.JoulesPerKilogram, R717.InternalEnergy.JoulesPerKilogram, 0.0001);
        }

        [TestMethod]
        public void UpdateStartValues()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);



            //Assert
            Assert.IsTrue(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


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
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);

            //Act
            R717.UpdateDH(setDensity, setEnthalpy);

            R717.SetValuesToNull();


            //Assert
            Assert.IsTrue(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

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
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Start new fluid
            Fluid CO2 = new Fluid();
            CO2.CopyType(R717);



            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

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
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Start new fluid
            Fluid CO2 = new Fluid(FluidList.CO2);
            CO2.UpdatePT(Pressure.FromBars(30), Temperature.FromDegreesCelsius(60));
            CO2.CopyType(R717);



            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, CO2.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, CO2.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, CO2.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, CO2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, CO2.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, CO2.LimitTemperatureMax.DegreesCelsius, 0.0001);

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
            Fluid R717 = new Fluid();
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);


            Assert.ThrowsException<InvalidOperationException>(() => R717.UpdateDH(setDensity, setEnthalpy));


            
        }

        [TestMethod]
        public void AddToUsingMassFlow()
        {

            //Arrange
            Fluid Input1 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            Input1.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            Input1.UpdateDH(setDensity, setEnthalpy);

            Fluid Input2 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(12);
            Temperature setTemperature = Temperature.FromDegreesCelsius(63);
            Input2.MassFlow = MassFlow.FromKilogramsPerSecond(1.4);
            Input2.UpdatePT(setPressure, setTemperature);


            //Act
            Input1.AddTo(Input2);



            //Assert
            //Assert.IsFalse(Input1.FailState);
            //Assert.AreEqual(0.060912231081315084, Input1.LimitPressureMin.Bars, 0.0001);
            //Assert.AreEqual(10000, Input1.LimitPressureMax.Bars, 0.0001);
            //Assert.AreEqual(113.634, Input1.CriticalPressure.Bars, 0.0001);
            //Assert.AreEqual(-77.655, Input1.LimitTemperatureMin.DegreesCelsius, 0.0001);
            //Assert.AreEqual(132.41, Input1.CriticalTemperature.DegreesCelsius, 0.0001);
            //Assert.AreEqual(451.85, Input1.LimitTemperatureMax.DegreesCelsius, 0.0001);

            //Assert.AreEqual(0.0722711120542153, Input1.Conductivity.WattsPerMeterKelvin, 0.0001);
            //Assert.AreEqual(3251.34163624504, Input1.Cp.JoulesPerKilogramKelvin, 0.0001);
            //Assert.AreEqual(2270.36197650328, Input1.Cv.JoulesPerKilogramKelvin, 0.0001);
            //Assert.AreEqual(1974756.3106341641, Input1.Enthalpy.JoulesPerKilogram, 0.0001);
            //Assert.AreEqual(0.93888901877652886, Input1.Prandtl, 0.0001);
            //Assert.AreEqual(74.334422054350568, Input1.Pressure.Bars, 0.0001);
            //Assert.AreEqual(50, Input1.Density.KilogramsPerCubicMeter, 0.0001);
            //Assert.AreEqual(1.4371720270740429, Input1.Entropy.CaloriesPerGramKelvin, 0.0001);
            //Assert.AreEqual(0, Input1.SurfaceTension.NewtonsPerMeter, 0.0001);
            //Assert.AreEqual(189.701240030246, Input1.Temperature.DegreesCelsius, 0.0001);
            //Assert.AreEqual(108.979422334718, Input1.Tsat.DegreesCelsius, 0.0001);
            //Assert.AreEqual(2.0869708899873E-05, Input1.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            //Assert.AreEqual(10000, Input1.Quality, 0.0001);
            //Assert.AreEqual(544.366706172041, Input1.SoundSpeed.MetersPerSecond, 0.0001);

            //Assert.AreEqual(17.03052, Input1.MolarMass.GramsPerMole, 0.001);
            //Assert.AreEqual(0.87621627068822328, Input1.Compressibility, 0.0001);
            //Assert.AreEqual(1912412.68517865, Input1.InternalEnergy.JoulesPerKilogram, 0.0001);

            //Assert.AreEqual(3.4, Input1.MassFlow.KilogramsPerSecond, 0.0001);
            //Assert.AreEqual(0.068, Input1.VolumeFlow.CubicMetersPerSecond, 0.0001);

            ////Assert
            //Assert.IsFalse(Input2.FailState);
            //Assert.AreEqual(0.060912231081315084, Input2.LimitPressureMin.Bars, 0.0001);
            //Assert.AreEqual(10000, Input2.LimitPressureMax.Bars, 0.0001);
            //Assert.AreEqual(113.634, Input2.CriticalPressure.Bars, 0.0001);
            //Assert.AreEqual(-77.655, Input2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            //Assert.AreEqual(132.41, Input2.CriticalTemperature.DegreesCelsius, 0.0001);
            //Assert.AreEqual(451.85, Input2.LimitTemperatureMax.DegreesCelsius, 0.0001);

            //Assert.AreEqual(0.0301054359321657, Input2.Conductivity.WattsPerMeterKelvin, 0.0001);
            //Assert.AreEqual(2718.6666345713, Input2.Cp.JoulesPerKilogramKelvin, 0.0001);
            //Assert.AreEqual(1941.4305998537359, Input2.Cv.JoulesPerKilogramKelvin, 0.0001);
            //Assert.AreEqual(1726765.7255915655, Input2.Enthalpy.JoulesPerKilogram, 0.0001);
            //Assert.AreEqual(1.0245751602823272, Input2.Prandtl, 0.0001);
            //Assert.AreEqual(11.999999999617073, Input2.Pressure.Bars, 0.0001);
            //Assert.AreEqual(7.9795686221167559, Input2.Density.KilogramsPerCubicMeter, 0.0001);
            //Assert.AreEqual(1.4416543274283831, Input2.Entropy.CaloriesPerGramKelvin, 0.0001);
            //Assert.AreEqual(0, Input2.SurfaceTension.NewtonsPerMeter, 0.0001);
            //Assert.AreEqual(63, Input2.Temperature.DegreesCelsius, 0.0001);
            //Assert.AreEqual(30.9545445068116, Input2.Tsat.DegreesCelsius, 0.0001);
            //Assert.AreEqual(1.1345738919708291E-05, Input2.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            //Assert.AreEqual(-1, Input2.Quality, 0.0001);
            //Assert.AreEqual(437.77319868672936, Input2.SoundSpeed.MetersPerSecond, 0.0001);

            //Assert.AreEqual(17.03052, Input2.MolarMass.GramsPerMole, 0.001);
            //Assert.AreEqual(0.91635253866863609, Input2.Compressibility, 0.0001);
            //Assert.AreEqual(1576381.6563733453, Input2.InternalEnergy.JoulesPerKilogram, 0.0001);

            //Assert.AreEqual(1.4, Input2.MassFlow.KilogramsPerSecond, 0.0001);
            //Assert.AreEqual(0.17544808075459839, Input2.VolumeFlow.CubicMetersPerSecond, 0.0001);




        }

        [TestMethod]
        public void AddToUsingMassFlowStartingWithZero()
        {

            //Arrange
            Fluid Input1 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulePerKilogram(2148349.72016398);
            Input1.MassFlow = MassFlow.FromKilogramPerSecond(2);
            Input1.UpdateDH(setDensity, setEnthalpy);

            Fluid Input2 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBar(12);
            Temperature setTemperature = Temperature.FromDegreesCelsius(63);
            Input2.MassFlow = MassFlow.FromKilogramPerSecond(1.4);
            Input2.UpdatePT(setPressure, setTemperature);

            Fluid Mixer = new Fluid(FluidList.Ammonia);


            //Act
            Mixer.SetValuesToNull();
            Mixer.AddTo(Input1);
            Mixer.AddTo(Input2);


            //Assert
            //Assert.IsFalse(Mixer.FailState);
            //Assert.AreEqual(0.060912231081315084, Input1.LimitPressureMin.Bars, 0.0001);
            //Assert.AreEqual(10000, Input1.LimitPressureMax.Bars, 0.0001);
            //Assert.AreEqual(113.634, Input1.CriticalPressure.Bars, 0.0001);
            //Assert.AreEqual(-77.655, Input1.LimitTemperatureMin.DegreesCelsius, 0.0001);
            //Assert.AreEqual(132.41, Input1.CriticalTemperature.DegreesCelsius, 0.0001);
            //Assert.AreEqual(451.85, Input1.LimitTemperatureMax.DegreesCelsius, 0.0001);

            //Assert.AreEqual(0.0722711120542153, Mixer.Conductivity.WattsPerMeterKelvin, 0.0001);
            //Assert.AreEqual(3251.34163624504, Mixer.Cp.JoulesPerKilogramKelvin, 0.0001);
            //Assert.AreEqual(2270.36197650328, Mixer.Cv.JoulesPerKilogramKelvin, 0.0001);
            //Assert.AreEqual(1974756.3106341641, Mixer.Enthalpy.JoulesPerKilogram, 0.0001);
            //Assert.AreEqual(0.93888901877652886, Mixer.Prandtl, 0.0001);
            //Assert.AreEqual(74.334422054350568, Mixer.Pressure.Bars, 0.0001);
            //Assert.AreEqual(50, Mixer.Density.KilogramsPerCubicMeter, 0.0001);
            //Assert.AreEqual(1.4371720270740429, Mixer.Entropy.CaloriesPerGramKelvin, 0.0001);
            //Assert.AreEqual(0, Mixer.SurfaceTension.NewtonsPerMeter, 0.0001);
            //Assert.AreEqual(189.701240030246, Mixer.Temperature.DegreesCelsius, 0.0001);
            //Assert.AreEqual(108.979422334718, Mixer.Tsat.DegreesCelsius, 0.0001);
            //Assert.AreEqual(2.0869708899873E-05, Mixer.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            //Assert.AreEqual(10000, Mixer.Quality, 0.0001);
            //Assert.AreEqual(544.366706172041, Mixer.SoundSpeed.MetersPerSecond, 0.0001);

            //Assert.AreEqual(17.03052, Mixer.MolarMass.GramsPerMole, 0.001);
            //Assert.AreEqual(0.87621627068822328, Mixer.Compressibility, 0.0001);
            //Assert.AreEqual(1912412.68517865, Mixer.InternalEnergy.JoulesPerKilogram, 0.0001);

            //Assert.AreEqual(3.4, Mixer.MassFlow.KilogramsPerSecond, 0.0001);
            //Assert.AreEqual(0.068, Mixer.VolumeFlow.CubicMetersPerSecond, 0.0001);

            ////Assert
            //Assert.AreEqual(0.0609122310813151, Input2.LimitPressureMin.Bars, 0.0001);
            //Assert.AreEqual(10000, Input2.LimitPressureMax.Bars, 0.0001);
            //Assert.AreEqual(113.634, Input2.CriticalPressure.Bars, 0.0001);
            //Assert.AreEqual(-77.655, Input2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            //Assert.AreEqual(132.41, Input2.CriticalTemperature.DegreesCelsius, 0.0001);
            //Assert.AreEqual(451.85, Input2.LimitTemperatureMax.DegreesCelsius, 0.0001);

            //Assert.AreEqual(0.030105435932165674, Input2.Conductivity.WattsPerMeterKelvin, 0.0001);
            //Assert.AreEqual(2718.666634571302, Input2.Cp.JoulesPerKilogramKelvin, 0.0001);
            //Assert.AreEqual(1941.4305998537359, Input2.Cv.JoulesPerKilogramKelvin, 0.0001);
            //Assert.AreEqual(1726765.7255915655, Input2.Enthalpy.JoulesPerKilogram, 0.0001);
            //Assert.AreEqual(1.0245751602823272, Input2.Prandtl, 0.0001);
            //Assert.AreEqual(11.999999999617073, Input2.Pressure.Bars, 0.0001);
            //Assert.AreEqual(7.9795686221167559, Input2.Density.KilogramsPerCubicMeter, 0.0001);
            //Assert.AreEqual(1.4416543274283831, Input2.Entropy.CaloriesPerGramKelvin, 0.0001);
            //Assert.AreEqual(0, Input2.SurfaceTension.NewtonsPerMeter, 0.0001);
            //Assert.AreEqual(63, Input2.Temperature.DegreesCelsius, 0.0001);
            //Assert.AreEqual(30.9545445068116, Input2.Tsat.DegreesCelsius, 0.0001);
            //Assert.AreEqual(1.1345738919708291E-05, Input2.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            //Assert.AreEqual(-1, Input2.Quality, 0.0001);
            //Assert.AreEqual(437.77319868672936, Input2.SoundSpeed.MetersPerSecond, 0.0001);

            //Assert.AreEqual(17.03052, Input2.MolarMass.GramsPerMole, 0.001);
            //Assert.AreEqual(0.91635253866863609, Input2.Compressibility, 0.0001);
            //Assert.AreEqual(1576381.6563733453, Input2.InternalEnergy.JoulesPerKilogram, 0.0001);

            //Assert.AreEqual(1.4, Input2.MassFlow.KilogramsPerSecond, 0.0001);
            //Assert.AreEqual(0.17544808075459839, Input2.VolumeFlow.CubicMetersPerSecond, 0.0001);




        }


        [TestMethod]
        public void AddPowerMassFlow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            R717.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Act
            R717.AddPower(Power.FromKilowatts(100));


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.0740824508482401, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(3196.92765925771, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2275.77928863739, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2198349.72016398, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.92442608105668511, R717.Prandtl, 0.0001);
            Assert.AreEqual(117.968517498023, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(47.8585478000212, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.4554031513314867, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.IsNull(R717.SurfaceTension);
            Assert.AreEqual(293.906571452989, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.14217389356299E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, R717.Quality, 0.0001);
            Assert.AreEqual(555.853464477809, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.89037731875072978, R717.Compressibility, 0.0001);
            Assert.AreEqual(1951855.57664972, R717.InternalEnergy.JoulesPerKilogram, 0.0001);


        }
        [TestMethod]
        public void AddPowerMassFlowMAX()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            R717.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Act
            R717.AddPower(Power.FromKilowatts(99999999));


            Debug.Print((R717.LimitTemperatureMax == R717.Temperature).ToString());


            //Assert
            Assert.IsTrue(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.0859782428759609, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(3097.85594384625, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2441.80870244021, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2688514.39365347, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.97573511970514171, R717.Prandtl, 0.0001);
            Assert.AreEqual(117.968517498023, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(34.477070684444, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(6852.38893489242, R717.Entropy.JoulesPerKilogramKelvin, 0.0001);
            Assert.IsNull(R717.SurfaceTension);
            Assert.AreEqual(451.85, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.70806624405054E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, R717.Quality, 0.0001);
            Assert.AreEqual(648.988154438777, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.96669979536621986, R717.Compressibility, 0.0001);
            Assert.AreEqual(2346349.25273296, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.058009568687121579, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);


        }

        [TestMethod]
        public void RemovePowerMassFlow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            R717.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Act
            R717.RemovePower(Power.FromKilowatts(100));


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.07053387608846, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(3322.84286345408, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2269.0067689874, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2098349.72016398, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.95791851419327612, R717.Prandtl, 0.0001);
            Assert.AreEqual(117.968517492702, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(52.3793974219741, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(5908.06618790933, R717.Entropy.JoulesPerKilogramKelvin, 0.0001);
            Assert.IsNull(R717.SurfaceTension);
            Assert.AreEqual(263.17307543127, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.03337047701124E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, R717.Quality, 0.0001);
            Assert.AreEqual(532.403853045307, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.86014765870726462, R717.Compressibility, 0.0001);
            Assert.AreEqual(1873130.41009377, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.038182951664903347, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);


        }



        [TestMethod]
        public void RemovePowerMassFlowMin()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            R717.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Act
            R717.RemovePower(Power.FromKilowatts(99999999));


            //Assert
            Assert.IsTrue(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.836238283804608, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4291.45072618964, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2977.0257935309, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(11216.0474289283, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(3.0501801705032769, R717.Prandtl, 0.0001);
            Assert.AreEqual(117.968517492702, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(737.991789248866, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(-0.00588517138163891, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.IsNull(R717.SurfaceTension);
            Assert.AreEqual(R717.LimitTemperatureMin, R717.Temperature);
            Assert.AreEqual(132.41, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(0.000594362511378813, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, R717.Quality, 0.0001);
            Assert.AreEqual(2045.83348665104, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.16748380087109294, R717.Compressibility, 0.0001);
            Assert.AreEqual(-4769.02438505104, R717.InternalEnergy.JoulesPerKilogram, 0.0001);


        }


        [TestMethod]
        public void TsatBelowCrit()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure pressure = Pressure.FromBars(20);
            R717.UpdatePX(pressure, 0.5);


            //Act
            Temperature Tsat = R717.Tsat;


            //Assert
            Assert.AreEqual(Tsat.DegreesCelsius, 49.371451247030961, 0.001);


        }

        [TestMethod]
        public void TsatOnCrit()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
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
            Fluid R717 = new Fluid(FluidList.Ammonia);
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
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure pressure = Pressure.FromBars(20);

            //Act
            Temperature temperature = R717.GetSatTemperature(pressure);


            //Assert
            Assert.AreEqual(temperature.DegreesCelsius, 49.371451247030279, 0.001);


        }

        [TestMethod]
        public void GetSatTemperatureAboveCrit()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
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
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Temperature temperature = Temperature.FromDegreesCelsius(30);

            //Act
            Pressure pressure = R717.GetSatPressure(temperature);


            //Assert
            Assert.AreEqual(pressure.Bars, 11.665360629887157, 0.001);


        }

        [TestMethod]
        public void GetSatPressureAboveCrit()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Temperature temperature = R717.LimitTemperatureMax;

            //Act
            Pressure pressure = R717.GetSatPressure(temperature);


            //Assert
            Assert.AreEqual(pressure.Bars, R717.CriticalPressure.Bars, 0.001);


        }

        [TestMethod]
        public void MinMaxFractionLimit()
        {

            Fluid mixref = new Fluid(FluidList.MixAmmoniaAQ);


            //Assert
            Assert.AreEqual(0, mixref.FractionMin);
            Assert.AreEqual(0.3, mixref.FractionMax);

        }

        [TestMethod]
        public void UpdateTSFunctionality()
        {
            Fluid water = new Fluid(FluidList.Water);
            Temperature temperature = Temperature.FromKelvins(286.15);
            SpecificEntropy entropy = SpecificEntropy.FromJoulesPerKilogramKelvin(195.27);
            water.UpdateTS(temperature, entropy);

            Assert.AreEqual(0.082063, water.Pressure.Megapascals, 0.001);


            Fluid ammonia = new Fluid(FluidList.Ammonia);
            temperature = Temperature.FromKelvins(286.15);
            entropy = SpecificEntropy.FromJoulesPerKilogramKelvin(1697.7);
            ammonia.UpdateTS(temperature, entropy);

            Assert.AreEqual(0.93555, ammonia.Pressure.Megapascals, 0.001);

            Fluid co2 = new Fluid(FluidList.CO2);
            temperature = Temperature.FromKelvins(286.15);
            entropy = SpecificEntropy.FromJoulesPerKilogramKelvin(2220.2);
            co2.UpdateTS(temperature, entropy);

            Assert.AreEqual(1.1240, co2.Pressure.Megapascals, 0.001);

        }


        [TestMethod]// (Skip = "race condition demonstration")]
        public void ParallelAccess2()
        {



            List<Task> tasks = new();
            for (int i = 1; i < 400; ++i)
            {
                tasks.Add(Task.Run(dividedUnit));
            }

            Task.WaitAll(tasks.ToArray());




            //Local function
            void dividedUnit()
            {
                Fluid water = new Fluid(FluidList.Water);
                Temperature temperature = Temperature.FromKelvins(286.15);
                SpecificEntropy entropy = SpecificEntropy.FromJoulePerKilogramKelvin(195.27);
                water.UpdateTS(temperature, entropy);
            }
        }


        [TestMethod]
        public void GasDensity()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            R717.UpdatePX(Pressure.FromBar(10), 0.5);

            //Act
            var Density = R717.Density;
            var Gasdensity = R717.GasDensity;
            var Liqdensity = R717.LiquidDensity;


            //Assert
            Assert.IsTrue(Gasdensity < Density);
            Assert.IsTrue(Density < Liqdensity);


        }
        [TestMethod]
        public void GasDensityOutSide()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            R717.UpdatePX(Pressure.FromBar(10), 1);
            R717.UpdatePT(R717.Pressure, R717.Temperature + Temperature.FromKelvins(10));

            //Act
            var Density = R717.Density;
            var Gasdensity = R717.GasDensity;
            var Liqdensity = R717.LiquidDensity;


            //Assert
            Assert.IsTrue(Gasdensity == Density);
            Assert.IsTrue(Density == Liqdensity);


        }

        [TestMethod]
        public void GasDensityOutSide2()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            R717.UpdatePX(Pressure.FromBar(10), 0);
            R717.UpdatePT(R717.Pressure, R717.Temperature - Temperature.FromKelvins(10));

            //Act
            var Density = R717.Density;
            var Gasdensity = R717.GasDensity;
            var Liqdensity = R717.LiquidDensity;


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
            Fluid R717 = new Fluid(FluidList.Ammonia);

            R717.UpdatePT(R717.CriticalPressure + Pressure.FromBar(10), R717.CriticalTemperature + Temperature.FromKelvins(25));

            //Act
            var Density = R717.Density;
            var Gasdensity = R717.GasDensity;
            var Liqdensity = R717.LiquidDensity;


            //Assert
            Assert.IsTrue(Gasdensity == Density);
            Assert.IsTrue(Density == Liqdensity);
            Assert.IsTrue(Density != Density.Zero);


        }

    }
}