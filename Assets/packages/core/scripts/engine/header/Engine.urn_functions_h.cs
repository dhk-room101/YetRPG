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
     // urn_functions_h
     // Copyright � 2008 Bioware Corp.
     //------------------------------------------------------------------------------
     //
     // Functions used in multiple places in the Urn plot.
     //
     //------------------------------------------------------------------------------
     // May 14, 2008 - Owner: Grant Mackay
     //------------------------------------------------------------------------------

     //#include"urn_constants_h"
     //#include"utility_h"
     //#include"sys_audio_h"

     //#include"plt_urnpt_main"
     //#include"plt_urn100pt_haven"
     //#include"plt_urn230pt_bridge"

     //#include"plt_gen00pt_class_race_gend"

     //------------------------------------------------------------------------------
     // FUNCTION PROTOTYPES
     //------------------------------------------------------------------------------
     //------------------------------------------------------------------------------
     // FUNCITON DEFINITIONS
     //------------------------------------------------------------------------------
     /* @brief Makes all the Ash Wraiths in the Urn plot friendly.
      *
      * Cycles the Ash Wraith team members in the Urn plot assigning them all
      * the the 'Friendly' group.
      *
      * January 8, 2009 -- The wraiths are no longer going to become friendly but
      * instead just die, awarding the player EngineConstants.XP.
      *
      * @author Grant Mackay
      **/
     public void URN_SetWraithsFriendly()
     {

          List<GameObject> arAshWraiths = UT_GetTeam(EngineConstants.URN_TEAM_ASH_WRAITHS);
          GameObject oPC = GetHero();
          GameObject oAshWraith;

          int nSize = GetArraySize(arAshWraiths);
          int i;

          for (i = 0; i < nSize; ++i)
          {

               oAshWraith = arAshWraiths[i];
               //SetGroupId(oAshWraith, EngineConstants.GROUP_FRIENDLY);
               ApplyEffectVisualEffect(oAshWraith, oAshWraith, 1109, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
               WR_SetObjectActive(oAshWraith, EngineConstants.TRUE);
               SetImmortal(oAshWraith, EngineConstants.FALSE);
               KillCreature(oAshWraith, oPC);

          }

     }

     /* @brief Sets up the visions in the Gauntlet area of the Urn plot.
 *
 * Activates a creature appropriate to the player's background and sets up a
 * nearby talk trigger.
 *
 * @param sTag The tag of the creature being set up.
 * @author Grant Mackay
 **/
     public void URN_VisionSetUp(string sTag)
     {

          GameObject oTarg = GetObjectByTag(sTag);

          WR_SetObjectActive(oTarg, EngineConstants.TRUE);

          oTarg = GetObjectByTag(EngineConstants.URN_TR_VISION_TALK);

          SetLocalString(oTarg, EngineConstants.TRIG_TALK_SPEAKER, sTag);

     }

     //------------------------------------------------------------------------------
     // Bridge Puzzle Functions
     /* @brief Implements the bridge puzzle state.
      *
      * Checks the state of the bridge puzzle, in the gauntlet area, and activates
      * or de-activates bridge pieces as required.
      *
      * @param oArea The area the puzzle is taking place in.
      * @author Grant Mackay
      **/
     public void URN_ActivateBridge(GameObject oArea)
     {

          // Determine if the first section should be active: 3,8 or 3,9
          URN_ActivateSection(3, -1, -1, 8, 9, -1, 1, oArea);

          // Determine if the second section should be active: 6,8; 6,10 or 6,12
          URN_ActivateSection(6, -1, -1, 8, 10, 12, 2, oArea);

          // Determine if the third section should be active: 1,10; 1,11 or 1,7
          URN_ActivateSection(1, -1, -1, 10, 11, 7, 3, oArea);

          // Determine if the fourth section should be active: 4,11; 2,11 or 5,11
          URN_ActivateSection(4, 2, 5, 11, -1, -1, 4, oArea);

     }

     /* @brief Activates a section of the bridge.
 *
 * Determines if a bridge section should be activated based on parameters and,
 * if so, activates it. Used by URN_ActivateBridge.
 *
 * @param nSideA1 The first switch number that must be active in order for the section to activate
 * @param nSideA2 An alternate first switch number that must be active in order for the section to activate
 * @param nSideA3 An alternate first switch number that must be active in order for the section to activate
 * @param nSideB1 The second switch number that must be active in order for the section to activate
 * @param nSideB2 An alternate second switch number that must be active in order for the section to activate
 * @param nSideB3 An alternate second switch number that must be active in order for the section to activate
 * @author Grant Mackay
 **/
     public void URN_ActivateSection(int nSideA1, int nSideA2, int nSideA3, int nSideB1, int nSideB2, int nSideB3, int nSection, GameObject oArea)
     {

          // Check to see if the section in question should be active.
          int bFirst = (URN_CheckBridgeCounters(nSideA1, oArea) != EngineConstants.FALSE
               || URN_CheckBridgeCounters(nSideA2, oArea) != EngineConstants.FALSE
               || URN_CheckBridgeCounters(nSideA3, oArea) != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
          int bSecond = (URN_CheckBridgeCounters(nSideB1, oArea) != EngineConstants.FALSE
               || URN_CheckBridgeCounters(nSideB2, oArea) != EngineConstants.FALSE
               || URN_CheckBridgeCounters(nSideB3, oArea) != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;

          // Gather the section.
          GameObject oSection = GetObjectByTag(EngineConstants.BRIDGE_SECTION_PREFIX + IntToString(nSection));
          GameObject oBlocker = GetObjectByTag(EngineConstants.BRIDGE_BLOCKER_PREFIX + IntToString(nSection));

          // Ensure multiple tranperencies aren't stacked.
          List<xEffect> arEffects = GetEffects(oSection);

          RemoveEffectArray(oSection, arEffects);

          // Active
          if (bFirst != EngineConstants.FALSE && bSecond != EngineConstants.FALSE)
          {

               SetObjectActive(oSection, EngineConstants.TRUE);
               SetObjectActive(oBlocker, EngineConstants.FALSE);

               // If not already active play a visual effect.
               if (GetLocalInt(oSection, EngineConstants.PLC_COUNTER_1) == EngineConstants.FALSE)
               {

                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(1118), oSection);

               }

               // Flag the section as active.
               SetLocalInt(oSection, EngineConstants.PLC_COUNTER_1, EngineConstants.TRUE);

               if (WR_GetPlotFlag(EngineConstants.PLT_URN230PT_BRIDGE, EngineConstants.URN_BRIDGE_SOLID_TILE_APPEARS) == EngineConstants.FALSE)
               {
                    GameObject oPC = GetHero();

                    WR_SetPlotFlag(EngineConstants.PLT_URN230PT_BRIDGE, EngineConstants.URN_BRIDGE_SOLID_TILE_APPEARS, EngineConstants.TRUE);
                    UT_Talk(oPC, oPC, EngineConstants.URN_DG_PARTY_BRIDGE_HELP);
               }

          }
          // Partially active
          else if (bFirst != EngineConstants.FALSE || bSecond != EngineConstants.FALSE)
          {

               // Visual transparency to represent state.
               xEffect eTransparent = Effect(EngineConstants.EFFECT_TYPE_ALPHA);

               SetEffectEngineFloatRef(ref eTransparent, EngineConstants.EFFECT_FLOAT_POTENCY, 0.4f);

               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eTransparent, oSection, 0.0f, oSection, 0);

               // Section AND blocker become active.
               SetObjectActive(oSection, EngineConstants.TRUE);
               SetObjectActive(oBlocker, EngineConstants.TRUE);
               SetObjectInteractive(oBlocker, EngineConstants.FALSE);

               // Disable the bridge section if active.
               if (GetLocalInt(oSection, EngineConstants.PLC_COUNTER_1) != EngineConstants.FALSE)
               {
                    URN_DisableSection(oSection);
               }
               else if (WR_GetPlotFlag(EngineConstants.PLT_URN230PT_BRIDGE, EngineConstants.URN_BRIDGE_GHOST_TILE_APPEARS) == EngineConstants.FALSE)
               {
                    GameObject oPC = GetHero();

                    WR_SetPlotFlag(EngineConstants.PLT_URN230PT_BRIDGE, EngineConstants.URN_BRIDGE_GHOST_TILE_APPEARS, EngineConstants.TRUE);
                    UT_Talk(oPC, oPC, EngineConstants.URN_DG_PARTY_BRIDGE_HELP);
               }

          }
          // Not active
          else
          {

               SetObjectActive(oSection, EngineConstants.FALSE);
               SetObjectActive(oBlocker, EngineConstants.TRUE);
               SetObjectInteractive(oBlocker, EngineConstants.FALSE);

               // Disable the bridge section if active.
               if (GetLocalInt(oSection, EngineConstants.PLC_COUNTER_1) != EngineConstants.FALSE)
               {
                    URN_DisableSection(oSection);
               }

          }

     }

     /* @brief Disables a section of the bridge puzzle.
 *
 * Disables a section of the bridge puzzle by expelling any creature standing
 * on it and flagging it as inactive.
 *
 * @param oSection The placeble GameObject section to be disabled.
 **/
     public void URN_DisableSection(GameObject oSection)
     {

          //Determine if plarty members are standing on the section and expell them
          List<GameObject> arNearest = GetNearestObject(oSection, EngineConstants.OBJECT_TYPE_CREATURE, 4);

          GameObject oNearest;
          float fDistance;
          int i;

          // Cyrcle the party; they are the only 4 nearby creatures.
          for (i = 0; i < 4; ++i)
          {

               oNearest = arNearest[i];

               fDistance = GetDistanceBetween(oSection, oNearest);

               // Anything within X of the placeable is standing on it?
               if (fDistance < 2.0f)
               {

                    UT_LocalJump(oNearest, EngineConstants.URN_WP_BRIDGE_PUZZLE);

                    // Visual
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(1005), oNearest, 3.0f);

                    if (WR_GetPlotFlag(EngineConstants.PLT_URN230PT_BRIDGE, EngineConstants.URN_BRIDGE_PLAYER_FELL) == EngineConstants.FALSE)
                    {
                         GameObject oPC = GetHero();

                         WR_SetPlotFlag(EngineConstants.PLT_URN230PT_BRIDGE, EngineConstants.URN_BRIDGE_PLAYER_FELL, EngineConstants.TRUE);
                         UT_Talk(oPC, oPC, EngineConstants.URN_DG_PARTY_BRIDGE_HELP);
                    }

               }

          }

          // Flag the GameObject as inactive.
          SetLocalInt(oSection, EngineConstants.PLC_COUNTER_1, EngineConstants.FALSE);

          // Visual
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(1118), oSection);

     }

     /* @brief Checks for nCheckFor on the area counters.
 *
 * Checks EngineConstants.AREA_COUNTER_1, EngineConstants.AREA_COUNTER_2 and EngineConstants.AREA_COUNTER_3 on oArea for
 * nCheckFor. Used to determine which switches are active on bridge puzzle
 * in the Gauntlet area of the Urn of Sacred Ashes plot.
 *
 * @param nCheckFor The number to check for
 * @param oArea The area whose variables should be polled.
 * @returns The counter (1,2 or 3) if the areas counters contains the number, EngineConstants.FALSE otherwise
 * @author Grant Mackay
 **/
     public int URN_CheckBridgeCounters(int nCheckFor, GameObject oArea)
     {

          // Area counters holding active switches.
          int nCounter1 = GetLocalInt(oArea, EngineConstants.AREA_COUNTER_1);
          int nCounter2 = GetLocalInt(oArea, EngineConstants.AREA_COUNTER_2);
          int nCounter3 = GetLocalInt(oArea, EngineConstants.AREA_COUNTER_3);

          // If a counter is determined to hold the desired value return the counter's number.
          if (nCounter1 == nCheckFor) return 1;
          if (nCounter2 == nCheckFor) return 2;
          if (nCounter3 == nCheckFor) return 3;

          // No counter contains the value.
          return EngineConstants.FALSE;

     }

     //------------------------------------------------------------------------------
     // Haven nonsense.
     /* @brief Sounds the alarm in the Village of Haven
      *
      * Sets the villagers of Haven into motion should the player be caught doing
      * something he should not be. Most Civilians move to the chantry while the
      * gaurds go hostile.
      *
      * @author Grant Mackay
      **/
     public void URN_SetVillageAlarm()
     {

          GameObject oTarg;

          // not combat types no longer active.
          oTarg = GetObjectByTag(EngineConstants.URN_CR_HAVEN_CHILD);
          WR_SetObjectActive(oTarg, EngineConstants.FALSE);

          oTarg = GetObjectByTag(EngineConstants.URN_CR_HAVEN_VILLAGER);
          WR_SetObjectActive(oTarg, EngineConstants.FALSE);

          UT_TeamAppears(EngineConstants.URN_TEAM_VILLAGE_POST_PLOT, EngineConstants.FALSE);

          // Gaurd fights.
          oTarg = GetObjectByTag(EngineConstants.URN_CR_HAVEN_GUARD);
          UT_CombatStart(oTarg, GetHero());

          // The shopkeepers talk triggers should be destroyed.
          oTarg = GetObjectByTag(EngineConstants.URN_TR_SHOPKEEPER, 0);
          WR_SetObjectActive(oTarg, EngineConstants.FALSE);
          oTarg = GetObjectByTag(EngineConstants.URN_TR_SHOPKEEPER, 1);
          WR_SetObjectActive(oTarg, EngineConstants.FALSE);

          if (WR_GetPlotFlag(EngineConstants.PLT_URN100PT_HAVEN, EngineConstants.SHOPKEEPER_KILLED) == EngineConstants.FALSE)
          {

               oTarg = GetObjectByTag(EngineConstants.URN_CR_SHOPKEEPER);
               WR_SetObjectActive(oTarg, EngineConstants.FALSE);

          }

          // Hostile villagers activate!
          UT_TeamAppears(EngineConstants.URN_TEAM_VILLAGE_AMBUSH);
          UT_TeamGoesHostile(EngineConstants.URN_TEAM_VILLAGE_AMBUSH);

     }

     /* @brief Removes the villagers of haven from the chantry
 *
 * Collects all the villagers present in the Chantry area of the Village of
 * Haven and disables them.
 *
 * @author Grant Mackay
 **/
     public void URN_RemoveChantryVillagers()
     {

          UT_TeamAppears(EngineConstants.URN_TEAM_CHANTRY_VILLAGERS, EngineConstants.FALSE);

     }

     //------------------------------------------------------------------------------
     // Universal item acquisition
     /* @brief Handles item acquisitions in the Urn of Sacred Ashes plot.
      *
      * Handles plot events related to picking up certain the taper and black
      * pearl in the Urn plot.
      *
      * @author Grant Mackay
      **/
     public void URN_ItemAcquired()
     {

          int bPearl = UT_CountItemInInventory(EngineConstants.URN_IT_PEARL_R);
          int bTaper = UT_CountItemInInventory(EngineConstants.URN_IT_TAPER_R);

          if (bPearl != EngineConstants.FALSE && bTaper != EngineConstants.FALSE)
          {

               GameObject oBrazier = GetObjectByTag(EngineConstants.URN_IP_WRAITH_BRAZIER);

               SetObjectInteractive(oBrazier, EngineConstants.TRUE);

          }

     }

     //------------------------------------------------------------------------------
     // Doppelganger fight set up
     /* @brief Sets up the Doppelganger fight in the Gauntlet.
      *
      *  Determines which player and party member clones should be activated and
      *  sets their transparency effects.
      *
      * @author Grant Mackay
      **/
     public void URN_SetupDoppelgangers()
     {

          // De-activate anything currently active.
          List<GameObject> arTeam = UT_GetTeam(EngineConstants.URN_TEAM_DOPPELGANGER);
          int nTeamSize = GetArraySize(arTeam);

          GameObject oTeam;
          int nIndex;

          for (nIndex = 0; nIndex < nTeamSize; ++nIndex)
          {
               oTeam = arTeam[nIndex];
               SetTeamId(oTeam, -1);
               WR_SetObjectActive(oTeam, EngineConstants.FALSE);
          }


          // Determine which player doppelganger to activate.
          List<GameObject> arParty;
          GameObject oPC, oParty, oDplg = null;
          string sTag;
          int nGender, nClass, nRace, nSize, nNth;

          oPC = GetHero();
          nGender = GetCreatureGender(oPC);
          nClass = GetCreatureCoreClass(oPC);
          nRace = GetCreatureRacialType(oPC);

          // Rogue
          if (nClass == EngineConstants.CLASS_ROGUE)
          {
               if (nGender == EngineConstants.GENDER_FEMALE)
               {
                    if (nRace == EngineConstants.RACE_DWARF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_ROGUE_F_D);
                    }
                    else if (nRace == EngineConstants.RACE_ELF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_ROGUE_F_E);
                    }
                    else
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_ROGUE_F);
                    }
               }
               else
               {
                    if (nRace == EngineConstants.RACE_DWARF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_ROGUE_M_D);
                    }
                    else if (nRace == EngineConstants.RACE_ELF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_ROGUE_M_E);
                    }
                    else
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_ROGUE_M);
                    }
               }
          }

          // Warrior
          else if (nClass == EngineConstants.CLASS_WARRIOR)
          {
               if (nGender == EngineConstants.GENDER_FEMALE)
               {
                    if (nRace == EngineConstants.RACE_DWARF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_WARRIOR_F_D);
                    }
                    else if (nRace == EngineConstants.RACE_ELF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_WARRIOR_F_E);
                    }
                    else
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_WARRIOR_F);
                    }
               }

               else
               {
                    if (nRace == EngineConstants.RACE_DWARF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_WARRIOR_M_D);
                    }
                    else if (nRace == EngineConstants.RACE_ELF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_WARRIOR_M_E);
                    }
                    else
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_WARRIOR_M);
                    }
               }
          }

          // Wizard
          if (nClass == EngineConstants.CLASS_WIZARD)
          {
               if (nGender == EngineConstants.GENDER_FEMALE)
               {
                    if (nRace == EngineConstants.RACE_ELF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_MAGE_F_E);
                    }
                    else
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_MAGE_F);
                    }
               }

               else
               {
                    if (nRace == EngineConstants.RACE_ELF)
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_MAGE_M_E);
                    }
                    else
                    {
                         oDplg = GetObjectByTag(EngineConstants.URN_CR_DPLG_MAGE_M);
                    }
               }
          }

          xEffect eTransparent = Effect(EngineConstants.EFFECT_TYPE_ALPHA);
          SetEffectEngineFloatRef(ref eTransparent, EngineConstants.EFFECT_FLOAT_POTENCY, 0.5f);

          SetObjectActive(oDplg, EngineConstants.TRUE);
          AddAbility(oDplg, EngineConstants.ABILITY_TRAIT_GHOST);
          SetName(oDplg, GetName(oPC));
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eTransparent, oDplg);

          SetTeamId(oDplg, EngineConstants.URN_TEAM_DOPPELGANGER);

          // Activate follower clones
          arParty = GetPartyList();
          nSize = GetArraySize(arParty);

          for (nNth = 0; nNth < nSize; ++nNth)
          {

               oParty = arParty[nNth];
               sTag = GetTag(oParty);
               oDplg = GetObjectByTag(sTag + "_dplg");

               SetObjectActive(oDplg, EngineConstants.TRUE);
               AddAbility(oDplg, EngineConstants.ABILITY_TRAIT_GHOST);
               SetTeamId(oDplg, EngineConstants.URN_TEAM_DOPPELGANGER);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eTransparent, oDplg);

               if (sTag == EngineConstants.GEN_FL_DOG)
               {
                    SetName(oDplg, GetName(oParty));
               }

          }

     }

     //------------------------------------------------------------------------------
     // Riddle section
     /* @brief Handles riddle solution in the Gauntlet.
      *
      *  Increments the counter representing the number of riddiles answered
      *  correctly, or Ash Wraith's killed, in the riddle section of the Gauntlet.
      *
      * @param oRiddler The GameObject activating the increment.
      *
      * @author Grant Mackay
      **/
     public void URN_RiddleIncrement(GameObject oRiddler)
     {

          GameObject oArea = GetArea(oRiddler);
          GameObject oDoor = GetObjectByTag(EngineConstants.URN_IP_RIDDLE_DOOR);
          int nRiddle = GetLocalInt(oArea, EngineConstants.AREA_COUNTER_8);

          // Update riddle counter
          nRiddle++;

          SetLocalInt(oArea, EngineConstants.AREA_COUNTER_8, nRiddle);

          // If all riddles have been answered open the door
          if (nRiddle == 8)
          {

               AudioTriggerPlotEvent(86);
               UT_OpenDoor(oDoor, oDoor);
               nRiddle = 0;

          }

          // Set the new riddle counter value
          SetLocalInt(oArea, EngineConstants.AREA_COUNTER_8, nRiddle);

          // Visual feedback
          Vector3 vSource = GetPosition(oRiddler);
          Vector3 vTarget = GetPosition(oDoor);

          vSource.z += 1.0f;
          vTarget.z += 1.0f;

          FireProjectile(211, vSource, vTarget, 0, EngineConstants.TRUE);
          FireProjectile(211, vSource, vTarget, 0, EngineConstants.TRUE);

          SetObjectActive(oRiddler, EngineConstants.FALSE);

     }
}