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
         lot_functions_h.nss
         Lothering generic functions
     */
     //==============================================================================
     //  Created By: Kaelin
     //  Created On: August 25, 2008
     //==============================================================================

     //#include"utility_h"

     //#include"lot_constants_h"
     //#include"sys_ambient_h"

     //#include"plt_lot100pt_traps101"
     //#include"plt_lot100pt_herbalism101"
     //#include"plt_lot100pt_poison101"
     //#include"plt_lot100pt_last_keepsake"
     //#include"plt_lot100pt_bandits"
     //#include"plt_lot100pt_bandits2"
     //#include"plt_lot100pt_bears"
     //#include"plt_lot100pt_sten"
     //#include"plt_lot110pt_ser_donall"
     //#include"plt_lotpt_actions"

     //#include"plt_gen00pt_party"
     //#include"plt_cod_cha_sten"

     //#include"plt_lite_fite_conscripts"

     //moved public const int    TRIGGER_TYPE_TALK      = 1;
     //moved public const int    TRIGGER_TYPE_IDLE      = 2;
     //moved public const int    TRIGGER_TYPE_PRAY      = 3;
     //moved public const int    TRIGGER_TYPE_PLACEABLE = 4;

     //------------------------------------------------------------------------------

     /* @brief Close Open Lothering Quests
*
*   This will close any open quests the player has in Lothering.
*   Also, if Sten was not recruited, add a codex entry saying so.
*
* @author Kaelin
**/
     public void CloseLotheringQuests()
     {

          int bLastKeepsakeAccepted = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_LAST_KEEPSAKE, EngineConstants.LAST_KEEPSAKE_QUEST_ACCEPTED);
          int bLastKeepsakeRewarded = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_LAST_KEEPSAKE, EngineConstants.LAST_KEEPSAKE_REWARD_GIVEN);
          int bBandits2Accepted = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS2, EngineConstants.BANDITS2_QUEST_ACCEPTED);
          int bBandits2Rewarded = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS2, EngineConstants.BANDITS2_REWARD_GIVEN);
          int bHerbalismAccepted = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_HERBALISM101, EngineConstants.HERBALISM101_QUEST_ACCEPTED);
          int bHerbalismDone = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_HERBALISM101, EngineConstants.HERBALISM101_QUEST_DONE);
          int bPoisonAccepted = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_POISON101, EngineConstants.POISON101_QUEST_ACCEPTED);
          int bPoisonDone = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_POISON101, EngineConstants.POISON101_QUEST_COMPLETE);
          int bTrapsAccepted = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_TRAPS101, EngineConstants.TRAPS101_QUEST_ACCEPTED);
          int bTrapsDone = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_TRAPS101, EngineConstants.TRAPS101_QUEST_DONE);
          int bTrapsDoneFree = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_TRAPS101, EngineConstants.TRAPS101_QUEST_DONE_FREE);
          int bTrapsDonePlus1 = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_TRAPS101, EngineConstants.TRAPS101_QUEST_DONE_PLUS_1);
          int bTrapsDonePlus2 = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_TRAPS101, EngineConstants.TRAPS101_QUEST_DONE_PLUS_2);
          int bStenFound = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_STEN, EngineConstants.STEN_FOUND);
          int bStenReleased = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_STEN, EngineConstants.STEN_RELEASED);
          int bDonallFoundNote = WR_GetPlotFlag(EngineConstants.PLT_LOT110PT_SER_DONALL, EngineConstants.SER_DONALL_FOUND_NOTE);
          int bHenricKnowsDonIsDead = WR_GetPlotFlag(EngineConstants.PLT_LOT110PT_SER_DONALL, EngineConstants.SER_DONALL_KNOWS_HENRIC_IS_DEAD);
          int bBanditsKilled = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_KILLED);
          int bBanditsLetGo = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_LET_GO);
          int bBryantSentTemplar = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_BRYNAT_SENT_TEMPLARS);
          int bBanditsBribed = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_PC_GIVE_BRIBE);
          int bBanditsBribed2 = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_PC_GIVE_BRIBE_2);
          int bBanditsScaredByMage = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_INTIMIDATED_BY_MAGE);
          int bBanditsScaredByWard = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_INTIMIDATED_BY_GREY_WARDEN);
          int bBanditsFooled = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_FOOLED);
          int bBanditsAttack = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_ATTACK);
          int bBearsAccepted = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BEARS, EngineConstants.BEARS_QUEST_ACCEPTED);
          int bBearsDone = WR_GetPlotFlag(EngineConstants.PLT_LOT100PT_BEARS, EngineConstants.BEARS_REWARD_GIVEN);

          int bStenRecruited = WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_RECRUITED);

          int bConscriptsStarted = WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_CONSCRIPTS, EngineConstants.CONSCRIPTS_QUEST_GIVEN);
          int bLotheringConscript = WR_GetPlotFlag(EngineConstants.PLT_LITE_FITE_CONSCRIPTS, EngineConstants.CONSCRIPTS_RECRUITED_ONE);

          // Flag to check if Lothering has been destroyed.
          WR_SetPlotFlag(EngineConstants.PLT_LOTPT_ACTIONS, EngineConstants.ACTION_LOTHERING_DESTROYED, EngineConstants.TRUE, EngineConstants.TRUE);

          // If Sten was NOT recruited, set the codex entry to reflect that.
          if (bStenRecruited == EngineConstants.FALSE)
          {

               WR_SetPlotFlag(EngineConstants.PLT_COD_CHA_STEN, EngineConstants.COD_CHA_STEN_LEFT_TO_DIE, EngineConstants.TRUE, EngineConstants.TRUE);

          }

          // If the first bandits have been encountered but the situation not resolved, resolve it.
          if ((bBanditsKilled == EngineConstants.FALSE || bBanditsLetGo == EngineConstants.FALSE || bBryantSentTemplar == EngineConstants.FALSE) &&
                  (bBanditsBribed != EngineConstants.FALSE || bBanditsBribed2 != EngineConstants.FALSE || bBanditsScaredByMage != EngineConstants.FALSE ||
                   bBanditsScaredByWard != EngineConstants.FALSE || bBanditsFooled != EngineConstants.FALSE || bBanditsAttack != EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS, EngineConstants.BANDITS_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          // If the Last Keepsake quest was accepted but not completed.
          if ((bLastKeepsakeAccepted != EngineConstants.FALSE) && (bLastKeepsakeRewarded == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT100PT_LAST_KEEPSAKE, EngineConstants.LAST_KEEPSAKE_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          // If the Chanter's Board bandit quest has been accepted but not completed.
          if ((bBandits2Accepted != EngineConstants.FALSE) && (bBandits2Rewarded == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT100PT_BANDITS2, EngineConstants.BANDITS2_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          // Herbalism101  if quest accepted but not completed
          if ((bHerbalismAccepted != EngineConstants.FALSE) && (bHerbalismDone == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT100PT_HERBALISM101, EngineConstants.HERBALISM101_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          // Poison101  if quest accepted but not completed
          if ((bPoisonAccepted != EngineConstants.FALSE) && (bPoisonDone == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT100PT_POISON101, EngineConstants.POISON101_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          // Traps101  if quest accepted but not completed
          if ((bTrapsAccepted != EngineConstants.FALSE) && (bTrapsDone == EngineConstants.FALSE) && (bTrapsDoneFree == EngineConstants.FALSE)
             && (bTrapsDonePlus1 == EngineConstants.FALSE) && (bTrapsDonePlus2 == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT100PT_TRAPS101, EngineConstants.TRAPS101_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          // Sten met but not freed.
          if ((bStenFound != EngineConstants.FALSE) && (bStenReleased == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT100PT_STEN, EngineConstants.STEN_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          // Fallen Templar accepted but not completed
          if ((bDonallFoundNote != EngineConstants.FALSE) && (bHenricKnowsDonIsDead == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT110PT_SER_DONALL, EngineConstants.SER_DONALL_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          // Bears quest accepted but not completed
          if ((bBearsAccepted != EngineConstants.FALSE) && (bBearsDone == EngineConstants.FALSE))
          {
               WR_SetPlotFlag(EngineConstants.PLT_LOT100PT_BEARS, EngineConstants.BEARS_DARK_TIDE_ENDING, EngineConstants.TRUE, EngineConstants.TRUE);
          }

          //Light content - lite_fite_conscripts
          if (bConscriptsStarted != EngineConstants.FALSE && bLotheringConscript == EngineConstants.FALSE)
          {
               //if you haven't found the lothering conscript yet, you ain't going to
               WR_SetPlotFlag(EngineConstants.PLT_LITE_FITE_CONSCRIPTS, EngineConstants.CONSCRIPTS_QUEST_ABANDONED, EngineConstants.TRUE);
          }
          else if (bConscriptsStarted == EngineConstants.FALSE)
          {
               //if it hasn't been started yet - remove it from the boards
               WR_SetPlotFlag(EngineConstants.PLT_LITE_FITE_CONSCRIPTS, EngineConstants.CONSCRIPTS_FIGHTER_BOARD, EngineConstants.FALSE);
          }
     }

     // Play one of five pre-determined animations from ambient_ai.xls
     // Appropriate for conversing with other NPC's. ie. idle_chatter
     public int LOT_RandTalkAnim()
     {
          int nRand = Engine_Random(6);
          int nAnim = 0;

          if (nRand == 0) nAnim = 10;
          else if (nRand == 1) nAnim = 11;
          else if (nRand == 2) nAnim = 12;
          else if (nRand == 3) nAnim = 13;
          else if (nRand == 4) nAnim = 14;
          else if (nRand == 5) nAnim = 76;

          return nAnim;

     }

     // Play one of five pre-determined animations from ambient_ai.xls
     // Appropriate for standing around. ie. hands_behind_back.
     public int LOT_RandIdleAnim()
     {
          int nRand = Engine_Random(6);
          int nAnim = 0;

          if (nRand == 0) nAnim = 5;
          else if (nRand == 1) nAnim = 6;
          else if (nRand == 2) nAnim = 24;
          else if (nRand == 3) nAnim = 28;
          else if (nRand == 4) nAnim = 29;
          else if (nRand == 5) nAnim = 30;

          return nAnim;

     }

     // Play one of five pre-determined animations from ambient_ai.xls
     // Appropriate for interacting with placeables. ie. rummaging.
     public int LOT_RandPlaceableAnim()
     {
          int nRand = Engine_Random(2);
          int nAnim = 0;

          if (nRand == 0) nAnim = 16;
          else if (nRand == 1) nAnim = 35;

          return nAnim;
     }

     // Play one of six pre-determined animations from ambient_ai.xls
     // Appropriate for relaxing. ie. laying down.
     public int LOT_RandPrayAnim()
     {
          int nRand = Engine_Random(2);
          int nAnim = 0;

          if (nRand == 0) nAnim = 9;
          else if (nRand == 1) nAnim = 33;

          return nAnim;
     }

     /* @brief Determine which animation to play based on the trigger tag.
*
*   For this function everything is based on the tag name.
*
*   The tag name convention is lot100tr_ambient_x where x is a single int
*   between 1 and 4 inclusive. Each int represents a different trigger type:
*
*   1   -   Conversation Trigger
*   2   -   Idle Trigger
*   3   -   Pray Trigger
*   4   -   Placeable Interaction Trigger
*
*   This will return a random animation int from ambient_ai.xls appropriate for
*   the trigger type.
*
*
* @author Kaelin
**/
     public int LOT_DetermineAnimToPlay(GameObject oTrigger)
     {
          int nAnimOverride = 0;

          // Get the tag and length of the Trigger.
          string sTag = GetTag(oTrigger);
          int nStringLength = GetStringLength(sTag);

          Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "***** Trigger Tag is: ", sTag);

          // Get the substring for the trigger type.
          string sTriggerType = SubString(sTag, nStringLength - 1, 1);
          int nType = StringToInt(sTriggerType);

          Log_Trace(EngineConstants.LOG_CHANNEL_AMBIENT_AI, "***** Trigger Type is: ", sTriggerType);

          switch (nType)
          {
               case EngineConstants.TRIGGER_TYPE_TALK:
                    {
                         nAnimOverride = LOT_RandTalkAnim();

                         break;
                    }

               case EngineConstants.TRIGGER_TYPE_IDLE:
                    {
                         nAnimOverride = LOT_RandIdleAnim();

                         break;
                    }

               case EngineConstants.TRIGGER_TYPE_PRAY:
                    {
                         nAnimOverride = LOT_RandPrayAnim();

                         break;
                    }

               case EngineConstants.TRIGGER_TYPE_PLACEABLE:
                    {
                         nAnimOverride = LOT_RandPlaceableAnim();

                         break;
                    }

          }

          return nAnimOverride;

     }

     // Restores ambient animations for an entire team.
     public void LOT_RestoreTeamAmbient(int nTeamID)
     {

          List<GameObject> arTeam = GetTeam(nTeamID);
          GameObject oCurrent;

          int nTeamSize = GetArraySize(arTeam);
          int nLoop;

          for (nLoop = 0; nLoop < nTeamSize; nLoop++)
          {

               oCurrent = arTeam[nLoop];

               WR_ClearAllCommands(oCurrent, EngineConstants.TRUE);

               Ambient_RestoreBehaviour(oCurrent);

          }

     }
}