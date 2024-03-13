using EngineeringUnits;

namespace SharpFluids
{
    public class CustomOil
    {

        public Temperature Temperature { get; set; }

        public KinematicViscosity KinematicViscosity { get; set; }

        public Density Density { get; set; }

        public SpecificEntropy Cp { get; set; }

        public ThermalConductivity ThermalConductivity { get; set; }

        public CustomOil(Temperature temperature, KinematicViscosity kinematicViscosity, Density density, SpecificEntropy specificEntropy, ThermalConductivity thermalConductivity)
        {
            Temperature = temperature;
            KinematicViscosity = kinematicViscosity;
            Density = density;
            Cp = specificEntropy;
            ThermalConductivity = thermalConductivity;
        }

        public override string ToString() => $"{Temperature.DegreesCelsius}";

    }
}
