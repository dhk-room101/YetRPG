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

         A Paragon of Her Kind
          -> Broodmother Include Script

     */
     //------------------------------------------------------------------------------
     // Created By: joshua
     // Created On: February 19, 2009
     //==============================================================================

     //#include"utility_h"
     //#include"events_h"
     //#include"cai_h"
     //#include"orz_constants_h"

     //------------------------------------------------------------------------------
     // Constants
     //------------------------------------------------------------------------------

     //moved public const int       EngineConstants.BROODMOTHER_EVENT_STOP_WAITING                  = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;
     //moved public const int       EngineConstants.BROODMOTHER_EVENT_TENTACLE_SURFACE              = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_02;
     //moved public const int       EngineConstants.BROODMOTHER_EVENT_TENTACLE_RESET                = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_03;
     //moved public const int       EngineConstants.BROODMOTHER_CAI_REINFORCEMENTS_FIRST_WAVE       = 1000;
     //moved public const int       EngineConstants.BROODMOTHER_CAI_REINFORCEMENTS_FIRST_WAVE_2     = 1001;
     //moved public const int       EngineConstants.BROODMOTHER_CAI_REINFORCEMENTS_SECOND_WAVE      = 1010;
     //moved public const int       EngineConstants.BROODMOTHER_CAI_REINFORCEMENTS_SECOND_WAVE_2    = 1011;
     //moved public const int       EngineConstants.BROODMOTHER_CAI_START                           = 1020;
     //moved public const int       EngineConstants.BROODMOTHER_CAI_WAITING                         = 1030;
     //moved public const int       EngineConstants.BROODMOTHER_CAI_TENTACLE_BURROW                 = 1040;
     //moved public const int       EngineConstants.BROODMOTHER_CAI_TENTACLE_SURFACE                = 1041;

     //moved public const float     EngineConstants.BROODMOTHER_WAVE_1_HPP                          = 0.66f;
     //moved public const float     EngineConstants.BROODMOTHER_WAVE_2_HPP                          = 0.33f;

     //moved public const float     EngineConstants.BROODMOTHER_TENTACLE_MIN_ATTACK_DIST            = 1.9f;
     //moved public const float     EngineConstants.BROODMOTHER_TENTACLE_MIN_NEW_TARGET_DIST        = 2.5f;
     //moved public const float     EngineConstants.BROODMOTHER_TENTACLE_WAVE_RESET_DELAY           = 30.0f;
     //moved public const float     EngineConstants.BROODMOTHER_TENTACLE_WAVE_RESET_DELAY_OFFSET    = 1.0f;
     //moved public const float     EngineConstants.BROODMOTHER_TENTACLE_BURROW_DELAY               = 2.5f;
     //moved public const float     EngineConstants.BROODMOTHER_TENTACLE_BURROW_RESET_DELAY         = 7.5f;
     //moved public const int       EngineConstants.BROODMOTHER_TENTACLE_NUM_TENTACLES              = 4;
     //moved public const string    EngineConstants.BROODMOTHER_TENTACLE_WP                         = "wp_broodmother_tentacle";


     //------------------------------------------------------------------------------
     // Function Implementation
     //------------------------------------------------------------------------------

     public int ORZ_Broodmother_Tentacle_IsEventValid(xEvent ev, GameObject oTentacle = null)
     {
          if (oTentacle == null) oTentacle = gameObject;
          GameObject oBroodmother = UT_GetNearestObjectByTag(oTentacle, EngineConstants.ORZ_CR_BROODMOTHER);
          if (GetEventIntegerRef(ref ev, 0) == GetLocalInt(oBroodmother, EngineConstants.CREATURE_COUNTER_1))
               return EngineConstants.TRUE;
          return EngineConstants.FALSE;
     }

     //------------------------------------------------------------------------------

     public void ORZ_Broodmother_Tentacle_LeaveCombat(GameObject oTentacle = null)
     {
          if (oTentacle == null) oTentacle = gameObject;
          // Stop Party members from attacking tentacle and remove all party-given
          // effects from the tentacles
          List<GameObject> arParty = GetPartyList();
          int i, size = GetArraySize(arParty);
          for (i = 0; i < size; i++)
          {
               if (GetAttackTarget(arParty[i]) == gameObject)
                    WR_ClearAllCommands(arParty[i], EngineConstants.TRUE);
               RemoveEffectsByParameters(gameObject, EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.ABILITY_INVALID, arParty[i]);
          }
          // Hack: dots don't seem to be helping here, so just hard clear them.
          RemoveEffectsByParameters(oTentacle, EngineConstants.EFFECT_TYPE_DOT);
          SetCreatureFlag(oTentacle, EngineConstants.CREATURE_RULES_FLAG_DOT, EngineConstants.FALSE);

          CAI_SetCustomAI(oTentacle, EngineConstants.CAI_STASIS);
          CAI_SetCustomAIObject(oTentacle, null);
          WR_SetObjectActive(oTentacle, EngineConstants.FALSE);
          SetPlot(oTentacle, EngineConstants.TRUE);
     }

     //------------------------------------------------------------------------------

     public void ORZ_Broodmother_Tentacle_GetTargetAndSurface(xEvent ev, GameObject oTentacle = null)
     {
          if (oTentacle == null) oTentacle = gameObject;
          int nTentacleNum = GetLocalInt(oTentacle, EngineConstants.CREATURE_COUNTER_1);
          GameObject oObject = GetEventObjectRef(ref ev, 0);
          GameObject oTarget;

          // check if the tentacle is going to the broodmother or to attack the party
          if (oObject == oTentacle)
               oTarget = UT_GetNearestObjectByTag(oTentacle, EngineConstants.BROODMOTHER_TENTACLE_WP + "_" + ToString(nTentacleNum));
          else
               oTarget = ORZ_Broodmother_Tentacle_SelectTarget();

          UT_CombatStart(oTentacle, GetHero());
          SetPosition(oTentacle, GetPosition(oTarget));
          WR_SetObjectActive(oTentacle, EngineConstants.TRUE);
          SetPlot(oTentacle, EngineConstants.FALSE);
          CAI_SetCustomAIObject(oTentacle, oTarget);
          CAI_SetCustomAI(oTentacle, EngineConstants.BROODMOTHER_EVENT_TENTACLE_SURFACE);
          WR_AddCommand(oTentacle, CommandAttack(UT_GetNearestHostileCreature(oTentacle)), EngineConstants.TRUE, EngineConstants.TRUE);

     }

     //------------------------------------------------------------------------------

     public int ORZ_Broodmother_Tentacle_NewTargetRequired(GameObject oTentacle = null)
     {
          if (oTentacle == null) oTentacle = gameObject;
          GameObject oNearestHostile = UT_GetNearestHostileCreature(oTentacle, EngineConstants.TRUE);
          if ((GetDistanceBetween(oTentacle, oNearestHostile) > EngineConstants.BROODMOTHER_TENTACLE_MIN_NEW_TARGET_DIST) &&
               (CAI_GetCustomAIObject(oTentacle) != ORZ_Broodmother_Tentacle_SelectTarget(oTentacle)))
          {
               return EngineConstants.TRUE;
          }
          return EngineConstants.FALSE;
     }

     //------------------------------------------------------------------------------

     public void ORZ_Broodmother_PlaySoundSet(GameObject oBroodmother = null, int nSoundSet = EngineConstants.SS_SCRIPTED_HELP, int nAnim = 100)
     {
          if (oBroodmother == null) oBroodmother = gameObject;
          // Soundset, Animation
          PlaySoundSet(oBroodmother, nSoundSet, 1.0f);
          WR_AddCommand(oBroodmother, CommandPlayAnimation(nAnim, 0, 0, 0), EngineConstants.TRUE, EngineConstants.TRUE);
     }

     //------------------------------------------------------------------------------

     public void ORZ_Broodmother_ForceScream(GameObject oBroodmother = null)
     {
          if (oBroodmother == null) oBroodmother = gameObject;
          xEffect effRestoreManaStam = EffectModifyManaStamina(Ability_GetAbilityCost(gameObject, EngineConstants.ABILITY_TALENT_BROODMOTHER_SCREAM));
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, effRestoreManaStam, gameObject);
          SetCooldown(gameObject, EngineConstants.ABILITY_TALENT_BROODMOTHER_SCREAM, 0.0f);
          Ability_UseAbilityWrapper(gameObject, EngineConstants.ABILITY_TALENT_BROODMOTHER_SCREAM, gameObject, EngineConstants.TRUE, EngineConstants.TRUE);
     }

     //------------------------------------------------------------------------------

     public void ORZ_Broodmother_Tentacle_Reset(GameObject oTentacle = null)
     {
          if (oTentacle == null) oTentacle = gameObject;
          CAI_SetCustomAI(oTentacle, EngineConstants.CAI_INACTIVE);
          UT_CombatStart(oTentacle, GetHero());
     }

     //------------------------------------------------------------------------------

     public void ORZ_Broodmother_Tentacle_Burrow(int nCounter, float fSurfaceOverride = EngineConstants.BROODMOTHER_TENTACLE_BURROW_DELAY, float fResetOverride = EngineConstants.BROODMOTHER_TENTACLE_BURROW_RESET_DELAY, GameObject oTargetOverride = null, GameObject oTentacle = null)
     {
          if (oTentacle == null) oTentacle = gameObject;
          xEvent ev;
          ev = Event(EngineConstants.BROODMOTHER_EVENT_TENTACLE_SURFACE);
          SetEventIntegerRef(ref ev, 0, nCounter);
          SetEventObjectRef(ref ev, 0, oTargetOverride);
          DelayEvent(fSurfaceOverride, oTentacle, ev);

          ev = Event(EngineConstants.BROODMOTHER_EVENT_TENTACLE_RESET);
          SetEventIntegerRef(ref ev, 0, nCounter);
          DelayEvent(fResetOverride, oTentacle, ev);

          List<GameObject> arParty = GetPartyList();
          int i, size = GetArraySize(arParty);
          for (i = 0; i < size; i++)
          {
               if (GetAttackTarget(arParty[i]) == gameObject)
                    WR_ClearAllCommands(arParty[i]);
          }
          WR_SetObjectActive(oTentacle, EngineConstants.FALSE);
          RemoveEffectsDueToPlotEvent(oTentacle);
          CAI_SetCustomAI(oTentacle, EngineConstants.BROODMOTHER_EVENT_TENTACLE_SURFACE);
     }

     //------------------------------------------------------------------------------

     public GameObject ORZ_Broodmother_Tentacle_SelectTarget(GameObject oTentacle = null)
     {
          if (oTentacle == null) oTentacle = gameObject;
          int bFound = EngineConstants.FALSE;
          int nNum = GetLocalInt(oTentacle, EngineConstants.CREATURE_COUNTER_1);
          float fMin = 9999.99f;
          List<GameObject> arParty = GetPartyList();
          List<GameObject> arTeam = UT_GetTeam(EngineConstants.ORZ_TEAM_BROODMOTHER_TENTACLES);
          int size, i, k;
          float fDist;
          GameObject oWP = null, oTarget, oMin = null;
          List<GameObject> arNearestWPs;

          // loop through each tentacle and get cai targets
          size = GetArraySize(arTeam);
          for (i = 0; i < size; i++)
          {
               if (arTeam[i] != oTentacle)
                    arTeam[i] = CAI_GetCustomAIObject(arTeam[i]);
          }
          // loop through party members and decide on a new target
          size = GetArraySize(arParty);
          for (i = 0; i < size; i++)
          {
               // don't target dead people plz
               oTarget = arParty[((i + nNum) % size)];
               if (IsDead(oTarget) != EngineConstants.FALSE)
                    continue;
               // grab 4 nearest waypoints to the target, take first unused one
               arNearestWPs = GetNearestObjectByTag(oTarget, EngineConstants.BROODMOTHER_TENTACLE_WP, EngineConstants.OBJECT_TYPE_WAYPOINT, EngineConstants.BROODMOTHER_TENTACLE_NUM_TENTACLES);
               for (k = 0; k < EngineConstants.BROODMOTHER_TENTACLE_NUM_TENTACLES; k++)
               {
                    oWP = arNearestWPs[k];
                    // make sure no other tentacles are here.
                    if (!(oWP == arTeam[0] || oWP == arTeam[1] || oWP == arTeam[2] || oWP == arTeam[3]))
                         break;
               }
               // check whether this waypoint is within acceptable distance limits
               // if it is:    sweet
               // if it isn't: keep tally of our closest target
               fDist = GetDistanceBetween(oWP, oTarget);
               if (fDist <= EngineConstants.BROODMOTHER_TENTACLE_MIN_ATTACK_DIST)
               {
                    bFound = EngineConstants.TRUE;
                    break;
               }
               else
               {
                    if (fDist < fMin)
                    {
                         fMin = fDist;
                         oMin = oWP;
                    }
               }
          }
          if (bFound == EngineConstants.FALSE)
               oWP = oMin;
          return oWP;
     }

     //------------------------------------------------------------------------------

     public void ORZ_Broodmother_RetractTentacles(float fResetDelay = EngineConstants.BROODMOTHER_TENTACLE_WAVE_RESET_DELAY, GameObject oBroodmother = null)
     {
          if (oBroodmother == null) oBroodmother = gameObject;
          List<GameObject> arTentacles = UT_GetTeam(EngineConstants.ORZ_TEAM_BROODMOTHER_TENTACLES);
          int nCounter = GetLocalInt(oBroodmother, EngineConstants.CREATURE_COUNTER_1);
          int size, i;

          size = GetArraySize(arTentacles);
          for (i = 0; i < size; i++)
          {
               RemoveEffectsDueToPlotEvent(arTentacles[i]);
               ORZ_Broodmother_Tentacle_Burrow(nCounter,
                   (EngineConstants.BROODMOTHER_TENTACLE_BURROW_DELAY + IntToFloat(i)),
                   (EngineConstants.BROODMOTHER_TENTACLE_WAVE_RESET_DELAY + (IntToFloat(i) * EngineConstants.BROODMOTHER_TENTACLE_WAVE_RESET_DELAY_OFFSET)),
                    arTentacles[i], arTentacles[i]);
          }

          xEvent ev = Event(EngineConstants.BROODMOTHER_EVENT_STOP_WAITING);
          DelayEvent(EngineConstants.BROODMOTHER_TENTACLE_WAVE_RESET_DELAY, oBroodmother, ev);
     }

     //------------------------------------------------------------------------------

     public void ORZ_Broodmother_AchievementStart()
     {
     }

     public void ORZ_Broodmother_AchievementCheck()
     {
     }
}