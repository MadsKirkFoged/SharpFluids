using EngineeringUnits;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpFluids
{
    public static class FluidExtensions
    {
        /// <summary>
        /// Mixing <paramref name="other"/> into this <see cref="Fluid"/>
        /// <br>This makes a simple mixing based on <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/> </br>
        /// <br>Both <see cref="Fluid"/>s should use either <see cref="UnitsNet.MassFlow"/> or <see cref="UnitsNet.Mass"/>!</br>
        /// </summary> 
        /// <param name="other"><see cref="Fluid"/> to be copied from</param>
        public static Fluid AddTo(this Fluid local, Fluid other)
        {
            //TODO Should also work if Mass is selected

            //This makes a simple mixing based on the massflow (weigted)
            //After the mixing an Update should be run


            if (local.Enthalpy.IsZero() ||
                local.Pressure.IsZero() ||
                local.Entropy.IsZero() ||
                local.Temperature.IsZero() ||
                local.MassFlow.IsZero())
            {
                local.Copy(other);
            }
            else if (other.Enthalpy.IsZero() ||
                     other.Pressure.IsZero() ||
                     other.Entropy.IsZero() ||
                     other.Temperature.IsZero() ||
                     other.MassFlow.IsZero())
            {
                //Do nothing
                Log.Debug($"SharpFluid -> AddTo -> {other.Enthalpy} or {other.Pressure} or {other.Entropy} or {other.Temperature} or {other.MassFlow} is zero and nothing is done!");
            }
            else
            {

                if ((other.MassFlow + local.MassFlow).IsNotZero())
                {
                    //Calculating the average H weighted on the massflow
                    local.Enthalpy = other.Enthalpy * (other.MassFlow / (other.MassFlow + local.MassFlow)) + local.Enthalpy * (local.MassFlow / (other.MassFlow + local.MassFlow));

                    //Calculating the average P weighted on the massflow
                    local.Pressure = other.Pressure * (other.MassFlow / (other.MassFlow + local.MassFlow)) + local.Pressure * (local.MassFlow / (other.MassFlow + local.MassFlow));


                    //Calculating the average S weighted on the massflow
                    local.Entropy = other.Entropy * (other.MassFlow / (other.MassFlow + local.MassFlow)) + local.Entropy * (local.MassFlow / (other.MassFlow + local.MassFlow));

                    //Calculating the average T weighted on the massflow
                    local.Temperature = Temperature.FromKelvins((double)(other.Temperature.Kelvins * (other.MassFlow / (other.MassFlow + local.MassFlow)) + local.Temperature.Kelvins * (local.MassFlow / (other.MassFlow + local.MassFlow))));
                }

                local.MassFlow = other.MassFlow + local.MassFlow;

                //this.CheckForNaN();

            }
            return local;


        }

        /// <summary>
        /// Add <see cref="UnitsNet.Power"/> to the <see cref="Fluid"/>
        /// <br>This does only work when using <see cref="UnitsNet.MassFlow"/></br>
        /// </summary>
        public static Fluid AddPower(this Fluid local, Power powerToBeAdded)
        {
            //TODO If mass is selected!
            //Finding the new H


            if (local.MassFlow == MassFlow.Zero)
            {
                return local;
            }

            try
            {
                SpecificEnergy localSpecificEnergy = ((local.Enthalpy * local.MassFlow) + powerToBeAdded) / local.MassFlow;

                //if (powerToBeAdded > Power.Zero)
                if (localSpecificEnergy > local.Enthalpy)
                {
                    local.UpdatePT(local.Pressure, local.LimitTemperatureMax);

                    if (local.Enthalpy > localSpecificEnergy)
                        local.UpdatePH(local.Pressure, localSpecificEnergy);
                }
                else
                {
                    local.UpdatePT(local.Pressure, local.LimitTemperatureMin);

                    if (local.Enthalpy < localSpecificEnergy)
                        local.UpdatePH(local.Pressure, localSpecificEnergy);

                }

            }
            catch (Exception e)
            {

                local.FailState = true;
                Log.Error($"SharpFluid -> AddPower -> {e}");
            }
            return local;


        }


        /// <summary>
        /// Remove <see cref="UnitsNet.Power"/> from the <see cref="Fluid"/>
        /// </summary> 
        /// <remarks>
        /// <br>This does only work when using <see cref="UnitsNet.MassFlow"/></br>
        /// </remarks>
        public static Fluid RemovePower(this Fluid local, Power powerToBeRemoved)
        {
            //TODO: If mass is selected 

            local.AddPower(powerToBeRemoved * -1);
            return local;
        }

        public static Speed FluidVelocity(this Fluid local, Area SizeOfPipe)
        {
            return local.VolumeFlow / SizeOfPipe;
        }


    }
}
