
using StardewValley;
using Netcode;

namespace StardewNewsFeed {
    public static class Extensions {

        public static string getDisplayName(this GameLocation @this) {
            if(@this.name == Constants.FARM_CAVE_LOCATION_NAME) {
                return "Farm Cave";
            }
            return @this.name;
        }

        public static NetBool tileIsReadyForHarvest(this GameLocation @this, int height, int width) {
            var objectAtLocation = @this.getObjectAtTile(height, width);
            if(objectAtLocation == null) {
                return new NetBool(false);
            }
            return objectAtLocation.readyForHarvest;
        }

    }
}
