#if NET10_0_OR_GREATER
using EngineeringFluids.Fluids;
using EngineeringUnits;
using EngineeringUnits.Units;
using Newtonsoft.Json;
using Serilog;
using System;

namespace SharpFluids
{
    // Ammonia through EngineeringFluids (a managed Helmholtz EOS built on EngineeringUnits.Fast) instead of CoolProp.
    // Only the Update overloads taking EngineeringUnits.Fast quantities use it (see FluidUpdatesFast.cs). The result is copied into the
    // same properties CoolProp would fill, so everything else in Fluid works as before. Properties computed on demand (Tsat in single phase,
    // GasDensity, LiquidDensity, GetSatTemperature, ...) still ask CoolProp, from the Pressure/Temperature the update left.
    public partial class Fluid
    {
        /// <summary>
        /// Use EngineeringFluids instead of CoolProp for Ammonia (R717) in the Update overloads that take EngineeringUnits.Fast quantities.<br></br>
        /// If EngineeringFluids fails on a state, the update falls back to CoolProp and logs a warning.<br></br>
        /// EngineeringFluids is at an early stage - set this to <see langword="false"/> to always use CoolProp.
        /// </summary>
        public static bool UseEngineeringFluidsForAmmonia { get; set; } = true;

        [JsonIgnore]
        private Ammonia? engineeringFluidsAmmonia;

        /// <summary>How many updates of this <see cref="Fluid"/> EngineeringFluids has done - lets the tests see which engine ran</summary>
        [JsonIgnore]
        internal int EngineeringFluidsUpdateCount { get; private set; }

        // CoolProp's names for ammonia. Only the HEOS backend: a REFPROP (or other) backend was chosen on purpose
        private bool IsEngineeringFluidsAmmonia =>
            Media is { BackendType: "HEOS" } &&
            (string.Equals(Media.InternalName, "AMMONIA", StringComparison.OrdinalIgnoreCase) ||
             string.Equals(Media.InternalName, "R717", StringComparison.OrdinalIgnoreCase) ||
             string.Equals(Media.InternalName, "NH3", StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Runs <paramref name="update"/> on EngineeringFluids and copies the state into this <see cref="Fluid"/>.
        /// Returns <see langword="false"/> when EngineeringFluids should not or could not be used - the caller then does the CoolProp update.
        /// </summary>
        private bool TryUpdateWithEngineeringFluids(Action<Ammonia> update, string methodName)
        {
            if (!UseEngineeringFluidsForAmmonia || !IsEngineeringFluidsAmmonia)
                return false;

            try
            {
                engineeringFluidsAmmonia ??= new Ammonia();
                update(engineeringFluidsAmmonia);
                UpdateValuesFromEngineeringFluids(engineeringFluidsAmmonia);
                EngineeringFluidsUpdateCount++;
                return true;
            }
            catch (Exception e)
            {
                //A failed update can leave the Ammonia half way - start from a new one next time
                engineeringFluidsAmmonia = null;
                Log.Warning($"SharpFluid -> {methodName} -> EngineeringFluids could not return your request, falling back to CoolProp: {e}");
                return false;
            }
        }

        /// <summary>
        /// The EngineeringFluids version of <see cref="UpdateValues"/>: sets the same properties, from <paramref name="ammonia"/>, and builds
        /// the values with the same factories as the CoolProp wrapper (AbstractState)
        /// </summary>
        private void UpdateValuesFromEngineeringFluids(Ammonia ammonia)
        {
            if (!double.IsFinite(ammonia.Temperature.SI) || !double.IsFinite(ammonia.Density.SI) ||
                !double.IsFinite(ammonia.Pressure.SI) || !double.IsFinite(ammonia.Enthalpy.SI))
                throw new InvalidOperationException($"EngineeringFluids returned a state that is not finite: T={ammonia.Temperature}, D={ammonia.Density}, P={ammonia.Pressure}, H={ammonia.Enthalpy}");

            //Removed cache values
            tsat_Cache = null;
            CacheMode = false;

            Phase = (Phases)(int)ammonia.Phase;

            //CoolProp reports 0 where EngineeringFluids says NaN (between the saturation lines)
            SoundSpeed = double.IsNaN(ammonia.SoundSpeed.SI) ? Speed.Zero : Speed.FromMeterPerSecond(ammonia.SoundSpeed.SI);
            MolarMass = MolarMass.FromKilogramPerMole(ammonia.MolarMass.SI);
            Compressibility = ammonia.Compressibility;

            Quality = ammonia.Quality;

            if (Phase is Phases.Twophase)
            {
                SurfaceTension = ForcePerLength.FromNewtonPerMeter(ammonia.SurfaceTension.SI);

                //In two-phase the saturation temperature is the temperature - no need to ask CoolProp later
                tsat_Cache = Temperature.FromKelvins(ammonia.Temperature.SI);
            }

            Enthalpy = SpecificEnergy.FromJoulePerKilogram(ammonia.Enthalpy.SI);
            Temperature = Temperature.FromKelvins(ammonia.Temperature.SI);
            Pressure = Pressure.From(ammonia.Pressure.SI, PressureUnit.Pascal);

            //Not set here (the CoolProp wrapper returns 0 for all of them anyway): Tau, Delta, fugacity_coefficient, fugacity, gibbsmolar,
            //gibbsmolar_excess, Alpha0, AlphaR, AlphaR_dDelta, AlphaR_dTau, Alpha0_dTau. Nor the cache_* fields, which nothing reads
            umolar = ammonia.MolarInternalEnergy.SI;
            rhomolar = ammonia.Rhomolar.SI;

            Entropy = SpecificEntropy.FromJoulePerKilogramKelvin(ammonia.Entropy.SI);
            Density = Density.FromKilogramPerCubicMeter(ammonia.Density.SI);
            Cp = SpecificEntropy.FromJoulePerKilogramKelvin(ammonia.Cp.SI);
            Cv = SpecificEntropy.FromJoulePerKilogramKelvin(ammonia.Cv.SI);
            DynamicViscosity = DynamicViscosity.FromPascalSecond(ammonia.DynamicViscosity.SI);
            Prandtl = ammonia.Prandtl;

            InternalEnergy = SpecificEnergy.FromJoulePerKilogram(ammonia.InternalEnergy.SI);
            Conductivity = ThermalConductivity.FromWattPerMeterKelvin(ammonia.Conductivity.SI);
            FailState = false;
        }
    }
}
#endif
