#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;

public class demo_module : MonoBehaviour
{

    //::///////////////////////////////////////////////
    //:: Module Template
    //:: Copyright (c) 2003 Bioware Corp.
    //:://////////////////////////////////////////////
    /*
        Module events
    */
    //:://////////////////////////////////////////////
    //:: Created By: Bryan Derksen
    //:: Created On: May 1 2009
    //:://////////////////////////////////////////////

    //# include "log_h"
    //# include "utility_h"
    //# include "wrappers_h"
    //# include "events_h"

    //#include "plt_gen00pt_generic_actions"
    //# include "plt_demo000pl_main"
    Engine engine { get; set; }
    xGameObjectBase oBase;
    int counter;
    //bool bStart;

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
        oBase = gameObject.GetComponent<xGameObjectBase>();
    }

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
        //GameObject oPC = engine.GetHero();
        //GameObject oParty = engine.GetParty(oPC);
        int bEventHandled = EngineConstants.FALSE;

        engine.Log_Events("", ev);

        switch (nEventType)
        {
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: The module starts. This can happen only once for a single
            //       game instance.
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_MODULE_START:
                {
                    //This sets a particular world map as being the current "primary" map.
                    //We only have one map in this module so we can set it once here and not
                    //need to worry about keeping track of it later.

                    /*GameObject oMapId = GetObjectByTag("demo000mp_world");
            WR_SetWorldMapPrimary(oMapId);

            //start character generation.
            PreloadCharGen();
            StartCharGen(GetHero(),0);*/
                    //TO DO loading screen

                    GameObject oArea = engine.CreateObject(EngineConstants.OBJECT_TYPE_AREA, xGameObjectMOD.instance.tArea, Vector3.zero);
                    engine.ParseArea(oArea, xGameObjectMOD.instance.tArea);

                    //Create the player, During debugging
                    engine.ParsePlayer(xGameObjectMOD.instance.player);

                    //Fire event preload,To move the camera at waypoint
                    xEvent evpr = engine.Event(EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT);
                    engine.SetEventCreatorRef(ref evpr, gameObject);
                    engine.SetEventObjectRef(ref ev, 0, oArea.gameObject);
                    engine.SignalEvent(oArea.gameObject, evpr);

                    engine.Warning("module start, Implement loading screen PLEASE!");
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: The module loads from a save game. This event can fire more than
            //       once for a single module or game instance.
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_MODULE_LOAD:
                {
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: A player enters the module
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_ENTER:
                {
                    GameObject oCreature = engine.GetEventCreatorRef(ref ev);
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: the player clicks on a destination in the world map
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_WORLD_MAP_USED:
                {
                    //int nFrom = GetEventInteger(ev, 0); // travel start location
                    //int nTo = GetEventInteger(ev, 1); // travel target location

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: the player clicks on a destination in the world map
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_BEGIN_TRAVEL:
                {
                    //string sSource = engine.GetEventStringRef(ref ev, 0); //area tag source location
                    //string sTarget = engine.GetEventStringRef(ref ev, 1); // area tag target location
                    //string sWPOverride = engine.GetEventStringRef(ref ev, 2); // waypoint tag override

                    //if you want to do any special-case code or random encounter handling, insert it here

                    /*if (sSource != sTarget)
                    {
                        //store target area's tag to a local module variable
                        SetLocalString(GetModule(), "WM_STORED_AREA", sTarget);
                        //store target waypoint tag
                        SetLocalString(GetModule(), "WM_STORED_WP", sWPOverride);
                        //initiate the map's travelling animation. The engine will
                        //send EngineConstants.EVENT_TYPE_WORLDMAP_PRETRANSITION once it's started.
                        WorldMapStartTravelling();
                    }*/

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: the world map has begun its "travelling" animation
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_WORLDMAP_PRETRANSITION:
                {
                    /*//retrieve the target area tag we stored in EngineConstants.EVENT_TYPE_BEGIN_TRAVEL
                    string sArea = GetLocalString(GetModule(), "WM_STORED_AREA");
        //retrieve the target waypoint tag
        string sWP = GetLocalString(GetModule(), "WM_STORED_WP");
                    //execute the area transition to that target.
                    UT_DoAreaTransition(sArea, sWP);*/

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: the world map has been called for, either via an area transition
            //       or via the player's GUI button
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_TRANSITION_TO_WORLD_MAP:
                {
                    //The world map's GUI status determines whether the player can use
                    //it for travel by clicking on destination map pins, or whether it
                    //only displays as an informational image.
                    //SetWorldMapGuiStatus(WM_GUI_STATUS_USE);
                    //Opens the map currently set as the primary map. See EngineConstants.EVENT_TYPE_MODULE_START
                    //for the code where we set which map this is.
                    //OpenPrimaryWorldMap();

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            //This event is sent by the engine whenever an item that has the variable
            //ITEM_SEND_ACQUIRED_EVENT set to true.
            case EngineConstants.EVENT_TYPE_CAMPAIGN_ITEM_ACQUIRED:
                {
                    //In any module of significant size there is likely to be more than one
                    //item that can send this event. We'll need to find the identity of
                    //the item that caused the event to be triggered and then decide what
                    //to do based on that.
                    GameObject oItem = engine.GetEventObjectRef(ref ev, 0);

                    if (engine.GetTag(oItem) == "demo200im_innkeepers_sword")
                    {
                        //The player has picked up the innkeeper's sword. Set the plot flag.
                        engine.WR_SetPlotFlag(EngineConstants.PLT_DEMO000PL_MAIN, EngineConstants.DEMO_SWORD_RECOVERED, EngineConstants.TRUE, EngineConstants.TRUE);
                    }

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            case EngineConstants.EVENT_TYPE_MODULE_AREA_TRANSITION:
                {
                    xGameObjectMOD.instance.bTransitioning = EngineConstants.FALSE;

                    GameObject oArea = engine.CreateObject(EngineConstants.OBJECT_TYPE_AREA, xGameObjectMOD.instance.tArea, Vector3.zero);
                    engine.ParseArea(oArea, xGameObjectMOD.instance.tArea);

                    //Fire event preload,To move the camera at waypoint
                    xEvent eval = engine.Event(EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT);
                    engine.SetEventCreatorRef(ref eval, gameObject);
                    engine.SetEventObjectRef(ref ev, 0, oArea.gameObject);
                    engine.SignalEvent(oArea.gameObject, eval);

                    GameObject w = GameObject.Find(xGameObjectMOD.instance.tWaypoint);

                    //Move player at starting waypoint
                    GameObject oHero = xGameObjectMOD.instance.oHero;
                    oHero.gameObject.transform.position = new Vector3(w.transform.position.x + 0.50f, w.transform.position.y + 0.10f, w.transform.position.z - 0.50f);
                    var rot = oHero.gameObject.gameObject.GetComponent<xGameObjectUTC>().orientation;
                    oHero.gameObject.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

                    //Move party members
                    foreach (var oPartyMember in engine.GetParty(engine.GetModule())
                        .GetComponent<xGameObjectPTY>().oPartyPool)
                    {
                        //Check if followers state is active
                        if (oPartyMember.GetComponent<xGameObjectUTC>().FOLLOWER_STATE == EngineConstants.FOLLOWER_STATE_ACTIVE)
                        {
                            oPartyMember.gameObject.transform.position = new Vector3(w.transform.position.x + 0.50f, w.transform.position.y + 0.10f, w.transform.position.z - 0.50f);
                            rot = oPartyMember.gameObject.gameObject.GetComponent<xGameObjectUTC>().orientation;
                            oPartyMember.gameObject.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
                        }
                    }

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
        }

        if (bEventHandled == EngineConstants.FALSE)
        {
            engine.Log_Events("Redirecting to module_core", ev);
            //engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_MODULE_CORE);
            oBase.bRedirected = EngineConstants.TRUE;
            //Put the event back in the queue
            engine.SignalEvent(gameObject, ev);
        }
    }
}
