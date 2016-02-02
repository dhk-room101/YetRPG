//ready and useless
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
     // sys_achievements
     // -----------------------------------------------------------------------------
     /*
         Game Achievements System

         This is the prototype library for the game achievements system.

     */
     // -----------------------------------------------------------------------------

     //#include"design_tracking_h"

     //moved public const int DEV_ACV_BHN_ROMANCE = 20;

     public void Acv_Grant(int nId)
     {
          // <-- Call Engine function for granting achievements here -->

          // log this for testing coverage report so we can query later where which achievement is given.
          /*if (TRACKING_ENABLED)
          {
              //                  log name                               acv id                                log silently                script
              //SendToRunDatabase("ddachieve" + TRACK_DELIMITER + ToString(nId)+ TRACK_DELIMITER  + "true" + TRACK_DELIMITER  + GetCurrentScriptName());
          }*/
     }
}