
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using UnitsNet;
using EngineeringUnits;
using Serilog;
//using UnitsNet.Serialization.JsonNet;

namespace SharpFluids
{
    public partial class Fluid
    {
        [JsonIgnore]
        //public ILogger Log { get; set; }

        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        private Mass _mass;

        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        private MassFlow _massflow;

        /// <summary>
        /// Get the <see cref="UnitsNet.Temperature"/> of the <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Temperature Temperature { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.Pressure"/> of the <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Pressure Pressure { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.SpecificEnergy"/> of the <see cref="Fluid"/>. 
        /// <br> <see cref="UnitsNet.SpecificEnergy"/> is also called Enthalpy or H </br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEnergy Enthalpy { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.SpecificEntropy"/> of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "S".</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEntropy Entropy { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.Density"/> of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "RHO".</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Density Density { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.DynamicViscosity"/> of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public DynamicViscosity DynamicViscosity { get; private set; }

        /// <summary>
        /// Get the <see cref="UnitsNet.ThermalConductivity"/> of the <see cref="Fluid"/>. 
        /// <br> <see cref="UnitsNet.ThermalConductivity"/> is also just called Conductivity</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public ThermalConductivity Conductivity { get; private set; }

        /// <summary>
        /// Get the Cp of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "Heat capacity at constant pressure".</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEntropy Cp { get; private set; }

        /// <summary>
        /// Get the Cv of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "Heat capacity at constant volume".</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEntropy Cv { get; private set; }

        /// <summary>
        /// Get the <see cref="UnitsNet.Speed"/> of Sound of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Speed SoundSpeed { get; private set; }

        /// <summary>
        /// Get the Surface Tension of the <see cref="Fluid"/>. 
        /// <br>This is only valid when the <see cref="Fluid"/> is a mixture of gas and liquid!</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public ForcePerLength SurfaceTension { get; private set; }

        /// <summary>
        /// Get the <see cref="UnitsNet.MolarMass"/> of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public MolarMass MolarMass { get; private set; }

        /// <summary>
        /// Get the Internal Energy of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEnergy InternalEnergy { get; private set; }

        /// <summary>
        /// Get the Prandtl number of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "Pr".</br>
        /// <br>Note that Prandtl number is dimensionless and is therefore a <see langword="double" /></br>
        /// </summary> 
        [JsonProperty]
        public double Prandtl { get; private set; }

        /// <summary>
        /// Get the Compressibility of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as 'coefficient of compressibility' or 'isothermal compressibility'.</br>
        /// <br>Note that Compressibility is dimensionless and is therefore a <see langword="double" /></br>
        /// </summary>
        [JsonProperty]
        public double Compressibility { get; private set; }

        /// <summary>
        /// Get the Quality of the <see cref="Fluid"/>. 
        /// <br>It is the Mass fraction of vapour in fluid</br>
        /// <br>Quality = 0 => 0% vapour</br>
        /// <br>Quality = 1 => 100% vapour</br>
        /// <br>Engineers may refers to this as "X".</br>
        /// </summary> 
        [JsonProperty]
        public double Quality { get; private set; }



        /// <summary>
        /// Set the <see cref="UnitsNet.MassFlow"/> of the <see cref="Fluid"/>. 
        /// <br> The <see cref="UnitsNet.MassFlow"/> is not changed inside the library - It will also remain at the value the user has set it to </br>
        /// <br>You can choose between using <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/> when defining a fluid.</br>
        /// <br>Once the <see cref="UnitsNet.MassFlow"/> is set you cannot set the <see cref="UnitsNet.Mass"/> anymore.</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public MassFlow MassFlow
        {
            get { return _massflow; }

            set
            {

                if (value is object)
                {

                    if (Mass is null || Mass == Mass.Zero || value == MassFlow.Zero)
                    {
                        _massflow = value;
                    }
                    else
                    {
                        Log.Error($"SharpFluid -> MassFlow -> You can either set a fluids massflow or mass - not both!");
                        throw new System.InvalidOperationException("You can either set a fluids massflow or mass - not both!");
                    }
                }



            }
        }

        /// <summary>
        /// Set the <see cref="UnitsNet.Mass"/> of the <see cref="Fluid"/>. 
        /// <br> The <see cref="UnitsNet.Mass"/> is not changed inside the library - It will also remain at the value the user has set it to </br>
        /// <br>You can choose between using <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/> when defining a fluid.</br>
        /// <br>Once the <see cref="UnitsNet.Mass"/> is set you cannot set the <see cref="UnitsNet.MassFlow"/> anymore.</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Mass Mass
        {
            get { return _mass; }

            set
            {
                if (value is object)
                {

                    if (MassFlow == MassFlow.Zero || value == Mass.Zero)
                    {
                        _mass = value;
                    }
                    else
                    {
                        Log.Error($"SharpFluid -> Mass -> You can either set a fluids massflow or mass - not both!");
                        throw new System.InvalidOperationException("You can either set a fluids massflow or mass - not both!");
                    }
                }

            }
        }

        /// <summary>
        /// Get the Saturation temperature of the <see cref="Fluid"/>. 
        /// <br>Beware: If you are above the critical pressure of the <see cref="Fluid"/> this will just return the Saturation temperature AT the critical pressure!</br>
        /// </summary>
        [JsonIgnore]
        public Temperature Tsat
        {

            get
            {

                CheckBeforeUpdate();

                if (Pressure >= LimitPressureMin && Pressure <= CriticalPressure && LimitPressureMin != CriticalPressure) //P_Min != P_Crit is a quick fix - We should create a check to see if the object has beed created correctly!
                {
                    try
                    {



                        if (!(REF is null))
                        {
                            REF.update(input_pairs.PQ_INPUTS, Pressure.Pascals, 1);
                            FailState = false;
                            return REF.T();
                        }
                        else
                        {
                            Log.Error($"SharpFluid -> Tsat -> REF is null");
                            return Temperature;
                        }





                    }
                    catch (Exception e)
                    {
                        Log.Error($"SharpFluid -> Tsat -> Failed {e}", e);
                        return Temperature;
                    }
                }
                else if (Pressure > CriticalPressure)
                {
                    Log.Debug($"SharpFluid -> Tsat -> Pressure ({Pressure}) is above CriticalPressure {CriticalPressure}. CriticalPressure is returned instead!");
                    return CriticalTemperature;
                }
                else
                {
                    Log.Error($"SharpFluid -> Tsat -> Something unexpected went wrong!");
                    return Temperature;
                }



            }



        }


        /// <summary>
        /// Get the <see cref="UnitsNet.Volume"/> of the <see cref="Fluid"/>. 
        /// <br>This can ONLY be used when the <see cref="UnitsNet.Mass"/> of the <see cref="Fluid"/> is set</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Volume Volume
        {
            get
            {
                //Calculate the volume
                if (Density != Density.Zero)
                {
                    return Mass / Density;
                }
                else
                {
                    //This fires all the time because of serializing
                    //Log?.LogError("SharpFluid -> Volume -> Density is zero so we cant return you the Volume!");
                    return Volume.Zero;
                }

            }
        }

        /// <summary>
        /// Get the <see cref="UnitsNet.VolumeFlow"/> of the fluid. 
        /// <br>This can ONLY be used when the <see cref="UnitsNet.MassFlow"/> of the <see cref="Fluid"/> is set</br>
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public VolumeFlow VolumeFlow
        {
            get
            {
                //Calculate the volumeflow
                if (Density != Density.Zero)
                {
                    return MassFlow / Density;
                }
                else
                {
                    //This fires all the time because of serializing
                    //Log?.LogError("SharpFluid -> VolumeFlow -> Density is zero so we cant return you the VolumeFlow!");
                    return VolumeFlow.Zero;
                }

            }
        }




        /// <summary>
        /// This library's maximum <see cref="UnitsNet.Temperature"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Temperature LimitTemperatureMax { get; private set; }

        /// <summary>
        /// This library's minimum <see cref="UnitsNet.Temperature"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Temperature LimitTemperatureMin { get; private set; }

        /// <summary>
        /// <see cref="UnitsNet.Temperature"/> at the critical point for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Temperature CriticalTemperature { get; private set; }

        /// <summary>
        /// Enthalpy at the critical point for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEnergy CriticalEnthalpy { get; private set; }

        /// <summary>
        /// <see cref="UnitsNet.Pressure"/> at the critical point for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Pressure CriticalPressure { get; private set; }

        /// <summary>
        /// This library's minimum <see cref="UnitsNet.Pressure"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Pressure LimitPressureMin { get; private set; }

        /// <summary>
        /// This library's maximum <see cref="UnitsNet.Pressure"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        //[JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Pressure LimitPressureMax { get; private set; }

        /// <summary>
        /// This library's maximum fraction for the selected mixture of <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        public double FractionMax { get; private set; }

        /// <summary>
        /// This library's minimum fraction for the selected mixture of <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        public double FractionMin { get; private set; }




        /// <summary>
        /// Used to access the CoolProp DLL.
        /// </summary>
        private AbstractState REF;


        /// <summary>
        /// Get the fluid type of the <see cref="Fluid"/>
        /// <br>The full list can be seen on <see cref="FluidList"/></br><br></br>
        /// <br>Example: <see cref="FluidList.Water"/></br> or <see cref="FluidList.CO2"/>
        /// </summary>
        [JsonProperty]
        public MediaType Media { get; private set; }

        /// <summary>
        /// When an Update fails this it set to <see langword="true"/>
        /// </summary>
        [JsonProperty]
        public bool FailState { get; private set; }



    }
}