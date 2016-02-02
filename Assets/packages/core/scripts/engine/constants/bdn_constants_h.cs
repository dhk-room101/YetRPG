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

         Dwarf Noble
          -> Constants

     */
     //------------------------------------------------------------------------------
     // Created By: Jon Epps
     // Created On: July 24, 2007
     //==============================================================================

     //------------------------------------------------------------------------------
     // CREATURES
     //------------------------------------------------------------------------------

     public const string BDN_CR_WEAPON_MERCHANT = "bdn100cr_weapon_merchant";
     public const string BDN_CR_VOLLNEY = "bdn100cr_vollney";
     public const string BDN_CR_SCHOLAR = "bdn100cr_scholar";
     public const string BDN_CR_MARDY = "bdn100cr_mardy";
     public const string BDN_CR_TELI = "bdn100cr_teli";
     public const string BDN_CR_CHATTING_MALE = "bdn100cr_chatting_male";
     public const string BDN_CR_CHATTING_FEMALE = "bdn100cr_chatting_female";
     public const string BDN_CR_BHELEN = "bdn100cr_bhelen";
     public const string BDN_CR_TRIAN = "bdn100cr_trian";
     public const string BDN_CR_PROVING_GUARD = "bdn100cr_provingguard";

     public const string BDN_CR_MANDAR_DACE = "bdn110cr_mandar_dace";
     public const string BDN_CR_ADAL_HELMI = "bdn110cr_adal_helmi";
     public const string BDN_CR_ALLER_BEMOT = "bdn110cr_aller_bemot";
     public const string BDN_CR_BLACKSTONE = "bdn110cr_blackstone";
     public const string BDN_CR_FRANDLIN_IVO = "bdn110cr_frandlin_ivo";
     public const string BDN_CR_PROVINGS_MASTER = "bdn110cr_proving_master";
     public const string BDN_CR_PROVINGS_TRAINER = "bdn110cr_proving_trainer";
     public const string BDN_CR_FIGHT_GUY = "temp_bdn110cr_fightguy";
     public const string BDN_CR_PROVING_ESCORT = "bdn100cr_provingescort";
     public const string BDN_CR_LESSER_NOBLE = "bdn100cr_lesser_noble";

     public const string BDN_CR_HELMI = "bdn120cr_helmi";
     public const string BDN_CR_RONUS_DACE = "bdn120cr_ronus_dace";
     public const string BDN_CR_KING = "bdn120cr_kingendrin";
     public const string BDN_CR_HARROWMONT = "bdn120cr_harrowmont";
     public const string BDN_CR_BEMOT = "bdn120cr_bemot";
     public const string BDN_CR_MEINO = "bdn120cr_meino";
     public const string BDN_CR_DUNCAN = "bdn120cr_duncan";
     public const string BDN_CR_GORIM = "bdn120cr_gorim";
     public const string BDN_CR_RICA = "bdn120cr_rica";

     public const string BDN_CR_MERC_CAPTAIN = "bdn200cr_merc_captain";
     public const string BDN_CR_FRANDLIN_SCOUT = "bdn200cr_frandlin_scout";
     public const string BDN_CR_SCOUT = "bdn200cr_scout";
     public const string BDN_CR_FIRST_SPIDER = "bdn200ar_firstspider";

     public const string BDN_CR_PRISON_GUARD = "bdn300cr_prison_guard";

     public const string BDN_CR_GUARD_DEEP_ROADS = "bdn400cr_prison_guard";
     public const string BDN_CR_GENLOCK_PATROLLER = "genlock_dagger_patrol";
     public const string BDN_CR_GIANT_SPIDER1 = "bdn400cr_spider_giant";
     public const string BDN_CR_GIANT_SPIDER2 = "bdn400cr_spider_giant2";

     //------------------------------------------------------------------------------
     // STORES
     //------------------------------------------------------------------------------

     public const string BDN_SR_ARMOR_MERCHANT = "store_bdn100cr_armor_merchant";
     public const string BDN_SR_SILK_MERCHANT = "store_bdn100cr_silk_merchant";

     //------------------------------------------------------------------------------
     // PLACEABLES
     //------------------------------------------------------------------------------

     public const string BDN_IP_PC_CHEST = "bdn120ip_player_chest";
     public const string BDN_IP_DOOR_BHELEN = "bdn120ip_door_bhelen";
     public const string BDN_IP_SIDE_DOOR = "bdn120ip_side_door";

     public const string BDN_IP_SHIELD_TRAP_DOOR = "bdn210ip_floor_trap_door";

     public const string BDN_IP_BALLISTA = "bdn200ip_ballista";

     public const string BDN_IP_EXILE_DOORS = "bdn400ip_outskirts_door";

     public const string BDN_IP_PUZZLE_TILE_1 = "bdn210ip_floor_pad_1";
     public const string BDN_IP_PUZZLE_TILE_2 = "bdn210ip_floor_pad_2";
     public const string BDN_IP_PUZZLE_TILE_3 = "bdn210ip_floor_pad_3";

     //------------------------------------------------------------------------------
     // WAYPOINTS
     //------------------------------------------------------------------------------

     public const string BDN_WP_START = "start";

     public const string BDN_WP_NOBLES_QUARTER_ENTER = "bdn100wp_nobles_quarter_enter";

     public const string BDN_WP_PROVING_FRANDLIN = "bdn110wp_proving_frandlin";
     public const string BDN_WP_PROVING_ENTER = "bdn110wp_proving_enter";
     public const string BDN_WP_PROVMASTER = "bdn110wp_provmaster";
     public const string BDN_WP_PROVING_WATCH_PC = "bdn110wp_watch_pc";
     public const string BDN_WP_PROVING_WATCH_PROVMASTER = "bdn110wp_watch_provmaster";
     public const string BDN_WP_PROVING_ESCORT_POST = "bdn110wp_escort_exit";
     public const string BDN_WP_PROVING_ESCORT_WATCHING = "bdn110wp_escort_watching";

     public const string BDN_WP_FEAST_PC_AFTER_HONOR_PROVING = "bdn120wp_pc_after_honor_proving";
     public const string BDN_WP_FEAST_DACE_AFTER_HONOR_POST = "bdn120wp_ronus_dace_post";

     public const string BDN_WP_RUINED_TAIG_ENTER = "bdn200wp_taig_start";
     public const string BDN_WP_RUINED_TAIG_TRIAN = "bdn200wp_taig_trian";
     public const string BDN_WP_RUINED_TAIG_RENDEVOUS_BHELEN = "bdn200wp_rendevous_bhelen";
     public const string BDN_WP_RUINED_TAIG_RENDEVOUS_KING = "bdn200wp_rendevous_king";
     public const string BDN_WP_RUINED_TAIG_RENDEVOUS_HARROWMONT = "bdn200wp_rendevous_harrowmont";
     public const string BDN_WP_RUINED_TAIG_RENDEVOUS_BEMOT = "bdn200wp_rendevous_bemot";
     public const string BDN_WP_RUINED_TAIG_RENDEVOUS_MEINO = "bdn200wp_rendevous_meino";
     public const string BDN_WP_RUINED_TAID_SCOUT1 = "bdn200wp_scout1";
     public const string BDN_WP_RUINED_TAID_SCOUT2 = "bdn200wp_scout2";

     public const string BDN_WP_PRISON_PC = "bdn300wp_prison_pc";
     public const string BDN_WP_PRISON_GUARD_01 = "bdn300wp_prison_guard_01";
     public const string BDN_WP_PRISON_GORIM_01 = "bdn300wp_prison_gorim_01";
     public const string BDN_WP_PRISON_GORIM_02 = "bdn300wp_prison_gorim_02";

     public const string BDN_WP_DEEP_ROADS_START = "bdn400wp_deep_roads_start";
     public const string BDN_WP_AFTER_EXILE = "bdn400wp_after_exile";
     public const string BDN_WP_GENLOCK_PATROL = "bdn400wp_genlock_patrol";

     //------------------------------------------------------------------------------
     // AREAS
     //------------------------------------------------------------------------------

     public const string BDN_AR_NOBLES_QUARTER = "bdn100ar_nobles_quarter";
     public const string BDN_AR_PROVINGS = "bdn110ar_proving";
     public const string BDN_AR_ROYAL_PALACE = "bdn120ar_royal_palace";
     public const string BDN_AR_RUINED_TAIG = "bdn200ar_ruined_taig";
     public const string BDN_AR_THAIG_CHAMBER = "bdn210ar_thaig_chamber";
     public const string BDN_AR_PRISON = "bdn300ar_orzammar_prison";
     public const string BDN_AR_DEEP_ROADS = "bdn400ar_deep_road_outskirt";

     //------------------------------------------------------------------------------
     // ITEMS
     //------------------------------------------------------------------------------

     public const string BDN_IM_FANCY_DAGGER_R = "bdn100im_fancy_dagger.uti";
     public const string BDN_IM_SCHOLAR_BOOK_R = "bdn100im_scholar_book.uti";
     public const string BDN_IM_STARTING_SHIELD_R = "gen_im_arm_shd_sml_wdn.uti";

     public const string BDN_IM_KINGS_ROBES_R = "gen_im_cth_mag_lrd.uti";

     public const string BDN_IM_AEDUCAN_SHIELD_R = "bdn200im_shield.uti";
     public const string BDN_IM_TRIANS_RING_R = "bdn200im_trian_ring.uti";
     public const string BDN_IM_BALLISTA_BOLT = "bdn200im_ballista_bolt.uti";

     public const string BDN_IM_PRISON_CLOTHES_R = "bdn300im_prison_clothes.uti";

     public const string BDN_IM_EXILE_SWORD_NORM_R = "bdn400im_sword_norm.uti";
     public const string BDN_IM_EXILE_SWORD_GOOD_R = "bdn400im_sword_good.uti";
     public const string BDN_IM_EXILE_SHIELD_NORM_R = "bdn400im_shield_norm.uti";
     public const string BDN_IM_EXILE_SHIELD_GOOD_R = "bdn400im_shield_good.uti";

     public const string BDN_IM_PRISON_GUARD_SWORD_R = "gen_im_wep_mel_lsw_lsw.uti";
     public const string BDN_IM_PRISON_GUARD_SHIELD_R = "gen_im_arm_shd_sml_met.uti";

     public const string BDN_CUT_PROVING_INTRO_CUTSCENE = "bdn110_proving_intro.cut";

     //------------------------------------------------------------------------------
     // TEAM IDS
     //------------------------------------------------------------------------------

     public const int BDN_TEAM_MERCS = 1;
     public const int BDN_TEAM_TRIAN = 2;
     public const int BDN_TEAM_DARKSPAWN_AMBUSH = 3;
     public const int BDN_TEAM_PROVING_BLACKSTONE = 4;
     public const int BDN_TEAM_PROVING_FRANDLIN = 5;
     public const int BDN_TEAM_RUINEDTHAIG_DARKSPAWN = 6;
     public const int BDN_TEAM_RUINEDTHAIG_DSPAWN_LAST = 7;
     public const int BDN_TEAM_RUINEDTHAIG_DSPAWN_SECOND = 8;
     public const int BDN_TEAM_DEEPROAD_DSPAWN_ONE = 9;
     public const int BDN_TEAM_RUINEDTHAIG_DSPAWN_FIRST = 10;
     public const int BDN_TEAM_RUINEDTHAIG_DEEPSTALKERS = 11;

     //------------------------------------------------------------------------------
     // GROUPS
     //------------------------------------------------------------------------------

     public const int BDN_GROUP_PROVINGS_1 = 47;
     public const int BDN_GROUP_PROVINGS_2 = 48;

     //------------------------------------------------------------------------------
     // SCRIPT RESOURCES
     //------------------------------------------------------------------------------

     public const string BDN_RS_TEAM_CORE = "bdncr_team_core.nss";
     public const string BDN_RS_AREA_CORE = "bdnar_core.nss";

     //------------------------------------------------------------------------------
     // ANIMATIONS
     //------------------------------------------------------------------------------
     public const int BDN_ANIMATION_POST_FAINTED = 46;

     //------------------------------------------------------------------------------
     // CONSTANTS TO BE MOVED AT A LATER TIME
     //------------------------------------------------------------------------------

     // The Scholar gives the player money for saving him.
     public const int BDN_SCHOLAR_ROBBED_COPPER = 0;
     public const int BDN_SCHOLAR_ROBBED_SILVER = 0;
     public const int BDN_SCHOLAR_ROBBED_GOLD = 1;
}