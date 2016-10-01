using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class Ability
    {
        public const int ID_SEARCH = 0;
        public const int ID_SLEEP = 1;

        public int id { get; private set; }
        private List<Consequence> consequences;
        private Life parent;

        public Ability(int id, Life parent)
        {
            this.id = id;
            this.parent = parent;
            this.consequences = new List<Consequence>();
        }

        internal float EvaluateEffectiveness(Needs needs)
        {
            float value = 0;
            foreach (Consequence consequence in consequences)
            {
                foreach (Need need in needs.SortedOnImportance())
                {
                    if (consequence.DoesSatisfyNeed(need.id))
                    {
                        value += need.GetImportance() * consequence.getMagnitude();
                        if (!consequence.CanSatisfyMultipleNeeds()) break;

                    }
                }
            }
            return value;
        }

        public void AddConsequence(Consequence consequence)
        {
            consequences.Add(consequence);
        }

        internal void Execute(IEnumerable<Need> sortedOnImportance)
        {
            Logger.PrintInfo(this, "Do " + Logger.Ability[id]);

            foreach (Consequence consequence in consequences)
            {
                foreach (Need need in sortedOnImportance)
                {
                    if (consequence.DoesSatisfyNeed(need.id))
                    {
                        consequence.Trigger(parent, need.id);
                        break;
                    }
                }
            }
        }

    }
}
