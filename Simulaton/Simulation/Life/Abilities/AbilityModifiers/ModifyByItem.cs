using Simulaton.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    class ModifyByItem : AbilityModifier
    {
        private int itemId;
        private TransformFunction<float, float> function;

        public ModifyByItem(int itemId, TransformFunction<float, float> function)
        {
            this.itemId = itemId; // TODO maybe having itemId is misguided here, some other link between what the item helps with and what ability does, especially if You'd want dynamix item generation
            this.function = function;
        }

        public float GetModification(Life target)
        {
            Item item;
            if (target.TryGetItem(itemId, out item)) 
            {
                return function.Transform(item.quality);
            } else
            {
                return 1;
            }
        }

    }
}
