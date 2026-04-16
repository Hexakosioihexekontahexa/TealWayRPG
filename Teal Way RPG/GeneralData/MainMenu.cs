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
                var hasCurrentGame = NewGame.PlayerLevel > 0;
                var hasSaveFile = SaveLoad.HasSaveFile();
                var canContinue = hasCurrentGame || hasSaveFile;

                Clear();

                SetColorsTo(ConsoleColor.Black, ConsoleColor.Cyan);
                Cw("Teal Way");
                SetColorsToDefault();
                Cw(" ");
                SetColorsTo(ConsoleColor.DarkYellow);
                Cw("RPG");
                SetColorsToDefault();
                CW($" - {Launcher.GameVersion}");
                CW("============");
                if (canContinue) CW("c. Continue");
                CW("n. Start New Game");
                CW("o. Options");
                CW("e. Exit");
                
                if (IsIdCheckerEnabled) CW("i. Id Checker");

                var input = CKL();
                switch (input)
                {
                    case "0":
                    case "1":
                    case "c":
                    case "с":
                        if (!canContinue)
                        {
                            WrongInput();
                            break;
                        }

                        if (!hasCurrentGame && hasSaveFile)
                        {
                            SaveLoad.Load();
                        }

                        GameMenu.Menu(debugVar, Towns.GoldenleafTown00);
                        break;
                    case "2":
                    case "n":
                    case "т":
                        NewGame.Starter();
                        GameMenu.Menu(debugVar, Towns.GoldenleafTown00);
                        break;
                    case "3":
                    case "o":
                    case "щ":
                        Options.Menu();
                        break;
                    case "e":
                    case "у":
                        if (hasCurrentGame)
                        {
                            SaveLoad.SaveSilent();
                        }
                        Environment.Exit(0);
                        break;
                    case "9":
                    case "i":
                    case "ш":
                        if (IsIdCheckerEnabled)
                        {
                            IdChecker.Opener();
                        }
                        else
                        {
                            WrongInput();
                        }
                        break;
                    default:
                        WrongInput();
                        break;
                }
            }
        }
    }
}
