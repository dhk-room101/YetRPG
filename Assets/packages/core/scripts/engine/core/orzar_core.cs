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

public class orzar_core : MonoBehaviour
{
    //==============================================================================
    /*

        Paragon of Her Kind
         -> Paragon Generic Area Script

        All area scripts should filter through here before area_core

    */
    //------------------------------------------------------------------------------
    // Created By: Joshua Stiksma
    // Created On: October 2, 2007
    //==============================================================================

    //#include"plt_orzpt_carta"
    //#include"plt_orzpt_main"
    //#include"plt_orzpt_events"
    //#include"plt_orzpt_anvil"
    //#include"plt_orz100pt_imrek"
    //#include"plt_orz200pt_figale"
    //#include"plt_orz200pt_filda"
    //#include"plt_orz340pt_find_lord_dace"
    //#include"plt_orz340pt_assembly"
    //#include"plt_orz400pt_rogek"
    //#include"plt_orz510pt_legion"
    //#include"plt_orz530pt_ruck"
    //#include"plt_orz540pt_anvil_ot_public void"
    //#include"plt_orz550pt_dead_trenches"
    //#include"plt_orz550pt_kardol"
    //#include"plt_gen00pt_stealing"
    //#include"plt_orz260pt_lite_proving"
    //#include"plt_orz550pt_hespith"

    //#include"plt_den200pt_assassin_orz"

    //#include"orz_constants_h"
    //#include"orz_functions_h"
    //#include"orz540_apparatus_h"

    //#include"wrappers_h"
    //#include"utility_h"
    //#include"plot_h"

    Engine engine { get; set; }

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

    public void HandleEvent(xEvent ev)
    {

        //--------------------------------------------------------------------------
        // Initialization
        //--------------------------------------------------------------------------

        // Load engine.Event Variables
        //xEvent ev = engine.GetCurrentEvent();            // engine.Event
        int nEventType = engine.GetEventTypeRef(ref ev);        // engine.Event Type
        GameObject oEventCreator = engine.GetEventCreatorRef(ref ev);     // engine.Event Creator

        // Standard Stuff
        GameObject oPC = engine.GetHero();
        int bEventHandled = EngineConstants.FALSE;

        //--------------------------------------------------------------------------
        // Area engine.Events
        //--------------------------------------------------------------------------

        switch (nEventType)
        {

            case EngineConstants.EVENT_TYPE_AREALOAD_SPECIAL:
                {

                    //------------------------------------------------------------------
                    // EngineConstants.EVENT_TYPE_AREALOAD_SPECIAL:
                    // Sent by: The engine
                    // When: it is for playing things like cutscenes and movies when
                    // you enter an area, things that do not involve AI or actual
                    // game play.
                    //------------------------------------------------------------------

                    break;

                }

            case EngineConstants.EVENT_TYPE_STEALING_SUCCESS:
                {

                    //------------------------------------------------------------------
                    // EngineConstants.EVENT_TYPE_STEALING_SUCCESS:
                    // Sent by: Skill Script (skill_stealing)
                    // When: player succeeds stealing skill
                    //------------------------------------------------------------------

                    engine.LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, "orzar_core::EngineConstants.EVENT_TYPE_STEALING_SUCCESS", gameObject);

                    //WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_STEALING,STEALING_ORZ_COUNTER_INCREASE,EngineConstants.TRUE,EngineConstants.TRUE);

                    break;

                }

            case EngineConstants.EVENT_TYPE_STEALING_FAILURE:
                {

                    //------------------------------------------------------------------
                    // EngineConstants.EVENT_TYPE_STEALING_FAILURE:
                    // Sent by: Skill Script (skill_stealing)
                    // When: player fails stealing skill
                    //------------------------------------------------------------------

                    engine.LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, "orzar_core::EngineConstants.EVENT_TYPE_STEALING_FAILURE", gameObject);

                    engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_STEALING, EngineConstants.STEALING_ORZ_COUNTER_INCREASE, EngineConstants.TRUE, EngineConstants.TRUE);

                    // Trigger Guard Bark
                    GameObject oNearestGuard = engine.UT_GetNearestObjectByTag(oPC, "orz300cr_guard_1");
                    if (engine.IsObjectValid(oNearestGuard) == EngineConstants.FALSE)
                        oNearestGuard = engine.UT_GetNearestObjectByTag(oPC, "orz200cr_guard_1");
                    if (engine.IsObjectValid(oNearestGuard) != EngineConstants.FALSE)
                    {
                        engine.WR_ClearAllCommands(oNearestGuard);
                        engine.WR_AddCommand(oNearestGuard, engine.CommandJumpToObject(oPC), EngineConstants.TRUE);
                        engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_STEALING, EngineConstants.STEALING_ORZ_PC_CAUGHT_BY_GUARDS_STEALING, EngineConstants.TRUE);
                        engine.UT_Talk(oNearestGuard, oNearestGuard);
                    }

                    break;

                }

            case EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT:
                {

                    //------------------------------------------------------------------
                    // EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT:
                    // Sent by: The engine
                    // When: for things you want to happen while the load screen is
                    // still up, things like moving creatures around.
                    //------------------------------------------------------------------

                    int bBhelenKing;
                    int bHarrowKing;
                    int bParagonDone;

                    //------------------------------------------------------------------

                    bBhelenKing = engine.WR_GetPlotFlag(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN___PLOT_04_BHELEN_CROWNED);
                    bHarrowKing = engine.WR_GetPlotFlag(EngineConstants.PLT_ORZPT_MAIN, EngineConstants.ORZ_MAIN___PLOT_04_COMPLETED_KING_IS_HARROWMONT);

                    //------------------------------------------------------------------

                    engine.UT_TeamAppears(EngineConstants.ORZ_TEAM_POST_PLOT_BHELEN_KING, bBhelenKing);
                    engine.UT_TeamAppears(EngineConstants.ORZ_TEAM_POST_PLOT_HARROW_KING, bHarrowKing);
                    engine.UT_TeamAppears(EngineConstants.ORZ_TEAM_POST_PLOT_DONE, (bBhelenKing != EngineConstants.FALSE || bHarrowKing != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE);
                    engine.UT_TeamAppears(EngineConstants.ORZ_TEAM_POST_PLOT_NOT_DONE, (bBhelenKing == EngineConstants.FALSE || bHarrowKing == EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE);

                    // TEMP
                    engine.ORZ_SetupGroupHostility();
                    engine.ORZ_SetupBackgroundKA();
                    //SetAtmosphericConditions(5);
                    //SetCloudConditions(0);
                    // END TEMP

                    break;

                }

            case EngineConstants.EVENT_TYPE_AREALOAD_POSTLOADEXIT:
                {

                    //------------------------------------------------------------------
                    // EngineConstants.EVENT_TYPE_AREALOAD_POSTLOADEXIT:
                    // Sent by: The engine
                    // When: fires at the same time that the load screen is going away,
                    // and can be used for things that you want to make sure the player
                    // sees.
                    //------------------------------------------------------------------

                    break;

                }

            case EngineConstants.EVENT_TYPE_ENTER:
                {

                    //------------------------------------------------------------------
                    // EngineConstants.EVENT_TYPE_ENTER:
                    // Sent by: The engine
                    // When: A creature enters the area.
                    //------------------------------------------------------------------

                    break;

                }

            case EngineConstants.EVENT_TYPE_EXIT:
                {

                    //------------------------------------------------------------------
                    // EngineConstants.EVENT_TYPE_EXIT:
                    // Sent by: The engine
                    // When: A creature exits the area.
                    //------------------------------------------------------------------

                    break;

                }

            case EngineConstants.EVENT_TYPE_TEAM_DESTROYED:
                {

                    //------------------------------------------------------------------
                    // EngineConstants.EVENT_TYPE_TEAM_DESTROYED:
                    // Sent by: The engine
                    // When: A creature's entire team dies
                    //------------------------------------------------------------------

                    int nTeamID = engine.GetEventIntegerRef(ref ev, 0);

                    switch (nTeamID)
                    {

                        case EngineConstants.ORZ_TEAM_PROHARROW_SUPPORTERS:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_PROHARROW_SUPPORTERS:
                                // TEAM 1
                                //----------------------------------------------------------

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZPT_EVENTS, EngineConstants.ORZ_EVENT_NOBLES_QUARTER_SUPPORTER_FIGHT_OVER_SIDED_BHELEN, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;

                            }

                        case EngineConstants.ORZ_TEAM_PROBHELEN_SUPPORTERS:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_PROBHELEN_SUPPORTERS:
                                // TEAM 2
                                //----------------------------------------------------------
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZPT_EVENTS, EngineConstants.ORZ_EVENT_NOBLES_QUARTER_SUPPORTER_FIGHT_OVER_SIDED_HARROWMONT, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;

                            }

                        case EngineConstants.ORZ_TEAM_DACE_AMBUSHERS_1:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_DACE_AMBUSHERS_1:
                                // TEAM 5
                                //----------------------------------------------------------

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ340PT_FIND_LORD_DACE, EngineConstants.ORZ_DACE__EVENT_DACE_AMBUSH_SECOND_ROUND_SETUP, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;

                            }

                        case EngineConstants.ORZ_TEAM_DACE_AMBUSHERS_2:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_DACE_AMBUSHERS_2:
                                // TEAM 6
                                //----------------------------------------------------------

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ340PT_FIND_LORD_DACE, EngineConstants.ORZ_DACE__EVENT_DACE_AMBUSH_OVER, EngineConstants.TRUE, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_CARTA_ROGGAR:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_CARTA_ROGGAR:
                                // TEAM 9
                                //----------------------------------------------------------

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ200PT_FIGALE, EngineConstants.ORZ_FIGALE_ROGGAR_KILLED, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;

                            }

                        case EngineConstants.ORZ_TEAM_AOTV_TRAP_1:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_AOTV_TRAP_1:
                                // TEAM 13
                                //----------------------------------------------------------

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ540PT_ANVIL_OT_VOID, EngineConstants.ORZ_AOTV__EVENT_TRAP_1_SOLVED, EngineConstants.TRUE, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_AOTV_TRAP_2:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_AOTV_TRAP_2:
                                // TEAM 14
                                //----------------------------------------------------------

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ540PT_ANVIL_OT_VOID, EngineConstants.ORZ_AOTV__EVENT_TRAP_2_SOLVED, EngineConstants.TRUE, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_CARTA_HOME_AMBUSHERS:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_CARTA_HOME_AMBUSHERS:
                                // TEAM 17
                                //----------------------------------------------------------

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZPT_CARTA, EngineConstants.ORZ_CARTA__EVENT_HOUSE_AMBUSH_LAST_THUG_SURRENDERS, EngineConstants.TRUE, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_ROGEK:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_ROGEK:
                                // TEAM 18
                                //----------------------------------------------------------

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ400PT_ROGEK, EngineConstants.ORZ_ROGEK_KILLED, EngineConstants.TRUE, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_BHELEN:
                            {
                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_BHELEN_ASSEMBLY:
                                // TEAM 19
                                //----------------------------------------------------------
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ340PT_ASSEMBLY, EngineConstants.ORZ_ASSEMBLY_FINAL_SCENE_BHELEN_DEFEATED_AFTER_VOTE, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;
                            }

                        case EngineConstants.ORZ_TEAM_IMREK:
                            {
                                //----------------------------------------------------------
                                // TEAM 26
                                //----------------------------------------------------------
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ100PT_IMREK, EngineConstants.ORZ_IMREK_IS_DEAD, EngineConstants.TRUE);
                                break;
                            }

                        case EngineConstants.ORZ_TEAM_KARDOL_DARKSPAWN_1:
                            {
                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_KARDOL_DARKSPAWN_1
                                // TEAM 30
                                //----------------------------------------------------------
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ550PT_KARDOL, EngineConstants.ORZ_KARDOL_INTRO_FIGHT_OVER, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;
                            }

                        case EngineConstants.ORZ_TEAM_KARDOL_DARKSPAWN_4:
                            {
                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_KARDOL_DARKSPAWN_1
                                // TEAM 89
                                //----------------------------------------------------------
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ550PT_KARDOL, EngineConstants.ORZ_KARDOL_BRIDGE_CLEARED, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;
                            }

                        case EngineConstants.ORZ_TEAM_RUCK:
                            {
                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_RUCK:
                                // TEAM 33
                                //----------------------------------------------------------
                                if (engine.WR_GetPlotFlag(EngineConstants.PLT_ORZ200PT_FILDA, EngineConstants.ORZ_FILDA___PLOT_ACTIVE) != EngineConstants.FALSE)
                                    engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ200PT_FILDA, EngineConstants.ORZ_FILDA___PLOT_02_RUCK_KILLED, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;
                            }

                        case EngineConstants.ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_1:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_1:
                                // TEAM 43
                                //----------------------------------------------------------
                                engine.UT_TeamAppears(EngineConstants.ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_2);
                                break;
                            }

                        case EngineConstants.ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_2:
                            {
                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_2:
                                // TEAM 44
                                //----------------------------------------------------------
                                engine.UT_TeamAppears(EngineConstants.ORZ_TEAM_CARIDINS_CROSS_DSTALKER_NEST_3);
                                break;
                            }

                        case EngineConstants.ORZ_TEAM_BRANKA_DARKSPAWN_WAVE_1:
                        case EngineConstants.ORZ_TEAM_BRANKA_DARKSPAWN_WAVE_2:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_BRANKA_DARKSPAWN_WAVE_1/2
                                // TEAM 50/ 51
                                //----------------------------------------------------------
                                GameObject oBranka = engine.GetObjectByTag(EngineConstants.ORZ_CR_BRANKA);
                                engine.UT_Talk(oBranka, oBranka, "", EngineConstants.FALSE);

                                break;
                            }

                        case EngineConstants.ORZ_TEAM_PALACE_THIEVES:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_PALACE_THIEVES
                                // TEAM 55
                                //----------------------------------------------------------

                                GameObject oGaurd, oWaypoint;

                                oGaurd = engine.GetObjectByTag(EngineConstants.ORZ_CR_TUNNELING_THIEF_GUARD);
                                oWaypoint = engine.GetObjectByTag(EngineConstants.ORZ_WP_GAURD_MOVETO);

                                engine.WR_SetObjectActive(oGaurd, EngineConstants.TRUE);

                                engine.AddCommand(oGaurd, engine.CommandMoveToObject(oWaypoint), EngineConstants.TRUE);
                                engine.UT_Talk(oGaurd, oPC);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_PROVING_LITE_1:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_PROVING_LITE_1
                                // TEAM 66
                                //----------------------------------------------------------

                                engine.ORZ_CleanUpLiteProving();

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_LITE_PROVING, EngineConstants.ORZ_LTP_FIGHT_WON, EngineConstants.TRUE, EngineConstants.TRUE);
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_LITE_PROVING, EngineConstants.ORZ_LTP_FIGHTS_WON_01, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_PROVING_LITE_2:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_PROVING_LITE_2
                                // TEAM 67
                                //----------------------------------------------------------

                                engine.ORZ_CleanUpLiteProving();

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_LITE_PROVING, EngineConstants.ORZ_LTP_FIGHT_WON, EngineConstants.TRUE, EngineConstants.TRUE);
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_LITE_PROVING, EngineConstants.ORZ_LTP_FIGHTS_WON_02, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_PROVING_LITE_3:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_PROVING_LITE_3
                                // TEAM 68
                                //----------------------------------------------------------

                                engine.ORZ_CleanUpLiteProving();

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_LITE_PROVING, EngineConstants.ORZ_LTP_FIGHT_WON, EngineConstants.TRUE, EngineConstants.TRUE);
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_LITE_PROVING, EngineConstants.ORZ_LTP_FIGHTS_WON_03, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_PROVING_LITE_4:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_PROVING_LITE_4
                                // TEAM 69
                                //----------------------------------------------------------

                                engine.ORZ_CleanUpLiteProving();

                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_LITE_PROVING, EngineConstants.ORZ_LTP_FIGHT_WON, EngineConstants.TRUE, EngineConstants.TRUE);
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ260PT_LITE_PROVING, EngineConstants.ORZ_LTP_FIGHTS_WON_04, EngineConstants.TRUE);

                                break;

                            }

                        case EngineConstants.ORZ_TEAM_AMBASSADOR:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_AMBASSADOR
                                // TEAM 900
                                //----------------------------------------------------------

                                // LC: For the Denerim Assassination Missions
                                engine.WR_SetPlotFlag(EngineConstants.PLT_DEN200PT_ASSASSIN_ORZ, EngineConstants.AMBASSADOR_KILLED, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;

                            }

                        case EngineConstants.ORZ_TEAM_CARIDIN_GOLEMS:
                            {

                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_CARIDIN_GOLEMS
                                // TEAM 16
                                //----------------------------------------------------------

                                // Update plot depending on who you sided with
                                if (engine.WR_GetPlotFlag(EngineConstants.PLT_ORZPT_ANVIL, EngineConstants.ORZ_ANVIL_CARIDIN_ATTACKS) != EngineConstants.FALSE)
                                    engine.WR_SetPlotFlag(EngineConstants.PLT_ORZPT_ANVIL, EngineConstants.ORZ_ANVIL___PLOT_07_CARIDIN_KILLED, EngineConstants.TRUE, EngineConstants.TRUE);
                                else
                                    engine.WR_SetPlotFlag(EngineConstants.PLT_ORZPT_ANVIL, EngineConstants.ORZ_ANVIL___PLOT_07_BRANKA_KILLED, EngineConstants.TRUE, EngineConstants.TRUE);

                                break;
                            }

                        case EngineConstants.ORZ_TEAM_BROODMOTHER:
                            {
                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_BROODMOTHER
                                // TEAM 78
                                //----------------------------------------------------------
                                // Hespith has something to say
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZ550PT_HESPITH, EngineConstants.ORZ_HESPITH_BROODMOTHER_DEFEATED, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;
                            }

                        case EngineConstants.ORZ_TEAM_CARTA_JARVIA:
                            {
                                //----------------------------------------------------------
                                // EngineConstants.ORZ_TEAM_CARTA_JARVIA
                                // TEAM 8
                                //----------------------------------------------------------
                                engine.WR_SetPlotFlag(EngineConstants.PLT_ORZPT_CARTA, EngineConstants.ORZ_CARTA_JARVIA_DEAD, EngineConstants.TRUE, EngineConstants.TRUE);
                                break;
                            }
                        case EngineConstants.ORZ_TEAM_SPIDER_QUEEN_SPIDERLINGS:
                        case EngineConstants.ORZ_TEAM_SPIDER_QUEEN_SPIDERLINGS_2:
                            {
                                GameObject oSpiderQueen = engine.UT_GetNearestCreatureByTag(oPC, EngineConstants.ORZ_CR_QUEEN_SPIDER);
                                engine.SignalEvent(oSpiderQueen, engine.Event(EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01));
                                break;
                            }
                        case EngineConstants.ORZ_TEAM_SPIDER_QUEEN:
                            {
                                GameObject oBrankaJournal = engine.UT_GetNearestObjectByTag(oPC, EngineConstants.ORZ_IP_BRANKA_JOURNAL);
                                // Branka's journal can now be interacted with
                                engine.SetObjectInteractive(oBrankaJournal, EngineConstants.TRUE);
                                break;
                            }
                    }

                    bEventHandled = EngineConstants.TRUE;

                    break;

                }

        }

        //--------------------------------------------------------------------------
        // Pass any unhandled events to area_core
        //--------------------------------------------------------------------------

        if (bEventHandled == EngineConstants.FALSE)
            engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_AREA_CORE);

    }
}