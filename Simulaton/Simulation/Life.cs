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
        private Abilities actions;
        public Properties properties { private set; get; }
        public Brain brain { private set; get; }

        public Life(int ticksBirth, String name, Location location)
            : base(ticksBirth, name)
        {
            this.brain = new Brain(this);
            this.location = location;
            this.properties = new Properties();
            this.actions = new Abilities();
        }

        public void AddProperty(Property property)
        {
            properties.Add(property);
        }

        internal bool TryGetPropertyValue(int propertyId, out float value)
        {
            return properties.TryGetValue(propertyId, out value);
        }

        public void AddAbility(Ability action)
        {
            actions.Add(action.id, action);
        }

        public override void OnTick()
        {
            AddSummary(CreateCurrentSummary());
            printProperties();
            location.Move(); // todo remove
            properties.OnTick();
            brain.MakeDecision(properties, actions);
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
                toModify.Modify(magnitude);
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

        private void printProperties()
        {
            Logger.PrintInfo(this, "_________________________________________");
            Logger.PrintInfo(this, name + ", at x: " + location.x + " y: " + location.y);
            string info = "";
            foreach (Property n in properties.Values)
            {
                info += " " + Property.Name[n.id] + ": " + Logger.FloatToPercent(properties[n.id].amount);
            }
            Logger.PrintInfo(this, info);
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo(this, name + " died");
        }


        private Summary[] CreateCurrentSummary()
        {
            List<Summary> summaries = new List<Summary>();
            foreach(Property p in properties.Values)
            {
                Summary summary = new Summary(Summary.TYPE_PROPERTY,p.id,p.amount);
                summaries.Add(summary);
            }
            return summaries.ToArray(); ;
        }

    }
}
