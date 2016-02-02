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
     ///////////////////////////////////////////////////////////////////////////////
     //  campaign_h
     ///////////////////////////////////////////////////////////////////////////////
     /*
         These are global constants that can be called anywhere in the Dragon Age
         campaign.
     */
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Ensemble
     //  Created On: Nov. 15, 2006
     ///////////////////////////////////////////////////////////////////////////////

     // origin story plots constants
     //#include"bhn_constants_h"
     //#include"bhm_constants_h"
     //#include"bed_constants_h"
     //#include"bec_constants_h"
     //#include"bdn_constants_h"
     //#include"bdc_constants_h"
     //#include"plt_gen00pt_backgrounds"
     //#include"wrappers_h"
     //#include"utility_h"
     //#include"log_h"
     //#include"sys_chargen_h"

     //#include"plt_mnp00pt_ssf_critpath"
     //#include"plt_mnp000pt_main_lothering"
     //#include"plt_mnp000pt_main_events"
     //#include"plt_arl000pt_contact_eamon"
     //#include"plt_urnpt_main"
     //#include"plt_clipt_main"
     //#include"plt_denpt_main"

     public void Campaign_SetBlight(int nStage)
     {
          GameObject oBlight1 = GetObjectByTag("wml_wow_blight1");
          GameObject oBlight2 = GetObjectByTag("wml_wow_blight2");
          GameObject oBlight3 = GetObjectByTag("wml_wow_blight3");
          GameObject oBlight4 = GetObjectByTag("wml_wow_blight4");
          GameObject oBlight5 = GetObjectByTag("wml_wow_blight5");
          GameObject oBlight6 = GetObjectByTag("wml_wow_blight6");
          GameObject oBlight7 = GetObjectByTag("wml_wow_blight7");

          // First deactivate all
          WR_SetWorldMapLocationStatus(oBlight1, EngineConstants.WM_LOCATION_INACTIVE);
          WR_SetWorldMapLocationStatus(oBlight2, EngineConstants.WM_LOCATION_INACTIVE);
          WR_SetWorldMapLocationStatus(oBlight3, EngineConstants.WM_LOCATION_INACTIVE);
          WR_SetWorldMapLocationStatus(oBlight4, EngineConstants.WM_LOCATION_INACTIVE);
          WR_SetWorldMapLocationStatus(oBlight5, EngineConstants.WM_LOCATION_INACTIVE);
          WR_SetWorldMapLocationStatus(oBlight6, EngineConstants.WM_LOCATION_INACTIVE);
          WR_SetWorldMapLocationStatus(oBlight7, EngineConstants.WM_LOCATION_INACTIVE);

          // activate requested stage
          GameObject oRequestStage = GetObjectByTag("wml_wow_blight" + IntToString(nStage));
          if (IsObjectValid(oRequestStage) != EngineConstants.FALSE)
               WR_SetWorldMapLocationStatus(oRequestStage, EngineConstants.WM_LOCATION_GRAYED_OUT, EngineConstants.TRUE);
     }

     public void Campaign_SetStorySoFar()
     {
          if (WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.PLAYER_FINISHED_ALL_MAJOR_PLOTS) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CRITPATH, EngineConstants.CRITPATH_LANDSMEET_OVER_ARMY_COMPLETE, EngineConstants.TRUE);

          else if (WR_GetPlotFlag(EngineConstants.PLT_ARL000PT_CONTACT_EAMON, EngineConstants.ARL_CONTACT_EAMON_EAMON_READY_FOR_LANDSMEET) != EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_QUEST_DONE) == EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.PLAYER_FINISHED_FIFTH_MAJOR_PLOT) != EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_OPENING_DONE) == EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CRITPATH, EngineConstants.CRITPATH_LANDSMEET_READY_ARMY_COMPLETE, EngineConstants.TRUE);

          else if (WR_GetPlotFlag(EngineConstants.PLT_ARL000PT_CONTACT_EAMON, EngineConstants.ARL_CONTACT_EAMON_EAMON_READY_FOR_LANDSMEET) != EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_QUEST_DONE) == EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.PLAYER_FINISHED_FIFTH_MAJOR_PLOT) == EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CRITPATH, EngineConstants.CRITPATH_LANDSMEET_READY_ARMY_NOT_COMPLETE, EngineConstants.TRUE);

          else if (WR_GetPlotFlag(EngineConstants.PLT_ARL000PT_CONTACT_EAMON, EngineConstants.ARL_CONTACT_EAMON_EAMON_REVIVED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CRITPATH, EngineConstants.CRITPATH_ARL_RESTORED, EngineConstants.TRUE);

          else if (WR_GetPlotFlag(EngineConstants.PLT_ARL000PT_CONTACT_EAMON, EngineConstants.ARL_CONTACT_EAMON_EAMON_REVIVED) == EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_URNPT_MAIN, EngineConstants.URN_PLOT_DONE) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CRITPATH, EngineConstants.CRITPATH_BRING_ASHES_TO_ARL_EAMON, EngineConstants.TRUE);

          else if (WR_GetPlotFlag(EngineConstants.PLT_ARL000PT_CONTACT_EAMON, EngineConstants.ARL_CONTACT_EAMON_PC_AGREES_TO_SEEK_OUT_ASHES) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CRITPATH, EngineConstants.CRITPATH_CURE_ARL_EAMON, EngineConstants.TRUE);

          else if (WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_LOTHERING, EngineConstants.MAIN_LOTHERING_PC_CROSSED_LOTHERING) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CRITPATH, EngineConstants.CRITPATH_GET_ARL_EAMON, EngineConstants.TRUE);

          else
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CRITPATH, EngineConstants.CRITPATH_GO_TO_LOTHERING, EngineConstants.TRUE);

     }

     public void Campaign_ChargenDone(string sParam1, string sParam2)
     {
          string sArea;
          string sWP;
          int nEquipIndex; // index into equipment 2da
          GameObject oPC = GetHero();

          // Disable party picker (re-enabled at end of Prelude)
          SetPartyPickerGUIStatus(EngineConstants.PP_GUI_STATUS_NO_USE);
          SetLocalInt(GetModule(), EngineConstants.PARTY_PICKER_GUI_ALLOWED_TO_POP_UP, EngineConstants.FALSE);
          SetLocalInt(GetModule(), EngineConstants.TUTORIAL_ENABLED, 1);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Campaign_ChargenDone", "START, param1: " + sParam1 + ", param2: " + sParam2);

          if (sParam1 != "" && sParam2 != "")
          {
               sArea = sParam1;
               sWP = sParam2;
          }
          else
          {
               int nRace = GetCreatureRacialType(GetHero()); /* Yes, GetHero is appropriate here */
               int nClass = GetCreatureCoreClass(GetHero());
               int nBackground = FloatToInt(GetCreatureProperty(GetHero(), EngineConstants.PROPERTY_SIMPLE_BACKGROUND));

               nEquipIndex = Chargen_GetEquipIndex(nRace, nClass, nBackground);
               sArea = GetM2DAString(EngineConstants.TABLE_STARTING_EQUIPMENT, "StartArea", nEquipIndex);
               sWP = GetM2DAString(EngineConstants.TABLE_STARTING_EQUIPMENT, "StartWP", nEquipIndex);
          }

          UT_DoAreaTransition(sArea, sWP);
     }

     // *** CURRENCY ***

     //moved public const string EngineConstants.GEN_IM_COPPER = "gen_im_copper.uti";

     // Denerim Gold Checks
     //moved public const int DLC_MONEY_BEGGAR_SMALL_DONATION = 10;
     //moved public const int DLC_MONEY_BEGGAR_LARGE_DONATION = 100;

     //moved public const int DEN_MONEY_HALF_VAUGHAN_TREASURE_FOR_SORIS_GOLD    = 20;  // should be half of BEC_VAUGHAN_BRIBE_GOLD  // this is mentioned in the dialog
     //moved public const int DEN_MONEY_FOR_SORIS_GOLD                          = 5;   // this is mentioned in the dialog
     //moved public const int DEN_MONEY_SIDEGUARD_BRIBE_LARGE_GOLD              = 12;  // this is mentioned in the dialog
     //moved public const int DEN_MONEY_SIDEGUARD_BRIBE_SMALL_GOLD              = 6;   // this is mentioned in the dialog
     //moved public const int DEN_MONEY_BROTHEL_FEE_FULL_SILVER                 = 40;  // this is mentioned in the dialog
     //moved public const int DEN_MONEY_BROTHEL_FEE_DISCOUNT_SILVER             = 30;  // this is mentioned in the dialog
     //moved public const int DEN_MONEY_VAUGHAN_BRIBE_GOLD                      = 40;  // this is mentioned in the dialog
     //moved public const int DEN_MONEY_EQUIPMENT_GUY_BRIBE_GOLD                = 15; // this is mentioned in the dialog
     //moved public const int DEN_MONEY_CALADRIUS_BRIBE_GOLD                    = 100; // this is mentioned in the dialog
     //moved public const int DEN_MONEY_SCARED_ELF_BRIBE_GOLD                   = 2;

     //moved public const int DEN_MONEY_RESCUE_TREASURY_GOLD                    = 20; // This is split up among several piles of gold in the treasury.

     // Prelude
     //moved public const int PRE_MONEY_BRIBE_FOR_GUARD_SILVER                  = 10;  // this is mentioned in the dialog

     //Nature of the Beast
     //moved public const int BEC_MONEY_HERMIT_CHECK = 1;

     //City Elf Background Checks
     //moved public const int BEC_MONEY_BEGGAR_SMALL_DONATION = 1;
     //moved public const int BEC_MONEY_BEGGAR_LARGE_DONATION = 10;

     //moved public const int BEC_MONEY_HOMELESS_LARGE_DONATION = 10;
     //moved public const int BEC_MONEY_HOMELESS_SMALL_DONATION = 3;

     //moved public const int BEC_VAUGHAN_BRIBE_GOLD = 40;    // this is mentioned in the dialog

     // Rogue Light Content (Box of Certain Interests
     //moved public const int LITE_ROGUE_NEGOTIATION_REQ = 15; // Creature Venom Required; mentioned in journal
     //moved public const int LITE_ROGUE_GEMS_REQ = 10;        // Greenstone Required; mentioned in journal
     //moved public const int LITE_ROGUE_LETTERS_REQ = 12;        // Greenstone Required; mentioned in journal

     // Background - Dwarven Commoner
     // These five money values are mentioned in dialogue. If the values change,
     // then the dialogues must be updated as well.
     // bdc100_ademaro.dlg, bdc100_olinda.dlg, bdc200_goilinar.dlg
     //moved public const int BDC_MONEY_NUGGET_PRICE = 1000;
     //moved public const int BDC_MONEY_ADEMARO_CHECK = 1000;
     //moved public const int BDC_MONEY_GOILINAR_BRIBE = 10;
     //moved public const int BDC_MONEY_GOILINAR_CHEAP = 1;
     //moved public const int BDC_MONEY_GOILINAR_NORMAL = 2;

     // Lothering
     //moved public const int LOT_BANDITS_BRIBE = 10;
     //moved public const int LOT_BANDITS_BRIBE_2 = 20;
     //moved public const int LOT_DWARF_MERCHANT_DISCOUNT_EXTRA = 100;
     //moved public const int LOT_ORPHAN_MONEY = 1;
     //moved public const int LOT_ROBBED_VILLAGER_HELP_MONEY = 10;
     //moved public const int LOT_GRAND_CLERIC_DONATION_SMALL = 10; // 10 silver
     //moved public const int LOT_GRAND_CLERIC_DONATION_MED = 30;   // 30 silver
     //moved public const int LOT_GRAND_CLERIC_DONATION_BIG = 5;    // 5 Gold
     //moved public const int LOT_ALE_MONEY_TAVERN_COPPERS = 3;

     // Denerim - Alistair's plot
     //moved public const int DEN_ALISTAIR_MONEY_FOR_GOLDANA_GOLD = 15;

     // Redcliffe - Arl Eamon plot
     //moved public const int ARL_MONEY_FOR_ANYON = 100;
     //moved public const int ARL_BRIBE_FOR_DWYN = 100;
     //moved public const int ARL_HALF_BRIBE_FOR_DWYN = 100;
     //moved public const int ARL_MONEY_FOR_BRIGID = 100;
     //moved public const int ARL_MONEY_FOR_KAITLYN_FAMILY_SWORD_HUGE = 1000;
     //moved public const int ARL_MONEY_FOR_KAITLYN_FAMILY_SWORD_LARGE = 500;
     //moved public const int ARL_MONEY_FOR_KAITLYN_FAMILY_SWORD_GOOD = 100;
     //moved public const int ARL_MONEY_FOR_KAITLYN_FAMILY_SWORD_SMALL = 10;
     //moved public const int ARL_MONEY_FOR_KAITLYN_FAMILY_SWORD_TINY = 1;
     //moved public const int ARL_MONEY_FOR_LLOYD_STONE_ROD_SMALL = 1;
     //moved public const int ARL_MONEY_FOR_LLOYD_STONE_ROD_MODERATE = 10;
     //moved public const int ARL_MONEY_FOR_MILITIA_DRINKS_LARGE = 100;
     //moved public const int ARL_MONEY_FOR_BELLA_PROMISE_LARGE = 100;
     //moved public const int ARL_MONEY_FOR_BELLA_PROMISE_HUGE = 500;

     // World maps
     //moved public const string WM_WOW_TAG = "wide_open_world";
     //moved public const string WM_UND_TAG = "underground_map";
     //moved public const string WM_DEN_TAG = "denerim";

     // Camp
     //moved public const string WML_AREA_TAG_CAMP = "cam100ar_camp_plains";
     //moved public const string WP_CAMP_START = "cam100wp_entrance";
     //moved public const string WOW_AR_CAMP = "cam100ar_camp_plains";

     // World Map Locations
     //moved public const string WML_WOW_LOTHERING = "wml_wow_lothering";
     //moved public const string WML_WOW_LOTHERING_LC = "wml_wow_lothering_lc";
     //moved public const string WML_WOW_WILDS = "wml_wow_wilds";
     //moved public const string WML_WOW_DALISH = "wml_wow_dalish";
     //moved public const string WML_WOW_DENERIM = "wml_wow_denerim";
     //moved public const string WML_WOW_DOCKS = "wml_wow_docks";
     //moved public const string WML_WOW_REDCLIFFE = "wml_wow_redcliffe";
     //moved public const string WML_WOW_FOREST = "wml_wow_forest";
     //moved public const string WML_WOW_RUINS = "wml_wow_ruins";
     //moved public const string WML_WOW_URN_VILLAGE = "wml_wow_urn_village";
     //moved public const string WML_WOW_URN_RUINS = "wml_wow_urn_ruins";
     //moved public const string WML_WOW_ORZAMMAR = "wml_wow_orzammar";
     //moved public const string WML_WOW_RED_CASTLE = "wml_wow_red_castle";
     //moved public const string WML_WOW_CAMP = "wml_wow_camp";
     //moved public const string WML_WOW_REDCLIFFE_VILLAGE_CLIMAX = "wml_wow_red_village_cli";
     //moved public const string WML_WOW_REDCLIFFE_CASTLE_CLIMAX = "wml_wow_red_castle_cli";
     //moved public const string WML_WOW_TOWER = "wml_wow_tower";

     // Underground Map Locations
     //moved public const string WML_UND_ORZAMMAR_COMMONS       = "wml_und_orzammar_commons";
     //moved public const string WML_UND_AEDUCAN_THAIG          = "wml_und_ruined_thaig";
     //moved public const string WML_UND_DEEP_ROAD_OUTSKIRTS    = "wml_und_deep_road_outskirts";
     //moved public const string WML_UND_CADASH_THAIG           = "wml_und_shale_thaig";
     //moved public const string WML_UND_CARIDINS_CROSS         = "wml_und_caridins_cross";
     //moved public const string WML_UND_ORTAN_THAIG            = "wml_und_ortan_thaig";
     //moved public const string WML_UND_DEAD_TRENCHES          = "wml_und_dead_trenches";
     //moved public const string WML_UND_ANVIL_OF_THE_VOID      = "wml_und_anvil_of_the_public void";
     //moved public const string WML_UND_ORZAMMAR_ASSEMBLY      = "wml_und_orzammar_assembly";

     // Denerim map locations
     //moved public const string WML_DEN_FORT_DRAKON            = "wml_den_fort_drakon";
     //moved public const string WML_DEN_ARL_ESTATE             = "wml_den_arl_estate";
     //moved public const string WML_DEN_ALIENAGE               = "wml_den_alienage";
     //moved public const string WML_DEN_MARKET                 = "wml_den_market";
     //moved public const string WML_DEN_EAMON                  = "wml_den_eamon";
     //moved public const string WML_DEN_PALACE                 = "wml_den_palace";
     //moved public const string WML_DEN_PEARL                  = "wml_den_brothel";
     //moved public const string WML_DEN_TEVINTER_WAREHOUSE     = "wml_den_warehouse";

     // Light content locations
     //moved public const string WML_LC_DEN_ALLEY_1             = "wml_lc_alley_justice_1";
     //moved public const string WML_LC_DEN_ALLEY_2             = "wml_lc_alley_justice_2";
     //moved public const string WML_LC_DEN_ALLEY_3             = "wml_lc_alley_justice_3";
     //moved public const string WML_LC_DEN_ASSASSIN_RANSOM     = "wml_lc_end_assassin";
     //moved public const string WML_LC_DEN_FRANDEREL_ESTATE    = "wml_lc_franderel_estate";
     //moved public const string WML_LC_DEN_KADAN_FE            = "wml_lc_qunari_assassin";
     //moved public const string WML_LC_BLOOD_MAGE_HQ           = "wml_lc_blood_mage";
     //moved public const string WML_LC_ROGUE_K                 = "wml_den_rogue_k";
     //moved public const string WML_LC_ROGUE_D                 = "wml_den_rogue_d";
}