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

         atmosphere_h

         Atmosphere
          -> Fade

         This is a self contained xEvent that will be sent to the Area Script.

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: September 21, 2007
     //==============================================================================

     //#include"utility_h"
     //#include"events_h"  
     //#include"2da_constants_h"

     //------------------------------------------------------------------------------
     // Constants
     //------------------------------------------------------------------------------

     //moved public const int   EVENT_ATM_FADE                   = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;

     //moved public const int   EngineConstants.ATM_FADE_STATE_INIT              = 0;
     //moved public const int   EngineConstants.ATM_FADE_STATE_FADE              = 1;
     //moved public const int   EngineConstants.ATM_FADE_STATE_COMPLETE          = 2;

     //moved public const int   EngineConstants.ATM_FADE_TYPE_NORMAL             = 0;
     //moved public const int   EngineConstants.ATM_FADE_TYPE_EASE_IN            = 1;
     //moved public const int   EngineConstants.ATM_FADE_TYPE_EASE_OUT           = 2;

     //moved public const int   EngineConstants.ATM_FADE_STEPS_PER_SECOND        = 30;

     //moved public const int   EngineConstants.ATM_PRESET_FB_DEFAULT       = 0;
     //moved public const int   EngineConstants.ATM_PRESET_FB_BATTLE        = 1;
     //moved public const int   EngineConstants.ATM_PRESET_FB_NIGHT         = 2;
     //moved public const int   EngineConstants.ATM_PRESET_FB_LIGHTNING     = 3;

     //moved public const string EngineConstants.ATM_COLUMN_FB_HSV_HUE              = "fHue";
     //moved public const string EngineConstants.ATM_COLUMN_FB_HSV_SATURATION       = "fSaturation";
     //moved public const string EngineConstants.ATM_COLUMN_FB_HSV_CONTRAST         = "fContrast";
     //moved public const string EngineConstants.ATM_COLUMN_FB_HSV_BRIGHTNESS       = "fHSVBrightness";
     //moved public const string EngineConstants.ATM_COLUMN_FB_BLOOM_GLOW_INTENSITY = "fGlowIntensity";
     //moved public const string EngineConstants.ATM_COLUMN_FB_BLOOM_CONTRAST       = "fBloomContrast";
     //moved public const string EngineConstants.ATM_COLUMN_FB_BLOOM_BRIGHTNESS     = "fBloomBrightness";
     //moved public const string EngineConstants.ATM_COLUMN_FB_BLOOM_H_BLUR_WIDTH   = "fBloomHBlurWidth";
     //moved public const string EngineConstants.ATM_COLUMN_FB_BLOOM_V_BLUR_WIDTH   = "fBloomVBlurWidth";

     //moved public const string EngineConstants.ATM_PARAM_FB_HSV_HUE               = "Hue";
     //moved public const string EngineConstants.ATM_PARAM_FB_HSV_SATURATION        = "Saturation";
     //moved public const string EngineConstants.ATM_PARAM_FB_HSV_CONTRAST          = "Contrast";
     //moved public const string EngineConstants.ATM_PARAM_FB_HSV_BRIGHTNESS        = "Brightness";
     //moved public const string EngineConstants.ATM_PARAM_FB_BLOOM_GLOW_INTENSITY  = "GlowIntensity";
     //moved public const string EngineConstants.ATM_PARAM_FB_BLOOM_CONTRAST        = "Contrast";
     //moved public const string EngineConstants.ATM_PARAM_FB_BLOOM_BRIGHTNESS      = "Brightness";
     //moved public const string EngineConstants.ATM_PARAM_FB_BLOOM_H_BLUR_WIDTH    = "HBlurWidth";
     //moved public const string EngineConstants.ATM_PARAM_FB_BLOOM_V_BLUR_WIDTH    = "VBlurWidth";

     //moved public const int EngineConstants.ATM_FB_POSITION_HSV_HUE               = 0;
     //moved public const int EngineConstants.ATM_FB_POSITION_HSV_SATURATION        = 1;
     //moved public const int EngineConstants.ATM_FB_POSITION_HSV_CONTRAST          = 2;
     //moved public const int EngineConstants.ATM_FB_POSITION_HSV_BRIGHTNESS        = 3;
     //moved public const int EngineConstants.ATM_FB_POSITION_BLOOM_GLOW_INTENSITY  = 4;
     //moved public const int EngineConstants.ATM_FB_POSITION_BLOOM_CONTRAST        = 5;
     //moved public const int EngineConstants.ATM_FB_POSITION_BLOOM_BRIGHTNESS      = 6;
     //moved public const int EngineConstants.ATM_FB_POSITION_BLOOM_H_BLUR_WIDTH    = 7;
     //moved public const int EngineConstants.ATM_FB_POSITION_BLOOM_V_BLUR_WIDTH    = 8;

     //moved public const string EngineConstants.ATM_FB_HSV_MATRIX  = "da_HSVMatrix";
     //moved public const string EngineConstants.ATM_FB_BLOOM       = "da_bloom";

     /* @brief Returns an array of floats representing the FrameBuffer settings of a preset such as EngineConstants.ATM_FB_PRESET_DEFAULT
     *
     * @param int nFBPreset - the EngineConstants.ATM_FB_PRESET_* constant to retrieve
     * @returns An array of floats representing the cloud conditions of the preset.
     *
     * @author Craig Graff
     */
     public List<float> GetFBSettings(int nFBPreset)
     {
          List<float> arValues = new List<float>();

          arValues[EngineConstants.ATM_FB_POSITION_HSV_HUE] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_HSV_HUE, nFBPreset);
          arValues[EngineConstants.ATM_FB_POSITION_HSV_SATURATION] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_HSV_SATURATION, nFBPreset);
          arValues[EngineConstants.ATM_FB_POSITION_HSV_CONTRAST] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_HSV_CONTRAST, nFBPreset);
          arValues[EngineConstants.ATM_FB_POSITION_HSV_BRIGHTNESS] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_HSV_BRIGHTNESS, nFBPreset);
          arValues[EngineConstants.ATM_FB_POSITION_BLOOM_GLOW_INTENSITY] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_BLOOM_GLOW_INTENSITY, nFBPreset);
          arValues[EngineConstants.ATM_FB_POSITION_BLOOM_CONTRAST] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_BLOOM_CONTRAST, nFBPreset);
          arValues[EngineConstants.ATM_FB_POSITION_BLOOM_BRIGHTNESS] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_BLOOM_BRIGHTNESS, nFBPreset);
          arValues[EngineConstants.ATM_FB_POSITION_BLOOM_H_BLUR_WIDTH] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_BLOOM_H_BLUR_WIDTH, nFBPreset);
          arValues[EngineConstants.ATM_FB_POSITION_BLOOM_V_BLUR_WIDTH] = GetM2DAFloat(EngineConstants.TABLE_FRAMEBUFFER, EngineConstants.ATM_COLUMN_FB_BLOOM_V_BLUR_WIDTH, nFBPreset);

          return arValues;
     }

     /* @brief Sets the cloud conditions equal to a preset such as EngineConstants.ATM_PRESET_FB_DEFAULT
     *
     *   Values below EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE will not be applied.
     *
     * @param int nFBPreset - the EngineConstants.ATM_PRESET_FB_* constant to set conditions to
     *
     * @author Craig Graff
     */
     public void SetFBSettings(int nFBPreset)
     {
          List<float> arConditions = GetFBSettings(nFBPreset);
          /*
              Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                         "pre100_atmosphere_h.nss::SetFBSettings",
                         ToString(nFBPreset) + ":" +  ToString(arConditions[0]) + ":" +
                         ToString(arConditions[1]) + ":" +  ToString(arConditions[2]) + ":" +
                         ToString(arConditions[3]) + ":" +  ToString(arConditions[4]) +
                         ToString(arConditions[5]) + ":" +  ToString(arConditions[6]),
                         gameObject);        */

          if (arConditions[EngineConstants.ATM_FB_POSITION_HSV_HUE] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_HSV_MATRIX, EngineConstants.ATM_PARAM_FB_HSV_HUE, arConditions[EngineConstants.ATM_FB_POSITION_HSV_HUE]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "pre100_atmosphere_h.nss::ATM_PARAM_FB_HSV_HUE", FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_HSV_HUE]));
          }
          if (arConditions[EngineConstants.ATM_FB_POSITION_HSV_SATURATION] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_HSV_MATRIX, EngineConstants.ATM_PARAM_FB_HSV_SATURATION, arConditions[EngineConstants.ATM_FB_POSITION_HSV_SATURATION]);

               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                         "pre100_atmosphere_h.nss::ATM_PARAM_FB_HSV_SATURATION",
                          FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_HSV_SATURATION]));
          }
          if (arConditions[EngineConstants.ATM_FB_POSITION_HSV_CONTRAST] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_HSV_MATRIX, EngineConstants.ATM_PARAM_FB_HSV_CONTRAST, arConditions[EngineConstants.ATM_FB_POSITION_HSV_CONTRAST]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                         "pre100_atmosphere_h.nss::ATM_PARAM_FB_HSV_CONTRAST",
                       FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_HSV_CONTRAST]));
          }
          if (arConditions[EngineConstants.ATM_FB_POSITION_HSV_BRIGHTNESS] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_HSV_MATRIX, EngineConstants.ATM_PARAM_FB_HSV_BRIGHTNESS, arConditions[EngineConstants.ATM_FB_POSITION_HSV_BRIGHTNESS]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                      "pre100_atmosphere_h.nss::ATM_PARAM_FB_HSV_BRIGHTNESS",
                       FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_HSV_BRIGHTNESS]));
          }
          if (arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_GLOW_INTENSITY] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_BLOOM, EngineConstants.ATM_PARAM_FB_BLOOM_GLOW_INTENSITY, arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_GLOW_INTENSITY]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                      "pre100_atmosphere_h.nss::ATM_PARAM_FB_BLOOM_GLOW_INTENSITY",
                       FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_GLOW_INTENSITY]));
          }
          if (arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_CONTRAST] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_BLOOM, EngineConstants.ATM_PARAM_FB_BLOOM_CONTRAST, arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_CONTRAST]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                      "pre100_atmosphere_h.nss::ATM_PARAM_FB_BLOOM_CONTRAST",
                       FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_CONTRAST]));
          }
          if (arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_BRIGHTNESS] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_BLOOM, EngineConstants.ATM_PARAM_FB_BLOOM_BRIGHTNESS, arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_BRIGHTNESS]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                      "pre100_atmosphere_h.nss::ATM_PARAM_FB_BLOOM_BRIGHTNESS",
                       FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_BRIGHTNESS]));
          }
          if (arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_V_BLUR_WIDTH] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_BLOOM, EngineConstants.ATM_PARAM_FB_BLOOM_V_BLUR_WIDTH, arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_V_BLUR_WIDTH]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                      "pre100_atmosphere_h.nss::ATM_PARAM_FB_BLOOM_BRIGHTNESS",
                       FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_BRIGHTNESS]));
          }
          if (arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_H_BLUR_WIDTH] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
          {
               FB_SetEffectResource(EngineConstants.ATM_FB_BLOOM, EngineConstants.ATM_PARAM_FB_BLOOM_H_BLUR_WIDTH, arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_H_BLUR_WIDTH]);
               Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                      "pre100_atmosphere_h.nss::ATM_PARAM_FB_BLOOM_BRIGHTNESS",
                       FloatToString(arConditions[EngineConstants.ATM_FB_POSITION_BLOOM_BRIGHTNESS]));
          }
          FB_SetEffectEnabled(EngineConstants.ATM_FB_HSV_MATRIX, EngineConstants.TRUE);
          FB_SetEffectEnabled(EngineConstants.ATM_FB_BLOOM, EngineConstants.TRUE);
     }

     //------------------------------------------------------------------------------
     // Function Implementation
     //------------------------------------------------------------------------------

     public void ATM_Fade(int nAtmId1, int nAtmId2, float fDelay, int nFadeType)
     {

          xEvent evEvent;
          GameObject oPC;

          //--------------------------------------------------------------------------

          oPC = GetMainControlled();

          evEvent = ATM_EventFade(nAtmId1, nAtmId2, fDelay, nFadeType);
          DelayEvent(0.0f, GetArea(oPC), evEvent);

     }

     public xEvent ATM_EventFade(int nAtmId1, int nAtmId2, float fDelay, int nFadeType)
     {

          xEvent evEvent;
          float fDelayStep;

          //--------------------------------------------------------------------------

          evEvent = Event(EngineConstants.EVENT_ATM_FADE);
          fDelayStep = 1 / IntToFloat(EngineConstants.ATM_FADE_STEPS_PER_SECOND);

          SetEventIntegerRef(ref evEvent, 0, EngineConstants.ATM_FADE_STATE_INIT);
          SetEventIntegerRef(ref evEvent, 1, nAtmId1);       // Atmosphere 1
          SetEventIntegerRef(ref evEvent, 2, nAtmId2);       // Atmosphere 2
          SetEventIntegerRef(ref evEvent, 3, nFadeType);     // Fade Type
          SetEventFloatRef(ref evEvent, 0, fDelay);        // Fade Delay
          SetEventFloatRef(ref evEvent, 1, fDelayStep);    // Delay Per Step
          SetEventFloatRef(ref evEvent, 2, 0.0f);          // Delay Used

          return evEvent;

     }

     public void ATM_HandleEventFade(xEvent evEvent)
     {

          int nFadeState;
          int nAtmId1;
          int nAtmId2;
          int nFadeType;
          float fDelay;
          float fDelayStep;
          float fDelayUsed;
          GameObject oPC;

          //--------------------------------------------------------------------------

          oPC = GetMainControlled();

          nFadeState = GetEventIntegerRef(ref evEvent, 0);
          nAtmId1 = GetEventIntegerRef(ref evEvent, 1);
          nAtmId2 = GetEventIntegerRef(ref evEvent, 2);
          nFadeType = GetEventIntegerRef(ref evEvent, 3);
          fDelay = GetEventFloatRef(ref evEvent, 0);
          fDelayStep = GetEventFloatRef(ref evEvent, 1);
          fDelayUsed = GetEventFloatRef(ref evEvent, 2);

          /*
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                     "pre100_atmosphere_h.nss::ATM_HandleEventFade()",
                     ToString(nFadeState) + ":" +  ToString(nAtmId1) + ":" +
                     ToString(nAtmId2) + ":" +  ToString(fDelay) + ":" +
                     ToString(fDelayStep) + ":" +  ToString(fDelayUsed),
                     gameObject);
          */

          switch (nFadeState)
          {

               case EngineConstants.ATM_FADE_STATE_INIT:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.ATM_FADE_STATE_INIT
                         //------------------------------------------------------------------

                         SetEventIntegerRef(ref evEvent, 0, EngineConstants.ATM_FADE_STATE_FADE);
                         DelayEvent(0.0f, gameObject, evEvent);

                         break;

                    }

               case EngineConstants.ATM_FADE_STATE_FADE:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.ATM_FADE_STATE_FADE
                         //------------------------------------------------------------------

                         int nNextState;
                         float fRatio;

                         //------------------------------------------------------------------

                         if (fDelay > fDelayUsed)
                         {

                              nNextState = EngineConstants.ATM_FADE_STATE_FADE;
                              fDelayUsed = (fDelayUsed + fDelayStep);
                              fRatio = ATM_GetFadeRatio(nFadeType, fDelayUsed, fDelay);
                              SetEventFloatRef(ref evEvent, 2, fDelayUsed);
                              ATM_BlendAtmosphere(nAtmId1, nAtmId2, fRatio);

                         }

                         else
                         {

                              nNextState = EngineConstants.ATM_FADE_STATE_COMPLETE;

                         }

                         //------------------------------------------------------------------

                         SetEventIntegerRef(ref evEvent, 0, nNextState);
                         DelayEvent(fDelayStep, gameObject, evEvent);

                         break;

                    }

               case EngineConstants.ATM_FADE_STATE_COMPLETE:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.ATM_FADE_STATE_COMPLETE
                         //------------------------------------------------------------------

                         break;

                    }

          }
     }

     public void ATM_BlendAtmosphere(int nAtmId1, int nAtmId2, float fRatio)
     {

          int nIndex;
          int nArraySize;
          List<float> arAtm1 = new List<float>();
          List<float> arAtm2 = new List<float>();

          //--------------------------------------------------------------------------

          if (fRatio > 1.0f) fRatio = 1.0f;
          else if (fRatio < 0.0f) fRatio = 0.0f;

          arAtm1 = GetAtmosphericConditions(nAtmId1);
          arAtm2 = GetAtmosphericConditions(nAtmId2);

          nArraySize = GetArraySize(arAtm1);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               if (arAtm1[nIndex] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE && arAtm2[nIndex] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
               {
                    /*
                    Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                           "pre100_atmosphere_h.nss::ATM_BlendAtmosphere, atmosphere setting",
                           ToString(nIndex)+ " of " + ToString(nArraySize) + ":" +
                           ToString(((arAtm2[nIndex]-arAtm1[nIndex])*fRatio)+arAtm1[nIndex]));    */
                    arAtm1[nIndex] = ((arAtm2[nIndex] - arAtm1[nIndex]) * fRatio) + arAtm1[nIndex];
                    //SetAtmosphere(nIndex, ((arAtm2[nIndex]-arAtm1[nIndex])*fRatio)+arAtm1[nIndex]);
               }
          }

          SetAtmosphericConditionsCustom(arAtm1);
          /*
Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
   "pre100_atmosphere_h.nss::ATM_BlendAtmosphere, atmosphere1",
   ToString(nAtmId1) + ":" +  ToString(arAtm1[0]) + ":" +
   ToString(arAtm1[1]) + ":" +  ToString(arAtm1[2]) + ":" +
   ToString(arAtm1[3]) + ":" +  ToString(arAtm1[4]) + ":" +
   ToString(arAtm1[5]) + ":" +  ToString(arAtm1[6]) + ":" +
   ToString(arAtm1[7]) + ":" +  ToString(arAtm1[8]) + ":" +
   ToString(arAtm1[9]) + ":" +  ToString(arAtm1[10]),
   gameObject);
Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
   "pre100_atmosphere_h.nss::ATM_BlendAtmosphere, atmosphere2",
   ToString(nAtmId2) + ":" +  ToString(arAtm2[0]) + ":" +
   ToString(arAtm2[1]) + ":" +  ToString(arAtm2[2]) + ":" +
   ToString(arAtm2[3]) + ":" +  ToString(arAtm2[4]) + ":" +
   ToString(arAtm2[5]) + ":" +  ToString(arAtm2[6]) + ":" +
   ToString(arAtm2[7]) + ":" +  ToString(arAtm2[8]) + ":" +
   ToString(arAtm2[9]) + ":" +  ToString(arAtm2[10]),
   gameObject);      */

          arAtm1 = GetCloudConditions(nAtmId1);
          arAtm2 = GetCloudConditions(nAtmId2);

          nArraySize = GetArraySize(arAtm1);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               if (arAtm1[nIndex] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE && arAtm2[nIndex] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
               {   /*
            Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                   "pre100_atmosphere_h.nss::ATM_BlendAtmosphere, cloud setting",
                   ToString(nIndex+ATM_CLOUDS_INDEX_OFFSET)+ " of " + ToString(nArraySize + EngineConstants.ATM_CLOUDS_INDEX_OFFSET) + ":" +
                   ToString(((arAtm2[nIndex]-arAtm1[nIndex])*fRatio)+arAtm1[nIndex]));  */
                    arAtm1[nIndex] = ((arAtm2[nIndex] - arAtm1[nIndex]) * fRatio) + arAtm1[nIndex];
                    //SetAtmosphere((nIndex+ATM_CLOUDS_INDEX_OFFSET), ((arAtm2[nIndex]-arAtm1[nIndex])*fRatio)+arAtm1[nIndex]);

               }
          }

          SetCloudConditionsCustom(arAtm1);

          /*
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                         "pre100_atmosphere_h.nss::ATM_BlendAtmosphere, clouds1",
                         ToString(nAtmId1) + ":" +  ToString(arAtm1[0]) + ":" +
                         ToString(arAtm1[1]) + ":" +  ToString(arAtm1[2]) + ":" +
                         ToString(arAtm1[3]) + ":" +  ToString(arAtm1[4]) + ":" +
                         ToString(arAtm1[5]) + ":" +  ToString(arAtm1[6]) + ":" +
                         ToString(arAtm1[7]), gameObject);
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                         "pre100_atmosphere_h.nss::ATM_BlendAtmosphere, clouds2",
                         ToString(nAtmId2) + ":" +  ToString(arAtm2[0]) + ":" +
                         ToString(arAtm2[1]) + ":" +  ToString(arAtm2[2]) + ":" +
                         ToString(arAtm2[3]) + ":" +  ToString(arAtm2[4]) + ":" +
                         ToString(arAtm2[5]) + ":" +  ToString(arAtm2[6]) + ":" +
                         ToString(arAtm2[7]), gameObject);   */

          arAtm1 = GetFogConditions(nAtmId1);
          arAtm2 = GetFogConditions(nAtmId2);

          nArraySize = GetArraySize(arAtm1);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               if (arAtm1[nIndex] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE && arAtm2[nIndex] > EngineConstants.ATM_ATMOSPHERE_INVALID_VALUE)
               {   /*
            Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
                   "pre100_atmosphere_h.nss::ATM_BlendAtmosphere, cloud setting",
                   ToString(nIndex+ATM_CLOUDS_INDEX_OFFSET)+ " of " + ToString(nArraySize + EngineConstants.ATM_CLOUDS_INDEX_OFFSET) + ":" +
                   ToString(((arAtm2[nIndex]-arAtm1[nIndex])*fRatio)+arAtm1[nIndex]));  */

                    arAtm1[nIndex] = ((arAtm2[nIndex] - arAtm1[nIndex]) * fRatio) + arAtm1[nIndex];
                    //SetAtmosphere((nIndex+ATM_CLOUDS_INDEX_OFFSET), ((arAtm2[nIndex]-arAtm1[nIndex])*fRatio)+arAtm1[nIndex]);

               }
          }

          SetFogConditionsCustom(arAtm1);

     }

     public float ATM_GetFadeRatio(int nFadeType, float fDelayUsed, float fDelay)
     {

          float fRatio = 0.0f;

          //--------------------------------------------------------------------------

          switch (nFadeType)
          {

               case EngineConstants.ATM_FADE_TYPE_NORMAL:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.ATM_FADE_TYPE_NORMAL
                         //------------------------------------------------------------------

                         fRatio = (fDelayUsed / fDelay);

                         break;

                    }

               case EngineConstants.ATM_FADE_TYPE_EASE_IN:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.ATM_FADE_TYPE_EASE_IN
                         //------------------------------------------------------------------

                         fRatio = (fDelayUsed / fDelay);
                         fRatio = sin((fRatio) * (EngineConstants.PI / 4)) / sin(EngineConstants.PI / 4);

                         break;

                    }

               case EngineConstants.ATM_FADE_TYPE_EASE_OUT:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.ATM_FADE_TYPE_EASE_OUT
                         //------------------------------------------------------------------

                         fRatio = (fDelayUsed / fDelay);
                         fRatio = (sin((fRatio + 1) * (EngineConstants.PI / 4)) - sin(EngineConstants.PI / 4)) / (1 - sin(EngineConstants.PI / 4));

                         break;

                    }

          }

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "ATM_GetFadeRatio(" + ToString(nFadeType) + ")", ToString(fRatio));

          return fRatio;

     }
}