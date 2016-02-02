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

public class ability_core : MonoBehaviour
{
    Engine engine { get; set; }
     // -----------------------------------------------------------------------------
     //  ability_core.nss
     // -----------------------------------------------------------------------------
     //
     // This script is fired by the engine whenever any ability (spell, talent) is used
     //
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"ability_h"
     //#include"rules_h"
     //#include"log_h"
     //#include"events_h"
     //#include"utility_h"
     void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     public void HandleEvent(xEvent ev)
     {

          // ------------------------------------------------------------------------
          // engine.Getting the xEvent parameters:
          //
          //   nEventType can be any of the following:
          //   * EngineConstants.EVENT_TYPE_COMMAND_PENDING (signaled by either creature_core, follower_core or player core)
          //   * EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT (signaled by the engine)
          //   * EngineConstants.EVENT_TYPE_ABILITY_CAST_START  (signaled by the engine)
          //
          //   oCaster  is the creature using the ability
          //   oTarget  is the GameObject that the ability was targeted
          //   oItem    is the item used to cast the ability (optional)
          //   nAbility is the ability that was triggered
          // ------------------------------------------------------------------------
          //xEvent ev = engine.GetCurrentEvent();
          int nEventType = engine.GetEventTypeRef(ref ev);
          GameObject oCaster = engine.GetEventObjectRef(ref ev, 0);
          GameObject oItem;
          GameObject oTarget;

          if (nEventType == EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT || nEventType == EngineConstants.EVENT_TYPE_ABILITY_CAST_START)
          {
               oItem = engine.GetEventObjectRef(ref ev, 1);
               oTarget = engine.GetEventObjectRef(ref ev, 2);
               // note: in case of a projectile impact, there might be more than one target (GameObject 2+...)
               // this allows doing things like lightning bolts going through targets, but we're not making
               // use of this in scripting right now.
          }
          else
          {
               oTarget = engine.GetEventObjectRef(ref ev, 1);
               oItem = engine.GetEventObjectRef(ref ev, 2);
          }

          int nAbility = 0;

          string sDebug;

          if (nEventType == EngineConstants.EVENT_TYPE_COMMAND_PENDING)
          {
               nAbility = engine.GetEventIntegerRef(ref ev, 1);
          }
          else
          {
               nAbility = engine.GetEventIntegerRef(ref ev, 0);
          }

          //<Debug>
          if (EngineConstants.LOG_ENABLED == EngineConstants.TRUE)
          {
               sDebug = engine.GetCurrentScriptName() + "." + engine.Log_GetEventNameById(nEventType) + "." + engine.Log_GetAbilityNameById(nAbility);
          }
          //</Debug>

          int bHasProjectile = engine.GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "projectile", nAbility);

          engine.Log_Events("ability_core", ev);

          // -------------------------------------------------------------------------
          // Some validation
          // -------------------------------------------------------------------------

          if (engine.IsDead(oCaster) != EngineConstants.FALSE)     // If the caster is dead for some reasons, ignore
          {

               engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, sDebug, "Caster is dead - ignoring event");
               return;
          }

          // -------------------------------------------------------------------------
          // Ability ID Invalid
          // -------------------------------------------------------------------------
          if (nAbility == 0)
          {
               engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, sDebug, "GOT EngineConstants.INVALID ABILITY ID - doing nothing");
               return;
          }

          // -------------------------------------------------------------------------
          // Retrieve further information:
          //  - Cost of Ability (stamina or mana depending on type)
          //    - Type of Ability (spell, talent, etc)
          // -------------------------------------------------------------------------
          //
          int nAbilityType = engine.Ability_GetAbilityType(nAbility);

          switch (nEventType)
          {

               // ---------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_COMMAND_PENDING
               // ---------------------------------------------------------------------
               // Fired when the ability is starting to execute in the xCommand queue
               // The engine might include some movement/orientation sub-commands after this stage
               // This xEvent should be used to:
               // - Check if we need to disable a modal talent
               // - Verify mana/stamina values -> notify engine if ability can't execute
               // - Verify valid target -> notify engine if ability can't execute
               // ---------------------------------------------------------------------

               case 90210:
                    {
                         engine.Ability_DoRunSpellScript(ev, nAbility, nAbilityType);
                         break;
                    }

               case EngineConstants.EVENT_TYPE_COMMAND_PENDING:
                    {

                         // -----------------------------------------------------------------
                         // Retrieve even more xEvent parameters.
                         // -----------------------------------------------------------------
                         Vector3 lTarget;
                         int nAbilityFailureCode = 0;
                         int bTargetValid = EngineConstants.TRUE;
                         int bTargetIsGround = EngineConstants.FALSE;
                         int nAbilityTargetType = engine.Ability_GetAbilityTargetType(nAbility, nAbilityType);

                         // -----------------------------------------------------------------
                         // Check if the target is valid based on the ability target type
                         // -----------------------------------------------------------------

                         // -----------------------------------------------------------------
                         // If no valid target GameObject was found AND the target GameObject is invalid
                         // -> then it's a ground target
                         // -----------------------------------------------------------------
                         if (engine.IsObjectValid(oTarget) == EngineConstants.FALSE &&
                              (nAbilityTargetType == EngineConstants.TARGET_TYPE_GROUND))
                         {

                              lTarget = engine.GetEventVectorRef(ref ev, 0);
                              if (engine.IsLocationValid(lTarget) != EngineConstants.FALSE)
                              {

                                   Vector3 v = engine.GetPositionFromLocation(lTarget);

                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_core.EngineConstants.EVENT_TYPE_COMMAND_PENDING",
                                             "Event Location: " + engine.ToString(v.x) + "," + engine.ToString(v.y));
                              }
                              else
                              {
                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_core.EngineConstants.EVENT_TYPE_COMMAND_PENDING", "Event Location: EngineConstants.INVALID");
                              }
                         }

                         // -----------------------------------------------------------------
                         // If there is no valid target and the ability is a self targeted one
                         // -> then the target is the caster
                         // This is probably not needed, since the engine should return the correct
                         // target when using a self ability
                         // -----------------------------------------------------------------
                         else if (engine.IsObjectValid(oTarget) == EngineConstants.FALSE &&
                              (nAbilityTargetType == EngineConstants.TARGET_TYPE_SELF))
                         {

                              oTarget = oCaster;
                         }

                         if (engine.Ability_IsAbilityTargetValid(oTarget, nAbility, nAbilityTargetType) == EngineConstants.FALSE)
                         {
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, sDebug, "Bad Target for Ability", oTarget);
                              engine.SetCommandResult(oCaster, EngineConstants.COMMAND_RESULT_FAILED_NO_VALID_TARGET);
                              return;
                         }

                         // -----------------------------------------------------------------
                         // Handle player requests to disable a modal ability
                         // -----------------------------------------------------------------
                         if (engine.Ability_IsModalAbility(nAbility) != EngineConstants.FALSE)
                         {
                              // -------------------------------------------------------------
                              // This only works if the ability is active, so no need to check here
                              // -------------------------------------------------------------
                              if (engine.Ability_DeactivateModalAbility(oCaster, nAbility, nAbilityType) == EngineConstants.COMMAND_RESULT_INVALID)
                              {
                                   engine.SetCommandResult(oCaster, EngineConstants.COMMAND_RESULT_INVALID);
                                   return;
                              }
                         }

                         // --------------------------------------- -------------------------
                         // Check the use conditions for the ability, e.g. melee only
                         // Later this should be
                         // done mostly in the UI (e.g. don't even allow to use it)
                         // --------------------------------------- -------------------------
                         if (engine.Ability_CheckUseConditions(oCaster, oTarget, nAbility, oItem) == EngineConstants.FALSE)
                         {
                              engine.UI_DisplayMessage(oCaster, EngineConstants.UI_MESSAGE_ABILITY_CONDITION_NOT_MET);
                              engine.DEBUG_PrintToScreen("Can not use this ability (conditions not met!)");
                              engine.SetCommandResult(oCaster, EngineConstants.COMMAND_RESULT_INVALID);

                              engine.PlaySoundSet(oCaster, EngineConstants.SS_CANNOT_DO);
                              return;
                         }

                         // --------------------------------------- --------------------------
                         // This handles special case talent stuff like animation syncronizing for shieldbash
                         // OPTIMIZATION_POTENTIAL: Flag spellscripts that need pending events to activate to save
                         // a bunch of instructions
                         // -----------------------------------------------------------------
                         xEvent evPending = engine.EventSpellScriptPending(oCaster, oTarget, nAbility, nAbilityType);

                         int nResult = engine.Ability_DoRunSpellScript(evPending, nAbility, nAbilityType);
                         if (nResult != EngineConstants.COMMAND_RESULT_SUCCESS)
                         {
                              // xCommand result is set by individual spellscript.

                              engine.SetCommandResult(oCaster, nResult);
                              return;
                         }

                         // -----------------------------------------------------------------
                         // Handle Ability Cost Check. We fail the ability if the player
                         // does not have the strings to use the ability.
                         // -----------------------------------------------------------------
                         if (engine.Ability_CostCheck(oCaster, nAbility, nAbilityType, oItem) == EngineConstants.FALSE)
                         {
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, sDebug, "insufficient strings (mana/stamina), will not cast");

                              if (nAbilityType == EngineConstants.ABILITY_TYPE_SPELL)
                              {
                                   engine.UI_DisplayMessage(oCaster, EngineConstants.UI_MESSAGE_OUT_OF_MANA);
                              }
                              else
                              {
                                   engine.UI_DisplayMessage(oCaster, EngineConstants.UI_MESSAGE_OUT_OF_STAMINA);
                              }

                              engine.SetCommandResult(oCaster, EngineConstants.COMMAND_RESULT_FAILED_NO_RESOURCES);
                              return;
                         }
                         else
                         {
                              // -------------------------------------------------------------
                              // Abilities that use the range weapon anim require an
                              // aim loop duration. This is set to 3.0f flat.
                              // -------------------------------------------------------------
                              if (engine.Ability_IsUsingRangedWeaponAnim(nAbility) != EngineConstants.FALSE)
                              {
                                   engine.SetAimLoopDuration(gameObject, 1.5f);
                              }
                              // -------------------------------------------------------------
                              // Melee Weapon abilities use weapon trails
                              // -------------------------------------------------------------
                              else if (engine.IsUsingMeleeWeapon(oCaster) != EngineConstants.FALSE)
                              {
                                   if (nAbilityType == EngineConstants.ABILITY_TYPE_TALENT)
                                   {
                                        // only when weapon is drawn
                                        if (engine.GetCombatState(oCaster) != EngineConstants.FALSE)
                                        {
                                             engine.EnableWeaponTrail(oCaster, EngineConstants.TRUE);
                                        }
                                   }
                              }
                              engine.SetCommandResult(oCaster, EngineConstants.COMMAND_RESULT_SUCCESS);
                         }

                         break;
                    } // case EngineConstants.EVENT_TYPE_COMMAND_PENDING end

               // ---------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_ABILITY_CAST_START
               // ---------------------------------------------------------------------
               // - Update mana/stamina
               // - Calculate *combat* attack/miss (only for hit based talents like Deadly Strike)
               // - Calculate resistance check/failure for non-AOE abilities (if valid)
               // - Calculate *combat* damage (only for hit based talents like Deadly Strike)
               // Most of the above information is then passed on to the engine who need to decide
               // what animations to play (or not to play). The engine then returns most of this
               // information along with the CAST_IMPACT xEvent back to this script (see below)
               // ---------------------------------------------------------------------

               case EngineConstants.EVENT_TYPE_ABILITY_CAST_START:
                    {

                         // -----------------------------------------------------------------
                         // Subtract Mana / Stamina cost from caster
                         // -----------------------------------------------------------------
                         if (engine.Ability_IsModalAbility(nAbility) == EngineConstants.FALSE)
                         {
                              engine.Ability_SubtractAbilityCost(oCaster, nAbility, oItem);
                         }
                         else if (engine.IsControlled(oCaster) == EngineConstants.FALSE)// modal - this cooldown is needed so the AI does not try to re-run this ability again
                                                                                                         // checking only engine.IsModalActive is not enough as the activation might come later down the xEvent queue
                              engine.Ability_SetCooldown(oCaster, nAbility, null, 1.5f);

                         // legacy, doesn't do anything.
                         int nResistanceCheckResult = EngineConstants.RESISTANCE_CHECK_FAILURE;
                         engine.UI_DisplayAbilityMessage(oCaster, nAbility);

                         //------------------------------------------------------------------
                         // Create and engine.Signal an xEvent to the spellscript
                         //------------------------------------------------------------------
                         xEvent evCast = engine.EventSpellScriptCast(oCaster, oTarget, nAbility, nAbilityType, nResistanceCheckResult);
                         engine.Ability_DoRunSpellScript(evCast, nAbility, nAbilityType);

                         break;
                    } // end of EngineConstants.COMMAND_PENDING case

               // ---------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_ABILITY_PROJECTILE_LAUNCHED
               // ---------------------------------------------------------------------
               // Fires the moment the projectile for an ability launches. Used
               // to set cooldown for projectile based abilities.
               // ---------------------------------------------------------------------
               case 93: /*EngineConstants.EVENT_TYPE_ABILITY_PROJECTILE_LAUNCHED*/
                    {
                         // Projectile abilities set their cooldown here. All other abilities
                         // set it on cast impact.

                         engine.Ability_SetCooldown(oCaster, nAbility, oItem);
                         break;
                    }

               // ---------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT
               // ---------------------------------------------------------------------
               // Fires for the moment of impact for every ability. This is where damage
               // should be applied, fireballs explode, enemies get poisoned etc'.
               // We assume that stamina/mana have been deducted when the ability was
               // triggered (EngineConstants.COMMAND_PENDING stage)
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT:
                    {
                         // read the following values from the event:
                         int nResistanceCheckResult = engine.GetEventIntegerRef(ref ev, 0);
                         int nCombatAttackResult = engine.GetEventIntegerRef(ref ev, 1);
                         int nHit = engine.GetEventIntegerRef(ref ev, 2);
                         int nHand = engine.GetEventIntegerRef(ref ev, 3);

                         // PATL 07-JAN-09 Do not allow the impact to occur from an item
                         // if the item is no longer valid.
                         if (oItem != null && engine.IsObjectValid(oItem) == EngineConstants.FALSE)
                         {
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_core.EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT", "Event Item: EngineConstants.INVALID");
                              return;
                         }

                         engine.UI_DisplayMessage(oCaster, EngineConstants.UI_DEBUG_EVENT_IMPACT_CAST);

                         // -----------------------------------------------------------------
                         // engine.Setcooldown for non modal abilities
                         //
                         // bHasProjectile is ignored for item abilities to fix cooldown errors
                         // -----------------------------------------------------------------
                         if (engine.Ability_IsModalAbility(nAbility) == EngineConstants.FALSE &&
                              (bHasProjectile == EngineConstants.FALSE || (engine.GetAbilityType(nAbility) == EngineConstants.ABILITY_TYPE_ITEM)))
                         {
                              engine.Ability_SetCooldown(oCaster, nAbility, oItem);

                              // Georg: Modal abilities deactivate their cooldown when their
                              //        effect_upkeep is unapplied (effect_upkeep_h.nss).
                         }

                         // break stealth when using an ability
                         if (engine.IsStealthy(oCaster) != EngineConstants.FALSE)
                         {
                              // ignore stealth dropping if its an item and you have stealth 2
                              float fRandom = engine.RandomFloat();
                              if ((engine.GetAbilityType(nAbility) != EngineConstants.ABILITY_TYPE_ITEM) || ((engine.HasAbility(oCaster, EngineConstants.ABILITY_SKILL_STEALTH_2) == EngineConstants.FALSE) || (fRandom < 0.1f)))
                              {
                                   // stealing is a special exception
                                   if (nAbility != EngineConstants.ABILITY_SKILL_STEALING_1)
                                   {
                                        engine.DropStealth(oCaster);
                                   }
                              }
                         }

                         engine.Log_Rules("GOT CAST_IMPACT: resistance: " + engine.IntToString(nResistanceCheckResult) +
                             ", attack result: " + engine.IntToString(nCombatAttackResult) + ", damage: " + engine.IntToString(0/*nCombatDamageResult*/),
                             EngineConstants.LOG_LEVEL_DEBUG, oCaster, oTarget);

                         Vector3 lTarget = engine.GetEventVectorRef(ref ev, 0);

                         /*if (engine.IsFollower(oCaster) != EngineConstants.FALSE)
                         {
                              //TrackPartyAbilityUse(nEventType, oCaster, oTarget, nAbility);
                         }
                         else if (engine.IsFollower(oTarget) != EngineConstants.FALSE && engine.IsFollower(oCaster) == EngineConstants.FALSE)
                         {
                              //TrackMonsterAbilityUse(nEventType, oCaster, oTarget, nAbility);
                         }*/

                         if (engine.IsLocationValid(lTarget) != EngineConstants.FALSE)
                         {

                              Vector3 v = engine.GetPositionFromLocation(lTarget);

                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_core.EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT",
                                        "Event Location: " + engine.ToString(v.x) + "," + engine.ToString(v.y));
                         }
                         else
                         {
                              // If we have no valid target location, but a valid target object, populate lTarget from
                              // the Vector3 of oTarget;
                              if (engine.IsObjectValid(oTarget) != EngineConstants.FALSE)
                              {
                                   lTarget = engine.GetLocation(oTarget);
                              }
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_core.EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT", "Event Location: EngineConstants.INVALID");
                         }

                         // -----------------------------------------------------------------
                         // Apply the Vector3 impact
                         // -----------------------------------------------------------------
                         engine.Ability_ApplyLocationImpactVFX(nAbility, lTarget);

                         xEvent evImpact = engine.EventSpellScriptImpact(oCaster, oTarget, nAbility, nAbilityType, nResistanceCheckResult, lTarget, nHit, nHand, oItem);
                         engine.Ability_DoRunSpellScript(evImpact, nAbility, nAbilityType);

                         // -----------------------------------------------------------------
                         // If item ability, remove one item from the stack. This needs to be the last thing
                         // in the script.
                         // -----------------------------------------------------------------
                         if ((engine.GetAbilityType(nAbility) == EngineConstants.ABILITY_TYPE_ITEM) && (nAbility != EngineConstants.ITEM_ABILITY_UNIQUE_POWER_UNLIMITED_USE) && (nAbility != EngineConstants.ITEM_ABILITY_KOLGRIMS_HORN))
                         {
                              engine.RemoveItem(oItem, 1);
                         }

                         break;
                    }
          }
     }
}