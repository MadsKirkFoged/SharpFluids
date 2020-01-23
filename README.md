# SharpFluids
Lightweight CoolProp C# Wrapper - for easy fluid properties lookups

Lets say you want to know some properties of water;
Density of Water at 13°C
Boiling point of Co2 at 25bar
..and many more options
Then you have come to the right place!



How to start:

1. Create a new C# Console App(.NET Framework) project in Visual studio
2. Right click your new project and press 'Manage NuGet Packages'
3. Go to 'Browse' and search for 'SharpFluids' and press 'Install'
4. In your 'Program.cs' add to the top:
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;


5. Add in your main:

//Find the Density of water at 13°C
Fluid Water = new Fluid(FluidList.Water);
Water.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(13));
Console.WriteLine("Density of water at 13°C: " + Water.RHO);
