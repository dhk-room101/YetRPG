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
     // -----------------------------------------------------------------------------------------------------------------------------------------------------
     // effect_wbomb_h
     // -----------------------------------------------------------------------------
     /*
         Effect: Walking Bomb

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"effect_constants_h"
     //#include"effect_damage_h"
     //#include"2da_data_h"
     //#include"effect_visualeffect_h"

     //moved public const int SWARM_VFX = 90100;
     //moved public const float SWARM_RANGE = 10.0f;
     //moved public const int SWARM_PROJECTILE = 130;

     public xEffect EffectSwarm(GameObject oOwner, int nAbility, int nJumps, float fDamage)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_SWARM);
          SetEffectIntegerRef(ref eEffect, 0, nAbility);
          SetEffectIntegerRef(ref eEffect, 1, nJumps);
          SetEffectObjectRef(ref eEffect, 0, oOwner);
          SetEffectFloatRef(ref eEffect, 0, fDamage);

          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Swarm Owner = " + GetTag(oOwner));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Swarm Ability = " + ToString(nAbility));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Swarm Jumps = " + ToString(nJumps));

          return eEffect;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the application of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectSwarm(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectSwarm(xEffect eEffect)
     {
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Swarm Removal = " + GetTag(gameObject));

          // if this ability is being removed from a dying/dead creature
          if ((IsDeadOrDying(gameObject) != EngineConstants.FALSE) || (GetHasEffects(gameObject, EngineConstants.EFFECT_TYPE_DEATH) != EngineConstants.FALSE) || (GetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_DYING) != EngineConstants.FALSE))
          {
               // if there are jumps left
               int nJumps = GetEffectIntegerRef(ref eEffect, 1);
               LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Jumps Remaining = " + ToString(nJumps));
               if (nJumps > 0)
               {
                    // find nearest enemy in range
                    GameObject oOwner = GetEffectObjectRef(ref eEffect, 0);
                    LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Owner = " + GetTag(oOwner));
                    List<GameObject> oTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(gameObject), EngineConstants.SWARM_RANGE, 0.0f, 0.0f, EngineConstants.TRUE);
                    int nMax = GetArraySize(oTargets);
                    LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Non-Dead Creatures Nearby = " + ToString(nMax));
                    int nCount;
                    int nTarget = -1;

                    for (nCount = 0; nCount < nMax; nCount++)
                    {
                         LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "    Creature " + ToString(nCount) + " = " + GetTag(oTargets[nCount]));
                         if (oTargets[nCount] != gameObject)
                         {
                              if (IsObjectHostile(oTargets[nCount], oOwner) != EngineConstants.FALSE)
                              {
                                   LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "      Hostile");
                                   if (CheckLineOfSightObject(gameObject, oTargets[nCount]) != EngineConstants.FALSE)
                                   {
                                        LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "      Line of Sight");
                                        nTarget = nCount;
                                        nCount = nMax;
                                   }
                              }
                         }
                    }
                    LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Final Target = " + ToString(nTarget));

                    // if one exists
                    if ((nTarget >= 0) && (nTarget < nMax))
                    {
                         int nAbility = GetEffectIntegerRef(ref eEffect, 0);
                         float fDamage = GetEffectFloatRef(ref eEffect, 0);
                         nJumps--;

                         // create event
                         xEvent ev = Event(90210);
                         SetEventIntegerRef(ref ev, 0, nAbility);
                         SetEventIntegerRef(ref ev, 1, nJumps);
                         SetEventObjectRef(ref ev, 0, oOwner);//oTargets[nTarget]);
                         SetEventObjectRef(ref ev, 1, oTargets[nTarget]);
                         //SetEventObjectRef(ref ev, 2, oOwner);
                         SetEventFloatRef(ref ev, 0, fDamage);

                         // fire projectile
                         Vector3 v = GetPosition(gameObject);
                         v.z += 1.5f;
                         GameObject oProjectile = FireHomingProjectile(EngineConstants.SWARM_PROJECTILE, v, oTargets[nTarget], 0, oTargets[nTarget]);
                         SetProjectileImpactEvent(oProjectile, ev);
                    }
               }
          }

          return EngineConstants.TRUE;
     }
}