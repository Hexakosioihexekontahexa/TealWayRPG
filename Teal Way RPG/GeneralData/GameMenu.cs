using System;
using System.Collections.Generic;
using System.Text;
using Teal_Way_RPG.GameData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    class GameMenu : Menus
    {
        public static void Menu(bool debugVar, Town town)
        {
            if (debugVar)
            {
                DebugMenu(new Dictionary<string, object> { { "Game version: ", Launcher.GameVersion } });
            }
            CW($"Welcome to {town}!");
            //CW();
        }
    }
}
