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
     /* lit_functions_h
     *
     *   Contains light content functions
     *
     *
     *

     * Created by: Keith Warner
     * Created on: March 5/09
     **/

     //#include"plot_h"
     //#include"lit_constants_h"
     //#include"plt_lite_fite_condolences"
     //#include"plt_lite_fite_conscripts"
     //#include"plt_lite_fite_deserters"
     //#include"plt_lite_fite_grease"
     //#include"plt_lite_fite_leadership"
     //#include"plt_lite_fite_lostorders"
     //#include"plt_lite_fite_quality"
     //#include"plt_lite_fite_restock"
     ////#include"plt_lite_fite_ropes"

     //#include"plt_lite_mage_collective"
     //#include"plt_lite_mage_banastor"
     //#include"plt_lite_mage_herbal"
     //#include"plt_lite_mage_termination"
     //#include"plt_lite_mage_places"
     //#include"plt_lite_mage_killer"
     //#include"plt_lite_mage_silence"
     //#include"plt_lite_mage_witnesses"
     //#include"plt_lite_mage_renold"
     //#include"plt_lite_mage_defending"
     //#include"plt_lite_mage_warning"

     //#include"plt_lite_chant_rand_civil"
     //#include"plt_lite_chant_rand_feed"
     //#include"plt_lite_chant_rand_jowan"
     //#include"plt_lite_chant_rand_refugee"
     //#include"plt_lite_chant_rand_remains"
     //#include"plt_lite_chant_red_zombie"
     //#include"plt_lite_chant_tow_trick"
     //#include"plt_den200pt_alley_justice"
     //#include"plt_den200pt_fazzil_request"
     //#include"plt_den200pt_mia"

     public int BlackstoneTurnInPossible()
     {
          int nResult = EngineConstants.FALSE;
          //Condolences done but not turned in
          if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_CONDOLENCES, EngineConstants.CONDOLENCES_DELIVERED) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_CONDOLENCES, EngineConstants.CONDOLENCES_QUEST_COMPLETE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Conscripts done but not turned in
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_CONSCRIPTS, EngineConstants.CONSCRIPTS_MET) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_CONSCRIPTS, EngineConstants.CONSCRIPTS_QUEST_COMPLETE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Deserters done but not turned in
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_DESERTERS, EngineConstants.DESERTERS_FOUND) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_DESERTERS, EngineConstants.DESERTERS_QUEST_COMPLETE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Grease done but not turned in
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_GREASE, EngineConstants.GREASE_NOTICES_DELIVERED) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_GREASE, EngineConstants.GREASE_QUEST_COMPLETE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Leadership (Raelnor killing) done but not turned in
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_LEADERSHIP, EngineConstants.LEADERSHIP_RAELNOR_DEAD) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_LEADERSHIP, EngineConstants.LEADERSHIP_COMPLETE_RAELNOR_DEAD) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Leadership (Taoran killing) done but not turned in
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_LEADERSHIP, EngineConstants.LEADERSHIP_TAORAN_KILLED) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_LEADERSHIP, EngineConstants.LEADERSHIP_COMPLETE_TAORAN_DEAD) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Lost Orders done but not turned in
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_LOSTORDERS, EngineConstants.LOSTORDERS_FOUND) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_LOSTORDERS, EngineConstants.LOSTORDERS_QUEST_COMPLETE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Quality done but not turned in
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_QUALITY, EngineConstants.QUALITY_OPPONENTS_BEATEN) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_QUALITY, EngineConstants.QUALITY_QUEST_COMPLETE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Restock done but not turned in
          if (nResult == EngineConstants.FALSE)
          {
               if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_RESTOCK, EngineConstants.RESTOCK_QUEST_COMPLETE) == EngineConstants.FALSE
                    && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_RESTOCK, EngineConstants.RESTOCK_QUEST_GIVEN) != EngineConstants.FALSE)
               {
                    int nPoulticeCount = 0;
                    nPoulticeCount = UT_CountItemInInventory(EngineConstants.rLITE_IM_HEALTH_POUL_LESSER);
                    nPoulticeCount = nPoulticeCount + UT_CountItemInInventory(EngineConstants.rLITE_IM_HEALTH_POUL);
                    nPoulticeCount = nPoulticeCount + UT_CountItemInInventory(EngineConstants.rLITE_IM_HEALTH_POUL_GREATER);
                    nPoulticeCount = nPoulticeCount + UT_CountItemInInventory(EngineConstants.rLITE_IM_HEALTH_POUL_POTENT);

                    if (nPoulticeCount >= 20)
                    {
                         nResult = EngineConstants.TRUE;
                    }
               }

               //Ropes done but not turned in  
               /*
               if (WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_ROPES, ROPES_QUEST_COMPLETE) == EngineConstants.FALSE && WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_ROPES, ROPES_QUEST_GIVEN) != EngineConstants.FALSE)
               {
                   //get how many ropes are in inventory
                   int nRopeTrap = UT_CountItemInInventory(EngineConstants.rLITE_IM_FITE_ROPE_TRAP);
                   if (nRopeTrap >= 10)
                   {
                       nResult = EngineConstants.TRUE;

                   }
               }
               */
          }

          return nResult;
     }

     public int MageCollectiveTurnInPossible(GameObject oPC)
     {
          int nResult = EngineConstants.FALSE;
          int nBanastorDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_BANASTOR, EngineConstants.BANASTOR_SCROLLS_FOUND);
          int nBanastorComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_BANASTOR, EngineConstants.BANASTOR_QUEST_COMPLETE);
          int nHerbalGiven = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_HERBAL, EngineConstants.HERBAL_QUEST_GIVEN);
          int nHerbalComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_HERBAL, EngineConstants.HERBAL_QUEST_COMPLETE);
          int nTerminationDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_TERMINATION, EngineConstants.TERMINATION_APPRENTICES_TERMINATED);
          int nTerminationComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_TERMINATION, EngineConstants.TERMINATION_QUEST_COMPLETE);
          int nPlacesDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_PLACES, EngineConstants.PLACES_FOUND);
          int nPlacesComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_PLACES, EngineConstants.PLACES_QUEST_COMPLETE);
          int nKillerDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_KILLER, EngineConstants.KILLER_MAGES_KILLED);
          int nKillerComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_KILLER, EngineConstants.KILLER_QUEST_COMPLETE);
          int nSilenceDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_SILENCE, EngineConstants.SILENCE_BRIBE_DELIVERED);
          int nSilenceComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_SILENCE, EngineConstants.SILENCE_QUEST_COMPLETE);
          int nWitnessessDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_WITNESSES, EngineConstants.WITNESSES_ADVENTURERS_STOPPED);
          int nWitnessessComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_WITNESSES, EngineConstants.WITNESSES_QUEST_COMPLETE);
          int nRenoldDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_RENOLD, EngineConstants.RENOLD_NOTE_FOUND);
          int nRenoldComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_RENOLD, EngineConstants.RENOLD_QUEST_COMPLETE);
          int nDefendingDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_DEFENDING, EngineConstants.DEFENDING_TESTIMONY_GIVEN);
          int nDefendingComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_DEFENDING, EngineConstants.DEFENDING_QUEST_COMPLETE);
          int nWarningDone = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_WARNING, EngineConstants.WARNING_DOORS_MARKED);
          int nWarningComplete = WR_GetPlotFlag(EngineConstants.PLT_LITE_MAGE_WARNING, EngineConstants.WARNING_QUEST_COMPLETE);

          //Mage Banastor
          if (nBanastorDone != EngineConstants.FALSE && nBanastorComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }

          //Mage Herbal
          if (nHerbalGiven != EngineConstants.FALSE && nHerbalComplete == EngineConstants.FALSE)
          {
               //get all of the stacks of health poultices
               List<GameObject> arrMushroom = GetItemsInInventory(oPC, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, EngineConstants.LITE_IM_DEEP_MUSHROOM);
               //if there is a valid stack
               if (IsObjectValid(arrMushroom[0]) != EngineConstants.FALSE)
               {
                    //if you have at least 20 in your stack, you can turn them in.
                    int nMushrooms = GetItemStackSize(arrMushroom[0]);
                    if (nMushrooms >= 10)
                    {
                         nResult = EngineConstants.TRUE;
                    }
               }
          }

          //Mage Termination
          if (nTerminationDone != EngineConstants.FALSE && nTerminationComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }

          //Mage Places
          if (nPlacesDone != EngineConstants.FALSE && nPlacesComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }

          //Mage Killer
          if (nKillerDone != EngineConstants.FALSE && nKillerComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }

          //Mage Silence
          if (nSilenceDone != EngineConstants.FALSE && nSilenceComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Mage Renold
          if (nRenoldDone != EngineConstants.FALSE && nRenoldComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Mage Witnesses
          if (nWitnessessDone != EngineConstants.FALSE && nWitnessessComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //Mage Defending
          if (nDefendingDone != EngineConstants.FALSE && nDefendingComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //mage Warning
          if (nWarningDone != EngineConstants.FALSE && nWarningComplete == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }

          return nResult;

     }

     public int ChanterTurnInPossible()
     {
          int nResult = EngineConstants.FALSE;
          // Alley Justice bad guys cleared AND the quest hasn't been turned in already
          if (WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_ALLEY_JUSTICE, EngineConstants.ALLEY_ALL_BAD_GUYS_KILLED) != EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_ALLEY_JUSTICE, EngineConstants.ALLEY_QUEST_DONE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }

          // Fazzil's sextant has been found AND the quest hasn't been turned in already
          if (WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_FAZZIL_REQUEST, EngineConstants.FAZZIL_SEXTANT_RECOVERED) != EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_FAZZIL_REQUEST, EngineConstants.FAZZIL_ACCEPTED) != EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_FAZZIL_REQUEST, EngineConstants.FAZZIL_QUEST_DONE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }

          // Rexel has been found AND the quest hasn't been turned in already
          if (WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_MIA, EngineConstants.MIA_REXEL_FOUND) != EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_MIA, EngineConstants.MIA_QUEST_DONE) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //chant rand civil
          if (WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_CIVIL, EngineConstants.CIVIL_PLOT_COMPLETED) != EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_CIVIL, EngineConstants.CIVIL_PLOT_CLOSED) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //chant rand feed
          if (WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_FEED, EngineConstants.FEED_PLOT_COMPLETED) != EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_FEED, EngineConstants.FEED_PLOT_CLOSED) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //chant rand jowan
          if ((WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_JOWAN, EngineConstants.JOWAN_PLOT_COMPLETED) != EngineConstants.FALSE || WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_JOWAN, EngineConstants.JOWAN_PLOT_COMPLETED_DEAD) != EngineConstants.FALSE) &&
                WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_JOWAN, EngineConstants.JOWAN_PLOT_CLOSED) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //chant rand refugee
          if ((WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_REFUGEE, EngineConstants.REFUGEE_PLOT_COMPLETED) != EngineConstants.FALSE || WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_REFUGEE, EngineConstants.REFUGEE_PLOT_COMPLETED_DEAD) != EngineConstants.FALSE) &&
                WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_REFUGEE, EngineConstants.REFUGEE_PLOT_CLOSED) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //chant rand remains
          if (WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_REMAINS, EngineConstants.REMAINS_PLOT_COMPLETED) != EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RAND_REMAINS, EngineConstants.REMAINS_PLOT_CLOSED) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //chant red zombie - can be closed with either 9 or 18
          if (WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RED_ZOMBIE, EngineConstants.RED_ZOMBIE_COMPLETE_WITH_18) != EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RED_ZOMBIE, EngineConstants.RED_ZOMBIE_CLOSED_WITH_18) == EngineConstants.FALSE &&
               WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RED_ZOMBIE, EngineConstants.RED_ZOMBIE_CLOSED_WITH_9) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RED_ZOMBIE, EngineConstants.RED_ZOMBIE_COMPLETE_WITH_9) != EngineConstants.FALSE &&
                    WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RED_ZOMBIE, EngineConstants.RED_ZOMBIE_CLOSED_WITH_18) == EngineConstants.FALSE &&
                    WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_RED_ZOMBIE, EngineConstants.RED_ZOMBIE_CLOSED_WITH_9) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          //chant tow trickster whim
          if (WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_TOW_TRICK, EngineConstants.TOW_TRICKSTER_COMPLETE) != EngineConstants.FALSE &&
              WR_GetPlotFlag(EngineConstants.PLT_LITE_CHANT_TOW_TRICK, EngineConstants.TOW_TRICKSTER_CLOSED) == EngineConstants.FALSE)
          {
               nResult = EngineConstants.TRUE;
          }
          return nResult;
     }
}