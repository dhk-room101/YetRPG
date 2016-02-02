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

public class module_core : MonoBehaviour
{
    // All module events

    //#include"ability_h"
    //#include"log_h"
    //#include"utility_h"
    //#include"wrappers_h"
    //#include"events_h"
    //#include"approval_h"
    //#include"events_h"
    //#include"world_maps_h"
    //#include"placeable_h"
    //#include"ai_main_h_2"
    //#include"sys_soundset_h"
    //#include"tutorials_h"
    //#include"plt_gen00pt_party"
    //#include"plt_tut_combat_basic"
    //#include"plt_tut_combat_basic_magic"
    //#include"plt_tut_codex_item"
    //#include"plt_tut_aicontrol"
    //#include"plt_tut_areamap"
    //#include"plt_tut_crafting"
    //#include"plt_tut_journal"
    //#include"plt_tut_store"
    //#include"plt_tut_worldmap"
    //#include"plt_tut_overload"
    //#include"plt_tut_chanters_board"
    //#include"plt_tut_item_upgrade"
    //#include"achievement_core_h"
    //#include"stats_core_h"

    Engine engine { get; set; }
    xGameObjectBase oBase;
    int counter;

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
        oBase = gameObject.GetComponent<xGameObjectBase>();
    }

    void Update()
    {
        //if (bStart)
        counter++;
        if (10 - counter < 0)
        {
            //engine.Warning(" increment update " + counter);
            counter = 0;
            //If current events not null 
            if (oBase.qEvent != null &&
            oBase.qEvent.Count > 0 &&
            //Or there is no custom class to take precedence, or if the event got redirected
            (oBase.bCustom == EngineConstants.FALSE ||
            oBase.bRedirected == EngineConstants.TRUE))
            {
                //and event is not type invalid
                if (oBase.qEvent[0].nType != EngineConstants.EVENT_TYPE_INVALID)
                {
                    //Do the obvious :-)
                    HandleEvent();
                }
            }
        }
    }

    public void HandleEvent()
    {
        xEvent ev = engine.GetCurrentEvent();
        int nEvent = engine.GetEventTypeRef(ref ev);

        engine.Log_Events("", ev);

        switch (nEvent)
        {
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: The module starts. This can happen only once for a single
            //       game instance.
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_MODULE_START:
                {
                    //TrackModuleEvent(nEvent, gameObject);

                    //TrackSendGameId(EngineConstants.TRUE);

                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: The module loads from a save game, or for the first time. This xEvent can fire more than
            //       once for a single module or game instance.
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_MODULE_LOAD:
                {
                    engine.Log_Trace(EngineConstants.LOG_LEVEL_DEBUG, "origin", "module loading", gameObject);

                    //            engine.Log_InitSystem(EngineConstants.LOG_LEVEL_DEBUG, EngineConstants.LOG_LEVEL_DEBUG, EngineConstants.LOG_LEVEL_DEBUG, EngineConstants.LOG_LEVEL_DEBUG);
                    engine.Approval_Init();

                    // Hostility setups
                    engine.SetGroupHostility(EngineConstants.GROUP_PC, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);
                    engine.SetGroupHostility(EngineConstants.GROUP_FRIENDLY, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);

                    //TrackModuleEvent(nEvent, gameObject);

                    //TrackSendGameId(EngineConstants.FALSE);

                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: The player changes a game option from the options menu
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_OPTIONS_CHANGED:
                {

                }
                break;
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: The player completes the final step of a quest
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_PLOT_COMPLETED:
                {
                    // Increment "finished quests" counter
                    engine.ACH_LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, "A quest was completed.");
                    engine.ACH_TrackCompletedQuests();
                }
                break;
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: The party's money changes
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_PARTY_MONEY_CHANGED:
                {
                    int nMoney = engine.GetEventIntegerRef(ref ev, 0);
                    // //Track the new money value
                    engine.STATS_HandleMaxMoney(nMoney);
                    // //Track money spent
                    //STATS_TrackMoneySpent(nMoney);
                }
                break;
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: A codex was added or changed (excludes tutorial codex)
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_CODEX_CHANGED:
                {
                    int iPrevious = engine.GetEventIntegerRef(ref ev, 0); //1 if the codex is unlocked, else it is an update
                    int iTitle = engine.GetEventIntegerRef(ref ev, 1); //strref of the codex title
                    int iDesc = engine.GetEventIntegerRef(ref ev, 2); //strref of the codex desc

                    //Send the description resref of the codex as the eventID to the server
                    engine.LogStoryEvent(iDesc);

                    // //Track the number of codex entries, only if this is the first unlock
                    if (iPrevious != EngineConstants.FALSE) engine.STATS_TrackCodexEntries();//uncomment by DHK
                }
                break;
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: A player objects enters the module
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_ENTER:
                {
                    GameObject oCreature = engine.GetEventObjectRef(ref ev, 0);
                    //TrackModuleEvent(nEventType, gameObject, oCreature);

                    break;
                }
            ////////////////////////////////////////////////////////////////////////
            // Sent by: The engine
            // When: the player clicks on a destination in the world map
            ////////////////////////////////////////////////////////////////////////
            case EngineConstants.EVENT_TYPE_WORLD_MAP_USED:
                {
                    int nFrom = engine.GetEventIntegerRef(ref ev, 0); // travel start location
                    int nTo = engine.GetEventIntegerRef(ref ev, 1); // travel target location

                    //TrackModuleEvent(nEvent, gameObject);

                    break;
                }

            case 95 /*EVENT_CHARACTER_SHEET_DISPLAYED*/:
                {
                    GameObject oChar = engine.GetEventObjectRef(ref ev, 0);

                    if (engine.IsObjectValid(oChar) != EngineConstants.FALSE)
                    {
                        // -------------------------------------------------------------
                        // This updates the damage number of the display.
                        // -------------------------------------------------------------
                        engine.RecalculateDisplayDamage(oChar);

                        //Update stats
                        engine.STATS_HandlePercentDamageDealt(oChar);
                        engine.STATS_SetGameTimePlayed();
                        engine.ACH_CalculatePercentageComplete();
                    }

                    break;
                }

            case 102: /*ITEM CRAFTED*/
                {
                    GameObject oChar = engine.GetEventObjectRef(ref ev, 0);

                    //An item has been crafted, add it to the running total for ach. and stats
                    engine.ACH_CraftAchievement(oChar);
                    //STATS_TrackItemsCrafted();

                    break;
                }

            // ---------------------------------------------------------------------
            // Game Mode Switch
            //      int(0) - New Game Mode (EngineConstants.GM_* constant)
            //      int(1) - Old Game Mode (EngineConstants.GM_* constant)
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_GAMEMODE_CHANGE:
                {
                    int nNewGameMode = engine.GetEventIntegerRef(ref ev, 0);
                    int nOldGameMode = engine.GetEventIntegerRef(ref ev, 1);

                    // -----------------------------------------------------------------
                    // Georg: I'm tracking game mode switches for aggregated
                    //        'time spent in mode x' analysis
                    // -----------------------------------------------------------------
                    //TrackModuleEvent(nEvent, gameObject,null, nNewGameMode,nOldGameMode);
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Game mode changed from " + engine.ToString(nOldGameMode) + " to " + engine.ToString(nNewGameMode));

                    //------------------------------------------------------------------
                    // 2008-Feb-29 - EV 89889 by Georg
                    // We don't care about the new game mode being GUI and other
                    // non gameplay related modes
                    //------------------------------------------------------------------
                    if (nNewGameMode == EngineConstants.GM_GUI || nNewGameMode == EngineConstants.GM_FLYCAM || nNewGameMode == EngineConstants.GM_INVALID || nNewGameMode == EngineConstants.GM_PREGAME)
                    {
                        return;
                    }

                    if (nNewGameMode != EngineConstants.GM_COMBAT)
                    {
                        // prevent party members from attacking until allowed to do so
                        engine.AI_SetPartyAllowedToAttack(EngineConstants.FALSE);

                        //Check for levelup tutorial if in Explore mode
                        //don't show if chargen has been skipped
                        if (engine.ReadIniEntry("DebugOptions", "SkipCharGen") == "1")
                        {
                            //do nothing
                        }
                        else
                        {
                            if (nNewGameMode == EngineConstants.GM_EXPLORE)
                            {
                                GameObject oModule = engine.GetModule();
                                //1 means show the tutorial, 2 means it has been seen
                                if (engine.GetLocalInt(oModule, "TUTORIAL_HAVE_SEEN_LEVEL_UP") == 1)
                                {
                                    engine.SetLocalInt(oModule, "TUTORIAL_HAVE_SEEN_LEVEL_UP", 2);
                                    engine.BeginTrainingMode(EngineConstants.TRAINING_SESSION_LEVEL_UP);
                                }
                            }
                        }
                    }

                    if (nNewGameMode == EngineConstants.GM_COMBAT || nNewGameMode == EngineConstants.GM_EXPLORE)
                    {
                        engine.Ability_OnGameModeChange(nNewGameMode);
                    }

                    // ----------------------------------------------------------------
                    // Remove party soundset restrictions
                    // -----------------------------------------------------------------
                    engine.SSPartyResetSoundsetRestrictions();

                    // -----------------------------------------------------------------
                    // If new game mode is combat, set CombatState on each Party Member
                    // -----------------------------------------------------------------
                    engine.SetCombatStateParty(nNewGameMode == EngineConstants.GM_COMBAT ? EngineConstants.TRUE : EngineConstants.FALSE);

                    if (nNewGameMode == EngineConstants.GM_COMBAT)
                    {
                        // Tutorial message
                        if (engine.GetLocalInt(engine.GetModule(), EngineConstants.TUTORIAL_ENABLED) != EngineConstants.FALSE)
                        {
                            if (engine.GetCreatureCoreClass(engine.GetHero()) == EngineConstants.CLASS_WIZARD)
                            {
                                engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_COMBAT_BASIC_MAGIC, EngineConstants.TUT_COMBAT_BASIC_MAGIC_1, EngineConstants.TRUE);
                            }
                            else
                                engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_COMBAT_BASIC, EngineConstants.TUT_COMBAT_BASIC_1, EngineConstants.TRUE);
                        }
                    }
                    // else below

                    // <rant>
                    // Georg Sept 10, 2008:
                    //        If we are going into dialog mode, we always revive the party.
                    //        Note: I am not happy about this. The core problem is that we have to
                    //              requirements fighting with each other.
                    //
                    //              a) Don't have people jump up immediately after combat
                    //              b) Before a dialog starts, people need to be revived and it has to happen
                    //                 inline in the currently executing context because once it ends, we're
                    //                 no longer in control of what happens, the dialog/cutscene engine takes over.
                    //
                    //              This lends itself to a host of issues, including race coditions.
                    //
                    //              The real solution is to have the writers stop writing 'combat ends,
                    //              force dialog situations' but that's not gonna happen for DA.
                    //
                    //              The following 3 lines will force all party members alive - immediately
                    //              in-line. Implicitly, this will cause termination of the delayed gamemode
                    //              change xEvent loop in the xEvent queue next time it gets processed.
                    //
                    //
                    else if (nNewGameMode == EngineConstants.GM_CONVERSATION)
                    {
                        int gm = engine.GetLocalInt(engine.GetModule(), "GAME_MODE");
                        //Check one more time to make sure you're not in combat mode Or dead, it shouldn't be necessary
                        if (gm == EngineConstants.GM_COMBAT || gm == EngineConstants.GM_DEAD)
                        {
                            //WTF, how did it get this far but still in combat mode Or dead
                            throw new NotImplementedException();
                        }

                        engine.ResurrectPartyMembers();//As per Georg comment above

                        //Now set the game mode in conversation mode and let the party begin :-)
                        //string co = engine.GetLocalString(engine.GetModule(), "CONVERSATION");
                        engine.SetLocalString(engine.GetModule(), "GAME_MODE", nNewGameMode.ToString());
                    }
                    // </rant>

                    break;
                }

            // ---------------------------------------------------------------------
            // Party Resurrection button (cheat, death UI)
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DEATH_RES_PARTY:
                {
                    //TrackModuleEvent(nEvent, gameObject,null);

                    engine.ResurrectPartyMembers(EngineConstants.TRUE);

                    engine.DelayEvent(0.5f, gameObject, engine.Event(EngineConstants.EVENT_TYPE_DEBUG_RESURRECTION));

                    break;
                }

            case EngineConstants.EVENT_TYPE_DELAYED_GM_CHANGE:
                {
                    //Needs to look into delayed game mode change
                    int gm = engine.GetGameMode();
                    if (engine.IsPartyPerceivingHostiles(engine.GetHero()) == EngineConstants.FALSE)
                    {
                        if (engine.IsPartyDead() == EngineConstants.FALSE)
                        {

                            if (gm == EngineConstants.GM_COMBAT)
                            {
                                if (engine.CheckResurrection() != EngineConstants.FALSE)
                                {
                                    // ------------------------------------------------------------------
                                    // ... we switch the game back to explore mode.
                                    // Note: This switches CombatState on all party members as party of
                                    //       the GameModeChange Module Level engine.Event
                                    // ------------------------------------------------------------------
                                    engine.WR_SetGameMode(EngineConstants.GM_EXPLORE);
                                }
                                else
                                {
                                    // Player out of range, try again later.
                                    engine.DelayEvent(1.0f, gameObject, engine.Event(EngineConstants.EVENT_TYPE_DELAYED_GM_CHANGE));
                                }
                            }
                            else if (gm == EngineConstants.GM_EXPLORE)
                            {
                                if (engine.CheckForDeadPartyMembers() != EngineConstants.FALSE)
                                {
                                    if (engine.CheckResurrection() == EngineConstants.FALSE)
                                    {
                                        engine.DelayEvent(2.0f, gameObject, engine.Event(EngineConstants.EVENT_TYPE_DELAYED_GM_CHANGE));
                                    }
                                }
                            }
                            else
                            {
                                // Lets try again in two seconds
                                engine.DelayEvent(2.0f, gameObject, engine.Event(EngineConstants.EVENT_TYPE_DELAYED_GM_CHANGE));
                            }
                        }
                    }

                    break;
                }

            case EngineConstants.EVENT_TYPE_AUTOPAUSE:
                {
                    if (engine.GetGameMode() == EngineConstants.GM_EXPLORE || engine.GetGameMode() == EngineConstants.GM_COMBAT)
                    {
                        engine.ToggleGamePause();
                    }
                    break;
                }

            //----------------------------------------------------------------------
            // Game Mode Changed.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_SET_GAME_MODE:
                {
                    int nGameMode = engine.GetEventIntegerRef(ref ev, 0);
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "Setting game mode to: " + engine.IntToString(nGameMode));

                    // -----------------------------------------------------------------
                    // Signal Pause xEvent to the game when hitting combat
                    // -----------------------------------------------------------------
                    if (nGameMode == EngineConstants.GM_COMBAT && engine.GetGameMode() == EngineConstants.GM_EXPLORE)
                    {
                        if (engine.ConfigIsAutoPauseEnabled() != EngineConstants.FALSE)
                        {
                            engine.DelayEvent(0.5f, gameObject, engine.Event(EngineConstants.EVENT_TYPE_AUTOPAUSE));
                        }
                    }

                    if (nGameMode == EngineConstants.GM_EXPLORE && engine.IsNoExploreArea() != EngineConstants.FALSE)
                    {
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "COMBAT-ONLY EngineConstants.AREA!!! ABORTING CHANGE TO EXPLORE MODE!!!");
                        break;
                    }

                    engine.SetGameMode(nGameMode);
                    
                    break;
                }

            // ---------------------------------------------------------------------
            // This makes the resurrection button work.
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DEBUG_RESURRECTION:
                {
                    if (engine.GetGameMode() == EngineConstants.GM_DEAD)
                    {
                        if (engine.IsPartyDead() == EngineConstants.FALSE)
                        {
                            if (engine.IsPartyPerceivingHostiles(engine.GetHero()) == EngineConstants.FALSE)
                            {
                                engine.SetGameMode(EngineConstants.GM_EXPLORE);
                            }
                        }
                    }

                    break;
                }
            case EngineConstants.EVENT_TYPE_CAMPAIGN_ITEM_ACQUIRED:
                {
                    GameObject oItem = engine.GetEventObjectRef(ref ev, 0);
                    string sItemTag = engine.GetTag(oItem);
                    GameObject oAcquirer = engine.GetEventCreatorRef(ref ev);

                    /// ----------------------------------------------------------------
                    // Handle specialization traiers
                    /// ----------------------------------------------------------------
                    int nSpec = engine.GetLocalInt(oItem, EngineConstants.ITEM_SPECIALIZATION_FLAG);
                    if (nSpec > 0)
                    {
                        engine.RW_UnlockSpecializationTrainer(nSpec);
                        engine.WR_DestroyObject(oItem);
                    }

                    int nCodexItem = engine.GetLocalInt(oItem, EngineConstants.ITEM_CODEX_FLAG);
                    if (nCodexItem > -1 && engine.IsFollower(oAcquirer) != EngineConstants.FALSE)
                    {

                        engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_CODEX_ITEM, EngineConstants.TUT_CODEX_ITEM, EngineConstants.TRUE);
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, engine.GetCurrentScriptName(), "Got a codex item: " + engine.GetTag(oItem));
                        engine.WR_SetPlotFlag(engine.GetTag(oItem), nCodexItem, EngineConstants.TRUE, EngineConstants.TRUE);
                        engine.WR_DestroyObject(oItem);
                    }

                    if (engine.GetTag(oItem) == "gen_im_misc_backpack")
                    {
                        if (engine.IsFollower(oAcquirer) != EngineConstants.FALSE)
                        {
                            int nSize = engine.GetMaxInventorySize(oAcquirer);
                            nSize += 10;
                            engine.SetMaxInventorySize(nSize, oAcquirer);
                            engine.WR_DestroyObject(oItem);
                        }
                    }

                    break;
                }

            case EngineConstants.EVENT_TYPE_CREATURE_ENTERS_CONVERSATION:
                {
                    GameObject oCreature = engine.GetEventObjectRef(ref ev, 0);

                    engine.RemoveEffectsDueToPlotEvent(oCreature);

                    engine.DEBUG_PrintToScreen("Enter Dialog:" + engine.ToString(oCreature));

                    break;
                }

            case EngineConstants.EVENT_TYPE_COMBO_IGNITE:
                {
                    engine.HandleEventRef(ref ev, "sys_comboeffects.ncs");
                    break;
                }

            // Party member added to active party using the party GUI
            case EngineConstants.EVENT_TYPE_PARTYMEMBER_ADDED:
                {
                    GameObject oFollower = engine.GetEventObjectRef(ref ev, 0);
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "EngineConstants.EVENT_TYPE_PARTYMEMBER_ADDED, follower: " + engine.GetTag(oFollower));

                    engine.WR_SetObjectActive(oFollower, EngineConstants.TRUE);

                    string sTag = engine.GetTag(oFollower);
                    if (sTag == EngineConstants.GEN_FL_ALISTAIR) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ALISTAIR_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_DOG) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_LELIANA) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_LOGHAIN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LOGHAIN_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_MORRIGAN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_MORRIGAN_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_OGHREN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_OGHREN_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_SHALE) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_SHALE_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_STEN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_WYNNE) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_ZEVRAN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_IN_PARTY, EngineConstants.TRUE, EngineConstants.TRUE);

                    break;
                }
            // Party member removed from active party using the party GUI
            case EngineConstants.EVENT_TYPE_PARTYMEMBER_DROPPED:
                {
                    GameObject oFollower = engine.GetEventObjectRef(ref ev, 0);
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), "EngineConstants.EVENT_TYPE_PARTYMEMBER_DROPPED, follower: " + engine.GetTag(oFollower));

                    string sTag = engine.GetTag(oFollower);
                    if (sTag == EngineConstants.GEN_FL_ALISTAIR) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ALISTAIR_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_DOG) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_LELIANA) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_LOGHAIN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LOGHAIN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_MORRIGAN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_MORRIGAN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_OGHREN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_OGHREN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_SHALE) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_SHALE_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_STEN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_WYNNE) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
                    else if (sTag == EngineConstants.GEN_FL_ZEVRAN) engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);

                    engine.WR_SetObjectActive(oFollower, EngineConstants.FALSE);
                    break;
                }
            case EngineConstants.EVENT_TYPE_WORLD_MAP_CLOSED:
                {
                    engine.WM_SetWorldMapGuiStatus();
                    break;
                }
            case EngineConstants.EVENT_TYPE_PARTYPICKER_CLOSED:
                {
                    engine.WM_SetPartyPickerGuiStatus();
                    break;
                }

            case EngineConstants.EVENT_TYPE_POPUP_RESULT:
                {
                    GameObject oOwner = engine.GetEventObjectRef(ref ev, 0);      // owner of popup
                    int nPopupID = engine.GetEventIntegerRef(ref ev, 0);     // popup ID
                    int nButton = engine.GetEventIntegerRef(ref ev, 1);     // button result (1 - 4)

                    switch (nPopupID)
                    {
                        case 1:     // Placeable area transition
                            engine.SignalEvent(oOwner, ev);
                            break;
                    }
                    break;
                }

            //GUI tutorial calls
            case EngineConstants.EVENT_TYPE_GUI_OPENED:
                {
                    int nGUIID = engine.GetEventIntegerRef(ref ev, 0);
                    switch (nGUIID)
                    {

                        case EngineConstants.GUI_INVENTORY: //Inventory ScreenOpened
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_INVENTORY, EngineConstants.TUT_INVENTORY_1, EngineConstants.TRUE);
                            break;
                        case EngineConstants.GUI_JOURNAL: //Journal
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_JOURNAL, EngineConstants.TUT_JOURNAL_1, EngineConstants.TRUE);
                            break;
                        case EngineConstants.GUI_MAP: //Area Map
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_AREAMAP, EngineConstants.TUT_AREA_MAP_1, EngineConstants.TRUE);
                            break;
                        case EngineConstants.GUI_WORLD_MAP: //World Map
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_WORLDMAP, EngineConstants.TUT_WORLDMAP_1, EngineConstants.TRUE);
                            break;
                        case EngineConstants.GUI_TACTICS: //AI Tactics screen
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_AICONTROL, EngineConstants.TUT_AI_CONTROL_1, EngineConstants.TRUE);
                            break;
                        case EngineConstants.GUI_STORE: //Store GUI
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_STORE, EngineConstants.TUT_FIRST_STORE_1, EngineConstants.TRUE);
                            break;
                        case EngineConstants.GUI_CRAFTING: //Crafting
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_CRAFTING, EngineConstants.TUT_CRAFTING_1, EngineConstants.TRUE);
                            break;
                        case EngineConstants.GUI_CHANTERS: //Chanter's Board
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_CHANTERS_BOARD, EngineConstants.TUT_CHANTERS_BOARD, EngineConstants.TRUE);
                            break;
                        case EngineConstants.GUI_ITEM_UPGRADE: //Enchanter Upgrade
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_ITEM_UPGRADE, EngineConstants.TUT_ITEM_UPGRADE_SCREEN_OPEN, EngineConstants.TRUE);
                            break;
                    }
                    break;
                }

            case EngineConstants.EVENT_TYPE_INVENTORY_FULL:
                {
                    engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_OVERLOAD, EngineConstants.TUT_INVENTORY_OVERLOAD_1, EngineConstants.TRUE);
                    break;
                }

            case EngineConstants.EVENT_TYPE_ACHIEVEMENT:
                {
                    engine.ACH_ProcessAchievementModuleEvent(ev);
                    break;
                }
            case EngineConstants.EVENT_TYPE_PARTYPICKER_INIT:
                {
                    // Match follower EngineConstants.XP to player (in case the player can open the party picker in this area
                    List<GameObject> arParty = engine.GetPartyPoolList();
                    arParty = engine.GetPartyPoolList();
                    int nSize = engine.GetArraySize(arParty);
                    int i;
                    GameObject oCurrent;

                    for (i = 0; i < nSize; i++)
                    {
                        oCurrent = arParty[i];
                        engine.RW_CatchUpToPlayer(oCurrent);
                    }

                    break;
                }

            default:
                {
                    // -----------------------------------------------------------------
                    // Handle character generation events sent by the engine.
                    // -----------------------------------------------------------------
                    if ((nEvent >= EngineConstants.EVENT_TYPE_CHARGEN_START && nEvent <= EngineConstants.EVENT_TYPE_CHARGEN_END) || nEvent == EngineConstants.EVENT_TYPE_PLAYERLEVELUP)
                    {
                        engine.Warning(" character generation started");
                        engine.HandleEventRef(ref ev, "sys_chargen.ncs");
                    }
                    else
                    {
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, engine.GetCurrentScriptName(), engine.Log_GetEventNameById(nEvent) + " (" + engine.ToString(nEvent) + ") *** Unhandled xEvent ***");
                    }
                    break;
                }
        }

        //Outside the switch loop, assuming a break
        //In case the event was actually redirected, giveback control to the custom script
        if (oBase.bRedirected == EngineConstants.TRUE)
        {
            oBase.bRedirected = EngineConstants.FALSE;
        }
    }
}