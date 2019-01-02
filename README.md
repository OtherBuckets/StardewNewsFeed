# StardewNewsFeed
Morning Notifications for Stardew Valley

This Stardew Valley Mod inspects various locations every morning for harvestable items. Any locations with harvestable items found will generate a notification to the player.

## Nexus Mods Page
https://www.nexusmods.com/stardewvalley/mods/3206/

## Currently Supported Locations (1.1)
* Farm Cave Notifications
* Greenhouse Notifications
* Cellar Notifications
* Shed Notifications

## How to configure this mod
The install directory for this mod will contain a configuration file. ~/Stardew Valley/Contents/Mods/StardewNewsFeed/config.json

By default, scanning for items in the Farm Cave and the Greenhouse will be turned on. The game will need to be restarted after making adjustments to the configuration.

|Config Property|Description|Default Value|
|-|-|-|
|DebugMode|Prints debug info to the console|false|
|GreenhouseNotificationsEnabled|Enables/Disables scanning and notifications for the greenhouse|true|
|CaveNotifications|Enables/Disables scanning and notifications for the farm cave|true|
|CellarNotificationsEnabled|Enables/Disables scanning and notifications for the cellar|false|
|ShedNotificationsEnabled|Enables/Disabled scanning and notifications for sheds|false|

## Plans for additional future updates
Notifications for npc birthdays
A persistent checklist containing areas with harvestable items
