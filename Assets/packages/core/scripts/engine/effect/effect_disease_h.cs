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
     // effect_disease_h
     //------------------------------------------------------------------------------
     /*

         Effect: Disease

             When applied to an object, this xEffect applies a disease to the object.
             This Disease has a sub-xEffect which will also be applied and removed
             with the disease itself.

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: January 16, 2007
     //==============================================================================

     //#include"2da_constants_h"
     //#include"log_h"

     //==============================================================================
     // CONSTANTS
     //==============================================================================

     //moved public const string    EngineConstants.DISEASE_2DA_EFFECT          = "effect";
     //moved public const string    EngineConstants.DISEASE_2DA_INT0            = "effect_int1";
     //moved public const string    EngineConstants.DISEASE_2DA_INT1            = "effect_int2";
     //moved public const string    EngineConstants.DISEASE_2DA_FLOAT0          = "effect_float1";
     //moved public const string    EngineConstants.DISEASE_2DA_FLOAT1          = "effect_float2";

     //==============================================================================
     // FUNCTION DECLARATIONS
     //==============================================================================

     //==============================================================================
     // FUNCTION DEFINITIONS
     //------------------------------------------------------------------------------
     // _CreateDiseaseSubEffect
     //------------------------------------------------------------------------------

     public xEffect _CreateDiseaseSubEffect(xEffect eEffect, int nDiseaseID)
     {

          int nEffect;
          int nInt0;
          int nInt1;
          float fFloat0;
          float fFloat1;
          xEffect eSubEffect;

          //--------------------------------------------------------------------------

          nEffect = GetM2DAInt(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_EFFECT, nDiseaseID);
          nInt0 = GetM2DAInt(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_INT0, nDiseaseID);
          nInt1 = GetM2DAInt(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_INT1, nDiseaseID);
          fFloat0 = GetM2DAFloat(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_FLOAT0, nDiseaseID);
          fFloat1 = GetM2DAFloat(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_FLOAT1, nDiseaseID);
          eSubEffect = Effect(nEffect);

        //--------------------------------------------------------------------------

        SetEffectIntegerRef(ref eSubEffect, 0, nInt0);
        SetEffectIntegerRef(ref eSubEffect, 1, nInt1);
        SetEffectIntegerRef(ref eSubEffect, 2, GetEffectIDRef(ref eEffect));
        SetEffectFloatRef(ref eSubEffect, 0, fFloat0);
        SetEffectFloatRef(ref eSubEffect, 1, fFloat1);

          return eSubEffect;

     }

     //------------------------------------------------------------------------------
     // _DestroyDiseaseSubEffect
     //------------------------------------------------------------------------------

     public int _DestroyDiseaseSubEffect(xEffect eEffect, int nDiseaseID)
     {

          int nIndex;
          int nArraySize;
          int nEffect;
          int nInt0;
          int nInt1;
          float fFloat0;
          float fFloat1;
          GameObject oEffectCreator;
          List<xEffect> arSubEffects;

          //--------------------------------------------------------------------------

          nEffect = GetM2DAInt(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_EFFECT, nDiseaseID);
          nInt0 = GetM2DAInt(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_INT0, nDiseaseID);
          nInt1 = GetM2DAInt(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_INT1, nDiseaseID);
          fFloat0 = GetM2DAFloat(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_FLOAT0, nDiseaseID);
          fFloat1 = GetM2DAFloat(EngineConstants.TABLE_RULES_DISEASES, EngineConstants.DISEASE_2DA_FLOAT1, nDiseaseID);
          oEffectCreator = GetEffectCreatorRef(ref eEffect);
          arSubEffects = GetEffects(oEffectCreator, nEffect);
          nArraySize = GetArraySize(arSubEffects);

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
            xEffect _effect = arSubEffects[nIndex];
               if (GetEffectIntegerRef(ref _effect, 0) == nInt0 &&
                    GetEffectIntegerRef(ref _effect, 1) == nInt1 &&
                    GetEffectFloatRef(ref _effect, 0) == fFloat0 &&
                    GetEffectFloatRef(ref _effect, 1) == fFloat1 &&
                    GetEffectIntegerRef(ref _effect, 2) == GetEffectIDRef(ref eEffect)
                 )
               {
                    RemoveEffect(oEffectCreator, arSubEffects[nIndex]);
                    return EngineConstants.TRUE;
               }
          }

          return EngineConstants.FALSE;

     }

     //------------------------------------------------------------------------------
     // EffectDisease
     //------------------------------------------------------------------------------

     /* @brief Returns an xEffect which diseases an GameObject it is applied to.
*
* Returns a Disease Effect which may be applied to a creature to disease it.
* The Effects of the Diseases are defined in the Disease 2DA (disease.xls)
*
* @param nDisease the ID of the Disease Effect to return.
* @return a valid xEffect of type EngineConstants.EFFECT_TYPE_DISEASE.
* @author joshua
*/
     public xEffect EffectDisease(int nDiseaseID)
     {

          xEffect eDisease;

          //--------------------------------------------------------------------------

          eDisease = Effect(EngineConstants.EFFECT_TYPE_DISEASE);
        SetEffectIntegerRef(ref eDisease, 0, nDiseaseID);

          //--------------------------------------------------------------------------

          return eDisease;

     }

     //------------------------------------------------------------------------------
     // Effects_HandleApplyEffectDisease
     //------------------------------------------------------------------------------

     /* @brief Handles applying a Disease xEffect to a creature
* @param eEffect the Disease xEffect to apply
* @return EngineConstants.TRUE if xEffect was applied successfully; otherwise EngineConstants.FALSE
* @author joshua
*/
     public int Effects_HandleApplyEffectDisease(xEffect eEffect)
     {

          int nIndex;
          int nArraySize;
          int nDiseaseID;
          int nDiseaseCount;
          xEffect eSubEffect;
          List<xEffect> arDiseaseEffects;
          GameObject oEffectCreator;

          //--------------------------------------------------------------------------

          nDiseaseID = GetEffectIntegerRef(ref eEffect, 0);
          nDiseaseCount = 0;
          oEffectCreator = GetEffectCreatorRef(ref eEffect);
          arDiseaseEffects = GetEffects(oEffectCreator, EngineConstants.EFFECT_TYPE_DISEASE);
          nArraySize = GetArraySize(arDiseaseEffects);
          eSubEffect = _CreateDiseaseSubEffect(eEffect, nDiseaseID);

          //--------------------------------------------------------------------------

          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_disease_h.Effects_HandleApplyEffectDisease", "");

          // Check if xEffect already exists
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
            xEffect _effect = arDiseaseEffects[nIndex];
               if (GetEffectIntegerRef(ref _effect, 0) == nDiseaseID)
                    nDiseaseCount++;
          }

          // We check if there are 2 or more because this handler is called AFTER
          // the xEffect is applied. Returning EngineConstants.FALSE will remove the one that was
          // added.
          if (nDiseaseCount > 1)
               return EngineConstants.FALSE;

          // Apply sub-xEffect from 2DA
          Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eSubEffect, oEffectCreator,
                                      0.0f, oEffectCreator);

          return EngineConstants.TRUE;

     }

     //------------------------------------------------------------------------------
     // Effects_HandleRemoveEffectDisease
     //------------------------------------------------------------------------------

     /* @brief Handles removing a Disease xEffect to a creature
* @param eEffect the Disease xEffect to remove
* @return EngineConstants.TRUE if xEffect was removed successfully; otherwise EngineConstants.FALSE
* @author joshua
*/
     public int Effects_HandleRemoveEffectDisease(xEffect eEffect)
     {

          int nDiseaseID;

          //--------------------------------------------------------------------------

          nDiseaseID = GetEffectIntegerRef(ref eEffect, 0);

          //--------------------------------------------------------------------------

          Log_Trace(EngineConstants.LOG_CHANNEL_EFFECTS, "effect_disease_h.Effects_HandleRemoveEffectDisease", "");

          // Remove any possible sub-effects of the disease
          if (_DestroyDiseaseSubEffect(eEffect, nDiseaseID) == EngineConstants.FALSE)
               return EngineConstants.FALSE;

          return EngineConstants.TRUE;

     }
}