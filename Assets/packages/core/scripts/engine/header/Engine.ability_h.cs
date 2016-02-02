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
     // ----------------------------------------------------------------------------
     // ability_h - Ability System Functions
     // ----------------------------------------------------------------------------
     /*
         General Purpose functions dealing with the ability/talent/spell system,
         including, but not limited to:

         - Calculating and Subtracting Ability Costs
         - Invoking and rerouting Spellscript events
         - Wrappers for dealing with Upkeep Effects applied by Abilities

     */
     // ----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // ----------------------------------------------------------------------------
     //#include"effects_h"
     //#include"2da_constants_h"
     //#include"config_h"
     //#include"events_h"
     //#include"items_h"
     //#include"sys_resistances_h"

     //moved public const int EngineConstants.ABILITY_RESIST_RESULT_FAILURE = 0;

     //moved public const int EngineConstants.ABILITY_COST_TYPE_NONE    = 0;
     //moved public const int EngineConstants.ABILITY_COST_TYPE_HEALTH  = 1;
     //moved public const int EngineConstants.ABILITY_COST_TYPE_MANA    = 2;
     //moved public const int EngineConstants.ABILITY_COST_TYPE_STAMINA = 4;

     // abi flags
     //moved public const int EngineConstants.ABILITY_FLAG_RANGED_WEAPON      = 1;  /*0x01*/

     // free:
     //
     //moved public const int EngineConstants.ABILITY_FLAG_END_ON_OUT_OF_MANA = 64; /*0x40*/
     //moved public const int EngineConstants.ABILITY_FLAG_DISPELLABLE = 128;       /*0x80*/
     //moved public const int EngineConstants.ABILITY_FLAG_CURABLE = 256;           /*0x100*/

     // flanking constants
     //moved public const float EngineConstants.ABILITY_FLANK_SIZE_BACK = 60.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_SIZE_BACK2 = 45.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_SIZE_SIDE = 60.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_SIZE_LARGE_SIDE = 90.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_SIZE_FRONT = 130.f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_BACK_LEFT = -135.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_BACK_RIGHT = 135.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_BACK_LEFT2 = -160.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_BACK_RIGHT2 = 160.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_LEFT = -60.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_RIGHT = 60.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_LARGE_RIGHT = 45.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_LARGE_LEFT = -45.0f;
     //moved public const float EngineConstants.ABILITY_FLANK_FACING_FRONT = 0.0f;

     /*
     * @brief Returns the EngineConstants.TABLE_* constants for 2da lookups for a specific abiltiy
     *
     * Talents, Spells, Skills and Items use different 2das, even tho they have the
     * same structure. This function returns the constant for use with the GetM2DA*
     * functions
     *
     * @param nAbilityType EngineConstants.ABILITY_TYPE_* constant
     *
     * @returns EngineConstants.TABLE_* constant
     *
     * @author   Georg Zoeller
     *
     **/
     public int Ability_GetAbilityTable(int nAbilityType)
     {
          // Which 2DA to read the data from
          int n2DA = EngineConstants.TABLE_ABILITIES_TALENTS;

          if (nAbilityType == EngineConstants.ABILITY_TYPE_SPELL)
          {
               n2DA = EngineConstants.TABLE_ABILITIES_SPELLS;
          }

          return n2DA;
     }

     /*
     * @brief Returns EngineConstants.TRUE if the specified ability is a blood magic ability
     *
     * Used by the spell cost functions to determine whether or not to cast
     * from health or mana/stamina
     *
     * @param nAbilityType EngineConstants.ABILITY_TYPE_* constant
     *
     * @returns EngineConstants.TRUE or False
     *
     * @author   Georg Zoeller
     *
     **/
     public int Ability_IsBloodMagic(GameObject oCaster)
     {
          /*
              return  (nAbility == EngineConstants.ABILITY_SPELL_BLOOD_WOUND ||
                        nAbility == EngineConstants.ABILITY_SPELL_BLOOD_CONTROL ||
                        nAbility == EngineConstants.ABILITY_SPELL_BLOOD_MAGIC ||
                        nAbility == EngineConstants.ABILITY_SPELL_BLOOD_SACRIFICE);
          */

          return IsModalAbilityActive(oCaster, EngineConstants.ABILITY_SPELL_BLOOD_MAGIC);
     }

     /*
     * @brief returns the cost (mana or stamina) of using an ability
     *
     * Note: nAbility = EngineConstants.ABILITY_TYPE_INVALID will cause a lookup in the 2da      
     * This function is duplicated within the game executable. Any change made to this function will 
     * result in GUI glitches and other bugs. Sorry.
     *
     *
     * @param oCaster        The creature to deactivate the ability on
     * @param nAbility       The modal ability to deactivate
     * @param nAbilityType   The ability type of the modal ability.
     *
     *
     * @returns EngineConstants.TRUE if the ability was terminated successfully
     *
     * @author   Georg Zoeller
     *
     **/
     public float Ability_GetAbilityCost(GameObject oCaster, int nAbility, int nAbilityType = EngineConstants.ABILITY_TYPE_INVALID, int bUpkeep = EngineConstants.FALSE)
     {
          //This function is duplicated within the game executable. Any change made to this function will 
          //result in GUI glitches and other bugs. Sorry.
          if (nAbilityType == EngineConstants.ABILITY_TYPE_INVALID)
          {
               nAbilityType = Ability_GetAbilityType(nAbility);
          }
          string sCol = bUpkeep != EngineConstants.FALSE ? "costupkeep" : "cost";

          float fCost = GetM2DAFloat(EngineConstants.TABLE_ABILITIES_SPELLS, sCol, nAbility);

          if (nAbilityType != EngineConstants.ABILITY_TYPE_ITEM)
          {
               if (fCost > 0.0f)
               {

                    float fModifier = 1.0f;

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_GetAbilityCost", "Initial Cost:" + ToString(fCost));
#endif

                    if (bUpkeep == EngineConstants.FALSE)
                    {
                         //This function is duplicated within the game executable. Any change made to this function will 
                         //result in GUI glitches and other bugs. Sorry.
                         fModifier += GetCreatureProperty(oCaster, EngineConstants.PROPERTY_ATTRIBUTE_FATIGUE) * 0.01f;
                         fCost = FloatToInt(fCost * fModifier + 0.5f) * 1.0f;

                    }

                    if (fCost > 0.0f)
                    {
                         float fDiffMod = Diff_GetAbilityUseMod(oCaster);
                         fCost *= (1.0f / fDiffMod);

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_GetAbilityCost", "Difficulty Mod : * " + ToString(fDiffMod));
#endif
                    }

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_GetAbilityCost", "Calculating: " + ToString(fCost) + "*(1+(" + ToString(GetCreatureProperty(oCaster, 41)) + "*0.01))");
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_GetAbilityCost", "New Cost:" + ToString(fCost));
#endif
               }
          }

          return fCost;
     }

     /*
     * @brief Returns if an ability uses the Ranged Weapon shoot anim as cast anim.
     *
     * @author georg;
     */
     public int Ability_IsUsingRangedWeaponAnim(int nAbility)
     {
          int nFlag = GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "flags", nAbility);
          return ((nFlag & EngineConstants.ABILITY_FLAG_RANGED_WEAPON) == EngineConstants.ABILITY_FLAG_RANGED_WEAPON) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
     * @brief Performs an ability cost check to see if an ability can be used.
     *
     * Returns EngineConstants.TRUE if the caster has enough mana or stamina (depending on ability type)
     * to use (used in ability_core)
     *
     * @param oCaster      The caster using the ability
     * @param nAbility     The ability being used
     * @param nAbilityType The ability type of that ability
     * @param oItem        The Item consumed to use the ability/spell (Optional)
     *
     * @returns  EngineConstants.TRUE (enough), EngineConstants.FALSE (not enough)
     * @author   Georg Zoeller
     *
     **/
     public int Ability_CostCheck(GameObject oCaster, int nAbility, int nAbilityType, GameObject oItem = null)
     {
          int bModal = Ability_IsModalAbility(nAbility);
          float fCost = Ability_GetAbilityCost(oCaster, nAbility, nAbilityType, bModal);
          float fMana = 0.0f;
          if (bModal != EngineConstants.FALSE)
          {
               fMana = GetCreatureProperty(oCaster, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_TOTAL);
               // You can use modal abilities when you have more mana available
               // than upkeep cost and at least 1 point of mana
               return fMana >= fCost && fMana >= 1.0f ? EngineConstants.TRUE : EngineConstants.FALSE;
          }

          int nNumItems;
          int bRet = EngineConstants.TRUE;

          // if the ability consumes an item, there is no cost
          if (nAbilityType == EngineConstants.ABILITY_TYPE_ITEM && IsObjectValid(oItem) != EngineConstants.FALSE)
          {
               return (GetItemStackSize(oItem) > 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
          }

          // only abilities and spells cost...
          else if (nAbilityType == EngineConstants.ABILITY_TYPE_SPELL || nAbilityType == EngineConstants.ABILITY_TYPE_TALENT)
          {

               // Shapeshifted characters cast their abilties for free.
               if (IsShapeShifted(oCaster) != EngineConstants.FALSE)
               {
                    return EngineConstants.TRUE;
               }

               // blood magic doesn't use mana or stamina, it uses health and the caster can very much
               // kill himself, so we don't subtract any damage here...
               if (Ability_IsBloodMagic(oCaster) == EngineConstants.FALSE)
               {
                    fMana = GetCreatureProperty(oCaster, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT);

                    if (fMana < fCost || fMana < 1.0f)
                    {
                         bRet = EngineConstants.FALSE;
                    }

               }

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.CostCheck", "AbilityUse Cost: " + FloatToString(fCost) + ", Have:" + FloatToString(fMana));
#endif

          }

          return bRet;
     }

     /*
     * @brief Performs an ability cost check to see if an ability can be used.
     *
     * Returns EngineConstants.TRUE if the caster has enough mana or stamina (depending on ability type)
     * to use (used in ability_core)
     *
     * @param oCaster      The caster using the ability
     * @param nAbility     The ability being used
     * @param nAbilityType The ability type of that ability
     *
     * @returns  EngineConstants.TRUE (enough), EngineConstants.FALSE (not enough)
     * @author   Georg Zoeller
     *
     **/
     public int Ability_GetAbilityTargetType(int nAbility, int nAbilityType)
     {
          int n2DA = Ability_GetAbilityTable(nAbilityType);
          int nTargetType = GetM2DAInt(n2DA, "TargetType", nAbility);
          return nTargetType;
     }

     public float Ability_AdjustDuration(GameObject oThis, float fDuration)
     {
          // Georg: Good place to add spell extension talents
          return fDuration;
     }

     public void Ability_RemoveAbilityEffectsByCreator(GameObject oCreator, int nAbility)
     {

          RemoveEffectsByCreator(oCreator, nAbility);

     }

     /*
     * @brief Utility Function for the use in spellscripts.
     *
     * If you think you need to change this functions, talk to Georg.
     *
     * @author   Georg Zoeller
     **/
     public int Ability_GetSpellscriptPendingEventResult()
     {

          int nRet = GetLocalInt(GetModule(), EngineConstants.HANDLE_EVENT_RETURN);
          return nRet;
     }

     /*
     * @brief Utility Function for the use in spellscripts. Talk to georg if needed.
     *
     * @author   Georg Zoeller
     **/
     public void Ability_SetSpellscriptPendingEventResult(int nResult)
     {

          int nRet = Ability_GetSpellscriptPendingEventResult();
          SetLocalInt(GetModule(), EngineConstants.HANDLE_EVENT_RETURN, nResult);
     }

     /*
     * @brief Special version of HandleEvent for use by Ability_DoRunSpellScript
     *
     *                 ** Utility Function, do not call elsewhere **
     *
     * @param ev           The xEvent to message to the spellscript
     * @param sFile        2da to run
     *
     * @returns  EngineConstants.COMMAND_RESULT_* constant if xEvent is EventSpellScriptPending:
     *
     * @author   Georg Zoeller
     *
     **/
     public int _Ability_HandleEventRef(ref xEvent ev, string rResource)
     {

          // this populates with the default value
          Ability_SetSpellscriptPendingEventResult(EngineConstants.COMMAND_RESULT_SUCCESS);

          HandleEventRef(ref ev, rResource);

          int nRet = Ability_GetSpellscriptPendingEventResult();

          return nRet;

     }

     /*
     * @brief Handles running an ability spellscript listed in the prop
     *
     * @param ev           The xEvent to message to the spellscript
     * @param nAbility     The Ability ID (EngineConstants.ABILITY_*)
     * @param nAbilityType The type of the ability
     *
     * @returns  EngineConstants.COMMAND_RESULT_* constant if xEvent is EventSpellScriptPending:
     *
     * @author   Georg Zoeller
     *
     **/
     public int Ability_DoRunSpellScript(xEvent ev, int nAbility, int nAbilityType)
     {

          int n2DA = Ability_GetAbilityTable(nAbilityType);

          string rResource = GetM2DAResource(n2DA, "SpellScript", nAbility);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.DoRunSpellScript", "running spellscript for ability: " + IntToString(nAbility) + " type=" + IntToString(nAbilityType));
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.DoRunSpellScript", "spell script = " + ResourceToString(rResource));
#endif

          if (rResource != "")
          {
               int nRet = _Ability_HandleEventRef(ref ev, rResource);

               return nRet;

          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.DoRunSpellScript", "ability_core: running spellscript failed, no 2da entry", null, EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
          }

          return EngineConstants.COMMAND_RESULT_INVALID; // 2EngineConstants.FALSE;
     }

     /*
     * @brief Applies the upkeep xEffect for a specific spell to the caster
     *
     * Note eEffect is always applied to oTarget and oCaster!
     *
     * @param oCaster            The creature using the ability
     * @param nAbility           The ability / spell that has been used
     * @param oTarget            The Target for eEffect  (default: caster)
     * @param eEffect            The beneficial xEffect to be upkept
     * @param bPartywide         Whether the whole party is affected
     *
     * @author   Georg Zoeller
     *
     */

     public void _ApplyUpkeepEffect(GameObject oCaster, xEffect eEffect, int nAbility, GameObject oTarget, int bPartyWide)
     {
          if (bPartyWide != EngineConstants.FALSE && IsFollower(oCaster) != EngineConstants.FALSE)
          {
               ApplyEffectOnParty(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eEffect, 0.0f, oCaster, nAbility, EngineConstants.TRUE, EngineConstants.FALSE);
          }
          else
          {
               // -------------------------------------------------------------------------
               // Apply the beneficial xEffect to the caster
               // -------------------------------------------------------------------------
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eEffect, oCaster, 0.0f, oCaster, nAbility);

               // -------------------------------------------------------------------------
               // If oTarget is not identical with oCaster, apply beneficial xEffect as well
               // -------------------------------------------------------------------------
               if (oTarget != oCaster)
               {
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eEffect, oTarget, 0.0f, oCaster, nAbility);
               }
          }
     }

     public void Ability_ApplyUpkeepEffects(GameObject oCaster, int nAbility, List<xEffect> eEffects, GameObject oTarget = null, int bPartywide = EngineConstants.FALSE)
     {

          float fCost = Ability_GetAbilityCost(oCaster, nAbility, GetAbilityType(nAbility), EngineConstants.TRUE) * -1.0f;

          if (IsObjectValid(oTarget) == EngineConstants.FALSE)
          {
               oTarget = oCaster;
          }

          int nCount = GetArraySize(eEffects);
          int i;

          for (i = 0; i < nCount; i++)
          {
               _ApplyUpkeepEffect(oCaster, eEffects[i], nAbility, oTarget, bPartywide);
          }

          // -------------------------------------------------------------------------
          // Spells cost mana, talents cost stamina
          // -------------------------------------------------------------------------
          xEffect eUpkeep = EffectUpkeep(EngineConstants.UPKEEP_TYPE_MANASTAMINA, fCost, nAbility, oTarget, bPartywide);

          // -------------------------------------------------------------------------
          // Upkeep effects are permanent until the xEffect is removed.
          // they also set the ui icon toggle inside the xEffect based on the
          // ability id
          // -------------------------------------------------------------------------

          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eUpkeep, oCaster, 0.0f, oCaster, nAbility);
     }

     public void Ability_ApplyUpkeepEffect(GameObject oCaster, int nAbility, xEffect eEffect, GameObject oTarget = null, int bPartywide = EngineConstants.FALSE)
     {

          float fCost = Ability_GetAbilityCost(oCaster, nAbility, EngineConstants.ABILITY_TYPE_INVALID, EngineConstants.TRUE) * -1.0f;

          if (IsObjectValid(oTarget) == EngineConstants.FALSE)
          {
               oTarget = oCaster;
          }

          _ApplyUpkeepEffect(oCaster, eEffect, nAbility, oTarget, bPartywide);

          // ---------------------------------------------------------------------
          // Spells cost mana, talents cost stamina
          // ---------------------------------------------------------------------
          xEffect eUpkeep = EffectUpkeep(EngineConstants.UPKEEP_TYPE_MANASTAMINA, fCost, nAbility, oTarget, bPartywide);

          // ---------------------------------------------------------------------
          // Upkeep effects are permanent until the xEffect is removed.
          // they also set the ui icon toggle inside the xEffect based on the
          // ability id
          // ---------------------------------------------------------------------

          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eUpkeep, oCaster, 0.0f, oCaster, nAbility);

     }

     /* ----------------------------------------------------------------------------
     * @brief Subtracts the cost (mana or stamina) for using an ability from the caster.
     *
     * @param oCaster            The creature using the ability
     * @param nAbility           The ability / spell that has been used
     *
     * @author   Georg Zoeller
     *  --------------------------------------------------------------------------**/
     public void Ability_SubtractAbilityCost(GameObject oCaster, int nAbility, GameObject oItem = null)
     {

          // -------------------------------------------------------------------------
          // get type and cost for the ability
          // -------------------------------------------------------------------------
          float fCost = Ability_GetAbilityCost(oCaster, nAbility);

          // -------------------------------------------------------------------------
          // Items don't cost
          // -------------------------------------------------------------------------
          if (GetAbilityType(nAbility) != EngineConstants.ABILITY_TYPE_ITEM)
          {

               // ---------------------------------------------------------------------
               //  Shapeshifted abilities were once cast for free.
               // ---------------------------------------------------------------------
               // if (IsShapeShifted(oCaster))
               //          {
               //          return;
               //        }

               fCost = FloatToInt(fCost) * -1.0f;

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_h.SubtractAbilityCost", "Cost for Ability: " + FloatToString(fCost));
#endif

               // ---------------------------------------------------------------------
               // is blood magic active?
               // ---------------------------------------------------------------------
               if (Ability_IsBloodMagic(oCaster) != EngineConstants.FALSE)
               {
                    // -----------------------------------------------------------------
                    // if there is a cost to the spell
                    // -----------------------------------------------------------------
                    if (fCost < 0.0f)
                    {
                         int nBloodMagicVFX = 1519;
                         float fMultiplier = 0.8f;
                         if (GetHasEffects(oCaster, EngineConstants.EFFECT_TYPE_BLOOD_MAGIC_BONUS) != EngineConstants.FALSE)
                         {
                              fMultiplier = 0.6f;
                         }

                         // negative multiplier because damage needs to be positive
                         fCost = fabs(fCost * fMultiplier);

                         DEBUG_PrintToScreen(ToString(fCost));

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.SubtractAbilityCost", "Blood Magic! Health " + FloatToString(fCost * fMultiplier) + " drained instead of Mana");
#endif
                         Effects_ApplyInstantEffectDamage(oCaster, oCaster, fCost, EngineConstants.DAMAGE_TYPE_PLOT, EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE, nAbility, nBloodMagicVFX);
                    }
               }
               else
               {
                    Effect_InstantApplyEffectModifyManaStamina(oCaster, fCost);
               }

          }

          /*
          xEffect eCost = EffectModifyManaStamina(fCost);
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eCost, oCaster, 0.0f, oCaster);
          */
     }

     /* ----------------------------------------------------------------------------
     * @brief Deactivates a modal ability
     *
     * Runs the spellscript for an ability with the DEACTIVE_MODAL_ABILITY event\
     *
     * @param oCaster        The creature to deactivate the ability on
     * @param nAbility       The modal ability to deactivate
     * @param nAbilityType   The ability type of the modal abiltiy
     *
     *
     * @returns EngineConstants.TRUE if the ability was terminated successfully
     *
     * @author   Georg Zoeller
     *  -----------------------------------------------------------------------------
     **/
     public int Ability_DeactivateModalAbility(GameObject oCaster, int nAbility, int nAbilityType = EngineConstants.ABILITY_TYPE_INVALID)
     {

          if (nAbilityType == EngineConstants.ABILITY_TYPE_INVALID)
          {
               nAbilityType = Ability_GetAbilityType(nAbility);
          }

          if (IsModalAbilityActive(oCaster, nAbility) != EngineConstants.FALSE)
          {

#if DEBUG
               Log_Rules(" ++++++Modal Ability  deactivated");
#endif

               // -----------------------------------------------------------------
               // Handle player requests to disable a modal ability
               // If the modal ability is running, send Deactivate xEvent to the spellscript
               // -----------------------------------------------------------------
               xEvent evDeactivate = EventSpellScriptDeactivate(oCaster, nAbility, nAbilityType);
               return Ability_DoRunSpellScript(evDeactivate, nAbility, nAbilityType);                // we don't care for the return value of this function

          }
#if DEBUG
          Log_Rules(" ++++++Modal Ability  not deactivated ");
#endif
          return EngineConstants.COMMAND_RESULT_SUCCESS;
     }

     /* ----------------------------------------------------------------------------
     * @brief (deprectated) Resolves the effects of abilities that trigger 'OnHit'
     *
     * @param oAttacker          The Attacking Creature
     * @param oTarget            The attacked creature
     *
     * @author   Georg Zoeller
     *  -----------------------------------------------------------------------------
     **/
     public void Ability_ResolveOnHitAbilities(GameObject oAttacker, GameObject oTarget)
     {

     }

     public float Ability_GetScaledEffectDuration(int nAbility, GameObject oCaster, float fDuration)
     {
          return fDuration;
     }

     public int Ability_GetScaledDamage(int nAbility, GameObject oCaster, int nDamage)
     {

          return nDamage;
     }

     public int Ability_GetScaledHeal(int nAbility, GameObject oCaster, int nHeal)
     {
          return nHeal;
     }

     public int Ability_GetScaledCost(int nAbility, GameObject oCaster, int nCost)
     {

          return nCost;
     }

     public void Ability_PreventAbilityEffectStacking(GameObject oTarget, GameObject oCaster, int nAbility)
     {
          RemoveStackingEffects(oTarget, oCaster, nAbility);
     }

     /*
     *  @brief Ability xEvent filtering functions.
     *
     *  Performs some filtering on Ability Event Targets. Be very careful with this,
     *  best don't touch if your name doesn't start with ge and doesn't end with org ;p
     *
     *  @author georg
     */
     public int Ability_IsValidAOETarget(GameObject oTarget, GameObject oCaster, int nAbility)
     {
          int bReturn = EngineConstants.TRUE;

          // For Now, return true for all placeables
          if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_PLACEABLE)
          {
               return EngineConstants.TRUE;
          }

          // Georg: This also prevents corpses from being affected by AoE objects.
          if (IsDead(oTarget) != EngineConstants.FALSE || IsDying(oTarget) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.IsValidAoETarget", "AOE Target ignored: dead or dying", oTarget);
#endif
               return EngineConstants.FALSE;
          }

          // non hostile opponents of a different. This isn't as clean as I want it to be, will have to revise later.
          if (IsObjectHostile(oCaster, oTarget) == EngineConstants.FALSE)
          {
               if (GetGroupId(oCaster) != GetGroupId(oTarget))
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.IsValidAoETarget", "not hostile and in different group", oTarget);
#endif
                    return EngineConstants.FALSE;
               }
          }

          return bReturn;
     }

     /*
     *  @brief Ability xEvent filtering functions.
     *
     *  Performs some filtering on Ability Event Targets
     *
     *  @author georg
     */
     public int Ability_IsAbilityTargetValid(GameObject oTarget, int nAbility, int nTargetType)
     {

          int bRet = EngineConstants.TRUE;

          if (IsDead(oTarget) != EngineConstants.FALSE)
          {
               if ((nTargetType & EngineConstants.TARGET_TYPE_BODY) == EngineConstants.TARGET_TYPE_BODY)
               {
                    bRet = EngineConstants.TRUE;
               }
               else
               {
                    bRet = EngineConstants.FALSE;
               }
          }

          if (IsDying(oTarget) != EngineConstants.FALSE && (nAbility != EngineConstants.ABILITY_SPELL_REVIVAL))
          {
               bRet = EngineConstants.FALSE;
          }

          return bRet;

     }

     /* ----------------------------------------------------------------------------
     * @brief (events__h) Wrapper for ApplyEffectOnObject on an array of Objects.
     *
     * @param nDurationType can be EngineConstants.EFFECT_DURATION_TYPE_PERMANENT EngineConstants.EFFECT_DURATION_TYPE_INSTANTANEOUS or EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY.
     * @param Effect the xEffect to be applied
     * @param arTarget the targets of the effect
     * @param fDuration  this value needs to be set only when nDurationType is EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY
     * @param oCreator xEffect creator
     * @param nAbilityId The ability ID of the xEffect (Important for dispelling!!!)
     * @param bSendAttackedEvent Whether to send an attacked xEvent as well.
     * @param bPreventStacking PrxEvent Stacking?.
     *
     * @author Georg Zoeller
     *  ---------------------------------------------------------------------------**/
     public void Ability_ApplyEffectOnObjectArray(int nDurationType, xEffect eEffect, List<GameObject> arTargets, float fDuration = 0.0f, GameObject oCreator = null, int nAbilityId = 0, int bSendAttackedEvent = EngineConstants.FALSE, int bPreventStacking = EngineConstants.TRUE, int bExcludeCreator = EngineConstants.FALSE)
     {
          if (oCreator == null) oCreator = gameObject;
          int nCount = GetArraySize(arTargets);
          int i;
          for (i = 0; i < nCount; i++)
          {
               if (bExcludeCreator == EngineConstants.FALSE || arTargets[i] != oCreator)
               {
                    RemoveStackingEffects(arTargets[i], oCreator, nAbilityId);
                    ApplyEffectOnObject(nDurationType, eEffect, arTargets[i], fDuration, oCreator, nAbilityId);

                    if (bSendAttackedEvent != EngineConstants.FALSE)
                    {
                         SendEventOnCastAt(arTargets[i], oCreator, nAbilityId, EngineConstants.TRUE);
                    }
               }
          }
     }

     /* ----------------------------------------------------------------------------
     * @brief (events__h) Wrapper for ApplyEffectOnObject on an array of Objects.
     *
     * @param nDurationType      - Will only accept EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY.
     * @param eEffect            - The xEffect to be applied.
     * @param arTarget           - The targets of the effect.
     * @param fDurationMin       - Minimum duration of the effect.
     * @param fDurationMax       - Maximum duration of the effect.
     * @param oCreator           - The xEffect creator.
     * @param nAbilityId         - The ability ID of the effect. (Important for dispelling!!!)
     * @param bSendAttackedEvent - Whether to send an attacked xEvent as well.
     * @param bPreventStacking   - PrxEvent Stacking?
     * @param bExcludeCreator    - Exclude creator?
     *
     * @author Georg Zoeller
     *
     * Modified 28/11/2007 by PeterT
     *
     * - Adjusted Ability_ApplyEffectOnObjectArray to make the duration random in a specified range.
     *  ---------------------------------------------------------------------------**/
     public void Ability_ApplyRandomDurationEffectOnObjectArray(int nDurationType, xEffect eEffect, List<GameObject> arTargets, float fDurationMin = 0.0f, float fDurationMax = 0.0f, GameObject oCreator = null, int nAbilityId = 0, int bSendAttackedEvent = EngineConstants.FALSE, int bPreventStacking = EngineConstants.TRUE, int bExcludeCreator = EngineConstants.FALSE)
     {
          if (oCreator == null) oCreator = gameObject;
          if (nDurationType == EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY)
          {
               int nCount = GetArraySize(arTargets);
               float fDuration = 0.0f;
               int i;
               for (i = 0; i < nCount; i++)
               {
                    if (bExcludeCreator == EngineConstants.FALSE || arTargets[i] != oCreator)
                    {
                         // determine random duration
                         fDuration = (RandomFloat() * (fDurationMax - fDurationMin)) + fDurationMin;
                         if (fDuration < fDurationMin) // lower bound
                         {
                              fDuration = fDurationMin;
                         }
                         if (fDuration > fDurationMax) // upper bound
                         {
                              fDuration = fDurationMax;
                         }

                         RemoveStackingEffects(arTargets[i], oCreator, nAbilityId);
                         ApplyEffectOnObject(nDurationType, eEffect, arTargets[i], fDuration, oCreator, nAbilityId);

                         if (bSendAttackedEvent != EngineConstants.FALSE)
                         {
                              SendEventOnCastAt(arTargets[i], oCreator, nAbilityId, EngineConstants.TRUE);
                         }
                    }
               }
          }
     }

     /*
     * @brief Determine whether or not all conditions for the current talent are met
     *
     * This is temporary, it will go into the engine at some point
     * Check the use conditions for the ability, e.g. melee only
     * Later this should be
     * done mostly in the UI (e.g. don't even allow to use it)
     * 0   None
     * 1   Melee
     * 2   Shield
     * 4   Ranged
     * 8   Behind Target
     * 16  Mode active
     *
     *
     * @author Georg
     */
     public int Ability_CheckUseConditions(GameObject oCaster, GameObject oTarget, int nAbility, GameObject oItem = null)
     {

          string sItemTag = IsObjectValid(oItem) != EngineConstants.FALSE ? GetTag(oItem) : "";
          int nAbilityType = GetAbilityType(nAbility);

          int nCondition = GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "conditions", nAbility);
          int bRet = EngineConstants.TRUE;

          float fCooldown = GetRemainingCooldown(oCaster, nAbility, sItemTag);
          if (fCooldown > 0.0f)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_CheckUseConditions", "Trying to execute ability with cooldown " + ToString(fCooldown) + " remaining ability which is already active. EngineConstants.FALSE.");
#endif
               return EngineConstants.FALSE;
          }
          else

          // If nAbility is modal and active already - do nothing
          if (IsModalAbilityActive(oCaster, nAbility) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_CheckUseConditions", "Trying to execute modal ability which is already active. EngineConstants.FALSE.");
#endif
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // No conditions? bail right here
          // -------------------------------------------------------------------------
          if (nCondition == 0)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "EngineConstants.TRUE (no condition)");
#endif
               return EngineConstants.TRUE;
          }

          // -------------------------------------------------------------------------
          // CONDITION_MELEE_WEAPON - Caster needs a melee weapon in main hand
          // -------------------------------------------------------------------------
          if ((nCondition & 1) == 1)
          {

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "Melee: " + ((bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
#endif
               bRet = bRet != EngineConstants.FALSE && IsUsingMeleeWeapon(oCaster) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;

               if (bRet == EngineConstants.FALSE)
               {
                    return EngineConstants.FALSE;
               }

          }

          // -------------------------------------------------------------------------
          // CONDITION_SHIELD - Caster needs a shield in the offhand
          // -------------------------------------------------------------------------
          if ((nCondition & 2) == 2)
          {
               bRet = bRet != EngineConstants.FALSE && IsUsingShield(oCaster) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "Shield: " + ((bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
#endif

               if (bRet == EngineConstants.FALSE)
               {
                    return EngineConstants.FALSE;
               }

          }

          // -------------------------------------------------------------------------
          // CONDITION_RANGED_WEAPON - Caster needs a ranged weapon in main hand
          // -------------------------------------------------------------------------
          if ((nCondition & 4) == 4)
          {
               bRet = bRet != EngineConstants.FALSE && IsUsingRangedWeapon(oCaster) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "Ranged: " + ((bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
#endif

               if (bRet == EngineConstants.FALSE)
               {
                    return EngineConstants.FALSE;
               }

          }

          // -------------------------------------------------------------------------
          // CONDITION_BEHIND_TARGET - Caster needs to be located behind the target
          // -------------------------------------------------------------------------
          if ((nCondition & 8) == 8)
          {
               float fAngle = GetAngleBetweenObjects(oTarget, oCaster);

               bRet = bRet != EngineConstants.FALSE && (fAngle >= 90.0f && fAngle <= 270.0f) ? EngineConstants.TRUE : EngineConstants.FALSE;
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "Back: " + ((bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
#endif

               if (bRet == EngineConstants.FALSE)
               {
                    return EngineConstants.FALSE;
               }

          }

          // -------------------------------------------------------------------------
          // CONDITION_ACTIVE_MODAL_ABILITY - A specific modal ability needs to be active
          // -------------------------------------------------------------------------
          if ((nCondition & 16) == 16)
          {
               int nModalAbility = GetM2DAInt(EngineConstants.TABLE_ABILITIES_TALENTS, "condition_mode", nAbility);
               if (nModalAbility != 0)
               {
                    bRet = bRet != EngineConstants.FALSE && IsModalAbilityActive(oCaster, nModalAbility) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "Mode Active: " + ((bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
#endif
               }

               if (bRet == EngineConstants.FALSE)
               {
                    return EngineConstants.FALSE;
               }

          }

          // -------------------------------------------------------------------------
          // CONDITION_TARGET_HUMANOID - Target is humanoid
          // -------------------------------------------------------------------------
          if ((nCondition & 32) == 32)
          {
               bRet = bRet != EngineConstants.FALSE && IsHumanoid(oTarget) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "IsHumanoid: " + ((bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
#endif

               if (bRet == EngineConstants.FALSE)
               {
                    return EngineConstants.FALSE;
               }

          }

          // -------------------------------------------------------------------------
          // CONDITION_DUAL_WEAPONS
          // -------------------------------------------------------------------------
          if ((nCondition & 64) == 64)
          {
               bRet = bRet != EngineConstants.FALSE && GetWeaponStyle(oCaster) == EngineConstants.WEAPONSTYLE_DUAL ? EngineConstants.TRUE : EngineConstants.FALSE;
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "UsingDualWeapons: " + ((bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
#endif

               if (bRet == EngineConstants.FALSE)
               {
                    return EngineConstants.FALSE;
               }
          }

          // -------------------------------------------------------------------------
          // CONDITION_DUAL_WEAPONS
          // -------------------------------------------------------------------------
          if ((nCondition & 128) == 128)
          {
               bRet = bRet != EngineConstants.FALSE && GetWeaponStyle(oCaster) == EngineConstants.WEAPONSTYLE_TWOHANDED ? EngineConstants.TRUE : EngineConstants.FALSE;
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", "Using2HWeapon: " + ((bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
#endif
               if (bRet == EngineConstants.FALSE)
               {
                    return EngineConstants.FALSE;
               }

          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CheckUseConditions", (bRet != EngineConstants.FALSE) ? "EngineConstants.TRUE" : "EngineConstants.FALSE" + " condition: " + IntToHexString(nCondition));
#endif

          return bRet;

     }

     /*
     *   @ Simple wrapper to use a modal ability on a creature
     *
     *   Makes a creature use a talent or ability. Will add ability to the creature if asked to
     *   Ability Use xCommand is added to the top of the action queue unless otherwise specified. This function is asynchronous
     *   as it uses a command.
     *   The xCommand can still fail if the creature doesn't have the mana to use it, etc.
     *
     *   @param oCreature The creature using the ability
     *   @param nAbility The EngineConstants.ABILITY_* to use
     *   @param oTarget The ability target (should be same as oCreature for modal talents
     *   @param bAddIfNeeded Add the talent to the creature if it doesn't have it
     *   @param bAddCommandToTop Whether or not to add the use ability xCommand at the top or bottom of the action queue
     *
     *   @author Georg Zoeller
     *
     **/
     public void Ability_UseAbilityWrapper(GameObject oCreature, int nAbility, GameObject oTarget = null, int bAddIfNeeded = EngineConstants.FALSE, int bAddCommandToTop = EngineConstants.TRUE)
     {
          if (oTarget == null) oTarget = gameObject;
          if (HasAbility(oCreature, nAbility) == EngineConstants.FALSE && bAddIfNeeded != EngineConstants.FALSE)
          {
               AddAbility(oCreature, nAbility);
          }

          xCommand cmdUse = CommandUseAbility(nAbility, oTarget, Vector3.zero);
          WR_AddCommand(oCreature, cmdUse, bAddCommandToTop);

     }

     /*
     * @brief Checks if a specific ability is active on an object
     *
     * The check includes modal abilities and any other abilities with
     * a duration (all buffs and de-buffs)
     *
     * @param oObject the GameObject we are checking for the ability
     * @param nAbilityID the ability we check if is active
     * @returns EngineConstants.TRUE if the ability is active, EngineConstants.FALSE otherwise
     *
     * @author   Yaron Jakobs
     *
     **/
     public int Ability_IsAbilityActive(GameObject oCreature, int nAbilityID)
     {
          int nActive = EngineConstants.FALSE;
          List<xEffect> thisEffects = GetEffects(oCreature, EngineConstants.EFFECT_TYPE_INVALID, nAbilityID);
          int nSize = GetArraySize(thisEffects);

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_IsAbilityActive", "START, abilityID: " + IntToString(nAbilityID) + ", number of effects for this ability: " + IntToString(nSize), oCreature);
#endif

          return (nSize > 0 ? EngineConstants.TRUE : EngineConstants.FALSE);

     }

     public int Ability_GetImpactLocationVfxId(int nAbility)
     {
          return GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "vfx_impact0", nAbility);
     }

     public int Ability_GetImpactObjectVfxId(int nAbility)
     {
          return GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "vfx_impact1", nAbility);
     }

     public void Ability_ApplyLocationImpactVFX(int nAbility, Vector3 lTarget)
     {
          int nVfx = Ability_GetImpactLocationVfxId(nAbility);

          if (nVfx > 0 /*ability has a vfx*/)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "ability_h.ApplyLocationImpactVF", "ApplyVFX: " + ToString(nVfx));
#endif
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(nVfx), lTarget, 0.0f);
          }
     }

     public void Ability_ApplyObjectImpactVFX(int nAbility, GameObject oTarget)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_ApplyObjectImpactVFX.ApplyLocationImpactVF",
              "Ability: " + IntToString(nAbility) + ", Target: " + GetTag(oTarget));
#endif

          int nVfx = Ability_GetImpactObjectVfxId(nAbility);

          if (nVfx > 0 /*ability has a vfx*/)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Ability_ApplyObjectImpactVFX", "ApplyVFX: " + ToString(nVfx));
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(nVfx), oTarget, 0.0f);
          }
     }

     public int Ability_IsAoE(int nAbility)
     {
          return (GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "aoe_type", nAbility) > 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
     *   @ HandleEventOutOfMana
     *
     *   Handles the ability specific side of the out of mana event, mostly deactivating
     *   continuously draining effects such as berserk.
     *
     *   @param oCreature The creature receiving the out of mana event
     *
     *   @author Georg Zoeller
     *
     **/
     public void Ability_HandleEventOutOfManaStamina(GameObject oCreature)
     {
          List<xEffect> aEffects = GetEffects(oCreature, EngineConstants.EFFECT_TYPE_UPKEEP, 0, oCreature);
          int nSize = GetArraySize(aEffects);
          int i;
          int nType;
          int nId;
          for (i = 0; i < nSize; i++)
          {
            xEffect _effect = aEffects[i];
               nId = GetEffectAbilityIDRef(ref _effect);
               nType = GetAbilityType(nId);

               if (nType == EngineConstants.ABILITY_TYPE_SPELL || nType == EngineConstants.ABILITY_TYPE_TALENT)
               {
                    // -----------------------------------------------------------------
                    // If the 'out of mana ends ability flag' is set, kill it.
                    // -----------------------------------------------------------------
                    int nFlag = GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "flags", nId);
                    if ((nFlag & EngineConstants.ABILITY_FLAG_END_ON_OUT_OF_MANA) == EngineConstants.ABILITY_FLAG_END_ON_OUT_OF_MANA)
                    {
                         RemoveEffect(oCreature, aEffects[i]);
                    }
               }
          }
     }

     public int Ability_CheckFlag(int nAbility, int nFlag)
     {

          int nAbiFlags = GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "Flags", nAbility);
          return ((nAbiFlags & nFlag) == nFlag) ? EngineConstants.TRUE : EngineConstants.FALSE;

     }

     /*
          @brief Utility function to remove all effects applied by abilities that have applied an effect
                 of the given type.

          @author georg
     */
     public void _RemoveAbilitiesMatchingEffectType(GameObject oTarget, int nType, int nExcludeAbility = 0)
     {

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "ability_h._RemoveAbilitiesMatchingEffectType", "Removing all effects matching type " + ToString(nType) + " excluding " + ToString(nExcludeAbility));
#endif

          List<xEffect> effects = GetEffects(oTarget, nType);
          int i;
          int nId;
          int nSize = GetArraySize(effects);
          for (i = 0; i < nSize; i++)
          {
               if (IsEffectValid(effects[i]) != EngineConstants.FALSE)
               {
                xEffect _effect = effects[i];
                    nId = GetEffectAbilityIDRef(ref _effect);
                    if (nId != nExcludeAbility && nId != 0 /* we don't touch non ability effects */)
                    {
                         RemoveEffectsByParameters(oTarget, EngineConstants.EFFECT_TYPE_INVALID, nId);
                    }
               }
          }

     }

     public void Ability_HandleOnDamageAbilities(GameObject oDamaged, GameObject oAttacker, float fDamage, int nDamageType, int nAbility)
     {

          // -------------------------------------------------------------------------
          // All sleep is cancelled when damaged
          // -------------------------------------------------------------------------
          if (GetHasEffects(oDamaged, EngineConstants.EFFECT_TYPE_SLEEP) != EngineConstants.FALSE)
          {
               _RemoveAbilitiesMatchingEffectType(oDamaged, EngineConstants.EFFECT_TYPE_SLEEP);
          }

          // -------------------------------------------------------------------------
          // All root is cancelled when damaged
          // -------------------------------------------------------------------------
          if (GetHasEffects(oDamaged, EngineConstants.EFFECT_TYPE_ROOT) != EngineConstants.FALSE)
          {
               _RemoveAbilitiesMatchingEffectType(oDamaged, EngineConstants.EFFECT_TYPE_ROOT, nAbility);
          }

          if (IsModalAbilityActive(oAttacker, EngineConstants.ABILITY_TALENT_SUPPRESSING_FIRE) != EngineConstants.FALSE)
          {
               xEffect eDebuff = EffectDecreaseProperty(EngineConstants.PROPERTY_ATTRIBUTE_ATTACK, -7.5f);
               SetEffectEngineIntegerRef(ref eDebuff, EngineConstants.EFFECT_INTEGER_VFX, 90063);

               float fDur = MinF(GetRankAdjustedEffectDuration(oDamaged, 10.0f), 15.0f);

               ApplyEffectVisualEffect(oAttacker, oDamaged, 90002, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eDebuff, oDamaged, fDur, oAttacker, EngineConstants.ABILITY_TALENT_SUPPRESSING_FIRE);
          }

          // -------------------------------------------------------------------------
          // DUAL_WEAPON_EXPERT
          //
          // If the attacker has the DUAL_WEAPON_EXPERT talent, he may cause bleeding.
          // This is done by simulating a hit with a 'vicious' item property weapon.
          // A bit weird, but saves us the whole implementation of that feature.
          // -------------------------------------------------------------------------
          if (IsObjectValid(oAttacker) != EngineConstants.FALSE)
          {
               if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_DUAL_WEAPON_EXPERT) != EngineConstants.FALSE)
               {
                    if (IsHumanoid(oAttacker) != EngineConstants.FALSE)
                    {
                         if (CanCreatureBleed(oDamaged) != EngineConstants.FALSE && IsCreatureSpecialRank(oAttacker) != EngineConstants.FALSE)
                         {
                              if (GetWeaponStyle(oAttacker) == EngineConstants.WEAPONSTYLE_DUAL)
                              {
                                   // ---------------------------------------------------------
                                   // Georg: Relevant engine code for end user reference:
                                   /*
                                       pAttackerOnHitEvent->SetCreator(nAttackerId);
                                       pAttackerOnHitEvent->SetObjectId(0, nAttackerId);
                                       pAttackerOnHitEvent->SetInteger(0, pAttackerItem->GetOnHitEffectId());
                                       pAttackerOnHitEvent->SetInteger(1, pAttackerItem->GetOnHitPower());

                                       pAttackerOnHitEvent->SetObjectId(1, pAttackerItem->GetId());
                                       pAttackerOnHitEvent->SetTarget(a_pEventData->m_nTargetId);
                                   */
                                   // ---------------------------------------------------------

                                   xEvent eOnHit = Event(EngineConstants.EVENT_TYPE_ITEM_ONHIT);
                                   SetEventCreatorRef(ref eOnHit, oAttacker);
                                   SetEventIntegerRef(ref eOnHit, 0, EngineConstants.ITEM_PROPERTY_ONHIT_VICIOUS);
                                   SetEventIntegerRef(ref eOnHit, 1, ((GetLevel(oAttacker) / 5) + 1));
                                   SetEventIntegerRef(ref eOnHit, 2, EngineConstants.TRUE);
                                   DelayEvent(0.0f, oDamaged, eOnHit);

                              }
                         }
                    }

               }

          }

     }

     public List<GameObject> Ability_GetTargetAllies(GameObject oCaster)
     {
          List<GameObject> arTargets;
          int nMaxAllies = GetLocalInt(GetModule(), EngineConstants.ABILITY_ALLY_NUMBER);

          if (IsFollower(oCaster) != EngineConstants.FALSE)
               arTargets = GetPartyList(oCaster);
          else // non party member
               arTargets = GetNearestObjectByGroup(oCaster, GetGroupId(oCaster), EngineConstants.OBJECT_TYPE_CREATURE, nMaxAllies, EngineConstants.TRUE, EngineConstants.FALSE, EngineConstants.TRUE);

          return arTargets;
     }

     public void Ability_OnGameModeChange(int nNewGM)
     {
          if (nNewGM == EngineConstants.GM_EXPLORE)
          {
               List<GameObject> partyMembers = GetPartyList();
               int memberCount = GetArraySize(partyMembers);
               int i;

               for (i = 0; i < memberCount; i++)
               {

                    // ----------------------------------------------------------------
                    // Feign Death ends automatically at the end of combat
                    // ----------------------------------------------------------------
                    if (IsModalAbilityActive(partyMembers[i], EngineConstants.ABILITY_TALENT_FEIGN_DEATH) != EngineConstants.FALSE)
                    {
                         Ability_DeactivateModalAbility(partyMembers[i], EngineConstants.ABILITY_TALENT_FEIGN_DEATH);
                    }

                    // ----------------------------------------------------------------
                    // Dueling ends automatically at the end of combat
                    // ----------------------------------------------------------------

                    // REMOVED PER EV 155600 -- yaron
                    /*if (IsModalAbilityActive(partyMembers[i],EngineConstants.ABILITY_TALENT_DUELING))
                    {
                       Ability_DeactivateModalAbility(partyMembers[i],EngineConstants.ABILITY_TALENT_DUELING);
                    }*/

                    /*
                    if (IsModalAbilityActive(partyMembers[i],EngineConstants.ABILITY_TALENT_SHIELD_DEFENSE))
                    {
                       Ability_DeactivateModalAbility(partyMembers[i],EngineConstants.ABILITY_TALENT_SHIELD_DEFENSE);
                    }

                    if (IsModalAbilityActive(partyMembers[i],EngineConstants.ABILITY_TALENT_SHIELD_WALL))
                    {
                       Ability_DeactivateModalAbility(partyMembers[i],EngineConstants.ABILITY_TALENT_SHIELD_WALL);
                    }

                    if (IsModalAbilityActive(partyMembers[i],EngineConstants.ABILITY_SPELL_SPELL_SHIELD))
                    {
                       Ability_DeactivateModalAbility(partyMembers[i],EngineConstants.ABILITY_SPELL_SPELL_SHIELD);
                    }

                    if (IsModalAbilityActive(partyMembers[i],EngineConstants.ABILITY_TALENT_SHIELD_COVER))
                    {
                       Ability_DeactivateModalAbility(partyMembers[i],EngineConstants.ABILITY_TALENT_SHIELD_COVER);
                    }

                    */
                    if (IsModalAbilityActive(partyMembers[i], EngineConstants.ABILITY_SPELL_BLOOD_MAGIC) != EngineConstants.FALSE)
                    {
                         Ability_DeactivateModalAbility(partyMembers[i], EngineConstants.ABILITY_SPELL_BLOOD_MAGIC);
                    }

                    if (IsModalAbilityActive(partyMembers[i], EngineConstants.ABILITY_TALENT_CAPTIVATE) != EngineConstants.FALSE)
                    {
                         Ability_DeactivateModalAbility(partyMembers[i], EngineConstants.ABILITY_TALENT_CAPTIVATE);
                    }

                    if (IsModalAbilityActive(partyMembers[i], EngineConstants.ABILITY_TALENT_PAIN) != EngineConstants.FALSE)
                    {
                         Ability_DeactivateModalAbility(partyMembers[i], EngineConstants.ABILITY_TALENT_PAIN);
                    }

                    // EV 156479
                    /*if (IsModalAbilityActive(partyMembers[i],EngineConstants.ABILITY_TALENT_BLOOD_FRENZY))
                    {
                       Ability_DeactivateModalAbility(partyMembers[i],EngineConstants.ABILITY_TALENT_BLOOD_FRENZY);
                    }*/

                    if (IsModalAbilityActive(partyMembers[i], EngineConstants.ABILITY_TALENT_BERSERK) != EngineConstants.FALSE)
                    {
                         Ability_DeactivateModalAbility(partyMembers[i], EngineConstants.ABILITY_TALENT_BERSERK);
                    }
               }

          }
     }
}