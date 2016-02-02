//ready
//double check modulo % 10 and ~
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
     ////////////////////////////////////////////////////////////////////////////////
     //  sys_ambient_h
     //  Copyright � 2007 BioWare Corp.
     /////////////////////////////////////////////////////////////////////////////
     /*
         Implementation of ambient behaviour system.

         By default, ambient behaviour begins when a creature sees the player and
         ceases a short time after the party leaves the creature's sight. Ambient
         behaviour alternates between a movement phase and an animation phase. In a
         movement phase, the creature (optionally) moves to a new destination object.
         In an animation phase, the creature (optionally) performs one or more
         animations either in sequence or at random. Exact behaviour is controlled by
         local variables on the creature template (see below).

         The actions comprising the animation pattern used in an animation phase are
         listed in ambient_ai.xls. Each row defines an animation pattern consisting
         of up to nine actions. Each action is composed of an animation (left of the
         decimal) and loop count (right of decimal). For example, 619.05 will play a
         sleeping animation for 5 loops and 619.99 will loop infinitely.
         Non-looping animations require no loop count (e.g. 808.00 = point right).

         The only functions safe to call outside sys_ambient_h or creature_core are:
             - Ambient_Start()
             - Ambient_StartTeam()
             - Ambient_StartTag()
             - Ambient_Stop()
             - Ambient_OverrideBehaviour()
             - Ambient_RestoreBehaviour()

         Local variables stored on the creature used by this script:
             EngineConstants.AMBIENT_SYSTEM_STATE = State bitmask of EngineConstants.AMBIENT_SYSTEM_XXX values
             EngineConstants.AMBIENT_MOVE_PATTERN = Ambient movement pattern constant EngineConstants.AMBIENT_MOVE_XXX (0-7)
             EngineConstants.AMBIENT_MOVE_PREFIX  = Prefix of ambient movement destination (waypoint or creature)
             EngineConstants.AMBIENT_ANIM_PATTERN = Ambient animation pattern - index into ambient_ai.xls
             EngineConstants.AMBIENT_ANIM_FREQ    = Animation frequency
                                         -1.0f = play all in order
                                          0.0f = no animations
                                          x.y = play random number (between x and y) animations in random order

             EngineConstants.AMBIENT_ANIM_PATTERN_OVERRIDE = (Internal use) If non-zero, takes precedence over EngineConstants.AMBIENT_ANIM_PATTERN
             EngineConstants.AMBIENT_ANIM_FREQ_OVERRIDE    = (Internal use) If non-zero, takes precedence over EngineConstants.AMBIENT_ANIM_FRE
             EngineConstants.AMBIENT_ANIM_OVERRIDE_COUNT   = (Internal use) If non-zero, the number of times to play the override animation pattern before resuming the default animation pattern
             EngineConstants.AMBIENT_ANIM_STATE   = (Internal use) Animation state: 0 = start move phase, -1 = start anim phase, +ve = # anims left to play
             EngineConstants.AMBIENT_MOVE_STATE   = (Internal use) loword = # of the waypoint moved to last, hiword = direction of travel
             EngineConstants.AMBIENT_MOVE_COUNT   = (Internal use) Number of NPC/WPs available to move to
     *///////////////////////////////////////////////////////////////////////////////

     //#include"var_constants_h"
     //#include"log_h"
     //#include"events_h"
     //#include"utility_h"
     //#include"wrappers_h"
     //#include"sys_rubberband_h"

     // EngineConstants.AMBIENT_COMMAND constants
     //moved public const int   EngineConstants.AMBIENT_COMMAND_ATTACK_PRACTICE = 1; // Creature attacks target with 1-5s delay between attack commands.
     //moved public const int   EngineConstants.AMBIENT_COMMAND_ATTACK          = 2; // Creature attacks target with no delay between attack commands.

     // EngineConstants.AMBIENT_SYSTEM_STATE bit flags
     //moved public const int   EngineConstants.AMBIENT_SYSTEM_DISABLED  = 0x00;
     //moved public const int   EngineConstants.AMBIENT_SYSTEM_ENABLED   = 0x01;    // Indicates creature uses ambient behaviour.
     //moved public const int   EngineConstants.AMBIENT_SYSTEM_SPAWNSTART= 0x02;    // Indicates ambient behaviour starts when creature spawns.
     //moved public const int   EngineConstants.AMBIENT_SYSTEM_NOPLAYNEXT= 0x04;    // Forces the PlayNext parameter to CommandPlayAnimation to be EngineConstants.FALSE.
     //moved public const int   EngineConstants.AMBIENT_SYSTEM_NOBLEND   = 0x08;    // Forces the BlendIn parameter to CommandPlayAnimation to be EngineConstants.FALSE.
     //moved public const int   EngineConstants.AMBIENT_SYSTEM_ALWAYSON  = 0x10;    // Sets ambient behaviour to never stop (even during combat or when player remains out of sight).
     //moved public const int   EngineConstants.AMBIENT_SYSTEM_RUNNING   = 0x40;    // (Internal use) Indicates ambient movement/animation is in progress.

     // EngineConstants.AMBIENT_MOVE_PATTERN types
     //moved public const int   EngineConstants.AMBIENT_MOVE_INVALID     = -1;
     //moved public const int   EngineConstants.AMBIENT_MOVE_NONE        =  0;      // No movement
     //moved public const int   EngineConstants.AMBIENT_MOVE_PATROL      =  1;      // Patrol waypoints (1, 2, 3, 2, 1, ...)
     //moved public const int   EngineConstants.AMBIENT_MOVE_LOOP        =  2;      // Loop through waypoints (1, 2, 3, 1, 2, 3, ...)
     //moved public const int   EngineConstants.AMBIENT_MOVE_WARP        =  3;      // Jump from the last waypoint to the first (1, 2, 3, jump to 1, 2, 3, ...)
     //moved public const int   EngineConstants.AMBIENT_MOVE_RANDOM      =  4;      // Go to random waypoint
     //moved public const int   EngineConstants.AMBIENT_MOVE_WANDER      =  5;      // Go to random Vector3 within EngineConstants.AMBIENT_RANGE_NEAR meters of home location
     //moved public const int   EngineConstants.AMBIENT_MOVE_WANDER_FAR  =  6;      // Go to random Vector3 within EngineConstants.AMBIENT_RANGE_FAR meters of home location
     //moved public const int   EngineConstants.AMBIENT_MOVE_PATH_PATROL =  7;      // Follow waypoints using CommandMoveToMultiLocation() in order (1, 2, 3, 2, 1, ...)
     //moved public const int   EngineConstants.AMBIENT_MOVE_PATH_LOOP   =  8;      // Follow waypoints using CommandMoveToMultiLocation() in order (1, 2, 3, 1, 2, 3, ...)
     //moved public const int   EngineConstants.AMBIENT_MOVE_ONCE        =  9;      // Follow waypoints in order once (1, 2, 3)

     // EngineConstants.AMBIENT_ANIM_FREQ constants
     //moved public const float EngineConstants.AMBIENT_ANIM_FREQ_NONE   =  0.0f;
     //moved public const float EngineConstants.AMBIENT_ANIM_FREQ_ORDERED= -1.0f;
     //moved public const float EngineConstants.AMBIENT_ANIM_FREQ_RANDOM =  3.5f;

     // EngineConstants.AMBIENT_ANIM_STATE constants
     //moved public const int   EngineConstants.AMBIENT_ANIM_NONE        =  0;      // Indicates no animations remaining to play, ergo movement phase should begin next.
     //moved public const int   EngineConstants.AMBIENT_ANIM_RESET       = -1;      // Indicates movement phase complete and animation phase should begin next.

     // EngineConstants.AMBIENT_MOVE_STATE direction constants
     //moved public const int   EngineConstants.AMBIENT_MOVE_NEXT        = 0x00000; // Indicates waypoint travel in increasing numerical order
     //moved public const int   EngineConstants.AMBIENT_MOVE_PREV        = 0x10000; // Indicates waypoint travel in decreasing numerical order

     // EngineConstants.AMBIENT_MOVE_COUNT constants
     //moved public const int   EngineConstants.AMBIENT_MOVE_COUNT_INVALID  = -1;   // Indicates destination count needs to be (re)done.

     // Time and distance constants
     //moved public const float EngineConstants.AMBIENT_SMALL_DELTA      =  0.01f;  // Small value used for floating point comparison.
     //moved public const float EngineConstants.AMBIENT_SMALL_DELAY      =  0.1f;   // Period to wait between ambient actions (in seconds).
     //moved public const float EngineConstants.AMBIENT_MOVE_TOLERANCE   =  2.0f;   // Move to target tolerance in metres
     //moved public const float EngineConstants.AMBIENT_RESUME_PERIOD    = 15.0f;   // Period to wait before attempting to resume ambient behaviour (in seconds).
     //moved public const int   EngineConstants.AMBIENT_PAUSE_PERIOD     = 30;      // Number of seconds the party must be out of range for a creature to pause ambient behaviour.
     //moved public const int   EngineConstants.AMBIENT_RANGE_NEAR       = 10;      // Range limit for wandering 'near' in metres.
     //moved public const int   EngineConstants.AMBIENT_RANGE_FAR        = 30;      // Range limit for wandering 'far' in metres.
     //moved public const int   EngineConstants.AMBIENT_LOOP_FOREVER     = 90;      // Any number of loops greater than this is considered infinite looping.

     // String constants
     //moved public const string EngineConstants.AMBIENT_MOVE_PREFIX_NONE = "";

     // 2DA column names in ambient_ai.xls
     //moved public const string EngineConstants.COL_AMBIENT_ANIM            = "Action";
     //moved public const string EngineConstants.COL_AMBIENT_ANIM_TOTAL      = "ActionTotal";
     //moved public const string EngineConstants.COL_AMBIENT_ANIM_NOPLAYNEXT = "NoPlayNext";

     // Animation pattern constants corresponding to rows in ambient_ai.xls
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_NONE             =  0;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_TWITCHES         =  1;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_RELAXED_LOOP     =  2;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_DOG_EATING       =  3;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_RELAXED          =  4;
     ////moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_GUARD_POST     =  5;
     ////moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_GUARD_POST     =  6;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_DEAD_LOOP        =  7;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_BLESSING         =  8;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_CROUCHPRAY_1     =  9;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_LECTURER_1       = 10;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_LISTENER_1       = 11;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_LECTURER_2       = 12;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_CHATTER_1        = 13;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_LISTENER_2       = 14;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_SHOPPER          = 15;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_NURSE            = 16;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_WRITHING         = 17;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_TABLE_MAP        = 18;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_CROSSARMS_LOOP   = 19;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_MERCHANT_BECKON  = 20;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_CLEAN_FLOOR      = 22;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_SITGROUND_LOOP   = 23;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_WANDER_LR        = 24;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_HANDSBACK_LOOP_1 = 25;   // without enter animation
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_ATTENTION        = 26;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_PRE_LOGHAIN      = 27;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_WANDER_R         = 28;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_WANDER_L         = 29;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_DARKSPAWN_WANDER = 30;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_CROUCH_LOOP      = 31;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_PRAY_LOOP        = 32;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_CROUCHPRAY_2     = 33;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_HANDSBACK_LOOP_2 = 34;   // with enter animation
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_RUMMAGING        = 35;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_DEER_GRAZING_1   = 59;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_DEER_GRAZING_2   = 60;

     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_TEST             = 1000;
     //moved public const int EngineConstants.AMBIENT_ANIM_PATTERN_TEST_LONG        = 1001;

     ////////////////////////////////////////////////////////////////////////////////

     /* @brief Starts (or restarts) a creature's ambient behaviour.
*
*   @param oCreature        The creature to perform ambient behaviour.
*   @param nAmbientState    If non-zero this replaces the EngineConstants.AMBIENT_SYSTEM_STATE flag on oCreature.
*   @param nMovePattern     If non-negative this replaces the EngineConstants.AMBIENT_MOVE_PATTERN variable on oCreature.
*   @param sMovePrefix      If non-empty this replaces the EngineConstants.AMBIENT_MOVE_PREFIX variable on oCreature.
*   @param nAnimPattern     If non-zero this replaces the EngineConstants.AMBIENT_ANIM_PATTERN variable on oCreature.
*   @param fAnimFreq        If non-zero this replaces the EngineConstants.AMBIENT_ANIM_FREQ variable on oCreature.
**//////////////////////////////////////////////////////////////////////////////
     public void Ambient_Start(GameObject oCreature = null, int nAmbientState = EngineConstants.AMBIENT_SYSTEM_ENABLED, int nMovePattern = EngineConstants.AMBIENT_MOVE_INVALID, string sMovePrefix = EngineConstants.AMBIENT_MOVE_PREFIX_NONE, int nAnimPattern = EngineConstants.AMBIENT_ANIM_PATTERN_NONE, float fAnimFreq = EngineConstants.AMBIENT_ANIM_FREQ_NONE)
     {
          if (oCreature == null) oCreature = gameObject;
          if (GetObjectActive(oCreature) == EngineConstants.FALSE || 
               GetObjectType(oCreature) != EngineConstants.OBJECT_TYPE_CREATURE ||
               IsDead(oCreature) != EngineConstants.FALSE || IsDying(oCreature) != EngineConstants.FALSE || IsPartyMember(oCreature) != EngineConstants.FALSE)
               return;

          if (nAmbientState == EngineConstants.FALSE)
               nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);

          if (nAmbientState == EngineConstants.AMBIENT_SYSTEM_ENABLED)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Start()", ToString(oCreature));

               if (nMovePattern != EngineConstants.AMBIENT_MOVE_INVALID)
                    SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN, nMovePattern);

               if (sMovePrefix != EngineConstants.AMBIENT_MOVE_PREFIX_NONE)
                    SetLocalString(oCreature, EngineConstants.AMBIENT_MOVE_PREFIX, sMovePrefix);

               if (nAnimPattern != EngineConstants.FALSE)
                    SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN, nAnimPattern);

               if (fabs(fAnimFreq) > EngineConstants.AMBIENT_SMALL_DELTA)
                    SetLocalFloat(oCreature, EngineConstants.AMBIENT_ANIM_FREQ, fAnimFreq);

               SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, nAmbientState);
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_RESET);

               Ambient_DoSomething(oCreature);
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Starts (or restarts) ambient behaviour of a team of creatures.
     *
     *   @param nTeam            The ID of the team of creatures.
     *   @param nAmbientState    If non-zero this replaces the EngineConstants.AMBIENT_SYSTEM_STATE flag on oCreature.
     *   @param nMovePattern     If non-negative this replaces the EngineConstants.AMBIENT_MOVE_PATTERN variable on oCreature.
     *   @param sMovePrefix      If non-empty this replaces the EngineConstants.AMBIENT_MOVE_PREFIX variable on oCreature.
     *   @param nAnimPattern     If non-zero this replaces the EngineConstants.AMBIENT_ANIM_PATTERN variable on oCreature.
     *   @param fAnimFreq        If non-zero this replaces the EngineConstants.AMBIENT_ANIM_FREQ variable on oCreature.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_StartTeam(int nTeam, int nAmbientState = EngineConstants.AMBIENT_SYSTEM_ENABLED, int nMovePattern = EngineConstants.AMBIENT_MOVE_INVALID, string sMovePrefix = EngineConstants.AMBIENT_MOVE_PREFIX_NONE, int nAnimPattern = EngineConstants.AMBIENT_ANIM_PATTERN_NONE, float fAnimFreq = EngineConstants.AMBIENT_ANIM_FREQ_NONE)
     {
          List<GameObject> arCreature = GetTeam(nTeam);
          int n = GetArraySize(arCreature);
          int i = 0;

          for (i = 0; i < n; i++)
          {
               Ambient_Start(arCreature[i], nAmbientState, nMovePattern, sMovePrefix, nAnimPattern, fAnimFreq);
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Starts (or restarts) ambient behaviour of all creatures with a given tag.
     *
     *   @param sTag             The tag of the creature(s).
     *   @param nAmbientState    If non-zero this replaces the EngineConstants.AMBIENT_SYSTEM_STATE flag on oCreature.
     *   @param nMovePattern     If non-negative this replaces the EngineConstants.AMBIENT_MOVE_PATTERN variable on oCreature.
     *   @param sMovePrefix      If non-empty this replaces the EngineConstants.AMBIENT_MOVE_PREFIX variable on oCreature.
     *   @param nAnimPattern     If non-zero this replaces the EngineConstants.AMBIENT_ANIM_PATTERN variable on oCreature.
     *   @param fAnimFreq        If non-zero this replaces the EngineConstants.AMBIENT_ANIM_FREQ variable on oCreature.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_StartTag(string sTag, int nAmbientState = EngineConstants.AMBIENT_SYSTEM_ENABLED, int nMovePattern = EngineConstants.AMBIENT_MOVE_INVALID, string sMovePrefix = EngineConstants.AMBIENT_MOVE_PREFIX_NONE, int nAnimPattern = EngineConstants.AMBIENT_ANIM_PATTERN_NONE, float fAnimFreq = EngineConstants.AMBIENT_ANIM_FREQ_NONE)
     {
          List<GameObject> arCreature = GetNearestObjectByTag(GetHero(), sTag, EngineConstants.OBJECT_TYPE_CREATURE, 20);
          int n = GetArraySize(arCreature);
          int i = 0;

          for (i = 0; i < n; i++)
          {
               Ambient_Start(arCreature[i], nAmbientState, nMovePattern, sMovePrefix, nAnimPattern, fAnimFreq);
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief  Checks whether to start ambient behaviour after creature spawns.
     *
     *   Called from creature's EngineConstants.EVENT_TYPE_SPAWN handler. Starts ambient behaviour
     *   if the local variable EngineConstants.AMBIENT_SYSTEM_STATE bit mask contains 0x02
     *   (EngineConstants.AMBIENT_SYSTEM_SPAWNSTART). Generally, only creatures with a single
     *   infinitely looping animation should have this bit set.
     *
     *   @param  oCreature        The creature.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_SpawnStart(GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          if (GetObjectActive(oCreature) == EngineConstants.FALSE || 
               GetObjectType(oCreature) != EngineConstants.OBJECT_TYPE_CREATURE || 
               IsDead(oCreature) != EngineConstants.FALSE || IsDying(oCreature) != EngineConstants.FALSE || IsPartyMember(oCreature) != EngineConstants.FALSE)
               return;

          int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);

          // Hack: Originally EngineConstants.AMBIENT_COMMAND didn't require the EngineConstants.AMBIENT_SYSTEM_ENABLED bit set. Now it does, so flip the bit here.
          if (GetLocalInt(oCreature, EngineConstants.AMBIENT_COMMAND) != EngineConstants.FALSE)
          {
               nAmbientState |= EngineConstants.AMBIENT_SYSTEM_SPAWNSTART;
          }

          if (nAmbientState == EngineConstants.AMBIENT_SYSTEM_SPAWNSTART)
          {
               // Start ambient behaviour.
               nAmbientState |= EngineConstants.AMBIENT_SYSTEM_ENABLED;
               SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, nAmbientState);
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_RESET);

               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_SpawnStart()", "AMBIENT_SYSTEM_STATE: " + IntToHexString(nAmbientState));

               Ambient_DoSomething(oCreature);
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Disables ambient behaviour for a creature.
     *
     *   @param oCreature    The creature.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_Stop(GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Stop()");

          int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);
          SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, nAmbientState & ~EngineConstants.AMBIENT_SYSTEM_ENABLED);
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Causes a creature to move a random distance away from its home location.
     *
     *   @param  oCreature   The creature moving.
     *   @param  nRange      Maximum distance (in meters) oCreature can wander from home location.
     *   @param  bRun        Whether the creature should walk or run.
     *   @returns            EngineConstants.TRUE if function succeeds (i.e. creature moves to a valid location).
     **//////////////////////////////////////////////////////////////////////////////
     public int Ambient_MoveRandom(GameObject oCreature, int nRange, int bRun = EngineConstants.FALSE)
     {
          Vector3 vHome = GetPositionFromLocation(Rubber_GetHome(oCreature));
          Vector3 lDest = Location(GetArea(oCreature),
                                    Vector(vHome.x + RandomF(2 * nRange, -nRange), vHome.y + RandomF(2 * nRange, -nRange), vHome.z),
                                    0.0f);
          WR_AddCommand(oCreature, CommandMoveToLocation(lDest, bRun));
          return EngineConstants.TRUE;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Gets the number of objects a creature can move to through ambient movement.
     *
     *   Gets the number of objects a creature can move to through ambient movement.
     *   The destinations can be waypoints, placeables, or creatures. Expected tag
     *   convention is either sequential unique objects (XXX_01, XXX_02, XXX_03, etc.) or
     *   all objects sharing a single common tag (XXX) where XXX is the EngineConstants.AMBIENT_MOVE_PREFIX
     *   local variable set on the creature.
     *
     *   @param    oCreature  The creature whose destinations to count.
     *   @returns             The number of valid destinations for oCreature.
     **//////////////////////////////////////////////////////////////////////////////
     public List<GameObject> Ambient_GetDestinations(GameObject oCreature)
     {
          string sWP = ReplaceString(GetLocalString(oCreature, EngineConstants.AMBIENT_MOVE_PREFIX), "<tag>", GetTag(oCreature));
          List<GameObject> aWP= new List<GameObject>();
          GameObject oWP;
          int i;
          //assuming waypoints have sequential tag, but each waypoint has a unique name
          for (i = 1; i < 10; i++)
          {
               oWP = GetObjectByTag(sWP + "_" + i);
               if (oWP != null)
               {
                    if (IsObjectValid(oWP) != EngineConstants.FALSE)
                    {
                         aWP.Add(oWP);
                    }
               }
          }
          
          /*for (i = 0; IsObjectValid(oWP = GetObjectByTag(sWP + "_" + (i + 1 < 10 ? "0" : "") + ToString(i + 1))); i++)
               aWP[i] = oWP;*/

          if (GetArraySize(aWP) == 0)
          {
               // Hack for non-existent engine function GetObjectsByTag()
               /*for (i = 0; IsObjectValid(oWP = GetObjectByTag(sWP, i)); i++)
                    aWP[i] = oWP;*/
               //if the above fails, just search for all the objects/waypoints that match the main tag
               throw new NotImplementedException();
          }
          return aWP;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Gets the Nth ambient destination for a creature.
     *
     *   See Ambient_GetDestinations() for a description of the expected tag format.
     *
     *   @param oCreature    The creature moving.
     *   @param n            The number of the destination.
     *   @returns            The Nth destination GameObject or null.
     **//////////////////////////////////////////////////////////////////////////////
     public GameObject Ambient_GetDestination(GameObject oCreature, int n)
     {
          string sWP = ReplaceString(GetLocalString(oCreature, EngineConstants.AMBIENT_MOVE_PREFIX), "<tag>", GetTag(oCreature));
          GameObject oWP = GetObjectByTag(sWP + "_" + (n < 10 ? "0" : "") + ToString(n));
          Warning( "sys_ambient_h: double check");
          if (IsObjectValid(oWP) == EngineConstants.FALSE)
               oWP = GetObjectByTag(sWP, n - 1);   // subtract 1 since GetObjectByTag() is zero-based.

          if (IsObjectValid(oWP) == EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_GetDestination()", "Failed to find valid destination: " + sWP);

          return oWP;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Causes a creature to walk between a set of objects (typically waypoints).
     *
     *   @param oCreature        The creature moving.
     *   @param nMovementPattern The pattern of movement between objects (loop, patrol, random, etc).
     *   @param bRun             Whether the creature should walk or run to the next waypoint.
     *   @returns                EngineConstants.TRUE if function succeeds (i.e. creature moves to a valid location).
     **//////////////////////////////////////////////////////////////////////////////
     public int Ambient_WalkWaypoints(GameObject oCreature, int nMovementPattern, int bRun = EngineConstants.FALSE)
     {
          int cWP = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_COUNT);   // Number of destinations available to move to.
          if (cWP == EngineConstants.AMBIENT_MOVE_COUNT_INVALID)
          {
               // Calculate and cache destination count
               cWP = GetArraySize(Ambient_GetDestinations(oCreature));
               SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_COUNT, cWP);
               SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE, EngineConstants.AMBIENT_MOVE_NEXT);
          }

          if (cWP == 0)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_WalkWaypoints()", "No valid destinations.");
               WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
               return EngineConstants.FALSE;
          }

          int nMoveState = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE);
          int nWP = nMoveState & 0x0000FFFF;    // loword = The number of the object/waypoint last moved to.
          long nMoveDir = nMoveState & 0xFFFF0000;    // hiword = Direction of travel.
          int bJump = EngineConstants.FALSE;

          /*
              if (nWP == cWP && cWP == 1)
              {
                  // There's only 1 ambient waypoint and creature is already at it.
                  WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
                  return EngineConstants.FALSE;
              }
          */

          // Figure out next destination.
          switch (nMovementPattern)
          {
               case EngineConstants.AMBIENT_MOVE_PATROL:     // 1,2,3,2,1,2,3, etc
                    {
                         if (nMoveDir == EngineConstants.AMBIENT_MOVE_NEXT)
                         {
                              nWP++;
                              if (nWP > cWP)
                              {
                                   nWP -= 2;
                                   nMoveDir = EngineConstants.AMBIENT_MOVE_PREV;
                              }
                         }
                         else
                         {
                              nWP--;
                              if (nWP < 1)
                              {
                                   nWP += 2;
                                   nMoveDir = EngineConstants.AMBIENT_MOVE_NEXT;
                              }
                         }
                         break;
                    }
               case EngineConstants.AMBIENT_MOVE_LOOP:       // 1,2,3,1,2,3, etc
                    {
                         nWP++;
                         if (nWP > cWP)
                              nWP = 1;
                         break;
                    }
               case EngineConstants.AMBIENT_MOVE_WARP:       // 1,2,3,jump to 1,2,3, etc
                    {
                         nWP++;
                         if (nWP > cWP)
                         {
                              nWP = 1;
                              bJump = EngineConstants.TRUE;
                         }
                         break;
                    }
               case EngineConstants.AMBIENT_MOVE_ONCE:
                    {
                         nWP++;
                         if (nWP > cWP)
                         {
                              // Reached last waypoint.
                              SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN, EngineConstants.AMBIENT_MOVE_NONE);
                              SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_RESET);
                              return EngineConstants.FALSE;
                         }
                         break;
                    }
               case EngineConstants.AMBIENT_MOVE_RANDOM:     // any destination other than current one
                    {
                         int r = Engine_Random(cWP) + 1;
                         while (cWP > 1 && r == nWP)
                              r = Engine_Random(cWP) + 1;
                         nWP = r;
                         break;
                    }
               default:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_WalkWaypoints()", "Invalid movement pattern: " + ToString(nMovementPattern));
                         break;
                    }
          }

          GameObject oWP = Ambient_GetDestination(oCreature, nWP);
          if (IsObjectValid(oWP) != EngineConstants.FALSE)
          {
               SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE, Convert.ToInt32(nMoveDir + nWP));

               if (bJump != EngineConstants.FALSE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_WalkWaypoints()", "Jump to: " + ToString(oWP));
                    WR_AddCommand(oCreature, CommandJumpToLocation(GetLocation(oWP)));
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_WalkWaypoints()", "Move to: " + ToString(oWP));
                    WR_AddCommand(oCreature, CommandMoveToLocation(GetLocation(oWP), bRun));
               }
               return EngineConstants.TRUE;

          }

          // Found no valid Vector3 to move to
          WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
          SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE, EngineConstants.AMBIENT_MOVE_NEXT);
          SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_COUNT, EngineConstants.AMBIENT_MOVE_COUNT_INVALID); // Force recount of destinations next invocation
          return EngineConstants.FALSE;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Causes a creature to follow a prescribed path using CommandMoveToMultiLocations().
     *
     *   @param  oCreature   The ambient creature.
     *   @param  bLoop       Whether the creature should loop or patrol waypoints.
     *   @param  bRun        Whether the creature should walk or run between waypoints.
     *   @returns            EngineConstants.TRUE if function succeeds (i.e. creature moves to a valid location).
     **//////////////////////////////////////////////////////////////////////////////
     public int Ambient_MovePath(GameObject oCreature, int bLoop, int bRun)
     {
          List<GameObject> aWP = Ambient_GetDestinations(oCreature);
          int cWP = GetArraySize(aWP);

          if (cWP == 0)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_MovePath()", "No valid destinations.");
               WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
               return EngineConstants.FALSE;
          }

          int nMoveState = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE);
          int nWP = nMoveState & 0x0000FFFF;    // loword = The number of the object/waypoint last moved to.
          long nMoveDir = nMoveState & 0xFFFF0000;    // hiword = Direction of travel.

          if ((nWP + 1) >= cWP)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_MovePath()", "nWP + 1 >= cWP (" + ToString(nWP) + " + 1 >= " + ToString(cWP) + ") -> changing direction");
               nMoveDir = (nMoveDir == EngineConstants.AMBIENT_MOVE_NEXT) ? EngineConstants.AMBIENT_MOVE_PREV : EngineConstants.AMBIENT_MOVE_NEXT;
               nWP = 0;
          }

          List<Vector3> lWP= new List<Vector3>();
          int i;
          for (i = 0; i < cWP; i++)
          {
               if (bLoop == EngineConstants.FALSE && nMoveDir == EngineConstants.AMBIENT_MOVE_PREV)
                    lWP[i] = GetLocation(aWP[cWP - i - 1]);
               else
                    lWP[i] = GetLocation(aWP[i]);
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_MovePath()", "Moving to destination " + ToString(nWP) + " of " + ToString(cWP));
          SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE, Convert.ToInt32(nMoveDir + nWP));
          WR_AddCommand(oCreature, CommandMoveToMultiLocations(lWP, bRun, nWP));
          return EngineConstants.TRUE;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Handler for EngineConstants.EVENT_TYPE_REACHED_WAYPOINT event.
     *
     *   @param   ev     The EngineConstants.EVENT_TYPE_REACHED_WAYPOINT event.
     *   @returns        EngineConstants.TRUE if creature moved to waypoint because of ambient behaviour.
     **//////////////////////////////////////////////////////////////////////////////
     public int Ambient_ReachedWaypoint(xEvent ev)
     {
          int bEventHandled = EngineConstants.FALSE;
          GameObject oCreature = GetEventCreatorRef(ref ev);
          int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);
          int nMovePattern = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN) % 10;

          if ((nAmbientState == EngineConstants.AMBIENT_SYSTEM_ENABLED)
             && (nMovePattern == EngineConstants.AMBIENT_MOVE_PATH_PATROL || nMovePattern == EngineConstants.AMBIENT_MOVE_PATH_LOOP))
          {
               int nWP = GetEventIntegerRef(ref ev, 0);
               int nMoveState = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE);
               long nMoveDir = nMoveState & 0xFFFF0000;    // hiword = Direction of travel.

               SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE, Convert.ToInt32(nMoveDir + nWP + 1)); // Add 1 since nWP is zero-based.
               bEventHandled = EngineConstants.TRUE;
          }
          return bEventHandled;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Checks whether a creature has reached to its final destination.
     *
     *   @param   oCreature  The ambient creature.
     *   @returns EngineConstants.TRUE if oCreature is within a reasonable tolerance of the destination
     *            to which it was last told to move.
     **//////////////////////////////////////////////////////////////////////////////
     public int Ambient_ReachedDestination(GameObject oCreature)
     {
          int nMovePattern = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN);
          int nRunChance = nMovePattern / 10;     // 2nd digit is the likelihood (0-9) of running (0 = 0%, 1 = 10%, etc).
          nMovePattern = nMovePattern % 10;     // 1st digit is the 'real' move pattern.
          int nMoveState = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_STATE);
          int nWP = nMoveState & 0x0000FFFF;    // loword = The number of the object/waypoint last moved to.

          switch (nMovePattern)
          {
               case EngineConstants.AMBIENT_MOVE_PATROL:
               case EngineConstants.AMBIENT_MOVE_LOOP:
               case EngineConstants.AMBIENT_MOVE_WARP:
               case EngineConstants.AMBIENT_MOVE_ONCE:
               case EngineConstants.AMBIENT_MOVE_RANDOM:
               case EngineConstants.AMBIENT_MOVE_WANDER:
               case EngineConstants.AMBIENT_MOVE_WANDER_FAR:
                    {
                         if (nWP != EngineConstants.FALSE)
                         {
                              GameObject oWP = Ambient_GetDestination(oCreature, nWP);
                              if (IsObjectValid(oWP) != EngineConstants.FALSE)
                              {
                                   // Keep moving if too far from destination.
                                   if (GetDistanceBetween(oCreature, oWP) > EngineConstants.AMBIENT_MOVE_TOLERANCE)
                                   {
                                        Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_ReachedDestination()", "Moving to: " + ToString(oWP));
                                        WR_AddCommand(oCreature, CommandWait(5.0f));
                                        WR_AddCommand(oCreature, CommandMoveToLocation(GetLocation(oWP), (nRunChance > Engine_Random(10)) ? EngineConstants.TRUE : EngineConstants.FALSE));
                                        return EngineConstants.FALSE;
                                   }
                              }
                         }
                         break;
                    }
               case EngineConstants.AMBIENT_MOVE_PATH_PATROL:
               case EngineConstants.AMBIENT_MOVE_PATH_LOOP:
                    {
                         int cWP = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_COUNT);
                         if (cWP == EngineConstants.AMBIENT_MOVE_COUNT_INVALID)
                         {
                              List<GameObject> aWP = Ambient_GetDestinations(oCreature);
                              cWP = GetArraySize(aWP);
                              SetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_COUNT, cWP);
                         }

                         if (cWP > 0 && (nWP + 1) < cWP)
                         {
                              Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_ReachedDestination()", "nWP + 1 < cWP (" + ToString(nWP) + " + 1 < " + ToString(cWP) + ") -> not there yet");
                              WR_AddCommand(oCreature, CommandWait(5.0f));
                              Ambient_MovePath(oCreature, (nMovePattern == EngineConstants.AMBIENT_MOVE_PATH_LOOP) ? EngineConstants.TRUE : EngineConstants.FALSE, (nRunChance > Engine_Random(10) ? EngineConstants.TRUE : EngineConstants.FALSE));
                              return EngineConstants.FALSE;
                         }

                         Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_ReachedDestination()", "nWP + 1 >= cWP (" + ToString(nWP) + " + 1 >= " + ToString(cWP) + ") -> reached destination");
                         Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_ReachedDestination()", "AMBIENT_ANIM_STATE: " + ToString(GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE)));
                         break;
                    }
          }
          return EngineConstants.TRUE;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Triggers next action in an ambient behaviour animation phase.
     *
     *   Causes oCreature to perform the next action in an ambient behaviour animation phase.
     *   Once oCreature runs out of actions to perform, a movement phase is triggered.
     *
     *   @param oCreature  The creature animating.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_Animate(GameObject oCreature)
     {
          int nAnimsToDo = GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE);
          int nAnimPattern = GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN_OVERRIDE);
          float fAnimFreq = GetLocalFloat(oCreature, EngineConstants.AMBIENT_ANIM_FREQ_OVERRIDE);

          // Decrement number of times to play override animations
          int nOverrideCount = GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_OVERRIDE_COUNT);
          if (nAnimsToDo == EngineConstants.AMBIENT_ANIM_RESET && nOverrideCount > 0)
          {
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_OVERRIDE_COUNT, --nOverrideCount);
          }

          // If override count reaches zero then clear override animation pattern & frequency.
          if (nOverrideCount == 0)
          {
               nAnimPattern = EngineConstants.AMBIENT_ANIM_PATTERN_NONE;
               fAnimFreq = EngineConstants.AMBIENT_ANIM_FREQ_NONE;
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN_OVERRIDE, EngineConstants.AMBIENT_ANIM_PATTERN_NONE);
               SetLocalFloat(oCreature, EngineConstants.AMBIENT_ANIM_FREQ_OVERRIDE, EngineConstants.AMBIENT_ANIM_FREQ_NONE);
          }

          // Load default animation pattern and frequency if no override values
          if (nAnimPattern == EngineConstants.AMBIENT_ANIM_PATTERN_NONE)
               nAnimPattern = GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN);
          if (fabs(fAnimFreq) < EngineConstants.AMBIENT_SMALL_DELTA)
               fAnimFreq = GetLocalFloat(oCreature, EngineConstants.AMBIENT_ANIM_FREQ);

          if (nAnimPattern <= 0)
          {
               // No animation pattern so trigger movement if movement pattern specified.
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_NONE);
               if (GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN) % 10 != EngineConstants.FALSE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Animate()", "No EngineConstants.AMBIENT_ANIM_PATTERN");
                    WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
                    return;
               }

               // Creature has no animation pattern or move pattern assigned so default
               // to playing random idle twitches (otherwise creature remains frozen in
               // last animation when returning from staged conversation).
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Animate()", " *** No EngineConstants.AMBIENT_ANIM_PATTERN or EngineConstants.AMBIENT_MOVE_PATTERN ***");

               nAnimsToDo = EngineConstants.AMBIENT_ANIM_RESET;
               nAnimPattern = EngineConstants.AMBIENT_ANIM_PATTERN_TWITCHES;
               fAnimFreq = EngineConstants.AMBIENT_ANIM_FREQ_RANDOM;

               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN, nAnimPattern);
               SetLocalFloat(oCreature, EngineConstants.AMBIENT_ANIM_FREQ, fAnimFreq);
          }

          if (fabs(fAnimFreq) < EngineConstants.AMBIENT_SMALL_DELTA)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Animate()", "No EngineConstants.AMBIENT_ANIM_FREQ");

               // Trigger movement if valid (check first to prevent endless cycling).
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_NONE);
               if (GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN) % 10 != EngineConstants.FALSE)
                    WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
               return;
          }

          if (nAnimsToDo == EngineConstants.AMBIENT_ANIM_RESET)
          {
               // Start of animation phase - determine number to play.
               // fAnimFreq format: min.max (-1.0f to play all in sequence)
               if (fAnimFreq < 0.0f)
               {
                    // -1.0f = play all animations (EngineConstants.AMBIENT_ANIM_FREQ_ORDERED)
                    nAnimsToDo = GetM2DAInt(EngineConstants.TABLE_AMBIENT, EngineConstants.COL_AMBIENT_ANIM_TOTAL, nAnimPattern);
               }
               else
               {
                    // Choose random number of animations
                    int nMin = FloatToInt(fAnimFreq);
                    int nMax = FloatToInt((fAnimFreq - nMin) * 10);
                    nAnimsToDo = Engine_Random(abs(nMax - nMin)) + Min(nMin, nMax);
               }
          }

          if (nAnimsToDo == 0)
          {
               // No animations - trigger movement if valid (check first to prevent endless cycling).
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_NONE);
               if (GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN) % 10 != EngineConstants.FALSE)
                    WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
               return;
          }

          if (nAnimsToDo > 0)     // Pick and play an animation from 2DA
          {
               int nAnims = GetM2DAInt(EngineConstants.TABLE_AMBIENT, EngineConstants.COL_AMBIENT_ANIM_TOTAL, nAnimPattern);
               if (nAnims == 0)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Animate()", "No animations for nAnimPattern: " + ToString(nAnimPattern));

                    // Trigger movement if valid (check first to prevent endless cycling).
                    SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_NONE);
                    if (GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN) % 10 != EngineConstants.FALSE)
                         WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
                    return;
               }

               float fAction = 0.0f;   // Action format: animation.loops
               if (fAnimFreq < 0.0f)
               {
                    // If pattern has changed, we may have to reset if it has less
                    // animations than before
                    if (nAnimsToDo > nAnims)
                    {
                         nAnimsToDo = nAnims;
                    }

                    // Select next action in sequence
                    fAction = GetM2DAFloat(EngineConstants.TABLE_AMBIENT, EngineConstants.COL_AMBIENT_ANIM + ToString(nAnims - nAnimsToDo), nAnimPattern);
               }
               else
               {
                    // Select random action
                    fAction = GetM2DAFloat(EngineConstants.TABLE_AMBIENT, EngineConstants.COL_AMBIENT_ANIM + ToString(Engine_Random(nAnims)), nAnimPattern);
               }

               // If fAction is positive then it's an animation, otherwise it's a placeable interaction.
               if (fAction > 0.0f)
               {
                    // Parse animation (left of decimal) and number of loops (right of decimal) from action.
                    int nAnimation = FloatToInt(fAction);
                    int nLoops = FloatToInt((fAction - nAnimation) * 100);

                    if (nAnimation > 0)
                    {
                         int bBlend = ~(GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE) & EngineConstants.AMBIENT_SYSTEM_NOBLEND);

                         // If NoPlayNext bit is set on the creature, respect it. Otherwise, use value set in 2da.
                         int bPlayNext = ~(GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE) & EngineConstants.AMBIENT_SYSTEM_NOPLAYNEXT);
                         if (bPlayNext != EngineConstants.FALSE)
                              bPlayNext = (GetM2DAInt(EngineConstants.TABLE_AMBIENT, EngineConstants.COL_AMBIENT_ANIM_NOPLAYNEXT, nAnimPattern) == EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
                         if (nAnimation == 1)
                              bPlayNext = EngineConstants.TRUE;

                         Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Animate()", "Animation: " + ToString(nAnimation)
                             + ((nLoops > 0) ? (", Loops: " + ToString(nLoops)) : (""))
                             + ", PlayNext: " + ToString(bPlayNext) + ", Blend: " + ToString(bBlend)
                             + ", nAnimsToDo: " + ToString(nAnimsToDo) + ", nAnimsTotal: " + ToString(nAnims)
                             + ", Pattern: " + ToString(nAnimPattern));

                         WR_AddCommand(oCreature, CommandPlayAnimation(nAnimation, (nLoops >= EngineConstants.AMBIENT_LOOP_FOREVER) ? -1 : nLoops, bPlayNext, bBlend));

                         // Mark as busy to prevent generation of EngineConstants.EVENT_TYPE_AMBIENT_CONTINUE events by engine.
                         if (nLoops > 0)
                         {
                              int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);
                              SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, nAmbientState | EngineConstants.AMBIENT_SYSTEM_RUNNING);
                         }
                    }
               }
               else
               {
                    // Parse interaction (left of decimal) and number of loops (right of decimal) from action.
                    int nInteraction = FloatToInt(fabs(fAction));
                    int nLoops = FloatToInt((fabs(fAction) - nInteraction) * 100);
                    int nPose = 1;

                    if (nInteraction > 0)
                    {
                         List<GameObject> aPlc = GetNearestObject(oCreature, EngineConstants.OBJECT_TYPE_PLACEABLE);
                         if (GetArraySize(aPlc) > 0)
                         {
                              GameObject oTarget = aPlc[0];
                              int bPlayNext = ~(GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE) & EngineConstants.AMBIENT_SYSTEM_NOPLAYNEXT);
                              int bBlend = ~(GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE) & EngineConstants.AMBIENT_SYSTEM_NOBLEND);

                              Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Animate()", "Interaction: " + ToString(nInteraction)
                                  + ((nLoops > 0) ? (", Loops: " + ToString(nLoops)) : (""))
                                  + ", PlayNext: " + ToString(bPlayNext) + ", Blend: " + ToString(bBlend)
                                  + ", nAnimsToDo: " + ToString(nAnims - nAnimsToDo) + "/" + ToString(nAnims)
                                  + ", Pattern: " + ToString(nAnimPattern));

                              InteractWithObject(oCreature, oTarget, nInteraction, 1, (nLoops >= EngineConstants.AMBIENT_LOOP_FOREVER) ? -1 : nLoops, bPlayNext, EngineConstants.FALSE);

                              // Mark as busy to prevent generation of EngineConstants.EVENT_TYPE_AMBIENT_CONTINUE events by engine.
                              if (nLoops > 0)
                              {
                                   int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);
                                   SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, nAmbientState | EngineConstants.AMBIENT_SYSTEM_RUNNING);
                              }
                         }
                    }
               }
          }

          // Decrement number of animations remaining to play.
          // A movement phase is triggered when no more animations remain.
          SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, --nAnimsToDo);
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Triggers an ambient behaviour movement phase.
     *
     *   Causes oCreature to move to a new location/waypoint. Once oCreature
     *   reaches its destination it will begin an ambient behaviour animation phase.
     *
     *   @param oCreature  The creature moving.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_Move(GameObject oCreature)
     {
          int bMoving = EngineConstants.FALSE;

          int nMovePattern = GetLocalInt(oCreature, EngineConstants.AMBIENT_MOVE_PATTERN);

          int nRunChance = nMovePattern / 10;     // 2nd digit is the likelihood (0-9) of running (0 = 0%, 1 = 10%, etc).
          int bRun = (nRunChance > Engine_Random(9)) ? EngineConstants.TRUE : EngineConstants.FALSE;

          nMovePattern = nMovePattern % 10;     // 1st digit is the 'real' move pattern.

          switch (nMovePattern)
          {
               case EngineConstants.AMBIENT_MOVE_NONE:
                    Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Move()", "No EngineConstants.AMBIENT_MOVE_PATTERN");

                    // Not moving so do animation phase instead.
                    if ((GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN) != EngineConstants.FALSE || 
                         GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN_OVERRIDE) != EngineConstants.FALSE))
                    {
                         SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_RESET);
                         Ambient_Animate(oCreature);
                         return;
                    }
                    break;
               case EngineConstants.AMBIENT_MOVE_PATROL:
               case EngineConstants.AMBIENT_MOVE_LOOP:
               case EngineConstants.AMBIENT_MOVE_WARP:
               case EngineConstants.AMBIENT_MOVE_ONCE:
               case EngineConstants.AMBIENT_MOVE_RANDOM:
                    bMoving = Ambient_WalkWaypoints(oCreature, nMovePattern, bRun);
                    break;
               case EngineConstants.AMBIENT_MOVE_WANDER:
                    bMoving = Ambient_MoveRandom(oCreature, EngineConstants.AMBIENT_RANGE_NEAR, bRun);
                    break;
               case EngineConstants.AMBIENT_MOVE_WANDER_FAR:
                    bMoving = Ambient_MoveRandom(oCreature, EngineConstants.AMBIENT_RANGE_FAR, bRun);
                    break;
               case EngineConstants.AMBIENT_MOVE_PATH_PATROL:
               case EngineConstants.AMBIENT_MOVE_PATH_LOOP:
                    bMoving = Ambient_MovePath(oCreature, (nMovePattern == EngineConstants.AMBIENT_MOVE_PATH_LOOP) ? EngineConstants.TRUE : EngineConstants.FALSE, bRun);
                    break;
               default:
                    Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_Move()", "Invalid EngineConstants.AMBIENT_MOVE_PATTERN");

                    // Trigger animation phase if valid and not already doing looping animation (check first to prevent endless cycling).
                    if ((GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN) != EngineConstants.FALSE || 
                         GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN_OVERRIDE) != EngineConstants.FALSE))
                         WR_AddCommand(oCreature, CommandWait(EngineConstants.AMBIENT_SMALL_DELAY));
                    break;
          }
          /*    
              if (bMoving)
              {
                  // Mark as busy to prevent generation of EngineConstants.EVENT_TYPE_AMBIENT_CONTINUE events by engine.
                  int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);
                  SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, nAmbientState | EngineConstants.AMBIENT_SYSTEM_RUNNING);
              }
          */
          // Movement phase done - animation phase is next.
          SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_RESET);
     }


     ////////////////////////////////////////////////////////////////////////////////
     /* @brief  Checks period since last EVENT_AMBIENT_CONTINUE xEvent was received.
     *
     *   @param  oCreature       The creature performing the ambient behaviour.
     *   @param  bUpdateTimer    EngineConstants.TRUE if timer should be updated with current timestamp.
     *   @returns                EngineConstants.TRUE if timer expired (meaning creature should cease ambient animations)
     **//////////////////////////////////////////////////////////////////////////////
     public int Ambient_TimerExpired(GameObject oCreature, int bUpdateTimer)
     {
          int nTimeNow = GetLowResTimer();
          int nTimeLast = GetLocalInt(oCreature, EngineConstants.AMBIENT_TICK_COUNT);

          if (nTimeLast == 0)
               nTimeLast = nTimeNow;

          if (bUpdateTimer != EngineConstants.FALSE)
          {
               SetLocalInt(oCreature, EngineConstants.AMBIENT_TICK_COUNT, nTimeNow);
          }
          else if (abs(nTimeNow - nTimeLast) > EngineConstants.AMBIENT_PAUSE_PERIOD * 1000)
          {
               // Pause ambient behaviour if party out of range for too long.
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_TimerExpired()", "Pausing (" + ToString(nTimeNow) + " - " + ToString(nTimeLast) + " = " + ToString(nTimeNow - nTimeLast));
               SetLocalInt(oCreature, EngineConstants.AMBIENT_TICK_COUNT, 0);
               return EngineConstants.TRUE;
          }
          return EngineConstants.FALSE;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief  Causes a creature to perform the next appropriate ambient behaviour (i.e. movement or animation).
     *
     *   @param  oCreature       The creature performing the ambient behaviour.
     *   @param  bUpdateTimer
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_DoSomething(GameObject oCreature = null, int bUpdateTimer = EngineConstants.FALSE)
     {
          if (oCreature == null) oCreature = gameObject;
          if (GetObjectActive(oCreature) == EngineConstants.FALSE || IsDead(oCreature) != EngineConstants.FALSE || IsPartyMember(oCreature) != EngineConstants.FALSE)
               return;

          int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);

          // Check whether ambient behaviour is enabled.
          if (nAmbientState != EngineConstants.AMBIENT_SYSTEM_ENABLED)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoSomething()", "AMBIENT_SYSTEM_ENABLED bit not set (nAmbientState: " + IntToHexString(nAmbientState) + ")");
               return;
          }

          // Check if creature already doing or about to do something.
          if (GetCommandType(GetCurrentCommand(oCreature)) != EngineConstants.COMMAND_TYPE_INVALID || GetCommandQueueSize(oCreature) > 0)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoSomething()", "Busy - command(s) in queue: " + Log_GetCommandNameById(GetCommandType(GetCurrentCommand(oCreature))));
               return;
          }

          // The EngineConstants.AMBIENT_SYSTEM_ALWAYSON flag skips timer and combat checks (ergo, use it sparingly).
          if (nAmbientState != EngineConstants.AMBIENT_SYSTEM_ALWAYSON)
          {
               // Check whether creature or player is in combat
               if (GetGameMode() == EngineConstants.GM_COMBAT || GetCombatState(oCreature) != EngineConstants.FALSE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoSomething()", "Busy - in combat");
                    return;
               }

               // Check whether ambient behaviour should start/continue or time out.
               if (Ambient_TimerExpired(oCreature, bUpdateTimer) != EngineConstants.FALSE)
                    return;
          }

          if (Ambient_DoCommand(oCreature) != EngineConstants.FALSE)
               return;

          //    if (GetGroupId(oCreature) == EngineConstants.GROUP_HOSTILE)
          //        return;

          if (Ambient_ReachedDestination(oCreature) != EngineConstants.FALSE)
          {
               // Play animations if in an animation phase, otherwise attempt a movement phase.
               if (GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE) != EngineConstants.AMBIENT_ANIM_NONE)
                    Ambient_Animate(oCreature);
               else
                    Ambient_Move(oCreature);
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoSomething()", "Do nothing");
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Returns EngineConstants.TRUE if a creature's ambient behaviour has been overridden.
     *
     *   @param oCreature       The creature in question.
     **//////////////////////////////////////////////////////////////////////////////
     public int Ambient_IsBehaviourOverridden(GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          return (GetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_OVERRIDE_COUNT) != 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Overrides a creature's predefined ambient behaviour.
     *
     *   Overrides the ambient behaviour animation pattern and animation frequency
     *   defined by the creature template. -1.0f to play all animations in order)
     *
     *   @param oCreature       The creature performing the ambient behaviour.
     *   @param nAnimPattern    The override animation pattern (index into ambient_ai.xls).
     *   @param fAnimFreq       The override animation frequency (see description).
     *   @param nOverrideCount  The number of times the override animation pattern is to be played (-1 for infinite).
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_OverrideBehaviour(GameObject oCreature, int nAnimPattern, float fAnimFreq, int nOverrideCount)
     {
          if (GetObjectType(oCreature) != EngineConstants.OBJECT_TYPE_CREATURE
              || GetObjectActive(oCreature) == EngineConstants.FALSE || IsDead(oCreature) != EngineConstants.FALSE || IsPartyMember(oCreature) != EngineConstants.FALSE
              || GetGroupId(oCreature) == EngineConstants.GROUP_HOSTILE)
               return;

          int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);

          if (nAmbientState == EngineConstants.AMBIENT_SYSTEM_ENABLED)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_OverrideBehaviour()", "nAnimPattern: " + ToString(nAnimPattern) + ", fAnimFreq: " + ToString(fAnimFreq) + ", nOverrideCount: " + ToString(nOverrideCount));

               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN_OVERRIDE, nAnimPattern);
               SetLocalFloat(oCreature, EngineConstants.AMBIENT_ANIM_FREQ_OVERRIDE, fAnimFreq);
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_RESET);
               SetLocalInt(oCreature, EngineConstants.AMBIENT_ANIM_OVERRIDE_COUNT, nOverrideCount);

               SignalEvent(oCreature, Event(EngineConstants.EVENT_TYPE_AMBIENT_CONTINUE));
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Restores a creature's ambient animation behaviour to that defined in the creature's template.
     *
     *   @param oCreature  The creature whose ambient animation behaviour to restore.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_RestoreBehaviour(GameObject oCreature)
     {
          Ambient_OverrideBehaviour(oCreature, EngineConstants.AMBIENT_ANIM_PATTERN_NONE, EngineConstants.AMBIENT_ANIM_FREQ_NONE, 0);
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Forces a creature to repeatedly attack an object.
     *
     *   @param oAttacker    The attacking creature.
     *   @param fDelay       Delay to wait (in seconds) before attacking again.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_DoCommandAttack(GameObject oAttacker, float fDelay)
     {
          Debug.LogWarning("system ambient: ambient do command attack: handle event");
          xEvent ev = new xEvent(EngineConstants.EVENT_TYPE_INVALID);//GetCurrentEvent();
          switch (GetEventTypeRef(ref ev))
          {
               // Ambient_DoCommand() can be invoked by the xEvent handler script of an
               // area or a creature. Hence the odd collection of events below.

               case EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT:   // Invoked from area's xEvent handler script.
               case EngineConstants.EVENT_TYPE_AREALOAD_POSTLOADEXIT:  // Invoked from area's xEvent handler script.
               case EngineConstants.EVENT_TYPE_SPAWN:                  // Invoked from creature's xEvent handler script.
               case EngineConstants.EVENT_TYPE_OBJECT_ACTIVE:          // Invoked from creature's xEvent handler script.
               case EngineConstants.EVENT_TYPE_AMBIENT_CONTINUE:       // Invoked from creature's xEvent handler script.
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoCommandAttack()", "Kickstarting " + ToString(oAttacker));

                         WR_AddCommand(oAttacker, CommandWait(fDelay));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_COMMAND_COMPLETE:       // Invoked from creature's xEvent handler script.
                    {
                         switch (GetEventIntegerRef(ref ev, 0))
                         {
                              case EngineConstants.COMMAND_TYPE_WAIT:
                                   {
                                        // Attack target if player is wandering around. Otherwise, wait a bit.
                                        // Expected attacker tag format: XXXcr_XXX.
                                        // Expected target tag format:   XXXip_XXX_target.
                                        switch (GetGameMode())
                                        {
                                             case EngineConstants.GM_EXPLORE:
                                             case EngineConstants.GM_FIXED:
                                             case EngineConstants.GM_FLYCAM:
                                                  {
                                                       string sTargetTag = ReplaceString(GetTag(oAttacker), "cr_", "ip_") + "_target";
                                                       List<GameObject> aTarget = GetNearestObjectByTag(oAttacker, sTargetTag);

                                                       if (GetArraySize(aTarget) == 0)
                                                            aTarget = GetNearestObjectByTag(oAttacker, GetTag(oAttacker) + "_target");

                                                       if (GetArraySize(aTarget) > 0)
                                                       {
                                                            if (IsDead(aTarget[0]) != EngineConstants.FALSE)
                                                                 Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoCommandAttack()", "WARNING: Target is dead - " + ToString(aTarget[0]));

                                                            // Only shoot if player isn't in the way.
                                                            float fAngleToPlayer = GetAngleBetweenObjects(oAttacker, GetMainControlled());
                                                            if ((fAngleToPlayer < 20.0f || fAngleToPlayer > 340.0f) && GetDistanceBetween(oAttacker, GetMainControlled()) < GetDistanceBetween(oAttacker, aTarget[0]))
                                                                 WR_AddCommand(oAttacker, CommandWait(4.0f));
                                                            else
                                                                 WR_AddCommand(oAttacker, CommandAttack(aTarget[0]));
                                                       }
                                                       else
                                                       {
                                                            Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoCommandAttack()", "Failed to find valid target");
                                                       }
                                                       break;
                                                  }
                                             default:
                                                  {
                                                       // Wait a bit till combat or conversation ends.
                                                       WR_AddCommand(oAttacker, CommandWait(6.0f));
                                                       break;
                                                  }
                                        }
                                        break;
                                   }
                              case EngineConstants.COMMAND_TYPE_ATTACK:
                                   {
                                        Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoCommandAttack()", "Wait.");

                                        // Wait a bit before attacking again.
                                        WR_AddCommand(oAttacker, CommandWait(fDelay));
                                        break;
                                   }
                         }
                         break;
                    }
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Triggers ambient commands.
     *
     *   @param oCreature  The creature to assign an ambient command.
     *   @param nCommand   The xCommand to perform (EngineConstants.AMBIENT_COMMAND_***). If zero, uses the EngineConstants.AMBIENT_COMMAND local integer set on oCreature.
     *   @returns          EngineConstants.TRUE if oCreature has an ambient xCommand to perform. EngineConstants.FALSE otherwise.
     **//////////////////////////////////////////////////////////////////////////////
     public int Ambient_DoCommand(GameObject oCreature = null, int nCommand = 0)
     {
          if (oCreature == null) oCreature = gameObject;
          if (nCommand == 0)
          {
               nCommand = GetLocalInt(oCreature, EngineConstants.AMBIENT_COMMAND);
          }
          else
          {
               SetLocalInt(oCreature, EngineConstants.AMBIENT_COMMAND, nCommand);

               // Hack: Originally EngineConstants.AMBIENT_COMMAND didn't require the EngineConstants.AMBIENT_SYSTEM_ENABLED bit set. Now it does, so flip the bit here.
               int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);
               SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, nAmbientState | EngineConstants.AMBIENT_SYSTEM_ENABLED);
          }

          if (nCommand != EngineConstants.FALSE)
          {
               switch (nCommand)
               {
                    case EngineConstants.AMBIENT_COMMAND_ATTACK_PRACTICE:
                         Ambient_DoCommandAttack(oCreature, RandomFloat() * 5.0f);
                         break;
                    case EngineConstants.AMBIENT_COMMAND_ATTACK:
                         Ambient_DoCommandAttack(oCreature, 0.05f);
                         break;
                    default:
                         Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "Ambient_DoCommand()", "Unhandled EngineConstants.AMBIENT_COMMAND value.");
                         break;
               }
          }
          return nCommand;
     }

     ////////////////////////////////////////////////////////////////////////////////
     /* @brief Called from xEvent handler for EngineConstants.EVENT_TYPE_COMMAND_COMPLETE
     *
     *   @param nCommandType   The type of the xCommand that completed.
     *   @param nCommandStatus The status of the xCommand that completed.
     *   @param oCreature      The creature that completed the command.
     **//////////////////////////////////////////////////////////////////////////////
     public void Ambient_CommandComplete(int nCommandType, int nCommandStatus, GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          if (IsPartyMember(oCreature) != EngineConstants.FALSE || GetCombatState(oCreature) != EngineConstants.FALSE)
               return;

          if (nCommandType == EngineConstants.COMMAND_TYPE_INTERACT || nCommandType == EngineConstants.COMMAND_TYPE_PLAY_ANIMATION)
          {
               if (nCommandStatus == EngineConstants.COMMAND_LOOPING)
                    return;
               if (nCommandStatus == EngineConstants.COMMAND_SUCCESSFUL)
               {
                    int nAmbientState = GetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE);
                    SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, nAmbientState & ~EngineConstants.AMBIENT_SYSTEM_RUNNING);
               }
          }

          Ambient_DoSomething();
     }
}