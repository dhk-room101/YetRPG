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
     //#include"effect_constants_h"

     public xEffect EffectConeCasting(int nVfx)
     {
          xEffect e = Effect(EngineConstants.EFFECT_TYPE_CONECASTING);
          SetEffectEngineIntegerRef(ref e, EngineConstants.EFFECT_INTEGER_VFX, nVfx);
          return e;

     }
}