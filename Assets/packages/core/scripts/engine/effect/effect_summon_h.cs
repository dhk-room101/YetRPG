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
     // -----------------------------------------------------------------------------------------------------------------------------------------------------
     // effect_summon_h
     // -----------------------------------------------------------------------------
     /*
         Effect: Summon

         Tethers a summoned creature to it's master.

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"effect_constants_h"
     //#include"effect_death_h"
     //#include"2da_data_h"
     //#include"wrappers_h"
     //#include"effect_upkeep_h"
     public xEffect EffectSummon(GameObject oOwner, GameObject oCreature)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_SUMMON);
          SetEffectObjectRef(ref eEffect, 0, oOwner);
          SetEffectObjectRef(ref eEffect, 1, oCreature);

          return eEffect;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the application of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectSummon(xEffect eEffect)
     {

          GameObject oOwner = GetEffectObjectRef(ref eEffect, 0);
          GameObject oCreature = GetEffectObjectRef(ref eEffect, 1);

          if ((gameObject == oOwner || gameObject == oCreature) && IsObjectValid(oCreature) != EngineConstants.FALSE)
          {
               //     ApplyEffectVisualEffect(oCreature, oCreature, 1070,EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f, GetEffectAbilityIDRef(ref eEffect));

               if (gameObject == oCreature)
               {
                    SetLocalInt(oCreature, EngineConstants.IS_SUMMONED_CREATURE, EngineConstants.TRUE);
                    SetGroupId(oCreature, GetGroupId(oOwner));

                    xEvent eActivate = Event(EngineConstants.EVENT_TYPE_SET_OBJECT_ACTIVE);
                    SetEventIntegerRef(ref eActivate, 0, EngineConstants.TRUE);

                    if (IsPartyMember(oOwner) != EngineConstants.FALSE)
                    {
                         // the follower state is now ignored for this xEvent - it has moved to ability_summon_h
                         SetEventIntegerRef(ref eActivate, 1, EngineConstants.FOLLOWER_STATE_ACTIVE);
                    }

                    DelayEvent(0.5f, oCreature, eActivate);
               }

               return EngineConstants.TRUE;
          }
          else
          {
               return EngineConstants.FALSE;
          }

     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectSummon(xEffect eEffect)
     {
          GameObject oOwner = GetEffectObjectRef(ref eEffect, 0);
          GameObject oCreature = GetEffectObjectRef(ref eEffect, 1);

          if (gameObject == oOwner || gameObject == oCreature)
          {
               if (gameObject == oOwner)
               {
                    if (IsInvalidDeadOrDying(oCreature) == EngineConstants.FALSE && GetObjectActive(oCreature) != EngineConstants.FALSE)
                    {
                         SetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, 0.0f, EngineConstants.PROPERTY_VALUE_CURRENT);
                         SetObjectInteractive(oCreature, EngineConstants.FALSE);
                    }
               }
               else if (gameObject == oCreature)
               {
                    if (IsInvalidDeadOrDying(oOwner) == EngineConstants.FALSE)
                    {
                         xEvent evNotice = Event(EngineConstants.EVENT_TYPE_SUMMON_DIED);
                         SetEventIntegerRef(ref evNotice, 0, GetEffectAbilityIDRef(ref eEffect));
                         DelayEvent(0.0f, oOwner, evNotice);
                    }

                    if (IsInvalidDeadOrDying(oCreature) == EngineConstants.FALSE && GetObjectActive(oCreature) != EngineConstants.FALSE)
                    {
                         SetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, 0.0f, EngineConstants.PROPERTY_VALUE_CURRENT);
                         SetObjectInteractive(oCreature, EngineConstants.FALSE);
                    }

                    if (IsPartyMember(oCreature) != EngineConstants.FALSE)
                    {
                         WR_SetFollowerState(oCreature, EngineConstants.FOLLOWER_STATE_INVALID);
                    }

                    //ApplyEffectVisualEffect(oOwner, oCreature, 1070,EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f, GetEffectAbilityIDRef(ref eEffect));
               }
          }

          return EngineConstants.TRUE;
     }

     public void EffectSummon_HandleEventSummonDied(GameObject oOwner, int nAbility)
     {
          if (nAbility > 0)
          {
               Effects_RemoveUpkeepEffect(oOwner, nAbility);
          }
     }

     /*
       Returns the current summon for a specified caster.

       @author: Georg
     */
     public GameObject GetCurrentSummon(GameObject oCaster)
     {
          List<xEffect> eSummons = GetEffects(oCaster, EngineConstants.EFFECT_TYPE_SUMMON, 0, oCaster);
          int nSize = GetArraySize(eSummons);

          if (nSize != EngineConstants.FALSE)
          {
            xEffect _eSummon = eSummons[0];
               return GetEffectObjectRef(ref _eSummon, 1);
          }
          else
          {
               return null;
          }

     }

     public void RemoveAllSummons(int bDestroySummon = EngineConstants.FALSE)
     {
          List<GameObject> partyList = GetPartyPoolList();
          int nSize = GetArraySize(partyList);
          int i;

          // -------------------------------------------------------------------------
          // For all summoned creatures, strip all effects.
          // This removes the summon xEffect that teathers the creature to it's master
          // causing it to die on the spot and deactivates the summon ability
          // on the master. Yay for the ranger, abuser of wildlife.
          // -------------------------------------------------------------------------
          for (i = 0; i < nSize; i++)
          {
               if (IsSummoned(partyList[i]) != EngineConstants.FALSE)
               {
                    RemoveAllEffects(partyList[i]);

                    if (bDestroySummon != EngineConstants.FALSE)
                    {
                         DestroyObject(partyList[i]);
                    }
               }
          }

     }
}