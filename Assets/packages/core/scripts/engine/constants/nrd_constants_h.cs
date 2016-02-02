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
         nrd_constants_h.nss
         List of constants for the Lake Calenhad/North Road plots.
     */
     //==============================================================================
     // Created By: Ferret Baudoin
     // Created On: Jan. 10, 2007
     //==============================================================================
     // Modified By: Kaelin
     //==============================================================================

     //public void main() {}

     //------------------------------------------------------------------------------
     // CREATURES
     //------------------------------------------------------------------------------

     // Major Characters
     public const string NRD_CR_GEVERE = "nrd100cr_gevere";                 // Ser Gevere, one of Arl Eamon's knights
     public const string NRD_CR_LIAN = "nrd101cr_lian";                     // Lian, depravati mage on the run
     public const string NRD_CR_NAB = "nrd103cr_nab";                       // Sergeant Nab, gives the Crow assassin quest
     public const string NRD_CR_NAB_2 = "nrd103cr_nab2";                    // Sergeant Nab, his clone to set active later
     public const string NRD_CR_PHERESON = "nrd100cr_phereson";             // Captain Phereson, leader of Junction Tower
     public const string NRD_CR_QUARTERMASTER = "nrd100cr_quartermaster";   // Quartermaster, at Junction Tower
     public const string NRD_CR_ROTHEL = "nrd100cr_rothel";                 // Rothel, works for Bann Tagger
     public const string NRD_CR_TRESSA = "nrd101cr_tressa";                 // Ser Tressa, templar tracking down Lian
     public const string NRD_CR_VIKKA = "nrd105cr_vikka";                   // Vikka, merchant that needs escort
     public const string NRD_CR_KASTIGIR = "nrd140cr_kastigir";             // Kastigir, the bandit leader

     // Minor Characters
     public const string NRD_CR_BANDIT_ASSASSIN = "nrd100cr_bandit_assassin"; // Bandit Assassin, sent after you've cleared a camp
     public const string NRD_CR_BARRIC = "nrd110cr_barric";                 // Brother Barric, gives the Divine Collection quest
     public const string NRD_CR_CROW_PRISONER = "nrd103cr_crow_prisoner";   // Crow Prisoner, assassin that needs transport
     public const string NRD_CR_DEJECTED_SOLDIER = "nrd102cr_log4_badend";  // Dejected Soldier (Log4), the tower is gone
     public const string NRD_CR_GEVERE_BANDIT = "nrd100cr_gev_cultist";     // Gevere's Cultist, Cult of the Prophetess assassin
     public const string NRD_CR_GOLEM = "nrd100cr_golem";                   // Golem, guarding body of his old master
     public const string NRD_CR_HERREN = "nrd_dencr_herren";                // Herren, clerk at Wade's Emporium (drake scale)
     public const string NRD_CR_INSTRUCTOR = "nrd140cr_bandit_instructor";  // Bandit Instructor, drill sergeant
     public const string NRD_CR_INST_BANDIT1 = "nrd140cr_bandit_student1";  // Student 1, of the drill sergeant
     public const string NRD_CR_INST_BANDIT2 = "nrd140cr_bandit_student2";  // Student 2, of the drill sergeant
     public const string NRD_CR_INTRO_SOLDIER = "nrd100cr_phere_soldier";   // Phereson Soldier, used in introing Phereson
     public const string NRD_CR_INTRO_MESSENGER = "nrd100cr_phere_messenger"; // Phereson Messenger, used in introing Phereson
     public const string NRD_CR_LISELLE_BRO = "nrd_dencr_liselle_bro";      // Liselle's brother, part of a Dead Man's Tale
     public const string NRD_CR_JUNCTION_SCOUT = "nrd100cr_tower_scout";    // Junction Scout, after the towers are cleared
     public const string NRD_CR_JUNCTION_SOLDIER = "nrd100cr_tower_soldier";  // Junction Soldier
     public const string NRD_CR_JUNCTION_ARCHER = "nrd100cr_tower_archer";  // Junction Tower Archer.
     public const string NRD_CR_LOGHAIN_PORTER = "nrd102cr_log3_porter";    // Loghain Porter (Log3), simple porter working for the wrong side
     public const string NRD_CR_MERCHANT_PRISONER = "nrd100cr_bandit_prisoner";     // Merchant, prisoner being held in bandit camp
     public const string NRD_CR_NAB_SCOUT = "nrd103cr_forward_scout";       // Scout, terse scout for Crows March
     public const string NRD_CR_REFUGEE = "nrd100cr_refugee";               // Refugee, about to be attacked
     public const string NRD_CR_REFUGEE2 = "nrd100cr_refugee2";             // Refugee, about to be attacked
     public const string NRD_CR_REFUGEE3 = "nrd100cr_refugee3";             // Refugee, about to be attacked
     public const string NRD_CR_REFUGEE_BANDIT = "nrd100cr_bandit_tough";   // Bandit, who is going to attack refugees
     public const string NRD_CR_REFUGEE_LEADER = "nrd100cr_refugee_leader"; // Refugee Leader, who is going to attack refugees
     public const string NRD_CR_ROTHELS_MAN = "nrd100cr_rothserv";          // Rothel's Man at Arms, works for Bann Tagger
     public const string NRD_CR_VIKKA_PORTER1 = "nrd105cr_vikka_porter1";   // Vikka's Elven porter
     public const string NRD_CR_VIKKA_PORTER2 = "nrd105cr_vikka_porter2";   // Vikka's Elven porter
     public const string NRD_CR_KASTIGIR_GUARD = "nrd140cr_kastigir_guard"; // Kastigir the bandit leader's guards
     public const string NRD_CR_SURRENDER_BANDIT = "nrd130cr_surr_bandit";  // Nervous bandit that surrenders
     public const string NRD_CR_BLACK_HUNTER = "nrd104cr_black_hunter";    // The Black Hunter...grrr. 
     public const string NRD_CR_BRANDON = "nrd120cr_brandon";               // The quest giver that starts off the Loghain invasion missions.

     // Enemy Leaders for making people attack right
     public const string NRD_CR_VIKKA_FIRST_AMBUSH_NLEAD = "ld_nrd100cr_bandit_t15";

     // Tower Scouts for Loghain 1
     public const string NRD_CR_TOWER_PATROL_1 = "nrd102cr_tow_patrol_1";
     public const string NRD_CR_TOWER_PATROL_2 = "nrd102cr_tow_patrol_2";
     public const string NRD_CR_TOWER_PATROL_3 = "nrd102cr_tow_patrol_3";
     public const string NRD_CR_TOWER_PATROL_4 = "nrd102cr_tow_patrol_4";

     // Rothel Assassins, still using a string for them because they are spawned in
     public const string NRD_CR_ROTHEL_ASSASSIN = "nrd100cr_rothass";
     public const string NRD_CR_ROTHEL_ASSASSIN2 = "nrd100cr_rothass_2";
     public const string NRD_CR_ROTHEL_ASSASSIN3 = "nrd100cr_rothass_3";
     public const string NRD_CR_ROTHEL_ASSASSIN4 = "nrd100cr_rothass_4";

     //------------------------------------------------------------------------------
     // TEAMS
     //------------------------------------------------------------------------------
     public const int NRD_TEAM_REFUGEE_BANDITS = 11;
     public const int NRD_TEAM_LIAN = 12;
     public const int NRD_TEAM_TRESSA = 13;
     public const int NRD_TEAM_GEVERE_BANDITS = 14;
     public const int NRD_TEAM_VIKKA_AMBUSH_1 = 15;
     public const int NRD_TEAM_VIKKA_AMBUSH_2 = 16;
     public const int NRD_TEAM_SOUTH_TOWER_BANDITS_FLOOR_1 = 17;
     public const int NRD_TEAM_GEVERE = 18;
     public const int NRD_TEAM_WEST_TOWER_BANDITS_FLOOR_1 = 20;
     public const int NRD_TEAM_WEST_TOWER_UNDEAD_1 = 21;
     public const int NRD_TEAM_TAGGER_ASSASSINS = 23;
     public const int NRD_TEAM_CROW_FIRST_ENCOUNTER = 24;
     public const int NRD_TEAM_CROW_SECOND_ENCOUNTER = 25;
     public const int NRD_TEAM_CROW_FINAL_BAD_GUYS = 26;
     public const int NRD_TEAM_CROW_FINAL_GOOD_GUYS = 27;
     public const int NRD_TEAM_NE_BANDITS = 28;
     public const int NRD_TEAM_NW_BANDITS = 29;
     public const int NRD_TEAM_SECOND_CAVE_BANDITS = 30;
     public const int NRD_TEAM_MAIN_CAVE_BANDITS = 31;
     public const int NRD_TEAM_NEAR_WEST_HILL_GUARD = 32;
     public const int NRD_TEAM_FAR_WEST_HILL_GUARD = 33;
     public const int NRD_TEAM_SOUTH_HILL_GUARD = 34;
     public const int NRD_TEAM_SOUTHEAST_HILL_GUARD = 35;
     public const int NRD_TEAM_LOG1_TOWER_PATROL = 36;
     public const int NRD_TEAM_LOG1_LOGHAIN_SCOUT = 37;
     public const int NRD_TEAM_LOG2_LOGHAIN_SCOUT = 38;
     public const int NRD_TEAM_LOG3_LOGHAIN_PORTER = 39;
     public const int NRD_TEAM_LOG3_LOGHAIN_ESCORT = 40;
     public const int NRD_TEAM_JUNCTION = 41;
     public const int NRD_TEAM_LOG4_LOGHAIN_INVASION_N = 42;
     public const int NRD_TEAM_KASTIGIRS_ROOM = 43;
     public const int NRD_TEAM_LOG4_LOGHAIN_INVASION_E = 44;
     public const int NRD_TEAM_LOG4_LOGHAIN_INVASION_W = 45;
     public const int NRD_TEAM_TOWER_SCOUTS = 46;
     public const int NRD_TEAM_RESTLESS_DEAD = 47;
     public const int NRD_TEAM_EXORCIST = 48;
     public const int NRD_TEAM_BLACK_HUNTER = 49;
     public const int NRD_TEAM_BLACK_MINIONS = 50;
     public const int NRD_TEAM_VIKKA = 51;
     public const int NRD_TEAM_LIANS_DEMONS = 52;
     public const int NRD_TEAM_KASTIGIRS_CAVE = 53;
     public const int NRD_TEAM_JUNCTION_CIVILIANS = 54;

     public const int NRD_TEAM_DEN_ASSASSIN_PADAN_FE = 900;     // This is used for the Denerim Assassination missions
     public const int NRD_TEAM_URN_CULTISTS = 901;     // For the Urn Cultist ambush.

     //------------------------------------------------------------------------------
     // WAYPOINTS
     //------------------------------------------------------------------------------
     //public const string URN_WP_DUNCAN_IN_FADE = "cir300wp_pc_to_duncan";   // The PC goes here first in the Fade - in the Weisshaupt

     public const string NRD_WP_REFUGEE_WALK = "nrd100wp_refugee_walk_point";       // This is where the refugees first walk to
     public const string NRD_WP_JUNCTION_MIDDLE = "nrd100wp_junction_middle";
     public const string NRD_WP_NAB_CONFRONT = "nrd100wp_nab_infamy_confront";
     public const string NRD_WP_VIKKA_CENTER_1 = "mp_nrd105cr_vikka_0";
     public const string NRD_WP_VIKKA_CENTER_2 = "mp_nrd105cr_vikka_1";

     //------------------------------------------------------------------------------
     // MAP NOTES
     //------------------------------------------------------------------------------

     public const string NRD_MN_SECOND_CAMP = "mn_nrd100wp_secondcamp";
     public const string NRD_MN_ASSASSIN_NOTE = "nrd100mn_qunari";      // Used for the Denerim Assassination mission

     //------------------------------------------------------------------------------
     // PLACEABLES
     //------------------------------------------------------------------------------
     public const string NRD_IP_BANDIT_MAIN_CAMP_DOOR = "nrd100ip_to_kastigircave";     // Door to the main bandit camp
     public const string NRD_IP_SCOUT_CAVE_DOOR = "nrd100ip_to_scout_cave";             // Door to Scout Cave
     public const string NRD_IP_KASTIGIR_WALL = "nrd100ip_kastigir_blocker";            // Boulders concealing Kastigir's
     public const string NRD_IP_EXORCIST_BRAZIER = "nrd104ip_exorcist_brazier";

     //------------------------------------------------------------------------------
     // TRIGGERS
     //------------------------------------------------------------------------------
     //public const string URN_TR_VISION_TALK = "urn230tr_vision_talk";       // This is a trigger that makes the spirits speak
     public const string NRD_TR_JUNCTION_ENTER = "nrd100tr_junc_tow_enter";
     public const string NRD_TR_JUNCTION_EXIT = "nrd100tr_junc_tow_exit";

     //------------------------------------------------------------------------------
     // CONSTANTS
     //------------------------------------------------------------------------------
     public const string NRD_BANDIT_BELT_COUNT = "NRD_BANDIT_BELT_COUNT";       //Number of bandit belts turned in.
     public const string NRD_DIVINE_COIN_COUNT = "NRD_DIVINE_COIN_COUNT";       //Number of divine coins turned in.

     public const string CUSTOM_HOME_ENABLED = "CUSTOM_HOME_ENABLED";
     public const string CUSTOM_HOME_LOCATION_X = "CUSTOM_HOME_LOCATION_X";
     public const string CUSTOM_HOME_LOCATION_Y = "CUSTOM_HOME_LOCATION_Y";
     public const string CUSTOM_HOME_LOCATION_Z = "CUSTOM_HOME_LOCATION_Z";
     public const string CUSTOM_HOME_LOCATION_FACING = "CUSTOM_HOME_LOCATION_FACING";

     //------------------------------------------------------------------------------
     // AREAS
     //------------------------------------------------------------------------------
     public const string NRD_AR_NORTH_ROAD = "nrd100ar_lake_calenhad_dock";
     public const string NRD_AR_THREE_PENNY_MONGREL = "nrd120ar_threepenny";
     public const string NRD_AR_TOWER_1 = "nrd150ar_south_tower_1";
     public const string NRD_AR_TOWER_2 = "nrd153ar_west_tower_1";
     public const string NRD_AR_KASTIGIRS_CAVE = "nrd140ar_kastigircave";
     public const string NRD_AR_SPOILED_PRINCESS = "nrd110ar_princess";

     //------------------------------------------------------------------------------
     // AREA LISTS
     //------------------------------------------------------------------------------
     //public const string URN_AL_RUINED_VILLAGE = "urn01al_ruined_village";

     //------------------------------------------------------------------------------
     // CONVERSATIONS
     //------------------------------------------------------------------------------
     //public const string URN_DG_BLOOD_MAGIC_CUTSCENE = "bhm100_cutscene_blood.dlg";

     //------------------------------------------------------------------------------
     // ITEMS
     //------------------------------------------------------------------------------
     public const string NRD_IT_DIVINE_COIN = "nrd110it_divine_coin";
     public const string NRD_R_IT_DIVINE_COIN = "nrd110it_divine_coin.uti";
     public const string NRD_IT_STOLEN_GOODS = "nrd100it_stolen_goods";
     public const string NRD_R_IT_STOLEN_GOODS = "nrd100it_stolen_goods.uti";
     public const string NRD_IT_BANDIT_BELT = "nrd100it_bandit_belt";
     public const string NRD_R_IT_BANDIT_BELT = "nrd100it_bandit_belt.uti";
     public const string NRD_IT_TAGGERDY_LETTERS = "nrd100it_taggerdy_letters";
     public const string NRD_R_IT_TAGGERDY_LETTERS = "nrd100it_taggerdy_letters.uti";
     public const string NRD_IT_DRAKE_SCALE = "nrd100it_drake_scale";
     public const string NRD_R_IT_DRAKE_SCALE = "nrd100it_drake_scale.uti";
     public const string NRD_IT_CAGE_KEY = "nrd100it_cage_key";
     public const string NRD_R_IT_CAGE_KEY = "nrd100it_cage_key.uti";
     public const string NRD_IT_LOGHAIN_MAP = "nrd100it_loghain_map";
     public const string NRD_R_IT_LOGHAIN_MAP = "nrd100it_loghain_map.uti";
     public const string NRD_IT_HOWE_LETTER = "nrd100it_howe_letter";
     public const string NRD_R_IT_HOWE_LETTER = "nrd100it_howe_letter.uti";
     public const string NRD_IT_TAGGER_DISGUISE = "nrd100it_tagger_disguise";
     public const string NRD_R_IT_TAGGER_DISGUISE = "nrd100it_tagger_disguise.uti";
     public const string NRD_IT_INCENSE = "nrd104it_exorcist_incense";
     public const string NRD_R_IT_INCENSE = "nrd104it_exorcist_incense.uti";
     public const string NRD_IT_TRESSA_PENDANT = "nrd100it_pendant";
     public const string NRD_R_IT_TRESSA_PENDANT = "nrd100it_pendant.uti";
     public const string NRD_IT_REFUGEE_SHIELD = "nrd100it_refugee_shield";
     public const string NRD_R_IT_REFUGEE_SHIELD = "nrd100it_refugee_shield.uti";
     public const string NRD_IT_QUARTER_NOTE = "nrd100it_quarter_note";
     public const string NRD_R_IT_QUARTER_NOTE = "nrd100it_quarter_note.uti";
}