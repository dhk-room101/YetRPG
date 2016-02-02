# YetRPG
this is actually an uninspired name ('yet another RPG') for an attempt to port Dragon Age 1/2 scripting language and eventually the backend engine extrapolation to Unity Engine. 

some sort of dev log here, for more specific updates: http://forum.unity3d.com/threads/dragon-age-1-2-engine-porting-to-unity.355270/

basically the original NSS files were updated to C# syntax, and LDF directives were extrapolated to fit Unity/C#.
The current functionality includes: parsing the original XML files and creating placeables and creatures: such as player, NPC, waypoints and add them in the scene at position/rotation as defined in the original Demo Module from Dragon Age Toolset
as in the original game, each new entity gets its script attached to it, i.e. creatures use creature_core script file, player uses player_core, area uses area_core, etc., or they can all get a custom script

For example: a new NPC has custom_script as its starting point to handle events, and if there was an event not handled in its custom script, then it gets automatically redirected to creature_core, and if once again the event doesn't get processed in creature_core, then it gets redirected to rules_core, following the exact same pattern as the original Dragon Age developers…

take a look at the picture below, it will give you a better idea of what I have in mind. My goal is to focus on functionality exclusively, content/how things look is not the priority. The final goal is to have a ready-made RPG framework in Unity

![Alt text](https://raw.githubusercontent.com/dhk-room101/YetRPG/master/Assets/Resources/Images/yet.jpg)

color codes for the picture
blue sphere: player
Gray sphere: neutral NPC
magenta cones: placeable
yellow cubes: waypoint

All of these objects were created on the fly and repositioned according to the information parsed from the original Demo Module from Dragon Age Toolset.

To do: expand AI, and changing levels, eventually some dialogue implementation

changes history:

09/01/15: porting of *_core and *_h files done 

01/21/16: parsing the XML files from Demo Module from Dragon Age Toolset

01/31/16: dialogue parser completed, basic game states changes, and other small tweaks