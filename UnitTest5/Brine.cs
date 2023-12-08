using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest5
{
    [TestClass]
    public class Brine
    {
        //Test MixPropyleneGlycolAQ
        [TestMethod]
        public void MixPropyleneGlycolAQ()
        {
            //Arrange
            Fluid brine = new Fluid(FluidList.MixPropyleneGlycolAQ);
            brine.SetFraction(0.5);

            //Act
            brine.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreesCelsius(30));


            //Assert
            Assert.IsFalse(brine.FailState);
            Assert.AreEqual(0.612456680161394, brine.Conductivity.WattsPerMeterKelvin, 0.0001);
            Assert.AreEqual(4175.05834570688, brine.Cp.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(4175.05834570688, brine.Cv.JoulesPerKilogramKelvin, 0.0001);
            Assert.AreEqual(42588.1100414518, brine.Enthalpy.JoulesPerKilogram, 0.0001);
            Assert.AreEqual(5.5313529457465771, brine.Prandtl, 0.0001);
            Assert.AreEqual(10, brine.Pressure.Bars, 0.0001);
            Assert.IsNull(brine.CriticalPressure);
            Assert.IsNull(brine.LimitPressureMax);
            Assert.IsNull(brine.LimitPressureMin);
            Assert.AreEqual(996.347664681825, brine.Density.KilogramsPerCubicMeter, 0.0001);
            Assert.AreEqual(0.033421173347805692, brine.Entropy.CaloriesPerGramKelvin, 0.0001);
            Assert.IsNull(brine.SurfaceTension);
            Assert.AreEqual(30, brine.Temperature.DegreesCelsius, 0.0001);
            Assert.AreEqual(30, brine.Tsat.DegreesCelsius, 0.0001);
            Assert.IsNull(brine.CriticalTemperature);
            Assert.AreEqual(100, brine.LimitTemperatureMax.DegreesCelsius, 0.0001);
            Assert.AreEqual(-100, brine.LimitTemperatureMin.DegreesCelsius, 0.0001);
            Assert.AreEqual(0.00081141717826205, brine.DynamicViscosity.NewtonSecondsPerMeterSquared, 0.0001);
            Assert.AreEqual(0, brine.Quality, 0.0001);
            Assert.IsNull(brine.SoundSpeed);
            Assert.IsNull(brine.MolarMass);
            Assert.AreEqual(0, brine.Compressibility, 0.0001);
            Assert.AreEqual(41584.4443176813, brine.InternalEnergy.JoulesPerKilogram, 0.001);



        }

    }
}
