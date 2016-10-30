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
            InitiateConsole();
            Engine engine = new Engine();
            Region region = new Region(0, 100, 100);

            DebugSetup setup = new DebugSetup();
            setup.SetupTestEnvironment();
            Life humanDobbs = setup.CreateHuman("Dobby", region);
            Life humanMarco = setup.CreateHuman("Mark", region);

            region.AddEntity(humanDobbs);
            region.AddEntity(humanMarco);

            engine.AddEntity(humanDobbs);
            engine.AddEntity(region);
            engine.AddEntity(humanMarco);

            ConsolePresenter gui = new ConsolePresenter(Console.WindowWidth, Console.WindowHeight, engine.summaryManager);
            engine.Subscribe(gui);
            engine.Start();
        }

        public static void InitiateConsole()
        {
            int width = 120 > Console.LargestWindowWidth ? Console.LargestWindowWidth : 120;
            int height = 50 > Console.LargestWindowHeight ? Console.LargestWindowHeight : 50;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }
    }

}
