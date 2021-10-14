using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFluids
{
    public static class SHC230
    {



        public static List<CustomOil> GetList()
        {

            List<CustomOil> ListOfOil = new List<CustomOil>();



            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-30),    KinematicViscosity.FromCentistokes(75984.3),    Density.FromKilogramsPerCubicMeter(873.6), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.885), ThermalConductivity.FromWattsPerMeterKelvin(0.1469)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-20),    KinematicViscosity.FromCentistokes(22001.9),    Density.FromKilogramsPerCubicMeter(868.2), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.922), ThermalConductivity.FromWattsPerMeterKelvin(0.1458)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-10),    KinematicViscosity.FromCentistokes(7604.5),     Density.FromKilogramsPerCubicMeter(862.9), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.959), ThermalConductivity.FromWattsPerMeterKelvin(0.1447)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(0),      KinematicViscosity.FromCentistokes(3042.5),     Density.FromKilogramsPerCubicMeter(857.7), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.995), ThermalConductivity.FromWattsPerMeterKelvin(0.1436)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(10),     KinematicViscosity.FromCentistokes(1374.9),     Density.FromKilogramsPerCubicMeter(852.6), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.032), ThermalConductivity.FromWattsPerMeterKelvin(0.1426)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(20),     KinematicViscosity.FromCentistokes(688),        Density.FromKilogramsPerCubicMeter(847.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.069), ThermalConductivity.FromWattsPerMeterKelvin(0.1415)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(30),     KinematicViscosity.FromCentistokes(375.2),      Density.FromKilogramsPerCubicMeter(842.4), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.106), ThermalConductivity.FromWattsPerMeterKelvin(0.1404)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(40),     KinematicViscosity.FromCentistokes(220),        Density.FromKilogramsPerCubicMeter(837.4), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.142), ThermalConductivity.FromWattsPerMeterKelvin(0.1394)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(50),     KinematicViscosity.FromCentistokes(137.2),      Density.FromKilogramsPerCubicMeter(832.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.179), ThermalConductivity.FromWattsPerMeterKelvin(0.1383)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(60),     KinematicViscosity.FromCentistokes(90.2),       Density.FromKilogramsPerCubicMeter(827.7), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.216), ThermalConductivity.FromWattsPerMeterKelvin(0.1372)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(70),     KinematicViscosity.FromCentistokes(62.1),       Density.FromKilogramsPerCubicMeter(822.8), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.253), ThermalConductivity.FromWattsPerMeterKelvin(0.1362)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(80),     KinematicViscosity.FromCentistokes(44.4),       Density.FromKilogramsPerCubicMeter(818.1), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.29),  ThermalConductivity.FromWattsPerMeterKelvin(0.1351)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(90),     KinematicViscosity.FromCentistokes(32.8),       Density.FromKilogramsPerCubicMeter(813.4), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.326), ThermalConductivity.FromWattsPerMeterKelvin(0.134)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(100),    KinematicViscosity.FromCentistokes(25),         Density.FromKilogramsPerCubicMeter(808.8), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.363), ThermalConductivity.FromWattsPerMeterKelvin(0.1329)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(110),    KinematicViscosity.FromCentistokes(19.5),       Density.FromKilogramsPerCubicMeter(804.2), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.4),   ThermalConductivity.FromWattsPerMeterKelvin(0.1319)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(120),    KinematicViscosity.FromCentistokes(15.6),       Density.FromKilogramsPerCubicMeter(799.6), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.437), ThermalConductivity.FromWattsPerMeterKelvin(0.1308)));



            return ListOfOil;





        }





    }
}
