using Simulaton.Events;

namespace Simulaton.Simulation
{
    public interface AbilityEvent
    {
        void Trigger(int propertyIdTrigger, Life target);
        float GetMagnitude();
        EvaluableResult EvaluateResult(int targetpropertyId);
    }
}
