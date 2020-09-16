using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using UnitsNet;

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
            Assert.AreEqual(0.028215527552977144, Water.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2263.0469328306258, Water.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1671.0843504212746, Water.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2724882.6302959691, Water.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2087299.8265576197, Water.H_Crit.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.0742998825687049, Water.Prandtl, 0.0001);
            Assert.AreEqual(3.0000000000000004, Water.Pressure.Bars, 0.0001);
            Assert.AreEqual(220.64, Water.P_Crit.Bars, 0.0001);
            Assert.AreEqual(10000, Water.P_Max.Bars, 0.0001);
            Assert.AreEqual(0.0061165480089686846, Water.P_Min.Bars, 0.0001);
            Assert.AreEqual(1.650819966799242, Water.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.6710365869337238, Water.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Water.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(133.522420460943, Water.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(133.52242046094256, Water.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(373.946, Water.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Water.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(0.010000000000047749, Water.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.3394303713738338E-05, Water.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(X, Water.X, 0.0001);
            Assert.AreEqual(487.367740010, Water.SoundSpeed.MetersPerSecond, 0.0001);
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
            Assert.AreEqual(0.03752160884674547, Water.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2034.1444264493639, Water.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1539.4771263572343, Water.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2941434.7212755163, Water.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2087299.8265576197, Water.H_Crit.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.95772763107228476, Water.Prandtl, 0.0001);
            Assert.AreEqual(3, Water.Pressure.Bars, 0.0001);
            Assert.AreEqual(220.64, Water.P_Crit.Bars, 0.0001);
            Assert.AreEqual(10000, Water.P_Max.Bars, 0.0001);
            Assert.AreEqual(0.0061165480089686846, Water.P_Min.Bars, 0.0001);
            Assert.AreEqual(1.2890299515474706, Water.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.7846226274892405, Water.S.KilocaloriesPerKelvin, 0.0001);
            //Assert.AreEqual(0.052144997752313106, Water.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(237, Water.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(133.522420460943, Water.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(373.946, Water.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Water.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(0.010000000000047749, Water.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.7666140657249425E-05, Water.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Water.X, 0.0001);
            Assert.AreEqual(551.279827045, Water.SoundSpeed.MetersPerSecond, 0.0001);

        }


    }
}