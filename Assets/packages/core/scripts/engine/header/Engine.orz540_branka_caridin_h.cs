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
          -> Caridin/Branka Include Script

     */
     //------------------------------------------------------------------------------
     // Created By: joshua
     // Created On: December 18, 2008
     //==============================================================================

     //#include"utility_h"
     //#include"events_h"
     //#include"orz_constants_h"
     //#include"cai_h"

     //------------------------------------------------------------------------------
     // Constants
     //------------------------------------------------------------------------------

     //moved public const int       EngineConstants.ORZ_ANVIL_EVENT_LAVA_QUAKE              = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;
     //moved public const int       EngineConstants.ORZ_ANVIL_EVENT_ROCK_FALL               = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_03;
     //moved public const int       EngineConstants.ORZ_ANVIL_EVENT_BRANKA_ILLUSION         = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_05;
     //moved public const int       EngineConstants.ORZ_ANVIL_EVENT_ROCK_FALL_IMPACT        = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_06;

     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_ERUPTION_VFX       = 93051;
     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_EARTHQUAKE_VFX     = 1052;
     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_DOT_CRUST_VFX      = 93056;
     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_DOT_IMPACT_VFX     = 1107;
     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_ABILITY_ID         = 90153;
     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_SCREENSHAKE_ID     = 2;
     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_MAX_ERUPTIONS      = 7;
     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_NUM_GROUPS         = 5; 
     //moved public const int       EngineConstants.ORZ_ANVIL_LAVA_QUAKE_AOE_ID             = 234;
     //moved public const float     EngineConstants.ORZ_ANVIL_LAVA_QUAKE_DOT_DAMAGE         = 30.0f;
     //moved public const float     EngineConstants.ORZ_ANVIL_LAVA_QUAKE_DOT_DURATION       = 10.0f;
     //moved public const float     EngineConstants.ORZ_ANVIL_LAVA_QUAKE_GROUP_DELAY        = 15.0f;
     //moved public const float     EngineConstants.ORZ_ANVIL_LAVA_QUAKE_RADIUS             = 2.0f;

     //moved public const int       EngineConstants.ORZ_ANVIL_ROCK_FALL_PROJECTILE_ID       = 207;
     //moved public const float     EngineConstants.ORZ_ANVIL_ROCK_FALL_DAMAGE              = 50.0f;
     //moved public const float     EngineConstants.ORZ_ANVIL_ROCK_FALL_GROUP_DELAY         = 5.0f;

     //moved public const int       EngineConstants.ORZ_ANVIL_ILLUSION_COPY_SPIRIT_VFX      = 1041;
     //moved public const int       EngineConstants.ORZ_ANVIL_ILLUSION_COPY_AOE_VFX         = 1057;

     //moved public const string    EngineConstants.ORZ_ANVIL_WP_LAVA_QUAKE_PREFIX          = "wp_lava_quake_g";

     //moved public const string  rORZ_ANVIL_CR_BRANKA_ILLUSION           = "orz540cr_branka_clone.utc";

     //------------------------------------------------------------------------------
     // Function Declaration
     //------------------------------------------------------------------------------
     // Event Handler Functions

     // Local stuff, these should only be called by the functions in this script

     //------------------------------------------------------------------------------
     // Function Implementation
     //------------------------------------------------------------------------------

     public void _CreateIllusionCopy(GameObject oSpirit, Vector3 loc, xEffect eff, GameObject oCreator)
     {
          RemoveAllEffects(oSpirit, EngineConstants.FALSE, EngineConstants.TRUE);
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eff, oSpirit, 0.0f, oCreator);
          SetLocalInt(oSpirit, EngineConstants.CREATURE_COUNTER_1, 0);
          WR_AddCommand(oSpirit, CommandJumpToLocation(loc), EngineConstants.TRUE);
          WR_SetObjectActive(oSpirit, EngineConstants.TRUE);
          CAI_SetCustomAI(oSpirit, EngineConstants.CAI_INACTIVE);
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_BrankaIllusionStart()
     {
          GameObject oBranka = UT_GetNearestObjectByTag(GetHero(), EngineConstants.ORZ_CR_BRANKA);
          xEvent evEvent = Event(EngineConstants.ORZ_ANVIL_EVENT_BRANKA_ILLUSION);

          SetEventVectorRef(ref evEvent, 0, GetLocation(gameObject));
          DelayEvent(1.0f, GetArea(GetHero()), evEvent);

          List<GameObject> arParty = GetPartyList();
          int i, size = GetArraySize(arParty);
          for (i = 0; i < size; i++)
          {
               if (GetAttackTarget(arParty[i]) == oBranka)
                    WR_ClearAllCommands(arParty[i], EngineConstants.TRUE);
          }

          ORZ_BrankaCaridin_BrankaIllusionRemoveAllCopies();

          WR_ClearAllCommands(oBranka, EngineConstants.TRUE);
          UT_CombatStop(oBranka, GetHero());
          WR_SetObjectActive(oBranka, EngineConstants.FALSE);
          SetObjectInteractive(oBranka, EngineConstants.FALSE);
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_HandleEventBrankaIllusion(xEvent evEvent)
     {
          GameObject oBranka = UT_GetNearestObjectByTag(GetHero(), EngineConstants.ORZ_CR_BRANKA);
          GameObject oArea = GetArea(oBranka);
          float fFacing = GetFacing(oBranka);
          Vector3 loc = GetEventVectorRef(ref evEvent, 0);
          Vector3 vPos = GetPositionFromLocation(loc);
          Vector3 vBranka = GetPosition(oBranka);
          Vector3 vDiff = Vector(vPos.x - vBranka.x, vPos.y - vBranka.y, 0.0f);
          xEffect effMod = EffectModifyAttribute(EngineConstants.ATTRIBUTE_STR, FloatToInt(GetCreatureAttribute(oBranka, EngineConstants.ATTRIBUTE_STR) * 0.8f) * -1);
          List<GameObject> arClones = UT_GetAllObjectsInAreaByTag(EngineConstants.ORZ_CR_BRANKA_CLONE);
          List<GameObject> arParty = GetPartyList();
          int nPartySize = GetArraySize(arParty);
          List<Vector3> arLocs = new List<Vector3>();

          arLocs[0] = GetSafeLocation(GetLocation(oBranka));
          arLocs[1] = GetSafeLocation(Location(oArea, Vector(vPos.x - vDiff.y, vPos.y + vDiff.x, vPos.z + 1.0f), fFacing + 90.0f));
          arLocs[2] = GetSafeLocation(Location(oArea, Vector(vPos.x + vDiff.y, vPos.y - vDiff.x, vPos.z + 1.0f), fFacing - 90.0f));
          arLocs[3] = GetSafeLocation(Location(oArea, Vector(vPos.x + vDiff.x, vPos.y + vDiff.y, vPos.z + 1.0f), fFacing + 180.0f));



          int nBrankaLoc = Engine_Random(3) + 1;
          WR_AddCommand(oBranka, CommandJumpToLocation(arLocs[nBrankaLoc++]), EngineConstants.TRUE);
          WR_SetObjectActive(oBranka, EngineConstants.TRUE);
          UT_CombatStart(oBranka, arParty[nBrankaLoc % nPartySize], EngineConstants.TRUE);
          ApplyEffectVisualEffect(oBranka, oBranka, EngineConstants.STEALTH_IMPACT_VFX, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, 2.0f);

          int i;
          for (i = 0; i < 3; i++)
          {
               _CreateIllusionCopy(arClones[i], arLocs[(nBrankaLoc + i) % 4], effMod, oBranka);
               UT_CombatStart(arClones[i], arParty[(nBrankaLoc + i) % nPartySize], EngineConstants.TRUE);
               //WR_AddCommand(arClones[i],CommandWait(1.0f));
               //WR_AddCommand(arClones[i],CommandPlayAnimation(EngineConstants.COMBAT_ANIMATION_ENTER_BERSERK,0,0,0),EngineConstants.FALSE,EngineConstants.TRUE);  
               //ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY,EffectParalyze(),arClones[i],1.0f);
               ApplyEffectVisualEffect(oBranka, arClones[i], EngineConstants.STEALTH_IMPACT_VFX, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, 2.0f);
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_BrankaIllusionRemoveCopy(GameObject oCopy)
     {
          if (GetObjectActive(oCopy) != EngineConstants.FALSE)
          {
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(93045), GetLocation(oCopy), 2.0f);
               WR_SetObjectActive(oCopy, EngineConstants.FALSE);
               WR_ClearAllCommands(oCopy, EngineConstants.TRUE);
               RemoveAllEffects(oCopy, EngineConstants.FALSE, EngineConstants.TRUE);
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_BrankaIllusionRemoveAllCopies()
     {
          List<GameObject> arClones = UT_GetAllObjectsInAreaByTag(EngineConstants.ORZ_CR_BRANKA_CLONE);
          int i, size = GetArraySize(arClones);

          for (i = 0; i < size; i++)
               ORZ_BrankaCaridin_BrankaIllusionRemoveCopy(arClones[i]);
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_LavaQuakeStart()
     {
          xEvent evEvent = Event(EngineConstants.ORZ_ANVIL_EVENT_LAVA_QUAKE);
          SetEventIntegerRef(ref evEvent, 0, 0);
          SetEventIntegerRef(ref evEvent, 1, Engine_Random(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_NUM_GROUPS));
          SignalEvent(GetArea(GetHero()), evEvent);
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_RockFallStart()
     {
          xEvent evEvent = Event(EngineConstants.ORZ_ANVIL_EVENT_ROCK_FALL);
          SetEventIntegerRef(ref evEvent, 0, 0);
          SetEventIntegerRef(ref evEvent, 1, Engine_Random(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_NUM_GROUPS));
          SignalEvent(GetArea(GetHero()), evEvent);
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_HandleEventRockFall(xEvent evEvent)
     {
          int nLoopNum = GetEventIntegerRef(ref evEvent, 0);
          int nLoopGroup = GetEventIntegerRef(ref evEvent, 1);
          xEffect effEruption = EffectVisualEffect(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_ERUPTION_VFX);
          GameObject oBranka = GetObjectByTag(EngineConstants.ORZ_CR_BRANKA);
          List<GameObject> arTargets = GetNearestObjectByTag(oBranka,
                          EngineConstants.ORZ_ANVIL_WP_LAVA_QUAKE_PREFIX + ToString(nLoopGroup),
                          EngineConstants.OBJECT_TYPE_WAYPOINT, EngineConstants.ORZ_ANVIL_LAVA_QUAKE_MAX_ERUPTIONS);
          int size, i;
          Vector3 vTarget, vSource;
          xEvent evBrankaImpact;
          GameObject oTarget, oPrj;

          if (IsDeadOrDying(oBranka) == EngineConstants.FALSE && GetCombatState(oBranka) != EngineConstants.FALSE)
          {
               // loop through the waypoint targets
               size = GetArraySize(arTargets);
               for (i = 0; i < size; i++)
               {
                    oTarget = arTargets[i];
                    vTarget = GetPosition(oTarget);
                    // the projectile is thick, so account for it's radius for
                    // when it collides with the ground.
                    vTarget.z = vTarget.z + 0.3f;
                    // The source of the projectile is high above it, so modify
                    // the z-value
                    vSource = Vector(vTarget.x, vTarget.y, vTarget.z + 100 + Engine_Random(50));
                    // The result for this is ALWAYS sent to Branka, she handles it
                    // in her character script.
                    oPrj = FireProjectile(EngineConstants.ORZ_ANVIL_ROCK_FALL_PROJECTILE_ID, vSource, vTarget, 0, EngineConstants.FALSE, oBranka);
                    evBrankaImpact = Event(EngineConstants.ORZ_ANVIL_EVENT_ROCK_FALL_IMPACT);
                    SetEventVectorRef(ref evBrankaImpact, 0, GetLocation(oTarget));
                    SetProjectileImpactEvent(oPrj, evBrankaImpact);
               }
               // Apply an earthquake VFX and Screen-Shake
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_EARTHQUAKE_VFX), oBranka, EngineConstants.ORZ_ANVIL_ROCK_FALL_GROUP_DELAY);
               ApplyEffectOnParty(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectScreenShake(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_SCREENSHAKE_ID), 3.0f);
               // Send the xEvent back to the area
               SetEventIntegerRef(ref evEvent, 0, (nLoopNum + 1));
               SetEventIntegerRef(ref evEvent, 1, ((nLoopGroup + 1) % EngineConstants.ORZ_ANVIL_LAVA_QUAKE_NUM_GROUPS));
               DelayEvent(EngineConstants.ORZ_ANVIL_ROCK_FALL_GROUP_DELAY, gameObject, evEvent);
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_HandleEventLavaQuake(xEvent evEvent)
     {
          int nLoopNum = GetEventIntegerRef(ref evEvent, 0);
          int nLoopGroup = GetEventIntegerRef(ref evEvent, 1);
          GameObject oCaridin = GetObjectByTag(EngineConstants.ORZ_CR_CARIDIN);
          xEffect effEruption = EffectVisualEffect(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_ERUPTION_VFX);
          List<GameObject> arTargets = GetNearestObjectByTag(oCaridin,
                      EngineConstants.ORZ_ANVIL_WP_LAVA_QUAKE_PREFIX + ToString(nLoopGroup),
                      EngineConstants.OBJECT_TYPE_WAYPOINT, EngineConstants.ORZ_ANVIL_LAVA_QUAKE_MAX_ERUPTIONS);
          int nNumTargets, nNumCreatures, i, k;
          Vector3 evLocation;
          List<GameObject> arNearestCreatures;

          if (IsDeadOrDying(oCaridin) == EngineConstants.FALSE && GetCombatState(oCaridin) != EngineConstants.FALSE)
          {
               // Loop through the waypoints
               nNumTargets = GetArraySize(arTargets);
               for (i = 0; i < nNumTargets; i++)
               {
                    evLocation = GetLocation(arTargets[i]);
                    Vector3 vPos = GetPositionFromLocation(evLocation);
                    vPos.z += 0.25f;
                    SetLocationPosition(evLocation, vPos);
                    xEffect eAoE = EffectAreaOfEffect(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_AOE_ID,
                        "orz540_lava_aoe.ncs", EngineConstants.ORZ_ANVIL_LAVA_QUAKE_ERUPTION_VFX);
                    Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eAoE,
                        evLocation, EngineConstants.ORZ_ANVIL_LAVA_QUAKE_GROUP_DELAY, oCaridin, EngineConstants.ORZ_ANVIL_LAVA_QUAKE_ABILITY_ID);
               }
               // Apply an earthquake VFX and screenshake
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_EARTHQUAKE_VFX), oCaridin, 5.0f);
               ApplyEffectOnParty(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectScreenShake(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_SCREENSHAKE_ID), 3.0f);

               // Resend the xEvent back to the area, with a different
               // lava_quake group for the next round
               SetEventIntegerRef(ref evEvent, 0, (nLoopNum + 1));
               SetEventIntegerRef(ref evEvent, 1, ((nLoopGroup + 1) % EngineConstants.ORZ_ANVIL_LAVA_QUAKE_NUM_GROUPS));
               DelayEvent(EngineConstants.ORZ_ANVIL_LAVA_QUAKE_GROUP_DELAY, gameObject, evEvent);
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_HandleEventBrankaIllusionDamaged(xEvent evEvent)
     {
          int nCounter = GetLocalInt(gameObject, EngineConstants.CREATURE_COUNTER_1);
          GameObject oBranka = GetObjectByTag(EngineConstants.ORZ_CR_BRANKA);

          // Apply ghost vfx
          if (nCounter == 0)
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, EffectVisualEffect(3007), gameObject, 0.0f, oBranka);
          // Remove Copy
          else if (nCounter >= 2)
               ORZ_BrankaCaridin_BrankaIllusionRemoveCopy(gameObject);

          SetLocalInt(gameObject, EngineConstants.CREATURE_COUNTER_1, (nCounter + 1));
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_HandleEventRockFallImpact(xEvent evEvent)
     {
          Vector3 evLocation = GetEventVectorRef(ref evEvent, 0);
          GameObject oBranka = GetObjectByTag(EngineConstants.ORZ_CR_BRANKA);
          List<GameObject> arNearestCreatures = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, evLocation, 2.0f);
          int i, size = GetArraySize(arNearestCreatures);
          float fDifficultyMod = GetGameDifficulty() * 0.5f + 0.5f;
          float fDamage = EngineConstants.ORZ_ANVIL_ROCK_FALL_DAMAGE * fDifficultyMod;
          GameObject oTarget;

          Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(90146), evLocation, 2.0f);

          for (i = 0; i < size; i++)
          {
               oTarget = arNearestCreatures[i];
               if (ResistanceCheck(oBranka, oTarget, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, EngineConstants.RESISTANCE_PHYSICAL) == EngineConstants.FALSE)
               {
                    if (IsObjectHostile(oBranka, oTarget) != EngineConstants.FALSE || oTarget == oBranka)
                         DamageCreature(oTarget, gameObject, fDamage, EngineConstants.DAMAGE_TYPE_PHYSICAL);
               }
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_LyriumUseBranka()
     {
          GameObject oBranka = GetObjectByTag(EngineConstants.ORZ_CR_BRANKA);
          int nCounter = GetLocalInt(oBranka, EngineConstants.CREATURE_COUNTER_1);

          if (nCounter == 1)
          {
               WR_ClearAllCommands(oBranka, EngineConstants.TRUE);
               PlaySoundSet(oBranka, EngineConstants.SS_THREATEN, 1.0f);
               WR_AddCommand(oBranka, CommandPlayAnimation(EngineConstants.COMBAT_ANIMATION_ENTER_BERSERK, 0, 0, 0), EngineConstants.TRUE, EngineConstants.TRUE);
               ApplyEffectVisualEffect(oBranka, oBranka, 1021, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
               HealCreature(oBranka, EngineConstants.FALSE, (GetMaxHealth(oBranka) * 0.15f), EngineConstants.TRUE);
          }
          else if (nCounter == 2)
          {
               PlaySoundSet(oBranka, EngineConstants.SS_THREATEN, 1.0f);
               ORZ_BrankaCaridin_BrankaIllusionStart();
          }
          else if (nCounter == 3)
          {
               WR_ClearAllCommands(oBranka, EngineConstants.TRUE);
               WR_AddCommand(oBranka, CommandPlayAnimation(EngineConstants.COMBAT_ANIMATION_ENTER_BERSERK, 0, 0, 0), EngineConstants.TRUE, EngineConstants.TRUE);
               PlaySoundSet(oBranka, EngineConstants.SS_THREATEN, 1.0f);
               ORZ_BrankaCaridin_RockFallStart();
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_BrankaCaridin_LyriumUseCaridin()
     {
          GameObject oCaridin = GetObjectByTag(EngineConstants.ORZ_CR_CARIDIN);
          int nCounter = GetLocalInt(oCaridin, EngineConstants.CREATURE_COUNTER_1);

          if (nCounter == 1)
          {
               WR_ClearAllCommands(oCaridin, EngineConstants.TRUE);
               WR_AddCommand(oCaridin, CommandPlayAnimation(EngineConstants.COMBAT_ANIMATION_ENTER_BERSERK, 0, 0, 0), EngineConstants.TRUE, EngineConstants.TRUE);
               PlaySoundSet(oCaridin, EngineConstants.SS_SCRIPTED_HELP, 1.0f);
               List<GameObject> arGolems = UT_GetTeam(EngineConstants.ORZ_TEAM_CARIDIN_GOLEMS);
               int i, size = GetArraySize(arGolems);
               for (i = 0; i < size; i++)
               {
                    if (IsDead(arGolems[i]) == EngineConstants.FALSE)
                    {
                         ApplyEffectVisualEffect(oCaridin, arGolems[i], 1021, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         HealCreature(arGolems[i], EngineConstants.FALSE, (GetMaxHealth(arGolems[i]) * 0.15f), EngineConstants.TRUE);
                    }
               }
          }
          else if (nCounter == 2)
          {
               // TEMPEST
               xEffect eff = EffectModifyManaStamina(Ability_GetAbilityCost(oCaridin, EngineConstants.ABILITY_TALENT_GOLEM_RANGED_1));
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eff, oCaridin);
               PlaySoundSet(oCaridin, EngineConstants.SS_COMBAT_BATTLE_CRY, 1.0f);
               AddCommand(oCaridin, CommandUseAbility(EngineConstants.ABILITY_TALENT_GOLEM_RANGED_1, UT_GetNearestHostileCreature(oCaridin), GetPosition(UT_GetNearestHostileCreature(oCaridin)), 0.0f), EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (nCounter == 3)
          {
               WR_ClearAllCommands(oCaridin, EngineConstants.TRUE);
               WR_AddCommand(oCaridin, CommandPlayAnimation(EngineConstants.COMBAT_ANIMATION_ENTER_BERSERK, 0, 0, 0), EngineConstants.TRUE, EngineConstants.TRUE);
               PlaySoundSet(oCaridin, EngineConstants.SS_THREATEN, 1.0f);
               ORZ_BrankaCaridin_LavaQuakeStart();
          }
     }
}