using Teal_Way_RPG.GeneralData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.BattleData
{
    public static class Levelup
    {
        public static void Process()
        {
            while (NewGame.PlayerExp >= NewGame.NextLvlExp)
            {
                NewGame.PlayerExp -= NewGame.NextLvlExp;
                NewGame.PlayerLevel++;
                NewGame.NextLvlExp += 10 + (NewGame.PlayerLevel * 10);

                var points = 3;
                while (points > 0)
                {
                    Clear();
                    CW($"Level up! You are now level {NewGame.PlayerLevel}.");
                    CW("You've learned something new...");
                    CW($"Choose your way ({points} skill point(s) left):");
                    CW($"1. Be strong (STR) - {NewGame.GetTotalStrength()}");
                    CW($"2. Be quick (AGI) - {NewGame.GetTotalAgility()}");
                    CW($"3. Be wise (INT) - {NewGame.GetTotalIntelligence()}");

                    switch (CK())
                    {
                        case "1":
                            NewGame.CurrentPlayerStr++;
                            points--;
                            break;
                        case "2":
                            NewGame.CurrentPlayerAgi++;
                            points--;
                            break;
                        case "3":
                            NewGame.CurrentPlayerInt++;
                            points--;
                            break;
                    }
                }
            }
        }
    }
}