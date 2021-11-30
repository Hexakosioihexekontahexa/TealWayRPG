using System;
using System.Collections.Generic;
using System.Text;
using Teal_Way_RPG.GameData.ArtifactData;

namespace Teal_Way_RPG.GameData
{
    public class Potion
    {
        public int SimplePotionId { get; }
        // ## of implementation
        // P code
        // ? rarity code
        public string PotionId { get; }
        public string PotionName { get; }
        public string PotionDescription { get; }
        public Rarity PotionRarity { get; }
        public Currency PotionCurrency { get; }
        public int PotionCost { get; }
        public Effect PotionEffect { get; }
        public int PotionImpactPoints { get; } //depends on PotionSpeciality
        public string PotionImpactType { get; }
        public string PotionPointsType { get; } //used for non-battle-related potions
        public string PotionSlot { get; }
        public string PotionLocation { get; }

        public static string BaseIdFormat = "#####";

        public Potion(int simplePotionId, string potionId, string potionName, string potionDescription,
            Rarity potionRarity, int potionCost, Currency potionCurrency,
            Effect potionEffect, string potionSlot, int potionImpactPoints,
            string potionImpactType, string potionPointsType = "points", string potionLocation = "everywhere")
        {
            SimplePotionId = simplePotionId;
            PotionId = potionId;
            PotionName = potionName;
            PotionDescription = potionDescription;
            PotionRarity = potionRarity;
            PotionCost = potionCost;
            PotionCurrency = potionCurrency;
            PotionSlot = potionSlot;
            PotionEffect = potionEffect;
            PotionImpactPoints = potionImpactPoints;
            PotionImpactType = potionImpactType;
            PotionPointsType = potionPointsType;
            PotionLocation = potionLocation;
        }

        public Potion(string potionId, string potionName)
        {
            PotionId = potionId;
            PotionName = potionName;
        }
    }

    public class Potions
    {
        private const string heal = "Heal";

        public static Potion DefaultPotion = new Potion("000PN", "Default");

        public static Potion FlaskOfVitalityPotion01 = new Potion(
            1,
            "001PC",
            "Flask of Vitality",
            "Made by those druids that live near forest.",
            Rarities.Common,
            30,
            Currencies.Gold,
            Effects.HealEffect,
            "slotUtil",
            Effects.SetEffectImpactPoints(heal, 10),
            Effects.SetEffectImpactType(heal, "active"));

        public static List<Potion> PotionList = new List<Potion>();

        public static List<string> PotionIdList = new List<string>();

        public static List<Potion> PotionListBuilder(params Potion[] potions)
        {
            foreach (var potion in potions)
            {
                PotionList.Add(potion);
            }

            return PotionList;
        }

        public static List<string> PotionIdListBuilder(params Potion[] potions)
        {
            foreach (var potion in potions)
            {
                PotionIdList.Add(potion.PotionId);
            }

            return PotionIdList;
        }

        public static Potion GetPotionById(string potionId)
        {
            foreach (var potion in PotionList)
            {
                if (potion.PotionId == potionId)
                    return potion;
            }

            //throw new Exception($"Unknown PotionId:{potionId} was transferred!");
            return DefaultPotion;
        }
    }

    public class PotionProcessor
    {
        public static void PotionInitializer()
        {
            Potions.PotionListBuilder(
                Potions.DefaultPotion,
                Potions.FlaskOfVitalityPotion01);

            Potions.PotionIdListBuilder(
                Potions.DefaultPotion,
                Potions.FlaskOfVitalityPotion01);
        }

    }
}
