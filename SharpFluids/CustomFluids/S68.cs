using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFluids
{
    public static class S68
    {



        public static List<CustomOil> GetList()
        {

            List<CustomOil> ListOfOil = new List<CustomOil>();



            ListOfOil.Add(new CustomOil(Temperature.FromDegreesFahrenheit(100), KinematicViscosity.FromCentistokes(75), Density.FromPoundsPerUSGallon(0.863), SpecificEntropy.FromBtusPerPoundFahrenheit(0.51), ThermalConductivity.FromWattsPerMeterKelvin(0.1384587733097)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesFahrenheit(150), KinematicViscosity.FromCentistokes(25), Density.FromPoundsPerUSGallon(0.843), SpecificEntropy.FromBtusPerPoundFahrenheit(0.53), ThermalConductivity.FromWattsPerMeterKelvin(0.1331774448254)));
            ListOfOil.Add(new CustomOil(Temperature.FromDegreesFahrenheit(200), KinematicViscosity.FromCentistokes(12), Density.FromPoundsPerUSGallon(0.827), SpecificEntropy.FromBtusPerPoundFahrenheit(0.54), ThermalConductivity.FromWattsPerMeterKelvin(0.1297182904144)));




            return ListOfOil;





        }





    }
}
