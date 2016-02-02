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
     // cai_h.nss
     // -----------------------------------------------------------------------------
     /*

         Custom AI
         This script contains all the constants an information on all the custom
         ai's that exist in scripting.

         <<DEFINED IN ai_main_h_2>>
          CAI_DISABLED:  Custom AI is disabled (normal AI takes over again)
          CAI_STASIS:    Creature will become dormant and issue no commands

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: May 7, 2008
     //==============================================================================

     //#include"ai_main_h_2"
     //#include"cai_runner_h"
     //#include"cai_useplc_h"

     //==============================================================================
     /*
         CONSTANTS
         0 - 999:        Reserved for Basic Custom AI Modes
         1000 - 9999:    Free to use for Local Custom AI
         10000 - *:      Reserved for Global Custom AI (***MUST BE DEFINED BELOW!!!***)
     */
     //==============================================================================

     // CAI_RUNNER (cai_runner_h)
     // //moved public const int   CAI_RUNNER_RUN                      = 10001;

     // CAI_USEPLACEABLE (cai_useplc_h)
     // //moved public const int   CAI_USEPLACEABLE_USEACTION       = 10100;    (internal)
     //moved public const int   CAI_USEPLACEABLE_LYRIUM_VEIN        = 10200;
     //moved public const int   CAI_USEPLACEABLE_BALISTA            = 10101;

     // Branka and Caridin
     //moved public const int   CAI_DISABLE_COUNTERPART             = 10102;

     //==============================================================================
     /*
         FUNCTION IMPLEMENTATION
     */
     //==============================================================================
     /*
     * @brief Handles Global Custom AI functionality
     *
     * Used to handle Global Custom AI. These are things that multiple creatures
     * would use, which are re-used over and over. It makes sense to have them all
     * handled in one location to avoid duplicate coding.
     *
     * @param oCreature the creature to ahandle the custom ai for
     * @returns EngineConstants.TRUE if the Custom AI was handled; otherwise EngineConstants.FALSE
     *
     * @author   Joshua Stiksma
     *
     **/
     public int CAI_HandleGlobalCAI(GameObject oCreature, int nCustomAI)
     {

          int bCAIHandled = EngineConstants.FALSE;

          switch (nCustomAI)
          {

               case EngineConstants.CAI_RUNNER_RUN:
                    CAI_Runner_HandleRunToObject(oCreature);
                    bCAIHandled = EngineConstants.TRUE;
                    break;

               case EngineConstants.CAI_USEPLACEABLE_LYRIUM_VEIN:
                    CAI_UsePlaceable_NearestByTag(oCreature, "lyrium_vein");
                    bCAIHandled = EngineConstants.TRUE;
                    break;

               case EngineConstants.CAI_USEPLACEABLE_USEACTION:
                    CAI_UsePlaceable_Use(oCreature);
                    bCAIHandled = EngineConstants.TRUE;
                    break;

          }

          return bCAIHandled;

     }
}