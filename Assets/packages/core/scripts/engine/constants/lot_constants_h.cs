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
         List of constants for the Lothering
     */
     //==============================================================================
     // Created By: Rick Burton
     //==============================================================================
     // Modified By: Kaelin
     //==============================================================================

     //------------------------------------------------------------------------------
     // CREATURES
     //------------------------------------------------------------------------------
     public const string LOT_CR_BANDIT_LEADER = "lot100cr_bandit_leader";
     public const string LOT_CR_BANDIT = "lot100cr_bandit";
     public const string LOT_CR_BANDIT_2 = "lot100cr_bandit2";
     public const string LOT_CR_BANDIT_3 = "lot100cr_bandit3";
     public const string LOT_CR_BANDIT_4 = "lot100cr_bandit4";
     public const string LOT_CR_DOOMSAYER = "lot100cr_doomsayer";
     public const string LOT_CR_DWARVEN_MERCHANT = "lot100cr_dwarfmerch";
     public const string LOT_CR_ORPHAN = "lot100cr_orphan";
     public const string LOT_CR_PRIESTESS = "lot100cr_priestf";
     public const string LOT_CR_SER_MARON = "lot100cr_templar";
     public const string LOT_CR_STEN = "GEN_FL_STEN";
     public const string LOT_CR_DOOMS_FARMER1 = "lot100cr_dooms_farmer1";
     public const string LOT_CR_DOOMS_FARMER2 = "lot100cr_dooms_farmer2";
     public const string LOT_CR_DOOMS_PRIEST = "lot100cr_dooms_priest";
     public const string LOT_CR_CHASIND_ARGUMENT_TEMPLAR = "lot100cr_templar3";
     public const string LOT_CR_CHASIND_ARGUMENT_FARMER = "lot100cr_farmer2";
     public const string LOT_CR_CHASIND_ARGUMENT_CHASID = "lot100cr_chasind2";
     public const string LOT_CR_DWARF_MERCHANT = "lot100cr_dwarfmerch";
     public const string LOT_CR_PRIEST = "lot100cr_dwarfm_priest";
     public const string LOT_CR_DWARF_MERCHANT_FARMER1 = "lot100cr_dwarfm_farmer1";
     public const string LOT_CR_DWARF_MERCHANT_FARMER2 = "lot100cr_dwarfm_farmer2";
     public const string LOT_CR_ROBBED_MAN = "lot100cr_robbed1";
     public const string LOT_CR_ROBBED_WOMAN = "lot100cr_robbed_woman";
     public const string LOT_CR_ROBBED_CHILD = "lot100cr_robbed_child";
     public const string LOT_CR_CHANTER = "lot100cr_chanter";
     public const string LOT_CR_JACOBSON_SOLDIER = "lot105cr_jacobson_soldier";
     public const string LOT_CR_DOOM_GIRL = "lot100cr_doom_girl";
     public const string LOT_CR_DOOM_BOY = "lot100cr_doom_boy";

     public const string LOT_CR_CHANTRY_FARMER = "lot110cr_farmer";
     public const string LOT_CR_CHANTRY_REFUGEEF = "lot100cr_refugeef";
     public const string LOT_CR_CHANTRY_REFUGEEM = "lot100cr_refugeem";
     public const string LOT_CR_CHANTRY_BRYANT = "lot110cr_bryant";
     public const string LOT_CR_CHANTRY_DONALL = "lot110cr_ser_donall";
     public const string LOT_CR_CHANTRY_TEMPLAR = "lot110cr_templar";
     public const string LOT_CR_CHANTRY_GEN_TEMPLAR = "lot110cr_generictemplar";
     public const string LOT_CR_CHANTRY_GRAND_CLERIC = "lot110cr_grand_cleric";
     public const string LOT_CR_CHANTRY_NUN = "lot110cr_nun";
     public const string LOT_CR_CHANTRY_PRIEST = "lot110cr_priest";
     public const string LOT_CR_CHANTRY_PRIEST_BRAZIER = "lot110cr_priest_brazier";
     public const string LOT_CR_CHANTRY_PRIEST_CHANTER1 = "lot110cr_priest_chanter1";
     public const string LOT_CR_CHANTRY_PRIEST_CHANTER2 = "lot110cr_priest_chanter2";
     public const string LOT_CR_CHANTRY_PRIEST_CHANTER3 = "lot110cr_priest_chanter3";
     public const string LOT_CR_CHANTRY_DARKSPAWN = "lot110cr_darkspawn";
     public const string LOT_CR_CHANTRY_MATRON_GUARD = "lot110cr_matronguard";

     public const string LOT_CR_TAVERN_COMMANDER = "lot120cr_commander";
     public const string LOT_CR_TAVERN_SOLDIER = "lot120cr_soldier";
     public const string LOT_CR_TAVERN_BARD_F = "lot120cr_bard_f";
     public const string LOT_CR_TAVERN_BARD_M = "lot120cr_bard_m";
     public const string LOT_CR_TAVERN_COOK = "lot120cr_cook";
     public const string LOT_CR_TAVERN_MERCHANT = "lot120cr_merchant";
     public const string LOT_CR_TAVERN_PRIEST_F = "lot120cr_priest_f";
     public const string LOT_CR_TAVERN_PRIEST_M = "lot120cr_priest_m";
     public const string LOT_CR_TAVERN_REFUGEEF = "lot120cr_refugeef";
     public const string LOT_CR_TAVERN_REFUGEEM = "lot120cr_refugeem";
     public const string LOT_CR_TAVERN_OWNER = "lot120cr_tavern_owner";
     public const string LOT_CR_TAVERN_DARKSPAWN = "lot120cr_darkspawn";

     // Bears plot
     public const string LOT_CR_DISESED_BEAR = "lot100cr_bear";

     // Scholorly Persuits plot
     public const string LOT_CR_AGITATED_BEAR = "lot105cr_agitated_bear";
     public const string LOT_CR_AGITATED_SPIDER = "lot105cr_giant_spider";
     public const string LOT_CR_AGITATED_WOLF = "lot105cr_agitated_wolf";

     // Trial Run plot
     public const string LOT_CR_TR_FIRST_DARKSPAWN = "lot105cr_tr_first_darkspawn";
     public const string LOT_CR_TR_SECOND_DARKSPAWN = "lot105cr_tr_secnd_darkspawn";
     public const string LOT_CR_TR_THIRD_DARKSPAWN = "lot105cr_tr_third_darkspawn";
     public const string LOT_CR_TR_LAST_DARKSPAWN = "lot105cr_tr_last_darkspawn";

     // Last Shift Plot
     public const string LOT_CR_MP_MINER_1 = "lot184cr_miner1";
     public const string LOT_CR_MP_MINER_2 = "lot184cr_miner2";
     public const string LOT_CR_MP_MINER_3 = "lot184cr_miner3";
     public const string LOT_CR_MP_MINER_4 = "lot184cr_miner4";

     //------------------------------------------------------------------------------
     // WAYPOINTS
     //------------------------------------------------------------------------------
     public const string LOT_WP_DANES_ENTRANCE1 = "lot120wp_entrance1";
     public const string LOT_WP_LOTHERING_ENTRANCE = "wmw_lot_south";
     public const string LOT_WP_ORPHAN = "lot100wp_orphan";
     public const string LOT_WP_FROM_DANES1 = "lot100wp_from_danes1"; //used as closest waypoint to gossips for debug
     public const string ZZ_LOT_WP_BODAHN = "zz_lot100wp_bodahn"; //cnm: used as a debug waypoint to jump to Bodahn encounter
     public const string LOT_WP_LC_MIRIAM = "jp_wrd110cr_miriam_0";
     public const string LOT_WP_LC_FELERRON = "jp_wrd110cr_felerron_0";
     public const string LOT_WP_LC_BARLIN = "jp_wrd110cr_barlin_0";
     public const string LOT_WP_LC_ALLISON = "jp_wrd110cr_allison_0";
     public const string LOT_WP_LC_PC_TO_MIRIAM = "lot105wp_pc_to_miriam";
     public const string LOT_WP_LC_MIRIAM2 = "lot105wp_miriam2";
     public const string LOT_WP_LC_CODY = "lot105wp_cody";
     public const string LOT_WP_LC_MENDEL = "lot105wp_mendel";
     public const string LOT_WP_LC_SAEVRIN = "lot105wp_saevrin";
     public const string LOT_WP_FROM_REMEM_CHANT = "lot105wp_from_remem_chant";
     public const string LOT_WP_LC_DARKSPAWN_ATTACK = "lot105wp_darkspawn_attack";
     public const string LOT_WP_REFUGEE_SCATTER = "lot100wp_scatter_1";
     public const string LOT_WP_DWARF_MERCH = "mn_lot100_merch";
     public const string LOT_WP_BANDITS_EXIT = "lot100wp_bandits_exit";

     //------------------------------------------------------------------------------
     // PLACEABLES
     //------------------------------------------------------------------------------
     public const string LOT_IP_CHANTER_BOARD = "lot100ip_chantry_board";
     public const string LOT_IP_DOOR_CHANTRY_TO_LOT = "lot110ip_to_village";
     public const string LOT_IP_DOOR_CHANTRY_TO_LOT_LC = "lot110ip_to_lot_lc";
     public const string LOT_IP_DOOR_DANES_TO_LOT = "lot120ip_to_village";
     public const string LOT_IP_DOOR_DANES_TO_LOT_LC = "lot120ip_to_lot_lc";
     public const string LOT_IP_STENS_CAGE = "lot100ip_stens_cage";
     public const string LOT_IP_GC_LOCKBOX = "lot110ip_gc_box";
     public const string LOT_IP_MATRON_DOOR = "lot110ip_grand_cleric_door";
     public const string LOT_IP_SARHA_CORPSE = "lot100ip_sarha_corpse";
     public const string LOT_IP_BARLIN_CRATE_1 = "lot105ip_ml_crate1";
     public const string LOT_IP_BARLIN_CRATE_2 = "lot105ip_ml_crate2";
     public const string LOT_IP_BARLIN_CRATE_3 = "lot105ip_ml_crate3";
     public const string LOT_IP_JUSTINE_SCROLL_RACK = "lot105ip_scroll_rack";
     public const string LOT_IP_CHANTRY_WAGON = "lot100ip_chantry_wagon";
     public const string LOT_IP_URN_BOOKSHELF = "lot110ip_codex_urn";

     // Last Rites plot
     public const string LOT_IP_SER_BRYANT = "lot105ip_lr_bryant";
     public const string LOT_IP_SER_MARON = "lot105ip_lr_maron";
     public const string LOT_IP_SER_PERTH = "lot105ip_lr_perth";

     //------------------------------------------------------------------------------
     // TRIGGERS
     //------------------------------------------------------------------------------
     public const string LOT_TR_TAVERN_SOLDIERS = "lot120tr_talk_soldiers";

     //------------------------------------------------------------------------------
     // AREAS
     //------------------------------------------------------------------------------
     public const string LOT_AR_LOTHERING = "lot100ar_lothering";
     public const string LOT_AR_LOTHERING_LC = "lot105ar_lothering_lc";
     public const string LOT_AR_CHANTRY = "lot110ar_chantry";
     public const string LOT_AR_DANES_REFUGE = "lot120ar_danes_refuge";

     //------------------------------------------------------------------------------
     // CONVERSATIONS
     //------------------------------------------------------------------------------
     //public const string LOT_DG_CHANTER_BOARD           = "lot100_chanter_board.dlg";
     public const string LOT_DG_FENNON_PARTY = "lot181lt_fennon_party.dlg";
     public const string LOT_DG_AUDIENCE = "lot100_audience.dlg";
     public const string LOT_DG_DOOMSAYER = "lot100_doomsayer.dlg";
     public const string LOT_DG_ORPHAN_SHOUT = "lot100_orphan2.dlg";
     public const string LOT_DG_DOOMSHOUTS = "lot100_doomshouts.dlg";

     //------------------------------------------------------------------------------
     // ITEMS
     //------------------------------------------------------------------------------
     public const string LOT_IM_STENS_KEY = "lot100im_stens_cage_key.uti";
     public const string LOT_IM_CABINET_KEY = "lot110im_cabinet_key.uti";
     public const string LOT_IM_KNIGHTS_LOCKET = "lot100im_knights_locket.uti";
     public const string LOT_IM_KNIGHTS_NOTE = "lot100im_knights_note.uti";
     public const string LOT_IM_KNIGHTS_FAVOR_JOURNAL = "lot105im_knights_favor_note.uti";
     public const string LOT_IM_JUSTINE_SCROLL = "lot185im_justine_scroll.uti";
     public const string LOT_IM_BEAR_ORGANS = "lot105im_bear_organs.uti";
     public const string LOT_IM_SPIDER_ORGANS = "lot105im_spider_organs.uti";
     public const string LOT_IM_WOLF_ORGANS = "lot105im_wolf_organs.uti";
     public const string LOT_IM_MIRIAMS_NOTE = "lot100im_miriams_note.uti";
     public const string LOT_IM_SARHAS_KEEPSAKE = "lot100im_sarhas_keepsake.uti";
     public const string LOT_R_MIRIAM_RECIPE = "gen_im_cft_hrb_102.uti";
     public const string LOT_R_MIRIAM_POULTICE = "gen_im_qck_health_101.uti";
     public const string LOT_R_BARLIN_POISON = "gen_im_qck_poison_101.uti";
     public const string LOT_R_ALLISON_TRAP = "gen_im_qck_trap_104.uti";
     public const string LOT_IM_MIRIAM_POULTICE = "gen_im_qck_health_101";
     public const string LOT_IM_BARLIN_POISON = "gen_im_qck_poison_101";
     public const string LOT_IM_ALLISON_TRAP = "gen_im_qck_trap_104";
     //light content
     public const string LOT_IM_MAGENOTE = "lot100im_magenote";
     public const string R_LOT_IM_MAGENOTE = "lot100im_magenote.uti";
     //------------------------------------------------------------------------------
     // CUTSCENES
     //------------------------------------------------------------------------------
     public const string LOT_LC_VILLAGERS_RETURN = "villagers_return.cut";

     //------------------------------------------------------------------------------
     // TEAMS
     //------------------------------------------------------------------------------
     public const int LOT_TEAM_BANDITS = 1;
     public const int LOT_TEAM_GRAND_MATRON = 2;
     public const int LOT_TEAM_LC_NORTH_DARKSPAWN = 3;
     public const int LOT_TEAM_LC_SOUTH_DARKSPAWN = 4;
     public const int LOT_TEAM_LC_TOWN_DARKSPAWN = 5;
     public const int LOT_TEAM_LC_CHANTRY_DARKSPAWN = 6;
     public const int LOT_TEAM_LC_DANES_DARKSPAWN = 7;
     public const int LOT_TEAM_BRYANT_TEMPLAR = 8;
     public const int LOT_TEAM_BANDIT_GROUP1 = 9;
     public const int LOT_TEAM_BANDIT_GROUP2 = 10;
     public const int LOT_TEAM_BANDIT_GROUP3 = 11;
     public const int LOT_TEAM_LAST_KEEPSAKE = 12;
     public const int LOT_TEAM_DANES_SOLDIERS = 13;
     public const int LOT_TEAM_DANES_CUSTOMERS = 14;
     public const int LOT_TEAM_BEARS = 15;
     public const int LOT_TEAM_BODAHN_HURLOCK = 16;
     public const int LOT_TEAM_LC_FAR_AFIELD_DARKSPAWN = 17;
     public const int LOT_TEAM_LC_FD_RIGHT_VILLAGERS = 18;
     public const int LOT_TEAM_LC_FD_LEFT_VILLAGERS = 19;
     public const int LOT_TEAM_LC_FD_CENTER_VILLAGERS = 20;
     public const int LOT_TEAM_LC_FD_BACK_DARKSPAWN = 21;
     public const int LOT_TEAM_LC_FD_CENTER_DARKSPAWN = 22;
     public const int LOT_TEAM_LC_FD_LEFT_DARKSPAWN = 23;
     public const int LOT_TEAM_LC_FD_RIGHT_DARKSPAWN = 24;
     public const int LOT_TEAM_LC_FD_ENTRY_DARKSPAWN = 25;
     public const int LOT_TEAM_LC_FD_FENNON_PARTY = 26;
     public const int LOT_TEAM_LC_RHUGGERS = 27;
     public const int LOT_TEAM_LC_MINE_DARKSPAWN = 28;
     public const int LOT_TEAM_LC_SP_ORGAN_DONORS = 29;
     public const int LOT_TEAM_REFUGEE_AMBUSH = 30;
     public const int LOT_TEAM_LC_SL_DESERTERS3 = 31;
     public const int LOT_TEAM_LC_SL_DARKSPAWN3 = 32;
     public const int LOT_TEAM_LC_JACOBSON_DARKSPAWN = 33;
     public const int LOT_TEAM_LC_VILLAGERS = 34;
     public const int LOT_TEAM_LC_FD_FIRST_LEFT = 35;
     public const int LOT_TEAM_LC_FD_FIRST_RIGHT = 36;
     public const int LOT_TEAM_LC_FD_BACK_DARKSPAWN_2 = 37;
     public const int LOT_TEAM_LC_JACOBSON_DARKSPAWN_2 = 38;
     public const int LOT_TEAM_LC_JACOBSON_DARKSPAWN_3 = 39;
     public const int LOT_TEAM_LC_JACOBSONS_MEN = 40;
     public const int LOT_TEAM_LC_MERCHANTS_CARTS = 41;

     /*------------------------------------------------------------------------------

     These were for Lothering LC and are no longer needed

     public const int WRD_TEAM_CHANTRY_GHOSTS               = 41;
     public const int WRD_TEAM_ULLER_CAVE                   = 42;
     public const int WRD_TEAM_ULLER_CAVE_2                 = 43;
     public const int WRD_TEAM_FENNON_DOWN                  = 44;
     public const int WRD_TEAM_BROODMOTHER_SPIDERS          = 45;
     public const int WRD_TEAM_BROODMOTHER_BERESKARN        = 46;
     public const int WRD_TEAM_BROODMOTHER_ENTRANCE_SPIDERS = 47;
     public const int WRD_TEAM_BROODMOTHER_DEAD_THINGS      = 48;
     public const int WRD_TEAM_BROODMOTHER                  = 49;
     public const int WRD_TEAM_BROODMOTHER_TENTACLE         = 50;
     public const int WRD_TEAM_CHANTRY_SPIDERS_1            = 51;
     public const int WRD_TEAM_CHANTRY_SPIDERS_2            = 52;
     public const int WRD_TEAM_MERCHANTS_CARTS              = 53;
     ------------------------------------------------------------------------------*/

     public const int LOT_TEAM_LC_CB_DARKSPAWN = 54;
     public const int LOT_TEAM_LC_CB_GIANT_SPIDERS = 55;
     public const int LOT_TEAM_CHANTRY_VILLAGERS = 56;
     public const int LOT_TEAM_CHANTRY_WAGONS = 57;
     public const int LOT_TEAM_LELIANA = 58;
     public const int LOT_TEAM_LC_ANGRY_SOLDIERS = 59;
     public const int LOT_TEAM_LC_DESERTERS_1 = 60;
     public const int LOT_TEAM_LC_DESERTERS_2 = 61;
     public const int LOT_TEAM_LC_DESERTERS_3 = 62;
     public const int LOT_TEAM_LC_JACOBSON_SOLD = 63;
     public const int LOT_TEAM_LC_TR_WAVE_1 = 64;
     public const int LOT_TEAM_LC_TR_WAVE_2 = 65;
     public const int LOT_TEAM_LC_TR_WAVE_3 = 66;
     public const int LOT_TEAM_LC_TR_WAVE_4 = 67;
     public const int LOT_TEAM_CHANTRY_BRAZIERS = 68;
     public const int LOT_TEAM_ROBBED_FAMILY = 69;
     public const int LOT_TEAM_GAIDER = 70;
     public const int LOT_TEAM_REFUGEE_AMBUSH_2 = 71;
     public const int LOT_TEAM_LOTHERING_SPIDERS = 72;

     //------------------------------------------------------------------------------
     // AUDIO EVENTS
     //------------------------------------------------------------------------------
     public const int LOT_AUDIO_DANES_REFUGE = 2;
     public const int LOT_AUDIO_TAVERN_SOLDIERS_SURRENDER = 54;
     public const int LOT_AUDIO_TAVERN_SOLDIERS_KILLED = 55;
}