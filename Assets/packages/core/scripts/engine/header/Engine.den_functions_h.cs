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
     //#include"den_constants_h"
     //#include"plt_denpt_rescue_the_queen"
     //#include"plt_denpt_captured"
     //#include"plt_denpt_slave_trade"
     //#include"plt_denpt_main"

     //#include"plt_mnp00pt_ssf_landsmeet"
     //#include"utility_h"
     //#include"campaign_h"
     //#include"world_maps_h"
     //#include"sys_ambient_h"

     //moved public const string DEN_WAYPOINT_STRING   = "wp_";
     //moved public const string DEN_STAND_STRING      = "_stand";
     //moved public const string DEN_SIT_STRING        = "_sit";

     /* @brief Switches the equipment of oCreature
     *
     *   This only works if the creature has exactly two sets of equipment, one equipped and one not (one set can be nothing).
     *
     *
     *
     * @param oCreature - The creature to switch the equipment of
     * @author Craig
     **/
     public void DEN_SwitchEquipment(GameObject oCreature)
     {
          List<GameObject> arrSpareEquipment = GetItemsInInventory(oCreature, EngineConstants.GET_ITEMS_OPTION_BACKPACK);
          List<GameObject> arrEquipment = GetItemsInInventory(oCreature, EngineConstants.GET_ITEMS_OPTION_EQUIPPED);
          int nIndex;
          int nTotalItems = GetArraySize(arrEquipment);

          nTotalItems = GetArraySize(arrEquipment);

          for (nIndex = 0; nIndex < nTotalItems; nIndex++)
          {
               UnequipItem(oCreature, arrEquipment[nIndex]);
          }

          nTotalItems = GetArraySize(arrSpareEquipment);

          for (nIndex = 0; nIndex < nTotalItems; nIndex++)
          {
               EquipItem(oCreature, arrSpareEquipment[nIndex]);
          }
     }

     //public void DEN_ForceAbility(GameObject oCaster, int nAbility, GameObject oTarget = null, Vector3 vTarget, float fConjureTime = -1.0f)
     // swapped target object with target position, due to not able to have empty constant past in vector
     public void DEN_ForceAbility(GameObject oCaster, int nAbility, Vector3 vTarget, GameObject oTarget = null, float fConjureTime = -1.0f)
     {
          xEffect effRestoreManaStam = EffectModifyManaStamina(Ability_GetAbilityCost(oCaster, nAbility));
          xCommand cSpell = CommandUseAbility(nAbility, oTarget, vTarget, fConjureTime);

          if (HasAbility(oCaster, nAbility) == EngineConstants.FALSE)
          {
               AddAbility(oCaster, nAbility);
          }

          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, effRestoreManaStam, oCaster);
          SetCooldown(oCaster, nAbility, 0.0f);

          AddCommand(oCaster, cSpell, EngineConstants.TRUE);
     }

     public void DEN_SetTeamGroup(int nTeamId, int nHostilityGroup)
     {
          int nIndex;
          int nTeamSize;
          List<GameObject> arTeam;
          arTeam = UT_GetTeam(nTeamId);
          nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
               SetGroupId(arTeam[nIndex], nHostilityGroup);
     }

     public void DEN_TeamStopAmbient(int nTeamID, int bClearAllCommands = EngineConstants.TRUE)
     {
          List<GameObject> arrTeam = GetTeam(nTeamID);
          GameObject oMember;
          int nTeamSize = GetArraySize(arrTeam);

          int n;
          for (n = 0; n < nTeamSize; n++)
          {
               oMember = arrTeam[n];
               Ambient_Stop(oMember);
               if (bClearAllCommands != EngineConstants.FALSE)
               {
                    WR_ClearAllCommands(oMember, EngineConstants.TRUE);
               }
          }
     }

     public int DEN_GameModeChanged(int nGameMode)
     {
          int bEventHandled = EngineConstants.FALSE;
          GameObject oPC = GetHero();
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_GameModeChanged", "firing");
          switch (nGameMode)
          {
               // Initiate the 'rescue' cutscene if the player is killed at the top of the singal tower by the Darkspawn horde.
               case EngineConstants.GM_DEAD:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "PRE_GameModeChanged.EngineConstants.GM_DEAD", "firing");
                         // If the beacon is lit but the rescue cutscene has not yet been played (i.e. the darkspawn horde)
                         if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_CAPTURED, EngineConstants.DEN_CAPTURED_CAUTHRIEN_ATTACKS) != EngineConstants.FALSE
                            && WR_GetPlotFlag(EngineConstants.PLT_DENPT_CAPTURED, EngineConstants.DEN_CAPTURED_CAUTHRIEN_DEFEATED) == EngineConstants.FALSE
                            && WR_GetPlotFlag(EngineConstants.PLT_DENPT_CAPTURED, EngineConstants.DEN_CAPTURED_FADE_TO_PC_IN_PRISON) == EngineConstants.FALSE)
                         {
                              //if (!WR_GetPlotFlag(EngineConstants.PLT_DENPT_CAPTURED, EngineConstants.DEN_CAPTURED_PC_CAPTURED))
                              //{
                              WR_SetPlotFlag(EngineConstants.PLT_DENPT_CAPTURED, EngineConstants.DEN_CAPTURED_PC_CAPTURED, EngineConstants.TRUE, EngineConstants.TRUE);
                              //}
                              bEventHandled = EngineConstants.TRUE; // avoid game mode setting; the player should not see the death gui.
                         }
                         break;
                    }
          }

          return bEventHandled;
     }

     public void DEN_TeamHelp(int nTeam, int bHelp)
     {
          int nIndex;
          int nTeamSize;
          List<GameObject> arTeam;
          arTeam = UT_GetTeam(nTeam);
          nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
               SetLocalInt(arTeam[nIndex], EngineConstants.AI_HELP_TEAM_STATUS, bHelp);
     }

     public void DEN_TeamFollow(int nTeam, int bFollow)
     {
          int nIndex;
          int nTeamSize;
          List<GameObject> arTeam;
          arTeam = UT_GetTeam(nTeam);
          nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               if (bFollow != EngineConstants.FALSE)
               {
                    AddNonPartyFollower(arTeam[nIndex]);
               }
               else
               {
                    RemoveNonPartyFollower(arTeam[nIndex]);
               }
          }

     }

     public void DEN_ModulePresave()
     {
          if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_PLOT_OPENED) != EngineConstants.FALSE)
          {
               if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_QUEST_DONE) != EngineConstants.FALSE)
               {
                    if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ALISTAIR_IS_KING) != EngineConstants.FALSE)
                    {
                         if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_LOGHAIN_LIVES) != EngineConstants.FALSE)
                         {
                              // (Craig) I suspect this should never appear - this option was cut, if it ever existed
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08A_COMPLETE_ALISTAIR_KING_LOGHAIN_LIVES, EngineConstants.TRUE);
                         }
                         else
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08B_COMPLETE_ALISTAIR_KING_LOGHAIN_DIES, EngineConstants.TRUE);
                         }
                    }
                    else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ALISTAIR_ENGAGED_TO_ANORA) != EngineConstants.FALSE)
                    {
                         if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_LOGHAIN_LIVES) != EngineConstants.FALSE)
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08G_COMPLETE_ALISTAIR_ANORA_MARRY_LOGHAIN_LIVES, EngineConstants.TRUE);
                         }
                         else
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08H_COMPLETE_ALISTAIR_ANORA_MARRY_LOGHAIN_DIES, EngineConstants.TRUE);
                         }
                    }
                    else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_PLAYER_IS_KING) != EngineConstants.FALSE)
                    {
                         if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ALISTAIR_KILLED) != EngineConstants.FALSE)
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08F_COMPLETE_PLAYER_KING_ALISTAIR_DIES, EngineConstants.TRUE);
                         }
                         else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_LOGHAIN_KILLED) != EngineConstants.FALSE)
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08J_COMPLETE_PLAYER_KING_LOGHAIN_DIES, EngineConstants.TRUE);
                         }
                         else
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08E_COMPLETE_PLAYER_KING_ALISTAIR_LIVES, EngineConstants.TRUE);
                         }
                    }
                    else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ANORA_IS_QUEEN) != EngineConstants.FALSE)
                    {
                         if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_ALISTAIR_KILLED) != EngineConstants.FALSE)
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08D_COMPLETE_ANORA_QUEEN_ALISTAIR_DIES, EngineConstants.TRUE);
                         }
                         else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_LOGHAIN_KILLED) != EngineConstants.FALSE)
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08I_COMPLETE_ANORA_QUEEN_LOGHAIN_DIES, EngineConstants.TRUE);
                         }
                         else
                         {
                              WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_08C_COMPLETE_ANORA_QUEEN_ALISTAIR_LIVES, EngineConstants.TRUE);
                         }
                    }
               }
               else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_MAIN, EngineConstants.LANDSMEET_EAMON_GOES_WITH_OR_WITHOUT_ALISTAIR) != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_07A_LANDSMEET_READY, EngineConstants.TRUE);
               }

               else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_SLAVE_TRADE, EngineConstants.DEN_SLAVE_TRADE_QUEST_GIVEN) != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_05A_QUESTS_AVAILABLE, EngineConstants.TRUE);
               }
               else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_CAPTURED, EngineConstants.DEN_CAPTURED_PC_CAPTURED) != EngineConstants.FALSE)
               {
                    if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_CAPTURED, EngineConstants.DEN_CAPTURED_PC_ESCAPING) != EngineConstants.FALSE)
                    {
                         WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_03B_CAPTURED_ESCAPING, EngineConstants.TRUE);
                    }
                    else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_CAPTURED, EngineConstants.DEN_CAPTURED_PARTY_COMING_IN) != EngineConstants.FALSE)
                    {
                         WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_03A_CAPTURED_BEING_RESCUED, EngineConstants.TRUE);
                    }
               }
               else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_RESCUE_THE_QUEEN, EngineConstants.DEN_RESCUE_CAUTHRIEN_SPEAKS) != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_04A_RESCUE_COMPLETE_NOT_CAPTURED, EngineConstants.TRUE);
               }
               else if (WR_GetPlotFlag(EngineConstants.PLT_DENPT_RESCUE_THE_QUEEN, EngineConstants.DEN_RESCUE_QUEST_GIVEN) != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_02_RESCUING_QUEEN, EngineConstants.TRUE);
               }
               else
               {
                    WR_SetPlotFlag(EngineConstants.PLT_MNP00PT_SSF_LANDSMEET, EngineConstants.SSF_DEN_01_ENTERED_DENERIM, EngineConstants.TRUE);
               }
          }

     }

     //moved public const string EngineConstants.DEN_STORAGE_CHEST              = "storage_chest_";
     //moved public const string DEN_DISGUISE_CHEST             = "disguise_chest_";
     //moved public const string EngineConstants.DEN_STORAGE_CHEST_PLAYER       = "player";
     //moved public const string EngineConstants.DEN_STORAGE_CHEST_PARTY        = "party";
     //moved public const string DEN_IP_PARTY_STORAGE_CHEST     = "storage_chest_party";
     //moved public const string DEN_IP_PARTY_DISGUISE_CHEST    = "disguise_chest_party";
     //moved public const string DEN_IP_PLAYER_STORAGE_CHEST    = "storage_chest_player";

     /* @brief Stores the equipment of a party member in a chest with the tag sChestPrefix + <tag of party member>.
     *
     * If oFollower is null, "party" is substituted for (tag of party member).
     * If oFollower is the player "player" is substituted for (tag of party member).
     *   EngineConstants.DEN_STORAGE_CHEST = "storage_chest_"
     *
     *
     * @param oFollower - The follower whose equipment should be stored.
     * @author Function Craig
     **/
     public void DEN_StoreInventory(GameObject oFollower = null, string sChestPrefix = EngineConstants.DEN_STORAGE_CHEST)
     {
          string sFollowerTag;
          string sChestTag;
          GameObject oPartyLeader;
          GameObject oChest;

          if (IsObjectValid(oFollower) == EngineConstants.FALSE)
          {
               sFollowerTag = EngineConstants.DEN_STORAGE_CHEST_PARTY;
          }
          else if (IsHero(oFollower) != EngineConstants.FALSE)
          {
               sFollowerTag = EngineConstants.DEN_STORAGE_CHEST_PLAYER;
          }
          else
          {
               sFollowerTag = GetTag(oFollower);
          }

          sChestTag = sChestPrefix + sFollowerTag;
          oPartyLeader = GetPartyLeader();
          oChest = UT_GetNearestObjectByTag(oPartyLeader, sChestTag);

          if (IsObjectValid(oFollower) == EngineConstants.FALSE)
          {
               StorePartyInventory(oChest);
          }
          else
          {
               StoreFollowerInventory(oFollower, oChest);
          }
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "den_functions_h.DEN_StoreInventory", "storing in chest with tag: " + GetTag(oChest));

     }

     /* @brief Restores the equipment of a party member from a chest with the tag sChestPrefix + <tag of party member>.
*
* If oFollower is null, "party" is substituted for (tag of party member)
* If oFollower is the player "player" is substituted for (tag of party member)
*
* @param oFollower - The follower whose equipment should be stored.
* @author Function Craig
**/
     public void DEN_RestoreInventory(GameObject oFollower = null, string sChestPrefix = EngineConstants.DEN_STORAGE_CHEST)
     {
          string sFollowerTag;
          string sChestTag;
          GameObject oPartyLeader;
          GameObject oChest;

          if (IsObjectValid(oFollower) == EngineConstants.FALSE)
          {
               sFollowerTag = EngineConstants.DEN_STORAGE_CHEST_PARTY;
          }
          else if (IsHero(oFollower) != EngineConstants.FALSE)
          {
               sFollowerTag = EngineConstants.DEN_STORAGE_CHEST_PLAYER;
          }
          else
          {
               sFollowerTag = GetTag(oFollower);
          }

          sChestTag = sChestPrefix + sFollowerTag;
          oPartyLeader = GetPartyLeader();
          oChest = UT_GetNearestObjectByTag(oPartyLeader, sChestTag);

          if (IsObjectValid(oFollower) == EngineConstants.FALSE)
          {
               RestorePartyInventory(oChest);
          }
          else
          {
               RestoreFollowerInventory(oFollower, oChest);
          }
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "den_functions_h.DEN_RestoreInventory", "restoring from chest with tag: " + GetTag(oChest));

     }

     public void DEN_CreateDisguise(GameObject oFollower)
     {
          int n;
          List<GameObject> arrArmor = new List<GameObject>();
          GameObject oArmor;
          GameObject oChest = UT_GetNearestObjectByTag(oFollower, EngineConstants.DEN_DISGUISE_CHEST + GetTag(oFollower));

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_CreateDisguise", "oChest: " + GetTag(oChest));

          arrArmor[0] = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_CHEST, oFollower);
          arrArmor[1] = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_GLOVES, oFollower);
          arrArmor[2] = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_BOOTS, oFollower);
          arrArmor[3] = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_HEAD, oFollower);

          for (n = 0; n < 4; n++)
          {
               MoveItem(oFollower, oChest, arrArmor[n]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_CreateDisguise", "Moving item: " + GetTag(arrArmor[n]));
          }

          arrArmor[0] = UT_AddItemToInventory(EngineConstants.DEN_IM_CAPTURED_DISGUISE);
          arrArmor[1] = UT_AddItemToInventory(EngineConstants.DEN_IM_CAPTURED_DISGUISE_BOOTS);
          arrArmor[2] = UT_AddItemToInventory(EngineConstants.DEN_IM_CAPTURED_DISGUISE_GLOVES);
          arrArmor[3] = UT_AddItemToInventory(EngineConstants.DEN_IM_CAPTURED_DISGUISE_HELM);

          for (n = 0; n < 4; n++)
          {
               oArmor = arrArmor[n];
               SetItemIrremovable(oArmor, EngineConstants.TRUE);
               SetItemIndestructible(oArmor, EngineConstants.TRUE);
               EquipItem(oFollower, oArmor);
          }

          //Gore_RemoveAllGore(oFollower);
     }

     public void DEN_CreateDisguises()
     {
          GameObject oPartyMember;
          List<GameObject> arParty = GetPartyList(GetPartyLeader());

          int nLoop;
          int nPartySize = GetArraySize(arParty);

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_CreateDisguises", "total party size: " + IntToString(nPartySize));
          for (nLoop = 0; nLoop < nPartySize; nLoop++)
          {
               oPartyMember = arParty[nLoop];
               if (GetTag(oPartyMember) != EngineConstants.GEN_FL_DOG)
               {
                    DEN_CreateDisguise(oPartyMember);
               }
          }
     }

     public void DEN_RemoveDisguise(GameObject oFollower)
     {
          int n;
          List<GameObject> arrArmor = new List<GameObject>();
          GameObject oItem;
          string sItemTag;
          GameObject oChest = UT_GetNearestObjectByTag(oFollower, EngineConstants.DEN_DISGUISE_CHEST + GetTag(oFollower));

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_RemoveDisguise", "oChest: " + GetTag(oChest));

          arrArmor[0] = GetItemPossessedBy(oFollower, EngineConstants.DEN_IT_CAPTURED_DISGUISE);
          arrArmor[1] = GetItemPossessedBy(oFollower, EngineConstants.DEN_IT_CAPTURED_DISGUISE_BOOTS);
          arrArmor[2] = GetItemPossessedBy(oFollower, EngineConstants.DEN_IT_CAPTURED_DISGUISE_GLOVES);
          arrArmor[3] = GetItemPossessedBy(oFollower, EngineConstants.DEN_IT_CAPTURED_DISGUISE_HELM);

          for (n = 0; n < GetArraySize(arrArmor); n++)
          {
               oItem = arrArmor[n];
               SetItemIrremovable(oItem, EngineConstants.FALSE);
               SetItemIndestructible(oItem, EngineConstants.FALSE);
               UnequipItem(oFollower, oItem);

               RemoveItem(oItem);
          }


          arrArmor = GetItemsInInventory(oChest);

          for (n = 0; n < GetArraySize(arrArmor); n++)
          {
               oItem = arrArmor[n];
               MoveItem(oChest, oFollower, oItem);
               EquipItem(oFollower, oItem);

               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_RemoveDisguise", "Moving item: " + GetTag(oItem));
          }
     }

     public void DEN_RemoveDisguises()
     {
          GameObject oPartyMember;
          List<GameObject> arParty = GetPartyList(GetPartyLeader());

          int nLoop;
          int nPartySize = GetArraySize(arParty);
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_RemoveDisguises", "attempting to remove disguises");
          for (nLoop = 0; nLoop < nPartySize; nLoop++)
          {
               oPartyMember = arParty[nLoop];
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_RemoveDisguises", "attempting to remove disguise for " + GetTag(oPartyMember));
               DEN_RemoveDisguise(oPartyMember);
          }
     }

     public void DEN_SetPartyDialogOverride(string rDialog)
     {
          GameObject oModule = GetModule();
          int bActive = (rDialog != EngineConstants.INVALID_RESOURCE) ? EngineConstants.TRUE : EngineConstants.FALSE;

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_SetPartyDialogOverride", "overriding dialog with: " + ResourceToString(rDialog));
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_SetPartyDialogOverride", "setting override dialog active to: " + IntToString(bActive));

          SetLocalResource(oModule, EngineConstants.PARTY_OVERRIDE_CONVERSATION, rDialog);
          SetLocalInt(oModule, EngineConstants.PARTY_OVERRIDE_CONVERSATION_ACTIVE, bActive);

          rDialog = GetLocalResource(oModule, EngineConstants.PARTY_OVERRIDE_CONVERSATION);
          bActive = GetLocalInt(oModule, EngineConstants.PARTY_OVERRIDE_CONVERSATION_ACTIVE);

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_SetPartyDialogOverride", "overriding dialog now: " + ResourceToString(rDialog));
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "den_functions_h.DEN_SetPartyDialogOverride", "override dialog active now: " + IntToString(bActive));

     }

     public void DEN_SetTeamHostility(int nTeamID, int bHostile = EngineConstants.TRUE, int bSetInteractive = EngineConstants.TRUE)
     {
          int nIndex;
          GameObject oPC = GetPartyLeader();
          GameObject oTeamMember = null;
          GameObject oNearestPartyMember;
          List<GameObject> arTeam = UT_GetTeam(nTeamID);

          if (bHostile != EngineConstants.FALSE)     // The team will go to EngineConstants.GROUP_HOSTILE
          {
               for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
               {
                    oTeamMember = arTeam[nIndex];

                    if (GetLocalInt(oTeamMember, EngineConstants.PHYSICS_DISABLED) != EngineConstants.FALSE)
                    {
                         GameObject oWP = UT_GetNearestObjectByTag(oTeamMember, EngineConstants.DEN_WAYPOINT_STRING + GetTag(oTeamMember) + EngineConstants.DEN_STAND_STRING);
                         SetPhysicsController(oTeamMember, EngineConstants.TRUE);
                         if (IsObjectValid(oWP) == EngineConstants.FALSE)
                         {
                              oWP = oTeamMember;
                         }

                         Ambient_Stop(oTeamMember);
                         SetPosition(oTeamMember, GetPosition(oTeamMember), EngineConstants.TRUE);
                    }
                    if (bSetInteractive != EngineConstants.FALSE)
                    {
                         SetObjectInteractive(oTeamMember, EngineConstants.TRUE);
                    }
                    SetImmortal(oTeamMember, EngineConstants.FALSE);

                    ClearPerceptionList(oTeamMember);
                    WR_ClearAllCommands(oTeamMember, EngineConstants.TRUE);
                    SetGroupId(oTeamMember, EngineConstants.GROUP_HOSTILE);
                    ClearPerceptionList(oTeamMember);
                    WR_ClearAllCommands(oTeamMember, EngineConstants.TRUE);
                    WR_AddCommand(oTeamMember, CommandWait(RandomFloat()));
                    Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "DEN_SetTeamHostility", GetTag(oTeamMember) + " sees player: " + IntToString(IsPerceiving(oTeamMember, oPC)));
               }
          }
          else                // The team will go to EngineConstants.GROUP_NEUTRAL
          {
               for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
               {
                    SetGroupId(arTeam[nIndex], EngineConstants.GROUP_NEUTRAL);
                    if (GetLocalInt(oTeamMember, EngineConstants.PHYSICS_DISABLED) != EngineConstants.FALSE)
                    {
                         GameObject oWP = UT_GetNearestObjectByTag(oTeamMember, EngineConstants.DEN_WAYPOINT_STRING + GetTag(oTeamMember) + EngineConstants.DEN_SIT_STRING);

                         SetPhysicsController(oTeamMember, EngineConstants.FALSE);
                         if (IsObjectValid(oWP) != EngineConstants.FALSE)
                         {
                              SetPosition(oTeamMember, GetPosition(oWP), EngineConstants.FALSE);
                         }
                         Ambient_Start(oTeamMember);
                    }
               }
          }

     }

     public void Rescue_TeamsGoHostile(int bHostile = EngineConstants.TRUE, int bSetInteractive = EngineConstants.TRUE)
     {
          GameObject oPartyMember;
          List<GameObject> arParty = GetPartyList(GetPartyLeader());

          int nLoop;
          int nPartySize = GetArraySize(arParty);

          if (bHostile != EngineConstants.FALSE)
          {
               for (nLoop = 0; nLoop < nPartySize; nLoop++)
               {
                    oPartyMember = arParty[nLoop];
                    ClearPerceptionList(oPartyMember);
                    WR_ClearAllCommands(oPartyMember, EngineConstants.TRUE);
               }
          }
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_ARMORY, bHostile, bSetInteractive);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_BARRACKS_1, bHostile, bSetInteractive);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_BARRACKS_2, bHostile, bSetInteractive);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_CAPTAIN, bHostile, bSetInteractive);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_DINING_ROOM, bHostile, bSetInteractive);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_GUARD_ROOM, bHostile, bSetInteractive);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_KENNEL, bHostile, bSetInteractive);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_MAIN_ENTRANCE, bHostile, bSetInteractive);
          //DEN_SetTeamHostility(EngineConstants.DEN_TEAM_RESCUE_MAKEOUT_GUARD, bHostile, bSetInteractive); Breaks his conversation

          if (bHostile != EngineConstants.FALSE)
          {
               for (nLoop = 0; nLoop < nPartySize; nLoop++)
               {
                    oPartyMember = arParty[nLoop];
                    ClearPerceptionList(oPartyMember);
                    WR_ClearAllCommands(oPartyMember, EngineConstants.TRUE);
               }
          }
     }

     public void Captured_TeamsGoHostile(int bHostile = EngineConstants.TRUE)
     {
          GameObject oPartyMember;
          List<GameObject> arParty = GetPartyList(GetPartyLeader());

          int nLoop;
          int nPartySize = GetArraySize(arParty);

          if (bHostile != EngineConstants.FALSE)
          {
               for (nLoop = 0; nLoop < nPartySize; nLoop++)
               {
                    oPartyMember = arParty[nLoop];
                    ClearPerceptionList(oPartyMember);
                    WR_ClearAllCommands(oPartyMember, EngineConstants.TRUE);
               }
          }
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_CAPTURED_CAPTAIN, bHostile);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_CAPTURED_COLONEL, bHostile);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_CAPTURED_EQUIPMENT_ROOM, bHostile);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_CAPTURED_GUARD_POST, bHostile);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_CAPTURED_MAIN_HALL, bHostile);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_CAPTURED_STORAGE, bHostile);
          DEN_SetTeamHostility(EngineConstants.DEN_TEAM_CAPTURED_KENNEL, bHostile);

          if (bHostile != EngineConstants.FALSE)
          {
               for (nLoop = 0; nLoop < nPartySize; nLoop++)
               {
                    oPartyMember = arParty[nLoop];
                    ClearPerceptionList(oPartyMember);
                    WR_ClearAllCommands(oPartyMember, EngineConstants.TRUE);
               }
          }
     }
}