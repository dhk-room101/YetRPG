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
     //------------------------------------------------------------------------------
     // pre440_darkspawn_h
     // Copyright � 2003 Bioware Corp.
     //------------------------------------------------------------------------------
     //
     // Include for handling the encoutner at the top of the tower wherein
     // darkspawn overwhelm the player.
     //
     //------------------------------------------------------------------------------
     // June 2008 - Owner: Grant Mackay
     //------------------------------------------------------------------------------

     //#include"log_h"
     //#include"utility_h"
     //#include"wrappers_h"
     //#include"events_h"

     //#include"plt_pre100pt_light_beacon"

     //#include"ai_main_h_2"

     //------------------------------------------------------------------------------
     // CONSTANTS
     //------------------------------------------------------------------------------

     // Waypoint names for darkspawn movement.
     //moved public const string PRE_TOWR_DARKSPAWN_PREFIX = "pre440cr_darkspawn_";
     //moved public const string PRE_WP_DARKSPAWN_ENTRANCE = "pre440wp_darkspawn_enter";
     //moved public const string PRE_WP_DARKSPAWN_JUMP_IN  = "pre440wp_darkspawn_jump";
     //moved public const string PRE_WP_ARCHER_DESTINATION = "pre440wp_archer";

     // Maximum and minimum darkspawn in the area at any one time.
     //moved public const int PRE_N_MAX_DARKSPAWN = 20;
     //moved public const int PRE_N_MIN_DARKSPAWN = 6;

     // Wayppoints available for archers to scatter to.
     //moved public const int PRE_N_DESTINATION_WAYPOINTS  =  3;

     // Delay between checks to see if Darkspawn should be activated and activation.
     //moved public const float PRE_F_DARKSPAWN_CHECK_DELAY      = 5.0f;
     //moved public const float PRE_F_DARKSPAWN_ACTIVATION_DELAY = 2.0f;

     // Darkspawn Custom AI states.
     //moved public const int PRE_AI_KNOCKDOWN = 1000;
     //moved public const int PRE_AI_WAIT      = 1001;
     //moved public const int PRE_AI_CUTSCENE  = 1002;      

     //------------------------------------------------------------------------------
     // FUNCTION DEFINITIONS
     //------------------------------------------------------------------------------
     /* @brief Activates the next darkspawn in order.
      *
      * Activates the next available darkspawn on the fourth floor of the tower
      * based on the first area counter.
      *
      * @param oArea The area the darkspawn encounter takes place in.
      * @author Grant Mackay
      **/
     public void PRE_ActivateDarkspawn(GameObject oArea = null)
     {
          if (oArea == null) oArea = gameObject;
          GameObject oJump, oEnter, oDarkspawn;
          string sDarkspawn;
          int nDarkspawn, nCount;

          // Determine the next in line.
          nDarkspawn = GetLocalInt(oArea, EngineConstants.AREA_COUNTER_1);
          sDarkspawn = EngineConstants.PRE_TOWR_DARKSPAWN_PREFIX + IntToString(nDarkspawn);

          // Gather the darkspawn and destination waypoints.
          oDarkspawn = GetObjectByTag(sDarkspawn);
          oEnter = GetObjectByTag(EngineConstants.PRE_WP_DARKSPAWN_ENTRANCE);
          oJump = GetObjectByTag(EngineConstants.PRE_WP_DARKSPAWN_JUMP_IN);

          // Verify the Darkspawn's validity.
          if (IsObjectValid(oDarkspawn)== EngineConstants.FALSE)
          {

               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, GetCurrentScriptName(), "Invalid creature retrieved with tag: " + sDarkspawn, gameObject);
               return;

          }

          // If it's an archer have them spread out.
          if (nDarkspawn % 4 == 1)
          {

               SetLocalInt(oDarkspawn, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CAI_INITIATE);
               SetLocalInt(oDarkspawn, EngineConstants.CREATURE_COUNTER_1, nDarkspawn % 3);

          }

          // Otherwise set up the knockdown custom AI loop.
          else
          {

               SetLocalInt(oDarkspawn, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CAI_INACTIVE);

          }

          //Activate the darkspawn.
          WR_SetObjectActive(oDarkspawn, EngineConstants.TRUE);

          // Jump him into the level at the bottom of the stairs.
          AddCommand(oDarkspawn, CommandJumpToObject(oJump), EngineConstants.TRUE, EngineConstants.TRUE);

          // Have the darkspawn run to the top of the stairs.
          AddCommand(oDarkspawn, CommandMoveToObject(oEnter), EngineConstants.FALSE, EngineConstants.TRUE);

          // Update the next in line counter.
          nDarkspawn++;

          // Store the new value.
          SetLocalInt(oArea, EngineConstants.AREA_COUNTER_1, nDarkspawn);

     }

     /* @brief Checks the current number of darkspawn in the area.
 *
 * Checks the number of darkspawn in the area and, if needed activates up to
 * five more aiming for a maximum of twenty at any given time.
 *
 * @param oArea The area the darkspawn encounter takes place in.
 * @author Grant Mackay
 **/
     public void PRE_CheckDarkspawn(GameObject oArea = null)
     {
          if (oArea == null) oArea = gameObject;
          int nDarkspawn, nActivate, nIndex;
          float fDelay;
          xEvent evActivate;

          // Establish the number of currently active darkspawn.
          nDarkspawn = GetLocalInt(oArea, EngineConstants.AREA_COUNTER_2);

          // If there are 20 abort.
          if (nDarkspawn > 19)
          {

               return;

          }

          // Activate darkspawn until there are at least MIN active.
          nActivate = EngineConstants.PRE_N_MIN_DARKSPAWN - nDarkspawn;

          // Never activate less than 1.
          if (nActivate < 1) nActivate = 1;

          // Initialize the spawn delay and activation event.
          fDelay = 0.0f;

          evActivate = Event(EngineConstants.EVENT_TYPE_CUSTOM_EVENT_02);

          // Call the activate function as needed.
          for (nIndex = 0; nIndex < nActivate; ++nIndex)
          {

               // Activate a Darkspawn.
               //PRE_ActivateDarpskawn(oArea);
               DelayEvent(fDelay, oArea, evActivate);

               // Increment the delay to avoid darlspawn displacement.
               fDelay += EngineConstants.PRE_F_DARKSPAWN_ACTIVATION_DELAY;

               // Increment the number of darkspawn in the area.
               nDarkspawn++;

          }

          // Store the new darkspawn number.
          SetLocalInt(oArea, EngineConstants.AREA_COUNTER_2, nDarkspawn);

     }

     /* @brief Checks to see if the rescue cutscene should be played.
 *  
 * Checks to determine if:
 * (1) The player is dead or dying.
 * (2) The player is knocked down.
 * If either of these conditions are true AND the cutscene plot flag has not
 * already been set the flag is set and, thus, the cutscene played.
 * 
 * @return EngineConstants.TRUE if the cutscene is ready to be  played. EngineConstants.FALSE otherwise.
 *
 * @author Grant Mackay
 **/
     public int PRE_CheckRescueCutscene()
     {

          GameObject oHero;
          int bDying, bDown, bRescued;

          oHero = GetHero();

          // Determine is the player is currently dead or dying.
          bDying = IsDeadOrDying(oHero);

          // Determine if the player is knocked.
          bDown = GetHasEffects(oHero, EngineConstants.EFFECT_TYPE_KNOCKDOWN);

          // Determine if the cutscene has been played or is playing.    
          bRescued = WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_LIGHT_BEACON, EngineConstants.PRE_BEACON_TRIGGER_RESCUE_CUTSCENE);

          // Dead/Dying OR Knocked Down.
          if (bDying != EngineConstants.FALSE || bDown != EngineConstants.FALSE)
          {

               // Cutscene has not yet played.
               if (bRescued == EngineConstants.FALSE)
               {

                    // Set the plot flag that will play the cutscene.
                    WR_SetPlotFlag(EngineConstants.PLT_PRE100PT_LIGHT_BEACON, EngineConstants.PRE_BEACON_TRIGGER_RESCUE_CUTSCENE, EngineConstants.TRUE, EngineConstants.TRUE);

                    return EngineConstants.TRUE;

               }

          }

          return EngineConstants.FALSE;

     }
}