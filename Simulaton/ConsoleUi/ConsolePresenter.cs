using Simulaton.ConsoleUi;
using Simulaton.Mechanics;
using Simulaton.Simulation;
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
        private Controls controls;

        public ConsolePresenter(int windowWidth, int windowHeight, SummaryManager summaryManager)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.controls = new Controls(this, windowWidth, windowHeight);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Logger.InjectSummaryManager(summaryManager);
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
            controls.NextTick(data);
        }

        public void RenderCurrentTick(SummaryManager data)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            var listData = data.GetCurrentData();
            ConsoleFrame ui = CreateFullFrame();
            ui.InsertEarliestTopLeft(controls.GetControlsFrame(false));
            foreach (Guid e in listData.Keys)
            {
                EntityUiFrame frame = new EntityUiFrame(e.ToString(), listData[e], 50, 10);
                ui.InsertEarliestTopLeft(frame);
            }
            string render = ui.GetFrameRender();
            Console.Write(render);
            Console.SetCursorPosition(0, windowHeight - 1);
            controls.UserAction(data);
        }

        public void ShowLog(SummaryManager data, int offset)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            var listData = data.GetCurrentLogs();
            ConsoleFrame ui = CreateFullFrame();
            ui.InsertEarliestTopLeft(controls.GetControlsFrame(true));

            for (int i  = 0; i < listData.Count; i++)
            {
                if (i < offset)
                {
                    continue;
                }
                bool success = ui.InsertEarliestTopLeft(listData[i]);
                if (!success)
                {
                    ui.InsertEarliestTopLeft("Logg too long");
                }
            }

            string render = ui.GetFrameRender();
            Console.Write(render);
            Console.SetCursorPosition(0, windowHeight - 1);
            controls.UserAction(data);
        }

        public ConsoleFrame CreateFullFrame()
        {
            return new ConsoleFrame(windowWidth - 1, windowHeight - 1);
        }
    }
}
