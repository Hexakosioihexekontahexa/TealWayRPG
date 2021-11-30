using System;
using System.Collections.Generic;
using System.Text;
using Teal_Way_RPG.GameData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public class MainMenu : Menus
    {
        public static bool IsIdCheckerEnabled = false;

        public new static void Menu(bool debugVar)
        {
            while (true)
            {
                Clear();

                if (debugVar)
                {
                    DebugMenu(new Dictionary<string, object> {{"Game version: ", Launcher.GameVersion}});
                }

                SetColorsTo(ConsoleColor.Black, ConsoleColor.Cyan);
                Cw("Teal Way");
                SetColorsToDefault();
                Cw(" ");
                SetColorsTo(ConsoleColor.DarkYellow);
                CW("RPG");
                SetColorsToDefault();
                CW("============");
                CW("1. Start New Game");
                CW("2. Options");
                CW("3. Exit");
                if (IsIdCheckerEnabled) CW("0. Id Checker");
                var input = Console.ReadKey().KeyChar.ToString();
                switch (input)
                {
                    case "0":
                        if (IsIdCheckerEnabled)
                        {
                            IdChecker.Opener();
                            break;
                        }
                        else
                        {
                            Clear();
                            CW("Wrong input detected. Please, try again!");
                            CR();
                            Menu(debugVar);
                            break;
                        }

                    case "1":
                        NewGame.Starter();
                        GameMenu.Menu(debugVar, Towns.GoldenleafTown00);
                        break;
                    case "2":
                        Options.Menu();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Clear();
                        CW("Wrong input detected. Please, try again!");
                        CR();
                        break;
                }
            }
        }
    }
}
