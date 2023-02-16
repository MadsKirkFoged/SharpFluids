
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

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (density <= Density.Zero || entropy <= SpecificEntropy.Zero)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateDS -> {density} cant be below {Density.Zero} and {entropy} cant be below {SpecificEntropy.Zero}");
                return;
            }


            try
            {
                REF.update(input_pairs.DmassSmass_INPUTS, density.KilogramsPerCubicMeter, entropy.JoulesPerKilogramKelvin);
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
                throw;
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

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }


            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (density <= Density.Zero || pressure <= Pressure.Zero)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateDP -> {density} cant be below {Density.Zero} and {pressure} cant be below {Pressure.Zero}");

                return;
            }

            if (pressure > LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdateDP -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased");


            try
            {
                REF.update(input_pairs.DmassP_INPUTS, density.KilogramsPerCubicMeter, pressure.Pascals);
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
                throw;
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

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }


            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            if (density <= Density.Zero || temperature < LimitTemperatureMin)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateDT -> {density} cant be below {Density.Zero} and {temperature} cant be below {LimitTemperatureMin}");
                return;
            }

            if (temperature > LimitTemperatureMax)
                Log.Debug($"SharpFluid -> UpdateDT -> {temperature} is above 'LimitTemperatureMax' ({LimitTemperatureMax}) - This result is extrapolated hence precision is decreased");


            try
            {
                REF.update(input_pairs.DmassT_INPUTS, density.KilogramsPerCubicMeter, temperature.Kelvins);
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
                throw;
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

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }



            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            if (density <= Density.Zero)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateDH -> {density} cant be below {Density.Zero} and {enthalpy} cant be below (limit unknown)");
                return;
            }


            try
            {
                REF.update(input_pairs.DmassHmass_INPUTS, density.KilogramsPerCubicMeter, enthalpy.JoulesPerKilogram);
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
                throw;
            }

        }

         private Temperature cache_temperature;


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
            //Check if we are close to previous lookup
            if (RepeatTolerance is object)
            {
                if (cache_pressure is object && cache_temperature is object)
                {

                    if ((pressure - cache_pressure).Abs() / pressure < RepeatTolerance &&
                       (temperature - cache_temperature).Abs() / temperature < RepeatTolerance)
                    {

                        //Saving old real values
                        cache_pressure = Pressure;
                        cache_temperature = Temperature;

                        //Setting input as New value
                        Pressure = pressure;
                        Temperature = temperature;

                        return;
                    }
                }

            }


            CheckBeforeUpdate();



            if (Media.BackendType == "CustomFluid")
            {
                UpdateCustomFluid(temperature, pressure);
                return;

            }



            if (pressure < LimitPressureMin || temperature < LimitTemperatureMin)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdatePT -> {pressure} cant be below {LimitPressureMin} and {temperature} cant be below {LimitTemperatureMin}");
                return;
            }

            if (temperature > LimitTemperatureMax)
                Log.Debug($"SharpFluid -> UpdatePT -> {temperature} is above 'LimitTemperatureMax' ({LimitTemperatureMax}) - This result is extrapolated hence precision is decreased");

            if (pressure > LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdatePT -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased");



            try
            {
                REF.update(input_pairs.PT_INPUTS, pressure.Pascal, temperature.Kelvins);
                UpdateValues();

                cache_pressure = Pressure;
                cache_enthalpy = Enthalpy;
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
                throw;
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

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (temperature < LimitTemperatureMin)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdateXT -> {temperature} cant be below {LimitTemperatureMin}", temperature, LimitTemperatureMin);
                return;
            }


            if (temperature > LimitTemperatureMax)
                Log.Debug($"SharpFluid -> UpdateXT -> {temperature} is above 'LimitTemperatureMax' ({LimitTemperatureMax}) - This result is extrapolated hence precision is decreased");

            try
            {
                //If we are above transcritical we just return the Critical point 
                if (temperature >= CriticalTemperature)
                {
                    Log.Debug($"SharpFluid -> UpdateXT -> {temperature} is above CriticalTemperature ({CriticalTemperature}) -> We will just return you the CriticalTemperature!");
                    REF.update(input_pairs.QT_INPUTS, quality, CriticalTemperature.Kelvins);

                }
                else
                {
                    REF.update(input_pairs.QT_INPUTS, quality, temperature.Kelvins);
                }

                UpdateValues();
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
        public void UpdatePS(Pressure pressure, SpecificEntropy entropy)
        {
            CheckBeforeUpdate();

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }


            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (pressure < LimitPressureMin)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdatePS -> {pressure} cant be below {LimitPressureMin} and {entropy} cant be below (limit unknown)");
                return;
            }

            if (pressure > LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdatePS -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased");

            try
            {
                REF.update(input_pairs.PSmass_INPUTS, pressure.Pascals, entropy.JoulesPerKilogramKelvin);
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
                throw;
            }


        }

        private Pressure cache_pressure;

        private bool CanPressureBeCached(Pressure pressure, double? RepeatTolerance)
        {
            if (RepeatTolerance is null)
                return false;

            if (cache_pressure is null)
                return false;

            if ((pressure - cache_pressure).Abs() / pressure > RepeatTolerance)
                return false;


            return true;

        }

        private void CachePressure(Pressure pressure)
        {
            //Saving old real values
            cache_pressure = Pressure;

            //Setting input as New value
            Pressure = pressure;

        }

        private SpecificEnergy cache_enthalpy;
        private bool CanEnthalpyBeCached(SpecificEnergy enthalpy, double? RepeatTolerance)
        {
            if (RepeatTolerance is null)
                return false;

            if (cache_enthalpy is null)
                return false;

            if ((enthalpy - cache_enthalpy).Abs() / enthalpy > RepeatTolerance)
                return false;


            return true;

        }

        private void CacheEnthalpy(SpecificEnergy enthalpy)
        {
            //Saving old real values
            cache_enthalpy = Enthalpy;

            //Setting input as New value
            Enthalpy = enthalpy;

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


            if (CanPressureBeCached(pressure, RepeatTolerance) && 
                CanEnthalpyBeCached(enthalpy, RepeatTolerance))
            {
                CacheEnthalpy(enthalpy);
                CachePressure(pressure);
                return;
            }


            //if (pressure < LimitPressureMin || enthalpy <= SpecificEnergy.Zero)
            //{
            //    FailState = true;
            //    Log.Debug($"SharpFluid -> UpdatePH -> {pressure} cant be below {LimitPressureMin} and {enthalpy} cant be below {SpecificEnergy.Zero}");
            //    return;
            //}

            //if (pressure > LimitPressureMax)
            //    Log.Debug($"SharpFluid -> UpdatePH -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased");


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
                throw;
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

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (pressure < LimitPressureMin || quality < 0)
            {
                FailState = true;
                Log.Debug($"SharpFluid -> UpdatePX -> {pressure} cant be bolow {LimitPressureMin}", pressure, LimitPressureMin);
                return;
            }

            if (pressure > LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdatePX -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased");

            try
            {
                if (pressure > CriticalPressure)
                {
                    UpdatePT(CriticalPressure, CriticalTemperature);
                    UpdatePH(pressure, Enthalpy);
                    Log.Debug($"SharpFluid -> UpdatePX -> {pressure} is above CriticalPressure ({CriticalPressure}) -> We will just return you the Critical point!");

                    if (FailState)
                    {
                        SetValuesToZero();
                    }
                }
                else
                {
                    REF.update(input_pairs.PQ_INPUTS, pressure.Pascals, quality);
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
                throw;
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

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            try
            {
                REF.update(input_pairs.HmassSmass_INPUTS, enthalpy.JoulesPerKilogram, entropy.JoulesPerKilogramKelvin);
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
                throw;
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

            if (Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            try
            {
                REF.update(input_pairs.SmassT_INPUTS, entropy.JoulesPerKilogramKelvin, temperature.Kelvins);
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
                throw;
            }
        }

        /// <summary>
        /// This is a Beta mehtod used only when looking a CustomFluids!.<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Custom_SHC228"/>);</c></br>
        /// <br><c>Water.UpdateCustomFluid(<see cref="UnitsNet.Temperature"/>.FromKelvins(286.15));</c></br>
        /// </summary> 
        /// <param name = "temperature" > The Temperature used in the update</param>
        private void UpdateCustomFluid(Temperature temperature, Pressure pressure)
        {

            //THIS IS IN BETA MODE


            CheckBeforeUpdate();


            if (Media.BackendType != "CustomFluid")
            {
                throw new NotImplementedException("This is in Beta and only works with CustomFluids!");
            }


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

       

    }
}

