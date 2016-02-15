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
     //#include"log_h"
     //#include"global_objects_h"
     //#include"wrappers_h"
     //#include"rules_h"
     //#include"sys_rubberband_h"
     //#include"effect_resurrection_h"
     //#include"events_h"
     //#include"ai_constants_h"
     //#include"stats_core_h"
     //#include"plt_tut_friendly_aoe"

     /* @addtogroup scripting_utility Scripting Utility
     *
     * Generic level design functions
     */
     /* @{*/

     //public void main() {}

     // Attribute checks
     //moved public const int EngineConstants.UT_ATTR_HIGH = 1;
     //moved public const int EngineConstants.UT_ATTR_MED = 2;
     //moved public const int EngineConstants.UT_ATTR_LOW = 3;

     //Skill checks
     //moved public const int EngineConstants.UT_SKILL_CHECK_LOW = 1;
     //moved public const int EngineConstants.UT_SKILL_CHECK_MED = 2;
     //moved public const int EngineConstants.UT_SKILL_CHECK_HIGH = 3;
     //moved public const int EngineConstants.UT_SKILL_CHECK_VERY_HIGH = 4;

     //Intimidation checks
     //moved public const int INTIMIDATE_CHECK_VERY_HIGH = 50;
     //moved public const int INTIMIDATE_CHECK_HIGH = 30;
     //moved public const int INTIMIDATE_CHECK_MEDIUM = 15;
     //moved public const int INTIMIDATE_CHECK_LOW = 5;

     //Skills
     //moved public const int EngineConstants.SKILL_PERSUADE = EngineConstants.ABILITY_SKILL_PERSUADE_1;
     //moved public const int EngineConstants.SKILL_HERBALISM = EngineConstants.ABILITY_SKILL_HERBALISM_1;
     //moved public const int EngineConstants.SKILL_POSION = EngineConstants.ABILITY_SKILL_POISON_1;
     //moved public const int EngineConstants.SKILL_TRAPS = EngineConstants.ABILITY_SKILL_TRAPS_1;
     //moved public const int EngineConstants.SKILL_STEALTH = EngineConstants.ABILITY_SKILL_STEALTH_1;
     //moved public const int EngineConstants.SKILL_STEALING = EngineConstants.ABILITY_SKILL_STEALING_1;
     //moved public const int EngineConstants.SKILL_SURVIVAL = EngineConstants.ABILITY_SKILL_SURVIVAL_1;
     //moved public const int EngineConstants.SKILL_LOCKPICKING = EngineConstants.ABILITY_SKILL_LOCKPICKING_1;
     //moved public const int EngineConstants.SKILL_INTIMIDATE = 9;

     // Generic exit
     //moved public const string EngineConstants.GENERIC_EXIT = "wp_gen_exit";

     //moved public const int EngineConstants.MAX_CREATURES_IN_AREA = 1000;

     // Surrender constants.
     //moved public const string EngineConstants.SURR_SURRENDER_ENABLED = "SURR_SURRENDER_ENABLED";
     //moved public const string EngineConstants.SURR_INIT_CONVERSATION = "SURR_INIT_CONVERSATION";
     //moved public const string EngineConstants.SURR_PLOT_NAME         = "SURR_PLOT_NAME";
     //moved public const string EngineConstants.SURR_PLOT_FLAG         = "SURR_PLOT_FLAG";

     //moved public const int EngineConstants.SURR_STATUS_DISABLED = 0; // The system is disabled.
     //moved public const int EngineConstants.SURR_STATUS_ENABLED  = 1; // System enabled; ready to surrender.
     //moved public const int EngineConstants.SURR_STATUS_ACTIVE   = 2; // System active; currently surrendering.

     // World map 'area' ID for area transition system
     //moved public const string UT_WORLD_MAP = "world_map";

     // public void unequips an item from an inventory slot
     public void UT_UnquipItem(GameObject oCreature, int nSlot, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
     {
          GameObject oItem = GetItemInEquipSlot(nSlot, oCreature, nWeaponSet);
          if (IsObjectValid(oItem) != EngineConstants.FALSE)
               UnequipItem(oCreature, oItem);
     }

     /******************************************************************************
     * FUNCTION DEFINITIONS
     *******************************************************************************/

     /* @brief Instantly initiate dialog with 2 objects
*
* Calling this function will instantly trigger dialog between 2 objects. The dialog
* can be ambient or not.
*
* @param oInitiator - The main talking creature - owner of the default dialog file, if any
* @param oTarget    - The creature being spoken to. Should be the player GameObject most of the time
* @returns EngineConstants.TRUE on success, EngineConstants.FALSE on error
* @author Yaron
*/
     public void UT_Talk(GameObject oSpeaker, GameObject oListener, string rConversation = "", int nPartyResurrection = EngineConstants.TRUE)
     {

          string sDialogResRef = ResourceToString(rConversation);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utlity_h.UT_Talk", "speaker: " + GetTag(oSpeaker) + ", listener: " + GetTag(oListener) + ", conversation: " +
              sDialogResRef);

          if (GetGameMode() == EngineConstants.GM_DEAD)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utlity_h.UT_Talk", "PARTY IS DEAD - CANT TRIGGER CONVERSATION!", null, EngineConstants.LOG_SEVERITY_CRITICAL);
               return;
          }

          int n = GetLocalInt(GetModule(), EngineConstants.DISABLE_FOLLOWER_CONVERSATION);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utlity_h.UT_Talk", "disable follower dialog=  " + IntToString(n), null, EngineConstants.LOG_SEVERITY_CRITICAL);

          if (GetFollowerState(oSpeaker) != EngineConstants.FOLLOWER_STATE_INVALID && GetLocalInt(GetModule(), EngineConstants.DISABLE_FOLLOWER_CONVERSATION) == 1 && GetHero() != GetPartyLeader())
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utlity_h.UT_Talk", "Party leader is not hero when clicking on follower - running soundset instead", null, EngineConstants.LOG_SEVERITY_CRITICAL);
               PlaySoundSet(oSpeaker, EngineConstants.SS_YES);
               return;
          }

          if (nPartyResurrection != EngineConstants.FALSE)
               ResurrectPartyMembers(EngineConstants.FALSE);

          ClearAmbientDialogs(oSpeaker); // this makes sure an already running ambient dialog triggers it's plot flag action

          if (IsFollower(oListener) != EngineConstants.FALSE)
               oListener = GetPartyLeader();

          GameObject oModule = GetModule();
          string rOverrideConversation = GetLocalResource(oModule, EngineConstants.PARTY_OVERRIDE_CONVERSATION);
          int bOverride = GetLocalInt(oModule, EngineConstants.PARTY_OVERRIDE_CONVERSATION_ACTIVE);

          if (IsFollower(oSpeaker) != EngineConstants.FALSE
             && bOverride != EngineConstants.FALSE
             && GetPartyLeader() != GetHero())
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utlity_h.UT_Talk", "overriding dialog with: " + ResourceToString(rOverrideConversation));
               rConversation = rOverrideConversation;
               sDialogResRef = ResourceToString(rConversation);
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utlity_h.UT_Talk", "Triggering BeginConversation NOW");

          //TrackDialogEvent(EngineConstants.EVENT_TYPE_CONVERSATION, oSpeaker, oListener, sDialogResRef);

          // //Track the number of dialogues initiated
          //STATS_TrackStartedDialogues(oListener);

          BeginConversation(oListener, oSpeaker, rConversation);
     }

     /* @brief Sets an GameObject to use shouts in his dialog file.
*
* This function sets an GameObject to use shouts whenever dialog is triggered.
* The function sets a variable on the object, that is read and cleared by
* Generic conversation flags. The flags can then be used to flag a shouts tree
* in the conversation.
*
* @param oObject - The GameObject whose shouts flag is being changed
* @param bEnable - EngineConstants.TRUE - enable shouts, EngineConstants.FALSE - disable
* @sa UT_GetShoutsFlag
* @author Yaron
*/
     public void UT_SetShoutsFlag(GameObject oObject, int bEnable = EngineConstants.TRUE)
     {
          SetLocalInt(oObject, EngineConstants.SHOUTS_ACTIVE, bEnable);
          // TBD once we have variables system
     }

     /* @brief Returns the shout flag for an object
*
* brief Returns the shout flag for an object
*
* @param oObject - The GameObject whose shouts flag is being changed
* @param bEnable - EngineConstants.TRUE - enable shouts, EngineConstants.FALSE - disable
* @returns the shouts flag for the creature (EngineConstants.TRUE or EngineConstants.FALSE)
* @sa UT_GetShoutsFlag
* @author Yaron
*/
     public int UT_GetShoutsFlag(GameObject oObject)
     {
          return GetLocalInt(oObject, EngineConstants.SHOUTS_ACTIVE);
     }

     /* @brief Forces a creature to stop combat and surrender to the player
*
* Sets a creature to surrender. This includes stopping combat, changing group hostility
* and to fire a one liner conversation (set EngineConstants.GEN_SURRENDER_DURING).
* This function also sets the creature to trigger special post-surrender dialog
* after the surrendering if the variables EngineConstants.SURR_PLOT_NAME and EngineConstants.SURR_PLOT_FLAG
* are set appropriately on the creature.
*
* Implemented Aug 16, 2006, Grant Mackay: No one liner conversation fired;
* straight to surrender dialog.
*
* @param oCreature - The surrendering creature
* @sa UT_SetShoutsFlag
* @author Yaron
*/
     public void UT_Surrender(GameObject oCreature)
     {
          GameObject oPC = GetPartyLeader();
          int i;

          // Have the creature end combat with the player.
          UT_CombatStop(oCreature, oPC);

          // Have all hostiles end combat with player.
          List<GameObject> arHostile = GetNearestObjectByHostility(oPC, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.MAX_CREATURES_IN_COMBAT, EngineConstants.TRUE);
          int nSize = GetArraySize(arHostile);

          GameObject oHostile;

          for (i = 0; i < nSize; ++i)
          {
               oHostile = arHostile[i];
               if (GetCombatState(oHostile) != EngineConstants.FALSE)
                    UT_CombatStop(arHostile[i], oPC);
          }

          // Have the player's party end combat with the surrenderer.
          List<GameObject> arParty = GetPartyList();
          nSize = GetArraySize(arParty);
          for (i = 0; i < nSize; ++i)
          {
               UT_CombatStop(arParty[i], oCreature);
          }

          // Update the surrender flag to reflect the active status for combat end verification.
          SetLocalInt(oCreature, EngineConstants.SURR_SURRENDER_ENABLED, EngineConstants.SURR_STATUS_ACTIVE);

     }

     /*
 * @brief Sets up a creature to surrender when they get low on health in combat.
 *
 * Enables the creature to surrender during combat and sets up the surrender
 * plot flag if sepcified.
 *
 * @param oCreature - The surrendering creature.
 * @param bSurrender - Set or un-set the creature's surrender functionality. Default is EngineConstants.TRUE.
 * @param InitConversation - Creature will initiate a conversation on surrendering if this is EngineConstants.TRUE.
 * @param sPlotName - The name of the plot which should have a flag set as the creature surrenders, if any. Default is empty.
 * @param nPlotFlag - The plot flag to be set as the creature surrenders, if any. The variable is irrelevant if sPlotName is empty.
 *
 * @author Grant
 */
     public void UT_SetSurrenderFlag(GameObject oCreature, int bSurrender = EngineConstants.TRUE, string sPlotName = "", int nPlotFlag = 0, int bInitConversation = EngineConstants.TRUE)
     {

          SetLocalInt(oCreature, EngineConstants.SURR_SURRENDER_ENABLED, bSurrender);

          if (bSurrender != EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "UT_SetSurrenderFlag", "Setting immortal to: EngineConstants.TRUE");
          else
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "UT_SetSurrenderFlag", "Setting immortal to: EngineConstants.FALSE");

          SetImmortal(oCreature, bSurrender);

          if (sPlotName != "")
          {

               SetLocalString(oCreature, EngineConstants.SURR_PLOT_NAME, sPlotName);
               SetLocalInt(oCreature, EngineConstants.SURR_PLOT_FLAG, nPlotFlag);

          }

          SetLocalInt(oCreature, EngineConstants.SURR_INIT_CONVERSATION, bInitConversation);
     }

     /* @brief Checks the attribute level of oObject
*
* This function checks the attribute level of oObject.
* nAttribute should be a constant represneting one of the attributes.
* Notice that an attribute level is dynamic and depends on the player�s level � the
* player will need to keep increasing his attribute in order to keep it in the �high� level.
*
* @param nAttribute - The attribute being checked
* @param nLevel - The level of the attribute being checked (EngineConstants.UT_ATTR_HIGH, EngineConstants.UT_ATTR_MED or EngineConstants.UT_ATTR_LOW)
* @param oCreature - the creature doing the attribute check
* @returns EngineConstants.TRUE if the attribute matches the specified level range, EngineConstants.FALSE otherwise.
* @author Yaron
*/
     public int UT_AttributeCheck(int nAttribute, int nLevel, GameObject oPlayer = null)
     {
          if (oPlayer == null) oPlayer = gameObject;
          // TBD
          // GZ: Remember to cast into to float on the attributes
          // to get an attribute value, use
          // GetCreatureProperty(oPlayer,nAttribute,EngineConstants.PROPERTY_VALUE_TOTAL);

          float fTargetLevel = 10.0f;
          float fPlayerLevel = GetCreatureProperty(oPlayer, EngineConstants.PROPERTY_SIMPLE_LEVEL);
          float fValue = GetCreatureProperty(oPlayer, nAttribute, EngineConstants.PROPERTY_VALUE_TOTAL);

          int nResult = EngineConstants.FALSE;
          switch (nLevel)
          {
               case EngineConstants.UT_ATTR_HIGH:
                    nResult = (fValue >= 30.0f) ? EngineConstants.TRUE : EngineConstants.FALSE;
                    break;

               case EngineConstants.UT_ATTR_MED:
                    nResult = (fValue >= 15.0f) ? EngineConstants.TRUE : EngineConstants.FALSE;
                    break;

               case EngineConstants.UT_ATTR_LOW:
                    nResult = EngineConstants.TRUE;
                    break;

               default:
                    // georg: dev warning if called with invalid parameters
                    Warning("[UT_AttributeCheck] Attribute check against unknown nLevel. Please notify yaron. Details: " + GetCurrentScriptName());
                    break;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_AttributeCheck", "Checking Attribute:" + ToString(nAttribute) + " Level:" + ToString(nLevel) + " Actual: " + ToString(fValue) + " Target:" + ToString(fTargetLevel) + " Result:" + ToString(nResult));

          return nResult;

     }

     public int _UT_GetIsPlotFollower(GameObject oFollower)
     {
          int nRet = EngineConstants.FALSE;
          string sTag = GetTag(oFollower);
          if (sTag == EngineConstants.GEN_FL_ALISTAIR || sTag == EngineConstants.GEN_FL_DOG ||
              sTag == EngineConstants.GEN_FL_MORRIGAN || sTag == EngineConstants.GEN_FL_WYNNE ||
              sTag == EngineConstants.GEN_FL_SHALE || sTag == EngineConstants.GEN_FL_STEN ||
              sTag == EngineConstants.GEN_FL_ZEVRAN || sTag == EngineConstants.GEN_FL_OGHREN ||
              sTag == EngineConstants.GEN_FL_LELIANA || sTag == EngineConstants.GEN_FL_LOGHAIN)
          {
               nRet = EngineConstants.TRUE;
          }

          return nRet;
     }

     /* @brief Hire non-plot follower into the active party for oPC
*
* Hire follower into the active party for oPC. This function will do nothing for plot followers.
*
* @param oFollower - The creature joining the party
* @param bPreventLevelup - whether or not to prevent the follower from levelling up
* @sa FireFollower
* @author Yaron
*/
     public void UT_HireFollower(GameObject oFollower, int bPreventLevelup = EngineConstants.FALSE)
     {
          GameObject oPC = GetPartyLeader();
          if (IsObjectValid(oFollower) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_HireFollower", "INVALID FOLLOWER OBJECT!", gameObject, EngineConstants.LOG_SEVERITY_CRITICAL);
               return;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_HireFollower",
              "Trying to hire follower: " + GetTag(oFollower));

          if (_UT_GetIsPlotFollower(oFollower) != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_HireFollower",
                   "This function can not be used for plot followers! - use plot flags instead!", oFollower, EngineConstants.LOG_SEVERITY_CRITICAL);
               return;
          }

          SetAutoLevelUp(oFollower, 2);
          SetGroupId(oFollower, GetGroupId(oPC));
          WR_SetFollowerState(oFollower, EngineConstants.FOLLOWER_STATE_ACTIVE, EngineConstants.TRUE, 0, EngineConstants.TRUE);
          //SendPartyMemberHiredEvent(oFollower, EngineConstants.FALSE, 0, bPreventLevelup);

          //Show the AOE flag when the PC is a mage and aquires a follower or the follower hired is a mage
          if (GetLocalInt(GetModule(), EngineConstants.TUTORIAL_ENABLED) != EngineConstants.FALSE && (GetCreatureCoreClass(oPC) == EngineConstants.CLASS_WIZARD || GetCreatureCoreClass(oFollower) == EngineConstants.CLASS_WIZARD))
          {
               WR_SetPlotFlag(EngineConstants.PLT_TUT_FRIENDLY_AOE, EngineConstants.TUT_FRIENDLY_AOE_1, EngineConstants.TRUE);
          }
     }

     /* @brief Removes a non-plot follower from the active party, sending him back to the party pool
*
* Removes a follower from the active party, sending him back to the party pool. This function will do nothing for plot followers.
*
* @param oFollower - The creature leaving the party
* @param bRemoveFromPool - Remove or not from the party pool
* @sa HireFollower
* @author Yaron
*/
     public void UT_FireFollower(GameObject oFollower, int bRemoveFromPool = EngineConstants.FALSE, int bRemoveEquipment = EngineConstants.TRUE)
     {
          if (IsObjectValid(oFollower) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_FireFollower", "INVALID FOLLOWER OBJECT!", gameObject, EngineConstants.LOG_SEVERITY_CRITICAL);
               return;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_FireFollower",
              "Trying to remove follower from the party: " + GetTag(oFollower));

          if (_UT_GetIsPlotFollower(oFollower) != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_FireFollower",
                   "This function can not be used for plot followers! - use plot flags instead!", oFollower, EngineConstants.LOG_SEVERITY_CRITICAL);
               return;
          }

          // Removing from active party -> back into the pool
          WR_SetFollowerState(oFollower, EngineConstants.FOLLOWER_STATE_AVAILABLE);

          if (bRemoveFromPool != EngineConstants.FALSE) // If we want to premanently remove the follower (active party AND pool)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_FireFollower",
                           "removing follower from active party AND party pool: " + GetTag(oFollower));
               WR_SetFollowerState(oFollower, EngineConstants.FOLLOWER_STATE_INVALID);
          }

          GameObject oLeader = GetPartyLeader();

          if (bRemoveEquipment != EngineConstants.FALSE)
          {
               List<GameObject> arItems = GetItemsInInventory(oFollower, EngineConstants.GET_ITEMS_OPTION_EQUIPPED);
               int nSize = GetArraySize(arItems);
               GameObject oCurrent;
               int i;

               for (i = 0; i < nSize; i++)
               {
                    oCurrent = arItems[i];
                    MoveItem(oFollower, oLeader, oCurrent);
               }
          }

     }

     /* @brief Checking to see if oObject has enough skill level for a specific skill
*
* Checking to see if oObject has enough skill level for a specific skill
*
* @param nSkill - the skill being checked
* @param nLevel - the level of the skill being checked
* @param oObject - the creature attempting the skill check
* @returns EngineConstants.TRUE if the skill checked succeeded, EngineConstants.FALSE otherwise
* @author Yaron
*/
     public int UT_SkillCheck(int nSkill, int nLevel, GameObject oObject = null)
     {
          if (oObject == null) oObject = gameObject;
          //There is an override set on the moduel to ensure success or failure.
          //if the override is 0, do not use it.
          //if the override is 1, always return EngineConstants.TRUE.
          //if the override is anything else, always return EngineConstants.FALSE.
          //David Sims, Novermber 7, 2006

          // Intimidate will now work like persuade
          // Yaron Jan 12, 2009
          GameObject oModule = GetModule();
          int nOverride = GetLocalInt(oModule, EngineConstants.DEBUG_SKILL_CHECK_OVERRIDE);
          int bReturn = EngineConstants.FALSE;
          if (nOverride == 0)
          {
               //Herbalism, survival, poison, traps
               if (nSkill == EngineConstants.SKILL_HERBALISM || nSkill == EngineConstants.SKILL_POSION || nSkill == EngineConstants.SKILL_SURVIVAL || nSkill == EngineConstants.SKILL_TRAPS)
               {
                    List<GameObject> oParty = GetPartyList(oObject);
                    int nSize = GetArraySize(oParty);
                    int i;
                    GameObject oCurrent;
                    for (i = 0; i < nSize; i++)
                    {
                         if (bReturn == EngineConstants.FALSE)
                         {
                              bReturn = GetHasSkill(nSkill, nLevel, oParty[i]);
                         }
                    }
               }
               else if (nSkill == EngineConstants.SKILL_PERSUADE || nSkill == EngineConstants.SKILL_INTIMIDATE)
               {
                    // Each skill rank is equal 25 points
                    // Each difficulty rank is 25 points
                    // The player gets a bonus skill rank check of 1 point per attribute bonus value

                    int nSkillCheck = 0;
                    int nAttribute = 0;
                    if (nSkill == EngineConstants.SKILL_PERSUADE)
                         nAttribute = EngineConstants.ATTRIBUTE_INT;
                    else if (nSkill == EngineConstants.SKILL_INTIMIDATE)
                         nAttribute = EngineConstants.ATTRIBUTE_STR;

                    int nCheckBonus = FloatToInt(GetAttributeModifier(oObject, nAttribute));
                    if (GetHasSkill(EngineConstants.SKILL_PERSUADE, 1, oObject) != EngineConstants.FALSE)
                         nSkillCheck = 25;
                    if (GetHasSkill(EngineConstants.SKILL_PERSUADE, 2, oObject) != EngineConstants.FALSE)
                         nSkillCheck = 50;
                    if (GetHasSkill(EngineConstants.SKILL_PERSUADE, 3, oObject) != EngineConstants.FALSE)
                         nSkillCheck = 75;
                    if (GetHasSkill(EngineConstants.SKILL_PERSUADE, 4, oObject) != EngineConstants.FALSE)
                         nSkillCheck = 100;

                    nSkillCheck += nCheckBonus;

                    if (nLevel == EngineConstants.UT_SKILL_CHECK_LOW && nSkillCheck >= 25)
                         bReturn = EngineConstants.TRUE;
                    else if (nLevel == EngineConstants.UT_SKILL_CHECK_MED && nSkillCheck >= 50)
                         bReturn = EngineConstants.TRUE;
                    else if (nLevel == EngineConstants.UT_SKILL_CHECK_HIGH && nSkillCheck >= 75)
                         bReturn = EngineConstants.TRUE;
                    else if (nLevel == EngineConstants.UT_SKILL_CHECK_VERY_HIGH && nSkillCheck >= 100)
                         bReturn = EngineConstants.TRUE;

               }
               else if (nSkill == EngineConstants.SKILL_LOCKPICKING)
               {
                    float fPlayerScore = GetDisableDeviceLevel(oObject);
                    float fTargetScore;

                    if (nLevel == EngineConstants.UT_SKILL_CHECK_VERY_HIGH)
                    {
                         fTargetScore = 60.0f; // very hard
                    }
                    else if (nLevel == EngineConstants.UT_SKILL_CHECK_HIGH)
                    {
                         fTargetScore = 40.0f; // medium
                    }
                    else if (nLevel == EngineConstants.UT_SKILL_CHECK_MED)
                    {
                         fTargetScore = 20.0f; // very easy
                    }
                    else
                    {
                         fTargetScore = 1.0f; // auto-success
                    }

                    if (fPlayerScore >= fTargetScore)
                    {
                         bReturn = EngineConstants.TRUE;
                    }
               }
               else
               {
                    //if the override is not set, random result.
                    bReturn = GetHasSkill(nSkill, nLevel, oObject);
               }
          }
          else if (nOverride == 1)
          {
               bReturn = EngineConstants.TRUE;
          }
          else
          {
               bReturn = EngineConstants.FALSE;
          }

          return bReturn;
     }

     /* @brief Returns the nearest creature
*
* Returns the nearest creature
*
* @param oObject - the GameObject that we try to find a nearest creature from
* @returns the nearest creature to oObject
* @sa UT_GetNearestCreatureByTag, UT_GetNearestObjectByTag, UT_GetNearestCreatureByGroup, UT_GetNearestHostileCreature
* @author Yaron

*/
     public GameObject UT_GetNearestCreature(GameObject oObject, int nIncludeSelf = EngineConstants.FALSE)
     {
          List<GameObject> arCreatures = GetNearestObject(oObject, EngineConstants.OBJECT_TYPE_CREATURE, 1, EngineConstants.FALSE, EngineConstants.FALSE, nIncludeSelf);
          return arCreatures[0];
     }

     /* @brief Returns the nearest creature with a specific tag
*
* Returns the nearest creature with a specific tag
*
* @param oObject - the GameObject that we try to find a nearest creature from
* @param sTag - the tag of the creature we are looking for
* @returns the nearest creature to oObject with the specified tag
* @sa UT_GetNearestCreature, UT_GetNearestObjectByTag, UT_GetNearestCreatureByGroup, UT_GetNearestHostileCreature
* @author Yaron
*/
     public GameObject UT_GetNearestCreatureByTag(GameObject oObject, string sTag, int nIncludeSelf = EngineConstants.FALSE)
     {
          List<GameObject> arCreatures = GetNearestObjectByTag(oObject, sTag, EngineConstants.OBJECT_TYPE_CREATURE, 1, EngineConstants.FALSE, EngineConstants.FALSE, nIncludeSelf);
          return arCreatures[0];
     }

     /* @brief Returns the nearest creature from a specific group
*
* Returns the nearest creature from a specific group
*
* @param oObject - the GameObject from which we are trying to find a nearest creature from
* @param nGroup - the group of the creature we are looking for
* @returns the nearest creature to oObject from a specific group
* @sa UT_GetNearestCreature, UT_GetNearestCreatureByTag, UT_GetNearestObjectByTag, UT_GetNearestHostileCreature
* @author Yaron
*/
     public GameObject UT_GetNearestCreatureByGroup(GameObject oObject, int nGroup, int nIncludeSelf = EngineConstants.FALSE)
     {
          List<GameObject> arCreatures = GetNearestObjectByGroup(oObject, nGroup, EngineConstants.OBJECT_TYPE_CREATURE, 1, EngineConstants.FALSE, EngineConstants.FALSE, nIncludeSelf);
          return arCreatures[0];
     }

     /* @brief Returns the nearest living hostile creature
*
* Returns the nearest living hostile creature
*
* @param oObject - the GameObject from which we are trying to find a nearest creature from
* @param nGroup - the group of the creature we are looking for
* @returns the nearest creature to oObject from a specific group
* @sa UT_GetNearestCreature, UT_GetNearestCreatureByTag, UT_GetNearestObjectByTag, UT_GetNearestCreatureByGroup
* @author Yaron
*/
     public GameObject UT_GetNearestHostileCreature(GameObject oObject, int nCheckLiving = EngineConstants.FALSE)
     {
          List<GameObject> arCreatures = GetNearestObjectByHostility(oObject, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.MAX_CREATURES_IN_COMBAT, nCheckLiving);
          int i;
          GameObject oCreature = null;
          int nSize = GetArraySize(arCreatures);
          for (i = 0; i < nSize; i++)
          {
               oCreature = arCreatures[i];
               if (IsDead(oCreature) == EngineConstants.FALSE)
                    break;
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_GetNearestHostileCreature", "returning: " + GetTag(oCreature));
#endif
          return oCreature;
     }

     /* @brief Transitions the entire party to a new area
*
* Jumps the party to sWP in a different area
*
* @param sArea - target area for the transition
* @param sWP - target wp for the transition
* @param sWorldMapLoc1 - world map Vector3 to set active
* @param sWorldMapLoc2 - world map Vector3 to set active
* @param sWorldMapLoc3 - world map Vector3 to set active
* @author Yaron
*/
     public void UT_DoAreaTransition(string sArea, string sWP, string sWorldMapLoc1 = "", string sWorldMapLoc2 = "", string sWorldMapLoc3 = "", string sWorldMapLoc4 = "", string sWorldMapLoc5 = "")
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_DoAreaTransition", "area= " + sArea + ", wp= " + sWP);
          int nSetLocationActive = -1;

          // First, check if specific area load hint is selected
          int iAreaHint = GetLocalInt(GetModule(), EngineConstants.AREA_LOAD_HINT);
          if (iAreaHint != 0)
          {
               SetLoadHint(iAreaHint, EngineConstants.TABLE_AREA_LOAD_HINT);
               SetLocalInt(GetModule(), EngineConstants.AREA_LOAD_HINT, 0);
          }
          else
          {
               // no specific area load hint. pick randomally
               // levels 1-3: very low level table
               // levels 4-7: low level table
               // levels 8-12: mid level table
               // levels 13+ high level table
               int nLevel = GetLevel(GetHero());
               int nTable;
            if (nLevel <= 3) nTable = EngineConstants.TABLE_AREA_LOAD_HINT_VLOW;//284;
            else if (nLevel > 3 && nLevel <= 7) nTable = EngineConstants.TABLE_AREA_LOAD_HINT_LOW;
            else if (nLevel > 7 && nLevel <= 12) nTable = EngineConstants.TABLE_AREA_LOAD_HINT_MID;
            else nTable = EngineConstants.TABLE_AREA_LOAD_HINT_HIGH;

               int iRows = GetM2DARows(nTable);
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_DoAreaTransition", "rows1= " + IntToString(iRows));
               int nRandRow = Engine_Random(iRows - 1);//DHK Just in case
               nRandRow = GetM2DARowIdFromRowIndex(nTable, nRandRow);
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_DoAreaTransition", "rows2= " + IntToString(iRows));
               SetLoadHint(nRandRow, nTable);
          }

          if (sArea == EngineConstants.UT_WORLD_MAP)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_DoAreaTransition", "world map transition");
               SendEventTransitionToWorldMap(sArea, sWP, sWorldMapLoc1, sWorldMapLoc2, sWorldMapLoc3, sWorldMapLoc4, sWorldMapLoc5);
          }
          else
          {
               DoAreaTransition(sArea, sWP);
          }
     }

     /* @brief Transitions the entire party to a new area
*
* Jumps the party to sWP. If only sWP is specified then its a local area transition (same area list).
* If sArea and sAreaList are specified then its an area list transition
*
* @param oPlayer - the player whose party we are transitioning
* @param sWP - target wp for the transition
* @param sArea - target area for the transition (only for area list transitions)
* @param sAreaList - target area list for the transition (only for area list transitions)
* @author Yaron
*/
     /*public void UT_AreaTransition(GameObject oPlayer, string sWP, string sArea = "", string sAreaList = "")
     {
         if(sArea == "") // Tranition within the same area list
         {
             GameObject oWP = GetObjectByTag(sWP);
             if(!IsObjectValid(oWP))
             {
                 return;
             }
             xCommand cJump = CommandJumpToObject(oWP);
             WR_ClearAllCommands(oPlayer);
             WR_AddCommand(oPlayer, cJump);
         }
         else // area list transition
         {

            // ChangeAreaList(sAreaList, sArea, sWP);
         }

     }*/

     /* @brief Makes someone go to the exit and then destroy himself.
*
* @param oTarg - The GameObject that is going to walk someplace.
* @param bRun - Whether the target will walk or run to the exit.
* @param sWP - This is an override string, if left blank oTarg will go to the nearest "wp_gen_exit".
* @param bRandomWait - Adds a short, random length wait xCommand before the move command
* @author Ferret
**/
     public void UT_ExitDestroy(GameObject oTarg, int bRun = EngineConstants.FALSE, string sWP = EngineConstants.GENERIC_EXIT, int bRandomWait = EngineConstants.FALSE)
     {
          float fWait;

          if (IsObjectValid(oTarg) == EngineConstants.FALSE) Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_ExitDestroy was passed a bad GameObject from " + GetTag(gameObject));
          else Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_ExitDestroy was passed GameObject " + GetTag(oTarg));

          GameObject oExit = UT_GetNearestObjectByTag(oTarg, sWP);
          xEvent evSetActive = Event(EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE);
          SetEventIntegerRef(ref evSetActive, 0, EngineConstants.FALSE);

          // Turn off HOME behavior
          SetLocalInt(oTarg, EngineConstants.RUBBER_HOME_ENABLED, 0);

          SetObjectInteractive(oTarg, EngineConstants.FALSE);

          if (bRandomWait != EngineConstants.FALSE)
          {
               // Add a random-length wait command. Useful when a group exits at
               // the same time, so their exits are slightly staggered
               fWait = RandomFloat() * 1.5f;
               WR_AddCommand(oTarg, CommandWait(fWait), EngineConstants.FALSE, EngineConstants.TRUE);
          }
          Vector3 lLoc = GetLocation(oExit);
          WR_AddCommand(oTarg, CommandMoveToLocation(lLoc, bRun, EngineConstants.TRUE), EngineConstants.FALSE, EngineConstants.TRUE);

     }

     /* @brief Makes someone jump to a specified waypoint within the same area
*
*   This should be used only for area/stage setup - arranging creatures for a certain encounter beforehand.
*   This should NOT be used for any instances in which the user can witness the jump. Note that this function
*   should be used only for jumps within the same area.
* @param oTarg - The GameObject that is going to jump someplace.
* @param sWP - If the string is a # then oTarg will go to "jp_<OBJTAG>_#". If sWP is blank it defaults to 0. If something other than a # is here, then that is the string of the destination waypoint. NOTE: If the waypoint string starts with a number, it will be treated as a number not a string.
* @param nJumpImmediately - If EngineConstants.TRUE, forces the jump to the front of the action queue.
* @param bNewHome - By default a creature's new "Home" is updated when you tell them to move someplace. Set this to EngineConstants.FALSE if you want to keep their home point as is.
* @param bStaticCommand - the xCommand will not be cleared by a standard WR_ClearAllCommands
* @author Ferret
**/
     public void UT_LocalJump(GameObject oTarg, string sWP = "", int nJumpImmediately = EngineConstants.TRUE, int bNewHome = EngineConstants.TRUE, int bStaticCommand = EngineConstants.FALSE, int bJumpParty = EngineConstants.FALSE)
     {

          if (IsPartyMember(oTarg) != EngineConstants.FALSE)
          {
               RemoveEffectsDueToPlotEvent(oTarg);
          }

          string sDestination = sWP;

          if (IsObjectValid(oTarg) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_LocalJump", "FAILED - <" + GetTag(oTarg) + "> is not a valid object.");
               return;
          }

          // Default case, go to tp_<OBJSTRING>_0
          if (sWP == "") sDestination = "jp_" + GetTag(oTarg) + "_0";

          // sWP is a number, use that instead
          if (StringToInt(sWP) != 0) sDestination = "jp_" + GetTag(oTarg) + "_" + sWP;

          GameObject oDest = UT_GetNearestObjectByTag(oTarg, sDestination);

          if (IsObjectValid(oDest) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_LocalJump", "FAILED - <" + sDestination + "> is not a valid waypoint.");
               return;
          }
          else if (IsObjectValid(GetArea(oDest)) == EngineConstants.FALSE || GetArea(oTarg) != GetArea(oDest))
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_LocalJump", "FAILED - jumping creature: <" + GetTag(oTarg) + "> and target waypoint: <" +
                   GetTag(oDest) + "> are not in the same area!");
               return;
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_LocalJump", "UT_LocalJump: Object " + GetTag(oTarg) + " is moving to " + sDestination);
          }

          // Added so that this is the new "Home" for the creature
          // FAB 9/4
          if (bNewHome != EngineConstants.FALSE)
          {
               Rubber_SetHome(oTarg, oDest);
          }

          // -------------------------------------------------------------------------
          // Georg: //Tracking wants to know about this too.
          // --------------------------------------------------------------------------
          //TrackJumpEvent(oTarg, oDest);

          Vector3 vPos = GetPosition(oDest);
          Vector3 vOrientation = GetOrientation(oDest);

          if (bJumpParty == EngineConstants.FALSE)
          {
               if (nJumpImmediately != EngineConstants.FALSE)
               {
                    SetPosition(oTarg, vPos, EngineConstants.TRUE);
                    SetOrientation(oTarg, vOrientation);
               }
               else
                    WR_AddCommand(oTarg, CommandJumpToObject(oDest), nJumpImmediately, bStaticCommand);
          }
          // Will jump the player's entire party.
          else //if(bJumpParty)
          {
               GameObject oPartyMember;
               List<GameObject> arParty = GetPartyList(GetPartyLeader());

               int nLoop;
               int nPartySize = GetArraySize(arParty);

               for (nLoop = 0; nLoop < nPartySize; nLoop++)
               {
                    oPartyMember = arParty[nLoop];

                    if (IsObjectValid(oPartyMember) != EngineConstants.FALSE)
                    {
                         if (nJumpImmediately != EngineConstants.FALSE)
                              SetPosition(oPartyMember, vPos, EngineConstants.TRUE);
                         else
                              WR_AddCommand(oPartyMember, CommandJumpToObject(oDest), nJumpImmediately, bStaticCommand);
                    }
                    else
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_LocalJump", "FAILED - <" + GetTag(oPartyMember) + "> is not a valid object.");
                    }
               }
          }

     }

     public List<GameObject> UT_GetAllObjectsInAreaByTag(string sTag, int nType = EngineConstants.OBJECT_TYPE_ALL)
     {
          GameObject oPC = GetPartyLeader();
          List<GameObject> arCreatures = GetNearestObjectByTag(oPC, sTag, nType, EngineConstants.MAX_CREATURES_IN_AREA);
          return arCreatures;
     }

     /* @brief Clears the entire active party and stores it.
*
*   Clears the entire active party and stores it. The party can then be restored using UT_PartyRestore().
*   The hero character remains in the party.
*
* @sa UT_PartyRestore
* @author Yaron
**/
     public void UT_PartyStore(int nSetNeutral = EngineConstants.FALSE)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_PartyStore", "Storing active party (up to 3 followers can be stores)");

          List<GameObject> arParty = GetPartyList(GetPartyLeader());
          int nSize = GetArraySize(arParty);
          int i;
          SetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_1, null);
          SetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_2, null);
          SetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_3, null);

          GameObject oCurrent;
          for (i = 0; i < nSize; i++)
          {
               oCurrent = arParty[i];
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_PartyStore", "current party member: " + GetTag(oCurrent));

               if (IsFollower(oCurrent) != EngineConstants.FALSE && IsHero(oCurrent) == EngineConstants.FALSE && IsSummoned(oCurrent) == EngineConstants.FALSE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_PartyStore", "STORING CURRENT PARTY MEMBER");
                    if (GetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_1) == null)
                    {
                         SetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_1, oCurrent);
                         WR_SetFollowerState(oCurrent, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
                         if (nSetNeutral != EngineConstants.FALSE)
                              SetGroupId(oCurrent, EngineConstants.GROUP_NEUTRAL);
                    }
                    else if (GetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_2) == null)
                    {
                         SetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_2, oCurrent);
                         WR_SetFollowerState(oCurrent, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
                         if (nSetNeutral != EngineConstants.FALSE)
                              SetGroupId(oCurrent, EngineConstants.GROUP_NEUTRAL);

                    }
                    else if (GetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_3) == null)
                    {
                         SetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_3, oCurrent);
                         WR_SetFollowerState(oCurrent, EngineConstants.FOLLOWER_STATE_UNAVAILABLE);
                         if (nSetNeutral != EngineConstants.FALSE)
                              SetGroupId(oCurrent, EngineConstants.GROUP_NEUTRAL);

                    }

               }

          }
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_PartyStore", "END");
     }

     /* @brief Restores a party that was cleared using UT_PartyStore
*
*   Restores a party that was cleared using UT_PartyStore.
*
* @sa UT_PartyStore
* @author Yaron
**/
     public void UT_PartyRestore()
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h.UT_PartyStore", "Restoring active party (any existing party members will be set invalid)");

          List<GameObject> arParty = GetPartyList(GetHero());
          int nSize = GetArraySize(arParty);
          int i;

          GameObject oCurrent;
          for (i = 0; i < nSize; i++)
          {
               oCurrent = arParty[i];
               if (IsHero(oCurrent) == EngineConstants.FALSE)
                    WR_SetFollowerState(oCurrent, EngineConstants.FOLLOWER_STATE_INVALID);
          }

          GameObject oFollower1 = GetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_1);
          GameObject oFollower2 = GetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_2);
          GameObject oFollower3 = GetLocalObject(GetModule(), EngineConstants.PARTY_STORE_SLOT_3);

          if (IsObjectValid(oFollower1) != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_PartyRestore", "Restoring: " + GetTag(oFollower1));
               WR_SetObjectActive(oFollower1, EngineConstants.TRUE);
               WR_SetFollowerState(oFollower1, EngineConstants.FOLLOWER_STATE_ACTIVE);
               SetGroupId(oFollower1, EngineConstants.GROUP_PC);
          }

          if (IsObjectValid(oFollower2) != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_PartyRestore", "Restoring: " + GetTag(oFollower2));
               WR_SetObjectActive(oFollower2, EngineConstants.TRUE);
               WR_SetFollowerState(oFollower2, EngineConstants.FOLLOWER_STATE_ACTIVE);
               SetGroupId(oFollower2, EngineConstants.GROUP_PC);
          }

          if (IsObjectValid(oFollower3) != EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_PartyRestore", "Restoring: " + GetTag(oFollower3));
               WR_SetObjectActive(oFollower3, EngineConstants.TRUE);
               WR_SetFollowerState(oFollower3, EngineConstants.FOLLOWER_STATE_ACTIVE);
               SetGroupId(oFollower3, EngineConstants.GROUP_PC);
          }

     }

     /* @brief Destroys all objects with tag sTag
*
*   This function destroys all objects with tag sTag that are in the same area
*   as the player.
*
* @param sTag - The tag of the objects that will be destroyed
* @author Ferret
**/
     public void UT_DestroyTag(string sTag)
     {
          GameObject oCreature;
          int i;

          List<GameObject> arTargets = UT_GetAllObjectsInAreaByTag(sTag);

          // Now destroy everything in the array
          int nSize = GetArraySize(arTargets);
          for (i = 0; i < nSize; i++)
          {
               oCreature = arTargets[i];
               DestroyObject(oCreature, 0);
          }

     }

     /* @brief Returns the position of a substring within a string.
*
*   Returns the starting index of the specified instance of the substring sSubstring within the string sString.
*   Returns -1 if the specified instance of the substring does not occur in the string.
*   The search is 0 indexed, so if the substring occurs at the very beginning of the string this function will return 0.
*   'UT_FindSubString("monkey_laser_fun", "_", 2);' would return 12.
*
* @param sString - The string to search.
* @param sSubString - The substring to search for.
* @param nInstance - The instance of the substring to return (default is the first instance).
* @returns Returns the position of the substring, returns -1 on error.
* @author Craig
**/
     public int UT_FindSubString(string sString, string sSubString, int nInstance = 1)
     {
          int nIndex = 1;
          int nAbsoluteIndex = 0;
          int nLength;
          while (nInstance >= 1 && nIndex > 0)
          {
               nIndex = FindSubString(sString, sSubString);
               nLength = GetStringLength(sString) - nIndex;
               sString = SubString(sString, nIndex + 1, nLength);

               if (nInstance != 1 && nIndex != -1)
               {
                    nIndex++;
               }
               nAbsoluteIndex += nIndex;
               nInstance--;
          }
          return nAbsoluteIndex;
     }

     /* @brief This function quickly moves a creature to a location.
*
*   Moves a creature to the Vector3 of the specified waypoint, or along a path if a number is given and bFollowPath is EngineConstants.TRUE.
*
*
*
* @param sTag - The tag of the creature that will be moved
* @param sWP - The string of the waypoint it will  move to. By default it is "mp_[sTag]_0". If a number is passed then it moves the GameObject to "_#".
* @param bRun - Indicates whether the creature will walk or run. By default it's walk.
* @param bFollowPath - Uses CommandMoveToMultiLocations. If a numbered waypoint is specified, the creature will move to each waypoint in turn until the specified waypoint is reached.
* @param bNewHome - By default a creature's new "Home" is updated when you tell them to move someplace. Set this to EngineConstants.FALSE if you want to keep their home point as is.
* @param bStaticCommand - (for plot critical movements) If this is EngineConstants.TRUE the movement commands will be static (not cleared by a standard ClearAllCommands)
* @author Ferret, Craig
**/
     public void UT_QuickMove(string sTag, string sWP = "0", int bRun = EngineConstants.FALSE, int bFollowPath = EngineConstants.FALSE, int bNewHome = EngineConstants.TRUE, int bStaticCommand = EngineConstants.FALSE)
     {
          GameObject oPC = GetPartyLeader();
          GameObject oMover = UT_GetNearestCreatureByTag(oPC, sTag);
          UT_QuickMoveObject(oMover, sWP, bRun, bFollowPath, bNewHome, bStaticCommand);
     }

     /* @brief This function quickly moves a creature to a location.
*
*   Moves a creature to the Vector3 of the specified waypoint, or along a path if a number is given and bFollowPath is EngineConstants.TRUE.
*
*
*
* @param oMover - The creature that will be moved
* @param sWP - The string of the waypoint it will  move to. By default it is "mp_[sTag]_0". If a number is passed then it moves the GameObject to "_#".
* @param bRun - Indicates whether the creature will walk or run. By default it's walk.
* @param bFollowPath - Uses CommandMoveToMultiLocations. If a numbered waypoint is specified, the creature will move to each waypoint in turn until the specified waypoint is reached.
* @param bNewHome - By default a creature's new "Home" is updated when you tell them to move someplace. Set this to EngineConstants.FALSE if you want to keep their home point as is.
* @param bStaticCommand - (for plot critical movements) If this is EngineConstants.TRUE the movement commands will be static (not cleared by a standard ClearAllCommands)
* @author Ferret, Craig
**/
     public void UT_QuickMoveObject(GameObject oMover, string sWP = "0", int bRun = EngineConstants.FALSE, int bFollowPath = EngineConstants.FALSE, int bNewHome = EngineConstants.TRUE, int bStaticCommand = EngineConstants.FALSE)
     {
          string sDestination = sWP;

          string sTag = GetTag(oMover);
          GameObject oTarget;
          xCommand cMoveToTarget;
          int nDestinationWP = StringToInt(sWP);
          GameObject oDestination;
          List<Vector3> arLocs = new List<Vector3>();
          int i;

          if (IsObjectValid(oMover) == EngineConstants.FALSE)
          {
               LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, "QuickMove failed - " + GetTag(oMover) + " is not a valid object.");
               return;
          }

          if (bFollowPath != EngineConstants.FALSE)
          {
               // if a numbered waypoint is specified, move to each waypoint in turn
               // until the last waypoint is reached
               if (nDestinationWP != 0)
               {
                    // Assemble an array of objects to move to
                    sDestination = "mp_" + GetTag(oMover) + "_1";
                    oDestination = UT_GetNearestObjectByTag(oMover, sDestination);

                    i = 0;
                    while (IsObjectValid(oDestination) != EngineConstants.FALSE && (i < nDestinationWP))
                    {
                         arLocs[i] = GetLocation(oDestination);
                         LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, "QuickMove Vector3 added - " + sDestination, oDestination);
                         i++;
                         sDestination = "mp_" + sTag + "_" + IntToString(i + 1);
                         oDestination = UT_GetNearestObjectByTag(oMover, sDestination);
                    }
               }
               int nSize = GetArraySize(arLocs);

               LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, "QuickMove size of queue: " + IntToString(nSize));
               if (nSize <= 0)
               {
                    LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, "QuickMove failed - " + GetTag(oMover) + " no valid list of locations to move to.");
                    return;
               }
               oTarget = UT_GetNearestObjectByTag(oMover, "mp_" + sTag + "_" + sWP);
               cMoveToTarget = CommandMoveToMultiLocations(arLocs, bRun);

          }
          else
          {
               // Default case, go to mp_<OBJSTRING>_0
               if (sWP == "" || sWP == "0") sDestination = "mp_" + sTag + "_0";
               // sWP is a number, use that instead
               if (nDestinationWP != 0) sDestination = "mp_" + sTag + "_" + sWP;

               oTarget = UT_GetNearestObjectByTag(oMover, sDestination);

               if (IsObjectValid(oTarget) == EngineConstants.FALSE)
               {
                    LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, "QuickMove failed - " + sDestination + " is not a valid waypoint.");
                    return;
               }
               cMoveToTarget = CommandMoveToObject(oTarget, bRun);
          }

          // Added so that this is the new "Home" for the creature
          // FAB 9/4
          if (bNewHome != EngineConstants.FALSE) Rubber_SetHome(oMover, oTarget);

          WR_AddCommand(oMover, cMoveToTarget, EngineConstants.FALSE, bStaticCommand);

          //Face the same way as the last waypoint
          xCommand cTurn = CommandTurn(GetFacing(oTarget));
          AddCommand(oMover, cTurn, EngineConstants.FALSE, bStaticCommand, EngineConstants.COMMAND_ADDBEHAVIOR_DONTCLEAR);

          LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Object " + sTag + " is moving to " + sDestination);
     }

     /* @brief Gets two creature to start combat.
*
* This function starts combat by turning 2 creatures hostile towards each other.
* It is assumed that the perception system will trigger combat once both sides are hostile.
* This function will switch the creature's group to be the 'hostile' group if it's current group is 'non-hostile'
* No other groups will be switched � for these cases the function will just set the 2 groups hostile.
*
* @param oAttacker - The attacking creature or the creature who initiates combat. This will not matter most of the time
* unless we are using the bTargetSelectionOverride parameter.
* @param oTarget - The target creature or the creature who is being attacked. This will not matter most of the time
* unless we are using the bTargetSelectionOverride parameter.
* @param bTargetSelectionOverride - if EngineConstants.TRUE this will override the default target selection for the attacker for the first few rounds
* @param nOverridePermanent - if EngineConstants.TRUE the attacker will not leave the specified target until it is dead.
* and will force the attacker to target the specified target. Otherwise they will just turn hostile and the AI
* system will decide who attacks who.
* @author Yaron
**/
     public void UT_CombatStart(GameObject oAttacker, GameObject oTarget, int bTargetSelectionOverride = EngineConstants.FALSE, int nOverridePermanent = EngineConstants.FALSE)
     {

          int nAttackGroup = GetGroupId(oAttacker);
          int nTargetGroup = GetGroupId(oTarget);
          int bHostilityChanged = EngineConstants.FALSE;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStart", "oAttacker: " + GetTag(oAttacker) + ", oTarget: " + GetTag(oTarget));
#endif

          // Handle special case:
          // If both objects have the same group, there is nothing that can be done.
          if (nAttackGroup == nTargetGroup)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStart",
                   "Creatures have the same group: [" +
                   IntToString(nAttackGroup) + "] [" + IntToString(nTargetGroup) + "]",
                   gameObject, EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
               return;
          }

          // If creature was lying on the groun then need to remove fake-death effect
          if (GetLocalInt(oAttacker, EngineConstants.CREATURE_SPAWN_DEAD) == 2)
          {
               SetLocalInt(oAttacker, EngineConstants.CREATURE_SPAWN_DEAD, 0);
               RemoveEffectsByParameters(oAttacker, EngineConstants.EFFECT_TYPE_SIMULATE_DEATH);
          }

          // [PC] attacking [Neutral/Friendly]
          if (((nAttackGroup == EngineConstants.GROUP_PC) && (nTargetGroup == EngineConstants.GROUP_NEUTRAL || nTargetGroup == EngineConstants.GROUP_FRIENDLY)))
          {
               SetGroupId(oTarget, EngineConstants.GROUP_HOSTILE);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStart",
               "Changing the following creature's group to be HOSTILE: " + GetTag(oTarget));
#endif
               bHostilityChanged = EngineConstants.TRUE;
          }
          // [Neutral/Friendly] attacking [PC]
          else if (((nTargetGroup == EngineConstants.GROUP_PC) && (nAttackGroup == EngineConstants.GROUP_NEUTRAL || nAttackGroup == EngineConstants.GROUP_FRIENDLY)))
          {
               SetGroupId(oAttacker, EngineConstants.GROUP_HOSTILE);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStart",
               "Changing the following creature's group to be HOSTILE: " + GetTag(oAttacker));
#endif
               bHostilityChanged = EngineConstants.TRUE;
          }

          // [The Rest]
          else if (!(nTargetGroup == EngineConstants.GROUP_PC || nTargetGroup == EngineConstants.GROUP_NEUTRAL || nTargetGroup == EngineConstants.GROUP_FRIENDLY || nTargetGroup == EngineConstants.GROUP_HOSTILE) ||
                    !(nAttackGroup == EngineConstants.GROUP_PC || nAttackGroup == EngineConstants.GROUP_NEUTRAL || nAttackGroup == EngineConstants.GROUP_FRIENDLY || nAttackGroup == EngineConstants.GROUP_HOSTILE))
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStart",
                   "Setting the following groups to be hostile towards each other: [" +
                   IntToString(nAttackGroup) + "] [" + IntToString(nTargetGroup) + "]");
#endif
               SetGroupHostility(nAttackGroup, nTargetGroup, EngineConstants.TRUE);
               bHostilityChanged = EngineConstants.TRUE;
          }

          // Check for overriding the Attackers Target.
          if (bTargetSelectionOverride != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStart",
                   "Forcing attacker to attack target");
#endif
               SetLocalObject(oAttacker, EngineConstants.AI_TARGET_OVERRIDE, oTarget);
               if (nOverridePermanent != EngineConstants.FALSE)
                    SetLocalInt(oAttacker, EngineConstants.AI_TARGET_OVERRIDE_DUR_COUNT, -1); // -1 flags permanent override
               else
                    SetLocalInt(oAttacker, EngineConstants.AI_TARGET_OVERRIDE_DUR_COUNT, 0);
          }

          // Hostility Change Failed
          if (bHostilityChanged == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStart",
                   "Could not turn creatures hostile towards each other: [" +
                   IntToString(nAttackGroup) + "] [" + IntToString(nTargetGroup) + "]",
                   gameObject, EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
          }

     }

     /* @brief Stops combat between 2 creatures.
*
* This function stops combat by turning 2 creatures non-hostile towards each other.
* The user is responsible for triggering it between any hostile creature towards the player
* or towards other creature. For example: this function will need to be called once for every
* creature that is attacking the player in order to stop combat.
* This function will switch the creature's group to be the 'non hostile group if it's current group is 'hostile'.
* No other groups will be switched � for these cases the function will just set the 2 groups non-hostile.
*
* @param oAttacker - The attacking creature or the creature who initiates combat. This will not matter most of the time
* unless we are using the bTargetSelectionOverride parameter.
* @param oTarget - The target creature or the creature who is being attacked. This will not matter most of the time
* unless we are using the bTargetSelectionOverride parameter.
* @param bTargetSelectionOverride - if EngineConstants.TRUE this will override the default target selection for the attacker
* and will force the attacker to target the specified target. Otherwise they will just turn hostile and the AI
* system will decide who attacks who.
* @author Yaron
**/
     public void UT_CombatStop(GameObject oCreatureA, GameObject oCreatureB)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStop", "CreatureA: " + GetTag(oCreatureA) + ", oCreatureB: " + GetTag(oCreatureB));
#endif
          // Handle attacker
          int nCreatureAGroup = GetGroupId(oCreatureA);
          int nCreatureBGroup = GetGroupId(oCreatureB);
          int nHostilityChanged = EngineConstants.FALSE;

          // First, clear everything for both creatures
          WR_ClearAllCommands(oCreatureA);
          WR_ClearAllCommands(oCreatureB);

          if (nCreatureAGroup == EngineConstants.GROUP_HOSTILE) // switch to neutral
          {
               SetGroupId(oCreatureA, EngineConstants.GROUP_NEUTRAL);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStop",
                   "Changing the following creature's group to be NON_HOSTILE: " + GetTag(oCreatureA));
#endif
               nHostilityChanged = EngineConstants.TRUE;
          }
          else if (nCreatureBGroup == EngineConstants.GROUP_HOSTILE)
          {
               SetGroupId(oCreatureB, EngineConstants.GROUP_NEUTRAL);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStop",
                   "Changing the following creature's group to be NON_HOSTILE: " + GetTag(oCreatureB));
#endif
               nHostilityChanged = EngineConstants.TRUE;
          }

          if (nHostilityChanged == EngineConstants.FALSE && (nCreatureAGroup != EngineConstants.GROUP_PC && nCreatureAGroup != EngineConstants.GROUP_HOSTILE &&
              nCreatureAGroup != EngineConstants.GROUP_FRIENDLY && nCreatureAGroup != EngineConstants.GROUP_NEUTRAL) || (nCreatureBGroup != EngineConstants.GROUP_PC &&
              nCreatureBGroup != EngineConstants.GROUP_FRIENDLY && nCreatureBGroup != EngineConstants.GROUP_NEUTRAL && nCreatureBGroup != EngineConstants.GROUP_HOSTILE)) // no change yet -> we are dealing with at least one custom group
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStop",
                   "Setting the following groups to be non-hostile towards each other: [" + IntToString(nCreatureAGroup) + "] ["
                       + IntToString(nCreatureBGroup) + "]");
#endif
               SetGroupHostility(nCreatureAGroup, nCreatureBGroup, EngineConstants.FALSE);
               nHostilityChanged = EngineConstants.TRUE;
          }

          if (nHostilityChanged == EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "UT_CombatStop",
                   "Could not turn creatures hostile towards each other", gameObject, EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
          }
     }

     /* @brief Increments a local integer.
*
* By default this function increments a specified local integer by 1.
*
* @param oObject - Object to store the var on.
* @param sVarName - The name of the integer variable to store.
* @param nIncrement - The amount the integer will be changed by. Default 1.
* @author Ferret
**/
     public void UT_IncLocalInt(GameObject oObject, string sVarName, int nIncrement = 1)
     {
          int nOldValue = GetLocalInt(oObject, sVarName);
          int nNewValue = nOldValue + nIncrement;

          SetLocalInt(oObject, sVarName, nNewValue);

     }

     /* @brief Jumps the PC to a waypoint, checking area first
*
* This function can only be used for the PC because of the area transition.
*
* @param sArea - The string of the area to be checked before jump/transition; also used in transition
* @param sWaypoint - The string of the waypoint to be jumped/transitioned to.
* @param sWorldMapLoc1 - world map Vector3 to set active
* @param sWorldMapLoc2 - world map Vector3 to set active
* @param sWorldMapLoc3 - world map Vector3 to set active
* Logging is covered by UT_LocalJump & UT_DoAreaTransition
* @author Cori
**/
     public void UT_PCJumpOrAreaTransition(string sArea, string sWaypoint, string sWorldMapLoc1 = "", string sWorldMapLoc2 = "", string sWorldMapLoc3 = "", string sWorldMapLoc4 = "", string sWorldMapLoc5 = "")
     {
          GameObject oPC = GetPartyLeader();
          GameObject oArea = GetArea(oPC);
          string sAreaTag = GetTag(oArea);

          if (GetGameMode() == EngineConstants.GM_COMBAT)
          {
               UI_DisplayMessage(GetMainControlled(), EngineConstants.UI_MESSAGE_CANT_DO_IN_COMBAT);
          }
          else if (sAreaTag == sArea)
          {
            UT_LocalJump(oPC, sWaypoint);
            //In the new design everything is basically local jump
            //UT_DoAreaTransition(sArea, sWaypoint, sWorldMapLoc1, sWorldMapLoc2, sWorldMapLoc3, sWorldMapLoc4, sWorldMapLoc5);
        }
          else
          {
               UT_DoAreaTransition(sArea, sWaypoint, sWorldMapLoc1, sWorldMapLoc2, sWorldMapLoc3, sWorldMapLoc4, sWorldMapLoc5);
          }
     }

     /* @brief Returns an array of all team members in the area.
*
* This function searches through all the creatures in the area and returns
* any creature that has it's team set to nTeamID
*
* @param nTeamID - This is the team ID number (stored in EngineConstants.CREATURE_TEAM_ID)
* @param nMembersType - The type of members to retrieve (EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.OBJECT_TYPE_PLACEABLE)
* @author joshua
**/
     public List<GameObject> UT_GetTeam(int nTeamID, int nMembersType = EngineConstants.OBJECT_TYPE_CREATURE)
     {
          List<GameObject> arNewList = new List<GameObject>();

          if (nTeamID <= 0)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_GetTeam", "Invalid Team ID");
               return arNewList;
          }

          arNewList = GetTeam(nTeamID, nMembersType);

          return arNewList;

     }

     /* @brief Makes a team appear (they are activated).
*
* This sets SetObjectActive to EngineConstants.TRUE for every member of the specified team.
* 0 is not a valid parameter (because that's the default value of the variable).
*
* @param nTeamID - This is the team ID number (stored in EngineConstants.CREATURE_TEAM_ID)
* @param bAppears - EngineConstants.TRUE or EngineConstants.FALSE
* @param nMembersType - The type of members to retrieve (EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.OBJECT_TYPE_PLACEABLE)
* @author Ferret
**/
     public void UT_TeamAppears(int nTeamID, int bAppears = EngineConstants.TRUE, int nMembersType = EngineConstants.OBJECT_TYPE_CREATURE)
     {

          int nIndex;
          GameObject oPC = GetPartyLeader();
          List<GameObject> arTeam = UT_GetTeam(nTeamID, nMembersType);

          for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
               WR_SetObjectActive(arTeam[nIndex], bAppears);

#if DEBUG
          if (nIndex == EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamAppears",
                   "No team members found for TeamID #" + ToString(nTeamID));

          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamAppears",
                   "Team ID #" + ToString(nTeamID) + " has been set to ACTIVE. " +
                   ToString(nIndex) + " objects have been affected");
#endif

     }

     /* @brief Makes a team go hostile.
*
* The specified team joins the hostile faction.
* 0 is not a valid parameter (because that's the default value of the variable).
*
* @param nTeamID - This is the team ID number (stored in EngineConstants.CREATURE_TEAM_ID)
* @param bHostile - Default behavior is to turn the team to EngineConstants.GROUP_HOSTILE. If this is set to EngineConstants.FALSE, then instead the team will turn to EngineConstants.GROUP_NEUTRAL.
* @author Ferret
**/
     public void UT_TeamGoesHostile(int nTeamID, int bHostile = EngineConstants.TRUE)
     {
          int nIndex;
          GameObject oPC = GetPartyLeader();
          List<GameObject> arTeam = UT_GetTeam(nTeamID);

          if (bHostile != EngineConstants.FALSE)     // The team will go to EngineConstants.GROUP_HOSTILE
          {
               for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
                    UT_CombatStart(arTeam[nIndex], oPC);
          }
          else                // The team will go to EngineConstants.GROUP_NEUTRAL
          {
               for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
                    SetGroupId(arTeam[nIndex], EngineConstants.GROUP_NEUTRAL);
          }

#if DEBUG
          if (nIndex == EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamGoesHostile",
                   "No team members found for Team ID #" + ToString(nTeamID));

          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamGoesHostile",
                   "Team ID #" + ToString(nTeamID) + " has gone HOSTILE. " +
                   ToString(nIndex) + " objects have been affected");
#endif

     }

     /* @brief Makes a team go to the nearest exit (wp_gen_exit).
*
* 0 is not a valid parameter (because that's the default value of the variable).
*
* @param nTeamID - This is the team ID number (stored in EngineConstants.CREATURE_TEAM_ID)
* @param nRun - Set to EngineConstants.TRUE if you want the team to run there
* @param sTagOverride - Instead of "wp_gen_exit" they go to this destination.
* @author Ferret
**/
     public void UT_TeamExit(int nTeamID, int nRun = EngineConstants.FALSE, string sTagOverride = EngineConstants.GENERIC_EXIT)
     {

          int nIndex;
          GameObject oPC = GetPartyLeader();
          List<GameObject> arTeam = UT_GetTeam(nTeamID);

          for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
               UT_ExitDestroy(arTeam[nIndex], nRun, sTagOverride, EngineConstants.TRUE);

#if DEBUG
          if (nIndex == EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamExit",
                   "No team members found for Team ID #" + ToString(nTeamID));

          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamExit",
                   "Team ID #" + ToString(nTeamID) + " is heading to the EXIT. " +
                   ToString(nIndex) + " objects have been affected");
#endif

     }

     /* @brief Makes a team jump to an object.
**
* @param nTeamID This is the team ID number
* @param sTagDestination The tag of the GameObject the team is going to jump to.
* @param bJumpImmediately Set to EngineConstants.TRUE if you want this added to the front of the xCommand queue
* @param bStaticCommand - the xCommand will not be cleared by a standard WR_ClearAllCommands
* @author Craig
**/
     public void UT_TeamJump(int nTeamID, string sTagDestination = "", int bJumpImmediately = EngineConstants.FALSE, int bStaticCommand = EngineConstants.FALSE, int bNewHome = EngineConstants.FALSE)
     {
          int nIndex = 0;
          GameObject oPC = GetPartyLeader();
          List<GameObject> arTeam = UT_GetTeam(nTeamID);
          GameObject oDestination = UT_GetNearestObjectByTag(oPC, sTagDestination);

          int nQuickJump = 0;

          // This checks if an integer was passed as the waypoint.
          // If so instead of everyone jumping to the same location, instead everyone moves to
          // their own spot using the same logic as the UT_LocalJump code.
          if (sTagDestination == "0" || StringToInt(sTagDestination) != 0) nQuickJump = EngineConstants.TRUE;

          if (nQuickJump != EngineConstants.FALSE)
          {
               for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, GetTag(arTeam[nIndex]));

                    oDestination = UT_GetNearestObjectByTag(oPC, "jp_" + GetTag(arTeam[nIndex]) + "_" + sTagDestination);

                    if (IsObjectValid(oDestination) != EngineConstants.FALSE)
                    {
                         WR_AddCommand(arTeam[nIndex], CommandJumpToObject(oDestination), bJumpImmediately, bStaticCommand);
                    }
                    else
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamJump",
                             "Function passed an invalid destination (" + sTagDestination + ") for team member " +
                                 GetTag(arTeam[nIndex]) + ".");
                    }
                    if (bNewHome != EngineConstants.FALSE)
                    {
                         Rubber_SetHome(arTeam[nIndex], oDestination);
                    }
               }
          }
          else if (IsObjectValid(oDestination) != EngineConstants.FALSE)
          {
               for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, GetTag(arTeam[nIndex]));
                    WR_AddCommand(arTeam[nIndex], CommandJumpToObject(oDestination), bJumpImmediately, bStaticCommand);
                    if (bNewHome != EngineConstants.FALSE)
                    {
                         Rubber_SetHome(arTeam[nIndex], oDestination);
                    }
               }
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamJump",
                   "Function passed an invalid destination (" + sTagDestination + ").");
          }

          if (nIndex == EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamJump",
                   "No team members found for Team ID #" + ToString(nTeamID));

          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamJump",
                   "Team ID #" + ToString(nTeamID) + " is heading to its desintation (" + sTagDestination + "). " +
                   ToString(nIndex) + " objects have been affected.");
     }

     /* @brief Makes a team go to an object.
*
* 0 is not a valid parameter (because that's the default value of the variable).
*
* @param nTeamID This is the team ID number
* @param sTagDestination The tag of the GameObject the team is going to move to.
* @param bRun Set to EngineConstants.TRUE if you want the team to run there
* @param fRange is the distance from the target the team will stop at
* @param bNewHome Set to EngineConstants.TRUE if you want the team to set their home Vector3 to the destination.
* @author Ferret
**/
     public void UT_TeamMove(int nTeamID, string sTagDestination = "", int bRun = EngineConstants.FALSE, float fRange = 0.0f, int bNewHome = EngineConstants.FALSE)
     {
          int nIndex = 0;
          GameObject oPC = GetPartyLeader();
          List<GameObject> arTeam = UT_GetTeam(nTeamID);
          GameObject oDestination = UT_GetNearestObjectByTag(oPC, sTagDestination);

          if (IsObjectValid(oDestination) != EngineConstants.FALSE)
          {
               for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, GetTag(arTeam[nIndex]));
                    WR_AddCommand(arTeam[nIndex], CommandMoveToObject(oDestination, bRun, fRange), EngineConstants.TRUE);
                    if (bNewHome != EngineConstants.FALSE)
                    {
                         Rubber_SetHome(arTeam[nIndex], oDestination);
                    }
               }
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamMove",
                   "Function passed an invalid destination (" + sTagDestination + ").");
          }

          if (nIndex == EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamMove",
                   "No team members found for Team ID #" + ToString(nTeamID));

          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_TeamMove",
                   "Team ID #" + ToString(nTeamID) + " is heading to its desintation (" + sTagDestination + "). " +
                   ToString(nIndex) + " objects have been affected.");
     }

     /* @brief Kills all the members of a team.
*
* @param nTeam      - The team number to kill.
* @param oKiller    - The killer (null for creatures killed by plot).
* @author Craig
**/
     public void UT_KillTeam(int nTeam, GameObject oKiller = null)
     {
          List<GameObject> arTeam = UT_GetTeam(nTeam);
          int nIndex;
          int nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               KillCreature(arTeam[nIndex], oKiller);
          }
     }

     /* @brief All the members of nTeam join nNewTeam.
*
* @param nTeam - The team merging into nNewTeam.
* @param nNewTeam - The team being joined.
* @param nMembersType - The type of members to retrieve (EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.OBJECT_TYPE_PLACEABLE)
* @author Craig
**/
     public void UT_TeamMerge(int nTeam, int nNewTeam, int nMembersType = EngineConstants.OBJECT_TYPE_CREATURE)
     {
          List<GameObject> arTeam = UT_GetTeam(nTeam, nMembersType);
          int nIndex;
          int nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               SetTeamId(arTeam[nIndex], nNewTeam);
          }
     }

     /* @brief Sets a team to be stationary.
*
*
* @param nTeam - The team number to set stationary.
* @param nStationaryStatus - The stationary status of the team.  EngineConstants.AI_STATIONARY_STATE_DISABLED EngineConstants.AI_STATIONARY_STATE_SOFT or EngineConstants.AI_STATIONARY_STATE_HARD
**/
     public void UT_SetTeamStationary(int nTeam, int nStationaryStatus)
     {
          List<GameObject> arTeam = UT_GetTeam(nTeam);
          int nIndex;
          int nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               SetLocalInt(arTeam[nIndex], EngineConstants.AI_FLAG_STATIONARY, nStationaryStatus);
          }
     }

     /* @brief Sets all the members of a team interractive or not.
*
* @param nTeam - The team number to set interactive.
* @param bInteractive - EngineConstants.TRUE or EngineConstants.FALSE.
* @param nMembersType - The type of members to retrieve (EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.OBJECT_TYPE_PLACEABLE)
* @author Craig
**/
     public void UT_SetTeamInteractive(int nTeam, int bInteractive, int nMembersType = EngineConstants.OBJECT_TYPE_CREATURE)
     {
          List<GameObject> arTeam = UT_GetTeam(nTeam, nMembersType);
          int nIndex;
          int nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               SetObjectInteractive(arTeam[nIndex], bInteractive);
          }
     }

     /* @brief Returns EngineConstants.TRUE if the follower is in the active party EngineConstants.FALSE otherwise
*
* Returns EngineConstants.TRUE if the follower is in the active party EngineConstants.FALSE otherwise
* This should be used only for non-plot followers. For plot followers (Alistair, Sten etc') -
* use the plot flags in the global party plot.
*
* @param oFollower - the follower being checked
* @returns EngineConstants.TRUE if the follower is in the active party EngineConstants.FALSE otherwise
* @sa FireFollower, HireFollower
* @author Yaron
*/
     public int UT_IsFollowerInParty(GameObject oFollower)
     {
          return (GetFollowerState(oFollower) == EngineConstants.FOLLOWER_STATE_ACTIVE) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /* @brief Removes an item from the Player's active inventory
*
* This function removes the given item from inventory by it's string.
* If nNumToRemove is specified, it will remove that many items from inventory,
* or as many items as it can until no more exist in inventory. This function
* returns the GameObject of the first item stack it creates.
*
* @param rItem - string of item to remove from inventory
* @param nNumToAdd - Amount of this item to remove from inventory
* @param oInvOwner - Override for applying function to GameObject other than PC
* @returns the GameObject of the first item stack created
* @author joshua
**/
     public void UT_RemoveItemFromInventory(string rItem, int nNumToRemove = 1, GameObject oInvOwner = null, string sTag = "")
     {

          if (oInvOwner == null)
               oInvOwner = GetPartyLeader();

          if (sTag == "")
               sTag = ResourceToTag(rItem);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_RemoveItemFromInventory",
                          "Total to Remove: [" + sTag + " x " + ToString(nNumToRemove) + "]", oInvOwner);

          RemoveItemsByTag(oInvOwner, sTag, nNumToRemove);

          /*
          int     nIndex;
          int     nNumItems;
          int     nNumLeftToRemove = nNumToRemove;
          int     nCurrentStackSize;
          GameObject  oItem;

          // We are standardizing the item's tag to be the same as it's string name,
          // minus the extension. We do this to insure that they match, because
          // they should. If they don't match, it is a bug. This squashes such an
          // occurance.
          string  sItemTag = (sTag != "") ? sTag :  ResourceToTag(rItem);
          List<GameObject> arItems = GetItemsInInventory(oInvOwner, EngineConstants.TRUE, 0, sItemTag);
          nNumItems = GetArraySize(arItems);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_RemoveItemFromInventory",
                          "Total to Remove: [" + sItemTag + " x " + ToString(nNumToRemove) + "]", oInvOwner);

          //--------------------------------------------------------------------------
          // ***TEMPORARY***
          // Simple Selection Sort: smallest stack size first
          int nIndex2, nSmallest = 0;
          for (nIndex = 0; nIndex < nNumItems; nIndex++)
          {
              nSmallest = nIndex;
              for (nIndex2 = nIndex; nIndex2 < nNumItems; nIndex2++)
              {
                  if (GetItemStackSize(arItems[nSmallest]) > GetItemStackSize(arItems[nIndex2]))
                      nSmallest = nIndex2;
              }
              oItem = arItems[nIndex];
              arItems[nIndex] = arItems[nSmallest];
              arItems[nSmallest] = oItem;
          }
          //--------------------------------------------------------------------------

          // Loop through all the item stacks to delete the correct amount of items.
          for (nIndex = 0; nIndex < nNumItems; nIndex++)
          {

              oItem = arItems[nIndex];

              if (IsObjectValid(oItem) != EngineConstants.FALSE)
              {

                  nCurrentStackSize = GetItemStackSize(oItem);

                  // Case 1:  There are enough items left in this stack to finish
                  //          the removing procedure. Simply remove the remaining
                  //          items out of the stack.
                  if (nCurrentStackSize > nNumLeftToRemove)
                  {

                      SetItemStackSize(oItem, (nCurrentStackSize-nNumLeftToRemove));

                      Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_RemoveItemFromInventory",
                          "Removed: [" + sItemTag + " x " + ToString(nCurrentStackSize) + "] --> " +
                          "[" + sItemTag + " x " + ToString((nCurrentStackSize-nNumLeftToRemove)) + "] " +
                          "(-"  + ToString(nNumLeftToRemove) + ")", oInvOwner);

                      nNumLeftToRemove = 0;

                      break;

                  }

                  // Case 2:  We need to remove more items then are available in this
                  //          stack. We will just destroy this stack and go on to the
                  //          next one if it exists.
                  else
                  {

                      Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_RemoveItemFromInventory",
                          "Removed: [" + sItemTag + " x " + ToString((nCurrentStackSize)) + "]", oInvOwner);

                      nNumLeftToRemove = nNumLeftToRemove - nCurrentStackSize;

                      WR_DestroyObject(oItem);

                  }
              }
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_RemoveItemFromInventory",
              "Total Removed: [" + sItemTag + " x " + ToString((nNumToRemove-nNumLeftToRemove)) + "]", oInvOwner);
         */

     }

     /* @brief Counts the number of an item in the Player's active inventory
*
* Counts the amount of a given item in inventory by it's string.
*
* @param rItem - string of item to check for
* @param oInvOwner - Override for applying function to GameObject other than PC
* @returns The quantity of this item that is currently in the player's inventory
* @author joshua
**/
     public int UT_CountItemInInventory(string rItem, GameObject oInvOwner = null, string sTag = "")
     {

          if (oInvOwner == null)
               oInvOwner = GetPartyLeader();

          if (sTag == "")
               sTag = ResourceToTag(rItem);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_CountItemInInventory",
                          "Item to Count: [" + sTag + "]", oInvOwner);

          return CountItemsByTag(oInvOwner, sTag);
     }

     /* @brief Adds an item to the Player's active inventory
*
* This fucntion adds the given item from inventory by it's string.
* If nNumToAdd is specified, it will add that many items to inventory.
*
* @param nTeamID - string of item to add to inventory
* @param nNumToAdd - Amount of this item to add to inventory
* @param oInvOwner - Override for applying function to GameObject other than PC
* @author joshua
**/
     public GameObject UT_AddItemToInventory(string rItem, int nNumToAdd = 1, GameObject oInvOwner = null, string sTag = "", int bSuppressNote = EngineConstants.FALSE, int bDroppable = EngineConstants.TRUE)
     {

          if (oInvOwner == null)
               oInvOwner = GetPartyLeader();

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "utility_h:UT_AddItemToInventory",
                          "Total to Add: [" + ResourceToTag(rItem) + " x " + ToString(nNumToAdd) + "]", oInvOwner);

          return CreateItemOnObject(rItem, oInvOwner, nNumToAdd, sTag, bSuppressNote, bDroppable);
     }

     /*-----------------------------------------------------------------------------
     * @brief Opens a door away from the user and sends the door an EngineConstants.EVENT_TYPE_OPENED event.
     * @param oDoor      The door to open
     * @param oUser      The creature opening the door.
     *-----------------------------------------------------------------------------*/
     public void UT_OpenDoor(GameObject oDoor, GameObject oUser)
     {
          float fAngle = GetAngleBetweenObjects(oDoor, oUser);
          SetPlaceableState(oDoor, ((fAngle > 90.0f && fAngle < 270.0f) ? EngineConstants.PLC_STATE_DOOR_OPEN_2 : EngineConstants.PLC_STATE_DOOR_OPEN));
          SendEventOpened(oDoor, oUser);
     }

     /* Takes care of stuff that should happen after cutscenes.
*
* The generic cutscene script (gen00cs_cutscene_end) must be set on the properties
* of the cutscene itself for this function to be called.
*
* @param None
* @author Jonathan
*/
     public void CS_CutsceneEnd()
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "cutscenes_h.nss, CS_CutsceneEnd",
              "CS_CutsceneEnd() called");

          GameObject oModule = GetModule();

          // Get info from last cutscene call
          string sPlot = GetLocalString(oModule, EngineConstants.CUTSCENE_SET_PLOT);
          int nFlag = GetLocalInt(oModule, EngineConstants.CUTSCENE_SET_PLOT_FLAG);
          string sTalkSpeaker = GetLocalString(oModule, EngineConstants.CUTSCENE_TALK_SPEAKER);

          // Reset local variables
          SetLocalString(oModule, EngineConstants.CUTSCENE_SET_PLOT, "");
          SetLocalInt(oModule, EngineConstants.CUTSCENE_SET_PLOT_FLAG, -1);
          SetLocalString(oModule, EngineConstants.CUTSCENE_TALK_SPEAKER, "");

          // Check if we need to set a plot flag
          if (sPlot != "")
          {
               if (nFlag > -1)
               {
                    // Set the plot flag
                    WR_SetPlotFlag(sPlot, nFlag, EngineConstants.TRUE, EngineConstants.TRUE);
               }
          }

          // Check if there is someone who should begin a conversation
          if (sTalkSpeaker != "")
          {
               GameObject oPC = GetHero();
               GameObject oTalkSpeaker = UT_GetNearestCreatureByTag(oPC, sTalkSpeaker);
               if (IsObjectValid(oTalkSpeaker) != EngineConstants.FALSE)
               {
                    UT_Talk(oTalkSpeaker, oPC);
               }
               else
               {
                    Log_Systems("CS_CutsceneEnd: could not find sTalkSpeaker: " + sTalkSpeaker, EngineConstants.LOG_LEVEL_ERROR);
               }
          }
     }

     /* @brief Loads a cutscene.
*
* Loads the specified cutscene. Sets the specified plot flag after the
* cutscene plays (optional), and makes sTalkSpeaker initiate dialog
* with the player (also optional).
*
* Note: The plot flag and talk speaker parameters require the script
* gen00cs_cutscene_end.nss to be attached to the cutscene itself.
*
* @param rCutscene: Cutscene string (EngineConstants.CUTSCENE_* constants defined in cutscenes_h.nss)
* @param strPlot: Plot which contains the flag to be set
* @param nPlotflag: Plot flag to be set
* @param sTalkSpeaker: Tag of creature who will initiate dialog
*
* @author Jonathan
*/
     public void CS_LoadCutscene(string rCutscene,
                     string strPlot = "",
                     int nPlotFlag = -1,
                     string sTalkSpeaker = "")
     {
          string[] arActors = new List<string>().ToArray();//workaround
          List<GameObject> arReplacements = new List<GameObject>();

          CS_LoadCutsceneWithReplacements(rCutscene,
                                          arActors,
                                          arReplacements,
                                          strPlot,
                                          nPlotFlag,
                                          sTalkSpeaker);
     }

     /* @brief Loads a cutscene with scripted replacement actors.
*
* ** FOR SPECIAL CASE USE ONLY **
* Most cutscenes should use CS_LoadCutscene(), as most actor mappings
* can be handled from the cutscene editor itself.
*
* @param rCutscene: Cutscene string (EngineConstants.CUTSCENE_* constants defined in cutscenes_h.nss)
* @param arActors: (string array) List of cutscene tracks whose actors will be replaced.
* @param arReplacements: (GameObject array) List of objects that will replace the default actors.
* @param strPlot: Plot which contains the flag to be set
* @param nPlotflag: Plot flag to be set
* @param sTalkSpeaker: Tag of creature who will initiate dialog
*
* @author Jonathan
*/
     public void CS_LoadCutsceneWithReplacements(string rCutscene,
                                     string[] arActors,
                                     List<GameObject> arReplacements,
                                     string strPlot = "",
                                     int nPlotFlag = -1,
                                     string sTalkSpeaker = "")
     {
          GameObject oModule = GetModule();

          SetLocalString(oModule, EngineConstants.CUTSCENE_SET_PLOT, strPlot);
          SetLocalInt(oModule, EngineConstants.CUTSCENE_SET_PLOT_FLAG, nPlotFlag);
          SetLocalString(oModule, EngineConstants.CUTSCENE_TALK_SPEAKER, sTalkSpeaker);

          LoadCutscene(rCutscene, null, EngineConstants.TRUE, arActors, arReplacements, EngineConstants.TRUE);
     }

     /* @} */
}