using System;
using System.Collections.Generic;
using Teal_Way_RPG.GameData.ArtifactData;

namespace Teal_Way_RPG.GameData
{
    public class Artifact
    {
        public int SimpleArtifactId { get; }
        // ## of implementation
        // A code
        // ? rarity code
        // ? impact type
        public string ArtifactId { get; }
        public string ArtifactName { get; }
        public string ArtifactDescription { get; }
        public Rarity ArtifactRarity { get; }
        public Currency ArtifactCurrency { get; }
        public int ArtifactCost { get; }
        public Effect ArtifactEffect { get; }
        public int ArtifactImpactPoints { get; } //depends on ArtifactSpeciality
        public string ArtifactImpactType { get; }
        public string ArtifactPointsType { get; } //used for non-battle-related artifacts
        public string ArtifactSlot { get; }
        public string ArtifactLocation { get; }

        public static string BaseIdFormat = "######";

        public Artifact(int simpleArtifactId, string artifactId, string artifactName, string artifactDescription,
                      Rarity artifactRarity, int artifactCost, Currency artifactCurrency, 
                      string artifactSlot, Effect artifactEffect, int artifactImpactPoints,  
                      string artifactImpactType, string artifactPointsType = "points", string artifactLocation = "everywhere")
        {
            SimpleArtifactId = simpleArtifactId;
            ArtifactId = artifactId;
            ArtifactName = artifactName;
            ArtifactDescription = artifactDescription;
            ArtifactRarity = artifactRarity;
            ArtifactCost = artifactCost;
            ArtifactCurrency = artifactCurrency;
            ArtifactSlot = artifactSlot;
            ArtifactEffect = artifactEffect;
            ArtifactImpactPoints = artifactImpactPoints;
            ArtifactImpactType = artifactImpactType;
            ArtifactPointsType = artifactPointsType;
            ArtifactLocation = artifactLocation;
        }

        public Artifact(string artifactId, string artifactName)
        {
            ArtifactId = artifactId;
            ArtifactName = artifactName;
        }
    }

    public class Artifacts
    {
        private const string AtkAddBonus = "Additional Attack Bonus";
        private const string DefAddBonus = "Additional Defense Bonus";
        private const string StrAddBonus = "Additional Strength Bonus";
        private const string AgiAddBonus = "Additional Agility Bonus";
        private const string HpAddBonus = "Additional Health Bonus";
        private const string Evasion = "Evasion";

        public static Artifact DefaultArtifact00 = new Artifact("000ANN", "Default");

        public static Artifact LeatherGlovesArtifact01 = new Artifact(
            1, 
            "001ACP", 
            "Leather Gloves", 
            "Worn gloves, stained in mud.", 
            Rarities.Common, 
            50, 
            Currencies.Gold,
            "slotHands",
            Effects.AdditionalAttackBonusEffect,
            Effects.SetEffectImpactPoints(AtkAddBonus, 1),
            Effects.SetEffectImpactType(AtkAddBonus, "passive"));

        public static Artifact ClothLeggingsArtifact02 = new Artifact(
            2, 
            "002ACP", 
            "Cloth Leggings", 
            "Leggings made in a hurry from torn cloth.", 
            Rarities.Common, 
            50, 
            Currencies.Gold, 
            "slotLegs",
            Effects.AdditionalDefenseBonusEffect,
            Effects.SetEffectImpactPoints(DefAddBonus, 1),
            Effects.SetEffectImpactType(DefAddBonus, "passive"));

        public static Artifact RustyBracerArtifact03 = new Artifact(
            3,
            "003ACP",
            "Rusty Bracer",
            "There's an abraded coat of arms of an Old House.",
            Rarities.Common,
            120,
            Currencies.Gold,
            "slotAccessory",
            Effects.AdditionalStrengthBonusEffect,
            Effects.SetEffectImpactPoints(StrAddBonus, 1),
            Effects.SetEffectImpactType(StrAddBonus, "passive"));

        public static Artifact TornJacketArtifact04 = new Artifact(
            4,
            "004ACP",
            "Torn Jacket",
            "This one can save your butt from a wolf fangs.",
            Rarities.Common,
            90,
            Currencies.Gold,
            "slotChest",
            Effects.AdditionalDefenseBonusEffect,
            Effects.SetEffectImpactPoints(DefAddBonus, 2),
            Effects.SetEffectImpactType(DefAddBonus, "passive"));

        public static Artifact BluntBladeArtifact05 = new Artifact(
            5,
            "005ACP",
            "Blunt Blade",
            "The only blade in the village.",
            Rarities.Common,
            120,
            Currencies.Gold,
            "slotRightHand",
            Effects.AdditionalAttackBonusEffect,
            Effects.SetEffectImpactPoints(AtkAddBonus, 3),
            Effects.SetEffectImpactType(AtkAddBonus, "passive"));

        public static Artifact CircletOfSpeedArtifact06 = new Artifact(
            6,
            "006ACP",
            "Circlet of Speed",
            "Left by elven archer in gratitude.",
            Rarities.Common,
            110,
            Currencies.Gold,
            "slotAccessory",
            Effects.AdditionalAgilityBonusEffect,
            Effects.SetEffectImpactPoints(AgiAddBonus, 1),
            Effects.SetEffectImpactType(AgiAddBonus, "passive"));

        public static Artifact NecklaceOfMightArtifact07 = new Artifact(
            7,
            "007ACP",
            "Necklace of Might",
            "A small gift from the captain of Grold's army.",
            Rarities.Common,
            150,
            Currencies.Gold,
            "slotAccessory",
            Effects.AdditionalHealthBonusEffect,
            Effects.SetEffectImpactPoints(HpAddBonus, 15),
            Effects.SetEffectImpactType(HpAddBonus, "passive"));

        public static Artifact PowerTreads08 = new Artifact(
            8,
            "008AUP",
            "Power Treads",
            "A comfortable boots from dwarven masters living in Alagari Mountains.",
            Rarities.Uncommon,
            650,
            Currencies.Gold,
            "slotBoots",
            Effects.AdditionalStrengthBonusEffect,
            Effects.SetEffectImpactPoints(StrAddBonus, 6),
            Effects.SetEffectImpactType(StrAddBonus, "passive"));

        public static Artifact GrainOfEvasionArtifact09 = new Artifact(
            9,
            "009ARP",
            "Grain of Evasion",
            "No one knows where from it appeared. There are rumors it can show next enemy attack sometimes.",
            Rarities.Rare,
            1200,
            Currencies.Gold,
            "slotGem",
            Effects.EvasionEffect,
            Effects.SetEffectImpactPoints(Evasion, 5),
            Effects.SetEffectImpactType(Evasion, "passive"),
            "percent");

        public static List<Artifact> ArtifactList = new List<Artifact>();

        public static List<Artifact> PhantasmesList = new List<Artifact>();

        public static List<string> ArtifactIdList = new List<string>();

        public static List<Artifact> ArtifactListBuilder(params Artifact[] artifacts)
        {
            foreach (var artifact in artifacts)
            {
                ArtifactList.Add(artifact);
            }

            return ArtifactList;
        }

        public static List<string> ArtifactIdListBuilder(params Artifact[] artifacts)
        {
            foreach (var artifact in artifacts)
            {
                ArtifactIdList.Add(artifact.ArtifactId);
            }

            return ArtifactIdList;
        }

        public static Artifact GetArtifactById(string artifactId)
        {
            foreach (var artifact in ArtifactList)
            {
                if (artifact.ArtifactId == artifactId)
                    return artifact;
            }

            //throw new Exception($"Unknown ArtifactId:{artifactId} was transferred!");
            //Utils.CW($"Unknown ArtifactId:{artifactId} was passed. Please, try again!");
            return DefaultArtifact00;
        }

        public static List<Artifact> GetArtifactListByRarity(Rarity rarity)
        {
            var artifactList = new List<Artifact>();
            foreach (var artifact in ArtifactList)
            {
                if (artifact.ArtifactRarity == rarity)
                {
                    artifactList.Add(artifact);
                }
            }

            return artifactList;
        }
    }

    public class ArtifactProcessor
    {
        public static void ArtifactInitializer()
        {
            Artifacts.ArtifactListBuilder(
                Artifacts.DefaultArtifact00,
                Artifacts.LeatherGlovesArtifact01,
                Artifacts.ClothLeggingsArtifact02,
                Artifacts.RustyBracerArtifact03,
                Artifacts.TornJacketArtifact04,
                Artifacts.BluntBladeArtifact05,
                Artifacts.CircletOfSpeedArtifact06,
                Artifacts.NecklaceOfMightArtifact07,
                Artifacts.PowerTreads08,
                Artifacts.GrainOfEvasionArtifact09);

            Artifacts.ArtifactIdListBuilder(
                Artifacts.DefaultArtifact00,
                Artifacts.LeatherGlovesArtifact01,
                Artifacts.ClothLeggingsArtifact02,
                Artifacts.RustyBracerArtifact03,
                Artifacts.TornJacketArtifact04,
                Artifacts.BluntBladeArtifact05,
                Artifacts.CircletOfSpeedArtifact06,
                Artifacts.NecklaceOfMightArtifact07,
                Artifacts.PowerTreads08,
                Artifacts.GrainOfEvasionArtifact09);
        }

        public static void PhantasmesRandomizer()
        {
            var phantasmesList = Artifacts.GetArtifactListByRarity(Rarities.Phantasm);
            for (var i = 0; i < 1; i++)
            {
                var j = Utils.GetRandom(phantasmesList.Count);
                Artifacts.PhantasmesList.Add(phantasmesList[j]);
            }
        }
    }

    public enum ArtifactArray
    {
        LeatherGlovesArtifact01,
        ClothLeggingsArtifact02,
        RustyBracerArtifact03,
        TornJacketArtifact04,
        BluntBladeArtifact05,
        CircletOfSpeedArtifact06,
        NecklaceOfMightArtifact07,
        PowerTreads08,
        GrainOfEvasionArtifact09,
    }
}

