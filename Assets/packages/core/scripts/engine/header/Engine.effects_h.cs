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
     // Effect Includes
     // -----------------------------------------------------------------------------
     /*
         This is the top level include for the effects system. All other files related
         to this system can be found under \_Game Effects\.

         Include Hierarchy:

         Effects_h includes \Game_Effects\effect_*_h includes "Effects_constants_h"
             includes core_h

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"effect_constants_h"
     //#include"events_h"
     ////#include"wrappers_h"
     //#include"2da_constants_h"
     //#include"core_h"
     //#include"effect_charm_h"
     //#include"ai_threat_h"

     // -----------------------------------------------------------------------------
     // Effect Includes
     // -----------------------------------------------------------------------------
     //#include"effect_damage_h"
     //#include"effect_death_h"
     ////#include"effect_armorpenetration_h"
     //#include"effect_resurrection_h"
     //#include"effect_heal_h"
     ////#include"effect_damageresistance_h"
     //#include"effect_modify_mana_stam_h"
     //#include"effect_knockdown_h"
     //#include"effect_modify_attribute_h"
     //#include"effect_disease_h"
     //#include"effect_upkeep_h"
     //#include"effect_dot2_h"
     //#include"effect_daze_h"
     //#include"effect_paralyze_h"
     //#include"effect_dispel_magic_h"
     //#include"effect_modify_critchance_h"
     //#include"effect_modify_property_h"
     //#include"effect_root_h"
     //#include"effect_ai_modifier_h"
     //#include"effect_visualeffect_h"
     //#include"effect_impact_h"
     //#include"effect_sleep_h"

     //#include"effect_screenshake_h"
     //#include"effect_regeneration_h"
     //#include"effect_stun_h"
     //#include"effect_conecasting_h"

     //#include"effect_addability"
     //#include"effect_stealth_h"
     //#include"effect_test_h"
     //#include"effect_heartbeat_h"
     //#include"effect_rec_knockdown_h"
     //#include"effect_lyrium_h"
     //#include"effect_wbomb_h"
     //#include"effect_summon_h"
     //#include"effect_polymorph"
     //#include"effect_feign_death_h"

     //#include"effect_enchantment_h"
     //#include"effect_confusion_h"
     //#include"effect_swarm_h"
     //#include"effect_mabari_dominance_h"

     public xEffect EffectModifyMovementSpeed(float fPotency, int bHostile = EngineConstants.FALSE)
     {
          xEffect eSlow;
          if (bHostile != EngineConstants.FALSE)
          {
               eSlow = Effect(EngineConstants.EFFECT_TYPE_MOVEMENT_RATE_DEBUFF);
          }
          else
          {
               eSlow = Effect(EngineConstants.EFFECT_TYPE_MOVEMENT_RATE);
          }
          SetEffectEngineFloatRef(ref eSlow, EngineConstants.EFFECT_FLOAT_POTENCY, fPotency);
          return eSlow;
     }

     public xEffect EffectLifeWard(float fHealth)
     {
          xEffect eRet = Effect(EngineConstants.EFFECT_TYPE_LIFE_WARD);
          SetEffectFloatRef(ref eRet, 0, fHealth);
          return eRet;
     }

     public int Effects_HandleRemoveEffectLifeWard(xEffect eEffect)
     {

          if (IsDeadOrDying(gameObject) == EngineConstants.FALSE)
          {
               float fHealth = GetEffectFloatRef(ref eEffect, 0);
               HealCreature(gameObject, EngineConstants.TRUE, fHealth);
          }

          return EngineConstants.TRUE;
     }

     public int IsEffectTypeHostile(int nEffectType)
     {
          return (GetM2DAInt(EngineConstants.TABLE_EFFECTS, "bConsiderHostile", nEffectType) != 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     // <Debug>
     // -----------------------------------------------------------------------------
     // This check is for preventing designers from applying an xEffect with a wrong
     // duration type.
     // -----------------------------------------------------------------------------
     public int _VerifyDurationType(xEffect eEffect, int nEffectType)
     {

          string sCol = String.Empty;

          // -------------------------------------------------------------------------
          // Get the current duration type on the effect
          // -------------------------------------------------------------------------
          int nDuration = GetEffectDurationTypeRef(ref eEffect);

          // -------------------------------------------------------------------------
          // Determine which column in effects.xls we need to check to verify it
          // -------------------------------------------------------------------------
          switch (nDuration)
          {
               case EngineConstants.EFFECT_DURATION_TYPE_PERMANENT: sCol = "AllowPermanent"; break;
               case EngineConstants.EFFECT_DURATION_TYPE_INSTANT: sCol = "AllowInstant"; break;
               case EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY: sCol = "AllowTemporary"; break;
          }

          // -------------------------------------------------------------------------
          // Check what the 2da says about the effect
          // -------------------------------------------------------------------------
          int nRet = GetM2DAInt(EngineConstants.TABLE_EFFECTS, sCol, nEffectType);

          // -------------------------------------------------------------------------
          // If the 2da column value for the xEffect is not 1, we're in trouble
          // -------------------------------------------------------------------------
          if (nRet != 1)
          {
#if DEBUG
               string sEffect = GetM2DAString(EngineConstants.TABLE_EFFECTS, "Label", nEffectType);
               Log_Systems("CRITICAL: - Effect " + sEffect + " applied with invalid (effects.xls) duration type " + IntToString(nDuration), EngineConstants.LOG_LEVEL_CRITICAL);
#endif
          }

          return (nRet == 1 ? EngineConstants.TRUE : EngineConstants.FALSE);

     }
     // </Debug>

     // -----------------------------------------------------------------------------
     // This handles rules_core.EVENT_TYPE_APPLY_EFFECT.
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffect(xEvent ev)
     {
          xEffect eEffect = GetCurrentEffect();
          int nEffectType = GetEffectTypeRef(ref eEffect);
          int nReturnValue = -1;
          GameObject oCreator = GetEffectCreatorRef(ref eEffect);
          int nAbilityId = GetEffectAbilityIDRef(ref eEffect);

          // <Debug>
          // -------------------------------------------------------------------------
          // If logging is enabled, we also verify that the xEffect we are trying to
          // apply here is applied with an allowed duration type. if not, fail it.
          // -------------------------------------------------------------------------
          /*if (EngineConstants.LOG_ENABLED==EngineConstants.TRUE)
          {
              _VerifyDurationType(eEffect, nEffectType);
              SetIsCurrentEffectValid(EngineConstants.FALSE);
              return EngineConstants.FALSE;
          }*/
          //

          string sTypeName = Log_GetEffectNameById(nEffectType);

#if DEBUG
          Log_Trace_Effects("effects_h.HandleApplyEffect", eEffect, "", gameObject);
#endif

          //</Debug>

          // -------------------------------------------------------------------------
          // Dead creatures no longer accept any other xEffect except for resurrection || death!
          // -------------------------------------------------------------------------
          if (IsDead(gameObject) != EngineConstants.FALSE && (nEffectType != EngineConstants.EFFECT_TYPE_RESURRECTION && nEffectType != EngineConstants.EFFECT_TYPE_DEATH))
          {
#if DEBUG
               Log_Trace_Effects("effects_h.HandleApplyEffect", eEffect, "EngineConstants.EFFECT_REJECTED - target is dead", gameObject);
#endif

               nReturnValue = EngineConstants.FALSE;
          }
          else
          {
               if (GetObjectType(gameObject) == EngineConstants.OBJECT_TYPE_CREATURE)
               {

                    int nMessage = EngineConstants.UI_MESSAGE_IMMUNE;

                    // -----------------------------------------------------------------
                    //  Knockdown and stun end grab
                    // -----------------------------------------------------------------
                    if (nEffectType == EngineConstants.EFFECT_TYPE_KNOCKDOWN || nEffectType == EngineConstants.EFFECT_TYPE_STUN || nEffectType == EngineConstants.EFFECT_TYPE_PARALYZE || nEffectType == EngineConstants.EFFECT_TYPE_SLIP)
                    {

                         List<xEffect> aGrab = GetEffects(gameObject, EngineConstants.EFFECT_TYPE_GRABBING, 0, gameObject);
                         if (RemoveEffectArray(gameObject, aGrab) > 0)
                         {
#if DEBUG
                              Log_Trace_Effects("effects_h.HandleApply", eEffect, "any grabbing xEffect removed by other effect!", gameObject);
#endif
                              nMessage = EngineConstants.UI_MESSAGE_GRAB_BROKEN;
                         }
                         else
                         {
#if DEBUG
                              Log_Trace_Effects("effects_h.HandleApply", eEffect, "conecasting xEffect removed by stun!", gameObject);
#endif
                              List<xEffect> aCast = GetEffects(gameObject, EngineConstants.EFFECT_TYPE_CONECASTING, 0, gameObject);
                              RemoveEffectArray(gameObject, aCast);
                         }

                    }
                    if (nEffectType == EngineConstants.EFFECT_TYPE_GRABBED || nEffectType == EngineConstants.EFFECT_TYPE_PARALYZE)
                    {
#if DEBUG
                         Log_Trace_Effects("effects_h.HandleApply", eEffect, "conecasting xEffect removed by grab or paralyze!", gameObject);
#endif
                         List<xEffect> aCast = GetEffects(gameObject, EngineConstants.EFFECT_TYPE_CONECASTING, 0, gameObject);
                         RemoveEffectArray(gameObject, aCast);
                    }

                    // -----------------------------------------------------------------
                    // Implementation for spell ward.
                    // -----------------------------------------------------------------
                    if (GetAbilityType(nAbilityId) == EngineConstants.ABILITY_TYPE_SPELL)
                    {
                         if (GetHasEffects(gameObject, EngineConstants.EFFECT_TYPE_SPELL_WARD) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_Effects("effects_h.HandleApply", eEffect, "spell xEffect not applied, creature has EngineConstants.EFFECT_SPELL_WARD!", gameObject);
#endif
                              ApplyEffectVisualEffect(oCreator, gameObject, 1556, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f, nAbilityId);
                              UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_SPELL_IMMUNITY);
                              nReturnValue = EngineConstants.FALSE;
                         }
                    }

                    if (nReturnValue == -1)
                    {
                         int nImmune = IsImmuneToEffectType(gameObject, nEffectType);
                         if (nImmune > 0)
                         {
#if DEBUG
                              Log_Trace_Effects("effects_h.HandleApply", eEffect, "effect not applied, creature immune!", gameObject);
#endif
                              if (nImmune == 1)
                              {
                                   UI_DisplayMessage(gameObject, nMessage);
                              }
                              else if (nImmune == 3)
                              {
                                   UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_RESISTED);
                              }

                              nReturnValue = EngineConstants.FALSE;
                         }
                    }
               }

               // ---------------------------------------------------------------------
               // Georg: Invalidate hostile effects per target
               // ---------------------------------------------------------------------
               if (nReturnValue != EngineConstants.FALSE)
               {
                    if (IsEffectTypeHostile(nEffectType) != EngineConstants.FALSE)
                    {
                         if (IsHostileEffectAllowed(gameObject, oCreator, nAbilityId) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace_Effects("effects_h.HandleApply", eEffect, "hostile xEffect NOT applied - not allowed on target", gameObject);
#endif
                              nReturnValue = EngineConstants.FALSE;
                         }
                    }

               }

               if (GetM2DAInt(EngineConstants.TABLE_EFFECTS, "SimpleEffect", nEffectType) != EngineConstants.FALSE)
               {
                    nReturnValue = EngineConstants.TRUE;
               }

               if (nReturnValue == -1)
               {

                    switch (nEffectType)
                    {
                         case EngineConstants.EFFECT_TYPE_NULL:
                         case EngineConstants.EFFECT_TYPE_HEAVY_IMPACT:
                         case EngineConstants.EFFECT_TYPE_LIFE_WARD:
                         case EngineConstants.EFFECT_TYPE_PETRIFY:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_DAMAGE:
                              {
                                   nReturnValue = Effects_HandleApplyEffectDamage(eEffect);
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_DEATH:
                              {
                                   nReturnValue = Effects_HandleApplyEffectDeath(eEffect);
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_RESURRECTION:
                              {
                                   nReturnValue = Effects_HandleApplyEffectResurrection(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_MODIFYMANASTAMINA:
                              {
                                   nReturnValue = Effects_HandleApplyEffectModifyManaStamina(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_HEALHEALTH:
                              {
                                   nReturnValue = Effects_HandleApplyEffectHeal(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_ROOT:
                              {
                                   nReturnValue = Effects_HandleApplyEffectRoot(eEffect);
                                   AI_Threat_ClearEnemiesThreatToMe(gameObject);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_KNOCKDOWN:
                              {
                                   nReturnValue = Effects_HandleApplyEffectKnockdown(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_MODIFYATTRIBUTE:
                              {
                                   nReturnValue = Effects_HandleApplyEffectModifyAttribute(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_UPKEEP:
                              {
                                   nReturnValue = Effects_HandleApplyEffectUpkeep(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_DOT:
                              {
                                   nReturnValue = Effects_HandleApplyEffectDOT(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_DAZE:
                              {
                                   nReturnValue = Effects_HandleApplyEffectDaze(eEffect);
                                   break;

                              }
                         case EngineConstants.EFFECT_TYPE_DISEASE:
                              {
                                   nReturnValue = Effects_HandleApplyEffectDisease(eEffect);
                                   break;

                              }
                         case EngineConstants.EFFECT_TYPE_DISPEL_MAGIC:
                              {
                                   nReturnValue = Effects_HandleApplyEffectDispelMagic(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_MODIFY_CRITCHANCE:
                              {
                                   nReturnValue = Effects_HandleApplyEffectModifyCritChance(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_DECREASE_PROPERTY:
                         case EngineConstants.EFFECT_TYPE_MODIFY_PROPERTY:
                              {
                                   nReturnValue = Effects_HandleApplyEffectModifyProperty(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_AI_MODIFIER:
                              {
                                   nReturnValue = Effects_HandleApplyEffectAIModifier(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_ADDABILITY:
                              {
                                   nReturnValue = Effects_HandleApplyEffectAddAbility(eEffect);
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_CONECASTING:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_ROOTING:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_GRABBING:
                         case EngineConstants.EFFECT_TYPE_GRABBED:
                         case EngineConstants.EFFECT_TYPE_OVERWHELMED:
                         case EngineConstants.EFFECT_TYPE_OVERWHELMING: /*intentional fallthrough*/
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_REGENERATION:
                              {
                                   nReturnValue = Effects_HandleApplyEffectRegeneration(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_STUN:
                              {
                                   nReturnValue = Effects_HandleApplyEffectStun(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_CONFUSION:
                              {
                                   nReturnValue = Effects_HandleApplyEffectConfusion(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_CHARM:
                              {
                                   nReturnValue = Effects_HandleApplyEffectCharm(eEffect);
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_SLEEP:
                         case EngineConstants.EFFECT_TYPE_SLEEP_PLOT:
                              {
                                   nReturnValue = Effects_HandleApplyEffectSleep(eEffect);
                                   AI_Threat_ClearEnemiesThreatToMe(gameObject);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_STEALTH:
                              {
                                   nReturnValue = Effects_HandleApplyEffectStealth(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_TEST: // 11/15/07
                              {
                                   nReturnValue = Effects_HandleApplyEffectTest(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_HEARTBEAT:
                              {
                                   nReturnValue = Effects_HandleApplyEffectHeartbeat(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_RECURRING_KNOCKDOWN: // 05/12/07
                              {
                                   nReturnValue = Effects_HandleApplyEffectRecurringKnockdown(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_WALKING_BOMB:
                              {
                                   nReturnValue = Effects_HandleApplyEffectWalkingBomb(eEffect);
                                   AI_Threat_ClearEnemiesThreatToMe(gameObject);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_SUMMON:
                              {
                                   nReturnValue = Effects_HandleApplyEffectSummon(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_SHAPECHANGE:
                              {
                                   nReturnValue = Effects_HandleApplyEffectShapechange(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_ENCHANTMENT:
                              {
                                   nReturnValue = Effects_HandleApplyEffectEnchantment(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_LOCK_INVENTORY:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_LOCK_QUICKBAR:
                              {
                                   nReturnValue = EngineConstants.TRUE;

                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_LOCK_CHARACTER:
                              {
                                   nReturnValue = EngineConstants.TRUE;

                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_FEIGN_DEATH:
                              {
                                   nReturnValue = Effects_HandleApplyEffectFeignDeath(eEffect);
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_SIMULATE_DEATH:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_FLANK_IMMUNITY:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_FEAR:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_MISDIRECTION_HEX:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_DEATH_HEX:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }

                         case EngineConstants.EFFECT_TYPE_CURSE_OF_MORTALITY:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_SLIP:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_SPELL_WARD:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_DAMAGE_WARD:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   AI_Threat_ClearEnemiesThreatToMe(gameObject);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_WYNNE_REMOVAL:
                              {
                                   nReturnValue = EngineConstants.TRUE;
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_SWARM:
                              {
                                   nReturnValue = Effects_HandleApplyEffectSwarm(eEffect);
                                   break;
                              }
                         case EngineConstants.EFFECT_TYPE_MABARI_DOMINANCE:
                              {
                                   nReturnValue = Effects_HandleApplyEffectMabariDominance(eEffect);
                                   break;
                              }

                              /*switch*/

                    } /* if nReturnValue == -1 */
               }
          } /*if*/

          // -------------------------------------------------------------------------
          // Notify if we forgot to handle an effect
          // -------------------------------------------------------------------------
          if (nReturnValue == -1)
          {
#if DEBUG
               Warning("EngineConstants.EFFECT_NOT_HANDLED! " + Log_GetEffectNameById(nEffectType) + ".  + Contact Georg");
               Log_Trace_Scripting_Error("effects_h.HandleApplyEffect", "EngineConstants.EFFECT_NOT_HANDLED! " + Log_GetEffectNameById(nEffectType), gameObject);
#endif
               nReturnValue = EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // This notifies the engine on whether or not to go ahead with applying the
          // effect. If this is false, the engine will discard the effect, firing a
          // RemoveEffect xEvent to rules_core, which will message it to Effects_HandleRemoveEffect
          // below
          // -------------------------------------------------------------------------
          if (nReturnValue == 0)
          {
#if DEBUG
               Log_Trace_Effects("effects_h.HandleApply", eEffect, "effect found not valid (EngineConstants.FALSE reported back)", gameObject);
#endif
          }

          if (nReturnValue != EngineConstants.FALSE)
          {
               // ---------------------------------------------------------------------
               // Certain effects kill stealth.
               // ---------------------------------------------------------------------
               if (GetM2DAInt(EngineConstants.TABLE_EFFECTS, "bDropStealth", nEffectType) != EngineConstants.FALSE)
               {
                    SignalEventDropStealth(gameObject);
               }

          }

          SetIsCurrentEffectValid(ev, nReturnValue);
          return nReturnValue;
     }

     // -----------------------------------------------------------------------------
     // This handles rules_core.EngineConstants.EVENT_TYPE_REMOVE_EFFECT.
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffect()
     {
          xEffect eEffect = GetCurrentEffect();
          int nEffectType = GetEffectTypeRef(ref eEffect);
          int nReturnValue = EngineConstants.FALSE;

#if DEBUG
          Log_Trace_Effects("effects_h.HandleRemoveEffect", eEffect, "", gameObject);
#endif

          if (GetM2DAInt(EngineConstants.TABLE_EFFECTS, "SimpleEffect", nEffectType) != EngineConstants.FALSE)
          {
               nReturnValue = EngineConstants.TRUE;
          }

          if (nReturnValue == EngineConstants.FALSE)
          {
               switch (nEffectType)
               {

                    case EngineConstants.EFFECT_TYPE_NULL:
                         {
                              nReturnValue = 1;
                              break;
                         }

                    case EngineConstants.EFFECT_TYPE_PETRIFY:
                    case EngineConstants.EFFECT_TYPE_HEAVY_IMPACT:
                         {
                              nReturnValue = 1;        /*anim only effect, no need for handler*/
                              break;
                         }

                    case EngineConstants.EFFECT_TYPE_RESURRECTION:
                         {
                              nReturnValue = Effects_HandleRemoveEffectResurrection(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_DEATH:
                         {
                              nReturnValue = Effects_HandleRemoveEffectDeath(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_MODIFYMANASTAMINA:
                         {
                              nReturnValue = Effects_HandleRemoveEffectModifyManaStamina(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_HEALHEALTH:
                         {
                              nReturnValue = Effects_HandleRemoveEffectHeal(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_ROOT:
                         {
                              nReturnValue = Effects_HandleRemoveEffectRoot(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_KNOCKDOWN:
                         {
                              nReturnValue = Effects_HandleRemoveEffectKnockdown(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_MODIFYATTRIBUTE:
                         {
                              nReturnValue = Effects_HandleRemoveEffectModifyAttribute(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_UPKEEP:
                         {
                              nReturnValue = Effects_HandleRemoveEffectUpkeep(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_CHARM:
                         {
                              nReturnValue = Effects_HandleRemoveEffectCharm(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_CONFUSION:
                         {
                              nReturnValue = Effects_HandleRemoveEffectConfusion(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_DOT:
                         {
                              nReturnValue = Effects_HandleRemoveEffectDOT(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_DAZE:
                         {
                              nReturnValue = Effects_HandleRemoveEffectDaze(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_DISEASE:
                         {
                              nReturnValue = Effects_HandleRemoveEffectDisease(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_DISPEL_MAGIC:
                         {
                              nReturnValue = Effects_HandleRemoveEffectDispelMagic(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_MODIFY_CRITCHANCE:
                         {
                              nReturnValue = Effects_HandleRemoveEffectModifyCritChance(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_DECREASE_PROPERTY:
                    case EngineConstants.EFFECT_TYPE_MODIFY_PROPERTY:
                         {
                              nReturnValue = Effects_HandleRemoveEffectModifyProperty(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_LIFE_WARD:
                         {
                              nReturnValue = Effects_HandleRemoveEffectLifeWard(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_AI_MODIFIER:
                         {
                              nReturnValue = Effects_HandleRemoveEffectAIModifier(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_ADDABILITY:
                         {
                              nReturnValue = Effects_HandleRemoveEffectAddAbility(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_ROOTING:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_GRABBING:
                    case EngineConstants.EFFECT_TYPE_OVERWHELMING: /*intentional fallthrough*/
                         {
                              // Get target that was being grabbed
                              GameObject oCreator = GetEffectCreatorRef(ref eEffect);
                              GameObject oTarget = GetEffectObjectRef(ref eEffect, 1);
                              int nAbilityId = GetEffectAbilityIDRef(ref eEffect);

                              // Remove all grabbing xEffect (there due this ogre grab)
                              List<xEffect> aEffects = GetEffects(oTarget, nEffectType - 1, GetEffectAbilityIDRef(ref eEffect), gameObject);
                              RemoveEffectArray(oTarget, aEffects);
                              Ability_SetCooldown(oCreator, nAbilityId);

                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_GRABBED:
                    case EngineConstants.EFFECT_TYPE_OVERWHELMED:  /*intentional fallthrough*/
                         {
                              // -----------------------------------------------------------------
                              // break grab if target dies
                              // -----------------------------------------------------------------
                              // Get target that was being grabbed
                              GameObject oCreator = GetEffectCreatorRef(ref eEffect);
                              int nAbilityId = GetEffectAbilityIDRef(ref eEffect);

                              // Remove all grabbing xEffect (there due this ogre grab)
                              List<xEffect> aEffects = GetEffects(oCreator, nEffectType + 1, nAbilityId, oCreator);
                              RemoveEffectArray(oCreator, aEffects);
                              nReturnValue = EngineConstants.TRUE;

                              break;
                         }

                    case EngineConstants.EFFECT_TYPE_CONECASTING:
                         {

                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }

                    case EngineConstants.EFFECT_TYPE_REGENERATION:
                         {
                              nReturnValue = Effects_HandleRemoveEffectRegeneration(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_STUN:
                         {
                              nReturnValue = Effects_HandleRemoveEffectStun(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_SLEEP:
                    case EngineConstants.EFFECT_TYPE_SLEEP_PLOT:
                         {
                              nReturnValue = Effects_HandleRemoveEffectSleep(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_STEALTH:
                         {
                              nReturnValue = Effects_HandleRemoveEffectStealth(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_TEST: // 15/11/07
                         {
                              nReturnValue = Effects_HandleRemoveEffectTest(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_HEARTBEAT:
                         {
                              nReturnValue = Effects_HandleRemoveEffectHeartbeat(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_RECURRING_KNOCKDOWN: // 05/12/07
                         {
                              nReturnValue = Effects_HandleRemoveEffectRecurringKnockdown(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_WALKING_BOMB:
                         {
                              nReturnValue = Effects_HandleRemoveEffectWalkingBomb(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_SUMMON:
                         {
                              nReturnValue = Effects_HandleRemoveEffectSummon(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_SHAPECHANGE:
                         {
                              nReturnValue = Effects_HandleRemoveEffectShapechange(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_ENCHANTMENT:
                         {
                              nReturnValue = Effects_HandleRemoveEffectEnchantment(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_LOCK_INVENTORY:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_LOCK_QUICKBAR:
                         {
                              nReturnValue = EngineConstants.TRUE;

                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_LOCK_CHARACTER:
                         {
                              nReturnValue = EngineConstants.TRUE;

                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_FEIGN_DEATH:
                         {
                              nReturnValue = Effects_HandleRemoveEffectFeignDeath(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_SIMULATE_DEATH:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_FLANK_IMMUNITY:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_FEAR:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_MISDIRECTION_HEX:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_DEATH_HEX:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_CURSE_OF_MORTALITY:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_SLIP:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_SPELL_WARD:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_DAMAGE_WARD:
                         {
                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_WYNNE_REMOVAL:
                         {
                              int nAbility = GetEffectAbilityIDRef(ref eEffect);
                              if (IsModalAbilityActive(gameObject, nAbility) != EngineConstants.FALSE)
                              {
                                   Effects_RemoveUpkeepEffect(gameObject, nAbility);

                                   // add weakness effect
                                   eEffect = EffectModifyProperty(EngineConstants.PROPERTY_ATTRIBUTE_ATTACK, EngineConstants.WYNNE_ATTACK_PENALTY,
                                        EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE, EngineConstants.WYNNE_DEFENSE_PENALTY);
                                   SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_VFX, EngineConstants.WYNNE_WEAKNESS_VFX);
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eEffect, gameObject, EngineConstants.WYNNE_WEAKNESS_DURATION, gameObject, nAbility);
                                   eEffect = EffectModifyMovementSpeed(EngineConstants.WYNNE_SPEED_PENALTY);
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eEffect, gameObject, EngineConstants.WYNNE_WEAKNESS_DURATION, gameObject, nAbility);

                                   // if trinket not present
                                   if (IsObjectValid(GetItemPossessedBy(gameObject, "gen_im_acc_amu_am11")) == EngineConstants.FALSE)
                                   {
                                        eEffect = EffectStun();
                                        ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eEffect, gameObject, EngineConstants.WYNNE_STUN_DURATION, gameObject, nAbility);
                                   }
                              }

                              nReturnValue = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_SWARM:
                         {
                              nReturnValue = Effects_HandleRemoveEffectSwarm(eEffect);
                              break;
                         }
                    case EngineConstants.EFFECT_TYPE_MABARI_DOMINANCE:
                         {
                              nReturnValue = Effects_HandleRemoveEffectMabariDominance(eEffect);
                              break;
                         }

               }
          }
          return nReturnValue;
     }

     public xEffect EffectEnchantment(int nType = 3000, int nPower = 1)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_ENCHANTMENT);
          SetEffectIntegerRef(ref eEffect, 0, nType);
          SetEffectIntegerRef(ref eEffect, 1, nPower);
          return eEffect;
     }

     public void MakeCreatureGhost(GameObject oCreature, int bGhost = 1)
     {
          xEffect eGhost = Effect(EngineConstants.EFFECT_TYPE_ALPHA);
          SetEffectEngineFloatRef(ref eGhost, EngineConstants.EFFECT_FLOAT_POTENCY, 0.5f);
          //eGhost = SetEffectEngineInteger(eGhost, EngineConstants.EFFECT_INTEGER_VFX, EngineConstants.VFX_CRUST_GHOST);
          Engine_ApplyEffectOnObject(5, eGhost, oCreature, 0.0f, oCreature, 0);
          // Visual xEffect to make ghosts spiffier.
          xEffect eVFX = EffectVisualEffect(EngineConstants.VFX_CRUST_GHOST);
          Engine_ApplyEffectOnObject(5, eVFX, oCreature, 0.0f, oCreature, 0);

     }

     public void ExplosionAtLocation(Vector3 lLoc, int nVFX, float fMinDamage, float fMaxDamage, int nDamageType, float fRadius)
     {
          // play explosion vfx at location
          xEffect eEffect = EffectVisualEffect(nVFX);
          Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, lLoc, 0.0f);

          // get objects in radius
          List<GameObject> oTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE | EngineConstants.OBJECT_TYPE_PLACEABLE, EngineConstants.SHAPE_SPHERE, lLoc, fRadius);

          float fDamage;

          // apply damage to objects in loop
          int nCount = 0;
          int nMax = GetArraySize(oTargets);
          for (nCount = 0; nCount < nMax; nCount++)
          {
               fDamage = (RandomFloat() * (fMaxDamage - fMinDamage)) + fMinDamage;
               eEffect = EffectDamage(fDamage, nDamageType);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, oTargets[nCount], 0.0f);
          }
     }

     /* ----------------------------------------------------------------------------
     * @brief This removes any xEffect (and any additional xEffect of the same ability id)
     *        from a creature as a result of a plot event, such as:
     *
     *        - UT_Jump
     *        - Dialog Start
     *
     * @author   Georg Zoeller
     *  -----------------------------------------------------------------------------
     **/
     public void RemoveEffectsDueToPlotEvent(GameObject oCreature)
     {
          List<xEffect> effects = GetEffects(oCreature);
          int i;
          int nSize = GetArraySize(effects);

          for (i = 0; i < nSize; i++)
          {
               if (IsEffectValid(effects[i]) != EngineConstants.FALSE)
               {
                    xEffect _effect = effects[i];
                    int bCancel = GetM2DAInt(EngineConstants.TABLE_EFFECTS, "CancelOnPlotEvent", GetEffectTypeRef(ref _effect));
                    int id = GetEffectAbilityIDRef(ref _effect);
                    if (bCancel != EngineConstants.FALSE)
                    {
                         if (id == 0)
                         {
                              RemoveEffect(oCreature, effects[i]);
                         }
                         else
                         {
                              RemoveEffectsByParameters(oCreature, EngineConstants.EFFECT_TYPE_INVALID, id);
                         }
                    }

               }
          }
     }
}