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

public class cir000cr_team_core : MonoBehaviour
{
    //==============================================================================
    /*

        Broken Circle
         -> Team Core Creature Script

    */
    //------------------------------------------------------------------------------
    // Created By: Joshua Stiksma
    // Created On: March 07, 2008
    //==============================================================================

    //#include"plt_cir330pt_cursed_mage"
    //#include"plt_cir320pt_templar_spirit"
    //#include"plt_cir310pt_templar_dream"
    //#include"plt_cir300pt_mage_apprentic"
    //#include"plt_cir300pt_fade"

    //#include"cir_constants_h"
    //#include"cir_functions_h"

    //#include"wrappers_h"
    //#include"utility_h"
    //#include"plot_h"
    Engine engine { get; set; }

    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     public void HandleEvent(xEvent ev)
     {

          //--------------------------------------------------------------------------
          // Initialization
          //--------------------------------------------------------------------------

          // Load engine.Event Variables
          //xEvent ev = engine.GetCurrentEvent();            // engine.Event parameters
          int nEventType = engine.GetEventTypeRef(ref ev);        // engine.Event type triggered
          GameObject nEventOwner = engine.GetEventCreatorRef(ref ev);     // Triggering character
          GameObject oPC = engine.GetHero();                    // Player character

          int bEventHandled = EngineConstants.FALSE;

          //--------------------------------------------------------------------------
          // engine.Events
          //--------------------------------------------------------------------------

          switch (nEventType)
          {

               case EngineConstants.EVENT_TYPE_TEAM_DESTROYED:
                    {

                         int nTeamID = engine.GetEventIntegerRef(ref ev, 0);

                         switch (nTeamID)
                         {

                              case EngineConstants.CIR_TEAM_DUNCAN_WARDENS:
                                   {

                                        //----------------------------------------------------------
                                        // CIR_TEAM_DUNCAN_WARDENS:
                                        // TEAM 2
                                        //----------------------------------------------------------

                                        engine.WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_FADE, EngineConstants.DUNCAN_KILLED, EngineConstants.TRUE, EngineConstants.TRUE);

                                        break;

                                   }

                              case EngineConstants.CIR_TEAM_MOUSE_ATTACKERS:
                                   {

                                        //----------------------------------------------------------
                                        // CIR_TEAM_MOUSE_ATTACKERS:
                                        // TEAM 3
                                        //----------------------------------------------------------

                                        engine.WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_MAGE_APPRENTIC, EngineConstants.CIR_MAGE_APPRENTICE_SAVED, EngineConstants.TRUE, EngineConstants.TRUE);

                                        break;

                                   }

                              case EngineConstants.CIR_TEAM_DARKSPAWN_SPIRITS:
                                   {

                                        //----------------------------------------------------------
                                        // CIR_TEAM_DARKSPAWN_SPIRITS:
                                        // TEAM 4
                                        //----------------------------------------------------------

                                        engine.WR_SetPlotFlag(EngineConstants.PLT_CIR320PT_TEMPLAR_SPIRIT, EngineConstants.CIR_TEMPLAR_SPIRIT_SAVED, EngineConstants.TRUE, EngineConstants.TRUE);

                                        break;

                                   }

                              case EngineConstants.CIR_TEAM_CHANTRY_PRIESTS:
                                   {

                                        //----------------------------------------------------------
                                        // CIR_TEAM_CHANTRY_PRIESTS:
                                        // TEAM 6
                                        //----------------------------------------------------------

                                        engine.WR_SetPlotFlag(EngineConstants.PLT_CIR330PT_CURSED_MAGE, EngineConstants.CIR_CURSED_MAGE_SAVED, EngineConstants.TRUE, EngineConstants.TRUE);

                                        break;

                                   }

                         }

                         bEventHandled = EngineConstants.TRUE;

                         break;

                    }

          }

          // -------------------------------------------------------------------------
          // Any xEvent not handled is also handled by creature_core:
          // -------------------------------------------------------------------------

          if (bEventHandled == EngineConstants.FALSE)
               engine.HandleEventRef(ref ev, EngineConstants.RESOURCE_SCRIPT_CREATURE_CORE);

     }
}