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
     // effect_stun_h
     // -----------------------------------------------------------------------------
     /*
         Effect Stun

             When applied to an object, this effect
             * applies the stun effect
             */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     /* @brief Returns an xEffect which stuns a creature
     * @author Georg Zoeller
     *
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_STUN
     */

     //#include"effect_visualeffect_h"
     //#include"ui_h"

     //moved public const float _EFFECT_STUN_DEXTERITY_DEBUFF = -1000.0f;

     public xEffect EffectStun()
     {
          xEffect eStun = Effect(EngineConstants.EFFECT_TYPE_STUN);
          SetEffectEngineIntegerRef(ref eStun, EngineConstants.EFFECT_INTEGER_VFX, EngineConstants.VFX_CRUST_STUN);
          return eStun;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleApplyEffectStun
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Georg Zoeller
     //  Created On: Jan 2007
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleApplyEffectStun(xEffect eEffect)
     {

          if (GetArraySize(GetEffects(gameObject, EngineConstants.EFFECT_TYPE_STUN)) > 1  /*this includes this xEffect */)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "ApplyStun", "NOT applying stun, target already stunned!");
               return EngineConstants.FALSE;
          }
          else
          {
               UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_STUNNED);
          }

          // Stun debuffs dexterity. Note that we're subtracting a very large number here, but because the
          // property system confines the actual value between floor and ceiling, it will never drop below 1.
          // This is intended behavior.
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, EngineConstants._EFFECT_STUN_DEXTERITY_DEBUFF, EngineConstants.PROPERTY_VALUE_MODIFIER);

          return EngineConstants.TRUE;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleRemoveEffectStun
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Georg Zoeller
     //  Created On: Jan 2007
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleRemoveEffectStun(xEffect eEffect)
     {

          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, (EngineConstants._EFFECT_STUN_DEXTERITY_DEBUFF * -1), EngineConstants.PROPERTY_VALUE_MODIFIER);
          return EngineConstants.TRUE;
     }
}