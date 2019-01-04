
using StardewValley;

namespace StardewNewsFeed {
    public static class Extensions {

        public static string getDisplayName(this GameLocation @this) {
            if(@this.Name == Constants.FARM_CAVE_LOCATION_NAME) {
                return "Farm Cave";
            }
            return @this.Name;
        }

    }
}
