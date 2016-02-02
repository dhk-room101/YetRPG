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
     // effect_modify_property
     // -----------------------------------------------------------------------------
     /*
         Generic Property Modification Effect.

         This xEffect modifies a creature property MODIFIER.

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"events_h"
     //#include"effect_constants_h"
     //#include"attributes_h"

     //moved public const int EngineConstants.EFFECT_MODIFY_PROPERTY_ATTRIBUTE_ALL = -1000;

     public void ModifyAllAttributes(float fChange)
     {
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, fChange, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, fChange, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION, fChange, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER, fChange, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE, fChange, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC, fChange, EngineConstants.PROPERTY_VALUE_MODIFIER);
     }

     // MGB - February 23, 2009
     // EffectModifyProperty Constructor moved into Engine.
     // All other functions moved into engine.

     // Georg: This is identical to xEffect modify property - it is just flagged as
     //        consider hostile in the 2da.
     public xEffect EffectDecreaseProperty(int nProperty0, float fChange0, int nProperty1 = 0, float fChange1 = 0.0f, int nProperty2 = 0, float fChange2 = 0.0f)
     {
          return EffectModifyPropertyHostile(nProperty0, fChange0, nProperty1, fChange1, nProperty2, fChange2);
     }

     public int Effects_HandleApplyEffectModifyProperty(xEffect eEffect)
     {
          ApplyEffectModifyProperty(eEffect);
          return EngineConstants.TRUE;

     }

     public int Effects_HandleRemoveEffectModifyProperty(xEffect eEffect)
     {
          RemoveEffectModifyProperty(eEffect);
          return EngineConstants.TRUE;
     }
}