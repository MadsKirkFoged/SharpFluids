
using System;
using System.IO;
using System.Runtime.InteropServices;

class CoolPropPINVOKE
{

  protected class SWIGExceptionHelper {

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

    [DllImport("CoolProp", EntryPoint="SWIGRegisterExceptionCallbacks_CoolProp")]
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

    [DllImport("CoolProp", EntryPoint="SWIGRegisterExceptionArgumentCallbacks_CoolProp")]
    public static extern void SWIGRegisterExceptionCallbacksArgument_CoolProp(
                                ExceptionArgumentDelegate argumentDelegate,
                                ExceptionArgumentDelegate argumentNullDelegate,
                                ExceptionArgumentDelegate argumentOutOfRangeDelegate);

    static void SetPendingApplicationException(string message) {
      SWIGPendingException.Set(new ApplicationException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingArithmeticException(string message) {
      SWIGPendingException.Set(new ArithmeticException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingDivideByZeroException(string message) {
      SWIGPendingException.Set(new DivideByZeroException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingIndexOutOfRangeException(string message) {
      SWIGPendingException.Set(new IndexOutOfRangeException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingInvalidCastException(string message) {
      SWIGPendingException.Set(new InvalidCastException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingInvalidOperationException(string message) {
      SWIGPendingException.Set(new InvalidOperationException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingIOException(string message) {
      SWIGPendingException.Set(new IOException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingNullReferenceException(string message) {
      SWIGPendingException.Set(new NullReferenceException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingOutOfMemoryException(string message) {
      SWIGPendingException.Set(new OutOfMemoryException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingOverflowException(string message) {
      SWIGPendingException.Set(new OverflowException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingSystemException(string message) {
      SWIGPendingException.Set(new SystemException(message, SWIGPendingException.Retrieve()));
    }

    static void SetPendingArgumentException(string message, string paramName) {
      SWIGPendingException.Set(new ArgumentException(message, paramName, SWIGPendingException.Retrieve()));
    }
    static void SetPendingArgumentNullException(string message, string paramName) 
    {
      Exception e = SWIGPendingException.Retrieve();
      if (e != null) message = message + " Inner Exception: " + e.Message;
      SWIGPendingException.Set(new ArgumentNullException(paramName, message));
    }
    static void SetPendingArgumentOutOfRangeException(string message, string paramName) {
      Exception e = SWIGPendingException.Retrieve();
      if (e != null) message = message + " Inner Exception: " + e.Message;
      SWIGPendingException.Set(new ArgumentOutOfRangeException(paramName, message));
    }

    static SWIGExceptionHelper() {
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

  public class SWIGPendingException {
    [ThreadStatic]
    private static Exception pendingException = null;
    private static int numExceptionsPending = 0;

    public static bool Pending {
      get {
        bool pending = false;
        if (numExceptionsPending > 0)
          if (pendingException != null)
            pending = true;
        return pending;
      } 
    }

    public static void Set(Exception e) {
      if (pendingException != null)
        throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + pendingException.ToString() + ")", e);
      pendingException = e;
      lock(typeof(CoolPropPINVOKE)) {
        numExceptionsPending++;
      }
    }

    public static Exception Retrieve() {
      Exception e = null;
      if (numExceptionsPending > 0) {
        if (pendingException != null) {
          e = pendingException;
          pendingException = null;
          lock(typeof(CoolPropPINVOKE)) {
            numExceptionsPending--;
          }
        }
      }
      return e;
    }
  }


  protected class SWIGStringHelper {

    public delegate string SWIGStringDelegate(string message);
    static SWIGStringDelegate stringDelegate = new SWIGStringDelegate(CreateString);

    [DllImport("CoolProp", EntryPoint="SWIGRegisterStringCallback_CoolProp")]
    public static extern void SWIGRegisterStringCallback_CoolProp(SWIGStringDelegate stringDelegate);

    static string CreateString(string cString) {
      return cString;
    }

    static SWIGStringHelper() {
      SWIGRegisterStringCallback_CoolProp(stringDelegate);
    }
  }

  static protected SWIGStringHelper swigStringHelper = new SWIGStringHelper();


  static CoolPropPINVOKE() {
  }


  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_Clear")]
  public static extern void DoubleVector_Clear(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_Add")]
  public static extern void DoubleVector_Add(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_size")]
  public static extern uint DoubleVector_size(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_capacity")]
  public static extern uint DoubleVector_capacity(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_reserve")]
  public static extern void DoubleVector_reserve(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_new_DoubleVector__SWIG_0")]
  public static extern IntPtr new_DoubleVector__SWIG_0();

  [DllImport("CoolProp", EntryPoint="CSharp_new_DoubleVector__SWIG_1")]
  public static extern IntPtr new_DoubleVector__SWIG_1(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_DoubleVector__SWIG_2")]
  public static extern IntPtr new_DoubleVector__SWIG_2(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_getitemcopy")]
  public static extern double DoubleVector_getitemcopy(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_getitem")]
  public static extern double DoubleVector_getitem(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_setitem")]
  public static extern void DoubleVector_setitem(HandleRef jarg1, int jarg2, double jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_AddRange")]
  public static extern void DoubleVector_AddRange(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_GetRange")]
  public static extern IntPtr DoubleVector_GetRange(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_Insert")]
  public static extern void DoubleVector_Insert(HandleRef jarg1, int jarg2, double jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_InsertRange")]
  public static extern void DoubleVector_InsertRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_RemoveAt")]
  public static extern void DoubleVector_RemoveAt(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_RemoveRange")]
  public static extern void DoubleVector_RemoveRange(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_Repeat")]
  public static extern IntPtr DoubleVector_Repeat(double jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_Reverse__SWIG_0")]
  public static extern void DoubleVector_Reverse__SWIG_0(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_Reverse__SWIG_1")]
  public static extern void DoubleVector_Reverse__SWIG_1(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_SetRange")]
  public static extern void DoubleVector_SetRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_Contains")]
  public static extern bool DoubleVector_Contains(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_IndexOf")]
  public static extern int DoubleVector_IndexOf(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_LastIndexOf")]
  public static extern int DoubleVector_LastIndexOf(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_DoubleVector_Remove")]
  public static extern bool DoubleVector_Remove(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_DoubleVector")]
  public static extern void delete_DoubleVector(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_Clear")]
  public static extern void VectorOfDoubleVector_Clear(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_Add")]
  public static extern void VectorOfDoubleVector_Add(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_size")]
  public static extern uint VectorOfDoubleVector_size(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_capacity")]
  public static extern uint VectorOfDoubleVector_capacity(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_reserve")]
  public static extern void VectorOfDoubleVector_reserve(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_new_VectorOfDoubleVector__SWIG_0")]
  public static extern IntPtr new_VectorOfDoubleVector__SWIG_0();

  [DllImport("CoolProp", EntryPoint="CSharp_new_VectorOfDoubleVector__SWIG_1")]
  public static extern IntPtr new_VectorOfDoubleVector__SWIG_1(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_VectorOfDoubleVector__SWIG_2")]
  public static extern IntPtr new_VectorOfDoubleVector__SWIG_2(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_getitemcopy")]
  public static extern IntPtr VectorOfDoubleVector_getitemcopy(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_getitem")]
  public static extern IntPtr VectorOfDoubleVector_getitem(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_setitem")]
  public static extern void VectorOfDoubleVector_setitem(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_AddRange")]
  public static extern void VectorOfDoubleVector_AddRange(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_GetRange")]
  public static extern IntPtr VectorOfDoubleVector_GetRange(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_Insert")]
  public static extern void VectorOfDoubleVector_Insert(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_InsertRange")]
  public static extern void VectorOfDoubleVector_InsertRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_RemoveAt")]
  public static extern void VectorOfDoubleVector_RemoveAt(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_RemoveRange")]
  public static extern void VectorOfDoubleVector_RemoveRange(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_Repeat")]
  public static extern IntPtr VectorOfDoubleVector_Repeat(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_Reverse__SWIG_0")]
  public static extern void VectorOfDoubleVector_Reverse__SWIG_0(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_Reverse__SWIG_1")]
  public static extern void VectorOfDoubleVector_Reverse__SWIG_1(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfDoubleVector_SetRange")]
  public static extern void VectorOfDoubleVector_SetRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_VectorOfDoubleVector")]
  public static extern void delete_VectorOfDoubleVector(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_Clear")]
  public static extern void StringVector_Clear(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_Add")]
  public static extern void StringVector_Add(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_size")]
  public static extern uint StringVector_size(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_capacity")]
  public static extern uint StringVector_capacity(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_reserve")]
  public static extern void StringVector_reserve(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_new_StringVector__SWIG_0")]
  public static extern IntPtr new_StringVector__SWIG_0();

  [DllImport("CoolProp", EntryPoint="CSharp_new_StringVector__SWIG_1")]
  public static extern IntPtr new_StringVector__SWIG_1(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_StringVector__SWIG_2")]
  public static extern IntPtr new_StringVector__SWIG_2(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_getitemcopy")]
  public static extern string StringVector_getitemcopy(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_getitem")]
  public static extern string StringVector_getitem(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_setitem")]
  public static extern void StringVector_setitem(HandleRef jarg1, int jarg2, string jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_AddRange")]
  public static extern void StringVector_AddRange(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_GetRange")]
  public static extern IntPtr StringVector_GetRange(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_Insert")]
  public static extern void StringVector_Insert(HandleRef jarg1, int jarg2, string jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_InsertRange")]
  public static extern void StringVector_InsertRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_RemoveAt")]
  public static extern void StringVector_RemoveAt(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_RemoveRange")]
  public static extern void StringVector_RemoveRange(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_Repeat")]
  public static extern IntPtr StringVector_Repeat(string jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_Reverse__SWIG_0")]
  public static extern void StringVector_Reverse__SWIG_0(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_Reverse__SWIG_1")]
  public static extern void StringVector_Reverse__SWIG_1(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_SetRange")]
  public static extern void StringVector_SetRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_Contains")]
  public static extern bool StringVector_Contains(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_IndexOf")]
  public static extern int StringVector_IndexOf(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_LastIndexOf")]
  public static extern int StringVector_LastIndexOf(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_StringVector_Remove")]
  public static extern bool StringVector_Remove(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_StringVector")]
  public static extern void delete_StringVector(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_Clear")]
  public static extern void VectorOfStringVector_Clear(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_Add")]
  public static extern void VectorOfStringVector_Add(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_size")]
  public static extern uint VectorOfStringVector_size(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_capacity")]
  public static extern uint VectorOfStringVector_capacity(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_reserve")]
  public static extern void VectorOfStringVector_reserve(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_new_VectorOfStringVector__SWIG_0")]
  public static extern IntPtr new_VectorOfStringVector__SWIG_0();

  [DllImport("CoolProp", EntryPoint="CSharp_new_VectorOfStringVector__SWIG_1")]
  public static extern IntPtr new_VectorOfStringVector__SWIG_1(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_VectorOfStringVector__SWIG_2")]
  public static extern IntPtr new_VectorOfStringVector__SWIG_2(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_getitemcopy")]
  public static extern IntPtr VectorOfStringVector_getitemcopy(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_getitem")]
  public static extern IntPtr VectorOfStringVector_getitem(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_setitem")]
  public static extern void VectorOfStringVector_setitem(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_AddRange")]
  public static extern void VectorOfStringVector_AddRange(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_GetRange")]
  public static extern IntPtr VectorOfStringVector_GetRange(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_Insert")]
  public static extern void VectorOfStringVector_Insert(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_InsertRange")]
  public static extern void VectorOfStringVector_InsertRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_RemoveAt")]
  public static extern void VectorOfStringVector_RemoveAt(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_RemoveRange")]
  public static extern void VectorOfStringVector_RemoveRange(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_Repeat")]
  public static extern IntPtr VectorOfStringVector_Repeat(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_Reverse__SWIG_0")]
  public static extern void VectorOfStringVector_Reverse__SWIG_0(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_Reverse__SWIG_1")]
  public static extern void VectorOfStringVector_Reverse__SWIG_1(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_VectorOfStringVector_SetRange")]
  public static extern void VectorOfStringVector_SetRange(HandleRef jarg1, int jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_VectorOfStringVector")]
  public static extern void delete_VectorOfStringVector(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_rhomolar_set")]
  public static extern void SimpleState_rhomolar_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_rhomolar_get")]
  public static extern double SimpleState_rhomolar_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_T_set")]
  public static extern void SimpleState_T_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_T_get")]
  public static extern double SimpleState_T_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_p_set")]
  public static extern void SimpleState_p_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_p_get")]
  public static extern double SimpleState_p_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_hmolar_set")]
  public static extern void SimpleState_hmolar_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_hmolar_get")]
  public static extern double SimpleState_hmolar_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_smolar_set")]
  public static extern void SimpleState_smolar_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_smolar_get")]
  public static extern double SimpleState_smolar_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_umolar_set")]
  public static extern void SimpleState_umolar_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_umolar_get")]
  public static extern double SimpleState_umolar_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_Q_set")]
  public static extern void SimpleState_Q_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_Q_get")]
  public static extern double SimpleState_Q_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_SimpleState")]
  public static extern IntPtr new_SimpleState();

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_fill")]
  public static extern void SimpleState_fill(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SimpleState_is_valid")]
  public static extern bool SimpleState_is_valid(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_SimpleState")]
  public static extern void delete_SimpleState(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_CriticalState_stable_set")]
  public static extern void CriticalState_stable_set(HandleRef jarg1, bool jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_CriticalState_stable_get")]
  public static extern bool CriticalState_stable_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_CriticalState")]
  public static extern IntPtr new_CriticalState();

  [DllImport("CoolProp", EntryPoint="CSharp_delete_CriticalState")]
  public static extern void delete_CriticalState(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SsatSimpleState_exists_set")]
  public static extern void SsatSimpleState_exists_set(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SsatSimpleState_exists_get")]
  public static extern int SsatSimpleState_exists_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_SsatSimpleState")]
  public static extern IntPtr new_SsatSimpleState();

  [DllImport("CoolProp", EntryPoint="CSharp_delete_SsatSimpleState")]
  public static extern void delete_SsatSimpleState(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_get_parameter_information")]
  public static extern string get_parameter_information(int jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_get_parameter_index")]
  public static extern int get_parameter_index(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_is_valid_phase")]
  public static extern bool is_valid_phase(string jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_get_phase_index")]
  public static extern int get_phase_index(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_is_trivial_parameter")]
  public static extern bool is_trivial_parameter(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_is_valid_parameter")]
  public static extern bool is_valid_parameter(string jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_is_valid_first_derivative")]
  public static extern bool is_valid_first_derivative(string jarg1, HandleRef jarg2, HandleRef jarg3, HandleRef jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_is_valid_first_saturation_derivative")]
  public static extern bool is_valid_first_saturation_derivative(string jarg1, HandleRef jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_is_valid_second_derivative")]
  public static extern bool is_valid_second_derivative(string jarg1, HandleRef jarg2, HandleRef jarg3, HandleRef jarg4, HandleRef jarg5, HandleRef jarg6);

  [DllImport("CoolProp", EntryPoint="CSharp_get_csv_parameter_list")]
  public static extern string get_csv_parameter_list();

  [DllImport("CoolProp", EntryPoint="CSharp_match_pair")]
  public static extern bool match_pair(int jarg1, int jarg2, int jarg3, int jarg4, HandleRef jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_get_input_pair_index")]
  public static extern int get_input_pair_index(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_get_input_pair_short_desc")]
  public static extern string get_input_pair_short_desc(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_get_input_pair_long_desc")]
  public static extern string get_input_pair_long_desc(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_split_input_pair")]
  public static extern void split_input_pair(int jarg1, HandleRef jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_get_mixture_binary_pair_data")]
  public static extern string get_mixture_binary_pair_data(string jarg1, string jarg2, string jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_set_mixture_binary_pair_data")]
  public static extern void set_mixture_binary_pair_data(string jarg1, string jarg2, string jarg3, double jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_extract_backend_families")]
  public static extern void extract_backend_families(string jarg1, HandleRef jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_extract_backend_families_string")]
  public static extern void extract_backend_families_string(string jarg1, HandleRef jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_get_backend_string")]
  public static extern string get_backend_string(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SpinodalData_tau_set")]
  public static extern void SpinodalData_tau_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SpinodalData_tau_get")]
  public static extern IntPtr SpinodalData_tau_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SpinodalData_delta_set")]
  public static extern void SpinodalData_delta_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SpinodalData_delta_get")]
  public static extern IntPtr SpinodalData_delta_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SpinodalData_M1_set")]
  public static extern void SpinodalData_M1_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_SpinodalData_M1_get")]
  public static extern IntPtr SpinodalData_M1_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_SpinodalData")]
  public static extern IntPtr new_SpinodalData();

  [DllImport("CoolProp", EntryPoint="CSharp_delete_SpinodalData")]
  public static extern void delete_SpinodalData(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_T_set")]
  public static extern void GuessesStructure_T_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_T_get")]
  public static extern double GuessesStructure_T_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_p_set")]
  public static extern void GuessesStructure_p_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_p_get")]
  public static extern double GuessesStructure_p_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_rhomolar_set")]
  public static extern void GuessesStructure_rhomolar_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_rhomolar_get")]
  public static extern double GuessesStructure_rhomolar_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_hmolar_set")]
  public static extern void GuessesStructure_hmolar_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_hmolar_get")]
  public static extern double GuessesStructure_hmolar_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_smolar_set")]
  public static extern void GuessesStructure_smolar_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_smolar_get")]
  public static extern double GuessesStructure_smolar_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_rhomolar_liq_set")]
  public static extern void GuessesStructure_rhomolar_liq_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_rhomolar_liq_get")]
  public static extern double GuessesStructure_rhomolar_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_rhomolar_vap_set")]
  public static extern void GuessesStructure_rhomolar_vap_set(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_rhomolar_vap_get")]
  public static extern double GuessesStructure_rhomolar_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_x_set")]
  public static extern void GuessesStructure_x_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_x_get")]
  public static extern IntPtr GuessesStructure_x_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_y_set")]
  public static extern void GuessesStructure_y_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_y_get")]
  public static extern IntPtr GuessesStructure_y_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_GuessesStructure")]
  public static extern IntPtr new_GuessesStructure();

  [DllImport("CoolProp", EntryPoint="CSharp_GuessesStructure_clear")]
  public static extern void GuessesStructure_clear(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_GuessesStructure")]
  public static extern void delete_GuessesStructure(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_AbstractState")]
  public static extern void delete_AbstractState(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_factory__SWIG_0")]
  public static extern IntPtr AbstractState_factory__SWIG_0(string jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_factory__SWIG_1")]
  public static extern IntPtr AbstractState_factory__SWIG_1(string jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_T")]
  public static extern void AbstractState_set_T(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_backend_name")]
  public static extern string AbstractState_backend_name(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_using_mole_fractions")]
  public static extern bool AbstractState_using_mole_fractions(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_using_mass_fractions")]
  public static extern bool AbstractState_using_mass_fractions(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_using_volu_fractions")]
  public static extern bool AbstractState_using_volu_fractions(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_reference_stateS")]
  public static extern void AbstractState_set_reference_stateS(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_reference_stateD")]
  public static extern void AbstractState_set_reference_stateD(HandleRef jarg1, double jarg2, double jarg3, double jarg4, double jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_mole_fractions")]
  public static extern void AbstractState_set_mole_fractions(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_mass_fractions")]
  public static extern void AbstractState_set_mass_fractions(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_volu_fractions")]
  public static extern void AbstractState_set_volu_fractions(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_mole_fractions_liquid")]
  public static extern IntPtr AbstractState_mole_fractions_liquid(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_mole_fractions_vapor")]
  public static extern IntPtr AbstractState_mole_fractions_vapor(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_mole_fractions")]
  public static extern IntPtr AbstractState_get_mole_fractions(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_mass_fractions")]
  public static extern IntPtr AbstractState_get_mass_fractions(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_update")]
  public static extern void AbstractState_update(HandleRef jarg1, int jarg2, double jarg3, double jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_update_with_guesses")]
  public static extern void AbstractState_update_with_guesses(HandleRef jarg1, int jarg2, double jarg3, double jarg4, HandleRef jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_available_in_high_level")]
  public static extern bool AbstractState_available_in_high_level(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_fluid_param_string")]
  public static extern string AbstractState_fluid_param_string(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_fluid_names")]
  public static extern IntPtr AbstractState_fluid_names(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_fluid_constant")]
  public static extern double AbstractState_get_fluid_constant(HandleRef jarg1, uint jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_binary_interaction_double__SWIG_0")]
  public static extern void AbstractState_set_binary_interaction_double__SWIG_0(HandleRef jarg1, string jarg2, string jarg3, string jarg4, double jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_binary_interaction_double__SWIG_1")]
  public static extern void AbstractState_set_binary_interaction_double__SWIG_1(HandleRef jarg1, uint jarg2, uint jarg3, string jarg4, double jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_binary_interaction_string__SWIG_0")]
  public static extern void AbstractState_set_binary_interaction_string__SWIG_0(HandleRef jarg1, string jarg2, string jarg3, string jarg4, string jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_binary_interaction_string__SWIG_1")]
  public static extern void AbstractState_set_binary_interaction_string__SWIG_1(HandleRef jarg1, uint jarg2, uint jarg3, string jarg4, string jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_binary_interaction_double__SWIG_0")]
  public static extern double AbstractState_get_binary_interaction_double__SWIG_0(HandleRef jarg1, string jarg2, string jarg3, string jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_binary_interaction_double__SWIG_1")]
  public static extern double AbstractState_get_binary_interaction_double__SWIG_1(HandleRef jarg1, uint jarg2, uint jarg3, string jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_binary_interaction_string")]
  public static extern string AbstractState_get_binary_interaction_string(HandleRef jarg1, string jarg2, string jarg3, string jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_apply_simple_mixing_rule")]
  public static extern void AbstractState_apply_simple_mixing_rule(HandleRef jarg1, uint jarg2, uint jarg3, string jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_cubic_alpha_C")]
  public static extern void AbstractState_set_cubic_alpha_C(HandleRef jarg1, uint jarg2, string jarg3, double jarg4, double jarg5, double jarg6);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_set_fluid_parameter_double")]
  public static extern void AbstractState_set_fluid_parameter_double(HandleRef jarg1, uint jarg2, string jarg3, double jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_fluid_parameter_double")]
  public static extern double AbstractState_get_fluid_parameter_double(HandleRef jarg1, uint jarg2, string jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_clear")]
  public static extern bool AbstractState_clear(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_reducing_state")]
  public static extern IntPtr AbstractState_get_reducing_state(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_state")]
  public static extern IntPtr AbstractState_get_state(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_Tmin")]
  public static extern double AbstractState_Tmin(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_Tmax")]
  public static extern double AbstractState_Tmax(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_pmax")]
  public static extern double AbstractState_pmax(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_Ttriple")]
  public static extern double AbstractState_Ttriple(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_phase")]
  public static extern int AbstractState_phase(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_specify_phase")]
  public static extern void AbstractState_specify_phase(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_unspecify_phase")]
  public static extern void AbstractState_unspecify_phase(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_T_critical")]
  public static extern double AbstractState_T_critical(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_p_critical")]
  public static extern double AbstractState_p_critical(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_rhomolar_critical")]
  public static extern double AbstractState_rhomolar_critical(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_rhomass_critical")]
  public static extern double AbstractState_rhomass_critical(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_all_critical_points")]
  public static extern IntPtr AbstractState_all_critical_points(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_build_spinodal")]
  public static extern void AbstractState_build_spinodal(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_spinodal_data")]
  public static extern IntPtr AbstractState_get_spinodal_data(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_criticality_contour_values")]
  public static extern void AbstractState_criticality_contour_values(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_tangent_plane_distance__SWIG_0")]
  public static extern double AbstractState_tangent_plane_distance__SWIG_0(HandleRef jarg1, double jarg2, double jarg3, HandleRef jarg4, double jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_tangent_plane_distance__SWIG_1")]
  public static extern double AbstractState_tangent_plane_distance__SWIG_1(HandleRef jarg1, double jarg2, double jarg3, HandleRef jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_T_reducing")]
  public static extern double AbstractState_T_reducing(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_rhomolar_reducing")]
  public static extern double AbstractState_rhomolar_reducing(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_rhomass_reducing")]
  public static extern double AbstractState_rhomass_reducing(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_p_triple")]
  public static extern double AbstractState_p_triple(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_name")]
  public static extern string AbstractState_name(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_dipole_moment")]
  public static extern double AbstractState_dipole_moment(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_keyed_output")]
  public static extern double AbstractState_keyed_output(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_trivial_keyed_output")]
  public static extern double AbstractState_trivial_keyed_output(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_saturated_liquid_keyed_output")]
  public static extern double AbstractState_saturated_liquid_keyed_output(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_saturated_vapor_keyed_output")]
  public static extern double AbstractState_saturated_vapor_keyed_output(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_T")]
  public static extern double AbstractState_T(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_rhomolar")]
  public static extern double AbstractState_rhomolar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_rhomass")]
  public static extern double AbstractState_rhomass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_p")]
  public static extern double AbstractState_p(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_Q")]
  public static extern double AbstractState_Q(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_tau")]
  public static extern double AbstractState_tau(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_delta")]
  public static extern double AbstractState_delta(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_molar_mass")]
  public static extern double AbstractState_molar_mass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_acentric_factor")]
  public static extern double AbstractState_acentric_factor(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_gas_constant")]
  public static extern double AbstractState_gas_constant(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_Bvirial")]
  public static extern double AbstractState_Bvirial(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_dBvirial_dT")]
  public static extern double AbstractState_dBvirial_dT(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_Cvirial")]
  public static extern double AbstractState_Cvirial(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_dCvirial_dT")]
  public static extern double AbstractState_dCvirial_dT(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_compressibility_factor")]
  public static extern double AbstractState_compressibility_factor(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_hmolar")]
  public static extern double AbstractState_hmolar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_hmass")]
  public static extern double AbstractState_hmass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_hmolar_excess")]
  public static extern double AbstractState_hmolar_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_hmass_excess")]
  public static extern double AbstractState_hmass_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_smolar")]
  public static extern double AbstractState_smolar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_smass")]
  public static extern double AbstractState_smass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_smolar_excess")]
  public static extern double AbstractState_smolar_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_smass_excess")]
  public static extern double AbstractState_smass_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_umolar")]
  public static extern double AbstractState_umolar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_umass")]
  public static extern double AbstractState_umass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_umolar_excess")]
  public static extern double AbstractState_umolar_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_umass_excess")]
  public static extern double AbstractState_umass_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_cpmolar")]
  public static extern double AbstractState_cpmolar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_cpmass")]
  public static extern double AbstractState_cpmass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_cp0molar")]
  public static extern double AbstractState_cp0molar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_cp0mass")]
  public static extern double AbstractState_cp0mass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_cvmolar")]
  public static extern double AbstractState_cvmolar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_cvmass")]
  public static extern double AbstractState_cvmass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_gibbsmolar")]
  public static extern double AbstractState_gibbsmolar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_gibbsmass")]
  public static extern double AbstractState_gibbsmass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_gibbsmolar_excess")]
  public static extern double AbstractState_gibbsmolar_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_gibbsmass_excess")]
  public static extern double AbstractState_gibbsmass_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_helmholtzmolar")]
  public static extern double AbstractState_helmholtzmolar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_helmholtzmass")]
  public static extern double AbstractState_helmholtzmass(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_helmholtzmolar_excess")]
  public static extern double AbstractState_helmholtzmolar_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_helmholtzmass_excess")]
  public static extern double AbstractState_helmholtzmass_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_volumemolar_excess")]
  public static extern double AbstractState_volumemolar_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_volumemass_excess")]
  public static extern double AbstractState_volumemass_excess(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_speed_sound")]
  public static extern double AbstractState_speed_sound(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_isothermal_compressibility")]
  public static extern double AbstractState_isothermal_compressibility(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_isobaric_expansion_coefficient")]
  public static extern double AbstractState_isobaric_expansion_coefficient(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_fugacity_coefficient")]
  public static extern double AbstractState_fugacity_coefficient(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_fugacity")]
  public static extern double AbstractState_fugacity(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_chemical_potential")]
  public static extern double AbstractState_chemical_potential(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_fundamental_derivative_of_gas_dynamics")]
  public static extern double AbstractState_fundamental_derivative_of_gas_dynamics(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_PIP")]
  public static extern double AbstractState_PIP(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_true_critical_point")]
  public static extern void AbstractState_true_critical_point(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_ideal_curve")]
  public static extern void AbstractState_ideal_curve(HandleRef jarg1, string jarg2, HandleRef jarg3, HandleRef jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_first_partial_deriv")]
  public static extern double AbstractState_first_partial_deriv(HandleRef jarg1, int jarg2, int jarg3, int jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_second_partial_deriv")]
  public static extern double AbstractState_second_partial_deriv(HandleRef jarg1, int jarg2, int jarg3, int jarg4, int jarg5, int jarg6);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_first_saturation_deriv")]
  public static extern double AbstractState_first_saturation_deriv(HandleRef jarg1, int jarg2, int jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_second_saturation_deriv")]
  public static extern double AbstractState_second_saturation_deriv(HandleRef jarg1, int jarg2, int jarg3, int jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_first_two_phase_deriv")]
  public static extern double AbstractState_first_two_phase_deriv(HandleRef jarg1, int jarg2, int jarg3, int jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_second_two_phase_deriv")]
  public static extern double AbstractState_second_two_phase_deriv(HandleRef jarg1, int jarg2, int jarg3, int jarg4, int jarg5, int jarg6);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_first_two_phase_deriv_splined")]
  public static extern double AbstractState_first_two_phase_deriv_splined(HandleRef jarg1, int jarg2, int jarg3, int jarg4, double jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_build_phase_envelope__SWIG_0")]
  public static extern void AbstractState_build_phase_envelope__SWIG_0(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_build_phase_envelope__SWIG_1")]
  public static extern void AbstractState_build_phase_envelope__SWIG_1(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_get_phase_envelope_data")]
  public static extern IntPtr AbstractState_get_phase_envelope_data(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_has_melting_line")]
  public static extern bool AbstractState_has_melting_line(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_melting_line")]
  public static extern double AbstractState_melting_line(HandleRef jarg1, int jarg2, int jarg3, double jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_saturation_ancillary")]
  public static extern double AbstractState_saturation_ancillary(HandleRef jarg1, int jarg2, int jarg3, int jarg4, double jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_viscosity")]
  public static extern double AbstractState_viscosity(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_viscosity_contributions")]
  public static extern void AbstractState_viscosity_contributions(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, HandleRef jarg4, HandleRef jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_conductivity")]
  public static extern double AbstractState_conductivity(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_conductivity_contributions")]
  public static extern void AbstractState_conductivity_contributions(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, HandleRef jarg4, HandleRef jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_surface_tension")]
  public static extern double AbstractState_surface_tension(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_Prandtl")]
  public static extern double AbstractState_Prandtl(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_conformal_state")]
  public static extern void AbstractState_conformal_state(HandleRef jarg1, string jarg2, HandleRef jarg3, HandleRef jarg4);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_change_EOS")]
  public static extern void AbstractState_change_EOS(HandleRef jarg1, uint jarg2, string jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_alpha0")]
  public static extern double AbstractState_alpha0(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_dalpha0_dDelta")]
  public static extern double AbstractState_dalpha0_dDelta(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_dalpha0_dTau")]
  public static extern double AbstractState_dalpha0_dTau(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d2alpha0_dDelta2")]
  public static extern double AbstractState_d2alpha0_dDelta2(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d2alpha0_dDelta_dTau")]
  public static extern double AbstractState_d2alpha0_dDelta_dTau(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d2alpha0_dTau2")]
  public static extern double AbstractState_d2alpha0_dTau2(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d3alpha0_dTau3")]
  public static extern double AbstractState_d3alpha0_dTau3(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d3alpha0_dDelta_dTau2")]
  public static extern double AbstractState_d3alpha0_dDelta_dTau2(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d3alpha0_dDelta2_dTau")]
  public static extern double AbstractState_d3alpha0_dDelta2_dTau(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d3alpha0_dDelta3")]
  public static extern double AbstractState_d3alpha0_dDelta3(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_alphar")]
  public static extern double AbstractState_alphar(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_dalphar_dDelta")]
  public static extern double AbstractState_dalphar_dDelta(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_dalphar_dTau")]
  public static extern double AbstractState_dalphar_dTau(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d2alphar_dDelta2")]
  public static extern double AbstractState_d2alphar_dDelta2(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d2alphar_dDelta_dTau")]
  public static extern double AbstractState_d2alphar_dDelta_dTau(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d2alphar_dTau2")]
  public static extern double AbstractState_d2alphar_dTau2(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d3alphar_dDelta3")]
  public static extern double AbstractState_d3alphar_dDelta3(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d3alphar_dDelta2_dTau")]
  public static extern double AbstractState_d3alphar_dDelta2_dTau(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d3alphar_dDelta_dTau2")]
  public static extern double AbstractState_d3alphar_dDelta_dTau2(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d3alphar_dTau3")]
  public static extern double AbstractState_d3alphar_dTau3(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d4alphar_dDelta4")]
  public static extern double AbstractState_d4alphar_dDelta4(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d4alphar_dDelta3_dTau")]
  public static extern double AbstractState_d4alphar_dDelta3_dTau(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d4alphar_dDelta2_dTau2")]
  public static extern double AbstractState_d4alphar_dDelta2_dTau2(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d4alphar_dDelta_dTau3")]
  public static extern double AbstractState_d4alphar_dDelta_dTau3(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractState_d4alphar_dTau4")]
  public static extern double AbstractState_d4alphar_dTau4(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_AbstractStateGenerator_get_AbstractState")]
  public static extern IntPtr AbstractStateGenerator_get_AbstractState(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_AbstractStateGenerator")]
  public static extern void delete_AbstractStateGenerator(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_register_backend")]
  public static extern void register_backend(int jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_Props1SI")]
  public static extern double Props1SI(string jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PropsSI")]
  public static extern double PropsSI(string jarg1, string jarg2, double jarg3, string jarg4, double jarg5, string jarg6);

  [DllImport("CoolProp", EntryPoint="CSharp_PropsSImulti")]
  public static extern IntPtr PropsSImulti(HandleRef jarg1, string jarg2, HandleRef jarg3, string jarg4, HandleRef jarg5, string jarg6, HandleRef jarg7, HandleRef jarg8);

  [DllImport("CoolProp", EntryPoint="CSharp_get_debug_level")]
  public static extern int get_debug_level();

  [DllImport("CoolProp", EntryPoint="CSharp_set_debug_level")]
  public static extern void set_debug_level(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_set_error_string")]
  public static extern void set_error_string(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_set_warning_string")]
  public static extern void set_warning_string(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_saturation_ancillary")]
  public static extern double saturation_ancillary(string jarg1, string jarg2, int jarg3, string jarg4, double jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_get_global_param_string")]
  public static extern string get_global_param_string(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_get_fluid_param_string")]
  public static extern string get_fluid_param_string(string jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_is_valid_fluid_string")]
  public static extern bool is_valid_fluid_string(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_add_fluids_as_JSON")]
  public static extern bool add_fluids_as_JSON(string jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_set_reference_stateS")]
  public static extern void set_reference_stateS(string jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_set_reference_stateD")]
  public static extern void set_reference_stateD(string jarg1, double jarg2, double jarg3, double jarg4, double jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseSI")]
  public static extern string PhaseSI(string jarg1, double jarg2, string jarg3, double jarg4, string jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_extract_backend")]
  public static extern void extract_backend(string jarg1, HandleRef jarg2, HandleRef jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_extract_fractions")]
  public static extern string extract_fractions(string jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_phase_lookup_string")]
  public static extern string phase_lookup_string(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_TypeI_set")]
  public static extern void PhaseEnvelopeData_TypeI_set(HandleRef jarg1, bool jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_TypeI_get")]
  public static extern bool PhaseEnvelopeData_TypeI_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_built_set")]
  public static extern void PhaseEnvelopeData_built_set(HandleRef jarg1, bool jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_built_get")]
  public static extern bool PhaseEnvelopeData_built_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_iTsat_max_set")]
  public static extern void PhaseEnvelopeData_iTsat_max_set(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_iTsat_max_get")]
  public static extern uint PhaseEnvelopeData_iTsat_max_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_ipsat_max_set")]
  public static extern void PhaseEnvelopeData_ipsat_max_set(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_ipsat_max_get")]
  public static extern uint PhaseEnvelopeData_ipsat_max_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_icrit_set")]
  public static extern void PhaseEnvelopeData_icrit_set(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_icrit_get")]
  public static extern uint PhaseEnvelopeData_icrit_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_T_set")]
  public static extern void PhaseEnvelopeData_T_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_T_get")]
  public static extern IntPtr PhaseEnvelopeData_T_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_p_set")]
  public static extern void PhaseEnvelopeData_p_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_p_get")]
  public static extern IntPtr PhaseEnvelopeData_p_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnT_set")]
  public static extern void PhaseEnvelopeData_lnT_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnT_get")]
  public static extern IntPtr PhaseEnvelopeData_lnT_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnp_set")]
  public static extern void PhaseEnvelopeData_lnp_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnp_get")]
  public static extern IntPtr PhaseEnvelopeData_lnp_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_rhomolar_liq_set")]
  public static extern void PhaseEnvelopeData_rhomolar_liq_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_rhomolar_liq_get")]
  public static extern IntPtr PhaseEnvelopeData_rhomolar_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_rhomolar_vap_set")]
  public static extern void PhaseEnvelopeData_rhomolar_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_rhomolar_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_rhomolar_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnrhomolar_liq_set")]
  public static extern void PhaseEnvelopeData_lnrhomolar_liq_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnrhomolar_liq_get")]
  public static extern IntPtr PhaseEnvelopeData_lnrhomolar_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnrhomolar_vap_set")]
  public static extern void PhaseEnvelopeData_lnrhomolar_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnrhomolar_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_lnrhomolar_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_hmolar_liq_set")]
  public static extern void PhaseEnvelopeData_hmolar_liq_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_hmolar_liq_get")]
  public static extern IntPtr PhaseEnvelopeData_hmolar_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_hmolar_vap_set")]
  public static extern void PhaseEnvelopeData_hmolar_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_hmolar_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_hmolar_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_smolar_liq_set")]
  public static extern void PhaseEnvelopeData_smolar_liq_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_smolar_liq_get")]
  public static extern IntPtr PhaseEnvelopeData_smolar_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_smolar_vap_set")]
  public static extern void PhaseEnvelopeData_smolar_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_smolar_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_smolar_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_Q_set")]
  public static extern void PhaseEnvelopeData_Q_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_Q_get")]
  public static extern IntPtr PhaseEnvelopeData_Q_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_cpmolar_liq_set")]
  public static extern void PhaseEnvelopeData_cpmolar_liq_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_cpmolar_liq_get")]
  public static extern IntPtr PhaseEnvelopeData_cpmolar_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_cpmolar_vap_set")]
  public static extern void PhaseEnvelopeData_cpmolar_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_cpmolar_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_cpmolar_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_cvmolar_liq_set")]
  public static extern void PhaseEnvelopeData_cvmolar_liq_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_cvmolar_liq_get")]
  public static extern IntPtr PhaseEnvelopeData_cvmolar_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_cvmolar_vap_set")]
  public static extern void PhaseEnvelopeData_cvmolar_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_cvmolar_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_cvmolar_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_viscosity_liq_set")]
  public static extern void PhaseEnvelopeData_viscosity_liq_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_viscosity_liq_get")]
  public static extern IntPtr PhaseEnvelopeData_viscosity_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_viscosity_vap_set")]
  public static extern void PhaseEnvelopeData_viscosity_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_viscosity_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_viscosity_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_conductivity_liq_set")]
  public static extern void PhaseEnvelopeData_conductivity_liq_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_conductivity_liq_get")]
  public static extern IntPtr PhaseEnvelopeData_conductivity_liq_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_conductivity_vap_set")]
  public static extern void PhaseEnvelopeData_conductivity_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_conductivity_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_conductivity_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_speed_sound_vap_set")]
  public static extern void PhaseEnvelopeData_speed_sound_vap_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_speed_sound_vap_get")]
  public static extern IntPtr PhaseEnvelopeData_speed_sound_vap_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_K_set")]
  public static extern void PhaseEnvelopeData_K_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_K_get")]
  public static extern IntPtr PhaseEnvelopeData_K_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnK_set")]
  public static extern void PhaseEnvelopeData_lnK_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_lnK_get")]
  public static extern IntPtr PhaseEnvelopeData_lnK_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_x_set")]
  public static extern void PhaseEnvelopeData_x_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_x_get")]
  public static extern IntPtr PhaseEnvelopeData_x_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_y_set")]
  public static extern void PhaseEnvelopeData_y_set(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_y_get")]
  public static extern IntPtr PhaseEnvelopeData_y_get(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_PhaseEnvelopeData")]
  public static extern IntPtr new_PhaseEnvelopeData();

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_resize")]
  public static extern void PhaseEnvelopeData_resize(HandleRef jarg1, uint jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_clear")]
  public static extern void PhaseEnvelopeData_clear(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_insert_variables")]
  public static extern void PhaseEnvelopeData_insert_variables(HandleRef jarg1, double jarg2, double jarg3, double jarg4, double jarg5, double jarg6, double jarg7, double jarg8, double jarg9, HandleRef jarg10, HandleRef jarg11, uint jarg12);

  [DllImport("CoolProp", EntryPoint="CSharp_PhaseEnvelopeData_store_variables")]
  public static extern void PhaseEnvelopeData_store_variables(HandleRef jarg1, double jarg2, double jarg3, double jarg4, double jarg5, double jarg6, double jarg7, double jarg8, double jarg9, HandleRef jarg10, HandleRef jarg11);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_PhaseEnvelopeData")]
  public static extern void delete_PhaseEnvelopeData(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_config_key_to_string")]
  public static extern string config_key_to_string(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_config_string_to_key")]
  public static extern int config_string_to_key(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_config_key_description__SWIG_0")]
  public static extern string config_key_description__SWIG_0(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_config_key_description__SWIG_1")]
  public static extern string config_key_description__SWIG_1(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_ConfigurationItem__SWIG_0")]
  public static extern IntPtr new_ConfigurationItem__SWIG_0(int jarg1, bool jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_new_ConfigurationItem__SWIG_1")]
  public static extern IntPtr new_ConfigurationItem__SWIG_1(int jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_new_ConfigurationItem__SWIG_2")]
  public static extern IntPtr new_ConfigurationItem__SWIG_2(int jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_new_ConfigurationItem__SWIG_3")]
  public static extern IntPtr new_ConfigurationItem__SWIG_3(int jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_ConfigurationItem_set_bool")]
  public static extern void ConfigurationItem_set_bool(HandleRef jarg1, bool jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_ConfigurationItem_set_integer")]
  public static extern void ConfigurationItem_set_integer(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_ConfigurationItem_set_double")]
  public static extern void ConfigurationItem_set_double(HandleRef jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_ConfigurationItem_set_string")]
  public static extern void ConfigurationItem_set_string(HandleRef jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_ConfigurationItem_get_key")]
  public static extern int ConfigurationItem_get_key(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_delete_ConfigurationItem")]
  public static extern void delete_ConfigurationItem(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_new_Configuration")]
  public static extern IntPtr new_Configuration();

  [DllImport("CoolProp", EntryPoint="CSharp_delete_Configuration")]
  public static extern void delete_Configuration(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_Configuration_get_item")]
  public static extern IntPtr Configuration_get_item(HandleRef jarg1, int jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_Configuration_add_item")]
  public static extern void Configuration_add_item(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_Configuration_get_items")]
  public static extern IntPtr Configuration_get_items(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_Configuration_set_defaults")]
  public static extern void Configuration_set_defaults(HandleRef jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_get_config_bool")]
  public static extern bool get_config_bool(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_get_config_double")]
  public static extern double get_config_double(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_get_config_string")]
  public static extern string get_config_string(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_get_config_as_json_string")]
  public static extern string get_config_as_json_string();

  [DllImport("CoolProp", EntryPoint="CSharp_set_config_bool")]
  public static extern void set_config_bool(int jarg1, bool jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_set_config_double")]
  public static extern void set_config_double(int jarg1, double jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_set_config_string")]
  public static extern void set_config_string(int jarg1, string jarg2);

  [DllImport("CoolProp", EntryPoint="CSharp_set_config_as_json_string")]
  public static extern void set_config_as_json_string(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_HAPropsSI")]
  public static extern double HAPropsSI(string jarg1, string jarg2, double jarg3, string jarg4, double jarg5, string jarg6, double jarg7);

  [DllImport("CoolProp", EntryPoint="CSharp_HAProps")]
  public static extern double HAProps(string jarg1, string jarg2, double jarg3, string jarg4, double jarg5, string jarg6, double jarg7);

  [DllImport("CoolProp", EntryPoint="CSharp_HAProps_Aux")]
  public static extern double HAProps_Aux(string jarg1, double jarg2, double jarg3, double jarg4, string jarg5);

  [DllImport("CoolProp", EntryPoint="CSharp_IceProps")]
  public static extern double IceProps(string jarg1, double jarg2, double jarg3);

  [DllImport("CoolProp", EntryPoint="CSharp_UseVirialCorrelations")]
  public static extern void UseVirialCorrelations(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_UseIsothermCompressCorrelation")]
  public static extern void UseIsothermCompressCorrelation(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_UseIdealGasEnthalpyCorrelations")]
  public static extern void UseIdealGasEnthalpyCorrelations(int jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_HAHelp")]
  public static extern void HAHelp();

  [DllImport("CoolProp", EntryPoint="CSharp_returnHumAirCode")]
  public static extern int returnHumAirCode(string jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_cair_sat")]
  public static extern double cair_sat(double jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_CriticalState_SWIGUpcast")]
  public static extern IntPtr CriticalState_SWIGUpcast(IntPtr jarg1);

  [DllImport("CoolProp", EntryPoint="CSharp_SsatSimpleState_SWIGUpcast")]
  public static extern IntPtr SsatSimpleState_SWIGUpcast(IntPtr jarg1);
}
