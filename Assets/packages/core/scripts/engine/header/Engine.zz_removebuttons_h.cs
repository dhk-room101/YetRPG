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
     //:: zz_removebuttons_h
     //:: Copyright (c) 2008 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         Functions remove proper button groups
         For the Epilogue
     */
     //:://////////////////////////////////////////////
     //:: Created By: Mark Barazzuol
     //:: Created On: June 17, 2008
     //:://////////////////////////////////////////////

     //#include"utility_h"

     public void InitializeEpiPlotDebug()
     {
          DEBUG_ConsoleCommand("removebuttongroup Default");
          DEBUG_ConsoleCommand("removebuttongroup Followers");
          DEBUG_ConsoleCommand("removebuttongroup InParty");
          DEBUG_ConsoleCommand("removebuttongroup Lineage");
          DEBUG_ConsoleCommand("removebuttongroup Epilogue");
          DEBUG_ConsoleCommand("removebuttongroup PartyRelations1");
          DEBUG_ConsoleCommand("removebuttongroup PartyRelations2");
          DEBUG_ConsoleCommand("removebuttongroup OriginRelations1");
          DEBUG_ConsoleCommand("removebuttongroup OriginRelations2");
          DEBUG_ConsoleCommand("removebuttongroup OriginRelations");

     }
}