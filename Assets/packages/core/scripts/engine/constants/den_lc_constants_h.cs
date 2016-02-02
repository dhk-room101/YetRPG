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
     //#include"events_h"

     //public void main() {}

     //------------------------------------------------------------------------------
     // PLACEABLES
     //------------------------------------------------------------------------------
     public const string DEN_IP_LC_DOOR_TO_ORPHANAGE = "den300ip_to_orphanage";
     public const string DEN_IP_LC_DOOR_TO_BLOOD_MAGE = "den300ip_to_blood_mage";
     public const string DEN_IP_LC_PAEDAN_DOOR = "den100ip_back_room";
     public const string DEN_IP_LC_HOWE_SILVER = "den260ip_howe_silver";
     public const string DEN_IP_ASSASSIN_CHEST = "den220ip_assassin_chest";
     public const string DEN_IP_BANN_SECRET_DOOR = "den970ip_secret_door";
     public const string DEN_IP_CHANTERS_BOARD = "den200ip_chanter_board";
     public const string DEN_IP_FRAN_INNER_DOOR = "den970ip_shut_door";
     public const string DEN_IP_FRAN_COURTYARD_DOOR = "den970ip_captain_door_2";
     public const string DEN_IP_FRAN_SECRET_DOOR = "den970ip_actual_secret_door";
     public const string DEN_IP_FRAN_SECRET_DOOR_BOOKCASE = "den970ip_secret_door";
     public const string DEN_IP_FRAN_SECRET_DOOR_BOOKCASE_2 = "den970ip_secret_door_2";
     public const string DEN_IP_DEMON_DOOR_AMBUSH = "den960ip_door_demon";
     public const string DEN_IP_DEMON1_DOOR = "den960ip_demon1_door";
     public const string DEN_IP_DEMON2_DOOR = "den960ip_demon2_door";
     public const string DEN_IP_WICKED_DEAD_DOG = "den300ip_dead_dog";
     public const string DEN_IP_WICKED_BLOOD_POOL = "den300ip_otto_blood";
     public const string DEN_IP_PEARL_BACKROOM_DOOR = "den100ip_backroom";
     public const string DEN_IP_GNAWED_SIDE_DOOR = "den220ip_side_room_door";
     public const string DEN_IP_GNAWED_IGNACIO_DOOR = "den220ip_ignacio_door";
     public const string DEN_IP_GNAWED_SOPHIE_DOOR = "den220ip_sophie_door";
     public const string DEN_IP_PAEDAN_POSTER = "den200ip_paedan_poster";
     public const string DEN_IP_CESARS_SECOND_STORE = "store_den200cr_cesar_2";
     public const string DEN_IP_IGNACIO_TRAP = "den220ip_hidden_trap";
     public const string DEN_IP_GOLEM_TRAPS = "den971tr_golem_trap";
     public const string DEN_IP_ORPHANAGE_BOSS_TRAPS = "den960ip_vert_fire_trap";
     public const string DEN_IP_ORPHANAGE_FIRE_BASE = "den960ip_fire_base";

     //------------------------------------------------------------------------------
     // AREAS
     //------------------------------------------------------------------------------
     public const string DEN_AR_BLOOD_MAGE_HQ = "den961ar_blood_mage";
     public const string DEN_AR_FALCON_ATTACK = "den962ar_falcon_attack";
     public const string DEN_AR_LANDRY_ATTACK = "den963ar_landry_attack";
     public const string DEN_AR_FRANDEREL_ESTATE = "den970ar_franderel_estate";
     public const string DEN_AR_FRANDEREL_ESTATE_2 = "den971ar_franderel_estate_2";
     public const string DEN_AR_WONDERS_OF_THEDAS = "den230ar_wonders_of_thedas";
     public const string DEN_AR_ORPHANAGE = "den960ar_orphanage";
     public const string DEN_AR_LITE_ROGUE_AMBUSH = "den976ar_rogue_random_enc";
     public const string DEN_AR_LITE_ROGUE_HIDEOUT_D = "den975ar_rogue_d";
     public const string DEN_AR_LITE_ROGUE_HIDEOUT_K = "den974ar_rogue_k";

     //------------------------------------------------------------------------------
     // CREATURES
     //------------------------------------------------------------------------------
     public const string DEN_CR_LC_DEMON_ORPHAN = "den960cr_demon_orphanage";
     public const string DEN_CR_LC_DEMON_ROW_HOUSE = "den960cr_demon_row_house";
     public const string DEN_CR_LC_DEMON_ROW_HOUSEx2 = "den960cr_demon_row_house_2";
     public const string DEN_CR_LC_OTTO = "den300cr_otto";
     public const string DEN_CR_WHITE_FALCON_LEADER = "den100cr_white_falcon_lead";
     public const string DEN_CR_KYLON = "den200cr_kylon";
     public const string DEN_CR_SER_LANDRY = "den200cr_ser_landry";
     public const string DEN_CR_SER_LANDRY_SECOND = "den200cr_ser_landry_2nd";
     public const string DEN_CR_SER_LANDRY_SEC_ARCHER = "den200cr_ser_landry_2nd_bow";
     public const string DEN_CR_CESAR = "den200cr_cesar";
     public const string DEN_CR_IGNACIO = "den200cr_ignacio";
     public const string DEN_CR_IGNACIO_DELIVERY = "den200cr_ignacio_delivery";
     public const string DEN_CR_COULDRY = "den200cr_couldry";
     public const string DEN_CR_CHANTER = "den200cr_chanter_denerim";
     public const string DEN_CR_SOPHIE_GUARD = "den220cr_sophie_guard";
     public const string DEN_CR_PICK1_MAID = "den200cr_pick1_maid";
     public const string DEN_CR_PICK2_NANCINE = "den230cr_pick2_nancine";
     public const string DEN_CR_PICK3_SILVERSMITH = "den200cr_pick3_silversmith";
     public const string DEN_CR_PICK3_GUARD1 = "den200cr_pick3_silver_guard";
     public const string DEN_CR_PICK3_GUARD2 = "den200cr_pick3_silver_grd_2";
     public const string DEN_CR_PICK4_SENESCHAL = "den220cr_pick4_seneshal";
     public const string DEN_CR_SNEAK4_DRINK_SERVER = "den971cr_drink_server";
     public const string DEN_CR_SNEAK4_GOSSIP_1 = "den971cr_gossip_guard_1";
     public const string DEN_CR_SNEAK4_GOSSIP_2 = "den971cr_gossip_guard_2";
     public const string DEN_CR_SNEAK4_INNER_GOSSIP_1 = "den971cr_inner_gossip_1";
     public const string DEN_CR_SNEAK4_INNER_GOSSIP_2 = "den971cr_inner_gossip_2";
     public const string DEN_CR_GHOST_CHILD = "den960cr_ghost_child";
     public const string DEN_CR_HERREN = "nrd_dencr_herren";               // Herren, clerk at Wade's Emporium (drake scale)
     public const string DEN_CR_DISGUISED_CROW = "den963cr_disguised_crow";
     public const string DEN_CR_SANGA = "den100cr_sanga";
     public const string DEN_CR_EDWINA = "den220cr_edwina";
     public const string DEN_CR_CRIMSON_OAR_LEADER = "den220cr_crimsons_oars_lead";
     public const string DEN_CR_SER_FRIDEN = "den952cr_ser_friden_dead";
     public const string DEN_CR_BLOOD_MAGE_AMBUSH_1 = "den961cr_blood_mage_ambush_1";
     public const string DEN_CR_FRAN_PATROL_1_1 = "den970cr_fran_patrol_1_1";
     public const string DEN_CR_FRAN_PATROL_1_2 = "den970cr_fran_patrol_1_2";
     public const string DEN_CR_FRAN_PATROL_2 = "den970cr_fran_patrol_2";
     public const string DEN_CR_FRAN_PATROL_3_1 = "den970cr_fran_patrol_3_1";
     public const string DEN_CR_FRAN_PATROL_3_2 = "den970cr_fran_patrol_3_2";
     public const string DEN_CR_FRAN_PATROL_3_3 = "den970cr_fran_patrol_3_3";
     public const string DEN_CR_FRAN_PATROL_3_4 = "den970cr_fran_patrol_3_4";
     public const string DEN_CR_BLOOD_MAGE_2 = "den961cr_blood_mage_2";
     public const string DEN_CR_OTTO_BEGGAR = "den300cr_otto_beggar";
     public const string DEN_CR_STARVED_VETERAN = "den300cr_beggar_veteran";

     //------------------------------------------------------------------------------
     // TEAMS
     //------------------------------------------------------------------------------

     // Light Content
     public const int DEN_TEAM_ALLEY1_THUGS = 900;
     public const int DEN_TEAM_ALLEY2_THUGS = 901;
     public const int DEN_TEAM_ALLEY3_THUGS = 902;
     public const int DEN_TEAM_ALIENAGE_BEGGAR_2 = 903;
     public const int DEN_TEAM_ALIENAGE_BEGGAR_3 = 904;
     public const int DEN_TEAM_ORPHANAGE_DEMON = 905;
     public const int DEN_TEAM_ROW_HOUSE_DEMON = 906;
     public const int DEN_TEAM_ROW_HOUSE_DEMONx2 = 907;
     public const int DEN_TEAM_BLOOD_MAGE_FIRST = 908;
     public const int DEN_TEAM_BLOOD_MAGE_SECOND = 909;
     public const int DEN_TEAM_BLOOD_MAGE_LAST = 910;
     public const int DEN_TEAM_WHITE_FALCON_1 = 911;
     public const int DEN_TEAM_WHITE_FALCON_2 = 912;
     public const int DEN_TEAM_ALLEY_KYLON = 913;
     public const int DEN_TEAM_CRIMSON_OARS = 914;
     public const int DEN_TEAM_SER_LANDRY = 915;
     public const int DEN_TEAM_SER_LANDRY_SECONDS = 916;
     public const int DEN_TEAM_IGNACIO = 917;
     public const int DEN_TEAM_PAEDAN = 918;
     public const int DEN_TEAM_CHASE = 919;
     public const int DEN_TEAM_CHASE_CROW_ALLIES = 920;
     public const int DEN_TEAM_HOWES_SILVER = 921;
     public const int DEN_TEAM_RUBBLE_DEMON = 922;
     public const int DEN_TEAM_HOWE_SILVER_GUARD = 923;
     public const int DEN_TEAM_PICK3_SILVERSMITH = 924;
     public const int DEN_TEAM_FRANDEREL_FIRST_AMBUSH = 925;
     public const int DEN_TEAM_FRANDEREL_SECOND = 926;
     public const int DEN_TEAM_PICK4_SENESHAL = 927;
     public const int DEN_TEAM_OTTO = 928;
     public const int DEN_TEAM_DEN_ASSASSIN_PADAN_FE = 929;
     public const int DEN_TEAM_SNEAK1_SOPHIE_GUARD = 930;
     public const int DEN_TEAM_PEARL_BACK_DOORS = 931;
     public const int DEN_TEAM_FRAN_REINFORCE_DOORS = 932;
     public const int DEN_TEAM_FRAN_REINFORCEMENTS = 933;
     public const int DEN_TEAM_FRAN_GOLEMS = 934;
     public const int DEN_TEAM_FRAN_LAST_WAVE = 935;
     public const int DEN_TEAM_QUNARI_CAMP_PLACEABLES = 936;
     public const int DEN_TEAM_MARJOLAINE = 937;
     public const int DEN_TEAM_TRAP_PLACEABLES = 938;
     public const int DEN_TEAM_FIRE_MAGE_AMBUSH = 939;
     public const int DEN_TEAM_BANN_FRANDRERAL_TRAPS_1 = 940;
     public const int DEN_TEAM_BANN_FIRST_AMBUSH_2 = 941;
     public const int DEN_TEAM_RABID_WARDOG_AMBUSH = 942;
     public const int DEN_TEAM_DEMON_REINFORCE_1 = 943;
     public const int DEN_TEAM_DEMON_REINFORCE_2 = 944;
     public const int DEN_TEAM_DEMON_REINFORCE_3 = 945;
     public const int DEN_TEAM_ORPHANAGE_LAST_BOSS_TRAPS = 946;
     public const int DEN_TEAM_BACK_ROOM_DEMONS = 950;
     public const int DEN_TEAM_ORPHANAGE_LAST_BOSS_1 = 951;
     public const int DEN_TEAM_ORPHANAGE_LAST_BOSS_2 = 952;
     public const int DEN_TEAM_BACK_ROOMS_SHADES = 953;
     public const int DEN_TEAM_ROGUE_TERMS_REVENGE = 954;
     public const int __NO_LONGER_USED_955 = 955;
     public const int __NO_LONGER_USED_956 = 956;
     public const int DEN_TEAM_ROGUE_WITNESS_1 = 957;
     public const int DEN_TEAM_ROGUE_WITNESS_2 = 958;
     public const int DEN_TEAM_ROGUE_WITNESS_3 = 959;
     public const int DEN_TEAM_BLOOD_MAGE_FLAME_TRAP_AMBUSH = 960;
     public const int DEN_TEAM_BLOOD_MAGE_FLAME_TRAP_DOGS = 961;
     public const int DEN_TEAM_ROGUE_K = 962;
     public const int DEN_TEAM_ROGUE_K_LIEUT = 963;
     public const int DEN_TEAM_ROGUE_D = 964;
     public const int DEN_TEAM_LANDRY_MARKET_PATRONS = 965;
     public const int DEN_TEAM_PROPAGANDIST = 966;
     public const int DEN_TEAM_OTTO_CLUES = 967;

     //------------------------------------------------------------------------------
     // WAYPOINTS
     //------------------------------------------------------------------------------
     public const string DEN_WP_LC_TO_ORPHANAGE = "den300wp_from_orphanage";
     public const string DEN_WP_FRANDEREL_ESTATE_2 = "wmw_lc_franderel_estate_2";
     public const string DEN_WP_FALCON_ATTACK = "den962wp_start";
     public const string DEN_WP_LANDRY_ATTACK = "start";
     public const string DEN_WP_SNEAK4_DRINK_SERVER_1 = "mp_den971cr_drink_server_1";
     public const string DEN_WP_SNEAK4_DRINK_SERVER_2 = "mp_den971cr_drink_server_2";
     public const string DEN_WP_BLOOD_AMBUSH_1_TARGET = "den961wp_ambush_1_target";
     public const string DEN_WP_BLOOD_MAGE_GREASE = "den961wp_grease_traps";
     public const string DEN_WP_ORPHAN_DEMON_BOSS = "den960mn_demon_2";
     public const string DEN_WP_SMITHY_FROM_MARKET = "den280wp_from_market";
     public const string DEN_WP_PARTY_MEMBER_1 = "den200wp_party_member_1";
     public const string DEN_WP_PARTY_MEMBER_2 = "den200wp_party_member_2";
     public const string DEN_WP_PARTY_MEMBER_3 = "den200wp_party_member_3";
     public const string DEN_WP_ORPHANAGE_WARDOG_MP = "mp_den960cr_rabid_wardog_0";

     //------------------------------------------------------------------------------
     // TRIGGERS
     //------------------------------------------------------------------------------
     public const string DEN_TR_DELIVERY_BOY_1 = "den200tr_messenger_speaks_1";
     public const string DEN_TR_DELIVERY_BOY_2 = "den200tr_messenger_speaks_2";
     public const string DEN_TR_DELIVERY_BOY_3 = "den200tr_messenger_speaks_3";
     public const string DEN_TR_MESSENGER = "den200tr_messenger_speaks";
     public const string DEN_TR_PICK3_GUARD2_TIMER = "den200tr_pick3_guard2_timer";

     //------------------------------------------------------------------------------
     // ITEMS
     //------------------------------------------------------------------------------
     //public const string  DEN_IM_CLERIC_ELFROOT         = "zz_den700im_elfroot.uti";
     public const string DEN_IM_BEGGAR_AMULET = "den300im_beggar_amulet.uti";
     public const string DEN_IT_BEGGAR_AMULET = "den300im_beggar_amulet";
     public const string DEN_IM_OTTO_JOURNAL = "den300im_otto_journal.uti";
     public const string DEN_IT_OTTO_JOURNAL = "den300im_otto_journal";
     public const string DEN_IM_TEARS_OF_ANDRASTE = "den970im_tears_andraste.uti";
     public const string DEN_IT_TEARS_OF_ANDRASTE = "den970im_tears_andraste";

     public const string DEN_IM_ASSASSIN_INTRO = "den200im_assassin_letter.uti";
     public const string DEN_IT_ASSASSIN_INTRO = "den200im_assassin_letter";
     public const string DEN_IM_ASSASSIN_CONTRACT_DEN = "den200im_contract_paedan.uti";
     public const string DEN_IT_ASSASSIN_CONTRACT_DEN = "den200im_contract_paedan";
     public const string DEN_IM_ASSASSIN_CONTRACT_ORZ = "den200im_contract_gainley.uti";
     public const string DEN_IT_ASSASSIN_CONTRACT_ORZ = "den200im_contract_gainley";
     public const string DEN_IM_ASSASSIN_CONTRACT_NRD = "den200im_contract_kadanfe.uti";
     public const string DEN_IT_ASSASSIN_CONTRACT_NRD = "den200im_contract_kadanfe";
     public const string DEN_IM_ASSASSIN_CONTRACT_END = "den200im_contract_ransom.uti";
     public const string DEN_IT_ASSASSIN_CONTRACT_END = "den200im_contract_ransom";
     public const string DEN_IM_FRAN_RAT_POISON = "gen_im_qck_poison_102.uti";
     public const string DEN_IT_FRAN_RAT_POISON = "gen_im_qck_poison_102";
     public const string DEN_IM_DRAKE_SCALE = "gen_im_misc_drakescale.uti";
     public const string DEN_IT_DRAKE_SCALE = "gen_im_misc_drakescale";
     public const string DEN_IM_DRAGON_SCALE = "gen_im_misc_dragonscale.uti";
     public const string DEN_IT_DRAGON_SCALE = "gen_im_misc_dragonscale";
     public const string DEN_IM_PICK1_PURSE = "den200im_pick1_gem_purse.uti";
     public const string DEN_IT_PICK1_PURSE = "den200im_pick1_gem_purse";
     public const string DEN_IM_PICK2_SWORD = "den200im_pick2_sword_orn.uti";
     public const string DEN_IT_PICK2_SWORD = "den200im_pick2_sword_orn";
     public const string DEN_IM_PICK3_KEY = "den200im_pick3_silver_key.uti";
     public const string DEN_IT_PICK3_KEY = "den200im_pick3_silver_key";
     public const string DEN_IM_PICK4_CROWN = "den200im_pick4_loghain_crwn.uti";
     public const string DEN_IT_PICK4_CROWN = "den200im_pick4_loghain_crwn";
     public const string DEN_IT_NANCINE_GOWN = "gen_im_cth_nob_c01";
     public const string DEN_IM_DRAKE_ARMOR_MK1 = "gen_im_arm_cht_lgt_drw.uti";
     public const string DEN_IT_DRAKE_ARMOR_MK1 = "gen_im_arm_cht_lgt_drw";
     public const string DEN_IT_DRAKE_ARMOR_MK1_BOOTS = "gen_im_arm_bot_lgt_drw.uti";
     public const string DEN_IT_DRAKE_ARMOR_MK1_GLOVES = "gen_im_arm_glv_lgt_drw.uti";
     public const string DEN_IM_DRAKE_ARMOR_MK2 = "den000it_drake_scale_mk2.uti";
     public const string DEN_IT_DRAKE_ARMOR_MK2 = "den000it_drake_scale_mk2";
     public const string DEN_IM_LANDRY_NOTE = "lite_sod_propaganda_page.uti";
     public const string DEN_IT_LANDRY_NOTE = "lite_sod_propaganda_page";

     // Rewards for the Assassin Quests
     public const string DEN_IM_ASSASSIN_NRD = "gen_im_arm_glv_crt1.uti";
     public const string DEN_IT_ASSASSIN_NRD = "gen_im_arm_glv_crt1";
     public const string DEN_IM_ASSASSIN_ORZ = "gen_im_wep_rng_cbw_ap1.uti";
     public const string DEN_IT_ASSASSIN_ORZ = "gen_im_wep_rng_cbw_ap1";
     public const string DEN_IM_ASSASSIN_END = "gen_im_arm_glv_bsb3.uti";
     public const string DEN_IT_ASSASSIN_END = "gen_im_arm_glv_bsb3";

     // Crow Daggers
     public const string DEN_IT_CROW_DAGGER = "gen_im_wep_mel_dag_crw";

     //------------------------------------------------------------------------------
     // EVENTS
     //------------------------------------------------------------------------------
     public const int EVENT_TYPE_PICK3_WANDER = EVENT_TYPE_CUSTOM_EVENT_01;

     //------------------------------------------------------------------------------
     // SCRIPTS
     //------------------------------------------------------------------------------

     //public const string DEN_SCRIPT_GUARD_OUTER_AOE     = "den510cr_guard_outer_aoe.ncs";

     //------------------------------------------------------------------------------
     // CONVERSATIONS
     //------------------------------------------------------------------------------
     public const string DEN_CONV_SNEAK4_GUARD = "den971_generic_guard.dlg";
     public const string DEN_CONV_SNEAK4_GOSSIP = "den971_gossip_guards.dlg";

     //------------------------------------------------------------------------------
     // DISTANCES
     //------------------------------------------------------------------------------

     //------------------------------------------------------------------------------
     // ANIMATIONS
     //------------------------------------------------------------------------------
     //public const int     DEN_ANIM_SNATCH_PURSE               = 840;

     //------------------------------------------------------------------------------
     //  AUDIO EVENTS
     //------------------------------------------------------------------------------
     public const int DEN_AUDIO_ORPHANAGE = 16;
}