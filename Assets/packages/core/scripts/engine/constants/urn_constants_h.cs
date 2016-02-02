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
     //------------------------------------------------------------------------------
     //  urn_constants_h.nss
     //------------------------------------------------------------------------------
     //
     //  Constants for the Urn of Sacred Ashes plot.
     //
     //  (1) CREATURES
     //  (2) WAYPOINTS
     //  (3) PLACEABLES
     //  (4) TRIGGERS
     //  (5) AREAS
     //  (6) AREA LISTS
     //  (7) RESOURCES
     //  (8) ITEMS
     //  (9) NUMERIC CONSTANTS
     //
     //------------------------------------------------------------------------------
     //  Jan 10, 2007 - Created: Ferret Baudoin
     //  Oct 26, 2007 - Owner: Grant Mackay
     //------------------------------------------------------------------------------

     //public void main() {}

     //------------------------------------------------------------------------------
     // (1) CREATURES
     //------------------------------------------------------------------------------

     // Major Characters
     public const string URN_CR_EIRIK = "urn110cr_eirik";        // Eirik, Revered Father in Haven
     public const string URN_CR_GENITIVI = "urn110cr_genitivi";     // Genitivi, Brother that is missing
     public const string URN_CR_GUARDIAN = "urn230cr_guardian";     // Guardian, at the Gauntlet
     public const string URN_CR_KOLGRIM = "urn210cr_kolgrim";      // Kolgrim, leader of the dragon cult
     public const string URN_CR_DRAGON = "urn220cr_dragon";       // It's his age.

     // Minor Characters
     public const string URN_CR_HAVEN_CHILD = "urn100cr_child";              // Child in Haven
     public const string URN_CR_HAVEN_GUARD = "urn100cr_guard";              // Guard in Haven
     public const string URN_CR_HAVEN_VILLAGER = "urn100cr_villager";           // Villager in Haven
     public const string URN_CR_CH_VILLAGER = "urn110cr_chantry_villager";   // Chantry Villagers
     public const string URN_CR_CH_GUARD = "urn110cr_chantry_guard_";     // Chantry Guard
     public const string URN_CR_SHOPKEEPER = "urn130cr_shopkeeper";         // Shopkeeper in the village
     public const string URN_CR_SHADY_PATRON = "zz_urncr_shady";              // Shady Patron in the Spoiled Princess
     public const string URN_CR_FAKE_WEYLON = "den270cr_weylon";             // Weylon imposter
     public const string URN_CR_PUZZLE_WRAITH = "urn230cr_puzzle_wraith";      // Assistants for the bridge puzzle

     // Riddlers in the Gauntlet
     public const string URN_CR_BRONA = "urn230cr_brona";         // One of the riddlers
     public const string URN_CR_CATHAIRE = "urn230cr_cathaire";      // One of the riddlers
     public const string URN_CR_EALISAY = "urn230cr_ealisay";       // One of the riddlers
     public const string URN_CR_HAVARD = "urn230cr_havard";        // One of the riddlers
     public const string URN_CR_HESSARIAN = "urn230cr_hessarian";     // One of the riddlers
     public const string URN_CR_MAFERATH = "urn230cr_maferath";      // One of the riddlers
     public const string URN_CR_SHARTAN = "urn230cr_shartan";       // One of the riddlers
     public const string URN_CR_VASILIA = "urn230cr_vasilia";       // One of the riddlers

     // Background Ghosts
     public const string URN_CR_BRIDGET = "urn230cr_bridget";         // Bridget, Human Commoner's sister
     public const string URN_CR_BRYCE = "urn230cr_bryce";           // Bryce, Human Noble's father
     public const string URN_CR_JOWAN = "urn230cr_jowan";           // Jowan, Mage's friend
     public const string URN_CR_LESKE = "urn230cr_leske";           // Leske, Dwarven Commoner's friend
     public const string URN_CR_SHIANNI = "urn230cr_shianni";         // Shianni, City Elf's friend
     public const string URN_CR_TAMLEN = "urn230cr_tamlen";          // Tamlen, Dalish Elf's friend
     public const string URN_CR_TRIAN = "urn230cr_trian";           // Trian, Dwarven Noble's brother

     //Combat creatures
     public const string ASH_WRAITH_PREFIX = "urn200cr_bound_wraith_";     // Ash Wraiths in the Hidden Temple
     public const string URN_CR_BRONTO = "urn200cr_bronto";            // Bronto in the Hidden Temple
     public const string URN_CR_AMBUSH = "urn200cr_ambush";            // Cultists involved in the temple ambush
     public const string URN_CR_CULTIST = "urn000cr_cultist";           // Generic culstist.
     public const string URN_CR_DRAGON_HANDLER = "urn210cr_dragonling_handler";// Dragonling Handler, in the Wyrmlings Lair
     public const string URN_CR_SHADE = "urn200cr_shade";             // Shade in the ruined temple

     // Doppelganger fight
     public const string URN_CR_DPLG_WARRIOR_M = "urn230cr_dplg_warrior_m";
     public const string URN_CR_DPLG_WARRIOR_F = "urn230cr_dplg_warrior_f";
     public const string URN_CR_DPLG_ROGUE_M = "urn230cr_dplg_rogue_m";
     public const string URN_CR_DPLG_ROGUE_F = "urn230cr_dplg_rogue_f";
     public const string URN_CR_DPLG_MAGE_M = "urn230cr_dplg_mage_m";
     public const string URN_CR_DPLG_MAGE_F = "urn230cr_dplg_mage_f";
     // Dwarves
     public const string URN_CR_DPLG_WARRIOR_M_D = "urn230cr_dplg_warrior_m_d";
     public const string URN_CR_DPLG_WARRIOR_F_D = "urn230cr_dplg_warrior_f_d";
     public const string URN_CR_DPLG_ROGUE_M_D = "urn230cr_dplg_rogue_m_d";
     public const string URN_CR_DPLG_ROGUE_F_D = "urn230cr_dplg_rogue_f_d";
     // Elves
     public const string URN_CR_DPLG_WARRIOR_M_E = "urn230cr_dplg_warrior_m_e";
     public const string URN_CR_DPLG_WARRIOR_F_E = "urn230cr_dplg_warrior_f_e";
     public const string URN_CR_DPLG_ROGUE_M_E = "urn230cr_dplg_rogue_m_e";
     public const string URN_CR_DPLG_ROGUE_F_E = "urn230cr_dplg_rogue_f_e";
     public const string URN_CR_DPLG_MAGE_M_E = "urn230cr_dplg_mage_m_e";
     public const string URN_CR_DPLG_MAGE_F_E = "urn230cr_dplg_mage_f_e";

     public const string URN_CR_DPLG_ALISTAIR = "urn230cr_alistair";
     public const string URN_CR_DPLG_LELIANA = "urn230cr_leliana";
     public const string URN_CR_DPLG_LOGHAIN = "urn230cr_loghain";
     public const string URN_CR_DPLG_MORRIGAN = "urn230cr_morrigan";
     public const string URN_CR_DPLG_OGHREN = "urn230cr_oghren";
     public const string URN_CR_DPLG_STEN = "urn230cr_sten";
     public const string URN_CR_DPLG_WYNNE = "urn230cr_wynne";
     public const string URN_CR_DPLG_ZEVRAN = "urn230cr_zevran";

     //Oghren plot
     public const string URN_CR_FELSI = "urn300cr_felsi";      //Oghren's love interest in Spoiled Princess

     //------------------------------------------------------------------------------
     // (2) WAYPOINTS
     //------------------------------------------------------------------------------

     public const string URN_WP_VILLAGE_START = "urn100wp_start";

     // Area Transition waypoint.
     public const string URN_WP_WYRMLING_LAIR_FROM_TEMPLE = "urn210wp_from_ruined_temple";
     public const string URN_WP_WYRMLING_LAIR_FROM_MOUNTAIN = "urn210wp_from_mountain_top";
     public const string URN_WP_MOUNTAIN_TOP_FROM_TEMPLE = "urn220wp_from_ruined_temple";
     public const string URN_WP_TEMPLE_FROM_MOUNTAIN_TOP = "urn200wp_from_mountain";
     public const string URN_WP_GAUNTLET_FROM_MOUNTAIN_TOP = "urn230wp_from_mountain_top";
     public const string URN_WP_DENERIM_FROM_GENITIVIS = "den270wp_from_exterior";
     public const string URN_WP_SPOILED_PRINCESS_ENTRANCE = "cir110wp_from_docks";
     public const string URN_WP_TO_CHANTRY = "urn110wp_from_village";
     public const string URN_WP_PC_TO_TEMPLE = "urn200wp_from_village";
     public const string URN_WP_KOLGRIM_SECOND_WAVE = "urn210wp_kolgrim_second_wave";
     public const string URN_WP_KOLGRIM_END = "urn220wp_kolgrim_end";

     // Genitivi's path in the Ruined temple.
     public const string URN_WP_GENITIVI_SEALED = "urn200wp_sealed_door";
     public const string URN_WP_GENITIVI_PILLARS = "urn200wp_pillars";
     public const string URN_WP_BRIDGE_PUZZLE = "urn230wp_bridge";

     // Combat waypoints.
     public const string URN_WP_BRONTO = "urn200wp_bronto";           // Bronto's ambush point
     public const string ASH_WRAITH_WP_PREFIX = "urn200wp_ash_wraith_";      // Respawn points for the Ash Wraiths

     //------------------------------------------------------------------------------
     // (3) PLACEABLES
     //------------------------------------------------------------------------------

     public const string URN_IP_FENCE = "urn100ip_fence";              // Fence to deactivate once Urn quest is complete.
     public const string URN_IP_BLOODY_ALTAR = "urn120ip_altar";              // Suspicious bloody altar in a peasant's hut
     public const string URN_IP_DEAD_KNIGHT = "urn130ip_deadknight";         // Arl Eamon's dead knight
     public const string URN_IP_TEMPLE_EXIT = "urn100ip_to_ruined_temple";   // This is the door that leads to the Ruined Temple
     public const string URN_IP_BRONTO_DOOR = "urn200ip_bronto_door";        // The door to the bronto's room
     public const string URN_IP_SEALED_DOOR = "urn200ip_sealed_door";        // The sealed door leading into the temple
     public const string URN_IP_OPEN_DOOR = "urn200ip_open_door";          // The opened version of the sealed door.
     public const string URN_IP_WRAITH_BRAZIER = "urn200ip_ash_wraith_brazier"; // The Ash wraith brazier.
     public const string URN_IP_CAVE_WALL = "urn200ip_wall";               // Cave wall to be removed at mountain top
     public const string URN_IP_SHADE_DOOR = "urn200ip_shade_door";         // Door to the shade ambush room
     public const string ASH_WRAITH_URN_PREFIX = "urn200ip_ash_wraith_urn_";    // Prefix for the Urns used by the Ash Wraiths
     public const string URN_IP_ARCHER_GATE = "urn200ip_archer_gate";        // Door out of hte archer/trap room.
     public const string URN_IP_GAUNTLET_DOOR = "urn230ip_guantlet_door";      // Door to the guantlet
     public const string URN_IP_SACRED_ASHES = "urn230ip_sacred_urn";         // The Urn of Sacred Ashes.
     public const string BRIDGE_SECTION_PREFIX = "urn230ip_bridge_";            // Sections in the bridge puzzle
     public const string BRIDGE_BLOCKER_PREFIX = "urn230ip_invisible_blocker_"; // Section blockers in the breige puzzle
     public const string URN_IP_GAUNTLET_ALTAR = "urn230ip_altar";              // Altar in the gauntlet
     public const string URN_IP_FIRE_WALL = "urn230ip_fire_wall";          // Invisible placeables for the fire wall
     public const string URN_IP_DPLG_DOOR_1 = "urn230ip_dplg_door_1";        // Doors in the room with doppelganger fight
     public const string URN_IP_DPLG_DOOR_2 = "urn230ip_dplg_door_2";        // Doors in the room with doppelganger fight
     public const string URN_IP_INV_STORE = "urn230ip_party_inv_";         // Prefix for inventory storage objects
     public const string URN_IP_FIREWALL = "urn230ip_fire_wall";          // Fire wall placeables.
     public const string URN_IP_RIDDLE_DOOR = "urn230ip_riddle_door";        // Riddle room door.
     public const string URN_IP_SACRED_URN = "urn230ip_sacred_urn";         // ....
     public const string URN_IP_RIDDLE_REWARD = "urn230ip_riddle_reward";      // Riddle room reward chest.

     public const string URN_IP_WEYLONS_CORPSE = "den270ip_weylon_corpse";

     public const string URN_IP_FIRE_SOUND_1 = "amb_ext_lrgfire_lp";
     public const string URN_IP_FIRE_SOUND_2 = "amb_ext_lrgfire_lp_2";

     public const string URN_IP_TEMPLE_TO_MOUNTAIN_TRANSITION = "urn200ip_to_mountain_top";

     //------------------------------------------------------------------------------
     // (4) TRIGGERS
     //------------------------------------------------------------------------------

     public const string URN_TR_VISION_TALK = "urn230tr_vision_talk";     // This is a trigger that makes the spirits speak
     public const string URN_TR_WRAITH_PRFX = "urn200tf_ash_wraiths_";    // Ash wraith activation triggers
     public const string URN_TR_SHOPKEEPER = "urn130tr_backroom_stopper";// Trigger's the shop-keeper to get upset about snoopy players
     public const string URN_TR_KOLGRIM_END = "urn100tr_kolgrim_end";     // Kolgrim's final trigger

     //------------------------------------------------------------------------------
     // (5) AREAS
     //------------------------------------------------------------------------------

     public const string URN_AR_DENERIM = "den270ar_genitivis_home";
     public const string URN_AR_PRINCESS = "cir110ar_inn";
     public const string URN_AR_HAVEN = "urn100ar_cultists_village";
     public const string URN_AR_RUINED_TEMPLE = "urn200ar_ruined_temple";
     public const string URN_AR_WYRMLINGS_LAIR = "urn210ar_wyrmlings_lair";
     public const string URN_AR_MOUNTAIN_TOP = "urn220ar_mountain_top";
     public const string URN_AR_GAUNTLET = "urn230ar_the_gauntlet";
     public const string URN_AR_VILLAGE_CHANTRY = "urn110ar_chantry";
     public const string URN_AR_VILLAGE_HOUSE = "urn120ar_village_house";
     public const string URN_AR_VILLAGE_SHOP = "urn130ar_village_shop";

     //------------------------------------------------------------------------------
     // (6) AREA LISTS
     //------------------------------------------------------------------------------

     public const string URN_AL_RUINED_VILLAGE = "urn01al_ruined_village";
     public const string URN_AL_RUINED_TEMPLE = "urn02al_ruined_temple";

     //------------------------------------------------------------------------------
     // (7) RESOURCES
     //------------------------------------------------------------------------------

     public const string URN_DG_BLOOD_MAGIC_CUTSCENE = "bhm100_cutscene_blood.dlg";      // Blood magic dialog
     public const string URN_DG_PARTY_BRIDGE_HELP = "urn230_party_bridge_help.dlg";   // Helpfull party tips on the bridge

     public const string URN_IT_MEDALLION_RESOURCE = "urn110ip_medallion.uti";         // Cultist medallion
     public const string URN_IT_RESEARCH_RESOURCE = "urn270im_research.uti";          // Genitivi's research
     public const string URN_IT_PEARL_R = "urn200im_pearl.uti";             // Pearl to dispel the brazier
     public const string URN_IT_TAPER_R = "urn200im_taper.uti";             // Taper to re-light the brazier
     public const string URN_IT_MH_KEY_R = "urn200im_wraith_key.uti";        // Key to the main hall of the temple
     public const string URN_IT_REWARD_R = "gen_im_acc_amu_urn.uti";        // Reward for the gauntlet
     public const string URN_IT_SACRED_ASHES_R = "urn230im_sacred_ashes.uti";      // The sacred ashes

     //------------------------------------------------------------------------------
     // (8) ITEMS
     //------------------------------------------------------------------------------

     public const string URN_IT_MEDALLION = "urn110ip_medallion";
     public const string URN_IT_TAPER = "urn200im_taper";
     public const string URN_IT_PEARL = "urn200im_pearl";
     public const string URN_IT_RESEARCH = "urn270im_research";
     public const string URN_IT_DRAGON_HORN = "urn210im_dragon_horn";

     //------------------------------------------------------------------------------
     //  (9) NUMERIC CONSTANTS
     //------------------------------------------------------------------------------

     // Chantry creature counters.
     public const int URN_N_CHANTRY_VILLAGERS = 11;
     public const int URN_N_CHANTRY_GUARDS = 3;

     // Teams
     public const int URN_TEAM_ASH_WRAITHS = 1;
     public const int URN_TEAM_KOLGRIM = 2;
     public const int URN_TEAM_GUARDIAN = 3;
     public const int URN_TEAM_VILLAGE_AMBUSH = 4;
     public const int URN_TEAM_KOLGRIM_WAVE_2 = 5;
     public const int URN_TEAM_DOPPELGANGER = 6;
     public const int URN_TEAM_SHADES = 7;
     public const int URN_TEAM_WYNNE = 9;
     public const int URN_TEAM_CHANTRY_VILLAGERS = 10;
     public const int URN_TEAM_LELIANA = 12;
     public const int URN_TEAM_GRAVESTONES = 13;
     public const int URN_TEAM_VILLAGE_POST_PLOT = 14;
     public const int URN_TEAM_KOLGRIM_MOUNTAINTOP = 15;
     public const int URN_TEAM_TEMPLE_ARCHER_GATE = 16;
}