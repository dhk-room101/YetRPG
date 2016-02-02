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
         Constants include for Light Content
     */
     //:://////////////////////////////////////////////
     //:: Created By: Keith
     //:: Created On: Jan 5/09
     //:://////////////////////////////////////////////

     //ITEMS (TAGS)

     public const string LITE_IM_UNBOUND_JOURNAL = "liteim_unbound_journal";
     public const string LITE_IM_UNBOUND_LETTER = "liteim_unbound_letter";
     public const string LITE_IM_EAMON_SPY_NOTE2 = "litim_eamon_spy_note";
     public const string LITE_IM_EAMON_SPY_NOTE3 = "litim_eamon_spy_note2";
     public const string LITE_IM_EAMON_SPY_NOTE4 = "litim_eamon_spy_note3";
     public const string LITE_IM_DESERTERS_SUPPLIES = "lite_fite_supplies";
     public const string LITE_IM_BLACKSTONE_BOX_1 = "litip_blackstone_box1";
     public const string LITE_IM_BLACKSTONE_BOX_2 = "litip_blackstone_box2";
     public const string LITE_IM_BLACKSTONE_BOX_3 = "litip_blackstone_box3";
     public const string LITE_IM_HEALTH_POULTICE_LESSER = "gen_im_qck_health_101";
     public const string LITE_IM_HEALTH_POULTICE = "gen_im_qck_health_201";
     public const string LITE_IM_HEALTH_POULTICE_GREATER = "gen_im_qck_health_301";
     public const string LITE_IM_HEALTH_POULTICE_POTENT = "gen_im_qck_health_401";
     public const string LITE_IM_ROPE_TRAP = "gen_im_qck_trap_104";
     public const string LITE_IM_MAGE_BANASTOR = "lite_mage_banastor";
     public const string LITE_IM_DEEP_MUSHROOM = "gen_im_cft_reg_mushroom";
     public const string LITE_IM_MAGE_TERMINATION = "lite_mage_termination";
     public const string LITE_IM_ROGUE_LETTER = "lite_rogue_letter";
     public const string LITE_IM_ROGUE_DWARF_TRAP_PART = "lite_rogue_dwarf_trap";
     public const string LITE_IM_ROGUE_BODYBAG = "lite_rogue_bodybag";
     public const string LITE_IM_MAGE_POWERGLYPH = "lite_mage_powerglyph";
     public const string LITE_IM_MAGE_RENOLDJOURNAL = "lite_mage_renoldbook";
     public const string LITE_IM_LESSER_LYRIUM_POTION = "gen_im_qck_mana_101";
     public const string LITE_IM_LYRIUM_POTION = "gen_im_qck_mana_201";
     public const string LITE_IM_GREATER_LYRIUM_POTION = "gen_im_qck_mana_301";
     public const string LITE_IM_POTENT_LYRIUM_POTION = "gen_im_qck_mana_401";
     public const string LITE_IM_KOR_MISS_LETTER = "litim_kor_miss_letter";
     public const string LITE_IM_KOR_MISS_LETTER2 = "litim_kor_miss_letter2";
     public const string LITE_IM_KOR_LASTWILL_WILL = "lite_kor_lastwill_will";
     public const string LITE_IM_KOR_SIGNS_NOTE = "litim_kor_signs_note";
     public const string LITE_IM_KOR_ASHES_BOOK = "lite_kor_localmyths";
     public const string LITE_IM_KOR_ASH_POUCH = "lite_kor_ash_pouch";
     public const string LITE_IM_CARTA_OPEN_IRON = "lit_im_carta_open_irn";
     public const string LITE_IM_CARTA_OPEN_RED = "lit_im_carta_open_red";
     public const string LITE_IM_CARTA_OPEN_STEEL = "lit_im_carta_open_ste";
     public const string LITE_IM_CARTA_RING_EMERALD = "lit_im_carta_ring_emd";
     public const string LITE_IM_CARTA_RING_GOLD = "lit_im_carta_ring_gld";
     public const string LITE_IM_CARTA_RING_SILVER = "lit_im_carta_ring_sil";
     public const string LITE_IM_CARTA_TRINKET_FLO = "lit_im_carta_trkt_flo";
     public const string LITE_IM_CARTA_TRINKET_GAR = "lit_im_carta_trkt_gar";
     public const string LITE_IM_CARTA_TRINKET_MAL = "lit_im_carta_trkt_mal";
     public const string LITE_IM_ROGUE_DIRECTIONS = "lite_rogue_directions";
     public const string LITE_IM_ROGUE_TERMS_NOTE = "lite_rogue_terms_note";

     //ITEMS (RESOURCES)
     public const string rLITE_IM_STOCK_ELFROOT = "gen_im_cft_reg_elfroot.uti";
     public const string rLITE_IM_STOCK_DEATHROOT = "gen_im_cft_reg_deathroot.uti";
     public const string rLITE_IM_STOCK_MUSHROOM = "gen_im_cft_reg_mushroom.uti";
     public const string rLITE_IM_STOCK_METALSHARD = "gen_im_cft_reg_metalshard.uti";
     public const string rLITE_IM_STOCK_NUG = "gen_im_gft_nugg.uti";
     public const string rLITE_IM_STOCK_AMETHYST = "gen_im_gem_ame.uti";
     public const string rLITE_IM_STOCK_MALACHITE = "gen_im_gem_mal.uti";
     public const string rLITE_IM_STOCK_TOPAZ = "gen_im_gem_top.uti";
     public const string rLITE_IM_STOCK_SAPPHIRE = "gen_im_gem_sap.uti";

     public const string rLITE_IM_KOR_SIGNS_NOTE = "litim_kor_signs_note.uti";

     //runes
     public const string rLITE_IM_STOCK_RUN_EXP_CHR = "gen_im_upg_run_exp_chr.uti";
     public const string rLITE_IM_STOCK_RUN_EXP_CIR = "gen_im_upg_run_exp_cir.uti";
     public const string rLITE_IM_STOCK_RUN_EXP_HAL = "gen_im_upg_run_exp_hal.uti";
     public const string rLITE_IM_STOCK_RUN_EXP_SIL = "gen_im_upg_run_exp_sil.uti";
     public const string rLITE_IM_STOCK_RUN_EXP_SLW = "gen_im_upg_run_exp_slw.uti";
     //*
     public const string rLITE_IM_STOCK_RUN_EXP_DWE = "gen_im_upg_cry_exp_dwe.uti";
     public const string rLITE_IM_STOCK_RUN_EXP_FLM = "gen_im_upg_cry_exp_flm.uti";
     public const string rLITE_IM_STOCK_RUN_EXP_FRS = "gen_im_upg_cry_exp_frs.uti";
     public const string rLITE_IM_STOCK_RUN_EXP_PAR = "gen_im_upg_cry_exp_par.uti";
     //*

     public const string rLITE_IM_STOCK_RUN_JNY_CHR = "gen_im_upg_run_jny_chr.uti";
     public const string rLITE_IM_STOCK_RUN_JNY_CIR = "gen_im_upg_run_jny_cir.uti";
     public const string rLITE_IM_STOCK_RUN_JNY_HAL = "gen_im_upg_run_jny_hal.uti";
     public const string rLITE_IM_STOCK_RUN_JNY_SIL = "gen_im_upg_run_jny_sil.uti";
     public const string rLITE_IM_STOCK_RUN_JNY_SLW = "gen_im_upg_run_jny_slw.uti";
     //*
     public const string rLITE_IM_STOCK_RUN_JNY_DWE = "gen_im_upg_cry_jny_dwe.uti";
     public const string rLITE_IM_STOCK_RUN_JNY_FLM = "gen_im_upg_cry_jny_flm.uti";
     public const string rLITE_IM_STOCK_RUN_JNY_FRS = "gen_im_upg_cry_jny_frs.uti";
     public const string rLITE_IM_STOCK_RUN_JNY_PAR = "gen_im_upg_cry_jny_par.uti";
     //*

     public const string rLITE_IM_STOCK_RUN_MAS_CHR = "gen_im_upg_run_mas_chr.uti";
     public const string rLITE_IM_STOCK_RUN_MAS_CIR = "gen_im_upg_run_mas_cir.uti";
     public const string rLITE_IM_STOCK_RUN_MAS_HAL = "gen_im_upg_run_mas_hal.uti";
     public const string rLITE_IM_STOCK_RUN_MAS_SIL = "gen_im_upg_run_mas_sil.uti";
     public const string rLITE_IM_STOCK_RUN_MAS_SLW = "gen_im_upg_run_mas_slw.uti";
     //*
     public const string rLITE_IM_STOCK_RUN_MAS_DWE = "gen_im_upg_cry_mas_dwe.uti";
     public const string rLITE_IM_STOCK_RUN_MAS_FLM = "gen_im_upg_cry_mas_flm.uti";
     public const string rLITE_IM_STOCK_RUN_MAS_FRS = "gen_im_upg_cry_mas_frs.uti";
     public const string rLITE_IM_STOCK_RUN_MAS_PAR = "gen_im_upg_cry_mas_par.uti";
     //*

     public const string rLITE_IM_STOCK_RUN_NOV_CHR = "gen_im_upg_run_nov_chr.uti";
     public const string rLITE_IM_STOCK_RUN_NOV_CIR = "gen_im_upg_run_nov_cir.uti";
     public const string rLITE_IM_STOCK_RUN_NOV_HAL = "gen_im_upg_run_nov_hal.uti";
     public const string rLITE_IM_STOCK_RUN_NOV_SIL = "gen_im_upg_run_nov_sil.uti";
     public const string rLITE_IM_STOCK_RUN_NOV_SLW = "gen_im_upg_run_nov_slw.uti";
     //*
     public const string rLITE_IM_STOCK_RUN_NOV_DWE = "gen_im_upg_cry_nov_dwe.uti";
     public const string rLITE_IM_STOCK_RUN_NOV_FLM = "gen_im_upg_cry_nov_flm.uti";
     public const string rLITE_IM_STOCK_RUN_NOV_FRS = "gen_im_upg_cry_nov_frs.uti";
     public const string rLITE_IM_STOCK_RUN_NOV_PAR = "gen_im_upg_cry_nov_par.uti";
     //*
     //end of runes

     public const string rLITE_IM_FITE_CONSCRIPT_LET = "lite_fite_conscription.uti";
     public const string rLITE_IM_FITE_DESERTERS_SUP = "lite_fite_supplies.uti";
     public const string rLITE_IM_FITE_GREASE_LET = "lite_fite_grease.uti";
     public const string rLITE_IM_HEALTH_POUL_LESSER = "gen_im_qck_health_101.uti";
     public const string rLITE_IM_HEALTH_POUL = "gen_im_qck_health_201.uti";
     public const string rLITE_IM_HEALTH_POUL_GREATER = "gen_im_qck_health_301.uti";
     public const string rLITE_IM_HEALTH_POUL_POTENT = "gen_im_qck_health_401.uti";
     public const string rLITE_IM_FITE_ROPE_TRAP = "gen_im_qck_trap_104.uti";
     public const string rLITE_IM_FITE_CONDOLENCE_LET = "lite_fite_condolences.uti";
     public const string rLITE_IM_MAGE_BANASTOR = "lite_mage_banastor.uti";
     public const string rLITE_IM_ROGUE_GREENSTONE = "gen_im_gem_gar.uti";
     public const string rLITE_IM_ROGUE_LETTER = "lite_rogue_letter.uti";
     public const string rLITE_IM_ROGUE_VENOM = "gen_im_cft_reg_venom.uti";
     public const string rLITE_IM_ROGUE_TRAP_PART = "lite_rogue_dwarf_trap_part.uti";
     public const string rLITE_IM_ROGUE_BODYBAG = "lite_rogue_bodybag.uti";
     public const string rLITE_IM_MAGE_TERMINATION = "lite_mage_termination.uti";
     public const string rLITE_IM_MAGE_POWERGLYPH = "lite_mage_powerglyph.uti";
     public const string rLITE_IM_MAGE_RENOLDJOURNAL = "lite_mage_renoldbook.uti";
     public const string rLITE_IM_LESSER_LYRIUM_POTION = "gen_im_qck_mana_101.uti";
     public const string rLITE_IM_LYRIUM_POTION = "gen_im_qck_mana_201.uti";
     public const string rLITE_IM_GREATER_LYRIUM_POTION = "gen_im_qck_mana_301.uti";
     public const string rLITE_IM_POTENT_LYRIUM_POTION = "gen_im_qck_mana_401.uti";
     public const string rLITE_IM_MAGE_WARNING_BLOOD = "lite_mage_goatsbloodl.uti";
     public const string rLITE_IM_MAGE_DEFEND_TEST = "lite_mage_testimony.uti";
     public const string rLITE_IM_KOR_LOCKBOX = "lite_kor_lastwill_lockbox.uti";
     public const string rLITE_IM_KOR_AMULET = "lite_kor_lastwill_amulet.uti";
     public const string rLITE_IM_KOR_EMERALD = "gen_im_gem_emr.uti";
     public const string rLITE_IM_KOR_MALACHITE = "gen_im_gem_mal.uti";
     public const string rLITE_IM_CARTA_JAMMER_KEY = "lit_im_carta_jam_key.uti";
     public const string rLITE_IM_CARTA_OPEN_IRON = "lit_im_carta_open_irn.uti";
     public const string rLITE_IM_CARTA_OPEN_RED = "lit_im_carta_open_red.uti";
     public const string rLITE_IM_CARTA_OPEN_STEEL = "lit_im_carta_open_ste.uti";
     public const string rLITE_IM_CARTA_RING_EMERALD = "lit_im_carta_ring_emd.uti";
     public const string rLITE_IM_CARTA_RING_GOLD = "lit_im_carta_ring_gld.uti";
     public const string rLITE_IM_CARTA_RING_SILVER = "lit_im_carta_ring_sil.uti";
     public const string rLITE_IM_CARTA_TRINKET_FLO = "lit_im_carta_trkt_flo.uti";
     public const string rLITE_IM_CARTA_TRINKET_GAR = "lit_im_carta_trkt_gar.uti";
     public const string rLITE_IM_CARTA_TRINKET_MAL = "lit_im_carta_trkt_mal.uti";

     public const string rLITE_IM_CORPSE_GALL = "gen_it_corpse_gall.uti";

     //CREATURES
     public const string LITE_CR_UNBOUND_ADVENTURER = "litecr_multi_gax_advent";
     public const string LITE_CR_UNBOUND_GAX = "litecr_multi_gax_gax";
     public const string LITE_CR_UNBOUND_GAX_DEMON = "litecr_multi_gax_demon";
     public const string LITE_CR_EAMON_SPY_FRAN = "lite_eamon_spy_fran";
     public const string LITE_CR_EAMON_SPY_GUARD = "lite_eamon_spy_guard";
     public const string LITE_CR_FITE_CONSCRIPT_DERNAL = "lite_conscript_dernal";
     public const string LITE_CR_FITE_CONSCRIPT_PATTER = "lite_conscripts_patter";
     public const string LITE_CR_FITE_CONSCRIPT_VAREL = "lite_conscripts_varel";
     public const string LITE_CR_GREASE_COURIER_1 = "lite_dencr_courier1";
     public const string LITE_CR_GREASE_COURIER_2 = "lite_dencr_courier2";
     public const string LITE_CR_GREASE_COURIER_3 = "lite_dencr_courier3";
     public const string LITE_CR_GREASE_COURIER_4 = "lite_dencr_courier4";
     public const string LITE_CR_GREASE_COURIER_5 = "lite_dencr_courier5";
     public const string LITE_CR_CONDOLENCES_WIDOW1 = "lite_fite_widow_irenia";
     public const string LITE_CR_CONDOLENCES_WIDOW2 = "lite_fite_widow_larana";
     public const string LITE_CR_CONDOLENCES_WIDOW3 = "lite_fite_widow_sara";
     public const string LITE_CR_CONDOLENCES_WIDOW4 = "lite_fite_widow_tania";
     public const string LITE_CR_FITE_BLACKSTONE = "lite_fite_blackstone";
     public const string LITE_CR_MAGE_COLLECTIVE = "lite_mage_collective";
     public const string LITE_CR_ACQUISITIONS = "lite_rogue_board";
     public const string LITE_CR_MAGE_TERMINATION1 = "lite_mage_termination1";
     public const string LITE_CR_MAGE_TERMINATION2 = "lite_mage_termination2";
     public const string LITE_CR_MAGE_TERMINATION3 = "lite_mage_termination3";
     public const string LITE_CR_ROGUE_SCARED1 = "lite_rogue_clean_scared1";
     public const string LITE_CR_ROGUE_SCARED2 = "lite_rogue_clean_scared2";
     public const string LITE_CR_ROGUE_SCARED3 = "lite_rogue_clean_scared3";
     public const string LITE_CR_ROGUE_WITNESS1 = "lite_rogue_false_witness1";
     public const string LITE_CR_ROGUE_WITNESS2 = "lite_rogue_false_witness2";
     public const string LITE_CR_ROGUE_WITNESS3 = "lite_rogue_false_witness3";
     public const string LITE_CR_ROGUE_TERMS_REVENGE = "lite_rogue_terms_revenge1";
     public const string LITE_CR_MAGE_SILENCE_HARRITH = "lite_mage_harrith";
     public const string LITE_CR_MAGE_TAVISH = "lite_mage_tavish";
     public const string LITE_CR_KOR_GAZARATH = "lite_kor_gazarath";
     public const string LITE_CR_ROGUE_K_LIEUT = "lite_rogue_competition2";
     public const string LITE_CR_ROGUE_D_LIEUT = "lite_rogue_competition1";
     public const string LITE_CR_ROGUE_BARTENDER = "den220cr_bartender";
     public const string LITE_CR_RED_JETTA = "lite_red_jetta";
     public const string LITE_CR_ROGUE_GUARD_CONTACT = "lite_rogue_board_decide";

     //PLACEABLES
     public const string LITE_IP_UNBOUND_GAXDOOR = "den951ip_to_gaz";
     public const string LITE_IP_UNBOUND_GAXDOOR_TALKER = "den951ip_to_gaz_talker";
     public const string LITE_IP_EAMON_SPY_NOTE = "liteip_eamon_spy_note";
     public const string LITE_IP_EAMON_SPY_NOTE2 = "liteip_eamon_spy_note2";
     public const string LITE_IP_EAMON_SPY_DOOR = "litip_door_eamon_spy";
     public const string LITE_IP_BLACKSTONE_BOX = "litip_blackstone_box";
     public const string LITE_IP_ROGUE_BOX = "liteip_rogue_box";
     public const string LITE_IP_MAGE_BAG_1 = "litip_mage_bag1";
     public const string LITE_IP_MAGE_BAG_2 = "litip_mage_bag2";
     public const string LITE_IP_MAGE_BAG_3 = "litip_mage_bag3";
     public const string LITE_IP_ROGUE_BODYBAG1 = "liteip_rogue_bodybag1";
     public const string LITE_IP_ROGUE_BODYBAG2 = "liteip_rogue_bodybag2";
     public const string LITE_IP_ROGUE_BODYBAG3 = "liteip_rogue_bodybag3";
     public const string LITE_IP_MAGE_MYSTICSITE = "litip_mage_mysticsite";
     public const string LITE_IP_ROGUE_DUMPSITE = "liteip_rogue_dumpsite";
     public const string LITE_IP_ROGUE_MESSAGE = "liteip_rogue_message";
     public const string LITE_IP_MAGE_WARNDOOR_1 = "litip_mage_warndoor1";
     public const string LITE_IP_MAGE_WARNDOOR_2 = "litip_mage_warndoor2";
     public const string LITE_IP_MAGE_WARNDOOR_3 = "litip_mage_warndoor3";
     public const string LITE_IP_MAGE_WARNDOOR_4 = "litip_mage_warndoor4";
     public const string LITE_IP_LASTWILL_CACHE = "litip_kor_will_cache";
     public const string LITE_IP_KOR_TRAILSIGN_1 = "litip_kor_trail_sign1";
     public const string LITE_IP_KOR_TRAILSIGN_2 = "litip_kor_trail_sign2";
     public const string LITE_IP_KOR_TRAILSIGN_3 = "litip_kor_trail_sign3";
     public const string LITE_IP_KOR_TRAILSIGN_4 = "litip_kor_trail_sign4";
     public const string LITE_IP_KOR_TRAILSIGN_5 = "litip_kor_trail_sign5";
     public const string LITE_IP_KOR_TRAILSIGN_6 = "litip_kor_trail_sign6";
     public const string LITE_IP_KOR_TRAILSIGN_7 = "litip_kor_trail_sign7";
     public const string LITE_IP_KOR_TRAIL_CACHE = "litip_kor_trail_cache";
     public const string LITE_IP_ROGUE_DEAD_DROP_1 = "liteip_rogue_deaddrop1";
     public const string LITE_IP_ROGUE_DEAD_DROP_2 = "liteip_rogue_deaddrop2";
     public const string LITE_IP_ROGUE_DEAD_DROP_3 = "liteip_rogue_deaddrop3";
     public const string LITE_IP_ROGUE_DEAD_DROP_4 = "liteip_rogue_deaddrop4";
     public const string LITE_IP_ROGUE_BARREL_1 = "liteip_rogue_barrel1";
     public const string LITE_IP_ROGUE_BARREL_2 = "liteip_rogue_barrel2";
     public const string LITE_IP_ROGUE_BARREL_3 = "liteip_rogue_barrel3";
     public const string LITE_IP_ROGUE_BARREL_4 = "liteip_rogue_barrel4";
     public const string LITE_IP_KOR_ASH_RUBBLE = "litip_kor_ash_rubble";
     public const string LITE_IP_KOR_ASH_CONVERSATION = "litip_kor_ash_dialog";

     public const string rLITE_IP_RENOLD_ABOM = "litip_renold_abom.utp";
     public const string rLITE_IP_GAZ_RAGS = "liteip_gaz_rags.utp";

     //WAYPOINTS
     public const string LITE_IP_UNBOUND_GAXHOME = "mn_den951_from_den972";
     public const string LITE_IP_UNBOUND_IN_GAXHOME = "wpden972_from_den951";
     public const string LITE_IP_EAMON_SPY_EXIT = "den211wp_from_exterior_left";
     public const string LITE_IP_EAMON_SPY_GUARD_POST = "den212wp_eamon_spy";
     public const string LITE_WP_KOR_TRAILSIGN_1 = "mnpre200_trailsign_1";
     public const string LITE_WP_KOR_TRAILSIGN_2 = "mnpre200_trailsign_2";
     public const string LITE_WP_KOR_TRAILSIGN_3 = "mnpre200_trailsign_3";
     public const string LITE_WP_KOR_TRAILSIGN_4 = "mnpre200_trailsign_4";
     public const string LITE_WP_KOR_TRAILSIGN_5 = "mnpre200_trailsign_5";
     public const string LITE_WP_KOR_TRAILSIGN_6 = "mnpre200_trailsign_6";
     public const string LITE_WP_KOR_TRAILSIGN_7 = "mnpre200_trailsign_7";
     public const string LITE_WP_KOR_CACHE = "mnpre200_trail_cache";

     //TEAMS
     public const int LITE_TEAM_UNBOUND_GAXDEMON = 972;
     public const int LITE_TEAM_FITE_DESERTERS_3 = 973;
     public const int LITE_TEAM_EAMON_SPY = 211;
     public const int LITE_TEAM_EAMON_SPY_NPCS = 212;
     public const int LITE_TEAM_FITE_TAORAN = 140;
     //public const int DEN_TEAM_ROGUE_TERMS_REVENGE_1                = 954;  - set in den_lc_constants_h
     //public const int DEN_TEAM_ROGUE_TERMS_REVENGE_2                = 955;
     //public const int DEN_TEAM_ROGUE_TERMS_REVENGE_3                = 956;
     //public const int LIT_TEAM_FITE_LEADERSHIP_RAELNOR              = 612;  - set in denerim constants
}