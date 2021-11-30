using System;
using System.Collections.Generic;
using System.Text;
using Teal_Way_RPG.GameData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public static class NewGame
    {
        public static double Pi = Math.Round(Math.PI, 3);
        public static int PlayerLevel;
        public static string PlayerStatus;
        public static int PlayerStr;//2
        public static int PlayerAgi; //1
        public static int PlayerInt; //1
        public static double BasePlayerDmg; //3.0
        public static double LevelBasedPlayerDmg; //??
        public static int PlayerExp;
        public static int NextLvlExp;
        public static int PlayerGold; //0
        public static bool SlotHead;
        public static bool SlotChest;
        public static bool SlotHands;
        public static bool SlotLeftHand; 
        public static bool SlotRightHand;
        public static bool SlotLegs;
        public static bool SlotBoots;
        public static bool[] SlotAccessory = new bool[2];
        public static bool[] SlotWeapon = new bool[2];
        public static bool WeaponIsTwoHanded;
        public static bool[] SlotUtil = new bool[4];
        public static bool SlotGem;
        public static bool IsShopUnlocked;

        public static void Starter()
        {
            PlayerLevel = 1;
            PlayerStatus = "N/A";
            PlayerStr = 0; //2
            PlayerAgi = 0; //1
            PlayerInt = 0; //1
            BasePlayerDmg = 1; //3
            PlayerExp = 0;
            NextLvlExp = 25;
            PlayerGold = 1000; //0
            #region Inventory Initialization
            SlotHead = false;
            SlotChest = false;
            SlotHands = false;
            SlotLeftHand = false;
            SlotRightHand = false;
            SlotLegs = false;
            SlotBoots = false;
            SlotAccessory[0] = false;
            SlotAccessory[1] = false;
            SlotWeapon[0] = false;
            SlotWeapon[1] = false;
            WeaponIsTwoHanded = false;
            SlotUtil[0] = false;
            SlotUtil[1] = false;
            SlotUtil[2] = false;
            SlotUtil[3] = false;
            SlotGem = false;
            #endregion
            IsShopUnlocked = false;

            //ArtifactProcessor.PhantasmesRandomizer();
            CharacterCreator();
        }

        private static void CharacterCreator()
        {
            int points = 3;
            for (var i = 0; i <= points; i++)
            {
                Clear();
                CW("Before you can seek for your destiny, warrior,");
                CW("you should choose what are you skilled most.");
                CW($"Please, choose skills you want to enhance ({points} point(s) left):");
                CW($"1. Strength: {PlayerStr}. Increases your inner vitality and damage.");
                CW($"2. Agility: {PlayerAgi}. Increases your damage resistance.");
                CW($"3. Intelligence: {PlayerInt}. Increases your arcane power potency.");
                switch (CK())
                {
                    case "1":
                        PlayerStr++;
                        points--;
                        break;
                    case "2":
                        PlayerAgi++;
                        points--;
                        break;
                    case "3":
                        PlayerInt++;
                        points--;
                        break;
                    default:
                        CW("Wrong input detected. Please, try again!");
                        CR();
                        i--;
                        break;
                }
            }
            CW("You are all set! Begin your journey or wish to re-arrange your skillset? [y/n]");
            var input = Console.ReadKey().KeyChar.ToString();
        }
    }
}
