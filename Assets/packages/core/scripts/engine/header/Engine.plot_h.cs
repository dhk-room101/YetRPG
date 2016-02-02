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
     /* @addtogroup scripting_plot Scripting Plot
     *
     * Generic plot handling functions
     */
     /* @{*/

     //#include"log_h"
     //#include"wrappers_h"
     //#include"sys_rewards_h"

     //public void main() {}

     /* @brief Handles global plot flag events
*
* This function handles any global procedures regarding plots in the game.
* Currently this includes only debug messages.
*
* @param ePlotEvent the plot xEvent to be handled
* @author Yaron
*/
     public void plot_GlobalPlotHandler(xEvent ePlotEvent)
     {
          int nType = GetEventTypeRef(ref ePlotEvent);               // GET or SET call
          string strPlot = GetEventStringRef(ref ePlotEvent, 0);         // Plot GUID
          int nFlag = GetEventIntegerRef(ref ePlotEvent, 1);          // The bit flag # being affected
          int nValue = GetEventIntegerRef(ref ePlotEvent, 2);        // On SET call, the value about to be written (on a normal SET that should be '1', and on a 'clear' it should be '0')
          int nOldValue = GetEventIntegerRef(ref ePlotEvent, 3);     // On SET call, the current flag value (can be either 1 or 0 regardless if it's a set or clear event)
          int nDefined = GetEventIntegerRef(ref ePlotEvent, 4); // 1 -> defined, 0 -> not defined
          GameObject oParty = GetEventCreatorRef(ref ePlotEvent);      // The owner of the plot table for this script
          GameObject oConversationOwner = GetEventObjectRef(ref ePlotEvent, 0); // Owner on the conversation, if any
          string sPlotResRef = GetEventStringRef(ref ePlotEvent, 2);

          int nGetResult = EngineConstants.FALSE;
          GameObject oPC = gameObject;
          string sPlotName = GetPlotResRef(strPlot);
          string sFlagName = GetPlotFlagName(strPlot, nFlag);

          if (nType == EngineConstants.EVENT_TYPE_SET_PLOT) // SET
          {


               if (IsObjectValid(oConversationOwner) != EngineConstants.FALSE
                    && GetObjectType(oConversationOwner) == EngineConstants.OBJECT_TYPE_ITEM)
               {
                    Warning("Ugh. The owner for this conversation is an EngineConstants.ITEM. This is bad. Please call Yaron over right now...");
               }

               LogTrace(EngineConstants.LOG_CHANNEL_PLOT, "SetPlot [" + sPlotName + "] ["
                   + sFlagName + "] [" + ToString(nOldValue) + " -> " + ToString(nValue) + "]");
               Log_Trace_Plot("plot_h.plot_GlobalPlotHandler", sPlotResRef, nFlag, nOldValue, nValue);

               RewardDistibuteByPlotFlag(strPlot, nFlag);

               //TrackPlotEvent(nType, oConversationOwner, oParty, nValue, nOldValue, sPlotName, sFlagName);
          }
          else // GET
          {
               string sFlagValue;
               if (nDefined == EngineConstants.FALSE) // get the real current flag value
               {
                    nGetResult = WR_GetPlotFlag(strPlot, nFlag);
                    sFlagValue = IntToString(nGetResult);

                    // No need to print anything since the wrapper above will take care of that

                    //LogTrace(EngineConstants.LOG_CHANNEL_PLOT, "GetPlot [" + sPlotName + "] ["
                    //    + sFlagName + "] = [" + ToString(nGetResult) + "]");
                    Log_Trace_Plot("plot_h.plot_GlobalPlotHandler", sPlotResRef, nFlag, nGetResult);
               }
               else
               {

                    // Now print the Deja Log message
                    string sState = "EngineConstants.FALSE";
                    if (nGetResult != EngineConstants.FALSE) sState = "EngineConstants.TRUE";

                    sFlagValue = "(DEFINED FLAG)";
                    LogTrace(EngineConstants.LOG_CHANNEL_PLOT, "GetPlot [" + sPlotName + "] ["
                        + sFlagName + "] = [ DEFINED ]");
                    Log_Trace_Plot("plot_h.plot_GlobalPlotHandler", sPlotResRef + " " + sFlagValue, nFlag, nGetResult);

               }

          }
     }

     /* @brief Output defined flag information
*
*
* @param ePlotEvent the plot xEvent to be handled
* @param nResult the value of the defined flag
* @author Yaron
*/
     public void plot_OutputDefinedFlag(xEvent ePlotEvent, int nResult)
     {
          int nType = GetEventTypeRef(ref ePlotEvent);               // GET or SET call
          string strPlot = GetEventStringRef(ref ePlotEvent, 0);         // Plot GUID
          int nFlag = GetEventIntegerRef(ref ePlotEvent, 1);          // The bit flag # being affected
          int nValue = GetEventIntegerRef(ref ePlotEvent, 2);        // On SET call, the value about to be written (on a normal SET that should be '1', and on a 'clear' it should be '0')
          int nOldValue = GetEventIntegerRef(ref ePlotEvent, 3);     // On SET call, the current flag value (can be either 1 or 0 regardless if it's a set or clear event)
          int nDefined = GetEventIntegerRef(ref ePlotEvent, 4); // 1 -> defined, 0 -> not defined
          GameObject oParty = GetEventCreatorRef(ref ePlotEvent);      // The owner of the plot table for this script
          GameObject oConversationOwner = GetEventObjectRef(ref ePlotEvent, 0); // Owner on the conversation, if any
          string sPlotResRef = GetEventStringRef(ref ePlotEvent, 2);

          string sPlotName = GetPlotResRef(strPlot);
          string sFlagName = GetPlotFlagName(strPlot, nFlag);

          if (nDefined != EngineConstants.FALSE && nType == EngineConstants.EVENT_TYPE_GET_PLOT) // printing only for defined flags
          {
               LogTrace(EngineConstants.LOG_CHANNEL_PLOT, "GetPlot *DEFINED* [" + sPlotName + "] ["
                       + sFlagName + "] [value of DEFINED flag after evaluation: " + IntToString(nResult) + "]");
          }
     }

     /* @} */
}