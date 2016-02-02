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
     //#include"ai_constants_h"
     //#include"core_h"
     //#include"2da_constants_h"

     public int AI_BehaviorCheck(string sBehaviorType)
     {
          int nBehavior = GetAIBehavior(gameObject);
          string sLabel = GetM2DAString(EngineConstants.TABLE_AI_BEHAVIORS, "Label", nBehavior);
          int nRet = GetM2DAInt(EngineConstants.TABLE_AI_BEHAVIORS, sBehaviorType, nBehavior);
#if DEBUG
          Log_Trace_AI("AI_BehaviorCheck", "*" + sLabel + "* [" + sBehaviorType + "]: " + IntToString(nRet));
#endif
          return nRet;
     }

     public int AI_BehaviorCheck_AvoidAOE()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_AVOID_AOE);
     }

     public int AI_BehaviorCheck_AttackBack()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_ATTACK_BACK);
     }

     public int AI_BehaviorCheck_ChaseEnemy()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_CHASE_ENEMY);
     }

     public int AI_BehaviorCheck_AttackOnCombatStart()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_ATTACK_ON_COMBAT_START);
     }

     public int AI_BehaviorCheck_PreferRange()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_PREFER_RANGE);
     }

     public int AI_BehaviorCheck_PreferMelee()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_PREFER_MELEE);
     }

     public int AI_BehaviorCheck_AvoidNearbyEnemies()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_AVOID_NEARBY_ENEMIES);
     }

     public int AI_BehaviorCheck_AvoidMelee()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_AVOID_MELEE_ENEMIES);
     }

     public int AI_BehaviorCheck_DefaultAttack()
     {
          return AI_BehaviorCheck(EngineConstants.AI_BEHAVIOR_DEFAULT_ATTACK);
     }
}