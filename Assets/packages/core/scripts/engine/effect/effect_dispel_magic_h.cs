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
     // effect_dispel_magic
     // -----------------------------------------------------------------------------
     /*
         Effect: Effect Dispel Magic

         */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"events_h"
     //#include"effect_constants_h"
     //#include"attributes_h"

     public xEffect EffectDispelMagic(int nNoSpells = 1)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_DISPEL_MAGIC);
        SetEffectIntegerRef(ref eEffect, 0, nNoSpells);
          return eEffect;
     }

     public int Effects_HandleApplyEffectDispelMagic(xEffect eEffect)
     {
          int nNoSpells = GetEffectIntegerRef(ref eEffect, 0);

          // -------------------------------------------------------------------------
          // First Remove all Upkeep Effects.
          // -------------------------------------------------------------------------
          List<xEffect> arSpell = GetEffects(gameObject, EngineConstants.EFFECT_TYPE_UPKEEP);
          int nSize = GetArraySize(arSpell);
          int i;
          int nId;
          for (i = 0; i < nSize; i++)
          {
            xEffect _effect = arSpell[i];
               nId = GetEffectAbilityIDRef(ref _effect);

               // ---------------------------------------------------------------------
               // We only dispell EngineConstants.ABILITY_TYPE_SPELL!
               // ---------------------------------------------------------------------
               if (Ability_GetAbilityType(nId) == EngineConstants.ABILITY_TYPE_SPELL)
               {
                    RemoveEffect(gameObject, arSpell[i]);
               }
          }

          // -------------------------------------------------------------------------
          // Second Remove all other spell effects
          // -------------------------------------------------------------------------

          arSpell = GetEffects(gameObject);
          nSize = GetArraySize(arSpell);
          for (i = 0; i < nSize; i++)
          {
            xEffect _effect = arSpell[i];
               nId = GetEffectAbilityIDRef(ref _effect);
               // ---------------------------------------------------------------------
               // We only dispell EngineConstants.ABILITY_TYPE_SPELL!
               // ---------------------------------------------------------------------
               if (Ability_GetAbilityType(nId) == EngineConstants.ABILITY_TYPE_SPELL)
               {
                    RemoveEffect(gameObject, arSpell[i]);
               }
          }

          return EngineConstants.TRUE;

     }

     public int Effects_HandleRemoveEffectDispelMagic(xEffect eEffect)
     {
          int nAttribute = GetEffectIntegerRef(ref eEffect, 0);

          return EngineConstants.TRUE;
     }
}