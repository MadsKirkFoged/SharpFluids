using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
//using EngineeringUnits;
using EngineeringUnits;

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
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(2148349.72016398);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;



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
            Assert.AreEqual(0, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
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
        }
        [TestMethod]
        public void UpdateDHFailedTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(-5);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(-100);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;



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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdateDHFailedTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(1000000);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(10000000);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;



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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }


        [TestMethod]
        public void UpdateDP()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.362783819911829);
            Pressure setPressure = Pressure.FromBars(10);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdateDP(setDensity, setPressure);
            R717.MassFlow = setMassFlow;



            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.912702090002085, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.912702089993616, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
            Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }
        [TestMethod]
        public void UpdateDPTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(-1);
            Pressure setPressure = Pressure.FromBars(-10);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdateDP(setDensity, setPressure);
            R717.MassFlow = setMassFlow;



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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdateDPTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(100000000);
            Pressure setPressure = Pressure.FromBars(100000);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdateDP(setDensity, setPressure);
            R717.MassFlow = setMassFlow;



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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }



        [TestMethod]
        public void UpdateDS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(50);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(6000);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDS(setDensity, setEntropy);
            R717.MassFlow = setMassFlow;


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
            Assert.AreEqual(0.938889018776402, R717.Prandtl, 0.0001);
            Assert.AreEqual(117.968517492664, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(50, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.434034416826004, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(278.39210805142, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.08697088998702E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(10000, R717.Quality, 0.0001);
            Assert.AreEqual(544.366706172041, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.87621627068822361, R717.Compressibility, 0.0001);
            Assert.AreEqual(1912412.68517865, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.04, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }
        [TestMethod]
        public void UpdateDSTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(-15.362783819911829);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(-3846.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDS(setDensity, setEntropy);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdateDSTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(1000000);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(1000000000);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDS(setDensity, setEntropy);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }

        [TestMethod]
        public void UpdateDT()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.362783819911829);
            Temperature setTemperature = Temperature.FromDegreesCelsius(24.912702090002085);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDT(setDensity, setTemperature);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.912702090002085, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.912702089993616, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
            Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }
        [TestMethod]
        public void UpdateDTTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(-15.362783819911829);
            Temperature setTemperature = Temperature.FromDegreesCelsius(-24.912702090002085);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDT(setDensity, setTemperature);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdateDTTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(90000);
            Temperature setTemperature = Temperature.FromDegreesCelsius(90000);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDT(setDensity, setTemperature);
            R717.MassFlow = setMassFlow;


            //Assert
           //Nothing is too high...
        }

        [TestMethod]
        public void UpdateHS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1045846.5098055181);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(3846.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateHS(setEnthalpy, setEntropy);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.912702090002085, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.912702089993616, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
            Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }
        [TestMethod]
        public void UpdateHSTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(-1045000000846.5098055181);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(-38000000046.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateHS(setEnthalpy, setEntropy);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdateHSTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(104500000000846.5098055181);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(3800000000046.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateHS(setEnthalpy, setEntropy);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }

        [TestMethod]
        public void UpdatePH()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1045846.5098055181);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePH(setPressure, setEnthalpy);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.912702090002085, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.912702089993616, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
            Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }
        [TestMethod]
        public void UpdatePHTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(-10);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(-1045846.5098055181);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePH(setPressure, setEnthalpy);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdatePHTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(100000);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(104580046.5098055181);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePH(setPressure, setEnthalpy);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }

        [TestMethod]
        public void UpdatePS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(3846.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePS(setPressure, setEntropy);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.912702090002085, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.912702089993616, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
            Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }
        [TestMethod]
        public void UpdatePSTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(-10);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(-3846.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePS(setPressure, setEntropy);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdatePSTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(1000000000);
            SpecificEntropy setEntropy = SpecificEntropy.FromJoulesPerKilogramKelvin(38400000006.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePS(setPressure, setEntropy);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }

        [TestMethod]
        public void UpdatePT()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            Temperature setTemperature = Temperature.FromDegreesCelsius(100);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePT(setPressure, setTemperature);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.034349411318899653, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2464.8830983211074, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1843.882651806066, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1829344.4765594436, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.92250631570868957, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(5.7513489580705048, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.5308777308424815, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(100, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.912702090001176, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(1.2855599076541866E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(-1, R717.Quality, 0.0001);
            Assert.AreEqual(470.62060167332942, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.95442258123496115, R717.Compressibility, 0.0001);
            Assert.AreEqual(1655472.2237557317, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.34774450560742387, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }
        [TestMethod]
        public void UpdatePTTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(-10);
            Temperature setTemperature = Temperature.FromDegreesCelsius(-1000);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdatePT(setPressure, setTemperature);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdatePTTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(9000000000000000000);
            Temperature setTemperature = Temperature.FromDegreesCelsius(9000000000000000000);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdatePT(setPressure, setTemperature);
            R717.MassFlow = setMassFlow;


            //Assert
            //Nothing is too high....
        }

        [TestMethod]
        public void UpdatePTCrit()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePT(R717.CriticalPressure, R717.CriticalTemperature + Temperature.FromKelvins(0.1));
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
        }

        [TestMethod]
        public void UpdatePX()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            double X = 0.5;
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdatePX(setPressure, X);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.912702090002085, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.912702089993616, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
            Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }

        [TestMethod]
        public void UpdatePXTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(-10);
            double X = -0.5;
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdatePX(setPressure, X);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }
        [TestMethod]
        public void UpdatePXTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10000000);
            double X = 0.5;
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdatePX(setPressure, X);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsTrue(R717.FailState);

            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            //Assert.AreEqual(0, R717.Conductivity.WattsPerMeterKelvin);
            //Assert.AreEqual(0, R717.Cp.JoulesPerKilogramKelvin);
            //Assert.AreEqual(0, R717.Cv.JoulesPerKilogramKelvin);
            //Assert.AreEqual(0, R717.Enthalpy.JoulesPerKilogram);
            //Assert.AreEqual(0, R717.Prandtl);
            //Assert.AreEqual(0, R717.Pressure.Bars);
            //Assert.AreEqual(0, R717.Density.KilogramsPerCubicMeter);
            //Assert.AreEqual(0, R717.Entropy.JoulesPerKilogramKelvin);
            //Assert.AreEqual(0, R717.SurfaceTension.NewtonsPerMeter);
            //Assert.AreEqual(0, R717.Temperature.Kelvins);
            //Assert.AreEqual(0, R717.Tsat.Kelvins);
            //Assert.AreEqual(0, R717.DynamicViscosity.NewtonSecondsPerMeterSquared);
            //Assert.AreEqual(0, R717.Quality);
            //Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond);

            //Assert.AreEqual(0, R717.MolarMass.GramsPerMole);
            //Assert.AreEqual(0, R717.Compressibility);
            //Assert.AreEqual(0, R717.InternalEnergy.JoulesPerKilogram);

            //Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond);
            //Assert.AreEqual(0, R717.VolumeFlow.CubicMetersPerSecond);
        }

        [TestMethod]
        public void UpdatePXCrit()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Fluid R717X0 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = R717.CriticalPressure;
            double X = 1;
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdatePX(setPressure, X);
            R717X0.UpdatePX(setPressure, 0);
            R717.MassFlow = setMassFlow;
            R717X0.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            //Assert.AreEqual(0, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(73158204704795.4, R717.Cp.JoulesPerKilogramKelvin, 1);
            Assert.AreEqual(6467.97163351429, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1247802.1607166, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(2513829873.6623368, R717.Prandtl, 0.0001);
            Assert.AreEqual(R717.CriticalPressure.Bars, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(233.25000192, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.95486829094462955, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(R717.CriticalTemperature.DegreesCelsius, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717.CriticalTemperature.DegreesCelsius, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(2.7906634676727E-05, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(1, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.24605095619157291, R717.Compressibility, 0.0001);
            Assert.AreEqual(1199084.51411734, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.008574490819022413, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);


            //Compare X=0 and X=1
            Assert.AreEqual(R717X0.Conductivity.WattsPerMeterKelvin, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(R717X0.Cp.JoulesPerKilogramKelvin, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(R717X0.Cv.JoulesPerKilogramKelvin, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(R717X0.Enthalpy.JoulesPerKilogram, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(R717X0.Prandtl, R717.Prandtl, 0.0001);
            Assert.AreEqual(R717X0.Pressure.Bars, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(R717X0.Density.KilogramsPerCubicMeter, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(R717X0.Entropy.CaloriesPerGramKelvin, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(R717X0.SurfaceTension.NewtonsPerMeter, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(R717X0.Temperature.DegreesCelsius, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717X0.Tsat.DegreesCelsius, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(R717X0.DynamicViscosity.NewtonSecondsPerMeterSquared, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(R717X0.SoundSpeed.MetersPerSecond, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(R717X0.MolarMass.GramsPerMole, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(R717X0.Compressibility, R717.Compressibility, 0.0001);
            Assert.AreEqual(R717X0.InternalEnergy.JoulesPerKilogram, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(R717X0.MassFlow.KilogramsPerSecond, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(R717X0.VolumeFlow.CubicMetersPerSecond, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);



        }


        [TestMethod]
        public void UpdateXT()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Temperature setTemperature = Temperature.FromDegreesCelsius(24.912702090002085);
            double X = 0.5;
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateXT(X, setTemperature);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.IsFalse(R717.FailState);
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.634, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.655, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.41, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.AreEqual(0.020506397334553166, R717.SurfaceTension.NewtonsPerMeter, 0.0001);
            Assert.AreEqual(24.912702090002085, R717.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(24.912702089993616, R717.Tsat.DegreesCelsius, 0.0001);
            Assert.AreEqual(9.6039015089135839E-06, R717.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0.5, R717.Quality, 0.0001);
            Assert.AreEqual(0, R717.SoundSpeed.MetersPerSecond, 0.0001);

            Assert.AreEqual(17.03052, R717.MolarMass.GramsPerMole, 0.001);
            Assert.AreEqual(0.76931601456949461, R717.Compressibility, 0.0001);
            Assert.AreEqual(980754.14036763739, R717.InternalEnergy.JoulesPerKilogram, 0.0001);

            Assert.AreEqual(2, R717.MassFlow.KilogramsPerSecond, 0.0001);
            Assert.AreEqual(0.13018473887576182, R717.VolumeFlow.CubicMetersPerSecond, 0.0001);
        }
        [TestMethod]
        public void UpdateXTTooLow()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Temperature setTemperature = Temperature.FromDegreesCelsius(-204.912702090002085);
            double X = 0.5;
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateXT(X, setTemperature);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }


        [TestMethod]
        public void UpdateXTTooHigh()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Temperature setTemperature = Temperature.FromDegreesCelsius(20000000000004);
            double X = 100;
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateXT(X, setTemperature);
            R717.MassFlow = setMassFlow;


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

            Assert.AreEqual(2, R717.MassFlow.KilogramPerSecond);
            Assert.AreEqual(null, R717.VolumeFlow);
        }

        [TestMethod]
        public void Phase()
        {
            Fluid amonia = new Fluid(FluidList.Ammonia);

            amonia.UpdatePT(Pressure.FromPascals(582999.74), Temperature.FromKelvins(282));
            Assert.AreEqual(Phases.Gas, amonia.Phase, "Should be in gas phase");

            amonia.UpdatePT(Pressure.FromPascals(582999.74), Temperature.FromKelvins(281));
            Assert.AreEqual(Phases.Liquid, amonia.Phase, "Should be in liquid phase");

            amonia.UpdatePX(Pressure.FromPascals(582999.74), 0.5);
            Assert.AreEqual(Phases.Twophase, amonia.Phase, "Should be in twophase");

            amonia.UpdatePT(Pressure.FromPascals(11433000.0), Temperature.FromKelvins(410.4));
            Assert.AreEqual(Phases.Supercritical, amonia.Phase, "Should be in supercritical phase");

            amonia.UpdatePT(Pressure.FromPascals(11233000.0), Temperature.FromKelvins(410.4));
            Assert.AreEqual(Phases.SupercriticalGas, amonia.Phase, "Should be in supercritical gas phase");

            amonia.UpdatePT(Pressure.FromPascals(11433000.0), Temperature.FromKelvins(400.4));
            Assert.AreEqual(Phases.SupercriticalLiquid, amonia.Phase, "Should be in supercritical liquid phase");
        }
    }
}