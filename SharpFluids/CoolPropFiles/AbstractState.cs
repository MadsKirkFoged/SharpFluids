
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnitsNet;

public class AbstractState : IDisposable
{

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
            if (swigCPtr.Handle != global::System.IntPtr.Zero)
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

    public static AbstractState factory(string backend, string fluid_names)
    {

        if (Environment.Is64BitProcess)
        {
            IntPtr cPtr = CoolPropPINVOKE64.AbstractState_factory__SWIG_0(backend, fluid_names);
            AbstractState ret = (cPtr == IntPtr.Zero) ? null : new AbstractState(cPtr, false);
            if (CoolPropPINVOKE64.SWIGPendingException.Pending) throw CoolPropPINVOKE64.SWIGPendingException.Retrieve();
            return ret;
        }
        else
        {
            IntPtr cPtr = CoolPropPINVOKE.AbstractState_factory__SWIG_0(backend, fluid_names);
            AbstractState ret = (cPtr == IntPtr.Zero) ? null : new AbstractState(cPtr, false);
            if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            return ret;

        }


    }
    public virtual void update(input_pairs input_pair, double Value1, double Value2)
    {

        if (Environment.Is64BitProcess)
        {
            CoolPropPINVOKE64.AbstractState_update(swigCPtr, (int)input_pair, Value1, Value2);
            if (CoolPropPINVOKE64.SWIGPendingException.Pending) throw CoolPropPINVOKE64.SWIGPendingException.Retrieve();
        }
        else
        {
            CoolPropPINVOKE.AbstractState_update(swigCPtr, (int)input_pair, Value1, Value2);
            if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
        }

       
    }

    public virtual string backend_name()
    {

        if (Environment.Is64BitProcess)
        {
            string ret = CoolPropPINVOKE64.AbstractState_backend_name(swigCPtr);
            if (CoolPropPINVOKE64.SWIGPendingException.Pending) throw CoolPropPINVOKE64.SWIGPendingException.Retrieve();
            return ret;
        }
        else
        {
            string ret = CoolPropPINVOKE.AbstractState_backend_name(swigCPtr);
            if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }


    }
    public virtual bool using_mass_fractions()
    {

        if (Environment.Is64BitProcess)
        {
            bool ret = CoolPropPINVOKE64.AbstractState_using_mass_fractions(swigCPtr);
            //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }
        else
        {
            bool ret = CoolPropPINVOKE.AbstractState_using_mass_fractions(swigCPtr);
            //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }




    }
    public void set_mass_fractions(DoubleVector mass_fractions)
    {

        if (Environment.Is64BitProcess)
        {
            CoolPropPINVOKE64.AbstractState_set_mass_fractions(swigCPtr, DoubleVector.getCPtr(mass_fractions));
            //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
        }
        else
        {
            CoolPropPINVOKE.AbstractState_set_mass_fractions(swigCPtr, DoubleVector.getCPtr(mass_fractions));
            //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
        }


    }
    public void set_volu_fractions(DoubleVector volu_fractions)
    {

        if (Environment.Is64BitProcess)
        {
            CoolPropPINVOKE64.AbstractState_set_volu_fractions(swigCPtr, DoubleVector.getCPtr(volu_fractions));
            //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
        }
        else
        {
            CoolPropPINVOKE.AbstractState_set_volu_fractions(swigCPtr, DoubleVector.getCPtr(volu_fractions));
            //if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
        }


    }

    public Temperature Tmin()
    {
        if (Environment.Is64BitProcess)
            return Temperature.FromKelvins(CoolPropPINVOKE64.AbstractState_Tmin(swigCPtr));
        else
            return Temperature.FromKelvins(CoolPropPINVOKE.AbstractState_Tmin(swigCPtr));
    }
    public Temperature Tmax()
    {
        if (Environment.Is64BitProcess)
            return Temperature.FromKelvins(CoolPropPINVOKE64.AbstractState_Tmax(swigCPtr));
        else
            return Temperature.FromKelvins(CoolPropPINVOKE.AbstractState_Tmax(swigCPtr));
    }
    public Pressure pmax()
    {
        if (Environment.Is64BitProcess)
            return Pressure.FromPascals(CoolPropPINVOKE64.AbstractState_pmax(swigCPtr));
        else
            return Pressure.FromPascals(CoolPropPINVOKE.AbstractState_pmax(swigCPtr));
    }
    public Temperature T_critical()
    {
        if (Environment.Is64BitProcess)        
            return Temperature.FromKelvins(CoolPropPINVOKE64.AbstractState_T_critical(swigCPtr));        
        else        
            return Temperature.FromKelvins(CoolPropPINVOKE.AbstractState_T_critical(swigCPtr));      
    }
    public Pressure p_critical()
    {
        if (Environment.Is64BitProcess)        
            return Pressure.FromPascals(CoolPropPINVOKE64.AbstractState_p_critical(swigCPtr));
        else
            return Pressure.FromPascals(CoolPropPINVOKE.AbstractState_p_critical(swigCPtr));        
    }
    public Pressure p_triple()
    {
        if (Environment.Is64BitProcess)        
            return Pressure.FromPascals(CoolPropPINVOKE64.AbstractState_p_triple(swigCPtr));        
        else        
            return Pressure.FromPascals(CoolPropPINVOKE.AbstractState_p_triple(swigCPtr));
        
    }
    public string name()
    {
        if (Environment.Is64BitProcess)        
            return CoolPropPINVOKE64.AbstractState_name(swigCPtr);
        else        
            return CoolPropPINVOKE.AbstractState_name(swigCPtr);       
    }
    public double keyed_output(parameters key)
    {
        if (Environment.Is64BitProcess)        
            return CoolPropPINVOKE64.AbstractState_keyed_output(swigCPtr, (int)key);
        else        
            return CoolPropPINVOKE.AbstractState_keyed_output(swigCPtr, (int)key);        
    }
    public Temperature T()
    {
        if (Environment.Is64BitProcess)        
            return Temperature.FromKelvins(CoolPropPINVOKE64.AbstractState_T(swigCPtr));
        else        
            return Temperature.FromKelvins(CoolPropPINVOKE.AbstractState_T(swigCPtr));        
    }
    public Density rhomass()
    {
        if (Environment.Is64BitProcess)        
            return Density.FromKilogramsPerCubicMeter(CoolPropPINVOKE64.AbstractState_rhomass(swigCPtr));
        else        
            return Density.FromKilogramsPerCubicMeter(CoolPropPINVOKE.AbstractState_rhomass(swigCPtr));        
    }
    public Pressure p()
    {
        if (Environment.Is64BitProcess)        
            return Pressure.FromPascals(CoolPropPINVOKE64.AbstractState_p(swigCPtr));
        else        
            return Pressure.FromPascals(CoolPropPINVOKE.AbstractState_p(swigCPtr));        
    }
    public double Q()
    {
        if (Environment.Is64BitProcess)        
            return CoolPropPINVOKE64.AbstractState_Q(swigCPtr);        
        else        
            return CoolPropPINVOKE.AbstractState_Q(swigCPtr);        
    }
    public MolarMass molar_mass()
    {
        if (Environment.Is64BitProcess)        
            return MolarMass.FromKilogramsPerMole(CoolPropPINVOKE64.AbstractState_molar_mass(swigCPtr));
        else        
            return MolarMass.FromKilogramsPerMole(CoolPropPINVOKE.AbstractState_molar_mass(swigCPtr));     
    }
    public double compressibility_factor()
    {

        if (Environment.Is64BitProcess)        
            return CoolPropPINVOKE64.AbstractState_compressibility_factor(swigCPtr);
        else        
            return CoolPropPINVOKE.AbstractState_compressibility_factor(swigCPtr);        
    }
    public SpecificEnergy hmass()
    {
        if (Environment.Is64BitProcess)        
            return SpecificEnergy.FromJoulesPerKilogram(CoolPropPINVOKE64.AbstractState_hmass(swigCPtr));
        else        
            return SpecificEnergy.FromJoulesPerKilogram(CoolPropPINVOKE.AbstractState_hmass(swigCPtr));        
    }
    public Entropy smass()
    {
        if (Environment.Is64BitProcess)        
            return Entropy.FromJoulesPerKelvin(CoolPropPINVOKE64.AbstractState_smass(swigCPtr));
        else        
            return Entropy.FromJoulesPerKelvin(CoolPropPINVOKE.AbstractState_smass(swigCPtr));        
    }
    public SpecificEnergy umass()
    {
        if (Environment.Is64BitProcess)        
            return SpecificEnergy.FromJoulesPerKilogram(CoolPropPINVOKE64.AbstractState_umass(swigCPtr));
        else        
            return SpecificEnergy.FromJoulesPerKilogram(CoolPropPINVOKE.AbstractState_umass(swigCPtr));      
    }
    public SpecificEntropy cpmass()
    {
        if (Environment.Is64BitProcess)        
            return SpecificEntropy.FromJoulesPerKilogramKelvin(CoolPropPINVOKE64.AbstractState_cpmass(swigCPtr));
        else        
            return SpecificEntropy.FromJoulesPerKilogramKelvin(CoolPropPINVOKE.AbstractState_cpmass(swigCPtr));        
    }
    public SpecificEntropy cvmass()
    {
        if (Environment.Is64BitProcess)        
            return SpecificEntropy.FromJoulesPerKilogramKelvin(CoolPropPINVOKE64.AbstractState_cvmass(swigCPtr));        
        else        
            return SpecificEntropy.FromJoulesPerKilogramKelvin(CoolPropPINVOKE.AbstractState_cvmass(swigCPtr));        
    }
    public Speed speed_sound()
    {

        if (Environment.Is64BitProcess)
        {
            if (!(0 <= Q() && Q() <= 1))            
                return Speed.FromMetersPerSecond(CoolPropPINVOKE64.AbstractState_speed_sound(swigCPtr));
        }
        else
        {
            if (!(0 <= Q() && Q() <= 1))            
                return Speed.FromMetersPerSecond(CoolPropPINVOKE.AbstractState_speed_sound(swigCPtr));            
        }

        return Speed.Zero;

    }


    //public PhaseEnvelopeData get_phase_envelope_data()
    //{
    //    PhaseEnvelopeData ret = new PhaseEnvelopeData(CoolPropPINVOKE.AbstractState_get_phase_envelope_data(swigCPtr), false);
    //    if (CoolPropPINVOKE.SWIGPendingException.Pending) throw CoolPropPINVOKE.SWIGPendingException.Retrieve();
    //    return ret;
    //}


    public DynamicViscosity viscosity()
    {
        double localVis = 0;

        if (Environment.Is64BitProcess)
            localVis = CoolPropPINVOKE64.AbstractState_viscosity(swigCPtr);
        else
            localVis = CoolPropPINVOKE.AbstractState_viscosity(swigCPtr);


        if (double.IsNaN(localVis))        
            return DynamicViscosity.Zero;
        else
            return DynamicViscosity.FromPascalSeconds(localVis);
    }
    public ThermalConductivity conductivity()
    {

        try
        {
            if (Environment.Is64BitProcess)        
                return ThermalConductivity.FromWattsPerMeterKelvin(CoolPropPINVOKE64.AbstractState_conductivity(swigCPtr));
            else        
                return ThermalConductivity.FromWattsPerMeterKelvin(CoolPropPINVOKE.AbstractState_conductivity(swigCPtr));        

        }
        catch (ArgumentException)
        {
            //NaN error
            return ThermalConductivity.Zero;
        }

    }
    public ForcePerLength surface_tension()
    {

        if (Environment.Is64BitProcess)
        {
            if (0 < Q() && Q() < 1)            
                return ForcePerLength.FromNewtonsPerMeter(CoolPropPINVOKE64.AbstractState_surface_tension(swigCPtr));            
        }
        else
        {
            if (0 < Q() && Q() < 1)            
                return ForcePerLength.FromNewtonsPerMeter(CoolPropPINVOKE.AbstractState_surface_tension(swigCPtr));            
        }


        return ForcePerLength.Zero;
    }
    public double Prandtl()
    {
        double localPrandtl = 0;

        if (Environment.Is64BitProcess)
            localPrandtl = CoolPropPINVOKE64.AbstractState_Prandtl(swigCPtr);
        else
            localPrandtl = CoolPropPINVOKE.AbstractState_Prandtl(swigCPtr);

        if (double.IsNaN(localPrandtl))
            return 0;
        else
            return localPrandtl;
    }

}
