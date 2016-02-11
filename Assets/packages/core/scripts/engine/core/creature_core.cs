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

public class creature_core : MonoBehaviour
{
    // -----------------------------------------------------------------------------
    // creature_core
    // -----------------------------------------------------------------------------
    /*

        engine.Handles AI Creature (NPC) events.

        Follower/Player events are handled in player_core.

        Note: this script redirects all events not handled into rules_core.

    */
    // -----------------------------------------------------------------------------
    // Owner: Yaron Jakobs, Georg Zoeller
    // -----------------------------------------------------------------------------

    // -----------------------------------------------------------------------------
    //
    //             L I M I T E D   E D I T   P E R M I S S I O N S
    //
    //      If you are not Georg or Yaron, you need permission to edit this file
    //      and the changes have to be code reviewed before they are checked
    //      in.
    //
    // -----------------------------------------------------------------------------

    //#include"rules_h"
    //#include"log_h"
    //#include"utility_h"
    //#include"global_objects_h"
    //#include"ai_main_h_2"
    //#include"ai_ballista_h"
    //#include"design_tracking_h"
    //#include"sys_rewards_h"
    //#include"sys_autoscale_h"
    //#include"sys_rubberband_h"
    //#include"sys_soundset_h"
    //#include"sys_ambient_h"
    //#include"sys_treasure_h"
    //#include"sys_areabalance"
    //#include"aoe_effects_h"

    Engine engine { get; set; }
    xGameObjectBase oBase;
    int counter;

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
        oBase = gameObject.GetComponent<xGameObjectBase>();
        /*xEvent _event = engine.Event(EngineConstants.EVENT_TYPE_SPAWN);
        engine.SetEventObjectRef(ref _event, 0, gameObject);
        _event.scriptname = engine.GetScriptName(this.ToString());
        engine.SignalEvent(gameObject, _event);*/
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

    public void EquipDefaultItem(int nAppType, int nTargetSlot, string s2daColumnName)
    {
        // Equip default creature item based on appearance (only if none equipped yet)
        GameObject oItem = engine.GetItemInEquipSlot(nTargetSlot, gameObject);
        string rDefaultCreatureItem;
        GameObject oDefaultCreatureoItem;
        string sItem;
        if (engine.IsObjectValid(oItem) == EngineConstants.FALSE)
        {
            rDefaultCreatureItem = engine.GetM2DAResource(EngineConstants.TABLE_APPEARANCE, s2daColumnName, nAppType);
            sItem = engine.ResourceToString(rDefaultCreatureItem);
#if DEBUG
            engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "creature_core.EngineConstants.EVENT_TYPE_SPAWN", "Default creature item for this appearance: [" + sItem + "] for inventory slot: " +
                engine.IntToString(nTargetSlot));
#endif
            if (sItem != "")
            {
                oDefaultCreatureoItem = engine.CreateItemOnObject(rDefaultCreatureItem, gameObject);
                engine.SetItemDroppable(oDefaultCreatureoItem, EngineConstants.FALSE);
                if (engine.IsObjectValid(oDefaultCreatureoItem) != EngineConstants.FALSE)
                {
#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "creature_core.EngineConstants.EVENT_TYPE_SPAWN", "Creating default item on creature");
#endif
                    engine.EquipItem(gameObject, oDefaultCreatureoItem, nTargetSlot);
                }
            }
        }
    }

    public void HandleEvent()
    {
        xEvent ev = engine.GetCurrentEvent();
        int nEventType = engine.GetEventTypeRef(ref ev);

        // If EngineConstants.TRUE, we won't redirect to rules core
        int bEventHandled = EngineConstants.FALSE;

#if DEBUG
        engine.Log_Events("", ev);
#endif

        if (engine.IsPartyMember(gameObject) != EngineConstants.FALSE)
        {
            engine.Warning("Party member " +
                 engine.ToString(gameObject) +
                 " firing events into creature_core. This is a critical bug. Please inform Yaron");
            engine.HandleEventRef(ref ev, "rules_core.ncs"); // fix it
            return;
        }

        switch (nEventType)
        {
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

                    // Event was handled - don't fall through to rules_core
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            case EngineConstants.EVENT_TYPE_PERCEPTION_DISAPPEAR:
                {
                    // A creature exits the perception area of creature receiving the event

                    GameObject oDisappearer = engine.GetEventObjectRef(ref ev, 0); //GetEventCreatorRef(ref ev);
                    if (engine.IsObjectHostile(oDisappearer, gameObject) != EngineConstants.FALSE)
                    {
                        engine.Combat_HandleCreatureDisappear(gameObject, oDisappearer);
                    }

                    // Event was handled - don't fall through to rules_core
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            //----------------------------------------------------------------------
            // EngineConstants.EVENT_TYPE_DAMAGED
            //
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DAMAGED:
                {
                    GameObject oThis = gameObject;

                    // temp randomizer, this will happen in the engine at some point.
                    /*            if (Random(5) == 1)
                                {
                                    PlaySoundSet(oThis,EngineConstants.SS_COMBAT_PAIN_GRUNT);
                                }*/

                    // Check surrender conditions
                    if (engine.GetLocalInt(oThis, "SURR_SURRENDER_ENABLED") != EngineConstants.FALSE)
                    {
                        if (engine.IsDead(oThis) == EngineConstants.FALSE &&
                             engine.GetCurrentHealth(oThis) < 2.0f)
                        {
                            engine.UT_Surrender(oThis);
                        }
                    }
                    break;
                }

            // ---------------------------------------------------------------------
            // EngineConstants.EVENT_TYPE_DYING - Received on the creature getting the killing blow
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DYING:
                {
                    GameObject oKiller = engine.GetEventObjectRef(ref ev, 0);

#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "creature_core.EngineConstants.EVENT_TYPE_DYING", "Killer: " + engine.ToString(oKiller));
#endif

                    // Dispense death rewards.
                    if (engine.IsObjectValid(oKiller) != EngineConstants.FALSE &&
                         engine.IsPartyMember(oKiller) != EngineConstants.FALSE)
                    {
                        engine.RewardXPParty(0, EngineConstants.XP_TYPE_COMBAT, gameObject, oKiller);
                        // if this creature is a combatant, pass the xEvent to the treasure function
                        if (engine.GetCombatantType(gameObject) != EngineConstants.CREATURE_TYPE_NON_COMBATANT)
                        {
                            engine.HandleEventRef(ref ev, "sys_treasure.ncs");
                        }
                    }

                    // Last words
                    engine.SSPlaySituationalSound(gameObject, EngineConstants.SOUND_SITUATION_DYING);

                    bEventHandled = EngineConstants.TRUE;
                    break;
                }
            // ---------------------------------------------------------------------
            // EngineConstants.EVENT_TYPE_DEATH:
            // The death xEffect has been applied to this creature, either by losing hit points
            // or by explicit calling of the effect
            // ---------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_DEATH:
                {
#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "creature_core.EngineConstants.EVENT_TYPE_DEATH", "received death event", gameObject);
#endif

                    engine.SetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_DYING, EngineConstants.FALSE);

                    // -------------------------------------------------------------
                    // Clear the object's perception list
                    // -------------------------------------------------------------
                    engine.ClearPerceptionList(gameObject);

                    GameObject oKiller = engine.GetEventCreatorRef(ref ev);
#if DEBUG
                    engine.Log_Trace_Combat("creature_core.EngineConstants.EVENT_TYPE_DEATH", "creature received death event", oKiller, gameObject);
#endif

                    // -----------------------------------------------------------------
                    // Clear any registed AI waypoints
                    // -----------------------------------------------------------------
                    GameObject oAIWP = engine.GetLocalObject(gameObject, EngineConstants.AI_REGISTERED_WP);
                    if (engine.IsObjectValid(oAIWP) != EngineConstants.FALSE)
                    {
                        engine.SetTag(oAIWP, EngineConstants.AI_WP_MOVE);
                        engine.SetLocalObject(gameObject, EngineConstants.AI_REGISTERED_WP, null);
                    }

                    // -----------------------------------------------------------------
                    // Killer may play a victory sound...
                    // -----------------------------------------------------------------
                    engine.SSPlaySituationalSound(oKiller, EngineConstants.SOUND_SITUATION_ENEMY_KILLED);

                    if (engine.GetCanDiePermanently(gameObject) != EngineConstants.FALSE)
                    {

                        if (engine.HasAbility(gameObject, 450000) != EngineConstants.FALSE)
                        {
                            engine.Effect_DoOnDeathExplosion(gameObject, EngineConstants.TRUE, 4.0f, 110109, EngineConstants.DAMAGE_TYPE_PHYSICAL, 0.0f, 0.25f);

                            engine.SetObjectActive(gameObject, 0, 0, 0);
                            engine.Safe_Destroy_Object(gameObject, 1500);
                        }
                        else if (engine.HasAbility(gameObject, EngineConstants.ABILITY_TRAIT_EXPLOSIVE) != EngineConstants.FALSE)
                        {
                            engine.Effect_DoOnDeathExplosion(gameObject, EngineConstants.TRUE, 4.0f, 93091, EngineConstants.DAMAGE_TYPE_FIRE);

                            engine.SetObjectActive(gameObject, 0, 0, 0);// 93091);
                            engine.Safe_Destroy_Object(gameObject, 1500);
                        }
                        else if (engine.HasAbility(gameObject, EngineConstants.ABILITY_TRAIT_GHOST) != EngineConstants.FALSE)
                        {
                            engine.Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, engine.EffectVisualEffect(93095), engine.GetLocation(gameObject), 0.0f, gameObject, 0);
                            engine.SetObjectActive(gameObject, 0, 0, 0);
                            engine.Safe_Destroy_Object(gameObject, 1500);
                        }
                        else if (engine.IsSummoned(gameObject) != EngineConstants.FALSE)
                        {
                            //PATL: Fallback: Looks bad but gets the job done.
                            //SetObjectActive(gameObject, 0, 0,0);
                            //Safe_Destroy_Object(gameObject, 1500);

                            // PATL - Insert favorite creature unsummon fun here.
                            engine.SpawnBodyBag(gameObject, EngineConstants.TRUE);
                            GameObject oBodyBag = engine.GetCreatureBodyBag(gameObject);

                            if (oBodyBag != null)
                            {
                                engine.SetBodybagDecayDelay(oBodyBag, 3000);
                                engine.SetObjectInteractive(oBodyBag, EngineConstants.FALSE);
                                engine.SetObjectActive(oBodyBag, 0, 0, 0);
                                DestroyObject(oBodyBag, 20000);
                            }
                        }
                        else
                        {
                            engine.SpawnBodyBag(gameObject);

                            if (engine.Engine_Random(EngineConstants.CONFIG_CONSTANT_BLOODPOOL_FREQ) + 1 < 100 || engine.GetCreatureRank(gameObject) == EngineConstants.CREATURE_RANK_BOSS)
                            {
                                int nAppr = engine.GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "bloodpool", engine.GetAppearanceType(gameObject));

                                int nBloodPoolVfx = 0;
                                switch (nAppr)
                                {
                                    case 0: break;
                                    case 1: nBloodPoolVfx = EngineConstants.VFX_GROUND_BLOODPOOL_S; break;
                                    case 2: nBloodPoolVfx = EngineConstants.VFX_GROUND_BLOODPOOL_M; break;
                                    case 3: nBloodPoolVfx = EngineConstants.VFX_GROUND_BLOODPOOL_L; break;
                                }

                                if (nBloodPoolVfx > 0)
                                {
                                    float fDur = (nAppr == 3) ? 600.0f : engine.RandFF(EngineConstants.CONFIG_CONSTANT_BLOODPOOL_DURATION, EngineConstants.CONFIG_CONSTANT_BLOODPOOL_DURATION);
                                    engine.Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, engine.EffectVisualEffect(nBloodPoolVfx), engine.GetLocation(gameObject), fDur, engine.GetArea(gameObject));
                                }

                            }

                        }
                    }
                    else if ((engine.HasAbility(gameObject, EngineConstants.ABILITY_TRAIT_EXPLOSIVE) == EngineConstants.FALSE) && (engine.HasAbility(gameObject, EngineConstants.ABILITY_TRAIT_GHOST) == EngineConstants.FALSE))
                    {
                        engine.SpawnBodyBag(gameObject);

                        if (engine.Engine_Random(EngineConstants.CONFIG_CONSTANT_BLOODPOOL_FREQ) + 1 < 100 || engine.GetCreatureRank(gameObject) == EngineConstants.CREATURE_RANK_BOSS)
                        {
                            int nAppr = engine.GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "bloodpool", engine.GetAppearanceType(gameObject));

                            int nBloodPoolVfx = 0;
                            switch (nAppr)
                            {
                                case 0: break;
                                case 1: nBloodPoolVfx = EngineConstants.VFX_GROUND_BLOODPOOL_S; break;
                                case 2: nBloodPoolVfx = EngineConstants.VFX_GROUND_BLOODPOOL_M; break;
                                case 3: nBloodPoolVfx = EngineConstants.VFX_GROUND_BLOODPOOL_L; break;
                            }

                            if (nBloodPoolVfx > 0)
                            {
                                float fDur = (nAppr == 3) ? 600.0f : engine.RandFF(EngineConstants.CONFIG_CONSTANT_BLOODPOOL_DURATION, EngineConstants.CONFIG_CONSTANT_BLOODPOOL_DURATION);
                                engine.Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, engine.EffectVisualEffect(nBloodPoolVfx), engine.GetLocation(gameObject), fDur, engine.GetArea(gameObject));
                            }

                        }

                    }
#if SKYNET
            //TrackObjectDeath(ev);
#endif

                    engine.AI_Threat_UpdateDeath(gameObject);

                    // Team handling: fire team destroyed xEvent if all team members are dead
                    int nTeamID = engine.GetTeamId(gameObject);
                    if (nTeamID != -1)
                    {
                        engine.SetTeamId(gameObject, -1); // this de-registers the creature from the team
                        List<GameObject> arTeam = engine.GetTeam(nTeamID);
                        int nSize = engine.GetArraySize(arTeam);
#if DEBUG
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "creature_core.EngineConstants.EVENT_TYPE_DEATH", "Handling death of team member, team ID: " + engine.IntToString(nTeamID) + ", # of team members left: " + engine.IntToString(nSize), gameObject);
#endif
                        if (nSize == 0) // no more team members
                        {
                            // send team death xEvent to self
                            engine.SendEventTeamDestroyed(engine.GetArea(gameObject), nTeamID);
                        }
                    }

                    engine.AI_Ballista_HandleDeath();

                    // Codex
                    if (engine.GetLocalInt(gameObject, EngineConstants.CREATURE_SPAWN_DEAD) == 0 && engine.GetLocalInt(gameObject, EngineConstants.SPAWN_HOSTILE_LYING_ON_GROUND) == 0)
                    {
                        int nApp = engine.GetAppearanceType(gameObject);
                        string sCodexPlot = engine.GetM2DAString(EngineConstants.TABLE_APPEARANCE, "CodexPlot", nApp);
                        int nCodexFlag = engine.GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "CodexFlag", nApp);
                        //string sCodexCounter = engine.GetM2DAString(EngineConstants.TABLE_APPEARANCE, "CodexCounter", nApp);
                        if (sCodexPlot != "")
                        {
                            string sCodexGUID = engine.GetPlotGUID(sCodexPlot);
                            if (sCodexGUID != "")
                            {
                                engine.WR_SetPlotFlag(sCodexGUID, nCodexFlag, EngineConstants.TRUE);
                            }
                        }
                    }
                    break;
                }

            case EngineConstants.EVENT_TYPE_SPAWN:
                {
                    // Georg: Not needed anymore but left for backwards compat reasons for now
                    if (engine.GetLocalInt(gameObject, EngineConstants.CREATURE_SPAWNED) != EngineConstants.FALSE)
                    {
#if DEBUG
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "creature_core.EngineConstants.EVENT_TYPE_SPAWN", "Creature spawned before - NOT triggering spawn routine again.");
#endif
                        break;
                    }
                    engine.SetLocalInt(gameObject, EngineConstants.CREATURE_SPAWNED, 1);

                    // -----------------------------------------------------------------
                    // Creatures spawning with 0 health will die instantly
                    // This is handled in engine, the script here just ensures
                    // some gore on them
                    // ------------------------------------------------------------------
                    if (engine.GetLocalInt(gameObject, EngineConstants.CREATURE_SPAWN_DEAD) == 1)
                    {

                        float fRandGore = engine.RandomFloat();
#if DEBUG
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "creature_core.EngineConstants.EVENT_TYPE_SPAWN", "gore: " + engine.FloatToString(fRandGore));
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "creature_core.EngineConstants.EVENT_TYPE_SPAWN", "Character spawned with EngineConstants.CREATURE_SPAWN_DEAD set, killing...", gameObject);
#endif
                        engine.SetCreatureGoreLevel(gameObject, fRandGore);
                        bEventHandled = EngineConstants.TRUE;
                        return;
                    }

                    // -----------------------------------------------------------------
                    // engine.Handle OnSpawn appearance based crust specified in apr_base
                    // -----------------------------------------------------------------
                    int nAppType = engine.GetAppearanceType(gameObject);
                    int nCrustEffect = engine.GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "CRUST_EFFECT", nAppType);
                    if (nCrustEffect > 0)
                    {
                        // -------------------------------------------------------------
                        // Georg: I am using ENGINE_INNATE here to apply the effect
                        //        permanently, unremovable
                        // -------------------------------------------------------------
                        engine.ApplyEffectVisualEffect(gameObject, gameObject, nCrustEffect, 5 /*EngineConstants.EFFECT_DURATION_TYPE_INNATE*/, 0.0f);
                    }

                    // -----------------------------------------------------------------
                    // One-hit-kill check (read area value from 2da)
                    // One-Hit rank creatures, in special areas, run specific, limited
                    // AI for mass battles.
                    // -----------------------------------------------------------------
                    int nAreaID = engine.GetLocalInt(engine.GetArea(gameObject), EngineConstants.AREA_ID);
                    int nOneHitKillArea = engine.GetM2DAInt(EngineConstants.TABLE_AREA_DATA, "OneHitKillArea", nAreaID);
                    if ((nAppType == EngineConstants.APP_TYPE_HURLOCK ||
                         nAppType == EngineConstants.APP_TYPE_GENLOCK ||
                         nAppType == 74 /* blight wolf*/) &&
                         nOneHitKillArea != EngineConstants.FALSE)
                    {
#if DEBUG
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "creature_core.EngineConstants.EVENT_TYPE_SPAWN", "Hurlock/Genlock appearance in a one-hit-kill area - changing rank to one-hit-kill");
#endif
                        engine.SetCreatureRank(gameObject, EngineConstants.CREATURE_RANK_ONE_HIT_KILL);
                    }

                    // -----------------------------------------------------------------
                    // Large creatures that can bump smaller ones out of their way
                    // -----------------------------------------------------------------

                    if (nAppType == EngineConstants.APR_TYPE_OGRE || nAppType == EngineConstants.APP_TYPE_PRIDE_DEMON)
                        engine.ApplyKnockbackAoe(gameObject);

                    // -----------------------------------------------------------------
                    // Support for Yaron's ambush type monsters that rise from the ground
                    // -----------------------------------------------------------------
                    if (engine.GetLocalInt(gameObject, EngineConstants.SPAWN_HOSTILE_LYING_ON_GROUND) == 1)
                    {
#if DEBUG
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "creature_core.EngineConstants.EVENT_TYPE_SPAWN", "Spawning creature as lying on the ground");
#endif

                        // NOTE: the death animaiton should be handled by using the EngineConstants.CREATURE_SPAWN flag set to 2

                        // If the creature is hostile: give him a temp non-hostile group so the player won't get into
                        // combat stance when he's near. Perception will change the group again.
                        if (engine.GetGroupId(gameObject) == EngineConstants.GROUP_HOSTILE)
                            engine.SetGroupId(gameObject, EngineConstants.GROUP_HOSTILE_ON_GROUND);
                        engine.SetObjectInteractive(gameObject, EngineConstants.FALSE);

                    }

                    // Creatures with a ranged weapon in their default slot, prefer
                    // ranged weapon
                    if (engine._AI_GetWeaponSetEquipped() == EngineConstants.AI_WEAPON_SET_RANGED)
                        engine._AI_SetFlag(EngineConstants.AI_FLAG_PREFERS_RANGED, EngineConstants.TRUE);

                    // -----------------------------------------------------------------
                    // Force default item equip from the 2da
                    // -----------------------------------------------------------------
                    EquipDefaultItem(nAppType, EngineConstants.INVENTORY_SLOT_MAIN, "DefaultWeapon");
                    EquipDefaultItem(nAppType, EngineConstants.INVENTORY_SLOT_CHEST, "DefaultArmor");

                    // -----------------------------------------------------------------
                    // START AutoScaling Block
                    // Important - do not change the order of function calls in this
                    // block
                    // >>---------------------------------------------------------------

                    // Autoscaling
                    // All ranks will be scaled her, including player ranks.
                    int nLevelToScale = engine.AS_GetCreatureLevelToScale(gameObject, engine.AB_GetAreaTargetLevel(gameObject));
                    engine.AS_InitCreature(gameObject, nLevelToScale);

                    // scale items (based on current level)
                    engine.ScaleEquippedItems(gameObject, nLevelToScale);

                    // -----------------------------------------------------------------
                    // This enables spawn tracking on the db. High volume event,
                    // disabled by default
                    // Georg's SkyNet uses this to create fancy maps of where
                    // each GameObject is located in an area.
                    // -----------------------------------------------------------------
#if SKYNET
            if (TRACKING_TRACK_SPAWN_EVENTS)
            {
                //TrackCreatureEvent(nEventType, gameObject,null,GetAppearanceType(gameObject));
            }
#endif

                    // Store start Vector3 for homing "rubberband" system
                    engine.Rubber_SetHome(gameObject);

                    // Check whether ambient behaviour should start on spawn.
                    engine.Ambient_SpawnStart();

                    // -----------------------------------------------------------------
                    // If the creature has the SpawnGhost flag set,
                    // setup transparency and vfx.
                    // -----------------------------------------------------------------
                    if (engine.HasAbility(gameObject, EngineConstants.ABILITY_TRAIT_GHOST) != EngineConstants.FALSE)
                    {
                        engine.MakeCreatureGhost(gameObject, 1);
                    }

                    // If has stealth and in combat and hostile to the player then trigge stealth
                    if (engine.HasAbility(gameObject, EngineConstants.ABILITY_TALENT_STEALTH) != EngineConstants.FALSE &&
                         engine.GetCombatState(gameObject) == EngineConstants.FALSE &&
                         engine.GetGroupHostility(EngineConstants.GROUP_PC, engine.GetGroupId(gameObject)) != EngineConstants.FALSE)
                    {
#if DEBUG
                        engine.Log_Trace_AI("creature_core", "TRIGGERING STEALTH");
#endif
                        engine.Ambient_Stop(gameObject);
                        xCommand cStealth = engine.CommandUseAbility(EngineConstants.ABILITY_TALENT_STEALTH, gameObject, Vector3.zero);
                        engine.WR_AddCommand(gameObject, cStealth);

                    }

                    // -----------------------------------------------------------------
                    // Deal with creatures that start out with % of health
                    // as defined in creature var
                    // -----------------------------------------------------------------
                    float fHealthMod = engine.MinF(engine.GetLocalFloat(gameObject, EngineConstants.CREATURE_SPAWN_HEALTH_MOD), 0.9f);
                    if (fHealthMod > 0.0f)
                    {
                        float fHealth = engine.GetCurrentHealth(gameObject);
                        fHealth = engine.MinF(1.0f, fHealth * fHealthMod);
                        engine.SetCurrentHealth(gameObject, fHealth);
                    }

                    // -----------------------------------------------------------------
                    // Creature with roaming enabled.
                    // -----------------------------------------------------------------           //
                    float fRoamDistance = engine.GetLocalFloat(gameObject, "ROAM_DISTANCE");
                    if (fRoamDistance > 25.0f)
                    {
                        //Vector3 lRoamLocation = Location(engine.GetArea(gameObject), engine.GetPosition(gameObject), 0.0f);
                        Vector3 lRoamLocation = engine.GetPosition(gameObject);

                        engine.SetRoamLocation(gameObject, lRoamLocation);
                        engine.SetRoamRadius(gameObject, fRoamDistance);
                    }

                    // Event was handled - don't fall through to rules_core
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            //----------------------------------------------------------------------
            // EngineConstants.EVENT_TYPE_COMBAT_END
            // Sent when a creature doesn't perceive any more hostiles
            //----------------------------------------------------------------------
            case EngineConstants.EVENT_TYPE_COMBAT_END:
                {

                    GameObject oThis = gameObject;

                    //------------------------------------------------------------------
                    // If the creature is set to surrender have them initiate dialog
                    // - Grant, Oct 30, 2007
                    //------------------------------------------------------------------
                    if (engine.GetLocalInt(oThis, EngineConstants.SURR_SURRENDER_ENABLED) != EngineConstants.FALSE &&
                         engine.IsPartyDead() == EngineConstants.FALSE)
                    {

                        // Verify that combat is ending due to creature surrender.
                        if (engine.GetLocalInt(oThis, EngineConstants.SURR_SURRENDER_ENABLED) == EngineConstants.SURR_STATUS_ACTIVE)
                        {

                            // engine.Set plot flag and trigger post-surrender dialog
                            string sPlotName = engine.GetLocalString(oThis, EngineConstants.SURR_PLOT_NAME);

                            if (sPlotName != "")
                            {

                                int nPlotFlag = engine.GetLocalInt(oThis, EngineConstants.SURR_PLOT_FLAG);

                                engine.WR_SetPlotFlag(sPlotName, nPlotFlag, EngineConstants.TRUE, EngineConstants.TRUE);

                            }

                            int bInit = engine.GetLocalInt(oThis, EngineConstants.SURR_INIT_CONVERSATION);

                            if (bInit != EngineConstants.FALSE)
                            {
                                engine.UT_Talk(oThis, engine.GetHero());
                                engine.SetLocalInt(oThis, EngineConstants.SURR_INIT_CONVERSATION, EngineConstants.FALSE);
                            }

                            // Reset the surrender flag to enabled status.
                            engine.SetLocalInt(oThis, EngineConstants.SURR_SURRENDER_ENABLED, EngineConstants.TRUE);

                        } // Verify

                    }

                    // Return to home location.
                    engine.Rubber_GoHome(gameObject);
                    break;

                }

            case EngineConstants.EVENT_TYPE_RESURRECTION:
                {
                    // -------------------------------------------------------------
                    // Event handled, do not fall through to rules_core
                    // -------------------------------------------------------------
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            case EngineConstants.EVENT_TYPE_DELAYED_SHOUT:
                {
#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS, "creature_core", "DELAYED SHOUT EVENT START", gameObject, EngineConstants.LOG_LEVEL_CRITICAL);
#endif

                    // Triggering a shouts dialog with a delay
                    int nShoutsActive = engine.GetLocalInt(gameObject, EngineConstants.SHOUTS_ACTIVE);
                    string rDialogOverride = engine.GetLocalResource(gameObject, EngineConstants.SHOUTS_CONVERSATION_OVERRIDE);
                    float fDelay = engine.GetLocalFloat(gameObject, EngineConstants.SHOUTS_DELAY);
                    if (fDelay < 3.0f)
                        fDelay = 3.0f; // so it doesn't happen too often

                    if (nShoutsActive == EngineConstants.FALSE)
                        break;

                    string rDialog = "";
                    if (rDialogOverride != "NONE")
                        rDialog = rDialogOverride;

#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "creature_core_h.EngineConstants.EVENT_TYPE_DELAYED_SHOUT", "delay= " + engine.FloatToString(fDelay) + ", dialog override: " +
                        engine.ResourceToString(rDialogOverride));
#endif

                    // Check perception list: if no party members are in it - stop shouting
                    List<GameObject> arPerceived = engine.GetPerceivedCreatureList(gameObject);
                    int nSize = engine.GetArraySize(arPerceived);
                    int i;
                    GameObject oCurrent;
                    int bPerceingPartyMembers = EngineConstants.FALSE;

                    for (i = 0; i < nSize; i++)
                    {
                        oCurrent = arPerceived[i];
                        if (engine.IsFollower(oCurrent) != EngineConstants.FALSE)
                        {
                            bPerceingPartyMembers = EngineConstants.TRUE;
                            break;
                        }

                    }

                    if (bPerceingPartyMembers == EngineConstants.FALSE)
                    {
#if DEBUG
                        engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "creature_core_h.EngineConstants.EVENT_TYPE_DELAYED_SHOUT", "Not perceiving any party members - stopping shouts");
#endif
                        engine.UT_SetShoutsFlag(gameObject, EngineConstants.FALSE); // This will break the delayed xEvent loop
                    }
                    else
                    {
                        engine.UT_Talk(gameObject, gameObject, rDialog);
                        engine.DelayEvent(fDelay, gameObject, ev);
                    }

                    // -------------------------------------------------------------
                    // Event handled, do not fall through to rules_core
                    // -------------------------------------------------------------
                    bEventHandled = EngineConstants.TRUE;
                    break;
                }

            case EngineConstants.EVENT_TYPE_REACHED_WAYPOINT:
                {
                    bEventHandled = engine.Ambient_ReachedWaypoint(ev);
                    break;
                }

            case EngineConstants.EVENT_TYPE_AMBIENT_CONTINUE:
                {
                    // If the xEvent was fired because the party is near a creature,
                    // the 'instigator' is the nearest party member. If the xEvent was
                    // fired at the end of a conversation, the 'instigator' is the creature
                    // conversing.
                    GameObject oInstigator = engine.GetEventObjectRef(ref ev, 0);
                    engine.Ambient_DoSomething(gameObject, engine.IsObjectValid(oInstigator));
                    bEventHandled = EngineConstants.TRUE;
                    return;
                }

            case EngineConstants.EVENT_TYPE_APPROACH_TRAP:
                {
                    GameObject oTrap = engine.GetEventObjectRef(ref ev, 0);
#if DEBUG
                    engine.Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "creature_core_h.EngineConstants.EVENT_TYPE_APPROACH_TRAP", "Got approach-trap event: " + engine.GetTag(oTrap));
#endif

                    if (engine.GetCombatState(gameObject) == EngineConstants.FALSE)
                    {
                        if (engine.GetObjectActive(gameObject) == EngineConstants.FALSE)
                            engine.WR_SetObjectActive(gameObject, EngineConstants.TRUE);

                        engine.WR_ClearAllCommands(gameObject, EngineConstants.TRUE);
                        xCommand cMove = engine.CommandMoveToObject(oTrap, EngineConstants.TRUE);
                        engine.WR_AddCommand(gameObject, cMove);
                    }
                    break;
                }

            case EngineConstants.EVENT_TYPE_ROAM_DIST_EXCEEDED:
                {
                    //DisplayFloatyMessage(gameObject,"GOING HOME!");

                    engine.ClearAllCommands(gameObject, EngineConstants.TRUE);

                    Vector3 lRoamLocation = engine.GetRoamLocation(gameObject);

                    xEffect e = engine.EffectModifyMovementSpeed(1.5f);
                    engine.ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, e, gameObject, 0.0f, gameObject, 0);

                    xCommand cmd = engine.CommandMoveToLocation(lRoamLocation, EngineConstants.TRUE, EngineConstants.FALSE);
                    engine.AddCommand(gameObject, cmd, EngineConstants.TRUE, EngineConstants.FALSE);
                    break;
                }

        }

        // -------------------------------------------------------------------------
        // Any xEvent not handled falls through to rules_core:
        // -------------------------------------------------------------------------
        if (bEventHandled == EngineConstants.FALSE)
        {
            //engine.Warning("event not handled in Creature core, redirecting to rules core!");
            //engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_RULES_CORE);
            gameObject.GetComponent<rules_core>().HandleEvent(ev);
        }

        //Outside the switch loop, assuming a break
        //In case the event was actually redirected, giveback control to the custom script
        if (oBase.bRedirected == EngineConstants.TRUE)
        {
            oBase.bRedirected = EngineConstants.FALSE;
        }

    }
}