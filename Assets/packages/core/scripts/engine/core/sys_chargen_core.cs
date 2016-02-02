//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sys_chargen_core : MonoBehaviour
{
    Engine engine { get; set; }

     // -----------------------------------------------------------------------------
     // sys_chargen
     // -----------------------------------------------------------------------------
     /*
         Character generation script
     */
     // -----------------------------------------------------------------------------
     // owner: georg zoeller
     // -----------------------------------------------------------------------------

     /*

                     int nSize = engine.GetMaxInventorySize(oChar);
                     engine.LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "size = " + engine.ToString(nSize));
                     if (nSize == -1)
                     {
                         engine.SetMaxInventorySize(INVENTORY_SIZE_BASE, oChar);
                         nSize = engine.GetMaxInventorySize(oChar);
                         engine.LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "size = " + engine.ToString(nSize));
                     }
     */
     //#include "sys_chargen_h"
     //#include "log_h"
     //#include "sys_autoscale_h"
     //#include "plt_gen00pt_backgrounds"
     //#include "sys_rewards_h"
     //#include "sys_autolevelup_h"
     //#include "achievement_core_h"

     //moved const int SCREEN_ENTER_DIRECTION_NEXT = 0;
     //moved const int SCREEN_ENTER_DIRECTION_BACK = 1;

     //moved const int CHARGEN_SCREEN_RACE_GENDER          = 0;
     //moved const int CHARGEN_SCREEN_CLASS_BACKGROUND     = 1;
     //moved const int CHARGEN_SCREEN_APPEARANCE_SOUNDSET  = 2;
     //moved const int CHARGEN_SCREEN_NAME                 = 3;
     //moved const int CHARGEN_SCREEN_ATTRIBUTES           = 4;
     //moved const int CHARGEN_SCREEN_SKILLS               = 5;
     //moved const int CHARGEN_SCREEN_TALENTS              = 6;
     //moved const int CHARGEN_SCREEN_SUMMARY              = 7;

     //moved const int INVENTORY_SIZE_BASE = 70;

     //moved const string UNINITIALIZED_NAME = "Jaden";

     //moved const int MAX_CLASS_INDEX = 3; // Last index of available base classes in cla_base.

     //moved const resource HERBALISM_STARTING_RECIPE1 = R"gen_im_cft_hrb_102.uti";
     //moved const resource HERBALISM_STARTING_RECIPE2 = R"gen_im_cft_hrb_206.uti";
     //moved const resource HERBALISM_STARTING_RECIPE3 = R"gen_im_cft_hrb_101.uti";
     //moved const resource POISON_MAKING_STARTING_RECIPE1 = R"gen_im_cft_psn_101.uti";
     //moved const resource POISON_MAKING_STARTING_RECIPE2 = R"gen_im_cft_psn_102.uti";
     //moved const resource TRAP_MAKING_STARTING_RECIPE1 = R"gen_im_cft_trp_104.uti";
     //moved const resource TRAP_MAKING_STARTING_RECIPE2 = R"gen_im_cft_trp_103.uti";
     //moved const resource TRAP_MAKING_STARTING_RECIPE3 = R"gen_im_cft_trp_205.uti";
     //moved const resource TRAP_MAKING_STARTING_RECIPE4 = R"gen_im_cft_trp_102.uti";
     //moved const resource RUNE_MAKING_STARTING_RECIPE1   = R"gen_im_cft_run_102.uti";     // upgrade flame
     //moved const resource RUNE_MAKING_STARTING_RECIPE2   = R"gen_im_cft_run_103.uti";     // upgrade frost
     //moved const resource RUNE_MAKING_STARTING_RECIPE3   = R"gen_im_cft_run_110.uti";     // upgrade storm
     //moved const resource RUNE_MAKING_STARTING_RECIPE4   = R"gen_im_cft_run_111.uti";     // upgrade sun

        void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     /*
      *  @brief Simulate a character levelup for test system
      *
      *  Simulates a character levelup for testing purposes
      *
      *  @param oChar The character
      *
      *  @author Georg Zoeller
     */
     public void _RunChargen(int nRace, int nClass, GameObject oChar = null, int nBackground = EngineConstants.BACKGROUND_NOBLE)
     {
          if (oChar == null) oChar = gameObject;
          engine.Chargen_InitializeCharacter(oChar, EngineConstants.FALSE);
          engine.Chargen_SelectGender(oChar, EngineConstants.GENDER_MALE);
          engine.Chargen_SelectRace(oChar, nRace);
          engine.Chargen_SelectCoreClass(oChar, nClass);
          engine.Chargen_SelectBackground(oChar, nBackground, EngineConstants.FALSE);

          int nEquipIdx = engine.Chargen_GetEquipIndex(nRace, nClass, nBackground);
          engine.Chargen_InitInventory(oChar, 0, nEquipIdx);
          engine.Chargen_SpendAttributePoints(oChar, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, 3, EngineConstants.FALSE);
          engine.Chargen_SpendAttributePoints(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, 2, EngineConstants.FALSE);

     }


     // -----------------------------------------------------------------------------
     // Core Character
     // -----------------------------------------------------------------------------
     public void HandleEvent(xEvent ev)
     {

          //xEvent ev = engine.GetCurrentEvent();
          int nEventType = engine.GetEventTypeRef(ref ev);
          GameObject oChar = engine.GetEventObjectRef(ref ev, 0);

          int nMode;
          int nInt0 = engine.GetEventIntegerRef(ref ev, 0);
          int nInt1 = engine.GetEventIntegerRef(ref ev, 1);

          // -------------------------------------------------------------------------
          // Debug Data.
          // -------------------------------------------------------------------------
          engine.Log_Trace(EngineConstants.LOG_CHANNEL_EVENTS_CHARGEN, "sys_chargen", "Chargen Event:" + engine.Log_GetEventNameById(nEventType) + " " + engine.ToString(nInt0) + "," + engine.ToString(nInt1), oChar);
          //TrackModuleEvent(nEventType, engine.GetModule(), oChar, engine.GetEventIntegerRef(ref ev, 0), engine.GetEventIntegerRef(ref ev, 1));

          switch (nEventType)
          {

               // ---------------------------------------------------------------------
               // Fires every time the player requests to enter the levelup screen
               // and causes the Chargen UI to show up.
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_PLAYERLEVELUP:
                    {
                         GameObject oPlayer = engine.GetEventObjectRef(ref ev, 0);

                         if (engine.IsShapeShifted(oPlayer) == EngineConstants.FALSE
                              && (engine.GetGameMode() == EngineConstants.GM_EXPLORE
                              || engine.GetGameMode() == EngineConstants.GM_GUI))
                         {
                              engine.StartCharGen(oPlayer, 1 /* MODE_LEVELUP */);
                         }
                         else
                         {
                              engine.UI_DisplayMessage(oPlayer, EngineConstants.UI_MESSAGE_CANT_DO_IN_COMBAT);

                         }
                         break;
                    }

               // ---------------------------------------------------------------------
               // Fired whenever the chargen was invoked, either by starting a new
               // game or by invoking the StartChargen commmand.
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CHARGEN_START:
                    {

                         // -----------------------------------------------------------------
                         // Retrieve and cache the mode in which chargen was started, either
                         // CREATE or LEVELIP
                         // -----------------------------------------------------------------
                         nMode = engine.GetEventIntegerRef(ref ev, 0);
                         engine.SetLocalInt(engine.GetModule(), EngineConstants.CHARGEN_MODE, nMode);

                         if (nMode == EngineConstants.CHARGEN_MODE_CREATE)
                         {
                              // -------------------------------------------------------------
                              // This blanks out the character completely, resetting all stats,
                              // abilities, etc.
                              // -------------------------------------------------------------
                              engine.Chargen_InitializeCharacter(oChar, EngineConstants.TRUE);
                              engine.Log_Chargen("sys_chargen.EVENT_TYPE_CHARGEN_START", "Chargen Mode : engine.Create", oChar);
                         }
                         else
                         {
                              engine.Log_Chargen("sys_chargen.EVENT_TYPE_CHARGEN_START", "Chargen Mode : Levelup", oChar);
                         }

                         break;
                    }

               // ---------------------------------------------------------------------
               // This fires whenever the player selects a soundset for the main hero
               // resource(0) is the conversation file used with the setsoundet xCommand
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CHARGEN_SELECT_SOUNDSET:
                    {
                         // -----------------------------------------------------------------
                         // Playing it safe here, in case someone debug invokes this on top
                         // of existing followers.
                         // -----------------------------------------------------------------
                         if (engine.IsHero(oChar) != EngineConstants.FALSE)
                         {
                              string r = engine.GetEventStringRef(ref ev, 0);
                              engine.SetSoundSet(oChar, r);
                         }

                         break;
                    }


               // ----------------------------------------------------------------------
               // This fires whenever the gender selection on the create a character
               // screen is modified. It also fires when the screen is entered the
               // first time, defaulting the character to male.
               // ----------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CHARGEN_SELECT_GENDER:
                    {
                         int nGender = engine.GetEventIntegerRef(ref ev, 0);
                         engine.Chargen_SelectGender(oChar, nGender);
                         break;
                    }

               // ----------------------------------------------------------------------
               // This fires when the player selects the icon corresponding to any of
               // the available races.
               //
               // int(0) - Constant RACE_* integer
               // int(1) - 1= race selected, 0=race unselected      (unused)
               // ----------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CHARGEN_SELECT_RACE:
                    {
                         int nRace = engine.GetEventIntegerRef(ref ev, 0);
                         int bSelected = engine.GetEventIntegerRef(ref ev, 1);

                         // -----------------------------------------------------------------
                         // Here we rebuild the character from scratch, reinitializing
                         // all values and applying all choices from selections higher
                         // on the UI. This avoids having to undo any kind of selections.
                         // -----------------------------------------------------------------
                         engine.Chargen_InitializeCharacter(oChar, EngineConstants.TRUE);
                         engine.Chargen_SelectGender(oChar, engine.GetCreatureGender(oChar));
                         engine.Chargen_SelectRace(oChar, nRace);

                         // -----------------------------------------------------------------
                         // Initialize to an invalid class id which InitInventory will handle
                         // to load basic gear that's safe for any race/class
                         // -----------------------------------------------------------------
                         engine.Chargen_InitInventory(oChar, 0, 0);

                         break;
                    }


               // ----------------------------------------------------------------------
               // This fires when the player selects the icon corresponding to any of
               // the available classes
               //
               // int(0) - Constant CLASS_* integer
               // int(1) - 1= class selected, 0 = class unselected
               // ----------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CHARGEN_SELECT_CLASS:
                    {
                         int nClass = engine.GetEventIntegerRef(ref ev, 0);
                         int bSelected = engine.GetEventIntegerRef(ref ev, 1);

                         // ----------------------------------------------------------------
                         // Safety check for classes. This avoids issues with some older,
                         // pre ship build exports.
                         // ----------------------------------------------------------------
                         if (nClass < (EngineConstants.MAX_CLASS_INDEX + 1) && nClass > 0)
                         {

                              // ------------------------------------------------------------
                              // If the player had a class selection before, we first need to
                              // rebuild the character.
                              // ------------------------------------------------------------
                              if (bSelected == EngineConstants.FALSE)
                              {
                                   engine.Chargen_InitializeCharacter(oChar, EngineConstants.TRUE);
                                   engine.Chargen_SelectGender(oChar, engine.GetCreatureGender(oChar));
                                   engine.Chargen_SelectRace(oChar, engine.GetCreatureRacialType(oChar));
                              }
                              else
                              {
                                   // --------------------------------------------------------
                                   // Now set the new class and initialize the inventory
                                   // from the class template.
                                   // --------------------------------------------------------
                                   engine.Chargen_SelectCoreClass(oChar, nClass);
                                   engine.Chargen_InitInventory(oChar, nClass, 0);
                              }
                         }
                         break;
                    }



               // ----------------------------------------------------------------------
               // This fires when the player selects the icon corresponding to any of
               // the available backgrounds
               //
               // int(0) - Constant BACKGROUND_* integer
               // ----------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CHARGEN_SELECT_BACKGROUND:
                    {
                         int nBG = engine.GetEventIntegerRef(ref ev, 0);

                         // -----------------------------------------------------------------
                         // engine.Set the background on the player and reinitialize plot flags
                         // for the background
                         // -----------------------------------------------------------------
                         engine.Chargen_InitializeCharacter(oChar, EngineConstants.TRUE);
                         engine.Chargen_SelectGender(oChar, engine.GetCreatureGender(oChar));
                         engine.Chargen_SelectRace(oChar, engine.GetCreatureRacialType(oChar));
                         engine.Chargen_SelectCoreClass(oChar, engine.GetCreatureCoreClass(oChar));


                         engine.Chargen_SelectBackground(oChar, nBG, EngineConstants.FALSE);
                         engine.Chargen_SetupPlotFlags(oChar);


                         // -----------------------------------------------------------------
                         // Generate the index into the equipment template 2da and
                         // then load the starting equipment based on the data returned.
                         // -----------------------------------------------------------------
                         int nClass = engine.GetCreatureCoreClass(oChar);
                         int nRace = engine.GetCreatureRacialType(oChar);
                         int nEquipIdx = engine.Chargen_GetEquipIndex(nRace, nClass, nBG);
                         engine.Chargen_InitInventory(oChar, 0, nEquipIdx);


                         break;
                    }


               // ---------------------------------------------------------------------
               // This fires whenever the player presses + or - on an attribute to spend
               // points on the attribute screen of character generation
               //
               // int(0) - Constant EngineConstants.PROPERTY_* integer
               // int(1) - # of points spent
               // ---------------------------------------------------------------------
               case EngineConstants.EVENT_TYPE_CHARGEN_ASSIGN_ATTRIBUTES:
                    {
                         int nAttribute = engine.GetEventIntegerRef(ref ev, 0);
                         int nPoints = engine.GetEventIntegerRef(ref ev, 1);

                         // -----------------------------------------------------------------
                         // Subtract from available points to spend
                         // -----------------------------------------------------------------
                         engine.Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS, engine.IntToFloat(nPoints * -1));


                         // -----------------------------------------------------------------
                         // Spend it.
                         // -----------------------------------------------------------------
                         engine.Chargen_SpendAttributePoints(oChar, nAttribute, nPoints, EngineConstants.FALSE);
                         break;
                    }


               // ---------------------------------------------------------------------
               // This fires whenever the player assings an ability
               // (skill/talent/spell/specialization)
               //
               // int(0) - ABILITY_* const ant integer. If <0 the ability is removed not added
               // int(1) - If 1, we're dealing with a specialization, if 0 any other ability.
               // ---------------------------------------------------------------------

               case EngineConstants.EVENT_TYPE_CHARGEN_ASSIGN_ABILITIES:
                    {
                         int nAbiNo = engine.GetEventIntegerRef(ref ev, 0);
                         int bSpecialization = (engine.GetEventIntegerRef(ref ev, 1) == 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
                         int nAbi = engine.abs(nAbiNo);

                         // -----------------------------------------------------------------
                         // Skill / Talent / Spell
                         // -----------------------------------------------------------------
                         if (bSpecialization != EngineConstants.FALSE)
                         {
                              int nType = EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS;

                              if (engine.GetAbilityType(nAbi) == EngineConstants.ABILITY_TYPE_SKILL)
                              {
                                   nType = EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS;
                              }
                              if (nAbiNo > 0)
                              {
                                   if (engine.HasAbility(oChar, nAbiNo) == EngineConstants.FALSE)
                                   {
                                        engine.Chargen_ModifyCreaturePropertyBase(oChar, nType, -1.0f);
                                   }
                              }
                              else
                              {
                                   if (engine.HasAbility(oChar, engine.abs(nAbiNo)) != EngineConstants.FALSE)
                                   {
                                        engine.Chargen_ModifyCreaturePropertyBase(oChar, nType, 1.0f);
                                   }
                              }
                         }
                         // -----------------------------------------------------------------
                         // Specialization
                         // -----------------------------------------------------------------
                         else
                         {
                              if (nAbiNo > 0)
                              {
                                   engine.Chargen_ModifyCreaturePropertyBase(oChar, 38, -1.0f);
                              }
                              else
                              {
                                   engine.Chargen_ModifyCreaturePropertyBase(oChar, 38, 1.0f);
                              }
                         }

                         // -----------------------------------------------------------------
                         // This actually adds / removes the ability.
                         // -----------------------------------------------------------------
                         engine._AddAbility(oChar, nAbiNo, nAbiNo < 0 ? EngineConstants.TRUE : EngineConstants.FALSE);

                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_chargen", "Assign Ability: " + engine.ToString(nAbiNo), oChar);

                         break;
                    }

               case EngineConstants.EVENT_TYPE_CHARGEN_SELECT_NAME:
                    {
                         string sName = engine.GetEventStringRef(ref ev, 0);
                         engine.SetName(oChar, sName);
                         break;
                    }

               case EngineConstants.EVENT_TYPE_CHARGEN_AUTOLEVEL:
                    {

                         engine.AL_DoAutoLevelUp(oChar);
                         engine.Chargen_SetNumTactics(oChar);
                         engine.SetCanLevelUp(oChar, engine.Chargen_HasPointsToSpend(oChar));
                         break;
                    }


               case EngineConstants.EVENT_TYPE_CHARGEN_END:
                    {
                         nMode = engine.GetEventIntegerRef(ref ev, 0);            //GetLocalInt(engine.GetModule(),CHARGEN_MODE);
                         int nQuickStart = engine.GetEventIntegerRef(ref ev, 1);
                         // 0 - quickstart
                         // 1 - normal    \
                         // 2 - advanced  / treat as the same
                         engine.Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_chargen", "MODE: " + engine.IntToString(nMode) + ", Quick Start: " + engine.IntToString(nQuickStart));


                         // -----------------------------------------------------------------
                         // Character Creation
                         // -----------------------------------------------------------------
                         if (nMode == EngineConstants.CHARGEN_MODE_CREATE)
                         {
                              engine.SetMaxInventorySize(EngineConstants.INVENTORY_SIZE_BASE, oChar);
                              engine.Chargen_EnableTacticsPresets(oChar);

                              // add recipes
                              engine.CreateItemOnObject(EngineConstants.HERBALISM_STARTING_RECIPE1, oChar, 1, "", EngineConstants.TRUE);
                              engine.CreateItemOnObject(EngineConstants.HERBALISM_STARTING_RECIPE2, oChar, 1, "", EngineConstants.TRUE);
                              engine.CreateItemOnObject(EngineConstants.HERBALISM_STARTING_RECIPE3, oChar, 1, "", EngineConstants.TRUE);
                              engine.CreateItemOnObject(EngineConstants.POISON_MAKING_STARTING_RECIPE1, oChar, 1, "", EngineConstants.TRUE);
                              engine.CreateItemOnObject(EngineConstants.POISON_MAKING_STARTING_RECIPE2, oChar, 1, "", EngineConstants.TRUE);
                              engine.CreateItemOnObject(EngineConstants.TRAP_MAKING_STARTING_RECIPE1, oChar, 1, "", EngineConstants.TRUE);
                              engine.CreateItemOnObject(EngineConstants.TRAP_MAKING_STARTING_RECIPE2, oChar, 1, "", EngineConstants.TRUE);
                              engine.CreateItemOnObject(EngineConstants.TRAP_MAKING_STARTING_RECIPE3, oChar, 1, "", EngineConstants.TRUE);
                              engine.CreateItemOnObject(EngineConstants.TRAP_MAKING_STARTING_RECIPE4, oChar, 1, "", EngineConstants.TRUE);

                              if (engine.IsUsingEP1Resources() != EngineConstants.FALSE)
                              {
                                   engine.CreateItemOnObject(EngineConstants.RUNE_MAKING_STARTING_RECIPE1, oChar, 1, "", EngineConstants.TRUE);
                                   engine.CreateItemOnObject(EngineConstants.RUNE_MAKING_STARTING_RECIPE2, oChar, 1, "", EngineConstants.TRUE);
                                   engine.CreateItemOnObject(EngineConstants.RUNE_MAKING_STARTING_RECIPE3, oChar, 1, "", EngineConstants.TRUE);
                                   engine.CreateItemOnObject(EngineConstants.RUNE_MAKING_STARTING_RECIPE4, oChar, 1, "", EngineConstants.TRUE);
                              }

                              // special handling for aluvian
                              if (nQuickStart == 0)
                              {
                                   engine.Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_chargen", "Setting default values for player character");

                                   int nRandClass = engine.abs((engine.GetLowResTimer() % 3) + 1);

                                   if (nRandClass == EngineConstants.CLASS_ROGUE || nRandClass == EngineConstants.CLASS_WARRIOR)
                                   {
                                        _RunChargen(EngineConstants.RACE_HUMAN, nRandClass, oChar, EngineConstants.BACKGROUND_NOBLE);
                                        engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_HUMAN_NOBLE, EngineConstants.TRUE);
                                   }
                                   else // mage
                                   {
                                        _RunChargen(EngineConstants.RACE_HUMAN, nRandClass, oChar, EngineConstants.BACKGROUND_MAGI);
                                        engine.WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_CIRCLE, EngineConstants.TRUE);
                                   }


                                   engine.Chargen_SetNumTactics(oChar);
                                   engine.SetCanLevelUp(oChar, engine.Chargen_HasPointsToSpend(oChar));

                                   engine.SendEventModuleChargenDone("", "");

                              }
                              else
                              {

                                   if (nQuickStart == 1)
                                   {
                                        engine.AL_DoAutoLevelUp(oChar, EngineConstants.TRUE, EngineConstants.TRUE);
                                   }
                                   engine.Chargen_SetNumTactics(oChar);
                                   engine.SetCanLevelUp(oChar, engine.Chargen_HasPointsToSpend(oChar));
                                   if (nQuickStart == 1 || nQuickStart == 2)
                                   {
                                        engine.SendEventModuleChargenDone("", "");
                                   }
                                   else if (nQuickStart == 3)
                                   {
                                        engine.SendEventModuleChargenDone(EngineConstants.LEVEL_OF_THE_WEEK_RESREF, EngineConstants.LEVEL_OF_THE_WEEK_WAYPOINT);
                                   }
                              }

                              // associate some tactics preset table
                              int nPresetTable;
                              if (engine.GetCreatureCoreClass(oChar) == EngineConstants.CLASS_WARRIOR)
                                   nPresetTable = 1; // tank
                              else if (engine.GetCreatureCoreClass(oChar) == EngineConstants.CLASS_ROGUE)
                                   nPresetTable = 2; // damage dealer
                              else // mage
                                   nPresetTable = 5; // nuker

                              //Chargen_LoadPresetsTable(oChar, nPresetTable);
                              engine.SetTacticPresetID(oChar, nPresetTable);
                         }
                         else
                         {
                              engine.Chargen_SetNumTactics(oChar);
                              engine.Chargen_EnableTacticsPresets(oChar);
                              engine.Chargen_PopulateTacticsForPreset(oChar);
                              engine.SetCanLevelUp(oChar, engine.Chargen_HasPointsToSpend(oChar));
                              // Run logic to grant any achievement ability or class related after committing levelup changes
                              engine.ACH_WhenHeroLevelsUp(oChar, EngineConstants.FALSE);
                              engine.ACH_WhenHeroGainsLevel(oChar, EngineConstants.FALSE);

                              // faster resources regen when higher level
                              engine.SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_HEALTH,
                                  EngineConstants.REGENERATION_HEALTH_EXPLORE_DEFAULT + engine.IntToFloat(engine.GetLevel(oChar)) * 2.0f,
                                  EngineConstants.PROPERTY_VALUE_BASE);
                              engine.SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA,
                                  EngineConstants.REGENERATION_STAMINA_EXPLORE_DEFAULT + engine.IntToFloat(engine.GetLevel(oChar)) * 2.0f,
                                  EngineConstants.PROPERTY_VALUE_BASE);

                         }
                         break;
                    }

          }
     }
}