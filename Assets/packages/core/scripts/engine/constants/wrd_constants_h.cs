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
     //==============================================================================
     /*
         List of constants for the West Road
     */
     //==============================================================================
     //  Created By: Ferret Baudoin
     //  Created On: Feb. 28, 2007
     //==============================================================================
     //  Modified By: Kaelin
     //==============================================================================

     //public void main() {}

     //------------------------------------------------------------------------------
     // CREATURES
     //------------------------------------------------------------------------------

     // Major Characters
     public const string WRD_CR_ALLISON = "wrd110cr_allison";               // Allison, widow farmer and quest-giver
     public const string WRD_CR_BARLIN = "wrd110cr_barlin";                 // Barlin, shop owner
     public const string WRD_CR_FELERRON = "wrd110cr_felerron";             // Felerron (Master), mine owner
     public const string WRD_CR_JACOBSON = "wrd111cr_jacobson";             // Jacobson (Lieutenant), army lt
     public const string WRD_CR_KARALEN = "wrd100cr_karalen";               // Karalen (Mistress), Scholar quest-giver
     public const string WRD_CR_OXLEY = "wrd100cr_oxley";                   // Oxley (Ser), Arl Eamon questing knight
     public const string WRD_CR_MIRIAM = "wrd110cr_miriam";                 // Elder miriam

     // Minor Characters
     public const string WRD_CR_ANGRY_SERGEANT = "wrd100cr_angry_sergeant"; // Beleageured Sergeant, for the angry soldier encounter
     public const string WRD_CR_ANGRY_SOLDIER_1 = "wrd100cr_angry_soldier_1"; // Observant Soldier, for the angry soldier encounter
     public const string WRD_CR_ANGRY_SOLDIER_2 = "wrd100cr_angry_soldier_2"; // Tired Soldier, for the angry soldier encounter
     public const string WRD_CR_ANGRY_SOLDIER = "wrd100cr_angry_soldier_gen"; // Tired Soldier, for the angry soldier encounter
     public const string WRD_CR_ARMY_SCOUT = "wrd111cr_army_scout";         // Scout to Lt. Jacobson
     public const string WRD_CR_CODY = "wrd112cr_cody";                     // Cody, one of the trainees on Trial Run
     public const string WRD_CR_GAERRON = "wrd110cr_gaerron";               // Gaerron, desperate father looking for son (Uller)
     public const string WRD_CR_MENDEL = "wrd112cr_mendel";                 // Mendel, one of the trainees on Trial Run
     public const string WRD_CR_MISSING_MINER = "wrd100cr_missing_miner";   // Missing Miner for Felerron
     public const string WRD_CR_REFUGEE = "wrd100cr_refugee";               // Refugee (generic), for the refugee encounter
     public const string WRD_CR_REFUGEE_1 = "wrd100cr_refugee_leader";      // Nervous Refugee, for the refugee encounter
     public const string WRD_CR_REFUGEE_2 = "wrd100cr_refugee_1";           // Tough Refugee, for the refugee encounter
     public const string WRD_CR_RHUGGERS = "wrd100cr_rhuggers";             // Rhuggers (Captain), evil PC encounter
     public const string WRD_CR_RHUGGERS_1 = "wrd100cr_rhug_flynn";         // "One-Eye" Flynn, evil PC groupie
     public const string WRD_CR_ROSSLEIGH_VILLAGER = "wrd100cr_villager";   // Villager, from Rossleigh
     public const string WRD_CR_SAEVRIN = "wrd112cr_saevrin";               // Saevrin, one of the trainees on Trial Run
     public const string WRD_CR_SERGEANT_DULTRY = "wrd111cr_sergeant";      // Sergeant Dultry, under Lt. Jacobson
     public const string WRD_CR_ULLER = "wrd110cr_uller";                   // Uller, missing Blight-tainted boy
     public const string WRD_CR_TELMANES = "wrd100cr_telmanes";
     public const string WRD_CR_TELMANES_DEAD = "wrd100cr_telmanes_dead";

     // Fennon's Down Party
     public const string WRD_CR_COLLIN = "wrd120cr_collin";                 // Collin, the squire
     public const string WRD_CR_DANCING = "wrd120cr_dancing";               // Ser Ely Dancing, knight and son of a Bann
     public const string WRD_CR_FELDERA = "wrd120cr_feldera";               // Feldera, clever rogue lady
     public const string WRD_CR_SORINSEN = "wrd120cr_sorinsen";             // Sorinsen the Hawk, their smart ranger
     public const string WRD_CR_ZANDARES = "wrd120cr_zandares";             // Master Zandares, dwarven "master" trader

     // Fennon Cave mobs
     public const string LOT_CR_FD_ENTRY_DARKSPAWN = "lot101cr_entry_darkspawn";
     public const string LOT_CR_FD_CENTER_DARKSPAWN = "lot101cr_center_darkspawn";
     public const string LOT_CR_FD_RIGHT_DARKSPAWN = "lot101cr_right_darkspawn";
     public const string LOT_CR_FD_LEFT_DARKSPAWN = "lot101cr_left_darkspawn";
     public const string LOT_CR_FD_BACK_DARKSPAWN = "lot101cr_back_darkspawn";

     // Remembered Chantry characters
     public const string WRD_CR_BROTHER_BUSY = "wrd130cr_brother_busy";     // Busy brother, casanova
     public const string WRD_CR_BROTHER_YOUNG = "wrd130cr_brother_young";   // Young brother, attentive
     public const string WRD_CR_DAUGHTER = "wrd130cr_daughter";             // Concerned Daughter, trying to rescue her father
     public const string WRD_CR_DERANGED_PRISONER = "wrd130cr_deranged_prisoner";   // Deranged Prisoner, drugged pyromaniac
     public const string WRD_CR_ELDER_ANXIOUS = "wrd130cr_elder_anxious";   // Anxious Elder, fussy old man
     public const string WRD_CR_ELDER_FRIENDLY = "wrd130cr_elder_friendly"; // Friendly Elder, two-faced and on the take
     public const string WRD_CR_GHOST_TELMANES = "wrd130cr_ghost_telmanes"; // Ghost of Telmanes, the quest-giver
     public const string WRD_CR_MOTHER_DEMANDING = "wrd130cr_mother_demanding";     // Demanding Mother, bitchy embezzler
     public const string WRD_CR_MOTHER_QUIET = "wrd130cr_mother_quiet";     // Quiet Mother, secretly in love
     public const string WRD_CR_PIOUS_HUSBAND = "wrd130cr_pious_husband";   // Pious Husband, old and upright citizen
     public const string WRD_CR_PIOUS_WIFE = "wrd130cr_pious_wife";         // Pious Wife, younger and randy
     public const string WRD_CR_REVERED_MOTHER = "wrd130cr_revered_mother"; // Revered Mother, stern
     public const string WRD_CR_TEMPLAR = "wrd130cr_templar";               // Templar, one of the guards
     public const string WRD_CR_TEMPLAR_2 = "wrd130cr_templar_2";           // Templar, one of the guards

     public const string WRD_IP_BRAZIER = "wrd130ip_brazier";               // Brazier, click on this to exit the area
     public const string WRD_IP_DOOR = "wrd130ip_door";                     // Door, let's you know you're a ghost
     public const string WRD_IP_DOOR_LOCK = "wrd130ip_door_lock";           // Locked Door, stopping the daughter
     public const string WRD_IP_PREPTABLE = "wrd130ip_preptable";           // Prep Table, where Friendly makes his poison
     public const string WRD_IP_REVMOM_NOTE = "wrd130ip_revmom_note";       // Note, about demonic possession
     public const string WRD_IP_VASE = "lot182ip_vase";                     // Vase, can be broken

     // Enemies
     public const string WRD_CR_BROODMOTHER = "wrd101cr_broodmother";       // Broodmother in Broodmother Cave

     //------------------------------------------------------------------------------
     // TEAMS
     //------------------------------------------------------------------------------
     public const int WRD_TEAM_DARK_TIDE_ENEMIES_NORTH = 11;
     public const int WRD_TEAM_DARK_TIDE_ENEMIES_SOUTH = 12;
     public const int WRD_TEAM_DARK_TIDE_ENEMIES_SQUARE = 13;
     public const int WRD_TEAM_CHANTRY_GHOSTS = 14;
     public const int WRD_TEAM_ULLER_CAVE = 15;
     public const int WRD_TEAM_ULLER_CAVE_2 = 16;
     public const int WRD_TEAM_FENNON_DOWN = 26;
     public const int WRD_TEAM_BROODMOTHER_SPIDERS = 27;
     public const int WRD_TEAM_BROODMOTHER_BERESKARN = 28;
     public const int WRD_TEAM_BROODMOTHER_ENTRANCE_SPIDERS = 29;
     public const int WRD_TEAM_BROODMOTHER_DEAD_THINGS = 30;
     public const int WRD_TEAM_BROODMOTHER = 31;
     public const int WRD_TEAM_BROODMOTHER_TENTACLE = 32;
     public const int WRD_TEAM_CHANTRY_SPIDERS_1 = 33;
     public const int WRD_TEAM_CHANTRY_SPIDERS_2 = 34;

     //------------------------------------------------------------------------------
     // WAYPOINTS
     //------------------------------------------------------------------------------

     public const string WRD_WP_PC_TO_TRIAL_RUN = "wrd112wp_to_trial_run";
     public const string WRD_WP_PC_TO_ROSSLEIGH_FOR_ULLER = "wrd110wp_to_rossleigh_for_uller";
     public const string WRD_WP_PC_TO_GHOST_CHANTRY = "wrd130wp_start";
     public const string WRD_WP_PC_LEAVES_GHOST_CHANTRY = "wrd100wp_from_ghostchantry";
     public const string WRD_WP_PC_TO_CHANTRY_BASEMENT = "lot185wp_from_lothering";

     //------------------------------------------------------------------------------
     // PLACEABLES
     //------------------------------------------------------------------------------
     public const string WRD_IP_BARLIN_CRATE = "wrd110ip_barlin_crate";     // Barlin's Crates (One Merchant's Loss...)
     public const string WRD_IP_BROOD_BLOCKER = "lot105ip_brood_blocker";    // Blocks entrance to broodmother's cave
     public const string WRD_IP_PREP_TABLE = "lot182ip_prep_table";       // Alchemical prep table in Ghost Chantry
     public const string WRD_IP_VASE_TABLE = "lot182ip_vase_table";       // Table that the ghost vase sits on
     public const string WRD_IP_GHOST_VASE = "lot182ip_vase";             // The ghost vase in the ghost chantry
     public const string WRD_IP_GHOST_BRAZIER = "lot182ip_brazier";          // The brazier in the ghost chantry
     public const string WRD_IP_REV_NOTE = "lot182ip_rev_note";         // Note on the revered mothers desk in ghost chantry.
     public const string WRD_IP_NOTE_CHEST = "lot182ip_note_chest";       // Chest where the conspiracy note is found in ghost chantry.
     public const string WRD_IP_ANTIDOTE_CHEST = "lot182ip_anitdote_chest";   // Chest that holds the antidote in the ghost chantry.
     public const string WRD_IP_TELMANES = "lot105ip_telmanes";
     public const string WRD_IP_DOOR_BROODMOTHER = "wrd100ip_to_broodmother";

     //------------------------------------------------------------------------------
     // TRIGGERS
     //------------------------------------------------------------------------------
     //public const string URN_TR_VISION_TALK = "urn230tr_vision_talk";       // This is a trigger that makes the spirits speak

     //------------------------------------------------------------------------------
     // ITEMS
     //------------------------------------------------------------------------------
     public const string WRD_IT_TATTERED_NOTE = "ran420im_note";
     public const string WRD_R_IT_TATTERED_NOTE = "ran420im_note.uti";
     public const string WRD_R_IT_POISON = "lot182it_ghost_poison.uti";
     public const string WRD_R_IT_ANTIDOTE = "lot182it_ghost_antidote.uti";
     public const string WRD_R_IT_TELMANES_KEY = "lot100im_telmanes_key.uti";

     //------------------------------------------------------------------------------
     // AREAS
     //-----------------------------------------------------------------------------

     public const string WRD_AR_FENNON_CAVE = "lot181ar_fennoncave";
     public const string WRD_AR_ULLER_CAVE = "lot180ar_ullercave";
     public const string WRD_AR_BROODMOTHER = "lot183ar_broodmother_cave";
     public const string WRD_AR_GHOST_CHANTRY = "lot182ar_ghostchantry";
     public const string WRD_AR_EAGHUNS_MINE = "lot184ar_eaghuns_gulch_mine";
     public const string WRD_AR_CHANTRY_BASEMENT = "lot185ar_chantry_basement";

     //------------------------------------------------------------------------------
     // AREA LISTS
     //------------------------------------------------------------------------------
     //public const string URN_AL_RUINED_VILLAGE = "urn01al_ruined_village";

     //------------------------------------------------------------------------------
     // CONVERSATIONS
     //------------------------------------------------------------------------------
     public const string WRD_DG_TRAINING_MONTAGE = "wrd112_training_montage.dlg";
     public const string WRD_DG_FENNON_PARTY = "lot181lt_fennon_party.dlg";
     public const string WRD_DG_TEMPLAR_VASE = "lot182lt_templar_vase.dlg";
     public const string WRD_DG_BROTHER_BUSY = "lot182lt_brother_busy.dlg";
     public const string WRD_DG_BROTHER_YOUNG = "lot182lt_brother_young.dlg";
     public const string WRD_DG_DAUGHTER = "lot182lt_daughter.dlg";
     public const string WRD_DG_DERANGED_PRISONER = "lot182lt_deranged_prisoner.dlg";
     public const string WRD_DG_ELDER_ANXIOUS = "lot182lt_elder_anxious.dlg";
     public const string WRD_DG_ELDER_FRIENDLY = "lot182lt_elder_friendly.dlg";
     public const string WRD_DG_GHOST_TELMANES = "lot182lt_ghost_telmanes.dlg";
     public const string WRD_DG_MOTHER_DEMANDING = "lot182lt_mother_demanding.dlg";
     public const string WRD_DG_MOTHER_QUIET = "lot182lt_mother_quiet.dlg";
     public const string WRD_DG_PIOUS_HUSBAND = "lot182lt_pious_husband.dlg";
     public const string WRD_DG_REVERED_MOTHER = "lot182lt_revered_mother.dlg";
     public const string WRD_DG_TEMPLAR = "lot182lt_templar.dlg";
     public const string WRD_DG_PREP_TABLE = "lot182lt_preptable.dlg";

     //------------------------------------------------------------------------------

     public const string WRD_DESERTERS_SENT_TO_ROSSLEIGH = "WRD_DESERTERS_SENT_TO_ROSSLEIGH";
     public const string WRD_JACOBSON_FIRST_BATTLE_LOSSES = "WRD_JACOBSON_FIRST_BATTLE_LOSSES";
}