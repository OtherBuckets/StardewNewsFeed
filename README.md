# StardewNewsFeed
Morning Notifications for Stardew Valley

This Stardew Valley Mod inspects various locations every morning for harvestable items. Any locations with harvestable items found will generate a notification to the player.

## Nexus Mods Page
https://www.nexusmods.com/stardewvalley/mods/3206/

## Currently Supported Locations
* Farm Cave
* Greenhouse

## Upcoming Locations
* Cellar
* Barns

## How to configure this mod
The install directory for this mod will contain a configuration file. ~/Stardew Valley/Contents/Mods/StardewNewsFeed/config.json

By default, scanning for items in the Farm Cave and the Greenhouse will be turned on. The game will need to be restarted after making adjustments to the configuration.

|Config Property|Description|Default Value|
|-|-|-|
|DebugMode|Prints debug info to the console|false|
|GreenhouseNotificationsEnabled|Enables/Disables scanning and notifications for the greenhouse|true|
|GreenhouseDirtOnlyMode|Not yet implemented, will be a performance option|false|
|CaveNotifications|Enables/Disables scanning and notifications for the farm cave|true|
|CaveMushroomMode|Setting this to true will only scan the 6 mushroom boxes instead of scanning the entire cave. Will improve performance.|false|
|CellarNotificationsEnabled|The Cellar is still a work in progress, so this does nothing|false|

## Plans for additional future updates
Notifications for npc birthdays
A persistent checklist containing areas with harvestable items
