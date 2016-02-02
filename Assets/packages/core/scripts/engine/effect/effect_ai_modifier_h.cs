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
     // effect_ai_modifier
     // -----------------------------------------------------------------------------
     /*
         Effect: AI Modifier

                 Used to apply modifiers taken into account by the game AI to the
                 creature

                 Example: EngineConstants.AI_MODIFIER_IGNORE - AI ignores the target as if it was
                                               not there

                 All these required manual integration into the game AI

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"2da_constants_h"
     //#include"wrappers_h"
     //#include"events_h"
     //#include"effect_constants_h"

     //moved public const int EngineConstants.AI_MODIFIER_IGNORE = 1;

     public xEffect EffectAIModifier(int nModifier)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_AI_MODIFIER);
          SetEffectIntegerRef(ref eEffect, 0, nModifier);
          return eEffect;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the application of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectAIModifier(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectAIModifier(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }

     public int Effects_HasAIModifier(GameObject oTarget, int nModifier)
     {

          if (IsObjectValid(oTarget) == EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          List<xEffect> eEffects = GetEffects(oTarget, EngineConstants.EFFECT_TYPE_AI_MODIFIER);

          // Note: If this ends up to be a performance liability, we can make
          //       the xEffect write a stat integer on the creature instead.

          // In general I expect this loop to have 1 or 2 entries tops.

          int nSize = GetArraySize(eEffects);
          int i;

          for (i = 0; i < nSize; i++)
          {
               xEffect _effect = eEffects[i];
               if (GetEffectIntegerRef(ref _effect, 0) == nModifier)
               {
                    return EngineConstants.TRUE;
               }
          }

          return EngineConstants.FALSE;

     }
}