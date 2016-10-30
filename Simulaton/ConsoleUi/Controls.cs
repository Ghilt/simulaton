using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.ConsoleUi
{
    class Controls
    {
        private const int CONTROLS_HEIGHT = 3;

        private const char CONTROLS_SHOW_ENTITIES = '1';
        private const char CONTROLS_SHOW_LOGS = '2';
        private const char CONTROLS_CONINUE_NEXT_TICK = 'c';

        private int width;
        private int height;
        private char lastShownFrame;
        private ConsolePresenter presenter;

        public Controls(ConsolePresenter presenter, int width, int height)
        {
            lastShownFrame = '1';
            this.presenter = presenter;
            this.width = width;
            this.height = height;
        }

        internal ConsoleFrame GetControlsFrame()
        {
            ConsoleFrame ui = new ConsoleFrame(width - 1, CONTROLS_HEIGHT);
            ui.CreateBorder();
            ui.InsertEarliestTopLeft(CONTROLS_SHOW_ENTITIES + ": Show entities,  "
                + CONTROLS_SHOW_LOGS + ": Show Logs,  "
                + CONTROLS_CONINUE_NEXT_TICK + ": Continue next tick ");
            return ui;
        }

        internal void NextTick(SummaryManager data)
        {
            UpdateUI(data, lastShownFrame);
        }

        public void UserAction(SummaryManager data)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            UpdateUI(data, consoleKeyInfo.KeyChar);
        }

        public void UpdateUI(SummaryManager data, char key)
        {
            switch (key)
            {
                case '1':
                    lastShownFrame = key;
                    presenter.RenderCurrentTick(data);
                    break;
                case '2':
                    lastShownFrame = key;
                    presenter.ShowLog(data);
                    break;
                case 'c':
                    // Next Tick
                    break;
                default:
                    UserAction(data);
                    break;
            }
        }

    }
}
