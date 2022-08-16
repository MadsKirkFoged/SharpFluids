
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
    public static class FluidUpdates
    {

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the <see cref="UnitsNet.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDS(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.SpecificEntropy"/>.FromJoulesPerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.SpecificEntropy"/> used in the update</param>
        public static Fluid UpdateDS(this Fluid local, Density density, SpecificEntropy entropy)
        {
            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (local.Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (density <= Density.Zero || entropy <= SpecificEntropy.Zero)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateDS -> {density} cant be below {Density.Zero} and {entropy} cant be below {SpecificEntropy.Zero}");
                return local;
            }


            try
            {
                local.REF.update(input_pairs.DmassSmass_INPUTS, density.KilogramsPerCubicMeter, entropy.JoulesPerKilogramKelvin);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateDS -> CoolProp could not return your request on {density} and {entropy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdateDS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {entropy} {e}");
                throw;
            }


            return local;

        }


        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the <see cref="UnitsNet.Pressure"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDP(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.Pressure"/>.FromBars(1.013));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        public static Fluid UpdateDP(this Fluid local, Density density, Pressure pressure)
        {
            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }


            if (local.Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (density <= Density.Zero || pressure <= Pressure.Zero)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateDP -> {density} cant be below {Density.Zero} and {pressure} cant be below {Pressure.Zero}");

                return local;
            }

            if (pressure > local.LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdateDP -> {pressure} is above 'LimitPressureMax' ({local.LimitPressureMax}) - This result is extrapolated hence precision is decreased");


            try
            {
                local.REF.update(input_pairs.DmassP_INPUTS, density.KilogramsPerCubicMeter, pressure.Pascals);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateDP -> CoolProp could not return your request on {density} and {pressure} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdateDP -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {pressure} {e}");
                throw;
            }
            return local;

        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the <see cref="UnitsNet.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDT(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.Temperature"/>.FromDegreesCelsius(13));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
        public static Fluid UpdateDT(this Fluid local, Density density, Temperature temperature)
        {
            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }


            if (local.Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            if (density <= Density.Zero || temperature < local.LimitTemperatureMin)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateDT -> {density} cant be below {Density.Zero} and {temperature} cant be below {local.LimitTemperatureMin}");
                return local;
            }

            if (temperature > local.LimitTemperatureMax)
                Log.Debug($"SharpFluid -> UpdateDT -> {temperature} is above 'LimitTemperatureMax' ({local.LimitTemperatureMax}) - This result is extrapolated hence precision is decreased");


            try
            {
                local.REF.update(input_pairs.DmassT_INPUTS, density.KilogramsPerCubicMeter, temperature.Kelvins);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateDT -> CoolProp could not return your request on {density} and {temperature} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdateDT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {temperature} {e}");
                throw;
            }
            return local;

        }


        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Density"/> and the Enthalpy<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDH(<see cref="UnitsNet.Density"/>.FromKilogramsPerCubicMeter(999.38), <see cref="UnitsNet.SpecificEnergy"/>.FromJoulesPerKilogram(54697.59));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="UnitsNet.Density"/> used in the update</param>
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        public static Fluid UpdateDH(this Fluid local, Density density, SpecificEnergy enthalpy)
        {
            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }



            if (local.Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            if (density <= Density.Zero)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateDH -> {density} cant be below {Density.Zero} and {enthalpy} cant be below (limit unknown)");
                return local;
            }


            try
            {
                local.REF.update(input_pairs.DmassHmass_INPUTS, density.KilogramsPerCubicMeter, enthalpy.JoulesPerKilogram);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateDH -> CoolProp could not return your request on {density} and {enthalpy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdateDH -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {enthalpy} {e}");
                throw;
            }
            return local;

        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the <see cref="UnitsNet.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePT(<see cref="UnitsNet.Pressure"/>.FromBars(1.013), <see cref="UnitsNet.Temperature"/>.FromDegreesCelsius(13));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
        public static Fluid UpdatePT(this Fluid local, Pressure pressure, Temperature temperature)
        {
            local.CheckBeforeUpdate();



            if (local.Media.BackendType == "CustomFluid")
            {
                local.UpdateCustomFluid(temperature, pressure);
                return local;

            }



            if (pressure < local.LimitPressureMin || temperature < local.LimitTemperatureMin)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdatePT -> {pressure} cant be below {local.LimitPressureMin} and {temperature} cant be below {local.LimitTemperatureMin}");
                return local;
            }

            if (temperature > local.LimitTemperatureMax)
                Log.Debug($"SharpFluid -> UpdatePT -> {temperature} is above 'LimitTemperatureMax' ({local.LimitTemperatureMax}) - This result is extrapolated hence precision is decreased");

            if (pressure > local.LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdatePT -> {pressure} is above 'LimitPressureMax' ({local.LimitPressureMax}) - This result is extrapolated hence precision is decreased");



            try
            {
                local.REF.update(input_pairs.PT_INPUTS, pressure.Pascals, temperature.Kelvins);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdatePT -> CoolProp could not return your request on {pressure} and {temperature} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdatePT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {temperature} {e}");
                throw;
            }
            return local;
        }


        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the Quality and the <see cref="UnitsNet.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> CO2 = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.CO2"/>);</c></br>
        /// <br><c>CO2.UpdateXT(0.7, <see cref="UnitsNet.Temperature"/>.FromDegreesCelsius(13));</c></br>
        /// </summary>
        /// <param name = "quality" > The Quality used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
        public static Fluid UpdateXT(this Fluid local, double quality, Temperature temperature)
        {

            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (temperature < local.LimitTemperatureMin)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateXT -> {temperature} cant be below {local.LimitTemperatureMin}", temperature, local.LimitTemperatureMin);
                return local;
            }


            if (temperature > local.LimitTemperatureMax)
                Log.Debug($"SharpFluid -> UpdateXT -> {temperature} is above 'LimitTemperatureMax' ({local.LimitTemperatureMax}) - This result is extrapolated hence precision is decreased");

            try
            {
                //If we are above transcritical we just return the Critical point 
                if (temperature >= local.CriticalTemperature)
                {
                    Log.Debug($"SharpFluid -> UpdateXT -> {temperature} is above CriticalTemperature ({local.CriticalTemperature}) -> We will just return you the CriticalTemperature!");
                    local.REF.update(input_pairs.QT_INPUTS, quality, local.CriticalTemperature.Kelvins);

                }
                else
                {
                    local.REF.update(input_pairs.QT_INPUTS, quality, temperature.Kelvins);
                }

                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateXT -> CoolProp could not return your request on {quality} and {temperature} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdateXT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {quality} and {temperature} {e}");
                throw;
            }

            return local;



        }

        /// <summary>
        /// Not yet supported by CoolProp!
        /// </summary>        ///
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        /// <param name = "temperature" > The <see cref="UnitsNet.Temperature"/> used in the update</param>
        public static Fluid UpdateHT(this Fluid local, SpecificEnergy enthalpy, Temperature temperature)
        {
            //Not yet supported by CoolProp!
            Log.Debug($"SharpFluid -> UpdateHT -> Not yet supported by CoolProp!");
            throw new NotImplementedException($"SharpFluid -> UpdateHT -> Not (yet) supported by CoolProp!");
            local.REF.update(input_pairs.HmassT_INPUTS, enthalpy.JoulesPerKilogram, temperature.Kelvins);
        }


        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the <see cref="UnitsNet.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePS(<see cref="UnitsNet.Pressure"/>.FromBars(1.013), <see cref="UnitsNet.SpecificEntropy"/>.FromJoulesPerKilogramKelvin(195.27));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.SpecificEntropy"/> used in the update</param>
        public static Fluid UpdatePS(this Fluid local, Pressure pressure, SpecificEntropy entropy)
        {
            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }


            if (local.Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (pressure < local.LimitPressureMin)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdatePS -> {pressure} cant be below {local.LimitPressureMin} and {entropy} cant be below (limit unknown)");
                return local;
            }

            if (pressure > local.LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdatePS -> {pressure} is above 'LimitPressureMax' ({local.LimitPressureMax}) - This result is extrapolated hence precision is decreased");

            try
            {
                local.REF.update(input_pairs.PSmass_INPUTS, pressure.Pascals, entropy.JoulesPerKilogramKelvin);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdatePS -> CoolProp could not return your request on {pressure} and {entropy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdatePS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {entropy} {e}");
                throw;
            }
            return local;


        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the Enthalpy<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePH(<see cref="UnitsNet.Pressure"/>.FromBars(1.013), <see cref="UnitsNet.SpecificEnergy"/>.FromJoulesPerKilogram(54697.59));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        public static Fluid UpdatePH(this Fluid local, Pressure pressure, SpecificEnergy enthalpy)
        {
            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }


            if (local.Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (pressure < local.LimitPressureMin || enthalpy <= SpecificEnergy.Zero)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdatePH -> {pressure} cant be below {local.LimitPressureMin} and {enthalpy} cant be below {SpecificEnergy.Zero}");
                return local;
            }

            if (pressure > local.LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdatePH -> {pressure} is above 'LimitPressureMax' ({local.LimitPressureMax}) - This result is extrapolated hence precision is decreased");


            try
            {
                local.REF.update(input_pairs.HmassP_INPUTS, enthalpy.JoulesPerKilogram, pressure.Pascals);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdatePH -> CoolProp could not return your request on {pressure} and {enthalpy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdatePH -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {enthalpy} {e}");
                throw;
            }
            return local;
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.Pressure"/> and the Quality<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> CO2 = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.CO2"/>);</c></br>
        /// <br><c>CO2.UpdatePX(<see cref="UnitsNet.Pressure"/>.FromBars(25), 0.7);</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="UnitsNet.Pressure"/> used in the update</param>
        /// <param name = "quality" > The Quality used in the update</param>
        public static Fluid UpdatePX(this Fluid local, Pressure pressure, double quality)
        {

            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (pressure < local.LimitPressureMin || quality < 0)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdatePX -> {pressure} cant be bolow {local.LimitPressureMin}", pressure, local.LimitPressureMin);
                return local;
            }

            if (pressure > local.LimitPressureMax)
                Log.Debug($"SharpFluid -> UpdatePX -> {pressure} is above 'LimitPressureMax' ({local.LimitPressureMax}) - This result is extrapolated hence precision is decreased");

            try
            {
                if (pressure > local.CriticalPressure)
                {
                    local.UpdatePT(local.CriticalPressure, local.CriticalTemperature);
                    local.UpdatePH(pressure, local.Enthalpy);
                    Log.Debug($"SharpFluid -> UpdatePX -> {pressure} is above CriticalPressure ({local.CriticalPressure}) -> We will just return you the Critical point!");

                    if (local.FailState)
                    {
                        local.SetValuesToZero();
                    }
                }
                else
                {
                    local.REF.update(input_pairs.PQ_INPUTS, pressure.Pascals, quality);
                    local.UpdateValues();
                }
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdatePX -> CoolProp could not return your request on {pressure} and {quality} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdatePX -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {quality} {e}");
                throw;
            }
            return local;
        }





        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the Enthalpy and the <see cref="UnitsNet.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateHS(<see cref="UnitsNet.SpecificEnergy"/>.FromJoulesPerKilogram(54697.59), <see cref="UnitsNet.SpecificEntropy"/>.FromJoulesPerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.SpecificEntropy"/> used in the update</param>
        public static Fluid UpdateHS(this Fluid local, SpecificEnergy enthalpy, SpecificEntropy entropy)
        {
            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (local.Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            try
            {
                local.REF.update(input_pairs.HmassSmass_INPUTS, enthalpy.JoulesPerKilogram, entropy.JoulesPerKilogramKelvin);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateHS -> CoolProp could not return your request on {enthalpy} and {entropy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdateHS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {enthalpy} and {entropy} {e}");
                throw;
            }
            return local;
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="UnitsNet.SpecificEntropy"/> and the Temperature.<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateTS(<see cref="UnitsNet.Temperature"/>.FromKelvins(286.15), <see cref="UnitsNet.SpecificEntropy"/>.FromJoulesPerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "temperature" > The Temperature used in the update</param>
        /// <param name = "entropy" > The <see cref="UnitsNet.SpecificEntropy"/> used in the update</param>
        public static Fluid UpdateTS(this Fluid local, Temperature temperature, SpecificEntropy entropy)
        {
            local.CheckBeforeUpdate();

            if (local.Media.BackendType == "CustomFluid")
            {
                throw new NotImplementedException("CustomFluid only works with UpdatePT()");
            }

            if (local.Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            try
            {
                local.REF.update(input_pairs.SmassT_INPUTS, entropy.JoulesPerKilogramKelvin, temperature.Kelvins);
                local.UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                local.FailState = true;
                Log.Debug($"SharpFluid -> UpdateHS -> CoolProp could not return your request on {temperature} and {entropy} and returns the followering error: {e}");
            }
            catch (System.Exception e)
            {
                local.FailState = true;
                Log.Error($"SharpFluid -> UpdateHS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {temperature} and {entropy} {e}");
                throw;
            }

            return local;
        }

        /// <summary>
        /// This is a Beta mehtod used only when looking a CustomFluids!.<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Custom_SHC228"/>);</c></br>
        /// <br><c>Water.UpdateCustomFluid(<see cref="UnitsNet.Temperature"/>.FromKelvins(286.15));</c></br>
        /// </summary> 
        /// <param name = "temperature" > The Temperature used in the update</param>
        private static Fluid UpdateCustomFluid(this Fluid local, Temperature temperature, Pressure pressure)
        {

            //THIS IS IN BETA MODE


            local.CheckBeforeUpdate();


            if (local.Media.BackendType != "CustomFluid")
            {
                throw new NotImplementedException("This is in Beta and only works with CustomFluids!");
            }



            CustomOil Above = local.GetCustomFluidFromDatabase().FindAll(x => x.Temperature >= temperature).OrderBy(p => p.Temperature).First();
            CustomOil Below = local.GetCustomFluidFromDatabase().FindAll(x => x.Temperature <= temperature).OrderByDescending(p => p.Temperature).First();


            local.Temperature = temperature;
            local.Pressure = pressure;


            local.Density = UnitMath.LinearInterpolation(temperature, Below.Temperature, Above.Temperature, Below.Density, Above.Density);


            local.Cp = UnitMath.LinearInterpolation(temperature, Below.Temperature, Above.Temperature, Below.Cp, Above.Cp);

            local.Conductivity = UnitMath.LinearInterpolation(temperature, Below.Temperature, Above.Temperature, Below.ThermalConductivity, Above.ThermalConductivity);

            local.DynamicViscosity = UnitMath.LinearInterpolation(temperature, Below.Temperature, Above.Temperature, Below.KinematicViscosity, Above.KinematicViscosity) * local.Density;
            return local;

        }


        public static List<CustomOil> GetCustomFluidFromDatabase(this Fluid local)
        {

            if (local.Media.InternalName == "SHC226E")
            {
                return SHC226E.GetList();
            }

            if (local.Media.InternalName == "SHC228")
            {
                return SHC228.GetList();
            }

            if (local.Media.InternalName == "SHC230")
            {
                return SHC230.GetList();
            }


            throw new NotImplementedException("GetCustomFluidFromDatabase didnt return anything");


        }



    }
}

