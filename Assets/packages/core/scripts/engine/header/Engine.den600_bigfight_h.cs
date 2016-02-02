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
     //#include"events_h"
     //#include"cai_h"

     //moved public const string    BIGFIGHT_RETREATING_FLAG            = EngineConstants.CREATURE_COUNTER_1;
     //moved public const string    BIGFIGHT_DO_NOT_FOLLOW_LEADER       = EngineConstants.CREATURE_COUNTER_2;
     //moved public const string    BIGFIGHT_TARGET_TEAM                = EngineConstants.CREATURE_COUNTER_3;
     //moved public const string    BIGFIGHT_PLAYER_ALLIES              = EngineConstants.AREA_COUNTER_2;

     //moved public const float LIEUTENANT_HEALTH_FLEE  = 0.2f;   
     //moved public const float LOGHAIN_HEALTH_DEFEATED = 0.12f;

     //moved public const int CAI_RUN               = 1000;

     //moved public const string BIGFIGHT_SPAWN_NE  = "den600wp_spawn_ne";
     //moved public const string BIGFIGHT_SPAWN_SE  = "den600wp_spawn_se";
     //moved public const string BIGFIGHT_SPAWN_NW  = "den600wp_spawn_nw";
     //moved public const string BIGFIGHT_SPAWN_SW  = "den600wp_spawn_sw";
     //moved public const string BIGFIGHT_WP_PREFIX = "wp_";

     public void DEN_SetObjectOnTeam(int nTeamID, string sVariableName, GameObject oObject)
     {
          List<GameObject> arrTeam = GetTeam(nTeamID);
          int n;
          int nTeamSize = GetArraySize(arrTeam);

          for (n = 0; n < nTeamSize; n++)
          {
               SetLocalObject(arrTeam[n], sVariableName, oObject);
          }
     }

     public int BIGFIGHT_IsHostileTargetValid(GameObject oTarget, GameObject oAttacker)
     {
          int nRet = EngineConstants.TRUE;
          string DEBUG_sInvalidReason = "";

          if (IsObjectValid(oTarget) == EngineConstants.FALSE)
          {
               DEBUG_sInvalidReason = "INVALID OBJECT";
               nRet = EngineConstants.FALSE;
          }
          else if (IsDead(oTarget) != EngineConstants.FALSE)
          {
               DEBUG_sInvalidReason = "DEAD";
               nRet = EngineConstants.FALSE;
          }
          else if (IsDying(oTarget) != EngineConstants.FALSE)
          {
               DEBUG_sInvalidReason = "DYING";
               nRet = EngineConstants.FALSE;
          }
          else if (GetObjectActive(oTarget) == EngineConstants.FALSE)
          {
               DEBUG_sInvalidReason = "TARGET INACTIVE";
               nRet = EngineConstants.FALSE;
          }
          else if (IsObjectHostile(oAttacker, oTarget) == EngineConstants.FALSE)
          {
               DEBUG_sInvalidReason = "NOT HOSTILE";
               nRet = EngineConstants.FALSE;
          }
          else if (Effects_HasAIModifier(oAttacker, EngineConstants.AI_MODIFIER_IGNORE) != EngineConstants.FALSE)
          {
               DEBUG_sInvalidReason = "AI IGNORE FLAG ACTIVE";
               nRet = EngineConstants.FALSE;
          }
          else if (IsPerceiving(oAttacker, oTarget) == EngineConstants.FALSE)
          {
               DEBUG_sInvalidReason = "TARGET NOT PERCEIVED";
               nRet = EngineConstants.FALSE;
          }
          else if (IsStealthy(oTarget) != EngineConstants.FALSE)
          {
               DEBUG_sInvalidReason = "TARGET STEALTHY";
               nRet = EngineConstants.FALSE;
          }

          if (nRet == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "BIGFIGHT_IsHostileTargetValid", "Target " + GetTag(oTarget) + " invalid: " + DEBUG_sInvalidReason);
          }

          return nRet;
     }

     public GameObject DEN_GetRandomTeamTarget(GameObject oAttacker, int nTeamID, int nRetries = 3)
     {
          GameObject oTarget;
          List<GameObject> arGroup = UT_GetTeam(nTeamID);
          int nArraySize;
          int nTarget;

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_GetRandomTeamTarget", "nTeamID: " + ToString(nTeamID));

          if (nTeamID == -1)
          {
               arGroup = GetNearestObjectByGroup(oAttacker, EngineConstants.GROUP_PC);
               nArraySize = GetArraySize(arGroup);
               nTarget = Engine_Random(nArraySize);
               oTarget = arGroup[nTarget];

               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_GetRandomTeamTarget", "targeting party member: " + GetTag(oTarget));
          }
          else
          {
               nArraySize = GetArraySize(arGroup);
               nTarget = Engine_Random(nArraySize);
               oTarget = arGroup[nTarget];

               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_GetRandomTeamTarget", "targeting team member: " + GetTag(oTarget));
          }
          if (BIGFIGHT_IsHostileTargetValid(oTarget, oAttacker) == EngineConstants.FALSE
             && nArraySize > 0
             && nRetries > 0)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_GetRandomTeamTarget", "retrying");
               nRetries--;
               oTarget = DEN_GetRandomTeamTarget(oAttacker, nTeamID, nRetries);
          }
          return oTarget;
     }

     public void DEN_SetTeamTargetOverride(int nAttackingTeam, int nTargetTeam, int bPermanentOverride = EngineConstants.FALSE)
     {
          List<GameObject> arAttackingTeam = UT_GetTeam(nAttackingTeam);
          GameObject oTarget;
          int n;
          int nAttackingTeamSize = GetArraySize(arAttackingTeam);
          int nOverrideCount = -1;

          nOverrideCount *= bPermanentOverride;

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_SetTeamTargetOverride", "called for nAttackingTeam: " + ToString(nAttackingTeam) + " against nTargetTeam: " + ToString(nTargetTeam));

          GameObject oAttackingTeamMember;

          for (n = 0; n < nAttackingTeamSize; n++)
          {
               oAttackingTeamMember = arAttackingTeam[n];
               oTarget = DEN_GetRandomTeamTarget(oAttackingTeamMember, nTargetTeam, 0);

               if (BIGFIGHT_IsHostileTargetValid(oTarget, oAttackingTeamMember) != EngineConstants.FALSE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_SetTeamTargetOverride", "setting override on " + ToString(oAttackingTeamMember) + " to: " + ToString(oTarget));
                    Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_SetTeamTargetOverride", "setting override count to: " + ToString(nOverrideCount));

                    SetLocalObject(oAttackingTeamMember, EngineConstants.AI_TARGET_OVERRIDE, oTarget);
                    SetLocalInt(oAttackingTeamMember, EngineConstants.AI_TARGET_OVERRIDE_DUR_COUNT, nOverrideCount);
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_SetTeamTargetOverride", "Target " + ToString(oTarget) + " not a valid target for " + ToString(oAttackingTeamMember) + ", target not assigned");
               }
          }
     }

     public void DEN_FollowLeader(GameObject oLeader, int nTeamID = -1)
     {
          GameObject oAssignedTarget = CAI_GetCustomAIObject(oLeader);
          GameObject oCurrentTarget = GetAttackTarget(oLeader);
          GameObject oNewTarget;
          GameObject oTeamMember;

          if (nTeamID == -1)
          {
               nTeamID = GetTeamId(oLeader);
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_FollowLeader", "Leader: " + GetTag(oLeader) + "; nTeamID: " + ToString(nTeamID));
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_FollowLeader", "oAssignedTarget: " + GetTag(oAssignedTarget) + "; oCurrentTarget: " + GetTag(oCurrentTarget));

          List<GameObject> arTeam = UT_GetTeam(nTeamID);
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_FollowLeader", "nTeamID: " + ToString(nTeamID));

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_FollowLeader", "nTeamSize: " + ToString(GetArraySize(arTeam)));

          if (oAssignedTarget != oCurrentTarget)
          {
               int n;
               int nTeamSize = GetArraySize(arTeam);
               int nTargetTeamID;
               GameObject oTeamMemberTarget=null;

               CAI_SetCustomAIObject(oLeader, oCurrentTarget);
               for (n = 0; n < nTeamSize; n++)
               {
                    oTeamMember = arTeam[n];
                    if (oTeamMember != oLeader)
                    {
                         // check if Target has an assigned team that isn't their own (e.g. Loghain)
                         nTargetTeamID = CAI_GetCustomAIInteger(oCurrentTarget);

                         Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_FollowLeader", "nTargetTeamID from CAI: " + ToString(nTargetTeamID));

                         if (nTargetTeamID == 0)
                         {
                              nTargetTeamID = GetTeamId(oCurrentTarget);
                              Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_FollowLeader", "nTargetTeamID from leader target: " + ToString(nTargetTeamID));
                         }

                         if (Engine_Random(3) < 2)
                         {
                              oNewTarget = oCurrentTarget;
                         }
                         else
                         {
                              oTeamMemberTarget = GetAttackTarget(oTeamMember);
                              if (BIGFIGHT_IsHostileTargetValid(oTeamMemberTarget, oTeamMember) == EngineConstants.FALSE)
                              {
                                   oNewTarget = DEN_GetRandomTeamTarget(oTeamMember, nTargetTeamID);
                              }
                              else
                              {
                                   oNewTarget = oTeamMemberTarget;
                              }
                         }

                         Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_FollowLeader", "oNewTarget: " + ToString(oNewTarget) + "; oTeamMemberTarget: " + ToString(oTeamMemberTarget));

                         if (BIGFIGHT_IsHostileTargetValid(oNewTarget, oTeamMember) != EngineConstants.FALSE)
                         {
                              Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_FollowLeader", "setting override on " + GetTag(oTeamMember) + " to: " + GetTag(oNewTarget));
                              SetLocalObject(oTeamMember, EngineConstants.AI_TARGET_OVERRIDE, oNewTarget);
                              SetLocalInt(oTeamMember, EngineConstants.AI_TARGET_OVERRIDE_DUR_COUNT, -1);

                              if (oNewTarget != oTeamMemberTarget)
                              {
                                   ClearAllCommands(oTeamMember, EngineConstants.TRUE);
                              }
                         }
                    }
               }
          }
     }
}