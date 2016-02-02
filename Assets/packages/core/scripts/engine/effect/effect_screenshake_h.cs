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
     // effect_ai_modifier
     // -----------------------------------------------------------------------------
     /*
         Effect: AI Modifier

                 Used to apply modifiers taken into account by the game AI to the
                 creature

                 Example: EngineConstants.AI_MODIFIER_IGNORE - AI ignores the target as if it was
                                               not there

                 All these required manual integration into the game AI

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"effect_constants_h"
     //#include"2da_constants_h"

     //moved public const int SCREEN_SHAKE_TYPE_DEFAULT = 1;
     //moved public const int SCREEN_SHAKE_TYPE_CRITICAL_HIT = 2;
     //moved public const int SCREEN_SHAKE_TYPE_OGRE_STOMP = 3;
     //moved public const int SCREEN_SHAKE_TYPE_BROODMOTHER_SCREEM = 7;  
     //moved public const int SCREEN_SHAKE_TYPE_WILD_SYLVAN = 8;

     public xEffect EffectScreenShake(int nScreenShakeType)
     {

          xEffect eEffect = Effect(6);

          float fFreqX = GetM2DAFloat(EngineConstants.TABLE_SCREEN_SHAKE, "frequency_x", nScreenShakeType);
          float fFreqY = GetM2DAFloat(EngineConstants.TABLE_SCREEN_SHAKE, "frequency_y", nScreenShakeType);
          float fAmpX = GetM2DAFloat(EngineConstants.TABLE_SCREEN_SHAKE, "amplitude_x", nScreenShakeType);
          float fAmpY = GetM2DAFloat(EngineConstants.TABLE_SCREEN_SHAKE, "amplitude_y", nScreenShakeType);

          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "EffectScreenShake", "Creating screen shake xEffect with " +
                                          ToString(fFreqX) + "," + ToString(fFreqY) + "," + ToString(fAmpX) + "," + ToString(fAmpY));

          SetEffectFloatRef(ref eEffect, 0, fFreqX);   // <frequency_x>
          SetEffectFloatRef(ref eEffect, 1, fFreqY);   // <frequency_y>
          SetEffectFloatRef(ref eEffect, 2, fAmpX);    // <amplitude_x>
          SetEffectFloatRef(ref eEffect, 3, fAmpY);    // <amplitude_y>

          return eEffect;
     }
}