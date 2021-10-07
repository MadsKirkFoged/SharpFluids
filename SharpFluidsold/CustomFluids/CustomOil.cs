using EngineeringUnits;
using EngineeringUnits.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return $"{Temperature.DegreesCelsius}";
        }


    }
}
