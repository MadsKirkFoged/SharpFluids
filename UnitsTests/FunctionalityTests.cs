using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Fluid R717JSON = new Fluid();
            R717JSON = R717JSON.LoadFromJSON(json);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
            
            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768620415, R717.Compressibility, 0.0001);
            Assert.AreEqual(978342.4226, R717.InternalEnergy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13015557603938352, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert JSON
            Assert.AreEqual(R717JSON.LimitPressureMin.Bars, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(R717JSON.LimitPressureMax.Bars, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(R717JSON.CriticalPressure.Bars, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(R717JSON.LimitTemperatureMin.DegreesCelsius, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.CriticalTemperature.DegreesCelsius, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.LimitTemperatureMax.DegreesCelsius, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.CriticalEnthalpy.JoulesPerKilogram, R717.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

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

            Assert.AreEqual(R717JSON.MolarMass.GramsPerMole, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(R717JSON.Compressibility, R717.Compressibility, 0.0001);
            Assert.AreEqual(R717JSON.InternalEnergy.JoulesPerKilogram, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(R717JSON.MassFlow.KilogramsPerSecond, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(R717JSON.VolumeFlow.CubicMetersPerSecond, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
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
            Fluid R717JSON = new Fluid();
            R717JSON = R717JSON.LoadFromJSON(json);


            Assert.AreEqual(43, R717.Mass.Kilograms, 0.0001);
            Assert.AreEqual(2.7983448848467454, R717.Volume.CubicMeters, 0.0001);

            //Assert JSON          
            Assert.AreEqual(R717JSON.MassFlow.KilogramsPerSecond, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(R717JSON.VolumeFlow.CubicMetersPerSecond, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
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

            //Save as JSON
            string json = R717.SaveAsJSON();


            //Start new fluid and load as json
            Fluid R717JSON = new Fluid();
            R717JSON.Copy(R717);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768620415, R717.Compressibility, 0.0001);
            Assert.AreEqual(978342.4226, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            //Assert JSON
            Assert.AreEqual(R717JSON.LimitPressureMin.Bars, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(R717JSON.LimitPressureMax.Bars, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(R717JSON.CriticalPressure.Bars, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(R717JSON.LimitTemperatureMin.DegreesCelsius, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.CriticalTemperature.DegreesCelsius, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.LimitTemperatureMax.DegreesCelsius, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.CriticalEnthalpy.JoulesPerKilogram, R717.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(426.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(426.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(426.85, CO2.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(426.85, CO2.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(426.85, Input1.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, Input1.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, Input1.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, Input1.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, Input1.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1323650.6959882693, Input1.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, Input1.Prandtl, 0.0001);
            Assert.AreEqual(10.823529411598573, Input1.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, Input1.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.1315919252210014, Input1.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, Input1.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(40.585348273697491, Input1.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(27.484631922677977, Input1.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, Input1.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, Input1.Quality, 0.0001);
            Assert.AreEqual(372.73404999072648, Input1.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, Input1.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768620415, Input1.Compressibility, 0.0001);
            Assert.AreEqual(978342.4226, Input1.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(3.4, Input1.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.22126447926695195, Input1.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert
            Assert.AreEqual(0.060912231081315084, Input2.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, Input2.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, Input2.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, Input2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, Input2.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, Input2.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, Input2.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.03010548171318805, Input2.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2715.3187011860464, Input2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1936.5617140726658, Input2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1723979.9608179531, Input2.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.023311246357786, Input2.Prandtl, 0.0001);
            Assert.AreEqual(11.999999999628866, Input2.Pressure.Bars, 0.0001);
            Assert.AreEqual(7.9798175145632211, Input2.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.4386765291357957, Input2.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Input2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(63, Input2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.935267029115494, Input2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.1345731902729294E-05, Input2.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Input2.Quality, 0.0001);
            Assert.AreEqual(438.04679913975394, Input2.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03026, Input2.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.91630873384435563, Input2.Compressibility, 0.0001);
            Assert.AreEqual(1573600.5821153785, Input2.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(1.4, Input2.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.17544260848634577, Input2.VolumeFlow.CubicMetersPerSecond, 0.0001);




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
            Assert.AreEqual(426.85, Mixer.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, Mixer.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, Mixer.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, Mixer.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, Mixer.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1323650.6959882693, Mixer.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, Mixer.Prandtl, 0.0001);
            Assert.AreEqual(10.823529411598573, Mixer.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, Mixer.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.1315919252210014, Mixer.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, Mixer.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(40.585348273697491, Mixer.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(27.484631922677977, Mixer.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, Mixer.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, Mixer.Quality, 0.0001);
            Assert.AreEqual(372.73404999072648, Mixer.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, Mixer.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768620415, Mixer.Compressibility, 0.0001);
            Assert.AreEqual(978342.4226, Mixer.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(3.4, Mixer.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.22126447926695195, Mixer.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert
            Assert.AreEqual(0.060912231081315084, Input2.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, Input2.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, Input2.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, Input2.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, Input2.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, Input2.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, Input2.CriticalEnthalpy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.03010548171318805, Input2.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2715.3187011860464, Input2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1936.5617140726658, Input2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1723979.9608179531, Input2.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.023311246357786, Input2.Prandtl, 0.0001);
            Assert.AreEqual(11.999999999628866, Input2.Pressure.Bars, 0.0001);
            Assert.AreEqual(7.9798175145632211, Input2.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.4386765291357957, Input2.Entropy.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Input2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(63, Input2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.935267029115494, Input2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.1345731902729294E-05, Input2.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Input2.Quality, 0.0001);
            Assert.AreEqual(438.04679913975394, Input2.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03026, Input2.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.91630873384435563, Input2.Compressibility, 0.0001);
            Assert.AreEqual(1573600.5821153785, Input2.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(1.4, Input2.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.17544260848634577, Input2.VolumeFlow.CubicMetersPerSecond, 0.0001);




        }

    }
}