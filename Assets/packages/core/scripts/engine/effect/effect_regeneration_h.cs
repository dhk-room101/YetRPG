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
     //#include"effect_constants_h"
     //#include"log_h"

     public xEffect EffectRegeneration(int nType)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_REGENERATION);

          if (nType != 0)
          {
               SetEffectIntegerRef(ref eEffect, 0, nType);
          }
          else
          {
               Warning("Invalid parameter passed to EffectRegeneration. -- contact georg");
          }

          return eEffect;

     }

     public void _ReadAndApplyRegenerationEffect(int nType, int bApply = EngineConstants.TRUE)
     {

          float fModifier = (bApply != EngineConstants.FALSE) ? 1.0f : -1.0f;

          int nProp1 = GetM2DAInt(EngineConstants.TABLE_EFFECT_REGENERATION, "property1", nType);
          int nProp2 = GetM2DAInt(EngineConstants.TABLE_EFFECT_REGENERATION, "property2", nType);

          float fVal1 = GetM2DAFloat(EngineConstants.TABLE_EFFECT_REGENERATION, "modifier1", nType);
          float fVal2 = GetM2DAFloat(EngineConstants.TABLE_EFFECT_REGENERATION, "modifier2", nType);

          if (nProp1 != 0)
          {
               UpdateCreatureProperty(gameObject, nProp1, fVal1 * fModifier, EngineConstants.PROPERTY_VALUE_MODIFIER);
          }
          if (nProp2 != 0)
          {
               UpdateCreatureProperty(gameObject, nProp2, fVal2 * fModifier, EngineConstants.PROPERTY_VALUE_MODIFIER);
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "EffectRegeneration", "Modifying properties " + ToString(nProp1) + "," + ToString(nProp2) +
                                                              " by " + ToString(fVal1 * fModifier) + "," + ToString(fVal2 * fModifier));

     }

     public int Effects_HandleApplyEffectRegeneration(xEffect eEffect)
     {
          int nType = GetEffectIntegerRef(ref eEffect, 0);

          if (nType != 0)
          {
               _ReadAndApplyRegenerationEffect(nType, EngineConstants.TRUE);
          }
          else
          {
               Log_Trace_Scripting_Error("effect_regeneration_h.Apply", "Type was 0 for regeneration effect");
               return EngineConstants.FALSE;
          }

          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectRegeneration(xEffect eEffect)
     {
          int nType = GetEffectIntegerRef(ref eEffect, 0);

          if (nType != 0)
          {
               _ReadAndApplyRegenerationEffect(nType, EngineConstants.FALSE);
          }

          return EngineConstants.TRUE;
     }
}