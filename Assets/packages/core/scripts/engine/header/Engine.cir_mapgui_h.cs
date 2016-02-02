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

         Broken Circle
          -> Fade Map GUI Include

         These are the generic functions for the Fade Map GUI

         *** THIS IS TEMPORARY, TO BE REPLACE BY REAL GUI ***
          - Entries into worldmaps.xls were made that will
            need to be removed when this include is removed.

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: March 03, 2007
     //==============================================================================

     //#include"plt_cir300pt_fade"
     //#include"plt_cir300pt_fade_portal"
     //#include"plt_cir000pt_talked_to"

     //#include"campaign_h"
     //#include"utility_h"

     //==============================================================================
     // CONSTANTS
     //==============================================================================

     // Fade Map Object
     //moved public const string  EngineConstants.WM_FAD_TAG                     = "fade_map";

     // Fade Map Locations
     //moved public const string WML_FAD_WEISSHAUPT             = "wml_fad_weisshaupt";
     //moved public const string WML_FAD_RAW_FADE               = "wml_fad_raw_fade";
     //moved public const string WML_FAD_COMPANION_A            = "wml_fad_comp_a";
     //moved public const string WML_FAD_COMPANION_B            = "wml_fad_comp_b";
     //moved public const string WML_FAD_COMPANION_C            = "wml_fad_comp_c";
     //moved public const string WML_FAD_BURNING_MANOR          = "wml_fad_burning";
     //moved public const string WML_FAD_DARKSPAWN_INVASION     = "wml_fad_invasion";
     //moved public const string WML_FAD_MAGE_HOLOCAUST         = "wml_fad_holocaust";
     //moved public const string WML_FAD_TEMPLARS_NIGHTMARE     = "wml_fad_nightmare";
     //moved public const string WML_FAD_SLOTH_DEMON_SANTCUM    = "wml_fad_sloth";


     //==============================================================================
     // FUNCTION IMPLEMENTATION
     //------------------------------------------------------------------------------
     // CIR_MapGUI_ActivateWideOpenWorldMap
     //------------------------------------------------------------------------------
     /*
     * @brief Activates WOW Map
     *
     * Makes the Wide Open World Map the Primary Map
     *
     * @author   Joshua Stiksma
     *
     **/
     public void CIR_MapGUI_ActivateWideOpenWorldMap()
     {

          GameObject oWideOpenWorldMap;

          //--------------------------------------------------------------------------

          oWideOpenWorldMap = GetObjectByTag( EngineConstants.WM_WOW_TAG);

          //--------------------------------------------------------------------------

          WR_SetWorldMapPrimary(oWideOpenWorldMap);
          WR_SetWorldMapSecondary(oWideOpenWorldMap);

     }

     //------------------------------------------------------------------------------
     // CIR_MapGUI_ActivateFadeMap
     //------------------------------------------------------------------------------
     /*
     * @brief Activates Fade Map
     *
     * Makes the Fade Map the Primary Map
     *
     * @author   Joshua Stiksma
     *
     **/
     public void CIR_MapGUI_ActivateFadeMap()
     {

          GameObject oFadeMap;
          GameObject oWideOpenWorldMap;

          //--------------------------------------------------------------------------

          oFadeMap = GetObjectByTag( EngineConstants.WM_FAD_TAG);
          oWideOpenWorldMap = GetObjectByTag( EngineConstants.WM_WOW_TAG);

          //--------------------------------------------------------------------------

          WR_SetWorldMapPrimary(oFadeMap);
          WR_SetWorldMapSecondary(oWideOpenWorldMap);
          CIR_MapGUI_UpdateActivePins();

     }
     //------------------------------------------------------------------------------
     // CIR_MapGUI_UpdateActivePins
     //------------------------------------------------------------------------------
     /*
     * @brief Updates the Map Pins for the Fade Map
     *
     * Updates the Map Pins for the Fade Map.
     *
     **/
     public void CIR_MapGUI_UpdateActivePins(int nUpdatedPlotFlag = -1)
     {

          if (nUpdatedPlotFlag > -1)
               WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, nUpdatedPlotFlag, EngineConstants.TRUE);

          //--------------------------------------------------------------------------

          int bMageAsunder;
          int bBurningTower;
          int bDarkspawnInvasion;
          int bTemplarsNightmare;
          int bRawFade;
          int bTalkedToNiall;

          //--------------------------------------------------------------------------

          bMageAsunder = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_MAGE_ASUNDER_COMPLETE);
          bBurningTower = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_BURNING_TOWER_COMPLETE);
          bDarkspawnInvasion = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_DARKSPAWN_INVASION_COMPLETE);
          bTemplarsNightmare = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_TEMPLARS_NIGHTMARE_COMPLETE);
          bRawFade = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_MAIN_FADE_COMPLETE);
          int bWeisshaupt = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_WEISSHAUPT_COMPLETE);
          int bComp1 = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_GREEN_1_COMPLETE);
          int bComp2 = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_GREEN_2_COMPLETE);
          int bComp3 = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_GREEN_3_COMPLETE);
          bTalkedToNiall = WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_TALKED_TO, EngineConstants.NIALL_TALKED_TO);
          
          //--------------------------------------------------------------------------

          // If PC has talked to Niall: Open All Fade Areas
          if (bTalkedToNiall != EngineConstants.FALSE)
          {
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_BURNING_MANOR), 2);
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_DARKSPAWN_INVASION), 2);
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_MAGE_HOLOCAUST), 2);
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_TEMPLARS_NIGHTMARE), 2);
          }

          //--------------------------------------------------------------------------
          // Check to see if we can open up extra areas
          //--------------------------------------------------------------------------

          // If PC has Completed Both Darkspawn Invasion AND Templar's Nightmare: Open Companion A
          if (bDarkspawnInvasion != EngineConstants.FALSE && bTemplarsNightmare != EngineConstants.FALSE && bComp1 == EngineConstants.FALSE)
          {
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_A), 2);
               WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_FADE, EngineConstants.COMPANION_AREA_OPEN, EngineConstants.TRUE);
          }

          // If PC has Completed Both Templar's Nightmare && Mage Asunder: Open Companion B
          if (bTemplarsNightmare != EngineConstants.FALSE && bMageAsunder != EngineConstants.FALSE)
          {
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_B), 2);
               WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_FADE, EngineConstants.COMPANION_AREA_OPEN, EngineConstants.TRUE);
          }

          // If PC has Completed Both Mage Asunder AND Burning Tower: Open Companion C
          if (bMageAsunder != EngineConstants.FALSE && bBurningTower != EngineConstants.FALSE)
          {
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_C), 2);
               WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_FADE, EngineConstants.COMPANION_AREA_OPEN, EngineConstants.TRUE);
          }

          // If PC has Completed All 5 Fade Areas: Open Sloth Demon's Sanctum
          if (bMageAsunder != EngineConstants.FALSE && bBurningTower != EngineConstants.FALSE && 
               bDarkspawnInvasion != EngineConstants.FALSE && bTemplarsNightmare != EngineConstants.FALSE && 
               bRawFade != EngineConstants.FALSE)
          {
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_SLOTH_DEMON_SANTCUM), 2);
          }

          // If PC has completed the areas, the area map should reflect that
          if (bMageAsunder != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_MAGE_HOLOCAUST), 5);
          if (bBurningTower != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_BURNING_MANOR), 5);
          if (bDarkspawnInvasion != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_DARKSPAWN_INVASION), 5);
          if (bTemplarsNightmare != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_TEMPLARS_NIGHTMARE), 5);
          if (bRawFade != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_RAW_FADE), 5);
          if (bWeisshaupt != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_WEISSHAUPT), 5);
          if (bComp1 != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_A), 5);
          if (bComp2 != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_B), 5);
          if (bComp3 != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_C), 5);

     }
}