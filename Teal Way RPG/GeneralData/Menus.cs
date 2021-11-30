using System.Collections.Generic;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public class Menus
    {
        public static void DebugMenu(Dictionary<string, object> menuPoints)
        {
            CW("Debug menu is active!");
                CW("-----");
                foreach (var point in menuPoints)
                {
                    CW($"{point.Key}: {point.Value}");
                }

                CW("=====");
        }

        public virtual void Menu(bool debugVar)
        {
            if (debugVar)
            {
                DebugMenu(new Dictionary<string, object> { { "Game version: ", Launcher.GameVersion } });
            }
        }
    }
}
