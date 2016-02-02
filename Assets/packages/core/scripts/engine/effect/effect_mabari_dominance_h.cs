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
     // effect_mabari_dominance_h
     // -----------------------------------------------------------------------------
     /*
     */
     // -----------------------------------------------------------------------------
     // Owner: PeterT
     // -----------------------------------------------------------------------------

     public xEffect EffectMabariDominance()
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_MABARI_DOMINANCE);
          return eEffect;
     }

     //moved public const float EngineConstants.MABARI_DOMINANCE_STRENGTH_BONUS = 2.0f;
     //moved public const float EngineConstants.MABARI_DOMINANCE_CONSTITUTION_BONUS = 2.0f;
     //moved public const float EngineConstants.MABARI_DOMINANCE_WILLPOWER_BONUS = 2.0f;
     public int Effects_HandleApplyEffectMabariDominance(xEffect eEffect)
     {
          // add buff
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, EngineConstants.MABARI_DOMINANCE_STRENGTH_BONUS, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION, EngineConstants.MABARI_DOMINANCE_CONSTITUTION_BONUS, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER, EngineConstants.MABARI_DOMINANCE_WILLPOWER_BONUS, EngineConstants.PROPERTY_VALUE_MODIFIER);

          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectMabariDominance(xEffect eEffect)
     {
          // remove buff
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, EngineConstants.MABARI_DOMINANCE_STRENGTH_BONUS * -1.0f, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION, EngineConstants.MABARI_DOMINANCE_CONSTITUTION_BONUS * -1.0f, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER, EngineConstants.MABARI_DOMINANCE_WILLPOWER_BONUS * -1.0f, EngineConstants.PROPERTY_VALUE_MODIFIER);

          return EngineConstants.TRUE;
     }
}