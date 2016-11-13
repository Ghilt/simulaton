using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class Needs : Dictionary<int, Need>
    {
        List<Need> sortedOnImportance;

        public Needs()
        {
            UpdateSortedList();
        }

        public void Add(Need need)
        {
            Add(need.property.id, need);
        }

        private void UpdateSortedList()
        {
            sortedOnImportance = this.ToList().Select(pair => pair.Value).ToList(); // todo refactor
            sortedOnImportance.Sort((need1, need2) => need1.GetImportance().CompareTo(need2.GetImportance()));
            sortedOnImportance.Reverse();
        }

        internal void OnTick()
        {
            foreach (Need need in this.Values)
            {
                need.OnTick();
            }
            UpdateSortedList();
        }

        internal bool TryGetValue(int propertyId, out float value)
        {
            Need outProp;
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

        internal IEnumerable<Need> SortedOnImportance()
        {
            return sortedOnImportance;
        }

        internal Need getMostImportantNeed ()
        {
            return sortedOnImportance[0];
        }
    }
}
