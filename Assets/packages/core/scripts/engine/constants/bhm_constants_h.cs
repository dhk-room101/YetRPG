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
         List of constants for the Mage Origin
     */
     //==============================================================================
     //  Created By: Ferret Baudoin
     //  Created On: Oct. 18, 2006
     //==============================================================================

     //------------------------------------------------------------------------------
     // CREATURES
     //------------------------------------------------------------------------------
     public const string BHM_CR_BLOOD_MAGE_CHAT_1 = "bhm100cr_jowan_room_chat_1";
     public const string BHM_CR_BLOOD_MAGE_CHAT_2 = "bhm100cr_jowan_room_chat_2";
     public const string BHM_CR_CULLEN = "bhm300cr_cullen";
     public const string BHM_CR_DEMON = "bhm600cr_demon";             // Demon, this is the main demon baddy of the Harrowing
     public const string BHM_CR_DUNCAN = "bhm300cr_duncan";            // Duncan, the Grey Warden
     public const string BHM_CR_GREAGOIR = "bhm400cr_greagoir";          // Greagoir, the Knight-Commander
     public const string BHM_CR_GREY_WARDEN_MAGE = "bhm200_grey_warden_mage";    // Mage who talks about becoming a grey warden
     public const string BHM_CR_IRVING = "bhm400cr_irving";            // Irving, the Master Enchanter of the Circle
     public const string BHM_CR_JOWAN = "bhm100cr_jowan";             // Jowan, the PC's pal and Blood Mage
     public const string BHM_CR_LILY = "bhm200cr_lily";              // Lily, Chantry initiate and paramour of Jowan
     public const string BHM_CR_MOUSE = "bhm600cr_mouse";             // Mouse, for the Harrowing
     public const string BHM_CR_MOUSE_HUMAN = "bhm600cr_mouse_human";       // Mouse-Human, for the Harrowing
     public const string BHM_CR_MOUSE_BEAR = "bhm600cr_mouse_bear";        // Mouse-Bear, for the Harrowing
     public const string BHM_CR_SLOTH = "bhm600cr_sloth";             // Sloth Demon, for the Harrowing
     public const string BHM_CR_VALOR = "bhm600cr_valor";             // Spirit of Valor, for the Harrowing
     public const string BHM_CR_KEILI = "bhm200cr_keili";
     public const string BHM_CR_LEORAH = "bhm200cr_leorah";
     public const string BHM_CR_PLAYER_ROOM_CHAT_1 = "bhm100cr_player_room_chat_1";
     public const string BHM_CR_PLAYER_ROOM_CHAT_2 = "bhm100cr_player_room_chat_2";
     public const string BHM_CR_SENTINELS = "bhm700cr_sentinel";
     public const string BHM_CR_SPECTERS = "bhm700cr_spectre";
     public const string BHM_CR_SPIDER1 = "spider_giant1";
     public const string BHM_CR_SPIDER2 = "spider_giant2";
     public const string BHM_CR_SPIDER3 = "spider_giant3";
     public const string BHM_CR_SPIDER4 = "spider_giant4";
     public const string BHM_CR_SPIDER5 = "spider_giant5";
     public const string BHM_CR_SECOND_FLOOR_DOOR_GUARD = "bhm200cr_templar";
     public const string BHM_CR_TORRIN = "bhm200cr_senior_torrin";
     public const string BHM_CR_NIALL = "bhm200cr_niall";
     public const string BHM_CR_CLASSROOM_3_STUDENT = "bhm100cr_classroom3_stuent";
     public const string BHM_CR_CLASSROOM_3_MENTOR = "bhm100cr_mentor3";
     public const string BHM_CR_CLASSROOM_1_STUDENT = "bhm100cr_classroom1_student";
     public const string BHM_CR_CLASSROOM_1_MENTOR = "bhm100cr_mentor1";
     public const string BHM_CR_LAB_MAGE_2 = "bhm200cr_lab_mage_2";

     //------------------------------------------------------------------------------
     // WAYPOINTS
     //------------------------------------------------------------------------------
     public const string GEN_WP_LIMBO = "wp_limbo";
     public const string BHM_WP_START = "bhm400wp_start";
     public const string BHM_WP_FROM_BASEMENT = "bhm100wp_from_basement";
     public const string BHM_WP_PC_POST_HARROWING = "bhm100wp_postpc_harrowing";
     public const string BHM_WP_PC_TO_LILY = "bhm200wp_pc_hears_plan";
     public const string BHM_WP_PC_TO_FADE = "bhm600wp_pc_start";
     public const string BHM_WP_FIRST_FLOOR_FROM_SECOND = "bhm200wp_from_level_1";
     public const string BHM_WP_SECOND_FLOOR = "bhm200wp_from_level_1";
     public const string BHM_WP_FOURTH_FLOOR = "bhm400wp_from_level_3";
     public const string BHM_WP_HARROWING = "bhm400wp_start";
     public const string BHM_WP_JOWAN_CHAPEL = "bhm200wp_jowan";
     public const string BHM_WP_DUNCAN_LIBRARY = "bhm200wp_duncan_library";
     public const string BHM_WP_FADE_EXIT_MAP_PIN = "bhm600wp_exit_portal";
     public const string BHM_WP_GUEST_QUARTERS = "bhm200wp_guest_quarters";
     public const string BHM_WP_GUEST_QUARTERS2 = "bhm200wp_guest_quarters2";
     public const string BHM_WP_DUNCAN_ROOM = "bhm200wp_duncan_room";
     public const string BHM_WP_IRVING_STUDDY_HELPER = "bhm100wp_mapnote1";
     public const string BHM_WP_POST_THRID_DOOR_CUTSCENE = "bhm700wp_post_third_door";   //Player position after the third door cutscene
     public const string BHM_WP_STORAGE_CAVE_MAP_NOTE = "bhm200mn_storage_caves";
     public const string BHM_WP_CLASS1_STUD_PREFIX = "ap_bhm100cr_class1_student";
     public const string BHM_WP_CLASS1_MENTOR_PREFIX = "bhm100cr_mentor1";
     public const string BHM_WP_LILY = "bhm200wp_lily";
     public const string BHM_WP_FADE_DEMON = "bhm600mn_demon";

     //------------------------------------------------------------------------------
     // PLACEABLES
     //------------------------------------------------------------------------------
     public const string BHM_IP_EXIT_FADE = "bhm600ip_exit_fade";
     public const string BHM_IP_BOOKCASE_1 = "bhm700ip_bookcase_1";
     public const string BHM_IP_BOOKCASE_2 = "bhm700ip_bookcase_2";
     public const string BHM_IP_DOOR_FIRST_TO_BASE = "bhm100ip_to_basement";
     public const string BHM_IP_DOOR_BASE_SECONDDOOR = "bhm700ip_seconddoor";
     public const string BHM_IP_DOOR_BASE_FIRSTDOOR = "bhm700_ip_first_door";
     public const string BHM_IP_BRICK_WALL = "bhm700ip_brick_wall";
     public const string BHM_IP_CANNON = "bhm700ip_cannon";
     public const string BHM_IP_FADE_BLOOD_BOOKS = "bhm600ip_book_blood";
     public const string BHM_IP_FADE_EXIT = "bhm600ip_exit_fade.uti";
     public const string BHM_IP_SIDE_DOOR = "bhm700ip_sidedoor";
     public const string BHM_FIRE_BASE = "bhm100ip_fire_base";         //The student who is trying to make fire.
     public const string BHM_PHYLACTERY = "bhm700ip_phylactery";
     public const string BHM_STATUE = "bhm700ip_statue";
     public const string BHM_BC_BLOOD_MAGIC = "bhm200ip_bc_school_blood";
     public const string BHM_BOOK_PILE_BLOOD_MAGIC = "bhm200ip_pile_school_blood";

     //------------------------------------------------------------------------------
     // AREAS
     //------------------------------------------------------------------------------
     public const string BHM_AR_FIRST_FLOOR = "bhm100ar_tower_level_1";
     public const string BHM_AR_SECOND_FLOOR = "bhm200ar_tower_level_2";
     public const string BHM_AR_THIRD_FLOOR = "bhm100ar_tower_level_3";
     public const string BHM_AR_FOURTH_FLOOR = "bhm400ar_tower_level_4";
     public const string BHM_AR_TOWER_HARROWING = "bhm500ar_tower_harrowing";
     public const string BHM_AR_FADE = "bhm600ar_fade_harrowing";
     public const string BHM_AR_BASEMENT = "bhm700ar_tower_basement";
     public const string BHM_AR_SPIDER_CAVE = "bhm250ar_spider_cave";

     //------------------------------------------------------------------------------
     // CONVERSATIONS
     //------------------------------------------------------------------------------
     public const string BHM_DG_BLOOD_MAGIC_CUTSCENE = "bhm100_cutscene_blood.dlg";
     public const string BHM_DG_HARROWING_CUTSCENE = "bhm500_cutscene_harrow.dlg";
     public const string BHM_DG_CANNON = "bhm700_cannon.dlg";
     public const string BHM_DG_BOOKCASE = "bhm700_bookcase.dlg";
     public const string BHM_DG_PLAYER_ROOM_AMBIENT = "bhm100_harrowing_chat.dlg";
     public const string BHM_DG_PHYLACTERY = "bhm700_phylacteryj.dlg";

     //------------------------------------------------------------------------------
     // PLOTS
     //------------------------------------------------------------------------------
     public const string BHM_PL_TRANQUILITY = "bhm000pt_tranquility";

     //------------------------------------------------------------------------------
     // ITEMS
     //------------------------------------------------------------------------------
     public const string BHM_IM_ROD_OF_FIRE = "bhm200im_rodoffire.uti";
     public const string BHM_IM_MAGE_ROBES = "gen_im_cth_mag_mag.uti";
     public const string BHM_IM_VALOR_SWORD = "bhm600im_valor_sword.uti";
     public const string BHM_IM_UNSIGNED_FORM = "bhm000im_unsigned_form.uti";
     public const string BHM_IM_SIGNED_FORM = "bhm000im_signed_form.uti";
     public const string BHM_IM_CAVE_KEY = "bhm200ip_laboratory_key.uti";
     public const string BHM_IM_FADE_HEALING_SALVE = "bhm600im_fade_healing_salve.uti";
     public const string BHM_IM_FADE_DAGGER = "gen_im_wep_mel_dag_dag.uti";
     public const string BHM_IM_MAGE_STAFF = "gen_im_wep_mag_sta_aco.uti";
     public const string BHM_IM_STOLEN_STAFF = "gen_im_wep_mag_sta_bhs.uti";    //The staff that the PC can steal (which is used in a dialogue).

     //------------------------------------------------------------------------------
     // TEAMS
     //------------------------------------------------------------------------------
     public const int BHM_TEAM_SPIDERS = 1;
     public const int BHM_TEAM_FADE_DEMON = 2;
     public const int BHM_TEAM_WISPS = 3;
     public const int BHM_TEAM_SPIRIT_WOLF_1 = 4;
     public const int BHM_TEAM_SPIRIT_WOLF_2 = 5;
     public const int BHM_TEAM_SPIRIT_WOLF_3 = 6;
     public const int BHM_TEAM_BASEMENT_PERSON = 7;
     public const int BHM_TEAM_BASEMENT_SPECTRE = 8;
     public const int BHM_TEAM_SPIRIT_WOLF_4 = 9;
     public const int BHM_TEAM_WISP_BACKUP = 10;
     public const int BHM_TEAM_BASEMENT_BOSS = 11;
     public const int BHM_TEAM_WISP_BEAR = 12;
     public const int BHM_TEAM_WISP_VALOR = 13;

     //------------------------------------------------------------------------------
     // VFX
     //------------------------------------------------------------------------------
     public const int BHM_VFX_SPIRIT_WOLF_APPEAR = 1134;
     public const int BHM_VFX_STATUE_CRUST = 90147;

     //------------------------------------------------------------------------------
     // AUDIO
     //------------------------------------------------------------------------------
     public const int BHM_AUDIO_TOGGLE_SPIDERS_DIED = 48;
}