using Simulaton.Attributes;
using Simulaton.Mechanics;
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
            CreateBorder();
            string nameFormat = "   " + name + "   ";
            InsertIgnoreOccupied(width/2 - nameFormat.Length/2,0, nameFormat);
            foreach (Summary data in stateSummary)
            {
                InsertEarliestTopLeft(data.ToString());
            }
        }
    }
}
