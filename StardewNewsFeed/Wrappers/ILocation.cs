using System.Collections.Generic;

namespace StardewNewsFeed.Wrappers {
    /// <summary>
    /// Wrapper for StardewValley.GameLocation
    /// </summary>
    public interface ILocation {

        string GetDisplayName();
        string GetLocationName();
        IList<IStardewObject> GetObjects();
        IStardewObject GetObjectAtTile(int height, int width);
        IList<ITerrainFeature> GetTerrainFeatures();
        bool IsGreenhouse();
        IList<NPC> GetCharacters();

    }
}
