using System;
using System.Diagnostics;
using UnitsNet;
using UnitsNet.Serialization.JsonNet;
using Newtonsoft.Json;
using JsonNet.ContractResolvers;

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


    public class Fluid
    {

        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        private Mass _mass;
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        private MassFlow _massflow;

        /// <summary>
        /// Get the <see cref="UnitsNet.Temperature"/> of the <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Temperature Temperature { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.Pressure"/> of the <see cref="Fluid"/>.
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Pressure Pressure { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.SpecificEnergy"/> of the <see cref="Fluid"/>. 
        /// <br> <see cref="UnitsNet.SpecificEnergy"/> is also called Enthalpy or H </br>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEnergy Enthalpy { get; private set; } //Also called Enthalpy 


        /// <summary>
        /// Get the <see cref="UnitsNet.Entropy"/> of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "S".</br>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Entropy Entropy { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.Density"/> of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "RHO".</br>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Density Density { get; private set; }


        /// <summary>
        /// Get the <see cref="UnitsNet.DynamicViscosity"/> of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public DynamicViscosity DynamicViscosity { get; private set; }

        /// <summary>
        /// Get the <see cref="UnitsNet.ThermalConductivity"/> of the <see cref="Fluid"/>. 
        /// <br> <see cref="UnitsNet.ThermalConductivity"/> is also just called Conductivity</br>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public ThermalConductivity Conductivity { get; private set; }

        /// <summary>
        /// Get the Cp of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "Heat capacity at constant pressure".</br>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEntropy Cp { get; private set; }

        /// <summary>
        /// Get the Cv of the <see cref="Fluid"/>. 
        /// <br>Engineers may refers to this as "Heat capacity at constant volume".</br>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public SpecificEntropy Cv { get; private set; }

        /// <summary>
        /// Get the <see cref="UnitsNet.Speed"/> of Sound of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public Speed SoundSpeed { get; private set; }

        /// <summary>
        /// Get the Surface Tension of the <see cref="Fluid"/>. 
        /// <br>This is only valid when the <see cref="Fluid"/> is a mixture of gas and liquid!</br>
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public ForcePerLength SurfaceTension { get; private set; }

        /// <summary>
        /// Get the <see cref="UnitsNet.MolarMass"/> of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
        public MolarMass MolarMass { get; private set; }

        /// <summary>
        /// Get the Internal Energy of the <see cref="Fluid"/>. 
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnitsNetIQuantityJsonConverter))]
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
        /// Set the <see cref="UnitsNet.Mass"/> of the <see cref="Fluid"/>. 
        /// <br> The <see cref="UnitsNet.Mass"/> is not changed inside the library - It will also remain at the value the user has set it to </br>
        /// <br>You can choose between using <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/> when defining a fluid.</br>
        /// <br>Once the <see cref="UnitsNet.Mass"/> is set you cannot set the <see cref="UnitsNet.MassFlow"/> anymore.</br>
        /// </summary>
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
        /// Get the Saturation temperature of the <see cref="Fluid"/>. 
        /// <br>Beware: If you are above the critical pressure of the <see cref="Fluid"/> this will just return the Saturation temperature AT the critical pressure!</br>
        /// </summary>
        [JsonIgnore]
        public Temperature Tsat
        {

            get
            {

                if (Pressure >= LimitPressureMin && Pressure <= CriticalPressure && LimitPressureMin != CriticalPressure) //P_Min != P_Crit is a quick fix - We should create a check to see if the object has beed created correctly!
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
                else if (Pressure > CriticalPressure)
                {
                    try
                    {
                        REF.update(input_pairs.HmassP_INPUTS, CriticalEnthalpy.JoulesPerKilogram, Pressure.Pascals);
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
        /// Get the <see cref="UnitsNet.Volume"/> of the <see cref="Fluid"/>. 
        /// <br>This can ONLY be used when the <see cref="UnitsNet.Mass"/> of the <see cref="Fluid"/> is set</br>
        /// </summary>
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
        /// Get the <see cref="UnitsNet.VolumeFlow"/> of the fluid. 
        /// <br>This can ONLY be used when the <see cref="UnitsNet.MassFlow"/> of the <see cref="Fluid"/> is set</br>
        /// </summary>
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
        /// This library's maximum <see cref="UnitsNet.Temperature"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Temperature LimitTemperatureMax { get; private set; }

        /// <summary>
        /// This library's minimum <see cref="UnitsNet.Temperature"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Temperature LimitTemperatureMin { get; private set; }

        /// <summary>
        /// <see cref="UnitsNet.Temperature"/> at the critical point for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Temperature CriticalTemperature { get; private set; }

        /// <summary>
        /// Enthalpy at the critical point for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public SpecificEnergy CriticalEnthalpy { get; private set; }

        /// <summary>
        /// <see cref="UnitsNet.Pressure"/> at the critical point for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Pressure CriticalPressure { get; private set; }

        /// <summary>
        /// This library's minimum <see cref="UnitsNet.Pressure"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonIgnore]
        public Pressure LimitPressureMin { get; private set; }

        /// <summary>
        /// This library's maximum <see cref="UnitsNet.Pressure"/> for the selected <see cref="Fluid"/>.
        /// </summary>
        [JsonIgnore]
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
        [JsonIgnore]
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


        /// <summary>
        /// Create an empty <see cref="Fluid"/> that does not have a fluid type!
        /// <br>You would normally use this:</br><br></br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList"/>.Water);</c></br>
        /// </summary>
        public Fluid()
        {
        }

        /// <summary>
        /// Create a <see cref="Fluid"/> with a fluid type!
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList"/>.Water);</c></br>
        /// </summary>
        [JsonConstructor]
        public Fluid(MediaType Media)
        {
            SetNewMedia(Media);
        }

        /// <summary>
        /// Create an new <see cref="Fluid"/> with a fluid type!
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList"/>.Water);</c></br>
        /// </summary>
        public Fluid(FluidList Type) :this(FluidListToMediaType(Type))
        {

        }


        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the <see cref="UnitsNet.Entropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDS(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.Entropy"/>.FromJoulesPerKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.Entropy"/> used in the update</param>
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
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the <see cref="UnitsNet.Pressure"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDP(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.Pressure"/>.FromBars(1.013));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
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
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the <see cref="UnitsNet.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDT(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.Temperature"/>.FromDegreesCelsius(13));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
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
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the Enthalpy<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDH(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.SpecificEnergy"/>.FromJoulesPerKilogram(54697.59));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
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
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the <see cref="UnitsNet.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePT(<see cref="UnitsNet.Pressure"/>.FromBars(1.013), <see cref="UnitsNet.Temperature"/>.FromDegreesCelsius(13));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
        public void UpdatePT(Pressure pressure, Temperature temperature)
        {
            CheckBeforeUpdate();
            if (pressure > LimitPressureMin && temperature > LimitTemperatureMin)
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
        /// Update the condition of the <see cref="Fluid"/> when you know the Quality and the <see cref="UnitsNet.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> CO2 = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.CO2"/>);</c></br>
        /// <br><c>CO2.UpdateXT(0.7, <see cref="UnitsNet.Temperature"/>.FromDegreesCelsius(13));</c></br>
        /// </summary>
        /// <param name = "quality" > The Quality used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
        public void UpdateXT(double quality, Temperature temperature)
        {

            CheckBeforeUpdate();
            if (temperature >= LimitTemperatureMin)
            {
                try
                {
                    //If we are above transcritical
                    if (temperature >= CriticalTemperature)
                    {
                        UpdatePT(CriticalPressure, CriticalTemperature);
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
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
        private void UpdateHT(SpecificEnergy enthalpy, Temperature temperature)
        {
            //Not yet supported by CoolProp!
            throw new NotImplementedException("Not yet supported by CoolProp");
        }


        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the <see cref="UnitsNet.Entropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePS(<see cref="UnitsNet.Pressure"/>.FromBars(1.013), <see cref="UnitsNet.Entropy"/>.FromJoulesPerKelvin(195.27));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.Entropy"/> used in the update</param>
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
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the Enthalpy<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePH(<see cref="UnitsNet.Pressure"/>.FromBars(1.013), <see cref="UnitsNet.SpecificEnergy"/>.FromJoulesPerKilogram(54697.59));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
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
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the Quality<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> CO2 = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.CO2"/>);</c></br>
        /// <br><c>CO2.UpdatePX(<see cref="UnitsNet.Pressure"/>.FromBars(25), 0.7);</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "quality" > The Quality used in the update</param>
        public void UpdatePX(Pressure pressure, double quality)
        {

            CheckBeforeUpdate();
            if (pressure > Pressure.Zero && quality >= 0)
            {

                try
                {
                    if (pressure > CriticalPressure)
                    {
                        UpdatePT(CriticalPressure, CriticalTemperature);
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
        /// Update the condition of the <see cref="Fluid"/> when you know the Enthalpy and the <see cref="UnitsNet.Entropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateHS(<see cref="UnitsNet.SpecificEnergy"/>.FromJoulesPerKilogram(54697.59), <see cref="UnitsNet.Entropy"/>.FromJoulesPerKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.Entropy"/> used in the update</param>
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
        /// Get the Saturation pressure of <see cref="Fluid"/>
        /// <br>This does not alter the condition of the <see cref="Fluid"/></br>
        /// </summary> 
        /// <returns>Saturation pressure</returns>
        ///<param name="temperature"> <see cref="UnitsNet.Temperature"/> </param>
        public Pressure UpdateP_sat(Temperature temperature)
        {
            CheckBeforeUpdate();
            if (temperature >= LimitTemperatureMin)
            {
                try
                {
                    //If we are above transcritical
                    if (temperature >= CriticalTemperature)
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
        /// Updates the constants of the <see cref="Fluid"/>
        /// <br><see cref="Fluid"/> constants are Limits and Criticalpoint values</br>
        /// </summary>   
        protected virtual void UpdateFluidConstants()
        {
            FailState = true;

            //Setting the constant values up
            LimitTemperatureMax = Temperature.FromKelvins(REF.Tmax());
            LimitTemperatureMin = Temperature.FromKelvins(REF.Tmin());


            if (REF.backend_name() == "HelmholtzEOSBackend")
            {
                CriticalTemperature = Temperature.FromKelvins(REF.T_critical());
                CriticalPressure = Pressure.FromPascals(REF.p_critical());
                LimitPressureMin = Pressure.FromPascals(REF.p_triple());
                LimitPressureMax = Pressure.FromPascals(REF.pmax());

                //Finding H_crit
                REF.update(input_pairs.PT_INPUTS, CriticalPressure.Pascals, CriticalTemperature.Kelvins);
                CriticalEnthalpy = SpecificEnergy.FromJoulesPerKilogram(REF.hmass());

            }



            //Setting Max/min Mass- or volfraction


        }

        /// <summary>
        /// Updates the values of the <see cref="Fluid"/> after an update
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
            DynamicViscosity = DynamicViscosity.FromPascalSeconds(REF.viscosity());
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
            if (RefType.ToLower() != REF?.name().ToLower() && RefType != "")
            {
                Debug.Print(REF.name());
                REF = AbstractState.factory("HEOS", RefType);
                UpdateFluidConstants();

                Debug.Print(REF.name());
            }
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
            if (!(Type is null))
            {
                if (Media is null)            
                    Media = new MediaType();            
            
                Media.Copy(Type);
                REF = AbstractState.factory(Media.BackendType, Media.InternalName);
                UpdateFluidConstants();
            }

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

            //TODO I think this just be done in the UpdateValues!

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
        /// Checks if the two <see cref="Fluid"/> are almost then same
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

        /// <summary>
        /// Not in use anymore
        /// </summary>
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