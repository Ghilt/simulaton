using Simulaton.Events;
using Simulaton.Simulation;
using System.Collections.Generic;


namespace Simulaton.Simulation
{
    public partial class Ability
    {

        public int id { get; private set; }
        public Life parent { get; private set; }
        public List<AbilityEvent> consequences { get; private set; }
        private List<AbilityRequirement> requirements;
        private HashSet<int> satisfiablePropertyIds;

        public Ability(int id, Life parent)
        {
            this.id = id;
            this.parent = parent;
            this.consequences = new List<AbilityEvent>();
            this.requirements = new List<AbilityRequirement>();
            this.satisfiablePropertyIds = new HashSet<int>();
        }

        public List<EvaluableResult> GetPrediction(int targetId)
        {
            List<EvaluableResult> impact = new List<EvaluableResult>();
            foreach (AbilityRequirement requirement in requirements)
            {
                if (!requirement.IsFulfilled())
                {
                    Logger.PrintInfo(this, Ability.Name[id] + " not possible, failing requirements");
                    return impact;
                }
            }
            foreach (AbilityEvent consequence in consequences)
            {
                impact.Add(consequence.EvaluateResult(targetId));
            }
            return impact;
        }

        public void AddSatisfiableProperty(int propertyId)
        {
            satisfiablePropertyIds.Add(propertyId);
        }

        public void AddConsequence(AbilityEvent consequence)
        {
            consequences.Add(consequence);
        }

        internal void AddRequirement(AbilityRequirement requirement)
        {
            requirements.Add(requirement);
        }

        internal virtual void Execute(int targetPropertyId)
        {
            Logger.PrintInfo(this, "Do " + Name[id] + " with target " + Property.Name[targetPropertyId]);

            foreach (AbilityEvent consequence in consequences)
            {
                consequence.Trigger(targetPropertyId, parent);
            }
        }

        internal bool Satisfies(int id)
        {
            return satisfiablePropertyIds.Contains(id);
        }
    }
}
