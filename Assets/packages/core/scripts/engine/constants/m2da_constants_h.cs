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
     //
     // constants_h
     // All DA 2da-related constants.
     // Any constants in here should be synched with a specific 2da

     // -----------------------------------------------------------------------------
     // 2DA tables names - 2da_server.xls
     // -----------------------------------------------------------------------------
     public const int TABLE_PACKAGES = 163;
     public const int TABLE_ITEMPRPS = 107;
     public const int TABLE_EXPERIENCE = 164;
     public const int TABLE_EFFECT_IMMUNITIES = 165;
     public const int TABLE_RANGES = 134;
     public const int TABLE_ABILITIES_TALENTS = 1;
     public const int TABLE_ITEMS = 6;
     public const int TABLE_MATERIAL_TYPES = 89;
     public const int TABLE_APPROVAL_NORMAL_RANGES = 166;
     public const int TABLE_APPROVAL_ROMANCE_RANGES = 167;
     public const int TABLE_APPROVAL_MOTIVATIONS = 168;
     public const int TABLE_ABILITIES_SPELLS = 1;
     public const int TABLE_CORE_RULES = 169;
     public const int TABLE_EFFECTS = 136;
     public const int TABLE_COMMANDS = 94;
     public const int TABLE_EVENTS = 114;
     public const int TABLE_AMBIENT = 170;
     public const int TABLE_BASE_ANIMATIONS = 0;
     public const int TABLE_COMBAT_ANIMATIONS = 0;
     public const int TABLE_TACTICS_CONDITIONS = 137;
     public const int TABLE_APPEARANCE = 2;
     public const int TABLE_WORLD_MAPS = 171;
     public const int TABLE_RULES_RACES = 5;
     public const int TABLE_RULES_CLASSES = 3;
     public const int TABLE_RULES_ARMOR_DATA = 172;
     public const int TABLE_RULES_BACKGROUNDS = 97;
     public const int TABLE_SOUNDSETS = 138;
     public const int TABLE_EFFECT_REGENERATION = 173;
     public const int TABLE_CREATURERANKS = 174;
     public const int TABLE_AUTOSCALE = 175;
     public const int TABLE_AUTOSCALE_EASY = 279;
     public const int TABLE_AUTOSCALE_HARD = 280;
     public const int TABLE_AUTOSCALE_DATA = 176;
     public const int TABLE_PLACEABLE_TYPES = 93;
     public const int TABLE_AREA_MUSIC = 177;
     public const int TABLE_CLOUDS = 178;
     public const int TABLE_ATMOSPHERE = 179;
     public const int TABLE_UI_MESSAGES = 180;
     public const int TABLE_QBAR = 181;
     public const int TABLE_TRAPS = 182;
     public const int TABLE_FOG = 183;
     public const int TABLE_ITEM_SETS = 184;
     public const int TABLE_TACTICS_BASE_CONDITIONS = 139;
     public const int TABLE_AI_TACTICS_TARGET_TYPE = 140;
     public const int TABLE_TACTICS_USER_PRESETS = 214;
     public const int TABLE_RULES_DISEASES = 185;
     public const int TABLE_PLOTACTIONS = 142;
     public const int TABLE_AI_ABILITY_COND = 186;
     public const int TABLE_PLOT_TYPES = 108;
     public const int TABLE_AI_BEHAVIORS = 187;
     public const int TABLE_ABILITY_DATA = 189;
     public const int TABLE_ABILITY_EFFECTS = 190;
     public const int TABLE_CHARACTERS = 191;
     public const int TABLE_ITEMSTATS = 110;
     public const int TABLE_CRAFTING = 144;
     public const int TABLE_TREASURES = 192;
     public const int TABLE_SHAPECHANGE = 188;
     public const int TABLE_SCREEN_SHAKE = 193;
     public const int TABLE_FRAMEBUFFER = 194;
     public const int TABLE_PROVING_FIGHTS = 195;
     public const int TABLE_PROVING_TYPES = 196;
     public const int TABLE_SPELL_CONJURINGSPEED = 135;
     public const int TABLE_COMMAND_TYPES = 141;
     public const int TABLE_RULES_INJURIES = 160;
     public const int TABLE_REWARDS = 197;
     public const int TABLE_VFX_PERSISTENT = 96;
     public const int TABLE_CATEGORY = 198;
     public const int TABLE_MATERIAL = 199;
     public const int TABLE_STEALING = 200;
     public const int TABLE_GIFT_VALUES = 201;
     public const int TABLE_DIFFICULTY = 211;

     public const int TABLE_CLIMAX_ARMIES = 216;
     public const int TABLE_CLIMAX_ALIENAGE_ARMY = 217;
     public const int TABLE_CLIMAX_MARKET_ARMY = 218;
     public const int TABLE_CLIMAX_PALACE_ARMY = 219;
     public const int TABLE_CLIMAX_GATESATT_ARMY = 220;
     public const int TABLE_CLIMAX_GATESDEF_ARMY = 221;
     public const int TABLE_CLIMAX_GOODGUYS_ARMY = 222;
     public const int TABLE_APP_FOLLOWER_BONUSES = 226;

     public const int TABLE_SUMMONS = 1000;
     public const int TABLE_STARTING_EQUIPMENT = 1001;

     public const int TABLE_ACHIEVEMENTS = 268;
     public const int TABLE_PROJECTILES = 1006;
     public const int TABLE_DAMAGETYPES = 1008;
     // -----------------------------------------------------------------------------
     // SoundSets
     // -----------------------------------------------------------------------------
     public const int SS_COMBAT_ATTACK = 1;
     public const int SS_COMBAT_BATTLE_CRY = 2;
     public const int SS_COMBAT_STAMINA_LOW = 3;
     public const int SS_MANA_LOW = 4;
     public const int SS_COMBAT_HEAL_ME = 5;
     public const int SS_EXPLORE_HEAL_ME = 6;
     public const int SS_SCRIPTED_HELP = 7;
     public const int SS_EXPLORE_ENEMIES_SIGHTED_UNDEAD = 8;
     public const int SS_EXPLORE_ENEMIES_SIGHTED_DARKSPAWN = 9;
     public const int SS_EXPLORE_ENEMIES_SIGHTED_DRAGON = 10;
     public const int SS_EXPLORE_ENEMIES_SIGHTED_ANIMAL = 11;
     public const int SS_EXPLORE_ENEMIES_SIGHTED_BEAST = 12;
     public const int SS_EXPLORE_ENEMIES_SIGHTED_OTHER = 13;
     public const int SS_COMBAT_TAUNT = 14;
     public const int SS_COMBAT_ATTACK_GRUNT = 15;
     public const int SS_COMBAT_PAIN_GRUNT = 16;
     public const int SS_COMBAT_NEAR_DEATH = 17;
     public const int SS_COMBAT_DEATH = 18;
     public const int SS_POISONED = 19;
     public const int SS_SPELL_FAILED = 20;
     public const int SS_COMBAT_ENEMY_KILLED = 21;
     public const int SS_COMBAT_MONSTER_SLEW_PARTY_MEMBER = 22;
     public const int SS_COMBAT_CHEER_PARTY = 23;
     public const int SS_COMBAT_WEAPON_INEFFECTIVE = 24;
     public const int SS_EXPLORE_TRAP_DETECTED = 25;
     public const int SS_EXPLORE_LOOK_HERE = 26;
     public const int SS_EXPLORE_MOVE_OVER = 27;
     public const int SS_EXPLORE_START_TASK = 28;
     public const int SS_EXPLORE_STEALTH = 29;
     public const int SS_CANNOT_DO = 30;
     public const int SS_TASK_COMPLETE = 31;
     public const int SS_COMBAT_SELECT_NEUTRAL = 32;
     //public const int SS_COMBAT_SELECT_FRIENDLY         = 33;     // CUT
     //public const int SS_COMBAT_SELECT_HATE             = 34;     // CUT
     //public const int SS_COMBAT_SELECT_LOVE             = 35;     // CUT
     public const int SS_EXPLORE_SELECT_NEUTRAL = 36;
     public const int SS_EXPLORE_SELECT_FRIENDLY = 37;
     public const int SS_EXPLORE_SELECT_HATE = 38;
     public const int SS_EXPLORE_SELECT_LOVE = 39;
     //public const int SS_ARMOR_IMPROVEMENT              = 40;     // CUT
     //public const int SS_WEAPON_IMPROVEMENT             = 41;     // CUT
     public const int SS_GIFT_NEUTRAL = 42;
     public const int SS_GIFT_NEGATIVE = 43;
     public const int SS_GIFT_POSITIVE = 44;
     public const int SS_GIFT_ECSTATIC = 45;
     public const int SS_HELLO = 46;
     public const int SS_YES = 47;
     public const int SS_NO = 48;
     public const int SS_STOP = 49;
     public const int SS_BORED = 50;
     public const int SS_GOODBYE = 51;
     public const int SS_THANK_YOU = 52;
     public const int SS_LAUGH = 53;
     public const int SS_CUSS = 54;
     public const int SS_CHEER = 55;
     public const int SS_SOMETHING_TO_SAY = 56;
     public const int SS_GOOD_IDEA = 57;
     public const int SS_BAD_IDEA = 58;
     public const int SS_THREATEN = 59;
     public const int SS_BERSERK = 60;
     public const int SS_WARCRY = 61;
     public const int SS_CAUGHT_STEALING = 62;
     public const int SS_NO_WEAPON = 63;
     public const int SS_ORDER_RECIEVED = 64;
     public const int SS_EXPLORE_ENEMIES_SIGHTED_DEMON = 65;
     public const int SS_SKILL_FAILURE = 66;

     // -----------------------------------------------------------------------------
     // Injury System
     // -----------------------------------------------------------------------------
     // This marks the start of the injury ability id block;
     public const int INJURY_ABILITY_EFFECT_ID = 500000;
     // this marks the maximum possible number of injuries ever possible in the 2da
     public const int INJURY_MAX_DEFINES = 100;

     // -----------------------------------------------------------------------------
     // Placeables states (placeables.xls)
     // -----------------------------------------------------------------------------
     public const int PLC_STATE_AREA_TRANSITION_UNLOCKED = 0;
     public const int PLC_STATE_AREA_TRANSITION_LOCKED = 1;
     public const int PLC_STATE_BRIDGE_ACTIVE = 0;
     public const int PLC_STATE_BRIDGE_INACTIVE = 1;
     public const int PLC_STATE_BRIDGE_DESTROYED = 2;
     public const int PLC_STATE_FURNITURE_IDLE = 0;
     public const int PLC_STATE_FURNITURE_DEAD = 1;
     public const int PLC_STATE_INFORMATIONAL_ACTIVE = 0;
     public const int PLC_STATE_AOE_IDLE = 0;
     public const int PLC_STATE_AOE_TRIGGERED = 1;
     public const int PLC_STATE_AOE_DEAD = 2;
     public const int PLC_STATE_FLIPCOVER_IDLE = 0;
     public const int PLC_STATE_FLIPCOVER_COVER = 1;
     public const int PLC_STATE_FLIPCOVER_DEAD = 2;
     public const int PLC_STATE_TRAP_TRIGGER_ENABLED = 0;
     public const int PLC_STATE_TRAP_TRIGGER_DISABLED = 1;
     public const int PLC_STATE_TRAP_TRIGGER_ACTIVE = 2;
     public const int PLC_STATE_TRAP_NONSELECTABLE_IDLE = 0;
     public const int PLC_STATE_TRAP_NONSELECTABLE_ACTIVE = 1;
     public const int PLC_STATE_TRAP_SELECTABLE_IDLE = 0;
     public const int PLC_STATE_TRAP_SELECTABLE_ACTIVE = 1;
     public const int PLC_STATE_TRAP_SELECTABLE_DEAD = 2;
     public const int PLC_STATE_PUZZLE_ACTIVE = 0;
     public const int PLC_STATE_PUZZLE_DEAD = 1;
     public const int PLC_STATE_CAGE_UNLOCKED = 0;
     public const int PLC_STATE_CAGE_LOCKED = 1;
     public const int PLC_STATE_CAGE_OPEN = 2;
     public const int PLC_STATE_CAGE_DEAD = 3;
     public const int PLC_STATE_BODYBAG_UNLOCKED = 0;
     public const int PLC_STATE_CONTAINER_STATIC_UNLOCKED = 0;
     public const int PLC_STATE_CONTAINER_STATIC_LOCKED = 1;
     public const int PLC_STATE_CONTAINER_STATIC_DEAD = 2;
     public const int PLC_STATE_CONTAINER_UNLOCKED = 0;
     public const int PLC_STATE_CONTAINER_LOCKED = 1;
     public const int PLC_STATE_CONTAINER_OPEN = 2;
     public const int PLC_STATE_CONTAINER_DEAD = 3;
     public const int PLC_STATE_TRIGGER_ACTIVE = 0;
     public const int PLC_STATE_TRIGGER_INACTIVE = 1;
     public const int PLC_STATE_TRIGGER_DEAD = 2;
     public const int PLC_STATE_DOOR_UNLOCKED = 0;
     public const int PLC_STATE_DOOR_LOCKED = 1;
     public const int PLC_STATE_DOOR_OPEN = 2;
     public const int PLC_STATE_DOOR_DEAD = 3;
     public const int PLC_STATE_DOOR_OPEN_2 = 4;
     public const int PLC_STATE_DOOR_DEAD_2 = 5;
     public const int PLC_STATE_STATIC_IDLE = 0;
     public const int PLC_STATE_STATIC_DEAD = 1;

     // -----------------------------------------------------------------------------
     // Placeable state controllers (placeables.xls)
     // -----------------------------------------------------------------------------
     public const string PLC_STATE_CONTROLLER_BRIDGE = "StateCnt_Bridge";
     public const string PLC_STATE_CONTROLLER_TRANS = "StateCnt_AreaTransition";
     public const string PLC_STATE_CONTROLLER_FURNITURE = "StateCnt_Furniture";
     public const string PLC_STATE_CONTROLLER_INFO = "StateCnt_Informational";
     public const string PLC_STATE_CONTROLLER_DOOR = "StateCnt_Door";
     public const string PLC_STATE_CONTROLLER_TRAP_TRIGGGER = "StateCnt_Trap_Trigger";
     public const string PLC_STATE_CONTROLLER_AOE = "StateCnt_AOE";
     public const string PLC_STATE_CONTROLLER_TRAP_SELECTABLE = "StateCnt_Selectable_Trap";
     public const string PLC_STATE_CONTROLLER_TRAP_NONSELECTABLE = "StateCnt_NonSelectable_Trap";
     public const string PLC_STATE_CONTROLLER_CONTAINER = "StateCnt_Container";
     public const string PLC_STATE_CONTROLLER_CONTAINER_STATIC = "StateCnt_Container_Static";

     // -----------------------------------------------------------------------------
     // Races -- RACE_base.xls
     // -----------------------------------------------------------------------------
     public const int RACE_INVALID = 0;
     public const int RACE_DWARF = 1;
     public const int RACE_ELF = 2;
     public const int RACE_HUMAN = 3;
     public const int RACE_QUNARI = 4;
     public const int RACE_ANIMAL = 5;
     public const int RACE_BEAST = 6;
     public const int RACE_DARKSPAWN = 7;
     public const int RACE_DRAGON = 8;
     public const int RACE_GOLEM = 9;
     public const int RACE_SPIRIT = 10;
     public const int RACE_UNDEAD = 11;

     // -----------------------------------------------------------------------------
     //
     // -----------------------------------------------------------------------------

     public const int MAX_SKILL_RANKS = 4;

     // -----------------------------------------------------------------------------
     // Backgrounds - rules/backgrounds.xls
     // -----------------------------------------------------------------------------

     public const int BACKGROUND_DALISH = 1;
     public const int BACKGROUND_COMMONER = 2;
     public const int BACKGROUND_CITY = 3;
     public const int BACKGROUND_MAGI = 4;
     public const int BACKGROUND_NOBLE = 5;

     // -----------------------------------------------------------------------------
     // Ability Types - ABI_Base.xls
     // -----------------------------------------------------------------------------
     public const int ABILITY_TYPE_INVALID = 0;
     public const int ABILITY_TYPE_TALENT = 1;
     public const int ABILITY_TYPE_SPELL = 2;
     public const int ABILITY_TYPE_SKILL = 3;
     public const int ABILITY_TYPE_ITEM = 4;

     // -----------------------------------------------------------------------------
     // Combat animations - ANIM_Combat.xls
     // -----------------------------------------------------------------------------
     public const int COMBAT_ANIMATION_ENTER_BERSERK = 144;
     public const int COMBAT_ANIMATION_SHIELDBASH_KNOCKDOWN_DAMAGE = 148;
     public const int COMBAT_ANIMATION_SHIELDBASH_KNOCKDOWN_GET_UP = 152;
     public const int COMBAT_ANIMATION_SHIELDBASH_MISS = 150;

     // -----------------------------------------------------------------------------
     // Base animations - ANIM_Base.xls
     // -----------------------------------------------------------------------------
     public const int BASE_ANIMATION_SLEEP = 619;
     public const int BASE_ANIMATION_PRAY_ENTER = 650;
     public const int BASE_ANIMATION_DEAD_1 = 653;
     public const int BASE_ANIMATION_DEAD_2 = 654;
     public const int BASE_ANIMATION_DEAD_3 = 655;
     public const int BASE_ANIMATION_DEAD_4 = 656;

     // -----------------------------------------------------------------------------
     // Groups - toolset_groups.2da
     // -----------------------------------------------------------------------------
     public const int GROUP_PC = 0;
     public const int GROUP_HOSTILE = 1;
     public const int GROUP_FRIENDLY = 2;
     public const int GROUP_NEUTRAL = 3;
     public const int GROUP_HOSTILE_ON_GROUND = 36;
     public const int GROUP_ZEVRAN_HOSTILE = 55;

     // -----------------------------------------------------------------------------
     // Approval System Motivations - rules/approval.xls
     // -----------------------------------------------------------------------------
     public const int APP_MOTIVATION_INVALID = 0;
     public const int APP_MOTIVATION_ANIMAL = 1;
     public const int APP_MOTIVATION_FAITH = 2;
     public const int APP_MOTIVATION_GLORY = 3;
     public const int APP_MOTIVATION_GOOD = 4;
     public const int APP_MOTIVATION_GREED = 5;
     public const int APP_MOTIVATION_HONOR = 6;
     public const int APP_MOTIVATION_KNOWLEDGE = 7;
     public const int APP_MOTIVATION_PLEASURE = 8;
     public const int APP_MOTIVATION_POWER = 9;

     // -----------------------------------------------------------------------------
     // Approval System ranges - rules/approval.xls
     // -----------------------------------------------------------------------------
     public const int APP_RANGE_INVALID = 0;
     public const int APP_RANGE_CRISIS = 1;
     public const int APP_RANGE_HOSTILE = 2;
     public const int APP_RANGE_NEUTRAL = 3;
     public const int APP_RANGE_WARM = 4;
     public const int APP_RANGE_FRIENDLY = 5;

     // -----------------------------------------------------------------------------
     // Approval System romance ranges - rules/approval.xls
     // -----------------------------------------------------------------------------
     public const int APP_ROMANCE_RANGE_INVALID = 0;
     public const int APP_ROMANCE_RANGE_INTERESTED = 6;
     public const int APP_ROMANCE_RANGE_CARE = 7;
     public const int APP_ROMANCE_RANGE_ADORE = 8;
     public const int APP_ROMANCE_RANGE_LOVE = 9;

     // -----------------------------------------------------------------------------
     // Target type - targettypes.xls
     // -----------------------------------------------------------------------------
     public const int TARGET_TYPE_INVALID = 0;
     public const int TARGET_TYPE_SELF = 1;
     public const int TARGET_TYPE_FRIENDLY_CREATURE = 2;
     public const int TARGET_TYPE_HOSTILE_CREATURE = 4;
     public const int TARGET_TYPE_PLACEABLE = 8;
     public const int TARGET_TYPE_AOE = 16;
     public const int TARGET_TYPE_GROUND = 32;
     public const int TARGET_TYPE_BODY = 64;

     // -----------------------------------------------------------------------------
     // Visual Effects - VFX_base.xls
     // -----------------------------------------------------------------------------
     public const int VFX_INVALID = 0;
     public const int VFX_TEST = 1;
     public const int VFX_PARTY_MEMBER_GROUND_RING = 2;
     public const int VFX_NON_PARTY_MEMBER_GROUND = 3;
     public const int VFX_HOSTILE_GROUND_RING = 4;
     public const int VFX_BLOOD_IMPACT = 5;
     public const int VFX_CRITICAL_BLOOD_IMPACT = 6;
     public const int VFX_DUST_IMPACT = 7;
     public const int VFX_SPARKS_IMPACT = 8;
     public const int VFX_CAST_TEST = 9;
     public const int VFX_IMMOLATE = 10;
     public const int VFX_IMMOLATE_NO_CRUST = 27;
     public const int VFX_RAIN = 4010;

     // -----------------------------------------------------------------------------
     // Damage Types
     // -----------------------------------------------------------------------------
     public const int DAMAGE_TYPE_INVALID = 0;
     public const int DAMAGE_TYPE_PHYSICAL = 1;
     public const int DAMAGE_TYPE_FIRE = 2;
     public const int DAMAGE_TYPE_COLD = 3;
     public const int DAMAGE_TYPE_ELECTRICITY = 4;
     public const int DAMAGE_TYPE_NATURE = 5;
     public const int DAMAGE_TYPE_PLOT = 6;
     public const int DAMAGE_TYPE_TBD = 7;  //debug
     public const int DAMAGE_TYPE_SPIRIT = 8;  //debug
     public const int DAMAGE_TYPE_DOT = 9;  //DoT damage is 'pre resisted'

     // -----------------------------------------------------------------------------
     // Special Case Stuff, ask Georg
     // These are used by the game, but do not map directly to 2da rows, instead
     // they map the 2da id ranges used in ABI_BASE...
     // -----------------------------------------------------------------------------
     public const int INVALID_SPELL = 10000;
     public const int INVALID_TALENT = 0;
     public const int INVALID_ITEM_ABILITY = 200000;
     public const int INVALID_SKILL = 100000;

     public const int ABI_BASE_TALENT_RANGE_START = INVALID_TALENT;
     public const int ABI_BASE_SPELL_RANGE_START = INVALID_SPELL;
     public const int ABI_BASE_ITEM_RANGE_START = INVALID_ITEM_ABILITY;
     public const int ABI_BASE_SKILL_RANGE_START = INVALID_SKILL;

     // ----------------------------------------------------------------------------*
     // AI Levels - leaving the constants like in code as I'm the only one supposed
     //             to ever deal with this -- Georg
     // ----------------------------------------------------------------------------*
     public const int CSERVERAIMASTER_AI_LEVEL_VERY_HIGH = 4;
     public const int CSERVERAIMASTER_AI_LEVEL_HIGH = 3;
     public const int CSERVERAIMASTER_AI_LEVEL_NORMAL = 2;
     public const int CSERVERAIMASTER_AI_LEVEL_LOW = 1;
     public const int CSERVERAIMASTER_AI_LEVEL_VERY_LOW = 0;
     public const int CSERVERAIMASTER_AI_LEVEL_INVALID = -1;

     // -----------------------------------------------------------------------------
     // BITEM_base.xls
     // -----------------------------------------------------------------------------
     public const int BASE_ITEM_TYPE_SHORTBOW = 19;
     public const int BASE_ITEM_TYPE_LONGBOW = 20;
     public const int BASE_ITEM_TYPE_ARMOR_LIGHT = 9;
     public const int BASE_ITEM_TYPE_ARMOR_MEDIUM = 10;
     public const int BASE_ITEM_TYPE_ARMOR_HEAVY = 11;
     public const int BASE_ITEM_TYPE_ARMOR_MASSIVE = 12;
     public const int BASE_ITEM_TYPE_ARMOR_SUPERMASSIVE = 22;
     public const int BASE_ITEM_TYPE_STAFF = 16;
     public const int BASE_ITEM_TYPE_QUICK = 39;
     public const int BASE_ITEM_TYPE_GIFT = 109;

     // -----------------------------------------------------------------------------
     // BITEM_base.xls::_ItemTypes
     // -----------------------------------------------------------------------------
     public const int ITEM_TYPE_INVALID = 0;
     public const int ITEM_TYPE_MISC = 1;
     public const int ITEM_TYPE_WEAPON_MELEE = 2;
     public const int ITEM_TYPE_SHIELD = 3;
     public const int ITEM_TYPE_ARMOUR = 4;
     public const int ITEM_TYPE_WEAPON_WAND = 5;
     public const int ITEM_TYPE_WEAPON_RANGED = 6;

     // move to script.ldf

     public const int PROPERTY_ATTRIBUTE_MISSILE_SHIELD = 22;
     public const int PROPERTY_ATTRIBUTE_REGENERATION_HEALTH_COMBAT = 28;
     public const int PROPERTY_ATTRIBUTE_REGENERATION_STAMINA = 29;
     public const int PROPERTY_ATTRIBUTE_REGENERATION_STAMINA_COMBAT = 30;
     public const int PROPERTY_SIMPLE_CURRENT_CLASS = 27;
     public const int PROPERTY_ATTRIBUTE_RESISTANCE_MENTAL = 32;
     public const int PROPERTY_ATTRIBUTE_ATTACK_SPEED_MODIFIER = 31;
     public const int PROPERTY_ATTRIBUTE_RESISTANCE_PHYSICAL = 33;
     public const int PROPERTY_ATTRIBUTE_FLANKING_ANGLE = 20;
     public const int PROPERTY_ATTRIBUTE_DAMAGE_SCALE = 12;

     public const int PROPERTY_SIMPLE_ATTRIBUTE_POINTS = 34;
     public const int PROPERTY_SIMPLE_SKILL_POINTS = 35;
     public const int PROPERTY_SIMPLE_TALENT_POINTS = 36;
     public const int PROPERTY_SIMPLE_BACKGROUND = 37;
     public const int PROPERTY_SIMPLE_SPEC_POINTS = 38;

     public const int PROPERTY_ATTRIBUTE_DAMAGE_BONUS = 39;
     public const int PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_FIRE = 42;
     public const int PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_COLD = 43;
     public const int PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_ELEC = 44;
     public const int PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_NATURE = 45;
     public const int PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_SPIRIT = 46;

     public const int PROPERTY_ATTRIBUTE_DAMAGE_SHIELD_POINTS = 47;
     public const int PROPERTY_ATTRIBUTE_DAMAGE_SHIELD_STRENGTH = 48;

    public const int PROPERTY_ATTRIBUTE_HEALING_BONUS = 51;
    public const int PROPERTY_ATTRIBUTE_FIRE_DAMAGE_BONUS = 55;
     public const int PROPERTY_ATTRIBUTE_SPIRIT_DAMAGE_BONUS = 56;
     public const int PROPERTY_ATTRIBUTE_COLD_DAMAGE_BONUS = 57;
     public const int PROPERTY_ATTRIBUTE_NATURE_DAMAGE_BONUS = 58;
     public const int PROPERTY_ATTRIBUTE_ELECTRICITY_DAMAGE_BONUS = 59;

     // Creature ranks
     public const int CREATURE_RANK_INVALID = 0;
     public const int CREATURE_RANK_CRITTER = 1;
     public const int CREATURE_RANK_NORMAL = 2;
     public const int CREATURE_RANK_LIEUTENANT = 3;
     public const int CREATURE_RANK_BOSS = 4;
     public const int CREATURE_RANK_ELITE_BOSS = 5;
     public const int CREATURE_RANK_PLAYER = 100;
     public const int CREATURE_RANK_WEAK_NORMAL = 11;
     public const int CREATURE_RANK_ONE_HIT_KILL = 12;

     // -----------------------------------------------------------------------------
     // Database autogenerated constant block from abi_base.xls, please see
     // http://staff.bioware.com/wiki/index.html?page=Ability+Script+Constants&wiki=da_data&action=view
     // for details. -- Georg
     // -----------------------------------------------------------------------------

     //-----------------------------------------------
     // Autogenerated Talent and Spell Constants
     //-----------------------------------------------

     public const int ABILITY_TALENT_ACID_SPRAY = 90034;
     public const int ABILITY_TALENT_AIM = 500;
     public const int ABILITY_TALENT_ARROW_OF_SLAYING = 802;
     public const int ABILITY_TALENT_ASSAULT = 38;
     public const int ABILITY_TALENT_AURA_OF_CORRUPTION = 90033;
     public const int ABILITY_TALENT_BELOW_THE_BELT = 3026;
     public const int ABILITY_TALENT_BERSERK = 700;
     public const int ABILITY_TALENT_BLOOD_FRENZY = 713;
     public const int ABILITY_TALENT_BRAVERY = 17;
     public const int ABILITY_TALENT_BROODMOTHER_CHARGE_LEFT = 90075;
     public const int ABILITY_TALENT_BROODMOTHER_CHARGE_RIGHT = 90076;
     public const int ABILITY_TALENT_BROODMOTHER_GAS = 90085;
     public const int ABILITY_TALENT_BROODMOTHER_LEFT_V1 = 90031;
     public const int ABILITY_TALENT_BROODMOTHER_LEFT_V2 = 90069;
     public const int ABILITY_TALENT_BROODMOTHER_RANGED_SPIT = 12010;
     public const int ABILITY_TALENT_BROODMOTHER_RESISTANCES = 90077;
     public const int ABILITY_TALENT_BROODMOTHER_RIGHT_V1 = 90032;
     public const int ABILITY_TALENT_BROODMOTHER_RIGHT_V2 = 90070;
     public const int ABILITY_TALENT_BROODMOTHER_SCREAM = 90084;
     public const int ABILITY_TALENT_BROODMOTHER_SWEEP = 90071;
     public const int ABILITY_TALENT_BROODMOTHER_VOMIT_ALL = 90072;
     public const int ABILITY_TALENT_BROODMOTHER_VOMIT_LEFT = 90073;
     public const int ABILITY_TALENT_BROODMOTHER_VOMIT_RIGHT = 90074;
     public const int ABILITY_TALENT_BROODMOTHER_GRAB_LEFT = 90098;
     public const int ABILITY_TALENT_BROODMOTHER_GRAB_RIGHT = 90099;
     public const int ABILITY_TALENT_CAPTIVATE = 1000;
     public const int ABILITY_TALENT_CLEANSE_AREA = 3017;
     public const int ABILITY_TALENT_CONSTRAINT = 3006;
     public const int ABILITY_TALENT_CRIPPLE = 3069;
     public const int ABILITY_TALENT_CRIPPLING_SHOT = 805;
     public const int ABILITY_TALENT_CRITICAL_SHOT = 804;
     public const int ABILITY_TALENT_CRITICAL_STRIKE = 3;
     public const int ABILITY_TALENT_CRY_OF_VALOR = 3045;
     public const int ABILITY_TALENT_DARKSPAWN_RESISTANCES = 90078;
     public const int ABILITY_TALENT_DEADLY_STRIKE = 708;
     public const int ABILITY_TALENT_DEATH_BLOW = 3021;
     public const int ABILITY_TALENT_DEATH_FURY = 4051;
     public const int ABILITY_TALENT_LACERATE = 3059;
     public const int ABILITY_TALENT_DEFENSIVE_FIRE = 33;
     public const int ABILITY_TALENT_DEMON_PROPERTIES = 90081;
     public const int ABILITY_TALENT_DEMORALIZE = 705;
     public const int ABILITY_TALENT_DESTROYER = 3032;
     public const int ABILITY_TALENT_DEVOUR = 3065;
     public const int ABILITY_TALENT_DIRTY_FIGHTING = 603;
     public const int ABILITY_TALENT_DISENGAGE = 3016;
     public const int ABILITY_TALENT_DISTRACTION = 701;
     public const int ABILITY_TALENT_EVASION = 3069;

     public const int ABILITY_TALENT_DODGE = 21;
     public const int ABILITY_TALENT_COMBAT_MOVEMENT = 21;

     public const int ABILITY_TALENT_EXPLOIT_WEAKNESS = 56;
     public const int ABILITY_TALENT_DUAL_WEAPON_CRIPPLE = 10;
     public const int ABILITY_TALENT_DUAL_WEAPON_DOUBLE_STRIKE = 11;
     public const int ABILITY_TALENT_DUAL_WEAPON_FLURRY = 3035;
     public const int ABILITY_TALENT_DUAL_WEAPON_MOMENTUM = 717;
     public const int ABILITY_TALENT_DUAL_WEAPON_RIPOSTE = 9;
     public const int ABILITY_TALENT_DUAL_WEAPON_SWEEP = 3044;
     public const int ABILITY_TALENT_DUAL_WEAPON_TBD = 8;
     public const int ABILITY_TALENT_DUELING = 709;
     public const int ABILITY_TALENT_FEAST_OF_THE_FALLEN = 3058;
     public const int ABILITY_TALENT_FEIGN_DEATH = 3023;
     public const int ABILITY_TALENT_FINAL_BLOW = 3009;
     public const int ABILITY_TALENT_FRIGHTENING = 3066;
     public const int ABILITY_TALENT_GENLOCK_PROPERTIES = 90079;
     public const int ABILITY_TALENT_GOLEM_KICK = 90058;
     public const int ABILITY_TALENT_GOLEM_QUAKE = 90056;
     public const int ABILITY_TALENT_GOLEM_RANGED_1 = 90057;
     public const int ABILITY_TALENT_GOLEM_SLAM = 90055;
     public const int ABILITY_TALENT_HIDDEN_ASSASSIN = 4014;
     public const int ABILITY_TALENT_HIDDEN_BERSERKER = 4016;
     public const int ABILITY_TALENT_HIDDEN_CHAMPION = 4013;
     public const int ABILITY_TALENT_HIDDEN_DOG = 4034;
     public const int ABILITY_TALENT_HIDDEN_DUELIST = 4030;
     public const int ABILITY_TALENT_HIDDEN_RANGER = 4029;
     public const int ABILITY_TALENT_HIDDEN_REAVER = 4019;
     public const int ABILITY_TALENT_HIDDEN_ROGUE = 4020;
     public const int ABILITY_TALENT_HIDDEN_SHALE = 4033;
     public const int ABILITY_TALENT_HIDDEN_TEMPLAR = 4021;
     public const int ABILITY_TALENT_HIDDEN_WARRIOR = 4022;
     public const int ABILITY_TALENT_HURLOCK_PROPERTIES = 90080;
     public const int ABILITY_TALENT_INDOMITABLE = 28;
     public const int ABILITY_TALENT_INVALID_TALENT = 0;
     public const int ABILITY_TALENT_KEEN_DEFENSE = 3049;
     public const int ABILITY_TALENT_LETHALITY = 777;
     public const int ABILITY_TALENT_MARK_OF_DEATH = 3060;
     public const int ABILITY_TALENT_MASTER_ARCHER = 35;
     public const int ABILITY_TALENT_MELEE_ARCHER = 34;
     public const int ABILITY_TALENT_MIGHTY_BLOW = 3028;
     public const int ABILITY_TALENT_MONSTER_ABOMINATION_RAGE = 90089;
     public const int ABILITY_TALENT_MONSTER_ABOMINATION_TRIPLESTRIKE_RAGE = 90090;
     public const int ABILITY_TALENT_MONSTER_ABOMINATION_TRIPLESTRIKE_HUNGER = 90116;
     public const int ABILITY_TALENT_MONSTER_ABOMINATION_TRIPLESTRIKE_SLOTH = 90117;
     public const int ABILITY_TALENT_MONSTER_ABOMINATION_TRIPLESTRIKE_DESIRE = 90118;
     public const int ABILITY_TALENT_MONSTER_ARCANEHORROR_AOE = 90091;
     public const int ABILITY_TALENT_MONSTER_AURA_HEALING = 90088;
     public const int ABILITY_TALENT_MONSTER_AURA_WEAKNESS = 90087;
     public const int ABILITY_TALENT_MONSTER_BEAR_HUG = 90018;
     public const int ABILITY_TALENT_MONSTER_BEAR_RAGE = 90020;
     public const int ABILITY_TALENT_MONSTER_BEAR_SLAM = 90019;
     public const int ABILITY_TALENT_MONSTER_BRONTO_CHARGE = 90024;
     public const int ABILITY_TALENT_MONSTER_BRONTO_STOMP = 90025;
     public const int ABILITY_TALENT_MONSTER_CANINE_HOWL = 90067;
     public const int ABILITY_TALENT_MONSTER_DOG_CHARGE = 90016;
     public const int ABILITY_TALENT_MONSTER_DOG_COMBAT_TRAINING = 90049;
     public const int ABILITY_TALENT_MONSTER_DOG_FORTITUDE = 90050;
     public const int ABILITY_TALENT_MONSTER_DOG_GROWL = 90017;
     public const int ABILITY_TALENT_MONSTER_DOG_NEMESIS = 90051;
     public const int ABILITY_TALENT_MONSTER_DOG_OVERWHELM = 90015;
     public const int ABILITY_TALENT_MONSTER_DOG_SHRED = 90047;
     public const int ABILITY_TALENT_MONSTER_FLANKING = 90023;
     public const int ABILITY_TALENT_MONSTER_GOLEM_HURL = 90060;
     public const int ABILITY_TALENT_MONSTER_GOLEM_QUAKE = 90056;
     public const int ABILITY_TALENT_MONSTER_GOLEM_RANGED_1 = 90057;
     public const int ABILITY_TALENT_MONSTER_GOLEM_SLAM = 90055;
     public const int ABILITY_TALENT_MONSTER_MABARI_HOWL = 90048;
     public const int ABILITY_TALENT_MONSTER_OGRE_ATTCK_BACK_LEFT = 90063;
     public const int ABILITY_TALENT_MONSTER_OGRE_ATTCK_BACK_RIGHT = 90062;
     public const int ABILITY_TALENT_MONSTER_OGRE_ATTCK_LEFT = 90065;
     public const int ABILITY_TALENT_MONSTER_OGRE_ATTCK_RIGHT = 90064;
     public const int ABILITY_TALENT_MONSTER_OGRE_GRAB = 90036;
     public const int ABILITY_TALENT_MONSTER_OGRE_RAM = 90066;
     public const int ABILITY_TALENT_MONSTER_OGRE_STOMP = 90038;
     public const int ABILITY_TALENT_MONSTER_OGRE_SWEEP = 90037;
     public const int ABILITY_TALENT_MONSTER_ORB_BLACK = 90044;
     public const int ABILITY_TALENT_MONSTER_PARALYZE = 90043;
     public const int ABILITY_TALENT_MONSTER_POISON_BITE = 90030;
     public const int ABILITY_TALENT_MONSTER_POISON_SPIT = 90068;
     public const int ABILITY_TALENT_MONSTER_RABID = 90083;
     public const int ABILITY_TALENT_MONSTER_REVENANT_DOUBLESTRIKE = 90086;
     public const int ABILITY_TALENT_MONSTER_REVENANT_PULL = 90092;
     public const int ABILITY_TALENT_MONSTER_SHRED = 90039;
     public const int ABILITY_TALENT_MONSTER_SHRIEK_LEAP = 90040;
     public const int ABILITY_TALENT_MONSTER_SHRIEK_SHRIEK = 90041;
     public const int ABILITY_TALENT_MONSTER_SHRIEK_FRENZY = 90128;
     public const int ABILITY_TALENT_MONSTER_SHRIEK_OVERWHLEM = 90129;
     public const int ABILITY_TALENT_MONSTER_SPIDER_WEB = 90029;
     public const int ABILITY_TALENT_MONSTER_STALKER_SCARE = 90026;
     public const int ABILITY_TALENT_MONSTER_STALKER_SLOW = 90028;
     public const int ABILITY_TALENT_MONSTER_STALKER_SPIT = 90027;
     public const int ABILITY_TALENT_MONSTER_UNDEAD_DRAIN = 90046;
     public const int ABILITY_TALENT_MONSTER_UNDEAD_SPIRIT = 90042;
     public const int ABILITY_TALENT_MONSTER_WISP_ATTACK = 11131;
     public const int ABILITY_TALENT_MOTIVATE = 42;
     public const int ABILITY_TALENT_NATURE_I_COURAGE_OF_THE_PACK = 1004;
     public const int ABILITY_TALENT_NATURE_II_HARDINESS_OF_THE_BEAR = 91;
     public const int ABILITY_TALENT_MASTER_RANGER = 92;
     public const int ABILITY_TALENT_ONE_MIND = 29;
     public const int ABILITY_TALENT_ONE_MIND_II = 30;
     public const int ABILITY_TALENT_ONE_MIND_III = 31;
     public const int ABILITY_TALENT_ONE_MIND_IV = 32;
     public const int ABILITY_TALENT_OVERPOWER = 3073;
     public const int ABILITY_TALENT_OVERRUN = 3024;
     public const int ABILITY_TALENT_PAIN = 3067;
     public const int ABILITY_TALENT_PERFECT_STRIKING = 20;
     public const int ABILITY_TALENT_PINNING_SHOT = 803;
     public const int ABILITY_TALENT_PINPOINT_STRIKE = 3051;
     public const int ABILITY_TALENT_POMMEL_STRIKE = 3024;
     public const int ABILITY_TALENT_POWER_SLAM = 90045;
     public const int ABILITY_TALENT_POWERFUL = 14;
     public const int ABILITY_TALENT_POWERFUL_SWINGS = 718;
     public const int ABILITY_TALENT_PRECISE_STRIKING = 19;
     public const int ABILITY_TALENT_PUNISHER = 7;
     public const int ABILITY_TALENT_RALLY = 3038;
     public const int ABILITY_TALENT_RAPIDSHOT = 3071;
     public const int ABILITY_TALENT_RESILIENCE = 48;
     public const int ABILITY_TALENT_RESIST_DECEPTION = 52;
     public const int ABILITY_TALENT_RIGHTEOUS_STRIKE = 23;
     public const int ABILITY_TALENT_SCATTERSHOT = 800;
     public const int ABILITY_TALENT_SHATTERING_BLOWS = 3001;
     public const int ABILITY_TALENT_SHATTERING_SHOT = 3072;
     public const int ABILITY_TALENT_SHIELD_BALANCE = 37;
     public const int ABILITY_TALENT_SHIELD_BASH = 617;
     public const int ABILITY_TALENT_SHIELD_BLOCK = 3074;
     public const int ABILITY_TALENT_SHIELD_COVER = 13;
     public const int ABILITY_TALENT_SHIELD_DEFENSE = 704;
     public const int ABILITY_TALENT_SHIELD_EXPERTISE = 36;
     public const int ABILITY_TALENT_SHIELD_MASTERY = 2;
     public const int ABILITY_TALENT_SHIELD_PUMMEL = 1;
     public const int ABILITY_TALENT_SHIELD_TACTICS = 3030;
     public const int ABILITY_TALENT_SHIELD_WALL = 12;
     public const int ABILITY_TALENT_HOLY_SMITE = 25;
     public const int ABILITY_TALENT_SPIDER_PROPERTIES = 90082;
     public const int ABILITY_TALENT_STRONG = 27;
     public const int ABILITY_TALENT_STUNNING_BLOWS = 3000;
     public const int ABILITY_TALENT_SUMMON_SPIDER = 93;
     public const int ABILITY_TALENT_SUNDER_ARMOR = 4;
     public const int ABILITY_TALENT_SUNDER_WEAPON = 3025;
     public const int ABILITY_TALENT_SUPERIORITY = 3039;
     public const int ABILITY_TALENT_SUPPRESSING_FIRE = 801;
     public const int ABILITY_TALENT_TAUNT = 3041;
     public const int ABILITY_TALENT_THREATEN = 808;
     public const int ABILITY_TALENT_TRAINING_DEMONIC_COMBAT = 3078;
     public const int ABILITY_TALENT_TRAINING_HUMANOID_COMBAT = 3077;
     public const int ABILITY_TALENT_TRAINING_SIMPLE_COMBAT = 3076;
     public const int ABILITY_TALENT_UPSET_BALANCE = 3050;
     public const int ABILITY_TALENT_DUAL_WEAPON_EXPERT = 5;
     public const int ABILITY_TALENT_DUAL_WEAPON_MASTER = 3036;
     public const int ABILITY_TALENT_DUAL_WEAPON_TRAINING = 6;
     public const int ABILITY_TALENT_WAR_CRY = 3037;
     public const int ABILITY_TALENT_WEAPON_SWEEP = 3031;
     public const int ABILITY_TALENT_DUAL_WEAPON_WHIRLWIND = 3043;
     public const int ABILITY_SPELL_ABOMINATION_FLAME = 10004;
     public const int ABILITY_SPELL_ANIMATE_DEAD = 10508;
     public const int ABILITY_SPELL_ANTIMAGIC_BURST = 11003;
     public const int ABILITY_SPELL_ANTIMAGIC_WARD = 10900;
     public const int ABILITY_SPELL_ARCANE_BOLT = 200254;
     public const int ABILITY_SPELL_ARCANE_HORROR_ATTACK = 14005;
     public const int ABILITY_SPELL_ARCANE_MIGHT = 10208;
     public const int ABILITY_SPELL_ARCANE_SHIELD = 200255;
     public const int ABILITY_SPELL_AURA_OF_MIGHT = 17020;
     public const int ABILITY_SPELL_BEAR = 17010;
     public const int ABILITY_SPELL_BLIZZARD = 13000;
     public const int ABILITY_SPELL_BLOOD_CONTROL = 10703;
     public const int ABILITY_SPELL_BLOOD_MAGIC = 10700;
     public const int ABILITY_SPELL_BLOOD_SACRIFICE = 10701;
     public const int ABILITY_SPELL_BLOOD_WOUND = 10702;
     public const int ABILITY_SPELL_BRANKA_AOE = 90004;
     public const int ABILITY_SPELL_BRANKA_MIND_CONTROL = 90005;
     public const int ABILITY_SPELL_BRANKA_ROCKSTRIKE = 90003;
     public const int ABILITY_SPELL_CHAOS_DRAKE_BREATH = 14003;
     public const int ABILITY_SPELL_CHAOS_MAGIC = 17028;
     public const int ABILITY_SPELL_CLEANSING_AURA = 10507;
     public const int ABILITY_SPELL_COMBAT_MAGIC = 17023;
     public const int ABILITY_SPELL_CONE_OF_COLD = 13002;
     public const int ABILITY_SPELL_CRUSHING_PRISON = 11123;
     public const int ABILITY_SPELL_CURE = 10200;
     public const int ABILITY_SPELL_DAZE = 11115;
     public const int ABILITY_SPELL_DEAD_MAN_WALKING = 12005;
     public const int ABILITY_SPELL_DEATH_CLOUD = 15003;
     public const int ABILITY_SPELL_DEATH_MAGIC = 17002;
     public const int ABILITY_SPELL_DEMON_AURA = 90053;
     public const int ABILITY_SPELL_DEMON_LIFT = 90054;
     public const int ABILITY_SPELL_DEMON_SCATTER = 90052;
     public const int ABILITY_SPELL_DIVINE_RESTORATION = 10201;
     public const int ABILITY_SPELL_DO_NOT_USE = 11126;
     public const int ABILITY_SPELL_DRAGON_BREATH_EVIL = 15000;
     public const int ABILITY_SPELL_DRAGON_BREATH_FIRE = 10005;
     public const int ABILITY_SPELL_DRAIN_LIFE = 15002;
     public const int ABILITY_SPELL_EARTHQUAKE = 11116;
     public const int ABILITY_SPELL_EARTHRAGE = 12000;
     public const int ABILITY_SPELL_EARTHRAGE_II = 12001;
     public const int ABILITY_SPELL_EARTHRAGE_III = 12002;
     public const int ABILITY_SPELL_EARTHRAGE_IV = 12003;
     public const int ABILITY_SPELL_ELEMENTAL_AURA = 10601;
     public const int ABILITY_SPELL_ELEMENTAL_CONDUIT = 17000;
     public const int ABILITY_SPELL_FADE_SHROUD = 17021;
     public const int ABILITY_SPELL_FIRE_ELEMENTAL_LASH = 10006;
     public const int ABILITY_SPELL_FIREBALL = 10003;
     public const int ABILITY_SPELL_FIREFIELD = 10007;
     public const int ABILITY_SPELL_FLAME_BLAST = 10001;
     public const int ABILITY_SPELL_FLAMING_WEAPONS = 10204;
     public const int ABILITY_SPELL_FLYING_SWARM = 17007;
     public const int ABILITY_SPELL_FROSTWALL = 11007;
     public const int ABILITY_SPELL_GAS_EXPLOSION = 10008;
     public const int ABILITY_SPELL_GLYPH_OF_NEUTRALIZATION = 17028;
     public const int ABILITY_SPELL_GLYPH_OF_PARALYSIS = 11001;
     public const int ABILITY_SPELL_GLYPH_OF_REPULSION = 17000;
     public const int ABILITY_SPELL_GLYPH_OF_WARDING = 10601;
     public const int ABILITY_SPELL_GOLEM_HURL = 90060;
     public const int ABILITY_SPELL_GREASE = 11113;
     public const int ABILITY_SPELL_GUARDIAN_GLYPH = 11001;
     public const int ABILITY_SPELL_HEAL = 10104;
     public const int ABILITY_SPELL_HEROIC_DEFENSE = 10203;
     public const int ABILITY_SPELL_HEROIC_OFFENSE = 10206;
     public const int ABILITY_SPELL_HEROS_ARMOR = 10202;
     public const int ABILITY_SPELL_HIDDEN_ARCANE_WARRIOR = 4012;
     public const int ABILITY_SPELL_HIDDEN_BARD = 4015;
     public const int ABILITY_SPELL_HIDDEN_BLOODMAGE = 4017;
     public const int ABILITY_SPELL_HIDDEN_SHAPESHIFTER = 4018;
     public const int ABILITY_SPELL_HIDDEN_SPIRIT_HEALER = 4025;
     public const int ABILITY_SPELL_HIDDEN_WIZARD = 4023;
     public const int ABILITY_SPELL_HORROR = 11108;
     public const int ABILITY_SPELL_IMMOBILIZE = 11100;
     public const int ABILITY_SPELL_INFERNO = 10002;
     public const int ABILITY_SPELL_INVALID_SPELL = 10000;
     public const int ABILITY_SPELL_LIFEWARD = 10506;
     public const int ABILITY_SPELL_LIGHTNING = 14001;
     public const int ABILITY_SPELL_LIGHTNING_WEAPONS = 10205;
     public const int ABILITY_SPELL_MAGE_BASE_ATTACK = 11130;
     public const int ABILITY_SPELL_MANA_CLASH = 11000;
     public const int ABILITY_SPELL_MANA_CLEANSE = 11004;
     public const int ABILITY_SPELL_MASS_CORPSE_DETONATION = 12011;
     public const int ABILITY_SPELL_MASS_PARALYSIS = 11110;
     public const int ABILITY_SPELL_MASS_SLOW = 11112;
     public const int ABILITY_SPELL_MIND_BLAST = 12006;
     public const int ABILITY_SPELL_MIND_FOCUS = 10209;
     public const int ABILITY_SPELL_MIND_ROT = 11109;
     public const int ABILITY_SPELL_MONSTER_OGRE_HURL = 90061;
     public const int ABILITY_SPELL_MONSTERBUFFER3 = 90006;
     public const int ABILITY_SPELL_MONSTERBUFFER4 = 90007;
     public const int ABILITY_SPELL_MONSTERBUFFER5 = 90008;
     public const int ABILITY_SPELL_NIGHTMARE = 11125;
     public const int ABILITY_SPELL_PARALYSIS_EXPLOSION = 90173;
     public const int ABILITY_SPELL_PARALYZE = 11107;
     public const int ABILITY_SPELL_PETRIFY = 11124;
     public const int ABILITY_SPELL_PURIFY = 10207;
     public const int ABILITY_SPELL_REANIMATE = 10500;
     public const int ABILITY_SPELL_REGENERATION = 10210;
     public const int ARCHDEMON_VORTEX = 90000;
     public const int ARCHDEMON_SMITE = 90001;
     public const int ARCHDEMON_DETONATE_DARKSPAWN = 90002;
     public const int ARCHDEMON_CORRUPTION_BLAST = 90172;
     public const int ABILITY_SPELL_REVIVAL = 10504;
     public const int ABILITY_SPELL_ROOT = 11114;
     public const int ABILITY_SPELL_SHAPESHIFTER = 17009;
     public const int ABILITY_SPELL_SHARED_FATE = 11101;
     public const int ABILITY_SPELL_SHIELD_PARTY = 10401;
     public const int ABILITY_SPELL_SHIMMERING_SHIELD = 17022;
     public const int ABILITY_SPELL_SHOCK = 14000;
     public const int ABILITY_SPELL_SLEEP = 11121;
     public const int ABILITY_SPELL_SLOW = 11111;
     public const int ABILITY_SPELL_SPELL_BOOST = 17015;
     public const int ABILITY_SPELL_SPELL_BOOST_II = 17016;
     public const int ABILITY_SPELL_SPELL_BOOST_III = 17017;
     public const int ABILITY_SPELL_SPELL_BOOST_IV = 17018;
     public const int ABILITY_SPELL_SPELL_MIGHT = 17001;
     public const int ABILITY_SPELL_SPELL_SHIELD = 10400;
     public const int ABILITY_SPELL_SPELL_WISP = 11006;
     public const int ABILITY_SPELL_SPELLBLOOM = 11005;
     public const int ABILITY_SPELL_SPIDER_SHAPE = 17008;
     public const int ABILITY_SPELL_STEAM_CLOUD = 10009;
     public const int ABILITY_SPELL_STINGING_SWARM = 12008;
     public const int ABILITY_SPELL_STONEFIST = 12004;
     public const int ABILITY_SPELL_STORM_OF_THE_CENTURY = 14004;
     public const int ABILITY_SPELL_SUPPORT_THE_WEAK = 10509;
     public const int ABILITY_SPELL_TBD_WAS_DANCE_OF_MADNESS = 11122;
     public const int ABILITY_SPELL_TEMPEST = 14002;
     public const int ABILITY_SPELL_TREMOR = 11117;
     public const int ABILITY_SPELL_TREMOR_II = 11118;
     public const int ABILITY_SPELL_TREMOR_III = 11119;
     public const int ABILITY_SPELL_TREMOR_IV = 11120;
     public const int ABILITY_SPELL_WALKING_BOMB = 12005;
     public const int ABILITY_SPELL_WALL_OF_FORCE = 17019;
     public const int ABILITY_SPELL_WALL_OF_STONE = 11002;
     public const int ABILITY_SPELL_WEAKNESS = 11106;
     public const int MONSTER_TALENT_WILD_SYLVAN_ROOTS = 12007;
     public const int ABILITY_SPELL_WINTERS_GRASP = 13001;
     public const int ABILITY_SPELL_WYNNES_SEAL_PORTAL = 10704;
     public const int ABILITY_TALENT_BACKSTAB = 3002;
     public const int ABILITY_SKILL_DWARVEN_RESISTANCE = 3080;
     public const int ABILITY_SKILL_HERBALISM_1 = 100061;
     public const int ABILITY_SKILL_HERBALISM_2 = 100062;
     public const int ABILITY_SKILL_HERBALISM_3 = 100063;
     public const int ABILITY_SKILL_HERBALISM_4 = 100064;
     public const int ABILITY_SKILL_INVALID_SKILL = 100000;
     public const int ABILITY_SKILL_LOCKPICKING_1 = 100001;
     public const int ABILITY_SKILL_LOCKPICKING_2 = 100002;
     public const int ABILITY_SKILL_LOCKPICKING_3 = 100003;
     public const int ABILITY_SKILL_LOCKPICKING_4 = 100004;
     public const int ABILITY_SKILL_PERSUADE_1 = 100011;
     public const int ABILITY_SKILL_PERSUADE_2 = 100012;
     public const int ABILITY_SKILL_PERSUADE_3 = 100013;
     public const int ABILITY_SKILL_PERSUADE_4 = 100014;
     public const int ABILITY_SKILL_PET_ATTACK = 100090;
     public const int ABILITY_SKILL_PET_DEFEND = 100092;
     public const int ABILITY_SKILL_PET_FOLLOW = 100091;
     public const int ABILITY_SKILL_PET_SPECIAL_ABILITY = 100093;
     public const int ABILITY_SKILL_POISON_1 = 100071;
     public const int ABILITY_SKILL_POISON_2 = 100072;
     public const int ABILITY_SKILL_POISON_3 = 100073;
     public const int ABILITY_SKILL_POISON_4 = 100074;

     public const int ABILITY_SKILL_COMBAT_TRAINING_1 = 100100;
     public const int ABILITY_SKILL_COMBAT_TRAINING_2 = 100101;
     public const int ABILITY_SKILL_COMBAT_TRAINING_3 = 100102;
     public const int ABILITY_SKILL_COMBAT_TRAINING_4 = 100104;

     public const int ABILITY_SKILL_SENSE_DARKSPAWN = 4050;
     public const int ABILITY_SKILL_SKILL_PLOT_SHAPESHIFT_BURNING = 100083;
     public const int ABILITY_SKILL_SKILL_PLOT_SHAPESHIFT_GOLEM = 100080;
     public const int ABILITY_SKILL_SKILL_PLOT_SHAPESHIFT_MOUSE = 100081;
     public const int ABILITY_SKILL_SKILL_PLOT_SHAPESHIFT_SPIRIT = 100084;
     public const int ABILITY_SKILL_STEALING_1 = 100021;
     public const int ABILITY_SKILL_STEALING_2 = 100022;
     public const int ABILITY_SKILL_STEALING_3 = 100023;
     public const int ABILITY_SKILL_STEALING_4 = 100024;
     public const int ABILITY_SKILL_STEALTH_1 = 100075;
     public const int ABILITY_SKILL_STEALTH_2 = 100076;
     public const int ABILITY_SKILL_STEALTH_3 = 100077;
     public const int ABILITY_SKILL_STEALTH_4 = 100078;
     public const int ABILITY_SKILL_STEALTHY_ATTACK = 12009;
     public const int ABILITY_SKILL_SURVIVAL_1 = 100051;
     public const int ABILITY_SKILL_SURVIVAL_2 = 100052;
     public const int ABILITY_SKILL_SURVIVAL_3 = 100053;
     public const int ABILITY_SKILL_SURVIVAL_4 = 100054;
     public const int ABILITY_SKILL_TRAPS_1 = 100041;
     public const int ABILITY_SKILL_TRAPS_2 = 100042;
     public const int ABILITY_SKILL_TRAPS_3 = 100043;
     public const int ABILITY_SKILL_TRAPS_4 = 100044;

     public const int ABILITY_SKILL_COMBAT_TACTICS_1 = 100110;
     public const int ABILITY_SKILL_COMBAT_TACTICS_2 = 100111;
     public const int ABILITY_SKILL_COMBAT_TACTICS_3 = 100112;
     public const int ABILITY_SKILL_COMBAT_TACTICS_4 = 100113;

     public const int ITEM_ABILITY_HEALING_SALVE = 200001;
     public const int ITEM_ABILITY_HEALING_SALVE_1 = 200010;
     public const int ITEM_ABILITY_HEALING_SALVE_2 = 200011;
     public const int ITEM_ABILITY_HEALING_SALVE_3 = 200012;
     public const int ITEM_ABILITY_HEALING_SALVE_4 = 200013;

     public const int ITEM_ABILITY_UNIQUE_POWER_UNLIMITED_USE = 200203;

     public const int ITEM_ABILITY_KOLGRIMS_HORN = 200262;

     public const int ABILITY_TRAIT_CLUMSY = 150003;
     public const int ABILITY_TRAIT_COLD_IMMUNITY = 150011;
     public const int ABILITY_TRAIT_COLD_VULNERABILITY = 150012;
     public const int ABILITY_TRAIT_CRITICAL_HIT_IMMUNITY = 150007;
     public const int ABILITY_TRAIT_DEMONIC_CASTER = 150008;
     public const int ABILITY_TRAIT_EXPLOSIVE = 150009;
     public const int ABILITY_TRAIT_FIRE_IMMUNITY = 150010;
     public const int ABILITY_TRAIT_FIRE_VULNERABILITY = 150013;
     public const int ABILITY_TRAIT_NATURE_IMMUNITY = 150014;
     public const int ABILITY_TRAIT_SPIRIT_IMMUNITY = 150015;
     public const int ABILITY_TRAIT_LIGHTNING_IMMUNITY = 150016;
     public const int ABILITY_TRAIT_STURDY = 90300;

     public const int ABILITY_TRAIT_FRAGILE = 150001;
     public const int ABILITY_TRAIT_GHOST = 150004;
     public const int ABILITY_TRAIT_HIGH_MORALE = 150005;
     public const int ABILITY_TRAIT_LOW_MORALE = 150006;
     public const int ABILITY_TRAIT_SENILE = 150002;
     public const int ABILITY_TRAIT_WEAKLY = 150000;

     public const int ABILITY_TALENT_MONSTER_REVENANT_MASS_PULL = 90100;
     public const int ABILITY_MONSTER_ARCANEHORROR_SWARM = 90115;
     public const int MONSTER_TALENT_WILD_SYLVAN_STOMP = 90119;
     public const int MONSTER_TALENT_WILD_SYLVAN_RAGE = 90120;
     public const int MONSTER_TALENT_WILD_SYLVAN_ATTK_BL = 90111;
     public const int MONSTER_TALENT_WILD_SYLVAN_ATTK_BR = 90122;
     public const int MONSTER_TALENT_WILD_SYLVAN_ATTK_FL = 90123;
     public const int MONSTER_TALENT_WILD_SYLVAN_ATTK_FR = 90124;
     public const int MONSTER_TALENT_MONSTER_CORRUPTION_BURST = 90127;
     public const int MONSTER_ASHWRAITH_WHIRLWIND = 90134;
     public const int MONSTER_ASHWRAITH_LEAP = 90135;
     public const int MONSTER_ASHWRAITH_SLAM = 90136;
     public const int MONSTER_SUCCUBUS_DANCE = 90137;
     public const int MONSTER_SUCCUBUS_SCREAM = 90138;
     public const int MONSTER_DRAGON_BREATH = 90094;
     public const int MONSTER_DRAGON_TAIL_SLAP = 90140;
     public const int MONSTER_DRAGON_WING_BUFFET = 90141;
     public const int MONSTER_DRAGON_SHRED = 90142;
     public const int MONSTER_DRAGON_RAKE = 90143;
     public const int MONSTER_DRAGON_ROAR = 90144;
     public const int MONSTER_DRAGONLING_BREATH = 90145;
     public const int MONSTER_PRIDE_DEMON_FIRE_BLAST = 90146;
     public const int MONSTER_PRIDE_DEMON_FROST_BLAST = 90147;
     public const int MOSNTER_PRIDE_DEMON_MANA_WAVE = 90148;
     public const int MONSTER_PRIDE_DEMON_FIRE_BOLT = 90149;
     public const int MONSTER_PRIDE_DEMON_FROST_BOLT = 90150;
     public const int ABILITY_TALENT_MONSTER_AURA_FIRE = 90151;
     public const int MONSTER_RAGE_DEMON_FIRE_BOLT = 90152;
     public const int MONSTER_RAGE_DEMON_LAVA_BURST = 90153;
     public const int MONSTER_RAGE_DEMON_SLAM = 90154;
     public const int MONSTER_HIGH_DRAGON_BREATH = 10005;
     public const int MONSTER_LARGE_ATTCK_BACK_LEFT = 90063;
     public const int MONSTER_LARGE_ATTCK_BACK_LEFT2 = 90157;
     public const int MONSTER_LARGE_ATTCK_BACK_LEFT3 = 90158;
     public const int MONSTER_LARGE_ATTCK_BACK_RIGHT = 90062;
     public const int MONSTER_LARGE_ATTCK_BACK_RIGHT2 = 90155;
     public const int MONSTER_LARGE_ATTCK_BACK_RIGHT3 = 90156;
     public const int MONSTER_LARGE_ATTCK_RIGHT = 90064;
     public const int MONSTER_LARGE_ATTCK_RIGHT2 = 90159;
     public const int MONSTER_LARGE_ATTCK_RIGHT3 = 90160;
     public const int MONSTER_LARGE_ATTCK_LEFT = 90065;
     public const int MONSTER_LARGE_ATTCK_LEFT2 = 90161;
     public const int MONSTER_LARGE_ATTCK_LEFT3 = 90162;
     public const int MONSTER_HIGH_DRAGON_WING_BUFFET = 90163;
     public const int MONSTER_HIGH_DRAGON_ROAR = 90164;
     public const int MONSTER_HIGH_DRAGON_TAIL_FLAP = 90165;
     public const int MONSTER_HIGH_DRAGON_SWEEP = 90166;
     public const int MONSTER_HIGH_DRAGON_STOMP = 90167;
     public const int MONSTER_HIGH_DRAGON_FIRE_SPIT = 90168;
     public const int MONSTER_HIGH_DRAGON_GRAB_LEFT = 90169;
     public const int MONSTER_HIGH_DRAGON_GRAB_RIGHT = 90170;
     public const int MONSTER_BEAR_OVERWHELM = 90130;
     public const int MONSTER_STALKER_OVERWHLEM = 90131;
     public const int MONSTER_DRAGON_OVERWHELM = 90132;
     public const int MONSTER_SPIDER_OVERWHELM = 90133;
     public const int MONSTER_ARCANE_HORROR_BUFF = 90171;
     public const int ABILITY_TALENT_STEALTH = 100075;
     public const int ABILITY_TALENT_COMBAT_STEALTH = 100077;

     public const int ABILITY_SPELL_WYNNE_SPECIAL = 10510;

     //public const int ABILITY_TALENT_TRAIT_HUMANOID = 4002;

     //Class Constants

     public const int CLASS_WIZARD = 2;
     public const int CLASS_ROGUE = 3;
     public const int CLASS_WARRIOR = 1;
     public const int CLASS_ARCANE_WARRIOR = 10;
     public const int CLASS_SHAPESHIFTER = 4;
     public const int CLASS_SPIRITHEALER = 5;
     public const int CLASS_CHAMPION = 6;
     public const int CLASS_TEMPLAR = 7;
     public const int CLASS_REAVER = 9;
     public const int CLASS_DOG = 17;
     public const int CLASS_ASSASSIN = 11;
     public const int CLASS_BLOOD_MAGE = 12;
     public const int CLASS_BARD = 13;
     public const int CLASS_RANGER = 14;
     public const int CLASS_DUELIST = 15;
     public const int CLASS_SHALE = 16;
     public const int CLASS_BERSERKER = 8;
     public const int CLASS_MONSTER_ANIMAL = 18;

     //End of autogenerated block

     //Auto generated @8/23/2007 7:59:43 PM

     // -----------------------------------------------------------------------------
     // Atmosphere constants - atmosphere.xls
     // -----------------------------------------------------------------------------

     // less than this value is invalid
     public const float ATM_ATMOSPHERE_INVALID_VALUE = -9999.0f;

     public const int ATM_CLOUDS_INDEX_OFFSET = 14;

     public const int ATM_PRESET_DEFAULT = 0;
     public const int ATM_PRESET_BATTLE = 1;
     public const int ATM_PRESET_NIGHT = 2;
     public const int ATM_PRESET_LIGHTNING = 3;
     public const int ATM_PRESET_EVENING = 4;
     public const int ATM_PRESET_DAY = 5;

     public const int ATM_PRESET_CLOUD_DEFAULT = 0;
     public const int ATM_PRESET_CLOUD_BATTLE = 1;
     public const int ATM_PRESET_CLOUD_NIGHT = 2;
     public const int ATM_PRESET_CLOUD_LIGHTING = 3;

     public const int ATM_PRESET_FOG_DEFAULT = 0;
     public const int ATM_PRESET_FOG_BATTLE = 1;
     public const int ATM_PRESET_FOG_NIGHT = 2;
     public const int ATM_PRESET_FOG_LIGHTING = 3;

     public const int ATM_PARAM_SUN_COLOR_RED = 0;
     public const int ATM_PARAM_SUN_COLOR_GREEN = 1;
     public const int ATM_PARAM_SUN_COLOR_BLUE = 2;
     public const int ATM_PARAM_SUN_INTENSITY = 3;
     public const int ATM_PARAM_TURBIDITY = 4;
     public const int ATM_PARAM_EARTH_REFLECTANCE = 5;
     public const int ATM_PARAM_MIE_MULTIPLIER = 6;
     public const int ATM_PARAM_RAYLEIGH_MULTIPLIER = 7;
     public const int ATM_PARAM_EARTHIN_SCATTER_POWER = 8;
     public const int ATM_PARAM_DISTANCE_MULTIPLIER = 9;
     public const int ATM_PARAM_HG = 10;

     public const int ATM_PARAM_CLOUD_COLOR_RED = 11;
     public const int ATM_PARAM_CLOUD_COLOR_GREEN = 12;
     public const int ATM_PARAM_CLOUD_COLOR_BLUE = 13;
     public const int ATM_PARAM_CLOUD_DENSITY = 14;
     public const int ATM_PARAM_CLOUD_SHARPNESS = 15;
     public const int ATM_PARAM_CLOUD_DEPTH = 16;
     public const int ATM_PARAM_CLOUD_RANGE_MULTIPLIER1 = 17;
     public const int ATM_PARAM_CLOUD_RANGE_MULTIPLIER2 = 18;

     public const int ATM_PARAM_SUN_COLOR_RGB = 20;
     public const int ATM_PARAM_CLOUD_COLOR_RGB = 21;
     public const int ATM_PARAM_ATMOSPHERE_ALPHA = 22;
     public const int ATM_PARAM_FOG_COLOR = 23;
     public const int ATM_PARAM_FOG_INTENSITY = 24;
     public const int ATM_PARAM_FOG_CAP = 25;
     public const int ATM_PARAM_FOG_ZENITH = 26;
     public const int ATM_PARAM_MOON_SCALE = 27;
     public const int ATM_PARAM_MOON_ALPHA = 28;

     // unnecessary if GetM2DAColumns is revised
     public const string ATM_COLUMN_SUN_COLOR_RED = "fSunColor0";
     public const string ATM_COLUMN_SUN_COLOR_GREEN = "fSunColor1";
     public const string ATM_COLUMN_SUN_COLOR_BLUE = "fSunColor2";
     public const string ATM_COLUMN_SUN_INTENSITY = "fSunIntensity";
     public const string ATM_COLUMN_TURBIDITY = "fTurbidity";
     public const string ATM_COLUMN_EARTH_REFLECTANCE = "fEarthReflectance";
     public const string ATM_COLUMN_MIE_MULTIPLIER = "fMieMultiplier";
     public const string ATM_COLUMN_RAYLEIGH_MULTIPLIER = "fRayLeighMultiplier";
     public const string ATM_COLUMN_EARTHIN_SCATTER_POWER = "fEarthInScatterPower";
     public const string ATM_COLUMN_DISTANCE_MULTIPLIER = "fDistanceMultiplier";
     public const string ATM_COLUMN_HG = "fHg";
     public const string ATM_COLUMN_ATMOSPHERE_ALPHA = "fAtmAlpha";
     public const string ATM_COLUMN_MOON_SCALE = "fMoonScale";
     public const string ATM_COLUMN_MOON_ALPHA = "fMoonAlpha";

     public const string ATM_COLUMN_CLOUD_COLOR_RED = "fCloudColor0";
     public const string ATM_COLUMN_CLOUD_COLOR_GREEN = "fCloudColor1";
     public const string ATM_COLUMN_CLOUD_COLOR_BLUE = "fCloudColor2";
     public const string ATM_COLUMN_CLOUD_DENSITY = "fCloudDensity";
     public const string ATM_COLUMN_CLOUD_SHARPNESS = "fCloudSharpness";
     public const string ATM_COLUMN_CLOUD_DEPTH = "fCloudDepth";
     public const string ATM_COLUMN_CLOUD_RANGE_MULTIPLIER1 = "fCloudRangeMultiplier1";
     public const string ATM_COLUMN_CLOUD_RANGE_MULTIPLIER2 = "fCloudRangeMultiplier2";

     public const string ATM_COLUMN_FOG_CAP = "fFogCap";
     public const string ATM_COLUMN_FOG_COLOR_RED = "fFogColor0";
     public const string ATM_COLUMN_FOG_COLOR_GREEN = "fFogColor1";
     public const string ATM_COLUMN_FOG_COLOR_BLUE = "fFogColor2";
     public const string ATM_COLUMN_FOG_INTENSITY = "fFogIntensity";
     public const string ATM_COLUMN_FOG_VERTICAL_ZENITH = "fFogZenith";

     // Creature Types
     // ----------------------------------------------------------------------------*

     public const int CREATURE_TYPE_INVALID = 0;
     public const int CREATURE_TYPE_OTHER = 1;
     public const int CREATURE_TYPE_HUMANOID = 2;
     public const int CREATURE_TYPE_DARKSPAWN = 3;
     public const int CREATURE_TYPE_ANIMAL = 4;
     public const int CREATURE_TYPE_BEAST = 5;
     public const int CREATURE_TYPE_DEMON = 6;
     public const int CREATURE_TYPE_DRAGON = 7;
     public const int CREATURE_TYPE_AMBIENT = 8;
     public const int CREATURE_TYPE_GOLEM = 9;
     public const int CREATURE_TYPE_UNDEAD = 10;

     // ----------------------------------------------------------------------------*
     // World map terrain types
     // ----------------------------------------------------------------------------*

     public const int WMT_MOUNTAINS = 1;
     public const int WMT_FOREST = 2;
     public const int WMT_UNDERGROUND = 3;
     public const int WMT_PLAINS = 4;

     // ----------------------------------------------------------------------------*
     // World Maps
     // ----------------------------------------------------------------------------*

     public const int WM_WOW = 1;
     public const int WM_DENERIM = 2;
     public const int WM_UNDERGROUND = 3;
     public const int WM_CLIMAX = 4;
     public const int WM_FADE = 5;

     // ----------------------------------------------------------------------------*
     // Item Stats
     // ----------------------------------------------------------------------------*

     public const int ITEM_STAT_INVALID = 0;
     public const int ITEM_STAT_DEFENSE = 1;
     public const int ITEM_STAT_DAMAGE = 2;
     public const int ITEM_STAT_SHIELD_RATING = 3;
     public const int ITEM_STAT_ATTACK = 4;
     public const int ITEM_STAT_CRIT_CHANCE_MODIFIER = 5;
     public const int ITEM_STAT_ARMOR_PENETRATION = 6;
     public const int ITEM_STAT_MISSILE_DEFLECTION = 8;
     public const int ITEM_STAT_OPTIMUM_RANGE = 9;
     public const int ITEM_STAT_ATTRIBUTE_MOD = 12;

     // ----------------------------------------------------------------------------*
     // Plot Action Sets and Actions
     // plotactions.xls
     // ----------------------------------------------------------------------------*

     public const int PLOT_ACTIONSTATE_INVALID = 0;
     public const int PLOT_ACTIONSTATE_ENABLED = 1;
     public const int PLOT_ACTIONSTATE_DISABLED = 2;
     public const int PLOT_ACTIONSTATE_ACTIVE = 3;

     public const int PLOT_ACTIONSET_INVALID = 0;
     public const int PLOT_ACTIONSET_ARMY = 1;
     public const int PLOT_ACTIONSET_SHAPESHIFT = 2;

     public const int PLOT_ACTION_ARMY_INVALID = 0;
     public const int PLOT_ACTION_ARMY_REDCLIFFE = 1;
     public const int PLOT_ACTION_ARMY_LEGION = 2;
     public const int PLOT_ACTION_ARMY_DWARVES = 3;
     public const int PLOT_ACTION_ARMY_GOLEMS = 4;
     public const int PLOT_ACTION_ARMY_WEREWOLVES = 5;
     public const int PLOT_ACTION_ARMY_ELVES = 6;
     public const int PLOT_ACTION_ARMY_TEMPLARS = 7;
     public const int PLOT_ACTION_ARMY_WIZARDS = 8;
     public const int PLOT_ACTION_SHAPESHIFT_GOLEM = 9;
     public const int PLOT_ACTION_SHAPESHIFT_SPIRIT = 10;
     public const int PLOT_ACTION_SHAPESHIFT_BURNING_MAN = 11;
     public const int PLOT_ACTION_SHAPESHIFT_MOUSE = 12;
     public const int PLOT_ACTION_SHAPESHIFT_ARCANE_HORROR = 13;

     // Starting Equipment rules
     public const int EQUIP_RULE_EQUIP_ALL = 1;
     public const int EQUIP_RULE_EQUIP_ONLY_ARMOR = 2;
     public const int EQUIP_RULE_EQUIP_NOTHING = 3;
     public const int EQUIP_RULE_CHEST_EXCEPT_ARMOR = 4;
     public const int EQUIP_RULE_CHEST_ALL = 5;

     // ----------------------------------------------------------------------------*
     // Area of Effect Data
     // AOE.xls
     // ----------------------------------------------------------------------------*

     // Trap AOEs
     public const int AOE_INVALID = 0;
     public const int AOE_TRAP_GEM_LURE = 201;
     public const int AOE_TRAP_SUMMONING_CIRCLE = 202;
     public const int AOE_GREASE = 203;
     public const int AOE_BURNING_GREASE = 204;
     public const int AOE_GLYPH_OF_WARDING = 205;
     public const int AOE_GAS_CLOUD = 15;

     // Plot Related AOEs
     public const int AOE_PLOT_CIR_FADE_FIRE_LARGE = 1001;
     public const int AOE_PLOT_CIR_FADE_FIRE_SMALL = 1002;
     public const int AOE_PLOT_CIR_FADE_BURNING_MAN_FORM = 1003;
     public const int AOE_PLOT_CIR_FADE_BURNING_DARKSPAWN = 1004;
     public const int AOE_PLOT_ARL_FIRE_TRAP = 1005;

     // APR_base
     public const int APR_TYPE_OGRE = 47;
     public const int APR_TYPE_BROODMOTHER = 10;
     public const int APR_TYPE_WEREWOLF_A = 22;
     public const int APP_TYPE_ARCHDEMON = 82;
     public const int APP_TYPE_HIGHDRAGON = 66;
     public const int APP_TYPE_WILD_SYLVAN = 65;
     public const int APP_TYPE_DRAGON = 4;
     public const int APP_TYPE_DRAKE = 80;
     public const int APP_TYPE_PRIDE_DEMON = 72;
     public const int APP_TYPE_HURLOCK = 16;
     public const int APP_TYPE_GENLOCK = 50;

     // itemprps.xls

     public const int ITEM_PROPERTY_ONHIT_SLAY_DARKSPAWN = 4005;
     public const int ITEM_PROPERTY_ONHIT_MAGESLAYER = 4006;
     public const int ITEM_PROPERTY_ONHIT_VICIOUS = 10002;
     public const int ITEM_PROPERTY_NO_ATTRIBUTE_REQUIREMENTS = 10000;
     public const int ITEM_PROPERTY_IS_HEALING_POTION = 6116;
     public const int ITEM_PROPERTY_IS_MANA_POTION = 6117;
     public const int ITEM_PROPERTY_ONHIT_SLOW = 6114;
}