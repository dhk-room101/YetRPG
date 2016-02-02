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
     // -----------------------------------------------------------------------------
     // sys_autoscale_h
     // -----------------------------------------------------------------------------
     /*
         Creature scaling and levelup system

     */
     // -----------------------------------------------------------------------------
     // owner: georg zoeller
     // -----------------------------------------------------------------------------

     //#include"sys_chargen_h"
     //#include"ai_main_h_2"
     //#include"plt_gen00pt_backgrounds"
     //#include"wrappers_h"
     //moved public const float AS_CREATURE_BASE_HEALTH  = 50.0f;
     //moved public const float AS_CREATURE_BASE_STAMINA = 50.0f;

     //moved public const string AUTO_SCALE_FACTOR_HEALTH = "AsHPScale";
     //moved public const string AUTO_SCALE_FACTOR_ATTR1 ="Attr1";
     //moved public const string AUTO_SCALE_FACTOR_ATTR2 ="Attr2";
     //moved public const string AUTO_SCALE_FACTOR_ATTR3 ="Attr3";

     //moved public const float AS_POINTS_PER_LEVEL = 3.0f;

     //moved public const int AS_STRATEGY_DEFAULT = 0;
     //moved public const int AS_STRATEGY_RANGED  = 3;

     //moved public const int AS_MAX_PACKAGE_ABILITIES = 12;

     public int AS_GetCreatureLevelToScale(GameObject oCreature, int nAreaLevel)
     {
          int nLevelToScale = nAreaLevel;
          int nRank = GetCreatureRank(oCreature);
          int nApr = GetAppearanceType(oCreature);
          int nMinLevel = GetLocalInt(oCreature, EngineConstants.MIN_LEVEL);
          int nDisableAppearanceLevelLimit = GetLocalInt(GetModule(), EngineConstants.DISABLE_APPEARANCE_LEVEL_LIMITS); // global module switch
          GameObject oArea = GetArea(oCreature);
          int nAreaID = GetLocalInt(oArea, EngineConstants.AREA_ID);
          int nDisableAppearanceLevelLimitPerArea = GetM2DAInt(225, "IgnoreAppMaxLevel", nAreaID);

          // scale by rank
          int nLevelRankScale = GetM2DAInt(Diff_GetAutoScaleTable(), "nLevelScale", nRank);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AS_GetCreatureLevelToScale", "rank level scale: " + IntToString(nLevelRankScale));
#endif

          nLevelToScale = Max(1, nLevelToScale + nLevelRankScale);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AS_GetCreatureLevelToScale", "level to scale: " + IntToString(nLevelToScale));
#endif

          // limit max level per appearance
          int nMaxScaleLevel = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "MaxScaleLevel", nApr);

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AS_GetCreatureLevelToScale", "max scale level: " + IntToString(nMaxScaleLevel));
#endif

          if (IsSummoned(oCreature) == EngineConstants.FALSE && nDisableAppearanceLevelLimit == EngineConstants.FALSE && nDisableAppearanceLevelLimitPerArea == EngineConstants.FALSE)
          {
               if (nRank == EngineConstants.CREATURE_RANK_CRITTER || (nRank == EngineConstants.CREATURE_RANK_NORMAL) || nRank == EngineConstants.CREATURE_RANK_ONE_HIT_KILL || nRank == EngineConstants.CREATURE_RANK_WEAK_NORMAL)
               {
                    if (nMaxScaleLevel > 0 && nLevelToScale > nMaxScaleLevel)
                         nLevelToScale = nMaxScaleLevel;
               }
          }

          if (nLevelToScale < nMinLevel)
               nLevelToScale = nMinLevel;

          // Must be last
          int nClimaxArmy = GetLocalInt(oCreature, EngineConstants.CLIMAX_ARMY_ID);
          if (nClimaxArmy > 0 && GetTag(gameObject) != "cli000cr_army_legion")
          {
               int nClimaxMaxLevel = GetM2DAInt(EngineConstants.TABLE_PLOTACTIONS, "MaxLevel", nClimaxArmy);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AS_GetCreatureLevelToScale", "critter creature belong to climax army: " + IntToString(nClimaxArmy) +
                   ", max level: " + IntToString(nClimaxMaxLevel));
#endif
               if (nClimaxMaxLevel > 0 && nLevelToScale > nClimaxMaxLevel)
                    nLevelToScale = nClimaxMaxLevel;
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AS_GetCreatureLevelToScale", "area level: " + IntToString(nAreaLevel) + ", final level: " + IntToString(nLevelToScale));
#endif

          if (nRank == EngineConstants.CREATURE_RANK_ONE_HIT_KILL && nLevelToScale > 10)
               nLevelToScale = 10;

          return nLevelToScale;
     }

     public int As_AddAbility(GameObject oCreature, int nPackage, string sAbilityIndex, int nNum)
     {
          int nAbility = 0;
          nAbility = GetM2DAInt(EngineConstants.TABLE_PACKAGES, sAbilityIndex, nPackage);
          if (nAbility <= 0) // -1 used to skip
               return nAbility;
          AddAbility(oCreature, nAbility);
          if (GetCreatureRank(oCreature) == EngineConstants.CREATURE_RANK_PLAYER)
               SetQuickslot(oCreature, -1, nAbility);
          nNum++;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AS_AssignAbilities", "Ability Added: " + Log_GetAbilityNameById(nAbility));
#endif

          return nNum;
     }

     public void AS_AssignAbilities(GameObject oCreature, int nAbilitiesToGive)
     {
          int nPackage = GetPackage(oCreature);
          int nNum = 0;
          int bIsFollower = EngineConstants.FALSE;
          if (GetCreatureRank(oCreature) == EngineConstants.CREATURE_RANK_PLAYER)
               bIsFollower = EngineConstants.TRUE;

          if (nAbilitiesToGive < 1) nAbilitiesToGive = 1;
          else if (nAbilitiesToGive > EngineConstants.AS_MAX_PACKAGE_ABILITIES) nAbilitiesToGive = EngineConstants.AS_MAX_PACKAGE_ABILITIES;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AS_AssignAbilities", "Number of abilities to assign: " + IntToString(nAbilitiesToGive));
#endif

          nNum = As_AddAbility(oCreature, nPackage, "Ability1", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability2", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability3", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability4", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability5", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability6", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability7", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability8", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability9", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability10", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability11", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

          nNum = As_AddAbility(oCreature, nPackage, "Ability12", nNum);
          if (nNum == 0 || nNum == nAbilitiesToGive) return;

     }

     public int AS_SelectStrategy(GameObject oCreature, int nClass, int bIgnorePackage = 0)
     {

          int nStrategy = EngineConstants.AS_STRATEGY_DEFAULT;

          int nPackage = (bIgnorePackage == 0) ? GetPackage(oCreature) : 0;

          if (nPackage > 0)
          {
               nStrategy = GetM2DAInt(EngineConstants.TABLE_PACKAGES, "Strategy", nPackage);

#if DEBUG
               if (nStrategy > 0)
                    Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "Determine strategy from PACKAGE ... success");
#endif
          }

          if (nStrategy == EngineConstants.AS_STRATEGY_DEFAULT) // no strategy from package
          {
               nStrategy += nClass * 100;              // Class package ids are class id * 100
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "using strategy: " + GetM2DAString(Diff_GetAutoScaleTable(), "Label", nStrategy));
#endif

          return (nStrategy);
     }

     public void AS_SetDepletable(GameObject oCreature, float fMax, int nProp)
     {
          if (fMax == 0.0f)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale_h.SetBaseHealth", "Data error: Health set to 0");
#endif

               fMax = 1.0f;
          }
          SetCreatureProperty(oCreature, nProp, fMax, EngineConstants.PROPERTY_VALUE_BASE);
          float fHeal = GetCreatureProperty(oCreature, nProp);
          SetCreatureProperty(oCreature, nProp, fHeal, EngineConstants.PROPERTY_VALUE_CURRENT);
     }

     public int AS_DetermineClass(GameObject oCreature)
     {
          if (GetCreatureCurrentClass(oCreature) == 0)
          {

               int nPackage = GetPackage(oCreature);

               int nClass;
               if (nPackage > 0)
               {
                    nClass = GetM2DAInt(EngineConstants.TABLE_PACKAGES, "StartingClass", nPackage);

                    if (nClass > 0)
                    {
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "Determine class from PACKAGE ... success");
#endif
                         return nClass;
                    }
               }

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "Determined class from PACKAGE ... failed, reverting to legacy code");
#endif

               // LEGACY CODE BELOW HERE, fallback for badly setup creatures.

               GameObject oItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_CHEST, oCreature);
               int nBaseItem = GetBaseItemType(oItem);
               if (nBaseItem == 38 /*clothing*/)
               {
                    // ---------------------------------------------------------------------
                    // Critters can't be mages
                    // ---------------------------------------------------------------------
                    if (GetCreatureRank(oCreature) != EngineConstants.CREATURE_RANK_CRITTER)
                    {
                         return EngineConstants.CLASS_WIZARD;
                    }
               }
               return EngineConstants.CLASS_WARRIOR;
          }
          else
          {
               return GetCreatureCoreClass(oCreature);
          }
     }

     public void AS_AddClassLevels(GameObject oCreature, int nClass = 0, int nLevels = 1, float fRankModifier = 0.0f, int bAdjustXP = EngineConstants.TRUE, int bChangedClass = EngineConstants.FALSE)
     {

          int nSubtractor = 0;
          if (nLevels < 1)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "Error adding classlevels. 0 requested...");
#endif

               return;
          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "AS_AddClassLevels * " + ToString(nLevels));
#endif
          }

          if (nClass == 0)
          {
               nClass = AS_DetermineClass(oCreature);
          }

          if (fRankModifier == 0.0f)
          {
               fRankModifier = GetAutoScaleDataFloat(GetCreatureRank(oCreature), EngineConstants.AS_RANK_SCALE_FACTOR);
          }

          float fRankModifierHealth = GetAutoScaleDataFloat(GetCreatureRank(oCreature), EngineConstants.AS_RANK_HEALTH_SCALE_FACTOR);

          if (fRankModifierHealth == 0.0f)
          {
               fRankModifierHealth = 1.0f;
          }

          int nCurLevel = GetLevel(oCreature);
          // -------------------------------------------------------------------------
          // If the creatures current level if 0/1, we need to apply the initial class
          //
          // Note:
          //   Rank Modifier is applied to only Health and Mana.
          // -------------------------------------------------------------------------
          if (nCurLevel <= 1 && GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE) < 1.0f)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "New character, picking base class: " + ToString(nClass));
#endif
               Chargen_ApplyClassAttributeModifiers(oCreature, nClass, EngineConstants.FALSE);
               Chargen_ApplyClassStatModifiers(oCreature, nClass, EngineConstants.FALSE, fRankModifier);

               // ---------------------------------------------------------------------
               // Grant class abilities
               // ---------------------------------------------------------------------
               Chargen_ApplyClassAbilities(oCreature, nClass, EngineConstants.FALSE);
               nSubtractor = 1;
               SetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_LEVEL, 1.0f);

          }
          else
          {
               if (bChangedClass != EngineConstants.FALSE)
               {
                    Chargen_ApplyClassAbilities(oCreature, nClass, EngineConstants.FALSE);
               }
          }

          if (nLevels - nSubtractor > 0)
          {

               int nLevelsToAdd = nLevels - nSubtractor;
               // -------------------------------------------------------------------------
               // Set stats that are modified per level.
               // instead of looping, we just multiply by level
               //
               // Note:
               //   Rank Modifier is applied to only Health and Mana.
               // -------------------------------------------------------------------------

               float fDamageMod = GetClassDataFloat(EngineConstants.CLASS_DATA_DAMAGE_BONUS, nClass) * nLevelsToAdd;
               float fHealth = (GetClassDataFloat(EngineConstants.CLASS_DATA_HEALTH_PER_LEVEL, nClass) * fRankModifierHealth) * nLevelsToAdd;
               float fMana = GetClassDataFloat(EngineConstants.CLASS_DATA_MANA_PER_LEVEL, nClass) * nLevelsToAdd * fRankModifier;

               Chargen_ModifyCreaturePropertyBase(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_BONUS, fDamageMod);
               Chargen_ModifyCreaturePropertyBase(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, fHealth);
               Chargen_ModifyCreaturePropertyBase(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fMana);

               // -------------------------------------------------------------------------
               // Increase level to new value
               // -------------------------------------------------------------------------
               Chargen_ModifyCreaturePropertyBase(oCreature, EngineConstants.PROPERTY_SIMPLE_LEVEL, IntToFloat(nLevelsToAdd));

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "As_AddClassLevel", "Adding " + ToString(nLevelsToAdd) + " levels");
#endif

          }

          // -------------------------------------------------------------------------
          // Adjust EngineConstants.XP to start of new level if requested
          // -------------------------------------------------------------------------
          if (bAdjustXP != EngineConstants.FALSE)
          {
               int nXP = GetM2DAInt(EngineConstants.TABLE_EXPERIENCE, "XP", nLevels);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "adjusting EngineConstants.XP to " + ToString(nXP) + " to the start of level " + ToString(nLevels));
#endif
               Chargen_ModifyCreaturePropertyBase(oCreature, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE, IntToFloat(nXP));
          }

          SetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_CURRENT_CLASS, IntToFloat(nClass));

     }

     public void AS_SetCommonStats(GameObject oCreature)
     {
          int nRank = GetCreatureRank(oCreature);

          // hackety
          float fRegenBonus = 0.0f; //IntToFloat(((nRank > EngineConstants.CREATURE_RANK_NORMAL && nRank < EngineConstants.CREATURE_RANK_PLAYER)? 2 : 0));

          SetCreatureProperty(oCreature, 51, 1.0f);
          SetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA_COMBAT, EngineConstants.REGENERATION_STAMINA_COMBAT_DEFAULT + fRegenBonus);

          SetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA, EngineConstants.REGENERATION_STAMINA_EXPLORE_DEFAULT);
          SetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_HEALTH, EngineConstants.REGENERATION_HEALTH_EXPLORE_DEFAULT);

          SetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_HEALTH_COMBAT, EngineConstants.REGENERATION_HEALTH_COMBAT_DEFAULT + fRegenBonus);

          // Set threat decrease rate
          SetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_THREAT_DECREASE_RATE, EngineConstants.AI_THREAT_DISSOLVE);

     }

     public void AS_CommenceAutoScaling(GameObject oCreature, int nLevel = 1, int nForceClass = 0)
     {
          int nRank = GetCreatureRank(oCreature);
          float fTotalPoints = 3.0f;
          int nClass = (nForceClass == 0) ? AS_DetermineClass(oCreature) : nForceClass;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "-------------------------------------------------------------");
#endif

          if (nRank != EngineConstants.CREATURE_RANK_INVALID)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "AutoScaling: " + ToString(oCreature) + " to level:" + ToString(nLevel) + " Rank:" + ToString(nRank) + " Class:" + ToString(nClass));
#endif
          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "AutoScaling to default on " + ToString(oCreature) + ": no rank assigned!");
#endif
               //return;
               if (IsFollower(oCreature) != EngineConstants.FALSE && nRank != 100)
               {
                    nRank = EngineConstants.CREATURE_RANK_PLAYER;
               }
               else
               {
                    nRank = EngineConstants.CREATURE_RANK_CRITTER;
               }
          }
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "-------------------------------------------------------------");
#endif

          Chargen_InitializeCharacter(oCreature, EngineConstants.FALSE); // we don't wipe the heroes ability list
          Chargen_ApplyRaceModifiers(oCreature);

          // -------------------------------------------------------------------------
          // Decide for an auto scaling strategy for this creature
          // -------------------------------------------------------------------------
          int nStrategy = AS_SelectStrategy(oCreature, nClass, nForceClass);

          float fRankModifier = GetAutoScaleDataFloat(nRank, EngineConstants.AS_RANK_SCALE_FACTOR);
          int nRankBonusPoints = GetM2DAInt(Diff_GetAutoScaleTable(), "nBonusPoints", nRank);
          float fRulesPoints = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS);

          float fEffectivePoints = (fRankModifier * fTotalPoints) + fRulesPoints;
          float fPointPerNLevels;
          float fWeight;
          float fValue;
          float fSum = 0.0f;
          int i;
          for (i = 1; i <= 6; i++)
          {

               fPointPerNLevels = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE_DATA, "s" + ToString(i), nStrategy);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", ToString(fPointPerNLevels));
#endif
               if (fPointPerNLevels > 0.0f)
               {
                    fWeight = (1 / fPointPerNLevels) / EngineConstants.AS_POINTS_PER_LEVEL;
                    fValue = ((nLevel - 1) * fEffectivePoints * fWeight + (fWeight * nRankBonusPoints) + (fWeight * fRulesPoints)) + EngineConstants.CHARGEN_BASE_ATTRIBUTE_VALUE;
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "stat: " + ToString(i) + ": " + ToString(fValue));
#endif
                    fSum += (fValue - EngineConstants.CHARGEN_BASE_ATTRIBUTE_VALUE);
                    if (i == 6 && GetCreatureRank(oCreature) == EngineConstants.CREATURE_RANK_ONE_HIT_KILL)
                         SetCreatureProperty(oCreature, 6, 10.0f, EngineConstants.PROPERTY_VALUE_BASE); // minimum const
                    else
                         SetCreatureProperty(oCreature, i, IntToFloat(FloatToInt(fValue + 0.5f)) /*makeint*/, EngineConstants.PROPERTY_VALUE_BASE);
               }
               else
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "error retrieving data for stat : " + ToString(i));
#endif
               }
          }

          AS_SetCommonStats(oCreature);

          AS_AddClassLevels(oCreature, nClass, nLevel, fRankModifier);

          int nPackage = GetPackage(oCreature);
          float fLevelsPerAbility = GetM2DAFloat(EngineConstants.TABLE_PACKAGES, "LevelsPerAbility", nPackage);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "Package levels per ability:" + ToString(fLevelsPerAbility));
#endif
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "Core class:" + ToString(GetCreatureCoreClass(oCreature)));
#endif
          if (fLevelsPerAbility == -1.0f) // use class rules instead of package
               fLevelsPerAbility = IntToFloat(GetM2DAInt(EngineConstants.TABLE_RULES_CLASSES, "LevelsPerAbility", nClass));
          if (fLevelsPerAbility <= 0.0f)
               fLevelsPerAbility = 1.0f;
          int nAbilitiesToGive = FloatToInt(IntToFloat(nLevel) / fLevelsPerAbility) + 1; // +1 rounding up
          if (fLevelsPerAbility >= 1.0f)
               nAbilitiesToGive += 2; // +2 bonus at level 1
          if (GetCreatureRank(oCreature) == EngineConstants.CREATURE_RANK_PLAYER && GetCreatureCoreClass(oCreature) != EngineConstants.CLASS_ROGUE) // rogues get dirty fighting automatically
               nAbilitiesToGive++;

          if (nPackage == 91) // mouse
               nAbilitiesToGive = 2;

          //if (GetCreatureRank(oCreature) != EngineConstants.CREATURE_RANK_PLAYER)
          AS_AssignAbilities(oCreature, nAbilitiesToGive);

          if (GetCreatureRank(oCreature) == EngineConstants.CREATURE_RANK_PLAYER)
               Chargen_SetNumTactics(oCreature);

          SetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_MELEE_CRIT_MODIFIER, 3.0f * fRankModifier, EngineConstants.PROPERTY_VALUE_BASE);
          SetCreatureProperty(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_RANGED_CRIT_MODIFIER, 3.0f * fRankModifier, EngineConstants.PROPERTY_VALUE_BASE);

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "rank:" + ToString(nRank));
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "level:" + ToString(nLevel) + " GetLevel says: " + ToString(GetLevel(oCreature)));
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "total pts:" + ToString(fSum));
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "sys_autoscale", "health:" + ToString(GetMaxHealth(oCreature)));
#endif
          HealCreature(oCreature);

          // Non followers:ensure they can't levelup
          if (GetFollowerState(oCreature) == EngineConstants.FOLLOWER_STATE_INVALID)
          {
               SetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS, 0.0f);
               SetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS, 0.0f);
               SetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS, 0.0f);
          }

     }

     public void AS_InitCreature(GameObject oCreature, int nLevel = -1, int bSetAbilities = EngineConstants.TRUE, int nForceClass = 0)
     {

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AS_InitCreature() " + ToString(oCreature) + " " + ToString(nLevel), "");
#endif

          // -------------------------------------------------------------------------
          // Non combatant
          // -------------------------------------------------------------------------
          if (GetCombatantType(oCreature) == EngineConstants.CREATURE_TYPE_NON_COMBATANT)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "Not scaling " + ToString(oCreature), ": creature is a non combatant!");
#endif
               return;
          }

          if (IsOneShotKillCreature(oCreature) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "Scaling as ONESHOT KILL Creature " + ToString(oCreature), "");
#endif

               Chargen_ModifyCreaturePropertyBase(oCreature, EngineConstants.PROPERTY_SIMPLE_LEVEL, 1.0f);
               Chargen_ModifyCreaturePropertyBase(oCreature, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE, 1.0f);
               AS_SetDepletable(oCreature, 1.0f, EngineConstants.PROPERTY_DEPLETABLE_HEALTH);
               return;
          }

          if (GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE) < 1.0f)
          {
               // -------------------------------------------------------------------------
               // Only creatures alive can be scaled.
               // -------------------------------------------------------------------------
               if (IsDead(oCreature) == EngineConstants.FALSE)
               {
                    AS_CommenceAutoScaling(oCreature, nLevel, nForceClass);
               }

          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "Not scaling " + ToString(oCreature), ": already has at least 1 xp!");
#endif
          }

          Chargen_SetNumTactics(oCreature);
          DEBUG_PrintToScreen("Class:" + ToString(GetCreatureCoreClass(oCreature)));

     }

     public void Levelup_SetReadyToLevelUp(GameObject oPartyMember, int bShowLevelupVFX = EngineConstants.TRUE)
     {
          SetCanLevelUp(oPartyMember, 1);

          int nNewLevel = GetLevel(oPartyMember) + 1;

          int nXPCur = GetExperience(oPartyMember);
          int nXPNext = GetM2DAInt(EngineConstants.TABLE_EXPERIENCE, "XP", nNewLevel);

          int nClass = GetCreatureCoreClass(oPartyMember);
          int nTalent = GetM2DAInt(EngineConstants.TABLE_RULES_CLASSES, "LevelsPerAbility", nClass);
          int nSkill = GetM2DAInt(EngineConstants.TABLE_RULES_CLASSES, "LevelsPerSkill", nClass);

          while (nXPCur >= nXPNext && nXPNext > 0)
          {

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "Levelup_SetReadyToLevelUp", ToString(nXPCur) + " >= " + ToString(nXPNext) + "... running levelup", oPartyMember);
#endif

               if (nTalent > 0 && nNewLevel % nTalent == 0)
               {
                    Chargen_ModifyCreaturePropertyBase(oPartyMember, EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS, 1.0f);
               }

               if (IsHumanoid(oPartyMember) != EngineConstants.FALSE)
               {
                    if (nSkill > 0 && nNewLevel % nSkill == 0)
                    {
                         Chargen_ModifyCreaturePropertyBase(oPartyMember, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS, 1.0f);
                    }
               }
               else
               {
                    // -----------------------------------------------------------------
                    // No EngineConstants.XP for non humanoids
                    // -----------------------------------------------------------------
                    SetCreatureProperty(oPartyMember, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS, 0.0f);
               }

               // Add 3 attribute points
               Chargen_ModifyCreaturePropertyBase(oPartyMember, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS, 3.0f);
               AS_AddClassLevels(oPartyMember, nClass, 1, 0.0f, EngineConstants.FALSE, EngineConstants.FALSE);

               //`   // ---------------------------------------------------------------------
               // Only humanoids gain specializations
               //`   // ---------------------------------------------------------------------
               if (IsHumanoid(oPartyMember) != EngineConstants.FALSE)
               {
                    float fPoints = GetCreatureProperty(oPartyMember, 38);

                    // at 7 as a player and 14, give spec points.
                    int nSpec = GetM2DAInt(EngineConstants.TABLE_EXPERIENCE, "SpecPoint", nNewLevel);
                    if (((nSpec == 1) && (fPoints == 0.0f) && (IsHero(oPartyMember) != EngineConstants.FALSE)) || ((nSpec == 2) && (fPoints < 2.0f)) || ((nSpec == 3) && (fPoints < 3.0f)))
                    {
                         fPoints += 1.0f;
                         SetCreatureProperty(oPartyMember, 38, fPoints);
                    }
               }

               nXPNext = GetM2DAInt(EngineConstants.TABLE_EXPERIENCE, "XP", ++nNewLevel);
          }

          // -------------------------------------------------------------------------
          // We have 4, sequentially ordered vfx in vfx_base.
          // depending on new level, different ones play.
          // -------------------------------------------------------------------------

          // Don't apply VFX if you are in cutscenes or a weird mode.
          if (GetGameMode() == EngineConstants.GM_COMBAT || GetGameMode() == EngineConstants.GM_EXPLORE)
          {
               int nVfx = 30020;
               if (nNewLevel > 5)
                    nVfx++;
               if (nNewLevel > 10)
                    nVfx++;
               if (nNewLevel > 15)
                    nVfx++;
               if (bShowLevelupVFX != EngineConstants.FALSE)
                    ApplyEffectVisualEffect(oPartyMember, oPartyMember, nVfx, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f, 0);
          }
          // --> Telemetry stub. New Level is nNewLevel here. <--

     }
}