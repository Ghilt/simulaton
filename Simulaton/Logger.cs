using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class Logger
    {
        public static Dictionary<int, string> needTranslations = new Dictionary<int, string>()
        {
            { Attributes.Need.ID_HEALTH, "Health"},
            { Attributes.Need.ID_NOURISHMENT, "Nourishment"},
            { Attributes.Need.ID_ENERGY, "Energy"}
        };

        internal static void PrintInfo(string info)
        {
            Console.WriteLine(info);
        }

        internal static void PrintInfo(string info, int needId)
        {
            Console.WriteLine(info.Replace("$", needTranslations[needId]));
        }
    }
}
