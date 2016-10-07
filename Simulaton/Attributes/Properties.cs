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

        internal void tick()
        {
            foreach (Property need in this.Values)
            {
                need.Tick();
                need.Affect();
            }
            UpdateSortedList();
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
