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

         Paragon of Her Kind
          -> AI Runner Include Script

          This include file contains functions to be used by a creature who flees
          from the PC in the middle of combat for some reason or another.

          **IMPORTANT:   This include file was created for the purpose of testing
                         the Custom AI functionality and solidifying a way of
                         making CAI (CustomAI) libraries of includes for varying
                         CAI's that will be needed in the game.

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: August 28, 2007
     //==============================================================================

     //#include"wrappers_h"
     //#include"utility_h"

     //#include"ai_main_h_2"

     //==============================================================================
     // CONSTANTS
     //==============================================================================

     //moved public const int       CAI_RUNNER_RUN              = 10001;
     //moved public const string    CAI_RUNNER_TEAM_WP_PREFIX   = "rp_";

     //==============================================================================
     // FUNCTION DECLARATION
     //==============================================================================


     //==============================================================================
     // FUNCTION IMPLEMENATION
     //------------------------------------------------------------------------------
     // CAI_Runner_RunToObject
     //------------------------------------------------------------------------------

     public void CAI_Runner_RunToObject(GameObject oRunner, GameObject oObject)
     {

          CAI_SetCustomAI(oRunner, EngineConstants.CAI_RUNNER_RUN);
          CAI_SetCustomAIObject(oRunner, oObject);

     }

     //------------------------------------------------------------------------------
     // CAI_Runner_RunToTeamWP
     //------------------------------------------------------------------------------

     public void CAI_Runner_RunToTeamWP(GameObject oRunner)
     {

          string sWPTag = EngineConstants.CAI_RUNNER_TEAM_WP_PREFIX + ToString(GetTeamId(oRunner));
          GameObject oWP = UT_GetNearestObjectByTag(oRunner, sWPTag);
          if (IsObjectValid(oWP) != EngineConstants.FALSE)
               CAI_Runner_RunToObject(oRunner, oWP);
          else
               CAI_SetCustomAI(oRunner, EngineConstants.CAI_INACTIVE, EngineConstants.TRUE);

     }

     //------------------------------------------------------------------------------
     // CAI_Runner_RunToNearestAlly
     //------------------------------------------------------------------------------

     public void CAI_Runner_RunToNearestAlly(GameObject oRunner)
     {

          WR_AddCommand(oRunner, CommandWait(0.1f));
          CAI_SetCustomAI(gameObject, EngineConstants.CAI_INACTIVE);

     }

     //------------------------------------------------------------------------------
     // CAI_Runner_HandleRunToObject
     //------------------------------------------------------------------------------

     public void CAI_Runner_HandleRunToObject(GameObject oRunner)
     {

          GameObject oCAIObject = CAI_GetCustomAIObject(oRunner);
          WR_AddCommand(oRunner, CommandMoveToObject(oCAIObject, EngineConstants.TRUE, 0.5f));
          Rubber_SetHome(oRunner, oCAIObject);
          CAI_SetCustomAI(gameObject, EngineConstants.CAI_INACTIVE);

     }

     //------------------------------------------------------------------------------
     // CAI_Runner_CountCurrentAllies
     //------------------------------------------------------------------------------

     public int CAI_Runner_CountCurrentAllies(GameObject oRunner)
     {

          int nIndex;
          int nArraySize;
          int nCount;
          GameObject oAlly;
          List<GameObject> arTeam;

          //--------------------------------------------------------------------------

          nCount = 0;
          arTeam = UT_GetTeam(GetTeamId(oRunner));
          nArraySize = GetArraySize(arTeam);

          //--------------------------------------------------------------------------

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oAlly = arTeam[nIndex];
               if (GetCombatState(oAlly) != EngineConstants.FALSE)
                    nCount++;
          }

          return nCount;

     }

     //------------------------------------------------------------------------------
     // CAI_Runner_HasAlliesToRunTo
     //------------------------------------------------------------------------------

     public int CAI_Runner_HasAlliesToRunTo(GameObject oRunner)
     {

          int nIndex;
          int nArraySize;
          int nCount;
          GameObject oAlly;
          List<GameObject> arTeam;

          //--------------------------------------------------------------------------

          nCount = 0;
          arTeam = UT_GetTeam(GetTeamId(oRunner));
          nArraySize = GetArraySize(arTeam);

          //--------------------------------------------------------------------------

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oAlly = arTeam[nIndex];
               if (GetCombatState(oAlly) == EngineConstants.FALSE)
                    nCount++;
          }

          return nCount;

     }
}