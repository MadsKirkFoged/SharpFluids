# SharpFluids
Lightweight CoolProp C# Wrapper - for easy fluid properties lookups



How to start:

1. Create a new C# Console App(.NET Framework) project in Visual studio
2. Right click your new project and press 'Manage NuGet Packages'
3. Go to 'Browse' and search for 'SharpFluids' and press 'Install'
4. In your 'Program.cs':
 - Write:
 
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Fluid test = new Fluid(FluidList.Ammonia);

            test.UpdatePT(Pressure.FromBars(10), Temperature.FromDegreesCelsius(10));


        }
    }
}



5. More information will be added later...
