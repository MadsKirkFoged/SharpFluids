using System;
using System.IO;
using System.Runtime.InteropServices;

class CoolPropPINVOKE
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


        [DllImport("CoolProp", EntryPoint = "SWIGRegisterExceptionCallbacks_CoolProp")]
        public static extern void SWIGRegisterExceptionCallbacks_CoolProp(
                                ExceptionDelegate applicationDelegate,
                                ExceptionDelegate arithmeticDelegate,
                                ExceptionDelegate divideByZeroDelegate,
                                ExceptionDelegate indexOutOfRangeDelegate,
                                ExceptionDelegate invalidCastDelegate,
                                ExceptionDelegate invalidOperationDelegate,
                                ExceptionDelegate ioDelegate,
                                ExceptionDelegate nullReferenceDelegate,
                                ExceptionDelegate outOfMemoryDelegate,
                                ExceptionDelegate overflowDelegate,
                                ExceptionDelegate systemExceptionDelegate);

        [DllImport("CoolProp", EntryPoint = "SWIGRegisterExceptionArgumentCallbacks_CoolProp")]
        public static extern void SWIGRegisterExceptionCallbacksArgument_CoolProp(
                            ExceptionArgumentDelegate argumentDelegate,
                            ExceptionArgumentDelegate argumentNullDelegate,
                            ExceptionArgumentDelegate argumentOutOfRangeDelegate);

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

        static SWIGExceptionHelper()
        {
            SWIGRegisterExceptionCallbacks_CoolProp(
                                      applicationDelegate,
                                      arithmeticDelegate,
                                      divideByZeroDelegate,
                                      indexOutOfRangeDelegate,
                                      invalidCastDelegate,
                                      invalidOperationDelegate,
                                      ioDelegate,
                                      nullReferenceDelegate,
                                      outOfMemoryDelegate,
                                      overflowDelegate,
                                      systemDelegate);

            SWIGRegisterExceptionCallbacksArgument_CoolProp(
                                      argumentDelegate,
                                      argumentNullDelegate,
                                      argumentOutOfRangeDelegate);
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

        [DllImport("CoolProp", EntryPoint = "SWIGRegisterStringCallback_CoolProp")]
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


    static CoolPropPINVOKE()
    {
    }


    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_Clear")]
    public static extern void DoubleVector_Clear(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_Add")]
    public static extern void DoubleVector_Add(HandleRef jarg1, double jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_size")]
    public static extern uint DoubleVector_size(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_capacity")]
    public static extern uint DoubleVector_capacity(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_reserve")]
    public static extern void DoubleVector_reserve(HandleRef jarg1, uint jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_new_DoubleVector__SWIG_0")]
    public static extern IntPtr new_DoubleVector__SWIG_0();

    [DllImport("CoolProp", EntryPoint = "CSharp_new_DoubleVector__SWIG_1")]
    public static extern IntPtr new_DoubleVector__SWIG_1(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_new_DoubleVector__SWIG_2")]
    public static extern IntPtr new_DoubleVector__SWIG_2(int jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_getitemcopy")]
    public static extern double DoubleVector_getitemcopy(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_getitem")]
    public static extern double DoubleVector_getitem(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_setitem")]
    public static extern void DoubleVector_setitem(HandleRef jarg1, int jarg2, double jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_AddRange")]
    public static extern void DoubleVector_AddRange(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_GetRange")]
    public static extern IntPtr DoubleVector_GetRange(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_Insert")]
    public static extern void DoubleVector_Insert(HandleRef jarg1, int jarg2, double jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_InsertRange")]
    public static extern void DoubleVector_InsertRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_RemoveAt")]
    public static extern void DoubleVector_RemoveAt(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_RemoveRange")]
    public static extern void DoubleVector_RemoveRange(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_Repeat")]
    public static extern IntPtr DoubleVector_Repeat(double jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_Reverse__SWIG_0")]
    public static extern void DoubleVector_Reverse__SWIG_0(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_Reverse__SWIG_1")]
    public static extern void DoubleVector_Reverse__SWIG_1(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_SetRange")]
    public static extern void DoubleVector_SetRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_Contains")]
    public static extern bool DoubleVector_Contains(HandleRef jarg1, double jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_IndexOf")]
    public static extern int DoubleVector_IndexOf(HandleRef jarg1, double jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_LastIndexOf")]
    public static extern int DoubleVector_LastIndexOf(HandleRef jarg1, double jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_DoubleVector_Remove")]
    public static extern bool DoubleVector_Remove(HandleRef jarg1, double jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_delete_DoubleVector")]
    public static extern void delete_DoubleVector(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_Clear")]
    public static extern void VectorOfDoubleVector_Clear(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_Add")]
    public static extern void VectorOfDoubleVector_Add(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_size")]
    public static extern uint VectorOfDoubleVector_size(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_capacity")]
    public static extern uint VectorOfDoubleVector_capacity(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_reserve")]
    public static extern void VectorOfDoubleVector_reserve(HandleRef jarg1, uint jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_new_VectorOfDoubleVector__SWIG_0")]
    public static extern IntPtr new_VectorOfDoubleVector__SWIG_0();

    [DllImport("CoolProp", EntryPoint = "CSharp_new_VectorOfDoubleVector__SWIG_1")]
    public static extern IntPtr new_VectorOfDoubleVector__SWIG_1(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_new_VectorOfDoubleVector__SWIG_2")]
    public static extern IntPtr new_VectorOfDoubleVector__SWIG_2(int jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_getitemcopy")]
    public static extern IntPtr VectorOfDoubleVector_getitemcopy(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_getitem")]
    public static extern IntPtr VectorOfDoubleVector_getitem(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_setitem")]
    public static extern void VectorOfDoubleVector_setitem(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_AddRange")]
    public static extern void VectorOfDoubleVector_AddRange(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_GetRange")]
    public static extern IntPtr VectorOfDoubleVector_GetRange(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_Insert")]
    public static extern void VectorOfDoubleVector_Insert(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_InsertRange")]
    public static extern void VectorOfDoubleVector_InsertRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_RemoveAt")]
    public static extern void VectorOfDoubleVector_RemoveAt(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_RemoveRange")]
    public static extern void VectorOfDoubleVector_RemoveRange(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_Repeat")]
    public static extern IntPtr VectorOfDoubleVector_Repeat(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_Reverse__SWIG_0")]
    public static extern void VectorOfDoubleVector_Reverse__SWIG_0(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_Reverse__SWIG_1")]
    public static extern void VectorOfDoubleVector_Reverse__SWIG_1(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfDoubleVector_SetRange")]
    public static extern void VectorOfDoubleVector_SetRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_delete_VectorOfDoubleVector")]
    public static extern void delete_VectorOfDoubleVector(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_Clear")]
    public static extern void StringVector_Clear(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_Add")]
    public static extern void StringVector_Add(HandleRef jarg1, string jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_size")]
    public static extern uint StringVector_size(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_capacity")]
    public static extern uint StringVector_capacity(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_reserve")]
    public static extern void StringVector_reserve(HandleRef jarg1, uint jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_new_StringVector__SWIG_0")]
    public static extern IntPtr new_StringVector__SWIG_0();

    [DllImport("CoolProp", EntryPoint = "CSharp_new_StringVector__SWIG_1")]
    public static extern IntPtr new_StringVector__SWIG_1(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_new_StringVector__SWIG_2")]
    public static extern IntPtr new_StringVector__SWIG_2(int jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_getitemcopy")]
    public static extern string StringVector_getitemcopy(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_getitem")]
    public static extern string StringVector_getitem(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_setitem")]
    public static extern void StringVector_setitem(HandleRef jarg1, int jarg2, string jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_AddRange")]
    public static extern void StringVector_AddRange(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_GetRange")]
    public static extern IntPtr StringVector_GetRange(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_Insert")]
    public static extern void StringVector_Insert(HandleRef jarg1, int jarg2, string jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_InsertRange")]
    public static extern void StringVector_InsertRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_RemoveAt")]
    public static extern void StringVector_RemoveAt(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_RemoveRange")]
    public static extern void StringVector_RemoveRange(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_Repeat")]
    public static extern IntPtr StringVector_Repeat(string jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_Reverse__SWIG_0")]
    public static extern void StringVector_Reverse__SWIG_0(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_Reverse__SWIG_1")]
    public static extern void StringVector_Reverse__SWIG_1(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_SetRange")]
    public static extern void StringVector_SetRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_Contains")]
    public static extern bool StringVector_Contains(HandleRef jarg1, string jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_IndexOf")]
    public static extern int StringVector_IndexOf(HandleRef jarg1, string jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_LastIndexOf")]
    public static extern int StringVector_LastIndexOf(HandleRef jarg1, string jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_StringVector_Remove")]
    public static extern bool StringVector_Remove(HandleRef jarg1, string jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_delete_StringVector")]
    public static extern void delete_StringVector(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_Clear")]
    public static extern void VectorOfStringVector_Clear(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_Add")]
    public static extern void VectorOfStringVector_Add(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_size")]
    public static extern uint VectorOfStringVector_size(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_capacity")]
    public static extern uint VectorOfStringVector_capacity(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_reserve")]
    public static extern void VectorOfStringVector_reserve(HandleRef jarg1, uint jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_new_VectorOfStringVector__SWIG_0")]
    public static extern IntPtr new_VectorOfStringVector__SWIG_0();

    [DllImport("CoolProp", EntryPoint = "CSharp_new_VectorOfStringVector__SWIG_1")]
    public static extern IntPtr new_VectorOfStringVector__SWIG_1(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_new_VectorOfStringVector__SWIG_2")]
    public static extern IntPtr new_VectorOfStringVector__SWIG_2(int jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_getitemcopy")]
    public static extern IntPtr VectorOfStringVector_getitemcopy(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_getitem")]
    public static extern IntPtr VectorOfStringVector_getitem(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_setitem")]
    public static extern void VectorOfStringVector_setitem(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_AddRange")]
    public static extern void VectorOfStringVector_AddRange(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_GetRange")]
    public static extern IntPtr VectorOfStringVector_GetRange(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_Insert")]
    public static extern void VectorOfStringVector_Insert(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_InsertRange")]
    public static extern void VectorOfStringVector_InsertRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_RemoveAt")]
    public static extern void VectorOfStringVector_RemoveAt(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_RemoveRange")]
    public static extern void VectorOfStringVector_RemoveRange(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_Repeat")]
    public static extern IntPtr VectorOfStringVector_Repeat(HandleRef jarg1, int jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_Reverse__SWIG_0")]
    public static extern void VectorOfStringVector_Reverse__SWIG_0(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_Reverse__SWIG_1")]
    public static extern void VectorOfStringVector_Reverse__SWIG_1(HandleRef jarg1, int jarg2, int jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_VectorOfStringVector_SetRange")]
    public static extern void VectorOfStringVector_SetRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

    [DllImport("CoolProp", EntryPoint = "CSharp_delete_VectorOfStringVector")]
    public static extern void delete_VectorOfStringVector(HandleRef jarg1);











    [DllImport("CoolProp", EntryPoint = "CSharp_delete_AbstractState")]
    public static extern void delete_AbstractState(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_factory__SWIG_0")]
    public static extern IntPtr AbstractState_factory__SWIG_0(string jarg1, string jarg2);


    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_backend_name")]
    public static extern string AbstractState_backend_name(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_using_mass_fractions")]
    public static extern bool AbstractState_using_mass_fractions(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_set_mass_fractions")]
    public static extern void AbstractState_set_mass_fractions(HandleRef jarg1, HandleRef jarg2);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_set_volu_fractions")]
    public static extern void AbstractState_set_volu_fractions(HandleRef jarg1, HandleRef jarg2);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_update")]
    public static extern void AbstractState_update(HandleRef jarg1, int jarg2, double jarg3, double jarg4);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_Tmin")]
    public static extern double AbstractState_Tmin(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_Tmax")]
    public static extern double AbstractState_Tmax(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_pmax")]
    public static extern double AbstractState_pmax(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_T_critical")]
    public static extern double AbstractState_T_critical(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_p_critical")]
    public static extern double AbstractState_p_critical(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_p_triple")]
    public static extern double AbstractState_p_triple(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_name")]
    public static extern string AbstractState_name(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_keyed_output")]
    public static extern double AbstractState_keyed_output(HandleRef jarg1, int jarg2);


    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_T")]
    public static extern double AbstractState_T(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_rhomass")]
    public static extern double AbstractState_rhomass(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_p")]
    public static extern double AbstractState_p(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_Q")]
    public static extern double AbstractState_Q(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_molar_mass")]
    public static extern double AbstractState_molar_mass(HandleRef jarg1);





    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_compressibility_factor")]
    public static extern double AbstractState_compressibility_factor(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_hmass")]
    public static extern double AbstractState_hmass(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_smass")]
    public static extern double AbstractState_smass(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_umass")]
    public static extern double AbstractState_umass(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_cpmass")]
    public static extern double AbstractState_cpmass(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_cvmass")]
    public static extern double AbstractState_cvmass(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_speed_sound")]
    public static extern double AbstractState_speed_sound(HandleRef jarg1);





    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_viscosity")]
    public static extern double AbstractState_viscosity(HandleRef jarg1);


    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_conductivity")]
    public static extern double AbstractState_conductivity(HandleRef jarg1);



    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_surface_tension")]
    public static extern double AbstractState_surface_tension(HandleRef jarg1);

    [DllImport("CoolProp", EntryPoint = "CSharp_AbstractState_Prandtl")]
    public static extern double AbstractState_Prandtl(HandleRef jarg1);



}