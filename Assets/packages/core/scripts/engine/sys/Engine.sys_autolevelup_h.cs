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
     // sys_autolevelup
     // -----------------------------------------------------------------------------
     /*
         Autolevelup System

         This system is used to pick talents, skills and attributes for characters where
         the player has decided to use auto-levelup.

         Talents / Skill
         ---------------------
         The system iterates over several auto_levelup package 2da that hold ability
         ids. It will test each ability on the list until it finds one it can pick and
         select it. It will continue to do so until no free talent points remain.

         Note: This system generally assumes 1 level up xEvent per run. If leveling
         up more than one level per run, the picked talents and skills not optimal.

         Attributes
         ---------------------
         TBD.

     */
     // -----------------------------------------------------------------------------
     // owner: georg zoeller, agauthier
     // -----------------------------------------------------------------------------

     //#include"core_h"
     //#include"global_objects_h"

     //moved public const int EngineConstants.TABLE_AL_WARRIOR_DEFAULT =  1002;
     //moved public const int EngineConstants.TABLE_AL_WIZARD_DEFAULT  = 1003;
     //moved public const int EngineConstants.TABLE_AL_ROGUE_DEFAULT   = 1004;
     //moved public const int EngineConstants.TABLE_AL_ALISTAIR = 256;
     //moved public const int EngineConstants.TABLE_AL_DOG = 257;
     //moved public const int EngineConstants.TABLE_AL_LELIANA = 258;
     //moved public const int EngineConstants.TABLE_AL_LOGHAIN = 259;
     //moved public const int EngineConstants.TABLE_AL_MORRIGAN = 260;
     //moved public const int EngineConstants.TABLE_AL_OGHREN = 261;
     //moved public const int EngineConstants.TABLE_AL_STEN = 262;
     //moved public const int EngineConstants.TABLE_AL_WYNNE = 263;
     //moved public const int EngineConstants.TABLE_AL_ZEVRAN = 264;
     //moved public const int EngineConstants.TABLE_AL_IRVING = 265;
     //moved public const int EngineConstants.TABLE_AL_MOUSE = 285;

     public int _GetTableToUseForAL(GameObject oChar)
     {

          int nClass = GetCreatureCoreClass(oChar);
          int nRet = EngineConstants.TABLE_AL_WARRIOR_DEFAULT;
          int nPackage = GetPackage(oChar);
          int nLevelupTable = GetM2DAInt(EngineConstants.TABLE_PACKAGES, "LevelupTable", nPackage);

          if (nLevelupTable > 0)
               nRet = nLevelupTable;

          else
          {
               if (nClass == 0)
               {
                    Warning("Player character has no class and tries to auto-level!. Assuming Warrior");
               }

               switch (nClass)
               {
                    case EngineConstants.CLASS_WARRIOR: nRet = EngineConstants.TABLE_AL_WARRIOR_DEFAULT; break;
                    case EngineConstants.CLASS_WIZARD: nRet = EngineConstants.TABLE_AL_WIZARD_DEFAULT; break;
                    case EngineConstants.CLASS_ROGUE: nRet = EngineConstants.TABLE_AL_ROGUE_DEFAULT; break;
               }

               if (GetTag(oChar) == EngineConstants.GEN_FL_ALISTAIR) nRet = EngineConstants.TABLE_AL_ALISTAIR;
               else if (GetTag(oChar) == EngineConstants.GEN_FL_DOG) nRet = EngineConstants.TABLE_AL_DOG;
               else if (GetTag(oChar) == EngineConstants.GEN_FL_LELIANA) nRet = EngineConstants.TABLE_AL_LELIANA;
               else if (GetTag(oChar) == EngineConstants.GEN_FL_LOGHAIN) nRet = EngineConstants.TABLE_AL_LOGHAIN;
               else if (GetTag(oChar) == EngineConstants.GEN_FL_MORRIGAN) nRet = EngineConstants.TABLE_AL_MORRIGAN;
               else if (GetTag(oChar) == EngineConstants.GEN_FL_OGHREN) nRet = EngineConstants.TABLE_AL_OGHREN;
               else if (GetTag(oChar) == EngineConstants.GEN_FL_STEN) nRet = EngineConstants.TABLE_AL_STEN;
               else if (GetTag(oChar) == EngineConstants.GEN_FL_WYNNE) nRet = EngineConstants.TABLE_AL_WYNNE;
               else if (GetTag(oChar) == EngineConstants.GEN_FL_ZEVRAN) nRet = EngineConstants.TABLE_AL_ZEVRAN;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:_GetTableToUseForAL", "_GetTableToUseForAL" + ToString(nRet));
          return nRet;
     }

     public void AL_SpendSpecializationPoints(GameObject oChar, int nTable)
     {
          //return;//?!?DHK
          int nPoints = FloatToInt(GetCreatureProperty(oChar, 38 /*spec points*/));

          if (nPoints == 0)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendSpecializationPoints", "No Specialization Points Available, Skipping this step.");
               return;
          }

          int nIdx = 0;
          int nCount = GetM2DAInt(nTable, "SpecID", nIdx);

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendSpecializationPoints", "Trying to spend " + ToString(nPoints) + " Spec Points");

          for (nIdx = 1; nIdx < nCount && nPoints > 0; nIdx++)
          {
               int nTalent = GetM2DAInt(nTable, "TalentID", nIdx);
               if (HasAbility(oChar, nTalent) == EngineConstants.FALSE)
               {
                    if (IsAbilityAvailable(oChar, nTalent) != EngineConstants.FALSE)
                    {
                         AddAbility(oChar, nTalent);
                         nPoints--;
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendSpecializationPoints", "Taking " + Log_GetAbilityNameById(nTalent) + "!");
                    }
                    else
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendSpecializationPoints", "-- Not Taking " + Log_GetAbilityNameById(nTalent) + " -  engine says: unavailable");
                    }
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendSpecializationPoints", "-- Not Taking " + Log_GetAbilityNameById(nTalent) + " -  already have it");
               }

          }

          // Resync Property
          SetCreatureProperty(oChar, 38, IntToFloat(nPoints));

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "------------- AutoLevelUp Spec Summary ---------------------");
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "Remaining Points: " + ToString(nPoints));
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "------------- AutoLevelUp Spec Summary ---------------------");

     }

     public void AL_SpendSkillPoints(GameObject oChar, int nTable, int bInitial = EngineConstants.FALSE)
     {

          int nPoints = FloatToInt(GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS));
          int nIdx = 0;
          int nCount;
          if (bInitial != EngineConstants.FALSE)
               nCount = GetM2DAInt(nTable, "InitialSkillID", nIdx);
          else
               nCount = GetM2DAInt(nTable, "SkillID", nIdx);

          int nPackage = GetPackage(oChar);
          if (nPackage == 91) // mouse
               nPoints = 0;

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendSkillPoints", "Trying to spend " + ToString(nPoints) + " Skill Points");

          for (nIdx = 1; nIdx < nCount && nPoints > 0; nIdx++)
          {
               int nTalent;
               if (bInitial != EngineConstants.FALSE)
                    nTalent = GetM2DAInt(nTable, "InitialSkillID", nIdx);
               else
                    nTalent = GetM2DAInt(nTable, "SkillID", nIdx);

               if (HasAbility(oChar, nTalent) == EngineConstants.FALSE)
               {
                    if (IsAbilityAvailable(oChar, nTalent) != EngineConstants.FALSE || bInitial != EngineConstants.FALSE)
                    {
                         AddAbility(oChar, nTalent);
                         SetQuickslot(oChar, -1, nTalent);
                         nPoints--;
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_AddSkill", "Taking " + Log_GetAbilityNameById(nTalent) + "!");
                    }
                    else
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_AddSkill", "-- Not Taking " + Log_GetAbilityNameById(nTalent) + " -  engine says: unavailable");
                    }
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_AddSkill", "-- Not Taking " + Log_GetAbilityNameById(nTalent) + " -  already have it");
               }
          }

          // Resync Property
          SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS, IntToFloat(nPoints));

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "------------- AutoLevelUp Skill  Summary ---------------------");
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "Remaining Points: " + ToString(nPoints));
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "------------- AutoLevelUp Skill Summary ---------------------");

     }

     public void AL_SpendTalentSpellPoints(GameObject oChar, int nTable, int bInitial = EngineConstants.FALSE)
     {

          int nPoints = FloatToInt(GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS));
          int nIdx = 0;
          int nCount;
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "TABLE: " + IntToString(nTable) + ", initial= " + IntToString(bInitial));

          if (bInitial != EngineConstants.FALSE)
               nCount = GetM2DAInt(nTable, "InitialTalentID", nIdx);
          else
               nCount = GetM2DAInt(nTable, "TalentID", nIdx);

          if (bInitial != EngineConstants.FALSE && IsHero(oChar) == EngineConstants.FALSE && GetCreatureCoreClass(oChar) != EngineConstants.CLASS_DOG && GetCreatureCoreClass(oChar) != EngineConstants.CLASS_ROGUE) // rogues get Dirty Fighting in addition
               nPoints++; // give the follower 1 extra point to match the bonus the player gets at level 1 based on origin

          int nPackage = GetPackage(oChar);
          if (nPackage == 91) // mouse
               nPoints = 2; // the 2 talents are added manually

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "Trying to spend " + ToString(nPoints) + " Talent Points, nCount= " + IntToString(nCount));

          for (nIdx = 1; nIdx < nCount && nPoints > 0; nIdx++)
          {
               int nTalent;
               if (bInitial != EngineConstants.FALSE)
                    nTalent = GetM2DAInt(nTable, "InitialTalentID", nIdx);
               else
                    nTalent = GetM2DAInt(nTable, "TalentID", nIdx);

               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "trying to add talent: " + IntToString(nTalent));
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "has ability= " + IntToString(HasAbility(oChar, nTalent)));

               if (HasAbility(oChar, nTalent) == EngineConstants.FALSE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "boom1");

                    if (IsAbilityAvailable(oChar, nTalent) != EngineConstants.FALSE || bInitial != EngineConstants.FALSE)
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "boom2");
                         AddAbility(oChar, nTalent);
                         SetQuickslot(oChar, -1, nTalent);
                         nPoints--;
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "Taking " + Log_GetAbilityNameById(nTalent) + "!");
                    }
                    else
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "-- Not Taking " + Log_GetAbilityNameById(nTalent) + " -  engine says: unavailable");
                    }
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "-- Not Taking " + Log_GetAbilityNameById(nTalent) + " -  already have it");
               }
          }

          // Resync Property
          SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS, IntToFloat(nPoints));

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "------------- AutoLevelUp Talent Summary ---------------------");
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "Remaining Points: " + ToString(nPoints));
          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendTalentSpellPoints", "------------- AutoLevelUp Talent Summary ---------------------");

     }

     public int _GetPointsToSpendOnAttribute(GameObject oChar, int nAttribute, int nPointsAvailable, int nTable)
     {

          float fCurrentValue = GetCreatureProperty(oChar, nAttribute, EngineConstants.PROPERTY_VALUE_BASE /*only consider non buffed values*/);
          float fTargetWeight = GetM2DAFloat(nTable, "AttWeight", nAttribute);
          int nLevel = GetLevel(oChar);
          float fTargetValue = 14.0f + (nLevel * fTargetWeight);

          float fRet = 0.0f;

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:_GetPointsToSpendOnAttribute", "Attribute " + ToString(nAttribute) + " fCur: " + ToString(fCurrentValue) + " fTarget:" + ToString(fTargetValue));

          if ((fCurrentValue < fTargetValue) && nPointsAvailable > 0)
          {
               fRet = MinF(IntToFloat(nPointsAvailable), MaxF(1.0f, fTargetValue - fCurrentValue));
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:_GetPointsToSpendOnAttribute", "Attribute " + ToString(nAttribute) + " Points Avail: " + ToString(nPointsAvailable) + " : " + ToString(fRet));

          return ((fRet >= 1.0f) ? FloatToInt(fRet) : 0);
     }

     public void AL_SpendAttributePoints(GameObject oChar, int nTable, int bChargenDefault)
     {
          int i;

          if (bChargenDefault != EngineConstants.FALSE) // giving attributes for level 1
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendAttributePoints", "Trying to spend points based on constant 2da values");
               for (i = 1; i <= 6; i++)
               {
                    int nSpend = GetM2DAInt(nTable, "AttInit", i);
                    float fCur = GetCreatureProperty(oChar, i, EngineConstants.PROPERTY_VALUE_BASE);
                    fCur += IntToFloat(nSpend);
                    Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendAttributePoints", "Spent: " + ToString(nSpend) + " on " + ToString(i));

                    SetCreatureProperty(oChar, i, fCur, EngineConstants.PROPERTY_VALUE_BASE);
               }
               SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS, 0.0f);

          }
          else
          {
               int nPoints = FloatToInt(GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS));

               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendAttributePoints", "Trying to spend: " + ToString(nPoints));

               for (i = 1; i <= 6 && nPoints > 0; i++)
               {

                    int nSpend = Min(nPoints, _GetPointsToSpendOnAttribute(oChar, i, nPoints, nTable));

                    if (nSpend > 0)
                    {
                         float fCur = GetCreatureProperty(oChar, i, EngineConstants.PROPERTY_VALUE_BASE);
                         fCur += IntToFloat(nSpend);
                         nPoints -= nSpend;
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendAttributePoints", "Spent: " + ToString(nSpend) + " on " + ToString(i));

                         SetCreatureProperty(oChar, i, fCur, EngineConstants.PROPERTY_VALUE_BASE);
                    }
                    else
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendAttributePoints", "Not Spending on " + ToString(i));
                    }

               }

               SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS, IntToFloat(nPoints));

               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendAttributePoints", "------------- AutoLevelUp Att Summary ---------------------");
               Log_Trace(EngineConstants.LOG_CHANNEL_AUTOBALANCE, "AL:AL_SpendAttributePoints", "Remaining Points: " + ToString(nPoints));
          }

     }

     public void AL_DoAutoLevelUp(GameObject oChar, int bInitial = EngineConstants.FALSE, int bChargenDefault = EngineConstants.FALSE)
     {
          int nTable = _GetTableToUseForAL(oChar);

          AL_SpendAttributePoints(oChar, nTable, bChargenDefault);
          AL_SpendSkillPoints(oChar, nTable, bInitial);
          AL_SpendSpecializationPoints(oChar, nTable);
          AL_SpendTalentSpellPoints(oChar, nTable, bInitial);

          // -------------------------------------------------------------------------
          // Update various UIs
          // -------------------------------------------------------------------------
          Chargen_SetNumTactics(oChar);
          SetCanLevelUp(oChar, Chargen_HasPointsToSpend(oChar));
     }
}