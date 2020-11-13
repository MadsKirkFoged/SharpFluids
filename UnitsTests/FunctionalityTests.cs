using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SharpFluids;
using UnitsNet;

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
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);

            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;

            //Save as JSON
            string json = R717.SaveAsJSON();


            //Start new fluid and load as json
            Fluid R717JSON = R717.LoadFromJSON(json);




            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.027574706528001671, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4955.8074280519995, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2792.0746261319227, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106073985, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7251500393076753, R717.Prandtl, 0.0001);
            Assert.AreEqual(9.9654295346437216, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.9176026758197906, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020532110006759582, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.800454983004158, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.800454983344366, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.5989415935353317E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.49818089549559319, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);
            
            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768972656755548, R717.Compressibility, 0.0001);
            Assert.AreEqual(978567.39952867059, R717.InternalEnergy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13015557603938352, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert JSON
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
            Assert.AreEqual(R717JSON.Entropy.KilocaloriesPerKelvin, R717.Entropy.KilocaloriesPerKelvin);
            Assert.AreEqual(R717JSON.SurfaceTension.NewtonsPerMeter, R717.SurfaceTension.NewtonsPerMeter);
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
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            Mass setMass = Mass.FromKilograms(43);

            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.Mass = setMass;

            //Save as JSON
            string json = R717.SaveAsJSON();


            //Start new fluid and load as json
            Fluid R717JSON = R717.LoadFromJSON(json);


            Assert.AreEqual(43, R717.Mass.Kilograms, 0.0001);
            Assert.AreEqual(2.7983448848467454, R717.Volume.CubicMeters, 0.0001);

            //Assert JSON          
            Assert.AreEqual(R717JSON.MassFlow.KilogramsPerSecond, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(R717JSON.VolumeFlow.CubicMetersPerSecond, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void LoadValuesThenJSON()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
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
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.027574706528001671, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4955.8074280519995, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2792.0746261319227, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106073985, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7251500393076753, R717.Prandtl, 0.0001);
            Assert.AreEqual(9.9654295346437216, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.9176026758197906, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020532110006759582, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.800454983004158, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.800454983344366, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.5989415935353317E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.49818089549559319, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768972656755548, R717.Compressibility, 0.0001);
            Assert.AreEqual(978567.39952867059, R717.InternalEnergy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13015557603938352, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert JSON
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
            Assert.AreEqual(R717JSON.Entropy.KilocaloriesPerKelvin, R717.Entropy.KilocaloriesPerKelvin);
            Assert.AreEqual(R717JSON.SurfaceTension.NewtonsPerMeter, R717.SurfaceTension.NewtonsPerMeter);
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
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
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
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);

            //Act
            R717.UpdateDH(setDensity, setEnthalpy);


            //Start new fluid and load as json
            Fluid R717JSON = new Fluid();
            R717JSON.Copy(R717);
            


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.027574706528001671, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4955.8074280519995, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2792.0746261319227, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106073985, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7251500393076753, R717.Prandtl, 0.0001);
            Assert.AreEqual(9.9654295346437216, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.9176026758197906, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020532110006759582, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.800454983004158, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.800454983344366, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.5989415935353317E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.49818089549559319, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768972656755548, R717.Compressibility, 0.0001);
            Assert.AreEqual(978567.39952867059, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            //Assert JSON
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
            Assert.AreEqual(R717JSON.Entropy.KilocaloriesPerKelvin, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(R717JSON.SurfaceTension.NewtonsPerMeter, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(ThermalConductivity.Zero, R717.Conductivity);
            Assert.AreEqual(SpecificEntropy.Zero, R717.Cp);
            Assert.AreEqual(SpecificEntropy.Zero, R717.Cv);
            Assert.AreEqual(SpecificEnergy.Zero, R717.Enthalpy);
            Assert.AreEqual(0, R717.Prandtl);
            Assert.AreEqual(Pressure.Zero, R717.Pressure);
            Assert.AreEqual(Density.Zero, R717.Density);
            Assert.AreEqual(Entropy.Zero, R717.Entropy);
            Assert.AreEqual(ForcePerLength.Zero, R717.SurfaceTension);
            Assert.AreEqual(Temperature.Zero, R717.Temperature);
            Assert.AreEqual(Temperature.Zero, R717.Tsat);
            Assert.AreEqual(DynamicViscosity.Zero, R717.DynamicViscosity);
            Assert.AreEqual(0, R717.Quality);
            Assert.AreEqual(Speed.Zero, R717.SoundSpeed);

            Assert.AreEqual(MolarMass.Zero, R717.MolarMass);
            Assert.AreEqual(0, R717.Compressibility);
            Assert.AreEqual(SpecificEnergy.Zero, R717.InternalEnergy);

           
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

            R717.SetValuesToZero();


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(ThermalConductivity.Zero, R717.Conductivity);
            Assert.AreEqual(SpecificEntropy.Zero, R717.Cp);
            Assert.AreEqual(SpecificEntropy.Zero, R717.Cv);
            Assert.AreEqual(SpecificEnergy.Zero, R717.Enthalpy);
            Assert.AreEqual(0, R717.Prandtl);
            Assert.AreEqual(Pressure.Zero, R717.Pressure);
            Assert.AreEqual(Density.Zero, R717.Density);
            Assert.AreEqual(Entropy.Zero, R717.Entropy);
            Assert.AreEqual(ForcePerLength.Zero, R717.SurfaceTension);
            Assert.AreEqual(Temperature.Zero, R717.Temperature);
            Assert.AreEqual(Temperature.Zero, R717.Tsat);
            Assert.AreEqual(DynamicViscosity.Zero, R717.DynamicViscosity);
            Assert.AreEqual(0, R717.Quality);
            Assert.AreEqual(Speed.Zero, R717.SoundSpeed);

            Assert.AreEqual(MolarMass.Zero, R717.MolarMass);
            Assert.AreEqual(0, R717.Compressibility);
            Assert.AreEqual(SpecificEnergy.Zero, R717.InternalEnergy);


        }

        [TestMethod]
        public void CopyTypeWithEmptyStartingFluid()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Start new fluid
            Fluid CO2 = new Fluid();
            CO2.CopyType(R717);



            //Assert
            Assert.AreEqual(0.060912231081315084, CO2.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, CO2.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, CO2.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, CO2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, CO2.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, CO2.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(ThermalConductivity.Zero, CO2.Conductivity);
            Assert.AreEqual(SpecificEntropy.Zero, CO2.Cp);
            Assert.AreEqual(SpecificEntropy.Zero, CO2.Cv);
            Assert.AreEqual(SpecificEnergy.Zero, CO2.Enthalpy);
            Assert.AreEqual(0, CO2.Prandtl);
            Assert.AreEqual(Pressure.Zero, CO2.Pressure);
            Assert.AreEqual(Density.Zero, CO2.Density);
            Assert.AreEqual(Entropy.Zero, CO2.Entropy);
            Assert.AreEqual(ForcePerLength.Zero, CO2.SurfaceTension);
            Assert.AreEqual(Temperature.Zero, CO2.Temperature);
            Assert.AreEqual(Temperature.Zero, CO2.Tsat);
            Assert.AreEqual(DynamicViscosity.Zero, CO2.DynamicViscosity);
            Assert.AreEqual(0, CO2.Quality);
            Assert.AreEqual(Speed.Zero, CO2.SoundSpeed);

            Assert.AreEqual(MolarMass.Zero, CO2.MolarMass);
            Assert.AreEqual(0, CO2.Compressibility);
            Assert.AreEqual(SpecificEnergy.Zero, CO2.InternalEnergy);
        }

        [TestMethod]
        public void CopyTypeWithNonEmptyStartingFluid()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Start new fluid
            Fluid CO2 = new Fluid(FluidList.CO2);
            CO2.UpdatePT(Pressure.FromBars(30), Temperature.FromDegreesCelsius(60));
            CO2.CopyType(R717);



            //Assert
            Assert.AreEqual(0.060912231081315084, CO2.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, CO2.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, CO2.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, CO2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, CO2.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, CO2.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(ThermalConductivity.Zero, CO2.Conductivity);
            Assert.AreEqual(SpecificEntropy.Zero, CO2.Cp);
            Assert.AreEqual(SpecificEntropy.Zero, CO2.Cv);
            Assert.AreEqual(SpecificEnergy.Zero, CO2.Enthalpy);
            Assert.AreEqual(0, CO2.Prandtl);
            Assert.AreEqual(Pressure.Zero, CO2.Pressure);
            Assert.AreEqual(Density.Zero, CO2.Density);
            Assert.AreEqual(Entropy.Zero, CO2.Entropy);
            Assert.AreEqual(ForcePerLength.Zero, CO2.SurfaceTension);
            Assert.AreEqual(Temperature.Zero, CO2.Temperature);
            Assert.AreEqual(Temperature.Zero, CO2.Tsat);
            Assert.AreEqual(DynamicViscosity.Zero, CO2.DynamicViscosity);
            Assert.AreEqual(0, CO2.Quality);
            Assert.AreEqual(Speed.Zero, CO2.SoundSpeed);

            Assert.AreEqual(MolarMass.Zero, CO2.MolarMass);
            Assert.AreEqual(0, CO2.Compressibility);
            Assert.AreEqual(SpecificEnergy.Zero, CO2.InternalEnergy);
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
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
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
            Assert.AreEqual(0.060912231081315084, Input1.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, Input1.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, Input1.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, Input1.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, Input1.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, Input1.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.027574706528001671, Input1.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4955.8074280519995, Input1.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2792.0746261319227, Input1.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1324797.775600879, Input1.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7251500393076753, Input1.Prandtl, 0.0001);
            Assert.AreEqual(10.803193843750396, Input1.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, Input1.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.133388650011564, Input1.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020532110006759582, Input1.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(40.529679401767169, Input1.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(27.440898990601966, Input1.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.5989415935353317E-06, Input1.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.49818089549559319, Input1.Quality, 0.0001);
            Assert.AreEqual(0, Input1.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, Input1.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768972656755548, Input1.Compressibility, 0.0001);
            Assert.AreEqual(978567.39952867059, Input1.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(3.4, Input1.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.22126447926695195, Input1.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert
            Assert.AreEqual(0.060912231081315084, Input2.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, Input2.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, Input2.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, Input2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, Input2.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, Input2.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.030105435932165674, Input2.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2718.666634571302, Input2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1941.4305998537359, Input2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1726765.7255915655, Input2.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.0245751602823272, Input2.Prandtl, 0.0001);
            Assert.AreEqual(11.999999999617073, Input2.Pressure.Bars, 0.0001);
            Assert.AreEqual(7.9795686221167559, Input2.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.4416543274283831, Input2.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Input2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(63, Input2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.9545445068116, Input2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.1345738919708291E-05, Input2.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Input2.Quality, 0.0001);
            Assert.AreEqual(437.77319868672936, Input2.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, Input2.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.91635253866863609, Input2.Compressibility, 0.0001);
            Assert.AreEqual(1576381.6563733453, Input2.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(1.4, Input2.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.17544808075459839, Input2.VolumeFlow.CubicMetersPerSecond, 0.0001);




        }

        [TestMethod]
        public void AddToUsingMassFlowStartingWithZero()
        {

            //Arrange
            Fluid Input1 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            Input1.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            Input1.UpdateDH(setDensity, setEnthalpy);

            Fluid Input2 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(12);
            Temperature setTemperature = Temperature.FromDegreesCelsius(63);
            Input2.MassFlow = MassFlow.FromKilogramsPerSecond(1.4);
            Input2.UpdatePT(setPressure, setTemperature);

            Fluid Mixer = new Fluid(FluidList.Ammonia);


            //Act
            Mixer.SetValuesToZero();
            Mixer.AddTo(Input1);
            Mixer.AddTo(Input2);


            //Assert
            Assert.AreEqual(0.060912231081315084, Mixer.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, Mixer.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, Mixer.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, Mixer.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, Mixer.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, Mixer.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.027574706528001671, Mixer.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4955.8074280519995, Mixer.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2792.0746261319227, Mixer.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1324797.775600879, Mixer.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7251500393076753, Mixer.Prandtl, 0.0001);
            Assert.AreEqual(10.803193843750396, Mixer.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, Mixer.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.133388650011564, Mixer.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020532110006759582, Mixer.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(40.529679401767169, Mixer.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(27.440898990601966, Mixer.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.5989415935353317E-06, Mixer.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.49818089549559319, Mixer.Quality, 0.0001);
            Assert.AreEqual(0, Mixer.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, Mixer.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768972656755548, Mixer.Compressibility, 0.0001);
            Assert.AreEqual(978567.39952867059, Mixer.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(3.4, Mixer.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.22126447926695195, Mixer.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert
            Assert.AreEqual(0.060912231081315084, Input2.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, Input2.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, Input2.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, Input2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, Input2.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, Input2.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.030105435932165674, Input2.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2718.666634571302, Input2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1941.4305998537359, Input2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1726765.7255915655, Input2.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.0245751602823272, Input2.Prandtl, 0.0001);
            Assert.AreEqual(11.999999999617073, Input2.Pressure.Bars, 0.0001);
            Assert.AreEqual(7.9795686221167559, Input2.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.4416543274283831, Input2.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Input2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(63, Input2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.9545445068116, Input2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.1345738919708291E-05, Input2.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Input2.Quality, 0.0001);
            Assert.AreEqual(437.77319868672936, Input2.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, Input2.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.91635253866863609, Input2.Compressibility, 0.0001);
            Assert.AreEqual(1576381.6563733453, Input2.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(1.4, Input2.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.17544808075459839, Input2.VolumeFlow.CubicMetersPerSecond, 0.0001);




        }


        [TestMethod]
        public void AddPowerMassFlow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            R717.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Act
            R717.AddPower(Power.FromKilowatts(100));


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.027340601791772289, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4588.5742406035952, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2683.6272590893277, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1093420.2106074, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.6164793487091984, R717.Prandtl, 0.0001);
            Assert.AreEqual(9.9654295346437216, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(14.17761237163829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.957710977744379, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020532110006681651, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.800454983344366, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.800454983344366, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.63164500785499E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.54103874375500483, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.78684896388758041, R717.Compressibility, 0.0001);
            Assert.AreEqual(1023130.3107750479, R717.InternalEnergy.JoulesPerKilogram, 0.0001);


        }
        [TestMethod]
        public void AddPowerMassFlowMAX()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            R717.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Act
            R717.AddPower(Power.FromKilowatts(99999999));


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.0795170822501053, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2887.525048795872, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2386.2756482293962, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2750807.3108348968, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.95670787106984034, R717.Prandtl, 0.0001);
            Assert.AreEqual(9.9654295346437216, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(2.8241346352757937, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.9429350356857431, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(R717.LimitTemperatureMax, R717.Temperature);
            Assert.AreEqual(24.800454983344366, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.634595966705381E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, R717.Quality, 0.0001);
            Assert.AreEqual(652.44754867833785, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.99693418850319648, R717.Compressibility, 0.0001);
            Assert.AreEqual(2397940.6517823422, R717.InternalEnergy.JoulesPerKilogram, 0.0001);


        }

        [TestMethod]
        public void RemovePowerMassFlow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            R717.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Act
            R717.RemovePower(Power.FromKilowatts(100));


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.027855773195711798, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5451.1392911477233, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2923.79614861175, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(993420.21060739993, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.8711461600292012, R717.Prandtl, 0.0001);
            Assert.AreEqual(9.96542953464731, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(16.772377991312993, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.87749437389623908, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020532110006678969, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.800454983356076, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.800454983356019, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.5617118304871416E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.45532304723311673, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.74783382782909569, R717.Compressibility, 0.0001);
            Assert.AreEqual(934004.4882822244, R717.InternalEnergy.JoulesPerKilogram, 0.0001);


        }



        [TestMethod]
        public void RemovePowerMassFlowMin()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            R717.MassFlow = MassFlow.FromKilogramsPerSecond(2);
            R717.UpdateDH(setDensity, setEnthalpy);


            //Act
            R717.RemovePower(Power.FromKilowatts(99999999));


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);

            Assert.AreEqual(0.82336574497686044, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4304.073099102181, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2968.329150078016, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(966.424186195203, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2.970732325948974, R717.Prandtl, 0.0001);
            Assert.AreEqual(9.96542953464731, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(734.20326325395592, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(-0.00047828927263162855, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(R717.LimitTemperatureMin, R717.Temperature);
            Assert.AreEqual(24.800454983344366, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(0.00056829872038930936, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, R717.Quality, 0.0001);
            Assert.AreEqual(2014.2840877222347, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.014221255159098667, R717.Compressibility, 0.0001);
            Assert.AreEqual(-390.88788709649447, R717.InternalEnergy.JoulesPerKilogram, 0.0001);


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
            Assert.AreEqual(Tsat.DegreesCelsius, 49.371451247030961);


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

    }
}