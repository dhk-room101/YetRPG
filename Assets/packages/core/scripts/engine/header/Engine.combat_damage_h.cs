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
     // combat_damage_h - Damage Include
     // -----------------------------------------------------------------------------
     /*
         Damage Utility Include

         Contains Damage calculation and resistance logic

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"core_h"
     //#include"2da_constants_h"
     //#include"effects_h"
     //#include"sys_disease"
     //#include"plt_cod_aow_spellcombo9"

     //moved public const float DESTROYER_ARMOR_PENALTY = -5.0f;
     //moved public const float DESTROYER_DURATION = 3.0f;
     //moved public const int DESTROYER_VFX = 90065;

     public int Combat_ShatterCheck(GameObject oCreature, GameObject oCaster)
     {
          int bShatter = EngineConstants.FALSE;

          if (IsCreatureBossRank(oCreature) == EngineConstants.FALSE &&
               IsPlot(oCreature) == EngineConstants.FALSE &&
               IsImmortal(oCreature) == EngineConstants.FALSE)
          {
               int nDifficulty = GetGameDifficulty();
               if ((IsPartyMember(oCreature) == EngineConstants.FALSE) || (nDifficulty >= EngineConstants.GAME_DIFFICULTY_HARD))
               {
                    float fChance = 1.0f;
                    if (IsCreatureSpecialRank(oCreature) != EngineConstants.FALSE)
                    {
                         if (nDifficulty == EngineConstants.GAME_DIFFICULTY_CASUAL)
                         {
                              fChance = 0.3f;
                         }
                         else if (nDifficulty == EngineConstants.GAME_DIFFICULTY_NORMAL)
                         {
                              fChance = 0.2f;
                         }
                         else if (nDifficulty == EngineConstants.GAME_DIFFICULTY_HARD)
                         {
                              fChance = 0.10f;
                         }
                         else // nightmare?
                         {
                              fChance = 0.05f;
                         }
                    }
                    float fRandom = RandomFloat();

#if DEBUG
                    LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Shatter fChance = " + ToString(fChance));
                    LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Shatter fRandom = " + ToString(fRandom));
#endif

                    if (fRandom < fChance)
                    {
                         List<xEffect> ePetrify = GetEffects(oCreature, EngineConstants.EFFECT_TYPE_PETRIFY);
                         if (GetArraySize(ePetrify) > 0)
                         {
                              if (GetCanDiePermanently(oCreature) != EngineConstants.FALSE)
                              {
                                   UI_DisplayMessage(oCreature, EngineConstants.UI_MESSAGE_SHATTERED);

                                   // play shattering vfx
                                   xEffect eEffect;
                                   xEffect _effect = ePetrify[0];
                                   if (GetEffectIntegerRef(ref _effect, 0) == 1)
                                   {
                                        // creature
                                        eEffect = EffectVisualEffect(90164);
                                        ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, oCreature, 0.0f, oCaster);
                                   }
                                   else
                                   {
                                        // location
                                        eEffect = EffectVisualEffect(90146);
                                        Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, GetLocation(oCreature), 0.0f, oCaster);

                                        // creature
                                        eEffect = EffectVisualEffect(90150);
                                        ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, oCreature, 0.0f, oCaster);
                                   }

                                   KillCreature(oCreature, oCaster);

                                   bShatter = EngineConstants.TRUE;

                                   // combo xEffect codex - shattering
                                   if (IsFollower(oCaster) != EngineConstants.FALSE)
                                   {
                                        WR_SetPlotFlag(EngineConstants.PLT_COD_AOW_SPELLCOMBO9, EngineConstants.COD_AOW_SPELLCOMBO_9_SHATTER, EngineConstants.TRUE);
                                   }
                              }
                         }
                    }
               }
          }

          return bShatter;
     }

     public float Combat_Damage_GetAbilityDamage(GameObject oDamager, GameObject oDamagee, float fBaseDamage, int nAbility)
     {

          return fBaseDamage;
     }

     // damage, no weapon
     public float Combat_Damage_GetBaseDamage(GameObject oAttacker, float fBaseMin = 0.0f, float fBaseMax = 0.0f)
     {

          float fDmg = Combat_Damage_GetAttributeBonus(oAttacker, EngineConstants.HAND_MAIN);
          return fDmg;
     }

     public float DmgGetArmorRating(GameObject oDefender)
     {
          float fAr = GetCreatureProperty(oDefender, EngineConstants.PROPERTY_ATTRIBUTE_ARMOR);
          // Armor is calculated 70 (fixed) /30 (random)
          float fArRolled = (RandFF(fAr) * EngineConstants.COMBAT_ARMOR_RANDOM_ELEMENT) + (fAr * (1.0f - EngineConstants.COMBAT_ARMOR_RANDOM_ELEMENT));

          GameObject oArmor = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_CHEST, oDefender);

#if DEBUG
          _LogDamage("  fAr:  " + ToString(oArmor) + ":" + ToString(fArRolled) + " = " + ToString(fAr * 0.75) + " + Rand(" + ToString(fAr * 0.25) + ")");
#endif

          return fArRolled;
     }

     public float DmgGetArmorPenetrationRating(GameObject oAttacker, GameObject oWeapon)
     {

          float fBase = GetItemStat(oWeapon, EngineConstants.ITEM_STAT_ARMOR_PENETRATION) + GetCreatureProperty(oAttacker, EngineConstants.PROPERTY_ATTRIBUTE_AP);

          if (IsMeleeWeapon2Handed(oWeapon) != EngineConstants.FALSE)
          {
               // -----------------------------------------------------------------
               // STRONG: Armor Rating
               // -----------------------------------------------------------------
               if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_STRONG) != EngineConstants.FALSE)
               {
#if DEBUG
                    _LogDamage("  fAP: (Modified +25% by TALENT_STRONG) ");
#endif
                    fBase *= 1.25f;
               }
          }

          return fBase;
     }

     public float DmgGetArmorMitigatedDamage(float fDamage, float fArmorPenetration, GameObject oDefender)
     {
          return MaxF(0.0f, fDamage - MaxF(0.0f, DmgGetArmorRating(oDefender) - fArmorPenetration));
     }

     public float GetCriticalDamageModifier(GameObject oAttacker)
     {
          return EngineConstants.COMBAT_CRITICAL_DAMAGE_MODIFIER + (GetCreatureProperty(oAttacker, 54 /*EngineConstants.PROPERTY_ATTRIBUTE_CRITICAL_RANGE*/) / 100.0f);
     }

     public float Combat_Damage_GetBackstabDamage(GameObject oAttacker, GameObject oWeapon, float fDamage)
     {

          //  ------------------------------------------------------------------------
          // Each backstab is an auto crit.
          //  ------------------------------------------------------------------------
          fDamage *= GetCriticalDamageModifier(oAttacker);

          // -------------------------------------------------------------------------
          // Exploit Weakness:  Backstab Damage = Int / 3
          // -------------------------------------------------------------------------
          if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_EXPLOIT_WEAKNESS) != EngineConstants.FALSE)
          {
               float fBase = MaxF(0.0f, (GetAttributeModifier(oAttacker, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE) / 3.0f));
               float fMod = MaxF(0.2f, RandomFloat());
               fDamage += (fBase * fMod);
          }

          // GXA Override
          if (HasAbility(oAttacker, 401312) != EngineConstants.FALSE) // GXA Deep Striking
          {
               if (IsModalAbilityActive(oAttacker, 401310) != EngineConstants.FALSE) // GXA Shadow Striking
               {
                    fDamage *= 1.5f;
               }
          }
          // GXA Override

          return fDamage;
     }

     public float Combat_Damage_GetTalentBoni(GameObject oAttacker, GameObject oDefender, GameObject oWeapon)
     {

          float fBase = 0.0f;

          if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_SHATTERING_BLOWS) != EngineConstants.FALSE)
          {
               if (IsObjectValid(oDefender) != EngineConstants.FALSE)
               {
                    if (GetCreatureAppearanceFlag(oDefender, EngineConstants.APR_RULES_FLAG_CONSTRUCT) != EngineConstants.FALSE)
                    {
                         if (IsUsingMeleeWeapon(oAttacker, oWeapon) != EngineConstants.FALSE && IsMeleeWeapon2Handed(oWeapon) != EngineConstants.FALSE)
                         {
                              fBase += (GetAttributeModifier(oAttacker, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH) * 0.5f);
                         }
                    }
               }
          }

          if (IsModalAbilityActive(oAttacker, EngineConstants.ABILITY_TALENT_BLOOD_FRENZY) != EngineConstants.FALSE)
          {
               float fMod = (10.0f * MaxF(0.0f, 1.0f - _GetRelativeResourceLevel(oAttacker, EngineConstants.PROPERTY_DEPLETABLE_HEALTH)));
#if DEBUG
               _LogDamage("-- BLOOD_FRENZY DAMAGE BONUS: " + ToString(fMod));
#endif

               fBase += fMod;
          }

          return fBase;
     }

     public float Combat_Damage_GetAttackDamage(GameObject oAttacker, GameObject oTarget, GameObject oWeapon, int nAttackResult, float fArmorPenetrationBonus = 0.0f, int bForceMaxWeaponDamage = EngineConstants.FALSE)
     {

          int nHand = EngineConstants.HAND_MAIN;
          int nSlot = GetItemEquipSlot(oWeapon);

          // -------------------------------------------------------------------------
          // special case: one hit kill forms generally don't do damage...
          // -------------------------------------------------------------------------
          if (IsShapeShifted(oAttacker) != EngineConstants.FALSE)
          {
               if (GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "OneShotKills", GetAppearanceType(oAttacker)) != EngineConstants.FALSE)
               {
                    return 1.0f;
               }
          }

          if (IsObjectValid(oWeapon) != EngineConstants.FALSE)
          {
               if (nSlot == EngineConstants.INVENTORY_SLOT_MAIN || nSlot == EngineConstants.INVENTORY_SLOT_BITE)
               {
                    nHand = EngineConstants.HAND_MAIN;
               }
               else if (nSlot == EngineConstants.INVENTORY_SLOT_OFFHAND)
               {
                    nHand = EngineConstants.HAND_OFFHAND;
               }

               // Mage staffs have their own rules
               if (nAttackResult != EngineConstants.COMBAT_RESULT_DEATHBLOW && GetBaseItemType(oWeapon) == EngineConstants.BASE_ITEM_TYPE_STAFF)
               {
                    if (GetHasEffects(oAttacker, EngineConstants.EFFECT_TYPE_SHAPECHANGE) == EngineConstants.FALSE)
                    {
                         return Combat_Damage_GetMageStaffDamage(oAttacker, oTarget, oWeapon);
                    }
                    else
                    {
                         oWeapon = null;
                    }
               }

          }

          // Weapon Attribute Bonus Factor
          float fFactor = GetWeaponAttributeBonusFactor(oWeapon);

          // Attribute Modifier
          float fStrength = Combat_Damage_GetAttributeBonus(oAttacker, nHand, oWeapon) * fFactor;

          // Weapon Damage
          float fWeapon = IsObjectValid(oWeapon) != EngineConstants.FALSE ? DmgGetWeaponDamage(oWeapon, bForceMaxWeaponDamage) : EngineConstants.COMBAT_DEFAULT_UNARMED_DAMAGE;

          // Game Difficulty Adjustments
          float fDiffBonus = Diff_GetRulesDamageBonus(oAttacker);

          float fDamage = fWeapon + fStrength + fDiffBonus;
          float fDamageScale = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, "fDamageScale", GetCreatureRank(oAttacker));

          float fAr = DmgGetArmorRating(oTarget);

          // GXA Override
          if (HasAbility(oAttacker, 401101) != EngineConstants.FALSE) // GXA Spirit Damage
          {
               if (IsModalAbilityActive(oAttacker, 401100) != EngineConstants.FALSE) // GXA Spirit Warrior
               {
                    // bypass armor for normal attacks
                    fAr = 0.0f;
               }
          }
          // GXA Override

          float fAp = DmgGetArmorPenetrationRating(oAttacker, oWeapon) + fArmorPenetrationBonus;
          float fDmgBonus = GetCreatureProperty(oAttacker, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_BONUS);

#if DEBUG
          _LogDamage("Total: " + ToString(fDamage), oTarget);
          _LogDamage("  fStrength: " + ToString(fStrength));
          _LogDamage("  fWeapon  : " + ToString(fWeapon));
          _LogDamage("  fDmgBonus: " + ToString(fDmgBonus));
          _LogDamage("        fAr: " + ToString(fAr));
          _LogDamage("        fAp: " + ToString(fAp));
          _LogDamage(" fRankScale: " + ToString(fDamageScale));
#endif

          if (nAttackResult == EngineConstants.COMBAT_RESULT_CRITICALHIT)
          {
               fDamage *= GetCriticalDamageModifier(oAttacker);
#if DEBUG
               _LogDamage("Crit:        " + ToString(fDamage));
#endif
          }
          else if (nAttackResult == EngineConstants.COMBAT_RESULT_BACKSTAB)
          {
               fDamage = Combat_Damage_GetBackstabDamage(oAttacker, oWeapon, fDamage);
#if DEBUG
               _LogDamage("Backstab:        " + ToString(fDamage));
#endif
          }
          else if (nAttackResult == EngineConstants.COMBAT_RESULT_DEATHBLOW)
          {
               fDamage = GetMaxHealth(oTarget) + 1.0f;
#if DEBUG
               _LogDamage("Deathblow damage:" + ToString(fDamage));
#endif
          }

          fDamage = fDamage - MaxF(0.0f, fAr - fAp);

          fDamage += fDmgBonus + Combat_Damage_GetTalentBoni(oAttacker, oTarget, oWeapon);

          // -------------------------------------------------------------------------
          // Damage scale only kicks in on 'significant' damage.
          // -------------------------------------------------------------------------
          if (fDamageScale > 0.0f && fDamage > GetDamageScalingThreshold())
          {
               fDamage *= fDamageScale;
          }

          // -------------------------------------------------------------------------
          // Weapon damage is always at least 1, even with armor. This is intentional
          // to avoid deadlocks of creatures that are both unable to damage each other
          // -------------------------------------------------------------------------
          fDamage = MaxF(1.0f, fDamage);

          return (fDamage);
     }

     public void Combat_Damage_CheckOnImpactAbilities(GameObject oTarget, GameObject oDamager, float fDamage, int nAttackResult, GameObject oWeapon, int nAbility)
     {

          if (nAbility != 0)
          {

          }

          // -------------------------------------------------------------------------
          // Some passive abilities grant 'to hit' effects...
          // -------------------------------------------------------------------------
          if (nAttackResult == EngineConstants.COMBAT_RESULT_CRITICALHIT)
          {
               /*
                       if (!IsCreatureBossRank(oTarget) && !IsImmortal(oTarget) && !IsPlot(oTarget))
                       {
                           // ---------------------------------------------------------------------
                           // Petrified creatures that are hit by
                           // ---------------------------------------------------------------------
                           if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_PETRIFY))
                           {
                               if (GetCanDiePermanently(oTarget))
                               {
                                   UI_DisplayMessage(oTarget,EngineConstants.UI_MESSAGE_SHATTERED);
                                   KillCreature(oTarget, oDamager);
                                   return;
                               }
                           }
                       }*/

               if (Combat_ShatterCheck(oTarget, oDamager) != EngineConstants.FALSE)
               {
                    return;
               }

               if (IsMeleeWeapon2Handed(oWeapon) != EngineConstants.FALSE)
               {
                    if (HasAbility(oDamager, EngineConstants.ABILITY_TALENT_STUNNING_BLOWS) != EngineConstants.FALSE)
                    {
                         // ~50%
                         if (RandomFloat() < 0.5f)
                         {
                              if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_STUN) == EngineConstants.FALSE)
                              {
                                   Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectStun(), oTarget, 1.5f + (RandomFloat() * 2.5f), oDamager, EngineConstants.ABILITY_TALENT_STUNNING_BLOWS);
#if DEBUG
                                   _LogDamage("DAMAGE-Combat-Efffect: STUNNING_BLOWS");
#endif

                              }
                         }
                    }

                    if (HasAbility(oDamager, EngineConstants.ABILITY_TALENT_DESTROYER) != EngineConstants.FALSE)
                    {
                         // Can not stack temporary effects on hit, might cause runaway memory usage in high speed attack situations
                         if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_MODIFY_PROPERTY, EngineConstants.ABILITY_TALENT_DESTROYER) == EngineConstants.FALSE)
                         {

                              xEffect eDebuff = EffectModifyProperty(EngineConstants.PROPERTY_ATTRIBUTE_ARMOR, EngineConstants.DESTROYER_ARMOR_PENALTY);
                              SetEffectEngineIntegerRef(ref eDebuff, EngineConstants.EFFECT_INTEGER_VFX, EngineConstants.DESTROYER_VFX);
                              Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eDebuff, oTarget, EngineConstants.DESTROYER_DURATION, oDamager, EngineConstants.ABILITY_TALENT_DESTROYER);

#if DEBUG
                              _LogDamage("DAMAGE-Combat-Efffect: EngineConstants.ABILITY_TALENT_DESTROYER");
#endif
                         }

                    }
               }    /* using 2 h weapon*/

          } /* crit*/

          // -------------------------------------------------------------------------
          // Any significant results in additional bleeding equivalent to 25% of damage
          // over 4 seconds on a backstab
          // -------------------------------------------------------------------------
          else if (nAttackResult == EngineConstants.COMBAT_RESULT_BACKSTAB)
          {
               if ((fDamage >= 10.0f) && IsModalAbilityActive(oDamager, EngineConstants.ABILITY_TALENT_LACERATE) != EngineConstants.FALSE)
               {
                    // Can not stack temporary effects on hit, might cause runaway memory usage in high speed attack situations
                    if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_DOT, EngineConstants.ABILITY_TALENT_LACERATE) == EngineConstants.FALSE)
                    {
                         ApplyEffectDamageOverTime(oTarget, oDamager, EngineConstants.ABILITY_TALENT_LACERATE, fDamage * 0.25f, 4.0f, EngineConstants.DAMAGE_TYPE_PHYSICAL);
                    }
               }
          }

     }      /* func*/
}