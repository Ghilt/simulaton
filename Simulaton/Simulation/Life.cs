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
        public Needs needs { private set; get; }
        public Brain brain { private set; get; }

        public Life(int ticksBirth, String name, Location location)
            : base(ticksBirth, name)
        {
            this.brain = new Brain(this);
            this.location = location;
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
            printneeds();
            location.Move(); // todo remove
            needs.OnTick();
            brain.MakeDecision(needs, actions);
        }

        public override void OnEvent(Event exteriorEvent)
        {
            exteriorEvent.Handle(this);
        }

        internal void ModifyNeed(int propertyIdTrigger, float magnitude)
        {
            Need toModify;
            needs.TryGetValue(propertyIdTrigger, out toModify);
            if (toModify != null)
            {
                toModify.Modify(magnitude);
            }
            else
            {
                Logger.PrintInfo(this, "Tried to modify need " + Need.Name[propertyIdTrigger] + " but Life did not have it");
            }
        }

        internal Location GetLocation()
        {
            return location;
        }

        private void printneeds()
        {
            Logger.PrintInfo(this, "_________________________________________");
            Logger.PrintInfo(this, name + ", at x: " + location.x + " y: " + location.y);
            string info = "";
            foreach (Need n in needs.Values)
            {
                info += " " + Need.Name[n.id] + ": " + Logger.FloatToPercent(needs[n.id].amount);
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
            foreach(Need p in needs.Values)
            {
                Summary summary = new Summary(Summary.TYPE_PROPERTY,p.id,p.amount);
                summaries.Add(summary);
            }
            return summaries.ToArray(); ;
        }

    }
}
