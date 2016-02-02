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
     // sys_stealth
     // -----------------------------------------------------------------------------
     /*
         Stealth Sytem Implementation
     */
     // -----------------------------------------------------------------------------
     // owner: georg zoeller
     // -----------------------------------------------------------------------------
     //#include"core_h"
     //#include"effect_upkeep_h"

     //moved public const float RANK_MULTIPLIER = 10.0f;
     //moved public const float LEVEL_HIGHER_MODIFIER = 10.0f;
     //moved public const float LEVEL_LOWER_MODIFIER = 5.0f;
     ////moved public const int ARMOR_STEALTH_MODIFIER = 8;

     //moved public const int STEALTH_CRUST_VFX = 90284;
     //moved public const int STEALTH_IMPACT_VFX = 90285;

     public int Stealth_GetStealthRank(GameObject oCreature)
     {
          int nStealthRank = -1;
          if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_STEALTH_4) != EngineConstants.FALSE)
          {
               nStealthRank = 4;
          }
          else if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_STEALTH_3) != EngineConstants.FALSE)
          {
               nStealthRank = 3;
          }
          else if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_STEALTH_2) != EngineConstants.FALSE)
          {
               nStealthRank = 2;
          }
          else if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_STEALTH_1) != EngineConstants.FALSE)
          {
               nStealthRank = 1;
          }
#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Stealth Rank = " + ToString(nStealthRank));
#endif

          return nStealthRank;
     }

     public int Stealth_GetPerceptionRank(GameObject oCreature)
     {
          int nCreatureRank = GetCreatureRank(oCreature);
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Creature Rank = " + ToString(nCreatureRank));
          int nPerceptionRank = GetM2DAInt(Diff_GetAutoScaleTable(), "nStealthRank", nCreatureRank);
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Perception Rank = " + ToString(nPerceptionRank));

          return nPerceptionRank;
     }

     public int Stealth_CheckSuccess(GameObject oStealther, GameObject oPerceiver = null)
     {
          if (oPerceiver == null) oPerceiver = gameObject;
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Perception check for " + GetTag(oPerceiver) + " detecting " + GetTag(oStealther));

          int bCombatState = GetCombatState(oStealther);
          int nStealthRank = Stealth_GetStealthRank(oStealther);
          int nPerceptionRank = Stealth_GetPerceptionRank(oPerceiver);

          if ((bCombatState == EngineConstants.FALSE) || (nStealthRank >= nPerceptionRank))
          {
               return EngineConstants.TRUE;
          }
          else
          {
#if DEBUG
               LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "DETECTED by " + GetTag(oPerceiver));
#endif

               // display stealth detected UI message
               UI_DisplayMessage(oStealther, 3511);
               UI_DisplayMessage(oPerceiver, 3513);

               return EngineConstants.FALSE;
          }
     }

     // -----------------------------------------------------------------------------
     // @brief  Returns if a target is visible nor not
     // @author georg
     // -----------------------------------------------------------------------------
     public int IsInvisible(GameObject oTarget)
     {
          // Note: This may get more conditions
          return IsModalAbilityActive(oTarget, EngineConstants.ABILITY_SKILL_STEALTH_1);
     }

     // -----------------------------------------------------------------------------
     // @brief  Returns if a target is stealthy
     // @author georg
     // -----------------------------------------------------------------------------
     public int IsStealthy(GameObject oTarget)
     {
          return IsModalAbilityActive(oTarget, EngineConstants.ABILITY_SKILL_STEALTH_1);
     }

     // -----------------------------------------------------------------------------
     // @brief  Forces a target to become drop out of stealth mode
     // @author georg
     // -----------------------------------------------------------------------------
     public void DropStealth(GameObject oTarget)
     {

          if (IsModalAbilityActive(oTarget, EngineConstants.ABILITY_SKILL_STEALTH_1) != EngineConstants.FALSE)
          {
               Effects_RemoveUpkeepEffect(oTarget, EngineConstants.ABILITY_SKILL_STEALTH_1);
          }

     }

     // -----------------------------------------------------------------------------
     // @brief  Forces two creatures to see each other,implicitly cancelling any stealth
     //         that might exist on the perceived creature
     // @author georg
     // -----------------------------------------------------------------------------
     public int WR_TriggerPerception(GameObject oPerceiver, GameObject oPerceivedCreature)
     {
          Debug.LogWarning("system stealth: wr_trigger perception: handle event");
          DropStealth(oPerceivedCreature);

          xEvent ev = new xEvent(EngineConstants.EVENT_TYPE_INVALID);//GetCurrentEvent();

#if DEBUG
          string sOut = ToString(oPerceiver) + " group: " + ToString(GetGroupId(oPerceiver)) + " perceived ";
          sOut += ToString(oPerceivedCreature) + " group: " + ToString(GetGroupId(oPerceivedCreature));
          Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_PERCEPTION, GetCurrentScriptName() + ".TriggerPerception (" + ToString(GetEventTypeRef(ref ev)) + ")", sOut);
#endif

          return TriggerPerception(oPerceiver, oPerceivedCreature);
     }
}