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
     // Rage Demon Lava Burst
     public const float RAGE_DEMON_LAVA_BURST_TICK_DAMAGE = 0.25f;
     public const float RAGE_DEMON_LAVA_BURST_PULSE_INTERVAL = 1.0f;
     public const float RAGE_DEMON_LAVA_BURST_INSTANT_DAMAGE = 20.0f;
     public const int RAGE_DEMON_LAVA_BURST_AOE = 226;
     public const float RAGE_DEMON_LAVA_BURST_DURATION = 10.0f;
     public const int RAGE_DEMON_LAVA_BURST_AOE_VFX = 93051;

     public const int AURA_HEALING_AOE = 211;
     public const float AURA_HEALING_DURATION = 20.0f;
     public const float AURA_HEALING_HEARTBEAT_INTERVAL = 5.0f;
     public const float AURA_HEALING_HEALTH_FRACTION = 0.05f;
     public const int AURA_HEALING_VFX = 93052;

     public const int AURA_WEAKNESS_AOE = 212;
     public const float AURA_WEAKNESS_DURATION = 20.0f;
     public const float AURA_WEAKNESS_MOVEMENT_RATE = 0.7f;
     public const float AURA_WEAKNESS_ATTACK_PENALTY = -10.0f;
     public const float AURA_WEAKNESS_DEFENSE_PENALTY = -10.0f;
     public const int AURA_WEAKNESS_TARGET_VFX = 93055;
     public const int AURA_WEAKNESS_VFX = 93053;

     public const int AURA_FIRE_AOE = 225;
     public const float AURA_FIRE_BASE_HEARTBIT_DAMAGE = 0.5f;
     public const float AURA_FIRE_HEARTBEAT_INTERVAL = 2.0f;
     public const int AURA_FIRE_VFX = 93054;
     public const int AURA_FIRE_IMPACT_VFX = 93056;

     public const float ARCANEHORROR_AOE_RADIUS = 10.0f;
     public const float ARCANEHORROR_AOE_DIRECT_DAMAGE = 25.0f;
     public const float ARCANEHORROR_AOE_DOT_DAMAGE = 25.0f;
     public const float ARCANEHORROR_AOE_DOT_DURATION = 5.0f;
     public const float ARCANEHORROR_AOE_MANA_DRAIN_PER_LEVEL = 1.5f; // how much mana/stamina per level each target loses
     public const float ARCANEHORROR_AOE_HEALTH_GAIN_PERCENT = 1.0f; // how much of the loss the arcane horror gains
     public const float ARCANEHORROR_SWARM_ROOT_DUR = 3.0f;
     public const int ARCANEHORROR_SWARM_ROOT_CRUST_VFX = 93057;
     public const int ARCANEHORROR_AOE_DIRECT_IMPACT_VFX = 93058;
     public const int ARCANEHORROR_AOE_DOT_IMPACT_VFX = 93059;

     public const float ARCANEHORROR_SWARM_RADIUS = 30.0f;
     public const float ARCANEHORROR_SWARM_ROOT_DURATION = 3.0f;

     public const float ARCANEHORROR_BUFF_HEALTH = 100.0f;
     public const float ARCANEHORROR_BUFF_STAMINA = 100.0f;
     public const int ARCANEHORROR_BUFF_MAX_TARGETS = 20;
     public const int ARCANEHORROR_BUFF_VFX = 93087;

     public const float ABOMINATION_RAGE_SPEED_BONUS = 1.2f;
     public const float ABOMINATION_RAGE_STRENGTH_BONUS = 8.0f;
     public const float ABOMINATION_RAGE_DEFENSE_PENALTY = -20.0f;
     public const float ABOMINATION_RAGE_DURATION = 20.0f;
     public const float ABOMINATION_TRIPPLESTRIKE_DURATION = 3.0f;
     public const int ABOMINATION_RAGE_VFX = 93060;
     public const int ABOMINATION_TRIPPLESTRIKE_VFX = 93061;

     public const float ABOMINATION_DOT_TOTAL_DAMAGE = 30.0f;
     public const float ABOMINATION_DOT_DURATION = 5.0f;
     public const int ABOMINATION_DOT_IMPACT_VFX = 93062;

     public const float ABOMINATION_DRAIN_STAMINA = 10.0f;

     public const float ABOMINATION_ATTACK_PENALTY = 10.0f;
     public const float ABOMINATION_DEFENSE_PENALTY = 10.0f;
     public const float ABOMINATION_RESISTANCE_PENALTY = 10.0f;
     public const float ABOMINATION_DEBUF_DURATION = 5.0f;

     public const float ABOMINATION_STUN_DURATION = 3.0f;

     public const float BEAR_RAGE_STRENGTH_BONUS = 10.0f;
     public const float BEAR_RAGE_DEFENSE_PENALTY = -15.0f;
     public const float BEAR_RAGE_DURATION = 30.0f;

     public const float BEAR_SLAM_ARMOR_PENETRATION = 3.0f;

     public const float BRONTO_CHARGE_ARMOR_PENATRATION = 3.0f;
     public const float BRONTO_CHARGE_KNOCKDOWN_DISTANCE = -4.0f;

     public const float MONSTER_REVENANT_DOUBLE_STRIKE_RADIUS = 4.0f;
     public const float MONSTER_REVENANT_PULL_DAMAGE_PER_LEVEL = 2.5f;

     public const float GOLEM_QUAKE_RADIUS = 5.0f;
     public const float GOLEM_LIGHTNING_RADIUS = 5.0f;
     public const float GOLEM_QUAKE_STUN_DURATION = 6.0f;
     public const float GOLEM_RANGED_DAMAGE = 50.0f;
     public const int GOLEM_QUAKE_VFX = 93017;
     public const int GOLEM_RANGED1_VFX = 93094;

     public const float CORRUPTION_BURST_DEBUF_DURATION = 10.0f;
     public const float CORRUPTION_BURST_ATTACK_PENALTY = -10.0f;
     public const float CORRUPTION_BURST_DEFESE_PENALTY = -10.0f;
     public const float CORRUPTION_BURST_TOTAL_DAMAGE = 15.0f;
     public const float CORRUPTION_BURST_SPEED_DEBUF = 0.5f;
     public const float CORRUPTION_BURST_RADIUS = 5.0f;
     public const int CORRUPTION_BURST_IMPACT_VFX = 93045;
     public const int CORRUPTION_BURST_DOT_IMPACT_VFX = 93064;

     public const float SHRIEK_FRENZY_AP_BONUS = 2.0f;
     public const float SHRIEK_LEAP_AP = 4.0f;
     public const float SHREIK_LEAP_KNOCKDOWN_DISTANCE = -2.0f;
     public const float WEREWOLF_RAGE_ATTACK_BONUS = 5.0f;
     public const float WEREWOLF_RAGE_DAMAGE_BONUS = 2.0f;
     public const float WEREWOLF_RAGE_DEFENSE_PENALTY = -20.0f;
     public const float WEREWOLF_RAGE_DURATION = 8.0f;
     public const int WEREWOLF_RAGE_VFX = 93065;

     public const float SHRIEK_SCREAM_RADIUS = 8.0f;
     public const float SHRIEK_SCREAM_STUN_DURATION = 3.0f;
     public const float SHRIEK_SCREAM_BASE_DAMAGE = 2.0f;
     public const float SHRIEK_SCREAM_CAST_DURATION = 2.0f;
     public const int SHRIEK_SCREAM_STUN_CRUST_VFX = 93066;
     public const int SHRIEK_SCREAN_AOE_VFX = 1024;

     public const float ASHWRAITH_WHIRLWIND_DAMAGE = 20.0f;
     public const float ASHWRAITH_WHIRLWIND_STUN_DURATION = 5.0f;
     public const float ASHWRAITH_WHIRLWIND_RADIUS = 5.0f;

     public const float ASHWRAITH_DRAIN_MANA_LEAP = 5.0f;
     public const float ASHWRAITH_LEAP_STUN_DURATION = 4.0f;
     public const int ASHWRAITH_LEAP_IMPACT_VFX = 93070;

     public const float MONSTER_SUCCUBUS_SCREAM_RADIUS = 10.0f;
     public const float MONSTER_SUCCUBUS_SCREAM_STUN_DURATION = 3.0f;
     public const float MONSTER_SUCCUBUS_SCREAM_BASE_DAMAGE = 3.0f;
     public const int MONSTER_SUCCUBUS_SCREAM_CRUST_VFX = 93067;
     public const int MONSTER_SUCCUBUS_SCREAM_AOE_VFX = 1024;

     public const float MONSTER_SUCCUBUS_DANCE_RADIUS = 12.0f;
     public const float MONSTER_SUCCUBUS_SLEEP_DURATION = 5.0f;
     public const float MONSTER_SUCCUBUS_CURSE_DAMAGE_TOTAL = 25.0f;
     public const float MONSTER_SUCCUBUS_DEBUF_DURATION = 10.0f;
     public const float MONSTER_SUCCUBUS_DEBUF_RESIST_PENALTY = -20.0f;
     public const float MONSTER_SUCCUBUS_HEALTH_DEGEN_PENALTY = -1000.0f;
     public const int MONSTER_SUCCUBUS_DANCE_AOE_VFX = 93071;
     public const int MONSTER_SUCCUBUS_DANCE_IMPACT_VFX = 93072;
     public const int MONSTER_SUCCUBUS_FEAR_VFX = 93073;
     //1104

     public const float STALKER_SCARE_ATTACK_PENALTY = -5.0f;
     public const float STALKER_SCARE_DEFENSE_PENALTY = -5.0f;
     public const float STALKER_SCARE_DURATION = 10.0f;
     public const float STALKER_SPIT_DAMAGE = 10.0f;
     public const int STALKER_SPIT_BLIND_CHANCE = 30;
     public const float STALKER_SPIT_BLIND_DURATION = 3.0f;
     public const float STALKER_SLOW_DURATION = 5.0f;
     public const float STALKER_SLOW_ATTACK_PENALTY = -5.0f;
     public const float STALKER_SLOW_DEFENSE_PENALTY = -5.0f;
     public const int STALKER_SLOW_CRUST_VFX = 93075;
     public const int STALKER_SPIT_IMPACT_VFX = 93076;

     // Pride Demon
     //=============================

     // Pride Demon Flame Blast
     public const float PRIDE_DEMON_FLAME_BLAST_INSTANT_DAMAGE_PER_LEVEL = 1.5f;
     public const float PRIDE_DEMON_FLAME_BLAST_DOT_DAMAGE_PER_LEVEL = 1.5f;
     public const float PRIDE_DEMON_FLAME_BLAST_DOT_DURATION = 5.0f;
     public const float PRIDE_DEMON_FLAME_BLAST_RANGE = 6.0f;

     // Pride Demon Frost Blast
     public const float PRIDE_DEMON_FROST_BLAST_INSTANT_DAMAGE_PER_LEVEL = 1.5f;
     public const float PRIDE_DEMON_FROST_BLAST_FREEZE_DURATION = 3.0f;
     public const float PRIDE_DEMON_FROST_BLAST_RANGE = 6.0f;
     public const int PRIDE_DEMON_FROST_BLAST_VFX = 93089;

     // Pride Demon Mana Wave
     public const float PRIDE_DEMON_MANA_WAVE_RANGE = 35.0f;
     public const float PRIDE_DEMON_MANA_WAVE_DRAIN_MANA_PER_LEVEL = 1.0f;

     // Pride Demon Fire Bolt
     public const float PRIDE_DEMON_FIRE_BOLT_INSTANT_DAMAGE_PER_LEVEL = 2.5f;
     public const float PRIDE_DEMON_FIRE_BOLT_DOT_DAMAGE_PER_LEVEL = 2.5f;
     public const float PRIDE_DEMON_FIRE_BOLT_DOT_DURATION = 5.0f;

     // Pride Demon Frost Bolt
     public const float PRIDE_DEMON_FROST_BOLT_INSTANT_DAMAGE_PER_LEVEL = 2.5f;
     public const float PRIDE_DEMON_FROST_BOLT_FREEZE_DURATION = 6.0f;
     public const int PRIDE_DEMON_FROST_BOLT_VFX = 93089;

     // Rage Demon
     //=============================

     // Pride Demon Fire Bolt
     public const float RAGE_DEMON_FIRE_BOLT_INSTANT_DAMAGE = 15.0f;
     public const float RAGE_DEMON_FIRE_BOLT_DOT_DAMAGE = 15.0f;
     public const float RAGE_DEMON_FIRE_BOLT_DOT_DURATION = 5.0f;

     // Shade
     //=============================

     // Shade Life Drain
     public const float SHADE_DRAIN_ENERGY = 15.0f;
     public const float SHADE_DAZE_DURARION = 2.0f;
     public const float SHADE_DRAIN_DAMAGE = 15.0f;
     public const int SHADE_IMPACT_VFX = 93074;

     public const float DRAGON_ATTACK_CONE_WIDTH = 60.0f;    // ogre attack cone for direcstional attack covers 60 degrees.
     public const float DRAGON_ATTACK_CONE_LENGTH = 4.5f;     // attack distance from center of creature

     // Dragon Roar
     public const float ROAR_RANGE = 10.0f;
     public const float ROAR_STUN_DURATION = 3.0f;
     public const float ROAR_ATTACK_PENALTY = -10.0f;
     public const float ROAR_DEFENSE_PENALTY = -10.0f;
     public const float ROAR_DEBUF_DURATION = 10.0f;

     // High Dragon Roar
     public const float HIGH_ROAR_RANGE = 20.0f;
     public const float HIGH_ROAR_STUN_DURATION = 2.0f;
     public const float HIGH_ROAR_ATTACK_PENALTY = -20.0f;
     public const float HIGH_ROAR_DEFENSE_PENALTY = -20.0f;
     public const float HIGH_ROAR_DEBUF_DURATION = 10.0f;
     public const float HIGH_ROAR_SHAKE_DURATION = 3.0f;

     // Dragon Breath
     public const float BREATH_INSTANT_DAMAGE = 30.0f;
     public const float BREATH_DOT_TOTAL = 30.0f;
     public const float BREATH_DOT_DURATION = 6.0f;
     public const float BREATH_ARC1_MIN = 20.0f;
     public const float BREATH_ARC1_MAX = 60.0f;
     public const float BREATH_ARC2_MIN = 340.0f;
     public const float BREATH_ARC2_MAX = 20.0f;
     public const float BREATH_ARC3_MIN = 300.0f;
     public const float BREATH_ARC3_MAX = 340.0f;
     public const int BREATH_IMPACT_VFX = 93077;

     // High Dragon Breath
     public const float HIGH_BREATH_INSTANT_DAMAGE_PER_LEVEL = 3.0f; // applied twice sometimes
     public const float HIGH_BREATH_DOT_TOTAL = 25.0f; // not used
     public const float HIGH_BREATH_DOT_DURATION = 10.0f;
     public const float HIGH_BREATH_ARC1_MIN = 20.0f;
     public const float HIGH_BREATH_ARC1_MAX = 80.0f;
     public const float HIGH_BREATH_ARC2_MIN = 340.0f;
     public const float HIGH_BREATH_ARC2_MAX = 20.0f;
     public const float HIGH_BREATH_ARC3_MIN = 280.0f;
     public const float HIGH_BREATH_ARC3_MAX = 340.0f;
     public const int HIGH_BREATH_IMPACT_VFX = 93078;

     // Dragon Tail Flap
     public const float TAIL_FLAP_RANGE = 5.0f;
     public const float TAIL_FLAP_KNOCKDOWN_RANGE = -6.0f;

     // High Dragon Tail Flap
     public const float HIGH_TAIL_FLAP_KNOCKDOWN_RANGE = -13.0f;

     // Dragon Wing Buffet
     public const float WING_BUFFET_RANGE = 3.5f; // +1.4 persoanl space = 4.9m (should be a bit shorter than the knockback range)
     public const float WING_BUFFET_DAMAGE = 15.0f;
     public const float WING_BUFFET_KNOCKDOWN_RANGE = -5.0f;

     // High Dragon Wing Buffet
     public const float HIGH_WING_BUFFET_RANGE = 6.5f; // +3 personal space = 9.5m (should be a bit shorter than the knockback range)
     public const float HIGH_WING_BUFFET_DAMAGE = 20.0f;
     public const float HIGH_WING_BUFFET_KNOCKDOWN_RANGE = -10.0f;

     // Dragon Shred
     public const float SHRED_AP_BONUS = 8.0f;

     // Dragon Rake
     public const float RAKE_KNOCKDOWN_DISTANCE = -3.0f;

     // Dragonling breath
     public const float DRAGONLING_BREATH_INSTANT_DAMAGE = 10.0f;
     public const float DRAGONLING_BREATH_DOT_DAMAGE = 5.0f;
     public const float DRAGONLING_BREATH_DOT_DURATION = 5.0f;

     // High Dragon Fire Spit
     public const float HIGH_DRAGON_FIRE_SPIT_RADIUS = 5.0f;
     public const float HIGH_DRAGON_FIRE_SPIT_INSTAT_DAMAGE = 60.0f;
     public const float HIGH_DRAGON_FIRE_SPIT_DOT_DAMAGE = 24.0f;
     public const float HIGH_DRAGON_FIRE_SPIT_DOT_DURATION = 6.0f;

     // High Dragon Grab
     public const float HIGH_DRAGON_GRAB_DURATION = 4.0f;

     //public const float     OGRE_ATTACK_CONE_WIDTH      = 60.0f;    // ogre attack cone for directional attack covers 60 degrees.
     //public const float     OGRE_ATTACK_CONE_LENGTH     = 3.5f;     // ogre attack distance from center of ogre creature

     // monster ogre stomp
     public const int OGRE_STOMP_KNOCKDOWN_DEFENSE_PENALTY = 20;
     public const float OGRE_STOMP_RADIUS = 4.0f;
     public const float OGRE_STOMP_KNOCKDOWN_DURATION_MIN = 1.0f;
     public const float OGRE_STOMP_KNOCKDOWN_DURATION_MAX = 4.0f;

     // monser wild sylvan
     public const int SYLVAN_STOMP_KNOCKDOWN_DEFENSE_PENALTY = 25;
     public const float SYLVAN_STOMP_RADIUS = 5.0f;
     public const float SYLVAN_BOSS_RAGE_RADIUS = 15.0f;
     public const float SYLVAN_STOMP_KNOCKDOWN_DURATION_MIN = 1.0f;
     public const float SYLVAN_STOMP_KNOCKDOWN_DURATION_MAX = 4.0f;

     // monster canine howl
     public const float CANINE_HOWL_RADIUS = 5.0f;
     public const float CANINE_HOWL_DEFENSE_PENALTY = -5.0f;
     public const float CANINE_HOWL_DEFENSE_PENALTY_DURATION = 10.0f;
     public const float CANINE_HOWL_ATTACK_BONUS = 5.0f;
     public const float CANINE_HOWL_ATTACK_BONUS_DURATION = 10.0f;

     // large monster attacks
     public const float LARGE_MONSTER_MINIMUM_DAMAGE = 10.0f;
     public const int LARGE_MONSTER_KNOCKDOWN_DEFENSE_PENALTY = 10;
     public const float LARGE_MONSTER_KNOCKDOWN_DURATION = 3.0f;

     // monster ogre grab
     public const float OGRE_GRAB_DURATION = 10.0f;

     // overwhelm
     public const float OVERWHELM_DURATION = 2.0f;

     // broodmother
     public const float OGRE_ATTACK_CONE_WIDTH = 60.0f;    // ogre attack cone for direcstional attack covers 60 degrees.

     public const float BROODMOTHER_ATTACK_CONE_LENGTH = 4.5f;     // attack distance from center of ogre creature

     public const float GAS_DURATION = 10.0f;
     public const int GAS_AOE = 210;
     public const float GAS_DAMAGE = 100.0f;
     public const int GAS_TARGET_VFX = 93079;
     public const int GAS_VFX = 93082;

     //
     // Appearance types (from apr_base.xls)
     //
     public const int APPEARANCE_BROODMOTHER = 10;
     public const int APPEARANCE_WISP = 14;
     public const int APPEARANCE_BROODMOTHER_TENTACLE = 73;

     public const float SCREAM_LONG_RANGE = 20.0f;
     public const float SCREAM_MEDIUM_RANGE = 10.0f;
     public const float SCREAM_SHORT_RANGE = 5.0f;
     public const float SCREAM_KNOCKDOWN_DURATION = 2.0f;
     public const float SCREAM_STUN_DURATION = 3.0f;
     public const float SCREAM_DAZE_DURATION = 5.0f;
     public const float SCREAM_SHAKE_DURATION = 3.0f;

     public const float VOMIT_RANGE = 5.0f;
     public const float VOMIT_LEFT_MIN = 00.0f;
     public const float VOMIT_LEFT_MAX = 60.0f;
     public const float VOMIT_RIGHT_MIN = 300.0f;
     public const float VOMIT_RIGHT_MAX = 360.0f;
     public const float VOMIT_ARC1_MIN = 20.0f;
     public const float VOMIT_ARC1_MAX = 60.0f;
     public const float VOMIT_ARC2_MIN = 340.0f;
     public const float VOMIT_ARC2_MAX = 20.0f;
     public const float VOMIT_ARC3_MIN = 300.0f;
     public const float VOMIT_ARC3_MAX = 340.0f;
     public const float VOMIT_DAMAGE = 40.0f;
     public const int VOMIT_IMPACT_VFX = 93080;
     public const int VOMIT_ALL_VFX = 93084;
     public const int VOMIT_LEFT_VFX = 93085;
     public const int VOMIT_RIGHT_VFX = 93086;

     public const float SPIT_DAMAGE = 26.0f;
     public const float SPIT_RADIUS = 2.0f;
     public const int SPIT_IMPACT_VFX = 93081;

     public const float CHARGE_LEFT_RANGE = 7.0f;
     public const float CHARGE_LEFT_ARC_MIN = 10.0f;
     public const float CHARGE_LEFT_ARC_MAX = 80.0f;

     public const float CHARGE_RIGHT_RANGE = 5.0f;
     public const float CHARGE_RIGHT_ARC_MIN = 280.0f;
     public const float CHARGE_RIGHT_ARC_MAX = 350.0f;

     public const float GRAB_DURATION = 8.0f;

     // poison spit
     public const float POISON_SPIT_DAMAGE_FACTOR = 0.5f;
     public const float POISON_SPIT_DAMAGE_DURATION = 3.0f;

     // sylvan
     public const float SYLVAN_ATTACK_CONE_WIDTH = 60.0f;    // sylvan attack cone for directional attack covers 60 degrees.
     public const float SYLVAN_ATTACK_CONE_LENGTH = 3.5f;     // sylvan attack distance from center of ogre creature

     public const float SYLVAN_RAGE_DAMAGE_BONUS = 10.0f;
     public const float SYLVAN_RAGE_ATTACK_BONUS = 20.0f;
     public const float SYLVAN_RAGE_DURATION = 10.0f;
     public const float SYLVAN_RAGE_CALL_HELP_DISTANCE = 20.0f;
     public const int SYLVAN_RAGE_MAX_SYLVANS = 3;
     // sylvan boss rage
     public const float SYLVAN_RAGE_ATTACK_PENALTY_PER_LEVEL = -3.0f;
     public const float SYLVAN_RAGE_DEFENSE_PENALTY_PER_LEVEL = -3.0f;
     public const float SYLVAN_RAGE_DOT_TOTAL_DAMAGE_PER_LEVEL = 5.0f;

     public const float SYLVAN_ROOTING_DURATION = 5.0f;
     //public const float SYLVAN_ROOT_SINGLE_TARGET_ROOT_VFX_DURATION = 3.0f;
     //public const float SYLVAN_ROOT_SINGLE_TARGET_ROOT_DURATION = 3.5f;
     public const float SYLVAN_ROOTS_RADIUS = 20.0f;
     public const int SYLVAN_ROOTS_MAX_TARGETS = 4;
     public const float SYLVAN_ROOTS_TOTAL_DAMAGE_PER_LEVEL = 4.5f;
     public const int SYLVAN_ROOTS_VFX = 93083;

     // hurl

     public const float OGRE_HURL_DAMAGE_FACTOR = 0.5f;
     public const float GOLEM_HURL_DAMAGE_FACTOR = 0.5f;

     //----------------------
     //Archdemon
     //----------------------

     // Vortex
     public const int ARCHDEMON_VORTEX_VFX = 93090;
     public const int ARHCDMEON_VORTEX_AOE = 229;
     public const float ARCHDEMON_VORTEX_DURATION = 12.0f;
     public const float ARCHDEMON_VORTEX_HEARBEAT_DURATION = 5.0f;
     public const float ARCHDEMON_VORTEX_CORRUPTION_DAMAGE = 8.0f;
     public const float ARCHDEMON_VORTEX_DEBUFF_STRENGTH = -30.0f;

     //Smite
     public const int SMITE_RAND_RANGE = 5;
     public const float SMITE_MIN_DISTANCE = 2.5f;
     public const float SMITE_BLAST_DELAY = 0.75f;
     public const float ARCHDEMON_SMITE_DAMAGE = 30.0f;
     public const float ARCHDEMON_SMITE_RADIUS = 2.5f;
     public const float ARCHDEMON_SMITE_STUN_DURATION = 1.5f;

     // Detonate Darkspawn
     public const float ARCHDEMON_DETONATE_RADIUS = 5.0f;
     public const float ARCHDEMON_DETONATE_DAMAGE = 30.0f;

     // Corruption Blast
     public const float ARCHDEMON_CORRUPTION_BLAST_DAMAGE_PER_LEVEL = 2.5f;
}