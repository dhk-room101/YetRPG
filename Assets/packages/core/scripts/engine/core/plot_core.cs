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

//simple template plot class
public class plot_core : xPlotConditional
{
    //::///////////////////////////////////////////////
    //:: Plot engine.Events Template
    //:: Copyright (c) 2003 Bioware Corp.
    //:://////////////////////////////////////////////
    /*
        Plot events
    */
    //:://////////////////////////////////////////////
    //:: Created By: Yaron
    //:: Created On: July 21st, 2006
    //:://////////////////////////////////////////////

    //#include"log_h"
    //#include"utility_h"
    //#include"wrappers_h"
    //#include"plot_h"

    ////#include"PLOT_NAME_GOES_HERE"

    Engine engine { get; set; }

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     public override int StartingConditional(xEvent eParms)
     {
          //xEvent eParms = engine.GetCurrentEvent();                // Contains all input parameters
          int nType = engine.GetEventTypeRef(ref eParms);               // GET or SET call
          string strPlot = engine.GetEventStringRef(ref eParms, 0);         // Plot GUID
          int nFlag = engine.GetEventIntegerRef(ref eParms, 1);          // The bit flag # being affected
          GameObject oParty = engine.GetEventCreatorRef(ref eParms);      // The owner of the plot table for this script
          GameObject oConversationOwner = engine.GetEventObjectRef(ref eParms, 0); // Owner on the conversation, if any
          int nPlotType = engine.GetEventIntegerRef(ref eParms, 5);
          int bIsTutorial = engine.GetM2DAInt(EngineConstants.TABLE_PLOT_TYPES, "IsTutorial", nPlotType);
          int bIsCodex = engine.GetM2DAInt(EngineConstants.TABLE_PLOT_TYPES, "IsCodex", nPlotType);
          int nResult = EngineConstants.FALSE; // used to return value for DEFINED GET events
          GameObject oPC = engine.GetHero();

          engine.plot_GlobalPlotHandler(eParms); // any global plot operations, including debug info

          if (nType == EngineConstants.EVENT_TYPE_SET_PLOT) // actions -> normal flags only
          {
               int nValue = engine.GetEventIntegerRef(ref eParms, 2);        // On SET call, the value about to be written (on a normal SET that should be '1', and on a 'clear' it should be '0')
               int nOldValue = engine.GetEventIntegerRef(ref eParms, 3);     // On SET call, the current flag value (can be either 1 or 0 regardless if it's a set or clear event)
                                                                                              // IMPORTANT: The flag value on a SET xEvent is set only AFTER this script finishes running!

               /*switch (nFlag)
               {

               }*/
          }
          else // EngineConstants.EVENT_TYPE_GET_PLOT -> defined conditions only
          {

               /*switch (nFlag)
               {

               }*/

          }

          engine.plot_OutputDefinedFlag(eParms, nResult);
          return nResult;
     }
}