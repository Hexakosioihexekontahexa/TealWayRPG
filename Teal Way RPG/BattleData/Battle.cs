using Teal_Way_RPG.GameData;
using Teal_Way_RPG.GeneralData;
using Teal_Way_RPG.InventoryData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.BattleData
{
    public static class Battle
    {
        public static bool ExitToMainMenuRequested { get; private set; }

        public static void RequestExitToMainMenu()
        {
            ExitToMainMenuRequested = true;
        }

        public static bool Run(Monster monster, bool allowFlee = true)
        {
            ExitToMainMenuRequested = false;
            var playerMaxHp = Inventory.GetEffectiveMaxHealth();
            var playerHp = playerMaxHp;
            var playerMinDamage = Inventory.GetPlayerMinDamage();
            var playerMaxDamage = Inventory.GetPlayerMaxDamage();
            var enemyMinDamage = monster.MonsterMinDmg - Inventory.GetEffectiveAgility() - Inventory.GetDefenseBonus();
            var enemyMaxDamage = monster.MonsterMaxDmg - Inventory.GetEffectiveAgility() - Inventory.GetDefenseBonus();

            if (enemyMinDamage < 0) enemyMinDamage = 0;
            if (enemyMaxDamage < enemyMinDamage) enemyMaxDamage = enemyMinDamage;

            var activeStatus = "N/A";
            var statusTurns = 0;
            var effect = monster.MonsterEffect;

            while (playerHp > 0 && monster.MonsterHp > 0)
            {
                var enemyHp = monster.MonsterHp;
                while (enemyHp > 0 && playerHp > 0)
                {
                    Clear();
                    DrawBattleUi(monster, playerHp, playerMaxHp, enemyHp, playerMinDamage, playerMaxDamage, enemyMinDamage, enemyMaxDamage, activeStatus, statusTurns, effect, allowFlee);

                    switch (CK())
                    {
                        case "0":
                            ExitToMainMenuRequested = true;
                            return false;
                        case "1":
                            var playerDamage = GetRandom(playerMinDamage, playerMaxDamage + 1);
                            enemyHp -= playerDamage;
                            CW($"\nA powerful swing! You inflicted {playerDamage} point(s) of damage.");
                            Sleep(1);
                            break;
                        case "2":
                            CW("\nMagic is not implemented yet.");
                            Sleep(1);
                            continue;
                        case "3":
                            return RunQuickBattle(monster, playerHp, playerMaxHp, playerMinDamage, playerMaxDamage,
                                enemyHp, enemyMinDamage, enemyMaxDamage, activeStatus, statusTurns, effect);
                        case "4":
                            if (!allowFlee)
                            {
                                CW("\nThere is no escape from this fight.");
                                Sleep(1);
                                continue;
                            }

                            var fleeChance = GetRandom(1, 11);
                            if (fleeChance >= 8)
                            {
                                CW("\nYou escaped successfully.");
                                Sleep(1);
                                SaveLoad.SaveSilent();
                                return false;
                            }

                            CW("\nYou failed to flee!");
                            Sleep(1);
                            break;
                        default:
                            continue;
                    }

                    if (enemyHp <= 0)
                    {
                        Clear();
                        CW("Another corpse on the battlefield.");
                        Bounty.Apply(monster);
                        PKC();
                        SaveLoad.SaveSilent();
                        return true;
                    }

                    if (Inventory.GetEvasionChance() > 0)
                    {
                        var evasionRoll = GetRandom(1, 101);
                        if (evasionRoll <= Inventory.GetEvasionChance())
                        {
                            CW("You evade the incoming attack.");
                            Sleep(1);
                        }
                        else
                        {
                            playerHp = MonsterTurn(playerHp, enemyMinDamage, enemyMaxDamage);
                        }
                    }
                    else
                    {
                        playerHp = MonsterTurn(playerHp, enemyMinDamage, enemyMaxDamage);
                    }

                    if (playerHp > 0 && effect != null && effect.EffectId != Effects.DefaultEffect.EffectId)
                    {
                        activeStatus = effect.EffectName;
                        statusTurns = effect.NumberOfTurns;
                    }

                    if (playerHp > 0 && statusTurns > 0 && effect != null && effect.EffectSpeciality == "Damage over time")
                    {
                        playerHp -= effect.EffectImpactPoints;
                        CW(effect.EffectStatusText);
                        CW($"You've suffered {effect.EffectImpactPoints} point(s) of damage.");
                        statusTurns--;
                        Sleep(1);
                    }

                    if (statusTurns <= 0)
                    {
                        activeStatus = "N/A";
                    }

                    if (playerHp <= 0)
                    {
                        GeneralData.GameOver.Process();
                        return false;
                    }
                }
            }

            return false;
        }

        private static bool RunQuickBattle(Monster monster, int playerHp, int playerMaxHp, int playerMinDamage,
            int playerMaxDamage, int enemyHp, int enemyMinDamage, int enemyMaxDamage, string activeStatus,
            int statusTurns, Effect effect)
        {
            var battleLog = new System.Collections.Generic.List<string>
            {
                $"Quick battle started against {monster.MonsterName}.",
                $"Your HP: {playerHp}/{playerMaxHp} | Enemy HP: {enemyHp}/{monster.MonsterHp}"
            };

            while (playerHp > 0 && enemyHp > 0)
            {
                var playerDamage = GetRandom(playerMinDamage, playerMaxDamage + 1);
                enemyHp -= playerDamage;
                if (enemyHp < 0)
                {
                    enemyHp = 0;
                }

                battleLog.Add($"You strike for {playerDamage} damage. Enemy HP: {enemyHp}/{monster.MonsterHp}");

                if (enemyHp <= 0)
                {
                    break;
                }

                if (Inventory.GetEvasionChance() > 0)
                {
                    var evasionRoll = GetRandom(1, 101);
                    if (evasionRoll <= Inventory.GetEvasionChance())
                    {
                        battleLog.Add("You evade the incoming attack.");
                    }
                    else
                    {
                        var enemyDamage = GetRandom(enemyMinDamage, enemyMaxDamage + 1);
                        playerHp -= enemyDamage;
                        battleLog.Add($"Enemy hits for {enemyDamage} damage. Your HP: {playerHp}/{playerMaxHp}");
                    }
                }
                else
                {
                    var enemyDamage = GetRandom(enemyMinDamage, enemyMaxDamage + 1);
                    playerHp -= enemyDamage;
                    battleLog.Add($"Enemy hits for {enemyDamage} damage. Your HP: {playerHp}/{playerMaxHp}");
                }

                if (playerHp > 0 && effect != null && effect.EffectId != Effects.DefaultEffect.EffectId)
                {
                    activeStatus = effect.EffectName;
                    statusTurns = effect.NumberOfTurns;
                    battleLog.Add($"Status applied: {activeStatus} ({statusTurns} turn(s)).");
                }

                if (playerHp > 0 && statusTurns > 0 && effect != null && effect.EffectSpeciality == "Damage over time")
                {
                    playerHp -= effect.EffectImpactPoints;
                    statusTurns--;
                    battleLog.Add($"{effect.EffectStatusText} You suffer {effect.EffectImpactPoints} extra damage. Your HP: {playerHp}/{playerMaxHp}");
                }

                if (statusTurns <= 0)
                {
                    activeStatus = "N/A";
                }
            }

            Clear();
            foreach (var line in battleLog)
            {
                CW(line);
            }

            if (enemyHp <= 0)
            {
                CW("Another corpse on the battlefield.");
                Bounty.Apply(monster);
                PKC();
                SaveLoad.SaveSilent();
                return true;
            }

            CW("You were defeated.");
            PKC();
            GeneralData.GameOver.Process(false);
            return false;
        }

        private static int MonsterTurn(int playerHp, int enemyMinDamage, int enemyMaxDamage)
        {
            var enemyDamage = GetRandom(enemyMinDamage, enemyMaxDamage + 1);
            playerHp -= enemyDamage;
            CW($"A crawling fear rises! You've suffered {enemyDamage} point(s) of damage.");
            Sleep(1);
            return playerHp;
        }

        private static void DrawBattleUi(Monster monster, int playerHp, int playerMaxHp, int enemyHp, int playerMinDamage, int playerMaxDamage, int enemyMinDamage, int enemyMaxDamage, string activeStatus, int statusTurns, Effect effect, bool allowFlee)
        {
            CW("########################################################################################################################");
            Cw($"S:{Inventory.GetEffectiveStrength()} A:{Inventory.GetEffectiveAgility()} I:{Inventory.GetEffectiveIntelligence()} | Your HP: {playerHp}/{playerMaxHp} | Your DMG: {playerMinDamage}-{playerMaxDamage} | LVL: {GeneralData.NewGame.PlayerLevel} | EXP: {GeneralData.NewGame.PlayerExp}/{GeneralData.NewGame.NextLvlExp} | G: {GeneralData.NewGame.PlayerGold} | STAT: ");
            if (statusTurns > 0)
            {
                SetColorsTo(effect?.ColorFore ?? DefaultForeColor, effect?.ColorBack ?? DefaultBackColor);
                Cw(activeStatus);
                SetColorsToDefault();
                Cw(" (");
                SetColorsTo(effect?.ColorFore ?? DefaultForeColor, effect?.ColorBack ?? DefaultBackColor);
                Cw($"{statusTurns}");
                SetColorsToDefault();
                Cw(" turn(s) left)");
                CW("");
            }
            else
            {
                CW(activeStatus);
            }
            CW($"Enemy: {monster.MonsterName} | Enemy HP: {enemyHp}/{monster.MonsterHp} | Enemy DMG: {enemyMinDamage}-{enemyMaxDamage}");
            CW("--------------------------------------------------------------------------------------------------------------------------");
            CW($"1. Attack ({playerMinDamage}-{playerMaxDamage})");
            CW("2. Cast (NYI)");
            CW("3. Quick Battle");
            if (allowFlee)
            {
                CW("4. Flee");
            }
            CW("########################################################################################################################");
        }
    }
}
