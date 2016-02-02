#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
/*////////////////////////////////////////////////////////////////////////////////
//  Placeable Events Template
//  Copyright ┬⌐ 2007 Bioware Corp.
////////////////////////////////////////////////////////////////////////////////
/*
    Handles placeable events.
*/
////////////////////////////////////////////////////////////////////////////////

//#include "placeable_h"
//#include "plt_demo000pl_main"

public class demo200pl_security_lever : MonoBehaviour
{
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
        counter++;
        if (10 - counter < 0)
        {
            //engine.Warning(" increment update " + counter);
            counter = 0;
            //If current events not null
            if (oBase.qEvent != null &&
            oBase.qEvent.Count > 0)
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
        int bEventHandled = EngineConstants.FALSE;

        switch (nEventType)
        {
            //----------------------------------------------------------------------
            // Sent by engine when GameObject spawns in game. This happens once
            // regardless of save games.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_SPAWN:
                {
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when player clicks on GameObject.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_PLACEABLE_ONCLICK:
                {
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when a creature uses the placeable.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_USE:
                {
                    GameObject oUser = engine.GetEventCreatorRef(ref ev);
                    int nAction = engine.GetPlaceableAction(gameObject);
                    int nVariation = engine.GetEventIntegerRef(ref ev, 0);
                    int nActionResult = EngineConstants.TRUE;

                    switch (nAction)
                    {
                        case EngineConstants.PLACEABLE_ACTION_OPEN:
                            //nVariation = !nVariation; break;// Makes doors swing away from player
                            {
                                nVariation = (nVariation == EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
                                break;
                            }
                        case EngineConstants.PLACEABLE_ACTION_CLOSE:
                        case EngineConstants.PLACEABLE_ACTION_USE:
                        case EngineConstants.PLACEABLE_ACTION_AREA_TRANSITION:
                        case EngineConstants.PLACEABLE_ACTION_CONVERSATION:
                        case EngineConstants.PLACEABLE_ACTION_EXAMINE:
                        case EngineConstants.PLACEABLE_ACTION_TRIGGER_TRAP:
                        case EngineConstants.PLACEABLE_ACTION_DISARM:
                        case EngineConstants.PLACEABLE_ACTION_UNLOCK:
                        case EngineConstants.PLACEABLE_ACTION_OPEN_INVENTORY:
                        case EngineConstants.PLACEABLE_ACTION_FLIP_COVER:
                        case EngineConstants.PLACEABLE_ACTION_USE_COVER:
                        case EngineConstants.PLACEABLE_ACTION_LEAVE_COVER:
                        case EngineConstants.PLACEABLE_ACTION_TOPPLE:
                            {
                                // *** Handle custom placeable usage here. ***

                                //Trigger plot file that opens the back room door. The plot
                                //file's event script will handle all the details:
                                //the explosion VFX, removing the barrel, and breaking the door
                                engine.WR_SetPlotFlag(EngineConstants.PLT_DEMO000PL_MAIN, EngineConstants.DEMO_BACK_ROOM_OPENED, EngineConstants.TRUE, EngineConstants.TRUE);

                                // Set the new state of the placeable using nActionResult
                                // (TRUE means the action succeeded, FALSE means action failed).
                                if (bEventHandled == EngineConstants.TRUE)
                                {
                                    engine.SetPlaceableActionResult(gameObject, nAction, nActionResult, nVariation);
                                }
                                break;
                            }
                        case EngineConstants.PLACEABLE_ACTION_DESTROY:
                            {
                                //throw new Not()
                                break;
                            }
                    }
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine or scripting when this GameObject is initiating dialog as
            // the speaker either when a player clicks to talk to this GameObject or
            // a script initiates dialog with this GameObject
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_CONVERSATION:
                {
                    //GameObject oTarget = engine.GetEventObject(ev, 0);               // player or NPC to talk to.
                    //resource rConversationName = engine.GetEventResource(ev, 0); // conversation to use, "" for default
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when an item is added to the inventory of this GameObject.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_INVENTORY_ADDED:
                {
                    //GameObject oOwner = engine.GetEventCreator(ev);    // old owner of the item
                    //GameObject oItem  = engine.GetEventObject(ev, 0);  // item added
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when an item is removed from inventory of this GameObject.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_INVENTORY_REMOVED:
                {
                    //GameObject oOwner = engine.GetEventCreator(ev);    // old owner of the item
                    //GameObject oItem  = engine.GetEventObject(ev, 0);  // item added
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when a placeable trap strikes an GameObject.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_TRAP_TRIGGERED:
                {
                    //GameObject   oTarget = engine.GetEventObject(ev, 0);    // Target hit by trap
                    //location lImpact = engine.GetEventLocation(ev, 0);  // Impact location
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when this GameObject is attacked.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_ATTACKED:
                {
                    //GameObject oAttacker = engine.GetEventCreator(ev);
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by AI scripts when GameObject suffers 1 or more points of damage.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DAMAGED:
                {
                    //GameObject oDamager = engine.GetEventCreator(ev);
                    //int nDamage     = engine.GetEventInteger(ev, 0);
                    //int nDamageType = engine.GetEventInteger(ev, 1);
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by AI scripts when GameObject is destroyed.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DEATH:
                {
                    //GameObject oKiller = engine.GetEventCreator(ev);
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when GameObject is hit by a spell.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_CAST_AT:
                {
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when GameObject needs to have an effect applied to itself
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_APPLY_EFFECT:
                {
                    //effect eEffect = engine.GetCurrentEffect();    // effect to be applied
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when GameObject needs to have an effect removed from itself
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_REMOVE_EFFECT:
                {
                    //effect eEffect = engine.GetCurrentEffect();    // effect to be removed
                    break;
                }

            //----------------------------------------------------------------------
            //  Sent by script when unlock attempt fails.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_UNLOCK_FAILED:
                {
                    //GameObject oUser = engine.GetEventObject(ev, 0);
                    break;
                }

            //----------------------------------------------------------------------
            //  Sent by script when placeable is unlocked.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_UNLOCKED:
                {
                    //GameObject oUser = engine.GetEventObject(ev, 0);
                    break;
                }

            //---------------------------------------------------------------------
            //  Sent by script when a door or container placeable is opened.
            //---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_OPENED:
                {
                    // GameObject oUser = engine.GetEventObject(ev, 0);
                    break;
                }


            // ---------------------------------------------------------------------
            // Sent by engine when player collides with GameObject (if EnableCollisionEvent
            // column in placeables.xls is non-zero).
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_PLACEABLE_COLLISION:
                {
                    break;
                }
        }

        if (bEventHandled == EngineConstants.FALSE)
        {
            engine.Warning("event not handled, redirecting to Area_core!");
            engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_PLACEABLE_CORE);
        }
    }
}
