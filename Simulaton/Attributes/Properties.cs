using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public class Properties : Dictionary<int, Property>
    {
        List<Property> sortedOnImportance;

        public Properties()
        {
            UpdateSortedList();
        }

        public void Add(Property property)
        {
            Add(property.id, property);
        }

        private void UpdateSortedList()
        {
            sortedOnImportance = this.ToList().Select(pair => pair.Value).ToList(); // todo refactor
            sortedOnImportance.Sort((property1, property2) => property1.GetImportance().CompareTo(property2.GetImportance()));
            sortedOnImportance.Reverse();
        }

        internal void OnTick()
        {
            foreach (Property property in this.Values)
            {
                property.OnTick();
            }
            UpdateSortedList();
        }

        internal bool TryGetValue(int propertyId, out float value)
        {
            Property outProp;
            if (TryGetValue(propertyId, out outProp))
            {
                value = outProp.amount;
                return true;
            } 
            else
            {
                value = -1;
                return false;
            }
        }

        internal IEnumerable<Property> SortedOnImportance()
        {
            return sortedOnImportance;
        }

        internal Property getMostImportantProperty()
        {
            return sortedOnImportance[0];
        }
    }
}
