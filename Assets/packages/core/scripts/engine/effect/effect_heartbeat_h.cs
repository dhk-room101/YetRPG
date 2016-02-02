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
     // effect_heartbeat
     // -----------------------------------------------------------------------------
     /*

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"events_h"
     //#include"log_h"
     public xEffect EffectHeartbeat(float fRate, int nSubEvent)
     {
          xEffect e = Effect(EngineConstants.EFFECT_TYPE_HEARTBEAT);
        SetEffectIntegerRef(ref e, 0, nSubEvent);
          SetEffectFloatRef(ref e, 0, fRate);
          return e;
     }

     public int Effects_HandleApplyEffectHeartbeat(xEffect eEffect)
     {

          int nSubEvent = GetEffectIntegerRef(ref eEffect, 0);
          // Force a single instance per subtype
          List<xEffect> aHbs = GetEffects(gameObject, EngineConstants.EFFECT_TYPE_HEARTBEAT, 0, GetModule());
          int nSize = GetArraySize(aHbs);
          int i;

          int nFound = 0;
          int nEffectId = 0;
          for (i = 0; i < nSize; i++)
          {
            xEffect _effect = aHbs[i];
               if (GetEffectIntegerRef(ref _effect, 0) == nSubEvent)
               {
                    nFound++;
                    // return EngineConstants.FALSE;
               }

               if (nFound > 1) /*>1 because this xEffect is technically applied already, we just unapply it on failure */
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_SCRIPTERROR, "effect_heartbeat", "More than one HB xEffect with id " + ToString(nSubEvent) + " found on object");
                    return EngineConstants.FALSE;
               }

          }

          nEffectId = GetEffectIDRef(ref eEffect);

          float fRate = GetEffectFloatRef(ref eEffect, 0);

          xEvent ev = Event(EngineConstants.EVENT_TYPE_HEARTBEAT);
          SetEventFloatRef(ref ev, 0, fRate);
          SetEventIntegerRef(ref ev, 0, nSubEvent);
          SetEventIntegerRef(ref ev, 1, nEffectId);

          SignalEvent(gameObject, ev);

          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectHeartbeat(xEffect eEffect)
     {

          return EngineConstants.TRUE;
     }
}