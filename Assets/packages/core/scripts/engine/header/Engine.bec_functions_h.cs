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
     //#include"bec_constants_h"

     //#include"PLT_BEC100PT_SORIS"
     //#include"PLT_BEC000PT_MAIN"

     public int BEC_GameModeChanged(int nGameMode)
     {
          int bEventHandled = EngineConstants.FALSE;

          switch (nGameMode)
          {
               case EngineConstants.GM_DEAD:
                    {
                         break;
                    }
               case EngineConstants.GM_EXPLORE:
                    {
                         GameObject oPC = GetHero();
                         GameObject oSoris = UT_GetNearestCreatureByTag(oPC, EngineConstants.BEC_CR_SORIS);

                         if (WR_GetPlotFlag(EngineConstants.PLT_BEC100PT_SORIS, EngineConstants.BEC_SORIS_TALK_AFTER_DEAD_NELAROS_FOUND) != EngineConstants.FALSE
                             || WR_GetPlotFlag(EngineConstants.PLT_BEC100PT_SORIS, EngineConstants.BEC_SORIS_TALK_AFTER_DEAD_BRIDESMAID_FOUND) != EngineConstants.FALSE
                             || WR_GetPlotFlag(EngineConstants.PLT_BEC100PT_SORIS, EngineConstants.BEC_SORIS_TALK_AFTER_FIRST_GUARD_FIGHT) != EngineConstants.FALSE)
                         {
                              //UT_Talk(oSoris, oPC);
                         }
                         else if (WR_GetPlotFlag(EngineConstants.PLT_BEC100PT_SORIS, EngineConstants.BEC_SORIS_TALK_AFTER_VAUGHAN_KILLED) != EngineConstants.FALSE)
                         {
                              //                WR_SetPlotFlag(EngineConstants.PLT_BEC000PT_MAIN,BEC_MAIN_VAUGHAN_DEAD,EngineConstants.TRUE);
                              //                UT_Talk(oSoris,oPC);
                         }
                         break;
                    }
               case EngineConstants.GM_COMBAT:
                    {
                         break;
                    }
          }
          return bEventHandled;
     }
}