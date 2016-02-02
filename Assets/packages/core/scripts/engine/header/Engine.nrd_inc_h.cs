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
     //:: nrd_inc_h
     //:: Copyright (c) 2007 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         Functions for the northroad
     */
     //:://////////////////////////////////////////////
     //:: Created By: Ferret Baudoin
     //:: Created On: Jul. 24, 2007
     //:://////////////////////////////////////////////

     //#include"nrd_constants_h"
     //#include"utility_h"

     //public void main() {}

     /* @brief Makes the GameObject oTarg go to its home point.
     *
     * Home points are only available for creatures using var_creature_home.
     *
     * @param oTarg - The tag of the creature being sent home.
     * @author Ferret
     **/

     public void NRD_GoHome(GameObject oTarg)
     {

          GameObject oPC = GetHero();
          GameObject oArea = GetArea(oPC);

          float fMyHomeX = GetLocalFloat(oTarg, EngineConstants.CUSTOM_HOME_LOCATION_X);
          float fMyHomeY = GetLocalFloat(oTarg, EngineConstants.CUSTOM_HOME_LOCATION_Y);
          float fMyHomeZ = GetLocalFloat(oTarg, EngineConstants.CUSTOM_HOME_LOCATION_Z);
          float fMyHomeFacing = GetLocalFloat(oTarg, EngineConstants.CUSTOM_HOME_LOCATION_FACING);

          Vector3 vMyHomePost = Vector(fMyHomeX, fMyHomeY, fMyHomeZ);
          Vector3 lMyHome = Location(oArea, vMyHomePost, fMyHomeFacing);

          WR_AddCommand(oTarg, CommandMoveToLocation(lMyHome));

     }
}