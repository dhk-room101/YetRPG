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

public class player_core : MonoBehaviour
{
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
        counter++;
        if (10 - counter < 0)
        {
            //engine.Warning(" increment update " + counter);
            counter = 0;
            //If current events not null
            if (oBase.qEvent != null &&
            oBase.qEvent.Count > 0)
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

    //#include"ability_h"
    //#include"effects_h"
    //#include"events_h"
    //#include"config_h"
    //#include"ai_main_h_2"
    //#include"global_objects_h"
    //#include"sys_injury"
    //#include"sys_autoscale_h"
    //#include"sys_itemsets_h"
    //#include"sys_traps_h"
    //#include"approval_h"
    //#include"sys_autolevelup_h"
    //#include"sys_rewards_h"
    //#include"tutorials_h"

    //#include"plt_tut_combat_salve"
    //#include"plt_tut_fatigue"
    //#include"plt_tut_armor_archer"
    //#include"plt_tut_first_gift"

    //#include"stats_core_h"

    //moved public const int APPROVAL_DEATH_PENALTY = -3;

    public void _ScheduleResurrectionAttempt(GameObject oCreature)
    {
        engine.DelayEvent(6.0f, oCreature, engine.Event(EngineConstants.EVENT_TYPE_PARTY_MEMBER_RES_TIMER));
    }

    // -----------------------------------------------------------------------------
    // @brief: Post resurrection event. Trigger soundset on player, add injury and
    // approval penalties
    // @author: Georg
    // -----------------------------------------------------------------------------
    public int HandleEvent_Resurrection(GameObject oCreature, xEvent ev)
    {
        int bApplyInjury = engine.GetEventIntegerRef(ref ev, 0);
        if (bApplyInjury != EngineConstants.FALSE)
        {
            engine.PlaySoundSet(oCreature, EngineConstants.SS_EXPLORE_HEAL_ME);
            engine.Injury_DetermineInjury(oCreature);
        }

        // redo itemset bonuses
        engine.ItemSet_Update(oCreature);

        // CUT!
        //int nFollower = engine.Approval_GetFollowerIndex(gameObject);
        //if(nFollower != -1)
        //    engine.Approval_ChangeApproval(nFollower, APPROVAL_DEATH_PENALTY);

        return EngineConstants.TRUE;

    }

    // -----------------------------------------------------------------------------
    // Spawn engine.Event Handler
    //
    // Purpose:
    // -- engine.Set Stats
    // -- Add Abilities
    //
    // -----------------------------------------------------------------------------
    public int HandleEvent_Spawn(xEvent ev)
    {
        //During debug, just initialize any party member, player or not
        engine.AS_InitCreature(gameObject);
        /*if (engine.IsHero(gameObject) == EngineConstants.FALSE)
        {
            engine.AS_InitCreature(gameObject);
        }
        else
        {
            // ---------------------------------------------------------------------
            // Hero character gets his heartbeat xEvent initialized here.
            // Followers get theirs when they are hired.
            // ---------------------------------------------------------------------
            engine.InitHeartbeat(gameObject, EngineConstants.CONFIG_CONSTANT_HEARTBEAT_RATE);
        }*/

        return EngineConstants.TRUE;

    }

    // -----------------------------------------------------------------------------
    // Perception Disappear engine.Event Handler
    // Parameters:
    // -- Obj(0): Creature appearing
    //
    // Purpose:
    // -- Ends engine.Delayed shout loop
    // -- engine.Sets combat mode to false if no hostiles are around anymore
    // -----------------------------------------------------------------------------
    public int HandleEvent_PerceptionDisappear(xEvent ev)
    {

        GameObject oDisappearer = engine.GetEventObjectRef(ref ev, 0); //GetEventCreatorRef(ref ev);

        // -----------------------------------------------------------------
        // If we unperceive a hostile object, and it's the last perceived
        // hostile, drop out of combat.
        // -----------------------------------------------------------------
        if (engine.IsObjectHostile(oDisappearer, gameObject) != EngineConstants.FALSE)
        {
            engine.Combat_HandleCreatureDisappear(gameObject, oDisappearer);
        }
        else if (engine.IsObjectValid(oDisappearer) == EngineConstants.FALSE) // For cases where creatures are destroyed when dead (spirit, explodes)
        {
            if (engine.IsPartyPerceivingHostiles(gameObject) == EngineConstants.FALSE)
            {

                if (engine.IsPartyDead() == EngineConstants.FALSE)
                {
#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "HandleEvent_PerceptionDisappear", "STOPPING COMBAT FOR PARTY!");
#endif
                    /* ResurrectPartyMembers();

                     // ------------------------------------------------------------------
                     // ... we switch the game back to explore mode.
                     // Note: This switches CombatState on all party members as party of
                     //       the GameModeChange Module Level engine.Event
                     // ------------------------------------------------------------------
                     engine.WR_SetGameMode(EngineConstants.GM_EXPLORE);*/
                    engine.DelayEvent(1.0f, engine.GetModule(), engine.Event(EngineConstants.EVENT_TYPE_DELAYED_GM_CHANGE));
                }
            }
        }

        // -------------------------------------------------------------
        // engine.Event was fully handled, do not fall through to rules_core
        // -------------------------------------------------------------
        return EngineConstants.TRUE;
    }

    // -----------------------------------------------------------------------------
    // Item Equip engine.Event Handler
    // -- sets 'prefer ranged' flag is equipping a ranged weapon in the main hand
    //
    //  Params:
    //      int (0) - the inventory slot the item was equipped to
    //      obj (0) - the item
    // -----------------------------------------------------------------------------
    public int HandleEvent_Equip(xEvent ev)
    {
        GameObject oItem = engine.GetEventObjectRef(ref ev, 0);
        int nEquipByPlayer = engine.GetEventIntegerRef(ref ev, 1);

#if DEBUG
        engine.Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "player_core", "itm:" + engine.ToString(oItem) + " abi:" + engine.ToString(engine.GetItemAbilityId(oItem)));
#endif

#if SKYNET
    //TrackItemEvent(engine.GetEventTypeRef(ref ev),gameObject,oItem);
#endif

        // Handle Item engine.Set //Tracking here
        engine.ItemSet_Update(gameObject);

        if (nEquipByPlayer != EngineConstants.FALSE)
        {
            if (engine.GetBaseItemType(oItem) == EngineConstants.BASE_ITEM_TYPE_ARMOR_HEAVY ||
                engine.GetBaseItemType(oItem) == EngineConstants.BASE_ITEM_TYPE_ARMOR_LIGHT ||
                engine.GetBaseItemType(oItem) == EngineConstants.BASE_ITEM_TYPE_ARMOR_MASSIVE ||
                engine.GetBaseItemType(oItem) == EngineConstants.BASE_ITEM_TYPE_ARMOR_MEDIUM)
                engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_FATIGUE, EngineConstants.TUT_FATIGUE_1, EngineConstants.TRUE);
            if (engine.GetBaseItemType(oItem) == EngineConstants.BASE_ITEM_TYPE_ARMOR_HEAVY ||
                    engine.GetBaseItemType(oItem) == EngineConstants.BASE_ITEM_TYPE_ARMOR_MASSIVE)
                engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_ARMOR_ARCHER, EngineConstants.TUT_ARMOR_ARCHER_1, EngineConstants.TRUE);
        }

        // ------------------------------------------------------------------------
        // Temporary item enchantment code
        // ------------------------------------------------------------------------
        int nSlot = engine.GetEventIntegerRef(ref ev, 0);
        if (nSlot == EngineConstants.INVENTORY_SLOT_MAIN || (nSlot == EngineConstants.INVENTORY_SLOT_OFFHAND && engine.GetItemType(oItem) == EngineConstants.ITEM_TYPE_WEAPON_MELEE))
        {
            if (engine.HasEnchantments(gameObject) != EngineConstants.FALSE)
            {
                engine.EffectEnchantment_HandleEquip(oItem, gameObject);
            }
        }

        return EngineConstants.FALSE; // EngineConstants.FALSE IS IMPORTANT HERE! DO NOT CHANGE!
    }

    // -----------------------------------------------------------------------------
    // Item UnEquip engine.Event Handler
    //
    //  Params:
    //      int (0) - the inventory slot the item was removed from
    //      obj (0) - the item
    // -----------------------------------------------------------------------------
    public int HandleEvent_UnEquip(xEvent ev)
    {
        GameObject oItem = engine.GetEventObjectRef(ref ev, 0);

#if SKYNET
    //TrackItemEvent(engine.GetEventTypeRef(ref ev),gameObject,oItem);
#endif

        // Handle Item engine.Set //Tracking here
        engine.ItemSet_Update(gameObject);

        // ------------------------------------------------------------------------
        // Temporary item enchantment code
        // ------------------------------------------------------------------------
        int nSlot = engine.GetEventIntegerRef(ref ev, 0);
        if (nSlot == EngineConstants.INVENTORY_SLOT_MAIN || (nSlot == EngineConstants.INVENTORY_SLOT_OFFHAND && engine.GetItemType(oItem) == EngineConstants.ITEM_TYPE_WEAPON_MELEE))
        {
            if (engine.HasEnchantments(gameObject) != EngineConstants.FALSE)
            {
                engine.EffectEnchantment_HandleUnEquip(oItem, gameObject);
            }
        }

        // -------------------------------------------------------------------------
        // Disable modal abilities that have their condition changed.
        // #define EngineConstants.ABILITY_CONDITION_NONE          0x0
        // #define EngineConstants.ABILITY_CONDITION_MELEEEngineConstants.WEAPON   0x1
        // #define EngineConstants.ABILITY_CONDITION_SHIELD        0x2
        // #define EngineConstants.ABILITY_CONDITION_RANGEDEngineConstants.WEAPON  0x4
        // #define EngineConstants.ABILITY_CONDITION_BEHINDTARGET  0x8
        // #define EngineConstants.ABILITY_CONDITION_DUALEngineConstants.WEAPONS 0x040
        // #define EngineConstants.ABILITY_CONDITION_2HEngineConstants.WEAPON 0x080

        // -------------------------------------------------------------------------
        List<int> abi = engine.GetConditionedAbilities(gameObject, 0xC7);
        int nSize = engine.GetArraySize(abi);
        int i;
        for (i = 0; i < nSize; i++)
        {
            engine.Effects_RemoveUpkeepEffect(gameObject, abi[i]);
        }

        if (nSlot == EngineConstants.INVENTORY_SLOT_CHEST)
        {
#if DEBUG
            engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_GORE, "player_core:HandleEquip", "All gore removed due to changing armor");
#endif

            engine.Gore_RemoveAllGore(gameObject);
        }

        return EngineConstants.FALSE; // EngineConstants.FALSE IS IMPORTANT HERE! DO NOT CHANGE!
    }

    // -----------------------------------------------------------------------------
    // Inventory engine.Event Handler
    // -- Does nothing right now
    // -----------------------------------------------------------------------------

    public int HandleEvent_InventoryEvent(xEvent ev)
    {
        int nEventType = engine.GetEventTypeRef(ref ev);
        GameObject oOwner = engine.GetEventCreatorRef(ref ev);
        GameObject oItem = engine.GetEventObjectRef(ref ev, 0);

        // -------------------------------------------------------------------------
        // Georg: Stores process their inventory events immediately, even while the
        //        gamestate is paused. We need to pass this information on to
        //        any sub events generated by the equip script or they'll get
        //        queued up until after the UI quits, causing all kind of havok
        // -------------------------------------------------------------------------
        int bProcessImmediate = engine.GetEventIntegerRef(ref ev, 0);

        switch (nEventType)
        {
            case EngineConstants.EVENT_TYPE_INVENTORY_ADDED:
                {

                    //If the item acquired has the EngineConstants.ITEM_SEND_ACQUIRED_EVENT variable set,
                    //send an xEvent to the module so that custom scripting can be done.
                    int bSendCampaignEvent = engine.GetLocalInt(oItem, EngineConstants.ITEM_SEND_ACQUIRED_EVENT);
                    if (bSendCampaignEvent != 0)
                    {
                        engine.SendEventCampaignItemAcquired(engine.GetModule(), oItem, bProcessImmediate);
                    }

                    if (engine.GetBaseItemType(oItem) == EngineConstants.BASE_ITEM_TYPE_QUICK)
                    {
                        int nItemAbility = engine.GetItemAbilityId(oItem);
                        if (nItemAbility == EngineConstants.ITEM_ABILITY_HEALING_SALVE ||
                           nItemAbility == EngineConstants.ITEM_ABILITY_HEALING_SALVE_1 ||
                           nItemAbility == EngineConstants.ITEM_ABILITY_HEALING_SALVE_2 ||
                           nItemAbility == EngineConstants.ITEM_ABILITY_HEALING_SALVE_3 ||
                           nItemAbility == EngineConstants.ITEM_ABILITY_HEALING_SALVE_4)
                            engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_COMBAT_SALVE, EngineConstants.TUT_COMBAT_SALVE_1, EngineConstants.TRUE);
                    }
                    else if (engine.GetBaseItemType(oItem) == EngineConstants.BASE_ITEM_TYPE_GIFT)
                        engine.WR_SetPlotFlag(EngineConstants.PLT_TUT_FIRST_GIFT, EngineConstants.TUT_FIRST_GIFT_1, EngineConstants.TRUE);

                    break;
                }
            case EngineConstants.EVENT_TYPE_INVENTORY_REMOVED:
                {

                    // If the item removed has EngineConstants.ITEM_SEND_LOST_EVENT set send the event.
                    int bSendCampaignEvent = engine.GetLocalInt(oItem, EngineConstants.ITEM_SEND_LOST_EVENT);

                    if (bSendCampaignEvent != EngineConstants.FALSE)
                        engine.SendEventCampaignItemLost(engine.GetModule(), oItem, bProcessImmediate);

                    break;

                }
        }
        return EngineConstants.TRUE;
    }

    // -----------------------------------------------------------------------------
    // Death engine.Event andler.
    // Purpose:
    // -- engine.Clears AI target.
    // -- Prints log message.
    // -----------------------------------------------------------------------------
    public int HandleEvent_Death(xEvent ev)
    {
        // -------------------------------------------------------------------------
        // The death xEffect has been applied to this creature, either by losing hit points
        // or by explicit calling of the effect.
        // -------------------------------------------------------------------------
        GameObject oKiller = engine.GetEventCreatorRef(ref ev);
        int bPartyWipe = engine.IsPartyDead();

        engine.SetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_DYING, EngineConstants.FALSE);

        // -------------------------------------------------------------------------
        // SkyNet creature death tracking event.
        // -------------------------------------------------------------------------
#if SKYNET
    //TrackObjectDeath(ev);
#endif

        // -------------------------------------------------------------------------
        // engine.Clear the object's perception list
        // -------------------------------------------------------------------------
        engine.ClearPerceptionList(gameObject);

        engine.AI_Threat_UpdateDeath(gameObject);

        // -------------------------------------------------------------------------
        // If the party was wiped, set gamemode dead.
        // -------------------------------------------------------------------------
        if (bPartyWipe != EngineConstants.FALSE)
        {

            int iDeathHint = engine.GetLocalInt(engine.GetModule(), EngineConstants.DEATH_HINT);

            //If module variable "DEATH_HINT" is not zero, use it.
            if (iDeathHint != 0)
            {
                engine.SetDeathHint(iDeathHint, 205);
            }
            else
            {
                //If DEATH_HINT is zero, use loop to determine statistics on party.
                List<GameObject> oParty = engine.GetPartyList(engine.GetHero());
                int nSize = engine.GetArraySize(oParty);
                int i;
                int iLevelCounter = 0;
                //          int iTacticCounter;
                //          int iStaminaCounter;
                GameObject oCurrent;
                for (i = 0; i < nSize; i++)
                {
                    oCurrent = oParty[i];
                    if (engine.GetCanLevelUp(oCurrent) != EngineConstants.FALSE)
                    {
                        iLevelCounter += 1;
                    }
                }
                //Fire if party member needs to level up.
                if (iLevelCounter > 0)
                {
                    engine.SetDeathHint(2, 205);
                }
                else
                {
                    //Random Death hint
                    int iRows = engine.GetM2DARows(272);
                    int nRand = engine.Engine_Random(iRows) + 1;
                    nRand = engine.GetM2DARowIdFromRowIndex(272, nRand);
                    engine.SetDeathHint(nRand, 272);
                }
                engine.SetLocalInt(engine.GetModule(), EngineConstants.DEATH_HINT, 0);
            }

#if DEBUG
            engine.Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DEATH, "player_core.HandleOnDeath", "Everyone dead, changing game mode...");
#endif
            engine.WR_SetGameMode(EngineConstants.GM_DEAD);
        }
        else
        {
            // ---------------------------------------------------------------------
            // handle any plot-specific logic for a follower death
            // currently needed only for Wynne's special ability
            // ---------------------------------------------------------------------
            engine.SendModuleHandleFollowerDeath(gameObject);

            engine.SetCombatState(gameObject, EngineConstants.FALSE);

            // ---------------------------------------------------------------------
            // If we are in explore mode, schedule auto resurrection
            // ---------------------------------------------------------------------
            if (engine.GetGameMode() == EngineConstants.GM_EXPLORE)
            {
                //------------------------------------------------------------------
                // Sorry, summoned creatures can't be revived.
                //------------------------------------------------------------------
                if (engine.IsSummoned(gameObject) == EngineConstants.FALSE)
                {
                    _ScheduleResurrectionAttempt(gameObject);
                }
            }

            // ---------------------------------------------------------------------
            // This handles the 'party member slain' message;
            // ---------------------------------------------------------------------
            List<GameObject> aAlly = engine.GetNearestObjectByGroup(gameObject, engine.GetGroupId(gameObject), EngineConstants.OBJECT_TYPE_CREATURE, 1, 1, 0, 0);
            if (engine.GetArraySize(aAlly) > 0)
            {
                engine.SSPlaySituationalSound(aAlly[0], EngineConstants.SOUND_SITUATION_PARTY_MEMBER_SLAIN, oKiller);
            }

        }

        return EngineConstants.TRUE;
    }

    // -----------------------------------------------------------------------------
    // Load tactics xEvent handler
    // -- Currently this just uses a naive method to populate tactics with valid
    // -- skills.
    // -----------------------------------------------------------------------------
    public int HandleEvent_LoadTactics(GameObject oCreature, xEvent ev)
    {
        int nPresetID = engine.GetEventIntegerRef(ref ev, 0);
        engine.Chargen_LoadPresetsTable(oCreature, nPresetID);

        return EngineConstants.TRUE;
    }

    // -----------------------------------------------------------------------------
    // Use ability immediately.
    // -- Some player abilities are used immediately, bypassing the ai xCommand queue
    // -- in order to process them while the game is paused.
    // -- Currently this is only used for the crafting GUI.
    // -----------------------------------------------------------------------------
    public int HandleEvent_UseAbilityImmediate(GameObject oCreature, xEvent ev)
    {

        int nAbility = engine.GetEventIntegerRef(ref ev, 0);
        engine.ShowCraftingGUI(nAbility);

        return EngineConstants.TRUE;
    }

    public void HandleEvent()
    {
        xEvent ev = engine.GetCurrentEvent();
        int nEventType = engine.GetEventTypeRef(ref ev);

        //Setting this to true will prevent the script from invoking rules_core
        int bEventHandled = EngineConstants.FALSE;

        // PrxEvent log spam
#if DEBUG
        if (nEventType != EngineConstants.EVENT_TYPE_HEARTBEAT2)
            engine.Log_Events("", ev);
#endif

        switch (nEventType)
        {
            // ---------------------------------------------------------------------
            // Fired by engine when creature is spawned.
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_SPAWN:
                {
                    // Only do this once...
                    if (engine.GetLocalInt(gameObject, EngineConstants.CREATURE_SPAWNED) == EngineConstants.FALSE)
                    {
                        engine.SetLocalInt(gameObject, EngineConstants.CREATURE_SPAWNED, 1);
                        bEventHandled = HandleEvent_Spawn(ev);
                    }
                    break;
                }

            // ---------------------------------------------------------------------
            // Handle Inventory Added / Removed engine.Events
            //  Params:
            //      int (0) - the inventory slot the item added or removed from
            //      obj (0) - the item
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_INVENTORY_REMOVED:
            case EngineConstants.EVENT_TYPE_INVENTORY_ADDED:
                {
                    bEventHandled = HandleEvent_InventoryEvent(ev);
                    break;
                }

            // ---------------------------------------------------------------------
            // Handle Perception Disappear engine.Events
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_PERCEPTION_DISAPPEAR:
                {
                    bEventHandled = HandleEvent_PerceptionDisappear(ev);
                    break;
                }

            // -----------------------------------------------------------------
            // Damage over time tick event.
            // This is activated from engine.EffectDOT and keeps rescheduling itself
            // while DOTs are in xEffect on the creature
            // -----------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DOT_TICK:
                {
                    if (engine.IsDead() == EngineConstants.FALSE &&
                         engine.IsDying() == EngineConstants.FALSE)
                    {
                        engine.Effects_HandleCreatureDotTickEvent();
                    }

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            // ---------------------------------------------------------------------
            // @brief Heartbeat xEvent generated by engine in response to InitHeartbeat()
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_HEARTBEAT2:
                {
                    // No heartbeat for dead people
                    if (engine.IsDeadOrDying(gameObject) != EngineConstants.FALSE)
                        return;

                    // gradual mana/stamina regen in combat
                    if (engine.GetGameMode() == EngineConstants.GM_COMBAT)
                    {
                        float fCurrentManaStamina = engine.GetCurrentManaStamina(gameObject);
                        float fCurrentStaminaRegen = engine.GetCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA_COMBAT, EngineConstants.PROPERTY_VALUE_BASE);
                        float fNewStaminaRegen = fCurrentStaminaRegen;
                        if (fCurrentManaStamina <= 25.0f) // fastest regen
                            fNewStaminaRegen = EngineConstants.REGENERATION_STAMINA_COMBAT_DEFAULT + 3.5f;
                        else if (fCurrentManaStamina <= 50.0f) // mid regen
                            fNewStaminaRegen = EngineConstants.REGENERATION_STAMINA_COMBAT_DEFAULT + 1.0f;

                        else // more than 50 -> slowest regen
                            fNewStaminaRegen = EngineConstants.REGENERATION_STAMINA_COMBAT_DEFAULT + 0.5f;

                        engine.SetCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA_COMBAT, fNewStaminaRegen);
                    }

                    // Check for traps
                    engine.Trap_RunDetectionPulse(gameObject);

                    // //Track movements for stats
                    //  if (engine.IsHero(gameObject)) //STATS_TrackWalkedDistance();

                    // ----------------------------------------------------------------
                    // Generate SkyNet Position //Tracking engine.Event
                    // http://georg/SkyNetWeb - Talk to georg if you have questions
                    // Note: For development telemetry only - will not work in SHIP exectuables.
                    // ----------------------------------------------------------------
#if SKYNET
             if (engine.IsHero(gameObject))
             {
                //TrackPos();
             }
#endif

                    if (EngineConstants.LOG_ENABLED == EngineConstants.TRUE)
                    {
                        if (engine.IsImmortal(gameObject) != EngineConstants.FALSE)
                        {
                            xCommand cCommand = engine.GetCurrentCommand(gameObject);
                            if (engine.GetCommandType(cCommand) != 38) // death blow xCommand (engine turns follower immortal during death blows)
                            {
                                //   Warning ("Warning: " + engine.ToString(gameObject) + " seems to be immortal, which is probably a bug. Hero tag: " + engine.GetTag(engine.GetHero())+"Please file a bug through SkyNet to Yaron");
                                engine.DEBUG_PrintToScreen("Warning: PC GameObject " + engine.ToString(gameObject) + " is immortal!", 15 + engine.Engine_Random(2), 2.0f);
                            }
                        }
                        engine.DEBUG_PrintToScreen("Difficulty " + engine.ToString(engine.GetGameDifficulty()) + "", 11, 2.0f);
                    }

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            // -----------------------------------------------------------------
            // Legacy Heartbeat event. Left for the consumption of modders.
            // Be careful with it, it's not nice to run on a lot of creatures...
            // -----------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_HEARTBEAT:
                {
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            case EngineConstants.EVENT_TYPE_EQUIP:
                {
                    bEventHandled = HandleEvent_Equip(ev);
                    break;
                }

            case EngineConstants.EVENT_TYPE_UNEQUIP:
                {
                    bEventHandled = HandleEvent_UnEquip(ev);
                    break;
                }

            // ---------------------------------------------------------------------
            // Fires first time a party member is added to the party
            // For plot followers: follower recruited (added to pool)
            // For other followers: UT_Hire called
            // Owner: Yaron
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED:
                {
                    int nScaled = engine.GetLocalInt(gameObject, EngineConstants.FOLLOWER_SCALED);
                    int nShowPartyPicker = engine.GetEventIntegerRef(ref ev, 0);
                    int nMinLevel = engine.GetEventIntegerRef(ref ev, 1);
                    int bPreventLevelup = engine.GetEventIntegerRef(ref ev, 2);

#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                        "show party picker: " + engine.IntToString(nShowPartyPicker));
#endif

                    int bSummoned = engine.IsSummoned(gameObject);

                    // -----------------------------------------------------------------
                    // @author georg Initialize Follower Heartbeat.
                    // Note: This is terminated in EngineConstants.EVENT_TYPE_PARTY_MEMBER_FIRED.
                    // -----------------------------------------------------------------
                    if (bSummoned == EngineConstants.FALSE)
                    {
                        // Heartbeat check moved to engine.WR_SetFollowerState
                        //InitHeartbeat(gameObject, EngineConstants.CONFIG_CONSTANT_HEARTBEAT_RATE);

                        // checking tactics presets
                        // It is fine to do this more than once
                        engine.Chargen_EnableTacticsPresets(gameObject);
                    }

                    // -----------------------------------------------------------------
                    // @author yaron
                    // This can fire only once - when first hired
                    // -----------------------------------------------------------------
                    if (nScaled == EngineConstants.FALSE && bSummoned == EngineConstants.FALSE && engine.IsHero(gameObject) == EngineConstants.FALSE)
                    {
                        engine.SetLocalInt(gameObject, EngineConstants.FOLLOWER_SCALED, 1);
                        int nPackage = engine.GetPackage(gameObject);
                        int nPackageClass = engine.GetM2DAInt(EngineConstants.TABLE_PACKAGES, "StartingClass", nPackage);

                        // set behavior
                        int nBehavior = engine.GetM2DAInt(EngineConstants.TABLE_PACKAGES, "FollowerBehavior", nPackage);
                        if (nBehavior >= 0)
                            engine.SetAIBehavior(gameObject, nBehavior);

                        // -------------------------------------------------------------
                        // <scaling>
                        //
                        // NOTE: creature was scaled already in creature_core - in here
                        // we clear him completely and reconstruct from scratch
                        // -------------------------------------------------------------
                        engine.Chargen_InitializeCharacter(gameObject, EngineConstants.TRUE);

                        // -------------------------------------------------------------
                        // Apply race and class modifiers.
                        // -------------------------------------------------------------
                        engine.Chargen_SelectRace(gameObject, engine.GetCreatureRacialType(gameObject));
                        engine.Chargen_SelectCoreClass(gameObject, engine.GetCreatureCoreClass(gameObject));

                        // -------------------------------------------------------------
                        // yaron: Scale followers to level.
                        // -------------------------------------------------------------
                        int nTargetLevel;
                        int nPlayerLevel = engine.GetLevel(engine.GetHero());
                        if (nPlayerLevel >= 13 || nPlayerLevel == 1 ||
                             engine._UT_GetIsPlotFollower(gameObject) == EngineConstants.FALSE)
                            nTargetLevel = nPlayerLevel;
                        else
                            nTargetLevel = nPlayerLevel + 1;
                        nMinLevel = engine.GetM2DAInt(EngineConstants.TABLE_PACKAGES, "MinLevel", nPackage);
                        if (nMinLevel > 0 && nMinLevel > nTargetLevel)
                            nTargetLevel = nMinLevel;
#if DEBUG
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                "Target level: " + engine.IntToString(nTargetLevel));
#endif

                        if (nPackageClass != EngineConstants.CLASS_MONSTER_ANIMAL)
                        {
                            // -------------------------------------------------------------
                            // Follower leveled one level higher than the player unless the player
                            // is too high level.
                            // -------------------------------------------------------------

                            int nXp = engine.RW_GetXPNeededForLevel(engine.Max(nTargetLevel, 1));

#if DEBUG
                            engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                    "Giving EngineConstants.XP: " + engine.IntToString(nXp));
#endif

                            int nState = engine.GetFollowerState(gameObject);
                            string sFollowerState = engine._GetFollowerStateName(nState);

#if DEBUG
                            engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                    "Follower state: " + sFollowerState);
#endif
                            engine.RewardXP(gameObject, nXp, EngineConstants.FALSE, EngineConstants.FALSE);
                        }

                        // -------------------------------------------------------------
                        // add hidden approval talents
                        // -------------------------------------------------------------
                        int nIndex = engine.Approval_GetFollowerIndex(gameObject);
                        engine.Approval_AddFollowerBonusAbility(nIndex, 0);

                        // Find specialization
                        int nSpecAbility = engine.GetM2DAInt(EngineConstants.TABLE_PACKAGES, "switch1_class", nPackage); // followers can have only 1 advanced class
                        if (nSpecAbility > 0)
                        {
#if DEBUG
                            engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                "Adding spec ability: " + engine.IntToString(nSpecAbility));
#endif
                            engine.AddAbility(gameObject, nSpecAbility);
                        }

                        // -------------------------------------------------------------
                        // This spends all available attribute and stat points on the
                        // creature according to the levelup table.
                        // -------------------------------------------------------------

                        engine.AL_DoAutoLevelUp(gameObject, EngineConstants.TRUE);

                        if (bPreventLevelup != EngineConstants.FALSE)
                        {
#if DEBUG
                            engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                "Preventing creature from levelling up");
#endif
                            engine.SetLocalInt(gameObject, EngineConstants.CREATURE_REWARD_FLAGS, 1);
                        }

                        // load tactics
                        int nTableID = engine.GetM2DAInt(EngineConstants.TABLE_PACKAGES, "FollowerTacticsTable", nPackage);
                        if (nTableID != -1)
                        {
#if DEBUG
                            engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                "Loading follower tactics from table: " + engine.IntToString(nTableID));
#endif
                            int nRows = engine.GetM2DARows(nTableID);
                            int nMaxTactics = engine.GetNumTactics(gameObject);
#if DEBUG
                            engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                "Loading follower tactics from table: " + engine.IntToString(nTableID) + ", row: " + engine.IntToString(nRows));
#endif

                            int nTacticsEntry = 1;
                            int i;
                            for (i = 1; i <= nRows && nTacticsEntry <= nMaxTactics; ++i)
                            {
                                int bAddEntry = EngineConstants.FALSE;
                                int nTargetType = engine.GetM2DAInt(nTableID, "TargetType", i);
                                int nCondition = engine.GetM2DAInt(nTableID, "Condition", i);
                                int nCommandType = engine.GetM2DAInt(nTableID, "Command", i);
                                int nCommandParam = engine.GetM2DAInt(nTableID, "SubCommand", i);

#if DEBUG
                                engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                    "adding tactics: " + engine.IntToString(i));
#endif
                                int nUseType = engine.GetM2DAInt(EngineConstants.TABLE_COMMAND_TYPES, "UseType", nCommandType);
                                if (nUseType == 0)
                                {
                                    bAddEntry = EngineConstants.TRUE;
                                }
                                else
                                {
                                    bAddEntry = engine.HasAbility(gameObject, nCommandParam);
                                }

                                if (bAddEntry != EngineConstants.FALSE)
                                {
                                    engine.SetTacticEntry(gameObject, nTacticsEntry, EngineConstants.TRUE, nTargetType, nCondition, nCommandType, nCommandParam);
                                    ++nTacticsEntry;
                                }
                            }
                        }

                        // @author yaron
                        // DEBUG - scale items
#if DEBUG
                        if (engine.GetLocalInt(engine.GetModule(), EngineConstants.DEBUG_ENABLE_PARTY_ITEM_SCALING) == 1)
                        {

                            engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "player_core.EngineConstants.EVENT_TYPE_PARTY_MEMBER_HIRED",
                                "DEBUG - scaling items - THIS CODE SHOULD NOT RUN NORMALLY");
                            engine.DEBUG_ScaleFolloweItems(gameObject);
                        }
#endif
                        //if this is Alistair - show the tutorial
                        if (engine.GetTag(gameObject) == "gen00fl_alistair")
                        {
                            engine.BeginTrainingMode(EngineConstants.TRAINING_SESSION_FOLLOWERS_AND_TACTICS);
                        }
                    }

                    if (nShowPartyPicker != EngineConstants.FALSE &&
                         engine.GetLocalInt(engine.GetArea(gameObject), EngineConstants.AREA_DEBUG) == EngineConstants.FALSE)
                    {
                        engine.SetPartyPickerGUIStatus(EngineConstants.PP_GUI_STATUS_USE);
                        engine.ShowPartyPickerGUI();
                    }

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            // ---------------------------------------------------------------------
            // Fires an active or locked-active party member is removed from the
            // active party
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_PARTY_MEMBER_FIRED:
                {
                    // NOTE: this xEvent actually does not fire in many cases
                    // follower-fired code in better put in engine.WR_SetFollowerState

                    break;
                }

            // ---------------------------------------------------------------------
            // Sent by engine when henchman or player is selected.
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_ON_SELECT:
                {
                    engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_SELECTED);
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            // ---------------------------------------------------------------------
            // Sent by engine when henchman or player is given an order.
            // ---------------------------------------------------------------------
            /*        case EngineConstants.EVENT_TYPE_ON_ORDER_RECEIVED:
                    {
                        engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_ORDER_RECEIVED,GetEventTargetRef(ref ev));
                        bEventHandled = EngineConstants.TRUE;
                        break;
                    }*/
            case 94: /*EngineConstants.EVENT_TYPE_PLAYER_COMMAND_ADDED:*/
                {
                    engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_ORDER_RECEIVED, engine.GetEventTargetRef(ref ev), engine.GetEventIntegerRef(ref ev, 0));
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            // ---------------------------------------------------------------------
            // Sent by engine when creature is killed.
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DEATH:
                {
                    bEventHandled = HandleEvent_Death(ev);
                    break;
                }

            // ---------------------------------------------------------------------
            // Resurrection timer used if a creature dies in explore mode.
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_PARTY_MEMBER_RES_TIMER:
                {
                    if (engine.GetGameMode() == EngineConstants.GM_EXPLORE)
                    {
                        engine.ResurrectCreature(gameObject);
                    }
                    break;
                }

            // ---------------------------------------------------------------------
            // Creature is resurrected. Fired by effect_resurrection.OnApply
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_RESURRECTION:
                {
                    bEventHandled = HandleEvent_Resurrection(gameObject, ev);
                    break;
                }

            // ---------------------------------------------------------------------
            // Creature is spawned. Fired by sys_rewards_h.RewardXP
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_PLAYER_LEVELUP:
                {
#if SKYNET
            //TrackPartyMemberEvent(nEventType, gameObject, null, engine.GetLevel(gameObject));
#endif

                    engine.UI_DisplayMessage(gameObject, EngineConstants.UI_MESSAGE_LEVELUP);
                    break;
                }

            case EngineConstants.EVENT_TYPE_LOAD_TACTICS_PRESET:
                {
                    bEventHandled = HandleEvent_LoadTactics(gameObject, ev);
                    break;
                }

            //----------------------------------------------------------------------
            // Sent by engine when player clicks on object.
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_PLACEABLE_ONCLICK:
                {
                    // Pass xEvent along to the placeable being clicked on.
                    engine.SignalEvent(engine.GetEventTargetRef(ref ev), ev);
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            case EngineConstants.EVENT_TYPE_USE_ABILITY_IMMEDIATE:
                {
                    bEventHandled = HandleEvent_UseAbilityImmediate(gameObject, ev);
                    break;
                }

        }

        if (bEventHandled == EngineConstants.FALSE)
        {
            engine.Warning("event not handled in player core, redirecting to rules core!");
            engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_RULES_CORE);
        }
    }
}