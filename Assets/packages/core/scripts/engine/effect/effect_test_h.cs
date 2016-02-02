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
     // effect_test
     // -----------------------------------------------------------------------------
     /*

     */
     // -----------------------------------------------------------------------------
     // Owner: PeterT
     // -----------------------------------------------------------------------------

     //#include"log_h"

     public xEffect EffectTest()
     {

          return Effect(EngineConstants.EFFECT_TYPE_TEST);
     }

     public int Effects_HandleApplyEffectTest(xEffect eEffect)
     {
          // check to see if the player already has an item of this name
          GameObject oItem = GetItemPossessedBy(gameObject, "gen_im_arm_shd_kit_wdn");
          if (oItem != null)
          {
               // if true, return false
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, GetCurrentScriptName(), "Object Exists");
               return EngineConstants.FALSE;
          }
          else
          {
               // if false, create GameObject and return true
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, GetCurrentScriptName(), "Object Does Not Exist");
               oItem = CreateItemOnObject("gen_im_arm_shd_kit_wdn.uti", gameObject);
               SetTag(oItem, "gen_im_arm_shd_kit_wdn");

               return EngineConstants.TRUE;
          }
     }

     public int Effects_HandleRemoveEffectTest(xEffect eEffect)
     {
          // check to see if the player has the object
          GameObject oItem = GetItemPossessedBy(gameObject, "gen_im_amb_rshield_kite_a");
          if (oItem != null)
          {
               // if true, destroy and return true
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, GetCurrentScriptName(), "Object Exists");
               DestroyObject(oItem, 0);

               return EngineConstants.TRUE;
          }
          else
          {
               // if false, return false
               Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, GetCurrentScriptName(), "Object Does Not Exist");
               return EngineConstants.FALSE;
          }
     }
}