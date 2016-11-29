using Simulaton.Simulation;
using System;

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

            Life[] test = new Life[8];
            test[0] = setup.CreateHuman("Dobby", region);
            test[1] = setup.CreateHuman("Mark", region);
            test[2] = setup.CreateHuman("Jez", region);
            test[3] = setup.CreateHuman("Alan", region);
            test[4] = setup.CreateHuman("Sophie", region);
            test[5] = setup.CreateHuman("Jeff", region);
            test[6] = setup.CreateHuman("Super Hans", region);
            test[7] = setup.CreateHuman("Gerrard", region);

            Item sleepingBag = setup.GiveSleepingBag(test[0]);


            foreach (Life l in test)
            {
                engine.AddEntity(l);
            }
            engine.AddEntity(sleepingBag);
            engine.AddEntity(region);

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
