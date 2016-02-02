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
     // urn_ssf_h
     //------------------------------------------------------------------------------
     //
     // Function to set up the story so far when pursuing the Urn plot.
     //
     //------------------------------------------------------------------------------
     // May 1, 2008 - Owner: Grant Mackay
     //------------------------------------------------------------------------------

     //#include"wrappers_h"

     //#include"plt_mnp000pt_ssf_sacred_urn"
     //#include"plt_urn100pt_haven"
     //#include"plt_urn200pt_cult"
     //#include"plt_urn200pt_temple"
     //#include"plt_urnpt_main"

     public void URN_HandleStorySoFar()
     {

          // Player knows Genitivi is missing.
          int bStart = WR_GetPlotFlag(EngineConstants.PLT_URNPT_MAIN, EngineConstants.HEARD_GENITIVI_IS_MISSING);

          // Player knows the Vector3 of haven.
          int bHaven = WR_GetPlotFlag(EngineConstants.PLT_URNPT_MAIN, EngineConstants.HAVEN_OPENED);

          // The player has discovered the Vector3 of the Ruined Temple.
          int bTemple1 = WR_GetPlotFlag(EngineConstants.PLT_URNPT_MAIN, EngineConstants.GENITIVI_DENIED) != EngineConstants.FALSE || 
               WR_GetPlotFlag(EngineConstants.PLT_URN100PT_HAVEN, EngineConstants.GENIVITI_TRANSPORTS_TO_TEMPLE) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;

          // The player has delved into the temple and met Kolgrim.
          int bTemple2 = WR_GetPlotFlag(EngineConstants.PLT_URN200PT_CULT, EngineConstants.KOLGRIM_OFFER_REFUSED) != EngineConstants.FALSE || 
               WR_GetPlotFlag(EngineConstants.PLT_URN200PT_CULT, EngineConstants.KOLGRIM_OFFER_TO_TAINT_ASHES_ACCEPTED) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;

          // The player has tainted the ashes.
          int bAshes1 = WR_GetPlotFlag(EngineConstants.PLT_URN200PT_CULT, EngineConstants.URN_TAINTED);

          // The player has recovered the ashes.
          int bAshes2 = WR_GetPlotFlag(EngineConstants.PLT_URN200PT_TEMPLE, EngineConstants.PC_HAS_ASHES);


          if (bAshes1 != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_SSF_SACRED_URN, EngineConstants.SSF_URN_ASHES_1, EngineConstants.TRUE);
          else if (bAshes2 != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_SSF_SACRED_URN, EngineConstants.SSF_URN_ASHES_2, EngineConstants.TRUE);
          else if (bTemple2 != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_SSF_SACRED_URN, EngineConstants.SSF_URN_RUINED_TEMPLE_2, EngineConstants.TRUE);
          else if (bTemple1 != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_SSF_SACRED_URN, EngineConstants.SSF_URN_RUINED_TEMPLE_1, EngineConstants.TRUE);
          else if (bHaven != EngineConstants.FALSE) WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_SSF_SACRED_URN, EngineConstants.SSF_URN_TO_HAVEN, EngineConstants.TRUE);
          else WR_SetPlotFlag(EngineConstants.PLT_MNP000PT_SSF_SACRED_URN, EngineConstants.SSF_URN_START, EngineConstants.TRUE);

     }
}