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
     /******************************************************************************
     * This include file contains wrappers functions to important engine functions.
     * Designers should use these instead of the engine functions wrapped inside.
     ********************************************************************************/

     //#include"log_h"
     //#include"global_objects_h"
     //#include"events_h"
     //#include"core_h"
     //#include"config_h"

     //#include"plt_tut_control_followers"
     //#include"autoss_constants_h"

     public string _GetFollowerStateName(int nState)
     {
          string sRet = String.Empty;
          switch (nState)
          {
               case EngineConstants.FOLLOWER_STATE_ACTIVE: sRet = "EngineConstants.FOLLOWER_STATE_ACTIVE"; break;
               case EngineConstants.FOLLOWER_STATE_AVAILABLE: sRet = "EngineConstants.FOLLOWER_STATE_AVAILABLE"; break;
               case EngineConstants.FOLLOWER_STATE_INVALID: sRet = "EngineConstants.FOLLOWER_STATE_INVALID"; break;
               case EngineConstants.FOLLOWER_STATE_LOADING: sRet = "EngineConstants.FOLLOWER_STATE_LOADING"; break;
               case EngineConstants.FOLLOWER_STATE_LOCKEDACTIVE: sRet = "EngineConstants.FOLLOWER_STATE_LOCKEDACTIVE"; break;
               case EngineConstants.FOLLOWER_STATE_SUSPENDED: sRet = "EngineConstants.FOLLOWER_STATE_ACTIVE"; break;
               case EngineConstants.FOLLOWER_STATE_UNAVAILABLE: sRet = "EngineConstants.FOLLOWER_STATE_ACTIVE"; break;

          }
          return sRet;
     }

     public void WR_SetStoryPlot(string sPlot)
     {
          string sPlotName = GetPlotResRef(sPlot);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "wrappers_h.WR_SetStoryPlot", "story plot: " + sPlotName);
          SetStoryPlot(sPlot);
     }

     public int WR_ClearAllCommands(GameObject oObject, int bHardClear = EngineConstants.FALSE)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "wrappers_h.WR_ClearAllCommands", "", oObject);
#endif

          int nRet = ClearAllCommands(oObject, bHardClear);
          return nRet;
     }

     public int WR_GetPlotFlag(string strPlot, int nFlag, int nCallScript = EngineConstants.FALSE)
     {
          GameObject oPC = GetHero();
          GameObject oParty = GetParty(oPC);
          int nPlotName = GetPlotEntryName(strPlot);
          string sPlotName = GetPlotResRef(strPlot);
          if ((sPlotName) == "") sPlotName = strPlot;
          int nCurrent = GetPartyPlotFlag(oParty, strPlot, nFlag, nCallScript);
          string sFlagName = GetPlotFlagName(strPlot, nFlag);

          LogTrace(EngineConstants.LOG_CHANNEL_PLOT, "GetPlot [" + sPlotName + "] ["
              + sFlagName + "] = [" + ToString(nCurrent) + "]");
          Log_Trace_Plot("wrappers_h.WR_GetPartyPlotFlag", sPlotName, nFlag, nCurrent);
          return nCurrent;
     }

     public void WR_SetPlotFlag(string strPlot, int nFlag, int nValue, int nCallScript = EngineConstants.FALSE)
     {
          GameObject oPC = GetHero();
          GameObject oParty = GetParty(oPC);
          int nPlotName = GetPlotEntryName(strPlot);
          string sPlotName = GetPlotResRef(strPlot);
          if ((sPlotName) == "") sPlotName = strPlot;
          int nCurrent = GetPartyPlotFlag(oParty, strPlot, nFlag, nCallScript);
          string sFlagName = GetPlotFlagName(strPlot, nFlag);
          LogTrace(EngineConstants.LOG_CHANNEL_PLOT, "SetPlot [" + sPlotName + "] ["
              + sFlagName + "] -> [" + ToString(nValue) + "]");

          if (nCallScript == EngineConstants.FALSE)
          {
               //TrackPlotEvent(EngineConstants.EVENT_TYPE_SET_PLOT, gameObject, oPC, nValue, -1, sPlotName, sFlagName);
          }
          SetPartyPlotFlag(oParty, strPlot, nFlag, nValue, nCallScript);
     }

     public void WR_DestroyObject(GameObject oObject)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "wrappers_h.WR_DestroyObject", "", oObject);

          DestroyObject(oObject, 0);
     }

     public void WR_SetObjectActive(GameObject oObject, int bActive, int nAnimation = -1, int nVFX = -1)
     {
          if (GetObjectActive(oObject) == bActive)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "wrappers_h.WR_SetObjectActive",
                      "Object already same state as requested - doing nothing");
               return;
          }
          // Handle default appear/disappear animation if one is not defined.
          if (nAnimation == -1)
          {
               nAnimation = 0;
               // Only use default if:
               //  - Object is a Creature
               //  - Creature is Alive
               //  - We are going from Active -> Inactive or Inactive -> Active
               if (GetObjectType(oObject) == EngineConstants.OBJECT_TYPE_CREATURE && IsDead(oObject) == EngineConstants.FALSE && GetObjectActive(oObject) != bActive)
               {
                    if (bActive != EngineConstants.FALSE && GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "bHasAppearAnim", GetAppearanceType(oObject)) != EngineConstants.FALSE)
                    {
                         nAnimation = 648;
                    }
                    else if (bActive == EngineConstants.FALSE && GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "bHasDisappearAnim", GetAppearanceType(oObject)) != EngineConstants.FALSE)
                    {
                         nAnimation = 649;
                    }
               }
          }

          // Handle default appear/disappear VFX if one is not defined
          if (nVFX == -1)
          {
               if (bActive != EngineConstants.FALSE)
               {
                    nVFX = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "AppearVFX", GetAppearanceType(oObject));
               }
               else
               {
                    nVFX = 0;
               }
          }

          SetObjectActive(oObject, bActive, nAnimation, nVFX);

          if (bActive != EngineConstants.FALSE)
          {
               SignalEvent(oObject, Event(EngineConstants.EVENT_TYPE_OBJECT_ACTIVE));
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "wrappers_h.WR_SetObjectActive",
                     "bActive: " + ToString(bActive) + ", nAnimation: " + ToString(nAnimation) + ", nVFX: " + ToString(nVFX),
                     oObject);
     }

     public void WR_RemoveCommand(GameObject oObject, xCommand cCommand)
     {
          int nCommandType = GetCommandType(cCommand);
          string sCommand = "wrappers_h.WR_RemoveCommand" + Log_GetCommandNameById(nCommandType);

          Log_Trace_Commands(sCommand, cCommand, oObject);

          if (IsObjectValid(oObject) == EngineConstants.FALSE)
          {
               Log_Trace_Scripting_Error(sCommand, "used on invalid object");
               return;
          }

          if (nCommandType == EngineConstants.COMMAND_TYPE_INVALID)
          {
               Log_Trace_Scripting_Error(sCommand, "invalid input parameter for cCommand (EngineConstants.COMMAND_TYPE_INVALID");
               return;
          }

          /*    xCommand cCurrent = GetCurrentCommand(oObject);
                if(cCurrent == cCommand)
                  Log_Systems("*** Removing current command", EngineConstants.LOG_LEVEL_WARNING, oObject, EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
                else
                  Log_Systems("*** Removing from queue", EngineConstants.LOG_LEVEL_WARNING, oObject, EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
          */

          int nSize = GetCommandQueueSize(oObject);
          /*    if(nSize == 0)
                  Log_Systems("*** Command queue is currently empty", EngineConstants.LOG_LEVEL_DEBUG, oObject
                      , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
              else
                  Log_Systems("*** Command queue size BEFORE REMOVING COMMAND is: " + IntToString(nSize), EngineConstants.LOG_LEVEL_DEBUG, oObject
                      , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);*/

          int i;
          int nType;
          /*    for(i = 0; i < nSize; i++)
              {
                  cCurrent = GetCommandByIndex(oObject, i);
                  nType = GetCommandType(cCurrent);
                  if(cCurrent == cCommand)
                      Log_Systems("*** (TO BE REMOVED) COMMAND[" + IntToString(i) + "]= " + IntToString(nType), EngineConstants.LOG_LEVEL_DEBUG, oObject
                      , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
                  else
                      Log_Systems("*** COMMAND[" + IntToString(i) + "]= " + IntToString(nType), EngineConstants.LOG_LEVEL_DEBUG, oObject
                      , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
              }
          */

          //    Log_Systems("*** REMOVING COMMAND (" + IntToString(nCommandType) + ")", EngineConstants.LOG_LEVEL_DEBUG, oObject, EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
          RemoveCommand(oObject, cCommand);
     }

     public void WR_AddCommand(GameObject oObject, xCommand cCommand, int bAddToFront = EngineConstants.FALSE, int bStatic = EngineConstants.FALSE, int nOverrideAddBehavior = -1, float fTimeout = 0.0f)
     {
          int nCommandType = GetCommandType(cCommand);

          // timeout check
          // if a timeout was selected AND this is not a follower AND we're in combat AND it's the creatures first timer command
          // then shorten the timer so if the creature moves he'll stop faster and will then have a chance to reevaluate
          // his threat towards the party
          if (fTimeout > 0.0f && IsFollower(oObject) == EngineConstants.FALSE && GetCombatState(oObject) != EngineConstants.FALSE &&
              GetLocalInt(oObject, EngineConstants.CREATURE_HAS_TIMER_ATTACK) == 0)
          {
               SetLocalInt(oObject, EngineConstants.CREATURE_HAS_TIMER_ATTACK, 1); // applied only for first timer check
               fTimeout = 1.0f; // quick timeout
          }

          if (IsObjectValid(oObject) == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_Scripting_Error("WR_AddCommand()", Log_GetCommandNameById(nCommandType) + " used on invalid object.");
#endif
               return;
          }

          if (nCommandType == EngineConstants.COMMAND_TYPE_INVALID || nCommandType == 0)
          {
#if DEBUG
               Log_Trace_Scripting_Error("WR_AddCommand()", "Invalid input parameter for cCommand (EngineConstants.COMMAND_TYPE_INVALID)");
#endif
               Warning("Something is trying to add an invalid xCommand from scripting (command_type 0). Please contact georg.)");
               return;
          }

#if DEBUG
          Log_Trace_Commands("WR_AddCommand()", cCommand, oObject);
#endif

#if DEBUG
          //  Log_Trace(EngineConstants.LOG_CHANNEL_COMMANDS, "WR_AddCommand", "*** START, object= " + GetTag(oObject) + ". command= " + IntToString(nCommandType));
          if (bAddToFront != EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_COMMANDS, "WR_AddCommand()", "*** Adding xCommand to front");
          if (bStatic != EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_COMMANDS, "WR_AddCommand()", "*** Command is static");
          if (fTimeout > 0.0f)
               Log_Trace(EngineConstants.LOG_CHANNEL_COMMANDS, "WR_AddCommand()", "*** Timeout: " + FloatToString(fTimeout));
#endif

          xCommand cCurrent = GetCurrentCommand(oObject);
          int nCurrentType = GetCommandType(cCurrent);
          //Uncommented by DHK
          if (nCurrentType == EngineConstants.COMMAND_TYPE_INVALID)
               Log_Systems("*** No xCommand is executing currently", EngineConstants.LOG_LEVEL_DEBUG, oObject,
                   EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
          else
               Log_Systems("*** Current executing xCommand is: " + IntToString(nCurrentType), EngineConstants.LOG_LEVEL_DEBUG, oObject
                   , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);

          //Uncommented by DHK
          int nSize = GetCommandQueueSize(oObject);
          if (nSize == 0)
          {
               Log_Systems("*** Command queue is currently empty", EngineConstants.LOG_LEVEL_DEBUG, oObject
                   , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
          }
          else
          {
               Log_Systems("*** Command queue size is: " + IntToString(nSize), EngineConstants.LOG_LEVEL_DEBUG, oObject
                   , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
               int i;
               int nType;
               for (i = 0; i < nSize; i++)
               {
                    cCurrent = GetCommandByIndex(oObject, i);
                    nType = GetCommandType(cCurrent);
                    Log_Systems("*** COMMAND[" + IntToString(i) + "]= " + IntToString(nType), EngineConstants.LOG_LEVEL_DEBUG, oObject
                        , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
               }
          }

          //Uncommented by DHK
          Log_Trace(EngineConstants.LOG_CHANNEL_COMMANDS, "*** ADDING NEW COMMAND (" + IntToString(nCommandType) + ")");

          if (GetCommandType(cCommand) == EngineConstants.COMMAND_TYPE_ATTACK)
          {
               Log_Trace_Combat("wrappers_h", "AddCommand(Attack) called from " + GetCurrentScriptName() + " on " + ToString(oObject));
          }

        if (fTimeout > 0.0f && IsFollower(oObject) == EngineConstants.FALSE) // followers can't have xCommand timeout
        {
            SetCommandFloatRef(ref cCommand, fTimeout, 5);
        }

          AddCommand(oObject, cCommand, bAddToFront, bStatic, nOverrideAddBehavior);

          //Uncommented by DHK
          nCurrentType = GetCommandType(GetCurrentCommand(oObject));

          if (nCurrentType == EngineConstants.COMMAND_TYPE_INVALID)
               Log_Systems("*** VERIFY: No xCommand is executing currently", EngineConstants.LOG_LEVEL_DEBUG, oObject,
                   EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);
          else
               Log_Systems("*** VERIFY: executing xCommand is: " + IntToString(nCurrentType), EngineConstants.LOG_LEVEL_DEBUG, oObject
                   , EngineConstants.LOG_SYSTEMS_SUBTYPE_WRAPPER_FUNCTIONS);

     }

     public void WR_SetFollowerState(GameObject oCreature, int nState, int nSendEvent = EngineConstants.TRUE, int nMinLevel = 0, int bPreventLevelup = EngineConstants.FALSE)
     {
          int nOldState = GetFollowerState(oCreature);
          int nOldSubState = GetFollowerSubState(oCreature);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS,
                  "wrappers_h.WR_SetFollowerSubState",
                  "OLD STATE: " + _GetFollowerStateName(nOldSubState), oCreature);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS,
                  "wrappers_h.WR_SetFollowerSubState",
                  "NEW STATE: " + _GetFollowerStateName(nState), oCreature);

          // change xEvent script if the follower is in or out of the party.
          switch (nState)
          {
               case EngineConstants.FOLLOWER_STATE_LOCKEDACTIVE:
               case EngineConstants.FOLLOWER_STATE_ACTIVE:
                    {
                         // remove stationary flag
                         SetLocalInt(oCreature, EngineConstants.AI_FLAG_STATIONARY, 0);
                         // Adding follower to active party
                         if (GetLocalInt(GetModule(), EngineConstants.TUTORIAL_ENABLED) != EngineConstants.FALSE)
                              WR_SetPlotFlag(EngineConstants.PLT_TUT_CONTROL_FOLLOWERS, EngineConstants.TUT_CONTROL_FOLLOWERS_1, EngineConstants.TRUE);
                         SetEventScript(oCreature, EngineConstants.RESOURCE_SCRIPT_PLAYER_CORE);

                         Debug.Log("wrappers_h: originally &, replaced to &&, double check");
                         if (IsPartyPerceivingHostiles(oCreature) != EngineConstants.FALSE && GetCombatState(oCreature) == EngineConstants.FALSE)
                              SetCombatState(oCreature, EngineConstants.TRUE);

                         // Remove from any team
                         SetTeamId(oCreature, -1);

                         // set interactive (in case some very bad script messed it up)
                         SetObjectInteractive(oCreature, EngineConstants.TRUE);

                         // Set player rank
                         SetCreatureRank(oCreature, EngineConstants.CREATURE_RANK_PLAYER);

                         SetImmortal(oCreature, EngineConstants.FALSE);
                         SetPlot(oCreature, EngineConstants.FALSE);
                         if (GetPartyLeader() != oCreature)
                              SetFollowPartyLeader(oCreature, EngineConstants.TRUE);
                         SetLocalInt(oCreature, EngineConstants.AMBIENT_SYSTEM_STATE, 0); // EngineConstants.AMBIENT_SYSTEM_DISABLED

                         InitHeartbeat(oCreature, EngineConstants.CONFIG_CONSTANT_HEARTBEAT_RATE);

                         if (nSendEvent != EngineConstants.FALSE)
                              SendPartyMemberHiredEvent(oCreature, EngineConstants.FALSE, nMinLevel, bPreventLevelup);
                         break;
                    }
               case EngineConstants.FOLLOWER_STATE_AVAILABLE:
               case EngineConstants.FOLLOWER_STATE_INVALID:
               case EngineConstants.FOLLOWER_STATE_UNAVAILABLE:
               case EngineConstants.FOLLOWER_STATE_SUSPENDED:
                    {
                         if (IsSummoned(oCreature) != EngineConstants.FALSE && nState != EngineConstants.FOLLOWER_STATE_INVALID)
                         {
                              Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "WR_SetFollowerState",
                                 "Trying to apply an invalid state to a summoned creature - aborting change");
                              return;
                         }
                         // -----------------------------------------------------------------
                         // Non party members do not get a heartbeat
                         // -----------------------------------------------------------------
                         EndHeartbeat(oCreature);

                         // Remove effects
                         Effects_RemoveAllEffects(oCreature);
                         // -----------------------------------------------------------------
                         // Georg: We got a problem with removing modal effects this person
                         //        might have applied to others. Reason: By the time this
                         //        xEvent fires, they are no longer in the party and can not
                         //        remove ukpeep effects of others. The following code takes
                         //        care of that
                         // -----------------------------------------------------------------
                         List<GameObject> partyList = GetPartyList();
                         int nArraySize = GetArraySize(partyList);
                         int iMember;

                         for (iMember = 0; iMember < nArraySize; iMember++)
                         {
                              RemoveEffectsByParameters(partyList[iMember], EngineConstants.EFFECT_TYPE_UPKEEP, EngineConstants.ABILITY_INVALID, oCreature); //first all upkeep effects
                              RemoveEffectsByParameters(partyList[iMember], EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.ABILITY_INVALID, oCreature); //then all others
                         }

                         SetEventScript(oCreature, EngineConstants.RESOURCE_SCRIPT_CREATURE_CORE);

                         SetGroupId(oCreature, EngineConstants.GROUP_FRIENDLY);

                         // set interactive (in case some very bad script messed it up)
                         SetObjectInteractive(oCreature, EngineConstants.TRUE);

                         if (IsSummoned(oCreature) == EngineConstants.FALSE)
                         {
                              // Setting immortal so follower can't be killed outside of party
                              // Note: a few places in the game allow the follower to be killed outside of the party but they
                              // set the follower script to be player_core so he would rise again when combat over.
                              SetImmortal(oCreature, EngineConstants.TRUE);
                         }

                         // If follower is currently in the ACTIVE party then fire a partry-member-fired event
                         if (nSendEvent != EngineConstants.FALSE)
                         {
                              if (GetFollowerState(oCreature) == EngineConstants.FOLLOWER_STATE_ACTIVE || GetFollowerState(oCreature) == EngineConstants.FOLLOWER_STATE_LOCKEDACTIVE)
                                   SendPartyMemberFiredEvent(oCreature);
                         }
                         break;
                    }

          }

          SetFollowerState(oCreature, nState);

     }

     public void WR_SetFollowerSubState(GameObject oCreature, int nSubState)
     {
          int nOldState = GetFollowerState(oCreature);
          int nOldSubState = GetFollowerSubState(oCreature);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS,
                  "wrappers_h.WR_SetFollowerSubState",
                  "OLD STATE: " + IntToString(nOldState) + ", OLD SUB-STATE: " + IntToString(nOldSubState), oCreature);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS,
                  "wrappers_h.WR_SetFollowerSubState",
                  "NEW SUB-STATE: " + IntToString(nSubState));

          SetFollowerSubState(oCreature, nSubState);
     }

     public void WR_SetGameMode(int nGameMode)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "wrappers_h.WR_SetGameMode", "nGameMode: " + IntToString(nGameMode));

          xEvent evModeChange = Event(EngineConstants.EVENT_TYPE_SET_GAME_MODE);
          SetEventIntegerRef(ref evModeChange, 0, nGameMode);

          SignalEvent(GetModule(), evModeChange);
     }

     public void WR_TogglePlotIcon(GameObject oCreature, int nActive = EngineConstants.TRUE)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "wrappers_h.WR_TogglePlotIcon", "ACTIVE/INACTIVE: " + IntToString(nActive)
              + ", for creature: " + GetTag(oCreature));

          if (IsObjectValid(oCreature) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "wrappers_h.WR_TogglePlotIcon", "INVALID CREATURE OBJECT");
               return;
          }

          if (nActive != EngineConstants.FALSE)
               ShowFloatyIcon(oCreature, "PlotDestination");
          //ShowFloatyIcon(oCreature, "");
          else
               ShowFloatyIcon(oCreature, "");

     }

     public void WR_SetWorldMapLocationStatus(GameObject oLocation, int nStatusId, int bSuppressActiveFlash = EngineConstants.FALSE)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "WR_SetWorldMapLocationStatus", "Location: " + GetTag(oLocation) + ", Status: " + IntToString(nStatusId));
          SetWorldMapLocationStatus(oLocation, nStatusId, bSuppressActiveFlash);
     }

     public void WR_SetWorldMapGuiStatus(int nStatus)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "WR_SetWorldMapGuiStatus", ", Status: " + IntToString(nStatus));
          SetWorldMapGuiStatus(nStatus);
     }

     public void WR_SetWorldMapPlayerLocation(GameObject oMap, GameObject oLocation)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "WR_SetWorldMapPlayerLocation", ", Map: " + GetTag(oMap) + ", Location: " + GetTag(oLocation));
          SetWorldMapPlayerLocation(oMap, oLocation);
     }

     public void WR_SetWorldMapPrimary(GameObject oMapId)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "WR_SetWorldMapPrimary", ", World Map: " + GetTag(oMapId));
          SetWorldMapPrimary(oMapId);
     }

     public void WR_SetWorldMapSecondary(GameObject oMapId)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_WRAPPERS, "WR_SetWorldMapSecondary", ", World Map: " + GetTag(oMapId));
          //SetWorldMapSecondary(oMapId);deprecated: replaced by below
          SetMapSwapEnabled(2, EngineConstants.TRUE);
     }

     public void WR_TakeAutoScreenshot(int flagID, int n2DA, string s2da = "")
     {
          // Does the majority of the work in the auto screenshot system. Takes
          // a plot flag ID and 2DA file information as arguments and takes the
          // screenshot. Should only be used on auto screenshot plot and 2DA files.

          //Grab screenshot information from the 2da file
          int titleStrRef = GetM2DAInt(n2DA, EngineConstants.AUTOSS_2DA_TITLE, flagID, s2da);
          int descStrRef = GetM2DAInt(n2DA, EngineConstants.AUTOSS_2DA_DESC, flagID, s2da);
          string screenshotID = GetM2DAString(n2DA, EngineConstants.AUTOSS_2DA_SSID, flagID, s2da);
          int priority = GetM2DAInt(n2DA, EngineConstants.AUTOSS_2DA_PRIORITY, flagID, s2da);
          int isOverride = GetM2DAInt(n2DA, EngineConstants.AUTOSS_2DA_OVERRIDE, flagID, s2da);

          //Strings for logging info
          string title = GetStringByStringId(titleStrRef);
          string desc = GetStringByStringId(descStrRef);

          //Dump the screenshot data for the log
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", "AUTOSCREENSHOTBEGIN");
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", screenshotID);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", title);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", desc);
          switch (priority)
          {
               case EngineConstants.AUTOSS_PRIORITY_DISABLED:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", "Priority: disabled");
                         break;
                    }
               case EngineConstants.AUTOSS_PRIORITY_PRIMARY:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", "Priority: primary (always fires)");
                         break;
                    }
               case EngineConstants.AUTOSS_PRIORITY_SECONDARY:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", "Priority: secondary");
                         break;
                    }
               case EngineConstants.AUTOSS_PRIORITY_TERTIARY:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", "Priority: tertiary");
                         break;
                    }
               case EngineConstants.AUTOSS_PRIORITY_TOTAL_FLUFF:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", "Priority: total fluff");
                         break;
                    }
          }
          if (isOverride != 0)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", "*** Overridden, xEvent sent without screenshot");
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "wrappers_h.WR_TakeAutoScreenshot", "AUTOSCREENSHOTEND");

          //Take the screenshot
          if (priority != EngineConstants.AUTOSS_PRIORITY_DISABLED) //Make sure it hasn't been disabled
          {
               if (isOverride == 0)
               {
                    //Check the game mode. We only want autoscreenshots taken in combat, exploration, dialog, or cutscene.
                    switch (GetGameMode())
                    {
                         case EngineConstants.GM_COMBAT:
                         //case EngineConstants.GM_CUTSCENE:deprecated
                         case EngineConstants.GM_CONVERSATION:
                         case EngineConstants.GM_EXPLORE:
                              {
                                   //Send it with the correct priority: not all screenshots are going to show up
                                   if (priority <= EngineConstants.AUTOSS_PRIORITY_CUTOFF)
                                        TakeScreenshot(EngineConstants.TRUE, titleStrRef, descStrRef, EngineConstants.AUTOSS_SCREENSHOT_STORY_HIGH_PRIORITY, screenshotID);

                                   else
                                        TakeScreenshot(EngineConstants.TRUE, titleStrRef, descStrRef, EngineConstants.AUTOSS_SCREENSHOT_STORY_LOW_PRIORITY, screenshotID);
                                   break;
                              }
                         default:
                              break;
                    }
               }
               else
               {
                    // Only sending an event, not a screenshot... this allows for a
                    // canned screenshot to be inserted
                    LogStoryEvent(StringToInt(screenshotID));
               }

          }
     }
}