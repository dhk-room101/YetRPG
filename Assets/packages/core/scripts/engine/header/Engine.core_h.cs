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
     //  core_h.nss
     //------------------------------------------------------------------------------
     //  Include file containing definitions for low level functions independent of
     //  specific systems.
     //
     //  This file may never include any other files with the exception of log_h
     //  or constant files functions in this file usually do not carry a prefix
     //
     //  Please talk to Georg before using any of these functions or making any
     //  changes.
     //------------------------------------------------------------------------------
     //  2006/11/27 - Owner: Georg Zoeller
     //------------------------------------------------------------------------------

     //#include"log_h"
     //#include"effect_constants_h"
     //#include"2da_data_h"
     //#include"core_difficulty_h"

     //moved public const float EngineConstants.AI_MELEE_RANGE = 3.5f; // Any target within this range is considered a melee target
     //moved public const int   DA_LEVEL_CAP   = 25;  // Dragon Age level cap. Note: This is one of several values that control this (including max_val on properties.xls!)

     //moved public const int EngineConstants.PROPERTY_SIMPLE_AI_BEHAVIOR = 18;
     //moved public const int EngineConstants.AI_BEHAVIOR_DEFAULT = 0;

     // -----------------------------------------------------------------------------
     // Basic combat system confiration
     // -----------------------------------------------------------------------------
     //moved public const float EngineConstants.COMBAT_CRITICAL_DAMAGE_MODIFIER = 1.5f; // critical hits increase damage by up to this factor.
     //moved public const float EngineConstants.COMBAT_DEFAULT_UNARMED_DAMAGE = 3.0f;   // basic unarmed damage
     //moved public const float EngineConstants.COMBAT_ARMOR_RANDOM_ELEMENT = 0.3f;     // How much of armor value is randomized when reducing incoming damage (default: 30%)
     //moved public const float EngineConstants.UNARMED_ATTRIBUTE_BONUS_FACTOR = 1.25f;

     // Weapon Timings
     //moved public const float EngineConstants.BASE_TIMING_DUAL_WEAPONS  = 1.5f;
     //moved public const float EngineConstants.BASE_TIMING_WEAPON_SHIELD = 2.0f;
     //moved public const float EngineConstants.BASE_TIMING_TWO_HANDED    = 2.5f;

     //moved public const float REGENERATION_STAMINA_COMBAT_DEFAULT = 1.0f;       // was .5
     //moved public const float REGENERATION_STAMINA_COMBAT_NULL = -1.0f;
     //moved public const float REGENERATION_STAMINA_COMBAT_DEGENERATION = -2.0f;

     //moved public const float REGENERATION_STAMINA_EXPLORE_DEFAULT = 17.5f;     // was 10
     //moved public const float REGENERATION_STAMINA_EXPLORE_NULL = -17.5f;
     //moved public const float REGENERATION_STAMINA_EXPLORE_DEGENERATION = -20.0f;

     //moved public const float REGENERATION_HEALTH_COMBAT_DEFAULT = 0.0f;
     //moved public const float REGENERATION_HEALTH_EXPLORE_DEFAULT = 10.0f;

     // Hand definitions
     //moved public const int EngineConstants.HAND_MAIN     =   0;
     //moved public const int EngineConstants.HAND_OFFHAND = 1;
     //moved public const int EngineConstants.HAND_BOTH    = 3;

     // -----------------------------------------------------------------------------

     //moved public const string EngineConstants.INVALID_RESOURCE = "";

     // Generic compare Results for Compare* functions
     //moved public const int EngineConstants.COMPARE_RESULT_HIGHER =  1;
     //moved public const int EngineConstants.COMPARE_RESULT_EQUAL  = -1;
     //moved public const int EngineConstants.COMPARE_RESULT_LOWER  =  0;

     // Placeable State Controller types
     //moved public const string EngineConstants.PLC_STATE_CNT_BRIDGE = "StateCnt_Bridge";
     //moved public const string EngineConstants.PLC_STATE_CNT_AREA_TRANSITION = "StateCnt_AreaTransition";
     //moved public const string EngineConstants.PLC_STATE_CNT_FURNITURE = "StateCnt_Furniture";
     //moved public const string EngineConstants.PLC_STATE_CNT_INFORMATIONAL = "StateCnt_Informational";
     //moved public const string EngineConstants.PLC_STATE_CNT_AOE = "StateCnt_AOE";
     //moved public const string EngineConstants.PLC_STATE_CNT_FLIPCOVER = "StateCnt_FlipCover";
     //moved public const string EngineConstants.PLC_STATE_CNT_TRAP_TRIGGER = "StateCnt_Trap_Trigger";
     //moved public const string EngineConstants.PLC_STATE_CNT_NON_SELECEngineConstants.TABLE_TRAP = "StateCnt_NonSelectable_Trap";
     //moved public const string EngineConstants.PLC_STATE_CNT_SELECEngineConstants.TABLE_TRAP = "StateCnt_Selectable_Trap";
     //moved public const string EngineConstants.PLC_STATE_CNT_PUZZLE = "StateCnt_Puzzle";
     //moved public const string EngineConstants.PLC_STATE_CNT_CAGE = "StateCnt_Cage";
     //moved public const string EngineConstants.PLC_STATE_CNT_BODYBAG = "StateCnt_Bodybag";
     //moved public const string EngineConstants.PLC_STATE_CNT_CONTAINER_STATIC = "StateCnt_Container_Static";
     //moved public const string EngineConstants.PLC_STATE_CNT_CONTAINER = "StateCnt_Container";
     //moved public const string EngineConstants.PLC_STATE_CNT_TRIGGER = "StateCnt_Trigger";
     //moved public const string EngineConstants.PLC_STATE_CNT_DOOR = "StateCnt_Door";

     // used as artifical delimiter for GetNearest* functions in some spells.
     //moved public const int EngineConstants.MAX_GETNEAREST_OBJECTS = 30;

     // Rules stuff
     //moved public const int MIN_ATTRIBUTE_VALUE = 1;

     //moved public const float EngineConstants.RULES_ATTRIBUTE_MODIFIER = 10.0f;

     //moved public const int EngineConstants.CREATURE_RULES_FLAG_DYING             = 0x00000002;
     //moved public const int EngineConstants.CREATURE_RULES_FLAG_DOT               = 0x00000004;
     //moved public const int EngineConstants.CREATURE_RULES_FLAG_AI_OFF            = 0x00000008;
     //moved public const int EngineConstants.CREATURE_RULES_FLAG_AI_NO_ABILITIES   = 0x00000010;
     //moved public const int EngineConstants.CREATURE_RULES_FLAG_NO_COOLDOWN       = 0x00000020;
     //moved public const int EngineConstants.CREATURE_RULES_FLAG_NO_RESISTANCE     = 0x00000040;
     //moved public const int EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_0    = 0x00000080; // bit0 of combat result force
     //moved public const int EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_1    = 0x00000100; // bit1 of combat result force

     //moved public const int EngineConstants.APR_RULES_FLAG_CONSTRUCT              = 0x00000002;

     //moved public const int EngineConstants.AREA_FLAG_IS_FADE                     = 0x00000001;

     //moved public const int EngineConstants.WEAPON_WIELD_TWO_HANDED_MELEE = 3;

     // Body bag stuff
     //moved public const string EngineConstants.BODY_BAG_TAG = "gen_ip_bodybag";

     public void _LogDamage(string msg, GameObject oTarget = null)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "combat_damage", msg, oTarget);
     }

     /*
     * @brief (core_h)Sets a EngineConstants.CREATURE_FLAG_* flag (boolean persistent variable) on a creature
     *
     * Flags are used by various game systems and should always be set through
     * this function.
     *
     * @param oCreature The creature to set the flag on
     * @param nFlag     EngineConstants.CREATURE_FLAG_* to set.
     * @param bSet      whether to set or to clear the flag.
     *
     * @returns  EngineConstants.TRUE or EngineConstants.FALSE
     *
     * @author Georg Zoeller
     **/
     public void SetCreatureFlag(GameObject oCreature, int nFlag, int bSet = EngineConstants.TRUE)
     {
          int nVal = GetLocalInt(oCreature, EngineConstants.CREATURE_RULES_FLAG0);

          int nOld = nVal;

          if (bSet != EngineConstants.FALSE)
          {
               nVal |= nFlag;
          }
          else
          {
               nVal &= ~nFlag;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "core_h.SetCreatureFlag", "Flag: " + IntToHexString(nFlag) + " Was: " + IntToHexString(nOld) + " Is: " + IntToHexString(nVal), oCreature);

          SetLocalInt(oCreature, EngineConstants.CREATURE_RULES_FLAG0, nVal);
     }
     public int IsShapeShifted(GameObject oCreature)
     {
          return GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_SHAPECHANGE);
     }

     /*
     * @brief (core_h)-----------------------------------------------------------------------------
     * Force a specific combat result on a creature or pass -1 to clear
     *
     * @param nResult: The automatic combat result the creature will generate until
     *                 this function is called with a different parameter
     *
     * Allowed results:
     *      EngineConstants.COMBAT_RESULT_MISS
     *      EngineConstants.COMBAT_RESULT_CRITICALHIT
     *      EngineConstants.COMBAT_RESULT_DEATHBLOW
     *      -1
     * @author: georg
     * -----------------------------------------------------------------------------
     **/
     public void SetForcedCombatResult(GameObject oCreature, int nResult = -1)
     {
          if (nResult == EngineConstants.COMBAT_RESULT_DEATHBLOW)
          {
               SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_0);
               SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_1);
          }
          else if (nResult == EngineConstants.COMBAT_RESULT_MISS)
          {
               SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_0);
               SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_1, EngineConstants.FALSE);
          }
          else if (nResult == EngineConstants.COMBAT_RESULT_CRITICALHIT)
          {
               SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_0, EngineConstants.FALSE);
               SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_1);
          }
          else if (nResult == -1)
          {
               SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_0, EngineConstants.FALSE);
               SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_1, EngineConstants.FALSE);
          }
     }

     // -----------------------------------------------------------------------------
     // Return if a forced combat result was set on the creature
     // -----------------------------------------------------------------------------
     public int GetForcedCombatResult(GameObject oCreature)
     {
          // bitmask as follows:
          // 0 1 = miss
          // 1 0 = crit
          // 1 1 = deathblow

          int nVal = GetLocalInt(oCreature, EngineConstants.CREATURE_RULES_FLAG0);
          int nMask = (EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_0 | EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_1);
          int nResult = (nVal & nMask);

          if (nResult == nMask)
          {
               return EngineConstants.COMBAT_RESULT_DEATHBLOW;
          }
          if (nResult == EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_0)
          {
               return EngineConstants.COMBAT_RESULT_MISS;
          }
          else if (nResult == EngineConstants.CREATURE_RULES_FLAG_FORCE_COMBAT_1)
          {
               return EngineConstants.COMBAT_RESULT_CRITICALHIT;
          }
          else
          {
               return -1;
          }
     }

     //moved public const int EngineConstants.CRITICAL_MODIFIER_MELEE  = EngineConstants.PROPERTY_ATTRIBUTE_MELEE_CRIT_MODIFIER;
     //moved public const int EngineConstants.CRITICAL_MODIFIER_MAGIC  = EngineConstants.PROPERTY_ATTRIBUTE_MAGIC_CRIT_MODIFIER;
     //moved public const int EngineConstants.CRITICAL_MODIFIER_RANGED = EngineConstants.PROPERTY_ATTRIBUTE_RANGED_CRIT_MODIFIER;

     /* ----------------------------------------------------------------------------
     * @brief (core_h) Return whether a xCommand is of higher priority than the current
     *                 command
     *
     * D E P R E C A T E D
     *
     * @param oObject      The GameObject that holds the current command
     * @param cNewCommand  The new xCommand that is tested
     *
     * @returns  EngineConstants.TRUE if cNewCommand has higher priority, EngineConstants.FALSE if not
     *
     * @author Georg Zoeller
     *  ---------------------------------------------------------------------------**/
     public int IsNewCommandHigherPriority(GameObject oObject, xCommand cNewCommand)
     {
          int bNewIsHigher = EngineConstants.FALSE;
          xCommand cCurrent = GetCurrentCommand(oObject);
          int nNewCommandPriority = GetCommandPriority(cNewCommand);
          int nCurrentCommandPriority = GetCommandPriority(cCurrent);
          int nNewCommandType = GetCommandType(cNewCommand);
          int nCurrentCommandType = GetCommandType(cCurrent);

          /*    Log_Rules("Rules_IsNewCommandHigherPriority: current command: <" +
                         IntToString(nCurrentCommandType) + "> new command: <" +
                         IntToString(nNewCommandType) + ">", EngineConstants.LOG_LEVEL_DEBUG, oObject);*/

          if (nNewCommandPriority == EngineConstants.COMMAND_PRIORITY_INVALID)
          {
               /*        Log_Rules("Rules_IsNewCommandHigherPriority: invalid priority for NEW command!",
                                 EngineConstants.LOG_LEVEL_ERROR, oObject);*/
          }
          else if (nCurrentCommandPriority == EngineConstants.COMMAND_PRIORITY_INVALID)
          {
               /*       Log_Rules("Rules_IsNewCommandHigherPriority: invalid priority for CURRENT command! (creature doing nothing?)",
                                 EngineConstants.LOG_LEVEL_ERROR, oObject);*/

               return EngineConstants.TRUE; // creature might just not doing anything at the moment - ok to interrupt
          }
          else // valid priorities for new and current commands
          {
               if (nNewCommandPriority > nCurrentCommandPriority)
               {
                    // the new xCommand is eligible to kick the current xCommand out of the queue
                    /* Log_Rules("Rules_IsNewCommandHigherPriority: new xCommand is higher priority",
                               EngineConstants.LOG_LEVEL_DEBUG, oObject);*/

                    return EngineConstants.TRUE;
               }
               else
               {
                    /*            Log_Rules("Rules_IsNewCommandHigherPriority: new xCommand is NOT higher priority",
                                           EngineConstants.LOG_LEVEL_DEBUG, oObject);*/
               }

          }
          return EngineConstants.FALSE;
     }

     /* ----------------------------------------------------------------------------
     * @brief (core_h) Compares two values and returns a result constant
     *
     * @param nA An integer to compare (A)
     * @param nB An integer to compare it to (B)
     *
     * @returns  - A >  B : EngineConstants.COMPARE_RESULT_HIGHER
     *           - A == B : EngineConstants.COMPARE_RESULT_EQUAL
     *           - A <  B : EngineConstants.COMPARE_RESULT_LOWER
     *
     * @author Georg Zoeller
     *  ---------------------------------------------------------------------------**/
     public int CompareInt(int nA, int nB)
     {
          if (nA > nB)
          {
               return EngineConstants.COMPARE_RESULT_HIGHER;
          }
          else if (nA == nB)
          {
               return EngineConstants.COMPARE_RESULT_EQUAL;
          }
          else
          {
               return EngineConstants.COMPARE_RESULT_LOWER;
          }
     }

     /* ----------------------------------------------------------------------------
     * @brief (core_h) Compares two float values and returns a result constant
     *
     * @param nA A float to compare (A)
     * @param nB A float to compare it to (B)
     *
     * @returns  - A >  B : EngineConstants.COMPARE_RESULT_HIGHER
     *           - A == B : EngineConstants.COMPARE_RESULT_EQUAL
     *           - A <  B : EngineConstants.COMPARE_RESULT_LOWER
     *
     * @author Georg Zoeller
     *  --------------------------------------------------------------------------**/
     public int CompareFloat(float fA, float fB)
     {
          if (fA > fB)
          {
               return EngineConstants.COMPARE_RESULT_HIGHER;
          }
          else if (fA == fB)
          {
               return EngineConstants.COMPARE_RESULT_EQUAL;
          }
          else
          {
               return EngineConstants.COMPARE_RESULT_LOWER;
          }
     }

     /*
     * @brief Returns whether or not an ability is a modal ability
     *
     * @param oAttacker          The Attacking Creature
     * @param oTarget            The attacked creature
     *
     * @author   Georg Zoeller

     **/
     public int Ability_IsModalAbility(int nAbility)
     {
          return GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "usetype", nAbility) == 2 ? EngineConstants.TRUE : EngineConstants.FALSE;

     }

     /*
     * @brief (core_h)Returns the state of a creature flag
     *
     * A creature flag (EngineConstants.CREATURE_FLAG_*) is a persistent boolean variable
     *
     * @param oCreature The creature to check
     *
     * @returns  EngineConstants.TRUE or EngineConstants.FALSE state of the flag.
     *
     * @author Georg Zoeller
     */
     public int GetCreatureFlag(GameObject oCreature, int nFlag)
     {
          int nVal = GetLocalInt(oCreature, EngineConstants.CREATURE_RULES_FLAG0);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "core_h.GetCreatureFlag", "Flag: " + IntToHexString(nFlag) + " Value: " + IntToHexString(nVal) + " Result: " + IntToString(((nVal & nFlag) == nFlag ? EngineConstants.TRUE : EngineConstants.FALSE)), oCreature);

          return ((nVal & nFlag) == nFlag ? EngineConstants.TRUE : EngineConstants.FALSE);
     }

     public int GetAreaId(GameObject oArea)
     {
          return GetLocalInt(oArea, "AREA_ID");
     }

     /*
     * @brief (core_h)Returns the state of an area flag.
     *
     * EngineConstants.AREA_FLAGs are static and defined in area_data.xls
     *
     * @returns  EngineConstants.TRUE or EngineConstants.FALSE state of the flag.
     *
     * @author Georg Zoeller
     */
     public int GetAreaFlag(GameObject oArea, int nFlag)
     {

          int nAreaId = GetAreaId(oArea);
          int nVal = GetM2DAInt(225, "AreaFlags", nAreaId);

          return ((nVal & nFlag) == nFlag ? EngineConstants.TRUE : EngineConstants.FALSE);
     }

     /*
     * @brief (core_h)Returns creature appearance flags (from apr_base)
     *
     * @param oCreature The creature to check
     *
     * @returns  EngineConstants.TRUE or EngineConstants.FALSE state of the flag.
     *
     * @author Georg Zoeller
     */
     public int GetCreatureAppearanceFlag(GameObject oCreature, int nFlag)
     {
          int nVal = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "AprRulesFlags", GetAppearanceType(oCreature));
          return ((nVal & nFlag) == nFlag ? EngineConstants.TRUE : EngineConstants.FALSE);
     }

     /*
     * @brief (core_h)Returns EngineConstants.TRUE if a creature is currently dying or has been dealt a deathblow
     *
     * @param oCreature The creature to check
     *
     * @returns  EngineConstants.TRUE or EngineConstants.FALSE
     *
     * @author Georg Zoeller
     **/
     public int IsDying(GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          // Death xEffect is present even before the creature is considered fully dead.
          int bRet = HasDeathEffect(oCreature, EngineConstants.TRUE);
          //int bRet = GetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_DYING);

#if DEBUG
          if (bRet != EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "core_h.GetCreatureFlag", "IsDying EngineConstants.TRUE", oCreature);
#endif

          return (bRet);

     }

     /* ----------------------------------------------------------------------------
     * @brief (core_h) Check if the creature is 'disabled' (negative effect)
     *
     * @param oCreature The creature to set the flag on
     *
     * @returns  EngineConstants.TRUE or EngineConstants.FALSE
     *
     * @author Georg Zoeller
     *  ---------------------------------------------------------------------------**/
     public int IsDisabled(GameObject oCreature = null, int bGroundCheck = EngineConstants.FALSE)
     {
          if (oCreature == null) oCreature = gameObject;
          /* List<xEffect> effectsArray = GetEffects(oCreature, EngineConstants.EFFECT_TYPE_KNOCKDOWN);
           xEffect eCurrentEffect;
           int nSize = GetArraySize(effectsArray);*/

          return (EngineConstants.FALSE/*nSize>0*/);

     }

     public int IsInjuryEffect(xEffect e)
     {

          int nId = GetEffectAbilityIDRef(ref e);
          return (nId > EngineConstants.INJURY_ABILITY_EFFECT_ID && nId < EngineConstants.INJURY_ABILITY_EFFECT_ID + EngineConstants.INJURY_MAX_DEFINES) ? EngineConstants.TRUE : EngineConstants.FALSE;

     }

     /* ----------------------------------------------------------------------------
     * @brief (core_h) Remove all effects from a creature
     *
     * @param oCreature The creature to clear all effects off
     *
     * @author Georg Zoeller
     *  ---------------------------------------------------------------------------**/
     public void Effects_RemoveAllEffects(GameObject oCreature, int bIgnoreInjuries = EngineConstants.TRUE, int bDeath = EngineConstants.FALSE)
     {

          List<xEffect> effectsArray = GetEffects(oCreature);
          xEffect eCurrentEffect;
          int nId;
          int nSize = GetArraySize(effectsArray);
          int i;
          for (i = 0; i < nSize; i++)
          {

               eCurrentEffect = effectsArray[i];

               if (IsInjuryEffect(eCurrentEffect) == EngineConstants.FALSE || bIgnoreInjuries == EngineConstants.FALSE)
               {
                    if (bDeath == EngineConstants.FALSE || GetM2DAInt(EngineConstants.TABLE_EFFECTS, "IgnoreDeath", GetEffectTypeRef(ref eCurrentEffect)) == 0)
                    {
#if DEBUG
                         LogTrace(EngineConstants.LOG_CHANNEL_TEMP, Log_GetEffectNameById(GetEffectTypeRef(ref eCurrentEffect)));
#endif
                         RemoveEffect(oCreature, eCurrentEffect);
                    }
               }
          }
     }

     public void Effects_RemoveEffectByType(GameObject oCreature, int nEffectType)
     {
          RemoveEffectsByParameters(oCreature, nEffectType);
     }

     public void RemoveStackingEffects(GameObject oTarget, GameObject oCaster, int nAbility)
     {

          List<xEffect> thisEffects = GetEffects(oTarget, EngineConstants.EFFECT_TYPE_INVALID, nAbility);
          int nSize = GetArraySize(thisEffects);
          int i = 0;
          for (i = 0; i < nSize; i++)
          {
               xEffect e = thisEffects[i];
               if (IsEffectValid(e) != EngineConstants.FALSE)
               {
                    // -----------------------------------------------------------------
                    // Georg: Ok, this is a bit obscure:
                    //        If the executing context for an xEffect is an Area Of Effect Object
                    //        we can assume (safely) that it should only be removed if the
                    //        creator of the xEffect is identical to the caster.
                    //
                    //        The reason this check is needed is to allow multiple, overlapping
                    //        effects by the same ability - from different casters (such as
                    //        two enemies casting blizzard on the party.
                    //
                    //        The check ensures that when one of the spells times out
                    //        the xEffect of the second spell stay on the target and thus
                    //        avoids desynchronizing AoEs from their effects.
                    // -----------------------------------------------------------------
                    if (GetObjectType(gameObject) == EngineConstants.OBJECT_TYPE_AREAOFEFFECTOBJECT)
                    {
                         if (oCaster == GetEffectCreatorRef(ref e) || IsObjectValid(GetEffectCreatorRef(ref e)) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_Effects("RemoveStackingEffects", e, "removing stacking effect", oTarget);
#endif
                              RemoveEffect(oTarget, e);

                         }
                         else
                         {
#if DEBUG
                              Log_Trace_Effects("RemoveStackingEffects", e, "NOT removing stacking xEffect from AoE due to not matching creator " + ToString(oCaster) + " " + ToString(GetEffectCreatorRef(ref e)), oTarget);
#endif
                         }
                    }
                    else
                    {
#if DEBUG
                         Log_Trace_Effects("RemoveStackingEffects", e, "removing stacking effect", oTarget);
#endif
                         RemoveEffect(oTarget, e);
                    }
               }
          }
     }

     public void RemoveStackingEffectsFromParty(GameObject oCaster, int nAbility)
     {
          List<GameObject> arParty = GetPartyList();

          int nSize = GetArraySize(arParty);
          int i = 0;

          for (i = 0; i < nSize; i++)
          {
               RemoveStackingEffects(arParty[i], oCaster, nAbility);
          }
     }

     /*-----------------------------------------------------------------------------
     *@brief Gets the EngineConstants.VFX_* constant representing the kind of the visual effect.
     *
     * @param eVFX   The visual effect.
     * @returns      The visual xEffect ID (EngineConstants.VFX_*) of the visual effect
     *               or EngineConstants.VFX_INVALID if the visual xEffect is invalid.
     *
     * @author       dsitar
     *-----------------------------------------------------------------------------*/
     public int GetVisualEffectID(xEffect eVFX)
     {
          if (GetEffectTypeRef(ref eVFX) == EngineConstants.EFFECT_TYPE_VISUAL_EFFECT)
               return GetEffectIntegerRef(ref eVFX, 0);
          return EngineConstants.VFX_INVALID;
     }

     /*-----------------------------------------------------------------------------
     * @brief Removes a specific visual xEffect from an object.
     *
     * @param oTarget The GameObject to remove the visual xEffect from.
     * @param nVfxID  The visual xEffect ID to remove (EngineConstants.VFX_*).
     *
     * @author        dsitar
     *-----------------------------------------------------------------------------*/
     public void RemoveVisualEffect(GameObject oTarget, int nVfxID)
     {
          List<xEffect> aEffects = GetEffects(oTarget, EngineConstants.EFFECT_TYPE_VISUAL_EFFECT);
          int nEffects = GetArraySize(aEffects);
          int i;
          for (i = 0; i < nEffects; i++)
          {
               if (GetVisualEffectID(aEffects[i]) == nVfxID)
               {
                    RemoveEffect(oTarget, aEffects[i]);
               }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Removes multiple visual effects from an object.
     *
     * @param oTarget The GameObject to remove the visual xEffect from.
     * @param aVfxID  Array of visual xEffect IDs to remove (EngineConstants.VFX_*).
     *
     * @author        dsitar
     *-----------------------------------------------------------------------------*/
     public void RemoveVisualEffects(GameObject oTarget, List<int> aVfxID)
     {
          List<xEffect> aEffects = GetEffects(oTarget, EngineConstants.EFFECT_TYPE_VISUAL_EFFECT);
          int nEffects = GetArraySize(aEffects);
          int nVfxIDs = GetArraySize(aVfxID);
          int i, j;
          for (i = 0; i < nEffects; i++)
          {
               for (j = 0; j < nVfxIDs; j++)
               {
                    if (GetVisualEffectID(aEffects[i]) == aVfxID[j])
                    {
                         RemoveEffect(oTarget, aEffects[i]);
                    }
               }
          }
     }

     /* ----------------------------------------------------------------------------
     * @brief (core_h) Wrapper for ApplyEffectOnObject.
     *
     * @param nDurationType can be EngineConstants.EFFECT_DURATION_TYPE_PERMANENT EngineConstants.EFFECT_DURATION_TYPE_INSTANTANEOUS or EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY.
     * @param Effect the xEffect to be applied
     * @param oTarget the target of the effect
     * @param fDuration  this value needs to be set only when nDurationType is EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY
     * @param oCreator xEffect creator
     * @param nAbilityId The ability ID of the xEffect (Important for dispelling!!!)
     *
     * @author Georg Zoeller
     *  ---------------------------------------------------------------------------**/
     public void ApplyEffectOnObject(int nDurationType, xEffect eEffect, GameObject oTarget, float fDuration = 0.0f, GameObject oCreator = null, int nAbilityId = 0)
     {
          if (oCreator == null) oCreator = gameObject;
          // -------------------------------------------------------------------------
          // For stun specifically, we apply a marking xEffect for 15 secs
          // that degrades incoming stun effects to 1/4 of their potency
          // -------------------------------------------------------------------------
          if (GetEffectTypeRef(ref eEffect) == EngineConstants.EFFECT_TYPE_STUN)
          {
               if (nDurationType == EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY)
               {
                    if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_RECENTLY_STUNNED, 0) != EngineConstants.FALSE)
                    {
                         fDuration *= 0.25f;
                    }
                    else
                    {
                         Engine_ApplyEffectOnObject(nDurationType,
                                             Effect(EngineConstants.EFFECT_TYPE_RECENTLY_STUNNED),
                                             oTarget,
                                             15.0f,
                                             oCreator,
                                             0 /*invisible*/);
                    }
               }
          }

          Engine_ApplyEffectOnObject(nDurationType,
                                  eEffect,
                                  oTarget,
                                  fDuration,
                                  oCreator,
                                  nAbilityId);

#if DEBUG
          Log_Trace_Effects("core_h.ApplyEffectOnObject", eEffect, ToString(fDuration), oTarget, nDurationType, nAbilityId);
#endif
     }

     /* ----------------------------------------------------------------------------
     * @brief (core_h) Wrapper for ApplyEffectOnObject on the Party.
     *
     * @param nDurationType can be EngineConstants.EFFECT_DURATION_TYPE_PERMANENT EngineConstants.EFFECT_DURATION_TYPE_INSTANTANEOUS or EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY.
     * @param Effect the xEffect to be applied
     * @param oTarget the target of the effect
     * @param fDuration  this value needs to be set only when nDurationType is EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY
     * @param oCreator xEffect creator
     * @param nAbilityId The ability ID of the xEffect (Important for dispelling!!!)
     *
     * @author Georg Zoeller
     *  ---------------------------------------------------------------------------**/
     public void ApplyEffectOnParty(int nDurationType, xEffect eEffect, float fDuration = 0.0f, GameObject oCreator = null, int nAbilityId = 0, int bIncludeSummons = EngineConstants.TRUE, int bExcludeCreator = EngineConstants.FALSE)
     {
          if (oCreator == null) oCreator = gameObject;
          // Georg: Move this function into the engine for performance reasons

          Engine_ApplyEffectOnParty(nDurationType, eEffect, fDuration, oCreator, nAbilityId, bExcludeCreator);
     }

     /*
     * @brief (core_h) Clamp an Integer value to nMin / nMax
     *
     * @param nVal The value
     * @param nMin The bottom
     * @param nMax The ceiling
     *
     * @returns  integer capped by ceiling and bottom
     *
     * @author Georg Zoeller
     */
     public int ClampInt(int nVal, int nMin, int nMax)
     {
          return ((nVal) < (nMin) ? (nMin) : (nVal) > (nMax) ? (nMax) : (nVal));
     }

     /*
        @brief Returns the greater value of f1 and f2

        @param f1, f2 - floats

        @returns the greater value of both
     */

     public int Max(int n1, int n2)
     {
          return ((n1 > n2) ? n1 : n2);
     }

     /*
        @brief Returns the greater value of f1 and f2

        @param f1, f2 - floats

        @returns the greater value of both
     */

     public int Min(int n1, int n2)
     {
          return ((n1 < n2) ? n1 : n2);
     }

     /*
        @brief Returns the greater value of f1 and f2

        @param f1, f2 - floats

        @returns the greater value of both
     */

     public float MaxF(float f1, float f2)
     {
          return ((f1 > f2) ? f1 : f2);
     }

     /*
        @brief Returns the lesser value of f1 and f2

        @param f1, f2 - floats

        @returns the lesser value of both
     */
     public float MinF(float f1, float f2)
     {
          return ((f1 < f2) ? f1 : f2);
     }

     /*
     * @brief (core_h) Hack for GetObjectsInRadius
     *
     * @author Georg Zoeller
     *
     **/
     public List<GameObject> GetHostileObjectsInRadius(GameObject oTarget, GameObject oHostilityRef, int nObjectType = EngineConstants.OBJECT_TYPE_ALL, float fRange = 10.0f)
     {

          List<GameObject> arTemp = GetNearestObject(oTarget, nObjectType, 20);

          List<GameObject> arRet = new List<GameObject>();

          int nCount = GetArraySize(arTemp);
          int i;

          int c = 0;

          for (i = 0; i < nCount && GetDistanceBetweenLocations(GetLocation(arTemp[i]), GetLocation(oTarget)) <= fRange; i++)
          {

               if (IsObjectHostile(arTemp[i], oHostilityRef) != EngineConstants.FALSE)
               {
                    arRet[c] = arTemp[i];
                    c++;
               }

          }

          if (IsObjectHostile(oTarget, oHostilityRef) != EngineConstants.FALSE)
          {
               arRet[c] = oTarget;
          }

          return arRet;

     }

     // return the index of the first occurence of an integer in an integer array
     public int GetIntArrayIndex(List<int> arArray, int nItem)
     {
          int nSize = GetArraySize(arArray);
          int bFound = 0;

          int i;
          for (i = 0; i < nSize && bFound == EngineConstants.FALSE; i++)
          {
               bFound = (arArray[i] == nItem) ? EngineConstants.TRUE : EngineConstants.FALSE;
          }

          return (bFound != EngineConstants.FALSE ? i : -1);
     }

     // return the index of the first occurence of an integer in an integer array
     public int GetObjectArrayIndex(List<GameObject> arArray, GameObject oItem)
     {
          int nSize = GetArraySize(arArray);
          int bFound = 0;

          int i;
          for (i = 0; i < nSize && bFound == EngineConstants.FALSE; i++)
          {
               bFound = (arArray[i] == oItem) ? EngineConstants.TRUE : EngineConstants.FALSE;
          }

          return (bFound != EngineConstants.FALSE ? i : -1);
     }

     /*
     * @brief (core_h) Returns the ability type as a EngineConstants.ABILITY_TYPE_* constant
     *
     * @param nAbility   ability id (row number in EngineConstants.ABI_BASE)
     *
     * @returns EngineConstants.ABILITY_TYPE_* constant
     *
     * @author   Georg Zoeller
     **/
     public int Ability_GetAbilityType(int nAbility)
     {
          return GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "AbilityType", nAbility);
     }

     public int IsSpell(int nAbility)
     {
          return Ability_GetAbilityType(nAbility) == EngineConstants.ABILITY_TYPE_SPELL ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     public int IsTalent(int nAbility)
     {
          return Ability_GetAbilityType(nAbility) == EngineConstants.ABILITY_TYPE_TALENT ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     public int IsSkill(int nAbility)
     {
          return Ability_GetAbilityType(nAbility) == EngineConstants.ABILITY_TYPE_SKILL ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     public List<xEffect> GetEffectsByAbilityId(GameObject oObject, int nAbilityId)
     {
          List<xEffect> arrRet = GetEffects(oObject, EngineConstants.EFFECT_TYPE_INVALID, nAbilityId);
          return arrRet;
     }

     /*
     * @brief return the EngineConstants.ITEM_TYPE_* of an item
     *
     * Returns the type an item belongs to (e.g. EngineConstants.ITEM_TYPE_WEAPON_RANGED) from
     * BITEM_base.xls, column "Type"
     *
     * @ param oItem - an Item
     *
     * @author Georg
     **/
     public int GetItemType(GameObject oItem)
     {
          int nBaseItemType = GetBaseItemType(oItem);

          int nType = GetM2DAInt(EngineConstants.TABLE_ITEMS, "Type", nBaseItemType);

          return nType;
     }

     /*
     *  @brief returns true if creature x is using a ranged weapon
     *  @param oCreature the creature to thest
     *  @param oItem an optional item to test (otherwise the main hand will be tested)
     *  @param bExlcudeWand use to exclude wand (e.g. check specifically for box/xbow)
     *  @author Georg
     **/
     public int IsUsingRangedWeapon(GameObject oCreature, GameObject oItem = null, int bExludeWand = EngineConstants.FALSE)
     {
          // Shapeshifting currently only supports melee weapons.
          if (IsShapeShifted(oCreature) != EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          if (IsObjectValid(oItem) == EngineConstants.FALSE)
          {
               oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oCreature);
          }

          if (IsObjectValid(oItem) != EngineConstants.FALSE)
          {

               return (GetItemType(oItem) == EngineConstants.ITEM_TYPE_WEAPON_RANGED ||
                       (bExludeWand == EngineConstants.FALSE && GetItemType(oItem) == EngineConstants.ITEM_TYPE_WEAPON_WAND)) ? EngineConstants.TRUE : EngineConstants.FALSE;
          }

          return EngineConstants.FALSE;
     }

     /*
     *  @brief returns true if creature x is using a ranged weapon
     *  @param oCreature the creature to thest
     *  @param oItem an optional item to test (otherwise the main hand will be tested)
     *  @author Georg
     **/
     public int IsUsingShield(GameObject oCreature, GameObject oItem = null)
     {

          if (IsObjectValid(oItem) == EngineConstants.FALSE)
          {
               oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, oCreature);
          }

          if (IsObjectValid(oItem) != EngineConstants.FALSE)
          {
               return (GetItemType(oItem) == EngineConstants.ITEM_TYPE_SHIELD) ? EngineConstants.TRUE : EngineConstants.FALSE;
          }

          return EngineConstants.FALSE;
     }

     public int Is2HandItem(GameObject oItem)
     {
          return (GetM2DAInt(EngineConstants.TABLE_ITEMS, "EquippableSlots", GetBaseItemType(oItem)) == 1) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
     *  @brief returns true if creature x is using a melee weapon (or fists)
     *  @param oCreature the creature to thest
     *  @param oItem an optional item to test (otherwise the main hand will be tested)
     *  @author Georg
     **/
     public int IsUsingMeleeWeapon(GameObject oCreature, GameObject oItem = null)
     {

          if (IsObjectValid(oItem) == EngineConstants.FALSE)
          {
               oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oCreature);
          }
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "IsUsingMeleeWeapon", "creature: " + ToString(oCreature) + ", Weapon= " + ToString(oItem) + ", type: " + ToString(GetItemType(oItem)));
          if (IsObjectValid(oItem) != EngineConstants.FALSE && IsShapeShifted(oCreature) == EngineConstants.FALSE)
          {
               return (GetItemType(oItem) == EngineConstants.ITEM_TYPE_WEAPON_MELEE) ? EngineConstants.TRUE : EngineConstants.FALSE;
          }
          else    // fists
          {
               return EngineConstants.TRUE;
          }

          return EngineConstants.FALSE;
     }

     /*
     *  @brief Gets an object's current health
     *  @sa    SetCurrentHealth
     *  @author Georg
     **/
     public float GetCurrentHealth(GameObject oObject)
     {

          float fHealth;

          int nObjectType = GetObjectType(oObject);
          if (nObjectType == EngineConstants.OBJECT_TYPE_PLACEABLE)
          {
               fHealth = IntToFloat(GetHealth(oObject));
          }
          else if (nObjectType == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               fHealth = GetCreatureProperty(oObject, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, EngineConstants.PROPERTY_VALUE_CURRENT);
          }
          else
          {
               fHealth = IntToFloat(GetHealth(oObject));
          }

          return fHealth;
     }

     /*
     *  @brief Set's an object's current health
     *  @sa    GetCurrentHealth
     *  @author Georg
     **/
     public void SetCurrentHealth(GameObject oObject, float fNewValue)
     {

          if (IsObjectValid(oObject) != EngineConstants.FALSE)
          {
               int nObjectType = GetObjectType(oObject);
               if (nObjectType == EngineConstants.OBJECT_TYPE_PLACEABLE)
               {
                    SetPlaceableHealth(oObject, FloatToInt(fNewValue));
               }
               else if (nObjectType == EngineConstants.OBJECT_TYPE_CREATURE)
               {
                SetCreatureProperty(oObject, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, fNewValue, EngineConstants.PROPERTY_VALUE_CURRENT);
            }
               else
               {
#if DEBUG
                    Warning("SetCurrentHealth called on GameObject [" + GetTag(oObject) + "] " + "[type: " + IntToString(nObjectType) + "] that is not a creature or placeable. Please contact Georg. Script:" + GetCurrentScriptName());
#endif
               }
          }

     }

     /*
     *  @brief Returns a creatures maximum health
     *  @author Georg
     **/
     public float GetMaxHealth(GameObject oObject)
     {
          int nObjectType = GetObjectType(oObject);
          if (nObjectType == EngineConstants.OBJECT_TYPE_PLACEABLE)
          {
               return IntToFloat(Engine_GetMaxHealth(oObject));
          }
          else if (nObjectType == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               return GetCreatureProperty(oObject, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, EngineConstants.PROPERTY_VALUE_TOTAL);
          }
          else
          {
#if DEBUG
               Warning("GetCurrentHealth called on GameObject [" + GetTag(oObject) + "] that is not a creature or placeable. Please contact Georg. Script:" + GetCurrentScriptName());
#endif
               return 1.0f;
          }

     }

     public float GetCreatureSpellPower(GameObject oCreature)
     {
          return GetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_SPELLPOWER, EngineConstants.PROPERTY_VALUE_TOTAL);
     }

     public int GetLevel(GameObject oCreature)
     {
#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "GetLevel: " + ToString(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_LEVEL, EngineConstants.PROPERTY_VALUE_TOTAL)));
#endif
          return FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_LEVEL, EngineConstants.PROPERTY_VALUE_TOTAL));
     }

     public int IsWounded(GameObject oObject)
     {

          return (GetCurrentHealth(oObject) < GetMaxHealth(oObject)) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
     *  @brief Returns the a random Integer cast to float between 0 and nBase
     *                                                                       \
     *  optionally adds nAdd to the result.
     *
     *  @author Georg
     **/
     public float RandomF(int nBase, int nAdd = 0)
     {
          return IntToFloat(Engine_Random(nBase) + nAdd);
     }

     public float GetCreatureAttackRating(GameObject oCreature)
     {
          return GetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_ATTACK);
     }

     /*
     *  @brief Returns the creature critical hit modifier used for combat
     *
     *  There are 3 different critical hit modifiers on a creature: Melee, Magic and
     *  Ranged.
     *
     *  @param oCreature the creature to retrieve the stat from
     *  @param nCritModifier A EngineConstants.CRITICAL_MODIFIER_* constant as follows:
     *                       EngineConstants.CRITICAL_MODIFIER_MELEE
     *                       EngineConstants.CRITICAL_MODIFIER_MAGIC
     *                       EngineConstants.CRITICAL_MODIFIER_RANGED
     *
     *  @author Georg
     **/
     public float GetCreatureCriticalHitModifier(GameObject oCreature, int nCritModifier)
     {
          float fRet = GetCreatureProperty(oCreature, nCritModifier, EngineConstants.PROPERTY_VALUE_TOTAL);
#if DEBUG
          float fMod = GetCreatureProperty(oCreature, nCritModifier, EngineConstants.PROPERTY_VALUE_MODIFIER);
          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "core_h.GetCreatureCriticalHitModifier", "Prop (" + ToString(nCritModifier) + ") on " + GetTag(oCreature) + " is " + ToString(fRet) + " modifier is: " + ToString(fMod));
#endif

          return fRet;
     }

     /*
     *  @brief Returns a creatures defense rating. This does not exclude shields
     *  @sa    GetCreatureShieldRating
     *  @author Georg
     **/
     public float GetCreatureDefense(GameObject oCreature)
     {
          float fRet = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE);
#if DEBUG
          float fMod = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE, EngineConstants.PROPERTY_VALUE_MODIFIER);
          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "core_h.GetCreatureDefense", "Defense on " + GetTag(oCreature) + " is " + ToString(fRet) + " modifier is: " + ToString(fMod));
#endif

          return fRet;
     }

     /*
     *  @brief Returns a creatures current mana/stamina
     *  @sa    SetCurrentManaStamina
     *  @author Georg
     **/
     public float GetCurrentManaStamina(GameObject oObject)
     {
          return GetCreatureProperty(oObject, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT);
     }

     /*
     *  @brief Small helper for dealing with random floats in some combat functions
     *  @author Georg
     **/
     public float RandFF(float fRange, float fStatic = 0.0f, int bDeterministic = EngineConstants.FALSE)
     {
          if (bDeterministic != EngineConstants.FALSE) /* used for UI display purposes)*/
          {
               return fRange * 0.5f + fStatic;
          }
          else
          {
               return RandomFloat() * fRange + fStatic;
          }
     }

     /*
     *   @brief returns the time for the ranged aim loop delay
     *
     *   The ranged aim loop is used on bows and crossbows and controls the rate of
     *   fire for the weapon by telling the engine how long to loop the aim animation
     *   before releasing the projectile. This function calculates the lengths of
     *   the aimloop for a particular character and weapon, which is later passed back
     *   to the engine via SetAimLoop function in the CommandPending event.
     *
     *   @param oShooter The creature shooting the weapon
     *   @param oWeapon  The ranged weapon used
     *
     *   @returns Calculated time of the aimloop, mininal 0.0f.
     *
     *   @author georg
     *
     **/
     public float GetCreatureRangedDrawSpeed(GameObject oCreature, GameObject oWeapon = null)
     {
          float fRet = 0.0f;

          // -- Get Total Draw Speed
          float fTotal = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_RANGED_AIM_SPEED, EngineConstants.PROPERTY_VALUE_TOTAL);

          // -- Get Mod (for debug output only)
          float fMod = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_RANGED_AIM_SPEED, EngineConstants.PROPERTY_VALUE_MODIFIER);

          // -- GetWeapon Draw Speed (can't be negative)
          float fWeapon = 0.0f;

          if (IsObjectValid(oWeapon) != EngineConstants.FALSE)
          {
               fWeapon = MaxF(GetM2DAFloat(EngineConstants.TABLE_ITEMSTATS, "BaseAimDelay", GetBaseItemType(oWeapon)), 0.0f);
          }

          fRet = fTotal + fWeapon;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "core_h.GetCreatureRangedDrawSpeed", "DrawSpeed on " + GetTag(oCreature) + " is " + ToString(fRet) + " modifier is: " + ToString(fMod) + " Weapon (" + ToString(oWeapon) + "):" + ToString(fWeapon));
#endif

          return MaxF(fRet, 0.0f);
     }

     public float GetAttributeModifier(GameObject oCreature, int nAttribute)
     {

          float fValue = GetCreatureProperty(oCreature, nAttribute) - EngineConstants.RULES_ATTRIBUTE_MODIFIER;

          return MaxF(fValue, 0.0f);

     }

     /*
     *   @brief returns if the creature can be deathblowed
     *
     *   @author georg
     **/
     public int CanDeathBlow(GameObject oCreature)
     {
          return EngineConstants.TRUE;// IsHumanoid(oCreature);
     }

     /*
     *   @brief Returns a creature's core class (mage, rogue, warrior)
     *
     *   @author georg
     **/
     public int GetCreatureCoreClass(GameObject oCreature)
     {
          int nCurrentClass = FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_CURRENT_CLASS));

          int nCoreClass = GetM2DAInt(EngineConstants.TABLE_RULES_CLASSES, "BaseClass", nCurrentClass);

          if (nCoreClass == 0) // the current is the core
               return nCurrentClass;
          else
               return nCoreClass;
     }

     /*
     *   @brief Returns a creature's current class (bard, assassin etc')
     *
     *   @author yaron
     **/
     public int GetCreatureCurrentClass(GameObject oCreature)
     {
          int nCurrentClass = FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_CURRENT_CLASS));

          return nCurrentClass;
     }

     /*
     *   @brief returns if the creature can be deathblowed
     *
     *   [core_h] Sets the requested combat state on all party members
     *
     *   @param bCombatState True to set combat state, false to unset
     *
     *   @author georg
     **/
     public void SetCombatStateParty(int bCombatState)
     {
          List<GameObject> arrParty = GetPartyList(GetHero());
          int nMemberCount = GetArraySize(arrParty);

          GameObject oSubject;
          int iter;
          for (iter = 0; iter < nMemberCount; iter++)
          {
               oSubject = arrParty[iter];
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "core_h.SetCombatStateParty", "Setting Combat State " + ToString(bCombatState), arrParty[iter]);
#endif
               if (GetCombatState(oSubject) != bCombatState)
               {
                    SetCombatState(oSubject, bCombatState);
               }
          }

     }

     /*
     *   @brief Returns whether or not a creature has a skill
     *
     *   [core_h] Returns true if a creature has nLevel ranks in a skill. To
     *            test if a creature has any level/rank, just test for rank 1)
     *
     *            Note that this function relies on skill ranks  existing sequential
     *            in abi_base.xls.
     *
     *   @param nSkill       EngineConstants.ABILITY_SKILL_* constant
     *   @param nSkill       Minimum skill ranks requrired to return true
     *   @param oCreature    The creature to check.
     *
     *   @returns    True if creature has at least nLevel ranks in nSkill
     *
     *   @author georg
     **/
     public int GetHasSkill(int nSkill, int nLevel = 1, GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          // -------------------------------------------------------------------------
          // Trap scripter error: invalid GameObject passed in
          // -------------------------------------------------------------------------
          if (IsObjectValid(oCreature) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_SCRIPTERROR, "core_h.GetHasSkill", "INVALID_OBJECT passed into function as oCreature", oCreature);
#endif
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // Trap scripter error: nLevel out of bounds
          // -------------------------------------------------------------------------
          if (nLevel > EngineConstants.MAX_SKILL_RANKS)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_SCRIPTERROR, "core_h.GetHasSkill", "nLevel out of bounds:" + ToString(nLevel), oCreature);
#endif
               nLevel = EngineConstants.MAX_SKILL_RANKS;
          }
          else if (nLevel < 1)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_SCRIPTERROR, "core_h.GetHasSkill", "nLevel out of bounds:" + ToString(nLevel), oCreature);
#endif
               nLevel = 1;
          }

          return (HasAbility(oCreature, (nSkill + (nLevel - 1))));

     }

     /*
     *   @brief Returns whether the whole party has been wiped
     *
     *   [core_h] Returns true if all party members are dead.
     *
     *   @returns    True if all party members are dead.
     *
     *   @author georg
     **/
     public int IsPartyDead()
     {
          List<GameObject> partyMembers = GetPartyList();
          int nMembers = GetArraySize(partyMembers);
          int i;

          int bAllDead = EngineConstants.TRUE;
          for (i = 0; i < nMembers; i++)
          {
               if (IsDead(partyMembers[i]) == EngineConstants.FALSE)
               {
                    bAllDead = EngineConstants.FALSE;
                    break;
               }
          }
          return bAllDead;
     }

     /*
     *   @brief returns proper tag for item based on it's string
     *
     *   @author joshua
     **/
     public string ResourceToTag(string rResource)
     {
          string sRes = ResourceToString(rResource);
          return SubString(sRes, 0, FindSubString(sRes, "."));
     }

     /*
     *   @brief Returns the experience points a party member has
     *
     *   @author georg
     **/
     public int GetExperience(GameObject oPartyMember)
     {
          return FloatToInt(GetCreatureProperty(oPartyMember, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE));
     }

     /*
     *   @brief Returns if a creature is immune to a specific xEffect type.
     *
     *   @author georg
     **/
     public int IsImmuneToEffectType(GameObject oCreature, int nEffectType)
     {

          // EngineConstants.ABILITY_TALENT_INDOMITABLE grants immunity to knockdown and stun
          if (nEffectType == EngineConstants.EFFECT_TYPE_KNOCKDOWN || nEffectType == EngineConstants.EFFECT_TYPE_STUN || nEffectType == EngineConstants.EFFECT_TYPE_SLIP)
          {
               if (IsModalAbilityActive(oCreature, EngineConstants.ABILITY_TALENT_INDOMITABLE) != EngineConstants.FALSE)
               {
                    return 3;
               }

               // GXA Override
               if (HasAbility(oCreature, 401100) != EngineConstants.FALSE) // GXA Spirit Warrior
               {
                    if (IsModalAbilityActive(oCreature, 401100) != EngineConstants.FALSE) // GXA Spirit Warrior
                    {
                         return 3;
                    }
               }

               if (HasAbility(oCreature, 401301) != EngineConstants.FALSE) // GXA Implacable Intent
               {
                    if (IsModalAbilityActive(oCreature, 401301) != EngineConstants.FALSE) // GXA Implacable Intent
                    {
                         return 3;
                    }
               }

               if ((nEffectType != EngineConstants.EFFECT_TYPE_STUN) && (HasAbility(oCreature, 401200) != EngineConstants.FALSE)) // GXA One With Nature
               {
                    if (IsModalAbilityActive(oCreature, 401200) != EngineConstants.FALSE)
                    {
                         return 3;
                    }
               }
               // GXA Override

               // trait sturdy does this too.
               if (HasAbility(oCreature, EngineConstants.ABILITY_TRAIT_STURDY) != EngineConstants.FALSE)
               {
                    return 1;
               }

               if (nEffectType == EngineConstants.EFFECT_TYPE_KNOCKDOWN && HasAbility(oCreature, EngineConstants.ABILITY_TALENT_SHIELD_EXPERTISE) != EngineConstants.FALSE && IsModalAbilityActive(oCreature, EngineConstants.ABILITY_TALENT_SHIELD_WALL) != EngineConstants.FALSE)
               {
                    return 3;
               }

               if (HasAbility(oCreature, EngineConstants.ABILITY_TALENT_EVASION) != EngineConstants.FALSE && Engine_Random(100) < 20)
               {
                    return 3;
               }

          }

          int nImmune = GetM2DAInt(EngineConstants.TABLE_EFFECT_IMMUNITIES, "e" + ToString(nEffectType), GetAppearanceType(oCreature));

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS,
                      "core_h.IsImmuneToEffectType",
                          "Immunity to " + Log_GetEffectNameById(nEffectType) + ": " + ToString(nImmune) + " app:" + ToString(GetAppearanceType(oCreature)),
                              oCreature);
#endif
          return nImmune;
     }

     /* @brief (core_h) Returns true if a creature is one of the 4 controllable party members.
     *   This is a wrapper around IsFollower.
     */
     public int IsPartyMember(GameObject oCreature)
     {
        if (oCreature.GetComponent<xGameObjectBase>().nObjectType != EngineConstants.OBJECT_TYPE_CREATURE)
            return EngineConstants.FALSE;
        else return IsFollower(oCreature);
     }

     /* @brief Converts degrees to radians.
     *
     * @param fDegrees - The value to convert.
     * @returns The value of fDegrees in radians.
     *
     * @author dsitar
     */
     public float ToRadians(float fDegrees)
     {
          return (fDegrees * EngineConstants.PI / 180.0f);
     }

     /* @brief Returns height of a creature based on its appearance.
     */
     public float GetHeight(GameObject oCreature)
     {
          return GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, (GetCreatureGender(oCreature) == EngineConstants.GENDER_FEMALE ? "height_f" : "height"), GetAppearanceType(oCreature));
     }

     /* @brief  Replaces all occurences of a substring within a string.
     *
     * Not to be used for production scripts, please use for debug purposes only.
     *
     * @param    sString  The string to search.
     * @param    sFind    The substring to find.
     * @param    sReplace The string to replace occurences of substring sFind with.
     * @returns           sString with all occurences of substring sFind replaced with sReplace.
     *
     * @author dsitar
     */
     public string ReplaceString(string sString, string sFind, string sReplace)
     {
          string sResult = sString;
          int i = 0;
          while ((i = FindSubString(sResult, sFind, i)) != -1)
          {
               sResult = StringLeft(sResult, i) + sReplace + StringRight(sResult, GetStringLength(sResult) - GetStringLength(sFind) - i);
               i += GetStringLength(sReplace);
          }
          return sResult;
     }

     /* @brief Splits a given string into a string array based on a delimiter.
     *
     * Not to be used for production scripts, please use for debug purposes only.
     *
     * @param sString - The string to split.
     * @param sSeparator - The delimiter string (can be more than 1 character in length).
     * @returns The array of substrings.
     *
     * @author dsitar
     */
     public string[] SplitString(string sString, string sSeparator = " ")
     {
          string[] aString = sString.Split(null);//default
          int i = 0;
          int lpos = 0;
          int pos = FindSubString(sString, sSeparator);

          while (lpos != -1)
          {
               if (IsStringEmpty(SubString(sString, lpos, pos - lpos)) == EngineConstants.FALSE)
               {
                    aString[i++] = SubString(sString, lpos, pos - lpos);
               }
               lpos = (pos == -1) ? -1 : pos + GetStringLength(sSeparator);
               pos = FindSubString(sString, sSeparator, lpos);
          }
          return aString;
     }

     /* @brief Gets parameters to the 'runscript' console xCommand as a string array.
     *
     *  Debug scripts should use this function to get the arguments of the 'runscript' console command.
     *  Not for use in production scripts.
     *
     * @author dsitar
     */
     public string[] GetRunscriptArgs()
     {
          string sArg = GetLocalString(GetModule(), "RUNSCRIPT_VAR");
          SetLocalString(GetModule(), "RUNSCRIPT_VAR", "");
          return SplitString(sArg);
     }

     /* @brief Returns an array of floats representing the atmospheric conditions of a preset such as EngineConstants.ATM_PRESET_DAY
     *
     * @param int nATMPreset - the EngineConstants.ATM_PRESET_* constant to retrieve
     * @returns An array of floats representing the atmospheric conditions of the preset.
     *
     * @author Craig Graff
     */
     public List<float> GetAtmosphericConditions(int nATMPreset)
     {
          List<float> arValues = new List<float>();

          arValues[0] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_SUN_COLOR_RED, nATMPreset);
          arValues[1] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_SUN_COLOR_GREEN, nATMPreset);
          arValues[2] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_SUN_COLOR_BLUE, nATMPreset);
          arValues[3] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_SUN_INTENSITY, nATMPreset);
          arValues[4] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_TURBIDITY, nATMPreset);
          arValues[5] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_EARTH_REFLECTANCE, nATMPreset);
          arValues[6] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_MIE_MULTIPLIER, nATMPreset);
          arValues[7] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_RAYLEIGH_MULTIPLIER, nATMPreset);
          arValues[8] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_EARTHIN_SCATTER_POWER, nATMPreset);
          arValues[9] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_DISTANCE_MULTIPLIER, nATMPreset);
          arValues[10] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_HG, nATMPreset);
          arValues[11] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_ATMOSPHERE_ALPHA, nATMPreset);
          arValues[12] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_MOON_SCALE, nATMPreset);
          arValues[13] = GetM2DAFloat(EngineConstants.TABLE_ATMOSPHERE, EngineConstants.ATM_COLUMN_MOON_ALPHA, nATMPreset);

          return arValues;
     }

     /* @brief Returns an array of floats representing the cloud conditions of a preset such as EngineConstants.ATM_PRESET_CLOUD_DEFAULT
     *
     * @param int nATMPreset - the EngineConstants.ATM_PRESET_CLOUD* constant to retrieve
     * @returns An array of floats representing the cloud conditions of the preset.
     *
     * @author Craig Graff
     */
     public List<float> GetCloudConditions(int nATMCloudPreset)
     {
          List<float> arValues = new List<float>();

          arValues[0] = GetM2DAFloat(EngineConstants.TABLE_CLOUDS, EngineConstants.ATM_COLUMN_CLOUD_COLOR_RED, nATMCloudPreset);
          arValues[1] = GetM2DAFloat(EngineConstants.TABLE_CLOUDS, EngineConstants.ATM_COLUMN_CLOUD_COLOR_GREEN, nATMCloudPreset);
          arValues[2] = GetM2DAFloat(EngineConstants.TABLE_CLOUDS, EngineConstants.ATM_COLUMN_CLOUD_COLOR_BLUE, nATMCloudPreset);
          arValues[3] = GetM2DAFloat(EngineConstants.TABLE_CLOUDS, EngineConstants.ATM_COLUMN_CLOUD_DENSITY, nATMCloudPreset);
          arValues[4] = GetM2DAFloat(EngineConstants.TABLE_CLOUDS, EngineConstants.ATM_COLUMN_CLOUD_SHARPNESS, nATMCloudPreset);
          arValues[5] = GetM2DAFloat(EngineConstants.TABLE_CLOUDS, EngineConstants.ATM_COLUMN_CLOUD_DEPTH, nATMCloudPreset);
          arValues[6] = GetM2DAFloat(EngineConstants.TABLE_CLOUDS, EngineConstants.ATM_COLUMN_CLOUD_RANGE_MULTIPLIER1, nATMCloudPreset);
          arValues[7] = GetM2DAFloat(EngineConstants.TABLE_CLOUDS, EngineConstants.ATM_COLUMN_CLOUD_RANGE_MULTIPLIER2, nATMCloudPreset);

          return arValues;
     }

     /* @brief Returns an array of floats representing the fog conditions of a preset such as EngineConstants.ATM_PRESET_FOG_DEFAULT
     *
     * @param int nATMPreset - the EngineConstants.ATM_PRESET_FOG* constant to retrieve
     * @returns An array of floats representing the fog conditions of the preset.
     *
     * @author Craig Graff
     */
     public List<float> GetFogConditions(int nATMFogPreset)
     {
          List<float> arValues = new List<float>();

          arValues[0] = GetM2DAFloat(EngineConstants.TABLE_FOG, EngineConstants.ATM_COLUMN_FOG_COLOR_RED, nATMFogPreset);
          arValues[1] = GetM2DAFloat(EngineConstants.TABLE_FOG, EngineConstants.ATM_COLUMN_FOG_COLOR_GREEN, nATMFogPreset);
          arValues[2] = GetM2DAFloat(EngineConstants.TABLE_FOG, EngineConstants.ATM_COLUMN_FOG_COLOR_BLUE, nATMFogPreset);
          arValues[3] = GetM2DAFloat(EngineConstants.TABLE_FOG, EngineConstants.ATM_COLUMN_FOG_INTENSITY, nATMFogPreset);
          arValues[4] = GetM2DAFloat(EngineConstants.TABLE_FOG, EngineConstants.ATM_COLUMN_FOG_CAP, nATMFogPreset);
          arValues[5] = GetM2DAFloat(EngineConstants.TABLE_FOG, EngineConstants.ATM_COLUMN_FOG_VERTICAL_ZENITH, nATMFogPreset);

          return arValues;
     }

     /* @brief Sets the fog conditions equal to a preset such as EngineConstants.ATM_PRESET_DAY
     *
     *   Values below EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE will not be applied.
     *
     * @param int ATMFogPreset - the EngineConstants.ATM_PRESET_* constant to set conditions to
     *
     * @author Craig Graff
     */
     public void SetFogConditions(int nATMFogPreset)
     {
          List<float> arFog = GetFogConditions(nATMFogPreset);
          SetAtmosphereRGB(EngineConstants.ATM_PARAM_FOG_COLOR, arFog[0], arFog[1], arFog[2]);
          SetAtmosphere(EngineConstants.ATM_PARAM_FOG_INTENSITY, arFog[3]);
          SetAtmosphere(EngineConstants.ATM_PARAM_FOG_CAP, arFog[4]);
          SetAtmosphere(EngineConstants.ATM_PARAM_FOG_ZENITH, arFog[5]);
     }

     /* @brief Sets the cloud conditions equal to a preset such as EngineConstants.ATM_PRESET_DAY
     *
     *   Values below EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE will not be applied.
     *
     * @param int nATMPreset - the EngineConstants.ATM_PRESET_* constant to set conditions to
     *
     * @author Craig Graff
     */
     public void SetCloudConditions(int nATMCloudPreset)
     {
          List<float> arClouds = GetCloudConditions(nATMCloudPreset);
          SetAtmosphereRGB(EngineConstants.ATM_PARAM_CLOUD_COLOR_RGB, arClouds[0], arClouds[1], arClouds[2]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_DENSITY, arClouds[3]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_SHARPNESS, arClouds[4]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_DEPTH, arClouds[5]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_RANGE_MULTIPLIER1, arClouds[6]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_RANGE_MULTIPLIER2, arClouds[7]);
     }

     /* @brief Sets the cloud conditions equal to a preset such as EngineConstants.ATM_PRESET_CLOUD_DEFAULT
     *
     *   Values below EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE will not be applied.
     *
     * @param int nATMPreset - the EngineConstants.ATM_PRESET_* constant to set conditions to
     *
     * @author Craig Graff
     */
     public void SetAtmosphericConditions(int nATMPreset)
     {
          List<float> arAtm = GetAtmosphericConditions(nATMPreset);
          SetAtmosphereRGB(EngineConstants.ATM_PARAM_SUN_COLOR_RGB, arAtm[0], arAtm[1], arAtm[2]);
          SetAtmosphere(EngineConstants.ATM_PARAM_SUN_INTENSITY, arAtm[3]);
          SetAtmosphere(EngineConstants.ATM_PARAM_TURBIDITY, arAtm[4]);
          SetAtmosphere(EngineConstants.ATM_PARAM_EARTH_REFLECTANCE, arAtm[5]);
          SetAtmosphere(EngineConstants.ATM_PARAM_MIE_MULTIPLIER, arAtm[6]);
          SetAtmosphere(EngineConstants.ATM_PARAM_RAYLEIGH_MULTIPLIER, arAtm[7]);
          SetAtmosphere(EngineConstants.ATM_PARAM_DISTANCE_MULTIPLIER, arAtm[9]);
          SetAtmosphere(EngineConstants.ATM_PARAM_ATMOSPHERE_ALPHA, arAtm[11]);
          SetAtmosphere(EngineConstants.ATM_PARAM_MOON_SCALE, arAtm[12]);
          SetAtmosphere(EngineConstants.ATM_PARAM_MOON_ALPHA, arAtm[13]);
     }

     /* @brief Sets the fog conditions equal to custom values.
     *
     *   Values below EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE will not be applied.
     *
     * @param List<float> arFog - the array of float values to set conditions to
     *
     * @author Craig Graff
     */
     public void SetFogConditionsCustom(List<float> arFog)
     {
          SetAtmosphereRGB(EngineConstants.ATM_PARAM_FOG_COLOR, arFog[0], arFog[1], arFog[2]);
          SetAtmosphere(EngineConstants.ATM_PARAM_FOG_INTENSITY, arFog[3]);
          SetAtmosphere(EngineConstants.ATM_PARAM_FOG_CAP, arFog[4]);
          SetAtmosphere(EngineConstants.ATM_PARAM_FOG_ZENITH, arFog[5]);
     }
     /* @brief Sets the cloud conditions equal to custom values.
     *
     *   Values below EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE will not be applied.
     *
     * @param List<float> arClouds - the array of float values to set conditions to
     *
     * @author Craig Graff
     */
     public void SetCloudConditionsCustom(List<float> arClouds)
     {
          SetAtmosphereRGB(EngineConstants.ATM_PARAM_CLOUD_COLOR_RGB, arClouds[0], arClouds[1], arClouds[2]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_DENSITY, arClouds[3]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_SHARPNESS, arClouds[4]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_DEPTH, arClouds[5]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_RANGE_MULTIPLIER1, arClouds[6]);
          SetAtmosphere(EngineConstants.ATM_PARAM_CLOUD_RANGE_MULTIPLIER2, arClouds[7]);
     }

     /* @brief Sets the cloud conditions equal to custom values.
     *
     *   Values below EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE will not be applied.
     *
     * @param List<float> arAtm - the array of float values to set conditions to
     *
     * @author Craig Graff
     */
     public void SetAtmosphericConditionsCustom(List<float> arAtm)
     {
          SetAtmosphereRGB(EngineConstants.ATM_PARAM_SUN_COLOR_RGB, arAtm[0], arAtm[1], arAtm[2]);
          SetAtmosphere(EngineConstants.ATM_PARAM_SUN_INTENSITY, arAtm[3]);
          SetAtmosphere(EngineConstants.ATM_PARAM_TURBIDITY, arAtm[4]);
          SetAtmosphere(EngineConstants.ATM_PARAM_EARTH_REFLECTANCE, arAtm[5]);
          SetAtmosphere(EngineConstants.ATM_PARAM_MIE_MULTIPLIER, arAtm[6]);
          SetAtmosphere(EngineConstants.ATM_PARAM_RAYLEIGH_MULTIPLIER, arAtm[7]);
          SetAtmosphere(EngineConstants.ATM_PARAM_DISTANCE_MULTIPLIER, arAtm[9]);
          SetAtmosphere(EngineConstants.ATM_PARAM_ATMOSPHERE_ALPHA, arAtm[11]);
          SetAtmosphere(EngineConstants.ATM_PARAM_MOON_SCALE, arAtm[12]);
          SetAtmosphere(EngineConstants.ATM_PARAM_MOON_ALPHA, arAtm[13]);
     }

     /* @brief runs the power operation.
     *
     *   runs the power operatio.
     *
     * @param nPower the number which we want the power of
     * @param nPowerLevel the power level required
     *
     * @author Yaron
     */
     public int Power(int nPower, int nPowerLevel)
     {
          int i;
          int nRet = nPower;
          for (i = 2; i <= nPowerLevel; i++)
          {
               nRet *= nPower;
          }

          if (nPowerLevel == 0)
               return 1;
          else if (nPowerLevel == 1)
               return nPower;

          return nRet;
     }

     /*
     * @brief (2da_data)  Returns the calculated cooldown to set after using an ability
     *
     * @returns float with cooldown in seconds
     *
     * @author   Georg Zoeller
     **/
     public float Ability_GetCooldown(GameObject oCaster, int nAbility)
     {
          float fBase = GetAbilityBaseCooldown(nAbility);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_GetCooldown", "base cooldown: " + FloatToString(fBase));
#endif
          return fBase;
     }

     /*
     * @brief (2da_data)  Sets the cooldown for an ability
     **
     * @author   Georg Zoeller
     **/
     public void Ability_SetCooldown(GameObject oCaster, int nAbility, GameObject oItem = null, float fCooldown = 0.0f)
     {

          string sItemTag = IsObjectValid(oItem) != EngineConstants.FALSE ? GetTag(oItem) : "";

          if (GetCreatureFlag(oCaster, EngineConstants.CREATURE_RULES_FLAG_NO_COOLDOWN) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.SetCoolDown", "setting NO Cooldown due to flag set on creature");
#endif
               return;
          }

          if (fCooldown == 0.0f)
          {
               fCooldown = Ability_GetCooldown(oCaster, nAbility);
          }
          else if (fCooldown < 0.0f)
          {
               fCooldown = fabs(fCooldown);
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.SetCoolDown", Log_GetAbilityNameById(nAbility) + ": " + FloatToString(fCooldown));
#endif

          SetCooldown(oCaster, nAbility, fCooldown, sItemTag);

     }

     public void AddAbilityEx(GameObject oTarget, int nAbility, int nQuickslot = 0)
     {
          if (nAbility > 0)
          {
               AddAbility(oTarget, nAbility);

               if (IsFollower(oTarget) != EngineConstants.FALSE)
               {
                    SetQuickslot(oTarget, nQuickslot, nAbility);
               }
          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_SCRIPTERROR, "ability_h.AddAbilityEx", "Attempt to add invalid ability id (negative or 0) rejected");
#endif

          }

     }

     public int IsDeadOrDying(GameObject oCreature)
     {
          return (IsDead(oCreature) != EngineConstants.FALSE || IsDying(oCreature) != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     public int IsInvalidDeadOrDying(GameObject oCreature)
     {
          return (IsObjectValid(oCreature) == EngineConstants.FALSE || IsDead(oCreature) != EngineConstants.FALSE || IsDying(oCreature) != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     public int IsSummoned(GameObject oCreature)
     {
          return GetLocalInt(oCreature, EngineConstants.IS_SUMMONED_CREATURE);
     }

     /*
     * @brief Returns the state controller table for the placeable
     *
     * @param oPlaceable the placeable
     *
     * @author   Yaron Jakobs

     **/
     public string GetPlaceableStateCntTable(GameObject oPlaceable)
     {

          int nAppearanceType = GetAppearanceType(oPlaceable);
          return GetM2DAString(EngineConstants.TABLE_PLACEABLE_TYPES, "StateController", nAppearanceType);
     }

     /*
     * @brief Returns EngineConstants.TRUE if the placeable is a trap trigger
     *
     * @param oPlaceable the placeable
     *
     * @author   Yaron Jakobs

     **/
     public int IsTrapTrigger(GameObject oPlaceable)
     {

          return GetPlaceableStateCntTable(oPlaceable) == EngineConstants.PLC_STATE_CNT_TRAP_TRIGGER ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     public void Party_SetFollowLeader(int bFollow)
     {
          List<GameObject> aParty = GetPartyList();
          int nSize = GetArraySize(aParty);
          int i;

          for (i = 0; i < nSize; i++)
          {
               SetFollowPartyLeader(aParty[i], bFollow);
          }
     }

     // -----------------------------------------------------------------------------
     // @brief Safe Wrapper for DestroyObject
     //      Reason: There were accidents where creatures would deplete themselves
     //              as Ammo because people were not paying attention....
     // @author Georg
     // -----------------------------------------------------------------------------
     public void Safe_Destroy_Object(GameObject oObject, int nDelayMs = 0)
     {
          int bDestroy = EngineConstants.TRUE;

          if (IsPartyMember(oObject) != EngineConstants.FALSE || 
            IsPlot(oObject) != EngineConstants.FALSE || 
            IsImmortal(oObject) != EngineConstants.FALSE)
          {
#if DEBUG
               Warning("Destroy Object Call rejected by Safe_Destroy_Object from script" + GetCurrentScriptName() + " on " + ToString(oObject) + ". This is serious, please contact georg.");
#endif
               bDestroy = EngineConstants.FALSE;
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "core_h.SafeDestroyObject", "Destroy:" + ToString(oObject) + " Result: " + ToString(bDestroy));
#endif

          if (bDestroy != EngineConstants.FALSE)
          {
               DestroyObject(oObject, nDelayMs);
          }
     }

     public int GetPlayerBackground(GameObject oChar)
     {
          return FloatToInt(GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_BACKGROUND));

     }

     //moved public const int EngineConstants.ABILITY_USE_TYPE_PASSIVE = 3;

     public int GetAbilityUseType(int nAbility)
     {
          return GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "usetype", nAbility);
     }

     public int CanCreatureBleed(GameObject oCreature)
     {
          int nApp = GetAppearanceType(oCreature);

          return GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "bCanBleed", nApp);

     }

     public GameObject GetRandomPartyMember()
     {
          List<GameObject> party = GetPartyList();
          return party[Engine_Random(GetArraySize(party))];

     }

     /*
     * @brief Permanently increases a EngineConstants.PROPERTY_ATTRIBUTE_* of the 6 core attributes by 1
     *
     * Meant for use by Ferret in a specific plot. Do not use for any other purpose!
     *
     * @param oCreature
     * @param nAttribute - e.g. EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWR
     *
     * @author   Georg Zoeller.
     *
     **/
     public void IncreaseAttributeScore(GameObject oCreature, int nAttribute)
     {
          // props 1 - 6 are attributes
          if (nAttribute > 0 && nAttribute < 7)
          {
               float fValue = GetCreatureProperty(oCreature, nAttribute, EngineConstants.PROPERTY_VALUE_BASE) + 1.0f;
               SetCreatureProperty(oCreature, nAttribute, fValue);
          }
     }

     /*
     * @brief Return the selected AI Behavior (idx into aibehaviors.xls)
     * @param oCreature
     * @author   Georg Zoeller.
     **/
     public int GetAIBehavior(GameObject oCreature)
     {
          if (GetObjectType(oCreature) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               return FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_AI_BEHAVIOR));
          }
          else
          {
               return EngineConstants.AI_BEHAVIOR_DEFAULT;
          }
     }

     /*
     * @brief Set selected AI Behavior (idx into aibehaviors.xls)
     * @param oCreature
     * @param value (index into aibehaviors.xls)
     * @author   Georg Zoeller.
     *
     **/
     public void SetAIBehavior(GameObject oCreature, int value)
     {
          if (GetObjectType(oCreature) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               if (value < 0)
                    value = 0;
               SetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_AI_BEHAVIOR, IntToFloat(value));
          }
     }

     /*
      * @brief The One and Only check to verify if a creature is a magic / mana user
      *        please use instead of checking for EngineConstants.CLASS_WIZARD, which breaks for monster
      *        classes.
      *
      * @author georg
     */
     public int IsMagicUser(GameObject oCreature)
     {

          int nClass = GetCreatureCoreClass(oCreature);
          int bMagicUser = ((nClass == EngineConstants.CLASS_WIZARD) ||
              (GetAppearanceType(oCreature) == 25 /* EngineConstants.APPEARANCE_TYPE_ABOMINATION */) ||
                  (GetM2DAInt(EngineConstants.TABLE_RULES_CLASSES, "bUsesMana", nClass) == 1)) ? EngineConstants.TRUE : EngineConstants.FALSE;

          return bMagicUser;
     }

     /*
      * @brief Return true if a party member has enough EngineConstants.XP to gain a level
      *
      * @author georg
      *
     */
     public int Chargen_CheckCanLevelUp(GameObject oPartyMember)
     {

          string sTag = GetTag(oPartyMember);

          // Sorry mouse, but you're out. You refuse to obey the normal rules of not leveling for party members,
          // so the gods of gaming have decided to hardcode your eternal mediocrity into this function as requested
          // by the imminent beta.
          if (sTag == "bhm600cr_mouse" || sTag == "bhm600cr_mouse_bear" || sTag == "bhm600cr_mouse_human")
          {
               return EngineConstants.FALSE;
          }

          int nCurrentLevel = GetLevel(oPartyMember);
          if (nCurrentLevel >= GetMaxLevel())
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "Chargen_CheckCanLevelUp", "nCurrentLevel >= DA_LEVEL_CAP. returning EngineConstants.FALSE");
               return EngineConstants.FALSE;
          }

          int nCurrentXP = GetExperience(oPartyMember);
          int nXPNeededForNext = GetM2DAInt(EngineConstants.TABLE_EXPERIENCE, "XP", (nCurrentLevel + 1));

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "core_h.Chargen_CheckCanLevelUp", "returning " + ToString(nCurrentXP) + " (current) >=" + ToString(nXPNeededForNext) + " (needed for next)");
#endif

          return (nCurrentXP >= nXPNeededForNext) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
      * @brief Return true if a weapon is a two handed melee weapon (bitm_base lookup)
      *
      * @author georg
      *
     */
     public int IsMeleeWeapon2Handed(GameObject oMeleeWeapon)
     {
          int nBitm = GetBaseItemType(oMeleeWeapon);
          return (GetM2DAInt(EngineConstants.TABLE_ITEMS, "WeaponWield", nBitm) == EngineConstants.WEAPON_WIELD_TWO_HANDED_MELEE) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
      * @brief Brute force wrapper around the SetImmortal xCommand to allow me to
      *        trace the use of this command.
      *
      *        If you encounter a creature that is set immortal but shouldn't be
      *        you can trace all calls to this function in context of the game session
      *        via the reports function on the SkyNet server.
      *
      * @author georg
      *
     */
     public void SetImmortal(GameObject oObject, int bImmortal)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName() + "->core_h.SetImmortal", "Setting Immortal " + ToString(bImmortal) + " on " + ToString(oObject));
#endif

          // -------------------------------------------------------------------------
          // //Track any use of Immortal on Party Members....
          // -------------------------------------------------------------------------
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               if (IsPartyMember(oObject) != EngineConstants.FALSE && bImmortal != EngineConstants.FALSE)
               {
                    //TrackPCImmortal(oObject, bImmortal) ;
               }
          }
          Engine_SetImmortal(oObject, bImmortal);
     }

     /*
      * @brief Return EngineConstants.TRUE if oPlaceable is a body bag (simple tag string compare)
      *
      * @author petert
      *
     */
     public int IsBodyBag(GameObject oPlaceable)
     {
          int bIsBodyBag = EngineConstants.FALSE;

          string sTag = GetTag(oPlaceable);
          if (sTag == EngineConstants.BODY_BAG_TAG)
          {
               bIsBodyBag = EngineConstants.TRUE;
          }

          return bIsBodyBag;
     }

     // -----------------------------------------------------------------------------
     // Attempt to get the body bad corresponding to oDeadCreature.
     // this is not guarrenteed to work! If two creatures
     // are piled up on each other, you may get the body bag
     // of the other creature!
     // -----------------------------------------------------------------------------
     public GameObject GetBodyBag(GameObject oDeadCreature)
     {

          GameObject oBodyBag = null;
          if (IsObjectValid(oDeadCreature) != EngineConstants.FALSE)
          {
               if (IsDead(oDeadCreature) != EngineConstants.FALSE)
               {
                    List<GameObject> oBodyBags = GetNearestObjectByTag(oDeadCreature, EngineConstants.BODY_BAG_TAG, EngineConstants.OBJECT_TYPE_PLACEABLE, 1);
                    int nCount = GetArraySize(oBodyBags);
                    if (nCount > 0)
                    {
                         oBodyBag = oBodyBags[0];
                    }
               }
          }
          return oBodyBag;
     }

     /*
      * @brief Recalculate the number of tactics slots a creature should have based
      *        on level and skills
      *
      * @author yaron
      *
      */
     public void Chargen_SetNumTactics(GameObject oChar)
     {
          int nLevel = GetLevel(oChar);

          int nTacticsNum = GetM2DAInt(EngineConstants.TABLE_EXPERIENCE, "Tactics", nLevel);

          if (HasAbility(oChar, EngineConstants.ABILITY_SKILL_COMBAT_TACTICS_4) != EngineConstants.FALSE)
               nTacticsNum += 6;
          else if (HasAbility(oChar, EngineConstants.ABILITY_SKILL_COMBAT_TACTICS_3) != EngineConstants.FALSE)
               nTacticsNum += 4;
          else if (HasAbility(oChar, EngineConstants.ABILITY_SKILL_COMBAT_TACTICS_2) != EngineConstants.FALSE)
               nTacticsNum += 2;
          else if (HasAbility(oChar, EngineConstants.ABILITY_SKILL_COMBAT_TACTICS_1) != EngineConstants.FALSE)
               nTacticsNum += 1;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "Chargen_SetNumTactics", "Number of tactics for creature " + IntToString(nTacticsNum));
#endif

          int nSendNotification = (nLevel == 1 ? EngineConstants.FALSE : EngineConstants.TRUE);
          SetNumTactics(oChar, nTacticsNum, nSendNotification);

          // Set default tactic for player when first generated only
          if (IsHero(oChar) != EngineConstants.FALSE && nLevel == 1)
          {
               SetTacticEntry(oChar, 1, EngineConstants.TRUE, 
                   EngineConstants.AI_TARGET_TYPE_ENEMY, 
                   EngineConstants.AI_BASE_CONDITION_SURROUNDED_BY_TARGETS, 
                   EngineConstants.AI_COMMAND_ATTACK);
          }
     }

     /*
      * @brief Return if the player has unspent character advancement points.
      *        This controls the display of the levelup icon next to the portrait.
      *
      * @author georg
      */
     public int Chargen_HasPointsToSpend(GameObject oChar)
     {
          float f = GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS);
          f += GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS);
          f += GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS);

          // -------------------------------------------------------------------------
          // comment this line back in if you want a levelup icon when you gain a
          // spec point.
          // -------------------------------------------------------------------------
          //  f+= GetCreatureProperty(oChar, 38 /* SPEC_POINTS */);

          return f > 0.0f ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
      * @brief Scale the items available in a store to the level of the player
      *        Only happens once(or selectively)
      *
      *
      * @author patl
      */
     public void ScaleStoreItems(GameObject oStore, int bReset = EngineConstants.FALSE)
     {
          // check for duplicate specialization books
          int nSpecialization;
          List<GameObject> oItems = GetItemsInInventory(oStore, EngineConstants.GET_ITEMS_OPTION_ALL);
          int nSize = GetArraySize(oItems);
          int nCount = 0;
          string acvId;
          for (nCount = 0; nCount < nSize; nCount++)
          {
               nSpecialization = GetLocalInt(oItems[nCount], EngineConstants.ITEM_SPECIALIZATION_FLAG);
               if (nSpecialization > 0)
               {
                    acvId = GetM2DAString(EngineConstants.TABLE_ACHIEVEMENTS, "AchievementID", nSpecialization);
                    if (GetHasAchievement(acvId) != EngineConstants.FALSE)
                    {
                         DestroyObject(oItems[nCount]);
                    }
               }
          }

          // Remove backpack items from store if party's inventory size is already maximized.
          if (GetMaxInventorySize() >= 125)
          {
               RemoveItemsByTag(oStore, "gen_im_misc_backpack");
          }

          // Only scale the merchant the first time opened.
          if (GetLocalInt(oStore, "MERCHANT_IS_SCALED") != EngineConstants.FALSE && bReset == EngineConstants.FALSE)
          {
               return;
          }

          SetLocalInt(oStore, "MERCHANT_IS_SCALED", 1);

          //    oItems = GetItemsInInventory(oStore, EngineConstants.GET_ITEMS_OPTION_ALL);
          //    nSize = GetArraySize(oItems);

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "ScaleStoreItems", "START, number of items: " + IntToString(nSize));
#endif

          int nStoreBase = GetLocalInt(oStore, "MERCHANT_LEVEL_OVERRIDE");
          int nStoreLevel = 0;
          if (nStoreLevel > 0)
          {
               nStoreLevel = nStoreBase;
          }
          else
          {
               nStoreLevel = GetLevel(GetHero());
          }
          int nStoreLevelModifier = GetLocalInt(oStore, "MERCHANT_LEVEL_MODIFIER");

          // modify and enforce range
          nStoreLevel += nStoreLevelModifier;
          nStoreLevel = Max(nStoreLevel, 1);
          nStoreLevel = Min(nStoreLevel, 45);

          int nItemType;
          int nMaterialProgression;
          int nRandomLevel;
          int nColumn;
          int nMaterial;
          float fHighChance = GetLocalFloat(oStore, "MERCHANT_HIGH_CHANCE");
          int nHighModifier = 3;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Base Store Level  Scaling " + ToString(nStoreBase));
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Store Level Modifier " + ToString(nStoreLevelModifier));
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Store High Chance " + ToString(fHighChance));
#endif

          nCount = 0;
          for (nCount = 0; nCount < nSize; nCount++)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    Scaling " + GetTag(oItems[nCount]));
#endif

               // if appropriate type (armor, shield, melee weapon, ranged weapon)
               nItemType = GetItemType(oItems[nCount]);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nItemType = " + ToString(nItemType));
#endif

               if ((nItemType == EngineConstants.ITEM_TYPE_ARMOUR) || (nItemType == EngineConstants.ITEM_TYPE_SHIELD) || (nItemType == EngineConstants.ITEM_TYPE_WEAPON_MELEE) || (nItemType == EngineConstants.ITEM_TYPE_WEAPON_RANGED))
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Item is scalable.");
#endif

                    // if not unique
                    if (GetItemUnique(oItems[nCount]) == EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Item is not unique.");
#endif

                         // get material progression
                         nMaterialProgression = GetItemMaterialProgression(oItems[nCount]);

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nMaterialProgression = " + ToString(nMaterialProgression));
#endif
                         if (nMaterialProgression > 0)
                         {
                              // find randomized level
                              nRandomLevel = nStoreLevel + Engine_Random(7) - 3;
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Initial nRandomLevel = " + ToString(nRandomLevel));
#endif

                              if (RandomFloat() < fHighChance)
                              {
                                   nRandomLevel += nHighModifier;
                                   SetLocalInt(oItems[nCount], "ITEM_RUNE_ENABLED", 1);
                              }

#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Modified nRandomLevel = " + ToString(nRandomLevel));
#endif

                              nRandomLevel = Max(1, nRandomLevel);
                              nRandomLevel = Min(45, nRandomLevel);

#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Final nRandomLevel = " + ToString(nRandomLevel));
#endif

                              // find material column
                              nColumn = ((nRandomLevel - 1) / 3) + 1;

#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nColumn = " + ToString(nColumn));
#endif

                              nColumn = Max(1, nColumn);
                              nColumn = Min(15, nColumn);

#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Min-Max nColumn = " + ToString(nColumn));
#endif

                              // get material
                              nMaterial = GetM2DAInt(EngineConstants.TABLE_MATERIAL, "Material" + ToString(nColumn), nMaterialProgression);

#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nMaterial = " + ToString(nMaterial));
#endif

                              // set material
                              SetItemMaterialType(oItems[nCount], nMaterial);
                         }
                    }
               }
          }
     }

     public float GetWeaponAttributeBonusFactor(GameObject oWeapon)
     {

          if (IsObjectValid(oWeapon) == EngineConstants.FALSE)
          {
               // ---------------------------------------------------------------------
               // Unarmed
               // ---------------------------------------------------------------------
               return EngineConstants.UNARMED_ATTRIBUTE_BONUS_FACTOR;
          }

          int nBase = GetBaseItemType(oWeapon);
          float fFactor = GetItemStat(oWeapon, EngineConstants.ITEM_STAT_ATTRIBUTE_MOD);

          return fFactor;
     }

     /*
      * @brief Calulcate attack damage bonus based on a number of parameters
      *        such as wield mode and talents
      *
      * @returns Damage bonsu
      *
      * @author georg
      */
     public float Combat_Damage_GetAttributeBonus(GameObject oCreature, int nHand = EngineConstants.HAND_MAIN, GameObject oWeapon = null, int bDeterministic = EngineConstants.FALSE)
     {
          int nAttribute = EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH;
          int nAttribute1 = 0;
          // -------------------------------------------------------------------------
          // some
          // -------------------------------------------------------------------------
          if (IsObjectValid(oWeapon) != EngineConstants.FALSE)
          {
               int nBaseItemType = GetBaseItemType(oWeapon);
               if (nBaseItemType > 0)
               {
                    nAttribute = GetM2DAInt(EngineConstants.TABLE_ITEMSTATS, "Attribute0", nBaseItemType);
                    nAttribute1 = GetM2DAInt(EngineConstants.TABLE_ITEMSTATS, "Attribute1", nBaseItemType);
               }
          }

          // -------------------------------------------------------------------------
          // Combat Magic: Using SpellPower (magic modifier) intead.
          // -------------------------------------------------------------------------
          if (IsModalAbilityActive(oCreature, EngineConstants.ABILITY_SPELL_COMBAT_MAGIC) != EngineConstants.FALSE)
          {
               nAttribute = EngineConstants.PROPERTY_ATTRIBUTE_MAGIC;
          }

          // ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---
          // Lethality: If the talent is present and the attribute tested is strength
          // then change the attribute to cunning if cunning is higher than int
          // ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---
          else if (nAttribute == EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH && HasAbility(oCreature, EngineConstants.ABILITY_TALENT_LETHALITY) != EngineConstants.FALSE)
          {
               if (GetAttributeModifier(oCreature, nAttribute) < GetAttributeModifier(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE))
               {
                    nAttribute = EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE;
               }
          }

          float fDmg = 0.0f;

          if (nAttribute1 == 0)
          {
               fDmg = MaxF(0.0f, GetAttributeModifier(oCreature, nAttribute));
          }
          else
          {
               fDmg = MaxF(0.0f, (GetAttributeModifier(oCreature, nAttribute) + GetAttributeModifier(oCreature, nAttribute1)) / 2.0f);

          }

          // Main hand attacks to 50-75% of att modifier
          if (nHand == EngineConstants.HAND_MAIN)
          {
               if (GetWeaponStyle(oCreature) == EngineConstants.WEAPONSTYLE_DUAL)
               {
                    return RandFF(fDmg * 0.25f, fDmg * 0.25f, bDeterministic);
               }
               else
               {
                    return RandFF(fDmg * 0.25f, fDmg * 0.5f, bDeterministic);
               }
          }
          else
          {
               // dual weapon training adds 1/4th of att modifier to damage / so does using a shield (offhand damage with shield is shield abilities only anyway)
               if (HasAbility(oCreature, EngineConstants.ABILITY_TALENT_DUAL_WEAPON_TRAINING) != EngineConstants.FALSE || IsUsingShield(oCreature) != EngineConstants.FALSE)
               {
                    return RandFF(fDmg * 0.25f, fDmg * 0.25f, bDeterministic);
               }
               else
               {
                    return RandFF(fDmg * 0.25f, 0.0f, bDeterministic);
               }
          }
     }

     /*
      * @brief Calculate the attack duration value to be passed to the engine based
      *        on factors like wieldstyle, effects and stats
      *
      * @returns Attack duration value or 0.0f for default (anim controlled)
      *
      * @author georg
      */
     public float CalculateAttackTiming(GameObject oAttacker, GameObject oWeapon)
     {
          // -------------------------------------------------------------------------
          // Without a weapon or if we are not a humanoid (which covers shapeshift)
          // we return 0.0f, which translates to animation controlled timing in the
          // engine when passed into attack speed.
          // -------------------------------------------------------------------------
          if (IsObjectValid(oWeapon) != EngineConstants.FALSE && IsHumanoid(oAttacker) != EngineConstants.FALSE)
          {

               float fSpeedMod = GetM2DAFloat(EngineConstants.TABLE_ITEMSTATS, "Dspeed", GetBaseItemType(oWeapon));

               // ---------------------------------------------------------------------
               // Calculate weapon speed.
               // ---------------------------------------------------------------------
               int nStyle = GetWeaponStyle(oAttacker);
               float fSpeed = 0.0f;

               switch (nStyle)
               {
                    case EngineConstants.WEAPONSTYLE_NONE:
                         fSpeed = EngineConstants.BASE_TIMING_WEAPON_SHIELD;
                         break;
                    case EngineConstants.WEAPONSTYLE_DUAL:
                         fSpeed = EngineConstants.BASE_TIMING_DUAL_WEAPONS;
                         break;
                    case EngineConstants.WEAPONSTYLE_SINGLE:
                         fSpeed = EngineConstants.BASE_TIMING_WEAPON_SHIELD;
                         break;
                    case EngineConstants.WEAPONSTYLE_TWOHANDED:
                         fSpeed = EngineConstants.BASE_TIMING_TWO_HANDED;
                         break;
               }

               // ---------------------------------------------------------------------
               // Only attacks that rely on timing can be modified
               // ---------------------------------------------------------------------
               if (fSpeed > 0.0f)
               {
                    // -----------------------------------------------------------------
                    // We're capping the actual values here to avoid animations breaking
                    // down when played too fast or too slow.
                    // -----------------------------------------------------------------
                    float fSpeedEffects = MinF(1.5f, MaxF(0.5f, GetCreatureProperty(oAttacker, EngineConstants.PROPERTY_ATTRIBUTE_ATTACK_SPEED_MODIFIER)));

                    // -----------------------------------------------------------------
                    // compatibility with some old savegames.
                    // -----------------------------------------------------------------
                    if (GetCreatureProperty(oAttacker, EngineConstants.PROPERTY_ATTRIBUTE_ATTACK_SPEED_MODIFIER) < 0.5f)
                    {
                         fSpeedEffects = 1.0f;
                    }

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_performattack", "weapon base speed :" + ToString(fSpeed) + " mod:" + ToString(fSpeedMod) + " effects: " + ToString(fSpeedEffects));
#endif

                    fSpeed += (fSpeedMod);
                    if (fSpeedEffects > 0.0f)
                    {
                         fSpeed *= fSpeedEffects;
                    }
                    return fSpeed;
               }
          }
          return 0.0f;
     }

     /*
      * @brief Retrieves a weapons base damage from the defined item stat.
      *
      * @returns weapon base damage
      *
      * @author georg
      */
     public float DmgGetWeaponBaseDamage(GameObject oWeapon)
     {
          if (IsObjectValid(oWeapon) == EngineConstants.FALSE)
          {
               return EngineConstants.COMBAT_DEFAULT_UNARMED_DAMAGE;
          }
          float fBase = GetItemStat(oWeapon, EngineConstants.ITEM_STAT_DAMAGE);
          return fBase;

     }

     /*
      * @brief Retrieves a weapons max damage from the defined item stat.
      *
      * @returns weapon max damage
      *
      * @author georg
      */
     public float DmgGetWeaponMaxDamage(GameObject oWeapon)
     {
          int nType = GetBaseItemType(oWeapon);

          if (IsObjectValid(oWeapon) == EngineConstants.FALSE)
          {
               return EngineConstants.COMBAT_DEFAULT_UNARMED_DAMAGE * 1.5f;
          }

          float fMax = DmgGetWeaponBaseDamage(oWeapon) * MaxF(1.0f, GetM2DAFloat(EngineConstants.TABLE_ITEMSTATS, "DamageRange", nType));
          return fMax;
     }

     /*
      * @brief Retrieves a weapons attack damage. This is randomized unless bForceMaxDamage is specificied.
      *
      * @returns Weapon damge of a single attack.
      *
      * @author georg
      */
     public float DmgGetWeaponDamage(GameObject oWeapon, int bForceMaxDamage = EngineConstants.FALSE)
     {

          float fBase = DmgGetWeaponBaseDamage(oWeapon);

          if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_SHIELD)
          {
               fBase += 5.0f;
          }

          float fMax = DmgGetWeaponMaxDamage(oWeapon);

          float fDmg = fBase + ((bForceMaxDamage != EngineConstants.FALSE) ? fMax - fBase : RandFF(fMax - fBase));

#if DEBUG
          _LogDamage("  fWeapon   " + ToString(oWeapon) + ": base: " + ToString(fBase) + " + max: " + ToString(fMax) + " md:" + ToString(bForceMaxDamage));
          _LogDamage("  fWeapon   " + ToString(oWeapon) + ":" + ToString(fDmg) + " = " + ToString(fBase) + " + Rand(" + ToString(fMax - fBase) + ")");
#endif

          return fDmg;

     }

     /*
      * @brief Retrieves weapon damage for the staff
      * @author georg
      */
     public float Combat_Damage_GetMageStaffDamage(GameObject oAttacker, GameObject oTarget, GameObject oWeapon, int bDeterministic = EngineConstants.FALSE)
     {
          int nProjectileIndex = GetLocalInt(oWeapon, EngineConstants.PROJECTILE_OVERRIDE);
          float fDamageBonus = 1.0f + GetM2DAFloat(EngineConstants.TABLE_PROJECTILES, "DamageBonus", nProjectileIndex);

          float fArcaneFocus = 1.0f;
          if (HasAbility(oAttacker, 200256) != EngineConstants.FALSE) /*EngineConstants.ABILITY_SPELL_ARCANE_FOCUS*/
          {
               fArcaneFocus = 1.0f + (1.0f / 3.0f);
          }

          float fSpellPowerComponent = (GetCreatureSpellPower(oAttacker) / 4.0f) * fArcaneFocus * fDamageBonus;

          return DmgGetWeaponDamage(oWeapon, bDeterministic) + RandFF(fSpellPowerComponent * 0.25f, fSpellPowerComponent * 0.75f, bDeterministic);

     }

     // @brief Calcluate weapon damage display values. See below for more info.
     // @author Georg
     public float CalculateWeaponDamage(GameObject oCreature, GameObject oWeapon, int nHand = EngineConstants.HAND_MAIN)
     {
          // ---------------------------------------------------------------------
          // Staves have a different damage formula.
          // ---------------------------------------------------------------------
          if (GetBaseItemType(oWeapon) == EngineConstants.BASE_ITEM_TYPE_STAFF)
          {
               return Combat_Damage_GetMageStaffDamage(oCreature, null, oWeapon, EngineConstants.TRUE);
          }

          // ---------------------------------------------------------------------
          // Generate a determinitic damage
          // ---------------------------------------------------------------------
          float fStat = DmgGetWeaponBaseDamage(oWeapon);
          float fMax = DmgGetWeaponMaxDamage(oWeapon);
          fStat = fStat + ((fMax - fStat) * 0.5f);

          // ---------------------------------------------------------------------
          // Generate some timing values
          // ---------------------------------------------------------------------
          float fTiming = CalculateAttackTiming(oCreature, oWeapon);
          if (fTiming > 0.0f)
          {
               fTiming = 1.0f / fTiming;
          }
          else
          {
               fTiming = 0.5f /* average base timing */;
          }

          // ---------------------------------------------------------------------
          // Add in attribute bonus and multiply with bonus factor based on
          // weapon type
          // ---------------------------------------------------------------------
          float fFactor = GetWeaponAttributeBonusFactor(oWeapon);
          fStat += Combat_Damage_GetAttributeBonus(oCreature, nHand, oWeapon, EngineConstants.TRUE) * fFactor;

          // ---------------------------------------------------------------------
          // This is mostly for debugging monsters, since damage scale is usually
          // set to 1.0f for player characters.
          // ---------------------------------------------------------------------
          float fDamageScale = GetM2DAFloat(Diff_GetAutoScaleTable(), "fDamageScale", GetCreatureRank(oCreature));
          if (fDamageScale > 0.0f)
          {
               fStat *= fDamageScale;
          }

          fStat += GetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_BONUS);

          // ---------------------------------------------------------------------
          // for non dual weapons, multiply with magic number to generate
          // a proportionally more correct display value.
          // ---------------------------------------------------------------------
          if (GetWeaponStyle(oCreature) == EngineConstants.WEAPONSTYLE_TWOHANDED)
          {
               fStat *= (fTiming * 2.25f);
          }
          else if (GetWeaponStyle(oCreature) != EngineConstants.WEAPONSTYLE_DUAL)
          {
               fStat *= (fTiming * 2.0f);
          }

          return fStat;
     }

     /* -----------------------------------------------------------------------------

         @brief Recalculates the damage number to display on the character sheet

         Damage is a complex mechanic with many contributing factors in DA.
         In order to provide the necessary feedback to the player, we need to
         create and display a damage stat on the character that approximates the
         damage potential of the character and reflects changes due to equipment.

         Note: Because damage is influenced by a number of factors not known at the
         time a player opens his UI (such as enemy resistances, armor, etc.) as well
         as randomized, it is not possible to just show a flat damage number.

         The main purpose of the number generated by this function is to give the player
         some number that is proportionally correct - meaning he can assess whether or not
         a weapon is an upgrade over another weapon, etc.

         The values in this function are generated by removing all non-deterministic
         factors for the player's damage - using maximum or average values where needed.

         We also include timing information to allow the player to assess the impact
         of abilities like haste and momentum as well as basic weapon style wieldspeeds
         on damage.

         Since the numbers generated by different styles are not immediately comparable,
         we are multiplying them with different magic numbers to generate output that
         makes sense on the character sheets and that matches the damage floaties the
         player will see when fighting an average opponent.

         Note: For the future - don't design too many non-deterministic elements into
         the rules system.

         This function is called in most places where a character's damage potential
         may change (ui_open and equipment change)

         @author Georg

        ---------------------------------------------------------------------------*/

     public void RecalculateDisplayDamage(GameObject oCreature, int nSlot = EngineConstants.INVENTORY_SLOT_INVALID)
     {

          if (nSlot == EngineConstants.INVENTORY_SLOT_INVALID || nSlot == EngineConstants.INVENTORY_SLOT_MAIN)
          {
               GameObject oMain = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oCreature);
               if (IsObjectValid(oMain) != EngineConstants.FALSE)
               {
                    float fStat = 1.0f;

                    if (GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_SHAPECHANGE) != EngineConstants.FALSE)
                    {
                         fStat = CalculateWeaponDamage(oCreature, null);
                    }
                    else
                    {
                         fStat = CalculateWeaponDamage(oCreature, oMain);
                    }
                    SetCreatureProperty(oCreature, 50, fStat);
               }
               // ---------------------------------------------------------------------
               // Unarmed
               // ---------------------------------------------------------------------
               else
               {
                    float fStat = DmgGetWeaponDamage(null) + Combat_Damage_GetAttributeBonus(oCreature, EngineConstants.HAND_MAIN, null, EngineConstants.TRUE) + GetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_BONUS);
                    SetCreatureProperty(oCreature, 50, fStat);
               }
          }

          if (nSlot == EngineConstants.INVENTORY_SLOT_INVALID || nSlot == EngineConstants.INVENTORY_SLOT_OFFHAND)
          {
               GameObject oOffHand = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, oCreature);

               // ---------------------------------------------------------------------
               // Armed
               // ---------------------------------------------------------------------
               if (IsObjectValid(oOffHand) != EngineConstants.FALSE)
               {
                    float fStat = 0.0f;

                    if (GetWeaponStyle(oCreature) == EngineConstants.WEAPONSTYLE_DUAL)

                    {
                         fStat = CalculateWeaponDamage(oCreature, oOffHand, EngineConstants.HAND_OFFHAND);
                    }

                    // -----------------------------------------------------------------
                    // If the offhand item doesn't have damage, it's not a weapon and
                    // we should not display it.
                    // -----------------------------------------------------------------
                    if (fStat > 0.0f)
                    {

                         float fDamageScale = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, "fDamageScale", GetCreatureRank(oCreature));
                         SetCreatureProperty(oCreature, 49, fStat);
                    }
                    else
                    {
                         SetCreatureProperty(oCreature, 49, -99.0f);
                    }
               }
               // ---------------------------------------------------------------------
               // Unarmed
               // ---------------------------------------------------------------------
               else
               {
                    if (oOffHand == null)
                    {
                         SetCreatureProperty(oCreature, 49, -98.0f);
                    }

                    SetCreatureProperty(oCreature, 49, -97.0f);
               }
          }

     }

     // @brief  Returns if an ability can be interrupted by damage
     // @author Georg
     public int CanInterruptSpell(int nAbi)
     {
          if (GetAbilityType(nAbi) == EngineConstants.ABILITY_TYPE_SPELL)
          {
               //Only spells of speed>0 are interruptible
               return GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "Speed", nAbi);
          }
          return EngineConstants.FALSE;
     }

     public float GetDisableDeviceLevel(GameObject oCreature)
     {
          float fPlayerScore = 0.0f;

          if (HasAbility(oCreature, EngineConstants.ABILITY_TALENT_HIDDEN_ROGUE) != EngineConstants.FALSE)
          {
               fPlayerScore = GetAttributeModifier(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE);

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName(), "Intelligence Modifier = " + ToString(fPlayerScore));
#endif

               int nSkillLevel = 0;
               if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_LOCKPICKING_4) != EngineConstants.FALSE)
               {
                    nSkillLevel = 4;
               }
               else if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_LOCKPICKING_3) != EngineConstants.FALSE)
               {
                    nSkillLevel = 3;
               }
               else if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_LOCKPICKING_2) != EngineConstants.FALSE)
               {
                    nSkillLevel = 2;
               }
               else if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_LOCKPICKING_1) != EngineConstants.FALSE)
               {
                    nSkillLevel = 1;
               }
               fPlayerScore += (10.0f * nSkillLevel);

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_PLACEABLES, GetCurrentScriptName(), "With Skill = " + ToString(fPlayerScore));
#endif
          }
          return fPlayerScore;
     }

     // @brief Return if the creature is a boss rank creature
     // @author Georg
     public int IsCreatureBossRank(GameObject oCreature)
     {
          return GetM2DAInt(EngineConstants.TABLE_CREATURERANKS, "IsBoss", GetCreatureRank(oCreature));
     }

     // @brief Returns if a creature is of special rank (lieutenant+)
     // @author georg
     public int IsCreatureSpecialRank(GameObject oCreature)
     {
          return GetM2DAInt(EngineConstants.TABLE_CREATURERANKS, "IsSpecial", GetCreatureRank(oCreature));
     }

     // @brief  Retrieve the floaty color associated with a specific type of damage
     // @author Georg
     public int GetColorByDamageType(int nDamageType)
     {
          int nColor = 0;
          nColor = GetM2DAInt(EngineConstants.TABLE_DAMAGETYPES, "Color", nDamageType);
          return nColor;
     }

     // @brief Certain areas do not allow exiting combat mode
     // @author Yaron
     public int IsNoExploreArea()
     {
          return GetTag(GetArea(GetPartyLeader())) == "cli220ar_fort_roof_1" ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     public int IsArmorMassive(GameObject oArmor)
     {
          if (IsObjectValid(oArmor) == EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }
          int nType = GetBaseItemType(oArmor);

          return (nType == EngineConstants.BASE_ITEM_TYPE_ARMOR_MASSIVE || nType == EngineConstants.BASE_ITEM_TYPE_ARMOR_SUPERMASSIVE) ? EngineConstants.TRUE : EngineConstants.FALSE;

     }

     public int IsArmorHeavyOrMassive(GameObject oArmor)
     {
          if (IsObjectValid(oArmor) == EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          int nType = GetBaseItemType(oArmor);
          return (nType == EngineConstants.BASE_ITEM_TYPE_ARMOR_HEAVY || nType == EngineConstants.BASE_ITEM_TYPE_ARMOR_MASSIVE || nType == EngineConstants.BASE_ITEM_TYPE_ARMOR_SUPERMASSIVE) ? EngineConstants.TRUE : EngineConstants.FALSE;

     }

     public int IsUsingEP1Resources()
     {
          return EngineConstants.TRUE;
     }
}