
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class DoubleVector : IDisposable
{
    private HandleRef swigCPtr;
    protected bool swigCMemOwn;

    internal DoubleVector(IntPtr cPtr, bool cMemoryOwn)
    {
        swigCMemOwn = cMemoryOwn;
        swigCPtr = new HandleRef(this, cPtr);
    }

    private static IntPtr Instantiate_swig()
    {
        if (Environment.Is64BitProcess)
        {
            return CoolPropPINVOKE64.new_DoubleVector__SWIG_0();
        }
        else
        {
            return CoolPropPINVOKE.new_DoubleVector__SWIG_0();
        }
    }
    public DoubleVector() : this(Instantiate_swig(), true)
    {
    }
    public DoubleVector(ICollection c) : this()
    {
        if (c == null)
            throw new ArgumentNullException("c");
        foreach (double element in c)
        {
            this.Add(element);
        }
    }

    internal static HandleRef getCPtr(DoubleVector obj) => (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;

    ~DoubleVector()
    {
        Dispose();
    }

    public virtual void Dispose()
    {
        lock (this)
        {
            if (swigCPtr.Handle != IntPtr.Zero)
            {
                if (swigCMemOwn)
                {
                    swigCMemOwn = false;

                    if (Environment.Is64BitProcess)
                    {
                        CoolPropPINVOKE64.delete_DoubleVector(swigCPtr);
                    }
                    else
                    {
                        CoolPropPINVOKE.delete_DoubleVector(swigCPtr);
                    }
                }

                swigCPtr = new HandleRef(null, IntPtr.Zero);
            }

            GC.SuppressFinalize(this);
        }
    }

    public void Add(double x)
    {
        if (Environment.Is64BitProcess)
        {
            CoolPropPINVOKE64.DoubleVector_Add(swigCPtr, x);
        }
        else
        {
            CoolPropPINVOKE.DoubleVector_Add(swigCPtr, x);

        }
    }
}
