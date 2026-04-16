using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public class Options : Menus
    {
        public static bool IsShopUnlocked = false;
        public delegate void VoidString(bool boolean);
        private static VoidString voidString = OptionStateChecker;

        public static void Menu()
        {
            while (true)
            {
                Clear();
                CW("Options");
                Cw("-----");
                VoidStringConcat("1. Debug mode - [", voidString, Launcher.DebugVar, "]");
                VoidStringConcat("2. Ingame Id checker - [", voidString, MainMenu.IsIdCheckerEnabled, "]");
                VoidStringConcat("3. Shop available - [", voidString, IsShopUnlocked, "]");
                CWL("0. Back");
                var input = CK();
                switch (input)
                {
                    case "0":
                        break;
                    case "1":
                        BoolReverser(ref Launcher.DebugVar);
                        continue;
                    case "2":
                        BoolReverser(ref MainMenu.IsIdCheckerEnabled);
                        continue;
                    case "3":
                        BoolReverser(ref IsShopUnlocked);
                        continue;
                    default:
                        Clear();
                        CW("Wrong input detected! Please try again!");
                        CK();
                        continue;
                }

                break;
            }
        }

        public static void OptionStateChecker(bool option)
        {
            if (option)
            {
                SetColorsTo(ConsoleColor.Green);
                Cw("Enabled");
                SetColorsToDefault();
            }
            else
            {
                SetColorsTo(ConsoleColor.Red);
                Cw("Disabled");
                SetColorsToDefault();
            }
        }

        public static void VoidStringConcat(string substring1, VoidString voidString, bool option, string substring2)
        {
            CWL(substring1);
            voidString(option);
            Cw(substring2);
        }
    }
}
