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
