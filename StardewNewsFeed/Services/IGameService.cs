using StardewNewsFeed.Wrappers;

namespace StardewNewsFeed.Services {
    public interface IGameService {
        void CheckFarmCave();
        void CheckGreenhouse();
        void CheckCellar();
        void CheckSheds();
        void CheckLocationForBirthdays(ILocation location);
    }
}
