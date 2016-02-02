//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class EngineConstants
{
     // var_constants_h
     // Variable tables constants (for variable names)

     // TABLE: var_creature
     public const string IS_SUMMONED_CREATURE = "IS_SUMMONED_CREATURE";
     //public const string CREATURE_TEAM_ID = "CREATURE_TEAM_ID";
     public const string AI_TARGET = "AI_TARGET";
     public const string CREATURE_COUNTER_1 = "CREATURE_COUNTER_1";
     public const string CREATURE_COUNTER_2 = "CREATURE_COUNTER_2";
     public const string CREATURE_COUNTER_3 = "CREATURE_COUNTER_3";
     public const string CREATURE_DO_ONCE_A = "CREATURE_DO_ONCE_A";
     public const string CREATURE_DO_ONCE_B = "CREATURE_DO_ONCE_B";
     public const string CREATURE_LYRIUM_USE = "CREATURE_LYRIUM_USE";
     public const string SHOUTS_ACTIVE = "SHOUTS_ACTIVE"; // whether or not shouts are active for the creature
     public const string SHOUTS_CONVERSATION_OVERRIDE = "SHOUTS_CONVERSATION_OVERRIDE"; // dialog to use for shouts, if not default creature dialog
     public const string SHOUTS_DELAY = "SHOUTS_DELAY"; // delay between shouts, if not using default delay
     public const string CREATURE_SPAWN_DEAD = "CREATURE_SPAWN_DEAD";
     public const string AI_THREAT_TARGET = "AI_THREAT_TARGET";
     public const string TEMP_AI_THREAT_TARGET_0 = "TEMP_AI_THREAT_TARGET_0";
     public const string TEMP_AI_THREAT_TARGET_1 = "TEMP_AI_THREAT_TARGET_1";
     public const string TEMP_AI_THREAT_TARGET_2 = "TEMP_AI_THREAT_TARGET_2";
     public const string TEMP_AI_THREAT_TARGET_3 = "TEMP_AI_THREAT_TARGET_3";
     public const string TEMP_AI_THREAT_TARGET_4 = "TEMP_AI_THREAT_TARGET_4";
     public const string TEMP_AI_THREAT_VALUE_0 = "TEMP_AI_THREAT_VALUE_0";
     public const string TEMP_AI_THREAT_VALUE_1 = "TEMP_AI_THREAT_VALUE_1";
     public const string TEMP_AI_THREAT_VALUE_2 = "TEMP_AI_THREAT_VALUE_2";
     public const string TEMP_AI_THREAT_VALUE_3 = "TEMP_AI_THREAT_VALUE_3";
     public const string TEMP_AI_THREAT_VALUE_4 = "TEMP_AI_THREAT_VALUE_4";
     public const string AI_THREAT_HATED_RACE = "AI_THREAT_HATED_RACE";
     public const string AI_THREAT_HATED_CLASS = "AI_THREAT_HATED_CLASS";
     public const string AI_THREAT_HATED_GENDER = "AI_THREAT_HATED_GENDER";
     public const string AI_THREAT_SWITCH_TIMER = "AI_THREAT_SWITCH_TIMER";
     public const string AI_TARGET_OVERRIDE = "AI_TARGET_OVERRIDE";
     public const string AI_TARGET_OVERRIDE_DUR_COUNT = "AI_TARGET_OVERRIDE_DUR_COUNT";
     public const string TEMP_AI_TACTICS_TABLE = "TEMP_AI_TACTICS_TABLE";
     public const string AI_THREAT_TARGET_SWITCH_COUNTER = "AI_THREAT_TARGET_SWITCH_COUNTER";
     public const string AI_LAST_TACTIC = "AI_LAST_TACTIC";
     public const string MOVEMENT_TARGET = "MOVEMENT_TARGET";
     public const string MOVEMENT_COUNT = "MOVEMENT_COUNT";
     public const string MOVEMENT_STATIC = "MOVEMENT_STATIC";
     public const string MOVEMENT_STATE = "MOVEMENT_STATE";
     public const string AI_PLAYER_TARGET = "AI_PLAYER_TARGET";
     public const string AI_REGISTERED_WP = "AI_REGISTERED_WP";
     public const string AI_THREAT_SWITCH_TIMER_MIN = "AI_THREAT_SWITCH_TIMER_MIN";
     public const string TEMP_LEVELUP_TABLE = "TEMP_LEVELUP_TABLE";
     public const string TEMP_AI_TABLE = "TEMP_AI_TABLE";
     public const string CREATURE_SPAWNED = "CREATURE_SPAWNED";
     public const string AMBIENT_SYSTEM_STATE = "AMBIENT_SYSTEM_STATE";   // Bitmask of AMBIENT_SYSTEM_XXX values
     public const string AMBIENT_MOVE_PATTERN = "AMBIENT_MOVE_PATTERN";   // Ambient movement pattern constant AMBIENT_MOVE_XXX (0-7)
     public const string AMBIENT_MOVE_PREFIX = "AMBIENT_MOVE_PREFIX";    // Prefix of ambient movement destination (waypoint or creature)
     public const string AMBIENT_ANIM_PATTERN = "AMBIENT_ANIM_PATTERN";   // Ambient animation pattern - index into ambient_ai.xls
     public const string AMBIENT_ANIM_FREQ = "AMBIENT_ANIM_FREQ";      // Animation frequency x.y = min.max, -1.0 = all (in order)
     public const string AMBIENT_ANIM_PATTERN_OVERRIDE = "AMBIENT_ANIM_PATTERN_OVERRIDE"; // If non-zero, takes precedence over AMBIENT_ANIM_PATTERN
     public const string AMBIENT_ANIM_FREQ_OVERRIDE = "AMBIENT_ANIM_FREQ_OVERRIDE";    // If non-zero, takes precedence over AMBIENT_ANIM_FRE
     public const string AMBIENT_ANIM_OVERRIDE_COUNT = "AMBIENT_ANIM_OVERRIDE_COUNT";   // If non-zero, the number of times the override animation pattern is used.
     public const string AMBIENT_ANIM_STATE = "AMBIENT_ANIM_STATE";    // anim state: 0 = start move phase, -1 = start anim phase, +ve = # anims left to play
     public const string AMBIENT_MOVE_STATE = "AMBIENT_MOVE_STATE";    // loword = # of the waypoint moved to last, hiword = direction of travel
     public const string AMBIENT_MOVE_COUNT = "AMBIENT_MOVE_COUNT";    // number of NPC/WPs available to move to
     public const string AMBIENT_COMMAND = "AMBIENT_COMMAND";
     public const string AMBIENT_TICK_COUNT = "AMBIENT_TICK_COUNT";
     public const string DEBUG_TRACKING_POS = "DEBUG_TRACKING_POS";
     public const string DEBUG_TRACKING_POS_Y = "DEBUG_TRACKING_POS_Y";
     public const string SPAWN_HOSTILE_LYING_ON_GROUND = "SPAWN_HOSTILE_LYING_ON_GROUND";
     public const string SURR_CONVERSATION_OVERRIDE = "SURR_CONVERSATION_OVERRIDE";
     public const string FLAG_STOLEN_FROM = "FLAG_STOLEN_FROM";
     public const string AI_DISABLE_PATH_BLOCKED_ACTION = "AI_DISABLE_PATH_BLOCKED_ACTION";
     public const string AI_HELP_TEAM_STATUS = "AI_HELP_TEAM_STATUS";
     public const string AI_FLAG_STATIONARY = "AI_FLAG_STATIONARY";
     public const string BASE_GROUP = "BASE_GROUP";
     public const string COMBAT_LAST_WEAPON = "COMBAT_LAST_WEAPON";
     public const string FOLLOWER_SCALED = "FOLLOWER_SCALED";
     public const string AI_LIGHT_ACTIVE = "AI_LIGHT_ACTIVE";
     public const string AI_MOVE_TIMER = "AI_MOVE_TIMER";
     public const string GO_HOSTILE_ON_PERCEIVE_PC = "GO_HOSTILE_ON_PERCEIVE_PC";
     public const string CREATURE_REWARD_FLAGS = "CREATURE_REWARD_FLAGS";
     public const string AI_WAIT_TIMER = "AI_WAIT_TIMER";
     public const string CREATURE_SPAWN_HEALTH_MOD = "CREATURE_SPAWN_HEALTH_MOD";
     public const string CLIMAX_ARMY_ID = "CLIMAX_ARMY_ID";
     public const string AI_THREAT_GENERATE_EXTRA_THREAT = "AI_THREAT_GENERATE_EXTRA_THREAT";
     public const string CREATURE_DAMAGED_THE_HERO = "CREATURE_DAMAGED_THE_HERO";// Set to EngineConstants.TRUE in effect_damage_h is the creature damages the hero. Used for the Tactician Achievement.
     public const string LOOKAT_DISABLED = "LOOKAT_DISABLED";
     public const string MIN_LEVEL = "MIN_LEVEL";
     public const string CREATURE_HAS_TIMER_ATTACK = "CREATURE_HAS_TIMER_ATTACK";
     public const string PHYSICS_DISABLED = "PHYSICS_DISABLED";

     // These currently store xEffect modifiers until we move that into
     // the engine
     public const string ATTR_STR_MOD = "ATTR_STR_MOD";
     public const string ATTR_DEX_MOD = "ATTR_DEX_MOD";
     public const string ATTR_INT_MOD = "ATTR_INT_MOD";
     public const string ATTR_WIL_MOD = "ATTR_WIL_MOD";
     public const string ATTR_MAG_MOD = "ATTR_MAG_MOD";
     public const string ATTR_CON_MOD = "ATTR_CON_MOD";

     public const string ATTR_MAX_MANA_MOD = "ATTR_MAX_MANA_MOD";
     public const string ATTR_MAX_STA_MOD = "ATTR_MAX_STA_MOD";
     public const string ATTR_MAX_HEALTH_MOD = "ATTR_MAX_HEALTH_MOD";

     public const string CREATURE_RULES_FLAG0 = "CREATURE_RULES_FLAG0";

     // Hack: used to register an enemy is being targeted so followers will try
     // not to pick the same target if possible
     //public const string TEMP_AI_IS_TARGETED = "TEMP_AI_IS_TARGETED";

     // TABLE: var_module
     public const string LOG_ACTIVE = "LOG_ACTIVE";
     public const string LOG_RULES_CURRENT_LEVEL = "LOG_RULES_CURRENT_LEVEL";
     public const string LOG_AI_CURRENT_LEVEL = "LOG_AI_CURRENT_LEVEL";
     public const string LOG_PLOT_CURRENT_LEVEL = "LOG_PLOT_CURRENT_LEVEL";
     public const string LOG_SYSTEMS_CURRENT_LEVEL = "LOG_SYSTEMS_CURRENT_LEVEL";
     public const string APP_RANGE_VALUE_CRISIS = "APP_RANGE_VALUE_CRISIS";
     public const string APP_RANGE_VALUE_HOSTILE = "APP_RANGE_VALUE_HOSTILE";
     public const string APP_RANGE_VALUE_NEUTRAL = "APP_RANGE_VALUE_NEUTRAL";
     public const string APP_RANGE_VALUE_WARM = "APP_RANGE_VALUE_WARM";
     public const string APP_RANGE_VALUE_FRIENDLY = "APP_RANGE_VALUE_FRIENDLY";
     public const string APP_ROMANCE_RANGE_VALUE_INTERESTED = "APP_ROMANCE_RANGE_VALUE_INTERESTED";
     public const string APP_ROMANCE_RANGE_VALUE_CARE = "APP_ROMANCE_RANGE_VALUE_CARE";
     public const string APP_ROMANCE_RANGE_VALUE_ADORE = "APP_ROMANCE_RANGE_VALUE_ADORE";
     public const string APP_ROMANCE_RANGE_VALUE_LOVE = "APP_ROMANCE_RANGE_VALUE_LOVE";
     public const string MODULE_COUNTER_1 = "MODULE_COUNTER_1";
     public const string MODULE_COUNTER_2 = "MODULE_COUNTER_2";
     public const string MODULE_COUNTER_3 = "MODULE_COUNTER_3";
     public const string HANDLE_EVENT_RETURN = "HANDLE_EVENT_RETURN";
     public const string PARTY_SAVE_SLOT_1 = "PARTY_SAVE_SLOT_1";
     public const string PARTY_SAVE_SLOT_2 = "PARTY_SAVE_SLOT_2";
     public const string PARTY_SAVE_SLOT_3 = "PARTY_SAVE_SLOT_3";
     public const string COUNTER_MAIN_PLOTS_DONE = "COUNTER_MAIN_PLOTS_DONE";
     public const string ALISTAIR_FRIEND_TRACK = "ALISTAIR_FRIEND_TRACK";
     public const string PARTY_BANTER_ROTATION_COUNTER = "PARTY_BANTER_ROTATION_COUNTER";
     public const string PARTY_BANTER_ARRAY_SLOT_1 = "PARTY_BANTER_ARRAY_SLOT_1";
     public const string PARTY_BANTER_ARRAY_SLOT_2 = "PARTY_BANTER_ARRAY_SLOT_2";
     public const string PARTY_BANTER_ARRAY_SLOT_3 = "PARTY_BANTER_ARRAY_SLOT_3";
     public const string PARTY_BANTER_CONVERSATION_FILE = "PARTY_BANTER_CONVERSATION_FILE";
     public const string PARTY_TRIGGER_CONVERSATION_FILE = "PARTY_TRIGGER_CONVERSATION_FILE";
     public const string AMB_SYSTEM_CONVERSATION = "AMB_SYSTEM_CONVERSATION";
     public const string PARTY_TRIGGER_PLOT = "PARTY_TRIGGER_PLOT";
     public const string APP_APPROVAL_RATE_ALISTAIR = "APP_APPROVAL_RATE_ALISTAIR";
     public const string APP_APPROVAL_RATE_DOG = "APP_APPROVAL_RATE_DOG";
     public const string APP_APPROVAL_RATE_MORRIGAN = "APP_APPROVAL_RATE_MORRIGAN";
     public const string APP_APPROVAL_RATE_WYNNE = "APP_APPROVAL_RATE_WYNNE";
     public const string APP_APPROVAL_RATE_SHALE = "APP_APPROVAL_RATE_SHALE";
     public const string APP_APPROVAL_RATE_STEN = "APP_APPROVAL_RATE_STEN";
     public const string APP_APPROVAL_RATE_ZEVRAN = "APP_APPROVAL_RATE_ZEVRAN";
     public const string APP_APPROVAL_RATE_OGHREN = "APP_APPROVAL_RATE_OGHREN";
     public const string APP_APPROVAL_RATE_LELIANA = "APP_APPROVAL_RATE_LELIANA";
     public const string APP_APPROVAL_RATE_LOGHAIN = "APP_APPROVAL_RATE_LOGHAIN";
     public const string APP_APPROVAL_GIFT_COUNT_ALISTAIR = "APP_APPROVAL_GIFT_COUNT_ALISTAIR";
     public const string APP_APPROVAL_GIFT_COUNT_DOG = "APP_APPROVAL_GIFT_COUNT_DOG";
     public const string APP_APPROVAL_GIFT_COUNT_MORRIGAN = "APP_APPROVAL_GIFT_COUNT_MORRIGAN";
     public const string APP_APPROVAL_GIFT_COUNT_WYNNE = "APP_APPROVAL_GIFT_COUNT_WYNNE";
     public const string APP_APPROVAL_GIFT_COUNT_SHALE = "APP_APPROVAL_GIFT_COUNT_SHALE";
     public const string APP_APPROVAL_GIFT_COUNT_STEN = "APP_APPROVAL_GIFT_COUNT_STEN";
     public const string APP_APPROVAL_GIFT_COUNT_ZEVRAN = "APP_APPROVAL_GIFT_COUNT_ZEVRAN";
     public const string APP_APPROVAL_GIFT_COUNT_OGHREN = "APP_APPROVAL_GIFT_COUNT_OGHREN";
     public const string APP_APPROVAL_GIFT_COUNT_LELIANA = "APP_APPROVAL_GIFT_COUNT_LELIANA";
     public const string APP_APPROVAL_GIFT_COUNT_LOGHAIN = "APP_APPROVAL_GIFT_COUNT_LOGHAIN";
     public const string AI_DISABLE_TABLES = "AI_DISABLE_TABLES";
     public const string RAND_MOUNTAIN_ENCOUNTERS_SET = "RAND_MOUNTAIN_ENCOUNTERS_SET";
     public const string RAND_FOREST_ENCOUNTERS_SET = "RAND_FOREST_ENCOUNTERS_SET";
     public const string RAND_UNDERGROUND_ENCOUNTERS_SET = "RAND_UNDERGROUND_ENCOUNTERS_SET";
     public const string RAND_PLAINS_ENCOUNTERS_SET = "RAND_PLAINS_ENCOUNTERS_SET";
     public const string RAND_HIGHWAY_ENCOUNTERS_SET = "RAND_HIGHWAY_ENCOUNTERS_SET";
     public const string RAND_DENERIM_ENCOUNTERS_SET = "RAND_DENERIM_ENCOUNTERS_SET";
     public const string WM_STORED_AREA = "WM_STORED_AREA";
     public const string WM_STORED_WP = "WM_STORED_WP";
     public const string DEBUG_SKILL_CHECK_OVERRIDE = "DEBUG_SKILL_CHECK_OVERRIDE";
     public const string WORLD_MAP_TRIPS_COUNT = "WORLD_MAP_TRIPS_COUNT";
     public const string PARTY_STORE_SLOT_1 = "PARTY_STORE_SLOT_1";
     public const string PARTY_STORE_SLOT_2 = "PARTY_STORE_SLOT_2";
     public const string PARTY_STORE_SLOT_3 = "PARTY_STORE_SLOT_3";
     public const string CIR_FADE_FOLLOWER_1 = "CIR_FADE_FOLLOWER_1";
     public const string CIR_FADE_FOLLOWER_2 = "CIR_FADE_FOLLOWER_2";
     public const string CIR_FADE_FOLLOWER_3 = "CIR_FADE_FOLLOWER_3";
     public const string CLI_ALISTAIR_AGREE_LEVEL = "CLI_ALISTAIR_AGREE_LEVEL";
     public const string AI_USE_GUI_TABLES_FOR_FOLLOWERS = "AI_USE_GUI_TABLES_FOR_FOLLOWERS";
     public const string MODULE_WORLD_MAP_ENABLED = "MODULE_WORLD_MAP_ENABLED";
     public const string TUTORIAL_ENABLED = "TUTORIAL_ENABLED";
     public const string PARTY_PICKER_GUI_ALLOWED_TO_POP_UP = "PARTY_PICKER_GUI_ALLOWED_TO_POP_UP";
     public const string CLIMAX_ARMY_CURRENT_AREA_BUFFER_SIZE = "CLIMAX_ARMY_CURRENT_AREA_BUFFER_SIZE";
     public const string PARTY_LEADER_STORE = "PARTY_LEADER_STORE";
     public const string CUTSCENE_SET_PLOT = "CUTSCENE_SET_PLOT";
     public const string CUTSCENE_SET_PLOT_FLAG = "CUTSCENE_SET_PLOT_FLAG";
     public const string CUTSCENE_TALK_SPEAKER = "CUTSCENE_TALK_SPEAKER";
     public const string AI_PARTY_CLEAR_TO_ATTACK = "AI_PARTY_CLEAR_TO_ATTACK";
     public const string NTB_ARCANE_HORROR_POSITION = "NTB_ARCANE_HORROR_POSITION";
     public const string NTB_ARCANE_HORROR_NEXT_POSITION = "NTB_ARCANE_HORROR_NEXT_POSITION";
     public const string DISABLE_WORLD_MAP_ENCOUNTER = "DISABLE_WORLD_MAP_ENCOUNTER";
     public const string DEMO_ACTIVE = "DEMO_ACTIVE";
     public const string DOG_WARLIKE_COUNTER = "DOG_WARLIKE_COUNTER";
     public const string PARTY_OVERRIDE_CONVERSATION = "PARTY_OVERRIDE_CONVERSATION";
     public const string PARTY_OVERRIDE_CONVERSATION_ACTIVE = "PARTY_OVERRIDE_CONVERSATION_ACTIVE";
     public const string ABILITY_ALLY_NUMBER = "ABILITY_ALLY_NUMBER";
     public const string DEBUG_ENABLE_PARTY_ITEM_SCALING = "DEBUG_ENABLE_PARTY_ITEM_SCALING";
     public const string DEATH_HINT = "DEATH_HINT";
     public const string AREA_LOAD_HINT = "AREA_LOAD_HINT";
     public const string WORLD_MAP_CURRENT_MAP = "WORLD_MAP_CURRENT_MAP";
     public const string WORLD_MAP_CURRENT_LOCATION = "WORLD_MAP_CURRENT_LOCATION";
     public const string DISABLE_FOLLOWER_CONVERSATION = "DISABLE_FOLLOWER_CONVERSATION";
     public const string WORLD_MAP_STORED_PRE_CAMP_AREA = "WORLD_MAP_STORED_PRE_CAMP_AREA";
     public const string DISABLE_APPEARANCE_LEVEL_LIMITS = "DISABLE_APPEARANCE_LEVEL_LIMITS";

     public const string SOUND_SET_FLAGS = "SOUND_SET_FLAGS_";
     public const string SOUND_SET_FLAGS_0 = "SOUND_SET_FLAGS_0";
     public const string SOUND_SET_FLAGS_1 = "SOUND_SET_FLAGS_1";
     public const string SOUND_SET_FLAGS_2 = "SOUND_SET_FLAGS_2";

     public const string TRACKING_GAME_ID = "TRACKING_GAME_ID";
     public const string TRACKING_SEQ_NO = "TRACKING_SEQ_NO";

     public const string CHARGEN_MODE = "CHARGEN_MODE";

     // TABLE: var_trigger_ambient
     public const string AMBIENT_TRIGGER_FILTER = "AMBIENT_TRIGGER_FILTER";
     public const string AMBIENT_TARGET_FILTER = "AMBIENT_TARGET_FILTER";

     // TABLE: var_trigger_talk
     public const string TRIG_TALK_SPEAKER = "TRIG_TALK_SPEAKER";
     public const string TRIG_TALK_REPEAT = "TRIG_TALK_REPEAT";
     public const string TRIG_TALK_CONVERSATION_OVERRIDE = "TRIG_TALK_CONVERSATION_OVERRIDE";
     public const string TRIG_TALK_SET_PLOT = "TRIG_TALK_SET_PLOT";
     public const string TRIG_TALK_SET_FLAG = "TRIG_TALK_SET_FLAG";
     public const string TRIG_TALK_ACTIVE_FOR_PLOT = "TRIG_TALK_ACTIVE_FOR_PLOT";
     public const string TRIG_TALK_ACTIVE_FOR_FLAG = "TRIG_TALK_ACTIVE_FOR_FLAG";
     public const string TRIG_TALK_INACTIVE_FOR_PLOT = "TRIG_TALK_INACTIVE_FOR_PLOT";
     public const string TRIG_TALK_INACTIVE_FOR_FLAG = "TRIG_TALK_INACTIVE_FOR_FLAG";
     public const string TRIG_TALK_DISABLED = "TRIG_TALK_DISABLED";
     public const string TRIG_TALK_LISTENER = "TRIG_TALK_LISTENER";
     public const string TRIG_TALK_SET_PLOT2 = "TRIG_TALK_SET_PLOT2";
     public const string TRIG_TALK_SET_FLAG2 = "TRIG_TALK_SET_FLAG2";
     public const string TRIG_TALK_INACTIVE_FOR_PLOT2 = "TRIG_TALK_INACTIVE_FOR_PLOT2";
     public const string TRIG_TALK_INACTIVE_FOR_FLAG2 = "TRIG_TALK_INACTIVE_FOR_FLAG2";
     public const string TRIG_TALK_NO_TALK_IF_STEALTH = "TRIG_TALK_NO_TALK_IF_STEALTH";

     // TABLE: var_trigger
     public const string TRIGGER_COUNTER_1 = "TRIGGER_COUNTER_1";
     public const string TRIGGER_COUNTER_2 = "TRIGGER_COUNTER_2";
     public const string TRIGGER_COUNTER_3 = "TRIGGER_COUNTER_3";
     public const string TRIGGER_DO_ONCE_A = "TRIGGER_DO_ONCE_A";
     public const string TRIGGER_DO_ONCE_B = "TRIGGER_DO_ONCE_B";
     public const string TRIGGER_AT_DEST_TAG = "TRIGGER_AT_DEST_TAG";
     public const string TRIGGER_AT_DEST_AREA_TAG = "TRIGGER_AT_DEST_AREA_TAG";
     public const string TRIGGER_AT_DEST_AREALIST_TAG = "TRIGGER_AT_DEST_AREALIST_TAG";
     public const string TRIGGER_PARTY_TRIGGER_LOCATION = "TRIGGER_PARTY_TRIGGER_LOCATION";
     public const string TRIGGER_ENCOUNTER_TEAM = "TRIGGER_ENCOUNTER_TEAM";
     public const string TRIGGER_ROOM_TEXT = "TRIGGER_ROOM_TEXT";
     public const string TRIGGER_ACTIVATE_TEAM_HELP = "TRIGGER_ACTIVATE_TEAM_HELP";
     public const string TRIGGER_ACTIVATE_TEAM_STATIONARY_HARD = "TRIGGER_ACTIVATE_TEAM_STATIONARY_HARD";
     public const string TRIGGER_ACTIVATE_TEAM_STATIONARY_SOFT = "TRIGGER_ACTIVATE_TEAM_STATIONARY_SOFT";
     public const string TRIGGER_SPAWNED = "TRIGGER_SPAWNED";

     // TABLE: var_item
     public const string APP_ITEM_MOTIVATION = "APP_ITEM_MOTIVATION";
     public const string ITEM_COUNTER_1 = "ITEM_COUNTER_1";
     public const string ITEM_COUNTER_2 = "ITEM_COUNTER_2";
     //public const string ITEM_COUNTER_3 = "ITEM_COUNTER_3";
     public const string ITEM_SPECIALIZATION_FLAG = "ITEM_SPECIALIZATION_FLAG";
     public const string ITEM_DO_ONCE_A = "ITEM_DO_ONCE_A";
     public const string ITEM_DO_ONCE_B = "ITEM_DO_ONCE_B";
     public const string ITEM_SEND_ACQUIRED_EVENT = "ITEM_SEND_ACQUIRED_EVENT";
     public const string ITEM_SEND_LOST_EVENT = "ITEM_SEND_LOST_EVENT";
     public const string ITEM_CODEX_FLAG = "ITEM_CODEX_FLAG";
     public const string ITEM_SET = "ITEM_SET";
     public const string PROJECTILE_OVERRIDE = "PROJECTILE_OVERRIDE";

     // TABLE: var_placeable
     public const string PLC_COUNTER_1 = "PLC_COUNTER_1";
     public const string PLC_COUNTER_2 = "PLC_COUNTER_2";
     public const string PLC_COUNTER_3 = "PLC_COUNTER_3";
     public const string PLC_DO_ONCE_A = "PLC_DO_ONCE_A";
     public const string PLC_DO_ONCE_B = "PLC_DO_ONCE_B";
     public const string PLC_AT_DEST_TAG = "PLC_AT_DEST_TAG";
     public const string PLC_AT_DEST_AREA_TAG = "PLC_AT_DEST_AREA_TAG";
     public const string PLC_FLIP_COVER_CREATURE_1 = "PLC_FLIP_COVER_CREATURE_1";
     public const string PLC_FLIP_COVER_USE_COUNT = "PLC_FLIP_COVER_USE_COUNT";
     public const string PLC_AT_WORLD_MAP_ACTIVE_1 = "PLC_AT_WORLD_MAP_ACTIVE_1";
     public const string PLC_AT_WORLD_MAP_ACTIVE_2 = "PLC_AT_WORLD_MAP_ACTIVE_2";
     public const string PLC_AT_WORLD_MAP_ACTIVE_3 = "PLC_AT_WORLD_MAP_ACTIVE_3";
     public const string PLC_AT_WORLD_MAP_ACTIVE_4 = "PLC_AT_WORLD_MAP_ACTIVE_4";
     public const string PLC_AT_WORLD_MAP_ACTIVE_5 = "PLC_AT_WORLD_MAP_ACTIVE_5";
     public const string PLC_CODEX_FLAG = "PLC_CODEX_FLAG";
     public const string PLC_CODEX_PLOT = "PLC_CODEX_PLOT";
     public const string PLC_SPAWNED = "PLC_SPAWNED";
     public const string PLC_TRAP_TYPE = "PLC_TRAP_TYPE";
     public const string PLC_TRAP_DETECTED = "PLC_TRAP_DETECTED";
     public const string PLC_TRAP_OWNER = "PLC_TRAP_OWNER";
     public const string PLC_TRAP_SIGNAL_TAG = "PLC_TRAP_SIGNAL_TAG";
     public const string PLC_TRAP_REARM_DELAY = "PLC_TRAP_REARM_DELAY";
     public const string PLC_TRAP_TEAM = "PLC_TRAP_TEAM";
     public const string PLC_TRAP_DEACTIVATE = "PLC_TRAP_DEACTIVATE";
     public const string PLC_TRAP_AOE = "PLC_TRAP_AOE";
     public const string PLC_TRAP_SIGNAL_HEIGHT = "PLC_TRAP_SIGNAL_HEIGHT";
     public const string PLC_SPAWN_NON_INTERACTIVE = "PLC_SPAWN_NON_INTERACTIVE";
     public const string PLC_TRAP_ITEM = "PLC_TRAP_ITEM";

     // TABLE: var_placeable_react
     public const string PLC_REACT_OVERRIDE_USE_BEHAVIOR = "PLC_REACT_OVERRIDE_USE_BEHAVIOR";
     public const string PLC_REACT_REPEAT = "PLC_REACT_REPEAT";
     public const string PLC_REACT_USE_SET_PLOT = "PLC_REACT_USE_SET_PLOT";
     public const string PLC_REACT_USE_SET_FLAG = "PLC_REACT_USE_SET_FLAG";
     public const string PLC_REACT_DESTROY_SET_PLOT = "PLC_REACT_DESTROY_SET_PLOT";
     public const string PLC_REACT_DESTROY_SET_FLAG = "PLC_REACT_DESTROY_SET_FLAG";
     public const string PLC_REACT_INVENTORY_REMOVED_SET_PLOT = "PLC_REACT_INVENTORY_REMOVED_SET_PLOT";
     public const string PLC_REACT_INVENTORY_REMOVED_SET_FLAG = "PLC_REACT_INVENTORY_REMOVED_SET_FLAG";

     // TABLE: var_area
     public const string AREA_COUNTER_1 = "AREA_COUNTER_1";
     public const string AREA_COUNTER_2 = "AREA_COUNTER_2";
     public const string AREA_COUNTER_3 = "AREA_COUNTER_3";
     public const string AREA_COUNTER_4 = "AREA_COUNTER_4";
     public const string AREA_COUNTER_5 = "AREA_COUNTER_5";
     public const string AREA_COUNTER_6 = "AREA_COUNTER_6";
     public const string AREA_COUNTER_7 = "AREA_COUNTER_7";
     public const string AREA_COUNTER_8 = "AREA_COUNTER_8";
     public const string AREA_COUNTER_9 = "AREA_COUNTER_9";
     public const string AREA_COUNTER_10 = "AREA_COUNTER_10";
     public const string AREA_COUNTER_11 = "AREA_COUNTER_11";
     public const string AREA_COUNTER_12 = "AREA_COUNTER_12";
     public const string AREA_DO_ONCE_A = "AREA_DO_ONCE_A";
     public const string AREA_DO_ONCE_B = "AREA_DO_ONCE_B";
     public const string CODEX_PLOT_NAME_1 = "CODEX_PLOT_NAME_1";
     public const string CODEX_PLOT_FLAG_1 = "CODEX_PLOT_FLAG_1";
     public const string CODEX_PLOT_NAME_2 = "CODEX_PLOT_NAME_2";
     public const string CODEX_PLOT_FLAG_2 = "CODEX_PLOT_FLAG_2";
     public const string CODEX_PLOT_NAME_3 = "CODEX_PLOT_NAME_3";
     public const string CODEX_PLOT_FLAG_3 = "CODEX_PLOT_FLAG_3";
     public const string CODEX_PLOT_NAME_4 = "CODEX_PLOT_NAME_4";
     public const string CODEX_PLOT_FLAG_4 = "CODEX_PLOT_FLAG_4";
     public const string CODEX_PLOT_NAME_5 = "CODEX_PLOT_NAME_5";
     public const string CODEX_PLOT_FLAG_5 = "CODEX_PLOT_FLAG_5";
     public const string CODEX_PLOT_NAME_6 = "CODEX_PLOT_NAME_6";
     public const string CODEX_PLOT_FLAG_6 = "CODEX_PLOT_FLAG_6";
     public const string CODEX_PLOT_NAME_7 = "CODEX_PLOT_NAME_7";
     public const string CODEX_PLOT_FLAG_7 = "CODEX_PLOT_FLAG_7";
     public const string CODEX_PLOT_NAME_8 = "CODEX_PLOT_NAME_8";
     public const string CODEX_PLOT_FLAG_8 = "CODEX_PLOT_FLAG_8";
     public const string CODEX_PLOT_NAME_9 = "CODEX_PLOT_NAME_9";
     public const string CODEX_PLOT_FLAG_9 = "CODEX_PLOT_FLAG_9";
     public const string CODEX_PLOT_NAME_10 = "CODEX_PLOT_NAME_10";
     public const string CODEX_PLOT_FLAG_10 = "CODEX_PLOT_FLAG_10";
     public const string AREA_WORLD_MAP_ENABLED = "AREA_WORLD_MAP_ENABLED";
     public const string AREA_DEBUG = "AREA_DEBUG";
     public const string AREA_PARTY_CAMP = "AREA_PARTY_CAMP";
     public const string PARTY_PICKER_ENABLED = "PARTY_PICKER_ENABLED";
     public const string AREA_GAME_MODE_OVERRIDE = "AREA_GAME_MODE_OVERRIDE";
     public const string ENTERED_FOR_THE_FIRST_TIME = "ENTERED_FOR_THE_FIRST_TIME";
     public const string AREA_ID = "AREA_ID";
     public const string WORLD_MAP_NOTE_TAG = "WORLD_MAP_NOTE_TAG";
     public const string WORLD_MAP_TAG = "WORLD_MAP_TAG";
     public const string AREA_NOTIFICATION_SHOWN = "AREA_NOTIFICATION_SHOWN";

     // TABLE: var_merchant
     public const string MERCHANT_COUNTER_1 = "MERCHANT_COUNTER_1";
     public const string MERCHANT_COUNTER_2 = "MERCHANT_COUNTER_2";
     public const string MERCHANT_COUNTER_3 = "MERCHANT_COUNTER_3";
     public const string MERCHANT_IS_SCALED = "MERCHANT_IS_SCALED";
}