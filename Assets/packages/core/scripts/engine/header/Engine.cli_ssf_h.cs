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
     // Climax story so far include

     //#include"plt_mnp00pt_ssf_climax"
     //#include"plt_clipt_main"
     //#include"plt_clipt_morrigan_ritual"
     //#include"wrappers_h"

     public void CLI_HandleStorySoFar()
     {
          int nPCKnowsAboutArchdemon = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MAIN, EngineConstants.CLI_MAIN_RIORDAN_GAVE_ARCHDEMON_INFO);
          int nPCKnowsAboutRitual = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MORRIGAN_RITUAL, EngineConstants.MORRIGAN_RITUAL_KNOWN);
          int nRitualRefused = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MORRIGAN_RITUAL, EngineConstants.MORRIGAN_RITUAL_REFUSED);
          int nRitualAlistair = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MORRIGAN_RITUAL, EngineConstants.MORRIGAN_RITUAL_WITH_ALISTAIR);
          int nRitualLoghain = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MORRIGAN_RITUAL, EngineConstants.MORRIGAN_RITUAL_WITH_LOGHAIN);
          int nRitualPlayer = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MORRIGAN_RITUAL, EngineConstants.MORRIGAN_RITUAL_WITH_PLAYER);
          int nInDenerim = WR_GetPlotFlag(EngineConstants.PLT_CLIPT_MAIN, EngineConstants.CLI_MAIN_AT_CITY_GATES);

          if (nPCKnowsAboutArchdemon == EngineConstants.FALSE)
               return;

          if (nInDenerim != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CLIMAX, EngineConstants.SSF_CLIMAX_IN_DENERIM, EngineConstants.TRUE);
          else if (nRitualRefused != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CLIMAX, EngineConstants.SSF_CLIMAX_RITUAL_REFUSED, EngineConstants.TRUE);
          else if (nRitualAlistair != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CLIMAX, EngineConstants.SSF_CLIMAX_RITUAL_ALISTAIR, EngineConstants.TRUE);
          else if (nRitualLoghain != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CLIMAX, EngineConstants.SSF_CLIMAX_RITUAL_LOGHAIN, EngineConstants.TRUE);
          else if (nRitualPlayer != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CLIMAX, EngineConstants.SSF_CLIMAX_RITUAL_PLAYER, EngineConstants.TRUE);
          else if (nPCKnowsAboutRitual != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CLIMAX, EngineConstants.SSF_CLIMAX_PC_KNOWS_ABOUT_RITUAL, EngineConstants.TRUE);
          if (nPCKnowsAboutArchdemon != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_CLIMAX, EngineConstants.SSF_CLIMAX_PC_KNOWS_ABOUT_ARCHDEMON, EngineConstants.TRUE);


     }
}