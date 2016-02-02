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
     // function include

     //#include"log_h"
     //#include"utility_h"
     //#include"wrappers_h"
     //#include"events_h"

     //#include"pre_objects_h"
     //#include"party_h"

     //#include"plt_pre100pt_darkspn_blood"
     //#include"plt_pre100pt_light_beacon"
     //#include"plt_pre100pt_find_wardens"
     //#include"plt_genpt_alistair_events"
     //#include"plt_pre200pt_dying_soldier"
     //#include"sys_ambient_h"
     //#include"plt_pre100pt_generic"
     //#include"plt_prept_defined_cond"
     //#include"plt_pre100pt_ritual"
     //#include"plt_mnp00pt_ssf_prelude"
     //#include"plt_prept_generic_actions"

     public void PRE_ItemAcquired(GameObject oItem)
     {
          string sItemTag = GetTag(oItem);
          if (sItemTag == EngineConstants.PRE_IT_DARKSPAWN_BLOOD)
          {
               WR_SetPlotFlag(EngineConstants.PLT_PRE100PT_DARKSPN_BLOOD, EngineConstants.PRE_BLOOD_PC_ACQUIRED_DARKSPAWN_BLOOD, EngineConstants.TRUE, EngineConstants.TRUE);
          }
     }

     public int PRE_GameModeChanged(int nGameMode)
     {
          int bEventHandled = EngineConstants.FALSE;
          GameObject oPC = GetHero();
          string sAreaTag = GetTag(GetArea(oPC));
          GameObject oAlistair = Party_GetActiveFollowerByTag(EngineConstants.GEN_FL_ALISTAIR);
          GameObject oBeaconWP = UT_GetNearestObjectByTag(oAlistair, EngineConstants.PRE_WP_TOWER_4_LIGHT_BEACON);
          GameObject oTowerGuard1 = Party_GetFollowerByTag(EngineConstants.PRE_CR_TOWER_GUARD);

          /* switch(nGameMode)
          {
              Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_GameModeChanged", "firing");
              // Initiate the 'rescue' cutscene if the player is killed at the top of the singal tower by the Darkspawn horde.

               -----------------------------------------------------------------------
               Georg: Cut.
               -----------------------------------------------------------------------
              case EngineConstants.GM_DEAD:
              {
                  Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_GameModeChanged.EngineConstants.GM_DEAD", "firing");
                  // If the beacon is lit but the rescue cutscene has not yet been played (i.e. the darkspawn horde)
                  if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_LIGHT_BEACON, EngineConstants.PRE_BEACON_AFTER_BEACON_CUTSCENE)
                     && !WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_LIGHT_BEACON, EngineConstants.PRE_BEACON_TRIGGER_RESCUE_CUTSCENE))
                  {
                     // Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, GetCurrentScriptName() + ".PRE_GameModeChanged.EngineConstants.GM_DEAD"+ "party is dead, apparently");
                      WR_SetPlotFlag(EngineConstants.PLT_PRE100PT_LIGHT_BEACON, EngineConstants.PRE_BEACON_TRIGGER_RESCUE_CUTSCENE, EngineConstants.TRUE, EngineConstants.TRUE);
                      bEventHandled = EngineConstants.TRUE; // avoid game mode setting; the player should not see the death gui.
                  }
                  break;
              }*/
          /*
          -----------------------------------------------------------------------
           Georg: Cut.
           -----------------------------------------------------------------------
          case EngineConstants.GM_COMBAT:
          {
              Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_GameModeChanged.EngineConstants.GM_COMBAT", "firing");
              // Alistair gives darkspawn warning at beginning of wilds combat, but only after dying soldier conversation
              if (!WR_GetPlotFlag(EngineConstants.PLT_GENPT_ALISTAIR_EVENTS, ALISTAIR_EVENT_ON)
                 && (WR_GetPlotFlag(EngineConstants.PLT_PRE200PT_DYING_SOLDIER, EngineConstants.PRE_DYING_SOLDIER_DIED)
                      || WR_GetPlotFlag(EngineConstants.PLT_PRE200PT_DYING_SOLDIER, EngineConstants.PRE_DYING_SOLDIER_GOT_HELP))
                 && GetObjectActive(oAlistair) && sAreaTag == EngineConstants.PRE_AR_KORCARI_WILDS)
              {
                  if (ReadIniEntry("DebugOptions","E3Mode") == "0")
                  {
                      Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_GameModeChanged.EngineConstants.GM_COMBAT", "conditions met");
                      WR_SetPlotFlag(EngineConstants.PLT_GENPT_ALISTAIR_EVENTS, ALISTAIR_EVENT_ON, EngineConstants.TRUE);
                      WR_SetPlotFlag(EngineConstants.PLT_GENPT_ALISTAIR_EVENTS, ALISTAIR_EVENT_DARKSPAWN_NEARBY, EngineConstants.TRUE);

                      UT_Talk(oAlistair, oPC);
                  }
              }

              break;
          }

      }
       */
          return bEventHandled;
     }

     /* @brief Brief description of function
*
*   Handles story-so-far plot states for the prelude.
*
*
*
* @author Craig Graff
**/
     public void PRE_ModulePresave()
     {

          if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_GENERIC, EngineConstants.PRE_GENERIC_PRELUDE_DONE) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_AFTERMATH, EngineConstants.TRUE);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_LIGHT_BEACON, EngineConstants.PRE_BEACON_AFTER_RESCUE_CUTSCENE) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_RESCUED, EngineConstants.TRUE);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_LIGHT_BEACON, EngineConstants.PRE_BEACON_GUARD_SPOKE) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_TOWER_TAKEN, EngineConstants.TRUE);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_LIGHT_BEACON, EngineConstants.PRE_BEACON_DUNCAN_LEAVES_FOR_BATTLE) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_DUNCAN_GONE, EngineConstants.TRUE);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_RITUAL, EngineConstants.PRE_RITUAL_END) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_RITUAL_DONE, EngineConstants.TRUE);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_PREPT_DEFINED_COND, EngineConstants.PRE_DEFINED_GOT_BLOOD_AND_DOCUMENTS, EngineConstants.TRUE) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_BLOOD_AND_DOCUMENTS_FOUND, EngineConstants.TRUE);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_DARKSPN_BLOOD, EngineConstants.PRE_BLOOD_PLOT_ACCEPTED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_INTO_THE_WILDS, EngineConstants.TRUE);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_FIND_WARDENS, EngineConstants.PRE_WARDENS_PLOT_FOUND_ALISTAIR) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_ALISTAIR_FOUND, EngineConstants.TRUE);
          }
          else
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_PRELUDE, EngineConstants.SSF_PRELUDE_START, EngineConstants.TRUE);
          }

     }

     public void PRE_QuickMoveTeam(int nTeam, string sWP = "0", int bRun = EngineConstants.TRUE, int bStatic = EngineConstants.FALSE)
     {
          //UT_TeamMove(nTeam, sWP, bRun);

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_QuickMoveTeam", "Moving team " + IntToString(nTeam) + " to " + sWP + ".");

          List<GameObject> arTeam = UT_GetTeam(nTeam);
          GameObject oCurrent;
          int nIndex;
          int nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_QuickMoveTeam", "Moving team member" + IntToString(nIndex) + " to " + sWP + ".");

               oCurrent = arTeam[nIndex];

               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_QuickMoveTeam", "Object index" + ObjectToString(oCurrent) + " to " + sWP + ".");

               //        AMB_StopAmbientAI(oCurrent);
               WR_ClearAllCommands(oCurrent);
               WR_AddCommand(oCurrent, CommandWait(nIndex * 0.5f));
               UT_QuickMoveObject(oCurrent, sWP, bRun, EngineConstants.TRUE, EngineConstants.TRUE, bStatic);
          }

     }

     public void PRE_SpawnBlood()
     {
          GameObject oPC = GetHero();
          // if PC doesn't have all the blood, create the blood
          if ((UT_CountItemInInventory(EngineConstants.PRE_IM_DARKSPAWN_BLOOD, oPC) < 3)
             && WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_DARKSPN_BLOOD, EngineConstants.PRE_BLOOD_PLOT_ACTIVE, EngineConstants.TRUE) != EngineConstants.FALSE)
          {
               // add the blood to the dying creature's inventory
               UT_AddItemToInventory(EngineConstants.PRE_IM_DARKSPAWN_BLOOD, 1, gameObject);
          }
     }

     public void PRE_SetupGroupHostility()
     {
          SetGroupHostility(EngineConstants.PRE_GROUP_HOSTILE_DARKSPAWN, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.PRE_GROUP_HOSTILE_DARKSPAWN, EngineConstants.GROUP_PC, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.PRE_GROUP_HOSTILE_WOLVES, EngineConstants.GROUP_PC, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.PRE_GROUP_HOSTILE_DARKSPAWN, EngineConstants.PRE_GROUP_HOSTILE_WOLVES, EngineConstants.TRUE);
     }

     public void PRE_SetTeamGoreLevel(int nTeam, float fGore)
     {
          List<GameObject> arrTeam = GetTeam(nTeam);
          int n;
          int nTeamSize = GetArraySize(arrTeam);
          for (n = 0; n < nTeamSize; n++)
          {
               SetCreatureGoreLevel(arrTeam[n], fGore);
          }
     }

     public void PRE_TeamUnequipSlot(int nTeam, int nSlot, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
     {
          List<GameObject> arrTeam = GetTeam(nTeam);
          GameObject oCreature;
          GameObject oItem;
          int n;
          int nTeamSize = GetArraySize(arrTeam);

          for (n = 0; n < nTeamSize; n++)
          {
               oCreature = arrTeam[n];
               oItem = GetItemInEquipSlot(nSlot, oCreature, nWeaponSet);
               UnequipItem(oCreature, oItem);
          }

     }

     public void PRE_HealParty()
     {
          List<GameObject> arrParty = GetPartyList();
          GameObject oPartyMember;
          int n;
          int nPartySize = GetArraySize(arrParty);

          for (n = 0; n < nPartySize; n++)
          {
               oPartyMember = arrParty[n];
               Injury_RemoveAllInjuries(oPartyMember);
               HealCreature(oPartyMember, EngineConstants.TRUE);
          }
     }

     public void PRE_SetupEnvironmentEffects()
     {

          GameObject oPC = GetMainControlled();
          GameObject oArea = GetArea(oPC);
          string sArea = GetTag(oArea);

          // Environment Effects for King's Camp (Day)
          if (sArea == EngineConstants.PRE_AR_KINGS_CAMP)
          {
               xEffect effBirds = EffectVisualEffect(4002);
               GameObject oWaypoint = UT_GetNearestObjectByTag(oPC, EngineConstants.PRE_WP_EA_TOWER_BIRDS);
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, effBirds, GetLocation(oWaypoint), 0.0f, oPC);
          }

          // Environment Effects for King's Camp (Night)
          else if (sArea == EngineConstants.PRE_AR_KINGS_CAMP_NIGHT)
          {
          }

          // Environment Effects for Korcari Wilds
          else if (sArea == EngineConstants.PRE_AR_KORCARI_WILDS)
          {
          }

     }

     public void PRE_StartTeamAmbient(int nTeam, int nAmbientEnable = 0, int nMovePattern = 0, string sMovePrefix = "", int nAnimPattern = 0, float fAnimFreq = 0.0f)
     {
          List<GameObject> arCreature = GetTeam(nTeam);
          int nIndex = 0;
          int nArraySize = GetArraySize(arCreature);

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               GameObject oCreature = arCreature[nIndex];
               Ambient_Start(oCreature, nAmbientEnable, nMovePattern, sMovePrefix, nAnimPattern, fAnimFreq);
          }
     }
}