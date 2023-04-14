using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFluids
{
    public static class Number13
    {



        public static List<CustomOil> GetList()
        {

            List<CustomOil> ListOfOil = new List<CustomOil>
            {

                new CustomOil(Temperature.FromDegreesCelsius(   -30 ),KinematicViscosity.FromCentistokes(   23903.374   ), Density.FromKilogramsPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.381   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1837  )),
                new CustomOil(Temperature.FromDegreesCelsius(   -20 ),KinematicViscosity.FromCentistokes(   6367.512    ), Density.FromKilogramsPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.242   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1872  )),
                new CustomOil(Temperature.FromDegreesCelsius(   -10 ),KinematicViscosity.FromCentistokes(   2103.033    ), Density.FromKilogramsPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.142   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1901  )),
                new CustomOil(Temperature.FromDegreesCelsius(   0   ),KinematicViscosity.FromCentistokes(   826.225 ), Density.FromKilogramsPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.074   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1924  )),
                new CustomOil(Temperature.FromDegreesCelsius(   10  ),KinematicViscosity.FromCentistokes(   373.795 ), Density.FromKilogramsPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.033   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1941  )),
                new CustomOil(Temperature.FromDegreesCelsius(   20  ),KinematicViscosity.FromCentistokes(   189.802 ), Density.FromKilogramsPerCubicMeter(  943 ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.014   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1952  )),
                new CustomOil(Temperature.FromDegreesCelsius(   30  ),KinematicViscosity.FromCentistokes(   105.975 ), Density.FromKilogramsPerCubicMeter(  954.2   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.012   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1957  )),
                new CustomOil(Temperature.FromDegreesCelsius(   40  ),KinematicViscosity.FromCentistokes(   64  ), Density.FromKilogramsPerCubicMeter(  958.1   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.022   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1956  )),
                new CustomOil(Temperature.FromDegreesCelsius(   50  ),KinematicViscosity.FromCentistokes(   41.254  ), Density.FromKilogramsPerCubicMeter(  959.9   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.04    ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1949  )),
                new CustomOil(Temperature.FromDegreesCelsius(   60  ),KinematicViscosity.FromCentistokes(   28.077  ), Density.FromKilogramsPerCubicMeter(  960.9   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.063   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1936  )),
                new CustomOil(Temperature.FromDegreesCelsius(   70  ),KinematicViscosity.FromCentistokes(   20.001  ), Density.FromKilogramsPerCubicMeter(  961.5   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.088   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1917  )),
                new CustomOil(Temperature.FromDegreesCelsius(   80  ),KinematicViscosity.FromCentistokes(   14.805  ), Density.FromKilogramsPerCubicMeter(  961.9   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.113   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1892  )),
                new CustomOil(Temperature.FromDegreesCelsius(   90  ),KinematicViscosity.FromCentistokes(   11.321  ), Density.FromKilogramsPerCubicMeter(  962.1   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.135   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1861  )),
                new CustomOil(Temperature.FromDegreesCelsius(   100 ),KinematicViscosity.FromCentistokes(   8.9 ), Density.FromKilogramsPerCubicMeter(  962.3   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.153   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1824  )),
                new CustomOil(Temperature.FromDegreesCelsius(   110 ),KinematicViscosity.FromCentistokes(   7.165   ), Density.FromKilogramsPerCubicMeter(  962.5   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.165   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1781  )),
                new CustomOil(Temperature.FromDegreesCelsius(   120 ),KinematicViscosity.FromCentistokes(   5.887   ), Density.FromKilogramsPerCubicMeter(  962.6   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.171   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1732  )),
                new CustomOil(Temperature.FromDegreesCelsius(   130 ),KinematicViscosity.FromCentistokes(   4.923   ), Density.FromKilogramsPerCubicMeter(  962.6   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.171   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1677  )),
                new CustomOil(Temperature.FromDegreesCelsius(   140 ),KinematicViscosity.FromCentistokes(   4.181   ), Density.FromKilogramsPerCubicMeter(  962.7   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.164   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1616  )),
                new CustomOil(Temperature.FromDegreesCelsius(   150 ),KinematicViscosity.FromCentistokes(   3.599   ), Density.FromKilogramsPerCubicMeter(  962.8   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.152   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1549  )),
                new CustomOil(Temperature.FromDegreesCelsius(   160 ),KinematicViscosity.FromCentistokes(   3.135   ), Density.FromKilogramsPerCubicMeter(  962.8   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.135   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1476  )),
                new CustomOil(Temperature.FromDegreesCelsius(   170 ),KinematicViscosity.FromCentistokes(   2.76    ), Density.FromKilogramsPerCubicMeter(  962.8   ), SpecificEntropy.FromKilojoulesPerKilogramKelvin( 2.115   ), ThermalConductivity.FromWattsPerMeterKelvin( 0.1397  )),


            };



            return ListOfOil;




        }


    }
}
