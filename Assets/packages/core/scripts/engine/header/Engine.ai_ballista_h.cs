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
     //#include"ai_constants_h"
     //#include"log_h"
     //#include"utility_h"
     //#include"ai_conditions_h"

     /*
      * @brief Handles custom AI for a ballista using creature.
      *
      * Determines which custom AI action should be taken fromt he creature's local
      * custom AI variables and attempts to execute the appropriate action. Sets
      * the creature's AI variable to the next sequential action or clears it.
      *
      * @author Grant Mackay
      */
     public int AI_Ballista_HandleAI()
     {
          int nBallistaAIStatus = GetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS);

          Log_Trace_AI("AI_Ballista_HandleAI", "Ballista AI value: " + IntToString(nBallistaAIStatus));

          // abort AI if enemies are within melee range
          List<GameObject> arEnemies = _AI_GetEnemies(-1, -1);
          GameObject oNearestEnemy = arEnemies[0];
          float fDistance = GetDistanceBetween(gameObject, oNearestEnemy);
          if (IsObjectValid(oNearestEnemy) != EngineConstants.FALSE && fDistance <= EngineConstants.AI_MELEE_RANGE)
          {
               Log_Trace_AI("AI_Ballista_HandleAI", "Got enemy in melee ranged - aborting ballista AI");
               return EngineConstants.FALSE;
          }

          // First, try to find a ballista if one is nearby
          if (nBallistaAIStatus == EngineConstants.AI_BALLISTA_SHOOTER_STATUS_AVAILABLE)
          {
               GameObject oBallista = UT_GetNearestObjectByTag(gameObject, EngineConstants.AI_BALLISTA_TAG);
               if (IsObjectValid(oBallista) == EngineConstants.FALSE || IsDead(oBallista) != EngineConstants.FALSE)
               {
                    Log_Trace_AI("AI_Ballista_HandleAI", "Could not find a valid and non-destroyed ballista - aborting ballista AI");
                    SetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS, EngineConstants.AI_BALLISTA_SHOOTER_STATUS_AVAILABLE);
                    return EngineConstants.FALSE;
               }
               fDistance = GetDistanceBetween(oBallista, gameObject);
               if (GetLocalInt(oBallista, EngineConstants.PLC_COUNTER_1) == 0 && fDistance <= EngineConstants.AI_BALLISTA_MAX_USE_DISTANCE)
               {
                    Log_Trace_AI("AI_Ballista_HandleAI", "Assigning creature to ballista: " + GetTag(oBallista));
                    // Flag the ballista as in use.
                    SetLocalInt(oBallista, EngineConstants.PLC_COUNTER_1, EngineConstants.TRUE);
                    SetLocalObject(gameObject, EngineConstants.AI_PLACEABLE_BEING_USED, oBallista);
                    SetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS, EngineConstants.AI_BALLISTA_SHOOTER_STATUS_MOVE_TO_BALLISTA);
                    nBallistaAIStatus = EngineConstants.AI_BALLISTA_SHOOTER_STATUS_MOVE_TO_BALLISTA;
               }
               else // balista is in use or too far away
               {
                    Log_Trace_AI("AI_Ballista_HandleAI", "Could not find a ballista - reverting to normal AI for 1 round");
                    return EngineConstants.FALSE;
               }
          }

          // found a ballista - do something with it
          GameObject oCurrentBallista = GetLocalObject(gameObject, EngineConstants.AI_PLACEABLE_BEING_USED);
          if (IsObjectValid(oCurrentBallista) == EngineConstants.FALSE || IsDead(oCurrentBallista) != EngineConstants.FALSE)
          {
               Log_Trace_AI("AI_Ballista_HandleAI", "INVALID OR DEAD BALLISTA OBJECT - aborting ballista AI");
               SetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS, EngineConstants.AI_BALLISTA_SHOOTER_STATUS_AVAILABLE);
               return EngineConstants.FALSE;
          }

          switch (nBallistaAIStatus)
          {
               case EngineConstants.AI_BALLISTA_SHOOTER_STATUS_MOVE_TO_BALLISTA:
                    {
                         AI_Ballista_MoveToBallista();
                         break;
                    }
               // Use the ballista.
               case EngineConstants.AI_BALLISTA_SHOOTER_STATUS_USE_BALLISTA:
                    {
                         AI_Ballista_UseBallista();
                         break;
                    }
               // Fire the ballista (user animation)
               case EngineConstants.AI_BALLISTA_SHOOTER_STATUS_FIRE:
                    {
                         AI_Ballista_FireBallista();
                         break;
                    }
               // Waiting for reload.
               case EngineConstants.AI_BALLISTA_SHOOTER_STATUS_WAIT_FOR_BALLISTA:
                    {
                         AI_Ballista_WaitForBallista();
                         break;
                    }
               // Reset the custom AI variable.
               default:
                    {
                         SetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS, EngineConstants.AI_BALLISTA_SHOOTER_STATUS_INACTIVE);
                         return EngineConstants.FALSE;
                    }
          }
          return EngineConstants.TRUE;
     }

     /*
 * @brief Has the subject move to the nearest ballista.
 *
 * Finds the nearest ballista, clears the subject's actions and moves them
 * to the ballista. Sets the custom AI to the next succesive action, in this
 * case using the ballista.
 *
 * @author Grant Mackay
 */
     public void AI_Ballista_MoveToBallista()
     {
          Log_Trace_AI("AI_Ballista_MoveToBallista", "START");
          GameObject oBallista = GetLocalObject(gameObject, EngineConstants.AI_PLACEABLE_BEING_USED);

          xCommand cMove = CommandMoveToObject(oBallista, EngineConstants.TRUE, 1.5f);
          WR_AddCommand(gameObject, cMove);
          SetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS, EngineConstants.AI_BALLISTA_SHOOTER_STATUS_FIRE);
     }

     /*
 * @brief Has the subject use the nearest ballista.
 *
 * Finds the nearest ballista, clears the subject's action queue and has
 * them use the ballista. Sets the custom AI to the next succesive action, in
 * this case waiting for the ballista to be available for usage again or
 * alternately deactivating custom AI if enemies are drawing near.
 *
 * @author Grant Mackay
 */
     public void AI_Ballista_UseBallista()
     {
          Log_Trace_AI("AI_Ballista_UseBallista", "START");
          GameObject oBallista = GetLocalObject(gameObject, EngineConstants.AI_PLACEABLE_BEING_USED);

          // Use the ballista 
          xCommand cUse = CommandUseObject(oBallista, EngineConstants.PLACEABLE_ACTION_USE);
          WR_AddCommand(gameObject, cUse);

          SetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS, EngineConstants.AI_BALLISTA_SHOOTER_STATUS_WAIT_FOR_BALLISTA);
     }

     /*
 * @brief Has the subject wait until the ballista has fired before using it.
 *
 * Queues a wait xCommand on the creature and sets the Custom AI to use the
 * ballista on the next command.
 *
 * @author Grant Mackay
 */
     public void AI_Ballista_WaitForBallista()
     {
          Log_Trace_AI("AI_Ballista_WaitForBallista", "START");
          xCommand cReload = CommandWait(2.5f); // should be a re-load animation of some sort
                                                //xCommand cReload = CommandPlayAnimation(259);

          WR_AddCommand(gameObject, cReload);

          SetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS, EngineConstants.AI_BALLISTA_SHOOTER_STATUS_FIRE);
     }

     // fire the thing
     public void AI_Ballista_FireBallista()
     {
          Log_Trace_AI("AI_Ballista_FireBallista", "START");

          xCommand cFire = CommandPlayAnimation(107);
          WR_AddCommand(gameObject, cFire);
          SetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS, EngineConstants.AI_BALLISTA_SHOOTER_STATUS_USE_BALLISTA);
     }

     /*
 * @brief Clears the in-use flag of a ballista the creature might be using.
 *
 * Checks to see if the creature has flagged a Ballista for use and, if so,
 * un-flags the Ballista.
 *
 * @author Grant Mackay
 */
     public void AI_Ballista_HandleDeath()
     {
          int nBallistaAI = GetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS);
          // Using or about to use a ballista.
          if (nBallistaAI != EngineConstants.FALSE)
          {
               GameObject oBallista = GetLocalObject(gameObject, EngineConstants.AI_PLACEABLE_BEING_USED);
               SetLocalObject(gameObject, EngineConstants.AI_PLACEABLE_BEING_USED, null);

               // Clear the ballista's 'in-use' flag.
               SetLocalInt(oBallista, EngineConstants.PLC_COUNTER_1, 0);
          }
     }
}