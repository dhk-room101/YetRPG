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
     //:://////////////////////////////////////////////
     /*
         Paragon of Her Kind
          -> Constants_h

          This file contains all the constants used
          throughout the Paragon plot.
     */
     //:://////////////////////////////////////////////
     //:: Created By: Joshua Stiksma
     //:: Created On: February 5, 2007
     //:://////////////////////////////////////////////

     //#include"global_objects_h"
     //#include"2da_constants_h"

     //==============================================================================
     // Important Constants
     //------------------------------------------------------------------------------

     public const string ORZ_SUPPORT_BHELEN = "ORZ_SUPPORT_BHELEN";
     public const string ORZ_SUPPORT_HARROWMONT = "ORZ_SUPPORT_HARROWMONT";
     public const string ORZ_OGHREN_DEEPROADS_TRACKER = "ORZ_OGHREN_DEEPROADS_TRACKER";

     public const int ORZ_GROUP_BHELEN = 7;
     public const int ORZ_GROUP_HARROWMONT = 8;
     public const int ORZ_GROUP_LEGION = 9;
     public const int ORZ_GROUP_HOSTILE = 6;
     public const int ORZ_GROUP_HOSTILE_DARKSPAWN = 15;
     public const int ORZ_GROUP_HOSTILE_DEEPSTALKERS = 16;
     public const int ORZ_GROUP_HOSTILE_SPIDERS = 17;

     public const string ORZ_RESOURCE_SCRIPT_AREA_CORE = "orzar_core.nss";

     //==============================================================================
     // Creature Teams
     //------------------------------------------------------------------------------

     public const int ORZ_TEAM_PROHARROW_SUPPORTERS = 1;
     public const int ORZ_TEAM_PROBHELEN_SUPPORTERS = 2;
     public const int ORZ_TEAM_CARIDINS_CROSS_AMBUSH = 3;
     public const int ORZ_TEAM_DACE = 4;
     public const int ORZ_TEAM_DACE_AMBUSHERS_1 = 5;
     public const int ORZ_TEAM_DACE_AMBUSHERS_2 = 6;
     public const int ORZ_TEAM_CARTA_THUGS = 7;
     public const int ORZ_TEAM_CARTA_JARVIA = 8;
     public const int ORZ_TEAM_CARTA_ROGGAR = 9;
     public const int ORZ_TEAM_KARDOL = 10;
     public const int ORZ_TEAM_KARDOL_AMBUSHERS = 11;
     public const int ORZ_TEAM_DS_HUNTING_PARTY = 12;
     public const int ORZ_TEAM_AOTV_TRAP_1 = 13;
     public const int ORZ_TEAM_AOTV_TRAP_2 = 14;
     public const int ORZ_TEAM_AOTV_TRAP_3 = 15;
     public const int ORZ_TEAM_CARIDIN_GOLEMS = 16;
     public const int ORZ_TEAM_CARTA_HOME_AMBUSHERS = 17;
     public const int ORZ_TEAM_ROGEK = 18;
     public const int ORZ_TEAM_BHELEN = 19;
     public const int ORZ_TEAM_JARVIA_SUPPORTERS = 20;
     public const int ORZ_TEAM_DEAD_TRENCHES_GENLOCK_WAVE = 21;
     public const int ORZ_TEAM_POST_PLOT_BHELEN_KING = 22;
     public const int ORZ_TEAM_POST_PLOT_HARROW_KING = 23;
     public const int ORZ_TEAM_POST_PLOT_DONE = 24;
     public const int ORZ_TEAM_ANVIL_LYRIUM_VEINS = 25;
     public const int ORZ_TEAM_IMREK = 26;
     public const int ORZ_TEAM_POST_PLOT_NOT_DONE = 27;
     public const int ORZ_TEAM_WARDEN_HUNTERS = 28;
     public const int ORZ_TEAM_SPIDERS_RUCK = 29;
     public const int ORZ_TEAM_KARDOL_DARKSPAWN_1 = 30;
     public const int ORZ_TEAM_KARDOL_DARKSPAWN_2 = 31;
     public const int ORZ_TEAM_BROODMOTHER_BLOCKER_PLC = 32;
     public const int ORZ_TEAM_RUCK = 33;
     public const int ORZ_TEAM_SPIDER_QUEEN_SPIDERLINGS = 34;
     public const int ORZ_TEAM_FORGE_GENLOCKS_1 = 35;
     public const int ORZ_TEAM_FORGE_GENLOCKS_2 = 36;
     public const int ORZ_TEAM_BROODMOTHER_TENTACLES = 37;
     public const int ORZ_TEAM_JARVIA_WAVE_1 = 38;
     public const int ORZ_TEAM_JARVIA_WAVE_2 = 39;
     public const int ORZ_TEAM_JARVIA_WAVE_3 = 40;
     public const int ORZ_TEAM_CARIDINS_CROSS_BRIDGE_AMBUSH = 41;
     public const int ORZ_TEAM_CARIDINS_CROSS_OGRE_AMBUSH = 42;
     public const int ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_1 = 43;
     public const int ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_2 = 44;
     public const int ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_3 = 45;
     public const int ORZ_TEAM_ANVIL_METAL_SHEET_RAMP_PLC = 46;
     public const int ORZ_TEAM_ANVIL_METAL_SHEET_TRAP_PLC = 47;
     public const int ORZ_TEAM_ASSEMBLY_DESHYRS_BHELEN = 48;
     public const int ORZ_TEAM_ASSEMBLY_DESHYRS_HARROW = 49;
     public const int ORZ_TEAM_BRANKA_DARKSPAWN_WAVE_1 = 50;
     public const int ORZ_TEAM_BRANKA_DARKSPAWN_WAVE_2 = 51;
     public const int ORZ_TEAM_BRANKA_DARKSPAWN_WAVE_3 = 52;
     public const int ORZ_TEAM_ESCAPED_NUGS = 53;
     public const int ORZ_TEAM_SLUMS_AMBUSH = 54;
     public const int ORZ_TEAM_PALACE_THIEVES = 55;
     public const int ORZ_TEAM_SHAPERATE_THIEF = 56;
     public const int ORZ_TEAM_SHAPERATE_THIEF_BOSS = 57;
     public const int ORZ_TEAM_HARROW_FANATIC_WAVE_1 = 58;
     public const int ORZ_TEAM_HARROW_FANATIC_WAVE_2 = 59;
     public const int ORZ_TEAM_HARROW_FANATIC_WAVE_3 = 60;
     public const int ORZ_TEAM_HARROW_FANATIC_WAVE_4 = 61;
     public const int ORZ_TEAM_BHELEN_FANATIC_WAVE_1 = 62;
     public const int ORZ_TEAM_BHELEN_FANATIC_WAVE_2 = 63;
     public const int ORZ_TEAM_BHELEN_FANATIC_WAVE_3 = 64;
     public const int ORZ_TEAM_BHELEN_FANATIC_WAVE_4 = 65;
     public const int ORZ_TEAM_PROVING_LITE_1 = 66;
     public const int ORZ_TEAM_PROVING_LITE_2 = 67;
     public const int ORZ_TEAM_PROVING_LITE_3 = 68;
     public const int ORZ_TEAM_PROVING_LITE_4 = 69;
     public const int ORZ_TEAM_CARIDIN_GOLEMS_2 = 70;
     public const int ORZ_TEAM_CARIDIN_GOLEMS_3 = 71;
     public const int ORZ_TEAM_AOTV_TRAP_3_SPIRITS = 72;
     public const int ORZ_TEAM_VARTAG_GUARDS = 73;
     public const int ORZ_TEAM_SPIDER_QUEEN_SPIDERLINGS_2 = 74;
     public const int ORZ_TEAM_BROODMOTHER_WAVE_1 = 75;
     public const int ORZ_TEAM_BROODMOTHER_WAVE_2 = 76;
     public const int ORZ_TEAM_BROODMOTHER_BLOCKER_TENTACLES = 77;
     public const int ORZ_TEAM_BROODMOTHER = 78;
     public const int ORZ_TEAM_KARDOL_DARKSPAWN_3 = 79;
     public const int ORZ_TEAM_OTRAN_THAIG_OGRE_SPIDERS_1 = 80;
     public const int ORZ_TEAM_OTRAN_THAIG_OGRE_SPIDERS_2 = 81;
     public const int ORZ_TEAM_OTRAN_THAIG_OGRE_1 = 82;
     public const int ORZ_TEAM_OTRAN_THAIG_OGRE_2 = 83;
     public const int ORZ_TEAM_RUCK_SPIDER_AMBUSH = 84;
     public const int ORZ_TEAM_SPIDER_QUEEN = 85;
     public const int ORZ_TEAM_OVERRIDE_GROUP_NEUTRAL = 86;
     public const int ORZ_TEAM_DEAD_TRENCHES_BRIDGE_MOVERS = 87;
     public const int ORZ_TEAM_DEAD_TRENCHES_BRIDGE_MOVERS_2 = 88;
     public const int ORZ_TEAM_KARDOL_DARKSPAWN_4 = 89;
     public const int ORZ_TEAM_LEGION_SPIRITS = 90;
     public const int ORZ_TEAM_AOTV_TRAP_3_COMBAT_HACK = 91;

     public const int LITE_TEAM_FITE_DESERTERS_2 = 101;

     public const int ORZ_TEAM_AMBASSADOR = 900;      // LC: For Denerim Assassination quest

     //==============================================================================
     // Area Lists
     //------------------------------------------------------------------------------

     public const string ORZ_AL_ENTRANCE = "orz01al_orzammar_entrance";
     public const string ORZ_AL_COMMONS = "orz02al_orzammar_commons";
     public const string ORZ_AL_NOBLES = "orz03al_orzammar_nobles";
     public const string ORZ_AL_SLUMS = "orz04al_orzammar_slums";
     public const string ORZ_AL_PROVING = "orz05al_orzammar_proving";
     public const string ORZ_AL_MINES = "orz06al_orzammar_mines";
     public const string ORZ_AL_ORTAN_TAIG = "orz07al_ortan_taig";
     public const string ORZ_AL_DEAD_TRENCHES = "orz08al_dead_trenches";
     public const string ORZ_AL_SHALE_TAIG = "orz09al_shale_taig";

     //==============================================================================
     // Areas
     //------------------------------------------------------------------------------

     public const string ORZ_AR_MOUNTAIN_PASS = "orz100ar_mountain_pass";
     public const string ORZ_AR_HALL_OF_HEROES = "orz110ar_hall_of_heroes";
     public const string ORZ_AR_COMMONS = "orz200ar_commons";
     public const string ORZ_AR_TAPSTERS = "orz210ar_tapsters";
     public const string ORZ_AR_COMMONS_SHOP = "orz220ar_shop";
     public const string ORZ_AR_GANGSTERS_HIDEOUT = "orz230ar_gangsters_hideout";
     public const string ORZ_AR_GANGSTERS_SHOP = "orz240ar_gangsters_shop";
     public const string ORZ_AR_CHANTRY = "orz250ar_chantry";
     public const string ORZ_AR_PROVING = "orz260ar_proving";
     public const string ORZ_AR_FIGHTERS_QUARTERS = "orz261ar_fighters_quarters";
     public const string ORZ_AR_NOBLES_QUARTER = "orz300ar_nobles_quarter";
     public const string ORZ_AR_SHAPERATE = "orz310ar_shaperate";
     public const string ORZ_AR_ROYAL_PALACE = "orz320ar_royal_palace";
     public const string ORZ_AR_HARROWMONTS_ESTATE = "orz330ar_harrowmonts_estate";
     public const string ORZ_AR_ASSEMBLY = "orz340ar_assembly";
     public const string ORZ_AR_NOBLE_ESTATE_1 = "orz350ar_noble_estate_1";
     public const string ORZ_AR_NOBLE_ESTATE_2 = "orz360ar_noble_estate_2";
     public const string ORZ_AR_SLUMS = "orz400ar_slums";
     public const string ORZ_AR_PLAYERS_HOME = "orz410ar_players_home";
     public const string ORZ_AR_SLUMS_SHOP = "orz420ar_shop";
     public const string ORZ_AR_SLUMS_GENERIC_HOUSE = "orz430ar_generic_house";
     public const string ORZ_AR_MINES = "orz200ar_commons";
     public const string ORZ_AR_CARIDINS_CROSS = "orz510ar_caridins_cross";
     public const string ORZ_AR_DACE_ENCOUNTER = "orz520ar_aeducan_thaig";
     public const string ORZ_AR_ORTAN_TAIG = "orz530ar_ortan_thaig";
     public const string ORZ_AR_ANVIL_OF_THE_VOID = "orz540ar_anvil_of_the_public void";
     public const string ORZ_AR_DEAD_TRENCHES = "orz550ar_dead_trenches";
     public const string ORZ_AR_SHALE_TAIG = "orz560ar_shale_taig";
     public const string ORZ_AR_DEEP_ROAD_OUTSKIRTS = "orz570ar_deeproad_outskirts";
     public const string ORZ_AR_STEALING_INFAMY_ENCOUNTER = "ran510ar_stealing_infamy";

     //------------------------------------------------------------------------------
     // Creatures
     //------------------------------------------------------------------------------

     public const string ORZ_CR_EXIT_GUARD = "orz100cr_exitguard";
     public const string ORZ_CR_IMREK = "orz100cr_imrek";
     public const string ORZ_CR_KNIGHT_ESCORT = "orz100cr_knight_escort";
     public const string ORZ_CR_MAGE_ESCORT = "orz100cr_mage_escort";
     public const string ORZ_CR_FARYN = "orz100cr_faryn"; //Part of Sten's follower plot

     public const string ORZ_CR_BRANKA_WOMAN = "orz110cr_brankawoman";
     public const string ORZ_CR_BRANKA_GIRL = "orz110cr_brankagirl";
     public const string ORZ_CR_HOH_SCHOLAR = "orz110cr_scholar";
     public const string ORZ_CR_HOH_GUARD_PATROL_1 = "orz110cr_guard_patrol_1";
     public const string ORZ_CR_HOH_GUARD_PATROL_2 = "orz110cr_guard_patrol_2";
     public const string ORZ_CR_HOH_GUARD_WELCOME = "orz110cr_guard_welcome";
     public const string ORZ_CR_HOH_BEGGAR = "orz110cr_beggar";

     public const string ORZ_CR_ARMS_MERCHANT = "orz200cr_armsmerc";
     public const string ORZ_CR_BURKEL = "orz200cr_burkel";
     public const string ORZ_CR_COMMONS_AMBIENT_1 = "orz200cr_ambient1";
     public const string ORZ_CR_COMMONS_AMBIENT_2 = "orz200cr_ambient2";
     public const string ORZ_CR_COMMONS_GUARD = "orz200cr_guard";
     public const string ORZ_CR_DAGNA = "orz200cr_dagna";
     public const string ORZ_CR_GARIN = "orz200cr_garin";
     public const string ORZ_CR_GUARDSMAN = "orz200cr_guardsman";
     public const string ORZ_CR_FIGALE = "orz200cr_figor";
     public const string ORZ_CR_JANAR = "orz200cr_janar";
     public const string ORZ_CR_LEGNAR = "orz200cr_legnar";
     public const string ORZ_CR_NERAV = "orz200cr_nerav";
     public const string ORZ_CR_PRO_BHELEN_GUARD = "orz200cr_probhelenguard";
     public const string ORZ_CR_PRO_HARROW_GUARD = "orz200cr_proharrowguard";
     public const string ORZ_CR_ROGGAR = "orz200cr_roggar";
     public const string ORZ_CR_ROGGAR_THUG = "orz200cr_roggarthug";
     public const string ORZ_CR_FILDA = "orz200cr_filda";
     public const string ORZ_CR_COMMANDER = "orz200cr_commander";
     public const string ORZ_CR_MINES_SOLDIER = "orz200cr_soldier_1";
     public const string ORZ_CR_NUG_WRANGLER = "orz200cr_nug_wrangler";
     public const string ORZ_CR_ESCAPED_NUG = "orzcr_escaped_nug";
     public const string ORZ_CR_ESCAPED_BRONTO = "orz200cr_bronto";

     public const string ORZ_CR_TAPSTERS_AMBIENT_1 = "orz210cr_ambient1";
     public const string ORZ_CR_TAPSTERS_AMBIENT_2 = "orz210cr_ambient2";
     public const string ORZ_CR_CORRA = "orz210cr_corra";
     public const string ORZ_CR_LORD_HELMI = "orz210cr_lordhelmi";
     public const string ORZ_CR_NEVIN = "orz210cr_nevin";
     public const string ORZ_CR_ORDEL = "orz210cr_ordel";
     public const string ORZ_CR_TAPSTERS_SINGER_1 = "orz210cr_singer1";
     public const string ORZ_CR_TAPSTERS_SINGER_2 = "orz210cr_singer2";
     public const string ORZ_CR_TAPSTERS_SINGER_3 = "orz210cr_singer3";
     public const string ORZ_CR_TAPSTERS_SINGER_4 = "orz210cr_singer4";
     public const string ORZ_CR_WAIT_STAFF = "orz210cr_waitstaff";

     public const string ORZ_CR_JARVIA = "orz230cr_jarvia";
     public const string ORZ_CR_HEAD_THUG = "orz230cr_headthug";

     public const string ORZ_CR_WORSHIPPER = "orz250cr_worshipper_1";

     public const string ORZ_CR_BAIZYL = "orz260cr_baizyl";
     public const string ORZ_CR_CONATH = "orz260cr_conath";
     public const string ORZ_CR_DAMEK = "orz260cr_damek";
     public const string ORZ_CR_DARVIANAK = "orz260cr_darvianak";
     public const string ORZ_CR_FARINDEN = "orz260cr_farinden";
     public const string ORZ_CR_FIGHTFAN = "orz260cr_fightfan";
     public const string ORZ_CR_GWIDDON = "orz260cr_gwiddon";
     public const string ORZ_CR_HANASHAN = "orz260cr_hanashan";
     public const string ORZ_CR_LUCJAN = "orz260cr_lucjan";
     public const string ORZ_CR_MARJA = "orz260cr_myaja";
     public const string ORZ_CR_OLANIV = "orz260cr_olaniv";
     public const string ORZ_CR_PIOTIN = "orz260cr_piotin";
     public const string ORZ_CR_PIOTINS_HENCHMAN_1 = "orz260cr_piotinhench1";
     public const string ORZ_CR_PIOTINS_HENCHMAN_2 = "orz260cr_piotinhench2";
     public const string ORZ_CR_PIOTINS_HENCHMAN_3 = "orz260cr_piotinhench3";
     public const string ORZ_CR_PROVAMB = "orz260cr_provamb";
     public const string ORZ_CR_PROVMASTER = "orz260cr_provmaster";
     public const string ORZ_CR_ROSHEN = "orz260cr_roshen";
     public const string ORZ_CR_SEWERYN = "orz260cr_seweryn";
     public const string ORZ_CR_VARICK = "orz260cr_varick";
     public const string ORZ_CR_VELANZ = "orz260cr_velanz";
     public const string ORZ_CR_WOJECH = "orz260cr_wojech";
     public const string ORZ_CR_FENCE = "orz260cr_fence";
     public const string ORZ_CR_THIEF_BOSS = "orz260cr_thief_boss";
     public const string ZZ_ORZ_CR_FIGHT_GUY = "zz_orz260cr_fight_guy";

     public const string ORZ_CR_BHELEN_SUPPORTER_1 = "orz300cr_bhelensupporter1";
     public const string ORZ_CR_BHELEN_SUPPORTER_2 = "orz300cr_bhelensupporter2";
     public const string ORZ_CR_HARROW_SUPPORTER_1 = "orz300cr_harrowsupporter1";
     public const string ORZ_CR_HARROW_SUPPORTER_2 = "orz300cr_harrowsupporter2";
     public const string ORZ_CR_LADY_DACE = "orz300cr_ladydace";
     public const string ORZ_CR_LOILINAR = "orz300cr_loilinar";
     public const string ORZ_CR_NOBLES_AMBIENT = "orz300cr_nobambient";
     public const string ORZ_CR_MARDY = "orz300cr_mardy";
     public const string ORZ_CR_TELI = "orz300cr_teli";
     public const string ORZ_CR_OGHREN = GEN_FL_OGHREN; // temp
     public const string ORZ_CR_RICA = "orz300cr_rica";
     public const string ORZ_CR_NOBLEBIYATCH = "orz300cr_noblebitch";
     public const string ORZ_CR_HOPEFULENOBLE_1 = "orz300cr_noblehopeful1";
     public const string ORZ_CR_HOPEFULENOBLE_2 = "orz300cr_noblehopeful2";
     public const string ORZ_CR_CRIER_BHELEN = "orz300cr_crier_bhelen";
     public const string ORZ_CR_CRIER_HARROW = "orz300cr_crier_harrowmont";

     public const string ORZ_CR_SHAPER = "orz310cr_shaper";
     public const string ORZ_CR_ORTA = "orz310cr_orta";

     public const string ORZ_CR_BHELEN = "orz320cr_bhelen";
     public const string ORZ_CR_BHELEN_GUARD = "orz320cr_bhelenguard";
     public const string ORZ_CR_BHELEN_AMB = "orz320cr_estatebamb";
     public const string ORZ_CR_MOTHER = "orz320cr_mother";
     public const string ORZ_CR_TUNNELING_THIEF = "orz320cr_thief";
     public const string ORZ_CR_TUNNELING_THIEF_GUARD = "orz320cr_tt_guard";
     public const string ORZ_CR_BHELEN_ESTATE_GUARD = "orz320cr_bhelenguard_estate";

     public const string ORZ_CR_BUTLER = "orz330cr_butler";
     public const string ORZ_CR_DULIN = "orz330cr_dulin";
     public const string ORZ_CR_HARROWMONT_AMB = "orz330cr_estatehamb";
     public const string ORZ_CR_HARROWMONT_GUARD = "orz330cr_harrowguard";
     public const string ORZ_CR_HARROWMONT = "orz330cr_harrowmont";
     public const string ORZ_CR_TERCY = "orz330cr_tercy";

     public const string ORZ_CR_ASSEMBLY_GUARD = "orz340cr_guard";
     public const string ORZ_CR_PRO_BHELEN_DESHYR_1 = "orz340cr_phdeshamb";
     public const string ORZ_CR_PRO_BHELEN_DESHYR_2 = "orz340cr_phdeshamb2";
     public const string ORZ_CR_PRO_HARROW_DESHYR_1 = "orz340cr_pbdeshamb";
     public const string ORZ_CR_PRO_HARROW_DESHYR_2 = "orz340cr_pbdeshamb2";
     public const string ORZ_CR_STEWARD = "orz340cr_steward";
     public const string ORZ_CR_VARTAG = "orz340cr_vartag";

     public const string ORZ_CR_ALIMAR = "orz420cr_alimar";
     public const string ORZ_CR_JARVIA_THUG = "orz400cr_jarviathug";
     public const string ORZ_CR_LESKE = "orz400cr_leske";
     public const string ORZ_CR_NADEZDA = "orz400cr_nadezda";
     public const string ORZ_CR_PATROL = "orz400cr_patrol";
     public const string ORZ_CR_ROGEK = "orz400cr_rogek";
     public const string ORZ_CR_ROGEK_THUG = "orz400cr_rogekthug";
     public const string ORZ_CR_SLUMS_AMBIENT = "orz400cr_slumsamb";
     public const string ORZ_CR_ZERLINDA = "orz400cr_zerlinda";
     public const string ORZ_CR_JARVIA_SUPPORTER = "orz400cr_jarviasupporter";
     public const string ORZ_CR_SLUMS_THUG_1 = "orz400cr_thug1";
     public const string ORZ_CR_SLUMS_THUG_2 = "orz400cr_thug2";
     public const string ORZ_CR_NUGG_KID = "orz400cr_nugcatcher";

     public const string ORZ_CR_CARTA_HOME_AMBUSHER = "orz410cr_jarviathug";

     public const string ORZ_CR_AMBUSHER = "orz510cr_ambusher";
     public const string ORZ_CR_AMBUSHER_LEADER = "orz510cr_ambusher_leader";
     public const string ORZ_CR_KARDOL = "orz550cr_kardol";
     public const string ORZ_CR_LEGIONNAIRE = "orz510cr_legionnair";

     public const string ORZ_CR_LORD_DACE = "orz520cr_lord_dace";
     public const string ORZ_CR_DACEMAN = "orz520cr_dacemen";
     public const string ORZ_CR_DARKSPAWN_CAT = "orz520cr_darkspawncat";
     public const string ORZ_CR_DACE_AMBUSHER_INIT = "orz520cr_dace_ambusher_01";
     public const string ORZ_CR_DACE_AMBUSHER = "orz530wp_dace_ambusher";

     public const string ORZ_CR_RUCK = "orz530cr_ruck";
     public const string ORZ_CR_QUEEN_SPIDERLING = "orz530cr_spider_summoned";
     public const string ORZ_CR_QUEEN_SPIDER = "orz530cr_spider_queen";

     public const string ORZ_CR_BRANKA = "orz540cr_branka";
     public const string ORZ_CR_CARIDIN = "orz540cr_caridin";
     public const string ORZ_CR_BRANKA_CLONE = "orz540cr_branka_clone";
     public const string ORZ_CR_GOLEM_ENCASED = "orz540cr_golem_encased";

     public const string ORZ_CR_HESPITH = "orz550cr_hespith";
     public const string ORZ_CR_HESPITH_AMB = "orz550cr_hespith_amb";
     public const string ORZ_CR_GENLOCK_ALPHA = "orz550cr_genlock_alpha";
     public const string ORZ_CR_GENLOCK_EMISSARY = "orz550cr_genlock_emissary";
     public const string ORZ_CR_BROODMOTHER = "orz550cr_broodmother";
     public const string ORZ_CR_GANGUE_SHADE = "orz550cr_gangue_shade";
     public const string ORZ_CR_TOPSIDER_DARKSPAWN = "orz550cr_topsider_darkspawn";

     public const string ORZ_CR_GERIENT_DOG = "orz100cr_gerient_dog";

     //light content creatures
     public const string ORZ_CR_REVENANT_PALACE = "orz320cr_lt_revenant";
     public const string ORZ_CR_REVENANT_CARIDINS = "orz510cr_lt_revenant";

     public const string ORZ_CR_CAGED_DRAGON = "orz320cr_lt_dragon";

     public const string ORZ_CR_ASSEMBLY_BEAST = "lite_orz_fade_beast";

     //------------------------------------------------------------------------------
     // Waypoints
     //------------------------------------------------------------------------------

     public const string ORZ_WP_ENTGUARD_REJECTED = "orz100wp_entguard_rejected";
     public const string ORZ_WP_IMREK_FIGHT = "orz100wp_imrek_fight";
     public const string ORZ_WP_MAGE_ESCORT_FIGHT = "orz100wp_mage_escort_fight";
     public const string ORZ_WP_KNIGHT_ESCORT_FIGHT = "orz100wp_knight_escort_fight";
     public const string ORZ_WP_PLAYER_IMREK_FIGHT = "orz110wp_player_imrek_fight";
     public const string ORZ_WP_GUARDSMAN_MOVETO = "orz100wp_guardsman_moveto";

     public const string ORZ_WP_HALL_ENTRANCE = "orz110wp_from_mountain_pass";

     public const string ORZ_WP_DAGNA_MOVETO = "orz200wp_dagna_moveto";
     public const string ORZ_WP_LEGNAR_MOVETO = "orz200wp_legnar_moveto";
     public const string ORZ_WP_ESCAPED_NUG = "orz200wp_nug_escape";
     public const string ORZ_WP_ESCAPED_BRONTO = "orz200wp_bronto_attack";

     public const string ORZ_WP_RETURN_FROM_DEEP_ROADS = "wmw_orz_commons";

     public const string ORZ_WP_FIGOR_MERCH = "mn_merch_figor";

     public const string ORZ_WP_CARTA_HIDEOUT = "mn_carta_hideout";

     public const string ORZ_WP_ENTER_HIDEOUT = "orz230wp_from_slums";
     public const string ORZ_WP_LESKE_CELL = "orz230wp_leske_cell";

     public const string ORZ_WP_PROVING_PC_START = "orz260wp_pc_start";
     public const string ORZ_WP_PROVING_NPC_START = "orz260wp_npc_start";
     public const string ORZ_WP_PROVING_EXIT = "orz260wp_proving_exit";
     public const string ORZ_WP_PROVMASTER_1 = "orz260wp_provmaster_1";
     public const string ORZ_WP_PROVMASTER_2 = "orz260wp_provmaster_2";
     public const string ORZ_WP_TWINS_LEAVE = "orz260wp_twins_move";
     public const string ORZ_WP_MYAJA_HOME = "orz260wp_myaja_home";
     public const string ORZ_WP_LUCJAN_HOME = "orz260wp_lucjan_home";
     public const string ORZ_WP_MYAJA_MOVETO = "orz260wp_myaja_moveto";
     public const string ORZ_WP_LUCJAN_MOVETO = "orz260wp_lucjan_moveto";
     public const string ORZ_WP_BAIZYL = "orz260wp_baizyl";
     public const string ORZ_WP_GWIDDON = "orz260wp_gwiddon";

     public const string ORZ_WP_DACE_TP = "orz300wp_dace_tp";
     public const string ORZ_WP_KARDOL_BHELEN = "orz300wp_kardol_bhelen";

     public const string ORZ_WP_TELEPORT_TO_BHELEN = "orz320wp_teleport_to_bhelen";
     public const string ORZ_WP_ROYAL_PALACE = "orz320wp_from_nobles_quarter";
     public const string ORZ_WP_BHELEN_KING = "orz320wp_bhelen_king";
     public const string ORZ_WP_VARTAG_MOVETO = "orz320wp_vartag_moveto";
     public const string ORZ_WP_GAURD_MOVETO = "orz320wp_tunneler_gaurd";
     public const string ORZ_WP_PALACE_LINE_AMB_MOVETO = "orz320wp_amb_moveto";

     public const string ORZ_WP_TELEPORT_TO_HARROW = "orz330wp_teleport_to_harrow";
     public const string ORZ_WP_HARROW_ESTATE = "orz330wp_from_nobles_quarter";
     public const string ORZ_WP_DULIN_MOVETO = "orz330wp_dulin_moveto";

     public const string ORZ_WP_ASSEMBLY_CENTER = "orz340wp_center";
     public const string ORZ_WP_VARTAG_TP = "orz340wp_vartag_tp";
     public const string ORZ_WP_ASSEMBLY_ENT = "orz340wp_from_nobles_quarter";
     public const string ORZ_WP_ASSEMBLY_CLOSED_VARTAG = "orz340wp_vartag_assembly_closed";
     public const string ORZ_WP_ASSEMBLY_CLOSED_STEWARD = "orz340wp_steward_assembly_closed";
     public const string ORZ_WP_ASSEMBLY_CLOSED_RICA = "orz340wp_rica_assembly_closed";
     public const string ORZ_WP_ASSEMBLY_CLOSED_PC = "orz340wp_pc_assembly_closed";
     public const string ORZ_WP_ASSEMBLY_STEWARD_POST_INTRO = "orz340wp_steward_after_intro";
     public const string ORZ_WP_ASSEMBLY_STEWARD_FINAL_SCENE = "orz340wp_steward_final_scene";
     public const string ORZ_WP_ASSEMBLY_PC_POST_INTRO = "orz340wp_pc_after_intro";
     public const string ORZ_WP_ASSEMBLY_PC_FINAL_SCENE = "orz340wp_pc_final_scene";
     public const string ORZ_WP_ASSEMBLY_DESHYR_MOVE_1 = "orz340wp_deshyr_move_1";
     public const string ORZ_WP_ASSEMBLY_DESHYR_MOVE_2 = "orz340wp_deshyr_move_2";

     public const string ORZ_WP_PATROL_1 = "orz400wp_patrol_1";
     public const string ORZ_WP_PATROL_2 = "orz400wp_patrol_2";
     public const string ORZ_WP_PATROL_3 = "orz400wp_patrol_3";
     public const string ORZ_WP_PATROL_4 = "orz400wp_patrol_4";
     public const string ORZ_WP_PATROL_5 = "orz400wp_patrol_5";
     public const string ORZ_WP_ROGEK_MOVETO = "orz400wp_rogek_moveto";

     public const string ORZ_WP_DEEP_ROADS_RETURN = "orz500wp_from_caridins_cross";
     public const string ORZ_WP_COMMANDER_DELAY = "orz500wp_commander_delay";
     public const string ORZ_WP_OGHREN_DELAY_MOVETO = "orz500wp_oghren_delay_pc_moveto";
     public const string ORZ_WP_OGHREN_DELAY = "orz500wp_oghren_delay";

     public const string ORZ_WP_GREASE_SPAWN = "orz510wp_grease_spawn";
     public const string ORZ_WP_WMW_CARIDINS_WEST = "wmw_caridins_west";

     public const string ORZ_WP_RUCK_MOVETO = "orz530wp_ruck_moveto";
     public const string ORZ_WP_QUEEN_RESET = "orz530wp_queen_reset";
     public const string ORZ_WP_QUEEN_SPIDERLING = "orz530wp_queen_spiderling";

     public const string ORZ_WP_BRANKA_TRAP_ROOM = "orz540wp_branka_traproom";
     public const string ORZ_WP_BRANKA_CARIDIN = "orz540wp_branka_caridin";
     public const string ORZ_WP_OGHREN_TRAPPED = "orz540wp_oghren_trapped";
     public const string ORZ_WP_OGHREN_LOCKED_OUT = "orz540wp_oghren_lockedout";
     public const string ORZ_WP_OGHREN_CARIDIN = "orz540wp_oghren_caridin";
     public const string ORZ_WP_BRANKA_DARKSPAWN = "orz540wp_branka_darkspawn";
     public const string ORZ_WP_BRANKA_POSTFIGHT = "orz540wp_branka_postfight";
     public const string ORZ_WP_BRANKA_ANVIL = "orz540wp_branka_anvil";
     public const string ORZ_WP_ANVIL_TRAP_1_GAS = "orz540wp_trap_1_gas";
     public const string ORZ_WP_ANVIL_TRAP_1_AOE = "orz540wp_trap_1_gas_aoe";
     public const string ORZ_WP_ANVIL_TRAP_3_PARTYJUMP = "orz540wp_spirit_head_postcs";

     public const string ORZ_WP_BOWNAM_KARDOL_POST = "orz550wp_kardol_post";
     public const string ORZ_WP_BOWNAM_KARDOL_RETAKE = "orz550wp_kardol_retake";
     public const string ORZ_WP_BOWNAM_LEGO_POST = "orz550wp_lego_post";
     public const string ORZ_WP_BOWNAM_LEGO_RETAKE = "orz550wp_lego_retake";
     public const string ORZ_WP_OGHREN_BROODMOTHER = "orz550wp_oghren_broodmother";
     public const string ORZ_WP_PC_BROODMOTHER = "orz550wp_pc_broodmother";
     public const string ORZ_WP_BROODMOTHER_WAVE = "orz550wp_wave_moveto";
     public const string ORZ_WP_MAP_EVENT_KARDOL = "orz550wp_map_event_kardol";
     public const string ORZ_WP_LEGION_ATTACK = "orz550wp_legion_attack";
     public const string ORZ_WP_GANGUE_SHADE = "orz550wp_gangue_shade";
     public const string ORZ_WP_HESPITH_RUN = "orz550wp_hespith_run";
     public const string ORZ_WP_HESPITH_RUN_FROM = "orz550wp_hespith_run_from";
     public const string ORZ_WP_HESPITH_AMBIENT_1 = "orz550wp_hespith_1";
     public const string ORZ_WP_HESPITH_AMBIENT_2 = "orz550wp_hespith_2";
     public const string ORZ_WP_HESPITH_AMBIENT_3 = "orz550wp_hespith_3";
     public const string ORZ_WP_HESPITH_AMBIENT_4 = "orz550wp_hespith_4";
     public const string ORZ_WP_HESPITH_AMBIENT_5 = "orz550wp_hespith_5";
     public const string ORZ_WP_HESPITH_AMBIENT_6 = "orz550wp_hespith_6";
     public const string ORZ_WP_HESPITH_AMBIENT_7 = "orz550wp_hespith_7";

     public const string ORZ_WP_CAGED_THRONEROOM = "orz320wp_caged_throneroom";

     //------------------------------------------------------------------------------
     // Triggers
     //------------------------------------------------------------------------------

     public const string ORZ_TR_TO_HALL_OF_HEROES = "orz100tr_to_hall_of_heroes";

     public const string ORZ_TR_CRIER_HARROW = "orz300tr_crier_harrowmont";
     public const string ORZ_TR_CRIER_BHELEN = "orz300tr_crier_bhelen";

     public const string ORZ_TR_LT_CAGED_1 = "orz320tr_lt_caged_1";
     public const string ORZ_TR_LT_CAGED_2 = "orz320tr_lt_caged_2";
     public const string ORZ_TR_LT_CAGED_3 = "orz320tr_lt_caged_3";

     //------------------------------------------------------------------------------
     // Placeables
     //------------------------------------------------------------------------------

     public const string ORZ_IP_ORZAMMAR_ENTRANCE = "orz100ip_to_hall_of_heroes";

     public const string ORZ_IP_CHANTRY_DOOR = "orz200ip_to_chantry";
     public const string ORZ_IP_OLD_CHANTRY_DOOR = "orz200ip_old_chantry";
     public const string ORZ_IP_COMMONS_SHOP_DOOR = "orz200ip_to_shop";
     public const string ORZ_IP_CODEX_SHAPER_1 = "orz200ip_codex_shaper_1";

     public const string ORZ_IP_TRIAN_LETTERS = "orz230ip_trianletters";
     public const string ORZ_IP_JARVIA_CHEST = "orz230ip_jarvia_chest";

     public const string ORZ_IP_GANGSTER_SHOP_SHELF_1 = "orz240ip_shelf_1";
     public const string ORZ_IP_GANGSTER_SHOP_SHELF_2 = "orz240ip_shelf_2";
     public const string ORZ_IP_HIDEOUT_HIDDEN_TRANS = "orz240ip_to_gangsters_hdout";

     public const string ORZ_IP_TO_FIGHTERS_1 = "orz260ip_to_fighters_1";
     public const string ORZ_IP_TO_FIGHTERS_2 = "orz260ip_to_fighters_2";

     public const string ORZ_IP_BLACKMAIL_DOOR = "orz261ip_quarters_door";
     public const string ORZ_IP_BLACKMAIL_LETTERS = "orz261ip_blackmail";

     public const string ORZ_IP_MEMORIES = "orz310ip_memories";
     public const string ORZ_IP_CODEX_SHAPER_0 = "orz310ip_codex_shaper_0";
     public const string ORZ_IP_CODEX_SHAPER_4 = "orz310ip_codex_shaper_4";

     public const string ORZ_IP_ROYAL_ESTATE_DOOR = "orz320ip_estate_door";
     public const string ORZ_IP_TUNNELING_THIEF_EPICENTER = "orz320ip_epicenter";
     public const string ORZ_IP_ROYAL_FEAST = "genip_dwarven_feast";

     public const string ORZ_IP_HARROW_DOOR = "orz330ip_harrow_door";

     public const string ORZ_IP_ASSEMBLY_BLOCKER = "orz340ip_assembly_blocker";
     public const string ORZ_IP_ASSEMBLY_DOOR = "orz340ip_assembly_door";
     public const string ORZ_IP_KEY_CACHE = "orz340ip_key_cache";

     public const string ORZ_IP_CARTA_DOOR = "orz400ip_carta_door";
     public const string ORZ_IP_CARTA_DOOR_TALKER = "orz400ip_carta_door_talker";

     public const string ORZ_IP_TRANS_TO_DEAD_TRENCHES = "wmt_und_cc_to_trenches";

     public const string ORZ_IP_CODEX_SHAPER_2 = "orz510ip_codex_shaper_2";

     public const string ORZ_IP_TOPSIDERS_CORPSE = "orz520ip_topsiders_corpse";

     public const string ORZ_IP_BRANKA_JOURNAL = "orz530ip_branka_journal";
     public const string ORZ_IP_ORTAN_RECORDS = "orz530ip_orta_records";

     public const string ORZ_IP_ANVIL_OF_THE_VOID = "orz540ip_anvil";

     public const string ORZ_IP_LEGION_ALTER = "orz550ip_legion_alter";
     public const string ORZ_IP_CODEX_SHAPER_3 = "orz550ip_codex_shaper_3";

     public const string ORZ_IP_APPARATUS = "orz540ip_apparatus";
     public const string ORZ_IP_GAS_VALVE_1 = "orz540ip_gas_valve_1";
     public const string ORZ_IP_GAS_VALVE_2 = "orz540ip_gas_valve_2";
     public const string ORZ_IP_TRAP_DOOR_1 = "orz540ip_trap_door_1";
     public const string ORZ_IP_TRAP_DOOR_2 = "orz540ip_trap_door_2";
     public const string ORZ_IP_TRAP_DOOR_3 = "orz540ip_trap_door_3";
     public const string ORZ_IP_LEGION_FINAL = "orz550ip_legion_final";

     public const string ORZ_IP_CARTA_SHELF_1 = "orz220ip_shelf_1";
     public const string ORZ_IP_CARTA_SHELF_2 = "orz220ip_shelf_2";
     public const string ORZ_IP_JARVIA_TRAP = "genip_trap_explosion";
     public const string ORZ_IP_JARVIA_TRAP_TARGET = "genip_trap_explosion_target";
     public const string ORZ_IP_JARVIA_ENT_DOOR = "orz230ip_jarvia_ent_door";

     public const string ORZ_IP_VIALS_PALACE_PHYLACTERY = "orz320ip_lt_phylactery";
     public const string ORZ_IP_VIALS_CARIDIN_PHYLACTERY = "orz510ip_lt_phylactery";

     public const string ORZ_IP_CAGED_THRONE = "orz320ip_lt_throne";
     public const string ORZ_IP_DOOR_THRONE1 = "orz320ip_door_throne1";
     public const string ORZ_IP_DOOR_THRONE2 = "orz320ip_door_throne2";

     public const string ORZ_IP_LT_ASSEMBLY_ALTAR = "orzltip_asunder_altar";

     //------------------------------------------------------------------------------
     // Plot Items -- Resources
     //------------------------------------------------------------------------------

     public const string ORZ_IM_IMREKS_LETTER_R = "orz100im_imrek_letter.uti";

     public const string ORZ_IM_DAGNAS_LETTER_R = "orz200im_dagna_letter.uti";
     public const string ORZ_IM_NUG_R = "orz200im_nug.uti";

     public const string ORZ_IM_TRIAN_EVIDENCE_R = "orz230im_trian_evidence.uti";

     public const string ORZ_IM_BAIZYL_LETTERS_R = "orz260im_baizyl_letters.uti";

     public const string ORZ_IM_DACE_RING_R = "orz300im_dace_ring.uti";
     public const string ORZ_IM_DACE_MAP_R = "orz300im_dace_map.uti";

     public const string ORZ_IM_STOLEN_TOME_R = "orz310im_stolen_tome.uti";
     public const string ORZ_IM_PROVING_RECEIPT_R = "orz310im_proving_receipt.uti";

     public const string ORZ_IM_PLANTED_PAPERS_R = "orz340im_planted_papers.uti";

     public const string ORZ_IM_LETTER_DACE_R = "orz340im_letter_dace.uti";
     public const string ORZ_IM_LETTER_HELMI_R = "orz340im_letter_helmi.uti";

     public const string ORZ_IM_ROGEK_LYRIUM_R = "orz400im_rogek_lyrium.uti";

     public const string ORZ_IM_CARTA_KEY_R = "orz410im_carta_key.uti";

     public const string ORZ_IM_TOPSIDER_SWORD_R = "orz510im_topsider_sword.uti";

     public const string ORZ_IM_BRANKA_JOURNAL_R = "orz530im_branka_journal.uti";
     public const string ORZ_IM_ORTAN_RECORDS_R = "orz530im_ortan_records.uti";

     public const string ORZ_IM_REGISTRY_TRACING_R = "orz540im_registry_tracing.uti";
     public const string ORZ_IM_KINGS_CROWN_R = "orz540im_kings_crown.uti";

     public const string ORZ_IM_LEGION_KEY_R = "orz550im_legion_key.uti";
     public const string ORZ_IM_LEGION_INSIGNIA_R = "orz550im_legion_insignia.uti";

     public const string ORZ_IM_DESHYR_STAFF_R = "gen_im_wep_mag_sta_har.uti";

     //------------------------------------------------------------------------------
     // Plot Items -- Tags
     //------------------------------------------------------------------------------

     public const string ORZ_IM_LEGION_ARMOR = "gen_im_arm_cht_mas_leg";
     public const string ORZ_IM_LEGION_HELMET = "gen_im_arm_hel_hvy_leg";
     public const string ORZ_IM_LEGION_GLOVES = "gen_im_arm_glv_mas_leg";
     public const string ORZ_IM_LEGION_BOOTS = "gen_im_arm_bot_mas_leg";

     public const string ORZ_IM_TOPSIDER_HILT = "orz510im_topsider_hilt";
     public const string ORZ_IM_TOPSIDER_POMMEL = "orz510im_topsider_pommel";
     public const string ORZ_IM_TOPSIDER_BLADE = "orz510im_topsider_blade";
     public const string ORZ_IM_TOPSIDER_SWORD = "orz510im_topsider_sword";

     public const string ORZ_IM_KINGS_CROWN_EQUIP = "gen_im_arm_hel_lgt_crn";

     public const string ORZ_IM_LEGION_INSIGNIA = "orz550im_legion_insignia";

     public const string ORZ_IM_ROGEK_LYRIUM = "orz400im_rogek_lyrium";

     public const string ORZ_IM_REV_NOTE1 = "orz320im_rev_note";
     public const string ORZ_IM_REV_NOTE2 = "orz510im_rev_note";

     public const string ORZ_IM_LT_ASSEMBLY_1 = "orz_lite_bodypart1";
     public const string ORZ_IM_LT_ASSEMBLY_2 = "orz_lite_bodypart2";
     public const string ORZ_IM_LT_ASSEMBLY_3 = "orz_lite_bodypart3";

     //------------------------------------------------------------------------------
     // Map Notes
     //------------------------------------------------------------------------------

     public const string ORZ_MN_CHANTRY = "mn_exit_chantry";
     public const string ORZ_MN_CARTA_ENTRANCE = "mn_exit_carta";

     //------------------------------------------------------------------------------
     //*****************************************************************************
     //------------------------------------------------------------------------------
     // Constants to move to global
     //------------------------------------------------------------------------------

     // ORZ_GOLD_REQUIRED_BY_ROGGAR used in dialogue: orz200_roggar; search: "ten"
     public const int ORZ_ROGGAR_CASH_REQ = 10;

     // ORZ_CORRA_CASH_REQ_* used in dialogue: orz210_corra; search: "sovereigns/silver/bits"
     public const int ORZ_CORRA_CASH_REQ_PINT = 10;
     public const int ORZ_CORRA_CASH_REQ_MEAD = 5;
     public const int ORZ_CORRA_CASH_REQ_WINE = 20;
     public const int ORZ_CORRA_CASH_REQ_D_WINE_1 = 50;
     public const int ORZ_CORRA_CASH_REQ_D_WINE_2 = 100;

     // ORZ_ALIMAR_CASH_REQ_* used in dialogue: orz400_alimar; search: "silver"
     public const int ORZ_ALIMAR_CASH_REQ_LOW = 20;
     public const int ORZ_ALIMAR_CASH_REQ_MED = 2;
     public const int ORZ_ALIMAR_CASH_REQ_HIGH = 30;
     public const int ORZ_ALIMAR_CASH_REQ_V_HIGH = 5;

     // ORZ_NADEZDA_CASH_REQ_* used in dialogue: orz400_nadezda; search: "silver"
     public const int ORZ_NADEZDA_CASH_REQ_LOW = 500;
     public const int ORZ_NADEZDA_CASH_REQ_HIGH = 1000;

     // ORZ_NADEZDA_CASH_REQ_* used in dialogue: orz400_nadezda; search: "silver"
     public const int ORZ_ZERLINDA_CASH_REQ_LOW = 1;
     public const int ORZ_ZERLINDA_CASH_REQ_HIGH = 5;

     // ORZ_ROGEK_CASH_REQ_* used in dialogue: orz400_rogek; search: "sovereigns"
     public const int ORZ_ROGEK_CASH_REQ_ACCEPT_LOW = 400000;
     public const int ORZ_ROGEK_CASH_REQ_ACCEPT_HIGH = 500000;
     public const int ORZ_ROGEK_CASH_REQ_BRIBE_LOW = 40;
     public const int ORZ_ROGEK_CASH_REQ_BRIBE_HIGH = 50;

     // ORZ_MARDY_CASH_REQ_* usedin dialgoue orz300_mardy; search: "sovereign"
     public const int ORZ_MARDY_CASH_REQ_CHEAP = 10000;
     public const int ORZ_MARDY_CASH_REQ_NORMAL = 100000;
     public const int ORZ_MARDY_CASH_REQ_SIZABLE = 500000;
     public const int ORZ_MARDY_CASH_REQ_SUBSTANTIAL = 10000000;

     //ORZ_FARYN_CASH_REQ_* used in dialogue: sten_faryn
     public const int ORZ_FARYN_CASH_REQ_LOW = 2;
     public const int ORZ_FARYN_CASH_REQ_MEDIUM = 4;
     public const int ORZ_FARYN_CASH_REQ_HIGH = 5;

     //------------------------------------------------------------------------------
     // Proving - Trials of Blood (FAB)
     //------------------------------------------------------------------------------

     public const int TABLE_TRIALS_EVENTS = 136;

     // Constants

     public const string ORZ_ARENA_CURRENT_SOLO_MATCHES = "ORZ_ARENA_CURRENT_SOLO_MATCHES";
     public const string ORZ_ARENA_CURRENT_GROUP_MATCHES = "ORZ_ARENA_CURRENT_GROUP_MATCHES";
     public const string ORZ_ARENA_CURRENT_GRAND_MELEE_MATCHES = "ORZ_ARENA_CURRENT_GRAND_MELEE_MATCHES";
     public const string ORZ_ARENA_CURRENT_CHALLENGE_MATCHES = "ORZ_ARENA_CURRENT_CHALLENGE_MATCHES";
     public const string ORZ_ARENA_CURRENT_BOAST_MATCHES = "ORZ_ARENA_CURRENT_BOAST_MATCHES";
     public const string ORZ_ARENA_CURRENT_DEEP_WALK_MATCHES = "ORZ_ARENA_CURRENT_DEEP_WALK_MATCHES";
     public const string ORZ_ARENA_CURRENT_VICTORIES = "ORZ_ARENA_CURRENT_VICTORIES";
     public const string ORZ_ARENA_CURRENT_DEFEATS = "ORZ_ARENA_CURRENT_DEFEATS";
     public const string ORZ_ARENA_CURRENT_KILLS = "ORZ_ARENA_CURRENT_KILLS";
     public const string ORZ_CURRENT_EVENT = "ORZ_CURRENT_TRIAL_EVENT";

     public const string ORZ_ARENA_TOTAL_VICTORIES = "ORZ_ARENA_TOTAL_VICTORIES";
     public const string ORZ_ARENA_TOTAL_DEFEATS = "ORZ_ARENA_TOTAL_DEFEATS";
     public const string ORZ_ARENA_TOTAL_KILLS = "ORZ_ARENA_TOTAL_KILLS";

     public const int ORZ_PRESEASON_MAX_SOLO_FIGHTS = 3;
     public const int ORZ_PRESEASON_MAX_GROUP_FIGHTS = 2;
     public const int ORZ_SEASON_MAX_SOLO_FIGHTS = 8;
     public const int ORZ_SEASON_MAX_GROUP_FIGHTS = 8;
     public const int ORZ_POSTSEASON_MAX_SOLO_FIGHTS = 6;
     public const int ORZ_POSTSEASON_MAX_GROUP_FIGHTS = 4;

     public const int ORZ_SEASON_GRAND_MELEE_1 = 2;
     public const int ORZ_SEASON_CHALLENGE_1 = 5;
     public const int ORZ_SEASON_GRAND_MELEE_2 = 7;
     public const int ORZ_SEASON_CHALLENGE_2 = 11;
     public const int ORZ_SEASON_GRAND_MELEE_3 = 15;

     public const string ORZ_EVENT_SPEAKER_1 = "Speaker1";
     public const string ORZ_EVENT_SPEAKER_2 = "Speaker2";
     public const string ORZ_EVENT_SPEAKER_3 = "Speaker3";

     public const string ORZ_EVENT_TRIGGER = "orz262tr_trial_event";

     public const string ORZ_TRIAL_WP_SPEAKER_1_1 = "orz262wp_tr_ev_speaker_1_1";
     public const string ORZ_TRIAL_WP_SPEAKER_1_2 = "orz262wp_tr_ev_speaker_1_2";
     public const string ORZ_TRIAL_WP_SPEAKER_1_3 = "orz262wp_tr_ev_speaker_1_3";

     // Creatures
     public const string ORZ_CR_OSSARD = "orz262cr_ossard";            // Ossard Temmet, brother of PC's rival
     public const string ORZ_CR_TAEKELLE = "orz262cr_taekelle";          // Taekelle Temmet, PC's rival
     public const string ORZ_CR_TAYLEN = "orz262cr_taylen";            // Taylen Glavonak, Armsman
     public const string ORZ_CR_TM_DAAVOS = "orz262cr_tm_daavos";         // Daavos, trial master for the post season
     public const string ORZ_CR_TRIAL_MASTER = "orz262cr_tm_suvrek";         // Suvrek, trial master for the Arena

     public const string ORZ_CR_IRRITATED_DWARF = "orz262cr_irritated_dwarf";   // Annoyed Dwarf, in the intro
     public const string ORZ_CR_PC_FAN_4 = "orz262cr_ev_pc_fan_4";       // PC Fan Girl 4
     public const string ORZ_CR_PC_FAN_5 = "orz262cr_ev_pc_fan_5";       // PC Fan Girl 5
     public const string ORZ_CR_TAEKELLE_FLUNKY_1 = "orz262cr_ev_taek_flunky_1";  // One of Taekelle's flunkies
     public const string ORZ_CR_TAEKELLE_FLUNKY_2 = "orz262cr_ev_taek_flunky_2";  // One of Taekelle's flunkies
     public const string ORZ_CR_TAEKELLE_REVENGE_1 = "orz262cr_ev_taek_revenge_1"; // Reactive to PC's revenge
     public const string ORZ_CR_TAEKELLE_REVENGE_2 = "orz262cr_ev_taek_revenge_2"; // Reactive to PC's revenge
     public const string ORZ_CR_TRIAL_FAN_PC = "orz262cr_amb_pc_fan";        // PC Fan Girl 5
     public const string ORZ_CR_TRIAL_FAN_DAMEK = "orz262cr_amb_damek_fan";     // PC Fan Girl 5
     public const string ORZ_CR_TRIAL_FAN_PIOTIN = "orz262cr_amb_piotin_fan";    // PC Fan Girl 5
     public const string ORZ_CR_TRIAL_FAN_WOJECH = "orz262cr_amb_wojech_fan";    // PC Fan Girl 5

     // Dialog
     public const string ORZ_DLG_NULL = "";
     public const string ORZ_DLG_TRIAL_EVENT = "orz262_event_ambient.dlg";
     public const string ZZ_ORZ_WINLOSS = "zz_trials_temp.dlg";
     public const string ORZ_DLG_DEAD_TRENCH_POPUP = "orz510_popup.dlg";
     public const string ORZ_DLG_JARVIA_CHEST = "orz230_jarvia_chest.dlg";
}