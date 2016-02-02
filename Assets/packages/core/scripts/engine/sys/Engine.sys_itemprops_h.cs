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
     //#include"core_h"
     //#include"effects_h"

     public xEffect ItemProp_ReadOnHitEffect(int nId, int nPower, int nType)
     {
          int nInt0 = GetM2DAInt(EngineConstants.TABLE_ITEMPRPS, "Int0", nId);
          int nInt1 = GetM2DAInt(EngineConstants.TABLE_ITEMPRPS, "Int1", nId);
          float fFloat0 = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "Float0", nId);
          float fFloat1 = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "Float1", nId);

          xEffect eRet = Effect(nType);
          SetEffectIntegerRef(ref eRet, 0, nInt0);
          SetEffectIntegerRef(ref eRet, 1, nInt1);

          // Damage grows with power
          if (nType == EngineConstants.EFFECT_TYPE_DAMAGE)
          {
               fFloat0 = fFloat0 * nPower;
          }
          SetEffectFloatRef(ref eRet, 0, fFloat0);
          SetEffectFloatRef(ref eRet, 1, fFloat1);

          return eRet;
     }

     public void HandlePoisonEffects(int nId, int nPower, float fDuration, int nDamageType, GameObject oTarget)
     {
          xEffect ePoisonEffect;
          float fRes = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_NATURE);
          int nAbilityId;
          int bAlwaysHits;

          if (RandomFloat() * 100 > (1 + nPower) * 5.0f)
          {
               return;
          }

          fDuration += 1.0f;
          switch (nId)
          {
               case 3008 /* Venom */:
                    {
                         ePoisonEffect = EffectModifyMovementSpeed(0.6f, EngineConstants.TRUE);
                         SetEffectEngineIntegerRef(ref ePoisonEffect, EngineConstants.EFFECT_INTEGER_VFX, 93029);
                         break;
                    }
               case 3009 /* Deathroot */:
               case 3010:
                    {
                         ePoisonEffect = EffectStun();
                         break;
                    }
               case 3014: /* Quiet Death */
                    {
                         if (IsCreatureSpecialRank(oTarget) == EngineConstants.FALSE)
                         {
                              // Instantly kills non special rank creatures with one hit if under 20% health.
                              if (_GetRelativeResourceLevel(oTarget, EngineConstants.PROPERTY_DEPLETABLE_HEALTH) < 0.2)
                              {
                                   ApplyEffectVisualEffect(oTarget, oTarget, 90005, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                                   KillCreature(oTarget, gameObject, 0, EngineConstants.FALSE, 0);
                              }
                         }
                         return;
                    }
               default:
                    {
                         return;
                    }
          }
          nAbilityId = GetM2DAInt(EngineConstants.TABLE_ITEMPRPS, "AbilityId", nId);

          int nEffectType = GetEffectTypeRef(ref ePoisonEffect);
          if (nEffectType != EngineConstants.EFFECT_TYPE_INVALID && nAbilityId != 0 && fDuration > 0.0f)
          {

               {
                    if (GetHasEffects(oTarget, nEffectType, nAbilityId) == EngineConstants.FALSE)
                    {
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, ePoisonEffect, oTarget, fDuration, oTarget, nAbilityId);
                    }

               }

          }
     }

     public void ItemProp_DoEffect(GameObject oAttacker, GameObject oTarget, int nId, int nPower = 1)
     {

          // --------------------------------------------------------------------------\
          // Warn about bad itemprops.
          // --------------------------------------------------------------------------
          if (EngineConstants.LOG_ENABLED == EngineConstants.TRUE)
          {
               if (nPower == 0)
               {
#if DEBUG
                    Warning("Item with OnHit Item Power == 0 Found. Please file High bug to georg. Details: " + ToString(nId));
#endif
                    return;
               }
          }

          int nType = GetM2DAInt(EngineConstants.TABLE_ITEMPRPS, "Effect", nId);
          xEffect eOnHit = new xEffect(EngineConstants.EFFECT_TYPE_INVALID);
          float fDuration = 0.0f;
          if (nType != EngineConstants.EFFECT_TYPE_INVALID)
          {
               eOnHit = ItemProp_ReadOnHitEffect(nId, nPower, nType);
               SetEffectCreatorRef(ref eOnHit, oAttacker);
               nType = GetEffectTypeRef(ref eOnHit);
               fDuration = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "BaseDuration", nId);
               if (fDuration > 0.0f)
               {
                    fDuration += IntToFloat(nPower);
                    fDuration = GetRankAdjustedEffectDuration(oTarget, fDuration);
               }

               /// --------------------------------------------------------------------
               //  OnHit Item Effects never apply on top of existing effects of the
               //  same type, for performance and balance reasons.
               /// --------------------------------------------------------------------
               if (GetHasEffects(oTarget, nType) != EngineConstants.FALSE)
               {
                    return;
               }
          }

          // -------------------------------------------------------------------------
          // This if/elseif statement is ordered by probablity for performance reasons.
          // insert high volume events at the top.
          // -------------------------------------------------------------------------
          if (nType == EngineConstants.EFFECT_TYPE_DAMAGE)
          {
               // ---------------------------------------------------------------------
               // if the Flags field for the xEffect was empty in the 2da, set the
               // 'bonus damage' flag.
               // ---------------------------------------------------------------------
               if (GetEffectIntegerRef(ref eOnHit, 1) == 0)
               {
                    SetEffectIntegerRef(ref eOnHit, 1, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
               }

               Effects_HandleApplyEffectDamage(eOnHit, oTarget);

               HandlePoisonEffects(nId, nPower, fDuration, GetEffectIntegerRef(ref eOnHit, 0), oTarget);

          }
          else if (nType == EngineConstants.EFFECT_TYPE_PARALYZE)
          {
               SetEffectEngineIntegerRef(ref eOnHit, EngineConstants.EFFECT_INTEGER_VFX, 90128);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eOnHit, oTarget, fDuration, oAttacker, EngineConstants.ABILITY_SPELL_PARALYZE);

          }
          else if (nType == 1057 /*EngineConstants.EFFECT_TYPE_KNOCKBACK*/)
          {
               eOnHit = Effect(1057);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eOnHit, oTarget, fDuration, oAttacker, 0);
          }
          else if (nType == EngineConstants.EFFECT_TYPE_STUN)
          {
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eOnHit, oTarget, fDuration, oAttacker, 0);
          }
          // -------------------------------------------------------------------------
          // Special case abilities (no effect, scripted by Id)
          // -------------------------------------------------------------------------
          else if (nType == EngineConstants.EFFECT_TYPE_INVALID)
          {

               // ---------------------------------------------------------------------
               // HardCoded: OnHit - Slay Darkspawn.
               //  Slays creatures flagged as apr_base.creaturetype darkspawn and
               //  not flagged as creatureranks.isspecial 1
               // ---------------------------------------------------------------------
               if (nId == EngineConstants.ITEM_PROPERTY_ONHIT_SLAY_DARKSPAWN)
               {
                    int nApr = GetAppearanceType(oTarget);
                    int bDarkspawn = (GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "creature_type", nApr) == EngineConstants.CREATURE_TYPE_DARKSPAWN) ? EngineConstants.TRUE : EngineConstants.FALSE;
                    int bSpecial = GetM2DAInt(EngineConstants.TABLE_CREATURERANKS, "IsSpecial", GetCreatureRank(oTarget));
                    if (bDarkspawn != EngineConstants.FALSE && bSpecial == EngineConstants.FALSE && GetCanDiePermanently(oTarget) != EngineConstants.FALSE && IsImmortal(oTarget) == EngineConstants.FALSE && IsPlot(oTarget) == EngineConstants.FALSE)
                    {
                         ApplyEffectVisualEffect(oAttacker, oTarget, 90005, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         KillCreature(oTarget, oAttacker, 0, EngineConstants.FALSE, 0);
                    }

               }
               else if (nId == EngineConstants.ITEM_PROPERTY_ONHIT_SLOW)
               {
                    if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_MOVEMENT_RATE, 90028) == EngineConstants.FALSE)
                    {

                         xEffect eSlow = EffectModifyMovementSpeed(0.5f);
                         SetEffectEngineIntegerRef(ref eSlow, EngineConstants.EFFECT_INTEGER_VFX, 1105);
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eSlow, oTarget, 10.0f, oAttacker, 90028);

                    }
               }
               else if (nId == EngineConstants.ITEM_PROPERTY_ONHIT_VICIOUS)
               {
                    // -----------------------------------------------------------------
                    // For performance reasons, we wont' apply on top of other dots.
                    // Not optimal, I know.
                    // -----------------------------------------------------------------
                    if (CanCreatureBleed(oTarget) != EngineConstants.FALSE)
                    {
                         if (IsCreatureSpecialRank(oAttacker) != EngineConstants.FALSE) /*having this on non lieutenants is waay to annoying */
                         {
                              if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_DOT) == EngineConstants.FALSE)
                              {
                                   float fTime = MaxF(2.0f + fDuration + IntToFloat(nPower), 5.0f);
                                   ApplyEffectDamageOverTime(oTarget, oAttacker, EngineConstants.ABILITY_TALENT_DUAL_WEAPON_EXPERT, 2.0f + 5.0f * IntToFloat(nPower), fTime, EngineConstants.DAMAGE_TYPE_PHYSICAL, 0, 1016 /*SMALL_BLOOD*/);
                              }
                         }
                    }
               }
               else if (nId == 4008 /* EngineConstants.ITEM_PROPERTY_ONHIT_THREAT */)
               {
                    UpdateThreatTable(oTarget, oAttacker, 5.0f);
               }

               // ---------------------------------------------------------------------
               // MageSlayer: Cancel spellcasting of non instant spells on hit.
               // ---------------------------------------------------------------------
               else if (nId == EngineConstants.ITEM_PROPERTY_ONHIT_MAGESLAYER)
               {
                    if (GetCreatureRank(oTarget) != EngineConstants.CREATURE_RANK_ELITE_BOSS)
                    {
                         xCommand cmd = GetCurrentCommand(oTarget);
                         int nCmdType = GetCommandType(cmd);
                         int nAbi = GetCommandIntRef(ref cmd, 0);

                         if (nCmdType == EngineConstants.COMMAND_TYPE_USE_ABILITY)
                         {
                              // Only abilities with speed >0 can be interrupted.
                              if (CanInterruptSpell(nAbi) != EngineConstants.FALSE)
                              {
                                   // ---------------------------------------------
                                   // Since EngineConstants.COMMAND_USEABILITY can be in a movement
                                   // subaction, filter additionally for conjure
                                   // phase.
                                   // ---------------------------------------------
                                   if (IsConjuring(oTarget) != EngineConstants.FALSE)
                                   {
                                        UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_INTERRUPTED);
                                        Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "MAGESLAYER EngineConstants.WEAPON PROC: Spell interrupted");
                                        ApplyEffectVisualEffect(oAttacker, oTarget, 90181, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                                        WR_ClearAllCommands(oTarget, EngineConstants.TRUE);
                                   }
                              }
                         }
                    }
               }
               else if (nId == 6107) // damage vs beasts
               {
                    int nAppearance = GetAppearanceType(oTarget);
                    nType = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "CREATURE_TYPE", nAppearance);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS BEASTS EngineConstants.WEAPON PROC: appearance " + ToString(nAppearance));
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS BEASTS EngineConstants.WEAPON PROC: type " + ToString(nType));
#endif
                    if ((nType == EngineConstants.CREATURE_TYPE_ANIMAL) || (nType == EngineConstants.CREATURE_TYPE_BEAST))
                    {
                         float fFloat0 = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "Float0", nId);
                         float fDamage = fFloat0 * IntToFloat(nPower);
                         Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_PHYSICAL, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS BEASTS EngineConstants.WEAPON PROC: damage +" + ToString(fDamage));
#endif
                    }
               }
               else if (nId == 1511) // damage vs darkspawn
               {
                    int nAppearance = GetAppearanceType(oTarget);
                    nType = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "CREATURE_TYPE", nAppearance);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS DARKSPAWN EngineConstants.WEAPON PROC: appearance " + ToString(nAppearance));
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS DARKSPAWN EngineConstants.WEAPON PROC: type " + ToString(nType));
#endif
                    if (nType == EngineConstants.CREATURE_TYPE_DARKSPAWN)
                    {
                         float fFloat0 = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "Float0", nId);
                         float fDamage = fFloat0 * IntToFloat(nPower);
                         Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_PHYSICAL, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS DARKSPAWN EngineConstants.WEAPON PROC: damage +" + ToString(fDamage));
#endif
                    }
               }
               else if (nId == 1512) // damage vs dragons
               {
                    int nAppearance = GetAppearanceType(oTarget);
                    nType = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "CREATURE_TYPE", nAppearance);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS DRAGONS EngineConstants.WEAPON PROC: appearance " + ToString(nAppearance));
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS DRAGONS EngineConstants.WEAPON PROC: type " + ToString(nType));
#endif
                    if (nType == EngineConstants.CREATURE_TYPE_DRAGON)
                    {
                         float fFloat0 = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "Float0", nId);
                         float fDamage = fFloat0 * IntToFloat(nPower);
                         Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_PHYSICAL, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS DARKSPAWN EngineConstants.WEAPON PROC: damage +" + ToString(fDamage));
#endif
                    }
               }
               else if (nId == 1513) // damage vs spirits
               {
                    int nAppearance = GetAppearanceType(oTarget);
                    nType = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "CREATURE_TYPE", nAppearance);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS SPIRITS EngineConstants.WEAPON PROC: appearance " + ToString(nAppearance));
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS SPIRITS EngineConstants.WEAPON PROC: type " + ToString(nType));
#endif
                    if (nType == EngineConstants.CREATURE_TYPE_UNDEAD)
                    {
                         float fFloat0 = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "Float0", nId);
                         float fDamage = fFloat0 * IntToFloat(nPower);
                         Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_PHYSICAL, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS SPIRITS EngineConstants.WEAPON PROC: damage +" + ToString(fDamage));
#endif
                    }
               }
               else if (nId == 3012 || nId == 3011) // magebane / soldier's bane
               {
                    int bMagicUser = IsMagicUser(oTarget);
                    if ((nId == 3012 && bMagicUser != EngineConstants.FALSE) || (nId == 3011 && bMagicUser == EngineConstants.FALSE))
                    {
                         float fAmount = nPower * 5.0f;
                         xEffect eMod = EffectModifyManaStamina(fAmount);
                         Effects_HandleApplyEffectModifyManaStamina(eMod);
                         UI_DisplayDamageFloaty(oTarget, oAttacker, FloatToInt(fAmount), 1, 0, 0, 1);
                    }

               }
               else if (nId == 1514) // damage vs fade creatures
               {
                    int nAppearance = GetAppearanceType(oTarget);
                    nType = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "CREATURE_TYPE", nAppearance);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS FADE EngineConstants.WEAPON PROC: appearance " + ToString(nAppearance));
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS FADE EngineConstants.WEAPON PROC: type " + ToString(nType));
#endif
                    if (nType == EngineConstants.CREATURE_TYPE_DEMON)
                    {
                         float fFloat0 = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "Float0", nId);
                         float fDamage = fFloat0 * IntToFloat(nPower);
                         Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_PHYSICAL, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "VS FADE EngineConstants.WEAPON PROC: damage +" + ToString(fDamage));
#endif
                    }
               }
               else if (nId == 6065) // weakens and bloody
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "WEAKEN EngineConstants.WEAPON PROC");
#endif

                    // weaken only if the target does not have weakness applied
                    if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.ABILITY_SPELL_WEAKNESS) == EngineConstants.FALSE)
                    {
                         xEffect eEffect = EffectDecreaseProperty(EngineConstants.PROPERTY_ATTRIBUTE_ATTACK, -5.0f,
                                                                 EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE, -5.0f);
                         SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_VFX, 90127);
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eEffect, oTarget, 10.0f, oAttacker, EngineConstants.ABILITY_SPELL_WEAKNESS);

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "WEAKEN EngineConstants.WEAPON PROC: weakened");
#endif
                    }

                    // bloody target
                    Gore_ModifyGoreLevel(oTarget, EngineConstants.GORE_CHANGE_CRITICAL);
               }
               else if (nId == 401011) // GXA
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "EXPLOSIVE EngineConstants.WEAPON PROC");
#endif

                    xEffect eEffect;
                    eEffect = EffectVisualEffect(110105);
                    Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, GetLocation(oTarget), 0.0f, oAttacker, 402201);

                    float fRadius = 2.0f;
                    List<GameObject> oTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(oTarget), fRadius);
                    int nCount = 0;
                    int nMax = GetArraySize(oTargets);
                    float fBaseDamage = GetAttributeModifier(oAttacker, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH);
                    float fArmor;
                    float fModified;
                    for (nCount = 0; nCount < nMax; nCount++)
                    {
                         // not the attacker
                         if (oTargets[nCount] != oAttacker)
                         {
                              // different target
                              if (oTargets[nCount] != oTarget)
                              {
                                   // not dead or dying
                                   if (IsDeadOrDying(oTargets[nCount]) == EngineConstants.FALSE)
                                   {
                                        // hostiles only
                                        if (IsObjectHostile(oAttacker, oTargets[nCount]) != EngineConstants.FALSE)
                                        {
                                             fArmor = GetCreatureProperty(oTargets[nCount], EngineConstants.PROPERTY_ATTRIBUTE_ARMOR);
                                             fModified = fBaseDamage - fArmor;
                                             if (fModified >= 1.0f)
                                             {
                                                  eEffect = EffectDamage(fModified);
                                                  ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, oTargets[nCount], 0.0f, oAttacker, 402201);
                                             }
                                        }
                                   }
                              }
                         }
                    }
               }
               else if (nId == 401013)
               {
                    xEffect eEffect = EffectDispelMagic();
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, oTarget, 0.0f, oAttacker, 430050);
               }
               else if ((nId == 401013) || (nId == 401014))
               {
                    float fFloat0 = GetM2DAFloat(EngineConstants.TABLE_ITEMPRPS, "Float0", nId);
                    float fDamage = fFloat0 * IntToFloat(nPower);
                    Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_FIRE, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
                    Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_COLD, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
                    Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_ELECTRICITY, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
                    Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_NATURE, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
                    Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, EngineConstants.DAMAGE_TYPE_SPIRIT, EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "sys_itemprop_h.Execute", "CHROMATIC EngineConstants.WEAPON PROC: damage +" + ToString(fDamage));
#endif
               }
               // ---------------------------------------------------------------------
               // Anything not handled above is a bug. Display message if we are not
               // on _SHIP build
               // ---------------------------------------------------------------------
               else
               {

                    Warning("UnHandled scripted OnHit xEffect found. This means implementation is missing. Please file a blocking bug Georg. Details: " + ToString(nId));
               }
          }
          else
          {
               Warning("Generic OnHit xEffect found. This means implementation might be faulty. Please file a high bug Georg. Details: " + ToString(nId));

               if (fDuration > 0.0f)
               {
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eOnHit, oTarget, fDuration, oAttacker, 0);
               }
               else
               {
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eOnHit, oTarget, 0.0f, oAttacker, 0);
               }
          }

     }
}