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
     //public void main() {} // MUST ALWAYS BE COMMENTED OUT. THIS IS FOR DEBUG ONLY
     //
     // achievement_sp_h
     // Includes all Single Player achievement-related functions

     //#include"achievement_core_h"

     // Army plots
     //#include"plt_ntb000pt_main"
     //#include"plt_orzpt_main"
     //#include"plt_cir000pt_main"

     // Check the validity of an army troop for the Denerim army achievement
     public int ACH_IsArmyValid(string sPlot, int nFlag, int nArmy)
     {
          int bReturn=0;
          // If army was recruited via plot flag and if army is not disabled
          if (WR_GetPlotFlag(sPlot, nFlag) != EngineConstants.FALSE && GetPlotActionState(nArmy) != EngineConstants.PLOT_ACTIONSTATE_DISABLED)
          {
               bReturn = EngineConstants.TRUE;
          }
          else if (WR_GetPlotFlag(sPlot, nFlag) == EngineConstants.FALSE)
          {
               bReturn = EngineConstants.FALSE;
          }
          return bReturn;
     }

     // Check starting and current counts; compare for achievements
     public void ACH_CompareTroopCounts()
     {
          // Starting counts
          int nWerewolfCount=0;
          int nElfCount=0;
          int nDwarfCount=0;
          int nGolemCount=0;
          int nMageCount=0;
          int nTemplarCount=0;
          int nSoldierCount=0;

          // Ending counts
          int nEndWerewolfCount=0;
          int nEndElfCount=0;
          int nEndDwarfCount=0;
          int nEndGolemCount=0;
          int nEndMageCount=0;
          int nEndTemplarCount=0;
          int nEndSoldierCount=0;

          // Validity logic
          if (ACH_IsArmyValid(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_WEREWOLVES_PROMISED_ALLIANCE, EngineConstants.PLOT_ACTION_ARMY_WEREWOLVES) != EngineConstants.FALSE)
          {
               nWerewolfCount = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxCount", EngineConstants.PLOT_ACTION_ARMY_WEREWOLVES);
               nEndWerewolfCount = GetPlotActionCount(EngineConstants.PLOT_ACTION_ARMY_WEREWOLVES);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, ToString(nWerewolfCount) + "/" + ToString(nEndWerewolfCount) + " werewolves are in the army.");
          }
          if (ACH_IsArmyValid(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ELVES_PROMISED_ALLIANCE, EngineConstants.PLOT_ACTION_ARMY_ELVES) != EngineConstants.FALSE)
          {
               nElfCount = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxCount", EngineConstants.PLOT_ACTION_ARMY_ELVES);
               nEndElfCount = GetPlotActionCount(EngineConstants.PLOT_ACTION_ARMY_ELVES);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, ToString(nElfCount) + "/" + ToString(nEndElfCount) + " elves are in the army.");
          }
          if (ACH_IsArmyValid(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN_ARMY_WILL_INCLUDE_DWARFS, EngineConstants.PLOT_ACTION_ARMY_DWARVES) != EngineConstants.FALSE)
          {
               nDwarfCount = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxCount", EngineConstants.PLOT_ACTION_ARMY_DWARVES);
               nEndDwarfCount = GetPlotActionCount(EngineConstants.PLOT_ACTION_ARMY_DWARVES);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, ToString(nDwarfCount) + "/" + ToString(nEndDwarfCount) + " dwarves are in the army.");
          }
          if (ACH_IsArmyValid(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN_ARMY_WILL_INCLUDE_GOLEMS, EngineConstants.PLOT_ACTION_ARMY_GOLEMS) != EngineConstants.FALSE)
          {
               nGolemCount = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxCount", EngineConstants.PLOT_ACTION_ARMY_GOLEMS);
               nEndGolemCount = GetPlotActionCount(EngineConstants.PLOT_ACTION_ARMY_GOLEMS);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, ToString(nGolemCount) + "/" + ToString(nEndGolemCount) + " golems are in the army.");
          }
          if (ACH_IsArmyValid(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.MAGES_IN_ARMY, EngineConstants.PLOT_ACTION_ARMY_WIZARDS) != EngineConstants.FALSE)
          {
               nMageCount = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxCount", EngineConstants.PLOT_ACTION_ARMY_WIZARDS);
               nEndMageCount = GetPlotActionCount(EngineConstants.PLOT_ACTION_ARMY_WIZARDS);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, ToString(nMageCount) + "/" + ToString(nEndMageCount) + " mages are in the army.");
          }
          if (ACH_IsArmyValid(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.TEMPLARS_IN_ARMY, EngineConstants.PLOT_ACTION_ARMY_TEMPLARS) != EngineConstants.FALSE)
          {
               nTemplarCount = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxCount", EngineConstants.PLOT_ACTION_ARMY_TEMPLARS);
               nEndTemplarCount = GetPlotActionCount(EngineConstants.PLOT_ACTION_ARMY_TEMPLARS);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, ToString(nTemplarCount) + "/" + ToString(nEndTemplarCount) + " templars are in the army.");
          }
          if (GetPlotActionState(EngineConstants.PLOT_ACTION_ARMY_REDCLIFFE) != EngineConstants.PLOT_ACTIONSTATE_DISABLED)
          {
               nSoldierCount = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxCount", EngineConstants.PLOT_ACTION_ARMY_REDCLIFFE);
               nEndSoldierCount = GetPlotActionCount(EngineConstants.PLOT_ACTION_ARMY_REDCLIFFE);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, ToString(nSoldierCount) + "/" + ToString(nEndSoldierCount) + " soldiers are in the army.");
          }

          // Sum of start counts
          int nStartCount = nWerewolfCount + nElfCount + nDwarfCount + nGolemCount + nMageCount + nTemplarCount + nSoldierCount;
          ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "TOTAL start count = " + ToString(nStartCount));
          // Sum of end counts
          int nEndCount = nEndWerewolfCount + nEndElfCount + nEndDwarfCount + nEndGolemCount + nEndMageCount + nEndTemplarCount + nEndSoldierCount;
          ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "TOTAL end count = " + ToString(nEndCount));

          // Achievement logic; half or more troops gives minor
          if (nEndCount >= (nStartCount / 2)) WR_UnlockAchievement(EngineConstants.ACH_FEAT_DEFENDER);
     }
}