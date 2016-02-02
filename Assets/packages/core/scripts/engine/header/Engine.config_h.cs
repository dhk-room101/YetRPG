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
     //------------------------------------------------------------------------------
     // config_h - Configuration Management
     //------------------------------------------------------------------------------
     /*
         This file manages configuration data read from core_rules.xls

     */
     //------------------------------------------------------------------------------
     // Georg Zoeller @ 2006/12/06
     //------------------------------------------------------------------------------

     //#include"2da_constants_h"

     //moved public const string EngineConstants.CONFIG_ACTIVESET = "Default";           // core_rules.xls supports 'sets' (columns) with different configurations

     // -----------------------------------------------------------------------------
     // Level of the Week development feature
     // -----------------------------------------------------------------------------

     //moved public const string LEVEL_OF_THE_WEEK_RESREF   = "lot100ar_lothering";     // tag/resref
     //moved public const string LEVEL_OF_THE_WEEK_WAYPOINT = "lot0100wp_from_wilds";   // waypoint

     // -----------------------------------------------------------------------------
     // These correspond to the rows of core_rules.xls
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.CONFIG_SETTING_INVALID           = 0;
     //moved public const int EngineConstants.CONFIG_SETTING_SPELLINTERRUPTION      = 1;  // spell interruption rule
     //moved public const int EngineConstants.CONFIG_SETTING_UPKEEP_COST_PERCENT    = 2;  // %mana cost of upkeep spells
     //moved public const int EngineConstants.CONFIG_SETTING_UPKEEP_RETURN_PERCENT  = 3;  // % of mana returned after cancelling upkeep spells
     //moved public const int EngineConstants.CONFIG_SETTING_HEALTH_REGEN_RATE      = 4; // health regen per X seconds
     //moved public const int EngineConstants.CONFIG_SETTING_HEALTH_REGEN_AMOUNT    = 5; // how much health is reagaine

     //moved public const int EngineConstants.CONFIG_VALUE_SPELLINTERRUPTION_OFF    = 0;
     //moved public const int EngineConstants.CONFIG_VALUE_SPELLINTERRUPTION_SIMPLE = 1;

     //moved public const int EngineConstants.CONFIG_CONSTANT_BLOODPOOL_FREQ = 50;      //% of creature death that spawn a bloodpool
     //moved public const float EngineConstants.CONFIG_CONSTANT_BLOODPOOL_DURATION = 20.0f;      //% of creature death that spawn a bloodpool

     //moved public const float EngineConstants.CONFIG_CONSTANT_HEARTBEAT_RATE = 1.0f;

     /* ----------------------------------------------------------------------------
     * @brief Returns an integer configuration setting from core_rules.xls
     *
     * See core_rules.xls for details
     *
     * @param nSetting  The EngineConstants.CONFIG_SETTING_* to query
     *
     * @returns  EngineConstants.CONFIG_VALUE_* or normal int depending on setting.
     * @author   Georg
     *  -----------------------------------------------------------------------------
     **/
     public int Config_GetSetting(int nSetting)
     {
          return GetM2DAInt(EngineConstants.TABLE_CORE_RULES, EngineConstants.CONFIG_ACTIVESET, nSetting);
     }

     public float Config_GetSettingFloat(int nSetting)
     {
          return IntToFloat(GetM2DAInt(EngineConstants.TABLE_CORE_RULES, EngineConstants.CONFIG_ACTIVESET, nSetting));
     }

     public int ConfigIsAutoPauseEnabled()
     {
          return (GetAutoPauseCombatStatus() != 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }
}