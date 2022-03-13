using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Teal_Way_RPG.GameData;
using Teal_Way_RPG.GameData.ArtifactData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    internal class IdChecker
    {
        //private static List<string> EveryList = new List<string>();
        private static bool exitCode = false;

        public static void Opener()
        {
            exitCode = false;
            while (true)
            {
                //EveryList.AddRange(Artifacts.ArtifactIdList);
                Clear();
                CW("Game Data id Checker");
                CW("-----");
                CW("1. Enter id");
                CW("0. Back");
                var input = CK();
                switch (input)
                {
                    case "1":
                        Table();
                        var globalLocker = true;
                        input = CR();
                        if (input == "0") break;
                        var charIdentifier = "";
                        var pos = 0;
                        var inputLength = input.ToCharArray().Length;

                        // localLocker start
                        bool localLocker;
                        //while (pos < inputLength && !char.IsLetter(input[pos]))
                        //{
                        //    ++pos;
                        //}
                        do
                        {
                            //pos = 0;
                            try
                            {
                                if (inputLength == 4) pos = 2;
                                if (inputLength > 4) pos = 3;
                                charIdentifier = input[pos].ToString();
                                localLocker = false;
                            }
                            catch
                            {
                                localLocker = true;
                                CW("Unexpected id passed, please try again!");
                                CWL("Awaiting input...: ");
                                input = CR();
                                //CW(input);
                                inputLength = input.ToCharArray().Length;
                                //while (pos < inputLength && !char.IsLetter(input[pos]))
                                //{
                                //    ++pos;
                                //}
                            }
                        } while (localLocker);
                        // localLocker end

                        dynamic output;
                        string outputType;
                        string dataCloser;
                        switch (charIdentifier.ToLower())
                        {
                            case "a":
                                output = Artifacts.GetArtifactById(input.ToUpper());
                                if (output == Artifacts.DefaultArtifact00)
                                {
                                    CW($"Unknown/Default ArtifactId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                    //output = Artifacts.GetArtifactById(input.ToUpper());
                                }

                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id           |{output.ArtifactId}");
                                CW($"{outputType} Name         |{output.ArtifactName}");
                                CW($"{outputType} Description  |{output.ArtifactDescription}");
                                CW($"{outputType} Location     |{output.ArtifactLocation}");
                                Cw($"{outputType} Rarity       |");
                                RarityProcessor.ProcessRarityText(output.ArtifactRarity.RarityName);
                                CWL($"{outputType} Cost Currency|{output.ArtifactCost} {output.ArtifactCurrency.CurrencyName}");
                                CWL($"{outputType} Effect Type  |[{output.ArtifactEffect.EffectId}] ");
                                SetColorsTo(output.ArtifactEffect.ColorFore, output.ArtifactEffect.ColorBack);
                                Cw($"{output.ArtifactEffect.EffectName}");
                                SetColorsToDefault();
                                Cw($" ({output.ArtifactImpactType})");
                                CWL($"{outputType} Points Type  |{output.ArtifactImpactPoints} {output.ArtifactPointsType}");
                                CWL($"{outputType} Slot         |{output.ArtifactSlot}");
                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                Opener("data_read");
                                break;

                            case "c":
                                output = Currencies.GetCurrencyById(input.ToUpper());
                                if (output == Currencies.DefaultCurrency)
                                {
                                    CW($"Unknown/Default CurrencyId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                    //output = Currencies.GetCurrencyById(input.ToUpper());
                                }

                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id             |{output.CurrencyId}");
                                CW($"{outputType} Name           |{output.CurrencyName}");
                                CW($"{outputType} Multiple Name  |{output.CurrencyMultipleName}");
                                CW($"{outputType} Gold Based Rate|{output.CurrencyGoldBasedRate}");
                                CW($"{outputType} Is Exchangeable|{output.IsExchangeable}");
                                CW(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                Opener("data_read");
                                break;

                            case "e":
                                output = Effects.GetEffectById(input.ToUpper());
                                if (output == Effects.DefaultEffect)
                                {
                                    CW($"Unknown/Default EffectId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }

                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id              |{output.EffectId}");
                                CW($"{outputType} Short Name      |{output.EffectShortName}");
                                CW($"{outputType} Name            |{output.EffectName}");
                                CW($"{outputType} Status Text     |{output.EffectStatusText}");
                                CW($"{outputType} Type            |{output.EffectType}");
                                CW($"{outputType} Speciality      |{output.EffectSpeciality}");
                                CW($"{outputType} Impact Points   |{output.EffectImpactPoints}");
                                CW($"{outputType} Number of Turns |{output.NumberOfTurns}");
                                CW($"{outputType} Impact Type     |{output.EffectImpactType}");
                                CW($"{outputType} Color Foreground|{output.ColorFore}");
                                CW($"{outputType} Color Background|{output.ColorBack}");
                                CW(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                Opener("data_read");
                                break;

                            case "l":
                                output = Locations.GetLocationById(input.ToUpper());
                                if (output == Locations.DefaultLocation)
                                {
                                    CW($"Unknown/Default LocationId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }

                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id           |{output.LocationId}");
                                CW($"{outputType} Short Name   |{output.LocationShortName}");
                                CW($"{outputType} Name         |{output.LocationName}");
                                CW($"{outputType} Monsters     |{output.LocationMonsters}");
                                CW($"{outputType} Town         |{output.LocationTown}");
                                CW($"{outputType} Artifacts    |{output.LocationArtifacts}");
                                CW($"{outputType} Potions      |{output.LocationPotions}");
                                CW("-----");
                                CW("1. Check Location Monsters");
                                CW("2. Check Location Artifacts");
                                CW("3. Check Location Potions");
                                CW("0. Cancel");
                                CWL("Awaiting input...: ");
                                input = CK();
                                // localLocker start
                                do
                                {
                                    switch (input)
                                    {
                                        case "1":
                                            localLocker = false;
                                            CWL("-----");
                                            CWL($"Monsters of {output.LocationName}");
                                            CWL("---");
                                            foreach (var monster in output.LocationMonsters) CWL(monster.Value.MonsterName + $" ({monster.Value.MonsterId})");

                                            break;

                                        case "2":
                                            localLocker = false;
                                            CWL("-----");
                                            CWL($"Artifacts of {output.LocationName}");
                                            CWL("---");
                                            foreach (var artifact in output.LocationArtifacts) CWL(artifact.Value.ArtifactName + $" ({artifact.Value.ArtifactId})");

                                            break;

                                        case "3":
                                            localLocker = false;
                                            CWL("-----");
                                            CWL($"Potions of {output.LocationName}");
                                            CWL("---");
                                            foreach (var potion in output.LocationPotions) CWL(potion.Value.PotionName + $" ({potion.Value.PotionId})");

                                            break;

                                        case "0":
                                            localLocker = false;
                                            break;

                                        default:
                                            localLocker = true;
                                            while (true)
                                            {
                                                CWL($"Wrong input detected. Please, try again!");
                                                CWL("Awaiting input...: ");
                                                input = CK();
                                                if (input == "1" || input == "2" || input == "3" || input == "0") break;
                                            }

                                            break;
                                    }
                                } while (localLocker);
                                // localLocker end

                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                Opener("data_read");
                                break;

                            case "m":
                                output = Monsters.GetMonsterById(input.ToUpper());
                                if (output == Monsters.DefaultMonster00)
                                {
                                    CW($"Unknown/Default MonsterId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                    //output = Monsters.GetMonsterById(input.ToUpper());
                                }

                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id               |{output.MonsterId}");
                                CW($"{outputType} Name             |{output.MonsterName}");
                                CW($"{outputType} Health points    |{output.MonsterHp}");
                                CW($"{outputType} Min Dmg - Max Dmg|{output.MonsterMinDmg} - {output.MonsterMaxDmg}");
                                CW($"{outputType} Gold Bounty      |{output.GoldBounty}");
                                CW($"{outputType} Experience Bounty|{output.ExpBounty}");
                                CW($"{outputType} Location         |{output.MonsterLocation}");
                                CW($"{outputType} Encounter Chance |{output.EncounterChance}");
                                Cw($"{outputType} Effect           |[{output.MonsterEffect.EffectId}] ");
                                try
                                {
                                    SetColorsTo(output.MonsterEffect.ColorFore, output.MonsterEffect.ColorBack);
                                    Cw($"{output.MonsterEffect.EffectName}");
                                    SetColorsToDefault();
                                    Cw($" ({output.MonsterEffect.EffectSpeciality})");
                                }
                                catch
                                {
                                    SetColorsToDefault();
                                    Cw("None");
                                }

                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                Opener("data_read");
                                break;

                            case "p":
                                output = Potions.GetPotionById(input.ToUpper());
                                if (output == Potions.DefaultPotion)
                                {
                                    CW($"Unknown/Default PotionId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                    //output = Potions.GetPotionById(input.ToUpper());
                                }

                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id           |{output.PotionId}");
                                CW($"{outputType} Name         |{output.PotionName}");
                                CW($"{outputType} Description  |{output.PotionDescription}");
                                CW($"{outputType} Location     |{output.PotionLocation}");
                                Cw($"{outputType} Rarity       |");
                                RarityProcessor.ProcessRarityText(output.PotionRarity.RarityName);
                                CWL($"{outputType} Cost Currency|{output.PotionCost} {output.PotionCurrency.CurrencyName}");
                                CWL($"{outputType} Effect Type  |[{output.PotionEffect.EffectId}] ");
                                SetColorsTo(output.PotionEffect.ColorFore, output.PotionEffect.ColorBack);
                                Cw($"{output.PotionEffect.EffectName}");
                                SetColorsToDefault();
                                Cw($" ({output.PotionImpactType})");
                                CWL($"{outputType} Points Type  |{output.PotionImpactPoints} {output.PotionPointsType}");
                                CWL($"{outputType} Slot         |{output.PotionSlot}");
                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                Opener("data_read");
                                break;

                            case "t":
                                output = Towns.GetTownById(input.ToUpper());
                                if (output == Towns.DefaultTown)
                                {
                                    CW($"Unknown/Default TownId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }

                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id                   |{output.TownId}");
                                CW($"{outputType} Name                 |{output.TownName}");
                                CW($"{outputType} Encounter Chance     |{output.TownEncounterChance}");
                                CW($"{outputType} True Encounter Serial|{output.TownTrueEncounterSerial}");
                                Cw($"{outputType} Specialization       |");
                                try
                                {
                                    Cw($"[{output.TownSpecialization.EffectId}] ");
                                    SetColorsTo(output.TownSpecialization.ColorFore, output.TownSpecialization.ColorBack);
                                    Cw($"{output.TownSpecialization.EffectName}");
                                }
                                catch
                                {
                                    SetColorsToDefault();
                                    Cw("None");
                                }

                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                Opener("data_read");
                                break;

                            case "0":
                                break;

                            default:
                                CW("Unexpected id passed, please try again!");
                                PKC();
                                Opener("data_read");
                                break;
                        }

                        break;

                    case "0":
                        break;

                    default:
                        CW("Wrong input detected. Please, try again!");
                        continue;
                }

                break;
            }
        }

        public static void Opener(string switchCase)
        {
            var input = "1";
            //if (exitCode) input = "0";
            switch (input)
            {
                case "1":
                    var globalLocker = true;
                    while (globalLocker)
                    {
                        if (exitCode)
                        {
                            globalLocker = false;
                            break;
                        }
                        Table();
                        if (switchCase == "data_read") switchCase = CR();
                        var pos = 0;
                        input = switchCase;
                        //CW(input);
                        var inputLength = input.ToCharArray().Length;
                        bool isLocked;
                        //while (pos < inputLength && !char.IsLetter(input[pos]))
                        //{
                        //    ++pos;
                        //}
                        do
                        {
                            //pos = 0;
                            try
                            {
                                if (inputLength == 4) pos = 2;
                                if (inputLength > 4) pos = 3;
                                switchCase = input[pos].ToString();
                                isLocked = false;
                            }
                            catch
                            {
                                isLocked = true;
                                CW("Unexpected id passed, please try again!");
                                CWL("Awaiting input...: ");
                                input = CR();
                                //CW(input);
                                inputLength = input.ToCharArray().Length;
                                //while (pos < inputLength && !char.IsLetter(input[pos]))
                                //{
                                //    ++pos;
                                //}

                            }
                        } while (isLocked);

                        dynamic output;
                        string outputType;
                        string dataCloser;
                        switch (switchCase.ToLower())
                        {
                            case "a":
                                output = Artifacts.GetArtifactById(input.ToUpper());
                                if (output == Artifacts.DefaultArtifact00)
                                {
                                    //locker = false;
                                    //CW(input);
                                    CW($"Unknown/Default ArtifactId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }
                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id           |{output.ArtifactId}");
                                CW($"{outputType} Name         |{output.ArtifactName}");
                                CW($"{outputType} Description  |{output.ArtifactDescription}");
                                CW($"{outputType} Location     |{output.ArtifactLocation}");
                                Cw($"{outputType} Rarity       |"); RarityProcessor.ProcessRarityText(output.ArtifactRarity.RarityName);
                                CWL($"{outputType} Cost Currency|{output.ArtifactCost} {output.ArtifactCurrency.CurrencyName}");
                                CWL($"{outputType} Effect Type  |[{output.ArtifactEffect.EffectId}] ");
                                SetColorsTo(output.ArtifactEffect.ColorFore, output.ArtifactEffect.ColorBack);
                                Cw($"{output.ArtifactEffect.EffectName}");
                                SetColorsToDefault();
                                Cw($" ({output.ArtifactImpactType})");
                                CWL($"{outputType} Points Type  |{output.ArtifactImpactPoints} {output.ArtifactPointsType}");
                                CWL($"{outputType} Slot         |{output.ArtifactSlot}");
                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                globalLocker = false;
                                Opener("data_read");
                                break;

                            case "c":
                                output = Currencies.GetCurrencyById(input.ToUpper());
                                if (output == Currencies.DefaultCurrency)
                                {
                                    //locker = false;
                                    //CW(input);
                                    CW($"Unknown/Default CurrencyId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }
                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id             |{output.CurrencyId}");
                                CW($"{outputType} Name           |{output.CurrencyName}");
                                CW($"{outputType} Multiple Name  |{output.CurrencyMultipleName}");
                                CW($"{outputType} Gold Based Rate|{output.CurrencyGoldBasedRate}");
                                CW($"{outputType} Is Exchangeable|{output.IsExchangeable}");
                                CW(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                globalLocker = false;
                                Opener("data_read");
                                break;

                            case "e":
                                output = Effects.GetEffectById(input.ToUpper());
                                if (output == Effects.DefaultEffect)
                                {
                                    //locker = false;
                                    //CW(input);
                                    CW($"Unknown/Default EffectId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }
                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id              |{output.EffectId}");
                                CW($"{outputType} Short Name      |{output.EffectShortName}");
                                CW($"{outputType} Name            |{output.EffectName}");
                                CW($"{outputType} Status Text     |{output.EffectStatusText}");
                                CW($"{outputType} Type            |{output.EffectType}");
                                CW($"{outputType} Speciality      |{output.EffectSpeciality}");
                                CW($"{outputType} Impact Points   |{output.EffectImpactPoints}");
                                CW($"{outputType} Number of Turns |{output.NumberOfTurns}");
                                CW($"{outputType} Impact Type     |{output.EffectImpactType}");
                                CW($"{outputType} Color Foreground|{output.ColorFore}");
                                CW($"{outputType} Color Background|{output.ColorBack}");
                                CW(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                globalLocker = false;
                                Opener("data_read");
                                break;

                            case "l":
                                output = Locations.GetLocationById(input.ToUpper());
                                if (output == Locations.DefaultLocation)
                                {
                                    //locker = false;
                                    //CW(input);
                                    CW($"Unknown/Default LocationId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }
                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id           |{output.LocationId}");
                                CW($"{outputType} Short Name   |{output.LocationShortName}");
                                CW($"{outputType} Name         |{output.LocationName}");
                                CW($"{outputType} Monsters     |{output.LocationMonsters}");
                                CW($"{outputType} Town         |{output.LocationTown}");
                                CW($"{outputType} Artifacts    |{output.LocationArtifacts}");
                                CW($"{outputType} Potions      |{output.LocationPotions}");
                                CW("-----");
                                CW("1. Check Location Monsters");
                                CW("2. Check Location Artifacts");
                                CW("3. Check Location Potions");
                                CW("0. Cancel");
                                CWL("Awaiting input...: ");
                                input = CK();
                                do
                                {
                                    switch (input)
                                    {
                                        case "1":
                                            isLocked = false;
                                            CWL("-----");
                                            CWL($"Monsters of {output.LocationName}");
                                            CWL("---");
                                            foreach (var monster in output.LocationMonsters) CWL(monster.Value.MonsterName + $" ({monster.Value.MonsterId})");

                                            break;

                                        case "2":
                                            isLocked = false;
                                            CWL("-----");
                                            CWL($"Artifacts of {output.LocationName}");
                                            CWL("---");
                                            foreach (var artifact in output.LocationArtifacts) CWL(artifact.Value.ArtifactName + $" ({artifact.Value.ArtifactId})");

                                            break;

                                        case "3":
                                            isLocked = false;
                                            CWL("-----");
                                            CWL($"Potions of {output.LocationName}");
                                            CWL("---");
                                            foreach (var potion in output.LocationPotions) CWL(potion.Value.PotionName + $" ({potion.Value.PotionId})");

                                            break;

                                        case "0":
                                            isLocked = false;
                                            break;

                                        default:
                                            isLocked = true;
                                            while (true)
                                            {
                                                CWL($"Wrong input detected. Please, try again!");
                                                CWL("Awaiting input...: ");
                                                input = CK();
                                                if (input == "1" || input == "2" || input == "3" || input == "0")
                                                    break;
                                            }

                                            break;
                                    }
                                } while (isLocked);

                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                globalLocker = false;
                                Opener("data_read");
                                break;

                            case "m":
                                output = Monsters.GetMonsterById(input.ToUpper());
                                if (output == Monsters.DefaultMonster00)
                                {
                                    //locker = false;
                                    //CW(input);
                                    CW($"Unknown/Default MonsterId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }
                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id               |{output.MonsterId}");
                                CW($"{outputType} Name             |{output.MonsterName}");
                                CW($"{outputType} Health points    |{output.MonsterHp}");
                                CW($"{outputType} Min Dmg - Max Dmg|{output.MonsterMinDmg} - {output.MonsterMaxDmg}");
                                CW($"{outputType} Gold Bounty      |{output.GoldBounty}");
                                CW($"{outputType} Experience Bounty|{output.ExpBounty}");
                                CW($"{outputType} Location         |{output.MonsterLocation}");
                                CW($"{outputType} Encounter Chance |{output.EncounterChance}");
                                Cw($"{outputType} Effect           |[{output.MonsterEffect.EffectId}] ");
                                try
                                {
                                    SetColorsTo(output.MonsterEffect.ColorFore, output.MonsterEffect.ColorBack);
                                    Cw($"{output.MonsterEffect.EffectName}");
                                    SetColorsToDefault();
                                    Cw($" ({output.MonsterEffect.EffectSpeciality})");
                                }
                                catch
                                {
                                    SetColorsToDefault();
                                    Cw("None");
                                }
                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                globalLocker = false;
                                Opener("data_read");
                                break;

                            case "p":
                                output = Potions.GetPotionById(input.ToUpper());
                                if (output == Potions.DefaultPotion)
                                {
                                    //locker = false;
                                    //CW(input);
                                    CW($"Unknown/Default PotionId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }
                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id           |{output.PotionId}");
                                CW($"{outputType} Name         |{output.PotionName}");
                                CW($"{outputType} Description  |{output.PotionDescription}");
                                CW($"{outputType} Location     |{output.PotionLocation}");
                                Cw($"{outputType} Rarity       |"); RarityProcessor.ProcessRarityText(output.PotionRarity.RarityName);
                                CWL($"{outputType} Cost Currency|{output.PotionCost} {output.PotionCurrency.CurrencyName}");
                                CWL($"{outputType} Effect Type  |[{output.PotionEffect.EffectId}] ");
                                SetColorsTo(output.PotionEffect.ColorFore, output.PotionEffect.ColorBack);
                                Cw($"{output.PotionEffect.EffectName}");
                                SetColorsToDefault();
                                Cw($" ({output.PotionImpactType})");
                                CWL($"{outputType} Points Type  |{output.PotionImpactPoints} {output.PotionPointsType}");
                                CWL($"{outputType} Slot         |{output.PotionSlot}");
                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                globalLocker = false;
                                Opener("data_read");
                                break;

                            case "t":
                                output = Towns.GetTownById(input.ToUpper());
                                if (output == Towns.DefaultTown)
                                {
                                    //locker = false;
                                    //CW(input);
                                    CW($"Unknown/Default TownId: \"{input}\" was passed. Please, try again!");
                                    PKC();
                                    Opener("data_read");
                                    break;
                                }
                                outputType = StringTypeCutter(output.ToString(), '.');
                                CW("=====");
                                dataCloser = $"Data Element type: {output.GetType()}";
                                CW(dataCloser);
                                CW("---");
                                CW($"{outputType} Id                   |{output.TownId}");
                                CW($"{outputType} Name                 |{output.TownName}");
                                CW($"{outputType} Encounter Chance     |{output.TownEncounterChance}");
                                CW($"{outputType} True Encounter Serial|{output.TownTrueEncounterSerial}");
                                Cw($"{outputType} Specialization       |");
                                try
                                {
                                    Cw($"[{output.TownSpecialization.EffectId}] ");
                                    SetColorsTo(output.TownSpecialization.ColorFore, output.TownSpecialization.ColorBack);
                                    Cw($"{output.TownSpecialization.EffectName}");
                                }
                                catch
                                {
                                    SetColorsToDefault();
                                    Cw("None");
                                }
                                CWL(DataCloser(dataCloser.Length));
                                CWL("Press any key to go back...");
                                CK();
                                globalLocker = false;
                                Opener("data_read");
                                break;

                            case "0":
                                globalLocker = false;
                                exitCode = true;
                                break;

                            default:
                                //locker = false;
                                //CW(input);
                                //CWE();
                                CW("Unexpected id passed, please try again!");
                                PKC();
                                Opener("data_read");
                                break;
                        }
                    }
                    break;

                case "0":
                    break;

                default:
                    CW("Wrong input detected. Please, try again!");
                    Opener("data_read");
                    break;
            }
        }

        private static void Table()
        {
            Clear();
            CW("Game Data id Checker");
            CW("--------------------");
            CW("Please, enter register-free id (Game Data EveryList is shown below):");
            CW(">>>");
            CW($"{"Arts",5}{"Curs",8}{"Effs",8}{"Locs",8}{"Mons",9}{"Pots",10}{"Twns",7}");
            //CW(Environment.NewLine);
            //CW("text");
            var iterator = UtilsExtra.GetMostElementsNumber();
            for (var i = 0; i < iterator; i++)
                CW($"{UtilsExtra.TryParseStringList(Artifacts.ArtifactIdList, i, Artifact.BaseIdFormat),5}" +
                   $"{UtilsExtra.TryParseStringList(Currencies.CurrencyIdList, i, Currency.BaseIdFormat),7}" +
                   $"{UtilsExtra.TryParseStringList(Effects.EffectIdList, i, Effect.BaseIdFormat),9}" +
                   $"{UtilsExtra.TryParseStringList(Locations.LocationIdList, i, Location.BaseIdFormat),7}" +
                   $"{UtilsExtra.TryParseStringList(Monsters.MonsterIdList, i, Monster.BaseIdFormat),11}" +
                   $"{UtilsExtra.TryParseStringList(Potions.PotionIdList, i, Potion.BaseIdFormat),8}" +
                   $"{UtilsExtra.TryParseStringList(Towns.TownIdList, i, Town.BaseIdFormat),8}");
            //Console.WriteLine(Environment.NewLine + "{0,5}", id);
            Cw(">>>");
            CWL("0. Back to Main Menu");
            CWL("Awaiting input...: ");
        }
    }
}
