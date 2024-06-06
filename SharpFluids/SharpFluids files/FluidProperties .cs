
//using EngineeringUnits;
using EngineeringUnits;
using Newtonsoft.Json;
using Serilog;
using System;
//using EngineeringUnits.Serialization.JsonNet;

namespace SharpFluids
{
    public partial class Fluid
    {


        public double Alpha0 { get; set; }
        public double AlphaR { get; set; }

        public double AlphaR_dDelta { get; set; }
        public double AlphaR_dTau { get; set; }
        public double Alpha0_dTau { get; set; }

        [JsonProperty(PropertyName = "_m", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        private Mass? _mass;

        [JsonProperty(PropertyName = "_mf", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        private MassFlow? _massflow;

        /// <summary>
        /// Get the <see cref="EngineeringUnits.Temperature"/> of the <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "T", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Temperature? Temperature { get; set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.Temperature"/> of the <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "Tfrez", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Temperature? T_freeze { get; set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.Pressure"/> of the <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "P", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Pressure? Pressure { get; set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.SpecificEnergy"/> of the <see cref="Fluid"/>. 
        /// <br> <see cref="EngineeringUnits.SpecificEnergy"/> is also called Enthalpy or H </br>
        /// </summary>
        [JsonProperty(PropertyName = "E", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public SpecificEnergy? Enthalpy { get; set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.SpecificEntropy"/> of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "S".</br>
        /// </summary>
        [JsonProperty(PropertyName = "S", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public SpecificEntropy? Entropy { get; set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.Density"/> of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "RHO".</br>
        /// </summary>
        [JsonProperty(PropertyName = "D", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Density? Density { get; protected set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.DynamicViscosity"/> of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty(PropertyName = "DV", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public DynamicViscosity? DynamicViscosity { get; protected set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.ThermalConductivity"/> of the <see cref="Fluid"/>. 
        /// <br> <see cref="EngineeringUnits.ThermalConductivity"/> is also just called Conductivity</br>
        /// </summary>
        [JsonProperty(PropertyName = "C", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public ThermalConductivity? Conductivity { get; protected set; }

        /// <summary>
        /// Get the Cp of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "Heat capacity at constant pressure".</br>
        /// </summary>
        [JsonProperty(PropertyName = "Cp", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public SpecificEntropy? Cp { get; protected set; }

        /// <summary>
        /// Get the Cv of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "Heat capacity at constant volume".</br>
        /// </summary>
        [JsonProperty(PropertyName = "Cv", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public SpecificEntropy? Cv { get; protected set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.Speed"/> of Sound of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty(PropertyName = "SS", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Speed? SoundSpeed { get; protected set; }

        /// <summary>
        /// Get the Surface Tension of the <see cref="Fluid"/>. 
        /// <br>This is only valid when the <see cref="Fluid"/> is a mixture of gas and liquid!</br>
        /// </summary>
        [JsonProperty(PropertyName = "ST", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public ForcePerLength? SurfaceTension { get; protected set; }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.MolarMass"/> of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty(PropertyName = "MM", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public MolarMass? MolarMass { get; protected set; }

        /// <summary>
        /// Get the Internal Energy of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty(PropertyName = "IE", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public SpecificEnergy? InternalEnergy { get; protected set; }

        /// <summary>
        /// Get the Prandtl number of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "Pr".</br>
        /// <br>Note that Prandtl number is dimensionless and is therefore a <see langword="double" /></br>
        /// </summary> 
        [JsonProperty(PropertyName = "Pl", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public double Prandtl { get; protected set; }

        /// <summary>
        /// Get the Compressibility of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as 'coefficient of compressibility' or 'isothermal compressibility'.</br>
        /// <br>Note that Compressibility is dimensionless and is therefore a <see langword="double" /></br>
        /// </summary>
        [JsonProperty(PropertyName = "Co", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public double Compressibility { get; protected set; }

        /// <summary>
        /// Get the Quality of the <see cref="Fluid"/>. 
        /// <br>It is the Mass fraction of vapour in fluid</br>
        /// <br>Quality = 0 => 0% vapour</br>
        /// <br>Quality = 1 => 100% vapour</br>
        /// <br>Engineers may refers to this as "X".</br>
        /// </summary> 
        [JsonProperty(PropertyName = "Q", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public double Quality { get; protected set; }

        /// <summary>
        /// Set the <see cref="EngineeringUnits.MassFlow"/> of the <see cref="Fluid"/>. 
        /// <br> The <see cref="EngineeringUnits.MassFlow"/> is not changed inside the library - It will also remain at the value the user has set it to </br>
        /// <br>You can choose between using <see cref="EngineeringUnits.MassFlow"/> or <see cref="EngineeringUnits.Mass"/> when defining a fluid.</br>
        /// <br>Once the <see cref="EngineeringUnits.MassFlow"/> is set you cannot set the <see cref="EngineeringUnits.Mass"/> anymore.</br>
        /// </summary>
        [JsonProperty(PropertyName = "MF", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public MassFlow? MassFlow
        {
            get => _massflow;

            set
            {

                if (value is not null)
                {

                    if (Mass is null)
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
        /// Set the <see cref="EngineeringUnits.Mass"/> of the <see cref="Fluid"/>. 
        /// <br> The <see cref="EngineeringUnits.Mass"/> is not changed inside the library - It will also remain at the value the user has set it to </br>
        /// <br>You can choose between using <see cref="EngineeringUnits.MassFlow"/> or <see cref="EngineeringUnits.Mass"/> when defining a fluid.</br>
        /// <br>Once the <see cref="EngineeringUnits.Mass"/> is set you cannot set the <see cref="EngineeringUnits.MassFlow"/> anymore.</br>
        /// </summary>
        //[JsonProperty(PropertyName = "M", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
        //[JsonProperty]
        public Mass? Mass
        {
            get => _mass;

            set
            {
                if (value is not null)
                {

                    if (MassFlow is null)
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

        [JsonIgnore]
        private Temperature? tsat_Cache = null;

        /// <summary>
        /// Get the Saturation temperature of the <see cref="Fluid"/>. 
        /// <br>Beware: If you are above the critical pressure of the <see cref="Fluid"/> this will just return the Saturation temperature AT the critical pressure!</br>
        /// </summary>
        [JsonIgnore]
        public Temperature? Tsat
        {

            get
            {
                if (Pressure is null)
                    return null;

                if (tsat_Cache is not null)
                    return tsat_Cache;

                CheckBeforeUpdate();

                if (Pressure >= LimitPressureMin && Pressure <= CriticalPressure && LimitPressureMin != CriticalPressure) //P_Min != P_Crit is a quick fix - We should create a check to see if the object has beed created correctly!
                {
                    try
                    {

                        if (REF is not null)
                        {
                            REF.update(input_pairs.PQ_INPUTS, Pressure.Pascal, 1);
                            FailState = false;
                            tsat_Cache = REF.T();

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
                    finally
                    {
                        if (Environment.Is64BitProcess)
                            CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                        else
                            CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    }
                }
                else if (Pressure > CriticalPressure)
                {
                    Log.Debug($"SharpFluid -> Tsat -> Pressure ({Pressure}) is above CriticalPressure {CriticalPressure}. CriticalPressure is returned instead!");
                    FailState = false;
                    return CriticalTemperature;
                }
                else
                {
                    //Log.Error($"SharpFluid -> Tsat -> Something unexpected went wrong!");
                    return Temperature;
                }
            }
        }

        [JsonIgnore]
        public MassFlow? GasMassFlow
        {

            get
            {

                if (Phase is Phases.Twophase)
                {
                    return MassFlow * Quality;
                }

                return MassFlow;
            }
        }

        [JsonIgnore]
        public MassFlow? LiquidMassFlow
        {

            get
            {
                if (Phase is Phases.Twophase)
                {
                    return MassFlow * (1 - Quality);
                }

                return MassFlow;
            }
        }

        [JsonIgnore]
        public Density? GasDensity
        {

            get
            {

                CheckBeforeUpdate();

                if (Phase is Phases.Twophase)
                {

                    try
                    {
                        REF.update(input_pairs.PQ_INPUTS, Pressure.Pascal, 1);
                        FailState = false;
                        return REF.rhomass();
                    }
                    catch (Exception e)
                    {
                        Log.Error($"SharpFluid -> GasDensity -> {e}");
                        return Density;
                    }
                }

                return Density;
            }
        }

        [JsonIgnore]
        public Density? LiquidDensity
        {

            get
            {

                CheckBeforeUpdate();

                if (Phase is Phases.Twophase)
                {

                    try
                    {
                        REF.update(input_pairs.PQ_INPUTS, Pressure.Pascal, 0);
                        FailState = false;
                        return REF.rhomass();
                    }
                    catch (Exception e)
                    {
                        Log.Error($"SharpFluid -> GasDensity -> {e}");
                        return Density;
                    }
                }

                return Density;
            }
        }

        /// <summary>
        /// Get the <see cref="EngineeringUnits.Volume"/> of the <see cref="Fluid"/>. 
        /// <br>This can ONLY be used when the <see cref="EngineeringUnits.Mass"/> of the <see cref="Fluid"/> is set</br>
        /// </summary>
        //[JsonProperty(PropertyName = "V", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonIgnore]
        public Volume? Volume
        {
            get
            {
                //Calculate the volume
                if (Density is not null && Density != Density.Zero)
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
        /// Get the <see cref="EngineeringUnits.VolumeFlow"/> of the fluid. 
        /// <br>This can ONLY be used when the <see cref="EngineeringUnits.MassFlow"/> of the <see cref="Fluid"/> is set</br>
        /// </summary>
        //[JsonProperty(PropertyName = "VF", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonIgnore]
        public VolumeFlow? VolumeFlow
        {
            get
            {
                //Calculate the volumeflow
                if (Density is not null && Density != Density.Zero)
                {
                    return MassFlow / Density;
                }
                else
                {
                    //This fires all the time because of serializing
                    //Log?.LogError("SharpFluid -> VolumeFlow -> Density is zero so we cant return you the VolumeFlow!");
                    return null;
                }
            }
        }

        /// <summary>
        /// This library's maximum <see cref="EngineeringUnits.Temperature"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "Tma", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Temperature? LimitTemperatureMax { get; protected set; }

        /// <summary>
        /// This library's minimum <see cref="EngineeringUnits.Temperature"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "Tmi", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Temperature? LimitTemperatureMin { get; protected set; }

        /// <summary>
        /// <see cref="EngineeringUnits.Temperature"/> at the critical point for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "CT", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Temperature? CriticalTemperature { get; protected set; }

        ///// <summary>
        ///// Enthalpy at the critical point for the selected <see cref="Fluid"/>.
        ///// </summary>
        //[JsonProperty(PropertyName = "CE", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        ////[JsonProperty]
        //public SpecificEnergy CriticalEnthalpy { get; protected set; }

        /// <summary>
        /// <see cref="EngineeringUnits.Pressure"/> at the critical point for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "CP", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Pressure? CriticalPressure { get; protected set; }

        /// <summary>
        /// This library's minimum <see cref="EngineeringUnits.Pressure"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "Pmi", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Pressure? LimitPressureMin { get; protected set; }

        /// <summary>
        /// This library's maximum <see cref="EngineeringUnits.Pressure"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "Pma", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public Pressure? LimitPressureMax { get; protected set; }

        /// <summary>
        /// This library's maximum fraction for the selected mixture of <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "Fma", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public double FractionMax { get; protected set; }

        /// <summary>
        /// This library's minimum fraction for the selected mixture of <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty(PropertyName = "Fmi", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public double FractionMin { get; protected set; }

        /// <summary>
        /// This library's fluid phase. For available phases see: <see cref="Phase"/>
        /// </summary>
        [JsonProperty(PropertyName = "Phase", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public Phases? Phase { get; protected set; }

        /// <summary>
        /// Used to access the CoolProp DLL.
        /// </summary>
        protected AbstractState REF;

        /// <summary>
        /// Get the fluid type of the <see cref="Fluid"/>
        /// <br>The full list can be seen on <see cref="FluidList"/></br><br></br>
        /// <br>Example: <see cref="FluidList.Water"/></br> or <see cref="FluidList.CO2"/>
        /// </summary>
        [JsonProperty(PropertyName = "Me", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public MediaType? Media { get; private set; }

        /// <summary>
        /// When an Update fails this it set to <see langword="true"/>
        /// </summary>
        [JsonProperty(PropertyName = "FS", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public bool FailState { get; set; }

        [JsonProperty(PropertyName = "CM", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonProperty]
        public bool CacheMode { get; set; } = false;

        private Pressure? cache_pressure;
        private Temperature? cache_temperature;
        private SpecificEnergy? cache_enthalpy;
        private SpecificEntropy? cache_entropy;
        private double? cache_quality;

    }
}