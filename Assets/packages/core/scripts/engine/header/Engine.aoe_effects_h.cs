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
     //#include"ability_h"
     //#include"plt_cod_aow_spellcombo1"

     //moved public const float GREASE_SLOW_FRACTION = 0.5f;

     //moved public const int AOE_FLAG_DESTRUCTION_PENDING = 1;

     public void ApplyAOEEffect_Grease(GameObject oTarget, GameObject oCreator, int nAbility, GameObject oAoE = null)
     {
          // slow effect
          xEffect eSlow = EffectModifyMovementSpeed(EngineConstants.GREASE_SLOW_FRACTION, EngineConstants.TRUE);
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eSlow, oTarget, 0.0f, oCreator, nAbility);

          // physical resistance
          if (ResistanceCheck(oCreator, oTarget, EngineConstants.PROPERTY_ATTRIBUTE_SPELLPOWER, EngineConstants.RESISTANCE_PHYSICAL) == EngineConstants.FALSE)
          {
               xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_SLIP);
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eEffect, oTarget, 0.0f, oCreator, nAbility);
          }

          if (HasDotEffectOfType(oTarget, EngineConstants.DAMAGE_TYPE_FIRE)!= EngineConstants.FALSE)
          {
               if (oAoE != null)
               {
                    //Change this to actually supplying the AoE effect...
                    SendComboEventAoE(EngineConstants.ABILITY_SPELL_GREASE, EngineConstants.SHAPE_SPHERE, GetLocation(oAoE), oTarget, 10.0f, 0.0f, 0.0f, 1.0f);
               }
               else
               {
                    DEBUG_PrintToScreen("INVALID OBJECT");
               }
          }
     }

     public void IgniteGreaseAoe(GameObject oGrease, GameObject oIgniter)
     {

          if (IsObjectValid(oIgniter)== EngineConstants.FALSE)
          {
               oIgniter = GetArea(gameObject);
          }
          int nFlag = GetAOEFlags(oGrease);

          if ((nFlag & EngineConstants.AOE_FLAG_DESTRUCTION_PENDING) != EngineConstants.AOE_FLAG_DESTRUCTION_PENDING)
          {
               SetAOEFlags(oGrease, (nFlag | EngineConstants.AOE_FLAG_DESTRUCTION_PENDING));

               // create the fire effect
               xEffect eAoE = EffectAreaOfEffect(204, "sys_comboeffects.ncs");
               SetEffectEngineIntegerRef(ref eAoE, 2, 1114); /*vfx*/

               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eAoE, GetLocation(oGrease), 20.0f, oIgniter, 0);
               DestroyObject(oGrease, 3000);

               // combo xEffect codex - grease fire
               if (IsFollower(oIgniter) != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_COD_AOW_SPELLCOMBO1, EngineConstants.COD_AOW_SPELLCOMBO_1_GREASE_FIRE, EngineConstants.TRUE);
               }
          }
     }

     // Will apply an AOE on a creature that makes everything in front of it be knocked back.
     public void ApplyKnockbackAoe(GameObject oTarget)
     {
          xEffect eAoe = EffectAreaOfEffect(21, "monster_aoe_knockback.ncs");
          Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eAoe, oTarget, 0.0f, oTarget, 0);
     }
}