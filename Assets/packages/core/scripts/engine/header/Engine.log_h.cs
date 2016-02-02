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
using System.Linq;

public partial class Engine
{
     //------------------------------------------------------------------------------
     // log_h - Design Log and Debug Framework
     //------------------------------------------------------------------------------
     /*
         Provide designers with standarized functions to add log and debug messages
         to the DejaInsight System.

         Georg:
         By default, the log functions will make use of LogTrace, which I wrote to
         directly access DEJA_TRACE. If deja is not running or the game executable
         has not been compiled with Deja support, the game will use ECLog instead.

     */
     //------------------------------------------------------------------------------
     // @author yaron, georg
     //------------------------------------------------------------------------------

     //#include"var_constants_h"
     //#include"2da_constants_h"
     //#include"design_tracking_h"

     // -----------------------------------------------------------------------------
     // Georg: This is the compiler side killswitch for DEJA/Eclipse Log output
     //        after Sunday, Feb 22, 2009, this defaults to commented out.
     //
     //        If you need log/deja output from your scripts to debug, you need to
     //        remove the 'NO' from the symbol. You should do so only ever in a local,
     //        temporary copy of this file and never while the file is checked out.
     //
     //        If you have questions regarding this mechanism, please talk to me.
     //
     //        Note: SHIP mode executables do not support the LogTrace xCommand and as
     //              such will never provide deja/log output, even if this symbol
     //              is defined.
     //
     // -----------------------------------------------------------------------------

     // -----------------------------------------------------------------------------

     // Old Design side log killswitch, just leave it alone.
     //moved public const int EngineConstants.LOG_ENABLED = EngineConstants.TRUE;

     // -----------------------------------------------------------------------------
     // Georg: This is the compiler side killswitch for my Telemetry and Stats Awareness
     //        system at http://georg/SkyNetWeb/default.aspx.
     //
     //        When turned off, script side telemetry and xEvent tracking will become
     //        non operational - you will no longer be able to file SmartBugs,
     //        and session tracking, analyis and developer achievements will not be
     //        available.
     //
     //        Note: SHIP mode executables do not have the Eclipse NetLayer enabled
     //              preventing communication with the SkyNet server. Defining this
     //              Symbol in SHIP mode will not do anyting.
     //
     // -----------------------------------------------------------------------------
     //#defsym SKYNET
     // -----------------------------------------------------------------------------

     // severity / priorit settings
     //moved public const int EngineConstants.LOG_SEVERITY_MESSAGE   = 0;
     //moved public const int EngineConstants.LOG_SEVERITY_WARNING   = 2;
     //moved public const int EngineConstants.LOG_SEVERITY_CRITICAL  = 3;

     // --- start deprecated section, left in until we can remove all legacy calls
     //moved public const int EngineConstants.LOG_LEVEL_ERROR = EngineConstants.LOG_SEVERITY_CRITICAL;
     //moved public const int EngineConstants.LOG_LEVEL_DEBUG = EngineConstants.LOG_SEVERITY_MESSAGE;
     //moved public const int EngineConstants.LOG_LEVEL_WARNING = EngineConstants.LOG_SEVERITY_WARNING ;
     //moved public const int EngineConstants.LOG_LEVEL_CRITICAL = EngineConstants.LOG_SEVERITY_CRITICAL;
     //moved public const int EngineConstants.LOG_RULES_SUBTYPE_ATTACK = 1;
     //moved public const int EngineConstants.LOG_RULES_SUBTYPE_DAMAGE = 2;
     //moved public const int EngineConstants.LOG_SYSTEMS_SUBTYPE_GENERAL =3 ;
     //moved public const int EngineConstants.LOG_SYSTEMS_SUBTYPE_TALK_TRIGGER = 4;
     //moved public const int EngineConstants.LOG_SYSTEMS_SUBTYPE_APPROVAL = 5;
     //moved public const int EngineConstants.LOG_RULES_SUBTYPE_DEATH = 6;
     // --- end deprecated section

     // -----------------------------------------------------------------------------
     // Helper Functions
     // -----------------------------------------------------------------------------

     // -----------------------------------------------------------------------------
     // Get the 2da table for an ability
     // -----------------------------------------------------------------------------
     public int _GetAbilityTable(int nAbilityType)
     {
          // Which 2DA to read the data from
          int n2DA = EngineConstants.TABLE_ABILITIES_TALENTS;

          if (nAbilityType == EngineConstants.ABILITY_TYPE_SPELL)
          {
               n2DA = EngineConstants.TABLE_ABILITIES_SPELLS;
          }

          return n2DA;
     }

     // -----------------------------------------------------------------------------
     // Get a human readable string for a command
     // -----------------------------------------------------------------------------
     /* @brief Return the name / label of a xCommand by ID.
     *
     *
     * @param nCommandId - The Command Id to look up.
     * @returns The contents of the label column in commands.xls
     * @author Georg Zoeller
     */
     public string Log_GetCommandNameById(int nCommandId)
     {
          string sRet = "";

#if DEBUG
          sRet = GetM2DAString(EngineConstants.TABLE_COMMANDS, "Label", nCommandId) + "(" + IntToString(nCommandId) + ") ";
#endif

          return sRet;

     }

     // -----------------------------------------------------------------------------
     // Get a human readable string for an event
     // -----------------------------------------------------------------------------
     /* @brief Return the name / label of an xEvent by ID.
          *
          *
          * @param nEventId - The xEvent Id to look up.
          * @returns The contents of the label column in events.xls
          * @author Georg Zoeller
          */
     public string Log_GetEventNameById(int nEventId)
     {

          string sRet = "";

#if DEBUG
          sRet = GetM2DAString(EngineConstants.TABLE_EVENTS, "Label", nEventId) + " (" + IntToString(nEventId) + ") ";
#endif

          return sRet;

     }

     // -----------------------------------------------------------------------------
     // Get a human readable string for an Effect
     // -----------------------------------------------------------------------------
     /* @brief Return the name / label of a xEffect by ID.
          *
          *
          * @param nEventId - The xEffect Id to look up.
          * @returns The contents of the label column in events.xls
          * @author Georg Zoeller
          */
     public string Log_GetEffectNameById(int nEffectId)
     {

          string sRet = "";

#if DEBUG
          sRet = GetM2DAString(EngineConstants.TABLE_EFFECTS, "Label", nEffectId) + " (" + IntToString(nEffectId) + ") ";
#endif

          return sRet;
     }

     // -----------------------------------------------------------------------------
     // Get the type of an ability. This is mirrored from core_h
     // -----------------------------------------------------------------------------
     public int _GetAbilityType(int nAbility)
     {
          // mirrored from core_h to avoid circular includes
          if (nAbility >= EngineConstants.ABI_BASE_ITEM_RANGE_START)
          {
               return EngineConstants.ABILITY_TYPE_ITEM;
          }
          else if (nAbility >= EngineConstants.ABI_BASE_SKILL_RANGE_START)
          {
               return EngineConstants.ABILITY_TYPE_SKILL;
          }
          else if (nAbility >= EngineConstants.ABI_BASE_SPELL_RANGE_START)
          {
               return EngineConstants.ABILITY_TYPE_SPELL;
          }
          else
          {
               return EngineConstants.ABILITY_TYPE_TALENT;
          }
     }

     // -----------------------------------------------------------------------------
     // Get a human readable string for an ability
     // -----------------------------------------------------------------------------
     public string Log_GetAbilityNameById(int nAbilityId)
     {
#if DEBUG
          int nType = _GetAbilityType(nAbilityId);
          int nTable = _GetAbilityTable(nType);
          return (GetM2DAString(nTable, "LABEL", nAbilityId) + " (" + IntToString(nAbilityId) + ")");
#endif

          return "";

     }

     // -----------------------------------------------------------------------------
     // Get a human readable string for an ability
     // -----------------------------------------------------------------------------
     public string Log_GetAttackResultNameById(int nAttackResult)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED == EngineConstants.FALSE)
          {
               return "";
          }

          if (nAttackResult == EngineConstants.COMBAT_RESULT_HIT)
          {
               return IntToString(nAttackResult) + " = EngineConstants.COMMAND_RESULT_HIT";
          }
          else if (nAttackResult == EngineConstants.COMBAT_RESULT_CRITICALHIT)
          {
               return IntToString(nAttackResult) + " = EngineConstants.COMMAND_RESULT_CRITICALHIT";
          }
          else if (nAttackResult == EngineConstants.COMBAT_RESULT_DEATHBLOW)
          {
               return IntToString(nAttackResult) + " = EngineConstants.COMMAND_RESULT_DEATHBLOW";
          }
          else if (nAttackResult == EngineConstants.COMBAT_RESULT_MISS)
          {
               return IntToString(nAttackResult) + " = EngineConstants.COMMAND_RESULT_MISS";
          }
          else if (nAttackResult == EngineConstants.COMBAT_RESULT_BACKSTAB)
          {
               return IntToString(nAttackResult) + " = EngineConstants.COMMAND_RESULT_BACKSTAB";
          }
          else
          {
               return IntToString(nAttackResult) + " = EngineConstants.COMMAND_RESULT_INVALID";
          }
#endif

          return "";

     }

     // -----------------------------------------------------------------------------
     // Get a human readable string for an ability
     // -----------------------------------------------------------------------------
     public string Log_GetCommandStatusById(int nCommandStatus)
     {
          string sStatus = "";

#if DEBUG
          switch (nCommandStatus)
          {
               case EngineConstants.COMMAND_SUCCESSFUL: sStatus = "EngineConstants.COMMAND_SUCCESSFUL"; break;
               case EngineConstants.COMMAND_LOOPING: sStatus = "EngineConstants.COMMAND_LOOPING"; break;
               case EngineConstants.COMMAND_FAILED: sStatus = "EngineConstants.COMMAND_FAILED"; break;
               case EngineConstants.COMMAND_FAILED_COMMAND_CLEARED: sStatus = "EngineConstants.COMMAND_FAILED_COMMAND_CLEARED"; break;
               case EngineConstants.COMMAND_FAILED_INVALID_DATA: sStatus = "EngineConstants.COMMAND_FAILED_INVALID_DATA"; break;
               case EngineConstants.COMMAND_FAILED_INVALID_PATH: sStatus = "EngineConstants.COMMAND_FAILED_INVALID_PATH"; break;
               case EngineConstants.COMMAND_FAILED_NO_SPACE_IN_MELEE_RING: sStatus = "EngineConstants.COMMAND_FAILED_NO_SPACE_IN_MELEE_RING"; break;
               case EngineConstants.COMMAND_FAILED_NO_LINE_OF_SIGHT: sStatus = "EngineConstants.COMMAND_FAILED_NO_LINE_OF_SIGHT"; break;
               case EngineConstants.COMMAND_FAILED_TARGET_DESTROYED: sStatus = "EngineConstants.COMMAND_FAILED_TARGET_DESTROYED "; break;
               case EngineConstants.COMMAND_FAILED_DISABLED: sStatus = "EngineConstants.COMMAND_FAILED_DISABLED"; break;
               case EngineConstants.COMMAND_FAILED_PATH_ACTION_REQUIRED: sStatus = "EngineConstants.COMMAND_FAILED_PATH_ACTION_REQUIRED"; break;
               default: sStatus = "*** Unknown xCommand status ***"; break;
          }
#endif
          return sStatus;
     }

     // -----------------------------------------------------------------------------
     // Get a human readable string for an ability type
     // -----------------------------------------------------------------------------
     public string Log_GetAbilityTypeNameById(int nAbilityType)
     {
          string sRet = "";

#if DEBUG
          switch (nAbilityType)
          {
               case EngineConstants.ABILITY_TYPE_ITEM: sRet = "EngineConstants.ABILITY_TYPE_ITEM"; break;
               case EngineConstants.ABILITY_TYPE_SKILL: sRet = "EngineConstants.ABILITY_TYPE_SKILL"; break;
               case EngineConstants.ABILITY_TYPE_SPELL: sRet = "EngineConstants.ABILITY_TYPE_SPELL"; break;
               case EngineConstants.ABILITY_TYPE_TALENT: sRet = "EngineConstants.ABILITY_TYPE_TALENT"; break;
               default: sRet = "*** Unknown/Invalid ability type***"; break;
          }
#endif
          return sRet;
     }

     // -----------------------------------------------------------------------------
     // Returns a human readable string for a placeable action
     // -----------------------------------------------------------------------------
     public string Log_GetPlaceableActionNameByID(int nAction)
     {
          string sAction = "";

#if DEBUG
          switch (nAction)
          {
               case EngineConstants.PLACEABLE_ACTION_USE: sAction = "PLACEABLE_ACTION_USE"; break;
               case EngineConstants.PLACEABLE_ACTION_OPEN: sAction = "PLACEABLE_ACTION_OPEN"; break;
               case EngineConstants.PLACEABLE_ACTION_CLOSE: sAction = "PLACEABLE_ACTION_CLOSE"; break;
               case EngineConstants.PLACEABLE_ACTION_AREA_TRANSITION: sAction = "PLACEABLE_ACTION_AREA_TRANSITION"; break;
               case EngineConstants.PLACEABLE_ACTION_CONVERSATION: sAction = "PLACEABLE_ACTION_CONVERSATION"; break;
               case EngineConstants.PLACEABLE_ACTION_EXAMINE: sAction = "PLACEABLE_ACTION_EXAMINE"; break;
               case EngineConstants.PLACEABLE_ACTION_TRIGGER_TRAP: sAction = "PLACEABLE_ACTION_TRIGGER_TRAP"; break;
               case EngineConstants.PLACEABLE_ACTION_DISARM: sAction = "PLACEABLE_ACTION_DISARM"; break;
               case EngineConstants.PLACEABLE_ACTION_UNLOCK: sAction = "PLACEABLE_ACTION_UNLOCK"; break;
               case EngineConstants.PLACEABLE_ACTION_OPEN_INVENTORY: sAction = "PLACEABLE_ACTION_OPEN_INVENTORY"; break;
               case EngineConstants.PLACEABLE_ACTION_FLIP_COVER: sAction = "PLACEABLE_ACTION_FLIP_COVER"; break;
               case EngineConstants.PLACEABLE_ACTION_USE_COVER: sAction = "PLACEABLE_ACTION_USE_COVER"; break;
               case EngineConstants.PLACEABLE_ACTION_LEAVE_COVER: sAction = "PLACEABLE_ACTION_LEAVE_COVER"; break;
               case EngineConstants.PLACEABLE_ACTION_TOPPLE: sAction = "PLACEABLE_ACTION_TOPPLE"; break;
               case EngineConstants.PLACEABLE_ACTION_DESTROY: sAction = "PLACEABLE_ACTION_DESTROY"; break;
               default: sAction = "*** Unknown action ***"; break;
          }
#endif
          return sAction;
     }

     /*
     *  @brief Write a log message with origin information to a channel
     *
     *  Standard log handler for designer use. Please provide the file and function you
     *  are calling the function from in sOrigin.
     *  You can use oTarget to associate the
     *  function call with a specific GameObject (such as the target of an attack). Do not
     *  pass gameObject as that information is already contained in the log scope anyway.
     *
     *  @example: Log_Trace(EngineConstants.LOG_CHANNEL_GENERAL,"myscript.myfunction", "hello world", oObject).
     *
     *  @param nLogChannel A EngineConstants.LOG_CHANNEL_* constant defining the deja channel to write to.
     *                     If you need additional channels, please talk to georg
     *  @param sOrigin   The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param sMessage  Your log message
     *  @param nSeverity The EngineConstants.LOG_SEVERITY_* severity of the mesage.
     *
     *  @sa Log_Msg.
     *
     *  @author georg
     **/
     public void Log_Trace(int nLogChannel, string sOrigin = "", string sMessage = "", GameObject oTarget = null, int nPriority = EngineConstants.LOG_SEVERITY_MESSAGE)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               LogTrace(nLogChannel, "[" + sOrigin + "] " + sMessage, oTarget);
          }
#endif
     }

     /*
     *  @brief Write a log message with origin information to a channel
     *
     *  Standard log handler for designer use. Please use Log_Trace instead when possible
     *
     *  You can use oTarget to associate the function call with a specific object
     *  (such as the target of an attack). Do not pass gameObject as that information
     *  is already contained in the log scope anyway.
     *
     *  @example: Log_Trace(EngineConstants.LOG_CHANNEL_GENERAL,"myscript.myfunction", "hello world", oObject).
     *
     *  @param nLogChannel A EngineConstants.LOG_CHANNEL_* constant defining the deja channel to write to.
     *                     If you need additional channels, please talk to georg
     *  @param sMessage  Your log message
     *  @param nSeverity The EngineConstants.LOG_SEVERITY_* severity of the mesage.
     *
     *  @sa Log_Trace
     *
     *  @author georg
     **/
     public void Log_Msg(int nLogChannel, string sMessage = "", GameObject oTarget = null, int nPriority = EngineConstants.LOG_SEVERITY_MESSAGE)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {

               LogTrace(nLogChannel, sMessage, oTarget);
          }
#endif
     }

     // -----------------------------------------------------------------------------
     // The functions below do not have an interface declaration on purpose
     // feel free to ask georg why...
     // -----------------------------------------------------------------------------

     /*
     *  @brief Specialized version of Log_Trace for xCommand queue debugging
     *

     *  @param sOrigin   The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param cmd       The Command to debug
     *  @param oTarget   The xCommand target
     *
     *  @author georg
     **/
     public void Log_Trace_Commands(string sOrigin, xCommand cmd, GameObject oTarget)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               int nCommandType = GetCommandType(cmd);
               Log_Trace(EngineConstants.LOG_CHANNEL_COMMANDS, sOrigin, Log_GetCommandNameById(nCommandType), oTarget);
          }
#endif
     }

     /*
     *  @brief Specialized version of Log_Trace to message scripting errors to designers
     *
     *  This writes to EngineConstants.LOG_CHANNEL_DESIGN_SCRIPTERROR
     *
     *  @param sOrigin   The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param sMessage  The message you want to put into the log
     *  @param oTarget   Optional target object
     *
     *  @author georg
     **/

     public void Log_Trace_Scripting_Error(string sOrigin, string sMessage, GameObject oTarget = null)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_SCRIPTERROR, sOrigin, sMessage, oTarget);
          }
#endif
     }

     /*
     *  @brief Specialized version of Log_Trace to debug effects
     *
     *  This writes to EngineConstants.LOG_CHANNEL_EFFECTS
     *
     *  @param sOrigin    The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param eEffect    The Effect to debug
     *  @param sMessage   Optional message
     *  @param oTarget    Optional target information
     *
     *  @author georg
     **/
     public void Log_Trace_Effects(string sOrigin, xEffect eEffect, string sMessage, GameObject oTarget = null, int nDurationType = -1, int nAbilityId = -1)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               int nEffectId = GetEffectTypeRef(ref eEffect);
               string sCreator = GetTag(GetEffectCreatorRef(ref eEffect));
            
               if (nDurationType == -1)
               {
                    nDurationType = GetEffectDurationTypeRef(ref eEffect);
               }

               if (nAbilityId == -1)
               {
                    nAbilityId = GetEffectAbilityIDRef(ref eEffect);
               }

               string sInfo = Log_GetEffectNameById(nEffectId) + ", duration: " + IntToString(nDurationType) + ", ability: " + IntToString(nAbilityId) + ", creator: " + sCreator;

               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, sOrigin, sMessage + " " + sInfo, oTarget);

          }
#endif
     }

     /*
     *  @brief Specialized version of Log_Trace to debug combat
     *
     *  This writes to EngineConstants.LOG_CHANNEL_COMBAT, unless changed.
     *
     *  @param sOrigin    The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param sMessage   The log message
     *  @param oAttacker  Attacking Object
     *  @param oTarget    Target / Defender / Victim object
     *  @param oTarget    Target / Defender / Victim object
     *  @param nLogChannel The log channel to write to (default EngineConstants.LOG_CHANNEL_COMBAT)
     *  @param nSeverity   The log severity
     *
     *  @author georg
     **/
     public void Log_Trace_Combat(string sOrigin, string sMessage = "", GameObject oAttacker = null, GameObject oTarget = null, int nLogChannel = EngineConstants.LOG_CHANNEL_COMBAT, int nSeverity = EngineConstants.LOG_SEVERITY_MESSAGE)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               LogTrace(nLogChannel, sOrigin + " " + sMessage + (IsObjectValid(oAttacker) != EngineConstants.FALSE ? ". Attacker: " + GetTag(oAttacker) : ""), oTarget);
          }
#endif
     }

     /*
     *  @brief Specialized version of Log_Trace to debug combat
     *
     *  This writes to EngineConstants.LOG_CHANNEL_AI
     *
     *  @param sOrigin    The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param sMessage   The log message
     *  @param nSeverity   The log severity
     *
     *  @author Yaron
     **/
     public void Log_Trace_AI(string sOrigin, string sMessage = "", int nSeverity = EngineConstants.LOG_SEVERITY_MESSAGE)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               string sTag = GetTag(gameObject) + "::";
               Log_Trace(EngineConstants.LOG_CHANNEL_AI, sTag + sOrigin, sMessage, null, nSeverity);
          }
#endif
     }

     /*
     *  @brief Specialized version of Log_Trace to debug combat
     *
     *  This writes to EngineConstants.LOG_CHANNEL_THREAT
     *
     *  @param sOrigin    The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param sMessage   The log message
     *  @param nSeverity   The log severity
     *
     *  @author Yaron
     **/
     public void Log_Trace_Threat(string sOrigin, string sMessage = "", int nSeverity = EngineConstants.LOG_SEVERITY_MESSAGE)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               string sTag = GetTag(gameObject) + "::";
               Log_Trace(EngineConstants.LOG_CHANNEL_THREAT, sTag + sOrigin, sMessage, null, nSeverity);
          }
#endif
     }

     /*
     *  @brief Specialized version of Log_Trace to debug plot changes
     *
     *  This writes to EngineConstants.LOG_CHANNEL_PLOT
     *
     *  @param sOrigin    The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param sPlot      The plot resref
     *  @param nPlotFlag  The flag that is being queried or touched
     *  @param nCurrent   The current value of that flag
     *  @param nNewValue  The new value of the flag (leave default there is no new value)
     *
     *  @author georg
     **/

     public void Log_Trace_Plot(string sOrigin, string sPlot, int nPlotFlag, int nCurrent, int nNewValue = -9999)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, sOrigin,
                         sPlot + " - Flag: " + IntToString(nPlotFlag) +
                                 " Value: " + IntToString(nCurrent) +
                                   ((nNewValue != -9999) ? " New:" + IntToString(nNewValue) : ""));
          }
#endif
     }

     /*
     *  @brief Specialized version of Log_Trace to debug spellscripts
     *
     *  This writes to EngineConstants.LOG_CHANNEL_COMBAT_ABILITY
     *
     *  @param sOrigin    The file and function you are calling from (e.g. "core_h.GetPlayerName")
     *  @param sMessage   LogMessage
     *  @param nAbilityId SpellID
     *  @param oTarget    Spell Target
     *  @param nSeverity  Message Severity
     *
     *  @author georg
     **/

     public void Log_Trace_Spell(string sOrigin, string sMessage, int nAbilityId, GameObject oTarget = null, int nSeverity = EngineConstants.LOG_SEVERITY_MESSAGE)
     {

#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               sOrigin = GetCurrentScriptName() + "." + sOrigin;
               string sSpell = Log_GetAbilityNameById(nAbilityId);
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, sOrigin, sSpell + ": " + sMessage, oTarget, nSeverity);
          }
#endif
     }

     /*
     *  @brief Specialized version of Log_Trace to debug Events firing
     *
     *  This writes to EngineConstants.LOG_CHANNEL_PLOT
     *
     *  @param sOrigin    The name of the calling function (e.g. "core_h.GetPlayerName")
     *  @param ev         The xEvent being debugged.
     *
     *  @author georg
     **/
     public void Log_Events(string sOrigin, xEvent ev, string sMessage = "", int nLogChannel = EngineConstants.LOG_CHANNEL_EVENTS)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED == EngineConstants.FALSE)
               return;

          int nEvent = GetEventTypeRef(ref ev);
          string sDetails = String.Empty;
          GameObject oCreator = GetEventCreatorRef(ref ev);
          GameObject oTarget = null;

          switch (nEvent)
          {
               case EngineConstants.EVENT_TYPE_SPAWN:
               {
                         if (GetObjectType(oCreator) == EngineConstants.OBJECT_TYPE_CREATURE)
                         {
                              sDetails = "Team: " + ToString(GetTeamId(oCreator));
                         }
                         break;
                    }

               case EngineConstants.EVENT_TYPE_CONVERSATION:
                    {
                         string sConv = ResourceToString(GetEventResourceRef(ref ev, 0));
                         sDetails = "Listener: " + ToString(GetEventCreatorRef(ref ev))
                                     + ", Conversation: " + (sConv == "" ? "<default>" : sConv);
                         break;
                    }

               case EngineConstants.EVENT_TYPE_ON_ORDER_RECEIVED:
                    {
                         sDetails = "Target: " + ToString(GetEventTargetRef(ref ev));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_ATTACK_IMPACT:
                    {
                         List<GameObject> arTarget = new List<GameObject>();
                         int i;
                         /*for (i = 1; IsObjectValid(GetEventObjectRef(ref ev, i)); i++)
                             arTarget[i-1] = GetEventObjectRef(ref ev, i);*/
                         for (i = 1; i < ev.oList.Count; i++)
                         {
                              GameObject oObject = ev.oList.ElementAt(i);
                              if (IsObjectValid(oObject) != EngineConstants.FALSE)
                              {
                                   arTarget.Add(oObject);
                              }
                         }

                         sDetails = "Targets: " + ToString(GetArraySize(arTarget))
                    + ", CombatResult: " + ToString(GetEventIntegerRef(ref ev, 0))
                    + ", DamageEffect: " + ToString(GetEventIntegerRef(ref ev, 1));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_COMMAND_PENDING:
                    {
                         oTarget = GetEventObjectRef(ref ev, 1);
                         int nCommandId = GetEventIntegerRef(ref ev, 0);
                         int nSubType = GetEventIntegerRef(ref ev, 1); // ability type for CommandUseAbility, # of attacks for CommandAttack

                         sDetails = Log_GetCommandNameById(nCommandId);

                         if (nCommandId == EngineConstants.COMMAND_TYPE_USE_ABILITY)
                         {
                              sDetails += ", Subtype: " + Log_GetAbilityTypeNameById(nSubType)
                                          + "(" + ToString(nSubType) + "), "
                                          + Log_GetAbilityNameById(nSubType);
                         }
                         else if (nCommandId == EngineConstants.COMMAND_TYPE_ATTACK)
                         {
                              sDetails += ", #Attacks: " + IntToString(nSubType);
                         }
                         else
                         {
                              sDetails += ", Subtype: " + IntToString(nSubType);
                         }
                         break;
                    }

               case EngineConstants.EVENT_TYPE_COMMAND_COMPLETE:
                    {
                         int nStatus = GetEventIntegerRef(ref ev, 1);
                         int nSubType = GetEventIntegerRef(ref ev, 2);

                         sDetails = Log_GetCommandNameById(GetEventIntegerRef(ref ev, 0))
                                     + (nSubType != EngineConstants.FALSE ? (", Subtype: " + ToString(nSubType)) : "")
                                     + ", Status: " + Log_GetCommandStatusById(nStatus) + " (" + ToString(nStatus) + ")";
                         break;
                    }

               case EngineConstants.EVENT_TYPE_REACHED_WAYPOINT:
                    {
                         sDetails = "Reached Vector3 " + ToString(GetEventIntegerRef(ref ev, 0)) + " of " + ToString(GetEventIntegerRef(ref ev, 1));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_ABILITY_CAST_IMPACT:
               case EngineConstants.EVENT_TYPE_ABILITY_CAST_START:
                    {
                         oTarget = GetEventObjectRef(ref ev, 1);
                         int nAbility = GetEventIntegerRef(ref ev, 0);

                         sDetails = Log_GetAbilityNameById(nAbility);
                         break;
                    }

               case EngineConstants.EVENT_TYPE_EQUIP:
               case EngineConstants.EVENT_TYPE_UNEQUIP:
               case EngineConstants.EVENT_TYPE_INVENTORY_ADDED:
               case EngineConstants.EVENT_TYPE_INVENTORY_REMOVED:
                    {
                         sDetails = "Item: " + ToString(GetEventObjectRef(ref ev, 0));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_PLACEABLE_ONCLICK:
                    {
                         sDetails = "Target: " + ToString(GetEventTargetRef(ref ev));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_PERCEPTION_APPEAR:
               case EngineConstants.EVENT_TYPE_PERCEPTION_DISAPPEAR:
                    {
                         sDetails = "Object: " + ToString(GetEventObjectRef(ref ev, 0));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_DAMAGED:
                    {
                         sDetails = "Damager: " + ToString(GetEventCreatorRef(ref ev))
                                     + ", Damage: " + ToString(GetEventFloatRef(ref ev, 0))
                                     + ", Type: " + ToString(GetEventIntegerRef(ref ev, 0));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_DEATH:
                    {
                         sDetails = "Killer: " + ToString(GetEventCreatorRef(ref ev));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_ENTER:
                    {
                         if (GetObjectType(GetEventCreatorRef(ref ev)) == EngineConstants.OBJECT_TYPE_PLACEABLE && GetLocalInt(GetEventCreatorRef(ref ev), EngineConstants.PLC_TRAP_TYPE) > 0)
                              sDetails = "Trap: " + ToString(GetEventCreatorRef(ref ev)) + ", ";
                         sDetails += "Target: " + ToString(GetEventTargetRef(ref ev));
                         break;
                    }

               case 1050:  // EngineConstants.EVENT_TYPE_TRAP_ARM:
                    {
                         sDetails = "Owner: " + ToString(GetEventObjectRef(ref ev, 0));
                         break;
                    }

               case EngineConstants.EVENT_TYPE_TRAP_TRIGGERED:
                    {
                         sDetails = "Target: " + ToString(GetEventObjectRef(ref ev, 0));
                         break;
                    }

               case 1051:  // EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ENTER:
                    {
                         sDetails = "Target: " + ToString(GetEventTargetRef(ref ev))
                                 + ", Trap: " + ToString(GetEventCreatorRef(ref ev))
                                 + ", Trap Type: " + ToString(GetLocalInt(GetEventCreatorRef(ref ev), EngineConstants.PLC_TRAP_TYPE))
                                 + ", AOE tag: " + GetEventStringRef(ref ev, 0);
                         break;

                    }

               case EngineConstants.EVENT_TYPE_USE:
                    {
                         int nAction = GetPlaceableAction(gameObject);
                         sDetails = Log_GetPlaceableActionNameByID(nAction) + " (" + ToString(nAction) + ") ";

                         if (nAction == EngineConstants.PLACEABLE_ACTION_AREA_TRANSITION)
                         {
                              sDetails += "Area: " + GetLocalString(gameObject, EngineConstants.PLC_AT_DEST_AREA_TAG)
                                      + ", Waypoint: " + GetLocalString(gameObject, EngineConstants.PLC_AT_DEST_TAG);
                         }
                         else if (nAction == EngineConstants.PLACEABLE_ACTION_UNLOCK)
                         {
                              string sKeyTag = GetPlaceableKeyTag(gameObject);
                              sDetails += "LockLevel: " + ToString(GetPlaceablePickLockLevel(gameObject))
                                      + ", KeyTag: " + sKeyTag
                                      + ", KeyRequired: " + ToString(GetPlaceableKeyRequired(gameObject))
                                      + ", RemoveKey: " + ToString(GetPlaceableAutoRemoveKey(gameObject));
                              if (sKeyTag != "")
                                   sDetails += ", PlayerHasKey: " + ToString(IsObjectValid(GetItemPossessedBy(GetEventCreatorRef(ref ev), sKeyTag)));
                         }
                         break;
                    }

               case 1026: //EngineConstants.EVENT_TYPE_AMBIENT_CONTINUE:
                    {
                         GameObject oInstigator = GetEventObjectRef(ref ev, 0);
                         if (IsObjectValid(oInstigator) != EngineConstants.FALSE)
                              sDetails = "Instigator: " + ToString(oInstigator);
                         break;
                    }
          }

          string sFinalMessage = "[" + GetCurrentScriptName(ev.scriptname) + ((sOrigin == "") ? "" : ".") + sOrigin + "] "
                                  + Log_GetEventNameById(nEvent) + sDetails + " " + sMessage;

          LogTrace(nLogChannel, sFinalMessage, oTarget);
#endif
     }

     public void Log_Systems(string s = "", int n = 0, GameObject o = null, int n2 = 0, int n3 = 0, int n4 = 0)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED == 2)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_HACK, "log_h.Log_Systems", "deprecated function Log_Systems called, please fix! " + s, o);
          }
#endif

     }

     public void Log_Rules(string s = "", int n = 0, GameObject o = null, GameObject o2 = null, int n2 = 0, int n3 = 0, string ss = "")
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED == 2)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_HACK, "log_h.Log_Rules", "deprecated function Log_Rules called, please fix! " + s, o);
          }
#endif
     }

     public void Log_AI(string s = "", int n2 = 0, GameObject o = null, GameObject o2 = null)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED == 2)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_HACK, "log_h.Log_AI", "deprecated function Log_AI called, please fix! " + s);
          }
#endif
     }

     public void Log_Plot(string s = "", int n2 = 0, GameObject o = null, GameObject o2 = null)
     {
#if DEBUG
          if (EngineConstants.LOG_ENABLED == 2)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_HACK, "log_h.Log_AI", "deprecated function Log_AI called, please fix! " + s);
          }
#endif
     }

     public void Log_Chargen(string sLocation, string sMessage, GameObject oScope = null)
     {

#if DEBUG
          if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_CHARGEN, sLocation, sMessage, oScope);
          }
#endif
     }
}