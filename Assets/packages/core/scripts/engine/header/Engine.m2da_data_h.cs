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
     // 2da_data_h
     // -----------------------------------------------------------------------------
     /*
         Purpose:
             Provide access to data stored in 2da files.
     */
     // -----------------------------------------------------------------------------
     // owner: georg zoeller
     // -----------------------------------------------------------------------------
     //#include"2da_constants_h"

     // cla_data.xls - Class Data
     //moved public const string EngineConstants.CLASS_DATA_DAMAGE_BONUS       = "DamagePerLevel";
     //moved public const string EngineConstants.CLASS_DATA_BASE_HEALTH        = "BaseHealth";
     //moved public const string EngineConstants.CLASS_DATA_BASE_MANA_STAMINA  = "BaseManaStamina";
     //moved public const string EngineConstants.CLASS_DATA_BASE_DEFENSE       = "BaseDefense";
     //moved public const string EngineConstants.CLASS_DATA_BASE_ATTACK        = "BaseAttack";
     //moved public const string EngineConstants.CLASS_DATA_ATTACK_PER_LEVEL   = "AttackPerLevel";
     //moved public const string EngineConstants.CLASS_DATA_DEFENSE_PER_LEVEL  = "DefensePerLevel";
     //moved public const string EngineConstants.CLASS_DATA_HEALTH_PER_LEVEL   = "HealthPerLevel";
     //moved public const string EngineConstants.CLASS_DATA_MANA_PER_LEVEL     = "DepletableProgression"; //also stamina

     // bitm_base.xls
     //moved public const string EngineConstants.ITEM_DATA_BASE_ARMORVALUE     = "ArmourValue";
     //moved public const string EngineConstants.ITEM_DATA_BASE_DAMAGE         = "BaseDamage";
     //moved public const string EngineConstants.ITEM_DATA_STRENGTH_MODIFIER   = "StrengthModifier";
     //moved public const string EngineConstants.ITEM_DATA_BASE_AP             = "armourpenetration";
     //moved public const string EngineConstants.ITEM_DATA_BASE_SPEED          = "dspeed";
     //moved public const string EngineConstants.ITEM_DATA_EQUIPPABLE_SLOTS    = "equippableslots";
     //moved public const string EngineConstants.ITEM_DATA_ARMOR_TYPE          = "armortype";
     //moved public const string EngineConstants.ITEM_DATA_RANGE               = "range";

     // as_data.xls - Autoscaling Data
     //moved public const string AS_RANK_SCALE_FACTOR             = "fscale";
     //moved public const string AS_RANK_HEALTH_SCALE_FACTOR     = "fHealthScale";

     // abi_base.xls
     //moved public const string EngineConstants.ABILITY_DATA_COOLDOWN           = "cooldown";

     // apr_base.xls
     //moved public const string EngineConstants.APPEARANCE_DATA_CREATURE_TYPE = "creature_type";
     //moved public const string EngineConstants.APPEARANCE_DATA_ONESHOT_KILL = "oneshotkills";
     //moved public const string EngineConstants.APPEARANCE_DATA_CAN_DO_DEATHBLOWS =  "candodeathblows";

     //ss_types.xls
     //moved public const string EngineConstants.SS_DATA_RESTRICT              = "brestrict";

     //moved public const string SUMMON_DATA_TEMPLATE          = "template";
     //moved public const string EngineConstants.SUMMON_DATA_ABILITY_0         = "ability0";
     //moved public const string EngineConstants.SUMMON_DATA_ABILITY_1         = "ability1";

     public float GetAutoScaleDataFloat(int nRank, string sType)
     {
          return GetM2DAFloat(EngineConstants.TABLE_AUTOSCALE, sType, nRank);
     }

     public float GetClassDataFloat(string sField, int nClass)
     {
          float fRet = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, sField, nClass);
          LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "GetClassDataFloat:" + sField + " nClass " + ToString(nClass) + "-> " + ToString(fRet));

          return fRet;
     }

     /*
     * @brief (2da_data)  Returns the ability type as a EngineConstants.ABILITY_TYPE_* constant
     *
     * @param nAbility   ability id (row number in EngineConstants.ABI_BASE)
     *
     * @returns EngineConstants.ABILITY_TYPE_* constant
     *
     * @author   Georg Zoeller
     **/
     public int GetAbilityType(int nAbility)
     {
          return GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "AbilityType", nAbility);
     }

     /*
     * @brief (2da_data) Returns the cooldown defined in the 2da for an ability
     *
     *   Returns a cooldown value (in seconds) that exists in EngineConstants.ABI_base.xls
     *
     * @param    nAbility   ability id (row number in EngineConstants.ABI_BASE)
     *
     * @returns  cooldown.
     *
     * @author   Georg Zoeller
     **/
     public float GetAbilityBaseCooldown(int nAbility)
     {
          return GetM2DAFloat(EngineConstants.TABLE_ABILITIES_SPELLS, EngineConstants.ABILITY_DATA_COOLDOWN, nAbility);
     }

     public float GetItemArmorBaseValue(int nBaseItemType)
     {
          return GetM2DAFloat(EngineConstants.TABLE_ITEMSTATS, EngineConstants.ITEM_DATA_BASE_ARMORVALUE, nBaseItemType);
     }

     public float GetItemStrengthModifierBaseValue(int nBaseItemType)
     {
          return GetM2DAFloat(EngineConstants.TABLE_ITEMS, EngineConstants.ITEM_DATA_STRENGTH_MODIFIER, nBaseItemType);

     }

     public float GetWeaponSpeed(int nBaseItemType)
     {
          return GetM2DAFloat(EngineConstants.TABLE_ITEMS, EngineConstants.ITEM_DATA_BASE_SPEED, nBaseItemType);
     }

     /*
     * @brief (2da_data) Returns the creature type associated with nAppearanceType
     *
     *   Returns the creature type classification associated with the given appearance
     *   type. This is not the racial type of the creature set on the creature template.
     *
     * @param    nAppearanceType   The appearance type of the creature.
     *
     * @returns  Creature type EngineConstants.CREATURE_TYPE_*
     *
     * @author   David Sims
     **/
     public int GetCreatureTypeClassification(int nAppearanceType)
     {
          return GetM2DAInt(EngineConstants.TABLE_APPEARANCE, EngineConstants.APPEARANCE_DATA_CREATURE_TYPE, nAppearanceType);
     }

     public int GetItemEquipSlotMask(int nBaseItemType)
     {
          return GetM2DAInt(EngineConstants.TABLE_ITEMS, EngineConstants.ITEM_DATA_EQUIPPABLE_SLOTS, nBaseItemType);
     }

     public float GetArmorPieceBaseValue(int nSlot, int nArmorType)
     {
          string sPiece = String.Empty;
          switch (nSlot)
          {
               case 16  /* chest */ : sPiece = "Chest"; break;
               case 32  /* helmet */: sPiece = "Helmet"; break;
               case 64  /* boots */ : sPiece = "Boots"; break;
               case 128  /* gloves*/ : sPiece = "Gloves"; break;
          }

          return GetM2DAFloat(EngineConstants.TABLE_RULES_ARMOR_DATA, sPiece, nArmorType);
     }

     public int GetItemArmorType(int nBaseItemType)
     {
          return GetM2DAInt(EngineConstants.TABLE_ITEMS, EngineConstants.ITEM_DATA_ARMOR_TYPE, nBaseItemType);
     }

     public float GetItemArmorPieceRating(GameObject oItem)
     {
          int nBase = GetBaseItemType(oItem);
          int nSlot = GetItemEquipSlotMask(nBase);
          int nArmorType = GetItemArmorType(nBase);

          float fAr = GetArmorPieceBaseValue(nSlot, nArmorType);

          //LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "AR for " + ToString(oItem) + " is:" + ToString(fAr));

          return fAr;
     }

     public int GetIsSoundSetEntryTypeRestricted(int nSSEntry)
     {
          return GetM2DAInt(EngineConstants.TABLE_SOUNDSETS, EngineConstants.SS_DATA_RESTRICT, nSSEntry);
     }

     public float GetItemRange(GameObject oItem)
     {
          int nBaseItemType = GetBaseItemType(oItem);
          return GetM2DAFloat(EngineConstants.TABLE_ITEMS, EngineConstants.ITEM_DATA_RANGE, nBaseItemType);
     }

     public int IsOneShotKillCreature(GameObject oCreature)
     {
          int nApp = GetAppearanceType(oCreature);
          return (GetM2DAInt(EngineConstants.TABLE_APPEARANCE, EngineConstants.APPEARANCE_DATA_ONESHOT_KILL, nApp));

     }

     public int CanPerformDeathblows(GameObject oCreature)
     {
          int nApp = GetAppearanceType(oCreature);
          return (GetM2DAInt(EngineConstants.TABLE_APPEARANCE, EngineConstants.APPEARANCE_DATA_CAN_DO_DEATHBLOWS, nApp));

     }
}