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
     //constants for the arl eamon plot

     //#include"events_h"

     //creatures
     public const string ARL_CR_TEAGAN = "arl110cr_teagan";
     public const string ARL_R_CR_TEAGAN = "arl110cr_teagan.utc";
     public const string ARL_CR_CHANTRY_BOARD = "arl100cr_chanter_redcliffe";
     public const string ARL_CR_TOMAS = "arl100cr_tomas";
     public const string ARL_CR_ISOLDE = "arl100cr_isolde";
     public const string ARL_CR_ISOLDE_GUARD = "arl100cr_isolde_guard";
     public const string ARL_CR_ANYON = "arl100cr_anyon";
     public const string ARL_CR_DWYN = "arl130cr_dwyn";
     public const string ARL_CR_BRIGID = "arl130cr_brigid";
     public const string ARL_CR_THUG_1 = "arl130cr_thug_1";
     public const string ARL_CR_THUG_2 = "arl130cr_thug_2";
     public const string ARL_CR_MURDOCK = "arl100cr_murdock";
     public const string ARL_CR_OWEN = "arl120cr_owen";
     public const string ARL_CR_BLACKSMITH = "arl120cr_blacksmith";
     public const string ARL_CR_HANNAH = "arl110cr_hannah";
     public const string ARL_CR_BEVIN = "arl140cr_bevin";
     public const string ARL_CR_KAITLYN = "arl110cr_kaitlyn";
     public const string ARL_CR_BERWICK = "arl150cr_berwick";
     public const string ARL_CR_LLOYD = "arl150cr_lloyd";
     public const string ARL_CR_BELLA = "arl150cr_bella";
     public const string ARL_CR_VALENA = "arl210cr_valena";
     public const string ARL_CR_JOWAN = "arl210cr_jowan";
     public const string ARL_CR_CONNOR = "arl220cr_connor";
     public const string ARL_CR_PERTH = "arl100cr_perth";
     public const string ARL_CR_GUDRUN = "arl220cr_gudrun";
     public const string ARL_CR_DEMON = "arl300cr_demon";
     public const string ARL_CR_GUARD_HALL = "arl220cr_guard_hall";
     public const string ARL_CR_GUARD = "arl200cr_guard";
     public const string ARL_CR_GUARD_BACK = "arl200cr_guard_back";
     public const string ARL_CR_CASTLE_DEMON = "arl230cr_demon";
     public const string ARL_CR_CASTLE_DEMON_2 = "arl230cr_demon_2";
     public const string ARL_CR_CASTLE_DEMON_3 = "arl230cr_demon_3";
     public const string ARL_CR_FADE_DEMON_1 = "arl300cr_demon1";
     public const string ARL_CR_FADE_DEMON_2 = "arl300cr_demon2";
     public const string ARL_CR_FADE_DEMON_3 = "arl300cr_demon3";
     public const string ARL_CR_JOWAN_CORPSE = "arl210cr_jowan_corpse";
     public const string ARL_CR_FAKE_CONNOR = "arl300cr_fake_connor";
     public const string ARL_CR_IRVING = "arl220cr_irving";
     public const string ARL_CR_CIRCLE_MAGE = "arl220cr_circle_mage";
     public const string ARL_CR_EAMON = "arl230cr_eamon";
     public const string ARL_CR_WATCHMAN = "arl100cr_watchman";
     public const string ARL_CR_CHAMBERLAIN = "arl220cr_chamberlain";
     public const string ARL_CR_DOOMSAYER = "arl100cr_doomsayer";
     public const string ARL_CR_CAPTAIN = "arl200cr_captain";
     public const string ARL_CR_INVISIBLE_JOWAN = "arl210cr_invis_jowan";
     public const string ARL_CR_AMBUSH_ARMOR = "arl220cr_suit_of_armor";

     public const string ARL_CR_SCAVENGER = "arl100cr_scavenger";
     public const string ARL_CR_SIEGE_SHALE = "arl101cr_siege_shale";

     public const string ARL_CR_KNIGHT_1 = "arl100cr_knight_1";
     public const string ARL_CR_KNIGHT_2 = "arl100cr_knight_2";
     public const string ARL_CR_KNIGHT_3 = "arl100cr_knight_3";

     public const string ARL_CR_MILITIA_1 = "arl100cr_militia_1";
     public const string ARL_CR_MILITIA_2 = "arl100cr_militia_2";
     public const string ARL_CR_MILITIA_3 = "arl100cr_militia_3";
     public const string ARL_CR_MILITIA_4 = "arl100cr_militia_4";
     public const string ARL_CR_MILITIA_5 = "arl100cr_militia_5";

     public const string ARL_CR_MILITIA_DRUNK_1 = "arl150cr_drunk_1";
     public const string ARL_CR_MILITIA_DRUNK_2 = "arl150cr_drunk_2";
     public const string ARL_CR_MILITIA_DRUNK_3 = "arl150cr_drunk_3";

     public const string ARL_CR_CASTLE_DEMON_CORPSE = "arl230cr_demon_corpse";
     public const string ARL_CR_SIEGE_CORPSE = "arl101cr_undead";
     public const string ARL_R_CR_SIEGE_CORPSE = "arl101cr_undead.utc";

     public const string ARL_CR_SIEGE_KNIGHT_TALKER = "arl101cr_knight_1_talker";
     public const string ARL_CR_CASTLE_HOSTILE = "arl200cr_castle_hostile";

     //items
     public const string ARL_IT_BARREL_OF_LAMP_OIL = "arl170it_lamp_oil";
     public const string ARL_R_IT_BARREL_OF_LAMP_OIL = "arl170it_lamp_oil.uti";
     public const string ARL_IT_CONTROL_STONE = "arl150it_control_stone";
     public const string ARL_R_IT_CONTROL_STONE = "arl150it_control_stone.uti";
     public const string ARL_IT_HOLY_SYMBOL = "arl110it_holy_symbol";
     public const string ARL_R_IT_HOLY_SYMBOL = "arl110it_holy_symbol.uti";
     public const string ARL_IT_CIRCLE_LETTER = "arl160it_circle_letter";
     public const string ARL_R_IT_CIRCLE_LETTER = "arl160it_circle_letter.uti";
     public const string ARL_IT_NOTE_OF_SALE = "arl170it_note_of_sale";
     public const string ARL_R_IT_NOTE_OF_SALE = "arl170it_note_of_sale.uti";
     public const string ARL_IT_SIGNET_RING = "arl100it_signet_ring";
     public const string ARL_R_IT_SIGNET_RING = "arl100it_signet_ring.uti";
     public const string ARL_IT_STASH = "arl120it_stash";
     public const string ARL_R_IT_STASH = "arl120it_stash.uti";
     public const string ARL_IT_SPY_LETTER = "arl150it_spy_letter";
     public const string ARL_R_IT_SPY_LETTER = "arl150it_spy_letter.uti";
     public const string ARL_IT_CHEST_KEY = "arl140it_chest_key";
     public const string ARL_R_IT_CHEST_KEY = "arl140it_chest_key.uti";
     public const string ARL_IT_BEVIN_SWORD = "gen_im_wep_mel_lsw_rwd";
     public const string ARL_R_IT_BEVIN_SWORD = "gen_im_wep_mel_lsw_rwd.uti";
     public const string ARL_IT_OWEN_STASH_KEY = "arl120it_stash_key";
     public const string ARL_R_IT_OWEN_STASH_KEY = "arl120it_stash_key.uti";
     public const string ARL_IT_LITE_RED_ZOMBIE = "arl220_corpse_gall";
     public const string ARL_R_IT_LITE_RED_ZOMBIE = "arl220_corpse_gall.uti";
     public const string ARL_IT_JOWAN_FADE_STAFF = "gen_im_wep_mag_sta_mgc";
     public const string ARL_R_IT_JOWAN_FADE_STAFF = "gen_im_wep_mag_sta_mgc.uti";
     public const string ARL_R_IT_FADE_REWARD_TOME = "gen_im_misc_demonbook.uti";
     public const string ARL_R_IT_DWYN_LOCKBOX_KEY = "arl130it_lockbox_key.uti";

     //Items for the militia in the siege.
     //Note: low quality items for if the player does not get new equipment are
     //equiped by default on the creature templates.
     public const string ARL_IT_MILITIA_ARMOR_GOOD = "gen_im_arm_cht_lgt_rlr";
     public const string ARL_R_IT_MILITIA_ARMOR_GOOD = "gen_im_arm_cht_lgt_rlr.uti";
     public const string ARL_IT_MILITIA_ARMOR_STANDARD = "gen_im_arm_cht_lgt_ltr";
     public const string ARL_R_IT_MILITIA_ARMOR_STANDARD = "gen_im_arm_cht_lgt_ltr.uti";

     public const string ARL_IT_MILITIA_GLOVES_GOOD = "gen_im_arm_glv_lgt_rlr";
     public const string ARL_R_IT_MILITIA_GLOVES_GOOD = "gen_im_arm_glv_lgt_rlr.uti";
     public const string ARL_IT_MILITIA_GLOVES_STANDARD = "gen_im_arm_glv_lgt_ltr";
     public const string ARL_R_IT_MILITIA_GLOVES_STANDARD = "gen_im_arm_glv_lgt_ltr.uti";

     public const string ARL_IT_MILITIA_BOOTS_GOOD = "gen_im_arm_bot_lgt_rlr";
     public const string ARL_R_IT_MILITIA_BOOTS_GOOD = "gen_im_arm_bot_lgt_rlr.uti";
     public const string ARL_IT_MILITIA_BOOTS_STANDARD = "gen_im_arm_bot_lgt_ltr";
     public const string ARL_R_IT_MILITIA_BOOTS_STANDARD = "gen_im_arm_bot_lgt_ltr.uti";

     public const string ARL_IT_MILITIA_HELMET_GOOD = "gen_im_arm_hel_lgt_std";
     public const string ARL_R_IT_MILITIA_HELMET_GOOD = "gen_im_arm_hel_lgt_std.uti";
     public const string ARL_IT_MILITIA_HELMET_STANDARD = "gen_im_arm_hel_lgt_rlr";
     public const string ARL_R_IT_MILITIA_HELMET_STANDARD = "gen_im_arm_hel_lgt_rlr.uti";

     public const string ARL_IT_MILITIA_BOW_GOOD = "gen_im_wep_rng_sbw_sbw";
     public const string ARL_R_IT_MILITIA_BOW_GOOD = "gen_im_wep_rng_sbw_sbw.uti";
     public const string ARL_IT_MILITIA_BOW_STANDARD = "gen_im_wep_rng_sbw_sbw";
     public const string ARL_R_IT_MILITIA_BOW_STANDARD = "gen_im_wep_rng_sbw_sbw.uti";

     public const string ARL_IT_MILITIA_WEAPON_GOOD = "gen_im_wep_mel_dag_dag";
     public const string ARL_R_IT_MILITIA_WEAPON_GOOD = "gen_im_wep_mel_dag_dag.uti";
     public const string ARL_IT_MILITIA_WEAPON_STANDARD = "gen_im_wep_mel_dag_dag";
     public const string ARL_R_IT_MILITIA_WEAPON_STANDARD = "gen_im_wep_mel_dag_dag.uti";

     //waypoints
     public const string ARL_WP_PC_INTRODUCTION = "arl110wp_pc_introduction";
     public const string ARL_WP_CONNOR_CUTSCENE = "arl220wp_connor_cutscene";
     public const string ARL_WP_TEAGAN_RUNS_OUT = "arl100wp_teagan_runs_out";
     public const string ARL_WP_FROM_WORLD = "arl300wp_from_world";
     public const string ARL_WP_BY_EAMON = "arl230wp_by_eamon";
     public const string ARL_WP_ISOLDE_BY_EAMON = "arl230wp_isolde_by_eamon";
     public const string ARL_WP_TAVERN_BERWICK_EXIT = "arl150wp_from_village";
     public const string ARL_WP_TEAGAN_MILL = "arl100wp_teagan_at_mill";
     public const string ARL_WP_CONNOR_2 = "arl300wp_connor_2";
     public const string ARL_WP_CONNOR_3 = "arl300wp_connor_3";
     public const string ARL_WP_FROM_VILLAGE = "arl150wp_from_village";
     public const string ARL_WP_CONNOR_DEAD = "arl_230wp_after_connor_dead";
     public const string ARL_WP_ANYON_POST = "arl100wp_anyon_post";
     public const string ARL_WP_SHALE_MILITIA = "arl100wp_shale_militia";
     public const string ARL_WP_SHALE_KNIGHTS = "arl100wp_shale_knights";
     public const string ARL_WP_END_CUTSCENE = "arl100wp_end_cutscene";
     public const string ARL_WP_AFTER_SIEGE = "arl100wp_after_battle";
     public const string ARL_WP_CRATE_MOVED = "arl120wp_crate_moved";
     public const string ARL_WP_CONNOR_SAVED = "arl230wp_connor_saved";
     public const string ARL_WP_COURTYARD_PERTH = "arl200wp_courtyard_perth";
     public const string ARL_WP_COURTYARD_KNIGHT1 = "arl200wp_courtyard_knight1";
     public const string ARL_WP_COURTYARD_KNIGHT2 = "arl200wp_courtyard_knight2";
     public const string ARL_WP_COURTYARD_KNIGHT3 = "arl200wp_courtyard_knight3";
     public const string ARL_WP_TEAGAN_UNCONSCIOUS = "arl110wp_teagan_unconscious";
     public const string ARL_WP_HANNAH_AFTER_BATTLE = "arl110wp_hannah_after_battle";
     public const string ARL_WP_MILITIA_1_DESTINATION = "arl100wp_militia_1_dest";
     public const string ARL_WP_TEAGAN_AFTER_DEMON = "arl220wp_teagan_after_demon";
     public const string ARL_WP_ISOLDE_DURING_FIGHT = "arl220wp_isolde_fight";
     public const string ARL_WP_TAVERN_LLOYD_DEAD = "arl150wp_lloyd_dead";

     public const string ARL_WP_MAPNOTE_FADE_PORTAL_2 = "arl300wp_mapnote_portal_2";
     public const string ARL_WP_MAPNOTE_FADE_PORTAL_3 = "arl300wp_mapnote_portal_3";
     public const string ARL_WP_MAPNOTE_FADE_PORTAL_4 = "arl300wp_mapnote_portal_4";
     public const string ARL_WP_MAPNOTE_VILLAGE_TO_CASTLE = "arl100wp_mapnote_castle";
     public const string ARL_WP_MAPNOTE_VILLAGE_TO_WORLD_MAP_2 = "arl100wp_mapnote_worldmap2";
     public const string ARL_WP_SIEGE_PARTY_START = "arl101wp_party_start";
     public const string ARL_WP_SIEGE_SHALE_KNIGHTS = "arl101wp_shale_knights";
     public const string ARL_WP_SIEGE_SHALE_MILITIA = "arl101wp_shale_militia";
     public const string ARL_WP_OIL_TRAP_FIRE = "arl101wp_oil_trap_fire";
     public const string ARL_WP_SIEGE_PERTH_VILLAGE = "arl101wp_perth_village";
     public const string ARL_WP_SIEGE_KNIGHT_1_VILLAGE = "arl101wp_knight_1_village";
     public const string ARL_WP_SIEGE_KNIGHT_2_VILLAGE = "arl101wp_knight_2_village";
     public const string ARL_WP_SIEGE_KNIGHT_3_VILLAGE = "arl101wp_knight_3_village";
     public const string ARL_WP_SIEGE_MILITIA_1_VILLAGE = "arl101wp_militia_1_village";
     public const string ARL_WP_SIEGE_WINDMILL_DESTINATION = "arl101wp_moveto_mill";
     public const string ARL_WP_SIEGE_VILLAGE_DESTINATION = "arl101wp_moveto_village";
     public const string ARL_WP_SIEGE_FOG = "arl101wp_fog";

     public const string ARL_WP_AFTER_BATTLE_TOMAS = "arl100wp_ab_tomas";
     public const string ARL_WP_AFTER_BATTLE_MURDOCK = "arl100wp_ab_murdock";
     public const string ARL_WP_AFTER_BATTLE_PERTH = "arl100wp_ab_perth";
     public const string ARL_WP_AFTER_BATTLE_WATCHMAN = "arl100wp_ab_watchman";
     public const string ARL_WP_AFTER_BATTLE_KNIGHT_1 = "arl100wp_ab_knight_1";
     public const string ARL_WP_AFTER_BATTLE_KNIGHT_2 = "arl100wp_ab_knight_2";
     public const string ARL_WP_AFTER_BATTLE_KNIGHT_3 = "arl100wp_ab_knight_3";

     //placeables
     public const string ARL_IP_CRATE = "arl120ip_crate";
     public const string ARL_IP_OWEN_STASH_PLOT = "arl120ip_trap_door";
     public const string ARL_IP_OWEN_STASH_REWARD = "arl120ip_trap_door_2";
     public const string ARL_IP_DWARVEN_CHEST = "arl170ip_dwarven_chest";
     public const string ARL_IP_KEG = "arl150ip_keg";
     public const string ARL_IP_DOOR_BLACKSMITH = "arl100ip_door_owen";
     public const string ARL_IP_DOOR_CASTLE_PASSAGE = "arl190ip_door_dungeon";
     public const string ARL_IP_LEDGERS = "arl170ip_ledgers";
     public const string ARL_IP_DOOR_DWYN = "arl100ip_door_dwyn";
     public const string ARL_IP_PAPERS = "arl160ip_papers_chantry";
     public const string ARL_IP_DOOR_JOWAN = "arl210ip_jowan_door";
     public const string ARL_IP_DOOR_DUNGEON_TO_MAIN_FLOOR = "arl210ip_door_main_floor";
     public const string ARL_IP_OIL_BARRELS_EXTRA = "arl170ip_barrels";
     public const string ARL_IP_OIL_BARREL = "arl170ip_oil_barrel";
     public const string ARL_IP_DOOR_DUNGEON_TO_WINDMILL = "arl210ip_door_village";
     public const string ARL_IP_CASTLE_GATE = "arl200ip_door_gate";
     public const string ARL_IP_CASTLE_GATE_IVNISIBLE_WALL = "arl200ip_invis_gate_wall";
     public const string ARL_IP_CASTLE_GATE_LEVER = "arl200ip_gate_lever";
     public const string ARL_IP_DOOR_CASTLE_MAIN_HALL = "arl220ip_door_main_hall";
     public const string ARL_IP_DOOR_CASTLE_TO_VILLAGE = "arl200ip_door_village";
     public const string ARL_IP_WORLD_MAP_CASTLE_TO_VILLAGE = "arl200ip_to_village_map";
     public const string ARL_IP_DOOR_VILLAGE_TO_CASTLE = "arl100ip_door_castle";
     public const string ARL_IP_WORLD_MAP_VILLAGE_TO_CASTLE = "arl100ip_to_castle_map";
     public const string ARL_IP_BARRICADE_PREFIX = "arl101ip_barricade_";
     public const string ARL_IP_FIRE_TRAP_TARGET = "arl101ip_fire_target";
     public const string ARL_IP_FADE_PARTY_INVENTORY_STORAGE = "arl200ip_party_inventory";
     public const string ARL_IP_FADE_BORROWED_INVENTORY_DUMP = "arl200ip_inventory_dump";
     public const string ARL_IP_DOOR_DEMON_FIGHT_1 = "arl230ip_eamon_door";
     public const string ARL_IP_DOOR_DEMON_FIGHT_2 = "arl230ip_eamon_door_2";
     public const string ARL_IP_DOOR_DEMON_FIGHT_3 = "arl230ip_eamon_door_3";
     public const string ARL_IP_CASTLE_VAULT_DOOR = "arl230ip_door_vault";
     public const string ARL_IP_CHANTRY_BOARD = "arl100ip_chantry_board";
     public const string ARL_IP_FUNERAL_PYRE_TARGET = "arl100ip_pyre_target";
     public const string ARL_IP_SIEGE_BURNING_WOOD = "arl101ip_burning_wood";

     public const string ARL_R_IP_FADE_INVISIBLE_TARGET = "arl300ip_invisible_target.utp";

     //dialogs
     public const string rARL_CR_AMBIENT2 = "arl110cr_ambient2.dlg";
     public const string rZZ_ARL_DG_BATTLE_DEBUGGER = "zz_battle_debugger.dlg";
     public const string rZZ_ARL_DG_DEBUGGER = "zz_main_debugger.dlg";
     public const string rARL_CR_BEVIN = "arl140cr_bevin.dlg";

     //integers
     public const int ARL_MILITIA_MORALE_HIGH = 1;
     public const int ARL_MILITIA_MORALE_LOW = -1;
     public const int ARL_KNIGHTS_MORALE_HIGH = 1;
     public const int ARL_KNIGHTS_MORALE_LOW = -1;

     //AREAS
     public const string ARL_AR_REDCLIFFE_VILLAGE = "arl100ar_redcliffe_village";
     public const string ARL_AR_REDCLIFFE_VILLAGE_NIGHT = "arl101ar_redcliffe_night";
     public const string ARL_AR_CHANTRY = "arl110ar_chantry";

     public const string ARL_AR_BLACKSMITH = "arl120ar_blacksmith";
     public const string ARL_AR_DWYNS_HOME = "arl130ar_dwyns_home";
     public const string ARL_AR_TAVERN = "arl150ar_tavern";
     public const string ARL_AR_WILHELMS_COTTAGE = "arl160ar_wilhelms_cottage";
     public const string ARL_AR_GENERAL_STORE = "arl170ar_general_store";
     public const string ARL_AR_WINDMILL = "arl190ar_windmill";

     public const string ARL_AR_CASTLE_COURTYARD = "arl200ar_redcliffe_castle";
     public const string ARL_AR_CASTLE_DUNGEON = "arl210ar_castle_dungeon";
     public const string ARL_AR_CASTLE_MAIN_FLOOR = "arl220ar_castle_main_floor";
     public const string ARL_AR_FADE = "arl300ar_fade";
     public const string ARL_AR_CASTLE_UPSTAIRS = "arl230ar_castle_upstairs";

     public const string ARL_AR_NONE = "arl000ar_none";
     public const string ARL_AR_INVALID = "arl100ar_invalid";

     //triggers
     public const string ARL_TR_TAVERN_GOSSIP = "gen00tr_gossip";
     public const string ARL_TR_CONNOR_AUTOSAVE = "arl230tr_connor_autosave";

     //Stores
     public const string ARL_STORE_OWEN_EXTRA = "store_arl120cr_owen_extra";
     public const string ARL_STORE_LLOYD_EXTRA = "store_arl150cr_lloyd_extra";
     //Teams

     public const int ARL_TEAM_DWYN = 1;
     public const int ARL_TEAM_JOWAN_ZOMBIE = 2;
     public const int ARL_TEAM_CONNOR_HALL = 3;
     public const int ARL_TEAM_KNIGHTS = 4;
     public const int ARL_TEAM_VILLAGERS = 5;
     public const int ARL_TEAM_OWEN = 6;
     public const int ARL_TEAM_BERWICK = 7;
     public const int ARL_TEAM_LLOYD = 8;
     public const int ARL_TEAM_STEALING_KIGHT = 9;
     public const int ARL_TEAM_JOWAN = 10;
     public const int ARL_TEAM_DEMON_1 = 11;
     public const int ARL_TEAM_DEMON_2 = 12;
     public const int ARL_TEAM_DEMON_3 = 13;
     public const int ARL_TEAM_DEMON_4 = 14;
     public const int ARL_TEAM_MAGE = 15;

     public const int ARL_TEAM_CONNOR_DEMON_CORPSE = 20;
     public const int ARL_TEAM_CONNOR_DEMON_RAGE_DEMONS = 21;
     public const int ARL_TEAM_CASTLE_CORPSE = 22;
     public const int ARL_TEAM_CASTLE_UPSTAIRS_AMBUSH = 23;
     public const int ARL_TEAM_CASTLE_DUNGEON_AMBUSH = 24;
     public const int ARL_TEAM_CASTLE_TROPHY_ARMOR = 25;
     public const int ARL_TEAM_CASTLE_TROPHY_STATUES = 26;
     public const int ARL_TEAM_FADE_GHOSTS = 27;
     public const int ARL_TEAM_CASTLE_TRAP_AMBUSH_1 = 28;
     public const int ARL_TEAM_CASTLE_COMBAT_PLACEABLES = 29;
     public const int ARL_TEAM_CASTLE_COURTYARD_AMBUSH = 30;
     public const int ARL_TEAM_DEMON_FIGHT_MAGIC_BARRIER = 31;
     public const int ARL_TEAM_CASTLE_TROPHY_NEW_ARMOR = 32;
     public const int ARL_TEAM_CASTLE_VILLAGER_CORPSE = 33;
     public const int ARL_TEAM_FADE_DEMON_SPLIT_1 = 34;
     public const int ARL_TEAM_FADE_DEMON_SPLIT_2 = 35;

     public const int ARL_TEAM_ABANDONED_VILLAGE_POST_CASTLE = 52;
     public const int ARL_TEAM_VILLAGE_POST_BATTLE = 53;
     public const int ARL_TEAM_VILLAGE_POST_CASTLE = 54;
     public const int ARL_TEAM_CASTLE_POST_DEMON = 55;
     public const int ARL_TEAM_CASTLE_POST_EAMON = 56;
     public const int ARL_TEAM_CASTLE_WEARWOLVES = 57;
     public const int ARL_TEAM_CASTLE_DWARVES = 58;
     public const int ARL_TEAM_CASTLE_GOLEMS = 59;
     public const int ARL_TEAM_CASTLE_TEMPLARS = 60;
     public const int ARL_TEAM_CASTLE_MAGES = 61;
     public const int ARL_TEAM_CASTLE_ELVES = 62;
     public const int ARL_TEAM_SEALED_PASSAGE = 63;
     public const int ARL_TEAM_TAVERN_GOSSIP = 64;
     public const int ARL_TEAM_POST_BATTLE_SPEECH_CROWD = 65;
     public const int ARL_TEAM_POST_SPEECH_AMBIENTS = 66;
     public const int ARL_TEAM_VILLAGE_ARCHERY_TARGETS = 67;
     public const int ARL_TEAM_VILLAGE_OIL_BARRELS = 68;
     public const int ARL_TEAM_VILLAGE_BARRICADES = 69;

     public const int ARL_TEAM_SIEGE_SHALE = 100;
     public const int ARL_TEAM_SIEGE_WINDMILL_CORPSES_1 = 101;
     public const int ARL_TEAM_SIEGE_WINDMILL_CORPSES_2 = 102;
     public const int ARL_TEAM_SIEGE_WINDMILL_CORPSES_3 = 103;
     public const int ARL_TEAM_SIEGE_VILLAGE_CORPSES = 104;
     public const int ARL_TEAM_CUTSCENE_MILITIA = 106;

     // Defines the creature track to replace for the Jowan Ritual cutscene
     public const string ARL_JOWAN_RITUAL_TRACK_TO_REPLACE = "player_placeholder";

     //Area Events
     public const int ARL_EVENT_FIRE_TRAP_SPREAD = EVENT_TYPE_CUSTOM_EVENT_06;
     public const int ARL_EVENT_BATTLE_OVER = EVENT_TYPE_CUSTOM_EVENT_07;
     public const int ARL_EVENT_CREATURE_GOES_HOSTILE = EVENT_TYPE_CUSTOM_EVENT_08;
     public const int ARL_EVENT_BATTLE_ARMY_DEPLETED = EVENT_TYPE_CUSTOM_EVENT_05;

     //Creature Events
     public const int ARL_EVENT_DEMON_DEATH_SEQUENCE_MIDDLE = EVENT_TYPE_CUSTOM_EVENT_01;
     public const int ARL_EVENT_DEMON_DEATH_SEQUENCE_END = EVENT_TYPE_CUSTOM_EVENT_02;

     //Trigger Events
     public const int ARL_EVENT_FIRE_TRAP_HEARTBEAT = EVENT_TYPE_CUSTOM_EVENT_01;

     //Scripts

     public const string ARL_R_GENERIC_AREA_SCRIPT = "arl000ar_generic_core.ncs";
     public const string ARL_R_FIRE_TRAP_SCRIPT = "arl101ev_fire_trap.ncs";

     //Conversations

     public const string ARL_R_CONVERSATION_ALISTAIR_INTRO = "arl100cr_alistair_origin.dlg";

     //VFX

     public const int ARL_VFX_FADE_JUMP = 1060;//1079
     public const int ARL_VFX_FADE_ACTIVE_PORTAL = 4045;
     public const int ARL_VFX_FADE_INACTIVE_PORTAL = 4046;
     public const int ARL_VFX_FIRE_TRAP_FLAMES = 1114;

     //TEMP
     public const int ARL_UNDEAD_AI_BASH_BARRICADES = 53001;

     public const int ARL_NUM_BARRICADES = 5;

     public const string ARL_VAR_UNDEAD_BARRICADE_NUMBER = "CREATURE_COUNTER_1";
}