//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Engine
{
     // -----------------------------------------------------------------------------
     // xEffect_addability
     // -----------------------------------------------------------------------------
     /*
         Effect: Add an ability for the duration of the xEffect

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------


     //#include "log_h"
     //#include "core_h"
     //#include "2da_constants_h"
     //#include "wrappers_h"
     //#include "xEvents_h"
     //#include "effect_constants_h"



     public xEffect EffectAddAbility(int nAbility, GameObject oOwner)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_ADDABILITY);
          SetEffectIntegerRef(ref eEffect, 0, nAbility);
          SetEffectObjectRef(ref eEffect, 0, oOwner);
          return eEffect;
     }



     // -----------------------------------------------------------------------------
     // This utility function handles the application of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectAddAbility(xEffect eEffect)
     {
          int nAbility = GetEffectIntegerRef(ref eEffect, 0);
          if (nAbility > EngineConstants.ABILITY_INVALID)
          {

               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "AddAbility:", ToString(nAbility));


               AddAbility(gameObject, nAbility);
               SetQuickslot(gameObject, 7, nAbility);
               return EngineConstants.TRUE;
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "AddAbility Failed:", ToString(nAbility));
               return EngineConstants.FALSE;
          }

     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectAddAbility(xEffect eEffect)
     {
          int nAbility = GetEffectIntegerRef(ref eEffect, 0);
          RemoveAbility(gameObject, nAbility);
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "AddAbility - REMOVE:", ToString(nAbility));


          return EngineConstants.TRUE;
     }

     /*public void AddItemAbilityByEffect(GameObject oItem, GameObject oTarget);
     public void AddItemAbilityByEffect(GameObject oItem, GameObject oTarget)
     {
         int nAbility = GetItemAbilityId(oItem);

         if (nAbility > INVALID_ITEM_ABILITY)
         {
             xEffect e = EffectAddAbility(nAbility, oItem);
             ApplyEffectOnObject(4  ,e,oTarget, 0.0f, oItem, 0);
         }

     }


     public void RemoveItemAbilityEffect(GameObject oItem, GameObject oTarget)
     {
         int nAbilityId = GetItemAbilityId(oItem);
         if (nAbilityId >  INVALID_ITEM_ABILITY)
         {
             List<xEffect> xEffects = GetEffects(oTarget, EngineConstants.EFFECT_TYPE_ADDABILITY, 0, oItem,4);
             if (GetArraySize(xEffects)>0)
             {
                 RemoveEffect(oTarget, xEffects[0]);
             }
         }
     }*/
}