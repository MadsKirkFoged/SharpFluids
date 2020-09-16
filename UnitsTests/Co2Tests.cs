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

            Assert.AreEqual(44.0098, Co2.MolarMass.GramsPerMole, 0.0001);
            Assert.AreEqual(0.758748472112, Co2.Compressibility, 0.0001);
            Assert.AreEqual(398229.047133, Co2.InternalEnergy.JoulesPerKilogram, 0.0001);
        }



        [TestMethod]
        public void LiquidState()
        {

            //Arrange
            Fluid Co2 = new Fluid(FluidList.CO2);
            Pressure setPressure = Pressure.FromBars(50);
            Temperature setTemperature = Temperature.FromDegreesCelsius(0);



            //Act
            Co2.UpdatePT(setPressure, setTemperature);



            //Assert
            Assert.AreEqual(0.11302927178715505, Co2.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2417.260925841651, Co2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(935.30983351562668, Co2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(198533.51952994024, Co2.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(329138.02273868845, Co2.H_Crit.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2.2073170762964236, Co2.Prandtl, 0.0001);
            Assert.AreEqual(50, Co2.Pressure.Bars, 0.0001);
            Assert.AreEqual(73.773, Co2.P_Crit.Bars, 0.0001);
            Assert.AreEqual(8000, Co2.P_Max.Bars, 0.0001);
            Assert.AreEqual(5.1796434344772573, Co2.P_Min.Bars, 0.0001);
            Assert.AreEqual(940.51699029972065, Co2.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.23630359663511932, Co2.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Co2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(0, Co2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(14.283923802417348, Co2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.978200000000015, Co2.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Co2.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(-56.557999999999964, Co2.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(0.00010321245798083137, Co2.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Co2.X, 0.0001);
            Assert.AreEqual(567.71652802229244, Co2.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(44.0098, Co2.MolarMass.GramsPerMole, 0.0001);
            Assert.AreEqual(0.103018421004, Co2.Compressibility, 0.0001);
            Assert.AreEqual(193217.294462, Co2.InternalEnergy.JoulesPerKilogram, 0.0001);
        }


        [TestMethod]
        public void GasState()
        {

            //Arrange
            Fluid Co2 = new Fluid(FluidList.CO2);
            Pressure setPressure = Pressure.FromBars(50);
            Temperature setTemperature = Temperature.FromDegreesCelsius(40);



            //Act
            Co2.UpdatePT(setPressure, setTemperature);



            //Assert
            Assert.AreEqual(0.023128791374493297, Co2.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(1469.6947917707614, Co2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(807.62943446640077, Co2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(467125.89386438479, Co2.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(329138.02273868845, Co2.H_Crit.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.0820593348809831, Co2.Prandtl, 0.0001);
            Assert.AreEqual(50, Co2.Pressure.Bars, 0.0001);
            Assert.AreEqual(73.773, Co2.P_Crit.Bars, 0.0001);
            Assert.AreEqual(8000, Co2.P_Max.Bars, 0.0001);
            Assert.AreEqual(5.1796434344772573, Co2.P_Min.Bars, 0.0001);
            Assert.AreEqual(113.05213925800003, Co2.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.4588969385489618, Co2.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Co2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(40, Co2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(14.283923802417348, Co2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.978200000000015, Co2.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Co2.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(-56.557999999999964, Co2.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.7028518268838521E-05, Co2.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Co2.X, 0.0001);
            Assert.AreEqual(237.78957338780987, Co2.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(44.0098, Co2.MolarMass.GramsPerMole, 0.0001);
            Assert.AreEqual(0.747569438228, Co2.Compressibility, 0.0001);
            Assert.AreEqual(422898.513161, Co2.InternalEnergy.JoulesPerKilogram, 0.0001);
        }

        [TestMethod]
        public void TranscriticalState()
        {

            //Arrange
            Fluid Co2 = new Fluid(FluidList.CO2);
            Pressure setPressure = Pressure.FromBars(90);
            Temperature setTemperature = Temperature.FromDegreesCelsius(30);



            //Act
            Co2.UpdatePT(setPressure, setTemperature);



            //Assert
            Assert.AreEqual(5.1796434344772573, Co2.P_Min.Bars, 0.0001);
            Assert.AreEqual(8000, Co2.P_Max.Bars, 0.0001);
            Assert.AreEqual(73.773, Co2.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-56.557999999999964, Co2.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.978200000000015, Co2.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Co2.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(329138.02273868845, Co2.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.0832514502504721, Co2.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(3796.4672563772951, Co2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(982.57493262354069, Co2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(276318.70433843043, Co2.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2.8243769924416289, Co2.Prandtl, 0.0001);
            Assert.AreEqual(90, Co2.Pressure.Bars, 0.0001);
            Assert.AreEqual(744.30948867565462, Co2.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.29681456863586464, Co2.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Co2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(30, Co2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(38.808190477557218, Co2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(6.1934810653208115E-05, Co2.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Co2.X, 0.0001);
            Assert.AreEqual(345.78742425860986, Co2.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(44.0098, Co2.MolarMass.GramsPerMole, 0.0001);
            Assert.AreEqual(0.211127165113, Co2.Compressibility, 0.0001);
            Assert.AreEqual(264226.960051, Co2.InternalEnergy.JoulesPerKilogram, 0.0001);
        }


        [TestMethod]
        public void CriticalState()
        {

            //Arrange
            Fluid Co2 = new Fluid(FluidList.CO2);
            Pressure setPressure = Co2.P_Crit;
            Temperature setTemperature = Co2.T_Crit;



            //Act
            Co2.UpdatePT(setPressure, setTemperature);



            //Assert
            Assert.AreEqual(5.1796434344772573, Co2.P_Min.Bars, 0.0001);
            Assert.AreEqual(8000, Co2.P_Max.Bars, 0.0001);
            Assert.AreEqual(73.773, Co2.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-56.557999999999964, Co2.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.978200000000015, Co2.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Co2.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(329138.02273868845, Co2.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.8813992269916463, Co2.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(77531751.281836763, Co2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(3896.0409397795493, Co2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(329138.02273868845, Co2.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2994.270142680401, Co2.Prandtl, 0.0001);
            Assert.AreEqual(73.773000000060662, Co2.Pressure.Bars, 0.0001);
            Assert.AreEqual(480.99114269329687, Co2.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.34020247992073177, Co2.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0, Co2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(30.978200000000015, Co2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.978200000012691, Co2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(3.4039568893124446E-05, Co2.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, Co2.X, 0.0001);
            Assert.AreEqual(100.34676588271913, Co2.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(44.0098, Co2.MolarMass.GramsPerMole, 0.0001);
            Assert.AreEqual(0.266941985609, Co2.Compressibility, 0.0001);
            Assert.AreEqual(313800.3176, Co2.InternalEnergy.JoulesPerKilogram, 0.0001);
        }


        [TestMethod]
        public void LiqGasState()
        {

            //Arrange
            Fluid Co2 = new Fluid(FluidList.CO2);
            Pressure setPressure = Pressure.FromBars(25);
            double X = 0.5;



            //Act
            Co2.UpdatePX(setPressure, X);



            //Assert
            Assert.AreEqual(5.1796434344772573, Co2.P_Min.Bars, 0.0001);
            Assert.AreEqual(8000, Co2.P_Max.Bars, 0.0001);
            Assert.AreEqual(73.773, Co2.P_Crit.Bars, 0.0001);
            Assert.AreEqual(-56.557999999999964, Co2.T_Min.DegreesCelsius, 0.0001);
            Assert.AreEqual(30.978200000000015, Co2.T_Crit.DegreesCelsius, 0.0001);
            Assert.AreEqual(1726.85, Co2.T_Max.DegreesCelsius, 0.0001);
            Assert.AreEqual(329138.02273868845, Co2.H_Crit.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(0.019438568632493974, Co2.Condutivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(9538.9198786323577, Co2.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1328.5132337829052, Co2.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(303821.86127955979, Co2.H.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(7.2362227645827124, Co2.Prandtl, 0.0001);
            Assert.AreEqual(25, Co2.Pressure.Bars, 0.0001);
            Assert.AreEqual(125.15644631203638, Co2.RHO.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.33552673235991382, Co2.S.KilocaloriesPerKelvin, 0.0001);
            Assert.AreEqual(0.006762279814496573, Co2.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(-12.01316976352382, Co2.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(-12.01316976352706, Co2.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.4746094383751521E-05, Co2.Viscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, Co2.X, 0.0001);
            Assert.AreEqual(185.85700925081477, Co2.SoundSpeed.MetersPerSecond, 0.0001);
            Assert.AreEqual(44.0098, Co2.MolarMass.GramsPerMole, 0.0001);
            Assert.AreEqual(0.557960456, Co2.Compressibility, 0.0001);
        }



    }
}