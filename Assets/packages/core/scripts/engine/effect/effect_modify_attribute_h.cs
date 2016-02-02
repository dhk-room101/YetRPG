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
     // effect_modifystrengths.nss
     // -----------------------------------------------------------------------------
     /*
         Effect: Modify Attribute

         */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"events_h"
     //#include"effect_constants_h"
     //#include"attributes_h"

     //moved public const int EngineConstants.EFFECT_MODIFY_ATTRIBUTE_ATTRIBUTE_ALL = -1000;
     //moved public const int EngineConstants.EFFECT_MODIFY_ATTRIBUTE_ATTRIBUTE_RANDOM = -2000;

     public void _ChangeAttributeModifier(GameObject oCreature, int nAttribute, int nAmount)
     {
          //    Log_Systems("+++ changing attribute " + IntToString(nAttribute) + " by " + IntToString(nAmount));
          UpdateCreatureProperty(oCreature, nAttribute, IntToFloat(nAmount), EngineConstants.PROPERTY_VALUE_MODIFIER);
     }

     public xEffect EffectModifyAttribute(int nAttribute, int nValue)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_MODIFYATTRIBUTE);

          if (nAttribute == EngineConstants.EFFECT_MODIFY_ATTRIBUTE_ATTRIBUTE_RANDOM)
          {
               nAttribute = Engine_Random(6) + 1;
          }
        SetEffectIntegerRef(ref eEffect, 0, nAttribute);
        SetEffectIntegerRef(ref eEffect, 1, nValue);
          return eEffect;
     }

     public int Effects_HandleApplyEffectModifyAttribute(xEffect eEffect)
     {
          int nAttribute = GetEffectIntegerRef(ref eEffect, 0);
          int nAmount = GetEffectIntegerRef(ref eEffect, 1);

          if (nAttribute == EngineConstants.EFFECT_MODIFY_ATTRIBUTE_ATTRIBUTE_ALL)
          {
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC, nAmount);
          }
          else
          {
               _ChangeAttributeModifier(gameObject, nAttribute, nAmount);
          }

          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectModifyAttribute(xEffect eEffect)
     {
          int nAttribute = GetEffectIntegerRef(ref eEffect, 0);
          int nAmount = GetEffectIntegerRef(ref eEffect, 1) * -1;

          if (nAttribute == EngineConstants.EFFECT_MODIFY_ATTRIBUTE_ATTRIBUTE_ALL)
          {
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, nAmount);
               _ChangeAttributeModifier(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC, nAmount);
          }
          else
          {
               _ChangeAttributeModifier(gameObject, nAttribute, nAmount);

          }
          return EngineConstants.TRUE;
     }
}