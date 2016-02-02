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
     //:: zz_plotguidebug_h
     //:: Copyright (c) 2008 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         Debug plot gui functions
     */
     //:://////////////////////////////////////////////
     //:: Created By: Mark Barazzuol
     //:: Created On: June 17, 2008
     //:://////////////////////////////////////////////
     //#include"utility_h"

     public string GetStrPlotFlag(string sPlot, int iFlag)
     {

          int iValue = WR_GetPlotFlag(sPlot, iFlag);

          if (iValue != EngineConstants.FALSE)
               return "1";
          else
               return "0";
     }

     public void MakeButton(string sPlot, int iFlag, string sName)
     {
          int iValue;
          string sValue;
          string sBool;

          // Store the plot string and number as local variables.
          SetLocalString(GetModule(), "sPlot", sPlot);
          SetLocalInt(GetModule(), "iFlag", iFlag);

          iValue = WR_GetPlotFlag(sPlot, iFlag);
          sBool = GetStrPlotFlag(sPlot, iFlag);

          // If it's a defined flag, you can't have a checkbox.
          if (iFlag > 255)
               sValue = "textfield " + sBool + "     " + sName;
          else
               sValue = "togglebutton " + sName + " runscript zz_toggleplotflag " + sPlot + "," + ToString(iFlag) + " " + sBool;

          DEBUG_ConsoleCommand(sValue);
     }
}