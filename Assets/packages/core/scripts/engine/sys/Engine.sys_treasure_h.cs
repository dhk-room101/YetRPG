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
     // sys_treasure_h
     // -----------------------------------------------------------------------------
     /*
         Treasure System
     */
     // -----------------------------------------------------------------------------
     // Owner: PeterT
     // -----------------------------------------------------------------------------

     //#include"core_h"
     //#include"sys_classify_h"
     //#include"sys_rewards_h"
     //#include"2da_constants_h"
     //#include"sys_areabalance"

     //moved public const int TS_MATERIAL_COLUMN_MIN = 1;
     //moved public const int TS_MATERIAL_COLUMN_MAX = 15;
     //moved public const int TS_MONEY_MULTIPLIER = 6;
     //moved public const int TS_LEVEL_MIN = 1;
     //moved public const int TS_LEVEL_MAX = 45;

     //moved public const int TS_GLOBAL_MONEY_DROP_CHANCE = 30; //percent

     //moved public const string EngineConstants.TS_OVERRIDE_RANK = "TS_OVERRIDE_RANK";
     //moved public const string EngineConstants.TS_OVERRIDE_CATEGORY = "TS_OVERRIDE_CATEGORY";
     //moved public const string EngineConstants.TS_OVERRIDE_MONEY = "TS_OVERRIDE_MONEY";
     //moved public const string EngineConstants.TS_OVERRIDE_ITEM_NUM = "TS_OVERRIDE_ITEM";
     //moved public const string EngineConstants.TS_OVERRIDE_HIGH_CHANCE = "TS_OVERRIDE_HIGH";
     //moved public const string EngineConstants.TS_OVERRIDE_EQUIPMENT_CHANCE = "TS_OVERRIDE_EQUIPMENT";
     //moved public const string EngineConstants.TS_OVERRIDE_OBJECT_TYPE = "TS_OVERRIDE_SCALING";
     //moved public const string EngineConstants.TS_OVERRIDE_STEALING = "TS_OVERRIDE_STEALING";
     //moved public const string EngineConstants.TS_OVERRIDE_REACTIVE = "TS_OVERRIDE_REACTIVE";
     //moved public const string EngineConstants.TS_TREASURE_GENERATED = "TS_TREASURE_GENERATED";

     //moved public const string EngineConstants.TS_COLUMN_LOW_TABLE = "TS_LowTable";
     //moved public const string EngineConstants.TS_COLUMN_HIGH_TABLE = "TS_HighTable";
     //moved public const string EngineConstants.TS_COLUMN_MONEY = "TS_Money";
     //moved public const string EngineConstants.TS_COLUMN_ITEM_NUM = "TS_ItemNum";
     //moved public const string EngineConstants.TS_COLUMN_HIGH_CHANCE = "TS_HighChance";
     //moved public const string EngineConstants.TS_COLUMN_EQUIPMENT_CHANCE = "TS_EquipmentChance";

     //moved public const string EngineConstants.TS_COLUMN_PREFIX = "Prefix";
     //moved public const string EngineConstants.TS_COLUMN_RESOURCE = "Resource";
     //moved public const string EngineConstants.TS_COLUMN_STACK_SIZE = "StackSize";
     //moved public const string EngineConstants.TS_COLUMN_DO_NOT_DROP = "DoNotDrop";

     //moved public const string EngineConstants.TS_OBJECT_CREATURE = "Cre";
     //moved public const string EngineConstants.TS_OBJECT_PLACEABLE = "Plc";

     //moved public const string rMoney = "gen_im_copper.uti";

     //moved public const float EngineConstants.REACTIVE_MANA_FACTOR = 1.0f;
     //moved public const float EngineConstants.REACTIVE_TIER1_VALUE = 0.4f;
     //moved public const float EngineConstants.REACTIVE_TIER2_VALUE = 0.3f;
     //moved public const float EngineConstants.REACTIVE_TIER3_VALUE = 0.2f;
     //moved public const float EngineConstants.REACTIVE_TIER4_VALUE = 0.1f;

     //moved public const float EngineConstants.REACTIVE_HEALTH_BASE_CHANCE = 0.1f;
     //moved public const float EngineConstants.REACTIVE_MANA_BASE_CHANCE = 0.1f;
     //moved public const float EngineConstants.REACTIVE_INJURY_BASE_CHANCE = 0.2;
     //moved public const string EngineConstants.REACTIVE_TIER1_HEALTH = "gen_im_qck_health_101.uti";
     //moved public const string EngineConstants.REACTIVE_TIER2_HEALTH = "gen_im_qck_health_201.uti";
     //moved public const string EngineConstants.REACTIVE_TIER3_HEALTH = "gen_im_qck_health_301.uti";
     //moved public const string EngineConstants.REACTIVE_TIER4_HEALTH = "gen_im_qck_health_401.uti";
     //moved public const string EngineConstants.REACTIVE_TIER1_MANA = "gen_im_qck_mana_101.uti";
     //moved public const string EngineConstants.REACTIVE_TIER2_MANA = "gen_im_qck_mana_201.uti";
     //moved public const string EngineConstants.REACTIVE_TIER3_MANA = "gen_im_qck_mana_301.uti";
     //moved public const string EngineConstants.REACTIVE_TIER4_MANA = "gen_im_qck_mana_401.uti";

     // get target's treasure rank
     public int TS_GetRank(GameObject oTarget)
     {
          int nRank = GetLocalInt(oTarget, EngineConstants.TS_OVERRIDE_RANK);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Override nRank = " + ToString(nRank), gameObject);
#endif

          // if not overridden
          if (nRank == 0)
          {
               int nObjectType = GetObjectType(oTarget);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nObjectType = " + ToString(nObjectType), gameObject);
#endif

               if (nObjectType == EngineConstants.OBJECT_TYPE_CREATURE)
               {
                    nRank = GetCreatureRank(oTarget);
               }
               else if (nObjectType == EngineConstants.OBJECT_TYPE_PLACEABLE)
               {
                    nRank = GetPlaceableTreasureRank(oTarget);
               }
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nRank = " + ToString(nRank), gameObject);
#endif
          }

          return nRank;
     }

     // get target's treasure category
     public int TS_GetCategory(GameObject oTarget)
     {
          int nCategory = GetLocalInt(oTarget, EngineConstants.TS_OVERRIDE_CATEGORY);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Override nCategory = " + ToString(nCategory), gameObject);
#endif

          // if not overridden
          if (nCategory == 0)
          {
               int nObjectType = GetObjectType(oTarget);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nObjectType = " + ToString(nObjectType), gameObject);
#endif

               if (nObjectType == EngineConstants.OBJECT_TYPE_CREATURE)
               {
                    nCategory = GetCreatureTreasureCategory(oTarget);
               }
               else if (nObjectType == EngineConstants.OBJECT_TYPE_PLACEABLE)
               {
                    nCategory = GetPlaceableTreasureCategory(oTarget);
               }
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nCategory = " + ToString(nCategory), gameObject);
#endif
          }

          return nCategory;
     }

     public int TS_GetLevel(GameObject oTarget, int nRank)
     {
          int nLevel = GetLevel(oTarget);
          if (nLevel < 1)
          {
               nLevel = AB_GetAreaTargetLevel(oTarget);
               nLevel += GetM2DAInt(EngineConstants.TABLE_AUTOSCALE, "nLevelScale", nRank);
               if (nLevel < 1)
               {
                    nLevel = 1;
               }
          }
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  nLevel = " + ToString(nLevel));
#endif

          return nLevel;
     }

     // get single-drop money scaled to level and drop size
     public int TS_GetScaledMoney(int nLevel, int nDropSize)
     {
          int nMoney = EngineConstants.TS_MONEY_MULTIPLIER * nLevel * nLevel;
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    Initial nMoney = " + ToString(nMoney));
#endif

          // money base
          nMoney *= nDropSize;
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    Semi-Final nMoney = " + ToString(nMoney));
#endif

          // 80-120%
          nMoney = FloatToInt(nMoney * (0.8f + (RandomFloat() * 0.4f)));
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    Final nMoney = " + ToString(nMoney));
#endif

          return nMoney;
     }

     // scale the material of an item
     public void TS_ScaleItem(GameObject oItem, int nLevel)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    Scaling " + GetTag(oItem));
#endif

          // if appropriate type (armor, shield, melee weapon, ranged weapon)
          int nItemType = GetItemType(oItem);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nItemType = " + ToString(nItemType));
#endif
          if ((nItemType == EngineConstants.ITEM_TYPE_ARMOUR) || (nItemType == EngineConstants.ITEM_TYPE_SHIELD) || (nItemType == EngineConstants.ITEM_TYPE_WEAPON_MELEE) || (nItemType == EngineConstants.ITEM_TYPE_WEAPON_RANGED))
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Item is scalable.");
#endif

               // if not unique
               if (GetItemUnique(oItem) == EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Item is not unique.");
#endif

                    // get material progression
                    int nMaterialProgression = GetItemMaterialProgression(oItem);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nMaterialProgression = " + ToString(nMaterialProgression));
#endif
                    if (nMaterialProgression > 0)
                    {
                         // find randomized level
                         int nRandomLevel = nLevel + Engine_Random(7) - 3;
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Initial nRandomLevel = " + ToString(nRandomLevel));
#endif

                         nRandomLevel = Max(EngineConstants.TS_LEVEL_MIN, nRandomLevel);
                         nRandomLevel = Min(EngineConstants.TS_LEVEL_MAX, nRandomLevel);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Final nRandomLevel = " + ToString(nRandomLevel));
#endif

                         // find material column
                         int nColumn = ((nRandomLevel - 1) / 3) + 1;
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nColumn = " + ToString(nColumn));
#endif

                         nColumn = Max(EngineConstants.TS_MATERIAL_COLUMN_MIN, nColumn);
                         nColumn = Min(EngineConstants.TS_MATERIAL_COLUMN_MAX, nColumn);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Min-Max nColumn = " + ToString(nColumn));
#endif

                         // get material
                         int nMaterial = GetM2DAInt(EngineConstants.TABLE_MATERIAL, "Material" + ToString(nColumn), nMaterialProgression);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nMaterial = " + ToString(nMaterial));
#endif

                         // set material
                         SetItemMaterialType(oItem, nMaterial);
                    }
               }
          }
     }

     // new item number system
     public int TS_GetItemNum(float fItemNum, int nObjectType)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Generating Item Num");
#endif
          int nItemNum = 0;
          int nCount = 0;
          int nMax = FloatToInt(fItemNum) + 1;
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nMax = " + ToString(nMax));
#endif
          float fRandom;

          for (nCount = 0; nCount < nMax; nCount++)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    fItemNum = " + ToString(fItemNum));
#endif

               fRandom = RandomFloat();
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      fRandom = " + ToString(fRandom));
#endif
               if (fRandom <= fItemNum)
               {
                    fItemNum -= fRandom;
                    nItemNum++;
               }
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nItemNum = " + ToString(nItemNum));
#endif

               fItemNum -= 1.0f;

               if (fItemNum <= 0.0f)
               {
                    nCount = nMax;
               }
          }

          // placeables will always have at least one thing in them
          if ((nItemNum < 1) && (nObjectType == EngineConstants.OBJECT_TYPE_PLACEABLE))
          {
               nItemNum = 1;
          }

          return nItemNum;
     }

     // generate money based on rank and level
     public void TS_GenerateMoney(GameObject oTarget, int nRank, int nLevel, int nMoneyOverride = 0)
     {
          // if money is not turned off
          if (nMoneyOverride >= 0)
          {
               int nMoney = nMoneyOverride;
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nMoney = " + ToString(nMoney));
#endif

               nMoney = TS_GetScaledMoney(nLevel, nMoney);

               if (nMoney > 0)
               {
                    if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
                    {
                         AddCreatureMoney(nMoney, oTarget, EngineConstants.FALSE);
                    }
                    else
                    {
                         CreateItemOnObject(EngineConstants.rMoney, oTarget, nMoney);
                    }
               }
          }
     }

     // generate items on the target
     public void TS_GenerateItems(GameObject oTarget, int nRank, int nCategory, int nLevel, int nObjectType = EngineConstants.OBJECT_TYPE_CREATURE, float fNumOverride = 0.0f, float fChanceOverride = 0.0f, int bStolen = EngineConstants.FALSE)
     {
          // if minor items are not turned off
          if (fNumOverride >= 0.0f)
          {
               // max number of items
               int nItemNum;
               if (fNumOverride == 0.0f) // if not overridden
               {
                    float fItemNum = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, EngineConstants.TS_COLUMN_ITEM_NUM, nRank);
                    nItemNum = TS_GetItemNum(fItemNum, nObjectType);
               }
               else
               {
                    nItemNum = TS_GetItemNum(fNumOverride, nObjectType);
               }
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  fNumOverride = " + ToString(fNumOverride));
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nItemNum = " + ToString(nItemNum));
#endif

               // chance of item being a higher level
               float fHighChance;
               if (fChanceOverride >= 0.0f) // if not overridden
               {
                    fHighChance = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, EngineConstants.TS_COLUMN_HIGH_CHANCE, nRank);
               }
               else
               {
                    fHighChance = fChanceOverride;
               }
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  fChanceOverride = " + ToString(fChanceOverride));
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  fHighChance = " + ToString(fHighChance));
#endif

               // get treasure table constructors
               string sPrefix = GetM2DAString(EngineConstants.TABLE_CATEGORY, EngineConstants.TS_COLUMN_PREFIX, nCategory);
               string sLowTable = GetM2DAString(EngineConstants.TABLE_AUTOSCALE, EngineConstants.TS_COLUMN_LOW_TABLE, nRank);
               string sHighTable = GetM2DAString(EngineConstants.TABLE_AUTOSCALE, EngineConstants.TS_COLUMN_HIGH_TABLE, nRank);
               string sObjectTable;
               if (nObjectType == EngineConstants.OBJECT_TYPE_PLACEABLE)
               {
                    sObjectTable = EngineConstants.TS_OBJECT_PLACEABLE;
               }
               else
               {
                    sObjectTable = EngineConstants.TS_OBJECT_CREATURE;
               }

               string sConstructor = String.Empty;
               int nLineNum;
               int nLine;
               string rItem;
               int nStackSize;
               GameObject oItem;
               List<GameObject> oItemsToDrop;

               // If the first line in the treasure table is money then try to create one instance of it before going into the loop
               // There is a global chance a treasure drop will be money
               if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
               {
                    string sPreConstructor = sPrefix + sObjectTable + sLowTable;
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    PRE sConstructor = " + sConstructor);
#endif
                    string rFirstItem = GetM2DAResource(-1, EngineConstants.TS_COLUMN_RESOURCE, 0, sPreConstructor);
                    if (rFirstItem == EngineConstants.rMoney && bStolen == EngineConstants.FALSE)
                    {
                         // first, determine the global chance of having money for this table
                         int nRand = Engine_Random(100) + 1;
                         if (nRand <= EngineConstants.TS_GLOBAL_MONEY_DROP_CHANCE)
                         {
                              nStackSize = GetM2DAInt(-1, EngineConstants.TS_COLUMN_STACK_SIZE, 0, sPreConstructor);
                              nStackSize = TS_GetScaledMoney(nLevel, nStackSize);
                              AddCreatureMoney(nStackSize, oTarget, EngineConstants.FALSE);
                         }
                    }
               }

               // create each item
               int nCount = 0;
               for (nCount = 0; nCount < nItemNum; nCount++)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Creating item " + ToString(nCount));
#endif
                    sConstructor = sPrefix + sObjectTable;

                    // is it a high item
                    if (RandomFloat() <= fHighChance)
                    {
                         sConstructor += sHighTable;
                    }
                    else
                    {
                         sConstructor += sLowTable;
                    }

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    sConstructor = " + sConstructor);
#endif

                    nLineNum = GetM2DARows(-1, sConstructor);
                    nLine = Engine_Random(nLineNum);
                    nLine = GetM2DARowIdFromRowIndex(-1, nLine, sConstructor); // converting row to proper id (for PRC 2da expansions)
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nLineNum = " + ToString(nLineNum));
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nLine = " + ToString(nLine));
#endif

                    rItem = GetM2DAResource(-1, EngineConstants.TS_COLUMN_RESOURCE, nLine, sConstructor);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    rItem = " + ResourceToString(rItem));
#endif

                    // money is treated differently
                    if (rItem == EngineConstants.rMoney)
                    {
                         if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE && bStolen == EngineConstants.FALSE)
                         {
                              // First, make sure no other items were creates. If other items are about to drop then avoid adding money
                              oItemsToDrop = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK);
                              if (GetArraySize(oItemsToDrop) > 0) // items were created
                                   continue; // skip money creation
                         }

                         // get money amount
                         nStackSize = GetM2DAInt(-1, EngineConstants.TS_COLUMN_STACK_SIZE, nLine, sConstructor);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nStackSize = " + ToString(nStackSize));
#endif
                         nStackSize = TS_GetScaledMoney(nLevel, nStackSize);

                         int bNotify = IsPartyMember(oTarget);

                         if (nObjectType == EngineConstants.OBJECT_TYPE_CREATURE)
                         {
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Creature");
#endif
                              AddCreatureMoney(nStackSize, oTarget, bNotify);
                         }
                         else
                         {
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Placeable");
#endif
                              CreateItemOnObject(EngineConstants.rMoney, oTarget, nStackSize);
                         }
                    }
                    else
                    {
                         // PREVENT NON-MONEY TREASURE IF CREATURE ALREADY HAS MONEY
                         if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE && GetCreatureMoney(oTarget) > 0)
                              continue;

                         // get stack size
                         nStackSize = GetM2DAInt(-1, EngineConstants.TS_COLUMN_STACK_SIZE, nLine, sConstructor);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    nStackSize = " + ToString(nStackSize));
#endif

                         if (nStackSize > 1)
                         {
                              // round up
                              float fRandom = (RandomFloat() * 0.2f) + 0.8f;
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      fRandom = " + ToString(fRandom));
#endif
                              int nRandomSize = Max(1, (FloatToInt(fRandom * nStackSize)));
                              if (IntToFloat(nRandomSize) < (nStackSize * fRandom))
                              {
                                   nRandomSize++;
                              }
                              nStackSize = Max(nRandomSize, 1);
                         }
                         else
                         {
                              nStackSize = 1;
                         }
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Final nStackSize = " + ToString(nStackSize));
#endif

                         // create item
                         oItem = CreateItemOnObject(rItem, oTarget, nStackSize);

                         // scale item
                         TS_ScaleItem(oItem, nLevel);
                    }
               }
          }
     }

     // drop a single piece of equipment
     public void TS_GenerateEquipment(GameObject oTarget, int nRank, float fOverride = 0.0f)
     {
          if (fOverride >= 0.0f)
          {
               // get chance to drop
               float fChance;
               if (fOverride == 0.0f) // if not overridden
               {
                    fChance = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, EngineConstants.TS_COLUMN_EQUIPMENT_CHANCE, nRank);
               }
               else
               {
                    fChance = fOverride;
               }
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "fOverride = " + ToString(fOverride));
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "fChance = " + ToString(fChance));
#endif

               // if there are equipped items
               List<GameObject> oEquip = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_EQUIPPED);
               int nEquipNum = GetArraySize(oEquip);

               if (nEquipNum > 0)
               {
                    // chance of equipment dropping
                    float fRandom = RandomFloat();
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "fRandom = " + ToString(fRandom));
#endif
                    if (fRandom <= fChance)
                    {
                         // pick a single random piece of equipment
                         int nEquip = Engine_Random(nEquipNum);

                         // if item is droppable
                         int nItemType = GetBaseItemType(oEquip[nEquip]);
                         if (GetM2DAInt(EngineConstants.TABLE_ITEMS, EngineConstants.TS_COLUMN_DO_NOT_DROP, nItemType) == EngineConstants.FALSE)
                         {
                              // set item droppable
                              SetItemDroppable(oEquip[nEquip], EngineConstants.TRUE);
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "Setting Droppable -  " + GetTag(oEquip[nEquip]));
#endif
                         }
                    }
               }
          }
     }

     public void TS_ScaleInventory(GameObject oTarget, int nLevel)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Scaling Inventory");
#endif

          int nFlag;
          if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               nFlag = EngineConstants.GET_ITEMS_OPTION_BACKPACK;
          }
          else
          {
               nFlag = EngineConstants.GET_ITEMS_OPTION_ALL;
          }
          List<GameObject> oItems = GetItemsInInventory(oTarget, nFlag);
          int nMax = GetArraySize(oItems);
          int nCount = 0;
          for (nCount = 0; nCount < nMax; nCount++)
          {
               TS_ScaleItem(oItems[nCount], nLevel);
          }
     }

     public int TS_GetStackSizes(List<GameObject> oItems)
     {
          int nSize = 0;
          int nCount = 0;
          int nMax = GetArraySize(oItems);
          for (nCount = 0; nCount < nMax; nCount++)
          {
               nSize += GetItemStackSize(oItems[nCount]);
          }

          return nSize;
     }

     public int TS_GetHealthPotions(GameObject oTarget)
     {
          int nHealthCount = 0;
          List<GameObject> oPotions;

          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_health_101", EngineConstants.TRUE);
          nHealthCount += TS_GetStackSizes(oPotions);
          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_health_201", EngineConstants.TRUE);
          nHealthCount += TS_GetStackSizes(oPotions);
          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_health_301", EngineConstants.TRUE);
          nHealthCount += TS_GetStackSizes(oPotions);
          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_health_401", EngineConstants.TRUE);
          nHealthCount += TS_GetStackSizes(oPotions);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nHealthCount = " + ToString(nHealthCount));
#endif

          return nHealthCount;
     }

     public int TS_GetInjuryPotions(GameObject oTarget)
     {
          int nInjuryCount = 0;
          List<GameObject> oPotions;

          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_injury_101", EngineConstants.TRUE);
          nInjuryCount += TS_GetStackSizes(oPotions);
          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_injury_201", EngineConstants.TRUE);
          nInjuryCount += TS_GetStackSizes(oPotions);
          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_injury_301", EngineConstants.TRUE);
          nInjuryCount += TS_GetStackSizes(oPotions);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nInjuryCount = " + ToString(nInjuryCount));
#endif

          return nInjuryCount;
     }

     public int TS_GetManaPotions(GameObject oTarget)
     {
          int nManaCount = 0;
          List<GameObject> oPotions;

          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_mana_101", EngineConstants.TRUE);
          nManaCount += TS_GetStackSizes(oPotions);
          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_mana_201", EngineConstants.TRUE);
          nManaCount += TS_GetStackSizes(oPotions);
          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_mana_301", EngineConstants.TRUE);
          nManaCount += TS_GetStackSizes(oPotions);
          oPotions = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK, 0, "gen_im_qck_mana_401", EngineConstants.TRUE);
          nManaCount += TS_GetStackSizes(oPotions);
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nManaCount = " + ToString(nManaCount));
#endif

          return nManaCount;
     }

     // generate normal treasure
     public void TreasureGenerate(GameObject oTarget)
     {
          // is target a valid object
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Treasure generation for target = " + ToString(oTarget));
#endif
          if (IsObjectValid(oTarget) != EngineConstants.FALSE)
          {
               // has treasure already been generated
               int bTreasureGenerated = GetLocalInt(oTarget, EngineConstants.TS_TREASURE_GENERATED);
               if (bTreasureGenerated == EngineConstants.FALSE)
               {
                    // make sure treasure doesnt generate again
                    SetLocalInt(oTarget, EngineConstants.TS_TREASURE_GENERATED, EngineConstants.TRUE);

                    // get rank and group
                    int nRank = TS_GetRank(oTarget);
                    int nCategory = TS_GetCategory(oTarget);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Rank = " + ToString(nRank) + ", Category = " + ToString(nCategory));
#endif
                    int nLevel = TS_GetLevel(oTarget, nRank);

                    // if valid rank
                    if (nRank > 0)
                    {
                         TS_ScaleInventory(oTarget, nLevel);

                         int nObjectTypeOverride = GetLocalInt(oTarget, EngineConstants.TS_OVERRIDE_OBJECT_TYPE);
                         if (nObjectTypeOverride == 0)
                         {
                              nObjectTypeOverride = GetObjectType(oTarget);
                         }

                         // generate gold
                         int nMoneyOverride = GetLocalInt(oTarget, EngineConstants.TS_OVERRIDE_MONEY);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Money Override = " + ToString(nMoneyOverride));
#endif
                         if (nMoneyOverride > 0)
                         {
                              TS_GenerateMoney(oTarget, nRank, nLevel, nMoneyOverride);
                         }

                         // reactive potion drops
                         if (nObjectTypeOverride == EngineConstants.OBJECT_TYPE_CREATURE)
                         {
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Checking creature for reactive health drops.");
#endif

                              // if hostile to the player
                              GameObject oPC = GetHero();
                              float fReactiveOverride = GetLocalFloat(oTarget, EngineConstants.TS_OVERRIDE_REACTIVE);
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    fReactiveOverride = " + ToString(fReactiveOverride));
#endif
                              if ((IsObjectHostile(oTarget, oPC) != EngineConstants.FALSE) && (fReactiveOverride >= 0.0f))
                              {
                                   Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    Creature is hostile to party.");

                                   int nHealthCount = 0;
                                   int nManaCount = 0;

                                   // check to make sure it isnt already dropping health/mana potions
                                   nHealthCount = TS_GetHealthPotions(oTarget);
                                   nManaCount = TS_GetManaPotions(oTarget);
#if DEBUG
                                   Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Target has " + ToString(nHealthCount) + " health and " + ToString(nManaCount) + " mana");
#endif
                                   if ((nHealthCount + nManaCount) <= 0)
                                   {
                                        // get number of health/mana potions
                                        nHealthCount = TS_GetHealthPotions(oPC);
                                        nManaCount = TS_GetManaPotions(oPC);
#if DEBUG
                                        Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Player has " + ToString(nHealthCount) + " health and " + ToString(nManaCount) + " mana");
#endif

                                        // if the player doesn't already have the maximum number of potions
                                        int nDifficulty = GetGameDifficulty();
                                        int nDifficultyLimit = GetM2DAInt(EngineConstants.TABLE_DIFFICULTY, "ReactiveLimit", nDifficulty);
#if DEBUG
                                        Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      nDifficulty " + ToString(nDifficulty) + " with limit " + ToString(nDifficultyLimit));
#endif

                                        if (nHealthCount < nDifficultyLimit || nManaCount < nDifficultyLimit)
                                        {
                                             nDifficultyLimit = Max(nDifficultyLimit, 1); // to prevent divide by 0 errors

                                             // base chance
                                             float fBaseChance = (1.0f - (Min(nHealthCount, nManaCount) / nDifficultyLimit)) / 4.0f;

                                             //float fBaseChance = IntToFloat(nDifficultyLimit - Min(nHealthCount,nManaCount)) / nDifficultyLimit;

                                             // factor
                                             float fFactor = GetM2DAFloat(EngineConstants.TABLE_DIFFICULTY, "ReactiveChance", nDifficulty);

                                             // item chance
                                             float fItemChance = GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, "TS_ItemNum", nRank);
                                             fItemChance = MaxF(fItemChance, 1.0f);

                                             // drop chance
                                             float fDropChance = fBaseChance * fFactor * fItemChance;
                                             if (fReactiveOverride > 0.0f)
                                             {
                                                  fDropChance = fReactiveOverride;
                                             }

#if DEBUG
                                             Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Base Chance = " + ToString(fBaseChance));
                                             Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Factor = " + ToString(fFactor));
                                             Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Item Chance = " + ToString(fItemChance));
                                             Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Drop Chance = " + ToString(fDropChance));
#endif

                                             if (RandomFloat() < fDropChance)
                                             {
                                                  int nTier = GetM2DAInt(EngineConstants.TABLE_AUTOSCALE, "nReactiveTier", nRank);
#if DEBUG
                                                  Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Tier = " + ToString(nTier));
#endif
                                                  if (nTier > 0)
                                                  {
                                                       // the chance of health dropping increases as the player has less potions
                                                       // the chance goes between EngineConstants.REACTIVE_HEALTH_BASE_CHANCE up to 50%, the less potions, the higher the chance
                                                       float fHealthChance = (1.0f - (nHealthCount / nDifficultyLimit)) / 2.0f;
                                                       if (fHealthChance < EngineConstants.REACTIVE_HEALTH_BASE_CHANCE) fHealthChance = EngineConstants.REACTIVE_HEALTH_BASE_CHANCE;
                                                       //float fHealthChance = EngineConstants.REACTIVE_HEALTH_BASE_CHANCE + ((1.0f - EngineConstants.REACTIVE_HEALTH_BASE_CHANCE - EngineConstants.REACTIVE_MANA_BASE_CHANCE) * (1.0f - (IntToFloat(nHealthCount) / Max(nManaCount + nHealthCount, 1))));
#if DEBUG
                                                       Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        fHealthChance = " + ToString(fHealthChance));
#endif
                                                       int bMana = (RandomFloat() > fHealthChance) ? EngineConstants.TRUE : EngineConstants.FALSE;
#if DEBUG
                                                       Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        bMana = " + ToString(bMana));
#endif

                                                       if (GetLevel(GetHero()) >= 10 && GetLevel(GetHero()) < 15)
                                                       {
                                                            int nRandIncChance = Engine_Random(100) + 1;
                                                            if (nRandIncChance <= 50)
                                                                 nTier++; // 50% chance for a higher tier level 10+
                                                       }
                                                       else if (GetLevel(GetHero()) >= 15)
                                                       {
                                                            int nRandIncChance = Engine_Random(100) + 1;
                                                            if (nRandIncChance <= 75)
                                                                 nTier++; // 75% chance for a higher tier level 15+
                                                       }

                                                       string rPotion;
                                                       if (nTier >= 4)
                                                       {
#if DEBUG
                                                            Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Potent potion spawned.");
#endif

                                                            if (bMana != EngineConstants.FALSE)
                                                            {
                                                                 rPotion = EngineConstants.REACTIVE_TIER4_MANA;
                                                            }
                                                            else
                                                            {
                                                                 rPotion = EngineConstants.REACTIVE_TIER4_HEALTH;
                                                            }
                                                       }
                                                       else if (nTier == 3)
                                                       {
#if DEBUG
                                                            Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Greater potion spawned.");
#endif

                                                            if (bMana != EngineConstants.FALSE)
                                                            {
                                                                 rPotion = EngineConstants.REACTIVE_TIER3_MANA;
                                                            }
                                                            else
                                                            {
                                                                 rPotion = EngineConstants.REACTIVE_TIER3_HEALTH;
                                                            }
                                                       }
                                                       else if (nTier == 2)
                                                       {
#if DEBUG
                                                            Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Normal potion spawned.");
#endif

                                                            if (bMana != EngineConstants.FALSE)
                                                            {
                                                                 rPotion = EngineConstants.REACTIVE_TIER2_MANA;
                                                            }
                                                            else
                                                            {
                                                                 rPotion = EngineConstants.REACTIVE_TIER2_HEALTH;
                                                            }
                                                       }
                                                       else
                                                       {
#if DEBUG
                                                            Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "        Lesser potion spawned.");
#endif

                                                            if (bMana != EngineConstants.FALSE)
                                                            {
                                                                 rPotion = EngineConstants.REACTIVE_TIER1_MANA;
                                                            }
                                                            else
                                                            {
                                                                 rPotion = EngineConstants.REACTIVE_TIER1_HEALTH;
                                                            }
                                                       }
                                                       if (bMana != EngineConstants.FALSE && nManaCount < nDifficultyLimit)
                                                            CreateItemOnObject(rPotion, oTarget);
                                                       if (bMana == EngineConstants.FALSE && nHealthCount < nDifficultyLimit)
                                                            CreateItemOnObject(rPotion, oTarget);

                                                       // there is a chance for an extra injury potion in normal or casual difficulty
                                                       if (GetGameDifficulty() <= EngineConstants.GAME_DIFFICULTY_NORMAL)
                                                       {
                                                            // Only if party has no injury potion and has injuries
                                                            int bHasInjury = EngineConstants.FALSE;
                                                            List<GameObject> arParty = GetPartyList();
                                                            int nPartySize = GetArraySize(arParty);
                                                            List<xEffect> eInjuries;
                                                            GameObject oCurrent;
                                                            int x;
                                                            for (x = 0; x < nPartySize; x++)
                                                            {
                                                                 oCurrent = arParty[x];
                                                                 eInjuries = Injury_GetInjuryEffects(oCurrent);
                                                                 if (GetArraySize(eInjuries) > 0)
                                                                 {
                                                                      bHasInjury = EngineConstants.TRUE;
                                                                      break;
                                                                 }
                                                            }

                                                            int nInjuryPotionCount = TS_GetInjuryPotions(oPC);
                                                            float fRand = RandomFloat();
                                                            if (nInjuryPotionCount == 0 && bHasInjury != EngineConstants.FALSE && fRand <= EngineConstants.REACTIVE_INJURY_BASE_CHANCE)
                                                            {
                                                                 CreateItemOnObject("gen_im_qck_injury_101.uti", oTarget);
                                                            }

                                                       }
                                                  }
                                             }
                                        }
                                   }
                              }
                         }

                         // if valid category
                         if (nCategory > 0)
                         {
                              // generate items
                              float fItemNumOverride = GetLocalFloat(oTarget, EngineConstants.TS_OVERRIDE_ITEM_NUM);
                              float fItemQualityOverride = GetLocalFloat(oTarget, EngineConstants.TS_OVERRIDE_HIGH_CHANCE);
                              TS_GenerateItems(oTarget, nRank, nCategory, nLevel, nObjectTypeOverride, fItemNumOverride, fItemQualityOverride);
                         }

                         // PREVENT NON-MONEY TREASURE IF CREATURE ALREADY HAS MONEY
                         if (GetCreatureMoney(oTarget) > 0)
                              return;

                         // if a creature, generate equipment
                         if (nObjectTypeOverride == EngineConstants.OBJECT_TYPE_CREATURE)
                         {
                              // generate equipment
                              float fEquipmentOverride = GetLocalFloat(oTarget, EngineConstants.TS_OVERRIDE_EQUIPMENT_CHANCE);
                              TS_GenerateEquipment(oTarget, nRank, fEquipmentOverride);
                         }

                    }
               }
               else
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Treasure already generated for this object.");
#endif
               }
          }
          else
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Object is not valid.");
#endif
          }
     }

     // generate stolen treasure
     public void TreasureStolen(GameObject oTarget, GameObject oThief)
     {
          // is target a valid object
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Treasure generation for " + ToString(oThief) + " stealing from " + ToString(oTarget));
#endif
          if (IsObjectValid(oTarget) != EngineConstants.FALSE)
          {
               int bStolen = EngineConstants.FALSE;

               // check override
               int nStealingOverride = GetLocalInt(oTarget, EngineConstants.TS_OVERRIDE_STEALING);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Stealing Override = " + ToString(nStealingOverride));
#endif
               if (nStealingOverride >= 0)
               {
                    // get rank and group
                    int nRank = TS_GetRank(oTarget);
                    int nCategory = TS_GetCategory(oTarget);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Rank = " + ToString(nRank) + ", Category = " + ToString(nCategory));
#endif
                    int nLevel = TS_GetLevel(oTarget, nRank);

                    // if rank and category are valid
                    if ((nRank > 0) && (nCategory > 0))
                    {
                         bStolen = EngineConstants.TRUE;

                         // generate items
                         TS_GenerateItems(oThief, nRank, nCategory, nLevel, EngineConstants.OBJECT_TYPE_CREATURE, 1.0f, 0.0f, EngineConstants.TRUE);
                    }

                    if (nStealingOverride > 0)
                    {
                         bStolen = EngineConstants.TRUE;

                         // look up item in 2DA
                         string rItem = GetM2DAResource(EngineConstants.TABLE_STEALING, EngineConstants.TS_COLUMN_RESOURCE, nStealingOverride);
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Override Item = " + ResourceToString(rItem));
#endif

                         // create item
                         GameObject oItem = CreateItemOnObject(rItem, oThief, 1);

                         // scale item
                         TS_ScaleItem(oItem, nLevel);
                    }
               }

               // get inventory list
               List<GameObject> oStealables = GetItemsInInventory(oTarget, EngineConstants.GET_ITEMS_OPTION_BACKPACK);
               int nMax = GetArraySize(oStealables);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "  Items in Backpack = " + ToString(nMax));
#endif

               // cycle through all items
               int nCount = 0;
               for (nCount = 0; nCount < nMax; nCount++)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "    Examining " + GetTag(oStealables[nCount]));
#endif

                    // is item stealable?
                    if (IsItemStealable(oStealables[nCount]) != EngineConstants.FALSE)
                    {
                         bStolen = EngineConstants.TRUE;

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_LOOT, GetCurrentScriptName(), "      Stealable.");
#endif

                         // move item
                         MoveItem(oTarget, oThief, oStealables[nCount]);
                    }
               }

               // feedback message
               if (bStolen != EngineConstants.FALSE)
               {
                    UI_DisplayMessage(oThief, 3502);
               }
               else
               {
                    UI_DisplayMessage(oThief, 3514);
               }
          }
     }
}