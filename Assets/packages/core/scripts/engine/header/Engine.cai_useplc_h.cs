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
     // cai_useplc_h.nss
     // -----------------------------------------------------------------------------
     /*

         Custom AI
          -> Use Placeable

     */
     //------------------------------------------------------------------------------
     // Created By: joshua
     // Created On: May 7, 2008
     //==============================================================================

     //#include"wrappers_h"
     //#include"utility_h"

     //#include"ai_main_h_2"

     //==============================================================================
     // CONSTANTS
     //==============================================================================

     // Change refernce in cai_h if you change this
     //moved public const int       CAI_USEPLACEABLE_USEACTION          = 10100;

     //moved public const float     CAI_USE_PLACEABLE_MIN_USE_DISTANCE  = 3.0f;
     //moved public const float     CAI_USE_PLACEABLE_MAX_FIND_DISTANCE = 30.0f;

     //==============================================================================
     // FUNCTION DECLARATION
     //==============================================================================

     //==============================================================================
     // FUNCTION IMPLEMENATION
     //------------------------------------------------------------------------------
     // CAI_UsePlaceable
     //------------------------------------------------------------------------------

     public void CAI_UsePlaceable(GameObject oCreature, GameObject oPlaceable)
     {

          if (IsObjectValid(oPlaceable) != EngineConstants.FALSE)
          {
               CAI_SetCustomAIObject(oCreature, oPlaceable);
               CAI_SetCustomAI(oCreature, EngineConstants.CAI_USEPLACEABLE_USEACTION);
               CAI_UsePlaceable_Use(oCreature);
          }
          else
          {
               CAI_SetCustomAI(oCreature, EngineConstants.CAI_INACTIVE);
               WR_AddCommand(oCreature, CommandWait(0.01f));
          }

     }

     public void CAI_UsePlaceable_Use(GameObject oCreature)
     {

          GameObject oPlaceable = CAI_GetCustomAIObject(oCreature);

          // Exit out of Custom AI if the placeable is invalid
          if (IsObjectValid(oPlaceable) == EngineConstants.FALSE 
               || GetObjectInteractive(oPlaceable) == EngineConstants.FALSE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_AI, GetCurrentScriptName() + ".CAI_UsePlaceable_Use()", "Error: Invalid Placeable to move to.", oCreature);
               CAI_UsePlaceable_NearestByTag(oCreature, GetTag(oPlaceable));
               return;
          }

          WR_AddCommand(oCreature, CommandUseObject(oPlaceable, EngineConstants.PLACEABLE_ACTION_USE));
          CAI_SetCustomAI(oCreature, EngineConstants.CAI_INACTIVE);

     }

     //------------------------------------------------------------------------------
     // CAI_UsePlaceable_NearestByTag
     //------------------------------------------------------------------------------

     public void CAI_UsePlaceable_NearestByTag(GameObject oCreature, string sPlaceableTag)
     {

          int nIndex;
          int nArraySize;
          float fTempDistance;
          float fNearestDistance;
          GameObject oNearestPlaceable=null;
          List<GameObject> arPlaceables;

          //----------------------------------------------------------

          fNearestDistance = 9999.0f;
          arPlaceables = UT_GetAllObjectsInAreaByTag(sPlaceableTag, EngineConstants.OBJECT_TYPE_PLACEABLE);
          nArraySize = GetArraySize(arPlaceables);

          //----------------------------------------------------------

          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               if (GetObjectInteractive(arPlaceables[nIndex]) != EngineConstants.FALSE)
               {
                    fTempDistance = GetDistanceBetween(arPlaceables[nIndex], oCreature);
                    if (fTempDistance < EngineConstants.CAI_USE_PLACEABLE_MAX_FIND_DISTANCE && fTempDistance < fNearestDistance)
                    {
                         oNearestPlaceable = arPlaceables[nIndex];
                         fNearestDistance = fTempDistance;
                    }
               }
          }

          CAI_UsePlaceable(oCreature, oNearestPlaceable);

     }

     //------------------------------------------------------------------------------
     // CAI_UsePlaceable_NearestByTag
     //------------------------------------------------------------------------------

     public void CAI_UsePlaceable_RandomByTag(GameObject oCreature, string sPlaceableTag)
     {

          int nIndex;
          int nArraySize;
          int nRandom;
          GameObject oRandomPlaceable=null;
          List<GameObject> arPlaceables;

          //----------------------------------------------------------

          arPlaceables = UT_GetAllObjectsInAreaByTag(sPlaceableTag, EngineConstants.OBJECT_TYPE_PLACEABLE);
          nArraySize = GetArraySize(arPlaceables);

          //----------------------------------------------------------

          nRandom = Engine_Random(nArraySize);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               if (GetObjectInteractive(arPlaceables[nRandom]) != EngineConstants.FALSE)
               {
                    oRandomPlaceable = arPlaceables[nRandom];
                    break;
               }
               nRandom = (nRandom + 1) % nArraySize;
          }

          CAI_UsePlaceable(oCreature, oRandomPlaceable);

     }
}