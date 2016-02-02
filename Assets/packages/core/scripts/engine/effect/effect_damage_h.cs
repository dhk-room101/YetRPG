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
     // effect_damage_h
     // -----------------------------------------------------------------------------
     /*

         This file is the SINGLE POINT OF ENTRY for all health modification in the
         game.

         Limited Editing Permissions: Only Georg edits this file.

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"sys_gore_h"
     //#include"events_h"
     ////#include"wrappers_h"
     //#include"ui_h"

     //#include"stats_core_h"

     //------------------------------------------------------------------------------
     // Include Effects we are dependent on
     //------------------------------------------------------------------------------
     //#include"effect_death_h"
     //#include"effect_heal_h"
     //#include"effect_modify_mana_stam_h"
     //#include"sys_stealth_h"
     //#include"sys_resistances_h"

     //moved public const int ANIMATION_DAMAGE_ADDITIVE = 103;

     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_NONE        = 0x00000000 ;
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL    = 0x00000001; //critical hit
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_DEATHBLOW   = 0x00000002; //death blow
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_UPDATE_GORE = 0x00000004; //update gore on attacker
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_50    = 0x00000008; //leech 50% health back to attacker
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_75    = 0x00000010; //leech 75% health back to attacker
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_100   = 0x00000020; //leech 100% health back to attacker
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_MANA    = 0x00000040; // mana is leeched instead of health
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE  = 0x00000080; // can not be resisted
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_BACKSTAB      = 0x00000100; // backstab
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_25      = 0x00000200; //leech 20% health back to attacker
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG      = 0x00000400; //'bonus' damage (different message) from item property.
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_FROM_DOT       = 0x00000800; //coming from dots.
     //moved public const int EngineConstants.DAMAGE_EFFECT_FLAG_NOLEECH        = 0x00001000; //convert leech into simple 'also deal damage'

     //moved public const float EngineConstants.DAMAGE_CRITICAL_DISPLAY_THRESHOLD = 10.0f; // any damage below this will not show up as critical (even though it is handled internally as such)
     //moved public const float EngineConstants.DAMAGE_IMMUNITY_MESSAGE_THRESHOLD = 8.0f;

     //moved public const float LIFEWARD_HEALTH_FRACTION = 0.33;
     //moved public const int   LIFEWARD_HEALING_VFX = 1021;

     //moved public const int FEAST_OF_THE_FALLEN_VFX = 90011;

     /*

     // -----------------------------------------------------------------------------
     // Damage Types
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.DAMAGE_TYPE_INVALID                       = 0;
     //moved public const int EngineConstants.DAMAGE_TYPE_PHYSICAL                      = 1;
     //moved public const int EngineConstants.DAMAGE_TYPE_FIRE                          = 2;
     //moved public const int EngineConstants.DAMAGE_TYPE_COLD                          = 3;
     //moved public const int EngineConstants.DAMAGE_TYPE_ELECTRICITY                   = 4;
     //moved public const int EngineConstants.DAMAGE_TYPE_POISON                        = 5;
     //moved public const int EngineConstants.DAMAGE_TYPE_LETHAL                        = 6;
     //moved public const int EngineConstants.DAMAGE_TYPE_TBD                           = 7;  //debug

     */

     public int IsHostileEffectAllowed(GameObject oTarget, GameObject oDamager, int nAbility)
     {

          int bValid = EngineConstants.TRUE;

          if (GetObjectType(oDamager) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               if (IsObjectHostile(oTarget, oDamager) == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsHostileEffectAllowed", "Damager and Target not hostile: false");
#endif
                    bValid = EngineConstants.FALSE;
               }
          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsHostileEffectAllowed", "Damager is a placeable, skipping hostility checks");
#endif
               return EngineConstants.TRUE;
          }

          // -----------------------------------------------------------------
          // Abilities by default don't care for hostilities
          // -----------------------------------------------------------------
          if (nAbility != 0)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsHostileEffectAllowed", "Ability: true");
#endif
               bValid = EngineConstants.TRUE;
          }

          // -----------------------------------------------------------------
          // Neutrals can never be damaged...
          // -----------------------------------------------------------------
          if (GetGroupId(oTarget) == EngineConstants.GROUP_NEUTRAL)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsHostileEffectAllowed", "Neutral Group: false");
#endif
               // Can harm self
               if (oTarget != oDamager)
               {
                    bValid = EngineConstants.FALSE;
               }
          }

          // -----------------------------------------------------------------
          // Non combatants don't get damaged either
          // -----------------------------------------------------------------
          if (GetCombatantType(oTarget) == EngineConstants.CREATURE_TYPE_NON_COMBATANT)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsHostileEffectAllowed", "Non Combatant: false");
#endif

               // Can harm self
               if (oTarget != oDamager)
               {
                    bValid = EngineConstants.FALSE;
               }
          }

          return bValid;
     }

     public int IsFriendlyFireParty(GameObject oTarget, GameObject oDamager)
     {
          // verify same group id
          if (GetGroupId(oTarget) == GetGroupId(oDamager))
          {
               // Only for party members, since this is a difficulty option. Monsters still can nuke each other
               if (IsPartyMember(oTarget) != EngineConstants.FALSE)
               {
                    return EngineConstants.TRUE;
               }
          }
          return EngineConstants.FALSE;
     }

     public int IsDamageAllowed(GameObject oTarget, GameObject oDamager, int nAbility, int nDamageType, int nDamageFlags = 0)
     {

          if (GetObjectType(oDamager) != EngineConstants.OBJECT_TYPE_CREATURE)
          {
               return EngineConstants.TRUE;
          }

          // ------------------------------------------------------------------------
          // No dealing damage in cutscene or dialog unless damage type is plot
          // ------------------------------------------------------------------------
          if (nDamageType != EngineConstants.DAMAGE_TYPE_PLOT)
          {
               int nMode = GetGameMode();

               if (nMode == EngineConstants.GM_CONVERSATION)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsDamageAllowed", "Game mode is CUTSCENE or CONVERSATION and damage type is not PLOT - not allowing damage");
#endif
                    return EngineConstants.FALSE;
               }

               // ---------------------------------------------------------------------
               // DIFFICULTY: No friendly fire in easy difficulty
               // ---------------------------------------------------------------------
               if (GetGameDifficulty() == EngineConstants.GAME_DIFFICULTY_CASUAL)
               {
                    int bUnresistable = (nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE) == EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE ? EngineConstants.TRUE : EngineConstants.FALSE;
                    int bDot = (nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_FROM_DOT) == EngineConstants.DAMAGE_EFFECT_FLAG_FROM_DOT ? EngineConstants.TRUE : EngineConstants.FALSE;

                    // Unresistable damage still gets through, unless it's from a dot
                    if (bUnresistable == EngineConstants.FALSE || bDot != EngineConstants.FALSE)
                    {
                         if (IsFriendlyFireParty(oTarget, oDamager) != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsDamageAllowed",
                                "Easy Difficulty, Not allowing damage between members of the player's party");
#endif

                              return EngineConstants.FALSE;
                         }
                    }

               }
               // ---------------------------------------------------------------------
               // DIFFICULTY END
               // ---------------------------------------------------------------------

          }

          int bValid = EngineConstants.TRUE;

          if (IsObjectValid(oDamager) != EngineConstants.FALSE && GetObjectType(oDamager) == EngineConstants.OBJECT_TYPE_CREATURE)
          {

               // ---------------------------------------------------------------------
               // Only creatures care about hostility settings
               // ---------------------------------------------------------------------
               if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
               {
                    bValid = bValid != EngineConstants.FALSE && IsHostileEffectAllowed(oTarget, oDamager, nAbility) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;
               }
          }

          // -----------------------------------------------------------------
          // ... neither can plot objects.
          // -----------------------------------------------------------------
          if (IsPlot(oTarget) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsDamageAllowed", "Plot:" + ToString(oTarget) + " - true: False");
#endif

               bValid = EngineConstants.FALSE;
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.IsDamageAllowed", "Result:" + ToString(bValid));
#endif

          return bValid;
     }

     public float GetModifiedDamage(GameObject oDamager, int nDamageType, float fAmount)
     {

          int nProperty = 0;

          switch (nDamageType)
          {
               case EngineConstants.DAMAGE_TYPE_FIRE:
                    nProperty = EngineConstants.PROPERTY_ATTRIBUTE_FIRE_DAMAGE_BONUS;
                    break;
               case EngineConstants.DAMAGE_TYPE_ELECTRICITY:
                    nProperty = EngineConstants.PROPERTY_ATTRIBUTE_ELECTRICITY_DAMAGE_BONUS;
                    break;
               case EngineConstants.DAMAGE_TYPE_SPIRIT:
                    nProperty = EngineConstants.PROPERTY_ATTRIBUTE_SPIRIT_DAMAGE_BONUS;
                    break;
               case EngineConstants.DAMAGE_TYPE_NATURE:
                    nProperty = EngineConstants.PROPERTY_ATTRIBUTE_NATURE_DAMAGE_BONUS;
                    break;
               case EngineConstants.DAMAGE_TYPE_COLD:
                    nProperty = EngineConstants.PROPERTY_ATTRIBUTE_COLD_DAMAGE_BONUS;
                    break;

          }

          if (nProperty != EngineConstants.FALSE)
          {
               float fMultiplier = 1.0f + (GetCreatureProperty(oDamager, nProperty) / 100.0f);
               return fAmount * fMultiplier;
          }

          return fAmount;

     }

     //===========================================================================//
     //                              Effect Damage                                //
     //===========================================================================//

     /*
     * @brief Instant Damage - Inline version of EffectDamage
     *
     *
     *
     * @author Georg
     */
     public int Effects_ApplyInstantEffectDamage(GameObject oTarget, GameObject oDamager, float fDamage, int nDamageType = EngineConstants.DAMAGE_TYPE_PHYSICAL, int nDamageFlags = EngineConstants.DAMAGE_EFFECT_FLAG_NONE, int nAbility = 0, int nImpactVfx = 0)
     {

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_damage_h.Effects_ApplyInstantEffectDamage", "damage:" + FloatToString(fDamage) + " flags: " + IntToHexString(nDamageFlags) + " abi: " + ToString(nAbility) + ", current health: " + IntToString(GetHealth(oTarget)), oTarget);
#endif

          float fOldHealth = GetCurrentHealth(oTarget);
          int bReturn = EngineConstants.FALSE;
          int bFatal = EngineConstants.FALSE;

          // GXA Override
          if (nDamageType == EngineConstants.DAMAGE_TYPE_PHYSICAL)
          {
               if (HasAbility(oDamager, 401101) != EngineConstants.FALSE) // GXA Spirit Damage
               {
                    if (IsModalAbilityActive(oDamager, 401100) != EngineConstants.FALSE) // GXA Spirit Warrior
                    {
                         // normal physical damage is converted to spirit damage
                         nDamageType = EngineConstants.DAMAGE_TYPE_SPIRIT;
                    }
               }
          }
          // GXA Override

          if (IsDamageAllowed(oTarget, oDamager, nAbility, nDamageType, nDamageFlags) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.Effects_ApplyInstantEffectDamage", "Damage nullified, target creature does not meet valid target requirements.");
#endif
               fDamage = 0.0f;
          }

          if (nDamageType != EngineConstants.DAMAGE_TYPE_PHYSICAL && nDamageType != EngineConstants.DAMAGE_TYPE_PLOT)
          {
               fDamage = GetModifiedDamage(oDamager, nDamageType, fDamage);

          }

          // -------------------------------------------------------------------------
          // Mana Shield: Take 1.5x the damage in mana. Reduce damage by the amount
          // reduced.
          // -------------------------------------------------------------------------
          if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               if (fDamage >= 1.0f)
               {
                    if (nDamageType != EngineConstants.DAMAGE_TYPE_PLOT && GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_MANA_SHIELD) != EngineConstants.FALSE)
                    {
                         float fMana = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA);

                         // GXA Override
                         List<xEffect> eEffects = GetEffects(oTarget, EngineConstants.EFFECT_TYPE_MANA_SHIELD);
                         xEffect _effect = eEffects[0];
                         float fManaFactor = GetEffectFloatRef(ref _effect, 0);
                         if (fManaFactor <= 0.0f) // default
                         {
                              fManaFactor = 1.5f;
                         }
                         float fManaDamage = MinF(fDamage * fManaFactor, fMana);
                         // GXA Override

                         UpdateCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fManaDamage * -1.0f, EngineConstants.PROPERTY_VALUE_CURRENT);
                         UI_DisplayDamageFloaty(oTarget, oDamager, FloatToInt(fManaDamage), 1, 0, 0, 1);

                         // GXA Override
                         float fDamageFactor = GetEffectFloatRef(ref _effect, 1);
                         if (fDamageFactor <= 0.0f) // default
                         {
                              fDamageFactor = 0.75f;
                         }
                         fDamage = MaxF(0.0f, fDamage - (fManaDamage * fDamageFactor));
                         // GXA Override

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.Effects_ApplyInstantEffectDamage", "Damage aborbed by mana shield. Remaining Damage " + ToString(fDamage));
#endif

                         if (fDamage < 1.0f)
                         {
                              return EngineConstants.TRUE;
                         }
                    }
               }
          }

          // -------------------------------------------------------------------------
          // DIFFICULTY: 50% friendly fire in normal difficulty
          // -------------------------------------------------------------------------
          if ((GetGameDifficulty() == EngineConstants.GAME_DIFFICULTY_NORMAL) && nDamageType != EngineConstants.DAMAGE_TYPE_PLOT)
          {
               if (GetObjectType(oDamager) == EngineConstants.OBJECT_TYPE_CREATURE)
               {
                    // Unresistable damage still gets through
                    int bUnresistable = (nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE) == EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE ? EngineConstants.TRUE : EngineConstants.FALSE;
                    int bDot = (nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_FROM_DOT) == EngineConstants.DAMAGE_EFFECT_FLAG_FROM_DOT ? EngineConstants.TRUE : EngineConstants.FALSE;

                    if (bUnresistable == EngineConstants.FALSE || bDot != EngineConstants.FALSE)
                    {
                         if (IsFriendlyFireParty(oTarget, oDamager) != EngineConstants.FALSE)
                         {

                              DEBUG_PrintToScreen("BBB2", 5);

#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.Effects_ApplyInstantEffectDamage", "Medium Difficulty, scaling FF back to 50%");
#endif

                              fDamage *= 0.5f;
                         }
                    }

               }
          }
          // -------------------------------------------------------------------------
          // DIFFICULTY END
          // -------------------------------------------------------------------------

          // -------------------------------------------------------------------------
          // Creatures may have damage shield or scale
          // -------------------------------------------------------------------------
          if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               float fScale = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_SCALE);
               if (fScale > 1.0f)
               {
                    fDamage *= fScale;
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "Increase through DAMAGE VULNERABILITY EFFECT: x" + ToString(fScale), oTarget);
#endif
               }

               // damage shield
               float fShieldPoints = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_SHIELD_POINTS);
               if (fShieldPoints > 0.0f)
               {
                    float fReduction;

                    // if shield has a strength, that is the maximum amount deducted
                    float fShieldStrength = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_SHIELD_STRENGTH);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "fShieldPoints = " + ToString(fShieldPoints), oTarget);
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "fShieldStrength = " + ToString(fShieldStrength), oTarget);
#endif
                    if (fShieldStrength > 0.0f)
                    {
                         fReduction = MinF(fShieldPoints, fShieldStrength);
                    }
                    else
                    {
                         fReduction = fShieldPoints;
                    }

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "Reduction through DAMAGE SHIELD: " + ToString(fReduction), oTarget);
#endif
                    if (fReduction >= fDamage)
                    {
                         fDamage = 0.0f;

                         UpdateCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_SHIELD_POINTS, (fDamage * -1.0f), EngineConstants.PROPERTY_VALUE_MODIFIER);

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "  Reducing damage to 0", oTarget);
#endif
                    }
                    else
                    {
                         fDamage -= fReduction;

                         if (fReduction >= fShieldPoints)
                         {
                              SetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_SHIELD_POINTS, 0.0f, EngineConstants.PROPERTY_VALUE_MODIFIER);

#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "  Reducing shield to 0", oTarget);
#endif
                         }
                         else
                         {
                              UpdateCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_SHIELD_POINTS, (fReduction * -1.0f), EngineConstants.PROPERTY_VALUE_MODIFIER);

#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "  Reducing damage by reduction", oTarget);
#endif
                         }
                    }

#if DEBUG
                    fShieldPoints = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_SHIELD_POINTS);
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "Remaining DAMAGE SHIELD: " + ToString(fShieldPoints - fReduction), oTarget);
#endif
               }
          }

          if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE) == EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.Effects_ApplyInstantEffectDamage", "Damage flagged as unresistable!");
#endif
          }
          else
          {
               if (fDamage > 0.0f)
               {

                    // -------------------------------------------------------------------------
                    // Handle Damage Immunity
                    // -------------------------------------------------------------------------
                    if (DamageIsImmuneToType(oTarget, nDamageType) != EngineConstants.FALSE)
                    {
                         if (GetObjectType(oTarget) != EngineConstants.OBJECT_TYPE_PLACEABLE)
                         {
                              // -------------------------------------------------------
                              // Only message immunity if a PC is involved
                              // -------------------------------------------------------
                              if (IsPartyMember(oTarget) != EngineConstants.FALSE || IsPartyMember(oDamager) != EngineConstants.FALSE)
                              {
                                   if (fDamage > EngineConstants.DAMAGE_IMMUNITY_MESSAGE_THRESHOLD)
                                   {
                                        UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_IMMUNE, "", GetColorByDamageType(nDamageType));
                                   }
                              }
                         }
                         return EngineConstants.TRUE;
                    }
                    else if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_DAMAGE_WARD) != EngineConstants.FALSE)
                    {
                         // -------------------------------------------------------
                         // Only message immunity if a PC is involved
                         // -------------------------------------------------------
                         if (IsPartyMember(oTarget) != EngineConstants.FALSE || IsPartyMember(oDamager) != EngineConstants.FALSE)
                         {
                              UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_NO_EFFECT);

                              if (IsPartyMember(oDamager) != EngineConstants.FALSE)
                              {
                                   PlaySoundSet(oDamager, EngineConstants.SS_COMBAT_WEAPON_INEFFECTIVE, 0.3f);
                              }
                         }

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", ToString(oTarget) + " DAMAGE ZEROED because of EngineConstants.DAMAGE_WARD_EFFECT");
#endif
                         return EngineConstants.TRUE;
                    }
                    else
                    {
                         // -------------------------------------------------------------------------
                         // Resist and nullify negative damage
                         // -------------------------------------------------------------------------
                         fDamage = ResistDamage(oDamager, oTarget, nAbility, fDamage, nDamageType);
                    }
               }
          }

          if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_LIFE_WARD) != EngineConstants.FALSE)
          {
               if ((fOldHealth - fDamage) < (GetMaxHealth(oTarget) * EngineConstants.LIFEWARD_HEALTH_FRACTION))
               {

                    // play healing vfx
                    // ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(LIFEWARD_HEALING_VFX), oTarget, 0.0f, oTarget, EngineConstants.ABILITY_SPELL_LIFEWARD));
                    List<xEffect> aWards = GetEffects(oTarget, EngineConstants.EFFECT_TYPE_LIFE_WARD);
                    xEffect _effect = aWards[0];
                    float fWardHealth = GetEffectFloatRef(ref _effect, 0);
                    Effect_ApplyInstantEffectHeal(oTarget, GetEffectCreatorRef(ref _effect), fWardHealth);
                    RemoveEffect(oTarget, aWards[0]);
                    fOldHealth = GetCurrentHealth(oTarget);
               }
          }

          // -------------------------------------------------------------------------
          // Calculate the new health
          // -------------------------------------------------------------------------
          float fNewHealth = fOldHealth - fDamage;

          float fFloatyValue = IntToFloat(FloatToInt(fDamage));
          // -------------------------------------------------------------------------
          // Anything lower than 1.0f is treated as 0
          // -------------------------------------------------------------------------
          if (FloatToInt(fNewHealth) < 1)
          {
               if (IsImmortal(oTarget) != EngineConstants.FALSE && GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h", "Immortal Target, fatal damage changed to nonfatal", oTarget);
#endif

                    UI_DisplayMessage(oTarget, EngineConstants.UI_DEBUG_CREATURE_IMMORTAL);

                    fNewHealth = 1.0f;
                    fFloatyValue = 0.0f;
               }
               else
               {
                    fNewHealth = 0.0f;
                    bFatal = EngineConstants.TRUE;
               }
          }

          // -------------------------------------------------------------------------
          // Edge case due to fractions.
          // -------------------------------------------------------------------------
          if (fDamage > 0.0f && fDamage < 1.0f)
          {
               fDamage = 1.0f;
               fFloatyValue = 1.0f;
          }

          // -------------------------------------------------------------------------
          // Display the damage floaty
          // -------------------------------------------------------------------------
          int bBonusDamage = ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG) == EngineConstants.DAMAGE_EFFECT_FLAG_BONUS_DMG) ? EngineConstants.TRUE : EngineConstants.FALSE;
          int bBackstab = ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_BACKSTAB) == EngineConstants.DAMAGE_EFFECT_FLAG_BACKSTAB) ? EngineConstants.TRUE : EngineConstants.FALSE;
          int bCritical = ((((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL) == EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL) || bBackstab != EngineConstants.FALSE) && fFloatyValue >= EngineConstants.DAMAGE_CRITICAL_DISPLAY_THRESHOLD) ? EngineConstants.TRUE : EngineConstants.FALSE;
          int bDeathblow = ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_DEATHBLOW) == EngineConstants.DAMAGE_EFFECT_FLAG_DEATHBLOW) ? EngineConstants.TRUE : EngineConstants.FALSE;

          UI_DisplayDamageFloaty(oTarget, oDamager, FloatToInt(fFloatyValue),
                                 bCritical, nAbility, bBonusDamage, 0, bBackstab, nDamageType);

          // -------------------------------------------------------------------------
          // Only proceed if there's actually damage dealt.
          // -------------------------------------------------------------------------
          if (fDamage > 0.0f)
          {

               if (fNewHealth < fOldHealth)
               {

                    // -------------------------------------------------------------------------
                    // Set the new health for non-fatal blows
                    // If the blow is fatal, then the health will be lowered when the target dies (applies the death effect)
                    // This is needed since the engine considers a target as 'dead' when it's health is 0.
                    // -------------------------------------------------------------------------
                    if (bFatal == EngineConstants.FALSE)
                    {

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_damage_h.Effects_ApplyInstantEffectDamage", "damage:" + FloatToString(fDamage) + " flags: " + IntToHexString(nDamageFlags) + " setting health to: " + FloatToString(fNewHealth), oTarget);
#endif

                         // -------------------------------------------------------------
                         // THIS IS THE ONLY SINGLE POINT IN EngineConstants.GAME THAT MODIFIES HEALTH
                         // IN THE EngineConstants.GAME AFTER CHARACTER GENERATION!
                         // -------------------------------------------------------------
                         SetCurrentHealth(oTarget, fNewHealth);

                         // -------------------------------------------------------------
                         // Notify the UI system to indicate damage to a party member
                         // -------------------------------------------------------------
                         if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
                              SignalDamage(oDamager, oTarget);

                         // -------------------------------------------------------------
                         // It really makes only sense to send OnDamage if you're not
                         // getting OnDeath the next frame, so this is signalled here.
                         // -------------------------------------------------------------
                         SendEventOnDamaged(oTarget, oDamager, fDamage, nDamageType, nAbility);

                    }

                    bReturn = EngineConstants.TRUE;
               }
               else
               {
                    return EngineConstants.FALSE;
               }

               if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
               {

                    // Stats - handle damage dealt
                    STATS_HandleDamageDealt(oDamager, oTarget, fDamage);

                    // ---------------------------------------------------------------------
                    // Allow some of the soundsets to play again
                    // ---------------------------------------------------------------------
                    SSResetSoundsetRestrictionsOnDamage(oTarget, fOldHealth);

                    // Apply the death xEffect for fatal blows
                    if (bFatal != EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_damage_h.Effects_ApplyInstantEffectDamage", "damage " + FloatToString(fDamage) + " fatal, killing target.", oTarget);
#endif

                         // -------------------------------------------------------------
                         // Feast of the fallen: regain stamina on backstab
                         // -------------------------------------------------------------
                         if (bBackstab != EngineConstants.FALSE || bDeathblow != EngineConstants.FALSE)
                         {
                              if (IsObjectValid(oDamager) != EngineConstants.FALSE && GetObjectType(oDamager) == EngineConstants.OBJECT_TYPE_CREATURE)
                              {
                                   if (HasAbility(oDamager, EngineConstants.ABILITY_TALENT_FEAST_OF_THE_FALLEN) != EngineConstants.FALSE)
                                   {
                                        float fMax = GetCreatureProperty(oDamager, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_TOTAL);
                                        float fCurrent = GetCreatureProperty(oDamager, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT);
                                        float fValue = GetLevel(oTarget) * 5.0f * GetM2DAFloat(Diff_GetAutoScaleTable(), "fScale", GetCreatureRank(oTarget));
                                        float fRegain = MinF(fMax - fCurrent, fValue);

                                        UpdateCreatureProperty(oDamager, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fRegain, EngineConstants.PROPERTY_VALUE_CURRENT);

                                        //UI_DisplayDamageFloaty(oDamager, oTarget, FloatToInt(fRegain), 1, 0, 0, 2);

                                        // vfx
                                        ApplyEffectVisualEffect(oDamager, oDamager, EngineConstants.FEAST_OF_THE_FALLEN_VFX, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f, EngineConstants.ABILITY_TALENT_FEAST_OF_THE_FALLEN);
                                   }
                              }
                         }

                         KillCreature(oTarget, oDamager, nAbility, EngineConstants.FALSE, FloatToInt(fDamage));
                    }
                    else
                    {

                         // -------------------------------------------------------------
                         // Damage must be significant enough to trigger anim
                         // -------------------------------------------------------------
                         if (fDamage >= 3.0f)
                         {
                              PlayAdditiveAnimation(oTarget, EngineConstants.ANIMATION_DAMAGE_ADDITIVE);
                         }

                         // -------------------------------------------------------------
                         // EL: //Tracking required for the TACTICIAN Achievement
                         // -------------------------------------------------------------
                         if (IsHero(oTarget) != EngineConstants.FALSE)
                         {
                              SetLocalInt(oDamager, EngineConstants.CREATURE_DAMAGED_THE_HERO, EngineConstants.TRUE);
                         }

                    }

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.ApplyInstantEffectDamage", "Damage: " + FloatToString(fDamage), oDamager);
#endif

                    // -------------------------------------------------------------------------
                    //                          *** Gore Handling ***
                    // -------------------------------------------------------------------------
                    float fAmount;
                    if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_UPDATE_GORE) == EngineConstants.DAMAGE_EFFECT_FLAG_UPDATE_GORE)
                    {

                         fAmount = EngineConstants.GORE_CHANGE_HIT;
                         int nVfx = 1016;
                         // ---------------------------------------------------------------------
                         // Crits and Deathblows apply different gorelevels than mere hits
                         // ---------------------------------------------------------------------
                         if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL) == EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL)
                         {
                              fAmount = EngineConstants.GORE_CHANGE_CRITICAL;
                              nVfx = 1015;
                         }
                         else if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_DEATHBLOW) == EngineConstants.DAMAGE_EFFECT_FLAG_DEATHBLOW)
                         {
                              fAmount = EngineConstants.GORE_CHANGE_DEATHBLOW;
                         }

                         // ---------------------------------------------------------------------
                         // Change the gore level to the requested amount and display VFX
                         // ---------------------------------------------------------------------
                         if (nDamageType == EngineConstants.DAMAGE_TYPE_PHYSICAL && CanCreatureBleed(oTarget) != EngineConstants.FALSE)
                         {
                              ApplyEffectVisualEffect(oDamager, oTarget, nVfx, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         }

                         Gore_ModifyGoreLevel(oDamager, fAmount);

                    }

                    fAmount = 0.0f;
                    // -------------------------------------------------------------------------
                    // If the Leech flag is set, heal the caster by the amount of damage
                    // done
                    // -------------------------------------------------------------------------
                    if (fDamage > 0.0f)
                    {
                         // GXA Override
                         if (HasAbility(oTarget, 401212) != EngineConstants.FALSE) // GXA Inner Power
                         {
                              UpdateCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, (fDamage * 0.5f), EngineConstants.PROPERTY_VALUE_CURRENT);
                         }
                         // GXA Override

                         // -------------------------------------------------------------
                         // Electricity based spells deal additional stamina damage
                         // -------------------------------------------------------------
                         if (nDamageType == EngineConstants.DAMAGE_TYPE_ELECTRICITY)
                         {
                              if (GetCreatureCoreClass(oTarget) != EngineConstants.CLASS_WIZARD)
                              {
                                   UpdateCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, (-1.0f * fDamage), EngineConstants.PROPERTY_VALUE_CURRENT);
                              }
                         }

                         int bDrain = EngineConstants.FALSE;

                         if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_100) == EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_100)
                         {
                              fAmount = fDamage;
                              bDrain = EngineConstants.TRUE;

                         }
                         else if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_75) == EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_75)
                         {
                              fAmount = fDamage * 0.75f;
                              bDrain = EngineConstants.TRUE;

                         }
                         else if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_50) == EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_50)
                         {
                              fAmount = fDamage * 0.5f;
                              bDrain = EngineConstants.TRUE;

                         }
                         else if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_25) == EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_25)
                         {
                              fAmount = fDamage * 0.25f;
                              bDrain = EngineConstants.TRUE;
                         }

                         if (nImpactVfx == 0)
                         {

                              if (nDamageType == EngineConstants.DAMAGE_TYPE_ELECTRICITY)
                              {
                                   nImpactVfx = 1006;
                              }
                              else if (nDamageType == EngineConstants.DAMAGE_TYPE_COLD)
                              {
                                   nImpactVfx = 1013;

                                   ApplyEffectVisualEffect(oDamager, oTarget, 1013, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                              }
                              else if (nDamageType == EngineConstants.DAMAGE_TYPE_FIRE)
                              {
                                   nImpactVfx = (fDamage > 20.0f) ? 1107 : 1108;
                              }
                              else if (nDamageType == EngineConstants.DAMAGE_TYPE_NATURE)
                              {
                                   nImpactVfx = 1504; // (PeterT) changed to smaller vfx
                              }
                              else if (nDamageType == EngineConstants.DAMAGE_TYPE_SPIRIT)
                              {
                                   nImpactVfx = 1514;
                              }

                              if (nImpactVfx != EngineConstants.FALSE)
                              {
                                   ApplyEffectVisualEffect(oDamager, oTarget, nImpactVfx, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                              }
                         }
                         else if (nImpactVfx > 0)
                         {
                              ApplyEffectVisualEffect(oDamager, oTarget, nImpactVfx, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         }

                         // -------------------------------------------------------------------------
                         // If we have a leech xEffect flag set, drain here
                         // -------------------------------------------------------------------------
                         if (bDrain != EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_damage_h.Effects_ApplyInstantEffectDamage", "health leech:" + FloatToString(fAmount), oTarget);
#endif

                              if (fAmount > 0.0f)
                              {

                                   if ((nDamageFlags & EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_MANA) == EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_MANA)
                                   {
                                        xEffect eEffect = EffectModifyManaStamina(fAmount);
                                        ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, oDamager, 0.0f, oTarget, 0);
                                        // Only show this floaty when it happens to the player.
                                        if (IsControlled(oTarget) != EngineConstants.FALSE)
                                        {
                                             UI_DisplayDamageFloaty(oTarget, oDamager, FloatToInt(fAmount), 1, 0, 0, 2);
                                        }
                                        UpdateCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, (-1.0f * fAmount), EngineConstants.PROPERTY_VALUE_CURRENT);

                                   }
                                   else
                                   {
                                        Effect_ApplyInstantEffectHeal(oDamager, oTarget, fAmount, EngineConstants.TRUE);
                                   }
                              }
                         }

                    }
               }
               else if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_PLACEABLE)
               {
                    // Apply the death xEffect for fatal blows
                    if (bFatal != EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_damage_h.Effects_ApplyInstantEffectDamage", "damage " + FloatToString(fDamage) + " fatal, killing placeable.", oTarget);
#endif
                         DestroyPlaceable(oTarget, oDamager, nAbility);
                    }
                    else
                    {
                         // Apparently Placeables do have a damage additive too.
                         PlayAdditiveAnimation(oTarget, EngineConstants.ANIMATION_DAMAGE_ADDITIVE);
                    }

               }

          }
          else
          {
#if DEBUG
               DisplayFloatyMessage(oTarget, "Debug: Zero damage dealt.");
#endif
          }

          return EngineConstants.TRUE;

     }

     // MGB - February 23, 2009
     // EffectDamage Constructor moved into Engine.

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleApplyEffectDamage
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: David Sims
     //  Created On: July 11, 2006
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleApplyEffectDamage(xEffect eEffect, GameObject oTarget = null)
     {
          if (oTarget == null) oTarget = gameObject;
          GameObject oDamager = GetEffectCreatorRef(ref eEffect);

          float fDamage = GetEffectFloatRef(ref eEffect, 0);
          int nDamageType = GetEffectIntegerRef(ref eEffect, 0);
          int nDamageFlags = GetEffectIntegerRef(ref eEffect, 1);
          int nImpactVfx = GetEffectIntegerRef(ref eEffect, 2);

#if DEBUG
          //catch instances of where we use EngineConstants.DAMAGE_TYPE_TBD
          if (nDamageType == EngineConstants.DAMAGE_TYPE_TBD && EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DAMAGE, "effect_damage_h.EffectDamage", "damage applied with EngineConstants.DAMAGE_TYPE_TBD", oTarget, EngineConstants.LOG_SEVERITY_WARNING);
          }
#endif

          return Effects_ApplyInstantEffectDamage(oTarget, oDamager, fDamage, nDamageType, nDamageFlags, GetEffectAbilityIDRef(ref eEffect), nImpactVfx);

     }

     public void DamageCreature(GameObject oTarget, GameObject oDamager, float fDamage, int nDamageType = EngineConstants.DAMAGE_TYPE_PLOT, int bUnresistable = EngineConstants.FALSE)
     {
          Effects_ApplyInstantEffectDamage(oTarget, oDamager, fDamage, nDamageType, bUnresistable != EngineConstants.FALSE ? EngineConstants.DAMAGE_EFFECT_FLAG_UNRESISTABLE : EngineConstants.DAMAGE_EFFECT_FLAG_NONE, 0, 0);
     }
}