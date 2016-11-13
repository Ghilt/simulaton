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
        private bool bounded;
        public int id { get; private set; }
        public float amount { get; private set; }

        public Property(int id, float amount, bool bounded = true)
        {
            this.id = id;
            this.amount = amount;
            this.bounded = bounded;
        }

        internal void ModifyAmount(float quantity)
        {
            amount += quantity;
            if (bounded)
            {
                amount = amount > 1 ? 1 : amount < 0 ? 0 : amount;
            }
        }

    }
}
