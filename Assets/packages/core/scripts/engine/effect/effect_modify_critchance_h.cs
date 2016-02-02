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
     // effect_modify_critchance
     // -----------------------------------------------------------------------------
     /*
         Modifies the critical chance of a character

         */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"events_h"
     //#include"effect_constants_h"
     //#include"attributes_h"

     public xEffect EffectModifyCritChance(int nCritType, float fChange)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_MODIFY_CRITCHANCE);
          SetEffectIntegerRef(ref eEffect, 0, nCritType);
          SetEffectFloatRef(ref eEffect, 0, fChange);
          return eEffect;
     }

     public int Effects_HandleApplyEffectModifyCritChance(xEffect eEffect)
     {
          int nProperty = GetEffectIntegerRef(ref eEffect, 0);
          float fChange = GetEffectFloatRef(ref eEffect, 0);

          UpdateCreatureProperty(gameObject, nProperty, fChange, EngineConstants.PROPERTY_VALUE_MODIFIER);

          //<Debug>
          float fNew = GetCreatureProperty(gameObject, nProperty, EngineConstants.PROPERTY_VALUE_TOTAL);
          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER_STATS, "New Critical Chance (" + IntToString(nProperty) + ") = " + FloatToString(fNew));
          // </Debug>
          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectModifyCritChance(xEffect eEffect)
     {
          int nProperty = GetEffectIntegerRef(ref eEffect, 0);
          float fChange = 0.0f - GetEffectFloatRef(ref eEffect, 0);

          UpdateCreatureProperty(gameObject, nProperty, fChange, EngineConstants.PROPERTY_VALUE_MODIFIER);

          //<Debug>
          float fNew = GetCreatureProperty(gameObject, nProperty, EngineConstants.PROPERTY_VALUE_TOTAL);
          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER_STATS, "New Critical Chance (" + IntToString(nProperty) + ") = " + FloatToString(fNew));
          //</Debug>

          return EngineConstants.TRUE;
     }
}