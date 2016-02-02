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
     //public void main() {}

     //#include"2da_constants_h"

     // Stat constants - Individual
     //moved public const int EngineConstants.HERO_STAT_KILLS = 1001;
     //moved public const int EngineConstants.HERO_STAT_TOTAL_DAMAGE_DEALT = 1002;
     //moved public const int EngineConstants.HERO_STAT_FRIENDLY_FIRE_DEALT = 1003;
     //moved public const int EngineConstants.HERO_STAT_HIGHEST_DAMAGE_DEALT = 1004;
     //moved public const int EngineConstants.HERO_STAT_TOTAL_DAMAGE_PERCENT = 1005;
     //moved public const int EngineConstants.HERO_STAT_HIT_RATE = 1006;
     //moved public const int EngineConstants.HERO_STAT_MOST_POWERFUL_SLAIN = 1007;
     //moved public const int EngineConstants.HERO_STAT_INJURIES = 1008;
     //moved public const int EngineConstants.HERO_STAT_MOST_POWERFUL_RATING = 1009;
     //moved public const int EngineConstants.HERO_STAT_ATTACK_COUNT = 1010;
     //moved public const int EngineConstants.HERO_STAT_HIT_COUNT = 1011;

     // Stat constants - Party
     //moved public const int EngineConstants.HERO_STAT_PARTY_KILLS = 2001;
     //moved public const int EngineConstants.HERO_STAT_PARTY_DAMAGE_DEALT = 2002;
     //moved public const int EngineConstants.HERO_STAT_MOST_POWERFUL_SLAIN_BY_PARTY = 2003;
     // Number of creature kills party stat
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_UNDEFINED = 0;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_DEMONS = 2004;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_FANTASTICAL = 2005;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_CONSTRUCT = 2006;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_HUMANOIDS = 2007;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_DARKSPAWNS = 2008;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_ARCH_ENEMIES = 2009;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_DRACONICS = 2010;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_CORRUPTED_ANIMALS = 2011;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_VERMIN = 2012;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_ANIMALS = 2013;
     //moved public const int EngineConstants.HERO_STAT_GROUPTYPE_HARMLESS = 2014;
     //moved public const int EngineConstants.HERO_STAT_STEALING_SUCCESS = 2015;
     //moved public const int EngineConstants.HERO_STAT_STEALING_FAILURE = 2016;
     //moved public const int EngineConstants.HERO_STAT_MOST_MONEY_EVER = 2017;
     //moved public const int EngineConstants.HERO_STAT_MONEY_SPENT = 2018;
     //moved public const int EngineConstants.HERO_STAT_CONVERSATIONS = 2019;
     //moved public const int EngineConstants.HERO_STAT_CODEX_ENTRIES = 2020;
     //moved public const int EngineConstants.HERO_STAT_WALKED_DISTANCE_MILES = 2021;
     //moved public const int EngineConstants.HERO_STAT_AREAS_EXPLORED = 2022;
     //moved public const int EngineConstants.HERO_STAT_TOTAL_TIME_PLAYED = 2023;
     //moved public const int EngineConstants.HERO_STAT_PREVIOUS_MONEY = 2024;
     //moved public const int EngineConstants.HERO_STAT_CURRENT_TIME_PLAYED = 2025;
     //moved public const int EngineConstants.HERO_STAT_TRACKING_POS_X = 2026;
     //moved public const int EngineConstants.HERO_STAT_TRACKING_POS_Y = 2027;
     //moved public const int EngineConstants.HERO_STAT_WALKED_DISTANCE_METERS = 2028;
     //moved public const int EngineConstants.HERO_STAT_MOST_POWERFUL_RATING_PARTY = 2029;
     //moved public const int EngineConstants.HERO_STAT_ITEMS_CRAFTED = 2031;

     /*
     //moved public const int EngineConstants.HERO_STAT_CURRENT_TIME_COUNTER = 1018;
     //moved public const int EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_COMBAT = 1019;
     //moved public const int EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_CONVERSATION = 1020;
     //moved public const int EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_EXPLORE = 1021;
     */

     // Stats values
     //moved public const float STATS_WALKED_VELOCITY_MAX_CAP = 150.0f;
     //moved public const float STATS_TRACKING_TRACK_ACCURACY_COMBAT  = 3.0f;      // In combat, track position updates greater than 3 meters difference
     //moved public const float STATS_TRACKING_TRACK_ACCURACY_DEFAULT = 1.5f;      // Out of combat, track greater than 1.5f meters

     // Stats Appearance constants. This is what is returned by the function GetAppearanceType()
     //moved public const int STATS_APP_INVALID = 0;
     //moved public const int STATS_APP_BLANK_MODEL = 1;
     //moved public const int STATS_APP_ELF = 2;
     //moved public const int STATS_APP_DWARF = 3;
     //moved public const int STATS_APP_DRAGON_NORMAL = 4;
     //moved public const int STATS_APP_GOLEM_SHALE = 5;
     //moved public const int STATS_APP_GOLEM_STONE = 6;
     //moved public const int STATS_APP_GOLEM_STEEL = 7;
     //moved public const int STATS_APP_BEAR_GREAT = 8;
     //moved public const int STATS_APP_BEAR_BLACK = 9;
     //moved public const int STATS_APP_BROODMOTHER = 10;
     //moved public const int STATS_APP_BRONTO = 11;
     //moved public const int STATS_APP_ARCANE_HORROR = 12;
     //moved public const int STATS_APP_QUNARI = 13;
     //moved public const int STATS_APP_WISP = 14;
     //moved public const int STATS_APP_HUMAN = 15;
     //moved public const int STATS_APP_HURLOCK_NORMAL = 16;
     //moved public const int STATS_APP_HURLOCK_ALPHA = 17;
     //moved public const int STATS_APP_HURLOCK_EMISSARY = 18;

     //moved public const int STATS_APP_NUG = 20;

     //moved public const int STATS_APP_WEREWOLF_A = 22;
     //moved public const int STATS_APP_SHRIEK_A = 23;
     //moved public const int STATS_APP_SUCCUBUS = 24;
     //moved public const int STATS_APP_ABOMINATION = 25;
     //moved public const int STATS_APP_REVENANT_A = 26;
     //moved public const int STATS_APP_RAGE_DEMON = 27;
     //moved public const int STATS_APP_CORPSE_D = 28;
     //moved public const int STATS_APP_CORPSE_E = 29;
     //moved public const int STATS_APP_CORPSE_S = 30;
     //moved public const int STATS_APP_SHADE = 31;
     //moved public const int STATS_APP_ASH_WRAITH = 32;
     //moved public const int STATS_APP_DEEPSTALKER = 33;
     //moved public const int STATS_APP_DOG_MABARI_ = 34;
     //moved public const int STATS_APP_DOG_PARTY_MEMBER = 35;
     //moved public const int STATS_APP_SQUIREL = 36;
     //moved public const int STATS_APP_RAT_LARGE = 37;
     //moved public const int STATS_APP_HUMAN_BOY = 38;
     //moved public const int STATS_APP_HUMAN_SERVANT_AMBIENT = 39;
     //moved public const int STATS_APP_HUMAN_LIBRARY_AMBIENT = 40;
     //moved public const int STATS_APP_RAT_SMALL = 41;
     //moved public const int STATS_APP_HUMAN_GUARD_AMBIENT = 42;
     //moved public const int STATS_APP_HUMAN_NOBLE_AMBIENT = 43;
     //moved public const int STATS_APP_HUMAN_MALE_FAT = 44;
     //moved public const int STATS_APP_HUMAN_FEMALE_FAT = 45;
     //moved public const int STATS_APP_NPC_DUNCAN = 46;
     //moved public const int STATS_APP_OGRE_A = 47;

     //moved public const int STATS_APP_WOLF = 49;
     //moved public const int STATS_APP_GENLOCK_NORMAL = 50;
     //moved public const int STATS_APP_GENLOCK_ALPHA = 51;
     //moved public const int STATS_APP_GENLOCK_EMISSARY = 52;
     //moved public const int STATS_APP_WITHERFANG = 53;
     //moved public const int STATS_APP_AMBIENT_GOAT = 54;
     //moved public const int STATS_APP_AMBIENT_MUTT = 55;

     //moved public const int STATS_APP_SPIDER_CORRUPTED = 57;
     //moved public const int STATS_APP_SPIDER_GIANT = 58;
     //moved public const int STATS_APP_SPIDER_POISONOUS = 59;
     //moved public const int STATS_APP_HUMAN_DYING_AMBIENT = 60;
     //moved public const int STATS_APP_HUMAN_PRELUDE_WIZARD = 61;

     //moved public const int STATS_APP_CAT = 63;
     //moved public const int STATS_APP_DRAGONLING = 64;
     //moved public const int STATS_APP_WILD_SYLVAN = 65;
     //moved public const int STATS_APP_DRAGON_HIGH = 66;
     //moved public const int STATS_APP_HUMAN_GIRL = 67;
     //moved public const int STATS_APP_BEAR_BEARESKAN = 68;
     //moved public const int STATS_APP_SKELETON_A = 69;
     //moved public const int STATS_APP_SKELETON_F = 70;
     //moved public const int STATS_APP_SKELETON_S = 71;
     //moved public const int STATS_APP_PRIDE_DEMON = 72;
     //moved public const int STATS_APP_BROODMOTHER_TENTACLE = 73;
     //moved public const int STATS_APP_WOLF_BLIGHT = 74;
     //moved public const int STATS_APP_LADY_OF_THE_FOREST = 75;
     //moved public const int STATS_APP_PIG = 76;
     //moved public const int STATS_APP_DEER = 77;
     //moved public const int STATS_APP_OX = 78;
     //moved public const int STATS_APP_RAM = 79;
     //moved public const int STATS_APP_DRAGON_DRAKE = 80;
     //moved public const int STATS_APP_SPIRIT_APPARATUS_HEAD = 81;
     //moved public const int STATS_APP_ARCHDEMON_WOUNDED_ = 82;

     //moved public const int STATS_APP_RAVEN = 84;
     //moved public const int STATS_APP_HALLA = 85;
     //moved public const int STATS_APP_CHICKEN = 86;
     //moved public const int STATS_APP_OWL = 87;
     //moved public const int STATS_APP_GRAND_OAK = 88;
     //moved public const int STATS_APP_ARCHDEMON = 89;

     //
     //moved public const string STATS_LOC_MOST_POWERFUL_SLAIN = "stats_loc_most_powerful_slain";
     //moved public const string STATS_LOC_MOST_POWERFUL_SLAIN_PARTY = "stats_loc_most_powerful_slain_party";

     // Events
     ////moved public const int EVENT_CUSTOM_STATS_REGEN = 10701;

     // Properties
     ////moved public const int EngineConstants.MAX_REGEN_TICKS = 5;

     //moved public const string EngineConstants.HERO_STAT_VERSION = ">> STATS V0.017 << ";

     //follower tags
     //moved public const string EngineConstants.FOLLOWER_ALISTAIR = "gen00fl_alistair";
     //moved public const string EngineConstants.FOLLOWER_DOG = "gen00fl_dog";
     //moved public const string EngineConstants.FOLLOWER_LELIANA = "gen00fl_leliana";
     //moved public const string EngineConstants.FOLLOWER_LOGHAIN = "gen00fl_loghain";
     //moved public const string EngineConstants.FOLLOWER_MORRIGAN = "gen00fl_morrigan";
     //moved public const string EngineConstants.FOLLOWER_OGHREN = "gen00fl_oghren";
     //moved public const string EngineConstants.FOLLOWER_SHALE = "gen00fl_shale";
     //moved public const string EngineConstants.FOLLOWER_STEN = "gen00fl_sten";
     //moved public const string EngineConstants.FOLLOWER_WYNNE = "gen00fl_wynne";
     //moved public const string EngineConstants.FOLLOWER_ZEVRAN = "gen00fl_zevran";

     // Print a log in the stats channel
     public void STATS_LogTrace(string sPrint)
     {
          LogTrace(EngineConstants.LOG_CHANNEL_SYSTEMS, EngineConstants.HERO_STAT_VERSION + sPrint);
     }

     // Sets the property to the total time in seconds the player has played this game.
     public void STATS_SetGameTimePlayed()
     {
          GameObject oHero = GetHero();
          int iSecondsPlayed = GetPlayTime();
          float fSecondsPlayed = IntToFloat(iSecondsPlayed);
          SetCreatureProperty(oHero, EngineConstants.HERO_STAT_TOTAL_TIME_PLAYED, fSecondsPlayed);
     }

     ////////////////////////////////////////////////////////////////////////////////
     // DAMAGE
     ////////////////////////////////////////////////////////////////////////////////

     // Handles the damager's INDIVIDUAL percent damage contribution
     // only tracks the 10 followers and the PC
     public void STATS_HandlePercentDamageDealt(GameObject oDamager)
     {
          GameObject oHero = GetHero();
          string sDamager = GetTag(oDamager);

          if (oDamager == oHero ||
              sDamager == EngineConstants.FOLLOWER_ALISTAIR ||
              sDamager == EngineConstants.FOLLOWER_DOG ||
              sDamager == EngineConstants.FOLLOWER_LELIANA ||
              sDamager == EngineConstants.FOLLOWER_LOGHAIN ||
              sDamager == EngineConstants.FOLLOWER_MORRIGAN ||
              sDamager == EngineConstants.FOLLOWER_OGHREN ||
              sDamager == EngineConstants.FOLLOWER_SHALE ||
              sDamager == EngineConstants.FOLLOWER_STEN ||
              sDamager == EngineConstants.FOLLOWER_WYNNE ||
              sDamager == EngineConstants.FOLLOWER_ZEVRAN)
          {
               float fPartyDamage = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_PARTY_DAMAGE_DEALT);
               float fIndividualDamage = GetCreatureProperty(oDamager, EngineConstants.HERO_STAT_TOTAL_DAMAGE_DEALT);
               if (fPartyDamage > 0.0f)
               {
                    float fDamagePercent = (fIndividualDamage / fPartyDamage) * 100.0f;
                    SetCreatureProperty(oDamager, EngineConstants.HERO_STAT_TOTAL_DAMAGE_PERCENT, fDamagePercent);
                    //STATS_LogTrace(ToString(oDamager) +" deals " +ToString(fDamagePercent) +" % of party's total damage");
               }
          }
     }

     // Handles TOTAL damage dealt by the party
     // Handed off to determine the damager's INDIVIDUAL percent damage contribution
     // only tracks the 10 followers and the PC
     public void STATS_HandlePartyDamageDealt(float fDamage, GameObject oDamager)
     {
          GameObject oHero = GetHero();
          string sDamager = GetTag(oDamager);

          if (oDamager == oHero ||
              sDamager == EngineConstants.FOLLOWER_ALISTAIR ||
              sDamager == EngineConstants.FOLLOWER_DOG ||
              sDamager == EngineConstants.FOLLOWER_LELIANA ||
              sDamager == EngineConstants.FOLLOWER_LOGHAIN ||
              sDamager == EngineConstants.FOLLOWER_MORRIGAN ||
              sDamager == EngineConstants.FOLLOWER_OGHREN ||
              sDamager == EngineConstants.FOLLOWER_SHALE ||
              sDamager == EngineConstants.FOLLOWER_STEN ||
              sDamager == EngineConstants.FOLLOWER_WYNNE ||
              sDamager == EngineConstants.FOLLOWER_ZEVRAN)
          {
               float fOldParty = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_PARTY_DAMAGE_DEALT);
               float fNewParty = fOldParty + fDamage;
               SetCreatureProperty(oHero, EngineConstants.HERO_STAT_PARTY_DAMAGE_DEALT, fNewParty);
               //DisplayFloatyMessage(oDamager, sDamager, FLOATY_MESSAGE, 16777215, 3.0f);
               //STATS_LogTrace("TOTAL PARTY DAMAGE = " +ToString(fNewParty));
          }
     }

     // Handles FRIENDLY FIRE damage dealt by the hero; all damage is tallied
     public void STATS_HandleFriendlyFireDealt(GameObject oDamager, float fDamage)
     {
          float fOld = GetCreatureProperty(oDamager, EngineConstants.HERO_STAT_FRIENDLY_FIRE_DEALT);
          float fNew = fOld + fDamage;
          SetCreatureProperty(oDamager, EngineConstants.HERO_STAT_FRIENDLY_FIRE_DEALT, fNew);
          //STATS_LogTrace(ToString(oDamager) + " | total friendly fire = " +ToString(fNew));
     }

     // Handles TOTAL damage dealt by the hero; all damage is tallied
     // Friendly fire damage is tallied separately
     public void STATS_HandleTotalDamageDealt(GameObject oDamager, GameObject oTarget, float fDamage)
     {
          if (IsFollower(oTarget) != EngineConstants.FALSE)
          {
               // Tally "friendly fire" stat instead
               STATS_HandleFriendlyFireDealt(oDamager, fDamage);
          }
          else
          {
               float fOld = GetCreatureProperty(oDamager, EngineConstants.HERO_STAT_TOTAL_DAMAGE_DEALT);
               float fNew = fOld + fDamage;
               SetCreatureProperty(oDamager, EngineConstants.HERO_STAT_TOTAL_DAMAGE_DEALT, fNew);
               STATS_LogTrace(ToString(oDamager) + " | total damage dealt = " + ToString(fNew));
          }
     }

     // Handles HIGHEST damage dealt by the hero; only the highest damage is saved
     // Handed off to tally TOTAL damage dealt by the individual
     // Handed off to tally TOTAL damage dealt by the party
     // Handed off to determine the individual damager's percent damage contribution
     public void STATS_HandleDamageDealt(GameObject oDamager, GameObject oTarget, float fDamage)
     {
          // Damage dealt to placeables doesn't count
          if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               // Only track the main hero's damage
               if (IsFollower(oDamager) != EngineConstants.FALSE)
               {
                    float fOld = GetCreatureProperty(oDamager, EngineConstants.HERO_STAT_HIGHEST_DAMAGE_DEALT);
                    if (fDamage > fOld)
                    {
                         SetCreatureProperty(oDamager, EngineConstants.HERO_STAT_HIGHEST_DAMAGE_DEALT, fDamage);
                         float fNew = GetCreatureProperty(oDamager, EngineConstants.HERO_STAT_HIGHEST_DAMAGE_DEALT);
                         //STATS_LogTrace("!!NEW RECORD!! " +FloatToString(fDamage, 4, 1) + " DAMAGE INFLICTED!");
                         //STATS_LogTrace(FloatToString(fNew, 4, 1) +" is the highest damage ever dealt.");
                    }
                    else
                    {
                         //STATS_LogTrace(FloatToString(fDamage, 4, 1) +" damage inflicted.");
                         //STATS_LogTrace(FloatToString(fOld, 4, 1) +" is the highest damage ever dealt.");
                    }
                    // Tally total damage dealt
                    STATS_HandleTotalDamageDealt(oDamager, oTarget, fDamage);
                    // Tally TOTAL party damage
                    STATS_HandlePartyDamageDealt(fDamage, oDamager);
               }
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     // KILLS
     ////////////////////////////////////////////////////////////////////////////////

     // Handles number of kills made by an individual
     public void STATS_HandleIndividualKills(GameObject oKiller, GameObject oTarget)
     {
          float fOld = GetCreatureProperty(oKiller, EngineConstants.HERO_STAT_KILLS);
          float fNew = fOld + 1.0f;
          SetCreatureProperty(oKiller, EngineConstants.HERO_STAT_KILLS, fNew);
     }

     // Handles number of kills made by the party
     public void STATS_HandlePartyKills(GameObject oKiller, GameObject oTarget)
     {
          GameObject oHero = GetHero();
          // Only increment this stat when the party killed the target
          if (IsFollower(oKiller) != EngineConstants.FALSE)
          {
               // Only creatures count towards this stat
               if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
               {
                    float fOld = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_PARTY_KILLS);
                    float fNew = fOld + 1.0f;
                    SetCreatureProperty(oHero, EngineConstants.HERO_STAT_PARTY_KILLS, fNew);
                    //STATS_LogTrace("Number of kills was " +FloatToString(fOld, 4, 1));
                    //STATS_LogTrace("Number of kills is now " +FloatToString(fNew, 4, 1));
                    // Hand off to individual kills
                    STATS_HandleIndividualKills(oKiller, oTarget);
               }
          }
     }

     ////////////////////////////////////////////////////////////////////////////////
     // OTHER
     ////////////////////////////////////////////////////////////////////////////////

     // Handles the hero's hit rate %
     public void STATS_HandleHitRate(GameObject oAttacker, int nAttackResult)
     {
          if (IsFollower(oAttacker) != EngineConstants.FALSE)
          {
               float fNewAttempt;
               float fNewHit = 0.0f;
               float fOldAttempt = GetCreatureProperty(oAttacker, EngineConstants.HERO_STAT_ATTACK_COUNT);
               float fOldHit = GetCreatureProperty(oAttacker, EngineConstants.HERO_STAT_HIT_COUNT);
               string sHit;

               // By virtue of being here, a party member has attacked; increment attack attempt counter
               fNewAttempt = fOldAttempt + 1.0f;
               SetCreatureProperty(oAttacker, EngineConstants.HERO_STAT_ATTACK_COUNT, fNewAttempt);
               // A hit isn't necessarily EngineConstants.COMBAT_RESULT_HIT (crits, deathblows, backstabs, etc)
               if (nAttackResult != EngineConstants.COMBAT_RESULT_MISS && nAttackResult != EngineConstants.COMBAT_RESULT_BLOCKED)
               {
                    fNewHit = fOldHit + 1.0f;
                    SetCreatureProperty(oAttacker, EngineConstants.HERO_STAT_HIT_COUNT, fNewHit);
                    sHit = " | attack HIT! Hit rate = ";
               }
               if (nAttackResult == EngineConstants.COMBAT_RESULT_MISS || nAttackResult == EngineConstants.COMBAT_RESULT_BLOCKED)
               {
                    // We missed, but we still need a "new" hit count
                    fNewHit = fOldHit;
                    sHit = " | attack missed. Hit rate = ";
               }
               // Finally, don't let attempt & hit counts climb forever, but keep amounts proportionate
               if (fNewHit >= 1000.0f && fNewAttempt >= 1000.0f)
               {
                    fNewHit = fNewHit / 2;
                    fNewAttempt = fNewAttempt / 2;
               }
               // Yield hit rate % stat
               float fHitRate = (fNewHit / fNewAttempt) * 100.0f;
               SetCreatureProperty(oAttacker, EngineConstants.HERO_STAT_HIT_RATE, fHitRate);
               //STATS_LogTrace(ToString(oAttacker) +sHit +FloatToString(fHitRate, 3, 2) + " %");
          }
     }

     // Return the "power rating" of the passed creature based on
     // its level, rank (boss, grunt, etc) and appearance type
     // The higher the power rating, the more formidable the creature.
     public float STATS_GetCreaturePowerRating(GameObject oCreature)
     {
          int nAppearance = GetAppearanceType(oCreature);
          float fAppearanceRating = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AppearanceRating", nAppearance);
          float fLevel = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_LEVEL, EngineConstants.PROPERTY_VALUE_TOTAL);
          int nRank = GetCreatureRank(oCreature);
#if DEBUG
          STATS_LogTrace("rank: " + IntToString(nRank));
#endif
          float fRankRating = GetM2DAFloat(EngineConstants.TABLE_CREATURERANKS, "RankRating", nRank);

          float fPowerRating = (fAppearanceRating + fLevel) * fRankRating;
#if DEBUG
          STATS_LogTrace("appearance rating: " + FloatToString(fAppearanceRating) + ", level: " + FloatToString(fLevel) + ", rank rating: " + FloatToString(fRankRating));
#endif

          return fPowerRating;
     }

     // //Tracks and saves the most powerful creature slain, as determined by its power rating
     public void STATS_TrackMostPowerfulSlain(GameObject oKiller, GameObject oCreature)
     {
          // Only track kills made by the party
          if (IsFollower(oKiller) == EngineConstants.FALSE) return;

          // Don't track friendly fire kills
          if (IsFollower(oCreature) != EngineConstants.FALSE) return;

          GameObject oHero = GetHero();

          // Convert our new power rating into a float for ease of comparison
          float fNewRating = STATS_GetCreaturePowerRating(oCreature);

          // Get the killed creature in float form for ease of setting as a property
          float fCreature = StringToFloat(ObjectToString(oCreature));
          int iCreatureNameResRef = GetNameStrref(oCreature);
          float fCreatureNameResRef = IntToFloat(iCreatureNameResRef);

#if DEBUG
          STATS_LogTrace(ToString(oCreature) + " killed. Float = " + ToString(fCreature) + ". Rating = " + ToString(fNewRating));
#endif

          // First determine if this is the "most powerful" for the individual
          float fOldIndividual = GetCreatureProperty(oKiller, EngineConstants.HERO_STAT_MOST_POWERFUL_RATING);
#if DEBUG
          STATS_LogTrace(ToString(oKiller) + "'s old individual record is a rating of " + ToString(fOldIndividual));
#endif
          //DisplayFloatyMessage(GetHero(), "New Rating: " + IntToString(FloatToInt(fNewRating)));
          if (fNewRating > fOldIndividual || fOldIndividual == 0.0f)
          {
               // The newly killed creature is "more powerful" than the old one; save new stat to individual
               SetCreatureProperty(oKiller, EngineConstants.HERO_STAT_MOST_POWERFUL_SLAIN, fCreatureNameResRef);
               SetCreatureProperty(oKiller, EngineConstants.HERO_STAT_MOST_POWERFUL_RATING, fNewRating);

#if DEBUG
               STATS_LogTrace("Creature of type " + ToString(fCreature) + " is the most powerful slain by " + ToString(oKiller));
#endif
          }

          // Then determine if this is the "most powerful" for the party
          float fOldParty = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_MOST_POWERFUL_RATING_PARTY);
#if DEBUG
          STATS_LogTrace("Old party record is a rating of " + ToString(fOldParty));
#endif
          if (fNewRating > fOldParty || fOldParty == 0.0f)
          {
               // The newly killed creature is "more powerful" than the old one; save new stat to party
               SetCreatureProperty(oHero, EngineConstants.HERO_STAT_MOST_POWERFUL_SLAIN_BY_PARTY, fCreatureNameResRef);
               SetCreatureProperty(oHero, EngineConstants.HERO_STAT_MOST_POWERFUL_RATING_PARTY, fNewRating);

#if DEBUG
               STATS_LogTrace("Creature of type " + ToString(fCreature) + " is the most powerful slain by the party");
#endif
          }
     }

     // Returns the hero stat of the passed creature: demons, fantastical, constructs, etc.
     public int STATS_GetCreatureGroupType(GameObject oCreature)
     {
          int nAppearance = GetAppearanceType(oCreature);
          int nGroupType = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "GroupType", nAppearance);

          return nGroupType;
     }

     // Increment number of kills based on the target's group type
     public void STATS_CountKillsByGroupType(GameObject oKiller, GameObject oTarget)
     {
          GameObject oHero = GetHero();
          if (IsFollower(oKiller) != EngineConstants.FALSE)
          {
               int nHeroStat = STATS_GetCreatureGroupType(oTarget);

               //EV152534 - Cannot kill harmless creatures, don't include them in stats.
               if (nHeroStat != EngineConstants.HERO_STAT_GROUPTYPE_HARMLESS)
               {
                    float fOld = GetCreatureProperty(oHero, nHeroStat);
                    float fNew = fOld + 1;
                    SetCreatureProperty(oHero, nHeroStat, fNew);
#if DEBUG
                    STATS_LogTrace("The party has killed " + ToString(fNew) + " creatures of group " + ToString(nHeroStat));
#endif
               }
          }
     }

     // Increments the number of injuries that an individual has incurred
     public void STATS_HandleInjuries(GameObject oCharacter)
     {
          if (IsFollower(oCharacter) != EngineConstants.FALSE)
          {
               float fOld = GetCreatureProperty(oCharacter, EngineConstants.HERO_STAT_INJURIES);
               float fNew = fOld + 1;
               SetCreatureProperty(oCharacter, EngineConstants.HERO_STAT_INJURIES, fNew);
               STATS_LogTrace(ToString(oCharacter) + " incurred " + ToString(fNew) + " injuries.");
          }
     }

     // Increments the number of dialogues started (not unique)
     public void STATS_TrackStartedDialogues(GameObject oListener)
     {
          if (IsHero(oListener) != EngineConstants.FALSE)
          {
               float fOld = GetCreatureProperty(oListener, EngineConstants.HERO_STAT_CONVERSATIONS);
               float fNew = fOld + 1;
               SetCreatureProperty(oListener, EngineConstants.HERO_STAT_CONVERSATIONS, fNew);
#if DEBUG
               STATS_LogTrace(ToString(fNew) + " dialogues initiated.");
#endif
          }
     }

     // //Tracks the distance the player travels in miles
     public void STATS_TrackWalkedDistance()
     {
          // ----------------------------------------------------------------
          // Player distance walked tracking system position updates.
          // ----------------------------------------------------------------
          float fOldX = GetCreatureProperty(gameObject, EngineConstants.HERO_STAT_TRACKING_POS_X);
          float fOldY = GetCreatureProperty(gameObject, EngineConstants.HERO_STAT_TRACKING_POS_Y);
          Vector3 vPos = GetPosition(gameObject);
          Vector3 lA = GetLocation(gameObject);
          Vector3 lB = Location(GetArea(gameObject), Vector(fOldX, fOldY, vPos.z), 0.0f);

          if ((fOldX == 0.0f) && (fOldY == 0.0f))
          {
               // Previous value was not valid (i.e. == 0.0f), so update the stored Vector3 of the player
               SetCreatureProperty(gameObject, EngineConstants.HERO_STAT_TRACKING_POS_X, vPos.x);
               SetCreatureProperty(gameObject, EngineConstants.HERO_STAT_TRACKING_POS_Y, vPos.y);
          }
          else
          {
               lA = GetLocation(gameObject);
               lB = Location(GetArea(gameObject), Vector(fOldX, fOldY, vPos.z), 0.0f);

               // In combat, we only register significant movements
               float fThreshHold = (GetGameMode() == EngineConstants.GM_COMBAT ? EngineConstants.STATS_TRACKING_TRACK_ACCURACY_COMBAT : EngineConstants.STATS_TRACKING_TRACK_ACCURACY_DEFAULT);

               // -----------------------------------------------------------------
               // basic 2d distance check. Check if the distance is relevant
               // and also do a sanity check to make sure something strange did
               // not occur (for example, due to an area change)
               // -----------------------------------------------------------------
               float fDist = GetDistanceBetweenLocations(lA, lB);
               if ((fDist > fThreshHold) && (fDist < EngineConstants.STATS_WALKED_VELOCITY_MAX_CAP))
               {
                    // Perform distance comparisons in meters ONLY
                    float fMetersWalked = GetCreatureProperty(gameObject, EngineConstants.HERO_STAT_WALKED_DISTANCE_METERS);
                    float fTotalMetersWalked = fMetersWalked + fDist;
                    SetCreatureProperty(gameObject, EngineConstants.HERO_STAT_WALKED_DISTANCE_METERS, fTotalMetersWalked);
                    // Current amount is in meters; convert to and save stat as miles
                    float fTotalMilesWalked = fTotalMetersWalked / 1609.344f;//meters to miles
                    SetCreatureProperty(gameObject, EngineConstants.HERO_STAT_WALKED_DISTANCE_MILES, fTotalMilesWalked);
                    /*
                    #if DEBUG
                    STATS_LogTrace(ToString(fTotalMilesWalked) +" miles walked.");
                    #endif
                    */
               }
               // update the stored Vector3 of the player
               SetCreatureProperty(gameObject, EngineConstants.HERO_STAT_TRACKING_POS_X, vPos.x);
               SetCreatureProperty(gameObject, EngineConstants.HERO_STAT_TRACKING_POS_Y, vPos.y);
          }
     }

     // Runs whenever the party's money changes; tracks the amount; saves the highest amount ever
     public void STATS_HandleMaxMoney(int nMoney)
     {
          //Money in copper
          GameObject oHero = GetHero();
          float fOld = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_MOST_MONEY_EVER);
          float fMoney = IntToFloat(nMoney);

          if (fMoney > fOld)
          {
               SetCreatureProperty(oHero, EngineConstants.HERO_STAT_MOST_MONEY_EVER, fMoney);
#if DEBUG
               STATS_LogTrace("Most gold ever = " + ToString(fMoney));
#endif
          }
     }

     // Runs whenever the party's money changes; if the change is negative, "money spent" increases
     public void STATS_TrackMoneySpent(int nMoney)
     {
          //money in copper
          GameObject oHero = GetHero();
          float fMoney = IntToFloat(nMoney);
          float fPrevious = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_PREVIOUS_MONEY);

          if (fMoney < fPrevious)
          {
               // The player spent money
               float fDiff = fPrevious - fMoney;
               float fOld = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_MONEY_SPENT);
               float fNew = fOld + fDiff;
               SetCreatureProperty(oHero, EngineConstants.HERO_STAT_MONEY_SPENT, fNew);
#if DEBUG
               STATS_LogTrace("Total gold spent = " + ToString(fNew));
#endif
          }
          SetCreatureProperty(oHero, EngineConstants.HERO_STAT_PREVIOUS_MONEY, fMoney);
     }

     // Runs whenever a new codex entry is added or updated; tracks the amount
     public void STATS_TrackCodexEntries()
     {
          GameObject oHero = GetHero();
          float fOld = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_CODEX_ENTRIES);
          float fNew = fOld + 1;
          SetCreatureProperty(oHero, EngineConstants.HERO_STAT_CODEX_ENTRIES, fNew);
#if DEBUG
          STATS_LogTrace("Codex entries unlocked = " + ToString(fNew));
#endif
     }

     // Runs whenever an item is crafted, increments amount
     public void STATS_TrackItemsCrafted()
     {
          GameObject oHero = GetHero();
          float fOld = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_ITEMS_CRAFTED);
          float fNew = fOld + 1;
          SetCreatureProperty(oHero, EngineConstants.HERO_STAT_ITEMS_CRAFTED, fNew);
#if DEBUG
          STATS_LogTrace("Items Crafted = " + ToString(fNew));
#endif
     }

     // //Tracks number of failed/succeeded stealing attempts
     public void STATS_TrackStealing(int bSuccess)
     {
          GameObject oHero = GetHero();
          if (bSuccess != EngineConstants.FALSE)
          {
               // Increment number of successes
               float fOld = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_STEALING_SUCCESS);
               float fNew = fOld + 1;
               SetCreatureProperty(oHero, EngineConstants.HERO_STAT_STEALING_SUCCESS, fNew);
#if DEBUG
               STATS_LogTrace("Number of stealing successes = " + ToString(fNew));
#endif
          }
          else
          {
               // Increment number of failures
               float fOld = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_STEALING_FAILURE);
               float fNew = fOld + 1;
               SetCreatureProperty(oHero, EngineConstants.HERO_STAT_STEALING_FAILURE, fNew);
#if DEBUG
               STATS_LogTrace("Number of stealing failures = " + ToString(fNew));
#endif
          }
     }

     // Determines the percent of the game world explored (entered areas)
     public void STATS_TrackExploredAreas(int nTable, int nAreaID)
     {
          GameObject oHero = GetHero();
          float fValue = GetM2DAFloat(nTable, "AreaWeight", nAreaID);
          // Update the stat
          float fOld = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_AREAS_EXPLORED);
          float fNew = fOld + fValue;
          SetCreatureProperty(oHero, EngineConstants.HERO_STAT_AREAS_EXPLORED, fNew);
#if DEBUG
          STATS_LogTrace(ToString(fValue) + "% added for exploring area #" + ToString(nAreaID));
          STATS_LogTrace("The player has explored " + ToString(fNew) + "% of the game world.");
#endif
     }

     /*
     public void ACH_ResetTimeSpentCurrentCounter();
     public void ACH_ResetTimeSpentCurrentCounter()
     {

         GameObject oHero = GetHero();
         float fCurrentTime = IntToFloat(GetTime());
         SetCreatureProperty(oHero, EngineConstants.HERO_STAT_CURRENT_TIME_COUNTER, fCurrentTime);

     }

     // Based on teh previous game mode, update the proper time counter
     public void ACH_UpdateTimeSpent(int nOldGameMode);
     public void ACH_UpdateTimeSpent(int nOldGameMode)
     {
         GameObject oHero = GetHero();
         float fCurrentTime = IntToFloat(GetTime());
         float fSpentTime =  fCurrentTime - GetCreatureProperty(oHero, EngineConstants.HERO_STAT_CURRENT_TIME_COUNTER);
         SetCreatureProperty(oHero, EngineConstants.HERO_STAT_CURRENT_TIME_COUNTER, fCurrentTime);

         switch (nOldGameMode)
         {
                 case EngineConstants.GM_COMBAT:
                 {
                     float fCombat = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_COMBAT) + fSpentTime ;
                     SetCreatureProperty(oHero, EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_COMBAT, fCombat);
                 }
                 break;

                 case EngineConstants.GM_CONVERSATION:
                 {
                     float fDialogue = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_CONVERSATION) + fSpentTime ;
                     SetCreatureProperty(oHero, EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_CONVERSATION, fDialogue);
                 }
                 break;

                 case EngineConstants.GM_EXPLORE:
                 {
                     float fExplore = GetCreatureProperty(oHero, EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_EXPLORE) + fSpentTime ;
                     SetCreatureProperty(oHero, EngineConstants.HERO_STAT_TOTAL_TIME_SPENT_IN_EXPLORE, fExplore);
                 }
                 break;

         }

     }
     */
}