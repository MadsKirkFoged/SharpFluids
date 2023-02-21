
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using UnitsNet;
using EngineeringUnits;
using Serilog;

namespace SharpFluids
{
    public partial class Fluid
    {

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the <see cref="UnitsNet.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDS(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.SpecificEntropy"/>.FromJoulesPerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.SpecificEntropy"/> used in the update</param>
        public void UpdateDS(Density density, SpecificEntropy entropy)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();


            try
            {
                REF.update(input_pairs.DmassSmass_INPUTS, density.KilogramPerCubicMeter, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateDS -> CoolProp could not return your request on {density} and {entropy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdateDS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {entropy} {e}");
                throw e;
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
            GuardFromCustomFluids();
            GuardFromMixFluids();

            try
            {
                REF.update(input_pairs.DmassP_INPUTS, density.KilogramPerCubicMeter, pressure.Pascal);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateDP -> CoolProp could not return your request on {density} and {pressure} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdateDP -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {pressure} {e}");
                throw e;
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
            GuardFromCustomFluids();
            GuardFromMixFluids();

            try
            {
                REF.update(input_pairs.DmassT_INPUTS, density.KilogramPerCubicMeter, temperature.Kelvins);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateDT -> CoolProp could not return your request on {density} and {temperature} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdateDT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {temperature} {e}");
                throw e;
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
            GuardFromCustomFluids();
            GuardFromMixFluids();

            try
            {
                REF.update(input_pairs.DmassHmass_INPUTS, density.KilogramPerCubicMeter, enthalpy.JoulePerKilogram);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateDH -> CoolProp could not return your request on {density} and {enthalpy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdateDH -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {enthalpy} {e}");
                throw e;
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
        public void UpdatePT(Pressure pressure, Temperature temperature, double? RepeatTolerance = null)
        {

            CheckBeforeUpdate();
            GuardFromCustomFluids();            

            if (ShouldItBeCached(pressure, cache_pressure, RepeatTolerance) &&
               ShouldItBeCached(temperature, cache_temperature, RepeatTolerance))
            {
                CacheTemperature(temperature);
                CachePressure(pressure);
                CacheMode = true;
                return;
            }


            try
            {
                REF.update(input_pairs.PT_INPUTS, pressure.Pascal, temperature.Kelvins);
                UpdateValues();

            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdatePT -> CoolProp could not return your request on {pressure} and {temperature} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdatePT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {temperature} {e}");
                throw e;
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
        public void UpdateXT(double quality, Temperature temperature, double? RepeatTolerance = null)
        {

            CheckBeforeUpdate();
            GuardFromCustomFluids();

            if (ShouldItBeCached(temperature, cache_temperature, RepeatTolerance) &&
               ShouldItBeCached(quality, cache_quality, RepeatTolerance))
            {
                CacheQuality(quality);
                CacheTemperature(temperature);
                CacheMode = true;
                return;
            }


           
            try
            {
                //If we are above transcritical we just return the Critical point 
                if (temperature >= CriticalTemperature)
                {
                    Log.Debug($"SharpFluid -> UpdateXT -> {temperature} is above CriticalTemperature ({CriticalTemperature}) -> We will just return you the CriticalTemperature!");
                    REF.update(input_pairs.QT_INPUTS, quality, CriticalTemperature.Kelvins);
                    UpdateValues();
                    FailState = true;

                }
                else
                {
                    REF.update(input_pairs.QT_INPUTS, quality, temperature.Kelvins);
                    UpdateValues();
                }


            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateXT -> CoolProp could not return your request on {quality} and {temperature} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdateXT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {quality} and {temperature} {e}");
                throw;
            }



        }

        /// <summary>
        /// Not yet supported by CoolProp!
        /// </summary>        ///
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
        public void UpdateHT(SpecificEnergy enthalpy, Temperature temperature)
        {
            //Not yet supported by CoolProp!
            Log.Debug($"SharpFluid -> UpdateHT -> Not yet supported by CoolProp!");
            throw new NotImplementedException($"SharpFluid -> UpdateHT -> Not (yet) supported by CoolProp!");
            REF.update(input_pairs.HmassT_INPUTS, enthalpy.JoulesPerKilogram, temperature.Kelvins);
        }


        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the <see cref="UnitsNet.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePS(<see cref="UnitsNet.Pressure"/>.FromBars(1.013), <see cref="UnitsNet.SpecificEntropy"/>.FromJoulesPerKilogramKelvin(195.27));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.SpecificEntropy"/> used in the update</param>
        public void UpdatePS(Pressure pressure, SpecificEntropy entropy, double? RepeatTolerance = null)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (ShouldItBeCached(pressure, cache_pressure, RepeatTolerance) &&
                ShouldItBeCached(entropy, cache_entropy, RepeatTolerance))
            {
                CacheEntropy(entropy);
                CachePressure(pressure);
                CacheMode = true;
                return;
            }


            try
            {
                REF.update(input_pairs.PSmass_INPUTS, pressure.Pascal, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdatePS -> CoolProp could not return your request on {pressure} and {entropy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdatePS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {entropy} {e}");
                throw e;
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
        public void UpdatePH(Pressure pressure, SpecificEnergy enthalpy, double? RepeatTolerance = null)
        {
            CheckBeforeUpdate();            
            GuardFromCustomFluids();
            GuardFromMixFluids();


            if (ShouldItBeCached(pressure, cache_pressure, RepeatTolerance) &&
                ShouldItBeCached(enthalpy,cache_enthalpy, RepeatTolerance))
            {
                CacheEnthalpy(enthalpy);
                CachePressure(pressure);
                CacheMode = true;
                return;
            }


            try
            {
                REF.update(input_pairs.HmassP_INPUTS, enthalpy.JoulePerKilogram, pressure.Pascal);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdatePH -> CoolProp could not return your request on {pressure} and {enthalpy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdatePH -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {enthalpy} {e}");
                throw e;
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
        public void UpdatePX(Pressure pressure, double quality, double? RepeatTolerance = null)
        {

            CheckBeforeUpdate();
            GuardFromCustomFluids();

            if (ShouldItBeCached(pressure, cache_pressure, RepeatTolerance) &&
                ShouldItBeCached(quality, cache_quality, RepeatTolerance))
            {
                CacheQuality(quality);
                CachePressure(pressure);
                CacheMode = true;
                return;
            }


            try
            {
                if (pressure > CriticalPressure)
                {
                    UpdatePT(CriticalPressure, CriticalTemperature);
                    UpdatePH(pressure, Enthalpy);
                    Log.Debug($"SharpFluid -> UpdatePX -> {pressure} is above CriticalPressure ({CriticalPressure}) -> We will just return you the Critical point!");

                    FailState = true;
                }
                else
                {
                    REF.update(input_pairs.PQ_INPUTS, pressure.Pascal, quality);
                    UpdateValues();
                }
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdatePX -> CoolProp could not return your request on {pressure} and {quality} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdatePX -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {quality} {e}");
                throw e;
            }
        }





        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the Enthalpy and the <see cref="UnitsNet.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateHS(<see cref="UnitsNet.SpecificEnergy"/>.FromJoulesPerKilogram(54697.59), <see cref="UnitsNet.SpecificEntropy"/>.FromJoulesPerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.SpecificEntropy"/> used in the update</param>
        public void UpdateHS(SpecificEnergy enthalpy, SpecificEntropy entropy)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            try
            {
                REF.update(input_pairs.HmassSmass_INPUTS, enthalpy.JoulePerKilogram, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateHS -> CoolProp could not return your request on {enthalpy} and {entropy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdateHS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {enthalpy} and {entropy} {e}");
                throw e;
            }
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.SpecificEntropy"/> and the Temperature.<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateTS(<see cref="UnitsNet.Temperature"/>.FromKelvins(286.15), <see cref="UnitsNet.SpecificEntropy"/>.FromJoulesPerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "temperature" > The Temperature used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.SpecificEntropy"/> used in the update</param>
        public void UpdateTS(Temperature temperature, SpecificEntropy entropy)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            try
            {
                REF.update(input_pairs.SmassT_INPUTS, entropy.JoulePerKilogramKelvin, temperature.Kelvins);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateHS -> CoolProp could not return your request on {temperature} and {entropy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdateHS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {temperature} and {entropy} {e}");
                throw e;
            }
        }

        /// <summary>
        /// This is a Beta mehtod used only when looking a CustomFluids!.<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Custom_SHC228"/>);</c></br>
        /// <br><c>Water.UpdateCustomFluid(<see cref="UnitsNet.Temperature"/>.FromKelvins(286.15));</c></br>
        /// </summary> 
        /// <param name = "temperature" > The Temperature used in the update</param>
        public void UpdateCustomFluid(Pressure pressure, Temperature temperature)
        {

            //THIS IS IN BETA MODE


            CheckBeforeUpdate();


            //if (Media.BackendType != "CustomFluid")
            //{
            //    throw new NotImplementedException("This is in Beta and only works with CustomFluids!");
            //}




            //Find the two closed points for Interpolation or Extrapolation
            CustomOil Above = GetCustomFluidFromDatabase().OrderBy(p => (p.Temperature - temperature).Abs()).First();
            CustomOil Below = GetCustomFluidFromDatabase().OrderBy(p => (p.Temperature - temperature).Abs()).Skip(1).First();


            Temperature = temperature;
            Pressure = pressure;
            Density = UnitMath.LinearInterpolation(temperature, Below.Temperature, Above.Temperature, Below.Density, Above.Density);
            Cp = UnitMath.LinearInterpolation(temperature, Below.Temperature, Above.Temperature, Below.Cp, Above.Cp);
            Conductivity = UnitMath.LinearInterpolation(temperature, Below.Temperature, Above.Temperature, Below.ThermalConductivity, Above.ThermalConductivity);
            DynamicViscosity = UnitMath.LinearInterpolation(temperature, Below.Temperature, Above.Temperature, Below.KinematicViscosity, Above.KinematicViscosity) * Density;

        }


        public List<CustomOil> GetCustomFluidFromDatabase()
        {

            if (Media.InternalName == "SHC226E")
            {
                return SHC226E.GetList();
            }

            if (Media.InternalName == "SHC228")
            {
                return SHC228.GetList();
            }

            if (Media.InternalName == "SHC230")
            {
                return SHC230.GetList();
            }


            throw new NotImplementedException("GetCustomFluidFromDatabase didnt return anything");


        }

       

        private void CachePressure(Pressure pressure)
        {
            //Saving old real values
            cache_pressure = Pressure;

            //Setting input as New value
            Pressure = pressure;

        }
        private void CacheTemperature(Temperature temperature)
        {
            //Saving old real values
            cache_temperature = Temperature;

            //Setting input as New value
            Temperature = temperature;

        }
        private void CacheEnthalpy(SpecificEnergy enthalpy)
        {
            //Saving old real values
            cache_enthalpy = Enthalpy;

            //Setting input as New value
            Enthalpy = enthalpy;

        }
        private void CacheQuality(double quality)
        {
            //Saving old real values
            cache_quality = Quality;

            //Setting input as New value
            Quality = quality;

        }

        private void CacheEntropy(SpecificEntropy entropy)
        {
            //Saving old real values
            cache_entropy = Entropy;

            //Setting input as New value
            Entropy = entropy;
        }

        private bool ShouldItBeCached(UnknownUnit value, UnknownUnit cache_value, double? RepeatTolerance)
        {
            if (RepeatTolerance is null)
                return false;

            if (cache_value is null)
                return false;

            if ((value - cache_value).Abs() / value > RepeatTolerance)
                return false;


            return true;

        }


        private void GuardFromCustomFluids()
        {
            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }
        }

        private void GuardFromMixFluids()
        {
            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }
        }
    }
}

