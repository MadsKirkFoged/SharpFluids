#if NET10_0_OR_GREATER
using EngineeringUnits.Fast;
using Serilog;
using System;

namespace SharpFluids
{
    // The Update* methods again, taking EngineeringUnits.Fast quantities (structs holding one SI double) instead of EngineeringUnits classes.
    // They are overloads, so nothing changes for existing callers: a struct is never null, so a call like UpdatePT(null, null) still only
    // matches the EngineeringUnits version. The properties of the Fluid (Pressure, Temperature, ...) are still EngineeringUnits types.
    // Inside Fluid the names Pressure, Temperature, ... are those properties, so a Fast type used as a value has to be written in full.
    // Ammonia goes through EngineeringFluids instead of CoolProp when it can (see FluidUpdatesEngineeringFluids.cs). The updates that can
    // land in two-phase use its Exact variants, which solve the saturation state against the EOS and match CoolProp to about 1e-10.
    public partial class Fluid
    {
        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Density"/> and the <see cref="SpecificEntropy"/> (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdateDS(EngineeringUnits.Fast.Density.FromKilogramPerCubicMeter(999.38), EngineeringUnits.Fast.SpecificEntropy.FromJoulePerKilogramKelvin(195.27));</c></br>
        /// </summary>
        public virtual void UpdateDS(Density density, SpecificEntropy entropy)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdateDS(density, entropy), nameof(UpdateDS)))
                return;

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.DmassSmass_INPUTS, density.KilogramPerCubicMeter, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }, nameof(UpdateDS), $"{density} and {entropy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Density"/> and the <see cref="Pressure"/> (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdateDP(EngineeringUnits.Fast.Density.FromKilogramPerCubicMeter(999.38), EngineeringUnits.Fast.Pressure.FromBar(1.013));</c></br>
        /// </summary>
        public virtual void UpdateDP(Density density, Pressure pressure)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdateDP(density, pressure), nameof(UpdateDP)))
                return;

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.DmassP_INPUTS, density.KilogramPerCubicMeter, pressure.Pascal);
                UpdateValues();
            }, nameof(UpdateDP), $"{density} and {pressure}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Density"/> and the <see cref="Temperature"/> (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdateDT(EngineeringUnits.Fast.Density.FromKilogramPerCubicMeter(999.38), EngineeringUnits.Fast.Temperature.FromDegreeCelsius(13));</c></br>
        /// </summary>
        public virtual void UpdateDT(Density density, Temperature temperature)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdateDT(density, temperature), nameof(UpdateDT)))
                return;

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.DmassT_INPUTS, density.KilogramPerCubicMeter, temperature.Kelvins);
                UpdateValues();
            }, nameof(UpdateDT), $"{density} and {temperature}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Density"/> and the Enthalpy (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdateDH(EngineeringUnits.Fast.Density.FromKilogramPerCubicMeter(999.38), EngineeringUnits.Fast.SpecificEnergy.FromJoulePerKilogram(54697.59));</c></br>
        /// </summary>
        public virtual void UpdateDH(Density density, SpecificEnergy enthalpy)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdateDH(density, (Enthalpy)enthalpy), nameof(UpdateDH)))
                return;

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.DmassHmass_INPUTS, density.KilogramPerCubicMeter, enthalpy.JoulePerKilogram);
                UpdateValues();
            }, nameof(UpdateDH), $"{density} and {enthalpy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Pressure"/> and the <see cref="Temperature"/> (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdatePT(EngineeringUnits.Fast.Pressure.FromBar(1.013), EngineeringUnits.Fast.Temperature.FromDegreeCelsius(13));</c></br>
        /// </summary>
        public virtual void UpdatePT(Pressure pressure, Temperature temperature, Ratio? RepeatTolerance = null)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdatePT(pressure, temperature), nameof(UpdatePT)))
                return;

            try
            {
                REF.update(input_pairs.PT_INPUTS, pressure.Pascal, temperature.Kelvins);
                UpdateValues();

            }
            catch (System.ApplicationException e)
            {

                if (e.Message.StartsWith("options.p is not valid in saturation_T_pure_1D_P for T") && temperature.Kelvins < 405.7 && temperature.Kelvins > 405.3)
                {
                    Log.Warning($"This is a known coolprop error");

                    UpdatePT(pressure, EngineeringUnits.Fast.Temperature.FromKelvins(405.3));
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
        /// Update the condition of the <see cref="Fluid"/> when you know the Quality and the <see cref="Temperature"/> (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>CO2.UpdateXT(0.7, EngineeringUnits.Fast.Temperature.FromDegreeCelsius(13));</c></br>
        /// </summary>
        public virtual void UpdateXT(double quality, Temperature temperature, double? RepeatTolerance = null)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();

            //Above the critical temperature the CoolProp path below returns the critical point
            if (!(CriticalTemperature is not null && temperature.Kelvins >= CriticalTemperature.Kelvins) &&
                TryUpdateWithEngineeringFluids(a => a.UpdateXTExact(quality, temperature), nameof(UpdateXT)))
                return;

            ExecuteUpdate(() =>
            {
                //If we are above transcritical we just return the Critical point
                if (CriticalTemperature is not null && temperature.Kelvins >= CriticalTemperature.Kelvins)
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
        /// </summary>
        public virtual void UpdateHT(SpecificEnergy enthalpy, Temperature temperature)
        {
            //Not yet supported by CoolProp!
            Log.Debug($"SharpFluid -> UpdateHT -> Not yet supported by CoolProp!");
            throw new NotImplementedException($"SharpFluid -> UpdateHT -> Not (yet) supported by CoolProp!");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Pressure"/> and the <see cref="SpecificEntropy"/> (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdatePS(EngineeringUnits.Fast.Pressure.FromBar(1.013), EngineeringUnits.Fast.SpecificEntropy.FromJoulePerKilogramKelvin(195.27));</c></br>
        /// </summary>
        public virtual void UpdatePS(Pressure pressure, SpecificEntropy entropy, Ratio? RepeatTolerance = null)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdatePSExact(pressure, entropy), nameof(UpdatePS)))
                return;

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.PSmass_INPUTS, pressure.Pascal, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }, nameof(UpdatePS), $"{pressure} and {entropy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Pressure"/> and the Enthalpy (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdatePH(EngineeringUnits.Fast.Pressure.FromBar(1.013), EngineeringUnits.Fast.SpecificEnergy.FromJoulePerKilogram(54697.59));</c></br>
        /// </summary>
        public virtual void UpdatePH(Pressure pressure, SpecificEnergy enthalpy, Ratio? RepeatTolerance = null)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdatePHExact(pressure, (Enthalpy)enthalpy), nameof(UpdatePH)))
                return;

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.HmassP_INPUTS, enthalpy.JoulePerKilogram, pressure.Pascal);
                UpdateValues();
            }, nameof(UpdatePH), $"{pressure} and {enthalpy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Pressure"/> and the Quality (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>CO2.UpdatePX(EngineeringUnits.Fast.Pressure.FromBar(25), 0.7);</c></br>
        /// </summary>
        public virtual void UpdatePX(Pressure pressure, double quality, double? RepeatTolerance = null)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();

            //Above the critical pressure the CoolProp path below returns the critical point
            if (!(CriticalPressure is not null && pressure.Pascal > CriticalPressure.Pascal) &&
                TryUpdateWithEngineeringFluids(a => a.UpdatePXExact(pressure, quality), nameof(UpdatePX)))
                return;

            ExecuteUpdate(() =>
            {
                if (CriticalPressure is not null && pressure.Pascal > CriticalPressure.Pascal)
                {
                    //The critical point is stored as EngineeringUnits, so this rare path uses the EngineeringUnits overloads
                    UpdatePT(CriticalPressure, CriticalTemperature);
                    UpdatePH(EngineeringUnits.Pressure.FromSI(pressure.SI), Enthalpy);
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
        /// Update the condition of the <see cref="Fluid"/> when you know the Enthalpy and the <see cref="SpecificEntropy"/> (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdateHS(EngineeringUnits.Fast.SpecificEnergy.FromJoulePerKilogram(54697.59), EngineeringUnits.Fast.SpecificEntropy.FromJoulePerKilogramKelvin(195.27));</c></br>
        /// </summary>
        public virtual void UpdateHS(SpecificEnergy enthalpy, SpecificEntropy entropy)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdateHS((Enthalpy)enthalpy, entropy), nameof(UpdateHS)))
                return;

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.HmassSmass_INPUTS, enthalpy.JoulePerKilogram, entropy.JoulePerKilogramKelvin);
                UpdateValues();
            }, nameof(UpdateHS), $"{enthalpy} and {entropy}");
        }

        /// <summary>
        /// Update the condition of the <see cref="Fluid"/> when you know the <see cref="Temperature"/> and the <see cref="SpecificEntropy"/> (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Water.UpdateTS(EngineeringUnits.Fast.Temperature.FromKelvins(286.15), EngineeringUnits.Fast.SpecificEntropy.FromJoulePerKilogramKelvin(195.27));</c></br>
        /// </summary>
        public virtual void UpdateTS(Temperature temperature, SpecificEntropy entropy)
        {
            CheckBeforeUpdate();
            GuardFromCustomFluids();
            GuardFromMixFluids();

            if (TryUpdateWithEngineeringFluids(a => a.UpdateTS(temperature, entropy), nameof(UpdateTS)))
                return;

            ExecuteUpdate(() =>
            {
                REF.update(input_pairs.SmassT_INPUTS, entropy.JoulePerKilogramKelvin, temperature.Kelvins);
                UpdateValues();
            }, nameof(UpdateTS), $"{temperature} and {entropy}");
        }

        /// <summary>
        /// This is a Beta mehtod used only when looking a CustomFluids! (EngineeringUnits.Fast)<br></br>
        /// <br>Exemple:</br>
        /// <br><c>Oil.UpdateCustomFluid(EngineeringUnits.Fast.Pressure.FromBar(1), EngineeringUnits.Fast.Temperature.FromKelvins(286.15));</c></br>
        /// </summary>
        public void UpdateCustomFluid(Pressure pressure, Temperature temperature) =>
            //The custom fluid tables and the interpolation are EngineeringUnits, so convert at the boundary
            UpdateCustomFluid(EngineeringUnits.Pressure.FromSI(pressure.SI), EngineeringUnits.Temperature.FromSI(temperature.SI));
    }
}
#endif
