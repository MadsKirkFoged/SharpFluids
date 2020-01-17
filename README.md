# SharpFluids
Lightweight CoolProp C# Wrapper - for easy fluid properties lookups



How to start:

1. Create a new C# Console App(.NET Framework) project in Visual studio
2. Right click your new project and press 'Manage NuGet Packages'
3. Go to 'Browse' and search for 'SharpFluids' and press 'Install'
4. In your 'Program.cs' add to the top:
 
using SharpFluids;
using UnitsNet;


5. Add in your main:

Fluid test = new Fluid(FluidList.Ammonia);
test.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(10));


6. More information will be added later...
