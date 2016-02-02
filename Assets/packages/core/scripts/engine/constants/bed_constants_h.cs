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
     //:: Constants Include
     //:: Copyright (c) 2003 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         Handles constants for the Background - Elf Dalish module
     */
     //:://////////////////////////////////////////////
     //:: Created By: Cori
     //:: Created On: 12/05/06
     //:://////////////////////////////////////////////

     //creatures

     public const string BED_CR_BERESKARN = "bed110cr_bereskarn";
     public const string BED_CR_DUNCAN = "bed100cr_duncan";
     public const string BED_CR_FENAREL = "bed200cr_fenarel";
     public const string BED_CR_HUNTER1 = "bed100cr_hunter1";
     public const string BED_CR_HUNTER2 = "bed100cr_hunter2";
     public const string BED_CR_HUNTER3 = "bed100cr_hunter3";
     public const string BED_CR_KEEPER = "bed200cr_keeper";
     public const string BED_CR_MERRILL = "bed200cr_merrill";
     public const string BED_CR_SKELETON = "bed110cr_skeleton";
     public const string BED_CR_TAMLEN = "bed100cr_tamlen";
     public const string ZZ_BED_CR_DEBUG = "zz_bed110cr_debug";
     public const string BED_CR_CHILD1 = "bed200cr_child";
     public const string BED_CR_CHILD2 = "bed200cr_child2";
     public const string BED_CR_CHILD3 = "bed200cr_child3";
     public const string BED_CR_CHILD4 = "bed200cr_child4";
     public const string BED_CR_PAIVEL = "bed200cr_paivel";

     //placeables

     public const string BED_IP_MIRROR = "bed110ip_mirror";
     public const string BED_IP_MIRROR_DESTROYED = "bed110ip_mirror_destroyed";
     public const string BED_IP_MIRROR_DOOR = "bed110ip_mirror_door";
     public const string BED_IP_LOCKED_DOOR = "bed110ip_locked_door";
     public const string BED_IP_DOOR_SOUTH = "bed110ip_door_south";
     public const string BED_IP_CAMPFIRE = "bed100ip_campfire";
     public const string BED_IP_CLAW_TRAP_WEST = "bed110ip_claw_w";
     public const string BED_IP_CLAW_TRAP_EAST = "bed110ip_claw_e";

     //triggers
     public const string BED_TR_HURLOCK_EAST = "bed100tr_hurlock_east";
     public const string BED_TR_HURLOCK_WEST = "bed100tr_hurlock_west";
     public const string BED_TR_SPIDER_EAST = "bed110tr_spider_east";
     public const string BED_TR_SPIDER_WEST = "bed110tr_spider_west";
     public const string BED_TR_SPIDER_SOUTH = "bed110tr_spider_south";
     public const string BED_TR_MIRROR = "bed110tr_mirror";
     public const string BED_TR_MIRROR_ROOM = "bed110tr_mirror_room";
     public const string BED_TR_SPIDER_NORTH = "bed110tr_spider_north";

     //waypoints

     public const string BED_WP_UNCONSCIOUS = "bed100wp_unconscious";
     public const string BED_WP_PC_CAMP = "bed200wp_pc_camp";
     public const string BED_WP_FENAREL = "bed200wp_fenarel";
     public const string BED_WP_WARNING = "bed200wp_warning";
     public const string BED_WP_AFTER_CAMP = "bed100wp_after_camp";
     //public const string ZZ_BED_WP_DEBUG = "zz_bed110wp_debug";
     public const string BED_WP_FROM_FOREST = "bed200wp_from_forest";
     //public const string BED_WP_DUNCAN_RUINS = "bec110wp_duncan_ruins";
     public const string BED_WP_KEEPER_ARAVEL = "bed200wp_keeper_aravel";
     public const string ZZ_BED_WP_MIRROR_ROOM = "zz_bed110wp_mirror_room";
     public const string BED_WP_START = "bed100wp_start";
     public const string BED_WP_RUINS_FROM_FOREST = "bed110wp_from_forest";
     public const string BED_WP_FROM_CAMP = "bed100wp_from_camp";
     public const string BED_WP_MIRROR_TAMLEN = "bed110wp_mirror_tamlen";
     public const string BED_WP_DUNCAN_ARAVEL = "bed200wp_duncan_aravel";
     public const string BED_WP_BERESKARN = "bed110wp_bereskarn";
     public const string BED_WP_FENAREL_FIRE = "bed200wp_fenarel_fire";
     public const string BED_WP_DEAD_HUNTER_1 = "bed100wp_dead_hunter_1";
     public const string BED_WP_DEAD_HUNTER_2 = "bed100wp_dead_hunter_2";
     public const string BED_WP_DEAD_HUNTER_3 = "bed100wp_dead_hunter_3";

     //areas
     public const string BED_AR_FOREST_CLEARING = "bed100ar_forest_clearing";
     public const string BED_AR_DALISH_CAMP = "bed200ar_dalish_camp";
     public const string BED_AR_ELVEN_RUINS = "bed110ar_elven_ruins";

     //dialog strings
     public const string ZZ_BED_DG_DEBUG = "zz_bed000dg_debug.dlg";
     public const string rBED_IP_TAINTED_MIRROR = "bed110ip_tainted_mirror.dlg";

     //item strings
     public const string rBED_IM_KEY_HEIRLOOM = "bed200im_key_heirloom.uti";

     //constants

     public const int BED_TEAM_HURLOCK_EAST = 1;
     public const int BED_TEAM_HURLOCK_WEST = 2;
     public const int BED_TEAM_SKELETON = 3;
     public const int BED_TEAM_SPIDER_EAST = 4;
     public const int BED_TEAM_SPIDER_WEST = 5;
     public const int BED_TEAM_HURLOCK_EAST_RUINS = 6;
     public const int BED_TEAM_HURLOCK_WEST_RUINS = 7;
     public const int BED_TEAM_HURLOCK_SOUTH_RUINS = 8;
     public const int BED_TEAM_SPIDER_NORTH_RUINS = 9;
     public const int BED_TEAM_HURLOCK_DEAD = 10;
     public const int BED_TEAM_CLAW_TRAP_WEST = 100;
     public const int BED_TEAM_CLAW_TRAP_EAST = 101;
     public const int BED_TEAM_RUINS_SKELETONS = 110;

     public const int BED_CUTSCENE_DUNCAN_BREAKS_MIRROR = 41;
}