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
using System.Linq;

public partial class Engine
{
     ////////////////////////////////////////////////////////////////////////////////
     //  sys_traps_h
     //  Copyright � 2007 BioWare Corp.
     ////////////////////////////////////////////////////////////////////////////////
     /*
         Trap functions.
     */
     ////////////////////////////////////////////////////////////////////////////////

     //#include"log_h"
     //#include"utility_h"
     //#include"events_h"
     //#include"sys_autoscale_h"      // auto-scaling functions (used by summoning trap)
     //#include"sys_rewards_h"
     //#include"core_difficulty_h"    // damage-scaling functions

     //#include"abi_templates"

     //#include"achievement_core_h"

     // -----------------------------------------------------------------------------
     // Trap types (from traps.xls)
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.TRAP_INVALID                  = 0;
     ////moved public const int EngineConstants.TRAP_SWINGING_LOG           = 1;
     //moved public const int EngineConstants.TRAP_EXPLOSION                = 2;
     //moved public const int EngineConstants.TRAP_NATURAL_WEB              = 200;
     //moved public const int EngineConstants.TRAP_FIREBALL                 = 205;
     //moved public const int EngineConstants.SPELL_GLYPH_PARALYSIS         = 301;
     //moved public const int EngineConstants.TRAP_GREASE                   = 206;
     //moved public const int EngineConstants.TRAP_GAS_TOXIC                = 207;
     //moved public const int EngineConstants.TRAP_LEGHOLD_STEEL            = 400;
     //moved public const int EngineConstants.TRAP_LEGHOLD_WOOD             = 401;
     //moved public const int EngineConstants.TRAP_PROJECTILE_BIGBOLT       = 402;
     //moved public const int EngineConstants.TRAP_PROJECTILE_ARROW         = 403;
     //moved public const int EngineConstants.TRAP_PROJECTILE_ARROWS        = 404;
     //moved public const int EngineConstants.TRAP_PROJECTILE_ARROW_ICE     = 405;
     //moved public const int EngineConstants.TRAP_PROJECTILE_ARROW_FIRE    = 406;
     //moved public const int EngineConstants.TRAP_PROJECTILE_ARROW_POISON  = 407;
     //moved public const int EngineConstants.TRAP_PROJECTILE_FIRE          = 408;
     //moved public const int EngineConstants.TRAP_PROJECTILE_ICE           = 409;
     //moved public const int EngineConstants.TRAP_PROJECTILE_LIGHTNING     = 410;
     //moved public const int EngineConstants.TRAP_PROJECTILE_WEB           = 411;
     //moved public const int EngineConstants.TRAP_SCYTHING_BLADE           = 412;
     //moved public const int EngineConstants.TRAP_PIERCING_BLADE           = 413;
     //moved public const int EngineConstants.TRAP_EXPLODING_BARREL         = 418;
     //moved public const int EngineConstants.TRAP_FIREBALL_EFFECT          = 441;

     //moved public const int EngineConstants.TRAP_AOE_FIRE                 = 417;
     //moved public const int EngineConstants.TRAP_AOE_GAS                  = 420;

     // -----------------------------------------------------------------------------
     // Appearance constants (from placeables.xls)
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.PLC_PIERCING_BLADE            = 35;
     //moved public const int EngineConstants.PLC_SCYTHING_BLADE            = 38;
     ////moved public const int EngineConstants.PLC_SWINGING_LOG            = 40;
     //moved public const int EngineConstants.PLC_INVISIBLE_TRAP_TRIGGER    = 474;

     // -----------------------------------------------------------------------------
     // Trap xEffect constants (from traps.xls)
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.TRAP_EFFECT_INVALID           =  0;
     //moved public const int EngineConstants.TRAP_EFFECT_DAMAGE            =  1;
     //moved public const int EngineConstants.TRAP_EFFECT_DAMAGE_SCALED     =  2;
     //moved public const int EngineConstants.TRAP_EFFECT_DOT               =  3;
     //moved public const int EngineConstants.TRAP_EFFECT_AOE               =  4;
     //moved public const int EngineConstants.TRAP_EFFECT_KNOCKDOWN         =  5;
     //moved public const int EngineConstants.TRAP_EFFECT_SUMMON            =  6;
     //moved public const int EngineConstants.TRAP_EFFECT_DAZE              =  7;
     //moved public const int EngineConstants.TRAP_EFFECT_DEATH             =  8;
     //moved public const int EngineConstants.TRAP_EFFECT_DISPEL            =  9;
     //moved public const int EngineConstants.TRAP_EFFECT_PARALYZE          = 10;
     //moved public const int EngineConstants.TRAP_EFFECT_ROOT              = 11;
     //moved public const int EngineConstants.TRAP_EFFECT_STUN              = 12;
     //moved public const int EngineConstants.TRAP_EFFECT_MODIFY_MANA       = 16;
     //moved public const int EngineConstants.TRAP_EFFECT_MODIFY_CRIT       = 17;
     //moved public const int EngineConstants.TRAP_EFFECT_SCREENSHAKE       = 20;
     //moved public const int EngineConstants.TRAP_EFFECT_VFX               = 21;
     //moved public const int EngineConstants.TRAP_EFFECT_SFX               = 22;

     // -----------------------------------------------------------------------------
     // VFX target constants for a trap
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.TRAP_VFX_APPLY_TO_LOCATION        = 0;
     //moved public const int EngineConstants.TRAP_VFX_APPLY_TO_TARGET          = 1;
     //moved public const int EngineConstants.TRAP_VFX_APPLY_TO_TRAP            = 2;
     //moved public const int EngineConstants.TRAP_VFX_APPLY_TO_SIGNAL_TARGET   = 3;

     // -----------------------------------------------------------------------------
     // AOE Vector3 constants for a trap
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.TRAP_AOE_LOCATION_TRAP            = 0;
     //moved public const int EngineConstants.TRAP_AOE_LOCATION_SIGNAL_TARGET   = 1;
     //moved public const int EngineConstants.TRAP_AOE_LOCATION_OFFSET          = 2;

     // -----------------------------------------------------------------------------
     // Behaviour bits for local integer EngineConstants.PLC_TRAP_DEACTIVATE
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.PLC_TRAP_SET_INACTIVE             = 0x01;   // Placeable is set inactive when trap is triggered or disarmed.
     //moved public const int EngineConstants.PLC_TRAP_DESTROY_WHEN_TRIGGERED   = 0x02;   // Placeable is destroyed when trap is triggered.
     //moved public const int EngineConstants.PLC_TRAP_DESTROY_WHEN_DISARMED    = 0x04;   // Placeable is destroyed when trap is disarmed.

     //moved public const float ROGUE_SKILL_BONUS               = 5.0f;

     //moved public const int   EngineConstants.VFX_TRAP_HOSTILE                = 30024;

     //moved public const int   EngineConstants.TRAP_EFFECTS_MAX                = 4;      // Max number of trap effects in traps.xls

     //moved public const float EngineConstants.TRAP_TRANSPARENCY               = 0.15;   // Degree of alpha xEffect applied to undetected traps.

     //moved public const int   EngineConstants.TRAP_DESTRUCTION_DELAY_MS       = 1000;

     //moved public const int   DEVICE_DIFFICULTY_IMPOSSIBLE    = 1000;   // difficulty score

     //moved public const string RES_PLC_TRAP_TRIGGER         = "genip_trap_trigger_1.utp";

     //moved public const string EngineConstants.SOUND_TRAP_DETECT_SUCCESS = "glo_fly_plc/trap_disarm/trap_detect";
     //moved public const string EngineConstants.SOUND_TRAP_DISARM_SUCCESS = "glo_fly_plc/trap_disarm/trap_disarm";
     //moved public const string EngineConstants.SOUND_TRAP_DISARM_FAILURE = "glo_fly_plc/trap_disarm/disarm_failure";

     // -----------------------------------------------------------------------------
     // Column name constants for traps.xls
     // -----------------------------------------------------------------------------
     //moved public const string EngineConstants.TRAP_COLUMN_ANIM_IMPACT = "AnimImpact";
     //moved public const string EngineConstants.TRAP_COLUMN_EFFECT      = "Effect";
     //moved public const string EngineConstants.TRAP_COLUMN_FLOAT1      = "_Float1";
     //moved public const string EngineConstants.TRAP_COLUMN_FLOAT2      = "_Float2";
     //moved public const string EngineConstants.TRAP_COLUMN_INT1        = "_Int1";
     //moved public const string EngineConstants.TRAP_COLUMN_INT2        = "_Int2";
     //moved public const string EngineConstants.TRAP_COLUMN_DURATION    = "_Duration";
     //moved public const string EngineConstants.TRAP_COLUMN_RESOURCE    = "_Resource";
     //moved public const string EngineConstants.TRAP_COLUMN_PLAYER_MADE = "PlayerCreated";

     // -----------------------------------------------------------------------------
     // The number of placeables a single detection pulse will iterate through (eg. 10
     // means that the pulse will only examine the 10 nearest placeables. This helps
     // performance and makes the system more realistic (as in "there's a lot of objects
     // around here, it takes a while to notice any traps")
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.TRAPS_MAX_SCANNED_PLACEABLES = 10;

     public struct TrapEffectStruct
     {
          public int nEffect;
          public int nInt1;
          public int nInt2;
          public float fFloat1;
          public float fFloat2;
          public float fDuration;

          // Row number and column name stored for later lookup since struct can't
          // contain a string type.
          public string sResource;   // column name
          public int nTrapType;   // row number
     };

     public struct TrapImpactStruct
     {
          public int nAnimImpact;
     };

     /*-----------------------------------------------------------------------------
     * @brief Generates EngineConstants.XP reward for disarming devices (locks & traps).
     *
     * @param    oDisarmer       Creature that disarmed the lock/trap.
     * @param    nDisarmLevel    The unlock/disarm difficulty level of the lock/trap.
     *-----------------------------------------------------------------------------*/
     public void AwardDisableDeviceXP(GameObject oDisarmer, int nDisarmLevel)
     {
          if (IsPartyMember(oDisarmer) != EngineConstants.FALSE)
          {
               int nMax = GetM2DAInt(EngineConstants.TABLE_REWARDS, EngineConstants.REWARDS_2DA_XP, 6);
               RewardXPParty(Min(Max(nDisarmLevel, 5), nMax), EngineConstants.XP_TYPE_EXPLORE, oDisarmer, oDisarmer);
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Returns a random float value between f1 and f2. Note that the decimal
     * component is always zero.
     *
     * @param    f1 First floating point value.
     * @param    f2 Second floating point value.
     * @returns     Floating point value between f1 and f2.
     *-----------------------------------------------------------------------------*/
     public float RandomRange(float f1, float f2)
     {
          float fMin = MinF(f1, f2);
          float fMax = MaxF(f1, f2);
          return RandFF(fMax - fMin, fMin);
     }

     /*-----------------------------------------------------------------------------
     * @brief Hack to determine the longest duration of a trap's effect(s).
     *
     * @param    nTrapType  The type of trap (index into trap.xls).
     * @returns             The duration of the longest duration xEffect for the given trap type.
     *-----------------------------------------------------------------------------*/
     public float _Trap_GetEffectDuration(int nTrapType)
     {
          float fDuration = 0.0f;
          int i;

          for (i = 1; i <= EngineConstants.TRAP_EFFECTS_MAX; i++)
          {
               float f = GetM2DAFloat(EngineConstants.TABLE_TRAPS, EngineConstants.TRAP_COLUMN_EFFECT + ToString(i) + EngineConstants.TRAP_COLUMN_DURATION, nTrapType);
               if (fDuration < f)
                    fDuration = f;
          }
          return fDuration;
     }

     /*-----------------------------------------------------------------------------
     * @brief Loads the data for a given trap type and effect.
     *
     * @param    nEffect   The xEffect to load. A trap can have up to 3 effects so
     *                     nEffect must be 1, 2, or 3.
     * @param    nTrapType The type of trap (index into trap.xls).
     * @returns            A TrapEffectStruct containing trap xEffect data for the
     *                     given trap type and effect.
     *-----------------------------------------------------------------------------*/
     public TrapEffectStruct _Trap_LoadEffectData(int nEffect, int nTrapType)
     {
          TrapEffectStruct stEffect;
          string sEffect = EngineConstants.TRAP_COLUMN_EFFECT + ToString(nEffect);

          stEffect.nEffect = GetM2DAInt(EngineConstants.TABLE_TRAPS, sEffect, nTrapType);
          stEffect.nInt1 = GetM2DAInt(EngineConstants.TABLE_TRAPS, sEffect + EngineConstants.TRAP_COLUMN_INT1, nTrapType);
          stEffect.nInt2 = GetM2DAInt(EngineConstants.TABLE_TRAPS, sEffect + EngineConstants.TRAP_COLUMN_INT2, nTrapType);
          stEffect.fFloat1 = GetM2DAFloat(EngineConstants.TABLE_TRAPS, sEffect + EngineConstants.TRAP_COLUMN_FLOAT1, nTrapType);
          stEffect.fFloat2 = GetM2DAFloat(EngineConstants.TABLE_TRAPS, sEffect + EngineConstants.TRAP_COLUMN_FLOAT2, nTrapType);
          stEffect.fDuration = GetM2DAFloat(EngineConstants.TABLE_TRAPS, sEffect + EngineConstants.TRAP_COLUMN_DURATION, nTrapType);
          stEffect.sResource = sEffect + EngineConstants.TRAP_COLUMN_RESOURCE;
          stEffect.nTrapType = nTrapType;

          return stEffect;
     }

     /*-----------------------------------------------------------------------------
     * @brief Loads data for a given trap type from traps.xls.
     *
     * @param    nTrapType  The type of trap (index into trap.xls).
     * @returns             A TrapImpactStruct containing data for a specific trap type.
     *-----------------------------------------------------------------------------*/
     public TrapImpactStruct _Trap_LoadTrapData(int nTrapType)
     {
          TrapImpactStruct stTrap;

          stTrap.nAnimImpact = GetM2DAInt(EngineConstants.TABLE_TRAPS, EngineConstants.TRAP_COLUMN_ANIM_IMPACT, nTrapType);

          return stTrap;
     }

     public int Trap_GetRank(GameObject oTrap)
     {
          int nRank = GetPlaceableTreasureRank(oTrap);     // 'Rank' property of the placeable.
          if (nRank < EngineConstants.CREATURE_RANK_CRITTER)
               nRank = EngineConstants.CREATURE_RANK_CRITTER;
          if (nRank > EngineConstants.CREATURE_RANK_ELITE_BOSS)
               nRank = EngineConstants.CREATURE_RANK_ELITE_BOSS;
          return nRank;
     }

     public int _HasEffect(GameObject oCreature, int nEffectType, GameObject oCreator = null, int nAbilityId = 0)
     {
          return GetArraySize(GetEffects(oCreature, nEffectType, nAbilityId, oCreator));
     }

     /*-----------------------------------------------------------------------------
     * @brief Applies a trap xEffect to a target creature.
     *
     * @param    oTarget  Target of the trap effect.
     * @param    lImpact  Impact Vector3 of the trap with the target.
     * @param    stEffect Trap xEffect structure that describes the trap xEffect to apply.
     * @param    oTrap    The trap placeable.

     * @returns  EngineConstants.TRUE if trap xEffect applied, EngineConstants.FALSE otherwise.
     *-----------------------------------------------------------------------------*/
     public int _Trap_ApplyEffect(GameObject oTarget, Vector3 lImpact, TrapEffectStruct stEffect, GameObject oTrap)
     {
          int bValidTrapEffect = EngineConstants.TRUE;
          int i = 0;

          switch (stEffect.nEffect)
          {
               case EngineConstants.TRAP_EFFECT_INVALID:
                    {
                         bValidTrapEffect = EngineConstants.FALSE;
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_DAMAGE:
                    {
                         int nDamageType = stEffect.nInt1;
                         float fDamage = stEffect.fFloat1 * Diff_GetTrapDamageModifier();
                         float fRange = stEffect.fFloat2;

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Damage: " + ToString(fDamage));

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   DamageCreature(arTargets[i], oTrap, fDamage, nDamageType);
                              }
                         }
                         else
                         {
                              DamageCreature(oTarget, oTrap, fDamage, nDamageType);
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_DAMAGE_SCALED:
                    {
                         int nDamageType = stEffect.nInt1;
                         float fDamageBase = stEffect.fFloat1;
                         float fRange = stEffect.fFloat2;
                         int nRank = Trap_GetRank(oTrap);     // 'Rank' property of the placeable.
                         float fDamage = fDamageBase * pow(2.0f, IntToFloat(nRank)) * Diff_GetTrapDamageModifier();

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Scaled Damage to rank " + ToString(nRank) + ": " + ToString(fDamage));

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   DamageCreature(arTargets[i], oTrap, fDamage, nDamageType);
                              }
                         }
                         else
                         {
                              DamageCreature(oTarget, oTrap, fDamage, nDamageType);
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_DOT:
                    {
                         int nDamageType = stEffect.nInt1;
                         int nImpactVFX = stEffect.nInt2;
                         float fDamageBase = stEffect.fFloat1;
                         float fRange = stEffect.fFloat2;
                         int nRank = Trap_GetRank(oTrap);     // 'Rank' property of the placeable.
                         float fDamage = fDamageBase * pow(2.0f, IntToFloat(nRank)) * Diff_GetTrapDamageModifier();

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   if (HasDotEffectOfType(arTargets[i], nDamageType) == EngineConstants.FALSE)
                                   {
                                        Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "DOT. Target: " + ToString(arTargets[i]) + ", Damage: " + ToString(fDamage) + ", duration: " + ToString(stEffect.fDuration));
                                        ApplyEffectDamageOverTime(arTargets[i], oTrap, 0, fDamage, stEffect.fDuration, nDamageType, 0, nImpactVFX);
                                   }
                              }
                         }
                         else
                         {
                              if (HasDotEffectOfType(oTarget, nDamageType) == EngineConstants.FALSE)
                              {
                                   Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "DOT. Target: " + ToString(oTarget) + ", Damage: " + ToString(fDamage) + ", duration: " + ToString(stEffect.fDuration));
                                   ApplyEffectDamageOverTime(oTarget, oTrap, 0, fDamage, stEffect.fDuration, nDamageType, 0, nImpactVFX);
                              }
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_AOE:
                    {
                         int nAOEID = stEffect.nInt1;
                         int nAOEVFX = stEffect.nInt2;
                         int nAOELocation = FloatToInt(stEffect.fFloat1);
                         float fOffset = stEffect.fFloat2;
                         string rScript = GetM2DAResource(EngineConstants.TABLE_TRAPS, stEffect.sResource, stEffect.nTrapType);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "AOE. location: " + ToString(nAOELocation) + ", script:" + ResourceToString(rScript) + ", duration: " + ToString(stEffect.fDuration) + (nAOEVFX != EngineConstants.FALSE ? ", VFX: " + ToString(nAOEVFX) : ""));

                         xEffect eAOE = EffectAreaOfEffect(nAOEID, rScript);
                         if (nAOEVFX > 0)
                              SetEffectEngineIntegerRef(ref eAOE, 2, nAOEVFX);

                         switch (nAOELocation)
                         {
                              case EngineConstants.TRAP_AOE_LOCATION_TRAP:
                                   Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eAOE, GetLocation(oTrap), stEffect.fDuration, oTrap, 0);
                                   break;
                              case EngineConstants.TRAP_AOE_LOCATION_SIGNAL_TARGET:
                                   Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eAOE, GetLocation(gameObject), stEffect.fDuration, oTrap, 0);
                                   break;
                              case EngineConstants.TRAP_AOE_LOCATION_OFFSET:
                                   {
                                        Vector3 vSelf = GetPosition(gameObject);
                                        float fAngle = VectorToAngle(GetPosition(oTrap) - vSelf);
                                        Vector3 v = AngleToVector(fAngle);
                                        Vector3 vAOE = Vector(vSelf.x + fOffset / 2.0f * v.x, vSelf.y + fOffset / 2.0f * v.y, vSelf.z);
                                        Vector3 lAOE = Location(GetArea(gameObject), vAOE, GetFacing(gameObject));

                                        Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "vTrap: " + VectorToString(GetPosition(oTrap)) + ", vSelf: " + VectorToString(vSelf) + ", fAngle: " + FloatToString(fAngle, 4, 1) + "v: " + VectorToString(v) + ", vAOE: " + VectorToString(vAOE));

                                        Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eAOE, lAOE, stEffect.fDuration, oTrap, 0);
                                        break;
                                   }
                              default:
                                   Warning("Invalid AOE Vector3 specified for trap '" + ToString(oTrap) + "': " + ToString(nAOELocation));
                                   break;
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_KNOCKDOWN:
                    {
                         int nAbility = stEffect.nInt1;
                         int bKnockback = stEffect.nInt2;
                         float fRange = stEffect.fFloat2;

                         Vector3 vImpact = GetPositionFromLocation(lImpact);
                         Vector3 vKnockback = Vector(-vImpact.x, -vImpact.y, -vImpact.z);
                         float fKnockback = VectorToAngle(vKnockback);
                         float fDuration = GetRankAdjustedEffectDuration(oTarget, stEffect.fDuration);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Knockdown. duration: " + ToString(stEffect.fDuration));

                         xEffect eKD = EffectKnockdown(oTrap, 0, nAbility);
                         SetEffectEngineFloatRef(ref eKD, EngineConstants.EFFECT_FLOAT_INTERPOLATION_ANGLE, fKnockback);
                         SetEffectEngineIntegerRef(ref eKD, EngineConstants.EFFECT_INTEGER_USE_INTERPOLATION_ANGLE, bKnockback);

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   fDuration = GetRankAdjustedEffectDuration(arTargets[i], stEffect.fDuration);
                                   if (_HasEffect(arTargets[i], EngineConstants.EFFECT_TYPE_KNOCKDOWN, oTrap) == EngineConstants.FALSE)
                                        ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eKD, arTargets[i], fDuration + RandomFloat(), oTrap);
                              }
                         }
                         else if (_HasEffect(oTarget, EngineConstants.EFFECT_TYPE_KNOCKDOWN, oTrap) == EngineConstants.FALSE)
                         {
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eKD, oTarget, fDuration + RandomFloat(), oTrap);
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_SUMMON:
                    {
                         int nVFX = stEffect.nInt1;
                         string rSummon = GetM2DAResource(EngineConstants.TABLE_TRAPS, stEffect.sResource, stEffect.nTrapType);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Summon: " + ResourceToString(rSummon) + ", Target: " + ToString(oTarget));

                         GameObject oSummon = CreateObject(EngineConstants.OBJECT_TYPE_CREATURE, rSummon, GetLocation(gameObject));
                         AS_InitCreature(oSummon, GetLevel(oTarget), EngineConstants.FALSE);
                         SetGroupId(oSummon, EngineConstants.GROUP_HOSTILE);
                         ApplyEffectVisualEffect(oSummon, oSummon, nVFX, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_DAZE:
                    {
                         float fRange = stEffect.fFloat2;
                         float fDuration = GetRankAdjustedEffectDuration(oTarget, stEffect.fDuration);
                         xEffect eDaze = EffectDaze();

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Daze.");

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   fDuration = GetRankAdjustedEffectDuration(arTargets[i], stEffect.fDuration);
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eDaze, arTargets[i], fDuration);
                              }
                         }
                         else
                         {
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eDaze, oTarget, fDuration);
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_DEATH:
                    {
                         float fRange = stEffect.fFloat2;

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Death.");

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   KillCreature(arTargets[i], oTrap);
                              }
                         }
                         else
                         {
                              KillCreature(oTarget, oTrap);
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_DISPEL:
                    {
                         int nSpells = stEffect.nInt1;
                         float fRange = stEffect.fFloat2;
                         xEffect eDispel = EffectDispelMagic(nSpells);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Dispel. # spells: " + ToString(nSpells));

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eDispel, arTargets[i]);
                              }
                         }
                         else
                         {
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eDispel, oTarget);
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_PARALYZE:
                    {
                         int nSubType = stEffect.nInt1;
                         float fRange = stEffect.fFloat2;
                         float fDuration = GetRankAdjustedEffectDuration(oTarget, stEffect.fDuration);
                         xEffect eParalyze = EffectParalyze(nSubType);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Paralyze.");

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   fDuration = GetRankAdjustedEffectDuration(arTargets[i], stEffect.fDuration);
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eParalyze, arTargets[i], fDuration);
                              }
                         }
                         else
                         {
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eParalyze, oTarget, fDuration);
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_ROOT:
                    {
                         float fRange = stEffect.fFloat2;
                         float fDuration = GetRankAdjustedEffectDuration(oTarget, stEffect.fDuration);
                         xEffect eRoot = EffectRoot();

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Root. duration: " + ToString(stEffect.fDuration));

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   fDuration = GetRankAdjustedEffectDuration(arTargets[i], stEffect.fDuration);
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eRoot, arTargets[i], fDuration);
                              }
                         }
                         else
                         {
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eRoot, oTarget, fDuration);
                         }

                         // Signal trap to 'release' target
                         if (IsObjectValid(oTrap) != EngineConstants.FALSE)
                         {
                              xEvent evRelease = Event(EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ARMED);
                              DelayEvent(fDuration - 0.3f, oTrap, evRelease);
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_STUN:
                    {
                         float fRange = stEffect.fFloat2;
                         xEffect eStun = EffectStun();
                         float fDuration = GetRankAdjustedEffectDuration(oTarget, stEffect.fDuration);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Stun.");

                         if (fRange > 0.1f)
                         {
                              List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                              for (i = 0; i < GetArraySize(arTargets); i++)
                              {
                                   fDuration = GetRankAdjustedEffectDuration(arTargets[i], stEffect.fDuration);
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eStun, arTargets[i], fDuration);
                              }
                         }
                         else
                         {
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eStun, oTarget, fDuration);
                         }
                         break;
                    }

               case EngineConstants.TRAP_EFFECT_MODIFY_MANA:
                    {
                         float fMin = stEffect.fFloat1;
                         float fMax = stEffect.fFloat2;
                         float fAmount = RandomRange(fMin, fMax);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Modify Mana/Stamina. amount: " + ToString(fAmount));

                         xEffect eModMana = EffectModifyManaStamina(fAmount);
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eModMana, oTarget);
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_MODIFY_CRIT:
                    {
                         int nType = stEffect.nInt1;
                         float fMin = stEffect.fFloat1;
                         float fMax = stEffect.fFloat2;
                         float fAmount = RandomRange(fMin, fMax);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Modify Crit Chance. amount: " + ToString(fAmount) + ", duration: " + ToString(stEffect.fDuration));

                         xEffect eModCrit = EffectModifyCritChance(nType, fAmount);
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eModCrit, oTarget, stEffect.fDuration);
                         break;
                    }

               case EngineConstants.TRAP_EFFECT_SCREENSHAKE:
                    {
                         int nType = stEffect.nInt1;

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Screenshake. type: " + ToString(nType) + ", duration: " + ToString(stEffect.fDuration));

                         xEffect eShake = EffectScreenShake(nType);
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eShake, oTarget, stEffect.fDuration);
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_VFX:
                    {
                         int nVFX = stEffect.nInt1;
                         int nVFXTarget = stEffect.nInt2;
                         float fDuration = stEffect.fDuration;
                         float fRange = stEffect.fFloat2;
                         int nDurationType = (fabs(fDuration) < 0.1) ? EngineConstants.EFFECT_DURATION_TYPE_INSTANT : EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY;

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "VFX: " + ToString(nVFX)
                             + ", Duration: " + ToString(fDuration)
                             + ", Location: " + (nVFXTarget == EngineConstants.TRAP_VFX_APPLY_TO_LOCATION ? VectorToString(GetPositionFromLocation(lImpact)) :
                                                 nVFXTarget == EngineConstants.TRAP_VFX_APPLY_TO_TARGET ? ToString(oTarget) :
                                                 nVFXTarget == EngineConstants.TRAP_VFX_APPLY_TO_TRAP ? ToString(oTrap) : ToString(gameObject)));
                         if (nVFX != EngineConstants.FALSE)
                         {
                              xEffect eVFX = EffectVisualEffect(nVFX);

                              if (nVFXTarget == EngineConstants.TRAP_VFX_APPLY_TO_LOCATION)
                                   Engine_ApplyEffectAtLocation(nDurationType, eVFX, lImpact, fDuration);
                              else if (nVFXTarget == EngineConstants.TRAP_VFX_APPLY_TO_TARGET)
                              {
                                   if (fRange > 0.1f)
                                   {
                                        List<GameObject> arTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lImpact, fRange);
                                        for (i = 0; i < GetArraySize(arTargets); i++)
                                        {
                                             ApplyEffectOnObject(nDurationType, eVFX, arTargets[i], fDuration);
                                        }
                                   }
                                   else
                                   {
                                        ApplyEffectOnObject(nDurationType, eVFX, oTarget, fDuration);
                                   }
                              }
                              else if (nVFXTarget == EngineConstants.TRAP_VFX_APPLY_TO_TRAP)
                                   ApplyEffectOnObject(nDurationType, eVFX, oTrap, fDuration);
                              else if (nVFXTarget == EngineConstants.TRAP_VFX_APPLY_TO_SIGNAL_TARGET)
                                   ApplyEffectOnObject(nDurationType, eVFX, gameObject, fDuration);
                              else
                                   Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", ToString(nVFXTarget) + " *** Invalid target type ***");
                         }
                         break;
                    }
               case EngineConstants.TRAP_EFFECT_SFX:
                    {
                         string sSound = GetM2DAString(EngineConstants.TABLE_TRAPS, stEffect.sResource, stEffect.nTrapType);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", "Play sound: " + sSound);

                         if (GetStringLength(sSound) > 0)
                              PlaySound(oTrap, sSound);
                         break;
                    }
               default:
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h._Trap_ApplyEffect", ToString(stEffect.nEffect) + " *** Unhandled trap xEffect ***");
                         Warning("Unhandled trap effect: " + ToString(stEffect.nEffect));
                         break;
                    }
          }
          return bValidTrapEffect;
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the type of a trap
     * @returns  Index into traps.xls
     *-----------------------------------------------------------------------------*/
     public int Trap_GetType(GameObject oPlc)
     {
          return GetLocalInt(oPlc, EngineConstants.PLC_TRAP_TYPE);
     }

     /*-----------------------------------------------------------------------------
     * @brief Applies trap effects to a target creature.
     *
     * @param    nTrapType The type of trap (index into traps.xls).
     * @param    oTarget   Target of the trap effect.
     * @param    lImpact   Impact Vector3 of the trap with the target.
     * @param    oTrap     The trap placeable
     * @returns  EngineConstants.TRUE if trap xEffect applied, EngineConstants.FALSE otherwise.
     *-----------------------------------------------------------------------------*/
     public void Trap_Triggered(int nTrapType, GameObject oTarget, Vector3 lImpact, GameObject oTrap = null)
     {
          TrapImpactStruct stTrap = _Trap_LoadTrapData(nTrapType);

          // Target plays impact animation
          if (stTrap.nAnimImpact > 0)
          {
               WR_AddCommand(oTarget, CommandPlayAnimation(stTrap.nAnimImpact), EngineConstants.TRUE);
          }

          // Apply trap effects to target until valid trap effects run out
          int i = 1;
          while (i <= EngineConstants.TRAP_EFFECTS_MAX &&
                  _Trap_ApplyEffect(oTarget, lImpact, _Trap_LoadEffectData(i, nTrapType), oTrap) != EngineConstants.FALSE)
               i++;
     }

     /*-----------------------------------------------------------------------------
     * @brief    Rewards creature for disarming a hostile trap.
     * @param    oDisarmedBy The party member who disarmed the trap.
     * @param    oTrap       The trap placeable.
     *-----------------------------------------------------------------------------*/

     // NOT USED??

     public void Trap_GiveRewardForDisarm(GameObject oDisarmedBy, GameObject oTrap)
     {

          int nXP = GetM2DAInt(EngineConstants.TABLE_REWARDS, EngineConstants.REWARDS_2DA_XP, 6);

          RewardXPParty(nXP, EngineConstants.XP_TYPE_EXPLORE);
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the armed state of a trap placeable.
     * @returns  EngineConstants.TRUE if trap is armed, EngineConstants.FALSE otherwise.
     *-----------------------------------------------------------------------------*/
     public int Trap_IsArmed(GameObject oTrap)
     {
          // Not armed if inactive
          if (GetObjectActive(oTrap) == EngineConstants.FALSE)
               return EngineConstants.FALSE;

          // Not armed if no AOE effect
          if (GetArraySize(GetEffects(oTrap, EngineConstants.EFFECT_TYPE_AOE, 0, oTrap, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT)) == 0)
               return EngineConstants.FALSE;

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_IsArmed", "GetPlaceableState(" + ToString(oTrap) + ") = " + ToString(GetPlaceableState(oTrap)));

          // Not armed if trap trigger placeable is in disabled/deactivated state.
          if (GetPlaceableStateCntTable(oTrap) == EngineConstants.PLC_STATE_CONTROLLER_TRAP_TRIGGGER && GetPlaceableState(oTrap) == EngineConstants.PLC_STATE_TRAP_TRIGGER_DISABLED)
               return EngineConstants.FALSE;

          return EngineConstants.TRUE;
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns EngineConstants.TRUE if oTrap is a player-created trap.
     * @param    oTrap   The trap trigger placeable.
     *-----------------------------------------------------------------------------*/
     public int Trap_IsPlayerCreated(GameObject oTrap)
     {
          GameObject oOwner = GetLocalObject(oTrap, EngineConstants.PLC_TRAP_OWNER);
          return (IsObjectValid(oOwner) != EngineConstants.FALSE && IsPartyMember(oOwner) != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*-----------------------------------------------------------------------------
     * @brief    Determines whether the trap should initially be hidden.
     * @param    oTrap   The trap trigger placeable.
     * @returns  EngineConstants.TRUE if trap should be initially hidden. EngineConstants.FALSE otherwise.
     *-----------------------------------------------------------------------------*/
     public int Trap_GetHiddenWhenUndetected(GameObject oTrap)
     {
          return GetM2DAInt(EngineConstants.TABLE_TRAPS, "hidewhenundetected", Trap_GetType(oTrap));
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the detection status of a trap
     * @param    oTrap   The trap trigger placeable.
     * @returns  EngineConstants.TRUE if trap has been detected.
     *-----------------------------------------------------------------------------*/
     public int Trap_GetDetected(GameObject oTrap)
     {
          return GetLocalInt(oTrap, EngineConstants.PLC_TRAP_DETECTED);
     }

     /*-----------------------------------------------------------------------------
     * @brief    Set the detection status of a trap and toggles player visibilty
     *-----------------------------------------------------------------------------*/
     public void Trap_SetDetected(GameObject oTrap, int bDetected = EngineConstants.TRUE)
     {
          if (bDetected != EngineConstants.FALSE)
          {
               if (GetLocalInt(oTrap, EngineConstants.PLC_TRAP_DETECTED) == EngineConstants.FALSE)
               {
                    if (Trap_GetHiddenWhenUndetected(oTrap) != EngineConstants.FALSE)
                    {
                         List<xEffect> eTrans = GetEffects(oTrap, EngineConstants.EFFECT_TYPE_ALPHA, 0, oTrap, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT);
                         RemoveEffectArray(oTrap, eTrans);
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_SetDetected", "Setting " + ToString(oTrap) + " detected: " + ToString(bDetected));
                    }
               }
          }
          else
          {
               if (GetLocalInt(oTrap, EngineConstants.PLC_TRAP_DETECTED) == EngineConstants.FALSE)
               {
                    if (Trap_GetHiddenWhenUndetected(oTrap) != EngineConstants.FALSE)
                    {
                         xEffect eTransparent = Effect(EngineConstants.EFFECT_TYPE_ALPHA);
                         SetEffectEngineFloatRef(ref eTransparent, EngineConstants.EFFECT_FLOAT_POTENCY, EngineConstants.TRAP_TRANSPARENCY);
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eTransparent, oTrap, 0.0f, oTrap, 0);
                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_SetDetected", "Setting " + ToString(oTrap) + " detected: " + ToString(bDetected));
                    }
               }

          }
          SetObjectInteractive(oTrap, bDetected);
          SetLocalInt(oTrap, EngineConstants.PLC_TRAP_DETECTED, bDetected);
     }

     /*-----------------------------------------------------------------------------
     * @brief    Wrapper for calculating trap disarm score.
     * @returns  Total skill score modified by the player's level, attributes, and abilities.
     *-----------------------------------------------------------------------------*/
     public int Trap_GetModifiedScore(GameObject oCreature)
     {
          float fAbility = 0.0f;
          if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_LOCKPICKING_1) != EngineConstants.FALSE)
               fAbility += 1.0f;
          if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_LOCKPICKING_2) != EngineConstants.FALSE)
               fAbility += 1.0f;
          if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_LOCKPICKING_3) != EngineConstants.FALSE)
               fAbility += 1.0f;
          if (HasAbility(oCreature, EngineConstants.ABILITY_SKILL_LOCKPICKING_4) != EngineConstants.FALSE)
               fAbility += 1.0f;
          fAbility *= EngineConstants.ROGUE_SKILL_BONUS;

          float fAttribute = GetAttributeModifier(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE);

          float fLevel = IntToFloat(GetLevel(oCreature));

          float fScore = fAttribute + fAbility + fLevel;

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_GetModifiedScore",
              "(Ability + Attribute + Level) = ("
              + FloatToString(fAbility, 3, 1) + " + " + FloatToString(fAttribute, 3, 1) + " + "
              + FloatToString(fLevel, 3, 1) + ") = " + FloatToString(fScore, 3, 1));

          return FloatToInt(fScore);
     }

     /*-----------------------------------------------------------------------------
     * @brief    Applies/remove highlight xEffect on trap trigger placeable.
     * @param    oTrap       The trap trigger placeable.
     * @param    bHighlight  True to highlight the trap placeable. False to remove highlighting.
     *-----------------------------------------------------------------------------*/
     public void Trap_Highlight(GameObject oTrap, int bHighlight = EngineConstants.TRUE)
     {
          if (GetAppearanceType(oTrap) == EngineConstants.PLC_INVISIBLE_TRAP_TRIGGER)
          {
               // Apply/remove red highlighting to invisible placeables.
               if (bHighlight != EngineConstants.FALSE)
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, EffectVisualEffect(EngineConstants.VFX_TRAP_HOSTILE), oTrap, 0.0f, oTrap, 0);
               else
                    RemoveVisualEffect(oTrap, EngineConstants.VFX_TRAP_HOSTILE);
          }
          else
          {
               // Apply/remove red highlighting to visible placeables.
               SetTrapDetected(oTrap, bHighlight);
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns EngineConstants.TRUE if oPlayer successfully detects oTrap.
     *-----------------------------------------------------------------------------*/
     public int Trap_TryDetectTrap(GameObject oPlayer, GameObject oTrap)
     {
          int nTargetScore = GetTrapDetectionDifficulty(oTrap);
          if (nTargetScore <= EngineConstants.DEVICE_DIFFICULTY_IMPOSSIBLE)
          {
               int nPlayerScore = FloatToInt(GetDisableDeviceLevel(oPlayer));
               if (GetHasEffects(oPlayer, EngineConstants.EFFECT_TYPE_TRAP_DETECTION_BONUS) != EngineConstants.FALSE)
               {
                    nPlayerScore += 10;
               }

               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_TryDetectTrap", ToString(oTrap) + ((nPlayerScore >= nTargetScore) ? " detected" : " undetected") + " (Player Skill: " + ToString(nPlayerScore) + " vs. Trap Detect Difficulty: " + ToString(nTargetScore) + ")");

               if (nPlayerScore >= nTargetScore)
               {
                    Trap_SetDetected(oTrap, EngineConstants.TRUE);
                    Trap_Highlight(oTrap, EngineConstants.TRUE);
                    UI_DisplayMessage(oPlayer, EngineConstants.UI_MESSAGE_TRAP_DETECTED);
                    return EngineConstants.TRUE;
               }
          }
          return EngineConstants.FALSE;
     }

     /*-----------------------------------------------------------------------------
     * @brief    Runs a 'detection' pulse on the player, scanning the immediate area
     *           for undetected traps. Size of the scan area is defined by the
     *           trap skill on the player.
     *-----------------------------------------------------------------------------*/
     public void Trap_RunDetectionPulse(GameObject oPlayer)
     {
          // Only rogues can detect traps.
          int nGameMode = GetGameMode();
          if ((nGameMode == EngineConstants.GM_COMBAT || nGameMode == EngineConstants.GM_EXPLORE) && HasAbility(oPlayer, EngineConstants.ABILITY_TALENT_HIDDEN_ROGUE) != EngineConstants.FALSE)
          {
               // Scale detection radius based on skill
               // Slight misnomer: EngineConstants.ABILITY_SKILL_LOCKPICKING is the universal 'disable devices' skill.
               float fDetectionRadius = 10.0f;
               if (HasAbility(oPlayer, EngineConstants.ABILITY_SKILL_LOCKPICKING_1) != EngineConstants.FALSE)
                    fDetectionRadius += 1.0f;
               if (HasAbility(oPlayer, EngineConstants.ABILITY_SKILL_LOCKPICKING_2) != EngineConstants.FALSE)
                    fDetectionRadius += 1.0f;
               if (HasAbility(oPlayer, EngineConstants.ABILITY_SKILL_LOCKPICKING_3) != EngineConstants.FALSE)
                    fDetectionRadius += 1.0f;
               if (HasAbility(oPlayer, EngineConstants.ABILITY_SKILL_LOCKPICKING_4) != EngineConstants.FALSE)
                    fDetectionRadius += 1.0f;

               // Get all placeables within fDetectionRadius radius
               List<GameObject> arTraps = GetObjectsInShape(EngineConstants.OBJECT_TYPE_PLACEABLE, EngineConstants.SHAPE_SPHERE, GetLocation(oPlayer), fDetectionRadius);
               int i, n = GetArraySize(arTraps);

               // Scan list of placeables for undetected, hostile, armed traps
               for (i = 0; i < n && i < EngineConstants.TRAPS_MAX_SCANNED_PLACEABLES; i++)
               {
                    if (GetObjectType(arTraps[i]) == EngineConstants.OBJECT_TYPE_PLACEABLE
                       && Trap_GetType(arTraps[i]) > 0
                       && Trap_IsPlayerCreated(arTraps[i]) == EngineConstants.FALSE
                       && Trap_GetDetected(arTraps[i]) == EngineConstants.FALSE
                       && Trap_IsArmed(arTraps[i]) != EngineConstants.FALSE
                       && CheckLineOfSightObject(oPlayer, arTraps[i]) != EngineConstants.FALSE)
                    {
                         // Try to detect the trap. If we detect one, we won't detect
                         // any others until the next pulse. This is to stagger detection
                         // messages as well as make for a bit more realistic 'searching'
                         if (Trap_TryDetectTrap(oPlayer, arTraps[i]) != EngineConstants.FALSE)
                         {
                              PlaySoundSet(oPlayer, EngineConstants.SS_EXPLORE_TRAP_DETECTED);
                              PlaySound(oPlayer, EngineConstants.SOUND_TRAP_DETECT_SUCCESS);
                              break;
                         }
                    }
               }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns array of objects the trap will signal its OnEnter/OnExit/OnArm/OnDisarm events to.
     * @returns  Array of objects to signal.
     *-----------------------------------------------------------------------------*/
     public List<GameObject> Trap_GetSignalTarget(GameObject oPlc)
     {
          List<GameObject> aTarget = new List<GameObject>();
          string sTag = GetLocalString(oPlc, EngineConstants.PLC_TRAP_SIGNAL_TAG);

          if (sTag == "BY_TAG")
          {

               sTag = GetTag(oPlc) + "_target";
               aTarget = GetNearestObjectByTag(oPlc, sTag, EngineConstants.OBJECT_TYPE_ALL, 10);
               if (GetArraySize(aTarget) == 0)
                    Warning("Trap signal target with tag " + sTag + " not found.");
          }
          else if (sTag == "BY_TEAM")
          {
               List<GameObject> aTeam = GetTeam(GetTeamId(oPlc), EngineConstants.OBJECT_TYPE_PLACEABLE);
               int i = 0, j = 0, n = GetArraySize(aTeam);
               for (; i < n; i++)
               {
                    if (aTeam[i] != oPlc)
                         aTarget[j++] = aTeam[i];
               }
          }
          else if (sTag != "")
          {
               aTarget = GetNearestObjectByTag(oPlc, sTag, EngineConstants.OBJECT_TYPE_ALL, 10);
               if (GetArraySize(aTarget) == 0)
                    Warning("Trap signal target with tag " + sTag + " not found.");
          }
          else
          {
               aTarget[0] = oPlc;
          }
          return aTarget;
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the 'owner' of a trap.
     * @returns  GameObject id of a creature or object_invalid for preplaced traps
     *-----------------------------------------------------------------------------*/
     public GameObject Trap_GetOwner(GameObject oTrap)
     {
          if (GetObjectType(oTrap) == EngineConstants.OBJECT_TYPE_PLACEABLE)
               return GetLocalObject(oTrap, EngineConstants.PLC_TRAP_OWNER);
          return null;
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the projectile associated with a trap.
     * @returns  The ID of the projectile the trap shoots.
     *-----------------------------------------------------------------------------*/
     public int Trap_GetProjectileType(GameObject oPlc)
     {
          return GetM2DAInt(EngineConstants.TABLE_TRAPS, "Projectile", Trap_GetType(oPlc));
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the number of projectiles the trap shoots.
     * @returns  The number of projectiles the trap shoots.
     *-----------------------------------------------------------------------------*/
     public int Trap_GetProjectileCount(GameObject oTrap)
     {
          return GetM2DAInt(EngineConstants.TABLE_TRAPS, "ProjectileCount", Trap_GetType(oTrap));
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the ID of the crust xEffect to applly to the projectile.
     * @returns  The ID of the crust xEffect to apply to a trap's projectile.
     *-----------------------------------------------------------------------------*/
     public int Trap_GetProjectileCrustVFX(GameObject oTrap)
     {
          return GetM2DAInt(EngineConstants.TABLE_TRAPS, "ProjectileCrust", Trap_GetType(oTrap));
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the delay (in seconds) after which the trap should reset.
     * @returns  The trap's reset delay in seconds, with 0.0f signifying no reset.
     *-----------------------------------------------------------------------------*/
     public float Trap_GetResetDelay(GameObject oTrap)
     {
          return GetM2DAFloat(EngineConstants.TABLE_TRAPS, "ResetDelay", Trap_GetType(oTrap));

     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the delay (in seconds) after which the trap should rearm.
     * @returns  The trap's rearm delay in seconds, with 0.0f signifying no rearming.
     *-----------------------------------------------------------------------------*/
     public float Trap_GetRearmDelay(GameObject oTrap)
     {
          return GetLocalFloat(oTrap, EngineConstants.PLC_TRAP_REARM_DELAY);
     }

     /*-----------------------------------------------------------------------------
     * @brief    Returns the ID of the AOE xEffect associated with this trap.
     * @returns  The ID of the AOE xEffect for this trap.
     *-----------------------------------------------------------------------------*/
     public int Trap_GetAOEID(GameObject oTrap)
     {
          int nAOE = GetLocalInt(oTrap, EngineConstants.PLC_TRAP_AOE);
          if (nAOE == EngineConstants.AOE_INVALID)
               nAOE = GetM2DAInt(EngineConstants.TABLE_TRAPS, "aoe_index", Trap_GetType(oTrap));
          if (nAOE == EngineConstants.AOE_INVALID)
               Warning("Invalid AOE associated with trap");
          return nAOE;
     }

     public int Trap_GetTriggerVFX(GameObject oTrap)
     {
          return GetM2DAInt(EngineConstants.TABLE_TRAPS, "locationvfx", Trap_GetType(oTrap));
     }

     public int Trap_GetAOEVFX(GameObject oTrap)
     {
          return GetM2DAInt(EngineConstants.TABLE_TRAPS, "aoevfx", Trap_GetType(oTrap));
     }

     public void Trap_PlayTriggerVFX(GameObject oTrap, GameObject oTarget)
     {
          int nVFX = Trap_GetTriggerVFX(oTrap);
          if (nVFX > 0 && IsObjectValid(oTarget) != EngineConstants.FALSE)
          {
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(nVFX), oTarget, 0.0f, oTarget, 0);
          }
          else if (nVFX < 0 && IsLocationValid(GetLocation(oTrap)) != EngineConstants.FALSE)
          {
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(abs(nVFX)), GetLocation(oTrap), 0.0f, oTrap, 0);
          }
     }

     public string Trap_GetAOETag(GameObject oPlc)
     {
          return GetM2DAString(EngineConstants.TABLE_VFX_PERSISTENT, "label", Trap_GetAOEID(oPlc));
     }

     /*-----------------------------------------------------------------------------
     * @brief    Creates a dynamic trap
     *
     * @param    nTrapType   The type of trap (index into traps.xls).
     * @param    lLoc        The Vector3 to create the trap.
     * @param    oOwner      The owner of the trap.
     * @param    rPlc        The string of the placeable trap trigger to create (if EngineConstants.INVALID_RESOURCE then string specified in traps.xls will be used).
     * @param    rItem       The string of the item received when the trap is recovered.
     *-----------------------------------------------------------------------------*/
     public GameObject Trap_CreateTrap(int nTrapType, Vector3 lLoc, GameObject oOwner = null, string rPlc = EngineConstants.INVALID_RESOURCE, string rItem = EngineConstants.INVALID_RESOURCE)
     {
          string rTrap;

          if (rPlc == EngineConstants.INVALID_RESOURCE)
          {
               rTrap = GetM2DAResource(EngineConstants.TABLE_TRAPS, "TrapPlaceable", nTrapType);
          }
          else
          {
               rTrap = EngineConstants.RES_PLC_TRAP_TRIGGER;
          }

          GameObject oTrap = CreateObject(EngineConstants.OBJECT_TYPE_PLACEABLE, rTrap, lLoc, "", EngineConstants.FALSE);

          // Set trap type, owner, and string received when trap is recovered.
          SetLocalInt(oTrap, EngineConstants.PLC_TRAP_TYPE, nTrapType);
          SetLocalObject(oTrap, EngineConstants.PLC_TRAP_OWNER, oOwner);
          if (rItem != EngineConstants.INVALID_RESOURCE)
               SetLocalResource(oTrap, EngineConstants.PLC_TRAP_ITEM, rItem);

          // Make player-created traps (except glyph of paralysis spell) interactive so they can be recovered.
          xEvent evInteractive = Event(EngineConstants.EVENT_TYPE_SET_INTERACTIVE);
          SetEventIntegerRef(ref evInteractive, 0, (IsObjectValid(oOwner) != EngineConstants.FALSE && nTrapType != EngineConstants.SPELL_GLYPH_PARALYSIS) ? EngineConstants.TRUE : EngineConstants.FALSE);
          DelayEvent(0.1f, oTrap, evInteractive);

          return oTrap;
     }

     /*-----------------------------------------------------------------------------
     * @brief    Arms a trap after fDelay seconds.
     *-----------------------------------------------------------------------------*/
     public void Trap_ArmTrap(GameObject oTrap, GameObject oOwner, float fDelay = 5.0f)
     {
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_ArmTrap", "Arming trap " + ToString(oTrap) + " in " + ToString(fDelay) + " seconds, Owner: " + ToString(oOwner));

          xEvent evArm = Event(EngineConstants.EVENT_TYPE_TRAP_ARM);
          SetEventObjectRef(ref evArm, 0, oOwner);
          SetLocalObject(oTrap, EngineConstants.PLC_TRAP_OWNER, oOwner);
          DelayEvent(fDelay, oTrap, evArm);

          // Unlock achievement for setting traps
          ACH_TrapAchievement(oOwner);
     }

     /*-----------------------------------------------------------------------------
     * @brief    Default handler for EngineConstants.EVENT_TYPE_TRAP_DISARM.
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventDisarm(xEvent ev)
     {
          GameObject oDisarmedBy = GetEventObjectRef(ref ev, 0);
          GameObject oTrap = gameObject;
          int nFlag = GetLocalInt(oTrap, EngineConstants.PLC_TRAP_DEACTIVATE);

          // If oDisarmedBy is valid the trap is being deactivated because it was disarmed.
          // Otherwise, the trap is being deactivated because it was triggered.

          if (IsObjectValid(oDisarmedBy) != EngineConstants.FALSE)
          {
               UI_DisplayMessage(oDisarmedBy, EngineConstants.TRAP_DISARM_SUCCEEDED);

               string rTrapItem = GetLocalResource(oTrap, EngineConstants.PLC_TRAP_ITEM);
               if (rTrapItem == EngineConstants.INVALID_RESOURCE)
               {
                    // Reward for disarming hostile traps.
                    ACH_DisarmAchievement(oDisarmedBy);
                    AwardDisableDeviceXP(oDisarmedBy, GetTrapDetectionDifficulty(oTrap));
                    PlaySound(oTrap, EngineConstants.SOUND_TRAP_DISARM_SUCCESS);
               }
               else
               {
                    // Recover player-created traps.
                    GameObject oTrapItem = CreateItemOnObject(rTrapItem, oDisarmedBy);
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventDisarm", ToString(oTrapItem) + " created on " + ToString(oDisarmedBy), oTrap);

                    // Always destroy the placeable associated with player-created traps.
                    nFlag |= EngineConstants.PLC_TRAP_DESTROY_WHEN_DISARMED;
               }

               // Destroy/deactivate trap as indicated by the EngineConstants.PLC_TRAP_DEACTIVATE flag
               Debug.Log("sys_traps_h: flag type & replaced with ==, double check");
               if (nFlag == EngineConstants.PLC_TRAP_DESTROY_WHEN_DISARMED)
               {
                    SetPlot(oTrap, EngineConstants.FALSE);
                    Safe_Destroy_Object(oTrap, 100);
               }
               else if (nFlag == EngineConstants.PLC_TRAP_SET_INACTIVE)
               {
                    Debug.Log("sys_traps_h: flag type & replaced with ==, double check");
                    SetObjectActive(oTrap, EngineConstants.FALSE);
               }
          }
          else  // Trap being deactivated because it was triggered.
          {
               if (nFlag == EngineConstants.PLC_TRAP_DESTROY_WHEN_TRIGGERED)
               {
                    Debug.Log("sys_traps_h: flag type & replaced with ==, double check");
                    SetPlot(oTrap, EngineConstants.FALSE);
                    Safe_Destroy_Object(oTrap, 100);
               }
               else if (nFlag == EngineConstants.PLC_TRAP_SET_INACTIVE)
               {
                    Debug.Log("sys_traps_h: flag type & replaced with ==, double check");
                    SetObjectActive(oTrap, EngineConstants.FALSE);
               }
          }

          // Animate placeable
          if (GetPlaceableStateCntTable(oTrap) == EngineConstants.PLC_STATE_CONTROLLER_TRAP_SELECTABLE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventDisarm", "Setting " + ToString(oTrap) + " to EngineConstants.PLC_STATE_TRAP_SELECTABLE_DEAD", oTrap);
               SetPlaceableState(oTrap, EngineConstants.PLC_STATE_TRAP_SELECTABLE_DEAD);
          }
          else if (GetPlaceableStateCntTable(oTrap) == EngineConstants.PLC_STATE_CONTROLLER_TRAP_TRIGGGER)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventDisarm", "Setting " + ToString(oTrap) + " to EngineConstants.PLC_STATE_TRAP_TRIGGER_DISABLED");
               SetPlaceableState(oTrap, EngineConstants.PLC_STATE_TRAP_TRIGGER_DISABLED);
          }

          Trap_Highlight(oTrap, EngineConstants.FALSE);
          SetObjectInteractive(oTrap, EngineConstants.FALSE);

          // Remove AOE trigger
          List<xEffect> arAOE = GetEffects(oTrap, EngineConstants.EFFECT_TYPE_AOE, 0, oTrap, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventDisarm", "Removing " + ToString(GetArraySize(arAOE)) + " EngineConstants.EFFECT_TYPE_AOE from " + ToString(oTrap));
          RemoveEffectArray(oTrap, arAOE);

          // If trap has attached signal target(s), notify them that trap was disarmed.
          List<GameObject> aTarget = Trap_GetSignalTarget(oTrap);
          int i, n = GetArraySize(aTarget);
          xEvent evDisarmed = Event(EngineConstants.EVENT_TYPE_TRAP_TRIGGER_DISARMED);
          SetEventObjectRef(ref evDisarmed, 0, oDisarmedBy);

          for (i = 0; i < n; i++)
          {
               SignalEvent(aTarget[i], evDisarmed);
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief    Default handler for EngineConstants.EVENT_TYPE_TRAP_TRIGGER_DISARMED.
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventTriggerDisarmed(xEvent ev)
     {
          // If oDisarmedBy is valid the trap is being deactivated because it was disarmed.
          // Otherwise, the trap is being deactivated because it was triggered.
          GameObject oDisarmedBy = GetEventObjectRef(ref ev, 0);

          int nFlag = GetLocalInt(gameObject, EngineConstants.PLC_TRAP_DEACTIVATE);

          if (IsObjectValid(oDisarmedBy) != EngineConstants.FALSE)
          {
               if (nFlag == EngineConstants.PLC_TRAP_DESTROY_WHEN_DISARMED)
               {
                    Debug.Log("sys_traps_h: flag type & replaced with ==, double check");
                    SetPlot(gameObject, EngineConstants.FALSE);
                    Safe_Destroy_Object(gameObject);
               }
               else if (nFlag == EngineConstants.PLC_TRAP_SET_INACTIVE)
               {
                    Debug.Log("sys_traps_h: flag type & replaced with ==, double check");
                    SetObjectActive(gameObject, EngineConstants.FALSE);
               }
          }
          else
          {
               if (nFlag == EngineConstants.PLC_TRAP_DESTROY_WHEN_TRIGGERED)
               {
                    Debug.Log("sys_traps_h: flag type & replaced with ==, double check");
                    SetPlot(gameObject, EngineConstants.FALSE);
                    Safe_Destroy_Object(gameObject);
               }
               else if (nFlag == EngineConstants.PLC_TRAP_SET_INACTIVE)
               {
                    Debug.Log("sys_traps_h: flag type & replaced with ==, double check");
                    SetObjectActive(gameObject, EngineConstants.FALSE);
               }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief    Handles the 'arm trap' event.
     *           Will make the trap visible and play the 'trap placed' message
     *           Note: gameObject in this context is the trap trigger placeable.
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventArm(xEvent ev)
     {
          GameObject oOwner = GetEventObjectRef(ref ev, 0);

          SetObjectActive(gameObject, EngineConstants.TRUE);

          if (GetPlaceableStateCntTable(gameObject) == EngineConstants.PLC_STATE_CONTROLLER_TRAP_TRIGGGER)
               SetPlaceableState(gameObject, EngineConstants.PLC_STATE_TRAP_TRIGGER_ENABLED);

          // Flag player-created traps as detected (excluding glyph since it's a spell).
          if (Trap_IsPlayerCreated(gameObject) != EngineConstants.FALSE)
          {
               if (Trap_GetType(gameObject) != EngineConstants.SPELL_GLYPH_PARALYSIS)
               {
                    Trap_SetDetected(gameObject, IsPartyMember(oOwner));
                    UI_DisplayMessage(oOwner, EngineConstants.UI_MESSAGE_TRAP_PLACED);
               }
          }
          else
          {
               // If it was previously detected, mark it as detected again (in case trap is rearming)
               Trap_SetDetected(gameObject, Trap_GetDetected(gameObject));
               Trap_Highlight(gameObject, Trap_GetDetected(gameObject));
          }

          // Apply AOE xEffect to trap
          if (GetArraySize(GetEffects(gameObject, EngineConstants.EFFECT_TYPE_AOE, 0, gameObject)) == 0)
          {
               // Get ID of AOE volume trigger to spawn around the trap
               int nAOE = Trap_GetAOEID(gameObject);
               if (nAOE > 0)
               {
                    xEffect eAOE = EffectAreaOfEffect(nAOE, EngineConstants.RESOURCE_SCRIPT_PLACEABLE_CORE);
                    SetEffectCreatorRef(ref eAOE, gameObject);

                    // Get AOE visual effect
                    int nVfx = Trap_GetAOEVFX(gameObject);
                    if (nVfx > 0)
                    {
                         SetEffectEngineIntegerRef(ref eAOE, EngineConstants.EFFECT_INTEGER_VFX, nVfx);
                    }

                    // Create AOE volume trigger around the trap
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eAOE, gameObject, 0.0f, gameObject);
               }
          }

          // If trap has attached signal target(s), notify them that trap is ready.
          List<GameObject> aTarget = Trap_GetSignalTarget(gameObject);
          int i, n = GetArraySize(aTarget);
          xEvent eSignal = Event(EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ARMED);
          SetEventObjectRef(ref eSignal, 0, gameObject);

          for (i = 0; i < n; i++)
          {
               SignalEvent(aTarget[i], eSignal);
               //      Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventArm", "Signaling arm xEvent to: " + ToString(aTarget[i]));
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief    Informs trap's signal target(s) that the trap was triggered.
     *-----------------------------------------------------------------------------*/
     public void Trap_SignalEnterEvent(GameObject oTrap, xEvent ev)
     {
          // If trap has valid signal target(s), notify them that the trap was triggered
          // so they can apply the trap effects.
          GameObject oAOE = gameObject;
          List<GameObject> arTarget = Trap_GetSignalTarget(oTrap);
          int i, n = GetArraySize(arTarget);

          SetEventTypeRef(ref ev, EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ENTER);
          SetEventStringRef(ref ev, 0, StringLowerCase(GetTag(oAOE)));

          for (i = 0; i < n; i++)
          {
               SignalEvent(arTarget[i], ev);
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief    Alerts nearby team members that traps has been triggered or disarmed.
     *-----------------------------------------------------------------------------*/
     public void Trap_SignalTeam(GameObject oTrap)
     {
          int nTrapTeam = GetLocalInt(oTrap, EngineConstants.PLC_TRAP_TEAM);
          if (nTrapTeam > 0)
          {
               List<GameObject> arTeam = GetTeam(nTrapTeam);
               int i, n = GetArraySize(arTeam);
               for (i = 0; i < n; i++)
               {
                    SendEventApproachTrap(arTeam[i], oTrap);
               }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief    Signals trap to disarm itself.
     *-----------------------------------------------------------------------------*/
     public void Trap_SignalDisarmEvent(GameObject oTrap, GameObject oDisarmedBy = null, float fDelay = 0.0f)
     {
          xEvent evDisarm = Event(EngineConstants.EVENT_TYPE_TRAP_DISARM);
          SetEventObjectRef(ref evDisarm, 0, oDisarmedBy);

          if (fDelay > 0.0f)
               DelayEvent(fDelay, oTrap, evDisarm);
          else
               SignalEvent(oTrap, evDisarm);
     }

     /*-----------------------------------------------------------------------------
     * @brief    Signals trap signal target(s) to reset/retract after trap has been triggered.
     *-----------------------------------------------------------------------------*/
     public void Trap_SignalResetEvent(GameObject oTrap, GameObject oTarget)
     {
          float fResetOffset = Trap_GetResetDelay(oTrap);
          if (fabs(fResetOffset) > 0.01f)
          {
               float fBaseDelay = _Trap_GetEffectDuration(Trap_GetType(oTrap));
               float fAdjustedDelay = GetRankAdjustedEffectDuration(oTarget, fBaseDelay);
               float fResetDelay = fAdjustedDelay + fResetOffset;

               xEvent evReset = Event(EngineConstants.EVENT_TYPE_TRAP_RESET);
               SetEventCreatorRef(ref evReset, oTrap);
               List<GameObject> arTarget = Trap_GetSignalTarget(oTrap);
               int i, n = GetArraySize(arTarget);

               for (i = 0; i < n; i++)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_SignalResetEvent", "Reset " + ToString(arTarget[i]) + " in " + FloatToString(fResetDelay, 3, 1) + " seconds.", oTrap);
                    DelayEvent(fResetDelay, arTarget[i], evReset);
               }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief  Returns EngineConstants.TRUE if target is hostile to trap owner (or trap).
     *-----------------------------------------------------------------------------*/
     public int Trap_IsTargetValid(GameObject oTrap, GameObject oTarget)
     {
          GameObject oOwner = GetLocalObject(oTrap, EngineConstants.PLC_TRAP_OWNER);  // Object that created the trap.

          if (IsObjectValid(oOwner) != EngineConstants.FALSE)
          {
               if (IsObjectHostile(oOwner, oTarget) != EngineConstants.FALSE)
                    return EngineConstants.TRUE;

               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_IsTargetValid", "Invalid target: " + ToString(oTarget) + " is not hostile to " + ToString(oOwner));
               return EngineConstants.FALSE;
          }

          // Wisps don't trigger traps
          if (GetAppearanceType(oTarget) == EngineConstants.APPEARANCE_WISP)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_IsTargetValid", "Trap not triggered - target is a wisp");
               return EngineConstants.FALSE;
          }

          return IsPartyMember(oTarget);
     }

     /*-----------------------------------------------------------------------------
     * @brief  Enter xEvent handler triggered by each creature that enters trap's AOE.
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventEnter(xEvent ev)
     {
          GameObject oAOE = gameObject;
          GameObject oTarget = GetEventTargetRef(ref ev);                       // Creature that triggered the trap.
          GameObject oTrap = GetEventCreatorRef(ref ev);                      // The trap placeable.
          int nTrapType = GetLocalInt(oTrap, EngineConstants.PLC_TRAP_TYPE);

          // Don't trigger if trap is disarmed.
          if (Trap_IsArmed(oTrap) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventEnter", "Discarding OnEnter xEvent - " + ToString(oTrap) + " is disarmed.");
               return;
          }

          // Don't trigger if target is invalid.
          if (Trap_IsTargetValid(oTrap, oTarget) == EngineConstants.FALSE)
               return;

          // Verify xEvent received from associated AOE effect.
          if (GetTag(oAOE) != Trap_GetAOETag(oTrap))
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventEnter", "Discarding OnEnter xEvent - not generated by trap's AoE ('" + GetTag(oAOE) + "' vs '" + Trap_GetAOETag(oTrap) + "')");
               return;
          }

          // Don't trigger if player is attempting to disarm this trap
          xCommand cCurrent = GetCurrentCommand(oTarget);
          if (GetCommandType(cCurrent) == EngineConstants.COMMAND_TYPE_USE_OBJECT && GetCommandObjectRef(ref cCurrent) == oTrap)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventEnter", "Discarding OnEnter xEvent - player attempting to disarm trap: " + GetTag(oTrap));
               return;
          }

          // Don't trigger if a follower trips the trap while the player is attempting to disarm it.
          cCurrent = GetCurrentCommand(GetMainControlled());
          if (IsFollower(oTarget) != EngineConstants.FALSE && GetCommandType(cCurrent) == EngineConstants.COMMAND_TYPE_USE_OBJECT && GetCommandObjectRef(ref cCurrent) == oTrap)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventEnter", "Discarding OnEnter xEvent - player attempting to disarm trap: " + GetTag(oTrap));
               return;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventEnter", "Triggered trap " + ToString(oTrap) + ", Owner:" + ToString(GetLocalObject(oTrap, EngineConstants.PLC_TRAP_OWNER)) + ", Target:" + ToString(oTarget));

          // Reveal the trap
          List<xEffect> eTrans = GetEffects(oTrap, EngineConstants.EFFECT_TYPE_ALPHA, 0, oTrap, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT);
          RemoveEffectArray(oTrap, eTrans);

          // Play visual effect.
          Trap_PlayTriggerVFX(oTrap, oTarget);

          // Notify target they triggered a trap.
          if (nTrapType != EngineConstants.TRAP_NATURAL_WEB)
          {
               UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_TRAP_TRIGGERED);
          }

          // Notify signal targets trap was triggered.
          Trap_SignalEnterEvent(oTrap, ev);

          // Alert nearby creatures.
          Trap_SignalTeam(oTrap);

          // Deactivate the trap after it's been triggered.
          Trap_SignalDisarmEvent(oTrap, null, 0.1f); // slight delay so vfx play properly

          // Schedule trap to reset/retract after it has been triggered.
          Trap_SignalResetEvent(oTrap, oTarget);

          // Schedule reactivation if trap has a rearm delay.
          float fRearm = Trap_GetRearmDelay(oTrap);
          if (fRearm > 0.01f)
          {
               Trap_ArmTrap(oTrap, Trap_GetOwner(oTrap), fRearm);
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief    Exit xEvent handler triggered for each target that leaves the trap's AOE.
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventExit(xEvent ev)
     {
          /*
              GameObject oTarget    = GetEventTargetRef(ref ev);
              GameObject oTrap      = GetEventCreatorRef(ref ev);
              List<GameObject> arTarget = Trap_GetSignalTarget(oTrap);
              int i, n          = GetArraySize(arTarget);

              xEvent eSignal = SetEventTypeRef(ref ev, EngineConstants.EVENT_TYPE_TRAP_TRIGGER_EXIT);
              eSignal = SetEventIntegerRef(ref ev, 0, Trap_GetType(oTrap));

              for (i = 0; i < n; i++)
              {
                  Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventExit", "Signaling target " + ToString(i+1) + " of " + ToString(n));
                  SignalEvent(arTarget[i], eSignal);
              }
          */
     }

     public void Trap_HandleEventTriggerExit(xEvent ev)
     {
     }

     /*-----------------------------------------------------------------------------
     * @brief Event handler sent to a trap's signal target(s) when the trap is armed.
     *
     * gameObject in this context is the trap's signal target(s).
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventTriggerArmed(xEvent ev)
     {
          GameObject oTrap = GetEventObjectRef(ref ev, 0);

          switch (Trap_GetType(oTrap))
          {
               case EngineConstants.TRAP_NATURAL_WEB:
                    {
                         xEffect eVFX = EffectVisualEffect(5019);
                         Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eVFX, GetLocation(oTrap));
                         break;
                    }
               case EngineConstants.TRAP_LEGHOLD_WOOD:
               case EngineConstants.TRAP_LEGHOLD_STEEL:
                    {
                         if (GetPlaceableStateCntTable(oTrap) == EngineConstants.PLC_STATE_CNT_TRAP_TRIGGER)
                              SetPlaceableState(oTrap, EngineConstants.PLC_STATE_TRAP_TRIGGER_ENABLED);
                         break;
                    }
               case EngineConstants.TRAP_SCYTHING_BLADE:
               case EngineConstants.TRAP_PIERCING_BLADE:
                    {
                         if (GetPlaceableStateCntTable(gameObject) == EngineConstants.PLC_STATE_CONTROLLER_TRAP_NONSELECTABLE)
                              SetPlaceableState(gameObject, EngineConstants.PLC_STATE_TRAP_NONSELECTABLE_IDLE);
                         break;
                    }
               default:
                    {
                         // Trip wires and pressure plates
                         if (GetPlaceableStateCntTable(oTrap) == EngineConstants.PLC_STATE_CNT_TRAP_TRIGGER)
                              SetPlaceableState(oTrap, EngineConstants.PLC_STATE_TRAP_TRIGGER_ENABLED);
                         break;
                    }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Event handler sent to a trap's signal target(s) when the trap is reset.
     *
     * gameObject in this context is the trap's signal target(s).
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventReset(xEvent ev)
     {
          GameObject oTrap = GetEventCreatorRef(ref ev);

          switch (Trap_GetType(oTrap))
          {
               case EngineConstants.TRAP_LEGHOLD_WOOD:
               case EngineConstants.TRAP_LEGHOLD_STEEL:
                    {
                         // Open trap to 'release' creature caught inside
                         if (GetPlaceableStateCntTable(oTrap) == EngineConstants.PLC_STATE_CNT_TRAP_TRIGGER)
                              SetPlaceableState(oTrap, EngineConstants.PLC_STATE_TRAP_TRIGGER_ENABLED);

                         // If not a one-shot trap, set it visible until it rearms.
                         if (Trap_GetRearmDelay(oTrap) > 0.01f)
                              Trap_Highlight(gameObject, EngineConstants.TRUE);
                         break;
                    }
               case EngineConstants.TRAP_SCYTHING_BLADE:
               case EngineConstants.TRAP_PIERCING_BLADE:
                    {
                         if (GetPlaceableStateCntTable(gameObject) == EngineConstants.PLC_STATE_CONTROLLER_TRAP_NONSELECTABLE)
                              SetPlaceableState(gameObject, EngineConstants.PLC_STATE_TRAP_NONSELECTABLE_IDLE);
                         break;
                    }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Randomizes a missile's target vector.
     *-----------------------------------------------------------------------------*/
     public Vector3 RandomSpread(Vector3 vBase, float fMaxSpread)
     {
          float x = vBase.x + ((RandomFloat() - 0.5f) * fMaxSpread);
          float y = vBase.y + ((RandomFloat() - 0.5f) * fMaxSpread);
          float z = vBase.z + ((RandomFloat() - 0.5f) * fMaxSpread * 0.5f);   // less variation in height
          return Vector(x, x, z);
     }

     /*-----------------------------------------------------------------------------
     * @brief Default handler for EngineConstants.EVENT_TYPE_TRAP_TRIGGERED event.
     *
     * Handles the EngineConstants.EVENT_TYPE_TRAP_TRIGGERED xEvent sent by the engine when a placeable
     * trap collides with something (eg. swinging log trap strikes a creature).
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventTriggered(xEvent ev)
     {
          GameObject oTarget = GetEventObjectRef(ref ev, 0);       // Target hit by trap
          Vector3 lImpact = GetEventVectorRef(ref ev, 0);     // Impact location

          switch (GetAppearanceType(gameObject))
          {
               case EngineConstants.PLC_PIERCING_BLADE:
                    Trap_Triggered(EngineConstants.TRAP_PIERCING_BLADE, oTarget, lImpact);
                    break;
               case EngineConstants.PLC_SCYTHING_BLADE:
                    Trap_Triggered(EngineConstants.TRAP_SCYTHING_BLADE, oTarget, lImpact);
                    break;
               default:
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventTriggered", ToString(GetAppearanceType(gameObject)) + " *** Unhandled placeable appearance ***");
                    break;
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Default handler for EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ENTER event.
     *
     * Handles the EngineConstants.EVENT_TYPE_TRAP_TRIGGER_ENTER xEvent sent to a trap's signal target(s)
     * when the trap is triggered.
     *
     * gameObject in this context is the trap's signal target(s).
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleEventTriggerEntered(xEvent ev)
     {
          GameObject oTarget = GetEventTargetRef(ref ev);        // Object that triggered the trap
          GameObject oTrap = GetEventCreatorRef(ref ev);       // Trap placeable
          string sAOE = GetEventStringRef(ref ev, 0);     // Lower case tag of AOE trigger associated witht the trap
          int nTrapType = Trap_GetType(oTrap);
          int i, n;

          switch (nTrapType)
          {
               case EngineConstants.TRAP_SCYTHING_BLADE:
               case EngineConstants.TRAP_PIERCING_BLADE:
                    {
                         // Reveal the signal target if hidden
                         RemoveEffectArray(gameObject, GetEffects(gameObject, EngineConstants.EFFECT_TYPE_ALPHA, 0, gameObject, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT));

                         // Activate the placeable (log swings down, blades swing out, etc).
                         // EngineConstants.EVENT_TYPE_TRAP_TRIGGERED xEvent is fired if it actually hits something/someone.
                         SetPlaceableState(gameObject, EngineConstants.PLC_STATE_TRAP_NONSELECTABLE_ACTIVE);
                         break;
                    }
               case EngineConstants.SPELL_GLYPH_PARALYSIS:
                    {
                         // Single shot
                         if (GetLocalInt(oTrap, EngineConstants.PLC_DO_ONCE_B) == EngineConstants.FALSE)
                         {
                              SetLocalInt(oTrap, EngineConstants.PLC_DO_ONCE_B, EngineConstants.TRUE);

                              if (ResistanceCheck(Trap_GetOwner(oTrap), oTarget, EngineConstants.PROPERTY_ATTRIBUTE_SPELLPOWER, EngineConstants.RESISTANCE_PHYSICAL) == EngineConstants.FALSE)
                              {
                                   float fDuration = GetRankAdjustedEffectDuration(oTarget, 10.0f);
                                   xEffect eParalyze = EffectParalyze(Ability_GetImpactObjectVfxId(EngineConstants.ABILITY_SPELL_GLYPH_OF_PARALYSIS));
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eParalyze, oTarget, fDuration, oTrap, EngineConstants.ABILITY_SPELL_GLYPH_OF_PARALYSIS);
                                   DelayEvent(fDuration, oTrap, Event(EngineConstants.EVENT_TYPE_DESTROY_OBJECT));
                              }
                              else
                              {
                                   DelayEvent(1.0f, oTrap, Event(EngineConstants.EVENT_TYPE_DESTROY_OBJECT));
                              }
                         }
                         break;
                    }
               case EngineConstants.TRAP_FIREBALL:
               case EngineConstants.TRAP_PROJECTILE_BIGBOLT:
               case EngineConstants.TRAP_PROJECTILE_ARROW:
               case EngineConstants.TRAP_PROJECTILE_ARROWS:
               case EngineConstants.TRAP_PROJECTILE_ARROW_ICE:
               case EngineConstants.TRAP_PROJECTILE_ARROW_FIRE:
               case EngineConstants.TRAP_PROJECTILE_ARROW_POISON:
               case EngineConstants.TRAP_PROJECTILE_FIRE:
               case EngineConstants.TRAP_PROJECTILE_ICE:
               case EngineConstants.TRAP_PROJECTILE_LIGHTNING:
               case EngineConstants.TRAP_PROJECTILE_WEB:
                    {
                         int nProjectile = Trap_GetProjectileType(oTrap);
                         n = Trap_GetProjectileCount(oTrap);
                         int nCrustVFX = Trap_GetProjectileCrustVFX(oTrap);

                         Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleEventTriggerEntered",
                             "Trap Type: " + ToString(nTrapType) + ", Target: " + ToString(oTarget) + ", Projectile ID: " + ToString(nProjectile) + " x " + ToString(n) + ", CrustVFX: " + ToString(nCrustVFX));

                         if (nProjectile != EngineConstants.FALSE)
                         {
                              // Spread out projectiles fired in volley.
                              float fMaxSpread = 0.15f * IntToFloat(n);
                              if (fMaxSpread > 0.75f)
                                   fMaxSpread = 0.75f;

                              Vector3 vSource = GetPosition(gameObject);
                              Vector3 vTarget = GetPosition(oTarget);
                              vSource.z += GetLocalFloat(gameObject, EngineConstants.PLC_TRAP_SIGNAL_HEIGHT);
                              vTarget.z += 0.75f * GetHeight(oTarget); // Aim chest high

                              for (i = 0; i < n; i++)
                              {
                                   //                  FireProjectile(nProjectile, vSource, RandomSpread(vTarget, fMaxSpread), nCrustVFX, EngineConstants.FALSE, oTrap);
                                   FireProjectile(nProjectile, vSource, vTarget, nCrustVFX, EngineConstants.FALSE, oTrap);
                              }
                         }
                         else
                         {
                              Warning("Invalid projectile specified for trap " + ToString(oTrap));
                         }

                         break;
                    }
               case EngineConstants.TRAP_NATURAL_WEB:
                    {
                         float fRand = RandFF(3.0f, 1.0f);
                         float fDuration = GetRankAdjustedEffectDuration(oTarget, fRand);
                         ApplyEffectVisualEffect(oTarget, oTarget, 1097, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, fRand, 0);
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectParalyze(), oTarget, fDuration, null, 0);
                         break;
                    }
               case EngineConstants.TRAP_EXPLOSION:
               case EngineConstants.TRAP_EXPLODING_BARREL:
                    {
                         Trap_Triggered(nTrapType, oTarget, GetLocation(gameObject), oTrap);
                         break;
                    }
               case EngineConstants.TRAP_GAS_TOXIC:
                    {
                         Trap_Triggered(nTrapType, oTarget, GetLocation(oTrap), oTrap);
                         break;
                    }
               case EngineConstants.TRAP_GREASE:
               case EngineConstants.TRAP_LEGHOLD_WOOD:
               case EngineConstants.TRAP_LEGHOLD_STEEL:
               default:
                    {
                         Trap_Triggered(nTrapType, oTarget, GetLocation(oTarget), oTrap);
                         break;
                    }
          }
     }

     /*-----------------------------------------------------------------------------
     * @brief Default handler for a trap's EngineConstants.EVENT_TYPE_ATTACK_IMPACT event.
     *
     * Handles the EngineConstants.EVENT_TYPE_ATTACK_IMPACT xEvent sent to a trap when a projectile
     * hits a target.
     *-----------------------------------------------------------------------------*/
     public void Trap_HandleImpact(xEvent ev)
     {
          int nTrapType = Trap_GetType(gameObject);
          GameObject oCreator = GetEventCreatorRef(ref ev);     // the projectile object
          Vector3 lImpact = GetLocation(oCreator);

          // HACK: The area component of lImpact is always invalid.
          lImpact = Location(GetArea(gameObject), GetPositionFromLocation(lImpact), GetFacingFromLocation(lImpact));

          List<GameObject> arTarget = new List<GameObject>();
          int i;
          /*for (i = 1; IsObjectValid(GetEventObjectRef(ref ev, i)); i++)
          {
              arTarget[i-1] = GetEventObjectRef(ref ev, i);
          }*/
          for (i = 1; i < ev.oList.Count; i++)
          {
               GameObject oObject = ev.oList.ElementAt(i);
               if (IsObjectValid(oObject) != EngineConstants.FALSE)
               {
                    arTarget.Add(oObject);
               }
          }
          int nTargets = GetArraySize(arTarget);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS_TRAPS, "sys_traps_h.Trap_HandleImpact",
              "Trap Type: " + ToString(nTrapType) + ", Targets:" + ToString(nTargets) + ", vImpact: " + VectorToString(GetPositionFromLocation(lImpact)));

          switch (nTrapType)
          {
               case EngineConstants.TRAP_FIREBALL:
                    {
                         SendComboEventAoE(EngineConstants.COMBO_EVENT_IGNITE, EngineConstants.SHAPE_SPHERE, lImpact, gameObject, 8.0f);
                         Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(1002), lImpact, 0.0f, gameObject, 0);
                         Trap_Triggered(EngineConstants.TRAP_FIREBALL_EFFECT, null, lImpact, gameObject);
                         break;
                    }
               default:
                    {
                         for (i = 0; i < nTargets; i++)
                         {
                              Trap_Triggered(nTrapType, arTarget[i], lImpact, gameObject);
                         }
                         break;
                    }
          }
     }
}