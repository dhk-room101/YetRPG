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
     /* @addtogroup party_systems Party Systems
     *
     * Includes all generic party systems
     */
     /* @{*/

     //#include"log_h"
     //#include"utility_h"

     //#include"plt_gen00pt_party"

     /* @brief Generates the initial party banter array
*
* This function generates the initial party banter array to be used to determine
* which follower will speak in the party banter dialog. The function makes a random
* list of party members, not including the main player.
*
* @author Yaron Jakobs
*/
     public void Party_GenerateBanterArray()
     {
          GameObject oPC = GetHero();
          List<GameObject> arParty = GetPartyList();
          int nSize = GetArraySize(arParty);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_GeneratePartyBanter", "START, party size= " + IntToString(nSize));
          // First, init all party banter system variables
          SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, null);
          SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, null);
          SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3, null);
          SetLocalInt(GetModule(), EngineConstants.PARTY_BANTER_ROTATION_COUNTER, 0);

          // If there are no party members or only 1 follower -> return
          // (system works for at least 2 followers)
          if (nSize <= 2) // player only (size 1), player+follower (size 2)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_GeneratePartyBanter", "No party members, not generating array");
               return;
          }

          // If two party members -> randomize them into slots 1 and 2 (3 remains empty to flag a smaller array)
          int nRand = Engine_Random(1); // 50/50, 2 options: 1-2 or 2-1
          if (nSize == 3)
          {
               if (nRand == 0)
               {
                    SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, arParty[1]);
                    SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, arParty[2]);
               }
               else
               {
                    SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, arParty[2]);
                    SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, arParty[1]);
               }
          }

          // We need to do something simple here - randomize the order of 3 party members
          // Lets call them 1, 2 and 3. Original, I know.
          // We have the following possible 6 results:
          // 1) 1-2-3
          // 2) 1-3-2
          // 3) 2-1-3
          // 4) 2-3-1
          // 5) 3-1-2
          // 6) 3-2-1
          // We'll just do a simple random generation of number between 1 and 6 and that
          // would be the new order
          // Notice that since the player is arParty[0] then the following applies:
          // follower 1 == arParty[1];
          // follower 2 == arParty[2];
          // follower 3 == arParty[3];

          nRand = Engine_Random(6) + 1;
          if (nSize >= 4)
          {
               switch (nRand)
               {
                    case 1: // 1-2-3
                         {
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, arParty[1]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, arParty[2]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3, arParty[3]);
                              break;
                         }
                    case 2: // 1-3-2
                         {
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, arParty[1]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, arParty[3]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3, arParty[2]);
                              break;
                         }
                    case 3: // 2-1-3
                         {
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, arParty[2]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, arParty[1]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3, arParty[3]);
                              break;
                         }
                    case 4: // 2-3-1
                         {
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, arParty[2]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, arParty[3]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3, arParty[1]);
                              break;
                         }
                    case 5: // 3-1-2
                         {
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, arParty[3]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, arParty[1]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3, arParty[2]);
                              break;
                         }
                    case 6: // 3-2-1
                         {
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, arParty[3]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, arParty[2]);
                              SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3, arParty[1]);
                              break;
                         }
               }
          }

          Party_DumpPartyBanterArray();
     }

     /* @brief Check if a specific follower is in a specific slot in the banter array
*
* The check is only for the 1st and 2nd slot - 1st for the primary speake, 2nd
* for the secondary
*
* @param oFollower the follower we are checking in the array
* @param nSlot the slot we are looking for the follower in
* @returns EngineConstants.TRUE if the follower is in the slot, EngineConstants.FALSE otherwise
* @author Yaron Jakobs
*/
     public int Party_FollowerInBanterArraySlot(GameObject oFollower, int nSlot)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_FollowerInBanterArraySlot",
              "Looking for follower in party banter array, follower= " + GetTag(oFollower) + ", slot= " + IntToString(nSlot));

          int nRet = EngineConstants.FALSE;
          GameObject oArrayFollower = null;

          if (nSlot == 1)
               oArrayFollower = GetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1);
          else if (nSlot == 2)
               oArrayFollower = GetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2);

          if (oArrayFollower == oFollower)
               nRet = EngineConstants.TRUE;

          if (nRet != EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_FollowerInBanterArraySlot", "Follower is in the slot!");
          else
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_FollowerInBanterArraySlot", "Follower is NOT in the slot!");

          return nRet;
     }

     /* @brief Rotates the party members array in the banter list
*
* This function changes the order of the followers in the banter array
* to generate a different order of interactions between them when checked
* inside the party banter dialog
*
* @author Yaron Jakobs
*/
     public void Party_RotateBanterArray()
     {
          // Party banter array can be size 2 or 3:
          // Size 2: 1-2 change into: 2-1
          // Size 3: 1-2-3 change into 2-3-1

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_RotateBanterArray", "START");

          GameObject oTempFollower; // used to store first follower during the switch
          int nSize;

          GameObject oFollower1 = GetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1);
          GameObject oFollower2 = GetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2);
          GameObject oFollower3 = GetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3);
          int nCounter = GetLocalInt(GetModule(), EngineConstants.PARTY_BANTER_ROTATION_COUNTER);
          nCounter++;
          SetLocalInt(GetModule(), EngineConstants.PARTY_BANTER_ROTATION_COUNTER, nCounter);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_RotateBanterArray", "Rotation counter is now: " + IntToString(nCounter));

          if (oFollower3 == null) // no 3rd follower
               nSize = 2;
          else
               nSize = 3;

          if (nSize == 2)
          {
               SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, oFollower2);
               SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, oFollower1);
          }
          else if (nSize == 3)
          {
               SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1, oFollower2);
               SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2, oFollower3);
               SetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3, oFollower1);
          }

          Party_DumpPartyBanterArray();

     }

     /* @brief Debug-dump of the current array and rotation counter
*
* Debug-dump of the current array and rotation counter
*
* @author Yaron Jakobs
*/
     public void Party_DumpPartyBanterArray()
     {
          GameObject oFollower1 = GetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_1);
          GameObject oFollower2 = GetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_2);
          GameObject oFollower3 = GetLocalObject(GetModule(), EngineConstants.PARTY_BANTER_ARRAY_SLOT_3);
          int nCounter = GetLocalInt(GetModule(), EngineConstants.PARTY_BANTER_ROTATION_COUNTER);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_DumpPartyBanterArray", "party banter follower 1: " + GetTag(oFollower1));
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_DumpPartyBanterArray", "party banter follower 2: " + GetTag(oFollower2));
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_DumpPartyBanterArray", "party banter follower 3: " + GetTag(oFollower3));
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "Party_DumpPartyBanterArray", "party banter rotation counter: " + IntToString(nCounter));

     }

     // Returns the follower's GameObject ID if the follower is in the active party
     public GameObject Party_GetActiveFollowerByTag(string sTag)
     {
          List<GameObject> arParty = GetPartyList(GetHero());
          int i;
          int nSize = GetArraySize(arParty);
          GameObject oCurrent;
          for (i = 0; i < nSize; i++)
          {
               oCurrent = arParty[i];
               if (GetTag(oCurrent) == sTag)
                    return oCurrent;
          }
          return null;
     }

     // Returns a follower GameObject ID if the follower is in the party (active or pool)
     // or if he is in the same area (inactive)
     public GameObject Party_GetFollowerByTag(string sTag)
     {
          // We have to retrieve the follower like this. Trying to do it with
          // GetObjectByTag proved to mess up lots of stuff. -- yaron.
          List<GameObject> arParty = GetPartyPoolList();
          int i;
          int nSize = GetArraySize(arParty);
          GameObject oCurrent;
          for (i = 0; i < nSize; i++)
          {
               oCurrent = arParty[i];
               if (GetTag(oCurrent) == sTag)
                    return oCurrent;
          }

          return UT_GetNearestObjectByTag(GetMainControlled(), sTag, EngineConstants.TRUE);

     }

     public void Party_SetFollowerInCamp(GameObject oFollower, int nFollowerPartyFlag, int bSetInactive = EngineConstants.TRUE)
     {
          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, nFollowerPartyFlag, EngineConstants.FALSE);
          WR_SetFollowerState(oFollower, EngineConstants.FOLLOWER_STATE_AVAILABLE, EngineConstants.FALSE);
          if (bSetInactive != EngineConstants.FALSE)
               WR_SetObjectActive(oFollower, EngineConstants.FALSE);
     }

     // This function locks a plot follower into the active party. If the follower is not in the active
     // party this function will clear the party, add the follower, lock it in and open the party picker
     // nFollowerPartyFlag and nFollowerCampFlag are the flags used in the gen00pt_party plot for this follower
     public void Party_LockFollower(GameObject oFollower, int nFollowerPartyFlag, int nFollowerCampFlag)
     {
          string sFollower = GetTag(oFollower);
          GameObject oPC = GetHero();
          GameObject oArea = GetArea(oPC);

          if (GetFollowerState(oFollower) == EngineConstants.FOLLOWER_STATE_ACTIVE)
          {
               WR_SetFollowerState(oFollower, EngineConstants.FOLLOWER_STATE_LOCKEDACTIVE);
               return;
          }
          // follower not in active

          // Clearing active party so there won't be 1+4 party members
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ALISTAIR_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ALISTAIR_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_DOG_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_SHALE_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_SHALE_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_STEN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_ZEVRAN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_OGHREN_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_OGHREN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LELIANA_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_MORRIGAN_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_MORRIGAN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_WYNNE_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);
          if (WR_GetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LOGHAIN_RECRUITED) != EngineConstants.FALSE)
               WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, EngineConstants.GEN_LOGHAIN_IN_CAMP, EngineConstants.TRUE, EngineConstants.TRUE);

          WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_PARTY, nFollowerPartyFlag, EngineConstants.TRUE, EngineConstants.TRUE); // Setting locked followers's in-party flag here since it won't be trigger using the GUI
          WR_SetFollowerState(oFollower, EngineConstants.FOLLOWER_STATE_LOCKEDACTIVE, EngineConstants.FALSE);

          // Party picker GUI is triggered in player_core, through the event

          SetPartyPickerGUIStatus(EngineConstants.PP_GUI_STATUS_USE);
          ShowPartyPickerGUI();
     }

     // unlocks a follower locked into the active party
     public void Party_UnlockFollower(GameObject oFollower)
     {
          if (GetFollowerState(oFollower) == EngineConstants.FOLLOWER_STATE_LOCKEDACTIVE)
               WR_SetFollowerState(oFollower, EngineConstants.FOLLOWER_STATE_ACTIVE);
     }

     public void DEBUG_ScaleFolloweItems(GameObject oFollower)
     {
          List<GameObject> arItems = GetItemsInInventory(oFollower, EngineConstants.GET_ITEMS_OPTION_ALL);
          int nSize = GetArraySize(arItems);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "DEBUG_ScaleFolloweItems", "START, number of item: " + IntToString(nSize));

          int i;
          GameObject oCurrent;
          int nItemMaterialProgression;
          int nMaterialType;
          int nItemLevel;
          for (i = 0; i < nSize; i++)
          {
               oCurrent = arItems[i];
               nItemMaterialProgression = GetItemMaterialProgression(oCurrent);
               if (nItemMaterialProgression > 0)
               {
                    // Find out what level/column to scale the item to
                    // == creature level / 3
                    nItemLevel = ((GetLevel(oFollower) - 1) / 3) + 1;
                    if (nItemLevel <= 0) nItemLevel = 1;
                    else if (nItemLevel > 8) nItemLevel = 8;
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "DEBUG_ScaleFolloweItems", "Scaling item: " + GetTag(oCurrent) + " to EngineConstants.ITEM LEVEL: " + IntToString(nItemLevel));

                    nMaterialType = GetM2DAInt(199, "Material" + IntToString(nItemLevel), nItemMaterialProgression);
                    // scale item
                    SetItemMaterialType(oCurrent, nMaterialType);
               }

          }
     }

     /* @} */
}