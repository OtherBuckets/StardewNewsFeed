using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

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
            CheckLocationForHarvestableItems(farmCave);
        }

        private void CheckGreenhouse(object sender, DayStartedEventArgs e) {
            var greenhouse = Game1.locations.SingleOrDefault(l => l.isGreenhouse);
            CheckLocationForHarvestableItems(greenhouse);
        }

        private void CheckCellar(object sender, DayStartedEventArgs e) {
            var cellar = Game1.getLocationFromName(CELLAR_LOCATION_NAME);
            CheckLocationForHarvestableItems(cellar);
        }

        private void CheckSheds(object sender, DayStartedEventArgs e) {
            var sheds = Game1.getFarm()
                .buildings
                .Select(b => b.indoors.Value)
                .Where(i => i is Shed);

            foreach(var shed in sheds) {
                CheckLocationForHarvestableItems(shed);
            }
        }

        private void CheckLocationForHarvestableItems(GameLocation location) {
            var itemsReadyForHarvest = location.Objects.Values.Where(o => o.readyForHarvest);

            if (itemsReadyForHarvest.Any()) {
                Game1.addHUDMessage(new HUDMessage($"There are {itemsReadyForHarvest.Count()} items ready for harvesting in the {location.Name}", 2));
                Log($"{itemsReadyForHarvest.Count()} items found in the {location.Name}");
            } else {
                Log($"No items found in the {location.Name}");
            }
        }
        #endregion

    }
}
