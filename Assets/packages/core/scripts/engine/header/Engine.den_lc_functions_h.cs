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
         den_lc_functions_h.nss

         Denerim LC functions.
     */
     //==============================================================================

     //#include"den_constants_h"
     //#include"utility_h"
     //#include"wrappers_h"
     //#include"sys_traps_h" 

     //#include"plt_nrdpt_drake_scales"
     //#include"den_lc_constants_h"

     //------------------------------------------------------------------------------

     public GameObject DEN_GetRandomPartyMember(GameObject oCreature)
     {

          List<GameObject> arPartyList = GetPartyList(oCreature);

          int nPartySize = GetArraySize(arPartyList);
          int nRand = Engine_Random(nPartySize);

          GameObject oPartyMember = arPartyList[nRand];

          return oPartyMember;

     }

     public void DEN_TriggerTrap(GameObject oTrapTarget, string sTrapTag)
     {

          xEvent evTrigger = Event(EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ENTER);

          GameObject oTrap = UT_GetNearestObjectByTag(oTrapTarget, sTrapTag);

          WR_SetObjectActive(oTrap, EngineConstants.TRUE);

          //Trap_HandleEventTriggerEntered(evTrigger);

          //Trap_Triggered(416, oTrapTarget, GetLocation(oTrap), oTrap);

          //WR_SetObjectActive(oTrap, EngineConstants.FALSE);

     }

     public void DEN_WadeArmorReady()
     {

          int bCraftDrakeArmor1 = WR_GetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.CRAFT_FIRST_DRAKE_ARMOR);
          int bCraftDrkArmor1Done = WR_GetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.CRAFT_FIRST_DRAKE_ARMOR_DONE);
          int bCraftTimeSlow = WR_GetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.PC_CHOSE_SLOW_CRAFTING);

          // IF the player has ordered their first set of drakescale armor
          // AND they have NOT received it yet
          if (bCraftDrakeArmor1 != EngineConstants.FALSE && bCraftDrkArmor1Done == EngineConstants.FALSE)
          {

               WR_SetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.JOURNAL_FIRST_DRAKE_ARMOR_DONE, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.TIME_PASSED_SLOW, EngineConstants.TRUE, EngineConstants.TRUE);

          }

          int bCraftDrakeArmor2 = WR_GetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.CRAFT_SECOND_DRAKE_ARMOR);
          int bCraftDrkArmor2Done = WR_GetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.CRAFT_SECOND_DRAKE_ARMOR_DONE);
          int bCraftTimeSlow2 = WR_GetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.PC_CHOSE_SLOW_CRAFTING_AGAIN);

          // IF the player has ordered their second set of drakescale armor
          // AND they have NOT received it yet
          if (bCraftDrakeArmor2 != EngineConstants.FALSE && bCraftDrkArmor2Done == EngineConstants.FALSE)
          {

               WR_SetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.JOURNAL_SECOND_DRAKE_ARMOR_DONE, EngineConstants.TRUE, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_NRDPT_DRAKE_SCALES, EngineConstants.TIME_PASSED_SLOW, EngineConstants.TRUE, EngineConstants.TRUE);

          }

     }
}