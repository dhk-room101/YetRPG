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
     //#include"log_h"
     //#include"utility_h"
     //#include"wrappers_h"
     //#include"plot_h"

     //#include"bhm_constants_h"
     //#include"plt_bhm600pt_harrowing"

     //#include"plt_tut_combat_salve"

     //moved public const int RAT_APPEARANCE = 37;
     //moved public const int SHAPESHIFT_TRANSFORM_EFFECT = 1134;

     public void BHM_ItemAcquired(string sItemTag)
     {

          if (sItemTag == EngineConstants.BHM_IM_FADE_HEALING_SALVE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_TUT_COMBAT_SALVE, EngineConstants.TUT_COMBAT_SALVE_1, EngineConstants.TRUE);
          }

     }

     /*A team appears that allows VFX to be applied.
      *  @param nTeamID - The team to appear.
      *  @param nVFX - The VFX to play
      */
     public void _TeamAppearsWithVFX(int nTeamID, int nVFX)
     {
          int nIndex;
          GameObject oPC = GetPartyLeader();
          List<GameObject> arTeam = UT_GetTeam(nTeamID, EngineConstants.OBJECT_TYPE_CREATURE);

          for (nIndex = 0; nIndex < GetArraySize(arTeam); nIndex++)
          {
               WR_SetObjectActive(arTeam[nIndex], EngineConstants.TRUE, 1, nVFX);
          }

     }

     /*For the harrowing. Transforms mouse (by firing a custom event)
 *  @param bMouseForm - Whether to become a mouse or not
 */
     public void TransformMouse(int bMouseForm)
     {
          GameObject oMouse;
          GameObject oMouseHuman = GetObjectByTag(EngineConstants.BHM_CR_MOUSE_HUMAN);
          GameObject oTarg;
          int bBear = WR_GetPlotFlag(EngineConstants.PLT_BHM600PT_HARROWING, EngineConstants.SLOTH_TAUGHT_BEAR_SHAPECHANGE); //Mouse is a bear

          if (bBear == EngineConstants.FALSE)
          {
               oMouse = GetObjectByTag(EngineConstants.BHM_CR_MOUSE);
          }
          else
          {
               oMouse = GetObjectByTag(EngineConstants.BHM_CR_MOUSE_BEAR);
          }

          if (bMouseForm != EngineConstants.FALSE)
          {   //Transform into mouse
              //Rubber_SetHome(oMouse, oMouseHuman);
              //         Rubber_JumpHome(oMouse);
               WR_SetObjectActive(oMouse, EngineConstants.TRUE);
               WR_SetObjectActive(oMouseHuman, EngineConstants.FALSE);
               oTarg = oMouse; //Mouse is the target for any effects
          }
          else
          {   //Transform back
              //Transform into human
              //Rubber_SetHome(oMouseHuman, oMouse);
              //Rubber_JumpHome(oMouseHuman);
               WR_SetObjectActive(oMouse, EngineConstants.FALSE);
               WR_SetObjectActive(oMouseHuman, EngineConstants.TRUE);
               oTarg = oMouseHuman;
          }
          //Transform VFX
          ApplyEffectVisualEffect(oTarg, oTarg, EngineConstants.SHAPESHIFT_TRANSFORM_EFFECT, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, 4.0f);
     }
}