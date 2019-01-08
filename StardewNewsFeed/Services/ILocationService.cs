using StardewNewsFeed.Wrappers;

namespace StardewNewsFeed.Services {
    public interface ILocationService {
        ILocation GetLocationByName(string locationName);
        ILocation GetGreenhouse();
        int GetNumberOfHarvestableObjects(ILocation gameLocation);
        int GetNumberOfHarvestableTerrainFeatures(ILocation gameLocation);
    }
}
