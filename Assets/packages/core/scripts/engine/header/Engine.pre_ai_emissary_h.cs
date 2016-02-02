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
     //  pre_ai_emissary_h
     //------------------------------------------------------------------------------
     //
     //  Genlock Emissary AI Header
     //
     //      Functions and constants used by the custom ai script of the Genlock
     //      Emissary on the third level of the Signal Tower.
     //
     //------------------------------------------------------------------------------
     //  2007/09/05 - Owner: Grant Mackay
     //------------------------------------------------------------------------------

     //#include"wrappers_h"
     //#include"pre_objects_h"
     //#include"utility_h"
     //#include"ai_constants_h"   
     //#include"ai_main_h_2"

     //------------------------------------------------------------------------------
     //  CONSTANTS
     //------------------------------------------------------------------------------

     //moved public const int EngineConstants.EMISSARY_CUSTOM_AI_RETREAT        = 1500;
     //moved public const int EngineConstants.EMISSARY_CUSTOM_AI_WAIT           = 1501;
     //moved public const int EngineConstants.EMISSARY_CUSTOM_AI_DISABLE        = 1502;
     //moved public const int EngineConstants.EMISSARY_CUSTOM_AI_TRIGGER_EVENT  = 1503;
     //moved public const int EngineConstants.EMISSARY_RETREATING               = 2000;
     //moved public const int EngineConstants.EMISSARY_NOT_RETREATING           = 0;
     //moved public const int EngineConstants.EMISSARY_CUSTOM_AI_INACTIVE       = 0;

     //moved public const string EngineConstants.EMISSARY_FINISHED_RETREATING   = EngineConstants.CREATURE_DO_ONCE_A;
     //moved public const string EngineConstants.EMISSARY_WAYPOINT              = EngineConstants.CREATURE_COUNTER_1;
     //moved public const string EngineConstants.EMISSARY_ACTIVELY_RETREATING   = EngineConstants.CREATURE_COUNTER_2;

     //------------------------------------------------------------------------------
     // FUNCTION IMPLEMENTATIONS
     //------------------------------------------------------------------------------
     /*
      * @brief applies the AOE
      *
      * Applies the Area of Effect which drives the emissary's retreat.
      *
      * @param oCreature - The creature on which the AoE is applied.
      * @param oCreature - The script that runs on the AoE.
      * @author Craig Graff
      */
     public void Emissary_ApplyAOE(GameObject oCreature, string rScript)
     {
          xEffect eAOE = EffectAreaOfEffect(EngineConstants.VFX_PER_DEN_GUARD_MEDIUM, rScript);
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eAOE, oCreature, 0.0f, oCreature);

     }

     /*
 * @brief Checks custom retreat AI
 *
 * Checks if the emissary is currently retreating.
 *
 * @param oCreature - The creature on which the retreat AI is checked.
 * @author Craig Graff
 */
     public int Emissary_IsRetreating(GameObject oCreature)
     {
          return (GetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE) == EngineConstants.EMISSARY_CUSTOM_AI_RETREAT) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
 * @brief Enables custom retreat AI
 *
 * Causes the creature passed into the function to enable custom AI which will
 * allow it to retreat to a nearby waypoint with an appropriate tag.
 *
 * @param oCreature - The creature on which the retreat AI should be activated.
 * @author Grant Mackay
 */
     public void Emissary_ActivateRetreat(GameObject oCreature)
     {

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "Emissary_ActivateRetreat", "START");
          // This behaviour is disabled once the emissary crosses a trigger.
          if (GetLocalInt(oCreature, EngineConstants.EMISSARY_FINISHED_RETREATING) == EngineConstants.FALSE)
          {
               SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.EMISSARY_CUSTOM_AI_RETREAT);
          }

     }

     public void Emissary_RetreatToNextPoint(GameObject oCreature, string sWayPointBase, int nNextAIState = EngineConstants.EMISSARY_CUSTOM_AI_WAIT)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "Emissary_RetreatToNextPoint", "START");
          int nNextWP = GetLocalInt(oCreature, EngineConstants.EMISSARY_WAYPOINT);
          string sNextWP;
          GameObject oNextWP;

          nNextWP++;
          sNextWP = sWayPointBase + IntToString(nNextWP);
          oNextWP = UT_GetNearestObjectByTag(oCreature, sNextWP);
          if (IsObjectValid(oNextWP) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "Emissary_RetreatToNextPoint", "No more vald WPs");
               SetLocalInt(oCreature, EngineConstants.EMISSARY_FINISHED_RETREATING, EngineConstants.TRUE);
               SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.FALSE);
               xCommand cWait = CommandWait(0.5f);
               WR_AddCommand(oCreature, cWait, EngineConstants.TRUE);
          }
          //ClearCurrentCommand(oCreature);
          WR_AddCommand(oCreature, CommandMoveToObject(oNextWP, EngineConstants.TRUE), EngineConstants.TRUE, EngineConstants.COMMAND_ADDBEHAVIOR_HARDCLEAR);
          //UT_QuickMoveObject(oCreature, sNextWP, EngineConstants.TRUE, EngineConstants.TRUE, EngineConstants.TRUE);

          SetLocalInt(oCreature, EngineConstants.EMISSARY_WAYPOINT, nNextWP);
          SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, nNextAIState);
          SetLocalInt(oCreature, EngineConstants.EMISSARY_ACTIVELY_RETREATING, EngineConstants.EMISSARY_RETREATING);
     }

     /*
 * @brief Causes a creature to retreat to a nearby waypoint.
 *
 * Causes the creature passed into the function to retreat to a nearby waypoint
 * where the creature will wait until it comes into contact with hostiles again.
 *
 * @param oCreature - The creature which will retreat.
 * @author Grant Mackay
 */
     public void Emissary_Retreat(GameObject oCreature, string sWayPoint = EngineConstants.PRE_WP_TOWER_EMMISARY_RETREAT)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "Emissary_Retreat", "START");
          // Retreat to the nearest waypoint.
          List<GameObject> arWaypoints = GetNearestObjectByTag(oCreature, sWayPoint, EngineConstants.OBJECT_TYPE_WAYPOINT, 1);
          GameObject oWaypoint = arWaypoints[0];

          if (IsObjectValid(oWaypoint) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "Emissary_Retreat", "No valid WPs to move to");
               SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.EMISSARY_CUSTOM_AI_WAIT);
               SetLocalInt(oCreature, EngineConstants.EMISSARY_ACTIVELY_RETREATING, EngineConstants.EMISSARY_NOT_RETREATING);
               AI_DetermineCombatRound();
               return;
          }
          Vector3 lRunTo = GetLocation(oWaypoint);

          WR_ClearAllCommands(oCreature);
          WR_AddCommand(oCreature, CommandMoveToLocation(lRunTo), EngineConstants.TRUE, EngineConstants.TRUE);

          WR_DestroyObject(oWaypoint);

          SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.EMISSARY_CUSTOM_AI_WAIT);
          SetLocalInt(oCreature, EngineConstants.EMISSARY_ACTIVELY_RETREATING, EngineConstants.EMISSARY_RETREATING);
     }

     /*
 * @brief Determines if the nearest hostile creature is within range.
 *
 * Checks to see if the nearest Hostile creature is within the specified
 * distance.
 *
 * @param oCreature - The creature being checked.
 * @param fDistance - The distance to be checked against.
 * @returns EngineConstants.TRUE if the nearest hostile is within fDistance of oCreature.
 */
     public int Emissary_CheckNearestHostile(GameObject oCreature, float fDistance)
     {

          List<GameObject> arHostile = GetNearestObjectByHostility(oCreature, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_CREATURE);

          GameObject oHostile = arHostile[0];

          float fDistanceBetween = GetDistanceBetween(oCreature, oHostile);

          if (fDistanceBetween < fDistance)
          {

               return EngineConstants.TRUE;

               //if (CheckLineOfSightObject(oCreature, oHostile))
               //    return EngineConstants.TRUE;

          }

          return EngineConstants.FALSE;

     }
}