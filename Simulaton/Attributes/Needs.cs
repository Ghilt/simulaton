﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public class Needs : Dictionary<int, Need>
    {
        List<Need> sortedOnImportance;

        public Needs()
        {
            UpdateSortedList();
        }

        private void UpdateSortedList()
        {
            sortedOnImportance = this.ToList().Select(pair => pair.Value).ToList(); // todo refactor
            sortedOnImportance.Sort((need1, need2) => need1.GetImportance().CompareTo(need2.GetImportance()));
            sortedOnImportance.Reverse();
        }

        internal void tick()
        {
            foreach (Need need in this.Values)
            {
                need.Tick();
                need.Affect();
            }
            UpdateSortedList();
        }

        internal IEnumerable<Need> SortedOnImportance()
        {
            return sortedOnImportance;
        }

        internal Need getMostImportantNeed()
        {
            return sortedOnImportance[0];
        }
    }
}
