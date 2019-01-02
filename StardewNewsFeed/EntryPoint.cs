using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace StardewNewsFeed {
    public class EntryPoint : Mod {

        #region Private Properties
        private ModConfig _modConfig;
        private const string FARM_CAVE_LOCATION_NAME = "FarmCave";
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

            // TODO: Celler
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

            if(_modConfig.CaveMushroomMode) {
                // In mushroom mode, we only need to check the 6 boxes
                var mushroomBoxLocations = new List<int[]> {
                    new [] {4,5},
                    new [] {4,7},
                    new [] {4,9},
                    new [] {6,5},
                    new [] {6,7},
                    new [] {6,9}
                };
                foreach (var location in mushroomBoxLocations) {
                    if (TileIsHarvestable(farmCave, location[0], location[1])) {
                        Game1.addHUDMessage(new HUDMessage("The Cave has items today!", 2));
                        return;
                    }
                }
            } else {
                // not in mushroom mode, so check all usable tiles, 4-8 and 2-10 are an educated guess
                for (int height = 4; height < 9; height++) {
                    for (int width = 2; width < 11; width++) {
                        if (TileIsHarvestable(farmCave, height, width)) {
                            Game1.addHUDMessage(new HUDMessage("The Cave has items today!", 2));
                            return;
                        }
                    }
                }
            }
            Log("No items found in the farm cave");
        }

        private void CheckGreenhouse(object sender, DayStartedEventArgs e) {
            var greenhouse = Game1.locations.SingleOrDefault(l => l.isGreenhouse);
            // TODO Handle Dirt Only Mode for efficiency
            for (int height = 0; height <= 20; height++) {
                for (int width = 0; width <= 20; width++) {
                    if (TileIsHarvestable(greenhouse, height, width)) {
                        Game1.addHUDMessage(new HUDMessage("The Greenhouse has items today!", 2));
                        return;
                    }
                }
            }
            Log("No items found in the greenhouse");
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
