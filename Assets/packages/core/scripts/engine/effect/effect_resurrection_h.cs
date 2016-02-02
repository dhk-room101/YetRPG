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
     // effect_resurrection_h
     // -----------------------------------------------------------------------------
     /*
         Effect to revive a dead creature.
         'Dead' is defined as health <= 0
     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"2da_constants_h"
     //#include"wrappers_h"
     //#include"events_h"
     //#include"effect_constants_h"

     // -----------------------------------------------------------------------------
     // This defines the distance the player has to have to his party members
     // before they resurrect after combat.
     //
     // Put it too long and people will stand up and get whacked down by monsters
     // because the player lost perception to them
     //
     // Put it too short and it gets tedious.
     // -----------------------------------------------------------------------------
     //moved public const float EngineConstants.RULES_RESURRECTION_DISTANCE = 7.5f;

     public void HandlePlayerRessurect(GameObject oCreature, int bApplyInjury = EngineConstants.TRUE)
     {
          // --------------------------------------------------------------------
          // Restart AI level after it was frozen...
          // --------------------------------------------------------------------
          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "HandleEvent_Resurrection", "Forcing AI level normal, then unlocking it!");

          // Restore AI level on target.
          SetAILevel(oCreature, EngineConstants.CSERVERAIMASTER_AI_LEVEL_INVALID);
          SetCreatureFlag(oCreature, EngineConstants.CREATURE_RULES_FLAG_DYING, EngineConstants.FALSE);
          // SetCreatureGoreLevel(oCreature, 0.0f);

          xEvent evRezz = Event(EngineConstants.EVENT_TYPE_RESURRECTION);
          SetEventIntegerRef(ref evRezz, 0, bApplyInjury);

          // -------------------------------------------------------------------------
          // Resurrection comes with a free, 25% heal
          // -------------------------------------------------------------------------
          float fHealth = MaxF(1.0f, GetMaxHealth(oCreature) * 0.25f);
          SetCurrentHealth(oCreature, fHealth);

          SignalEvent(oCreature, evRezz);
     }

     /* ----------------------------------------------------------------------------
     @brief Returns an xEffect which restores a dead creature to life.
     *
     * Constructor for the resurrection effect. When applied to a dead creature, this
     * xEffect instantly restores the creature to life with 1 health. If the creature
     * is alive then this xEffect does nothing.
     *
     * @author David Sims
     *
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_RESURRECTION.
     -----------------------------------------------------------------------------**/
     public xEffect EffectResurrection(int bApplyInjury = EngineConstants.FALSE, int bHealFull = EngineConstants.FALSE)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_RESURRECTION);
          SetEffectIntegerRef(ref eEffect, 0, bApplyInjury);
          SetEffectIntegerRef(ref eEffect, 1, bHealFull);
          return eEffect;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the application of EffectResurrection and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectResurrection(xEffect eEffect, GameObject oTarget = null)
     {
          if (oTarget == null) oTarget = gameObject;
          int bHealFull = GetEffectIntegerRef(ref eEffect, 1);

          // -------------------------------------------------------------------------
          // Onle dead creatures can be resurrected.
          // -------------------------------------------------------------------------
          if (IsDead(oTarget) != EngineConstants.FALSE)
          {

               if (bHealFull == EngineConstants.FALSE)
               {
                    SetCurrentHealth(oTarget, 1.00f);
               }
               else
               {
                    SetCurrentHealth(oTarget, GetMaxHealth(oTarget));
               }

               // flag injury system

               HandlePlayerRessurect(oTarget, GetEffectIntegerRef(ref eEffect, 0));

          }

          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of EffectResurrection and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectResurrection(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }

     public void ResurrectPartyMembers(int bHealFull = EngineConstants.FALSE)
     {

          xEffect eRez = EffectResurrection(EngineConstants.TRUE, bHealFull);
          SetEffectCreatorRef(ref eRez, GetModule());

          List<GameObject> partyMembers = GetPartyList();
          int nMembers = GetArraySize(partyMembers);
          int i;

          for (i = 0; i < nMembers; i++)
          {
               if (IsDead(partyMembers[i]) != EngineConstants.FALSE)
               {
                    Effects_HandleApplyEffectResurrection(eRez, partyMembers[i]);
               }
          }
     }

     public void ResurrectCreature(GameObject oCreature, int bHealFull = EngineConstants.FALSE)
     {

          xEffect eRez = EffectResurrection(EngineConstants.TRUE, bHealFull);

          if (IsDead(oCreature) != EngineConstants.FALSE)
          {
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eRez, oCreature, 0.0f, gameObject);
          }

     }

     public int CheckForDeadPartyMembers()
     {

          List<GameObject> party = GetPartyList();
          int nArraySize = GetArraySize(party);
          int i;
          for (i = 0; i < nArraySize; i++)
          {
               if (IsDead(party[i]) != EngineConstants.FALSE)
               {
                    return EngineConstants.TRUE;
               }
          }
          return EngineConstants.FALSE;
     }

     /* ----------------------------------------------------------------------------
     @brief Checks all party members if resurrection is possible and ressurects them
     *
     * Function is semi-recursive, don't pass a scope to perform full party check.
     * Talk to Georg before using.
     *
     * Rules for resurrection:
     *
     *   Party Member must be dead (duh)
     *   Another Party member must be with in EngineConstants.RULES_RESURRECTION_DISTANCE
     *
     @return EngineConstants.TRUE only if ALL party members were resurrected succesfully.
     @author Georg Zoeller
     -----------------------------------------------------------------------------**/
     public int CheckResurrection(GameObject oScope = null)
     {

          // -------------------------------------------------------------------------
          // No scope, check all party members
          // -------------------------------------------------------------------------
          if (oScope == null)
          {
               List<GameObject> aParty = GetPartyList();
               int nSize = GetArraySize(aParty);
               int i = 0;
               int bResult = EngineConstants.TRUE;
               GameObject oTmp;
               for (i = 0; i < nSize; i++)
               {
                    oTmp = aParty[i];
                    // -----------------------------------------------------------------
                    // For each dead (and valid) party member, check resurrection
                    // -----------------------------------------------------------------
                    if (IsObjectValid(oTmp) != EngineConstants.FALSE && IsDead(oTmp) != EngineConstants.FALSE)
                    {
                         int tResult = CheckResurrection(oTmp);
                         bResult = bResult != EngineConstants.FALSE && tResult != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;
                    }
               }
               return bResult;
          }
          // -------------------------------------------------------------------------
          // oScope provided, check if oScope can rezz
          // -------------------------------------------------------------------------
          else
          {
               int bResult = EngineConstants.FALSE;
               List<GameObject> aParty = GetPartyList();
               int nSize = GetArraySize(aParty);
               int i = 0;
               GameObject oTmp;

               if (GetGameMode() != EngineConstants.GM_EXPLORE && IsNoExploreArea() != EngineConstants.FALSE)
                    return EngineConstants.FALSE; // ressurection loop can not succeed unless we are in explore (for some specific areas...)

               for (i = 0; i < nSize; i++)
               {
                    oTmp = aParty[i];

                    // -----------------------------------------------------------------
                    // Check every non dead party member that is not identical to
                    // oScope and see if they are within resurrection distance.
                    // -----------------------------------------------------------------
                    if (bResult == EngineConstants.FALSE && IsObjectValid(oTmp) != EngineConstants.FALSE
                         && IsDead(oTmp) == EngineConstants.FALSE && oTmp != oScope)
                    {
                         if (GetDistanceBetween(oTmp, oScope) < EngineConstants.RULES_RESURRECTION_DISTANCE)
                         {
                              // ---------------------------------------------------------
                              // They are, resurrect them right now.
                              // ---------------------------------------------------------
                              ResurrectCreature(oScope);
                              bResult = EngineConstants.TRUE;
                         }
                    }
               }
               return bResult;
          }
     }
}