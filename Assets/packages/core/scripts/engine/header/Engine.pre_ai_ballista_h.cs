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
     //  pre_ai_ballista_h
     //------------------------------------------------------------------------------
     //
     //  Ballista User AI Header
     //
     //      Functions and constants used by the custom ai scripts of creatures using
     //      the various ballistae in the prelude.
     //
     //  Ballista Local Variables
     //
     //      EngineConstants.PLC_COUNTER_1: Used to track that a given ballista is manned. If the
     //          ballista is manned (has an NPC firing it when it's ready) this
     //          counter is EngineConstants.TRUE otherwise EngineConstants.FALSE.
     //
     //  Creature Local Variables
     //
     //      EngineConstants.CREATURE_COUNTER_1: Used to track the waiting state of the creature.
     //
     //
     //------------------------------------------------------------------------------
     //  2007/09/06 - Owner: Grant Mackay
     //------------------------------------------------------------------------------

     //#include"log_h"
     //#include"ai_main_h_2"
     //#include"wrappers_h"

     //------------------------------------------------------------------------------
     //  CONSTANTS
     //------------------------------------------------------------------------------

     // AI constants
     //moved public const int EngineConstants.CUSTOM_AI_INACTIVE            = 0;
     //moved public const int EngineConstants.CUSTOM_AI_MOVE_TO_BALLISTA    = 1500;
     //moved public const int EngineConstants.CUSTOM_AI_FIRE_BALLISTA       = 1501;
     //moved public const int EngineConstants.CUSTOM_AI_USE_BALLISTA        = 1502;
     //moved public const int EngineConstants.CUSTOM_AI_WAIT_FOR_BALLISTA   = 1503;

     //moved public const float EngineConstants.CUSTOM_AI_NEXT_STATE_DELAY  = 1.0f;

     // Tags
     //moved public const string EngineConstants.BALLISTA_TAG = "pre420ip_ballista";

     // DEBUG
     // public void main() {}


     //------------------------------------------------------------------------------
     //  FUNCTION IMPLEMENTATIONS
     //------------------------------------------------------------------------------
     /*
      * @brief Checks for an available ballista to use.
      *
      * Cycles the ballista to see if any are in range of the creature and not
      * currently in use by somone else. If any meet the requirements the creature's
      * custom AI is set to move to the ballista.
      *
      * @param oCreature - The creature seeking a ballista to use.
      * @author Grant Mackay
      */
     public void Ballista_CheckForBallista(GameObject oCreature)
     {

          // Determine if nearest ballista in the area aren't in use
          GameObject oBallista = UT_GetNearestObjectByTag(oCreature, EngineConstants.BALLISTA_TAG);

          // If the ballista is not in use.
          if (GetLocalInt(oBallista, EngineConstants.PLC_COUNTER_1) == EngineConstants.FALSE)
          {

               // Flag the ballista as in use.
               SetLocalInt(oBallista, EngineConstants.PLC_COUNTER_1, EngineConstants.TRUE);

               // Enable custom AI.
               SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CUSTOM_AI_MOVE_TO_BALLISTA);

          }

     }

     /*
 * @brief Handles custom AI for a ballista using creature.
 *
 * Determines which custom AI action should be taken fromt he creature's local
 * custom AI variables and attempts to execute the appropriate action. Sets
 * the creature's AI variable to the next sequential action or clears it.
 *
 * @param oCreature - The creature running the cusom AI.
 * @author Grant Mackay
 */
     public void Ballista_HandleCustomAI(GameObject oCreature)
     {

          // Determine the custom AI state.
          int nCustomAI = GetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE);

          Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, GetCurrentScriptName(), "nCumstomAI = " + IntToString(nCustomAI), gameObject);

          switch (nCustomAI)
          {

               // Moving to the nearest ballista.
               case EngineConstants.CUSTOM_AI_MOVE_TO_BALLISTA:
                    {

                         Ballista_MoveToBallista(oCreature);
                         break;

                    }
               // Animation to 'pull the trigger'
               case EngineConstants.CUSTOM_AI_FIRE_BALLISTA:
                    {

                         Ballista_FireBallista(oCreature);
                         break;

                    }
               // Firing the ballista.
               case EngineConstants.CUSTOM_AI_USE_BALLISTA:
                    {

                         Ballista_UseBallista(oCreature);
                         break;

                    }
               // Waiting for reload.
               case EngineConstants.CUSTOM_AI_WAIT_FOR_BALLISTA:
                    {

                         Ballista_WaitForBallista(oCreature);
                         break;
                    }
               // Reset the custom AI variable if an unknown state is reached.
               default:
                    {

                         SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CUSTOM_AI_INACTIVE);
                         break;

                    }

          }

     }

     /*
 * @brief Clears the in-use flag of a ballista the creature might be using.
 *
 * Checks to see if the creature has flagged a Ballista for use and, if so,
 * un-flags the Ballista.
 *
 * @param oCreature - The dying ballista user.
 * @author Grant Mackay
 */
     public void Ballista_HandleDeath(GameObject oCreature)
     {

          // Check for custom ai activity.
          int nCustomAI = GetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE);

          // Using or about to use a ballista.
          if (nCustomAI != EngineConstants.FALSE)
          {

               // Clear the ballista's 'in-use' flag.
               List<GameObject> arBallista = GetNearestObjectByTag(oCreature, EngineConstants.BALLISTA_TAG);

               SetLocalInt(arBallista[0], EngineConstants.PLC_COUNTER_1, EngineConstants.FALSE);

          }

     }

     /*
 * @brief Has the subject move to the nearest ballista.
 *
 * Finds the nearest ballista, clears the subject's actions and moves them
 * to the ballista. Sets the custom AI to the next succesive action, in this
 * case EngineConstants.using the ballista.
 *
 * @param oCreature - The subject to be moved.
 * @author Grant Mackay
 */
     public void Ballista_MoveToBallista(GameObject oCreature)
     {

          // Find the ballista
          GameObject oBallista = UT_GetNearestObjectByTag(oCreature, EngineConstants.BALLISTA_TAG);

          // Move to the ballista
          WR_ClearAllCommands(oCreature);
          WR_AddCommand(oCreature, CommandMoveToObject(oBallista, EngineConstants.TRUE), EngineConstants.TRUE);

          // Store the ballista     ?

          // Set the next custom action to be completed on arrival.
          SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CUSTOM_AI_FIRE_BALLISTA);

          // Clear the creature's primary counter for the wait state.
          SetLocalInt(oCreature, EngineConstants.CREATURE_COUNTER_1, 0);

     }

     /*
 * @brief Has the subject fire the nearest ballista.
 *
 * Has the subject creature play a 'fire' animation. Sets the custom AI to the
 * next succesive action, in this case EngineConstants.using the ballista.
 *
 * @param oCreature - The creature that will be using the ballista.
 * @author Grant Mackay
 */
     public void Ballista_FireBallista(GameObject oCreature)
     {

          // First verify the ballista the creature is supposed to be using.
          if (Ballista_VerifyBallista(oCreature) == EngineConstants.FALSE) return;

          // Play an animation to 'fire' the ballista.
          WR_AddCommand(oCreature, CommandPlayAnimation(107), EngineConstants.TRUE);

          SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CUSTOM_AI_USE_BALLISTA);

     }

     /*
 * @brief Has the subject use the nearest ballista.
 *
 * Finds the nearest ballista, clears the subject's action queue and has
 * them use the ballista. Sets the custom AI to the next succesive action, in
 * this case EngineConstants.waiting for the ballista to be available for usage again or
 * alternately deactivating custom AI if enemies are drawing near.
 *
 * @param oCreature - The creature that will be using the ballista.
 * @author Grant Mackay
 */
     public void Ballista_UseBallista(GameObject oCreature)
     {

          // First verify the ballista the creature is supposed to be using.
          if (Ballista_VerifyBallista(oCreature) == EngineConstants.FALSE) return;

          // Find the ballista
          GameObject oBallista = UT_GetNearestObjectByTag(oCreature, EngineConstants.BALLISTA_TAG);

          // Set the ballista to be interactive                                                   <-------------------
          SetObjectInteractive(oBallista, EngineConstants.TRUE);

          // Use the ballista
          WR_AddCommand(oCreature, CommandUseObject(oBallista, EngineConstants.PLACEABLE_ACTION_USE));

          // Set up the next state.
          SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CUSTOM_AI_WAIT_FOR_BALLISTA);

     }

     /*
 * @brief Has the subject wait until the ballista has fired before using it.
 *
 * Queues a wait xCommand on the creature and sets the Custom AI to use the
 * ballista on the next command.
 *
 * @param oCreature - The creature waiting for the ballista.
 * @author Grant Mackay
 */
     public void Ballista_WaitForBallista(GameObject oCreature)
     {

          // First verify the ballista the creature is supposed to be using.
          if (Ballista_VerifyBallista(oCreature) == EngineConstants.FALSE) return;

          // Count three seperate wait states.
          int nCount = GetLocalInt(oCreature, EngineConstants.CREATURE_COUNTER_1);

          nCount++;

          switch (nCount)
          {
               case 1:
                    {

                         // Find the ballista
                         GameObject oBallista = UT_GetNearestObjectByTag(oCreature, EngineConstants.BALLISTA_TAG);

                         // Set the ballista to be non-interactive                                       <-------------------
                         SetObjectInteractive(oBallista, EngineConstants.FALSE);

                         // play a busy animation.
                         WR_AddCommand(oCreature, CommandPlayAnimation(904), EngineConstants.TRUE);

                         break;

                    }

               case 2:
                    {
                         // nothing right now.
                         break;
                    }

               case 3:
                    {

                         // Move to the next AI state.
                         SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CUSTOM_AI_FIRE_BALLISTA);
                         nCount = 0;

                         break;

                    }

          }

          SetLocalInt(oCreature, EngineConstants.CREATURE_COUNTER_1, nCount);

     }

     /*
 * @brief Verifies the nearest ballista isn't destroyed.
 *
 * Verifies that the nearest ballista hasn't been destroyed. If it has stops the
 * ballista firing and reloading animations and sets up ambient AI to start
 * firing a bow instead.
 *
 * @param oCreature - The creature using the ballista.
 * @returns EngineConstants.TRUE if the ballista is still action-worthy, EngineConstants.FALSE otherwise.
 * @author Grant Mackay
 */
     public int Ballista_VerifyBallista(GameObject oCreature)
     {

          GameObject oBallista;
          float fDistance;
          int nState;

          // Find the Ballista
          oBallista = UT_GetNearestObjectByTag(oCreature, EngineConstants.BALLISTA_TAG);

          // Determine the ballista's state and distance
          fDistance = GetDistanceBetween(oCreature, oBallista);
          nState = GetPlaceableState(oBallista);

          // Should the nearest ballista be too far or broken.
          if ((fDistance > 4.0f) || (nState == 2))
          {

               // Clear the creatures actions.
               WR_ClearAllCommands(oCreature);

               // Shut off custom AI
               SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.CUSTOM_AI_INACTIVE);

               // Set up ambient variables
               SetLocalInt(oCreature, "AMBIENT_SYSTEM_ENABLED", EngineConstants.TRUE);
               SetLocalInt(oCreature, EngineConstants.AMBIENT_COMMAND, 2);

               // Clear the creature counter that was being used.
               SetLocalInt(oCreature, EngineConstants.CREATURE_COUNTER_1, 0);

               return EngineConstants.FALSE;
          }

          return EngineConstants.TRUE;

     }
}