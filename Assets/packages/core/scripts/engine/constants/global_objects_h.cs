using UnityEngine;
using System.Collections;

public static partial class EngineConstants
{
     // This include file defines global objects used all through the game

     /*******************************************************************************
     * CORE RESOURCES
     *******************************************************************************/

     public const string RESOURCE_SCRIPT_RULES_CORE = "rules_core";
     public const string RESOURCE_SCRIPT_ABILITY_CORE = "ability_core";
     public const string RESOURCE_SCRIPT_CREATURE_CORE = "creature_core";
     public const string RESOURCE_SCRIPT_PLAYER_CORE = "player_core";
     public const string RESOURCE_SCRIPT_AOE_CORE = "aoe_core";
     public const string RESOURCE_SCRIPT_AREA_CORE = "area_core";
     public const string RESOURCE_SCRIPT_MODULE_CORE = "module_core";
     public const string RESOURCE_SCRIPT_PLACEABLE_CORE = "placeable_core";
     public const string RESOURCE_SCRIPT_TRIGGER_CORE = "trigger_core";
     public const string RESOURCE_SCRIPT_TRAP_STATUE = "trap_statue";
     public const string RESOURCE_SCRIPT_MODULE_CORE_START = "sp_module_start";
     public const string RESOURCE_SCRIPT_MODULE_CORE_TRAVEL = "sp_module_travel";
     public const string RESOURCE_SCRIPT_MODULE_ITEM_ACQUIRED = "sp_module_item_acq";
     public const string RESOURCE_SCRIPT_MODULE_ITEM_LOST = "sp_module_item_lost";
     public const string RESOURCE_SCRIPT_MODULE_SET_GAME_MODE = "sp_module_set_game_mode";
     public const string RESOURCE_SCRIPT_MODULE_PPICKER_CLOSED = "sp_module_ppicker_closed";

     /*******************************************************************************
     * CORE CONVERSATION RESOURCES
     *******************************************************************************/
     public const string GEN_DL_CAMP_EVENTS = "party_camp.dlg";
     public const string GEN_DL_PARTY_EVENTS = "party_events.dlg";

     /*******************************************************************************
     * FOLLOWERS
     *******************************************************************************/

     public const string GEN_FL_ALISTAIR = "gen00fl_alistair";
     public const string GEN_FL_DOG = "gen00fl_dog";
     public const string GEN_FL_MORRIGAN = "gen00fl_morrigan";
     public const string GEN_FL_WYNNE = "gen00fl_wynne";
     public const string GEN_FL_SHALE = "gen00fl_shale";
     public const string GEN_FL_STEN = "gen00fl_sten";
     public const string GEN_FL_ZEVRAN = "gen00fl_zevran";
     public const string GEN_FL_OGHREN = "gen00fl_oghren";
     public const string GEN_FL_LELIANA = "gen00fl_leliana";
     public const string GEN_FL_LOGHAIN = "gen00fl_loghain";

     /*******************************************************************************
     * ITEMS
     * NOTE: these must be in a core global file since they are referenced in
     * player_core
     *******************************************************************************/
     public const string GEN_IM_GIFT_DUNCAN_SHIELD = "gen_im_gift_duncan_shield";
     public const string GEN_IM_GIFT_ALISTAIR_AMULET = "gen_im_gift_alistair_amulet";
     public const string GEN_IM_GIFT_DALISH_GLOVES = "gen_im_gift_dalish_gloves";
     public const string GEN_IM_GIFT_ANTIVAN_BOOTS = "gen_im_gift_antivan_boots";

     /*******************************************************************************
     * Areas
     *******************************************************************************/

     public const string GEN_AR_CAMP = "gen00ar_camp";
     public const string PRE_AR_KINGS_CAMP = "pre100ar_kings_camp";

     /*******************************************************************************
     * Area Lists
     *******************************************************************************/

     public const string RAN01AL_PLAINS = "ran01al_plains";

     /*******************************************************************************
     * Waypoints
     *******************************************************************************/

     public const string RAN110WP_START = "ran110wp_start";
     public const string PRE_WP_START = "prelude_start";

     /*******************************************************************************
     * XP Rewards
     *******************************************************************************/
     public const int XP_CODEX = 50;

     /*******************************************************************************
     * Codex
     *******************************************************************************/
     public const int CODEX_COUNT_LEVEL_1 = 1;
     public const int CODEX_COUNT_LEVEL_2 = 3;
     public const int CODEX_COUNT_LEVEL_3 = 7;
}