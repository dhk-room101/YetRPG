//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sys_treasure_core : MonoBehaviour
{
    // -----------------------------------------------------------------------------
    // sys_treasure
    // -----------------------------------------------------------------------------
    /*
        treasure system

        The treasure system creates random treasure items on container objects.

            *** TEMP SYSTEM ***

            This is a temporary system for the purpose of the september release.



    */
    // -----------------------------------------------------------------------------
    // owner: georg zoeller
    // -----------------------------------------------------------------------------


    //#include "xEvents_h"
    //#include "log_h"
    //#include "sys_treasure_h"

    Engine engine { get; set; }

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     public void HandleEvent(xEvent ev)
     {

          //xEvent ev = engine.GetCurrentEvent();

          int nEventType = engine.GetEventTypeRef(ref ev);
          string sDebug;

#if DEBUG
          engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Received xEvent " + engine.ToString(nEventType), gameObject);
#endif

          switch (nEventType)
          {
               // placeables only
               case EngineConstants.EVENT_TYPE_SPAWN:
#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, engine.GetCurrentScriptName(), "Generating Treasure - SPAWN", gameObject);
#endif

                    if (engine.GetPlaceableBaseType(gameObject) != EngineConstants.PLACEABLE_TYPE_BAG)
                    {
                         engine.TreasureGenerate(gameObject);
                    }
                    break;

               // creatures only
               case EngineConstants.EVENT_TYPE_DYING:

                    if (engine.IsSummoned(gameObject) == EngineConstants.FALSE)
                    {
#if DEBUG
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, engine.GetCurrentScriptName(), "Generating Treasure - DYING", gameObject);
#endif
                         engine.TreasureGenerate(gameObject);
                    }
                    break;
          }


     }
}