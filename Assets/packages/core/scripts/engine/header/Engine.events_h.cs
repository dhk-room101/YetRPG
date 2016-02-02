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
     // -----------------------------------------------------------------------------
     // Events Includes
     // -----------------------------------------------------------------------------
     /*

         This is an include file of all events that are created via scripting

         Contents:

             SendXXXEvent Functions
             Event structs

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"core_h"

     //#include"log_h"

     /* -----------------------------------------------------------------------------
     *  Designer defined events
     *  ------------------------------------ ----------------------------------------*/

     //------------------------------------------------------------------------------
     // ** PLEASE ALSO UPDATE //tag/main/data/Source/2DA/events.xls if you add new
     //    events here !!!! ** -- Georg
     //------------------------------------------------------------------------------

     //moved public const int EngineConstants.EVENT_TYPE_ATTACKED               = 1001;
     //moved public const int EngineConstants.EVENT_TYPE_ALLY_ATTACKED          = 1002; // an ally has got the 'attacked' xEvent and asked for help
     //moved public const int EngineConstants.EVENT_TYPE_WORLD_MAP_USED         = 1003; // player used the world map to travel from point A to point B
     //moved public const int EngineConstants.EVENT_TYPE_DELAYED_SHOUT          = 1004; // used to fire a dialog shout every few seconds
     //moved public const int EngineConstants.EVENT_TYPE_TRANSITION_TO_WORLD_MAP= 1005; // player uses the generic transition system to open the world map

     //------------------------------------------------------------------------------
     // Redirector events for spellscripts in ability_core
     //------------------------------------------------------------------------------
     //moved public const int EngineConstants.EVENT_TYPE_SPELLSCRIPT_PENDING    = 1005;
     //moved public const int EngineConstants.EVENT_TYPE_SPELLSCRIPT_CAST       = 1006;  // cast
     //moved public const int EngineConstants.EVENT_TYPE_SPELLSCRIPT_IMPACT     = 1007;  // impact
     //moved public const int EngineConstants.EVENT_TYPE_SPELLSCRIPT_DEACTIVATE = 1008; // talent deactivated

     //moved public const int EngineConstants.EVENT_TYPE_DOT_TICK               = 1010;
     //moved public const int EngineConstants.EVENT_TYPE_CAST_AT                = 1011; //an ability has been cast on me
     //moved public const int EngineConstants.EVENT_TYPE_STAT_REGEN             = 1012; // stat regeneration. might be changed in the future.
     //moved public const int EngineConstants.EVENT_TYPE_RESURRECTION           = 1013; // creature resurrected.
     //moved public const int EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE      = 1014; // needed since CommandDoFunction was removed and we're using CommandDoEvent
     // 1015 used by ambient ai
     //moved public const int EngineConstants.EVENT_TYPE_HANDLE_CUSTOM_AI       = 1016; // handle any custom AI before handling the built-in AI
     //moved public const int EngineConstants.EVENT_TYPE_OUT_OF_AMMO            = 1017;
     //moved public const int EngineConstants.EVENT_TYPE_HEARTBEAT              = 1018; // runs 2 seconds after death to set AI level to low
     //moved public const int EngineConstants.EVENT_TYPE_TEAM_DESTROYED         = 1019; // fires when an entire team of creatures is destroyed. The xEvent is fired to the last living creature on the team
     //moved public const int EngineConstants.EVENT_TYPE_CAMPAIGN_ITEM_ACQUIRED = 1020;  // fires when items with the EngineConstants.ITEM_ACQUIRED_EVENT_ID variable set are picked up. The xEvent is fired to the module.
     //moved public const int EngineConstants.EVENT_TYPE_SET_GAME_MODE          = 1021; // fires to the module when the game mode is set with the game mode being set carried as an integer on the event.
     //moved public const int EngineConstants.EVENT_TYPE_COMBAT_END             = 1022; // fires to creature_core when combat ends
     //moved public const int EngineConstants.EVENT_TYPE_DYING                  = 1023; // fired by effect_death_h when a creature received the killing blow.
     //moved public const int EngineConstants.EVENT_TYPE_PLAYER_LEVELUP         = 1024; // fired by the levelup system into player_core.
     ////moved public const int EngineConstants.EVENT_TYPE_...                  = 1025; // unused
     //moved public const int EngineConstants.EVENT_TYPE_AMBIENT_CONTINUE       = 1026; // fired by engine when conversation ends or player is nearby to resume ambient behaviour
     //moved public const int EngineConstants.EVENT_TYPE_MODULE_CHARGEN_DONE    = 1027; // fired the the core chargen script into the module
     //moved public const int EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED     = 1028; // fired when a party member is added to the party pool
     //moved public const int EngineConstants.EVENT_TYPE_SUMMON_DIED            = 1029; // fired when the summon died.
     //moved public const int EngineConstants.EVENT_TYPE_CONFUSION_CALLBACK     = 1030;
     //moved public const int EngineConstants.EVENT_TYPE_PARTY_MEMBER_FIRED     = 1031;
     //moved public const int EngineConstants.EVENT_TYPE_UNIQUE_POWER           = 1032;
     //moved public const int EngineConstants.EVENT_TYPE_APPROACH_TRAP          = 1033; // Trap triggered and the creature receiving this xEvent should approach the trap
     //moved public const int EngineConstants.EVENT_TYPE_CAMPAIGN_ITEM_LOST     = 1034; // fire to the module when the EngineConstants.ITEM_LOST_EVENT variable is set on an item removed form player inventory.

     //moved public const int EngineConstants.EVENT_TYPE_TRAP_TRIGGER_DISARMED  = 1045; // Fired to trap's signal target(s) when the trap is disarmed.
     //moved public const int EngineConstants.EVENT_TYPE_UNLOCK_FAILED          = 1046; // Fired to placeable when unlock attempt fails.
     //moved public const int EngineConstants.EVENT_TYPE_OPENED                 = 1047; // Fired to placeable when it has been opened.
     //moved public const int EngineConstants.EVENT_TYPE_TRAP_RESET             = 1048; // Fired to trap's signal target(s) so it releases/retracts/resets when trap is triggered.
     //moved public const int EngineConstants.EVENT_TYPE_TRAP_DISARM            = 1049; // Fired to a trap to cause itself to disarm.
     //moved public const int EngineConstants.EVENT_TYPE_TRAP_ARM               = 1050; // Fired to a trap to cause itself to arm.
     //moved public const int EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ENTER     = 1051;
     //moved public const int EngineConstants.EVENT_TYPE_TRAP_TRIGGER_EXIT      = 1052;
     //moved public const int EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ARMED     = 1053;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_COMMAND_COMPLETE= 1054;
     //moved public const int EngineConstants.EVENT_TYPE_MODULE_HANDLE_GIFT     = 1055;
     //moved public const int EngineConstants.EVENT_TYPE_MODULE_HANDLE_FOLLOWER_DEATH = 1056; // yes! its only here for Wynne!
     //moved public const int EngineConstants.EVENT_TYPE_SET_INTERACTIVE        = 1057;
     //moved public const int EngineConstants.EVENT_TYPE_OBJECT_ACTIVE          = 1058;
     //moved public const int EngineConstants.EVENT_TYPE_SPELLSCRIPT_INDIVIDUAL_IMPACT = 1066;  // impact
     //moved public const int EngineConstants.EVENT_TYPE_DROP_STEALTH           = 1090;
     //moved public const int EngineConstants.EVENT_TYPE_CREATURE_SHAPESHIFTED  = 1100;

     //moved public const int EngineConstants.EVENT_TYPE_DELAYED_GM_CHANGE = 2000;
     //moved public const int EngineConstants.EVENT_TYPE_AUTOPAUSE = 2001;

     //moved public const int EngineConstants.EVENT_TYPE_CHARGEN_AUTOLEVEL            = 56;

     //moved public const int EngineConstants.EVENT_TYPE_DESTROY_OBJECT         = 1070; // GameObject should destroy itself

     //moved public const int EngineConstants.EVENT_TYPE_COMBO_IGNITE           = 1080;

     //moved public const int EngineConstants.EVENT_TYPE_PARTY_MEMBER_RES_TIMER = 1201;// party members are rezzed on this xEvent if it happens outside of combat.

     // QA is using events 30200-30300
     //moved public const int EngineConstants.EVENT_TYPE_QA_EVENT = 30200;
     //moved public const int EngineConstants.EVENT_TYPE_QA_EVENT_BLA =  30201;

     // Events 50000 to 60000 are reserved for plot events
     //moved public const int EngineConstants.EVENT_TYPE_PROVING_ENTER = 50001;
     //moved public const int EngineConstants.EVENT_TYPE_PROVING_START = 50002;
     //moved public const int EngineConstants.EVENT_TYPE_PROVING_WIN   = 50003;
     //moved public const int EngineConstants.EVENT_TYPE_PROVING_LOSE  = 50004;
     //moved public const int EngineConstants.EVENT_TYPE_PROVING_EXIT  = 50005;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01 = 50006;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_EVENT_02 = 50007;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_EVENT_03 = 50008;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_EVENT_04 = 50009;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_EVENT_05 = 50010;
     //moved public const int EngineConstants.EVENT_TYPE_STEALING_SUCCESS = 50011;
     //moved public const int EngineConstants.EVENT_TYPE_STEALING_FAILURE = 50012;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_EVENT_06 = 50013;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_EVENT_07 = 50014;
     //moved public const int EngineConstants.EVENT_TYPE_CUSTOM_EVENT_08 = 50015;

     //moved public const int EngineConstants.EVENT_TYPE_DEBUG_KICKSTART_AI = 9998; // (georg) kickstart the AI if it was frozen.
     //moved public const int EngineConstants.EVENT_TYPE_DEBUG_RESURRECTION = 9999; // (georg) resurrection button handler for death panel/

     //------------------------------------------------------------------------------
     // Events 80000-81000 are reserved for run database events,
     //
     //   *** See design_tracking_h for their actual definition ***
     //
     //------------------------------------------------------------------------------

     //------------------------------------------------------------------------------
     // ** PLEASE ALSO UPDATE //tag/main/data/Source/2DA/events.xls if you add new
     //    events here !!!! ** -- Georg
     //------------------------------------------------------------------------------

     /* -----------------------------------------------------------------------------
     *  Using this to typecast xEvent parameters for EVENT_CAST_AT
     *  ----------------------------------------------------------------------------*/
     public struct EventOnCastAtParamStruct
     {
          public GameObject oCaster;
          public int nAbility;
          public int bHostile;
          public float fParam1; // for healing spells: amount being healed
     };

     /* -----------------------------------------------------------------------------
     *  Script.nss defines
     *  -----------------------------------------------------------------------------
     public int EngineConstants.EVENT_TYPE_INVALID              =        0;
     public int EngineConstants.EVENT_TYPE_SPELLCASTAT          =        1;
     public int EngineConstants.EVENT_TYPE_DAMAGED              =        2;
     public int EngineConstants.EVENT_TYPE_SPAWN        = 3;
     public int EngineConstants.EVENT_TYPE_DEATH        = 4;
     public int EngineConstants.EVENT_TYPE_MELEE_ATTACK_START = 5;
     public int EngineConstants.EVENT_TYPE_INVENTORY_ADDED  = 6;
     public int EngineConstants.EVENT_TYPE_INVENTORY_REMOVED = 7;
     public int EngineConstants.EVENT_TYPE_ENTER        = 8;
     public int EngineConstants.EVENT_TYPE_EXIT     = 9;
     public int EngineConstants.EVENT_TYPE_BLOCKED      = 10;
     public int EngineConstants.EVENT_TYPE_EQUIP        = 11;
     public int EngineConstants.EVENT_TYPE_UNEQUIP      = 12;
     public int EngineConstants.EVENT_TYPE_FAILTOOPEN   = 13;
     public int EngineConstants.EVENT_TYPE_USE      = 14;
     public int EngineConstants.EVENT_TYPE_CLICK        = 15;
     public int EngineConstants.EVENT_TYPE_TRAP_TRIGGERED   = 16;
     public int EngineConstants.EVENT_TYPE_TRAP_DISARMED    = 17;
     public int EngineConstants.EVENT_TYPE_CONVERSATION     = 18;
     public int EngineConstants.EVENT_TYPE_MODULE_START = 19;
     public int EngineConstants.EVENT_TYPE_MODULE_LOAD  = 20;
     public int EngineConstants.EVENT_TYPE_LISTENER     = 21;
     public int EngineConstants.EVENT_TYPE_LOCKED       = 22;
     public int EngineConstants.EVENT_TYPE_UNLOCKED     = 23;
     public int EngineConstants.EVENT_TYPE_PLAYERLEVELUP    = 24;
     public int EngineConstants.EVENT_TYPE_PERCEPTION_APPEAR = 25;
     public int EngineConstants.EVENT_TYPE_PERCEPTION_DISAPPEAR = 26;
     public int EngineConstants.EVENT_TYPE_SET_PLOT = 27;
     public int EngineConstants.EVENT_TYPE_GET_PLOT = 28;
     public int EngineConstants.EVENT_TYPE_ATTACK_IMPACT = 29;
     public int EngineConstants.EVENT_TYPE_COMBAT_INITIATED = 30; // was ENGAGE_TARGET
     public int EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT = 31; // was CAST
     public int EngineConstants.EVENT_TYPE_APPLY_EFFECT = 33;
     public int EngineConstants.EVENT_TYPE_REMOVE_EFFECT = 34;
     public int EngineConstants.EVENT_TYPE_COMMAND_PENDING = 35; // was RESOLVE_ATTACK, EngineConstants.WEAPON_ATTACK_PENDING
     public int EngineConstants.EVENT_TYPE_COMMAND_COMPLETE = 36;
     public int EngineConstants.EVENT_TYPE_ABILITY_CAST_START = 32;
     public int EngineConstants.EVENT_TYPE_GAMEOBJECTSLOADED = 37;

     */

     /*
     * @brief Signals a handle custom AI event
     *
     * @param oTarget   the target to signal the xEvent to
     *
     * @returns  nothing
     * @author   Georg
     *
     **/
     public void SendEventOutOfAmmo(GameObject oTarget)
     {
#if DEBUG
          Log_Msg(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOutOfAmmo", oTarget);
#endif

          xEvent ev = Event(EngineConstants.EVENT_TYPE_OUT_OF_AMMO);

          SignalEvent(oTarget, ev);
     }

     /*
     * @brief Signals a handle custom AI event
     *
     * @param oTarget   the target to signal the xEvent to
     *
     * @returns  nothing
     * @author   Yaron
     *
     **/
     public void SendEventHandleCustomAI(GameObject oTarget, GameObject oLastTarget, int nLastCommand,
         int nLastCommandStatus, int nLastSubCommand, int nAITargetType = -1, int nAIParameter = 0, int nTacticID = -1)
     {
#if DEBUG
          Log_Msg(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventHandleCustomAI", oTarget);
#endif

          xEvent evHandleCustomAI = Event(EngineConstants.EVENT_TYPE_HANDLE_CUSTOM_AI);
          SetEventObjectRef(ref evHandleCustomAI, 0, oLastTarget);
          SetEventIntegerRef(ref evHandleCustomAI, 1, nLastCommand);
          SetEventIntegerRef(ref evHandleCustomAI, 2, nLastCommandStatus);
          SetEventIntegerRef(ref evHandleCustomAI, 3, nLastSubCommand);
          SetEventIntegerRef(ref evHandleCustomAI, 4, nAITargetType);
          SetEventIntegerRef(ref evHandleCustomAI, 5, nAIParameter);
          SetEventIntegerRef(ref evHandleCustomAI, 6, nTacticID);

          SignalEvent(oTarget, evHandleCustomAI);
     }

     /*
     * @brief Signals a set creature active xEvent to oTarget.
     *
     * @param oTarget   the target to signal the xEvent to
     * @param nActive whether the creature should be active or not
     * @param nAnimation the animation to play
     * @param nPartyMember - set to 1 to make this work on party members
     *
     * @returns  nothing
     * @author   Yaron
     *
     **/
     public void SendEventOnSetObjectActive(GameObject oTarget, int nActive, int nAnim = -1, float fDelay = 0.0f, int nPartyMember = 0)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOnSetObjectActive", IntToString(nActive), oTarget);
#endif

          xEvent evOnSetObjectActive = Event(EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE);
          SetEventIntegerRef(ref evOnSetObjectActive, 0, nActive);
          SetEventIntegerRef(ref evOnSetObjectActive, 1, nPartyMember);
          SetEventIntegerRef(ref evOnSetObjectActive, 2, nAnim);

          if (fDelay == 0.0f)
          {
               SignalEvent(oTarget, evOnSetObjectActive);
          }
          else
          {
               DelayEvent(fDelay, oTarget, evOnSetObjectActive);
          }
     }

     /*
     * @brief Signals an Damaged xEvent to oTarget.
     *
     * @param oTarget   the target to signal the xEvent to
     * @param oAttacker the attacker that caused the event
     *
     * @returns  nothing
     * @author   Brent
     *
     **/
     public void SendEventOnDamaged(GameObject oTarget, GameObject oAttacker, float fDamage, int nDamageType, int nAbility)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOnDamaged", "Damage: " + FloatToString(fDamage), oTarget);
#endif

          xEvent evOnDamage = Event(EngineConstants.EVENT_TYPE_DAMAGED);
          SetEventFloatRef(ref evOnDamage, 0, fDamage);
          SetEventIntegerRef(ref evOnDamage, 0, nDamageType);
          SetEventCreatorRef(ref evOnDamage, oAttacker);
          SetEventIntegerRef(ref evOnDamage, 1, nAbility);

          SignalEvent(oTarget, evOnDamage);
     }

     /*
     * @brief Signals a delayed shout xEvent to oTarget.
     *
     * @param oTarget   the target to signal the xEvent to
     *
     * @returns  nothing
     * @author   Yaron
     *
     **/
     public void SendEventOnDelayedShout(GameObject oTarget)
     {

          xEvent evOnDelayedShout = Event(EngineConstants.EVENT_TYPE_DELAYED_SHOUT);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOnDelayedShout", "", oTarget);
#endif

          SignalEvent(oTarget, evOnDelayedShout);
     }

     /*
     * @brief Signals placeable that it has been opened.
     *
     * @param oTarget   the target to signal the xEvent to
     *
     * @returns  nothing
     * @author   Georg
     *
     **/
     public void SendEventOpened(GameObject oTarget, GameObject oUser)
     {
#if DEBUG
          Log_Msg(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOpened", oTarget);
#endif

          xEvent ev = Event(EngineConstants.EVENT_TYPE_OPENED);
          SetEventObjectRef(ref ev, 0, oUser);
          SignalEvent(oTarget, ev);
     }

     /*
     * @brief Returns EventOnCastAtParamStruct with xEvent parameters for evt
     *
     *  structure fields - the target of the event
     *       GameObject oCaster - the caster of the event
     *       int nAbility   - the ability id
     *       int bHostile   - whether or not to treat the xEvent as hostile action
     *
     * @param evt       The xEvent to get the parameters from
     *
     * @returns  struct with fields.
     * @author   Georg
     *
     **/
     public EventOnCastAtParamStruct GetEventOnCastAtParams(xEvent evt)
     {
          EventOnCastAtParamStruct stRet = new EventOnCastAtParamStruct();

          // <Debug>
          if (GetEventTypeRef(ref evt) != EngineConstants.EVENT_TYPE_CAST_AT && EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               Warning("Critical designer errorr: events_h.GetEventOnCastAtParams called with invalid parameter");
               return stRet;
          }
          // </Debug>

          stRet.oCaster = GetEventObjectRef(ref evt, 0);
          stRet.bHostile = GetEventIntegerRef(ref evt, 0);
          stRet.nAbility = GetEventIntegerRef(ref evt, 1);
          stRet.fParam1 = GetEventFloatRef(ref evt, 0);

          return stRet;

     }

     /*
     * @brief Signals an Attacked xEvent to oTarget.
     *
     * @param oTarget   the target to signal the xEvent to
     * @param oAttacker the attacker that caused the event
     *
     * @returns  nothing
     * @author   Brent
     *
     **/

     public void SendEventOnAttacked(GameObject oTarget, GameObject oAttacker)
     {
          if (oTarget != oAttacker)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOnAttacked", "", oTarget);
#endif

               xEvent evOnAttacked = Event(EngineConstants.EVENT_TYPE_ATTACKED);
               SetEventObjectRef(ref evOnAttacked, 0, oAttacker);

               SignalEvent(oTarget, evOnAttacked);
          }
     }

     /*
     * @brief Signals an Cast_At xEvent to oTarget.
     *
     * @param oTarget   the target to signal the xEvent to
     * @param oCaster   the caster that caused the event
     * @param bHostile  true if the spell was cast with hostile intent
     * @param nAbilityId the ability id that was cast
     *
     * @returns  nothing
     * @author   Georg
     *
     **/
     public void SendEventOnCastAt(GameObject oTarget, GameObject oCaster, int nAbilityId, int bHostile = EngineConstants.TRUE, float fParam1 = 0.0f)
     {
          if (oTarget != oCaster)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOnCastAt", "Ability:" + IntToString(nAbilityId) + " hostile: " + ToString(bHostile), oTarget);
#endif

               xEvent evOnCast = Event(EngineConstants.EVENT_TYPE_CAST_AT);
               SetEventObjectRef(ref evOnCast, 0, oCaster);
               SetEventIntegerRef(ref evOnCast, 0, bHostile);
               SetEventIntegerRef(ref evOnCast, 1, nAbilityId);
               SetEventFloatRef(ref evOnCast, 0, fParam1);
               SignalEvent(oTarget, evOnCast);
          }
     }

     /*
     * @brief Signals an AllyAttacked xEvent to oTarget.
     *
     * @param oTarget   the target to signal the xEvent to
     * @param oAttacker the attacker that caused the event
     * @param bDoNotTriggerUpdate Don't fire a 'update my allies' xEvent as well
     *
     * @returns  nothing
     * @author   Brent
     *
     **/
     public void SendEventOnAllyAttacked(GameObject oTarget, GameObject oAttacker, int bDoNotTriggerUpdate = EngineConstants.FALSE)
     {
          if (oTarget != oAttacker)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOnAllyAttacked", "", oTarget);
#endif
               xEvent evOnAllyAttacked = Event(EngineConstants.EVENT_TYPE_ALLY_ATTACKED);
               SetEventObjectRef(ref evOnAllyAttacked, 0, oAttacker);
               SetEventIntegerRef(ref evOnAllyAttacked, 0, bDoNotTriggerUpdate);

               SignalEvent(oTarget, evOnAllyAttacked);
          }
     }

     /*
     * @brief Sends the death xEvent FROM the death effect
     *
     * @param oTarget   the target to signal the xEvent to
     * @param oAttacker the attacker that caused the Death of the creature
     *
     * @returns  nothing
     * @author   Georg Zoeller
     *
     **/
     public void SendEventOnDeath(GameObject oTarget, GameObject oAttacker)
     {
          xEvent evOnDeath = Event(EngineConstants.EVENT_TYPE_DEATH);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventOnDeath", "", oTarget);
#endif

          SetEventCreatorRef(ref evOnDeath, oAttacker);
          SignalEvent(oTarget, evOnDeath);
     }

     /*
     * @brief Sends the world-map-used xEvent to the module object
     *
     * @param sSource   the area tag the player is travelling from
     * @param sTarget the area tag the player is travelling to
     * @param nSourceTerrain terrain type traveling from
     * @param nTargetTerrain terrain type traveling to
     *
     * @author   Yaron
     *
     **/
     public void SendEventWorldMapUsed(string sSource, string sTarget, int nSourceTerrain, int nTargetTerrain)
     {
          xEvent evWorldMapUsed = Event(EngineConstants.EVENT_TYPE_WORLD_MAP_USED);
          SetEventStringRef(ref evWorldMapUsed, 0, sSource);
          SetEventStringRef(ref evWorldMapUsed, 1, sTarget);
          SetEventIntegerRef(ref evWorldMapUsed, 0, nSourceTerrain);
          SetEventIntegerRef(ref evWorldMapUsed, 1, nTargetTerrain);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.SendEventWorldMapUsed", "from:" + sSource + " to:" + sTarget);
#endif

          SignalEvent(GetModule(), evWorldMapUsed);
     }

     // -----------------------------------------------------------------------------
     //                          *** Spellscript Event Handling ***
     // -----------------------------------------------------------------------------

     // -----------------------------------------------------------------------------
     // Event Parameter Structs (for safe 'typecasting' of xEvent parameters)
     // -----------------------------------------------------------------------------
     public struct EventSpellScriptPendingStruct
     {
          public GameObject oCaster;
          public GameObject oTarget;
          public int nAbility;
          public int nAbilityType;
     };

     public struct EventSpellScriptCastStruct
     {
          public GameObject oCaster;
          public GameObject oTarget;
          public int nAbility;
          public int nAbilityType;
          public int nResistanceCheckResult;
     };

     public struct EventSpellScriptImpactStruct
     {
          public GameObject oCaster;
          public GameObject oTarget;
          public GameObject oItem;
          public int nAbility;
          public int nAbilityType;
          public int nResistanceCheckResult;
          public int nHit;     // the number of the hit in the current attack (for things like flurry)
          public int nHand;      // the hand that hit main 0 or offhand 1
          public Vector3 lTarget;
     };

     public struct EventSpellScriptDeactivateStruct
     {
          public GameObject oCaster;
          public int nAbility;
          public int nAbilityType;
     };

     // -----------------------------------------------------------------------------
     // Event Parameter Struct Accessor Functions
     // -----------------------------------------------------------------------------

     /*
     * @brief Retrieves the xEvent parameters for a EngineConstants.EVENT_TYPE_SPELLSCRIPT_CAST event
     *
     * @param ev The EventSpellPreScriptCast to retrieve the parameters from
     *
     * @returns  EventSpellScriptPendingStruct  with xEvent parameters
     * @author   Georg Zoeller
     *
     **/
     public EventSpellScriptPendingStruct Events_GetEventSpellScriptPendingParameters(xEvent ev)
     {
          EventSpellScriptPendingStruct stRet;

          stRet.oCaster = GetEventObjectRef(ref ev, 0);
          stRet.oTarget = GetEventObjectRef(ref ev, 1);

          stRet.nAbility = GetEventIntegerRef(ref ev, 0);
          stRet.nAbilityType = GetEventIntegerRef(ref ev, 1);

          return stRet;
     }

     /*
     * @brief Retrieves the xEvent parameters for a EngineConstants.EVENT_TYPE_SPELLSCRIPT_CAST event
     *
     * @param ev           The EventSpellScriptCast to retrieve the parameters from
     *
     * @returns  EventSpellScriptCastStruct  with xEvent parameters
     * @author   Georg Zoeller
     *
     **/
     public EventSpellScriptCastStruct Events_GetEventSpellScriptCastParameters(xEvent ev)
     {
          EventSpellScriptCastStruct stRet;

          stRet.oCaster = GetEventObjectRef(ref ev, 0);
          stRet.oTarget = GetEventObjectRef(ref ev, 1);

          stRet.nAbility = GetEventIntegerRef(ref ev, 0);
          stRet.nAbilityType = GetEventIntegerRef(ref ev, 1);
          stRet.nResistanceCheckResult = GetEventIntegerRef(ref ev, 2);

          return stRet;
     }

     /*
     * @brief Retrieves the xEvent parameters for a EngineConstants.EVENT_TYPE_SPELLSCRIPT_CAST event
     *
     * @param ev The EventSpellPreScriptCast to retrieve the parameters from
     *
     * @returns  EventSpellScriptPendingStruct  with xEvent parameters
     * @author   Georg Zoeller
     *
     **/
     public EventSpellScriptImpactStruct Events_GetEventSpellScriptImpactParameters(xEvent ev)
     {
          EventSpellScriptImpactStruct stRet;

          stRet.oCaster = GetEventObjectRef(ref ev, 0);
          stRet.oTarget = GetEventObjectRef(ref ev, 1);
          stRet.oItem = GetEventObjectRef(ref ev, 2);

          stRet.nAbility = GetEventIntegerRef(ref ev, 0);
          stRet.nAbilityType = GetEventIntegerRef(ref ev, 1);
          stRet.nResistanceCheckResult = GetEventIntegerRef(ref ev, 2);
          stRet.nHit = GetEventIntegerRef(ref ev, 3);
          stRet.nHand = GetEventIntegerRef(ref ev, 4);

          stRet.lTarget = GetEventVectorRef(ref ev, 0);

          return stRet;
     }

     /*
     * @brief Retrieves the xEvent parameters for a EngineConstants.EVENT_TYPE_SPELLSCRIPT_DEACTIVATE event
     *
     * @param ev The EventSpellScriptDeactivate to retrieve the parameters from
     *
     * @returns  EventSpellScriptPendingStruct  with xEvent parameters
     * @author   Georg Zoeller
     *
     **/
     public EventSpellScriptDeactivateStruct Events_GetEventSpellScriptDeactivateParameters(xEvent ev)
     {
          EventSpellScriptDeactivateStruct stRet;

          stRet.oCaster = GetEventObjectRef(ref ev, 0);

          stRet.nAbility = GetEventIntegerRef(ref ev, 0);
          stRet.nAbilityType = GetEventIntegerRef(ref ev, 1);

          return stRet;
     }

     // -----------------------------------------------------------------------------
     // Spellscript Event Constructors
     // -----------------------------------------------------------------------------

     /*
     * @brief Creates a EngineConstants.EVENT_TYPE_SPELLSCRIPT_PENDING xEvent and returns it
     *
     * @param oCaster      The caster using the ability (EventObject 0)
     * @param oTarget      The target of the ability    (EventObject 1)
     * @param nAbility     The ability id               (EventInteger 0)
     * @param nAbilityType The ability type             (EventInteger 1)
     *
     * @returns  xEvent with populated xEvent parameters
     * @author   Georg Zoeller
     *
     **/
     public xEvent EventSpellScriptPending(GameObject oCaster, GameObject oTarget, int nAbility, int nAbilityType)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_SPELLSCRIPT_PENDING);

          SetEventObjectRef(ref ev, 0, oCaster);
          SetEventObjectRef(ref ev, 1, oTarget);

          SetEventIntegerRef(ref ev, 0, nAbility);
          SetEventIntegerRef(ref ev, 1, nAbilityType);

          return ev;
     }

     /*
     * @brief Creates a EngineConstants.EVENT_TYPE_SPELLSCRIPT_CAST xEvent and returns it
     *
     * @param oCaster      The caster using the ability (EventObject 0)
     * @param oTarget      The target of the ability    (EventObject 1)
     * @param nAbility     The ability id               (EventInteger 0)
     * @param nAbilityType The ability type             (EventInteger 1)
     * @param nResistanceCheckResult Result of the (EventInteger 2)
     *
     * @returns  xEvent with populated xEvent parameters
     * @author   Georg Zoeller
     *
     **/
     public xEvent EventSpellScriptCast(GameObject oCaster, GameObject oTarget, int nAbility, int nAbilityType, int nResistanceCheckResult)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_SPELLSCRIPT_CAST);

          SetEventObjectRef(ref ev, 0, oCaster);
          SetEventObjectRef(ref ev, 1, oTarget);

          SetEventIntegerRef(ref ev, 0, nAbility);
          SetEventIntegerRef(ref ev, 1, nAbilityType);
          SetEventIntegerRef(ref ev, 2, nResistanceCheckResult);

          return ev;
     }

     /*
     * @brief Creates a EngineConstants.EVENT_TYPE_SPELLSCRIPT_IMPACT xEvent and returns it
     *
     * @param oCaster      The caster using the ability (EventObject 0)
     * @param oTarget      The target of the ability    (EventObject 1)
     * @param nAbility     The ability id               (EventInteger 0)
     * @param nAbilityType The ability type             (EventInteger 1)
     * @param nResistanceCheckResult Result of the (EventInteger 2)
     * @returns  xEvent with populated xEvent parameters
     * @author   Georg Zoeller
     **/
     public xEvent EventSpellScriptImpact(GameObject oCaster, GameObject oTarget, int nAbility, int nAbilityType, int nResistanceCheckResult, Vector3 lTarget, int nHit = 1, int nHand = 0, GameObject oItem = null)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_SPELLSCRIPT_IMPACT);

          SetEventObjectRef(ref ev, 0, oCaster);
          SetEventObjectRef(ref ev, 1, oTarget);
          SetEventObjectRef(ref ev, 2, oItem);

          SetEventIntegerRef(ref ev, 0, nAbility);
          SetEventIntegerRef(ref ev, 1, nAbilityType);
          SetEventIntegerRef(ref ev, 2, nResistanceCheckResult);

          SetEventIntegerRef(ref ev, 3, nHit);
          SetEventIntegerRef(ref ev, 4, nHand);

          SetEventVectorRef(ref ev, 0, lTarget);

          return ev;
     }

     /* ----------------------------------------------------------------------------
     * @brief Creates a EngineConstants.EVENT_TYPE_SPELLSCRIPT_DEACTIVATE xEvent and returns it
     *
     * @param oCaster      The caster using the ability (EventObject 0)
     * @param nAbility     The ability id               (EventInteger 0)
     * @param nAbilityType The ability type             (EventInteger 1)

     * @returns  xEvent with populated xEvent parameters
     * @author   Georg Zoeller
     *  -----------------------------------------------------------------------------
     **/
     public xEvent EventSpellScriptDeactivate(GameObject oCaster, int nAbility, int nAbilityType)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_SPELLSCRIPT_DEACTIVATE);

          SetEventObjectRef(ref ev, 0, oCaster);

          SetEventIntegerRef(ref ev, 0, nAbility);
          SetEventIntegerRef(ref ev, 1, nAbilityType);

          return ev;

     }

     /*
     *   @brief Filters the Attack Impact Event
     *
     *   Returns whether or not to continue processing the attack impact event
     *
     *   @param oAttacker  - The Attacker
     *   @param oTarget    - The Attack Target
     *   @param nHitResult - The Hit Result of the Attack as propagated from EngineConstants.COMMAND_PENDING
     *
     *   @returns    EngineConstants.FALSE if processing the xEvent should be aborted.
     *
     *   @author Georg Zoeller
     */
     public int Events_FilterAttackImpactEvent(GameObject oAttacker, GameObject oTarget, int nHitResult)
     {

          // -------------------------------------------------------------------------
          // NOTE
          //  We can not filter out IsDying(oTarget) as the attack impact ensures
          //  that the creature is actually killed. Filtering it out would
          //  get creatures stuck as dying but not dead...
          // -------------------------------------------------------------------------

          // -------------------------------------------------------------------------
          // Don't process impacts from invalid attacker (GameObject unloaded?)
          // -------------------------------------------------------------------------
          if (IsObjectValid(oAttacker) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_Combat("events_h.Events_FilterAttackImpactEvent", "Attacker Invalid  - xEvent discarded", oAttacker, oTarget);
#endif
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // Don't process impacts from dead or dying attackers anymore
          // -------------------------------------------------------------------------
          if (IsDead(oAttacker) != EngineConstants.FALSE || IsDying(oAttacker) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_Combat("events_h.Events_FilterAttackImpactEvent", "Attacker Dead or Dying - xEvent discarded", oAttacker, oTarget);
#endif

               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // Don't process impacts from disabled attackers
          // -------------------------------------------------------------------------
          if (IsDisabled(oAttacker) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_Combat("events_h.Events_FilterAttackImpactEvent", "Attacker Disabled - xEvent discarded", oAttacker, oTarget);
#endif
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // Don't process impacts on invalid targets
          // -------------------------------------------------------------------------
          if (IsObjectValid(oTarget) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_Combat("events_h.Events_FilterAttackImpactEvent", "Target invalid - xEvent discarded", oAttacker, oTarget);
#endif
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // Don't process impacts on dead targets
          // -------------------------------------------------------------------------
          if (IsDead(oTarget) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_Combat("events_h.Events_FilterAttackImpactEvent", "Target dead - xEvent discarded", oAttacker, oTarget);
#endif
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // Don't process on invalid hit results
          // -------------------------------------------------------------------------
          if (nHitResult == EngineConstants.COMBAT_RESULT_INVALID)
          {
#if DEBUG
               Log_Trace_Combat("events_h.Events_FilterAttackImpactEvent", "COMBAT_RESULT_INVALID - xEvent discarded", oAttacker, oTarget);
#endif
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // Looks like we're clear to continue running this event.
          // -------------------------------------------------------------------------
          return EngineConstants.TRUE;
     }

     /*
     *  @brief Decides whether or not a command_pending should be processed or discarded
     *
     *  @param oCommandOwner   The xCommand owner, usually gameObject
     *  @param oTarget         The Target of the command
     *  @param nCommandId      The xCommand Id
     *  @param nCommandSubType The xCommand subtype
     *
     *  @author Georg Zoeller
     **/
     public int Events_ValidateCommandPending(GameObject oCommandOwner, GameObject oTarget, int nCommandId, int nCommandSubType)
     {

          if (IsObjectValid(oCommandOwner) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.ValidateCommandPending", "owner is EngineConstants.INVALID!");
#endif
               SetCommandResult(oCommandOwner, EngineConstants.COMMAND_RESULT_INVALID);
               return EngineConstants.FALSE;
          }

          if (IsObjectValid(oTarget) != EngineConstants.FALSE && (GetArea(oCommandOwner) != GetArea(oTarget)))
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "events_h.ValidateCommandPending", "Discarded Command Pending ... not in the same area as target");
#endif
               return EngineConstants.FALSE;
          }

          if (IsDead(oCommandOwner) != EngineConstants.FALSE || IsDying(oCommandOwner) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "rules_core: (EngineConstants.COMMAND_PENDING) - I'm dead or dying- ignoring xEvent - ignoring event!");
#endif
               SetCommandResult(oCommandOwner, EngineConstants.COMMAND_RESULT_INVALID);
               return EngineConstants.FALSE;
          }
          if (IsDisabled(oCommandOwner) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "rules_core: (EngineConstants.COMMAND_PENDING) -  I'm disabled - ignoring event!");
#endif
               SetCommandResult(oCommandOwner, EngineConstants.COMMAND_RESULT_INVALID);
               return EngineConstants.FALSE;
          }

          if (IsObjectValid(oTarget) == EngineConstants.FALSE)
          {
               Log_Rules("rules_core, EngineConstants.COMMAND_PENDING target is EngineConstants.INVALID! - NOT stopping - it may be for a non-target ability (Berserk)", EngineConstants.LOG_LEVEL_WARNING);
               // NOT STOPPING - it may be Berserk, that has no target
          }
          else
          // -----------------------------------------------------------------
          // We can never attack or cast at a dying target, but
          // UseAbility (nCommandId=25) is potentially valid on dead people
          // -----------------------------------------------------------------
          if ((IsDying(oTarget) != EngineConstants.FALSE && nCommandId != 25 || (IsDead(oTarget) != EngineConstants.FALSE) && nCommandId != 25))
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "rules_core, EngineConstants.COMMAND_PENDING target is DEAD or DYING");
#endif
               SetCommandResult(oCommandOwner, EngineConstants.COMMAND_RESULT_FAILED_NO_VALID_TARGET);
               return EngineConstants.FALSE;

          }

          return EngineConstants.TRUE;
     }

     // Sent to the last living creature in a team when he dies.
     public void SendEventTeamDestroyed(GameObject oTarget, int nTeamID)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "SendEventTeamDestroyed, team: ", IntToString(nTeamID));
#endif

          xEvent evTeamDestroyed = Event(EngineConstants.EVENT_TYPE_TEAM_DESTROYED);
          SetEventIntegerRef(ref evTeamDestroyed, 0, nTeamID);

          SignalEvent(oTarget, evTeamDestroyed);
     }

     /*
     * @brief Signals an Item Acquired event
     *
     * @param oTarget   the target to signal the xEvent to
     * @param oItem     the item acquired
     *
     * @returns  nothing
     * @author   David Sims
     *
     **/
     public void SendEventCampaignItemAcquired(GameObject oTarget, GameObject oItem, int bProcessImmediate = EngineConstants.FALSE)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "SendEventCampaignItemAcquired, item tag: ", GetTag(oItem));
#endif

          xEvent evItemAquired = Event(EngineConstants.EVENT_TYPE_CAMPAIGN_ITEM_ACQUIRED);
          SetEventObjectRef(ref evItemAquired, 0, oItem);

          SignalEvent(oTarget, evItemAquired, bProcessImmediate);
     }

     /*
      * @brief Signals an Item Lost event
      *
      * Signals the module when an item is removed from the player's inventory should
      * that item have the EngineConstants.ITEM_SEND_LOST_EVENT variable set.
      *
      * @param oTarget   The target GameObject to signal the xEvent to.
      * @param oItem     The item removed from the player's inventory.
      *
      * @returns Nothing.
      * @author  Grant Mackay
      */
     public void SendEventCampaignItemLost(GameObject oTarget, GameObject oItem, int bProcessImmediate = EngineConstants.FALSE)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "SendEventCampaignItemLost, item tag: ", GetTag(oItem));
#endif

          xEvent evItemLost = Event(EngineConstants.EVENT_TYPE_CAMPAIGN_ITEM_LOST);

          SetEventObjectRef(ref evItemLost, 0, oItem);

          SignalEvent(oTarget, evItemLost, bProcessImmediate);

     }

     /*
    * @brief Signals a dying xEvent (when a creature receives the killing blow)
    *
    * @param oTarget   The target for the event
    * @param oKiller   The Killer
    *
    * @author   Georg
    *
    **/
     public void SendEventDying(GameObject oTarget, GameObject oKiller)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_DYING);
          SetEventObjectRef(ref ev, 0, oKiller);
          SignalEvent(oTarget, ev);

     }

     /*
     * @brief Signals the module that the player is transitioning into the world map
     * using the generic area transition system. The module then handles campaign-specific
     * world maps
     *
     * @param sWorldMap A world map ID, most likely "world_map"
     * @param sTranType Transition type: will be "rand" when exiting random encounters
     * @param nWorldMapLoc1 a Vector3 on the map to set active right before opening the map
     * @param nWorldMapLoc2 a Vector3 on the map to set active right before opening the map
     * @param nWorldMapLoc3 a Vector3 on the map to set active right before opening the map
     * this Vector3 is equivalent to a plot flag in the world map plot or Vector3 ID
     *
     * @author   Georg
     *
     **/
     public void SendEventTransitionToWorldMap(string sWorldMap, string sTransType, string nWorldMapLoc1, string nWorldMapLoc2, string nWorldMapLoc3, string nWorldMapLoc4, string nWorldMapLoc5)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_TRANSITION_TO_WORLD_MAP);
          SetEventStringRef(ref ev, 0, sWorldMap);
          SetEventStringRef(ref ev, 1, sTransType);
          SetEventStringRef(ref ev, 2, nWorldMapLoc1);
          SetEventStringRef(ref ev, 3, nWorldMapLoc2);
          SetEventStringRef(ref ev, 4, nWorldMapLoc3);
          SetEventStringRef(ref ev, 5, nWorldMapLoc4);
          SetEventStringRef(ref ev, 6, nWorldMapLoc5);
          SignalEvent(GetModule(), ev);

     }

     public void SendLevelUpEvent(GameObject oPartyMember, int nNewLevel)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_PLAYER_LEVELUP);
          SetEventIntegerRef(ref ev, 0, nNewLevel);
          SignalEvent(oPartyMember, ev);
     }

     public void SendEventModuleChargenDone(string sParam1, string sParam2)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_MODULE_CHARGEN_DONE);
          SetEventStringRef(ref ev, 0, sParam1);
          SetEventStringRef(ref ev, 1, sParam2);
          SignalEvent(GetModule(), ev);
     }

     public void SendEventApproachTrap(GameObject oCreature, GameObject oTrap)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_APPROACH_TRAP);
          SetEventObjectRef(ref ev, 0, oTrap);
          SignalEvent(oCreature, ev);
     }

     public void SendPartyMemberHiredEvent(GameObject oPartyMember, int nShowPartyPicker, int nMinLevel = 0, int bPreventLevelup = EngineConstants.FALSE)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "SendPartyMemberHiredEvent", "follower: " + GetTag(oPartyMember) +
              ", show party picker: " + IntToString(nShowPartyPicker) + ", prevent levelup: " + IntToString(bPreventLevelup));
#endif
          xEvent ev = Event(EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED);
          SetEventIntegerRef(ref ev, 0, nShowPartyPicker);
          SetEventIntegerRef(ref ev, 1, nMinLevel);
          SetEventIntegerRef(ref ev, 2, bPreventLevelup);
          SignalEvent(oPartyMember, ev);
     }

     public void SendPartyMemberFiredEvent(GameObject oPartyMember)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_PARTY_MEMBER_FIRED);
          SignalEvent(oPartyMember, ev);
     }

     //moved public const int COMBO_EVENT_IGNITE =  EngineConstants.EVENT_TYPE_COMBO_IGNITE;
     //moved public const int COMBO_EVENT_DOUSE  = 2;

     public void SendComboEventAoE(int nAbility, int nShapeId, Vector3 lLoc, GameObject oCreator, float fA, float fB = 0.0f, float fC = 0.0f, float fDelay = 0.0f)
     {
          xEvent eve = Event(EngineConstants.EVENT_TYPE_COMBO_IGNITE);
          SetEventIntegerRef(ref eve, 0, nAbility);
          SetEventIntegerRef(ref eve, 1, nShapeId);
          SetEventFloatRef(ref eve, 0, fA);
          SetEventFloatRef(ref eve, 1, fB);
          SetEventFloatRef(ref eve, 2, fC);
          SetEventVectorRef(ref eve, 0, lLoc);
          SetEventObjectRef(ref eve, 0, oCreator);

          DelayEvent(fDelay, GetModule(), eve);
     }

     public void SendHandleModuleGift(GameObject oFollower, GameObject oGiftTag, int nApprovalChange)
     {
          xEvent evModuleHandleGift = Event(EngineConstants.EVENT_TYPE_MODULE_HANDLE_GIFT);
          SetEventObjectRef(ref evModuleHandleGift, 0, oFollower);
          SetEventObjectRef(ref evModuleHandleGift, 1, oGiftTag);
          SetEventIntegerRef(ref evModuleHandleGift, 0, nApprovalChange);

          SignalEvent(GetModule(), evModuleHandleGift);
     }

     public void SendModuleHandleFollowerDeath(GameObject oFollower)
     {
          xEvent ev = Event(EngineConstants.EVENT_TYPE_MODULE_HANDLE_FOLLOWER_DEATH);
          SetEventObjectRef(ref ev, 0, oFollower);

          SignalEvent(GetModule(), ev);
     }

     public void SignalEventDropStealth(GameObject oTarget)
     {
          DelayEvent(0.0f, oTarget, Event(EngineConstants.EVENT_TYPE_DROP_STEALTH));
     }
}