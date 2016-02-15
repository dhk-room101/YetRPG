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
    // ai_conditions_h
    //
    // This script includes all tactics conditions related code
    //
    // Owner: Yaron Jakobs
    //
    /////////////////////////////////////////////

    /* @addtogroup scripting_ai_conditions Scripting AI Conditions handling
    *
    * This script includes all tactics conditions related code
    */
    /* @{*/

    //#include"ai_constants_h"
    //#include"log_h"
    //#include"ability_h"
    //#include"items_h"
    //#include"ai_threat_h"
    //#include"party_h"
    //#include"global_objects_h"
    //#include"ai_behaviors_h"
    //#include"monster_constants_h"

    /* @brief Checks if a creature is vulnerable to a specific damage type
*
* @returns EngineConstants.TRUE if the creature is vulnerable, EngineConstants.FALSE otherwise
* @author Yaron
*/
    public int _AI_IsVulnerableToDamage(GameObject oCreature, int nDamageType)
    {
        // CUT
        int nRet = EngineConstants.FALSE;

        return nRet;
    }

    /* @brief Returns a creature with a specifc AI Status
*
* When looking for a target of type SELF: the function will return gameObject if the status is valid
* When looking for a target of type ALLY: the function will return the nearest ally with the status
* When looking for a target of type ENEMY: the function will return the nearest enemy with the status
*
* @param nAIStatus the status being checked for if active on a creature
* @param nTargetType the target type of creature we are looking for that might have the specified status
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns A creature of a specific type (self, ally, hostile) that has the status applied, null otherwise
* @author Yaron
*/
    public GameObject _AI_Condition_GetCreatureWithAIStatus(int nAIStatus, int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetCreatureWithAIStatus", "[START] AI Status:" + IntToString(nAIStatus) + ", Target Type: " + IntToString(nTargetType));
#endif

        GameObject oTarget = null;
        List<GameObject> arCreatures = new List<GameObject>();
        int nSize = 0;
        GameObject oCurrentCreature;
        int i;

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF:
                {
                    // Making sure we're not trying to apply an ability that is already active
                    if (nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY && Ability_IsAbilityActive(gameObject, nCommandSubType) == EngineConstants.FALSE)
                        arCreatures[0] = gameObject;
                    else if (nCommandType != EngineConstants.AI_COMMAND_USE_ABILITY) // not an ability - can use self as target
                        arCreatures[0] = gameObject;

                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    // Getting a list of the nearest creatures with the same group, alive and perceived, not including self
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);

                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    // Getting a list of the nearest hostiles, alive and preceived, not including self:
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);

                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    arCreatures[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
            default: // It can still be a party target
                {
                    arCreatures[0] = _AI_GetPartyTarget(nTargetType, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
        }

        nSize = GetArraySize(arCreatures);
        for (i = 0; i < nSize; i++)
        {
            oCurrentCreature = arCreatures[i];

            if (_AI_HasAIStatus(oCurrentCreature, nAIStatus) != EngineConstants.FALSE)
            {
                oTarget = oCurrentCreature;
                break;
            }
        }

        return oTarget;
    }

    /* @brief Returns a creature with a specific HP level
*
* When looking for a target of type SELF: the function will return gameObject if the creature HP at the specified level
* When looking for a target of type ALLY: the function will return the nearest ally HP with the specified level
* When looking for a target of type ENEMY: the function will return the nearest enemy with HP of the specified level
*
* @params nHPLevel a percentage value (25, 50, etc') representing a wounded level. A creature is considered 'wounded'
* for that level if his HP are below the level specified. For positive values: the check is greater or equal (50% ->
* creature >= 50%), for negative values: the check is lower (-50% -> creature < 50%)
* @params nTargetType the target type of creature we are looking for that might have his HP at the specified level
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns A creature of a specific type (self, ally, hostile) that has his HP are at the specified level, null otherwise
* @author Yaron
*/
    public GameObject _AI_Condition_GetCreatureWithHPLevel(int nHPLevel, int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetCreatureWithHPLevel", "[START] HP Level: " + IntToString(nHPLevel));
#endif
        return _AI_SubCondition_GetCreatureWithStatLevel(EngineConstants.AI_STAT_TYPE_HP, nHPLevel, nTargetType, nCommandType, nCommandSubType, nTacticID);
    }

    // Returns the nearest target of the specified type if SELF HP level is valid
    public GameObject _AI_Condition_SelfHPLevel(int nHPLevel, int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_SelfHPLevel", "[START] HP Level: " + IntToString(nHPLevel));
#endif

        GameObject oTarget = _AI_SubCondition_GetCreatureWithStatLevel(EngineConstants.AI_STAT_TYPE_HP, nHPLevel, EngineConstants.AI_TARGET_TYPE_SELF, nCommandType, nCommandSubType, nTacticID);
        if (oTarget != gameObject)
            return null;

        List<GameObject> arCreatures = new List<GameObject>();

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    arCreatures[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_SelfHPLevel", "INVALID Target Type: " + IntToString(nTargetType));
#endif
                    break;
                }
        }

        return arCreatures[0];
    }

    // Returns the nearest target of the specified type if SELF Mana or stamina level is valid
    public GameObject _AI_Condition_SelfManaStaminaLevel(int nManaStaminaLevel, int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_SelfManaStaminaLevel", "[START] Mana/Stamina Level: " + IntToString(nManaStaminaLevel));
#endif

        GameObject oTarget = _AI_SubCondition_GetCreatureWithStatLevel(EngineConstants.AI_STAT_TYPE_MANA_OR_STAMINA, nManaStaminaLevel, EngineConstants.AI_TARGET_TYPE_SELF, nCommandType, nCommandSubType, nTacticID);
        if (oTarget != gameObject)
            return null;

        List<GameObject> arCreatures = new List<GameObject>();

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    arCreatures[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_SelfManaStaminaLevel", "INVALID Target Type: " + IntToString(nTargetType));
#endif
                    break;
                }
        }

        return arCreatures[0];
    }

    /* @brief Returns a creature with a specific Mana or Stamina level
*
* When looking for a target of type SELF: the function will return gameObject if the creature Mana or Stamina at the specified level
* When looking for a target of type ALLY: the function will return the nearest ally Mana or Stamina with the specified level
* When looking for a target of type ENEMY: the function will return the nearest enemy with Mana or Stamina of the specified level
*
* @params nManaOrStaminaLevel a percentage value (25, 50, etc') representing a wounded level. A creature is considered 'wounded'
* for that level if his Mana are below the level specified. For positive values: the check is greater or equal (50% ->
* creature >= 50%), for negative values: the check is lower (-50% -> creature < 50%)
* @params nTargetType the target type of creature we are looking for that might have his Mana or Stamina at the specified level
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns A creature of a specific type (self, ally, hostile) that has his Mana or Stamina are at the specified level, null otherwise
* @author Yaron
*/
    public GameObject _AI_Condition_GetCreatureWithManaOrStaminaLevel(int nManaOrStaminaLevel, int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetCreatureWithManaOrStaminaLevel", "[START] Mana or Stamina Level: " + IntToString(nManaOrStaminaLevel));
#endif
        return _AI_SubCondition_GetCreatureWithStatLevel(EngineConstants.AI_STAT_TYPE_MANA_OR_STAMINA, nManaOrStaminaLevel, nTargetType, nCommandType, nCommandSubType, nTacticID);
    }

    /* @brief A sub-condition function for the HPLevel and ManaStaminaLevel condition functions
*
* Contains all the logic for all related condition functions
*
* @params nStatType the stat type we are checking the level for (EngineConstants.AI_STAT_TYPE_*)
* @params nStatLevel a percentage value (25, 50, etc') representing a wounded level. A creature is considered 'wounded'
* for that level if his Mana are below the level specified. For positive values: the check is greater or equal (50% ->
* creature >= 50%), for negative values: the check is lower (-50% -> creature < 50%)
* @params nTargetType the target type of creature we are looking for that might have his Mana or Stamina at the specified level
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns A creature of a specific type (self, ally, hostile) that has his stat at the specified level, null otherwise
* @author Yaron
*/
    public GameObject _AI_SubCondition_GetCreatureWithStatLevel(int nStatType, int nStatLevel, int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_SubCondition_GetCreatureWithStatLevel", "[START] Stat level: " + IntToString(nStatLevel));
#endif
        GameObject oTarget = null;
        List<GameObject> arCreatures = new List<GameObject>();
        int nSize = 0;
        GameObject oCurrentCreature;
        int i;
        float fDistance;

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF:
                {

                    // Making sure we're not trying to apply an ability that is already active
                    if (nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY && Ability_IsAbilityActive(gameObject, nCommandSubType) == EngineConstants.FALSE)
                        arCreatures[0] = gameObject;
                    else if (nCommandType != EngineConstants.AI_COMMAND_USE_ABILITY) // not an ability - can use self as target
                        arCreatures[0] = gameObject;

                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    // Getting a list of the nearest creatures with the same group, alive and perceived, not including self
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    // Getting a list of the nearest hostiles, alive and preceived, not including self:
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    arCreatures[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
            default: // It can still be a party target
                {
                    arCreatures[0] = _AI_GetPartyTarget(nTargetType, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
        }

        // If we have an array, and don't have a target yet, then try to find the target in the array
        nSize = GetArraySize(arCreatures);

        // This loops will be evaluated only if any array was generated above
        for (i = 0; i < nSize; i++)
        {
            oCurrentCreature = arCreatures[i];

            if (_AI_HasStatLevel(oCurrentCreature, nStatType, nStatLevel) != EngineConstants.FALSE)
            {
                oTarget = oCurrentCreature;
                break; // got what we need
            }

        }

        return oTarget;
    }

    /* @brief Returns the nth most damaged creature in a group
*
* When looking for a target of type ALLY: the function will return the Nth most damaged ally
* When looking for a target of type ENEMY: the function will return the Nth most damaged enemy
*
* @params nMostDamagedNum to specify N for the Nth most damaged enemy
* @params nTargetType the target type of group we're looking for the most damaged enemy in
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns the Nth most damaged creature in the group
* @author Yaron
*/
    public GameObject _AI_Condition_GetNthMostDamagedCreatureInGroup(int nHighLow, int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetNthMostDamagedCreatureInGroup (=> get creature with highest or lowest health)", "START");
#endif

        GameObject oTarget = null;
        List<GameObject> arCreatures = new List<GameObject>();

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    // Getting a list of the nearest creatures with the same group, alive and perceived, not including self
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    // Getting a list of the nearest hostiles, alive and preceived, not including self:
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_GetNthMostDamagedCreatureInGroup", "INVALID Target Type: " + IntToString(nTargetType));
#endif
                    break;
                }
        }

        oTarget = _AI_GetNthDamagedCreatureInArray(arCreatures, nHighLow);

        return oTarget;
    }

    /* @brief Returns the nearest creature clustered with the same group of a minimum size
*
* It is assumed the cluster is for a generic AOE size, a bit smaller than a fireball.
* The function only looks for enemies and does not care for distance or if the enemies are moving or not.
*
* @params nMinClusterSize the minimum number of enemies to be considered clustered
* @returns the center Vector3 of the cluster
* @author Yaron
*/
    public Vector3 _AI_Condition_GetEnemyClusteredWithSameGroup(int nMinClusterSize, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetEnemyClusteredWithSameGroup", "START");
#endif
        int nAllyFailChance = 0; // will be higher the smaller the cluster is

        switch (nCommandSubType)
        {
            case EngineConstants.ABILITY_SPELL_INFERNO: nAllyFailChance = 100; break;
            case EngineConstants.ABILITY_SPELL_FIREBALL: nAllyFailChance = 75; break;
            case EngineConstants.ABILITY_SPELL_MASS_PARALYSIS: nAllyFailChance = 0; break;
            case EngineConstants.ABILITY_SPELL_GREASE: nAllyFailChance = 50; break;
            case EngineConstants.ABILITY_SPELL_EARTHQUAKE: nAllyFailChance = 100; break;
            case EngineConstants.ABILITY_SPELL_SLEEP: nAllyFailChance = 0; break;
            case EngineConstants.ABILITY_SPELL_BLIZZARD: nAllyFailChance = 75; break;
            case EngineConstants.ABILITY_SPELL_TEMPEST: nAllyFailChance = 75; break;
            case EngineConstants.ABILITY_SPELL_DEATH_CLOUD: nAllyFailChance = 75; break;
        }
        if (IsFollower(gameObject) != EngineConstants.FALSE && nAllyFailChance > 0)
            nAllyFailChance = 100;

        Vector3 lLoc = Vector3.zero;
        if (GetCombatState(gameObject) == EngineConstants.FALSE)
            return lLoc;
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetEnemyClusteredWithSameGroup", "ability: " + IntToString(nCommandSubType) + ", cluster: " + IntToString(nMinClusterSize) + ", ally fail: " + IntToString(nAllyFailChance));
#endif
        lLoc = GetClusterCenter(gameObject, nCommandSubType, nMinClusterSize, nAllyFailChance, EngineConstants.FALSE);
        List<GameObject> arNearest = GetNearestObjectToLocation(lLoc, EngineConstants.OBJECT_TYPE_CREATURE, 1);
        GameObject oNearestToCluster = arNearest[0];
        if (IsObjectValid(oNearestToCluster) == EngineConstants.FALSE)
        {
            Vector3 lInvalid = Vector3.zero;
            return lInvalid;
        }
        float fDistance = GetDistanceBetweenLocations(lLoc, GetLocation(oNearestToCluster));
        if (fDistance > 5.0f)
        {
#if DEBUG
            Log_Trace_AI("_AI_Condition_GetEnemyClusteredWithSameGroup", "can't perceive nearest creature to cluster - invalid cluster");
#endif
            Vector3 lInvalid = Vector3.zero;
            return lInvalid;
        }

        // special case for archdemon abilities - fail if they are too close
        float fClusterDistance = GetDistanceBetweenLocations(GetLocation(gameObject), lLoc);
        if (nCommandSubType == EngineConstants.ARCHDEMON_VORTEX || nCommandSubType == EngineConstants.ARCHDEMON_SMITE)
        {
            if (fClusterDistance < EngineConstants.AI_RANGE_MID_LONG)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetEnemyClusteredWithSameGroup", "Archdemon abilities can't trigger too close");
#endif
                Vector3 lInvalid = Vector3.zero;
                return lInvalid;
            }
        }
        return lLoc;
    }

    /* @brief Returns the Nth most hated enemy
*
* @params n the Nth most hated enemy
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns the Nth most hated enemy
* @author Yaron
*/
    public GameObject _AI_Condition_GetMostHatedEnemy(int n, int nCommandType, int nCommandSubType, int nTacticID)
    {
        GameObject oTarget;
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetMostHatedEnemy", "START, xCommand type: " + IntToString(nCommandType) + ", xCommand sub type: " + IntToString(nCommandSubType));
#endif
        GameObject oTargetOverride = _AI_GetTargetOverride();
        if (IsObjectValid(oTargetOverride) != EngineConstants.FALSE)
            return oTargetOverride;

        int nSize = GetThreatTableSize(gameObject);

        oTarget = AI_Threat_GetThreatTarget(gameObject);

        // final check - if the action is ability and already active then not return this target
        if (nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY && Ability_IsAbilityActive(oTarget, nCommandSubType) != EngineConstants.FALSE)
        {
#if DEBUG
            Log_Trace_AI("_AI_Condition_GetMostHatedEnemy", "Most hated already has this ability applied... aborting");
            oTarget = null;
#endif
        }

        return oTarget;
    }

    /* @brief Returns the Nth nearest visible enemy
*
* @params n Nth nearest visible
* @returns the Nth nearest visible enemy
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @author Yaron
*/
    public GameObject _AI_Condition_GetNearestVisibleCreature(int nTargetType, int n, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetNearestVisibleCreature", "START, creature index: " + IntToString(n));
#endif
        GameObject oTarget = null;
        List<GameObject> arCreatures = new List<GameObject>();

        if (IsFollower(gameObject) != EngineConstants.FALSE && nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
        {
            // A follower should prefer to keep his selected target in this case
            GameObject oSelectedTarget = GetAttackTarget(gameObject);
            if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetNearestVisibleCreature", "Follower - keeping selected target instead of picking nearest");
#endif
                return oSelectedTarget;
            }
        }

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    // Getting a list of the nearest creatures with the same group, alive and perceived, not including self
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    // Getting a list of the nearest hostiles, alive and preceived, not including self:
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_GetNearestVisibleCreature", "INVALID Target Type: " + IntToString(nTargetType));
#endif
                    break;
                }
        }

        if (GetArraySize(arCreatures) + 1 <= n)
        {
#if DEBUG
            Log_Trace_AI("_AI_Condition_GetNearestVisibleCreature", "Invalid creature index requested: " + IntToString(n));
#endif
            return null;
        }

        return arCreatures[n - 1]; // function asks for 1st enemy - but array starts at 0
    }

    /* @brief Returns the nearest visible enemy of a specific race
*
* @params nRace the race of the enemy being looked for
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns the nearest visible enemy of the specified race
* @author Yaron
*/
    public GameObject _AI_Condition_GetNearestVisibleCreatureByRace(int nTargetType, int nRace, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByRace", "START, race: " + IntToString(nRace));
#endif
        List<GameObject> arCreatures = new List<GameObject>();
        int nSize;
        int i;
        GameObject oCurrent;

        if (IsFollower(gameObject) != EngineConstants.FALSE && nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
        {
            // A follower should prefer to keep his selected target in this case
            GameObject oSelectedTarget = GetAttackTarget(gameObject);
            if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByRace", "Follower - keeping selected target instead of picking nearest");
#endif
                return oSelectedTarget;
            }
        }

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    // Getting a list of the nearest creatures with the same group, alive and perceived, not including self
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    // Getting a list of the nearest hostiles, alive and preceived, not including self:
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByRace", "INVALID Target Type: " + IntToString(nTargetType));
#endif
                    break;
                }
        }

        nSize = GetArraySize(arCreatures);

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arCreatures[i];
            if (GetCreatureRacialType(oCurrent) == nRace)
                return oCurrent;
        }

        return null;
    }

    /* @brief Returns the nearest visible enemy of a specific class
*
* @params nClass the class of the enemy being looked for
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns the nearest visible enemy of the specified class
* @author Yaron
*/
    public GameObject _AI_Condition_GetNearestVisibleCreatureByClass(int nTargetType, int nClass, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByClass", "START, class:" + IntToString(nClass));
#endif

        List<GameObject> arCreatures = new List<GameObject>();
        int nSize;
        int i;
        GameObject oCurrent;

        if (IsFollower(gameObject) != EngineConstants.FALSE && nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
        {
            // A follower should prefer to keep his selected target in this case
            GameObject oSelectedTarget = GetAttackTarget(gameObject);
            if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByClass", "Follower - keeping selected target instead of picking nearest");
#endif
                return oSelectedTarget;
            }
        }

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    // Getting a list of the nearest creatures with the same group, alive and perceived, not including self
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    // Getting a list of the nearest hostiles, alive and preceived, not including self:
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByClass", "INVALID Target Type: " + IntToString(nTargetType));
#endif
                    break;
                }
        }

        nSize = GetArraySize(arCreatures);

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arCreatures[i];
            if (GetCreatureCoreClass(oCurrent) == nClass)
                return oCurrent;
        }

        return null;
    }

    /* @brief Returns the nearest visible enemy of a specific gender
*
* @params nGender the gender of the enemy being looked for
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns the nearest visible enemy of the specified gender
* @author Yaron
*/
    public GameObject _AI_Condition_GetNearestVisibleCreatureByGender(int nTargetType, int nGender, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByGender", "START, gender: " + IntToString(nGender));
#endif

        List<GameObject> arCreatures = new List<GameObject>();
        int nSize;
        int i;
        GameObject oCurrent;

        if (IsFollower(gameObject) != EngineConstants.FALSE && nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
        {
            // A follower should prefer to keep his selected target in this case
            GameObject oSelectedTarget = GetAttackTarget(gameObject);
            if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByGender", "Follower - keeping selected target instead of picking nearest");
#endif
                return oSelectedTarget;
            }
        }

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    // Getting a list of the nearest creatures with the same group, alive and perceived, not including self
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    // Getting a list of the nearest hostiles, alive and preceived, not including self:
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_GetNearestVisibleCreatureByGender", "INVALID Target Type: " + IntToString(nTargetType));
#endif
                    break;
                }
        }

        nSize = GetArraySize(arCreatures);

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arCreatures[i];
            if (GetCreatureGender(oCurrent) == nGender)
                return oCurrent;
        }

        return null;
    }

    /* @brief Returns the nearest enemy attacking any or a specific party member
*
* @params nTargetType the target follower
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns the nearest enemy attacking the specified party member
* @author Yaron
*/
    public GameObject _AI_Condition_GetNearestEnemyAttackingPartyMember(int nCommandType, int nCommandSubType, int nPartyMemberType, int nTacticID)
    {
        List<GameObject> arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType);
        GameObject oFollower;
        if (nPartyMemberType == EngineConstants.AI_TARGE_TYPE_HERO)
            oFollower = GetPartyLeader();
        else if (nPartyMemberType == EngineConstants.AI_TARGET_TYPE_MAIN_CONTROLLED)
            oFollower = GetMainControlled();
        else
            oFollower = GetTacticConditionObject(gameObject, nTacticID);
        if (IsObjectValid(oFollower) == EngineConstants.FALSE)
            return null;
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetNearestEnemyAttackingPartyMember", "START, party member: " + GetTag(oFollower));
#endif

        int nSize = GetArraySize(arCreatures);
        int i;
        GameObject oCurrent;
        GameObject oTarget;

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arCreatures[i];
            oTarget = GetAttackTarget(oCurrent);
#if DEBUG
            Log_Trace_AI("_AI_Condition_GetNearestEnemyAttackingPartyMember", "Attack target of:[" + GetTag(oCurrent) +
            "] is: " + GetTag(oTarget));
#endif

            if (oTarget == oFollower)
                return oCurrent;
        }
        return null;
    }

    /* @brief Returns the nearest enemy with any magical buff applied to it
*
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nRangeID the type of range the enemy must be in
* @returns the nearest enemy with any magical buff applied to it
* @author Yaron
*/
    public GameObject _AI_Condition_GetNearestEnemyWithAnyBuffEffect(int nCommandType, int nCommandSubType, int nRangeID, int nTacticID, int nTargetType)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetNearestEnemyWithAnyBuffEffect", "START, rangeID: " + IntToString(nRangeID));
#endif

        if (IsFollower(gameObject) != EngineConstants.FALSE)
        {
            // A follower should prefer to keep his selected target in this case
            GameObject oSelectedTarget = GetAttackTarget(gameObject);
            if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE && _AI_HasAnyBuffEffect(oSelectedTarget) != EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetNearestEnemyWithAnyBuffEffect", "Follower - keeping selected target");
#endif
                return oSelectedTarget;
            }
        }

        List<GameObject> arCreatures = new List<GameObject>();

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    arCreatures[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
        }

        int nSize = GetArraySize(arCreatures);
        int i;
        GameObject oCurrent;
        float fDistance;

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arCreatures[i];
            fDistance = GetDistanceBetween(gameObject, oCurrent);
            if (nRangeID != EngineConstants.AI_RANGE_ID_INVALID && fDistance > _AI_GetRangeFromID(nRangeID))
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetNearestEnemyWithAnyBuffEffect", "This target not valid (not in proper range)");
#endif
                continue;
            }
            if (_AI_HasAnyBuffEffect(oCurrent) != EngineConstants.FALSE)
                return oCurrent;
        }
        return null;
    }

    /* @brief Returns the nearest flip-cover placeable by state
*
* Can be used to look for placeables that are ready for flip-cover or being used by someone as flip cover
*
* @returns the nearest flip cover by the specified state, or null if none
* @param nFlipState State can be EngineConstants.AI_FLIP_COVER_READY_FOR_USE or EngineConstants.AI_FLIP_COVER_USED_BY_ENEMY.
* @author Yaron
*/
    public GameObject _AI_Condition_GetNearestFlipCoverByState(int nFlipState, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetNearestFlipCoverByState", "START, state: " + IntToString(nFlipState));
#endif
        GameObject oTarget = null;

        /*if(nFlipState != 0)
        {
            Log_Trace_AI("_AI_Condition_GetNearestFlipCoverByState", "Invalid state requested for flipcover (currently supporting only Idle state)");
                return null;
        }*/

        List<GameObject> arPlaceables = GetNearestObject(gameObject, EngineConstants.OBJECT_TYPE_PLACEABLE, EngineConstants.AI_MAX_NEAREST_PLACEABLE);
        int nSize = GetArraySize(arPlaceables);
        // Return first flip cover placeable in an Idle state, the does not yet have anyone registering a flipcover
        // on it.
        int i;
        GameObject oCurrent;
        for (i = 0; i < nSize; i++)
        {
            oCurrent = arPlaceables[i];
            if (GetPlaceableBaseType(oCurrent) != EngineConstants.PLACEABLE_TYPE_FLIPCOVER)
                continue;
            if (GetPlaceableState(oCurrent) != nFlipState)
                continue;
            if (GetLocalInt(oCurrent, EngineConstants.PLC_FLIP_COVER_USE_COUNT) > 0) // Got users already
                continue;
            oTarget = oCurrent;
            break;
        }
        return oTarget;
    }

    /* @brief Returns the nearest enemy vulnerable to a specific damage type
*
* 'Vulnerable' means the creature will be damaged more than usual with being hit by a damage of the specified type
*
* @returns the nearest enemy vulnerable to the specified damage
* @param nDamageType the type of damage the creature should be vulnerable to
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @author Yaron
*/
    public GameObject _AI_Condition_GetEnemyVulnerableToDamage(int nDamageType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetEnemyVulnerableToDamage", "START, damage type: " + IntToString(nDamageType));
#endif

        int i;
        GameObject oCurrent;

        if (IsFollower(gameObject) != EngineConstants.FALSE)
        {
            // A follower should prefer to keep his selected target in this case
            GameObject oSelectedTarget = GetAttackTarget(gameObject);
            if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE && _AI_IsVulnerableToDamage(oSelectedTarget, nDamageType) != EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetEnemyVulnerableToDamage", "Follower - keeping selected target");
#endif
                return oSelectedTarget;
            }
            else if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetEnemyVulnerableToDamage", "Follower - selected target not fitting condition");
#endif
                return null;
            }
        }

        List<GameObject> arCreatures = _AI_GetEnemies(nCommandType, nCommandSubType, EngineConstants.AI_TARGET_TYPE_ENEMY);
        int nSize = GetArraySize(arCreatures);

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arCreatures[i];
            if (_AI_IsVulnerableToDamage(oCurrent, nDamageType) != EngineConstants.FALSE)
                return oCurrent;
        }
        return null;
    }

    /* @brief Returns any target that fits the type
*
* If more than one target is optional than the nearest is returned
*
* @returns any target of a specific type
* @param nTargetType the type of the target we are looking for
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @author Yaron
*/
    public GameObject _AI_Condition_GetAnyTarget(int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetAnyTarget", "START, target type: " + IntToString(nTargetType));
#endif
        GameObject oTarget = null;
        List<GameObject> arCreatures = new List<GameObject>();

        if (IsFollower(gameObject) != EngineConstants.FALSE && nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
        {
            // A follower should prefer to keep his selected target in this case
            GameObject oSelectedTarget = GetAttackTarget(gameObject);
            if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_GetAnyTarget", "Follower - keeping selected target");
#endif
                return oSelectedTarget;
            }
        }

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF:
                {
                    // Making sure we're not trying to apply an ability that is already active
                    if (nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY && Ability_IsAbilityActive(gameObject, nCommandSubType) == EngineConstants.FALSE)
                        oTarget = gameObject;
                    else if (nCommandType != EngineConstants.AI_COMMAND_USE_ABILITY) // not an ability - can use self as target
                        oTarget = gameObject;
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    arCreatures = _AI_GetAllies(nCommandType, nCommandSubType);
                    oTarget = arCreatures[0];
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    oTarget = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
            default: // It can still be a party target
                {
                    oTarget = _AI_GetPartyTarget(nTargetType, nCommandType, nCommandSubType, nTacticID);
                    break;
                }
        }

        return oTarget;
    }

    // Returns gameObject if ammo level is appropriate
    public GameObject _AI_Condition_SelfHasAmmoLevel(int nAmmoLevel)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_SelfHasAmmoLevel", "START, ammo level: " + IntToString(nAmmoLevel));
#endif
        GameObject oRet = null;

        int nCurrentStackSize = Items_CheckAmmo(gameObject);
        int nMaxStackSize = Items_CheckMaxAmmo(gameObject);
        if (nMaxStackSize == 0)
        {
#if DEBUG
            Log_Trace_AI("_AI_Condition_SelfHasAmmoLevel", "Invalid stack size for ammo: 0");
#endif
            return null;
        }

        //float fStackRatio = fCurrentStackSize / fMaxStackSize * 100.0f; // should give a percentage ratio
        //float fCheckRatio;
#if DEBUG
        Log_Trace_AI("_AI_Condition_SelfHasAmmoLevel", "current ammo: " + IntToString(nCurrentStackSize));
#endif

        switch (nAmmoLevel)
        {
            case EngineConstants.AI_AMMO_LEVEL_LOW: // valid if between 0 and low ration
                {
                    if (nCurrentStackSize >= 0 && nCurrentStackSize <= EngineConstants.AI_AMMO_RATIO_LOW)
                        oRet = gameObject;
                    break;
                }
            case EngineConstants.AI_AMMO_LEVEL_MEDIUM: // valid if between low and high ratio
                {
                    if (nCurrentStackSize > EngineConstants.AI_AMMO_RATIO_LOW && nCurrentStackSize < EngineConstants.AI_AMMO_RATIO_HIGH)
                        oRet = gameObject;
                    break;
                }
            case EngineConstants.AI_AMMO_LEVEL_HIGH: // valid if high and max ratio
                {
                    if (nCurrentStackSize >= EngineConstants.AI_AMMO_RATIO_HIGH)
                        oRet = gameObject;
                    break;
                }
        }

        return oRet;
    }

    // Returns gameObject if the most hated enemy has the specified armor type
    public GameObject _AI_Condition_HasArmorType(int nTargetType, int nArmorType, int nCommandType, int nCommandSubType)
    {
        GameObject oTarget = null;
#if DEBUG
        Log_Trace_AI("_AI_Condition_HasArmorType", "Target type: " + IntToString(nTargetType) + ", Armor Type: " + IntToString(nArmorType));
#endif

        List<GameObject> arEnemies = new List<GameObject>();

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF:
                {
                    arEnemies[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, -1);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    arEnemies[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, -1);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    arEnemies = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_HasArmorType", "Invalid target type");
#endif
                    return null;
                }

        }

        int nSize = GetArraySize(arEnemies);
        int i;
        GameObject oCurrent;
        int nEnemyArmorType;
        for (i = 0; i < nSize; i++)
        {
            oCurrent = arEnemies[i];
            nEnemyArmorType = _AI_GetArmorType(oCurrent);
#if DEBUG
            Log_Trace_AI("_AI_Condition_HasArmorType", "Enemy: " + GetTag(oCurrent) + ", Enemy's armor type: " +
                IntToString(nEnemyArmorType) + ", requested armor bit field: " + IntToString(nArmorType));
#endif
            Debug.Log("AI_conditions_h: armor type & replaced with ==, double check");
            if (nEnemyArmorType == nArmorType)
            {
                if (nTargetType == EngineConstants.AI_TARGET_TYPE_SELF)
                    oTarget = gameObject; // creature's armor fits in the requested armor bit field (nArmorType)
                else
                    oTarget = oCurrent;

                break;
            }
        }

        return oTarget;
    }

    // Returns gameObject if the most enemies have the specified armor type
    public GameObject _AI_Condition_MostEnemiesHaveArmorType(int nArmorType)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_MostEnemiesHaveArmorType", "START, looking for armor type: " + IntToString(nArmorType));
#endif
        int nSize = GetThreatTableSize(gameObject);
        int i;
        int nEnemiesWithArmorType = 0;
        GameObject oCurrentEnemy;
        int nEnemyArmorType;
        GameObject oRet = null;

        for (i = 0; i < nSize; i++)
        {
            oCurrentEnemy = GetThreatEnemy(gameObject, i);
            nEnemyArmorType = _AI_GetArmorType(oCurrentEnemy);
            Debug.Log("AI_conditions_h: armor type & replaced with ==, double check");
            if (nEnemyArmorType == nArmorType)
                nEnemiesWithArmorType++;
        }
#if DEBUG
        Log_Trace_AI("_AI_Condition_MostEnemiesHaveArmorType", "number of enemies with armor type: " + IntToString(nEnemiesWithArmorType) +
            ", Total number of enemies: " + IntToString(nSize));
#endif

        if (nEnemiesWithArmorType >= nSize / 2)
            oRet = gameObject;

        return oRet;
    }

    // Returns gameObject if all enemies have the specified armor type
    public GameObject _AI_Condition_AllEnemiesHaveArmorType(int nArmorType)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_AllEnemiesHaveArmorType", "START, looking for armor type: " + IntToString(nArmorType));
#endif
        int nSize = GetThreatTableSize(gameObject);
        int i;
        GameObject oCurrentEnemy;
        int nEnemyArmorType;
        GameObject oRet = gameObject;

        for (i = 0; i < nSize; i++)
        {
            oCurrentEnemy = GetThreatEnemy(gameObject, i);
            nEnemyArmorType = _AI_GetArmorType(oCurrentEnemy);
            Debug.Log("AI_conditions_h: armor type ! and & replaced with !=, double check");
            if (nEnemyArmorType != nArmorType)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_AllEnemiesHaveArmorType", "found one enemy with wrong armor type: " + GetTag(oCurrentEnemy));
#endif
                oRet = null;
                break;
            }
        }

        return oRet;
    }

    // If target type is SELF: returns gameObject if the most hated enemy is at least at the specified rank
    // If target type is ENEMY: returns the most hated enemy if his rank is at least as the specified rank
    public GameObject _AI_Condition_TargetHasRank(int nTargetType, int nTargetRank, int nTacticID, int nTacticCommand, int nTacticSubCommand)
    {
        GameObject oRet = null;
        List<GameObject> arEnemies = _AI_GetEnemies(nTacticCommand, nTacticSubCommand, nTargetType);
        int nEnemyRank;
#if DEBUG
        Log_Trace_AI("_AI_Condition_TargetHasRank", "START, target rank: " + IntToString(nTargetRank));
#endif

        GameObject oEnemy;
        int i;
        int nSize = GetArraySize(arEnemies);
        for (i = 0; i < nSize; i++)
        {
            oEnemy = arEnemies[i];
            nEnemyRank = GetCreatureRank(oEnemy);

            if (nTargetRank > 0) // looking for specific rank
            {
                if (nEnemyRank == nTargetRank)
                    oRet = oEnemy;
            }
            else if (nTargetRank < 0) // looking for a range of ranks
            {
                switch (nTargetRank)
                {
                    case EngineConstants.AI_RANK_RANGE_BOSS_OR_HIGHER:
                        {
                            if (nEnemyRank == EngineConstants.CREATURE_RANK_BOSS || nEnemyRank == EngineConstants.CREATURE_RANK_ELITE_BOSS)
                                oRet = oEnemy;
                            break;
                        }
                    case EngineConstants.AI_RANK_RANGE_ELITE_OR_HIGER:
                        {
                            if (nEnemyRank == EngineConstants.CREATURE_RANK_LIEUTENANT || nEnemyRank == EngineConstants.CREATURE_RANK_BOSS || nEnemyRank == EngineConstants.CREATURE_RANK_ELITE_BOSS)
                                oRet = oEnemy;
                            break;
                        }
                    case EngineConstants.AI_RANK_RANGE_ELITE_OR_LOWER:
                        {
                            if (nEnemyRank == EngineConstants.CREATURE_RANK_LIEUTENANT || nEnemyRank == EngineConstants.CREATURE_RANK_NORMAL || nEnemyRank == EngineConstants.CREATURE_RANK_CRITTER ||
                                    nEnemyRank == EngineConstants.CREATURE_RANK_ONE_HIT_KILL || nEnemyRank == EngineConstants.CREATURE_RANK_WEAK_NORMAL)
                                oRet = oEnemy;
                            break;
                        }
                    case EngineConstants.AI_RANK_RANGE_NORMAL_OR_HIGHER:
                        {
                            if (nEnemyRank == EngineConstants.CREATURE_RANK_NORMAL || nEnemyRank == EngineConstants.CREATURE_RANK_LIEUTENANT || nEnemyRank == EngineConstants.CREATURE_RANK_BOSS || nEnemyRank == EngineConstants.CREATURE_RANK_ELITE_BOSS)
                                oRet = oEnemy;
                            break;
                        }
                    case EngineConstants.AI_RANK_RANGE_NORMAL_OR_LOWER:
                        {
                            if (nEnemyRank == EngineConstants.CREATURE_RANK_NORMAL || nEnemyRank == EngineConstants.CREATURE_RANK_CRITTER ||
                                    nEnemyRank == EngineConstants.CREATURE_RANK_ONE_HIT_KILL || nEnemyRank == EngineConstants.CREATURE_RANK_WEAK_NORMAL)
                                oRet = oEnemy;
                            break;
                        }
                }
            }
        }

        return oRet;
    }

    // If target type is SELF: Returns gameObject if I'm being attacked by the specified attack type (melee, ranged or magic)
    // If target type is ALLY: Returns nearest ALLY that is being attacked by the specified attack type
    public GameObject _AI_Condition_BeingAttackedByAttackType(int nTargetType, int nAttackType, int nTacticCommand, int nTacticSubCommand, int nTacticID)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_BeingAttackedByAttackType", "START, attack type: " + IntToString(nAttackType));
#endif
        GameObject oTarget = null;
        GameObject oCurrentEnemy;
        int i;
        int j;
        List<GameObject> arAllies = _AI_GetAllies(nTacticCommand, nTacticSubCommand);
        int nAlliesNum = GetArraySize(arAllies);
        List<GameObject> arEnemies = _AI_GetEnemies(nTacticCommand, nTacticSubCommand, nTargetType);
        int nEnemiesNum = GetArraySize(arEnemies);
        GameObject oCurrentAlly;
        GameObject oAlly;

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF:
                {
                    for (i = 0; i < nEnemiesNum; i++)
                    {
                        oCurrentEnemy = arEnemies[i];
                        Debug.Log("AI_conditions_h: attack type & replaced with ==, double check");
                        if (GetAttackTarget(oCurrentEnemy) == gameObject &&
                             (_AI_GetAttackType(oCurrentEnemy) == nAttackType))//possible equivalent (a&b)==b
                        {
                            // if melee need to check range
                            if (nAttackType == EngineConstants.AI_ATTACK_TYPE_MELEE)
                            {
                                float fRange = GetDistanceBetween(gameObject, oCurrentEnemy);
                                if (fRange <= EngineConstants.AI_MELEE_RANGE)
                                {
                                    oTarget = gameObject;
                                    break;
                                }
                            }
                            else // non melee
                            {
                                oTarget = gameObject; // found one enemy that attacks with the specified type - that's all we need
                                break;
                            }
                        }
                    }
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    // Checking all enemies - stopping when finding first ally being attack by a specific type
                    for (i = 0; i < nEnemiesNum && oTarget == null; i++)
                    {
                        oCurrentEnemy = arEnemies[i];
                        for (j = 0; j < nAlliesNum && oTarget == null; j++)
                        {
                            oCurrentAlly = arAllies[j];
                            Debug.Log("AI_conditions_h: attack type & replaced with ==, double check");
                            if (GetAttackTarget(oCurrentAlly) == oCurrentEnemy &&
                                 (_AI_GetAttackType(oCurrentAlly) == nAttackType))
                            {
                                // if melee need to check range
                                if (nAttackType == EngineConstants.AI_ATTACK_TYPE_MELEE)
                                {
                                    float fRange = GetDistanceBetween(oCurrentAlly, oCurrentEnemy);
                                    if (fRange <= EngineConstants.AI_MELEE_RANGE)
                                    {
                                        oTarget = oCurrentEnemy;
                                        break;
                                    }
                                }
                                else // non melee
                                {
                                    oTarget = oCurrentEnemy; // found one enemy that attacks with the specified type - that's all we need
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    // Checking all allies - stopping when finding first ally being attack by a specific type
                    for (i = 0; i < nAlliesNum && oTarget == null; i++)
                    {
                        oCurrentAlly = arAllies[i];
                        for (j = 0; j < nEnemiesNum && oTarget == null; j++)
                        {
                            oCurrentEnemy = arEnemies[j];
                            Debug.Log("AI_conditions_h: attack type & replaced with ==, double check");
                            if (GetAttackTarget(oCurrentEnemy) == oCurrentAlly &&
                                 (_AI_GetAttackType(oCurrentEnemy) == nAttackType))
                            {
                                // if melee need to check range
                                if (nAttackType == EngineConstants.AI_ATTACK_TYPE_MELEE)
                                {
                                    float fRange = GetDistanceBetween(oCurrentAlly, oCurrentEnemy);
                                    if (fRange <= EngineConstants.AI_MELEE_RANGE)
                                    {
                                        oTarget = oCurrentAlly;
                                        break;
                                    }
                                }
                                else // non melee
                                {
                                    oTarget = oCurrentAlly; // found one enemy that attacks with the specified type - that's all we need
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    GameObject oMostHated = _AI_Condition_GetMostHatedEnemy(1, nTacticCommand, nTacticSubCommand, nTacticID);
                    Debug.Log("AI_conditions_h: attack type & replaced with ==, double check");
                    if ((_AI_GetAttackType(oMostHated) == nAttackType) && GetAttackTarget(oMostHated) == gameObject)
                        oTarget = gameObject;
                    break;
                }
            default: // It can still be a party target
                {
                    oAlly = _AI_GetPartyTarget(nTargetType, nTacticCommand, nTacticSubCommand, nTacticID);
                    for (j = 0; j < nEnemiesNum && oTarget == null; j++)
                    {
                        oCurrentEnemy = arEnemies[j];
                        Debug.Log("AI_conditions_h: attack type & replaced with ==, double check");
                        if ((_AI_GetAttackType(oCurrentEnemy) == nAttackType) && GetAttackTarget(oCurrentEnemy) == oAlly)
                        {
                            // if melee need to check range
                            if (nAttackType == EngineConstants.AI_ATTACK_TYPE_MELEE)
                            {
                                float fRange = GetDistanceBetween(oAlly, oCurrentEnemy);
                                if (fRange <= EngineConstants.AI_MELEE_RANGE)
                                {
                                    oTarget = oAlly;
                                    break;
                                }
                            }
                            else // non melee
                            {
                                oTarget = oAlly; // found one enemy that attacks with the specified type - that's all we need
                                break;
                            }
                        }
                    }
                    break;
                }

        }

        return oTarget;
    }

    // Returns gameObject if most hated enemy is using the specified attack type
    public GameObject _AI_Condition_UsingAttackType(int nTargetType, int nAttackType, int nCommandType, int nCommandSubType)
    {
        GameObject oTarget = null;

#if DEBUG
        Log_Trace_AI("_AI_Condition_UsingAttackType", "START, attack type: " + IntToString(nAttackType));
#endif
        List<GameObject> arTargets = new List<GameObject>();

        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF:
                {
                    arTargets[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, -1);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED:
                {
                    arTargets[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, -1);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ENEMY:
                {
                    arTargets = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
                    break;
                }
            case EngineConstants.AI_TARGET_TYPE_ALLY:
                {
                    arTargets = _AI_GetAllies(nCommandType, nCommandSubType);
                    break;
                }
            default:
                {
#if DEBUG
                    Log_Trace_AI("_AI_Condition_UsingAttackType", "Invalid target type");
#endif
                    return null;
                }

        }

        int nSize = GetArraySize(arTargets);
        int i;
        GameObject oCurrent;
        int nEnemyAttackType;
        for (i = 0; i < nSize; i++)
        {
            oCurrent = arTargets[i];
            nEnemyAttackType = _AI_GetAttackType(oCurrent);
#if DEBUG
            Log_Trace_AI("_AI_Condition_UsingAttackType", "Target: " + GetTag(oCurrent) + ", Target's attack type: " +
                IntToString(nEnemyAttackType) + ", requested attack bit field: " + IntToString(nAttackType));
#endif
            Debug.Log("AI_conditions_h: attack type & replaced with ==, double check");
            if (nEnemyAttackType == nAttackType)
            {
                if (nTargetType == EngineConstants.AI_TARGET_TYPE_SELF)
                    oTarget = gameObject; // creature's armor fits in the requested armor bit field (nArmorType)
                else
                    oTarget = oCurrent;

                break;
            }
        }

        return oTarget;
    }

    // Returns gameObject if most enemies are using the specified attack type
    public GameObject _AI_Condition_MostEnemiesUsingAttackType(int nAttackType)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_MostEnemiesUsingAttackType", "START, looking for attack type: " + IntToString(nAttackType));
#endif
        List<GameObject> arEnemies = _AI_GetEnemies(-1, -1, EngineConstants.AI_TARGET_TYPE_ENEMY);
        int nSize = GetArraySize(arEnemies);
        int i;
        int nEnemiesWithAttackType = 0;
        GameObject oCurrentEnemy;
        int nEnemyAttackType;
        GameObject oRet = null;

        for (i = 0; i < nSize; i++)
        {
            oCurrentEnemy = arEnemies[i];
            nEnemyAttackType = _AI_GetAttackType(oCurrentEnemy);
            Debug.Log("AI_conditions_h: attack type & replaced with ==, double check");
            if (nEnemyAttackType == nAttackType)
                nEnemiesWithAttackType++;
        }
#if DEBUG
        Log_Trace_AI("_AI_Condition_MostEnemiesUsingAttackType", "number of enemies with attack type: " + IntToString(nEnemiesWithAttackType) +
            ", Total number of enemies: " + IntToString(nSize));
#endif

        if (nEnemiesWithAttackType > nSize - nEnemiesWithAttackType)
            oRet = gameObject;

        return oRet;
    }

    // Returns gameObject if all enemies are using the specified attack type
    public GameObject _AI_Condition_AllEnemiesUsingAttackType(int nAttackType)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_AllEnemiesUsingAttackType", "START, looking for attack type: " + IntToString(nAttackType));
#endif
        int nSize = GetThreatTableSize(gameObject);
        int i;
        GameObject oCurrentEnemy;
        int nEnemyAttackType;
        GameObject oRet = gameObject;

        for (i = 0; i < nSize; i++)
        {
            oCurrentEnemy = GetThreatEnemy(gameObject, i);
            nEnemyAttackType = _AI_GetAttackType(oCurrentEnemy);
            Debug.Log("AI_conditions_h: attack type & replaced with ==, double check");
            if (nEnemyAttackType != nAttackType)
            {
#if DEBUG
                Log_Trace_AI("_AI_Condition_AllEnemiesUsingAttackType", "found one enemy with wrong attack type: " + GetTag(oCurrentEnemy));
#endif
                oRet = null;
                break;
            }
        }

        return oRet;
    }

    // Returns gameObject if at least X enemies are alive
    public GameObject _AI_Condition_AtLeastXEnemiesAreAlive(int nTargetType, int nNum)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_AtLeastXEnemiesAreAlive", "START, X=: " + IntToString(nNum));
#endif
        List<GameObject> arEnemies = _AI_GetEnemies(-1, -1, nTargetType);

        GameObject oTarget = null;
        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF: oTarget = gameObject; break;
            case EngineConstants.AI_TARGET_TYPE_ENEMY: oTarget = arEnemies[0]; break;
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED: oTarget = _AI_Condition_GetMostHatedEnemy(1, -1, -1, -1); break;
        }

        int nCount = 0;
        int nEnemiesNum = GetThreatTableSize(gameObject);
        if (nEnemiesNum < nNum)
            oTarget = null;
        return oTarget;
    }

    // Returns gameObject if at least X enemies are dead
    public GameObject _AI_Condition_AtLeastXCreaturesAreDead(int nTargetType, int nNum, int nTacticCommand, int nTacticSubCommand)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_AtLeastXCreaturesAreDead", "START, X=: " + IntToString(nNum));
#endif

        List<GameObject> arEnemies = _AI_GetEnemies(-1, -1, nTargetType);

        GameObject oTarget = null;
        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF: oTarget = gameObject; break;
            case EngineConstants.AI_TARGET_TYPE_ENEMY: oTarget = arEnemies[0]; break;
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED: oTarget = _AI_Condition_GetMostHatedEnemy(1, -1, -1, -1); break;
        }

        List<GameObject> arCorpses = GetNearestObject(gameObject,
                                                EngineConstants.OBJECT_TYPE_CREATURE,
                                                EngineConstants.AI_MAX_CREATURES_NEAREST);

        float fRange = 0.0f;
        if (nTacticCommand == EngineConstants.AI_COMMAND_USE_ABILITY &&
            (nTacticSubCommand == EngineConstants.ABILITY_TALENT_DEVOUR || nTacticSubCommand == EngineConstants.ABILITY_SPELL_ANIMATE_DEAD))
        {
            int nRangeID = GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "range", nTacticSubCommand);
            fRange = GetM2DAFloat(EngineConstants.TABLE_RANGES, "PrimaryRange", nRangeID);
        }

        int nSize = GetArraySize(arCorpses);

#if DEBUG
        Log_Trace_AI("_AI_Condition_AtLeastXCreaturesAreDead", "Total corpses found: " + IntToString(nSize) +
            ", looking for range: " + FloatToString(fRange));
#endif

        int i;
        GameObject oCurrent;
        int nCorpseCount = 0;

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arCorpses[i];
            if (IsDead(oCurrent) != EngineConstants.FALSE)
            {
                if (fRange > 0.0f) // range check
                {
                    float fDistance = GetDistanceBetween(gameObject, oCurrent);
                    if (fDistance <= fRange)
                        nCorpseCount++;
                }
                else // no need for range check
                    nCorpseCount++;
            }
        }

        if (nCorpseCount < nNum)
            oTarget = null;

        return oTarget;
    }

    // Returns gameObject if at least X allies are alive
    public GameObject _AI_Condition_AtLeastXAlliesAreAlive(int nTargetType, int nNum, int nParam2)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_AtLeastXAlliesAreAlive", "START, X=: " + IntToString(nNum));
#endif

        List<GameObject> arEnemies = _AI_GetEnemies(-1, -1, nTargetType);

        GameObject oTarget = null;
        switch (nTargetType)
        {
            case EngineConstants.AI_TARGET_TYPE_SELF: oTarget = gameObject; break;
            case EngineConstants.AI_TARGET_TYPE_ENEMY: oTarget = arEnemies[0]; break;
            case EngineConstants.AI_TARGET_TYPE_MOST_HATED: oTarget = _AI_Condition_GetMostHatedEnemy(1, -1, -1, -1); break;
        }

        int nCount = 0;
        List<GameObject> arAllies = _AI_GetAllies(EngineConstants.COMMAND_TYPE_INVALID, -1);
        int nAlliesNum = GetArraySize(arAllies);

        if (nParam2 == 0) // normal check of "at least alive"
        {
            if (nAlliesNum < nNum)
                oTarget = null;
        }
        else if (nParam2 == 1) // special check if at least wounded 50%
        {
            int i;
            GameObject oCurrent;
            for (i = 0; i < nAlliesNum; i++)
            {
                oCurrent = arAllies[i];
                if (_AI_HasStatLevel(oCurrent, EngineConstants.AI_STAT_TYPE_HP, -50) != EngineConstants.FALSE)
                    nCount++;
            }
            if (nCount < nNum)
                oTarget = null;
        }
        else if (nParam2 == 2) // special check if at least has curable effect
        {
            int i;
            GameObject oCurrent;
            for (i = 0; i < nAlliesNum; i++)
            {
                oCurrent = arAllies[i];
                if (_AI_IsTargetValidForBeneficialAbility(oCurrent, EngineConstants.ABILITY_SPELL_CURE) != EngineConstants.FALSE)
                    nCount++;
            }
            if (nCount < nNum)
                oTarget = null;
        }
        return oTarget;
    }

    /* @brief Checks a creature has a specific AI status applied
*
* @param oCreature the creature we are checking for an AI status
* @nAIStatus the AI status we are checking on the creature (EngineConstants.AI_STATUS_***)
* @returns EngineConstants.TRUE if the creature has the specified AI status, EngineConstants.FALSE otherwise
* @author Yaron
*/
    public int _AI_HasAIStatus(GameObject oCreature, int nAIStatus)
    {
#if DEBUG
        Log_Trace_AI("_AI_HasAIStatus", "checking for creature: " + GetTag(oCreature) + ", status: " + IntToString(nAIStatus));
#endif
        int nRet = EngineConstants.FALSE;

        switch (nAIStatus)
        {
            case EngineConstants.AI_STATUS_POLYMORPH: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_SHAPECHANGE); break;
            case EngineConstants.AI_STATUS_CHARM: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_CHARM); break;
            case EngineConstants.AI_STATUS_PARALYZE: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_PARALYZE); break;
            case EngineConstants.AI_STATUS_STUN: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_STUN); break;
            case EngineConstants.AI_STATUS_SLEEP: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_SLEEP); break;
            case EngineConstants.AI_STATUS_ROOT: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_ROOT); break;
            case EngineConstants.AI_STATUS_DAZE: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_DAZE); break;
            case EngineConstants.AI_STATUS_SLOW: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_MOVEMENT_RATE); break;
            case EngineConstants.AI_STATUS_STEALTH: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_STEALTH); break;
            case EngineConstants.AI_STATUS_DISEASED: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_DISEASE); break;
            case EngineConstants.AI_STATUS_DEAD: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_DEATH); break;
            case EngineConstants.AI_STATUS_UNCONSIOUS: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_DEATH); break;
            case EngineConstants.AI_STATUS_KNOCKDOWN: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_KNOCKDOWN) != EngineConstants.FALSE || GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_SLIP) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE; break;
            case EngineConstants.AI_STATUS_GRABBED: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_GRABBED) != EngineConstants.FALSE || GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_OVERWHELMED) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE; break;
            case EngineConstants.AI_STATUS_GRABBING: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_GRABBING) != EngineConstants.FALSE || GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_OVERWHELMING) != EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE; break;
            case EngineConstants.AI_STATUS_BERSERK: nRet = IsModalAbilityActive(oCreature, EngineConstants.ABILITY_TALENT_BERSERK); break;
            case EngineConstants.AI_STATUS_CONFUSED: nRet = GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_CONFUSION); break;
            case EngineConstants.AI_STATUS_IMMOBLIZED:
                {
                    int nFlags = GetEffectsFlags(oCreature);
                    Debug.Log("AI_conditions_h: flag type & replaced with ==, double check");
                    if (nFlags == EngineConstants.EFFECT_FLAG_DISABLE_MOVEMENT)
                        nRet = EngineConstants.TRUE;
                    break;
                }
            case EngineConstants.AI_STATUS_MOVEMENT_IMPAIRED:
                {
                    int nFlags = GetEffectsFlags(oCreature);
                    Debug.Log("AI_conditions_h: flag type & replaced with ==, double check");
                    if ((nFlags == EngineConstants.EFFECT_FLAG_DISABLE_MOVEMENT) || GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_MOVEMENT_RATE) != EngineConstants.FALSE)
                        nRet = EngineConstants.TRUE;

                    break;
                }
            case EngineConstants.AI_STATUS_CANT_ATTACK:
                {
                    int nFlags = GetEffectsFlags(oCreature);
                    Debug.Log("AI_conditions_h: flag type & replaced with ==, double check");
                    if (nFlags == EngineConstants.EFFECT_FLAG_DISABLE_COMBAT)
                        nRet = EngineConstants.TRUE;

                    break;
                }
        }
#if DEBUG
        Log_Trace_AI("_AI_HasAIStatus", "return: " + IntToString(nRet));
#endif

        return nRet;
    }

    // Returns a creature that currently uses a ranged attack while being at a certain distance
    public GameObject _AI_Condition_GetTargetUsingRangedWeaponsAtRange(int nTargetType, int nRange, int nCommandType, int nCommandSubType)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetTargetUsingRangedWeaponsAtRange", "START, looking for range: " + IntToString(nRange));
#endif

        List<GameObject> arEnemies = new List<GameObject>();
        if (nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
            arEnemies = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
        else // most hatd
            arEnemies[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, -1);

        int nSize = GetArraySize(arEnemies);
        int i;
        GameObject oCurrent;
        float fDistance;

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arEnemies[i];
            fDistance = GetDistanceBetween(gameObject, oCurrent);

#if DEBUG
            Log_Trace_AI("_AI_Condition_GetTargetUsingRangedWeaponsAtRange", "Current Target: " + GetTag(oCurrent) + ", Distance: " + FloatToString(fDistance) + ", attack type: " +
                IntToString(_AI_GetAttackType(oCurrent)));
#endif

            if (_AI_GetAttackType(oCurrent) == EngineConstants.AI_ATTACK_TYPE_RANGED || _AI_GetAttackType(oCurrent) == EngineConstants.AI_ATTACK_TYPE_MAGIC)
            {

#if DEBUG
                Log_Trace_AI("_AI_Condition_GetTargetUsingRangedWeaponsAtRange", "Target using ranged or magic! - checking range");
#endif

                // Target is ranged or magic
                switch (nRange)
                {
                    case EngineConstants.AI_RANGE_ID_SHORT:
                        {
#if DEBUG
                            Log_Trace_AI("_AI_Condition_GetTargetUsingRangedWeaponsAtRange", "Checking for range lower than: " + FloatToString(EngineConstants.AI_RANGE_SHORT));
#endif
                            if (fDistance <= EngineConstants.AI_RANGE_SHORT)
                                return oCurrent;
                            break;
                        }
                    case EngineConstants.AI_RANGE_ID_MEDIUM:
                        {
#if DEBUG
                            Log_Trace_AI("_AI_Condition_GetTargetUsingRangedWeaponsAtRange", "Checking for range between: " + FloatToString(EngineConstants.AI_RANGE_SHORT) + " and " + FloatToString(EngineConstants.AI_RANGE_MEDIUM));
#endif
                            if (fDistance > EngineConstants.AI_RANGE_SHORT && fDistance <= EngineConstants.AI_RANGE_MEDIUM)
                                return oCurrent;
                            break;
                        }
                    case EngineConstants.AI_RANGE_ID_LONG:
                        {
#if DEBUG
                            Log_Trace_AI("_AI_Condition_GetTargetUsingRangedWeaponsAtRange", "Checking for range between: " + FloatToString(EngineConstants.AI_RANGE_MEDIUM) + " and " + FloatToString(EngineConstants.AI_RANGE_LONG));
#endif
                            if (fDistance > EngineConstants.AI_RANGE_MEDIUM && fDistance <= EngineConstants.AI_RANGE_LONG)
                                return oCurrent;
                            break;
                        }

                }
            }
        }

        return null;
    }

    // Returns the nearest target within the specified range
    public GameObject _AI_Condition_GetTargetAtRange(int nTargetType, int nRange, int nCommandType, int nCommandSubType)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetTargetAtRange", "START, looking for range: " + IntToString(nRange));
#endif

        float fDistance;

        List<GameObject> arTargets = new List<GameObject>();
        if (nTargetType == EngineConstants.AI_TARGET_TYPE_ALLY)
            arTargets = _AI_GetAllies(nCommandType, nCommandSubType);
        else if (nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
            arTargets = _AI_GetEnemies(nCommandType, nCommandSubType, nTargetType);
        else // most hated OR self
            arTargets[0] = _AI_Condition_GetMostHatedEnemy(1, nCommandType, nCommandSubType, -1);

        int nSize = GetArraySize(arTargets);
        int i;
        GameObject oCurrent;
        GameObject oTarget = null;

        for (i = 0; i < nSize && oTarget == null; i++)
        {
            oCurrent = arTargets[i];
            fDistance = GetDistanceBetween(gameObject, oCurrent);

#if DEBUG
            Log_Trace_AI("_AI_Condition_GetTargetAtRange", "Current Target: " + GetTag(oCurrent) + ", Distance: " + FloatToString(fDistance));
#endif

            switch (nRange)
            {
                case EngineConstants.AI_RANGE_ID_SHORT:
                    {
#if DEBUG
                        Log_Trace_AI("_AI_Condition_GetTargetAtRange", "Checking for range lower than: " + FloatToString(EngineConstants.AI_RANGE_SHORT));
#endif
                        if (fDistance <= EngineConstants.AI_RANGE_SHORT)
                            oTarget = oCurrent;
                        break;
                    }
                case EngineConstants.AI_RANGE_ID_MEDIUM:
                    {
#if DEBUG
                        Log_Trace_AI("_AI_Condition_GetTargetAtRange", "Checking for range between: " + FloatToString(EngineConstants.AI_RANGE_SHORT) + " and " + FloatToString(EngineConstants.AI_RANGE_MEDIUM));
#endif
                        if (fDistance > EngineConstants.AI_RANGE_SHORT && fDistance <= EngineConstants.AI_RANGE_MEDIUM)
                            oTarget = oCurrent;
                        break;
                    }
                case EngineConstants.AI_RANGE_ID_LONG:
                    {
#if DEBUG
                        Log_Trace_AI("_AI_Condition_GetTargetAtRange", "Checking for range between: " + FloatToString(EngineConstants.AI_RANGE_MEDIUM) + " and " + FloatToString(EngineConstants.AI_RANGE_LONG));
#endif
                        if (fDistance > EngineConstants.AI_RANGE_MEDIUM && fDistance <= EngineConstants.AI_RANGE_LONG)
                            oTarget = oCurrent;
                        break;
                    }

            }

        }

        if (nTargetType == EngineConstants.AI_TARGET_TYPE_SELF && IsObjectValid(oTarget) != EngineConstants.FALSE)
            oTarget = gameObject;

        if (IsFollower(gameObject) != EngineConstants.FALSE && nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
        {
            Warning("the selected target is always empty?!?");
            GameObject oSelectedTarget = null;
            if (_AI_IsHostileTargetValid(oSelectedTarget) != EngineConstants.FALSE && oSelectedTarget != oTarget)
                oTarget = null; // for followers this is valid only if they current target is in the specified range
        }

        return oTarget;
    }

    // Returns a target at the specified flank or null if none
    public GameObject _AI_Condition_GetTargetAtFlankLocation(int nFlankType, int nTargetType)
    {
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetTargetAtFlankLocation", "START, flank type: " + IntToString(nFlankType));
#endif
        float fAngleStart = 0.0f;
        float fAngleEnd = 0.0f;
        GameObject oTarget = null;
        float fAbilityFacing = 0.0f;
        int nMeleeRing = 0; // for creatures with multiple rings (high dragon)
        int nApp = GetAppearanceType(gameObject);
        float fRange = 0.0f;
        int bCheckShape = EngineConstants.FALSE;
        float fMinRange = 0.0f;

        switch (nFlankType)
        {
            case EngineConstants.AI_FLANK_DRAGON_FR:
            case EngineConstants.AI_FLANK_RIGHT:
                {
                    if (nApp == EngineConstants.APR_TYPE_BROODMOTHER)
                    {
                        fAngleStart = EngineConstants.ABILITY_FLANK_FACING_RIGHT - EngineConstants.ABILITY_FLANK_SIZE_SIDE / 2;
                        fAngleEnd = EngineConstants.ABILITY_FLANK_FACING_RIGHT + EngineConstants.ABILITY_FLANK_SIZE_SIDE / 2;
                        fAbilityFacing = EngineConstants.ABILITY_FLANK_FACING_RIGHT;
                    }
                    else
                    {
                        float fAttackFR = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AttackFR", nApp);
                        float fAttackFRWidth = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AttackFRWidth", nApp);
                        fAngleStart = fAttackFR - fAttackFRWidth / 2;
                        fAngleEnd = fAttackFR + fAttackFRWidth / 2;
                        fAbilityFacing = fAttackFR;
                        nMeleeRing = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "AttackFRRing", nApp);
                    }
                    break;
                }
            case EngineConstants.AI_FLANK_DRAGON_FL:
            case EngineConstants.AI_FLANK_LEFT:
                {
                    if (nApp == EngineConstants.APR_TYPE_BROODMOTHER)
                    {
                        fAngleStart = EngineConstants.ABILITY_FLANK_FACING_LEFT - EngineConstants.ABILITY_FLANK_SIZE_SIDE / 2;
                        fAngleEnd = EngineConstants.ABILITY_FLANK_FACING_LEFT + EngineConstants.ABILITY_FLANK_SIZE_SIDE / 2;
                        fAbilityFacing = EngineConstants.ABILITY_FLANK_FACING_LEFT;
                    }
                    else
                    {
                        float fAttackFL = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AttackFL", nApp);
                        float fAttackFLWidth = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AttackFLWidth", nApp);
                        fAngleStart = fAttackFL - fAttackFLWidth / 2;
                        fAngleEnd = fAttackFL + fAttackFLWidth / 2;
                        fAbilityFacing = fAttackFL;
                        nMeleeRing = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "AttackFLRing", nApp);
                    }
                    break;
                }
            case EngineConstants.AI_FLANK_DRAGON_BR:
            case EngineConstants.AI_FLANK_BRIGHT:
                {
                    float fAttackBR = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AttackBR", nApp);
                    float fAttackBRWidth = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AttackBRWidth", nApp);
                    fAngleStart = fAttackBR - fAttackBRWidth / 2;
                    fAngleEnd = fAttackBR + fAttackBRWidth / 2;
                    fAbilityFacing = fAttackBR;
                    nMeleeRing = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "AttackBRRing", nApp);
                    break;
                }
            case EngineConstants.AI_FLANK_DRAGON_BL:
            case EngineConstants.AI_FLANK_BLEFT:
                {
                    float fAttackBL = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AttackBL", nApp);
                    float fAttackBLWidth = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "AttackBLWidth", nApp);
                    fAngleStart = fAttackBL - fAttackBLWidth / 2;
                    fAngleEnd = fAttackBL + fAttackBLWidth / 2;
                    fAbilityFacing = fAttackBL;
                    nMeleeRing = GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "AttackBLRing", nApp);
                    break;
                }
            case EngineConstants.AI_FLANK_LARGE_LEFT:
                {
                    fAngleStart = EngineConstants.ABILITY_FLANK_FACING_LARGE_LEFT - EngineConstants.ABILITY_FLANK_SIZE_LARGE_SIDE / 2;
                    fAngleEnd = EngineConstants.ABILITY_FLANK_FACING_LARGE_LEFT + EngineConstants.ABILITY_FLANK_SIZE_LARGE_SIDE / 2;
                    fAbilityFacing = EngineConstants.ABILITY_FLANK_FACING_LARGE_LEFT;
                    break;
                }
            case EngineConstants.AI_FLANK_LARGE_RIGHT:
                {
                    fAngleStart = EngineConstants.ABILITY_FLANK_FACING_LARGE_RIGHT - EngineConstants.ABILITY_FLANK_SIZE_LARGE_SIDE / 2;
                    fAngleEnd = EngineConstants.ABILITY_FLANK_FACING_LARGE_RIGHT + EngineConstants.ABILITY_FLANK_SIZE_LARGE_SIDE / 2;
                    fAbilityFacing = EngineConstants.ABILITY_FLANK_FACING_LARGE_RIGHT;
                    break;
                }
            case EngineConstants.AI_FLANK_FRONT:
                {
                    fAngleStart = EngineConstants.ABILITY_FLANK_FACING_FRONT - EngineConstants.ABILITY_FLANK_SIZE_FRONT / 2;
                    fAngleEnd = EngineConstants.ABILITY_FLANK_FACING_FRONT + EngineConstants.ABILITY_FLANK_SIZE_FRONT / 2;
                    fAbilityFacing = EngineConstants.ABILITY_FLANK_FACING_FRONT;
                    break;
                }
            case EngineConstants.AI_FLANK_BRIGHT2:
                {
                    fAngleStart = EngineConstants.ABILITY_FLANK_FACING_BACK_RIGHT2 - EngineConstants.ABILITY_FLANK_SIZE_BACK2 / 2;
                    fAngleEnd = EngineConstants.ABILITY_FLANK_FACING_BACK_RIGHT2 + EngineConstants.ABILITY_FLANK_SIZE_BACK2 / 2;
                    fAbilityFacing = EngineConstants.ABILITY_FLANK_FACING_BACK_RIGHT2;
                    break;
                }
            case EngineConstants.AI_FLANK_BLEFT2:
                {
                    fAngleStart = EngineConstants.ABILITY_FLANK_FACING_BACK_LEFT2 - EngineConstants.ABILITY_FLANK_SIZE_BACK2 / 2;
                    fAngleEnd = EngineConstants.ABILITY_FLANK_FACING_BACK_LEFT2 + EngineConstants.ABILITY_FLANK_SIZE_BACK2 / 2;
                    fAbilityFacing = EngineConstants.ABILITY_FLANK_FACING_BACK_LEFT2;
                    break;
                }
            case EngineConstants.AI_FLANK_DRAGON_BACK:
            case EngineConstants.AI_FLANK_DRAGON_BACK_2:
            case EngineConstants.AI_FLANK_DRAGON_BACK_3:
                {
                    fAngleStart = EngineConstants.HIGH_TAIL_FLAP_ANGLE_START;
                    fAngleEnd = EngineConstants.HIGH_TAIL_FLAP_ANGLE_END;
                    fAbilityFacing = GetFacing(gameObject) - EngineConstants.HIGH_TAIL_FLAP_FACING;
                    fRange = EngineConstants.HIGH_TAIL_FLAP_RANGE;
                    bCheckShape = EngineConstants.TRUE;
                    fMinRange = EngineConstants.HIGH_TAIL_MIN_RANGE;
                    break;
                }
            case EngineConstants.AI_FLANK_DRAGON_FRONT_FAR:
            case EngineConstants.AI_FLANK_DRAGON_FRONT_FAR_2:
            case EngineConstants.AI_FLANK_DRAGON_FRONT_FAR_3:
                {
                    fAngleStart = EngineConstants.HIGH_BREATH_ANGLE_START;
                    fAngleEnd = EngineConstants.HIGH_BREATH_ANGLE_END;
                    fAbilityFacing = GetFacing(gameObject) - EngineConstants.HIGH_BREATH_FACING;
                    fRange = EngineConstants.HIGH_BREATH_RANGE;
                    fMinRange = EngineConstants.HIGH_BREATH_MIN_RANGE;
                    bCheckShape = EngineConstants.TRUE;
                    break;
                }
            case EngineConstants.AI_FLANK_DRAGON_FRONT_CLOSE:
            case EngineConstants.AI_FLANK_DRAGON_FRONT_CLOSE_2:
            case EngineConstants.AI_FLANK_DRAGON_FRONT_CLOSE_3:
                {
                    fAngleStart = EngineConstants.HIGH_SWEEP_ANGLE_START;
                    fAngleEnd = EngineConstants.HIGH_SWEEP_ANGLE_END;
                    fAbilityFacing = GetFacing(gameObject) - EngineConstants.HIGH_SWEEP_FACING;
                    //fAbilityFacing = EngineConstants.HIGH_SWEEP_FACING;
                    fRange = EngineConstants.HIGH_SWEEP_RANGE;
                    bCheckShape = EngineConstants.TRUE;
                    break;
                }

        }

        if (fRange == 0.0f)
        {
            switch (nApp)
            {
                case EngineConstants.APR_TYPE_OGRE: fRange = EngineConstants.MONSTER_MELEE_RANGE_OGRE; break;
                case EngineConstants.APR_TYPE_BROODMOTHER: fRange = EngineConstants.MONSTER_MELEE_RANGE_BROODMOTHER; break;
                case EngineConstants.APP_TYPE_ARCHDEMON: fRange = EngineConstants.MONSTER_MELEE_RANGE_HIGHDRAGON; break;
                case EngineConstants.APP_TYPE_HIGHDRAGON: fRange = EngineConstants.MONSTER_MELEE_RANGE_HIGHDRAGON; break;
                case EngineConstants.APP_TYPE_WILD_SYLVAN: fRange = EngineConstants.MONSTER_MELEE_RANGE_WILD_SYLVAN; break;
                case EngineConstants.APP_TYPE_DRAGON: fRange = EngineConstants.MONSTER_MELEE_RANGE_DRAGON; break;
                case EngineConstants.APP_TYPE_DRAKE: fRange = EngineConstants.MONSTER_MELEE_RANGE_DRAGON; break;
                case EngineConstants.APP_TYPE_PRIDE_DEMON: fRange = EngineConstants.MONSTER_MELEE_RANGE_PRIDE_DEMON; break;
            }
        }
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetTargetAtFlankLocation", "ability facing: " + FloatToString(fAbilityFacing));
#endif

        Vector3 lConeLocation = Location(GetArea(gameObject), GetPosition(gameObject), fAbilityFacing);
        List<GameObject> arEnemies = new List<GameObject>();
        if (bCheckShape != EngineConstants.FALSE)
        {

            arEnemies = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, lConeLocation, fRange);
            arEnemies = FilterObjectsInShape(arEnemies, EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_CONE, lConeLocation, fAngleEnd - fAngleStart, fRange, 0.0f, EngineConstants.TRUE);
        }
        else
        {
            if (fAngleEnd == 360.0f)
                fAngleEnd = 359.9f;
            arEnemies = GetCreaturesInMeleeRing(gameObject, fAngleStart, fAngleEnd, EngineConstants.TRUE, nMeleeRing);
        }

        // Main filter - no need to check anything if not surrounded by enough enemies

        int nSize = GetArraySize(arEnemies);
#if DEBUG
        if (nMeleeRing != 0)
            Log_Trace_AI("_AI_Condition_GetTargetAtFlankLocation", "Melee Ring: " + IntToString(nMeleeRing));

        Log_Trace_AI("_AI_Condition_GetTargetAtFlankLocation", "Looking between angle: " + IntToString(FloatToInt(fAngleStart)) +
            ", and: " + IntToString(FloatToInt(fAngleEnd)) + ", number of creatures between angles: " + IntToString(nSize));
#endif
        if (nSize == 1 && arEnemies[0] == gameObject)
        {
#if DEBUG
            Log_Trace_AI("_AI_Condition_GetTargetAtFlankLocation", "ERROR: found self in target AOE", EngineConstants.LOG_SEVERITY_CRITICAL);
#endif
            return null;
        }

        // filter min range if needed
        if (fMinRange > 0.0f)
        {
            int i;
            GameObject oCurrent;
            int nNewSize = 0;
            float fDistance;
            for (i = 0; i < nSize; i++)
            {
                oCurrent = arEnemies[i];
                fDistance = GetDistanceBetween(gameObject, oCurrent);
                if (fDistance > fMinRange)
                    nNewSize++;
            }
            nSize = nNewSize;
#if DEBUG
            Log_Trace_AI("_AI_Condition_GetTargetAtFlankLocation", "Ability has min range. Num of targets after filter: " + IntToString(nSize));
#endif

        }

        int nMinTargets = 1;
        if (nFlankType == EngineConstants.AI_FLANK_DRAGON_BACK_2 || nFlankType == EngineConstants.AI_FLANK_DRAGON_FRONT_CLOSE_2 || nFlankType == EngineConstants.AI_FLANK_DRAGON_FRONT_FAR_2)
            nMinTargets = 2;
        else if (nFlankType == EngineConstants.AI_FLANK_DRAGON_BACK_3 || nFlankType == EngineConstants.AI_FLANK_DRAGON_FRONT_CLOSE_3 || nFlankType == EngineConstants.AI_FLANK_DRAGON_FRONT_FAR_3)
            nMinTargets = 2;

        if (nSize >= nMinTargets && nTargetType == EngineConstants.AI_TARGET_TYPE_SELF)
            oTarget = gameObject;
        else if (nTargetType == EngineConstants.AI_TARGET_TYPE_ENEMY)
            oTarget = arEnemies[0];
        return oTarget;
    }

    // Returns gameObject if surrounded by the specified number of enemies
    public GameObject _AI_Condition_SurroundedByAtLeastXEnemies(int nTacticCommand, int nTacticSubCommand, int nNumOfTargets, int nTacticID)
    {
        GameObject oTarget = null;
#if DEBUG
        Log_Trace_AI("_AI_Condition_SurroundedByAtLeastXEnemies", "START, Min num of enemies: " + IntToString(nNumOfTargets));
#endif

        float fStart = 0.0f;
        float fEnd = 359.0f;

        if (nTacticCommand == EngineConstants.AI_COMMAND_USE_ABILITY && nTacticSubCommand == EngineConstants.ABILITY_TALENT_DUAL_WEAPON_SWEEP)
        {
#if DEBUG
            Log_Trace_AI("_AI_Condition_SurroundedByAtLeastXEnemies", "Using a SWEEP talent - narrowing angles");
#endif
            fStart = 0.0f;
            fEnd = 180.0f;
        }
        else if (nTacticCommand == EngineConstants.AI_COMMAND_USE_ABILITY && nTacticSubCommand == EngineConstants.MONSTER_DRAGON_BREATH)
        {
            fStart = EngineConstants.BREATH_ANGLE_START;
            fEnd = EngineConstants.BREATH_ANGLE_END;
        }
        int nApp = GetAppearanceType(gameObject);
        List<GameObject> arEnemies = new List<GameObject>();
        if (nApp == EngineConstants.APP_TYPE_ARCHDEMON || nApp == EngineConstants.APP_TYPE_HIGHDRAGON)
        {
            arEnemies = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(gameObject), EngineConstants.MONSTER_MELEE_RANGE_HIGHDRAGON);
            arEnemies = FilterObjectsInShape(arEnemies, EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(gameObject), EngineConstants.MONSTER_MELEE_RANGE_HIGHDRAGON, 0.0f, 0.0f, EngineConstants.TRUE);
        }
        else if (nTacticCommand == EngineConstants.AI_COMMAND_USE_ABILITY && nTacticSubCommand == EngineConstants.ABILITY_TALENT_MONSTER_ARCANEHORROR_AOE)
        {
            arEnemies = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(gameObject), EngineConstants.ARCANEHORROR_AOE_RADIUS);
            arEnemies = FilterObjectsInShape(arEnemies, EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(gameObject), EngineConstants.ARCANEHORROR_AOE_RADIUS, 0.0f, 0.0f, EngineConstants.TRUE);
        }
        else
            arEnemies = GetCreaturesInMeleeRing(gameObject, fStart, fEnd, EngineConstants.TRUE);

        int nSize = GetArraySize(arEnemies);
        int nEnemiesCount = nSize;
        if (nApp == EngineConstants.APP_TYPE_ARCHDEMON || nApp == EngineConstants.APP_TYPE_HIGHDRAGON)
        {
            nEnemiesCount = 0;
            int i;
            GameObject oCurrent;
            for (i = 0; i < nSize; i++)
            {
                oCurrent = arEnemies[i];
                if (oCurrent != gameObject && IsObjectHostile(gameObject, oCurrent) != EngineConstants.FALSE)
                    nEnemiesCount++;
            }
        }

#if DEBUG
        Log_Trace_AI("_AI_Condition_SurroundedByAtLeastXEnemies", "Number of enemies around self: " + IntToString(nEnemiesCount));
#endif

        // If nNumofTargets is 0 - then we want NO enemies
        if (nNumOfTargets == 0 && nEnemiesCount == 0)
            oTarget = gameObject;
        else if (nNumOfTargets != 0 && nEnemiesCount >= nNumOfTargets)
            oTarget = gameObject;
        return oTarget;
    }

    // Returns the current target of a specific party member (nTargetType)
    public GameObject _AI_Condition_GetPartyMemberTarget(int nTacticCommand, int nTacticSubCommand, int nPartyMemberType, int nTacticID)
    {
        GameObject oFollower;
        if (nPartyMemberType == EngineConstants.AI_TARGE_TYPE_HERO)
            oFollower = GetPartyLeader();
        else if (nPartyMemberType == EngineConstants.AI_TARGET_TYPE_MAIN_CONTROLLED)
            oFollower = GetMainControlled();
        else
            oFollower = GetTacticConditionObject(gameObject, nTacticID);
#if DEBUG
        Log_Trace_AI("_AI_Condition_GetPartyMemberTarget", "START, looking for target of party member: " + GetTag(oFollower));
#endif

        return GetAttackTarget(oFollower);
    }

    /* @brief Returns a list of creatures that are allied to the current creature
*
* Ally creatures are defined as creatures that are in the same group. This function
* does not return all the allies - but a limited number of them. The returned list
* is sorted by proximity to the current creature.
*
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @returns an array of allied creatures sorted by distance
* @author Yaron
*/
    public List<GameObject> _AI_GetAllies(int nCommandType, int nCommandSubType)
    {
        List<GameObject> arAllies = new List<GameObject>();
        List<GameObject> arAlliesFinal = new List<GameObject>();

        // This is the top candidate for being in the engine.
        // It is called a lot of times and the second distance filter is not efficient.

        arAllies = GetNearestObjectByGroup(gameObject,
                                                     GetGroupId(gameObject),
                                                     EngineConstants.OBJECT_TYPE_CREATURE,
                                                     EngineConstants.AI_MAX_CREATURES_NEAREST,
                                                     EngineConstants.TRUE,      // Living
                                                     EngineConstants.TRUE,      // Perceived
                                                     EngineConstants.FALSE);    // Not including self

        // Filter distance - NOT EFFICIENT! - MOVE TO ENGINE!
        // This includes also ability filter - if the action related to the condition for which we retrieve
        // the list of allies/enemies is a duration ability then we will targeting creatures which already
        // have the specified ability active

        int i;
        int nSize = GetArraySize(arAllies);
#if DEBUG
        Log_Trace_AI("_AI_GetAllies", "INITIAL LIST SIZE: " + IntToString(nSize) + ", xCommand type: " + IntToString(nCommandType) + ", sub command: " + IntToString(nCommandSubType));
#endif

        GameObject oCurrent;
        float fDistance;
        //int j = 0;
        for (i = 0; i < nSize; i++)
        {
            oCurrent = arAllies[i];
            fDistance = GetDistanceBetween(gameObject, oCurrent);

            // Evaluating allies outside of combat ONLY if they are within a cerain range
            if (fDistance > EngineConstants.AI_RANGE_MAX_ALLY_HELP && GetCombatState(oCurrent) == EngineConstants.FALSE)
                continue;

            if (IsDying(oCurrent) != EngineConstants.FALSE)
                continue; // dead are not included in the array already

            // certain creatures may be filtered out if they have the abiliy active
            if (nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY)
            {
                if (Ability_IsAbilityActive(oCurrent, nCommandSubType) != EngineConstants.FALSE)
                    continue; // just ignore the creature
                if (_AI_IsTargetValidForBeneficialAbility(oCurrent, nCommandSubType) == EngineConstants.FALSE)
                    continue;
            }
            else if (Effects_HasAIModifier(oCurrent, EngineConstants.AI_MODIFIER_IGNORE) != EngineConstants.FALSE)
                continue; // ignore this target
                          // Creature is valid to be a target - update final array:
#if DEBUG
            Log_Trace_AI("_AI_GetAllies", "Adding creature to list of allies: [" + GetTag(oCurrent) + "]");
#endif
            //arAlliesFinal[j] = oCurrent;
            //j++;
            arAlliesFinal.Add(oCurrent);//DHK
        }

        return arAlliesFinal;
    }

    /* @brief Returns a list of creatures that are hostile to the current creature
*
* Enemy creatures are defined as creatures that are hostile. This function
* does not return all the allies - but a limited number of them. The returned list
* is sorted by proximity to the current creature.
*
* @param nCommandType type of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nCommandSubType subtype of xCommand - if ability the condition will avoid returning an enemy that has an ability applied
* @param nTargetTypeOfFollower - used only for followers - if the follower already attacks someone in melee he'd prefer doing it in some cases
* @returns an array of allied creatures sorted by distance
* @author Yaron
*/
    public List<GameObject> _AI_GetEnemies(int nCommandType, int nCommandSubType, int nTargetTypeOfFollower = -1)
    {
        List<GameObject> arEnemies = new List<GameObject>();
        List<GameObject> arEnemiesFinal = new List<GameObject>();
        GameObject oTargetOverride = _AI_GetTargetOverride();

#if DEBUG
        Log_Trace_AI("_AI_GetEnemies", "nTargetTypeOfFollower: " + IntToString(nTargetTypeOfFollower) + ", commandtype: " + IntToString(nCommandType)
            + ", nCommandSubType: " + IntToString(nCommandSubType));
#endif
        // if there is target override, then the array of enemies has one GameObject with the overriden target
        if (IsObjectValid(oTargetOverride) != EngineConstants.FALSE)
        {
            arEnemies[0] = oTargetOverride;
#if DEBUG
            Log_Trace_AI("_AI_GetEnemies", "Got override target: " + GetTag(oTargetOverride));
#endif
            return arEnemies;
        }
        else if (IsFollower(gameObject) != EngineConstants.FALSE && nTargetTypeOfFollower == EngineConstants.AI_TARGET_TYPE_ENEMY && nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY)
        {
            GameObject oCurrentTarget = GetAttackTarget(gameObject);

            if (IsObjectValid(oCurrentTarget) != EngineConstants.FALSE)
            {
                float fRangeToTarget = GetDistanceBetween(gameObject, oCurrentTarget);
                int nAbilityTargetType = GetM2DAInt(EngineConstants.TABLE_ABILITIES_TALENTS, "range", nCommandSubType);
#if DEBUG
                Log_Trace_AI("_AI_GetEnemies", "Follower current target: " + GetTag(oCurrentTarget) + ", range: " + FloatToString(fRangeToTarget)
                    + ", ability range: " + IntToString(GetM2DAInt(EngineConstants.TABLE_ABILITIES_TALENTS, "range", nCommandSubType)));
#endif
                if ((nAbilityTargetType == 1 || nAbilityTargetType == 0) &&
                    fRangeToTarget <= EngineConstants.AI_MELEE_RANGE &&
                    IsUsingMeleeWeapon(gameObject) != EngineConstants.FALSE)
                {
                    arEnemies[0] = oCurrentTarget;
#if DEBUG
                    Log_Trace_AI("_AI_GetEnemies", "Follower keeping melee target for melee use-ability: " + GetTag(oCurrentTarget));
#endif
                    return arEnemies;
                }
            }
        }

        // This is the top candidate for being in the engine.
        // It is called a lot of times and the second distance filter is not efficient.

        arEnemies = GetNearestObjectByHostility(gameObject,
                                                          EngineConstants.TRUE,
                                                          EngineConstants.OBJECT_TYPE_CREATURE,
                                                          EngineConstants.AI_MAX_CREATURES_NEAREST,
                                                          EngineConstants.TRUE,   // Living
                                                          EngineConstants.TRUE,   // Perceived
                                                          EngineConstants.FALSE); // Not including self

        // Filter distance - NOT EFFICIENT! - MOVE TO ENGINE!

        // This includes also ability filter - if the action related to the condition for which we retrieve
        // the list of allies/enemies is a duration ability then we will targeting creatures which already
        // have the specified ability active

        int i;
        int nSize = GetArraySize(arEnemies);

#if DEBUG
        Log_Trace_AI("_AI_GetEnemies", "Initial size: " + IntToString(nSize));
#endif

        // check what distance to evalulate targets from main controlled
        float fFollowerMaxEngageRange = EngineConstants.AI_FOLLOWER_ENGAGE_DISTANCE_LONG;
        if (IsFollower(gameObject) != EngineConstants.FALSE && AI_BehaviorCheck_ChaseEnemy() == EngineConstants.FALSE)
            fFollowerMaxEngageRange = EngineConstants.AI_FOLLOWER_ENGAGE_DISTANCE_CLOSE;

        GameObject oCurrent;
        float fDistance;
        float fEnemyDistanceToMainControlled;
        //int j = 0;
        for (i = 0; i < nSize; i++)
        {
            oCurrent = arEnemies[i];
            fDistance = GetDistanceBetween(gameObject, oCurrent);
            if (IsFollower(gameObject) != EngineConstants.FALSE)
            {
                fEnemyDistanceToMainControlled = GetDistanceBetween(oCurrent, GetMainControlled());
                if (fEnemyDistanceToMainControlled > fFollowerMaxEngageRange)
                {
                    // Filter only if trying melee attack or 0/touch range ability
                    if (nCommandType == EngineConstants.AI_COMMAND_ATTACK && IsUsingMeleeWeapon(gameObject) != EngineConstants.FALSE)
                    {
#if DEBUG
                        Log_Trace_AI("_AI_GetEnemies", "Stopping to evaluate enemies for MELEE EngineConstants.ATTACK - too far from main controlled follower");
#endif
                    }
                    else if (nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY && GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "Range", nCommandSubType) == 1)
                    {
#if DEBUG
                        Log_Trace_AI("_AI_GetEnemies", "Stopping to evaluate enemies for TOUCH RANGE ABILITY - too far from main controlled follower");
#endif
                    }

                    break;
                }
            }

            // certain creatures may be filtered out if they have the abiliy active
            if (nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY)
            {
                if (Ability_IsAbilityActive(oCurrent, nCommandSubType) != EngineConstants.FALSE)
                    continue; // just ignore the creature
            }
            else if (Effects_HasAIModifier(oCurrent, EngineConstants.AI_MODIFIER_IGNORE) != EngineConstants.FALSE)
                continue; // ignore this target
                          // Creature is valid to be a target - update final array:
#if DEBUG
            Log_Trace_AI("_AI_GetEnemies", "Adding creature to list of enemies: [" + GetTag(oCurrent) + "]");
#endif
            //arEnemiesFinal[j] = oCurrent;
            //j++;
            arEnemiesFinal.Add(oCurrent);
        }

        nSize = GetArraySize(arEnemiesFinal);
#if DEBUG
        Log_Trace_AI("_AI_GetEnemies", "Final size: " + IntToString(nSize));
#endif

        return arEnemiesFinal;
    }

    /* @brief Checks a creature has a specific stat (HP, mana or stamina) within a specific range
*
* This function should be used when a creature has HP >= 50% or mana < 20% etc'.
*
* @param oCreature the creature we are checking for a stat level
* nStatType the stat type we are looking for a range (EngineConstants.AI_STAT_TYPE_*)
* nStatLevel the level of the stat we are checking for. This should be a percentage number (50 for 50%).
* A positive number should be for checks like HP >= 50%, a negative number should be for checks like HP < 50%.
* @returns EngineConstants.TRUE if the creature has the stat in the required level, EngineConstants.FALSE otherwise
* @author Yaron
*/
    public int _AI_HasStatLevel(GameObject oCreature, int nStatType, int nStatLevel)
    {
        float fCurrentStat = 0.0f;
        float fMaxStat = 0.0f;
        int nCurrentStatLevel;
        int nRet = EngineConstants.FALSE;

        switch (nStatType)
        {
            case EngineConstants.AI_STAT_TYPE_HP:
                {
                    fCurrentStat = GetCurrentHealth(oCreature);
                    fMaxStat = GetMaxHealth(oCreature);
                    break;
                }
            case EngineConstants.AI_STAT_TYPE_MANA_OR_STAMINA:
                {
                    fCurrentStat = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT);
                    fMaxStat = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_TOTAL);

                    if (fMaxStat <= 0.0f)
                    {
#if DEBUG
                        Log_Trace_AI("_AI_HasStatLevel", "ERRRO! bad value for max stamina or mana: " + FloatToString(fMaxStat));
#endif
                        return EngineConstants.FALSE;
                    }
                    break;
                }
        }

        // Determine what type of stat we are dealing with
        nCurrentStatLevel = FloatToInt(fCurrentStat / fMaxStat * 100);

#if DEBUG
        Log_Trace_AI("_AI_HasStatLevel", "Creature: " + GetTag(oCreature) + ", MAX: " + FloatToString(fMaxStat) +
            ", Current: " + FloatToString(fCurrentStat));
#endif

        if (nStatLevel > 0) // For example: creature >= 50%
        {
            if (nCurrentStatLevel >= nStatLevel)
                nRet = EngineConstants.TRUE;
        }
        else // For example: creature < 50% - use absolute value of function parameter
        {
            if (nCurrentStatLevel < abs(nStatLevel))
                nRet = EngineConstants.TRUE;
        }

#if DEBUG
        Log_Trace_AI("_AI_HasStatLevel", "Creature: " + GetTag(oCreature) + ", Current: " + IntToString(nCurrentStatLevel) +
            ", Required: " + IntToString(nStatLevel) + ", return: " + IntToString(nRet));
#endif

        return nRet;

    }

    /* Returns creature with highest or lowest health in a group
// This is a very good candidate for being in the engine
// Currently supports only 1st most damaged, anything else will fail*/
    public GameObject _AI_GetNthDamagedCreatureInArray(List<GameObject> arCreatures, int nHighLow)
    {
        // NOTE: this function actually returns the creature with highest or lower hp
        int nSize = GetArraySize(arCreatures);
        GameObject oMostDamagedCreature = null;
        GameObject oCurrent;
        int i;
        float fCurrentHP;
        float fLowestHP = -1.0f;
        float fHighestHP = 0.0f;
        GameObject oLowestHealthCreature = null;
        GameObject oHighestHealthCreature = null;

        if (nHighLow != 0 && nHighLow != 1)
        {
#if DEBUG
            Log_Trace_AI("_AI_GetNthDamagedCreatureInArray (=> get creature with highest or lowest health)",
                "ERROR! wrong value", EngineConstants.LOG_SEVERITY_WARNING);
#endif

            return null;
        }

        for (i = 0; i < nSize; i++)
        {
            oCurrent = arCreatures[i];
            fCurrentHP = GetCurrentHealth(oCurrent);
            if (fCurrentHP < fLowestHP || fLowestHP == -1.0f)
            {
                fLowestHP = fCurrentHP;
                oLowestHealthCreature = oCurrent;
            }
            if (fCurrentHP > fHighestHP)
            {
                fHighestHP = fCurrentHP;
                oHighestHealthCreature = oCurrent;
            }
        }

        if (nHighLow == 0) // => return lowest health
            return oLowestHealthCreature;
        else // 1 => return highest health
            return oHighestHealthCreature;
    }

    /* @brief Returns override target, if any
*
* An override target can be specified when using the UT_CombatStart function. This
* target will then be used instead of trying to find something with other target-looking routines
* like GetNearest or GetMostHated.
*
* @returns override target, null otherwise
* @author Yaron
*/
    public GameObject _AI_GetTargetOverride()
    {
        GameObject oTarget = null;
        GameObject oTargetOverride = GetLocalObject(gameObject, EngineConstants.AI_TARGET_OVERRIDE); // if overriden by UT_CombatStart
        int nTargetOverrideCount = GetLocalInt(gameObject, EngineConstants.AI_TARGET_OVERRIDE_DUR_COUNT); // how long was overriden

        if (IsObjectValid(oTargetOverride) == EngineConstants.FALSE || IsDead(oTargetOverride) != EngineConstants.FALSE)
        {
            SetLocalInt(gameObject, EngineConstants.AI_TARGET_OVERRIDE_DUR_COUNT, 0);
            SetLocalObject(gameObject, EngineConstants.AI_TARGET_OVERRIDE, null);
            return null;
        }

        if (nTargetOverrideCount < EngineConstants.AI_TARGET_OVERRIDE_DURATION)
        {
            // Has override target and override duration is still valid -> use override target
#if DEBUG
            Log_Trace_AI("_AI_GetTargetOverride", "Using target override: " + GetTag(oTargetOverride));
#endif
            oTarget = oTargetOverride;
            // Increase counter, only if not permanent
            if (nTargetOverrideCount != -1)
            {
                nTargetOverrideCount++;
                SetLocalInt(gameObject, EngineConstants.AI_TARGET_OVERRIDE_DUR_COUNT, nTargetOverrideCount);
            }
        }
        else if (nTargetOverrideCount >= EngineConstants.AI_TARGET_OVERRIDE_DURATION)
        {
            Log_Trace_AI("_AI_GetTargetOverride", "Stopping target override (timed out) - will try to pick new target. Override target: " + GetTag(oTargetOverride));
            SetLocalInt(gameObject, EngineConstants.AI_TARGET_OVERRIDE_DUR_COUNT, 0);
            SetLocalObject(gameObject, EngineConstants.AI_TARGET_OVERRIDE, null);
        }

        return oTarget;
    }

    public float _AI_GetRangeFromID(int nRangeID)
    {
        float fRet = 0.0f;
        switch (nRangeID)
        {
            case EngineConstants.AI_RANGE_ID_SHORT: fRet = EngineConstants.AI_RANGE_SHORT; break;
            case EngineConstants.AI_RANGE_ID_MEDIUM: fRet = EngineConstants.AI_RANGE_MEDIUM; break;
            case EngineConstants.AI_RANGE_ID_LONG: fRet = EngineConstants.AI_RANGE_LONG; break;
        }
        return fRet;
    }

    /* @brief Checks if a creature has any normal buff effect
*
* Normal buff effects are effects that can be displled.
*
* @returns EngineConstants.TRUE if the creature has any buff xEffect active, EngineConstants.FALSE otherwise
* @author Yaron
*/
    public int _AI_HasAnyBuffEffect(GameObject oCreature)
    {
        List<xEffect> arEffects = GetEffects(oCreature);
        int nSize = GetArraySize(arEffects);
        int i;
        xEffect eCurrent;
        int nEffectAbility;
#if DEBUG
        Log_Trace_AI("_AI_HasAnyBuffEffect", "START, number of effects: " + IntToString(nSize));
#endif

        for (i = 0; i < nSize; i++)
        {
            eCurrent = arEffects[i];
            nEffectAbility = GetEffectAbilityIDRef(ref eCurrent);
            if (GetM2DAInt(EngineConstants.TABLE_AI_ABILITY_COND, "MagicalBuff", nEffectAbility) == 1)
            {
#if DEBUG
                Log_Trace_AI("_AI_HasAnyBuffEffect", "Creature has buff ability applied: " + Log_GetAbilityNameById(nEffectAbility));
#endif
                return EngineConstants.TRUE;

            }

        }
        return EngineConstants.FALSE;
    }

    public int _AI_GetArmorType(GameObject oCreature)
    {
        int nArmorType = EngineConstants.AI_ARMOR_TYPE_INVALID;

        // Trying to find if humanoid
        GameObject oArmor;
        int nBaseItemType;

        oArmor = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_CHEST, oCreature);
        if (IsObjectValid(oArmor) == EngineConstants.FALSE)
            nArmorType = EngineConstants.AI_ARMOR_TYPE_LOW;
        else
        {
            nBaseItemType = GetBaseItemType(oArmor);
            switch (nBaseItemType)
            {
                case EngineConstants.BASE_ITEM_TYPE_ARMOR_LIGHT:
                    {
                        nArmorType = EngineConstants.AI_ARMOR_TYPE_LOW;
                        break;
                    }
                case EngineConstants.BASE_ITEM_TYPE_ARMOR_MEDIUM:
                    {
                        nArmorType = EngineConstants.AI_ARMOR_TYPE_MEDIUM;
                        break;
                    }
                case EngineConstants.BASE_ITEM_TYPE_ARMOR_HEAVY:
                case EngineConstants.BASE_ITEM_TYPE_ARMOR_MASSIVE:
                case EngineConstants.BASE_ITEM_TYPE_ARMOR_SUPERMASSIVE:
                    {
                        nArmorType = EngineConstants.AI_ARMOR_TYPE_HIGH;
                        break;
                    }
            }
        }

        return nArmorType;
    }

    // Returns the CURRENT attack type of the creature (using melee, ranged or magic)
    public int _AI_GetAttackType(GameObject oCreature)
    {
        int nRet = EngineConstants.AI_ATTACK_TYPE_INVALID;

        int nItemType = -1;
        int nHasAIStatusCantAttack = _AI_HasAIStatus(oCreature, EngineConstants.AI_STATUS_CANT_ATTACK);

#if DEBUG
        Log_Trace_AI("_AI_GetAttackType", "creature core class: " + IntToString(GetCreatureCoreClass(oCreature)));
        Log_Trace_AI("_AI_GetAttackType", "IsMelee: " + IntToString(IsUsingMeleeWeapon(oCreature)));
        Log_Trace_AI("_AI_GetAttackType", "IsRanged: " + IntToString(IsUsingRangedWeapon(oCreature)));
#endif

        // Removed the commandtype conditions so AI can detect the player as melee/range even before he started attacking

        if (nHasAIStatusCantAttack == EngineConstants.FALSE &&
            (GetCreatureCoreClass(oCreature) == EngineConstants.CLASS_WIZARD || GetCreatureCoreClass(oCreature) == 25)) // not a perfect check but covers 99% of the cases. Good enough.
        {
#if DEBUG
            Log_Trace_AI("_AI_GetAttackType", "MAGIC attack type for: " + GetTag(oCreature));
#endif
            nRet = EngineConstants.AI_ATTACK_TYPE_MAGIC;
        }
        else if (IsUsingRangedWeapon(oCreature) != EngineConstants.FALSE && nHasAIStatusCantAttack == EngineConstants.FALSE)
        {
#if DEBUG
            Log_Trace_AI("_AI_GetAttackType", "RANGED attack type for: " + GetTag(oCreature));
#endif
            nRet = EngineConstants.AI_ATTACK_TYPE_RANGED;
        }
        else if (nHasAIStatusCantAttack == EngineConstants.FALSE)// melee
        {
#if DEBUG
            Log_Trace_AI("_AI_GetAttackType", "MELEE attack type for: " + GetTag(oCreature));
#endif
            nRet = EngineConstants.AI_ATTACK_TYPE_MELEE;
        }

        return nRet;
    }

    // Checks if the target can make use of beneficial abilities like cure poison etc'
    public int _AI_IsTargetValidForBeneficialAbility(GameObject oCreature, int nAbilityID)
    {
        int nRet = EngineConstants.TRUE;
        float fMaxHealth = GetMaxHealth(oCreature);
        float fCurrentHealth = GetCurrentHealth(oCreature);
        int nDebuf = GetM2DAInt(EngineConstants.TABLE_AI_ABILITY_COND, "MagicalDebuf", nAbilityID);

        Log_Trace_AI("_AI_IsTargetValidForBeneficialAbility", "Creature: " + GetTag(oCreature) + ", current health: " + FloatToString(fCurrentHealth) + ", max health: " + FloatToString(fMaxHealth));

        int nWounded = fCurrentHealth < fMaxHealth ? EngineConstants.TRUE : EngineConstants.FALSE;

        Log_Trace_AI("_AI_IsTargetValidForBeneficialAbility", "Wounded: " + IntToString(nWounded));

        switch (nAbilityID)
        {
            case EngineConstants.ARCHDEMON_DETONATE_DARKSPAWN:
                {
                    if (GetCreatureRank(oCreature) == EngineConstants.CREATURE_RANK_LIEUTENANT || GetCreatureRank(oCreature) == EngineConstants.CREATURE_RANK_BOSS)
                    {
                        nRet = EngineConstants.FALSE;
                        break;
                    }
                    // Special case... a darkspawn is 'valid' if he has some enemies near him
                    List<GameObject> arEnemies = GetNearestObjectByHostility(oCreature, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_CREATURE, 5, EngineConstants.TRUE, EngineConstants.FALSE, EngineConstants.FALSE);
                    int i;
                    int nSize = GetArraySize(arEnemies);
                    Log_Trace_AI("_AI_IsTargetValidForBeneficialAbility", "array size: " + IntToString(nSize));

                    GameObject oCurrent;
                    float fDistance;
                    nRet = EngineConstants.FALSE;
                    for (i = 0; i < nSize; i++)
                    {
                        oCurrent = arEnemies[i];
                        fDistance = GetDistanceBetween(oCurrent, oCreature);
                        if (fDistance <= EngineConstants.ARCHDEMON_DETONATE_RADIUS)
                        {
                            Log_Trace_AI("_AI_IsTargetValidForBeneficialAbility", "Got enemy near detonate darkspawn: " + GetTag(oCurrent));
                            nRet = EngineConstants.TRUE;
                            break;
                        }
                    }
                    break;
                }
            case EngineConstants.ABILITY_SPELL_HEAL:
                {
                    if (nWounded != EngineConstants.FALSE)
                        nRet = EngineConstants.TRUE;
                    else
                        nRet = EngineConstants.FALSE;
                    break;
                }
            case EngineConstants.ABILITY_SPELL_CURE:
                {
                    nRet = EngineConstants.TRUE;
                    // valid if mana/stamina is not maxed

                    float fCurrentManaStamina = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT);
                    float fMaxManaStamina = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_BASE);
                    if (fCurrentManaStamina == fMaxManaStamina)
                        nRet = EngineConstants.FALSE;

                    break;
                }
            case EngineConstants.ABILITY_SPELL_PURIFY:
                {
                    // check wounds
                    if (nWounded != EngineConstants.FALSE)
                        nRet = EngineConstants.TRUE;
                    else
                        nRet = EngineConstants.FALSE;

                    // .. and curable effects
                    List<xEffect> eEffects = GetEffects(oCreature);
                    int nAbility;

                    // cycle through effects
                    int nCount = 0;
                    int nMax = GetArraySize(eEffects);
                    for (nCount = 0; nCount < nMax; nCount++)
                    {
                        // get xEffect ability id
                        xEffect _effect = eEffects[nCount];
                        nAbility = GetEffectAbilityIDRef(ref _effect);

                        // is ability curable
                        if (Ability_CheckFlag(nAbility, EngineConstants.ABILITY_FLAG_CURABLE) != EngineConstants.FALSE)
                        {
                            nRet = EngineConstants.TRUE;
                            break;
                        }
                    }
                    break;
                }
            case EngineConstants.ABILITY_SPELL_SHIELD_PARTY: // dispel magic
            case EngineConstants.ABILITY_SPELL_ANTIMAGIC_BURST:
                {
                    if (nDebuf != EngineConstants.FALSE)
                        nRet = EngineConstants.TRUE;
                    else
                        nRet = EngineConstants.FALSE;

                    break;
                }
        }
        return nRet;
    }

    // Returns a party member target, if valid
    public GameObject _AI_GetPartyTarget(int nTargetType, int nCommandType, int nCommandSubType, int nTacticID)
    {
        GameObject oTarget = null;

        GameObject oTacticTargetObject = GetTacticTargetObject(gameObject, nTacticID);

        if (nTargetType == EngineConstants.AI_TARGE_TYPE_HERO)
            oTarget = GetHero();
        else if (nTargetType == EngineConstants.AI_TARGET_TYPE_MAIN_CONTROLLED)
            oTarget = GetMainControlled();
        else
            oTarget = oTacticTargetObject;

        if (IsObjectValid(oTarget) != EngineConstants.FALSE && nCommandType == EngineConstants.AI_COMMAND_USE_ABILITY && Ability_IsAbilityActive(oTarget, nCommandSubType) != EngineConstants.FALSE)
            oTarget = null;

        return oTarget;
    }

    /* @brief Checks if a target is valid to be hostile target
*
* The checks includes: hostility, dead check, dying check, special AI ignore flags.
*
* @param oTarget the target being checked to see if valid as hostile target
* @returns EngineConstants.TRUE if target is valid as hostile target, EngineConstants.FALSE otherwise
* @author Yaron
*/
    public int _AI_IsHostileTargetValid(GameObject oTarget)
    {
        int nRet = EngineConstants.TRUE;
#if DEBUG
        string DEBUG_sInvalidReason = "";
#endif

        if (IsObjectValid(oTarget) == EngineConstants.FALSE)
        {
#if DEBUG
            DEBUG_sInvalidReason = "INVALID OBJECT";
#endif
            nRet = EngineConstants.FALSE;
        }
        else if (IsDead(oTarget) != EngineConstants.FALSE)
        {
#if DEBUG
            DEBUG_sInvalidReason = "DEAD";
#endif
            nRet = EngineConstants.FALSE;
        }
        else if (IsDying(oTarget) != EngineConstants.FALSE)
        {
#if DEBUG
            DEBUG_sInvalidReason = "DYING";
#endif
            nRet = EngineConstants.FALSE;
        }
        else if (GetObjectActive(oTarget) == EngineConstants.FALSE)
        {
#if DEBUG
            DEBUG_sInvalidReason = "TARGET INACTIVE";
#endif
            nRet = EngineConstants.FALSE;
        }
        else if (IsObjectHostile(gameObject, oTarget) == EngineConstants.FALSE)
        {
#if DEBUG
            DEBUG_sInvalidReason = "NOT HOSTILE";
#endif
            nRet = EngineConstants.FALSE;
        }
        else if (Effects_HasAIModifier(gameObject, EngineConstants.AI_MODIFIER_IGNORE) != EngineConstants.FALSE)
        {
#if DEBUG
            DEBUG_sInvalidReason = "AI IGNORE FLAG ACTIVE";
#endif
            nRet = EngineConstants.FALSE;
        }
        else if (IsPerceiving(gameObject, oTarget) == EngineConstants.FALSE)
        {
#if DEBUG
            DEBUG_sInvalidReason = "TARGET NOT PERCEIVED";
#endif
            nRet = EngineConstants.FALSE;
        }
        else if (IsStealthy(oTarget) != EngineConstants.FALSE)
        {
#if DEBUG
            DEBUG_sInvalidReason = "TARGET STEALTHY";
#endif
            nRet = EngineConstants.FALSE;
        }

#if DEBUG
        if (nRet == EngineConstants.FALSE)
        {
            Log_Trace_AI("_AI_IsHostileTargetValid", "Target invalid: " + DEBUG_sInvalidReason);
        }
#endif

        return nRet;
    }

    /* @brief Returns the currently equipped weapon set
*
* @returns the currently equipped weapon set (EngineConstants.AI_WEAPON_SET_*)
* @author Yaron
*/
    //removed: May needs to be redone DHK zDA:O
}