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
     // -----------------------------------------------------------------------------                                                                                                     // -----------------------------------------------------------------------------
     // effect_stealth
     // -----------------------------------------------------------------------------
     /*

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"sys_stealth_h"

     public xEffect EffectStealth()
     {

          return Effect(EngineConstants.EFFECT_TYPE_STEALTH);
     }

     public int Effects_HandleApplyEffectStealth(xEffect eEffect)
     {

          // -------------------------------------------------------------------------
          // Safety for Stealth flag
          // -------------------------------------------------------------------------
          /* if (GetStealthEnabled(gameObject))
           {
               return EngineConstants.FALSE;
           }*/

          xEffect eTransparent = Effect(EngineConstants.EFFECT_TYPE_ALPHA);
          float fPotency = 0.0f;

          // -------------------------------------------------------------------------
          // Party members only become partially invisible
          // -------------------------------------------------------------------------
          if (IsPartyMember(gameObject) != EngineConstants.FALSE)
          {
               fPotency = 0.5f;
          }
          else
          {
               // --------------------------------------------------------------------
               // Non party members are not interactive anymore
               // --------------------------------------------------------------------
               SetObjectInteractive(gameObject, EngineConstants.FALSE);
          }

          SetEffectEngineFloatRef(ref eTransparent, EngineConstants.EFFECT_FLOAT_POTENCY, fPotency);
          Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eTransparent, gameObject, 0.0f, gameObject, GetEffectAbilityIDRef(ref eEffect));
          SetStealthEnabled(gameObject, EngineConstants.TRUE);

          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectStealth(xEffect eEffect)
     {

          SetStealthEnabled(gameObject, EngineConstants.FALSE);

          List<xEffect> aTransparent = GetEffects(gameObject, EngineConstants.EFFECT_TYPE_ALPHA, GetEffectAbilityIDRef(ref eEffect), gameObject, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT);
          int nSize = GetArraySize(aTransparent);
          int i;

          for (i = 0; i < nSize; i++)
          {
               RemoveEffect(gameObject, aTransparent[i]);
          }

          if (IsPartyMember(gameObject) == EngineConstants.FALSE)
          {
               //  ----------------------------------------------------------------
               //  This makes one assumption:
               //  None interactive objects are never using stealth, otherwise
               //  dropping out of stealth will modify their interactive status.
               //  ----------------------------------------------------------------
               SetObjectInteractive(gameObject, EngineConstants.TRUE);
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "effect_stealth", "Deactivating stealth flag");

          DEBUG_PrintToScreen("stealth enabled: " + ToString(GetStealthEnabled(gameObject)));

          return EngineConstants.TRUE;
     }
}