using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class AttachedEntitiesList
    {
        List<Life> attachedLife = new List<Life>();
        Dictionary<int, List<Item>> attachedItems = new Dictionary<int, List<Item>>();

        public void Attach(Life life)
        {
            attachedLife.Add(life);
        }

        public void Detach(Life life)
        {
            attachedLife.Remove(life);
        }

        public void Attach(Item item)
        {
            List<Item> list;
            if (attachedItems.TryGetValue(item.id, out list))
            {
                list.Add(item);
            } else
            {
                attachedItems.Add(item.id, new List<Item> { item });
            }
        }

        public void Detach(Item item)
        {
            attachedItems.Remove(item.id);
        }

        internal bool TryGetItem(int itemId, out List<Item> item)
        {
            return attachedItems.TryGetValue(itemId, out item);
        }
    }
}
