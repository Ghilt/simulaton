﻿using SictalSim.Attributes;
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

        public Human(int ticksBirth, Location location) : base(ticksBirth)
        {
            this.location = location;
            this.needs = NeedFactory.CreateBasicBiologicalNeeds(this);
        }


        public override void PerformTick()
        {
            location.Move();
            foreach (Need need in needs.Values)
            {
                need.Tick();
                need.Affect();
            }

        }

        public override string GetCurrentInfoLog()
        {
            return "Human, at x: " + location.GetX() + " y: " + location.GetY() + " Hunger: " + needs[Need.ID_HUNGER].ToString() + " Health: " + needs[Need.ID_HEALTH].ToString();
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo("Captain Albert Alexander died");
        }
    }
}
