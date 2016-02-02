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

public class rules_core: MonoBehaviour
{
    // -----------------------------------------------------------------------------
    // rules_core.nss
    // -----------------------------------------------------------------------------
    //
    // This is the core creature xEvent scripts handling any xEvent not handled
    // in a previous script.
    //
    // It is the main entry point into combat AI, ability use and any xEvent that
    // is global to all creatures.
    //
    //
    // Event Chain:
    //                             / player_core (followers) \
    //      Custom creature script                            > rules_core
    //                             \ creature_core           /
    //
    //
    // Note: certain events bypass the chain and process directly in here:
    //
    //  EngineConstants.EFFECT_APPLIED
    //  EFECT_UNAPPLIED
    //  IMPACT
    //
    // -----------------------------------------------------------------------------
    // Owner: Georg Zoeller.
    // -----------------------------------------------------------------------------

    // -----------------------------------------------------------------------------
    //
    //             L I M I T E D   E D I T   P E R M I S S I O N S
    //
    //      If you are not Georg or Yaron, you need permission to edit this file
    //      and the changes have to be code reviewed before they are checked
    //      in.
    //
    // -----------------------------------------------------------------------------

    //#include"utility_h"
    //#include"rules_h"
    //#include"events_h"
    //#include"effects_h"
    //#include"ai_main_h_2"
    //#include"ui_h"
    //#include"sys_soundset_h"
    //#include"sys_stealth_h"
    //#include"sys_ambient_h"
    //#include"sys_itemprops_h"
    //#include"ability_h"
    //#include"approval_h"

    Engine engine { get; set; }

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     public void HandleEvent(xEvent ev)
     {

          //xEvent ev = engine.GetCurrentEvent();
          int nEventType = engine.GetEventTypeRef(ref ev);

          // -------------------------------------------------------------------------
          // Generic xEvent message
          // -------------------------------------------------------------------------
#if DEBUG
          engine.Log_Events("", ev);
#endif

          switch (nEventType)
          {
               // GM - Temp handling of RUBBER BAND event
               // --------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_RUBBER_BAND - Creature chased someone too far
               // --------------------------------------------------------------------
               case 87:
                    {
                         /*
                         ClearAllCommands(gameObject);
                         GameObject oArea = engine.GetArea(gameObject);
                         Vector3 vPos;
                         vPos.x = engine.GetLocalFloat(gameObject, "RUBBER_HOME_LOCATION_X");
                         vPos.y = engine.GetLocalFloat(gameObject, "RUBBER_HOME_LOCATION_Y");
                         vPos.z = engine.GetLocalFloat(gameObject, "RUBBER_HOME_LOCATION_Z");
                         Vector3 lLoc = Location(oArea, vPos, 0.0f);

                         xCommand cMove = engine.CommandMoveToLocation(lLoc);
                         engine.WR_AddCommand(gameObject, cMove);
                         DEBUG_PrintToScreen(engine.GetTag(gameObject) + "Reached Rubber Band Limit");
                         */
                         break;
                    }
               // GM - Temp handling of GIVE UP event
               // --------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_GIVE_UP - Creature chased someone for too long
               // --------------------------------------------------------------------
               case 88:
                    {
                         /*
                         ClearAllCommands(gameObject);
                         xCommand cWait = engine.CommandWait(2.0f);
                         engine.WR_AddCommand(gameObject, cWait);
                         DEBUG_PrintToScreen(engine.GetTag(gameObject) + " Giving Up");
                         */
                         break;
                    }
               // --------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_APPLY_EFFECT - Effect being applied
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_APPLY_EFFECT:
                    {
                         engine.Effects_HandleApplyEffect();

                         break;
                    }

               case 90210:
                    {
                         engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_ABILITY_CORE);
                         break;
                    }

               // --------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_APPLY_EFFECT - Effect being unapplied
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_REMOVE_EFFECT:
                    {

                         engine.Effects_HandleRemoveEffect();
                         break;
                    }

               // --------------------------------------------------------------------
               // EngineConstants.COMMAND_PENDING - Deal with pending commands issued either by the AI
               //                   or by player input
               // Params:
               //          obj(0)  - engine.Command Owner
               //          obj(1)  - engine.Command Target
               //          obj(2)  - varies by command.
               //          int(0)  - The xCommand id (EngineConstants.COMMAND_TYPE_* constant)
               //          int(1)  - The xCommand subtype:
               //                    EngineConstants.COMMAND_TYPE_ATTACK  - # of attacks
               //                    EngineConstants.COMMAND_TYPE_ABILITY - EngineConstants.ABILITY_TYPE constant *
               //
               //  Note: Ranged Attacks always send one attack, never two.
               //
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_COMMAND_PENDING:
                    {

                         GameObject oCommandOwner = engine.GetEventObjectRef(ref ev, 0);
                         GameObject oTarget = engine.GetEventObjectRef(ref ev, 1);
                         int nCommandId = engine.GetEventIntegerRef(ref ev, 0);
                         int nCommandSubType = engine.GetEventIntegerRef(ref ev, 1);

                         // -----------------------------------------------------------------
                         // Validate the xEvent (target ok? user alife, etc.)
                         // -----------------------------------------------------------------
                         if (engine.Events_ValidateCommandPending(oCommandOwner, oTarget, nCommandId, nCommandSubType) == EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Discarding EngineConstants.EVENT_TYPE_COMMAND_PENDING (" + engine.ToString(nCommandId) + ")");
#endif
                              return;
                         }

                         // -----------------------------------------------------------------
                         // Melee Attack...
                         // -----------------------------------------------------------------
                         if (nCommandId == EngineConstants.COMMAND_TYPE_ATTACK)
                         {

                              if (engine.GetCombatantType(oTarget) == EngineConstants.CREATURE_TYPE_NON_COMBATANT)
                              {
                                   engine.Warning("Noncombatant Creature " + engine.ToString(oTarget) + " is target of an attack command! Pause and get Georg or Yaron");
                              }
                              // Flagging party as clear to attack (if controlled follower)
                              if (engine.IsControlled(gameObject) != EngineConstants.FALSE && engine.IsObjectValid(oTarget) != EngineConstants.FALSE && engine.IsObjectHostile(gameObject, oTarget) != EngineConstants.FALSE)
                              {
                                   engine.AI_SetPartyAllowedToAttack(EngineConstants.TRUE);
#if DEBUG
                                   engine.Log_Trace_AI("rules_core.EngineConstants.EVENT_TYPE_COMMAND_PENDING", "Controlled follower attacking - clearing party to attack");
#endif
                              }

                              int nCommandResult =  engine.Combat_HandleCommandAttack(oCommandOwner, oTarget, nCommandSubType);

                              // -------------------------------------------------------------
                              // Trigger a battle cry
                              // -------------------------------------------------------------
                              if (nCommandResult == EngineConstants.COMMAND_RESULT_SUCCESS)
                              {
                                   engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_COMBAT_BATTLECRY);
                              }

                              // -------------------------------------------------------------
                              // Send the xCommand result to the engine, this starts the
                              // xCommand execution.
                              // -------------------------------------------------------------
                              engine.SetCommandResult(oCommandOwner, nCommandResult);
                         }
                         // -----------------------------------------------------------------
                         // Abilities, Spells
                         // -----------------------------------------------------------------
                         else if (nCommandId == EngineConstants.COMMAND_TYPE_USE_ABILITY)
                         {
                              // Flagging party as clear to attack (if controlled follower)
                              if (engine.IsControlled(gameObject) != EngineConstants.FALSE && engine.IsObjectValid(oTarget) != EngineConstants.FALSE && engine.IsObjectHostile(gameObject, oTarget) != EngineConstants.FALSE)
                              {
#if DEBUG
                                   engine.Log_Trace_AI("rules_core.EngineConstants.EVENT_TYPE_COMMAND_PENDING", "Controlled follower using hostile ability - clearing party to attack");
#endif
                                   engine.AI_SetPartyAllowedToAttack(EngineConstants.TRUE);
                              }

                              // -------------------------------------------------------------
                              // Redirect into ability_core
                              // -------------------------------------------------------------
                              engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_ABILITY_CORE);
                         }
                         else
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_GENERAL, "rules_core.EngineConstants.EVENT_TYPE_COMMAND_PENDING ",
                                                             "Unknown EngineConstants.COMMAND_TYPE " + engine.IntToString(nCommandId) + " received, ignoring", oTarget);
#endif
                         }

                         break;
                    }

               // ---------------------------------------------------------------------
               // Georg: relevant eclipse engine code
               /*
                   pAttackerOnHitEvent->SetCreator(nAttackerId);
                   pAttackerOnHitEvent->SetObjectId(0, nAttackerId);
                   pAttackerOnHitEvent->SetInteger(0, pAttackerItem->engine.GetOnHitEffectId());
                   pAttackerOnHitEvent->SetInteger(1, pAttackerItem->engine.GetOnHitPower());

                   pAttackerOnHitEvent->SetObjectId(1, pAttackerItem->engine.GetId());
                   pAttackerOnHitEvent->SetTarget(a_pEventData->m_nTargetId);
               */
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_ITEM_ONHIT:
                    {
                         // -----------------------------------------------------------------
                         // This xEvent is only ever processed in EngineConstants.GM_COMBAT
                         // note: That means that NPCs fighting NPCs while not visible to
                         //       the player (shouldn't ever happen anyway) will not trigger
                         //       OnHit item abilities.
                         // -----------------------------------------------------------------
                         if (engine.GetGameMode() == EngineConstants.GM_COMBAT)
                         {
                              int nOnHitEffectId = engine.GetEventIntegerRef(ref ev, 0);
                              int nForceProc = engine.GetEventIntegerRef(ref ev, 2);

                              // -----------------------------------------------------------------
                              // First determine proc chance. If no proc, no point in wasting
                              // cpu time on the rest
                              // -----------------------------------------------------------------
                              float fProc = engine.GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "ProcChance", nOnHitEffectId);
                              if (engine.RandomFloat() < fProc || nForceProc > 0)
                              {
                                   GameObject oAttacker = engine.GetEventCreatorRef(ref ev);
                                   // check to see if the character is in a shapeshifted form
                                   // this prevents a rat from having a huge stack of damage floaties from equipment
                                   if (engine.IsShapeShifted(oAttacker) == EngineConstants.FALSE)
                                   {
                                        GameObject oTarget = engine.GetEventTargetRef(ref ev);
                                        GameObject oItem = engine.GetEventObjectRef(ref ev, 1);
                                        int nPower = 1;
                                        if (engine.IsObjectValid(oItem) != EngineConstants.FALSE)
                                        {
                                             nPower = Convert.ToInt32(engine.GetItemPropertyPower(oItem, nOnHitEffectId, EngineConstants.TRUE));
                                        }

                                        // -------------------------------------------------------------
                                        // No care about dead targets or non creatures
                                        // -------------------------------------------------------------
                                        if (engine.GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE && engine.IsDeadOrDying(oTarget) == EngineConstants.FALSE)
                                        {
                                             engine.ItemProp_DoEffect(oAttacker, oTarget, nOnHitEffectId, nPower);
                                        }
                                   }
                              }
                         }

                         break;
                    }

               case EngineConstants.EVENT_TYPE_DAMAGED:
                    {
                         // This GameObject lost 1 hit point or more

                         if (engine.IsDeadOrDying(gameObject) != EngineConstants.FALSE)
                         {
                              return;
                         }

                         int bSound = EngineConstants.TRUE;

                         GameObject oDamager = engine.GetEventCreatorRef(ref ev);
                         float fDamage = engine.GetEventFloatRef(ref ev, 0);
                         int nDamageType = engine.GetEventIntegerRef(ref ev, 0);
                         int nAbility = engine.GetEventIntegerRef(ref ev, 1);

                         if (engine.IsFollower(gameObject) == EngineConstants.FALSE)
                              engine.AI_Threat_UpdateDamage(gameObject, oDamager, fDamage);

                         if (engine.IsStealthy(gameObject) != EngineConstants.FALSE && nDamageType != EngineConstants.DAMAGE_TYPE_PHYSICAL)
                         {
                              // stealth 3 and 4 have a chance to not drop on non-physical damage
                              int nLevel = engine.GetLevel(gameObject);
                              float fChance = -1.0f;
                              if (engine.HasAbility(gameObject, EngineConstants.ABILITY_SKILL_STEALTH_4) != EngineConstants.FALSE)
                              {
                                   fChance = nLevel * 0.02f;
                              }
                              else if (engine.HasAbility(gameObject, EngineConstants.ABILITY_SKILL_STEALTH_3) != EngineConstants.FALSE)
                              {
                                   fChance = nLevel * 0.01f;
                              }
                              fChance = engine.MinF(fChance, 0.8f);

                              if (engine.RandomFloat() > fChance)
                              {
                                   engine.DropStealth(gameObject);
                              }
                         }

                         // If not a follower and damaged outside of combat and does not perceive the damager
                         // then try to move to damager.
                         // the following check needs to be outside of gamemode=combat check because it can happen when the gamemode is not combat
                         // for example: around a corner where no one perceives each other yet
                         if (engine.GetObjectType(oDamager) == EngineConstants.OBJECT_TYPE_CREATURE &&
                             engine.GetCombatState(gameObject) == EngineConstants.FALSE && engine.IsFollower(gameObject) == EngineConstants.FALSE && engine.IsPerceiving(gameObject, oDamager) == EngineConstants.FALSE)
                         {
                              Vector3 lLoc = engine.GetLocation(oDamager);
                              engine.WR_ClearAllCommands(gameObject, EngineConstants.TRUE);
                              xCommand cMove = engine.CommandMoveToLocation(lLoc, EngineConstants.TRUE);
                              engine.WR_AddCommand(gameObject, cMove);
                         }

                         // -----------------------------------------------------------------
                         // Attack Interruption
                         // This should only ever happen in combat.
                         // @author georg
                         // -----------------------------------------------------------------
                         if (engine.GetGameMode() == EngineConstants.GM_COMBAT)
                         {
                              // -------------------------------------------------------------
                              // Only significant damage disrupts
                              // -------------------------------------------------------------
                              if (nDamageType == EngineConstants.DAMAGE_TYPE_PHYSICAL)
                              {

                                   xCommand cmd = engine.GetCurrentCommand(gameObject);
                                   int nCmdType = engine.GetCommandType(cmd);

                                   // ---------------------------------------------------------
                                   // We only interrupt attack commands at this point
                                   // ---------------------------------------------------------
                                   if (nCmdType == EngineConstants.COMMAND_TYPE_ATTACK)
                                   {
                                        //---------------------------------------------------------
                                        // Damage needs to exceed dexterity modifier/3 to interrupt
                                        //----------------------------------------------------------
                                        float fModifier = engine.GetAttributeModifier(gameObject, EngineConstants.ATTRIBUTE_DEX) * (1.0f / 3.0f);
                                        if (fDamage > fModifier)
                                        {

                                             // -------------------------------------------------
                                             // Melee archers ignore interruptions
                                             // -------------------------------------------------
                                             if (engine.HasAbility(gameObject, EngineConstants.ABILITY_TALENT_MELEE_ARCHER) == EngineConstants.FALSE)
                                             {

                                                  if (engine.IsUsingMeleeWeapon(oDamager, null) != EngineConstants.FALSE)
                                                  {
                                                       if (engine.IsUsingRangedWeapon(gameObject, null, EngineConstants.TRUE) != EngineConstants.FALSE)
                                                       {
                                                            engine.UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_INTERRUPTED);
#if DEBUG
                                                            engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "rules_core.OnDamage", "Ranged attack interrupted by damage");
#endif
                                                            engine.WR_ClearAllCommands(gameObject, EngineConstants.TRUE);
                                                       }
                                                  }

                                             }
                                        }
                                   }
                                   // ---------------------------------------------------------
                                   // Experimental: Taking any damage > 1 causes spell interruption
                                   // ---------------------------------------------------------
                                   else if (nCmdType == EngineConstants.COMMAND_TYPE_USE_ABILITY)
                                   {

                                        int nAbi = engine.GetCommandIntRef(ref cmd, 0);

                                        // Only abilities with speed >0 can be interrupted.
                                        if (engine.CanInterruptSpell(nAbi) != EngineConstants.FALSE)
                                        {

                                             int nCombatTrainingRank = 0;
                                             int nModifier = 0;
                                             float fThres = 0.0f;

                                             // -----------------------------------------------------

                                             if (engine.IsPartyMember(gameObject) == EngineConstants.FALSE)
                                             {
                                                  nCombatTrainingRank = engine.GetM2DAInt(engine.Diff_GetAutoScaleTable(), "nCombatTraining", engine.GetCreatureRank(gameObject));
                                                  nModifier = engine.GetLevel(gameObject);
                                             }
                                             else
                                             {
                                                  nCombatTrainingRank = (engine.HasAbility(gameObject, EngineConstants.ABILITY_SKILL_COMBAT_TRAINING_1)) + (engine.HasAbility(gameObject, EngineConstants.ABILITY_SKILL_COMBAT_TRAINING_2)) + (engine.HasAbility(gameObject, EngineConstants.ABILITY_SKILL_COMBAT_TRAINING_3)) + (engine.HasAbility(gameObject, EngineConstants.ABILITY_SKILL_COMBAT_TRAINING_4));
                                                  nModifier = 5 + engine.GetLevel(gameObject) / 2;
                                             }

                                             fThres = (nCombatTrainingRank * 10.0f) + nModifier;

                                             // -----------------------------------------------------
                                             // Damage needs to exceed a certain threshold before it
                                             // can interrupt. Otherwise playing mages gets very
                                             // frustrating.
                                             // -----------------------------------------------------
                                             if (fDamage > fThres)
                                             {
                                                  // ---------------------------------------------
                                                  // Since EngineConstants.COMMAND_USEABILITY can be in a movement
                                                  // subaction, filter additionally for conjure
                                                  // phase.
                                                  // ---------------------------------------------
                                                  if (engine.IsConjuring(gameObject) != EngineConstants.FALSE)
                                                  {
                                                       engine.UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_INTERRUPTED);
#if DEBUG
                                                       engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "rules_core.OnDamage", "Spell interrupted by damage");
#endif

                                                       engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_SPELL_INTERRUPTED);
                                                       bSound = EngineConstants.FALSE;
                                                       engine.WR_ClearAllCommands(gameObject, EngineConstants.TRUE);
                                                  }
                                             }
                                             else
                                             {
#if DEBUG
                                                  engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "rules_core.OnDamage", "Spell not interrupted, dmg " + engine.ToString(fDamage) + " below threshold: " + engine.ToString(fThres));
#endif
                                             }
                                        }
                                   }
                              }

                              if (fDamage >= EngineConstants.SOUND_THRESH_DAMAGE_AMOUNT && bSound != EngineConstants.FALSE)
                              {
                                   engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_GOT_DAMAGED, oDamager);
                              }

                              // -------------------------------------------------------------
                              // engine.Handle various effects
                              // -------------------------------------------------------------
                              engine.Ability_HandleOnDamageAbilities(gameObject, oDamager, fDamage, nDamageType, nAbility);

                         }

                         break;
                    }

               // ---------------------------------------------------------------------
               // Object is spawned.  Fires once per object
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_SPAWN:
                    {
                         // The GameObject spawned into the game.
                         int nSpawned = engine.GetLocalInt(gameObject, EngineConstants.CREATURE_SPAWNED);
                         if (nSpawned == 1)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "rules_core.EngineConstants.EVENT_TYPE_SPAWN", "creature spawned before - NOT triggering spawn routine again.");
#endif
                         }
                         else
                              engine.SetLocalInt(gameObject, EngineConstants.CREATURE_SPAWNED, 1);

                         // Enable/disable lookat
                         engine.SetLookAtEnabled(gameObject, engine.GetLocalInt(gameObject, EngineConstants.LOOKAT_DISABLED) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE);

                         break;
                    }

               // ---------------------------------------------------------------------
               // An attack has impacted the target. This can be melee (sword hit),
               // ranged (arrow hit) or spell (fireball explodes). Used for applying damage
               // and handling abilities that need to do something on hit (Berserk etc').
               //
               // Here we do the actual damage for successful attacks. The damage is
               // being sent by this event, but it has been passed through from
               // scripting in EngineConstants.COMMAND_PENDING
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_ATTACK_IMPACT:
                    {

                         GameObject oAttacker = engine.GetEventObjectRef(ref ev, 0);
                         GameObject oTarget = engine.GetEventObjectRef(ref ev, 1);

                         int nHitResult = engine.GetEventIntegerRef(ref ev, 0);
                         int nEffectId = engine.GetEventIntegerRef(ref ev, 1);
                         xEffect eImpactEffect = engine.GetAttackImpactDamageEffect(oAttacker, nEffectId);

                         engine.UI_DisplayMessage(oAttacker, EngineConstants.UI_DEBUG_EVENT_IMPACT_ATTACK);

                         // -----------------------------------------------------------------
                         // //Track any combat impact by the player.
                         // This is potentially expensive, so it's disabled until georg
                         // has evaluated if the server can handle it.
                         // -----------------------------------------------------------------
                         if (engine.IsFollower(oAttacker) != EngineConstants.FALSE)
                         {
                              /*if (TRACKING_TRACK_COMBAT_IMPACT)
                              {
#if SKYNET
                    //TrackCombatEvent(nEventType, oAttacker, oTarget, nHitResult,  0);
#endif
                              }*/
                         }

                         // -----------------------------------------------------------------
                         // Check for dead attacker, etc, etc, to invalidate events that
                         // should no longer be processed.
                         // -----------------------------------------------------------------
                         if (engine.Events_FilterAttackImpactEvent(oAttacker, oTarget, nHitResult) == EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Discarding EngineConstants.EVENT_TYPE_ATTACK_IMPACT", oTarget);
#endif
                              return;
                         }

                         // -----------------------------------------------------------------
                         // Signal an OnAttacked result to the attacked creature
                         // -----------------------------------------------------------------
                         engine.SendEventOnAttacked(oTarget, oAttacker);

                          engine.Combat_HandleAttackImpact(oAttacker, oTarget, nHitResult, eImpactEffect);

                         break;
                    }

               // ---------------------------------------------------------------------
               // Equip Event
               // EventCreator - The creature owning the item
               // obj(0)       - The item.
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_UNEQUIP:
               case EngineConstants.EVENT_TYPE_EQUIP:
                    {

                         GameObject oItem = engine.GetEventObjectRef(ref ev, 0);
                         GameObject oUser = engine.GetEventCreatorRef(ref ev);
                         int nSlot = engine.GetEventIntegerRef(ref ev, 0);
                         if (engine.IsPartyMember(oUser) != EngineConstants.FALSE)
                         {
                              engine.RecalculateDisplayDamage(gameObject);
                         }

                         break;
                    }

               case EngineConstants.EVENT_TYPE_CAST_AT:
                    {
                         Engine.EventOnCastAtParamStruct stEvent = engine.GetEventOnCastAtParams(ev);

                         // Updating ability-use threat. This value applies threat when specific abilities are
                         // used regardless of who the target is. For example: Berserk, buffing self or allies etc'
                         //AI_Threat_UpdateAbilityUsed(stEvent.oCaster, stEvent.nAbility);

                         if (stEvent.bHostile != EngineConstants.FALSE)
                         {
                              // -----------------------------------------------------------------
                              // Core engine functionality, do not mess with these lines
                              // Adding oAttacker to oTarget's perception list and vice versa
                              // -----------------------------------------------------------------
                              if (engine.IsPerceiving(gameObject, stEvent.oCaster) == EngineConstants.FALSE)
                              {
                                   engine.WR_TriggerPerception(gameObject, stEvent.oCaster);
                                   engine.WR_TriggerPerception(stEvent.oCaster, gameObject);
                              }

                              // IMPORTANT: it is assumed this works for talents impact as well!
                              // This threat is generated for hostile ability effects like paralyze or disease.
                              if (engine.IsFollower(gameObject) == EngineConstants.FALSE)
                                   engine.AI_Threat_UpdateAbilityImpact(gameObject, stEvent.oCaster, stEvent.nAbility);
                         }
                         //else // non-hostile
                         //{
                         //    if(stEvent.fParam1 > 0.0f) // for now - this is used ONLY for damage healed
                         //        engine.AI_Threat_UpdateHealing(gameObject, stEvent.oCaster, stEvent.fParam1);
                         //}
                         break;
                    }
               case EngineConstants.EVENT_TYPE_ATTACKED:
                    {
                         GameObject oAttacker = engine.GetEventObjectRef(ref ev, 0);
                         // -----------------------------------------------------------------
                         // Core engine functionality, do not mess with these lines
                         // Adding oAttacker to oTarget's perception list and vice versa
                         // -----------------------------------------------------------------

                         if (engine.IsStealthy(gameObject) == EngineConstants.FALSE)
                         {
                              engine.WR_TriggerPerception(gameObject, oAttacker);
                              engine.WR_TriggerPerception(oAttacker, gameObject);
                         }

                         // -----------------------------------------------------------------
                         // Brute force fix for any lingering game mode issues:
                         // If we are attacked by a hostile GameObject in explore mode
                         // and we are a follower, we force the game into combat mode.
                         // -----------------------------------------------------------------
                         if (engine.GetGameMode() == EngineConstants.GM_EXPLORE)
                         {
                              if (engine.IsFollower(gameObject) != EngineConstants.FALSE && engine.IsObjectHostile(gameObject, oAttacker) != EngineConstants.FALSE && engine.GetCombatState(gameObject) == EngineConstants.FALSE)
                              {
                                   if (engine.GetObjectType(oAttacker) == EngineConstants.OBJECT_TYPE_CREATURE && engine.IsPerceiving(gameObject, oAttacker) != EngineConstants.FALSE)
                                        engine.WR_SetGameMode(EngineConstants.GM_COMBAT);
                              }
                         }

                         if (engine.IsFollower(gameObject) == EngineConstants.FALSE)
                              engine.AI_Threat_UpdateEnemyAttacked(gameObject, oAttacker);

                         xCommand cCurrent = engine.GetCurrentCommand(gameObject);

                         // Disable stationary state (only if 'soft' state)
                         // If a creature is attacked we don't want him anymore to stand and wait for enemy to be in range -
                         // at this stange we'll allow him to chase enemies

                         if (engine.GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY) == EngineConstants.AI_STATIONARY_STATE_SOFT)
                              engine.SetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY, EngineConstants.AI_STATIONARY_STATE_DISABLED);

                         // A follower will be clear to attack enemies back once attacked himself (or once another follower was attacked)
                         // (or when the controlled follower attacks - handles elsewhere)
                         if (engine.IsFollower(gameObject) != EngineConstants.FALSE)
                              engine.AI_SetPartyAllowedToAttack(EngineConstants.TRUE);

                         // For ANY party member:
                         // If in combat - run DetermineCombatRound_Partial if I'm doing nothing
                         // This is done in order to have the player attack back someone who attack him
                         if (engine.IsFollower(gameObject) != EngineConstants.FALSE && engine.GetCombatState(gameObject) != EngineConstants.FALSE && engine.GetCommandType(cCurrent) == EngineConstants.COMMAND_TYPE_INVALID)
                         {
                              if (engine.GetLocalInt(gameObject, EngineConstants.AI_CUSTOM_AI_ACTIVE) != EngineConstants.FALSE) // custom AI active
                              {
#if DEBUG
                                   engine.Log_Trace_AI("rules_core", "Executing CUSTOM AI");
#endif
                                   engine.SendEventHandleCustomAI(gameObject, oAttacker, -1, EngineConstants.COMMAND_RESULT_SUCCESS, -1); //
                              }
                              else
                              {
#if DEBUG
                                   engine.Log_Trace_AI("rules_core.EngineConstants.EVENT_TYPE_ATTACKED", "calling DetermineCombatRound from the EngineConstants.ATTACKED event");
#endif
                                   engine.AI_DetermineCombatRound(oAttacker, -1, EngineConstants.COMMAND_RESULT_SUCCESS, -1);
                              }
                         }

                         break;
                    }

               // ---------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_COMMAND_COMPLETE:
               // Called after a xCommand has finished executing.
               //
               // Params:
               //      int(0) - Type of the last xCommand (e.g. EngineConstants.COMMAND_TYPE_ATTACKED)
               //      int(1) - The status of the execution (EngineConstants.COMMAND_SUCCESSFUL, etc)
               //      obj(0) - The target that xCommand was applied to
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_COMMAND_COMPLETE:
                    {
                         int nLastCommandType = engine.GetEventIntegerRef(ref ev, 0);
                         int nCommandStatus = engine.GetEventIntegerRef(ref ev, 1);
                         int nLastSubCommand = engine.GetEventIntegerRef(ref ev, 2);
                         GameObject oLastTarget = null;
                         GameObject oBlockingObject = engine.GetEventObjectRef(ref ev, 2);

                         if (nLastCommandType == 0)
                         {
#if DEBUG
                              engine.Log_Trace_Scripting_Error("rules_core", "Invalid xCommand complete received from engine. Contact Yaron");
#endif
                              return;
                         }

                         if (nLastCommandType == EngineConstants.COMMAND_TYPE_USE_ABILITY)
                         {
                              engine.EnableWeaponTrail(gameObject, EngineConstants.FALSE);
                         }

                         if (engine.IsDisabled() != EngineConstants.FALSE || engine.IsDead() != EngineConstants.FALSE || engine.IsDying() != EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), " discardng EngineConstants.EVENT_TYPE_COMMAND_COMPLETE (disabled, dead, or dying)");
#endif
                              return;
                         }

                         // If this GameObject has a roam distance defined check if we
                         // are close enough to the roam point to remove the speed
                         // up and clear this creature's perception list.
                         float fRoamDistance = engine.GetLocalFloat(gameObject, "ROAM_DISTANCE");
                         if (fRoamDistance > 25.0f)
                         {
                              if (nLastCommandType == EngineConstants.COMMAND_TYPE_MOVE_TO_LOCATION)
                              {
                                   if (engine.GetHasEffects(gameObject, EngineConstants.EFFECT_TYPE_MOVEMENT_RATE, 0) != EngineConstants.FALSE)
                                   {
                                        if (engine.GetDistanceBetweenLocations(engine.GetRoamLocation(gameObject), engine.GetLocation(gameObject)) < 3.0f)
                                        {
                                             //DisplayFloatyMessage(gameObject,"BACK HOME!YAY!");
                                             engine.RemoveEffectsByParameters(gameObject, EngineConstants.EFFECT_TYPE_MOVEMENT_RATE, 0, gameObject);
                                             engine.SetCombatState(gameObject, EngineConstants.FALSE);
                                             engine.ClearPerceptionList(gameObject);
                                             return;
                                        }
                                   }
                              }
                         }

                         engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_COMMAND_COMPLETE);

                         // In case something interactive (e.g. a door) was blocking the path
                         // for the command, a solution is found here and immediately queued up.
                         // After completing this intermediate action, the AI can resume normal execution.
                         if (nCommandStatus == EngineConstants.COMMAND_FAILED_PATH_ACTION_REQUIRED)
                         {
                              int bAllowPathAction = (engine.IsControlled(gameObject) != EngineConstants.FALSE || engine.IsFollower(gameObject) == EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
                              if (bAllowPathAction != EngineConstants.FALSE &&
                                   engine.GetLocalInt(gameObject, EngineConstants.AI_DISABLE_PATH_BLOCKED_ACTION) == EngineConstants.FALSE)
                              {
                                   if (engine.AI_DeterminePathBlockedAction(oBlockingObject) != EngineConstants.FALSE)
                                        return;
                              }
                              else
                              {
#if DEBUG
                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_AI, engine.GetCurrentScriptName(), "Not handling Path-blocked xEvent (follower or has flag to disable this AI)");
#endif
                              }

                         }

                         // Determine what to do for next round.
                         // Behaviour depends on GameObject type and can execute outside combat.
                         // For party members this should run every time a xCommand ends.
                         // For normal creatures this should run only during combat.
                         if (engine.IsFollower(gameObject) != EngineConstants.FALSE ||
                              (engine.IsFollower(gameObject) == EngineConstants.FALSE && engine.GetCombatState(gameObject) != EngineConstants.FALSE))
                         {
                              if (nLastCommandType == EngineConstants.COMMAND_TYPE_ATTACK || nLastCommandType == EngineConstants.COMMAND_TYPE_USE_ABILITY)
                                   oLastTarget = engine.GetEventObjectRef(ref ev, 1);

#if DEBUG
                              if (engine.GetCreatureRank(gameObject) == EngineConstants.CREATURE_RANK_INVALID)
                                   engine.Log_Trace_Scripting_Error(engine.GetCurrentScriptName(), engine.ToString(gameObject) + " has no RANK");
#endif

                              if (engine.GetLocalInt(gameObject, EngineConstants.AI_CUSTOM_AI_ACTIVE) != EngineConstants.FALSE) // custom AI active
                              {
#if DEBUG
                                   engine.Log_Trace_AI("rules_core", "Executing CUSTOM AI");
#endif
                                   engine.SendEventHandleCustomAI(gameObject, oLastTarget, nLastCommandType, nCommandStatus, nLastSubCommand);
                              }
                              else
                              {
                                   engine.AI_DetermineCombatRound(oLastTarget, nLastCommandType, nCommandStatus, nLastSubCommand);
                                   break;
                              }
                         }

                         // Ambient behaviour
                         engine.Ambient_CommandComplete(nLastCommandType, nCommandStatus);

                         // Adding this so people can have a SAFE substitute for command_complete when nothing else works
                         if (engine.GetCombatState(gameObject) == EngineConstants.FALSE &&
                              engine.GetLocalInt(gameObject, EngineConstants.AI_CUSTOM_AI_ACTIVE) != EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace_AI("rules_core", "Executing CUSTOM COMMAND COMPLETE");
#endif
                              xEvent evCustomCommandComplete = engine.Event(EngineConstants.EVENT_TYPE_CUSTOM_COMMAND_COMPLETE);
                              engine.SetEventIntegerRef(ref evCustomCommandComplete, 0, nLastCommandType);
                              engine.SetEventIntegerRef(ref evCustomCommandComplete, 1, nCommandStatus);
                              engine.SetEventIntegerRef(ref evCustomCommandComplete, 2, nLastSubCommand);
                              engine.SetEventObjectRef(ref evCustomCommandComplete, 2, oBlockingObject);

                              engine.SignalEvent(gameObject, evCustomCommandComplete);
                         }

                         // handle creatures that should run away from hostiles (for example: city elf servant escorting the player)
                         if (engine.GetPackageAI(gameObject) == 10130) // coward package
                              engine.AI_HandleCowardFollower();

                         break;
                    }

               case EngineConstants.EVENT_TYPE_PERCEPTION_APPEAR:
                    {
                         GameObject oAppear = engine.GetEventObjectRef(ref ev, 0);
                         int nHostile = engine.GetEventIntegerRef(ref ev, 0); // if hostile or not (EngineConstants.TRUE/EngineConstants.FALSE)
                         int bStealthed = engine.GetEventIntegerRef(ref ev, 1); // if stealthed or not (EngineConstants.TRUE/EngineConstants.FALSE)
                         int nHostilityChanged = engine.GetEventIntegerRef(ref ev, 2); // if the creature was already perceived but just changed hostility (EngineConstants.TRUE/EngineConstants.FALSE)
                         int nSize;
                         int i;
                         GameObject oAlly;
                         float fCombatDelay; // before starting combat;
                         int nRand;

                         // If it's a hostile creature lying on the ground and it perceives a party member
                         // then it spawn as non-hostile. Need to turn him hostile now
                         if (engine.GetLocalInt(gameObject, EngineConstants.SPAWN_HOSTILE_LYING_ON_GROUND) == 1 && engine.GetGroupId(gameObject) == EngineConstants.GROUP_HOSTILE_ON_GROUND)
                         {
                              if (engine.GetGroupId(oAppear) == EngineConstants.GROUP_PC)
                              {
                                   engine.UT_CombatStart(gameObject, oAppear);
                                   engine.SetLocalInt(gameObject, EngineConstants.SPAWN_HOSTILE_LYING_ON_GROUND, 0); // disable it for next perception
                              }
                         }

                         //if it is a creature that has its GO_HOSTILE_ON_PERCEIVE_PC variable set - go hostile and attack
                         if (engine.GetLocalInt(gameObject, EngineConstants.GO_HOSTILE_ON_PERCEIVE_PC) == 1)
                         {
                              if (engine.GetGroupId(oAppear) == EngineConstants.GROUP_PC)
                              {
#if DEBUG
                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PERCEPTION, "rules_core", "PC Group Perceived - Changing from Neutral to Hostile");
#endif
                                   //Do Once Only
                                   engine.SetLocalInt(gameObject, EngineConstants.GO_HOSTILE_ON_PERCEIVE_PC, 0);
                                   //Go hostile
                                   engine.UT_CombatStart(gameObject, oAppear);
                              }

                         }

                         if (bStealthed != EngineConstants.FALSE)
                         {
                              // -------------------------------------------------------------
                              // Stealth Check
                              // -------------------------------------------------------------
                              if (engine.IsFollower(oAppear) != EngineConstants.FALSE && engine.Stealth_CheckSuccess(oAppear, gameObject) == EngineConstants.FALSE)
                              {
                                   //WR_TriggerPerception(gameObject,oAppear);
                                   engine.DropStealth(oAppear);
#if DEBUG
                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PERCEPTION, "rules_core", "Stealth Check failed!");
#endif
                              }
                              else
                              {
#if DEBUG
                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PERCEPTION, "rules_core", "Stealth Check Success!");
#endif
                              }

                              return;
                         }

                         if (nHostilityChanged != EngineConstants.FALSE)
                         {
                              //re-check hostility between self and creature.
                              //The nHostile flag will not be correct in this case since the engine set the flag before the
                              //xEvent was set. By the time we get the xEvent creature hostility towards each other will be different
                              if (engine.IsObjectHostile(gameObject, oAppear) != EngineConstants.FALSE)
                                   nHostile = EngineConstants.TRUE;
                              else
                                   nHostile = EngineConstants.FALSE;
                         }

                         // If the hostility changed, and the GameObject is not hostile, it means the GameObject was hostile and turned
                         // non-hostile. This means that we need to run some combat-stopping logic that is usually run on a
                         // perception disappear event, so we could remove this creature from combat state if needed.
                         if (nHostilityChanged != EngineConstants.FALSE && nHostile == EngineConstants.FALSE)
                         {
                               engine.Combat_HandleCreatureDisappear(gameObject, oAppear);
                         }

                         // engine.Handle shouts
                         // Triggering when perceiving a follower
                         // NOTE: this needs to be triggered when triggering a follower since if the check was for "controlled"
                         // the player might switch control and confuse the system
                         if (engine.IsFollower(oAppear) != EngineConstants.FALSE && engine.UT_GetShoutsFlag(gameObject) != EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Perceived party member: Activating shouts");
#endif
                              engine.SendEventOnDelayedShout(gameObject); // This will start the delayed xEvent loop
                         }

                         if (nHostile != EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Perceived hostile: " + engine.GetTag(oAppear) + ", my combat state= " + engine.IntToString(engine.GetCombatState(gameObject)));
#endif

                              if (engine.IsFollower(gameObject) == EngineConstants.FALSE)
                              {
                                   engine.AI_Threat_UpdateEnemyAppeared(gameObject, oAppear);
                              }

                              if (engine.GetCombatState(gameObject) == EngineConstants.FALSE)
                              {
#if DEBUG
                                   engine.Log_Trace_AI("rules_core.EngineConstants.EVENT_TYPE_PERCEPTION_APPEAR", "COMBAT ACTIVE!");
#endif

                                   if (engine.IsFollower(gameObject) == EngineConstants.FALSE)
                                   {
                                        engine.SetCombatState(gameObject, EngineConstants.TRUE);
                                        engine.WR_ClearAllCommands(gameObject, EngineConstants.TRUE);
                                        if (engine.IsFollower(oAppear) != EngineConstants.FALSE)
                                        {
                                             engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_ENEMY_SIGHTED, oAppear);
                                        }
                                   }

                                   // ---------------------------------------------------------
                                   // If we are not the controlled creature, engage combat AI.
                                   // ---------------------------------------------------------
                                   if (engine.IsControlled(gameObject) == EngineConstants.FALSE)
                                   {
                                        if (engine.IsFollower(gameObject) == EngineConstants.FALSE &&
                                             engine.GetLocalInt(gameObject, EngineConstants.AI_CUSTOM_AI_ACTIVE) != EngineConstants.FALSE) // custom AI active
                                        {
                                             engine.Ambient_Stop(gameObject);

#if DEBUG
                                             engine.Log_Trace_AI("rules_core", "Executing CUSTOM AI");
#endif
                                             engine.SendEventHandleCustomAI(gameObject, null, -1, -1, -1); //
                                        }
                                        else if (engine.IsFollower(gameObject) == EngineConstants.FALSE)
                                        {
                                             // Initial call - all others should be when commands are completed
                                             engine.Ambient_Stop(gameObject);

                                             float fDistance = engine.GetDistanceBetween(gameObject, oAppear);
                                             if (fDistance > 25.0f && nHostilityChanged == EngineConstants.FALSE && engine.IsUsingMeleeWeapon(gameObject) == EngineConstants.FALSE)
                                             {
                                                  nRand = engine.Engine_Random(EngineConstants.AI_TRIGGER_DELAY_RANDOM_RANGE) + 1; // randomizing 10 int values to determine a delay between 0.0f and EngineConstants.AI_TRIGGER_DELAY_RANDOM_RANGE
                                                  fCombatDelay = engine.IntToFloat(nRand) / engine.IntToFloat(EngineConstants.AI_TRIGGER_DELAY_RANDOM_RANGE) * EngineConstants.AI_TRIGGER_DELAY_RANDOM_MAX;

#if DEBUG
                                                  engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Triggering combat with a delay of: " + engine.FloatToString(fCombatDelay));
#endif
                                                  xCommand cWait = engine.CommandWait(fCombatDelay);
                                                  engine.WR_AddCommand(gameObject, cWait);
                                             }
                                             else
                                             {
#if DEBUG
                                                  engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Enemy perceived too close - not delaying start of combat");
#endif
                                                  engine.AI_DetermineCombatRound();
                                             }
                                             engine.SetFacingObject(gameObject, oAppear);
                                        }
                                        else if (engine.IsFollower(gameObject) != EngineConstants.FALSE) // non-followers get wait combat instead, above
                                             engine.AI_DetermineCombatRound();

                                   }

                                   if (engine.GetLocalInt(gameObject, EngineConstants.AI_LIGHT_ACTIVE) == 1)
                                   {
#if DEBUG
                                        engine.Log_Trace_AI("rules_core.EngineConstants.EVENT_TYPE_PERCEPTION_APPEAR", "LIGHT AI CREATURE: aborting rest of perception event");
#endif
                                        break;
                                   }

                                   // ---------------------------------------------------------
                                   // Force all allies into combat
                                   // ---------------------------------------------------------
                                   List<GameObject> arAllies = engine._AI_GetAllies(EngineConstants.COMMAND_TYPE_INVALID, -1);
                                   nSize = engine.GetArraySize(arAllies);
                                   xCommand cMove;
                                   int bIsStealthy = engine.IsStealthy(gameObject);
#if DEBUG
                                   engine.Log_Trace_AI("rules_core.EngineConstants.EVENT_TYPE_PERCEPTION_APPEAR", "BOOM stealthy= " + engine.IntToString(bIsStealthy));
#endif

                                   for (i = 0; i < nSize; i++)
                                   {
                                        oAlly = arAllies[i];
                                        if (engine.GetCombatState(oAlly) == EngineConstants.FALSE)
                                        {
                                             // Yaron, Feb 25 2008 - changing to move toward ally instead of perceiving enemy
                                             // Followers still trigger perception
                                             if (engine.IsFollower(oAlly) != EngineConstants.FALSE && engine.IsStealthy(gameObject) == EngineConstants.FALSE)
                                             {
                                                  if (engine.WR_TriggerPerception(oAlly, oAppear) == EngineConstants.FALSE && engine.IsControlled(oAlly) == EngineConstants.FALSE
                                                     && engine.GetMainControlled() == gameObject)
                                                  {
                                                       // TriggerPerception failed for an ally while I'm the main controlled
                                                       // - Move to party leader
                                                       Vector3 lLoc = engine.GetFollowerWouldBeLocation(oAlly);
                                                       cMove = engine.CommandMoveToLocation(lLoc, EngineConstants.TRUE);
                                                       engine.WR_AddCommand(oAlly, cMove);

                                                  }
                                                  else if (engine.IsControlled(oAlly) == EngineConstants.FALSE)
                                                  // combat time
                                                  // NOTE: this used to be delayed, but was removed becasue it casues followers
                                                  // to linger behind the player sometimes when charging into combat
                                                  {
                                                       engine.AI_DetermineCombatRound();

                                                  }
                                             }
                                             else if (engine.IsFollower(oAlly) == EngineConstants.FALSE) // non party members
                                             {
                                                  if (engine.GetLocalInt(oAlly, EngineConstants.AI_FLAG_STATIONARY) == 0 && engine.GetLocalInt(oAlly, EngineConstants.CLIMAX_ARMY_ID) == 0)
                                                  {
#if DEBUG
                                                       engine.Log_Trace(EngineConstants.LOG_CHANNEL_AI, engine.GetCurrentScriptName(), "Bringing over ally: " + engine.GetTag(oAlly));
#endif

                                                       //int nRand = Engine_Random(10) + 1;
                                                       //float fDistanceToMove = IntToFloat(nRand);
                                                       if (engine.GetLocalInt(gameObject, EngineConstants.AI_HELP_TEAM_STATUS) <= 1) // allow for 0 (not active) or 1 (active for special system)
                                                       {
                                                            engine.SetLocalInt(gameObject, EngineConstants.AI_HELP_TEAM_STATUS, EngineConstants.AI_HELP_TEAM_STATUS_NORMAL_ALLY_HELP_ACTIVE);
                                                            cMove = engine.CommandMoveToObject(oAppear, EngineConstants.TRUE);
                                                            engine.SetCreatureIsStatue(oAlly, EngineConstants.FALSE);
                                                            engine.WR_ClearAllCommands(oAlly, EngineConstants.TRUE);
                                                            engine.Ambient_Stop(oAlly);
                                                            engine.WR_AddCommand(oAlly, cMove, EngineConstants.TRUE, EngineConstants.FALSE);
                                                       }
                                                  }
                                             }

                                        }
                                   }

                              }
                              else // IN combat, perceiving hostile
                                   // (can be when a stealth player goes out of stealth and drags enemies back to his party)
                              {
                                   xCommand cCurrent = engine.GetCurrentCommand(gameObject);
                                   int nQSize = engine.GetCommandQueueSize(gameObject);
                                   if (engine.IsFollower(gameObject) != EngineConstants.FALSE && engine.IsControlled(gameObject) == EngineConstants.FALSE)
                                   {
                                        // a non-controlled follower peceiving hostile while doing nothing
                                        if (nQSize == 0 && engine.GetCommandType(cCurrent) == EngineConstants.COMMAND_TYPE_INVALID)
                                        {
#if DEBUG
                                             engine.Log_Trace_AI("rules_core.EngineConstants.EVENT_TYPE_PERCEPTION_APPEAR", "Non-controlled follower perceivng hostile while in combat state and not running any action");
#endif
                                             engine.AI_DetermineCombatRound();
                                        }
                                   }
                              }

                              // -----------------------------------------------------------------
                              // engine.Set the game into combat camera if we perceive a hostile creature
                              // and are not in combat mode
                              // -----------------------------------------------------------------
                              if (engine.IsPartyMember(gameObject) != EngineConstants.FALSE)
                              {
                                   int nMode = engine.GetGameMode();
#if DEBUG
                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Party game mode: " + engine.IntToString(nMode));
#endif

                                   if (nMode == EngineConstants.GM_EXPLORE || nMode == EngineConstants.GM_DEAD)
                                   {
                                        if (engine.IsObjectHostile(gameObject, oAppear) != EngineConstants.FALSE)
                                        {
#if DEBUG
                                             engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "PARTY perceived hostiles and not in combat yet - ENTERING COMBAT MODE");
#endif

                                             // -------------------------------------------------
                                             // Play Enemy Sighted if controlled character...
                                             // -------------------------------------------------
                                             if (engine.IsControlled(gameObject) != EngineConstants.FALSE)
                                             {
                                                  engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_ENEMY_SIGHTED, oAppear);
                                             }
                                             engine.WR_SetGameMode(EngineConstants.GM_COMBAT);
                                        }
                                   }
                                   // This handles ressurection while the party is already in combat mode and the rejoining
                                   // follower combat state does not match the party's
                                   else if (nMode == EngineConstants.GM_COMBAT && engine.GetCombatState(gameObject) == EngineConstants.FALSE)
                                   {
#if DEBUG
                                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "RESSURECTED PARTY MEMBER: setting combat state ACTIVE");
#endif
                                        engine.SetCombatState(gameObject, EngineConstants.TRUE);
                                   }

                              }

                         }
#if DEBUG
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "tactic table: " + engine.IntToString(engine.GetPackageAI(gameObject)));
#endif
                         // handle creatures that should run away from hostiles (for example: city elf servant escorting the player)
                         if (engine.GetPackageAI(gameObject) == 10130) // coward package
                              engine.AI_HandleCowardFollower(oAppear);

                         break;
                    }

               // --------------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_ALLY_ATTACKED  - AI EVENT
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_ALLY_ATTACKED:
                    {
                         GameObject oAlly = engine.GetEventCreatorRef(ref ev);
                         GameObject oAttacker = engine.GetEventObjectRef(ref ev, 0);

                         if (engine.IsDead(oAttacker) != EngineConstants.FALSE || engine.IsDying(oAttacker) != EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Discarding EngineConstants.EVENT_TYPE_ALLY_ATTACKED (attacker dead or dying)", oAttacker);
#endif
                              return;
                         }

                         // Do not help ally if creature is stationary
                         if (engine.GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY) != EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_AI, engine.GetCurrentScriptName(), "Discarding EngineConstants.EVENT_TYPE_ALLY_ATTACKED (I'm stationary - can't move to help)");
#endif
                              return;
                         }

                         // Just call perception and allow it to decide what to do with the attacker

                         engine.WR_TriggerPerception(gameObject, oAttacker);
                         engine.WR_TriggerPerception(oAttacker, gameObject);

                         break;
                    }

               /*case EngineConstants.EVENT_TYPE_DEATH:
               {
                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER,"rules_core.EngineConstants.EVENT_TYPE_AFTER_DEATH", "Forcing AI level to very low!");
                   engine.SetAILevel(gameObject,CSERVERAIMASTER_AI_LEVEL_VERY_LOW);
                   engine.WR_SetCombatState(gameObject,EngineConstants.FALSE);
                   break;
               } */

               case EngineConstants.EVENT_TYPE_RESURRECTION:
                    {
                         // --------------------------------------------------------------------
                         // //Track engine.Item Equip Events for party
                         // --------------------------------------------------------------------
#if SKYNET
            //TrackCreatureEvent(nEventType,gameObject);
#endif

                         // --------------------------------------------------------------------
                         // Restart AI level after it was frozen...
                         // --------------------------------------------------------------------
#if DEBUG
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "rules_core.EngineConstants.EVENT_TYPE_RESURRECTION", "Forcing AI level normal, then unlocking it!");
#endif
                         engine.SetAILevel(gameObject, EngineConstants.CSERVERAIMASTER_AI_LEVEL_HIGH);
                         engine.SetAILevel(gameObject, EngineConstants.CSERVERAIMASTER_AI_LEVEL_INVALID);
                         break;
                    }

               case EngineConstants.EVENT_TYPE_CONVERSATION:
                    {
                         // Player clicked on this GameObject or dialog initiated using a script
                         // Need to initiate the dialog itself.
                         GameObject oInitiator = engine.GetEventCreatorRef(ref ev);
                         string rConversation = engine.GetEventResourceRef(ref ev, 0); // default is ""
                         string sConv = engine.ResourceToString(rConversation);

                         engine.UT_Talk(gameObject, oInitiator, rConversation);

                         // --------------------------------------------------------------------
                         // //Track Dialog Events
                         // --------------------------------------------------------------------

                         // -------------------------------------------------------------
                         // Event was fully handled, do not fall through to rules_core
                         // -------------------------------------------------------------
                         break;
                    }

               case EngineConstants.EVENT_TYPE_OUT_OF_AMMO:
                    {
                         engine.UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_OUT_OF_AMMO);

                         // Doing nothing now since the AI checks for ammo when it tries to use ranged weapons,
                         // but we may want to add here a static flag setup so the AI can read the flag instead
                         break;
                    }

               // ---------------------------------------------------------------------
               //  Debug Event, do not use in production scripts
               //  Kickstart the AI if it was frozen out
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_DEBUG_KICKSTART_AI:
                    {
                         engine.AI_DetermineCombatRound();
                         break;
                    }

               // ---------------------------------------------------------------------
               //  Creature ran out of mana or stamina
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_MANA_STAM_DEPLETED:
                    {
                         // -----------------------------------------------------------------
                         // Ability system has some events happening when running out of
                         // a string (e.g. deactivating berserk).
                         // -----------------------------------------------------------------

                         engine.Ability_HandleEventOutOfManaStamina(gameObject);
                         break;
                    }

               case EngineConstants.EVENT_TYPE_CONFUSION_CALLBACK:
                    {
                         engine.Effects_HandleConfusionCallback(gameObject);
                         break;
                    }

               case EngineConstants.EVENT_TYPE_SUMMON_DIED:
                    {
                         engine.EffectSummon_HandleEventSummonDied(gameObject, engine.GetEventIntegerRef(ref ev, 0));
                         break;
                    }

               // ---------------------------------------------------------------------
               // Georg: Because DropStealth itself generates 3-4 levels of recursion
               // on the stack, some scripts are using a delayed xEvent to terminate
               // stealth.
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_DROP_STEALTH:
                    {
                         if (engine.IsStealthy(gameObject) != EngineConstants.FALSE)
                         {
                              engine.DropStealth(gameObject);
                         }
                         break;
                    }

               // -----------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE - Changes creature's active state
               // -----------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE:
                    {
                         int nActive = engine.GetEventIntegerRef(ref ev, 0);
#if DEBUG
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "rules_core", "creature set to active=" + engine.IntToString(nActive));
#endif
                         int nAnimation = engine.GetEventIntegerRef(ref ev, 2);
                         int bCallAI = engine.GetEventIntegerRef(ref ev, 3);

                         if (bCallAI != EngineConstants.FALSE)
                         {
                              engine.AI_ExecuteAppearStomp(ev);
                              break;
                         }

                         //Vector3 lLoc = engine.GetEventVectorRef(ref ev, 0);

                         if (nAnimation == 0)
                              nAnimation = -1;

                         engine.WR_SetObjectActive(gameObject, nActive, nAnimation);

                         // Activating summoned creature
                         if (engine.GetEventIntegerRef(ref ev, 1) != 0)
                         {
                              // for summoned creatures it should come here only to REMOVE them. Adding is done in ability_summon_h
                              engine.WR_SetFollowerState(gameObject, engine.GetEventIntegerRef(ref ev, 1));
                         }
                         break;
                    }

               // -----------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_OBJECT_ACTIVE - Creature changed to active state
               // -----------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_OBJECT_ACTIVE:
                    {
                         engine.Ambient_SpawnStart(gameObject);
                         break;
                    }

               // -----------------------------------------------------------------
               // EngineConstants.EVENT_TYPE_DESTROY_OBJECT - Object should be destroyed
               // -----------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_DESTROY_OBJECT:
                    {
#if DEBUG
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "rules_core", "GameObject should be destroyed");
#endif
                         engine.Safe_Destroy_Object(gameObject, 0);

                         break;
                    }

               // ---------------------------------------------------------------------
               //
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_GIFT_ITEM:
                    {
                         GameObject oItem = engine.GetEventObjectRef(ref ev, 0);

                         int nFollower = engine.Approval_GetFollowerIndex(gameObject);
                         int nApprovalChange = engine.Approval_HandleGift(nFollower, oItem);

                         if (engine.IsObjectValid(oItem) == EngineConstants.FALSE)
                         {
#if DEBUG
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "rules_core:EngineConstants.EVENT_TYPE_GIFT_ITEM", "INVALID EngineConstants.ITEM!");
#endif
                              engine.Warning("ERROR! Invalid item was gifted");
                              break;
                         }

#if DEBUG
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "rules_core:EngineConstants.EVENT_TYPE_GIFT_ITEM", "item: " + engine.GetTag(oItem) + ", pending approval change: " + engine.IntToString(nApprovalChange));
#endif

                         engine.SendHandleModuleGift(gameObject, oItem, nApprovalChange);

                         break;
                    }

          }
     }
}