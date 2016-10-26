using Simulaton.Attributes;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.ConsoleUi
{
    class LifeUiFrame : ConsoleFrame
    {
        private List<Summary> stateSummary;
        private string name;

        public LifeUiFrame(string name, List<Summary> stateSummary, int width, int height) : base(width, height)
        {
            this.name = name;
            this.stateSummary = stateSummary;
            CreateBorder(width, height);
            foreach (Summary data in stateSummary)
            {
                InsertEarliestTopLeft(data.ToString());
            }
        }
    }
}
