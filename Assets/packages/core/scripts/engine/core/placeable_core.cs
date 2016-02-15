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

public class placeable_core : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////
    //  placeable_core
    //  Copyright � 2007 BioWare Corp.
    ////////////////////////////////////////////////////////////////////////////////
    /*
        Default xEvent handler for placeable objects.
    */
    ////////////////////////////////////////////////////////////////////////////////

    //#include"placeable_h"

    Engine engine { get; set; }
    xGameObjectBase oBase;
    int counter;

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
            //Or there is no custom class to take precedence, or if the event got redirected
            (oBase.bCustom == EngineConstants.FALSE ||
            oBase.bRedirected == EngineConstants.TRUE))
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

    public void HandleEvent()
     {
          xEvent ev = engine.GetCurrentEvent();
          int nEvent = engine.GetEventTypeRef(ref ev);

          engine.Log_Events("", ev, "[" + engine.ToString(gameObject) + "]", EngineConstants.LOG_CHANNEL_EVENTS_PLACEABLES);

          switch (nEvent)
          {
               //---------------------------------------------------------------------
               //  Sent by engine when the placeable spawns into the game.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_SPAWN:
                    engine.Placeable_HandleSpawned(ev);
                    break;

               //----------------------------------------------------------------------
               // Sent by engine when player clicks on object.
               //----------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_PLACEABLE_ONCLICK:
                    engine.Placeable_HandleClicked(ev);
                    break;

               //---------------------------------------------------------------------
               //  Sent by engine when creature clicks on the placeable.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_USE:
                    engine.Placeable_HandleUsed(ev);
                    break;

               //---------------------------------------------------------------------
               //  Sent when placeable is initiating dialog as the main speaker.
               //  This can be triggered by the engine when a player clicks on the
               //  GameObject or by a script using the utility function engine.UT_Talk.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CONVERSATION:
                    engine.Placeable_HandleDialog(ev);
                    break;

               //---------------------------------------------------------------------
               //  Sent by engine when an item is added to or removed from the
               //  inventory of the placeble.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_INVENTORY_ADDED:
               case EngineConstants.EVENT_TYPE_INVENTORY_REMOVED:
                    engine.Placeable_HandleInventory(ev);
                    break;

               //---------------------------------------------------------------------
               // Sent by engine when placeable is attacked.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_ATTACKED:
                    engine.Placeable_HandleAttacked(ev);
                    break;

               //---------------------------------------------------------------------
               //  Sent by AI scripts when placeable suffers at least 1 point damage
               //  from a single attack.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_DAMAGED:
                    engine.Placeable_HandleDamaged(ev);
                    break;

               //---------------------------------------------------------------------
               // Sent by AI scripts when placeable GameObject is destroyed.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_DEATH:
                    engine.Placeable_HandleDeath(ev);
                    break;

               //---------------------------------------------------------------------
               // Sent by engine when placeable is hit by a spell.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CAST_AT:
                    engine.Placeable_HandleCastAt(ev);
                    break;

               //---------------------------------------------------------------------
               // Sent by engine when projectile fired by placeable hits something.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_ATTACK_IMPACT:
                    if (engine.Trap_GetType(gameObject) != EngineConstants.FALSE)
                         engine.Trap_HandleImpact(ev);
                    else
                         engine.Placeable_HandleImpact(ev);
                    break;

               //---------------------------------------------------------------------
               // Sent by engine when an xEffect is to be applied to the placeable.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_APPLY_EFFECT:
                    engine.Effects_HandleApplyEffect(ev);//DHK
                    break;

               //---------------------------------------------------------------------
               // Sent by engine when an xEffect is to be removed from the placeable
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_REMOVE_EFFECT:
                    engine.Effects_HandleRemoveEffect();
                    break;

               //---------------------------------------------------------------------
               // Sent by engine when a xCommand is completed.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_COMMAND_COMPLETE:
                    engine.Placeable_HandleCommandCompleted(ev);
                    break;

               //----------------------------------------------------------------------
               //  Sent by script when unlock attempt fails.
               //----------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_UNLOCK_FAILED:
                    {
                         engine.Placeable_HandleUnlockFailed(ev);
                         break;
                    }

               //----------------------------------------------------------------------
               //  Sent by script when placeable is unlocked.
               //----------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_UNLOCKED:
                    {
                         engine.Placeable_HandleUnlocked(ev);
                         break;
                    }

               //---------------------------------------------------------------------
               //  Sent by script when a door or container placeable is opened.
               //---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_OPENED:
                    {
                         // GameObject oUser = engine.GetEventObjectRef(ref ev, 0);
                         break;
                    }

               // ---------------------------------------------------------------------
               // Sent by script to change active state.
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE:
                    {
                         int bActive = engine.GetEventIntegerRef(ref ev, 0);
                         engine.WR_SetObjectActive(gameObject, bActive);
                         break;
                    }

               // ---------------------------------------------------------------------
               // Sent by script to change interactive state.
               // ---------------------------------------------------------------------        
               case EngineConstants.EVENT_TYPE_SET_INTERACTIVE:
                    {
                         int bInteractive = engine.GetEventIntegerRef(ref ev, 0);
                         engine.SetObjectInteractive(gameObject, bInteractive);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by script to request that a trap arm itself.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_TRAP_ARM:
                    {
                         engine.Trap_HandleEventArm(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by trap to its signal target(s) when it is armed.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ARMED:
                    {
                         engine.Trap_HandleEventTriggerArmed(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by script to request that a trap disarm itself.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_TRAP_DISARM:
                    {
                         engine.Trap_HandleEventDisarm(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by script to trap's signal target(s) when trap is disarmed.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_TRAP_TRIGGER_DISARMED:
                    {
                         engine.Trap_HandleEventTriggerDisarmed(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by script to request that a trap's signal target(s) reset/retract.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_TRAP_RESET:
                    {
                         engine.Trap_HandleEventReset(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by engine when a creature enters the area of xEffect 'trigger'
               // associated with a trap placeable.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_ENTER:
                    {
                         engine.Trap_HandleEventEnter(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by trap's area of xEffect 'trigger' to the trap's signal target(s).
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ENTER:
                    {
                         engine.Trap_HandleEventTriggerEntered(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by engine when a creature leaves the area of xEffect attached to
               // a trap placeable.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_EXIT:
                    {
                         engine.Trap_HandleEventExit(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by trap's area of xEffect 'trigger' to the trap's signal target(s).
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_TRAP_TRIGGER_EXIT:
                    {
                         engine.Trap_HandleEventTriggerExit(ev);
                         break;
                    }

               //----------------------------------------------------------------------
               // Sent by engine when placeable strikes a target (eg. swinging log trap).
               //----------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_TRAP_TRIGGERED:
                    {
                         engine.Trap_HandleEventTriggered(ev);
                         break;
                    }

               // ---------------------------------------------------------------------
               // Sent by engine when player collides with GameObject (if EnableCollisionEvent
               // column in placeables.xls is non-zero).
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_PLACEABLE_COLLISION:
                    {
                         engine.Placeable_PromptAreaTransition();
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by module_core script when player responds to area transition prompt.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_POPUP_RESULT:
                    {
                         engine.Placeable_HandlePopupResult(ev);
                         break;
                    }

               // --------------------------------------------------------------------
               // Sent by script to signal GameObject to destroy itself.
               // --------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_DESTROY_OBJECT:
                    {
                         engine.SetPlot(gameObject, EngineConstants.FALSE);
                         engine.Safe_Destroy_Object(gameObject, 0);
                         break;
                    }

               default:
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PLACEABLES, engine.GetCurrentScriptName(), engine.Log_GetEventNameById(nEvent) + " (" + engine.ToString(nEvent) + ") *** Unhandled xEvent ***");
                    break;
          }
     }
}