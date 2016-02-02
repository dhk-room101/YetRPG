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
     //------------------------------------------------------------------------------
     // cir_ssf_h
     //------------------------------------------------------------------------------
     //
     // Function to set up the story so far when in the broken circle
     //
     //------------------------------------------------------------------------------
     // August 21, 2008 - Owner: Gary Stewart
     //------------------------------------------------------------------------------

     //#include"wrappers_h"

     //#include"plt_cir300pt_x"
     //#include"plt_cir300pt_fade"
     //#include"plt_cir000pt_main"
     //#include"plt_mnp00pt_ssf_circle"

     public void CIR_HandleStorySoFar()
     {

          int bStart = WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.GREAGOIR_CLOSES_DOOR);  // Player has been locked in the circle
          int bFade = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_X, EngineConstants.X_QUEST_DONE);          // Player had entered the raw fade.
          int bFadeEnd = WR_GetPlotFlag(EngineConstants.PLT_CIR300PT_FADE, EngineConstants.SLOTH_DEMON_DEFEATED); //The end of the fade
          int bMageEnd = WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.MAGES_IN_ARMY); //Ending, mage style                                             
          int bTemplarEnd = WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.TEMPLARS_IN_ARMY); //Ending, templar style  

          //Set flags in reverse order of events
          if (bTemplarEnd != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CIRCLE, EngineConstants.SSF_CIR_TEMPLAR_END, EngineConstants.TRUE);
          else if (bMageEnd != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CIRCLE, EngineConstants.SSF_CIR_MAGE_END, EngineConstants.TRUE);
          else if (bFadeEnd != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CIRCLE, EngineConstants.SSF_CIR_FADE_END, EngineConstants.TRUE);
          else if (bFade != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CIRCLE, EngineConstants.SSF_CIR_FADE, EngineConstants.TRUE);
          else if (bStart != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CIRCLE, EngineConstants.SSF_CIR_START, EngineConstants.TRUE);

     }
}