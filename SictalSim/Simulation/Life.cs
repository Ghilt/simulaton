using SictalSim.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Simulation
{
    class Life : Simulator
    {

        private Location location;
        private Needs needs;

        public Life(int ticksBirth, NeedFactory needFactory, Location location) : base(ticksBirth)
        {
            this.location = location;
            this.needs = needFactory.CreateNeeds(this);
        }


        public override void PerformTick()
        {
            location.Move();
            needs.tick();
            Need pressingDesire = needs.getMostImportantNeed();
            //talents.ActUpon(pressingDesire)
        }

        public override string GetCurrentInfoLog()
        {
            return "Lifeform, at x: " + location.GetX() + " y: " + location.GetY() + " Hunger: " + needs[Need.ID_HUNGER].ToString() + " Health: " + needs[Need.ID_HEALTH].ToString();
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo("Captain Albert Alexander died");
        }
    }
}
