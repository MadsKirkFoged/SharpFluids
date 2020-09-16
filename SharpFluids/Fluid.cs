
using System;
using System.Diagnostics;
using UnitsNet;

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

        public SpecificEnergy H { get; set; } //Also called Enthalpy 
        public MassFlow MassFlow { get; set; }
        public MolarMass MolarMass { get; set; }
        public VolumeFlow VolumeFlow
        {
            get
            {
                //Calculate the volumeflow
                if (RHO != Density.Zero)
                {
                    return MassFlow / RHO;
                }
                else
                {
                    return VolumeFlow.Zero;
                }
            }
        }
        public Entropy S { get; set; }
        public double X { get; set; }
        public Density RHO { get; set; }
        public DynamicViscosity Viscosity { get; set; }
        public ThermalConductivity Condutivity { get; set; }
        public SpecificEntropy Cp { get; set; }
        public SpecificEntropy Cv { get; set; }
        public double Prandtl { get; set; }
        public Speed SoundSpeed { get; set; }
        public ForcePerLength SurfaceTension { get; set; }
        //{ 
        //    get {

        //        return ForcePerLength.FromNewtonsPerMeter(REF.surface_tension()); 


        //    }
        //    private set {  } //Cant be set!

        //}


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

        public AbstractState REF { get; protected set; }
        public MediaType Media { get; protected set; }
        public bool FailState { get; protected set; }


        /// Constructors
        public Fluid()
        {

            //No fluid is selected! Trying to set AMMONIA as default
            REF = AbstractState.factory("HEOS", "AMMONIA");


        }
        public Fluid(MediaType Type)
        {

            REF = AbstractState.factory(Type.BackendType, Type.InternalName);

            //DoubleVector z = new DoubleVector(new double[] { 1 });
            //REF.set_mole_fractions(z);

            //REF.set_mass_fractions(z);

            Media = Type;


            UpdateStartValues();

        }
        public Fluid(FluidList Type)
        {


            var type = Type.GetType();
            var memInfo = type.GetMember(Type.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(MediaType), false);
            var test = (attributes.Length > 0) ? (MediaType)attributes[0] : null;

            Media = test;

            REF = AbstractState.factory(test.BackendType, test.InternalName);

            //DoubleVector z = new DoubleVector(new double[] { 1 });
            //REF.set_mole_fractions(z);

            //REF.set_mass_fractions(z);

            UpdateStartValues();
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
                        UpdateHT(H, temperature);
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
                        UpdatePH(pressure, H);
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

            H = SpecificEnergy.FromJoulesPerKilogram(REF.hmass());
            Temperature = Temperature.FromKelvins(REF.T());
            Pressure = Pressure.FromPascals(REF.p());
            S = Entropy.FromJoulesPerKelvin(REF.smass());
            X = REF.Q();
            RHO = Density.FromKilogramsPerCubicMeter(REF.rhomass());
            Cp = SpecificEntropy.FromJoulesPerKilogramKelvin(REF.cpmass());
            Cv = SpecificEntropy.FromJoulesPerKilogramKelvin(REF.cvmass());
            Viscosity = DynamicViscosity.FromPascalSeconds(REF.viscosity());
            Prandtl = REF.Prandtl();
            SoundSpeed = Speed.FromMetersPerSecond(REF.speed_sound());
            MolarMass = MolarMass.FromKilogramsPerMole( REF.molar_mass());
            SurfaceTension = ForcePerLength.FromNewtonsPerMeter(REF.surface_tension());


            if (HasValue(REF.conductivity()))
                Condutivity = ThermalConductivity.FromWattsPerMeterKelvin(REF.conductivity());
            else
                Condutivity = ThermalConductivity.Zero;
            

            FailState = false;

        }
        public virtual void ZeroValues()
        {
            H = SpecificEnergy.Zero;
            Temperature = Temperature.Zero;
            Pressure = Pressure.Zero;
            S = Entropy.Zero;
            X = 0;
            RHO = Density.Zero;
            Cp = SpecificEntropy.Zero;
            Cv = SpecificEntropy.Zero;
            MassFlow = MassFlow.Zero;
            Prandtl = 0;
            SurfaceTension = ForcePerLength.Zero;
            FailState = true;
        }




        ///Method with another 'Fluid' as input
        public void Copy(Fluid other)
        {
            this.H = other.H;
            this.MassFlow = other.MassFlow;
            this.Pressure = other.Pressure;
            this.Temperature = other.Temperature;
            this.S = other.S;
            this.X = other.X;
            this.RHO = other.RHO;
            this.Cp = other.Cp;
            this.Cv = other.Cv;
            this.P_Crit = other.P_Crit;
            this.Viscosity = other.Viscosity;
            this.Condutivity = other.Condutivity;
            this.Prandtl = other.Prandtl;
            this.SoundSpeed = other.SoundSpeed;
            this.MolarMass = other.MolarMass;
            //this.SurfaceTension = other.SurfaceTension;
            this.FailState = other.FailState;

            //Copying Refrigerant type
            CopyType(other);

        }
        public void CopyType(Fluid other)
        {
            if (this.REF?.name() != other?.REF?.name() && !(other.REF is null))
            {
                this.REF = AbstractState.factory("HEOS", other.REF.name());
                UpdateStartValues();
            }
        }
        public void AddTo(Fluid other)
        {

            //This makes a simple mixing based on the massflow (weigted)
            //After the mixing an Update should be run


            if (this.H == SpecificEnergy.Zero || this.Pressure == Pressure.Zero || this.S == Entropy.Zero || this.Temperature == Temperature.Zero || this.MassFlow == MassFlow.Zero)
            {
                this.Copy(other);

            }
            else if (other.H == SpecificEnergy.Zero || other.Pressure == Pressure.Zero || other.S == Entropy.Zero || other.Temperature == Temperature.Zero || other.MassFlow == MassFlow.Zero)
            {

                //Do nothing
            }
            else
            {

                if ((other.MassFlow + this.MassFlow) != MassFlow.Zero)
                {
                    //Calculating the average H weighted on the massflow
                    this.H = other.H * (other.MassFlow / (other.MassFlow + this.MassFlow)) + this.H * (this.MassFlow / (other.MassFlow + this.MassFlow));


                    //Calculating the average P weighted on the massflow
                    this.Pressure = other.Pressure * (other.MassFlow / (other.MassFlow + this.MassFlow)) + this.Pressure * (this.MassFlow / (other.MassFlow + this.MassFlow));


                    //Calculating the average S weighted on the massflow
                    this.S = other.S * (other.MassFlow / (other.MassFlow + this.MassFlow)) + this.S * (this.MassFlow / (other.MassFlow + this.MassFlow));

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

            var type = Type.GetType();
            var memInfo = type.GetMember(Type.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(MediaType), false);
            var localMedia = (attributes.Length > 0) ? (MediaType)attributes[0] : null;

            SetNewMedia(localMedia);

        }
        public void SetNewMedia(MediaType Type)
        {
            this.Media = Type;
            REF = AbstractState.factory(Type.BackendType, Type.InternalName);
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




        ///Methods to manipulate with 'This Fluid'
        public void AddPower(Power powerToBeAdded)
        {
            //Finding the new H

            if (MassFlow != MassFlow.Zero)
            {
                try 
	            {	        
                    SpecificEnergy local = ((H * MassFlow) + powerToBeAdded) / MassFlow;
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
            if (Double.IsNaN(H.Value))
            {
                H = SpecificEnergy.Zero;
            }

            //Entropy 
            if (Double.IsNaN(S.Value))
            {
                S = Entropy.Zero;
            }

            //X
            if (Double.IsNaN(X))
            {
                X = 1;

            }

            //Density
            if (Double.IsNaN(RHO.Value))
            {
                RHO = Density.Zero;
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


            SpecificEnergy HDiss = (other1.H - other2.H);
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

        //Other privates function

        public bool HasValue(double value)
        {
            return !Double.IsNaN(value) && !Double.IsInfinity(value);
        }
    }

}
