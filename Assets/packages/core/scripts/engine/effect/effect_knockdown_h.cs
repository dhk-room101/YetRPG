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

     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Yaron Jakobs
     //  Created On: Sep 13, 2006
     ///////////////////////////////////////////////////////////////////////////////
     public xEffect EffectKnockdown(GameObject oAttacker, int nDefensePenalty, int nAbility = EngineConstants.ABILITY_TALENT_SHIELD_BASH)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_KNOCKDOWN);
          SetEffectObjectRef(ref eEffect, 0, oAttacker);
          SetEffectIntegerRef(ref eEffect, 0, nDefensePenalty);
        SetEffectIntegerRef(ref eEffect, 1, nAbility);

          return eEffect;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Yaron Jakobs
     //  Created On: Sep 13, 2006
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleApplyEffectKnockdown(xEffect eEffect)
     {

          if (GetArraySize(GetEffects(gameObject, EngineConstants.EFFECT_TYPE_KNOCKDOWN)) > 1)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "ApplyKnockdown", "NOT applying knockdown, target already knocked down!");
               return EngineConstants.FALSE;
          }

          // This ie being removed because of possible corruption issues.
          //UpdateCreatureProperty(gameObject,EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE,-25.0f,EngineConstants.PROPERTY_VALUE_MODIFIER);

          return EngineConstants.TRUE;

     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Yaron Jakobs
     //  Created On: Sep 13, 2006
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleRemoveEffectKnockdown(xEffect eEffect)
     {

          // This ie being removed because of possible corruption issues.
          //UpdateCreatureProperty(gameObject,EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE,25.0f,EngineConstants.PROPERTY_VALUE_MODIFIER);

          return EngineConstants.TRUE;
     }
}