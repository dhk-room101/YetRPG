#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class demo200tr_attack : MonoBehaviour
{

    //::///////////////////////////////////////////////
    //:: Trigger Events Template
    //:: Copyright (c) 2003 Bioware Corp.
    //:://////////////////////////////////////////////
    /*
        Trigger events
    */
    //:://////////////////////////////////////////////
    //:: Created By:
    //:: Created On:
    //:://////////////////////////////////////////////

    /*# include "log_h"
    # include "utility_h"
    # include "wrappers_h"
    # include "events_h"

    # include "demo_consts_h"*/

    Engine engine { get; set; }
    xGameObjectBase oBase;
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
        string sDebug;
        GameObject oPC = engine.GetHero();
        GameObject oParty = engine.GetParty(oPC);
        int nEventHandled = EngineConstants.FALSE;

        switch (nEventType)
        {
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: The GameObject spawns into the game. This can happen only once,
            //       regardless of save games.
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_SPAWN:
                {
                    nEventHandled = EngineConstants.TRUE;
                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: A creature enters the trigger
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_ENTER:
                {
                    GameObject oCreature = engine.GetEventCreatorRef(ref ev);

                    //If by some chance the creature that enters the trigger isn't the
                    //PC or a party member, don't trigger the conversation
                    if (engine.IsPartyMember(oCreature) != EngineConstants.FALSE)
                    {
                        //The triggerer is indeed a party member, so we need to trigger
                        //the confrontation conversation.

                        //The conversation function needs a speaker and a listener.
                        //In this case we're calling a conversation that doesn't have a specific
                        //"owner" so the identity of the speaker doesn't matter so much,
                        //and we're arbitrarily picking the bandit as the principle speaker
                        GameObject oBandit = engine.UT_GetNearestObjectByTag(oPC, EngineConstants.DEMO_BANDIT);

                        //This utility function causes the conversation resource to be
                        //started, with the given speaker and listener.
                        engine.UT_Talk(oBandit, oPC, EngineConstants.DEMO_CONFRONTATION_R);

                        // This DestroyObject is to prevent the trigger from firing again.
                        // Setting a trigger "inactive" doesn't prevent it from firing
                        // an enter event. If you need to deactivate a trigger and
                        // then reactivate it again later, you'll need to use a plot
                        // flag and a conditional statement like the IsPartyMember one
                        // above to make the trigger disregard enter events.

                        engine.Safe_Destroy_Object(gameObject, 0);

                    }

                    nEventHandled = EngineConstants.TRUE;
                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: A creature exits the trigger
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_EXIT:
                {
                    GameObject oCreature = engine.GetEventCreatorRef(ref ev);
                    nEventHandled = EngineConstants.TRUE;
                    break;
                }

        }
        
        if (nEventHandled == EngineConstants.FALSE)
        {
            //HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_TRIGGER_CORE);
            engine.Log_Events("Redirecting to Trigger_core", ev);
            oBase.bRedirected = EngineConstants.TRUE;
            //Put the event back in the queue
            engine.SignalEvent(gameObject, ev);
        }
    }
}
