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
     // effect_sleep_h
     // -----------------------------------------------------------------------------
     /*
         Effect Sleep

             When applied to an object, this effect
             * applies the sleep effect
             */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------


     //#include"effect_constants_h"
     //#include"log_h"  

     //moved public const float _EFFECT_SLEEP_DEXTERITY_DEBUFF = -1000.0f;

     /* @brief Creates a sleep effect
     *                 
     * @param nEffectID - The specific sleep xEffect (EngineConstants.EFFECT_TYPE_SLEEP or EngineConstants.EFFECT_TYPE_SLEEP_PLOT).
     * @returns A valid xEffect of type EngineConstants.EFFECT_TYPE_SLEEP (or EngineConstants.EFFECT_TYPE_SLEEP_PLOT).
     */
     public xEffect EffectSleep(int nEffectID = EngineConstants.EFFECT_TYPE_SLEEP)
     {
          xEffect eSleep = Effect(nEffectID);
          SetEffectEngineIntegerRef(ref eSleep, EngineConstants.EFFECT_INTEGER_VFX, EngineConstants.VFX_CRUST_SLEEP);
          return eSleep;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleApplyEffectSleep
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Georg Zoeller
     //  Created On: Jan 2007
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleApplyEffectSleep(xEffect eEffect)
     {

          if (GetArraySize(GetEffects(gameObject, EngineConstants.EFFECT_TYPE_SLEEP)) > 1  /*this includes this xEffect */)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "ApplySleep", "NOT applying sleep, target already sleepling!");
               return EngineConstants.FALSE;
          }

          // Sleep debuffs dexterity. Note that we're subtracting a very large number here, but because the
          // property system confines the actual value between floor and ceiling, it will never drop below 1.
          // This is intended behavior.
          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, EngineConstants._EFFECT_SLEEP_DEXTERITY_DEBUFF, EngineConstants.PROPERTY_VALUE_MODIFIER);

          return EngineConstants.TRUE;
     }

     ///////////////////////////////////////////////////////////////////////////////
     //  Effects_HandleRemoveEffectSleep
     ///////////////////////////////////////////////////////////////////////////////
     //  Created By: Georg Zoeller
     //  Created On: Jan 2007
     ///////////////////////////////////////////////////////////////////////////////
     public int Effects_HandleRemoveEffectSleep(xEffect eEffect)
     {

          UpdateCreatureProperty(gameObject, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, (EngineConstants._EFFECT_SLEEP_DEXTERITY_DEBUFF * -1), EngineConstants.PROPERTY_VALUE_MODIFIER);
          return EngineConstants.TRUE;
     }


     /* @brief Causes a creature to sleep.
     *
     * @param oCreature - The creature to force asleep.
     */
     public void ForceSleepStart(GameObject oCreature)
     {
          if (IsObjectValid(oCreature) != EngineConstants.FALSE)
          {
               xEffect eSleep = Effect(EngineConstants.EFFECT_TYPE_SLEEP_PLOT);
               Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eSleep, oCreature, 0.0f, oCreature, 0);
          }
     }

     /* @brief Causes a sleeping creature to wake.
     *
     * @param oCreature - The creature to wake.
     */
     public void ForceSleepEnd(GameObject oCreature)
     {
          List<xEffect> arEffects = GetEffects(oCreature);
          int i;
          for (i = 0; i < GetArraySize(arEffects); i++)
          {
            xEffect _effect = arEffects[i];
               if (GetEffectTypeRef(ref _effect) == EngineConstants.EFFECT_TYPE_SLEEP_PLOT
                  && GetEffectCreatorRef(ref _effect) == oCreature)
               {
                    RemoveEffect(oCreature, arEffects[i]);
               }
          }
     }
}