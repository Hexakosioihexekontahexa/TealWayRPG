using System;
using System.Collections.Generic;
using System.Text;
using Teal_Way_RPG.GameData;
using Teal_Way_RPG.GameData.ArtifactData;
using Teal_Way_RPG.GameData.QuestData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public static class NewGame
    {
        public static double Pi = Math.Round(Math.PI, 3);
        public static int PlayerLevel;
        public static string PlayerStatus;
        public const int BasePlayerStr = 1;//1
        public const int PlayerAgi = 0; //0
        public const int PlayerInt = 0; //0
        public static int CurrentPlayerStr;
        public static int CurrentPlayerAgi;
        public static int CurrentPlayerInt;
        public const double BasePlayerDmg = 1.0; //1.0
        public static double CurrentPlayerDmg;
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
            PlayerLevel = 0;
            PlayerStatus = "N/A";
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

        private static void CharacterCreator(params int[] choice)
        {
            int points;
            if (choice.Length == 2)
            {
                switch (choice[0])
                {
                    case 1:
                        CurrentPlayerStr++;
                        break;
                    case 2:
                        CurrentPlayerAgi++;
                        break;
                    case 3:
                        CurrentPlayerInt++;
                        break;
                }
            }

            while (true)
            {
                CurrentPlayerStr = 0;
                CurrentPlayerAgi = 0;
                CurrentPlayerInt = 0;

                if (choice.Length == 2)
                {
                    points = choice[1];
                }
                else
                {
                    points = 3;
                }

                for (var i = 0; i < points; i++)
                {
                    Clear();
                    Text = "Before you can seek for your destiny, warrior,\n" +
                           "you should choose what are you skilled most.\n" +
                           $"Please, choose skills you want to enhance ({points-i} point(s) left):\n" +
                           $"1. Strength: {BasePlayerStr + CurrentPlayerStr}. Increases your inner vitality and damage.\n" +
                           $"2. Agility: {PlayerAgi + CurrentPlayerAgi}. Increases your damage resistance.\n" +
                           $"3. Intelligence: {PlayerInt + CurrentPlayerInt}. Increases your arcane power potency.";
                    CW(Text);
                    switch (CK())
                    {
                        case "1":
                            CurrentPlayerStr++;
                            break;
                        case "2":
                            CurrentPlayerAgi++;
                            break;
                        case "3":
                            CurrentPlayerInt++;
                            break;
                        default:
                            switch (WrongInput(Text, 1, 2, 3))
                            {
                                case "1":
                                    CurrentPlayerStr++;
                                    break;
                                case "2":
                                    CurrentPlayerAgi++;
                                    break;
                                case "3":
                                    CurrentPlayerInt++;
                                    break;
                            } 
                            break;
                    }
                }

                Clear();
                Text = "You are all set!\n" +
                       $"Strength: {BasePlayerStr + CurrentPlayerStr}.\n" +
                       $"Agility: {PlayerAgi + CurrentPlayerAgi}.\n" +
                       $"Intelligence: {PlayerInt + CurrentPlayerInt}.\n" +
                       "Begin your journey or wish to Re-arrange your skillset? [b/r]";
                CW(Text);
                switch (CKL())
                {
                    case "b":
                        break;
                    case "r":
                        continue;
                    default:
                        if(WrongInput(Text, "b", "r") == "r") continue;
                        break;
                }

                break;
            }

            GameMenu.Menu(Launcher.DebugVar);
        }
    }
}
