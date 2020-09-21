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
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
            
            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768620415, R717.Compressibility, 0.0001);
            Assert.AreEqual(978342.4226, R717.InternalEnergy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13015557603938352, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);

            //Assert JSON
            Assert.AreEqual(R717JSON.P_Min.Bars, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(R717JSON.P_Max.Bars, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(R717JSON.P_Crit.Bars, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(R717JSON.T_Min.DegreesCelsius, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.T_Crit.DegreesCelsius, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.T_Max.DegreesCelsius, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.H_Crit.JoulesPerKilogram, R717.H_Crit.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(R717JSON.Viscosity.NewtonSecondsPerMeterSquared, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.768620415, R717.Compressibility, 0.0001);
            Assert.AreEqual(978342.4226, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            //Assert JSON
            Assert.AreEqual(R717JSON.P_Min.Bars, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(R717JSON.P_Max.Bars, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(R717JSON.P_Crit.Bars, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(R717JSON.T_Min.DegreesCelsius, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.T_Crit.DegreesCelsius, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.T_Max.DegreesCelsius, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717JSON.H_Crit.JoulesPerKilogram, R717.H_Crit.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(R717JSON.Viscosity.NewtonSecondsPerMeterSquared, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(DynamicViscosity.Zero, R717.Viscosity);
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

            R717.ZeroValues();


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(DynamicViscosity.Zero, R717.Viscosity);
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
            Assert.AreEqual(0.060912231081315084, CO2.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, CO2.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, CO2.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, CO2.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, CO2.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, CO2.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(DynamicViscosity.Zero, CO2.Viscosity);
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
            Assert.AreEqual(0.060912231081315084, CO2.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, CO2.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, CO2.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, CO2.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, CO2.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, CO2.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

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
            Assert.AreEqual(DynamicViscosity.Zero, CO2.Viscosity);
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
    }
}