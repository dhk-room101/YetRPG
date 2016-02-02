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

         Urn of Sacred Ashes
             -> High Dragon include file.

         Constants and functions used in the high dragon encounter.

     */
     //------------------------------------------------------------------------------
     // Created By: Grant Mackay
     // Created On: Feb 18, 2009
     //==============================================================================

     //#include"utility_h"
     //#include"wrappers_h"

     //#include"plt_urnpt_area_jumps"

     //------------------------------------------------------------------------------
     //  CONSTANTS
     //------------------------------------------------------------------------------

     //moved public const int EngineConstants.UHD_STATE_INVALID     = 0;
     //moved public const int EngineConstants.UHD_STATE_ASLEEP      = 1;
     //moved public const int EngineConstants.UHD_STATE_IDLE        = 2;
     //moved public const int EngineConstants.UHD_STATE_ABSENT      = 3;
     //moved public const int EngineConstants.UHD_STATE_ATTACK      = 4;
     //moved public const int EngineConstants.UHD_STATE_WARNING     = 5;

     //moved public const int UHD_EVENT_TYPE_WAIT               = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;
     //moved public const int UHD_EVENT_TYPE_START_WARNING      = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_02;
     //moved public const int UHD_EVENT_TYPE_SLEEP              = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_03;
     //moved public const int UHD_EVENT_TYPE_GO_INACTIVE        = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_04;
     //moved public const int UHD_EVENT_TYPE_WARNING_COMPLETE   = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_05;

     //------------------------------------------------------------------------------
     //  FUNCITON IMPLEMENTATIONS
     //------------------------------------------------------------------------------
     /*
      *  @brief Causes the dragon to fall from the sky on to the nearest creature.
      *
      *  Sets up an activation even that has the dragon land on a waypoint near
      *  the contolled character cause knockdown and damage.
      *
      *  @param oDragon The high dragon object.
      *
      */
     public void UHD_DragonPounce(GameObject oDragon)
     {

          // The dragon no longer accepts new state input.
          SetLocalInt(oDragon, EngineConstants.CREATURE_COUNTER_1, EngineConstants.UHD_STATE_INVALID);

          // Determine the nearest creature.
          GameObject oTarget = GetHero();
          // Nearest landing waypoint.
          GameObject oWaypoint = UT_GetNearestObjectByTag(oTarget, "ai_move");

          // Establish the landing event.
          xEvent eFlyDown = Event(EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE);
          SetEventVectorRef(ref eFlyDown, 0, GetLocation(oWaypoint));
          SetEventIntegerRef(ref eFlyDown, 3, EngineConstants.TRUE); // tells it to call an AI function

          SetGroupId(oDragon, EngineConstants.GROUP_HOSTILE);
          DelayEvent(4.0f, oDragon, eFlyDown);

     }

     /*
 *  @brief Sets the dragon's internal state.
 *
 *  Sets the state variable stored in EngineConstants.CREATURE_COUNTER_1 on the dragon and
 *  plays any animations associated with the animation.
 *
 *  @param oDragon The high dragon object.
 *  @param nState The state to set the dragon to.
 */
     public void UHD_SetState(GameObject oDragon, int nState)
     {

          // If the dragon has been placated, is in combat or dead the state is invalid.
          int bPlacated = WR_GetPlotFlag(EngineConstants.PLT_URNPT_AREA_JUMPS, EngineConstants.KOLGRIM_TO_MOUNTAIN_TOP);
          int bCombatState = GetCombatState(oDragon);
          int bDead = IsDead(oDragon);

          if (bPlacated != EngineConstants.FALSE || bCombatState != EngineConstants.FALSE || bDead != EngineConstants.FALSE)
          {
               SetLocalInt(oDragon, EngineConstants.CREATURE_COUNTER_1, EngineConstants.UHD_STATE_INVALID);
               return;
          }

          WR_ClearAllCommands(oDragon);

          // Determine the animation to play.
          int nAnimation, bActive;

          switch (nState)
          {

               case EngineConstants.UHD_STATE_ASLEEP:
                    nAnimation = 624;
                    bActive = EngineConstants.TRUE;
                    break;

               case EngineConstants.UHD_STATE_IDLE:
                    nAnimation = 626;
                    bActive = EngineConstants.TRUE;
                    break;

               case EngineConstants.UHD_STATE_ABSENT:
                    nAnimation = 0;
                    bActive = EngineConstants.FALSE;
                    break;

               case EngineConstants.UHD_STATE_ATTACK:
                    nAnimation = 0;
                    bActive = EngineConstants.FALSE;
                    UHD_DragonPounce(oDragon);
                    break;

               case EngineConstants.UHD_STATE_WARNING:
                    nAnimation = 2023;
                    bActive = EngineConstants.TRUE;
                    break;

               default:
                    nAnimation = 0;
                    bActive = EngineConstants.TRUE;
                    break;
          }

          // Play any needed animations.
          if (nAnimation != EngineConstants.FALSE)
               AddCommand(oDragon, CommandPlayAnimation(nAnimation), EngineConstants.TRUE);

          // Set the dragon inactive.
          //if (!bActive)
          //    WR_SetObjectActive(oDragon, EngineConstants.FALSE);
          WR_SetObjectActive(oDragon, bActive);

          SetLocalInt(oDragon, EngineConstants.CREATURE_COUNTER_1, nState);

     }

     /*
 *  @brief Returns the dragon's internal state.
 *
 *  Retrieves the state variable stored in EngineConstants.CREATURE_COUNTER_1 on the dragon.
 *
 *  @param oDragon The high dragon object.
 *  @return A constant representing the high dragon's current state.
 */
     public int UHD_GetState(GameObject oDragon)
     {

          // If the dragon has been placated, is in combat or dead the state is invalid.
          int bPlacated = WR_GetPlotFlag(EngineConstants.PLT_URNPT_AREA_JUMPS, EngineConstants.KOLGRIM_TO_MOUNTAIN_TOP);
          int bCombatState = GetCombatState(oDragon);
          int bDead = IsDead(oDragon);

          if (bPlacated != EngineConstants.FALSE || bCombatState != EngineConstants.FALSE || bDead != EngineConstants.FALSE)
               return EngineConstants.UHD_STATE_INVALID;

          return GetLocalInt(oDragon, EngineConstants.CREATURE_COUNTER_1);

     }
}