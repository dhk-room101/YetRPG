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
     // core_difficulty_h
     // -----------------------------------------------------------------------------
     /*
         game difficulty scaling

         as a core file, this CANNOT include any other files
     */
     // -----------------------------------------------------------------------------
     // owner: noel borstad
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"

     /* @brief Returns the appropriate autoscale table to use based on current difficulty
     *
     *   Looks up which autoscale table to use based on the current difficulty setting
     * @author Noel
     **/
     public int Diff_GetAutoScaleTable()
     {
          return EngineConstants.TABLE_AUTOSCALE;/*  EngineConstants.TABLE_AUtos GetM2DAInt(EngineConstants.TABLE_DIFFICULTY, "AUTOSCALE", GetGameDifficulty());*/
     }

     /* @brief Returns an attack modifier for enemies who are flanking a party character
     *
     *   Allows us to increase the importance of character positioning in combat
     * @author Noel
     **/
     public float Diff_GetEnemyFlankModifier()
     {
          return GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "ENEMYFLANKBONUS", GetGameDifficulty());
     }

     public float Diff_GetDurationModifier(GameObject oCreature)
     {
          int nRank = GetCreatureRank(oCreature);
          string sColumn = "fEffectDur";
          sColumn += GetM2DAString(EngineConstants.TABLE_DIFFICULTY, "Suffix", GetGameDifficulty());

          float fRet = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, sColumn, nRank);
          if (fRet > 0.0f)
          {
               return fRet;
          }
          return 1.0f;
     }

     public float Diff_GetRulesAttackBonus(GameObject oAttacker)
     {
          if (GetCreatureRank(oAttacker) != EngineConstants.CREATURE_RANK_PLAYER)
          {
               return 0.0f;
          }

          return GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "AttackBonus", GetGameDifficulty());

     }

     public float Diff_GetRulesDefenseBonus(GameObject oCreature)
     {
          if (GetCreatureRank(oCreature) != EngineConstants.CREATURE_RANK_PLAYER)
          {
               return 0.0f;
          }

          return GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "DefenseBonus", GetGameDifficulty());

     }

     public float Diff_GetRulesDamageBonus(GameObject oCreature)
     {
          if (GetCreatureRank(oCreature) != EngineConstants.CREATURE_RANK_PLAYER)
          {
               return 0.0f;
          }

          return GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "DamageBonus", GetGameDifficulty());

     }

     public float Diff_GetRulesHealingModifier(GameObject oCreature)
     {
          if (GetCreatureRank(oCreature) != EngineConstants.CREATURE_RANK_PLAYER)
          {
               return 1.0f;
          }

          float fRet = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "HealingModifier", GetGameDifficulty());

          if (fRet > 0.0f)
          {
               return fRet;
          }
          else /* outdated strings */
          {
               return 1.0f;
          }

     }

     public float GetDamageScalingThreshold()
     {
          float fRet = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "DmgScalingThresh", GetGameDifficulty());

          if (fRet > 0.0f)
          {
               return fRet;
          }
          else /* outdated strings */
          {
               return 5.0f;
          }
     }

     /*
         @brief  Gets the spell resistance runtime modifiers based on game difficulty
         @author georg
     */
     public float Diff_GetSRMod(GameObject oCreature)
     {
          int nRank = GetCreatureRank(oCreature);
          if (nRank == EngineConstants.CREATURE_RANK_PLAYER)
          {
               float fMod = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "SRModPlayer", GetGameDifficulty());
               return fMod;
          }

          // -------------------------------------------------------------------------
          // Base is defined in difficulties.xls and modified per rank
          // -------------------------------------------------------------------------
          float fBase = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "SRMod", GetGameDifficulty());
          float fRank = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, "BaseSR", nRank);

          return fBase + fRank;

     }

     /*
         @brief  Gets the damage resistance runtime modifiers based on game difficulty
         @author georg
     */
     public float Diff_GetDRMod(GameObject oCreature)
     {
          int nRank = GetCreatureRank(oCreature);
          if (nRank == EngineConstants.CREATURE_RANK_PLAYER)
          {
               float fMod = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "DRModPlayer", GetGameDifficulty());
               return fMod;
          }

          // -------------------------------------------------------------------------
          // Base is defined in difficulties.xls and modified per rank
          // -------------------------------------------------------------------------
          float fBase = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "DRMod", GetGameDifficulty());
          float fRank = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, "BaseDR", nRank);

          return fBase + fRank;

     }

     //This function is duplicated within the game executable. Any change made to this function will 
     //result in GUI glitches and other bugs. Sorry.
     public float Diff_GetAbilityUseMod(GameObject oCreature)
     {
          int nRank = GetCreatureRank(oCreature);
          if (nRank == EngineConstants.CREATURE_RANK_PLAYER || nRank == EngineConstants.CREATURE_RANK_CRITTER || nRank == EngineConstants.CREATURE_RANK_WEAK_NORMAL)
          {
               return 1.0f;
          }

          // --------------------------------------------------------------------------
          // Up to level 3, we don't enforce this rule on HARD
          // --------------------------------------------------------------------------
          float fLevel = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_LEVEL);
          if (fLevel <= 3.0f || GetGameDifficulty() <= 2)
          {
               return 1.0f;
          }

          float fBase = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "AIAbilityUseMod", GetGameDifficulty());

          // capping at 0.5f, just in case someone adds bad 2da data
          if (fBase > 0.75)
          {
               return fBase;
          }
          return 0.75f;
     }

     /*-----------------------------------------------------------------------------
     * @brief Returns modifier for scaling trap damage to game difficulty.
     *-----------------------------------------------------------------------------*/
     public float Diff_GetTrapDamageModifier()
     {
          float fMod = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "TrapDmgScale", GetGameDifficulty());
          return (fMod > 0.0f) ? fMod : 1.0f;
     }
}