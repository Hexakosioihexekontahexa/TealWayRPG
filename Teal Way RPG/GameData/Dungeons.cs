using System;
using System.Collections.Generic;
using System.Text;

namespace Teal_Way_RPG.GameData
{
    public class Dungeon
    {
        public int DungeonSimpleId;

        public string DungeonId; //## number of implementation

        // T code
        //? type (storyline of random)
        public string DungeonName;
        public string DungeonDescription;

        public int DungeonEncounterChance;

        public int DungeonTrueEncounterSerial; //allows to 100%-ly encounter dungeon upon Traveling action; optional
        public Effect DungeonEffect; //optional
        public bool WasPicked;
        public static string LastDungeonId;

        public static string BaseIdFormat = "#####";


        public Dungeon(int dungeonSimpleId, string dungeonId, string dungeonName, int dungeonEncounterChance,
            string dungeonDescription = "This is just another ordinary wild place.",
            bool wasPicked = false, int dungeonTrueEncounterSerial = -1,
            Effect dungeonEffect = null)
        {
            DungeonSimpleId = dungeonSimpleId;
            DungeonId = dungeonId;
            DungeonName = dungeonName;
            DungeonDescription = dungeonDescription;
            DungeonEncounterChance = dungeonEncounterChance;
            DungeonTrueEncounterSerial = dungeonTrueEncounterSerial;
            DungeonEffect = dungeonEffect;
            WasPicked = wasPicked;
        }

        public Dungeon(string dungeonId, string dungeonName)
        {
            DungeonId = dungeonId;
            DungeonName = dungeonName;
        }
    }

    public class Dungeons
    {
        public static Dungeon DefaultDungeon = new Dungeon("000DD", "Default");
        public static Dungeon WildwoodsDungeon00 = new Dungeon(0, "000DS", "Wild Woods",
                                                            100, "This is just another ordinary wild place.",
                                                            false, 0); //Demodungeon
        public static Dungeon ChamberLostDungeon01 = new Dungeon(1, "001DR", "Chamber of the Lost", 
                                                            80, "This is just another ordinary wild place.",
                                                            false, -1, Effects.Wild10Effect);
        public static Dungeon YawnDungeon02 = new Dungeon(2, "002DR", "Yawning Tunnels", 80, 
                                                            "There are some caverns previously weathered by rivers and wind.");
        public static Dungeon DesolatedDungeon03 = new Dungeon(3, "003DR", "Desolated Quarters", 80,
                                                            "This town quarter is not guarded anymore due to unknown sickness raged before.");
        public static Dungeon IronbarkDungeon04 = new Dungeon(4, "004DS", "Ironbark Point", 100,
                                                            "Past time it was a local robbers meeting place.", 
                                                            false, 4);

        public static List<Dungeon> DungeonList = new List<Dungeon>();

        public static List<string> DungeonIdList = new List<string>();

        public static List<Dungeon> DungeonListBuilder(params Dungeon[] dungeons)
        {
            foreach (var dungeon in dungeons)
            {
                DungeonList.Add(dungeon);
            }

            return DungeonList;
        }

        public static List<Dungeon> StorylineDungeonListBuilder(params Dungeon[] dungeons)
        {
            foreach (var dungeon in dungeons)
            {
                if (dungeon.DungeonId.Contains('S'))
                {
                    DungeonList.Add(dungeon);
                }
            }

            return DungeonList;
        }

        public static List<Dungeon> RandomDungeonListBuilder(params Dungeon[] dungeons)
        {
            foreach (var dungeon in dungeons)
            {
                if (dungeon.DungeonId.Contains('R'))
                {
                    DungeonList.Add(dungeon);
                }
            }

            return DungeonList;
        }

        public static List<string> DungeonIdListBuilder(params Dungeon[] dungeons)
        {
            foreach (var dungeon in dungeons)
            {
                DungeonIdList.Add(dungeon.DungeonId);
            }

            return DungeonIdList;
        }

        public static Dungeon GetDungeonById(string dungeonId)
        {
            foreach (var dungeon in DungeonList)
            {
                if (dungeon.DungeonId == dungeonId)
                    return dungeon;
            }

            //throw new Exception($"Unknown DungeonId:{dungeonId} was transferred!");
            return DefaultDungeon;
        }

        public static List<Dungeon> GetDungeonListByEncounterChance(List<Dungeon> dungeonList, int encChance)
        {
            var dungeonListByEncChance = new List<Dungeon>();
            foreach (var dungeon in DungeonList)
            {
                if (dungeon.DungeonEncounterChance != encChance)
                {
                    dungeonListByEncChance.Add(dungeon);
                }
            }

            return dungeonListByEncChance;
        }

        public static Dungeon GetDungeonListByTrueEncounterSerial(List<Dungeon> dungeonList, int trueEncSerial)
        {
            foreach (var dungeon in DungeonList)
            {
                if (dungeon.DungeonTrueEncounterSerial == trueEncSerial)
                {
                    return dungeon;
                }
            }

            //throw new Exception("Unknown Dungeon was transferred!");
            return DefaultDungeon;
        }

        public static Dungeon CurrentDungeon()
        {
            return GetDungeonById(Dungeon.LastDungeonId);
        }

        public static Dungeon GetUniqueRandomDungeon(List<Dungeon> dungeonList)
        {
            var counter = 0;
            var randomDungeon80 = dungeonList[Utils.GetRandom(1, dungeonList.Count) + 1];
            while (randomDungeon80.WasPicked)
            {
                randomDungeon80 = dungeonList[counter];
                if (counter > dungeonList.Count)
                    return DefaultDungeon;
                counter++;
            }

            return randomDungeon80;
        }
    }

    public class DungeonProcessor
    {
        public static Dungeon RandomizeDungeons(int random, int trueEncounterSerial = -1)
        {
            bool isBlocked80 = false;

            if (trueEncounterSerial >= 0)
            {
                var serialDungeon = Dungeons.GetDungeonListByTrueEncounterSerial(Dungeons.DungeonList, trueEncounterSerial);
                Dungeon.LastDungeonId = serialDungeon.DungeonId;
                return serialDungeon;
            }
            if (random <= 100 && !isBlocked80) //<=80 TODO
            {
                var dungeons80 = Dungeons.GetDungeonListByEncounterChance(Dungeons.DungeonList, 80);
                var randomizedDungeon80 = Dungeons.GetUniqueRandomDungeon(dungeons80);
                if (randomizedDungeon80 == Dungeons.DefaultDungeon)
                {
                    isBlocked80 = true;
                    RandomizeDungeons(Utils.GetRandom(1, 101));
                    return Dungeons.CurrentDungeon();
                }

                Dungeon.LastDungeonId = randomizedDungeon80.DungeonId;
                return randomizedDungeon80;
            }

            return Dungeons.CurrentDungeon();
        }

        public static void DungeonInitializer()
        {
            Dungeons.DungeonListBuilder();

            Dungeons.DungeonIdListBuilder();

            Dungeons.StorylineDungeonListBuilder();

            Dungeons.RandomDungeonListBuilder();
        }
    }
}
