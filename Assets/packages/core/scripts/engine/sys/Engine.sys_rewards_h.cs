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
     // sys_rewards
     // -----------------------------------------------------------------------------
     /*
         reward system

         the reward system provides the central wrapper for granting EngineConstants.XP, Money
         and other rewards.

     */
     // -----------------------------------------------------------------------------
     // owner: georg zoeller (EngineConstants.XP), Peter Thomas (economy)
     // -----------------------------------------------------------------------------

     //#include"ui_h"
     //#include"log_h"
     //#include"2da_constants_h"
     //#include"sys_autoscale_h"
     //#include"events_h"
     //#include"tutorials_h"
     //#include"plt_tut_specialty_class"
     ////#include"achievement_core_h"

     //moved public const int       EngineConstants.XP_TYPE_COMBAT             = 1;
     //moved public const int       EngineConstants.XP_TYPE_PLOT               = 2;
     //moved public const int       EngineConstants.XP_TYPE_EXPLORE            = 3;
     //moved public const int       EngineConstants.XP_TYPE_CODEX              = 4;

     //moved public const int       EngineConstants.REWARDS_2DA_ITEM_MAX    = 6;
     //moved public const string    EngineConstants.REWARDS_2DA_ITEM_PREFIX = "Item";
     //moved public const string    EngineConstants.REWARDS_2DA_COPPER      = "Copper";
     //moved public const string    EngineConstants.REWARDS_2DA_SILVER      = "Silver";
     //moved public const string    EngineConstants.REWARDS_2DA_GOLD        = "Gold";
     //moved public const string    EngineConstants.REWARDS_2DA_XP          = "XP";
     public float RewardGetXPValue(GameObject oObject)
     {

          if (GetObjectType(oObject) == EngineConstants.OBJECT_TYPE_CREATURE)
          {

               float fPoints = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, "fPointVal", GetCreatureRank(oObject));
               float fPointVal = GetM2DAFloat(EngineConstants.TABLE_EXPERIENCE, "fPointVal", GetLevel(oObject));

               Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardGetXPValue",
                   "fPointVal (" + ToString(oObject) + ") = " + ToString(fPoints) + " fPointVal: " + ToString(fPointVal));

               return fPoints * fPointVal * 0.2875f; // should be half the weigth of total xp
                                                     // EDIT (yaron, jan 29 2009): creature EngineConstants.XP is now half of total + 15% (0.25 * 15%)
                                                     // EDIT (yaron, feb 17, 2009): removing the 15% bonus. Light content seem to have bumped up the total EngineConstants.XP (was 0.2875 before this change)
                                                     // EDIT (yaron, feb 19, 2009): readding 15% bonus (no level limit)

               //return IntToFloat(GetM2DAInt(EngineConstants.TABLE_CREATURERANKS, "tempxp", GetCreatureRank(oObject)));

          }

          return 5.0f;
     }

     /*
         @brief Give EngineConstants.XP to a party member.

         Single point of entry for granting EngineConstants.XP in the game.
         All EngineConstants.XP goes through this function!

         @author: Georg

     */
     public void RewardXP(GameObject oPartyMember, int nXP, int bNotify = EngineConstants.TRUE, int bShowLevelupVFX = EngineConstants.TRUE)
     {

          if (GetLocalInt(oPartyMember, EngineConstants.CREATURE_REWARD_FLAGS) != 0)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardXP", "Not giving rewards to creauture due to NO EngineConstants.REWARD flag on it.  " + ToString(oPartyMember));
               return;
          }

          if (GetFollowerState(oPartyMember) != EngineConstants.FOLLOWER_STATE_INVALID && nXP > 0) // is party member and xp>0
          {

               int nMaxXP = GetM2DAInt(EngineConstants.TABLE_EXPERIENCE, "XP", GetMaxLevel());
               int nCurrentXP = GetExperience(oPartyMember);

               // ---------------------------------------------------------------------
               // Support for the Preorder promo for the memory band item
               // ---------------------------------------------------------------------
               if (GetPRCEnabled("DAO_PRC_PROMO_1") != EngineConstants.FALSE)
               {
                    //FAB: 2/3 Tag added
                    string sTag = "gen_im_acc_rng_exp";
                    if (GetTag(GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_RING1, oPartyMember)) == sTag || GetTag(GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_RING2, oPartyMember)) == sTag)
                    {
                         // -------------------------------------------------------------
                         // Memory band adds 1% EngineConstants.XP.
                         // -------------------------------------------------------------
                         nXP += FloatToInt(nXP * 0.01f);
                    }
               }

               if (nCurrentXP < nMaxXP)
               {
                    int nNew = Min(nCurrentXP + nXP, nMaxXP);
                    SetCreatureProperty(oPartyMember, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE, IntToFloat(nNew), EngineConstants.PROPERTY_VALUE_BASE);

                    if (bNotify != EngineConstants.FALSE && nNew > nCurrentXP)
                    {
                         UI_DisplayXPFloaty(oPartyMember, nNew - nCurrentXP);
                    }

                    if (Chargen_CheckCanLevelUp(oPartyMember) != EngineConstants.FALSE)
                    {

                         // -----------------------------------------------------------------
                         //
                         // -----------------------------------------------------------------
                         if (GetHasEffects(oPartyMember, EngineConstants.EFFECT_TYPE_SHAPECHANGE) == EngineConstants.FALSE)
                         {
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardXP", "Setting Ready to Levelup " + ToString(oPartyMember));
#endif

                              Levelup_SetReadyToLevelUp(oPartyMember, bShowLevelupVFX);

                              //   SendLevelUpEvent(oPartyMember, (GetLevel(oPartyMember) +1));

                              //Specialty Class Tutorial Call
                              if (FloatToInt(GetCreatureProperty(oPartyMember, 38)) > 0)
                              {
                                   WR_SetPlotFlag(EngineConstants.PLT_TUT_SPECIALTY_CLASS, EngineConstants.TUT_SPECIALTY_CLASS, EngineConstants.TRUE);
                              }
                              //Levelup Tutorial Call
                              if (GetLevel(oPartyMember) > 0 && IsHero(oPartyMember) != EngineConstants.FALSE)
                              {
                                   //**OLD EngineConstants.TUTORIAL***
                                   //WR_SetPlotFlag(EngineConstants.PLT_TUT_LEVELUP, EngineConstants.TUT_LEVELUP_1, EngineConstants.TRUE);

                                   GameObject oModule = GetModule();

                                   //if we're in explore mode - we can show the tutorial
                                   //don't show if chargen has been skipped
                                   if (ReadIniEntry("DebugOptions", "SkipCharGen") == "1")
                                   {
                                        //do nothing
                                   }
                                   else
                                   {
                                        //skip if we've vever seen the tutorial before
                                        if (GetLocalInt(oModule, "TUTORIAL_HAVE_SEEN_LEVEL_UP") < 1)
                                        {
                                             //skip tutorial if pc is on autolevelup
                                             if (GetAutoLevelUp(GetHero()) == EngineConstants.FALSE)
                                             {
                                                  if (GetGameMode() == EngineConstants.GM_EXPLORE)
                                                  {
                                                       BeginTrainingMode(EngineConstants.TRAINING_SESSION_LEVEL_UP);
                                                       //set the module variable to show tutorial has been seen
                                                       SetLocalInt(oModule, "TUTORIAL_HAVE_SEEN_LEVEL_UP", 2);
                                                  }
                                                  else
                                                  {
                                                       //if we're not in explore mode - cue up the module tutorial variable so it
                                                       //fires next time we are
                                                       SetLocalInt(oModule, "TUTORIAL_HAVE_SEEN_LEVEL_UP", 1);
                                                  }
                                             }
                                             else
                                             {
                                                  //set the module variable to show tutorial has been seen
                                                  SetLocalInt(oModule, "TUTORIAL_HAVE_SEEN_LEVEL_UP", 2);
                                             }
                                        }
                                   }
                              }
                         }
                         else
                         {
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardXP", "Levelup delayed - can not levelup while shapeshifted  " + ToString(oPartyMember));
#endif
                         }
                    }

                    SetCanLevelUp(oPartyMember, Chargen_HasPointsToSpend(oPartyMember));

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardXP", "Reward: " + ToString(nXP) + " EngineConstants.XP", oPartyMember);
#endif
               }
          }

     }

     public void RewardXPParty(int nXP = 0, int nXPType = EngineConstants.XP_TYPE_COMBAT, GameObject oTarget = null, GameObject oSource = null)
     {

          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardXPParty:", "Reward: " + ToString(nXP) + " EngineConstants.XP");
          List<GameObject> aParty = GetPartyList();

          if (nXPType == EngineConstants.XP_TYPE_COMBAT)
          {

               if (IsObjectValid(oTarget) != EngineConstants.FALSE)
               {
                    // GXA
                    if (GetName(GetModule()) == "DAO_PRC_EP_1")
                    {
                         nXP = FloatToInt(RewardGetXPValue(oTarget) * 2.5f); // slush factor
                    }
                    // GXA
                    else
                    {
                         nXP = FloatToInt(RewardGetXPValue(oTarget));
                    }

                    Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardXPParty", "Reward2: " + ToString(nXP) + " EngineConstants.XP" + ToString(GetLevel(oTarget)) + " " + ToString(GetLevel(oSource)));

               }
          }
          else if (nXPType == EngineConstants.XP_TYPE_PLOT)
          {

          }
          else if (nXPType == EngineConstants.XP_TYPE_CODEX)
          {

          }

          if (nXP > 0)
          {
               int nSize = GetArraySize(aParty);
               Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardXPParty", "nSize: " + ToString(nSize));

               int i;

               for (i = 0; i < nSize; i++)
               {

                    int nEffectiveXP = nXP;

                    Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardXPParty", ToString(aParty[i]) + " " + ToString(nEffectiveXP));

                    if (nXPType == EngineConstants.XP_TYPE_COMBAT)
                    {
                         // less xp for dead people and party memebers in camp
                         if (IsDead(aParty[i]) != EngineConstants.FALSE)
                         {
                              nEffectiveXP = FloatToInt(nEffectiveXP * 0.9f);
                         }
                         else
                         {
                              nEffectiveXP = (oSource == aParty[i]) ? nEffectiveXP : FloatToInt(nEffectiveXP * 0.95f);
                         }
                    }
                    else if (nXPType == EngineConstants.XP_TYPE_CODEX)
                    {
                         // -------------------------------------------------------------
                         // Parse for any reward bonus effects of type EngineConstants.EFFECT_REWARD_BONUS_TYPE_CODEX
                         //
                         // -------------------------------------------------------------
                         List<xEffect> aRewardBoni = GetEffects(aParty[i], EngineConstants.EFFECT_TYPE_REWARD_BONUS);
                         int nEffects = GetArraySize(aRewardBoni);
                         int j;
                         for (j = 0; j < nEffects; j++)
                         {
                              xEffect _effect = aRewardBoni[j];
                              if (GetEffectIntegerRef(ref _effect, EngineConstants.EFFECT_REWARD_BONUS_FIELD_TYPE) == EngineConstants.EFFECT_REWARD_BONUS_TYPE_CODEX)
                              {
                                   nEffectiveXP += GetEffectIntegerRef(ref _effect, EngineConstants.EFFECT_REWARD_BONUS_FIELD_MAGNITUDE);
                              }
                         }

                    }

                    // minimum xp is 1
                    if (nEffectiveXP < 1)
                    {
                         nEffectiveXP = 1;
                    }

                    RewardXP(aParty[i], nEffectiveXP, (aParty[i] == oSource) ? EngineConstants.TRUE : EngineConstants.FALSE);
               }

          }
     }

     public void RewardMoney(int nCopper, int nSilver = 0, int nGold = 0)
     {
          GameObject oHero = GetHero();
          int nMoney = nCopper + (100 * nSilver) + (100 * 100 * nGold);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardMoney", "Reward: " +
                  ToString(nCopper) + " Copper, " +
                  ToString(nSilver) + " Silver, " +
                  ToString(nGold) + " Gold ", oHero);
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardMoney", "Total Reward = " + ToString(nMoney));
#endif

          AddCreatureMoney(nMoney, oHero, EngineConstants.TRUE);
     }

     //moved public const int RW_REWARD_ITEM_LEVEL_MODIFIER = 3;

     public void RewardItem(string rItem, int nNumToReward = 1)
     {
          GameObject oHero = GetHero();
          GameObject oItem = CreateItemOnObject(rItem, oHero, nNumToReward);

          int nLevel = GetLevel(oHero);
          if (GetLocalInt(GetArea(oHero), "INJURY_FLAG") == EngineConstants.FALSE)
          {
               nLevel += EngineConstants.RW_REWARD_ITEM_LEVEL_MODIFIER;
          }
          RW_ScaleItem(oItem, nLevel);

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.RewardItem", "Reward: " +
              ResourceToString(rItem) + " [x" + ToString(nNumToReward) + "]", oHero);
#endif
     }

     /* @brief Grants a bonus ability point to the player. Used by books and plot events.
     *
     * @param nType: 1- talent/spell, 2- skill, 3-skill for rogues/talent for everyone else, 4-attribute points
     * @author georg
     */
     public void RW_GrantHeroBonusAbilityPoint(int nType, float fPoints = 1.0f, GameObject oTarget = null)
     {
          if (IsObjectValid(oTarget) == EngineConstants.FALSE)
          {
               oTarget = GetHero();
          }

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "RW_GrantHeroBonusAbility", "Bonus ability point of type " + ToString(nType) + " granted to " + ToString(oTarget));
#endif
          if (nType == 1 || (nType == 3 && GetCreatureCoreClass(oTarget) != EngineConstants.CLASS_ROGUE))
          {
               float fCurrentPoints = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS) + fPoints;
               SetCreatureProperty(oTarget, EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS, fCurrentPoints);
          }
          else if (nType == 2 || (nType == 3 && GetCreatureCoreClass(oTarget) == EngineConstants.CLASS_ROGUE))
          {
               float fCurrentPoints = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS) + fPoints;
               SetCreatureProperty(oTarget, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS, fCurrentPoints);
          }
          else if (nType == 4)
          {
               float fCurrentPoints = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS) + fPoints;
               SetCreatureProperty(oTarget, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS, fCurrentPoints);
          }

          // -------------------------------------------------------------------------
          // Show 'levelup' icon.
          // -------------------------------------------------------------------------
          SetCanLevelUp(oTarget, Chargen_HasPointsToSpend(oTarget));
     }

     /* @brief Handles rewards for plot events
     *
     * This function handles any rewards regarding plots in the game.
     * This includes rewards of money, xp, and items.
     *
     * @param sPlot the plot containing the reward plot flag
     * @param nFlag the plot flag containing the reward
     * @author joshua
     */
     public void RewardDistibuteByPlotFlag(string sPlot, int nFlag)
     {

          GameObject oHero = GetHero();
          int nRewardId = GetPlotFlagRewardId(sPlot, nFlag);

          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_rewards_h:RewardDistibuteByPlotFlag",
                      "plot: " + sPlot + ", flag: " + IntToString(nFlag) + ", reward id: " + IntToString(nRewardId));

          if (nRewardId <= 0)
               return;

          string sRewardItem; // current item 2DA string label
          string rItem;     // current item 2DA string
          int nIndex;

          int nCopper = GetM2DAInt(EngineConstants.TABLE_REWARDS, EngineConstants.REWARDS_2DA_COPPER, nRewardId);
          int nSilver = GetM2DAInt(EngineConstants.TABLE_REWARDS, EngineConstants.REWARDS_2DA_SILVER, nRewardId);
          int nGold = GetM2DAInt(EngineConstants.TABLE_REWARDS, EngineConstants.REWARDS_2DA_GOLD, nRewardId);
          int nXP = GetM2DAInt(EngineConstants.TABLE_REWARDS, EngineConstants.REWARDS_2DA_XP, nRewardId);

          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_rewards_h:RewardDistibuteByPlotFlag",
                     "XP: " + IntToString(nXP));

          int nBonusAbilityType = GetM2DAInt(EngineConstants.TABLE_REWARDS, "BonusAbility", nRewardId);
          if (nBonusAbilityType > 0)
          {
               RW_GrantHeroBonusAbilityPoint(nBonusAbilityType);
          }

          // reward Money
          RewardMoney(nCopper, nSilver, nGold);

          // reward EngineConstants.XP
          nXP = FloatToInt(nXP * 0.5f); // half of total EngineConstants.XP reward
          RewardXPParty(nXP, EngineConstants.XP_TYPE_PLOT);

          // loop through reward Items and award all that exist
          for (nIndex = 1; nIndex <= EngineConstants.REWARDS_2DA_ITEM_MAX; nIndex++)
          {

               sRewardItem = EngineConstants.REWARDS_2DA_ITEM_PREFIX + ToString(nIndex);
               rItem = GetM2DAResource(EngineConstants.TABLE_REWARDS, sRewardItem, nRewardId);
               if (rItem == EngineConstants.INVALID_RESOURCE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_rewards_h:RewardDistibuteByPlotFlag",
                        "Reward " + sRewardItem + ": invalid", oHero);
                    break;
               }
               RewardItem(rItem);
          }

     }

     /*
       @author joshua
     */
     /* @brief Returns EngineConstants.TRUE if the GameObject has at least the amount of money specified.
     *
     *   You may specify the total amount of copper, or you can specify the
     *   amount of copper, silver, and gold and the function will convert it.
     * @param oTarg - The GameObject to check.
     * @param nCopper - Amount of copper to check for.
     * @param nSilver - Amount of silver to check for.
     * @param nGold - Amount of gold to check for.
     * @author Jonathan
     **/
     public int UT_MoneyCheck(GameObject oTarg, int nCopper, int nSilver = 0, int nGold = 0)
     {
          int nResult = EngineConstants.FALSE;

          // Calculate total amount of money to check
          // 1 gold = 100 silver = 10000 copper
          int nTotal = nCopper + (nSilver * 100) + (nGold * 10000);

          int nCurrentMoney = GetCreatureMoney(oTarg);
          if (nCurrentMoney >= nTotal)
               nResult = EngineConstants.TRUE;

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.UT_MoneyCheck", "Has: " +
              ToString(nCurrentMoney) + "; Needs: " + ToString(nTotal) + "; Result=" + (nResult != EngineConstants.FALSE ? "EngineConstants.TRUE" : "EngineConstants.FALSE"), oTarg);
#endif

          return nResult;
     }

     /*
       @author joshua
     */
     /* @brief Takes the specified amount of money from the target object.
     *
     *   You may specify the total amount of copper, or you can specify the
     *   amount of copper, silver, and gold and the function will convert it.
     * @param oTarg - The GameObject to take the money from.
     * @param nCopper - Amount of copper to take.
     * @param nSilver - Amount of silver to take.
     * @param nGold - Amount of gold to take.
     * @author Jonathan
     **/
     public void UT_MoneyTakeFromObject(GameObject oTarg, int nCopper, int nSilver = 0, int nGold = 0)
     {
          // Calculate total amount of money
          // 1 gold = 100 silver = 10000 copper
          int nTotal = nCopper + (nSilver * 100) + (nGold * 10000);
          int nHasEnoughMoney = UT_MoneyCheck(oTarg, nTotal);

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, "sys_reward_h.UT_MoneyTakeFromObject",
              "Removing Money: " + ToString(nTotal), oTarg);
#endif

          AddCreatureMoney(-1 * nTotal, oTarg, EngineConstants.TRUE);
     }

     //moved public const int SPEC_WIZARD_SHAPESHIFTER = 11;
     //moved public const int SPEC_WIZARD_SPIRITHEALER = 12;
     //moved public const int SPEC_WIZARD_ARCANE_WARRIOR = 13;
     //moved public const int SPEC_WIZARD_BLOOD_MAGE     = 14;

     //moved public const int SPEC_WARRIOR_CHAMPION = 7;
     //moved public const int SPEC_WARRIOR_TEMPLAR = 8;
     //moved public const int SPEC_WARRIOR_BERSERKER = 9;
     //moved public const int SPEC_WARRIOR_REAVER = 10;

     //moved public const int SPEC_ROGUE_ASSASSIN = 15;
     //moved public const int SPEC_ROGUE_BARD = 16;
     //moved public const int SPEC_ROGUE_RANGER   = 17;
     //moved public const int SPEC_ROGUE_DUELIST  = 18;

     /*
         @brief  - Unlocks a specialization trainer.
         @param  nSpecialization - A SPEC_* constant

         @author Georg
     */
     public void RW_UnlockSpecializationTrainer(int nSpecialization)
     {
          string acvId = GetM2DAString(EngineConstants.TABLE_ACHIEVEMENTS, "AchievementID", nSpecialization);
          UnlockAchievement(acvId);

          int nClass = GetM2DAInt(EngineConstants.TABLE_ACHIEVEMENTS, "Class", nSpecialization);

          ShowSpecUnlockedNotification(nClass);
          //WR_UnlockAchievement(nSpecialization, EngineConstants.FALSE, EngineConstants.TRUE);

     }

     /*
         @brief  - Checks if the player has a specific specialization
         @param  nSpecialization - A SPEC_* constant
         @returns EngineConstants.TRUE if the player has it, EngineConstants.FALSE otherwise

         @author Georg
     */
     public int RW_HasSpecialization(int nSpecialization)
     {

          string acvId = GetM2DAString(EngineConstants.TABLE_ACHIEVEMENTS, "AchievementID", nSpecialization);
          return GetHasAchievement(acvId);
     }

     public int RW_GetXPNeededForLevel(int nLevel)
     {
          int nXP = GetM2DAInt(EngineConstants.TABLE_EXPERIENCE, "XP", nLevel);
          return nXP;
     }

     /*
         @brief  - Causes a follower to catch up the player in terms of EngineConstants.XP.
                   by rewarding enough EngineConstants.XP to stay 1 level below the player.

         @author Georg
     */
     public void RW_CatchUpToPlayer(GameObject oPartyMember)
     {
          int nCurrent = GetExperience(oPartyMember);
          int nTarget = RW_GetXPNeededForLevel(Max(1, GetLevel(GetHero()) - 1));

          if (nCurrent < nTarget)
          {

               RewardXP(oPartyMember, nTarget - nCurrent, EngineConstants.FALSE);
          }

     }

     //moved public const int RW_MATERIAL_COLUMN_MIN = 1;
     //moved public const int RW_MATERIAL_COLUMN_MAX = 15;
     //moved public const int RW_LEVEL_MIN = 1;
     //moved public const int RW_LEVEL_MAX = 45;

     // scale the material of an item
     public void RW_ScaleItem(GameObject oItem, int nLevel)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "    Scaling " + GetTag(oItem));
#endif

          // if appropriate type (armor, shield, melee weapon, ranged weapon)
          int nItemType = GetItemType(oItem);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "      nItemType = " + ToString(nItemType));
#endif
          if ((nItemType == EngineConstants.ITEM_TYPE_ARMOUR) || (nItemType == EngineConstants.ITEM_TYPE_SHIELD) || (nItemType == EngineConstants.ITEM_TYPE_WEAPON_MELEE) || (nItemType == EngineConstants.ITEM_TYPE_WEAPON_RANGED))
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "      Item is scalable.");
#endif

               // if not unique
               if (GetItemUnique(oItem) == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "      Item is not unique.");
#endif

                    // get material progression
                    int nMaterialProgression = GetItemMaterialProgression(oItem);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "      nMaterialProgression = " + ToString(nMaterialProgression));
#endif
                    if (nMaterialProgression > 0)
                    {
                         nLevel = Max(EngineConstants.RW_LEVEL_MIN, nLevel);
                         nLevel = Min(EngineConstants.RW_LEVEL_MAX, nLevel);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "      Final nLevel = " + ToString(nLevel));
#endif

                         // find material column
                         int nColumn = ((nLevel - 1) / 3) + 1;
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "      nColumn = " + ToString(nColumn));
#endif

                         nColumn = Max(EngineConstants.RW_MATERIAL_COLUMN_MIN, nColumn);
                         nColumn = Min(EngineConstants.RW_MATERIAL_COLUMN_MAX, nColumn);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "      Min-Max nColumn = " + ToString(nColumn));
#endif

                         // get material
                         int nMaterial = GetM2DAInt(EngineConstants.TABLE_MATERIAL, "Material" + ToString(nColumn), nMaterialProgression);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_REWARDS, GetCurrentScriptName(), "      nMaterial = " + ToString(nMaterial));
#endif

                         // set material
                         SetItemMaterialType(oItem, nMaterial);
                    }
               }
          }
     }
}