using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Mechanics;

namespace Simulaton
{
    class Engine : IObservable<SummaryManager>
    {
        private Tick tick;
        public SummaryManager summaryManager { private set; get; }
        private List<IObserver<SummaryManager>> observers;
        private List<Entity> entities;

        public Engine()
        {
            tick = new Tick();
            entities = new List<Entity>();
            this.summaryManager = new SummaryManager();
            this.observers = new List<IObserver<SummaryManager>>();
            summaryManager.SetTimeTicker(tick);
        }

        public void Start()
        {
            List<Entity> entitiesToRemove = new List<Entity>();
            Logger.PrintInfo(this, "Started, press any key to continue");
            Console.ReadKey(true);
            while (entities.Count != 0)
            {

                tick++;
                Logger.PrintInfo(this, tick.ToString());
                foreach (Entity sim in entities)
                {
                    sim.Tick();
                }


                foreach (Entity sim in entities)
                {
                    sim.PostTick();
                    int status = sim.CheckStatus();
                    if (status == Entity.SIMULATION_STATUS_TERMINATED)
                    {
                        entitiesToRemove.Add(sim);
                    }

                }

                entitiesToRemove.ForEach(entity => entities.Remove(entity));
                entitiesToRemove.Clear();

                observers.ForEach(observer => observer.OnNext(summaryManager));
                Logger.PrintInfo(this, "> \n");
            }

            Logger.PrintInfo(this, "finished, press enter to exit");
            Console.ReadLine();

        }

        public void AddEntity(Entity sim)
        {
            entities.Add(sim);
            sim.SetSummaryManager(summaryManager);
        }

        public IDisposable Subscribe(IObserver<SummaryManager> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                observer.OnNext(summaryManager);
            }
            return new Unsubscriber(observers,observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<SummaryManager>> _observers;
            private IObserver<SummaryManager> _observer;

            internal Unsubscriber(List<IObserver<SummaryManager>> observers, IObserver<SummaryManager> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }

}
