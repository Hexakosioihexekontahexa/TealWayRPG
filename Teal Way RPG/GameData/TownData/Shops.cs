using System.Collections.Generic;
using Teal_Way_RPG.BattleData;
using Teal_Way_RPG.GeneralData;
using Teal_Way_RPG.InventoryData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GameData.TownData
{
    public static class Shops
    {
        public static void OpenBaseShop(Town town)
        {
            while (true)
            {
                if (Battle.ExitToMainMenuRequested)
                {
                    return;
                }

                Clear();
                CW($"Welcome to the {town.TownName} shop.");
                CW($"Gold: {NewGame.PlayerGold}");
                CW("-----");

                var shopEntries = new Dictionary<int, object>();
                var index = 1;

                foreach (var artifact in Artifacts.ArtifactList)
                {
                    if (artifact.ArtifactId == Artifacts.DefaultArtifact00.ArtifactId)
                    {
                        continue;
                    }

                    CW($"{index}. {artifact.ArtifactName} - {artifact.ArtifactCost} {artifact.ArtifactCurrency.CurrencyName}");
                    shopEntries[index] = artifact;
                    index++;
                }

                foreach (var potion in Potions.PotionList)
                {
                    if (potion.PotionId == Potions.DefaultPotion.PotionId)
                    {
                        continue;
                    }

                    CW($"{index}. {potion.PotionName} - {potion.PotionCost} {potion.PotionCurrency.CurrencyName}");
                    shopEntries[index] = potion;
                    index++;
                }

                CW("b. Back to town");
                var input = CKL();
                if (input is "b" or "и")
                {
                    return;
                }

                if (!int.TryParse(input, out var selected) || !shopEntries.ContainsKey(selected))
                {
                    continue;
                }

                if (shopEntries[selected] is Artifact artifactChoice)
                {
                    BuyArtifact(artifactChoice);
                    if (Battle.ExitToMainMenuRequested)
                    {
                        return;
                    }
                }
                else if (shopEntries[selected] is Potion potionChoice)
                {
                    BuyPotion(potionChoice);
                    if (Battle.ExitToMainMenuRequested)
                    {
                        return;
                    }
                }
            }
        }

        private static void BuyArtifact(Artifact artifact)
        {
            Clear();
            CW($"You are willing to buy: {artifact.ArtifactName} - {artifact.ArtifactCost} {artifact.ArtifactCurrency.CurrencyName}");
            CW($"Item description: {artifact.ArtifactDescription} ({artifact.ArtifactEffect.EffectName} +{artifact.ArtifactImpactPoints})");
            CW("Are you sure you want to buy this item? [y/n]");
            switch (CKL())
            {
                case "y":
                case "н":
                    if (NewGame.PlayerGold < artifact.ArtifactCost)
                    {
                        Clear();
                        CW("You have not enough money, get lost!");
                        PKC();
                        return;
                    }

                    NewGame.PlayerGold -= artifact.ArtifactCost;
                    if (!NewGame.IsShopUnlocked)
                    {
                        Clear();
                        CW("Ez robbed ya, get lost!");
                        CW("-----");
                        NewGame.IsShopUnlocked = true;
                        while (true)
                        {
                            CW($"Fight for your {artifact.ArtifactCost} gold or leave with nothing? [f/l]");

                            switch (CKL())
                            {
                                case "f":
                                case "а":
                                    Clear();
                                    CW("The shop robber attacks!");
                                    PKC();
                                    if (TravelBattle.StartSpecialEncounter("special01"))
                                    {
                                        NewGame.PlayerGold += artifact.ArtifactCost;
                                        Clear();
                                        CW($"You defeated the robber and recovered {artifact.ArtifactCost} gold.");
                                        PKC();
                                    }

                                    return;
                                case "l":
                                case "д":
                                    Clear();
                                    CW("You leave the shop with empty pockets.");
                                    PKC();
                                    return;
                                default:
                                    Clear();
                                    CW("Wrong input detected. Please, try again!");
                                    CW("-----");
                                    CW("Ez robbed ya, get lost!");
                                    CW("-----");
                                    break;
                            }
                        }
                    }

                    Inventory.AddArtifact(artifact);
                    Clear();
                    CW($"{artifact.ArtifactName} added to inventory.");
                    CW("Equip it now? [y/n]");
                    switch (CKL())
                    {
                        case "y":
                        case "н":
                            Clear();
                            Inventory.ToggleEquip(artifact, out var message);
                            CW(message);
                            break;
                        case "n":
                        case "т":
                            break;
                    }
                    PKC();
                    return;
                default:
                    return;
            }
        }

        private static void BuyPotion(Potion potion)
        {
            Clear();
            CW($"You are willing to buy: {potion.PotionName} - {potion.PotionCost} {potion.PotionCurrency.CurrencyName}");
            CW($"Item description: {potion.PotionDescription} ({potion.PotionEffect.EffectName} +{potion.PotionImpactPoints})");
            CW("Are you sure you want to buy this item? [y/n]");
            switch (CKL())
            {
                case "y":
                case "н":
                    if (NewGame.PlayerGold < potion.PotionCost)
                    {
                        Clear();
                        CW("You have not enough money, get lost!");
                        PKC();
                        return;
                    }

                    NewGame.PlayerGold -= potion.PotionCost;
                    Inventory.AddPotion(potion);
                    Clear();
                    CW($"{potion.PotionName} added to inventory.");
                    PKC();
                    return;
                default:
                    return;
            }
        }
    }
}
