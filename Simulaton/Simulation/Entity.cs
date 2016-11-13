using Simulaton.Events;
using Simulaton.Mechanics;

namespace Simulaton.Simulation
{
    public abstract class Entity
    {
        public static int SIMULATION_STATUS_ALIVE = 0;
        public static int SIMULATION_STATUS_TERMINATED = 1;

        private readonly int ticksBirth;
        private int ticksAlive;
        private bool loggActive;
        private int simulationStatus;
        private EventManager eventManager;
        private SummaryManager summaryManager;

        public string name { private set; get; }

        public Entity(int ticksBirth, string name)
        {
            eventManager = new EventManager(this);
            this.name = name;
            this.ticksBirth = ticksBirth;
            simulationStatus = SIMULATION_STATUS_ALIVE;
            loggActive = true;
        }

        public void Tick()
        {
            //if (loggActive) Logger.PrintInfo(this, GetCurrentInfoLog());
            ticksAlive++;
            OnTick();
            eventManager.TriggerEvents();
        }

        internal void PostTick()
        {
            eventManager.UpdateEvents();
        }

        public abstract void OnTick();

        public abstract void OnEvent(Event exteriorEvent);

        internal int CheckStatus()
        {
            return simulationStatus;
        }

        public void PostEvent(Event e){
            eventManager.Add(e);
        }

        public void Terminate()
        {
            simulationStatus = SIMULATION_STATUS_TERMINATED;
            OnTerminate();
        }

        public abstract void OnTerminate();

        internal void SetSummaryManager(SummaryManager summaryManager)
        {
            this.summaryManager = summaryManager;
        }

        internal void AddSummary(params Summary[] summaries)
        {
            summaryManager.AddSummary(this, summaries);
        }
    }
}
