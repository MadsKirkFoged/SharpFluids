using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
//using EngineeringUnits;
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

            Assert.AreEqual(null, Water.Conductivity?.WattPerMeterKelvin);
            Assert.AreEqual(null, Water.Cp?.JoulePerKilogramKelvin);
            Assert.AreEqual(null, Water.Cv?.JoulePerKilogramKelvin);
            Assert.AreEqual(null, Water.Enthalpy?.JoulePerKilogram);
            Assert.AreEqual(0, Water.Prandtl);
            Assert.AreEqual(null, Water.Pressure?.Bar);
            Assert.AreEqual(null, Water.Density?.KilogramPerCubicMeter);
            Assert.AreEqual(null, Water.Entropy?.KilocaloriePerGramKelvin);
            Assert.AreEqual(null, Water.SurfaceTension?.NewtonPerMeter);
            Assert.AreEqual(null, Water.Temperature?.Kelvins);
            Assert.AreEqual(null, Water.Tsat?.Kelvins);
            Assert.AreEqual(null, Water.DynamicViscosity?.NewtonSecondPerMeterSquared);
            Assert.AreEqual(0, Water.Quality);
            Assert.AreEqual(null, Water.SoundSpeed?.MeterPerSecond);
            Assert.AreEqual(null, Water.MolarMass?.GramPerMole);
            Assert.AreEqual(0, Water.Compressibility);
            Assert.AreEqual(null, Water.InternalEnergy?.JoulePerKilogram);
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