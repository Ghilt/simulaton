using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics
{
    class PropertySummary : Summary
    {
        private int id;
        private float amount;
        private bool bounded;

        public PropertySummary(int id, float amount, bool bounded)
        {
            this.bounded = bounded;
            this.id = id;
            this.amount = amount;
        }

        public override string ToString()
        {
            string name = Property.Name[id];
            string amount = (bounded) ? Logger.FloatToPercent(this.amount) : "" + this.amount;
            return name + " " + amount;
        }
    }
}
