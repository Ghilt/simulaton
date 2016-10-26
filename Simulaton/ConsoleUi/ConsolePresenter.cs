using Simulaton.ConsoleUi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class ConsolePresenter : IObserver<SummaryManager>
    {
        private int windowHeight;
        private int windowWidth;

        public ConsolePresenter(int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

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
            Console.SetCursorPosition(0, 0);
            var listData = data.GetCurrentData();
            //ConsoleFrame ui = new ConsoleFrame(windowWidth - 1, windowHeight - 1);
            ConsoleFrame ui = new ConsoleFrame(50, 40);
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (Entity e in listData.Keys)
            {
                LifeUiFrame frame = new LifeUiFrame(e.name, listData[e], 50, 20);
                ui.InsertEarliestTopLeft(frame);
            }
            string render = ui.GetFrameRender();
            Console.Write(render);
            Console.SetCursorPosition(0, 40);
            //Console.SetCursorPosition(0, windowHeight - 1);
        }
    }
}
