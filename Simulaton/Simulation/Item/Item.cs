using System;
using Simulaton.Events;

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

        }

        public override void OnEvent(Event exteriorEvent)
        {
            exteriorEvent.Handle(this);
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo(this, name + " terminated");
        }

        public override void OnAttach(AttachedEntitiesList attachedEntities)
        {
            attachedEntities.Attach(this);
        }

        public override void OnDetach(AttachedEntitiesList attachedEntities)
        {
            attachedEntities.Detach(this);
        }
    }
}
