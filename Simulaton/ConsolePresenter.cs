using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class ConsolePresenter : IObserver<SummaryManager>
    {

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(SummaryManager data)
        {

            foreach (Entity e in data.GetCurrentData().Keys)
            {
                foreach (Summary s in data.GetCurrentData()[e])
                {
                    Logger.PrintInfo(this, e.name + " " + s.ToString());

                }
            }
        }
    }
}
