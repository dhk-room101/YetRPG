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
     //#include"den_lc_constants_h"
     // Global objects for Denerim and Landsmeet plots

     ////////////////////////////////////////////////////////////////////////////////
     // PLACEABLES
     ////////////////////////////////////////////////////////////////////////////////
     // Alienage
     public const string DEN_IP_TO_HOSPICE_FRONT = "den300ip_to_hospice_front";
     public const string DEN_IP_ALIEANGE_GATES_OPEN = "den300ip_front_gate_open";
     public const string DEN_IP_ALIEANGE_GATES_CLOSED = "den300ip_front_gate_closed";
     public const string DEN_IP_VALENDRIANS_ENTRANCE = "den300ip_to_valendrians";
     public const string DEN_IP_CYRIONS_ENTRANCE = "den300ip_to_players_house";
     public const string DEN_IP_NOTE_AND_KEY = "den340ip_note";
     public const string DEN_IP_FROM_APARTMENT_BACK = "den350ip_to_alienage_rear";
     public const string DEN_IP_SLAVER_COMPOUND_TO_ALLEY = "den360ip_to_alienage";
     public const string DEN_IP_CALADRIUS_DOOR = "opentalker_den360cr_caladrius";

     //Eamon's
     public const string DEN_IP_EAMON_ALISTAIR_CHEST = "den211ip_alistairs_stuff";
     public const string DEN_IP_EAMON_KITCHEN_TO_MARKET = "den211ip_to_exterior_right";
     public const string DEN_IP_EAMON_CENTER_TO_MARKET = "den211ip_to_exterior_main";
     public const string DEN_IP_EAMON_SERVANT_TO_MARKET = "den211ip_to_exterior_left";

     // Market
     public const string DEN_IP_OUTSIDE_GOLDANA = "den200ip_to_goldanas_house";
     public const string DEN_IP_MARKET_EAMON_PORTCULLIS = "den200ip_portcullis";
     public const string DEN_IP_MARKET_EAMON_PORTCULLIS_OPEN = "den200ip_portcullis_open";
     public const string DEN_IP_MARKET_ALIENAGE_PORTCULLIS = "den200ip_nw_gate";
     public const string DEN_IP_MARKET_TO_EAMON_LANDSMEET = "den200ip_to_eamon_exterior";
     public const string DEN_IP_MARKET_TO_ALIENAGE_EXIT_UNAVAILABLE = "den200ip_alienage_exit";
     public const string DEN_IP_MARKET_TO_ALIENAGE_EXIT_AVAILABLE = "den200ip_northwest_exit";

     public const string DEN_IP_LITE_RED_JENNY_DOOR = "den200ip_lt_jenny_door";

     //Brothel
     public const string DEN_IP_BROTHEL_PLAYER_DOOR = "den100ip_player_bedroom";

     // Rescue
     public const string DEN_IP_RESCUE_SERVANTS_WING_DOOR = "den510ip_servants_wing";
     public const string DEN_IP_RESCUE_MAIN_EXIT = "den510ip_to_arl_front";
     public const string DEN_IP_RESCUE_VAUGHANS_LOCKBOX = "den510ip_vaughans_lockbox";
     public const string DEN_IP_RESCUE_TO_MAP = "den500ip_exit";
     public const string DEN_IP_RESCUE_ANORA_DOOR = "den510ip_talk_door_anora";
     public const string DEN_IP_RESCUE_RIORDAN_DOOR = "den510ip_cell_door";

     // Captured
     public const string DEN_IP_CAPTURED_WRECKED_BALLISTA = "den400ip_ballista_1";
     public const string DEN_IP_CAPTURED_DOOR_BARRACKS = "den400ip_door_barracks";
     public const string DEN_IP_CAPTURED_DOOR_RECEPTION = "den400ip_door_reception";
     public const string DEN_IP_CAPTURED_DOOR_CAPTAIN = "talker_den400cr_captain";
     public const string DEN_IP_CAPTURED_DOOR_HALL_FRONT = "den400ip_door_hall_front";
     public const string DEN_IP_CAPTURED_DOOR_HALL_REAR = "den400ip_door_hall_rear";
     public const string DEN_IP_CAPTURED_CAGE = "den400ip_cage";
     public const string DEN_IP_CAPTURED_EXIT = "den400ip_front_door";
     public const string DEN_IP_CAPTURED_ARMOR_STAND = "den400ip_armor_stand";
     public const string DEN_IP_CAPTURED_SWORD_STAND = "den400ip_regulation_swords";

     // Landsmeet
     public const string DEN_IP_LANDSMEET_CHAMBER_DOORS = "den600ip_chamber_doors";
     public const string DEN_IP_LANDSMEET_EXIT = "den600ip_exit";

     // Instances
     public const string DEN_IP_RESCUE_VAUGHANS_DOOR = "talker_den511cr_vaughan";
     public const string DEN_IP_RESCUE_SORIS_DOOR = "talker_den511cr_soris";
     public const string DEN_IP_RESCUE_IRMINRIC_DOOR = "talker_den511cr_irminric";
     public const string DEN_IP_RESCUE_CRAZY_DOOR = "talker_den511cr_crazy_victim";

     public const string DEN_IP_CLERIC_APOTHECARY_DOOR = "talker_den800cr_apothecary";
     public const string DEN_IP_CLERIC_BLOOD_MAGE_DOOR = "talker_den800cr_bloodmage1";

     public const string DEN_IP_MARJORLAINE = "den200ip_to_leliana_plot";

     //Light Content - den922ar_wide_2 alley
     public const string DEN_IP_VIAL_PHYLACTERY = "den922ip_lt_phylactery";

     ////////////////////////////////////////////////////////////////////////////////
     // AREAS
     ////////////////////////////////////////////////////////////////////////////////
     public const string DEN_AR_PREFIX = "den";
     public const string DEN_AR_MARKET = "den200ar_market";
     public const string DEN_AR_ALIENAGE = "den300ar_elven_alienage";
     public const string DEN_AR_SLAVER_COMPOUND = "den360ar_slaver_compound";
     public const string DEN_AR_SLAVER_APARTMENTS = "den300ar_elven_alienage";
     public const string DEN_AR_VALENDRIANS_HOME = "den330ar_valendrians_home";
     public const string DEN_AR_CITY_ELF_PC_HOME = "den310ar_cityelf_pc_house";
     public const string DEN_AR_HOSPICE = "den340ar_hospice";
     public const string DEN_AR_EAMON_ESTATE_1 = "den211ar_arl_eamon_estate_1";
     public const string DEN_AR_ARL_EXTERIOR = "den500ar_arl_exterior";
     public const string DEN_AR_ARL_ESTATE = "den510ar_arl_estate";
     public const string DEN_AR_ARL_DUNGEON = "den511ar_arl_dungeon";
     public const string DEN_AR_START_AREA = "default_start_area";
     public const string DEN_AR_NOBLE_TAVERN = "den220ar_noble_tavern";
     public const string DEN_AR_FORT = "den400ar_fort";
     public const string DEN_AR_EAMON_CAPTURED = "den410ar_eamon_estate_2";
     public const string DEN_AR_LANDSMEET_CHAMBER = "den600ar_landsmeet_chamber";
     public const string DEN_AR_CROW_ENCOUNTER = "den920ar_crows";
     public const string DEN_AR_TEMPLAR_ENCOUNTER = "den910ar_templar";

     ////////////////////////////////////////////////////////////////////////////////
     // CREATURES
     ////////////////////////////////////////////////////////////////////////////////

     //Eamon's Estate

     //Alienage
     public const string DEN_CR_ALIENAGE_GUARD = "den300cr_alienage_guard";
     public const string DEN_CR_ELF_THUG_1 = "den300cr_elf_thug_1";
     public const string DEN_CR_ELF_THUG_2 = "den300cr_elf_thug_2";
     public const string DEN_CR_ELF_THUG_3 = "den300cr_elf_thug_3";
     public const string DEN_CR_BEATEN_GUY = "den300cr_beaten_guy";
     public const string DEN_CR_SHIANNI = "den300cr_shianni";
     public const string DEN_CR_CROWD_FEM_3 = "den300cr_crowd_elf_fem_3";
     public const string DEN_CR_VERAS = "den300cr_veras";
     public const string DEN_CR_SARITOR = "den300cr_saritor";
     public const string DEN_CR_TEVINTER_GUARD_1 = "den300cr_tevinter_guard_1";
     public const string DEN_CR_TEVINTER_GUARD_2 = "den300cr_tevinter_guard_2";
     public const string DEN_CR_ALLEY_GUARD = "den350cr_alley_guard";
     public const string DEN_CR_HOSPICE_SUPERVISOR = "den340cr_supervisor";
     public const string DEN_CR_CALADRIUS = "den360cr_caladrius";
     public const string DEN_CR_VALENDRIAN = "den330cr_valendrian";
     public const string DEN_CR_CITY_ELF_FATHER = "den360cr_father";
     public const string DEN_CR_DEVERA = "den360cr_devera";
     public const string DEN_CR_HOSPICE_SIDE_GUARD = "den300cr_side_guard";

     //Rescue
     public const string DEN_CR_FRONT_GUARD_1 = "den500cr_front_guard_1";
     public const string DEN_CR_REAR_GUARD_1 = "den500cr_rear_guard_1";
     public const string DEN_CR_REAR_GUARD_2 = "den500cr_rear_guard_2";
     public const string DEN_CR_ARL_CRAFTSMAN_1 = "den500cr_craftsman_1";
     public const string DEN_CR_ARL_HOWE = "den511cr_howe";
     public const string DEN_CR_OSWYN = "den511cr_oswyn";
     public const string DEN_CR_CRAZY_VICTIM = "den511cr_crazy_victim";
     public const string DEN_CR_SORIS = "den511cr_soris";
     public const string DEN_CR_IRMINRIC = "den511cr_irminric";
     public const string DEN_CR_HEAD_COOK = "den510cr_head_cook";
     public const string DEN_CR_COOK_1 = "den510cr_cook_1";
     public const string DEN_CR_VAUGHAN = "den511cr_vaughan";
     public const string DEN_CR_WALKER_1_1 = "den510cr_walking1_1";
     public const string DEN_CR_WALKER_1_2 = "den510cr_walking1_2";
     public const string DEN_CR_WALKER_2_1 = "den510cr_walking_2_1";
     public const string DEN_CR_WALKER_2_2 = "den510cr_walking_2_2";
     public const string DEN_CR_WALKER_2_3 = "den510cr_walking_2_3";
     public const string DEN_CR_RIORDAN = "den510cr_riordan";
     public const string DEN_CR_RIORDAN_JAILOR = "den510cr_jailor";
     public const string DEN_CR_RIORDAN_JAILOR_DEAD = "den510cr_jailor_dead";
     public const string DEN_CR_MAKEOUT_SERVANT = "den510cr_servant";
     public const string DEN_CR_UPPER_WATCH = "den500cr_upperwatch";

     // Captured
     public const string DEN_CR_CAPTURED_FRONT_GUARD_1 = "den400cr_front_guard_1";
     public const string DEN_CR_OFF_DUTY_1 = "den400cr_offduty_guard_1";
     public const string DEN_CR_OFF_DUTY_2 = "den400cr_offduty_guard_2";
     public const string DEN_CR_SERGEANT = "den400cr_sergeant";
     public const string DEN_CR_CAPTURED_CAPTAIN = "den400cr_captain";
     public const string DEN_CR_AUGUSTINE = "den400cr_augustine";
     public const string DEN_CR_JAILOR = "den400cr_jailor";
     public const string DEN_CR_PRISONER = "den400cr_prisoner";
     public const string DEN_CR_POST_GUARD_1 = "den400cr_post_guard_1";
     public const string DEN_CR_POST_GUARD_2 = "den400cr_post_guard_2";
     public const string DEN_CR_STORAGE_GUARD_1 = "den400cr_storage_guard_1";
     public const string DEN_CR_STORAGE_GUARD_2 = "den400cr_storage_guard_2";
     public const string DEN_CR_KENNEL_MASTER = "den400cr_kennel_master";

     // Market
     public const string DEN_CR_THEOHILD = "den700cr_theohild";
     public const string DEN_CR_PERPETUA = "den700cr_perpetua";
     public const string DEN_CR_RUFFIAN_1 = "den200cr_ruffian_1";
     public const string DEN_CR_RUFFIAN_2 = "den200cr_ruffian_2";
     public const string DEN_CR_RUFFIAN_3 = "den200cr_ruffian_3";
     public const string DEN_CR_ISABELA = "den200cr_isabela";
     public const string DEN_CR_CASAVIR = "den200cr_casavir";
     public const string LIT_CR_FITE_RAELNOR = "lite_fite_raelnor";

     // Noble Tavern
     public const string DEN_CR_SIGHARD = "den220cr_sighard";
     public const string DEN_CR_CEORLIC = "den220cr_ceorlic";

     //Eamon's Estate
     public const string DEN_CR_ARL_EAMON = "den211cr_eamon";
     public const string DEN_CR_SER_CAUTHRIEN = "den211cr_ser_cauthrien";
     public const string DEN_CR_ERLINA = "den212cr_erlina";
     public const string DEN_CR_ANORA = "den510cr_anora";
     public const string DEN_CR_AGATHA = "den211cr_agatha";
     public const string DEN_CR_NIGELLA = "den211cr_nigella";
     public const string DEN_CR_DENOEL = "den211cr_denoel";
     public const string DEN_CR_MAID_1 = "den212cr_maid_1";
     public const string DEN_CR_MAID_2 = "den212cr_maid_2";
     public const string DEN_CR_SERVANT = "den212cr_servant";
     public const string DEN_CR_HOUSEKEEPER = "den211cr_housekeeper";
     public const string DEN_CR_SCULLION = "den211cr_scullion";
     public const string DEN_CR_UNDERCOOK = "den211cr_undercook";

     // Crow ambush
     public const string DEN_CR_TALIESIN = "den920cr_taliesin";
     public const string DEN_CR_CROW_BOW = "den920cr_crow_bow";

     //Stealing encounters
     public const string DEN_CR_STEALING_GUARD_1 = "den911cr_soldier_1";
     public const string DEN_CR_STEALING_GUARD_2 = "den912cr_soldier_1";
     public const string DEN_CR_KYLON_GUARD = "den200cr_kylon_guard";

     // Landsmeet
     public const string DEN_CR_ALFSTANNA = "den220cr_alfstanna";
     public const string DEN_CR_ARL_BRYLAND = "den220cr_bryland";
     public const string DEN_CR_ARL_WULFF = "den220cr_wulff";
     public const string DEN_CR_LOGHAIN_BOSS = "den600cr_loghain_boss";
     public const string DEN_CR_LOGHAIN_DUEL = "den600cr_loghain_duel";
     public const string DEN_CR_ELEMENA = "den600cr_elemena";

     //Dog plot
     public const string DEN_CR_CHILD_DOG = "den200cr_child_dog";

     public const string DEN_CR_BOUNCER_1 = "den100cr_bouncer_1";
     public const string DEN_CR_BOUNCER_2 = "den100cr_bouncer_2";

     //Light Content - alley
     public const string DEN_CR_VIALS_REVENANT = "den922cr_lt_revenant";

     ////////////////////////////////////////////////////////////////////////////////
     // TEAMS
     ////////////////////////////////////////////////////////////////////////////////

     // Slave trade
     public const int DEN_TEAM_ALIENAGE_CITY_GUARD = 1;
     public const int DEN_TEAM_ALIENAGE_HOSPICE_FRONT_GUARD = 2;
     public const int DEN_TEAM_ALIENAGE_HOSPICE_SIDE_GUARD = 3;
     public const int DEN_TEAM_ALIENAGE_HOSPICE_INTERIOR_GUARDS = 4;
     public const int DEN_TEAM_ALIENAGE_ALLEY_GUARD_AMBUSH = 5;
     public const int DEN_TEAM_ALIENAGE_DEVERAS_GROUP = 6;
     public const int DEN_TEAM_ALIENAGE_ELF_CROWD = 8;
     public const int DEN_TEAM_ALIENAGE_COMPOUND_GUARDS = 9;
     public const int DEN_TEAM_ALIENAGE_HOSPICE_SLAVES = 12;
     public const int DEN_TEAM_ALIENAGE_CALADRIUS_WAVE_1 = 13;
     public const int DEN_TEAM_ALIENAGE_CALADRIUS_WAVE_2 = 14;
     public const int DEN_TEAM_ALIENAGE_ELF_BULLIES = 50;
     public const int DEN_TEAM_ALIENAGE_COMPOUND_SLAVES = 51;
     public const int DEN_TEAM_ALIENAGE_ELF_CROWD_EXPLODERS = 52;
     public const int DEN_TEAM_ALIENAGE_CALADRIUS_DOORS = 53;
     public const int DEN_TEAM_ALIENAGE_CALADRIUS_CHEST = 54;
     public const int DEN_TEAM_ALIENAGE_APARTMENT_GUARDS = 55;
     public const int DEN_TEAM_ALIENAGE_NOTICES = 56;
     //                                                        57-100
     //91 is used for the intro trap in Howe's dungeon

     // Rescue
     public const int DEN_TEAM_RESCUE_REAR_GUARDS = 7;
     public const int DEN_TEAM_RESCUE_CAPTAIN = 10;
     public const int DEN_TEAM_RESCUE_COOKS = 11;
     public const int DEN_TEAM_RESCUE_BARRACKS_1 = 15;
     public const int DEN_TEAM_RESCUE_BARRACKS_2 = 16;
     public const int DEN_TEAM_RESCUE_KENNEL = 17;
     public const int DEN_TEAM_RESCUE_DINING_ROOM = 18;
     public const int DEN_TEAM_RESCUE_GUARD_ROOM = 19;
     public const int DEN_TEAM_RESCUE_MAIN_ENTRANCE = 20;
     public const int DEN_TEAM_RESCUE_ARMORY = 21;
     public const int DEN_TEAM_RESCUE_HOWE = 22;
     public const int DEN_TEAM_RESCUE_ANORA = 23;
     public const int DEN_TEAM_RESCUE_FRONT_AMBIENTS = 24;
     public const int DEN_TEAM_RESCUE_TORTURERS = 25;
     public const int DEN_TEAM_RESCUE_GATEKEEPER = 26;
     public const int DEN_TEAM_RESCUE_MAKEOUT_GUARD = 27;
     public const int DEN_TEAM_RESCUE_HALL_AMBUSH = 28;
     public const int DEN_TEAM_RESCUE_HIDDEN_BRIBE = 29;
     public const int DEN_TEAM_RESCUE_SUMMONED_GUARDS_1 = 30;
     public const int DEN_TEAM_RESCUE_UPPER_WATCH = 31;
     public const int DEN_TEAM_RESCUE_PATROLLER = 32;
     public const int DEN_TEAM_RESCUE_DUNGEON_WELCOME = 33;
     public const int DEN_TEAM_RESCUE_DUNGEON_WELCOME_SPEAKERS = 34;
     public const int DEN_TEAM_RESCUE_MAGE_AMBUSH = 35;
     public const int DEN_TEAM_RESCUE_RIORDAN = 36;
     //                                                        37-49

     // Captured
     public const int DEN_TEAM_CAPTURED_CAUTHRIEN = 100;
     public const int DEN_TEAM_CAPTURED_MAIN_HALL = 101;
     public const int DEN_TEAM_CAPTURED_FRONT_GUARDS = 102;
     public const int DEN_TEAM_CAPTURED_BARRACKS = 103;
     public const int DEN_TEAM_CAPTURED_CAPTAIN = 104;
     public const int DEN_TEAM_CAPTURED_JAILOR = 105;
     public const int DEN_TEAM_CAPTURED_CAUTHRIEN_GUARD = 106;
     public const int DEN_TEAM_CAPTURED_CAUTHRIEN_RANGED = 107;
     public const int DEN_TEAM_CAPTURED_KENNEL = 108;
     public const int DEN_TEAM_CAPTURED_STORAGE = 109;
     public const int DEN_TEAM_CAPTURED_GUARD_POST = 110;
     public const int DEN_TEAM_CAPTURED_EQUIPMENT_ROOM = 111;
     public const int DEN_TEAM_CAPTURED_COLONEL = 112;
     public const int DEN_TEAM_CAPTURED_ARMOR_RACK = 113;
     public const int DEN_TEAM_CAPTURED_DEAD_PRISONERS = 114;
     public const int DEN_TEAM_CAPTURED_JAILOR_2 = 115;
     public const int DEN_TEAM_CAPTURED_STATUE_WEST_WHOLE = 116;
     public const int DEN_TEAM_CAPTURED_STATUE_WEST_BROKEN = 117;
     public const int DEN_TEAM_CAPTURED_STATUE_EAST_WHOLE = 118;
     public const int DEN_TEAM_CAPTURED_STATUE_EAST_BROKEN = 119;
     public const int DEN_TEAM_CAPTURED_MAIN_HALL_FRONT_DOOR = 120;
     public const int DEN_TEAM_CAPTURED_MAIN_HALL_FRONT_DOOR_BROKEN = 121;
     public const int DEN_TEAM_CAPTURED_BALLISTAE = 122;
     // 140 being used by "lite" content, apparently
     // 200s being used by "lite" content, apparently

     // Landsmeet
     public const int DEN_TEAM_LANDSMEET_CAUTHRIEN = 400;
     public const int DEN_TEAM_LANDSMEET_CAUTHRIEN_GUARD = 401;
     public const int DEN_TEAM_LANDSMEET_CAUTHRIEN_RANGED = 402;
     public const int DEN_TEAM_LANDSMEET_ALFSTANNA = 403;
     public const int DEN_TEAM_LANDSMEET_BRYLAND = 404;
     public const int DEN_TEAM_LANDSMEET_CEORLIC = 405;
     public const int DEN_TEAM_LANDSMEET_EAMON = 406;
     public const int DEN_TEAM_LANDSMEET_LOGHAIN_BOSS = 407;
     public const int DEN_TEAM_LANDSMEET_ROYAL_GUARD = 408;
     public const int DEN_TEAM_LANDSMEET_SIGHARD = 409;
     public const int DEN_TEAM_LANDSMEET_WULFF = 410;
     public const int DEN_TEAM_LANDSMEET_LOGHAIN_REINFORCMENTS_1 = 411;
     public const int DEN_TEAM_LANDSMEET_LOGHAIN_REINFORCMENTS_2 = 412;
     public const int DEN_TEAM_LANDSMEET_LOGHAIN_REINFORCMENTS_3 = 413;
     public const int DEN_TEAM_LANDSMEET_LOGHAIN_REINFORCMENTS_4 = 414;
     public const int DEN_TEAM_LANDSMEET_NONCOMBATANTS = 415;
     public const int DEN_TEAM_LANDSMEET_DUEL_WALLS = 416;
     public const int DEN_TEAM_LANDSMEET_LOGHAIN_BOSS_RANGED = 417;
     public const int DEN_TEAM_LANDSMEET_DUEL_LOGHAIN_CROWD = 418;
     public const int DEN_TEAM_LANDSMEET_DUEL_PLAYER_CROWD = 419;
     public const int DEN_TEAM_LANDSMEET_LOGHAIN_MAGES = 420;

     // Brothel
     public const int DEN_TEAM_BROTHEL_HAMMER_AND_TONGS = 500;
     public const int DEN_TEAM_BROTHEL_MAGIC_WAND = 501;
     public const int DEN_TEAM_BROTHEL_WHIP = 502;
     public const int DEN_TEAM_BROTHEL_NUG = 503;
     public const int DEN_TEAM_BROTHEL_DWARF = 504;

     public const int DEN_TEAM_BROTHEL_SELECTION_FEMALES_FIRST = 550;
     public const int DEN_TEAM_BROTHEL_SELECTION_FEMALES_REST = 551;
     public const int DEN_TEAM_BROTHEL_SELECTION_MALES_LAST = 552;
     public const int DEN_TEAM_BROTHEL_SELECTION_MALES_REST = 553;
     public const int DEN_TEAM_BROTHEL_SELECTION_TRANSVESTITES = 554;
     public const int DEN_TEAM_BROTHEL_ISABELA_AND_CASIVIR = 555;

     // Market
     public const int DEN_TEAM_MARKET_HABREN = 600;

     public const int DEN_TEAM_NOBLES = 601;
     public const int DEN_TEAM_CACHE_INTERACTIVE = 610;
     public const int DEN_TEAM_CACHE_NON_INTERACTIVE = 611;
     public const int LIT_TEAM_FITE_LEADERSHIP_RAELNOR = 612;
     public const int DEN_TEAM_MARKET_AMBIENT = 615;
     public const int DEN_TEAM_MARKET_KITTENS = 616;

     // Alleys
     public const int DEN_TEAM_CROWS = 700;
     public const int DEN_TEAM_CROW_TRAPS = 705;
     public const int DEN_TEAM_INFAMY_SOLDIERS_1 = 710;
     public const int DEN_TEAM_INFAMY_SOLDIERS_2 = 720;

     public const int DEN_TEAM_RANDOM_1 = 750;
     public const int DEN_TEAM_RANDOM_2 = 751;
     public const int DEN_TEAM_RANDOM_PORTCULLIS_OPEN = 752;
     public const int DEN_TEAM_RANDOM_PORTCULLIS_CLOSED = 753;
     public const int DEN_TEAM_RANDOM_PORTCULLIS_LEVER = 754;

     ////////////////////////////////////////////////////////////////////////////////
     // WAYPOINTS
     ////////////////////////////////////////////////////////////////////////////////
     // Slave Trade
     public const string DEN_WP_SLAVE_CALADRIUS = "den360wp_caladrius";
     public const string DEN_WP_SLAVE_DEVERA = "den360wp_devera";
     public const string DEN_WP_SLAVE_COMPOUND_PLAYER = "den360wp_player";
     public const string DEN_WP_CYRIONS_ENTRANCE = "den300wp_from_players_house";
     public const string DEN_WP_ALIENAGE_SHIANI_EXIT = "den300wp_shianni_exit";
     public const string DEN_WP_HOSPICE_FRONT_ENTRANCE = "den340wp_from_alienage_front";
     public const string DEN_WP_COMPOUND_FROM_ALLEY = "den360wp_from_alienage";
     public const string DEN_WP_APARTMENTS_FROM_ALLEY = "den350wp_from_alienage_rear";
     public const string DEN_WP_ALLEY_FROM_APARTMENTS = "den300wp_from_apartments_rear";
     public const string DEN_WP_ALLEY_FROM_COMPOUND = "den300wp_from_compound";
     public const string DEN_WP_CITY_ELF_PC_HOME = "den310wp_from_alienage";
     public const string DEN_WP_VALENDRIANS_HOME = "den330wp_from_alienage";

     public const string DEN_WP_MARKET_FROM_MAP = "wmw_denerim";
     public const string DEN_WP_MARKET_ALIENAGE_MAPNOTE = "den200wp_alienage_map_note";
     public const string DEN_WP_MARKET_FROM_EAMON = "den200wp_from_eamon";
     public const string DEN_WP_MARKET_FROM_EAMON_SERVANT = "den200wp_from_eamon_left";
     public const string DEN_WP_MARKET_FROM_EAMON_KITCHEN = "den200wp_from_eamon_right";
     public const string DEN_WP_MARKET_FROM_MARJOLAINE = "den200wp_from_leliana_plot";
     public const string DEN_WP_FROM_GOLDANAS = "den200wp_from_goldanas";

     public const string DEN_WP_MARKET_TO_ALIENAGE = "wmw_den_market_nw";
     public const string DEN_WP_ALIENAGE_FROM_MAP = "wmw_den_alienage";
     public const string WMW_DENERIM = "wmw_denerim"; //CNM: using this for testing the gossips in Denerim market, too

     public const string DEN_WP_CROW_ENCOUNTER_START = "start";

     // noble tavern
     public const string DEN_WP_NOBLE_SIGHARD = "den220wp_sighard";

     public const string ZZ_DEN_WP_NOBLE_TAVERN = "zz_den220wp_noble_tavern";
     public const string DEN_WP_START_AREA = "wp_start";

     //Brothel
     public const string DEN_WP_BROTHEL_PLAYER_ROOM = "den100wp_player_room";
     public const string DEN_WP_BROTHEL_SANGA = "den100wp_sanga";
     public const string DEN_WP_BROTHEL_BOUNCER_1 = "den100wp_bouncer_1";
     public const string DEN_WP_BROTHEL_BOUNCER_2 = "den100wp_bouncer_2";

     // Rescue
     public const string DEN_WP_ARL_EXTERIOR_FROM_MAP = "wmw_den_arl_exterior";
     public const string DEN_WP_EXIT_UPPERWATCH = "den500wp_exit_upperwatch";
     public const string DEN_WP_RESCUE_DUNGEON_ENTRANCE = "den511wp_from_arl_estate";
     public const string DEN_WP_RESCUE_ERLINA_BY_WAGON = "den500wp_erlina_by_wagon";
     public const string DEN_WP_RESCUE_ERLINA_NEAR_GUARDS = "wp_erlina_by_guards";
     public const string DEN_WP_RESCUE_HIDING_SPOT = "den500wp_hiding_spot";
     public const string DEN_WP_RESCUE_DISTRACTION_ERLINA = "wp_distraction_erlina";
     public const string DEN_WP_RESCUE_REAR_GUARD_1 = "den500wp_rear_guard_1";
     public const string DEN_WP_RESCUE_REAR_GUARD_2 = "den500wp_rear_guard_2";
     public const string DEN_WP_RESCUE_DISTRACTION_GUARD_1 = "wp_distraction_guard_1";
     public const string DEN_WP_RESCUE_DISTRACTION_GUARD_2 = "wp_distraction_guard_2";
     public const string DEN_WP_RESCUE_DIST_START_ERLINA = "wp_distraction_start_erlina";
     public const string DEN_WP_RESCUE_DIST_START_GUARD_1 = "wp_distraction_start_guard_1";
     public const string DEN_WP_RESCUE_DIST_START_GUARD_2 = "wp_distraction_start_guard_2";
     public const string DEN_WP_RESCUE_ERLINA_IN_KITCHEN = "den510wp_erlina_in_kitchen";
     public const string DEN_WP_RESCUE_ERLINA_EXITS_KITCHEN = "den510wp_erlina_exits_kitchen";
     public const string DEN_WP_RESCUE_ERLINA_AT_QUEEN = "den510wp_erlina_at_queen";
     public const string DEN_WP_RESCUE_ERLINA_NEAR_QUEEN = "den510wp_erlina_near_queen";
     public const string DEN_WP_RESCUE_REAR_EXIT = "den510wp_from_arl_exterior";
     public const string DEN_WP_RESCUE_DUNGEON_EXIT = "den510wp_from_arl_dungeon_mid";
     public const string DEN_WP_RESCUE_VAUGHAN_ROOM = "den510wp_vaughan_room";

     // Captured
     public const string DEN_WP_CAPTURED_EAMON = "den410wp_captured_conversation";
     public const string DEN_WP_CAPTURED_ENTRANCE = "den400wp_front_door";
     public const string DEN_WP_CAPTURED_PLAYER_START = "den400wp_player_start";
     public const string DEN_WP_CAPTURED_WRECKED_BALLISTA = "den400wp_wrecked_ballista";
     public const string DEN_WP_CAPTURED_FRONT_GUARDS = "den400wp_front_guards";
     public const string DEN_WP_CAPTURED_WAITING_ROOM = "den400wp_waiting_room";
     public const string DEN_WP_CAPTURED_CAPTAIN = "den400wp_captain";
     public const string DEN_WP_CAPTURED_GUARD_FIGHT = "den400wp_guard_fight";
     public const string DEN_WP_CAPTURED_AUGUSTINE = "den400wp_augustine";
     public const string DEN_WP_CAPTURED_ALISTAIR = "den400wp_alistair";
     public const string DEN_WP_CAPTURED_ALISTAIR_RESCUE = "den400wp_alistair_rescue";
     public const string DEN_WP_CAPTURED_DOG = "den400wp_dog";
     public const string DEN_WP_CAPTURED_LELIANA = "den400wp_leliana";
     public const string DEN_WP_CAPTURED_MORRIGAN = "den400wp_morrigan";
     public const string DEN_WP_CAPTURED_OGHREN = "den400wp_oghren";
     public const string DEN_WP_CAPTURED_SHALE = "den400wp_shale";
     public const string DEN_WP_CAPTURED_STEN = "den400wp_sten";
     public const string DEN_WP_CAPTURED_WYNNE = "den400wp_wynne";
     public const string DEN_WP_CAPTURED_ZEVRAN = "den400wp_zevran";
     public const string DEN_WP_CAPTURED_INSPECTION_FAILED = "den400wp_inspection_failed";
     public const string DEN_WP_CAPTURED_STORAGE_GUARDS = "den400wp_storage_guards";
     public const string DEN_WP_CAPTURED_PLAYER_OUTSIDE_CELL = "den400wp_player_outside_cell";
     public const string DEN_WP_CAPTURED_JAILOR_IN_CELL = "den400wp_jailor_in_cell";

     //Eamon's estate
     public const string DEN_WP_EAMON_ANORA_ROOM = "den211wp_anora_room";
     public const string DEN_WP_EAMON_ALISTAIR_DOWNSTAIRS = "den211wp_dining_alistair";
     public const string DEN_WP_EAMON_UPSTAIRS = "den211wp_eamon_upstairs";
     public const string DEN_WP_EAMON_ANORA = "den212wp_anora";
     public const string DEN_WP_EAMON_ANORA_DOWNSTAIRS = "den211wp_dining_anora";
     public const string DEN_WP_EAMON_PLAYER_DOWNSTAIRS = "den211wp_dining_player";
     public const string DEN_WP_EAMON_ANORA_ERLINA = "den212wp_anora_room_erlina";
     public const string DEN_WP_EAMON_KITCHEN = "den211wp_from_exterior_right";
     public const string DEN_WP_EAMON_SERVANTS_QUARTERS = "den211wp_from_exterior_left";
     public const string DEN_WP_EAMON_FIRST_FLOOR = "den211wp_from_exterior_main";
     public const string DEN_WP_EAMON_ALISTAIR = "wp_camp_gen00fl_alistair";

     // Landsmeet
     public const string DEN_WP_LANDSMEET_ALISTAIR_BALCONY = "den600wp_alistair_balcony";
     public const string DEN_WP_LANDSMEET_DUELIST = "den600wp_duel";
     public const string DEN_WP_LANDSMEET_CHAMBER = "wmw_den_landsmeet_chamber";

     ////////////////////////////////////////////////////////////////////////////////
     // TRIGGERS
     ////////////////////////////////////////////////////////////////////////////////
     public const string DEN_TR_SHIANNI_RANDOM = "den300tr_shianni_random";
     public const string DEN_TR_HOSPICE_SLAVES_TALK = "den340tr_slaves_talk";

     // Rescue
     public const string DEN_TR_RESCUE_CRAFTSMEN_AMBIENT = "den500tr_craftsmen_ambient";
     public const string DEN_TR_RESCUE_ERLINA_DISTRACTION = "den500tr_distraction";
     public const string DEN_TR_RESCUE_COOKS_TALK = "den510tr_cooks_talk";
     public const string DEN_TR_RESCUE_ANORA_TALK = "den510tr_anora_talk";
     public const string DEN_TR_RESCUE_SORIS_LEFT = "den511tr_soris_left";

     ////////////////////////////////////////////////////////////////////////////////
     // ITEMS
     ////////////////////////////////////////////////////////////////////////////////

     // Alienage
     public const string DEN_IM_HOSPICE_KEY = "den300im_hospice_key.uti";
     public const string DEN_IM_SLAVER_DOCUMENTS = "den360im_slaver_documents.uti";
     public const string DEN_IT_SLAVER_DOCUMENTS = "den360im_slaver_documents";
     public const string DEN_IM_APARTMENT_KEY = "den300im_apartment_key.uti";
     public const string DEN_IT_APARTMENT_KEY = "den300im_apartment_key";
     public const string DEN_IM_SLAVER_NOTE = "den340im_note.uti";

     //Landsmeet
     public const string DEN_IT_LOGHAIN_SWORD = "gen_im_wep_mel_lsw_lsw";
     public const string DEN_IT_LOGHAIN_SHIELD = "gen_im_arm_shd_kit_log";

     // Rescue
     public const string DEN_IM_LOWER_PRISON_KEY = "den511im_low_prison_key.uti";
     public const string DEN_IM_VAUGHANS_KEY = "den511im_vaughan_key.uti";
     public const string DEN_IT_VAUGHANS_TREASURE = "den510im_vaughans_treasure";
     public const string DEN_IM_IRMINRIC_SIGNET_RING = "den511im_irminric_ring.uti";

     public const string DEN_IM_RESCUE_HOWE_KEY = "den511im_howe_key.uti";

     public const string DEN_IT_RESCUE_RIORDAN_PAPERS = "den511im_riordan_papers";

     //Captured
     public const string DEN_IM_CAPTURED_CAGE_KEY = "den400im_key_cage.uti";
     public const string DEN_IT_CAPTURED_CAGE_KEY = "den400im_key_cage";
     public const string DEN_IM_CAPTURED_RECEPTION_KEY = "den400im_key_reception.uti";
     public const string DEN_IM_CAPTURED_FRONT_HALL_KEY = "den400im_key_hall_front.uti";
     public const string DEN_IM_CAPTURED_REAR_HALL_KEY = "den400im_key_hall_rear.uti";
     public const string DEN_IM_CAPTURED_DISGUISE = "den400im_disguise.uti";
     public const string DEN_IT_CAPTURED_DISGUISE = "den400im_disguise";
     public const string DEN_IM_CAPTURED_DISGUISE_BOOTS = "den400im_disguise_boots.uti";
     public const string DEN_IT_CAPTURED_DISGUISE_BOOTS = "den400im_disguise_boots";
     public const string DEN_IM_CAPTURED_DISGUISE_HELM = "den400im_disguise_helm.uti";
     public const string DEN_IT_CAPTURED_DISGUISE_HELM = "den400im_disguise_helm";
     public const string DEN_IM_CAPTURED_DISGUISE_GLOVES = "den400im_disguise_gloves.uti";
     public const string DEN_IT_CAPTURED_DISGUISE_GLOVES = "den400im_disguise_gloves";

     public const string DEN_IM_ACID_COATING = "gen_im_qck_coating_101.uti";
     public const string DEN_IM_ACID_FLASK = "gen_im_qck_grenade_101.uti";

     public const string DEN_IM_CAPTURED_PASSWORD_LIST = "den400im_password_list.uti";
     public const string DEN_IT_CAPTURED_PASSWORD_LIST = "den400im_password_list";

     public const string DEN_IM_CAPTURED_REGULATION_SWORD = "den400im_regulation_sword.uti";

     // Light Content
     public const string DEN_IM_FAZZIL_SEXTANT = "den350im_fazzil_sextant.uti";
     public const string DEN_IT_FAZZIL_SEXTANT = "den350im_fazzil_sextant";

     public const string DEN_IM_VIALS_REVNOTE6 = "den922im_rev_note";
     public const string DEN_IM_RED_JENNY_BOX = "cir210im_lt_paintedbox.uti";

     ////////////////////////////////////////////////////////////////////////////////
     // SCRIPTS
     ////////////////////////////////////////////////////////////////////////////////

     public const string DEN_SCRIPT_GUARD_OUTER_AOE = "den510cr_guard_outer_aoe.ncs";
     public const string DEN_SCRIPT_GUARD_INNER_AOE = "den510cr_guard_inner_aoe.ncs";
     public const string DEN_SCRIPT_AREA_CORE = "denar_core.ncs";
     public const string DEN_GEN_TALK_TRIGGER = "gen00tr_talk.ncs";

     ////////////////////////////////////////////////////////////////////////////////
     // CONVERSATIONS
     ////////////////////////////////////////////////////////////////////////////////
     public const string DEN_CONV_RESCUE_PARTY = "den400_rescueparty.dlg";
     public const string DEN_CONV_PARTY_CLICKED = "den400_party_clicked.dlg";
     public const string DEN_CONV_ARMOR_STAND = "den400_armor_stand.dlg";
     public const string DEN_CONV_PC_PRISON = "den400_playerprison.dlg";
     public const string DEN_CONV_IGNATA = "den200_ignata.dlg";
     public const string DEN_CONV_LANDSMEET = "den600_landsmeet.dlg";
     public const string DEN_CONV_LEAVE_HIDING = "den500_leave_hiding.dlg";
     public const string DEN_CONV_FRONT_GUARDS = "den400_frontguards.dlg";
     public const string DEN_CONV_DEBUG = "zz_den_debug.dlg";
     ////////////////////////////////////////////////////////////////////////////////
     // CUSTOM GROUPS
     ////////////////////////////////////////////////////////////////////////////////

     public const int DEN_GROUP_OFFDUTY_1 = 34;
     public const int DEN_GROUP_OFFDUTY_2 = 35;
     public const int DEN_GROUP_WALKING_BOMBS = 14;
}