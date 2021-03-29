using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace SharpFluids
{
    public partial class Fluid
    {

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

            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (density <= Density.Zero || entropy <= Entropy.Zero)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateDS -> {density} cant be below {densityLimit} and {entropy} cant be below {entropyLimit}", density, Density.Zero, entropy, Entropy.Zero);
                return;
            }


            try
            {
                REF.update(input_pairs.DmassSmass_INPUTS, density.KilogramsPerCubicMeter, entropy.JoulesPerKelvin);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateDS -> CoolProp could not return your request on {density} and {entropy} and returns the followering error: {e}", density, entropy, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdateDS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {entropy} {e}", density, entropy, e);
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


            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (density <= Density.Zero || pressure <= Pressure.Zero)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateDP -> {density} cant be below {densityLimit} and {pressure} cant be below {entropyLimit}", density, Density.Zero, pressure, Pressure.Zero);

                return;
            }

            if (pressure > LimitPressureMax)
                Log?.LogWarning("SharpFluid -> UpdateDP -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased", pressure, LimitPressureMax);


            try
            {
                REF.update(input_pairs.DmassP_INPUTS, density.KilogramsPerCubicMeter, pressure.Pascals);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateDP -> CoolProp could not return your request on {density} and {pressure} and returns the followering error: {e}", density, pressure, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdateDP -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {entropy} {e}", density, pressure, e);
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


            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            if (density <= Density.Zero || temperature < LimitTemperatureMin)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateDT -> {density} cant be below {densityLimit} and {temperature} cant be below {LimitTemperatureMin}", density, Density.Zero, temperature, LimitTemperatureMin);
                return;
            }

            if (temperature > LimitTemperatureMax)
                Log?.LogWarning("SharpFluid -> UpdateDT -> {temperature} is above 'LimitTemperatureMax' ({LimitTemperatureMax}) - This result is extrapolated hence precision is decreased", temperature, LimitTemperatureMax);


            try
            {
                REF.update(input_pairs.DmassT_INPUTS, density.KilogramsPerCubicMeter, temperature.Kelvins);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateDT -> CoolProp could not return your request on {density} and {temperature} and returns the followering error: {e}", density, temperature, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdateDT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {entropy} {e}", density, temperature, e);
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



            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            if (density <= Density.Zero)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateDH -> {density} cant be below {densityLimit} and {enthalpy} cant be below (limit unknown)", density, Density.Zero, enthalpy);
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
                Log?.LogWarning("SharpFluid -> UpdateDH -> CoolProp could not return your request on {density} and {enthalpy} and returns the followering error: {e}", density, enthalpy, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdateDH -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {density} and {entropy} {e}", density, enthalpy, e);
                throw;
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
            if (pressure < LimitPressureMin || temperature < LimitTemperatureMin)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdatePT -> {pressure} cant be below {LimitPressureMin} and {temperature} cant be below {LimitTemperatureMin}", pressure, LimitPressureMin, temperature, LimitTemperatureMin);
                return;
            }

            if (temperature > LimitTemperatureMax)
                Log?.LogWarning("SharpFluid -> UpdatePT -> {temperature} is above 'LimitTemperatureMax' ({LimitTemperatureMax}) - This result is extrapolated hence precision is decreased", temperature, LimitTemperatureMax);

            if (pressure > LimitPressureMax)
                Log?.LogWarning("SharpFluid -> UpdatePT -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased", pressure, LimitPressureMax);



            try
            {
                REF.update(input_pairs.PT_INPUTS, pressure.Pascals, temperature.Kelvins);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdatePT -> CoolProp could not return your request on {pressure} and {temperature} and returns the followering error: {e}", pressure, temperature, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdatePT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {temperature} {e}", pressure, temperature, e);
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
            if (temperature < LimitTemperatureMin)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateXT -> {temperature} cant be below {LimitTemperatureMin}", temperature, LimitTemperatureMin);
                return;
            }


            if (temperature > LimitTemperatureMax)
                Log?.LogWarning("SharpFluid -> UpdateXT -> {temperature} is above 'LimitTemperatureMax' ({LimitTemperatureMax}) - This result is extrapolated hence precision is decreased", temperature, LimitTemperatureMax);

            try
            {
                //If we are above transcritical we just return the Critical point 
                if (temperature >= CriticalTemperature)
                {
                    Log?.LogWarning("SharpFluid -> UpdateXT -> {temperature} is above CriticalTemperature ({CriticalTemperature}) -> We will just return you the CriticalTemperature!", temperature, CriticalTemperature);
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
                Log?.LogWarning("SharpFluid -> UpdateXT -> CoolProp could not return your request on {quality} and {temperature} and returns the followering error: {e}", quality, temperature, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdateXT -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {quality} and {temperature} {e}", quality, temperature, e);
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
            //throw new NotImplementedException("Not yet supported by CoolProp");
            Log?.LogWarning("SharpFluid -> UpdateHT -> Not yet supported by CoolProp!");
            REF.update(input_pairs.HmassT_INPUTS, enthalpy.JoulesPerKilogram, temperature.Kelvins);
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


            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (pressure < LimitPressureMin)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdatePS -> {pressure} cant be below {LimitPressureMin} and {entropy} cant be below (limit unknown)", pressure, LimitPressureMin, entropy);
                return;
            }

            if (pressure > LimitPressureMax)
                Log?.LogWarning("SharpFluid -> UpdatePS -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased", pressure, LimitPressureMax);

            try
            {
                REF.update(input_pairs.PSmass_INPUTS, pressure.Pascals, entropy.JoulesPerKelvin);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdatePS -> CoolProp could not return your request on {pressure} and {entropy} and returns the followering error: {e}", pressure, entropy, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdatePS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {entropy} {e}", pressure, entropy, e);
                throw;
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


            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }


            if (pressure < LimitPressureMin || enthalpy <= SpecificEnergy.Zero)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdatePH -> {pressure} cant be below {LimitPressureMin} and {enthalpy} cant be below {enthalpyLimit}", pressure, LimitPressureMin, enthalpy, SpecificEnergy.Zero);
                return;
            }

            if (pressure > LimitPressureMax)
                Log?.LogWarning("SharpFluid -> UpdatePH -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased", pressure, LimitPressureMax);


            try
            {
                REF.update(input_pairs.HmassP_INPUTS, enthalpy.JoulesPerKilogram, pressure.Pascals);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdatePH -> CoolProp could not return your request on {pressure} and {enthalpy} and returns the followering error: {e}", pressure, enthalpy, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdatePH -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {enthalpy} {e}", pressure, enthalpy, e);
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
            if (pressure < LimitPressureMin || quality < 0)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdatePX -> {pressure} cant be bolow {LimitPressureMin}", pressure, LimitPressureMin);
                return;
            }

            if (pressure > LimitPressureMax)
                Log?.LogWarning("SharpFluid -> UpdatePX -> {pressure} is above 'LimitPressureMax' ({LimitPressureMax}) - This result is extrapolated hence precision is decreased", pressure, LimitPressureMax);

            try
            {
                if (pressure > CriticalPressure)
                {
                    UpdatePT(CriticalPressure, CriticalTemperature);
                    UpdatePH(pressure, Enthalpy);
                    Log?.LogWarning("SharpFluid -> UpdatePX -> {pressure} is above CriticalPressure ({CriticalPressure}) -> We will just return you the Critical point!", pressure, CriticalPressure);
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
                Log?.LogWarning("SharpFluid -> UpdatePX -> CoolProp could not return your request on {pressure} and {quality} and returns the followering error: {e}", pressure, quality, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdatePX -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {pressure} and {quality} {e}", pressure, quality, e);
                throw;
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


            if (Media.InternalName.Contains(".mix"))
            {
                throw new NotImplementedException("For mixtures only UpdatePX, UpdateXT and UpdatePT works");
            }

            try
            {
                REF.update(input_pairs.HmassSmass_INPUTS, enthalpy.JoulesPerKilogram, entropy.JoulesPerKelvin);
                UpdateValues();
            }
            catch (System.ApplicationException e)
            {
                FailState = true;
                Log?.LogWarning("SharpFluid -> UpdateHS -> CoolProp could not return your request on {enthalpy} and {entropy} and returns the followering error: {e}", enthalpy, entropy, e);
            }
            catch (System.Exception e)
            {
                FailState = true;
                Log?.LogError("SharpFluid -> UpdateHS -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {enthalpy} and {entropy} {e}", enthalpy, entropy, e);
                throw;
            }
        }



    }
}

