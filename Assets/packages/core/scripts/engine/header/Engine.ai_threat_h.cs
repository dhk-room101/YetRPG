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
     //////////////////////////////////////////////
     // ai_threat_h
     //
     // This script includes all Threat AI functions
     //
     // Owner: Yaron Jakobs
     //
     /////////////////////////////////////////////

     /* @addtogroup scripting_threat Scripting AI Threat handling
     *
     * Scripting AI Threat handling
     */
     /* @{*/

     ////#include"ai_threat_engine_h"
     //#include"var_constants_h"
     //#include"log_h"
     //#include"core_h"
     //#include"wrappers_h"
     //#include"core_difficulty_h"

     // Constant
     //moved public const int EngineConstants.AI_THREAT_SIZE = 5;                           // Max number of creatures in threat list
     //moved public const float EngineConstants.AI_THREAT_MIN = 0.0f;                        // Min threat value
     //moved public const float EngineConstants.AI_THREAT_MAX = 1000.0f;                       // Max threat value
     //moved public const float EngineConstants.AI_THREAT_DISSOLVE = -0.5f;                    // Amount of threat lowered each second
     //moved public const float EngineConstants.AI_THREAT_ROGUE_DAMAGE_RATIO = 0.8;         // rogues takes less threat from damage
     //moved public const float EngineConstants.AI_THREAT_VALUE_DIRECT_DAMAGE = 1.0f;        // Multiplier for damage threat
     //moved public const float EngineConstants.AI_THREAT_VALUE_DIRECT_HEALING = 1.0f;       // Multiplier for healing threat
     //moved public const float EngineConstants.AI_THREAT_VALUE_ALLY_DAMAGE = 0.5f;          // Multiplier for ally damage threat
     //moved public const float EngineConstants.AI_THREAT_VALUE_ATTACKED = 1.0f;             // A constant value of threat to apply only when being attacked
     //moved public const float EngineConstants.AI_THREAT_VALUE_ENEMY_PERCEIVED = 10.0f;     // A max setting for a random value that is set when first perceived a hostile enemy
     //moved public const float EngineConstants.AI_THREAT_PERCEIVED_ARMOR_LIGHT = 5.0f;       // A constant value to add to perception threat when perceiving someone in light armor
     //moved public const float EngineConstants.AI_THREAT_PERCEIVED_ARMOR_MEDIUM = 10.0f;       // A constant value to add to perception threat when perceiving someone in light armor
     //moved public const float EngineConstants.AI_THREAT_PERCEIVED_ARMOR_HEAVY = 10.0f;       // A constant value to add to perception threat when perceiving someone in light armor
     //moved public const float EngineConstants.AI_THREAT_PERCEIVED_ARMOR_MASSIVE = 10.0f;       // A constant value to add to perception threat when perceiving someone in light armor
     //moved public const float EngineConstants.AI_THREAT_PERCEIVED_WEAPON_MELEE = 5.0f;    // A constant value to add to perception threat
     //moved public const float EngineConstants.AI_THREAT_HATED_ENEMY_COEFFICIENT = 2.0f;    // Multiply any threat chance by this value if coming from a hated target
     //moved public const float EngineConstants.AI_THREAT_ABILITY_USE_THREAT_COEFF = 2.0f;         // The amount of threat per point assigned to the ability to apply when the ability is fired
     //moved public const float EngineConstants.AI_THREAT_ABILITY_IMPACT_THREAT_COEFF = 10.0f;      // The amount of threat per point assign to the ability to apply when the ability hits the target
     //moved public const int EngineConstants.AI_THREAT_SWITCH_TIMER_JUMP = 5;               // The amount of seconds the switch timer increases each time there is a switch
     //moved public const int EngineConstants.AI_THREAT_SWITCH_TIME_MAX = 25;               // The maximum amount of switch timer. The timer revers to 0 afterwards
     //moved public const float EngineConstants.AI_THREAT_MIN_CHANGE = 1.0f;                 // The minimum value of each threat change
     //moved public const float EngineConstants.AI_THREAT_PASSIVE_RANGE = 30.0f;              // The range other creatures need to be in order to hate something that happens in the middle of that area
     //moved public const int EngineConstants.AI_THREAT_CHANCE_KEEP_TARGET_20_HEALTH = 50; // the chance to keep the current target if it has 20% or less health (on top of other timer restrictions)
     //moved public const int EngineConstants.AI_THREAT_CHANCE_KEEP_TARGET_10_HEALTH = 90; // the chance to keep the current target is it has 10% or less health (on top of other timer restrictions)
     //moved public const float EngineConstants.AI_THREAT_CANT_ATTACK_TARGET_REDUCTION = 0.75f;  // how much  to reduce the threat if can't attack the target
     //moved public const float EngineConstants.AI_THREAT_NON_DAMAGING_AOE_EXTRA = 20.0f; // extra threat fired when enemies go on non-hostile AOE and only on hard difficulty.

     /* @brief Returns most hated enemy
*
* This function is the main interface for the threat system. It will mostly return
* the most hated enemy, but it sometimes can be not the most hated (to avoid too many target switching)
*
* @param oCreature the creature for which we need a target for
* @param bUpdateThreatTarget whether or not to update the threat target during the check
* @returns most hated enemy
* @author Yaron
*/
     public GameObject AI_Threat_GetThreatTarget(GameObject oCreature, int bUpdateThreatTarget = EngineConstants.TRUE)
     {
          if (bUpdateThreatTarget != EngineConstants.FALSE)
               AI_Threat_UpdateCreatureThreat(oCreature, null, 0.0f);
          GameObject oTarget = GetLocalObject(oCreature, EngineConstants.AI_THREAT_TARGET);
          if (IsObjectValid(oTarget) != EngineConstants.FALSE &&
              (IsDead(oTarget) != EngineConstants.FALSE || IsDying(oTarget) != EngineConstants.FALSE || IsPerceiving(oCreature, oTarget) == EngineConstants.FALSE))
          {
#if DEBUG
               Log_Trace_Threat("AI_Threat_GetThreatTarget", "ERROR: threat target is dead, dying or not perceived! (" + GetTag(oTarget) + ")", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
               if (bUpdateThreatTarget != EngineConstants.FALSE)
                    AI_Threat_UpdateCreatureThreat(oCreature, null, 0.0f);
          }
#if DEBUG
          Log_Trace_Threat("AI_Threat_GetThreatTarget", "Threat Target: " + GetTag(oTarget));
#endif
          return oTarget;
     }

     // Updated the threat target for oCreature
     public void AI_Threat_SetThreatTarget(GameObject oCreature, GameObject oTarget)
     {
#if DEBUG
          Log_Trace_Threat("AI_Threat_SetThreatTarget", "New threat target: " + GetTag(oTarget));
          AI_Threat_Display(oCreature, "Before setting threat target");
#endif
          SetLocalObject(oCreature, EngineConstants.AI_THREAT_TARGET, oTarget);
#if DEBUG
          AI_Threat_Display(oCreature, "After setting threat target");
#endif
     }

     /* @brief Updates threat based on damage inflicted
*
* The threat is updated for the damaged creature
* The exact amount of threat is based on the relative damage done. For example:
* If someone has a maximum health of 100 and it was damaged for 50 HP, the generated
* basic threat will be equal to 50 (for 50%) - multiplied by the global coefficient.
* NOTE: currently not updating threat for allies (it is not easy to retreive the list of allies)
*
* @param oCreature the creature being damaged
* @param oAttacker the attacking creature doing the damage
* @param nDamage the amount of damage inflicted
* @author Yaron
*/
     public void AI_Threat_UpdateDamage(GameObject oCreature, GameObject oAttacker, float fDamage)
     {
          if (IsObjectHostile(oCreature, oAttacker) == EngineConstants.FALSE)
               return; // doing nothing if both objects are not hostile towards each other
#if DEBUG
          Log_Trace_Threat("AI_Threat_UpdateDamage", "Attacker:" + GetTag(oAttacker) + ", Damage: " + FloatToString(fDamage));
          AI_Threat_Display(oCreature, "damage update before changing threat");
#endif
          float fMaxHealth = GetMaxHealth(oCreature);
          if (fMaxHealth == 0.0f)
          {
#if DEBUG
               Log_Trace_Threat("AI_Threat_UpdateDamage", "Max Health is 0!", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
               return; // Do nothing - this should not happen
          }

          float fThreatChange = fDamage / fMaxHealth * 100.0f * EngineConstants.AI_THREAT_VALUE_DIRECT_DAMAGE;

          // if rogue - less threat
          if (GetCreatureCoreClass(oAttacker) == EngineConstants.CLASS_ROGUE)
               fThreatChange *= 0.8f;

          // -------------------------------------------------------------------------
          // Threaten.
          // The presence of the threaten ability increases the amount of threat
          // generated from damage by 20%. This allows warriors to maintain hate
          // more effectively
          // Edit (yaron) feb 6, 2009: increasing to 100%
          // -------------------------------------------------------------------------
          if (IsModalAbilityActive(oAttacker, EngineConstants.ABILITY_TALENT_THREATEN) != EngineConstants.FALSE)
          {
               fThreatChange *= 2.0f;
          }

          AI_Threat_UpdateCreatureThreat(oCreature, oAttacker, fThreatChange);
     }

     /* @brief Updates threat based on ablity impacting
*
* The threat is updated for the creature who was the impact of the ability.
* The threat change is ability-specific (Ability Impact Threat)
* This is valid only for hostile abilities and is in addition to any damage the ability may cause
*
* @param oCreature the creature being impacted by the ability
* @param oAttacker the creature using the ability
* @param nAbilityID the ability type being used
* @author Yaron
*/
     public void AI_Threat_UpdateAbilityImpact(GameObject oCreature, GameObject oAttacker, int nAbilityID)
     {
          if (IsObjectHostile(oCreature, oAttacker) == EngineConstants.FALSE)
               return; // doing nothing if both objects are not hostile towards each other
#if DEBUG
          AI_Threat_Display(oCreature, "ability impact before changing threat");
          Log_Trace_Threat("AI_Threat_UpdateAbilityImpact", "Attacker:" + GetTag(oAttacker) + ", Ability: " + IntToString(nAbilityID));
#endif

          float fThreatChange = AI_Threat_GetAbilityImpactThreat(nAbilityID) * EngineConstants.AI_THREAT_ABILITY_IMPACT_THREAT_COEFF;

          if (nAbilityID == EngineConstants.ABILITY_SPELL_WALKING_BOMB && GetGameDifficulty() >= 2)
               fThreatChange *= 2.0f; // Georg is a bastard...

          AI_Threat_UpdateCreatureThreat(oCreature, oAttacker, fThreatChange);
     }

     /*public void AI_Threat_UpdateAbilityUsed(GameObject oCaster, int nAbilityID)
     {
         Log_Trace_Threat("AI_Threat_UpdateAbilityUsed", "Caster:" + GetTag(oCaster) + ", Ability: " + IntToString(nAbilityID));

         float fThreatChange = AI_Threat_GetAbilityUseThreat(nAbilityID);

         List<GameObject> arEnemies = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(gameObject), EngineConstants.AI_THREAT_PASSIVE_RANGE);
         int nSize = GetArraySize(arEnemies);
         int i;
         GameObject oCurrent;
         for(i = 0; i < nSize; i++)
         {
             oCurrent = arEnemies[i];
             if(GetCombatState(oCurrent) != EngineConstants.FALSE && IsObjectHostile(gameObject, oCurrent) && IsPerceiving(oCurrent, gameObject))
             {
                 AI_Threat_UpdateCreatureThreat(oCurrent, gameObject, fThreatChange);
             }
         }
     }*/

     /* @brief Updates threat based perceiving a creature
*
* Update the threat for the current creature based on the enemy that was perceived.
*
* @param oCreature the creature perceiving the enemy
* @param oEnemy the hostile creature by perceived by oCreature
* @author Yaron
*/
     public void AI_Threat_UpdateEnemyAppeared(GameObject oCreature, GameObject oEnemy)
     {
          //int nMaxRand = FloatToInt(EngineConstants.AI_THREAT_VALUE_ENEMY_PERCEIVED);
          //int nRand = Engine_Random(nMaxRand) + 1; // randomized so not the first perceived is always attacked
          //float fThreatChange = IntToFloat(nRand);
#if DEBUG
          Log_Trace_Threat("AI_Threat_UpdateEnemyAppeared", "Enemy:" + GetTag(oEnemy));
#endif

          GameObject oChestItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_CHEST, oEnemy);
          GameObject oWeaponItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oEnemy);
          int nChestItemType = GetBaseItemType(oChestItem);
          int nWeaponItemType = GetBaseItemType(oWeaponItem);
          float fThreatExtra = 0.0f;
          if (GetGameDifficulty() < 3) // apply this for difficulty levels lower than nightmare
          {
               switch (nChestItemType)
               {
                    case EngineConstants.BASE_ITEM_TYPE_ARMOR_LIGHT: fThreatExtra = EngineConstants.AI_THREAT_PERCEIVED_ARMOR_LIGHT; break;
                    case EngineConstants.BASE_ITEM_TYPE_ARMOR_MEDIUM: fThreatExtra = EngineConstants.AI_THREAT_PERCEIVED_ARMOR_MEDIUM; break;
                    case EngineConstants.BASE_ITEM_TYPE_ARMOR_HEAVY: fThreatExtra = EngineConstants.AI_THREAT_PERCEIVED_ARMOR_HEAVY; break;
                    case EngineConstants.BASE_ITEM_TYPE_ARMOR_MASSIVE:
                    case EngineConstants.BASE_ITEM_TYPE_ARMOR_SUPERMASSIVE:
                         fThreatExtra = EngineConstants.AI_THREAT_PERCEIVED_ARMOR_MASSIVE; break;
               }
          }
          float fThreatChange = EngineConstants.AI_THREAT_VALUE_ENEMY_PERCEIVED + fThreatExtra;

          float fMoreExtra = GetLocalFloat(oEnemy, EngineConstants.AI_THREAT_GENERATE_EXTRA_THREAT);

          fThreatChange += fMoreExtra;

          // weapon check
          if (GetGameDifficulty() < 3 && // extra threat only for difficulty lower than nightmare
              nChestItemType != EngineConstants.BASE_ITEM_TYPE_LONGBOW && nChestItemType != EngineConstants.BASE_ITEM_TYPE_SHORTBOW && nChestItemType != 21
             && nChestItemType != EngineConstants.BASE_ITEM_TYPE_STAFF)
               fThreatChange += EngineConstants.AI_THREAT_PERCEIVED_WEAPON_MELEE;

          // last - if this is a 'coward' creature, give it minimal hate
          if (GetPackageAI(oEnemy) == 10130)
               fThreatChange = 1.0f;

          // Check to see if we need to do a force-target-switch-check (ClearAllCommands)
          // This is so enemies attack newly perceived party members who have higher threat while still trying to attack an
          // older target
          /*if(!IsFollower(oCreature) && AI_Threat_ClearToSwitchTarget(oCreature))
          {
              GameObject oCurrentTarget = AI_Threat_GetThreatTarget(oCreature); // current threat target
              float fCurrentTargetThreat = GetThreatValueByObjectID(oCreature, oCurrentTarget);
              Log_Trace_Threat("AI_Threat_UpdateEnemyAppeared", "Current target threat: " + FloatToString(fCurrentTargetThreat));

              if(IsObjectValid(oCurrentTarget) && fThreatChange > fCurrentTargetThreat)
              {
                  Log_Trace_Threat("AI_Threat_UpdateEnemyAppeared", "Newly perceived creature is more threatening then existing threat target - clearing all actions");
                  WR_ClearAllCommands(oCreature);
              }
          }*/

#if DEBUG
          AI_Threat_Display(oCreature, "Enemy appeared, before changing threat");
#endif
          AI_Threat_UpdateCreatureThreat(oCreature, oEnemy, fThreatChange);
     }

     /* @brief Updates threat based on losing sight of a creature
*
* Remove the enemy creature from the current creature's threat list and remove the current creature
* from the enemie's inverse threat list.
*
* @param oCreature the creature perceiving the enemy
* @param oEnemy the hostile creature by perceived by oCreature
* @author Yaron
*/
     public void AI_Threat_UpdateEnemyDisappeared(GameObject oCreature, GameObject oEnemy)
     {
#if DEBUG
          Log_Trace_Threat("AI_Threat_UpdateEnemyDisappeared", "Enemy:" + GetTag(oEnemy));
          AI_Threat_Display(oCreature, "Enemy disappeared, before changing threat");
#endif
          ClearEnemyThreat(oCreature, oEnemy);
#if DEBUG
          Log_Trace_Threat("AI_Threat_UpdateEnemyDisappeared", "ENEMY CLEARED FROM LIST");
#endif
          // Check if the enemy was my target:
          GameObject oCurrent = AI_Threat_GetThreatTarget(oCreature);
          GameObject oMostHated = GetThreatEnemy(oCreature, 0);
          if (oCurrent == oEnemy)
          {
               if (IsObjectValid(oMostHated) != EngineConstants.FALSE && IsDead(oMostHated) == EngineConstants.FALSE)
                    AI_Threat_SetThreatTarget(oCreature, oMostHated);
               else // last enemy
                    AI_Threat_SetThreatTarget(oCreature, null);
          }
#if DEBUG
          AI_Threat_Display(oCreature, "Enemy disappeared, after clearing threat");
#endif
     }

     /* @brief Updates threat based on a creature attacking
*
* Update the threat for the current creature based on the attacking enemy.
*
* @param oCreature the creature perceiving the enemy
* @param oAttacker the hostile creature attacking oCreature
* @author Yaron
*/
     public void AI_Threat_UpdateEnemyAttacked(GameObject oCreature, GameObject oAttacker)
     {
          // Updating 'attacked threat' only if threat towards the target is lower
          // or equal to 'attacked threat' value. This is done in order to prevent
          // adding the 'attacked threat' value again and again for each attack.
          float fAttackerThreat = GetThreatValueByObjectID(oCreature, oAttacker);
          if (fAttackerThreat >= EngineConstants.AI_THREAT_VALUE_ATTACKED)
               return; // too high to update

#if DEBUG
          Log_Trace_Threat("AI_Threat_UpdateEnemyAttacked", "Attacker:" + GetTag(oAttacker));
          AI_Threat_Display(oCreature, "Enemy attacked, before changing threat");
#endif
          AI_Threat_UpdateCreatureThreat(oCreature, oAttacker, EngineConstants.AI_THREAT_VALUE_ATTACKED);
     }

     /* @brief Updates threat based on a creature dying
*
* This function clears the dead creature's threat list.
* Any other creatures that have this creature in their threat list are clearing it out on the disappear event.
*
* @param oDeadCreature the dead creature
* @author Yaron
*/
     public void AI_Threat_UpdateDeath(GameObject oDeadCreature)
     {
#if DEBUG
          Log_Trace_Threat("AI_Threat_UpdateDeath", "Dead Creature:" + GetTag(oDeadCreature));
          AI_Threat_Display(oDeadCreature, "update death, before clearing table");
#endif
          ClearThreatTable(oDeadCreature);
#if DEBUG
          AI_Threat_Display(oDeadCreature, "update death, after clearing table");
#endif
     }

     // Returns the 2da value for ability threat impact
     public float AI_Threat_GetAbilityImpactThreat(int nAbilityID)
     {
          return GetM2DAFloat(EngineConstants.TABLE_ABILITIES_TALENTS, "threat_impact", nAbilityID);
     }

     // Returns the 2da value for ability threat use
     public float AI_Threat_GetAbilityUseThreat(int nAbilityID)
     {
          return GetM2DAFloat(EngineConstants.TABLE_ABILITIES_TALENTS, "threat_use", nAbilityID);
     }

     /* @brief returns updates threat value based on hated targets
*
* If the creature hates the threat target because of race, gender or class - the threat may increase.
*
* @param oCreature the hating creature
* @param oThreatTarget the hated creature
* @param fThreat the threat before being changed
* @returns the updated threat, if any, based on hated target
* @author Yaron
*/
     public float AI_Threat_GetHatedThreat(GameObject oCreature, GameObject oThreatTarget, float fThreat)
     {
          int nHatedRace = GetLocalInt(oCreature, EngineConstants.AI_THREAT_HATED_RACE);
          int nHatedClass = GetLocalInt(oCreature, EngineConstants.AI_THREAT_HATED_CLASS);
          int nHatedGender = GetLocalInt(oCreature, EngineConstants.AI_THREAT_HATED_GENDER);

          if (nHatedRace == GetCreatureRacialType(oThreatTarget))
               fThreat *= EngineConstants.AI_THREAT_HATED_ENEMY_COEFFICIENT;

          if (nHatedClass == GetCreatureCoreClass(oCreature))
               fThreat *= EngineConstants.AI_THREAT_HATED_ENEMY_COEFFICIENT;

          if (nHatedGender == GetCreatureGender(oThreatTarget))
               fThreat *= EngineConstants.AI_THREAT_HATED_ENEMY_COEFFICIENT;

          return fThreat;
     }

     /* @brief Handle the update of threat for a single creature
*
* Do any global handling and call the engine function that handles most of the logic
*
* @param oCreature the hating creature
* @param oEnemy the hated creature
* @param fThreatChange the threat before being changed
* @author Yaron
*/
     public void AI_Threat_UpdateCreatureThreat(GameObject oCreature, GameObject oEnemy, float fThreatChange)
     {
#if DEBUG
          AI_Threat_Display(oCreature, "BEFORE UPDATING THREAT");
#endif

          if (IsObjectValid(oEnemy) != EngineConstants.FALSE)
          {
               if (IsDead(oEnemy) != EngineConstants.FALSE) // no threat towards dead enemy
               {
#if DEBUG
                    Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "Can not update threat against a dead enemy");
#endif
                    return;
               }

               if (IsObjectHostile(oCreature, oEnemy) == EngineConstants.FALSE)
                    return; // doing nothing if both objects are not hostile towards each other
               fThreatChange = AI_Threat_GetHatedThreat(oCreature, oEnemy, fThreatChange);

               if (fThreatChange > 0.0f && fThreatChange < EngineConstants.AI_THREAT_MIN_CHANGE)
                    fThreatChange = EngineConstants.AI_THREAT_MIN_CHANGE;

               float fCurrentThreat = GetThreatValueByObjectID(oCreature, oEnemy);
               if (fThreatChange < 0.0f && fCurrentThreat + fThreatChange <= 0.0f)
                    fThreatChange = (-1) * (fCurrentThreat - 1.0f);

               if (fThreatChange > 0.0f && fCurrentThreat + fThreatChange > EngineConstants.AI_THREAT_MAX)
                    fThreatChange = EngineConstants.AI_THREAT_MAX - fCurrentThreat;

#if DEBUG
               Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "Enemy: " + GetTag(oEnemy) + ", Threat Change: " + FloatToString(fThreatChange));
#endif

               UpdateThreatTable(oCreature, oEnemy, fThreatChange);

#if DEBUG
               AI_Threat_Display(oCreature, "AFTER ADDING NEW CREATURE TO TABLE");
#endif
          }
          else
          {
#if DEBUG
               Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "Update check for threat target (no specific enemy)");
#endif
          }

          // Check if we can update the threat target (if different from current)
          GameObject oMostHated = GetThreatEnemy(oCreature, 0); // 0 is the most hated
          GameObject oCurrentTarget = AI_Threat_GetThreatTarget(oCreature, EngineConstants.FALSE); // current threat target
          int nNewMaxTimer;
          int nCurrentTime = GetTime();
          int nCurrentMaxTimer = GetLocalInt(oCreature, EngineConstants.AI_THREAT_SWITCH_TIMER_MIN);

#if DEBUG
          Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "Most Hated: " + GetTag(oMostHated) + ", Current Target: " + GetTag(oCurrentTarget));
#endif

          if (IsObjectValid(oCurrentTarget) == EngineConstants.FALSE) // setting target for the first time
          {
               AI_Threat_SetThreatTarget(oCreature, oMostHated);
               SetLocalInt(oCreature, EngineConstants.AI_THREAT_TARGET_SWITCH_COUNTER, GetTime());
          }
          else if (oMostHated != oCurrentTarget) // if they are different - try to update threat target to be most hated
          {
               // After the first switch - set a timer great then 0 (it was 0 until now to allow initial quick switching)

               // Check if to keep wounded target
               if (IsObjectValid(oEnemy) != EngineConstants.FALSE && AI_Threat_KeepWoundedTarget(oEnemy) != EngineConstants.FALSE)
               {
#if DEBUG
                    Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "Keeping wounded target (not checking any other threat rules)");
#endif
               }
               else // target not wounded enough, or decided not to keep it
               {
                    if (AI_Threat_ClearToSwitchTarget(oCreature) != EngineConstants.FALSE || IsPerceiving(gameObject, oCurrentTarget) == EngineConstants.FALSE || IsDead(oCurrentTarget) != EngineConstants.FALSE || IsDying(oCurrentTarget) != EngineConstants.FALSE)
                    {
#if DEBUG
                         Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "Clear to switch threat target");
#endif
                         // init timer
                         SetLocalInt(oCreature, EngineConstants.AI_THREAT_TARGET_SWITCH_COUNTER, nCurrentTime);
                         // Update target to be the most hated target
                         AI_Threat_SetThreatTarget(oCreature, oMostHated);

                         nNewMaxTimer = nCurrentMaxTimer + EngineConstants.AI_THREAT_SWITCH_TIMER_JUMP;
                         if (nNewMaxTimer > EngineConstants.AI_THREAT_SWITCH_TIME_MAX)
                              nNewMaxTimer = 0;
#if DEBUG
                         Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "New threat timer value: " + IntToString(nNewMaxTimer));
#endif

                         SetLocalInt(oCreature, EngineConstants.AI_THREAT_SWITCH_TIMER_MIN, nNewMaxTimer);
                    }
                    else
                    {
#if DEBUG
                         Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "NOT Clear to switch threat target");
#endif
                    }
               }
          }

          if (IsObjectValid(oEnemy) != EngineConstants.FALSE)
          {
               float fNewThreat = GetThreatValueByObjectID(oCreature, oEnemy);
#if DEBUG
               Log_Trace_Threat("AI_Threat_UpdateCreatureThreat", "Threat updated against enemy: " + GetTag(oEnemy) + ", new value: " + FloatToString(fNewThreat));
#endif
          }
#if DEBUG
          AI_Threat_Display(oCreature, "AFTER UPDATING THREAT");
#endif
     }

     // Returns EngineConstants.TRUE if this target should be kept, on top of any other threat rules
     public int AI_Threat_KeepWoundedTarget(GameObject oTarget)
     {
          float fCurrentHealth = GetCurrentHealth(oTarget);
          float fMaxHealth = GetMaxHealth(oTarget);

          if (fMaxHealth <= 0.0f) fMaxHealth = 1.0f;

          if (fCurrentHealth < 0.0f) fCurrentHealth = 0.0f;

          float fHealthRatio = fCurrentHealth / fMaxHealth;
          int nRand;
#if DEBUG
          Log_Trace_Threat("AI_Threat_KeepWoundedTarget", "Target health ratio:" + FloatToString(fHealthRatio) + "%");
#endif

          if (fHealthRatio <= 0.2 && fHealthRatio > 0.1)
          {
#if DEBUG
               Log_Trace_Threat("AI_Threat_KeepWoundedTarget", "Target health lower than 20%, chance of keeping target:" + IntToString(EngineConstants.AI_THREAT_CHANCE_KEEP_TARGET_20_HEALTH) + "%");
#endif
               nRand = Engine_Random(100) + 1;
               if (nRand <= EngineConstants.AI_THREAT_CHANCE_KEEP_TARGET_20_HEALTH)
                    return EngineConstants.TRUE;

          }
          else if (fHealthRatio <= 0.1)
          {
#if DEBUG
               Log_Trace_Threat("AI_Threat_KeepWoundedTarget", "Target health lower than 10%, chance of keeping target:" + IntToString(EngineConstants.AI_THREAT_CHANCE_KEEP_TARGET_10_HEALTH) + "%");
#endif
               nRand = Engine_Random(100) + 1;
               if (nRand <= EngineConstants.AI_THREAT_CHANCE_KEEP_TARGET_10_HEALTH)
                    return EngineConstants.TRUE;
          }

          return EngineConstants.FALSE;
     }

     // This is fired when a stantionary creature is trying to attack a target that is beyond the range of his
     // abilities or with no line of sight.
     public void AI_Threat_UpdateCantAttackTarget(GameObject oCreature, GameObject oEnemy)
     {
          if (IsObjectHostile(oCreature, oEnemy) == EngineConstants.FALSE)
               return; // doing nothing if both objects are not hostile towards each other
#if DEBUG
          AI_Threat_Display(oCreature, "cant attack target before changing threat");
          Log_Trace_Threat("AI_Threat_UpdateCantAttackTarget", "oEnemy:" + GetTag(oEnemy));
#endif

          float fThreatChange = (-1) * GetThreatValueByObjectID(oCreature, oEnemy) * EngineConstants.AI_THREAT_CANT_ATTACK_TARGET_REDUCTION;

          // clear the switch timer (can't attack current anyways)
          SetLocalInt(oCreature, EngineConstants.AI_THREAT_TARGET_SWITCH_COUNTER, 0);
          AI_Threat_UpdateCreatureThreat(oCreature, oEnemy, fThreatChange);

     }

     public void AI_Threat_Display(GameObject oCreature, string sMes)
     {
          int i;
          GameObject oCurrent;
          Log_Trace(EngineConstants.LOG_CHANNEL_THREAT_DATA, "THREAT DUMP", "START: " + sMes);
          GameObject oTarget = GetLocalObject(oCreature, EngineConstants.AI_THREAT_TARGET);

          Log_Trace(EngineConstants.LOG_CHANNEL_THREAT_DATA, "THREAT", "CURRENT TARGET: " + GetTag(oTarget));
          int nSize = GetThreatTableSize(oCreature);
          if (nSize == 0 && IsObjectValid(oTarget) != EngineConstants.FALSE)
               Log_Trace(EngineConstants.LOG_CHANNEL_THREAT_DATA, "THREAT", "ERROR! got a threat target while table is empty!");
          for (i = 0; i < nSize; i++)
          {
               oCurrent = GetThreatEnemy(oCreature, i);
               Log_Trace(EngineConstants.LOG_CHANNEL_THREAT_DATA, "THREAT", "Enemy " + IntToString(i) + ": " + GetTag(oCurrent) + ", threat: " + FloatToString(GetThreatValueByIndex(oCreature, i)));
          }
          Log_Trace(EngineConstants.LOG_CHANNEL_THREAT_DATA, "THREAT", "END");
     }

     // switch check
     public int AI_Threat_ClearToSwitchTarget(GameObject oCreature)
     {
          int nSwitchTime = GetLocalInt(oCreature, EngineConstants.AI_THREAT_TARGET_SWITCH_COUNTER); // the time stamp when the target switched last time
          int nCurrentTime = GetTime();
          int nCurrentMaxTimer;
          if (IsUsingMeleeWeapon(oCreature) != EngineConstants.FALSE)
               nCurrentMaxTimer = GetLocalInt(oCreature, EngineConstants.AI_THREAT_SWITCH_TIMER_MIN);
          else // using ranged attack -> can switch much faster
               nCurrentMaxTimer = 0;

#if DEBUG
          Log_Trace_Threat("AI_Threat_ClearToSwitchTarget", "creature: " + GetTag(oCreature) + ", current time: " + IntToString(nCurrentTime)
           + ", switch time: " + IntToString(nSwitchTime) + ", current max: " + IntToString(nCurrentMaxTimer));
#endif

          return (nCurrentTime - nSwitchTime >= nCurrentMaxTimer) ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     // Clears the creature's threat from all of his enemies. This is used when the creature
     // acquires effects like root or sleep that should make enemies stop attacking this creature
     public void AI_Threat_ClearEnemiesThreatToMe(GameObject oCreature)
     {
#if DEBUG
          Log_Trace_Threat("AI_Threat_ClearEnemiesThreatToMe", "Clearing threat of all enemies towards this creature: " + GetTag(oCreature));
#endif

          int nEnemies = GetThreatTableSize(oCreature);
          int i;
          GameObject oEnemy;
          float fThreat;
          for (i = 0; i < nEnemies; i++)
          {
               oEnemy = GetThreatEnemy(oCreature, i);
#if DEBUG
               Log_Trace_Threat("AI_Threat_ClearEnemiesThreatToMe", "Enemy: " + GetTag(oEnemy) + " - threat set to 0 towards: " + GetTag(oCreature));
#endif
               fThreat = GetThreatValueByObjectID(oEnemy, oCreature);
               UpdateThreatTable(oEnemy, oCreature, fThreat * -1);
               // Clearing enemy to switch target if this creature was also his threat target
               if (AI_Threat_GetThreatTarget(oEnemy, EngineConstants.FALSE) == oCreature)
               {
                    SetLocalInt(oCreature, EngineConstants.AI_THREAT_TARGET_SWITCH_COUNTER, 0);
#if DEBUG
                    Log_Trace_Threat("AI_Threat_ClearEnemiesThreatToMe", "Enemy: " + GetTag(oEnemy) + " - clearing threat timer (this creature was threat target)");
#endif
               }
          }
     }

     /*public void main()
     {
         GameObject oBoom = AI_Threat_GetThreatTarget(gameObject);
         return;
     }*/

     /* @brief Updates threat based on healing
*
* Updating the threat for all enemies by creating an AOE xEffect around the healing
* creature. Each enemy will get the xEffect data and update the threat towards the healing creature.
*
* @param oCreature the creature being healed
* @param oHealer the healing creature
* @param nHealthHealed the amount of damage being healed
* @author Yaron
*/
     //public void AI_Threat_UpdateHealing(GameObject oCreature, GameObject oHealer, float fHealthHealed);

     /* @brief Updates threat based on ablity used
     *
     * Updating the threat for all enemies by creating an AOE xEffect around the creature using the ability.
     * Each enemy will get the xEffect data and update the threat towards the creature using the ability.
     *
     * The threat change is ability-specific (Ability Use Threat)
     *
     * @param oCreature the creature using the ability
     * @param nAbilityID the ability type being used
     * @author Yaron
     */
     //public void AI_Threat_UpdateAbilityUsed(GameObject oCreature, int nAbilityID);

     /* @} */
}