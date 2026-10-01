using EngineeringUnits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFluids;
using System.Collections.Generic;

namespace UnitsTests;

/// <summary>
/// Equals() is identity, == compares the state within tolerances. Consumers rely on the difference
/// (COMP2.PlantSolver matches connections with Equals).
/// </summary>
[TestClass]
public class EqualityTests
{
    private static Fluid Ammonia5Bar40C()
    {
        var fluid = new Fluid(FluidList.Ammonia) { MassFlow = MassFlow.FromKilogramPerSecond(1) };
        fluid.UpdatePT(Pressure.FromBar(5), Temperature.FromDegreesCelsius(40));
        return fluid;
    }

    [TestMethod]
    public void EqualsIsIdentity()
    {
        var a = Ammonia5Bar40C();
        var b = Ammonia5Bar40C();

        Assert.IsTrue(a.Equals(a));
        Assert.IsFalse(a.Equals(b), "Two different Fluid objects with the same state must not be Equal");
        Assert.IsFalse(a.Equals(null));
    }

    [TestMethod]
    public void OperatorComparesState()
    {
        var a = Ammonia5Bar40C();
        var b = Ammonia5Bar40C();

        Assert.IsTrue(a == b, "== compares the state within tolerances");
        Assert.IsFalse(a != b);
    }

    [TestMethod]
    public void CollectionsFindTheObjectNotAnEqualState()
    {
        var a = Ammonia5Bar40C();
        var b = Ammonia5Bar40C();
        var list = new List<Fluid> { a };

        Assert.IsTrue(list.Contains(a));
        Assert.IsFalse(list.Contains(b));
        Assert.AreEqual(-1, list.IndexOf(b));
    }
}
