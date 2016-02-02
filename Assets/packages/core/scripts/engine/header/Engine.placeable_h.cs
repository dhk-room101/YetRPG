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
using System.Linq;

public partial class Engine
{
     ////////////////////////////////////////////////////////////////////////////////
     //  placeable_h
     //  Copyright � 2007 BioWare Corp.
     ////////////////////////////////////////////////////////////////////////////////
     /*
         Default xEvent handler functions for placeables objects.
     */
     ////////////////////////////////////////////////////////////////////////////////

     //#include"ui_h"
     //#include"sys_traps_h"
     //#include"design_tracking_h"
     //#include"sys_rewards_h"
     //#include"sys_treasure_h"

     //#include"plt_tut_inventory"
     //#include"plt_tut_placeable_locked"

     //#include"achievement_core_h"

     //moved public const string STRING_VAR_NONE  = "none";

     //moved public const string EngineConstants.PLC_TAG_BIG_BALLISTA = "genip_ballista_big";

     /*-----------------------------------------------------------------------------
     * @brief Area transition based on placeable's local variables.
     *-----------------------------------------------------------------------------*/
     public void Placeable_DoAreaTransition(GameObject oPlc)
     {
          string sDest_WP = GetLocalString(oPlc, EngineConstants.PLC_AT_DEST_TAG);
          string sDest_Area = GetLocalString(oPlc, EngineConstants.PLC_AT_DEST_AREA_TAG);
          string sWorldMapLoc1 = GetLocalString(oPlc, EngineConstants.PLC_AT_WORLD_MAP_ACTIVE_1);
          string sWorldMapLoc2 = GetLocalString(oPlc, EngineConstants.PLC_AT_WORLD_MAP_ACTIVE_2);
          string sWorldMapLoc3 = GetLocalString(oPlc, EngineConstants.PLC_AT_WORLD_MAP_ACTIVE_3);
          string sWorldMapLoc4 = GetLocalString(oPlc, EngineConstants.PLC_AT_WORLD_MAP_ACTIVE_4);
          string sWorldMapLoc5 = GetLocalString(oPlc, EngineConstants.PLC_AT_WORLD_MAP_ACTIVE_5);
          UT_PCJumpOrAreaTransition(sDest_Area, sDest_WP, sWorldMapLoc1, sWorldMapLoc2, sWorldMapLoc3, sWorldMapLoc4, sWorldMapLoc5);
     }

     /*-----------------------------------------------------------------------------
     * @brief Area transition confirmation popup.
     *-----------------------------------------------------------------------------*/
     public void Placeable_PromptAreaTransition()
     {
          if (GetObjectInteractive(gameObject) != EngineConstants.FALSE)
          {
               int nGM = GetGameMode();
               if (nGM != EngineConstants.GM_COMBAT && nGM != EngineConstants.GM_GUI)
               {
                    ShowPopup(321167, 1, gameObject);     // Result xEvent is handled in module_core
               }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles the EngineConstants.EVENT_TYPE_POPUP_RESULT event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandlePopupResult(xEvent ev)
     {
          GameObject oOwner = GetEventObjectRef(ref ev, 0);      // owner of popup
          int nPopupID = GetEventIntegerRef(ref ev, 0);     // popup ID (index into popup.xls)
          int nButton = GetEventIntegerRef(ref ev, 1);     // button result (1 - 4)

          switch (nPopupID)
          {
               case 1:     // Placeable area transition
                    {
                         if (nButton == 1)
                              Placeable_DoAreaTransition(oOwner);
                         break;
                    }
               default:
                    Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandlePopupResult()", "*** Unhandled popup ID: " + ToString(nPopupID));
                    break;
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Displays the codex entry (if any) associated with a placeable.
     *-----------------------------------------------------------------------------*/
     public void Placeable_ShowCodexEntry(GameObject oPlc)
     {
          string sCodexPlot = GetLocalString(oPlc, EngineConstants.PLC_CODEX_PLOT);
          int nCodexFlag = GetLocalInt(oPlc, EngineConstants.PLC_CODEX_FLAG);

          if (sCodexPlot != "" && nCodexFlag >= 0)
          {
               string sSummary = GetPlotSummary(sCodexPlot, nCodexFlag);

               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PLACEABLES, GetCurrentScriptName() + ".Placeable_ShowCodexEntry()", "Codex plot: " + sCodexPlot + ", Codex flag: " + IntToString(nCodexFlag));
               if (WR_GetPlotFlag(sCodexPlot, nCodexFlag) == EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(sCodexPlot, nCodexFlag, EngineConstants.TRUE, EngineConstants.TRUE);
                    RewardXPParty(EngineConstants.XP_CODEX, EngineConstants.XP_TYPE_CODEX, null, GetHero());
               }
               UI_DisplayCodexMessage(oPlc, sSummary);
               SetObjectInteractive(oPlc, EngineConstants.FALSE);
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles the EngineConstants.EVENT_TYPE_SPAWN placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleSpawned(xEvent ev)
     {
          // Database spawn tracking. High volume xEvent so disabled by default.
          /*if (TRACKING_TRACK_SPAWN_EVENTS)
          {
               //TrackPlaceableEvent(GetEventTypeRef(ref ev), gameObject, null, GetAppearanceType(gameObject));
          }*/

          if (GetLocalInt(gameObject, EngineConstants.PLC_SPAWN_NON_INTERACTIVE) == 1)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PLACEABLES, GetCurrentScriptName() + "Spawning placeable non-interactive");
               SetObjectInteractive(gameObject, EngineConstants.FALSE);
          }

          //Codex placeables will spawn non-interactive if the player already has the entry.
          string sCodexPlot = GetLocalString(gameObject, EngineConstants.PLC_CODEX_PLOT);
          int nCodexFlag = GetLocalInt(gameObject, EngineConstants.PLC_CODEX_FLAG);

          if ((nCodexFlag >= 0) && (sCodexPlot != ""))
          {
               if (WR_GetPlotFlag(sCodexPlot, nCodexFlag) != EngineConstants.FALSE)
               {
                    SetObjectInteractive(gameObject, EngineConstants.FALSE);
               }
          }

          // Generate random treasure
          if (GetPlaceableBaseType(gameObject) == EngineConstants.PLACEABLE_TYPE_CHEST)
          {
               TreasureGenerate(gameObject);
          }

          // Automatically arm trap if no owner.
          if (GetObjectActive(gameObject) != EngineConstants.FALSE
             && Trap_GetType(gameObject) > 0
             && IsObjectValid(Trap_GetOwner(gameObject)) == EngineConstants.FALSE)
          {
               Trap_ArmTrap(gameObject, null, 0.0f);
          }

          // Set initial health
          if (GetMaxHealth(gameObject) <= 1.1f)
          {
               int nHealth = GetM2DAInt(EngineConstants.TABLE_PLACEABLE_TYPES, "Health", GetAppearanceType(gameObject));
               if (nHealth > 1)
                    SetMaxHealth(gameObject, nHealth);
          }

          // Apply crust effect
          int nCrustEffect = GetM2DAInt(EngineConstants.TABLE_PLACEABLE_TYPES, "CrustVFX", GetAppearanceType(gameObject));
          if (nCrustEffect != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleSpawned()", " Applying CrustVFX: " + ToString(nCrustEffect));
               ApplyEffectVisualEffect(gameObject, gameObject, nCrustEffect, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, 0.0f);
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles the EngineConstants.EVENT_TYPE_USE placeable event.
     *
     * This xEvent handler uses GetPlaceableAction() to identify the specific action
     * that triggered the xEvent (i.e. placeable was used, opened, unlocked, etc.) and
     * SetPlaceableActionResult() to trigger a state change based on the result of
     * the action.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleUsed(xEvent ev)
     {
          GameObject oThis = gameObject;
          GameObject oUser = GetEventCreatorRef(ref ev);
          int nAction = GetPlaceableAction(gameObject);
          int nActionResult = EngineConstants.TRUE;
          int bVariation = GetEventIntegerRef(ref ev, 0); // if true, Success0A column chosen in 2DA
                                                          // (used to make door always open away from player)
          if (GetObjectActive(gameObject) == EngineConstants.FALSE)
               return;

          Placeable_ShowCodexEntry(gameObject);

          switch (nAction)
          {
               case EngineConstants.PLACEABLE_ACTION_USE:
                    {
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_OPEN:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleUsed()", "Variation: " + ToString(bVariation));

                         // For doors, bVariation is 1/0 to indicate the player is using it from the front/back.
                         // However, the Success0 column should be used by SetPlaceableActionResult() if the player
                         // is using the door from the front and Success0A column if from the back. To facilitate
                         // this logic, simply invert the value of bVariation.
                         //bVariation = !bVariation;
                         bVariation = (bVariation + 1 == 1) ? EngineConstants.TRUE : EngineConstants.FALSE;

                         // PrxEvent use of containers during combat.
                         string sController = GetPlaceableStateCntTable(gameObject);
                         if (GetGameMode() == EngineConstants.GM_COMBAT &&
                             (sController == EngineConstants.PLC_STATE_CNT_CONTAINER_STATIC ||
                             sController == EngineConstants.PLC_STATE_CNT_CONTAINER ||
                             sController == EngineConstants.PLC_STATE_CNT_BODYBAG))
                         {
                              UI_DisplayMessage(oUser, EngineConstants.UI_MESSAGE_CANT_DO_IN_COMBAT);
                              nActionResult = EngineConstants.FALSE;
                         }
                         else
                         {
                              SendEventOpened(gameObject, oUser);
                         }
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_CLOSE:
                    {
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_AREA_TRANSITION:
                    {
                         Placeable_DoAreaTransition(gameObject);
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_CONVERSATION:
                    {
                         if (HasConversation(gameObject) != EngineConstants.FALSE && GetCombatState(oUser) == EngineConstants.FALSE)
                         {
                              BeginConversation(oUser, gameObject);
                         }
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_EXAMINE:
                    {
                         if (UI_DisplayPopupText(gameObject, gameObject) == EngineConstants.FALSE)
                         {
                              if (HasConversation(gameObject) != EngineConstants.FALSE && GetCombatState(oUser) == EngineConstants.FALSE)
                                   BeginConversation(oUser, gameObject);
                         }
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_TRIGGER_TRAP:
                    {
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_DISARM:
                    {
                         if (HasAbility(oUser, EngineConstants.ABILITY_TALENT_HIDDEN_ROGUE) == EngineConstants.FALSE)
                         {
                              // Only rogues can disarm traps
                              nActionResult = EngineConstants.FALSE;
                         }

                         // Trap detection difficulty property is used instead of trap disarm property
                         // because as a rule any trap you can detect you should be able to disarm.
                         // It's easier to enforce this in screipt than check every trap placed in
                         // every level of the game.
                         int nTargetScore = GetTrapDisarmDifficulty(gameObject);
                         int nPlayerScore = FloatToInt(GetDisableDeviceLevel(oUser));

                         if (nActionResult != EngineConstants.FALSE)
                         {
                              if (Trap_GetOwner(gameObject) == oUser)
                              {
                                   // Can always disarm your own traps
                                   nActionResult = EngineConstants.TRUE;
                              }
                              else
                              {
                                   Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName(), "Player score: " + ToString(nPlayerScore) + " vs Disarm Level: " + ToString(nTargetScore));

                                   nActionResult = (nPlayerScore >= nTargetScore) ? EngineConstants.TRUE : EngineConstants.FALSE;
                              }

                              WR_AddCommand(oUser, CommandPlayAnimation(904));

                              if (nActionResult != EngineConstants.FALSE)
                              {
                                   // Can only disarm a trap once.
                                   if (GetLocalInt(gameObject, EngineConstants.PLC_DO_ONCE_A) == EngineConstants.FALSE)
                                   {
                                        SetLocalInt(gameObject, EngineConstants.PLC_DO_ONCE_A, EngineConstants.TRUE);

                                        // Slight delay to account for disarm animation.
                                        Trap_SignalDisarmEvent(gameObject, oUser, 3.0f);
                                   }
                              }
                              else
                              {
                                   if (nTargetScore >= EngineConstants.DEVICE_DIFFICULTY_IMPOSSIBLE)
                                   {
                                        UI_DisplayMessage(oUser, EngineConstants.UI_MESSAGE_DISARM_NOT_POSSIBLE);
                                   }
                                   else
                                   {
                                        UI_DisplayMessage(oUser, EngineConstants.TRAP_DISARM_FAILED);
                                        SSPartyMemberComment(EngineConstants.CLASS_ROGUE, EngineConstants.SOUND_SITUATION_SKILL_FAILURE, oUser);
                                   }
                                   Trap_SignalTeam(gameObject);
                                   PlaySound(gameObject, EngineConstants.SOUND_TRAP_DISARM_FAILURE);
                              }
                         }
                         else
                         {
                              UI_DisplayMessage(oUser, EngineConstants.TRAP_DISARM_FAILED);
                              Trap_SignalTeam(gameObject);
                         }

                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_UNLOCK:
                    {
                         int nLockLevel = GetPlaceablePickLockLevel(gameObject);
                         int bRemoveKey = GetPlaceableAutoRemoveKey(gameObject);
                         int bKeyRequired = GetPlaceableKeyRequired(gameObject);
                         string sKeyTag = GetPlaceableKeyTag(gameObject);
                         int bUsedKey = EngineConstants.FALSE;

                         if (IsPartyMember(oUser) != EngineConstants.FALSE)
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_TUT_PLACEABLE_LOCKED, EngineConstants.TUT_PLACEABLE_LOCKED_ENCOUNTER_1, EngineConstants.TRUE);
                         }

                         // Set ActionResult to reflect the 'unlocked' state
                         nActionResult = EngineConstants.FALSE;

                         // Attempt to use key
                         if (sKeyTag != "")
                         {
                              GameObject oKey = GetItemPossessedBy(oUser, sKeyTag);
                              if (IsObjectValid(oKey) != EngineConstants.FALSE)
                              {
                                   bUsedKey = EngineConstants.TRUE;
                                   nActionResult = EngineConstants.TRUE;
                                   if (bRemoveKey != EngineConstants.FALSE)
                                        DestroyObject(oKey, 0);
                              }
                         }

                         int bLockPickable = (nLockLevel < EngineConstants.DEVICE_DIFFICULTY_IMPOSSIBLE) ? EngineConstants.TRUE : EngineConstants.FALSE;
                         if (bLockPickable != EngineConstants.FALSE)
                         {
                              // If still locked and key not required then rogues can attempt to pick lock.
                              if (nActionResult == EngineConstants.FALSE && bKeyRequired == EngineConstants.FALSE && HasAbility(oUser, EngineConstants.ABILITY_TALENT_HIDDEN_ROGUE) != EngineConstants.FALSE)
                              {
                                   // player score
                                   float fPlayerScore = GetDisableDeviceLevel(oUser);
                                   float fTargetScore = IntToFloat(nLockLevel);

                                   Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName(), "nLockLevel = " + ToString(nLockLevel));
                                   Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName(), "Final Value = " + ToString(fPlayerScore));

                                   nActionResult = (fPlayerScore >= fTargetScore) ? EngineConstants.TRUE : EngineConstants.FALSE;
                              }
                         }

                         if (nActionResult != EngineConstants.FALSE)
                         {
                              // Success
                              UI_DisplayMessage(gameObject, (bUsedKey != EngineConstants.FALSE ? EngineConstants.UI_MESSAGE_UNLOCKED_BY_KEY : EngineConstants.UI_MESSAGE_UNLOCKED));
                              PlaySound(gameObject, GetM2DAString(EngineConstants.TABLE_PLACEABLE_TYPES, "PickLockSuccess", GetAppearanceType(gameObject)));

                              if (bKeyRequired == EngineConstants.FALSE)
                                   AwardDisableDeviceXP(oUser, nLockLevel);
                         }
                         else
                         {//`
                              if (bKeyRequired != EngineConstants.FALSE)
                              {
                                   UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_KEY_REQUIRED);
                              }
                              else
                              {
                                   if (bLockPickable == EngineConstants.FALSE)
                                   {
                                        UI_DisplayMessage(oUser, EngineConstants.UI_MESSAGE_LOCKPICK_NOT_POSSIBLE);
                                   }
                                   else
                                   {
                                        UI_DisplayMessage(oUser, EngineConstants.UI_MESSAGE_UNLOCK_SKILL_LOW);
                                        SSPartyMemberComment(EngineConstants.CLASS_ROGUE, EngineConstants.SOUND_SITUATION_SKILL_FAILURE, oUser);
                                   }
                              }
                              PlaySound(gameObject, GetM2DAString(EngineConstants.TABLE_PLACEABLE_TYPES, "PickLockFailure", GetAppearanceType(gameObject)));
                         }

                         //increment unlocking achievement
                         if (nActionResult != EngineConstants.FALSE && bUsedKey == EngineConstants.FALSE)
                         {
                              ACH_LockpickAchievement(oUser);
                         }

                         // Signal result to self.
                         xEvent evResult = Event(nActionResult != EngineConstants.FALSE ? EngineConstants.EVENT_TYPE_UNLOCKED : EngineConstants.EVENT_TYPE_UNLOCK_FAILED);
                         SetEventObjectRef(ref evResult, 0, oUser);
                         SignalEvent(gameObject, evResult);

                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_OPEN_INVENTORY:
                    {
                         SendEventOpened(gameObject, oUser);

                         if (IsBodyBag(gameObject) != EngineConstants.FALSE)
                         {
                              // get number of inventory items
                              List<GameObject> oInventory = GetItemsInInventory(gameObject, EngineConstants.GET_ITEMS_OPTION_ALL);
                              int nSize1 = GetArraySize(oInventory);

                              // if inventory exists
                              if (nSize1 > 0)
                              {
                                   // get number of money items
                                   List<GameObject> oMoney = GetItemsInInventory(gameObject, EngineConstants.GET_ITEMS_OPTION_ALL, 0, "gen_im_copper");
                                   int nSize2 = GetArraySize(oMoney);

                                   // if the only items are money items
                                   if (nSize1 == nSize2)
                                   {
                                        // add money stack sizes and delete objects
                                        int nMoney = 0;
                                        int nCount = 0;
                                        for (nCount = 0; nCount < nSize2; nCount++)
                                        {
                                             nMoney += GetItemStackSize(oMoney[nCount]);
                                             Safe_Destroy_Object(oMoney[nCount], 0);
                                        }

                                        // add money directly to looter
                                        AddCreatureMoney(nMoney, oUser);
                                   }
                                   else
                                   {
                                        OpenInventory(gameObject, oUser);
                                   }
                              }
                              else
                              {
                                   OpenInventory(gameObject, oUser);
                              }
                         }
                         else
                         {
                              if (FindSubString(GetTag(gameObject), "_autoloot") >= 0)
                              {
                                   MoveAllItems(gameObject, oUser);
                              }
                              else
                              {
                                   OpenInventory(gameObject, oUser);
                              }
                         }

                         if (GetLocalInt(GetModule(), EngineConstants.TUTORIAL_ENABLED) != EngineConstants.FALSE)
                              WR_SetPlotFlag(EngineConstants.PLT_TUT_INVENTORY, EngineConstants.TUT_INVENTORY_1, EngineConstants.TRUE);
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_FLIP_COVER:
                    {
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_USE_COVER:
                    {
                         // Store user so they can be un-crouched if placeable is destroyed.
                         SetLocalObject(gameObject, EngineConstants.PLC_FLIP_COVER_CREATURE_1, oUser);

                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_LEAVE_COVER:
                    {
                         SetLocalObject(gameObject, EngineConstants.PLC_FLIP_COVER_CREATURE_1, null);
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_TOPPLE:
                    {
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_DESTROY:
                    {

                         // Remove stealth
                         if (IsStealthy(oUser) != EngineConstants.FALSE)
                              SetStealthEnabled(oUser, EngineConstants.FALSE);

                         // Make user attack placeable
                         WR_AddCommand(oUser, CommandAttack(gameObject));

                         // Return (instead of break) since the action result for the
                         // destroy action is set by the death xEvent handler (i.e. the
                         // destroy action succeeds when the placeable reaches 0 health).
                         return;
                    }

               case EngineConstants.PLACEABLE_ACTION_TURN_LEFT:
                    {
                         // large ballista rotation (activated on base)
                         // Find nearest large ballista GameObject and rotate to match base rotation
                         GameObject oTop = UT_GetNearestObjectByTag(gameObject, EngineConstants.PLC_TAG_BIG_BALLISTA);
                         if (IsObjectValid(oTop) != EngineConstants.FALSE)
                         {
                              SetFacing(oTop, GetFacing(oTop) - 15.0f);
                         }
                         PlaySound(gameObject, "glo_fly_plc/placeables/ballista_mount/ballista_mount");
                         break;
                    }

               case EngineConstants.PLACEABLE_ACTION_TURN_RIGHT:
                    {
                         // large ballista rotation (activated on base)
                         // Find nearest large ballista GameObject and rotate to match base rotation
                         GameObject oTop = UT_GetNearestObjectByTag(gameObject, EngineConstants.PLC_TAG_BIG_BALLISTA);
                         if (IsObjectValid(oTop) != EngineConstants.FALSE)
                         {
                              SetFacing(oTop, GetFacing(oTop) + 15.0f);
                         }
                         PlaySound(gameObject, "glo_fly_plc/placeables/ballista_mount/ballista_mount");
                         break;
                    }

               default:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleUsed", "PLACEABLE_ACTION (" + ToString(nAction) + ") *** Unhandled action ***");
                         break;
                    }
          }

          //TrackPlaceableEvent(GetEventTypeRef(ref ev), gameObject, oUser, nAction, nActionResult);

          // Action result determines next state transition.
          SetPlaceableActionResult(gameObject, nAction, nActionResult, bVariation);
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles the EngineConstants.EVENT_TYPE_CONVERSATION placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleDialog(xEvent ev)
     {
          GameObject oInitiator = GetEventCreatorRef(ref ev);      // Player or NPC to talk to.
          string rConversation = GetEventResourceRef(ref ev, 0);  // Conversation to play.

          if (GetCombatState(oInitiator) == EngineConstants.FALSE)
          {
               UT_Talk(gameObject, oInitiator);
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles EngineConstants.EVENT_TYPE_INVENTORY_* placeable events.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleInventory(xEvent ev)
     {
          GameObject oOwner = GetEventCreatorRef(ref ev);      // Previous owner
          GameObject oItem = GetEventObjectRef(ref ev, 0);    // item added/removed

          switch (GetEventTypeRef(ref ev))
          {
               case EngineConstants.EVENT_TYPE_INVENTORY_ADDED:
                    {
                         break;
                    }
               case EngineConstants.EVENT_TYPE_INVENTORY_REMOVED:
                    {
                         break;
                    }
          }
     }

     /*----------------------------------------------------------------------------
     * @brief Handles the EngineConstants.EVENT_TYPE_ATTACKED placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleAttacked(xEvent ev)
     {
          GameObject oAttacker = GetEventCreatorRef(ref ev);
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles the EngineConstants.EVENT_TYPE_DAMAGED placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleDamaged(xEvent ev)
     {
          GameObject oDamager = GetEventCreatorRef(ref ev);
          float fDamage = GetEventFloatRef(ref ev, 0);
          int nDamageType = GetEventIntegerRef(ref ev, 0);

          // Outside of combat, force damager to continue bashing placeable
          if (GetGameMode() == EngineConstants.GM_EXPLORE && GetCurrentHealth(gameObject) > 0.0f)
          {
               WR_AddCommand(oDamager, CommandAttack(gameObject));
          }

     }

     /*-----------------------------------------------------------------------------
     * @brief Handles the EngineConstants.EVENT_TYPE_DEATH placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleDeath(xEvent ev)
     {
          GameObject oKiller = GetEventCreatorRef(ref ev);

          // Play death visual effect
          int nType = GetAppearanceType(gameObject);
          int nVFX = GetM2DAInt(EngineConstants.TABLE_PLACEABLE_TYPES, "DestroyVFX", nType);
          if (nVFX != EngineConstants.FALSE)
          {
               ApplyEffectVisualEffect(gameObject, gameObject, nVFX, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, 1.5f);
          }

          // Determine angle to last attacker for doors and set variation accordingly.
          int bVariation = EngineConstants.TRUE;
          if (GetPlaceableStateCntTable(gameObject) == EngineConstants.PLC_STATE_CONTROLLER_DOOR)
          {
               float fAngle = GetAngleBetweenObjects(gameObject, oKiller);
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleDeath()", "fAngle: " + ToString(fAngle));
               if (fAngle < 90.0f || fAngle > 270.0f)
                    bVariation = EngineConstants.FALSE;
          }

          // The result of the destroy action is set in the death xEvent handler since
          // the destroy action 'succeeds' only when the placeable reaches 0 health.
          //SetPlaceableActionResult(gameObject, EngineConstants.PLACEABLE_ACTION_DESTROY, EngineConstants.TRUE, bVariation);
          string sStateTable = GetPlaceableStateCntTable(gameObject);
          int nDeathState = 0;
          if (sStateTable == "StateCnt_Furniture" || sStateTable == "StateCnt_Puzzle" || sStateTable == "StateCnt_Static")
               nDeathState = 1;
          else if (sStateTable == "StateCnt_AOE" || sStateTable == "StateCnt_FlipCover" || sStateTable == "StateCnt_Selectable_Trap"
              || sStateTable == "StateCnt_Container_Static" || sStateTable == "StateCnt_Trigger" || sStateTable == "StateCnt_Door_Secret")
               nDeathState = 2;
          else if (sStateTable == "StateCnt_Cage" || sStateTable == "StateCnt_Container" || sStateTable == "StateCnt_BBase" || sStateTable == "StateCnt_Door")
               nDeathState = 3;

          SetPlaceableState(gameObject, nDeathState);

          // If killer is not in combat, stop attacking placeable.
          if (GetGameMode() == EngineConstants.GM_EXPLORE)
          {

               //WR_ClearAllCommands(oKiller);
               WR_AddCommand(oKiller, CommandWait(2.0f));
               WR_AddCommand(oKiller, CommandSheatheWeapons());
          }

          // Damage contents of containers.
          List<GameObject> aItems = GetItemsInInventory(gameObject);
          int nItems = GetArraySize(aItems);
          if (nItems > 0)
          {
               int i;
               for (i = 0; i < nItems; i++)
               {
                    int n = GetItemStackSize(aItems[i]);
                    if (n > 1)
                    {
                         SetItemStackSize(aItems[i], n / 2);
                    }
                    else if (IsPlot(aItems[i]) != EngineConstants.FALSE && GetItemEquipSlotMask(GetBaseItemType(aItems[i])) != EngineConstants.FALSE && Engine_Random(2) > 1)//>1 added by DHK
                    {
                         //SetItemDamaged(aItems[i], EngineConstants.TRUE);//deprecated
                         Warning("set item damaged used in DA:O was deprecated in DA2…");
                    }
               }
          }

          // Debug - set inactive
          if (GetPlaceableBaseType(gameObject) == 220)
               SetObjectActive(gameObject, EngineConstants.FALSE);

          // //Track death for game metrics
          //TrackObjectDeath(ev);
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles EngineConstants.EVENT_TYPE_COMMAND_COMPLETE placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleCommandCompleted(xEvent ev)
     {
          //    int nLastCommandType = GetEventIntegerRef(ref ev, 0);
          //    int nCommandStatus   = GetEventIntegerRef(ref ev, 1);
          //    int nLastSubCommand  = GetEventIntegerRef(ref ev, 2);
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles EngineConstants.EVENT_TYPE_UNLOCK_FAILED placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleUnlockFailed(xEvent ev)
     {
          // play unlock failed sound
          string sSound = GetM2DAString(EngineConstants.TABLE_PLACEABLE_TYPES, "PickLockFailure", GetAppearanceType(gameObject));
          PlaySound(gameObject, sSound);
          /*
              GameObject oPlc = gameObject;
              if (!IsPlot(oPlc))
              {
                  // Try bashing it open instead.
                  GameObject oUser = GetEventObjectRef(ref ev, 0);
                  WR_AddCommand(oUser, CommandAttack(oPlc));
              }
          */
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles EngineConstants.EVENT_TYPE_UNLOCKED placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleUnlocked(xEvent ev)
     {
          // play unlock success sound
          string sSound = GetM2DAString(EngineConstants.TABLE_PLACEABLE_TYPES, "PickLockSuccess", GetAppearanceType(gameObject));
          PlaySound(gameObject, sSound);

          // Automatically open doors/containers when they are unlocked.
          GameObject oUser = GetEventObjectRef(ref ev, 0);
          string sStateController = GetPlaceableStateCntTable(gameObject);
          if (sStateController == EngineConstants.PLC_STATE_CONTROLLER_CONTAINER)
          {
               AddCommand(oUser, CommandUseObject(gameObject, EngineConstants.PLACEABLE_ACTION_OPEN_INVENTORY));
          }
          /*  else if (sStateController == EngineConstants.PLC_STATE_CONTROLLER_DOOR)
              {
                  AddCommand(oUser, CommandUseObject(gameObject, EngineConstants.PLACEABLE_ACTION_OPEN));
              }
          */
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles EngineConstants.EVENT_TYPE_COMMAND_COMPLETE placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleCastAt(xEvent ev)
     {
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles EngineConstants.EVENT_TYPE_PLACEABLE_ONCLICK placeable event.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleClicked(xEvent ev)
     {
     }

     /*-----------------------------------------------------------------------------
     * @brief Handles EngineConstants.EVENT_TYPE_ATTACK_IMPACT placeable event.
     *
     * The EngineConstants.EVENT_TYPE_ATTACK_IMPACT xEvent is triggered when a projectile fired by
     * the placeable strikes something (creature, placeable, surface, etc). The
     * xEvent fires once for all targets hit in a single frame.
     *
     * @param    ev  The xEvent being handled.
     *-----------------------------------------------------------------------------*/
     public void Placeable_HandleImpact(xEvent ev)
     {
          int i;
          List<GameObject> arTarget = new List<GameObject>();
          //for (i = 1; IsObjectValid(GetEventObjectRef(ref ev, i)); i++)
          for (i = 1; i < ev.oList.Count; i++)
          {
               GameObject oObject = ev.oList.ElementAt(i);
               if (IsObjectValid(oObject) != EngineConstants.FALSE)
               {
                    arTarget.Add(oObject);
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleImpact()", "Target [" + IntToString(i) + "]: " + GetTag(GetEventObjectRef(ref ev, i)));
               }
               //perhaps in inner loop?
               if (i >= 2 && arTarget[i - 1] == arTarget[i - 2])
               {
                    arTarget[i - 1] = null;
                    break;
               }
          }
          GameObject oAttacker = GetEventObjectRef(ref ev, 0);
          int nTargets = GetArraySize(arTarget);
          int nCombatResult = GetEventIntegerRef(ref ev, 0);
          int nProjectileType = GetEventIntegerRef(ref ev, 2);

          Vector3 lImpact = GetEventVectorRef(ref ev, 0);  // position
          Vector3 lMissile = GetEventVectorRef(ref ev, 1);  // orientation

          for (i = 0; i < nTargets; i++)
          {
               if (IsObjectValid(arTarget[i]) == EngineConstants.FALSE)
                    break;

               // Apply damage to targets based on projectile type
               float fDamage = 0.0f;
               switch (nProjectileType)
               {
                    case 51: // ballista bolt (from BITM_base.xls)
                         {
                              fDamage = 7.0f + RandomF(4, 1) * GetLevel(arTarget[i]);   // base damage
                              fDamage = DmgGetArmorMitigatedDamage(fDamage, 2.0f, arTarget[i]);

                              break;
                         }
                    case 54: // big ballista bolt
                    case 58: // same, climax only
                         {
                              // checking only first target as the archdemon ended up being hit 3 times
                              // by the same bolt.
                              fDamage = 50.0f + RandomF(4, 1) * GetLevel(arTarget[i]);   // base damage
                              fDamage = DmgGetArmorMitigatedDamage(fDamage, 5.0f, arTarget[i]);

                              break;
                         }
                    default:
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleImpact()", ToString(nProjectileType) + " *** Unhandled projectile type ***");
                         break;
               }

               xEffect eDamage = EffectDamage(fDamage, EngineConstants.DAMAGE_TYPE_PHYSICAL, EngineConstants.DAMAGE_EFFECT_FLAG_UPDATE_GORE | EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eDamage, arTarget[i]);
               if (GetAppearanceType(arTarget[i]) == EngineConstants.APP_TYPE_ARCHDEMON)
               {
                    if (nProjectileType == 54 || nProjectileType == 58)
                    {
                         oAttacker = GetLocalObject(oAttacker, EngineConstants.PLC_FLIP_COVER_CREATURE_1);
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleImpact()", "attacker: " + GetTag(oAttacker));
                    }
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleImpact()", "ARCHDEMON!");
                    WR_ClearAllCommands(arTarget[i], EngineConstants.TRUE);
                    xCommand cScream = CommandPlayAnimation(149);
                    //xCommand cScream = CommandUseAbility(EngineConstants.MONSTER_HIGH_DRAGON_ROAR, arTarget[i]);
                    WR_AddCommand(arTarget[i], cScream, EngineConstants.TRUE);

                    // jump somewhere
                    //List<GameObject> arWPs = GetNearestObjectByTag(arTarget[i], EngineConstants.AI_WP_MOVE, EngineConstants.OBJECT_TYPE_WAYPOINT, 2);
                    //Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleImpact()", "Jump wps found: " + IntToString(GetArraySize(arWPs)));
                    //GameObject oWP = arWPs[1]; // second farthest
                    //if(IsObjectValid(oWP))
                    //{
                    //        Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleImpact()", "JUMPING");
                    //        xCommand cJump = CommandFly(GetLocation(oWP));
                    //        WR_AddCommand(arTarget[i], cJump, EngineConstants.FALSE);
                    //}

                    // generate tons of threat against the shooter (from archdemon and anyone else around
                    float fThreatChange = 150.0f;
                    // Not updating for archdemon (too brutal)
                    //AI_Threat_UpdateCreatureThreat(arTarget[i], oAttacker, fThreatChange);
                    List<GameObject> arEnemies = GetNearestObjectByGroup(gameObject, EngineConstants.GROUP_HOSTILE, EngineConstants.OBJECT_TYPE_CREATURE, 10, EngineConstants.TRUE);
                    int nSize = GetArraySize(arEnemies);
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName() + ".Placeable_HandleImpact()", "found enemies: " + IntToString(nSize));
                    GameObject oCurrent;
                    for (i = 0; i < nSize; i++)
                    {
                         oCurrent = arEnemies[i];
                         if (oCurrent != arTarget[i])
                              AI_Threat_UpdateCreatureThreat(oCurrent, oAttacker, fThreatChange);
                    }

               }
               else
               {
                    xEffect eKnockdown = EffectKnockdown(gameObject, 0);
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eKnockdown, arTarget[i]);
               }
          }
     }
}