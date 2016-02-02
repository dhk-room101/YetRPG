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
     ///////////////////////////////////////////////////////////////////////////////
     //  camp_constants_h
     // Constants file for the party camp
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By:Cori
     //  Created On:07/03/08
     ///////////////////////////////////////////////////////////////////////////////

     // -----------------------------------------------------
     // creatures
     // -----------------------------------------------------
     public const string CAMP_BODAHN = "camp_bodahn";
     public const string CAMP_SANDAL = "camp_sandal";
     public const string CAMP_DARKSPAWN_TAMLEN = "cam110cr_tamlen_darkspawn";
     public const string CAMP_EMISSARY_DALISH = "lite_camp_emiss_dalish";
     public const string CAMP_EMISSARY_DWARF = "lite_camp_emiss_dwarf";
     public const string CAMP_EMISSARY_EAMON = "lite_camp_emiss_eamon";
     public const string CAMP_EMISSARY_TRANQUIL = "lite_camp_emiss_tranq";
     public const string CAMP_EMISSARY_WEREWOLF = "lite_camp_emiss_were";
     public const string CAMP_NUG = "cam100cr_nug_leliana";
     public const string CAMP_SHRIEK_ATTACKER_NORM = "cam110cr_shriek_norm";

     //------------------------------------------------------
     // Merchants
     //------------------------------------------------------
     public const string STORE_CAMP_BODAHN = "store_camp_bodahn";

     // -----------------------------------------------------
     // Placeables
     // -----------------------------------------------------
     public const string CAMP_PL_WAGON = "genip_wagon";
     public const string CAMP_PL_ALLIED_SUPPLIES = "liteip_camp_turnin";
     // -----------------------------------------------------
     // Teams
     // -----------------------------------------------------
     public const int CAMP_TEAM_DARKSPAWN_CAMP_ATTACKERS = 1;
     public const int CAMP_TEAM_TAMLEN = 2;

     // -----------------------------------------------------
     // dialogues
     // -----------------------------------------------------

     public const string ZZ_RUMOUR_DEBUG = "zz_rumour_debug.dlg";
     public const string CAM_POST_SHRIEK_ATTACK = "cam110_reaction.dlg";

     // -----------------------------------------------------
     // waypoints
     // -----------------------------------------------------
     public const string CAM_WP_ENTRANCE = "cam100wp_entrance";
     public const string CAM_WP_TAMLEN_WALK_AWAY = "camp110wp_tamlen_walk_away";
     public const string ZZ_CAM_WP_RUMOUR = "zz_cam104wp_rumour";           // for testing the rumour plot
     public const string ZZ_CAM_WP_BODAHN = "zz_cam110wp_bodahn";           // for testing the rumour plot
     public const string WP_CAMP_GEN_FL_ALISTAIR = "wp_camp_gen00fl_alistair";
     public const string WP_CAMP_GEN_FL_MORRIGAN = "wp_camp_gen00fl_morrigan";
     public const string WP_CAMP_GEN_FL_WYNNE = "wp_camp_gen00fl_wynne";
     public const string WP_CAMP_GEN_FL_STEN = "wp_camp_gen00fl_sten";

     // -----------------------------------------------------
     // areas
     // -----------------------------------------------------
     public const string CAM_AR_CAMP_PLAINS = "cam100ar_camp_plains";
     public const string CAM_AR_ARCH1 = "cam104ar_camp_arch1";
     public const string CAM_AR_ARCH3 = "cam110ar_camp_arch3";
     public const string CAM_CASTLE_CLIMAX = "cli300ar_redcliffe_castle";

     // -----------------------------------------------------
     // constants to go into the random constants
     // as soon as it's checked back in
     // -----------------------------------------------------
}