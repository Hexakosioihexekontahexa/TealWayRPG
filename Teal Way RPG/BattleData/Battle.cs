using System;
using System.Collections.Generic;
using System.Text;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.BattleData
{
    public class Battle
    {
        public void BattleProcessor()
        {
            CW("##########################################################################################################################");
            CW($"S");
            //if ! [[ -z "$numberOfStatusTurns" || "$numberOfStatusTurns" -le 0 ]]; then echo -e "S:$currentPlayerSTR A:$currentPlayerAGI I:N/A    | Your  HP: $currentPlayerHP/$maxPlayerHP | Your DMG: $currentPlayerMINDMG - $currentPlayerMAXDMG | LVL: $playerLevel | EXP: $playerEXP/$nextLvlEXP | G: $playerGold | STAT: $playerStatusColor$playerStatus\E[37;40m ($playerStatusColor$numberOfStatusTurns\E[37;40m turn(s) left)" 
            //else echo -e "S:$currentPlayerSTR A:$currentPlayerAGI I:N/A    | Your  HP: $currentPlayerHP/$maxPlayerHP | Your DMG: $currentPlayerMINDMG - $currentPlayerMAXDMG | G: $playerGold | STAT: $playerStatusColor$playerStatus\E[37;40m | EXP: $playerEXP/$nextLvlEXP | LVL: $playerLevel" 
            //fi
            //    echo "Enemy: ${enemyName[$eid]} | Enemy HP: $currentMonsterHP/${enemyHP[$eid]} | Enemy's DMG: $currentMonsterMINDMG - $currentMonsterMAXDMG"
            //echo "--------------------------------------------------------------------------------------------------------------------------"
            //echo "1. Attack ($currentPlayerMINDMG - $currentPlayerMAXDMG)"
            //echo "2. Cast (NYI)"
            //echo "3. Flee"

            //CW(
            //    "##########################################################################################################################");
        }
    }
}
