#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class demo100ar_wilderness : MonoBehaviour
{
    Engine engine { get; set; }
    xGameObjectBase oBase;

    //::///////////////////////////////////////////////
    //:: Area Core
    //:: Copyright (c) 2003 Bioware Corp.
    //:://////////////////////////////////////////////
    /*
        Handles global area events
    */
    //:://////////////////////////////////////////////
    //:: Created By: Yaron
    //:: Created On: July 17th, 2006
    //:://////////////////////////////////////////////

    /*# include "log_h"
    # include "utility_h"
    # include "wrappers_h"
    # include "events_h"
    # include "2da_constants_h"

    # include "plt_demo000pl_main"*/

    int counter;
    
    // Use this for initialization
    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
        oBase = gameObject.GetComponent<xGameObjectBase>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (bStart)
        counter++;
        if (10 - counter < 0)
        {
            //engine.Warning(" increment update " + counter);
            counter = 0;
            //If current events not null 
            if (oBase.qEvent != null &&
            oBase.qEvent.Count > 0 &&
            //As this script is already custom, we checked only to see 
            //if it needs to redirect the event to its parent core class
            oBase.bRedirected == EngineConstants.FALSE)
            {
                //and event is not type invalid
                if (oBase.qEvent[0].nType != EngineConstants.EVENT_TYPE_INVALID)
                {
                    //Do the obvious :-)
                    HandleEvent();
                }
            }
        }
    }

    void HandleEvent()
    {
        xEvent ev = engine.GetCurrentEvent();
        int nEventType = engine.GetEventTypeRef(ref ev);
        //string sDebug;
        GameObject oPC = engine.GetHero();
        GameObject oParty = engine.GetParty(oPC);
        int bEventHandled = EngineConstants.FALSE;

        engine.Log_Events("", ev);

        switch (nEventType)
        {
            ///////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: it is for playing things like cutscenes and movies when
            // you enter an area, things that do not involve AI or actual game play
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_AREALOAD_SPECIAL:
                {
                    //This event fires every time the area is loaded, but we only
                    //want to play the introductory cutscene once. Therefore we use this
                    //if statement and plot flag to check whether it's been played before.

                    if (engine.WR_GetPlotFlag(EngineConstants.PLT_DEMO000PL_MAIN, EngineConstants.DEMO_INTRO_COMPLETE) != EngineConstants.FALSE)
                    {
                        //CS_LoadCutscene(R"demo100ct_intro.cut");
                        engine.WR_SetPlotFlag(EngineConstants.PLT_DEMO000PL_MAIN, EngineConstants.DEMO_INTRO_COMPLETE, EngineConstants.TRUE); //sets the plot flag to ensure we don't repeat the intro cutscene.
                    }
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            ///////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: for things you want to happen while the load screen is still up,
            // things like moving creatures around
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT:
                {
                    //Reposition the camera on top of the start waypoint
                    GameObject c = GameObject.FindGameObjectWithTag("MainCamera");
                    c.transform.position = new Vector3(0, 10, 0);
                    c.transform.rotation = Quaternion.Euler(90, 0, 0);

                    GameObject m = GameObject.FindGameObjectWithTag("Module");
                    GameObject w = GameObject.Find(xGameObjectMOD.instance.tWaypoint);
                    c.transform.position = new Vector3(w.transform.position.x, w.transform.position.y + 25, w.transform.position.z);

                    engine.LoadingScreen(false);

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: fires at the same time that the load screen is going away,
            // and can be used for things that you want to make sure the player sees.
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_AREALOAD_POSTLOADEXIT:
                {
                    //TO DO remove the loading screen
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: A creature enters the area
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_ENTER:
                {
                    //GameObject oCreature = engine.GetEventCreatorRef(ref ev);
                    //GameObject oCreature = engine.GetEventObjectRef(ref ev, 0);
                    //GameObject a = GameObject.FindGameObjectWithTag("Area");
                    //engine.Warning("object " + oCreature.name + " has entered area " + a.name);
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: A creature exits the area
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_EXIT:
                {
                    GameObject oCreature = engine.GetEventCreatorRef(ref ev);

                    break;
                }
        }
        if (bEventHandled == EngineConstants.FALSE)
        {
            //engine.Warning("event not handled, redirecting to Area_core!");
            //engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_AREA_CORE);
            engine.Log_Events("Redirecting to Area_core", ev);
            oBase.bRedirected = EngineConstants.TRUE;
            //Put the event back in the queue
            engine.SignalEvent(gameObject, ev);
        }
    }
}
