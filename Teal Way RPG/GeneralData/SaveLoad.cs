using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Teal_Way_RPG.InventoryData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public static class SaveLoad
    {
        private static readonly string SavePath = Path.Combine(AppContext.BaseDirectory, "savegame.json");

        public static bool HasSaveFile()
        {
            return File.Exists(SavePath);
        }

        public static void Save()
        {
            SaveSilent();
            Clear();
            CW($"Game saved to {SavePath}");
            PKC();
        }

        public static void SaveSilent()
        {
            var data = new SaveGameModel
            {
                PlayerLevel = NewGame.PlayerLevel,
                PlayerExp = NewGame.PlayerExp,
                NextLvlExp = NewGame.NextLvlExp,
                PlayerGold = NewGame.PlayerGold,
                CurrentPlayerStr = NewGame.CurrentPlayerStr,
                CurrentPlayerAgi = NewGame.CurrentPlayerAgi,
                CurrentPlayerInt = NewGame.CurrentPlayerInt,
                IsShopUnlocked = NewGame.IsShopUnlocked,
                ArtifactIds = Inventory.ArtifactItems.Select(a => a.ArtifactId).ToList(),
                PotionIds = Inventory.PotionItems.Select(p => p.PotionId).ToList(),
                EquippedArtifactIds = Inventory.EquippedArtifacts.Select(a => a.ArtifactId).ToList()
            };

            File.WriteAllText(SavePath, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static void DeleteSaveFile()
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }
        }

        public static void Load()
        {
            if (!File.Exists(SavePath))
            {
                Clear();
                CW("Save file was not found.");
                PKC();
                return;
            }

            var raw = File.ReadAllText(SavePath);
            var data = JsonSerializer.Deserialize<SaveGameModel>(raw);
            if (data == null)
            {
                Clear();
                CW("Save file is corrupted or empty.");
                PKC();
                return;
            }

            NewGame.PlayerLevel = data.PlayerLevel;
            NewGame.PlayerExp = data.PlayerExp;
            NewGame.NextLvlExp = data.NextLvlExp;
            NewGame.PlayerGold = data.PlayerGold;
            NewGame.CurrentPlayerStr = data.CurrentPlayerStr;
            NewGame.CurrentPlayerAgi = data.CurrentPlayerAgi;
            NewGame.CurrentPlayerInt = data.CurrentPlayerInt;
            NewGame.IsShopUnlocked = data.IsShopUnlocked;

            Inventory.RestoreState(data.ArtifactIds, data.PotionIds, data.EquippedArtifactIds);

            Clear();
            CW("Game loaded successfully.");
            PKC();
        }

        private class SaveGameModel
        {
            public int PlayerLevel { get; set; }
            public int PlayerExp { get; set; }
            public int NextLvlExp { get; set; }
            public int PlayerGold { get; set; }
            public int CurrentPlayerStr { get; set; }
            public int CurrentPlayerAgi { get; set; }
            public int CurrentPlayerInt { get; set; }
            public bool IsShopUnlocked { get; set; }
            public List<string> ArtifactIds { get; set; }
            public List<string> PotionIds { get; set; }
            public List<string> EquippedArtifactIds { get; set; }
        }
    }
}
