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
     //////////////////////////////////////////////
     // ai_main_h
     //
     // This script includes all general AI functions
     //
     // Owner: Yaron Jakobs
     //
     /////////////////////////////////////////////

     /* @addtogroup scripting_ai2 Scripting AI handling
     *
     * Main AI interface functions
     */
     /* @{*/

     //#include "log_h"
     //#include "wrappers_h"
     //#include "ai_threat_h"
     //#include "xEvents_h"
     //#include "effects_h"
     //#include "items_h"
     //#include "ability_h"
     //#include "ai_conditions_h"
     //#include "ai_constants_h"
     //#include "ai_ballista_h"
     //#include "ai_behaviors_h"


     ////////////////////////////////////////////////////////////////////////////////
     //
     //                            FUNCTIONS DEFINITIONS
     //
     ////////////////////////////////////////////////////////////////////////////////

     /* @brief Determines the exact action to take when an GameObject is blocking the way
*
* A few examples are bashing a container or door, lockpicking and then opening a gate, etc.
*
* @param oBlockingObject the GameObject blocking the path
* @returns EngineConstants.TRUE if the AI found a solution to deal with the blocking GameObject
* @author Jose
*/
     public int AI_DeterminePathBlockedAction(GameObject oBlockingObject)
     {
          // Check that we have a valid GameObject
          if (IsObjectValid(oBlockingObject) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("AI_DeterminePathBlockedAction", "INVALID BLOCKING OBJECT");
#endif
               return EngineConstants.FALSE;
          }

          // Attempt to find an action to unblock the path
#if DEBUG
          Log_Trace_AI("AI_DeterminePathBlockedAction", "***** START ***** , blocking GameObject: " + ObjectToString(oBlockingObject));
#endif
          xCommand cPathAction = new xCommand(EngineConstants.COMMAND_TYPE_INVALID);
          xCommand cPathActionContinue;

          // DOOR - Locked
          if (GetPlaceableState(oBlockingObject) == EngineConstants.PLC_STATE_DOOR_LOCKED)
          {
               if (IsControlled(gameObject) == EngineConstants.FALSE)
               {
                    cPathAction = _AI_DoNothing(-1, -1, EngineConstants.FALSE, EngineConstants.TRUE);
               }
               // Other interesting cases to consider are:
               //   if rogue has lockpicking
               //   if ranged combatant has shatteting shot
               //   if mage has spell for breaking a placeable
               //   ... etc
          }
          // DOOR - Unlocked
          else if (GetPlaceableState(oBlockingObject) == EngineConstants.PLC_STATE_DOOR_UNLOCKED)
          {
               cPathAction = CommandUseObject(oBlockingObject, EngineConstants.PLACEABLE_ACTION_OPEN);

               // Other interesting cases to consider are:
               //   hostile creatures might still decide to break the door to make this more aggresive?
               //   I don't see hurlocks or ogres gently pushing the door...
          }
          // Add more cases here as necessary
          // ...

          // If we found a valid way to unblock the path, add it to the AI queue
          if (GetCommandType(cPathAction) != EngineConstants.COMMAND_TYPE_INVALID)
          {
               // This action should unblock the path
               WR_AddCommand(gameObject, cPathAction, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);

               // After that, the character can resume the previous action
               xCommand cPreviousCommand = GetPreviousCommand(gameObject);
               WR_AddCommand(gameObject, cPreviousCommand, EngineConstants.FALSE, EngineConstants.FALSE, EngineConstants.COMMAND_ADDBEHAVIOR_DONTCLEAR, EngineConstants.AI_COMMAND_TIMER);

               return EngineConstants.TRUE;
          }
          // If not, just report a warning and return failure
          else
          {
#if DEBUG
               Log_Trace_AI("AI_DeterminePathBlockedAction", "Couldn't find a solution for dealing with the blocking GameObject", EngineConstants.LOG_SEVERITY_WARNING);
#endif
               return EngineConstants.FALSE;
          }
     }

     /* @brief Determines the exact action to take for the next combat round for the current creature
*
* This also includes acquiring a hostile target for offensive attacks and acquiring a friendly target
* for buffs and heals. The function assumes we handle gameObject as the attacking GameObject
* This function will run for all creature types: enemy, controlled follower and non-controlled follower
* This function does NOT care about combat mode. It can run with or without it.
* Some cases might check the combat mode, but no code should try to enable to disable combat mode.
* It is assumed that some other external check were made to make sure that gameObject can run this function.
*
* @param oLastTarget the target the attacker attacked last round (if valid)
* @param nLastCommand the last xCommand executed
* @param nLastCommandStatus EngineConstants.COMMAND_SUCCESSFUL or EngineConstants.COMMAND_FAILURE - used mostly to detect movement failures
* @param nLastSubCommand last sub xCommand (ability ID for use ability xCommands)
* @author Yaron
*/
     public void AI_DetermineCombatRound(GameObject oLastTarget = null, int nLastCommand = 0, int nLastCommandStatus = EngineConstants.COMMAND_SUCCESSFUL, int nLastSubCommand = -1)
     {
#if DEBUG
          Log_Trace_AI("AI_DetermineCombatRound", "***** START ***** , last xCommand status: " + IntToString(nLastCommandStatus));
#endif

          // -------------------------------------------------------------------------
          // Non combatant
          // -------------------------------------------------------------------------
          if (GetCombatantType(gameObject) == EngineConstants.CREATURE_TYPE_NON_COMBATANT)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "NON COMBATANT EngineConstants.CREATURE - running away");
#endif
               return;
          }

          // make stealthed creature get out of stealth if alone and not at the start of combat
          if (IsFollower(gameObject) == EngineConstants.FALSE && IsStealthy(gameObject) != EngineConstants.FALSE && GetGameMode() == EngineConstants.GM_COMBAT)
          {
               List<GameObject> arAllies = _AI_GetAllies(-1, -1);
               int nSize = GetArraySize(arAllies);
               if (nSize == 0)
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound", "No allies left dropping out of stealth and continueing with AI");
#endif
                    DropStealth(gameObject);
               }
               else // some allies alive -> if one of them is not in stealth then we're good
               {
                    int i;
                    GameObject oCurrent;
                    int bRemoveStealth = EngineConstants.TRUE;
                    for (i = 0; i < nSize; i++)
                    {
                         oCurrent = arAllies[i];
                         if (IsStealthy(oCurrent) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("AI_DetermineCombatRound", "Found at least one non-stealth ally - clear to keep stealth");
#endif
                              bRemoveStealth = EngineConstants.FALSE;
                              break;
                         }

                    }
                    if (bRemoveStealth != EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace_AI("AI_DetermineCombatRound", "All allies are stealthy - dropping stealth");
#endif
                         DropStealth(gameObject);
                    }
               }
          }

          if (GetGameMode() != EngineConstants.GM_COMBAT && GetGameMode() != EngineConstants.GM_EXPLORE)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "Not combat or explore game mode - WAITING");
#endif
               if (IsControlled(gameObject) == EngineConstants.FALSE)
               {
                    xCommand cWait = _AI_DoNothing(-1, nLastCommandStatus, EngineConstants.FALSE, EngineConstants.FALSE);
                    WR_AddCommand(gameObject, cWait);
               }
               return;
          }

          SetObjectInteractive(gameObject, EngineConstants.TRUE);

          // Do not run any AI if the creature is doing something right now
          xCommand cCurrent = GetCurrentCommand(gameObject);
          int nCurrentType = GetCommandType(cCurrent);
          int nQueueSize = GetCommandQueueSize(gameObject);
          if (nCurrentType != EngineConstants.COMMAND_TYPE_INVALID || nQueueSize > 0)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "Do nothing - creature running xCommand already: " + Log_GetCommandNameById(nCurrentType), EngineConstants.LOG_SEVERITY_WARNING);
#endif
               return;
          }

          // -------------------------------------------------------------------------
          // Ability usage disabled - Master off switch per creature
          // -------------------------------------------------------------------------
          else if (GetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_AI_OFF) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "ABORT: EngineConstants.CREATURE_RULES_FLAG_AI_OFF was set");
#endif
               if (IsFollower(gameObject) != EngineConstants.FALSE)
               {
                    Warning("IMPORTANT! follower has ability use by AI disabled! This should never happen without debug scripts! - please contact YARON");
               }
               return;
          }
          else if (IsFollower(gameObject) == EngineConstants.FALSE && GetLocalInt(gameObject, EngineConstants.AI_LIGHT_ACTIVE) != EngineConstants.FALSE)
          {
               //List<GameObject> oNearestFollowers = GetNearestObjectByGroup(gameObject, GROUP_PC, EngineConstants.OBJECT_TYPE_CREATURE, 1, EngineConstants.TRUE, EngineConstants.TRUE);
               //GameObject oNearestFollower = oNearestFollowers[0];
               //float fDistance = GetDistanceBetween(gameObject, oNearestFollower);

               //if(!IsObjectValid(oNearestFollower) || fDistance > LIGHT_AI_MIN_DISTANCE)
               AI_DetermineCombatRound_Light(oLastTarget, nLastCommand, nLastCommandStatus, nLastSubCommand);
               return;

          }

          // AOE check
          int nCheckChance = 0; // chance to try and avoid AOE if actually inside
          if (IsFollower(gameObject) == EngineConstants.FALSE)
          {
               int nAppearance = GetAppearanceType(gameObject);
               nCheckChance = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "AvoidAOEChance", nAppearance);
          }
          else if (IsControlled(gameObject) == EngineConstants.FALSE)// follower - chance based on behavior
          {
               if (AI_BehaviorCheck_AvoidAOE() != EngineConstants.FALSE)
                    nCheckChance = EngineConstants.AI_FOLLOWER_AVOID_AOE_CHANCE;
          }

          if (nCheckChance > 0 && GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY) != EngineConstants.AI_STATIONARY_STATE_HARD)
          {
               List<int> AbilityAOEs = GetAbilitiesDueToAOEs(gameObject);
               int nArraySize = GetArraySize(AbilityAOEs);
               if (nArraySize > 0) // in ability AOEs
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound", "Creature is in an ability AOE");
#endif
                    int i;
                    int nAOE;
                    int nRand;
                    for (i = 0; i < nArraySize; i++)
                    {
                         if (GetM2DAInt(EngineConstants.TABLE_AI_ABILITY_COND, "HostileAOE", AbilityAOEs[i]) == 1)
                         {
                              // in hostile AOE

                              // random chance to try and exit
                              nRand = Engine_Random(100);
#if DEBUG
                              Log_Trace_AI("AI_DetermineCombatRound", "Hostile AOE ability: " + IntToString(AbilityAOEs[i]) + ", escape chance: " + IntToString(nCheckChance) +
                                  ", roll: " + IntToString(nRand));
#endif
                              if (nRand <= nCheckChance)
                              {
                                   // use the regular movement cooldown
                                   if (_AI_CheckMoveTimer() != EngineConstants.FALSE)
                                   {
#if DEBUG
                                        Log_Trace_AI("AI_DetermineCombatRound", "Escaping AOE!");
#endif
                                        Vector3 lLoc = GetLocation(gameObject);
                                        Vector3 vPos = GetPositionFromLocation(lLoc);
                                        int nRandX = Engine_Random(16) - 8;
                                        int nRandY = Engine_Random(16) - 8;
                                        vPos.x += nRandX;
                                        vPos.y += nRandY;
                                        Vector3 lNewLoc = Location(GetArea(gameObject), vPos, GetFacing(gameObject));
                                        //xCommand cMove = CommandMoveToLocation(lNewLoc, EngineConstants.TRUE);
                                        xCommand cMove = CommandMoveAwayFromObject(gameObject, EngineConstants.AI_AOE_FLEE_DISTANCE, EngineConstants.TRUE);
                                        WR_AddCommand(gameObject, cMove, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);
                                        _AI_SetMoveTimer();
                                        return;
                                   }
                                   else
                                        break; // exit loop

                              }
                         }
                    }
               }
          }



          // -------------------------------------------------------------------------
          // AI disabled (followers only)
          // -------------------------------------------------------------------------
          if (IsFollower(gameObject) != EngineConstants.FALSE && IsPartyAIEnabled(gameObject) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "FOLLOWER AI DISABLED - will run partial AI instead");
#endif
               AI_DetermineCombatRound_Partial(oLastTarget, nLastCommand, nLastCommandStatus, nLastSubCommand);
               return;
          }



          // We assume that whoever called AI_DetermineCombatRound has validated that the creature is valid for combat
          if (Effects_HasAIModifier(gameObject, EngineConstants.AI_MODIFIER_IGNORE) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "I have the IGNORE flag set - doing nothing", EngineConstants.LOG_SEVERITY_WARNING);
#endif
               return;
          }

          if (IsFollower(gameObject) != EngineConstants.FALSE && IsControlled(gameObject) == EngineConstants.FALSE) // check hidden tactics based on behavior
          {
               if (AI_BehaviorCheck_PreferRange() != EngineConstants.FALSE && _AI_GetWeaponSetEquipped() != EngineConstants.AI_WEAPON_SET_RANGED
                   && _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED) != EngineConstants.FALSE &&
                   _AI_Condition_BeingAttackedByAttackType(EngineConstants.AI_TARGET_TYPE_SELF, EngineConstants.AI_ATTACK_TYPE_MELEE, -1, -1, -1) == null)
               {
                    List<GameObject> arEnemies = GetCreaturesInMeleeRing(gameObject, 0.0f, 360.0f, EngineConstants.TRUE);
                    if (GetArraySize(arEnemies) == 0)
                    {
#if DEBUG
                         Log_Trace_AI("AI_DetermineCombatRound", "Follower prefering RANGED weapon - switching");
#endif
                         xCommand cSwitch = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED);
                         WR_AddCommand(gameObject, cSwitch);
                         return;
                    }
               }
               else if (AI_BehaviorCheck_PreferMelee() != EngineConstants.FALSE && _AI_GetWeaponSetEquipped() != EngineConstants.AI_WEAPON_SET_MELEE
                   && _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_MELEE) != EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound", "Follower prefering MELEE weapon - switching");
#endif
                    xCommand cSwitch = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_MELEE);
                    WR_AddCommand(gameObject, cSwitch);
                    return;
               }
               else if (AI_BehaviorCheck_AvoidNearbyEnemies() != EngineConstants.FALSE)
               {
                    if ((nLastCommand == EngineConstants.COMMAND_TYPE_MOVE_TO_OBJECT || nLastCommand == EngineConstants.COMMAND_TYPE_MOVE_TO_LOCATION) &&
                        nLastCommandStatus < 0)
                    {
#if DEBUG
                         Log_Trace_AI("AI_DetermineCombatRound", "Last xCommand was movement and failed - not evaluating Avoid Enemies behavior");
#endif
                    }
                    else
                    {
                         List<GameObject> arEnemies = _AI_GetEnemies(-1, -1);
                         int nSize = GetArraySize(arEnemies);
                         if (nSize > 0)
                         {
                              GameObject oEnemy = arEnemies[0];
                              float fDistance = GetDistanceBetween(gameObject, oEnemy);
                              if (fDistance < EngineConstants.AI_RANGE_SHORT)
                              {
#if DEBUG
                                   Log_Trace_AI("AI_DetermineCombatRound", "Follower avoiding nearby enemies - moving away");
#endif
                                   xCommand cMove = CommandMoveAwayFromObject(oEnemy, EngineConstants.AI_MOVE_AWAY_DISTANCE_SHORT, EngineConstants.TRUE);
                                   WR_AddCommand(gameObject, cMove, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);
                              }
                         }
                    }
               }
               else if (AI_BehaviorCheck_AvoidMelee() != EngineConstants.FALSE)
               {
                    if ((nLastCommand == EngineConstants.COMMAND_TYPE_MOVE_TO_OBJECT || nLastCommand == EngineConstants.COMMAND_TYPE_MOVE_TO_LOCATION) &&
                        nLastCommandStatus < 0)
                    {
#if DEBUG
                         Log_Trace_AI("AI_DetermineCombatRound", "Last xCommand was movement and failed - not evaluating Avoid Melee Enemies behavior");
#endif
                    }
                    else
                    {
                         List<GameObject> arEnemies = GetCreaturesInMeleeRing(gameObject, 0.0f, 360.0f, EngineConstants.TRUE);
                         int nSize = GetArraySize(arEnemies);
                         int i;
                         GameObject oCurrent;
                         for (i = 0; i < nSize; i++)
                         {
                              oCurrent = arEnemies[i];
                              if (IsUsingMeleeWeapon(oCurrent) != EngineConstants.FALSE && GetAttackTarget(oCurrent) == gameObject)
                              {
#if DEBUG
                                   Log_Trace_AI("AI_DetermineCombatRound", "Follower avoiding melee enemies - moving away");
#endif
                                   xCommand cMove = CommandMoveToObject(oCurrent, EngineConstants.TRUE, EngineConstants.AI_MOVE_AWAY_DISTANCE_SHORT, EngineConstants.TRUE);
                                   WR_AddCommand(gameObject, cMove, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);
                                   return;
                              }
                         }
                    }
               }
          }

          // Check if the creature should bring his team to help
          int nTeamHelpStatus = GetLocalInt(gameObject, EngineConstants.AI_HELP_TEAM_STATUS);
#if DEBUG
          Log_Trace_AI("AI_DetermineCombatRound", "Team Help Status: " + IntToString(nTeamHelpStatus), EngineConstants.LOG_SEVERITY_WARNING);
#endif
          if (nTeamHelpStatus == EngineConstants.AI_HELP_TEAM_STATUS_ACTIVE)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "Calling team for help", EngineConstants.LOG_SEVERITY_WARNING);
#endif
               SetLocalInt(gameObject, EngineConstants.AI_HELP_TEAM_STATUS, EngineConstants.AI_HELP_TEAM_STATUS_CALLED_FOR_HELP);
               xCommand cMove = CommandMoveToLocation(GetLocation(gameObject));
               int nTeamID = GetTeamId(gameObject);
               if (nTeamID > 0)
               {
                    List<GameObject> arTeam = GetTeam(nTeamID);
                    int nSize = GetArraySize(arTeam);
                    int i;
                    GameObject oCurrent;
                    float fHelpDistance;
                    for (i = 0; i < nSize; i++)
                    {
                         oCurrent = arTeam[i];
                         // Sending only if the creature is not in combat yet and not helping yet
                         if (GetCombatState(oCurrent) == EngineConstants.FALSE && GetLocalInt(oCurrent, EngineConstants.AI_HELP_TEAM_STATUS) == EngineConstants.AI_HELP_TEAM_STATUS_ACTIVE)
                         {
#if DEBUG
                              Log_Trace_AI("AI_DetermineCombatRound", "Bring creature to help: " + GetTag(oCurrent));
#endif
                              SetLocalInt(oCurrent, EngineConstants.AI_HELP_TEAM_STATUS, EngineConstants.AI_HELP_TEAM_STATUS_HELPING);
                              WR_ClearAllCommands(oCurrent);
                              WR_AddCommand(oCurrent, cMove, EngineConstants.FALSE, EngineConstants.FALSE, -1, 0.0f); // No timeout so they won't stop too soon
                         }
                    }
               }
          }

          // controlled party member -> use partial AI
          if (IsControlled(gameObject) != EngineConstants.FALSE)
          {
               if (GetCombatState(gameObject) == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound", "Follower not in combat state - aborting AI", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                    return;
               }
               AI_DetermineCombatRound_Partial(oLastTarget, nLastCommand, nLastCommandStatus, nLastSubCommand);
          }
          // enemy OR non-controlled party member -> let the AI rules table determine the round
          else
          {
               if (GetLocalInt(gameObject, EngineConstants.AI_BALLISTA_SHOOTER_STATUS) > 0)
               {
                    if (AI_Ballista_HandleAI() != EngineConstants.FALSE)
                         return;
               }

               if (IsFollower(gameObject) == EngineConstants.FALSE && GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY) == EngineConstants.AI_STATIONARY_STATE_VERY_SOFT)
               {
                    // clear state if there is an enemy nearby
                    List<GameObject> arEnemies = _AI_GetEnemies(-1, -1);
                    if (GetArraySize(arEnemies) > 0)
                    {
                         float fDistance = GetDistanceBetween(gameObject, arEnemies[0]);
                         if (fDistance > EngineConstants.AI_MELEE_RANGE)
                         {
#if DEBUG
                              Log_Trace_AI("AI_DetermineCombatRound", "Found nearby enemy - clearing stationary flag");
#endif
                              SetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY, EngineConstants.AI_STATIONARY_STATE_DISABLED);
                         }
                    }
               }
               // Iterate the AI rules table, until executing a valid rule
               // First we get the first tactic ID/Priority. This can be any positive number since
               // the package 2da includes tactics which may not apply to the current level of the creature

               int nPackageTable = _AI_GetPackageTable();
               int nTacticsNum;
               if (_AI_UseGUITables() != EngineConstants.FALSE)
                    nTacticsNum = GetNumTactics(gameObject);
               else
                    nTacticsNum = _AI_GetTacticsNum(nPackageTable);
               if (nTacticsNum > EngineConstants.AI_MAX_TACTICS)
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound", "Too many tactics: " + IntToString(nTacticsNum));
#endif
                    return;
               }
               int i = 1;
               int nLastTactic = GetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC);
               int nTablesDisabled = GetLocalInt(GetModule(), EngineConstants.AI_DISABLE_TABLES);
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "tactics num: " + IntToString(nTacticsNum));
               Log_Trace_AI("AI_DetermineCombatRound", "Last Tactic ID: " + IntToString(nLastTactic));
#endif
               // If the last tactic failed then resume from the tactic after it
               if (nLastCommandStatus < 0 && nLastTactic >= 0)
               {
                    i = nLastTactic;
                    i++;
                    if (IsFollower(gameObject) != EngineConstants.FALSE && i == nTacticsNum)
                    {
#if DEBUG
                         Log_Trace_AI("AI_DetermineCombatRound", "Last tactic failed for follower and failed tactic was last at table: WAITING");
#endif
                         xCommand cWait = _AI_DoNothing(nLastTactic, nLastCommandStatus, EngineConstants.TRUE);
                         WR_AddCommand(gameObject, cWait);
                         return;
                    }
                    else
                    {
#if DEBUG
                         Log_Trace_AI("AI_DetermineCombatRound", "Last tactic failed, resuming from the next tactic: " + IntToString(i));
#endif
                    }
               }
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound", "nTablesDisabled= " + IntToString(nTablesDisabled));
               Log_Trace_AI("AI_DetermineCombatRound", "i= " + IntToString(i));
#endif

               // If a default action failed - we would try the entire table again
               int nExecuteRet;
               int nUseGUITables = _AI_UseGUITables();

               if (nTablesDisabled == EngineConstants.FALSE)
               {
                    //for (i; i <= nTacticsNum; i++)
                    while (i <= nTacticsNum)
                    {
                         // For every rule: evaluate condition - if valid -> execute
                         nExecuteRet = _AI_ExecuteTactic(nPackageTable, i, nLastCommandStatus, nUseGUITables);
                         // if EngineConstants.TRUE/1 => return (tactic executed)
                         // if EngineConstants.FALSE/0 => continue normally (tactic not executed)
                         // if -1 => jump to default action
                         // if greater then 1 => jump to specific tactic
                         if (nExecuteRet != EngineConstants.FALSE)
                              return;
                         else if (nExecuteRet == -1)
                              break;
                         else if (nExecuteRet > 1 && nExecuteRet > i)
                              i = nExecuteRet - 1; // it will be increased by 1 by the for loop
                         i++; //DHK
                    }
               }

               if (GetObjectActive(gameObject) == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound", "Object inactive - exiting");
#endif
                    return;
               }

               // Continue from this point only if in combat state
               if (GetCombatState(gameObject) == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound", "Not in combat. Not evaluating default action (attack)");
#endif
                    // IMPORTANT: can't put here any wait or move xCommands as they will conflict with the engine follow
                    // xCommands, especially if the user selected the GUI option to disable party following.
                    return;
               }

               AI_ExecuteDefaultAction(oLastTarget, nLastCommand, nLastCommandStatus, nLastSubCommand);


          }  // END else (non-controlled follower OR enemy)

     }

     // Light AI for creatures fighting other creatures away from the player
     public void AI_DetermineCombatRound_Light(GameObject oLastTarget = null, int nLastCommand = -1, int nLastCommandStatus = EngineConstants.COMMAND_SUCCESSFUL, int nLastSubCommand = -1)
     {
#if DEBUG
          Log_Trace_AI("AI_DetermineCombatRound_Light", "Last target: " + GetTag(oLastTarget));
#endif
          GameObject oTarget = oLastTarget;
          if (_AI_IsHostileTargetValid(oTarget) == EngineConstants.FALSE)
          {
               //List<GameObject> arTargets = GetNearestObjectByHostility(gameObject, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_CREATURE, 1, EngineConstants.TRUE, EngineConstants.TRUE);
               //oTarget = arTargets[0];
               //oTarget = AI_Threat_GetThreatTarget(gameObject, EngineConstants.TRUE);

               // Yaron, Jan 2 2009: removed the above threat call as it grabbed sometimes targets that were not percevied
               // Instead, we'll call the engine threat target directly
               // Yaron, Jan 5, 2009: some climax armies were getting stuck on the most hated target and trying to attack
               // it even when it was surrounded by enemies already and thus continually failing. Instead, they would now
               // pick a random target from their threat array.
               int nThreatArraySize = GetThreatTableSize(gameObject);
               int nEnemy = Engine_Random(nThreatArraySize);
               oTarget = GetThreatEnemy(gameObject, nEnemy);
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound_Light", "Threat target: " + GetTag(oTarget));
#endif
          }

          if ((nLastCommandStatus != EngineConstants.COMMAND_SUCCESSFUL && nLastCommandStatus != EngineConstants.COMMAND_FAILED_TARGET_DESTROYED &&
              nLastCommandStatus != EngineConstants.COMMAND_FAILED_TIMEOUT) || _AI_IsHostileTargetValid(oTarget) == EngineConstants.FALSE)
          {
               xCommand cWait = CommandWait(EngineConstants.AI_DO_NOTHING_DELAY);
               WR_AddCommand(gameObject, cWait);
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound_Light", "No valid target - waiting");
#endif
          }
          else
          {
               //xCommand cAttack = _AI_ExecuteAttack(oTarget, nLastCommandStatus);
               xCommand cAttack = CommandAttack(oTarget);
               WR_AddCommand(gameObject, cAttack, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER_QUICK);
          }
     }

     // Executes the AI default action for DetermineCombatRound
     public void AI_ExecuteDefaultAction(GameObject oLastTarget = null, int nLastCommand = 0, int nLastCommandStatus = EngineConstants.COMMAND_SUCCESSFUL, int nLastSubCommand = -1)
     {
#if DEBUG
          Log_Trace_AI("AI_ExecuteDefaultAction", "START");
#endif
          GameObject oNewTarget = null;

          // Finished checking all rules and nothing got executed
          // -> Execute default attack
          GameObject oSelectedTarget = GetAttackTarget(gameObject);

#if DEBUG
          Log_Trace_AI("AI_ExecuteDefaultAction", "Could not assign any AI tactic - creature will try to attack normally");
#endif

          if (IsFollower(gameObject) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("AI_ExecuteDefaultAction", "Follower selected target: " + GetTag(oSelectedTarget));
#endif
               float fDistanceToPlayer = GetDistanceBetween(gameObject, GetMainControlled());

               if (nLastCommand == EngineConstants.COMMAND_TYPE_ATTACK)
               {
                    GameObject oTargetOverride = _AI_GetTargetOverride();
                    if (_AI_IsHostileTargetValid(oTargetOverride) != EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace_AI("AI_ExecuteDefaultAction", "Follower picking override target (probably a summon): " + GetTag(oTargetOverride));
#endif
                         oNewTarget = oTargetOverride;
                    }
                    // A follower can NOT switch target by himself
                    else if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE)
                         oNewTarget = oSelectedTarget;
                    else // no valid target -> try to find a new target
                    {
                         oNewTarget = _AI_Condition_GetNearestVisibleCreature(EngineConstants.AI_TARGET_TYPE_ENEMY, 1, -1, -1, -1);
                         if (_AI_IsHostileTargetValid(oNewTarget) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("AI_ExecuteDefaultAction", "Follower: could not find a new target - moving to player or waiting");
#endif
                              if (fDistanceToPlayer > EngineConstants.AI_FOLLOWER_PLAYER_DISTANCE && nLastCommandStatus == EngineConstants.COMMAND_SUCCESSFUL)
                              {
                                   Vector3 lLoc = GetFollowerWouldBeLocation(gameObject);
                                   xCommand cMove = CommandMoveToLocation(lLoc, EngineConstants.TRUE);
                                   WR_AddCommand(gameObject, cMove, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);
                                   return;
                              }
                              else
                              {
                                   xCommand cWait = _AI_DoNothing(-1, nLastCommandStatus, EngineConstants.FALSE, EngineConstants.TRUE);
                                   WR_AddCommand(gameObject, cWait);
                                   return;
                              }
                         }
#if DEBUG
                         Log_Trace_AI("AI_ExecuteDefaultAction", "Follower: acquired new target: " + GetTag(oNewTarget));
#endif
                    }
                    // Follower allowed to continue attacking
#if DEBUG
                    Log_Trace_AI("AI_ExecuteDefaultAction", "Follower: continue attacking current target");
#endif
                    xCommand cFollowerAttack = _AI_ExecuteAttack(oNewTarget, nLastCommandStatus);
                    WR_AddCommand(gameObject, cFollowerAttack);
                    return;
               }
               else if (AI_BehaviorCheck_DefaultAttack() == EngineConstants.FALSE) // last xCommand not attack
               {
                    if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE)
                    {
                         // continue attacking if already has a target
                         xCommand cFollowerAttack = _AI_ExecuteAttack(oNewTarget, nLastCommandStatus);
                         WR_AddCommand(gameObject, cFollowerAttack);
                         return;
                    }
                    else
                    {
#if DEBUG
                         Log_Trace_AI("AI_ExecuteDefaultAction", "Follower: last xCommand wasn't attack - moving or doing nothing (follower not allowed to pick a target)");
#endif
                         xCommand cMoveOrWait = _AI_MoveToControlled(nLastCommandStatus);
                         WR_AddCommand(gameObject, cMoveOrWait, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);
                         return;
                    }
               }
               else if (AI_GetPartyAllowedToAttack() == EngineConstants.FALSE && AI_BehaviorCheck_AttackOnCombatStart() == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("AI_ExecuteDefaultAction", "Follower: not allowed to attack - moving or doing nothing (follower not allowed to pick a target)");
#endif
                    xCommand cMoveOrWait = _AI_MoveToControlled(nLastCommandStatus);
                    WR_AddCommand(gameObject, cMoveOrWait, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);
                    return;
               }

          }
          else // not a follower, can switch a target
          {
               oNewTarget = _AI_Condition_GetMostHatedEnemy(1, EngineConstants.COMMAND_TYPE_ATTACK, -1, -1);
#if DEBUG
               Log_Trace_AI("AI_ExecuteDefaultAction", "Not a follower picking new target (most hated): " + GetTag(oNewTarget));
#endif
          }

          if (IsFollower(gameObject) != EngineConstants.FALSE)
          {
               // A follower that is allowed to pick a target by himself
#if DEBUG
               Log_Trace_AI("AI_ExecuteDefaultAction", "Follower: behavior allows picking new target - attacking nearest visible");
#endif
               oNewTarget = _AI_Condition_GetNearestVisibleCreature(EngineConstants.AI_TARGET_TYPE_ENEMY, 1, -1, -1, -1);
               if (IsObjectValid(oNewTarget) == EngineConstants.FALSE && GetGameMode() == EngineConstants.GM_COMBAT)
               {
#if DEBUG
                    Log_Trace_AI("AI_ExecuteDefaultAction", "Follower: could not find valid target (room connection error OR enemy too far away) - trying to move to leader");
#endif
                    xCommand cMoveOrWait = _AI_MoveToControlled(nLastCommandStatus);
                    WR_AddCommand(gameObject, cMoveOrWait, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);
                    return;
               }
          }

          xCommand cAttack;

          if (_AI_IsHostileTargetValid(oNewTarget) != EngineConstants.FALSE)
          {
               cAttack = _AI_ExecuteAttack(oNewTarget, nLastCommandStatus);
               if (GetCommandType(cAttack) == EngineConstants.COMMAND_TYPE_INVALID)
               {
#if DEBUG
                    Log_Trace_AI("AI_ExecuteDefaultAction", "INVALID TACTIC COMMAND - WAITING", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
                    cAttack = _AI_DoNothing(-1, -1, EngineConstants.TRUE);
               }
          }
          else // new target
          {
#if DEBUG
               Log_Trace_AI("AI_ExecuteDefaultAction", "COULD NOT FIND A NEW TARGET - WAITING", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
               cAttack = _AI_DoNothing(-1, -1, EngineConstants.TRUE);
          }

          WR_AddCommand(gameObject, cAttack, EngineConstants.FALSE, EngineConstants.FALSE, -1, EngineConstants.AI_COMMAND_TIMER);

     }

     /* @brief Determines the exact action to take for the next combat round for a controlled party member
*
* This includes minimal AI handling
*
* @param oLastTarget the target the attacker attacked last round (if valid)
* @param nLastCommand the xCommand the attacker used last round
* @param nLastCommandStatus EngineConstants.COMMAND_SUCCESSFUL or EngineConstants.COMMAND_FAILURE - used mostly to detect movement failures
* @param nLastSubCommand last sub xCommand (ability ID for use ability xCommands)
* @author Yaron
*/
     public void AI_DetermineCombatRound_Partial(GameObject oLastTarget = null, int nLastCommand = -1, int nLastCommandStatus = EngineConstants.COMMAND_SUCCESSFUL, int nLastSubCommand = -1)
     {
#if DEBUG
          Log_Trace_AI("AI_DetermineCombatRound_Partial", "START, last target: " + GetTag(oLastTarget));
#endif
          if (GetCombatState(gameObject) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound_Partial", "Follower not in combat - aborting partial AI");
#endif
               return;
          }

          xCommand cCommand = new xCommand(EngineConstants.COMMAND_TYPE_INVALID);

          GameObject oSelectedTarget = GetAttackTarget(gameObject);
#if DEBUG
          Log_Trace_AI("AI_DetermineCombatRound_Partial", "Selected target: " + GetTag(oSelectedTarget));
#endif

          GameObject oTarget = oSelectedTarget;
          if (_AI_IsHostileTargetValid(oTarget) == EngineConstants.FALSE)
               oTarget = oLastTarget;

#if DEBUG
          if (_AI_IsHostileTargetValid(oTarget) == EngineConstants.FALSE)
               Log_Trace_AI("AI_DetermineCombatRound_Partial", "COULD NOT FIND VALID TARGET");
          else
               Log_Trace_AI("AI_DetermineCombatRound_Partial", "FINAL target: " + GetTag(oTarget));
#endif

          GameObject oCurrent;
          int i;
          float fRangeToSelected = GetDistanceBetween(gameObject, oTarget);

          // If enemy is in melee range and has melee weapon -> attack
          if (_AI_IsHostileTargetValid(oTarget) != EngineConstants.FALSE && _AI_IsTargetInMeleeRange(oTarget) != EngineConstants.FALSE && IsUsingMeleeWeapon(gameObject) != EngineConstants.FALSE)
          {
               cCommand = CommandAttack(oTarget);
          }
          // If has ranged weapon and has ammo and is within the range of my ranged weapon -> attack
          else if (_AI_IsHostileTargetValid(oTarget) != EngineConstants.FALSE && IsUsingRangedWeapon(gameObject) != EngineConstants.FALSE
                  && fRangeToSelected <= _AI_GetEquippedWeaponRange() &&
                  (nLastCommand == EngineConstants.COMMAND_TYPE_ATTACK || nLastCommand == EngineConstants.COMMAND_TYPE_USE_ABILITY))
          {
               if (AI_BehaviorCheck_AttackBack() == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound_Partial", "Creature behavior set to NOT attack back - aborting");
#endif
               }
               else
                    cCommand = CommandAttack(oTarget);
          }

          // NOTE: there is another part of the player attack logic that does not go here, but to the attack xEvent in rules_core
          // This deals with any creatures that attack me while I don't have any target

          if (GetCommandType(cCommand) == EngineConstants.COMMAND_TYPE_INVALID)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound_Partial", "NO VALID ACTION - DOING NOTHING (waiting)", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
               if (IsControlled(gameObject) != EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("AI_DetermineCombatRound_Partial", "Controlled follower - aborting wait - keeping queue empty", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
                    return;
               }
               cCommand = _AI_DoNothing(-1, nLastCommandStatus, EngineConstants.FALSE, EngineConstants.TRUE);
          }

          if (IsObjectValid(oTarget) != EngineConstants.FALSE && IsObjectHostile(gameObject, oTarget) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("AI_DetermineCombatRound_Partial", "Attacking hostile target: allowing rest of party to attack");
#endif
               AI_SetPartyAllowedToAttack(EngineConstants.TRUE);
          }

          WR_AddCommand(gameObject, cCommand);
     }

     // MGB - March 9, 2009 - Exposed hash values to speed up evaluation of AI Tactics.
     //moved const int EngineConstants.HASH_TYPE                = 0x12A02374; // "Type"
     //moved const int EngineConstants.HASH_USECHANCE           = 0x36032F31; // "UseChance"
     //moved const int EngineConstants.HASH_SUBCOMMAND          = 0x20804179; // "SubCommand"
     //moved const int EngineConstants.HASH_COMMAND             = 0x0DF6E88A; // "Command"
     //moved const int EngineConstants.HASH_CONDITION           = 0x03C7F222; // "Condition"
     //moved const int EngineConstants.HASH_TARGETTYPE          = 0x0F642429; // "TargetType"
     //moved const int EngineConstants.HASH_CONDITIONBASE       = 0x56BB2EC7; // "ConditionBase"
     //moved const int EngineConstants.HASH_VALIDFORTARGET      = 0x77485DB5; // "ValidForTarget"
     //moved const int EngineConstants.HASH_CONDITIONPARAMETER  = 0x706DADFC; // "ConditionParameter"
     //moved const int EngineConstants.HASH_CONDITIONPARAMETER2 = 0xBCC74707; // "ConditionParameter2"

     /* @brief Checks if a tactic is valid and executes it if valid
*
* @param nPackageTable the package table for the specified tactic
* @param nTacticID the tactic ID that we are trying to execute
* @param nLastCommandStatus used in AI_ExecuteAttack
* @returns EngineConstants.TRUE if the tactic was executed, EngineConstants.FALSE otherwise
* @author Yaron
*/
     public int _AI_ExecuteTactic(int nPackageTable, int nTacticID, int nLastCommandStatus, int nUseGUITables)
     {
          Log_Trace_AI("_AI_ExecuteTactic", "START [Package Table: " + IntToString(nPackageTable) + "], TacticID: [" + IntToString(nTacticID) + "]");
          // read the package and retrieve the condition, target type and action

          // First checking if the tactic is enabled (followers only)
          if (nUseGUITables != 0 && IsTacticEnabled(gameObject, nTacticID) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "Tactic not enabled - moving to next tactic");
#endif
               return EngineConstants.FALSE;
          }

          // MGB - March 9, 2009
          // Only evaluate the random trigger chance if we are using an AI Package table.
          // This should be done before retrieving the other numbers to trivially reject
          // rules that have extremely low chances of being fired.
          if (nUseGUITables == 0)
          {
               int nTacticTriggerChance = GetHashedM2DAInt(nPackageTable, EngineConstants.HASH_USECHANCE, nTacticID);

               int nDifficulty = GetGameDifficulty();
               if (nDifficulty == EngineConstants.GAME_DIFFICULTY_CASUAL)
               {
                    // should not affect 100% tactics
                    if (nTacticTriggerChance < 100 && nTacticTriggerChance >= 80)
                         nTacticTriggerChance = 50;
                    else if (nTacticTriggerChance >= 50 && nTacticTriggerChance < 80)
                         nTacticTriggerChance = 25;
                    else if (nTacticTriggerChance >= 20 && nTacticTriggerChance < 50)
                         nTacticTriggerChance = 10;
                    else if (nTacticTriggerChance >= 10 && nTacticTriggerChance < 20)
                         nTacticTriggerChance = 5;
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteTactic", "Adjusted trigger chance (casual difficulty): " + IntToString(nTacticTriggerChance));
#endif
               }

               // Verifying random chance
               int nRandom = Engine_Random(100) + 1;
               Log_Trace_AI("_AI_ExecuteTactic", "RANDOM: " + IntToString(nRandom) + ", Tactic Trigger Chance:" + IntToString(nTacticTriggerChance));
               if (nRandom > nTacticTriggerChance)
               {
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteTactic", "Tactic did not pass random check");
#endif
                    return EngineConstants.FALSE;
               }
          }

          if (GetObjectActive(gameObject) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "Object Inactive - exiting");
#endif
               return EngineConstants.FALSE;
          }

          int nRet;
          int nTacticTargetType;
          int nTacticTargetBitField;
          int nTacticCondition;
          int nTacticCommand;
          int nTacticSubCommand;
          int nLastTacticID = GetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC);
          string sTacticItemTag = GetTacticCommandItemTag(gameObject, nTacticID);

          if (nUseGUITables != 0)
          {
               nTacticTargetType = GetTacticTargetType(gameObject, nTacticID);
               nTacticCondition = GetTacticCondition(gameObject, nTacticID);
               nTacticCommand = GetTacticCommand(gameObject, nTacticID);
               nTacticSubCommand = GetTacticCommandParam(gameObject, nTacticID);
          }
          else
          {
               nTacticTargetType = GetHashedM2DAInt(nPackageTable, EngineConstants.HASH_TARGETTYPE, nTacticID);
               nTacticCondition = GetHashedM2DAInt(nPackageTable, EngineConstants.HASH_CONDITION, nTacticID);
               nTacticCommand = GetHashedM2DAInt(nPackageTable, EngineConstants.HASH_COMMAND, nTacticID);
               nTacticSubCommand = GetHashedM2DAInt(nPackageTable, EngineConstants.HASH_SUBCOMMAND, nTacticID);
          }

          nTacticTargetBitField = GetHashedM2DAInt(EngineConstants.TABLE_AI_TACTICS_TARGET_TYPE, EngineConstants.HASH_TYPE, nTacticTargetType);

#if DEBUG
          string sTacticSubCommand = IntToString(nTacticSubCommand);
          string sTacticCommand = _AI_GetCommandString(nTacticCommand);

          if (nTacticCommand == EngineConstants.AI_COMMAND_USE_ABILITY || nTacticCommand == EngineConstants.AI_COMMAND_ACTIVATE_MODE || nTacticCommand == EngineConstants.AI_COMMAND_DEACTIVATE_MODE)
               sTacticSubCommand = Log_GetAbilityNameById(nTacticSubCommand);

          Log_Trace_AI("_AI_ExecuteTactic", "[" + IntToString(nTacticID) + "]" +
                                            "[Target: " + IntToString(nTacticTargetType) + "] " +
                                            "[Cond: " + IntToString(nTacticCondition) + "] " +
                                            "[" + sTacticCommand + "] " +
                                            "[" + sTacticSubCommand + "] ");

          if (sTacticItemTag != "")
               Log_Trace_AI("_AI_ExecuteTactic", "[Item Tag]: " + sTacticItemTag);
#endif

          int nTacticCondition_Base = GetHashedM2DAInt(EngineConstants.TABLE_TACTICS_CONDITIONS, EngineConstants.HASH_CONDITIONBASE, nTacticCondition);

          // Retrieve condition details
          int nTacticCondition_ValidTarget = GetHashedM2DAInt(EngineConstants.TABLE_TACTICS_BASE_CONDITIONS, EngineConstants.HASH_VALIDFORTARGET, nTacticCondition_Base);
          int nTacticCondition_Parameter = GetHashedM2DAInt(EngineConstants.TABLE_TACTICS_CONDITIONS, EngineConstants.HASH_CONDITIONPARAMETER, nTacticCondition);
          int nTacticCondition_Parameter2 = GetHashedM2DAInt(EngineConstants.TABLE_TACTICS_CONDITIONS, unchecked((int)EngineConstants.HASH_CONDITIONPARAMETER2), nTacticCondition);

          Log_Trace_AI("_AI_ExecuteTactic", "[Condition Valid for Target: " + IntToString(nTacticCondition_ValidTarget) + "] " +
                                            "[Condition Base: " + IntToString(nTacticCondition_Base) + "] " +
                                            "[Condition Parameter: " + IntToString(nTacticCondition_Parameter) + "] ");

          // Verify that the target is valid
          if ((nTacticTargetBitField & nTacticCondition_ValidTarget) == 0)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "Tactic target type is not valid for the specified condition!", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
               return EngineConstants.FALSE;
          }

          // Verify that the ability can be executed (ignoring any possible target)

          if (_AI_IsCommandValid(nTacticCommand, nTacticSubCommand, nTacticTargetType) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "Tactic xCommand can not be executed");
#endif
               return EngineConstants.FALSE;
          }

          // Handling the tactic based on the base condition

          GameObject oTarget = null; // Any tactic action will be applied to this GameObject
          Vector3 lTarget = Vector3.zero;
          GameObject oFollowerSelectedTarget = null; // for tracking follower targets
          if (IsFollower(gameObject) != EngineConstants.FALSE)
               oFollowerSelectedTarget = GetAttackTarget(gameObject); // last target - hostile or not

          if (GetHasEffects(gameObject, EngineConstants.EFFECT_TYPE_CONFUSION) != EngineConstants.FALSE && nTacticTargetType == EngineConstants.AI_TARGET_TYPE_SELF)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "Confused creature trying to target SELF - aborting tactic");
#endif
               return EngineConstants.FALSE;
          }

          switch (nTacticCondition_Base)
          {
               case EngineConstants.AI_BASE_CONDITION_HAS_EFFECT_APPLIED:
                    {
                         oTarget = _AI_Condition_GetCreatureWithAIStatus(nTacticCondition_Parameter, nTacticTargetType, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_HP_LEVEL:
                    {
                         oTarget = _AI_Condition_GetCreatureWithHPLevel(nTacticCondition_Parameter, nTacticTargetType, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_MANA_OR_STAMINA_LEVEL:
                    {
                         oTarget = _AI_Condition_GetCreatureWithManaOrStaminaLevel(nTacticCondition_Parameter, nTacticTargetType, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_MOST_DAMAGED_IN_PARTY:
                    {
                         oTarget = _AI_Condition_GetNthMostDamagedCreatureInGroup(nTacticCondition_Parameter, nTacticTargetType, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_CLUSTERED_WITH_SAME_GROUP:
                    {
                         lTarget = _AI_Condition_GetEnemyClusteredWithSameGroup(nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_MOST_HATED_ENEMY:
                    {
                         oTarget = _AI_Condition_GetMostHatedEnemy(nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_NEAREST_VISIBLE:
                    {
                         oTarget = _AI_Condition_GetNearestVisibleCreature(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_NEAREST_RACE:
                    {
                         oTarget = _AI_Condition_GetNearestVisibleCreatureByRace(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_NEAREST_CLASS:
                    {
                         oTarget = _AI_Condition_GetNearestVisibleCreatureByClass(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_NEAREST_GENDER:
                    {
                         oTarget = _AI_Condition_GetNearestVisibleCreatureByGender(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_ATTACKING_PARTY_MEMBER:
                    {
                         oTarget = _AI_Condition_GetNearestEnemyAttackingPartyMember(nTacticCommand, nTacticSubCommand, nTacticCondition_Parameter, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_HAS_ANY_BUFF_EFFECT:
                    {
                         oTarget = _AI_Condition_GetNearestEnemyWithAnyBuffEffect(nTacticCommand, nTacticSubCommand, nTacticCondition_Parameter, nTacticID, nTacticTargetType);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_FLIP_COVER_STATE:
                    {
                         oTarget = _AI_Condition_GetNearestFlipCoverByState(nTacticCondition_Parameter, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_VULNERABLE_TO_DAMAGE:
                    {
                         oTarget = _AI_Condition_GetEnemyVulnerableToDamage(nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_ANY:
                    {
                         oTarget = _AI_Condition_GetAnyTarget(nTacticTargetType, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_HAS_AMMO_LEVEL:
                    {
                         oTarget = _AI_Condition_SelfHasAmmoLevel(nTacticCondition_Parameter);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_HAS_ARMOR_TYPE:
                    {
                         oTarget = _AI_Condition_HasArmorType(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_MOST_ENEMIES_HAVE_ARMOR_TYPE:
                    {
                         oTarget = _AI_Condition_MostEnemiesHaveArmorType(nTacticCondition_Parameter);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_ALL_ENEMIES_HAVE_ARMOR_TYPE:
                    {
                         oTarget = _AI_Condition_AllEnemiesHaveArmorType(nTacticCondition_Parameter);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_TARGET_HAS_RANK:
                    {
                         oTarget = _AI_Condition_TargetHasRank(nTacticTargetType, nTacticCondition_Parameter, nTacticID, nTacticCommand, nTacticSubCommand);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_BEING_ATTACKED_BY_ATTACK_TYPE:
                    {
                         oTarget = _AI_Condition_BeingAttackedByAttackType(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_USING_ATTACK_TYPE:
               case EngineConstants.AI_BASE_CONDITION_TARGET_USING_ATTACK_TYPE_FOLLOWER:
                    {
                         oTarget = _AI_Condition_UsingAttackType(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_MOST_ENEMIES_USING_ATTACK_TYPE:
                    {
                         oTarget = _AI_Condition_MostEnemiesUsingAttackType(nTacticCondition_Parameter);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_ALL_ENEMIES_USING_ATTACK_TYPE:
                    {
                         oTarget = _AI_Condition_AllEnemiesUsingAttackType(nTacticCondition_Parameter);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_AT_LEAST_X_ENEMIES_ARE_ALIVE:
                    {
                         oTarget = _AI_Condition_AtLeastXEnemiesAreAlive(nTacticTargetType, nTacticCondition_Parameter);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_AT_LEAST_X_CREATURES_ARE_DEAD:
                    {
                         oTarget = _AI_Condition_AtLeastXCreaturesAreDead(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_AT_LEAST_X_ALLIES_ARE_ALIVE:
                    {
                         oTarget = _AI_Condition_AtLeastXAlliesAreAlive(nTacticTargetType, nTacticCondition_Parameter, nTacticCondition_Parameter2);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_ENEMY_AI_TARGET_AT_RANGE:
                    {
                         oTarget = _AI_Condition_GetTargetAtRange(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_TARGET_AT_FLANK_LOCATION:
                    {
                         oTarget = _AI_Condition_GetTargetAtFlankLocation(nTacticCondition_Parameter, nTacticTargetType);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_SURROUNDED_BY_TARGETS:
                    {
                         oTarget = _AI_Condition_SurroundedByAtLeastXEnemies(nTacticCommand, nTacticSubCommand, nTacticCondition_Parameter, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_USING_RANGED_ATTACKS_AT_RANGE:
                    {
                         oTarget = _AI_Condition_GetTargetUsingRangedWeaponsAtRange(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_PARTY_MEMBERS_TARGET:
                    {
                         oTarget = _AI_Condition_GetPartyMemberTarget(nTacticCommand, nTacticSubCommand, nTacticCondition_Parameter, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_SELF_HP_LEVEL:
                    {
                         oTarget = _AI_Condition_SelfHPLevel(nTacticCondition_Parameter, nTacticTargetType, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_SELF_MANA_STAMINA_LEVEL:
                    {
                         oTarget = _AI_Condition_SelfManaStaminaLevel(nTacticCondition_Parameter, nTacticTargetType, nTacticCommand, nTacticSubCommand, nTacticID);
                         break;
                    }
               case EngineConstants.AI_BASE_CONDITION_FOLLOWER_AI_TARGET_AT_RANGE:
                    {
                         oTarget = _AI_Condition_GetTargetAtRange(nTacticTargetType, nTacticCondition_Parameter, nTacticCommand, nTacticSubCommand);
                         break;
                    }


          }

          // If target type is SELF: fail in case was tryng to trigger an ability that tries to cure something SELF doesn't have
          // For example: casting 'remove poison' while SELF does not have any poison xEffect
          // NOTE: this is handled for allies in _AIGetAllies
          if (nTacticTargetType == EngineConstants.AI_TARGET_TYPE_SELF && nTacticCommand == EngineConstants.AI_COMMAND_USE_ABILITY)
          {
               if (_AI_IsTargetValidForBeneficialAbility(gameObject, nTacticSubCommand) == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteTactic", "Trying to apply a beneficial ability to SELF, but SELF does not need it");
#endif
                    oTarget = null;
               }
          }

          if (IsLocationValid(lTarget) == EngineConstants.FALSE && IsObjectValid(oTarget) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "No valid target for condition");
#endif
               return EngineConstants.FALSE;
          }
          if (IsLocationValid(lTarget) != EngineConstants.FALSE)
          {
               Vector3 vDebug = GetPositionFromLocation(lTarget);
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "VALID LOCATION: " + VectorToString(vDebug));
#endif
          }

#if DEBUG
          Log_Trace_AI("_AI_ExecuteTactic", "GOT TARGET: " + GetTag(oTarget));
#endif


          // Check if the xCommand is valid on the target and execute the xCommand
          xCommand cTacticCommand = new xCommand(EngineConstants.COMMAND_TYPE_INVALID);
          int nAbilityTargetType;
          int nStationary = GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY);
          if (IsFollower(gameObject) != EngineConstants.FALSE) nStationary = EngineConstants.FALSE;

          Log_Trace_AI("_AI_ExecuteTactic", "Creature stationary state: " + IntToString(nStationary));

          switch (nTacticCommand)
          {
               case EngineConstants.AI_COMMAND_USE_HEALTH_POTION_MOST:
                    {
                         float fCurrentHealth = GetCurrentHealth(gameObject);
                         float fMaxHealth = GetMaxHealth(gameObject);
                         if (fCurrentHealth == fMaxHealth)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "health full, not using health potion");
#endif
                              return EngineConstants.FALSE;
                         }
                         GameObject oItem = _AI_GetPotionByFilter(EngineConstants.AI_POTION_TYPE_HEALTH, EngineConstants.AI_POTION_LEVEL_MOST_POWERFUL);
                         cTacticCommand = _AI_GetPotionUseCommand(oItem);

                         if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID)
                              return EngineConstants.FALSE;

                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_HEALTH_POTION_LEAST:
                    {
                         float fCurrentHealth = GetCurrentHealth(gameObject);
                         float fMaxHealth = GetMaxHealth(gameObject);
                         if (fCurrentHealth == fMaxHealth)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "health full, not using health potion");
#endif
                              return EngineConstants.FALSE;
                         }
                         GameObject oItem = _AI_GetPotionByFilter(EngineConstants.AI_POTION_TYPE_HEALTH, EngineConstants.AI_POTION_LEVEL_LEAST_POWERFUL);
                         cTacticCommand = _AI_GetPotionUseCommand(oItem);

                         if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID)
                              return EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_LYRIUM_POTION_MOST:
                    {
                         float fCurrentMana = GetCurrentManaStamina(gameObject);
                         float fMaxMana = IntToFloat(GetCreatureMaxMana(gameObject));
                         if (fCurrentMana == fMaxMana)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "mana full, not using mana potion");
#endif
                              return EngineConstants.FALSE;
                         }
                         GameObject oItem = _AI_GetPotionByFilter(EngineConstants.AI_POTION_TYPE_MANA, EngineConstants.AI_POTION_LEVEL_MOST_POWERFUL);
                         cTacticCommand = _AI_GetPotionUseCommand(oItem);

                         if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID)
                              return EngineConstants.FALSE;

                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_LYRIUM_POTION_LEAST:
                    {
                         float fCurrentMana = GetCurrentManaStamina(gameObject);
                         float fMaxMana = IntToFloat(GetCreatureMaxMana(gameObject));
                         if (fCurrentMana == fMaxMana)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "mana full, not using mana potion");
#endif
                              return EngineConstants.FALSE;
                         }
                         GameObject oItem = _AI_GetPotionByFilter(EngineConstants.AI_POTION_TYPE_MANA, EngineConstants.AI_POTION_LEVEL_LEAST_POWERFUL);
                         cTacticCommand = _AI_GetPotionUseCommand(oItem);

                         if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID)
                              return EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.AI_COMMAND_RUN_SCRIPT:
                    {
#if DEBUG
                         Log_Trace_AI("_AI_ExecuteTactic", "Running custom script (custom AI xEvent)");
#endif
                         xEvent evCustomAI = Event(EngineConstants.EVENT_TYPE_HANDLE_CUSTOM_AI);
                         xCommand cLast = GetPreviousCommand(gameObject);
                         int nLastCommand = GetCommandType(cLast);
                         SendEventHandleCustomAI(gameObject, null, nLastCommand, nLastCommandStatus,
                             -1, nTacticTargetType, nTacticSubCommand, nTacticID);

                         return EngineConstants.TRUE;
                    }
               case EngineConstants.AI_COMMAND_SWITCH_TO_MELEE:
                    {
#if DEBUG
                         Log_Trace_AI("_AI_ExecuteTactic", "Switching to melee weapon set");
#endif
                         if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_MELEE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Creature already has melee weapons equipped - aborting xCommand");
#endif
                              return EngineConstants.FALSE;
                         }
                         cTacticCommand = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_MELEE);
                         break;
                    }
               case EngineConstants.AI_COMMAND_SWITCH_TO_RANGED:
                    {
#if DEBUG
                         Log_Trace_AI("_AI_ExecuteTactic", "Switching to ranged weapon set");
#endif
                         if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_RANGED)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Creature already has ranged weapons equipped - aborting xCommand");
#endif
                              return EngineConstants.FALSE;
                         }
                         cTacticCommand = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED);
                         break;
                    }
               case EngineConstants.AI_COMMAND_JUMP_TO_LATER_TACTIC:
                    {
                         if (nTacticSubCommand != -1 && nTacticSubCommand <= nTacticID)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Jump to later tactic: invalid value (must be greater then current tactic id)");
#endif
                              return EngineConstants.FALSE;
                         }
                         return nTacticSubCommand;
                    }
               case EngineConstants.AI_COMMAND_FLY:
                    {
                         GameObject oTurnTo = null;

                         if (_AI_CheckMoveTimer() == EngineConstants.FALSE)
                              return EngineConstants.FALSE;

                         switch (nTacticSubCommand)
                         {
                              case EngineConstants.AI_FLY_TURN_MOST_HATED:
                                   {
                                        oTurnTo = AI_Threat_GetThreatTarget(gameObject);
                                        cTacticCommand = _AI_GetFlyCommand(oTurnTo);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_FLY_TURN_NEAREST_AI_WP:
                                   {
                                        oTurnTo = UT_GetNearestObjectByTag(gameObject, EngineConstants.AI_WP_MOVE);
                                        cTacticCommand = _AI_GetFlyCommand(oTurnTo);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_FLY_TURN_NEAREST_ALLY:
                                   {
                                        List<GameObject> arAllies = _AI_GetAllies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oTurnTo = arAllies[0];
                                        cTacticCommand = _AI_GetFlyCommand(oTurnTo);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_FLY_TURN_NEAREST_ENEMY:
                                   {
                                        List<GameObject> arEnemies = _AI_GetEnemies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oTurnTo = arEnemies[0];
                                        cTacticCommand = _AI_GetFlyCommand(oTurnTo);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_FLY_APPROACH_MOST_HATED:
                                   {
                                        oTurnTo = AI_Threat_GetThreatTarget(gameObject);
                                        cTacticCommand = _AI_GetFlyCommand(oTurnTo, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_FLY_APPROACH_NEAREST_ENEMY:
                                   {
                                        List<GameObject> arEnemies = _AI_GetEnemies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oTurnTo = arEnemies[0];
                                        cTacticCommand = _AI_GetFlyCommand(oTurnTo, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_FLY_APPROACH_AI_WP_NEAREST_TO_MOST_HATED:
                                   {
                                        GameObject oEnemy = AI_Threat_GetThreatTarget(gameObject);
                                        oTurnTo = UT_GetNearestObjectByTag(oEnemy, EngineConstants.AI_WP_MOVE);
                                        cTacticCommand = _AI_GetFlyCommand(oTurnTo, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_FLY_APPROACH_AI_WP_NEAREST_TO_NEAREST_ENEMY:
                                   {
                                        List<GameObject> arEnemies = _AI_GetEnemies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        GameObject oEnemy = arEnemies[0];
                                        oTurnTo = UT_GetNearestObjectByTag(oEnemy, EngineConstants.AI_WP_MOVE);
                                        cTacticCommand = _AI_GetFlyCommand(oTurnTo, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                         }
                         if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_WAIT)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Wait xCommand for fly/turn AI action - aborting AI instead");
#endif
                              return EngineConstants.FALSE;
                         }

                         if (IsObjectValid(oTurnTo) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Invalid turn target");
#endif
                              return EngineConstants.FALSE;
                         }
                         if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Invalid fly/turn xCommand");
#endif
                              return EngineConstants.FALSE;
                         }
                         _AI_SetMoveTimer();
                         break;
                    }
               case EngineConstants.AI_COMMAND_MOVE:
                    {
                         GameObject oMoveTo = null;
                         if (nStationary > 0)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Creature stationary - can't excute move xCommand");
#endif
                              return EngineConstants.FALSE;
                         }
                         if (nLastCommandStatus == EngineConstants.COMMAND_FAILED_TIMEOUT)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Last xCommand failed on timeout - can't excute move xCommand");
#endif
                              return EngineConstants.FALSE;
                         }
                         if (_AI_CheckMoveTimer() == EngineConstants.FALSE)
                              return EngineConstants.FALSE;

                         switch (nTacticSubCommand)
                         {
                              case EngineConstants.AI_MOVE_HATED_ENEMY:
                                   {
                                        oMoveTo = AI_Threat_GetThreatTarget(gameObject);
                                        cTacticCommand = CommandMoveToObject(oMoveTo, EngineConstants.TRUE, 0.0f, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_MOVE_NEAREST_AI_WP:
                                   {
                                        oMoveTo = UT_GetNearestObjectByTag(gameObject, EngineConstants.AI_WP_MOVE);
                                        cTacticCommand = CommandMoveToObject(oMoveTo, EngineConstants.TRUE, 0.0f, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_MOVE_NEAREST_ALLY:
                                   {
                                        List<GameObject> arAllies = _AI_GetAllies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oMoveTo = arAllies[0];
                                        cTacticCommand = CommandMoveToObject(oMoveTo, EngineConstants.TRUE, 0.0f, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_MOVE_NEAREST_ENEMY:
                                   {
                                        List<GameObject> arEnemies = _AI_GetEnemies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oMoveTo = arEnemies[0];
                                        cTacticCommand = CommandMoveToObject(oMoveTo, EngineConstants.TRUE, 0.0f, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_MOVE_RANDOM_AI_WP:
                                   {
                                        int nRand = Engine_Random(3);
                                        List<GameObject> arWPs = GetNearestObjectByTag(gameObject, EngineConstants.AI_WP_MOVE, EngineConstants.OBJECT_TYPE_WAYPOINT, 3);
                                        oMoveTo = arWPs[nRand];
                                        if (IsObjectValid(oMoveTo) == EngineConstants.FALSE) // in case there are not enough AI waypoints
                                             oMoveTo = arWPs[0];
                                        cTacticCommand = CommandMoveToObject(oMoveTo, EngineConstants.TRUE, 0.0f, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_MOVE_AWAY_FROM_ENEMY_MEDIUM:
                                   {
                                        List<GameObject> arEnemies = _AI_GetEnemies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oMoveTo = arEnemies[0]; // Move away from
                                        float fDistance = GetDistanceBetween(gameObject, oMoveTo);
                                        if (fDistance >= EngineConstants.AI_MOVE_AWAY_DISTANCE_MEDIUM)
                                        {
#if DEBUG
                                             Log_Trace_AI("_AI_ExecuteTactic", "NOT MOVING AWAY - already far away from target");
#endif
                                             return EngineConstants.FALSE;
                                        }
                                        cTacticCommand = CommandMoveAwayFromObject(oMoveTo, EngineConstants.AI_MOVE_AWAY_DISTANCE_MEDIUM, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_MOVE_AWAY_FROM_ENEMY_SHORT:
                                   {
                                        List<GameObject> arEnemies = _AI_GetEnemies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oMoveTo = arEnemies[0]; // Move away from
                                        float fDistance = GetDistanceBetween(gameObject, oMoveTo);
                                        if (fDistance >= EngineConstants.AI_MOVE_AWAY_DISTANCE_SHORT)
                                        {
#if DEBUG
                                             Log_Trace_AI("_AI_ExecuteTactic", "NOT MOVING AWAY - already far away from target");
#endif
                                             return EngineConstants.FALSE;
                                        }
                                        cTacticCommand = CommandMoveAwayFromObject(oMoveTo, EngineConstants.AI_MOVE_AWAY_DISTANCE_SHORT, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_MOVE_AWAY_FROM_ENEMY_RANDOM:
                                   {
                                        List<GameObject> arEnemies = _AI_GetEnemies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oMoveTo = arEnemies[0]; // Move away from
                                        float fDistanceToMoveAway = RandomF(FloatToInt(EngineConstants.AI_MOVE_AWAY_DISTANCE_MEDIUM - EngineConstants.AI_MOVE_AWAY_DISTANCE_SHORT), FloatToInt(EngineConstants.AI_MOVE_AWAY_DISTANCE_SHORT));

                                        float fDistance = GetDistanceBetween(gameObject, oMoveTo);
                                        if (fDistance >= fDistanceToMoveAway)
                                        {
#if DEBUG
                                             Log_Trace_AI("_AI_ExecuteTactic", "NOT MOVING AWAY - already far away from target");
#endif
                                             return EngineConstants.FALSE;
                                        }
                                        cTacticCommand = CommandMoveAwayFromObject(oMoveTo, fDistanceToMoveAway, EngineConstants.TRUE);
                                        SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, EngineConstants.AI_TACTIC_ID_MOVE); // the last tactic being used
                                        break;
                                   }
                              case EngineConstants.AI_MOVE_AWAY_FROM_ENEMY_COWARD:
                                   {
                                        List<GameObject> arEnemies = _AI_GetEnemies(EngineConstants.AI_COMMAND_MOVE, nTacticSubCommand);
                                        oMoveTo = arEnemies[0]; // Move away from
                                        float fDistance = GetDistanceBetween(gameObject, oMoveTo);
                                        if (fDistance < EngineConstants.AI_MOVE_AWAY_DISTANCE_MEDIUM)
                                             // run away
                                             cTacticCommand = CommandMoveToObject(oMoveTo, EngineConstants.TRUE, EngineConstants.AI_MOVE_AWAY_DISTANCE_MEDIUM, EngineConstants.FALSE);
                                        else
                                             // cower in fear
                                             cTacticCommand = CommandPlayAnimation(602);

                                        break;
                                   }
                         }

                         if (IsObjectValid(oMoveTo) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Invalid movement target");
#endif
                              return EngineConstants.FALSE;
                         }
                         if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Invalid move xCommand");
#endif
                              return EngineConstants.FALSE;
                         }
                         _AI_SetMoveTimer();
                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_ITEM:
                    {
                         // ASSUMING ITEMS CAN BE USED ONLY ON SELF

                         if (Ability_CheckUseConditions(gameObject, gameObject, nTacticSubCommand) == EngineConstants.FALSE)
                              return EngineConstants.FALSE; // failed tactic

                         if (_AI_CanUseAbility(nTacticSubCommand, gameObject) == EngineConstants.FALSE)
                              return EngineConstants.FALSE; // can't use specific ability

                         // Command is valid to be executed on the target
                         Vector3 vNul = Vector3.zero;
                         cTacticCommand = CommandUseAbility(nTacticSubCommand, gameObject, vNul, -1.0f, sTacticItemTag);
                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_PLACEABLE:
                    {
                         // Target should be valid now
                         // At this moment the user should register an action on the placeable
                         if (nStationary > 0)
                         {
                              float fDistance = GetDistanceBetween(gameObject, oTarget);
                              if (fDistance > EngineConstants.AI_STATIONARY_RANGE)
                              {
#if DEBUG
                                   Log_Trace_AI("_AI_ExecuteTactic", "Creature stationary - placeable too far away to execute xCommand");
#endif
                                   return EngineConstants.FALSE;
                              }
                         }

                         int nCount = GetLocalInt(oTarget, EngineConstants.PLC_FLIP_COVER_USE_COUNT);
                         nCount++;
                         SetLocalInt(oTarget, EngineConstants.PLC_FLIP_COVER_USE_COUNT, nCount);

                         // Check that I'm not already using a flip cover
                         GameObject oPlaceable = GetLocalObject(gameObject, EngineConstants.AI_PLACEABLE_BEING_USED);
                         if (IsObjectValid(oPlaceable) != EngineConstants.FALSE && IsDead(oPlaceable) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "I'm already using a placeable!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                              return EngineConstants.FALSE;
                         }
                         SetLocalObject(gameObject, EngineConstants.AI_PLACEABLE_BEING_USED, oTarget);

                         cTacticCommand = CommandUseObject(oTarget, EngineConstants.PLACEABLE_ACTION_USE);
                         break;
                    }
               case EngineConstants.AI_COMMAND_ATTACK:
                    {
                         // If target is not a hostile creature then fail the attack
                         if (IsObjectHostile(gameObject, oTarget) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Non-hostile target for ATTACK action!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                              return EngineConstants.FALSE;
                         }
                         cTacticCommand = _AI_ExecuteAttack(oTarget, nLastCommandStatus);

                         break;
                    }
               case EngineConstants.AI_COMMAND_ACTIVATE_MODE:
                    {
                         if (Ability_CheckUseConditions(gameObject, oTarget, nTacticSubCommand) == EngineConstants.FALSE)
                              return EngineConstants.FALSE; // failed tactic

                         cTacticCommand = CommandUseAbility(nTacticSubCommand, gameObject, Vector3.zero);
                         break;
                    }
               case EngineConstants.AI_COMMAND_DEACTIVATE_MODE:
                    {
                         // No need to check use conditions for the ability since we are trying to deactivate it

                         cTacticCommand = CommandUseAbility(nTacticSubCommand, gameObject, Vector3.zero);
                         break;
                    }
               case EngineConstants.AI_COMMAND_WAIT:
                    {
                         int bQuick = EngineConstants.FALSE;
                         if (nTacticSubCommand == 1) // this is a wait with cooldown (used for rogues for example)
                         {
                              int nMoveStart = GetLocalInt(gameObject, EngineConstants.AI_WAIT_TIMER);
                              int nCurrentTime = GetTime();
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Wait time dif: " + IntToString(nCurrentTime - nMoveStart));
#endif
                              if (nMoveStart != 0 && nCurrentTime - nMoveStart <= EngineConstants.AI_WAIT_MIN_TIME)
                              {
#if DEBUG
                                   Log_Trace_AI("_AI_ExecuteTactic", "Last wait happened too soon");
#endif
                                   return EngineConstants.FALSE;
                              }
                              SetLocalInt(gameObject, EngineConstants.AI_WAIT_TIMER, nCurrentTime);
                              bQuick = EngineConstants.TRUE;
                         }
                         cTacticCommand = _AI_DoNothing(nLastTacticID, nLastCommandStatus, EngineConstants.FALSE, EngineConstants.TRUE);
                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_ABILITY:
                    {

                         // -----------------------------------------------------------------
                         // Ability usage disabled
                         // -----------------------------------------------------------------
                         if (GetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_AI_NO_ABILITIES) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "ABORT: AI_COMMAND_USE_ABILITY - EngineConstants.CREATURE_RULES_FLAG_AI_NO_ABILITIES was set.", EngineConstants.LOG_SEVERITY_WARNING);
                              Warning("ERROR! ability use disabled by a debug flag - call Yaron if you weren't using debug scripts!!!");
#endif
                              return EngineConstants.FALSE;
                         }

                         // Checking target types
                         nAbilityTargetType = Ability_GetAbilityTargetType(nTacticSubCommand, Ability_GetAbilityType(nTacticSubCommand));
                         // NOTICE: tactic target types are not exactly the same as ability target types

                         // Make sure the target type specified for the ability matches a target type that is valid for the ability
                         switch (nTacticTargetType)
                         {
                              case EngineConstants.AI_TARGET_TYPE_ENEMY:
                              case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                                   {
                                        if (nAbilityTargetType != EngineConstants.TARGET_TYPE_HOSTILE_CREATURE)
                                        {
                                             // Trying to find a target anyways
#if DEBUG
                                             Log_Trace_AI("_AI_ExecuteTactic", "Hostile target for an ability that does not support hostile targets - trying to find a new target!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                                             if (nAbilityTargetType == EngineConstants.TARGET_TYPE_SELF)
                                             {
#if DEBUG
                                                  Log_Trace_AI("_AI_ExecuteTactic", "Target will now be gameObject");
#endif
                                                  oTarget = gameObject;
                                             }
                                             else if (nAbilityTargetType == EngineConstants.TARGET_TYPE_GROUND)
                                             {
#if DEBUG
                                                  Log_Trace_AI("_AI_ExecuteTactic", "Target will now be Vector3 of target");
#endif
                                                  lTarget = GetLocation(oTarget);
                                             }
                                             else
                                                  return EngineConstants.FALSE;
                                        }
                                        break;
                                   }
                              case EngineConstants.AI_TARGET_TYPE_ALLY:
                                   {
                                        if (nAbilityTargetType != EngineConstants.TARGET_TYPE_FRIENDLY_CREATURE)
                                        {
                                             if (nAbilityTargetType == EngineConstants.TARGET_TYPE_SELF)
                                             {
#if DEBUG
                                                  Log_Trace_AI("_AI_ExecuteTactic", "Ally target for an ability that does not support friendly targets - Target will now be SELF!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                                                  oTarget = gameObject;
                                             }
                                             else
                                             {
#if DEBUG
                                                  Log_Trace_AI("_AI_ExecuteTactic", "Ally target for an ability that does not support friendly targets - FAILING TACTIC!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                                                  return EngineConstants.FALSE;
                                             }
                                        }
                                        break;
                                   }
                              case EngineConstants.AI_TARGET_TYPE_PLACEABLE:
                                   {
                                        if (nAbilityTargetType != EngineConstants.TARGET_TYPE_PLACEABLE)
                                        {
#if DEBUG
                                             Log_Trace_AI("_AI_ExecuteTactic", "Placeable target for an ability that does not support placeable targets - FAILING TACTIC!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                                             return EngineConstants.FALSE;
                                        }
                                        break;
                                   }
                              case EngineConstants.AI_TARGET_TYPE_SELF:
                                   {
                                        if (nAbilityTargetType != EngineConstants.TARGET_TYPE_SELF)
                                        {
#if DEBUG
                                             Log_Trace_AI("_AI_ExecuteTactic", "Self target for an ability that does not support self targets - trying to find a different target!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                                             if (nAbilityTargetType == EngineConstants.TARGET_TYPE_HOSTILE_CREATURE)
                                             {
                                                  if (IsFollower(gameObject) != EngineConstants.FALSE)
                                                  {
                                                       if (_AI_IsHostileTargetValid(oFollowerSelectedTarget) != EngineConstants.FALSE)
                                                            oTarget = oFollowerSelectedTarget;
                                                       else
                                                            oTarget = _AI_Condition_GetNearestVisibleCreature(EngineConstants.AI_TARGET_TYPE_ENEMY, 1, nTacticCommand, nTacticSubCommand, nTacticID);
                                                  }
                                                  else
                                                       oTarget = _AI_Condition_GetMostHatedEnemy(1, nTacticCommand, nTacticSubCommand, nTacticID);
                                             }
                                             else if (nAbilityTargetType == EngineConstants.TARGET_TYPE_FRIENDLY_CREATURE)
                                             {
                                                  oTarget = _AI_Condition_GetAnyTarget(EngineConstants.AI_TARGET_TYPE_ALLY, nTacticCommand, nTacticSubCommand, nTacticID);
                                             }
                                             else
                                             {
#if DEBUG
                                                  Log_Trace_AI("_AI_ExecuteTactic", "Can not find a different target for this ability target type!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                                                  return EngineConstants.FALSE;
                                             }

                                             if (IsObjectValid(oTarget) == EngineConstants.FALSE)
                                             {
#if DEBUG
                                                  Log_Trace_AI("_AI_ExecuteTactic", "Failed to find a secondary target for this ability!", EngineConstants.LOG_SEVERITY_WARNING);
#endif
                                                  return EngineConstants.FALSE;
                                             }

                                        }
                                        break;
                                   }
                         }

                         if (nLastCommandStatus == EngineConstants.COMMAND_FAILED_TIMEOUT && IsObjectHostile(gameObject, oTarget) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteTactic", "Last xCommand failed on timeout and this ability targets hostiles - aborting ability use");
#endif
                              return EngineConstants.FALSE;
                         }

                         if (Ability_CheckUseConditions(gameObject, oTarget, nTacticSubCommand) == EngineConstants.FALSE)
                              return EngineConstants.FALSE; // failed tactic

                         if (_AI_CanUseAbility(nTacticSubCommand, oTarget) == EngineConstants.FALSE)
                              return EngineConstants.FALSE; // can't use specific ability

                         Vector3 vTarget = Vector3.zero;

                         if (IsLocationValid(lTarget) != EngineConstants.FALSE)
                         {
                              vTarget = GetPositionFromLocation(lTarget);
                              oTarget = null;
                         }
                         // Command is valid to be executed on the target
                         cTacticCommand = CommandUseAbility(nTacticSubCommand, oTarget, vTarget);
                         break;
                    }
          }

          if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "INVALID TACTIC COMMAND - FAILING TACTIC", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
               return EngineConstants.FALSE; ;
          }

          if (GetHasEffects(gameObject, EngineConstants.EFFECT_TYPE_CONFUSION) != EngineConstants.FALSE && oTarget == gameObject)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "Confused creature trying to target SELF (second check) - aborting tactic");
#endif
               return EngineConstants.FALSE;
          }

          if (IsFollower(gameObject) != EngineConstants.FALSE && IsObjectValid(oTarget) != EngineConstants.FALSE && IsObjectHostile(gameObject, oTarget) != EngineConstants.FALSE
              && AI_GetPartyAllowedToAttack() == EngineConstants.FALSE)
          {

               if (IsControlled(gameObject) != EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteTactic", "Controlled follower attacking - clearing rest of party to target enemies");
#endif
                    AI_SetPartyAllowedToAttack(EngineConstants.TRUE);
               }
               else if (AI_BehaviorCheck_AttackOnCombatStart() == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteTactic", "Non-controlled follower trying to attack a hostile before being allowed - trying to move closer to leader");
#endif
                    cTacticCommand = _AI_MoveToControlled(nLastCommandStatus);
               }
          }

          // Flagging last tactic used
          SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, nTacticID);
          float fTimer = EngineConstants.AI_COMMAND_TIMER;
          if (oTarget == gameObject)
               fTimer = 0.0f;
          WR_AddCommand(gameObject, cTacticCommand, EngineConstants.FALSE, EngineConstants.FALSE, -1, fTimer);

#if DEBUG
          Log_Trace_AI("_AI_ExecuteTactic", "***** TACTIC EXECUTED! *****");
#endif
          return EngineConstants.TRUE;
     }

     /* @brief Returns the number of tactics the creature has in it's package
*
* The number of tactics is basically the number of rows in the package 2da
*
* @param rPackageTable the package table for which we want the number of tactics in
* @returns Number of tactics the creature has in the associated package file
* @author Yaron
*/
     public int _AI_GetTacticsNum(int nPackageTable)
     {
          return GetM2DARows(nPackageTable);
     }

     /* @brief Returns the package table name ID that was set for the current creature
*
* @returns The name of the ID of table attached to the creature, or the default one if the creature has none
* @author Yaron
*/
     public int _AI_GetPackageTable()
     {
          // For now, returning a table number set on the creature

          int nTable = -1;
          if (_AI_HasAIStatus(gameObject, EngineConstants.AI_STATUS_POLYMORPH) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_GetPackageTable", "POLYMORPH ON!");
#endif
               int nNewTable = -1;
               if (IsModalAbilityActive(gameObject, EngineConstants.ABILITY_SPELL_BEAR) != EngineConstants.FALSE)
                    nNewTable = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "TacticsTable", EngineConstants.ABILITY_SPELL_BEAR);
               else if (IsModalAbilityActive(gameObject, EngineConstants.ABILITY_SPELL_SPIDER_SHAPE) != EngineConstants.FALSE)
                    nNewTable = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "TacticsTable", EngineConstants.ABILITY_SPELL_SPIDER_SHAPE);
#if DEBUG
               Log_Trace_AI("_AI_GetPackageTable", "polymorph new table: " + IntToString(nNewTable));
#endif
               if (nNewTable != -1)
               {
#if DEBUG
                    Log_Trace_AI("_AI_GetPackageTable", "SHAPECHANGED - switching to new table: " + IntToString(nNewTable));
#endif
                    nTable = nNewTable;
               }
          }
          else if (_AI_UseGUITables() != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_GetPackageTable", "Creature is FOLLOWER - using GUI table");
#endif
          }

          if (nTable == -1)
          {
               nTable = GetPackageAI(gameObject);

               if (nTable <= EngineConstants.AI_TABLE_DEFAULT)
                    nTable = EngineConstants.AI_TABLE_DEFAULT;
#if DEBUG
               Log_Trace_AI("_AI_GetPackageTable", "Using table: " + IntToString(nTable));
#endif
          }

          return nTable;
     }

     public int _AI_IsAbilityValid(int nAbilityID)
     {
          int nAbilityType = Ability_GetAbilityType(nAbilityID);
          int nResult = EngineConstants.TRUE;
          if (HasAbility(gameObject, nAbilityID) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_IsAbilityValid", "ERROR: Creature does not have ability: " + IntToString(nAbilityID), EngineConstants.LOG_SEVERITY_WARNING);
#endif
               nResult = EngineConstants.FALSE;
          }
          else if (Ability_CostCheck(gameObject, nAbilityID, nAbilityType) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_IsAbilityValid", "Not enough resources to trigger ability: " + IntToString(nAbilityID), EngineConstants.LOG_SEVERITY_WARNING);
#endif
               nResult = EngineConstants.FALSE;
          }
          else if (GetRemainingCooldown(gameObject, nAbilityID) > 0.0f)
          {
#if DEBUG
               Log_Trace_AI("_AI_IsAbilityValid", "Can't trigger ability (Cooldown running) - time left: " + FloatToString(GetRemainingCooldown(gameObject, nAbilityID)));
#endif
               nResult = EngineConstants.FALSE;
          }
          else if (nAbilityID == EngineConstants.ABILITY_TALENT_STEALTH && GetCombatState(gameObject) != EngineConstants.FALSE &&
              HasAbility(gameObject, EngineConstants.ABILITY_TALENT_COMBAT_STEALTH) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_IsAbilityValid", "Can't execute stealth in combat");
#endif
               nResult = EngineConstants.FALSE;
          }

          // Check special ability conditions for triggering tactic
          int nFollower_TrigOutComb = GetM2DAInt(EngineConstants.TABLE_AI_ABILITY_COND, "Follower_TrigOutComb", nAbilityID); ;
          int nTrigOutCombat = GetM2DAInt(EngineConstants.TABLE_AI_ABILITY_COND, "TrigOutComb", nAbilityID); ;
          if (nResult != EngineConstants.FALSE)
          {
               if (IsFollower(gameObject) != EngineConstants.FALSE && GetCombatState(gameObject) == EngineConstants.FALSE && nFollower_TrigOutComb == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_IsAbilityValid", "Ability not allowed to trigger outside combat for followers");
#endif
                    nResult = EngineConstants.FALSE;
               }
               else if (IsFollower(gameObject) == EngineConstants.FALSE && GetCombatState(gameObject) == EngineConstants.FALSE && nTrigOutCombat == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_IsAbilityValid", "Ability not allowed to trigger outside combat for non-followers");
#endif
                    nResult = EngineConstants.FALSE;
               }
          }

          // special case: stealth
          // Do not allow stealth if no ally is left alive or non stealthed
          if (nAbilityID == EngineConstants.ABILITY_TALENT_STEALTH)
          {
               List<GameObject> arAllies = _AI_GetAllies(-1, -1);
               int nSize = GetArraySize(arAllies);
               if (nSize == 0)
               {
#if DEBUG
                    Log_Trace_AI("_AI_IsAbilityValid", "Trying to use stealth with no allies around - aborting");
#endif
                    nResult = EngineConstants.FALSE;
               }
               else // some allies alive
               {
                    int i;
                    GameObject oCurrent;
                    for (i = 0; i < nSize; i++)
                    {
                         oCurrent = arAllies[i];
                         if (IsStealthy(oCurrent) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_IsAbilityValid", "Trying to use stealth while at least one ally is stealthy - aborting");
#endif
                              nResult = EngineConstants.FALSE;
                              break;
                         }
                    }
               }
          }

          // HACK START
          // Allow followers to trigger only specific abilities outside of combat
          //if(IsFollower(gameObject) && GetCombatState(gameObject) == EngineConstants.FALSE && GetAbilityType(nAbilityID) != EngineConstants.ABILITY_TYPE_ITEM)
          //{
          // only heal can work
          // joshua@23/01/08: I allowed for Modal Abilities as well.
          //    if(nAbilityID == 10104 || Ability_IsModalAbility(nAbilityID) )
          //        nResult = EngineConstants.TRUE;
          //    else
          //        nResult = EngineConstants.FALSE;
          //}
          // HACK END

          return nResult;
     }

     /* @brief Verifies that a creature can execute a specific xCommand
*
* The exact conditions depend on the xCommand itself, but they include resources check (mana/stamina) equipment check (weapons) etc'
*
* @param nCommand the main xCommand being checked
* @param nSubCommand the sub-xCommand being checked
* @returns EngineConstants.TRUE if the xCommand can be executed, EngineConstants.FALSE otherwise
* @author Yaron
*/
     public int _AI_IsCommandValid(int nCommand, int nSubCommand, int nTacticTargetType = -1)
     {
          int nResult = EngineConstants.TRUE;
          int nAbilityType;

          switch (nCommand)
          {
               case EngineConstants.AI_COMMAND_ATTACK:
                    {
                         if (GetEffectsFlags(gameObject) == EngineConstants.EFFECT_FLAG_DISABLE_COMBAT)
                              nResult = EngineConstants.FALSE;
                         // Otherwise, this can not fail. The attacker can always attack with fists.
                         // NOTE: this xCommand can still be valid if the attacker does not have enough ammo for a ranged weapon
                         // -> the 'attack' routine will try to switch a weapon set.

                         break;
                    }
               case EngineConstants.AI_COMMAND_ACTIVATE_MODE:
                    {
                         nResult = _AI_IsAbilityValid(nSubCommand);
                         if (nResult != EngineConstants.FALSE)
                         {
                              if (IsModalAbilityActive(gameObject, nSubCommand) != EngineConstants.FALSE)
                                   nResult = EngineConstants.FALSE; // Ability is already active - can't activate again
                         }
                         if (Ability_GetAbilityType(nSubCommand) == EngineConstants.ABILITY_TYPE_SPELL && (GetEffectsFlags(gameObject) == EngineConstants.EFFECT_FLAG_DISABLE_SPELLS))
                              nResult = EngineConstants.FALSE;
                         else if (Ability_GetAbilityType(nSubCommand) == EngineConstants.ABILITY_TYPE_TALENT && (GetEffectsFlags(gameObject) == EngineConstants.EFFECT_FLAG_DISABLE_TALENTS))
                              nResult = EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.AI_COMMAND_DEACTIVATE_MODE:
                    {
                         if (IsModalAbilityActive(gameObject, nSubCommand) == EngineConstants.FALSE)
                              nResult = EngineConstants.FALSE; // Ability is already inactive - can't deactivate again
                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_ABILITY:
                    {
                         if (Ability_IsModalAbility(nSubCommand) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_IsCommandValid", "ERROR: Trying to execute a modal ability as a normal ability: " + IntToString(nSubCommand), EngineConstants.LOG_SEVERITY_WARNING);
#endif
                              nResult = EngineConstants.FALSE;
                         }

                         nResult = _AI_IsAbilityValid(nSubCommand);

                         if (Ability_GetAbilityType(nSubCommand) == EngineConstants.ABILITY_TYPE_SPELL && (GetEffectsFlags(gameObject) == EngineConstants.EFFECT_FLAG_DISABLE_SPELLS))
                         {
#if DEBUG
                              Log_Trace_AI("_AI_IsCommandValid", "can't trigger spell - spells disabled for this creature: " + IntToString(nSubCommand), EngineConstants.LOG_SEVERITY_WARNING);
#endif
                              nResult = EngineConstants.FALSE;
                         }
                         else if (Ability_GetAbilityType(nSubCommand) == EngineConstants.ABILITY_TYPE_TALENT && (GetEffectsFlags(gameObject) == EngineConstants.EFFECT_FLAG_DISABLE_TALENTS))
                         {
#if DEBUG
                              Log_Trace_AI("_AI_IsCommandValid", "can't trigger talent - talents disabled for this creature: " + IntToString(nSubCommand), EngineConstants.LOG_SEVERITY_WARNING);
#endif
                              nResult = EngineConstants.FALSE;
                         }
                         else if (Ability_GetAbilityType(nSubCommand) == EngineConstants.ABILITY_TYPE_TALENT &&
                             GetHasEffects(gameObject, EngineConstants.EFFECT_TYPE_MISDIRECTION_HEX) != EngineConstants.FALSE &&
                             nTacticTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_IsCommandValid", "can't trigger talent - having misdirection hex for a hostile target talent: " + IntToString(nSubCommand), EngineConstants.LOG_SEVERITY_WARNING);
#endif
                              nResult = EngineConstants.FALSE;

                         }


                         // TEMP - staff hack
                         if (nSubCommand == 11130) // STAFF
                         {
                              if (GetCreatureCoreClass(gameObject) != EngineConstants.CLASS_WIZARD)
                                   nResult = EngineConstants.FALSE;
                         }
                         // END TEMP
                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_PLACEABLE:
                    {
                         // can only work if the creature is in combat
                         if (GetCombatState(gameObject) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_IsCommandValid", "Can't use placeable outside of combat");
#endif
                              nResult = EngineConstants.FALSE;
                         }
                         break;
                    }
               case EngineConstants.AI_COMMAND_USE_ITEM:
                    {
                         nResult = _AI_IsAbilityValid(nSubCommand);

                         if (GetEffectsFlags(gameObject) == EngineConstants.EFFECT_FLAG_DISABLE_ITEMS)
                              nResult = EngineConstants.FALSE;
                         else if (Ability_GetAbilityType(nSubCommand) != EngineConstants.ABILITY_TYPE_ITEM)
                              nResult = EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.AI_COMMAND_MOVE:
                    {
                         if (GetEffectsFlags(gameObject) == EngineConstants.EFFECT_FLAG_DISABLE_MOVEMENT)
                              nResult = EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.AI_COMMAND_SWITCH_TO_MELEE:
                    {
                         if (_AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_MELEE) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_IsCommandValid", "No melee weapon set for switch-to-melee action - aborting");
#endif
                              return EngineConstants.FALSE;
                         }
                         break;
                    }
               case EngineConstants.AI_COMMAND_SWITCH_TO_RANGED:
                    {
                         if (_AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_IsCommandValid", "No ranged weapon set for switch-to-melee action - aborting");
#endif
                              return EngineConstants.FALSE;
                         }
                         break;
                    }
          }
          return nResult;
     }

     public int _AI_GetWeaponSetEquipped(GameObject oTarget = null)
     {
          if (oTarget == null) oTarget = gameObject;
          int nRet = EngineConstants.AI_WEAPON_SET_INVALID;
          int nActiveWeaponSet = GetActiveWeaponSet(oTarget);
          GameObject oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oTarget, nActiveWeaponSet);

          if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_WAND)
               nRet = EngineConstants.AI_WEAPON_SET_RANGED;
          else if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_RANGED)
               nRet = EngineConstants.AI_WEAPON_SET_RANGED;
          else if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_MELEE)
               nRet = EngineConstants.AI_WEAPON_SET_MELEE;
          else
               nRet = EngineConstants.AI_WEAPON_SET_MELEE;

          //int nCurrentSet = GetActiveWeaponSet(gameObject);
          //if(nCurrentSet == Items_GetRangedWeaponSet(gameObject))
          //    nRet = EngineConstants.AI_WEAPON_SET_RANGED;
          //else if(nCurrentSet == Items_GetMeleeWeaponSet(gameObject))
          //    nRet = EngineConstants.AI_WEAPON_SET_MELEE;

          return nRet;
     }

     /* @brief Check if the specified target is within melee range
*
* @param oTarget the target we check melee range for
* @returns EngineConstants.TRUE if oTarget is within melee range, EngineConstants.FALSE otherwise
* @author Yaron
*/
     public int _AI_IsTargetInMeleeRange(GameObject oTarget)
     {
          int nApp = GetAppearanceType(oTarget);
          if (IsObjectValid(oTarget) == EngineConstants.FALSE)
               return EngineConstants.FALSE;

          float fMaxRange = EngineConstants.AI_MELEE_RANGE;
          float fPerSpace = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "PERSPACE", nApp);
          if (fPerSpace > 1.0 && fPerSpace <= 2.0f)
               fMaxRange += (fPerSpace / 2);
          else if (fPerSpace > 2.0f)
               fMaxRange = EngineConstants.AI_RANGE_MEDIUM;
          float fDistance = GetDistanceBetween(gameObject, oTarget);
#if DEBUG
          Log_Trace_AI("_AI_IsTargetInMeleeRange", "Target: " + GetTag(oTarget) + ", Distance: " + FloatToString(fDistance));
#endif

          if (fDistance <= fMaxRange)
               return EngineConstants.TRUE;

          return EngineConstants.FALSE;

          /*List<GameObject> arEnemiesInMelee = GetCreaturesInMeleeRing(gameObject, 0.0f, 360.0f, EngineConstants.TRUE);
          GameObject oCurrent;
          int nSize = GetArraySize(arEnemiesInMelee);
          int i;
          for(i = 0; i < nSize; i++)
          {
              oCurrent = arEnemiesInMelee[i];
              if(oCurrent == oTarget)
                  return EngineConstants.TRUE;
          }

          return EngineConstants.FALSE;*/
     }

     /* @brief Check if the current creature has a specific weapon set available (non-equipped)
*
* @param nWeaponSetType the weapon set we are looking for, assming it is the non-equipped weapon set
* @returns EngineConstants.TRUE if the current creature has the weapon set, EngineConstants.FALSE otherwise
* @author Yaron
*/
     public int _AI_HasWeaponSet(int nWeaponSetType)
     {
          int nRet = EngineConstants.FALSE;
          switch (nWeaponSetType)
          {
               case EngineConstants.AI_WEAPON_SET_MELEE:
                    {
                         return Items_GetMeleeWeaponSet(gameObject) != -1 ? EngineConstants.TRUE : EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.AI_WEAPON_SET_RANGED:
                    {
                         return Items_GetRangedWeaponSet(gameObject, EngineConstants.TRUE) != -1 ? EngineConstants.TRUE : EngineConstants.FALSE; // Also checks for ammo
                         break;
                    }
          }

          return -1;
     }

     /* @brief Switches the weapon set of the current creature to a weapon set of the specified type
*
* @param nWeaponSetType the weapon set we want to switch to
* @returns a xCommand to switch the weapons
* @author Yaron
*/
     public xCommand _AI_SwitchWeaponSet(int nWeaponSetType)
     {
#if DEBUG
          Log_Trace_AI("_AI_SwitchWeaponSet", "Switching weapons to set type: " + IntToString(nWeaponSetType));
#endif

          int nSet = -1;
          switch (nWeaponSetType)
          {
               case EngineConstants.AI_WEAPON_SET_MELEE:
                    {
                         nSet = Items_GetMeleeWeaponSet(gameObject);
                         break;
                    }
               case EngineConstants.AI_WEAPON_SET_RANGED:
                    {
                         nSet = Items_GetRangedWeaponSet(gameObject, EngineConstants.TRUE);
                         break;
                    }
          }
#if DEBUG
          Log_Trace_AI("_AI_SwitchWeaponSet", "Switching weapons to set: " + IntToString(nSet));
#endif

          xCommand cmd = new xCommand(EngineConstants.COMMAND_TYPE_INVALID);

#if DEBUG
          if (nSet == -1)
               Log_Trace_AI("_AI_SwitchWeaponSet", "ERROR: FAILED TO FIND A SET TO SWITCH TO");
#endif

          if (nSet != -1)
          {
               cmd = CommandSwitchWeaponSet(nSet);
          }

          return cmd;
     }

     /* @brief Gets the value of a specific AI flag on the current creature
*
* @param nFlag the flag we check for
* @returns EngineConstants.TRUE if the flag is set on the current creature, EngineConstants.FALSE otherwise
* @author Yaron
*/
     public int _AI_GetFlag(string sFlag, GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          int nValue = GetLocalInt(oCreature, sFlag);
          Log_Trace_AI("_AI_GetFlag", "flag: " + sFlag + ", value= " + IntToString(nValue));
          return nValue;
     }

     /* @brief Sets the value of a specific AI flag on the current creature
*
* @param sFlag the flag we check for
* @author Yaron
*/
     public void _AI_SetFlag(string sFlag, int nValue)
     {
#if DEBUG
          Log_Trace_AI("_AI_SetFlag", "flag: " + sFlag + ", value= " + IntToString(nValue));
#endif
          SetLocalInt(gameObject, sFlag, nValue);
     }

     public void _AI_ApplyTimerDifficultyEffects(GameObject oTarget)
     {
          if (GetGameDifficulty() > EngineConstants.GAME_DIFFICULTY_NORMAL)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteAttack", "timer failure - increasing movement speed");
#endif
               xEffect eSpeed = EffectModifyMovementSpeed(1.5f);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eSpeed, gameObject, EngineConstants.AI_COMMAND_TIMER / 2);
          }
     }

     /* @brief Executes an attack xCommand including possible weapon switching
*
* @param oTarget the target being attacked
* @param nLastCommandStatus used to determine what to do in case of failed movement or weapon switch
* @returns an attack or weapon switch xCommand
* @author Yaron
*/
     public xCommand _AI_ExecuteAttack(GameObject oTarget, int nLastCommandStatus)
     {
          // This can include a weapon switch condition as well:
          // If current creature equips a ranged weapon and the target is within melee range -> switch to melee
          // If current creature equips a melee weapon and the target is not within melee range AND
          // the creature prefers ranged weapons (flag) AND the ranged weapon set has enough ammo -> switch to ranged weapon
          // All of the conditions above assume the creature has the appropriate weapon sets
          // If the creature decides to switch weapons then we will NOT add another melee xCommand this round



          xCommand cTacticCommand = new xCommand(EngineConstants.COMMAND_TYPE_INVALID);
          int nTacticID = 0; // used to store the tactic that was executed, if it there is no tactic ID from a table
          int nLastTacticID = GetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC); // the last tactic being used
#if DEBUG
          Log_Trace_AI("_AI_ExecuteAttack", "*** START ***, Target: " + GetTag(oTarget) + ", Last Command Status: "
              + IntToString(nLastCommandStatus) + ", last tactic id: " + IntToString(nLastTacticID));
#endif

          float fDistance;
          int nRand;
          float fAvoidDistance;
          GameObject oCurrentWP;
          List<GameObject> arWPs;
          float fWPDistance;
          int nSize;
          int i;
          int bWPFound = EngineConstants.FALSE;
          List<GameObject> oCreaturesNearWP;
          float fNearestCreatureToWPDistance;
          List<GameObject> arPerceivedCreatures;
          GameObject oCreatureNearWP;
          GameObject oOldWP;
          int nFailMessage = 0;
          switch (nLastCommandStatus)
          {
               case EngineConstants.COMMAND_FAILED_COMMAND_CLEARED: nFailMessage = EngineConstants.UI_DEBUG_COMMAND_FAILED; break;
               case EngineConstants.COMMAND_FAILED_INVALID_DATA: nFailMessage = EngineConstants.UI_DEBUG_INVALID_DATA; break;
               case EngineConstants.COMMAND_FAILED_INVALID_PATH: nFailMessage = EngineConstants.UI_DEBUG_INVALID_PATH; break;
               case EngineConstants.COMMAND_FAILED_NO_LINE_OF_SIGHT: nFailMessage = EngineConstants.UI_DEBUG_NO_LOS; break;
               //case EngineConstants.COMMAND_FAILED_NO_SPACE_IN_MELEE_RING: nFailMessage = EngineConstants.UI_DEBUG_NO_SPACE_IN_MELEE_RING; break;
               case EngineConstants.COMMAND_FAILED_TARGET_DESTROYED: nFailMessage = EngineConstants.UI_DEBUG_TARGET_DESTROYED; break;
               case EngineConstants.COMMAND_FAILED_DISABLED: nFailMessage = EngineConstants.UI_DEBUG_MOVEMENT_DISABLED; break;
               case EngineConstants.COMMAND_FAILED_TIMEOUT: nFailMessage = EngineConstants.UI_DEBUG_COMMAND_TIMED_OUT; break;
          }
#if DEBUG
          if (nFailMessage > 0)
               UI_DisplayMessage(gameObject, nFailMessage);
#endif


          if (GetEffectsFlags(gameObject) == EngineConstants.EFFECT_FLAG_DISABLE_COMBAT)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteAttack", "Can't add any combat xCommands: combat is disabled by xEffect - waiting instead");
#endif
               cTacticCommand = _AI_DoNothing(nLastTacticID, nLastCommandStatus, EngineConstants.TRUE);
               nTacticID = EngineConstants.AI_TACTIC_ID_WAIT;
          }

          // Special handling:
          // If the last xCommand failed AND the last xCommand was a part of this generic 'attack' sequence
          // dec 17, 2008 -- yaron: adding a condition that allows this part to run if the xCommand before was not a normal attack and the failure is 'movement disable'
          // feb 24, 2009 -- yaron: same as above but also for timeoutfailures
          else if (nLastCommandStatus < 0 &&
                  (nLastTacticID < 0 || (nLastTacticID >= 0 && (nLastCommandStatus == EngineConstants.COMMAND_FAILED_DISABLED || nLastCommandStatus == EngineConstants.COMMAND_FAILED_TIMEOUT))) &&
                  nLastCommandStatus != EngineConstants.COMMAND_FAILED_TARGET_DESTROYED)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteAttack", "Last tactic failed AND was an Attack tactic - trying something else, error: " + IntToString(nLastCommandStatus));
#endif
               if (IsFollower(gameObject) != EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteAttack", "Command failed for follower - trying only to WAIT");
#endif
                    // Nothing much we can do in these cases except wait
                    cTacticCommand = _AI_DoNothing(nLastTacticID, nLastCommandStatus, EngineConstants.TRUE);
                    nTacticID = EngineConstants.AI_TACTIC_ID_WAIT;
               }
               else
               {
                    fDistance = GetDistanceBetween(gameObject, oTarget);

                    // timeout failure - special case
                    // (regardles of what the last action was)
                    if (nLastCommandStatus == EngineConstants.COMMAND_FAILED_TIMEOUT)
                    {
#if DEBUG
                         Log_Trace_AI("_AI_ExecuteAttack", "Handling attack timeout");
#endif
                         if (IsFollower(gameObject) == EngineConstants.FALSE &&
                             GetLocalInt(gameObject, EngineConstants.CREATURE_HAS_TIMER_ATTACK) == 1) // first failure -> try to attack again
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteAttack", "First timer failure - try to attack again");
#endif
                              SetLocalInt(gameObject, EngineConstants.CREATURE_HAS_TIMER_ATTACK, 2);
                              cTacticCommand = CommandAttack(oTarget);
                              nTacticID = EngineConstants.AI_TACTIC_ID_ATTACK;

                         }
                         else // not first failure -> switch weapon or try to attack again
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteAttack", "Not first timer failure - trying to switch to ranged");
#endif
                              if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_MELEE &&
                                  _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED) != EngineConstants.FALSE)
                              {
#if DEBUG
                                   Log_Trace_AI("_AI_ExecuteAttack", "switching to ranged because of timer failure");
#endif
                                   cTacticCommand = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED);
                                   nTacticID = EngineConstants.AI_TACTIC_ID_SWITCH_MELEE_TO_RANGED;
                              }
                              else // can't switch -> try to attack again (no need for wait as this is a timeout failure
                              {
#if DEBUG
                                   Log_Trace_AI("_AI_ExecuteAttack", "timer failure - can't switch - trying to attack again");
#endif
                                   _AI_ApplyTimerDifficultyEffects(oTarget);
                                   // adding speed boost for normal/hard difficulty
                                   cTacticCommand = CommandAttack(oTarget);
                                   nTacticID = EngineConstants.AI_TACTIC_ID_ATTACK;
                              }

                         }
                    }
                    else // not a timer failure
                    {

                         switch (nLastTacticID)
                         {
                              case EngineConstants.AI_TACTIC_ID_ATTACK:
                                   {
                                        // if a follower has the movement disabled GUI activated and he fails the last action
                                        if (nLastCommandStatus == EngineConstants.COMMAND_FAILED_DISABLED &&
                                                IsFollower(gameObject) != EngineConstants.FALSE &&
                                                _AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_MELEE &&
                                                _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED) != EngineConstants.FALSE &&
                                                _AI_IsTargetInMeleeRange(oTarget) == EngineConstants.FALSE)
                                        {
                                             cTacticCommand = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED);
                                             nTacticID = EngineConstants.AI_TACTIC_ID_SWITCH_MELEE_TO_RANGED;
                                        }
                                        else if (IsFollower(gameObject) == EngineConstants.FALSE &&
                                                _AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_MELEE &&
                                                _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED) != EngineConstants.FALSE && _AI_IsTargetInMeleeRange(oTarget) == EngineConstants.FALSE)
                                        {
                                             cTacticCommand = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED);
                                             nTacticID = EngineConstants.AI_TACTIC_ID_SWITCH_MELEE_TO_RANGED;
                                        }
                                        // if has melee but no ranged: move closer
                                        else if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_MELEE &&
                                                _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED) == EngineConstants.FALSE &&
                                                fDistance > (EngineConstants.AI_MINIMAL_MELEE_DISTANCE + 1.0f) &&
                                                nLastCommandStatus != EngineConstants.COMMAND_FAILED_DISABLED &&
                                                nLastCommandStatus != EngineConstants.COMMAND_FAILED_PATH_ACTION_REQUIRED &&
                                                nLastCommandStatus != EngineConstants.COMMAND_FAILED_INVALID_PATH)
                                        {
#if DEBUG
                                             Log_Trace_AI("_AI_ExecuteAttack", "Trying to move closer to target");
#endif
                                             cTacticCommand = CommandMoveToObject(oTarget, EngineConstants.TRUE, EngineConstants.AI_MINIMAL_MELEE_DISTANCE);
                                             nTacticID = EngineConstants.AI_TACTIC_ID_MOVE;
                                        }
                                        // if has ranged: change to melee
                                        else if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_RANGED &&
                                                _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_MELEE) != EngineConstants.FALSE && _AI_IsTargetInMeleeRange(oTarget) != EngineConstants.FALSE)
                                        {
                                             cTacticCommand = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_MELEE);
                                             nTacticID = EngineConstants.AI_TACTIC_ID_SWITCH_RANGED_TO_MELEE;
                                        }
                                        // if too close and not on ring, move a little ways away from target
                                        else if (nLastCommandStatus == EngineConstants.COMMAND_FAILED_NO_SPACE_IN_MELEE_RING &&
                                                  fDistance < EngineConstants.AI_MELEE_RANGE)
                                        {
#if DEBUG
                                             Log_Trace_AI("_AI_ExecuteAttack", "Trying to move away from target");
#endif
                                             cTacticCommand = CommandMoveAwayFromObject(oTarget, 2.0f, EngineConstants.FALSE);
                                             nTacticID = EngineConstants.AI_TACTIC_ID_MOVE;
                                        }
                                        else
                                        {
                                             cTacticCommand = _AI_DoNothing(nLastTacticID, nLastCommandStatus, EngineConstants.TRUE, EngineConstants.FALSE, oTarget);
                                             nTacticID = EngineConstants.AI_TACTIC_ID_WAIT;
                                        }
                                        break;
                                   }
                              case EngineConstants.AI_TACTIC_ID_SWITCH_MELEE_TO_RANGED:
                              case EngineConstants.AI_TACTIC_ID_SWITCH_RANGED_TO_MELEE:
                              case EngineConstants.AI_TACTIC_ID_WAIT:
                              case EngineConstants.AI_TACTIC_ID_MOVE:
                              default:
                                   {
                                        // Nothing much we can do in these cases except wait
                                        cTacticCommand = _AI_DoNothing(nLastTacticID, nLastCommandStatus, EngineConstants.TRUE, EngineConstants.FALSE, oTarget);
                                        nTacticID = EngineConstants.AI_TACTIC_ID_WAIT;
                                        break;
                                   }
                         } // end of tactic ID failure switch
                    } // end of else (not a timer failure)
               } // end of 'not follower' if-else
          }
          else if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_RANGED &&
              (_AI_IsTargetInMeleeRange(oTarget) != EngineConstants.FALSE || _AI_GetFlag(EngineConstants.AI_FLAG_PREFERS_RANGED) == EngineConstants.FALSE) &&
              _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_MELEE) != EngineConstants.FALSE &&
              nLastTacticID != EngineConstants.AI_TACTIC_ID_SWITCH_MELEE_TO_RANGED) // so it won't try to switch to melee right after changing to ranged
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteAttack", "Trying to switch into a melee weapon set");
#endif

               if (IsFollower(gameObject) != EngineConstants.FALSE && AI_BehaviorCheck_PreferRange() != EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteAttack", "Follower prefers range - switch to melee aborted");
#endif
               }
               else
               {
                    int nRandBackToMelee = Engine_Random(100) + 1;
                    if (IsFollower(gameObject) == EngineConstants.FALSE && _AI_GetFlag(EngineConstants.AI_FLAG_PREFERS_RANGED) == EngineConstants.FALSE && nRandBackToMelee > 33)
                    {
#if DEBUG
                         Log_Trace_AI("_AI_ExecuteAttack", "Not prefering range, but random chance failed to allow changing back to melee");
#endif
                    }
                    else if (_AI_IsTargetInMeleeRange(oTarget) != EngineConstants.FALSE)
                    {
                         cTacticCommand = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_MELEE);
                         nTacticID = EngineConstants.AI_TACTIC_ID_SWITCH_RANGED_TO_MELEE;

                         if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID) // failed to switch
                         {
                              cTacticCommand = CommandAttack(oTarget); // Continue attacking with ranged weapon
                              nTacticID = EngineConstants.AI_TACTIC_ID_ATTACK;
                         }
                    }
               }
          }
          else if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_MELEE &&
                 _AI_IsTargetInMeleeRange(oTarget) == EngineConstants.FALSE &&
                  (IsFollower(gameObject) != EngineConstants.FALSE || _AI_GetFlag(EngineConstants.AI_FLAG_PREFERS_RANGED) != EngineConstants.FALSE) &&
                  _AI_HasWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED) != EngineConstants.FALSE &&     // Ranged weapon set check includes ammo check
                  nLastTacticID != EngineConstants.AI_TACTIC_ID_SWITCH_RANGED_TO_MELEE) // Did not try
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteAttack", "Trying to switch into a ranged weapon set");
#endif
               if (IsFollower(gameObject) != EngineConstants.FALSE && AI_BehaviorCheck_PreferMelee() != EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteAttack", "Follower prefers range - switch to range aborted");
#endif
               }
               else if (IsFollower(gameObject) != EngineConstants.FALSE && AI_BehaviorCheck_PreferRange() == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_AI("_AI_ExecuteAttack", "Follower does NOT prefer range - switch to range aborted");
#endif
               }
               else
               {
                    cTacticCommand = _AI_SwitchWeaponSet(EngineConstants.AI_WEAPON_SET_RANGED);
                    //cTacticCommand = _AI_SwitchWeaponSet(0);
                    nTacticID = EngineConstants.AI_TACTIC_ID_SWITCH_MELEE_TO_RANGED;

                    if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID) // failed to switch
                    {
                         cTacticCommand = CommandAttack(oTarget); // Continue attacking with melee weapon
                         nTacticID = EngineConstants.AI_TACTIC_ID_ATTACK;
                    }
               }

          }
          else// Did not switch any weapon set -> continue attacking with current.
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteAttack", "NORMAL ATTACK");
#endif

               cTacticCommand = CommandAttack(oTarget);
               nTacticID = EngineConstants.AI_TACTIC_ID_ATTACK;
          }
          // ...one last ammo check!


          // One last check - in case we failed to switch a weapon:
          if (GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_INVALID)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteAttack", "FAILED TO FIND A VALID COMMAND - TRYING COMMAND ATTACK", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
               cTacticCommand = CommandAttack(oTarget);
               nTacticID = EngineConstants.AI_TACTIC_ID_ATTACK;
          }

          // Last check - making sure selected attack matches stationary flag
          if (IsFollower(gameObject) == EngineConstants.FALSE && GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY) > 0 && GetCommandType(cTacticCommand) == EngineConstants.COMMAND_TYPE_ATTACK)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteAttack", "Creature stationary - checking if he can execute seleted attack");
#endif
               if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_MELEE)
               {
                    // if melee weapon -> attack only if target is in stationaty range
                    fDistance = GetDistanceBetween(gameObject, oTarget);
                    if (fDistance > EngineConstants.AI_STATIONARY_RANGE)
                    {
#if DEBUG
                         Log_Trace_AI("_AI_ExecuteAttack", "Creature stationary - too far from melee target to execute attack - WAITING");
#endif
                         cTacticCommand = _AI_DoNothing(nLastTacticID, nLastCommandStatus, EngineConstants.TRUE, EngineConstants.TRUE);
                         nTacticID = EngineConstants.AI_TACTIC_ID_WAIT;
                    }
               }
               else if (_AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_RANGED)
               {
                    // if ranged weapon -> attack only if target is in weapon range
                    GameObject oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN);
                    fDistance = GetDistanceBetween(gameObject, oTarget);
                    float fWeaponRange = GetItemRange(oWeapon);

#if DEBUG
                    Log_Trace_AI("_AI_ExecuteAttack", "Weapon range check for stationary creature - weapon range: " + FloatToString(fWeaponRange));
#endif
                    if (fDistance > fWeaponRange)
                    {
#if DEBUG
                         Log_Trace_AI("_AI_ExecuteAttack", "Creature stationary - too far from ranged target to execute attack - WAITING");
#endif
                         cTacticCommand = _AI_DoNothing(nLastTacticID, nLastCommandStatus, EngineConstants.TRUE, EngineConstants.TRUE);
                         nTacticID = EngineConstants.AI_TACTIC_ID_WAIT;
                    }
                    else // creature within range - check line of sight
                    {
                         if (CheckLineOfSightObject(gameObject, oTarget) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_ExecuteAttack", "Creature stationary - no line of sight to target - WAITING");
#endif
                              cTacticCommand = _AI_DoNothing(nLastTacticID, nLastCommandStatus, EngineConstants.TRUE, EngineConstants.TRUE);
                              nTacticID = EngineConstants.AI_TACTIC_ID_WAIT;
                         }
                    }

               }
          }

#if DEBUG
          Log_Trace_AI("_AI_ExecuteAttack", "Setting last tactic ID to: " + IntToString(nTacticID));
#endif
          SetLocalInt(gameObject, EngineConstants.AI_LAST_TACTIC, nTacticID);
          return cTacticCommand;
     }

     public string _AI_GetCommandString(int nAICommand)
     {
          string sRet = "INVALID";

          switch (nAICommand)
          {
               case EngineConstants.AI_COMMAND_ACTIVATE_MODE: sRet = "***** ACTIVATE MODE *****"; break;
               case EngineConstants.AI_COMMAND_ATTACK: sRet = "***** ATTACK *****"; break;
               case EngineConstants.AI_COMMAND_DEACTIVATE_MODE: sRet = "***** DEACTIVATE MODE *****"; break;
               case EngineConstants.AI_COMMAND_USE_ABILITY: sRet = "USE ABILITY"; break;
               case EngineConstants.AI_COMMAND_USE_ITEM: sRet = "***** USE ITEM *****"; break;
               case EngineConstants.AI_COMMAND_USE_PLACEABLE: sRet = "***** USE PLACEABLE *****"; break;
               case EngineConstants.AI_COMMAND_WAIT: sRet = "***** WAIT *****"; break;
               case EngineConstants.AI_COMMAND_MOVE: sRet = "***** MOVE *****"; break;
               case EngineConstants.AI_COMMAND_JUMP_TO_LATER_TACTIC: sRet = "***** JUMP TO LATER TACTIC *****"; break;
               case EngineConstants.AI_COMMAND_SWITCH_TO_MELEE: sRet = "***** SWTICH TO MELEE *****"; break;
               case EngineConstants.AI_COMMAND_SWITCH_TO_RANGED: sRet = "***** SWITCH TO RANGED *****"; break;
               case EngineConstants.AI_COMMAND_FLY: sRet = "***** FLY *****"; break;
               case EngineConstants.AI_COMMAND_USE_HEALTH_POTION_LEAST: sRet = "**** USE LEAST POWERFUL HEALING POTION"; break;
               case EngineConstants.AI_COMMAND_USE_HEALTH_POTION_MOST: sRet = "**** USE MOST POWERFUL HEALING POTION"; break;
               case EngineConstants.AI_COMMAND_USE_LYRIUM_POTION_LEAST: sRet = "**** USE LEAST POWERFUL MANA POTION"; break;
               case EngineConstants.AI_COMMAND_USE_LYRIUM_POTION_MOST: sRet = "**** USE MOST POWERFUL MANA POTION"; break;
          }
          return sRet;
     }

     // checks for AI-specifics conditions for using this ability on the required target
     public int _AI_CanUseAbility(int nAbility, GameObject oTarget)
     {
          int nRet = EngineConstants.TRUE;
          float fDistance;

          switch (nAbility)
          {
               case EngineConstants.ABILITY_SPELL_CHAIN_LIGHTNING:
                    {
                         // for followers - valid only if there are at least 3+ enemies
                         if (IsFollower(gameObject) != EngineConstants.FALSE)
                         {
                              List<GameObject> arEnemies = GetNearestObjectByGroup(gameObject, EngineConstants.GROUP_HOSTILE, EngineConstants.OBJECT_TYPE_CREATURE, 3, EngineConstants.TRUE, EngineConstants.TRUE, EngineConstants.FALSE);
                              int nSize = GetArraySize(arEnemies);
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "number of hostiles found: " + IntToString(nSize));
#endif
                              if (nSize < 3)
                                   nRet = EngineConstants.FALSE;
                         }
                         break;
                    }
               case EngineConstants.ABILITY_TALENT_SHIELD_BASH:
               case EngineConstants.ABILITY_TALENT_OVERPOWER:
               case EngineConstants.ABILITY_TALENT_OVERRUN: // pommel strike
               case EngineConstants.ABILITY_TALENT_MONSTER_DOG_CHARGE:
               case EngineConstants.ABILITY_TALENT_PINNING_SHOT:
               case EngineConstants.ABILITY_TALENT_FRIGHTENING:
               case EngineConstants.ABILITY_TALENT_DIRTY_FIGHTING:
                    {
                         if (_AI_HasAIStatus(oTarget, EngineConstants.AI_STATUS_CANT_ATTACK) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "target already can't fight - aborting use of stun/knockdown ability");
#endif
                              nRet = EngineConstants.FALSE;
                         }
                         break;
                    }
               case EngineConstants.ABILITY_TALENT_HOLY_SMITE:
               case EngineConstants.ABILITY_SPELL_WYNNES_SEAL_PORTAL: // mana drain
               case EngineConstants.ABILITY_SPELL_MANA_CLASH:
                    {
                         // only on mages and applies only for non-followers (followers have the 'nearest by class' to filter)
                         if (IsFollower(gameObject) == EngineConstants.FALSE)
                         {
                              int nClass = GetCreatureCoreClass(oTarget);
                              if (nClass != EngineConstants.CLASS_WIZARD)
                              {
#if DEBUG
                                   Log_Trace_AI("_AI_CanUseAbility", "mage-only spell targetted at non mages");
#endif
                                   nRet = EngineConstants.FALSE;
                              }
                         }
                         break;
                    }
               case EngineConstants.MONSTER_HIGH_DRAGON_FIRE_SPIT:
               case EngineConstants.ARCHDEMON_DETONATE_DARKSPAWN:
               case EngineConstants.ARCHDEMON_CORRUPTION_BLAST:
                    {
                         float fAngle = GetAngleBetweenObjects(gameObject, oTarget);
                         if (fAngle > 60.0 && fAngle < 300.0f)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "Too large angle to trigger ability");
#endif
                              nRet = EngineConstants.FALSE;
                         }
                         break;
                    }
               case EngineConstants.ARCHDEMON_VORTEX:
               case EngineConstants.ARCHDEMON_SMITE:
                    {
                         fDistance = GetDistanceBetween(gameObject, oTarget);
                         if (fDistance < EngineConstants.AI_RANGE_MEDIUM)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "Archdemon abilities can't trigger too close");
#endif
                              nRet = EngineConstants.FALSE;

                         }

                         break;
                    }
               case EngineConstants.ABILITY_SPELL_WALKING_BOMB:
                    {
                         // can't use if virulent is on
                         if (Ability_IsAbilityActive(oTarget, 12011) != EngineConstants.FALSE)
                              nRet = EngineConstants.FALSE;
                         break;
                    }
               case 12011: // virulent walking bomb
                    {
                         // can't use if virulent is on
                         if (Ability_IsAbilityActive(oTarget, EngineConstants.ABILITY_SPELL_WALKING_BOMB) != EngineConstants.FALSE)
                              nRet = EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.ABILITY_TALENT_MONSTER_DOG_OVERWHELM:
               case EngineConstants.ABILITY_TALENT_MONSTER_SHRIEK_OVERWHLEM:
               case EngineConstants.MONSTER_BEAR_OVERWHELM:
               case EngineConstants.MONSTER_SPIDER_OVERWHELM:
               case EngineConstants.MONSTER_STALKER_OVERWHLEM:
               case EngineConstants.MONSTER_DRAGON_OVERWHELM:
                    {
                         if (IsHumanoid(oTarget) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "Overwhelm target not humanoid - can't execute ability");
#endif
                              nRet = EngineConstants.FALSE;
                         }
                         else if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_OVERWHELMED) != EngineConstants.FALSE || GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_GRABBED) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "Tareget is already being overwhelmed or grabbed - can't execute another overwhelm");
#endif
                              nRet = EngineConstants.FALSE;
                         }

                         break;
                    }
               case EngineConstants.ABILITY_TALENT_MONSTER_OGRE_GRAB:
               case EngineConstants.ABILITY_TALENT_BROODMOTHER_GRAB_LEFT:
               case EngineConstants.ABILITY_TALENT_BROODMOTHER_GRAB_RIGHT:
               case EngineConstants.MONSTER_HIGH_DRAGON_GRAB_LEFT:
               case EngineConstants.MONSTER_HIGH_DRAGON_GRAB_RIGHT:
                    {

                         if (IsHumanoid(oTarget) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "Grabbed target not humanoid - can't execute ability");
#endif
                              nRet = EngineConstants.FALSE;
                         }
                         else if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_OVERWHELMED) != EngineConstants.FALSE || GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_GRABBED) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "Tareget is already being overwhelmed or grabbed - can't execute another grab");
#endif
                              nRet = EngineConstants.FALSE;
                         }

                         // Archdemon only: not trigger it on party members
                         if (nAbility == EngineConstants.MONSTER_HIGH_DRAGON_GRAB_LEFT || nAbility == EngineConstants.MONSTER_HIGH_DRAGON_GRAB_RIGHT)
                         {
                              if (GetAppearanceType(gameObject) == EngineConstants.APP_TYPE_ARCHDEMON && IsFollower(oTarget) != EngineConstants.FALSE)
                              {
#if DEBUG
                                   Log_Trace_AI("_AI_CanUseAbility", "Archdemon can't grab party members");
#endif
                                   nRet = EngineConstants.FALSE;
                              }
                         }
                         break;
                    }
               case EngineConstants.ABILITY_SPELL_MONSTER_OGRE_HURL:
                    {
                         // Ogre Hurl ability can be used only with a minimum distance
                         fDistance = GetDistanceBetween(gameObject, oTarget);
                         if (fDistance < (EngineConstants.AI_RANGE_SHORT * 2))
                              nRet = EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.ABILITY_TALENT_SHIELD_DEFENSE:
                    {
                         // Do not run it if I have Shield Wall
                         if (HasAbility(gameObject, EngineConstants.ABILITY_TALENT_SHIELD_WALL) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "I have a better ability (Shield Wall) - not trying to run this one");
#endif
                              nRet = EngineConstants.FALSE;
                         }
                         break;
                    }
               case 200010: // healing potions
               case 200011:
               case 200012:
               case 200013:
                    {
                         float fCurrentStat = GetCurrentHealth(gameObject);
                         float fMaxStat = GetMaxHealth(gameObject);
                         if (fCurrentStat == fMaxStat)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "full health - not using health potion");
#endif
                              nRet = EngineConstants.FALSE;
                         }
                         break;
                    }
               case 200030: // mana potions
               case 200031:
               case 200032:
               case 200033:
                    {
                         float fCurrentStat = GetCurrentManaStamina(gameObject);
                         float fMaxStat = IntToFloat(GetCreatureMaxMana(gameObject));
                         if (fCurrentStat == fMaxStat)
                         {
#if DEBUG
                              Log_Trace_AI("_AI_CanUseAbility", "full mana - not using mana potion");
#endif
                              nRet = EngineConstants.FALSE;
                         }
                         break;
                    }

          }

          // Stationary non-follower check
          if (IsFollower(gameObject) == EngineConstants.FALSE && GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY) > 0 && oTarget != gameObject)
          {
               fDistance = GetDistanceBetween(gameObject, oTarget);
               int nAbilityRangeID = GetM2DAInt(EngineConstants.TABLE_ABILITIES_TALENTS, "range", nAbility);
               float fAbilityRange = GetM2DAFloat(EngineConstants.TABLE_RANGES, "PrimaryRange", nAbilityRangeID);

#if DEBUG
               Log_Trace_AI("_AI_CanUseAbility", "Stationary creature ability range: " + FloatToString(fAbilityRange) + ", distance to target: " +
                   FloatToString(fDistance));
#endif

               if (fDistance > fAbilityRange)
               {
#if DEBUG
                    Log_Trace_AI("_AI_CanUseAbility", "Stationary creature: target too far for ability range");
#endif
                    nRet = EngineConstants.FALSE;
               }
               else
               {
                    if (CheckLineOfSightObject(gameObject, oTarget) == EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace_AI("_AI_CanUseAbility", "Stationary creature: no line of sight for target");
#endif
                         nRet = EngineConstants.FALSE;
                    }
                    else
                    {
#if DEBUG
                         Log_Trace_AI("_AI_CanUseAbility", "Stationary creature: Clear to execute ability without moving");
#endif
                    }
               }
          }

          return nRet;
     }

     // Returns the range of the currently equipped ranged weapon
     public float _AI_GetEquippedWeaponRange()
     {
          GameObject oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN);
          float fRange = GetItemRange(oWeapon);
#if DEBUG
          Log_Trace_AI("_AI_GetEquippedWeaponRange", "Weapon: " + GetTag(oWeapon) + ", Weapon Range: " + FloatToString(fRange));
#endif

          return fRange;
     }

     // Returns EngineConstants.TRUE if GUI tables should be used, EngineConstants.FALSE if 2da tables should be used
     public int _AI_UseGUITables()
     {
          int nUseGUI = GetLocalInt(GetModule(), EngineConstants.AI_USE_GUI_TABLES_FOR_FOLLOWERS);
          if (nUseGUI == EngineConstants.FALSE)
               return EngineConstants.FALSE;

          return (GetFollowerState(gameObject) != EngineConstants.FOLLOWER_STATE_INVALID && _AI_HasAIStatus(gameObject, EngineConstants.AI_STATUS_POLYMORPH) == EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     // returns a xCommand to wait or play a taunt animation
     public xCommand _AI_DoNothing(int nLastTacticID, int nLastCommandStatus, int nAllowTaunts, int bQuick = EngineConstants.FALSE, GameObject oTarget = null, int nClearThreat = EngineConstants.TRUE)
     {
#if DEBUG
          Log_Trace_AI("_AI_DoNothing", "START");
#endif

          int nRand;
          int nRand2;
          xCommand cRet = new xCommand(EngineConstants.COMMAND_TYPE_INVALID);
          int nAnim = 0;

          if (IsFollower(gameObject) != EngineConstants.FALSE
              || _AI_HasAIStatus(gameObject, EngineConstants.AI_STATUS_PARALYZE) != EngineConstants.FALSE
              || _AI_HasAIStatus(gameObject, EngineConstants.AI_STATUS_DAZE) != EngineConstants.FALSE
              || _AI_HasAIStatus(gameObject, EngineConstants.AI_STATUS_STUN) != EngineConstants.FALSE)
               nAllowTaunts = EngineConstants.FALSE;

          nRand2 = Engine_Random(3) + 1;

          if (IsControlled(gameObject) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_DoNothing", "Controled follower - NOT WAITING");
#endif
               return cRet;
          }

          if (nClearThreat != EngineConstants.FALSE && oTarget != null && IsFollower(gameObject) == EngineConstants.FALSE)
          {
               // Lowering threat to oTarget
               AI_Threat_UpdateCantAttackTarget(gameObject, oTarget);
          }

          // If last xCommand was DoNothing and it failed (possibly by trying to play a non-existing taunt animation)
          if (nLastTacticID == EngineConstants.AI_TACTIC_ID_WAIT && nLastCommandStatus < 0)
          {
               if (bQuick != EngineConstants.FALSE)
                    cRet = CommandWait(EngineConstants.AI_DO_NOTHING_DELAY_QUICK);
               else
                    cRet = CommandWait(EngineConstants.AI_DO_NOTHING_DELAY);
#if DEBUG
               Log_Trace_AI("_AI_DoNothing", "Last DoNothing xCommand failed - trying to wait");
#endif
          }
          else if (nAllowTaunts != EngineConstants.FALSE)
          {
               nRand = Engine_Random(100) + 1;
               if (nRand <= EngineConstants.AI_TAUNT_CHANCE && GetAppearanceType(gameObject) != EngineConstants.APR_TYPE_OGRE)
               {
                    if (nRand2 == 1) nAnim = 144;
                    else if (nRand2 == 2) nAnim = 2005;
                    else if (nRand2 == 3) nAnim = 149;
                    cRet = CommandPlayAnimation(nAnim);
#if DEBUG
                    Log_Trace_AI("_AI_DoNothing", "Playing taunt animation");
#endif

               }
               if (GetCommandType(cRet) == EngineConstants.COMMAND_TYPE_INVALID)
               {
#if DEBUG
                    Log_Trace_AI("_AI_DoNothing", "failed to add taunt animation - waiting instead");
#endif
                    if (bQuick != EngineConstants.FALSE)
                         cRet = CommandWait(EngineConstants.AI_DO_NOTHING_DELAY_QUICK);
                    else
                         cRet = CommandWait(EngineConstants.AI_DO_NOTHING_DELAY);
               }
          }
          else
          {
               if (bQuick != EngineConstants.FALSE)
                    cRet = CommandWait(EngineConstants.AI_DO_NOTHING_DELAY_QUICK);
               else
                    cRet = CommandWait(EngineConstants.AI_DO_NOTHING_DELAY);
#if DEBUG
               Log_Trace_AI("_AI_DoNothing", "Waiting");
#endif
          }
          return cRet;
     }

     // Loads a table currently assigned to this creature into his GUI table
     public void _AI_LoadTacticsIntoGUI()
     {
          int nTable = GetPackageAI(gameObject);
          int nRows = GetM2DARows(nTable);
#if DEBUG
          Log_Trace_AI("_AI_LoadTacticsIntoGUI", "START - number of rows to load: " + IntToString(nRows));
#endif
          int i;
          int nTargetType;
          int nCondition;
          int nCommandType;
          int nCommandParam;
          for (i = 1; i <= nRows; i++)
          {
               nTargetType = GetM2DAInt(nTable, "TargetType", i);
               nCondition = GetM2DAInt(nTable, "Condition", i);
               nCommandType = GetM2DAInt(nTable, "Command", i);
               nCommandParam = GetM2DAInt(nTable, "SubCommand", i);
#if DEBUG
               Log_Trace_AI("_AI_LoadTacticsIntoGUI", "Loading entry: [" + IntToString(i) + "], target type: " +
                   IntToString(nTargetType) + ", Condition: " + IntToString(nCondition) + ", Command Type: " +
                   IntToString(nCommandType) + ", nCommand Param: " + IntToString(nCommandParam));
#endif
               SetTacticEntry(gameObject, i, EngineConstants.TRUE, nTargetType, nCondition, nCommandType, nCommandParam);
          }
     }

     // Move to main controlled follower in formation
     public xCommand _AI_MoveToControlled(int nLastCommandStatus)
     {
          xCommand cTacticCommand;
          GameObject oMainControlled = GetMainControlled();
          Vector3 lLoc = GetFollowerWouldBeLocation(gameObject);
          float fDistance = GetDistanceBetween(gameObject, oMainControlled);
          // NOTE: there used to be a distance check but I removed it since it cause followers to linger behind
          // when combat starts
          // yaron nov 28, 2008
          //-----
          // NOTE II: putting the distance check back, now with a very short distance
          // Without a distance check the move xCommands are eveluated constantly, flooding the AI.
          // This is just to slow it down a bit
          // yaron dec 8, 2008
          if (IsStealthy(oMainControlled) == EngineConstants.FALSE && nLastCommandStatus == EngineConstants.COMMAND_SUCCESSFUL && fDistance > EngineConstants.AI_RANGE_SHORT)
          {
               if (AI_BehaviorCheck_AvoidNearbyEnemies() != EngineConstants.FALSE)
               {
                    // move to player only if no enemies nearby
                    List<GameObject> arEnemies = GetNearestObjectByHostility(oMainControlled, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_CREATURE, 1);
                    fDistance = GetDistanceBetween(oMainControlled, arEnemies[0]);
                    if (fDistance <= EngineConstants.AI_RANGE_SHORT)
                         cTacticCommand = _AI_DoNothing(-1, nLastCommandStatus, EngineConstants.FALSE, EngineConstants.TRUE);
                    else
                         cTacticCommand = CommandMoveToLocation(lLoc, EngineConstants.TRUE);
               }
               else
                    cTacticCommand = CommandMoveToLocation(lLoc, EngineConstants.TRUE);
          }
          else
               cTacticCommand = _AI_DoNothing(-1, nLastCommandStatus, EngineConstants.FALSE, EngineConstants.TRUE);

          return cTacticCommand;
     }

     public int AI_GetPartyAllowedToAttack()
     {
          int nAllowed = GetLocalInt(GetModule(), EngineConstants.AI_PARTY_CLEAR_TO_ATTACK);
#if DEBUG
          Log_Trace_AI("AI_GetPartyAllowedToAttack", "Party allowed-to-attack status: " + IntToString(nAllowed));
#endif
          return nAllowed;
     }

     public void AI_SetPartyAllowedToAttack(int nStatus)
     {
#if DEBUG
          Log_Trace_AI("AI_SetPartyAllowedToAttack", "Setting party allowed-to-attack status to: " + IntToString(nStatus));
#endif
          SetLocalInt(GetModule(), EngineConstants.AI_PARTY_CLEAR_TO_ATTACK, nStatus);
          // giving a wait xCommand if the follower is doing nothing (to enable combat xCommands again)
          List<GameObject> arParty = GetPartyList();
          int nSize = GetArraySize(arParty);
          int i;
          GameObject oCurrent;
          xCommand cCurrentCommand;
          xCommand cWait = CommandWait(EngineConstants.AI_DO_NOTHING_DELAY_QUICK);
          for (i = 0; i < nSize; i++)
          {
               oCurrent = arParty[i];
               cCurrentCommand = GetCurrentCommand(oCurrent);
               if (oCurrent != gameObject && GetCommandQueueSize(oCurrent) == 0 && 
                GetCommandType(cCurrentCommand) == EngineConstants.COMMAND_TYPE_INVALID)
                    WR_AddCommand(oCurrent, cWait);

          }

     }

     // returns EngineConstants.TRUE if cooldown is clear to run again, EngineConstants.FALSE otherwise
     public int _AI_CheckMoveTimer()
     {
          int nMoveStart = GetLocalInt(gameObject, EngineConstants.AI_MOVE_TIMER);
          int nCurrentTime = GetTime();
#if DEBUG
          Log_Trace_AI("_AI_CheckMoveTimer", "current time: " + IntToString(nCurrentTime) + ", move start: " + IntToString(nMoveStart));
          Log_Trace_AI("_AI_CheckMoveTimer", "Move/Turn time dif: " + IntToString(nCurrentTime - nMoveStart));
#endif
          if (nMoveStart != 0 && nCurrentTime - nMoveStart <= EngineConstants.AI_MOVE_MIN_TIME)
          {
#if DEBUG
               Log_Trace_AI("_AI_ExecuteTactic", "Last move/turn happened too soon");
#endif
               return EngineConstants.FALSE;
          }
          return EngineConstants.TRUE;
     }

     public void _AI_SetMoveTimer()
     {
          SetLocalInt(gameObject, EngineConstants.AI_MOVE_TIMER, GetTime());
          Log_Trace_AI("_AI_CheckMoveTimer", "Set move timer to: " + IntToString(GetTime()));
     }

     public xCommand _AI_GetFlyCommand(GameObject oTurnTo, int bMoveTo = EngineConstants.FALSE)
     {
          float fAngle = GetAngleBetweenObjects(gameObject, oTurnTo);
          float fMyFacing = GetFacing(gameObject);
          float fEnemyFacing = GetFacing(oTurnTo);
          float fTurnAngle = 360.0f - fAngle + fMyFacing;
          if (fTurnAngle >= 180.0f)
               fTurnAngle = fMyFacing - fAngle;
          float fDif;
          if (fTurnAngle > fMyFacing) fDif = fTurnAngle - fMyFacing;
          else fDif = fMyFacing - fTurnAngle;
          float fDistance = GetDistanceBetween(gameObject, oTurnTo);
          xCommand cFly = new xCommand(EngineConstants.COMMAND_TYPE_INVALID);
#if DEBUG
          Log_Trace_AI("_AI_GetFlyCommand", "Object: " + GetTag(oTurnTo) + ", turn angle: " + FloatToString(fTurnAngle) + ", distance: " + FloatToString(fDistance)
              + ", angle dif: " + FloatToString(fDif));
#endif


          if (fDif < EngineConstants.AI_TURN_MIN_ANGLE && bMoveTo == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_GetFlyCommand", "Too small angle to turn - avoiding turn and returning invalid xCommand");
#endif
               return cFly;
          }
          else if (fDistance < EngineConstants.AI_FLY_MIN_DISTANCE && bMoveTo != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_AI("_AI_GetFlyCommand", "Too small distance to fly - avoiding fly and returning invalid xCommand. Distance: " + FloatToString(fDistance));
#endif
               return cFly;
          }


          Vector3 lLoc;
          if (bMoveTo != EngineConstants.FALSE)
          {
               if (fDistance > EngineConstants.AI_FLY_MAX_DISTANCE &&
                   GetLocalInt(gameObject, EngineConstants.CREATURE_COUNTER_3) == 0) // EngineConstants.CREATURE_COUNTER_3 used to enable/disable stomp. 1 is for disabled
               {
                    WR_ClearAllCommands(gameObject, EngineConstants.TRUE);
                    WR_SetObjectActive(gameObject, EngineConstants.FALSE);
                    xEvent eFlyDown = Event(EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE);
                    float fFacing = GetFacing(oTurnTo);
                    SetEventFloatRef(ref eFlyDown, 0, fFacing);
                    SetEventVectorRef(ref eFlyDown, 0, GetLocation(oTurnTo));
                    SetEventIntegerRef(ref eFlyDown, 3, EngineConstants.TRUE); // tells it to call an AI function
                    DelayEvent(2.5f, gameObject, eFlyDown);

                    // putting wait xCommand as a flag to abort AI
                    cFly = CommandWait(2.5f);

               }
               else if (fDistance < EngineConstants.AI_FLY_MAX_DISTANCE)
               {
                    lLoc = Location(GetArea(gameObject), GetPosition(oTurnTo), fTurnAngle);
                    cFly = CommandFly(lLoc);
               }
          }
          else if (fDistance > EngineConstants.AI_TURN_MIN_DISTANCE)
          {
               lLoc = Location(GetArea(gameObject), GetPosition(gameObject), fTurnAngle);
               cFly = CommandFly(lLoc);
          }



          return cFly;
     }

     public void AI_ExecuteAppearStomp(xEvent ev)
     {

          Vector3 lLoc = GetEventVectorRef(ref ev, 0);
          float fFacing = GetEventFloatRef(ref ev, 0);
          SetLocation(gameObject, lLoc);
          SetFacing(gameObject, fFacing);
          WR_SetObjectActive(gameObject, EngineConstants.TRUE);
          int nType = GetEventIntegerRef(ref ev, 4); // whether it's the appear (0) or screenshake (1) part
          int nPerceiveParty = GetEventIntegerRef(ref ev, 5);

#if DEBUG
          Log_Trace_AI("AI_ExecuteAppearStomp", "START, type=" + IntToString(nType));
#endif

          if (nType == 0) // appear
          {
               List<GameObject> oTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lLoc, EngineConstants.HIGH_STOMP_RANGE);

               int nCount = 0;
               int nNum = GetArraySize(oTargets);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AI, "AI_ExecuteAppearStomp", "Targets in range = " + ToString(nNum));
#endif
               xEffect eKnock = EffectKnockdown(gameObject, 10);
               SetEffectEngineFloatRef(ref eKnock, EngineConstants.EFFECT_FLOAT_KNOCKBACK_DISTANCE, EngineConstants.HIGH_STOMP_KNOCKDOWN_RANGE);
               GameObject oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, gameObject);
               int nResult;
               float fDamage;
               xEffect eImpactEffect;
               GameObject oTarget;

               for (nCount = 0; nCount < nNum; nCount++)
               {
                    oTarget = oTargets[nCount];
                    if (oTarget != gameObject)
                    {
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_AI, "AI_ExecuteAppearStomp", "Target = " + ToString(oTargets[nCount]));
#endif

                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eKnock, oTarget, 0.0f, gameObject);
                         nResult = Combat_GetAttackResult(gameObject, oTarget, oWeapon);
                         if (nResult == EngineConstants.COMBAT_RESULT_MISS)
                              nResult = EngineConstants.COMBAT_RESULT_HIT;
                         fDamage = Combat_Damage_GetAttackDamage(gameObject, oTarget, oWeapon, nResult);
                         eImpactEffect = EffectImpact(fDamage, oWeapon);
                         Combat_HandleAttackImpact(gameObject, oTarget, nResult, eImpactEffect);
                    }

               }

               if (nPerceiveParty != EngineConstants.FALSE)
               {
                    List<GameObject> arParty = GetPartyList();
                    int nSize = GetArraySize(arParty);
                    int i;
                    GameObject oCurrent;
                    for (i = 0; i < nSize; i++)
                    {
                         oCurrent = arParty[i];
                         if (IsDead(oCurrent) == EngineConstants.FALSE && IsDying(oCurrent) == EngineConstants.FALSE && IsStealthy(oCurrent) == EngineConstants.FALSE && IsPerceiving(gameObject, oCurrent) == EngineConstants.FALSE
                             && GetFollowerState(oCurrent) == EngineConstants.FOLLOWER_STATE_ACTIVE)
                              WR_TriggerPerception(gameObject, oCurrent);
                    }
               }

               // set the type field and re-sent the xEvent
               SetEventIntegerRef(ref ev, 4, 1);
               DelayEvent(0.25f, gameObject, ev);
          }
          else // 1-> screenshake
          {
               Ability_ApplyLocationImpactVFX(EngineConstants.MONSTER_HIGH_DRAGON_STOMP, lLoc);
               xEffect eEffect = EffectScreenShake(EngineConstants.SCREEN_SHAKE_TYPE_BROODMOTHER_SCREEM);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eEffect, gameObject, 2.0f, gameObject, EngineConstants.MONSTER_HIGH_DRAGON_STOMP);

          }
     }

     // returns a return value for using a potion
     public xCommand _AI_GetPotionUseCommand(GameObject oItem)
     {
          xCommand cRet = new xCommand(EngineConstants.COMMAND_TYPE_INVALID);
          // WORKS ONLY IN COMBAT!!!
          if (IsObjectValid(oItem) != EngineConstants.FALSE && GetCombatState(gameObject) != EngineConstants.FALSE)
          {
               int nAbility = GetItemAbilityId(oItem);
               if (Ability_CheckUseConditions(gameObject, gameObject, nAbility) == EngineConstants.FALSE)
                    return cRet; // failed tactic

               if (_AI_CanUseAbility(nAbility, gameObject) == EngineConstants.FALSE)
                    return cRet; // can't use specific ability

               // Command is valid to be executed on the target
               Vector3 vNul = Vector3.zero;
               cRet = CommandUseAbility(nAbility, gameObject, vNul, -1.0f, GetTag(oItem));

          }
          else
               return cRet;

          return cRet;
     }

     // returns a potion based on cretiria
     public GameObject _AI_GetPotionByFilter(int nPotionType, int nPotionPower)
     {
#if DEBUG
          Log_Trace_AI("_AI_GetPotionByFilter", "START, potion type: " + IntToString(nPotionType) + ", potion power type: " + IntToString(nPotionPower));
#endif


          List<GameObject> arItems;

          if (IsFollower(gameObject) != EngineConstants.FALSE)
               arItems = GetItemsInInventory(GetPartyLeader());
          else
               arItems = GetItemsInInventory(gameObject);
          int nSize = GetArraySize(arItems);
          int i;
          GameObject oCurrent;
          GameObject oSelectedItem = null;
          int nCurrentCost;
          int nStoredCost = 0;
#if DEBUG
          Log_Trace_AI("_AI_GetPotionByFilter", "Items #: " + IntToString(nSize));
#endif
          for (i = 0; i < nSize; i++)
          {
               // this can be made faster if we add a special item property just to flag specific types of potions
               oCurrent = arItems[i];
#if DEBUG
               Log_Trace_AI("_AI_GetPotionByFilter", "GOT ITEM: " + GetTag(oCurrent));
#endif

               if (nPotionType == EngineConstants.AI_POTION_TYPE_HEALTH)
               {
                    if (_HasItemProperty(oCurrent, EngineConstants.ITEM_PROPERTY_IS_HEALING_POTION) != EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace_AI("_AI_GetPotionByFilter", "HEALTH POTION!");
#endif
                         nCurrentCost = GetItemValue(oCurrent);
                         if (nPotionPower == EngineConstants.AI_POTION_LEVEL_MOST_POWERFUL && nCurrentCost > nStoredCost)
                         {
                              nStoredCost = nCurrentCost;
                              oSelectedItem = oCurrent;
                         }
                         else if (nPotionPower == EngineConstants.AI_POTION_LEVEL_LEAST_POWERFUL && (nCurrentCost <= nStoredCost || nStoredCost == 0))
                         {
                              nStoredCost = nCurrentCost;
                              oSelectedItem = oCurrent;
                         }
                    }
               }
               else if (nPotionType == EngineConstants.AI_POTION_TYPE_MANA)
               {
                    if (_HasItemProperty(oCurrent, EngineConstants.ITEM_PROPERTY_IS_MANA_POTION) != EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace_AI("_AI_GetPotionByFilter", "MANA POTION!");
#endif
                         nCurrentCost = GetItemValue(oCurrent);
                         if (nPotionPower == EngineConstants.AI_POTION_LEVEL_MOST_POWERFUL && nCurrentCost > nStoredCost)
                         {
                              nStoredCost = nCurrentCost;
                              oSelectedItem = oCurrent;
                         }
                         else if (nPotionPower == EngineConstants.AI_POTION_LEVEL_LEAST_POWERFUL && (nCurrentCost <= nStoredCost || nStoredCost == 0))
                         {
                              nStoredCost = nCurrentCost;
                              oSelectedItem = oCurrent;
                         }
                    }
               }
          }

          return oSelectedItem;
     }

     public void AI_HandleCowardFollower(GameObject oAppear = null)
     {
#if DEBUG
          Log_Trace_AI("AI_HandleCowardFollower", "START, appear: " + GetTag(oAppear));
#endif

          if (GetGameMode() != EngineConstants.GM_COMBAT)
               return;

          if (IsObjectValid(oAppear) != EngineConstants.FALSE &&
               GetGroupHostility(EngineConstants.GROUP_PC, GetGroupId(oAppear)) != EngineConstants.FALSE) // perceived a creature that hates the player
          {
               // run away from this creature
               xCommand cWalk = CommandMoveAwayFromObject(oAppear, EngineConstants.AI_RANGE_MEDIUM, EngineConstants.TRUE);
               PlaySoundSet(gameObject, EngineConstants.SS_EXPLORE_ENEMIES_SIGHTED_OTHER);
               WR_ClearAllCommands(gameObject);
               WR_AddCommand(gameObject, cWalk);
          }
          else // xCommand complete. look for someone to run away from
          {
               GameObject oNearestHostile = UT_GetNearestCreatureByGroup(gameObject, EngineConstants.GROUP_HOSTILE);
               float fDistance = GetDistanceBetween(gameObject, oNearestHostile);
#if DEBUG
               Log_Trace_AI("AI_HandleCowardFollower", "nearest visibile hostile: " + GetTag(oNearestHostile) + ", distance: " + FloatToString(fDistance));
#endif

               if (fDistance < (EngineConstants.AI_RANGE_MEDIUM / 2))
               {
                    xCommand cWalk = CommandMoveAwayFromObject(oNearestHostile, EngineConstants.AI_RANGE_MEDIUM, EngineConstants.TRUE);
                    WR_AddCommand(gameObject, cWalk);
               }
               else if (IsObjectValid(oNearestHostile) != EngineConstants.FALSE && fDistance <= EngineConstants.AI_RANGE_MEDIUM)// cower
               {
                    xCommand cPlayAnimation = CommandPlayAnimation(3009, 1, EngineConstants.TRUE, EngineConstants.TRUE);
                    WR_AddCommand(gameObject, cPlayAnimation);
               }
          }
     }

     //moved const int CAI_DISABLED = 0;
     //moved const int CAI_INACTIVE = 1;
     //moved const int CAI_STASIS   = 2;
     //moved const int CAI_INITIATE = 3;

     public int CAI_SetCustomAI(GameObject oCreature, int nCustomAI, int bInstant = EngineConstants.FALSE)
     {
#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_AI, "CAI_SetCustomAI( " + ToString(nCustomAI) + "); Instant: " + ToString(bInstant), oCreature);
#endif
          SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE, nCustomAI);
          if (bInstant != EngineConstants.FALSE)
          {
               WR_ClearAllCommands(oCreature, EngineConstants.TRUE);
               WR_AddCommand(oCreature, CommandWait(0.01f), EngineConstants.TRUE);
          }
          return nCustomAI;
     }

     public void CAI_SetCustomAIInteger(GameObject oCreature, int nValue)
     {
#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_AI, "CAI_SetCustomAIInteger( " + ToString(nValue) + ")", oCreature);
#endif
          SetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_VAR_INT, nValue);
     }

     public void CAI_SetCustomAIFloat(GameObject oCreature, float fValue)
     {
#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_AI, "CAI_SetCustomAIFloat( " + ToString(fValue) + ")", oCreature);
#endif
          SetLocalFloat(oCreature, EngineConstants.AI_CUSTOM_AI_VAR_FLOAT, fValue);
     }

     public void CAI_SetCustomAIString(GameObject oCreature, string sValue)
     {
#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_AI, "CAI_SetCustomAIString( " + sValue + ")", oCreature);
#endif
          SetLocalString(oCreature, EngineConstants.AI_CUSTOM_AI_VAR_STRING, sValue);
     }

     public void CAI_SetCustomAIObject(GameObject oCreature, GameObject oValue)
     {
#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_AI, "CAI_SetCustomAIObject( " + GetTag(oValue) + ")", oCreature);
#endif
          SetLocalObject(oCreature, EngineConstants.AI_CUSTOM_AI_VAR_OBJECT, oValue);
     }

     public int CAI_GetCustomAI(GameObject oCreature) { return GetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_ACTIVE); }
     public int CAI_GetCustomAIInteger(GameObject oCreature) { return GetLocalInt(oCreature, EngineConstants.AI_CUSTOM_AI_VAR_INT); }
     public float CAI_GetCustomAIFloat(GameObject oCreature) { return GetLocalFloat(oCreature, EngineConstants.AI_CUSTOM_AI_VAR_FLOAT); }
     public string CAI_GetCustomAIString(GameObject oCreature) { return GetLocalString(oCreature, EngineConstants.AI_CUSTOM_AI_VAR_STRING); }
     public GameObject CAI_GetCustomAIObject(GameObject oCreature) { return GetLocalObject(oCreature, EngineConstants.AI_CUSTOM_AI_VAR_OBJECT); }

     //Unused
     // Handles any AI-related routines for the player's party for when combat is over
     //public void _AI_HandlePartyCombatEnd() { }

     /* @brief Calculates what percentage of a team is using ranged weapons
*
* @param oTarget member of the group to check
* @returns percentage as a float [0.0f,1.0]
* @author Noel
*/
     //public float _AI_GetTeamUsingRangedPct(GameObject oTarget = gameObject) { throw new NotImplementedException();}

     /*public void main()
     {
         AI_DetermineCombatRound(gameObject);
         return;
     }*/
     /* @} */
}