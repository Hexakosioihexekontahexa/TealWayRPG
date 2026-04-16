using System;
using System.IO;
using System.Text.Json;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public class Options : Menus
    {
        public static bool IsShopUnlocked = false;
        public static bool DeleteSaveOnDeath = true;

        public delegate void VoidString(bool boolean);
        private static readonly VoidString VoidStringHandler = OptionStateChecker;
        private static readonly string OptionsPath = Path.Combine(AppContext.BaseDirectory, "options.json");

        public static void Menu()
        {
            while (true)
            {
                Clear();
                CW("Options");
                Cw("-----");
                VoidStringConcat("1. Debug mode - [", VoidStringHandler, Launcher.DebugVar, "]");
                VoidStringConcat("2. Ingame Id checker - [", VoidStringHandler, MainMenu.IsIdCheckerEnabled, "]");
                VoidStringConcat("3. Shop available - [", VoidStringHandler, IsShopUnlocked, "]");
                VoidStringConcat("4. Delete save on death - [", VoidStringHandler, DeleteSaveOnDeath, "]");
                CWL("0. Back");
                var input = CK();
                switch (input)
                {
                    case "0":
                        break;
                    case "1":
                        BoolReverser(ref Launcher.DebugVar);
                        SaveOptions();
                        continue;
                    case "2":
                        BoolReverser(ref MainMenu.IsIdCheckerEnabled);
                        SaveOptions();
                        continue;
                    case "3":
                        BoolReverser(ref IsShopUnlocked);
                        SaveOptions();
                        continue;
                    case "4":
                        BoolReverser(ref DeleteSaveOnDeath);
                        SaveOptions();
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

        public static void LoadOptions()
        {
            if (!File.Exists(OptionsPath))
            {
                return;
            }

            var raw = File.ReadAllText(OptionsPath);
            var data = JsonSerializer.Deserialize<OptionsModel>(raw);
            if (data == null)
            {
                return;
            }

            Launcher.DebugVar = data.DebugVar;
            MainMenu.IsIdCheckerEnabled = data.IsIdCheckerEnabled;
            IsShopUnlocked = data.IsShopUnlocked;
            DeleteSaveOnDeath = data.DeleteSaveOnDeath;
        }

        public static void SaveOptions()
        {
            var data = new OptionsModel
            {
                DebugVar = Launcher.DebugVar,
                IsIdCheckerEnabled = MainMenu.IsIdCheckerEnabled,
                IsShopUnlocked = IsShopUnlocked,
                DeleteSaveOnDeath = DeleteSaveOnDeath
            };

            File.WriteAllText(OptionsPath, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
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

        private class OptionsModel
        {
            public bool DebugVar { get; set; }
            public bool IsIdCheckerEnabled { get; set; }
            public bool IsShopUnlocked { get; set; }
            public bool DeleteSaveOnDeath { get; set; }
        }
    }
}
