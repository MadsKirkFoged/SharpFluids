//using EngineeringUnits;
using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;

namespace UnitsTests;

[TestClass]
public class Co2Lookups
{
    [TestMethod]
    public void UpdatePX()
    {

        //Arrange
        var Co2 = new Fluid(FluidList.CO2);
        var setPressure = Pressure.FromBar(25);
        double X = 1;
        var SetMass = Mass.FromKilogram(43);

        //Act
        Co2.UpdatePX(setPressure, X);
        Co2.Mass = SetMass;

        //Assert
        Assert.IsFalse(Co2.FailState);
        Assert.AreEqual(0.017049291589814664, Co2.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(1456.9187615135381, Co2.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(800.30959584601669, Co2.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(435661.92547518451, Co2.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.153755153131331, Co2.Prandtl, 0.0001);
        Assert.AreEqual(24.999999999999989, Co2.Pressure.Bar, 0.0001);
        Assert.AreEqual(73.773, Co2.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(8000, Co2.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(5.1796434344772573, Co2.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(66.78620802621117, Co2.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.45619347847526537, Co2.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0, Co2.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(-12.01316976352382, Co2.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(-12.013169763523649, Co2.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(30.978200000000015, Co2.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(1726.85, Co2.LimitTemperatureMax.DegreeCelsius, 0.0001);
        Assert.AreEqual(-56.557999999999964, Co2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(1.3701931500680655E-05, Co2.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(X, Co2.Quality, 0.0001);
        Assert.AreEqual(0, Co2.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(44.0098, Co2.MolarMass.GramPerMole, 0.0001);
        Assert.AreEqual(0.758748472112, Co2.Compressibility, 0.0001);
        Assert.AreEqual(398229.047133, Co2.InternalEnergy.JoulePerKilogram, 0.0001);

        Assert.AreEqual(43, Co2.Mass.Kilogram, 0.0001);
        Assert.AreEqual(0.64384550749046954, Co2.Volume.CubicMeter, 0.0001);
    }

    [TestMethod]
    public void LiquidState()
    {

        //Arrange
        var Co2 = new Fluid(FluidList.CO2);
        var setPressure = Pressure.FromBar(50);
        var setTemperature = Temperature.FromDegreeCelsius(0);
        var SetMass = Mass.FromKilogram(43);

        //Act
        Co2.UpdatePT(setPressure, setTemperature);
        Co2.Mass = SetMass;

        //Assert
        Assert.IsFalse(Co2.FailState);
        Assert.AreEqual(0.11188873908231274, Co2.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(2417.260925841651, Co2.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(935.30983351562668, Co2.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(198533.51952994024, Co2.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(2.251743224800212, Co2.Prandtl, 0.0001);
        Assert.AreEqual(50, Co2.Pressure.Bar, 0.0001);
        Assert.AreEqual(73.773, Co2.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(8000, Co2.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(5.1796434344772573, Co2.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(940.51699029972065, Co2.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.23630359663511932, Co2.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(Co2.SurfaceTension);
        Assert.AreEqual(0, Co2.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(14.283923802417348, Co2.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(30.978200000000015, Co2.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(1726.85, Co2.LimitTemperatureMax.DegreeCelsius, 0.0001);
        Assert.AreEqual(-56.557999999999964, Co2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(0.00010321245798083137, Co2.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, Co2.Quality, 0.0001);
        Assert.AreEqual(567.71652802229244, Co2.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(44.0098, Co2.MolarMass.GramPerMole, 0.0001);
        Assert.AreEqual(0.103018421004, Co2.Compressibility, 0.0001);
        Assert.AreEqual(193217.294462, Co2.InternalEnergy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(43, Co2.Mass.Kilogram, 0.0001);
        Assert.AreEqual(0.045719535578296049, Co2.Volume.CubicMeter, 0.0001);
    }

    [TestMethod]
    public void GasState()
    {

        //Arrange
        var Co2 = new Fluid(FluidList.CO2);
        var setPressure = Pressure.FromBar(50);
        var setTemperature = Temperature.FromDegreeCelsius(40);
        var SetMass = Mass.FromKilogram(43);

        //Act
        Co2.UpdatePT(setPressure, setTemperature);
        Co2.Mass = SetMass;

        //Assert
        Assert.IsFalse(Co2.FailState);
        Assert.AreEqual(0.02323693174762031, Co2.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(1469.6947917707614, Co2.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(807.62943446640077, Co2.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(467125.89386438479, Co2.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(1.0758671791236813, Co2.Prandtl, 0.0001);
        Assert.AreEqual(50, Co2.Pressure.Bar, 0.0001);
        Assert.AreEqual(73.773, Co2.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(8000, Co2.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(5.1796434344772573, Co2.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(113.05213925800003, Co2.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.4588969385489618, Co2.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(Co2.SurfaceTension);
        Assert.AreEqual(40, Co2.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(14.283923802417348, Co2.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(30.978200000000015, Co2.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(1726.85, Co2.LimitTemperatureMax.DegreeCelsius, 0.0001);
        Assert.AreEqual(-56.557999999999964, Co2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(1.7028518268838521E-05, Co2.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, Co2.Quality, 0.0001);
        Assert.AreEqual(237.78957338780987, Co2.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(44.0098, Co2.MolarMass.GramPerMole, 0.0001);
        Assert.AreEqual(0.747569438228, Co2.Compressibility, 0.0001);
        Assert.AreEqual(422898.513161, Co2.InternalEnergy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(43, Co2.Mass.Kilogram, 0.0001);
        Assert.AreEqual(0.38035547387447732, Co2.Volume.CubicMeter, 0.0001);
    }

    [TestMethod]
    public void TranscriticalState()
    {

        //Arrange
        var Co2 = new Fluid(FluidList.CO2);
        var setPressure = Pressure.FromBar(90);
        var setTemperature = Temperature.FromDegreeCelsius(30);
        var SetMass = Mass.FromKilogram(43);

        //Act
        Co2.UpdatePT(setPressure, setTemperature);
        Co2.Mass = SetMass;

        //Assert
        Assert.IsFalse(Co2.FailState);
        Assert.AreEqual(5.1796434344772573, Co2.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(8000, Co2.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(73.773, Co2.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-56.557999999999964, Co2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(30.978200000000015, Co2.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(1726.85, Co2.LimitTemperatureMax.DegreeCelsius, 0.0001);

        Assert.AreEqual(0.08120357133235015, Co2.Conductivity.WattPerMeterKelvin, 0.0001);
        Assert.AreEqual(3796.4672563772951, Co2.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(982.57493262354069, Co2.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(276318.70433843043, Co2.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(2.9179895982700548, Co2.Prandtl, 0.0001);
        Assert.AreEqual(90, Co2.Pressure.Bar, 0.0001);
        Assert.AreEqual(744.30948867565462, Co2.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.29681456863586464, Co2.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(Co2.SurfaceTension);
        Assert.AreEqual(30, Co2.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(30.978200000000015, Co2.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(6.1934810653208115E-05, Co2.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, Co2.Quality, 0.0001);
        Assert.AreEqual(345.78742425860986, Co2.SoundSpeed.MeterPerSecond, 0.0001);

        Assert.AreEqual(44.0098, Co2.MolarMass.GramPerMole, 0.0001);
        Assert.AreEqual(0.211127165113, Co2.Compressibility, 0.0001);
        Assert.AreEqual(264226.960051, Co2.InternalEnergy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(43, Co2.Mass.Kilogram, 0.0001);
        Assert.AreEqual(0.0577716671011539, Co2.Volume.CubicMeter, 0.0001);
    }

    [TestMethod]
    public void CriticalState()
    {

        //Arrange
        var Co2 = new Fluid(FluidList.CO2);
        Pressure setPressure = Co2.CriticalPressure;
        Temperature setTemperature = Co2.CriticalTemperature;
        var SetMass = Mass.FromKilogram(43);

        //Act
        Co2.UpdatePT(setPressure, setTemperature);
        Co2.Mass = SetMass;

        //Assert
        Assert.IsFalse(Co2.FailState);
        Assert.AreEqual(5.1796434344772573, Co2.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(8000, Co2.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(73.773, Co2.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-56.557999999999964, Co2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(30.978200000000015, Co2.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(1726.85, Co2.LimitTemperatureMax.DegreeCelsius, 0.0001);

        //Conductivity (critical enhancement), Cp, Cv and Prandtl diverge at the critical point, so only their size is checked
        Assert.IsTrue(Co2.Conductivity.WattPerMeterKelvin > 1);
        Assert.IsTrue(Co2.Cp.JoulePerKilogramKelvin > 1e9);
        Assert.IsTrue(Co2.Cv.JoulePerKilogramKelvin > 1e4);
        Assert.AreEqual(332245.6585403426, Co2.Enthalpy.JoulePerKilogram, 1);
        Assert.IsTrue(Co2.Prandtl > 1e4);
        Assert.AreEqual(73.773000000060662, Co2.Pressure.Bar, 0.0001);
        Assert.AreEqual(467.59996991047996, Co2.Density.KilogramPerCubicMeter, 0.001);
        Assert.AreEqual(0.34264468522655805, Co2.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.IsNull(Co2.SurfaceTension);
        Assert.AreEqual(30.978200000000015, Co2.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(30.978200000012691, Co2.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(3.4039568893124446E-05, Co2.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(-1, Co2.Quality, 0.0001);
        //The speed of sound collapses towards zero at the critical point and is badly conditioned there
        Assert.IsTrue(Co2.SoundSpeed.MeterPerSecond is > 0 and < 100);

        Assert.AreEqual(44.0098, Co2.MolarMass.GramPerMole, 0.0001);
        Assert.AreEqual(0.27458633322180614, Co2.Compressibility, 0.0001);
        Assert.AreEqual(316468.71489584417, Co2.InternalEnergy.JoulePerKilogram, 1);
        Assert.AreEqual(43, Co2.Mass.Kilogram, 0.0001);
        Assert.AreEqual(0.09195894518178044, Co2.Volume.CubicMeter, 0.0001);
    }

    [TestMethod]
    public void LiqGasState()
    {

        //Arrange
        var Co2 = new Fluid(FluidList.CO2);
        var setPressure = Pressure.FromBar(25);
        var X = 0.5;
        var SetMass = Mass.FromKilogram(43);

        //Act
        Co2.UpdatePX(setPressure, X);
        Co2.Mass = SetMass;

        //Assert
        Assert.IsFalse(Co2.FailState);
        Assert.AreEqual(5.1796434344772573, Co2.LimitPressureMin.Bar, 0.0001);
        Assert.AreEqual(8000, Co2.LimitPressureMax.Bar, 0.0001);
        Assert.AreEqual(73.773, Co2.CriticalPressure.Bar, 0.0001);
        Assert.AreEqual(-56.557999999999964, Co2.LimitTemperatureMin.DegreeCelsius, 0.0001);
        Assert.AreEqual(30.978200000000015, Co2.CriticalTemperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(1726.85, Co2.LimitTemperatureMax.DegreeCelsius, 0.0001);

        //Conductivity and Prandtl are not checked: CoolProp evaluates the single-phase transport models at the bulk two-phase density, so two-phase values have no physical meaning
        Assert.AreEqual(9538.9198786323577, Co2.Cp.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(1328.5132337829052, Co2.Cv.JoulePerKilogramKelvin, 0.0001);
        Assert.AreEqual(303821.86127955979, Co2.Enthalpy.JoulePerKilogram, 0.0001);
        Assert.AreEqual(25, Co2.Pressure.Bar, 0.0001);
        Assert.AreEqual(125.15644631203638, Co2.Density.KilogramPerCubicMeter, 0.0001);
        Assert.AreEqual(0.33552673235991382, Co2.Entropy.CaloriePerGramKelvin, 0.0001);
        Assert.AreEqual(0.006762279814496573, Co2.SurfaceTension.NewtonPerMeter, 0.0001);
        Assert.AreEqual(-12.01316976352382, Co2.Temperature.DegreeCelsius, 0.0001);
        Assert.AreEqual(-12.01316976352706, Co2.Tsat.DegreeCelsius, 0.0001);
        Assert.AreEqual(1.4746094383751521E-05, Co2.DynamicViscosity.NewtonSecondPerMeterSquared, 0.0001);
        Assert.AreEqual(0.5, Co2.Quality, 0.0001);
        Assert.AreEqual(0, Co2.SoundSpeed.MeterPerSecond, 0.0001);
        Assert.AreEqual(44.0098, Co2.MolarMass.GramPerMole, 0.0001);
        Assert.AreEqual(0.557960456, Co2.Compressibility, 0.0001);
        Assert.AreEqual(43, Co2.Mass.Kilogram, 0.0001);
        Assert.AreEqual(0.34356999792718357, Co2.Volume.CubicMeter, 0.0001);
    }
}