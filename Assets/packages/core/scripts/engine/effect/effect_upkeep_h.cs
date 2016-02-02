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
     // effect_upkeep.nss
     // -----------------------------------------------------------------------------
     /*
         Effect: Upkeep

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"events_h"
     //#include"effect_constants_h"
     //#include"attributes_h"
     //#include"config_h"
     //#include"ui_h"

     //moved public const int EngineConstants.VFX_MODAL_DEACTIVATE = 1146;

     // -----------------------------------------------------------------------------
     /* @brief Applies an upkeep xEffect to a creature for the duration of the parent ability
     *
     * Upkeep Effects take away some of the characters maximum stamina or mana and restore
     * it when they expire. They also take away current mana when created and partially
     * refund it when expiring.
     *
     * @param  nUpkeepType The EngineConstants.UPKEEP_TYPE_* (mana or stamina)
     * @param  nValue The cost of the upkeep xEffect (usually a negative integer read from abi_base)
     * @param  nAbilityId Ability ID of the ability that is being upkept
     * @param  oTarget The target of the beneficial xEffect of the upkeep spell
     *
     * @returns Upkeep Effect
     *
     * @author Georg
     **/

     //moved public const int EngineConstants.PROPERTY_ATTRIBUTE_FATIGUE = 41;

     public xEffect EffectUpkeep(int nUpkeepType, float fCost, int nAbilityId, GameObject oTarget, int bPartywide)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_UPKEEP);
          SetEffectIntegerRef(ref eEffect, 0, nUpkeepType);
          SetEffectIntegerRef(ref eEffect, 1, nAbilityId);
          SetEffectIntegerRef(ref eEffect, 2, bPartywide);
          SetEffectObjectRef(ref eEffect, 0, oTarget);

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "BAFAF", "Cost:" + ToString(fCost));

          SetEffectFloatRef(ref eEffect, 0, fCost);

          float fFatigue = GetM2DAFloat(EngineConstants.TABLE_ABILITIES_SPELLS, "fatigue", nAbilityId);
          DEBUG_PrintToScreen(ToString(fCost));

          SetEffectFloatRef(ref eEffect, 1, fFatigue);
          return eEffect;
     }

     public int Effects_HandleApplyEffectUpkeep(xEffect eEffect)
     {
          int nUpkeepType = GetEffectIntegerRef(ref eEffect, 0);
          int nAbilityId = GetEffectIntegerRef(ref eEffect, 1);
          float fFatigue = GetEffectFloatRef(ref eEffect, 1);

          float fAmount = GetEffectFloatRef(ref eEffect, 0);
          // -----------------------------------------------------------------
          // Do not apply this xEffect if it would bring us below 0
          // -----------------------------------------------------------------

          float fMaxStamina = GetCreatureProperty(gameObject, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_TOTAL);

          if (fMaxStamina + fAmount < 0.0f)
          {
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // This is where the upkeep amount is reserved
          // -------------------------------------------------------------------------
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fAmount, EngineConstants.PROPERTY_VALUE_MODIFIER);

          // -------------------------------------------------------------------------
          // This is where the fatigue cost gets applied
          // -------------------------------------------------------------------------
          if (fFatigue > 0.0f)
          {
               UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_FATIGUE, fFatigue, EngineConstants.PROPERTY_VALUE_MODIFIER);
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_upkeep_h.Effects_HandleApplyEffectUpkeep", "Upkeep xEffect applied for ability " + IntToString(nAbilityId));
#endif

          // -------------------------------------------------------------------------
          // Valid Ability ID - Synchronize Modal ability UI
          // -------------------------------------------------------------------------
          if (nAbilityId != EngineConstants.ABILITY_INVALID)
          {
               Engine_SetModalAbilityGUI(gameObject, nAbilityId, EngineConstants.TRUE);
          }

          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     // @brief Helper, remove effects held up by an upkeep effect.
     //
     // @param oCreator      - The xEffect creator
     // @param oTarget       - The creature to remove the effects from
     // @param nAbilityId    - The ability id of the effects to be removed
     // @author Georg
     // -----------------------------------------------------------------------------
     public void _RemoveUpkeptEffects(GameObject oCreator, GameObject oTarget, int nAbilityId)
     {
          List<xEffect> effects = GetEffectsByAbilityId(oTarget, nAbilityId);
          int nCount = GetArraySize(effects);
          int i;
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_upkeep_h._RemoveUpkeptEffects", "removing " + IntToString(nCount) + " upkept effects for ability id: " + IntToString(nAbilityId) + " creator:" + GetTag(oCreator), oTarget);

          for (i = 0; i < nCount; i++)
          {
            xEffect _effect = effects[i];
               if (IsEffectValid(_effect) != EngineConstants.FALSE)
               {
                    if (GetEffectTypeRef(ref _effect) != EngineConstants.EFFECT_TYPE_UPKEEP)
                    {
                         if (GetEffectCreatorRef(ref _effect) == oCreator)
                         {

                              RemoveEffect(oTarget, effects[i]);
                         }
                    }
               }
          }

          if (nCount != EngineConstants.FALSE)
          {
               ApplyEffectVisualEffect(gameObject, gameObject, EngineConstants.VFX_MODAL_DEACTIVATE, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
          }

     }

     public int Effects_HandleRemoveEffectUpkeep(xEffect eEffect)
     {
          int nUpkeepType = GetEffectIntegerRef(ref eEffect, 0);
          int nPercent = Config_GetSetting(EngineConstants.CONFIG_SETTING_UPKEEP_RETURN_PERCENT);
          int nAbilityId = GetEffectIntegerRef(ref eEffect, 1);
          int bPartywide = GetEffectIntegerRef(ref eEffect, 2);
          GameObject oTarget = GetEffectObjectRef(ref eEffect, 0);
          GameObject oCreator = GetEffectCreatorRef(ref eEffect);
          float fFatigue = GetEffectFloatRef(ref eEffect, 1) * -1.0f;

          float fAmount = GetEffectFloatRef(ref eEffect, 0) * -1.0f;

          if (bPartywide != EngineConstants.FALSE && IsFollower(oCreator) != EngineConstants.FALSE)
          {
               List<GameObject> arrParty = GetPartyList();
               int nCount = GetArraySize(arrParty);
               int i;

               for (i = 0; i < nCount; i++)
               {
                    if (IsObjectValid(arrParty[i]) != EngineConstants.FALSE)
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "effect_upkeep_h.Effects_HandleRemoveEffectUpkeep", "bPartyWide: removing from " + GetTag(arrParty[i]), arrParty[i]);

                         _RemoveUpkeptEffects(oCreator, arrParty[i], nAbilityId);
                    }
               }

          }
          else
          {
               // -------------------------------------------------------------------------
               // Purge all the effects held up by this upkeep xEffect of the original
               // target and the caster
               // * identified by ability_id
               // -------------------------------------------------------------------------
               if (IsObjectValid(oTarget) != EngineConstants.FALSE && oTarget != gameObject)
               {
                    _RemoveUpkeptEffects(oCreator, oTarget, nAbilityId);
               }

               _RemoveUpkeptEffects(oCreator, gameObject, nAbilityId);
          }

          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fAmount, EngineConstants.PROPERTY_VALUE_MODIFIER);

          if (fFatigue < 0.0f)
          {
               UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_FATIGUE, fFatigue, EngineConstants.PROPERTY_VALUE_MODIFIER);
          }

          // -------------------------------------------------------------------------
          // Valid Ability ID - Synchronize Modal ability UI
          // -------------------------------------------------------------------------
          if (nAbilityId != EngineConstants.ABILITY_INVALID)
          {
               Engine_SetModalAbilityGUI(gameObject, nAbilityId, EngineConstants.FALSE);
               Ability_SetCooldown(gameObject, nAbilityId);
          }

          return EngineConstants.TRUE;
     }

     public void Effects_RemoveUpkeepEffect(GameObject oCaster, int nAbilityId)
     {
          List<xEffect> effects = GetEffects(oCaster, EngineConstants.EFFECT_TYPE_UPKEEP, nAbilityId);

          int nSize = GetArraySize(effects);
          int i;

          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_upkeep_h.Effects_RemoveUpkeepEffect", "removing " + IntToString(nSize) + " upkeep effects for ability id: " + IntToString(nAbilityId), oCaster);
          UI_DisplayAbilityMessage(oCaster, nAbilityId, EngineConstants.TRUE);

          for (i = 0; i < nSize; i++)
          {
               RemoveEffect(oCaster, effects[i]);
          }
     }
}