using System;
using StardewModdingAPI;

namespace StardewNewsFeed {
    public class EntryPoint : Mod {

        private ModConfig _modConfig;

        public override void Entry(IModHelper helper) {
            _modConfig = Helper.ReadConfig<ModConfig>();
        }

    }
}
