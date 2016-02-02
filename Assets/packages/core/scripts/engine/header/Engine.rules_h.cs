//ready
//this class is obsolete and kept for educational purposes
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
     ///////////////////////////////////////////////////
     //
     //  rules_h
     //
     // Include file used to organize rules related functions.
     // Owner: Brent Knowles
     //
     // Note to Georg or Yaron: We will have to handle "item scaling"
     // in this script. I have some imcomplete hacks but we'll need
     // to talk the programmers about how we'll implement the scaling as discussed
     // here:  http://staff.bioware.com/wiki/index.html?page=CombatRules&wiki=eclipse&action=view
     //
     ///////////////////////////////////////////////////

     //#include"ability_h"
     //#include"sys_gore_h"
     //#include"config_h"
     //#include"combat_h"
     //#include"2da_constants_h"

     // -----------------------------------------------------------------------------
     // Max creatures in combat at the same time we can support.
     // -----------------------------------------------------------------------------
     //moved public const int EngineConstants.MAX_CREATURES_IN_COMBAT = 24;

     /* @brief <brief description>
     *
     * Wrapper function to pull the currently equipped weapon's "make modifier"
     * @param <param - first parameter
     *
     * @returns <what>
     */
     public float Rules_GetWeaponMakeModifier(GameObject oAttacker)
     {
          return 1.0f; // need GetM2DAFloat for this to work properly
     }
     /* @brief <brief description>
     *
     * Wrapper function to pull the currently equipped weapon's absolute speed
     * @param <param - first parameter
     *
     * @returns <what>
     */
     public float Rules_GetWeaponSpeed(GameObject oAttacker)
     {
          return 1.5f; // need GetM2DAFloat for this to work properly
     }
     /* @brief <brief description>
     *
     * returns a Control Panel number (from 2da) that influences damage scaling
     * @param <param - first parameter
     *
     * @returns <what>
     */
     public int Rules_GetBaseDamageScaler(GameObject oAttacker)
     {
          return GetM2DAInt(EngineConstants.TABLE_SPELL_CONJURINGSPEED, "DamageBase", 1);
     }
     /* @brief <brief description>
     *
     * returns a Control Panel number (from 2da) that influences damage scaling
     * @param <param - first parameter
     *
     * @returns <what>
     */
     public int Rules_GetAttackScaler(GameObject oAttacker)
     {
          return GetM2DAInt(EngineConstants.TABLE_SPELL_CONJURINGSPEED, "AttackModifier", 1);
     }
     /* @brief <brief description>
     *
     * Tempoary function due to a bug. Looks up 2da for weapon type and
     * returns the "CombatRating" column
     * @param <param - first parameter
     *
     * @returns <what>
     */
     public int Rules_GetWeaponAttackRating(GameObject oAttacker)
     {
          // return GetM2DAInt(111, "CombatRating", 2);
          //return FloatToInt(GetWeaponAttackRating(oAttacker));
          return 0;
     }
     /* @brief <brief description>
     *
     * Tempoary function due to a bug. Looks up 2da for weapon type and
     * returns the "CombatRating" column
     * @param <param - first parameter
     *
     * @returns <what>
     */
     public float Rules_GetModifiedWeaponAttackRating(GameObject oAttacker)
     {
          // TBD: run the rules here

          return 7.0f;
     }

     public float Rules_GetWeaponArmorPenetration(GameObject oAttacker, GameObject oTarget)
     {
          // returns calculated number based on 2da lookup for material type
          float fValue = GetWeaponArmorPenetration(oAttacker, oTarget);
          return 1.05f * fValue;
     }
     /*int Rules_GetArmor(GameObject oAttacker)
     {
     // returns calculated number based on 2da lookup for material type
         //int nValue = GetCreatureArmor(oAttacker);
         //return nValue;
         return 0;
     }*/

     /*-----------------------------------------------------------------------------
     * @brief does an ability resistance check
     *
     * Calculates resistance based on attacker and target. Returning EngineConstants.TRUE if attack
     * has been resisted, EngineConstants.FALSE otherwise
     * @param oAttacker creature triggering the ability
     * @param oTarget creature defending against oAttacker's ability
     *
     * @returns EngineConstants.TRUE for resisting, EngineConstants.FALSE for failing to resist
     *
     * @author Brent
     * -----------------------------------------------------------------------------*/
     /*
     public int Rules_ResistanceCheck(GameObject oAttacker, GameObject oTarget);
     public int Rules_ResistanceCheck(GameObject oAttacker, GameObject oTarget)
     {
         // TBD: put real rules here

         int nRand = Random(100);
         int nRet;
         if(nRand < 60)
             nRet = EngineConstants.RESISTANCE_CHECK_FAILURE; // always failing
         else
             nRet = EngineConstants.RESISTANCE_CHECK_SUCCESS;

         return nRet;
     }
     */

     // -----------------------------------------------------------------------------
     // This code figures out the Min Damage a weapon does and the max damage a weapon does.
     // Remember that a weapon's speed AND the values in the base items 2da work together
     // using a formula to create the min damage and the maximum damage.
     // -----------------------------------------------------------------------------

     public int Rules_GetWeaponMinDamage(GameObject oAttacker)
     {   //DEBUG_PrintToScreen("A");
          int nBaseDamage2DA = Rules_GetBaseDamageScaler(oAttacker);
          //DEBUG_PrintToScreen(IntToString(nBaseDamage2DA));
          //DEBUG_PrintToScreen(IntToString(nBaseDamage2DA));
          float fMaterialWeaponModifier = Rules_GetWeaponMakeModifier(oAttacker); // need GetM2DAFloat for this to work properly
          int nWeaponBaseDamage = 0;//GetWeaponBaseDamage(oAttacker);
          //DEBUG_PrintToScreen(IntToString(nWeaponBaseDamage));
          int nTotal = (nWeaponBaseDamage + nBaseDamage2DA);
          //DEBUG_PrintToScreen("599");
          float fValue = nTotal * fMaterialWeaponModifier;
          int nBaseDamage = FloatToInt(fValue);

          return nBaseDamage - 1;
     }
     // determines the max weapon damage based on the rules formula
     public int Rules_GetWeaponMaxDamage(int nMin, GameObject oAttacker)
     {
          float fSpeed = Rules_GetWeaponSpeed(oAttacker);
          int nDamageBase = GetM2DAInt(EngineConstants.TABLE_SPELL_CONJURINGSPEED, "DamageRange", 1); ;
          //DEBUG_PrintToScreen(IntToString(nDamageBase));
          int nValue = nMin + FloatToInt((fSpeed * IntToFloat(nDamageBase)));
          return nValue;
     }

     /* ----------------------------------------------------------------------------
     * @brief Checks oCreature is currently casting a spell and if so, performs a check
     *        according to the rules to see if it is interrupted.
     *
     * @param oCreature          The Creature being damaged
     * @param nDamage            The Damage Received
     * @param nDamageType        The Damage Type Received
     *
     * @author   Georg Zoeller
     *  --------------------------------------------------------------------------**/
     public int Rules_CheckSpellInterruption(GameObject oCreature, int nDamage, int nDamageType)
     {

          // -------------------------------------------------------------------------
          // Check spell interruption
          // -------------------------------------------------------------------------
          if (IsConjuring(gameObject)!= EngineConstants.FALSE)
          {
               Log_Rules("rules_core->EVENT_TYPED_DAMAGED->Rules_CheckSpellInterruption: got DAMAGED xEvent while conjuring a spell - interrupting spell!",
               EngineConstants.LOG_LEVEL_WARNING, gameObject);

               // ---------------------------------------------------------------------
               // Get config mode setting from 2da. Possible Options:
               // Spell Interruption Mode Simple - Any attack cancels our spell
               // ---------------------------------------------------------------------
               if (Config_GetSetting(EngineConstants.CONFIG_SETTING_SPELLINTERRUPTION) == EngineConstants.CONFIG_VALUE_SPELLINTERRUPTION_SIMPLE)
               {
                    return EngineConstants.TRUE;
               }
          }
          return EngineConstants.FALSE;
     }

     /* ----------------------------------------------------------------------------
     * @brief Interrupts a creature's spell (by cancelling the current command)
     *
     * @param oInterrupt The Creature being interrupted
     *
     * @author Georg Zoeller
     *  --------------------------------------------------------------------------**/
     public void Rules_InterrupSpell(GameObject oInterrupt)
     {
          xCommand cCurrent = GetCurrentCommand(gameObject);
          WR_RemoveCommand(gameObject, cCurrent);
     }
}