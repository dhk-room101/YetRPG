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
     //:: mtn_constants_h
     //:: Copyright (c) 2007 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         List of constants for the Mountain Pass
     */
     //:://////////////////////////////////////////////
     //:: Created By: Ferret Baudoin
     //:: Created On: Apr. 26, 2007
     //:://////////////////////////////////////////////

     //public void main() {}

     /**************/
     // CREATURES
     /**************/

     // Major Characters
     public const string MTN_CR_CESAR = "mtn100cr_cesar";                   // Cesar, a Calabrian trader (secretly a Crow);
     public const string MTN_CR_IGNACIO = "mtn100cr_ignacio";               // Master Ignacio, a master trader - actually in charge of the Crows
     public const string MTN_CR_VALAZZ = "mtn101cr_valazz";                 // Valazz Glavonak, leader of the rebel Glavonaks

     // Minor Characters
     public const string MTN_CR_BANDIT_LEADER = "mtn101cr_dwarf_leader";    // Glavonak Bandit Leader
     public const string MTN_CR_BANDIT_FLUNKY = "mtn101cr_dwarf_subordinate"; // Glavonak Bandit Flunky
     public const string MTN_CR_CULT_END_1 = "mtn110cr_cultist_reverent";   // End Cultist
     public const string MTN_CR_CULT_LEADER = "mtn110cr_cultists_leader";   // Cultist Leader
     public const string MTN_CR_CULT_SPY_1 = "mtn100cr_cultist_old";        // Old Cultist (whom you can overhear)
     public const string MTN_CR_CULT_SPY_2 = "mtn100cr_cultist_eager";      // Eager Cultist (whom you can overhear)
     public const string MTN_CR_HORNED_ONE = "mtn110cr_horned_one";         // Horned One, demon summoned by Cult
     public const string MTN_CR_KEVHANAR = "mtn102cr_kevhanar";             // Khevenar Glavonak, escort to the Rest

     /**************/
     // WAYPOINTS
     /**************/
     // The PC goes to the Trial Run trainees
     //public const string WRD_WP_PC_TO_TRIAL_RUN = "wrd112wp_to_trial_run";

     /**************/
     // PLACEABLES
     /**************/
     // This is the door to the cultist ruins
     public const string MTN_IP_CULTIST_RUINS_DOOR = "mtn100ip_to_cultists_ruins";

     /**************/
     // TRIGGERS
     /**************/
     //public const string URN_TR_VISION_TALK = "urn230tr_vision_talk";       // This is a trigger that makes the spirits speak

     /**************/
     // AREAS
     /**************/
     /*public const string WRD_AR_FENNON_CAVE = "wrd120ar_fennoncave";
     public const string WRD_AR_ULLER_CAVE = "wrd110ar_ullercave";
     public const string WRD_AR_WEST_ROAD = "wrd100ar_west_road";
     public const string WRD_AR_GHOST_CHANTRY = "wrd130ar_ghostchantry";*/

     /**************/
     // AREA LISTS
     /**************/
     //public const string URN_AL_RUINED_VILLAGE = "urn01al_ruined_village";

     /**************/
     // CONVERSATIONS
     /**************/
     public const string ZZ_MTN_DG_DEBUG = "zz_mtn_debug.dlg";
}