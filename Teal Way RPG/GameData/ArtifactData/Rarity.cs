using System;
using System.Collections.Generic;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GameData.ArtifactData
{
    public class Rarity
    {
        public int SimpleRarityId { get; }
        // ## of implementation
        // R code
        // rarity type code
        public string RarityId { get; }
        public string RarityName { get; }
        public int RarityLevel { get; }
        public ConsoleColor ColorFore { get; }
        public ConsoleColor ColorBack { get; }


        public Rarity(int simpleRarityId, string rarityId, string rarityName, int rarityLevel, ConsoleColor colorFore, ConsoleColor colorBack = ConsoleColor.Black)
        {
            SimpleRarityId = simpleRarityId;
            RarityId = rarityId;
            RarityName = rarityName;
            RarityLevel = rarityLevel;
            ColorFore = colorFore;
            ColorBack = colorBack;
        }
    }

    public class Rarities
    {
        public static Rarity DefaultRarity = new Rarity(0, "00RD", "Default", 0, ConsoleColor.White);
        public static Rarity Common = new Rarity(1, "01RC", "Common", 0, ConsoleColor.White);
        public static Rarity Uncommon = new Rarity(2, "02RU", "Uncommon", 1, ConsoleColor.Cyan);
        public static Rarity Rare = new Rarity(3, "03RR", "Rare", 2, ConsoleColor.Blue);
        public static Rarity Infused = new Rarity(4, "04RI", "Infused", 2, ConsoleColor.DarkBlue); // infused rare
        public static Rarity Transcendent = new Rarity(5, "05RT", "Transcendent", 3, ConsoleColor.DarkCyan); // same as Epic, will be used in addons and from the other worlds/dimensions except for Underworld - those are Hellish rarity items
        public static Rarity Epic = new Rarity(6, "06RE", "Epic", 3, ConsoleColor.DarkMagenta); // has more than just 1 ability
        public static Rarity Legendary = new Rarity(7, "07RL", "Legendary", 4, ConsoleColor.Yellow);
        public static Rarity Mythical = new Rarity(8, "08RM", "Mythical", 5, ConsoleColor.Yellow, ConsoleColor.DarkMagenta);
        public static Rarity Hellish = new Rarity(9, "09RH", "Hellish", 5, ConsoleColor.DarkRed);
        public static Rarity Arcana = new Rarity(10, "10RA", "Arcana", 6, ConsoleColor.Green);
        public static Rarity Phantasm = new Rarity(11, "11RP", "Phantasm", 7, ConsoleColor.Black, ConsoleColor.White);

        public static List<Rarity> RarityList = new List<Rarity>();

        public static List<Rarity> RarityListBuilder(params Rarity[] rarities)
        {
            foreach (var rarity in rarities)
            {
                RarityList.Add(rarity);
            }

            return RarityList;
        }

        public static Rarity GetRarityById(string rarityId)
        {
            foreach (var rarity in RarityList)
            {
                if (rarity.RarityId == rarityId)
                    return rarity;
            }

            //throw new Exception($"Unknown RarityId:{rarityId} was transferred!");
            return DefaultRarity;
        }

        public static Rarity GetRarityByName(string rarityName)
        {
            foreach (var rarity in RarityList)
            {
                if (rarity.RarityName == rarityName)
                    return rarity;
            }

            //throw new Exception($"Unknown RarityId:{rarityId} was transferred!");
            return DefaultRarity;
        }
    }

    public class RarityProcessor
    {
        public static void ProcessRarityText(string rarityName)
        {
            var rarity = Rarities.GetRarityByName(rarityName);
            //Cw(" ");
            SetColorsTo(rarity.ColorFore, rarity.ColorBack);
            Cw($"{rarityName}");
            SetColorsToDefault();
            //Cw(" ");
        }

        public static void RarityInitializer()
        {
            Rarities.RarityListBuilder(
                Rarities.Common,
                Rarities.Uncommon,
                Rarities.Rare,
                Rarities.Infused,
                Rarities.Epic,
                Rarities.Transcendent,
                Rarities.Legendary,
                Rarities.Mythical,
                Rarities.Hellish,
                Rarities.Arcana,
                Rarities.Phantasm);
        }
    }
}


