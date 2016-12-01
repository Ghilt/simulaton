using System;
using Simulaton.Events;
using Simulaton.DataInterface;
using static Simulaton.Simulation.AttachedEntitiesList;

namespace Simulaton.Simulation
{
    public partial class Item : ProteanEntity
    {
        public int id { get; private set; }
        public float quality { get; private set; }

        public Item(int id, int ticksBirth, string name, float quality, Location location) : base(ticksBirth, name, location)
        {
            this.id = id;
            this.quality = quality;
            location.OnEnter(this);
        }

        public override void OnTick()
        {
            AddSummary(new TextSummary(Item.Name[id]));
        }

        public override void OnEvent(Event exteriorEvent)
        {
            exteriorEvent.Handle(this);
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo(this, name + " terminated");
        }

        public override void OnSelfAttached(AttachDelegate attachDelegate)
        {
            attachDelegate.Action(this);
        }
    }
}
