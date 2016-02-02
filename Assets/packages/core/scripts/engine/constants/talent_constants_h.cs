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
     //#include"core_h"
     // Archery

     // Master Archer

     // aim
     public const float AIM_CRITICAL_HIT_BONUS_FACTOR = 1.0f;
     public const float AIM_ATTACK_BONUS = 10.0f;
     public const float AIM_SPEED_DELAY = 1.5f;
     public const float AIM_ARMOR_PENETRATION_INCREASE = 3.0f;
     public const float AIM_DAMAGE_INCREASE = 2.0f;
     public const float AIM_VFX_DURATION = 3.0f;

     // defensive fire
     public const float DEFENSIVE_FIRE_AIM_SPEED_INCREASE = 1.2f;
     public const float DEFENSIVE_FIRE_DEFENSE_BONUS = 15.0f;
     public const float DEFENSIVE_FIRE_VFX_DURATION = 3.0f;

     // master archer
     public const float MASTER_ARCHER_AIM_ATTACK_BONUS = 5.0f;
     public const float MASTER_ARCHER_AIM_PENETRATION_BONUS = 2.0f;
     public const float MASTER_ARCHER_AIM_DAMAGE_BONUS = 1.0f;
     public const float MASTER_ARCHER_DEFENSIVE_FIRE_BONUS = 15.0f;
     public const float MASTER_ARCHER_CRIPPLING_SHOT_ATTACK_BONUS = 15.0f;
     public const float MASTER_ARCHER_CRITICAL_SHOT_PENETRATION_BONUS = 10.0f;
     public const float MASTER_ARCHER_ARROW_OF_SLAYING_ATTACK_BONUS = 15.0f;
     public const float MASTER_ARCHER_RAPID_SHOT_BONUS = -1.0f;
     public const float MASTER_ARCHER_SHATTERING_SHOT_ARMOR_PENALTY_BONUS = -10.0f;

     // Precision

     // pinning shot
     public const float PINNING_SHOT_SLOW_FACTOR = 0.5f;
     public const float PINNING_SHOT_DURATION = 8.5f;

     // crippling shot
     public const float CRIPPLING_SHOT_ATTACK_PENALTY = -10.0f;
     public const float CRIPPLING_SHOT_DEFENSE_PENALTY = -10.0f;
     public const float CRIPPLING_SHOT_DURATION = 20.0f;

     // critical shot
     public const float CRITICAL_SHOT_PENETRATION_BONUS = 10.0f;
     public const float CRITICAL_SHOT_PENETRATION_FACTOR = 1.0f;

     // arrow of slaying
     public const float ARROW_OF_SLAYING_ATTACK_BONUS = 5.0f;
     public const int ARROW_OF_SLAYING_MINIMUM_LEVEL_DIFFERENCE = 1;
     public const float ARROW_OF_SLAYING_DAMAGE_BASE = 25.0f;
     public const float ARROW_OF_SLAYING_BOSS_DAMAGE_FACTOR = 0.1f;
     public const float ARROW_OF_SLAYING_REGENERATION_PENALTY = -1.0f;
     public const float ARROW_OF_SLAYING_REGENERATION_PENALTY_DURATION = 10.0f;
     public const int ARROW_OF_SLAYING_CASTER_VFX = 90006;

     // Arrow Mastery

     // rapidshot
     public const float RAPIDSHOT_AIM_SPEED_FACTOR = 0.05f;
     public const float RAPIDSHOT_AIM_SPEED_MAX = 2.0f;
     public const float RAPIDSHOT_ATTACK_PENALTY = -10.0f;
     public const float RAPIDSHOT_VFX_DURATION = 3.0f;

     // shattering shot
     public const float SHATTERING_SHOT_ARMOR_PENALTY = -10.0f;
     public const float SHATTERING_SHOT_DURATION = 20.0f;
     public const int SHATTERING_SHOT_VFX = 90009;

     // Assassin

     // Assassination

     // mark of death
     public const float MARK_OF_DEATH_DAMAGE_SCALE = 0.2f;
     public const float MARK_OF_DEATH_DURATION = 20.0f;

     // Bard

     // Bardic Song

     // song of valor
     public const float SONG_OF_VALOR_STAMINA_REGENERATION_BONUS = 0.5f;
     public const float SONG_OF_VALOR_ATTRIBUTE_FACTOR = 0.01f;
     public const float SONG_OF_VALOR_EXPLORE_FACTOR = 4.0f;

     // song of courage
     public const float SONG_OF_COURAGE_CRITICAL_BASE = 3.0f;
     public const float SONG_OF_COURAGE_CRITICAL_FACTOR = 0.1f;
     public const float SONG_OF_COURAGE_ATTACK_BASE = 3.0f;
     public const float SONG_OF_COURAGE_ATTACK_FACTOR = 0.1f;
     public const float SONG_OF_COURAGE_DAMAGE_BASE = 2.0f;
     public const float SONG_OF_COURAGE_DAMAGE_FACTOR = 0.05f;

     // distraction
     public const float DISTRACTION_DURATION = 5.0f;

     // captivate
     public const int CAPTIVATE_AOE = 217;
     public const float CAPTIVATE_DURATION = 3.0f;
     public const float CAPTIVATE_INTERVAL_DURATION = 4.0f;
     public const float CAPTIVATE_STAMINA_REGENERATION_PENALTY = -12.0f;
     public const int CAPTIVATE_AOE_VFX = 90016;

     // Berserker

     // Berserker's Rage

     // berserk
     public const float BERSERK_DAMAGE_BONUS = 8.0f;
     public const float BERSERK_MENTAL_RESISTANCE_BONUS = 10.0f;

     // resilience
     public const float RESILIENCE_HEALTH_REGENERATION_BONUS = 1.5f;

     // constraint
     public const float CONSTRAINT_STAMINA_REGENERATION_BONUS_COMBAT = 2.0f;

     // final blow
     public const float FINAL_BLOW_FACTOR = 0.5f;
     public const int FINAL_BLOW_CASTER_VFX = 90021;

     // Champion

     // Champion's Aura

     // war cry
     public const float WAR_CRY_ATTACK_PENALTY = -10.0f;
     public const float WAR_CRY_DURATION = 20.0f;
     public const int WAR_CRY_CASTER_VFX = 90022;

     // rally
     public const int RALLY_AOE = 218;
     public const float RALLY_DEFENSE_BONUS = 10.0f;
     public const int RALLY_AOE_VFX = 90024;

     // motivate
     public const float MOTIVATE_RALLY_ATTACK_BONUS = 10.0f;

     // Dog

     // Wardog Strength

     // Shred
     public const float DOG_SHRED_BLEED_DAMAGE_FACTOR = 1.0f;
     public const float DOG_SHRED_BLEED_DAMAGE_DURATION = 10.0f;

     // Wardog Intimidation

     // Growl
     public const float DOG_GROWL_DEFENSE_PENALTY = -15.0f;
     public const float DOG_GROWL_DURATION = 30.0f;

     // Overwhelm
     public const float DOG_OVERWHELM_DURATION = 10.0f;
     public const float DOG_OVERWHELM_DEFENSE_PENALTY = -10.0f;

     // Mabari Howl
     public const float DOG_MABARI_HOWL_DEFENSE_PENALTY = -15.0f;
     public const float DOG_MABARI_HOWL_DURATION = 5.0f;

     // Duelist

     // Duelist's Art

     // Dueling
     public const float DUELING_ATTACK_BONUS = 10.0f;

     // Upset Balance
     public const float UPSET_BALANCE_DEFENSE_PENALTY = -20.0f;
     public const float UPSET_BALANCE_SPEED_PENALTY = 0.8f;
     public const float UPSET_BALANCE_DURATION = 10.0f;

     // Keen Defense
     public const float KEEN_DEFENSE_DEFENSE_BONUS = 10.0f;

     // pinpoint strike
     public const float PINPOINT_STRIKE_DURATION = 15.0f;

     // Ranger

     // Nature's Ally

     // Natural Regeneration
     public const float NATURAL_REGENERATION_HEALTH_REGENERATION_BONUS = 2.5f;

     // Reaver

     // Path of Pain

     // Devour
     public const int DEVOUR_CORPSE_MAX = 100;
     public const float DEVOUR_HEAL_FACTOR = 0.4f;
     public const int DEVOUR_HEAL_VFX = 90040;

     // Frightening
     public const float FRIGHTENING_TAUNT_BONUS = 100.0f;
     public const float FRIGHTENING_THREATEN_BONUS = 1.0f;
     public const float FRIGHTENING_DURATION = 10.0f;

     // Pain
     public const int PAIN_AOE = 219;
     public const float PAIN_HEALTH_REGENERATION_PENALTY = -5.0f;
     public const float PAIN_INTERVAL_DURATION = 4.0f;
     public const float PAIN_INTERVAL_DAMAGE = 20.0f;
     public const int PAIN_AOE_VFX = 90043;

     // Blood Frenzy
     public const float BLOOD_FRENZY_HEALTH_REGENERATION_PENALTY = -5.0f;

     // Rogue

     // Rogue Special Techniques

     // Dirty Fighting
     public const float DIRTY_FIGHTING_STUN_DURATION = 4.0f;

     // Rogue Combat Tactics

     // Below the Belt
     public const float BELOW_THE_BELT_SPEED_PENALTY = 0.5f;
     public const float BELOW_THE_BELT_DEFENSE_PENALTY = -5.0f;
     public const float BELOW_THE_BELT_DURATION = 10.0f;

     // Deadly Strike
     public const float DEADLY_STRIKE_ARMOR_PENETRATION_BONUS = 10.0f;
     public const float DEADLY_STRIKE_ATTACK_BONUS = 10.0f;
     public const float DEADLY_STRIKE_DEXTERITY_FACTOR = 0.5f;

     // Devices

     // Stealth

     // Templar

     // The Order of the Templar

     // Holy Smite
     public const float HOLY_SMITE_BASE_DAMAGE_FACTOR = 0.2f;
     public const float HOLY_SMITE_MANA_FACTOR = 0.4f;
     public const float HOLY_SMITE_MANA_DAMAGE_FACTOR = 0.5f;
     public const float HOLY_SMITE_STUN_DURATION = 10.0f;

     // Warrior

     // Aggression

     // Threaten
     public const int THREATEN_IMPACT_VFX = 90286;

     // Tactics

     // Precise Striking
     public const float PRECISE_STRIKING_ATTACK_BONUS = 10.0f;
     public const float PRECISE_STRIKING_CRIT_BONUS_BASE = 2.5f;
     public const float PRECISE_STRIKING_CRIT_BONUS_PER_LEVEL = 0.5f;
     public const float PRECISE_STRIKING_SPEED_PENALTY = 0.1f;

     // Disengage
     public const float DISENGAGE_THREAT_DECREASE = -100.0f;

     // Taunt
     public const float TAUNT_THREAT_INCREASE = 300.0f;

     // Perfect Strike
     public const float PERFECT_STRIKE_ATTACK_BONUS = 100.0f;
     public const float PERFECT_STRIKE_DURATION = 15.0f;

     // Dual Weapon

     // Momentum

     // Dual Weapon Sweep
     public const float DUAL_WEAPON_SWEEP_LEFT_ARC_START = 0.0f;
     public const float DUAL_WEAPON_SWEEP_LEFT_ARC_END = 90.0f;
     public const float DUAL_WEAPON_SWEEP_RIGHT_ARC_START = 270.0f;
     public const float DUAL_WEAPON_SWEEP_RIGHT_ARC_END = 361.0f;

     // Momentum
     public const float MOMENTUM_MOVEMENT_SPEED_INCREASE = 1.4f;
     public const float MOMENTUM_ANIMATION_SPEED_INCREASE = -0.3f;
     public const float MOMENTUM_STAMINA_REGENERATION_PENALTY = REGENERATION_STAMINA_EXPLORE_NULL;
     public const float MOMENTUM_STAMINA_REGENERATION_PENALTY_COMBAT = REGENERATION_STAMINA_COMBAT_DEGENERATION;

     // Crippling Strikes

     // Riposte
     public const float RIPOSTE_STUN_DURATION = 4.0f;

     // Cripple
     public const float CRIPPLE_DEFENSE_PENALTY = -10.0f;
     public const float CRIPPLE_ATTACK_PENALTY = -10.0f;
     public const float CRIPPLE_SPEED_PENALTY = 0.6f;
     public const float CRIPPLE_DURATION = 10.0f;

     // Punisher
     public const float PUNISHER_ATTACK_PENALTY = -10.0f;
     public const float PUNISHER_DEFENSE_PENALTY = -10.0f;
     public const float PUNISHER_DURATION = 20.0f;

     // Dual Weapon Mastery

     // Skill

     // Herbalism

     // Persuade

     // Poison Making

     // Stealing

     // Survival

     // Trap Making

     // Two-Handed

     // Power Strikes

     // Powerful Swing
     public const float POWERFUL_SWING_ATTACK_PENALTY = -10.0f;
     public const float POWERFUL_SWING_DEFENSE_PENALTY = -10.0f;
     public const float POWERFUL_SWING_DAMAGE_BONUS = 5.0f;

     // Strong
     public const float STRONG_POWERFUL_SWING_ATTACK_BONUS = 5.0f;
     public const float STRONG_POWERFUL_SWING_DEFENSE_BONUS = 5.0f;

     // Two-Handed Sweep
     public const float TWO_HANDED_SWEEP_ATTACK_PENALTY = -10.0f;

     // Overpower

     // Critical Strike
     public const float CRITICAL_STRIKE_DEATHBLOW_PERCENTAGE = 0.2f;

     // Destruction

     // Sunder Weapon
     public const float SUNDER_WEAPON_ATTACK_PENALTY = -10.0f;
     public const float SUNDER_WEAPON_DURATION = 10.0f;

     // Sunder Armor
     public const float SUNDER_ARMOR_ARMOR_PENALTY = -20.0f;
     public const float SUNDER_ARMOR_DURATION = 10.0f;

     // Weapon and Shield

     // Shield Offense

     // Shield Pummel
     public const float SHIELD_PUMMEL_DURATION = 4.0f;

     // Assault
     public const float ASSAULT_ATTACK_BONUS = 8.0f;
     public const float ASSAULT_AP_BONUS = 3.0f;
     public const float ASSAULT_DAMAGE_MULTIPLIER = 0.6f;

     // Overpower
     public const float OVERPOWER_DURATION = 10.0f;

     // Shield Defense

     // Shield Defense
     public const float SHIELD_DEFENSE_DEFENSE_BONUS = 5.0f;
     public const float SHIELD_DEFENSE_MISSILE_SHIELD_BONUS = 5.0f;
     public const float SHIELD_DEFENSE_ATTACK_PENALTY = -5.0f;
     public const float SHIELD_DEFENSE_ABILITY_COST_PENALTY = 10.0f;

     // Shield Balance
     public const float SHIELD_BALANCE_SHIELD_DEFENSE_ATTACK_BONUS = 5.0f;

     // Shield Wall
     public const float SHIELD_WALL_ARMOR_BONUS = 5.0f;
     public const float SHIELD_WALL_MISSILE_SHIELD_BONUS = 5.0f;
     public const float SHIELD_WALL_ABILITY_COST_PENALTY = 20.0f;

     // Shield Expertise
     public const float SHIELD_EXPERTISE_SHIELD_DEFENSE_DEFENSE_BONUS = 5.0f;
     public const float SHIELD_EXPERTISE_SHIELD_DEFENSE_ABILITY_COST_BONUS = -5.0f;

     // Shield Mastery

     // Shield Cover
     public const float SHIELD_COVER_MISSILE_SHIELD_BONUS = 10.0f;

     // Shield Mastery
     public const float SHIELD_MASTERY_SHIELD_DEFENSE_DEFENSE_BONUS = 5.0f;
     public const float SHIELD_MASTERY_SHIELD_WALL_DEFENSE_BONUS = 10.0f;
     public const float SHIELD_MASTERY_SHIELD_COVER_MISSILE_SHIELD_BONUS = 10.0f;
}