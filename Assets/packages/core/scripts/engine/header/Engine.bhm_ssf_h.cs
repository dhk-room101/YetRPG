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
     // Function to set up the story so far when in the mage origin
     //
     //------------------------------------------------------------------------------
     // August 21, 2008 - Owner: Gary Stewart
     //------------------------------------------------------------------------------

     //#include"wrappers_h"

     //#include"plt_bhm000pt_tranquility"
     //#include"plt_bhm600pt_harrowing"
     //#include"plt_mnp00pt_ssf_mage"

     public void BHM_HandleStorySoFar()
     {

          int nBHMStart = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_CIRCLE);
          int nLilyAndJowanJoin = WR_GetPlotFlag(EngineConstants.PLT_BHM000PT_TRANQUILITY, EngineConstants.JOWAN_AND_LILY_JOIN_PARTY);
          int nJowanHasInfo = WR_GetPlotFlag(EngineConstants.PLT_BHM000PT_TRANQUILITY, EngineConstants.JOWAN_HAS_INFO);
          int nRewarded = WR_GetPlotFlag(EngineConstants.PLT_BHM600PT_HARROWING, EngineConstants.MAGE_REWARD);
          int nBHMEnd = WR_GetPlotFlag(EngineConstants.PLT_BHM000PT_TRANQUILITY, EngineConstants.QUEST_COMPLETE);
          int nBHMHarrowing = WR_GetPlotFlag(EngineConstants.PLT_BHM600PT_HARROWING, EngineConstants.HARROWING_PASSED);

          ///////////////////////////////////////////////////////////////////////////////////
          // circle mage started and circle mage completed
          ////////////////////////////////////////////////////////////////////////////////////
          if ((nBHMStart != EngineConstants.FALSE) && (nBHMEnd != EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_MAGE, EngineConstants.SSF_BHM_END, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //We are in the tranquility mission
          else if ((nBHMStart != EngineConstants.FALSE) && (nLilyAndJowanJoin != EngineConstants.FALSE) && (nBHMEnd == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_MAGE, EngineConstants.SSF_BHM_TRANQUILITY, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //if we have seen jowan for the second time
          else if ((nBHMStart != EngineConstants.FALSE) && nJowanHasInfo != EngineConstants.FALSE && (nBHMEnd == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_MAGE, EngineConstants.SSF_BHM_HARROWING_PASSED, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          //If we have seen irving
          else if ((nBHMStart != EngineConstants.FALSE) && (nRewarded != EngineConstants.FALSE) && (nBHMEnd == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_MAGE, EngineConstants.SSF_BHM_MET_IRVING, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          ////////////////////////////////////////////////////////////////////////////////////
          // circle mage started, harrowing passed, circle mage not completed
          ////////////////////////////////////////////////////////////////////////////////////
          else if ((nBHMStart != EngineConstants.FALSE) && (nBHMHarrowing != EngineConstants.FALSE) && (nBHMEnd == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_MAGE, EngineConstants.SSF_BHM_HARROWING_OVER, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          ////////////////////////////////////////////////////////////////////////////////////
          // circle mage started but before harrowing passed
          ////////////////////////////////////////////////////////////////////////////////////
          else if ((nBHMStart != EngineConstants.FALSE) && (nBHMHarrowing == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_MAGE, EngineConstants.SSF_BHM_START, EngineConstants.TRUE, EngineConstants.TRUE);
          }

     }
}