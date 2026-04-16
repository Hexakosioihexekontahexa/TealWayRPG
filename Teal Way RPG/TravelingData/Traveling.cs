using Teal_Way_RPG.BattleData;
using Teal_Way_RPG.GameData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.TravelingData
{
    public static class Traveling
    {
        public static string CurrentLocationShortName = "tuvale";

        public static void EnterWildWoods()
        {
            Clear();
            CW("You step into the Wild Woods...");
            PKC();
            TravelBattle.StartRandomEncounter(CurrentLocationShortName);
        }
    }
}