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
     //==============================================================================
     /*

         Paragon of Her Kind
          -> Generic Codex Functions Script

         These are the generic functions for the codex plots in Paragon.

     */
     //------------------------------------------------------------------------------
     // Created By: Grant Mackay
     // Created On: October 28, 2008
     //==============================================================================

     //#include"wrappers_h"

     //#include"plt_cod_hst_orz_dead_caste"
     //#include"plt_cod_hst_orz_drifters"
     //#include"plt_cod_hst_orz_topsider"
     //#include"plt_cod_hst_orz_shaper"  
     //#include"plt_cod_hst_orz_key"

     //------------------------------------------------------------------------------
     // FUNCTION IMPLEMENTATIONS
     //------------------------------------------------------------------------------

     /*
* @brief Determines if the codex set is complete.
*
* @param sPlot    The codex plot in question.
* @param nEntries The number of codex entries in the set.
* @param nNth     The nNth codex in the series being added when checking.
*
* @author Grant Mackay
*/
     public int CheckCodexComplete(string sPlot, int nEntries, int nNth)
     {

          int nFlag, bCondition1, bCondition2;

          for (nFlag = 0; nFlag < nEntries; ++nFlag)
          {

               // Not already set and not being set right now.
               bCondition1 = WR_GetPlotFlag(sPlot, nFlag);
               bCondition2 = (nFlag == nNth) ? EngineConstants.TRUE : EngineConstants.FALSE;

               if (bCondition1 == EngineConstants.FALSE && bCondition2 == EngineConstants.FALSE)
                    return EngineConstants.FALSE;

          }

          return EngineConstants.TRUE;

     }
}