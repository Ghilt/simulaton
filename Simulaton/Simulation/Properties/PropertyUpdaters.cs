using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class PropertyUpdaters : Dictionary<int, PropertyUpdater>
    {
        List<PropertyUpdater> sortedOnImportance;

        public PropertyUpdaters()
        {
            UpdateSortedList();
        }

        public void Add(PropertyUpdater propertyUpdater)
        {
            Add(propertyUpdater.property.id, propertyUpdater);
        }

        private void UpdateSortedList()
        {
            sortedOnImportance = this.ToList().Select(pair => pair.Value).ToList(); // todo refactor
            sortedOnImportance.Sort((propertyUpdater1, propertyUpdater2) => propertyUpdater1.GetImportance().CompareTo(propertyUpdater2.GetImportance()));
            sortedOnImportance.Reverse();
        }

        internal void OnTick()
        {
            foreach (PropertyUpdater propertyUpdater in this.Values)
            {
                propertyUpdater.OnTick();
            }
            UpdateSortedList();
        }

        internal bool TryGetValue(int propertyId, out float value)
        {
            PropertyUpdater outProp;
            if (TryGetValue(propertyId, out outProp))
            {
                value = outProp.property.amount;
                return true;
            } 
            else
            {
                value = -1;
                return false;
            }
        }

        internal IEnumerable<PropertyUpdater> SortedOnImportance()
        {
            return sortedOnImportance;
        }

        internal PropertyUpdater getMostImportantPropertyUpdater ()
        {
            return sortedOnImportance[0];
        }
    }
}
