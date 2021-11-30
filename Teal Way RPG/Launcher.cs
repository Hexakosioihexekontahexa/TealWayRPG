using System;
using System.Collections.Generic;
using System.Threading;
using Teal_Way_RPG.GameData;
using Teal_Way_RPG.GameData.ArtifactData;
using Teal_Way_RPG.GameData.TownData;
using Teal_Way_RPG.GeneralData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG
{
    public class Launcher
    {
        public static string GameVersion = "1.9.10";
        public static bool DebugVar = false;

        public static void Main(string[] args)
        {
            
            //Check if lines below are still needed
            ////CW("In order to play this game, you need to have some packages installed.");
            ////Sleep(1);
            ////CW("Game will now check for all the needed packages automatically and will install all the missing libraries.");
            ////Sleep(1);
            ////CW("In case, you afraid of being cheated, please, install it manually.");
            ////Sleep(1);
            ////CW("Currently needed packages: ");
            ////Sleep(1);
            ////CW("1. pi");
            ////Sleep(1);
            ////CW("=====");
            ////Sleep(1);
            ////CW("Please, press any key to continue...");
            ////CR();
            Test(1);
            Initializer.Initialize();
            MainMenu.Menu(false);
            //NewGame;
        }

        public static int[] list = new int[2];
        
        public static int A = 2;
        public static int B = 5;

        public static void Test(int testDebugVar)
        {
            if (testDebugVar == 1)
            {
                //TestExec(list);
                //CW(list[0].ToString());
                //CW(list[1].ToString());

                CW("This is default text.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                CW("This is Venom");
                Console.ForegroundColor = ConsoleColor.Blue;
                CW("This is another venom");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                CW("That's it!");
                SetColorsToDefault();
                RarityProcessor.RarityInitializer();
                RarityProcessor.ProcessRarityText(Rarities.Common.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Uncommon.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Rare.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Infused.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Transcendent.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Epic.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Legendary.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Mythical.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Hellish.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Arcana.RarityName);
                RarityProcessor.ProcessRarityText(Rarities.Phantasm.RarityName);

                //CW("test key");
                //var a = Console.ReadKey().KeyChar;
                //CW($"{a}");
                //var b = Console.ReadKey().Key.ToString();
                //CW($"{b}");
                List<string> list = new List<string>() { "1111", "3333", "4444", "5555", "7777", "9999" };


                CW("");
                Cw("1. Debug mode - [");
                Options.OptionStateChecker(Launcher.DebugVar);
                Cw("]");

                //Initializer.Initialize();
                //Locations.GetLocationById("01LP");
                CR();

                for (int i = 0; i < 15; i++)
                {
                    var output = UtilsExtra.TryParseStringList(list, i, list[0]);
                    CW($"{output}");

                }

                CW("WARNING!!!");
                CR();
            }
        }

        static int[] TestExec(int[] list)
        {
            list[0] = A;
            list[1] = B;
            //CW(" " + list);
            return list;
        }

    }

    public class Initializer
    {
        public static void Initialize()
        {
            //new Thread(EffectProcessor.EffectInitializer);
            ////EffectProcessor.ProcessEffectText("venom");
            //new Thread(RarityProcessor.RarityInitializer);
            //new Thread(CurrencyProcessor.CurrencyInitializer);
            //new Thread(MonsterProcessor.MonsterInitializer);
            ////MonsterProcessor.RandomizeMonster("tuvale");
            //new Thread(ArtifactProcessor.ArtifactInitializer);
            //new Thread(PotionProcessor.PotionInitializer);
            //new Thread(TownProcessor.TownInitializer);
            //new Thread(LocationProcessor.LocationInitializer);
            EffectProcessor.EffectInitializer();
            RarityProcessor.RarityInitializer();
            CurrencyProcessor.CurrencyInitializer();
            MonsterProcessor.MonsterInitializer();
            ArtifactProcessor.ArtifactInitializer();
            PotionProcessor.PotionInitializer();
            TownProcessor.TownInitializer();
            LocationProcessor.LocationInitializer();
        }
    }
}