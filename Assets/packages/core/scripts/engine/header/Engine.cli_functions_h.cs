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
     // Generic climax functions

     //#include"wrappers_h"
     //#include"log_h"
     //#include"cli_constants_h"
     //#include"sys_treasure_h"
     //#include"plt_clipt_generic_actions"
     // 13 - werewolves, 0 - elvels
     //#include"plt_ntb000pt_main"

     // 15 - dwarves, 16 - golems, 13 - legion
     //#include"plt_orzpt_main"

     // 4 - templars, 5 - mages
     //#include"plt_cir000pt_main"

     //#include"ai_constants_h"
     //#include"plt_tut_army_picker"

     public void Cli_CreateArmyPool(int nArmyID, int nInstancesInArea = 1)
     {
          if (nArmyID <= 0)
               return;
          int nArmyTotal = GetM2DAInt(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyBufferMax", nArmyID);
          int nArmyTable = GetM2DAInt(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyTable", nArmyID);
          int nArmySoldiersCount = GetM2DARows(nArmyTable);
          int[] arSoldiers; // an array to store
          int i;
          int nSpawnChance;
          int nNoPermRules;
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Cli_CreateArmyPool", "START, army: " + IntToString(nArmyID) + ", instances in area: " + IntToString(nInstancesInArea));
#endif

          if (nInstancesInArea <= 0)
               return;

          // creating pools
          int nPoolSize;
          float fMyProbablity;
          string rTemplate;
          for (i = 0; i < nArmySoldiersCount; i++)
          {
               fMyProbablity = GetM2DAFloat(nArmyTable, "ChanceToSpawn", i);
               if (fMyProbablity > 0.0f)
               {
                    nPoolSize = FloatToInt(IntToFloat(nArmyTotal) * fMyProbablity);
                    nPoolSize++;
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Cli_CreateArmyPool", "Pool size for soldiers [" + IntToString(i) + "] is: " + IntToString(nPoolSize));
#endif
                    rTemplate = GetM2DAResource(nArmyTable, "ResourceName", i);
                    nPoolSize *= nInstancesInArea;

                    // check if to create a bigger pool to allow no permanent death
                    nNoPermRules = GetM2DAInt(nArmyTable, "AllowPermDeath", i);
                    if (nNoPermRules == 2) // allow permanent death AND make a bigger pool
                         nPoolSize *= 2; // double the size (death blow chances are around 20%

                    CreatePool(rTemplate, nPoolSize);
               }
          }
     }

     public void Cli_SetTeamScript(List<GameObject> arObjects, GameObject oArea = null, int nInstancesInArea = 1)
     {
          Warning("this script runs attached to an area?!?");
          if (oArea = null) oArea = gameObject;
          int i;
          int nSize = GetArraySize(arObjects);
          GameObject oCurrent;
          int nArmyID;
          nArmyID = GetTeamId(arObjects[0]);
          string sBufferVar = GetM2DAString(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyBufferVar", nArmyID);
          string sArmyTotalVar = GetM2DAString(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyTotalVar", nArmyID);
          string rScript = GetM2DAResource(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyScript", nArmyID);
          int nCustomAI = GetM2DAInt(EngineConstants.TABLE_CLIMAX_ARMIES, "CustomAI", nArmyID);
          int nSpawnCounter = 0;
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Cli_SetTeamScript", "Number of creatures in array: " + IntToString(nSize));
#endif

          for (i = 0; i < nSize; i++)
          {
               oCurrent = arObjects[i];

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Cli_SetTeamScript", "Changing creature script to army script: " + GetTag(oCurrent));
#endif
               SetEventScript(oCurrent, rScript);
               if (GetCreatureRank(oCurrent) != EngineConstants.CREATURE_RANK_LIEUTENANT && GetCreatureRank(oCurrent) != EngineConstants.CREATURE_RANK_BOSS
                  && GetCreatureRank(oCurrent) != EngineConstants.CREATURE_RANK_ELITE_BOSS)
               {
                    SetLocalInt(oCurrent, EngineConstants.AI_LIGHT_ACTIVE, EngineConstants.TRUE);
                    SetLocalInt(oCurrent, EngineConstants.CLIMAX_ARMY_ID, nArmyID);
                    SetLocalInt(oCurrent, EngineConstants.TS_OVERRIDE_CATEGORY, -1); // so it has no treasure
                    SetLocalInt(oCurrent, EngineConstants.TS_OVERRIDE_EQUIPMENT_CHANCE, -1);
               }
               if (nCustomAI != EngineConstants.FALSE)
                    SetLocalInt(oCurrent, EngineConstants.AI_CUSTOM_AI_ACTIVE, EngineConstants.TRUE);

               // update name for hurlocks and genlocks
               string sName;
               if (GetAppearanceType(oCurrent) == EngineConstants.APP_TYPE_GENLOCK)
               {
                    sName = GetStringByStringId(395039);
                    SetName(oCurrent, sName);
               }
               else if (GetAppearanceType(oCurrent) == EngineConstants.APP_TYPE_HURLOCK)
               {
                    sName = GetStringByStringId(395038);
                    SetName(oCurrent, sName);
               }

               // Update army buffer size and creature counter
               nSpawnCounter++;
          }
          SetLocalInt(oArea, sBufferVar, nSpawnCounter);
          SetLocalInt(oArea, sArmyTotalVar, nSpawnCounter);

          Cli_CreateArmyPool(nArmyID, nInstancesInArea);
     }

     public void Climax_SpawnDarkspawnArmy(GameObject oArea, int nArmyID, int bQuick = EngineConstants.FALSE)
     {
          int nArmyTable = GetM2DAInt(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyTable", nArmyID);
          string sArmySpawnWP = GetM2DAString(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmySpawnWP", nArmyID);
          int nArmyBufferMax = GetM2DAInt(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyBufferMax", nArmyID);
          string sArmyBufferVar = GetM2DAString(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyBufferVar", nArmyID);
          int nArmyTotalMax = GetM2DAInt(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyTotalMax", nArmyID);
          string sArmyTotalVar = GetM2DAString(EngineConstants.TABLE_CLIMAX_ARMIES, "ArmyTotalVar", nArmyID);
          int nCurrentBuffer = GetLocalInt(oArea, sArmyBufferVar);
          int nCurrentDSCount = GetLocalInt(oArea, sArmyTotalVar);

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Climax_SpawnDarkspawnArmy", "Army ID: " + IntToString(nArmyID) +
                                                                  ", Spawn WP: " + sArmySpawnWP +
                                                                  ", Buffer Max: " + IntToString(nArmyBufferMax) +
                                                                  ", Buffer Cur: " + IntToString(nCurrentBuffer) +
                                                                  ", Total Max: " + IntToString(nArmyTotalMax) +
                                                                  ", Total Cur: " + IntToString(nCurrentDSCount));
#endif

          int nToSpawn = nArmyBufferMax - nCurrentBuffer;
          if (nToSpawn < 0) nToSpawn = 0;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "Climax_SpawnDarkspawnArmy", "TOTAL TO SPAWN: " + IntToString(nToSpawn));
#endif

          xEvent evSpawnDS = Event(EngineConstants.EVENT_TYPE_CUSTOM_EVENT_04);
          SetEventStringRef(ref evSpawnDS, 0, sArmySpawnWP);
          SetEventIntegerRef(ref evSpawnDS, 0, -1); // last creature ID to die (if any)
          SetEventIntegerRef(ref evSpawnDS, 1, nArmyTable);
          SetEventIntegerRef(ref evSpawnDS, 2, nArmyID);

          float fDelay = 1.0f;
          int i;
          int nSpawnCount = 0;
          for (i = 0; i < nToSpawn; i++)
          {
               DelayEvent(fDelay, oArea, evSpawnDS);
               if (bQuick != EngineConstants.FALSE)
                    fDelay += 3.0f;
               else
                    fDelay += RandFF(EngineConstants.CLI_ARMY_SPAWN_DELAY_MAX - EngineConstants.CLI_ARMY_SPAWN_DELAY_MIN, EngineConstants.CLI_ARMY_SPAWN_DELAY_MIN);
               nSpawnCount++;
          }
          // Update buffer size
          SetLocalInt(oArea, sArmyBufferVar, nSpawnCount + nCurrentBuffer);

          // Update spawn counter
          SetLocalInt(oArea, sArmyTotalVar, nSpawnCount + nCurrentBuffer);
     }

     public void Climax_SetArmyGUIState(string sPlot, int nFlag, int nArmy)
     {
          int nCurrentState = GetPlotActionState(nArmy);
          int nCount = GetPlotActionCount(nArmy);
          if (nCount == -1) // first time
               nCount = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxCount", nArmy);
          Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, GetCurrentScriptName(), "Current army state: " + IntToString(nCurrentState) + ", current count: " + IntToString(nCount));
          // Nature of the beast - werewolves or elves
          if ((sPlot == "" || WR_GetPlotFlag(sPlot, nFlag) != EngineConstants.FALSE) &&
              GetPlotActionState(nArmy) != EngineConstants.PLOT_ACTIONSTATE_DISABLED)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, GetCurrentScriptName(), "Enabling army: " + IntToString(nArmy));
               SetPlotActionState(nArmy, EngineConstants.PLOT_ACTIONSTATE_ENABLED);
               SetPlotActionCount(nArmy, nCount);
          }
          else if (sPlot != "" && WR_GetPlotFlag(sPlot, nFlag) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, GetCurrentScriptName(), "Disabling army: " + IntToString(nArmy));
               SetPlotActionState(nArmy, EngineConstants.PLOT_ACTIONSTATE_INVALID);
               SetPlotActionCount(nArmy, nCount);
          }

     }

     public void Climax_SetArmyGUI()
     {
          // Tutorial
          WR_SetPlotFlag(EngineConstants.PLT_TUT_ARMY_PICKER, EngineConstants.TUT_ARMY_PICKER, EngineConstants.TRUE);

          Climax_SetArmyGUIState("", -1, EngineConstants.PLOT_ACTION_ARMY_REDCLIFFE);

          // Nature of the beast - werewolves or elves
          Climax_SetArmyGUIState(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_WEREWOLVES_PROMISED_ALLIANCE, EngineConstants.PLOT_ACTION_ARMY_WEREWOLVES);
          Climax_SetArmyGUIState(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ELVES_PROMISED_ALLIANCE, EngineConstants.PLOT_ACTION_ARMY_ELVES);

          // Paragon: Dwarves and possibly golems (legion is part of the dwarves army)
          Climax_SetArmyGUIState(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN_ARMY_WILL_INCLUDE_DWARFS, EngineConstants.PLOT_ACTION_ARMY_DWARVES);
          Climax_SetArmyGUIState(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN_ARMY_WILL_INCLUDE_GOLEMS, EngineConstants.PLOT_ACTION_ARMY_GOLEMS);

          // Broken Circle - mages or templars
          Climax_SetArmyGUIState(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.MAGES_IN_ARMY, EngineConstants.PLOT_ACTION_ARMY_WIZARDS);
          Climax_SetArmyGUIState(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.TEMPLARS_IN_ARMY, EngineConstants.PLOT_ACTION_ARMY_TEMPLARS);

          SetPlotActionSet(EngineConstants.PLOT_ACTIONSET_ARMY);
          SetPlotActionsEnabled(EngineConstants.TRUE);
     }

     public void Cli_SetTeamHelp(int nTeam)
     {
          List<GameObject> arTeam = GetTeam(nTeam);
          int nIndex;
          int nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               SetLocalInt(arTeam[nIndex], EngineConstants.AI_HELP_TEAM_STATUS, EngineConstants.AI_HELP_TEAM_STATUS_ACTIVE);
          }
     }

     public void SetFinalFightAlly(GameObject oAlly)
     {
          GameObject oMoveTo = GetObjectByTag("cli220wp_ally_leader_moveto");
          xCommand cMove = CommandMoveToObject(oMoveTo, EngineConstants.TRUE, 1.0f);
          WR_SetObjectActive(oAlly, EngineConstants.TRUE);
          WR_AddCommand(oAlly, cMove, EngineConstants.TRUE, EngineConstants.TRUE, -1, 5.0f);
          SetLocalInt(oAlly, EngineConstants.AI_CUSTOM_AI_ACTIVE, 1);
          //SetFollowPartyLeader(oAlly, EngineConstants.TRUE);
     }

     public void SpawnFinalFightAlly(int nArmyID = -1)
     {
          GameObject oEamon = GetObjectByTag("cli220cr_arl_eamon");
          GameObject oGreagoir = GetObjectByTag("cli220cr_greagoir");
          GameObject oIrving = GetObjectByTag("cli220cr_irving");
          GameObject oKardol = GetObjectByTag("cli220cr_kardol");
          GameObject oSwift = GetObjectByTag("cli220cr_swiftrunner");
          GameObject oZathrian = GetObjectByTag("cli220cr_zathrian");
          int nSpawned = EngineConstants.FALSE;
          if (WR_GetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_ARL_EAMON) == EngineConstants.FALSE)
          {
               if (nArmyID == -1 || (nArmyID != -1 && nArmyID == EngineConstants.PLOT_ACTION_ARMY_REDCLIFFE))
               {
                    WR_SetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_ARL_EAMON, EngineConstants.TRUE);
                    SetFinalFightAlly(oEamon);
                    nSpawned = EngineConstants.TRUE;
               }
          }
          if (nSpawned == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_GREGOIR) == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.TEMPLARS_IN_ARMY) != EngineConstants.FALSE)
          {
               if (nArmyID == -1 || (nArmyID != -1 && nArmyID == EngineConstants.PLOT_ACTION_ARMY_TEMPLARS))
               {
                    WR_SetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_GREGOIR, EngineConstants.TRUE);
                    SetFinalFightAlly(oGreagoir);
                    nSpawned = EngineConstants.TRUE;
               }
          }
          if (nSpawned == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_IRVING) == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.MAGES_IN_ARMY) != EngineConstants.FALSE)
          {
               if (nArmyID == -1 || (nArmyID != -1 && nArmyID == EngineConstants.PLOT_ACTION_ARMY_WIZARDS))
               {
                    WR_SetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_IRVING, EngineConstants.TRUE);
                    SetFinalFightAlly(oIrving);
                    nSpawned = EngineConstants.TRUE;
               }
          }
          if (nSpawned == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_KARDOL) == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN_ARMY_WILL_INCLUDE_LEGION) != EngineConstants.FALSE)
          {
               if (nArmyID == -1 || (nArmyID != -1 && nArmyID == EngineConstants.PLOT_ACTION_ARMY_DWARVES))
               {
                    WR_SetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_KARDOL, EngineConstants.TRUE);
                    SetFinalFightAlly(oKardol);
                    nSpawned = EngineConstants.TRUE;
               }
          }
          if (nSpawned == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_SWIFTRUNNER) == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_WEREWOLVES_PROMISED_ALLIANCE) != EngineConstants.FALSE)
          {
               if (nArmyID == -1 || (nArmyID != -1 && nArmyID == EngineConstants.PLOT_ACTION_ARMY_WEREWOLVES))
               {
                    WR_SetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_SWIFTRUNNER, EngineConstants.TRUE);
                    SetFinalFightAlly(oSwift);
                    nSpawned = EngineConstants.TRUE;
               }
          }
          if (nSpawned == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_ZATHRIAN) == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ELVES_PROMISED_ALLIANCE) != EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ZATHRIAN_KILLED_BY_PC) == EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ZATHRIAN_SACRIFICES_HIMSELF) == EngineConstants.FALSE)
          {
               if (nArmyID == -1 || (nArmyID != -1 && nArmyID == EngineConstants.PLOT_ACTION_ARMY_ELVES))
               {
                    WR_SetPlotFlag(EngineConstants.PLT_CLIPT_GENERIC_ACTIONS, EngineConstants.CLI_ACTIONS_JOIN_FINAL_FIGHT_ZATHRIAN, EngineConstants.TRUE);
                    SetFinalFightAlly(oZathrian);
                    nSpawned = EngineConstants.TRUE;
               }
          }
     }
}