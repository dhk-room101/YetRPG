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
     //::///////////////////////////////////////////////
     //:: ran_constants_h
     //:: Copyright (c) 2008 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         List of constants for the Random Encounters
     */
     //:://////////////////////////////////////////////
     //:: Created By: Mark Barazzuol
     //:: Created On: May 26, 2008
     //:://////////////////////////////////////////////

     /**************/
     // CREATURES
     /**************/

     public const string RAN_BANNORN_COMMANDER = "ran260cr_bannorn_knight";
     public const string RAN_BANNORN_TROOP_A = "ran260cr_bannorn_troop_a";
     public const string RAN_BANNORN_TROOP_B = "ran260cr_bannorn_troop_b";
     public const string RAN_BANNORN_TROOP_C = "ran260cr_bannorn_troop_c";
     public const string RAN_BANNORN_TROOP_D = "ran260cr_bannorn_troop_d";
     public const string RAN_BANNORN_TROOP_E = "ran260cr_bannorn_troop_e";

     public const string RAN_LOGHAIN_COMMANDER = "ran260cr_loghain_sergeant";
     public const string RAN_LOGHAIN_TROOP_A = "ran260cr_loghain_troop_a";
     public const string RAN_LOGHAIN_TROOP_B = "ran260cr_loghain_troop_b";
     public const string RAN_LOGHAIN_TROOP_C = "ran260cr_loghain_troop_c";
     public const string RAN_LOGHAIN_TROOP_D = "ran260cr_loghain_troop_d";
     public const string RAN_LOGHAIN_TROOP_E = "ran260cr_loghain_troop_e";

     public const string RND_CR_120_MERCHANT = "ran120cr_merchant";

     public const string RND_CR_JOWAN = "ran250cr_jowan";           // Jowan, Plains 5 encounter
     public const string RND_CR_250_REFUGEE_A = "ran250cr_jowan_refugee_a";
     public const string RND_CR_250_REFUGEE_B = "ran250cr_jowan_refugee_b";
     public const string RND_CR_250_REFUGEE_C = "ran250cr_jowan_refugee_c";
     public const string RAN_CR_SURVIVOR = "ran401cr_survivor";        // fake survivor for Zevran's encounter

     public const string RAN_CR_280_DWARF = "ran280cr_dwarf_soldier";

     public const string RAN_409_ASSASIN = "ran409cr_assasin_leader";
     public const string RAN_800_TEMPLAR_BOSS = "ran800cr_templar_boss";

     public const string RAN_KENT_PA = "ran000cr_kent";
     public const string RAN_KENT_MA = "ran000cr_kent_ma";

     public const string RAN_CR_950_PEASANT = "ran000cr_axe";

     public const string RAN_CR_220_WEREWOLF_E = "ran220cr_werewolf_e";
     public const string RAN_CR_220_WEREWOLF_F = "ran220cr_werewolf_f";
     public const string RAN_CR_220_WEREWOLF_G = "ran220cr_werewolf_g";
     public const string RAN_CR_220_WEREWOLF_H = "ran220cr_werewolf_h";
     public const string RAN_CR_220_WEREWOLF_I = "ran220cr_werewolf_i";
     public const string RAN_CR_220_WEREWOLF_J = "ran220cr_werewolf_j";

     public const string RAN_CR_220_ELF_B = "ran220cr_elf_hunter_b";
     public const string RAN_CR_220_ELF_D = "ran220cr_elf_hunter_d";
     public const string RAN_CR_220_ELF_G = "ran220cr_elf_hunter_g";
     public const string RAN_CR_220_ELF_H = "ran220cr_elf_hunter_h";

     public const string RAN_CR_290_MELORA = "ran290cr_melora";

     public const string RAN_CR_401_ZEVRAN = "ran401cr_zevran";
     public const string RAN_CR_ASSASSIN_A = "ran401cr_assassin_a";

     public const string RAN_CR_270_LEADER = "ran270cr_bandit_leader";
     public const string RAN_CR_270_GRUMPY = "ran270cr_bandit_grumpy";
     public const string RAN_CR_270_HUNGRY = "ran270cr_bandit_hungry";

     public const string RAN_CR_180_TRICKSTER_2A = "ran180cr_trickster2a";
     public const string RAN_CR_180_TRICKSTER_2B = "ran180cr_trickster2b";
     public const string RAN_CR_180_TRICKSTER_3A = "ran180cr_trickster3a";
     public const string RAN_CR_180_TRICKSTER_3B = "ran180cr_trickster3b";

     public const string RAN_CR_300_MONSTER_HARD_A = "ran300cr_ds_hrd_a";
     public const string RAN_CR_300_MONSTER_HARD_B = "ran300cr_ds_hrd_b";

     /**************/
     // ITEMS
     /**************/

     public const string RAN_600_SOLDIERS_DIARY = "rnd600ar_soldiers_diary";
     public const string R_RAN_600_SOLDIERS_DIARY = "rnd600ar_soldiers_diary.uti";
     public const string RAN_402_JENNY_LETTER = "ran401_im_lt_jenletter";

     /**************/
     // WAYPOINTS
     /**************/
     public const string RAN_WP_START = "start";
     public const string RAN_WP_ZEVRAN_AMBUSH = "ran401wp_ambush";
     public const string RAN_WP_270_COMBATSTART = "wp_startfight_a";
     public const string RAN_WP_AFTER_TREE_PC = "ran401wp_after_tree_pc";

     /**************/
     // PLACEABLES
     /**************/

     public const string WMT_RAND_ENCOUNTER_EXIT = "wmt_rand_encounter_exit";
     public const string RAN_PL_ORE = "ran920ip_orepile";
     public const string RANIP_TREE_DEAD = "ranip_tree_dead";
     public const string RANIP_TREE_DEAD_DOWN = "ranip_tree_dead_down";

     /**************/
     // MAP LOCATIONS
     /**************/

     public const string WML_LC_CARAVAN = "wml_lc_darkspawn";
     public const string WML_LC_JOWAN = "wml_lc_jowan";
     public const string WML_LC_BATTLE = "wml_lc_battlefield";
     public const string WML_LC_CIVIL = "wml_lc_civil_war";
     public const string WML_LC_REFUGEES = "wml_lc_refugees";

     /**************/
     // GROUPS
     /**************/

     public const int RAN_TEAM_ARMY_DARKSPAWN_ENEMIES = 1;
     public const int RAN_TEAM_MAGES_DARKSPAWN = 2;
     public const int RAN_TEAM_LOGHAIN = 2;
     public const int RAN_TEAM_ORZ_GUARDS = 3;
     public const int RAN_TEAM_NTB_MELORA_HUNTERS = 4;
     public const int RAN_TEAM_TEMPLARS = 5;
     public const int RAN_TEAM_DEMONS = 6;
     public const int RAN_TEAM_DEMONS_B = 130;

     // Spider Teams
     public const int RAN_TEAM_RAN210_TEAM_A = 10;
     public const int RAN_TEAM_RAN210_TEAM_B = 11;
     public const int RAN_TEAM_RAN210_TEAM_C = 12;
     public const int RAN_TEAM_RAN210_TEAM_D = 14;

     // More Teams
     public const int RAN_TEAM_RAN900_SHADES = 20;
     public const int RAN_TEAM_ELF = 25;
     public const int RAN_TEAM_WEREWOLF = 26;

     public const int RAN_TEAM_RAN240_ENEMY = 30;

     public const int RAN_TEAM_RAN270_BANDITS = 40;

     public const int RAN_TEAM_RAN600_RATS = 50;
     public const int RAN_TEAM_RAN600_WOLVES = 51;
     public const int RAN_TEAM_RAN600_BEAR = 52;

     public const int RAN_TEAM_250_WAVE_1 = 60;
     public const int RAN_TEAM_250_WAVE_2 = 61;
     public const int RAN_TEAM_250_WAVE_3 = 62;

     public const int RAN_TEAM_RAN401_ASSASSINS = 70;
     public const int RAN_TEAM_RAN401_SURVIVOR = 71;
     public const int RAN_TEAM_RAN401_TRAPS = 72;
     public const int RAN_TEAM_RAN401_INVISIBLE = 73;

     public const int RAN_TEAM_300_DARKSPAWN = 80;
     public const int RAN_TEAM_300_INACTIVE = 81;

     public const int RAN_TEAM_310_BEASTS = 90;
     public const int RAN_TEAM_310_INACTIVE = 91;

     public const int RAN_TEAM_320_WOLVES = 100;
     public const int RAN_TEAM_320_INACTIVE = 101;

     public const int RAN_TEAM_105_DARKSPAWN = 105;
     public const int RAN_TEAM_105_REFUGEES = 106;

     public const int RAN_TEAM_405_DARKSPAWN = 110;

     public const int RAN_TEAM_120_CARAVAN = 120;
     public const int RAN_TEAM_RAN120_PAGES = 121;

     public const int RAN_TEAM_140_TAORAN = 140;

     public const int RAN_TEAM_160_WITNESS = 160;

     public const int RAN_TEAM_170_SEEN_ME = 170;

     public const int RAN_TEAM_180_TRICKSTER = 180;
     public const int RAN_TEAM_180_TRICK_SUMMONS = 181;

     public const int RAN_TEAM_409_ASSASINS = 409;
     public const int RAN_TEAM_409_LEADER = 410;

     /**************/
     // AREA LISTS
     /**************/

     public const string RAN_AR_ZEVRAN_1 = "ran401ar_highway_zevran1";
     public const string RAN_AR_DOG = "ran405ar_highway_dog";
     public const string RAN_AR_OPEN_LOTHERING_LC = "ran420ar_open_lothering_lc";
     public const string RAN_AR_FOREST_NTB_STEAL = "ran290ar_forest_ntb_steal";
     public const string RAN_AR_HW_CIR_TEMPLARS = "ran800ar_hw_cir_templars";
     public const string RAN_AR_WYNNE_1 = "ran110ar_plains_darkspawn_1";
     public const string RAN_AR_WYNNE_2 = "ran130ar_plains_undead";
     public const string RAN_AR_LELIANA = "ran409ar_leliana_assasins";

     /**************/
     // Items
     /**************/

     public const string RAN_ZEVRAN_BAIT_WOMAN_WEAPON = "gen_im_wep_mag_sta_mgc";
     public const string RAN_IM_LOTHERING_NOTE = "ran420im_note";

     public const string RAN_IM_AXAMETER = "gen_im_wep_mel_axe_spi.uti";

     /**************/
     // Dialogs
     /**************/
     public const string RAN_DG_ADVENTURERS = "r_hwy_friendly_adventurers.dlg";

     /**************/
     // EFFECTS
     /**************/

     public const int RAN_VFX_STEAM = 4029;
}