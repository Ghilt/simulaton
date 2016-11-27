using System;
using Simulaton.Events;

namespace Simulaton.Simulation
{
    class AttachedLocation : Location
    {
        private ProteanEntity holder;

        public AttachedLocation(ProteanEntity holder)
        {
            this.holder = holder;
        }

        public float Extract(int propertyId)
        {
            throw new NotImplementedException(); // todo restructure/remove/refacotr entire 'resource' concept
        }

        public void OnEnter(ProteanEntity proteanEntity)
        {
            holder.Attach(proteanEntity);
        }

        public void OnExit(ProteanEntity proteanEntity)
        {
            holder.Detach(proteanEntity);
        }

        public void PostEvent(Event e)
        {
            holder.PostEvent(e);
        }

        public bool TryMove(float direction)
        {
            return false;
        }
    }
}
