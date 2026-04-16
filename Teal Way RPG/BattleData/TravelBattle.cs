using Teal_Way_RPG.GameData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.BattleData
{
    public static class TravelBattle
    {
        public static void StartRandomEncounter(string locationShortName)
        {
            var monster = MonsterProcessor.RandomizeMonster(locationShortName);
            Battle.Run(monster, true);
        }

        public static bool StartSpecialEncounter(string locationShortName)
        {
            var monster = MonsterProcessor.RandomizeMonster(locationShortName);
            return Battle.Run(monster, false);
        }
    }
}