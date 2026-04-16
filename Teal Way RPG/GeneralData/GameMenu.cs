using System.Collections.Generic;
using Teal_Way_RPG.BattleData;
using Teal_Way_RPG.GameData;
using Teal_Way_RPG.GameData.TownData;
using Teal_Way_RPG.InventoryData;
using Teal_Way_RPG.TravelingData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public static class GameMenu
    {
        public static void Menu(bool debugVar)
        {
            Menu(debugVar, Towns.GoldenleafTown00);
        }

        public static void Menu(bool debugVar, Town town)
        {
            while (true)
            {
                Clear();
                if (debugVar)
                {
                    Menus.DebugMenu(new Dictionary<string, object>
                    {
                        { "Game version", Launcher.GameVersion },
                        { "Town", town.TownName },
                        { "Level", NewGame.PlayerLevel },
                        { "Gold", NewGame.PlayerGold },
                        { "Stats", $"STR {Inventory.GetEffectiveStrength()} " +
                                   $"/ AGI {Inventory.GetEffectiveAgility()} " +
                                   $"/ INT {Inventory.GetEffectiveIntelligence()}" 
                        }
                    });
                }
                CW($"Version: {Launcher.GameVersion}");
                CW("-----");
                CW($"Welcome to {town.TownName}!");
                CW("1. Go to Wild Woods");
                CW("2. Visit Shop");
                CW("3. Check inventory");
                CW("4. Save game");
                CW("5. Load game");
                CW("6. Exit to main menu");

                switch (CK())
                {
                    case "1":
                        Traveling.EnterWildWoods();
                        if (Battle.ExitToMainMenuRequested)
                        {
                            return;
                        }
                        break;
                    case "2":
                        Shops.OpenBaseShop(town);
                        if (Battle.ExitToMainMenuRequested)
                        {
                            return;
                        }
                        break;
                    case "3":
                        Inventory.Menu();
                        break;
                    case "4":
                        SaveLoad.Save();
                        break;
                    case "5":
                        SaveLoad.Load();
                        break;
                    case "6":
                        return;
                }
            }
        }

        public static void Menu(bool debugVar, params string[] specs)
        {
            Menu(debugVar);
        }
    }
}
