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
     public const string SCRIPT_ITEM_AOE_DURATION = "item_aoe_duration.ncs";

     public const float ITEM_POWER_SCALING_FACTOR = 0.025f;

     // item ability constants
     public const int ABILITY_ITEM_ELFROOT = 200001;
     public const int ABILITY_ITEM_LYRIUM_DUST = 200002;
     public const int ABILITY_ITEM_DEEP_MUSHROOM = 200003;
     public const int ABILITY_ITEM_FIRE_CRYSTAL = 200004;
     public const int ABILITY_ITEM_FROSTROCK = 200005;
     public const int ABILITY_ITEM_LIFESTONE = 200006;
     public const int ABILITY_ITEM_FROZEN_LIGHTNING = 200007;
     public const int ABILITY_ITEM_SPIRIT_SHARD = 200008;
     public const int ABILITY_ITEM_LESSER_HEALTH_POULTICE = 200010;
     public const int ABILITY_ITEM_HEALTH_POULTICE = 200011;
     public const int ABILITY_ITEM_GREATER_HEALTH_POULTICE = 200012;
     public const int ABILITY_ITEM_POTENT_HEALTH_POULTICE = 200013;
     public const int ABILITY_ITEM_SHIMMERING_ORB = 200014;
     public const int ABILITY_ITEM_LESSER_LYRIUM_POTION = 200030;
     public const int ABILITY_ITEM_LYRIUM_POTION = 200031;
     public const int ABILITY_ITEM_GREATER_LYRIUM_POTION = 200032;
     public const int ABILITY_ITEM_POTENT_LYRIUM_POTION = 200033;
     public const int ABILITY_ITEM_ROCK_SALVE = 200040;
     public const int ABILITY_ITEM_INCENSE_OF_AWARENESS = 200041;
     public const int ABILITY_ITEM_SWIFT_SALVE = 200042;
     public const int ABILITY_ITEM_MABARI_CRUNCH = 200044;
     public const int ABILITY_ITEM_DOUBLE_BAKED_MABARI_CRUNCH = 200045;
     public const int ABILITY_ITEM_MINOR_INJURY_REPAIR_KIT = 200050;
     public const int ABILITY_ITEM_INJURY_REPAIR_KIT = 200051;
     public const int ABILITY_ITEM_MAJOR_INJURY_REPAIR_KIT = 200052;
     public const int ABILITY_ITEM_LESSER_ICE_SALVE = 200060;
     public const int ABILITY_ITEM_GREATER_ICE_SALVE = 200062;
     public const int ABILITY_ITEM_LESSER_WARMTH_BALM = 200070;
     public const int ABILITY_ITEM_GREATER_WARMTH_BALM = 200072;
     public const int ABILITY_ITEM_LESSER_ELIXIR_OF_GROUNDING = 200080;
     public const int ABILITY_ITEM_GREATER_ELIXIR_OF_GROUNDING = 200082;
     public const int ABILITY_ITEM_LESSER_NATURE_SALVE = 200090;
     public const int ABILITY_ITEM_GREATER_NATURE_SALVE = 200092;
     public const int ABILITY_ITEM_LESSER_SPIRIT_BALM = 200100;
     public const int ABILITY_ITEM_GREATER_SPIRIT_BALM = 200102;

     public const int ABILITY_ITEM_ACIDIC_COATING = 200110;
     public const int ABILITY_ITEM_FLAMING_COATING = 200111;
     public const int ABILITY_ITEM_FREEZING_COATING = 200112;
     public const int ABILITY_ITEM_SHOCK_COATING = 200113;
     public const int ABILITY_ITEM_SOULROT_COATING = 200114;
     public const int ABILITY_ITEM_VENOM = 200235;
     public const int ABILITY_ITEM_DEATHROOT_EXTRACT = 200236;
     public const int ABILITY_ITEM_CONCENTRATED_VENOM = 200237;
     public const int ABILITY_ITEM_CROW_POISON = 200238;
     public const int ABILITY_ITEM_CONCENTRATED_DEATHROOT_EXTRACT = 200239;
     public const int ABILITY_ITEM_SOLDIERS_BANE = 200240;
     public const int ABILITY_ITEM_MAGEBANE = 200241;
     public const int ABILITY_ITEM_ADDERS_KISS = 200242;
     public const int ABILITY_ITEM_DEMONIC_POISON = 200243;
     public const int ABILITY_ITEM_CONCENTRATED_CROW_POISON = 200244;
     public const int ABILITY_ITEM_FLESHROT = 200245;
     public const int ABILITY_ITEM_CONCENTRATED_SOLDIERS_BANE = 200246;
     public const int ABILITY_ITEM_CONCENTRATED_MAGEBANE = 200247;
     public const int ABILITY_ITEM_CONCENTRATED_DEMONIC_POISON = 200248;
     public const int ABILITY_ITEM_QUIET_DEATH = 200249;

     public const int ABILITY_ITEM_ACID_FLASK = 200120;
     public const int ABILITY_ITEM_FIRE_BOMB = 200121;
     public const int ABILITY_ITEM_FREEZE_BOMB = 200122;
     public const int ABILITY_ITEM_SHOCK_BOMB = 200123;
     public const int ABILITY_ITEM_SOULROT_BOMB = 200124;

     public const int ABILITY_ITEM_SMALL_GREASE_TRAP = 200210;
     public const int ABILITY_ITEM_LARGE_GREASE_TRAP = 200211;
     public const int ABILITY_ITEM_ACIDIC_GREASE_TRAP = 200212;
     public const int ABILITY_ITEM_SMALL_CALTROP_TRAP = 200213;
     public const int ABILITY_ITEM_LARGE_CALTROP_TRAP = 200214;
     public const int ABILITY_ITEM_POISONED_CALTROP_TRAP = 200215;
     public const int ABILITY_ITEM_MILD_CHOKING_POWDER_TRAP = 200216;
     public const int ABILITY_ITEM_CHOKING_POWDER_TRAP = 200217;
     public const int ABILITY_ITEM_CHOKING_POWDER_CLOUD_TRAP = 200218;
     public const int ABILITY_ITEM_SMALL_LURE = 200219;
     public const int ABILITY_ITEM_LARGE_LURE = 200220;
     public const int ABILITY_ITEM_IRRESISTABLE_LURE = 200221;
     public const int ABILITY_ITEM_SMALL_SHRAPNEL_TRAP = 200222;
     public const int ABILITY_ITEM_LARGE_SHRAPNEL_TRAP = 200223;
     public const int ABILITY_ITEM_SMALL_CLAW_TRAP = 200224;
     public const int ABILITY_ITEM_LARGE_CLAW_TRAP = 200225;
     public const int ABILITY_ITEM_ROPE_TRAP = 200226;
     public const int ABILITY_ITEM_MILD_SLEEPING_GAS_TRAP = 200227;
     public const int ABILITY_ITEM_SLEEPING_GAS_TRAP = 200228;
     public const int ABILITY_ITEM_SLEEPING_GAS_CLOUD_TRAP = 200229;
     public const int ABILITY_ITEM_ACIDIC_TRAP = 200230;
     public const int ABILITY_ITEM_FIRE_TRAP = 200231;
     public const int ABILITY_ITEM_FREEZE_TRAP = 200232;
     public const int ABILITY_ITEM_SHOCK_TRAP = 200233;
     public const int ABILITY_ITEM_SOULROT_TRAP = 200234;

     public const int ABILITY_ITEM_CURE_POTION = 200251;

     public const int ABILITY_ITEM_TALENT_BOOK = 200258;
     public const int ABILITY_ITEM_SKILL_BOOK = 200259;
     public const int ABILITY_ITEM_ATTRIBUTE_BOOK = 200260;

     public const int ABILITY_ITEM_KOLGRIMS_HORN = 200262;

     // item constant values

     public const float ELFROOT_HEAL = 10.0f;

     public const float LYRIUM_DUST_MANA = 10.0f;

     public const float DEEP_MUSHROOM_STAMINA = 10.0f;

     public const float FIRE_CRYSTAL_FIRE_RESISTANCE = 10.0f;
     public const float FIRE_CRYSTAL_DURATION = 60.0f;

     public const float FROSTROCK_COLD_RESISTANCE = 10.0f;
     public const float FROSTROCK_DURATION = 60.0f;

     public const float LIFESTONE_NATURE_RESISTANCE = 10.0f;
     public const float LIFESTONE_DURATION = 60.0f;

     public const float FROZEN_LIGHTNING_ELECTRICITY_RESISTANCE = 10.0f;
     public const float FROZEN_LIGHTNING_DURATION = 60.0f;

     public const float SPIRIT_SHARD_SPIRIT_RESISTANCE = 10.0f;
     public const float SPIRIT_SHARD_DURATION = 60.0f;

     public const float HEALTH_SALVE_BASE = 50.0f;
     public const float LESSER_HEALTH_SALVE_FACTOR = 1.0f;
     public const float HEALTH_SALVE_FACTOR = 2.0f;
     public const float GREATER_HEALTH_SALVE_FACTOR = 3.0f;
     public const float POTENT_HEALTH_SALVE_FACTOR = 4.0f;

     public const float LYRIUM_POTION_MAGIC_FACTOR = 0.5f;
     public const float LESSER_LYRIUM_POTION_BASE = 50.0f;
     public const float LYRIUM_POTION_BASE = 100.0f;
     public const float GREATER_LYRIUM_POTION_BASE = 150.0f;
     public const float POTENT_LYRIUM_POTION_BASE = 200.0f;

     public const float ROCK_SALVE_ARMOR_BONUS = 5.0f;
     public const float ROCK_SALVE_PHYSICAL_RESISTANCE_BONUS = 10.0f;
     public const float ROCK_SALVE_SPEED_PENALTY = 0.6f;
     public const float ROCK_SLAVE_DURATION = 120.0f;

     public const float INCENSE_OF_AWARENESS_DEFENSE_BONUS = 10.0f;
     public const float INCENSE_OF_AWARENESS_MENTAL_RESISTANCE_PENALTY = -10.0f;
     public const float INCENSE_OF_AWARENESS_DURATION = 120.0f;

     public const float SWIFT_SALVE_MOVEMENT_INCREASE = 1.2f;
     public const float SWIFT_SALVE_ATTACK_INCREASE = -0.2f;
     public const float SWIFT_SALVE_AIM_INCREASE = 0.9f;
     public const float SWIFT_SALVE_DURATION = 60.0f;

     public const float MABARI_CRUNCH_HEALTH_REGENERATION = 8.0f;
     public const float MABARI_CRUNCH_STAMINA_REGENERATION = 8.0f;
     public const float MABARI_CRUNCH_DURATION = 10.0f;

     public const float DOUBLE_BAKED_MABARI_CRUNCH_HEALTH_REGENERATION = 16.0f;
     public const float DOUBLE_BAKED_MABARI_CRUNCH_STAMINA_REGENERATION = 16.0f;
     public const float DOUBLE_BAKED_MABARI_CRUNCH_DURATION = 10.0f;

     public const float LESSER_INJURY_REPAIR_KIT_HEAL = 10.0f;

     public const float INJURY_REPAIR_KIT_HEAL = 20.0f;

     public const float GREATER_INJURY_REPAIR_KIT_HEAL = 40.0f;

     public const float LESSER_ICE_SALVE_COLD_RESISTANCE = 30.0f;
     public const float LESSER_ICE_SALVE_DURATION = 180.0f;

     public const float GREATER_ICE_SALVE_COLD_RESISTANCE = 60.0f;
     public const float GREATER_ICE_SALVE_DURATION = 180.0f;

     public const float LESSER_WARMTH_BALM_FIRE_RESISTANCE = 30.0f;
     public const float LESSER_WARMTH_BALM_DURATION = 180.0f;

     public const float GREATER_WARMTH_BALM_FIRE_RESISTANCE = 60.0f;
     public const float GREATER_WARMTH_BALM_DURATION = 180.0f;

     public const float LESSER_ELIXIR_OF_GROUNDING_ELECTRICITY_RESISTANCE = 30.0f;
     public const float LESSER_ELIXIR_OF_GROUNDING_DURATION = 180.0f;

     public const float GREATER_ELIXIR_OF_GROUNDING_ELECTRICITY_RESISTANCE = 60.0f;
     public const float GREATER_ELIXIR_OF_GROUNDING_DURATION = 180.0f;

     public const float LESSER_NATURE_SALVE_NATURE_RESISTANCE = 30.0f;
     public const float LESSER_NATURE_SALVE_DURATION = 180.0f;

     public const float GREATER_NATURE_SALVE_NATURE_RESISTANCE = 60.0f;
     public const float GREATER_NATURE_SALVE_DURATION = 180.0f;

     public const float LESSER_SPIRIT_BALM_SPIRIT_RESISTANCE = 30.0f;
     public const float LESSER_SPIRIT_BALM_DURATION = 180.0f;

     public const float GREATER_SPIRIT_BALM_SPIRIT_RESISTANCE = 60.0f;
     public const float GREATER_SPIRIT_BALM_DURATION = 180.0f;

     public const float ACID_FLASK_DAMAGE = 80.0f;

     public const float FIRE_BOMB_DAMAGE = 80.0f;

     public const float FREEZE_BOMB_DAMAGE = 80.0f;

     public const float SHOCK_BOMB_DAMAGE = 80.0f;

     public const float SPIRIT_BOMB_DAMAGE = 80.0f;

     public const int SMALL_GREASE_TRAP_OBJECT = 302;
     public const int SMALL_GREASE_TRAP_VFX = 92000;
     public const int SMALL_GREASE_TRAP_AOE = 300;
     public const int SMALL_GREASE_TRAP_AOE_VFX = 92001;
     public const float SMALL_GREASE_TRAP_DURATION = 20.0f;
     public const float SMALL_GREASE_TRAP_SPEED_PENALTY = 0.5f;
     public const string SMALL_GREASE_TRAP_RESOURCE = "gen_im_qck_trap_101.uti";

     public const int LARGE_GREASE_TRAP_OBJECT = 303;
     public const int LARGE_GREASE_TRAP_VFX = 92003;
     public const int LARGE_GREASE_TRAP_AOE = 301;
     public const int LARGE_GREASE_TRAP_AOE_VFX = 92004;
     public const float LARGE_GREASE_TRAP_DURATION = 20.0f;
     public const float LARGE_GREASE_TRAP_SPEED_PENALTY = 0.5f;
     public const string LARGE_GREASE_TRAP_RESOURCE = "gen_im_qck_trap_201.uti";

     public const int ACIDIC_GREASE_TRAP_OBJECT = 304;
     public const int ACIDIC_GREASE_TRAP_VFX = 92006;
     public const int ACIDIC_GREASE_TRAP_AOE = 301;
     public const int ACIDIC_GREASE_TRAP_AOE_VFX = 92007;
     public const float ACIDIC_GREASE_TRAP_DURATION = 20.0f;
     public const float ACIDIC_GREASE_TRAP_SPEED_PENALTY = 0.5f;
     public const float ACIDIC_GREASE_TRAP_NATURE_DPS = 4.0f;
     public const string ACIDIC_GREASE_TRAP_RESOURCE = "gen_im_qck_trap_301.uti";

     public const int SMALL_CALTROP_TRAP_AOE = 300;
     public const int SMALL_CALTROP_TRAP_AOE_VFX = 92009;
     public const float SMALL_CALTROP_TRAP_DURATION = 60.0f;
     public const float SMALL_CALTROP_TRAP_SPEED_PENALTY = 0.6f;
     public const float SMALL_CALTROP_TRAP_PHYSICAL_DPS = 4.0f;
     public const float SMALL_CALTROP_TRAP_INTERVAL = 2.0f;

     public const int LARGE_CALTROP_TRAP_AOE = 301;
     public const int LARGE_CALTROP_TRAP_AOE_VFX = 92010;
     public const float LARGE_CALTROP_TRAP_DURATION = 60.0f;
     public const float LARGE_CALTROP_TRAP_SPEED_PENALTY = 0.6f;
     public const float LARGE_CALTROP_TRAP_PHYSICAL_DPS = 4.0f;
     public const float LARGE_CALTROP_TRAP_INTERVAL = 2.0f;

     public const int POISONED_CALTROP_TRAP_AOE = 301;
     public const int POISONED_CALTROP_TRAP_AOE_VFX = 92011;
     public const float POISONED_CALTROP_TRAP_DURATION = 60.0f;
     public const float POISONED_CALTROP_TRAP_SPEED_PENALTY = 0.6f;
     public const float POISONED_CALTROP_TRAP_PHYSICAL_DPS = 4.0f;
     public const float POISONED_CALTROP_TRAP_NATURE_DPS = 4.0f;
     public const float POISONED_CALTROP_TRAP_INTERVAL = 2.0f;

     public const int MILD_CHOKING_POWDER_TRAP_OBJECT = 308;
     public const int MILD_CHOKING_POWDER_TRAP_VFX = 92012;
     public const float MILD_CHOKING_POWDER_TRAP_RADIUS = 2.5f;
     public const float MILD_CHOKING_POWDER_TRAP_DURATION = 10.0f;
     public const float MILD_CHOKING_POWDER_TRAP_SPEED_PENALTY = 0.6f;
     public const string MILD_CHOKING_POWDER_TRAP_RESOURCE = "gen_im_qck_trap_203.uti";

     public const int CHOKING_POWDER_TRAP_OBJECT = 309;
     public const int CHOKING_POWDER_TRAP_VFX = 92014;
     public const float CHOKING_POWDER_TRAP_RADIUS = 5.0f;
     public const float CHOKING_POWDER_TRAP_DURATION = 20.0f;
     public const float CHOKING_POWDER_TRAP_SPEED_PENALTY = 0.6f;
     public const string CHOKING_POWDER_TRAP_RESOURCE = "gen_im_qck_trap_303.uti";

     public const int CHOKING_POWDER_CLOUD_TRAP_OBJECT = 310;
     public const int CHOKING_POWDER_CLOUD_TRAP_VFX = 92016;
     public const int CHOKING_POWDER_CLOUD_TRAP_AOE = 301;
     public const int CHOKING_POWDER_CLOUD_TRAP_AOE_VFX = 92018;
     public const float CHOKING_POWDER_CLOUD_TRAP_DURATION = 20.0f;
     public const float CHOKING_POWDER_CLOUD_TRAP_SPEED_PENALTY = 0.6f;
     public const string CHOKING_POWDER_CLOUD_TRAP_RESOURCE = "gen_im_qck_trap_401.uti";

     public const int SMALL_LURE_OBJECT = 311;
     public const int SMALL_LURE_AOE = 301;
     public const int SMALL_LURE_AOE_VFX = 92037;
     public const string SMALL_LURE_RESOURCE = "gen_im_qck_trap_204.uti";
     public const int SMALL_LURE_VFX = 92034;
     public const float SMALL_LURE_STUN_DURATION = 10.0f;
     public const float SMALL_LURE_DESTROY_DELAY = 1.0f;

     public const int LARGE_LURE_OBJECT = 312;
     public const int LARGE_LURE_AOE = 302;
     public const int LARGE_LURE_AOE_VFX = 92038;
     public const string LARGE_LURE_RESOURCE = "gen_im_qck_trap_304.uti";
     public const int LARGE_LURE_VFX = 92035;
     public const float LARGE_LURE_STUN_DURATION = 20.0f;
     public const float LARGE_LURE_DESTROY_DELAY = 1.0f;

     public const int IRRESISTABLE_LURE_OBJECT = 313;
     public const int IRRESISTABLE_LURE_AOE = 303;
     public const int IRRESISTABLE_LURE_AOE_VFX = 92039;
     public const string IRRESISTABLE_LURE_RESOURCE = "gen_im_qck_trap_402.uti";
     public const int IRRESISTABLE_LURE_VFX = 92036;
     public const float IRRESISTABLE_LURE_STUN_DURATION = 20.0f;
     public const float IRRESISTABLE_LURE_DESTROY_DELAY = 10.0f;

     public const int SMALL_SHRAPNEL_TRAP_OBJECT = 314;
     public const int SMALL_SHRAPNEL_TRAP_VFX = 92041;
     public const float SMALL_SHRAPNEL_TRAP_RADIUS = 2.5f;
     public const float SMALL_SHRAPNEL_TRAP_DAMAGE = 60.0f;
     public const string SMALL_SHRAPNEL_TRAP_RESOURCE = "gen_im_qck_trap_205.uti";

     public const int LARGE_SHRAPNEL_TRAP_OBJECT = 315;
     public const int LARGE_SHRAPNEL_TRAP_VFX = 92042;
     public const float LARGE_SHRAPNEL_TRAP_RADIUS = 5.0f;
     public const float LARGE_SHRAPNEL_TRAP_DAMAGE = 80.0f;
     public const string LARGE_SHRAPNEL_TRAP_RESOURCE = "gen_im_qck_trap_305.uti";

     public const int SMALL_CLAW_TRAP_OBJECT = 316;
     public const float SMALL_CLAW_TRAP_DURATION = 15.0f;
     public const float SMALL_CLAW_TRAP_DAMAGE = 100.0f;
     public const string SMALL_CLAW_TRAP_RESOURCE = "gen_im_qck_trap_103.uti";

     public const int LARGE_CLAW_TRAP_OBJECT = 317;
     public const float LARGE_CLAW_TRAP_DURATION = 15.0f;
     public const float LARGE_CLAW_TRAP_DAMAGE = 150.0f;
     public const string LARGE_CLAW_TRAP_RESOURCE = "gen_im_qck_trap_206.uti";

     public const int ROPE_TRAP_OBJECT = 318;
     public const float ROPE_TRAP_DURATION = 5.0f;
     public const string ROPE_TRAP_RESOURCE = "gen_im_qck_trap_104.uti";

     public const int MILD_SLEEPING_GAS_TRAP_OBJECT = 319;
     public const int MILD_SLEEPING_GAS_TRAP_VFX = 92022;
     public const float MILD_SLEEPING_GAS_TRAP_RADIUS = 2.5f;
     public const float MILD_SLEEPING_GAS_TRAP_DURATION = 10.0f;
     public const string MILD_SLEEPING_GAS_TRAP_RESOURCE = "gen_im_qck_trap_207.uti";

     public const int SLEEPING_GAS_TRAP_OBJECT = 320;
     public const int SLEEPING_GAS_TRAP_VFX = 92024;
     public const float SLEEPING_GAS_TRAP_RADIUS = 5.0f;
     public const float SLEEPING_GAS_TRAP_DURATION = 20.0f;
     public const string SLEEPING_GAS_TRAP_RESOURCE = "gen_im_qck_trap_306.uti";

     public const int SLEEPING_GAS_CLOUD_TRAP_OBJECT = 321;
     public const int SLEEPING_GAS_CLOUD_TRAP_VFX = 92026;
     public const int SLEEPING_GAS_CLOUD_TRAP_AOE = 301;
     public const int SLEEPING_GAS_CLOUD_TRAP_AOE_VFX = 92028;
     public const float SLEEPING_GAS_CLOUD_TRAP_DURATION = 20.0f;
     public const string SLEEPING_GAS_CLOUD_TRAP_RESOURCE = "gen_im_qck_trap_403.uti";

     public const int ACIDIC_TRAP_OBJECT = 322;
     public const int ACIDIC_TRAP_VFX = 92029;
     public const float ACIDIC_TRAP_RADIUS = 2.5f;
     public const float ACIDIC_TRAP_DAMAGE = 100.0f;
     public const string ACIDIC_TRAP_RESOURCE = "gen_im_qck_trap_105.uti";

     public const int FIRE_TRAP_OBJECT = 323;
     public const int FIRE_TRAP_VFX = 92030;
     public const float FIRE_TRAP_RADIUS = 2.5f;
     public const float FIRE_TRAP_DAMAGE = 100.0f;
     public const string FIRE_TRAP_RESOURCE = "gen_im_qck_trap_208.uti";

     public const int FREEZE_TRAP_OBJECT = 324;
     public const int FREEZE_TRAP_VFX = 92031;
     public const float FREEZE_TRAP_RADIUS = 2.5f;
     public const float FREEZE_TRAP_DAMAGE = 100.0f;
     public const string FREEZE_TRAP_RESOURCE = "gen_im_qck_trap_209.uti";

     public const int SHOCK_TRAP_OBJECT = 325;
     public const int SHOCK_TRAP_VFX = 92032;
     public const float SHOCK_TRAP_RADIUS = 2.5f;
     public const float SHOCK_TRAP_DAMAGE = 100.0f;
     public const string SHOCK_TRAP_RESOURCE = "gen_im_qck_trap_210.uti";

     public const int SOULROT_TRAP_OBJECT = 326;
     public const int SOULROT_TRAP_VFX = 92033;
     public const float SOULROT_TRAP_RADIUS = 2.5f;
     public const float SOULROT_TRAP_DAMAGE = 100.0f;
     public const string SOULROT_TRAP_RESOURCE = "gen_im_qck_trap_307.uti";

     public const float ACIDIC_COATING_DURATION = 60.0f;
     public const float FLAMING_COATING_DURATION = 60.0f;
     public const float FREEZING_COATING_DURATION = 60.0f;
     public const float SHOCK_COATING_DURATION = 60.0f;
     public const float SOULROT_COATING_DURATION = 60.0f;
     public const float VENOM_DURATION = 60.0f;
     public const float DEATHROOT_EXTRACT_DURATION = 60.0f;
     public const float CONCENTRATED_VENOM_DURATION = 60.0f;
     public const float CROW_POISON_DURATION = 60.0f;
     public const float CONCENTRATED_DEATHROOT_EXTRACT_DURATION = 60.0f;
     public const float SOLDIERS_BANE_DURATION = 60.0f;
     public const float MAGEBANE_DURATION = 60.0f;
     public const float ADDERS_KISS_DURATION = 60.0f;
     public const float DEMONIC_POISON_DURATION = 60.0f;
     public const float CONCENTRATED_CROW_POISON_DURATION = 60.0f;
     public const float FLESHROT_DURATION = 60.0f;
     public const float CONCENTRATED_SOLDIERS_BANE_DURATION = 60.0f;
     public const float CONCENTRATED_MAGEBANE_DURATION = 60.0f;
     public const float CONCENTRATED_DEMONIC_POISON_DURATION = 60.0f;
     public const float QUIET_DEATH_DURATION = 60.0f;

     public const float TALENT_BOOK_DEFAULT = 1.0f;
     public const string TALENT_BOOK_STRING = "gen_im_qck_book_talent";
     public const int TALENT_BOOK_LENGTH = 22;

     public const float SKILL_BOOK_DEFAULT = 1.0f;
     public const string SKILL_BOOK_STRING = "gen_im_qck_book_skill";
     public const int SKILL_BOOK_LENGTH = 21;

     public const float ATTRIBUTE_BOOK_DEFAULT = 1.0f;
     public const string ATTRIBUTE_BOOK_STRING = "gen_im_qck_book_attribute";
     public const int ATTRIBUTE_BOOK_LENGTH = 25;
}