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

public class trigger_core : MonoBehaviour
{
    //::///////////////////////////////////////////////
    //:: Trigger engine.Events Template
    //:: Copyright (c) 2003 Bioware Corp.
    //:://////////////////////////////////////////////
    /*
        Trigger events
    */
    //:://////////////////////////////////////////////
    //:: Created By: Yaron
    //:: Created On: Oct 29th, 2006
    //:://////////////////////////////////////////////

    //#include"log_h"
    //#include"utility_h"
    //#include"wrappers_h"
    //#include"events_h"

    Engine engine { get; set; }

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     public void SetLocalVarOnTeam(GameObject oCreature, string sTriggerTeamVar, string sVarToSetOnCreatures, int nValue)
     {
          int nTeam = engine.GetLocalInt(gameObject, sTriggerTeamVar);

          if (engine.IsFollower(oCreature) != EngineConstants.FALSE && nTeam > 0)
          {
               engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "trigger_core.SetLocalVarOnTeam", "Setting local var [" + sVarToSetOnCreatures + "]" + " on team: " + engine.IntToString(nTeam));
               // make sure it's done only once
               engine.SetLocalInt(gameObject, sVarToSetOnCreatures, -1);
               List<GameObject> arTeam = engine.GetTeam(nTeam);
               int nSize = engine.GetArraySize(arTeam);
               int i;
               GameObject oCurrent;
               for (i = 0; i < nSize; i++)
               {
                    oCurrent = arTeam[i];
                    if (engine.GetLocalInt(oCurrent, sVarToSetOnCreatures) == 0)
                         engine.SetLocalInt(oCurrent, sVarToSetOnCreatures, 1);
               }
          }
     }

     public void HandleEvent(xEvent ev)
     {
          //xEvent ev = engine.GetCurrentEvent();
          int nEventType = engine.GetEventTypeRef(ref ev);
          string sDebug;

          engine.Log_Events("", ev);

          switch (nEventType)
          {
               ////////////////////////////////////////////////////////////////////////
               // Sent by engine when trigger spawns into the game. Occurs once,
               // regardless of save games.
               ////////////////////////////////////////////////////////////////////////
               case EngineConstants.EVENT_TYPE_SPAWN:
                    {
                         break;
                    }
               ////////////////////////////////////////////////////////////////////////
               // Sent by engine when a creature enters the trigger
               ////////////////////////////////////////////////////////////////////////
               case EngineConstants.EVENT_TYPE_ENTER:
                    {
                         if (engine.GetObjectActive(gameObject) == EngineConstants.FALSE)
                         {
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "trigger_core.EngineConstants.EVENT_TYPE_ENTER", "Trigger inactive");
                              return;
                         }

                         GameObject oCreature = engine.GetEventCreatorRef(ref ev);
                         string sAT_WP = engine.GetLocalString(gameObject, EngineConstants.TRIGGER_AT_DEST_TAG);
                         string sAT_Area = engine.GetLocalString(gameObject, EngineConstants.TRIGGER_AT_DEST_AREA_TAG);
                         GameObject oPC = engine.GetMainControlled();

                         int nTeam = engine.GetLocalInt(gameObject, EngineConstants.TRIGGER_ENCOUNTER_TEAM);

                         if (oPC == oCreature)
                         {

                              if (sAT_WP != "none" && sAT_Area != "none"
                                     && sAT_WP != "" && sAT_Area != "") // an area transition
                              {
                                   if (engine.GetGameMode() == EngineConstants.GM_EXPLORE)
                                   {
                                        engine.UT_DoAreaTransition(sAT_Area, sAT_WP);
                                   }
                                   else
                                   {
                                        engine.UI_DisplayMessage(oCreature, EngineConstants.UI_MESSAGE_CANT_DO_IN_COMBAT);
                                   }
                              }

                              engine.UI_DisplayRoomEnterMessage(gameObject);
                         }

                         if (engine.IsFollower(oCreature) != EngineConstants.FALSE)
                         {
                              if (nTeam > 0)
                              {
                                   engine.UT_TeamAppears(nTeam);
                              }
                              //TrackTriggerEvent(nEventType, gameObject, oCreature);

                         }

                         // Activating team-help system on a team of creatures
                         SetLocalVarOnTeam(oCreature, EngineConstants.TRIGGER_ACTIVATE_TEAM_HELP, EngineConstants.AI_HELP_TEAM_STATUS, 1);

                         // Activate a team to be stationary (AI system)  - soft
                         SetLocalVarOnTeam(oCreature, EngineConstants.TRIGGER_ACTIVATE_TEAM_STATIONARY_SOFT, EngineConstants.AI_FLAG_STATIONARY, 1);

                         // Activate a team to be stationary (AI system) - hard
                         SetLocalVarOnTeam(oCreature, EngineConstants.TRIGGER_ACTIVATE_TEAM_STATIONARY_HARD, EngineConstants.AI_FLAG_STATIONARY, 2);

                         break;
                    }

               ////////////////////////////////////////////////////////////////////////
               // Sent by engine when creature exits the trigger
               ////////////////////////////////////////////////////////////////////////
               case EngineConstants.EVENT_TYPE_EXIT:
                    {
                         GameObject oCreature = engine.GetEventCreatorRef(ref ev);

                         break;
                    }

          }
     }
}