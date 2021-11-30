using System;
using System.Collections.Generic;
using System.Text;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GameData
{
    public class Location
    {
        public int SimpleLocationId { get; }
        // ## of implementation
        // L code
        // location type (plot/random/dimension etc)
        public string LocationId { get; }
        public string LocationShortName { get; }
        public string LocationName { get; }
        public Dictionary<string, Monster> LocationMonsters { get; }
        public Town LocationTown { get; }
        public Dictionary<string, Artifact> LocationArtifacts { get; }
        public Dictionary<string, Potion> LocationPotions { get; }
        //public Dictionary<string, Quest> LocationQuests { get; } //TODO

        public static string BaseIdFormat = "####";

        public Location(int simpleLocationId, string locationId, string locationShortName, string locationName,
                        Dictionary<string, Monster> locationMonsters, Town locationTown,
                        Dictionary<string, Artifact> locationArtifacts, Dictionary<string, Potion> locationPotions)
        {
            SimpleLocationId = simpleLocationId;
            LocationId = locationId;
            LocationShortName = locationShortName;
            LocationName = locationName;
            LocationMonsters = locationMonsters;
            LocationTown = locationTown;
            LocationArtifacts = locationArtifacts;
            LocationPotions = locationPotions;
        }

        public Location(string locationId, string locationName)
        {
            LocationId = locationId;
            LocationName = locationName;
        }
    }

    public class Locations
    {
        public static Dictionary<string, Monster> MonsterDictionaryBuilder(string locationShortName)
        {
            var locationMonsters = new Dictionary<string, Monster>();
            var monsters = Monsters.MonsterList;
            foreach (var monster in monsters)
            {
                if(monster.MonsterLocation == locationShortName)
                    locationMonsters.Add(monster.MonsterName, monster);
            }

            return locationMonsters;
        }

        public static Dictionary<string, Artifact> ArtifactDictionaryBuilder(string locationShortName)
        {
            var locationArtifacts = new Dictionary<string, Artifact>();
            var artifacts = Artifacts.ArtifactList;
            foreach (var artifact in artifacts)
            {
                if (artifact.ArtifactLocation == locationShortName || artifact.ArtifactLocation == "everywhere")
                    locationArtifacts.Add(artifact.ArtifactName, artifact);
            }

            return locationArtifacts;
        }

        public static Dictionary<string, Potion> PotionDictionaryBuilder(string locationShortName)
        {
            var locationPotions = new Dictionary<string, Potion>();
            var potions = Potions.PotionList;
            foreach (var potion in potions)
            {
                if (potion.PotionLocation == locationShortName || potion.PotionLocation == "everywhere")
                    locationPotions.Add(potion.PotionName, potion);
            }

            return locationPotions;
        }

        public static Town LocationTownSelector(int serialVisitNumber = -1)
        {
            switch (serialVisitNumber)
            {
                case 0:
                    return TownProcessor.RandomizeTowns(0, 0);
                case 4:
                    return TownProcessor.RandomizeTowns(0, 4);
                default:
                    return TownProcessor.RandomizeTowns(GetRandom(101));
            }
        }

        public static Location DefaultLocation = new Location("00LN", "Default");

        public static Location TuvaleLocation = new Location(
            1,
            "01LP",
            "tuvale",
            "Tuvale Region",
            MonsterDictionaryBuilder("tuvale"),
            LocationTownSelector(0),
            ArtifactDictionaryBuilder("tuvale"),
            PotionDictionaryBuilder("tuvale"));

        public static List<Location> LocationList = new List<Location>();

        public static List<string> LocationIdList = new List<string>();

        public static List<Location> LocationListBuilder(params Location[] locations)
        {
            foreach (var location in locations)
            {
                LocationList.Add(location);
            }

            return LocationList;
        }

        public static List<string> LocationIdListBuilder(params Location[] locations)
        {
            foreach (var location in locations)
            {
                LocationIdList.Add(location.LocationId);
            }

            return LocationIdList;
        }

        public static Location GetLocationById(string locationId)
        {
            foreach (var location in LocationList)
            {
                if (location.LocationId == locationId)
                    return location;
            }

            //throw new Exception($"Unknown LocationId:{locationId} was transferred!");
            return DefaultLocation;
        }
    }

    public class LocationProcessor
    {
        public static void LocationInitializer()
        {
            Locations.LocationListBuilder(
                Locations.DefaultLocation,
                Locations.TuvaleLocation);

            Locations.LocationIdListBuilder(
                Locations.DefaultLocation,
                Locations.TuvaleLocation);
        }
    }
}
