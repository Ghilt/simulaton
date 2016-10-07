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
            Region r = new Region(0, 100, 100);

            //engine.AddSimulator(r);
            Life human = DebugSetup.CreateHuman();
            engine.AddSimulator(human);
            //engine.AddSimulator(h2);
            //engine.AddSimulator(h3);
            //engine.AddSimulator(h4);
            //engine.AddSimulator(h5);
            engine.Start();
        }
    }
}
