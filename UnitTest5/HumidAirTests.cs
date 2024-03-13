using EngineeringUnits;
using EngineeringUnits.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using System;

namespace UnitTest5
{
    [TestClass]
    public class HumidAirTests
    {
        [TestMethod]
        public void MoistAirDensityTest()
        {
            // define a function to test equality for two double values since Assert do not 
            // have such of function to my knowledge 
            Func<double, double, double, bool> AlmostEqual = (expect, actual, prec) =>
                Math.Abs(expect - actual) <= prec;

            Temperature tatm = Temperature.FromDegreeCelsius(20.0);
            Pressure patm = Pressure.FromAtmosphere(1.0);
            double rh = 0.4; // 40 %

            /// the expected value was tooked from Python CoolProp 6.4.1
            /// this is the command:
            /// spec_volume = HAPropsSI('Vha','T', Tatm.to('K').m, 'P',Patm.to('Pa').m,'RH',rh)
            /// density = 1/spec_volume

            var expected = 1.200406648931211;
            var sut = new MoistAir();


            sut.UpdateAir(pressure: patm, DryBulbTemperature: tatm, RelativeHumidity: rh);

            var actual = sut.Density.As(DensityUnit.KilogramPerCubicMeter);

            Assert.IsTrue(AlmostEqual(expected, actual,1e-5));
        }
    }
}
