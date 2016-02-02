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
     // effect_root_h
     // -----------------------------------------------------------------------------
     /*
         Effect Root

         When applied to an object, this applies xEffect root to that object. It also
         increments the stationary AI counter of that GameObject by 2. When removed, it
         decrements that counter. This should allow creature AI to handle the root
         with issue, and will handle the case of creatures being manually set to
         stationary who are then affected by this. The reason this value is set to
         2 is so it bypasses the existing functionality of the counter being set to
         1. When the stationary ai flag is 1, the creature will resume moving when
         hit. Incrementing and decrementing by 2 bypasses this, preserving its effect.
     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     /* @brief Returns an xEffect which Paralyzes a creature
     * @author Georg Zoeller
     *
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_Paralyze.
     */

     //#include"effect_constants_h"
     //#include"log_h"
     public xEffect EffectRoot()
     {
          return Effect(EngineConstants.EFFECT_TYPE_ROOT);
     }

     public int Effects_HandleApplyEffectRoot(xEffect eEffect)
     {
          // increment stationary counter
          int nStationaryFlag = GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY);
          LogTrace(EngineConstants.LOG_CHANNEL_EFFECTS, "Stationary AI Flag = " + ToString(nStationaryFlag), gameObject);
          nStationaryFlag += 2;
          LogTrace(EngineConstants.LOG_CHANNEL_EFFECTS, "Altered Stationary AI Flag = " + ToString(nStationaryFlag), gameObject);
          SetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY, nStationaryFlag);

          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectRoot(xEffect eEffect)
     {
          // decrement stationary counter
          int nStationaryFlag = GetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY);
          LogTrace(EngineConstants.LOG_CHANNEL_EFFECTS, "Stationary AI Flag = " + ToString(nStationaryFlag), gameObject);
          nStationaryFlag -= 2;

          // make sure it doesn't go below 0
          if (nStationaryFlag < 0)
          {
               nStationaryFlag = 0;
          }

          LogTrace(EngineConstants.LOG_CHANNEL_EFFECTS, "Altered Stationary AI Flag = " + ToString(nStationaryFlag), gameObject);
          SetLocalInt(gameObject, EngineConstants.AI_FLAG_STATIONARY, nStationaryFlag);

          return EngineConstants.TRUE;
     }
}