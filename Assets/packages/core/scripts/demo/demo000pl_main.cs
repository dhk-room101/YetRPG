#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class demo000pl_main : xPlotConditional
{
    //::///////////////////////////////////////////////
    //:: Plot Events Template
    //:: Copyright (c) 2003 Bioware Corp.
    //:://////////////////////////////////////////////
    /*
        Plot xEvents
    */
    //:://////////////////////////////////////////////
    //:: Created By: Bryan Derksen
    //:: Created On: May 5 2009
    //:://////////////////////////////////////////////

    /*# include "log_h"
    # include "utility_h"
    # include "wrappers_h"
    # include "plot_h"

    # include "plt_demo000pl_main"

    # include "demo_consts_h"*/
    Engine engine { get; set; }

    void Awake()
    {
        if (engine == null) engine = gameObject.GetComponent<Engine>();
    }

    public override int StartingConditional(xEvent eParms)
    {
        //xEvent eParms = engine.GetCurrentEvent();                // Contains all input parameters
        int nType = engine.GetEventTypeRef(ref eParms);               // GET or SET call
        string strPlot = engine.GetEventStringRef(ref eParms, 0);         // Plot GUID
        int nFlag = engine.GetEventIntegerRef(ref eParms, 1);          // The bit flag # being affected
        GameObject oParty = engine.GetEventCreatorRef(ref eParms);      // The owner of the plot table for this script
        GameObject oConversationOwner = engine.GetEventObjectRef(ref eParms, 0); // Owner on the conversation, if any
        int nResult = EngineConstants.FALSE; // used to return value for DEFINED GET xEvents
        GameObject oPC = engine.GetHero();

        engine.plot_GlobalPlotHandler(eParms); // any global plot operations, including debug info

        if (nType == EngineConstants.EVENT_TYPE_SET_PLOT) // actions -&gt; normal flags only
        {
            int nValue = engine.GetEventIntegerRef(ref eParms, 2);        // On SET call, the value about to be written (on a normal SET that should be '1', and on a 'clear' it should be '0')
            int nOldValue = engine.GetEventIntegerRef(ref eParms, 3);     // On SET call, the current flag value (can be either 1 or 0 regardless if it's a set or clear xEvent)
                                                                          // IMPORTANT: The flag value on a SET xEvent is set only AFTER this script finishes running!
            switch (nFlag)
            {

                case EngineConstants.DEMO_QUEST_ACCEPTED:
                    {
                        //Gives the inkeeper's key to the player. The key itself
                        //is a "plot item", so we don't need to do anything fancy
                        //to prevent the player from dropping it - it'll go into the
                        //plot item section of his inventory until such time as we
                        //remove it using another script.
                        engine.UT_AddItemToInventory(EngineConstants.DEMO_INKEEPER_KEY_R);
                        break;
                    }

                case EngineConstants.DEMO_BACK_ROOM_OPENED:
                    {
                        //We don't want to blow up the barrel every time the lever's pulled,
                        //just the first time. So test if this flag's been set already.
                        if (engine.WR_GetPlotFlag(EngineConstants.PLT_DEMO000PL_MAIN, EngineConstants.DEMO_BACK_ROOM_OPENED) == EngineConstants.FALSE)
                        {
                            //Blow up the barrel and unlock the door
                            GameObject oDoor = engine.UT_GetNearestObjectByTag(oPC, EngineConstants.DEMO_KITCHEN_DOOR, EngineConstants.FALSE);
                            GameObject oBarrel = engine.UT_GetNearestObjectByTag(oPC, EngineConstants.DEMO_BARRIER_BARREL, EngineConstants.FALSE);
                            GameObject oExplosion = engine.UT_GetNearestObjectByTag(oPC, EngineConstants.DEMO_EXPLOSION_TARGET, EngineConstants.FALSE);

                            //triggers the blast VFX
                            engine.ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, engine.EffectVisualEffect(EngineConstants.VFX_FIREBALL_IMPACT), oExplosion);
                            //Makes the barrel "go away"
                            engine.SetObjectActive(oBarrel, EngineConstants.FALSE);
                            //Changes the door to its "destroyed" state, which makes it
                            //passable.
                            engine.DestroyPlaceable(oDoor);

                        }
                        break;
                    }

                case EngineConstants.DEMO_BANDIT_HOSTILE:
                    {
                        //This causes all members of the bandit's "team" (the bandit and
                        //the other bar patrons) to turn hostile. Since they're in the presence
                        //of the player already, they'll immediately percieve him and initiate
                        //combat.

                        engine.UT_TeamGoesHostile(EngineConstants.BANDIT_TEAM);
                        
                        //Turn hostile team red
                        List<GameObject> arTeam = engine.UT_GetTeam(EngineConstants.BANDIT_TEAM);
                        for (int nIndex = 0; nIndex < engine.GetArraySize(arTeam); nIndex++)
                        {
                            GameObject _member = arTeam[nIndex];
                            GameObject _sphere = _member.transform.Find("Sphere").gameObject;
                            _sphere.GetComponent<Renderer>().material.color = Color.red;
                        }
                        //During debug manually add the player to the bandits threat target
                        for (int nIndex = 0; nIndex < engine.GetArraySize(arTeam); nIndex++)
                        {
                            GameObject _member = arTeam[nIndex];
                            engine.SetEnemy(_member, engine.GetHero());
                        }
                        //During debug manually add the player to the bandits threat target
                        for (int nIndex = 0; nIndex < engine.GetArraySize(arTeam); nIndex++)
                        {
                            GameObject _member = arTeam[nIndex];
                            xEvent ev = engine.Event(EngineConstants.EVENT_TYPE_PERCEPTION_APPEAR);
                            engine.SetEventObjectRef(ref ev, 0, engine.GetHero());
                            engine.SetEventIntegerRef(ref ev, 0, EngineConstants.TRUE);//Hostile True
                            engine.SetEventIntegerRef(ref ev, 1, EngineConstants.FALSE);//Stealth false
                            engine.SetEventIntegerRef(ref ev, 2, EngineConstants.TRUE);//Hostility changed True
                            engine.SignalEvent(_member, ev);
                        }
                        
                        engine.WR_SetGameMode(EngineConstants.GM_COMBAT);
                        //end DHK

                        break;
                    }

                case EngineConstants.DEMO_BARKEEP_JOINED_PARTY:
                    {
                        //When the sword quest is done the barkeep will offer to join
                        //the party. If the offer is accepted, this code handles adding
                        //him.
                        GameObject oBarkeep = engine.UT_GetNearestCreatureByTag(oPC, EngineConstants.DEMO_BARKEEP);
                        //remove the plot-related properties, since they'd interfere
                        //with him functioning as a normal party member
                        engine.SetPlotGiver(oBarkeep, EngineConstants.FALSE);
                        engine.SetPlot(oBarkeep, EngineConstants.FALSE);
                        engine.UT_HireFollower(oBarkeep);
                        break;
                    }

                case EngineConstants.DEMO_SWORD_RETURNED:
                    {
                        //The player can't discard plot items on his own, so make sure to remove the
                        //sword here. Create a separate non-plot sword for the innkeeper
                        //to use and leave it in his inventory, the player will never know
                        //that the innkeeper had it all along.
                        engine.UT_RemoveItemFromInventory(EngineConstants.DEMO_INKEEPERS_SWORD_R);
                        //Once the sword quest is done we want to open up a new area
                        //on the map for the player.
                        GameObject oMapPinRoad = engine.GetObjectByTag(EngineConstants.DEMO_ROAD_MAP_PIN);
                        //engine.WR_SetWorldMapLocationStatus(oMapPinRoad, WM_LOCATION_ACTIVE);
                        break;
                    }

            }
        }
        else // EVENT_TYPE_GET_PLOT -&gt; defined conditions only
        {

            switch (nFlag)
            {
                case EngineConstants.DEMO_DECLINED_QUEST:
                    {
                        //This is a "defined" plot flag. When the plot is checked
                        //to see whether the flag is true or false, its status is
                        //determined using the following code.
                        if (
                            engine.WR_GetPlotFlag(EngineConstants.PLT_DEMO000PL_MAIN, EngineConstants.DEMO_TALKED_TO_BARKEEP) == EngineConstants.TRUE &&
                            engine.WR_GetPlotFlag(EngineConstants.PLT_DEMO000PL_MAIN, EngineConstants.DEMO_QUEST_ACCEPTED) == EngineConstants.FALSE
                    )
                        {
                            return EngineConstants.TRUE;
                        }
                        else
                        {
                            return EngineConstants.FALSE;
                        }
                    }
            }

        }

        engine.plot_OutputDefinedFlag(eParms, nResult);

        return nResult;
    }
}
