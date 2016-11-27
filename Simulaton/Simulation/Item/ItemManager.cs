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
        public Dictionary<int, List<Item>> accessible; // not currently used, remove?

        public ItemManager(Life parent)
        {
            this.parent = parent;
            this.accessible = new Dictionary<int, List<Item>>();
        }

        internal bool AccessToItem(int itemId) //Todo maybe count access to items to build up need for unaccessible items
        {
            List<Item> item;
            bool exist = parent.attachedEntities.TryGetItem(itemId, out item);
            return accessible.ContainsKey(itemId) || exist; 
        }

        internal bool TryGetItem(int itemId, out Item item)
        {
            List<Item> items;
            bool exist = accessible.TryGetValue(itemId, out items) ? true: parent.attachedEntities.TryGetItem(itemId, out items);

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
