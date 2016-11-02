using Simulaton.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Events;

namespace Simulaton.Simulation
{
    public class Life : Entity
    {

        private Location location;
        public Brain brain { private set; get; }
        public Dictionary<int, Property> properties { private set; get; }
        public Needs needs { private set; get; }
        private Abilities actions;

        public Life(int ticksBirth, String name, Location location)
            : base(ticksBirth, name)
        {
            this.location = location;
            this.brain = new Brain(this);
            this.properties = new Dictionary<int, Property>();
            this.needs = new Needs();
            this.actions = new Abilities();
        }

        public void AddNeed(Need need)
        {
            needs.Add(need);
        }

        internal bool TryGetPropertyValue(int propertyId, out float value)
        {
            return needs.TryGetValue(propertyId, out value);
        }

        public void AddAbility(Ability action)
        {
            actions.Add(action.id, action);
        }

        public override void OnTick()
        {
            AddSummary(CreateCurrentSummary());
            PrintNeeds();
            location.Move(); // todo remove
            needs.OnTick();
            brain.MakeDecision(needs, actions);
        }

        public override void OnEvent(Event exteriorEvent)
        {
            exteriorEvent.Handle(this);
        }

        internal void ModifyProperty(int propertyIdTrigger, float magnitude)
        {
            Property toModify;
            properties.TryGetValue(propertyIdTrigger, out toModify);
            if (toModify != null)
            {
                toModify.ModifyAmount(magnitude);
            }
            else
            {
                Logger.PrintInfo(this, "Tried to modify property " + Property.Name[propertyIdTrigger] + " but Life did not have it");
            }
        }

        internal Location GetLocation()
        {
            return location;
        }

        private void PrintNeeds()
        {
            Logger.PrintInfo(this, "_________________________________________");
            Logger.PrintInfo(this, name + ", at x: " + location.x + " y: " + location.y);
            string info = "";
            foreach (Need n in needs.Values)
            {
                info += " " + Property.Name[n.property.id] + ": " + Logger.FloatToPercent(needs[n.property.id].property.amount);
            }
            Logger.PrintInfo(this, info);
        }

        internal void AddProperty(Property property)
        {
            properties.Add(property.id, property);
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo(this, name + " died");
        }

        private Summary[] CreateCurrentSummary()
        {
            List<Summary> summaries = new List<Summary>();
            foreach (Need p in needs.Values)
            {
                Summary summary = new Summary(Summary.TYPE_PROPERTY, p.property.id, p.property.amount);
                summaries.Add(summary);
            }
            return summaries.ToArray(); ;
        }

        internal Property GetProperty(int propertyId)
        {
            return properties[propertyId];
        }
    }
}
