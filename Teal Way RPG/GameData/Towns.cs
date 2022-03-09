using System;
using System.Collections.Generic;

namespace Teal_Way_RPG.GameData
{
    public class Town
    {
        /*Implement either: random chain of towns (traveling A-B-C)
        /         or: pseudo-random chain of towns with randomization of 2 variables and their occasional equality
        /         (so traveling A-B(C)-D; B-C; A-D is possible (user can choose 1 of 2 towns)) [preferable]
        /         or: hardcoded defined order of towns [non-preferable]
        */
        public int TownSimpleId;
        public string TownId; //## number of implementation
                              // T code
                              //? type (storyline of random)
        public string TownName;
        public int TownEncounterChance;
        public int TownTrueEncounterSerial; //allows to 100%-ly encounter town upon Traveling action; optional
        public Effect TownSpecialization; //optional
        public string TownDungeon;
        public bool WasPicked;
        public static string LastTownId;

        public static string BaseIdFormat = "#####";

        public Town(int townSimpleId, string townId, string townName, int townEncounterChance, bool wasPicked = false, int townTrueEncounterSerial = -1,
            Effect townSpecialization = null)
        {
            TownSimpleId = townSimpleId;
            TownId = townId;
            TownName = townName;
            TownEncounterChance = townEncounterChance;
            TownTrueEncounterSerial = townTrueEncounterSerial;
            TownSpecialization = townSpecialization;
            WasPicked = wasPicked;
        }

        public Town(string townId, string townName)
        {
            TownId = townId;
            TownName = townName;
        }
    }

    public class Towns
    {
        public static Town DefaultTown = new Town("000TD", "Default");
        public static Town GoldenleafTown00 = new Town(0, "000TS", "Goldenleaf", 100,
            false, 0); //Demotown
        public static Town SinashariTown01 = new Town(1, "001TR", "Sinashari", 80 );
        public static Town PitmerdenTown02 = new Town(2, "002TR", "Pitmerden", 80);
        public static Town IshnualaTown03 = new Town(3, "003TR", "Ishnuala", 80);
        public static Town GreenflowerTown04 = new Town(4, "004TS", "Greenflower", 100, false,
                                                     4, Effects.Discount10Effect);

        public static List<Town> TownList = new List<Town>();

        public static List<string> TownIdList = new List<string>();

        public static List<Town> TownListBuilder(params Town[] towns)
        {
            foreach (var town in towns)
            {
                TownList.Add(town);
            }

            return TownList;
        }

        public static List<string> TownIdListBuilder(params Town[] towns)
        {
            foreach (var town in towns)
            {
                TownIdList.Add(town.TownId);
            }

            return TownIdList;
        }

        public static Town GetTownById(string townId)
        {
            foreach (var town in TownList)
            {
                if (town.TownId == townId)
                    return town;
            }

            //throw new Exception($"Unknown TownId:{townId} was transferred!");
            return DefaultTown;
        }

        public static List<Town> GetTownListByEncounterChance(List<Town> townList, int encChance)
        {
            var townListByEncChance = new List<Town>();
            foreach (var town in TownList)
            {
                if (town.TownEncounterChance != encChance)
                {
                    townListByEncChance.Add(town);
                }
            }

            return townListByEncChance;
        }

        public static Town GetTownListByTrueEncounterSerial(List<Town> townList, int trueEncSerial)
        {
            foreach (var town in TownList)
            {
                if (town.TownTrueEncounterSerial == trueEncSerial)
                {
                    return town;
                }
            }

            //throw new Exception("Unknown Town was transferred!");
            return DefaultTown;
        }

        public static Town CurrentTown()
        {
            return GetTownById(Town.LastTownId);
        }

        public static Town GetUniqueRandomTown(List<Town> townList)
        {
            var counter = 0;
            var randomTown80 = townList[Utils.GetRandom(1, townList.Count) + 1];
            while (randomTown80.WasPicked)
            {
                randomTown80 = townList[counter];
                if (counter > townList.Count)
                    return DefaultTown;
                counter++;
            }

            return randomTown80;
        }
    }

    public class TownProcessor
    {
        public static Town RandomizeTowns(int random, int trueEncounterSerial = -1)
        {
            bool isBlocked80 = false;

            if (trueEncounterSerial >= 0)
            {
                var serialTown = Towns.GetTownListByTrueEncounterSerial(Towns.TownList, trueEncounterSerial);
                Town.LastTownId = serialTown.TownId;
                return serialTown;
            }
            if (random <= 100 && !isBlocked80) //<=80 TODO
            {
                var towns80 = Towns.GetTownListByEncounterChance(Towns.TownList, 80);
                var randomizedTown80 = Towns.GetUniqueRandomTown(towns80);
                if (randomizedTown80 == Towns.DefaultTown)
                {
                    isBlocked80 = true;
                    RandomizeTowns(Utils.GetRandom(1, 101));
                    return Towns.CurrentTown();
                }

                Town.LastTownId = randomizedTown80.TownId;
                return randomizedTown80;
            }

            return Towns.CurrentTown();
        }

        public static void TownInitializer()
        {
            Towns.TownListBuilder(
                Towns.DefaultTown,
                Towns.GoldenleafTown00,
                Towns.SinashariTown01,
                Towns.PitmerdenTown02,
                Towns.IshnualaTown03,
                Towns.GreenflowerTown04);

            Towns.TownIdListBuilder(
                Towns.DefaultTown,
                Towns.GoldenleafTown00,
                Towns.SinashariTown01,
                Towns.PitmerdenTown02,
                Towns.IshnualaTown03,
                Towns.GreenflowerTown04);
        }
    }
}
