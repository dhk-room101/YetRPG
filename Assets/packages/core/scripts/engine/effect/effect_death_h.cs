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
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"2da_constants_h"
     ////#include"wrappers_h"
     //#include"events_h"
     //#include"effect_constants_h"
     //#include"achievement_core_h"
     //#include"stats_core_h"
     /* ----------------------------------------------------------------------------
     * @brief Returns an xEffect which kills a creature.
     *
     * Constructor for the death effect. When applied to a creature, this effect
     * instantly kills the creature. If the creature is flagged as plot or immortal
     * then this xEffect will do nothing.
     *
     * @author Georg Zoeller
     *
     * @param bForceDeath - Force death regardless of death status. Used only OnSpawn
     *
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_DEATH
     *
     /
     ------------------------------------------------------------------------------*/

     public xEffect EffectDeath(int bForceDeath = 0)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_DEATH);
          SetEffectIntegerRef(ref eEffect, 0, 1);
          return eEffect;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the application of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectDeath(xEffect eEffect, GameObject oContext = null)
     {
          if (oContext == null) oContext = gameObject;
          GameObject oTarget = oContext;
          GameObject oKiller = GetEffectCreatorRef(ref eEffect);
          xCommand pAnimationCommand;
          int nType = GetObjectType(oTarget);

          int bForceDeath = GetEffectIntegerRef(ref eEffect, 0);
          int bReturn = EngineConstants.FALSE;

          // -------------------------------------------------------------------------
          // NOTE: IsDead is actually an engine check that checks if the creature
          // has 0 HP or lower - it is no real flag.
          // -------------------------------------------------------------------------
          if ((IsPlot(oTarget) == EngineConstants.FALSE) && (IsImmortal(oTarget) == EngineConstants.FALSE))
          {

               // ---------------------------------------------------------------------
               // PLC Death
               // ---------------------------------------------------------------------
               if (nType == EngineConstants.OBJECT_TYPE_PLACEABLE)
               {

                    // -----------------------------------------------------------------
                    // Sent the OnDeathEvent to the target
                    // -----------------------------------------------------------------
                    SendEventOnDeath(oTarget, oKiller);

                    // -----------------------------------------------------------------
                    // Actually killing the target (by setting Health to 0)
                    // -----------------------------------------------------------------
                    SetPlaceableHealth(oTarget, 0); // Same as setting target to being 'dead'

                    Log_Trace_Combat("effect_death_h.Effects_HandleApplyEffectDeath", "Placeable killed", oKiller, oTarget, EngineConstants.LOG_CHANNEL_COMBAT_DEATH);

               }
               // ---------------------------------------------------------------------
               // Creature Death
               // ---------------------------------------------------------------------
               else if (nType == EngineConstants.OBJECT_TYPE_CREATURE)
               {

                    if (HasDeathEffect(oTarget) != EngineConstants.FALSE)
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DEATH, "effect_death_h", "Not killing already dying creature", oTarget);
                         return EngineConstants.FALSE;
                    }
                    //if (GetCreatureFlag(oTarget,EngineConstants.CREATURE_RULES_FLAG_DYING))
                    //{
                    //    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DEATH, "effect_death_h", "Not killing already dying creature", oTarget);
                    //    return EngineConstants.FALSE;
                    //}

                    bReturn = EngineConstants.TRUE;

                    // -----------------------------------------------------------------
                    // NOTE: the death reaction for synched death (death blow) is handled
                    // during the Rules_CalculateDamage function. This is needed because
                    // synched reactions must be played immediately, but any other reaction
                    // can be waited for the death xEffect to handle it.
                    // -----------------------------------------------------------------

                    // (Georg) Ghosts just avoid the whole chain and disintegrate right away
                    if (HasAbility(oTarget, EngineConstants.ABILITY_TRAIT_GHOST) != EngineConstants.FALSE)
                    {

                         Log_Trace_Combat("effect_death_h.Effects_HandleApplyEffectDeath", "Target killed - Ghost", oKiller, oTarget, EngineConstants.LOG_CHANNEL_COMBAT_DEATH);

                         SendEventDying(oTarget, oKiller);
                         ApplyEffectVisualEffect(oTarget, oTarget, EngineConstants.VFX_IMP_DEATH, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         SendEventOnDeath(oTarget, oKiller);
                         return EngineConstants.TRUE;
                    }

                    Log_Trace_Combat("effect_death_h.Effects_HandleApplyEffectDeath", "Target killed", oKiller, oTarget, EngineConstants.LOG_CHANNEL_COMBAT_DEATH);

                    SetCreatureFlag(oTarget, EngineConstants.CREATURE_RULES_FLAG_DYING, EngineConstants.TRUE);

                    /*if (IsSummoned(oTarget))
                    {
                        DestroyObject(oTarget,1000);
                    } */

                    // remove all effects applied ON me
                    Effects_RemoveAllEffects(oTarget, EngineConstants.TRUE, EngineConstants.TRUE);

                    // -----------------------------------------------------------------
                    // <Achievement entry point>
                    // -----------------------------------------------------------------
                    ACH_HandleDeathEffect(oTarget, oKiller, eEffect);
                    // -----------------------------------------------------------------
                    // <Achievement entry point/>
                    // -----------------------------------------------------------------

                    // Stats - count party kills
                    STATS_HandlePartyKills(oKiller, oTarget);

                    // Stats - handle "most powerful slain"
                    //STATS_TrackMostPowerfulSlain(oKiller, oTarget);

                    // Stats - handle "number of creatures of type killed"
                    STATS_CountKillsByGroupType(oKiller, oTarget);

                    // -----------------------------------------------------------------
                    // Set Mana / Stamina to 0 on death.
                    // -----------------------------------------------------------------
                    SetCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, 0.0f, EngineConstants.PROPERTY_VALUE_CURRENT);

                    // -----------------------------------------------------------------
                    // Slayers with the death blow talent gain 15% of enemy health back in mana
                    // -----------------------------------------------------------------
                    if (IsObjectValid(oKiller) != EngineConstants.FALSE && 
                         (HasAbility(oKiller, EngineConstants.ABILITY_TALENT_DEATH_BLOW)) != EngineConstants.FALSE)
                    {
                         //float fMana     = GetCreatureProperty(oKiller, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA);
                         float fRestore = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, EngineConstants.PROPERTY_VALUE_BASE) * 0.2f;
                         UpdateCreatureProperty(oKiller, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fRestore, EngineConstants.PROPERTY_VALUE_CURRENT);
                         DEBUG_PrintToScreen(ToString(fRestore));
                         ApplyEffectVisualEffect(oKiller, oKiller, 90184, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);

                    }

                    SendEventDying(oTarget, oKiller);

               }

          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_death_h.Effects_HandleApplyEffectDeath", "GameObject is PLOT or IMMORTANT - NOT applying death effect", oTarget);
          }

          return bReturn;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectDeath(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }

     /* @brief (effect_death_h) Kills a creature.
     *
     * Kills a creature by creating and applying a death effect.
     * - Does nothing if applied to an already dead creature.
     * - Does nothing on immortal or plot flagged creatures
     *
     * @param oTarget    - The unfortunate creature to die.
     * @param oKiller    - The killer (null for creatures killed by plot).
     * @param nAbilityId - If used from an ability script, pass in stEvent.nAbility.
     */
     public void KillCreature(GameObject oVictim, GameObject oKiller = null, int nAbilityId = 0, int bProcessInline = EngineConstants.FALSE, int nDamage = 0)
     {

          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DEATH, "effect_death_h.KillCreature", "victim:" + ToString(oVictim) + ", killer: " + ToString(oKiller));

          if (IsObjectValid(oVictim) != EngineConstants.FALSE && GetObjectType(oVictim) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               if (IsDead(oVictim) == EngineConstants.FALSE)
               {
                    if (IsPlot(oVictim) == EngineConstants.FALSE && IsImmortal(oVictim) == EngineConstants.FALSE)
                    {

                         xEffect eDeath = EffectDeath();
                    SetEffectCreatorRef(ref eDeath, oKiller);
                    SetEffectAbilityIDRef(ref eDeath, nAbilityId);
                    // This introduced a bug with death animations. We are temporarily commenting it out (Jose)
                    SetEffectIntegerRef(ref eDeath, EngineConstants.ACH_EFFECT_DEATH_DAMAGE_INDEX, nDamage);    // EffectDeath already records information on Integer Index 0, so we use Integer Index 1 to store the damage.
                                                                                                                       //                Effects_HandleApplyEffectDeath(eDeath, oVictim);
                         if (bProcessInline == EngineConstants.FALSE)
                         {
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_DEATH, eDeath, oVictim, 0.0f, oKiller, nAbilityId);
                         }
                         else
                         {
                              Effects_HandleApplyEffectDeath(eDeath, oVictim);
                         }
                    }
               }
          }
     }

     /* @brief (effect_death_h) Destroys a placeable
     *
     * Destroys a placeable by creating and applying a death effect.
     * - Does nothing if applied to an already dead destroyed object.
     * - Does nothing on immortal or plot flagged objects
     *
     * @param oPlaceable  - The placeable to be destroyed
     * @param oDestroyer  - The killer (null for creatures killed by plot).
     * @param nAbilityId  - If used from an ability script, pass in stEvent.nAbility.
     */
     public void DestroyPlaceable(GameObject oPlaceable, GameObject oDestroyer = null, int nAbilityId = 0)
     {
          if (IsObjectValid(oPlaceable) != EngineConstants.FALSE && GetObjectType(oPlaceable) == EngineConstants.OBJECT_TYPE_PLACEABLE)
          {
               if (IsDead(oPlaceable) == EngineConstants.FALSE)
               {
                    if (IsPlot(oPlaceable) == EngineConstants.FALSE && IsImmortal(oPlaceable) == EngineConstants.FALSE)
                    {
                         xEffect eDeath = EffectDeath();
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eDeath, oPlaceable, 0.0f, oDestroyer, nAbilityId);

                         //SetPlaceableActionResult(oPlaceable, EngineConstants.PLACEABLE_ACTION_DESTROY, EngineConstants.TRUE);
                    }
               }
          }
     }
}