//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Engine
{
     //==============================================================================
     /*

         Prelude
          -> Bridge Attack

         This is a self contained event, controlling the attacks pummeling the
         bridge in the night time version of the King's Camp.

         Calling PRE_BridgeAttack_Setup() will spawn archers/ballistas/smoke.

         Calling PRE_BridgeAttack_Setup() will begin the attacks, beginning with the
         closest two impact points.

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: September 11, 2007
     //==============================================================================

     //#include"utility_h"
     //#include"events_h"
     //#include"pre_objects_h"
     //#include"sys_ambient_h"

     //------------------------------------------------------------------------------

     //moved public const int       PRE_BRIDGE_ARCHER_OFFICER_READY = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;
     //moved public const int       PRE_BRIDGE_ARCHER_OFFICER_AIM   = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_02;
     //moved public const int       PRE_BRIDGE_ARCHER_OFFICER_FIRE  = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_03;
     //moved public const int       PRE_BRIDGE_ARCHER_OFFICER_MOVE  = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_04;
     //moved public const int       PRE_BRIDGE_ARCHER_MOVE          = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_05;

     //------------------------------------------------------------------------------

     //moved public const int       PRE_CATAPULT_EVENT_FIRE         = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;
     //moved public const int       PRE_CATAPULT_EVENT_RELOAD       = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_02;
     //moved public const int       PRE_CATAPULT_EVENT_KILL         = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_03;
     //moved public const int       PRE_CATAPULT_EVENT_FREE_TARGET  = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_04;
     //moved public const int       PRE_CATAPULT_IMPACT_EFFECT_1    = 5025;     // impact VFX #1
     //moved public const int       PRE_CATAPULT_IMPACT_EFFECT_2    = 3001;     // impact VFX #2
     //moved public const int       PRE_CATAPULT_IMPACT_MAX_OBJ     = 5;        // impact max objects effected
     //moved public const float     PRE_CATAPULT_IMPACT_RANGE_1     = 5.0f;     // impact xEffect range #1
     //moved public const float     PRE_CATAPULT_IMPACT_RANGE_2     = 8.0f;     // impact xEffect range #2
     //moved public const float     PRE_CATAPULT_IMPACT_RANGE_3     = 30.0f;    // impact xEffect range #3
     //moved public const float     PRE_CATAPULT_IMPACT_RANGE_4     = 60.0f;    // impact xEffect range #4
     //moved public const float     PRE_CATAPULT_DELAY_RELOAD_F     = 0.0f;     // fixed reload time
     //moved public const float     PRE_CATAPULT_DELAY_RELOAD_V     = 0.0f;     // variable reload time
     //moved public const float     PRE_CATAPULT_DELAY_FREE_TARGET  = 20.0f;    // time till target valid again

     //------------------------------------------------------------------------------

     public void _BridgeDefenderGenericSetup(GameObject oDefender)
     {
          WR_SetObjectActive(oDefender, EngineConstants.TRUE);
          WR_AddCommand(oDefender, CommandWait(0.0f));
          SetObjectInteractive(oDefender, EngineConstants.FALSE);
          if (GetObjectType(oDefender) == EngineConstants.OBJECT_TYPE_CREATURE)
               Ambient_Start(oDefender);
     }

     public void PRE_BridgeAttack_Setup()
     {

          int nIndex;
          int nArraySize;
          GameObject oCurrent;
          List<GameObject> arDefenderArchers;
          List<GameObject> arDefenderOfficers;
          List<GameObject> arDefenderBallistaSoldiers;
          List<GameObject> arDefenderWounded;
          List<GameObject> arDefenderCreatures;
          List<GameObject> arDefenderPlaceables;

          //--------------------------------------------------------------------------

          arDefenderArchers = UT_GetTeam(EngineConstants.PRE_TEAM_CAMP_NIGHT_BRIDGE_ARCHERS);
          arDefenderOfficers = UT_GetTeam(EngineConstants.PRE_TEAM_CAMP_NIGHT_BRIDGE_OFFICERS);
          arDefenderBallistaSoldiers = UT_GetTeam(EngineConstants.PRE_TEAM_CAMP_NIGHT_BRIDGE_BALLISTA_SOLDIERS);
          arDefenderWounded = UT_GetTeam(EngineConstants.PRE_TEAM_CAMP_NIGHT_BRIDGE_WOUNDED);
          arDefenderCreatures = UT_GetTeam(EngineConstants.PRE_TEAM_CAMP_NIGHT_BRIDGE_DEFENDERS);
          arDefenderPlaceables = UT_GetTeam(EngineConstants.PRE_TEAM_CAMP_NIGHT_BRIDGE_DEFENDERS, EngineConstants.OBJECT_TYPE_PLACEABLE);

          //--------------------------------------------------------------------------

          // Setup Bridge Archers
          nArraySize = GetArraySize(arDefenderArchers);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCurrent = arDefenderArchers[nIndex];
               _BridgeDefenderGenericSetup(oCurrent);
               SetForcedCombatResult(oCurrent, EngineConstants.COMBAT_RESULT_MISS);
               /*
               SetLocalInt(oCurrent,EngineConstants.CREATURE_COUNTER_1,StringToInt(SubString(GetTag(oCurrent),17,1)));
               SetLocalInt(oCurrent,EngineConstants.CREATURE_COUNTER_2,StringToInt(SubString(GetTag(oCurrent),19,1)));
               SetLocalInt(oCurrent,EngineConstants.CREATURE_COUNTER_3,0);
               */

          }

          // Setup Bridge Officers
          nArraySize = GetArraySize(arDefenderOfficers);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCurrent = arDefenderOfficers[nIndex];
               _BridgeDefenderGenericSetup(oCurrent);
               SetForcedCombatResult(oCurrent, EngineConstants.COMBAT_RESULT_MISS);
               /*
               SetLocalInt(oCurrent,EngineConstants.CREATURE_COUNTER_1,StringToInt(SubString(GetTag(oCurrent),17,1)));
               SetLocalInt(oCurrent,EngineConstants.CREATURE_COUNTER_2,StringToInt(SubString(GetTag(oCurrent),19,1)));
               SetLocalInt(oCurrent,EngineConstants.CREATURE_COUNTER_3,0);
               */
          }

          // Setup Bridge Ballista Soldiers
          nArraySize = GetArraySize(arDefenderBallistaSoldiers);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCurrent = arDefenderBallistaSoldiers[nIndex];
               _BridgeDefenderGenericSetup(oCurrent);
               SetForcedCombatResult(oCurrent, EngineConstants.COMBAT_RESULT_MISS);
               DelayEvent((nIndex * 0.5f), oCurrent, Event(EngineConstants.EVENT_TYPE_HANDLE_CUSTOM_AI));
          }

          // Setup Bridge Wounded
          nArraySize = GetArraySize(arDefenderWounded);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCurrent = arDefenderWounded[nIndex];
               Ambient_Start(oCurrent, EngineConstants.AMBIENT_SYSTEM_ENABLED | EngineConstants.AMBIENT_SYSTEM_NOBLEND, EngineConstants.AMBIENT_MOVE_INVALID, EngineConstants.AMBIENT_MOVE_PREFIX_NONE, EngineConstants.AMBIENT_ANIM_PATTERN_WRITHING, EngineConstants.AMBIENT_ANIM_FREQ_ORDERED);
               _BridgeDefenderGenericSetup(oCurrent);
          }

          // Setup Bridge Defenders
          nArraySize = GetArraySize(arDefenderCreatures);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCurrent = arDefenderCreatures[nIndex];
               _BridgeDefenderGenericSetup(oCurrent);
          }

          // Setup Bridge Defender Placeables
          nArraySize = GetArraySize(arDefenderPlaceables);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCurrent = arDefenderPlaceables[nIndex];
               _BridgeDefenderGenericSetup(oCurrent);
          }

          PRE_BridgeAttack_Start();

     }

     public void PRE_BridgeAttack_Start()
     {

          int nIndex;
          int nArraySize;
          GameObject oCatapult;
          List<GameObject> arCatapults;

          //--------------------------------------------------------------------------

          arCatapults = UT_GetTeam(EngineConstants.PRE_TEAM_CAMP_NIGHT_CATAPULTS, EngineConstants.OBJECT_TYPE_PLACEABLE);
          nArraySize = GetArraySize(arCatapults);

          //--------------------------------------------------------------------------

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCatapult = arCatapults[nIndex];
               WR_SetObjectActive(oCatapult, EngineConstants.TRUE);
               DelayEvent((nIndex * 1.0f), oCatapult, Event(EngineConstants.PRE_CATAPULT_EVENT_FIRE));
          }

     }

     public void PRE_BridgeAttack_ArcherFormations()
     {

     }
}