//using EngineeringUnits;
using EngineeringUnits;
//using EngineeringUnits.Units;
//using EngineeringUnits.Serialization.JsonNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
//using Serilog.Context;

namespace SharpFluids
{
    /// <summary>
    /// A <see cref="Fluid"/> object carries the condition and properties of a real fluid.         
    /// <br> To change the <see cref="Fluid"/> condition use a Update function:</br>
    /// <br> <see cref="UpdateDS"/></br>
    /// <br> <see cref="UpdatePT"/></br>
    /// <br> ..more to select</br>
    /// <br> Example:</br>
    /// <br> </br>
    /// <br><c> <see cref="Fluid"/> Water = <see langword="new" /> <see cref="Fluid"/>(<see cref="FluidList"/>.Water); </c></br>
    /// <br><c>Water.UpdatePT(<see cref="EngineeringUnits.Pressure"/>.FromBar(1.013), <see cref="EngineeringUnits.Temperature"/>.FromDegreeCelsius(13));</c></br>
    /// <br><c><see langword="Debug" />.Print("Density of water is: " + Water.Density);</c></br>
    /// </summary>

    public partial class Fluid
    {

        /// <summary>
        /// Create an empty <see cref="Fluid"/> that does not have a fluid type!
        /// <br>You would normally use this:</br><br></br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList"/>.Water);</c></br>
        /// </summary>
        public Fluid()
        {
            //REF = new AbstractState();
        }

        /// <summary>
        /// Create a <see cref="Fluid"/> with a fluid type!
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList"/>.Water);</c></br>
        /// </summary>
        public Fluid(MediaType Media) : this()
        {
            SetNewMedia(Media);
        }

        /// <summary>
        /// Create an new <see cref="Fluid"/> with a fluid type!
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList"/>.Water);</c></br>
        /// </summary>
        public Fluid(FluidList Type) : this(FluidListToMediaType(Type))
        {
            //This is just converting from One media type to another
        }

        /// <summary>
        /// Get Saturation Temperature from a given Pressure using the type of <see cref="Fluid"/>
        /// </summary> 
        public Temperature? GetSatTemperature(Pressure? FromThisPressure)
        {
            if (FromThisPressure is null)            
                return null;            


            if (FromThisPressure < LimitPressureMin)
            {
                Log.Debug($"SharpFluid -> GetSatTemperature -> {FromThisPressure} cant be bolow {LimitPressureMin}");
                return null;
            }

            if (FromThisPressure > CriticalPressure)
            {
                Log.Debug($"SharpFluid -> GetSatTemperature -> Pressure ({FromThisPressure}) is above CriticalPressure {CriticalPressure}. CriticalPressure is returned instead!");
                return CriticalTemperature;
            }
            else
            {
                REF.update(input_pairs.PQ_INPUTS, FromThisPressure.Pascal, 1);
                return REF.T();
            }
        }

        /// <summary>
        /// Get Saturation Pressure from a given Temperature using the type of <see cref="Fluid"/>
        /// </summary> 
        public Pressure? GetSatPressure(Temperature? FromThisTemperature)
        {
            if (FromThisTemperature is null)
                return null;


            if (FromThisTemperature < LimitTemperatureMin)
            {
                Log.Debug($"SharpFluid -> GetSatPressure -> {FromThisTemperature} cant be bolow {LimitTemperatureMin}");
                return null;
            }

            if (FromThisTemperature >= CriticalTemperature)
            {
                Log.Debug($"SharpFluid -> GetSatPressure -> Temperature ({FromThisTemperature}) is above CriticalTemperature {CriticalTemperature}. CriticalTemperature is returned instead!");
                return CriticalPressure;
            }
            else
            {
                REF.update(input_pairs.QT_INPUTS, 1, FromThisTemperature.Kelvins);
                return REF.p();
            }
        }

        /// <summary>
        /// Updates the constants of the <see cref="Fluid"/>
        /// <br><see cref="Fluid"/> constants are Limits and Criticalpoint values</br>
        /// </summary>   
        protected virtual void UpdateFluidConstants()
        {
            if (Media is null)
                return;

            FailState = true;

            //Removed cache values
            this.tsat_Cache = null;
            cache_pressure = null;
            cache_enthalpy = null;
            cache_temperature = null;
            cache_quality = null;

            if (Media.BackendType == "CustomFluid")
                return;

            try
            {
                //Setting the constant values up
                LimitTemperatureMax = REF.Tmax();
                LimitTemperatureMin = REF.Tmin();

                if (Media.Mix is not MixType.None)
                {
                    //Fraction
                    FractionMin = REF.keyed_output(Parameters.ifraction_min);
                    FractionMax = REF.keyed_output(Parameters.ifraction_max);
                }

                if (REF.backend_name() is "HelmholtzEOSBackend" && Media.Mix is MixType.None)
                {
                    CriticalTemperature = REF.TCritical();
                    CriticalPressure = REF.p_critical();
                    LimitPressureMin = REF.p_triple();
                    LimitPressureMax = REF.pmax();
                }
            }
            catch (Exception e)
            {
                Log.Error($"SharpFluid -> UpdateFluidConstants -> {e}");
            }
        }

        /// <summary>
        /// Updates the values of the <see cref="Fluid"/> after an update
        /// </summary>           
        protected virtual void UpdateValues()
        {

            //Removed cache values
            tsat_Cache = null;
            CacheMode = false;

            try
            {
                Phase = (Phases)REF.phase();

                if (REF.backend_name() is not "IncompressibleBackend")
                {
                    if (Media.Mix is MixType.None)
                    {
                        SoundSpeed = REF.speed_sound();
                        MolarMass = REF.molar_mass();
                        Compressibility = REF.compressibility_factor();

                    }

                    Quality = REF.Q();
                    cache_quality = Quality;

                    if (Phase is Phases.Twophase)
                        SurfaceTension = REF.surface_tension();

                }
                else
                {
                    T_freeze = Temperature.FromKelvins(REF.keyed_output(Parameters.iT_freeze));
                }

                Enthalpy = REF.hmass();
                cache_enthalpy = Enthalpy;

                Temperature = REF.T();
                cache_temperature = Temperature;


                Pressure = REF.p();

                Tau = REF.Tau();
                Delta = REF.Delta();

                fugacity_coefficient = REF.fugacity_coefficient();
                fugacity= REF.fugacity();

                umolar = REF.umolar();
                rhomolar = REF.rhomolar();
                Alpha0 = REF.Alpha0();
                AlphaR = REF.AlphaR();
                AlphaR_dDelta = REF.AlphaR_dDelta();
                AlphaR_dTau = REF.AlphaR_dTau();
                Alpha0_dTau = REF.Alpha0_dTau();
                cache_pressure = Pressure;

                Entropy = REF.smass();
                cache_entropy = Entropy;

                Density = REF.rhomass();
                Cp = REF.cpmass();
                Cv = REF.cvmass();
                DynamicViscosity = REF.viscosity();
                Prandtl = REF.Prandtl();

                InternalEnergy = REF.umass();
                Conductivity = REF.conductivity();
                FailState = false;

                if (Environment.Is64BitProcess)
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                else
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();

            }
            catch (Exception e)
            {
                Log.Error($"SharpFluid -> UpdateValues -> {e}");
                throw new Exception("UpdateValues", e);
            }
        }

        ///// <summary>
        ///// Set all values of <see cref="Fluid"/> to Zero
        ///// </summary>   
        //public virtual void SetValuesToZero()
        //{
        //    Enthalpy = 0;
        //    Temperature = 0;
        //    Pressure = new Pressure(0, PressureUnit.SI,PressureReference.Absolute);
        //    Entropy = 0;
        //    Quality = 0;
        //    Density = 0;
        //    Cp = 0;
        //    Cv = 0;
        //    MassFlow = 0;
        //    Mass = 0;
        //    Prandtl = 0;
        //    SurfaceTension = 0;
        //    SoundSpeed = 0;
        //    FailState = true;
        //    DynamicViscosity = 0;
        //    Conductivity = 0;
        //    MolarMass = 0;
        //    Compressibility = 0;
        //    InternalEnergy = 0;

        //    //Removed cache values
        //    tsat_Cache = null;
        //    cache_pressure = null;
        //    cache_enthalpy = null;
        //    cache_temperature = null;
        //    cache_entropy = null;
        //    cache_quality = null;
        //}

        public virtual void SetValuesToNull()
        {
            Enthalpy = null;
            Temperature = null;
            Pressure = null;
            Entropy = null;
            Quality = 0;
            Density = null;
            Cp = null;
            Cv = null;
            MassFlow = null;
            Mass = null;
            Prandtl = 0;
            SurfaceTension = null;
            SoundSpeed = null;
            FailState = true;
            DynamicViscosity = null;
            Conductivity = null;
            MolarMass = null;
            Compressibility = 0;
            InternalEnergy = null;

            //Removed cache values
            tsat_Cache = null;
            cache_pressure = null;
            cache_enthalpy = null;
            cache_temperature = null;
            cache_entropy = null;
            cache_quality = null;
        }

        //public virtual void SetLimitsToZero()
        //{
        //    LimitTemperatureMax = 0;
        //    LimitTemperatureMin = 0;
        //    CriticalTemperature = 0;
        //    CriticalPressure = new Pressure(0, PressureUnit.SI, PressureReference.Absolute); 
        //    LimitPressureMin = new Pressure(0, PressureUnit.SI, PressureReference.Absolute); 
        //    LimitPressureMax = new Pressure(0, PressureUnit.SI, PressureReference.Absolute); 
        //    //CriticalEnthalpy = 0;
        //}

        /// <summary>
        /// Copy all the values from <paramref name="other"/> to this <see cref="Fluid"/>
        /// </summary>  
        /// <param name="other"><see cref="Fluid"/> to be copied from</param>
        public void Copy(Fluid other)
        {
            //Copying Refrigerant type
            CopyType(other);

            Enthalpy = other.Enthalpy;
            MassFlow = other.MassFlow;
            Mass = other.Mass;
            Pressure = other.Pressure;
            Temperature = other.Temperature;
            Entropy = other.Entropy;
            Quality = other.Quality;
            Density = other.Density;
            Cp = other.Cp;
            Cv = other.Cv;
            CriticalPressure = other.CriticalPressure;
            DynamicViscosity = other.DynamicViscosity;
            Conductivity = other.Conductivity;
            Prandtl = other.Prandtl;
            SoundSpeed = other.SoundSpeed;
            SurfaceTension = other.SurfaceTension;
            FailState = other.FailState;
            MolarMass = other.MolarMass;
            Compressibility = other.Compressibility;
            InternalEnergy = other.InternalEnergy;
            Phase = other.Phase;
            T_freeze = other.T_freeze;

            //Removed cache values
            this.tsat_Cache = other.tsat_Cache;
            this.cache_pressure = other.cache_pressure;
            this.cache_enthalpy = other.cache_enthalpy;
            this.cache_entropy = other.cache_entropy;
            this.cache_temperature = other.cache_temperature;
            this.cache_quality = other.cache_quality;

        }

        /// <summary>
        /// Copy just the type of fluid from <paramref name="other"/> to this <see cref="Fluid"/>
        /// </summary>  
        /// <param name="other"><see cref="Fluid"/> to be copied from</param>
        public void CopyType(Fluid other)
        {
            //Null check
            if (other.Media is not null)
            {
                //Actually changing media
                if (Media != other.Media)
                {
                    //Since we are changing media when the old values doesn't make sense to keep
                    SetValuesToNull();

                    //Set new media
                    SetNewMedia(other.Media);
                }
            }
            else
            {
                Log.Error($"SharpFluid -> CopyType -> 'other.Media is null'");
            }
        }
        /**
                /// <summary>
                /// Mixing <paramref name="other"/> into this <see cref="Fluid"/>
                /// <br>This makes a simple mixing based on <see cref="EngineeringUnits.MassFlow"/> or <see cref="EngineeringUnits.Mass"/> </br>
                /// <br>Both <see cref="Fluid"/>s should use either <see cref="EngineeringUnits.MassFlow"/> or <see cref="EngineeringUnits.Mass"/>!</br>
                /// </summary> 
                /// <param name="other"><see cref="Fluid"/> to be copied from</param>
                public void AddTo(Fluid other)
                {
                    //TODO Should also work if Mass is selected

                    //This makes a simple mixing based on the massflow (weigted)
                    //After the mixing an Update should be run


                    if (this.Enthalpy.IsZero() ||
                        this.Pressure.IsZero() || 
                        this.Entropy.IsZero() || 
                        this.Temperature.IsZero() || 
                        this.MassFlow.IsZero())
                    {
                        this.Copy(other);
                    }
                    else if (other.Enthalpy.IsZero() || 
                             other.Pressure.IsZero() || 
                             other.Entropy.IsZero() || 
                             other.Temperature.IsZero() || 
                             other.MassFlow.IsZero())
                    {
                        //Do nothing
                        Log.Debug($"SharpFluid -> AddTo -> {other.Enthalpy} or {other.Pressure} or {other.Entropy} or {other.Temperature} or {other.MassFlow} is zero and nothing is done!");
                    }
                    else
                    {

                        if ((other.MassFlow + this.MassFlow).IsNotZero())
                        {
                            //Calculating the average H weighted on the massflow
                            this.Enthalpy = other.Enthalpy * (other.MassFlow / (other.MassFlow + this.MassFlow)) + this.Enthalpy * (this.MassFlow / (other.MassFlow + this.MassFlow));

                            //Calculating the average P weighted on the massflow
                            this.Pressure = other.Pressure * (other.MassFlow / (other.MassFlow + this.MassFlow)) + this.Pressure * (this.MassFlow / (other.MassFlow + this.MassFlow));


                            //Calculating the average S weighted on the massflow
                            this.Entropy = other.Entropy * (other.MassFlow / (other.MassFlow + this.MassFlow)) + this.Entropy * (this.MassFlow / (other.MassFlow + this.MassFlow));

                            //Calculating the average T weighted on the massflow
                            this.Temperature = Temperature.FromKelvins((double)(other.Temperature.Kelvins * (other.MassFlow / (other.MassFlow + this.MassFlow)) + this.Temperature.Kelvins * (this.MassFlow / (other.MassFlow + this.MassFlow))));
                        }

                        this.MassFlow = other.MassFlow + this.MassFlow;

                        //this.CheckForNaN();

                    }


                }**/
        /// <summary>
        /// Check if two <see cref="Fluid"/> have almost the same <see cref="EngineeringUnits.MassFlow"/> or <see cref="EngineeringUnits.Mass"/>
        /// <br>Both <see cref="Fluid"/>s should use either <see cref="EngineeringUnits.MassFlow"/> or <see cref="EngineeringUnits.Mass"/>!</br>
        /// <br>Tolerence is set to: 0.00001 [kg/s] or 0.00001 [kg]</br>
        /// </summary>
        /// <param name="other"><see cref="Fluid"/> to be copied from</param> 
        //public bool MassBalance(Fluid other)
        //{
        //    //TODO Split this up in MassFlow and Mass

        //    var tolerence = MassFlow.FromKilogramPerSecond(0.0001);
        //    MassFlow? MassFlowDiss = (MassFlow - other.MassFlow).Abs();

        //    return MassFlowDiss <= tolerence;
        //}

        ///// <summary>
        ///// Set a new fluid type to the <see cref="Fluid"/>
        ///// </summary> 
        //public void SetNewType(string RefType)
        //{

        //    if (RefType.ToLower() == REF?.name().ToLower())
        //    {
        //        Log.Debug($"SharpFluid -> SetNewType -> The two fluids is already the same");
        //        return;
        //    }

        //    if (RefType == "")
        //    {
        //        Log.Debug($"SharpFluid -> SetNewType -> You are trying to set a new fluid to nothing!");
        //        return;
        //    }

        //    //if (RefType.ToLower() != REF?.name().ToLower() && RefType != "")

        //    REF = AbstractState.factory("HEOS", RefType);
        //    UpdateFluidConstants();

        //}

        /// <summary>
        /// Set a new fluid type to the <see cref="Fluid"/>
        /// </summary> 
        public void SetNewMedia(FluidList Type) => SetNewMedia(FluidListToMediaType(Type));

        //static object lockObj = new();

        /// <summary>
        /// Set a new fluid type to the <see cref="Fluid"/>
        /// </summary> 
        public void SetNewMedia(MediaType Type)
        {

            if (Type is null)
            {
                Log.Debug($"SharpFluid -> SetNewMedia ->Selected MediaType is null!");
                return;
            }

            if (Media is null)
                Media = new MediaType();

            //lock(lockObj)
            {
                Media.Copy(Type);
                if (Media.BackendType != "CustomFluid")
                    REF = AbstractState.factory(Media.BackendType, Media.InternalName);

                //Set fraction again
                SetFraction(Type.MassFration);

                UpdateFluidConstants();

            }
        }

        //static object lockObj2 = new();

        /// <summary>
        /// Set a mass fraction to the <see cref="Fluid"/> in procent
        /// <br>It might work but it is still in Beta!</br>
        /// </summary>
        public void SetFraction(double? fraction)
        {
            if (fraction is null)
                return;

            //lock(lockObj2)
            {
                CheckFractionLimits((double)fraction);

                if (Media.Mix == MixType.Mass)
                {
                    Media.MassFration = fraction;
                    REF.set_mass_fractions(new DoubleVector(new double[] { (double)fraction/100 }));
                }
                else if (Media.Mix == MixType.Vol)
                {
                    Media.MassFration = fraction;
                    REF.set_volu_fractions(new DoubleVector(new double[] { (double)fraction/100 }));
                }
            }
        }

        /// <summary>
        /// Converts <see cref="FluidList"/> to <see cref="MediaType"/>
        /// </summary>
        public static MediaType FluidListToMediaType(FluidList Type)
        {
            //This Converts FluidList object to MediaType object

            Type type = Type.GetType();
            System.Reflection.MemberInfo[] memInfo = type.GetMember(Type.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(MediaType), false);
            return (attributes.Length > 0) ? (MediaType)attributes[0] : null;
        }

        ///// <summary>
        ///// Add <see cref="EngineeringUnits.Power"/> to the <see cref="Fluid"/>
        ///// <br>This does only work when using <see cref="EngineeringUnits.MassFlow"/></br>
        ///// </summary>
        //public void AddPower(Power powerToBeAdded)
        //{
        //    //TODO If mass is selected!
        //    //Finding the new H

        //    if (MassFlow == MassFlow.Zero)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        SpecificEnergy local = ((Enthalpy * MassFlow) + powerToBeAdded) / MassFlow;

        //        //if (powerToBeAdded > Power.Zero)
        //        if (local > Enthalpy)
        //        {
        //            UpdatePT(Pressure, LimitTemperatureMax);

        //            if (Enthalpy > local)
        //                UpdatePH(Pressure, local);
        //        }
        //        else
        //        {
        //            UpdatePT(Pressure, LimitTemperatureMin);

        //            if (Enthalpy < local)                    
        //                UpdatePH(Pressure, local);

        //        }

        //    }
        //    catch (Exception e)
        //    {

        //        FailState = true;
        //        Log.Error($"SharpFluid -> AddPower -> {e}");
        //    }

        //}

        ///// <summary>
        ///// Remove <see cref="EngineeringUnits.Power"/> from the <see cref="Fluid"/>
        ///// </summary> 
        ///// <remarks>
        ///// <br>This does only work when using <see cref="EngineeringUnits.MassFlow"/></br>
        ///// </remarks>
        //public void RemovePower(Power powerToBeRemoved)
        //{
        //    //TODO: If mass is selected 

        //    AddPower(powerToBeRemoved * -1);
        //}

        /// <summary>
        /// Fractions er still in Beta! And is not yet tested!
        /// </summary> 
        protected void CheckFractionLimits(double fraction)
        {

            if (Media.BackendType == "INCOMP")
            {
                var min = REF.keyed_output(Parameters.ifraction_min) * 100;
                var max = REF.keyed_output(Parameters.ifraction_max) * 100;
                if (fraction < min)
                    throw new System.ArgumentException("Selected fraction is below the limit");
                else if (fraction > max)
                    throw new System.ArgumentException("Selected fraction is above the limit");
            }
        }

        private void CheckBeforeUpdate()
        {
            if (REF is null)
                SetNewMedia(Media);

            if (Media is null)
                throw new System.InvalidOperationException("No Media is selected - Cant do an update on nothing!");
        }

        /// <summary>
        /// Override this function and you can set new defaul (display) units
        /// </summary> 
        //public virtual void SetDefalutDisplayUnits()
        //{

        //    //Units to specifig units
        //    Enthalpy = Enthalpy.ToUnit(SpecificEnergyUnit.KilojoulePerKilogram);
        //    Temperature = Temperature.ToUnit(TemperatureUnit.DegreeCelsius);
        //    Pressure = Pressure.ToUnit(PressureUnit.Bar).ToUnit(PressureReference.Absolute);
        //    Entropy = Entropy.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin);
        //    Density = Density.ToUnit(DensityUnit.KilogramPerCubicMeter);
        //    Cp = Cp.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin);
        //    Cv = Cv.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin);
        //    MassFlow = MassFlow.ToUnit(MassFlowUnit.KilogramPerSecond);
        //    Mass = Mass.ToUnit(MassUnit.Kilogram);
        //    SurfaceTension = SurfaceTension.ToUnit(ForcePerLengthUnit.NewtonPerMeter);
        //    SoundSpeed = SoundSpeed.ToUnit(SpeedUnit.MeterPerSecond);
        //    DynamicViscosity = DynamicViscosity.ToUnit(DynamicViscosityUnit.NewtonSecondPerMeterSquared);
        //    Conductivity = Conductivity.ToUnit(ThermalConductivityUnit.WattPerMeterKelvin);
        //    MolarMass = MolarMass.ToUnit(MolarMassUnit.KilogramPerMole);
        //    InternalEnergy = InternalEnergy.ToUnit(SpecificEnergyUnit.KilojoulePerKilogram);

        //    LimitTemperatureMax = LimitTemperatureMax.ToUnit(TemperatureUnit.DegreeCelsius);
        //    LimitTemperatureMin = LimitTemperatureMin.ToUnit(TemperatureUnit.DegreeCelsius);
        //    CriticalTemperature = CriticalTemperature.ToUnit(TemperatureUnit.DegreeCelsius);
        //    CriticalPressure = CriticalPressure.ToUnit(PressureUnit.Bar).ToUnit(PressureReference.Absolute);
        //    LimitPressureMin = LimitPressureMin.ToUnit(PressureUnit.Bar).ToUnit(PressureReference.Absolute);
        //    LimitPressureMax = LimitPressureMax.ToUnit(PressureUnit.Bar).ToUnit(PressureReference.Absolute);
        //    //CriticalEnthalpy = CriticalEnthalpy.ToUnit(SpecificEnergyUnit.KilojoulePerKilogram);

        //}

        /// <summary>
        /// Checks if the two <see cref="Fluid"/> are almost then same
        /// </summary>
        public static bool operator ==(Fluid other1, Fluid other2)
        {
            //TODO If mass is selected!

            var MassFlowTolerance = MassFlow.FromKilogramPerSecond(0.00001);
            var HTolerance = SpecificEnergy.FromKilojoulePerKilogram(0.001);
            var PTolerance = Pressure.FromBar(0.001);
            var TTolerance = Temperature.FromKelvins(0.0001);

            MassFlow? MassFlowDiss = (other1.MassFlow - other2.MassFlow).Abs();
            SpecificEnergy? HDiss = (other1.Enthalpy - other2.Enthalpy).Abs();
            Pressure? PDiss = (other1.Pressure - other2.Pressure).Abs();
            Temperature? TDiss = (other1.Temperature - other2.Temperature).Abs();

            return (MassFlowDiss <= MassFlowTolerance) &&
                    (HDiss <= HTolerance) &&
                    (PDiss <= PTolerance) &&
                    (TDiss <= TTolerance);
        }

        public static bool operator !=(Fluid Input1, Fluid Input2)
        {
            return !(Input1 == Input2);
        }
        public override bool Equals(object? obj) => base.Equals(obj);
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(_mass);
            hashCode.Add(_massflow);
            hashCode.Add(Temperature);
            hashCode.Add(Pressure);
            hashCode.Add(Enthalpy);
            hashCode.Add(Entropy);
            hashCode.Add(Density);
            hashCode.Add(DynamicViscosity);
            hashCode.Add(Conductivity);
            hashCode.Add(Cp);
            hashCode.Add(Cv);
            hashCode.Add(SoundSpeed);
            hashCode.Add(SurfaceTension);
            hashCode.Add(MolarMass);
            hashCode.Add(InternalEnergy);
            hashCode.Add(Prandtl);
            hashCode.Add(Compressibility);
            hashCode.Add(Quality);
            hashCode.Add(_massflow);

            return hashCode.ToHashCode();

        }
        public void Dispose()
        {
            REF.Dispose();
            REF = null;
            Dispose();
        }

        public string SaveAsJSON() => JsonConvert.SerializeObject(this);

        public Fluid LoadFromJSON(string json) => JsonConvert.DeserializeObject<Fluid>(json);

        public Fluid Clone()
        {
            var Local = new Fluid(Media)
            {
                Compressibility = Compressibility,
                Conductivity = Conductivity,
                Cp = Cp,
                //Local.CriticalEnthalpy = CriticalEnthalpy;
                CriticalPressure = CriticalPressure,
                CriticalTemperature = CriticalTemperature,
                Cv = Cv,
                Density = Density,
                DynamicViscosity = DynamicViscosity,
                Enthalpy = Enthalpy,
                Entropy = Entropy,
                FailState = FailState,
                FractionMax = FractionMax,
                FractionMin = FractionMin,
                InternalEnergy = InternalEnergy,
                LimitPressureMax = LimitPressureMax,
                LimitPressureMin = LimitPressureMin,
                LimitTemperatureMax = LimitTemperatureMax,
                LimitTemperatureMin = LimitTemperatureMin,
                Mass = Mass,
                MassFlow = MassFlow,
                MolarMass = MolarMass,
                Phase = Phase,
                Prandtl = Prandtl,
                Pressure = Pressure,
                Quality = Quality,
                SoundSpeed = SoundSpeed,
                SurfaceTension = SurfaceTension,
                Temperature = Temperature
            };

            return Local;

        }

        public void ResetErrors()
        {
            if (Environment.Is64BitProcess)
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
            else
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();

        }

        public Exception RetrieveErrors()
        {
            if (Environment.Is64BitProcess)
                return CoolPropPINVOKE64.SWIGPendingException.Retrieve();
            else
                return CoolPropPINVOKE.SWIGPendingException.Retrieve();

        }
    }
}