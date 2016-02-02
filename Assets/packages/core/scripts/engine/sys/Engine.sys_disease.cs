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
     //==============================================================================
     // sys_disease
     //------------------------------------------------------------------------------
     /*

         Disease System

         The disease system uses special, permanently applied xEffects to a party
         member to mimic diseases contracted during combat or due to some xEvent.

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: January 16, 2007
     //==============================================================================

     //#include "effect_disease_h"
     //#include "effects_h"
     //#include "log_h"
     //#include "core_h"


     //==============================================================================
     // FUNCTION DEFINITIONS
     //------------------------------------------------------------------------------
     // Disease_HasDisease
     //------------------------------------------------------------------------------
     /** @brief Returns whether or not a character has a Disease
     *
     * Checks if a Character currently has a certain Disease Effect applied to him.
     * Disease Effects are defined in the Disease 2DA (diseases.xls)
     *
     * @param oCharacter the Creature to check
     * @param nDiseaseID the Disease to check for. If -1, it will return EngineConstants.TRUE if ANY disease is present.
     * @return EngineConstants.TRUE if the Creature has the Disease; otherwise EngineConstants.FALSE
     * @author joshua
*/
     public int Disease_HasDisease(GameObject oCharacter, int nDiseaseID)
     {

          int nIndex;
          int nArraySize;
          List<xEffect> arDiseaseEffects;

          //--------------------------------------------------------------------------

          arDiseaseEffects = GetEffects(oCharacter, EngineConstants.EFFECT_TYPE_DISEASE);
          nArraySize = GetArraySize(arDiseaseEffects);

          //--------------------------------------------------------------------------

          if (nDiseaseID == -1)
          {
               return (nArraySize != 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
          }

          if (nDiseaseID > 0)
          {
               for (nIndex = 0; nIndex < nArraySize; nIndex++)
               {
                    xEffect _effect = arDiseaseEffects[nIndex];
                    if (GetEffectIntegerRef(ref _effect, 0) == nDiseaseID)
                         return EngineConstants.TRUE;
               }
          }

          return EngineConstants.FALSE;

     }

     //------------------------------------------------------------------------------
     // Disease_AddDisease
     //------------------------------------------------------------------------------
     /** @brief Adds Disease to a Character
     *
     * Adds a certain Disease Effect to a Charaacter.
     * Disease Effects are defined in the Disease 2DA (diseases.xls)
     *
     * @param oCharacter the Creature to add Disease to
     * @param nDiseaseID the Disease to add.
     * @author joshua
*/
     public void Disease_AddDisease(GameObject oCharacter, int nDiseaseID, int nAbilityId = 0)
     {

          xEffect eDisease;

          //--------------------------------------------------------------------------

          eDisease = EffectDisease(nDiseaseID);

          //--------------------------------------------------------------------------

          if (Disease_HasDisease(oCharacter, nDiseaseID) == EngineConstants.FALSE)
          {
               if (IsEffectValid(eDisease) != EngineConstants.FALSE)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_disease.AddDisease", "Applying Disease " + ToString(nDiseaseID), oCharacter);
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eDisease, oCharacter, 0.0f, oCharacter, nAbilityId);
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_disease.AddDisease", "NOT Applying Disease (invalid xEffect)" + ToString(nDiseaseID), oCharacter);
               }
          }
          else
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_disease.AddDisease", "Not adding Disease, already present #" + ToString(nDiseaseID), oCharacter);
          }

     }

     //------------------------------------------------------------------------------
     // Disease_RemoveDisease
     //------------------------------------------------------------------------------
     /** @brief Removes Disease from a Character
     *
     * Removes a certain Disease from a Character.
     * Disease Effects are defined in the Disease 2DA (diseases.xls)
     *
     * @param oCharacter the Creature to remove Disease from
     * @param nDiseaseID the Disease to remove
     * @author joshua
*/
     public void Disease_RemoveDisease(GameObject oCharacter, int nDiseaseID)
     {

          int nIndex;
          int nArraySize;
          List<xEffect> arDiseaseEffects;

          //--------------------------------------------------------------------------

          arDiseaseEffects = GetEffects(oCharacter, EngineConstants.EFFECT_TYPE_DISEASE);
          nArraySize = GetArraySize(arDiseaseEffects);

          //--------------------------------------------------------------------------

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               xEffect _effect = arDiseaseEffects[nIndex];
               if (GetEffectIntegerRef(ref _effect, 0) == nDiseaseID)
               {
                    RemoveEffect(oCharacter, arDiseaseEffects[nIndex]);
                    Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_disease.RemoveDisease", "Removing Disease #" + ToString(nDiseaseID), oCharacter);
               }
          }

     }

     //------------------------------------------------------------------------------
     // Disease_RemoveAllDiseases
     //------------------------------------------------------------------------------
     /** @brief Removes ALL Diseases from a Character
     *
     * Removes ALL Diseases from a Character .
     * Disease Effects are defined in the Disease 2DA (diseases.xls)
     *
     * @param oCharacter the Creature to remove Diseases from
     * @author joshua
*/
     public void Disease_RemoveAllDiseases(GameObject oCharacter)
     {

          int nIndex;
          int nArraySize;
          List<xEffect> arDiseaseEffects;

          //--------------------------------------------------------------------------

          arDiseaseEffects = GetEffects(oCharacter, EngineConstants.EFFECT_TYPE_DISEASE);
          nArraySize = GetArraySize(arDiseaseEffects);

          //--------------------------------------------------------------------------

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               xEffect _effect = arDiseaseEffects[nIndex];
               Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_disease.Disease_RemoveAllDiseases", "Removing Disease #" + ToString(GetEffectIntegerRef(ref _effect, 0)), oCharacter);
               RemoveEffect(oCharacter, arDiseaseEffects[nIndex]);
          }

     }

     //------------------------------------------------------------------------------
     // Disease_RemoveAllDiseasesFromParty
     //------------------------------------------------------------------------------
     /** @brief Removes ALL Diseases from ALL Party Members
     *
     * Removes ALL Diseases from ALL Party Members.
     * Disease Effects are defined in the Disease 2DA (diseases.xls)
     *
     * @author joshua
*/
     public void Disease_RemoveAllDiseasesFromParty()
     {

          int nIndex;
          int nArraySize;
          List<GameObject> arPartyMembers;

          //--------------------------------------------------------------------------

          arPartyMembers = GetPartyList();
          nArraySize = GetArraySize(arPartyMembers);

          //--------------------------------------------------------------------------

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               Disease_RemoveAllDiseases(arPartyMembers[nIndex]);
          }

     }
}