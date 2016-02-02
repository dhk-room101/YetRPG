//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Engine
{
     //public void main() {} // MUST ALWAYS BE COMMENTED OUT. THIS IS FOR DEBUG ONLY
     //
     // achievement_core_h
     // Includes all core achievement-related constants

     // -----------------------------------------------------------------------------
     // Associated 2DA: Achievements.xls
     // //tag/main/data/Source/2DA/Achievements.xls
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"wrappers_h"
     //#include"design_tracking_h"
     //#include"sys_achievements_h"
     //#include"plt_genpt_core_achievements"

     //moved public const string sACHIEVEMENT_DEBUG_VERSION = "|ACH V0.07006|"; //Debug Version
     //moved public const int ACH_SCREENSHOT_TYPE = 1;
     //moved public const int ACH_SCREENSHOT_HP_TYPE = 3;

     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_ALISTAIR = 1;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_DOG = 2;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_MORRIGAN = 3;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_WYNNE = 4;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_SHALE = 5;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_STEN = 6;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_ZEVRAN = 7;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_OGHREN = 8;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_LELIANA = 9;
     //moved public const int EngineConstants.ACHIEVEMENT__APP_FOLLOWER_LOGHAIN = 10;

     //moved public const int EngineConstants.ACHIEVEMENT__CREATURE_TYPE_DARKSPAWN = 7;
     // Broodmother Achievement constants
     //moved public const float BROODMOTHER_ACHIEVEMENT_DURATION = 120.0f; // May need adjusting; this is a pretty long time
     //moved public const int BROOD_PSEUDO_ABILITY_ID = 1;

     // Gauntlet Achievement constants
     //moved public const float RIDDLE_ACHIEVEMENT_DURATION = 300.0f;
     //moved public const int RIDDLE_PSEUDO_ABILITY_ID_TEMP = 2;
     //moved public const int RIDDLE_PSEUDO_ABILITY_ID_PERM = 3;

     // Ogre Achievement constants
     //moved public const int OGRE_PSEUDO_ABILITY_ID = 4;

     // Options constants
     //moved public const int EngineConstants.GAME_DIFFICULTY_CASUAL = 0;
     //moved public const int EngineConstants.GAME_DIFFICULTY_NORMAL = 1;
     //moved public const int EngineConstants.GAME_DIFFICULTY_HARD   = 2;

     ///////////////////////////////////////////////////////////////////
     //moved public const int EngineConstants.EVENT_TYPE_ACHIEVEMENT    = 1081;
     ///////////////////////////////////////////////////////////////////

     //moved public const int ACH_NUMBER_OF_ACHIEVEMENT_IN_THIS_EVENT = 0; // INDEX of where on the Event the number of achievement should be stored
     // Chain Lightning is not declared elsewhere
     //moved public const int EngineConstants.ABILITY_SPELL_CHAIN_LIGHTNING = 10211;
     // Mental Fortress is not declared elsewhere
     //moved public const int EngineConstants.ABILITY_TALENT_MENTAL_FORTRESS = 52;
     // Two-Handed Strength is not declared elsewhere
     //moved public const int EngineConstants.ABILITY_TALENT_2H_STRENGTH = 27;
     // Coup De Grace is not declared elsewhere
     //moved public const int EngineConstants.ABILITY_TALENT_COUP_DE_GRACE = 3002;
     // Summon Wolf is not declared elsewhere
     //moved public const int EngineConstants.ABILITY_TALENT_SUMMON_WOLF = 1004;
     // Stealthy Item Use is not declared elsewhere
     //moved public const int EngineConstants.ABILITY_TALENT_STEALTHY_ITEM_USE = 100076;

     ///////////////////////////////////////////////////////////////////
     //moved public const int ACH_INVALID_ACHIEVEMENT = -1;

     // Advancement constants
     //moved public const int ACH_ADVANCE_LAST_OF_YOUR_LINE = 0;
     //moved public const int ACH_ADVANCE_CORRUPTED = 1;
     //moved public const int ACH_ADVANCE_CONSCRIPTED = 2;
     //moved public const int ACH_ADVANCE_HARROWED = 3;
     //moved public const int ACH_ADVANCE_CASTELESS = 4;
     //moved public const int ACH_ADVANCE_KINSLAYER = 5;
     //moved public const int ACH_ADVANCE_LAST_OF_THE_WARDENS = 6;

     //moved public const int ACH_ADVANCE_STANDARD_BEARER = 31;
     //moved public const int ACH_ADVANCE_HERO_OF_REDCLIFFE = 32;
     //moved public const int ACH_ADVANCE_RABBLE_ROUSER = 33;

     // Decisive Advancement constants
     //moved public const int ACH_DECISIVE_ULTIMATE_SACRIFICE = 42;
     //moved public const int ACH_DECISIVE_WARDEN_COMMANDER = 43;
     //moved public const int ACH_DECISIVE_A_DARK_PROMISE = 44;
     //moved public const int ACH_DECISIVE_REDEEMER = 45;
     //moved public const int ACH_DECISIVE_MAGIC_SYMPATHIZER = 46;
     //moved public const int ACH_DECISIVE_ANNULMENT_INVOKER = 47;
     //moved public const int ACH_DECISIVE_SLAYER = 48;
     //moved public const int ACH_DECISIVE_POACHER = 49;
     //moved public const int ACH_DECISIVE_SACRILEGIOUS = 50;
     //moved public const int ACH_DECISIVE_CEREMONIALIST = 51;
     //moved public const int ACH_DECISIVE_BHELEN_S_ALLY = 52;
     //moved public const int ACH_DECISIVE_HARROWMONT_S_ALLY = 53;
     //moved public const int ACH_DECISIVE_LIBERATOR = 54;
     //moved public const int ACH_DECISIVE_PRAGMATIST = 55;

     // Specialist
     //moved public const int SPC_CHAMPION = 7;
     //moved public const int SPC_TEMPLAR = 8;
     //moved public const int SPC_BERSERKER = 9;
     //moved public const int SPC_REAVER = 10;
     //moved public const int SPC_SHAPESHIFT = 11;
     //moved public const int SPC_SPIRIT_HEALER = 12;
     //moved public const int SPC_ARCANE_WARRIOR = 13;
     //moved public const int SPC_BLOOD_MAGE = 14;
     //moved public const int SPC_ASSASSIN = 15;
     //moved public const int SPC_BARD = 16;
     //moved public const int SPC_RANGER = 17;
     //moved public const int SPC_DUELIST = 18;

     // Ability constants
     //moved public const int ACH_ABI_BLOOD_MAGE = 19;
     //moved public const int ACH_ABI_SHAPESHIFTER = 20;
     //moved public const int ACH_ABI_ARCANE_WARRIOR = 21;
     //moved public const int ACH_ABI_SPIRIT_HEALER = 22;
     //moved public const int ACH_ABI_DUELIST = 23;
     //moved public const int ACH_ABI_ASSASSIN = 24;
     //moved public const int ACH_ABI_RANGER = 25;
     //moved public const int ACH_ABI_BARD = 26;
     //moved public const int ACH_ABI_REAVER = 27;
     //moved public const int ACH_ABI_BERSERKER = 28;
     //moved public const int ACH_ABI_TEMPLAR = 29;
     //moved public const int ACH_ABI_CHAMPION = 30;
     //moved public const int ACH_ABI_DUAL_WEAPONRY_MASTER = 63;
     //moved public const int ACH_ABI_ARCHERY_MASTER = 64;
     //moved public const int ACH_ABI_SHIELD_MASTER = 65;
     //moved public const int ACH_ABI_2H_WEAPON_MASTER = 66;
     //moved public const int ACH_ABI_ACCOMPLISHED_WARRIOR = 67;
     //moved public const int ACH_ABI_ACCOMPLISHED_ROGUE = 68;
     //moved public const int ACH_ABI_ELEMENTALIST = 69;
     //moved public const int ACH_ABI_CONJURER = 70;
     //moved public const int ACH_ABI_THAUMATURGIST = 71;
     //moved public const int ACH_ABI_HEXER = 72;
     //moved public const int ACH_ABI_VETERAN = 143;
     //moved public const int ACH_ABI_ELITE = 144;
     //moved public const int ACH_ABI_EDUCATED = 142;

     // Collection constants
     //moved public const int ACH_COLLECT_MERCENARY = 34;
     //moved public const int ACH_COLLECT_PARAMOUR = 35;
     //moved public const int ACH_COLLECT_LORE_MASTER = 36;
     //moved public const int ACH_COLLECT_RECRUITER = 37;
     //moved public const int ACH_COLLECT_HOPELESSLY_ROMANTIC = 38;
     //moved public const int ACH_COLLECT_PERFECTIONIST = 39;
     //moved public const int ACH_COLLECT_EASILY_SIDETRACKED = 40;
     //moved public const int ACH_COLLECT_DEATH_DEALER = 41;

     // Feat constants
     //moved public const int ACH_FEAT_TRAVELER = 56;
     //moved public const int ACH_FEAT_MASTER_OF_ARMS = 57;
     //moved public const int ACH_FEAT_SHADOW = 58;
     //moved public const int ACH_FEAT_ARCHMAGE = 59;
     ////moved public const int ACH_FEAT_HARDBOILED = 60;
     //moved public const int ACH_FEAT_DRAGONSLAYER = 61;
     //moved public const int ACH_FEAT_OGREBANE = 62;
     //moved public const int ACH_FEAT_WORLDLY = 89;
     //moved public const int ACH_FEAT_GREY_WARDEN = 90;
     //moved public const int ACH_FEAT_MASTER_WARDEN = 91;
     //moved public const int ACH_FEAT_BLIGHT_QUELLER = 92;
     //moved public const int ACH_FEAT_BLOODIED = 93;
     //moved public const int ACH_FEAT_VETERAN = 94;
     //moved public const int ACH_FEAT_GENERAL = 95;
     //moved public const int ACH_FEAT_I_M_KIND_OF_A_BIG_DEAL = 96;
     //moved public const int ACH_FEAT_BROOD_EXTERMINATOR = 97;
     //moved public const int ACH_FEAT_DEFENDER = 98;
     //moved public const int ACH_FEAT_PROTECTOR = 99;
     //moved public const int ACH_FEAT_RIDDLER = 100;
     //moved public const int ACH_FEAT_STREET_SWEEPER = 101;
     //moved public const int ACH_FEAT_HEAVY_HITTER = 102;
     //moved public const int ACH_FEAT_TACTICIAN = 103;
     //moved public const int ACH_FEAT_DESTROYER = 104;

     // Stat constants
     //moved public const int ACH_STATS_PERSUASIVE = 105;
     //moved public const int ACH_STATS_SILVER_TONGUED = 106;
     //moved public const int ACH_STATS_BULLY = 107;
     //moved public const int ACH_STATS_MENACING = 108;
     //moved public const int ACH_STATS_THE_PUNISHER = 109;
     //moved public const int ACH_STATS_WHIRLING_DERVISH = 110;
     //moved public const int ACH_STATS_CALL_ME_CRITICLES = 111;
     //moved public const int ACH_STATS_CRUSHER = 112;
     //moved public const int ACH_STATS_BATTERY = 113;
     //moved public const int ACH_STATS_MUGGER = 114;
     //moved public const int ACH_STATS_TINKERER = 115;
     //moved public const int ACH_STATS_CRAFTY = 116;
     //moved public const int ACH_STATS_CLEVER = 117;
     //moved public const int ACH_STATS_INSIDIOUS = 118;
     //moved public const int ACH_STATS_NIMBLE = 138;
     //moved public const int ACH_STATS_LIGHTNING = 139;
     //moved public const int ACH_STATS_LOCKPICKER = 140;
     //moved public const int ACH_STATS_MASTER_LOCKPICKER = 141;

     //THESE ARE NO LONGER FAKE
     //made into real achievements but not renamed in
     //script to avoid breaking.
     //moved public const int ACH_FAKE_PARAMOUR_ALISTAIR = 119;
     //moved public const int ACH_FAKE_PARAMOUR_MORRIGAN = 120;
     //moved public const int ACH_FAKE_PARAMOUR_ZEVRAN = 121;
     //moved public const int ACH_FAKE_PARAMOUR_LELIANA = 122;

     // Fakes
     //moved public const int ACH_FAKE_ALISTAIR_ENDING = 123;
     //moved public const int ACH_FAKE_LOGHAIN_ENDING = 124;
     //moved public const int ACH_FAKE_PC_ENDING = 125;
     //moved public const int ACH_FAKE_RECRUITER_ALISTAIR = 126;
     //moved public const int ACH_FAKE_RECRUITER_MORRIGAN = 127;
     //moved public const int ACH_FAKE_RECRUITER_WYNNE = 128;
     //moved public const int ACH_FAKE_RECRUITER_ZEVRAN = 129;
     //moved public const int ACH_FAKE_RECRUITER_OGHREN = 130;
     //moved public const int ACH_FAKE_RECRUITER_DOG = 131;
     //moved public const int ACH_FAKE_RECRUITER_LELIANA = 132;
     //moved public const int ACH_FAKE_RECRUITER_STEN = 133;
     //moved public const int ACH_FAKE_RECRUITER_LOGHAIN = 134;

     //Boards
     //moved public const int ACH_PILGRIM = 135;
     //moved public const int ACH_STREETWISE = 136;
     //moved public const int ACH_COLLECTIVE = 137;
     //moved public const int ACH_BLACKSTONE = 145;

     //moved public const int EngineConstants.ACH_PARAM_STREET_SWEEPER_COUNT = 4;
     //moved public const int EngineConstants.ACH_PARAM_DESTROYER_COUNT = 15;
     //moved public const int EngineConstants.ACH_PARAM_HEAVY_HITTER_THRESHOLD = 250;
     //moved public const int EngineConstants.ACH_PARAM_TACTICIAN = 250;

     //moved public const int ACH_EFFECT_DEATH_DAMAGE_INDEX = 4;

     // to optimize things, we also have constant int to use for achievements counts
     //moved public const int EngineConstants.ACH_PARAM_GREY_WARDEN    =   100; // MUST CORRESPOND TO value in achievements.xls
     //moved public const int EngineConstants.ACH_PARAM_MASTER_WARDEN  =  500; // MUST CORRESPOND TO value in achievements.xls
     //moved public const int EngineConstants.ACH_PARAM_BLIGHT_QUELLER = 1000; // MUST CORRESPOND TO value in achievements.xls

     //moved public const int EngineConstants.ACH_PARAM_SKILLS_MAJOR_ACHIEVEMENT = 25;
     //moved public const int EngineConstants.ACH_PARAM_SKILLS_MINOR_ACHIEVEMENT = 5;
     //moved public const int EngineConstants.ACH_PARAM_SKILLS_SIMPLE_ACHIEVEMENT = 10;

     //moved public const int EngineConstants.ACH_PARAM_PERSUADE_SKILL_BASE_INDEX = 1;
     //moved public const int EngineConstants.ACH_PARAM_INTIMIDATE_SKILL_BASE_INDEX = 9;
     //moved public const int EngineConstants.ACH_PARAM_DISARM_SKILL_BASE_INDEX = 17;
     //moved public const int EngineConstants.ACH_PARAM_TRAP_SKILL_BASE_INDEX = 25;
     //moved public const int EngineConstants.ACH_PARAM_CODEX_BASE_INDEX = 33;
     //moved public const int EngineConstants.ACH_PARAM_CODEX_COUNTER_SIZE = 10;
     //moved public const int EngineConstants.ACH_PARAM_TRAVELER_BASE_INDEX = 43;
     //moved public const int EngineConstants.ACH_PARAM_EASILY_SIDETRACKED_BASE_INDEX = 54;
     //moved public const int EngineConstants.ACH_PARAM_CRAFTING_BASE_INDEX = 64;
     //moved public const int EngineConstants.ACH_PARAM_UNLOCK_BASE_INDEX = 86;
     //moved public const int EngineConstants.ACH_PARAM_BOARD_BASE_INDEX = 94;
     //moved public const int EngineConstants.ACH_PARAM_BOARD_COUNTER_SIZE = 8;

     //plot tracking achievements - hidden
     //JH Sep 11, 2009: hidden achievements all cut, percent complete,
     //based off visible achievements now.
     //moved public const int EngineConstants.NUMBER_OF_QUESTS = 83;    //should be 83 to ship

     //moved public const int ACH_FAKE_BLIGHT_1 = 146;
     //moved public const int ACH_FAKE_BLIGHT_2a = 147;
     //moved public const int ACH_FAKE_BLIGHT_2b = 148;
     //moved public const int ACH_FAKE_BLIGHT_2c = 149;
     //moved public const int ACH_FAKE_BLIGHT_3a = 150;
     //moved public const int ACH_FAKE_BLIGHT_3b = 151;
     //moved public const int ACH_FAKE_BLIGHT_3c = 152;
     //moved public const int ACH_FAKE_BLIGHT_3d = 153;
     //moved public const int ACH_FAKE_BLIGHT_3e = 154;
     //moved public const int ACH_FAKE_BLIGHT_3f = 155;
     //moved public const int ACH_FAKE_BLIGHT_3g = 156;
     //moved public const int ACH_FAKE_BLIGHT_3h = 157;
     //moved public const int ACH_FAKE_BLIGHT_4a = 158;
     //moved public const int ACH_FAKE_BLIGHT_4b = 159;
     //moved public const int ACH_FAKE_BLIGHT_5a = 160;
     //moved public const int ACH_FAKE_BLIGHT_5b = 161;
     //moved public const int ACH_FAKE_BLIGHT_5c = 162;
     //moved public const int ACH_FAKE_BLIGHT_5d = 163; // CUT
     //moved public const int ACH_FAKE_BLIGHT_5e = 164;
     //moved public const int ACH_FAKE_BLIGHT_5f = 165;
     //moved public const int ACH_FAKE_BLIGHT_6 = 166;
     //moved public const int ACH_FAKE_BLIGHT_7a = 167;
     //moved public const int ACH_FAKE_BLIGHT_7b = 168;
     //moved public const int ACH_FAKE_BLIGHT_7c = 169;
     //moved public const int ACH_FAKE_BLIGHT_7d = 170;
     //moved public const int ACH_FAKE_BLIGHT_7e = 171;

     //moved public const int ACH_FAKE_REDCLIFFE_1a = 172;
     //moved public const int ACH_FAKE_REDCLIFFE_1b = 173;
     //moved public const int ACH_FAKE_REDCLIFFE_1c = 174;
     //moved public const int ACH_FAKE_REDCLIFFE_1d = 175;
     //moved public const int ACH_FAKE_REDCLIFFE_1e = 176;
     //moved public const int ACH_FAKE_REDCLIFFE_1f = 177;
     //moved public const int ACH_FAKE_REDCLIFFE_1g = 178; // CUT
     //moved public const int ACH_FAKE_REDCLIFFE_1h = 179; // CUT
     //moved public const int ACH_FAKE_REDCLIFFE_1i = 180;
     //moved public const int ACH_FAKE_REDCLIFFE_1j = 181;
     //moved public const int ACH_FAKE_REDCLIFFE_1k = 182;
     //moved public const int ACH_FAKE_REDCLIFFE_1l = 183;
     //moved public const int ACH_FAKE_REDCLIFFE_2a = 184;
     //moved public const int ACH_FAKE_REDCLIFFE_2b = 185;
     //moved public const int ACH_FAKE_REDCLIFFE_3 = 186;
     //moved public const int ACH_FAKE_REDCLIFFE_4 = 187;

     //moved public const int ACH_FAKE_CIRCLE_1a = 188;
     //moved public const int ACH_FAKE_CIRCLE_1b = 189;
     //moved public const int ACH_FAKE_CIRCLE_1c = 190;
     //moved public const int ACH_FAKE_CIRCLE_1d = 191;//does not get counted.
     //moved public const int ACH_FAKE_CIRCLE_1e = 192;
     //moved public const int ACH_FAKE_CIRCLE_1f = 193;
     //moved public const int ACH_FAKE_CIRCLE_1g = 194;
     //moved public const int ACH_FAKE_CIRCLE_1h = 195;
     //moved public const int ACH_FAKE_CIRCLE_1i = 196;
     //moved public const int ACH_FAKE_CIRCLE_1j = 197;
     //moved public const int ACH_FAKE_CIRCLE_1k = 198;
     //moved public const int ACH_FAKE_CIRCLE_1l = 199;
     //moved public const int ACH_FAKE_CIRCLE_1m = 200;
     //moved public const int ACH_FAKE_CIRCLE_1n = 201;
     //moved public const int ACH_FAKE_CIRCLE_2 = 202;
     //moved public const int ACH_FAKE_CIRCLE_3 = 203;
     //moved public const int ACH_FAKE_CIRCLE_4 = 204;

     //moved public const int ACH_FAKE_FINAL_1a = 205;
     //moved public const int ACH_FAKE_FINAL_1b = 206;
     //moved public const int ACH_FAKE_FINAL_1c = 207;
     //moved public const int ACH_FAKE_FINAL_1d = 208;
     //moved public const int ACH_FAKE_FINAL_2 = 209;
     //moved public const int ACH_FAKE_FINAL_3a = 210;
     //moved public const int ACH_FAKE_FINAL_3b = 211; // CUT

     //moved public const int ACH_FAKE_DENERIM_1 = 212;
     //moved public const int ACH_FAKE_DENERIM_2 = 213;
     //moved public const int ACH_FAKE_DENERIM_3 = 214;
     //moved public const int ACH_FAKE_DENERIM_4a = 215;
     //moved public const int ACH_FAKE_DENERIM_4b = 216;
     //moved public const int ACH_FAKE_DENERIM_4c = 217;
     //moved public const int ACH_FAKE_DENERIM_4d = 218;
     //moved public const int ACH_FAKE_DENERIM_4e = 219;
     //moved public const int ACH_FAKE_DENERIM_4f = 220;
     //moved public const int ACH_FAKE_DENERIM_4g = 221;
     //moved public const int ACH_FAKE_DENERIM_4h = 222;
     //moved public const int ACH_FAKE_DENERIM_5 = 223;
     //moved public const int ACH_FAKE_DENERIM_6 = 224;
     //moved public const int ACH_FAKE_DENERIM_7 = 225; // CUT
     //moved public const int ACH_FAKE_DENERIM_8 = 226;
     //moved public const int ACH_FAKE_DENERIM_9 = 227;
     //moved public const int ACH_FAKE_DENERIM_10 = 228;
     //moved public const int ACH_FAKE_DENERIM_11 = 229;
     //moved public const int ACH_FAKE_DENERIM_12 = 230;
     //moved public const int ACH_FAKE_DENERIM_13 = 231;
     //moved public const int ACH_FAKE_DENERIM_14 = 232;
     //moved public const int ACH_FAKE_DENERIM_15 = 233;
     //moved public const int ACH_FAKE_DENERIM_16 = 234;
     //moved public const int ACH_FAKE_DENERIM_17a = 235;
     //moved public const int ACH_FAKE_DENERIM_17b = 236;
     //moved public const int ACH_FAKE_DENERIM_17c = 237;
     //moved public const int ACH_FAKE_DENERIM_17d = 238;
     //moved public const int ACH_FAKE_DENERIM_18 = 239;
     //moved public const int ACH_FAKE_DENERIM_19 = 240;
     //moved public const int ACH_FAKE_DENERIM_20 = 241;

     //moved public const int ACH_FAKE_CHANTRY_1 = 242;
     //moved public const int ACH_FAKE_CHANTRY_2 = 243;
     //moved public const int ACH_FAKE_CHANTRY_3 = 244;
     //moved public const int ACH_FAKE_CHANTRY_4 = 245; // CUT
     //moved public const int ACH_FAKE_CHANTRY_5 = 246;
     //moved public const int ACH_FAKE_CHANTRY_6 = 247;

     //moved public const int ACH_FAKE_BLACKSTONE_1 = 248;
     //moved public const int ACH_FAKE_BLACKSTONE_2  = 249; // CUT
     //moved public const int ACH_FAKE_BLACKSTONE_3 = 250;
     //moved public const int ACH_FAKE_BLACKSTONE_4 = 251;
     //moved public const int ACH_FAKE_BLACKSTONE_5 = 252;
     //moved public const int ACH_FAKE_BLACKSTONE_6 = 253;
     //moved public const int ACH_FAKE_BLACKSTONE_7 = 254;

     //moved public const int ACH_FAKE_PARTY_1 = 255;

     //moved public const int ACH_FAKE_ORZAMMAR_1 = 256;
     //moved public const int ACH_FAKE_ORZAMMAR_2a = 257;
     //moved public const int ACH_FAKE_ORZAMMAR_2b = 258;
     //moved public const int ACH_FAKE_ORZAMMAR_2c = 259;
     //moved public const int ACH_FAKE_ORZAMMAR_2d = 260;
     //moved public const int ACH_FAKE_ORZAMMAR_3a = 261;
     //moved public const int ACH_FAKE_ORZAMMAR_3b = 262;
     //moved public const int ACH_FAKE_ORZAMMAR_3c = 263;
     //moved public const int ACH_FAKE_ORZAMMAR_4 = 264;
     //moved public const int ACH_FAKE_ORZAMMAR_5 = 265;
     //moved public const int ACH_FAKE_ORZAMMAR_6a = 266;
     //moved public const int ACH_FAKE_ORZAMMAR_6b = 267;
     //moved public const int ACH_FAKE_ORZAMMAR_6c = 268;
     //moved public const int ACH_FAKE_ORZAMMAR_7 = 269;
     //moved public const int ACH_FAKE_ORZAMMAR_8a = 270; // CUT
     //moved public const int ACH_FAKE_ORZAMMAR_8b = 271; // CUT
     //moved public const int ACH_FAKE_ORZAMMAR_9 = 272;
     //moved public const int ACH_FAKE_ORZAMMAR_10a = 273;
     //moved public const int ACH_FAKE_ORZAMMAR_10b = 274; // CUT
     //moved public const int ACH_FAKE_ORZAMMAR_11 = 275;
     //moved public const int ACH_FAKE_ORZAMMAR_12 = 276;
     //moved public const int ACH_FAKE_ORZAMMAR_13 = 277;
     //moved public const int ACH_FAKE_ORZAMMAR_14 = 278;
     //moved public const int ACH_FAKE_ORZAMMAR_15 = 279;
     //moved public const int ACH_FAKE_ORZAMMAR_16 = 280;
     //moved public const int ACH_FAKE_ORZAMMAR_17 = 281;
     //moved public const int ACH_FAKE_ORZAMMAR_18 = 282;
     //moved public const int ACH_FAKE_ORZAMMAR_19 = 283;
     //moved public const int ACH_FAKE_ORZAMMAR_20 = 284;
     //moved public const int ACH_FAKE_ORZAMMAR_21 = 285;
     //moved public const int ACH_FAKE_ORZAMMAR_22 = 286;
     //moved public const int ACH_FAKE_ORZAMMAR_23 = 287;
     //moved public const int ACH_FAKE_ORZAMMAR_24 = 288;
     //moved public const int ACH_FAKE_ORZAMMAR_25a = 289;
     //moved public const int ACH_FAKE_ORZAMMAR_25b = 290; // CUT
     //moved public const int ACH_FAKE_ORZAMMAR_25c = 291; // CUT
     //moved public const int ACH_FAKE_ORZAMMAR_26 = 292;
     //moved public const int ACH_FAKE_ORZAMMAR_27 = 293; // CUT
     //moved public const int ACH_FAKE_ORZAMMAR_28 = 294;
     //moved public const int ACH_FAKE_ORZAMMAR_29 = 295;
     //moved public const int ACH_FAKE_ORZAMMAR_30 = 296;

     //moved public const int ACH_FAKE_URN_1 = 297;
     //moved public const int ACH_FAKE_URN_2 = 298;
     //moved public const int ACH_FAKE_URN_3a = 299; // CUT
     //moved public const int ACH_FAKE_URN_3b = 300; // CUT

     //moved public const int ACH_FAKE_BRECILIAN_1a = 301;
     //moved public const int ACH_FAKE_BRECILIAN_1b = 302; // CUT
     //moved public const int ACH_FAKE_BRECILIAN_1c = 303; // CUT
     //moved public const int ACH_FAKE_BRECILIAN_2a = 304;
     //moved public const int ACH_FAKE_BRECILIAN_2b = 305; // CUT
     //moved public const int ACH_FAKE_BRECILIAN_3 = 306;
     //moved public const int ACH_FAKE_BRECILIAN_4a = 307;
     //moved public const int ACH_FAKE_BRECILIAN_4b = 308; // CUT
     //moved public const int ACH_FAKE_BRECILIAN_4c = 309; // CUT
     //moved public const int ACH_FAKE_BRECILIAN_4d = 310; // CUT
     //moved public const int ACH_FAKE_BRECILIAN_5a = 311;
     //moved public const int ACH_FAKE_BRECILIAN_5b = 312; // CUT
     //moved public const int ACH_FAKE_BRECILIAN_6 = 313;
     //moved public const int ACH_FAKE_BRECILIAN_7 = 314;
     //moved public const int ACH_FAKE_BRECILIAN_8a = 315;
     //moved public const int ACH_FAKE_BRECILIAN_8b = 316; // CUT

     //moved public const int ACH_FAKE_LOTHERING_1 = 317;
     //moved public const int ACH_FAKE_LOTHERING_2a = 318;
     //moved public const int ACH_FAKE_LOTHERING_2b = 319;
     //moved public const int ACH_FAKE_LOTHERING_2c = 320;
     //moved public const int ACH_FAKE_LOTHERING_3a = 321;
     //moved public const int ACH_FAKE_LOTHERING_3b = 322;
     //moved public const int ACH_FAKE_LOTHERING_3c = 323;
     //moved public const int ACH_FAKE_LOTHERING_4 = 324;
     //moved public const int ACH_FAKE_LOTHERING_5 = 325;
     //moved public const int ACH_FAKE_LOTHERING_6 = 326;
     //moved public const int ACH_FAKE_LOTHERING_7 = 327;

     //moved public const int ACH_FAKE_OSTAGAR_1 = 328; // CUT
     //moved public const int ACH_FAKE_OSTAGAR_2 = 329;
     //moved public const int ACH_FAKE_OSTAGAR_3 = 330;
     //moved public const int ACH_FAKE_OSTAGAR_4 = 331;
     //moved public const int ACH_FAKE_OSTAGAR_5 = 332;
     //moved public const int ACH_FAKE_OSTAGAR_6 = 333;
     //moved public const int ACH_FAKE_OSTAGAR_7 = 334;

     //moved public const int ACH_FAKE_COLLECTIVE_1 = 335;
     //moved public const int ACH_FAKE_COLLECTIVE_2 = 336;
     //moved public const int ACH_FAKE_COLLECTIVE_3 = 337;
     //moved public const int ACH_FAKE_COLLECTIVE_4 = 338;
     //moved public const int ACH_FAKE_COLLECTIVE_5 = 339;
     //moved public const int ACH_FAKE_COLLECTIVE_6a = 340;
     //moved public const int ACH_FAKE_COLLECTIVE_6b = 341; // CUT
     //moved public const int ACH_FAKE_COLLECTIVE_7 = 342;
     //moved public const int ACH_FAKE_COLLECTIVE_8 = 343;
     //moved public const int ACH_FAKE_COLLECTIVE_9 = 344;
     //moved public const int ACH_FAKE_COLLECTIVE_10 = 345;

     //moved public const int ACH_FAKE_ROGUE_1 = 346;
     //moved public const int ACH_FAKE_ROGUE_2 = 347;
     //moved public const int ACH_FAKE_ROGUE_3 = 348;
     //moved public const int ACH_FAKE_ROGUE_4 = 349;
     //moved public const int ACH_FAKE_ROGUE_5 = 350;
     //moved public const int ACH_FAKE_ROGUE_6 = 351;
     //moved public const int ACH_FAKE_ROGUE_7 = 352;
     //moved public const int ACH_FAKE_ROGUE_8 = 353;
     //moved public const int ACH_FAKE_ROGUE_9 = 354;

     //moved public const int ACH_FAKE_COMPANIONS_1 = 355;
     //moved public const int ACH_FAKE_COMPANIONS_2a = 356;
     //moved public const int ACH_FAKE_COMPANIONS_2b = 357; // CUT
     //moved public const int ACH_FAKE_COMPANIONS_3a = 358;
     //moved public const int ACH_FAKE_COMPANIONS_3b = 359; // CUT
     //moved public const int ACH_FAKE_COMPANIONS_4 = 360;
     //moved public const int ACH_FAKE_COMPANIONS_5 = 361;
     //moved public const int ACH_FAKE_COMPANIONS_6 = 362;

     //moved public const int ACH_FAKE_KOKARI_1 = 363;
     //moved public const int ACH_FAKE_KOKARI_2 = 364;
     //moved public const int ACH_FAKE_KOKARI_3 = 365; // CUT
     //moved public const int ACH_FAKE_KOKARI_4 = 366;

     //moved public const int ACH_FAKE_BDC_1 = 367;
     //moved public const int ACH_FAKE_BDC_2 = 368;
     //moved public const int ACH_FAKE_BDC_3 = 369;
     //moved public const int ACH_FAKE_BDN_1 = 370;
     //moved public const int ACH_FAKE_BDN_2 = 371;
     //moved public const int ACH_FAKE_BDN_3 = 372;
     //moved public const int ACH_FAKE_BEC_1 = 373;
     //moved public const int ACH_FAKE_BEC_2 = 374;
     //moved public const int ACH_FAKE_BEC_3 = 375; // CUT
     //moved public const int ACH_FAKE_BHM_1 = 376;
     //moved public const int ACH_FAKE_BHM_2 = 377;
     //moved public const int ACH_FAKE_BHM_3 = 378;
     //moved public const int ACH_FAKE_BHM_4 = 379;
     //moved public const int ACH_FAKE_BHN_1 = 380;
     //moved public const int ACH_FAKE_BHN_2 = 381;
     //moved public const int ACH_FAKE_BHN_3 = 382;
     //moved public const int ACH_FAKE_BHN_4 = 383;
     //moved public const int ACH_FAKE_BHN_5 = 384;
     //moved public const int ACH_FAKE_BHN_6 = 385;
     //moved public const int ACH_FAKE_BED_1 = 386;
     //moved public const int ACH_FAKE_BED_2 = 387;
     //moved public const int ACH_FAKE_BED_3 = 388;
     //moved public const int ACH_FAKE_BED_4 = 389;
     //moved public const int ACH_FAKE_BED_5 = 390;

     ///////////////////////////////////////////////////////////////////

     ///////////////////////////////////////////////////////////////////
     // Functions
     ///////////////////////////////////////////////////////////////////

     public void ACH_LogTrace(int nChannel, string sLogEntry, GameObject oTarget = null)
     {
#if DEBUG
          LogTrace(nChannel, EngineConstants.sACHIEVEMENT_DEBUG_VERSION + sLogEntry, oTarget);
#endif
     }

     ///////////////////////////////////////////////////////////////////
     // DUMMY FUNCTIONS TO BE USED UNTIL WE GET NEW ONES FROM PROGRAMMING
     ///////////////////////////////////////////////////////////////////
     public int ACH__GetAchievementCount(string sAchievement)
     {
#if DEBUG
          ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Trying to read achievement count for " + sAchievement + ", returning 0 for now.");
#endif
          return 0;
     }

     public void ACH__SetAchievementCount(string sAchievement, int nCount)
     {
#if DEBUG
          ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Trying to set an achievement count for " + sAchievement + ", doing nothing for now.");
#endif

     }

     ///////////////////////////////////////////////////////////////////

     // Read a series of consecutive plot flags as an integer.
     public int ACH_GetPlotCounter(string sPlot, int nBaseIndex, int nCounterSize)
     {
          int nCount = 0;
          int nBitIndex = 0;
          int nMultiplier = 1;

          // read each bit and add 1 for the first bit, 2 for the second, 4 for the third, etc.
          while (nBitIndex < nCounterSize)
          {

               if (WR_GetPlotFlag(sPlot, nBaseIndex + nBitIndex) != EngineConstants.FALSE)
               {
                    nCount = nCount + nMultiplier;
               }
               nMultiplier = nMultiplier * 2; // value of each bit double with each consecutive bit
               nBitIndex = nBitIndex + 1;
          }

          return nCount;
     }

     // Read a series of consecutive plot flag as an integer, increment that integer and return it.
     public int ACH_IncrementPlotCounter(string sPlot, int nBaseIndex, int nCounterSize)
     {
          // read the current count and increment it
          int nCount = ACH_GetPlotCounter(sPlot, nBaseIndex, nCounterSize);
          nCount++;

          int nBitIndex = 0;
          int bOverflow = EngineConstants.TRUE;

          // read each bit starting with the lowest.
          // If the bit is equal to 1, flip it and move to the next bit.
          // If the bit is equal to 0, flip it and stop
          while (nBitIndex < nCounterSize)
          {
               if (WR_GetPlotFlag(sPlot, nBaseIndex + nBitIndex) != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(sPlot, nBaseIndex + nBitIndex, EngineConstants.FALSE);
                    nBitIndex++;
               }
               else
               {
                    WR_SetPlotFlag(sPlot, nBaseIndex + nBitIndex, EngineConstants.TRUE);
                    nBitIndex = nCounterSize; // break out of the loop
                    bOverflow = EngineConstants.FALSE;
               }
          }

          if (bOverflow != EngineConstants.FALSE)
          {
#if DEBUG
               string sWarning = "ACHIEVEMENT PLOT COUNTER OVERFLOW! PLOT= " + sPlot + ", BaseIndex= " + IntToString(nBaseIndex) + ", nCounterSize=" + IntToString(nCounterSize) + ". Contact Emmanuel or Georg";
               Warning(sWarning);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, sWarning);
               Log_Trace_Scripting_Error("achievement_core_h.ACH_IncrementPlotCountert", sWarning, gameObject);
#endif
          }

          return nCount;
     }

     /*
     * This function is used to handle general procedures that occur
     * when an achievement is unlocked.  An eventID will always be sent to the
     * web server.  If bTakeScreenshot is set to false when WR_UnlockAchievement or
     * WR_IncrementAndMaybeGrantAchievement is called, no screenshot is taken.  If
     * it is set to true (default), it then goes to the Achievements 2DA and
     * uses the HandleFlag to determine how to take the screenshot.  0 is no screenshot.
     * Anything greater than 0 is take a screenshot.  The exact value can be used in the
     * future to classify individual shots.
     *
     * EV152396
     * EV153349
     *
     * Jason Hill
     * Mar 19, 2009
     */
     public void HandleAchGrant(int nAchievementID, int bTakeScreenshot)
     {
          // -----------------------------------------------------------------
          // Georg: Grant achievement on the SkyNet Development Telemetry
          //        system as well.
          // -----------------------------------------------------------------
#if SKYNET
        Acv_Grant(nAchievementID + 10000 /* magic number for enduser acvs*/);
#endif

          // Grant screenshot if indicated, xEvent sent in TakeScreenshot
          if (bTakeScreenshot > 0)
          {
               string sAchievement = GetM2DAString(EngineConstants.TABLE_ACHIEVEMENTS, "AchievementID", nAchievementID);
               int nTitle = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "TitleStrRef", nAchievementID);
               int nDescription = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "DescStrRef", nAchievementID);
               string sEventID = IntToString(nTitle);

#if DEBUG
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Screenshot Taken: " + sAchievement + "; Script: " + GetCurrentScriptName());
#endif

               if (bTakeScreenshot == 1) //HIGH PRIORITY
                    TakeScreenshot(EngineConstants.FALSE, nTitle, nDescription, EngineConstants.ACH_SCREENSHOT_HP_TYPE, sEventID);
               else //NOT SO HIGH PRIORITY
                    TakeScreenshot(EngineConstants.FALSE, nTitle, nDescription, EngineConstants.ACH_SCREENSHOT_TYPE, sEventID);
               //DisplayFloatyMessage(GetHero(), "Shot");
          }
          else
          {
               //Send a Log Event regardless
               int iEventID = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "TitleStrRef", nAchievementID);
               LogStoryEvent(iEventID);
               //DisplayFloatyMessage(GetHero(), "No Shot");
          }
     }

     /*
     * @brief                Wrapper function to unlock an achievement and log
     * @param nAchievementID   The achievement to unlock; //tag/main/data/Source/2DA/Achievements.xls
     *
     * @author               Austin Peckenpaugh
     *
     **/
     public int WR_IncrementAndMaybeGrantAchievement(int nAchievementID, int bTakeScreenshot = EngineConstants.FALSE)
     {
          // Unlock the achievement. This is called everytime the player could unlock
          // the achievement and the game will sort out if this has already occured or not

#if DEBUG
          ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Unlocking " + IntToString(nAchievementID) + ", screenshot=" + IntToString(bTakeScreenshot));
#endif

          int bAchieved = IncrementAchievementCountByID(nAchievementID);

          if (bAchieved != EngineConstants.FALSE)
          {
               //If true, take value from 2DA
               if (bTakeScreenshot != EngineConstants.FALSE)
                    bTakeScreenshot = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "HandleFlag", nAchievementID);

#if DEBUG
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Achievement Unlocked: ID= " + IntToString(nAchievementID) + ", screenshot=" + IntToString(bTakeScreenshot));
#endif

               HandleAchGrant(nAchievementID, bTakeScreenshot);
          }

          return bAchieved;
     }

     /*
     * @brief                  Wrapper function to unlock an achievement regardless of count
     * @param nAchievementID   The achievement to unlock; //tag/main/data/Source/2DA/Achievements.xls
     *
     * @author               Austin Peckenpaugh
     *
     **/
     public void WR_UnlockAchievement(int nAchievementID, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE)
     {

          // Unlock the achievement. This is called everytime the player could unlock
          // the achievement and the game will sort out if this has already occured or not

#if DEBUG
          ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Unlocking " + IntToString(nAchievementID) + ", screenshot=" + IntToString(bTakeScreenshot) + ", grantforreal=" + IntToString(bGrantAchievement));
#endif

          // Grant achievement if indicated
          if (bGrantAchievement != EngineConstants.FALSE && GetHasAchievementByID(nAchievementID) == EngineConstants.FALSE)
          {
               //If true, take value from 2DA
               if (bTakeScreenshot != EngineConstants.FALSE)
                    bTakeScreenshot = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "HandleFlag", nAchievementID);

               UnlockAchievementByID(nAchievementID);

#if DEBUG
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Achievement Unlocked: ID= " + IntToString(nAchievementID) + ", screenshot=" + IntToString(bTakeScreenshot) + ", grantforreal=" + IntToString(bGrantAchievement));
#endif

               HandleAchGrant(nAchievementID, bTakeScreenshot);
          }
     }

     public void ACH_CalculatePercentageComplete()
     {
          //get all the tracking achievements and get percentage
          int iNumberOfQuestsCompleted = GetHasAchievementByID(EngineConstants.ACH_ADVANCE_LAST_OF_YOUR_LINE) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_CORRUPTED) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_CONSCRIPTED) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_HARROWED) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_CASTELESS) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_KINSLAYER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_LAST_OF_THE_WARDENS) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_STANDARD_BEARER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_HERO_OF_REDCLIFFE) +
                                          GetHasAchievementByID(EngineConstants.ACH_ADVANCE_RABBLE_ROUSER) +
                                          GetHasAchievementByID(EngineConstants.ACH_COLLECT_MERCENARY) +
                                          GetHasAchievementByID(EngineConstants.ACH_COLLECT_RECRUITER) +
                                          GetHasAchievementByID(EngineConstants.ACH_COLLECT_HOPELESSLY_ROMANTIC) +
                                          GetHasAchievementByID(EngineConstants.ACH_COLLECT_PERFECTIONIST) +
                                          GetHasAchievementByID(EngineConstants.ACH_COLLECT_EASILY_SIDETRACKED) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_WARDEN_COMMANDER) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_REDEEMER) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_ULTIMATE_SACRIFICE) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_A_DARK_PROMISE) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_MAGIC_SYMPATHIZER) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_ANNULMENT_INVOKER) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_SLAYER) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_POACHER) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_SACRILEGIOUS) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_CEREMONIALIST) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_BHELEN_S_ALLY) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_HARROWMONT_S_ALLY) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_LIBERATOR) +
                                          GetHasAchievementByID(EngineConstants.ACH_DECISIVE_PRAGMATIST) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_TRAVELER) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_MASTER_OF_ARMS) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_SHADOW) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_ARCHMAGE) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_DRAGONSLAYER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_DUAL_WEAPONRY_MASTER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_ARCHERY_MASTER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_SHIELD_MASTER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_2H_WEAPON_MASTER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_ACCOMPLISHED_WARRIOR) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_ACCOMPLISHED_ROGUE) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_ELEMENTALIST) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_CONJURER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_THAUMATURGIST) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_HEXER) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_GREY_WARDEN) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_MASTER_WARDEN) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_BLIGHT_QUELLER) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_BLOODIED) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_VETERAN) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_GENERAL) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_I_M_KIND_OF_A_BIG_DEAL) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_DEFENDER) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_HEAVY_HITTER) +
                                          GetHasAchievementByID(EngineConstants.ACH_FEAT_TACTICIAN) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_PERSUASIVE) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_SILVER_TONGUED) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_BULLY) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_MENACING) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_THE_PUNISHER) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_WHIRLING_DERVISH) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_CALL_ME_CRITICLES) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_CRUSHER) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_BATTERY) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_MUGGER) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_TINKERER) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_CRAFTY) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_CLEVER) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_INSIDIOUS) +
                                          GetHasAchievementByID(EngineConstants.ACH_FAKE_PARAMOUR_ALISTAIR) +
                                          GetHasAchievementByID(EngineConstants.ACH_FAKE_PARAMOUR_MORRIGAN) +
                                          GetHasAchievementByID(EngineConstants.ACH_FAKE_PARAMOUR_ZEVRAN) +
                                          GetHasAchievementByID(EngineConstants.ACH_FAKE_PARAMOUR_LELIANA) +
                                          GetHasAchievementByID(EngineConstants.ACH_PILGRIM) +
                                          GetHasAchievementByID(EngineConstants.ACH_STREETWISE) +
                                          GetHasAchievementByID(EngineConstants.ACH_COLLECTIVE) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_NIMBLE) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_LIGHTNING) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_LOCKPICKER) +
                                          GetHasAchievementByID(EngineConstants.ACH_STATS_MASTER_LOCKPICKER) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_EDUCATED) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_VETERAN) +
                                          GetHasAchievementByID(EngineConstants.ACH_BLACKSTONE) +
                                          GetHasAchievementByID(EngineConstants.ACH_ABI_ELITE);

          float fPercentageComplete = IntToFloat(iNumberOfQuestsCompleted) / EngineConstants.NUMBER_OF_QUESTS * 100;
          float fResult = MinF(fPercentageComplete, 100.0f);
          SetGameCompletionPercentage(fResult);
     }

     public void ACH_TrackPercentageComplete(int iPlotCompleted)
     {
          //This functionality was cut on Sep 11, 2009
          //DisplayFloatyMessage(GetHero(), IntToString(iPlotCompleted));
          //WR_UnlockAchievement(iPlotCompleted);
     }

     // Check the hero's level and grant achievement if 20
     public void ACH_WhenHeroGainsLevel(GameObject oChar, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE)
     {
          if (IsHero(oChar) != EngineConstants.FALSE)
          {
               int nLevel = GetLevel(oChar);
               int nClass = GetCreatureCoreClass(oChar);
               if (nLevel >= 20)
               {
                    switch (nClass)
                    {
                         case EngineConstants.CLASS_WARRIOR:
                              {
                                   WR_UnlockAchievement(EngineConstants.ACH_FEAT_MASTER_OF_ARMS);
                              }
                              break;

                         case EngineConstants.CLASS_ROGUE:
                              {
                                   WR_UnlockAchievement(EngineConstants.ACH_FEAT_SHADOW);
                              }
                              break;

                         case EngineConstants.CLASS_WIZARD:
                              {
                                   WR_UnlockAchievement(EngineConstants.ACH_FEAT_ARCHMAGE);
                              }
                              break;
                    }
               }
          }
     }

     // Check the hero's abilities and grant achievements for certain combos
     public void ACH_WhenHeroLevelsUp(GameObject oChar, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE)
     {
          if (IsHero(oChar) != EngineConstants.FALSE)
          {
               ///////////////////////////////////////////////////////////////////
               // Ability combos
               ///////////////////////////////////////////////////////////////////

               // Weapon Talents
               int bDualWeaponryMaster = ((HasAbility(oChar, EngineConstants.ABILITY_TALENT_DUAL_WEAPON_MASTER) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_PUNISHER) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_DUAL_WEAPON_WHIRLWIND) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               int bArcheryMaster = ((HasAbility(oChar, EngineConstants.ABILITY_TALENT_MASTER_ARCHER) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_ARROW_OF_SLAYING) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_SCATTERSHOT) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               int bShieldMaster = ((HasAbility(oChar, EngineConstants.ABILITY_TALENT_SHIELD_MASTERY) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_SHIELD_EXPERTISE) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_OVERPOWER) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               int b2HWeaponMaster = ((HasAbility(oChar, EngineConstants.ABILITY_TALENT_CRITICAL_STRIKE) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_DESTROYER) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_WEAPON_SWEEP) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               // Spell Talents
               int bElementalist = ((HasAbility(oChar, EngineConstants.ABILITY_SPELL_BLIZZARD) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_INFERNO) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_PETRIFY) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_CHAIN_LIGHTNING) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               int bConjurer = ((HasAbility(oChar, EngineConstants.ABILITY_SPELL_PURIFY) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_HEROS_ARMOR) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_GLYPH_OF_NEUTRALIZATION) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_STINGING_SWARM) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               int bThaumaturgist = ((HasAbility(oChar, EngineConstants.ABILITY_SPELL_ANTIMAGIC_BURST) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_MANA_CLASH) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_ANIMATE_DEAD) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_CRUSHING_PRISON) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               int bHexer = ((HasAbility(oChar, EngineConstants.ABILITY_SPELL_MASS_PARALYSIS) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_IMMOBILIZE) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_MIND_ROT) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SPELL_DEATH_CLOUD) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               // Class Talents
               int bAccomplishedWarrior = ((HasAbility(oChar, EngineConstants.ABILITY_TALENT_PERFECT_STRIKING) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_DEATH_BLOW) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               int bAccomplishedRogue = ((HasAbility(oChar, EngineConstants.ABILITY_SKILL_LOCKPICKING_4) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SKILL_STEALTH_4) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_EVASION) != EngineConstants.FALSE))
                                           ||
                                           ((HasAbility(oChar, EngineConstants.ABILITY_SKILL_LOCKPICKING_4) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SKILL_STEALTH_4) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_FEIGN_DEATH) != EngineConstants.FALSE))
                                           ||
                                           ((HasAbility(oChar, EngineConstants.ABILITY_TALENT_EVASION) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_SKILL_STEALTH_4) != EngineConstants.FALSE) &&
                                           (HasAbility(oChar, EngineConstants.ABILITY_TALENT_FEIGN_DEATH) != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;

               // If hero has the ability combo, grant the achievement
               if (bDualWeaponryMaster != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_DUAL_WEAPONRY_MASTER);
               if (bArcheryMaster != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_ARCHERY_MASTER);
               if (bShieldMaster != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_SHIELD_MASTER);
               if (b2HWeaponMaster != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_2H_WEAPON_MASTER);
               if (bElementalist != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_ELEMENTALIST);
               if (bConjurer != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_CONJURER);
               if (bThaumaturgist != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_THAUMATURGIST);
               if (bHexer != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_HEXER);
               if (bAccomplishedWarrior != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_ACCOMPLISHED_WARRIOR);
               if (bAccomplishedRogue != EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_ABI_ACCOMPLISHED_ROGUE);
          }

          if (IsHero(oChar) != EngineConstants.FALSE)
          {
               ///////////////////////////////////////////////////////////////////
               // Specialty Classes
               ///////////////////////////////////////////////////////////////////

               //Player has ability
               int bAssassin = HasAbility(oChar, EngineConstants.ABILITY_TALENT_HIDDEN_ASSASSIN);
               int bBerserker = HasAbility(oChar, EngineConstants.ABILITY_TALENT_HIDDEN_BERSERKER);
               int bChampion = HasAbility(oChar, EngineConstants.ABILITY_TALENT_HIDDEN_CHAMPION);
               int bDuelist = HasAbility(oChar, EngineConstants.ABILITY_TALENT_HIDDEN_DUELIST);
               int bRanger = HasAbility(oChar, EngineConstants.ABILITY_TALENT_HIDDEN_RANGER);
               int bReaver = HasAbility(oChar, EngineConstants.ABILITY_TALENT_HIDDEN_REAVER);
               int bTemplar = HasAbility(oChar, EngineConstants.ABILITY_TALENT_HIDDEN_TEMPLAR);
               int bArcaneWarrior = HasAbility(oChar, EngineConstants.ABILITY_SPELL_HIDDEN_ARCANE_WARRIOR);
               int bBard = HasAbility(oChar, EngineConstants.ABILITY_SPELL_HIDDEN_BARD);
               int bBloodMage = HasAbility(oChar, EngineConstants.ABILITY_SPELL_HIDDEN_BLOODMAGE);
               int bShapeshifter = HasAbility(oChar, EngineConstants.ABILITY_SPELL_HIDDEN_SHAPESHIFTER);
               int bSpiritHealer = HasAbility(oChar, EngineConstants.ABILITY_SPELL_HIDDEN_SPIRIT_HEALER);

               //Player has recieved this achievement
               int bAchAssassin = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ASSASSIN);
               int bAchBerserker = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BERSERKER);
               int bAchChampion = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_CHAMPION);
               int bAchDuelist = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_DUELIST);
               int bAchRanger = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_RANGER);
               int bAchReaver = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_REAVER);
               int bAchTemplar = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TEMPLAR);
               int bAchArcaneWarrior = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ARCANEWARRIOR);
               int bAchBard = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BARD);
               int bAchBloodMage = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BLOODMAGE);
               int bAchShapeshifter = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_SHAPESHIFTER);
               int bAchSpiritHealer = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_SPIRITHEALER);

               int bOneSpec = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC);
               int bTwoSpec = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS);

               //do this test only if the player doesn't have two specializations
               if (bTwoSpec == EngineConstants.FALSE)
               {
                    //grant the ach. if the player has the spec, does not have the ach.
                    //for it, and doesn't already have two specs.
                    if (bAssassin != EngineConstants.FALSE && bAchAssassin == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ASSASSIN, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ASSASSIN, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bBerserker != EngineConstants.FALSE && bAchBerserker == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BERSERKER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BERSERKER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bChampion != EngineConstants.FALSE && bAchChampion == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_CHAMPION, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_CHAMPION, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bDuelist != EngineConstants.FALSE && bAchDuelist == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_DUELIST, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_DUELIST, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bRanger != EngineConstants.FALSE && bAchRanger == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_RANGER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_RANGER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bReaver != EngineConstants.FALSE && bAchReaver == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_REAVER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_REAVER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bTemplar != EngineConstants.FALSE && bAchTemplar == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TEMPLAR, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TEMPLAR, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bArcaneWarrior != EngineConstants.FALSE && bAchArcaneWarrior == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ARCANEWARRIOR, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ARCANEWARRIOR, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bBard != EngineConstants.FALSE && bAchBard == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BARD, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BARD, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bBloodMage != EngineConstants.FALSE && bAchBloodMage == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BLOODMAGE, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_BLOODMAGE, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bShapeshifter != EngineConstants.FALSE && bAchShapeshifter == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_SHAPESHIFTER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_SHAPESHIFTER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }

                    if (bSpiritHealer != EngineConstants.FALSE && bAchSpiritHealer == EngineConstants.FALSE)
                    {
                         if (bOneSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_VETERAN);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_ONE_SPEC, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_SPIRITHEALER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                         else if (bTwoSpec == EngineConstants.FALSE)
                         {
                              WR_UnlockAchievement(EngineConstants.ACH_ABI_ELITE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_TWO_SPECS, EngineConstants.TRUE, EngineConstants.TRUE);
                              WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_HAS_SPIRITHEALER, EngineConstants.TRUE, EngineConstants.TRUE);
                         }
                    }
               }
          }
     }

     // Handle and apply romance-related achievements
     // Additionally, grant "hidden" achievements associated with the follower for each romance unlocked
     // Then check to see if all romances have been unlocked
     public void ACH_HandleHeroRomance(int nFollower, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE)
     {
          //THESE ARE NO LONGER FAKE
          //made into real achievements but not renamed in
          //script to avoid breaking.
          switch (nFollower)
          {
               case EngineConstants.ACHIEVEMENT__APP_FOLLOWER_ALISTAIR:
                    {
                         WR_UnlockAchievement(EngineConstants.ACH_FAKE_PARAMOUR_ALISTAIR);
                    }
                    break;

               case EngineConstants.ACHIEVEMENT__APP_FOLLOWER_LELIANA:
                    {
                         WR_UnlockAchievement(EngineConstants.ACH_FAKE_PARAMOUR_LELIANA);
                    }
                    break;

               case EngineConstants.ACHIEVEMENT__APP_FOLLOWER_ZEVRAN:
                    {
                         WR_UnlockAchievement(EngineConstants.ACH_FAKE_PARAMOUR_ZEVRAN);
                    }
                    break;

               case EngineConstants.ACHIEVEMENT__APP_FOLLOWER_MORRIGAN:
                    {
                         WR_UnlockAchievement(EngineConstants.ACH_FAKE_PARAMOUR_MORRIGAN);
                    }
                    break;
          }

          int bAlistair = GetHasAchievementByID(EngineConstants.ACH_FAKE_PARAMOUR_ALISTAIR);
          int bLeliana = GetHasAchievementByID(EngineConstants.ACH_FAKE_PARAMOUR_LELIANA);
          int bZevran = GetHasAchievementByID(EngineConstants.ACH_FAKE_PARAMOUR_ZEVRAN);
          int bMorrigan = GetHasAchievementByID(EngineConstants.ACH_FAKE_PARAMOUR_MORRIGAN);

          if ((bAlistair != EngineConstants.FALSE) && (bMorrigan != EngineConstants.FALSE) && (bLeliana != EngineConstants.FALSE) && (bZevran != EngineConstants.FALSE))
          {
               // Grant "has all romances" achievement
               WR_UnlockAchievement(EngineConstants.ACH_COLLECT_HOPELESSLY_ROMANTIC);
          }
     }

     public void ACH_HandleDeathEffect(GameObject oTarget, GameObject oKiller, xEffect eEffect)
     {
          xEvent eAchievement = Event(EngineConstants.EVENT_TYPE_ACHIEVEMENT);
          int nNumberOfAchievement = 0;
          // for each achievement found, nNumberOfAchievement will be incremented

          if (IsObjectValid(oKiller) != EngineConstants.FALSE)
          {
               if (IsHero(oTarget) != EngineConstants.FALSE)
               {
                    // Only acknowledge this death if not in camp
                    int bCamp = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.PLT_CORE_BOOKKEEPING_CAMP);
                    if (bCamp == EngineConstants.FALSE)
                    {
                         // Set a Plot Flag to indicate that the Hero has been defeated.
                         // This is used to disqualify players from the following achievements:
                         // Bloodied, Veteran, General and I'm kind of a Big Deal
                         WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.PLT_CORE_BOOKKEEPING_HERO_DIED, EngineConstants.TRUE);
#if DEBUG
                         ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "The hero has died. No more survival achievement for you!");
#endif
                    }
                    else
                    {
                         // The player died in camp; ignore the death
#if DEBUG
                         ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "The hero has died, but this was a camping death. Ignore this death.");
#endif
                    }
               }
               else
               if (IsPartyMember(oKiller) != EngineConstants.FALSE)
               {

                    //                                                        //
                    // ---------------- DARKSPAWN KILLING ------------------- //
                    //                                                        //
                    if (GetCreatureRacialType(oTarget) == EngineConstants.ACHIEVEMENT__CREATURE_TYPE_DARKSPAWN)
                    {
#if DEBUG
                         ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Potential achievement: killed a darkspawn");
#endif

                         if (GetHasAchievementByID(EngineConstants.ACH_FEAT_GREY_WARDEN) == EngineConstants.FALSE)
                         {
                              // Player does not have the lowest achievement: GREY WARDEN
                              // Increment the count for that achievement and automatically
                              // grant it if the right count has been reached
                              WR_IncrementAndMaybeGrantAchievement(EngineConstants.ACH_FEAT_GREY_WARDEN);
                         }
                         else
                         {
                              if (GetHasAchievementByID(EngineConstants.ACH_FEAT_MASTER_WARDEN) == EngineConstants.FALSE)
                              {
                                   // Player has GREY WARDEN, but does not have MASTER WARDEN
                                   WR_IncrementAndMaybeGrantAchievement(EngineConstants.ACH_FEAT_MASTER_WARDEN);
                              }
                              else
                              {

                                   if (GetHasAchievementByID(EngineConstants.ACH_FEAT_BLIGHT_QUELLER) == EngineConstants.FALSE)
                                   {
                                        // Player has MASTER WARDEN, but does not have BLIGHT QUELLER
                                        WR_IncrementAndMaybeGrantAchievement(EngineConstants.ACH_FEAT_BLIGHT_QUELLER);
                                   }
                              }
                         }
                    }

                    int nAbilityID = GetEffectAbilityIDRef(ref eEffect);
                    int nAchievementID = -1;
#if DEBUG
                    ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Other Achievement? Ability used is: " + IntToString(nAbilityID));
#endif
                    switch (nAbilityID)
                    {

                         case EngineConstants.ABILITY_TALENT_PUNISHER: nAchievementID = EngineConstants.ACH_STATS_THE_PUNISHER; break;
                         case EngineConstants.ABILITY_TALENT_DUAL_WEAPON_WHIRLWIND: nAchievementID = EngineConstants.ACH_STATS_WHIRLING_DERVISH; break;
                         case EngineConstants.ABILITY_TALENT_ARROW_OF_SLAYING: nAchievementID = EngineConstants.ACH_STATS_CALL_ME_CRITICLES; break;
                         case EngineConstants.ABILITY_TALENT_MIGHTY_BLOW: nAchievementID = EngineConstants.ACH_STATS_CRUSHER; break;
                         case EngineConstants.ABILITY_TALENT_ASSAULT: nAchievementID = EngineConstants.ACH_STATS_BATTERY; break;
                    }

                    if (nAchievementID != -1)
                    {
                         if (GetHasAchievementByID(nAchievementID) == EngineConstants.FALSE)
                         {
                              WR_IncrementAndMaybeGrantAchievement(nAchievementID);
                         }
                    }

                    // All other achievements must be done by the Hero himself
                    if (IsHero(oKiller) != EngineConstants.FALSE)
                    {
                         int bDamagedTheHero = GetLocalInt(oTarget, EngineConstants.CREATURE_DAMAGED_THE_HERO); // set in effect_damage_h

                         if (bDamagedTheHero == EngineConstants.FALSE)
                         {

                              // the Hero killed this creature without the creature damaging the hero
                              if (GetHasAchievementByID(EngineConstants.ACH_FEAT_TACTICIAN) == EngineConstants.FALSE)
                              {
#if DEBUG
                                   ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Tactician achievement: This creature has been killed by the hero without damaging the hero...");
#endif
                                   WR_IncrementAndMaybeGrantAchievement(EngineConstants.ACH_FEAT_TACTICIAN);
                              }

                         }

                         int nDamage = GetEffectIntegerRef(ref eEffect, EngineConstants.ACH_EFFECT_DEATH_DAMAGE_INDEX);

                         if (nDamage >= EngineConstants.ACH_PARAM_HEAVY_HITTER_THRESHOLD)
                         {
#if DEBUG
                              ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Achievement HEAVY Hitter? Damage caused is: " + IntToString(nDamage));
#endif

                              if (GetHasAchievementByID(EngineConstants.ACH_FEAT_HEAVY_HITTER) == EngineConstants.FALSE)
                              {
                                   WR_UnlockAchievement(EngineConstants.ACH_FEAT_HEAVY_HITTER);
                              }
                         }
                    }

               }

          }

     }

     public void ACH_ProcessAchievementModuleEvent(xEvent ev)
     {
          Debug.Log("function restored by DHK");

          int nEventCount = GetEventIntegerRef(ref ev, 0);
          ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Received an achievement xEvent containing " + IntToString(nEventCount) + " achievements.");

          if (nEventCount > 0)
          {

               int nAchievementID = 0;

               int nIndex = 1;
               while (nIndex <= nEventCount)
               {
                    nAchievementID = GetEventIntegerRef(ref ev, nIndex);

                    ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Now processing achievement #" + IntToString(nIndex) + " with ID = " + IntToString(nAchievementID));

                    switch (nAchievementID)
                    {
                         case EngineConstants.ACH_FEAT_GREY_WARDEN:
                         case EngineConstants.ACH_FEAT_MASTER_WARDEN:
                         case EngineConstants.ACH_FEAT_BLIGHT_QUELLER:
                         case EngineConstants.ACH_FEAT_TACTICIAN:
                         case EngineConstants.ACH_FEAT_HEAVY_HITTER:
                         case EngineConstants.ACH_STATS_THE_PUNISHER:
                         case EngineConstants.ACH_STATS_WHIRLING_DERVISH:
                         case EngineConstants.ACH_STATS_CALL_ME_CRITICLES:
                         case EngineConstants.ACH_STATS_CRUSHER:
                         case EngineConstants.ACH_STATS_BATTERY:
                              {
                                   WR_UnlockAchievement(nAchievementID);
                                   break;
                              }


                         default:
                              {
                                   Warning("ACHIEVEMENT EVENT NOT_HANDLED! " + IntToString(nAchievementID) + " (" + Log_GetAbilityNameById(nAchievementID) + ").  + Contact Emmanuel or Georg");
                                   ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "ACHIEVEMENT EVENT NOT_HANDLED! " + IntToString(nAchievementID) + " (" + Log_GetAbilityNameById(nAchievementID) + ").  + Contact Emmanuel or Georg");
                                   Log_Trace_Scripting_Error("achievement_core_h.ACH_ProcessAchievementModuleEvent", "ABIlITY ID NOT HANDLED! " + IntToString(nAchievementID) + " (" + Log_GetAbilityNameById(nAchievementID) + ")", gameObject);
                                   break;
                              }

                    }

                    nIndex++;
               }
          }
          else
          {
               // Received an xEvent but no data stored on it? Something bad happened. Log it.
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Received an xEvent but no data stored on it? Something bad happened...");

          }

     }

     public void ACH_ProcessAchievementImpactDamageAndEffects(EventSpellScriptImpactStruct stEvent, List<GameObject> oTargets)
     {
          Debug.Log("function restored by DHK");
          // only do test for certain abilities
          if ((stEvent.nAbility == EngineConstants.ABILITY_TALENT_DUAL_WEAPON_SWEEP) || (stEvent.nAbility == EngineConstants.ABILITY_TALENT_WEAPON_SWEEP))
          {
               // cycle through objects
               int nMax = GetArraySize(oTargets);
#if DEBUG
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "The Hero has potentially killed " + IntToString(nMax) + " creatures in one blow!");
#endif
               // only if there's a chance that there will be enough killed enemies
               if (nMax >= EngineConstants.ACH_PARAM_STREET_SWEEPER_COUNT)
               {
                    // only if the achievement hasn't been obtained yet
                    if (GetHasAchievementByID(EngineConstants.ACH_FEAT_STREET_SWEEPER) == EngineConstants.FALSE)
                    {
                         int nDeadCount = 0;

                         int nIndex = 0;
                         while (nIndex < nMax)
                         {
                              // count the dead
                              if (IsDeadOrDying(oTargets[nIndex]) != EngineConstants.FALSE)
                              {
                                   nDeadCount++;

                                   // stop as soon as we have enough death to qualify
                                   if (nDeadCount >= EngineConstants.ACH_PARAM_STREET_SWEEPER_COUNT)
                                   {
                                        // grant the achievement
                                        WR_UnlockAchievement(EngineConstants.ACH_FEAT_STREET_SWEEPER);

                                        // break the loop
                                        nIndex = nMax;
                                   }
                              }
                              nIndex++;
                         }
#if DEBUG
                         ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "In fact, the Hero has actually killed " + IntToString(nDeadCount) + " creatures in one blow...");
#endif

                    }
               }
          }

     }

     // If the player hasn't died yet, grant the relevant achievement (Bloodied, veteran, general, kind of big deal)
     public void ACH_CheckForSurvivalAchievement(int nAchievementToGrant)
     {
          if (WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.PLT_CORE_BOOKKEEPING_HERO_DIED) == EngineConstants.FALSE)
          {
               // If the Player hasn't died, grant achievement: Bloodied
               WR_UnlockAchievement(nAchievementToGrant);
          }
          else
          {
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "The Hero has died at least once... no achievement for you.");
          }
     }

     // Check if the player should receive a minor or a major plot-counter based achievement
     public void _CheckMinorMajorPlotCountAchievements(GameObject oActor, int nBaseIndex, int nMinorAchievementID, int nMajorAchievementID, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE, int nCounterSize = 8)
     {
          if (IsFollower(oActor) != EngineConstants.FALSE)
          {
               int nAchievementID = -1;
               //DisplayFloatyMessage(oActor, GetTag(oActor), FLOATY_MESSAGE, 16777215, 5.0f);
               // if the player already has the major achievment, no need to do anything else
               if (GetHasAchievementByID(nMajorAchievementID) == EngineConstants.FALSE)
               {
                    // finding out how many successes the Hero has with the skill
                    int nNeededMajorCount = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "UnlockedAt", nMajorAchievementID);
                    int nCount = ACH_IncrementPlotCounter(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, nBaseIndex, nCounterSize);

                    ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Skill Successes = " + IntToString(nCount) + "; baseindex = " + IntToString(nBaseIndex));

                    // if enough to get the major achievement, "grant" it
                    if (nCount >= nNeededMajorCount) nAchievementID = nMajorAchievementID;
                    else
                    {
                         // if the player already has the minor achievement, no need to go further
                         if (GetHasAchievementByID(nMinorAchievementID) != EngineConstants.FALSE) return;
                         else
                         {
                              int nNeededMinorCount = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "UnlockedAt", nMinorAchievementID);

                              // if enough to get the minor achievement, "grant" it
                              if (nCount >= nNeededMinorCount) nAchievementID = nMinorAchievementID;
                         }
                    }
               }

               // if we found an achivement, grant it for real.
               if (nAchievementID != -1)
               {
                    WR_UnlockAchievement(nAchievementID);
               }
          }
     }

     // Check if the player should receive a plot-counter based achievement
     public void _CheckPlotCountAchievement(GameObject oActor, int nBaseIndex, int nAchievementID, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE, int nCounterSize = 8)
     {
          if (IsHero(oActor) != EngineConstants.FALSE)
          {
               if (GetHasAchievementByID(nAchievementID) == EngineConstants.FALSE)
               {
                    int nNeededCount = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "UnlockedAt", nAchievementID);

                    // finding out how many successes the Hero has with the skill and increment it
                    int nCount = ACH_IncrementPlotCounter(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, nBaseIndex, nCounterSize);
                    ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Skill Successes = " + IntToString(nCount) + "; baseindex = " + IntToString(nBaseIndex));

                    // if enough to get the achievement, "grant" it
                    if (nCount >= nNeededCount) WR_UnlockAchievement(nAchievementID);
               }
          }
     }

     // Check if crafter is player; if so, grant (increment) achievement
     public void ACH_CraftAchievement(GameObject oActor, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE)
     {
          _CheckMinorMajorPlotCountAchievements(oActor, EngineConstants.ACH_PARAM_CRAFTING_BASE_INDEX, EngineConstants.ACH_STATS_TINKERER, EngineConstants.ACH_STATS_CRAFTY, bTakeScreenshot, bGrantAchievement);
     }

     // Check if trap creator is player; if so, grant (increment) achievement
     public void ACH_TrapAchievement(GameObject oActor, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE)
     {
          _CheckMinorMajorPlotCountAchievements(oActor, EngineConstants.ACH_PARAM_TRAP_SKILL_BASE_INDEX, EngineConstants.ACH_STATS_CLEVER, EngineConstants.ACH_STATS_INSIDIOUS, bTakeScreenshot, bGrantAchievement);
     }

     // Check if trap creator is player; if so, grant (increment) achievement
     public void ACH_DisarmAchievement(GameObject oActor, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE)
     {
          _CheckMinorMajorPlotCountAchievements(oActor, EngineConstants.ACH_PARAM_DISARM_SKILL_BASE_INDEX, EngineConstants.ACH_STATS_NIMBLE, EngineConstants.ACH_STATS_LIGHTNING, bTakeScreenshot, bGrantAchievement);
     }

     // grant (increment) achievement
     public void ACH_LockpickAchievement(GameObject oActor, int bTakeScreenshot = EngineConstants.TRUE, int bGrantAchievement = EngineConstants.TRUE)
     {
          _CheckMinorMajorPlotCountAchievements(oActor, EngineConstants.ACH_PARAM_UNLOCK_BASE_INDEX, EngineConstants.ACH_STATS_LOCKPICKER, EngineConstants.ACH_STATS_MASTER_LOCKPICKER, bTakeScreenshot, bGrantAchievement);
     }

     // grant achievement
     public void ACH_MuggerAchievement()
     {
          if (GetHasAchievementByID(EngineConstants.ACH_STATS_MUGGER) == EngineConstants.FALSE)
          {
               WR_UnlockAchievement(EngineConstants.ACH_STATS_MUGGER);
          }
     }

     // Check if the player has enough codex entries; if so, grant (increment) achievement
     public void ACH_CodexAchievement(GameObject oActor)
     {
          _CheckPlotCountAchievement(oActor, EngineConstants.ACH_PARAM_CODEX_BASE_INDEX, EngineConstants.ACH_COLLECT_LORE_MASTER, EngineConstants.TRUE, EngineConstants.TRUE, EngineConstants.ACH_PARAM_CODEX_COUNTER_SIZE);
     }

     // Check if the player has enough codex entries; if so, grant (increment) achievement
     public void ACH_MercAchievement(GameObject oActor)
     {
          _CheckPlotCountAchievement(oActor, EngineConstants.ACH_PARAM_BOARD_BASE_INDEX, EngineConstants.ACH_COLLECT_MERCENARY, EngineConstants.TRUE, EngineConstants.TRUE, EngineConstants.ACH_PARAM_BOARD_COUNTER_SIZE);
     }

     // grant achievement
     public void ACH_PilgrimAchievement()
     {
          if (GetHasAchievementByID(EngineConstants.ACH_PILGRIM) == EngineConstants.FALSE)
          {
               WR_UnlockAchievement(EngineConstants.ACH_PILGRIM);
          }
     }

     // grant achievement
     public void ACH_StreetwiseAchievement()
     {
          if (GetHasAchievementByID(EngineConstants.ACH_STREETWISE) == EngineConstants.FALSE)
          {
               WR_UnlockAchievement(EngineConstants.ACH_STREETWISE);
          }
     }

     // grant achievement
     public void ACH_CollectiveAchievement()
     {
          if (GetHasAchievementByID(EngineConstants.ACH_COLLECTIVE) == EngineConstants.FALSE)
          {
               WR_UnlockAchievement(EngineConstants.ACH_COLLECTIVE);
          }
     }

     // grant achievement
     public void ACH_FighterAchievement()
     {
          if (GetHasAchievementByID(EngineConstants.ACH_BLACKSTONE) == EngineConstants.FALSE)
          {
               WR_UnlockAchievement(EngineConstants.ACH_BLACKSTONE);
          }
     }

     // grant achievement
     public void ACH_Educated()
     {
          WR_UnlockAchievement(EngineConstants.ACH_ABI_EDUCATED);
     }

     // Check if the player has discovered a specially flagged area; if so, increment the count for such areas (and possibly grant the achievement)
     // The flag is stored on the column labeled AchievementFlag in areadata.xls
     public void ACH_TravelerAchievement(int n2DA, int nAreaID)
     {
          int bAchievementFlag = GetM2DAInt(n2DA, "AchievementFlag", nAreaID);

          if (bAchievementFlag != EngineConstants.FALSE)
          {
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Entered area flagged for Traveler achievement for the first time! AreaID=" + ToString(nAreaID));

               GameObject oActor = GetHero();
               _CheckPlotCountAchievement(oActor, EngineConstants.ACH_PARAM_TRAVELER_BASE_INDEX, EngineConstants.ACH_FEAT_TRAVELER, EngineConstants.TRUE, EngineConstants.TRUE);
          }
     }

     // Check if the player has enough Persuade successes; if so, grant (increment) achievement
     public void ACH_CheckPersuadeAchievement(GameObject oPC, int bResult, int nSkill, int nNeededSkill)
     {
          if (bResult != EngineConstants.FALSE)
          {
               if (nSkill == nNeededSkill)
               {
                    _CheckMinorMajorPlotCountAchievements(oPC, EngineConstants.ACH_PARAM_PERSUADE_SKILL_BASE_INDEX, EngineConstants.ACH_STATS_PERSUASIVE, EngineConstants.ACH_STATS_SILVER_TONGUED, EngineConstants.TRUE, EngineConstants.TRUE);
                    ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Persuade success sent.");
               }
          }
     }

     // Check if the player has enough Intimidate successes; if so, grant (increment) achievement
     public void ACH_CheckIntimidateAchievement(GameObject oPC, int bResult, int nSkill, int nNeededSkill)
     {
          if (bResult != EngineConstants.FALSE)
          {
               if (nSkill == nNeededSkill)
               {
                    _CheckMinorMajorPlotCountAchievements(oPC, EngineConstants.ACH_PARAM_INTIMIDATE_SKILL_BASE_INDEX, EngineConstants.ACH_STATS_BULLY, EngineConstants.ACH_STATS_MENACING, EngineConstants.TRUE, EngineConstants.TRUE);
                    ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Intimidate success sent.");
               }
          }
     }

     // Checks the game "plot marker assist" setting and sets a plot flag when it is turned on
     public void ACH_CheckPlotMarkers()
     {
          if (GetGamePlotAssist() != EngineConstants.GAME_PLOT_ASSIST_OFF)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.PLT_CORE_BOOKKEEPING_PLOT_MARKERS, EngineConstants.TRUE);
               ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Plot assist markers turned ON. No achievement for you...");
          }
     }

     // Checks for the "plot marker assist" plot flag and grants the achievement if it's not set
     public void ACH_CheckAndGrantWorldly()
     {
          int bEZMode = WR_GetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.PLT_CORE_BOOKKEEPING_PLOT_MARKERS);
          if (bEZMode == EngineConstants.FALSE) WR_UnlockAchievement(EngineConstants.ACH_FEAT_WORLDLY);
     }

     /*
     * @brief                Function to route the death incurred when entering the camp for the purpose of preserving the "survivor" achievements.
                             EngineConstants.TRUE means the player is in camp and death should be ignored.
                             EngineConstants.FALSE means the player is out of camp and death is counted.
     * @param bFlag          The state of the plot flag: EngineConstants.TRUE or EngineConstants.FALSE
     *
     * @author               Austin Peckenpaugh
     *
     **/
     public void ACH_SetCampingState(int bFlag)
     {
          WR_SetPlotFlag(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.PLT_CORE_BOOKKEEPING_CAMP, bFlag);
     }

     // Increments quest completion counter for Easily Sidetracked achievement
     public void ACH_TrackCompletedQuests()
     {
          int nCount = ACH_IncrementPlotCounter(EngineConstants.PLT_GENPT_CORE_ACHIEVEMENTS, EngineConstants.ACH_PARAM_EASILY_SIDETRACKED_BASE_INDEX, 10);
          int nNeededCount = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "UnlockedAt", EngineConstants.ACH_COLLECT_EASILY_SIDETRACKED);
          ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "Quests completed = " + IntToString(nCount) + "; baseindex = " + IntToString(EngineConstants.ACH_PARAM_EASILY_SIDETRACKED_BASE_INDEX));
          // if enough to get the achievement, "grant" it
          if (nCount >= nNeededCount) WR_UnlockAchievement(EngineConstants.ACH_COLLECT_EASILY_SIDETRACKED);
     }
}