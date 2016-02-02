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
     // World map and random encounters generic functions

     //#include"log_h"
     //#include"utility_h"

     //moved public const string RANDOM_ENCOUNTER_START_WAYPOINT = "start";
     //moved public const string RANDOM_ENCOUNTER_TRANSITION_ID = "rand"; // used in the wp field in area transition placeables to flag as a random ecounter (re-use stored transition)
     /*/moved public const string CAMP_EXIT_TRANSITION_ID = "camp_exit"; // used to exit the camp*/

     // Returns the target waypoint for travel based on 2da tables
     public string WM_GetWorldMapTargetWaypoint(int nWorldMap, string sSource, string sTarget)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "GetWorldMapTargetWaypoint", "START, worldmap: " + IntToString(nWorldMap) + ", source: " + sSource + ", Target: " + sTarget);
          int nWorldMap2da = GetM2DAInt(EngineConstants.TABLE_WORLD_MAPS, "TargetWpTableID", nWorldMap);

          int nRows = GetM2DARows(nWorldMap2da);
          string sWP = "";
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "GetWorldMapTargetWaypoint", "Targets table rows number: " + IntToString(nRows));
          int i;
          string sCurrentSource;
          int nCurrentRow;
          for (i = 0; i < nRows; i++)
          {
               nCurrentRow = GetM2DARowIdFromRowIndex(nWorldMap2da, i);
               sCurrentSource = GetM2DAString(nWorldMap2da, "SourceLocation", nCurrentRow);
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "GetWorldMapTargetWaypoint", "current row: " + IntToString(nCurrentRow) + ", current source: " + sCurrentSource);
               if (sCurrentSource == sSource || sCurrentSource == "default")
               {
                    sWP = GetM2DAString(nWorldMap2da, sTarget, nCurrentRow);
                    break;
               }
          }
          if (sWP != "")
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "GetWorldMapTargetWaypoint", "Found target WP: " + sWP);
          }
          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "GetWorldMapTargetWaypoint", "ERROR: could not find target WP", null, EngineConstants.LOG_SEVERITY_CRITICAL);

          return sWP;
     }

     // Checks if to run a random encounter.
     // If yes: run the encounter and return EngineConstants.TRUE
     // If no: return EngineConstants.FALSE
     public int WM_CheckRandomEncounter(int nWorldMap, int nTerrainType, GameObject oPreviousLocation = null)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "START, terrain type: " + IntToString(nTerrainType));

          // Find the table to use:
          string sEncounterTable = GetM2DAString(-1, "RandTable", nTerrainType, "terrain_types");
          int nEncounterChance = GetM2DAInt(-1, "RandChance", nTerrainType, "terrain_types");
          string sEncounterBitField = GetM2DAString(-1, "RepeatVar", nTerrainType, "terrain_types");
          string sTripsCounterTable = GetM2DAString(-1, "TripsCounterTable", nWorldMap, "worldmaps");
          int i;

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Encounter table: " + sEncounterTable +
              ", trigger chance: " + IntToString(nEncounterChance) + ", encounters bitfield var name: " + sEncounterBitField);

          int nEncounterBitField = GetLocalInt(GetModule(), sEncounterBitField);

          // Checking global chance for reading encounter table
          int nRand = Engine_Random(100) + 1;
          if (nRand > nEncounterChance)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Did not pass random encounter check, result: " + IntToString(nRand));
               return EngineConstants.FALSE;
          }

          // Checking chance for triggering random encounter based on the trips counter
          int nTripsCounter = GetLocalInt(GetModule(), EngineConstants.WORLD_MAP_TRIPS_COUNT);
          nTripsCounter++;
          SetLocalInt(GetModule(), EngineConstants.WORLD_MAP_TRIPS_COUNT, nTripsCounter);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Trips count: " + IntToString(nTripsCounter));
          int nTripsCounterTableRows = GetM2DARows(-1, sTripsCounterTable);
          if (nTripsCounter > nTripsCounterTableRows)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Not enough trips entries in table for current trip count - initializing trip count to 1");
               SetLocalInt(GetModule(), EngineConstants.WORLD_MAP_TRIPS_COUNT, 1);
          }
          // Get the encounter chance for this specific trip:
          int nEncounterChanceByTrip = GetM2DAInt(-1, "EncounterChance", nTripsCounter, sTripsCounterTable);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Encounter chance based on trip counte: " + IntToString(nEncounterChanceByTrip) + "%");
          nRand = Engine_Random(100) + 1;
          if (nRand > nEncounterChanceByTrip)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Did not pass random encounter check by trip, result: " + IntToString(nRand));
               return EngineConstants.FALSE;
          }

          // Start checking all lines in the table, one by one, until a valid encounter is found
          int nRows = GetM2DARows(-1, sEncounterTable);
          string sLabel;
          int nTriggerChance;
          int nRepeat;
          string sTriggerCondPlot;
          int nTriggerCondFlag;
          int nTriggerFlagSet;
          string sArea;
          int nBitPosition;
          int nCanRunForPRC;

          for (i = 0; i < nRows; i++)
          {
               sLabel = GetM2DAString(-1, "Label", i, sEncounterTable);
               nTriggerChance = GetM2DAInt(-1, "TriggerChance", i, sEncounterTable);
               nRepeat = GetM2DAInt(-1, "Repeat", i, sEncounterTable);
               sTriggerCondPlot = GetM2DAString(-1, "TriggerCondPlot", i, sEncounterTable);
               nTriggerCondFlag = GetM2DAInt(-1, "TriggerPlotFlag", i, sEncounterTable);
               nTriggerFlagSet = GetM2DAInt(-1, "TriggerFlagSet", i, sEncounterTable);
               sArea = GetM2DAString(-1, "Area", i, sEncounterTable);
               nCanRunForPRC = GetM2DAInt(-1, "CanRunForPRC", i, sEncounterTable);
               nBitPosition = Power(2, i - 1);  // needed for non-repeatable encounters

               if (nTriggerChance == 0)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "INVALID entry - skipping" + IntToString(nRand));
                    continue; // invalid entry
               }
               if (nCanRunForPRC == 0)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "encounter disabled by PRC override");
                    continue; // invalid entry
               }

               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Encounter data: Label: " + sLabel +
                                                                           ", Chance: " + IntToString(nTriggerChance) +
                                                                           ", Rep: " + IntToString(nRepeat) +
                                                                           ", Plot: " + sTriggerCondPlot +
                                                                           ", Flag: " + IntToString(nTriggerCondFlag) +
                                                                           ", set/unset: " + IntToString(nTriggerFlagSet) +
                                                                           ", Area: " + sArea);

               // Random check
               nRand = Engine_Random(100) + 1;
               if (nRand > nTriggerChance)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Encounter did not pass random check, result: " + IntToString(nRand));
                    continue;
               }

               // if not allowing repeat - check if the encounter was triggered before
               if (nRepeat == EngineConstants.FALSE)
               {
                    // Find bit field position for this encounter
                    // The bit field position is equal to the encounter ID (i) - which should be converted into a binary
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Encounter bit position: " + IntToString(nBitPosition));
                    if (nEncounterBitField == nBitPosition)
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Non-repeatable encounter triggered before - aborting");
                         continue;
                    }
               }

               // Check plot condition
               if (sTriggerCondPlot != "")
               {
                    if (nTriggerFlagSet != EngineConstants.FALSE) // encounter will be aborted if the flag is NOT SET
                    {
                         if (WR_GetPlotFlag(sTriggerCondPlot, nTriggerCondFlag) == EngineConstants.FALSE)
                         {
                              Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Plot flag NOT SET - aborting");
                              continue;
                         }
                    }
                    else // encounter will be aborted if the flag is SET
                    {
                         if (WR_GetPlotFlag(sTriggerCondPlot, nTriggerCondFlag) != EngineConstants.FALSE)
                         {
                              Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Plot flag SET - aborting");
                              continue;
                         }
                    }

               }

               // Encounter is being triggered - set bitfield flag:
               nEncounterBitField = nEncounterBitField | nBitPosition;
               SetLocalInt(GetModule(), sEncounterBitField, nEncounterBitField);

               // And finally - trigger the encounter
               //UT_DoAreaTransition(sArea, RANDOM_ENCOUNTER_START_WAYPOINT);
               WorldMapStartTravelling(sArea, EngineConstants.RANDOM_ENCOUNTER_START_WAYPOINT, oPreviousLocation);
               return EngineConstants.TRUE;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_CheckRandomEncounter", "Could not trigger any random encounter");

          return EngineConstants.FALSE; // no encounter

     }

     public void WM_SetWorldMapGuiStatus()
     {
          int nAreaWorldMapEnabled = GetLocalInt(GetArea(GetHero()), EngineConstants.AREA_WORLD_MAP_ENABLED);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_SetWorldMapGuiStatus", "Area world map enabled: " + IntToString(nAreaWorldMapEnabled));

          int nModuleWorldMapEnabled = GetLocalInt(GetModule(), EngineConstants.MODULE_WORLD_MAP_ENABLED);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "WM_SetWorldMapGuiStatus", "Module world map enabled: " + IntToString(nModuleWorldMapEnabled));

          if (nAreaWorldMapEnabled != EngineConstants.FALSE && nModuleWorldMapEnabled != EngineConstants.FALSE)
               SetWorldMapGuiStatus(EngineConstants.WM_GUI_STATUS_USE);
          else if (nModuleWorldMapEnabled != EngineConstants.FALSE && nAreaWorldMapEnabled == EngineConstants.FALSE)
               SetWorldMapGuiStatus(EngineConstants.WM_GUI_STATUS_READ_ONLY);
          else // module map not opened
               SetWorldMapGuiStatus(EngineConstants.WM_GUI_STATUS_NO_USE);
     }

     public void WM_SetPartyPickerGuiStatus()
     {
          GameObject oPC = GetHero();
          GameObject oArea = GetArea(oPC);
          int nPartyPickerEnabled = GetLocalInt(oArea, EngineConstants.PARTY_PICKER_ENABLED);

          if (GetLocalInt(oArea, EngineConstants.AREA_DEBUG) == 1)
               nPartyPickerEnabled = EngineConstants.TRUE;

          if (nPartyPickerEnabled != EngineConstants.FALSE)
               SetPartyPickerGUIStatus(EngineConstants.PP_GUI_STATUS_USE);
          else if (GetLocalInt(oArea, EngineConstants.AREA_PARTY_CAMP) == 1)
               SetPartyPickerGUIStatus(EngineConstants.PP_GUI_STATUS_NO_USE);
          else
               SetPartyPickerGUIStatus(EngineConstants.PP_GUI_STATUS_NO_USE);
     }
}