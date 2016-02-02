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
     //public void main() {} // FOR DEBUG ONLY! MUST ALWAYS BE COMMENTED OUT!
     // -----------------------------------------------------------------------------------------------------------------------------------------------------
     // effect_confussion_h
     // -----------------------------------------------------------------------------
     /*
         Effect: Confusion

     */
     // -----------------------------------------------------------------------------
     // Owner: Austin Peckenpaugh
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"effect_constants_h"
     //#include"effect_death_h"
     //#include"2da_data_h"
     //#include"wrappers_h"
     //#include"effect_upkeep_h"
     //#include"ai_threat_h"
     //#include"sys_resistances_h"

     //moved public const int EngineConstants.CONFUSION_GROUP_1 = 5056;
     //moved public const int EngineConstants.CONFUSION_GROUP_2 = 5057;
     //moved public const int EngineConstants.CONFUSION_GROUP_3 = 5058;
     //moved public const int EngineConstants.CONFUSION_GROUP_4 = 5059;
     //moved public const int EngineConstants.CONFUSION_GROUP_5 = 5060;

     //moved public const float MINIMUM_MEDIUM_CUTOFF = 0.53;
     //moved public const float MINIMUM_LARGE_CUTOFF = 1.401;
     //moved public const int EngineConstants.CREATURE_SIZE_SMALL = 1;
     //moved public const int EngineConstants.CREATURE_SIZE_MEDIUM = 2;
     //moved public const int EngineConstants.CREATURE_SIZE_LARGE = 3;

     // return a xCommand that make oTarget move to a random nearby place
     public xCommand _MoveToRandomPlace(GameObject oCaster, GameObject oTarget)
     {

          // the creature is going to randomly move to a nearby "safe" location
          List<Vector3> lLocations;

          int nLocation = 0;
          GameObject oArea = GetArea(oTarget);

          float fX = GetPosition(oTarget).x;
          float fY = GetPosition(oTarget).y;
          float fZ = GetPosition(oTarget).z;
          Vector3 lCaster = GetSafeLocation(GetLocation(oCaster));
          Vector3 lStart = GetSafeLocation(GetLocation(oTarget));

          float fRadius = EngineConstants.EFFECT_CONFUSION_WANDER_RADIUS;
          float fNewX = fX + (Engine_Random(2) - 1) * fRadius;
          float fNewY = fY + (Engine_Random(2) - 1) * fRadius;
          float fNewZ = fZ;

          Vector3 vNewPosition = Vector(fNewX, fNewY, fNewZ);
          float fNewAngle = RandomFloat() * 360;

          Vector3 lNewLocation = Location(oArea, vNewPosition, fNewAngle);

          // DEBUG information about the location. Uncomment if you are suspicious
          //    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "EFFECT CONFUSION H",
          //                    " CORDINATES: "
          //                    + "Is ("      + ToString(GetPositionFromLocation(lNewLocation).x)
          //                    + ","       + ToString(GetPositionFromLocation(lNewLocation).y)
          //                    + ","       + ToString(GetPositionFromLocation(lNewLocation).z)
          //                    +") Was: (" + ToString(GetPositionFromLocation(lNewLocation).x)
          //                    +","        + ToString(GetPositionFromLocation(lNewLocation).y)
          //                    +","        + ToString(GetPositionFromLocation(lNewLocation).z)
          //                    +")"
          //                    , oTarget
          //              );

          // make sure the position is safe
          lNewLocation = GetSafeLocation(lNewLocation);
          if (IsLocationValid(lNewLocation) == EngineConstants.FALSE)
          {
               lNewLocation = lStart;
               if (IsLocationValid(lNewLocation) == EngineConstants.FALSE)
               {
                    lNewLocation = lCaster;
               }
          }

          // issue the move command
          xCommand cMove = CommandMoveToLocation(lNewLocation, EngineConstants.FALSE);
          return cMove;

     }

     // Returns the creature's size based on its ring size in EngineConstants.APR_base
     public int _GetCreatureSize(GameObject oObject)
     {
          // Get creature's appearance from the 2DA EngineConstants.APR_base
          int nAppearance = GetAppearanceType(oObject);
          float fAppearance = GetM2DAFloat(EngineConstants.TABLE_APPEARANCE, "PERSPACE", nAppearance);
          // Depending on the creature's size (float values)
          if (fAppearance < EngineConstants.MINIMUM_MEDIUM_CUTOFF)
          {
               // Creature is small
               int nSize = EngineConstants.CREATURE_SIZE_SMALL;
               return nSize;
          }
          else
          {
               if (fAppearance >= EngineConstants.MINIMUM_LARGE_CUTOFF)
               {
                    // Creature is large
                    int nSize = EngineConstants.CREATURE_SIZE_LARGE;
                    return nSize;
               }
               else
               {
                    // Creature is medium
                    int nSize = EngineConstants.CREATURE_SIZE_MEDIUM;
                    return nSize;
               }
          }
     }

     /* @brief Creates and apply a confusion xEffect on a creature
     *
     * @param oCaster - the character who created the confusion effect
     * @param oTarget - the character who is affected by the confusion effect
     * @param nAbility - the ability which creates the confusion effect
     * @param fBaseDuration - the unmodified duration of the confusion effect. Some creatures are more affected than others
     * @param nVFX - the visual xEffect to play on the confused creature while he is confused
     * @returns the confusion effect
     */

     public xEffect ApplyConfusionEffectOnCreature(GameObject oCaster, GameObject oTarget, int nAbility, float fBaseDuration, int nVFX)
     {
          xEffect eConfusion = Effect(EngineConstants.EFFECT_TYPE_CONFUSION);
          if ((IsEffectValid(eConfusion) != EngineConstants.FALSE) && (IsImmuneToEffectType(oTarget, EngineConstants.EFFECT_TYPE_CONFUSION) == EngineConstants.FALSE))
          {
               if (nVFX > 0)
               {
                    SetEffectEngineIntegerRef(ref eConfusion, EngineConstants.EFFECT_INTEGER_VFX, nVFX);
               }

               if (IsDead(oTarget) == EngineConstants.FALSE)
               {
                    float fDuration;

                    // confuse behave differently on party members and on NPCs
                    if (IsPartyMember(oTarget) != EngineConstants.FALSE)
                    {
                         fDuration = fBaseDuration;

                         // confuse slow the party member and make it moves to a nearby spot in a manner
                         // that the Player cannot cancel
                         xEffect eSlow = Effect(EngineConstants.EFFECT_TYPE_MOVEMENT_RATE);
                         SetEffectEngineFloatRef(ref eSlow, EngineConstants.EFFECT_FLOAT_POTENCY, 0.5f);
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eSlow, oTarget, fBaseDuration / 2, oCaster, nAbility);

                         xCommand cMove = _MoveToRandomPlace(oCaster, oTarget);
                         AddCommand(oTarget, cMove, EngineConstants.TRUE, EngineConstants.TRUE);

                    }
                    else
                    {
                         // adjust duration based on creature "rank"
                         fDuration = GetRankAdjustedEffectDuration(oTarget, fBaseDuration);

                         // save the real group ID of the target. This will be used if the target group is modified
                         int nGroup = GetGroupId(oTarget);
                         SetEffectIntegerRef(ref eConfusion, EngineConstants.EFFECT_CONFUSION_EFFECT_INTEGER_INDEX, nGroup);

                         // Removed: Random chance to turn on his friends and join a random group ID
                         // int nAttackFriendChance     = FloatToInt(GetRankAdjustedEffectDuration(oTarget, 1.0f) * EngineConstants.EFFECT_CONFUSION_SWITCH_FACTION_PERCENTAGE);
                         // int nRandom;
                         // nRandom = Engine_Random(100)+1;

                         //always true DHK
                         if (EngineConstants.TRUE == EngineConstants.TRUE) // (nRandom <= nAttackFriendChance)
                         {
                              int nConfusionGroupID = 0;
                              int nRandomID = Engine_Random(5);
                              switch (nRandomID)
                              {
                                   case 0:
                                        {
                                             nConfusionGroupID = EngineConstants.CONFUSION_GROUP_1;
                                        }
                                        break;
                                   case 1:
                                        {
                                             nConfusionGroupID = EngineConstants.CONFUSION_GROUP_2;
                                        }
                                        break;
                                   case 2:
                                        {
                                             nConfusionGroupID = EngineConstants.CONFUSION_GROUP_3;
                                        }
                                        break;
                                   case 3:
                                        {
                                             nConfusionGroupID = EngineConstants.CONFUSION_GROUP_4;
                                        }
                                        break;
                                   case 4:
                                        {
                                             nConfusionGroupID = EngineConstants.CONFUSION_GROUP_5;
                                        }
                                        break;
                              }

                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_1, EngineConstants.CONFUSION_GROUP_2, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_1, EngineConstants.CONFUSION_GROUP_3, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_1, EngineConstants.CONFUSION_GROUP_4, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_1, EngineConstants.CONFUSION_GROUP_5, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_2, EngineConstants.CONFUSION_GROUP_3, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_2, EngineConstants.CONFUSION_GROUP_4, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_2, EngineConstants.CONFUSION_GROUP_5, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_3, EngineConstants.CONFUSION_GROUP_4, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_3, EngineConstants.CONFUSION_GROUP_5, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_4, EngineConstants.CONFUSION_GROUP_5, EngineConstants.TRUE);

                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_1, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_2, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_3, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_4, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_5, EngineConstants.GROUP_HOSTILE, EngineConstants.TRUE);

                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_1, EngineConstants.GROUP_PC, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_2, EngineConstants.GROUP_PC, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_3, EngineConstants.GROUP_PC, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_4, EngineConstants.GROUP_PC, EngineConstants.TRUE);
                              SetGroupHostility(EngineConstants.CONFUSION_GROUP_5, EngineConstants.GROUP_PC, EngineConstants.TRUE);

                              /*  SetGroupHostility(EngineConstants.CONFUSION_GROUP_1, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE);
                                SetGroupHostility(EngineConstants.CONFUSION_GROUP_2, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE);
                                SetGroupHostility(EngineConstants.CONFUSION_GROUP_3, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE);
                                SetGroupHostility(EngineConstants.CONFUSION_GROUP_4, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE);
                                SetGroupHostility(EngineConstants.CONFUSION_GROUP_5, EngineConstants.GROUP_FRIENDLY, EngineConstants.TRUE); */
                              // -------------------------------------------------

                              // Set the target's group ID to the randomly chosen group number
                              SetGroupId(oTarget, nConfusionGroupID);
                              LogTrace(EngineConstants.LOG_CHANNEL_REWARDS, ToString(oTarget) + " belongs to group " + ToString(nConfusionGroupID));

                              // clear threats
                              //ClearThreatTable(oTarget); // make me forget who I hate
                              ClearAllCommands(oTarget); // stop what I was doing

                              // Get a target and attack it - this is not 100% needed but give much better results
                              // and allow us to handle the case of the mob still wanting to beat down on a party member
                              List<GameObject> oThreats = GetNearestObjectByHostility(oTarget, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_CREATURE, 1, 1, 1);
                              GameObject oThreat = oThreats[0];

                              AI_Threat_SetThreatTarget(oTarget, oThreat);

                              if (IsPartyMember(oThreat) != EngineConstants.FALSE)
                              {
                                   // if the creature is still targetting a party member, add a
                                   // bit of slow and random move to make the spell more useful

                                   xEffect eSlow = Effect(EngineConstants.EFFECT_TYPE_MOVEMENT_RATE);
                                   SetEffectEngineFloatRef(ref eSlow, EngineConstants.EFFECT_FLOAT_POTENCY, 0.5f);
                                   ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eSlow, oTarget, fDuration / 4, oCaster, nAbility);

                                   xCommand cPreMoveAnim = CommandPlayAnimation(EngineConstants.EFFECT_CONFUSION_ANIMATION_ID, 1);
                                   xCommand cMove = _MoveToRandomPlace(oCaster, oTarget);
                                   xCommand cPostMoveAnim = CommandPlayAnimation(EngineConstants.EFFECT_CONFUSION_ANIMATION_ID, 1);

                                   // this works well, however dead enemy will finish playing daze animation before falling to the ground
                                   AddCommand(oTarget, cPostMoveAnim, EngineConstants.FALSE, EngineConstants.TRUE);
                                   AddCommand(oTarget, cMove, EngineConstants.FALSE, EngineConstants.TRUE);
                                   AddCommand(oTarget, cPreMoveAnim, EngineConstants.FALSE, EngineConstants.TRUE);    // was EngineConstants.FALSE EngineConstants.TRUE

                              }
                         }
                    }

                    // Apply confusion xEffect proper
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eConfusion, oTarget, fDuration, oCaster, nAbility);

                    // apply DAZE VFX - this is common to all confusion xEffect regardless of its nature (e.g. bard 'confuse' versus 'waking nightmare' confuse)
                    ApplyEffectVisualEffect(oCaster, oTarget, EngineConstants.EFFECT_CONFUSION_DAZE_VFX, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, fDuration, nAbility);
               }
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "ApplyConfusion", "$$$ Effect not valid; target is immune");
          }
          return eConfusion;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleApplyEffectConfusion
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Austin Peckenpaugh
     //  Created On: Oct 2008
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleApplyEffectConfusion(xEffect eEffect)
     {
          //Do not apply confusion if already confused
          int nArraySize = GetArraySize(GetEffects(gameObject, EngineConstants.EFFECT_TYPE_CONFUSION));
          if (nArraySize > 0  /*this includes this xEffect */)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "ApplyConfusion", "Not applying confusion because target is already confused! ArraySize is " + ToString(nArraySize));
               return EngineConstants.FALSE;
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "ApplyConfusion", "Applying confusion! ArraySize is " + ToString(nArraySize));
          }
          return EngineConstants.TRUE;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleRemoveEffectConfusion
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Austin Peckenpaugh
     //  Created On: Oct 2008
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleRemoveEffectConfusion(xEffect eEffect)
     {
          //On removal, reassociate threat with caster
          GameObject oCreature = gameObject;

          int nRealGroupId = GetEffectIntegerRef(ref eEffect, EngineConstants.EFFECT_CONFUSION_EFFECT_INTEGER_INDEX);
          Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "EFFECT CONFUSION H", " Effect Int stop: " + ToString(nRealGroupId));

          if (nRealGroupId != 0)
          {
               int nCurrentGroupId = GetGroupId(gameObject);

               // only change group id if it's one of the confusion groups. otherwise it got overridden during the
               // xEffect duration from script and should not be touched, period.
               if (nCurrentGroupId == EngineConstants.CONFUSION_GROUP_1 ||
                   nCurrentGroupId == EngineConstants.CONFUSION_GROUP_2 ||
                   nCurrentGroupId == EngineConstants.CONFUSION_GROUP_3 ||
                   nCurrentGroupId == EngineConstants.CONFUSION_GROUP_4 ||
                   nCurrentGroupId == EngineConstants.CONFUSION_GROUP_5)
               {
                    SetGroupId(oCreature, nRealGroupId);
               }
          }
          //ClearThreatTable(oCreature); // make me forget who I hate
          AI_Threat_UpdateEnemyAppeared(oCreature, GetEffectCreatorRef(ref eEffect));

          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_confusion_h.HandleRemoveEffectConfusion", "Confusion removed from " + ToString(oCreature));

          return EngineConstants.TRUE;
     }

     public int Effects_HandleConfusionCallback(GameObject oCreature)
     {
          return EngineConstants.TRUE;
     }
}