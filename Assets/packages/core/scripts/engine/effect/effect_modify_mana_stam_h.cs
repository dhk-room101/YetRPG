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
     // effect_modify_stamina
     // -----------------------------------------------------------------------------
     /*
         Effect: Modify Stamina

                 When applied to a creature, this xEffect increase or decreases the
                 creature�s current stamina. The creature�s  current stamina will not
                 be raised above its maximum stamina nor will it be lowered bellow 0.
     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"2da_constants_h"
     //#include"wrappers_h"
     //#include"events_h"
     //#include"effect_constants_h"

     public xEffect EffectModifyManaStamina(float fValue)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_MODIFYMANASTAMINA);
          SetEffectFloatRef(ref eEffect, 0, fValue);
          return eEffect;
     }

     public int Effect_InstantApplyEffectModifyManaStamina(GameObject oTarget, float fAmount)
     {

          if (fAmount == 0.0f)
          {
               return EngineConstants.FALSE;
          }

          UpdateCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fAmount, EngineConstants.PROPERTY_VALUE_CURRENT);

          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the application of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectModifyManaStamina(xEffect eEffect)
     {
          float fAmount = GetEffectFloatRef(ref eEffect, 0);
          GameObject oTarget = gameObject;

          return Effect_InstantApplyEffectModifyManaStamina(oTarget, fAmount);
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectModifyManaStamina(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }
}