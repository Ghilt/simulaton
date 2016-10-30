using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Events;

namespace Simulaton
{
    class Logger
    {
        private const int leftColumnWidth = 30;
        private const int rightColumnWidth = 60;

        private static SummaryManager summaryManager;

        internal static void PrintInfo(Object obj, string info)
        {
            string loggString = string.Format("{0," + leftColumnWidth + "}{1,-" + rightColumnWidth + "}", obj.GetType().Name + ":\t\t", info);
            if (summaryManager != null)
            {
                summaryManager.AddLogg(loggString);
            }
            else
            {
                Console.WriteLine(loggString);
                Console.ReadKey(true);
            }
        }

        internal static string FloatToPercentWithSign(float value)
        {
            string formatted = ((int)(value * 100) + "%");
            return value >= 0 ? "+" + formatted : formatted;
        }

        internal static void InjectSummaryManager(SummaryManager manager)
        {
            summaryManager = manager;
        }

        internal static string FloatToPercent(float value)
        {
            return ((int)(value * 100) + "%");
        }

    }
}