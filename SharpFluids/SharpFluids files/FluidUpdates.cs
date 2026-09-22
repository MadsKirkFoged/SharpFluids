
//using EngineeringUnits;
using EngineeringUnits;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpFluids
{
    public partial class Fluid
    {

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.Density"/> and the <see cref="EngineeringUnits.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDS(<see cref="EngineeringUnits.Density"/>.FromKilogramPerCubicMeter(999.38), <see cref="EngineeringUnits.SpecificEntropy"/>.FromJoulePerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "density" > The <see cref="EngineeringUnits.Density"/> used in the update</param>
        /// <param name = "entropy" > The <see cref="EngineeringUnits.SpecificEntropy"/> used in the update</param>
        public virtual void UpdateDS(Density? density, SpecificEntropy? entropy)
        {
            if (density is null || entropy is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.DmassSmass_INPUTS, density.KilogramPerCubicMeter, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }, nameof(UpdateDS), $"{density} and {entropy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.Density"/> and the <see cref="EngineeringUnits.Pressure"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDP(<see cref="EngineeringUnits.Density"/>.FromKilogramPerCubicMeter(999.38), <see cref="EngineeringUnits.Pressure"/>.FromBar(1.013));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="EngineeringUnits.Density"/> used in the update</param>
        /// <param name = "pressure" > The <see cref="EngineeringUnits.Pressure"/> used in the update</param>
        public virtual void UpdateDP(Density? density, Pressure? pressure)
        {
            if (density is null || pressure is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.DmassP_INPUTS, density.KilogramPerCubicMeter, pressure.Pascal);
                UpdateValues();
            }, nameof(UpdateDP), $"{density} and {pressure}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.Density"/> and the <see cref="EngineeringUnits.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDT(<see cref="EngineeringUnits.Density"/>.FromKilogramPerCubicMeter(999.38), <see cref="EngineeringUnits.Temperature"/>.FromDegreeCelsius(13));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="EngineeringUnits.Density"/> used in the update</param>
        /// <param name = "temperature" > The <see cref="EngineeringUnits.Temperature"/> used in the update</param>
        public virtual void UpdateDT(Density? density, Temperature? temperature)
        {
            if (density is null || temperature is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.DmassT_INPUTS, density.KilogramPerCubicMeter, temperature.Kelvins);
                UpdateValues();
            }, nameof(UpdateDT), $"{density} and {temperature}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.Density"/> and the Enthalpy<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateDH(<see cref="EngineeringUnits.Density"/>.FromKilogramPerCubicMeter(999.38), <see cref="EngineeringUnits.SpecificEnergy"/>.FromJoulePerKilogram(54697.59));</c></br>
        /// </summary>
        /// <param name = "density" > The <see cref="EngineeringUnits.Density"/> used in the update</param>
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        public virtual void UpdateDH(Density? density, SpecificEnergy? enthalpy)
        {
            if (density is null || enthalpy is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.DmassHmass_INPUTS, density.KilogramPerCubicMeter, enthalpy.JoulePerKilogram);
                UpdateValues();
            }, nameof(UpdateDH), $"{density} and {enthalpy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.Pressure"/> and the <see cref="EngineeringUnits.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePT(<see cref="EngineeringUnits.Pressure"/>.FromBar(1.013), <see cref="EngineeringUnits.Temperature"/>.FromDegreeCelsius(13));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="EngineeringUnits.Pressure"/> used in the update</param>
        /// <param name = "temperature" > The <see cref="EngineeringUnits.Temperature"/> used in the update</param>
        public virtual void UpdatePT(Pressure? pressure, Temperature? temperature, Ratio? RepeatTolerance = null)
        {
            if (pressure is null || temperature is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();

            try
            {
                REF.update(input_pairs.PT_INPUTS, pressure.Pascal, temperature.Kelvins);
                UpdateValues();

            }
            catch (System.ApplicationException e)
            {

                if (e.Message.StartsWith("options.p is not valid in saturation_T_pure_1D_P for T") && temperature < Temperature.FromSI(405.7) && temperature > Temperature.FromSI(405.3))
                {
                    Log.Warning($"This is a known coolprop error");

                    UpdatePT(pressure, Temperature.FromSI(405.3));
                }
                else
                {
                    FailState = true;
                    Log.Warning($"SharpFluid -> UpdatePT -> CoolProp could not return your request on {pressure} and {temperature} and returns the followering error: {e}");

                }

            }
            catch (System.Exception e)
            {
                FailState = true;
                Log.Error($"SharpFluid -> UpdatePT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {temperature} {e}");
            }
            finally
            {
                ResetErrors();
            }
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the Quality and the <see cref="EngineeringUnits.Temperature"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> CO2 = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.CO2"/>);</c></br>
        /// <br><c>CO2.UpdateXT(0.7, <see cref="EngineeringUnits.Temperature"/>.FromDegreeCelsius(13));</c></br>
        /// </summary>
        /// <param name = "quality" > The Quality used in the update</param>
        /// <param name = "temperature" > The <see cref="EngineeringUnits.Temperature"/> used in the update</param>
        public virtual void UpdateXT(double quality, Temperature? temperature, double? RepeatTolerance = null)
        {
            if (temperature is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();

            ExecuteUpdate(() =>
            {
                //If we are above transcritical we just return the Critical point
                if (temperature >= CriticalTemperature)
                {
                    Log.Warning($"SharpFluid -> UpdateXT -> {temperature} is above CriticalTemperature ({CriticalTemperature}) -> We will just return you the CriticalTemperature!");
                    REF.update(input_pairs.QT_INPUTS, quality, CriticalTemperature.Kelvins);
                    UpdateValues();
                    FailState = true;
                }
                else
                {
                    REF.update(input_pairs.QT_INPUTS, quality, temperature.Kelvins);
                    UpdateValues();
                }
            }, nameof(UpdateXT), $"{quality} and {temperature}", rethrowUnexpected: true);
        }

        /// <summary>
        /// Not yet supported by CoolProp!
        /// </summary>        ///
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        /// <param name = "temperature" > The <see cref="EngineeringUnits.Temperature"/> used in the update</param>
        public virtual void UpdateHT(SpecificEnergy? enthalpy, Temperature? temperature)
        {
            //Not yet supported by CoolProp!
            Log.Debug($"SharpFluid -> UpdateHT -> Not yet supported by CoolProp!");
            throw new NotImplementedException($"SharpFluid -> UpdateHT -> Not (yet) supported by CoolProp!");
            //REF.update(input_pairs.HmassT_INPUTS, enthalpy.JoulePerKilogram, temperature.Kelvins);
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.Pressure"/> and the <see cref="EngineeringUnits.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePS(<see cref="EngineeringUnits.Pressure"/>.FromBar(1.013), <see cref="EngineeringUnits.SpecificEntropy"/>.FromJoulePerKilogramKelvin(195.27));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="EngineeringUnits.Pressure"/> used in the update</param>
        /// <param name = "entropy" > The <see cref="EngineeringUnits.SpecificEntropy"/> used in the update</param>
        public virtual void UpdatePS(Pressure? pressure, SpecificEntropy? entropy, Ratio? RepeatTolerance = null)
        {
            if (pressure is null || entropy is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.PSmass_INPUTS, pressure.Pascal, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }, nameof(UpdatePS), $"{pressure} and {entropy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.Pressure"/> and the Enthalpy<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdatePH(<see cref="EngineeringUnits.Pressure"/>.FromBar(1.013), <see cref="EngineeringUnits.SpecificEnergy"/>.FromJoulePerKilogram(54697.59));</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="EngineeringUnits.Pressure"/> used in the update</param>
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        public virtual void UpdatePH(Pressure? pressure, SpecificEnergy? enthalpy, Ratio? RepeatTolerance = null)
        {
            if (pressure is null || enthalpy is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.HmassP_INPUTS, enthalpy.JoulePerKilogram, pressure.Pascal);
                UpdateValues();
            }, nameof(UpdatePH), $"{pressure} and {enthalpy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.Pressure"/> and the Quality<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> CO2 = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.CO2"/>);</c></br>
        /// <br><c>CO2.UpdatePX(<see cref="EngineeringUnits.Pressure"/>.FromBar(25), 0.7);</c></br>
        /// </summary>
        /// <param name = "pressure" > The <see cref="EngineeringUnits.Pressure"/> used in the update</param>
        /// <param name = "quality" > The Quality used in the update</param>
        public virtual void UpdatePX(Pressure? pressure, double quality, double? RepeatTolerance = null)
        {
            if (pressure is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();

            ExecuteUpdate(() =>
            {
                if (pressure > CriticalPressure)
                {
                    UpdatePT(CriticalPressure, CriticalTemperature);
                    UpdatePH(pressure, Enthalpy);
                    Log.Warning($"SharpFluid -> UpdatePX -> {pressure} is above CriticalPressure ({CriticalPressure}) -> We will just return you the Critical point!");

                    FailState = true;
                }
                else
                {
                    REF.update(input_pairs.PQ_INPUTS, pressure.Pascal, quality);
                    UpdateValues();
                }
            }, nameof(UpdatePX), $"{pressure} and {quality}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the Enthalpy and the <see cref="EngineeringUnits.SpecificEntropy"/><br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateHS(<see cref="EngineeringUnits.SpecificEnergy"/>.FromJoulePerKilogram(54697.59), <see cref="EngineeringUnits.SpecificEntropy"/>.FromJoulePerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "enthalpy" > The Enthalpy used in the update</param>
        /// <param name = "entropy" > The <see cref="EngineeringUnits.SpecificEntropy"/> used in the update</param>
        public virtual void UpdateHS(SpecificEnergy? enthalpy, SpecificEntropy? entropy)
        {
            if (enthalpy is null || entropy is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.HmassSmass_INPUTS, enthalpy.JoulePerKilogram, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }, nameof(UpdateHS), $"{enthalpy} and {entropy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="EngineeringUnits.SpecificEntropy"/> and the Temperature.<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Water"/>);</c></br>
        /// <br><c>Water.UpdateTS(<see cref="EngineeringUnits.Temperature"/>.FromKelvins(286.15), <see cref="EngineeringUnits.SpecificEntropy"/>.FromJoulePerKilogramKelvin(195.27));</c></br>
        /// </summary> 
        /// <param name = "temperature" > The Temperature used in the update</param>
        /// <param name = "entropy" > The <see cref="EngineeringUnits.SpecificEntropy"/> used in the update</param>
        public virtual void UpdateTS(Temperature? temperature, SpecificEntropy? entropy)
        {
            if (temperature is null || entropy is null)
                return;

            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.SmassT_INPUTS, entropy.JoulePerKilogramKelvin, temperature.Kelvins);
                UpdateValues();
            }, nameof(UpdateTS), $"{temperature} and {entropy}");
        }

        /// <summary>
        /// This is a Beta mehtod used only when looking a CustomFluids!.<br></br>
        /// <br>Exemple:</br>
        /// <br><c><see cref="Fluid"/> Water = <see langword="new"/> <see cref="Fluid"/>(<see cref="FluidList.Custom_SHC228"/>);</c></br>
        /// <br><c>Water.UpdateCustomFluid(<see cref="EngineeringUnits.Temperature"/>.FromKelvins(286.15));</c></br>
        /// </summary> 
        /// <param name = "temperature" > The Temperature used in the update</param>
        public void UpdateCustomFluid(Pressure? pressure, Temperature? temperature)
        {
            //if (pressure is not null)
            //    Pressure = pressure;

            //if (temperature is not null)
            //    Temperature = temperature;

            if (pressure is null || temperature is null)
                return;

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
            //Enthalpy = Cp * temperature;
            FailState = false;

        }

        public List<CustomOil> GetCustomFluidFromDatabase()
        {

            if (Media?.InternalName == "SHC226E")
            {
                return SHC226E.GetList();
            }

            if (Media?.InternalName == "SHC228")
            {
                return SHC228.GetList();
            }

            if (Media?.InternalName == "SHC230")
            {
                return SHC230.GetList();
            }

            if (Media?.InternalName == "Number13")
            {
                return Number13.GetList();
            }

            throw new NotImplementedException("GetCustomFluidFromDatabase didnt return anything");

        }

        private void CachePressure(Pressure pressure) =>
            //Saving old real values
            cache_pressure = Pressure;//Setting input as New value//Pressure = pressure.ToUnit(PressureReference.Absolute);
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

        private bool ShouldItBeCached(UnknownUnit value, UnknownUnit cache_value, Ratio RepeatTolerance)
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

        /// <summary>
        /// Runs <paramref name="updateAction"/> (a REF.update() call followed by <see cref="UpdateValues"/>) with the
        /// error handling shared by all the Update* methods: on a known CoolProp <see cref="ApplicationException"/> it
        /// logs and sets <see cref="FailState"/>; on anything else it does the same unless <paramref name="rethrowUnexpected"/>
        /// is set, in which case the exception propagates. <see cref="ResetErrors"/> always runs afterwards.
        /// </summary>
        private void ExecuteUpdate(Action updateAction, string methodName, string argsDescription, bool rethrowUnexpected = false)
        {
            try
            {
                updateAction();
            }
            catch (ApplicationException e)
            {
                FailState = true;
                Log.Warning($"SharpFluid -> {methodName} -> CoolProp could not return your request on {argsDescription} and returns the followering error: {e}");
            }
            catch (Exception e)
            {
                FailState = true;

                if (rethrowUnexpected)
                    throw;

                Log.Error($"SharpFluid -> {methodName} -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {argsDescription} {e}");
            }
            finally
            {
                ResetErrors();
            }
        }
    }
}

