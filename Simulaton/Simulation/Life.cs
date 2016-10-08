using Simulaton.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class Life : Entity
    {

        private Location location;
        public Properties properties { private set; get; }
        private Abilities actions;
        private Brain brain;

        public Life(int ticksBirth, Location location)
            : base(ticksBirth)
        {
            this.brain = new Brain();
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
            location.Move();
            properties.OnTick();
            brain.MakeDecision(properties, actions);
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
                Logger.PrintInfo(this, "Tried to modify property " + Logger.Property[propertyIdTrigger] + " but Life did not have it");
            }
        }

        internal Location GetLocation()
        {
            return location;
        }

        public override string GetCurrentInfoLog()
        {
            string info = "Lifeform, at x: " + location.x + " y: " + location.y;
            foreach (Property n in properties.Values)
            {
                info += " , " + Logger.Property[n.id] + ": " + Logger.FloatToPercent(properties[n.id].amount);
            }
            return info;
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo(this, "Captain Albert Alexander died");
        }
    }
}
