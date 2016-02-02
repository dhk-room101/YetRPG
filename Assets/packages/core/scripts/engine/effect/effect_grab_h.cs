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
     // effect_grab_h
     // -----------------------------------------------------------------------------
     /*
         Grab effects

             Grab consists of two effects: EffectGrabbing() and EffectGrabbed().
             EffectGrabbing() is applied to the grabber.
             EffectGrabbed() is applied to the grabbee (i.e. the victim).

     */
     // -----------------------------------------------------------------------------
     // Owner:  Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"effect_constants_h"
     //#include"wrappers_h"

     /* -----------------------------------------------------------------------------
     * @brief Returns an xEffect of grabbing a creature.
     *
     * Constructor for the grabbing effect. When applied to an object, this effect
     * causes the GameObject to grab a victim. This xEffect works in tandem with
     * EffectGrabbed() which is applied to the victim.
     *
     * @param oVictim     � The GameObject being grabbed.
     *
     * @return A valid xEffect of type EngineConstants.EFFECT_TYPE_GRABBING.
     * -----------------------------------------------------------------------------**/
     public xEffect EffectGrabbing(GameObject oVictim, int nType = EngineConstants.EFFECT_TYPE_GRABBING)
     {
          xEffect eEffect = Effect(nType);
        SetEffectObjectRef(ref eEffect, 1, oVictim);
        SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_DISABLE_PHYSICS, EngineConstants.TRUE);
          return eEffect;
     }

     /* -----------------------------------------------------------------------------
     * @brief Returns an xEffect of being grabbed by a creature.
     *
     * Constructor for the grabbed effect. When applied to an object, this effect
     * causes the GameObject to be grabbed. This xEffect works in tandem with
     * EffectGrabbing() which is applied to the grabber.
     *
     * @param oGrabber  � The GameObject doing the grabbing.
     *
     * @return A valid xEffect of type EngineConstants.EFFECT_TYPE_GRABBED.
     * -----------------------------------------------------------------------------**/
     public xEffect EffectGrabbed(int nType = EngineConstants.EFFECT_TYPE_GRABBED)
     {
          xEffect eEffect = Effect(nType);
        SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_DISABLE_PHYSICS, EngineConstants.TRUE);
          return eEffect;
     }
}