//ready and useless
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

     /*
     //moved public const float EngineConstants.CREATURE_RULES_TICK_DELAY = 1.5f;

     ///////////////////////////////////////////////////////////////////////////////
     //  EffectDOT
     ///////////////////////////////////////////////////////////////////////////////

     public xEffect EffectDOT(float fDamagePerSecond, int nInterval, int nDamageType = EngineConstants.DAMAGE_TYPE_FIRE, int nVFX = EngineConstants.VFX_INVALID)
     {
         xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_DOT);
         eEffect = SetEffectFloatRef(ref eEffect, 0, fDamagePerSecond);
         eEffect = SetEffectIntegerRef(ref eEffect, 0, nInterval);
         eEffect = SetEffectIntegerRef(ref eEffect, 1, nDamageType);
         eEffect = SetEffectIntegerRef(ref eEffect, 2, nVFX);

         return eEffect;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleApplyEffectDOT
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleApplyEffectDOT(xEffect eEffect)
     {
         GameObject oCreator = GetEffectCreatorRef(ref eEffect);
         int nVFX = GetEffectIntegerRef(ref eEffect,2);
         int nAbilityId = GetEffectAbilityIDRef(ref eEffect);

         // we only support creature dots
         if (GetObjectType(gameObject) == EngineConstants.OBJECT_TYPE_CREATURE)
         {

             if (nVFX != EngineConstants.VFX_INVALID)
             {
                 ApplyEffectVisualEffect(oCreator, gameObject, nVFX, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, 0.0f,nAbilityId);
             }

             if (!GetCreatureFlag(gameObject,EngineConstants.CREATURE_RULES_FLAG_DOT))
             {
                 SetCreatureFlag(gameObject,EngineConstants.CREATURE_RULES_FLAG_DOT);
                 DelayEvent(EngineConstants.CREATURE_RULES_TICK_DELAY,  gameObject,  Event(  EngineConstants.EVENT_TYPE_DOT_TICK));
             }
         }
         else
         {
             return EngineConstants.FALSE;
         }

         return EngineConstants.TRUE;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleRemoveEffectDOT
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleRemoveEffectDOT(xEffect eEffect)
     {
         GameObject oCreator = GetEffectCreatorRef(ref eEffect);
         int nVFX = GetEffectIntegerRef(ref eEffect,2);

         if (nVFX != EngineConstants.VFX_INVALID)
         {

             int nAbilityId = GetEffectAbilityIDRef(ref eEffect);

             List<xEffect> effects = GetEffects(gameObject,EngineConstants.EFFECT_TYPE_VISUAL_EFFECT);
             int nSize = GetArraySize(effects);
             int i;

             for (i = 0; i < nSize; i++)
             {
                 if (GetEffectAbilityIDRef(ref effects[i]) ==  nAbilityId &&
                     GetEffectCreatorRef(ref effects[i]) == oCreator)
                 {
                   RemoveEffect(gameObject,effects[i]);
                 }
             }

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
     public void Effects_HandleDotEffectTick(xEffect eEffect);
     public void Effects_HandleDotEffectTick(xEffect eEffect)
     {

         // ---------------------------------------------------------------------------
         // Since the parameter xEffect is damage per second, we're normalizing it
         // ---------------------------------------------------------------------------
         float fDamage = (GetEffectFloatRef(ref eEffect,0) * EngineConstants.CREATURE_RULES_TICK_DELAY);
         int nSecondsPerTick = GetEffectIntegerRef(ref eEffect,0);
         int nDamageType    = GetEffectIntegerRef(ref eEffect,1);
         GameObject oCreator = GetEffectCreatorRef(ref eEffect);
         int nAbilityId = GetEffectAbilityIDRef(ref eEffect);
         Effects_ApplyInstantEffectDamage(gameObject, oCreator, fDamage, nDamageType,nAbilityId);

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
     public void Effects_HandleCreatureDotTickEvent(GameObject oCreature = gameObject);
     public void Effects_HandleCreatureDotTickEvent(GameObject oCreature = gameObject)
     {
         if (GetCreatureFlag(oCreature,EngineConstants.CREATURE_RULES_FLAG_DOT))
         {

             List<xEffect> effects = GetEffects(oCreature, EngineConstants.EFFECT_TYPE_DOT);
             int nSize = GetArraySize(effects);
             int i;

             if (nSize == 0)
             {
                 // ---------------------------------------------
                 // Sync the flag to false if no DOTs are active
                 // ---------------------------------------------
                 SetCreatureFlag(oCreature,EngineConstants.CREATURE_RULES_FLAG_DOT,EngineConstants.FALSE);

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
                 DelayEvent(EngineConstants.CREATURE_RULES_TICK_DELAY,  oCreature ,  Event(  EngineConstants.EVENT_TYPE_DOT_TICK));
             }
         }
     }
     */
}