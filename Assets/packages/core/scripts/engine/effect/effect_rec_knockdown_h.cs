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
     // -----------------------------------------------------------------------------                                                                                                     // -----------------------------------------------------------------------------
     // effect_rec_knockdown_h
     // -----------------------------------------------------------------------------
     /*

     */
     // -----------------------------------------------------------------------------
     // Owner: PeterT
     // -----------------------------------------------------------------------------

     //#include"log_h"

     public xEffect EffectRecurringKnockdown()
     {

          return Effect(EngineConstants.EFFECT_TYPE_RECURRING_KNOCKDOWN);
     }

     public int Effects_HandleApplyEffectRecurringKnockdown(xEffect eEffect)
     {
          // knockdown the player for a set amount of time

          return 1;
     }

     public int Effects_HandleRemoveEffectRecurringKnockdown(xEffect eEffect)
     {
          // check to see if the xEffect should be removed
          // if not, delay apply again

          return 1;
     }
}