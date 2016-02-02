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
     //This function sets the correct story so far variable for the Nature of the Beast plot.

     //#include"utility_h"

     //#include"plt_mnp00pt_ssf_nature"

     //#include"plt_ntb000pt_main"
     //#include"plt_ntb000pt_generic"
     //#include"plt_ntb000pt_plot_items"
     //#include"plt_ntb340pt_lady"
     //#include"plt_ntb100pt_zathrian"

     public void NTB_HandleStorySoFar()
     {
          int nMet = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_PC_TOLD_TO_FIND_HEART, EngineConstants.TRUE);
          int nForest = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_GENERIC, EngineConstants.NTB_GENERIC_PC_ENTERED_FOREST, EngineConstants.TRUE);
          int nRuins = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_GENERIC, EngineConstants.NTB_GENERIC_PC_ENTERED_RUINS, EngineConstants.TRUE);
          int nHeart = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_PLOT_ITEMS, EngineConstants.NTB_PLOT_ITEMS_PC_GETS_WITHERFANG_HEART, EngineConstants.TRUE);
          int nFetch = WR_GetPlotFlag(EngineConstants.PLT_NTB340PT_LADY, EngineConstants.NTB_LADY_PC_NEEDS_TO_BRING_ZATHRIAN, EngineConstants.TRUE);
          int nWerewolfSide = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_PC_SIDES_WITH_WEREWOLVES_AGAINST_ELVES, EngineConstants.TRUE);
          int nZathrianHeart1 = WR_GetPlotFlag(EngineConstants.PLT_NTB100PT_ZATHRIAN, EngineConstants.NTB_ZATHRIAN_RETURNS_TO_CAMP, EngineConstants.TRUE);
          int nZathrianHeart2 = WR_GetPlotFlag(EngineConstants.PLT_NTB100PT_ZATHRIAN, EngineConstants.NTB_ZATHRIAN_RETURNS_TO_CAMP_WITH_PC, EngineConstants.TRUE);
          int nZathrianKill = WR_GetPlotFlag(EngineConstants.PLT_NTB100PT_ZATHRIAN, EngineConstants.NTB_ZATHRIAN_HELPED_KILL_WITHERFANG, EngineConstants.TRUE);
          int nZathrianKilled = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ZATHRIAN_KILLED_BY_PC, EngineConstants.TRUE);
          int nZathrianSacrificed = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ZATHRIAN_SACRIFICES_HIMSELF, EngineConstants.TRUE);
          int nElvesDestroyed = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ELVES_DEFEATED, EngineConstants.TRUE);
          int nWerewolvesFlee = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_HUMANS_ESCAPE, EngineConstants.TRUE);
          int nElvesAlliance = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ELVES_PROMISED_ALLIANCE, EngineConstants.TRUE);

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "ntb_ssf_h", "NTB_HandleStorySoFar checked");

          //----------------------------------------------------------------------
          //if the elves have allied with the PC
          //set the ssf depending on whether the PC made the humans flee 
          //or the PC returned in PC
          //----------------------------------------------------------------------
          if (nElvesAlliance != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "ntb_ssf_h", "ElvesAlliance checked");

               if (nWerewolvesFlee != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_06B3B_KILL_HUMANS_BEFORE_RETURN, EngineConstants.TRUE, EngineConstants.TRUE);
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "ntb_ssf_h", "Sets that the PC has returned in peace");

                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_06B3A_RETURN_IN_PEACE, EngineConstants.TRUE, EngineConstants.TRUE);
               }
          }
          //----------------------------------------------------------------------
          //if the PC sided with the werewolves and killed all the elves
          //----------------------------------------------------------------------
          else if (nElvesDestroyed != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_05C_ELVES_DESTROYED, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if Zathrian sacrificed himself
          //----------------------------------------------------------------------
          else if (nZathrianSacrificed != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_05B3_SACRIFICE_ZATHRIAN, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if the PC just killed Zathrian after killing Witherfang
          //----------------------------------------------------------------------
          else if (nZathrianKilled != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "ntb_ssf_h", "Sets that the PC has killed Zathrian & Witherfang");

               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_05B2_KILL_WITHERFANG_AND_ZATHRIAN, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if the PC helped Zathrian kill witherfang
          //----------------------------------------------------------------------
          else if (nZathrianKill != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "ntb_ssf_h", "Sets that the PC has killed Witherfang with Zathrian's help");

               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_05B1_KILL_WITHERFANG_WITH_ZATHRIAN, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if Zathrian just has Witherfang's heart
          //----------------------------------------------------------------------
          else if ((nZathrianHeart1 != EngineConstants.FALSE) || (nZathrianHeart2 != EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_05A_GIVE_ZATHRIAN_HEART, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if the PC has sided with the werewolves
          //----------------------------------------------------------------------
          else if (nWerewolfSide != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_04C_JOIN_WEREWOLVES_AGAINST_ELVES, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if the PC has gone to bring Zathrian back to the Lady
          //----------------------------------------------------------------------
          else if (nFetch != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_04B_GET_ZATHRIAN, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if the PC has killed the Lady and has her heart
          //----------------------------------------------------------------------
          else if (nHeart != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_04A_KILL_LADY_OF_FOREST, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if the PC has entered the ruins
          //----------------------------------------------------------------------
          else if (nRuins != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_03_ENTER_RUINS, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if the PC has entered the forest
          //----------------------------------------------------------------------
          else if (nForest != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_02_ENTER_FOREST, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //----------------------------------------------------------------------
          //if the PC has met the Dalish
          //----------------------------------------------------------------------
          else if (nMet != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_NATURE, EngineConstants.SSF_NTB_01_MEET_DALISH, EngineConstants.TRUE, EngineConstants.TRUE);
          }
     }
}