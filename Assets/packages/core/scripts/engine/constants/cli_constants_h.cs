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
     // Climax GameObject constants

     // Army deployment area script
     public const string CLI_AR_ARMY_SCRIPT = "cli000ar_army_deployment.ncs";

     // Army deployment constants
     public const float CLI_GOOD_ARMY_SPAWN_DELAY_MIN = 3.0f;
     public const float CLI_GOOD_ARMY_SPAWN_DELAY_MAX = 6.0f;
     public const int CLI_ARMY_MAX_SOLDIERS_IN_AREA = 8; // max # of soldiers to be in the area at the same time
     public const float CLI_ARMY_SPAWN_DELAY_MIN = 3.0f; // spawn army members in a random delay between min and max
     public const float CLI_ARMY_SPAWN_DELAY_MAX = 10.0f;
     public const int CLI_ARMY_LEGION_PERCENTAGE_IN_DWARVEN_ARMY = 20; // the chance to spawn a legion soldier instead of normal (out of 100) (if recruited)
     public const string CLI_CR_ARMY_REDCLIFF = "cli000cr_army_redcliffe.utc";
     public const string CLI_CR_ARMY_WEREWOLF = "cli000cr_army_werewolf.utc";
     public const string CLI_CR_ARMY_TEMPLAR = "cli000cr_army_templar.utc";
     public const string CLI_CR_ARMY_MAGE = "cli000cr_army_mage.utc";
     public const string CLI_CR_ARMY_LEGION = "cli000cr_army_legion.utc";
     public const string CLI_CR_ARMY_GOLEM = "cli000cr_army_golem.utc";
     public const string CLI_CR_ARMY_ELF = "cli000cr_army_elf.utc";
     public const string CLI_CR_ARMY_DWARF = "cli000cr_army_dwarf.utc";

     public const string CLI_WP_ARMY_SPAWN = "cli_wp_army_spawn";
     public const string CLI_WP_ARMY_SPAWN_MOVE_AWAY = "cli_wp_army_spawn_move_away";
     public const string CLI_WP_ARMY_SPAWN_MOVE_RANGED = "cli_wp_army_spawn_move_ranged";

     public const int CLI_ARMY_ID_DS_ALIENAGE = 1;
     public const int CLI_ARMY_ID_DS_MARKET_1 = 2;
     public const int CLI_ARMY_ID_DS_MARKET_2 = 3;
     public const int CLI_ARMY_DS_ALIENAGE_OGRE = 2;
     public const int CLI_ARMY_DS_ALIENAGE_MELEE = 1;
     public const int CLI_ARMY_DS_ALIENAGE_RANGED = 2;
     public const int CLI_ARMY_DS_PALACE_1 = 4;
     public const int CLI_ARMY_DS_PALACE_2 = 5;
     public const int CLI_ARMY_DS_PALACE_3 = 6;
     public const int CLI_ARMY_DS_CITY_ATT_1 = 7;
     public const int CLI_ARMY_DS_CITY_ATT_2 = 8;
     public const int CLI_ARMY_DS_CITY_DEF = 9;
     public const int CLI_ARMY_FRIENDLY_ATTACKER = 10;
     public const int CLI_ARMY_DS_FORT_EXTERIOR = 11;
     public const int CLI_ARMY_DS_FORT_ROOF_1 = 12;
     public const int CLI_ARMY_DS_REDCLIFFE = 13;
     public const int CLI_ARMY_DS_CITY_DEF2 = 14;
     public const int CLI_ARMY_DS_CITY_DEF3 = 15;
     public const int CLI_ARMY_ID_DS_MARKET_3 = 16;
     public const int CLI_ARMY_ID_DS_MARKET_4 = 17;
     public const int CLI_ARMY_DS_FORT_ROOF_2 = 18;
     public const int CLI_ARMY_DS_FORT_ROOF_3 = 19;

     // Creatures
     public const string CLI_MESSENGER = "cli01cr_messenger";
     public const string CLI_CR_RIORDAN = "cli300cr_riordan";
     public const string CLI_CR_DWARVEN_SOLDIER1 = "arl230cr_dwarf_soldier1";
     public const string CLI_CR_DWARVEN_SOLDIER2 = "arl230cr_dwarf_soldier2";
     public const string CLI_CR_CITY_GATES_DEFENDER = "cli400cr_city_gate_defender";
     public const string CLI_CR_CITY_GATES_OFFICER = "cli410cr_officer";
     public const string CLI_CR_ALIENAGE_ELDER = "cli600cr_elder";
     public const string CLI_CR_PALACE_DEFENDER = "cli800cr_palace_defender";
     public const string CLI_CR_CHASING_OGRE = "cli900cr_chasing_ogre";
     public const string CLI_CR_CHASED_MAN = "cli900cr_rescued_man";
     public const string CLI_CR_ANORA = "den510cr_anora";
     public const string CLI_CR_ARCHDEMON = "cli220cr_archdemon";
     public const string CLI_CR_ALIENAGE_DEFENDER = "cli600cr_alien_defender";
     public const string CLI_CR_ALIENAGE_OGRE = "cli600cr_ogre";
     public const string CLI_CR_ALIENAGE_GENERAL = "cli600cr_alien_ds_leader";
     public const string CLI_CR_MARKET_GENERAL = "cli700cr_market_ds_leader";
     public const string CLI_CR_SURVIVOR = "cli100cr_survivor";

     // Waypoints
     public const string CLI_WP_REDCLIFFE_CASTLE_START = "arl200wp_from_village";
     public const string CLI_WP_REDCLIFFE_CASTLE_INTERIOR_START = "cli300wp_start";
     public const string CLI_WP_WAITING_OUTSIDE_RIORDANS_ROOM = "cli310wp_waiting_out_riordan";
     public const string CLI_WP_ALISTAIR_IN_ROOM = "cli310wp_gen00fl_alistair";
     public const string CLI_WP_LOGHAIN_IN_ROOM = "cli310wp_gen00fl_loghain";
     public const string CLI_WP_MORRIGAN_WAIT = "cli310wp_gen00fl_morrigan";
     public const string CLI_WP_CITY_GATES_START_FIGHT = "cli400wp_start_fight";
     public const string CLI_WP_CITY_GATES_SPEECH = "cli420wp_speech";
     public const string CLI_WP_CITY_GATES_DEFENCE_START = "cli410wp_start";
     public const string CLI_WMW_PALACE_FROM_CITY = "wmw_cli_palace_from_city";
     public const string CLI_WP_FORT_EXTERIOR_START = "wmw_cli_fort";
     public const string CLI_WP_FORT_EXTERIOR_CHASE_END = "cli900wp_chase_end";
     public const string CLI_WP_FORT_EXTERIOR_CHASE_EXIT = "cli900wp_chase_exit";
     public const string CLI_WP_FORT_DRAKON_ROOF_START = "cli220wp_from_second_floor";
     public const string CLI_WP_ALIENAGE_DEF_UP_1 = "wp_def_up1";
     public const string CLI_WP_ALIENAGE_DEF_UP_2 = "wp_def_up2";
     public const string CLI_WP_ALIENAGE_DEF_DOWN = "wp_def_down";
     public const string CLI_WP_ALIENAGE_DEF_GROUNDS_1 = "wp_ground_1";
     public const string CLI_WP_ALIENAGE_DEF_GROUNDS_2 = "wp_ground_2";
     public const string CLI_WP_ALIENAGE_DEF_GROUNDS_3 = "wp_ground_3";
     public const string CLI_WP_ALIENAGE_DEF_EXIT1 = "wp_def_exit1";
     public const string CLI_WP_ALIENAGE_DEF_EXIT2 = "wp_def_exit2";
     public const string CLI_WP_ALIENAGE_DEF_EXIT3 = "wp_def_exit3";
     public const string CLI_WP_ALIENAGE_CENTER = "wp_def_center";
     public const string CLI_WP_ALIENAGE_DS_SPAWN = "cli600wp_ds_spawn";
     public const string CLI_WP_DS_MOVETO = "cli_wp_ds_moveto";
     public const string CLI_WP_PALACE_DS_CHARGE = "cli800wp_ds_charge";
     public const string CLI_WP_FORT_STORAGE_AMBUSH = "cli200wp_ambush";
     public const string CLI_WP_FORT_ROOF_ARCHDEMON_STAGE_1 = "cli220wp_stage_1";
     public const string CLI_WP_FORT_ROOF_ARCHDEMON_STAGE_2 = "cli220wp_stage_2";
     public const string CLI_WP_FORT_ROOF_ARCHDEMON_STAGE_3 = "cli220wp_stage_3";
     public const string CLI_WP_FORT_ROOF_ARCHDEMON_STAGE_4 = "cli220wp_stage_4";
     public const string CLI_WP_FORT_FIRST_FLOOR_START = "cli200wp_from_exterior";

     public const string CLI_WP_CITY_GATES_FOLLOWER_PREFIX_START = "cli401wp_follower_";
     public const string CLI_WP_CITY_GATES_FOLLOWER_PREFIX_MOVETO = "cli400wp_follower_";

     // Areas
     public const string CLI_REDCLIFFE_CASTLE_INTERIOR = "cli300ar_redcliffe_castle";
     public const string CLI_REDCLIFFE_CASTLE_UPSTAIRS = "cli310ar_redcliffe_castle_2";
     public const string CLI_REDCLIFFE_CASTLE_EXTERIOR = "arl200ar_redcliffe_castle";
     public const string CLI_CITY_GATES = "cli400ar_city_gates";
     public const string CLI_CITY_GATES_DEFENSE = "cli410ar_city_gates_defend";
     public const string CLI_CITY_GATES_CUTSCENE = "cli420ar_city_gates_cutscn";
     public const string CLI_PALACE_DISTRICT = "cli800ar_palace_district";
     public const string CLI_FORT_EXTERIOR = "cli900ar_fort_exterior";
     public const string CLI_FORT_DRAKON_ROOF = "cli220ar_fort_roof_1";
     public const string CLI_MARKET = "cli700ar_markets";
     public const string CLI_ALIENAGE = "cli600ar_elven_alienage";
     public const string CLI_FORT_SECOND_FLOOR = "cli210ar_fort_second_floor";
     public const string CLI_FORT_FIRST_FLOOR = "cli200ar_fort_main_floor";

     // Cutscene
     public const int CLI_CS_MORRIGAN_RITUAL = 56;
     public const int CLI_CS_MORRIGAN_LEAVES = 57;
     public const int CLI_CS_ENEMY_AT_GATES = 58;
     public const int CLI_CS_RIORDAN_FIGHTS_ARCHDEMON = 59;
     public const int CLI_CS_BRIDGE = 60;
     public const int CLI_CS_ARCHDEMON_INTRO = 61;
     public const int CLI_CS_ARCHDEMON_SLAIN = 62;
     public const int CLI_CS_ARMY_MARCH = 64;

     // Placeables
     public const string CLI_IP_ALIENAGE_GATE_OPEN = "cli600ip_gate_to_citygates";
     public const string CLI_IP_ALIENAGE_GATE_CLOSED = "cli600ip_closed_gate";
     public const string CLI_IP_ALIENAGE_BRIDGE_1 = "cli600ip_bridge_1";
     public const string CLI_IP_ALIENAGE_BRIDGE_2 = "cli600ip_bridge_2";
     public const string CLI_IP_ALIENAGE_BRIDGE_3 = "cli600ip_bridge_3";
     public const string CLI_IP_FIRE_MEDIUM = "cli000ip_fire_medium";
     public const string CLI_IP_FIRE_SMALL = "cli000ip_fire_small";
     public const string CLI_IP_FIRE_LARGE = "cli000ip_fire_large";

     // World map
     public const string CLI_WM_DENERIN_CLIMAX = "denerim_climax";

     // Teams
     public const int CLI_TEAM_CITY_GATES_SCARED_DARKSPAWN = 1;
     public const int CLI_TEAM_CITY_GATES_DEFENDERS = 5;
     public const int CLI_TEAM_CITY_GATES_DARKSPAWN_ATTACK = 11;
     public const int CLI_TEAM_ALIENAGE_DEFENDERS = 15;
     public const int CLI_TEAM_ALIENAGE_ATTACKERS = 21;
     public const int CLI_TEAM_PALACE_DEFENDERS = 31;
     public const int CLI_TEAM_PALACE_DEFENDERS_ATTACKERS = 41;
     public const int CLI_TEAM_ARMY_MEMBERS = 51;
     public const int CLI_TEAM_PLC_FORT_BARRICADE = 101;
     public const int CLI_TEAM_FORT_STORAGE_DS = 102;

     // strings
     public const string CLI_CONVERSATION_SPEECH = "cli400_the_speech.dlg";
     public const string CLI_SCRIPT_DS_ARMY = "cli000cr_army_ds.ncs";

     public const string CLI_AR_REDCLIFFE_VILLAGE = "cli100ar_redcliffe_village";
     public const string CLI_AR_REDCLIFFE_CASTLE = "cli150ar_redcliffe_castle";
     public const string CLI_AR_REDCLIFFE_CASTLE_MAIN = "cli300ar_redcliffe_castle";
     public const string CLI_AR_REDCLIFFE_CASTLE_2 = "cli310ar_redcliffe_castle_2";
     public const string CLI_AR_FORT_MAIN_FLOOR = "cli200ar_fort_main_floor";
     public const string CLI_AR_FORT_SECOND_FLOOR = "cli210ar_fort_second_floor";
     public const string CLI_AR_FORT_ROOF_1 = "cli220ar_fort_roof_1";
     public const string CLI_AR_CITY_GATES = "cli400ar_city_gates";
     public const string CLI_AR_CITY_GATES_DEFEND = "cli410ar_city_gates_defend";
     public const string CLI_AR_CITY_GATES_CUTSCN = "cli420ar_city_gates_cutscn";
     public const string CLI_AR_ELVEN_ALIENAGE = "cli600ar_elven_alienage";
     public const string CLI_AR_MARKETS = "cli700ar_markets";
     public const string CLI_AR_PALACE_DISTRICT = "cli800ar_palace_district";
     public const string CLI_AR_FORT_EXTERIOR = "cli900ar_fort_exterior";
}