using SictalSim.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Simulation
{
    class Human : Simulator
    {

        private Location location;
        private Dictionary<int, Need> needs;
        private Dictionary<int, Basis> attributes;

        public Human(int ticksBirth, Location location) : base(ticksBirth)
        {
            this.location = location;
            this.needs = new Dictionary<int, Need>();
            this.attributes = new Dictionary<int, Basis>();
            needs.Add(Need.ID_HUNGER, NeedFactory.CreateBasicNeed(Need.ID_HUNGER));
            attributes.Add(Basis.ID_HEALTH, new Basis(Basis.ID_HEALTH,0.8f));
        }


        public override void PerformTick()
        {
            location.Move();
            foreach (Need need in needs.Values)
            {
                need.Tick();
                need.Affect(attributes);
            }
        }

        public override string GetCurrentInfoLog()
        {
            return "Human, at x: " + location.GetX() + " y: " + location.GetY() + " Hunger: " + needs[Need.ID_HUNGER].ToString() + " Health: " + attributes[Basis.ID_HEALTH].ToString();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
