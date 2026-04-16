using System.Collections.Generic;
using System.Linq;
using Teal_Way_RPG.GameData;
using Teal_Way_RPG.GeneralData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.InventoryData
{
    public static class Inventory
    {
        private static readonly (string SlotId, string SlotName)[] PlayerSlots =
        [
            ("slotHead", "Head"),
            ("slotChest", "Chest"),
            ("slotArms", "Arms"),
            ("slotLegs", "Legs"),
            ("slotBoots", "Boots"),
            ("slotRightHand", "Right Hand"),
            ("slotLeftHand", "Left Hand"),
            ("slotAccessory", "Accessory"),
            ("slotUtil", "Util"),
            ("slotGem", "Gem")
        ];

        public static List<Artifact> ArtifactItems { get; } = new List<Artifact>();
        public static List<Potion> PotionItems { get; } = new List<Potion>();
        public static List<Artifact> EquippedArtifacts { get; } = new List<Artifact>();

        public static void Reset()
        {
            ArtifactItems.Clear();
            PotionItems.Clear();
            EquippedArtifacts.Clear();
        }

        public static void AddArtifact(Artifact artifact)
        {
            if (artifact == null || artifact.ArtifactId == Teal_Way_RPG.GameData.Artifacts.DefaultArtifact00.ArtifactId)
            {
                return;
            }

            ArtifactItems.Add(artifact);
        }

        public static void AddPotion(Potion potion)
        {
            if (potion == null || potion.PotionId == Teal_Way_RPG.GameData.Potions.DefaultPotion.PotionId)
            {
                return;
            }

            PotionItems.Add(potion);
        }

        public static void RemovePotionAt(int index)
        {
            if (index >= 0 && index < PotionItems.Count)
            {
                PotionItems.RemoveAt(index);
            }
        }

        public static bool ToggleEquip(Artifact artifact, out string message)
        {
            if (artifact == null)
            {
                message = "Unknown artifact.";
                return false;
            }

            if (EquippedArtifacts.Any(a => a.ArtifactId == artifact.ArtifactId))
            {
                EquippedArtifacts.RemoveAll(a => a.ArtifactId == artifact.ArtifactId);
                message = $"{artifact.ArtifactName} has been unequipped.";
                return true;
            }

            var capacity = GetSlotCapacity(artifact.ArtifactSlot);
            var currentlyEquipped = EquippedArtifacts.Count(a => a.ArtifactSlot == artifact.ArtifactSlot);
            if (currentlyEquipped >= capacity)
            {
                message = $"All {artifact.ArtifactSlot} slots are occupied.";
                return false;
            }

            EquippedArtifacts.Add(artifact);
            message = $"{artifact.ArtifactName} has been equipped.";
            return true;
        }

        public static int GetAttackBonus()
        {
            return GetArtifactBonus("Additional Attack Bonus");
        }

        public static int GetDefenseBonus()
        {
            return GetArtifactBonus("Additional Defense Bonus");
        }

        public static int GetStrengthBonus()
        {
            return GetArtifactBonus("Additional Strength Bonus");
        }

        public static int GetAgilityBonus()
        {
            return GetArtifactBonus("Additional Agility Bonus");
        }

        public static int GetHealthBonus()
        {
            return GetArtifactBonus("Additional Health Bonus");
        }

        public static int GetEvasionChance()
        {
            return EquippedArtifacts
                .Where(a => a.ArtifactEffect is { EffectName: "Evasion" })
                .Sum(a => a.ArtifactImpactPoints);
        }

        public static int GetEffectiveStrength()
        {
            return NewGame.GetTotalStrength() + GetStrengthBonus();
        }

        public static int GetEffectiveAgility()
        {
            return NewGame.GetTotalAgility() + GetAgilityBonus();
        }

        public static int GetEffectiveIntelligence()
        {
            return NewGame.GetTotalIntelligence();
        }

        public static int GetEffectiveMaxHealth()
        {
            return GetEffectiveStrength() * 10 + GetHealthBonus();
        }

        public static int GetPlayerMinDamage()
        {
            return (int)NewGame.BasePlayerDmg + GetAttackBonus();
        }

        public static int GetPlayerMaxDamage()
        {
            return GetPlayerMinDamage() + GetEffectiveStrength();
        }

        public static void Menu(bool showTownReturn = true)
        {
            while (true)
            {
                Clear();
                CW($"Your inventory: {ArtifactItems.Count + PotionItems.Count} item(s)");
                if (ArtifactItems.Count + PotionItems.Count > 0) CW("-----");

                var itemIndex = 1;
                var artifactLookup = new Dictionary<int, Artifact>();
                var potionLookup = new Dictionary<int, Potion>();

                foreach (var artifact in ArtifactItems)
                {
                    var equippedMark = EquippedArtifacts.Any(a => a.ArtifactId == artifact.ArtifactId)
                        ? " [E]"
                        : string.Empty;
                    CW($"{itemIndex}. {artifact.ArtifactName}{equippedMark}");
                    artifactLookup[itemIndex] = artifact;
                    itemIndex++;
                }

                foreach (var potion in PotionItems)
                {
                    CW($"{itemIndex}. {potion.PotionName}");
                    potionLookup[itemIndex] = potion;
                    itemIndex++;
                }

                CW("-----");
                CW($"Equipped bonuses: DMG +{GetAttackBonus()}, DEF +{GetDefenseBonus()}, STR +{GetStrengthBonus()}, AGI +{GetAgilityBonus()}, HP +{GetHealthBonus()}, EVS {GetEvasionChance()}%");
                CW("-----");
                foreach (var slotStatus in GetSlotStatusLines())
                {
                    CW(slotStatus);
                }
                CW("-----");
                CW(showTownReturn ? "b. Back to town" : "b. Back");

                var input = CR();
                if (input is "b" or "и")
                {
                    return;
                }

                if (int.TryParse(input, out var selectedIndex))
                {
                    if (artifactLookup.TryGetValue(selectedIndex, out var artifact))
                    {
                        ItemMenu.OpenArtifact(artifact);
                    }
                    else if (potionLookup.TryGetValue(selectedIndex, out var potion))
                    {
                        ItemMenu.OpenPotion(potion);
                    }
                }
            }
        }

        public static void RestoreState(IEnumerable<string> artifactIds, IEnumerable<string> potionIds,
            IEnumerable<string> equippedArtifactIds)
        {
            Reset();

            if (artifactIds != null)
            {
                foreach (var artifactId in artifactIds)
                {
                    var artifact = Artifacts.GetArtifactById(artifactId);
                    if (artifact.ArtifactId != Artifacts.DefaultArtifact00.ArtifactId)
                    {
                        ArtifactItems.Add(artifact);
                    }
                }
            }

            if (potionIds != null)
            {
                foreach (var potionId in potionIds)
                {
                    var potion = Potions.GetPotionById(potionId);
                    if (potion.PotionId != Potions.DefaultPotion.PotionId)
                    {
                        PotionItems.Add(potion);
                    }
                }
            }

            if (equippedArtifactIds != null)
            {
                foreach (var artifactId in equippedArtifactIds)
                {
                    var artifact = ArtifactItems.FirstOrDefault(a => a.ArtifactId == artifactId);
                    if (artifact != null)
                    {
                        EquippedArtifacts.Add(artifact);
                    }
                }
            }
        }

        private static int GetSlotCapacity(string slot)
        {
            switch (slot)
            {
                case "slotAccessory":
                case "slotUtil":
                    return 2;
                default:
                    return 1;
            }
        }

        private static IEnumerable<string> GetSlotStatusLines()
        {
            foreach (var (slotId, slotName) in PlayerSlots)
            {
                var equippedInSlot = EquippedArtifacts.Where(a => a.ArtifactSlot == slotId).ToList();
                var capacity = GetSlotCapacity(slotId);

                if (capacity == 1)
                {
                    var artifactName = equippedInSlot.FirstOrDefault()?.ArtifactName ?? "Empty";
                    yield return $"{slotName}: [{artifactName}]";
                    continue;
                }

                for (var i = 0; i < capacity; i++)
                {
                    var artifactName = i < equippedInSlot.Count ? equippedInSlot[i].ArtifactName : "Empty";
                    yield return $"{slotName} {i + 1}: [{artifactName}]";
                }
            }
        }

        private static int GetArtifactBonus(string effectName)
        {
            return EquippedArtifacts
                .Where(a => a.ArtifactEffect != null && a.ArtifactEffect.EffectName == effectName)
                .Sum(a => a.ArtifactImpactPoints);
        }
    }
}
