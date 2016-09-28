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

        public int id { get; private set; }
        private List<Consequence> consequences;
        private Life parent;


        public Ability(int id, Life parent)
        {
            this.id = id;
            this.parent = parent;
            this.consequences = new List<Consequence>();
        }

        public void AddConsequence(Consequence consequence)
        {
            consequences.Add(consequence);
        }

        internal bool DoesSatisfyNeed(int needId)
        {
            foreach (Consequence consequence in consequences) // TODO: maintain data on which needs are being satisfied in map?
            {
                if (consequence.DoesSatisfyNeed(needId))
                {
                    return true;
                }
            }
            return false;
        }

        internal void Execute(int needIdTriggering)
        {
            foreach (Consequence consequence in consequences)
            {
                consequence.Trigger(parent, needIdTriggering);
            }
        }

    }
}
