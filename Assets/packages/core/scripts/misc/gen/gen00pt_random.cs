#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;

public class gen00pt_random : xPlotConditional
{
    //:://////////////////////////////////////////////
    /*
        random checks

        //--------------------------------------------
        Added: Feb 6, 2007 (Josh)
        The format of the new LINES_LEFT flags is:

            GEN_RAND_RXX

        Where XX is the total number of random elements
        left in the series of randoms after the current line.
        Naturally, GEN_RAND_LINES_LEFT_01 has a 50% chance of
        being chosen. The very last dialogue line does
        not need a condition.
        //--------------------------------------------

    */
    //:://////////////////////////////////////////////
    //:: Created By: Yaron
    //:: Created On: July 11th, 2006
    //:://////////////////////////////////////////////


    /*# include "plt_gen00pt_random"
    # include "utility_h"
    # include "wrappers_h"
    # include "global_objects_h"
    # include "plot_h"*/

    Engine engine { get; set; }

    void Awake()
    {
        if (engine == null) engine = gameObject.GetComponent<Engine>();
    }

    public override int StartingConditional(xEvent eParms)
    {

        engine.Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "RANDOM HIT: BARK"); //DEBUG

        //xEvent eParms = engine.GetCurrentEvent();                // Contains all input parameters
        int nType = engine.GetEventTypeRef(ref eParms);               // GET or SET call
        string strPlot = engine.GetEventStringRef(ref eParms, 0);         // Plot GUID
        int nFlag = engine.GetEventIntegerRef(ref eParms, 1);          // The bit flag # being affected
        object oParty = engine.GetEventCreatorRef(ref eParms);      // The owner of the plot table for this script
        int nResult = EngineConstants.FALSE;

        engine.plot_GlobalPlotHandler(eParms); // any global plot operations, including debug info

        object oPC = engine.GetHero();

        if (nType == EngineConstants.EVENT_TYPE_SET_PLOT) // actions -> normal flags only
        {
            int nValue = engine.GetEventIntegerRef(ref eParms, 2);        // On SET call, the value about to be written
            int nOldValue = engine.GetEventIntegerRef(ref eParms, 3);     // On SET call, the current flag value

            switch (nFlag)
            {
                default: break;
            }
        }
        else // EVENT_TYPE_GET_PLOT -> defined conditions only
        {

            // Create a random float number between 0.00 and 99.99
            float fRandom = engine.RandomFloat() * 100.0f;

            switch (nFlag)
            {
                case EngineConstants.GEN_R5:
                    {
                        if (fRandom < 5.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }
                case EngineConstants.GEN_R10:
                    {
                        if (fRandom < 10.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }
                case EngineConstants.GEN_R25:
                    {
                        if (fRandom < 25.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }
                case EngineConstants.GEN_R30:
                    {
                        if (fRandom < 30.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }
                case EngineConstants.GEN_R33:
                    {
                        if (fRandom < 33.33f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }
                case EngineConstants.GEN_R50:
                    {
                        if (fRandom < 50.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                //------------------------------------------------------------------
                // Begin new case statements (Josh)
                //------------------------------------------------------------------

                case EngineConstants.GEN_RAND_LINES_LEFT_01:
                    {
                        if (fRandom < 50.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_02:
                    {
                        if (fRandom < 33.33f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_03:
                    {
                        if (fRandom < 25.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_04:
                    {
                        if (fRandom < 20.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_05:
                    {
                        if (fRandom < 16.67f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_06:
                    {
                        if (fRandom < 14.29f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_07:
                    {
                        if (fRandom < 12.50f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_08:
                    {
                        if (fRandom < 11.11f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_09:
                    {
                        if (fRandom < 10.00f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_10:
                    {
                        if (fRandom < 9.09f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_11:
                    {
                        if (fRandom < 8.33f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_12:
                    {
                        if (fRandom < 7.69f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_13:
                    {
                        if (fRandom < 7.14f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_14:
                    {
                        if (fRandom < 6.67f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                case EngineConstants.GEN_RAND_LINES_LEFT_15:
                    {
                        if (fRandom < 6.25f)
                            nResult = EngineConstants.TRUE;
                        break;
                    }

                //------------------------------------------------------------------
                // Begin Bark Case Structure
                //------------------------------------------------------------------

                case EngineConstants.GEN_BARK_STEN:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_STEN)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }

                case EngineConstants.GEN_BARK_OGHREN:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_OGHREN)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }

                case EngineConstants.GEN_BARK_ALISTAIR:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_ALISTAIR)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }

                case EngineConstants.GEN_BARK_WYNNE:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_WYNNE)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }

                case EngineConstants.GEN_BARK_LELIANA:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_LELIANA)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }

                case EngineConstants.GEN_BARK_MORRIGAN:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_MORRIGAN)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }

                case EngineConstants.GEN_BARK_ZEVRAN:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_ZEVRAN)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }

                case EngineConstants.GEN_BARK_LOGHAIN:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_LOGHAIN)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }

                case EngineConstants.GEN_BARK_SHALE:
                    {
                        if (engine.IsFollower(engine.GetObjectByTag(EngineConstants.GEN_FL_SHALE)) != EngineConstants.FALSE)
                        {
                            if (fRandom < 33.33f)
                                nResult = EngineConstants.TRUE;
                        }
                        break;
                    }
            }

        }
        engine.plot_OutputDefinedFlag(eParms, nResult);

        return nResult;
    }
}
