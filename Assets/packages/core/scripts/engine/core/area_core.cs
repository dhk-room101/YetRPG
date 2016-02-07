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

public class area_core : MonoBehaviour
{
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

    //#include"log_h"
    //#include"utility_h"
    //#include"wrappers_h"
    //#include"events_h"
    //#include"2da_constants_h"
    //#include"design_tracking_h"
    //#include"world_maps_h"
    //#include"sys_areabalance"
    //#include"achievement_core_h"
    //moved public const string EngineConstants.FOLLOWER_DUP_POSTFIX = "_DUP";

    //moved public const string CODEX_PLOT_NAME_PREFIX = "CODEX_PLOT_NAME_";
    //moved public const string CODEX_PLOT_FLAG_PREFIX = "CODEX_PLOT_FLAG_";
    //moved public const int CODEX_MAX_ENTRIES_PER_AREA = 10;

    //moved public const int DEFAULT_BODY_DECAY_TIMER = 120000;
    //moved public const int QUICK_BODY_DECAY_TIMER = 5000;
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
          int nEventType = engine.GetEventTypeRef(ref ev);
          string sDebug;

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
                         // -----------------------------------------------------------------
                         // Georg: In the unlikely xEvent that there are dead party members
                         //        in the savegame, fix it.
                         // -----------------------------------------------------------------
                         engine.ResurrectPartyMembers();

                         // Hostility update
                         engine.SetGroupHostility(EngineConstants.GROUP_PC, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);
                         engine.SetGroupHostility(EngineConstants.GROUP_FRIENDLY, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);
                         engine.SetGroupHostility(EngineConstants.GROUP_PC, EngineConstants.GROUP_NEUTRAL, EngineConstants.FALSE);
                         engine.SetGroupHostility(EngineConstants.GROUP_FRIENDLY, EngineConstants.GROUP_NEUTRAL, EngineConstants.FALSE);
                         //TrackPartyAreaEvent(gameObject,nEventType);
                         // Play music
                         string sMusic = engine.GetM2DAString(EngineConstants.TABLE_AREA_MUSIC, engine.GetTag(gameObject), 1);
                         if (sMusic == "")
                              sMusic = engine.GetM2DAString(EngineConstants.TABLE_AREA_MUSIC, "default", 1);
                         engine.PlayMusic(sMusic);

                         //TrackPartyAreaEvent(gameObject,nEventType);
                         engine.WM_SetWorldMapGuiStatus();

                         engine.WM_SetPartyPickerGuiStatus();

                         // Clearing dialog override: (just in case)
                         engine.SetLocalInt(engine.GetModule(), EngineConstants.PARTY_OVERRIDE_CONVERSATION_ACTIVE, EngineConstants.FALSE);

                         // Activate world map flavor note
                         string sLocationTag = engine.GetLocalString(gameObject, EngineConstants.WORLD_MAP_NOTE_TAG);
                         if (sLocationTag != "")
                         {
                              string sWorldMapTag = engine.GetLocalString(gameObject, EngineConstants.WORLD_MAP_TAG);
                              if (sWorldMapTag != "")
                              {
                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "area_core", "Activating as flavor world map note");
                                   GameObject oWorldMap = engine.GetObjectByTag(sWorldMapTag);
                                   GameObject oLocation = engine.GetObjectByTag(sLocationTag);
                                   if (engine.IsObjectValid(oWorldMap) != EngineConstants.FALSE)
                                        engine.WR_SetWorldMapLocationStatus(oLocation, EngineConstants.WM_LOCATION_GRAYED_OUT);

                              }
                         }

                         break;
                    }
               ///////////////////////////////////////////////////////////////////////
               // Sent by: The engine
               // When: for things you want to happen while the load screen is still up,
               // things like moving creatures around
               ////////////////////////////////////////////////////////////////////////
               case EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT:
                    {
                    //Added by DHK
                    //Reposition the camera on top of the start waypoint
                    GameObject c = GameObject.FindGameObjectWithTag("MainCamera");
                    GameObject m = GameObject.FindGameObjectWithTag("Module");
                    GameObject w = GameObject.Find(xGameObjectMOD.instance.tWaypoint);
                    c.transform.position = new Vector3(w.transform.position.x, w.transform.position.y + 25, w.transform.position.z);
                    //End comment DHK

                    //Original code
                    List<GameObject> arParty = engine.GetPartyPoolList();
                         int i = 0;
                         int nSize = engine.GetArraySize(arParty);
                         GameObject oFollower;
                         for (i = 0; i < nSize; i++)
                         {
                              oFollower = arParty[i];
                              engine.SetFollowPartyLeader(oFollower, EngineConstants.TRUE);
                              engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "area_core", "Gore reset to 0.0f on follower: " + engine.GetTag(oFollower));
                              engine.Gore_RemoveAllGore(oFollower);
                         }

                         // setting body decay timer
                         int nAreaId = engine.GetLocalInt(gameObject, EngineConstants.AREA_ID);
                         int nFastBodyFade = engine.GetM2DAInt(EngineConstants.TABLE_AREA_DATA, "FastBodyDecay", nAreaId);
                         int nDecay = EngineConstants.DEFAULT_BODY_DECAY_TIMER;
                         if (nFastBodyFade != EngineConstants.FALSE)
                              nDecay = EngineConstants.QUICK_BODY_DECAY_TIMER;

                         engine.SetCreaturesGlobalMaxTimeBeforeDecay(nDecay);

                         // re-activating party banter trigger (picking 1 trigger, assuming very few areas have more than a single trigger)
                         GameObject oBanterTrigger = engine.GetObjectByTag("gentr_party_banter");
                         if (engine.IsObjectValid(oBanterTrigger) != EngineConstants.FALSE)
                              engine.WR_SetObjectActive(oBanterTrigger, EngineConstants.TRUE);

                         engine.RemoveAllSummons(EngineConstants.TRUE);

                         break;
                    }
               ////////////////////////////////////////////////////////////////////////
               // Sent by: The engine
               // When: fires at the same time that the load screen is going away,
               // and can be used for things that you want to make sure the player sees.
               ////////////////////////////////////////////////////////////////////////
               case EngineConstants.EVENT_TYPE_AREALOAD_POSTLOADEXIT:
                    {
                         GameObject oPlayer = engine.GetEventCreatorRef(ref ev);

                         //TrackPartyAreaEvent(gameObject,nEventType);

                         // Make sure all followers are following the player

                         // area notifiction
                         int nID = engine.GetLocalInt(gameObject, EngineConstants.AREA_ID);
                         int nShowNotification = engine.GetM2DAInt(EngineConstants.TABLE_AREA_DATA, "UnlockNotification", nID);
                         if (nShowNotification > 0 && engine.GetLocalInt(gameObject, EngineConstants.AREA_NOTIFICATION_SHOWN) == 0)
                         {
                              engine.SetLocalInt(gameObject, EngineConstants.AREA_NOTIFICATION_SHOWN, 1);
                              engine.ShowAreaUnlockedNotification(engine.GetName(gameObject));
                         }

                         // set Codex plots
                         int j;
                         string sCodexPlotName;
                         int nCodexFlag;
                         for (j = 1; j <= EngineConstants.CODEX_MAX_ENTRIES_PER_AREA; j++)
                         {
                              sCodexPlotName = engine.GetLocalString(gameObject, EngineConstants.CODEX_PLOT_NAME_PREFIX + engine.IntToString(j));
                              nCodexFlag = engine.GetLocalInt(gameObject, EngineConstants.CODEX_PLOT_FLAG_PREFIX + engine.IntToString(j));
                              if (sCodexPlotName != "") engine.WR_SetPlotFlag(sCodexPlotName, nCodexFlag, EngineConstants.TRUE);
                         }

                         //DEBUG_ConsoleCommand("spawn tools_ambient");

                         engine.UI_DisplayAreaEnterMessage();

                         //string sTag = engine.GetTag(gameObject);
                         //if(StringLeft(sTag, 3) != "pre" || sTag == "default_start_area") // not a prelude area
                         //{
                         // disable bloom
                         //    DEBUG_ConsoleCommand("FBDisableEffect da_bloom");

                         //}

                         // -----------------------------------------------------------------
                         // Entering an area grants a certain amount of exploration EngineConstants.XP the
                         // first time.
                         // -----------------------------------------------------------------
                         if (engine.GetLocalInt(gameObject, EngineConstants.ENTERED_FOR_THE_FIRST_TIME) == EngineConstants.FALSE)
                         {
                              int nAreaID = engine.GetLocalInt(gameObject, EngineConstants.AREA_ID);
                              int nXP = engine.GetM2DAInt(EngineConstants.TABLE_AREA_DATA, "XPReward", nAreaID);
                              engine.RewardXPParty(nXP, EngineConstants.XP_TYPE_EXPLORE, null, gameObject);
                              engine.SetLocalInt(gameObject, EngineConstants.ENTERED_FOR_THE_FIRST_TIME, EngineConstants.TRUE);

                              // Check if the player has discovered a specially flagged area; if so, increment the count for such areas (and possibly grant the achievement)
                              // The flag is stored on the column labeled AchievementFlag in areadata.xls
                              engine.ACH_TravelerAchievement(EngineConstants.TABLE_AREA_DATA, nAreaID);

                              // Stats - increment percent of game world explored
                              //STATS_TrackExploredAreas(EngineConstants.TABLE_AREA_DATA, nAreaID);

                         }
                         else
                         {
#if DEBUG
                              engine.STATS_LogTrace("This area has already been explored.");
#endif
                         }

                         break;
                    }

               ////////////////////////////////////////////////////////////////////////
               // Sent by: The engine
               // When: A creature enters the area
               ////////////////////////////////////////////////////////////////////////
               case EngineConstants.EVENT_TYPE_ENTER:
                    {
                         GameObject oCreature = engine.GetEventCreatorRef(ref ev);
                         // Georg: Disabled this, not actual use for it.

                         if (engine.IsFollower(oCreature) != EngineConstants.FALSE)
                         {

                              if (engine.IsHero(oCreature) != EngineConstants.FALSE)
                              {
                                   //TrackPartyAreaEvent(gameObject,nEventType);
                                   //TrackSendPositionUpdate(2, gameObject);

                              }

                         }

                         break;
                    }
               ////////////////////////////////////////////////////////////////////////
               // Sent by: The engine
               // When: A creature exits the area
               ////////////////////////////////////////////////////////////////////////
               case EngineConstants.EVENT_TYPE_EXIT:
                    {
                         GameObject oCreature = engine.GetEventCreatorRef(ref ev);

                         /*if (engine.IsHero(oCreature) != EngineConstants.FALSE)
                         {
                              //TrackPartyAreaEvent(gameObject,nEventType);
                         }*/
                         break;
                    }

               ////////////////////////////////////////////////////////////////////////
               // Sent by: The engine
               // When: fires at the same time that the load screen is going away,
               // but only when loading a savegame.
               ////////////////////////////////////////////////////////////////////////
               case EngineConstants.EVENT_TYPE_AREALOADSAVE_POSTLOADEXIT:
                    {
                         GameObject oPlayer = engine.GetEventCreatorRef(ref ev);

                         break;
                    }
          }

        //Outside the switch loop, assuming a break
        //In case the event was actually redirected, giveback control to the custom script
        if (oBase.bRedirected == EngineConstants.TRUE)
        {
            oBase.bRedirected = EngineConstants.FALSE;
        }
    }
}