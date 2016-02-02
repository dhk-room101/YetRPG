//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sys_comboeffects_core : MonoBehaviour
{
    Engine engine { get; set; }

     //------------------------------------------------------------------------------
     // SysComboEffects_h
     //------------------------------------------------------------------------------
     /*
         Top Level Event Handler for ComboEffects.

         Invoked via module_core when it receives the appropriate xEvent.

         This is xEvent driven to limit the recursion level of spellscripts.

     */
     //------------------------------------------------------------------------------
     // georg zoeller
     //------------------------------------------------------------------------------


     //#include "xEvents_h"
     //#include "effects_h"
     //#include "aoe_xEffects_h"
     //#include "plt_cod_aow_spellcombo4"

     //moved const string AOE_GREASE_TAG = "aoe_grease";
     //moved const string AOE_FIRE_TAG   = "aoe_fire";

     // -----------------------------------------------------------------------------
     //           ___ _          _
     //          | _ (_)_ _ ___ | |
     //          | _|| | '_/ -_)|_|
     //          |_| |_|_| \___ (_)
     //
     // -----------------------------------------------------------------------------

        void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

     public void _IgniteFlammables(xEvent ev)
     {
          Vector3 lLoc = engine.GetEventVectorRef(ref ev, 0);
          int nType = engine.GetEventTypeRef(ref ev);
          int nAbility = engine.GetEventIntegerRef(ref ev, 0);
          int nShape = engine.GetEventIntegerRef(ref ev, 1);
          GameObject oCreator = engine.GetEventObjectRef(ref ev, 0);
          float fA = engine.GetEventFloatRef(ref ev, 0);
          float fB = engine.GetEventFloatRef(ref ev, 1);
          float fC = engine.GetEventFloatRef(ref ev, 2);


          /* if (oObject != OBJECT_INVALID)
           {
               if (nType == COMBO_EVENT_IGNITE)
               {
                   string sTag = engine.GetTag(oObject);
                   if (StringLowerCase(StringLeft(sTag, engine.GetStringLength(AOE_GREASE_TAG))) == AOE_GREASE_TAG)
                   {
                       xEffect eAoE = EffectAreaOfEffect(204,GetCurrentScriptResource());
                       eAoE = SetEffectEngineInteger(eAoE,2, 1114);

                       Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY,eAoE, engine.GetLocation(oObject), 20.0f,GetArea(oObject), 0);
                       DestroyObject(oObject,10000);
                   }
                   else
                   {
                      // DEBUG_PrintToScreen("FOUND:" + engine.ToString(oAoE[i]),10*i);
                   }
               }

           }
           else  */
          {
               List<GameObject> oAoE = engine.GetObjectsInShape(EngineConstants.OBJECT_TYPE_AREAOFEFFECTOBJECT, nShape, lLoc, fA, fB, fC);
               int nSize = engine.GetArraySize(oAoE);
               int i;
               string sTag;

               engine.DEBUG_PrintToScreen("FOUND:" + engine.ToString(nSize) + " fa " + engine.ToString(fA));

               for (i = 0; i < nSize; i++)
               {

                    if (nAbility == EngineConstants.ABILITY_SPELL_BLIZZARD)
                    {
                         sTag = engine.GetTag(oAoE[i]);
                         if (engine.StringLowerCase(engine.StringLeft(sTag, engine.GetStringLength(EngineConstants.AOE_FIRE_TAG))) == EngineConstants.AOE_FIRE_TAG)
                         {
                              DestroyObject(oAoE[i], 1000);

                              // combo xEffect codex - fire quencher
                              if (engine.IsFollower(oCreator) != EngineConstants.FALSE)
                              {
                                   engine.WR_SetPlotFlag(EngineConstants.PLT_COD_AOW_SPELLCOMBO4, EngineConstants.COD_AOW_SPELLCOMBO_4_FLAME_QUENCHER, EngineConstants.TRUE);
                              }
                         }
                         else
                         {
                              engine.LogTrace(EngineConstants.LOG_CHANNEL_TEMP, engine.ToString(oAoE[i]));
                              engine.DEBUG_PrintToScreen("FOUND:" + engine.ToString(oAoE[i]), 10 * i);
                         }
                    }
                    else if (nType == EngineConstants.COMBO_EVENT_IGNITE)
                    {
                         sTag = engine.GetTag(oAoE[i]);
                         if (engine.StringLowerCase(engine.StringLeft(sTag, engine.GetStringLength(EngineConstants.AOE_GREASE_TAG))) == EngineConstants.AOE_GREASE_TAG)
                         {
                              engine.IgniteGreaseAoe(oAoE[i], oCreator);
                         }
                         else
                         {
                              engine.LogTrace(EngineConstants.LOG_CHANNEL_TEMP, engine.ToString(oAoE[i]));
                              engine.DEBUG_PrintToScreen("FOUND:" + engine.ToString(oAoE[i]), 10 * i);
                         }
                    }


               }
          }

     }
    
     // -----------------------------------------------------------------------------
     // Main Event Handler
     // -----------------------------------------------------------------------------
     public void HandleEvent(xEvent ev)
     {
          //xEvent ev = engine.GetCurrentEvent();


          engine.Log_Events("", ev);

          switch (engine.GetEventTypeRef(ref ev))
          {
               case EngineConstants.EVENT_TYPE_COMBO_IGNITE:
                    {
                         _IgniteFlammables(ev);
                         break;
                    }


               case EngineConstants.EVENT_TYPE_ENTER:
                    {

                         GameObject oCaster = engine.GetEventCreatorRef(ref ev);
                         GameObject oTarget = engine.GetEventTargetRef(ref ev);

                         if (engine.CheckSpellResistance(oTarget, oCaster, EngineConstants.ABILITY_SPELL_INFERNO) == EngineConstants.FALSE)
                         {
                              engine.ApplyEffectDamageOverTime(oTarget, oCaster, EngineConstants.ABILITY_SPELL_INFERNO, 30.0f, 20.0f, EngineConstants.DAMAGE_TYPE_FIRE, 10);
                         }
                         else
                         {
                              engine.UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_RESISTED);
                         }

                         break;
                    }

               case EngineConstants.EVENT_TYPE_EXIT:
                    {
                         GameObject oCaster = engine.GetEventCreatorRef(ref ev);
                         GameObject oTarget = engine.GetEventTargetRef(ref ev);
                         engine.RemoveStackingEffects(oTarget, oCaster, EngineConstants.ABILITY_SPELL_INFERNO);
                         break;
                    }

          }

     }
}