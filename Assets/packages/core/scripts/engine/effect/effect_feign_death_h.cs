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
     // effect_feign_death
     // -----------------------------------------------------------------------------
     /*
         Effect for feign death.

         Implementation Notes:

         - Engages stealth without change in GameObject transparency.
             - Hostile creatures lose perception and threat.
             - AoE spells still affect target (makes sense).

         - 2da data disables most of character control and plays death anim.

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"effect_constants_h"
     //#include"log_h"

     public xEffect EffectFeignDeath()
     {
          return Effect(EngineConstants.EFFECT_TYPE_FEIGN_DEATH);
     }

     public int Effects_HandleApplyEffectFeignDeath(xEffect eEffect)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "Checking Feign Death");

          // -------------------------------------------------------------------------
          // Stealth and Feign Death don't like each other.
          // -------------------------------------------------------------------------
          if (GetStealthEnabled(gameObject) != EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          int nCount = GetArraySize(GetEffects(gameObject, EngineConstants.EFFECT_TYPE_FEIGN_DEATH));
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "Feign Death Count = " + ToString(nCount));

          if (nCount > 0)//!= 1)
          {

               return EngineConstants.FALSE;
          }
          else
          {

               SetStealthEnabled(gameObject, EngineConstants.TRUE);
          }
          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectFeignDeath(xEffect eEffect)
     {
          int nCount = GetArraySize(GetEffects(gameObject, EngineConstants.EFFECT_TYPE_FEIGN_DEATH));
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "Feign Death Count = " + ToString(nCount));

          if (nCount == 0)
          {
               SetStealthEnabled(gameObject, EngineConstants.FALSE);
          }

          return EngineConstants.TRUE;
     }
}