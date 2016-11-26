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
        public Dictionary<int, List<Item>> accessible;

        public ItemManager(Life parent)
        {
            this.parent = parent;
            this.accessible = new Dictionary<int, List<Item>>();
        }

        internal bool AccessToItem(int itemId, float amount) //Todo maybe count access to items to build up need for unaccessible items
        {
            return accessible.ContainsKey(itemId) && accessible[itemId].Count >= amount; 
        }

        internal bool TryGetItem(int itemId, out Item item)
        {
            List<Item> items;
            bool exist = accessible.TryGetValue(itemId, out items);
            if (exist)
            {
                item = items.First();
            }
            else
            {
                item = null;
            }
            return exist;
        }
    }
}
