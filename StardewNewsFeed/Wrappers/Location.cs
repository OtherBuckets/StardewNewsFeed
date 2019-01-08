using System;
using System.Linq;
using StardewValley;
using StardewModdingAPI;
using System.Collections.Generic;

namespace StardewNewsFeed.Wrappers {
    public class Location : ILocation {

        private readonly GameLocation _gameLocation;
        private readonly ITranslationHelper _translationHelper;

        public Location(GameLocation gameLocation, ITranslationHelper translationHelper) {
            _gameLocation = gameLocation ?? throw new ArgumentNullException(nameof(gameLocation));
            _translationHelper = translationHelper ?? throw new ArgumentNullException(nameof(translationHelper));
        }

        public string GetLocationName() {
            return _gameLocation.Name;
        }

        public string GetDisplayName() {
            if (_gameLocation.Name == Constants.FARM_CAVE_LOCATION_NAME) {
                return _translationHelper.Get("news-feed.cave-display-name");
            }
            return _gameLocation.Name;
        }

        public IStardewObject GetObjectAtTile(int height, int width) {
            return new StardewObject(_gameLocation.getObjectAtTile(height, width));
        }

        public IList<IStardewObject> GetObjects() {
            return _gameLocation.Objects.Values.Select(o => new StardewObject(o) as IStardewObject).ToList();
        }

        public IList<ITerrainFeature> GetTerrainFeatures() {

            // TODO Implementation

            return new List<ITerrainFeature>();
        }

        public bool IsGreenhouse() {
            return _gameLocation.IsGreenhouse;
        }

        public IList<NPC> GetCharacters() {
            var npcs = _gameLocation.getCharacters().Select(c => new NPC(c)).ToList();
            return npcs;
        }

    }
}
