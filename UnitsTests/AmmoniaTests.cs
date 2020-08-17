using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using UnitsNet;

namespace UnitsTests
{
    [TestClass]
    public class AmmoniaTests
    {

        [TestMethod]
        public void UpdateDH()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);



            //Act
            R717.UpdateDH(setDensity, setEnthalpy);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdateDP()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            Pressure setPressure = Pressure.FromBars(10);



            //Act
            R717.UpdateDP(setDensity, setPressure);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdateDS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            Entropy setEntropy = Entropy.FromJoulesPerKelvin(3835.1912271790293);



            //Act
            R717.UpdateDS(setDensity, setEntropy);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdateDT()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.36622602626586);
            Temperature setTemperature = Temperature.FromDegreesCelsius(24.8950920654342);



            //Act
            R717.UpdateDT(setDensity, setTemperature);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdateHS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);
            Entropy setEntropy = Entropy.FromJoulesPerKelvin(3835.1912271790293);



            //Act
            R717.UpdateHS(setEnthalpy, setEntropy);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdatePH()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1043420.2106074861);




            //Act
            R717.UpdatePH(setPressure, setEnthalpy);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdatePS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            Entropy setEntropy = Entropy.FromJoulesPerKelvin(3835.1912271790293);




            //Act
            R717.UpdatePS(setPressure, setEntropy);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdatePT()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            Temperature setTemperature = Temperature.FromDegreesCelsius(100);




            //Act
            R717.UpdatePT(setPressure, setTemperature);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.034349319392862628, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2466.1130739513806, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1844.312171594087, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1826601.9754952656, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.92296946216906417, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(5.7508515930960495, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.5279290194907087, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(100, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.2855603897799563E-05, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, R717.X, 0.0001);
            Assert.AreEqual(470.71491332048453, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdatePX()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            double X = 0.5;



            //Act
            R717.UpdatePX(setPressure, X);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }



        [TestMethod]
        public void UpdateXT()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Temperature setTemperature = Temperature.FromDegreesCelsius(24.8950920654342);
            double X = 0.5;



            //Act
            R717.UpdateXT(X, setTemperature);



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.P_Min.Bars, 0.0001);
            Assert.AreEqual(10000, R717.P_Max.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(426.85, R717.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(1316216.3157251189, R717.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.027583261794832403, R717.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(5074.0398715329484, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2847.6400619419965, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1043420.2106074861, R717.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7665134008664596, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.36622602626586, R717.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91663270248064743, R717.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.020510431023511407, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.8950920654342, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.895092065439826, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6030387686839971E-06, R717.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.X, 0.0001);
            Assert.AreEqual(372.73404999072648, R717.SoundSpeed.MetersPerSecond, 0.0001);
        }
    
    
    
    


    }
}
