using System.Linq;
using Teal_Way_RPG.GameData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.InventoryData
{
    public static class ItemMenu
    {
        public static void OpenArtifact(Artifact artifact)
        {
            while (true)
            {
                Clear();
                var isEquipped = Inventory.EquippedArtifacts.Any(a => a.ArtifactId == artifact.ArtifactId);
                CW($"{artifact.ArtifactName}");
                CW("-----");
                CW($"Description: {artifact.ArtifactDescription}");
                CW($"Rarity: {artifact.ArtifactRarity.RarityName}");
                CW($"Cost: {artifact.ArtifactCost} {artifact.ArtifactCurrency.CurrencyName}");
                CW($"Effect: {artifact.ArtifactEffect.EffectName} +{artifact.ArtifactImpactPoints} {artifact.ArtifactPointsType}");
                CW($"Slot: {artifact.ArtifactSlot}");
                CW($"State: {(isEquipped ? "Equipped" : "In backpack")}");
                CW("-----");
                CW("e. Equip / Unequip");
                CW("b. Back");

                switch (CKL())
                {
                    case "e":
                    case "у":
                        Inventory.ToggleEquip(artifact, out var message);
                        Clear();
                        CW(message);
                        PKC();
                        break;
                    case "b":
                    case "и":
                        return;
                }
            }
        }

        public static void OpenPotion(Potion potion)
        {
            while (true)
            {
                Clear();
                CW($"{potion.PotionName}");
                CW("-----");
                CW($"Description: {potion.PotionDescription}");
                CW($"Cost: {potion.PotionCost} {potion.PotionCurrency.CurrencyName}");
                CW($"Effect: {potion.PotionEffect.EffectName} +{potion.PotionImpactPoints} {potion.PotionPointsType}");
                CW("-----");
                CW("u. Use");
                CW("b. Back");

                switch (CKL())
                {
                    case "u":
                    case "г":
                        Clear();
                        CW($"{potion.PotionName} can be consumed in later battle iterations. For now it restores nothing outside battle and is removed from inventory.");
                        var index = Inventory.PotionItems.FindIndex(p => p.PotionId == potion.PotionId);
                        Inventory.RemovePotionAt(index);
                        PKC();
                        return;
                    case "b":
                    case "и":
                        return;
                }
            }
        }
    }
}
