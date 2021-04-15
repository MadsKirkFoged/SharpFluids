using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using UnitsNet;
//using Microsoft.Extensions.Logging;

namespace UnitsTests
{
    [TestClass]
    public class TestAllFluids
    {

        [TestMethod]
        public void TestAllHEOS()
        {

            ////Arrange
            //foreach (FluidList suit in (FluidList[])Enum.GetValues(typeof(FluidList)))
            //{
            //    //Arrange
            //    Fluid TestFluid = new Fluid(suit);

            //    if (TestFluid.Media.BackendType != "HEOS")
            //    {
            //        continue;
            //    }


            //    using (var loggerFactory = LoggerFactory.Create(builder =>
            //    { builder.AddConsole(); }))

            //    {
            //        ILogger logger = loggerFactory.CreateLogger<TestAllFluids>();
            //        logger.LogInformation("Logging has stared");
            //        TestFluid.Log = logger;

            //    }


            //    Pressure Reduce = TestFluid.CriticalPressure - Pressure.FromBars(5);
            //    if (Reduce < TestFluid.LimitPressureMin)
            //        Reduce = TestFluid.LimitPressureMin;

            //    //Act
            //    TestFluid.UpdatePX(Reduce, 0.5);

            //    //Assert
            //    Assert.IsFalse(TestFluid.FailState);
            //    Assert.AreNotEqual(0, TestFluid.LimitPressureMin.Bars);
            //    Assert.AreNotEqual(0, TestFluid.LimitPressureMax.Bars);
            //    Assert.AreNotEqual(0, TestFluid.CriticalPressure.Bars);
            //    Assert.AreNotEqual(0, TestFluid.LimitTemperatureMin.DegreesCelsius);
            //    Assert.AreNotEqual(0, TestFluid.CriticalTemperature.DegreesCelsius);
            //    Assert.AreNotEqual(0, TestFluid.LimitTemperatureMax.DegreesCelsius);


            //    //Assert.AreNotEqual(0, TestFluid.Conductivity.WattsPerMeterKelvin);
            //    Assert.AreNotEqual(0, TestFluid.Cp.JoulesPerKilogramKelvin);
            //    Assert.AreNotEqual(0, TestFluid.Cv.JoulesPerKilogramKelvin);
            //    Assert.AreNotEqual(0, TestFluid.Enthalpy.JoulesPerKilogram);
            //    //Assert.AreNotEqual(0, TestFluid.Prandtl);
            //    Assert.AreNotEqual(0, TestFluid.Pressure.Bars);
            //    Assert.AreNotEqual(0, TestFluid.Density.KilogramsPerCubicMeter);
            //    Assert.AreNotEqual(0, TestFluid.Entropy.JoulesPerKelvin);
            //    //Assert.AreNotEqual(0, TestFluid.SurfaceTension.NewtonsPerMeter);
            //    Assert.AreNotEqual(0, TestFluid.Temperature.DegreesCelsius);
            //    Assert.AreNotEqual(0, TestFluid.Tsat.DegreesCelsius);
            //    //Assert.AreNotEqual(0, TestFluid.DynamicViscosity.NewtonSecondsPerMeterSquared);
            //    Assert.AreNotEqual(0, TestFluid.Quality);
            //    //Assert.AreNotEqual(0, TestFluid.SoundSpeed.MetersPerSecond);

            //    Assert.AreNotEqual(0, TestFluid.MolarMass.GramsPerMole);
            //    Assert.AreNotEqual(0, TestFluid.Compressibility);
            //    Assert.AreNotEqual(0, TestFluid.InternalEnergy.JoulesPerKilogram);

            //    //Assert.AreNotEqual(0, TestFluid.MassFlow.KilogramsPerSecond);
            //    //Assert.AreNotEqual(0, TestFluid.VolumeFlow.CubicMetersPerSecond);

                



            

            
        }
        
      

    }
}