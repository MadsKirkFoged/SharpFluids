using System;
using System.Diagnostics;
//using UnitsNet;
using EngineeringUnits;
//using UnitsNet.Units;
//using UnitsNet.Serialization.JsonNet;
using Newtonsoft.Json;
//using Microsoft.Extensions.Logging;
using EngineeringUnits.Units;
using Serilog;
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
    /// <br><c>Water.UpdatePT(<see cref="UnitsNet.Pressure"/>.FromBars(1.013), <see cref="UnitsNet.Temperature"/>.FromDegreesCelsius(13));</c></br>
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
            SetValuesToZero();
            SetLimitsToZero();
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
        public Temperature GetSatTemperature(Pressure FromThisPressure)
        {

            if (FromThisPressure < LimitPressureMin)
            {
                Log.Debug($"SharpFluid -> GetSatTemperature -> {FromThisPressure} cant be bolow {LimitPressureMin}");
                return Temperature.Zero;
            }


            if (FromThisPressure > CriticalPressure)
            {
                Log.Debug($"SharpFluid -> GetSatTemperature -> Pressure ({FromThisPressure}) is above CriticalPressure {CriticalPressure}. CriticalPressure is returned instead!");
                return CriticalTemperature;
            }
            else
            {
                REF.update(input_pairs.PQ_INPUTS, FromThisPressure.Pascals, 1);
                return REF.T();
            }
        }


        /// <summary>
        /// Get Saturation Pressure from a given Temperature using the type of <see cref="Fluid"/>
        /// </summary> 
        public Pressure GetSatPressure(Temperature FromThisTemperature)
        {

            if (FromThisTemperature < LimitTemperatureMin)
            {
                Log.Debug($"SharpFluid -> GetSatPressure -> {FromThisTemperature} cant be bolow {LimitTemperatureMin}");
                return Pressure.Zero;
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
            FailState = true;


            if (Media.BackendType == "CustomFluid")
            {
                return;
            }


            try
            {
                //Setting the constant values up
                LimitTemperatureMax = REF.Tmax();
                LimitTemperatureMin = REF.Tmin();


                if (REF.backend_name() == "HelmholtzEOSBackend")
                {
                    CriticalTemperature = REF.T_critical();
                    CriticalPressure = REF.p_critical();
                    LimitPressureMin = REF.p_triple();
                    LimitPressureMax = REF.pmax();

                    //Finding H_crit
                    REF.update(input_pairs.PQ_INPUTS, CriticalPressure.Pascals, 1);
                    CriticalEnthalpy = REF.hmass();
                }

                //Fraction
                FractionMin = REF.keyed_output(parameters.ifraction_min);
                FractionMax = REF.keyed_output(parameters.ifraction_max);


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
            try
            {
                if (Media.BackendType == "HEOS")
                {
                    //Mixed fluids does not have these properties 
                    SoundSpeed = REF.speed_sound();
                    MolarMass = REF.molar_mass();
                    Compressibility = REF.compressibility_factor();
                }

                Enthalpy = REF.hmass();
                Temperature = REF.T();
                Pressure = REF.p();
                Entropy = REF.smass();
                Quality = REF.Q();
                Density = REF.rhomass();
                Cp = REF.cpmass();
                Cv = REF.cvmass();
                DynamicViscosity = REF.viscosity();
                Prandtl = REF.Prandtl();
                SurfaceTension = REF.surface_tension();
                InternalEnergy = REF.umass();
                Conductivity = REF.conductivity();
                Phase = (Phases)REF.phase();
                FailState = false;

            }
            catch (Exception e)
            {
                Log.Error($"SharpFluid -> UpdateValues -> {e}");
                throw new Exception("UpdateValues", e);
            }


        }

        /// <summary>
        /// Set all values of <see cref="Fluid"/> to Zero
        /// </summary>   
        public virtual void SetValuesToZero()
        {
            Enthalpy = 0;
            Temperature = 0;
            Pressure = 0;
            Entropy = 0;
            Quality = 0;
            Density = 0;
            Cp = 0;
            Cv = 0;
            MassFlow = 0;
            Mass = 0;
            Prandtl = 0;
            SurfaceTension = 0;
            SoundSpeed = 0;
            FailState = true;
            DynamicViscosity = 0;
            Conductivity = 0;
            MolarMass = 0;
            Compressibility = 0;
            InternalEnergy = 0;
        }

        public virtual void SetLimitsToZero()
        {
            LimitTemperatureMax = 0;
            LimitTemperatureMin = 0;
            CriticalTemperature = 0;
            CriticalPressure = 0;
            LimitPressureMin = 0;
            LimitPressureMax = 0;
            CriticalEnthalpy = 0;
        }


        /// <summary>
        /// Copy all the values from <paramref name="other"/> to this <see cref="Fluid"/>
        /// </summary>  
        /// <param name="other"><see cref="Fluid"/> to be copied from</param>
        public void Copy(Fluid other)
        {
            //Copying Refrigerant type
            CopyType(other);

            this.Enthalpy = other.Enthalpy;
            this.MassFlow = other.MassFlow;
            this.Mass = other.Mass;
            this.Pressure = other.Pressure;
            this.Temperature = other.Temperature;
            this.Entropy = other.Entropy;
            this.Quality = other.Quality;
            this.Density = other.Density;
            this.Cp = other.Cp;
            this.Cv = other.Cv;
            this.CriticalPressure = other.CriticalPressure;
            this.DynamicViscosity = other.DynamicViscosity;
            this.Conductivity = other.Conductivity;
            this.Prandtl = other.Prandtl;
            this.SoundSpeed = other.SoundSpeed;
            this.SurfaceTension = other.SurfaceTension;
            this.FailState = other.FailState;
            this.MolarMass = other.MolarMass;
            this.Compressibility = other.Compressibility;
            this.InternalEnergy = other.InternalEnergy;
        }

        /// <summary>
        /// Copy just the type of fluid from <paramref name="other"/> to this <see cref="Fluid"/>
        /// </summary>  
        /// <param name="other"><see cref="Fluid"/> to be copied from</param>
        public void CopyType(Fluid other)
        {
            //Null check
            if (!(other.Media is null))
            {
                //Actually changing media
                if (this.Media != other.Media)
                {
                    //Since we are changing media when the old values doesn't make sense to keep
                    SetValuesToZero();

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
                /// <br>This makes a simple mixing based on <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/> </br>
                /// <br>Both <see cref="Fluid"/>s should use either <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/>!</br>
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
        /// Check if two <see cref="Fluid"/> have almost the same <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/>
        /// <br>Both <see cref="Fluid"/>s should use either <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/>!</br>
        /// <br>Tolerence is set to: 0.00001 [kg/s] or 0.00001 [kg]</br>
        /// </summary>
        /// <param name="other"><see cref="Fluid"/> to be copied from</param> 
        public bool MassBalance(Fluid other)
        {
            //TODO Split this up in MassFlow and Mass

            MassFlow tolerence = MassFlow.FromKilogramsPerSecond(0.00001);
            MassFlow MassFlowDiss = (this.MassFlow - other.MassFlow).Abs();

            return MassFlowDiss <= tolerence;
        }


        /// <summary>
        /// Set a new fluid type to the <see cref="Fluid"/>
        /// </summary> 
        public void SetNewType(string RefType)
        {

            if (RefType.ToLower() == REF?.name().ToLower())
            {
                Log.Debug($"SharpFluid -> SetNewType -> The two fluids is already the same");
                return;
            }

            if (RefType == "")
            {
                Log.Debug($"SharpFluid -> SetNewType -> You are trying to set a new fluid to nothing!");
                return;
            }



            //if (RefType.ToLower() != REF?.name().ToLower() && RefType != "")

            REF = AbstractState.factory("HEOS", RefType);
            UpdateFluidConstants();

        }

        /// <summary>
        /// Set a new fluid type to the <see cref="Fluid"/>
        /// </summary> 
        public void SetNewMedia(FluidList Type)
        {
            SetNewMedia(FluidListToMediaType(Type));
        }

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


            Media.Copy(Type);
            REF = AbstractState.factory(Media.BackendType, Media.InternalName);
            UpdateFluidConstants();


        }

        /// <summary>
        /// Set a mass fraction to the <see cref="Fluid"/>
        /// <br>It might work but it is still in Beta!</br>
        /// </summary>
        public void SetFraction(double fraction)
        {
            CheckFractionLimits(fraction);

            if (Media.Mix == MixType.Mass)
            {
                REF.set_mass_fractions(new DoubleVector(new double[] { fraction }));
            }
            else if (Media.Mix == MixType.Vol)
            {
                REF.set_volu_fractions(new DoubleVector(new double[] { fraction }));
            }
        }

        /// <summary>
        /// Converts <see cref="FluidList"/> to <see cref="MediaType"/>
        /// </summary>
        public static MediaType FluidListToMediaType(FluidList Type)
        {
            //This Converts FluidList object to MediaType object

            var type = Type.GetType();
            var memInfo = type.GetMember(Type.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(MediaType), false);
            return (attributes.Length > 0) ? (MediaType)attributes[0] : null;
        }


        ///// <summary>
        ///// Add <see cref="UnitsNet.Power"/> to the <see cref="Fluid"/>
        ///// <br>This does only work when using <see cref="UnitsNet.MassFlow"/></br>
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
        ///// Remove <see cref="UnitsNet.Power"/> from the <see cref="Fluid"/>
        ///// </summary> 
        ///// <remarks>
        ///// <br>This does only work when using <see cref="UnitsNet.MassFlow"/></br>
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
                double min = 0;
                double max = 0;

                min = REF.keyed_output(parameters.ifraction_min);
                max = REF.keyed_output(parameters.ifraction_max);

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
        public virtual void SetDefalutDisplayUnits()
        {


            //Units to specifig units
            Enthalpy = Enthalpy.ToUnit(SpecificEnergyUnit.KilojoulePerKilogram);
            Temperature = Temperature.ToUnit(TemperatureUnit.DegreeCelsius);
            Pressure = Pressure.ToUnit(PressureUnit.Bar);
            Entropy = Entropy.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin);
            Density = Density.ToUnit(DensityUnit.KilogramPerCubicMeter);
            Cp = Cp.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin);
            Cv = Cv.ToUnit(SpecificEntropyUnit.KilojoulePerKilogramKelvin);
            MassFlow = MassFlow.ToUnit(MassFlowUnit.KilogramPerSecond);
            Mass = Mass.ToUnit(MassUnit.Kilogram);
            SurfaceTension = SurfaceTension.ToUnit(ForcePerLengthUnit.NewtonPerMeter);
            SoundSpeed = SoundSpeed.ToUnit(SpeedUnit.MeterPerSecond);
            DynamicViscosity = DynamicViscosity.ToUnit(DynamicViscosityUnit.NewtonSecondPerMeterSquared);
            Conductivity = Conductivity.ToUnit(ThermalConductivityUnit.WattPerMeterKelvin);
            MolarMass = MolarMass.ToUnit(MolarMassUnit.KilogramPerMole);
            InternalEnergy = InternalEnergy.ToUnit(SpecificEnergyUnit.KilojoulePerKilogram);

            LimitTemperatureMax = LimitTemperatureMax.ToUnit(TemperatureUnit.DegreeCelsius);
            LimitTemperatureMin = LimitTemperatureMin.ToUnit(TemperatureUnit.DegreeCelsius);
            CriticalTemperature = CriticalTemperature.ToUnit(TemperatureUnit.DegreeCelsius);
            CriticalPressure = CriticalPressure.ToUnit(PressureUnit.Bar);
            LimitPressureMin = LimitPressureMin.ToUnit(PressureUnit.Bar);
            LimitPressureMax = LimitPressureMax.ToUnit(PressureUnit.Bar);
            CriticalEnthalpy = CriticalEnthalpy.ToUnit(SpecificEnergyUnit.KilojoulePerKilogram);

        }




        /// <summary>
        /// Checks if the two <see cref="Fluid"/> are almost then same
        /// </summary>
        public static bool operator ==(Fluid other1, Fluid other2)
        {
            //TODO If mass is selected!

            MassFlow MassFlowTolerance = MassFlow.FromKilogramsPerSecond(0.00001);
            SpecificEnergy HTolerance = SpecificEnergy.FromKilojoulesPerKilogram(0.001);
            Pressure PTolerance = Pressure.FromBars(0.001);
            Temperature TTolerance = Temperature.FromKelvins(0.0001);



            MassFlow MassFlowDiss = (other1.MassFlow - other2.MassFlow).Abs();
            SpecificEnergy HDiss = (other1.Enthalpy - other2.Enthalpy).Abs();
            Pressure PDiss = (other1.Pressure - other2.Pressure).Abs();
            Temperature TDiss = (other1.Temperature - other2.Temperature).Abs();



            return (MassFlowDiss <= MassFlowTolerance) &&
                    (HDiss <= HTolerance) &&
                    (PDiss <= PTolerance) &&
                    (TDiss <= TTolerance);
        }

        public static bool operator !=(Fluid Input1, Fluid Input2)
        {
            return !(Input1 == Input2);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();
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
            this.Dispose();
        }

        public string SaveAsJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Fluid LoadFromJSON(string json)
        {
            return JsonConvert.DeserializeObject<Fluid>(json);
        }


        public Fluid Clone()
        {
            Fluid Local = new Fluid(Media);


            Local.Compressibility = Compressibility;
            Local.Conductivity = Conductivity;
            Local.Cp = Cp;
            Local.CriticalEnthalpy = CriticalEnthalpy;
            Local.CriticalPressure = CriticalPressure;
            Local.CriticalTemperature = CriticalTemperature;
            Local.Cv = Cv;
            Local.Density = Density;
            Local.DynamicViscosity = DynamicViscosity;
            Local.Enthalpy = Enthalpy;
            Local.Entropy = Entropy;
            Local.FailState = FailState;
            Local.FractionMax = FractionMax;
            Local.FractionMin = FractionMin;
            Local.InternalEnergy = InternalEnergy;
            Local.LimitPressureMax = LimitPressureMax;
            Local.LimitPressureMin = LimitPressureMin;
            Local.LimitTemperatureMax = LimitTemperatureMax;
            Local.LimitTemperatureMin = LimitTemperatureMin;
            Local.Mass = Mass;
            Local.MassFlow = MassFlow;
            Local.MolarMass = MolarMass;
            Local.Phase = Phase;
            Local.Prandtl = Prandtl;
            Local.Pressure = Pressure;
            Local.Quality = Quality;
            Local.SoundSpeed = SoundSpeed;
            Local.SurfaceTension = SurfaceTension;
            Local.Temperature = Temperature;

            return Local;

        }


    }
}