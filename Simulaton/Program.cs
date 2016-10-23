using Simulaton.Attributes;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            Region region = new Region(0, 100, 100);

            Life humanDobbs = DebugSetup.CreateHuman("Dobby", region);
            Life humanMarco = DebugSetup.CreateHuman("Mark", region);

            region.AddEntity(humanDobbs);
            region.AddEntity(humanMarco);

            engine.AddEntity(humanDobbs);
            engine.AddEntity(region);
            engine.AddEntity(humanMarco);

            ConsolePresenter gui = new ConsolePresenter();
            engine.Subscribe(gui);
            engine.Start();
        }
    }
}
