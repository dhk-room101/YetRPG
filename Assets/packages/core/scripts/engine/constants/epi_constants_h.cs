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
     //:: epi_constants_h
     //:: Copyright (c) 2008 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         List of constants for the Epilogue
     */
     //:://////////////////////////////////////////////
     //:: Created By: Mark Barazzuol
     //:: Created On: May 26, 2008
     //:://////////////////////////////////////////////

     //public void main() {}

     /**************/
     // CREATURES
     /**************/
     // Party Characters
     public const string EPI_CR_ALISTAIR = "epi100cr_alistair";           // Alistair
     public const string EPI_CR_DOG = "epi200cr_dog";                // Dog
     public const string EPI_CR_LELIANA = "epi200cr_leliana";            // Leliana
     public const string EPI_CR_LOGHAIN = "epi200cr_loghain";            // Loghain
     public const string EPI_CR_OGHREN = "epi200cr_oghren";             // Oghren
     public const string EPI_CR_SHALE = "epi200cr_shale";              // Shale
     public const string EPI_CR_STEN = "epi200cr_sten";               // Sten
     public const string EPI_CR_WYNNE = "epi200cr_wynne";              // Wynne
     public const string EPI_CR_ZEVRAN = "epi200cr_zevran";             // Zevran

     // Minor Characters

     // Encounter Characters

     // Characters
     public const string EPI_CR_ANORA = "den510cr_anora";             // Anora
     public const string EPI_CR_GREAGOIR = "cir200cr_greagoir";          // Greagoir
     public const string EPI_CR_ASHALLE = "bed200cr_ashalle";           // Ashalle
     public const string EPI_CR_BRYLAND = "den220cr_bryland";           // Bryland
     public const string EPI_CR_CONNOR = "arl220cr_connor";            // Connor
     public const string EPI_CR_CULLEN = "cir230cr_cullen";            // Cullen
     public const string EPI_CR_CYRION = "bec110cr_father";            // Cyrion
     public const string EPI_CR_DULIN = "orz330cr_dulin";             // Dulin
     public const string EPI_CR_EAMON = "den211cr_eamon";             // Arl Eamon
     public const string EPI_CR_FERGUS = "bhn100cr_fergus";            // Fergus
     public const string EPI_CR_GENITIVI = "urn110cr_genitivi";          // Father Genitivi
     public const string EPI_CR_GORIM = "bdn120cr_gorim";             // Gorim
     public const string EPI_CR_GRANDCLERIC = "lot110cr_grand_cleric";      // Gorim
     public const string EPI_CR_IRVING = "cir240cr_irving";            // Irving
     public const string EPI_CR_ISOLDE = "arl100cr_isolde";            // Isolde
     public const string EPI_CR_JOWAN = "arl210cr_jowan";             // Jowan
     public const string EPI_CR_KALAH = "bdc210cr_mother";            // Kalah
     public const string EPI_CR_KARDOL = "orz510cr_kardol";            // Kardol
     public const string EPI_CR_KEEPER = "bed200cr_keeper";            // Keeper
     public const string EPI_CR_LADY = "ntb340cr_lady";              // Lady
     public const string EPI_CR_LANAYA = "ntb100cr_lanaya";            // Lanaya
     public const string EPI_CR_LILY = "den200cr_lily";              // Lily
     public const string EPI_CR_MARDY = "orz300cr_mardy";             // Mardy
     public const string EPI_CR_RICA = "bdc210cr_rica";              // Rica
     public const string EPI_CR_SERCAUTH = "den211cr_ser_cauthrien";     // Ser Cauthrien
     public const string EPI_CR_SHIANNI = "den300cr_shianni";           // Shianni
     public const string EPI_CR_SIGHARD = "den220cr_sighard";           // Sighard
     public const string EPI_CR_SORIS = "den511cr_soris";             // Soris
     public const string EPI_CR_TEAGEN = "arl110cr_teagan";            // Teagan
     public const string EPI_CR_VARTAG = "orz340cr_vartag";            // Vartag
     public const string EPI_CR_WULFF = "den220cr_wulff";             // Arl Wulff
     public const string EPI_CR_ZATHRIAN = "ntb100cr_zathrian";          // Zathrian
     public const string EPI_CR_ZERLINDA = "orz400cr_zerlinda";          // Zerlinda

     public const string EPI_CR_NOBLEMAN_M = "epi_nobleman_m";          // Nobleman M

     /**************/
     // WAYPOINTS
     /**************/
     public const string EPI_WP_START = "epi100wp_start";
     public const string AREA_CORE = "area_core.nss";
     public const string EPI_WP_DOG_SPAWN = "epi300wp_dog_spawn";

     /**************/
     // AREAS
     /**************/
     public const string EPI_AR_CORONATION = "epi100ar_coronation";
     public const string EPI_AR_FUNERAL = "epi200ar_players_funeral";
     public const string EPI_AR_POST_CORONATION = "epi300ar_post_coronation";

     /**************/
     // CUTSCENES
     /**************/

     // See cutscene constants file instead

     /**************/
     // Items
     /**************/

     // For Alistair
     public const string EPI_CLOTH_ALISTAIR_CHEST = "pre100im_kings_armor.uti";
     public const string EPI_CLOTH_ALISTAIR_GLOVES = "pre100im_kings_gloves.uti";
     public const string EPI_CLOTH_ALISTAIR_BOOTS = "pre100im_kings_boots.uti";

     // For Leliana if she has changed.
     public const string EPI_LELIANA_LEATHER_BOOTS = "gen_im_arm_bot_lgt_drb.uti";
     public const string EPI_LELIANA_LEATHER_GLOVES = "gen_im_arm_glv_lgt_drb.uti";
     public const string EPI_LELIANA_LEATHER_CHEST = "gen_im_arm_cht_lgt_drb.uti";

     // For Sten.
     public const string EPI_STEN_SWORD = "gen_im_wep_mel_gsw_stn.uti";

     /**************/
     // Dialogs
     /**************/
     public const string TALK_EPI_CORONATION_START = "cutscene_coronation.dlg";
     public const string TALK_EPI_FUNERAL_START = "cutscene_funeral.dlg";
     public const string TALK_EPI_POST_CORONATION_START = "cutscene_postcoronation.dlg";
     public const string TALK_EPI_SLIDESHOW = "cutscene_slideshow.dlg";
     public const string TALK_EPI_CREDITS = "cutscene_credits_ph.dlg";

     /**************/
     // Audio Events
     /**************/

     public const int EPI_AUDIO_POSTCORONATION_AMB_OFF = 65;
     public const int EPI_AUDIO_FUNERAL_AMB_OFF = 85;

     /**************/
     // Teams
     /**************/

     public const int EPI_TEAM_ALISTAIR_DEFAULT = 1;
     public const int EPI_TEAM_ALISTAIR_CHANGED = 2;
     public const int EPI_TEAM_ALISTAIR_WOMEN = 3;
     public const int EPI_TEAM_STEN_STAND_IN = 4;
     public const int EPI_TEAM_LELIANA_DEFAULT = 5;
     public const int EPI_TEAM_LELIANA_CHANGED = 6;
     public const int EPI_TEAM_LELIANA_CHANTRY = 7;
     public const int EPI_TEAM_ZEVRAN_DEFAULT = 8;
     public const int EPI_TEAM_ZEVRAN_BROMANCE = 9;
     public const int EPI_TEAM_ZEVRAN_ROMANCE = 10;
     public const int EPI_TEAM_ZEVRAN_SHADY = 11;
     public const int EPI_TEAM_FELSI = 12;
     public const int EPI_TEAM_WYNNE_DEFAULT = 13;
     public const int EPI_TEAM_WYNNE_SCHOLARS = 14;
}