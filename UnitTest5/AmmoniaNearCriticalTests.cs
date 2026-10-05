using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Linq;
#if NET10_0_OR_GREATER
using Fast = EngineeringUnits.Fast;
#endif

namespace UnitsTests;

/// <summary>
/// CoolProp 6.4.2dev failed PT updates of Ammonia between 405.40 K and its critical temperature (405.56 K) at every pressure,
/// and SharpFluids used to work around it by updating at 405.3 K instead. These check that every state there is now exact.
/// </summary>
[TestClass]
public class AmmoniaNearCriticalTests
{
    //Away from the critical pressure (113.63 bar), where PT doesn't define the state
    private static readonly double[] Bars = { 1, 21, 22.9344357087332, 48.57593, 60, 110, 150, 500 };

    private static readonly double[] Kelvins = Enumerable.Range(0, 41).Select(i => Math.Round(405.30 + i * 0.01, 2))
                                                         .Concat(new[] { 405.4, 405.55873230045091, 405.56 })
                                                         .Distinct()
                                                         .OrderBy(t => t)
                                                         .ToArray();

    [TestMethod]
    public void UpdatePTAroundCriticalTemperature()
    {
        AssertExactAndIncreasing((fluid, bar, kelvin) => fluid.UpdatePT(Pressure.FromBar(bar), Temperature.FromKelvin(kelvin)));
    }

#if NET10_0_OR_GREATER
    [TestMethod]
    public void FastUpdatePTAroundCriticalTemperatureWithCoolProp()
    {
        try
        {
            Fluid.UseEngineeringFluidsForAmmonia = false;

            AssertExactAndIncreasing((fluid, bar, kelvin) => fluid.UpdatePT(Fast.Pressure.FromBar(bar), Fast.Temperature.FromKelvins(kelvin)));
        }
        finally
        {
            Fluid.UseEngineeringFluidsForAmmonia = true;
        }
    }
#endif

    //Collects every failing state so one run shows them all
    private static void AssertExactAndIncreasing(Action<Fluid, double, double> updatePT)
    {
        var failures = new List<string>();

        foreach (double bar in Bars)
        {
            double previousEnthalpy = double.NegativeInfinity;

            foreach (double kelvin in Kelvins)
            {
                var R717 = new Fluid(FluidList.Ammonia);
                updatePT(R717, bar, kelvin);

                if (R717.FailState)
                {
                    failures.Add($"{bar} bar, {kelvin} K: FailState");
                    continue;
                }

                //The pressure comes back from CoolProp's density solver, which near the critical point converges to about 1e-9
                if (Math.Abs(R717.Temperature.Kelvins - kelvin) > 1e-9 * kelvin || Math.Abs(R717.Pressure.Bar - bar) > 1e-6 * bar)
                    failures.Add($"{bar} bar, {kelvin} K: got {R717.Pressure.Bar} bar, {R717.Temperature.Kelvins} K");

                double enthalpy = R717.Enthalpy.JoulePerKilogram;
                if (!(enthalpy > previousEnthalpy))
                    failures.Add($"{bar} bar, {kelvin} K: enthalpy {enthalpy} J/kg is not above the previous temperature's {previousEnthalpy} J/kg");

                previousEnthalpy = enthalpy;
            }
        }

        Assert.AreEqual(0, failures.Count, string.Join(Environment.NewLine, failures));
    }
}
