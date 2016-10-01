using Simulaton.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    class Life : Simulator
    {

        private Location location;
        private Needs needs;
        private Abilities actions;
        private Brain brain;

        public Life(int ticksBirth, NeedFactory needFactory, Location location)
            : base(ticksBirth)
        {
            this.brain = new Brain();
            this.location = location;
            this.needs = needFactory.CreateNeeds(this);
            this.actions = new Abilities();
        }

        public void AddAbility(Ability action)
        {
            actions.Add(action.id, action);
        }

        public override void PerformTick()
        {
            location.Move();
            needs.tick();
            brain.MakeDecision(needs, actions);
        }

        internal void ModifyNeed(int needIdTrigger, float magnitude)
        {
            Need toModify;
            needs.TryGetValue(needIdTrigger, out toModify);
            if (toModify != null)
            {
                toModify.Modify(magnitude);

            }
            else
            {
                Logger.PrintInfo(this, "Tried to modify need " + Logger.Need[needIdTrigger] + " but Life did not have it");
            }
        }

        internal Location GetLocation()
        {
            return location;
        }

        public override string GetCurrentInfoLog()
        {
            string info = "Lifeform, at x: " + location.x + " y: " + location.y;
            foreach (Need n in needs.Values)
            {
                info += " , " + Logger.Need[n.id] + ": " + needs[n.id].ToString();
            }
            return info;
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo(this, "Captain Albert Alexander died");
        }
    }
}
