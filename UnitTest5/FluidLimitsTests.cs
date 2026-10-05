using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using System;
using System.Collections.Generic;

namespace UnitsTests;

/// <summary>
/// EngineeringUnits keeps 15 significant digits of a double, so a limit read from CoolProp can come back just outside
/// CoolProp's own range (Ammonia's critical pressure did). SharpFluids rounds its limits towards the inside instead,
/// so it can hand them back to CoolProp.
/// </summary>
[TestClass]
public class FluidLimitsTests
{
    [TestMethod]
    public void LimitsAreInsideCoolPropsRange()
    {
        var failures = new List<string>();

        foreach (var (item, fluid, coolProp) in PureFluids())
        {
            using (coolProp)
            {
                if (fluid.CriticalPressure!.Pascal > coolProp.keyed_output(Parameters.iP_critical))
                    failures.Add($"{item}: CriticalPressure above CoolProp's");
                if (fluid.CriticalTemperature!.Kelvins > coolProp.keyed_output(Parameters.iT_critical))
                    failures.Add($"{item}: CriticalTemperature above CoolProp's");
                if (fluid.LimitPressureMin!.Pascal < coolProp.keyed_output(Parameters.iP_triple))
                    failures.Add($"{item}: LimitPressureMin below CoolProp's");
                if (fluid.LimitPressureMax!.Pascal > coolProp.keyed_output(Parameters.iP_max))
                    failures.Add($"{item}: LimitPressureMax above CoolProp's");
                if (fluid.LimitTemperatureMin!.Kelvins < coolProp.keyed_output(Parameters.iT_min))
                    failures.Add($"{item}: LimitTemperatureMin below CoolProp's");
                if (fluid.LimitTemperatureMax!.Kelvins > coolProp.keyed_output(Parameters.iT_max))
                    failures.Add($"{item}: LimitTemperatureMax above CoolProp's");
            }
        }

        Assert.AreEqual(0, failures.Count, string.Join(Environment.NewLine, failures));
    }

    [TestMethod]
    public void UpdatesAtTheCriticalPoint()
    {
        var failures = new List<string>();

        foreach (var (item, fluid, coolProp) in PureFluids())
        {
            coolProp.Dispose();

            foreach (double quality in new[] { 0.0, 1.0 })
            {
                var atCriticalPressure = new Fluid(item);
                atCriticalPressure.UpdatePX(fluid.CriticalPressure, quality);
                if (atCriticalPressure.FailState)
                    failures.Add($"{item}: UpdatePX(CriticalPressure, {quality}) failed");

                //UpdateXT returns the critical point with FailState set when it gets the critical temperature, but it has to return it
                var atCriticalTemperature = new Fluid(item);
                atCriticalTemperature.UpdateXT(quality, fluid.CriticalTemperature);
                if (atCriticalTemperature.Pressure is null)
                    failures.Add($"{item}: UpdateXT({quality}, CriticalTemperature) gave no state");
            }
        }

        Assert.AreEqual(0, failures.Count, string.Join(Environment.NewLine, failures));
    }

    //The pure HEOS fluids SharpFluids can create, with a CoolProp AbstractState of the same fluid
    private static IEnumerable<(FluidList Item, Fluid Fluid, AbstractState CoolProp)> PureFluids()
    {
        foreach (FluidList item in Enum.GetValues(typeof(FluidList)))
        {
            Fluid fluid;
            try
            {
                fluid = new Fluid(item);
            }
            catch (Exception)
            {
                continue;
            }

            if (fluid.Media?.BackendType != "HEOS" || fluid.Media.Mix is not MixType.None || fluid.CriticalPressure is null)
                continue;

            var coolProp = AbstractState.factory("HEOS", fluid.Media.InternalName);
            if (coolProp is not null)
                yield return (item, fluid, coolProp);
        }
    }
}
