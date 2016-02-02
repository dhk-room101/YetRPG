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
     // combat_h - Combat related functions
     // -----------------------------------------------------------------------------
     /*
         This file holds the combat resolution logic for the gtacticame.

         Item specific functions are included from items_h
         Damage specific functions (such as attack damage calculations and resists)
          are included from combat_damage_h

         core game string - any change to this file has the potential to wreck
         the combat system, handle with care.

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"effects_h"
     //#include"items_h"
     //#include"combat_damage_h"
     //#include"ui_h"
     //#include"sys_soundset_h"
     //#include"ai_threat_h"
     //#include"2da_constants_h"

     //#include"stats_core_h"

     // -----------------------------------------------------------------------------
     //
     //             L I M I T E D   E D I T   P E R M I S S I O N S
     //
     //      If you are not Georg, you need permission to edit this file
     //      and the changes have to be code reviewed before they are checked
     //      in.
     //
     // -----------------------------------------------------------------------------

     //moved public const float EngineConstants.ATTACK_LOOP_DURATION_INVALID = 999.0f;
     //moved public const float EngineConstants.ATTACK_HIT_BIAS = 4.0f; // General bias in the system towards hits instead of misses.
     //moved public const int EngineConstants.ATTACK_TYPE_MELEE = 1;
     //moved public const int EngineConstants.ATTACK_TYPE_RANGED = 2;

     //moved public const float SPECIAL_BOSS_DEATHBLOW_THRESHOLD = 0.04;        // at this % of health, any meelee attack may trigger the deathblow of special bosses;

     // -----------------------------------------------------------------------------
     // Point blank range (no penalty range for bows)
     // -----------------------------------------------------------------------------
     //moved public const float POINT_BLANK_RANGE = 8.0f;

     /*
    * @brief Determine which hand to use for an attack
    *
    * Only applicable in dual weapon style. Returns which hand to use for the next attack
    *
    * @returns  0 - main hand, 1 - offhand
    *
    * @author Georg Zoeller
    *
    **/

     public int Combat_GetAttackHand(GameObject oCreature = null)
     {
          if (oCreature == null) oCreature = gameObject;
          int nHand = 0;
          if (GetWeaponStyle(oCreature) == EngineConstants.WEAPONSTYLE_DUAL)
          {
               nHand = GetLocalInt(oCreature, EngineConstants.COMBAT_LAST_WEAPON);
               SetLocalInt(oCreature, EngineConstants.COMBAT_LAST_WEAPON,
                    (nHand + 1 == 1) ? EngineConstants.TRUE : EngineConstants.FALSE);
               //the above expression is a workaround for "the other hand" :-) DHK
          }

          return nHand;
     }

     /*
     * @brief Check if a deathblow should occur.
     *
     * Examine a number of parameters to see whether or not we can play a deathblow
     * without interrupting the flow of combat.
     *
     * @author Georg Zoeller
     **/

     public int CheckForDeathblow(GameObject oAttacker, GameObject oTarget)
     {
          int nRank = GetCreatureRank(oTarget);
          int nLevel = GetLevel(oAttacker);
          float fChance = GetM2DAFloat(EngineConstants.TABLE_CREATURERANKS, "fDeathblow", nRank);

          // -------------------------------------------------------------------------
          // Any rank flagged 1.0f or higher triggers a deathblow.
          // -------------------------------------------------------------------------
          if (fChance >= 1.0f)
          {
               return EngineConstants.TRUE;
          }

          // -------------------------------------------------------------------------
          // If we perceive more than 1 creature, then half the chance of triggering
          // a deathblow with each perceived hostile.
          // -------------------------------------------------------------------------
          int nCount = GetArraySize(GetPerceivedCreatureList(oAttacker, EngineConstants.TRUE));
          if (nCount > 1)
          {
               fChance *= (2.0f / IntToFloat(nCount));
          }
          //Increase chance of death blow in origin stories (level 1 and 2)
          //1.5f times the chance
          if (nLevel < 3)
          {
               fChance *= 1.5f;
          }
          return (RandomFloat() < fChance) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     /*
     * @brief Determine a valid deathblow for the current target
     *
     * Called when the script decides to do a deathblow, this function will return
     * a DEATHBLOW_* constant representing the deathblow to use against the creature
     *
     * If the creature does not support deathblows (immunity, no animation) or can not
     * be deathblowed (Immortal, Plot, Party Member, Flagged as invalid in the toolset)
     * the function returns 0
     *
     * @param oTarget    The target for the Deathblow
     *
     * @returns  0 or DEATH_BLOW_*
     *
     * @author Georg Zoeller
     *
     **/
     public int Combat_GetValidDeathblow(GameObject oAttacker, GameObject oTarget)
     {

          // ------------------------------------------------------------------------
          // First, let's check if we can even perform deathblows with the attacker
          // ------------------------------------------------------------------------
          int bCanDeathblow = CanPerformDeathblows(oAttacker);

          if (bCanDeathblow == EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          int nValid = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "ValidDeathblows", GetAppearanceType(oTarget));

          int bImmortal = IsImmortal(oTarget);
          int bPlot = IsPlot(oTarget);
          int bCanDiePermanently = GetCanDiePermanently(oTarget);
          int bAlreadyDead = HasDeathEffect(oTarget);

#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DEATH, "GetValidDeathblow", "nValid: " + ToString(nValid) + " bImmortal: " + ToString(bImmortal) + " bPlot:" +
                                     ToString(bPlot) + " bCanDiePermanently:" + ToString(bCanDiePermanently));

#endif

          // ------------------------------------------------------------------------
          // If we are immortal, Plot, or can not die permanently (e.g. a party member),
          // we return 0
          // ------------------------------------------------------------------------
          if (bImmortal != EngineConstants.FALSE || bPlot != EngineConstants.FALSE || bCanDiePermanently == EngineConstants.FALSE || IsPartyMember(oTarget) != EngineConstants.FALSE || bAlreadyDead != EngineConstants.FALSE)
          {
               return 0;
          }

          return 1;
     }

     // -----------------------------------------------------------------------------
     // Return the type of the current attack based on the weapon in the main hand
     // used only in command_pending...
     // -----------------------------------------------------------------------------
     public int Combat_GetAttackType(GameObject oAttacker, GameObject oWeapon)
     {
          if (IsUsingRangedWeapon(oAttacker, oWeapon) != EngineConstants.FALSE)
          {
               return EngineConstants.ATTACK_TYPE_RANGED;
          }
          else
          {
               return EngineConstants.ATTACK_TYPE_MELEE;
          }
     }

     // -----------------------------------------------------------------------------
     // Attack Result struct, used by Combat_PerformAttack*
     // -----------------------------------------------------------------------------
     public struct CombatAttackResultStruct
     {

          public int nAttackResult;      //  - EngineConstants.COMBAT_RESULT_* constant
          public int nDeathblowType;
          public float fAttackDuration;   //  - Duration of the aim loop for ranged weapons
          public xEffect eImpactEffect;       //  - Impact Effect
     };

     public float Combat_GetFlankingBonus(GameObject oAttacker, GameObject oTarget)
     {

          if (HasAbility(oTarget, EngineConstants.ABILITY_TALENT_SHIELD_TACTICS) != EngineConstants.FALSE)
          {
               if (IsUsingShield(oTarget) != EngineConstants.FALSE)
                    return 0.0f;
          }

          if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_FLANK_IMMUNITY) != EngineConstants.FALSE)
          {
               return 0.0f;
          }

          float fAngle = GetAngleBetweenObjects(oTarget, oAttacker);
          float fFactor = 0.0f;
          float fMaxModifier = 15.0f;

          // The attackers ability to flank is stored in a creature property
          float fFlankingAngle = GetCreatureProperty(oAttacker, EngineConstants.PROPERTY_ATTRIBUTE_FLANKING_ANGLE);

          if (fFlankingAngle <= 10.0f) /*old savegames have this at 10*/
          {
               fFlankingAngle = 60.0f; // old savegames need this to avoid divby0 later
          }
          else if (fFlankingAngle > 180.0f)
          {
               fFlankingAngle = 180.0f;
          }

          // -------------------------------------------------------------------------
          if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_COMBAT_MOVEMENT) != EngineConstants.FALSE)
          {
               fMaxModifier = 20.0f;
          }

          if (fMaxModifier <= 0.0f)
          {
               return 0.0f;
          }

          if ((fAngle >= (180.0f - fFlankingAngle) && fAngle <= (180.0f + fFlankingAngle)))
          {
               // Shield block negats flanking on the left.

               int bShieldBlock = HasAbility(oTarget, EngineConstants.ABILITY_TALENT_SHIELD_BLOCK);
               int bUsingShield = IsUsingShield(oTarget);

               if (bShieldBlock == EngineConstants.FALSE || fAngle < 180.0f || (bShieldBlock != EngineConstants.FALSE && bUsingShield == EngineConstants.FALSE))
               {
                    fFactor = (fFlankingAngle - fabs(180.0f - fAngle)) / fFlankingAngle;
               }

          }

          // Only rogues get the full positional benefits on the battlefield,
          // everyone else gets halfa
          float fClassModifier = GetCreatureCoreClass(oAttacker) == EngineConstants.CLASS_ROGUE ? 1.0f : 0.5f;

          return fFactor * fMaxModifier * fClassModifier;
     }

     // -----------------------------------------------------------------------------
     // Check if backstab conditions are true
     // -----------------------------------------------------------------------------
     public int Combat_CheckBackstab(GameObject oAttacker, GameObject oTarget, GameObject oWeapon, float fFlankingBonus)
     {

          // -------------------------------------------------------------------------
          // If we we are a rogue
          // -------------------------------------------------------------------------
          if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_HIDDEN_ROGUE) == EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          if (IsHumanoid(oAttacker) == EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // And target is not immune
          // -------------------------------------------------------------------------
          if (HasAbility(oTarget, EngineConstants.ABILITY_TRAIT_CRITICAL_HIT_IMMUNITY) != EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // And attacker does not use double strike mode
          // -------------------------------------------------------------------------
          if (IsModalAbilityActive(oAttacker, EngineConstants.ABILITY_TALENT_DUAL_WEAPON_DOUBLE_STRIKE) != EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          // -------------------------------------------------------------------------
          // We can only backstab if we are flanking.
          // -------------------------------------------------------------------------
          if (fFlankingBonus > 0.0f)
          {
               return EngineConstants.TRUE;
          }
          else
          {
               /* Coup de grace*/
               if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_BACKSTAB) != EngineConstants.FALSE)
               {
                    if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_STUN) != EngineConstants.FALSE)
                    {
                         return EngineConstants.TRUE;
                    }
                    else if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_PARALYZE) != EngineConstants.FALSE)
                    {
                         return EngineConstants.TRUE;
                    }
               }

          }

          return EngineConstants.FALSE;
     }

     /*
     * @brief Determine whether or not an attack hits a target.
     *
     * Note that this function only calculates to hit and crits, death blows are
     * determined in Combat_PerformAttack
     *
     * @param oAttacker  The attacker
     * @param oTarget    The target that is being attacked
     * @param nAbility   If != 0, it won't trigger backstabs and deathblows.
     *
     * @returns  EngineConstants.COMBAT_RESULT_HIT, EngineConstants.COMBAT_RESULT_CRITICALHIT or EngineConstants.COMBAT_RESULT_MISS
     *
     * @author Georg Zoeller
     *
     **/
     public int Combat_GetAttackResult(GameObject oAttacker, GameObject oTarget, GameObject oWeapon, float fBonus = 0.0f, int nAbility = 0)
     {

          // -------------------------------------------------------------------------
          // Debug
          // -------------------------------------------------------------------------
          int nForcedCombatResult = GetForcedCombatResult(oAttacker);
          if (nForcedCombatResult != -1)
          {
#if DEBUG
               Log_Trace_Combat("combat_h.GetAttackResult", " Skipped rules, FORCED RESULT IS:" + ToString(nForcedCombatResult), oTarget);
#endif
               return nForcedCombatResult;
          }

          // -------------------------------------------------------------------------
          // Placeables are always hit
          // -------------------------------------------------------------------------
          if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_PLACEABLE)
          {
#if DEBUG
               Log_Trace_Combat("combat_h.GetAttackResult", " Placeable, automatic result : EngineConstants.COMBAT_RESULT_HIT", oTarget);
#endif
               return EngineConstants.COMBAT_RESULT_HIT;
          }

          // -------------------------------------------------------------------------
          // Displacement
          // -------------------------------------------------------------------------
          float fDisplace = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_DISPLACEMENT);
          float fRand = RandomFloat() * 100.0f;
          if (fRand < fDisplace)
          {
#if DEBUG
               Log_Trace_Combat("combat_h.GetAttackResult", " Displacement xEffect kciked in, automatic result : EngineConstants.COMBAT_RESULT_MISS", oTarget);
#endif

               // ---------------------------------------------------------------------
               // if the target has the evasion talent, attribute this miss to the talent
               // (random 50% because the anim is interrupting)
               // ---------------------------------------------------------------------
               if (HasAbility(oTarget, EngineConstants.ABILITY_TALENT_EVASION) != EngineConstants.FALSE && fRand < (20.0f - (RandomFloat() * 10.0f)))
               {
                    xCommand currentCmd = GetCurrentCommand(oTarget);
                    int nCmdType = GetCommandType(currentCmd);
                    // Evasion only plays during attack and wait commands.
                    if (nCmdType == EngineConstants.COMMAND_TYPE_WAIT || nCmdType == EngineConstants.COMMAND_TYPE_ATTACK || nCmdType == EngineConstants.COMMAND_TYPE_INVALID)
                    {
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, Effect(1055), oTarget, 0.0f, oTarget, EngineConstants.ABILITY_TALENT_EVASION);
                    }
               }

               return EngineConstants.COMBAT_RESULT_MISS;
          }

          int nAttackType = Combat_GetAttackType(oAttacker, oWeapon);
          int nRet;

          // -------------------------------------------------------------------------
          // Get the attackers attack rating (includes effects and equipment stats)
          // -------------------------------------------------------------------------
          float fAttackRating;

          if (GetBaseItemType(oWeapon) == EngineConstants.BASE_ITEM_TYPE_STAFF)
          {
               // ---------------------------------------------------------------------
               // Staves always hit
               // ---------------------------------------------------------------------
               return EngineConstants.COMBAT_RESULT_HIT;
          }

          fAttackRating = GetCreatureAttackRating(oAttacker);

          // -------------------------------------------------------------------------
          // Add item stat (usually 0) along with scripted boni and attack bias.
          // -------------------------------------------------------------------------

          fAttackRating += GetItemStat(oWeapon, EngineConstants.ITEM_STAT_ATTACK) + fBonus + EngineConstants.ATTACK_HIT_BIAS;

          // -------------------------------------------------------------------------
          // Easier difficulties grant the player a bonus.
          // -------------------------------------------------------------------------
          fAttackRating += Diff_GetRulesAttackBonus(oAttacker);

          // -------------------------------------------------------------------------
          // This section deals with figuring out which critical hit modifier (melee, ranged, etc)
          // to use for this attack.
          // -------------------------------------------------------------------------
          float fCriticalHitModifier;
          int nCriticalHitModifier = (nAttackType == EngineConstants.ATTACK_TYPE_RANGED) ? EngineConstants.CRITICAL_MODIFIER_RANGED : EngineConstants.CRITICAL_MODIFIER_MELEE;

          fCriticalHitModifier = GetCreatureCriticalHitModifier(oAttacker, nCriticalHitModifier);
          fCriticalHitModifier += GetItemStat(oWeapon, EngineConstants.ITEM_STAT_CRIT_CHANCE_MODIFIER);

          // -------------------------------------------------------------------------
          //  Bravery grants +3.5f critical hit per enemy past the first 2
          // -------------------------------------------------------------------------
          if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_BRAVERY) != EngineConstants.FALSE)
          {

               int nEnemies = Max(0, GetArraySize(GetCreaturesInMeleeRing(oAttacker, 0.0f, 359.99f, EngineConstants.TRUE, 0)) - 2);
               fCriticalHitModifier += nEnemies * 3.5f;
          }

          // -------------------------------------------------------------------------
          // Calculate Flanking Bonus
          // -------------------------------------------------------------------------
          float fFlanking = Combat_GetFlankingBonus(oAttacker, oTarget);
          if (fFlanking > 0.0f)
          {
               // ---------------------------------------------------------------------
               // Also increase chance for critical hits by 1/5th of the flanking bonus
               // ---------------------------------------------------------------------
               fCriticalHitModifier *= (1.0f + (fFlanking / 5.0f));
          }

          // -------------------------------------------------------------------------
          // Range plays a role too.
          // -------------------------------------------------------------------------
          float fDistance = GetDistanceBetween(oAttacker, oTarget);
          float fNoPenaltyDistance = MaxF(EngineConstants.POINT_BLANK_RANGE, GetItemStat(oWeapon, EngineConstants.ITEM_STAT_OPTIMUM_RANGE));
          fDistance = MaxF(fDistance - fNoPenaltyDistance, 0.0f);

          float fAttackRoll = 50.0f;
          float fPenalties = fDistance; // every meter distance past the free range is -1!
          float fAttack = fAttackRating + fAttackRoll + fFlanking - fPenalties;

          // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
          // BEGIN SECTION EngineConstants.CRITICAL HITS
          // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

          int bThreatenCritical = (RandomFloat() * 100.0f < fCriticalHitModifier) ? EngineConstants.TRUE : EngineConstants.FALSE;

          if (bThreatenCritical == EngineConstants.FALSE)
          {
               // ---------------------------------------------------------------------
               // Attacking out of stealth always threatens crit.
               // ---------------------------------------------------------------------
               if (IsModalAbilityActive(oAttacker, EngineConstants.ABILITY_SKILL_STEALTH_1) != EngineConstants.FALSE)
               {
                    bThreatenCritical = EngineConstants.TRUE;
               }

               // -----------------------------------------------------------------
               // Death hex xEffect ... all hits are auto crit
               // -----------------------------------------------------------------
               if (GetHasEffects(oTarget, EngineConstants.EFFECT_TYPE_DEATH_HEX) != EngineConstants.FALSE)
               {
                    bThreatenCritical = EngineConstants.TRUE;
               }

               // -----------------------------------------------------------------
               // Autocrit effect
               // -----------------------------------------------------------------
               if (GetHasEffects(oAttacker, EngineConstants.EFFECT_TYPE_AUTOCRIT) != EngineConstants.FALSE)
               {
                    bThreatenCritical = EngineConstants.TRUE;
               }
          }
          else
          {
               // ---------------------------------------------------------------------
               // Double strike does not allow crits
               // ---------------------------------------------------------------------
               if (IsModalAbilityActive(oAttacker, EngineConstants.ABILITY_TALENT_DUAL_WEAPON_DOUBLE_STRIKE) != EngineConstants.FALSE)
               {
                    bThreatenCritical = EngineConstants.FALSE;
               }

               // rapid shot doesn't allow critical strikes
               if (IsModalAbilityActive(oAttacker, EngineConstants.ABILITY_TALENT_RAPIDSHOT) != EngineConstants.FALSE)
               {
                    bThreatenCritical = EngineConstants.FALSE;
               }
          }

          // ---------------------------------------------------------------------
          // Targets that have critical hit immunity can not be crit...
          // ---------------------------------------------------------------------
          if (bThreatenCritical != EngineConstants.FALSE)
          {
               Log_Trace_Combat("combat_h.GetAttackResult", " Critical hit averted, target has critical hit immunity", oTarget);
               bThreatenCritical = HasAbility(oTarget, EngineConstants.ABILITY_TRAIT_CRITICAL_HIT_IMMUNITY) == EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE;
          }

          // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
          // END SECTION EngineConstants.CRITICAL HITS
          // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

          // -------------------------------------------------------------------------
          // This section deals with calculating the defense values of the attack target
          // -------------------------------------------------------------------------
          float fDefenseRating = GetCreatureDefense(oTarget) + ((nAttackType == EngineConstants.ATTACK_TYPE_RANGED) ? GetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_MISSILE_SHIELD) : 0.0f);

          // -------------------------------------------------------------------------
          // Easier difficulties grant the player a bonus.
          // -------------------------------------------------------------------------
          fDefenseRating += Diff_GetRulesDefenseBonus(oTarget);

          // -------------------------------------------------------------------------
          // A hit is successful if the attack rating exceeds the defense rating
          // -------------------------------------------------------------------------
          if (RandomFloat() * 100.0f < fAttack - fDefenseRating)
          {

               // ---------------------------------------------------------------------
               // If we threatened a critical, we crit here, otherwise just report normal hit
               // ---------------------------------------------------------------------
               nRet = ((bThreatenCritical != EngineConstants.FALSE) ? EngineConstants.COMBAT_RESULT_CRITICALHIT : EngineConstants.COMBAT_RESULT_HIT);

               if (nAttackType == EngineConstants.ATTACK_TYPE_MELEE)
               {
                    // -----------------------------------------------------------------
                    // If we are backstabbing, we change the result here if it
                    // was a crit. Abilities never bs (anim priority)
                    // -----------------------------------------------------------------
                    if (nAbility == 0 && Combat_CheckBackstab(oAttacker, oTarget, oWeapon, fFlanking) != EngineConstants.FALSE)
                    {
                         nRet = EngineConstants.COMBAT_RESULT_BACKSTAB;
                    }
               }

          }
          // -------------------------------------------------------------------------
          // Miss...
          // -------------------------------------------------------------------------
          else
          {
               nRet = EngineConstants.COMBAT_RESULT_MISS;

          }

          // -------------------------------------------------------------------------
          // Misdirection Hex
          // -------------------------------------------------------------------------
          if (GetHasEffects(oAttacker, EngineConstants.EFFECT_TYPE_MISDIRECTION_HEX) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace_Combat("combat_h.GetAttackResult", " Attacker under misdirection hex: Hits are misses, crits are hits.", oTarget);
#endif

               if (nRet == EngineConstants.COMBAT_RESULT_HIT || nRet == EngineConstants.COMBAT_RESULT_BACKSTAB)
               {
                    nRet = EngineConstants.COMBAT_RESULT_MISS;
               }
               else if (nRet == EngineConstants.COMBAT_RESULT_CRITICALHIT)
               {
                    nRet = EngineConstants.COMBAT_RESULT_HIT;
               }
          }

          //  return EngineConstants.COMBAT_RESULT_BACKSTAB;

#if DEBUG
          Log_Trace_Combat("combat_h.GetAttackResult", " ToHit Calculation: " +
                           " fAttack:" + ToString(fAttack) +
                           " = (fAttackRating: " + ToString(fAttackRating) +
                           " fAttackRoll:" + ToString(fAttackRoll) +
                           " (range penalty:" + ToString(fPenalties) + ")" +
                           " fFlanking: " + ToString(fFlanking) +
                           " fBonus(script): " + ToString(fBonus) +
                           ")", oAttacker, oTarget, EngineConstants.LOG_CHANNEL_COMBAT_TOHIT);

          Log_Trace_Combat("combat_h.GetAttackResult", " ToHit Calculation (2):  " +
                           " fDefenseRating: " + ToString(fDefenseRating) +
                           " fCriticalHitModifier: " + ToString(fCriticalHitModifier) +
                           " bThreatenCritical: " + ToString(bThreatenCritical), oAttacker, oTarget, EngineConstants.LOG_CHANNEL_COMBAT_TOHIT);

#endif

          return nRet;

     }

     /*
     *  @brief Handles processing an Attack Command
     *
     *  @param oAttacker       The xCommand owner, usually gameObject
     *  @param oTarget         The Target of the command
     *
     *  @returns CombatAttackResultStruct with damage and attackresult populated
     *
     *  "Don't touch this if you want to live"
     *
     *  @author Georg Zoeller
     **/
     public CombatAttackResultStruct Combat_PerformAttack(GameObject oAttacker, GameObject oTarget, GameObject oWeapon, float fDamageOverride = 0.0f, int nAbility = 0)
     {
          CombatAttackResultStruct stRet = new CombatAttackResultStruct();
          float fDamage = 0.0f;
          int nAttackType = Combat_GetAttackType(oAttacker, oWeapon);

          stRet.fAttackDuration = EngineConstants.ATTACK_LOOP_DURATION_INVALID;

          // -------------------------------------------------------------------------
          // Attack check happens here...
          // -------------------------------------------------------------------------

          stRet.nAttackResult = Combat_GetAttackResult(oAttacker, oTarget, oWeapon);

          // -------------------------------------------------------------------------
          // If attack result was not a miss, go on to calculate damage
          // -------------------------------------------------------------------------
          if (stRet.nAttackResult != EngineConstants.COMBAT_RESULT_MISS)
          {
               int bCriticalHit = (stRet.nAttackResult == EngineConstants.COMBAT_RESULT_CRITICALHIT) ? EngineConstants.TRUE : EngineConstants.FALSE;

               // -------------------------------------------------------------------------
               // If attack result was not a miss, check if we need to handle a deathblow
               // -------------------------------------------------------------------------
               fDamage = ((fDamageOverride == 0.0f) ?
                                 Combat_Damage_GetAttackDamage(oAttacker, oTarget, oWeapon, stRet.nAttackResult) : fDamageOverride);

               float fTargetHealth = GetCurrentHealth(oTarget);

               // -----------------------------------------------------------------
               //  Ranged weapons attacks are not synched and therefore we never
               //  need to worry about reporting deathblows to the engine.
               // ---------------------------------------------------------------------

               // ---------------------------------------------------------------------
               // When not using a ranged weapon, there are synchronize death blows to handle
               // ---------------------------------------------------------------------
               if (nAttackType != EngineConstants.ATTACK_TYPE_RANGED && nAbility == 0 && stRet.nAttackResult != EngineConstants.COMBAT_RESULT_MISS)
               {

                    // -----------------------------------------------------------------
                    // Deathblows against doors look cool, but really...
                    // -----------------------------------------------------------------
                    if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
                    {

                         // ---------------------------------------------------------
                         // Special conditions.
                         //
                         // There are a few cases in the single player campaign
                         // where we want the spectacular deathblow to occur if possible.
                         //
                         // The following logic defines these conditions
                         // ---------------------------------------------------------
                         int nAppearance = GetAppearanceType(oTarget);
                         int nRank = GetCreatureRank(oTarget);

                         int bSpecial = EngineConstants.FALSE;

                         // ---------------------------------------------------------
                         // ... all boss ogres (there's 1 in the official campaign) by request
                         //     from Dr. Muzyka.
                         // ... all elite bosses
                         // ---------------------------------------------------------
                         if ((nAppearance == EngineConstants.APR_TYPE_OGRE || (nRank == EngineConstants.CREATURE_RANK_BOSS || nRank == EngineConstants.CREATURE_RANK_ELITE_BOSS)) ||
                               nRank == EngineConstants.CREATURE_RANK_ELITE_BOSS)
                         {
                              // -------------------------------------------------
                              // ... but only if they are at the health threshold
                              //     required for deathblows to trigger
                              // -------------------------------------------------
                              if (IsHumanoid(oAttacker) != EngineConstants.FALSE)
                              {
                                   bSpecial = _GetRelativeResourceLevel(oTarget, EngineConstants.PROPERTY_DEPLETABLE_HEALTH) < EngineConstants.SPECIAL_BOSS_DEATHBLOW_THRESHOLD ? EngineConstants.TRUE : EngineConstants.FALSE;
                              }
                         }

                         // ---------------------------------------------------------
                         // Deathblows occur when
                         //  ... target isn't immortal (duh) AND
                         //      ... the damage of the hit exceeds the creature's health OR
                         //      ... aforementioned 'special' conditions are met.
                         // ---------------------------------------------------------
                         if ((IsImmortal(oTarget) == EngineConstants.FALSE && (fDamage >= fTargetHealth || bSpecial != EngineConstants.FALSE)))
                         {

                              // -----------------------------------------------------
                              // ... only from party members AND
                              //    ... if we determine that a deathblow doesn't interrupt gameplay OR
                              //    ... aforementioned 'special' conditions are met
                              // -----------------------------------------------------
                              if (IsPartyMember(oAttacker) != EngineConstants.FALSE && (CheckForDeathblow(oAttacker, oTarget) != EngineConstants.FALSE || bSpecial != EngineConstants.FALSE))
                              {
                                   // -------------------------------------------------
                                   // Verify some more conditions...
                                   // -------------------------------------------------
                                   int bDeathBlow = Combat_GetValidDeathblow(oAttacker, oTarget);
                                   if (bDeathBlow != EngineConstants.FALSE)
                                   {
                                        stRet.nAttackResult = EngineConstants.COMBAT_RESULT_DEATHBLOW;

                                        // ---------------------------------------------
                                        // Special treatment for ogre
                                        // Reason: The ogre, unlike all other special bosses
                                        //         has a second, non spectacular deathblow.
                                        //         if we specify 0, there's a 50% chance that
                                        //         one is played, which we don't want in this
                                        //         case, so we're passing the id of the
                                        //         spectacular one instead.
                                        // ---------------------------------------------
                                        if (bSpecial != EngineConstants.FALSE && nAppearance == EngineConstants.APR_TYPE_OGRE)
                                        {
                                             stRet.nDeathblowType = 5; // 5 - ogre slowmo deathblow
                                        }
                                        else
                                        {
                                             stRet.nDeathblowType = 0;  // 0 - auto select in engine;
                                        }

                                   }
                                   else
                                   {
                                        // Failure to meet conditions: convert to hit.
                                        if (stRet.nAttackResult != EngineConstants.COMBAT_RESULT_BACKSTAB)
                                        {
                                             stRet.nAttackResult = EngineConstants.COMBAT_RESULT_HIT;
                                        }
                                   }
                              }
                              else
                              {
                                   // Failure to meet conditions: convert to hit.
                                   if (stRet.nAttackResult != EngineConstants.COMBAT_RESULT_BACKSTAB)
                                   {
                                        stRet.nAttackResult = EngineConstants.COMBAT_RESULT_HIT;
                                   }
                              }

                         } /* ishumanoid*/

                    } /* obj_type creature*/
               }

          }
          int nDamageType = EngineConstants.DAMAGE_TYPE_PHYSICAL;

          if (nAttackType == EngineConstants.ATTACK_TYPE_RANGED)
          {
               // ---------------------------------------------------------------------
               // Certain projectiles modifyt the damage type done by a ranged weapon
               // This is defined in PRJ_BASE.
               // ---------------------------------------------------------------------
               int nProjectileIndex = GetLocalInt(oWeapon, EngineConstants.PROJECTILE_OVERRIDE);
               if (nProjectileIndex != EngineConstants.FALSE)
               {
                    int nDamageTypeOverride = GetM2DAInt(EngineConstants.TABLE_PROJECTILES, "DamageType", nProjectileIndex);
                    if (nDamageTypeOverride > 0)
                    {
                         nDamageType = nDamageTypeOverride;
                    }
               }

               // ---------------------------------------------------------------------
               // When using a ranged weapon, we need to report the duration of the
               // aim loop to the engine
               // ---------------------------------------------------------------------
               stRet.fAttackDuration = GetCreatureRangedDrawSpeed(oAttacker, oWeapon);
          }
          else
          {
               float fSpeed = CalculateAttackTiming(oAttacker, oWeapon);
               if (fSpeed > 0.0f)
               {
                    stRet.fAttackDuration = fSpeed;
               }
          }

          // -------------------------------------------------------------------------
          // The Impact xEffect is not a real xEffect - it is not ever applied. Instead
          // it is used to marshal information about the attack back to the impact
          // event.
          // -------------------------------------------------------------------------
          stRet.eImpactEffect = EffectImpact(fDamage, oWeapon, 0, 0, nDamageType);

#if DEBUG
          Log_Trace_Combat("combat_h.Combat_PerformAttack", " Attack Result: " + Log_GetAttackResultNameById(stRet.nAttackResult), oAttacker, oTarget, EngineConstants.LOG_CHANNEL_COMBAT_TOHIT);
#endif

          return stRet;
     }

     /*
     *  @brief Handles processing an Attack Command
     *
     *  @param oAttacker       The xCommand owner, usually OBJEC_TSE
     *  @param oTarget         The Target of the command
     *  @param nCommandId      The xCommand Id
     *  @param nCommandSubType The xCommand subtype
     *
     *  @returns EngineConstants.COMBAT_RESULT_* constant
     *
     *  @author Georg Zoeller
     **/
     public int Combat_HandleCommandAttack(GameObject oAttacker, GameObject oTarget, int nCommandSubType)
     {

          CombatAttackResultStruct stAttack1 = new CombatAttackResultStruct();
          CombatAttackResultStruct stAttack2 = new CombatAttackResultStruct();

          GameObject oWeapon = null;
          GameObject oWeapon2 = null;

          int nHand = Combat_GetAttackHand(oAttacker);

          if (nHand == EngineConstants.HAND_MAIN)
          {
               oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN);
          }
          else if (nHand == EngineConstants.HAND_OFFHAND)
          {
               oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND);
          }

          // -------------------------------------------------------------------------
          // Double Weapon Strike.
          // -------------------------------------------------------------------------
          if (IsModalAbilityActive(oAttacker, EngineConstants.ABILITY_TALENT_DUAL_WEAPON_DOUBLE_STRIKE) != EngineConstants.FALSE)
          {
               nHand = EngineConstants.HAND_BOTH;
               oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN);
               oWeapon2 = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND);
          }

          int nAttackType = Combat_GetAttackType(oAttacker, oWeapon);

          // -------------------------------------------------------------------------
          // Handle Attack #1
          // -------------------------------------------------------------------------
          stAttack1 = Combat_PerformAttack(oAttacker, oTarget, oWeapon);

          if (nHand == EngineConstants.HAND_BOTH)
          {
               stAttack2 = Combat_PerformAttack(oAttacker, oTarget, oWeapon2);

               if (stAttack1.nAttackResult != EngineConstants.COMBAT_RESULT_DEATHBLOW && stAttack2.nAttackResult == EngineConstants.COMBAT_RESULT_DEATHBLOW)
               {
                    stAttack1 = stAttack2;
                    nHand = EngineConstants.HAND_MAIN; // Deathblows just use the main hand.
               }

          }

          // -------------------------------------------------------------------------
          // If we execute a deathblow, we gain the death fury xEffect for a couple of
          // seconds and apply the deathblow command
          // -------------------------------------------------------------------------
          if (stAttack1.nAttackResult == EngineConstants.COMBAT_RESULT_DEATHBLOW)
          {

               // ----------------------------------------------------------------------
               // Georg: Do Not Modify the following section.
               // START >>
               // GM - Adding the deathblow should be the last thing done because it
               // will clear the attack command.
               // Specifically, SetAttackResult MUST be executed before adding the deathblow.
               // ----------------------------------------------------------------------
               SetAttackResult(oAttacker, stAttack1.nAttackResult, stAttack1.eImpactEffect,
                                           EngineConstants.COMBAT_RESULT_INVALID, Effect());

               WR_AddCommand(oAttacker, CommandDeathBlow(oTarget, stAttack1.nDeathblowType), EngineConstants.TRUE, EngineConstants.TRUE);

               return EngineConstants.COMMAND_RESULT_SUCCESS;
               // ----------------------------------------------------------------------
               // << END
               // ----------------------------------------------------------------------
          }

          // -------------------------------------------------------------------------
          // SetAttackResult requires a result in either the first or second result
          // field to determine which hand should attack.
          // -------------------------------------------------------------------------
          if (nHand == EngineConstants.HAND_MAIN || stAttack1.nAttackResult == EngineConstants.COMBAT_RESULT_BACKSTAB)
          {
               SetAttackResult(oAttacker, stAttack1.nAttackResult, stAttack1.eImpactEffect,
                                         EngineConstants.COMBAT_RESULT_INVALID, Effect());

          }
          else if (nHand == EngineConstants.HAND_OFFHAND)
          {
               SetAttackResult(oAttacker, EngineConstants.COMBAT_RESULT_INVALID, Effect(),
                                             stAttack1.nAttackResult, stAttack1.eImpactEffect);
          }
          else if (nHand == EngineConstants.HAND_BOTH)
          {
               SetAttackResult(oAttacker, stAttack1.nAttackResult, stAttack1.eImpactEffect, stAttack2.nAttackResult, stAttack2.eImpactEffect);
          }
          else
          {
               SetAttackResult(oAttacker, stAttack1.nAttackResult, stAttack1.eImpactEffect,
                                         EngineConstants.COMBAT_RESULT_INVALID, Effect());
          }

          if (stAttack1.fAttackDuration != EngineConstants.ATTACK_LOOP_DURATION_INVALID)
          {

               if (IsHumanoid(oAttacker) != EngineConstants.FALSE)
               {

                    if (nAttackType == EngineConstants.ATTACK_TYPE_RANGED)
                    {
                         // the "attack duration" for ranged weapons actually overrides
                         // the time spent drawing and preparing to aim
                         if (GetBaseItemType(oWeapon) == EngineConstants.BASE_ITEM_TYPE_STAFF)
                         {
                              SetAttackDuration(oAttacker, 0.30f);
                         }
                         else
                         {
                              GameObject oArmor = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_CHEST);
                              if (IsArmorMassive(oArmor) == EngineConstants.FALSE &&
                                   HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_MASTER_ARCHER) != EngineConstants.FALSE)
                              {
                                   if (IsFollower(oAttacker) != EngineConstants.FALSE)
                                        SetAttackDuration(oAttacker, 0.8f);
                                   else
                                        SetAttackDuration(oAttacker, 1.5f);
                              }
                              else if (IsArmorHeavyOrMassive(oArmor) != EngineConstants.FALSE)
                              {
                                   if (IsFollower(oAttacker) != EngineConstants.FALSE)
                                        SetAttackDuration(oAttacker, 2.0f);
                                   else
                                        SetAttackDuration(oAttacker, 2.5f);
                              }
                              else
                              {
                                   if (IsFollower(oAttacker) != EngineConstants.FALSE)
                                        SetAttackDuration(oAttacker, 0.8f);
                                   else
                                        SetAttackDuration(oAttacker, 1.5f);
                              }
                         }

                         SetAimLoopDuration(oAttacker, stAttack1.fAttackDuration);

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleCommandAttack", "RangedAim Loop Duration set to " + FloatToString(stAttack1.fAttackDuration));
#endif
                    }
                    else if (nAttackType == EngineConstants.ATTACK_TYPE_MELEE)
                    {

                         SetAttackDuration(oAttacker, stAttack1.fAttackDuration);

                    }
               }
          }

          return EngineConstants.COMMAND_RESULT_SUCCESS;
     }

     /*
     *  @brief Handles processing an attack impact event
     *
     *  @param oAttacker       The xCommand owner, usually gameObject
     *  @param oTarget         The Target of the command
     *  @param nAttackResult   The result of the attack (EngineConstants.COMBAT_RESULT_*)
     *  @param eImpactEffect   The attack's impact effect.
     *
     *  @returns EngineConstants.COMBAT_RESULT_* constant
     *
     *  @author Georg Zoeller
     **/
     public void Combat_HandleAttackImpact(GameObject oAttacker, GameObject oTarget, int nAttackResult, xEffect eImpactEffect)
     {

          int nUIMessage = EngineConstants.UI_MESSAGE_MISSED;

          // -------------------------------------------------------------------------
          // We retired combat result blocked, so we treat it as a miss here.
          // Might be back in DA2
          // -------------------------------------------------------------------------
          if (nAttackResult == EngineConstants.COMBAT_RESULT_BLOCKED)
          {
               nAttackResult = EngineConstants.COMBAT_RESULT_MISS;
               nUIMessage = EngineConstants.UI_MESSAGE_BLOCKED;
          }

          // -----------------------------------------------------------------
          // If the target managed to stealth, we're converting it into a miss
          // -----------------------------------------------------------------
          if (nAttackResult != EngineConstants.COMBAT_RESULT_DEATHBLOW && nAttackResult != EngineConstants.COMBAT_RESULT_MISS && IsInvisible(oTarget) != EngineConstants.FALSE)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "Target turned invisible - hit converted to miss");
#endif
               nAttackResult = EngineConstants.COMBAT_RESULT_MISS;
          }

          if (nAttackResult != EngineConstants.COMBAT_RESULT_MISS)
          {
               // -------------------------------------------------------------
               // Tell the damage xEffect to update gore and by how much...
               // see gore_h for details or ask georg.
               // -------------------------------------------------------------
               GameObject oWeapon = GetEffectObjectRef(ref eImpactEffect, 0);
               int nApp = GetAppearanceType(oAttacker);
               int nAbility = GetEffectIntegerRef(ref eImpactEffect, 1);

               float fPerSpace = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "PERSPACE", nApp);
               // Skipping range check if the creature is large (personal space greater or equal to one)
               if (IsObjectValid(oWeapon) != EngineConstants.FALSE && IsUsingMeleeWeapon(oAttacker, oWeapon) != EngineConstants.FALSE && fPerSpace < 1.0f)
               {
                    if (GetDistanceBetween(oTarget, oAttacker) > 5.0f)
                    {
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "Melee attack impact from past 5m found, changed to EngineConstants.COMBAT_RESULT_MISS");
#endif
                         nAttackResult = EngineConstants.COMBAT_RESULT_MISS;
                    }

               }

               // -----------------------------------------------------------------
               // The attack was successful, ...
               // -----------------------------------------------------------------
               float fDamage = GetEffectFloatRef(ref eImpactEffect, 0);
               int nVfx = GetEffectIntegerRef(ref eImpactEffect, 0);
               int nDamageType = GetEffectIntegerRef(ref eImpactEffect, 2);

               // special damage type override for Shale, filtered for performance reasons
               int nAppearanceType = GetAppearanceType(oAttacker);
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "Attacker appearance type = " + ToString(nAppearanceType));
#endif
               if (nAppearanceType == 10100) // new shale type
               {
                    // check for damage type modifying effect
                    List<xEffect> eModifiers = GetEffects(oAttacker, 10000);
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "  Modifier effects present = " + ToString(GetArraySize(eModifiers)));
#endif
                    if (IsEffectValid(eModifiers[0]) != EngineConstants.FALSE)
                    {
                         xEffect _effect = eModifiers[0];
                         int nOverrideDamageType = GetEffectIntegerRef(ref _effect, 0);
                         nDamageType = nOverrideDamageType;

#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "  First modifier is valid with type " + ToString(nOverrideDamageType));
#endif
                    }
               }

               // -------------------------------------------------------------
               // If damage was negative, something went wrong and we bail out
               // -------------------------------------------------------------
               if (fDamage < 0.0f)
               {
                    return;
               }

               int nGore = 0;

               // ---------------------------------------------------------------------
               // Only melee weapons update gore.
               // ---------------------------------------------------------------------
               if (IsObjectValid(oWeapon) != EngineConstants.FALSE)
               {
                    if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_MELEE)
                    {
                         // -------------------------------------------------------------
                         // Attacks that are not deathblows may play an attack grount.
                         // -------------------------------------------------------------
                         if (nAttackResult != EngineConstants.COMBAT_RESULT_DEATHBLOW)
                         {
                              SSPlaySituationalSound(oAttacker, EngineConstants.SOUND_SITUATION_ATTACK_IMPACT);
                         }

                         // only update gore if the target can bleed (PeterT 25/7/08)
                         int nTargetAppearance = GetAppearanceType(oTarget);
                         if (GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "bCanBleed", nTargetAppearance) != EngineConstants.FALSE)
                         {
                              nGore = EngineConstants.DAMAGE_EFFECT_FLAG_UPDATE_GORE;
                         }
                    }
               }

               if (nAttackResult == EngineConstants.COMBAT_RESULT_CRITICALHIT)
               {

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "COMBAT_RESULT_CRITICALHIT");
#endif

                    // -----------------------------------------------------------------
                    // Damage below a certain threshold is not displayed as critical
                    // -----------------------------------------------------------------
                    if (fDamage >= EngineConstants.DAMAGE_CRITICAL_DISPLAY_THRESHOLD)
                    {
                         if (IsPartyMember(oAttacker) != EngineConstants.FALSE)
                         {
                              Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectScreenShake(EngineConstants.SCREEN_SHAKE_TYPE_CRITICAL_HIT), oTarget, 1.0f, oAttacker);
                         }

                         nGore |= EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL;
                    }

               }
               else if (nAttackResult == EngineConstants.COMBAT_RESULT_BACKSTAB)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "COMBAT_RESULT_BACKSTAB");
#endif

                    nGore |= EngineConstants.DAMAGE_EFFECT_FLAG_BACKSTAB;
                    nGore |= EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL;
                    //   UI_DisplayMessage(oAttacker, EngineConstants.UI_MESSAGE_BACKSTAB);

               }
               else if (nAttackResult == EngineConstants.COMBAT_RESULT_DEATHBLOW)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "COMBAT_RESULT_DEATHBLOW");
#endif

                    nGore |= EngineConstants.DAMAGE_EFFECT_FLAG_CRITICAL;

               }
               else
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "COMBAT_RESULT_HIT");
#endif

               }

               // ---------------------------------------------------------------------
               // Templar's Righteous Strike - 25% mana damage
               // ---------------------------------------------------------------------
               if (IsMagicUser(oTarget) != EngineConstants.FALSE)
               {
                    if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_MELEE)
                    {
                         if (HasAbility(oAttacker, EngineConstants.ABILITY_TALENT_RIGHTEOUS_STRIKE) != EngineConstants.FALSE)
                         {
                              nGore |= EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_MANA;
                              nGore |= EngineConstants.DAMAGE_EFFECT_FLAG_LEECH_25;

                              ApplyEffectVisualEffect(oAttacker, oTarget, 90181, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f, EngineConstants.ABILITY_TALENT_RIGHTEOUS_STRIKE);
                         }
                    }
               }

               // -------------------------------------------------------------
               //            Here is where the actual damage is dealt
               // -------------------------------------------------------------
               Effects_ApplyInstantEffectDamage(oTarget, oAttacker, fDamage, nDamageType, nGore, nAbility, nVfx);
               Combat_Damage_CheckOnImpactAbilities(oTarget, oAttacker, fDamage, nAttackResult, oWeapon, nAbility);
          }

          else if (nAttackResult == EngineConstants.COMBAT_RESULT_MISS)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "COMBAT_RESULT_MISS");
#endif

               UI_DisplayMessage(oAttacker, nUIMessage);

          }
          else if (nAttackResult == EngineConstants.COMBAT_RESULT_BLOCKED)
          {
#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "combat_h.HandleAttackImpact", "COMBAT_RESULT_BLOCKED");
#endif

               // Do something smart here in the future
               //        UI_DisplayMessage(oAttacker, EngineConstants.UI_MESSAGE_MISSED);
          }

          // Stats - handle hit rate
          STATS_HandleHitRate(oAttacker, nAttackResult);
     }

     // GZ: Temporary function to prevent the world from collapsing upon itself
     public void Combat_HandleAbilityAttackImpact(GameObject oAttacker, GameObject oTarget, int nAttackResult, float fDamage)
     {

          xEffect eImpactEffect = EffectImpact(fDamage, null);

          Combat_HandleAttackImpact(oAttacker, oTarget, nAttackResult, eImpactEffect);
     }

     // Handles any combat related logic for when a creature disappears from combat. This function should be run
     // at 2 cases both for a player GameObject and a creature object:
     // 1. Disappear event
     // 2. Appear xEvent when the hostile creature turned non-hostile
     public void Combat_HandleCreatureDisappear(GameObject oCreature, GameObject oDisappearer)
     {
#if DEBUG
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "Combat_HandleCreatureDisappeart", "creature: " + GetTag(oCreature) + ", disappearer: " + GetTag(oDisappearer));
#endif

          if (IsFollower(oCreature) != EngineConstants.FALSE)
          {
               AI_Threat_UpdateEnemyDisappeared(oCreature, oDisappearer);

               // ----------------------------------------------------------------------
               // If the party does no longer perceive any hostiles....
               // ----------------------------------------------------------------------
               if (IsPartyPerceivingHostiles(oCreature) == EngineConstants.FALSE)
               {

                    if (IsPartyDead() == EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "Combat_HandleCreatureDisappear", "STOPPING COMBAT FOR PARTY!");
#endif

                         SSPlaySituationalSound(GetRandomPartyMember(), EngineConstants.SOUND_SITUATION_END_OF_COMBAT);

                         DelayEvent(3.0f, GetModule(), Event(EngineConstants.EVENT_TYPE_DELAYED_GM_CHANGE));
                    }
               }
          }
          else // non party members
          {
               AI_Threat_UpdateEnemyDisappeared(oCreature, oDisappearer);
               if (IsPerceivingHostiles(oCreature) == EngineConstants.FALSE && GetCombatState(oCreature) != EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT, "Combat_HandleCreatureDisappear", "STOPPING COMBAT FOR NORMAL CREATURE!");
#endif
                    SetCombatState(oCreature, EngineConstants.FALSE);

                    // Added to make a EngineConstants.COMBAT_END event
                    // FAB 9/4
                    xEvent evCombatEnds = Event(EngineConstants.EVENT_TYPE_COMBAT_END);
                    //evCombatEnds = SetEventIntegerRef(ref evCombatEnds, 0, bcombatstate);
                    SignalEvent(oCreature, evCombatEnds);
               }
          }
     }

     public int IsCombatHit(int nAttackResult)
     {
          if (nAttackResult == EngineConstants.COMBAT_RESULT_MISS || nAttackResult == EngineConstants.COMBAT_RESULT_BLOCKED || nAttackResult == EngineConstants.COMBAT_RESULT_INVALID)
          {
               return EngineConstants.FALSE;
          }
          else
          {
               return EngineConstants.TRUE;
          }
     }
}