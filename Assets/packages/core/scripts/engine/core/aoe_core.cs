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

public class aoe_core : MonoBehaviour
{
    Engine engine { get; set; }
     //#include"log_h"
     void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     public void HandleEvent(xEvent ev)
     {
          //xEvent ev = engine.GetCurrentEvent();
          int nEventType = engine.GetEventTypeRef(ref ev);

#if DEBUG
          engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), engine.Log_GetEventNameById(nEventType), gameObject);
#endif

          switch (nEventType)
          {

               case EngineConstants.EVENT_TYPE_ENTER:
                    {
                         int nAbility = engine.GetEventIntegerRef(ref ev, 0);
                         GameObject oTarget = engine.GetEventTargetRef(ref ev);
                         GameObject oCreator = engine.GetEventCreatorRef(ref ev);

#if DEBUG
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "ENTER" + engine.ToString(oTarget), oCreator);
#endif
                         break;
                    }
               case EngineConstants.EVENT_TYPE_EXIT:
                    {
                         int nAbility = engine.GetEventIntegerRef(ref ev, 0);
                         GameObject oTarget = engine.GetEventTargetRef(ref ev);
                         GameObject oCreator = engine.GetEventCreatorRef(ref ev);

#if DEBUG
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "EXIT " + engine.ToString(oTarget), oCreator);
#endif
                         break;
                    }
          }

     }
}