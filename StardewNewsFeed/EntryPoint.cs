using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;

namespace StardewNewsFeed {
    public class EntryPoint : Mod {

        #region Private Properties
        private ModConfig _modConfig;
        #endregion

        #region StardewModdingApi.Mod Overrides
        public override void Entry(IModHelper helper) {
            _modConfig = Helper.ReadConfig<ModConfig>();

            if(_modConfig.CaveNotificationsEnabled) {
                helper.Events.GameLoop.DayStarted += CheckFarmCave;
            }

            if(_modConfig.GreenhouseNotificationsEnabled) {
                helper.Events.GameLoop.DayStarted += CheckGreenhouse;
            }

            if(_modConfig.CellarNotificationsEnabled) {
                helper.Events.GameLoop.DayStarted += CheckCellar;
            }

            if(_modConfig.ShedNotificationsEnabled) {
                helper.Events.GameLoop.DayStarted += CheckSheds;
            }

            if (_modConfig.BirthdayCheckEnabled) {
                helper.Events.Player.Warped += CheckBirthdays;
            }
        }
        #endregion

        #region Private Methods
        private void Log(string message) {
            if(_modConfig.DebugMode) {
                Monitor.Log(message);
            }
        }

        private void CheckFarmCave(object sender, DayStartedEventArgs e) {
            var farmCave = Game1.getLocationFromName(Constants.FARM_CAVE_LOCATION_NAME);
            Log($"Player Cave Choice: {Game1.player.caveChoice}");
            if(Game1.player.caveChoice == 2) {
                CheckLocationForHarvestableObjects(farmCave);
            } else {
                //ScanLocationForFruit(farmCave);
                CheckLocationForHarvestableObjects(farmCave);
            }

        }

        private void CheckGreenhouse(object sender, DayStartedEventArgs e) {
            var greenhouse = Game1.locations.SingleOrDefault(l => l.isGreenhouse);
            CheckLocationForHarvestableTerrain(greenhouse);
        }

        private void CheckCellar(object sender, DayStartedEventArgs e) {
            var cellar = Game1.getLocationFromName(Constants.CELLAR_LOCATION_NAME);
            CheckLocationForHarvestableObjects(cellar);
        }

        private void CheckSheds(object sender, DayStartedEventArgs e) {
            var sheds = Game1.getFarm()
                .buildings
                .Select(b => b.indoors.Value)
                .Where(i => i is Shed);

            foreach(var shed in sheds) {
                CheckLocationForHarvestableObjects(shed);
            }
        }

        private void CheckBirthdays(object sender, WarpedEventArgs e) {
            Log("Checking for birthdays: ");
            foreach(var npc in e.NewLocation.getCharacters()) {
                Log($"Checking {npc.displayName} for a birthday today");
                if (npc.isBirthday(Game1.Date.Season, Game1.Date.DayOfMonth)) {
                    Game1.addHUDMessage(new HUDMessage($"Today is {npc.getName()}'s birthday. Don't forget to give them a gift", 2));
                }
            }
        }

        private void CheckLocationForHarvestableObjects(GameLocation location) {
            var itemsReadyForHarvest = location.Objects.Values.Where(o => o.readyForHarvest);

            if (itemsReadyForHarvest.Any()) {
                Game1.addHUDMessage(new HUDMessage($"{itemsReadyForHarvest.Count()} item(s) ready for harvesting in the {location.getDisplayName()}", 2));
                Log($"{itemsReadyForHarvest.Count()} item(s) found in the {location.getDisplayName()}");
            } else {
                Log($"No items found in the {location.getDisplayName()}");
            }
        }

        private void CheckLocationForHarvestableTerrain(GameLocation location) {
            var hoeDirtReadyForHavest = location.terrainFeatures.Pairs
                .Where(p => p.Value is HoeDirt)
                .Select(p => p.Value as HoeDirt)
                .Where(hd => hd.readyForHarvest());

            if(hoeDirtReadyForHavest.Any()) {
                Game1.addHUDMessage(new HUDMessage($"{hoeDirtReadyForHavest.Count()} item(s) ready for harvest in the {location.getDisplayName()}."));
            } else {
                Log($"No items found in the {location.getDisplayName()}");
            }
        }

        //private void ScanLocationForFruit(GameLocation location) {
        //    var tiles = new List<Object>();
        //    for (int height = 4; height < 9; height++) {
        //        for (int width = 2; width < 11; width++) {
        //            if (location.tileIsReadyForHarvest(height, width)) {
        //                tiles.Add(location.getObjectAtTile(height, width));
        //            }
        //        }
        //    }
        //    if(tiles.Any()) {
        //        Game1.addHUDMessage(new HUDMessage($"The bats have brought you {tiles.Count()} pieces of fruit!", 2));
        //        return;
        //    }
        //}
        #endregion

    }
}
