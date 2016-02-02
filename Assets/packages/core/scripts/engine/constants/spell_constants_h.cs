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
     // constants for spells
     public const string SCRIPT_SPELL_AOE_DURATION = "spell_aoe_duration.ncs";
     public const float SHATTER_SPECIAL_CHANCE = 0.25f;

     // Arcane Warrior

     // Combat Magic

     // Combat Magic - M2
     public const float COMBAT_MAGIC_ATTACK_BONUS = 5.0f;

     // Aura of Might - P5
     public const float AURA_OF_MAGIC_COMBAT_MAGIC_ATTACK_BONUS = 5.0f;
     public const float AURA_OF_MAGIC_COMBAT_MAGIC_DEFENSE_BONUS = 10.0f;
     public const float AURA_OF_MAGIC_COMBAT_MAGIC_DAMAGE_BONUS = 5.0f;

     // Shimmering Shield - M2
     public const float SHIMMERING_SHIELD_ARMOR_BONUS = 15.0f;
     public const float SHIMMERING_SHIELD_FIRE_RESISTANCE_BONUS = 75.0f;
     public const float SHIMMERING_SHIELD_COLD_RESISTANCE_BONUS = 75.0f;
     public const float SHIMMERING_SHIELD_ELECTRICITY_RESISTANCE_BONUS = 75.0f;
     public const float SHIMMERING_SHIELD_NATURE_RESISTANCE_BONUS = 75.0f;
     public const float SHIMMERING_SHIELD_SPIRIT_RESISTANCE_BONUS = 75.0f;
     public const float SHIMMERING_SHIELD_PHYSICAL_RESISTANCE_BONUS = 75.0f;
     public const float SHIMMERING_SHIELD_MENTAL_RESISTANCE_BONUS = 75.0f;
     public const float SHIMMERING_SHIELD_MANA_REGENERATION_PENALTY = -10.0f;

     // Fade Shroud - P2
     public const float FADE_SHROUD_COMBAT_MAGIC_MANA_REGENERATION_BONUS = 1.0f;
     public const float FADE_SHROUD_COMBAT_MAGIC_DISPLACEMENT_BONUS = 25.0f;
     public const float FADE_SHROUD_COMBAT_MAGIC_TRANSPARENCY = 0.5f;

     // Blood Mage

     // Blood Magic

     // Blood Magic - M4
     public const float BLOOD_MAGIC_MANA_REGENERATION_PENALTY = -1000.0f;

     // Blood Sacrifice - S4
     public const float BLOOD_SACRIFICE_MAX_HEALTH = 50.0f;
     public const int BLOOD_SACRIFICE_CASTER_VFX = 90074;

     // Blood Wound - A4
     public const float BLOOD_WOUND_DAMAGE_FACTOR = 0.6f;
     public const float BLOOD_WOUND_DAMAGE_DURATION = 10.0f;
     public const float BLOOD_WOUND_PARALYSIS_DURATION = 10.0f;

     // Blood Control - S2
     public const float BLOOD_CONTROL_DURATION = 20.0f;
     public const float BLOOD_CONTROL_DAMAGE_FACTOR = 1.0f;

     // Shapeshifter

     // Shapeshifting

     // Bear Shape - M2
     public const float BEAR_SHAPE_MANA_REGENERATION_PENALTY = -5.0f;
     public const int BEAR_SHAPE_TRANSFORM_START_VFX = 90033;
     public const int BEAR_SHAPE_TRANSFORM_END_VFX = 90034;

     // Spider Shape - M2
     public const float SPIDER_SHAPE_MANA_REGENERATION_PENALTY = -5.0f;
     public const int SPIDER_SHAPE_TRANSFORM_START_VFX = 90035;
     public const int SPIDER_SHAPE_TRANSFORM_END_VFX = 90036;

     // Flying Swarm - M2
     public const float FLYING_SWARM_MANA_REGENERATION_PENALTY = -5.0f;
     public const int FLYING_SWARM_VFX = 90082;
     public const int FLYING_SWARM_TRANSFORM_START_VFX = 90038;
     public const int FLYING_SWARM_TRANSFORM_END_VFX = 90039;

     // Shapeshifter - PI2
     public const float SHAPESHIFTER_BEAR_SHAPE_MANA_REGENERATION_BONUS = 1.0f;
     public const float SHAPESHIFTER_SPIDER_SHAPE_MANA_REGENERATION_BONUS = 1.0f;
     public const float SHAPESHIFTER_FLYING_SWARM_MANA_REGENERATION_BONUS = 1.0f;

     // Spirithealer

     // Advanced Healing

     // Support the Weak - S4
     public const float SUPPORT_THE_WEAK_HEALING_FACTOR = 0.5f;

     // Revival - A4
     public const float REVIVAL_HEALING_FACTOR = 0.3f;

     // Lifeward - SH2
     public const float LIFEWARD_HEALING_FACTOR = 0.3f;
     public const float LIFEWARD_DURATION = 60.0f;
     public const int LIFEWARD_HEAL_VFX = 90087;

     // Cleansing Aura - M4
     public const int CLEANSING_AURA_AOE = 220;
     public const float CLEANSING_AURA_PULSE_INTERVAL = 5.0f;
     public const float CLEANSING_AURA_PULSE_INNER_RADIUS = 5.0f;
     public const float CLEANSING_AURA_PULSE_HEALING_FACTOR = 0.3f;
     public const float CLEANSING_AURA_MANA_REGENERATION_PENALTY = -10.0f;
     public const int CLEANSING_AURA_AOE_VFX = 90088;

     // Creation

     // Healing

     // Heal - S4
     public const float HEAL_HEALTH_FRACTION = 0.40f;

     // Rejuv - S4
     public const float REJUVINATION_REGEN_FACTOR = 8.0f;
     public const float REJUVINATION_DURATION = 10.0f;

     // Mass Rejuv
     public const float MASS_REJUVINATION_DURATION = 15.0f;
     public const float MASS_REJUVINATION_REGEN_FACTOR = 5.0f;

     // Regeneration - S4
     public const float REGENERATION_HEALING_FACTOR = 1.0f;
     public const float REGENERATION_DURATION = 10.0f;

     // Purify - S4
     public const float PURIFY_HEALING_FACTOR = 0.5f;

     // Nature

     // Spell Wisp - M4
     public const float SPELL_WISP_SPELLPOWER_FACTOR = 0.05f;

     // Grease - U4
     public const int GREASE_AOE = 203;
     public const float GREASE_DURATION = 20.0f;
     public const float GREASE_INTERVAL_DURATION = 1.5f;
     public const int GREASE_AOE_VFX = 90096;

     // Spellbloom - D4
     public const int SPELLBLOOM_AOE = 215;
     public const float SPELLBLOOM_MANA_REGENERATION_BONUS = 3.0f;
     public const float SPELLBLOOM_DURATION = 20.0f;
     public const int SPELLBLOOM_AOE_VFX = 90098;

     // Stinging Swarm - S
     public const float STINGING_SWARM_DAMAGE_FACTOR = 1.0f;
     public const float STINGING_SWARM_DURATION = 10.0f;
     public const float STINGING_SWARM_JUMP_THRESHOLD = 10.0f;

     // Glyphs

     // Glyph of Paralysis - S4
     public const int GLYPH_OF_PARALYSIS_TRAP = 301;
     public const int GLYPH_OF_PARALYSIS_MAX_NUMBER = 5;
     public const float GLYPH_OF_PARALYSIS_DURATION = 60.0f;
     public const string GLYPH_OF_PARALYSIS_TAG = "genip_trap_glyph";

     // Glyph of Warding - D2
     public const int GLYPH_OF_WARDING_AOE = 230;
     public const float GLYPH_OF_WARDING_DURATION = 20.0f;
     public const float GLYPH_OF_WARDING_DEFENSE_BONUS = 30.0f;
     public const float GLYPH_OF_WARDING_MENTAL_RESISTANCE_BONUS = 50.0f;
     public const float GLYPH_OF_WARDING_MISSILE_SHIELD_BONUS = 30.0f;
     public const int GLYPH_OF_WARDING_AOE_VFX = 90103;
     public const string GLYPH_OF_WARDING_TAG = "AOE_GLYPH_OF_WARDING";

     // Glyph of Repulsion - D4
     public const int GLYPH_OF_REPULSION_AOE = 231;
     public const float GLYPH_OF_REPULSION_DURATION = 20.0f;
     public const float GLYPH_OF_REPULSION_SPEED_PENALTY = 0.6f;
     public const int GLYPH_OF_REPULSION_AOE_VFX = 90105;
     public const string GLYPH_OF_REPULSION_TAG = "AOE_GLYPH_OF_REPULSION";

     // Glyph of Neutralization - D2
     public const int GLYPH_OF_NEUTRALIZATION_AOE = 232;
     public const float GLYPH_OF_NEUTRALIZATION_DURATION = 20.0f;
     public const float GLYPH_OF_NEUTRALIZATION_MANA_REGENERATION_PENALTY = -1000.0f;
     public const int GLYPH_OF_NEUTRALIZATION_AOE_VFX = 90107;

     // Heroism

     // Heroic Offense - S4
     public const float HEROIC_OFFENSE_ATTACK_FACTOR = 0.1f;
     public const float HEROIC_OFFENSE_DURATION = 20.0f;

     // Heroic Aura - S2
     public const float HEROIC_AURA_DURATION = 20.0f;
     public const float HEROIC_AURA_MISSILE_SHIELD_BONUS = 30.0f;

     // Heroic Defense - S4
     public const float HEROIC_DEFENSE_DEFENSE_FACTOR = 0.2f;
     public const float HEROIC_DEFENSE_DURATION = 20.0f;
     public const float HEROIC_DEFENSE_RESISTANCE_FACTOR = 0.1f;
     public const float HEROIC_DEFENSE_ABILITY_COST_PENALTY = 5.0f;

     // Haste - SH2
     public const float HASTE_MOVEMENT_SPEED_MODIFIER = 1.3f;
     public const float HASTE_ANIMATION_SPEED_MODIFIER = -0.25f;
     public const float HASTE_AIM_SPEED_MODIFIER = 0.8f;
     public const float HASTE_MANA_REGENERATION_PENALTY = -10.0f;

     // Entropy

     // Mental Contamination

     // Daze - S4
     public const float DAZE_DURATION = 10.0f;

     // Horror - S2
     public const float HORROR_DAMAGE_FACTOR = 1.0f;
     public const float HORROR_DURATION = 10.0f;
     public const int HORROR_NIGHTMARE_VFX = 90299;

     // Sleep - A4
     public const float SLEEP_DURATION = 12.0f;

     // Waking Nightmare - A2
     public const float WAKING_NIGHTMARE_DURATION = 20.0f;

     // Entropic Mortality

     // Drain Life - S4
     public const float DRAIN_LIFE_DAMAGE_FACTOR = 0.2f;
     public const int DRAIN_LIFE_CASTER_VFX = 90120;

     // Death Magic - MD2
     public const int DEATH_MAGIC_AOE = 221;
     public const float DEATH_MAGIC_HEALTH_FACTOR = 0.1f;
     public const int DEATH_MAGIC_AOE_VFX = 90121;
     public const int DEATH_MAGIC_CASTER_VFX = 90123;
     public const float DEATH_MAGIC_INTERVAL = 2.0f;

     // Curse of Mortality - SH2
     public const float CURSE_OF_MORTALITY_DURATION = 20.0f;
     public const float CURSE_OF_MORTALITY_HEALTH_REGENERATION_PENALTY = -1000.0f;
     public const float CURSE_OF_MORTALITY_DAMAGE_FACTOR = 0.5f;

     // Death Cloud - D4
     public const int DEATH_CLOUD_AOE = 214;
     public const float DEATH_CLOUD_DURATION = 30.0f;
     public const float DEATH_CLOUD_INTERVAL = 2.0f;
     public const float DEATH_CLOUD_DAMAGE_FACTOR = 0.1f;
     public const int DEATH_CLOUD_AOE_VFX = 90125;
     public const float DEATH_CLOUD_DEATH_HEX_DAMAGE_FACTOR = 2.0f;

     // Chains of Entropy

     // Weakness - S4
     public const float WEAKNESS_ATTACK_PENALTY = -10.0f;
     public const float WEAKNESS_DEFENSE_PENALTY = -10.0f;
     public const float WEAKNESS_SPEED_PENALTY = 0.8f;
     public const float WEAKNESS_DURATION = 20.0f;

     // Paralyze - S4
     public const float PARALYZE_DURATION = 15.0f;
     public const float PARALYZE_SPEED_PENALTY = 0.8f;

     // Miasma - MD4
     public const int MIASMA_AOE = 216;
     public const float MIASMA_ATTACK_PENALTY = -10.0f;
     public const float MIASMA_DEFENSE_PENALTY = -10.0f;
     public const float MIASMA_SPEED_PENALTY = 0.6f;
     public const int MIASMA_AOE_VFX = 90129;

     // Mass Paralysis - A4
     public const float MASS_PARALYSIS_DURATION = 10.0f;
     public const float MASS_PARALYSIS_SPEED_PENALTY = 0.8f;

     // Hexes

     // Vulnerability Hex - S4
     public const float VULNERABILITY_HEX_RESISTANCE_PENALTY_FACTOR = -0.3f;
     public const float VULNERABILITY_HEX_DURATION = 20.0f;

     // Affliction Hex - D4
     public const int AFFLICTION_HEX_AOE = 223;
     public const float AFFLICTION_HEX_DURATION = 20.0f;
     public const float AFFLICTION_HEX_RESISTANCE_PENALTY_FACTOR = -0.2f;
     public const int AFFLICTION_HEX_AOE_VFX = 90298;
     public const int AFFLICTION_HEX_VFX = 90134;

     // Misdirection Hex - S2
     public const float MISDIRECTION_HEX_DURATION = 20.0f;

     // Death Hex - S2
     public const float DEATH_HEX_DURATION = 10.0f;

     // Primal

     // Path of Fire

     // Flame Blast - C (fire DoT)
     public const float FLAME_BLAST_DAMAGE_FACTOR = 0.45f;
     public const float FLAME_BLAST_DAMAGE_DURATION = 5.5f;
     public const int FLAME_BLAST_BURNING_VFX = 90140;

     // Flaming Weapons - M (flaming weapons for party)

     // Fireball - A4
     public const float FIREBALL_DAMAGE_FACTOR = 0.3f;
     public const float FIREBALL_DURATION = 5.0f;
     public const int FIREBALL_BURNING_VFX = 90143;

     // Inferno - D4
     public const int INFERNO_AOE = 224;
     public const int INFERNO_AOE_VFX = 90144;
     public const float INFERNO_DURATION = 30.0f;
     public const float INFERNO_INTERVAL = 4.0f;
     public const float INFERNO_DAMAGE_FACTOR = 0.4f;
     public const float INFERNO_RADIUS = 10.0f;
     public const int INFERNO_BURNING_VFX = 90145;

     // Path of Stone

     // Rock Armor - M
     public const float ROCK_ARMOR_BONUS_FACTOR = 0.25f;
     public const float ROCK_ARMOR_BONUS_CAP = 12.25f;

     // Stonefist - S4
     public const float STONEFIST_DAMAGE_FACTOR = 0.3f;
     public const float STONEFIST_PETRIFY_SHATTER_CHANCE = 0.7f;

     // Earthquake - D4
     public const int EARTHQUAKE_AOE = 216;
     public const float EARTHQUAKE_DURATION = 20.0f;
     public const float EARTHQUAKE_INTERVAL = 4.0f;
     public const float EARTHQUAKE_SPEED_PENALTY = 0.6f;
     public const int EARTHQUAKE_AOE_VFX = 90148;

     // Petrify - S2
     public const float PETRIFY_DURATION = 20.0f;
     public const int PATRIFY_SHATTER_VFX = 90150;

     // Path of Storms

     // Shock - C (lightning dot)
     public const float SHOCK_DAMAGE_FACTOR = 0.4f;
     public const int SHOCK_ELECTRICITY_VFX = 90152;

     // Lightning - S4
     public const float LIGHTNING_DAMAGE_FACTOR = 0.3f;

     // Tempest - D4
     public const int TEMPEST_AOE = 213;
     public const float TEMPEST_DURATION = 30.0f;
     public const float TEMPEST_INTERVAL = 2.0f;
     public const float TEMPEST_DAMAGE_FACTOR = 0.1f;
     public const float TEMPEST_RADIUS = 10.0f;
     public const int TEMPEST_AOE_VFX = 90155;
     public const int TEMPEST_ELECTRICITY_VFX = 90156;
     public const string TEMPEST_TAG = "TEMPEST";

     // Path of Ice

     // Winter's Grasp - S4
     public const float WINTERS_GRASP_DAMAGE_FACTOR = 0.36f;
     public const float WINTERS_GRASP_DURATION = 5.0f;
     public const float WINTERS_GRASP_SPEED_PENALTY = 0.8f;
     public const int WINTERS_GRASP_FROZEN_VFX = 90158;
     public const int WINTERS_GRASP_SLOW_VFX = 90159;

     // Frost Weapons - M (cold weapons for party)

     // Cone of Cold - C (cold dot, speed debuff/frozen; frozen creatures are brittle)
     public const float CONE_OF_COLD_DAMAGE_FACTOR = 0.34f;
     public const float CONE_OF_COLD_DURATION = 8.0f;
     public const float CONE_OF_COLD_SPEED_PENALTY = 0.6f;
     public const int CONE_OF_COLD_FROZEN_VFX = 90162;
     public const int CONE_OF_COLD_SLOW_VFX = 90163;

     // Blizzard - U3 (cold dot, speed debuff/frozen)
     public const float BLIZZARD_DURATION = 30.0f;
     public const int BLIZZARD_AOE = 206;
     public const string BLIZZARD_RESOURCE = "spell_blizzard.ncs";
     public const float BLIZZARD_SLOW_FRACTION = 0.8f;
     public const float BLIZZARD_FIRE_RESISTANCE_BONUS = 50.0f;
     public const float BLIZZARD_DEFENSE_BONUS = 10.0f;
     public const float BLIZZARD_DAMAGE_FRACTION = 0.1f;
     public const float BLIZZARD_INTERVAL_DURATION = 2.0f;
     public const float BLIZZARD_FROZEN_DURATION = 4.0f;
     public const int BLIZZARD_AOE_VFX = 90165;
     public const int BLIZZARD_ICE_SHEET_VFX = 90166;
     public const int BLIZZARD_FROZEN_VFX = 90167;
     public const int BLIZZARD_SLOW_VFX = 90168;
     public const string BLIZZARD_TAG = "VFX_PER_BLIZZARD";

     // Spirit

     // Necromancy

     // Walking Bomb - S3
     public const float WALKING_BOMB_DURATION = 20.0f;
     public const int WALKING_BOMB_DEATH_VFX = 90170;
     public const float WALKING_BOMB_DAMAGE_FACTOR = 1.0f;

     // Death Syphon - MD2
     public const int DEATH_SYPHON_AOE = 224;
     public const float DEATH_SYPHON_MANA_FACTOR = 0.08f;
     public const int DEATH_SYPHON_AOE_VFX = 90171;
     public const int DEATH_SYPHON_CASTER_VFX = 90173;
     public const float DEATH_SYPHON_INTERVAL = 2.0f;

     // Virulent Walking Bomb - S3
     public const float VIRULENT_WALKING_BOMB_DURATION = 20.0f;
     public const int VIRULENT_WALKING_BOMB_DEATH_VFX = 90175;
     public const float VIRULENT_WALKING_BOMB_DAMAGE_FACTOR = 1.5f;

     // Animate Dead - M (if target is dead creature, degrad it and spawns skeleton (was:AOE + ghost)
     public const int ANIMATE_DEAD_SUMMON_VFX = 5005;
     public const int ANIMATE_DEAD_CONSUME_CORPSE_VFX = 1077;
     public const int ANIMATE_DEAD_NEW_PET_GLOW_VFX = 3007;
     public const int ANIMATE_WRONG_CORPSE_GLOW_VFX = 1011;
     public const int ANIMATE_RIGHT_CORPSE_GLOW_VFX = 1524;
     public const float ANIMATE_DEAD_AOE_RADIUS = 5.0f;
     public const int ANIMATE_DEAD_GET_UP_ANIMATION = 360;

     // Telekinesis

     // Mind Blast - C (knockdown)
     public const float MIND_BLAST_DURATION = 3.0f;

     // Force Field - S2
     public const float FORCE_FIELD_DURATION = 18.0f; // ev 200160

     // Telekinetic Weapons - M (knockdown weapons for party)

     // Crushing Prison - S3
     public const float CRUSHING_PRISON_DAMAGE_FACTOR = 1.25f;
     public const float CRUSHING_PRISON_DURATION = 9.0f;
     public const float CRUSHING_PRISON_SHATTER_CHANCE = 0.7f;

     // Soul Warding

     // Spell Shield - S2
     public const float SPELL_SHIELD_SPELL_RESISTANCE = 75.0f;
     public const float SPELL_SHIELD_DURATION = 20.0f;

     // Dispel Magic - S4

     // Antimagic Ward - S2
     public const float ANTIMAGIC_WARD_DURATION = 10.0f;

     // Antimagic Burst - S4
     public const string ANTIMAGIC_BURST_BLIZZARD_TAG = "VFX_PER_BLIZZARD";
     public const string ANTIMAGIC_BURST_TEMPEST_TAG = "TEMPEST";
     public const string ANTIMAGIC_BURST_DEATHCLOUD_TAG = "AOE_DEATH_CLOUD";
     public const string ANTIMAGIC_BURST_SPELLBLOOM_TAG = "AOE_SPELLBLOOM";
     public const string ANTIMAGIC_BURST_EARTHQUAKE_TAG = "AOE_EARTHQUAKE";
     public const string ANTIMAGIC_BURST_INFERNO_TAG = "AOE_INFERNO";

     // Mana Drain

     // Mana Drain - S4
     public const float MANA_DRAIN_FACTOR = 0.2f;
     public const int MANA_DRAIN_CASTER_VFX = 90186;

     // Mana Cleanse - A4
     public const float MANA_CLEANSE_FACTOR = 0.4f;

     // Spell Might - M4
     public const float SPELL_MIGHT_SPELLPOWER_FACTOR = 0.1f;
     public const float SPELL_MIGHT_MANA_REGENERATION_PENALTY = -4.0f;

     // Mana Clash - A4
     public const float MANA_CLASH_DAMAGE_FACTOR_BASE = 0.50f;
     public const float MANA_CLASH_DAMAGE_FACTOR = 0.01f;
     public const int MANA_CLASH_CASTER_VFX = 90192;

     // Wynne's Special Ability
     public const float WYNNE_DURATION = 60.0f;
     public const float WYNNE_PRE_TRINKET_RADIUS = 2.5f;
     public const float WYNNE_PRE_TRINKET_DAZE_DURATION = 6.0f;
     public const float WYNNE_PRE_TRINKET_HEALTH_FACTOR = 0.5f;
     public const float WYNNE_PRE_TRINKET_MANA_FACTOR = 0.5f;
     public const float WYNNE_PRE_TRINKET_SPELLPOWER_LEVEL_FACTOR = 1.0f;
     public const float WYNNE_PRE_TRINKET_SPELLPOWER_BONUS = 10.0f;
     public const float WYNNE_PRE_TRINKET_MANA_REGENERATION_BONUS = 2.0f;
     public const float WYNNE_POST_TRINKET_RADIUS = 5.0f;
     public const float WYNNE_POST_TRINKET_HEALTH_FACTOR = 1.0f;
     public const float WYNNE_POST_TRINKET_MANA_FACTOR = 1.0f;
     public const float WYNNE_POST_TRINKET_SPELLPOWER_BONUS = 20.0f;
     public const float WYNNE_POST_TRINKET_SPELLPOWER_LEVEL_FACTOR = 2.0f;
     public const float WYNNE_POST_TRINKET_MANA_REGENERATION_BONUS = 4.0f;

     // Branka/Caridin Special Abilities
     public const float BRANKA_ROCK_STRIKE_DURATION = 10.0f;
     public const float BRANKA_ROCK_STRIKE_DAZE_DURATION = 2.0f;
     public const float BRANKA_ROCK_STRIKE_NATURE_RESIST_MODIFER = -50.0f;
     public const float BRANKA_ROCK_STRIKE_DEFENSE_MODIFER = 10.0f;

     // magical implosion
     public const float MAGICAL_IMPLOSION_DAMAGE_FACTOR = 2.0f;
     public const int MAGICAL_IMPLOSION_VFX = 90276;

     // improved drain
     public const float IMPROVED_DRAIN_HEALTH_FACTOR = 0.3f;
     public const int IMPROVED_DRAIN_HEALTH_VFX = 90293;
     public const float IMPROVED_DRAIN_MANA_FACTOR = 0.3f;
     public const int IMPROVED_DRAIN_MANA_VFX = 90294;

     // death combo
     public const float DEATH_COMBO_DAMAGE_FACTOR = 2.0f;
     public const int DEATH_COMBO_CRUST_VFX = 90295;
     public const int DEATH_COMBO_LOCATION_VFX = 90300;

     // force explosion
     public const float FORCE_EXPLOSION_DAMAGE_FACTOR = 0.5f;
     public const float FORCE_EXPLOSION_RADIUS = 5.0f;
     public const int FORCE_EXPLOSION_VFX = 90296;
     public const int FORCE_EXPLOSION_IMPACT_VFX = 90276;

     // paralysis explosion
     public const float PARALYSIS_EXPLOSION_DURATION = 20.0f;
     public const int PARALYSIS_EXPLOSION_IMPACT_VFX = 90301;

     // storm of the century
     public const float STORM_OF_THE_CENTURY_DURATION = 30.0f;
     public const float STORM_OF_THE_CENTURY_DAMAGE_FACTOR = 0.4f;
     public const int STORM_OF_THE_CENTURY_AOE = 233;
     public const int STORM_OF_THE_CENTURY_AOE_VFX = 90273;
     public const float STORM_OF_THE_CENTURY_INTERVAL = 2.0f;
     public const float STORM_OF_THE_CENTURY_MANA_DRAIN = -100.0f;

     // Mage

     // Arcane Bolt
     public const float ARCANE_BOLT_DAMAGE_FACTOR = 0.30f;

     // Arcane Shield
     public const float ARCANE_SHIELD_DEFENSE_FACTOR = 0.1f;
}