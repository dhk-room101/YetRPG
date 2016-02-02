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
     // effect_charm_h
     // -----------------------------------------------------------------------------
     /*
         Effect Charm

         Causes creature to temporarily switch groups and hostility.

         Note: Only one charm xEffect can exist at any time.
         If a charm xEffect exists, the new one will fail.

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     /* @brief Returns an xEffect which charms
     * @author Georg Zoeller
     *
     * @return a valid xEffect of type EngineConstants.EFFECT_TYPE_CHARM.
     */
     //#include"effect_constants_h"
     //#include"wrappers_h"
     //#include"ai_threat_h"

     //moved public const float EngineConstants.CHARM_MINIMUM_MEDIUM_CUTOFF = 0.53;
     //moved public const float EngineConstants.CHARM_MINIMUM_LARGE_CUTOFF = 1.201;
     //moved public const int EngineConstants.CHARM_CREATURE_SIZE_SMALL = 1;
     //moved public const int EngineConstants.CHARM_CREATURE_SIZE_MEDIUM = 2;
     //moved public const int EngineConstants.CHARM_CREATURE_SIZE_LARGE = 3;

     // Returns the creature's size based on its ring size in EngineConstants.APR_base
     public int _GetCreatureSize2(GameObject oObject)
     {
          // Get creature's appearance from the 2DA EngineConstants.APR_base
          int nAppearance = GetAppearanceType(oObject);
          float fAppearance = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "PERSPACE", nAppearance);
          // Depending on the creature's size (float values)
          if (fAppearance < EngineConstants.CHARM_MINIMUM_MEDIUM_CUTOFF)
          {
               // Creature is small
               int nSize = EngineConstants.CHARM_CREATURE_SIZE_SMALL;
               return nSize;
          }
          else
          {
               if (fAppearance >= EngineConstants.CHARM_MINIMUM_LARGE_CUTOFF)
               {
                    // Creature is large
                    int nSize = EngineConstants.CHARM_CREATURE_SIZE_LARGE;
                    return nSize;
               }
               else
               {
                    // Creature is medium
                    int nSize = EngineConstants.CHARM_CREATURE_SIZE_MEDIUM;
                    return nSize;
               }
          }
     }

     public xEffect EffectCharm(GameObject oMaster, GameObject oTarget)
     {
          xEffect eCharm = Effect(EngineConstants.EFFECT_TYPE_CHARM);
          if (IsDead(oMaster) == EngineConstants.FALSE)
          {
               // save target group
               int nGroup = GetGroupId(oTarget);
               SetEffectIntegerRef(ref eCharm, 0, nGroup);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_charm_h.EffectCharm", "Charm on " + ToString(oTarget) + " with group " + ToString(nGroup));
#endif

               // save master group
               nGroup = GetGroupId(oMaster);
               SetEffectIntegerRef(ref eCharm, 1, nGroup);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_charm_h.EffectCharm", "Master is " + ToString(oMaster) + " with group " + ToString(nGroup));
#endif
          }

          return eCharm;
     }

     public int Effects_HandleApplyEffectCharm(xEffect eEffect)
     {
          // fail if there are charm effects on creature
          int nArraySize = GetArraySize(GetEffects(gameObject, EngineConstants.EFFECT_TYPE_CHARM));
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_charm_h.Effects_HandleApplyEffectCharm", "ArraySize is " + ToString(nArraySize));
#endif
          if (nArraySize > 0  /*this includes this xEffect */)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "ApplyCharm", "Not applying charm because target is already charmed! ArraySize is " + ToString(nArraySize));
#endif

               return EngineConstants.FALSE;
          }
          else
          {
               int nGroup = GetEffectIntegerRef(ref eEffect, 1); ;
               SetGroupId(gameObject, nGroup);

               // clear commands and switch to new target
               ClearAllCommands(gameObject);
               List<GameObject> oThreats = GetNearestObjectByHostility(gameObject, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_CREATURE, 1, 1, 1);
               GameObject oThreat = oThreats[0];
               AI_Threat_SetThreatTarget(gameObject, oThreat);

               /*        ClearAllCommands(gameObject);
                       ClearThreatTable(gameObject);
                       AI_Threat_ClearToSwitchTarget(gameObject);
                       AI_Threat_UpdateEnemyAppeared(gameObject, oThreat);
                       //AI_Threat_SetThreatTarget(gameObject, oThreat);
                       //AddCommand(gameObject, CommandAttack(oThreat), EngineConstants.TRUE, EngineConstants.TRUE);
               */

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "Threat num = " + ToString(GetArraySize(oThreats)));
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "Threat target = " + GetTag(oThreat));
#endif
          }

          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectCharm(xEffect eEffect)
     {
          // if a valid group was stored
          int nGroup = GetEffectIntegerRef(ref eEffect, 0);

          // Georg: Emmanuel's script had a race condition if people changed the group while the xEffect was active. Adding a check
          int nMasterGroup = GetEffectIntegerRef(ref eEffect, 1);
          if (nGroup != 0 && GetGroupId(gameObject) == nMasterGroup)
          {
               SetGroupId(gameObject, nGroup);
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_charm_h.Effects_HandleRemoveEffectCharm", "Charm to " + ToString(nGroup) + " on " + ToString(gameObject));
#endif

          // clear threat info
          AI_Threat_UpdateEnemyAppeared(gameObject, GetEffectCreatorRef(ref eEffect));

          return EngineConstants.TRUE;
     }
}