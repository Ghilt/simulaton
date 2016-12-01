using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class AttachedEntitiesList
    {
        private List<Life> attachedLife = new List<Life>();
        private Dictionary<int, List<Item>> attachedItems = new Dictionary<int, List<Item>>();
        public AttachDelegate detacher { get; private set; }
        public AttachDelegate attacher { get; private set; }

        public AttachedEntitiesList()
        {
            detacher = new AttachDelegate(x => attachedLife.Remove(x), x => attachedItems.Remove(x.id));
            attacher = new AttachDelegate(attachedLife.Add, AddItem);
        }

        public void AddItem(Item item)
        {
            List<Item> list;
            if (attachedItems.TryGetValue(item.id, out list))
            {
                list.Add(item);
            }
            else
            {
                attachedItems.Add(item.id, new List<Item> { item });
            }
        }

        internal bool TryGetItem(int itemId, out List<Item> item)
        {
            return attachedItems.TryGetValue(itemId, out item);
        }

        public class AttachDelegate
        {
            private Action<Item> itemAction;
            private Action<Life> lifeAction;

            public AttachDelegate(Action<Life>  actionOnLifeList, Action<Item> actionOnItemList)
            {
                this.itemAction = actionOnItemList;
                this.lifeAction = actionOnLifeList;
            }

            public void Action(Life life)
            {
                lifeAction(life);
            }

            public void Action(Item item)
            {
                itemAction(item);
            }
        }
    }
}
