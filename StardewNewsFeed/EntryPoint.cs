﻿using System.Linq;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.TerrainFeatures;

namespace StardewNewsFeed {
    public class EntryPoint : Mod {

        #region Private Properties
        private ModConfig _modConfig;
        private const string FARM_CAVE_LOCATION_NAME = "FarmCave";
        private const string CELLAR_LOCATION_NAME = "Cellar";
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
                helper.Events.GameLoop.DayStarted += CheckBirthdays;
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
            var farmCave = Game1.getLocationFromName(FARM_CAVE_LOCATION_NAME);
            Log($"Player Cave Choice: {Game1.player.caveChoice}");
            if(Game1.player.caveChoice == 2) {
                CheckLocationForHarvestableObjects(farmCave);
            } else {
                ScanLocationForFruit(farmCave);
            }

        }

        private void CheckGreenhouse(object sender, DayStartedEventArgs e) {
            var greenhouse = Game1.locations.SingleOrDefault(l => l.isGreenhouse);
            CheckLocationForHarvestableTerrain(greenhouse);
        }

        private void CheckCellar(object sender, DayStartedEventArgs e) {
            var cellar = Game1.getLocationFromName(CELLAR_LOCATION_NAME);
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

        private void CheckBirthdays(object sender, DayStartedEventArgs e) {
            foreach (var npc in Game1.currentLocation.getCharacters()) {
                Log($"Checking {npc.displayName} for a birthday today");
                if (npc.isBirthday(Game1.Date.Season, Game1.Date.DayOfMonth)) {
                    Game1.addHUDMessage(new HUDMessage($"Today is {npc.getName()}'s birthday. Don't forget to give them a gift", 2));
                }
            }
        }

        private void CheckLocationForHarvestableObjects(GameLocation location) {
            var itemsReadyForHarvest = location.Objects.Values.Where(o => o.readyForHarvest);

            if (itemsReadyForHarvest.Any()) {
                Game1.addHUDMessage(new HUDMessage($"There are {itemsReadyForHarvest.Count()} items ready for harvesting in the {location.Name}", 2));
                Log($"{itemsReadyForHarvest.Count()} items found in the {location.Name}");
            } else {
                Log($"No items found in the {location.Name}");
            }
        }

        private void CheckLocationForHarvestableTerrain(GameLocation location) {
            var hoeDirtReadyForHavest = location.terrainFeatures.Pairs
                .Where(p => p.Value is HoeDirt)
                .Select(p => p.Value as HoeDirt)
                .Where(hd => hd.readyForHarvest());

            if(hoeDirtReadyForHavest.Any()) {
                Game1.addHUDMessage(new HUDMessage($"There are {hoeDirtReadyForHavest.Count()} items ready for harvest in the {location.name}."));
            } else {
                Log($"No items found in the {location.name}");
            }
        }

        private void ScanLocationForFruit(GameLocation location) {
            for (int height = 4; height < 9; height++) {
                for (int width = 2; width < 11; width++) {
                    if (TileIsHarvestable(location, height, width)) {
                        Game1.addHUDMessage(new HUDMessage("The Cave has items today!", 2));
                        return;
                    }
                }
            }
        }

        private bool TileIsHarvestable(GameLocation location, int height, int width) {
            var tile = location.getObjectAtTile(height, width);
            if (tile == null) {
                return false;
            }

            return tile.readyForHarvest;
        }
        #endregion

    }
}
