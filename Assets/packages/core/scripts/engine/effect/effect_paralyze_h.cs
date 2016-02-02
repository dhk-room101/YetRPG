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
     // effect_Paralyze_h
     // -----------------------------------------------------------------------------
     /*
         Effect Paralyze

             When applied to an object, this effect
             * applies the Paralyze VFX
     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"wrappers_h"

     /*
     @brief Returns an xEffect which Paralyzes a creature
     * @author Georg Zoeller
     *
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_Paralyze.
     */
     public xEffect EffectParalyze(int nVfx = 0)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_PARALYZE);

          if (nVfx > 0)
          {
               SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_VFX, nVfx);
          }

          return eEffect;
     }
}