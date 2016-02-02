//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Engine
{
     //::///////////////////////////////////////////////
     //:: epi_attendees_h
     //:: Copyright (c) 2008 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         Functions to trigger proper
         attendees for the Epilogue
     */
     //:://////////////////////////////////////////////
     //:: Created By: Mark Barazzuol
     //:: Created On: June 12, 2008
     //:://////////////////////////////////////////////

     //#include"utility_h"
     //#include"epi_constants_h"
     //#include"camp_constants_h"

     //#include"plt_arl100pt_enter_castle"
     //#include"plt_arl200pt_remove_demon"
     //#include"plt_bdn100pt_noble_hunters"
     //#include"plt_clipt_archdemon"
     //#include"plt_clipt_alistair"
     //#include"plt_clipt_morrigan_ritual"
     //#include"plt_cir000pt_main"
     //#include"plt_denpt_alistair"
     //#include"plt_denpt_anora"
     //#include"plt_denpt_main"
     //#include"plt_denpt_rescue_the_queen"
     //#include"plt_gen00pt_backgrounds"
     //#include"plt_gen00pt_party"
     //#include"plt_ntb000pt_main"
     //#include"plt_ntb100pt_lanaya"
     //#include"plt_orz400pt_zerlinda"
     //#include"plt_orz510pt_legion"
     //#include"plt_orzpt_main"
     //#include"plt_ranpt_generic_actions"
     //#include"plt_urn200pt_cult"
     //#include"plt_genpt_leliana_main"
     //#include"plt_genpt_sten_main"
     //#include"plt_genpt_oghren_main"
     //#include"plt_gen00pt_class_race_gend"

     //#include"plt_genpt_app_zevran"
     //#include"plt_genpt_app_leliana"
     //#include"plt_genpt_app_alistair"

     //#include"plt_urnpt_main"

     //#include"plt_zz_epi_debug"

     // Function to show if all NPCs should appear in epilogue.
     // This is a debug command.
     public int IsAllNPCShowing()
     {
          return WR_GetPlotFlag(EngineConstants.PLT_ZZ_EPI_DEBUG, EngineConstants.ZZ_EPI_DEBUG_SET_FULL_NPCS_ATTENDING);
     }

     public void SetKingAndQueen()
     {

          GameObject oPC = GetHero();

          // Alistair Vars
          int bSoleKing = WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ALISTAIR_IS_KING);
          int bAlistairAnoraMarried = WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ALISTAIR_ENGAGED_TO_ANORA);
          int bAlistairKillArchdemon = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_ARCHDEMON, EngineConstants.CLIMAX_ARCHDEMON_ALISTAIR_KILLS_ARCHDEMON);
          int bAlistairRitual = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MORRIGAN_RITUAL, EngineConstants.MORRIGAN_RITUAL_WITH_ALISTAIR);
          int bAlistairAlive;
          int bAlistairWedPlayer = WR_GetPlotFlag(EngineConstants.PLT_DENPT_ALISTAIR, EngineConstants.DEN_ALISTAIR_MARRYING_PLAYER);
          int bAlistairAtFinalBattle = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_ALISTAIR, EngineConstants.CLIMAX_ALISTAIR_WITH_PARTY_AT_ARCHDEMON_FIGHT);
          GameObject oAlistair = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_ALISTAIR);

          // Anora Vars
          GameObject oAnora = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_ANORA);
          int bSoleQueen = WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ANORA_IS_QUEEN);
          int bAnoraOnThrone = WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ANORA_ON_THRONE, EngineConstants.TRUE);
          int bAnoraWedPlayer = WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_PLAYER_IS_KING);

          // Arl Eamon Vars
          GameObject oEamon = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_EAMON);

          // Check to see if Alistair is alive
          if (bAlistairKillArchdemon != EngineConstants.FALSE && bAlistairRitual == EngineConstants.FALSE)
          {
               // Alistair is dead
               bAlistairAlive = EngineConstants.FALSE;
          }
          else bAlistairAlive = EngineConstants.TRUE;

          // If Alistair is alive, and he's on the throne or was at the final
          // battle he will be at the coronation.
          if ((bAlistairAlive != EngineConstants.FALSE) &&
          (bAlistairAnoraMarried != EngineConstants.FALSE || bSoleKing != EngineConstants.FALSE || bAlistairWedPlayer != EngineConstants.FALSE || bAlistairAtFinalBattle != EngineConstants.FALSE))
          {
               WR_SetObjectActive(oAlistair, EngineConstants.TRUE);

               // If Alistair is king Arl Eamon is there.
               if (bSoleKing != EngineConstants.FALSE || bAlistairAnoraMarried != EngineConstants.FALSE || bAlistairWedPlayer != EngineConstants.FALSE)
                    WR_SetObjectActive(oEamon, EngineConstants.TRUE);
          }

          // If Anora is on the throne OR
          // Anora is marrying the player OR
          // Alistair is dead.
          if (bAnoraOnThrone != EngineConstants.FALSE || bAnoraWedPlayer != EngineConstants.FALSE || bAlistairAlive == EngineConstants.FALSE)
               WR_SetObjectActive(oAnora, EngineConstants.TRUE);

     }

     public void SetPartyMembersAttending()
     {
          GameObject oPC = GetHero();

          //Dog
          int bDog = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_RECRUITED);
          GameObject oDog = GetObjectByTag(EngineConstants.EPI_CR_DOG);
          GameObject oPartyDog = GetObjectByTag(EngineConstants.GEN_FL_DOG);
          string sDogName = GetName(oPartyDog);
          //if dog has been recruited - we need to grab the dog GameObject from the party pool and set it active and jump it to its post
          if (bDog != EngineConstants.FALSE)
          {
               WR_SetObjectActive(oDog, EngineConstants.TRUE);
               SetName(oDog, sDogName);
          }

          //Lelania
          int bLeliana = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_RECRUITED);
          GameObject oLelania = GetObjectByTag(EngineConstants.EPI_CR_LELIANA);
          if (bLeliana != EngineConstants.FALSE)
          {
               WR_SetObjectActive(oLelania, EngineConstants.TRUE);
               EPI_LelianaCrowd();
          }

          //Loghain
          // Check if Logain died in climax
          int bLoghain = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LOGHAIN_RECRUITED);
          int bLoghainRitual = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MORRIGAN_RITUAL, EngineConstants.MORRIGAN_RITUAL_WITH_LOGHAIN);
          int bLoghainDeath = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_ARCHDEMON, EngineConstants.CLIMAX_ARCHDEMON_LOGHAIN_KILLS_ARCHDEMON);
          GameObject oLoghain = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_LOGHAIN);

          // IF:  Recruited AND
          // IF: Either Died, and had ritual, or Didn't die.
          if ((bLoghain != EngineConstants.FALSE) &&
            ((bLoghainDeath != EngineConstants.FALSE && bLoghainRitual != EngineConstants.FALSE) || (bLoghainDeath == EngineConstants.FALSE)))
               WR_SetObjectActive(oLoghain, EngineConstants.TRUE);

          //Oghren
          int bOghren = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_OGHREN_RECRUITED);
          GameObject oOghren = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_OGHREN);
          if (bOghren != EngineConstants.FALSE)
          {
               WR_SetObjectActive(oOghren, EngineConstants.TRUE);
               EPI_OghrenCrowd();
          }

          //Shale
          int bShale = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_SHALE_RECRUITED);
          GameObject oShale = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_SHALE);
          if (bShale != EngineConstants.FALSE)
               WR_SetObjectActive(oShale, EngineConstants.TRUE);

          //Sten
          int bSten = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_RECRUITED);
          int bStenSword = WR_GetPlotFlag(EngineConstants.PLT_GENPT_STEN_MAIN, EngineConstants.STEN_MAIN_HAS_SWORD_BACK);
          GameObject oSten = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_STEN);
          GameObject oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oSten);

          if (bSten != EngineConstants.FALSE)
          {
               WR_SetObjectActive(oSten, EngineConstants.TRUE);

               if (bStenSword != EngineConstants.FALSE)
               {
                    // Give Sten
                    UnequipItem(oSten, oItem);

                    GameObject oSword = CreateItemOnObject(EngineConstants.EPI_STEN_SWORD, oSten, 1, "", EngineConstants.TRUE);

                    EquipItem(oSten, oSword, EngineConstants.INVENTORY_SLOT_MAIN);

               }

          }

          else
          {
               UT_TeamAppears(EngineConstants.EPI_TEAM_STEN_STAND_IN, EngineConstants.TRUE);
          }

          //Wynne
          int bWynne = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_RECRUITED);
          GameObject oWynne = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_WYNNE);
          if (bWynne != EngineConstants.FALSE)
          {
               WR_SetObjectActive(oWynne, EngineConstants.TRUE);
               EPI_WynneCrowd();
          }

          //Zevran
          int bZevran = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_RECRUITED);
          GameObject oZevran = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_ZEVRAN);
          if (bZevran != EngineConstants.FALSE)
          {
               WR_SetObjectActive(oZevran, EngineConstants.TRUE);
               EPI_ZevranCrowd();
          }

     }

     public void SetOriginMembersAttending()
     {

          GameObject oPC = GetHero();

          //Ashalle
          int bElfDalish = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_ELF_DALISH);
          GameObject oAshalle = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_ASHALLE);
          if (bElfDalish != EngineConstants.FALSE)
               WR_SetObjectActive(oAshalle, EngineConstants.TRUE);

          //Cyrion
          int bElfCity = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_ELF_CITY);
          GameObject oCyrion = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_CYRION);
          if (bElfCity != EngineConstants.FALSE)
               WR_SetObjectActive(oCyrion, EngineConstants.TRUE);

          //Fergus
          int bHumanNoble = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_HUMAN_NOBLE);
          GameObject oFergus = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_FERGUS);
          if (bHumanNoble != EngineConstants.FALSE)
               WR_SetObjectActive(oFergus, EngineConstants.TRUE);

          //Gorim
          int bDwarfNoble = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_NOBLE);
          GameObject oGorim = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_GORIM);
          if (bDwarfNoble != EngineConstants.FALSE)
               WR_SetObjectActive(oGorim, EngineConstants.TRUE);

          //Irving
          int bIrvingDead = WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.ALL_MAGES_DEAD);
          int bMageOrigin = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_CIRCLE);
          GameObject oIrving = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_IRVING);
          if ((bIrvingDead == EngineConstants.FALSE) && (bMageOrigin != EngineConstants.FALSE))
               WR_SetObjectActive(oIrving, EngineConstants.TRUE);

          //Rica
          int bDwarfCommoner = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_COMMONER);
          GameObject oRica = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_RICA);
          if (bDwarfCommoner != EngineConstants.FALSE)
               WR_SetObjectActive(oRica, EngineConstants.TRUE);

          //Leliana's Nug Scmooples
          int bLeliana = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_RECRUITED);
          int bNug = WR_GetPlotFlag(EngineConstants.PLT_GENPT_LELIANA_MAIN, EngineConstants.LELIANA_MIAN_LELIANA_HAS_NUG);
          GameObject oNug = UT_GetNearestCreatureByTag(oPC, EngineConstants.CAMP_NUG);
          if (bLeliana != EngineConstants.FALSE && bNug != EngineConstants.FALSE)
               WR_SetObjectActive(oNug, EngineConstants.TRUE);

     }

     public void SetOtherNPCsAttending()
     {

          GameObject oPC = GetHero();

          // Check for debug, showing all NPCs
          int bAllNPCs = IsAllNPCShowing();

          // Bryland
          // Always supports you and is always there.
          GameObject oBryland = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_BRYLAND);
          WR_SetObjectActive(oBryland, EngineConstants.TRUE);

          // Connor
          int bConnor = WR_GetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_CONNOR_FREED);
          GameObject oConnor = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_CONNOR);
          if (bConnor != EngineConstants.FALSE || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oConnor, EngineConstants.TRUE);

          // Cullen
          int bCullen = WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.TEMPLARS_IN_ARMY);
          GameObject oCullen = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_CULLEN);
          if (bCullen != EngineConstants.FALSE || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oCullen, EngineConstants.TRUE);

          // Dulin
          int bDulin = WR_GetPlotFlag(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN___PLOT_04_COMPLETED_KING_IS_HARROWMONT);
          GameObject oDulin = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_DULIN);
          if (bDulin != EngineConstants.FALSE || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oDulin, EngineConstants.TRUE);

          // Genitivi
          int bPartedWell = WR_GetPlotFlag(EngineConstants.PLT_URNPT_MAIN, EngineConstants.GENITIVI_RETURNS_TO_DENERIM);
          int bUrnTainted = WR_GetPlotFlag(EngineConstants.PLT_URN200PT_CULT, EngineConstants.URN_TAINTED);
          GameObject oGenitivi = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_GENITIVI);
          if ((bPartedWell != EngineConstants.FALSE && bUrnTainted == EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oGenitivi, EngineConstants.TRUE);

          // Greagoir
          int bGreagoir = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_CIRCLE);
          GameObject oGreagoir = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_GREAGOIR);
          if (bGreagoir != EngineConstants.FALSE)
               WR_SetObjectActive(oGreagoir, EngineConstants.TRUE);

          // Isolde
          int bIsolde = WR_GetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_CIRCLE_DOES_RITUAL);
          GameObject oIsolde = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_ISOLDE);
          if (bIsolde != EngineConstants.FALSE || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oIsolde, EngineConstants.TRUE);

          // Jowan
          int bDefendedJowan = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_ENTER_CASTLE, EngineConstants.ARL_ENTER_CASTLE_PC_BRINGS_JOWAN_TO_HALL);
          int bJowanDead = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_ENTER_CASTLE, EngineConstants.ARL_ENTER_CASTLE_JOWAN_DEAD);
          int bJowanKilled = WR_GetPlotFlag(EngineConstants.PLT_RANPT_GENERIC_ACTIONS, EngineConstants.RAN_JOWAN_DEAD);
          GameObject oJowan = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_JOWAN);
          if ((bDefendedJowan != EngineConstants.FALSE && bJowanDead == EngineConstants.FALSE && bJowanKilled == EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oJowan, EngineConstants.TRUE);

          // Kalah
          int bDwarfCommoner = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_COMMONER);
          GameObject oKalah = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_KALAH);
          if (bDwarfCommoner != EngineConstants.FALSE)
               WR_SetObjectActive(oKalah, EngineConstants.TRUE);

          // Kardol
          GameObject oKardol = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_KARDOL);

          WR_SetObjectActive(oKardol, EngineConstants.TRUE);

          // Keeper
          GameObject oKeeper = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_KEEPER);
          int bElfDalish = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_ELF_DALISH);
          if (bElfDalish != EngineConstants.FALSE)
               WR_SetObjectActive(oKeeper, EngineConstants.TRUE);

          // LadyOfForest
          int bLadyOfForest = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_WEREWOLVES_PROMISED_ALLIANCE);
          GameObject oLadyOfForest = UT_GetNearestCreatureByTag(oPC, "ntb340cr_lady");
          if ((bLadyOfForest != EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oLadyOfForest, EngineConstants.TRUE);

          // Lanaya
          int bElfAlliance = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ELVES_PROMISED_ALLIANCE);
          int bLanayaAngry = WR_GetPlotFlag(EngineConstants.PLT_NTB100PT_LANAYA, EngineConstants.NTB_LANAYA_ANGRY_AT_PC);
          int bLanayaAngrier = WR_GetPlotFlag(EngineConstants.PLT_NTB100PT_LANAYA, EngineConstants.NTB_LANAYA_ANGRIER_AT_PC);
          GameObject oLanaya = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_LANAYA);
          if ((bElfAlliance != EngineConstants.FALSE && bLanayaAngry == EngineConstants.FALSE && bLanayaAngrier == EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oLanaya, EngineConstants.TRUE);

          // Lily
          GameObject oLily = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_LILY);
          if ((bDefendedJowan != EngineConstants.FALSE && bJowanDead == EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oLily, EngineConstants.TRUE);

          // Mardy
          int bSleptMardy = WR_GetPlotFlag(EngineConstants.PLT_BDN100PT_NOBLE_HUNTERS, EngineConstants.BDN_NOBLE_HUNTERS_CHOSE_SEX_WITH_MARDY);
          int bSleptBoth = WR_GetPlotFlag(EngineConstants.PLT_BDN100PT_NOBLE_HUNTERS, EngineConstants.BDN_NOBLE_HUNTERS_CHOSE_SEX_WITH_BOTH);
          GameObject oMardy = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_MARDY);
          if ((bSleptMardy != EngineConstants.FALSE || bSleptBoth != EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oMardy, EngineConstants.TRUE);

          // SerCauthrien
          int bCauthrienKilled = WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_CAUTHRIEN_KILLED);
          GameObject oSerCauthrien = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_SERCAUTH);
          if ((bCauthrienKilled != EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oSerCauthrien, EngineConstants.TRUE);

          // Sighard
          int bSighard = WR_GetPlotFlag(EngineConstants.PLT_DENPT_RESCUE_THE_QUEEN, EngineConstants.DEN_RESCUE_OSWYN_SAVED);
          GameObject oSighard = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_SIGHARD);
          if ((bSighard != EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oSighard, EngineConstants.TRUE);

          // Soris
          int bSoris = WR_GetPlotFlag(EngineConstants.PLT_DENPT_RESCUE_THE_QUEEN, EngineConstants.DEN_RESCUE_FREED_SORRIS);
          GameObject oSoris = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_SORIS);
          if ((bSoris != EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oSoris, EngineConstants.TRUE);

          // Vartag
          int bVartag = WR_GetPlotFlag(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN___PLOT_04_COMPLETED_KING_IS_BHELEN);
          GameObject oVartag = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_VARTAG);
          if ((bVartag != EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oVartag, EngineConstants.TRUE);

          // Zathrian
          int bZathrianDead = WR_GetPlotFlag(EngineConstants.PLT_NTB000PT_MAIN, EngineConstants.NTB_MAIN_ZATHRIAN_SACRIFICES_HIMSELF);
          GameObject oZathrian = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_ZATHRIAN);
          if ((bZathrianDead != EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oZathrian, EngineConstants.TRUE);

          // Zelinda
          int bZelinda = WR_GetPlotFlag(EngineConstants.PLT_ORZ400PT_ZERLINDA, EngineConstants.ORZ_ZERLINDA___PLOT_03_COMPLETED_GONE_TO_SURFACE);
          GameObject oZelinda = UT_GetNearestCreatureByTag(oPC, EngineConstants.EPI_CR_ZERLINDA);
          if ((bZelinda != EngineConstants.FALSE) || bAllNPCs != EngineConstants.FALSE)
               WR_SetObjectActive(oZelinda, EngineConstants.TRUE);

     }

     public void EPI_RemoveParty()
     {
          GameObject oPC = GetHero();
          int i;                              // Counter
          List<GameObject> oMember = GetPartyPoolList();

          GameObject oAlistair = GetObjectByTag(EngineConstants.GEN_FL_ALISTAIR);
          GameObject oDog = GetObjectByTag(EngineConstants.GEN_FL_DOG);
          GameObject oLeliana = GetObjectByTag(EngineConstants.GEN_FL_LELIANA);
          GameObject oLoghain = GetObjectByTag(EngineConstants.GEN_FL_LOGHAIN);
          GameObject oMorrigan = GetObjectByTag(EngineConstants.GEN_FL_MORRIGAN);
          GameObject oOghren = GetObjectByTag(EngineConstants.GEN_FL_OGHREN);
          GameObject oShale = GetObjectByTag(EngineConstants.GEN_FL_SHALE);
          GameObject oSten = GetObjectByTag(EngineConstants.GEN_FL_STEN);
          GameObject oWynne = GetObjectByTag(EngineConstants.GEN_FL_WYNNE);
          GameObject oZevran = GetObjectByTag(EngineConstants.GEN_FL_ZEVRAN);

          // Set the PC as the leader
          SetPartyLeader(oPC);

          // Deflag all the party members.
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ALISTAIR_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LOGHAIN_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_MORRIGAN_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_OGHREN_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_SHALE_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_IN_PARTY, EngineConstants.FALSE);
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_IN_PARTY, EngineConstants.FALSE);


          if (IsObjectValid(oAlistair) != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "boom", "Alistair GameObject valid, state: " + IntToString(GetFollowerState(oAlistair)));

               if (GetFollowerState(oAlistair) != EngineConstants.FOLLOWER_STATE_INVALID)
                    WR_SetFollowerState(oAlistair, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          }
          else
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "boom", "Alistair GameObject EngineConstants.INVALID");

          if (IsObjectValid(oDog) != EngineConstants.FALSE)
               WR_SetFollowerState(oDog, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          if (IsObjectValid(oLeliana) != EngineConstants.FALSE)
               WR_SetFollowerState(oLeliana, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          if (IsObjectValid(oLoghain) != EngineConstants.FALSE)
               WR_SetFollowerState(oLoghain, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          if (IsObjectValid(oMorrigan) != EngineConstants.FALSE)
               WR_SetFollowerState(oMorrigan, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          if (IsObjectValid(oOghren) != EngineConstants.FALSE)
               WR_SetFollowerState(oOghren, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          if (IsObjectValid(oShale) != EngineConstants.FALSE)
               WR_SetFollowerState(oShale, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          if (IsObjectValid(oSten) != EngineConstants.FALSE)
               WR_SetFollowerState(oSten, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          if (IsObjectValid(oWynne) != EngineConstants.FALSE)
               WR_SetFollowerState(oWynne, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
          if (IsObjectValid(oZevran) != EngineConstants.FALSE)
               WR_SetFollowerState(oZevran, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);

          // Remove all party members
          for (i = 0; i < 6; i++)
          {
               //if (!IsObjectValid(oMember[i]))
               //    break;
               // Fire member if not the leader

               Effects_RemoveEffectByType(oMember[i], EngineConstants.EFFECT_TYPE_UPKEEP);

               if ((IsFollower(oMember[i]) != EngineConstants.FALSE) && (oMember[i] != GetPartyLeader()))
               {

                    //SetFollowerState(oMember[i], EngineConstants.FOLLOWER_STATE_INVALID);
                    // Set X member in party to not in party
                    //DestroyObject(oMember[i]);
               }

          }
     }

     public void EPI_EquipAlistair()
     {
          GameObject oItem;
          GameObject oAlistair = GetObjectByTag(EngineConstants.EPI_CR_ALISTAIR);

          GameObject oChest = CreateItemOnObject(EngineConstants.EPI_CLOTH_ALISTAIR_CHEST, oAlistair, 1, "", EngineConstants.TRUE);
          GameObject oGloves = CreateItemOnObject(EngineConstants.EPI_CLOTH_ALISTAIR_GLOVES, oAlistair, 1, "", EngineConstants.TRUE);
          GameObject oBoots = CreateItemOnObject(EngineConstants.EPI_CLOTH_ALISTAIR_BOOTS, oAlistair, 1, "", EngineConstants.TRUE);

          //StoreFollowerInventory

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_HEAD, oAlistair);
          UnequipItem(oAlistair, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_CHEST, oAlistair);
          UnequipItem(oAlistair, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_GLOVES, oAlistair);
          UnequipItem(oAlistair, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_BOOTS, oAlistair);
          UnequipItem(oAlistair, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oAlistair);
          UnequipItem(oAlistair, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, oAlistair);
          UnequipItem(oAlistair, oItem);

          EquipItem(oAlistair, oChest, EngineConstants.INVENTORY_SLOT_CHEST);
          EquipItem(oAlistair, oGloves, EngineConstants.INVENTORY_SLOT_GLOVES);
          EquipItem(oAlistair, oBoots, EngineConstants.INVENTORY_SLOT_BOOTS);

     }

     public void EPI_EquipLeliana()
     {
          GameObject oItem;
          GameObject oLeliana = GetObjectByTag(EngineConstants.EPI_CR_LELIANA);

          GameObject oChest = CreateItemOnObject(EngineConstants.EPI_LELIANA_LEATHER_CHEST, oLeliana, 1, "", EngineConstants.TRUE);
          GameObject oGloves = CreateItemOnObject(EngineConstants.EPI_LELIANA_LEATHER_GLOVES, oLeliana, 1, "", EngineConstants.TRUE);
          GameObject oBoots = CreateItemOnObject(EngineConstants.EPI_LELIANA_LEATHER_BOOTS, oLeliana, 1, "", EngineConstants.TRUE);

          //StoreFollowerInventory

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_HEAD, oLeliana);
          UnequipItem(oLeliana, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_CHEST, oLeliana);
          UnequipItem(oLeliana, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_GLOVES, oLeliana);
          UnequipItem(oLeliana, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_BOOTS, oLeliana);
          UnequipItem(oLeliana, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oLeliana);
          UnequipItem(oLeliana, oItem);

          oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, oLeliana);
          UnequipItem(oLeliana, oItem);

          EquipItem(oLeliana, oChest, EngineConstants.INVENTORY_SLOT_CHEST);
          EquipItem(oLeliana, oGloves, EngineConstants.INVENTORY_SLOT_GLOVES);
          EquipItem(oLeliana, oBoots, EngineConstants.INVENTORY_SLOT_BOOTS);

     }

     public void EPI_ZevranCrowd()
     {

          int bRomance = WR_GetPlotFlag(EngineConstants.PLT_GENPT_APP_ZEVRAN, EngineConstants.APP_ZEVRAN_ROMANCE_ACTIVE);
          int bPCFemale = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_CLASS_RACE_GEND, EngineConstants.GEN_GENDER_FEMALE);

          if (bRomance != EngineConstants.FALSE)
          {

               if (bPCFemale != EngineConstants.FALSE)
               {
                    // Deactivate default dudes.
                    UT_TeamAppears(EngineConstants.EPI_TEAM_ZEVRAN_DEFAULT, EngineConstants.FALSE);

                    // Team of women show up.
                    UT_TeamAppears(EngineConstants.EPI_TEAM_ZEVRAN_ROMANCE, EngineConstants.TRUE);
               }

               else
               {
                    // Deactivate default dudes.
                    UT_TeamAppears(EngineConstants.EPI_TEAM_ZEVRAN_DEFAULT, EngineConstants.FALSE);

                    // Team of men show up.
                    UT_TeamAppears(EngineConstants.EPI_TEAM_ZEVRAN_BROMANCE, EngineConstants.TRUE);
               }

          }

          else
          {
               // Deactivate default dudes.
               UT_TeamAppears(EngineConstants.EPI_TEAM_ZEVRAN_DEFAULT, EngineConstants.FALSE);

               // Shady peeps.
               UT_TeamAppears(EngineConstants.EPI_TEAM_ZEVRAN_SHADY, EngineConstants.TRUE);

          }

     }

     public void EPI_OghrenCrowd()
     {

          int bFelsi = WR_GetPlotFlag(EngineConstants.PLT_GENPT_OGHREN_MAIN, EngineConstants.OGHREN_MAIN_GOT_HIS_MOJO_BACK);

          if (bFelsi != EngineConstants.FALSE)
          {
               // Set Felsi active.
               UT_TeamAppears(EngineConstants.EPI_TEAM_FELSI, EngineConstants.TRUE);
          }

     }

     public void EPI_LelianaCrowd()
     {

          int bChanged = WR_GetPlotFlag(EngineConstants.PLT_GENPT_APP_LELIANA, EngineConstants.APP_LELIANA_CHANGED);

          if (bChanged != EngineConstants.FALSE)
          {

               // Change her outfit.
               EPI_EquipLeliana();

               // Some men show up.
               UT_TeamAppears(EngineConstants.EPI_TEAM_LELIANA_CHANGED, EngineConstants.TRUE);

               // Deactivate default dudes.
               UT_TeamAppears(EngineConstants.EPI_TEAM_LELIANA_DEFAULT, EngineConstants.FALSE);
          }

          else
          {

               // Team of chantry people.
               UT_TeamAppears(EngineConstants.EPI_TEAM_LELIANA_CHANTRY, EngineConstants.TRUE);

               // Deactivate default dudes.
               UT_TeamAppears(EngineConstants.EPI_TEAM_LELIANA_DEFAULT, EngineConstants.FALSE);

          }

     }

     public void EPI_WynneCrowd()
     {

          int bWynne = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_RECRUITED);

          if (bWynne != EngineConstants.FALSE)
          {
               // Deactivate default dudes.
               UT_TeamAppears(EngineConstants.EPI_TEAM_WYNNE_DEFAULT, EngineConstants.FALSE);

               // Scholars and mages.
               UT_TeamAppears(EngineConstants.EPI_TEAM_WYNNE_SCHOLARS, EngineConstants.TRUE);
          }

     }

     public void EPI_AlistairCrowd()
     {

          int bChanged = WR_GetPlotFlag(EngineConstants.PLT_GENPT_APP_ALISTAIR, EngineConstants.APP_ALISTAIR_CHANGED);

          if (bChanged != EngineConstants.FALSE)
          {

               // Nobles/Admirers show up.
               UT_TeamAppears(EngineConstants.EPI_TEAM_ALISTAIR_CHANGED, EngineConstants.TRUE);

          }

          else
          {

               // Lots of women.
               UT_TeamAppears(EngineConstants.EPI_TEAM_ALISTAIR_WOMEN, EngineConstants.TRUE);

          }

     }
}