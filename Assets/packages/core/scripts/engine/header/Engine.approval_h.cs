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
     // approval_h

     /* @addtogroup scripting_approval Scripting Approval
     *
     * These are the main system's functions - used mostly to handle approval rating in dialogs or by item gifting
     */
     /* @{*/

     //#include"plt_genpt_app_alistair"
     //#include"plt_genpt_app_dog"
     //#include"plt_genpt_app_leliana"
     //#include"plt_genpt_app_loghain"
     //#include"plt_genpt_app_morrigan"
     //#include"plt_genpt_app_oghren"
     //#include"plt_genpt_app_shale"
     //#include"plt_genpt_app_sten"
     //#include"plt_genpt_app_wynne"
     //#include"plt_genpt_app_zevran"
     //#include"2da_constants_h"
     //#include"global_objects_h"
     //#include"log_h"
     //#include"wrappers_h"
     //#include"ui_h"
     //#include"party_h"
     //#include"plt_tut_approval_warm"

     // Follower constants - any follower that can be affected by approval
     //moved public const int EngineConstants.APP_FOLLOWER_ALISTAIR = 1;
     //moved public const int EngineConstants.APP_FOLLOWER_DOG = 2;
     //moved public const int EngineConstants.APP_FOLLOWER_MORRIGAN = 3;
     //moved public const int EngineConstants.APP_FOLLOWER_WYNNE = 4;
     //moved public const int EngineConstants.APP_FOLLOWER_SHALE = 5;
     //moved public const int EngineConstants.APP_FOLLOWER_STEN = 6;
     //moved public const int EngineConstants.APP_FOLLOWER_ZEVRAN = 7;
     //moved public const int EngineConstants.APP_FOLLOWER_OGHREN = 8;
     //moved public const int EngineConstants.APP_FOLLOWER_LELIANA = 9;
     //moved public const int EngineConstants.APP_FOLLOWER_LOGHAIN = 10;

     // Approval bonus ranges
     //moved public const int EngineConstants.APP_BONUS_1 = 25;
     //moved public const int EngineConstants.APP_BONUS_2 = 50;
     //moved public const int EngineConstants.APP_BONUS_3 = 75;
     //moved public const int EngineConstants.APP_BONUS_4 = 90;

     // bonuses:
     // Alistair: Constitution
     // Dog: Dexterity
     // Sten: Strength
     // Leliana: Intelligence
     // Morrigan: Magic
     // Wynne: Will
     // Oghren: Constitution
     // Loghain: Strength
     // Zevran: Dexterity

     /******************************************************************************
     /*                        FUNCTION DEFINITIONS
     /*****************************************************************************/

     public GameObject Approval_GetFollowerObject(int nFollower)
     {
          string sFollowerTag = String.Empty;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: sFollowerTag = EngineConstants.GEN_FL_ALISTAIR; break;
               case EngineConstants.APP_FOLLOWER_DOG: sFollowerTag = EngineConstants.GEN_FL_DOG; break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: sFollowerTag = EngineConstants.GEN_FL_MORRIGAN; break;
               case EngineConstants.APP_FOLLOWER_WYNNE: sFollowerTag = EngineConstants.GEN_FL_WYNNE; break;
               case EngineConstants.APP_FOLLOWER_SHALE: sFollowerTag = EngineConstants.GEN_FL_SHALE; break;
               case EngineConstants.APP_FOLLOWER_STEN: sFollowerTag = EngineConstants.GEN_FL_STEN; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: sFollowerTag = EngineConstants.GEN_FL_ZEVRAN; break;
               case EngineConstants.APP_FOLLOWER_OGHREN: sFollowerTag = EngineConstants.GEN_FL_OGHREN; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: sFollowerTag = EngineConstants.GEN_FL_LELIANA; break;
               case EngineConstants.APP_FOLLOWER_LOGHAIN: sFollowerTag = EngineConstants.GEN_FL_LOGHAIN; break;
          }

          return Party_GetFollowerByTag(sFollowerTag);
     }

     public int Approval_CheckFollowerBonusAbility(int nFollower, int nBonusRank)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_CheckFollowerBonusAbility", "follower: " + IntToString(nFollower) + ", bonus rank: " + IntToString(nBonusRank));
          string sColumn = "bonus" + IntToString(nBonusRank);
          int nAbility = GetM2DAInt(EngineConstants.TABLE_APP_FOLLOWER_BONUSES, sColumn, nFollower);
          GameObject oFollower = Approval_GetFollowerObject(nFollower);
          return HasAbility(oFollower, nAbility);
     }

     public void Approval_AddFollowerBonusAbility(int nFollower, int nBonusRank)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_AddFollowerBonusAbility", "follower: " + IntToString(nFollower) + ", bonus rank: " + IntToString(nBonusRank));
          string sColumn = "bonus" + IntToString(nBonusRank);
          int nAbility = GetM2DAInt(EngineConstants.TABLE_APP_FOLLOWER_BONUSES, sColumn, nFollower);
          GameObject oFollower = Approval_GetFollowerObject(nFollower);
          if (HasAbility(oFollower, nAbility) == EngineConstants.FALSE)
          {
               AddAbility(oFollower, nAbility, EngineConstants.TRUE);
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_AddFollowerBonusAbility", "follower: " + GetTag(oFollower) + ", add ability: " + IntToString(nAbility));
          }

     }

     public void Approval_RemoveFollowerBonusAbility(int nFollower, int nBonusRank)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_AddFollowerBonusAbility", "follower: " + IntToString(nFollower) + ", bonus rank: " + IntToString(nBonusRank));
          string sColumn = "bonus" + IntToString(nBonusRank);
          int nAbility = GetM2DAInt(EngineConstants.TABLE_APP_FOLLOWER_BONUSES, sColumn, nFollower);
          GameObject oFollower = Approval_GetFollowerObject(nFollower);
          RemoveAbility(oFollower, nAbility);
     }

     /* @brief Initialize the approval system
*
* This includes reading the ranges values from the 2da level for each log system
*
* @author Yaron
*/
     public void Approval_Init()
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_Init", "##############################################");
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_Init", "APPROVAL SYSTEM INIT");
          SetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_CRISIS, Approval_GetNormalApprovalRangeValue(EngineConstants.APP_RANGE_CRISIS));
          SetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_HOSTILE, Approval_GetNormalApprovalRangeValue(EngineConstants.APP_RANGE_HOSTILE));
          SetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_NEUTRAL, Approval_GetNormalApprovalRangeValue(EngineConstants.APP_RANGE_NEUTRAL));
          SetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_WARM, Approval_GetNormalApprovalRangeValue(EngineConstants.APP_RANGE_WARM));
          SetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_FRIENDLY, Approval_GetNormalApprovalRangeValue(EngineConstants.APP_RANGE_FRIENDLY));
          SetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_INTERESTED, Approval_GetRomanceApprovalRangeValue(EngineConstants.APP_ROMANCE_RANGE_INTERESTED));
          SetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_CARE, Approval_GetRomanceApprovalRangeValue(EngineConstants.APP_ROMANCE_RANGE_CARE));
          SetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_ADORE, Approval_GetRomanceApprovalRangeValue(EngineConstants.APP_ROMANCE_RANGE_ADORE));
          SetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_LOVE, Approval_GetRomanceApprovalRangeValue(EngineConstants.APP_ROMANCE_RANGE_LOVE));
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_Init", "##############################################");
     }

     /* @brief Returns the approval rating of oFollower
*
* Returns the approval rating of nFollower
*
* @param nFollower the follower whose approval we want to get
* @returns the approval value of the follower
* @author Yaron
*/
     public int Approval_GetApproval(int nFollower)
     {
          //string sVar = Approval_GetFollowerApprovalVar(nFollower);
          GameObject oFollower = Approval_GetFollowerObject(nFollower);
          int nApproval = GetFollowerApproval(oFollower);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetApproval", "FOLLOWER: " + Approval_GetFollowerName(nFollower) + ", APPROVAL: " + IntToString(nApproval));
          return nApproval;
     }

     /* @brief Returns the approval range limit value for a certain approval range
*
* Returns the approval range limit value for a certain approval range.
* For exampme: the result for EngineConstants.APP_RANGE_HOSTILE should be -26.
*
* @param nRange EngineConstants.APP_RANGE_*** constant - the range we are checking the limit for
* @returns the approval value for a specific range
* @author Yaron
*/
     public int Approval_GetNormalApprovalRangeValue(int nRange)
     {
          int nRet = EngineConstants.APP_RANGE_INVALID;
          nRet = GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", nRange);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetNormalApprovalRangeValue", "Getting NORMAL range for range <"
              + IntToString(nRange) + "> : " + IntToString(nRet));
          return nRet;
     }

     /* @brief Returns the approval range limit value for a certain ROMANCE approval range
*
* Returns the approval range limit value for a certain ROMANCE approval range
*
* @param nRange EngineConstants.APP_ROMANCE_RANGE_*** constants - the range we are checking the limit for
* @returns the approval value for a specific range
* @author Yaron
*/
     public int Approval_GetRomanceApprovalRangeValue(int nRange)
     {
          int nRet = EngineConstants.APP_RANGE_INVALID;
          nRet = GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "Range", nRange);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetRomanceApprovalRangeValue", "Getting ROMANCE range for range <"
              + IntToString(nRange) + "> : " + IntToString(nRet));
          return nRet;
     }

     /* @brief Returns the approval rating range for the follower
*
* This function might return values in the 'romance' range - any code that uses this function should
* also check the 'romance active' flag.
*
* @param nFollower the follower whose approval range we check
* @returns the approval of the follower - return value can be EngineConstants.APP_RANGE_CRISIS, EngineConstants.APP_RANGE_HOSTILE etc�
* @author Yaron
*/
     public int Approval_GetApprovalRange(int nFollower)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetApprovalRange", "Getting approval range of follower: " + IntToString(nFollower));

          GameObject oFollower = Approval_GetFollowerObject(nFollower);
          int nApproval = GetFollowerApproval(oFollower);

          int nRange = EngineConstants.APP_RANGE_INVALID;
          int nCrisisRange = GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_CRISIS);
          int nHostileRange = GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_HOSTILE);
          int nNeutralRange = GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_NEUTRAL);
          int nWarmRange = GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_WARM);
          int nFriendlyRange = GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_FRIENDLY);
          int nInterestedRange = GetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_INTERESTED);
          int nCareRange = GetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_CARE);
          int nAdoreRange = GetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_ADORE);
          int nLoveRange = GetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_LOVE);

          if (Approval_GetRomanceActive(nFollower) == EngineConstants.FALSE) // no romance - normal table
          {
               if (nApproval == nCrisisRange) nRange = nCrisisRange;
               else if (nApproval > nCrisisRange && nApproval <= nHostileRange) nRange = nHostileRange;
               else if (nApproval > nHostileRange && nApproval <= nNeutralRange) nRange = nNeutralRange;
               else if (nApproval > nNeutralRange && nApproval <= nWarmRange) nRange = nWarmRange;
               else if (nApproval > nWarmRange && nApproval <= nFriendlyRange) nRange = nFriendlyRange;
          }
          else // romance active - special table
          {
               if (nApproval == nCrisisRange) nRange = nCrisisRange;
               else if (nApproval > nCrisisRange && nApproval <= nHostileRange) nRange = nHostileRange;
               else if (nApproval > nHostileRange && nApproval <= nNeutralRange) nRange = nNeutralRange;
               else if (nApproval > nNeutralRange && nApproval <= nInterestedRange) nRange = nInterestedRange;
               else if (nApproval > nInterestedRange && nApproval <= nCareRange) nRange = nCareRange;
               else if (nApproval > nCareRange && nApproval <= nAdoreRange) nRange = nAdoreRange;
               else if (nApproval > nAdoreRange && nApproval <= nLoveRange) nRange = nLoveRange;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetApprovalRange", "Approval range check, FOLLOWER: " + Approval_GetFollowerName(nFollower) + ", RANGE: " + IntToString(nRange));
          return nRange;
     }

     /* @brief Sets a oFollower's approval to 0
*
* Sets a nFollower's approval to 0
*
* @param nFollower the follower whose approval we want to change
* @author Yaron
*/
     public void Approval_SetToZero(int nFollower)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetToZero", "Setting follower approval to ZERO, FOLLOWER: " + Approval_GetFollowerName(nFollower));
          int nCurrentApproval = Approval_GetApproval(nFollower);
          int nChange = 0 - nCurrentApproval;
          Approval_ChangeApproval(nFollower, nChange);
     }

     /* @brief Changes the approval rating of nFollower by nChange (positive or negative)
*
* Changes the approval rating of oFollower by nChange (positive or negative)
*
* @param nFollower the follower whose approval we want to change
* @param nChange the approval change value
* @author Yaron
*/
     public void Approval_ChangeApproval(int nFollower, int nChange)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_ChangeApproval", "FOLLOWER: " + IntToString(nFollower) + ", CHANGE: " + IntToString(nChange));
          //string sVar = Approval_GetFollowerApprovalVar(nFollower);
          GameObject oFollower = Approval_GetFollowerObject(nFollower);
          int nApproval = GetFollowerApproval(oFollower);
          int nOldApproval = nApproval;
          nApproval += nChange;
          if (nApproval < -100) nApproval = -100;
          if (nApproval > 100) nApproval = 100;

          // Verify new ranges:
          // Player can get to 'friendly' only if he is eligible
          // Player can get to 'in love' only if he is eligible
          if (Approval_GetRomanceActive(nFollower) == EngineConstants.FALSE && nApproval >= GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_WARM))
          {
               // Trying to get to the 'friendly' range
               // if elig flag set then set GUI tag to 'friendly'
               if (Approval_GetFriendlyEligible(nFollower) != EngineConstants.FALSE)
               {
                    int nStringRef = GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "StringRef", EngineConstants.APP_RANGE_FRIENDLY);
                    SetFollowerApprovalDescription(oFollower, nStringRef);
               }

          }
          else if (Approval_GetRomanceActive(nFollower) != EngineConstants.FALSE && nApproval >= GetLocalInt(GetModule(), EngineConstants.APP_ROMANCE_RANGE_VALUE_ADORE))
          {
               // Trying to get to the in love range
               // if elig flag set then set GUI tag to 'in love'
               if (Approval_GetFriendlyEligible(nFollower) != EngineConstants.FALSE)
               {
                    int nStringRef = GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "StringRef", EngineConstants.APP_ROMANCE_RANGE_LOVE);
                    SetFollowerApprovalDescription(oFollower, nStringRef);
               }

          }
          else // dealing with lower range approvals - clear to change tag
          {
               int nRange = Approval_GetApprovalRangeID(nFollower);
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_ChangeApproval", "Approval range: " + IntToString(nRange));
               int nStringRef;
               if (Approval_GetRomanceActive(nFollower) != EngineConstants.FALSE)
                    nStringRef = GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "StringRef", nRange);
               else
                    nStringRef = GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "StringRef", nRange);
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_ChangeApproval", "Approval tag string ref: " + IntToString(nStringRef));
               SetFollowerApprovalDescription(oFollower, nStringRef);
          }
          nChange = nApproval - nOldApproval;
          AdjustFollowerApproval(oFollower, nChange, EngineConstants.TRUE);
          //SetLocalInt(GetModule(), sVar, nApproval);
          if (nApproval != nOldApproval)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_ChangeApproval", "Changed approval - old value: " + IntToString(nOldApproval) + " new value: " + IntToString(nApproval));
               // check bonus
               if (nChange > 0) // increase - bonuses might be added
               {
                    if (nApproval >= GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_NEUTRAL))
                    {
                         WR_SetPlotFlag(EngineConstants.PLT_TUT_APPROVAL_WARM, EngineConstants.TUT_APPROVAL_WARM_1, EngineConstants.TRUE);
                         // tutorial
                    }

                    if (nApproval >= EngineConstants.APP_BONUS_1 && nApproval < EngineConstants.APP_BONUS_2)
                    {
                         Approval_AddFollowerBonusAbility(nFollower, 1);
                         Approval_AddFollowerGiftCodex(nFollower);
                    }
                    else if (nApproval >= EngineConstants.APP_BONUS_2 && nApproval < EngineConstants.APP_BONUS_3)
                    {
                         Approval_AddFollowerBonusAbility(nFollower, 1);
                         Approval_AddFollowerBonusAbility(nFollower, 2);
                    }
                    else if (nApproval >= EngineConstants.APP_BONUS_3 && nApproval < EngineConstants.APP_BONUS_4)
                    {
                         Approval_AddFollowerBonusAbility(nFollower, 1);
                         Approval_AddFollowerBonusAbility(nFollower, 2);
                         Approval_AddFollowerBonusAbility(nFollower, 3);
                    }
                    else if (nApproval >= EngineConstants.APP_BONUS_4)
                         Approval_AddFollowerBonusAbility(nFollower, 4);
               }
               else // decrease - bonuses might be lost
               {
                    if (nApproval < EngineConstants.APP_BONUS_1)
                    {
                         Approval_RemoveFollowerBonusAbility(nFollower, 1);
                         Approval_RemoveFollowerBonusAbility(nFollower, 2);
                         Approval_RemoveFollowerBonusAbility(nFollower, 3);
                         Approval_RemoveFollowerBonusAbility(nFollower, 4);
                    }
                    else if (nApproval < EngineConstants.APP_BONUS_2)
                    {
                         Approval_RemoveFollowerBonusAbility(nFollower, 2);
                         Approval_RemoveFollowerBonusAbility(nFollower, 3);
                         Approval_RemoveFollowerBonusAbility(nFollower, 4);
                    }
                    else if (nApproval < EngineConstants.APP_BONUS_3)
                    {
                         Approval_RemoveFollowerBonusAbility(nFollower, 3);
                         Approval_RemoveFollowerBonusAbility(nFollower, 4);
                    }
                    else if (nApproval < EngineConstants.APP_BONUS_4)
                         Approval_RemoveFollowerBonusAbility(nFollower, 4);

               }
               //UI_DisplayApprovalChangeMessage(oFollower, nChange);
          }
          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_ChangeApproval", "Approval NOT changed - not eligible for new range!");
     }

     /* @brief Sets or unsets the romance status for oFollower
*
* Sets or unsets the romance status for oFollower
*
* @param nFollower the follower whose romance status we want to change
* @param nStatus EngineConstants.TRUE to set it active, EngineConstants.FALSE to set it inactive
* @sa Approval_GetRomanceActive
* @author Yaron
*/
     public void Approval_SetRomanceActive(int nFollower, int nStatus)
     {
          string sPlot = Approval_GetFollowerPlot(nFollower);
          int nFlag = Approval_GetFollowerRomanceFlag(nFollower);
          int nCutOffFlag = Approval_GetFollowerCutOffFlag(nFollower);
          GameObject oFollower = Approval_GetFollowerObject(nFollower);
          int nStringRef;
          if (nFlag != -1)
               WR_SetPlotFlag(sPlot, nFlag, nStatus, EngineConstants.FALSE);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetRomanceActive", "FOLLOWER: " + Approval_GetFollowerName(nFollower) + ", CHANGING ROMANCE STATUS TO: " + IntToString(nStatus));

          if (nStatus == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetRomanceActive", "Romance is now INACTIVE");
               // If disabling romance and the player is not eligible for friendly, while being in the 'friendly' range,
               // than the approval rating should go down just below the 'friendly' range.
               if (Approval_GetApproval(nFollower) >= GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_WARM)
                  && Approval_GetFriendlyEligible(nFollower) == EngineConstants.FALSE)
               {
                    int nLowerRate = GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_WARM) - Approval_GetApproval(nFollower) - 1; // this should lower to 1 below the friendly range
                    Approval_ChangeApproval(nFollower, nLowerRate);
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetRomanceActive", "Romance disabled while approval is lower than 'friendly' - lowered approval to " + IntToString(Approval_GetApproval(nFollower)));
               }
               // update GUI
               int nRange = Approval_GetApprovalRangeID(nFollower);
               nStringRef = GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "StringRef", nRange);
               SetFollowerApprovalDescription(oFollower, nStringRef);
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetRomanceActive", "Romance is now ACTIVE");
               WR_SetPlotFlag(sPlot, nCutOffFlag, EngineConstants.FALSE, EngineConstants.FALSE);
               // change GUI defs
               int nRange = Approval_GetApprovalRangeID(nFollower);
               nStringRef = GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "StringRef", nRange);
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetRomanceActive", "setting string ref: " + IntToString(nStringRef));
               SetFollowerApprovalDescription(oFollower, nStringRef);
          }

     }

     /* @brief Returns the romace status of a follower
*
* Returns the romace status of a follower
*
* @param nFollower the follower whose romance status we want to get
* @returns the romace status of the follower (EngineConstants.TRUE if active, EngineConstants.FALSE if inactive)
* @sa Approval_GetRomanceActive
* @author Yaron
*/
     public int Approval_GetRomanceActive(int nFollower)
     {
          string sPlot = Approval_GetFollowerPlot(nFollower);
          int nFlag = Approval_GetFollowerRomanceFlag(nFollower);

          int bRet = (nFlag != -1 && WR_GetPlotFlag(sPlot, nFlag, EngineConstants.FALSE) != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetRomanceActive", "Checking romance flag for follower: " + IntToString(nFollower) + ", flag= " + IntToString(bRet));

          return bRet;
     }

     /* @brief Allows the follower to advance to the 'friendly' range
*
* Allows the follower to advance to the 'friendly' range
*
* @param nFollower the follower who we want to enable the friendly status
* @sa Approval_GetFriendlyEligible
* @author Yaron
*/
     public void Approval_SetFriendlyEligible(int nFollower, int nStatus = EngineConstants.TRUE)
     {
          if (nStatus != EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetFriendlyEligible", "Setting follower to be friendly-eligible: " + IntToString(nFollower));
          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetFriendlyEligible", "CLEARING friendly-eligible flag: " + IntToString(nFollower));

          string sPlot = Approval_GetFollowerPlot(nFollower);
          int nFlag = Approval_GetFollowerFriendEligFlag(nFollower);
          WR_SetPlotFlag(sPlot, nFlag, nStatus, EngineConstants.TRUE);

     }

     /* @brief Returns the friendly status of a follower
*
* Returns the friendly status of a follower
*
* @param nFollower the follower who we want to get the friendly status
* @returns EngineConstants.TRUE if friendly eligible, EngineConstants.FALSE if not
* @sa Approval_SetFriendlyEligible
* @author Yaron
*/
     public int Approval_GetFriendlyEligible(int nFollower)
     {
          string sPlot = Approval_GetFollowerPlot(nFollower);
          int nFlag = Approval_GetFollowerFriendEligFlag(nFollower);
          int nRet = WR_GetPlotFlag(sPlot, nFlag, EngineConstants.FALSE);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetFriendlyEligible", "Checking friendly-eligible flag for follower: " + IntToString(nFollower) + ", flag= " + IntToString(nRet));
          return nRet;
     }

     /* @brief Allows the follower to advance to the 'love' range
*
* Allows the follower to advance to the 'love' range
*
* @param nFollower the follower who we want to enable the love status
* @sa Approval_GetLoveEligible
* @author Yaron
*/
     public void Approval_SetLoveEligible(int nFollower, int nStatus = EngineConstants.TRUE)
     {
          if (nStatus != EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetLoveEligible", "Setting follower to be love-eligible: " + IntToString(nFollower));
          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_SetLoveEligible", "CLEARING follower love-eligible flag: " + IntToString(nFollower));

          string sPlot = Approval_GetFollowerPlot(nFollower);
          int nFlag = Approval_GetFollowerLoveEligFlag(nFollower);
          WR_SetPlotFlag(sPlot, nFlag, nStatus, EngineConstants.TRUE);
     }

     /* @brief Returns the love status of a follower
*
* Returns the love status of a follower
*
* @param nFollower the follower who we want to get the love status
* @returns EngineConstants.TRUE if love eligible, EngineConstants.FALSE if not
* @sa Approval_SetLoveEligible
* @author Yaron
*/
     public int Approval_GetLoveEligible(int nFollower)
     {
          string sPlot = Approval_GetFollowerPlot(nFollower);
          int nFlag = Approval_GetFollowerLoveEligFlag(nFollower);
          int nRet = WR_GetPlotFlag(sPlot, nFlag, EngineConstants.FALSE);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetLoveEligible", "Checking love-eligible flag for follower: " + IntToString(nFollower) + ", flag= " + IntToString(nRet));
          return nRet;
     }

     /* @brief Checks if a specific approval range is valid for a follower (romance or not)
*
* A range is valid if the follower's approval is inside the range or higher.
* For example: if the approval is friendly and I check for neutral then it should
* still return EngineConstants.TRUE.
*
* @param nFollower the follower whose approval range we check
* @param nRange the level of approval we want to check the follower for (EngineConstants.APP_RANGE_***)
* @param bRomanceTable EngineConstants.TRUE if we check the romance table, EngineConstants.FALSE if we check the normal approval table
* @returns EngineConstants.TRUE if within the range, EngineConstants.FALSE if not
* @author Yaron
*/
     public int Approval_IsRangeValid(int nFollower, int nRange, int bRomanceTable)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_IsRangeValid", "START, follower: " + IntToString(nFollower) + ", range: " + IntToString(nRange) + ", check romanace= " + IntToString(bRomanceTable));

          int nResult = EngineConstants.FALSE;
          int nApproval = Approval_GetApproval(nFollower);

          // First, check if the creature's approval table matches the requested table
          int bFoundResult = EngineConstants.FALSE;
          int bRomanceActive = Approval_GetRomanceActive(nFollower);
          if (bRomanceActive == EngineConstants.FALSE && bRomanceTable != EngineConstants.FALSE) // Creature uses normal table but it was requested to check the romance table
          {
               bFoundResult = EngineConstants.TRUE;
          }

          if (bFoundResult == EngineConstants.FALSE) // normal approval ranges
          {
               switch (nRange)
               {
                    case EngineConstants.APP_RANGE_CRISIS:
                         {
                              if (nApproval <= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_CRISIS))
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.APP_RANGE_HOSTILE:
                         {
                              if (nApproval <= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_HOSTILE))
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.APP_RANGE_NEUTRAL:
                         {
                              if (nApproval > GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_HOSTILE))
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.APP_RANGE_WARM:
                         {
                              if (nApproval > GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_NEUTRAL))
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.APP_RANGE_FRIENDLY:
                         {
                              if (nApproval > GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_WARM) &&
                                  Approval_GetFriendlyEligible(nFollower) != EngineConstants.FALSE)
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.APP_ROMANCE_RANGE_INTERESTED:
                         {
                              if (nApproval > GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_NEUTRAL) &&
                                  Approval_GetRomanceActive(nFollower) != EngineConstants.FALSE)
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.APP_ROMANCE_RANGE_CARE:
                         {
                              if (nApproval > GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "Range", EngineConstants.APP_ROMANCE_RANGE_INTERESTED) &&
                                  Approval_GetRomanceActive(nFollower) != EngineConstants.FALSE)
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.APP_ROMANCE_RANGE_ADORE:
                         {
                              if (nApproval > GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "Range", EngineConstants.APP_ROMANCE_RANGE_CARE) &&
                                  Approval_GetRomanceActive(nFollower) != EngineConstants.FALSE)
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
                    case EngineConstants.APP_ROMANCE_RANGE_LOVE:
                         {
                              if (nApproval > GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "Range", EngineConstants.APP_ROMANCE_RANGE_ADORE) &&
                                  Approval_GetRomanceActive(nFollower) != EngineConstants.FALSE &&
                                  Approval_GetFriendlyEligible(nFollower) != EngineConstants.FALSE)
                                   nResult = EngineConstants.TRUE;
                              break;
                         }
               }
          }

          return nResult;
     }

     /* @brief Gift an item to a follower: destorying the item and adjusting approval rating if needed
*
* Gift an item to a follower: destorying the item and adjusting approval rating if needed
*
* @param nFollower the follower who we want to give the gift to
* @param oItem the item being gifted
* @author Yaron
*/
     public int Approval_HandleGift(int nFollower, GameObject oItem)
     {
          int nValue = GetItemValue(oItem); // in bits (100 bit= 1 silver, 100 silver=1 gold, 10000 bits = 1 gold
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_HandleGift", "Value of item: " + IntToString(nValue));

          int nApprovalChange = 5;

          // Each subsequent gift given for the same follower is worth 1 point less.
          // If the item�s motivation matches the follower�s motivation then it is worth as least 1 point.
          string sGiftCountVar = Approval_GetFollowerGiftCountVar(nFollower);
          int nGiftCount = GetLocalInt(GetModule(), sGiftCountVar);
          nApprovalChange -= nGiftCount;
          if (nApprovalChange <= 0)
               nApprovalChange = 1;
          nGiftCount++;
          SetLocalInt(GetModule(), sGiftCountVar, nGiftCount);

          // if follower likes item increase by 5
          int nLike = GetLocalInt(oItem, EngineConstants.APP_ITEM_MOTIVATION);
          if (nLike == nFollower)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_HandleGift", "Follower likes item - increasing by 5");
               nApprovalChange += 5;
          }

          // APPROVAL CHANGE MOVED TO sp_module_item_acquired. This was done to handle gifts refunds.
          //Approval_ChangeApproval(nFollower, nApprovalChange);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_HandleGift", "Item gifted to " + Approval_GetFollowerName(nFollower) + ", approval change: " + IntToString(nApprovalChange));

          return nApprovalChange;

     }

     public string Approval_GetFollowerPlot(int nFollower)
     {
          string sPlot = String.Empty;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: sPlot = EngineConstants.PLT_GENPT_APP_ALISTAIR; break;
               case EngineConstants.APP_FOLLOWER_DOG: sPlot = EngineConstants.PLT_GENPT_APP_DOG; break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: sPlot = EngineConstants.PLT_GENPT_APP_MORRIGAN; break;
               case EngineConstants.APP_FOLLOWER_WYNNE: sPlot = EngineConstants.PLT_GENPT_APP_WYNNE; break;
               case EngineConstants.APP_FOLLOWER_SHALE: sPlot = EngineConstants.PLT_GENPT_APP_SHALE; break;
               case EngineConstants.APP_FOLLOWER_STEN: sPlot = EngineConstants.PLT_GENPT_APP_STEN; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: sPlot = EngineConstants.PLT_GENPT_APP_ZEVRAN; break;
               case EngineConstants.APP_FOLLOWER_OGHREN: sPlot = EngineConstants.PLT_GENPT_APP_OGHREN; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: sPlot = EngineConstants.PLT_GENPT_APP_LELIANA; break;
               case EngineConstants.APP_FOLLOWER_LOGHAIN: sPlot = EngineConstants.PLT_GENPT_APP_LOGHAIN; break;
          }
          return sPlot;
     }

     public int Approval_GetFollowerRomanceFlag(int nFollower)
     {
          int nFlag = -1;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: nFlag = EngineConstants.APP_ALISTAIR_ROMANCE_ACTIVE; break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: nFlag = EngineConstants.APP_MORRIGAN_ROMANCE_ACTIVE; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: nFlag = EngineConstants.APP_ZEVRAN_ROMANCE_ACTIVE; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: nFlag = EngineConstants.APP_LELIANA_ROMANCE_ACTIVE; break;
          }
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetFollowerRomanceFlag", "Follower romance flag= " + IntToString(nFlag));

          return nFlag;
     }

     public int Approval_GetFollowerCutOffFlag(int nFollower)
     {
          int nFlag = 0;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: nFlag = EngineConstants.APP_ALISTAIR_ROMANCE_CUT_OFF; break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: nFlag = EngineConstants.APP_MORRIGAN_ROMANCE_CUT_OFF; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: nFlag = EngineConstants.APP_ZEVRAN_ROMANCE_CUT_OFF; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: nFlag = EngineConstants.APP_LELIANA_ROMANCE_CUT_OFF; break;
          }
          return nFlag;
     }

     public int Approval_GetFollowerLoveEligFlag(int nFollower)
     {
          int nFlag = 0;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: nFlag = EngineConstants.APP_ALISTAIR_LOVE_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: nFlag = EngineConstants.APP_MORRIGAN_LOVE_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: nFlag = EngineConstants.APP_ZEVRAN_LOVE_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: nFlag = EngineConstants.APP_LELIANA_LOVE_ELIGIBLE; break;
          }
          return nFlag;
     }
     public int Approval_GetFollowerFriendEligFlag(int nFollower)
     {
          int nFlag = 0;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: nFlag = EngineConstants.APP_ALISTAIR_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_DOG: nFlag = EngineConstants.APP_DOG_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: nFlag = EngineConstants.APP_MORRIGAN_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_WYNNE: nFlag = EngineConstants.APP_WYNNE_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_SHALE: nFlag = EngineConstants.APP_SHALE_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_STEN: nFlag = EngineConstants.APP_STEN_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: nFlag = EngineConstants.APP_ZEVRAN_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_OGHREN: nFlag = EngineConstants.APP_OGHREN_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: nFlag = EngineConstants.APP_LELIANA_FRIENDLY_ELIGIBLE; break;
               case EngineConstants.APP_FOLLOWER_LOGHAIN: nFlag = EngineConstants.APP_LOGHAIN_FRIENDLY_ELIGIBLE; break;
          }
          return nFlag;
     }

     public string Approval_GetFollowerGiftCountVar(int nFollower)
     {
          string sVar = String.Empty;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_ALISTAIR; break;
               case EngineConstants.APP_FOLLOWER_DOG: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_DOG; break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_MORRIGAN; break;
               case EngineConstants.APP_FOLLOWER_WYNNE: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_WYNNE; break;
               case EngineConstants.APP_FOLLOWER_SHALE: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_SHALE; break;
               case EngineConstants.APP_FOLLOWER_STEN: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_STEN; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_ZEVRAN; break;
               case EngineConstants.APP_FOLLOWER_OGHREN: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_OGHREN; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_LELIANA; break;
               case EngineConstants.APP_FOLLOWER_LOGHAIN: sVar = EngineConstants.APP_APPROVAL_GIFT_COUNT_LOGHAIN; break;
          }
          return sVar;
     }

     public int Approval_GetFollowerIndex(GameObject oFollower)
     {
          string sTag = GetTag(oFollower);
          int nRet = -1;
          if (sTag == EngineConstants.GEN_FL_ALISTAIR) nRet = 1;
          else if (sTag == EngineConstants.GEN_FL_DOG) nRet = 2;
          else if (sTag == EngineConstants.GEN_FL_MORRIGAN) nRet = 3;
          else if (sTag == EngineConstants.GEN_FL_WYNNE) nRet = 4;
          else if (sTag == EngineConstants.GEN_FL_SHALE) nRet = 5;
          else if (sTag == EngineConstants.GEN_FL_STEN) nRet = 6;
          else if (sTag == EngineConstants.GEN_FL_ZEVRAN) nRet = 7;
          else if (sTag == EngineConstants.GEN_FL_OGHREN) nRet = 8;
          else if (sTag == EngineConstants.GEN_FL_LELIANA) nRet = 9;
          else if (sTag == EngineConstants.GEN_FL_LOGHAIN) nRet = 10;

          return nRet;
     }

     public string Approval_GetFollowerName(int nFollower)
     {
          string sName = String.Empty;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: sName = "Alistair"; break;
               case EngineConstants.APP_FOLLOWER_DOG: sName = "Dog"; break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: sName = "Morrigan"; break;
               case EngineConstants.APP_FOLLOWER_WYNNE: sName = "Wynne"; break;
               case EngineConstants.APP_FOLLOWER_SHALE: sName = "Shale"; break;
               case EngineConstants.APP_FOLLOWER_STEN: sName = "Sten"; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: sName = "Zevran"; break;
               case EngineConstants.APP_FOLLOWER_OGHREN: sName = "Oghren"; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: sName = "Leliana"; break;
               case EngineConstants.APP_FOLLOWER_LOGHAIN: sName = "Loghain"; break;
          }
          return sName;
     }

     public void Approval_AddFollowerGiftCodex(int nFollower)
     {
          string sPlot = String.Empty;
          int nFlag = 0;
          switch (nFollower)
          {
               case EngineConstants.APP_FOLLOWER_ALISTAIR: sPlot = "cod_cha_alistair"; nFlag = 8; break;
               case EngineConstants.APP_FOLLOWER_DOG: break;
               case EngineConstants.APP_FOLLOWER_MORRIGAN: sPlot = "cod_cha_morrigan"; nFlag = 6; break;
               case EngineConstants.APP_FOLLOWER_WYNNE: sPlot = "cod_cha_wynne"; nFlag = 7; break;
               case EngineConstants.APP_FOLLOWER_SHALE: break;
               case EngineConstants.APP_FOLLOWER_STEN: sPlot = "cod_cha_sten"; nFlag = 5; break;
               case EngineConstants.APP_FOLLOWER_ZEVRAN: sPlot = "cod_cha_zevran"; nFlag = 10; break;
               case EngineConstants.APP_FOLLOWER_OGHREN: sPlot = "cod_cha_oghren"; nFlag = 5; break;
               case EngineConstants.APP_FOLLOWER_LELIANA: sPlot = "cod_cha_leliana"; nFlag = 10; break;
               case EngineConstants.APP_FOLLOWER_LOGHAIN: sPlot = "cod_cha_loghain"; nFlag = 11; break;
          }
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_AddFollowerGiftCodex", "Follower: " + IntToString(nFollower));

          if (sPlot != "")
               WR_SetPlotFlag(sPlot, nFlag, EngineConstants.TRUE);
     }

     public int Approval_GetApprovalRangeID(int nFollower)
     {
          GameObject oFollower = Approval_GetFollowerObject(nFollower);
          int nApproval = GetFollowerApproval(oFollower);
          int nRet;
          if (Approval_GetRomanceActive(nFollower) != EngineConstants.FALSE)
          {
               if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "Range", EngineConstants.APP_ROMANCE_RANGE_ADORE))
                    nRet = EngineConstants.APP_ROMANCE_RANGE_LOVE;
               else if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "Range", EngineConstants.APP_ROMANCE_RANGE_CARE))
                    nRet = EngineConstants.APP_ROMANCE_RANGE_ADORE;
               else if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_ROMANCE_RANGES, "Range", EngineConstants.APP_ROMANCE_RANGE_INTERESTED))
                    nRet = EngineConstants.APP_ROMANCE_RANGE_CARE;
               else if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_NEUTRAL))
                    nRet = EngineConstants.APP_ROMANCE_RANGE_INTERESTED;
               else if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_HOSTILE))
                    nRet = EngineConstants.APP_RANGE_NEUTRAL;
               else if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_CRISIS))
                    nRet = EngineConstants.APP_RANGE_HOSTILE;
               else
                    nRet = EngineConstants.APP_RANGE_CRISIS;

          }
          else
          {
               if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_WARM))
                    nRet = EngineConstants.APP_RANGE_FRIENDLY;
               else if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_NEUTRAL))
                    nRet = EngineConstants.APP_RANGE_WARM;
               else if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_HOSTILE))
                    nRet = EngineConstants.APP_RANGE_NEUTRAL;
               else if (nApproval >= GetM2DAInt(EngineConstants.TABLE_APPROVAL_NORMAL_RANGES, "Range", EngineConstants.APP_RANGE_CRISIS))
                    nRet = EngineConstants.APP_RANGE_HOSTILE;
               else
                    nRet = EngineConstants.APP_RANGE_CRISIS;
          }
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Approval_GetApprovalRangeID", "Approval range is: " + IntToString(nRet));
          return nRet;
     }

     /* @} */
}