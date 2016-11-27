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

        public RequireItem(Life target, int itemId)
        {
            this.target = target;
            this.itemId = itemId;
        }

        public bool IsFulfilled()
        {
            return target.AccessToItem(itemId);
        }
    }
}
