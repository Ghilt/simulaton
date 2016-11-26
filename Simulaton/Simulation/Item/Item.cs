using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Events;

namespace Simulaton.Simulation
{
    public partial class Item : Entity
    {
        public int id { get; private set; }

        public Item(int id, int ticksBirth, string name) : base(ticksBirth, name)
        {
            this.id = id;
        }

        public override void OnTick()
        {

        }

        public override void OnEvent(Event exteriorEvent)
        {
            exteriorEvent.Handle(this);
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo(this, name + " terminated");
        }

    }
}
