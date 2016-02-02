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
     //This function sets the correct story so far variable for the Arl Eamon plot.

     //#include"plt_arl000pt_contact_eamon"
     //#include"plt_arl100pt_siege_prep"
     //#include"plt_arl100pt_siege"
     //#include"plt_arl100pt_enter_castle"
     //#include"plt_arl000pt_talked_to"
     //#include"plt_arl200pt_remove_demon"
     //#include"plt_mnp00pt_ssf_arl_eamon"
     //#include"utility_h"

     public void ARL_HandleStorySoFar()
     {
          int bAgreedToHelp = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_PC_AGREED_TO_HELP);
          int bRefusedToHelp = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_PC_REFUSED_TO_HELP);
          int bVillageAbandoned = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_VILLAGE_ABANDONED);
          int bBattleWon = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE, EngineConstants.ARL_SIEGE_SIEGE_OVER);
          int bJowanFound = WR_GetPlotFlag(EngineConstants.PLT_ARL000PT_TALKED_TO, EngineConstants.ARL_TALKED_TO_JOWAN);
          int bConnorFound = WR_GetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_TALKED_ABOUT_DEMON);
          int bDemonDealtWith = WR_GetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_DEMON_DEALT_WITH);
          int bConnorKilled = WR_GetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_CONNOR_DEFEATED);
          int bCircleUsed = WR_GetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_CIRCLE_DOES_RITUAL);
          int bBloodMagicUsed = WR_GetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_JOWAN_DOES_RITUAL);
          int bTeaganRevived = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE, EngineConstants.ARL_SIEGE_TEAGAN_RUNS_OUT_TO_FIND_VILLAGE_DESTROYED);
          int bEamonRestored = WR_GetPlotFlag(EngineConstants.PLT_ARL000PT_CONTACT_EAMON, EngineConstants.ARL_CONTACT_EAMON_EAMON_REVIVED);
          int bLandsmeetStarted = WR_GetPlotFlag(EngineConstants.PLT_ARL000PT_CONTACT_EAMON, EngineConstants.ARL_CONTACT_EAMON_START_LANDSMEET_PLOT);

          if (bLandsmeetStarted!= EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_07_LANDSMEET_STARTED, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (bEamonRestored != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_06_RESTORED_EAMON, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (bDemonDealtWith != EngineConstants.FALSE)
          {
               if (bCircleUsed != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_05C_ENLISTED_CIRCLE_MAGES, EngineConstants.TRUE, EngineConstants.TRUE);
               }
               else if (bBloodMagicUsed != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_05A_USED_BLOOD_MAGIC, EngineConstants.TRUE, EngineConstants.TRUE);
               }
               else if (bConnorKilled != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_05B_KILLED_CONNOR, EngineConstants.TRUE, EngineConstants.TRUE);
               }
          }
          else if (bConnorFound != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_04_FOUND_CONNOR, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (bJowanFound != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_03_MET_JOWAN, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (bVillageAbandoned != EngineConstants.FALSE)
          {
               if (bTeaganRevived != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_02B2_SPOKE_TO_TEAGAN, EngineConstants.TRUE, EngineConstants.TRUE);
               }
               else
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_02B1_ABANDONED_VILLAGE, EngineConstants.TRUE, EngineConstants.TRUE);
               }
          }
          else if (bBattleWon != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_02A_DEFEATED_UNDEAD, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (bAgreedToHelp != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_01A_AGREED_TO_HELP, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (bRefusedToHelp != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_ARL_EAMON, EngineConstants.SSF_ARL_01B_REFUSED_TO_HELP, EngineConstants.TRUE, EngineConstants.TRUE);
          }
     }
}