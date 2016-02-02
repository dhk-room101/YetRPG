//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class EngineConstants
{
     //::///////////////////////////////////////////////
     //:: Autoscreenshot constants
     //:://////////////////////////////////////////////
     //
     // These are the constants for the automatic
     // screenshot system.
     //
     //:://////////////////////////////////////////////
     //:: Created By: Ian Morrison
     //:: Created On: Jan. 28th, 2009
     //:://////////////////////////////////////////////

     //2DA column references
     public const string AUTOSS_2DA_TITLE = "titlestrref";
     public const string AUTOSS_2DA_DESC = "descstrref";
     public const string AUTOSS_2DA_SSID = "screenshotID";
     public const string AUTOSS_2DA_PRIORITY = "priority";
     public const string AUTOSS_2DA_OVERRIDE = "override";

     //Constant for the screenshot type
     public const int AUTOSS_SCREENSHOT_STORY_HIGH_PRIORITY = 2;
     public const int AUTOSS_SCREENSHOT_STORY_LOW_PRIORITY = 0;

     //Constants for screenshot priority
     public const int AUTOSS_PRIORITY_DISABLED = 0;
     public const int AUTOSS_PRIORITY_PRIMARY = 1;
     public const int AUTOSS_PRIORITY_SECONDARY = 2;
     public const int AUTOSS_PRIORITY_TERTIARY = 3;
     public const int AUTOSS_PRIORITY_TOTAL_FLUFF = 4;

     public const int AUTOSS_PRIORITY_CUTOFF = 1; //Set the threshhold
}