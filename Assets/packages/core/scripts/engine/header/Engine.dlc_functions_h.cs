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
     // Functions for Denerim Side Plots
     // Copied from ARL_FUNCTIONS_H
     // 9/9/08

     //#include"utility_h"

     public void DLC_SetTeamGroup(int nTeam, int nGroup)
     {
          List<GameObject> oTeamArray = GetTeam(nTeam);
          int nTeamSize = GetArraySize(oTeamArray);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               GameObject oCreature = oTeamArray[nIndex];
               SetGroupId(oCreature, nGroup);
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "", "Setting group of creature to " + IntToString(GetGroupId(oCreature)) + " Name: " + GetName(oCreature) + " ObjectID: " + ObjectToString(oCreature) + " Tag: " + GetTag(oCreature));
          }
     }

     /* @brief This makes the GameObject with name sTag patrol along waypoints.
*
* The specific path is based off of the tag name of the patroller and follows
* the UT_QuickMove naming convention. So you start at mp_<sTag>_0 and you loop
* back to 1 at the integer provided in nLastPatrolNum.
*
* @param sTag - The tag of the GameObject on patrol
* @param nLastPatrolNum - The last waypoint before the patroller loops back to 1.
* @param nRun - Set to EngineConstants.TRUE if you want the person to run on their patrol
* @param nLoop - To keep doing circles, by default they'll walk forever
*
* @author Ferret
*/
     public void DLC_PatrolWPs(string sTag, int nLastPatrolNum, int nRun = EngineConstants.FALSE, int nLoop = EngineConstants.TRUE)
     {
          GameObject oPatroller = GetObjectByTag(sTag);
          GameObject oWaypoint;
          List<Vector3> lPatrolPath=new List<Vector3>();

          int nLoopCounter;

          // Set up an array of the waypoints the patroller will be walking between
          for (nLoopCounter = 0; nLoopCounter < nLastPatrolNum; nLoopCounter++)
          {
               oWaypoint = GetObjectByTag("mp_" + sTag + "_" + IntToString(nLoopCounter + 1));
               lPatrolPath[nLoopCounter] = GetLocation(oWaypoint);
          }

          AddCommand(oPatroller, CommandMoveToMultiLocations(lPatrolPath, nRun, 0, nLoop));

     }

     //public void main() {}
}