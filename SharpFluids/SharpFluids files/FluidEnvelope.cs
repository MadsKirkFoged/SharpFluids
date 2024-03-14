//using EngineeringUnits;
using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.Linq;
//using EngineeringUnits.Serialization.JsonNet;

namespace SharpFluids
{
    public partial class Fluid
    {

        public List<(Pressure, SpecificEnergy)> GetEnvelopePhase()
        {

            var localListLiq = new List<(Pressure, SpecificEnergy)>();
            var localListGas = new List<(Pressure, SpecificEnergy)>();
            var CompleteList = new List<(Pressure, SpecificEnergy)>();

            Pressure? Increment = (CriticalPressure - LimitPressureMin) / 10000;

            for (Pressure i = CriticalPressure - Pressure.FromBar(1); i > LimitPressureMin; i -= Increment)
            {
                UpdatePX(i, 0);

                if (!FailState)
                    localListLiq.Add((i, Enthalpy!));

                UpdatePX(i, 1);

                if (!FailState)
                    localListGas.Add((i, Enthalpy!));

                Increment *= 1.10;
            }

            //Merging into one list
            foreach ((Pressure, SpecificEnergy) item in localListLiq.AsEnumerable().Reverse())
                CompleteList.Add(item);

            foreach ((Pressure, SpecificEnergy) item in localListGas)
                CompleteList.Add(item);

            return CompleteList;
        }
    }
}
