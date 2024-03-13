using EngineeringUnits;
using System.Collections.Generic;

namespace SharpFluids
{
    public static class SHC230
    {

        public static List<CustomOil> GetList()
        {

            var ListOfOil = new List<CustomOil>
            {
                new CustomOil(Temperature.FromDegreeCelsius(-30), KinematicViscosity.FromCentistokes(75984.3), Density.FromKilogramPerCubicMeter(873.6), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.885), ThermalConductivity.FromWattPerMeterKelvin(0.1469)),
                new CustomOil(Temperature.FromDegreeCelsius(-20), KinematicViscosity.FromCentistokes(22001.9), Density.FromKilogramPerCubicMeter(868.2), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.922), ThermalConductivity.FromWattPerMeterKelvin(0.1458)),
                new CustomOil(Temperature.FromDegreeCelsius(-10), KinematicViscosity.FromCentistokes(7604.5), Density.FromKilogramPerCubicMeter(862.9), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.959), ThermalConductivity.FromWattPerMeterKelvin(0.1447)),
                new CustomOil(Temperature.FromDegreeCelsius(0), KinematicViscosity.FromCentistokes(3042.5), Density.FromKilogramPerCubicMeter(857.7), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.995), ThermalConductivity.FromWattPerMeterKelvin(0.1436)),
                new CustomOil(Temperature.FromDegreeCelsius(10), KinematicViscosity.FromCentistokes(1374.9), Density.FromKilogramPerCubicMeter(852.6), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.032), ThermalConductivity.FromWattPerMeterKelvin(0.1426)),
                new CustomOil(Temperature.FromDegreeCelsius(20), KinematicViscosity.FromCentistokes(688), Density.FromKilogramPerCubicMeter(847.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.069), ThermalConductivity.FromWattPerMeterKelvin(0.1415)),
                new CustomOil(Temperature.FromDegreeCelsius(30), KinematicViscosity.FromCentistokes(375.2), Density.FromKilogramPerCubicMeter(842.4), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.106), ThermalConductivity.FromWattPerMeterKelvin(0.1404)),
                new CustomOil(Temperature.FromDegreeCelsius(40), KinematicViscosity.FromCentistokes(220), Density.FromKilogramPerCubicMeter(837.4), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.142), ThermalConductivity.FromWattPerMeterKelvin(0.1394)),
                new CustomOil(Temperature.FromDegreeCelsius(50), KinematicViscosity.FromCentistokes(137.2), Density.FromKilogramPerCubicMeter(832.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.179), ThermalConductivity.FromWattPerMeterKelvin(0.1383)),
                new CustomOil(Temperature.FromDegreeCelsius(60), KinematicViscosity.FromCentistokes(90.2), Density.FromKilogramPerCubicMeter(827.7), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.216), ThermalConductivity.FromWattPerMeterKelvin(0.1372)),
                new CustomOil(Temperature.FromDegreeCelsius(70), KinematicViscosity.FromCentistokes(62.1), Density.FromKilogramPerCubicMeter(822.8), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.253), ThermalConductivity.FromWattPerMeterKelvin(0.1362)),
                new CustomOil(Temperature.FromDegreeCelsius(80), KinematicViscosity.FromCentistokes(44.4), Density.FromKilogramPerCubicMeter(818.1), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.29), ThermalConductivity.FromWattPerMeterKelvin(0.1351)),
                new CustomOil(Temperature.FromDegreeCelsius(90), KinematicViscosity.FromCentistokes(32.8), Density.FromKilogramPerCubicMeter(813.4), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.326), ThermalConductivity.FromWattPerMeterKelvin(0.134)),
                new CustomOil(Temperature.FromDegreeCelsius(100), KinematicViscosity.FromCentistokes(25), Density.FromKilogramPerCubicMeter(808.8), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.363), ThermalConductivity.FromWattPerMeterKelvin(0.1329)),
                new CustomOil(Temperature.FromDegreeCelsius(110), KinematicViscosity.FromCentistokes(19.5), Density.FromKilogramPerCubicMeter(804.2), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.4), ThermalConductivity.FromWattPerMeterKelvin(0.1319)),
                new CustomOil(Temperature.FromDegreeCelsius(120), KinematicViscosity.FromCentistokes(15.6), Density.FromKilogramPerCubicMeter(799.6), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.437), ThermalConductivity.FromWattPerMeterKelvin(0.1308))
            };

            return ListOfOil;

        }
    }
}
