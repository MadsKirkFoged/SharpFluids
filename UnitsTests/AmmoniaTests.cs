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
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.362783819911829);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1045846.5098055181);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDH(setDensity, setEnthalpy);
            R717.MassFlow = setMassFlow;



            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(3846.6363350450893, R717.Entropy.JoulesPerKelvin, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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
        public void UpdateDS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Density setDensity = Density.FromKilogramsPerCubicMeter(15.362783819911829);
            Entropy setEntropy = Entropy.FromJoulesPerKelvin(3846.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateDS(setDensity, setEntropy);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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
        public void UpdateHS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            SpecificEnergy setEnthalpy = SpecificEnergy.FromJoulesPerKilogram(1045846.5098055181);
            Entropy setEntropy = Entropy.FromJoulesPerKelvin(3846.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);


            //Act
            R717.UpdateHS(setEnthalpy, setEntropy);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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
        public void UpdatePS()
        {

            //Arrange
            Fluid R717 = new Fluid(FluidList.Ammonia);
            Pressure setPressure = Pressure.FromBars(10);
            Entropy setEntropy = Entropy.FromJoulesPerKelvin(3846.6363350450893);
            MassFlow setMassFlow = MassFlow.FromKilogramsPerSecond(2);



            //Act
            R717.UpdatePS(setPressure, setEntropy);
            R717.MassFlow = setMassFlow;


            //Assert
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.034349411318899653, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(2464.8830983211074, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1843.882651806066, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1829344.4765594436, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(0.92250631570868957, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(5.7513489580705048, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(1.5308777308424815, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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
            Assert.AreEqual(0.060912231081315084, R717.LimitPressureMin.Bars, 0.0001);
            Assert.AreEqual(10000, R717.LimitPressureMax.Bars, 0.0001);
            Assert.AreEqual(113.33, R717.CriticalPressure.Bars, 0.0001);
            Assert.AreEqual(-77.654999999999973, R717.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(132.25, R717.CriticalTemperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(451.85, R717.LimitTemperatureMax.DegreesCelsius, 0.0001);


            Assert.AreEqual(0.02758417186590599, R717.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4946.6414603438907, R717.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(2789.2425141052067, R717.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(1045846.5098055181, R717.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(1.7222578809324292, R717.Prandtl, 0.0001);
            Assert.AreEqual(10, R717.Pressure.Bars, 0.0001);
            Assert.AreEqual(15.362783819911829, R717.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.91936814891141638, R717.Entropy.KilocaloriesPerKelvin, 0.0001);
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






    }
}