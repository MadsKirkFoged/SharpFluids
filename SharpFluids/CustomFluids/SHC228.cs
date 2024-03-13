using EngineeringUnits;
using System.Collections.Generic;

namespace SharpFluids
{
    public static class SHC228
    {

        public static List<CustomOil> GetList()
        {

            var ListOfOil = new List<CustomOil>
            {
                new CustomOil(Temperature.FromDegreeCelsius(-30), KinematicViscosity.FromCentistokes(22124.9), Density.FromKilogramPerCubicMeter(863.3), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.896), ThermalConductivity.FromWattPerMeterKelvin(0.1486)),
                new CustomOil(Temperature.FromDegreeCelsius(-20), KinematicViscosity.FromCentistokes(6864.5), Density.FromKilogramPerCubicMeter(858), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.933), ThermalConductivity.FromWattPerMeterKelvin(0.1475)),
                new CustomOil(Temperature.FromDegreeCelsius(-10), KinematicViscosity.FromCentistokes(2535.8), Density.FromKilogramPerCubicMeter(852.8), SpecificEntropy.FromKilojoulePerKilogramKelvin(1.97), ThermalConductivity.FromWattPerMeterKelvin(0.1464)),
                new CustomOil(Temperature.FromDegreeCelsius(0), KinematicViscosity.FromCentistokes(1080.9), Density.FromKilogramPerCubicMeter(847.6), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.007), ThermalConductivity.FromWattPerMeterKelvin(0.1454)),
                new CustomOil(Temperature.FromDegreeCelsius(10), KinematicViscosity.FromCentistokes(518.5), Density.FromKilogramPerCubicMeter(842.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.044), ThermalConductivity.FromWattPerMeterKelvin(0.1443)),
                new CustomOil(Temperature.FromDegreeCelsius(20), KinematicViscosity.FromCentistokes(274.4), Density.FromKilogramPerCubicMeter(837.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.081), ThermalConductivity.FromWattPerMeterKelvin(0.1432)),
                new CustomOil(Temperature.FromDegreeCelsius(30), KinematicViscosity.FromCentistokes(157.6), Density.FromKilogramPerCubicMeter(832.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.118), ThermalConductivity.FromWattPerMeterKelvin(0.1421)),
                new CustomOil(Temperature.FromDegreeCelsius(40), KinematicViscosity.FromCentistokes(97), Density.FromKilogramPerCubicMeter(827.6), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.155), ThermalConductivity.FromWattPerMeterKelvin(0.141)),
                new CustomOil(Temperature.FromDegreeCelsius(50), KinematicViscosity.FromCentistokes(63.3), Density.FromKilogramPerCubicMeter(822.7), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.192), ThermalConductivity.FromWattPerMeterKelvin(0.1399)),
                new CustomOil(Temperature.FromDegreeCelsius(60), KinematicViscosity.FromCentistokes(43.3), Density.FromKilogramPerCubicMeter(817.9), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.229), ThermalConductivity.FromWattPerMeterKelvin(0.1389)),
                new CustomOil(Temperature.FromDegreeCelsius(70), KinematicViscosity.FromCentistokes(30.9), Density.FromKilogramPerCubicMeter(813.2), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.266), ThermalConductivity.FromWattPerMeterKelvin(0.1378)),
                new CustomOil(Temperature.FromDegreeCelsius(80), KinematicViscosity.FromCentistokes(22.9), Density.FromKilogramPerCubicMeter(808.5), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.303), ThermalConductivity.FromWattPerMeterKelvin(0.1367)),
                new CustomOil(Temperature.FromDegreeCelsius(90), KinematicViscosity.FromCentistokes(17.5), Density.FromKilogramPerCubicMeter(803.8), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.34), ThermalConductivity.FromWattPerMeterKelvin(0.1356)),
                new CustomOil(Temperature.FromDegreeCelsius(100), KinematicViscosity.FromCentistokes(13.7), Density.FromKilogramPerCubicMeter(799.2), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.377), ThermalConductivity.FromWattPerMeterKelvin(0.1345)),
                new CustomOil(Temperature.FromDegreeCelsius(110), KinematicViscosity.FromCentistokes(11), Density.FromKilogramPerCubicMeter(794.7), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.414), ThermalConductivity.FromWattPerMeterKelvin(0.1334)),
                new CustomOil(Temperature.FromDegreeCelsius(120), KinematicViscosity.FromCentistokes(9), Density.FromKilogramPerCubicMeter(790.2), SpecificEntropy.FromKilojoulePerKilogramKelvin(2.451), ThermalConductivity.FromWattPerMeterKelvin(0.1324))
            };

            return ListOfOil;

        }
    }
}
