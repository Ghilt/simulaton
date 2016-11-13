using Simulaton.Attributes;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics
{
    public class Summary
    {
        public const int TYPE_PROPERTY = 0;
        public const int TYPE_ABILITY = 1;

        private float amount;
        private int type;
        private int id;

        public Summary(int type, int id, float amount)
        {
            this.type = type;
            this.id = id;
            this.amount = amount;
        }

        public Summary(int type, int id)
        {
            this.type = type;
            this.id = id;
            this.amount = 0;
        }

        public override string ToString()
        {
            string name = (type == TYPE_PROPERTY) ? Property.Name[id] : Ability.Name[id];
            string amount = (type == TYPE_PROPERTY) ? Logger.FloatToPercent(this.amount) : "";
            return name + " " + amount;
        }
    }
}
