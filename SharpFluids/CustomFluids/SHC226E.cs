using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFluids
{
    public static class SHC226E
    {



        public static List<CustomOil> GetList()
        {

            List<CustomOil> ListOfOil = new List<CustomOil>();



            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-30),    KinematicViscosity.FromCentistokes(17737.8),    Density.FromKilogramsPerCubicMeter(853),   SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.908), ThermalConductivity.FromWattsPerMeterKelvin(0.1504)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-20),    KinematicViscosity.FromCentistokes(5242.9),     Density.FromKilogramsPerCubicMeter(847.8), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.945), ThermalConductivity.FromWattsPerMeterKelvin(0.1493)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-10),    KinematicViscosity.FromCentistokes(1874.8),     Density.FromKilogramsPerCubicMeter(842.6), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.982), ThermalConductivity.FromWattsPerMeterKelvin(0.1482)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(0),      KinematicViscosity.FromCentistokes(782.7),      Density.FromKilogramsPerCubicMeter(837.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.019), ThermalConductivity.FromWattsPerMeterKelvin(0.1471)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(10),     KinematicViscosity.FromCentistokes(370.9),      Density.FromKilogramsPerCubicMeter(832.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.056), ThermalConductivity.FromWattsPerMeterKelvin(0.146)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(20),     KinematicViscosity.FromCentistokes(195.1),      Density.FromKilogramsPerCubicMeter(827.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.094), ThermalConductivity.FromWattsPerMeterKelvin(0.1449)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(30),     KinematicViscosity.FromCentistokes(111.9),      Density.FromKilogramsPerCubicMeter(822.6), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.131), ThermalConductivity.FromWattsPerMeterKelvin(0.1438)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(40),     KinematicViscosity.FromCentistokes(69),         Density.FromKilogramsPerCubicMeter(817.7), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.168), ThermalConductivity.FromWattsPerMeterKelvin(0.1427)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(50),     KinematicViscosity.FromCentistokes(45.2),       Density.FromKilogramsPerCubicMeter(812.9), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.205), ThermalConductivity.FromWattsPerMeterKelvin(0.1416)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(60),     KinematicViscosity.FromCentistokes(31.1),       Density.FromKilogramsPerCubicMeter(808.2), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.243), ThermalConductivity.FromWattsPerMeterKelvin(0.1405)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(70),     KinematicViscosity.FromCentistokes(22.4),       Density.FromKilogramsPerCubicMeter(803.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.28),  ThermalConductivity.FromWattsPerMeterKelvin(0.1394)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(80),     KinematicViscosity.FromCentistokes(16.7),       Density.FromKilogramsPerCubicMeter(798.8), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.317), ThermalConductivity.FromWattsPerMeterKelvin(0.1383)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(90),     KinematicViscosity.FromCentistokes(12.8),       Density.FromKilogramsPerCubicMeter(794.3), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.354), ThermalConductivity.FromWattsPerMeterKelvin(0.1372)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(100),    KinematicViscosity.FromCentistokes(10.1),       Density.FromKilogramsPerCubicMeter(789.7), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.391), ThermalConductivity.FromWattsPerMeterKelvin(0.1361)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(110),    KinematicViscosity.FromCentistokes(8.2),        Density.FromKilogramsPerCubicMeter(785.2), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.429), ThermalConductivity.FromWattsPerMeterKelvin(0.135)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(120),    KinematicViscosity.FromCentistokes(6.7),        Density.FromKilogramsPerCubicMeter(780.8), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.466), ThermalConductivity.FromWattsPerMeterKelvin(0.134)));



            return ListOfOil;




        }


    }
}
