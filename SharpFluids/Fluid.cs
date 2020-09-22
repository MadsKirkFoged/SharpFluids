using System;
using System.Diagnostics;
using UnitsNet;
using UnitsNet.Serialization.JsonNet;
using Newtonsoft.Json;
using JsonNet.ContractResolvers;

namespace SharpFluids
{


    /// <summary>
    /// A Fluid object carries the condition and properties of a fluid.        
    /// <br> You use the Update functions to change the fluids condition.</br>
    /// <br> Example:</br>
    /// </summary>
    /// <remarks>
    /// <Code>
    /// <paramref name="Fluid"/> Water = new <paramref name="Fluid"/>(FluidList.Water);
    /// <br>Water.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(13));</br>
    /// <para> Debug.Print("Density of the water is: " + Water.Density);</para>
    /// </Code>
    /// </remarks>

    public class Fluid
    {

        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        private Mass _mass;
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        private MassFlow _massflow;

        /// <summary>
        /// Get the <paramref name="Temperature"/> of the fluid.
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Temperature Temperature { get; private set; }


        /// <summary>
        /// Get the <paramref name="Pressure"/> of the fluid.
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Pressure Pressure { get; private set; }


        /// <summary>
        /// Get the <paramref name="Enthalpy"/> of the fluid. 
        /// <br>Engineers may refers to this a "H".<br/>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEnergy Enthalpy { get; private set; } //Also called Enthalpy 


        /// <summary>
        /// Get the <paramref name="Entropy"/> of the fluid. 
        /// <br>Engineers may refers to this a "S".<br/>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Entropy Entropy { get; private set; }


        /// <summary>
        /// Get the <paramref name="Density"/> of the fluid. 
        /// <br>Engineers may refers to this a "RHO".<br/>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Density Density { get; private set; }


        /// <summary>
        /// Get the <paramref name="Viscosity"/> of the fluid. 
        /// <br>This is the DynamicViscosity.<br/>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public DynamicViscosity Viscosity { get; private set; }

        /// <summary>
        /// Get the <paramref name="Conductivity"/> of the fluid. 
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public ThermalConductivity Conductivity { get; private set; }

        /// <summary>
        /// Get the <paramref name="Cp"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// Engineers may refers to this a "Heat capacity at constant pressure".
        /// </remarks>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEntropy Cp { get; private set; }

        /// <summary>
        /// Get the <paramref name="Cv"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// Engineers may refers to this a "Heat capacity at constant volume".
        /// </remarks>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEntropy Cv { get; private set; }

        /// <summary>
        /// Get the <paramref name="SoundSpeed"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// Speed of sound in the fluid.
        /// </remarks>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Speed SoundSpeed { get; private set; }

        /// <summary>
        /// Get the <paramref name="SurfaceTension"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// Surface tension of the fluid
        /// <br>This is only valid when the <paramref name="Fluid"/> is a mixture of gas and liquid!.</br>
        /// </remarks>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public ForcePerLength SurfaceTension { get; private set; }

        /// <summary>
        /// Get the <paramref name="MolarMass"/> of the fluid. 
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public MolarMass MolarMass { get; private set; }

        /// <summary>
        /// Get the <paramref name="InternalEnergy"/> of the fluid. 
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEnergy InternalEnergy { get; private set; }

        /// <summary>
        /// Get the <paramref name="Prandtl"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// Engineers may refers to this as "Prandtl number" or "Pr".
        /// </remarks> 
        [JsonProperty]
        public double Prandtl { get; private set; }

        /// <summary>
        /// Get the <paramref name="Prandtl"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// Engineers may refers to this as coefficient of compressibility or isothermal compressibility.
        /// </remarks> 
        [JsonProperty]
        public double Compressibility { get; private set; }

        /// <summary>
        /// Get the <paramref name="Quality"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// Mass fraction of vapour
        /// <br>Quality = 0 => 0% vapour</br>
        /// <br>Quality = 1 => 100% vapour</br>
        /// <br>Engineers may refers to this as "X".</br>
        /// </remarks> 
        [JsonProperty]
        public double Quality { get; private set; }



        /// <summary>
        /// Set the <paramref name="MassFlow"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// You can choose between using <paramref name="MassFlow"/> or <paramref name="Mass"/> when defining a fluid.
        /// </remarks> 
        [JsonIgnore]
        public MassFlow MassFlow 
        {
            get { return _massflow; }

            set 
            {

                if (Mass == Mass.Zero)
                {
                    _massflow = value;
                }
                else
                {
                    throw new System.InvalidOperationException("You can either set a fluids massflow or mass - not both!");
                }

            } 
        }

        /// <summary>
        /// Set the <paramref name="Mass"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// You can choose between using <paramref name="MassFlow"/> or <paramref name="Mass"/> when defining a fluid.
        /// </remarks> 
        [JsonIgnore]
        public Mass Mass
        {
            get { return _mass; }

            set
            {

                if (MassFlow == MassFlow.Zero)
                {
                    _mass = value;
                }
                else
                {
                    throw new System.InvalidOperationException("You can either set a fluids massflow or mass - not both!");
                }

            }
        }

        /// <summary>
        /// Get the Saturation temperature of the fluid. 
        /// </summary>
        /// <remarks>
        /// Beware: If you are above the critical pressure of the <paramref name="Fluid"/> this will just return Saturation temperature at the critical pressure!
        /// </remarks> 
        [JsonIgnore]
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


        /// <summary>
        /// Get the <paramref name="Volume"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// This is used when the <paramref name="Mass"/> of the <paramref name="Fluid"/> is set
        /// </remarks> 
        [JsonIgnore]
        public Volume Volume
        {
            get
            {
                //Calculate the volume
                if (Density != Density.Zero)                
                    return Mass / Density;                
                else               
                    return Volume.Zero;
                
            }
        }

        /// <summary>
        /// Get the <paramref name="VolumeFlow"/> of the fluid. 
        /// </summary>
        /// <remarks>
        /// This is used when the <paramref name="MassFlow"/> of the <paramref name="Fluid"/> is set
        /// </remarks> 
        [JsonIgnore]
        public VolumeFlow VolumeFlow
        {
            get
            {
                //Calculate the volumeflow
                if (Density != Density.Zero)                
                    return MassFlow / Density;                
                else                
                    return VolumeFlow.Zero;
                
            }
        }




        /// <summary>
        /// The top limit temperature of the selected <paramref name="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Temperature T_Max { get; private set; }

        /// <summary>
        /// The lower limit temperature of the selected <paramref name="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Temperature T_Min { get; private set; }

        /// <summary>
        /// <paramref name="Temperature"/> at the critical point
        /// </summary>
        [JsonIgnore]
        public Temperature T_Crit { get; private set; }

        /// <summary>
        /// <paramref name="Enthalpy"/> at the critical point
        /// </summary>
        [JsonIgnore]
        public SpecificEnergy H_Crit { get; private set; }

        /// <summary>
        /// <paramref name="Pressure"/> at the critical point
        /// </summary>
        [JsonIgnore]
        public Pressure P_Crit { get; private set; }

        /// <summary>
        /// The lower limit <paramref name="Pressure"/> of the selected <paramref name="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Pressure P_Min { get; private set; }

        /// <summary>
        /// The top limit <paramref name="Pressure"/> of the selected <paramref name="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Pressure P_Max { get; private set; }

        /// <summary>
        /// The top limit <paramref name="FractionMax"/> of the selected <paramref name="Fluid"/>.
        /// </summary>
        [JsonProperty]
        public double FractionMax { get; private set; }

        /// <summary>
        /// The lower limit <paramref name="FractionMin"/> of the selected <paramref name="Fluid"/>.
        /// </summary>
        [JsonProperty]
        public double FractionMin { get; private set; }




        /// <summary>
        /// Used to access the CoolProp DLL.
        /// </summary>
        [JsonIgnore]
        private AbstractState REF;

        /// <summary>
        /// Get the selected fluid type that is set to the <paramref name="Fluid"/>
        /// </summary>
        /// <remarks>
        /// The full list can be seen on the <paramref name="FluidList"/>
        /// </remarks> 
        [JsonProperty]
        public MediaType Media { get; private set; }

        /// <summary>
        /// When an <paramref name="Update"/> fails this it set to <paramref name="True"/>
        /// </summary>
        [JsonProperty]
        public bool FailState { get; private set; }


        /// <summary>
        /// Set an empty <paramref name="Fluid"/> that does not have a fluid type!
        /// </summary>
        /// <remarks>
        /// <Code>
        /// <br>You would normally use this:</br>
        /// <br><paramref name="Fluid"/> Water = new <paramref name="Fluid"/>(<paramref name="FluidList"/>.Water);</br>
        /// </Code>
        /// </remarks> 
        public Fluid()
        {
        }

        /// <summary>
        /// Set an <paramref name="Fluid"/> with a fluid type!
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// </remarks> 
        [JsonConstructor]
        public Fluid(MediaType Media)
        {
            SetNewMedia(Media);
        }

        /// <summary>
        /// Set an <paramref name="Fluid"/> with a fluid type!
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// </remarks> 
        public Fluid(FluidList Type) :this(FluidListToMediaType(Type))
        {

        }


        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Density"/> and the <paramref name="Entropy"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// <br><c>Water.UpdateDS(Density.FromKilogramsPerCubicMeter(999.38), Entropy.FromJoulesPerKelvin(195.27));</c></br>
        /// </remarks> 
        /// <param name = "density" > The <paramref name="Density"/> used in the update</param>
        /// <param name = "entropy" > The <paramref name="Entropy"/> used in the update</param>
        public void UpdateDS(Density density, Entropy entropy)
        {
            CheckBeforeUpdate();

            if (density > Density.Zero && entropy > Entropy.Zero)
            {
                try
                {
                    REF.update(input_pairs.DmassSmass_INPUTS, density.KilogramsPerCubicMeter, entropy.JoulesPerKelvin);
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


        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Density"/> and the <paramref name="Pressure"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// <br><c>Water.UpdateDP(Density.FromKilogramsPerCubicMeter(999.38), Pressure.FromBars(1.013));</c></br>
        /// </remarks> 
        /// <param name = "density" > The <paramref name="Density"/> used in the update</param>
        /// <param name = "pressure" > The <paramref name="Pressure"/> used in the update</param>
        public void UpdateDP(Density density, Pressure pressure)
        {
            CheckBeforeUpdate();
            try
            {
                REF.update(input_pairs.DmassP_INPUTS, density.KilogramsPerCubicMeter, pressure.Pascals);
                UpdateValues();
            }
            catch (Exception e)
            {
                FailState = true;
                Debug.Print("Coolprop: Warning in UpdateDP" + e);
            }

        }

        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Density"/> and the <paramref name="Temperature"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// <br><c>Water.UpdateDT(Density.FromKilogramsPerCubicMeter(999.38), Temperature.FromDegreesCelsius(13));</c></br>
        /// </remarks> 
        /// <param name = "density" > The <paramref name="Density"/> used in the update</param>
        /// <param name = "temperature" > The <paramref name="Temperature"/> used in the update</param>
        public void UpdateDT(Density density, Temperature temperature)
        {
            CheckBeforeUpdate();
            try
            {
                REF.update(input_pairs.DmassT_INPUTS, density.KilogramsPerCubicMeter, temperature.Kelvins);
                UpdateValues();
            }
            catch (Exception e)
            {
                FailState = true;
                Debug.Print("Coolprop: Warning in UpdateDT" + e);
            }

        }


        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Density"/> and the <paramref name="Enthalpy"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// <br><c>Water.UpdateDH(Density.FromKilogramsPerCubicMeter(999.38), SpecificEnergy.FromJoulesPerKilogram(54697.59));</c></br>
        /// </remarks> 
        /// <param name = "density" > The <paramref name="Density"/> used in the update</param>
        /// <param name = "enthalpy" > The <paramref name="Enthalpy"/> used in the update</param>
        public void UpdateDH(Density density, SpecificEnergy enthalpy)
        {
            CheckBeforeUpdate();
            try
            {
                REF.update(input_pairs.DmassHmass_INPUTS, density.KilogramsPerCubicMeter, enthalpy.JoulesPerKilogram);
                UpdateValues();
            }
            catch (Exception e)
            {
                FailState = true;
                Debug.Print("Coolprop: Warning in UpdateDH" + e);
            }

        }

        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Pressure"/> and the <paramref name="Temperature"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// <br><c>Water.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(13));</c></br>
        /// </remarks> 
        /// <param name = "pressure" > The <paramref name="Pressure"/> used in the update</param>
        /// <param name = "temperature" > The <paramref name="Temperature"/> used in the update</param>
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


        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Quality"/> and the <paramref name="Temperature"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid CO2 = new Fluid(FluidList.CO2);</c></br>
        /// <br><c>CO2.UpdateXT(0.7, Temperature.FromDegreesCelsius(13));</c></br>
        /// </remarks> 
        /// <param name = "quality" > The <paramref name="Quality"/> used in the update</param>
        /// <param name = "temperature" > The <paramref name="Temperature"/> used in the update</param>
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

        /// <summary>
        /// Not yet supported by CoolProp!
        /// </summary>        ///
        /// <param name = "enthalpy" > The <paramref name="Enthalpy"/> used in the update</param>
        /// <param name = "temperature" > The <paramref name="Temperature"/> used in the update</param>
        private void UpdateHT(SpecificEnergy enthalpy, Temperature temperature)
        {
            //Not yet supported by CoolProp!
        }


        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Pressure"/> and the <paramref name="Entropy"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// <br><c>Water.UpdatePS(Pressure.FromBars(1.013), Entropy.FromJoulesPerKelvin(195.27));</c></br>
        /// </remarks> 
        /// <param name = "pressure" > The <paramref name="Pressure"/> used in the update</param>
        /// <param name = "entropy" > The <paramref name="Entropy"/> used in the update</param>
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

        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Pressure"/> and the <paramref name="Enthalpy"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// <br><c>Water.UpdatePH(Pressure.FromBars(1.013), SpecificEnergy.FromJoulesPerKilogram(54697.59));</c></br>
        /// </remarks> 
        /// <param name = "pressure" > The <paramref name="Pressure"/> used in the update</param>
        /// <param name = "enthalpy" > The <paramref name="Enthalpy"/> used in the update</param>
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

        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Pressure"/> and the <paramref name="Quality"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid CO2 = new Fluid(FluidList.CO2);</c></br>
        /// <br><c>CO2.UpdatePX(Pressure.FromBars(25), 0.7);</c></br>
        /// </remarks> 
        /// <param name = "pressure" > The <paramref name="Pressure"/> used in the update</param>
        /// <param name = "quality" > The <paramref name="Quality"/> used in the update</param>
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

        /// <summary>
        /// Update the condition of the <paramref name="Fluid"/> when you know the <paramref name="Enthalpy"/> and the <paramref name="Entropy"/>
        /// </summary>
        /// <remarks>
        /// <br>Exemple:</br>
        /// <br><c>Fluid Water = new Fluid(FluidList.Water);</c></br>
        /// <br><c>Water.UpdateHS(SpecificEnergy.FromJoulesPerKilogram(54697.59), Entropy.FromJoulesPerKelvin(195.27));</c></br>
        /// </remarks> 
        /// <param name = "enthalpy" > The <paramref name="Enthalpy"/> used in the update</param>
        /// <param name = "entropy" > The <paramref name="Entropy"/> used in the update</param>
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



        /// <summary>
        /// Use the selected <paramref name="Fluid"/> to return the Saturation pressure
        /// </summary>
        /// <remarks>
        /// This does not alter the state of the <paramref name="Fluid"/>
        /// </remarks> 
        ///<param name="temperature">The JSON data to read from</param>
        /// <returns>Saturation pressure</returns>
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



        /// <summary>
        /// Updates the start values of the <paramref name="Fluid"/>
        /// </summary>   
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

        /// <summary>
        /// Updates the values of the <paramref name="Fluid"/> after an update
        /// </summary>   
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
            Mass = Mass.Zero;
            Prandtl = 0;
            SurfaceTension = ForcePerLength.Zero;
            SoundSpeed = Speed.Zero;
            FailState = true;

            Viscosity = DynamicViscosity.Zero;
            Conductivity = ThermalConductivity.Zero;
            MolarMass = MolarMass.Zero;
            Compressibility = 0;
            InternalEnergy = SpecificEnergy.Zero;
        }




        /// <summary>
        /// Copy all the values from <paramref name="other"/> to this <paramref name="Fluid"/>
        /// </summary>  
        /// <param name="other"><paramref name="Fluid"/> to be copied from</param>
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
            this.P_Crit = other.P_Crit;
            this.Viscosity = other.Viscosity;
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
        /// Copy just the type of fluid from <paramref name="other"/> to this <paramref name="Fluid"/>
        /// </summary>  
        /// <param name="other"><paramref name="Fluid"/> to be copied from</param>
        public void CopyType(Fluid other)
        {
            //Null check
            if (!(other.Media is null))
            {
                //Actually changing media
                if (this.Media != other.Media)
                {
                    //Since we are changing media when the old values doesn't make sense to keep
                    ZeroValues();

                    //Set new media
                    SetNewMedia(other.Media);

                }
            }
        }

        /// <summary>
        /// Mixing <paramref name="other"/> into this <paramref name="Fluid"/>
        /// </summary> 
        /// <remarks>
        /// This makes a simple mixing based on the Massflow or the Mass 
        /// <br>Both <paramref name="Fluid"/> should use either Massflow or the Mass!</br>
        /// </remarks> 
        /// <param name="other"><paramref name="Fluid"/> to be copied from</param>
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

        /// <summary>
        /// Check if two <paramref name="Fluid"/> have almost the same <paramref name="MassFlow"/> or <paramref name="Mass"/>
        /// </summary> 
        /// <remarks>
        /// <br>Both <paramref name="Fluid"/> should use either Massflow or the Mass!</br>
        /// <br>tolerence is set to: 0.00001 [kg/s] or 0.00001 [kg]</br>
        /// </remarks>
        /// <param name="other"><paramref name="Fluid"/> to be copied from</param> 
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


        /// <summary>
        /// Set a new fluid type to the <paramref name="Fluid"/>
        /// </summary> 
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
        
        /// <summary>
        /// Set a new fluid type to the <paramref name="Fluid"/>
        /// </summary> 
        public void SetNewMedia(FluidList Type)
        {
            SetNewMedia(FluidListToMediaType(Type));
        }
        
        /// <summary>
        /// Set a new fluid type to the <paramref name="Fluid"/>
        /// </summary> 
        public void SetNewMedia(MediaType Type)
        {
            if (!(Type is null))
            {
                if (Media is null)            
                    Media = new MediaType();            
            
                Media.Copy(Type);
                REF = AbstractState.factory(Media.BackendType, Media.InternalName);
                UpdateStartValues();
            }

        }

        /// <summary>
        /// Set a mass fraction to the <paramref name="Fluid"/>
        /// </summary> 
        /// <remarks>
        /// <br>It might work but it is still in Beta!</br>
        /// </remarks>
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


        /// <summary>
        /// Add <paramref name="Power"/> to the <paramref name="Fluid"/>
        /// </summary> 
        /// <remarks>
        /// <br>This does only work when using <paramref name="MassFlow"/></br>
        /// </remarks>
        public void AddPower(Power powerToBeAdded)
        {

            //TODO If mass is selected!
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

        /// <summary>
        /// Remove <paramref name="Power"/> from the <paramref name="Fluid"/>
        /// </summary> 
        /// <remarks>
        /// <br>This does only work when using <paramref name="MassFlow"/></br>
        /// </remarks>
        public void RemovePower(Power powerToBeRemoved)
        {
            //TODO: If mass is selected 

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



        /// <summary>
        /// Checks if the two <paramref name="Fluid"/> are almost then same
        /// </summary>
        public static bool operator ==(Fluid other1, Fluid other2)
        {

            //TODO If mass is selected!

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

            //We might not have to use this anymore!

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
            //return JsonConvert.SerializeObject(this, Formatting.Indented, ReturnJSONSettings());
            return JsonConvert.SerializeObject(this);
        }

        public Fluid LoadFromJSON(string json)
        {
            //return JsonConvert.DeserializeObject<Fluid>(json, ReturnJSONSettings());
            return JsonConvert.DeserializeObject<Fluid>(json);
        }

        //Other privates function

        public bool HasValue(double value)
        {
            return !Double.IsNaN(value) && !Double.IsInfinity(value);
        }
    }

}