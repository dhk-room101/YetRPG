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
     //:: Constants include
     //:: Copyright (c) 2003 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         Constants include for Nature of the Beast
     */
     //:://////////////////////////////////////////////
     //:: Created By: Cori
     //:: Created On: Jan 30/07
     //:://////////////////////////////////////////////

     //ITEMS
     public const string GEN_IM_ACC_RNG_R11 = "gen_im_acc_rng_r11";
     public const string GEN_IM_ARM_HEL_MED_ELV = "gen_im_arm_hel_med_elv";
     public const string GEN_IM_PELT_WEREWOLF = "gen_im_pelt_werewolf";
     public const string GEN_IM_PELT_WOLF = "gen_im_pelt_wolf";
     public const string NTB_IM_IRONBARK = "ntb200im_ironbark";
     public const string NTB_IM_DEYGAN_FIGURINE = "ntb200im_deygan_figurine";
     public const string NTB_IM_HALLA_ANTLERS = "ntb100im_halla_antlers";
     public const string NTB_IM_GRAND_OAK_HEART = "ntb210im_grand_oak_heart";
     public const string NTB_IM_HERMIT_BOOK = "ntb210im_hermit_book";
     public const string GEN_IM_ARM_BOT_LGT_DEY = "gen_im_arm_bot_lgt_dey";
     public const string NTB_IM_CAMMEN_BOOK = "ntb100im_cammen_book";
     public const string NTB_IM_LANAYA_SONGBOOK = "ntb100im_lanaya_songbook";
     public const string GEN_IM_ACC_AMU_HAL = "gen_im_acc_amu_hal";
     public const string NTB_IM_IRONBARK_BRACER = "ntb100im_ironbark_bracer";
     public const string GEN_IM_ACC_AMU_ATH = "gen_im_acc_amu_ath";
     public const string NTB_IM_WITHERFANG_HEART = "ntb340im_witherfang_heart";
     public const string NTB_IM_HERMIT_PELT = "ntb210im_hermit_pelt";
     public const string NTB_IM_DANYLA_SCARF = "ntb220im_danyla_scarf";
     public const string NTB_IM_EARTHEN_JUG_EMPTY = "ntb330im_earthen_jug_empty";
     public const string NTB_IM_EARTHEN_JUG_FULL = "ntb330im_earthen_jug_full";
     public const string NTB_IM_PUZZLE_TABLET = "ntb330im_puzzle_tablet";
     public const string NTB_IM_JUGGERNAUT_HELM = "gen_im_arm_hel_mas_jug";
     public const string NTB_IM_JUGGERNAUT_BOOTS = "gen_im_arm_bot_mas_jug";
     public const string NTB_IM_JUGGERNAUT_CHEST = "gen_im_arm_cht_mas_jug";
     public const string NTB_IM_JUGGERNAUT_GLOVES = "gen_im_arm_glv_mas_jug";
     public const string NTB_IM_FALONDINS_REACH = "gen_im_wep_rng_lbw_fal";
     public const string NTB_IM_MYTHALSBLESSING = "gen_im_arm_shd_sml_mth";

     public const string NTB_IM_VIALS_REVNOTE4 = "ntb330im_rev_note";
     public const string NTB_IM_VIALS_REVNOTE5 = "ntb340im_rev_note";

     //public const string LITE_IM_UNBOUND_JOURNAL = "liteim_unbound_journal";

     //item strings
     public const string rGEN_IM_PELT_WEREWOLF = "gen_im_pelt_werewolf.uti";
     public const string rGEN_IM_PELT_WOLF = "gen_im_pelt_wolf.uti";
     public const string rNTB_IM_WITHERFANG_HEART = "ntb340im_witherfang_heart.uti";
     public const string rNTB_IM_HALLA_ANTLERS = "ntb100im_halla_antlers.uti";
     public const string rNTB_IM_DEYGAN_FIGURINE = "ntb200im_deygan_figurine.uti";
     public const string rNTB_IM_GRAND_OAK_HEART = "ntb210im_grand_oak_heart.uti";
     public const string rNTB_IM_HERMIT_PELT = "ntb210im_hermit_pelt.uti";
     public const string rNTB_IM_DANYLA_SCARF = "ntb220im_danyla_scarf.uti";
     public const string rNTB_IM_EARTHEN_JUG_EMPTY = "ntb330im_earthen_jug_empty.uti";
     public const string rNTB_IM_EARTHEN_JUG_FULL = "ntb330im_earthen_jug_full.uti";
     public const string rNTB_IM_PUZZLE_TABLET = "ntb330im_puzzle_tablet.uti";

     //CREATURES
     public const string NTB_CR_ANEIRIN = "ntb220cr_aneirin";
     public const string NTB_CR_APPRENTICE = "ntb100cr_apprentice";
     public const string NTB_CR_ARCANE_HORROR = "ntb330cr_arcane_horror";
     public const string NTB_CR_ATHRAS = "ntb100cr_athras";
     public const string NTB_CR_ATHRAS_WEREWOLF = "ntb220cr_athras_werewolf";
     public const string NTB_CR_CAMMEN = "ntb100cr_cammen";
     public const string NTB_CR_CAMP_SHADE = "ntb210cr_camp_shade";
     public const string NTB_CR_DANYLA = "ntb220cr_danyla";
     public const string NTB_CR_DEYGAN = "ntb200cr_deygan";
     public const string NTB_CR_DEYGAN_DEAD = "ntb200cr_deygan_dead";
     public const string NTB_CR_ELF_CHILD = "ntb100cr_elf_child";
     public const string NTB_CR_ELF_CHILD_02 = "ntb100cr_elf_child_02";
     public const string NTB_CR_ELF_FEMALE = "ntb100cr_elf_female";
     public const string NTB_CR_ELF_FEMALE_02 = "ntb100cr_elf_female_02";
     public const string NTB_CR_ELF_FEMALE_03 = "ntb100cr_elf_female_03";
     public const string NTB_CR_ELF_MALE = "ntb100cr_elf_male";
     public const string NTB_CR_ELF_NURSE = "ntb100cr_elf_nurse";
     public const string NTB_CR_ELORA = "ntb100cr_elora";
     public const string NTB_CR_GATEKEEPER = "ntb230cr_gatekeeper";
     public const string NTB_CR_GATEMINION1 = "ntb210cr_gatekeeper_minion";
     public const string NTB_CR_GATEMINION2 = "ntb210cr_gatekeeper_minion2";
     public const string NTB_CR_GATEKEEPGUARD1 = "ntb340cr_werewolf_guard1";
     public const string NTB_CR_GATEKEEPGUARD2 = "ntb340cr_werewolf_guard2";
     public const string NTB_CR_GATEKEEPGUARD3 = "ntb340cr_werewolf_guard3";
     public const string NTB_CR_GHEYNA = "ntb100cr_gheyna";
     public const string NTB_CR_GHOSTLY_BOY = "ntb330cr_ghostly_boy";
     public const string NTB_CR_GHOSTLY_ELF = "ntb330cr_ghostly_elf";
     public const string NTB_CR_GIANT_SPIDER_CENTRE = "ntb310cr_giant_spider_centre";
     public const string NTB_CR_GRAND_OAK = "ntb220cr_grand_oak";
     public const string NTB_CR_HERMIT = "ntb210cr_hermit";
     public const string NTB_CR_LADY = "ntb340cr_lady";
     public const string NTB_CR_LANAYA = "ntb100cr_lanaya";
     public const string NTB_CR_MESSENGER = "ntb100cr_messenger";
     public const string NTB_CR_MITHRA = "ntb100cr_mithra";
     public const string NTB_CR_PANOWEN = "ntb200cr_panowen";
     public const string NTB_CR_QUEEN_SPIDER = "ntb320cr_queen_spider";
     public const string NTB_CR_RECOVERING = "ntb100cr_recovering";
     public const string NTB_CR_REFUGEE = "ntb100cr_refugee";
     public const string NTB_CR_RETURNING = "ntb100cr_returning";
     public const string NTB_CR_RUINS_DRAGON = "ntb310cr_ruins_dragon";
     public const string NTB_CR_SAREL = "ntb100cr_sarel";
     public const string NTB_CR_SHADE = "ntb330cr_shade";
     public const string NTB_CR_SR_WEREWOLF = "ntb200cr_sr_werewolf";
     public const string NTB_CR_SR_WEREWOLF_E = "ntb200cr_sr_werewolf_e";
     public const string NTB_CR_SR_WEREWOLF_W = "ntb200cr_sr_werewolf_w";
     public const string NTB_CR_SR_WEREWOLF2 = "ntb200cr_sr_werewolf2";
     public const string NTB_CR_SWIFTRUNNER = "ntb200cr_swiftrunner";
     public const string NTB_CR_SWIFTRUNNER_HUMAN = "ntb340cr_swiftrunner_human";
     public const string NTB_CR_SW_WOLF01 = "ntb200cr_sr_werewolf_e";
     public const string NTB_CR_SW_WOLF02 = "ntb200cr_sr_werewolf2";
     public const string NTB_CR_TRANSFORMING = "ntb100cr_transforming";
     public const string NTB_CR_TOPLEVEL_SCOUT1 = "ntb310cr_werewolf_scout1";
     public const string NTB_CR_TOPLEVEL_SCOUT2 = "ntb310cr_werewolf_scout2";
     public const string NTB_CR_VARATHORN = "ntb100cr_varathorn";
     public const string NTB_CR_WEREWOLF_01 = "ntb100cr_werewolf_01";
     public const string NTB_CR_WEREWOLF_02 = "ntb100cr_werewolf_02";
     public const string NTB_CR_WEREWOLF_03 = "ntb100cr_werewolf_03";
     public const string NTB_CR_WEREWOLF_04 = "ntb100cr_werewolf_04";
     public const string NTB_CR_WEREWOLF_PROTECT_1 = "ntb340cr_werewolf_protect_1";
     public const string NTB_CR_WEREWOLF_PROTECT_2 = "ntb340cr_werewolf_protect_2";
     public const string NTB_CR_WEREWOLF_PROTECT_3 = "ntb340cr_werewolf_protect_3";
     public const string NTB_CR_WEREWOLF_PROTECT_4 = "ntb340cr_werewolf_protect_4";
     public const string NTB_CR_WEREWOLF_PROTECT_5 = "ntb340cr_werewolf_protect_5";
     public const string NTB_CR_WEREWOLF_PROTECT_6 = "ntb340cr_werewolf_protect_6";
     public const string NTB_CR_WEREWOLF_PROTECTOR_05 = "ntb340cr_werewolf_protector_05";
     public const string NTB_CR_WEREWOLF_PROTECTOR_06 = "ntb340cr_werewolf_protector_06";
     public const string NTB_CR_WEREWOLF_PROTECTOR_07 = "ntb340cr_werewolf_protector_07";
     public const string NTB_CR_WEREWOLF_PROTECTOR_08 = "ntb340cr_werewolf_protector_08";
     public const string NTB_CR_WEREWOLF_PROTECTOR_09 = "ntb340cr_werewolf_protector_09";
     public const string NTB_CR_WEREWOLF_PROTECTOR_10 = "ntb340cr_werewolf_protector_10";
     public const string NTB_CR_WEREWOLF_PROTECTOR_11 = "ntb340cr_werewolf_protector_11";
     public const string NTB_CR_WEREWOLF_PROTECTOR_12 = "ntb340cr_werewolf_protector_12";
     public const string NTB_CR_WHITE_WOLF = "ntb230cr_white_wolf";
     public const string NTB_CR_ZATHRIAN = "ntb100cr_zathrian";
     public const string NTB_WEST_FOREST_OGRE_SCOUT = "ntb200_hurlock_scout";
     public const string NTB_WEST_FOREST_HIDDEN_BEAR = "ntb200cr_bear_great";
     public const string NTB_CR_CAMP_SICK_ELF_1 = "ntb100cr_elf_sick_f";
     public const string NTB_CR_CAMP_SICK_ELF_2 = "ntb100cr_elf_sick_f2";
     public const string NTB_CR_CAMP_SICK_ELF_3 = "ntb100cr_elf_sick_f2";
     public const string NTB_CR_CAMP_SICK_ELF_4 = "ntb100cr_elf_sick_m";
     public const string NTB_CR_CAMP_SICK_ELF_5 = "ntb100cr_elf_sick_m2";
     public const string NTB_CR_CAMP_SICK_ELF_6 = "ntb100cr_elf_sick_m3";
     public const string NTB_CR_CAMP_SICK_ELF_7 = "ntb100cr_elf_sick_m4";

     public const string NTB_VIALS_REVENANT1 = "ntb330cr_lt_revenant";
     public const string NTB_VIALS_REVENANT2 = "ntb340cr_lt_revenant";

     //ints
     public const int NTB_INT_CLAN_OUTSIDER = 0;
     public const int NTB_INT_CLAN_DALISH = 3;
     public const int NTB_INT_CLAN_ELF = 1;
     public const int NTB_INT_CLAN_ATTITUDE_HIGH = 2;
     public const int NTB_INT_CLAN_ATTITUDE_LOW = -1;
     public const int NTB_INT_CLAN_ATTITUDE_MED = 0;
     public const int NTB_INT_CAMPFIRE_FINAL_FAILURE = -6;
     public const int NTB_INT_CAMPFIRE_RESISTANCE_HIGH = 5;
     public const int NTB_INT_CAMPFIRE_RESISTANCE_MED = 0;
     public const int NTB_INT_CAMPFIRE_RESISTANCE_LOW = -1;

     //locals
     public const string NTB_CLAN_ATTITUDE_COUNTER = "NTB_CLAN_ATTITUDE_COUNTER";
     public const string NTB_PC_CAMP_RESISTANCE = "NTB_PC_CAMP_RESISTANCE";

     //waypoints
     public const string NTB_WP_VARATHORN_STORE = "ntb100mn_merchant_varathorn";
     public const string NTB_WP_ZATHRIAN_INTERVIEW = "ntb100wp_zathrian_interview";
     public const string NTB_WP_ZATHRIAN_POST = "ntb100wp_zathrian_post";
     public const string NTB_WP_WEREWOLF1_POST = "ap_ntb100cr_werewolf_01_01";
     public const string NTB_WP_WEREWOLF2_POST = "ap_ntb100cr_werewolf_02_01";
     public const string NTB_WP_WEREWOLF3_POST = "ntb100wp_werewolf3_post";
     public const string NTB_WP_WOUNDED_TENTS = "ntb100wp_wounded_tents";
     public const string NTB_WP_MITHRA_POST = "ntb100wp_mithra_post";
     public const string NTB_WP_LANAYA_POST = "ntb100wp_lanaya_post";
     public const string NTB_WP_MITHRA_DEYGAN = "ntb100wp_mithra_deygan";
     public const string NTB_WP_PC_ALTAR = "ntb340wp_pc_altar";
     public const string NTB_WP_LADY_FIGHT = "ntb340wp_lady_fight";
     public const string NTB_WP_FROM_FOREST = "ntb100wp_from_forest";
     public const string NTB_WP_FROM_RUINS = "ntb230wp_from_ruins";
     public const string NTB_WP_ZATHRIAN_FIGHT = "ntb100wp_zathrian_fight";
     public const string NTB_WP_PC_LANAYA = "ntb100wp_pc_lanaya";
     public const string NTB_WP_FROM_LADY = "ntb310wp_from_lady";
     public const string NTB_WP_FROM_TOP_SHORTCUT = "ntb340wp_from_top_shortcut";
     public const string NTB_WP_FROM_CAMP = "ntb200wp_from_camp";
     public const string NTB_WP_FROM_FOREST_END = "ntb310wp_from_forest";
     public const string ZZ_NTB_WP_TOP_LEVEL_DEBUG = "zz_ntb310wp_top_level_debug";
     public const string ZZ_NTB_WP_UNDEAD_DEBUG = "zz_ntb330wp_undead_debug";
     public const string ZZ_NTB_WP_WEREWOLVES_DEBUG = "zz_ntb340wp_werewolves_debug";
     public const string NTB_WP_TOPLEVEL_SCOUT1 = "ntb310wp_werewolf_scout1";
     public const string NTB_WP_TOPLEVEL_SCOUT2 = "ntb310wp_werewolf_scout2";
     public const string NTB_WP_START = "ntb100wp_start";
     public const string NTB_WP_FROM_NORTHWEST_TO_NE = "ntb210wp_from_northwest_to_ne";
     public const string NTB_WP_FROM_NORTHWEST_TO_SW = "ntb220wp_from_northwest_to_sw";
     public const string NTB_WP_FROM_SOUTHWEST_TO_SE = "ntb220wp_from_southwest_to_se";
     public const string NTB_WP_FROM_ENTRANCE = "ntb330wp_from_entrance";
     public const string NTB_WP_FROM_UNDEAD_LAIR = "ntb340wp_from_undead_lair";
     public const string ZZ_NTB_WP_SWIFTRUNNER_WEST = "zz_ntb200wp_swiftrunner_west";
     public const string ZZ_NTB_WP_SWIFTRUNNER_EAST = "zz_ntb210wp_swiftrunner_east";
     public const string ZZ_NTB_WP_GATEKEEPER_FOREST = "zz_ntb210wp_gatekeeper_forest";
     public const string ZZ_NTB_WP_HERMIT = "zz_ntb210wp_hermit";
     public const string NTB_WP_HERMIT_JUMP = "ntb230wp_hermit_jump";
     public const string ZZ_NTB_WP_GRAND_OAK = "zz_ntb200wp_grand_oak";
     public const string ZZ_NTB_WP_TABLET = "zz_ntb330wp_tablet";
     public const string ZZ_NTB_WP_ALTAR_POOL = "zz_ntb330wp_altar_pool";
     public const string ZZ_NTB_WP_ARCANE_HORROR = "zz_ntb330wp_arcane_horror";
     public const string ZZ_NTB_WP_GATEKEEPER_RUINS = "zz_ntb340wp_gatekeeper_ruins";
     public const string ZZ_NTB_WP_DEYGAN = "zz_ntb200wp_deygan";
     public const string ZZ_NTB_WP_DANYLA = "zz_ntb210wp_danyla";
     public const string ZZ_NTB_WP_CAMP = "zz_ntb200wp_camp";
     public const string ZZ_NTB_WP_FALLEN_IRONBARK = "zz_ntb200wp_fallen_ironbark";
     public const string NTB_WP_ALCOVE_SPIDER_SWARM = "ntb310wp_alcove_spider_swarm";
     public const string NTB_WP_DRAGON_RETURN = "ntb310wp_dragon_return";
     public const string NTB_WP_QUEEN_SPIDER = "ntb320wp_queen_spider";
     public const string NTB_WP_SKELETON_JUNCTION = "ntb330wp_skeleton_junction";
     public const string NTB_WP_CHILD_SKELETONS = "ntb330wp_child_skeletons";
     public const string NTB_WP_SHADE_JUMP = "ntb330wp_shade_jump";
     public const string NTB_WP_GATEKEEPER_EXIT = "ntb210wp_gatekeeper_exit";
     public const string NTB_WP_RUINS_MINION1 = "ntb201wp_ruins_minion1";
     public const string NTB_WP_RUINS_MINION2 = "ntb201wp_ruins_minion2";
     public const string NTB_WP_RUINS_GATEKEEPER = "ntb201wp_ruins_gatekeeper";
     public const string NTB_WP_GATEKEEPER_WEREWOLVES = "ntb340wp_gatekeeper_werewolves";
     public const string NTB_WP_ELF_RALLY = "ntb100wp_elf_rally";
     public const string NTB_WP_ALCOVE_WEREWOLF = "ntb310wp_alcove_werewolf";
     public const string NTB_WP_LIGHTNING_01 = "ntb330wp_lightning_01";
     public const string NTB_WP_LIGHTNING_02 = "ntb330wp_lightning_02";
     public const string NTB_WP_LIGHTNING_03 = "ntb330wp_lightning_03";
     public const string NTB_WP_LIGHTNING_04 = "ntb330wp_lightning_04";
     public const string NTB_WP_LIGHTNING_05 = "ntb330wp_lightning_05";
     public const string NTB_WP_SW_MINION_E = "ntb200wp_sw_minion_e";
     public const string NTB_WP_SW_MINION_W = "ntb200wp_sw_minion_w";
     public const string NTB_WP_SWIFTRUNNER = "ntb200wp_swiftrunner";
     public const string NTB_WP_SWIFTRUNNER_RETREAT = "ntb200wp_swiftrunner_retreat";
     public const string NTB_WP_SW_WOLF01_RETREAT = "ntb200wp_sw_wolf1_retreat";
     public const string NTB_WP_SW_WOLF02_RETREAT = "ntb200wp_sw_wolf2_retreat";
     public const string NTB_WP_TABLET = "ntb330wp_tablet";
     public const string NTB_WP_WEREWOLF_PROTECTOR = "ntb340wp_werewolf_protector";
     public const string NTB_WP_GATEGUARD1 = "ntb340wp_guard1post";
     public const string NTB_WP_GATEGUARD3 = "ntb340wp_guard3post";
     public const string NTB_WP_GATEKEEPER_LAIRPOST = "ntb340wp_gatekeeperpost";
     public const string NTB_WP_HERMIT_VFX = "ntb210wp_hermit_vfx";

     public const string NTB_WP_ARCANE_HORROR_ = "ntb330wp_arcane_horror_";
     public const string NTB_WP_ARCANE_HORROR_1 = "ntb330wp_arcane_horror_1";
     public const string NTB_WP_ARCANE_HORROR_2 = "ntb330wp_arcane_horror_2";
     public const string NTB_WP_ARCANE_HORROR_3 = "ntb330wp_arcane_horror_3";
     public const string NTB_WP_ARCANE_HORROR_4 = "ntb330wp_arcane_horror_4";
     public const string NTB_WP_VFX_ = "ntb330wp_vfx_";
     public const string NTB_WP_VFX_1_ = "ntb330wp_vfx_1_";
     public const string NTB_WP_VFX_2_ = "ntb330wp_vfx_2_";
     public const string NTB_WP_VFX_3_ = "ntb330wp_vfx_3_";
     public const string NTB_WP_VFX_4_ = "ntb330wp_vfx_4_";
     public const string NTB_WP_VFX_1_1 = "ntb330wp_vfx_1_1";
     public const string NTB_WP_VFX_1_2 = "ntb330wp_vfx_1_2";
     public const string NTB_WP_VFX_1_3 = "ntb330wp_vfx_1_3";
     public const string NTB_WP_VFX_1_4 = "ntb330wp_vfx_1_4";
     public const string NTB_WP_VFX_2_1 = "ntb330wp_vfx_2_1";
     public const string NTB_WP_VFX_2_2 = "ntb330wp_vfx_2_2";
     public const string NTB_WP_VFX_2_3 = "ntb330wp_vfx_2_3";
     public const string NTB_WP_VFX_2_4 = "ntb330wp_vfx_2_4";
     public const string NTB_WP_VFX_3_1 = "ntb330wp_vfx_3_1";
     public const string NTB_WP_VFX_3_2 = "ntb330wp_vfx_3_2";
     public const string NTB_WP_VFX_3_3 = "ntb330wp_vfx_3_3";
     public const string NTB_WP_VFX_3_4 = "ntb330wp_vfx_3_4";
     public const string NTB_WP_VFX_4_1 = "ntb330wp_vfx_4_1";
     public const string NTB_WP_VFX_4_2 = "ntb330wp_vfx_4_2";
     public const string NTB_WP_VFX_4_3 = "ntb330wp_vfx_4_3";
     public const string NTB_WP_VFX_4_4 = "ntb330wp_vfx_4_4";
     public const string ZZ_NTB_WP_HEART_PATH = "zz_ntb210wp_heart_path";
     public const string ZZ_NTB_WP_PHYLACTERY = "zz_ntb330wp_phylactery";
     public const string NTB_WP_WEST_OGRE_SCOUT = "ntb200wp_ogre_scout";
     public const string NTB_WP_WEST_DEYGEN_RETURN = "ntb220wp_deygan_return";
     public const string NTB_WP_LADY_MAPNOTE = "ntb340mn_lady_of_the_forest";
     public const string NTB_WP_PROTECT1_POST = "ntb340wp_protect1_post";
     public const string NTB_WP_PROTECT3_POST = "ntb340wp_protect3_post";
     public const string NTB_WP_PROTECT5_POST = "ntb340wp_protect5_post";
     public const string NTB_WP_PROTECT6_POST = "ntb340wp_protect6_post";

     //AREAS

     public const string NTB_AR_DALISH_CAMP = "ntb100ar_dalish_camp";
     public const string NTB_AR_BRECILIAN_FORESTW = "ntb200ar_brecilian_forestnw";
     public const string NTB_AR_LAIR_OF_WEREWOLVES = "ntb340ar_lair_of_werewolves";
     public const string NTB_AR_TOP_LEVEL = "ntb310ar_top_level";
     public const string NTB_AR_LAIR_OF_THE_UNDEAD = "ntb330ar_lair_of_the_undead";
     public const string NTB_AR_BRECILIAN_FORESTE = "ntb210ar_brecilian_forestne";
     public const string DEFAULT_START_AREA = "default_start_area";

     //area lists

     public const string NTB_AL_DALISH_CAMP = "ntb01al_dalish_camp";
     public const string NTB_AL_BRECILIAN_FOREST = "ntb02al_brecilian_forest";
     public const string NTB_AL_ELVEN_RUINS = "ntb03al_elven_ruins";

     //triggers
     public const string NTB_TR_GATEKEEPER = "ntb230tr_gatekeeper";
     public const string NTB_TR_HEART_PATH = "ntb210tr_heart_path";
     public const string NTB_TR_SWIFTRUNNER_INIT = "ntb200tr_swiftrunner_init";
     public const string NTB_TR_SWIFTRUNNER_INIT2 = "ntb230tr_swiftrunner_init";
     public const string NTB_TR_WEST_WOLF = "ntb200tr_west_wolf";
     public const string NTB_TR_EAST_WEREWOLF_HERMIT = "ntb210tr_east_werewolf_hermit";
     public const string NTB_TR_EAST_WEREWOLF_AMBUSH = "ntb210tr_east_werewolf_ambush";
     public const string NTB_TR_EAST_WEREWOLF_DANYLA = "ntb210tr_east_werewolf_danyla";
     public const string NTB_TR_EAST_WEREWOLF_EAST = "ntb210tr_east_werewolf_east";
     public const string NTB_TR_SKELETON_SW = "ntb220tr_skeleton_sw";
     public const string NTB_TR_EAST_WEREWOLF_ROCKS = "ntb210tr_east_werewolf_rocks";
     public const string NTB_TR_EAST_WEREWOLF_CENTRAL = "ntb210tr_east_werewolf_central";
     public const string NTB_TR_EST_WEREWOLF_SOUTH_EXIT = "ntb210tr_est_werewolf_south_exit";
     public const string NTB_TR_SPIDER_SWARM_WEST = "ntb310tr_spider_swarm_west";
     public const string NTB_TR_SPIDER_SWARM_NORTH = "ntb310tr_spider_swarm_north";
     public const string NTB_TR_GROUND_SKELETONS = "ntb310tr_ground_skeletons";
     public const string NTB_TR_CAMPSITE = "ntb200tr_campsite";
     public const string NTB_TR_EAST_WEREWOLF_NORTH = "ntb210tr_east_werewolf_north";
     public const string NTB_TR_LIGHTNING_01 = "ntb330tr_lightning_01";
     public const string NTB_TR_LIGHTNING_02 = "ntb330tr_lightning_02";
     public const string NTB_TR_LIGHTNING_03 = "ntb330tr_lightning_03";
     public const string NTB_TR_LIGHTNING_04 = "ntb330tr_lightning_04";
     public const string NTB_TR_LIGHTNING_05 = "ntb330tr_lightning_05";
     public const string NTB_TR_ARCANE_HORROR_ = "ntb330tr_arcane_horror_";
     public const string NTB_TR_ARCANE_HORROR_1 = "ntb330tr_arcane_horror_1";
     public const string NTB_TR_ARCANE_HORROR_2 = "ntb330tr_arcane_horror_2";
     public const string NTB_TR_ARCANE_HORROR_3 = "ntb330tr_arcane_horror_3";
     public const string NTB_TR_ARCANE_HORROR_4 = "ntb330tr_arcane_horror_4";
     public const string NTB_TR_ZATHRIAN_INIT = "ntb310tr_zathrian_init";
     public const string NTB_TR_WOLF = "ntb200tr_wolf";
     public const string NTB_TR_WEST_FOREST_WOLF = "ntb200tr_west_forest_wolf";
     public const string NTB_TR_WEST_FOREST_WOLF2 = "ntb200tr_west_forest_wolf2";
     public const string NTB_TR_WEST_FOREST_WEREWOLVES2 = "ntb200tr_west_werewolf2";
     public const string NTB_TR_WEST_FOREST_OGRE = "ntb200tr_west_ogre";
     public const string NTB_TR_WEST_FOREST_HIDDEN_BEAR = "ntb200tr_west_hiddenbear";
     public const string NTB_TR_RUINS_SECRETDOOR_1 = "ntb310tr_secretdoor1";
     public const string NTB_TR_RUINS_SECRETDOOR_2 = "ntb310tr_secretdoor2";

     //placeables

     public const string NTB_IP_DOOR_SHADE = "ntb330ip_door_shade";
     public const string NTB_IP_DOOR_ARCHERS = "ntb330ip_door_archers";
     public const string NTB_IP_DOOR_SKELETONS = "ntb330ip_door_skeletons";
     public const string NTB_IP_DEAD_DEYGAN = "ntb200ip_dead_deygan";
     public const string NTB_IP_DOOR_GATEKEEPER_NORTH = "ntb340ip_door_gatekeeper_north";
     public const string NTB_IP_DOOR_GATEKEEPER_SOUTH = "ntb340ip_door_gatekeeper_south";
     public const string NTB_IP_DOOR_LADY = "ntb340ip_door_lady";
     public const string NTB_IP_DOOR_SHORTCUT = "ntb340ip_door_shortcut";
     public const string NTB_IP_SE_TO_RUINS = "ntb230ip_se_to_ruins";
     public const string NTB_IP_HEART_PATH_1 = "ntb210ip_heart_path_1";
     public const string NTB_IP_RUINS_TO_FOREST = "ntb310ip_ruins_to_forest";
     public const string WMT_WOW_ELF_RUINS_EXIT = "wmt_wow_elf_ruins_exit";
     public const string NTB_IP_CAMP_DESTROYED = "ntb200ip_camp_destroyed";
     public const string NTB_IP_CAMP_EXIT = "ntb200ip_camp_exit";
     public const string NTB_IP_CAMP_ENTRANCE = "ntb200ip_camp_entrance";
     public const string NTB_IP_HEART_PATH = "ntb210ip_heart_path";
     public const string NTB_IP_PHYLACTERY = "ntb330ip_phylactery";
     public const string NTB_IP_ALTAR = "ntb330ip_altar";
     public const string NTB_IP_ALTAR_CONVERSATION = "ntb330ip_altar_dialog";
     public const string NTB_IP_PHYLACTERY_ALTAR = "ntb330ip_phylactery_altar";
     public const string NTB_IP_HERMIT_STUMP = "ntb210ip_tree_stump";
     public const string NTB_IP_DEAD_HALLA = "ntb100ip_dead_halla";
     public const string NTB_IP_LANAYA_CHEST = "ntb100ip_lanaya_chest";
     public const string NTB_IP_IRONBARK_TREE = "ntb200ip_ironbark";

     public const string NTB_IP_VIALS_PHYLACTERY1 = "ntb330ip_lt_phylactery";
     public const string NTB_IP_VIALS_PHYLACTERY2 = "ntb340ip_lt_phylactery";
     public const string NTB_IP_LITE_MAGE_ALTAR = "ntb210ip_killer_altar"; //for lite_mage_killer

     public const string NTB_IP_ARC_HORROR_DROP = "ntb330ip_arc_hor.utp";

     //CONVERSATIONS
     public const string ZZ_NTB_DG_DEBUG = "zz_ntb000dg_debug.dlg";
     public const string rNTB_IP_CAMPSITE_06 = "ntb210ip_campsite_06.dlg";
     public const string rNTB_IP_HEART_PATH = "ntb210ip_heart_path.dlg";
     public const string rNTB_IP_CAMPSITE_02 = "ntb210ip_campsite_02.dlg";
     public const string rNTB_IP_CAMPSITE_01 = "ntb210ip_campsite_01.dlg";

     //CONSTANTS
     public const float NTB_HIT_POINTS_LOW = 1.0f;
     public const int NTB_LIGHTNING_BOLT_PROJECTILE = 124;
     public const int NTB_MAGICAL_APPARATUS_GLOW = 3027;//5023;
     public const int NTB_MAGICAL_APPARATUS_NW = 1;
     public const int NTB_MAGICAL_APPARATUS_NE = 2;
     public const int NTB_MAGICAL_APPARATUS_SE = 3;
     public const int NTB_MAGICAL_APPARATUS_SW = 4;
     public const int NTB_FIREBALL_IMPACT = 1002;

     //GROUPS
     public const int NTB_GROUP_SWIFTRUNNER = 22;
     public const int NTB_GROUP_WITHERFANG = 27;
     public const int NTB_GROUP_ZATHRAIN = 28;
     public const int NTB_GROUP_HOSTILE = 1;

     //TEAMS
     public const int NTB_TEAM_NOTHING = -1;
     public const int NTB_TEAM_TOP_LEVEL_WEREWOLF_ALCOVE = 1;
     public const int NTB_TEAM_TOP_LEVEL_SPIDER_ALCOVE = 2;
     public const int NTB_TEAM_WEREWOLF_LAIR_GATEKEEPER = 3;
     public const int NTB_TEAM_WEREWOLF_LAIR_LADY = 4;
     public const int NTB_TEAM_WEREWOLF_LAIR_GOLEM = 5;
     public const int NTB_TEAM_SWIFTRUNNER = 6;
     public const int NTB_TEAM_WEST_FOREST_WOLF = 7;
     public const int NTB_TEAM_EAST_FOREST_WEREWOLF_HERMIT = 8;
     public const int NTB_TEAM_WEST_FOREST_PANOWEN = 9;
     public const int NTB_TEAM_WEST_FOREST_SWIFTRUNNER = 10;
     public const int NTB_TEAM_EAST_FOREST_HERMIT_DEMON = 11;
     public const int NTB_TEAM_EAST_FOREST_WEREWOLF_AMBUSH = 12;
     public const int NTB_TEAM_EAST_FOREST_WEREWOLF_DANYLA = 13;
     public const int NTB_TEAM_WEST_FOREST_HIDDEN_BEAR = 14;
     public const int NTB_TEAM_EAST_FOREST_WEREWOLF_EAST = 15;
     public const int NTB_TEAM_TO_BE_USED = 16;
     public const int NTB_TEAM_WEST_FOREST_POST_SWIFTRUNNER = 17;
     public const int NTB_TEAM_CAMP_SICK_ELVES = 18;
     public const int NTB_TEAM_EAST_FOREST_WEREWOLF_ROCKS = 19;
     public const int NTB_TEAM_EAST_FOREST_WEREWOLF_CENTRAL = 20;
     public const int NTB_TEAM_EAST_FOREST_WEREWOLF_SOUTH_EXIT = 21;
     public const int NTB_TEAM_CAMP_ZATHRIAN = 22;
     public const int NTB_TEAM_UNDEAD_SKELETON_ARCHERS = 23;
     public const int NTB_TEAM_UNDEAD_SKELETONS = 24;
     public const int NTB_TEAM_CAMP_NON_COMBATANTS = 25;
     public const int NTB_TEAM_WEREWOLF_LAIR_ENTRY = 26;
     public const int NTB_TEAM_UNDEAD_SKELETON_JUNCTION = 27;
     public const int NTB_TEAM_UNDEAD_SKELETON_ROOMS = 28;
     public const int NTB_TEAM_WEREWOLF_LAIR_DEAD_UNDEAD = 29;
     public const int NTB_TEAM_EAST_FOREST_SWIFTRUNNER = 30;
     public const int NTB_TEAM_CAMP_GATING_WOLVES = 31;
     public const int NTB_TEAM_WEST_FOREST_BONES = 32;
     public const int NTB_TEAM_WEST_FOREST_CAMPSITE = 33;
     public const int NTB_TEAM_UNDEAD_SPIDERS = 34;
     public const int NTB_TEAM_WEST_FOREST_EXIT_WERES = 35;
     public const int NTB_TEAM_WEST_DEYGEN_RETRIEVERS = 36;
     public const int NTB_TEAM_UNDEAD_PREARCHER_SKELETONS = 37;
     public const int NTB_TEAM_EAST_FOREST_WEREWOLF_NORTH = 38;
     public const int NTB_TEAM_TOP_LEVEL_ALCOVE_DOORS = 39;
     public const int NTB_TEAM_UNDEAD_SKELETON_NW = 40;
     public const int NTB_TEAM_UNDEAD_SKELETON_NE = 41;
     public const int NTB_TEAM_UNDEAD_SKELETON_SE = 42;
     public const int NTB_TEAM_UNDEAD_SKELETON_SW = 43;
     public const int NTB_TEAM_UNDEAD_SHADES = 44;
     public const int NTB_TEAM_WEREWOLF_LAIR_HUMAN = 45;
     public const int NTB_TEAM_WEREWOLF_LAIR_SHADES = 46;
     public const int NTB_TEAM_UNDEAD_ALTAR_SHADES = 47;
     public const int NTB_TEAM_TOP_LEVEL_WEREWOLF = 48;
     public const int NTB_TEAM_TEVINTER_REVENANTPLOT_1 = 49;
     public const int NTB_TEAM_TEVINTER_REVENANTPLOT_2 = 50;
     public const int NTB_TEAM_TEVINTER_REVENANTPLOT_3 = 51;
     public const int NTB_TEAM_GATEKEEPER_GUARDS = 52;
     public const int NTB_TEAM_EAST_FOREST_GATEKEEPER = 53;
     public const int NTB_TEAM_WEST_FOREST_WOLF2 = 54;
     public const int NTB_TEAM_WEST_FOREST_WEREWOLVES2 = 55;
     public const int NTB_TEAM_WEST_FOREST_WEREWOLVES2A = 56;
     public const int NTB_TEAM_WEST_FOREST_OGRE = 57;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM1 = 61;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM2 = 62;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM3 = 63;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM4 = 64;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM5 = 65;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM6 = 66;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM7 = 67;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM8 = 68;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM9 = 69;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM10 = 70;
     public const int NTB_TEAM_UNDEAD_TRAP_TEAM11 = 71;
     public const int NTB_TEAM_CAMP_AMBIENT_WEREWOLF = 72;
     public const int NTP_TEAM_POST_GRAND_OAK = 73;
     public const int NTP_TEAM_POST_HERMIT = 74;
     public const int NTP_TEAM_POST_REVENANT = 75;
     public const int NTB_TEAM_EASTFOREST_OGRE = 76;
     public const int NTB_TEAM_LITE_MAGE_KILLER = 77;
     public const int NTB_TEAM_ARCANE_WARRIOR_SKELS = 78;
     public const int NTB_TEAM_ARCANE_WARRIOR_SKELS2 = 79;
     public const int NTB_TEAM_LITE_ROGUE_TERMS = 80;
}