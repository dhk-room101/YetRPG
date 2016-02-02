//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Engine
{
     // -----------------------------------------------------------------------------
     // sys_injury
     // -----------------------------------------------------------------------------
     /*
         Injury System

         The injury system uses special, permanently applied xEffects to a party member
         to mimic last damage sustained when dropping in combat.

         Note about injury xEffects:

         -   these are normal xEffects, however they are applied with an id of
             50000 (EngineConstants.INJURY_SPELL_ID) + injury index


     */
     // -----------------------------------------------------------------------------
     // owner: georg zoeller
     // -----------------------------------------------------------------------------

     //#include "log_h"
     //#include "core_h"
     //#include "wrappers_h"
     //#include "2da_constants_h"

     //#include "plt_tut_combat_injury"
     //#include "plt_tut_combat_injury_lots"

     //#include "stats_core_h"

     //moved const int TUTORIAL_LOTS_OF_INJURIES_THRESHOLD = 5;
     public xEffect _CreateInjuryEffect(int nInjuryIndex)
     {

          // -------------------------------------------------------------------------


          int nEffect = GetM2DAInt(EngineConstants.TABLE_RULES_INJURIES, "effect", nInjuryIndex);
          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "injury.CreateInjuryEffect", "Effect for index " + ToString(nInjuryIndex) + " = " + ToString(nEffect));
          int nIntParam1 = GetM2DAInt(EngineConstants.TABLE_RULES_INJURIES, "effect_int1", nInjuryIndex);
          int nIntParam2 = GetM2DAInt(EngineConstants.TABLE_RULES_INJURIES, "effect_int2", nInjuryIndex);


          float fFloatParam1 = GetM2DAFloat(EngineConstants.TABLE_RULES_INJURIES, "effect_float1", nInjuryIndex);
          float fFloatParam2 = GetM2DAFloat(EngineConstants.TABLE_RULES_INJURIES, "effect_float2", nInjuryIndex);


          xEffect e = new xEffect(EngineConstants.EFFECT_TYPE_INVALID);
          if (nEffect > 0)
          {
               e = Effect(nEffect);

               SetEffectIntegerRef(ref e, 0, nIntParam1);
               SetEffectIntegerRef(ref e, 1, nIntParam2);

               SetEffectFloatRef(ref e, 0, fFloatParam1);
               SetEffectFloatRef(ref e, 1, fFloatParam2);

          }

          return e;
     }


     public void _ApplyInjuryEffect(GameObject oCharacter, xEffect eEffect, int nInjury)
     {

          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eEffect, oCharacter, 0.0f, oCharacter, EngineConstants.INJURY_ABILITY_EFFECT_ID + nInjury);
          // Handle injury stats
          STATS_HandleInjuries(oCharacter);
     }

     public int Injury_HasInjury(GameObject oCharacter, int nInjury)
     {
          return GetHasEffects(oCharacter, EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.INJURY_ABILITY_EFFECT_ID + nInjury);

     }

     // -----------------------------------------------------------------------------
     // Add a single injury
     // -----------------------------------------------------------------------------
     public void Injury_AddInjury(GameObject oCharacter, int nInjury)
     {

          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_injury.AddInjury", "Adding Injury " + ToString(nInjury) + " for " + ToString(oCharacter));

          // is this area a tutorial one?
          int bTutorial = GetLocalInt(GetArea(oCharacter), "INJURY_FLAG");
          if (bTutorial == EngineConstants.FALSE)
          {
               // is the injury not already present?
               if (Injury_HasInjury(oCharacter, nInjury) == EngineConstants.FALSE)
               {
                    xEffect e = _CreateInjuryEffect(nInjury);
                    if (IsEffectValid(e) != EngineConstants.FALSE)
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_injury.AddInjury", "Applying Injury " + ToString(nInjury), oCharacter);
                         _ApplyInjuryEffect(oCharacter, e, nInjury);

                         if (IsPartyMember(oCharacter) != EngineConstants.FALSE)
                         {
                              if (WR_GetPlotFlag(EngineConstants.PLT_TUT_COMBAT_INJURY, EngineConstants.TUT_COMBAT_INJURY_1) == EngineConstants.FALSE)
                                   WR_SetPlotFlag(EngineConstants.PLT_TUT_COMBAT_INJURY, EngineConstants.TUT_COMBAT_INJURY_1, EngineConstants.TRUE);
                              if (WR_GetPlotFlag(EngineConstants.PLT_TUT_COMBAT_INJURY_LOTS, EngineConstants.TUT_COMBAT_INJURY_LOTS_1) == EngineConstants.FALSE)
                              {
                                   if (Injury_GetInjuriesNum(oCharacter) >= EngineConstants.TUTORIAL_LOTS_OF_INJURIES_THRESHOLD)
                                        WR_SetPlotFlag(EngineConstants.PLT_TUT_COMBAT_INJURY_LOTS, EngineConstants.TUT_COMBAT_INJURY_LOTS_1, EngineConstants.TRUE);
                              }
                         }
                    }
                    else
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_injury.AddInjury", "Not adding injury, xEffect is INVALID!!");
                    }
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_injury.AddInjury", "Not adding injury, already present #" + ToString(nInjury));
               }
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_injury.AddInjury", "Not adding injury - Tutorial level.");
          }
     }


     // -----------------------------------------------------------------------------
     //  Remove a single injury
     // -----------------------------------------------------------------------------
     public void Injury_RemoveInjury(GameObject oCharacter, int nInjury)
     {
          List<xEffect> eEffects = GetEffects(oCharacter, EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.INJURY_ABILITY_EFFECT_ID + nInjury);

          int nCount = GetArraySize(eEffects);
          int i;
          for (i = 0; i < nCount; i++)
          {
               RemoveEffect(oCharacter, eEffects[i]);
          }

          if (nCount != EngineConstants.FALSE)
          {
               ApplyEffectVisualEffect(oCharacter, oCharacter, 90092 /* purify vfx */, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
          }

     }


     public void Injury_DetermineInjury(GameObject oCharacter)
     {

          int nMax = GetM2DARows(EngineConstants.TABLE_RULES_INJURIES);
          int nStart = (Engine_Random(nMax) + 1);
          int nLoopEnd = nStart;

          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_injury.DetermineInjury", "Determining Injuries for " + ToString(oCharacter));


          // Only add injuries flagged valid for auto-apply and randomize the entry point
          // This causes a lower probabilty of an injury being found and applied the more injuries a
          // character has, which is fine.      
          int nTableIndex = GetM2DARowIdFromRowIndex(EngineConstants.TABLE_RULES_INJURIES, nStart); // table index used for engine 2da references only
          while (GetM2DAInt(EngineConstants.TABLE_RULES_INJURIES, "auto_apply", nTableIndex) == EngineConstants.FALSE || Injury_HasInjury(oCharacter, nStart) != EngineConstants.FALSE)
          {
               nStart = (nStart % (nMax + 1) + 1);
               nTableIndex = GetM2DARowIdFromRowIndex(EngineConstants.TABLE_RULES_INJURIES, nStart);
               // We definately never want to get stuck in a loop, in the case where
               // all valid injuries are applied, this will exit us from the loop.
               if (nStart == nLoopEnd)
                    break;
          }

          Injury_AddInjury(oCharacter, nStart);
     }


     public void Injury_RemoveAllInjuries(GameObject oCharacter)
     {

          List<xEffect> eEffects = GetEffects(oCharacter);
          int nCount = GetArraySize(eEffects);
          int nInjuryID;

          int i;

          for (i = 0; i < nCount; i++)
          {
               if (IsInjuryEffect(eEffects[i]) != EngineConstants.FALSE)
               {
                    // Only remove injuries flagged valid for auto-remove
                    nInjuryID = Injury_GetInjuryIdFromEffect(eEffects[i]);
                    if (GetM2DAInt(EngineConstants.TABLE_RULES_INJURIES, "auto_remove", nInjuryID) != EngineConstants.FALSE)
                         RemoveEffect(oCharacter, eEffects[i]);
               }
          }

     }

     public void Injury_RemoveAllInjuriesFromParty()
     {
          List<GameObject> partyMembers = GetPartyPoolList();
          int nMemberCount = GetArraySize(partyMembers);
          int i;

          for (i = 0; i < nMemberCount; i++)
          {
               Injury_RemoveAllInjuries(partyMembers[i]);
          }

     }

     public List<xEffect> Injury_GetInjuryEffects(GameObject oCharacter)
     {
          List<xEffect> xEffects = GetEffects(oCharacter);
          int nCount = GetArraySize(xEffects);
          int i;
          int nTargetIdx = 0;
          List<xEffect> returnList = new List<xEffect>();

          xEffect eTmp;

          for (i = 0; i < nCount; i++)
          {
               eTmp = xEffects[i];

               if (IsInjuryEffect(eTmp) != EngineConstants.FALSE)
               {
                    returnList[nTargetIdx++] = eTmp;
               }
          }

          return returnList;

     }

     public int Injury_GetInjuryIdFromEffect(xEffect eInjury)
     {
          if (IsInjuryEffect(eInjury) != EngineConstants.FALSE)
          {
               return GetEffectAbilityIDRef(ref eInjury) - EngineConstants.INJURY_ABILITY_EFFECT_ID;
          }
          else
          {
               return 0;
          }
     }

     public int Injury_GetInjuriesNum(GameObject oCharacter)
     {
          List<xEffect> arInjuries = Injury_GetInjuryEffects(oCharacter);
          return GetArraySize(arInjuries);
     }
}