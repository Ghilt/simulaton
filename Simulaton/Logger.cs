using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class Logger
    {
        static Logger()
        {
            int width = 120 > Console.LargestWindowWidth ? Console.LargestWindowWidth : 120;
            int height = 70 > Console.LargestWindowHeight ? Console.LargestWindowHeight : 70;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public static Dictionary<int, string> Need = new Dictionary<int, string>()
        {
            { Attributes.Need.ID_HEALTH, "Health"},
            { Attributes.Need.ID_NOURISHMENT, "Nourishment"},
            { Attributes.Need.ID_ENERGY, "Energy"}
        };

        public static Dictionary<int, string> Ability = new Dictionary<int, string>()
        {
            { Attributes.Ability.ID_SEARCH, "Search"},
            { Attributes.Ability.ID_SLEEP, "Sleep"}
        };

        internal static void PrintInfo(Object obj, string info)
        {
            Console.WriteLine("{0,22}{1,-60}", obj.GetType().Name + ":\t\t", info);
        }
    }
}
