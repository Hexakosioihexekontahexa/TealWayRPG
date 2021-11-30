using System;
using System.Collections.Generic;

namespace Teal_Way_RPG.GameData
{
    public class Monster
    {
        public int SimpleMonsterId { get; }
        // ### of implementation
        // M code
        // ?? code of location
        // id of appearance in location
        public string MonsterId { get; }
        public string MonsterName { get; }
        public int MonsterHp { get; }
        public int MonsterMinDmg { get; }
        public int MonsterMaxDmg { get; }
        public int GoldBounty { get; }
        public int ExpBounty { get; }
        public string MonsterLocation { get; }
        public int EncounterChance { get; }
        public Effect MonsterEffect { get; }

        public static string BaseIdFormat = "########";

        public Monster(int simpleMonsterId, string monsterId, string monsterName, string monsterLocation, int monsterHp,
            int monsterMinDmg, int monsterMaxDmg, int goldBounty,
            int expBounty, int encounterChance, Effect monsterEffect = null)
        {
            MonsterId = monsterId;
            SimpleMonsterId = simpleMonsterId;
            MonsterName = monsterName;
            MonsterHp = monsterHp;
            MonsterMinDmg = monsterMinDmg;
            MonsterMaxDmg = monsterMaxDmg;
            GoldBounty = goldBounty;
            ExpBounty = expBounty;
            MonsterLocation = monsterLocation;
            EncounterChance = encounterChance;
            MonsterEffect = monsterEffect;
        }

        public Monster(string monsterId, string monsterName)
        {
            MonsterId = monsterId;
            MonsterName = monsterName;
        }
    }

    public class Monsters
    {
        public static Monster DefaultMonster00 = new Monster("000MNN00", "Default");

        #region Tuvale Region
        public static Monster AngryWolfMonster01 = new Monster(1,"001MAD01", "Angry Wolf", "tuvale", 
                                                      5, 1, 3, 3, 3, 25, 
                                                      Effects.RuptureEffect);
        
        public static Monster WildBoarMonster02 = new Monster(2, "002MAD02", "Wild Boar", "tuvale", 
                                                     10, 2, 4, 5, 5, 25);
        public static Monster StoneGolemMonster03 = new Monster(3, "003MAD03", "Stone Golem", "tuvale", 
                                                       15, 4, 7, 3, 8, 15);
        public static Monster VenomSnakeMonster04 = new Monster(4, "004MAD04", "Venom Snake", "tuvale", 
                                                       10, 2, 5,5,5, 15, 
                                                       Effects.VenomStrikeEffect);
        public static Monster GoblinWarriorMonster05 = new Monster(5, "005MAD05", "Goblin Warrior", "tuvale",
                                                          20, 5, 9, 8, 10, 8);
        public static Monster GoblinMageMonster06 = new Monster(6, "006MAD06", "Goblin Mage", "tuvale", 
                                                       15, 7, 10, 10, 8, 7);
        public static Monster GiantSpiderMonster07 = new Monster(7, "007MAD07", "Giant Spider", "tuvale", 
                                                        25, 4, 11, 25, 20, 5,
                                                        Effects.VenomStrikeEffect);
        #endregion

        #region Tuvale Special
        public static Monster ShopRobberMonster08 = new Monster(8, "008MST01", "Shop Robber", "special01", 
                                                       40, 15, 25, 100, 35, 100,
                                                       Effects.VenomStrikeEffect);
        #endregion

        public static List<Monster> MonsterList = new List<Monster>();

        public static List<string> MonsterIdList = new List<string>();

        public static List<Monster> MonsterListBuilder(params Monster[] monsters)
        {
            foreach (var monster in monsters)
            {
                MonsterList.Add(monster);
            }
            return MonsterList;

        }

        public static List<string> MonsterIdListBuilder(params Monster[] monsters)
        {
            foreach (var monster in monsters)
            {
                MonsterIdList.Add(monster.MonsterId);
            }
            return MonsterIdList;

        }

        public static Monster GetMonsterById(string monsterId)
        {
            foreach (var monster in MonsterList)
            {
                if (monster.MonsterId == monsterId)
                    return monster;
            }

            //throw new Exception($"Unknown MonsterId:{monsterId} was transferred!");
            return DefaultMonster00;
        }

        public static List<Monster> GetMonsterListByLocation(List<Monster> monsterList, string location)
        {
            List<Monster> listByLocation = new List<Monster>();
            foreach (var monster in monsterList)
            {
                if (monster.MonsterLocation == location)
                {
                    listByLocation.Add(monster);
                }
            }

            return listByLocation;
        }
    }

    public class MonsterProcessor
    {
        public static Monster RandomizeMonster(string location)
        {
            var listByLocation = Monsters.GetMonsterListByLocation(Monsters.MonsterList, location);
            string id;
            switch(location)
            {
                case "tuvale":
                    var encounterChance = Utils.GetRandom(1, 101);
                        // 1 <= Y <= 25
                    if (encounterChance <= listByLocation[0].EncounterChance)
                    {
                        id = listByLocation[0].MonsterId;
                        return Monsters.GetMonsterById(id);
                    }
                    else if (listByLocation[0].EncounterChance < encounterChance &&
                             encounterChance <= 
                             listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance) 
                        // 25 < Y <= 50
                    {
                        id = listByLocation[1].MonsterId;
                        return Monsters.GetMonsterById(id);
                    }
                    else if (listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance < 
                             encounterChance &&
                             encounterChance <= 
                             listByLocation[2].EncounterChance +
                             listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance) 
                        // 50 < Y <= 65
                    {
                        id = listByLocation[2].MonsterId;
                        return Monsters.GetMonsterById(id);
                    }
                    else if (listByLocation[2].EncounterChance +
                             listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance < 
                             encounterChance &&
                             encounterChance <= 
                             listByLocation[3].EncounterChance +
                             listByLocation[2].EncounterChance +
                             listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance) 
                        // 65 < Y <= 80
                    {
                        id = listByLocation[3].MonsterId;
                        return Monsters.GetMonsterById(id);
                    }
                    else if (listByLocation[3].EncounterChance +
                             listByLocation[2].EncounterChance +
                             listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance < 
                             encounterChance &&
                             encounterChance <= 
                             listByLocation[4].EncounterChance +
                             listByLocation[3].EncounterChance +
                             listByLocation[2].EncounterChance +
                             listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance) 
                        // 80 < Y <= 88
                    {
                        id = listByLocation[4].MonsterId;
                        return Monsters.GetMonsterById(id);
                    }
                    else if (listByLocation[4].EncounterChance +
                             listByLocation[3].EncounterChance +
                             listByLocation[2].EncounterChance +
                             listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance < 
                             encounterChance &&
                             encounterChance <= 
                             listByLocation[5].EncounterChance +
                             listByLocation[4].EncounterChance +
                             listByLocation[3].EncounterChance +
                             listByLocation[2].EncounterChance +
                             listByLocation[1].EncounterChance +
                             listByLocation[0].EncounterChance) 
                        // 88 < Y <= 95
                    {
                        id = listByLocation[5].MonsterId;
                        return Monsters.GetMonsterById(id);
                    }
                    else if (100 - listByLocation[6].EncounterChance < 
                             encounterChance &&
                             encounterChance <= 100) 
                        // 95 < Y <= 100
                    {
                        id = listByLocation[6].MonsterId;
                        return Monsters.GetMonsterById(id);
                    }
                    break;
                case "special01":
                    id = Monsters.ShopRobberMonster08.MonsterId;
                    return Monsters.GetMonsterById(id);
                default:
                    throw new Exception($"Location: {location} is out of known cases!");
            }

            throw new Exception("Random value is out of bounds!");
        }

        public static void MonsterInitializer()
        {
            Monsters.MonsterListBuilder(
                Monsters.DefaultMonster00,
                Monsters.AngryWolfMonster01,
                Monsters.WildBoarMonster02,
                Monsters.StoneGolemMonster03,
                Monsters.VenomSnakeMonster04,
                Monsters.GoblinWarriorMonster05,
                Monsters.GoblinMageMonster06,
                Monsters.GiantSpiderMonster07,
                Monsters.ShopRobberMonster08);

            Monsters.MonsterIdListBuilder(
                Monsters.DefaultMonster00,
                Monsters.AngryWolfMonster01,
                Monsters.WildBoarMonster02,
                Monsters.StoneGolemMonster03,
                Monsters.VenomSnakeMonster04,
                Monsters.GoblinWarriorMonster05,
                Monsters.GoblinMageMonster06,
                Monsters.GiantSpiderMonster07,
                Monsters.ShopRobberMonster08);
        }
    }
}
