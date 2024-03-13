using EngineeringUnits;
using System.Collections.Generic;

namespace SharpFluids
{
    public static class Number13
    {
        public static List<CustomOil> GetList()
        {

            var ListOfOil = new List<CustomOil>
            {

                new CustomOil(Temperature.FromDegreeCelsius(   -30 ),KinematicViscosity.FromCentistokes(   23903.374   ), Density.FromKilogramPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.381   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1837  )),
                new CustomOil(Temperature.FromDegreeCelsius(   -20 ),KinematicViscosity.FromCentistokes(   6367.512    ), Density.FromKilogramPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.242   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1872  )),
                new CustomOil(Temperature.FromDegreeCelsius(   -10 ),KinematicViscosity.FromCentistokes(   2103.033    ), Density.FromKilogramPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.142   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1901  )),
                new CustomOil(Temperature.FromDegreeCelsius(   0   ),KinematicViscosity.FromCentistokes(   826.225 ), Density.FromKilogramPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.074   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1924  )),
                new CustomOil(Temperature.FromDegreeCelsius(   10  ),KinematicViscosity.FromCentistokes(   373.795 ), Density.FromKilogramPerCubicMeter(  882.6   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.033   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1941  )),
                new CustomOil(Temperature.FromDegreeCelsius(   20  ),KinematicViscosity.FromCentistokes(   189.802 ), Density.FromKilogramPerCubicMeter(  943 ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.014   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1952  )),
                new CustomOil(Temperature.FromDegreeCelsius(   30  ),KinematicViscosity.FromCentistokes(   105.975 ), Density.FromKilogramPerCubicMeter(  954.2   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.012   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1957  )),
                new CustomOil(Temperature.FromDegreeCelsius(   40  ),KinematicViscosity.FromCentistokes(   64  ), Density.FromKilogramPerCubicMeter(  958.1   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.022   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1956  )),
                new CustomOil(Temperature.FromDegreeCelsius(   50  ),KinematicViscosity.FromCentistokes(   41.254  ), Density.FromKilogramPerCubicMeter(  959.9   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.04    ), ThermalConductivity.FromWattPerMeterKelvin( 0.1949  )),
                new CustomOil(Temperature.FromDegreeCelsius(   60  ),KinematicViscosity.FromCentistokes(   28.077  ), Density.FromKilogramPerCubicMeter(  960.9   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.063   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1936  )),
                new CustomOil(Temperature.FromDegreeCelsius(   70  ),KinematicViscosity.FromCentistokes(   20.001  ), Density.FromKilogramPerCubicMeter(  961.5   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.088   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1917  )),
                new CustomOil(Temperature.FromDegreeCelsius(   80  ),KinematicViscosity.FromCentistokes(   14.805  ), Density.FromKilogramPerCubicMeter(  961.9   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.113   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1892  )),
                new CustomOil(Temperature.FromDegreeCelsius(   90  ),KinematicViscosity.FromCentistokes(   11.321  ), Density.FromKilogramPerCubicMeter(  962.1   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.135   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1861  )),
                new CustomOil(Temperature.FromDegreeCelsius(   100 ),KinematicViscosity.FromCentistokes(   8.9 ), Density.FromKilogramPerCubicMeter(  962.3   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.153   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1824  )),
                new CustomOil(Temperature.FromDegreeCelsius(   110 ),KinematicViscosity.FromCentistokes(   7.165   ), Density.FromKilogramPerCubicMeter(  962.5   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.165   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1781  )),
                new CustomOil(Temperature.FromDegreeCelsius(   120 ),KinematicViscosity.FromCentistokes(   5.887   ), Density.FromKilogramPerCubicMeter(  962.6   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.171   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1732  )),
                new CustomOil(Temperature.FromDegreeCelsius(   130 ),KinematicViscosity.FromCentistokes(   4.923   ), Density.FromKilogramPerCubicMeter(  962.6   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.171   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1677  )),
                new CustomOil(Temperature.FromDegreeCelsius(   140 ),KinematicViscosity.FromCentistokes(   4.181   ), Density.FromKilogramPerCubicMeter(  962.7   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.164   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1616  )),
                new CustomOil(Temperature.FromDegreeCelsius(   150 ),KinematicViscosity.FromCentistokes(   3.599   ), Density.FromKilogramPerCubicMeter(  962.8   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.152   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1549  )),
                new CustomOil(Temperature.FromDegreeCelsius(   160 ),KinematicViscosity.FromCentistokes(   3.135   ), Density.FromKilogramPerCubicMeter(  962.8   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.135   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1476  )),
                new CustomOil(Temperature.FromDegreeCelsius(   170 ),KinematicViscosity.FromCentistokes(   2.76    ), Density.FromKilogramPerCubicMeter(  962.8   ), SpecificEntropy.FromKilojoulePerKilogramKelvin( 2.115   ), ThermalConductivity.FromWattPerMeterKelvin( 0.1397  )),

            };

            return ListOfOil;

        }
    }
}
