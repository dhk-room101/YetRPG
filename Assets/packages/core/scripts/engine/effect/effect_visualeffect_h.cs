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
     // effect_visualaffect_h.nss
     // -----------------------------------------------------------------------------
     /*
         Returns a visual effect.

         Note that visual effects of different types require to be applied with
         different EngineConstants.EFFECT_DURATION_TYPE_* constants to work properly.

         Please consult VFX.xls for the valid duration types for each effect

     */
     // -----------------------------------------------------------------------------
     // Georg Zoeller@2007-03-23
     // -----------------------------------------------------------------------------

     //#include"effect_constants_h"
     //#include"core_h"

     /* @brief Returns a visual effect
     *
     * @ param nVfxId - Index from VFX.xls
     * @author Georg Zoeller
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_SEPLL_TARGET.
     */
     public xEffect EffectVisualEffect(int nVfxId)
     {

          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_VISUAL_EFFECT);

          // int(0) is the VFX id
          SetEffectIntegerRef(ref eEffect, 0, nVfxId);

          return eEffect;

     }

     // -----------------------------------------------------------------------------
     //              ENGINE EFFECT - SPECIAL CASE - Please Read
     // -----------------------------------------------------------------------------
     /*
         EngineConstants.EFFECT_TYPE_VISUALEFFECT is an engine effect, which means the Apply and
         Remove Events are handled in the engine. There are no events fired into
         scripting languge when visual effects are applied or removed.
     */
     // -----------------------------------------------------------------------------
     // -- Georg@2007-03-23
     // -----------------------------------------------------------------------------
}