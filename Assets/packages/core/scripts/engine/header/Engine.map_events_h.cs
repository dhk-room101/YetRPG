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
     //==============================================================================
     /*

          World Map
          -> Wide Open World Event Handler Function

     */
     //------------------------------------------------------------------------------
     // Created On: November 21, 2007
     //==============================================================================

     //#include"log_h"
     //#include"utility_h"
     //#include"wrappers_h"
     //#include"events_h"
     //#include"cir_functions_h"
     ////#include"orz_constants_h"
     //#include"world_maps_h"
     //#include"campaign_h"
     //#include"cutscenes_h"
     //#include"cli_constants_h"
     //#include"camp_constants_h"
     //#include"plt_denpt_map"
     //#include"arl_constants_h"

     //#include"den_functions_h"
     //#include"plt_cli400pt_city_gates"

     //#include"plt_gen00pt_stealing"
     //#include"plt_den200pt_pearls_swine"
     //#include"plt_den200pt_ser_landry"
     //#include"plt_den200pt_thief_sneak4"
     //#include"plt_lite_rogue_new_ground"
     //#include"plt_lite_rogue_decisions"
     //#include"plt_denpt_main"
     //#include"plt_pre100pt_mabari"
     //#include"plt_orz510pt_legion"
     //#include"plt_mnp000pt_main_events"
     //#include"plt_arl100pt_siege_prep"
     //#include"plt_arl200pt_remove_demon"
     //#include"plt_arl100pt_enter_castle"
     //#include"plt_gen00pt_party"
     //#include"plt_pre100pt_generic"
     //#include"plt_genpt_wynne_events"
     //#include"plt_genpt_leliana_main"
     //#include"plt_genpt_app_leliana"
     //#include"plt_cod_cha_anora"
     //#include"plt_cod_cha_howe"
     //#include"plt_cod_cha_loghain"
     //#include"plt_cod_cha_teagan"
     //#include"plt_cod_cha_zevran"
     //#include"plt_mnp000pt_generic"

     public int WM_HandleCutscenesWOW()
     {
          int nRet = EngineConstants.FALSE;
          //--------------------------------------------------------------------------
          // Check if we need to play cutscenes.
          // NOTE: a cutscene can be followed by a plot or random encounter
          //--------------------------------------------------------------------------

          if (WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_PC_LEFT_REDCLIFFE_AFTER_TALKING_TO_TEAGAN) != EngineConstants.FALSE
             && WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.REDCLIFFE_DESTROYED) == EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.REDCLIFFE_DESTROYED, EngineConstants.TRUE);
               CS_LoadCutscene("arl100cs_sunset_alt.cut");
               nRet = EngineConstants.TRUE;
               WR_SetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_VILLAGE_ABANDONED, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          if (WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.LOGHAIN_EVENT_ONE) == EngineConstants.FALSE)
          {
               // Should trigger when entering/travelling to Lothering
               WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.LOGHAIN_EVENT_ONE, EngineConstants.TRUE);

               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: triggering Loghain xEvent I (cutscene) ");
               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_ANORA, EngineConstants.COD_CHA_ANORA_MAIN, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_LOGHAIN, EngineConstants.COD_CHA_LOGHAIN_SECOND_QUOTE, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_LOGHAIN, EngineConstants.COD_CHA_LOGHAIN_QUOTE, EngineConstants.FALSE);
               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_LOGHAIN, EngineConstants.COD_CHA_LOGHAIN_CIVIL_UNREST, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_TEAGAN, EngineConstants.COD_CHA_TEAGAN_MAIN, EngineConstants.TRUE);

               nRet = EngineConstants.TRUE;
               CS_LoadCutscene(EngineConstants.CUTSCENE_LOGHAIN_EVENT_ONE);
          }

          else if (WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.LOGHAIN_EVENT_TWO) == EngineConstants.FALSE &&
                   WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.PLAYER_FINISHED_FIRST_MAJOR_PLOT) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.LOGHAIN_EVENT_TWO, EngineConstants.TRUE);

               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: triggering Loghain xEvent II (cutscene)");
               if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_HUMAN_NOBLE) == EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_HOWE, EngineConstants.COD_CHA_HOWE_QUOTE_EVERYONE_ELSE, EngineConstants.TRUE);
                    WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_HOWE, EngineConstants.COD_CHA_HOWE_MAIN, EngineConstants.TRUE);
               }

               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_LOGHAIN, EngineConstants.COD_CHA_LOGHAIN_CIVIL_WAR, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_ZEVRAN, EngineConstants.COD_CHA_ZEVRAN_QUOTE_1, EngineConstants.TRUE);
               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_ZEVRAN, EngineConstants.COD_CHA_ZEVRAN_MAIN, EngineConstants.TRUE);

               nRet = EngineConstants.TRUE;
               CS_LoadCutscene(EngineConstants.CUTSCENE_LOGHAIN_EVENT_TWO);

          }

          else if (WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.LOGHAIN_EVENT_THREE) == EngineConstants.FALSE &&
                   WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.PLAYER_FINISHED_SECOND_MAJOR_PLOT) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.LOGHAIN_EVENT_THREE, EngineConstants.TRUE);

               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: triggering Loghain xEvent III (cutscene) ");

               nRet = EngineConstants.TRUE;
               CS_LoadCutscene(EngineConstants.CUTSCENE_LOGHAIN_EVENT_THREE);
          }

          // MOVED TO EngineConstants.AREA LOAD

          /*else if(!WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ARCHDEMON_EVENT_TWO) &&
                   WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.PLAYER_FINISHED_THIRD_MAJOR_PLOT))
          {
              WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ARCHDEMON_EVENT_TWO, EngineConstants.TRUE);

              Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: triggering Archdemon xEvent II (cutscene) ");

              nRet = EngineConstants.TRUE;
              CS_LoadCutscene(EngineConstants.CUTSCENE_ARCHDEMON_EVENT_TWO);
          }*/

          return nRet;
     }

     // Returns:
     // 1(true): area transition was done
     // 0(false): area transition was NOT done - clear to check random encounter
     // -1: area transition was NOT done and NOT clear to check random encounter (proceed to normal travel)
     public int WM_HandleEventsWOW(string sSourceArea, string sTargetArea, GameObject oPreviousLocation)
     {

          int bDoAreaTransition = EngineConstants.FALSE; // Set if UT_DoAreaTransition is called

          //signal that the prelude areas are no longer needed (largely for Morrigan)
          if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_GENERIC, EngineConstants.PRE_GENERIC_PARTY_LEFT_PRELUDE_AREAS) == EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_PRE100PT_GENERIC, EngineConstants.PRE_GENERIC_PARTY_LEFT_PRELUDE_AREAS, EngineConstants.TRUE);
          }

          if ((sSourceArea == EngineConstants.ARL_AR_REDCLIFFE_VILLAGE && sTargetArea == EngineConstants.ARL_AR_CASTLE_COURTYARD) ||
              sSourceArea == EngineConstants.ARL_AR_CASTLE_COURTYARD && sTargetArea == EngineConstants.ARL_AR_REDCLIFFE_VILLAGE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: Player travels between redcliffe castle and village - NOT checking for special events (cutscenes can still run)");
               return -1;
          }
          else if ((sSourceArea == EngineConstants.CIR_AR_LAKE_CALENHAD && sTargetArea == EngineConstants.CIR_AR_TOWER_FIRST_FLOOR) ||
              sSourceArea == EngineConstants.CIR_AR_TOWER_FIRST_FLOOR && sTargetArea == EngineConstants.CIR_AR_LAKE_CALENHAD)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: Player travels between docks and circle tower - NOT checking for special events (cutscenes can still run)");
               return -1;
          }

          //If the player leaves Redcliffe village before the battle, the village is destroyed.
          int bPCSpokeToTeagan = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_PC_BROUGHT_TO_TEAGAN, EngineConstants.TRUE);
          int bBattleStarted = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_SIEGE_BEGINS, EngineConstants.TRUE);
          int bPCLeft = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_PC_LEFT_REDCLIFFE_AFTER_TALKING_TO_TEAGAN, EngineConstants.TRUE);
          if ((bPCSpokeToTeagan != EngineConstants.FALSE) && (bBattleStarted == EngineConstants.FALSE) && (bPCLeft == EngineConstants.FALSE))
          {
               if ((sTargetArea != EngineConstants.WML_WOW_RED_CASTLE) && (sTargetArea != EngineConstants.WML_WOW_REDCLIFFE) && (sTargetArea != EngineConstants.WML_AREA_TAG_CAMP))
               {
                    WR_SetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_PC_LEFT_REDCLIFFE_AFTER_TALKING_TO_TEAGAN, EngineConstants.TRUE, EngineConstants.TRUE);

                    //Remove now useless plot items.
                    UT_RemoveItemFromInventory(EngineConstants.ARL_R_IT_STASH);
                    UT_RemoveItemFromInventory(EngineConstants.ARL_R_IT_BARREL_OF_LAMP_OIL);
                    UT_RemoveItemFromInventory(EngineConstants.ARL_R_IT_OWEN_STASH_KEY);
               }

          }

          //If the player leaves the Redcliffe area after the hall confrontation, set a flag.
          int bLeftRedcliffe = WR_GetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_PC_LEFT_REDCLIFFE);
          int bConfrontedConnor = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_ENTER_CASTLE, EngineConstants.ARL_ENTER_CASTLE_PC_LEARNS_THAT_CONNOR_IS_RESPONSIBLE);
          if ((bLeftRedcliffe == EngineConstants.FALSE) && (bConfrontedConnor != EngineConstants.FALSE))
          {
               if ((sTargetArea != EngineConstants.WML_WOW_RED_CASTLE) && (sTargetArea != EngineConstants.WML_WOW_REDCLIFFE) && (sTargetArea != EngineConstants.WML_AREA_TAG_CAMP))
               {
                    WR_SetPlotFlag(EngineConstants.PLT_ARL200PT_REMOVE_DEMON, EngineConstants.ARL_REMOVE_DEMON_PC_LEFT_REDCLIFFE, EngineConstants.TRUE, EngineConstants.TRUE);
               }
          }

          //--------------------------------------------------------------------------
          // Check for plot encounters
          // NOTE: if a plot encounter triggers then a random encounter can NOT
          //       trigger.
          //--------------------------------------------------------------------------

          if (WR_GetPlotFlag(EngineConstants.PLT_PRE100PT_MABARI, EngineConstants.PRE_MABARI_DOG_HEALED) != EngineConstants.FALSE &&
             WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_RECRUITED) == EngineConstants.FALSE &&
             WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.DOG_JOINS_PARTY) == EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.DOG_JOINS_PARTY, EngineConstants.TRUE);

               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: triggering dog encounter!");
               // Jump player to special dog encounter area:
               bDoAreaTransition = EngineConstants.TRUE;
               //UT_DoAreaTransition(EngineConstants.RAN_AR_DOG, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
               WorldMapStartTravelling(EngineConstants.RAN_AR_DOG, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT, oPreviousLocation);
          }

          //else if(!WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, LOTHERING_LC_OPEN) &&
          //         WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ARCHDEMON_EVENT_ONE))
          //{
          //    WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, LOTHERING_LC_OPEN, EngineConstants.TRUE);

          //    Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: triggering lothering-lc encounter");
          //    bDoAreaTransition = EngineConstants.TRUE;
          //    SetLocalInt(GetModule(), EngineConstants.DISABLE_WORLD_MAP_ENCOUNTER, EngineConstants.TRUE); // we don't want an encounter right after this one
          //    UT_DoAreaTransition(EngineConstants.RAN_AR_OPEN_LOTHERING_LC, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
          //}

          else if (WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ZEVRAN_ATTACK_ONE) == EngineConstants.FALSE &&
                   WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.LOGHAIN_EVENT_TWO) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ZEVRAN_ATTACK_ONE, EngineConstants.TRUE);

               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: triggering Zevran encounter I");
               bDoAreaTransition = EngineConstants.TRUE;
               SetLocalInt(GetModule(), EngineConstants.DISABLE_WORLD_MAP_ENCOUNTER, EngineConstants.TRUE); // we don't want an encounter right after this one
                                                                                                            //UT_DoAreaTransition(EngineConstants.RAN_AR_ZEVRAN_1, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
               WorldMapStartTravelling(EngineConstants.RAN_AR_ZEVRAN_1, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT, oPreviousLocation);
          }

          else if (WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ARCHDEMON_EVENT_THREE) == EngineConstants.FALSE &&
                   WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.PLAYER_FINISHED_FOURTH_MAJOR_PLOT) != EngineConstants.FALSE &&
                  sSourceArea != EngineConstants.CAM_AR_ARCH3 && sSourceArea != EngineConstants.CAM_AR_CAMP_PLAINS)
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ARCHDEMON_EVENT_THREE, EngineConstants.TRUE);

               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "map_events_h", "WORLD MAP: triggering Archdemon xEvent III (encounter in camp) ");

               bDoAreaTransition = EngineConstants.TRUE;
               SetLocalInt(GetModule(), EngineConstants.DISABLE_WORLD_MAP_ENCOUNTER, EngineConstants.TRUE); // we don't want an encounter right after this one
               UT_DoAreaTransition(EngineConstants.CAM_AR_ARCH3, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
               //WorldMapStartTravelling(CAM_AR_ARCH3, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_GENPT_WYNNE_EVENTS, EngineConstants.WYNNE_ENC_ELIGIBLE_COLLAPSE) != EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_GENPT_WYNNE_EVENTS, EngineConstants.WYNNE_ENC_COLLAPSE_TRIGGERED) == EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_IN_PARTY) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GENPT_WYNNE_EVENTS, EngineConstants.WYNNE_ENC_COLLAPSE_TRIGGERED, EngineConstants.TRUE);
               bDoAreaTransition = EngineConstants.TRUE;
               SetLocalInt(GetModule(), EngineConstants.DISABLE_WORLD_MAP_ENCOUNTER, EngineConstants.TRUE); // we don't want an encounter right after this one
                                                                                                            //UT_DoAreaTransition(EngineConstants.RAN_AR_WYNNE_1, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
               WorldMapStartTravelling(EngineConstants.RAN_AR_WYNNE_1, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT, oPreviousLocation);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_GENPT_WYNNE_EVENTS, EngineConstants.WYNNE_ENC_ELIGIBLE_FIRST_SUMMON) != EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_GENPT_WYNNE_EVENTS, EngineConstants.WYNNE_ENC_FIRST_SUMMON_TRIGGERED) == EngineConstants.FALSE &&
                  WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_IN_PARTY) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_GENPT_WYNNE_EVENTS, EngineConstants.WYNNE_ENC_FIRST_SUMMON_TRIGGERED, EngineConstants.TRUE);
               bDoAreaTransition = EngineConstants.TRUE;
               SetLocalInt(GetModule(), EngineConstants.DISABLE_WORLD_MAP_ENCOUNTER, EngineConstants.TRUE); // we don't want an encounter right after this one
                                                                                                            //UT_DoAreaTransition(EngineConstants.RAN_AR_WYNNE_2, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
               WorldMapStartTravelling(EngineConstants.RAN_AR_WYNNE_2, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT, oPreviousLocation);
          }
          else if (WR_GetPlotFlag(EngineConstants.PLT_GENPT_LELIANA_MAIN, EngineConstants.LELIANA_MAIN_ASSASSIN_ENC_START) != EngineConstants.FALSE)
          {
               bDoAreaTransition = EngineConstants.TRUE;
               Log_Trace_Scripting_Error(GetCurrentScriptName(), "Assasin Encounter triggered");
               SetLocalInt(GetModule(), EngineConstants.DISABLE_WORLD_MAP_ENCOUNTER, EngineConstants.TRUE); // we don't want an encounter right after this one
                                                                                                            //UT_DoAreaTransition(EngineConstants.RAN_AR_LELIANA, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
               WorldMapStartTravelling(EngineConstants.RAN_AR_LELIANA, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT, oPreviousLocation);
          }
          Log_Trace_Scripting_Error(GetCurrentScriptName(), "Bypassed Assasin Encounter");

          //  else if(!WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ARCHDEMON_EVENT_FOUR) &&
          //           WR_GetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.PLAYER_FINISHED_FIFTH_MAJOR_PLOT))
          //  {
          //      WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_MAIN_EVENTS, EngineConstants.ARCHDEMON_EVENT_FOUR, EngineConstants.TRUE);

          //      Log_Plot("WORLD MAP: triggering Archdemon xEvent IV (encounter) ", EngineConstants.LOG_LEVEL_WARNING);

          //      bDoAreaTransition = EngineConstants.TRUE;
          //       UT_DoAreaTransition(CAM_AR_ARCH4, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
          // }

          //--------------------------------------------------------------------------

          return bDoAreaTransition;

     }

     public int WM_HandleEventsUND(string sSourceArea, string sTargetArea)
     {

          int bDoAreaTransition = EngineConstants.FALSE; // Set if UT_DoAreaTransition is called

          //--------------------------------------------------------------------------
          // Check if we need to play cutscenes.
          // NOTE: a cutscene can be followed by a plot or random encounter
          //--------------------------------------------------------------------------

          //--------------------------------------------------------------------------
          // Check for plot encounters
          // NOTE: if a plot encounter triggers then a random encounter can NOT
          //       trigger.
          //--------------------------------------------------------------------------

          // Legion of the Dead Plot Complete Encounter
          //  ----> Legion of the dead plot has been cut.

          return bDoAreaTransition;

     }

     public int WM_HandleEventsCLI(string sTarget)
     {

          int bDoAreaTransition = EngineConstants.FALSE; // Set if UT_DoAreaTransition is called

          //--------------------------------------------------------------------------
          // Check if we need to play cutscenes.
          // NOTE: a cutscene can be followed by a plot or random encounter
          //--------------------------------------------------------------------------

          //--------------------------------------------------------------------------
          // Check for plot encounters
          // NOTE: if a plot encounter triggers then a random encounter can NOT
          //       trigger.
          //--------------------------------------------------------------------------

          // do not trigger the encounter if the player did not leave anyone beyond to defend
          GameObject oLeader = GetLocalObject(GetModule(), EngineConstants.PARTY_LEADER_STORE);

          if (sTarget == EngineConstants.CLI_PALACE_DISTRICT && WR_GetPlotFlag(EngineConstants.PLT_CLI400PT_CITY_GATES, EngineConstants.CLI_CITY_GATES_ATTACK_START, EngineConstants.TRUE) == EngineConstants.FALSE
         && WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MAIN, EngineConstants.CLI_MAIN_NO_GATE_DEFENSE) == EngineConstants.FALSE && IsObjectValid(oLeader) != EngineConstants.FALSE && oLeader != GetHero())
          {
               bDoAreaTransition = EngineConstants.TRUE;
               SetLoadHint(3, 206);
               SetLocalInt(GetModule(), EngineConstants.AREA_LOAD_HINT, 0);
               //UT_DoAreaTransition(CLI_CITY_GATES_DEFENSE, EngineConstants.CLI_WP_CITY_GATES_DEFENCE_START);
               WorldMapStartTravelling(EngineConstants.CLI_CITY_GATES_DEFENSE, EngineConstants.CLI_WP_CITY_GATES_DEFENCE_START);
          }

          return bDoAreaTransition;

     }

     public int WM_HandleEventsDEN(string sSourceArea, string sTargetArea)
     {

          int bDoAreaTransition = EngineConstants.FALSE; // Set if UT_DoAreaTransition is called

          //--------------------------------------------------------------------------
          // Check if we need to play cutscenes.
          // NOTE: a cutscene can be followed by a plot or random encounter
          //--------------------------------------------------------------------------

          //--------------------------------------------------------------------------
          // Check for plot encounters
          // NOTE: if a plot encounter triggers then a random encounter can NOT
          //       trigger.
          //--------------------------------------------------------------------------

          // These are for the "Pearls Before Swine" subquest
          int bFalconsKilled = WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_PEARLS_SWINE, EngineConstants.FALCONS_KILLED);
          int bFalconsSpared = WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_PEARLS_SWINE, EngineConstants.FALCONS_LEAVE);
          int bFalconsQuelled = WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_PEARLS_SWINE, EngineConstants.FALCONS_QUELLED);
          int bKylonEncountered = WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_PEARLS_SWINE, EngineConstants.KYLON_ENCOUNTERED_IN_ALLEY);

          if (sSourceArea == EngineConstants.DEN_AR_MARKET && sTargetArea == EngineConstants.DEN_AR_EAMON_ESTATE_1)
          {
               bDoAreaTransition = EngineConstants.TRUE;
               WorldMapStartTravelling();
          }
          else if (sSourceArea == EngineConstants.DEN_AR_MARKET
                 && WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_OPENING_DONE) != EngineConstants.FALSE
                 && WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_PLAYER_AMBUSHED_BY_CROWS) == EngineConstants.FALSE
                 && WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_RECRUITED) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_PLAYER_AMBUSHED_BY_CROWS, EngineConstants.TRUE, EngineConstants.TRUE);
               bDoAreaTransition = EngineConstants.TRUE;
               WorldMapStartTravelling(EngineConstants.DEN_AR_CROW_ENCOUNTER, EngineConstants.DEN_WP_CROW_ENCOUNTER_START);
          }

          /*  Disabled at Yaron's request
          if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_OPENING_DONE)
             && !WR_GetPlotFlag(EngineConstants.PLT_DENPT_RESCUE_THE_QUEEN, DEN_RESCUE_LEFT_MARKET))
          {
              WR_SetPlotFlag(EngineConstants.PLT_DENPT_RESCUE_THE_QUEEN, DEN_RESCUE_LEFT_MARKET, EngineConstants.TRUE, EngineConstants.FALSE);
          } */

          // This is the forced encounter in the "Pearls Before Swine" quest after fighting in the Pearl brothel.
          // If there's ever any DENERIM MARKET specific area overrides, this should be on top of those or the
          // quest breaks.
          else if ((bFalconsKilled != EngineConstants.FALSE || bFalconsSpared != EngineConstants.FALSE || bFalconsQuelled != EngineConstants.FALSE) && bKylonEncountered == EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_DEN200PT_PEARLS_SWINE, EngineConstants.KYLON_ENCOUNTERED_IN_ALLEY, EngineConstants.TRUE, EngineConstants.TRUE);

               bDoAreaTransition = EngineConstants.TRUE;
               //UT_DoAreaTransition(EngineConstants.DEN_AR_FALCON_ATTACK, EngineConstants.DEN_WP_FALCON_ATTACK);
               WorldMapStartTravelling(EngineConstants.DEN_AR_FALCON_ATTACK, EngineConstants.DEN_WP_FALCON_ATTACK);
          }
          else if (sTargetArea == EngineConstants.DEN_AR_MARKET
                 && WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_SER_LANDRY, EngineConstants.LANDRY_AMBUSHED_PC) == EngineConstants.FALSE
                 && WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_SER_LANDRY, EngineConstants.LANDRY_DUEL_REFUSED) != EngineConstants.FALSE)
          {
               // If the PC refused Landry's duel, then he will ambush the PC the next time he goes to the Market

               {
                    WR_SetPlotFlag(EngineConstants.PLT_DEN200PT_SER_LANDRY, EngineConstants.LANDRY_AMBUSHED_PC, EngineConstants.TRUE, EngineConstants.TRUE);

                    bDoAreaTransition = EngineConstants.TRUE;
                    //UT_DoAreaTransition(EngineConstants.DEN_AR_LANDRY_ATTACK, EngineConstants.DEN_WP_LANDRY_ATTACK);
                    WorldMapStartTravelling(EngineConstants.DEN_AR_LANDRY_ATTACK, EngineConstants.DEN_WP_LANDRY_ATTACK);
               }
          }
          /*else if (sTargetArea == EngineConstants.DEN_AR_FRANDEREL_ESTATE
             && WR_GetPlotFlag(EngineConstants.PLT_DEN200PT_THIEF_SNEAK4, THIEF_SNEAK4_ASSIGNED))
          {
              bDoAreaTransition = EngineConstants.TRUE;
              //UT_DoAreaTransition(EngineConstants.DEN_AR_FRANDEREL_ESTATE_2, EngineConstants.DEN_WP_FRANDEREL_ESTATE_2);
              WorldMapStartTravelling(EngineConstants.DEN_AR_FRANDEREL_ESTATE_2, EngineConstants.DEN_WP_FRANDEREL_ESTATE_2);
          }*/
          // Rogue Light Content: New Ground
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_ROGUE_NEW_GROUND, EngineConstants.NEW_GROUND_RANDOM_ENCOUNTER_ACTIVE) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_LITE_ROGUE_NEW_GROUND, EngineConstants.NEW_GROUND_RANDOM_ENCOUNTER, EngineConstants.TRUE);
               WorldMapStartTravelling(EngineConstants.DEN_AR_LITE_ROGUE_AMBUSH, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
               bDoAreaTransition = EngineConstants.TRUE;
          }
          // Rogue Light Content: Decisions
          else if (WR_GetPlotFlag(EngineConstants.PLT_LITE_ROGUE_DECISIONS, EngineConstants.DECISIONS_RANDOM_ENCOUNTER_ACTIVE) != EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_LITE_ROGUE_DECISIONS, EngineConstants.DECISIONS_RANDOM_ENCOUNTER, EngineConstants.TRUE);
               WorldMapStartTravelling(EngineConstants.DEN_AR_LITE_ROGUE_AMBUSH, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT);
               bDoAreaTransition = EngineConstants.TRUE;
          }

          return bDoAreaTransition;

     }

     /*

         redirects player to correct follower location

     */

     public int WM_HandleEventsFADE()
     {

          string sArea = GetLocalString(GetModule(), EngineConstants.WM_STORED_AREA);
          string sWP = GetLocalString(GetModule(), EngineConstants.WM_STORED_WP);
          GameObject oMap = GetObjectByTag(EngineConstants.WM_FAD_TAG);
          GameObject oWMLoc;

          if (sArea == "Fade_Follower")
          {
               int nFollower = 0;
               switch (StringToInt(sWP))
               {
                    case 1:
                         {
                              nFollower = GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_1);
                              oWMLoc = GetObjectByTag("wml_fad_comp_a");
                              SetWorldMapPlayerLocation(oMap, oWMLoc);
                              break;
                         }
                    case 2:
                         {
                              nFollower = GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_2);
                              oWMLoc = GetObjectByTag("wml_fad_comp_b");
                              SetWorldMapPlayerLocation(oMap, oWMLoc);
                              break;
                         }
                    case 3:
                         {
                              nFollower = GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_3);
                              oWMLoc = GetObjectByTag("wml_fad_comp_c");
                              SetWorldMapPlayerLocation(oMap, oWMLoc);
                              break;
                         }
               }

               CIR_JumpToFadeFollower(nFollower);
          }
          else
               UT_PCJumpOrAreaTransition(sArea, sWP);

          return EngineConstants.TRUE;

     }
}