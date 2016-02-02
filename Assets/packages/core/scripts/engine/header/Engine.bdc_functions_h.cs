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
     //==============================================================================
     /*

         Dwarf Commoner
          -> Generic Functions Script

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: August 23, 2007
     //==============================================================================

     //#include"plt_bdcpt_main"
     //#include"plt_bdc130pt_oskias"
     //#include"plt_cod_mgc_lyrium"

     //#include"bdc_constants_h"

     //#include"utility_h"

     public void BDC_ItemAcquired(string sItemTag)
     {
          GameObject oPC = GetHero();

          if (sItemTag == ResourceToTag(EngineConstants.BDC_IM_LYRIUM_ORE_R))
          {
               WR_SetPlotFlag(EngineConstants.PLT_COD_MGC_LYRIUM, EngineConstants.COD_MGC_LYRIUM, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_BDC130PT_OSKIAS, EngineConstants.BDC_OSKIAS_PC_DID_LOOT_LYRIUM, EngineConstants.TRUE, EngineConstants.TRUE);
          }

     }

     public void UT_SetTeamPlot(int nTeamID, int bPlot, int nMembersType = EngineConstants.OBJECT_TYPE_CREATURE)
     {

          int nIndex;
          GameObject oPC = GetHero();
          List<GameObject> arTeam = UT_GetTeam(nTeamID, nMembersType);

          for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
               SetPlot(arTeam[nIndex], bPlot);

          if (nIndex == EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_SetTeamPlot",
                   "No team members found for TeamID #" + ToString(nTeamID));

          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_SetTeamPlot",
                   "Team ID #" + ToString(nTeamID) + " has been set to plot. " +
                   ToString(nIndex) + " objects have been affected");
     }
}