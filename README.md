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

conversation parser is done, it includes: correctly parsing NPC and player lines, correctly exchanging the lines in the dialogue flow, correctly analyzing conditions and actions and setting them properly, correctly highlight the hovered over line. visually the dialogue follows Dragon age 2 and Dragon age Inquisition dialogue wheel. Most importantly, in order to create elaborate dialogues, the original Dragon age origins Toolset can be used, the only catch is to add two extra characters at the beginning of any text, the first character represents position on the wheel 0-5, starting top right going clockwise, the second character 0-f is used to fetch the mood icon to be displayed on the dialogue wheel

Example:"1a||put some text" will be parsed as follows: the text "put some text"will be displayed on middle right position of the wheel, and if one hovers over, the icon a.png will be turned into a Sprite and displayed in the center of the wheel

![Alt text](https://raw.githubusercontent.com/dhk-room101/YetRPG/master/Assets/Resources/Images/conversation1.jpg)

To do: expand AI

changes history:

09/01/15: porting of *_core and *_h files done 

01/21/16: parsing the XML files from Demo Module from Dragon Age Toolset, correctly changing between areas

01/31/16: dialogue parser completed, basic game states changes, and other small tweaks

02/04/16: complete dialogue implementation

02/14/16: basic combat implemented