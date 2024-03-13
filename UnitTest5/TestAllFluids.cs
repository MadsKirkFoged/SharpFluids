using Microsoft.VisualStudio.TestTools.UnitTesting;
//using EngineeringUnits;
//using Microsoft.Extensions.Logging;

namespace UnitsTests;

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

        //    Pressure Reduce = TestFluid.CriticalPressure - Pressure.FromBar(5);
        //    if (Reduce < TestFluid.LimitPressureMin)
        //        Reduce = TestFluid.LimitPressureMin;

        //    //Act
        //    TestFluid.UpdatePX(Reduce, 0.5);

        //    //Assert
        //    Assert.IsFalse(TestFluid.FailState);
        //    Assert.AreNotEqual(0, TestFluid.LimitPressureMin.Bar);
        //    Assert.AreNotEqual(0, TestFluid.LimitPressureMax.Bar);
        //    Assert.AreNotEqual(0, TestFluid.CriticalPressure.Bar);
        //    Assert.AreNotEqual(0, TestFluid.LimitTemperatureMin.DegreeCelsius);
        //    Assert.AreNotEqual(0, TestFluid.CriticalTemperature.DegreeCelsius);
        //    Assert.AreNotEqual(0, TestFluid.LimitTemperatureMax.DegreeCelsius);

        //    //Assert.AreNotEqual(0, TestFluid.Conductivity.WattPerMeterKelvin);
        //    Assert.AreNotEqual(0, TestFluid.Cp.JoulePerKilogramKelvin);
        //    Assert.AreNotEqual(0, TestFluid.Cv.JoulePerKilogramKelvin);
        //    Assert.AreNotEqual(0, TestFluid.Enthalpy.JoulePerKilogram);
        //    //Assert.AreNotEqual(0, TestFluid.Prandtl);
        //    Assert.AreNotEqual(0, TestFluid.Pressure.Bar);
        //    Assert.AreNotEqual(0, TestFluid.Density.KilogramPerCubicMeter);
        //    Assert.AreNotEqual(0, TestFluid.Entropy.JoulesPerKelvin);
        //    //Assert.AreNotEqual(0, TestFluid.SurfaceTension.NewtonPerMeter);
        //    Assert.AreNotEqual(0, TestFluid.Temperature.DegreeCelsius);
        //    Assert.AreNotEqual(0, TestFluid.Tsat.DegreeCelsius);
        //    //Assert.AreNotEqual(0, TestFluid.DynamicViscosity.NewtonSecondPerMeterSquared);
        //    Assert.AreNotEqual(0, TestFluid.Quality);
        //    //Assert.AreNotEqual(0, TestFluid.SoundSpeed.MeterPerSecond);

        //    Assert.AreNotEqual(0, TestFluid.MolarMass.GramPerMole);
        //    Assert.AreNotEqual(0, TestFluid.Compressibility);
        //    Assert.AreNotEqual(0, TestFluid.InternalEnergy.JoulePerKilogram);

        //    //Assert.AreNotEqual(0, TestFluid.MassFlow.KilogramPerSecond);
        //    //Assert.AreNotEqual(0, TestFluid.VolumeFlow.CubicMeterPerSecond);

    }
}