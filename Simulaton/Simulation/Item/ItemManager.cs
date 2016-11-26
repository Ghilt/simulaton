using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class ItemManager
    {
        private Life parent;
        public Dictionary<int, List<Item>> accessable;

        public ItemManager(Life parent)
        {
            this.parent = parent;
            this.accessable = new Dictionary<int, List<Item>>();
        }

        internal bool AccessToItem(int itemId, float amount)
        {
            return accessable.ContainsKey(itemId) && accessable[itemId].Count >= amount;
        }
    }
}
