using Simulaton.Simulation;
using System;
using Simulaton.Events;

namespace Simulaton.Simulation
{
    public interface Location : Resource
    {

        bool TryMove(float direction);

        void PostEvent(Event e);
        void OnExit(ProteanEntity proteanEntity);
        void OnEnter(ProteanEntity proteanEntity);
    }
}