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

        /// <summary>
        /// Used for default playable town.
        /// </summary>
        /// <param name="specs">Used for defining town properties like encountering special shopkeepers, town effects, etc.</param>
        public static void Menu(bool debugVar, params string[] specs)
        {
            if (debugVar)
            {
                DebugMenu(new Dictionary<string, object> { { "Game version: ", Launcher.GameVersion } });
            }

            for (var i = 0; i < specs.Length; i++)
            {
                StringBuilder text = SB($"Welcome to {Towns.GoldenleafTown00}!\n" +
                                        $"1. Go to ");
                CW(Text);
            }
        }
    }
}
