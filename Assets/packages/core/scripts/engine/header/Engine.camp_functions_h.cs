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

public partial class Engine
{
     //#include"party_h"
     //#include"utility_h"
     //#include"wrappers_h"
     //#include"plt_gen00pt_party"
     //#include"sys_rewards_h"
     //#include"sys_ambient_h"
     //#include"camp_constants_h"

     //moved public const string WP_CAMP_FOLLOWER_PREFIX = "wp_camp_";

     public void Camp_ActivateShrieks()
     {

          List<GameObject> arParty = GetPartyPoolList();
          List<GameObject> arShrieks = UT_GetAllObjectsInAreaByTag(EngineConstants.CAMP_SHRIEK_ATTACKER_NORM, EngineConstants.OBJECT_TYPE_CREATURE);

          int nPartySize = GetArraySize(arParty);
          int nIndex;

          for (nIndex = 0; nIndex < nPartySize; nIndex++)
          {

               // Max 4 Normal Shrieks.
               if (nIndex >= 3)
               {
                    return;
               }

               else
               {
                    GameObject oAttacker = arShrieks[nIndex];

                    if (IsInvalidDeadOrDying(oAttacker) == EngineConstants.FALSE)
                    {

                         WR_SetObjectActive(oAttacker, EngineConstants.TRUE);

                         SetTeamId(oAttacker, EngineConstants.CAMP_TEAM_DARKSPAWN_CAMP_ATTACKERS);

                         Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Activating normal shriek #: ", IntToString(nIndex + 1));

                    }

               }

          }

     }

     public void Camp_FollowerAmbient(GameObject oFollower, int bStart)
     {

          string sTag = GetTag(oFollower);
          string sArea = GetTag(GetArea(oFollower));

          Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Follower: " + sTag, "Found in Area: " + sArea);

          // No movement phase, just animation.
          SetLocalInt(oFollower, EngineConstants.AMBIENT_ANIM_STATE, EngineConstants.AMBIENT_ANIM_RESET);

          int nAnim=0;

          // Set ambient system variables
          if (bStart != EngineConstants.FALSE)
          {

               // The Redcliff Castle main floor - climax.
               // Alistair, Loghain and Morrigan are not in this area.
               if (sTag == EngineConstants.CAM_CASTLE_CLIMAX)
               {

                    if (sTag == EngineConstants.GEN_FL_DOG) nAnim = 4;      // Relaxed.
                    else if (sTag == EngineConstants.GEN_FL_WYNNE) nAnim = 85;     // Listener Passive 3
                    else if (sTag == EngineConstants.GEN_FL_STEN) nAnim = 19;     // Arms crossed.
                    else if (sTag == EngineConstants.GEN_FL_ZEVRAN) nAnim = 67;     // Bored Loitering 1
                    else if (sTag == EngineConstants.GEN_FL_OGHREN) nAnim = 103;    // Bored Stationary.
                    else if (sTag == EngineConstants.GEN_FL_LELIANA) nAnim = 14;     // Listener Passive 2

                    Ambient_Start(oFollower, EngineConstants.AMBIENT_SYSTEM_ENABLED, EngineConstants.AMBIENT_MOVE_NONE, EngineConstants.AMBIENT_MOVE_PREFIX_NONE, nAnim, EngineConstants.AMBIENT_ANIM_FREQ_ORDERED);

               }

               // If in the party camp.
               else/*((sTag == EngineConstants.CAM_AR_ARCH1) || (sTag == EngineConstants.CAM_AR_CAMP_PLAINS) || (sTag == EngineConstants.CAM_AR_ARCH3))*/
               {

                    if (sTag == EngineConstants.GEN_FL_DOG) nAnim = 4;      // Relaxed.
                    else if (sTag == EngineConstants.GEN_FL_WYNNE) nAnim = 85;     // Listener Passive 3
                    else if (sTag == EngineConstants.GEN_FL_STEN) nAnim = 24;     // Guard Wander Left and Right
                    else if (sTag == EngineConstants.GEN_FL_ZEVRAN) nAnim = 67;     // Bored Loitering 1
                    else if (sTag == EngineConstants.GEN_FL_OGHREN) nAnim = 103;    // Bored Stationary
                    else if (sTag == EngineConstants.GEN_FL_LELIANA) nAnim = 71;     // Chat by fire.
                    else if (sTag == EngineConstants.GEN_FL_MORRIGAN) nAnim = 70;     // Warm by fire.
                    else if (sTag == EngineConstants.GEN_FL_ALISTAIR) nAnim = 100;    // Squat by fire.
                    else if (sTag == EngineConstants.GEN_FL_LOGHAIN) nAnim = 68;     // Bored Loitering 2

                    Ambient_Start(oFollower, EngineConstants.AMBIENT_SYSTEM_ENABLED, EngineConstants.AMBIENT_MOVE_NONE, EngineConstants.AMBIENT_MOVE_PREFIX_NONE, nAnim, EngineConstants.AMBIENT_ANIM_FREQ_ORDERED);

               }

               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Starting Ambient Animations for: " + sTag, "Playing Animation: " + IntToString(nAnim));

          }

          else
          {

               Ambient_Stop(oFollower);

          }

     }

     public void Camp_PlaceFollowersInCamp()
     {
          GameObject oAlistair = Party_GetFollowerByTag(EngineConstants.GEN_FL_ALISTAIR);
          GameObject oMorrigan = Party_GetFollowerByTag(EngineConstants.GEN_FL_MORRIGAN);
          GameObject oDog = Party_GetFollowerByTag(EngineConstants.GEN_FL_DOG);
          GameObject oWynne = Party_GetFollowerByTag(EngineConstants.GEN_FL_WYNNE);
          GameObject oShale = Party_GetFollowerByTag(EngineConstants.GEN_FL_SHALE);
          GameObject oSten = Party_GetFollowerByTag(EngineConstants.GEN_FL_STEN);
          GameObject oZevran = Party_GetFollowerByTag(EngineConstants.GEN_FL_ZEVRAN);
          GameObject oOghren = Party_GetFollowerByTag(EngineConstants.GEN_FL_OGHREN);
          GameObject oLeliana = Party_GetFollowerByTag(EngineConstants.GEN_FL_LELIANA);
          GameObject oLoghain = Party_GetFollowerByTag(EngineConstants.GEN_FL_LOGHAIN);

          // Activating any followers that are in the party

          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ALISTAIR_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ALISTAIR_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oAlistair, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oDog, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oWynne, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_SHALE_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_SHALE_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oShale, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oSten, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oZevran, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_OGHREN_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_OGHREN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oOghren, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oLeliana, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_MORRIGAN_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_MORRIGAN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oMorrigan, EngineConstants.TRUE);
          }
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LOGHAIN_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LOGHAIN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetObjectActive(oLoghain, EngineConstants.TRUE);
          }

          // Place all active party members in their spots
          List<GameObject> arParty = GetPartyPoolList();
          int nSize = GetArraySize(arParty);
          int i;
          GameObject oCurrent;
          string sWP;
          GameObject oWP;
          int nXP;
          int nHeroXP = GetExperience(GetHero());
          //    int nMinFollowerXP = FloatToInt(MIN_CAMP_FOLLOWER_XP * IntToFloat(nHeroXP));

          // first, remove all map pins (in case someone left the group)
          List<GameObject> arPins = GetObjectsInArea(gameObject);
          int nObjectsSize = GetArraySize(arPins);
          int j;
          GameObject oCurrentObject;
          for (j = 0; j < nObjectsSize; j++)
          {
               oCurrentObject = arPins[j];
               if (GetObjectType(oCurrentObject) == EngineConstants.OBJECT_TYPE_WAYPOINT && StringLeft(GetTag(oCurrentObject), 8) == EngineConstants.WP_CAMP_FOLLOWER_PREFIX)
                    SetMapPinState(oCurrentObject, EngineConstants.FALSE);
          }

          // then add the proper ones

          for (i = 0; i < nSize; i++)
          {
               oCurrent = arParty[i];
               if (GetObjectActive(oCurrent) != EngineConstants.FALSE && IsHero(oCurrent) == EngineConstants.FALSE)
               {
                    sWP = EngineConstants.WP_CAMP_FOLLOWER_PREFIX + GetTag(oCurrent);
                    UT_LocalJump(oCurrent, sWP);
                    oWP = GetObjectByTag(sWP);
                    SetMapPinState(oWP, EngineConstants.TRUE);
                    SetImmortal(oCurrent, EngineConstants.TRUE);
                    nXP = GetExperience(oCurrent);
                    RW_CatchUpToPlayer(oCurrent);

                    // Start the follower ambient system.
                    Camp_FollowerAmbient(oCurrent, EngineConstants.TRUE);

               }
          }

     }
}