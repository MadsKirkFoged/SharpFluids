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
            //SetDefalutDisplayUnits();
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

                //SetDefalutDisplayUnits();
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

                //Debug.Print($"Value: {REF.viscosity()}");
                DynamicViscosity = REF.viscosity();


                Prandtl = REF.Prandtl();
                SurfaceTension = REF.surface_tension();
                InternalEnergy = REF.umass();
                Conductivity = REF.conductivity();

                FailState = false;
                //SetDefalutDisplayUnits();
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
            Enthalpy = SpecificEnergy.Zero;
            Temperature = Temperature.Zero;
            Pressure = Pressure.Zero;
            Entropy = Entropy.Zero;
            Quality = 0;
            Density = Density.Zero;
            Cp = SpecificEntropy.Zero;
            Cv = SpecificEntropy.Zero;
            MassFlow = MassFlow.Zero;
            Mass = Mass.Zero;
            Prandtl = 0;
            SurfaceTension = ForcePerLength.Zero;
            SoundSpeed = Speed.Zero;
            FailState = true;
            DynamicViscosity = DynamicViscosity.Zero;
            Conductivity = ThermalConductivity.Zero;
            MolarMass = MolarMass.Zero;
            Compressibility = 0;
            InternalEnergy = SpecificEnergy.Zero;

        }

        public virtual void SetLimitsToZero()
        {
           
            LimitTemperatureMax = Temperature.Zero;
            LimitTemperatureMin = Temperature.Zero;
            CriticalTemperature = Temperature.Zero;
            CriticalPressure = Pressure.Zero;
            LimitPressureMin = Pressure.Zero;
            LimitPressureMax = Pressure.Zero;
            CriticalEnthalpy = SpecificEnergy.Zero;


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


            if (this.Enthalpy == SpecificEnergy.Zero || this.Pressure == Pressure.Zero || this.Entropy == Entropy.Zero || this.Temperature == Temperature.Zero || this.MassFlow == MassFlow.Zero)
            {
                this.Copy(other);
            }
            else if (other.Enthalpy == SpecificEnergy.Zero || other.Pressure == Pressure.Zero || other.Entropy == Entropy.Zero || other.Temperature == Temperature.Zero || other.MassFlow == MassFlow.Zero)
            {
                //Do nothing
                Log.Debug($"SharpFluid -> AddTo -> {other.Enthalpy} or {other.Pressure} or {other.Entropy} or {other.Temperature} or {other.MassFlow} is zero and nothing is done!");
            }
            else
            {

                if ((other.MassFlow + this.MassFlow) != MassFlow.Zero)
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


        }

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

            MassFlow MassFlowDiss = (this.MassFlow - other.MassFlow);

            if (MassFlowDiss < MassFlow.Zero)
            {
                MassFlowDiss *= -1;
            }

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


        /// <summary>
        /// Add <see cref="UnitsNet.Power"/> to the <see cref="Fluid"/>
        /// <br>This does only work when using <see cref="UnitsNet.MassFlow"/></br>
        /// </summary>
        public void AddPower(Power powerToBeAdded)
        {
            //TODO If mass is selected!
            //Finding the new H


            if (MassFlow == MassFlow.Zero)
            {
                return;
            }


            
            try
            {
                SpecificEnergy local = ((Enthalpy * MassFlow) + powerToBeAdded) / MassFlow;

                //if (powerToBeAdded > Power.Zero)
                if (local > Enthalpy)
                {
                    UpdatePT(Pressure, LimitTemperatureMax);

                    if (Enthalpy > local)
                        UpdatePH(Pressure, local);
                }
                else
                {
                    UpdatePT(Pressure, LimitTemperatureMin);

                    if (Enthalpy < local)                    
                        UpdatePH(Pressure, local);

                }

            }
            catch (Exception e)
            {

                FailState = true;
                Log.Error($"SharpFluid -> AddPower -> {e}");
            }


        }

        /// <summary>
        /// Remove <see cref="UnitsNet.Power"/> from the <see cref="Fluid"/>
        /// </summary> 
        /// <remarks>
        /// <br>This does only work when using <see cref="UnitsNet.MassFlow"/></br>
        /// </remarks>
        public void RemovePower(Power powerToBeRemoved)
        {
            //TODO: If mass is selected 

            AddPower(powerToBeRemoved * -1);
        }


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


                //foreach (parameters suit in (parameters[])Enum.GetValues(typeof(parameters)))
                //{

                //    Debug.Print($"{suit}: {REF.keyed_output(suit)}");
                //}



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
            ////Units to a default IS unit
            //Enthalpy = Enthalpy.ToUnit(SpecificEnergyUnit.JoulePerKilogram);
            //Temperature = Temperature.ToUnit(TemperatureUnit.Kelvin);
            //Pressure = Pressure.ToUnit(PressureUnit.Pascal);
            //Entropy = Entropy.ToUnit(EntropyUnit.JoulePerKelvin);
            //Density = Density.ToUnit(DensityUnit.KilogramPerCubicMeter);
            //Cp = Cp.ToUnit(SpecificEntropyUnit.JoulePerKilogramKelvin);
            //Cv = Cv.ToUnit(SpecificEntropyUnit.JoulePerKilogramKelvin);
            //MassFlow = MassFlow.ToUnit(MassFlowUnit.KilogramPerSecond);
            //Mass = Mass.ToUnit(MassUnit.Kilogram);
            //SurfaceTension = SurfaceTension.ToUnit(ForcePerLengthUnit.NewtonPerMeter);
            //SoundSpeed = SoundSpeed.ToUnit(SpeedUnit.MeterPerSecond);
            //DynamicViscosity = DynamicViscosity.ToUnit(DynamicViscosityUnit.NewtonSecondPerMeterSquared);
            //Conductivity = Conductivity.ToUnit(ThermalConductivityUnit.WattPerMeterKelvin);
            //MolarMass = MolarMass.ToUnit(MolarMassUnit.KilogramPerMole);
            //InternalEnergy = InternalEnergy.ToUnit(SpecificEnergyUnit.JoulePerKilogram);

            //LimitTemperatureMax = LimitTemperatureMax.ToUnit(TemperatureUnit.Kelvin);
            //LimitTemperatureMin = LimitTemperatureMin.ToUnit(TemperatureUnit.Kelvin);
            //CriticalTemperature = CriticalTemperature.ToUnit(TemperatureUnit.Kelvin);
            //CriticalPressure = CriticalPressure.ToUnit(PressureUnit.Pascal);
            //LimitPressureMin = LimitPressureMin.ToUnit(PressureUnit.Pascal);
            //LimitPressureMax = LimitPressureMax.ToUnit(PressureUnit.Pascal);
            //CriticalEnthalpy = CriticalEnthalpy.ToUnit(SpecificEnergyUnit.JoulePerKilogram);

            //Units to specifig units
            Enthalpy = Enthalpy.ToUnit(SpecificEnergyUnit.KilojoulePerKilogram);
            Temperature = Temperature.ToUnit(TemperatureUnit.DegreeCelsius);
            Pressure = Pressure.ToUnit(PressureUnit.Bar);
            Entropy = Entropy.ToUnit(EntropyUnit.KilojoulePerKelvin);
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



            MassFlow MassFlowDiss = (other1.MassFlow - other2.MassFlow);
            if (MassFlowDiss < MassFlow.Zero)            
                MassFlowDiss *= -1;          

            SpecificEnergy HDiss = (other1.Enthalpy - other2.Enthalpy);
            if (HDiss < SpecificEnergy.Zero)            
                HDiss *= -1;
            

            Pressure PDiss = (other1.Pressure - other2.Pressure);
            if (PDiss < Pressure.Zero)            
                PDiss *= -1;
            

            Temperature TDiss = (other1.Temperature - other2.Temperature);
            if (TDiss < Temperature.Zero)            
                TDiss *= -1;
            

            return (MassFlowDiss <= MassFlowTolerance) && (HDiss <= HTolerance) && (PDiss <= PTolerance) && (TDiss <= TTolerance);
        }

        public static bool operator !=(Fluid Input1, Fluid Input2)
        {

            if (Input1 == Input2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public void Dispose()
        {

            
                REF.Dispose();
                REF = null;
                this.Dispose();
            


            
        }

        /// <summary>
        /// Not in use anymore
        /// </summary>
        //public JsonSerializerSettings ReturnJSONSettings()
        //{

        //    //We might not have to use this anymore!

        //    //Setting for both UnitsNet and PreserveReferences
        //    var JsonSettings = new JsonSerializerSettings
        //    {
        //        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        //        TypeNameHandling = TypeNameHandling.All,
        //    };

        //    JsonSettings.Converters.Add(new UnitsNetIQuantityJsonConverter());

        //    return JsonSettings;
        //}

        public string SaveAsJSON()
        {
            //return JsonConvert.SerializeObject(this, ReturnJSONSettings());
            return JsonConvert.SerializeObject(this);
        }

        public Fluid LoadFromJSON(string json)
        {
            //return JsonConvert.DeserializeObject<Fluid>(json, ReturnJSONSettings());
            return JsonConvert.DeserializeObject<Fluid>(json);
        }


        public void GetListOfPreMix()
        {

            var test = REF.GetInfo("predefined_mixtures");


        }


    }



}