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
     // effect_impact_h
     // -----------------------------------------------------------------------------
     /*
         Effect Impact
             Effect structure that stores impact information.

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"effect_constants_h"
     //#include"2da_constants_h"

     // MGB - February 23, 2009
     // EffectImpact Constructor moved into Engine.

     // -----------------------------------------------------------------------------
     //  Effects_HandleApplyEffectImpact
     // -----------------------------------------------------------------------------
     //  Created By: Georg Zoeller
     //  Created On: July 2007
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectImpact(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     //  Effects_HandleRemoveEffectImpact
     // -----------------------------------------------------------------------------
     //  Created By: Georg Zoeller
     //  Created On: July 2007
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectImpact(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }
}