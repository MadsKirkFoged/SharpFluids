using System;
using System.Diagnostics;
using UnitsNet;
using UnitsNet.Serialization.JsonNet;
using Newtonsoft.Json;
using JsonNet.ContractResolvers;

namespace SharpFluids
{


    public class Fluid
    {

        ///Fluid properties
        public Temperature Temperature { get; set; }
        public Temperature Tsat
        {

            get
            {

                if (Pressure >= P_Min && Pressure <= P_Crit && P_Min != P_Crit) //P_Min != P_Crit is a quick fix - We should create a check to see if the object has beed created correctly!
                {
                    try
                    {
                        if (!(REF is null))
                        {
                            REF.update(input_pairs.PQ_INPUTS, Pressure.Pascals, 1);
                            return Temperature.FromKelvins(REF.T());
                        }
                        else
                        {
                            return Temperature;
                        }
                    }
                    catch (Exception)
                    {
                        return Temperature;
                    }
                }
                else if (Pressure > P_Crit)
                {
                    try
                    {
                        REF.update(input_pairs.HmassP_INPUTS, H_Crit.JoulesPerKilogram, Pressure.Pascals);
                        return Temperature.FromKelvins(REF.T());

                    }
                    catch (Exception)
                    {

                        return Temperature;
                    }
                }
                else
                {
                    return Temperature;
                }



            }



        }
        public Pressure Pressure { get; set; }
        public SpecificEnergy Enthalpy { get; set; } //Also called Enthalpy 
        public MassFlow MassFlow { get; set; }

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
                    return VolumeFlow.Zero;
                }
            }
        }
        public Entropy Entropy { get; set; }
        public double Quality { get; set; }
        public Density Density { get; set; }
        public DynamicViscosity Viscosity { get; set; }
        public ThermalConductivity Conductivity { get; set; }
        public SpecificEntropy Cp { get; set; }
        public SpecificEntropy Cv { get; set; }
        public double Prandtl { get; set; }
        public Speed SoundSpeed { get; set; }
        public ForcePerLength SurfaceTension { get; set; }

        public MolarMass MolarMass { get; set; }
        public double Compressibility { get; set; }
        public SpecificEnergy InternalEnergy { get; set; }





        ///Fluid Limits
        public Temperature T_Max { get; protected set; }
        public Temperature T_Min { get; protected set; }
        public Temperature T_Crit { get; protected set; }
        public SpecificEnergy H_Crit { get; protected set; }
        public Pressure P_Crit { get; protected set; }
        public Pressure P_Min { get; protected set; }
        public Pressure P_Max { get; protected set; }
        public double FractionMax { get; protected set; }
        public double FractionMin { get; protected set; }



        /// Other values

        public AbstractState REF { get; set; }
        public MediaType Media { get; protected set; }
        public bool FailState { get; protected set; }


        /// Constructors
        public Fluid()
        {
        }
        public Fluid(MediaType Type)
        {
            SetNewMedia(Type);
        }
        public Fluid(FluidList Type) :this(FluidListToMediaType(Type))
        {

        }



        ///Updates
        public void UpdateDS(Density rho, Entropy entropy)
        {
            CheckBeforeUpdate();

            if (rho > Density.Zero && entropy > Entropy.Zero)
            {
                try
                {
                    REF.update(input_pairs.DmassSmass_INPUTS, rho.KilogramsPerCubicMeter, entropy.JoulesPerKelvin);
                    UpdateValues();

                }
                catch (Exception e)
                {
                    FailState = true;
                    Debug.Print("CoolProp: Warning in UpdateDS" + e);
                }
            }
            else
            {
                FailState = true;
            }

        }
        public void UpdateDP(Density rho, Pressure pressure)
        {
            CheckBeforeUpdate();
            try
            {
                REF.update(input_pairs.DmassP_INPUTS, rho.KilogramsPerCubicMeter, pressure.Pascals);
                UpdateValues();
            }
            catch (Exception e)
            {
                FailState = true;
                Debug.Print("Coolprop: Warning in UpdateDP" + e);
            }

        }
        public void UpdateDT(Density rho, Temperature temperature)
        {
            CheckBeforeUpdate();
            try
            {
                REF.update(input_pairs.DmassT_INPUTS, rho.KilogramsPerCubicMeter, temperature.Kelvins);
                UpdateValues();
            }
            catch (Exception e)
            {
                FailState = true;
                Debug.Print("Coolprop: Warning in UpdateDT" + e);
            }

        }
        public void UpdateDH(Density rho, SpecificEnergy enthalpy)
        {
            CheckBeforeUpdate();
            try
            {
                REF.update(input_pairs.DmassHmass_INPUTS, rho.KilogramsPerCubicMeter, enthalpy.JoulesPerKilogram);
                UpdateValues();
            }
            catch (Exception e)
            {
                FailState = true;
                Debug.Print("Coolprop: Warning in UpdateDH" + e);
            }

        }
        public void UpdatePT(Pressure pressure, Temperature temperature)
        {
            CheckBeforeUpdate();
            if (pressure > P_Min && temperature > T_Min)
            {
                try
                {
                    REF.update(input_pairs.PT_INPUTS, pressure.Pascals, temperature.Kelvins);
                    UpdateValues();
                }
                catch (Exception e)
                {
                    FailState = true;
                    Debug.Print("Coolprop: Warning in UpdatePT " + e);
                }
            }
            else
            {
                FailState = true;
            }



        }
        public void UpdateXT(double quality, Temperature temperature)
        {

            CheckBeforeUpdate();
            if (temperature >= T_Min)
            {
                try
                {
                    //If we are above transcritical
                    if (temperature >= T_Crit)
                    {
                        UpdatePT(P_Crit, T_Crit);
                        UpdateHT(Enthalpy, temperature);
                    }
                    else
                    {
                        REF.update(input_pairs.QT_INPUTS, quality, temperature.Kelvins);
                        UpdateValues();
                    }
                }
                catch (Exception e)
                {
                    FailState = true;
                    Debug.Print("Coolprop: Warning in UpdateXT" + e);
                }

            }
            else
            {
                FailState = true;
            }
        }
        private void UpdateHT(SpecificEnergy enthalpy, Temperature temperature)
        {
            //Not yet supported by CoolProp!
        }
        public void UpdatePS(Pressure pressure, Entropy entropy)
        {
            CheckBeforeUpdate();
            if (pressure > Pressure.Zero && entropy != Entropy.Zero)
            {
                try
                {
                    REF.update(input_pairs.PSmass_INPUTS, pressure.Pascals, entropy.JoulesPerKelvin);
                    UpdateValues();
                }
                catch (Exception e)
                {
                    FailState = true;
                    Debug.Print("Coolprop: Warning in UpdatePS" + e);
                }
            }
            else
            {
                FailState = true;
            }
        }
        public void UpdatePH(Pressure pressure, SpecificEnergy enthalpy)
        {
            CheckBeforeUpdate();
            if (pressure > Pressure.Zero && enthalpy > SpecificEnergy.Zero)
            {
                try
                {
                    REF.update(input_pairs.HmassP_INPUTS, enthalpy.JoulesPerKilogram, pressure.Pascals);
                    UpdateValues();
                }
                catch (Exception e)
                {
                    FailState = true;
                    Debug.Print("Coolprop: Warning in UpdatePH" + e);
                }
            }
            else
            {
                FailState = true;
            }

        }
        public void UpdatePX(Pressure pressure, double quality)
        {

            CheckBeforeUpdate();
            if (pressure > Pressure.Zero && quality >= 0)
            {

                try
                {
                    if (pressure > P_Crit)
                    {
                        UpdatePT(P_Crit, T_Crit);
                        UpdatePH(pressure, Enthalpy);
                    }
                    else
                    {
                        REF.update(input_pairs.PQ_INPUTS, pressure.Pascals, quality);
                        UpdateValues();
                    }
                }
                catch (Exception e)
                {
                    FailState = true;
                    Debug.Print("Coolprop: Warning in UpdatePX " + e);
                }

            }
            else
            {
                FailState = true;
            }

        }
        public void UpdateHS(SpecificEnergy enthalpy, Entropy entropy)
        {
            CheckBeforeUpdate();
            try
            {
                REF.update(input_pairs.HmassSmass_INPUTS, enthalpy.JoulesPerKilogram, entropy.JoulesPerKelvin);
                UpdateValues();
            }
            catch (Exception e)
            {
                FailState = true;
                Debug.Print("Coolprop: Warning in UpdateHS " + e);
            }


        }



        //Returns without altering internal values
        public Pressure UpdateP_sat(Temperature temperature)
        {
            CheckBeforeUpdate();
            if (temperature >= T_Min)
            {
                try
                {
                    //If we are above transcritical
                    if (temperature >= T_Crit)
                    {

                        //Need to fix this!

                        //UpdatePT(P_Crit, T_Crit);
                        //UpdateHT(H, temperature);

                        return Pressure.FromPascals(0);
                    }
                    else
                    {
                        REF.update(input_pairs.QT_INPUTS, 1, temperature.Kelvins);
                        return Pressure.FromPascals(REF.p());
                    }
                }
                catch (Exception e)
                {
                    FailState = true;
                    Debug.Print("Refrigerant: Warning in UpdateXT" + e);
                }

            }

            return Pressure.FromPascals(0);
        }



        ///Update internal values        
        protected virtual void UpdateStartValues()
        {
            FailState = true;

            //Setting the constant values up
            T_Max = Temperature.FromKelvins(REF.Tmax());
            T_Min = Temperature.FromKelvins(REF.Tmin());


            if (REF.backend_name() == "HelmholtzEOSBackend")
            {
                T_Crit = Temperature.FromKelvins(REF.T_critical());
                P_Crit = Pressure.FromPascals(REF.p_critical());
                P_Min = Pressure.FromPascals(REF.p_triple());
                P_Max = Pressure.FromPascals(REF.pmax());

                //Finding H_crit
                REF.update(input_pairs.PT_INPUTS, P_Crit.Pascals, T_Crit.Kelvins);
                H_Crit = SpecificEnergy.FromJoulesPerKilogram(REF.hmass());

            }



            //Setting Max/min Mass- or volfraction


        }
        protected virtual void UpdateValues()
        {

            Enthalpy = SpecificEnergy.FromJoulesPerKilogram(REF.hmass());
            Temperature = Temperature.FromKelvins(REF.T());
            Pressure = Pressure.FromPascals(REF.p());
            Entropy = Entropy.FromJoulesPerKelvin(REF.smass());
            Quality = REF.Q();
            Density = Density.FromKilogramsPerCubicMeter(REF.rhomass());
            Cp = SpecificEntropy.FromJoulesPerKilogramKelvin(REF.cpmass());
            Cv = SpecificEntropy.FromJoulesPerKilogramKelvin(REF.cvmass());
            Viscosity = DynamicViscosity.FromPascalSeconds(REF.viscosity());
            Prandtl = REF.Prandtl();
            SurfaceTension = ForcePerLength.FromNewtonsPerMeter(REF.surface_tension());
            InternalEnergy = SpecificEnergy.FromJoulesPerKilogram(REF.umass());


            if (Media.BackendType == "HEOS")
            {         
                //Mixed fluids does not have these properties 
                SoundSpeed = Speed.FromMetersPerSecond(REF.speed_sound());
                MolarMass = MolarMass.FromKilogramsPerMole(REF.molar_mass());
                Compressibility = REF.compressibility_factor();
            }


            if (HasValue(REF.conductivity()))
                Conductivity = ThermalConductivity.FromWattsPerMeterKelvin(REF.conductivity());
            else
                Conductivity = ThermalConductivity.Zero;


            FailState = false;

        }
        public virtual void ZeroValues()
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
            Prandtl = 0;
            SurfaceTension = ForcePerLength.Zero;
            FailState = true;

            MolarMass = MolarMass.Zero;
            Compressibility = 0;
            InternalEnergy = SpecificEnergy.Zero;
        }




        ///Method with another 'Fluid' as input
        public void Copy(Fluid other)
        {
            this.Enthalpy = other.Enthalpy;
            this.MassFlow = other.MassFlow;
            this.Pressure = other.Pressure;
            this.Temperature = other.Temperature;
            this.Entropy = other.Entropy;
            this.Quality = other.Quality;
            this.Density = other.Density;
            this.Cp = other.Cp;
            this.Cv = other.Cv;
            this.P_Crit = other.P_Crit;
            this.Viscosity = other.Viscosity;
            this.Conductivity = other.Conductivity;
            this.Prandtl = other.Prandtl;
            this.SoundSpeed = other.SoundSpeed;
            //this.SurfaceTension = other.SurfaceTension;
            this.FailState = other.FailState;

            this.MolarMass = other.MolarMass;
            this.Compressibility = other.Compressibility;
            this.InternalEnergy = other.InternalEnergy;


            //Copying Refrigerant type
            CopyType(other);

        }
        public void CopyType(Fluid other)
        {

            if (!(other.Media is null))
            {
                SetNewMedia(other.Media);
            }


        }
        public void AddTo(Fluid other)
        {

            //This makes a simple mixing based on the massflow (weigted)
            //After the mixing an Update should be run


            if (this.Enthalpy == SpecificEnergy.Zero || this.Pressure == Pressure.Zero || this.Entropy == Entropy.Zero || this.Temperature == Temperature.Zero || this.MassFlow == MassFlow.Zero)
            {
                this.Copy(other);

            }
            else if (other.Enthalpy == SpecificEnergy.Zero || other.Pressure == Pressure.Zero || other.Entropy == Entropy.Zero || other.Temperature == Temperature.Zero || other.MassFlow == MassFlow.Zero)
            {

                //Do nothing
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
                    this.Temperature = Temperature.FromKelvins(other.Temperature.Kelvins * (other.MassFlow / (other.MassFlow + this.MassFlow)) + this.Temperature.Kelvins * (this.MassFlow / (other.MassFlow + this.MassFlow)));
                }

                this.MassFlow = other.MassFlow + this.MassFlow;

                this.CheckForNaN();

            }


        }
        public bool MassBalance(Fluid other)
        {

            MassFlow tolerence = MassFlow.FromKilogramsPerSecond(0.00001);

            MassFlow MassFlowDiss = (this.MassFlow - other.MassFlow);

            if (MassFlowDiss < MassFlow.Zero)
            {
                MassFlowDiss *= -1;
            }

            return MassFlowDiss <= tolerence;

        }


        ///Methods to set stuff
        public void SetNewType(string RefType)
        {
            if (RefType.ToLower() != REF?.name().ToLower() && RefType != "")
            {
                Debug.Print(REF.name());
                REF = AbstractState.factory("HEOS", RefType);
                UpdateStartValues();

                Debug.Print(REF.name());
            }
        }
        public void SetNewMedia(FluidList Type)
        {
            SetNewMedia(FluidListToMediaType(Type));
        }
        public void SetNewMedia(MediaType Type)
        {

            if (Media is null)            
                Media = new MediaType();            
            
            Media.Copy(Type);
            REF = AbstractState.factory(Media.BackendType, Media.InternalName);
            UpdateStartValues();

        }
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
        public static MediaType FluidListToMediaType(FluidList Type)
        {

            //This Converts FluidList object to MediaType object

            var type = Type.GetType();
            var memInfo = type.GetMember(Type.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(MediaType), false);
            return (attributes.Length > 0) ? (MediaType)attributes[0] : null;


        }



        ///Methods to manipulate with 'This Fluid'
        public void AddPower(Power powerToBeAdded)
        {
            //Finding the new H

            if (MassFlow != MassFlow.Zero)
            {
                try
                {
                    SpecificEnergy local = ((Enthalpy * MassFlow) + powerToBeAdded) / MassFlow;
                    UpdatePH(Pressure, local);

                }
                catch (Exception e)
                {

                    FailState = true;
                    Debug.Print("CoolProp: Warning in AddPower" + e);
                }

            }
        }
        public void RemovePower(Power powerToBeRemoved)
        {
            AddPower(powerToBeRemoved * -1);
        }


        //Checks
        protected void CheckFractionLimits(double fraction)
        {


            if (Media.BackendType == "INCOMP")
            {



                double min = REF.keyed_output(parameters.ifraction_min);
                double max = REF.keyed_output(parameters.ifraction_max);




                if (fraction < min)
                {
                    throw new System.ArgumentException("Selected fraction is below the limit");
                }
                else if (fraction > max)
                {
                    throw new System.ArgumentException("Selected fraction is above the limit");
                }


            }

        }
        public void CheckForNaN()
        {
            //Temperature 
            if (Double.IsNaN(Temperature.Value))
            {
                Temperature = Temperature.Zero;
            }


            //Pressure 
            if (Double.IsNaN(Pressure.Value))
            {
                Pressure = Pressure.Zero;

            }


            //Massflow 
            if (Double.IsNaN(MassFlow.Value))
            {
                MassFlow = MassFlow.Zero;

            }

            //Entalphy 
            if (Double.IsNaN(Enthalpy.Value))
            {
                Enthalpy = SpecificEnergy.Zero;
            }

            //Entropy 
            if (Double.IsNaN(Entropy.Value))
            {
                Entropy = Entropy.Zero;
            }

            //X
            if (Double.IsNaN(Quality))
            {
                Quality = 1;

            }

            //Density
            if (Double.IsNaN(Density.Value))
            {
                Density = Density.Zero;
            }


            //Cp 
            if (Double.IsNaN(Cp.Value))
            {
                Cp = SpecificEntropy.Zero;

            }


        }
        private void CheckBeforeUpdate()
        {

            if (Media is null)
            {
                throw new System.InvalidOperationException("No Media is selected - Cant do an update on nothing!");
            }



        }



        //Overloads
        public static bool operator ==(Fluid other1, Fluid other2)
        {

            MassFlow MassFlowTolerance = MassFlow.FromKilogramsPerSecond(0.00001);
            SpecificEnergy HTolerance = SpecificEnergy.FromKilojoulesPerKilogram(0.001);
            Pressure PTolerance = Pressure.FromBars(0.001);
            TemperatureDelta TTolerance = TemperatureDelta.FromDegreesCelsius(0.0001);



            MassFlow MassFlowDiss = (other1.MassFlow - other2.MassFlow);
            if (MassFlowDiss < MassFlow.Zero)
            {
                MassFlowDiss *= -1;
            }


            SpecificEnergy HDiss = (other1.Enthalpy - other2.Enthalpy);
            if (HDiss < SpecificEnergy.Zero)
            {
                HDiss *= -1;
            }

            Pressure PDiss = (other1.Pressure - other2.Pressure);
            if (PDiss < Pressure.Zero)
            {
                PDiss *= -1;
            }

            TemperatureDelta TDiss = (other1.Temperature - other2.Temperature);
            if (TDiss < TemperatureDelta.Zero)
            {
                TDiss *= -1;
            }




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
        public string GetREFType()
        {
            return REF.name();
        }
        public void Dispose()
        {
            REF.Dispose();
            REF = null;
            this.Dispose();
        }

        //JSON
        public JsonSerializerSettings ReturnJSONSettings()
        {
            //Setting for both UnitsNet and PreserveReferences
            var JsonSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.All,
                ContractResolver = new PrivateSetterContractResolver(),

            };

            JsonSettings.Converters.Add(new UnitsNetIQuantityJsonConverter());

            return JsonSettings;
        }

        public string SaveAsJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, ReturnJSONSettings());
        }

        public Fluid LoadFromJSON(string json)
        {
            return JsonConvert.DeserializeObject<Fluid>(json, ReturnJSONSettings());
        }

        //Other privates function

        public bool HasValue(double value)
        {
            return !Double.IsNaN(value) && !Double.IsInfinity(value);
        }
    }

}