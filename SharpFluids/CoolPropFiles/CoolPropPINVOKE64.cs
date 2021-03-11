
using System;
using System.IO;
using System.Runtime.InteropServices;

class CoolPropPINVOKE64
{

    protected class SWIGExceptionHelper
    {

        public delegate void ExceptionDelegate(string message);
        public delegate void ExceptionArgumentDelegate(string message, string paramName);

        static ExceptionDelegate applicationDelegate = new ExceptionDelegate(SetPendingApplicationException);
        static ExceptionDelegate arithmeticDelegate = new ExceptionDelegate(SetPendingArithmeticException);
        static ExceptionDelegate divideByZeroDelegate = new ExceptionDelegate(SetPendingDivideByZeroException);
        static ExceptionDelegate indexOutOfRangeDelegate = new ExceptionDelegate(SetPendingIndexOutOfRangeException);
        static ExceptionDelegate invalidCastDelegate = new ExceptionDelegate(SetPendingInvalidCastException);
        static ExceptionDelegate invalidOperationDelegate = new ExceptionDelegate(SetPendingInvalidOperationException);
        static ExceptionDelegate ioDelegate = new ExceptionDelegate(SetPendingIOException);
        static ExceptionDelegate nullReferenceDelegate = new ExceptionDelegate(SetPendingNullReferenceException);
        static ExceptionDelegate outOfMemoryDelegate = new ExceptionDelegate(SetPendingOutOfMemoryException);
        static ExceptionDelegate overflowDelegate = new ExceptionDelegate(SetPendingOverflowException);
        static ExceptionDelegate systemDelegate = new ExceptionDelegate(SetPendingSystemException);

        static ExceptionArgumentDelegate argumentDelegate = new ExceptionArgumentDelegate(SetPendingArgumentException);
        static ExceptionArgumentDelegate argumentNullDelegate = new ExceptionArgumentDelegate(SetPendingArgumentNullException);
        static ExceptionArgumentDelegate argumentOutOfRangeDelegate = new ExceptionArgumentDelegate(SetPendingArgumentOutOfRangeException);




        static void SetPendingApplicationException(string message)
        {
            SWIGPendingException.Set(new ApplicationException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingArithmeticException(string message)
        {
            SWIGPendingException.Set(new ArithmeticException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingDivideByZeroException(string message)
        {
            SWIGPendingException.Set(new DivideByZeroException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingIndexOutOfRangeException(string message)
        {
            SWIGPendingException.Set(new IndexOutOfRangeException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingInvalidCastException(string message)
        {
            SWIGPendingException.Set(new InvalidCastException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingInvalidOperationException(string message)
        {
            SWIGPendingException.Set(new InvalidOperationException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingIOException(string message)
        {
            SWIGPendingException.Set(new IOException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingNullReferenceException(string message)
        {
            SWIGPendingException.Set(new NullReferenceException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingOutOfMemoryException(string message)
        {
            SWIGPendingException.Set(new OutOfMemoryException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingOverflowException(string message)
        {
            SWIGPendingException.Set(new OverflowException(message, SWIGPendingException.Retrieve()));
        }
        static void SetPendingSystemException(string message)
        {
            SWIGPendingException.Set(new SystemException(message, SWIGPendingException.Retrieve()));
        }

        static void SetPendingArgumentException(string message, string paramName)
        {
            SWIGPendingException.Set(new ArgumentException(message, paramName, SWIGPendingException.Retrieve()));
        }
        static void SetPendingArgumentNullException(string message, string paramName)
        {
            Exception e = SWIGPendingException.Retrieve();
            if (e != null) message = message + " Inner Exception: " + e.Message;
            SWIGPendingException.Set(new ArgumentNullException(paramName, message));
        }
        static void SetPendingArgumentOutOfRangeException(string message, string paramName)
        {
            Exception e = SWIGPendingException.Retrieve();
            if (e != null) message = message + " Inner Exception: " + e.Message;
            SWIGPendingException.Set(new ArgumentOutOfRangeException(paramName, message));
        }


    }

    protected static SWIGExceptionHelper swigExceptionHelper = new SWIGExceptionHelper();

    public class SWIGPendingException
    {
        [ThreadStatic]
        private static Exception pendingException = null;
        private static int numExceptionsPending = 0;

        public static bool Pending
        {
            get
            {
                bool pending = false;
                if (numExceptionsPending > 0)
                    if (pendingException != null)
                        pending = true;
                return pending;
            }
        }

        public static void Set(Exception e)
        {
            if (pendingException != null)
                throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + pendingException.ToString() + ")", e);
            pendingException = e;
            lock (typeof(CoolPropPINVOKE))
            {
                numExceptionsPending++;
            }
        }

        public static Exception Retrieve()
        {
            Exception e = null;
            if (numExceptionsPending > 0)
            {
                if (pendingException != null)
                {
                    e = pendingException;
                    pendingException = null;
                    lock (typeof(CoolPropPINVOKE))
                    {
                        numExceptionsPending--;
                    }
                }
            }
            return e;
        }
    }


    protected class SWIGStringHelper
    {

        public delegate string SWIGStringDelegate(string message);
        static SWIGStringDelegate stringDelegate = new SWIGStringDelegate(CreateString);

        [DllImport("CoolProp64", EntryPoint = "SWIGRegisterStringCallback_CoolProp")]
        public static extern void SWIGRegisterStringCallback_CoolProp(SWIGStringDelegate stringDelegate);

        static string CreateString(string cString)
        {
            return cString;
        }

        static SWIGStringHelper()
        {
            SWIGRegisterStringCallback_CoolProp(stringDelegate);
        }
    }

    static protected SWIGStringHelper swigStringHelper = new SWIGStringHelper();



    [DllImport("CoolProp64", EntryPoint = "CSharp_DoubleVector_Add")]
    public static extern void DoubleVector_Add(HandleRef jarg1, double jarg2);

    [DllImport("CoolProp64", EntryPoint = "CSharp_new_DoubleVector__SWIG_0")]
    public static extern IntPtr new_DoubleVector__SWIG_0();

    [DllImport("CoolProp64", EntryPoint = "CSharp_delete_DoubleVector")]
    public static extern void delete_DoubleVector(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_delete_AbstractState")]
    public static extern void delete_AbstractState(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_factory__SWIG_0")]
    public static extern IntPtr AbstractState_factory__SWIG_0(string jarg1, string jarg2);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_backend_name")]
    public static extern string AbstractState_backend_name(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_using_mass_fractions")]
    public static extern bool AbstractState_using_mass_fractions(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_set_mass_fractions")]
    public static extern void AbstractState_set_mass_fractions(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_set_volu_fractions")]
    public static extern void AbstractState_set_volu_fractions(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_update")]
    public static extern void AbstractState_update(HandleRef jarg1, int jarg2, double jarg3, double jarg4);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_Tmin")]
    public static extern double AbstractState_Tmin(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_Tmax")]
    public static extern double AbstractState_Tmax(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_pmax")]
    public static extern double AbstractState_pmax(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_T_critical")]
    public static extern double AbstractState_T_critical(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_p_critical")]
    public static extern double AbstractState_p_critical(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_p_triple")]
    public static extern double AbstractState_p_triple(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_name")]
    public static extern string AbstractState_name(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_keyed_output")]
    public static extern double AbstractState_keyed_output(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_T")]
    public static extern double AbstractState_T(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_rhomass")]
    public static extern double AbstractState_rhomass(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_p")]
    public static extern double AbstractState_p(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_Q")]
    public static extern double AbstractState_Q(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_molar_mass")]
    public static extern double AbstractState_molar_mass(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_compressibility_factor")]
    public static extern double AbstractState_compressibility_factor(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_hmass")]
    public static extern double AbstractState_hmass(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_smass")]
    public static extern double AbstractState_smass(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_umass")]
    public static extern double AbstractState_umass(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_cpmass")]
    public static extern double AbstractState_cpmass(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_cvmass")]
    public static extern double AbstractState_cvmass(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_speed_sound")]
    public static extern double AbstractState_speed_sound(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_viscosity")]
    public static extern double AbstractState_viscosity(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_conductivity")]
    public static extern double AbstractState_conductivity(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_surface_tension")]
    public static extern double AbstractState_surface_tension(HandleRef jarg1);

    [DllImport("CoolProp64", EntryPoint = "CSharp_AbstractState_Prandtl")]
    public static extern double AbstractState_Prandtl(HandleRef jarg1);



}
