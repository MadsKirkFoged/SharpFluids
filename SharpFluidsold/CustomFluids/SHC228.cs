using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFluids
{
    public static class SHC228
    {



        public static List<CustomOil> GetList()
        {

            List<CustomOil> ListOfOil = new List<CustomOil>();



            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-30),    KinematicViscosity.FromCentistokes(22124.9),    Density.FromKilogramsPerCubicCentimeter(863.3), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.896), ThermalConductivity.FromWattsPerMeterKelvin(0.1486)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-20),    KinematicViscosity.FromCentistokes(6864.5 ),    Density.FromKilogramsPerCubicCentimeter(858  ), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.933), ThermalConductivity.FromWattsPerMeterKelvin(0.1475)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(-10),    KinematicViscosity.FromCentistokes(2535.8 ),    Density.FromKilogramsPerCubicCentimeter(852.8), SpecificEntropy.FromKilojoulesPerKilogramKelvin(1.97 ), ThermalConductivity.FromWattsPerMeterKelvin(0.1464)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(0),      KinematicViscosity.FromCentistokes(1080.9 ),    Density.FromKilogramsPerCubicCentimeter(847.6), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.007), ThermalConductivity.FromWattsPerMeterKelvin(0.1454)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(10),     KinematicViscosity.FromCentistokes(518.5  ),    Density.FromKilogramsPerCubicCentimeter(842.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.044), ThermalConductivity.FromWattsPerMeterKelvin(0.1443)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(20),     KinematicViscosity.FromCentistokes(274.4  ),    Density.FromKilogramsPerCubicCentimeter(837.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.081), ThermalConductivity.FromWattsPerMeterKelvin(0.1432)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(30),     KinematicViscosity.FromCentistokes(157.6  ),    Density.FromKilogramsPerCubicCentimeter(832.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.118), ThermalConductivity.FromWattsPerMeterKelvin(0.1421)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(40),     KinematicViscosity.FromCentistokes(97     ),    Density.FromKilogramsPerCubicCentimeter(827.6), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.155), ThermalConductivity.FromWattsPerMeterKelvin(0.141 )));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(50),     KinematicViscosity.FromCentistokes(63.3   ),    Density.FromKilogramsPerCubicCentimeter(822.7), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.192), ThermalConductivity.FromWattsPerMeterKelvin(0.1399)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(60),     KinematicViscosity.FromCentistokes(43.3   ),    Density.FromKilogramsPerCubicCentimeter(817.9), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.229), ThermalConductivity.FromWattsPerMeterKelvin(0.1389)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(70),     KinematicViscosity.FromCentistokes(30.9   ),    Density.FromKilogramsPerCubicCentimeter(813.2), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.266), ThermalConductivity.FromWattsPerMeterKelvin(0.1378)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(80),     KinematicViscosity.FromCentistokes(22.9   ),    Density.FromKilogramsPerCubicCentimeter(808.5), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.303), ThermalConductivity.FromWattsPerMeterKelvin(0.1367)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(90),     KinematicViscosity.FromCentistokes(17.5   ),    Density.FromKilogramsPerCubicCentimeter(803.8), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.34 ), ThermalConductivity.FromWattsPerMeterKelvin(0.1356)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(100),    KinematicViscosity.FromCentistokes(13.7   ),    Density.FromKilogramsPerCubicCentimeter(799.2), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.377), ThermalConductivity.FromWattsPerMeterKelvin(0.1345)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(110),    KinematicViscosity.FromCentistokes(11     ),    Density.FromKilogramsPerCubicCentimeter(794.7), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.414), ThermalConductivity.FromWattsPerMeterKelvin(0.1334)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesCelsius(120),    KinematicViscosity.FromCentistokes(9),          Density.FromKilogramsPerCubicCentimeter(790.2), SpecificEntropy.FromKilojoulesPerKilogramKelvin(2.451), ThermalConductivity.FromWattsPerMeterKelvin(0.1324)));



            return ListOfOil;




        }




    }
}
