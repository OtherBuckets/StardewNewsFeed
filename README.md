# StardewNewsFeed
Morning Notifications for Stardew Valley

This Stardew Valley Mod inspects various locations every morning for harvestable items. Any locations with harvestable items found will generate a notification to the player.

## Currently Supported Locations
* Farm Cave
* Greenhouse

## Upcoming Locations
* Cellar
* Barns

## How to configure this mod
The install directory for this mod will contain a configuration file. ~/Stardew Valley/Contents/Mods/StardewNewsFeed/config.json

By default, scanning for items in the Farm Cave and the Greenhouse will be turned on. The game will need to be restarted after making adjustments to the configuration.

|Config Property|Description|
|-|-|
|DebugMode|Debug mode|
|GreenhouseNotificationsEnabled|Enables/Disables scanning and notifications for the greenhouse|
|GreenhouseDirtOnlyMode|Not yet implemented, will be a performance option|
|CaveNotifications|Enables/Disables scanning and notifications for the farm cave|
|CaveMushroomMode|Setting this to true will only scan the 6 mushroom boxes instead of scanning the entire cave. Will improve performance.|
|CellarNotificationsEnabled|The Cellar is still a work in progress, so this does nothing|

## Plans for additional future updates
Notifications for npc birthdays
A persistent checklist containing areas with harvestable items
