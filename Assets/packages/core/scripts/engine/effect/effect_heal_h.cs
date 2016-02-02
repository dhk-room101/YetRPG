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
     // effect_heal_h
     // -----------------------------------------------------------------------------
     /*
         Effect Heal

             When n applied to an object, this effect
             * restores the given amount of health to the object. The object�s health will
             * not be raised above its maximum health.

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"ui_h"
     //#include"log_h"
     //#include"core_h"
     //#include"2da_constants_h"
     ////#include"wrappers_h"
     //#include"events_h"
     //#include"effect_constants_h"
     //#include"sys_soundset_h"

     //moved public const float BLOOD_MAGIC_FACTOR = 10.0f;

     /* @brief Returns an xEffect which heals an object.
     *
     * Constructor for the heal health effect. When applied to an object, this effect
     * restores the given amount of health to the object. The object�s health will
     * not be raised above its maximum health. If the GameObject is a creature which is
     * dead no health will be healed unless bHealIfDead is EngineConstants.TRUE, in which case the
     * creature will be resurrected and healed.
     *
     * @param nValue � The amount of health.
     * @param bHealIfDead � Whether or not to resurrect and heal dead creatures.
     *
     * @author David Sims
     *
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_HEALHEALTH.
     */
     public xEffect EffectHeal(float fValue, int bIgnoreBloodMagic = EngineConstants.FALSE)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_HEALHEALTH);
        SetEffectFloatRef(ref eEffect, 0, fValue);
        SetEffectIntegerRef(ref eEffect, 0, bIgnoreBloodMagic);
          return eEffect;
     }

     //------------------------------------------------------------------------------
     // @brief: Instantly heals the target by nAmount, using oHealer as source
     //
     //------------------------------------------------------------------------------
     public int Effect_ApplyInstantEffectHeal(GameObject oTarget, GameObject oHealer, float fAmount, int bIgnoreBloodMagic = EngineConstants.FALSE)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_heal_h.Effects_HandleApplyEffectHeal", GetTag(oHealer) + " healing target for " + FloatToString(fAmount), oTarget);
#endif
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_heal_h.Effects_HandleApplyEffectHeal", "bIgnoreBloodMagic = " + ToString(bIgnoreBloodMagic));
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_heal_h.Effects_HandleApplyEffectHeal", "IsModalAbilityActive(oTarget, EngineConstants.ABILITY_SPELL_BLOOD_MAGIC) = " + ToString(IsModalAbilityActive(oTarget, EngineConstants.ABILITY_SPELL_BLOOD_MAGIC)));

          // -------------------------------------------------------------------------
          // Allow some of the soundsets to play again
          // -------------------------------------------------------------------------
          SSResetSoundsetRestrictionsOnHeal(oTarget);

          float fMaxHealth = GetMaxHealth(oTarget);
          float fCurrentHealth = GetCurrentHealth(oTarget);
          int bIsDead = IsDead(oTarget);

          // -------------------------------------------------------------------------
          // Retrieve the healing modifier from the creature.
          // -------------------------------------------------------------------------
          float fFactor = GetCreatureProperty(oTarget, 51) / 100.0f;
          // -------------------------------------------------------------------------
          // Savegame compatibility for builds <= 7793
          // -------------------------------------------------------------------------
#if DEBUG
          if (fFactor < 0.01)
          {
               fFactor = 1.0f;
          }
#endif
          fAmount *= fFactor;

          //--------------------------------------------------------------------------
          // factor in game difficulty.
          //--------------------------------------------------------------------------
          fAmount *= Diff_GetRulesHealingModifier(oTarget);

          // -------------------------------------------------------------------------
          // Zero negative heals
          // -------------------------------------------------------------------------
          if (fAmount < 0.0f)
          {
               fAmount = 0.0f;
          }

          // if cursed, don't heal
          if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_CURSE_OF_MORTALITY) != EngineConstants.FALSE)
          {
               return EngineConstants.TRUE;
          }

          // is Blood Magic active
          if (IsModalAbilityActive(oTarget, EngineConstants.ABILITY_SPELL_BLOOD_MAGIC) != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_heal_h.Effects_HandleApplyEffectHeal", "Blood Magic Active.", oTarget);

               if (bIgnoreBloodMagic == EngineConstants.FALSE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_heal_h.Effects_HandleApplyEffectHeal", "Applying Blood Magic Factor", oTarget);

                    // healing is reduced by a specific factor
                    fAmount /= EngineConstants.BLOOD_MAGIC_FACTOR;
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_heal_h.Effects_HandleApplyEffectHeal", "NOT Applying Blood Magic Factor", oTarget);
               }
          }

          if ((fCurrentHealth + fAmount) > fMaxHealth)
          {
               fAmount = fMaxHealth - fCurrentHealth;
          }

          float fNewHealth = fCurrentHealth + fAmount;

          if (IsDead(oTarget) == EngineConstants.FALSE)
          {
               SetCurrentHealth(oTarget, (fCurrentHealth + fAmount));

               UI_DisplayHealFloaty(oTarget, oHealer, FloatToInt(fAmount));

               // 06/12/07 - Halve the target's gore level.
               // PeterT - Re-enabling at Georg's request. How else is gore reduced on a creature?
               float fCurrentGore = GetCreatureGoreLevel(oTarget);
               fCurrentGore /= 2.0f;
               SetCreatureGoreLevel(oTarget, fCurrentGore);
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_heal_h.Effects_HandleApplyEffectHeal", "creature DEAD - NOT healing dead creature", oTarget);
               return EngineConstants.FALSE;
          }
          return EngineConstants.TRUE;

     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleApplyEffectHealHealth
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: David Sims
     //  Created On: July 14, 2006
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleApplyEffectHeal(xEffect eEffect)
     {
          float fAmount = GetEffectFloatRef(ref eEffect, 0);
          int bIgnoreBloodMagic = GetEffectIntegerRef(ref eEffect, 0);

          GameObject oHealer = GetEffectCreatorRef(ref eEffect);
          GameObject oTarget = gameObject;

          return Effect_ApplyInstantEffectHeal(oTarget, oHealer, fAmount, bIgnoreBloodMagic);

     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleRemoveEffectHealHealth
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: David Sims
     //  Created On: July 14, 2006
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleRemoveEffectHeal(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }

     public void HealPartyMembers(int bIgnoreBloodMagic = EngineConstants.FALSE, int bRestoreStamina = EngineConstants.FALSE)
     {

          List<GameObject> partyMembers = GetPartyList();
          int nMembers = GetArraySize(partyMembers);
          int i;

          for (i = 0; i < nMembers; i++)
          {
               Effect_ApplyInstantEffectHeal(partyMembers[i], null, 5000.0f, bIgnoreBloodMagic);
               if (bRestoreStamina != EngineConstants.FALSE)
               {
                    UpdateCreatureProperty(partyMembers[i], EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, 5000.00f, EngineConstants.PROPERTY_VALUE_CURRENT);
               }
          }
     }

     public void HealCreature(GameObject oCreature, int bIgnoreBloodMagic = EngineConstants.FALSE, float fAmount = -1.0f, int bRestoreStamina = EngineConstants.FALSE)
     {
          float fHeal = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH);

          if (fAmount > 0.0f)
          {
               fHeal = MinF(fHeal, fAmount);
          }

          if (bRestoreStamina != EngineConstants.FALSE)
          {
               UpdateCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, 5000.00f, EngineConstants.PROPERTY_VALUE_CURRENT);
          }

          Effect_ApplyInstantEffectHeal(oCreature, null, fHeal, bIgnoreBloodMagic);
     }
}