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
         cir_constants_h.nss
         List of constants for the Broken Circle
     */
     //==============================================================================
     //  Created By: Ferret Baudoin
     //  Created On: Dec. 14, 2006
     //==============================================================================

     //public void main() {}

     //------------------------------------------------------------------------------
     // Abilities
     //------------------------------------------------------------------------------

     public const int CIR_FADE_FORM_MOUSE = 100081;
     public const int CIR_FADE_FORM_ARCANE_HORROR = 100082;

     public const int CIR_FADE_SHADE_INVISIBLITY = 100084;

     //------------------------------------------------------------------------------
     // CREATURES
     //------------------------------------------------------------------------------
     // Major Characters
     public const string CIR_CR_CARROLL = "cir100cr_carroll";                   // Carroll, (out) crazy templar ferryman
     public const string CIR_CR_CULLEN = "cir230cr_cullen";                    // Cullen, (4) tortured templar
     public const string CIR_CR_GREAGOIR = "cir200cr_greagoir";                  // Greagoir, (1) Knight-Commander of the Templars
     public const string CIR_CR_IRVING = "cir240cr_irving";                    // Irving, (5) First Enchanter of the Circle
     public const string CIR_CR_IRVING_POST_PLOT = "cir240cr_irving_post";               // Irving, post plot reference.
     public const string CIR_CR_IRVING_FOURTH_FLOOR = "cir240cr_irving";
     public const string CIR_CR_OWAIN = "cir210cr_owain";                     // Owain, (1) tranquil storekeeper + info giver
     public const string CIR_CR_SLOTH_DEMON = "cir300cr_sloth_demon";               // Sloth Demon, (fade) the big bad
     public const string CIR_CR_SLOTH_DEMON_FINAL = "cir300cr_sloth_demon_f";             // Sloth Demon final form, (fade) the big bad
     public const string CIR_CR_SLOTH_ON_4TH = "cir230cr_sloth_demon";               // Sloth Demon, (4) in the Real World
     public const string CIR_CR_ULDRED = "cir240cr_uldred";                    // Uldred, (5) possessed mage
     public const string CIR_CR_ULDRED_HUMAN = "cir240cr_uldred_human";              // Uldred in human form

     // Minor Characters
     public const string CIR_CR_ABOMINATION = "cir240cr_abomination";               // Abomination, (5) Uldred's lackey
     public const string CIR_CR_GODWIN = "cir210cr_godwin";                    // Godwin, (2) lyrium smuggler
     public const string CIR_CR_KEILI = "cir200cr_keili";                     // Keili, (1) insane religious mage
     public const string CIR_CR_KESTER = "cir100cr_kester";                    // Kester, (out) former ferryman
     public const string CIR_CR_KESTER_POST_PLOT = "cir100cr_kester_post_plot";          // Kester, (out) current ferryman post plot
     public const string CIR_CR_KID = "cir200cr_kids";                      // Child, (1) mage student kids that Wynne is looking after
     public const string CIR_CR_KINNON = "cir200cr_kinnon";                    // Kinnon, (1) Wynne's apprentice
     public const string CIR_CR_MERCHANT = "cir100cr_merchant";                  // Merchant, (out) offers a light content quest
     public const string CIR_CR_NIALL = "cir300cr_niall";                     // Niall, (fade) heroic mage
     public const string CIR_CR_NIALLS_BODY = "cir230cr_nialls_body";               // Niall's Body, (4) can search for Litany
     public const string CIR_CR_PETRA = "cir200cr_petra";                     // Petra, (1) Wynne's apprentice
     public const string CIR_CR_QUARTERMASTER = "cir200cr_shopman";                   // Templar Quartermaster (1)
     public const string CIR_CR_TEMPLAR = "cir200cr_templar";                   // Templar, (1) generic
     public const string CIR_CR_TRANQUIL = "cir210cr_gentled";                   // Tranquil, (2) rescued guy
     public const string CIR_CR_MOUSE_APPRENTICE = "cir300cr_mage_apprentice";           // Mouse form mage apprentice
     public const string CIR_CR_WOUNDED_MAGE = "cir200cr_wounded";                   // Wounded mage at the end
     public const string CIR_CR_WOUNDED_TEMPLAR = "cir200cr_wounded_templar";           // Wounded templar at the end
     public const string CIR_CR_THANKFUL_MAGE = "cir200cr_thankful_mage";             // Mage that is thankful at the end
     public const string CIR_CR_THANKFUL_MAGE_2 = "cir200cr_thankful_mage_2";           // Second mage that is thankful at the end
     public const string CIR_CR_POST_TEMPLAR = "cir200cr_post_templar";              // Post templar
     public const string CIR_CR_POST_TEMPLAR_2 = "cir200cr_post_templar_2";            // Post templar
     public const string CIR_CR_MEMORIAL_TEMPLAR = "cir100cr_memorial_templar";          // Post templar outside
     public const string CIR_CR_SACR_TRANQUIL = "cir220cr_sacrificial_tranq";         // The tranquil that are going to be used in a ritual

     // Encounter Characters
     public const string CIR_CR_BLOOD_MAGE_1_1 = "cir210cr_blood_mage_01";             // Blood mage (2nd floor)
     public const string CIR_CR_BLOOD_MAGE_1_2 = "cir210cr_blood_mage_02";             // Blood mage (2nd floor)
     public const string CIR_CR_BLOOD_MAGE_1_3 = "cir210cr_blood_mage_03";             // Blood mage (2nd floor)

     public const string CIR_CR_DESIRE_DEMON = "cir220cr_desire_demon";              // Desire Demon (3)
     public const string CIR_CR_DESIRE_TEMPLAR = "cir220cr_desire_templar";            // Bewitched Templar, (3) Desire has under control

     public const string CIR_CR_BLOOD_MAGE_2_1 = "cir230cr_blood_mage_01";             // Blood mage (4)
     public const string CIR_CR_BLOOD_MAGE_2_2 = "cir230cr_blood_mage_02";             // Blood mage (4)
     public const string CIR_CR_BMS_ABOMINATION = "cir230cr_cs_abomination";            // Abomination that interrupts them
     public const string CIR_CR_TRANQUIL_TRANSFORM = "cir220cr_tranquil_mon";                         // Creature the tranquil transform into
     public const string CIR_CR_ENRAGED = "corpse_enraged";
     public const string CIR_CR_TEMPLAR_DESIRE = "cir230cr_tempar_desire";
     public const string CIR_CR_ASH_WRAITH = "cir230cr_ash_wraith";
     public const string CIR_CR_MAGE1 = "cir240cr_mage1";
     public const string CIR_CR_MAGE2 = "cir240cr_mage2";
     public const string CIR_CR_MAGE3 = "cir240cr_mage3";
     public const string CIR_CR_MAGE4 = "cir240cr_mage4";
     public const string CIR_CR_THIRD_FLOOR_ABOM = "cir230cr_abomination2";
     public const string CIR_CR_SUMMONED_ABOM = "cir240cr_summoned_aboms";
     public const string CIR_CR_RAGE_DEMON = "rage_demon";
     public const string CIR_CR_SHADE_BOSS = "cir300cr_shade_boss";
     public const string CIR_CR_OGRE_BOSS = "cir320cr_ogre_boss";
     public const string CIR_CR_SUCCUBUS_BOSS = "cir340cr_succubus_boss";
     public const string CIR_CR_ABOMINATION_BOSS = "cir330cr_hunger_demon_boss";
     public const string CIR_CR_RAGE_BOSS = "cir310cr_rage_demon_boss";
     public const string CIR_CR_SLOTH_MOUSE = "rat_giant";
     public const string CIR_CR_ARCANE_HORROR = "cir220cr_arcane_horror";

     // Fade Characters
     public const string CIR_CR_CAILAN = "cir300cr_cailan";                    // Prince Cailin, from Loghain's dream
     public const string CIR_CR_DEAD_MAGE_1 = "cir300cr_dead_apprentice";           // Wynne's dead apprentices
     public const string CIR_CR_DEAD_MAGE_2 = "cir300cr_dead_apprentice2";
     public const string CIR_CR_DEAD_MAGE_3 = "cir300cr_dead_apprentice3";
     public const string CIR_CR_CRAZY_MAGE_2 = "cir330cr_crazy_mage_2";
     public const string CIR_CR_CRAZY_MAGE_3 = "cir330cr_crazy_mage_3";
     public const string CIR_CR_DUNCAN = "cir300cr_duncan";                    // Duncan, Fade version of PC mentor
     public const string CIR_CR_FLEMETH = "cir300cr_flemeth";                   // Flemeth, Morrigan's mother
     public const string CIR_CR_GOLDANNA = "cir300cr_goldanna";                  // Goldanna, Alistair's sister
     public const string CIR_CR_GREY_WARDEN = "cir300cr_warden";                    // Grey Warden, Fade Warden from Weisshaupt
     public const string CIR_CR_MARIC = "cir300cr_maric";                     // King Maric, from Loghain's dream
     public const string CIR_CR_OGHREN_DWARF_1 = "cir300cr_og_dwarf1";                 // Some of Oghren's dwarven tormentors
     public const string CIR_CR_OGHREN_DWARF_2 = "cir300cr_og_dwarf2";
     public const string CIR_CR_OGHREN_DWARF_3 = "cir300cr_og_dwarf3";
     public const string CIR_CR_OGHREN_DWARF_4 = "cir300cr_og_dwarf4";
     public const string CIR_CR_REV_MOTHER = "cir300cr_lelianas_mother";           // Leliana's Revered Mother
     public const string CIR_CR_REV_MOTHER_DEMON = "cir300cr_lelianas_mother_demon";     // Leliana's Revered Mother
     public const string CIR_CR_STENS_GIRL = "cir300cr_stens_gf";                  // Sten's girlfriend that he killed
     public const string CIR_CR_STENS_RIVAL = "cir300cr_stens_enemy";               // Sten's rival (who he also killed)
     public const string CIR_CR_ZEV_TORTURE_1 = "cir300cr_zev_torture_1";             // Zevran's torturers
     public const string CIR_CR_ZEV_TORTURE_2 = "cir300cr_zev_torture_2";             // Zevran's torturers
     public const string CIR_CR_WEISSHAUPT_DARKSPAWN = "cir350cr_darkspawn";                 // DArkspawn in Weisshaupt fade
     public const string CIR_CR_TEMPLAR_DREAMER = "cir310cr_templar_dreamer";           // NPC that give Burning Man Form
     public const string CIR_CR_MAGE_APPRENTICE = "cir300cr_mage_apprentice";           // Mage Apprentice who gives Mouse form
     public const string CIR_CR_SPIRIT_TEMPLAR = "cir320cr_templar_spirit";            // Spirit Templar who gives Spirit form
     public const string CIR_CR_CURSED_MAGE = "cir330cr_cursed_mage";               // Cursed Mage who gives Golem form
     public const string CIR_CR_COMPLAINING_MAGE = "cir330cr_complaining_mage_1";        // Mage who complains about noise
     public const string CIR_CR_COMPLAINING_MAGE_2 = "cir330cr_complaining_mage_2";        // Friend of mage who complains about noise
     public const string CIR_CR_KO_TEMPLAR = "cir340cr_unconscious_templa";        // Unconscious templar who gives Arcane Horror form
     public const string CIR_CR_DEMON_CHILD = "cir300cr_demon_child";               // Child that turns into a demon
     public const string CIR_CR_DEMON_CHILD_2 = "cir300cr_demon_child_2";             // Another child that turns into a demon

     //LIGHT CONTENT CREATURES
     //summoning sciences
     public const string CIR_CR_LT_SUMM_NUG = "cir200cr_lt_summ_nug";               //Summoning NUG - Exercise 1
     public const string CIR_CR_LT_WHIM = "cir200cr_lt_summ_whim";              //Summoning Trickster Whim - Exercise 2
     public const string CIR_CR_LT_RIFTER = "cir200cr_lt_summ_rifter";            //Summoning Fade Rifter = exercise 3
     public const string CIR_CR_LT_RIFTER2 = "cir200cr_lt_summ_rifter2";            //Summoning Fade Rifter = exercise 3
     public const string CIR_CR_LT_ARL = "cir200cr_lt_foreshadow";          //arl foreshadow - exercise 4
                                                                            //watchguard of the reaching
     public const string CIR_CR_LT_FIEND = "cir200cr_lt_rea_demon";
     //promises of pride
     public const string CIR_CR_LT_ABOM_1 = "cir200cr_lt_abom_1";
     public const string CIR_CR_LT_ABOM_2 = "cir200cr_lt_abom_2";
     public const string CIR_CR_LT_ABOM_3 = "cir200cr_lt_abom_3";
     public const string CIR_CR_LT_ABOM_4 = "cir220cr_abom_creation";
     public const string CIR_CR_LT_ABOM_5 = "cir220cr_abom_primal";
     public const string CIR_CR_LT_ABOM_6 = "cir210cr_lt_abom_6";
     //Multi Vials
     public const string CIR_CR_LT_REVENANT1 = "cir210cr_lt_revenant";

     //------------------------------------------------------------------------------
     // WAYPOINTS
     //------------------------------------------------------------------------------
     public const string ZZ_CIR_WP_RUMOUR_DEBUG = "zz_cir100wp_rumour_debug";
     // CNM: for testing the rumour man at Lake Calenhad
     public const string CIR_WP_CULLEN_TO_HARROWING = "cir240wp_cullen_to_harrowing";
     // Cullen goes to the Harrowing Chamber
     public const string CIR_WP_FADE_WEISSHAUPT = "cir350wp_fade_weiss_entrance";       // The PC goes here first in the Fade - in the Weisshaupt

     public const string CIR_WP_FADE_TEMPLARS_NIGHTMARE = "cir340wp_temp_night_entrance";       // Fade entrance to Templar's Nightmare

     public const string CIR_WP_FADE_MAGE_ASUNDER = "cir330wp_mage_asunder_entrance";     // Fade entrance to Mage Asunder
     public const string CIR_WP_FADE_MAGE_ASUNDER_DREAMER = "cir330wp_mage_asunder_dreamer";

     public const string CIR_WP_FADE_DARKSPAWN_INVASION = "cir320wp_dark_inv_entrance";         // Fade entrance to Darkspawn Invasion
     public const string CIR_WP_FADE_DARKSPAWN_INVASION_DREAMER = "cir320wp_dark_inv_dreamer";

     public const string CIR_WP_FADE_BURNING_TOWER = "cir310wp_burning_tower_entrance";    // Fade entrance to Burning Tower
     public const string CIR_WP_FADE_BURNING_TOWER_DREAMER = "cir310wp_burning_tower_dreamer";     // Fade entrance to Burning Tower Dreamer

     public const string CIR_WP_FADE_RAW_FADE = "cir300wp_main_fade_entrance";
     // Fade entrance to Burning Tower
     public const string CIR_WP_FADE_GREEN_1 = "cir300wp_green_1_entrance";          // First henchman location in Fade
     public const string CIR_WP_FADE_GREEN_2 = "cir300wp_green_2_entrance";          // First henchman location in Fade
     public const string CIR_WP_FADE_GREEN_3 = "cir300wp_green_3_entrance";          // First henchman location in Fade
     public const string CIR_WP_NIALL_IN_FADE = "cir300wp_niall_spot";                // This is where Niall is in the Fade, PC port point
     public const string CIR_WP_NIALL_TO_DEMON = "cir300wp_niall_to_demon";            // Niall teleports to the Demon to speak to the PC
     public const string CIR_WP_PC_END_POINT = "cir100wp_pc_ending";                 // PC goes to the ending with Greagoir and Pals
     public const string CIR_WP_PC_RETURNS_FROM_FADE = "cir230wp_pc_returns_from_fade";
     // The PC comes back from the Fade
     public const string CIR_WP_FADE_SLOTH_DEMON = "cir300wp_pc_to_demon";               // The PC goes to the Sloth Demon
     public const string CIR_WP_TOWER_START = "cir100wp_start";                     // Carroll takes the player here (1st floor)
     public const string CIR_WP_WYNNE_TO_CORNER = "mp_gen00fl_wynne_0";
     // Wynne goes to her corner, she isn't accepted as a companion
     public const string CIR_WP_KEILI = "cir200wp_keili";
     public const string CIR_WP_KIDS = "cir200wp_kids";
     public const string CIR_WP_KINNON = "cir200wp_kinnon";
     public const string CIR_WP_PETRA = "cir200wp_petra";
     public const string CIR_WP_DOOR_CLOSES = "cir100wp_greagoir_closes_door";

     public const string CIR_WP_KIDS_ESCAPE = "cir200wp_hide_kids";
     public const string CIR_WP_CULLEN_POST_PLOT = "cir200wp_cullen_post_plot";           // The place that cullen that appears post plot

     // These waypoints are for the Fade and Followers
     public const string CIR_WP_FADE_ALISTAIR = "cir300wp_alistair";
     public const string CIR_WP_FADE_DOG = "cir300wp_dog";
     public const string CIR_WP_FADE_LELIANA = "cir300wp_leliana";
     public const string CIR_WP_FADE_LOGHAIN = "cir300wp_loghain";
     public const string CIR_WP_FADE_MORRIGAN = "cir300wp_morrigan";
     public const string CIR_WP_FADE_OGHREN = "cir300wp_oghren";
     public const string CIR_WP_FADE_STEN = "cir300wp_sten";
     public const string CIR_WP_FADE_SHALE = "cir300wp_shale";
     public const string CIR_WP_FADE_WYNNE = "cir300wp_wynne";
     public const string CIR_WP_FADE_ZEVRAN = "cir300wp_zevran";

     public const string CIR_WP_FADE_FOL_ALISTAIR = "cir300wp_fol_alistair";
     public const string CIR_WP_FADE_FOL_DOG = "cir300wp_fol_dog";
     public const string CIR_WP_FADE_FOL_LELIANA = "cir300wp_fol_leliana";
     public const string CIR_WP_FADE_FOL_LOGHAIN = "cir300wp_fol_loghain";
     public const string CIR_WP_FADE_FOL_MORRIGAN = "cir300wp_fol_morrigan";
     public const string CIR_WP_FADE_FOL_OGHREN = "cir300wp_fol_oghren";
     public const string CIR_WP_FADE_FOL_STEN = "cir300wp_fol_sten";
     public const string CIR_WP_FADE_FOL_SHALE = "cir300wp_fol_shale";
     public const string CIR_WP_FADE_FOL_WYNNE = "cir300wp_fol_wynne";
     public const string CIR_WP_FADE_FOL_ZEVRAN = "cir300wp_fol_zevran";

     // Templar's Nightmare Stuff
     public const string CIR_WP_FADE_SUCCUBUS_BOSS_1 = "mp_cir340cr_succubus_boss_1";
     public const string CIR_WP_FADE_SUCCUBUS_BOSS_JUMP_1 = "jp_cir340cr_succubus_boss_1";

     // MAP NOTES
     public const string CIR_MN_WEISSHAUPT_EXIT = "cir350mn_exit";
     public const string CIR_MN_FADE_EXIT = "cir300mn_hidden_exit";

     public const string CIR_WP_SLOTH_NPC = "cir360wp_sloth_npc_spawn";

     // Outside
     public const string CIR_KESTER_POST_PLOT = "jp_cir100cr_kester_0";       // Where Kester should live post plot

     //------------------------------------------------------------------------------
     // PLACEABLES
     //------------------------------------------------------------------------------
     public const string CIR_IP_FADE_PEDISTAL = "cir000ip_fade_pedistal";         // Fade Portal
     public const string CIR_IP_FADE_FIRE_BASE = "cir000ip_fade_fire_base";
     public const string CIR_IP_FADE_SPIRIT_BASE = "cir000ip_fade_spirit_fx";
     public const string CIR_IP_FADE_GOLEM_DOOR = "cir000ip_fade_golem_door";       // Fade Golem Doors
     public const string CIR_IP_WYNNE_BARRIER = "cir200ip_wynne_barrier";         // Wynne's Barrier
     public const string CIR_IP_CLOSET_GODWIN = "cir210cr_godwin_closet";         // Closet placeable that has Godwin hidden inside
     public const string CIR_IP_RAW_FADE_OPENER = "cir300wp_portal_4_b";
     public const string CIR_IP_ASUNDER_AMBUSH_DOOR = "cir330ip_ambush_door";
     public const string CIR_IP_GREAGOIR_DOOR = "cir100ip_greagoir_door";
     public const string CIR_IP_SHAMBLING_DOOR = "cir200ip_shambling_door";
     public const string CIR_IP_IRVING_BARRIER = "cir210ip_irving_barrier";
     public const string CIR_IP_QUEST_UPDATE_DOOR = "cir330ip_fade_door_quest";
     public const string CIR_IP_SANCTUM_DOOR = "cir330ip_arcane_hall_door";
     public const string CIR_IP_FADE_SECOND_HUB_BLOCKER = "cir000ip_fdoor_second_blocker";
     public const string CIR_IP_FADE_SPIRIT_SECOND = "cir340wp_jump_4";
     public const string CIR_IP_PRISON = "cir230ip_prison";
     public const string CIR_IP_SLOTH_MOUSEHOLE = "cir360wp_sloth_mousehole";
     public const string CIR_IP_FORCE_FIELD_DOOR = "cip000ip_fade_force_field";
     public const string CIR_IP_CULLEN_PRISON = "cir230ip_prison_vfx";            // Cullen's magical prison
     public const string CIR_IP_GODWIN_CLOSET = "cir200ip_godwin_closet";         // The closet Godwin is in.
     public const string CIR_IP_BLOOD_MAGE_BARRIER = "cir210ip_blood_mage_barrier";    // The magic barrier the blood mages have errected.
     public const string CIR_IP_FOURTH_FLOOR_TRANS = "cir240ip_to_level_4";

     //LIGHT CONTENT
     //summoning sciences
     public const string CIR_IP_SUMMONING_FONT = "cir100ip_summoning_font";
     public const string CIR_IP_SUMM_FLAME1 = "cir100ip_lt_summ_flame"; //light content summoning flame
     public const string CIR_IP_SUMM_FLAME2 = "cir100ip_lt_summ_flame2"; //light content summoning flame
     public const string CIR_IP_SUMM_FLAME3 = "cir100ip_lt_summ_flame3"; //light content summoning flame
     public const string CIR_IP_SUMM_FLAME4 = "cir100ip_lt_summ_flame4"; //light content summoning flame
     public const string CIR_IP_SUMM_TOME1 = "cir100ip_lt_tome1"; //Exercise 1 - Tome of Spirit Personages
     public const string CIR_IP_SUMM_RECITE = "cir100ip_lt_recite"; //Exercise 2 - Recited passage
     public const string CIR_IP_SUMM_STATUE = "cir100ip_lt_statue"; //Exercise 2 -Statue for exercise 2
     public const string CIR_IP_SUMM_TABLE = "cir100ip_lt_table"; //exercise 3 - table
     public const string CIR_IP_SUMM_BESTIARY = "cir100ip_lt_bestiary"; //exercise 3 - bestiary
     public const string CIR_IP_SUMM_SPIRITBOOK = "cir100ip_lt_spiritbook"; //exercise 3 - spiritbook
     public const string CIR_IP_SUMM_PHYLACTORY = "cir100ip_lt_phylactory"; //exercise 3 - phylactory
                                                                            //watchguard of the reaching
     public const string CIR_IP_REA_STATUE1 = "cir220ip_lt_statue1";
     public const string CIR_IP_REA_STATUE2 = "cir220ip_lt_statue2";
     public const string CIR_IP_REA_STATUE3 = "cir220ip_lt_statue3";
     public const string CIR_IP_REA_STATUE4 = "cir220ip_lt_statue4";
     //the spot
     public const string CIR_IP_SPOT_BED = "cir100ip_lt_denribed";
     //Maelefactor Regrets
     public const string CIR_IP_REGRET_CACHE = "cir210ip_lt_belcache";
     //black vials
     public const string CIR_IP_VIALS_PHYLACTERY = "cir210ip_topplestat";

     //------------------------------------------------------------------------------
     // TRIGGERS
     //------------------------------------------------------------------------------
     public const string CIR_TR_FADE_HUB1 = "cir300tr_hub1_talk";           // Hub 1 of the Fade's trigger
     public const string CIR_TR_FADE_HUB2 = "cir300tr_hub2_talk";           // Hub 1 of the Fade's trigger
     public const string CIR_TR_FADE_HUB3 = "cir300tr_hub3_talk";           // Hub 1 of the Fade's trigger

     public const string CIR_TR_WYNNE_BARRIER = "cir200tr_wynne_barrier";

     public const string CIR_TR_WOUNDED_AMBIENT_1 = "cir200tr_wounded_bark_1";
     public const string CIR_TR_WOUNDED_AMBIENT_2 = "cir200tr_wounded_bark_2";

     //------------------------------------------------------------------------------
     // AREAS
     //------------------------------------------------------------------------------
     public const string CIR_AR_LAKE_CALENHAD = "cir100ar_docks";
     public const string CIR_AR_TOWER_FIRST_FLOOR = "cir200ar_tower_level_1";
     public const string CIR_AR_TOWER_SECOND_FLOOR = "cir210ar_tower_level_2";
     public const string CIR_AR_TOWER_THIRD_FLOOR = "cir220ar_tower_level_3";
     public const string CIR_AR_TOWER_FOURTH_FLOOR = "cir230ar_tower_level_4";
     public const string CIR_AR_TOWER_HARROWING_CHAMBER = "cir240ar_tower_harrowing";
     public const string CIR_AR_FADE = "cir300ar_fade";
     public const string CIR_AR_FADE_BURNING_TOWER = "cir310ar_fade_burning_tower";
     public const string CIR_AR_FADE_DARKSPAWN_INVASION = "cir320ar_fade_darkspawn_inv";
     public const string CIR_AR_FADE_MAGE_ASUNDER = "cir330ar_fade_mage_asunder";
     public const string CIR_AR_FADE_TEMPLAR_NIGHTMARE = "cir340ar_fade_templar_night";
     public const string CIR_AR_FADE_WEISSHAUPT = "cir350ar_fade_weisshaupt";
     public const string CIR_AR_FADE_SLOTH = "cir360ar_sloth_demon";
     public const string CIR_AR_INN = "cir110ar_inn";

     // Companion Areas
     public const string CIR_AR_ALISTAIR = "cir361ar_alistair";
     public const string CIR_AR_DOG = "cir362ar_dog";
     public const string CIR_AR_LELIANA = "cir363ar_leliana";
     public const string CIR_AR_LOGHAIN = "cir364ar_loghain";
     public const string CIR_AR_MORRIGAN = "cir365ar_morrigan";
     public const string CIR_AR_OGHREN = "cir366ar_oghren";
     public const string CIR_AR_SHALE = "cir367ar_shale";
     public const string CIR_AR_STEN = "cir368ar_sten";
     public const string CIR_AR_WYNNE = "cir369ar_wynne";
     public const string CIR_AR_ZEVRAN = "cir370ar_zevran";
     public const string CIR_AR_SLOTH_DEMON = "cir360ar_sloth_demon";

     //------------------------------------------------------------------------------
     // CONVERSATIONS
     //------------------------------------------------------------------------------
     public const string CIR_CONVERSATION_BLOOD_MAGES = "cir210_blood_mage01.dlg";
     public const string CIR_CONVERSATION_ULDRED_BARK = "cir240_uldred_combat.dlg";
     public const string CIR_CONVERSATION_COMPLAINING_MAGE = "cir330_complaining_mages.dlg";

     //------------------------------------------------------------------------------
     // GROUPS
     //------------------------------------------------------------------------------

     // Actual groups!
     public const int GROUP_FADE_SPIRIT_PORTALS = 30;
     public const int GROUP_FADE_MOUSE_HOLES = 31;
     public const int GROUP_FADE_GOLEM_DOORS = 32;
     public const int GROUP_FADE_MOUSE_IGNORERS = 33;

     //Complete lies
     public const int GROUP_FADE_DESIRE_DEMON = 34;       // Raw Fade
     public const int GROUP_FADE_SPIRIT_OBJECTS = 35;
     public const int GROUP_FADE_RAGE_DEMON = 36;       // Burning Tower
     public const int GROUP_FADE_DARK_EMISSARY = 37;       // Darkspawn Invasion
     public const int GROUP_FADE_HUNGER_DEMON = 38;       // Mage Asunder
     public const int GROUP_FADE_PRIDE_DEMON = 39;       // Templar's Nightmare
     public const int GROUP_FADE_ARCANE_HORRORS = 40;
     public const int GROUP_FADE_CRAZY_MAGES = 41;       // Speakers
     public const int GROUP_FADE_STEN_DEMONS = 42;       // Enemies for Sten's Nightmare
     public const int GROUP_FADE_WYNNE_DEMONS = 43;       // Enemies for Wynne's Nightmare
     public const int GROUP_FADE_ZEVRAN_DEMONS = 44;       // Enemies for Zevran's Nightmare

     //Actual groups
     public const int GROUP_FADE_CRAZY_MAGE_1 = 38;
     public const int GROUP_FADE_CRAZY_MAGE_2 = 39;
     public const int GROUP_FADE_CRAZY_MAGE_3 = 40;
     public const int GROUP_FADE_CRAZY_MAGE_4 = 41;

     public const int GROUP_FADE_INSANE_MAGE_1 = 42;
     public const int GROUP_FADE_INSANE_MAGE_2 = 43;
     public const int GROUP_FADE_INSANE_MAGE_3 = 44;

     public const int GROUP_CIR_BLOOD_MAGE_1 = 49;
     public const int GROUP_CIR_BLOOD_MAGE_2 = 50;

     //------------------------------------------------------------------------------
     // AREA LISTS
     //------------------------------------------------------------------------------
     public const string CIR_AL_LAKE = "cir01al_lake_calenhad";
     public const string CIR_AL_CIRCLE_TOWER = "cir02al_circle_tower";
     public const string CIR_AL_FADE = "cir03al_fade";

     //------------------------------------------------------------------------------
     // SCRIPTS
     //------------------------------------------------------------------------------

     public const string CIR_RESOURCE_SCRIPT_AR_CORE = "cir000ar_core.ncs";
     public const string CIR_RESOURCE_SCRIPT_AR_FADE_CORE = "cir000ar_fade_core.ncs";
     public const string CIR_RESOURCE_SCRIPT_CR_TEAM_CORE = "cir000cr_team_core.ncs";
     public const string CIR_RESOURCE_SCRIPT_IP_FADE_JUMP = "cir000ip_fade_jump.ncs";
     public const string CIR_RESOURCE_SCRIPT_IP_FADE_DOOR = "cir000ip_fade_door.ncs";        // The basic fade door script

     //------------------------------------------------------------------------------
     // Ints
     //------------------------------------------------------------------------------
     public const int CIR_FOLLOWER_ALISTAIR = 1;
     public const int CIR_FOLLOWER_DOG = 2;
     public const int CIR_FOLLOWER_LELIANA = 3;
     public const int CIR_FOLLOWER_LOGHAIN = 4;
     public const int CIR_FOLLOWER_MORRIGAN = 5;
     public const int CIR_FOLLOWER_OGHREN = 6;
     public const int CIR_FOLLOWER_SHALE = 7;
     public const int CIR_FOLLOWER_STEN = 8;
     public const int CIR_FOLLOWER_WYNNE = 9;
     public const int CIR_FOLLOWER_ZEVRAN = 10;

     //------------------------------------------------------------------------------
     // Items
     //------------------------------------------------------------------------------

     public const string CIR_IM_LITANY = "cir000im_litany.uti";
     public const string CIR_IM_ADRALLA = "cir000im_litany";
     public const string CIR_IM_CENSURENOTE = "cir200im_censurenote";
     public const string CIR_IM_ABOM_PACKAGE_1 = "cir100ip_lt_abom_1.utp";
     public const string CIR_IM_ABOM_PACKAGE_2 = "cir200ip_lt_abom_2.utp";
     public const string CIR_IM_ABOM_PACKAGE_3 = "cir200ip_lt_abom_3.utp";
     public const string CIR_IM_ABOM_PACKAGE_4 = "cir220ip_lt_abom_4.utp";
     public const string CIR_IM_ABOM_PACKAGE_5 = "cir220ip_lt_abom_5.utp";
     public const string CIR_IM_ABOM_PACKAGE_6 = "cir230ip_lt_abom_6.utp";
     public const string CIR_IM_ABOM_NOTE_1 = "cir200im_abom_note1";
     public const string CIR_IM_ABOM_NOTE_2 = "cir200im_abom_note2";
     public const string CIR_IM_ABOM_NOTE_3 = "cir200im_abom_note3";
     public const string CIR_IM_ABOM_NOTE_4 = "cir200im_abom_note4";
     public const string CIR_IM_ABOM_NOTE_5 = "cir200im_abom_note5";
     public const string CIR_IM_ABOM_NOTE_6 = "cir200im_abom_note6";
     public const string CIR_IM_REV_NOTE = "cir210im_rev_note";
     public const string CIR_IM_SUMM_FOREBOOK = "cir200im_fore_book";
     public const string CIR_IM_JENNY_BOX = "cir210im_lt_paintedbox";
     public const string CIR_IM_REACH_PACKAGE = "cir100ip_lt_reach.utp";

     //------------------------------------------------------------------------------
     // Dialogs
     //------------------------------------------------------------------------------
     public const string CIR_DG_TEMPLAR_SPIRIT = "cir320_templar_spirit.dlg";
     public const string CIR_DG_CURSED_MAGE = "cir330_cursed_mage.dlg";
     public const string CIR_DG_SLOTH_SHAPESHIFT = "cir300_sloth_shapeshift.dlg";

     //------------------------------------------------------------------------------
     // Teams
     //------------------------------------------------------------------------------
     public const int CIR_TEAM_3RD_DESIRE_DEMON = 1;
     public const int CIR_TEAM_DUNCAN_WARDENS = 2;
     public const int CIR_TEAM_MOUSE_ATTACKERS = 3;
     public const int CIR_TEAM_DARKSPAWN_SPIRITS = 4;
     public const int CIR_TEAM_TEMPLAR_DREAMER = 5;
     public const int CIR_TEAM_CHANTRY_PRIESTS = 6;
     public const int CIR_TEAM_WYNNE_HOSTILE = 7;
     public const int CIR_TEAM_MA_KEY_KEEPER_1 = 8;
     public const int CIR_TEAM_MA_KEY_KEEPER_2 = 9;
     public const int CIR_TEAM_MA_KEY_KEEPER_3 = 10;
     public const int CIR_TEAM_MA_KEY_KEEPER_4 = 11;
     public const int CIR_TEAM_SHAMBLING = 12;
     public const int CIR_TEAM_ENRAGED_1 = 13;
     public const int CIR_TEAM_ENRAGED_2 = 14;
     public const int CIR_TEAM_ENRAGED_3 = 15;
     public const int CIR_TEAM_CHARMED_TEMPLAR = 16;
     public const int CIR_TEAM_FOURTH_ABOMINATION = 17;
     public const int CIR_TEAM_ASH_SHADES = 18;
     public const int CIR_TEAM_ULDRED = 19;
     public const int CIR_TEAM_SACRIFICIAL_TRANQUIL = 20;
     public const int CIR_TEAM_ULDRED_MAGES = 21;
     public const int CIR_TEAM_BLOOD_MAGES_L2 = 22;
     public const int CIR_TEAM_OGHREN_NIGHTMARE = 23;
     public const int CIR_TEAM_WYNNE_NIGHTMARE = 24;
     public const int CIR_TEAM_FADE_ARCANE_HORROR_OBJECTS = 25;       // Show or hide the arcane horror form team
     public const int CIR_TEAM_CRAZY_MAGES = 26;       // Crazy mages
     public const int CIR_TEAM_INSANE = 27;       // Crazy mages
     public const int CIR_TEAM_MAGE_INSTRUCTION = 28;       // Crazy mages
     public const int CIR_TEAM_MAGE_INSTRUCTION_2 = 29;       // Crazy mages
                                                              //GROUPS ARE AT 30!
                                                              //Starting at a 100
     public const int CIR_TEAM_GOLDANNA_DEMONS = 100;
     public const int CIR_TEAM_WOUNDED_PEOPLE = 101;
     public const int CIR_TEAM_MAGE_KIDS = 102;

     public const int CIR_TEAM_REWARD_CONSTITUTION = 103;
     public const int CIR_TEAM_REWARD_INTELLIGENCE = 104;
     public const int CIR_TEAM_REWARD_MAGIC = 105;
     public const int CIR_TEAM_REWARD_STRENGTH = 106;
     public const int CIR_TEAM_REWARD_DEXTERITY = 107;
     public const int CIR_TEAM_REWARD_WILLPOWER = 108;

     public const int CIR_TEAM_SLOTH_OGRE = 109;
     public const int CIR_TEAM_SLOTH_RAGE = 110;
     public const int CIR_TEAM_SLOTH_ABOMINATION = 111;
     public const int CIR_TEAM_SLOTH_SHADE = 112;
     public const int CIR_TEAM_SLOTH_FINAL = 113;
     //This number is intentionally missed, for now
     public const int CIR_TEAM_RAW_FADE_DEMON = 115;      // Demon in the raw fade.
     public const int CIR_TEAM_RAGE_DEMONS_AL = 116;      // Rage demons that pair with Goldanna Demons.
     public const int CIR_TEAM_CHILD_DEMONS_AL = 117;      // Rage demons before they are rage demons.
     public const int CIR_TEAM_GREAGOIR = 118;      // Greagoir gets his own team because he is special (that and we want to know when he dies)
     public const int CIR_TEAM_COMPLAINING_MAGES = 119;
     public const int CIR_TEAM_POST_TEMPLAR = 120;      // Post templar conversation
     public const int CIR_TEAM_GHOST_OBJECTS = 121;      // These are objects that need a ghost xEffect in the fade companion areas.
     public const int CIR_TEAM_OGHREN_DWARVES = 122;
     public const int CIR_TEAM_LEVEL_3_DEMONS = 123;      // The demons on level 3 that attack at the same time.
     public const int CIR_TEAM_DEAD_MAGES_AND_TEMPLAR = 124;      // The dead mages and templar.
     public const int CIR_TEAM_3RD_DESIRE_DEMON_CORPSES = 125;
     public const int CIR_TEAM_HOSTILES_DEACTIVATE = 126;
     public const int CIR_TEAM_ULDRED_SACRIFICE = 127;
     public const int CIR_TEAM_ULDRED_BARRIER = 128;
     public const int CIR_TEAM_RANDOM_TEMPLARS = 129;

     //LIGHT CONTENT TEAMS
     public const int LIT_TEAM_FITE_DESERTERS_1 = 130;

     //Back to main dude (sometimes I think I have too many teams)
     public const int CIR_TEAM_OTHER_TEMPLARS = 131;
     public const int CIR_TEAM_MAGE_ASUNDER_DEMONS_1 = 132;
     public const int CIR_TEAM_MAGE_ASUNDER_DEMONS_2 = 133;
     public const int CIR_BLOOD_MAGE_BARRIER = 134; //The barrier the blood mage has set up

     //cir330ar_constants
     public const int INSANE_GROUP_BASE_INT = GROUP_FADE_INSANE_MAGE_1;     // The group's base to use
     public const int NUMBER_OF_INSANE_GROUPS = 3;                            // Number of groups involved
     public const float SWITCH_DELAY = 12.0f;                         // The delay before the xEvent is fired again
     public const int SWITCH_DELAY_RANDOM = 8;                            // A random factor added on to the delay (to stop it looking mechanic)

     //------------------------------------------------------------------------------
     //      Appearance
     //------------------------------------------------------------------------------

     public const int CIR_APR_ABOMINATION = 25;
     public const int CIR_APR_ARCANE_HORROR = 12;

     //------------------------------------------------------------------------------
     //        VFX
     //------------------------------------------------------------------------------
     public const int FADE_VFX_TELEPORT = 3027;
     public const int CIR_REWARD_CRUST_VFX = 1117;
     public const int CIR_REWARD_CRUST_VFX_2 = 1098;
     public const int SHAPESHIFT_TRANSFORM_EFFECT = 1134;
     public const int SHAPESHIFT_TRANSFORM_EFFECT_OUT = 3007;     // In case we want to make the shapeshift out xEffect different from the shapeshift in
     public const int CIR_ULDRED_MIND_BREAK = 1528;     // Using the litany on a mage will play this xEffect on them
     public const int CIR_ULDRED_CANCEL = 1024;     // The xEffect to play on Uldred when the power is cancelled
     public const int CIR_PC_MIND_BREAK = 1012;     // The xEffect played on the PC when they break the mind effect.
     public const int CIR_PC_LITANY_FAIL = 1009;     // The xEffect played on the PC uses the litany at the wrong time.
     public const int CIR_ULDRED_DRAIN_EFFECT = 1537;     // The xEffect played while uldric is draining people
     public const int CIR_ULDRED_MAGE_DRAIN_EFFECT = 1053;     // The xEffect played while uldric is draining people
     public const int CIR_ULDRED_SHIELD_EFFECT = 1077;
     public const int VFX_DISPEL_AOE = 90047;

     //------------------------------------------------------------------------------
     //        Audio
     //------------------------------------------------------------------------------

     public const int CIR_AUDIO_TOGGLE_ULDRED_DIES_TOWER_LEVEL_1 = 8;
     public const int CIR_AUDIO_TOGGLE_ULDRED_DIES_TOWER_LEVEL_2 = 9;
     public const int CIR_AUDIO_TOGGLE_ULDRED_DIES_TOWER_LEVEL_3 = 10;
     public const int CIR_AUDIO_TOGGLE_ULDRED_DIES_TOWER_LEVEL_4 = 11;
     public const int CIR_AUDIO_TOGGLE_ULDRED_DIES_TOWER = 12;

     //Music

     public const string CIR_MUSIC_ULDRED_DIES = "end_plot";
     public const int CIR_MUSIC_ULDRED_DIES_STATE = 4;
     //------------------------------------------------------------------------------
     //        Shops
     //------------------------------------------------------------------------------
     public const string CIR_RUMOUR_DEFAULT = "store_cir100cr_rumour_merchant";
     public const string CIR_RUMOUR_MAGES = "store_cir110sr_rumour_mage";
     public const string CIR_RUMOUR_TEMPLAR = "store_cir110sr_rumour_templar";
}