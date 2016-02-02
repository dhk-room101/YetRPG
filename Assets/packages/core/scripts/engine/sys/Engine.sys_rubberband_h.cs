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
     //::///////////////////////////////////////////////
     //:: sys_rubberband_h
     //:: Copyright (c) 2007 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         Implementation of the homing 'rubberband' system for creature AI.
     */
     //:://////////////////////////////////////////////
     //:: Created By: Ferret Baudoin
     //:: Created On: Aug. 27, 2007
     //:://////////////////////////////////////////////                       

     //#include"wrappers_h"

     //moved public const string EngineConstants.RUBBER_HOME_ENABLED         = "RUBBER_HOME_ENABLED";
     //moved public const string EngineConstants.RUBBER_HOME_LOCATION_X      = "RUBBER_HOME_LOCATION_X";
     //moved public const string EngineConstants.RUBBER_HOME_LOCATION_Y      = "RUBBER_HOME_LOCATION_Y";
     //moved public const string EngineConstants.RUBBER_HOME_LOCATION_Z      = "RUBBER_HOME_LOCATION_Z";
     //moved public const string EngineConstants.RUBBER_HOME_LOCATION_FACING = "RUBBER_HOME_LOCATION_FACING";

     /* @brief Sets a creature's home location.
     *
     * The home Vector3 of a creature is where it was placed on the level. 
     * The home lcoation is updated after any QuickMove or LocalJump commands.
     *
     * @param oTarget The creature being assigned a new home.
     * @param oNewHome The GameObject whose Vector3 is to be oTarget's new home location. 
     *                 If oNewHome is invalid, the new home Vector3 is set to the 
     *                 current Vector3 of oTarget. 
     * @author Ferret
     **/
     public void Rubber_SetHome(GameObject oTarget = null, GameObject oNewHome = null)
     {
          if (oTarget == null) oTarget = gameObject;
          GameObject oTargetLoc = oTarget;
          if (IsObjectValid(oNewHome) != EngineConstants.FALSE)
               oTargetLoc = oNewHome;

          Vector3 vStartPosition = GetPosition(oTargetLoc);
          SetLocalFloat(oTarget, EngineConstants.RUBBER_HOME_LOCATION_X, vStartPosition.x);
          SetLocalFloat(oTarget, EngineConstants.RUBBER_HOME_LOCATION_Y, vStartPosition.y);
          SetLocalFloat(oTarget, EngineConstants.RUBBER_HOME_LOCATION_Z, vStartPosition.z);

          float fStartFacing = GetFacing(oTargetLoc);
          SetLocalFloat(oTarget, EngineConstants.RUBBER_HOME_LOCATION_FACING, fStartFacing);
     }

     /* @brief Gets a creature's home location.
     *
     * @param oTarget The creature whose home Vector3 to fetch.
     **/
     public Vector3 Rubber_GetHome(GameObject oTarget = null)
     {
          if (oTarget == null) oTarget = gameObject;
          float fX = GetLocalFloat(oTarget, EngineConstants.RUBBER_HOME_LOCATION_X);
          float fY = GetLocalFloat(oTarget, EngineConstants.RUBBER_HOME_LOCATION_Y);
          float fZ = GetLocalFloat(oTarget, EngineConstants.RUBBER_HOME_LOCATION_Z);
          float fFacing = GetLocalFloat(oTarget, EngineConstants.RUBBER_HOME_LOCATION_FACING);

          return Location(GetArea(GetHero()), Vector(fX, fY, fZ), fFacing);
     }


     /* @brief Causes oTarget to go to its home location.
     *
     * The home lcoation of a creature is the Vector3 of where it was placed on the level. 
     * The home Vector3 is updated after any QuickMove or LocalJump command.
     *
     * @param oTarget    The creature being sent home.        
     * @param bRun       EngineConstants.TRUE/EngineConstants.FALSE to make oTarget run/walk home.
     * @author Ferret
     **/
     public void Rubber_GoHome(GameObject oTarget = null, int bRun = EngineConstants.FALSE)
     {
          if (oTarget == null) oTarget = gameObject;
          if (GetObjectActive(oTarget) != EngineConstants.FALSE
             && IsDead(oTarget) == EngineConstants.FALSE
             && GetLocalInt(oTarget, EngineConstants.RUBBER_HOME_ENABLED) != EngineConstants.FALSE)
          {
               // Walk/run to home location
               Vector3 lHome = Rubber_GetHome(oTarget);
               WR_AddCommand(oTarget, CommandWait(RandomF(3, 1)));
               WR_AddCommand(oTarget, CommandMoveToLocation(lHome, bRun));
          }
     }


     /* @brief Causes oTarget to jump instantly to its home location.
     *
     * The home Vector3 of a creature is where it was placed on the level. 
     * The home Vector3 is updated after a QuickMove or LocalJump command.
     *
     * @param oTarget    The creature being sent home. 
     * @author Ferret
     **/
     public void Rubber_JumpHome(GameObject oTarget = null)
     {
          if (oTarget == null) oTarget = gameObject;
          if (GetObjectActive(oTarget) != EngineConstants.FALSE
             && IsDead(oTarget) == EngineConstants.FALSE
             && GetLocalInt(oTarget, EngineConstants.RUBBER_HOME_ENABLED) != EngineConstants.FALSE)
          {
               // Jump GameObject to home location
               Vector3 lHome = Rubber_GetHome(oTarget);
               WR_AddCommand(oTarget, CommandJumpToLocation(lHome));
          }
     }
}