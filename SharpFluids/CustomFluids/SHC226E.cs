using EngineeringUnits;
using System.Collections.Generic;

namespace SharpFluids
{
    public static class SHC226E
    {

        public static List<CustomOil> GetList()
        {

            var ListOfOil = new List<CustomOil>
            {
                new CustomOil(Temperature.FromDegreeCelsius(-30), KinematicViscosity.FromCentistokes(17737.8), Density.FromKilogramPerCubicMeter(853), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.908), ThermalConductivity.FromWattPerMeterKelvin(0.1504)),
                new CustomOil(Temperature.FromDegreeCelsius(-20), KinematicViscosity.FromCentistokes(5242.9), Density.FromKilogramPerCubicMeter(847.8), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.945), ThermalConductivity.FromWattPerMeterKelvin(0.1493)),
                new CustomOil(Temperature.FromDegreeCelsius(-10), KinematicViscosity.FromCentistokes(1874.8), Density.FromKilogramPerCubicMeter(842.6), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.982), ThermalConductivity.FromWattPerMeterKelvin(0.1482)),
                new CustomOil(Temperature.FromDegreeCelsius(0), KinematicViscosity.FromCentistokes(782.7), Density.FromKilogramPerCubicMeter(837.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.019), ThermalConductivity.FromWattPerMeterKelvin(0.1471)),
                new CustomOil(Temperature.FromDegreeCelsius(10), KinematicViscosity.FromCentistokes(370.9), Density.FromKilogramPerCubicMeter(832.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.056), ThermalConductivity.FromWattPerMeterKelvin(0.146)),
                new CustomOil(Temperature.FromDegreeCelsius(20), KinematicViscosity.FromCentistokes(195.1), Density.FromKilogramPerCubicMeter(827.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.094), ThermalConductivity.FromWattPerMeterKelvin(0.1449)),
                new CustomOil(Temperature.FromDegreeCelsius(30), KinematicViscosity.FromCentistokes(111.9), Density.FromKilogramPerCubicMeter(822.6), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.131), ThermalConductivity.FromWattPerMeterKelvin(0.1438)),
                new CustomOil(Temperature.FromDegreeCelsius(40), KinematicViscosity.FromCentistokes(69), Density.FromKilogramPerCubicMeter(817.7), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.168), ThermalConductivity.FromWattPerMeterKelvin(0.1427)),
                new CustomOil(Temperature.FromDegreeCelsius(50), KinematicViscosity.FromCentistokes(45.2), Density.FromKilogramPerCubicMeter(812.9), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.205), ThermalConductivity.FromWattPerMeterKelvin(0.1416)),
                new CustomOil(Temperature.FromDegreeCelsius(60), KinematicViscosity.FromCentistokes(31.1), Density.FromKilogramPerCubicMeter(808.2), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.243), ThermalConductivity.FromWattPerMeterKelvin(0.1405)),
                new CustomOil(Temperature.FromDegreeCelsius(70), KinematicViscosity.FromCentistokes(22.4), Density.FromKilogramPerCubicMeter(803.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.28), ThermalConductivity.FromWattPerMeterKelvin(0.1394)),
                new CustomOil(Temperature.FromDegreeCelsius(80), KinematicViscosity.FromCentistokes(16.7), Density.FromKilogramPerCubicMeter(798.8), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.317), ThermalConductivity.FromWattPerMeterKelvin(0.1383)),
                new CustomOil(Temperature.FromDegreeCelsius(90), KinematicViscosity.FromCentistokes(12.8), Density.FromKilogramPerCubicMeter(794.3), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.354), ThermalConductivity.FromWattPerMeterKelvin(0.1372)),
                new CustomOil(Temperature.FromDegreeCelsius(100), KinematicViscosity.FromCentistokes(10.1), Density.FromKilogramPerCubicMeter(789.7), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.391), ThermalConductivity.FromWattPerMeterKelvin(0.1361)),
                new CustomOil(Temperature.FromDegreeCelsius(110), KinematicViscosity.FromCentistokes(8.2), Density.FromKilogramPerCubicMeter(785.2), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.429), ThermalConductivity.FromWattPerMeterKelvin(0.135)),
                new CustomOil(Temperature.FromDegreeCelsius(120), KinematicViscosity.FromCentistokes(6.7), Density.FromKilogramPerCubicMeter(780.8), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.466), ThermalConductivity.FromWattPerMeterKelvin(0.134))
            };

            return ListOfOil;

        }
    }
}
