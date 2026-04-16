using Teal_Way_RPG.GameData;
using Teal_Way_RPG.GeneralData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.BattleData
{
    public static class Bounty
    {
        public static void Apply(Monster monster)
        {
            NewGame.PlayerGold += monster.GoldBounty;
            NewGame.PlayerExp += monster.ExpBounty;

            CW($"You gain {monster.GoldBounty} gold.");
            CW($"You gain {monster.ExpBounty} exp.");

            Levelup.Process();
        }
    }
}