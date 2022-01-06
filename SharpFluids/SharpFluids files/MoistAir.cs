using EngineeringUnits;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFluids
{
    public class MoistAir: Fluid
    {


        [JsonProperty(PropertyName = "Twb", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public Temperature WetBulbTemperature { get; private set; }

        [JsonProperty(PropertyName = "Tdp", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public Temperature DewPointTemperature { get; private set; }

        [JsonProperty(PropertyName = "RelH", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public double RelativeHumidity { get; private set; }

        [JsonProperty(PropertyName = "HumR", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public double HumidityRatio { get; private set; }


        public MoistAir()
        {
            SetValuesToZero();
            SetLimitsToZero();
            //SetDefalutDisplayUnits();
        }


        public void UpdateAir(  Pressure pressure,
                                Temperature WetBulbTemperature = null,
                                Temperature DewPointTemperature = null,
                                SpecificEnergy Enthalpy = null,
                                double? RelativeHumidity = null,
                                SpecificEntropy Entropy  = null,
                                Temperature DryBulbTemperature = null,
                                Density Density = null,
                                double? HumidityRatio = null)
        {


            string input1 = "P";
            double input1Value = pressure.SI;


            string input2 = "";
            double input2Value = 0;


            string input3 = "";
            double input3Value = 0;


            if (WetBulbTemperature is object)
            {
                if (input1 == "")
                {
                    input1 = "B";
                    input1Value = WetBulbTemperature.SI;
                }
                else if (input2 == "")
                {
                    input2 = "B";
                    input2Value = WetBulbTemperature.SI;

                }
                else if(input3 == "")
                {
                    input3 = "B";
                    input3Value = WetBulbTemperature.SI;
                }
            }


            if (DewPointTemperature is object)
            {
                if (input1 == "")
                {
                    input1 = "D";
                    input1Value = DewPointTemperature.SI;
                }
                else if (input2 == "")
                {
                    input2 = "D";
                    input2Value = DewPointTemperature.SI;

                }
                else if (input3 == "")
                {
                    input3 = "D";
                    input3Value = DewPointTemperature.SI;
                }
            }



            if (Enthalpy is object)
            {
                if (input1 == "")
                {
                    input1 = "Hha";
                    input1Value = Enthalpy.SI;
                }
                else if (input2 == "")
                {
                    input2 = "Hha";
                    input2Value = Enthalpy.SI;

                }
                else if (input3 == "")
                {
                    input3 = "Hha";
                    input3Value = Enthalpy.SI;
                }
            }

            if (RelativeHumidity is object)
            {
                if (input1 == "")
                {
                    input1 = "R";
                    input1Value = (double)RelativeHumidity;
                }
                else if (input2 == "")
                {
                    input2 = "R";
                    input2Value = (double)RelativeHumidity;

                }
                else if (input3 == "")
                {
                    input3 = "R";
                    input3Value = (double)RelativeHumidity;
                }
            }



            if (Entropy is object)
            {
                if (input1 == "")
                {
                    input1 = "Sha";
                    input1Value = Entropy.SI;
                }
                else if (input2 == "")
                {
                    input2 = "Sha";
                    input2Value = Entropy.SI;

                }
                else if (input3 == "")
                {
                    input3 = "Sha";
                    input3Value = Entropy.SI;
                }
            }

            if (DryBulbTemperature is object)
            {
                if (input1 == "")
                {
                    input1 = "T";
                    input1Value = DryBulbTemperature.SI;
                }
                else if (input2 == "")
                {
                    input2 = "T";
                    input2Value = DryBulbTemperature.SI;

                }
                else if (input3 == "")
                {
                    input3 = "T";
                    input3Value = DryBulbTemperature.SI;
                }
            }

            if (Density is object)
            {
                if (input1 == "")
                {
                    input1 = "Vha";
                    input1Value = Density.SI;
                }
                else if (input2 == "")
                {
                    input2 = "Vha";
                    input2Value = Density.SI;

                }
                else if (input3 == "")
                {
                    input3 = "Vha";
                    input3Value = Density.SI;
                }
            }

            if (HumidityRatio is object)
            {
                if (input1 == "")
                {
                    input1 = "W";
                    input1Value = (double)HumidityRatio;
                }
                else if (input2 == "")
                {
                    input2 = "W";
                    input2Value = (double)HumidityRatio;

                }
                else if (input3 == "")
                {
                    input3 = "W";
                    input3Value = (double)HumidityRatio;
                }
            }





            this.WetBulbTemperature = Temperature.FromSI(AbstractState.updateAir("Twb", input1, input1Value, input2, input2Value, input3, input3Value));

            this.Cp = SpecificEntropy.FromSI(AbstractState.updateAir("cp_ha", input1, input1Value, input2, input2Value, input3, input3Value));

            this.Cv = SpecificEntropy.FromSI(AbstractState.updateAir("cv_ha", input1, input1Value, input2, input2Value, input3, input3Value));

            this.DewPointTemperature = Temperature.FromSI(AbstractState.updateAir("Tdp", input1, input1Value, input2, input2Value, input3, input3Value));

            this.Enthalpy = SpecificEnergy.FromSI(AbstractState.updateAir("Hha", input1, input1Value, input2, input2Value, input3, input3Value));

            this.Conductivity = ThermalConductivity.FromSI(AbstractState.updateAir("Conductivity", input1, input1Value, input2, input2Value, input3, input3Value));

            this.DynamicViscosity = DynamicViscosity.FromSI(AbstractState.updateAir("Visc", input1, input1Value, input2, input2Value, input3, input3Value));

            this.Pressure = Pressure.FromSI(AbstractState.updateAir("P", input1, input1Value, input2, input2Value, input3, input3Value));

            this.RelativeHumidity = AbstractState.updateAir("R", input1, input1Value, input2, input2Value, input3, input3Value);

            this.Entropy = SpecificEntropy.FromSI(AbstractState.updateAir("Sha", input1, input1Value, input2, input2Value, input3, input3Value));

            this.Temperature = Temperature.FromSI(AbstractState.updateAir("T", input1, input1Value, input2, input2Value, input3, input3Value));

            this.Density = Density.FromSI(AbstractState.updateAir("Vha", input1, input1Value, input2, input2Value, input3, input3Value));
            
            this.HumidityRatio = AbstractState.updateAir("W", input1, input1Value, input2, input2Value, input3, input3Value);

            this.Compressibility = AbstractState.updateAir("Z", input1, input1Value, input2, input2Value, input3, input3Value);


            //Vi mangler Humidity Ratio


        }


       
        

    }
}
