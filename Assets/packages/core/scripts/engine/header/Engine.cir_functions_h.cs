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
     //==============================================================================
     /*

         Broken Circle
          -> Generic Functions Script

         These are the generic functions for the Paragon plot.

     */
     //------------------------------------------------------------------------------
     // Created By: Ferret Baudoin
     // Created On: December 13, 2006
     //==============================================================================

     //#include"plt_cir000pt_encounters"
     //#include"plt_cir300pt_shapeshifting"
     //#include"plt_cir330pt_mage_asunder"
     //#include"plt_cir300pt_fade_portal"
     //#include"plt_cir000pt_main"

     //#include"cir_constants_h"
     //#include"cir_mapgui_h"
     //#include"ran_constants_h"

     //#include"log_h"
     //#include"utility_h"
     //#include"wrappers_h"
     //#include"plot_h"

     //public void main() {}

     //==============================================================================
     // FUNCTION IMPLEMENTATION

     public void CIR_SlothSummonNPC(string sNPC)
     {
          GameObject oPC = GetHero();
          GameObject oFollower = Party_GetFollowerByTag(sNPC);

          WR_SetObjectActive(oFollower, EngineConstants.TRUE);
          WR_SetFollowerState(UT_GetNearestCreatureByTag(oPC, sNPC), EngineConstants.FOLLOWER_STATE_ACTIVE);
          SetPosition(oFollower, GetPosition(GetObjectByTag(EngineConstants.CIR_WP_SLOTH_NPC)), EngineConstants.TRUE);
     }

     //Swaps one team with another, will only work on equal size teams.
     public void CIR_SwapTeams(int nTeamOut, int nTeamIn)
     {
          if (nTeamOut == nTeamIn)
          {
               Log_Trace_Scripting_Error("cir_functions_h.CIR_SwapTeams", "Passed the same team to swap out as in");
               return;
          }

          List<GameObject> arTeamOut = UT_GetTeam(nTeamOut);
          List<GameObject> arTeamIn = UT_GetTeam(nTeamIn);

          int iTeamOutSize = GetArraySize(arTeamOut);
          int iTeamInSize = GetArraySize(arTeamIn);

          if (iTeamOutSize != iTeamInSize)
          {
               Log_Trace_Scripting_Error("cir_functions_h.CIR_SwapTeams", "Teams are of different sizes");
               return;
          }

          int iCurrent; //The current team member we are working with

          for (iCurrent = 0; iCurrent < iTeamOutSize; iCurrent++)
          {   //Swap the creatures
               CIR_SwapCreatures(arTeamOut[iCurrent], arTeamIn[iCurrent]);
          }
     }

     /*Checks the state of post plot and sets it as nessacary.
 */
     public void CIR_CheckAndSetPostPlot()
     {
          if (WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.BROKEN_CIRCLE_PLOT_DONE_TOWER_SAVED) != EngineConstants.FALSE
               && WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.POST_PLOT) == EngineConstants.FALSE)
          {
               WR_SetPlotFlag(EngineConstants.PLT_CIR000PT_MAIN, EngineConstants.POST_PLOT, EngineConstants.TRUE, EngineConstants.TRUE);
          }
     }

     //------------------------------------------------------------------------------
     // public void CIR_SwapCreatures
     //------------------------------------------------------------------------------
     //Swaps one creature with another, set the first creature inactive and the second active.
     public void CIR_SwapCreatures(GameObject oSwapOut, GameObject oSwapIn)
     {
          SetGroupId(oSwapOut, EngineConstants.GROUP_NEUTRAL);
          //Set the home of the swap in to the same as the swap out location
          Rubber_SetHome(oSwapIn, oSwapOut);
          Vector3 lHome = GetLocation(oSwapOut);
          //Jump the new form to the current form location
          WR_AddCommand(oSwapIn, CommandJumpToLocation(lHome), EngineConstants.TRUE, EngineConstants.TRUE);
          WR_SetObjectActive(oSwapIn, EngineConstants.TRUE);
          //, EngineConstants.COMBAT_ANIMATION_ENTER_BERSERK, SHAPESHIFT_TRANSFORM_EFFECT);
          SetLocalInt(oSwapIn, EngineConstants.RUBBER_HOME_ENABLED, EngineConstants.TRUE);

          lHome = Rubber_GetHome(oSwapOut);
          WR_AddCommand(oSwapOut, CommandJumpToLocation(lHome), EngineConstants.TRUE, EngineConstants.TRUE);
          //Set the current form to inactive
          WR_SetObjectActive(oSwapOut, EngineConstants.FALSE);
     }

     //------------------------------------------------------------------------------
     // CIR_MarkFollowerAreaComplete
     //------------------------------------------------------------------------------
     /*Work out which of the sleep spots the follower is in and mark it complete
      *  @param nFollower The follower that we are trying to find
      */
     public void CIR_MarkFollowerAreaComplete(int nFollower)
     {
          //Mark the area that the follower is in as complete
          if (GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_1) == nFollower)
          {
               WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_GREEN_1_COMPLETE, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_2) == nFollower)
          {
               WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_GREEN_2_COMPLETE, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else if (GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_3) == nFollower)
          {
               WR_SetPlotFlag(EngineConstants.PLT_CIR300PT_FADE_PORTAL, EngineConstants.FADE_PORTAL_GREEN_3_COMPLETE, EngineConstants.TRUE, EngineConstants.TRUE);
          }
          else
          {
               Log_Trace_Scripting_Error("cir_functions_h.CIR_MarkFollowerAreaComplete", "Got to a follower flag that doesn't exist so either a follower that wasn't listed as entering with the player is in an area or something weird has happened");
          }
     }

     //------------------------------------------------------------------------------
     // CIR_SetInFade
     //------------------------------------------------------------------------------
     //Sets up which follower is in which of the 3 "sleep" spots in the fade
     public void CIR_SetInFade(int nFollower)
     {
          if (GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_1) < 1)
          {
               SetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_1, nFollower);
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_A), EngineConstants.WM_LOCATION_GRAYED_OUT);
          }
          else if (GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_2) < 1)
          {
               SetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_2, nFollower);
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_B), EngineConstants.WM_LOCATION_GRAYED_OUT);
          }
          else if (GetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_3) < 1)
          {
               SetLocalInt(GetModule(), EngineConstants.CIR_FADE_FOLLOWER_3, nFollower);
               WR_SetWorldMapLocationStatus(GetObjectByTag(EngineConstants.WML_FAD_COMPANION_C), EngineConstants.WM_LOCATION_GRAYED_OUT);
          }
     }

     //------------------------------------------------------------------------------
     // CIR_JumpToFadeFollower
     //------------------------------------------------------------------------------

     public void CIR_JumpToFadeFollower(int nFollower)
     {
          switch (nFollower)
          {
               case EngineConstants.CIR_FOLLOWER_ALISTAIR:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_ALISTAIR, EngineConstants.CIR_WP_FADE_ALISTAIR);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_DOG:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_DOG, EngineConstants.CIR_WP_FADE_DOG);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_LELIANA:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_LELIANA, EngineConstants.CIR_WP_FADE_LELIANA);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_LOGHAIN:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_LOGHAIN, EngineConstants.CIR_WP_FADE_LOGHAIN);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_MORRIGAN:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_MORRIGAN, EngineConstants.CIR_WP_FADE_MORRIGAN);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_OGHREN:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_OGHREN, EngineConstants.CIR_WP_FADE_OGHREN);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_SHALE:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_SHALE, EngineConstants.CIR_WP_FADE_SHALE);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_STEN:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_STEN, EngineConstants.CIR_WP_FADE_STEN);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_WYNNE:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_WYNNE, EngineConstants.CIR_WP_FADE_WYNNE);
                         break;
                    }
               case EngineConstants.CIR_FOLLOWER_ZEVRAN:
                    {
                         UT_PCJumpOrAreaTransition(EngineConstants.CIR_AR_ZEVRAN, EngineConstants.CIR_WP_FADE_ZEVRAN);
                         break;
                    }
          }
     }

     //------------------------------------------------------------------------------
     // CIR_ItemAcquired
     //------------------------------------------------------------------------------

     public void CIR_ItemAcquired(string sItemTag)
     {
          //A public void useless function!

     }

     //------------------------------------------------------------------------------
     // CIR_ItemActivated
     //------------------------------------------------------------------------------

     public void CIR_ItemActivated(GameObject oItem, GameObject oCaster, GameObject oTarget, int nAbility)
     {
          string sTag = GetTag(oItem);

          if (sTag == EngineConstants.CIR_IM_LITANY)
          {
               if (WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_ENCOUNTERS, EngineConstants.ULDRED_DRAINS_MAGE) != EngineConstants.FALSE)
               {
                    WR_SetPlotFlag(EngineConstants.PLT_CIR000PT_ENCOUNTERS, EngineConstants.ULDRED_BREAK_MAGE_DRAIN, EngineConstants.TRUE, EngineConstants.TRUE);
               }
               else
               {
               }
          }
     }

     //------------------------------------------------------------------------------
     // SpiritPortals
     //------------------------------------------------------------------------------
     /* Get the spirit portals and show or hide them
      * @param bSpiritForm - If the player is in spirit form or not (show on EngineConstants.TRUE)
      */
     public void SpiritPortals(int bSpiritForm)
     {

          UT_TeamAppears(EngineConstants.GROUP_FADE_SPIRIT_OBJECTS, bSpiritForm, EngineConstants.OBJECT_TYPE_PLACEABLE);

          if (WR_GetPlotFlag(EngineConstants.PLT_CIR000PT_ENCOUNTERS, EngineConstants.SHADE_BOSS_GOES_HOSTILE) != EngineConstants.FALSE)
          {
               GameObject oShade = UT_GetNearestObjectByTag(GetHero(), EngineConstants.CIR_CR_SHADE_BOSS); //Get the shade boss.
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "cir_functions_h", "Got creature hostile part " + GetTag(oShade) + " setting xEffect to " + IntToString(bSpiritForm));
               xEffect eInvis = Effect(EngineConstants.EFFECT_TYPE_STEALTH);
               if (bSpiritForm != EngineConstants.FALSE)
               {
                    RemoveEffectsByCreator(oShade, EngineConstants.CIR_FADE_SHADE_INVISIBLITY);
               }
               else
               {
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eInvis, oShade, 0.0f, oShade, EngineConstants.CIR_FADE_SHADE_INVISIBLITY);
               }
          }
     }

     //Toggle visual xEffect on a team.
     // int nTeam The team to set
     // int bToggle Whether to turn the xEffect on or off
     // int nVFXId The id of the visual xEffect to apply or remove
     // int nMembersType The type of objects
     public void _ToggleVisualEffectOnTeam(int nTeam, int bToggle, int nVFXId, int nMembersType)
     {
          List<GameObject> arTeam = UT_GetTeam(nTeam, nMembersType);
          int nIndex;
          int nTeamSize = GetArraySize(arTeam);
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               if (bToggle != EngineConstants.FALSE)
               {
                    ApplyEffectVisualEffect(arTeam[nIndex], arTeam[nIndex], nVFXId, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, 0.0f);
               }
               else //bToggle is false
               {
                    RemoveVisualEffect(arTeam[nIndex], nVFXId);
               }
          }
     }

     //------------------------------------------------------------------------------
     // CIR_MouseHoles
     //------------------------------------------------------------------------------
     //Set up mouse holes and any mouse form behaviour
     // bMouseForm - If the mouse form is on or not
     public void CIR_MouseHoles(int bMouseForm)
     {

          int nIndex;
          int nArraySize;
          GameObject oPC;

          //--------------------------------------------------------------------------

          oPC = GetHero();

          //--------------------------------------------------------------------------

     }

     //------------------------------------------------------------------------------
     // CIR_SetItemPlotLike
     //------------------------------------------------------------------------------
     //Sets an item Indestrubtable and Undropable if EngineConstants.TRUE.
     public void CIR_SetItemPlotLike(GameObject oItem, int bPlotLike)
     {
          SetItemIndestructible(oItem, bPlotLike);
          SetItemDroppable(oItem, bPlotLike == EngineConstants.FALSE ? EngineConstants.TRUE : EngineConstants.FALSE);
     }

     //------------------------------------------------------------------------------
     // CIR_PlayFollowerDisappearingEffect
     //------------------------------------------------------------------------------
     //Play an xEffect when a follower is teleported away
     // oObject - The GameObject the xEffect is for.
     public void CIR_PlayFollowerDisappearingEffect(GameObject oObject)
     {
          //Play the teleport xEffect we use for the PC in the fade.
          ApplyEffectVisualEffect(oObject, oObject, EngineConstants.FADE_VFX_TELEPORT, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
     }

     //------------------------------------------------------------------------------
     // CIR_HandleRewardFeedback
     //------------------------------------------------------------------------------
     /* Handle effects and such on reward items in the fade. Annoying it does a bit more including setting the team to -1.
      *      @param oObject The placeable that gives you the reward
      */
     public void CIR_HandleRewardFeedback(GameObject oReward, GameObject oPC)
     {
          //Float a message over the PC at the item
          UI_DisplayPopupText(oReward, oPC);

          //Turn off effect
          RemoveVisualEffect(oReward, EngineConstants.CIR_REWARD_CRUST_VFX);
          RemoveVisualEffect(oReward, EngineConstants.CIR_REWARD_CRUST_VFX_2);
          //Set to non-interactive
          SetObjectInteractive(oReward, EngineConstants.FALSE);
          //Apply VFX to character
          ApplyEffectVisualEffect(oReward, oPC, EngineConstants.CIR_REWARD_CRUST_VFX, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, 2.5f);

          //VFX 10 98
          ApplyEffectVisualEffect(oReward, oReward, 1098, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, 7.0f);
          //Remove GameObject from spirit GameObject type
          if (GetTeamId(oReward) == EngineConstants.GROUP_FADE_SPIRIT_OBJECTS)
          {
               WR_SetObjectActive(UT_GetNearestObjectByTag(oReward, EngineConstants.CIR_IP_FADE_SPIRIT_BASE), EngineConstants.FALSE);
               SetTeamId(oReward, -1);
          }

          //Fade the item out
          xEffect eTransparent = Effect(EngineConstants.EFFECT_TYPE_ALPHA);
          SetEffectEngineFloatRef(ref eTransparent, EngineConstants.EFFECT_FLOAT_POTENCY, 0.5f);
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eTransparent, oReward, 0.0f, oReward, 0);
     }

     // enable or disable Uldred's Immunity shield
     /* Apply a shield xEffect on uldred and handle his AI events and such
      *  @param nTF If the shield is being activated or disabled. I don't know why it is called this either.
      */
     public void UldredShield(int nTF)
     {
          GameObject oPC = GetHero();
          GameObject oUldred = UT_GetNearestObjectByTag(oPC, EngineConstants.CIR_CR_ULDRED);

          //Uldred is plot while sheilded
          SetPlot(oUldred, nTF);
          //Stop AI events if shielded
          SetLocalInt(oUldred, EngineConstants.AI_CUSTOM_AI_ACTIVE, nTF);

          //Give him a pretty VFX
          if (nTF != EngineConstants.FALSE)
          {
               ApplyEffectVisualEffect(oUldred, oUldred, EngineConstants.CIR_ULDRED_SHIELD_EFFECT, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, 0.0f);
          }
          //
          else
          {
               RemoveVisualEffect(oUldred, EngineConstants.CIR_ULDRED_SHIELD_EFFECT);
          }
     }

     /*Disable all hostile creatures in the area
      *  @param oArea The area to disable them in
      */
     public void _DisableAllHostileCreatures(GameObject oArea)
     {
          List<GameObject> arObjects = GetObjectsInArea(oArea);
          int iNumOfObjects = GetArraySize(arObjects);
          int i;
          GameObject oCurrent;

          for (i = 0; i < iNumOfObjects; i++)
          {
               oCurrent = arObjects[i]; //Set the current
                                        //If the GameObject is a creature, is Active, isn't dead or dying and is hostile
               if (GetObjectType(oCurrent) == EngineConstants.OBJECT_TYPE_CREATURE && GetObjectActive(oCurrent) != EngineConstants.FALSE
                  && IsDeadOrDying(oCurrent) == EngineConstants.FALSE && GetGroupId(oCurrent) == EngineConstants.GROUP_HOSTILE)
               {
                    WR_SetObjectActive(oCurrent, EngineConstants.FALSE); //Deactivate the creature
               }
          }
     }
}