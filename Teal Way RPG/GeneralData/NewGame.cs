using System;
using System.Collections.Generic;
using System.Text;
using Teal_Way_RPG.GameData;
using Teal_Way_RPG.GameData.ArtifactData;
using Teal_Way_RPG.GameData.QuestData;
using Teal_Way_RPG.InventoryData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public static class NewGame
    {
        public static double Pi = Math.Round(Math.PI, 3);
        public static int PlayerLevel;
        public static string PlayerStatus;
        public const int BasePlayerStr = 1;//1
        public const int BasePlayerAgi = 0; //0
        public const int BasePlayerInt = 0; //0
        public static int CurrentPlayerStr;
        public static int CurrentPlayerAgi;
        public static int CurrentPlayerInt;
        public const double BasePlayerDmg = 1.0; //1.0
        public static double CurrentPlayerDmg;
        public static double LevelBasedPlayerDmg; //??
        public static int PlayerExp;
        public static int NextLvlExp;
        public static int PlayerGold; //0
        
        // public static bool SlotHead;
        // public static bool SlotChest;
        // public static bool SlotArms;
        // public static bool SlotLeftHand; 
        // public static bool SlotRightHand;
        // public static bool SlotLegs;
        // public static bool SlotBoots;
        // public static bool[] SlotAccessory = new bool[2];
        // public static bool[] SlotWeapon = new bool[2];
        // public static bool WeaponIsTwoHanded;
        // public static bool[] SlotUtil = new bool[4];
        // public static bool SlotGem;
        
         public static bool IsShopUnlocked;

        public static void Starter()
        {
            PlayerLevel = 1;
            PlayerStatus = "N/A";
            PlayerExp = 0;
            NextLvlExp = 25;
            PlayerGold = 1000; //0
            // #region Inventory Initialization
            // SlotHead = false;
            // SlotChest = false;
        // SlotArms = false;
            // SlotLeftHand = false;
            // SlotRightHand = false;
            // SlotLegs = false;
            // SlotBoots = false;
            // SlotAccessory[0] = false;
            // SlotAccessory[1] = false;
            // SlotWeapon[0] = false;
            // SlotWeapon[1] = false;
            // WeaponIsTwoHanded = false;
            // SlotUtil[0] = false;
            // SlotUtil[1] = false;
            // SlotUtil[2] = false;
            // SlotUtil[3] = false;
            // SlotGem = false;
            // #endregion
            CurrentPlayerStr = 0;
            CurrentPlayerAgi = 0;
            CurrentPlayerInt = 0;
            CurrentPlayerDmg = BasePlayerDmg;
            LevelBasedPlayerDmg = 0;
            IsShopUnlocked = Options.IsShopUnlocked;
            Inventory.Reset();

            //ArtifactProcessor.PhantasmesRandomizer();
            CharacterCreator();
        }

        public static void ResetCurrentProgress()
        {
            PlayerLevel = 0;
            PlayerStatus = "N/A";
            PlayerExp = 0;
            NextLvlExp = 0;
            PlayerGold = 0;
            CurrentPlayerStr = 0;
            CurrentPlayerAgi = 0;
            CurrentPlayerInt = 0;
            CurrentPlayerDmg = BasePlayerDmg;
            LevelBasedPlayerDmg = 0;
            IsShopUnlocked = false;
            Inventory.Reset();
        }

        public static int GetTotalStrength()
        {
            return BasePlayerStr + CurrentPlayerStr;
        }
        
        public static int GetTotalAgility()
        {
            return BasePlayerAgi + CurrentPlayerAgi;
        }
        
        public static int GetTotalIntelligence()
        {
            return BasePlayerInt + CurrentPlayerInt;
        }
        
        private static void CharacterCreator()
        {
            while (true)
            {
                CurrentPlayerStr = 0;
                CurrentPlayerAgi = 0;
                CurrentPlayerInt = 0;

                for (var i = 0; i < 3; i++)
                {
                    Clear();
                    var text = "Before you can seek for your destiny, warrior,\n" +
                           "you should choose what are you skilled most.\n" +
                           $"Please, choose skills you want to enhance ({3-i} point(s) left):\n" +
                           $"1. Strength: {GetTotalStrength()}. Increases your inner vitality and damage.\n" +
                           $"2. Agility: {GetTotalAgility()}. Decreases incoming physical damage.\n" +
                           $"3. Intelligence: {GetTotalIntelligence()}. Increases your arcane power potency.";
                    CW(text);
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
                            switch (WrongInput(text, 1, 2, 3))
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
                var summary = "You are all set!\n" +
                       $"Strength: {GetTotalStrength()}.\n" +
                       $"Agility: {GetTotalAgility()}.\n" +
                       $"Intelligence: {GetTotalIntelligence()}.\n" +
                       "Begin your journey or wish to Re-arrange your skillset? [b/r]";
                CW(summary);
                switch (CKL())
                {
                    case "b":
                    case "и":
                        return;
                    case "r":
                    case "к":
                        continue;
                    default:
                        if (WrongInput(summary, "b", "r") is "b" or "и")
                        {
                            return;
                        }
                        continue;
                }
            }
        }
    }
}
