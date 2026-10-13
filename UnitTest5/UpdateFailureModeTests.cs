using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitsTests;

/// <summary>
/// A solver can ask for a state CoolProp cannot do (0 Pa and 0 K). <see cref="Fluid.OnUpdateFailure"/> picks between logging it
/// (with the caller's stack trace, so the log says where it happened) and throwing a <see cref="FluidUpdateException"/>.
/// </summary>
[TestClass]
public class UpdateFailureModeTests
{
    private readonly ListSink log = new();

    [TestInitialize]
    public void Setup()
    {
        Fluid.OnUpdateFailure = UpdateFailureMode.Log;
        Log.Logger = new LoggerConfiguration().WriteTo.Sink(log).CreateLogger();
    }

    [TestCleanup]
    public void Cleanup()
    {
        Fluid.OnUpdateFailure = UpdateFailureMode.Log;
        Log.Logger = Logger.None;
    }

    [TestMethod]
    public void LogMode_LogsWithCallerStackTraceAndSetsFailState()
    {
        var R717 = new Fluid(FluidList.Ammonia);

        R717.UpdatePT(Pressure.FromPascal(0), Temperature.FromKelvins(0));

        Assert.IsTrue(R717.FailState);
        var message = log.Messages.Single(m => m.Contains("UpdatePT"));
        StringAssert.Contains(message, "Called from:");
        var callerFrames = message.Substring(message.IndexOf("Called from:", StringComparison.Ordinal));
        StringAssert.Contains(callerFrames, nameof(LogMode_LogsWithCallerStackTraceAndSetsFailState));
        Assert.IsFalse(callerFrames.Contains("SharpFluids.Fluid"), "Frames inside SharpFluids should be left out of the caller stack trace");
    }

    [TestMethod]
    public void ThrowMode_UpdatePT_ThrowsFluidUpdateException()
    {
        Fluid.OnUpdateFailure = UpdateFailureMode.Throw;
        var R717 = new Fluid(FluidList.Ammonia);

        var e = Assert.ThrowsException<FluidUpdateException>(() => R717.UpdatePT(Pressure.FromPascal(0), Temperature.FromKelvins(0)));

        Assert.AreEqual("UpdatePT", e.Method);
        StringAssert.Contains(e.Inputs, "0");
        Assert.IsNotNull(e.InnerException);
        Assert.IsTrue(R717.FailState);
        //The CoolProp wrapper logs the raw error itself (AbstractState) - the Update failure report should not be logged in Throw mode
        Assert.IsFalse(log.Messages.Any(m => m.Contains("UpdatePT")), "The failure should be thrown, not logged, but got: " + string.Join(" | ", log.Messages));

        //The fluid is still usable afterwards
        R717.UpdatePT(Pressure.FromBar(10), Temperature.FromDegreeCelsius(20));
        Assert.IsFalse(R717.FailState);
    }

    [TestMethod]
    public void ThrowMode_UpdatePH_ThrowsFluidUpdateException()
    {
        Fluid.OnUpdateFailure = UpdateFailureMode.Throw;
        var CO2 = new Fluid(FluidList.CO2);

        var e = Assert.ThrowsException<FluidUpdateException>(() => CO2.UpdatePH(Pressure.FromPascal(0), SpecificEnergy.FromJoulePerKilogram(0)));

        Assert.AreEqual("UpdatePH", e.Method);
        Assert.AreEqual("CARBONDIOXIDE", e.FluidName, true);
        Assert.IsTrue(CO2.FailState);
    }

#if NET10_0_OR_GREATER
    [TestMethod]
    public void ThrowMode_FastUpdatePT_ThrowsFluidUpdateException()
    {
        Fluid.OnUpdateFailure = UpdateFailureMode.Throw;
        var CO2 = new Fluid(FluidList.CO2);

        var e = Assert.ThrowsException<FluidUpdateException>(() =>
            CO2.UpdatePT(EngineeringUnits.Fast.Pressure.FromPascal(0), EngineeringUnits.Fast.Temperature.FromKelvins(0)));

        Assert.AreEqual("UpdatePT", e.Method);
        Assert.IsTrue(CO2.FailState);
    }
#endif

    private class ListSink : ILogEventSink
    {
        public List<string> Messages { get; } = new();

        public void Emit(LogEvent logEvent) => Messages.Add(logEvent.RenderMessage());
    }
}
