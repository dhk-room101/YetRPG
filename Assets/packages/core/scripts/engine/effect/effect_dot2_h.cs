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
     // effect_dot_h.nss
     // -----------------------------------------------------------------------------
     /*
         Effect: Damage over Time

         */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"core_h"
     //#include"effect_constants_h"
     //#include"2da_constants_h"
     //#include"effect_damage_h"
     //#include"ui_h"

     //moved public const float EngineConstants.CREATURE_RULES_TICK_DELAY = 1.5f;

     //moved public const float STAT_UPDATE_COMBAT = 2.0f;

     /* ----------------------------------------------------------------------------
     * @brief Remove an array of effects from a target;
     *
     * @returns  # of removed effects
     * @author   Georg Zoeller
     *  -----------------------------------------------------------------------------
     **/
     public int RemoveEffectArray(GameObject oTarget, List<xEffect> aEffects)
     {
          int nSize = GetArraySize(aEffects);
          int i;
          for (i = 0; i < nSize; i++)
          {
               RemoveEffect(oTarget, aEffects[i]);
          }
          return nSize;
     }

     public List<xEffect> GetDotEffectByDamageType(GameObject oCreature, int nDamageType = EngineConstants.DAMAGE_TYPE_FIRE)
     {
          List<xEffect> ret = new List<xEffect>();

          List<xEffect> effects = GetEffects(oCreature, EngineConstants.EFFECT_TYPE_DOT);
          int nSize = GetArraySize(effects);
          int i;
          int j = 0;

          for (i = 0; i < nSize; i++)
          {
            xEffect _effect = effects[i];
               if (GetEffectIntegerRef(ref _effect, 1) == nDamageType)
               {
                    ret[j++] = effects[i];
               }
          }

          return ret;
     }

     public int HasDotEffectOfType(GameObject oCreature, int nDamageType)
     {
          return GetArraySize(GetDotEffectByDamageType(oCreature, nDamageType));
     }

     public void RemoveFireBasedEffects(GameObject oCreature)
     {
          List<xEffect> effects = GetDotEffectByDamageType(oCreature, EngineConstants.DAMAGE_TYPE_FIRE);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_dot2_h._EffectDOT()", "Removing" + ToString(GetArraySize(effects)));
#endif
          RemoveEffectArray(oCreature, effects);
     }

     public void RemoveColdBasedEffects(GameObject oCreature)
     {
          List<xEffect> effects = GetDotEffectByDamageType(oCreature, EngineConstants.DAMAGE_TYPE_COLD);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_dot2_h._EffectDOT()", "Removing" + ToString(GetArraySize(effects)));
#endif
          RemoveEffectArray(oCreature, effects);

          // -------------------------------------------------------------------------
          // Handle non DOT types
          // -------------------------------------------------------------------------    
          List<xEffect> effects2 = GetEffects(oCreature, EngineConstants.EFFECT_TYPE_PETRIFY);
          int nSize = GetArraySize(effects2);

          if (nSize != EngineConstants.FALSE)
          {
               int i;
               int nId = 0;
               for (i = 0; i < nSize; i++)
               {
                xEffect _effect2 = effects2[i];
                    nId = GetEffectAbilityIDRef(ref _effect2);
                    if (nId == EngineConstants.ABILITY_SPELL_CONE_OF_COLD || nId == EngineConstants.ABILITY_SPELL_WINTERS_GRASP || nId == EngineConstants.MONSTER_PRIDE_DEMON_FROST_BLAST || nId == EngineConstants.MONSTER_PRIDE_DEMON_FROST_BOLT) /*note: blizzard handles this internally, do not interfere*/
                    {
                         RemoveEffect(oCreature, effects2[i]);
                    }
               }
          }


     }

     public float _EffectDotGetDamagePerTick(float fTotalDamage, float fDuration)
     {
          int nTicks = FloatToInt(fDuration / EngineConstants.CREATURE_RULES_TICK_DELAY) + 1 /*there's an initial tick at the start*/;

          return (fTotalDamage / IntToFloat(nTicks));
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  EffectDOT
     ///////////////////////////////////////////////////////////////////////////////

     public xEffect _EffectDOT(float fTotalDamage, float fDuration, int nVfx = 0, int nDamageType = EngineConstants.DAMAGE_TYPE_FIRE, int nImpactVfx = 0)
     {

          if (fDuration < 1.0f)
          {
               Warning("_EffectDOT created with zero duration. This is a problem. Please inform georg. Script: " + GetCurrentScriptName());
               fDuration = 1.0f;
          }

          if (nVfx == 0)
          {
               switch (nDamageType)
               {
                    case EngineConstants.DAMAGE_TYPE_FIRE: nVfx = 10; break;
                    case EngineConstants.DAMAGE_TYPE_ELECTRICITY: nVfx = 1005; break;
                    case EngineConstants.DAMAGE_TYPE_COLD: nVfx = 1008; break;
               }
          }

          float fDamagePerTick = _EffectDotGetDamagePerTick(fTotalDamage, fDuration);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_dot2_h._EffectDOT()", "Dot Ctor:  Damage" + ToString(fTotalDamage) + " over " + ToString(fDuration) + ": " + ToString(fDamagePerTick));
#endif
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_DOT);
        SetEffectFloatRef(ref eEffect, 0, fDamagePerTick);

          // -------------------------------------------------------------------------
          // Sorry, crusts will not play on lowest graphics detail level
          // -------------------------------------------------------------------------
          if (GetGraphicsDetailLevel() > 0)
          {
               SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_VFX, nVfx);
          }
        SetEffectIntegerRef(ref eEffect, 1, nDamageType);
          SetEffectIntegerRef(ref eEffect, 2, nImpactVfx);

          return eEffect;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleApplyEffectDOT
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleApplyEffectDOT(xEffect eEffect)
     {
          int nDamageType = GetEffectIntegerRef(ref eEffect, 1);

          GameObject oCreator = GetEffectCreatorRef(ref eEffect);

          int bFF = IsFriendlyFireParty(oCreator, gameObject);

          if (bFF != EngineConstants.FALSE)
          {

               if ((GetGameDifficulty() == EngineConstants.GAME_DIFFICULTY_CASUAL) && (nDamageType != EngineConstants.DAMAGE_TYPE_PLOT))
               {
                    // Still get benefits from cancelling out effects.

                    nDamageType = GetEffectIntegerRef(ref eEffect, 1);
                    if (nDamageType == EngineConstants.DAMAGE_TYPE_FIRE)
                    {
                         RemoveColdBasedEffects(gameObject);
                    }
                    else if (nDamageType == EngineConstants.DAMAGE_TYPE_COLD)
                    {
                         RemoveFireBasedEffects(gameObject);
                    }

                    return EngineConstants.FALSE;
               }

          }

          if (GetEffectFloatRef(ref eEffect, 0) >= 1.0f)
          {
               // we only support creature dots
               if (GetObjectType(gameObject) == EngineConstants.OBJECT_TYPE_CREATURE)
               {

                    if (GetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_DOT) == EngineConstants.FALSE)
                    {
                         SetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_DOT);
                         DelayEvent(0.0f, gameObject, Event(EngineConstants.EVENT_TYPE_DOT_TICK)); /*first xEvent is just postponed to the next frame*/
                    }
                    else
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_dot2_h", "Dot Heartbeat desynchronized with flag?");

                         /*
                        List<xEffect> dots = GetEffects(gameObject, EngineConstants.EFFECT_TYPE_DOT);
                        if (GetArraySize(dots)>1)
                        {
                            #if DEBUG

                            #endif
                            DelayEvent(0.0f,  gameObject,  Event(  EngineConstants.EVENT_TYPE_DOT_TICK));
                        }
                    */

                    }
               }
               else
               {
#if DEBUG
                    Warning("DOT Effect applied to non creature. Talk To georg!!! Source:" + GetCurrentScriptName() + " on " + ToString(gameObject));
#endif
                    return EngineConstants.FALSE;
               }
          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_dot2_h", "DoT cancelled as individual tick damage is below 1.0f threshold");
#endif
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // Dot Effects cancel each other out...
          // -------------------------------------------------------------------------
          if (nDamageType == EngineConstants.DAMAGE_TYPE_FIRE)
          {
               RemoveColdBasedEffects(gameObject);
          }
          else if (nDamageType == EngineConstants.DAMAGE_TYPE_COLD)
          {
               RemoveFireBasedEffects(gameObject);
          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "Effects_HandleApplyEffectDOT", "....");
#endif
          }

          return EngineConstants.TRUE;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleRemoveEffectDOT
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleRemoveEffectDOT(xEffect eEffect)
     {
          List<xEffect> effects = GetEffects(gameObject, EngineConstants.EFFECT_TYPE_DOT);
          int nSize = GetArraySize(effects);
          int i;

          if (nSize == 0)
          {
               // ---------------------------------------------
               // Sync the flag to false if no DOTs are active
               // ---------------------------------------------
               SetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_DOT, EngineConstants.FALSE);

               // ---------------------------------------------
               // We're not scheduling more ticks, so the HB dies here
               // ---------------------------------------------

          }

          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     // @brief Runs the DOT (causes damage)
     //
     // This is called from the DOT Event handler on the creatures and calculates
     // and applies the damage for the passed in eEffect
     //
     // @param eEffect Effect of type EngineConstants.EFFECT_TYPE_DOT
     //
     // @author Georg Zoeller
     // -----------------------------------------------------------------------------
     public void Effects_HandleDotEffectTick(xEffect eEffect)
     {

          // -------------------------------------------------------------------------
          // We don't ever tick these effects if we are not in an active game mode
          // e.g. dialog / conversation.
          // -------------------------------------------------------------------------
          int nGameMode = GetGameMode();
          if (nGameMode == EngineConstants.GM_EXPLORE || nGameMode == EngineConstants.GM_COMBAT)
          {
               float fDamage = GetEffectFloatRef(ref eEffect, 0);
               GameObject oCreator = GetEffectCreatorRef(ref eEffect);
               int nDamageType = GetEffectIntegerRef(ref eEffect, 1);
               int nImpactVfx = GetEffectIntegerRef(ref eEffect, 2);

               if (nDamageType == EngineConstants.DAMAGE_TYPE_FIRE)
               {
                    RemoveColdBasedEffects(gameObject);
               }
               else if (nDamageType == EngineConstants.DAMAGE_TYPE_COLD)
               {
                    RemoveFireBasedEffects(gameObject);
               }

               int nAbilityId = GetEffectAbilityIDRef(ref eEffect);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_dot2_h.HandleTick()", "Dot Ticking for: " + ToString(fDamage) + " id: " + ToString(nAbilityId) + " ImpVfx:" + ToString(nImpactVfx));
#endif

               if (DamageIsImmuneToType(gameObject, nDamageType) == EngineConstants.FALSE)
               {
                    if ((GetHasEffects(gameObject, EngineConstants.EFFECT_TYPE_DAMAGE_WARD) != EngineConstants.FALSE) && (nDamageType != EngineConstants.DAMAGE_TYPE_PLOT))
                    {
                         // -------------------------------------------------------
                         // Only message immunity if a PC is involved
                         // -------------------------------------------------------
                         if (IsPartyMember(gameObject) != EngineConstants.FALSE || IsPartyMember(oCreator) != EngineConstants.FALSE)
                         {
                              UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_NO_EFFECT, "", GetColorByDamageType(nDamageType));
                         }
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_dot2_h", ToString(gameObject) + " DAMAGE ZEROED because of EngineConstants.DAMAGE_WARD_EFFECT");
#endif
                    }
                    else
                    {

                         DEBUG_PrintToScreen("dt:" + ToString(nDamageType), 6);
                         Effects_ApplyInstantEffectDamage(gameObject, oCreator, fDamage, nDamageType, EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE | EngineConstants.DAMAGE_EFFECT_FLAG_FROM_DOT, nAbilityId, nImpactVfx);
                    }
               }
               else
               {
                    // -------------------------------------------------------
                    // Only message immunity if a PC is involved
                    // -------------------------------------------------------
                    if (IsPartyMember(gameObject) != EngineConstants.FALSE || IsPartyMember(oCreator) != EngineConstants.FALSE)
                    {
                         UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_IMMUNE);
                    }
               }

               // -------------------------------------------------------------------------
               // All Dot's kill stealth.
               // -------------------------------------------------------------------------
               DropStealth(gameObject);
          }
     }

     // -----------------------------------------------------------------------------
     // @brief DOT Event handler code called from creature_core and player_core
     //
     // This manages applying damage over time from all DOT effects.
     //
     // @param oCreature Creature that wants it's DOT effects processed
     //
     // @author Georg Zoeller
     // -----------------------------------------------------------------------------
     public void Effects_HandleCreatureDotTickEvent(GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          if (GetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_DOT) != EngineConstants.FALSE)
          {

               List<xEffect> effects = GetEffects(oCreature, EngineConstants.EFFECT_TYPE_DOT);
               int nSize = GetArraySize(effects);
               int i;

               if (nSize == 0)
               {
                    // ---------------------------------------------
                    // Sync the flag to false if no DOTs are active
                    // ---------------------------------------------
                    SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_DOT, EngineConstants.FALSE);

                    // ---------------------------------------------
                    // We're not scheduling more ticks, so the HB dies here
                    // ---------------------------------------------

               }
               else
               {
                    for (i = 0; i < nSize; i++)
                    {
                         Effects_HandleDotEffectTick(effects[i]);
                    }

                    // ---------------------------------------------
                    // Continue to tick...
                    // ---------------------------------------------
                    DelayEvent(EngineConstants.CREATURE_RULES_TICK_DELAY, oCreature, Event(EngineConstants.EVENT_TYPE_DOT_TICK));
               }
          }
     }

     // -----------------------------------------------------------------------------
     // Apply an affect DamageOverTime. Does factor in damage resistances.
     // -----------------------------------------------------------------------------
     public void ApplyEffectDamageOverTime(GameObject oTarget, GameObject oCaster, int nAbility, float fTotalDamage, float fDuration, int nDamageType, int nCrustVfx = 0, int nImpactVfx = 0)
     {
          float fDamage = GetModifiedDamage(oCaster, nDamageType, fTotalDamage);

          fDamage = ResistDamage(oCaster, oTarget, nAbility, fTotalDamage, nDamageType);

          xEffect eDot = _EffectDOT(fDamage, fDuration, nCrustVfx, nDamageType, nImpactVfx);

          if (nDamageType == EngineConstants.DAMAGE_TYPE_FIRE)
          {
               int nAppearance = GetAppearanceType(oTarget);
               if (nAppearance == 65) // wild sylvan
               {
                SetEffectEngineIntegerRef(ref eDot, EngineConstants.EFFECT_INTEGER_VFX, 93092);
               }
          }

          //fduration is lengthened to allow the last tick to go through
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eDot, oTarget, fDuration + (EngineConstants.CREATURE_RULES_TICK_DELAY / 2.0f), oCaster, nAbility);
     }
}