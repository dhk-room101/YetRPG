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

         Paragon of Her Kind
          -> Generic Functions Script

         These are the generic functions for the Paragon plot.

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: August 23, 2007
     //==============================================================================

     //#include"plt_orzpt_carta"
     //#include"plt_orzpt_wfharrow_da"
     //#include"plt_orz260pt_baizyl"
     //#include"plt_orz310pt_orta"
     //#include"plt_genpt_shale_main"
     //#include"plt_orzpt_knows_about"
     //#include"plt_mnp00pt_ssf_paragon"
     //#include"orz_constants_h"

     //#include"plt_gen00pt_backgrounds"

     //#include"campaign_h"
     //#include"utility_h"


     //==============================================================================
     // FUNCTION IMPLEMENTATION
     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_ActivateWideOpenWorldMap
     //------------------------------------------------------------------------------
     /*
     * @brief Activates WOW Map
     *
     * Makes the Wide Open World Map the Primary Map
     *
     * @author   Joshua Stiksma
     *
     **/
     public void ORZ_ActivateWideOpenWorldMap()
     {

          GameObject oWideOpenWorldMap;

          oWideOpenWorldMap = GetObjectByTag(EngineConstants.WM_WOW_TAG);
          GameObject oInvalid = null;

          WR_SetWorldMapPrimary(oWideOpenWorldMap);
          WR_SetWorldMapSecondary(oInvalid);

     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_ActivateUndergroundMap
     //------------------------------------------------------------------------------
     /*
     * @brief Activates UG Map
     *
     * Makes the Underground Map the Primary Map
     *
     * @author   Joshua Stiksma
     *
     **/
     public void ORZ_ActivateUndergroundMap()
     {

          GameObject oUndergroundMap;
          GameObject oWideOpenWorldMap;

          oUndergroundMap = GetObjectByTag(EngineConstants.WM_UND_TAG);
          oWideOpenWorldMap = GetObjectByTag(EngineConstants.WM_WOW_TAG);

          WR_SetWorldMapPrimary(oUndergroundMap);
          WR_SetWorldMapSecondary(oWideOpenWorldMap);

     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_ActivatePinCaridinsCross
     //------------------------------------------------------------------------------
     /*
     * @brief Activates Caridin's Cross
     *
     * Activates Caridin's Cross pin on the Underground Map so the player can
     * go there.
     *
     **/
     public void ORZ_ActivatePinCaridinsCross()
     {

          GameObject oCaridinPin;

          oCaridinPin = GetObjectByTag(EngineConstants.WML_UND_CARIDINS_CROSS);

          WR_SetWorldMapLocationStatus(oCaridinPin, 2);

     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_ActivatePinDeadTrenches
     //------------------------------------------------------------------------------
     /*
     * @brief Activates The Dead Trenches
     *
     * Activates The Dead Trenches pin on the Underground Map so the player can
     * go there.
     *
     * @param nStatus new pin status
     *
     * @author   Joshua Stiksma
     *
     **/
     public void ORZ_ActivatePinDeadTrenches(int nStatus = 2)
     {

          GameObject oDeadTrenchesPin;

          oDeadTrenchesPin = GetObjectByTag(EngineConstants.WML_UND_DEAD_TRENCHES);

          WR_SetWorldMapLocationStatus(oDeadTrenchesPin, nStatus);

     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_ItemAcquired
     //------------------------------------------------------------------------------
     /*
     * @brief Handles events for when certain items are acquired in the Paragon plot
     *
     * Used to Activate the Runner Custom AI on a creature that is currently
     * in combat.
     *
     * @param sItemTag The tag of the item that was just acquired by the PC.
     *
     * @author   Joshua Stiksma
     *
     **/
     public void ORZ_ItemAcquired(string sItemTag)
     {

          if (sItemTag == ResourceToTag(EngineConstants.ORZ_IM_CARTA_KEY_R))
          {
               if (WR_GetPlotFlag(EngineConstants.PLT_ORZPT_CARTA, EngineConstants.ORZ_CARTA___PLOT_03_PC_HAS_FINGER_BONE_KEY) == EngineConstants.FALSE)
                    WR_SetPlotFlag(EngineConstants.PLT_ORZPT_CARTA, EngineConstants.ORZ_CARTA___PLOT_03_PC_HAS_FINGER_BONE_KEY, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          else if (sItemTag == ResourceToTag(EngineConstants.ORZ_IM_TRIAN_EVIDENCE_R))
          {
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_WFHARROW_DA, EngineConstants.ORZ_WFHDA___PLOT_03_RETURN, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          else if (sItemTag == ResourceToTag(EngineConstants.ORZ_IM_BAIZYL_LETTERS_R))
          {
               WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_BAIZYL, EngineConstants.ORZ_BAIZYL___PLOT_03_PC_GOT_BLACKMAIL_LETTERS, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          else if (sItemTag == ResourceToTag(EngineConstants.ORZ_IM_ORTAN_RECORDS_R))
          {
               if (WR_GetPlotFlag(EngineConstants.PLT_ORZ310PT_ORTA, EngineConstants.ORZ_ORTA___PLOT_01A_ACCEPTED) != EngineConstants.FALSE)
                    WR_SetPlotFlag(EngineConstants.PLT_ORZ310PT_ORTA, EngineConstants.ORZ_ORTA___PLOT_02_PC_RETRIEVED_RECORDS, EngineConstants.TRUE, EngineConstants.TRUE);
               else
                    WR_SetPlotFlag(EngineConstants.PLT_ORZ310PT_ORTA, EngineConstants.ORZ_ORTA___PLOT_01B_PC_FOUND_RECORDS, EngineConstants.TRUE, EngineConstants.TRUE);
          }

     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_SetupGroupHostility
     //------------------------------------------------------------------------------
     /*
     * @brief Setup Group Hostility for Paragon
     *
     * Setup Group Hostility for the various NPC groups defined in Paragon.
     *
     * @author   Joshua Stiksma
     *
     **/
     public void ORZ_SetupGroupHostility()
     {

          SetGroupHostility(EngineConstants.GROUP_PC, EngineConstants.ORZ_GROUP_HOSTILE_DARKSPAWN, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.GROUP_PC, EngineConstants.ORZ_GROUP_HOSTILE_DEEPSTALKERS, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.GROUP_PC, EngineConstants.ORZ_GROUP_HOSTILE_SPIDERS, EngineConstants.TRUE);

          SetGroupHostility(EngineConstants.GROUP_HOSTILE, EngineConstants.ORZ_GROUP_LEGION, EngineConstants.TRUE);

          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_DARKSPAWN, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_DARKSPAWN, EngineConstants.ORZ_GROUP_HOSTILE_DEEPSTALKERS, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_DARKSPAWN, EngineConstants.ORZ_GROUP_HOSTILE_SPIDERS, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_DARKSPAWN, EngineConstants.ORZ_GROUP_LEGION, EngineConstants.TRUE);

          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_DEEPSTALKERS, EngineConstants.ORZ_GROUP_LEGION, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_DEEPSTALKERS, EngineConstants.ORZ_GROUP_HOSTILE_SPIDERS, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_DEEPSTALKERS, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE);

          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_SPIDERS, EngineConstants.ORZ_GROUP_LEGION, EngineConstants.TRUE);
          SetGroupHostility(EngineConstants.ORZ_GROUP_HOSTILE_SPIDERS, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE);

     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_SetupBackgroundKA
     //------------------------------------------------------------------------------
     /*
     * @brief Setup what the PC should Know about in Paragon
     *
     * Checks if the player is either a Dwarven Commoner or Dwarven Noble and makes
     * sure that he knows about certain things in Orzammar to keep the NPC's from
     * mentioning them as if the player didn't grow up there.
     *
     * @author   Joshua Stiksma
     *
     **/
     public void ORZ_SetupBackgroundKA()
     {

          int bBackgroundDwarfNoble = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_NOBLE);
          int bBackgroundDwarfCommoner = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_COMMONER);

          if (bBackgroundDwarfNoble != EngineConstants.FALSE)
          {

               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_DEEPROADS, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_TAIG, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_DESHYR, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_CASTES, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_SHAPERATE, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_PROVING, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_PARAGON, EngineConstants.TRUE);

          }

          else if (bBackgroundDwarfCommoner != EngineConstants.FALSE)
          {

               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_DEEPROADS, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_PROVING, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_JARVIA, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_CASTES, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_ORZPT_KNOWS_ABOUT, EngineConstants.ORZ_KA_PARAGON, EngineConstants.TRUE);

          }

     }

     public List<int> ORZ_GetSSFPlotFlags()
     {
          List<int> SSF_Flags = new List<int>();
          SSF_Flags[0] = EngineConstants.SSF_ORZ_09A_DONE_BHELEN;
          SSF_Flags[1] = EngineConstants.SSF_ORZ_09B_DONE_HARROWMONT;
          SSF_Flags[2] = EngineConstants.SSF_ORZ_08A_ANVIL_RESOLVED_BRANKA_ALIVE;
          SSF_Flags[3] = EngineConstants.SSF_ORZ_08B_ANVIL_RESOLVED_BRANKA_DEAD;
          SSF_Flags[4] = EngineConstants.SSF_ORZ_08C_ANVIL_RESOLVED_CARIDIN;
          SSF_Flags[5] = EngineConstants.SSF_ORZ_07_TRAPPED_BY_BRANKA;
          SSF_Flags[6] = EngineConstants.SSF_ORZ_06_BRANKA_FOUND;
          SSF_Flags[7] = EngineConstants.SSF_ORZ_05_GO_TO_DEAD_TRENCHES;
          SSF_Flags[8] = EngineConstants.SSF_ORZ_04C_BOTH_TASK_3;
          SSF_Flags[9] = EngineConstants.SSF_ORZ_04A_BHELEN_TASK_3;
          SSF_Flags[10] = EngineConstants.SSF_ORZ_04B_HARROWMONT_TASK_3;
          SSF_Flags[11] = EngineConstants.SSF_ORZ_03C_BHELEN_TASK_DA;
          SSF_Flags[12] = EngineConstants.SSF_ORZ_03D_HARROWMONT_TASK_DA;
          SSF_Flags[13] = EngineConstants.SSF_ORZ_03A_BHELEN_TASK_2;
          SSF_Flags[14] = EngineConstants.SSF_ORZ_03B_HARROWMONT_TASK_2;
          SSF_Flags[15] = EngineConstants.SSF_ORZ_02C_BOTH_TASK_1;
          SSF_Flags[16] = EngineConstants.SSF_ORZ_02B_HARROWMONT_TASK_1;
          SSF_Flags[17] = EngineConstants.SSF_ORZ_02A_BHELEN_TASK_1;
          SSF_Flags[18] = EngineConstants.SSF_ORZ_01_ENTERED_ORZAMMAR;
          SSF_Flags[19] = EngineConstants.SSF_ORZ_00_ENTERED_MOUNTAIN_PASS;
          return SSF_Flags;
     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_ModulePresave
     //------------------------------------------------------------------------------

     public void ORZ_ModulePresave()
     {
          string sSSFPlot = EngineConstants.PLT_MNP00PT_SSF_PARAGON;
          List<int> SSF_Flags = ORZ_GetSSFPlotFlags();
          int i, size = GetArraySize(SSF_Flags);
          for (i = 0; i < size; i++)
          {
               if (WR_GetPlotFlag(sSSFPlot, SSF_Flags[i]) != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(sSSFPlot, SSF_Flags[i], EngineConstants.TRUE);
                    break;
               }
          }
     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_UpdateStorySoFar
     //------------------------------------------------------------------------------
     /*
     * @brief Properly updates Paragon Story-So-Far
     *
     * Properly updates Paragon Story-So-Far
     * There are a few paths to take, this ensures we cannot possibly back-track
     * @param nSSFPlotFlag the Story-So-Far
     *
     * @author   Joshua Stiksma
     *
     **/
     public void ORZ_UpdateStorySoFar(int bSSFPlotFlag)
     {
          string sSSFPlot = EngineConstants.PLT_MNP00PT_SSF_PARAGON;
          List<int> SSF_Flags = ORZ_GetSSFPlotFlags();
          int i, size = GetArraySize(SSF_Flags);

          // Exceptions
          if (bSSFPlotFlag == EngineConstants.SSF_ORZ_03A_BHELEN_TASK_2 ||
               bSSFPlotFlag == EngineConstants.SSF_ORZ_03B_HARROWMONT_TASK_2)
          {
               if (WR_GetPlotFlag(sSSFPlot, EngineConstants.SSF_ORZ_03C_BHELEN_TASK_DA) != EngineConstants.FALSE ||
                    WR_GetPlotFlag(sSSFPlot, EngineConstants.SSF_ORZ_03D_HARROWMONT_TASK_DA) != EngineConstants.FALSE)
               {
                    return;
               }
          }
          // Set flag
          WR_SetPlotFlag(sSSFPlot, bSSFPlotFlag, EngineConstants.TRUE);
     }

     //------------------------------------------------------------------------------
     // EngineConstants.ORZ_CleanUpLiteProving
     //------------------------------------------------------------------------------
     /*
      * @brief Cleans up the lite proving fighters
      *  
      * Ensures that all the creature involved in the lite proving fights are set
      * to inactive. To be used in between fights to clean up corpse piles.
      *
      * @author Grant Mackay
      *
      **/
     public void ORZ_CleanUpLiteProving()
     {

          List<GameObject> arFighters = GetNearestObjectByTag(GetHero(), "orz260cr_prov_lite", EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.MAX_CREATURES_IN_AREA);
          int nSize = GetArraySize(arFighters);

          GameObject oFighter;
          int nIndex;

          for (nIndex = 0; nIndex < nSize; ++nIndex)
          {

               oFighter = arFighters[nIndex];
               WR_SetObjectActive(oFighter, EngineConstants.FALSE);

          }

     }

     public void UT_PartyJump(string sWaypointTag)
     {
          int nIndex;
          int nArraySize;
          GameObject oCurrent;
          List<GameObject> arPartyList;
          //--------------------------------------------------------------------------
          arPartyList = GetPartyList(GetHero());
          nArraySize = GetArraySize(arPartyList);
          //--------------------------------------------------------------------------
          UT_LocalJump(GetMainControlled(), sWaypointTag, EngineConstants.TRUE, EngineConstants.TRUE, EngineConstants.TRUE);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCurrent = arPartyList[nIndex];
               UT_LocalJump(oCurrent, sWaypointTag, EngineConstants.TRUE, EngineConstants.TRUE, EngineConstants.TRUE);
          }
     }
}