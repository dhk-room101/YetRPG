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
     // effect_daze_h
     // -----------------------------------------------------------------------------
     /*
         Effect Daze

             When applied to an object, this effect
             * applies the daze VFX
     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     /* @brief Returns an xEffect which dazes a creature
     * @author Georg Zoeller
     *
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_DAZE.
     */
     public xEffect EffectDaze()
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_DAZE);
          SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_VFX, 1133);
          return eEffect;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleApplyEffectDaze
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Georg Zoeller
     //  Created On: Jan 2007
     ///////////////////////////////////////////////////////////////////////////////
     //moved public const float EngineConstants.DAZE_ATTACK_PENALTY = -5.0f;
     //moved public const float EngineConstants.DAZE_DEFENSE_PENALTY = -5.0f;
     public int Effects_HandleApplyEffectDaze(xEffect eEffect)
     {
          // attack and defense debuff
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_ATTACK, EngineConstants.DAZE_ATTACK_PENALTY, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE, EngineConstants.DAZE_DEFENSE_PENALTY, EngineConstants.PROPERTY_VALUE_MODIFIER);

          return EngineConstants.TRUE;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleRemoveEffectDaze
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Georg Zoeller
     //  Created On: Jan 2007
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleRemoveEffectDaze(xEffect eEffect)
     {
          // remove attack and defense debuff
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_ATTACK, EngineConstants.DAZE_ATTACK_PENALTY * -1.0f, EngineConstants.PROPERTY_VALUE_MODIFIER);
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE, EngineConstants.DAZE_DEFENSE_PENALTY * -1.0f, EngineConstants.PROPERTY_VALUE_MODIFIER);

          return EngineConstants.TRUE;
     }
}