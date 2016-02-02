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

         Dwarf Noble
          -> Generic Functions Script

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: August 23, 2007
     //==============================================================================

     //#include"plt_bdnpt_main"
     //#include"plt_bdn120pt_gorim"
     //#include"plt_bdn200pt_expedition"

     //#include"bdn_constants_h"

     //#include"utility_h"

     public void BDN_ItemAcquired(string sItemTag)
     {

          GameObject oPC = GetHero();

          if (sItemTag == ResourceToTag(EngineConstants.BDN_IM_AEDUCAN_SHIELD_R))
          {
               WR_SetPlotFlag(EngineConstants.PLT_BDN200PT_EXPEDITION, EngineConstants.BDN_EXPEDITION___PLOT_02_SHIELD_FOUND, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          if (sItemTag == ResourceToTag(EngineConstants.BDN_IM_TRIANS_RING_R))
          {
               GameObject oGorim = UT_GetNearestCreatureByTag(oPC, EngineConstants.BDN_CR_GORIM);
               WR_SetPlotFlag(EngineConstants.PLT_BDN120PT_GORIM, EngineConstants.BDN_GORIM__EVENT_FOUND_TRIANS_RING, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_ClearAllCommands(oPC);
               WR_ClearAllCommands(oGorim);
               UT_Talk(oGorim, oPC);
          }

     }

     public int BDN_CountTeam(int nTeamId, int bAlive = EngineConstants.TRUE)
     {

          int nIndex;
          int nArraySize;
          int nCount = 0;
          List<GameObject> arTeam = UT_GetTeam(nTeamId);

          nArraySize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               if (IsDead(arTeam[nIndex]) == EngineConstants.FALSE || bAlive == EngineConstants.FALSE)
                    nCount++;
          }

          return nCount;

     }

     public int BDN_GetPartyListIndex(GameObject oPartyMember)
     {

          int nIndex;
          int nArraySize;
          GameObject oPC = GetHero();
          List<GameObject> arPartyList = GetPartyList(oPC);

          nArraySize = GetArraySize(arPartyList);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               if (oPartyMember == arPartyList[nIndex])
                    return nIndex;
          }

          return -1;

     }
}