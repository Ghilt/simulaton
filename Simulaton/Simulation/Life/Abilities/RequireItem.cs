using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    class RequireItem : AbilityRequirement
    {
        private Life target;
        private int itemId;
        private float amount;

        public RequireItem(Life target, int itemId, float amount)
        {
            this.target = target;
            this.itemId = itemId;
            this.amount = amount;
        }

        public bool IsFulfilled()
        {
            return target.AccessToItem(itemId, amount);
        }
    }
}
