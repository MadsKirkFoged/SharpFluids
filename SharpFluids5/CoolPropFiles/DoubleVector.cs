

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class DoubleVector : IDisposable
 {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DoubleVector(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }
  public DoubleVector() : this(CoolPropPINVOKE.new_DoubleVector__SWIG_0(), true) {
  }
  public DoubleVector(ICollection c) : this() {
    if (c == null)
      throw new ArgumentNullException("c");
    foreach (double element in c) {
      this.Add(element);
    }
  }


  internal static HandleRef getCPtr(DoubleVector obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~DoubleVector() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          CoolPropPINVOKE.delete_DoubleVector(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }


  public void Add(double x) {
    CoolPropPINVOKE.DoubleVector_Add(swigCPtr, x);
  }




}
