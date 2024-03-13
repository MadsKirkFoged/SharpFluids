using EngineeringUnits;
using System.Collections.Generic;

namespace SharpFluids
{
    public static class S68
    {
        public static List<CustomOil> GetList()
        {

            var ListOfOil = new List<CustomOil>
            {
                new CustomOil(Temperature.FromDegreesFahrenheit(100), KinematicViscosity.FromCentistokes(75), Density.FromPoundPerUSGallon(0.863), SpecificEntropy.FromBtuPerPoundFahrenheit(0.51), ThermalConductivity.FromWattPerMeterKelvin(0.1384587733097)),
                new CustomOil(Temperature.FromDegreesFahrenheit(150), KinematicViscosity.FromCentistokes(25), Density.FromPoundPerUSGallon(0.843), SpecificEntropy.FromBtuPerPoundFahrenheit(0.53), ThermalConductivity.FromWattPerMeterKelvin(0.1331774448254)),
                new CustomOil(Temperature.FromDegreesFahrenheit(200), KinematicViscosity.FromCentistokes(12), Density.FromPoundPerUSGallon(0.827), SpecificEntropy.FromBtuPerPoundFahrenheit(0.54), ThermalConductivity.FromWattPerMeterKelvin(0.1297182904144))
            };

            return ListOfOil;

        }
    }
}
