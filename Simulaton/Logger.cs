using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class Logger
    {
        private const int leftColumnWidth = 30;
        private const int rightColumnWidth = 60;
        static Logger()
        {
            int width = 120 > Console.LargestWindowWidth ? Console.LargestWindowWidth : 120;
            int height = 50 > Console.LargestWindowHeight ? Console.LargestWindowHeight : 50;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public static Dictionary<int, string> Property = new Dictionary<int, string>()
        {
            { Attributes.Property.ID_HEALTH, "Health"},
            { Attributes.Property.ID_NOURISHMENT, "Nourishment"},
            { Attributes.Property.ID_ENERGY, "Energy"},
            { Attributes.Property.ID_SOCIAL_INTERACTION, "Social interaction"}
        };

        public static Dictionary<int, string> Ability = new Dictionary<int, string>()
        {
            { Attributes.Ability.ID_SEARCH, "Search"},
            { Attributes.Ability.ID_SLEEP, "Sleep"},
            { Attributes.Ability.ID_SOCIALIZE, "Socialize"}
        };

        internal static void PrintInfo(Object obj, string info)
        {
            Console.WriteLine("{0," + leftColumnWidth + "}{1,-" + rightColumnWidth + "}", obj.GetType().Name + ":\t\t", info);
        }

        internal static string FloatToPercentWithSign(float value)
        {
            string formatted = ((int)(value * 100) + "%");
            return value >= 0 ? "+" + formatted : formatted;
        }

        internal static string FloatToPercent(float value)
        {
            return ((int)(value * 100) + "%");
        }
    }
}
