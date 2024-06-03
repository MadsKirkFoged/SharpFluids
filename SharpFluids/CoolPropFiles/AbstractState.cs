//using EngineeringUnits;
using EngineeringUnits;
using EngineeringUnits.Units;
using Serilog;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class AbstractState : IDisposable
{
    private static readonly object lockObj = new();

    public HandleRef swigCPtr;
    public bool swigCMemOwn;

    internal AbstractState()
    {

    }
    internal AbstractState(IntPtr cPtr, bool cMemoryOwn)
    {
        swigCMemOwn = cMemoryOwn;
        swigCPtr = new HandleRef(this, cPtr);
    }
    ~AbstractState()
    {
        Dispose();
    }

    public virtual void Dispose()
    {
        lock (this)
        {
            if (swigCPtr.Handle != IntPtr.Zero)
            {
                swigCMemOwn = false;
                if (Environment.Is64BitProcess)
                    CoolPropPINVOKE64.delete_AbstractState(swigCPtr);
                else
                    CoolPropPINVOKE.delete_AbstractState(swigCPtr);

                swigCPtr = new HandleRef(null, IntPtr.Zero);
            }

            GC.SuppressFinalize(this);
        }
    }

    public static AbstractState? factory(string backend, string fluid_names)
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    IntPtr cPtr = CoolPropPINVOKE64.AbstractState_factory__SWIG_0(backend, fluid_names);
                    AbstractState? ret = (cPtr == IntPtr.Zero) ? null : new AbstractState(cPtr, false);
                    return ret;
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    IntPtr cPtr = CoolPropPINVOKE.AbstractState_factory__SWIG_0(backend, fluid_names);
                    AbstractState? ret = (cPtr == IntPtr.Zero) ? null : new AbstractState(cPtr, false);
                    return ret;
                }
            }
        }
    }
    public virtual void update(input_pairs input_pair, double Value1, double Value2)
    {

        //Internal look up

        lock (lockObj)
        {

            try
            {
                //CoolProp Lookup
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    CoolPropPINVOKE64.AbstractState_update(swigCPtr, (int)input_pair, Value1, Value2);
                    if (CoolPropPINVOKE64.SWIGPendingException.Pending)
                        throw CoolPropPINVOKE64.SWIGPendingException.Retrieve();
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    CoolPropPINVOKE.AbstractState_update(swigCPtr, (int)input_pair, Value1, Value2);
                    if (CoolPropPINVOKE.SWIGPendingException.Pending)
                        throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
                }
            }
            catch (NullReferenceException e)
            {
                Log.Error($"SharpFluid created a null error that we are yet to figure out! {e}");
                Log.Error($"SharpFluid: What was swigCPtr: {swigCPtr}");
            }
            catch (Exception e)
            {
                Log.Information($"SharpFluid error: {e}");
                throw;
            }
        }
    }

    //public virtual void update_with_guesses(input_pairs input_pair, double Value1, double Value2, GuessesStructure guesses)
    //{
    //    CoolPropPINVOKE.AbstractState_update_with_guesses(swigCPtr, (int)input_pair, Value1, Value2, GuessesStructure.getCPtr(guesses));
    //    if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
    //}

    public static double updateAir(string jarg1, string jarg2, double jarg3, string jarg4, double jarg5, string jarg6, double jarg7)
    {
        lock (lockObj)
        {

            //Internal look up
            double local = 0;

            //CoolProp Lookup
            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                local = CoolPropPINVOKE64.HAPropsSI(jarg1, jarg2, jarg3, jarg4, jarg5, jarg6, jarg7);
                if (CoolPropPINVOKE64.SWIGPendingException.Pending)
                    throw CoolPropPINVOKE64.SWIGPendingException.Retrieve();
            }
            else
            {
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                local = CoolPropPINVOKE.HAPropsSI(jarg1, jarg2, jarg3, jarg4, jarg5, jarg6, jarg7);
                if (CoolPropPINVOKE.SWIGPendingException.Pending)
                    throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            }

            return local;
        }
    }

    public virtual string backend_name()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    var ret = CoolPropPINVOKE64.AbstractState_backend_name(swigCPtr);
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return ret;
                }
                else
                {
                    var ret = CoolPropPINVOKE.AbstractState_backend_name(swigCPtr);
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return ret;
                }
            }
        }
    }
    public virtual bool using_mass_fractions()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    var ret = CoolPropPINVOKE64.AbstractState_using_mass_fractions(swigCPtr);
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return ret;
                }
                else
                {
                    var ret = CoolPropPINVOKE.AbstractState_using_mass_fractions(swigCPtr);
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return ret;
                }
            }
        }
    }
    public void set_mass_fractions(DoubleVector mass_fractions)
    {

        lock (lockObj)
        {

            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.AbstractState_set_mass_fractions(swigCPtr, DoubleVector.getCPtr(mass_fractions));
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            }
            else
            {
                CoolPropPINVOKE.AbstractState_set_mass_fractions(swigCPtr, DoubleVector.getCPtr(mass_fractions));
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();

                //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            }
        }
    }
    public void set_volu_fractions(DoubleVector volu_fractions)
    {
        lock (lockObj)
        {
            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.AbstractState_set_volu_fractions(swigCPtr, DoubleVector.getCPtr(volu_fractions));
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            }
            else
            {
                CoolPropPINVOKE.AbstractState_set_volu_fractions(swigCPtr, DoubleVector.getCPtr(volu_fractions));
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            }
        }
    }

    //public string GetInfo(string ParamName)
    //{

    //    lock (lockObj)
    //    {

    //        lock (lockObj)
    //        {
    //            if (Environment.Is64BitProcess)
    //            {
    //                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
    //                return CoolPropPINVOKE64.get_global_param_string(ParamName);
    //            }
    //            else
    //            {
    //                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
    //                return CoolPropPINVOKE.get_global_param_string(ParamName);
    //            }
    //        }
    //    }
    //}

    public Temperature Tmin()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return Temperature.FromKelvins(CoolPropPINVOKE64.AbstractState_Tmin(swigCPtr));
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return Temperature.FromKelvins(CoolPropPINVOKE.AbstractState_Tmin(swigCPtr));
                }
            }
        }
    }
    public Temperature Tmax()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return Temperature.FromKelvins(CoolPropPINVOKE64.AbstractState_Tmax(swigCPtr));
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return Temperature.FromKelvins(CoolPropPINVOKE.AbstractState_Tmax(swigCPtr));
                }
            }
        }
    }
    public Pressure pmax()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return Pressure.From(CoolPropPINVOKE64.AbstractState_pmax(swigCPtr), PressureUnit.Pascal);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return Pressure.From(CoolPropPINVOKE.AbstractState_pmax(swigCPtr), PressureUnit.Pascal);
                }
            }
        }
    }
    public Temperature TCritical()
    {
        lock (lockObj)
        {

            if (Environment.Is64BitProcess)
            {
                if (CoolPropPINVOKE64.AbstractState_T_critical(swigCPtr) != 0)
                    return Temperature.FromKelvins(CoolPropPINVOKE64.AbstractState_T_critical(swigCPtr));
                else
                {
                    Debug.Print("It could not return T_critical!");
                    return Tmax(); //Workaround for when T_critical does not get returned by coolprop
                }
            }
            else
            {

                if (CoolPropPINVOKE.AbstractState_T_critical(swigCPtr) != 0)
                    return Temperature.FromKelvins(CoolPropPINVOKE.AbstractState_T_critical(swigCPtr));
                else
                {
                    Debug.Print("It could not return T_critical!");
                    return Tmax(); //Workaround for when T_critical does not get returned by coolprop
                }
            }
        }
    }
    public Pressure p_critical()
    {
        lock (lockObj)
        {

            if (Environment.Is64BitProcess)
            {
                if (CoolPropPINVOKE64.AbstractState_p_critical(swigCPtr) != 0)
                {
                    //return Pressure.FromPascal(CoolPropPINVOKE64.AbstractState_p_critical(swigCPtr));
                    return Pressure.From(CoolPropPINVOKE64.AbstractState_p_critical(swigCPtr), PressureUnit.Pascal);
                }
                else
                {
                    Debug.Print("It could not return p_critical!");
                    return pmax(); //Workaround for when p_critical does not get returned by coolprop
                }
            }
            else if (CoolPropPINVOKE.AbstractState_p_critical(swigCPtr) != 0)
            {
                //return Pressure.FromPascal(CoolPropPINVOKE.AbstractState_p_critical(swigCPtr));
                return Pressure.From(CoolPropPINVOKE.AbstractState_p_critical(swigCPtr), PressureUnit.Pascal);
            }
            else
            {
                Debug.Print("It could not return p_critical!");
                return pmax(); //Workaround for when p_critical does not get returned by coolprop
            }
        }
    }
    public Pressure p_triple()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return Pressure.From(CoolPropPINVOKE64.AbstractState_p_triple(swigCPtr), PressureUnit.Pascal);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return Pressure.From(CoolPropPINVOKE.AbstractState_p_triple(swigCPtr), PressureUnit.Pascal);
                }
            }
        }
    }

    public PhasesCoolProp phase()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return (PhasesCoolProp)CoolPropPINVOKE64.AbstractState_phase(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return (PhasesCoolProp)CoolPropPINVOKE.AbstractState_phase(swigCPtr);
                }
            }
        }
    }
    public string name()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_name(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE.AbstractState_name(swigCPtr);
                }
            }
        }
    }
    public double keyed_output(Parameters key)
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    var test = CoolPropPINVOKE64.AbstractState_keyed_output(swigCPtr, (int)key);
                    return test;
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    var test = CoolPropPINVOKE.AbstractState_keyed_output(swigCPtr, (int)key);
                    return test;
                }
            }
        }
    }
    public Temperature T()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return Temperature.FromKelvins(CoolPropPINVOKE64.AbstractState_T(swigCPtr));
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return Temperature.FromKelvins(CoolPropPINVOKE.AbstractState_T(swigCPtr));
                }
            }
        }
    }
    public Density rhomass()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return Density.FromKilogramPerCubicMeter(CoolPropPINVOKE64.AbstractState_rhomass(swigCPtr));
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return Density.FromKilogramPerCubicMeter(CoolPropPINVOKE.AbstractState_rhomass(swigCPtr));
                }
            }
        }
    }
    public Pressure p()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return Pressure.From(CoolPropPINVOKE64.AbstractState_p(swigCPtr), PressureUnit.Pascal);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return Pressure.From(CoolPropPINVOKE.AbstractState_p(swigCPtr), PressureUnit.Pascal);
                }
            }
        }
    }

    public double Alpha0()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_alpha0(swigCPtr);
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    //return CoolPropPINVOKE.AbstractState_alpha0(swigCPtr);
                    return 0;
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
            }
        }
    }



    public double AlphaR()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    //return CoolPropPINVOKE.AbstractState_alphar(swigCPtr);
                    return 0;
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
            }
        }
    }

    public double AlphaR_dDelta()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_dalphar_dDelta(swigCPtr);
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    //return CoolPropPINVOKE.AbstractState_dalphar_dDelta(swigCPtr);
                    return 0;
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
            }
        }
    }

    public double AlphaR_dTau()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_dalphar_dTau(swigCPtr);
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    //return CoolPropPINVOKE.AbstractState_dalphar_dTau(swigCPtr);
                    return 0;
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
            }
        }
    }

    public double Alpha0_dTau()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_dalpha0_dTau(swigCPtr);
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_dalpha0_dTau(swigCPtr);
                    //var AlphaR = CoolPropPINVOKE64.AbstractState_alphar(swigCPtr);
                }
            }
        }
    }


    
    public double Q()
    {

        lock (lockObj)
        {
            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_Q(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE.AbstractState_Q(swigCPtr);
                }
            }
        }
    }

    public MolarMass molar_mass()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return MolarMass.FromKilogramPerMole(CoolPropPINVOKE64.AbstractState_molar_mass(swigCPtr));
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return MolarMass.FromKilogramPerMole(CoolPropPINVOKE.AbstractState_molar_mass(swigCPtr));
                }
            }
        }
    }
    public double compressibility_factor()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE64.AbstractState_compressibility_factor(swigCPtr);
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return CoolPropPINVOKE.AbstractState_compressibility_factor(swigCPtr);
                }
            }
        }
    }

    public SpecificEnergy hmass()
    {
        lock (lockObj)
        {

            lock (lockObj)
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return SpecificEnergy.FromJoulePerKilogram(CoolPropPINVOKE64.AbstractState_hmass(swigCPtr));
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return SpecificEnergy.FromJoulePerKilogram(CoolPropPINVOKE.AbstractState_hmass(swigCPtr));
                }
            }
        }
    }
    public SpecificEntropy smass()
    {
        lock (lockObj)
        {
            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                return SpecificEntropy.FromJoulePerKilogramKelvin(CoolPropPINVOKE64.AbstractState_smass(swigCPtr));
            }
            else
            {
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                return SpecificEntropy.FromJoulePerKilogramKelvin(CoolPropPINVOKE.AbstractState_smass(swigCPtr));
            }
        }
    }
    public SpecificEnergy umass()
    {
        lock (lockObj)
        {
            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                return SpecificEnergy.FromJoulePerKilogram(CoolPropPINVOKE64.AbstractState_umass(swigCPtr));
            }
            else
            {
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                return SpecificEnergy.FromJoulePerKilogram(CoolPropPINVOKE.AbstractState_umass(swigCPtr));
            }
        }
    }
    public SpecificEntropy cpmass()
    {
        lock (lockObj)
        {
            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                return SpecificEntropy.FromJoulePerKilogramKelvin(CoolPropPINVOKE64.AbstractState_cpmass(swigCPtr));
            }
            else
            {
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                return SpecificEntropy.FromJoulePerKilogramKelvin(CoolPropPINVOKE.AbstractState_cpmass(swigCPtr));
            }
        }
    }
    public SpecificEntropy cvmass()
    {
        lock (lockObj)
        {
            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                return SpecificEntropy.FromJoulePerKilogramKelvin(CoolPropPINVOKE64.AbstractState_cvmass(swigCPtr));
            }
            else
            {
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                return SpecificEntropy.FromJoulePerKilogramKelvin(CoolPropPINVOKE.AbstractState_cvmass(swigCPtr));
            }
        }
    }

    public Speed speed_sound()
    {
        lock (lockObj)
        {
            if (Environment.Is64BitProcess)
            {
                if (Q() is not (>=0 and <=1))
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return Speed.FromMeterPerSecond(CoolPropPINVOKE64.AbstractState_speed_sound(swigCPtr));
                }
            }
            else
            {
                if (Q() is not (>=0 and <=1))
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return Speed.FromMeterPerSecond(CoolPropPINVOKE.AbstractState_speed_sound(swigCPtr));
                }
            }

            return Speed.Zero;
        }
    }

    //public PhaseEnvelopeData get_phase_envelope_data()
    //{
    //    PhaseEnvelopeData ret = new PhaseEnvelopeData(CoolPropPINVOKE.AbstractState_get_phase_envelope_data(swigCPtr), false);
    //    if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
    //    return ret;
    //}

    public DynamicViscosity viscosity()
    {
        lock (lockObj)
        {

            double localVis = 0;

            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                localVis = CoolPropPINVOKE64.AbstractState_viscosity(swigCPtr);
            }
            else
            {
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                localVis = CoolPropPINVOKE.AbstractState_viscosity(swigCPtr);
            }

            if (double.IsNaN(localVis) || localVis < (double)decimal.MinValue || localVis > (double)decimal.MaxValue)
                return DynamicViscosity.Zero;
            else
                return DynamicViscosity.FromPascalSecond(localVis);

        }
    }

    public ThermalConductivity conductivity()
    {
        lock (lockObj)
        {

            try
            {
                if (Environment.Is64BitProcess)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return ThermalConductivity.FromWattPerMeterKelvin(CoolPropPINVOKE64.AbstractState_conductivity(swigCPtr));
                }
                else
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return ThermalConductivity.FromWattPerMeterKelvin(CoolPropPINVOKE.AbstractState_conductivity(swigCPtr));
                }
            }
            catch (ArgumentException)
            {
                //NaN error
                return ThermalConductivity.Zero;
            }
        }
    }
    public ForcePerLength surface_tension()
    {
        lock (lockObj)
        {

            if (Environment.Is64BitProcess)
            {
                if (Q() is>0 and <1)
                {
                    CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                    return ForcePerLength.FromNewtonPerMeter(CoolPropPINVOKE64.AbstractState_surface_tension(swigCPtr));
                }
            }
            else
            {
                if (Q() is>0 and <1)
                {
                    CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                    return ForcePerLength.FromNewtonPerMeter(CoolPropPINVOKE.AbstractState_surface_tension(swigCPtr));
                }
            }

            return ForcePerLength.Zero;

        }
    }
    public double Prandtl()
    {
        lock (lockObj)
        {

            double localPrandtl = 0;

            if (Environment.Is64BitProcess)
            {
                CoolPropPINVOKE64.SWIGPendingException.ResetErrors();
                localPrandtl = CoolPropPINVOKE64.AbstractState_Prandtl(swigCPtr);
            }
            else
            {
                CoolPropPINVOKE.SWIGPendingException.ResetErrors();
                localPrandtl = CoolPropPINVOKE.AbstractState_Prandtl(swigCPtr);
            }

            if (double.IsNaN(localPrandtl))
                return 0;
            else
                return localPrandtl;

        }
    }
}
