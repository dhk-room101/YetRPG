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

         Proving General Script

         Events and the order they happen:
         ----------------------------------------------------------------------------
         | ENTER:    When the PC First enters the Arena
         | START:    When the PC begins combat (NPC's become hostile)
         | WIN:      All NPC Opponents are dead, PC Wins
         | LOSE:     PC's Party is dead, PC Loses
         | EXIT:     After the PC is teleported outside the Arena
         ----------------------------------------------------------------------------

         Variables Reserved: EngineConstants.CREATURE_DO_ONCE_B - used on proving created creatures

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: April 17, 2007
     //==============================================================================

     //#include"wrappers_h"
     //#include"utility_h"
     //#include"plot_h"
     //#include"events_h"
     //#include"2da_constants_h"

     //------------------------------------------------------------------------------
     // Modifiable Constants
     //------------------------------------------------------------------------------

     // Fight ID Constants. These should be updated here as well as in proving.2da
     // Moved to genpt_proving plot

     //------------------------------------------------------------------------------
     // Required Constants
     //------------------------------------------------------------------------------

     //moved public const int       EngineConstants.PROVING_MAX_ALLIES                  = 11;
     //moved public const int       EngineConstants.PROVING_MAX_OPPONENTS               = 20;

     //moved public const int       EngineConstants.PROVING_TEAM_ALLIES                 = 10001;
     //moved public const int       EngineConstants.PROVING_TEAM_OPPONENTS              = 10002;
     //moved public const int       EngineConstants.PROVING_TEAM_NON_COMBATANT          = 10003;

     //moved public const string    EngineConstants.PROVING_NULL                        = "";
     //moved public const string    EngineConstants.PROVING_FIGHT_ID                    = "PROVING_FIGHT_ID";
     //moved public const string    EngineConstants.PROVING_ALLY_ITERATOR               = "PROVING_ALLY_ITERATOR";
     //moved public const string    EngineConstants.PROVING_OPPONENT_ITERATOR           = "PROVING_OPPONENT_ITERATOR";
     //moved public const string    EngineConstants.PROVING_DEAD_OPPONENT_TAG           = "gen000cr_prov_dead";

     //moved public const string    EngineConstants.PROVING_WP_PC_ENTER                 = "proving_pc_enter";
     //moved public const string    EngineConstants.PROVING_WP_PC_EXIT                  = "proving_pc_exit";
     //moved public const string    EngineConstants.PROVING_WP_OPPONENT_ENTER_PREFIX    = "proving_opp_";
     //moved public const string    EngineConstants.PROVING_WP_ALLY_ENTER_PREFIX        = "proving_ally_";

     //moved public const string    EngineConstants.PROVING_IP_PROVING_CROWD            = "genip_proving_crowd";

     //moved public const string    EngineConstants.PROVING_SCRIPT                      = "ProvingScript";
     //moved public const string    EngineConstants.PROVING_CONVERSATION_ENTER                = "DialogCreatureEnter";
     //moved public const string    EngineConstants.PROVING_CONVERSATION_START                = "DialogCreatureStart";
     //moved public const string    EngineConstants.PROVING_CONVERSATION_WIN                  = "DialogCreatureWin";
     //moved public const string    EngineConstants.PROVING_CONVERSATION_LOSE                 = "DialogCreatureLose";
     //moved public const string    EngineConstants.PROVING_CONVERSATION_EXIT                 = "DialogCreatureExit";
     //moved public const string    EngineConstants.PROVING_SET_PLOT_ENTER              = "SetPlotEnter";
     //moved public const string    EngineConstants.PROVING_SET_FLAG_ENTER              = "SetFlagEnter";
     //moved public const string    EngineConstants.PROVING_SET_PLOT_START              = "SetPlotStart";
     //moved public const string    EngineConstants.PROVING_SET_FLAG_START              = "SetFlagStart";
     //moved public const string    EngineConstants.PROVING_SET_PLOT_WIN                = "SetPlotWin";
     //moved public const string    EngineConstants.PROVING_SET_FLAG_WIN                = "SetFlagWin";
     //moved public const string    EngineConstants.PROVING_SET_PLOT_LOSE               = "SetPlotLose";
     //moved public const string    EngineConstants.PROVING_SET_FLAG_LOSE               = "SetFlagLose";
     //moved public const string    EngineConstants.PROVING_SET_PLOT_EXIT               = "SetPlotExit";
     //moved public const string    EngineConstants.PROVING_SET_FLAG_EXIT               = "SetFlagExit";
     //moved public const string    EngineConstants.PROVING_ALLY_PREFIX                 = "Ally_";
     //moved public const string    EngineConstants.PROVING_OPPONENT_PREFIX             = "Opponent_";

     //moved public const string  EngineConstants.RESOURCE_SCRIPT_PROVING_CORE        = "proving_core.nss";

     //------------------------------------------------------------------------------
     // Function Defintions
     //------------------------------------------------------------------------------

     //------------------------------------------------------------------------------
     // Proving_Enter
     //------------------------------------------------------------------------------
     /* @brief Causes PC to Enter a Proving Arena Match
* @param nFightID the corresponding match you want to start
* @returns <nothing>
* @author joshua
*/
     public void Proving_Enter(int nFightID)
     {

          Proving_Log("Proving_Enter()");

          // Make sure the Fight ID is valid
          if (nFightID <= 0)
          {
               Proving_Log("Proving_EnterArena()", "Invalid FightID: " + ToString(nFightID));
               return;
          }

          // Set the current Fight ID, so we can know how to clean up after
          SetLocalInt(GetModule(), EngineConstants.PROVING_FIGHT_ID, nFightID);

          // Handle events
          Proving_HandleEvents(nFightID, EngineConstants.EVENT_TYPE_PROVING_ENTER);

     }

     //------------------------------------------------------------------------------
     // Proving_Start
     //------------------------------------------------------------------------------
     /* @brief Starts combat events for Proving Arena Match
* @author joshua
*/
     public void Proving_Start()
     {

          Proving_Log("Proving_Start()");

          // Grab the current fight ID to reference on the 2DA
          int nFightID = Proving_GetCurrentFightId();

          // This function depends entirely on the fact that a fight has been started
          // If it has not, we want to error
          if (nFightID <= 0)
          {
               Proving_Log("Proving_Start()", "Invalid FightID: " + ToString(nFightID));
               return;
          }

          // Handle events
          Proving_HandleEvents(nFightID, EngineConstants.EVENT_TYPE_PROVING_START);

     }

     //------------------------------------------------------------------------------
     // Proving_Win
     //------------------------------------------------------------------------------
     /* @brief Starts Win events for Proving Arena Match
     * @author joshua
     */
     public void Proving_Win()
     {

          Proving_Log("Proving_Win()");

          // Grab the current fight ID to reference on the 2DA
          int nFightID = Proving_GetCurrentFightId();

          // Take all opponents out of combat
          Proving_SetFightersHostile(nFightID, EngineConstants.FALSE);

          // This function depends entirely on the fact that a fight has been started
          if (nFightID <= 0)
          {
               Proving_Log("Proving_Win()", "Invalid FightID: " + ToString(nFightID));
               return;
          }

          // Handle events
          Proving_HandleEvents(nFightID, EngineConstants.EVENT_TYPE_PROVING_WIN);

     }

     //------------------------------------------------------------------------------
     // Proving_Lose
     //------------------------------------------------------------------------------
     /* @brief Starts Lose events for Proving Arena Match
     * @author joshua
     */
     public void Proving_Lose()
     {

          Proving_Log("Proving_Lose()");

          // Grab the current fight ID to reference on the 2DA
          int nFightID = Proving_GetCurrentFightId();

          // Take all opponents out of combat
          Proving_SetFightersHostile(nFightID, EngineConstants.FALSE);

          // This function depends entirely on the fact that a fight has been started
          if (nFightID <= 0)
          {
               Proving_Log("Proving_Lose()", "Invalid FightID: " + ToString(nFightID));
               return;
          }

          // Handle events
          Proving_HandleEvents(nFightID, EngineConstants.EVENT_TYPE_PROVING_LOSE);

     }

     //------------------------------------------------------------------------------
     // Proving_Exit
     //------------------------------------------------------------------------------
     /* @brief Exits PC from Proving Arena Match
     * @author joshua
     */
     public void Proving_Exit()
     {

          Proving_Log("Proving_Exit()");

          // Grab the current fight ID to reference on the 2DA
          int nFightID = Proving_GetCurrentFightId();

          // This function depends entirely on the fact that a fight has been started
          // If it has not, we want to error
          if (nFightID <= 0)
          {
               Proving_Log("Proving_Exit()", "Invalid FightID: " + ToString(nFightID));
               return;
          }

          // Handle events
          Proving_HandleEvents(nFightID, EngineConstants.EVENT_TYPE_PROVING_EXIT);

          // Set the current Fight ID to -1 (Invalid)
          SetLocalInt(GetModule(), EngineConstants.PROVING_FIGHT_ID, -1);

     }

     //------------------------------------------------------------------------------
     // Proving_SetupFighters
     //------------------------------------------------------------------------------
     /* @brief Adds fighters to the Arena for the Current Fight ID
     * @param nFightID the Fight ID to use
     *
     * Opponents are added to the Team: EngineConstants.PROVING_TEAM_OPPONENTS (30).
     * Allies are added to the Team: EngineConstants.PROVING_TEAM_ALLIES (31).
     *
     * @author joshua
     */
     public void Proving_SetupFighters(int nFightID)
     {

          int nIndex;             // counter
          int nArraySize;         // number of fighters in the battle
          GameObject oPC;                // PC object
          GameObject oFighter;           // Fighter Object
          GameObject oWaypoint;          // Fighter Object' start Waypoint

          //--------------------------------------------------------------------------

          oPC = GetHero();

          //--------------------------------------------------------------------------

          Proving_Log("Proving_SetupFighters()");

          // Activate Opponents
          nArraySize = Proving_GetNumOpponents(nFightID);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oWaypoint = UT_GetNearestObjectByTag(oPC, EngineConstants.PROVING_WP_OPPONENT_ENTER_PREFIX + (nIndex < 10 ? "0" : "") + IntToString(nIndex));
               oFighter = UT_GetNearestCreatureByTag(oWaypoint, Proving_GetOpponent(nFightID, nIndex));
               UT_LocalJump(oFighter, GetTag(oWaypoint));
               WR_SetObjectActive(oFighter, EngineConstants.TRUE);
               RemoveEffectsDueToPlotEvent(oFighter);
               SetTeamId(oFighter, EngineConstants.PROVING_TEAM_OPPONENTS);
          }

          // Activate Allies
          nArraySize = Proving_GetNumAllies(nFightID);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oWaypoint = UT_GetNearestObjectByTag(oPC, EngineConstants.PROVING_WP_ALLY_ENTER_PREFIX + (nIndex < 10 ? "0" : "") + IntToString(nIndex));
               oFighter = UT_GetNearestCreatureByTag(oWaypoint, Proving_GetAlly(nFightID, nIndex));
               UT_LocalJump(oFighter, GetTag(oWaypoint));
               WR_SetObjectActive(oFighter, EngineConstants.TRUE);
               RemoveEffectsDueToPlotEvent(oFighter);
               SetTeamId(oFighter, EngineConstants.PROVING_TEAM_ALLIES);
          }

     }

     //------------------------------------------------------------------------------
     // Proving_RemoveFighters
     //------------------------------------------------------------------------------
     /* @brief Removes fighters from the Arena for the Current Fight ID
     * @param nFightID the Fight ID to use
     * @author joshua
     */
     public void Proving_RemoveFighters(int nFightID)
     {

          int nIndex;         // counter
          int nArraySize;     // number of fighters in the battle
          GameObject oFighter;
          GameObject oEnterWP;       // This is used to grab the creatures closest
                                     // to the arena in case there are duplicates
                                     // outside

          //--------------------------------------------------------------------------

          oEnterWP = UT_GetNearestObjectByTag(GetHero(), EngineConstants.PROVING_WP_PC_ENTER);

          //--------------------------------------------------------------------------

          Proving_Log("Proving_RemoveFighters()");

          // Deactivate Opponents
          nArraySize = Proving_GetNumOpponents(nFightID);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oFighter = UT_GetNearestCreatureByTag(oEnterWP, Proving_GetOpponent(nFightID, nIndex));
               WR_SetObjectActive(oFighter, EngineConstants.FALSE);
          }

          // Deactivate Allies
          nArraySize = Proving_GetNumAllies(nFightID);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oFighter = UT_GetNearestCreatureByTag(oEnterWP, Proving_GetAlly(nFightID, nIndex));
               if (IsPartyMember(oFighter) == EngineConstants.FALSE)
                    WR_SetObjectActive(oFighter, EngineConstants.FALSE);
          }

     }

     //------------------------------------------------------------------------------
     // Proving_SetFightersHostile
     //------------------------------------------------------------------------------
     /* @brief Sets all fighters hostile for the Current Fight ID
     * @param nFightID the Fight ID to use
     * @param bHostile EngineConstants.TRUE for hostile, EngineConstants.FALSE for non-hostile (optional)
     * @author joshua
     */
     public void Proving_SetFightersHostile(int nFightID, int bHostile = EngineConstants.TRUE)
     {
          Proving_Log("Proving_SetFightersHostile()", (bHostile != EngineConstants.FALSE ? "EngineConstants.TRUE" : "EngineConstants.FALSE"));
          UT_TeamGoesHostile(EngineConstants.PROVING_TEAM_OPPONENTS, bHostile);
     }

     //------------------------------------------------------------------------------
     // Proving_RunEvents
     //------------------------------------------------------------------------------
     /* @brief Handles events for each stage of the fight
     * @param nFightID the Current Fight ID
     * @param nEventType which proving xEvent to fire (EngineConstants.EVENT_TYPE_PROVING_[ENTER|START|WIN|LOSE|EXIT])
     * @author joshua
     */
     public void Proving_HandleEvents(int nFightID, int nEventType)
     {

          if (Proving_IsValidEvent(nEventType) == EngineConstants.FALSE)
          {
               Proving_Log("Proving_HandleEvents()", "Invalid Event Passed: " + ToString(nEventType));
               return;
          }

          string rScript; // Script we will send our events to
          string sPlot;   // Plot we want to set a flag in
          int nFlag;   // Flag ID we want to set

          string sPlotToUse = String.Empty;
          string sFlagToUse = String.Empty;

          switch (nEventType)
          {
               case EngineConstants.EVENT_TYPE_PROVING_ENTER:
                    {
                         sPlotToUse = EngineConstants.PROVING_SET_PLOT_ENTER;
                         sFlagToUse = EngineConstants.PROVING_SET_FLAG_ENTER;
                         break;
                    }
               case EngineConstants.EVENT_TYPE_PROVING_START:
                    {
                         sPlotToUse = EngineConstants.PROVING_SET_PLOT_START;
                         sFlagToUse = EngineConstants.PROVING_SET_FLAG_START;
                         break;
                    }
               case EngineConstants.EVENT_TYPE_PROVING_WIN:
                    {
                         sPlotToUse = EngineConstants.PROVING_SET_PLOT_WIN;
                         sFlagToUse = EngineConstants.PROVING_SET_FLAG_WIN;
                         break;
                    }
               case EngineConstants.EVENT_TYPE_PROVING_LOSE:
                    {
                         sPlotToUse = EngineConstants.PROVING_SET_PLOT_LOSE;
                         sFlagToUse = EngineConstants.PROVING_SET_FLAG_LOSE;
                         break;
                    }
               case EngineConstants.EVENT_TYPE_PROVING_EXIT:
                    {
                         sPlotToUse = EngineConstants.PROVING_SET_PLOT_EXIT;
                         sFlagToUse = EngineConstants.PROVING_SET_FLAG_EXIT;
                         break;
                    }
          }

          sPlot = GetM2DAString(EngineConstants.TABLE_PROVING_FIGHTS, sPlotToUse, nFightID);
          nFlag = GetM2DAInt(EngineConstants.TABLE_PROVING_FIGHTS, sFlagToUse, nFightID);
          rScript = GetM2DAResource(EngineConstants.TABLE_PROVING_FIGHTS, EngineConstants.PROVING_SCRIPT, nFightID);

          if (ResourceToString(rScript) == EngineConstants.PROVING_NULL)
               rScript = EngineConstants.RESOURCE_SCRIPT_PROVING_CORE;

          // Did we have to set a plot flag
          if (sPlot != EngineConstants.PROVING_NULL && nFlag > -1)
               WR_SetPlotFlag(sPlot, nFlag, EngineConstants.TRUE, EngineConstants.TRUE);

          // Pass events off onto the xEvent script
          xEvent evProvingEvent = Event(nEventType);
          HandleEventRef(ref evProvingEvent, rScript);

     }

     //------------------------------------------------------------------------------
     // Proving_GetOpponent
     //------------------------------------------------------------------------------
     /* @brief Get a certain opponent fighter based on his fighter number and the Fight ID
     * @param nFightID the Fight ID to use
     * @param nFighterID the Fighter number to use (this MUST be valid)
     * @returns tag string for the fighter
     * @author joshua
     */
     public string Proving_GetOpponent(int nFightID, int nFighterID)
     {

          string sFighterColumn = EngineConstants.PROVING_OPPONENT_PREFIX + (nFighterID < 10 ? "0" : "") + ToString(nFighterID);
          Proving_Log("Proving_GetOpponent()", sFighterColumn + ": " + GetM2DAString(EngineConstants.TABLE_PROVING_FIGHTS, sFighterColumn, nFightID));
          return GetM2DAString(EngineConstants.TABLE_PROVING_FIGHTS, sFighterColumn, nFightID);

     }

     //------------------------------------------------------------------------------
     // Proving_GetAlly
     //------------------------------------------------------------------------------
     /* @brief Get a certain ally fighter based on his fighter number and the Fight ID
     * @param nFightID the Fight ID to use
     * @param nFighterID the Fighter number to use (this MUST be valid)
     * @returns tag string for the ally fighter
     * @author joshua
     */
     public string Proving_GetAlly(int nFightID, int nFighterID)
     {
          string sFighterColumn = EngineConstants.PROVING_ALLY_PREFIX + (nFighterID < 10 ? "0" : "") + ToString(nFighterID);
          Proving_Log("Proving_GetAlly()", sFighterColumn + ": " + GetM2DAString(EngineConstants.TABLE_PROVING_FIGHTS, sFighterColumn, nFightID));
          return GetM2DAString(EngineConstants.TABLE_PROVING_FIGHTS, sFighterColumn, nFightID);
     }

     //------------------------------------------------------------------------------
     // Proving_Log
     //------------------------------------------------------------------------------
     /* @brief Outputs errors/messages to Systems Log Channel
     * @param sFunction the function the message occurs in
     * @param sError the error message
     * @author joshua
     */
     public void Proving_Log(string sFunction, string sError = "")
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS,
                     GetCurrentScriptName() + "." + sFunction,
                     sError
        );
     }

     //------------------------------------------------------------------------------
     // Proving_GetCurrentFightId
     //------------------------------------------------------------------------------
     /* @brief Grabs the Current Fight ID
* @returns Current Fight ID
* @author joshua
*/
     public int Proving_GetCurrentFightId()
     {
          return GetLocalInt(GetModule(), EngineConstants.PROVING_FIGHT_ID);
     }

     //------------------------------------------------------------------------------
     // Proving_GetDialogCreature
     //------------------------------------------------------------------------------
     /* @brief Grabs the Dialog Creature based on the Fight ID and xEvent type
     * @param nFightID the Fight ID to use
     * @author joshua
     */
     public GameObject Proving_GetDialogCreature(int nFightID, int nEventType)
     {

          if (Proving_IsValidEvent(nEventType) == EngineConstants.FALSE)
          {
               Proving_Log("Proving_HandleEvents()", "Invalid Event Passed: " + ToString(nEventType));
               return null;
          }

          GameObject oPC = GetHero();
          string sDialogType = String.Empty;
          string sCreature;
          GameObject oCreature = null;

          switch (nEventType)
          {
               case EngineConstants.EVENT_TYPE_PROVING_ENTER: sDialogType = EngineConstants.PROVING_CONVERSATION_ENTER; break;
               case EngineConstants.EVENT_TYPE_PROVING_START: sDialogType = EngineConstants.PROVING_CONVERSATION_START; break;
               case EngineConstants.EVENT_TYPE_PROVING_WIN: sDialogType = EngineConstants.PROVING_CONVERSATION_WIN; break;
               case EngineConstants.EVENT_TYPE_PROVING_LOSE: sDialogType = EngineConstants.PROVING_CONVERSATION_LOSE; break;
               case EngineConstants.EVENT_TYPE_PROVING_EXIT: sDialogType = EngineConstants.PROVING_CONVERSATION_EXIT; break;
          }

          sCreature = GetM2DAString(EngineConstants.TABLE_PROVING_FIGHTS, sDialogType, nFightID);
          if (sCreature != "")
          {
               oCreature = UT_GetNearestCreatureByTag(oPC, sCreature);
          }

          if (IsObjectValid(oCreature) == EngineConstants.FALSE)
          {
               Proving_Log("Proving_GetDialogCreature()", "No Dialog Creature Found (" + sCreature + "] for " + sDialogType);
               return null;
          }

          return oCreature;

     }

     //------------------------------------------------------------------------------
     // Proving_GetNumOpponents
     //------------------------------------------------------------------------------
     /* @brief Get number of PC opponents for a given Fight ID
     * @param nFightID the Fight ID to use
     * @returns number of opponents
     * @author joshua
     */
     public int Proving_GetNumOpponents(int nFightID)
     {

          int nIndex;
          int nArraySize;

          //--------------------------------------------------------------------------

          nArraySize = 0;
          for (nIndex = 0; nIndex < EngineConstants.PROVING_MAX_OPPONENTS; nIndex++)
          {
               if (Proving_GetOpponent(nFightID, nIndex) != EngineConstants.PROVING_NULL)
                    nArraySize++;
               else
                    break;
          }

          Proving_Log("Proving_GetNumOpponents()", "Opponents: " + ToString(nArraySize));

          return nArraySize;

     }

     //------------------------------------------------------------------------------
     // Proving_GetNumAllies
     //------------------------------------------------------------------------------
     /* @brief Get number of PC allies for a given Fight ID
     * @param nFightID the Fight ID to use
     * @returns number of allies
     * @author joshua
     */
     public int Proving_GetNumAllies(int nFightID)
     {

          int nIndex;
          int nArraySize;

          //--------------------------------------------------------------------------

          nArraySize = 0;
          for (nIndex = 0; nIndex < EngineConstants.PROVING_MAX_ALLIES; nIndex++)
          {
               if (Proving_GetAlly(nFightID, nIndex) != EngineConstants.PROVING_NULL)
                    nArraySize++;
               else
                    break;
          }

          Proving_Log("Proving_GetNumAllies()", "Allies: " + ToString(nArraySize));

          return nArraySize;

     }

     //------------------------------------------------------------------------------
     // Proving_SpawnSpriteCrowd
     //------------------------------------------------------------------------------
     /* @brief Adds all proving crowd sprites at proper locations (using the
     * locations of the placebles EngineConstants.PROVING_IP_PROVING_CROWD) It removed all existing
     * VFX with the given ID from the area before spawning the new ones.
     * @param nVFXOverride Override sprite to use for crowd
     * @author joshua
     */
     public void Proving_SpawnSpriteCrowd(int nVFXOverride = 10011)
     {

          int nIndex;
          int nArraySize;
          GameObject oPC;
          List<GameObject> arPLC;

          //--------------------------------------------------------------------------

          oPC = GetHero();
          arPLC = GetNearestObjectByTag(oPC, EngineConstants.PROVING_IP_PROVING_CROWD, EngineConstants.OBJECT_TYPE_PLACEABLE, EngineConstants.MAX_GETNEAREST_OBJECTS);

          //--------------------------------------------------------------------------
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "I'm in the Generate Crowd Function");
          //(((("+ToString(vPos.x)+","+ToString(vPos.y)+","+ToString(vPos.z)+
          // Remove Old VFX
          Proving_RemoveSpriteCrowd(nVFXOverride);

          // Apply New VFX
          nArraySize = GetArraySize(arPLC);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT,
                   EffectVisualEffect(nVFXOverride), GetLocation(arPLC[nIndex]));
          }

     }

     //------------------------------------------------------------------------------
     // Proving_RemoveSpriteCrowd
     //------------------------------------------------------------------------------
     /* @brief Removes all sprites of type nVFXOverride in the area
     * @param nVFXOverride Override sprite to use for crowd
     * @author joshua
     */
     public void Proving_RemoveSpriteCrowd(int nVFXOverride = 10011)
     {

          int nIndex;
          int nArraySize;
          GameObject oArea;
          List<xEffect> arAreaEffects;

          //--------------------------------------------------------------------------

          oArea = GetArea(GetHero());
          arAreaEffects = GetEffects(oArea);

          //--------------------------------------------------------------------------

          nArraySize = GetArraySize(arAreaEffects);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               if (GetVisualEffectID(arAreaEffects[nIndex]) == nVFXOverride)
                    RemoveEffect(oArea, arAreaEffects[nIndex]);
          }

     }

     //------------------------------------------------------------------------------
     // Proving_IsValidEvent
     //------------------------------------------------------------------------------
     /* @brief Checks if the input xEvent is valid for a proving
     * @param nEventType The xEvent that we want to check
     * @returns EngineConstants.TRUE if xEvent type is valid
     * @author joshua
     */
     public int Proving_IsValidEvent(int nEventType)
     {

          switch (nEventType)
          {
               case EngineConstants.EVENT_TYPE_PROVING_ENTER: return EngineConstants.TRUE;
               case EngineConstants.EVENT_TYPE_PROVING_START: return EngineConstants.TRUE;
               case EngineConstants.EVENT_TYPE_PROVING_WIN: return EngineConstants.TRUE;
               case EngineConstants.EVENT_TYPE_PROVING_LOSE: return EngineConstants.TRUE;
               case EngineConstants.EVENT_TYPE_PROVING_EXIT: return EngineConstants.TRUE;
          }

          return EngineConstants.FALSE;

     }

     //Unused
     /* @brief Sets all fighters non-hostile for the Current Fight ID
* @param nFightID the Fight ID to use
* @author joshua
*/
     public void Proving_ResetFighters(int nFightID) { }
}