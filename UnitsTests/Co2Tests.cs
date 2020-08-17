using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using UnitsNet;

namespace UnitsTests
{
    [TestClass]
    public class Co2Lookups
    {
        [TestMethod]
        public void UpdatePX()
        {

            //Arrange
            Fluid Co2 = new Fluid(FluidList.CO2);
            Pressure setPressure = Pressure.FromBars(25);
            double X = 1;



            //Act
            Co2.UpdatePX(setPressure, X);



            //Assert
            Assert.AreEqual(0.016083133611710109, Co2.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(1456.9187615135381, Co2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(800.30959584601669, Co2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(435661.92547518451, Co2.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(329138.02273868845, Co2.H_Crit.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.2412134074282795, Co2.Prandtl, 0.0001);
            Assert.AreEqual(24.999999999999989, Co2.Pressure.Bars, 0.0001);
            Assert.AreEqual(73.773, Co2.P_Crit.Bars, 0.0001);
            Assert.AreEqual(8000, Co2.P_Max.Bars, 0.0001);
            Assert.AreEqual(5.1796434344772573, Co2.P_Min.Bars, 0.0001);
            Assert.AreEqual(66.78620802621117, Co2.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.45619347847526537, Co2.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Co2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(-12.01316976352382, Co2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(-12.013169763523649, Co2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.978200000000015, Co2.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Co2.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(-56.557999999999964, Co2.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.3701931500680655E-05, Co2.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(X, Co2.X, 0.0001);
            Assert.AreEqual(217.753745740, Co2.SoundSpeed.MetersPerSecond, 0.0001);
        }
    }
}
