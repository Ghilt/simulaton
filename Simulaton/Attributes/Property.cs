using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;

namespace Simulaton.Attributes
{
    public partial class Property
    {
        private Life owner;

        public int id { get; private set; }
        public float amount { get; private set; }

        public Property(Life human, int id, float amount)
        {
            this.owner = human;
            this.id = id;
            this.amount = amount;
        }

        internal void ModifyAmount(float quantity)
        {
            amount += quantity;
            if (amount > 1)
            {
                amount = 1;
            }
            else if (amount < 0)
            {
                amount = 0;
            }
        }
    }
}
