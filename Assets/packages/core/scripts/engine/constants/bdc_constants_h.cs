//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class EngineConstants
{
     //==============================================================================
     /*

         Dwarf Commoner
          -> Constants

     */
     //------------------------------------------------------------------------------
     // Created By: Jon Epps
     // Created On: July 24, 2007
     //==============================================================================

     //------------------------------------------------------------------------------
     // CREATURES
     //------------------------------------------------------------------------------

     public const string BDC_DESTROYED_CREATURE = "bdc140cr_destroyed_creature";

     public const string BDC_CR_CARAVAN_GUARD = "bdc100cr_guard_caravan";
     public const string BDC_CR_CARAVAN_MERCHANT = "bdc100cr_caravan_merch";

     public const string BDC_CR_JARVIA = "bdc110cr_jarvia";

     public const string BDC_CR_CELL_GUARD = "bdc120cr_guard";
     public const string BDC_CR_THUG1 = "bdc120cr_thug1";
     public const string BDC_CR_THUG2 = "bdc120cr_thug2";
     public const string BDC_CR_THUG3 = "bdc120cr_thug3";

     public const string BDC_CR_OSKIAS = "bdc130cr_oskias";
     public const string BDC_CR_BAR_PATRON = "bdc130cr_patron";
     public const string BDC_CR_BARTENDER = "bdc130cr_bartender";

     public const string BDC_CR_DUNCAN = "bdc140cr_duncan";
     public const string BDC_CR_LENKA = "bdc140cr_lenka";
     public const string BDC_CR_ADALBO = "bdc140cr_adalbo";
     public const string BDC_CR_MAINAR = "bdc140cr_mainar";
     public const string BDC_CR_PROVING_MASTER = "bdc140cr_proving_master";
     public const string BDC_CR_EVERD = "bdc140cr_everd";

     public const string BDC_CR_LESKE = "bdc200cr_leske";

     public const string BDC_CR_BERAHT = "bdc210cr_beraht";
     public const string BDC_CR_RICA = "bdc210cr_rica";

     public const string BDC_CR_COMMONS_AMB_M_1 = "bdc100cr_amb_m_1";
     public const string BDC_CR_COMMONS_AMB_M_3 = "bdc100cr_amb_m_3";
     public const string BDC_CR_COMMONS_AMB_F_1 = "bdc100cr_amb_f_1";
     public const string BDC_CR_COMMONS_AMB_F_4 = "bdc100cr_amb_f_4";

     //------------------------------------------------------------------------------
     // WAYPOINTS
     //------------------------------------------------------------------------------

     public const string BDC_WP_START = "bdc210wp_start";

     public const string BDC_WP_BERAHTS_HIDEOUT_PC_CELL = "bdc120wp_pc_cell";
     public const string BDC_WP_BERAHTS_HIDEOUT_PC_LESKE_LEFT_BEHIND = "bdc120wp_pc_leske_left_behind";
     public const string BDC_WP_LESKE_ESCAPE = "bdc200cr_leske_escape";

     public const string BDC_WP_LESKE_DRUG = "bdc140wp_leske_drug";
     public const string BDC_WP_LESKE_PC_DRUG = "bdc140wp_leske_pc_drug";
     public const string BDC_WP_PROVMASTER_FIGHT = "bdc140wp_provmaster_fight";
     public const string BDC_WP_PC_AFTER_DRUG = "bdc140wp_pc_after_drug";
     public const string BDC_WP_LESKE_AFTER_DRUG = "bdc140wp_leske_after_drug";

     public const string BDC_WP_COMMONS_AMB_M_1 = "bdc100wp_amb_m_1";
     public const string BDC_WP_COMMONS_AMB_M_3 = "bdc100wp_amb_m_3";
     public const string BDC_WP_COMMONS_AMB_F_1 = "bdc100wp_amb_f_1";
     public const string BDC_WP_COMMONS_AMB_F_4 = "bdc100wp_amb_f_4";

     //------------------------------------------------------------------------------
     // AREAS
     //------------------------------------------------------------------------------

     public const string BDC_AR_COMMONS = "bdc100ar_commons";
     public const string BDC_AR_BERAHTS_SHOP = "bdc110ar_berahts_shop";
     public const string BDC_AR_BERAHTS_HIDEOUT = "bdc120ar_berahts_hideout";
     public const string BDC_AR_TAPSTERS_TAVERN = "bdc130ar_tapsters_tavern";
     public const string BDC_AR_PROVING = "bdc140ar_proving";
     public const string BDC_AR_SLUMS = "bdc200ar_slums";
     public const string BDC_AR_PLAYERS_HOME = "bdc210ar_players_home";

     //------------------------------------------------------------------------------
     // INTERACTIVE PLACEABLES
     //------------------------------------------------------------------------------

     public const string BDC_IP_PROVING_DOOR = "bdc100ip_to_proving";

     public const string BDC_IP_SHELF_1 = "bdc110ip_shelf_1";
     public const string BDC_IP_SHELF_2 = "bdc110ip_shelf_2";
     public const string BDC_IP_HIDEOUT_TRANSITION = "bdc110ip_to_berahts_hideout";

     public const string BDC_IP_CELL_DOOR = "bdc120ip_cell_door";
     public const string BDC_IP_LESKE_CELL_DOOR = "bdc120ip_cell_door_leske";
     public const string BDC_IP_PLAYER_EQUIP_CHEST = "bdc120ip_player_equip_chest";
     public const string BDC_IP_RUBBLE = "bdc120ip_rubble";
     public const string BDC_IP_HIDEOUT_EXIT = "bdc120ip_to_berahts_shop";

     public const string BDC_IP_MAINAR_DOOR = "bdc140ip_mainar_door";
     public const string BDC_IP_MAINAR_BASIN = "bdc140ip_basin";

     //------------------------------------------------------------------------------
     // ITEMS
     //------------------------------------------------------------------------------

     public const string BDC_IM_DRUG_R = "bdc110im_drug.uti";
     public const string BDC_IM_PROVING_PASS_R = "bdc110im_proving_pass.uti";

     public const string BDC_IM_LOCK_PICK_R = "bdc120im_lock_pick.uti";
     public const string BDC_IM_CELL_KEY_R = "bdc120im_cell_key.uti";
     public const string BDC_IM_PRISON_CLOTH_LESKE = "gen_im_cth_com_i00.uti";
     public const string BDC_IM_PRISON_CLOTH_PC = "gen_im_cth_com_i00.uti";

     public const string BDC_IM_LYRIUM_ORE_R = "bdc130im_lyrium_ore.uti";

     public const string BDC_IM_EVERD_ARMOR_R = "bdc140im_everd_armor.uti";
     public const string BDC_IM_EVERD_HELM_R = "bdc140im_everd_helm.uti";
     public const string BDC_IM_EVERD_SWORD_R = "bdc140im_everd_sword.uti";
     public const string BDC_IM_EVERD_MACE_R = "bdc140im_everd_mace.uti";
     public const string BDC_IM_EVERD_AXE_R = "bdc140im_everd_axe.uti";
     public const string BDC_IM_EVERD_SHIELD_R = "bdc140im_everd_shield.uti";
     public const string BDC_IM_EVERD_BOOTS_R = "bdc140im_everd_boots.uti";
     public const string BDC_IM_EVERD_GLOVES_R = "bdc140im_everd_gloves.uti";

     public const string BDC_IM_FIGHT_SCHEDULE_R = "bdc140im_fight_schedule.uti";

     public const string BDC_IM_WINE_MOTHER_R = "bdc210im_wine_mother.uti";

     //------------------------------------------------------------------------------
     // TEAM IDS
     //------------------------------------------------------------------------------

     public const int BDC_TEAM_BERAHT = 1;
     public const int BDC_TEAM_THUG_ROOM_1 = 2;
     public const int BDC_TEAM_THUG_ROOM_2 = 3;
     public const int BDC_TEAM_THUG_ROOM_3 = 4;
     public const int BDC_TEAM_END_CONVERSATION = 5;
     public const int BDC_TEAM_OSKIAS = 6;
     public const int BDC_TEAM_MERCHANT_BLOCKAGE = 7;
     public const int BDC_TEAM_PRISON_GUARD = 8;

     //------------------------------------------------------------------------------
     // CUTSCENES
     //------------------------------------------------------------------------------

     public const string BDC_CS_INTRO = "bdccs_intro.cut";

     //------------------------------------------------------------------------------
     // SCRIPT RESOURCES
     //------------------------------------------------------------------------------

     public const string BDC_RS_AREA_CORE = "bdcar_core.nss";
}