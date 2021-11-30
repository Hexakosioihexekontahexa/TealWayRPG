using System;
using System.Collections.Generic;

namespace Teal_Way_RPG.GameData
{
    public class Effect
    {
        public int SimpleEffectId { get; }
        // ## of implementation
        // E code
        // ? code of type
        // ? code of application to (e.g. on thyself, enemy, area, group of enemies, etc.)
        public string EffectId { get; }
        public string EffectShortName { get; }
        public string EffectName { get; }
        //public string EffectShortSpeciality { get; }
        public string EffectSpeciality { get; }
        public int EffectImpactPoints { get; set; } //depends on EffectSpeciality
        public string EffectImpactType { get; set; }
        public string EffectType { get; } //used for non-battle-related effects
        public int NumberOfTurns { get; }
        public string EffectStatusText { get; }
        public ConsoleColor ColorFore { get; }
        public ConsoleColor ColorBack { get; }

        public static string BaseIdFormat = "######";

        //Battle-related
        public Effect(int simpleEffectId, string effectId, string effectShortName, string effectName, 
                      string effectSpeciality, int effectImpactPoints, int numberOfTurns, string effectStatusText, 
                      ConsoleColor colorFore = ConsoleColor.Gray, ConsoleColor colorBack = ConsoleColor.Black)
        {
            EffectId = effectId;
            SimpleEffectId = simpleEffectId;
            EffectShortName = effectShortName;
            EffectName = effectName;
            //EffectShortSpeciality = effectShortSpeciality;
            EffectSpeciality = effectSpeciality;
            EffectImpactPoints = effectImpactPoints;
            NumberOfTurns = numberOfTurns;
            EffectStatusText = effectStatusText;
            ColorFore = colorFore;
            ColorBack = colorBack;
        }

        //Town-related
        public Effect(int simpleEffectId, string effectId, string effectShortName, string effectName,
            string effectSpeciality, int effectImpactPoints, string effectType, string effectStatusText,
            ConsoleColor colorFore = ConsoleColor.Gray, ConsoleColor colorBack = ConsoleColor.Black)
        {
            SimpleEffectId = simpleEffectId;
            EffectId = effectId;
            EffectShortName = effectShortName;
            EffectName = effectName;
            //EffectShortSpeciality = effectShortSpeciality;
            EffectSpeciality = effectSpeciality;
            EffectImpactPoints = effectImpactPoints;
            EffectType = effectType;
            EffectStatusText = effectStatusText;
            ColorFore = colorFore;
            ColorBack = colorBack;
        }

        //Artifact-related
        public Effect(int simpleEffectId, string effectId, string effectShortName, string effectName,
            string effectSpeciality, ConsoleColor colorFore = ConsoleColor.Gray, ConsoleColor colorBack = ConsoleColor.Black)
        {
            SimpleEffectId = simpleEffectId;
            EffectId = effectId;
            EffectShortName = effectShortName;
            EffectName = effectName;
            EffectSpeciality = effectSpeciality;
            EffectImpactType = null;
            EffectImpactPoints = 0;
            ColorFore = colorFore;
            ColorBack = colorBack;
        }

        public Effect(string effectId, string effectName)
        {
            EffectId = effectId;
            EffectName = effectName;
        }
    }

    public class Effects
    {
        public static Effect DefaultEffect = new Effect("000ENN", "Default");

        #region Battle-related effectsList
        public static Effect VenomStrikeEffect = new Effect(1, "001EDU", "venom",
            "Venom Strike",
            "Damage over time", 
            2,
            2,
            "Life begins to fade slowly after the last hit...",
            ConsoleColor.Green);

        public static Effect RuptureEffect = new Effect(2, "002EDU", "bleed",
            "Rupture",
            "Damage over time", 
            1,
            3,
            "Life begins to fade slowly after the last hit...",
            ConsoleColor.DarkRed);
        #endregion

        #region Artifact-related effectList
        public static Effect AdditionalAttackBonusEffect =
            new Effect(1, "004EAS", "atk_add_bonus", "Additional Attack Bonus", "Attack Bonus");
        public static Effect AdditionalDefenseBonusEffect =
            new Effect(2, "005EAS", "def_add_bonus", "Additional Defense Bonus", "Defense Bonus");
        public static Effect AdditionalStrengthBonusEffect =
            new Effect(3, "006EAS", "str_add_bonus", "Additional Strength Bonus", "Strength Bonus");
        public static Effect AdditionalAgilityBonusEffect =
            new Effect(4, "007EAS", "agi_add_bonus", "Additional Agility Bonus", "Agility Bonus");
        public static Effect AdditionalHealthBonusEffect =
            new Effect(5, "008EAS", "hp_add_bonus", "Additional Health Bonus", "Health Bonus");
        public static Effect HealEffect =
            new Effect(6, "009EAS", "heal", "Heal", "Heal");
        public static Effect EvasionEffect =
            new Effect(7, "010EAS", "evasion", "Evasion", "Evasion");
        #endregion

        #region Other effectsList
        public static Effect Discount10Effect = new Effect(
            1, 
            "003ETP", 
            "discount10",
            "10% Discount",
            "Discount", 
            10, 
            "townRelated",
            "10% discount on shop prices!");
        #endregion

        public static List<Effect> EffectList = new List<Effect>();

        public static List<string> EffectIdList = new List<string>();

        public static List<Effect> EffectListBuilder(params Effect[] effects)
        {
            foreach (var effect in effects)
            {
                EffectList.Add(effect);
            }

            return EffectList;
        }

        public static List<string> EffectIdListBuilder(params Effect[] effects)
        {
            foreach (var effect in effects)
            {
                EffectIdList.Add(effect.EffectId);
            }

            return EffectIdList;
        }

        public static Effect GetEffectById(string effectId)
        {
            foreach (var effect in EffectList)
            {
                if (effect.EffectId == effectId)
                    return effect;
            }

            //throw new Exception($"Unknown EffectId:{effectId} was transferred!");
            return DefaultEffect;
        }

        public static Effect GetEffectByName(string effectName)
        {
            foreach (var effect in EffectList)
            {
                if (effect.EffectName == effectName)
                    return effect;
            }

            //throw new Exception($"Unknown EffectName:{effectName} was transferred!");
            return DefaultEffect;
        }

        public static Effect GetEffectByShortName(string effectShortName)
        {
            foreach (var effect in EffectList)
            {
                if (effect.EffectShortName == effectShortName)
                {
                    return effect;
                }
            }

            //throw new Exception($"Unknown Effect Short Name:{effectShortName} was transferred!");
            return DefaultEffect;
        }

        public static int SetEffectImpactPoints(string name, int points)
        {
            var effect = GetEffectByName(name);
            effect.EffectImpactPoints = points;
            return effect.EffectImpactPoints;
        }

        public static string SetEffectImpactType(string name, string type)
        {
            var effect = GetEffectByName(name);
            effect.EffectImpactType = type;
            return effect.EffectImpactType;
        }
    }

    public class EffectProcessor
    {
        public static void ProcessEffectText(string effectShortName)
        {
            var effect = Effects.GetEffectByShortName(effectShortName);
            switch (effectShortName)
            {
                case "bleed":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Utils.Cw($"{effect.EffectName}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "venom":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Utils.Cw($"{effect.EffectName}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "discount10":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Utils.Cw($"{effect.EffectName}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }

        public static void EffectInitializer()
        {
            Effects.EffectListBuilder(
                Effects.DefaultEffect,
                Effects.VenomStrikeEffect,
                Effects.RuptureEffect,
                Effects.Discount10Effect,
                Effects.AdditionalAttackBonusEffect,
                Effects.AdditionalDefenseBonusEffect,
                Effects.AdditionalStrengthBonusEffect,
                Effects.AdditionalAgilityBonusEffect,
                Effects.AdditionalHealthBonusEffect,
                Effects.HealEffect,
                Effects.EvasionEffect);

            Effects.EffectIdListBuilder(
                Effects.DefaultEffect,
                Effects.VenomStrikeEffect,
                Effects.RuptureEffect,
                Effects.Discount10Effect,
                Effects.AdditionalAttackBonusEffect,
                Effects.AdditionalDefenseBonusEffect,
                Effects.AdditionalStrengthBonusEffect,
                Effects.AdditionalAgilityBonusEffect,
                Effects.AdditionalHealthBonusEffect,
                Effects.HealEffect,
                Effects.EvasionEffect);
        }
    }
}


