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
     // gore_h - Gore related function include
     // -----------------------------------------------------------------------------
     /*
                 Gore System, Brief Technical Overview

         Gore is handled by the engine on a per creature basis and modified via the
         SetCreatureGoreLevel(float fLevel) engine command.

         fLevel ranges between 0.0f (no gore) and 1.0f (max gore)

         Unless the game plot calls for it, gore handling in the game is transparent,
         handled by EffectDamage, if created with EngineConstants.DAMAGE_FLAG_UPDATE_GORE.

     */
     // -----------------------------------------------------------------------------
     // georg@2006/12/01
     // -----------------------------------------------------------------------------

     //#include"log_h"

     //moved public const float GORE_CHANGE_HIT        =  0.001f; // 1000 hits equal full gore
     //moved public const float GORE_CHANGE_CRITICAL   =  0.005f;  // 50 hits equal full gore
     //moved public const float GORE_CHANGE_DEATHBLOW  =  0.010f;

     /* ----------------------------------------------------------------------------
     * @brief Modifies the GoreLevel on a Creature by fGoreChange
     *        GoreLevel ranges from 0.0(no gore) to 1.0f (max gore), any excess values
     *        are clipped.
     *
     *       Predefined values:
     *                //moved public const float GORE_CHANGE_HIT        =  0.01f;
     *                //moved public const float GORE_CHANGE_CRITICAL   =  0.15f;
     *                //moved public const float GORE_CHANGE_DEATHBLOW  =  0.25f;
     *
     * @param oCreature    The creature who gets the gore level upgrade
     * @param fGoreChange  The amount by which to increase or decrease the gore level
     *
     * @author   Georg Zoeller
     *  -----------------------------------------------------------------------------
     **/
     public void Gore_ModifyGoreLevel(GameObject oCreature, float fGoreChange)
     {
          if (GetM2DAInt(EngineConstants.TABLE_APPEARANCE, "bDisableGore", GetAppearanceType(oCreature)) == EngineConstants.FALSE)
          {
               float fCurrentGore = GetCreatureGoreLevel(oCreature);

               if (GetCreatureGoreLevel(oCreature) < 0.6f)
               {
                    fCurrentGore += fGoreChange;
                    if (fCurrentGore > 1.0f)
                         fCurrentGore = 1.0f;
                    else if (fCurrentGore < 0.0f)
                         fCurrentGore = 0.0f;
                    SetCreatureGoreLevel(oCreature, fCurrentGore);
#if DEBUG
                    Log_Trace_Combat("gore_h.Gore_ModifyGoreLevel", "gore changed to: " + FloatToString(fCurrentGore), null, oCreature, EngineConstants.LOG_CHANNEL_COMBAT_GORE);
#endif
               }
          }
     }

     /* ----------------------------------------------------------------------------
     * @brief Removes all gore from the specified creature
     * @param oCreature    The creature who gets cleaned
     *
     * @author   Georg Zoeller
     *  -----------------------------------------------------------------------------
     **/
     public void Gore_RemoveAllGore(GameObject oCreature)
     {
          SetCreatureGoreLevel(oCreature, 0.0f);
     }
}