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

        public Life(int ticksBirth, NeedFactory needFactory, Location location) : base(ticksBirth)
        {
            this.location = location;
            this.needs = needFactory.CreateNeeds(this);
            this.actions = new Abilities();
        }

        public void AddAbility(Ability action)
        {
            actions.Add(action.id ,action);
        }

        public override void PerformTick()
        {
            location.Move();
            needs.tick();
            Need pressingDesire = needs.getMostImportantNeed();
            actions.ActUpon(pressingDesire.id);
            Logger.PrintInfo("Most pressing need: $ ", pressingDesire.id);
        }

        internal void ModifyNeed(int needIdTrigger, float magnitude)
        {
            needs[needIdTrigger].Modify(magnitude);
        }

        internal Location GetLocation()
        {
            return location;
        }

        public override string GetCurrentInfoLog()
        {
            return "Lifeform, at x: " + location.x + " y: " + location.y + " Food: " + needs[Need.ID_NOURISHMENT].ToString() + " Health: " + needs[Need.ID_HEALTH].ToString();
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo("Captain Albert Alexander died");
        }
    }
}
