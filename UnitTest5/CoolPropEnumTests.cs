using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitsTests;

//SharpFluids passes its Parameters and input_pairs enums to CoolProp as plain integers, and CoolProp renumbers these
//enums between versions. These tests fail if the enums don't match the loaded CoolProp library.
[TestClass]
public class CoolPropEnumTests
{
    //CoolProp's names for these differ from the enum member name without its 'i' prefix
    private static readonly Dictionary<Parameters, string> parameterNames = new()
    {
        [Parameters.iacentric_factor] = "acentric",
        [Parameters.iP_critical] = "p_critical",
        [Parameters.iP_reducing] = "p_reducing",
        [Parameters.iP_triple] = "p_triple",
        [Parameters.ispeed_sound] = "speed_of_sound",
    };

    [TestMethod]
    public void LoadsCoolProp8()
    {
        Assert.AreEqual("8.0.0", GlobalParam("version"));
    }

    [TestMethod]
    public void ParametersMatchCoolProp()
    {
        var mismatches = Enum.GetValues(typeof(Parameters)).Cast<Parameters>()
                             .Where(p => p is not (Parameters.INVALID_PARAMETER or Parameters.iundefined_parameter))
                             .Select(p => (Member: p, Native: NativeParameterIndex(parameterNames.TryGetValue(p, out string? name) ? name : p.ToString().Substring(1))))
                             .Where(x => x.Native != (int)x.Member)
                             .Select(x => $"{x.Member} = {(int)x.Member}, CoolProp: {x.Native}")
                             .ToList();

        Assert.AreEqual(0, mismatches.Count, string.Join(Environment.NewLine, mismatches));
    }

    [TestMethod]
    public void InputPairsMatchCoolProp()
    {
        //CoolProp registers the molar and mass versions of the QS and HQ pairs under one name, so the round trip tests cover those
        var sharedNames = new[]
        {
            input_pairs.QSmolar_INPUTS, input_pairs.QSmass_INPUTS, input_pairs.HmolarQ_INPUTS, input_pairs.HmassQ_INPUTS,
            input_pairs.QmassSmolar_INPUTS, input_pairs.QmassSmass_INPUTS, input_pairs.HmolarQmass_INPUTS, input_pairs.HmassQmass_INPUTS,
        };

        var mismatches = Enum.GetValues(typeof(input_pairs)).Cast<input_pairs>()
                             .Where(p => p != input_pairs.INPUT_PAIR_INVALID && !sharedNames.Contains(p))
                             .Select(p => (Member: p, Native: NativeInputPairIndex(p)))
                             .Where(x => x.Native != (int)x.Member)
                             .Select(x => $"{x.Member} = {(int)x.Member}, CoolProp: {x.Native}")
                             .ToList();

        Assert.AreEqual(0, mismatches.Count, string.Join(Environment.NewLine, mismatches));
    }

    [TestMethod]
    public void KeyedOutputMatchesDedicatedGetters()
    {
        using var water = AbstractState.factory("HEOS", "Water")!;
        water.update(input_pairs.PT_INPUTS, 3e5, 400);

        Assert.AreEqual(water.T().Kelvins, water.keyed_output(Parameters.iT), 1e-9);
        Assert.AreEqual(water.p().Pascal, water.keyed_output(Parameters.iP), 1e-6);
        Assert.AreEqual(water.rhomass().KilogramPerCubicMeter, water.keyed_output(Parameters.iDmass), 1e-9);
        Assert.AreEqual(water.hmass().JoulePerKilogram, water.keyed_output(Parameters.iHmass), 1e-6);
        Assert.AreEqual(water.smass().JoulePerKilogramKelvin, water.keyed_output(Parameters.iSmass), 1e-9);
        Assert.AreEqual(water.cpmass().JoulePerKilogramKelvin, water.keyed_output(Parameters.iCpmass), 1e-9);
        Assert.AreEqual(water.speed_sound().MeterPerSecond, water.keyed_output(Parameters.ispeed_sound), 1e-9);
        Assert.AreEqual(water.viscosity().PascalSecond, water.keyed_output(Parameters.iviscosity), 1e-15);
        Assert.AreEqual(water.conductivity().WattPerMeterKelvin, water.keyed_output(Parameters.iconductivity), 1e-12);
        Assert.AreEqual(water.molar_mass().KilogramPerMole, water.keyed_output(Parameters.imolar_mass), 1e-12);

        Assert.AreEqual(647.096, water.keyed_output(Parameters.iT_critical), 1e-6);
        Assert.AreEqual(22.064e6, water.keyed_output(Parameters.iP_critical), 1e-3);
        Assert.AreEqual(water.p_triple().Pascal, water.keyed_output(Parameters.iP_triple), 1e-6);
        Assert.AreEqual(0.3443, water.keyed_output(Parameters.iacentric_factor), 1e-4);
    }

    [TestMethod]
    public void BrineFractionLimitsAndFreezingPoint()
    {
        using var brine = AbstractState.factory("INCOMP", "MEG")!;
        using var fractions = new DoubleVector(new[] { 0.3 });
        brine.set_mass_fractions(fractions);
        brine.update(input_pairs.PT_INPUTS, 2e5, 293.15);

        Assert.AreEqual(0.0, brine.keyed_output(Parameters.ifraction_min), 1e-9);
        Assert.AreEqual(0.6, brine.keyed_output(Parameters.ifraction_max), 1e-9);
        Assert.AreEqual(-15.0, brine.keyed_output(Parameters.iT_freeze) - 273.15, 1.0);
    }

    [TestMethod]
    public void EveryInputPairRoundTripsToTheSameState()
    {
        using var reference = AbstractState.factory("HEOS", "Water")!;
        reference.update(input_pairs.PT_INPUTS, 3e5, 400);

        double T = reference.T().Kelvins;
        double P = reference.p().Pascal;
        double D = reference.rhomass().KilogramPerCubicMeter;
        double H = reference.hmass().JoulePerKilogram;
        double S = reference.smass().JoulePerKilogramKelvin;
        double U = reference.umass().JoulePerKilogram;
        double M = reference.molar_mass().KilogramPerMole;

        //HmassT, TUmass and SmassUmass (and the molar version of the first and last) are left out: HEOS can't flash them
        var inputs = new (input_pairs Pair, double Value1, double Value2)[]
        {
            (input_pairs.PT_INPUTS, P, T),
            (input_pairs.DmassT_INPUTS, D, T),
            (input_pairs.DmolarT_INPUTS, D / M, T),
            (input_pairs.SmassT_INPUTS, S, T),
            (input_pairs.SmolarT_INPUTS, S * M, T),
            (input_pairs.TUmolar_INPUTS, T, U * M),
            (input_pairs.DmassP_INPUTS, D, P),
            (input_pairs.DmolarP_INPUTS, D / M, P),
            (input_pairs.HmassP_INPUTS, H, P),
            (input_pairs.HmolarP_INPUTS, H * M, P),
            (input_pairs.PSmass_INPUTS, P, S),
            (input_pairs.PSmolar_INPUTS, P, S * M),
            (input_pairs.PUmass_INPUTS, P, U),
            (input_pairs.PUmolar_INPUTS, P, U * M),
            (input_pairs.HmassSmass_INPUTS, H, S),
            (input_pairs.HmolarSmolar_INPUTS, H * M, S * M),
            (input_pairs.DmassHmass_INPUTS, D, H),
            (input_pairs.DmolarHmolar_INPUTS, D / M, H * M),
            (input_pairs.DmassSmass_INPUTS, D, S),
            (input_pairs.DmolarSmolar_INPUTS, D / M, S * M),
            (input_pairs.DmassUmass_INPUTS, D, U),
            (input_pairs.DmolarUmolar_INPUTS, D / M, U * M),
        };

        AssertAllRoundTrip(inputs, T, P);
    }

    [TestMethod]
    public void EveryTwoPhaseInputPairRoundTripsToTheSameState()
    {
        using var reference = AbstractState.factory("HEOS", "Water")!;
        //HEOS only flashes QS inputs at saturation, so this uses saturated liquid
        reference.update(input_pairs.PQ_INPUTS, 3e5, 0);

        double T = reference.T().Kelvins;
        double P = reference.p().Pascal;
        double D = reference.rhomass().KilogramPerCubicMeter;
        double S = reference.smass().JoulePerKilogramKelvin;
        double M = reference.molar_mass().KilogramPerMole;

        //Water is a pure fluid, so the mass-based quality (Qmass) equals the molar one
        AssertAllRoundTrip(new[]
        {
            (input_pairs.PQ_INPUTS, P, 0.0),
            (input_pairs.QT_INPUTS, 0.0, T),
            (input_pairs.DmassQ_INPUTS, D, 0.0),
            (input_pairs.DmolarQ_INPUTS, D / M, 0.0),
            (input_pairs.QSmass_INPUTS, 0.0, S),
            (input_pairs.QSmolar_INPUTS, 0.0, S * M),
            (input_pairs.PQmass_INPUTS, P, 0.0),
            (input_pairs.QmassT_INPUTS, 0.0, T),
            (input_pairs.DmassQmass_INPUTS, D, 0.0),
            (input_pairs.DmolarQmass_INPUTS, D / M, 0.0),
            (input_pairs.QmassSmass_INPUTS, 0.0, S),
            (input_pairs.QmassSmolar_INPUTS, 0.0, S * M),
        }, T, P);
    }

    [TestMethod]
    public void HQInputPairsRoundTripToTheSameState()
    {
        using var reference = AbstractState.factory("HEOS", "Water")!;
        //HEOS only flashes HQ inputs on saturated vapor, which has a single temperature for its enthalpy above the enthalpy maximum (~30 bar)
        reference.update(input_pairs.PQ_INPUTS, 200e5, 1);

        double T = reference.T().Kelvins;
        double P = reference.p().Pascal;
        double H = reference.hmass().JoulePerKilogram;
        double M = reference.molar_mass().KilogramPerMole;

        AssertAllRoundTrip(new[]
        {
            (input_pairs.HmassQ_INPUTS, H, 1.0),
            (input_pairs.HmolarQ_INPUTS, H * M, 1.0),
            (input_pairs.HmassQmass_INPUTS, H, 1.0),
            (input_pairs.HmolarQmass_INPUTS, H * M, 1.0),
        }, T, P);
    }

    //Collects every failing pair so one run shows them all
    private static void AssertAllRoundTrip((input_pairs Pair, double Value1, double Value2)[] inputs, double T, double P)
    {
        using var water = AbstractState.factory("HEOS", "Water")!;
        var failures = new List<string>();

        foreach (var (pair, value1, value2) in inputs)
        {
            try
            {
                water.update(pair, value1, value2);
                if (Math.Abs(water.T().Kelvins - T) > 1e-7 * T || Math.Abs(water.p().Pascal - P) > 1e-7 * P)
                    failures.Add($"{pair}: T={water.T().Kelvins} P={water.p().Pascal}");
            }
            catch (Exception e)
            {
                failures.Add($"{pair}: {e.Message}");
            }
        }

        Assert.AreEqual(0, failures.Count, string.Join(Environment.NewLine, failures));
    }

    private static string GlobalParam(string name)
    {
        return Environment.Is64BitProcess
            ? CoolPropPINVOKE64.get_global_param_string(name)
            : CoolPropPINVOKE.get_global_param_string(name);
    }

    private static int NativeParameterIndex(string name)
    {
        return Environment.Is64BitProcess
            ? Lookup(() => CoolPropPINVOKE64.get_parameter_index(name))
            : Lookup(() => CoolPropPINVOKE.get_parameter_index(name));
    }

    private static int NativeInputPairIndex(input_pairs pair)
    {
        return Environment.Is64BitProcess
            ? Lookup(() => CoolPropPINVOKE64.get_input_pair_index(pair.ToString()))
            : Lookup(() => CoolPropPINVOKE.get_input_pair_index(pair.ToString()));
    }

    //An unknown name leaves a pending CoolProp exception, which has to be cleared before SharpFluids calls CoolProp again
    private static int Lookup(Func<int> lookup)
    {
        if (Environment.Is64BitProcess)
        {
            CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
            int index = lookup();
            return CoolPropPINVOKE64.SWIGPendingException.Retrieve() is null ? index : -1;
        }
        else
        {
            CoolPropPINVOKE.SWIGPendingException.ResetErrors();
            int index = lookup();
            return CoolPropPINVOKE.SWIGPendingException.Retrieve() is null ? index : -1;
        }
    }
}
