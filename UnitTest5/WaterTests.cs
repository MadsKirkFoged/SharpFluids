﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
//using UnitsNet;
using EngineeringUnits;
using EngineeringUnits.Units;

namespace UnitsTests
{
    [TestClass]
    public class WaterLookups
    {
        [TestMethod]
        public void UpdatePX()
        {

            //Arrange
            Fluid Water = new Fluid(FluidList.Water);
            Pressure setPressure = Pressure.FromBars(3);
            double X = 1;



            //Act
            Water.UpdatePX(setPressure, X);



            //Assert
            Assert.IsFalse(Water.FailState);
            Assert.AreEqual(0.028215527552977144, Water.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2263.0469328306258, Water.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1671.0843504212746, Water.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2724882.6302959691, Water.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.0742998825687049, Water.Prandtl, 0.0001);
            Assert.AreEqual(3.0000000000000004, Water.Pressure.Bars, 0.0001);
            Assert.AreEqual(220.64, Water.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(10000, Water.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(0.0061165480089686846, Water.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(1.650819966799242, Water.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.6710365869337238, Water.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0, Water.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(133.522420460943, Water.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(133.52242046094256, Water.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(373.946, Water.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Water.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(0.010000000000047749, Water.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.3394303713738338E-05, Water.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(X, Water.Quality, 0.0001);
            Assert.AreEqual(0, Water.SoundSpeed.MetersPerSecond, 0.0001);
            Assert.AreEqual(18.015268, Water.MolarMass.GramsPerMole, 0.0001);
            Assert.AreEqual(0.968251392124, Water.Compressibility, 0.0001);
            Assert.AreEqual(2543154.75807, Water.InternalEnergy.JoulesPerKilogram, 0.001);
        }

        [TestMethod]
        public void UpdatePT()
        {

            //Arrange
            Fluid Water = new Fluid(FluidList.Water);
            Pressure setPressure = Pressure.FromBars(3);
            Temperature setTemperature = Temperature.FromDegreesCelsius(237);



            //Act
            Water.UpdatePT(setPressure, setTemperature);



            //Assert
            Assert.IsFalse(Water.FailState);
            Assert.AreEqual(0.03752160884674547, Water.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2034.1444264493639, Water.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1539.4771263572343, Water.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2941434.7212755163, Water.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.95772763107228476, Water.Prandtl, 0.0001);
            Assert.AreEqual(3, Water.Pressure.Bars, 0.0001);
            Assert.AreEqual(220.64, Water.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(10000, Water.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(0.0061165480089686846, Water.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(1.2890299515474706, Water.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.7846226274892405, Water.Entropy.CaloriesPerGramKelvin, 0.0001);
            //Assert.AreEqual(0.052144997752313106, Water.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(237, Water.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(133.522420460943, Water.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(373.946, Water.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Water.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(0.010000000000047749, Water.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.7666140657249425E-05, Water.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Water.Quality, 0.0001);
            Assert.AreEqual(551.279827045, Water.SoundSpeed.MetersPerSecond, 0.0001);
            Assert.AreEqual(18.015268, Water.MolarMass.GramsPerMole, 0.0001);
            Assert.AreEqual(0.988488597881, Water.Compressibility, 0.0001);
            Assert.AreEqual(2708701.57212, Water.InternalEnergy.JoulesPerKilogram, 0.001);

        }

        [TestMethod]
        public void UpdatePTFailedTestIceWater()
        {

            //It cant do lookups on Ice

            //Arrange
            Fluid Water = new Fluid(FluidList.Water);
            Pressure setPressure = Pressure.FromBars(3);
            Temperature setTemperature = Temperature.FromDegreesCelsius(-5);



            //Act
            Water.UpdatePT(setPressure, setTemperature);



            //Assert
            Assert.IsTrue(Water.FailState);

            Assert.AreEqual(0, Water.Conductivity.WattsPerMeterKelvin);
            Assert.AreEqual(0, Water.Cp.JoulesPerKilogramKelvin);
            Assert.AreEqual(0, Water.Cv.JoulesPerKilogramKelvin);
            Assert.AreEqual(0, Water.Enthalpy.JoulesPerKilogram);
            Assert.AreEqual(0, Water.Prandtl);
            Assert.AreEqual(0, Water.Pressure.Bars);
            Assert.AreEqual(0, Water.Density.KilogramsPerCubicMeter);
            Assert.AreEqual(0, Water.Entropy.KilocaloriesPerGramKelvin);
            Assert.AreEqual(0, Water.SurfaceTension.NewtonsPerMeter);
            Assert.AreEqual(0, Water.Temperature.Kelvins);
            Assert.AreEqual(0, Water.Tsat.Kelvins);
            Assert.AreEqual(0, Water.DynamicViscosity.NewtonSecondsPerMeterSquared);
            Assert.AreEqual(0, Water.Quality);
            Assert.AreEqual(0, Water.SoundSpeed.MetersPerSecond);
            Assert.AreEqual(0, Water.MolarMass.GramsPerMole);
            Assert.AreEqual(0, Water.Compressibility);
            Assert.AreEqual(0, Water.InternalEnergy.JoulesPerKilogram);
        }

        private bool AreAproximativellyEqual(double expected, double actual, double abs_err) => Math.Abs(expected - actual) <= abs_err;

        [TestMethod]
        public void InCompWaterGetsCorrectCpValue()
        {
            Fluid liquidWater = new Fluid(FluidList.InCompWater);
            liquidWater.UpdatePT(Pressure.FromBar(1), Temperature.FromDegreesCelsius(20));
            double expected = 4174.8884511999999;
            double actual = liquidWater.Cp.As(SpecificHeatCapacityUnit.JoulePerKilogramDegreeCelsius);

            Assert.IsTrue(AreAproximativellyEqual(expected, actual, 1e-5));
        }
    }
}