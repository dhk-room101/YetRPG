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

         Prelude
          -> Lightning

         This is a self contained event.

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: September 20, 2007
     //==============================================================================

     //#include"utility_h"
     //#include"events_h"
     //#include"pre100_atmosphere_h"
     //#include"pre_objects_h"

     //------------------------------------------------------------------------------
     // Constants
     //------------------------------------------------------------------------------

     //moved public const int   PRE_STORM_EVENT_LIGHTNING                   = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;

     //moved public const int   PRE_STORM_LIGHTNING_STATE_IMPACT            = 1000;
     //moved public const int   PRE_STORM_LIGHTNING_STATE_IMPACT_CHAIN      = 1001;
     //moved public const int   PRE_STORM_LIGHTNING_STATE_THUNDER           = 2000;
     //moved public const int   PRE_STORM_LIGHTNING_STATE_COOLDOWN          = 3000;

     //moved public const int   PRE_STORM_LIGHTNING_EFFECT_IMPACT           = 4016;
     //moved public const int   PRE_STORM_LIGHTNING_EFFECT_IMPACT_CHAIN     = 0;
     //moved public const int   PRE_STORM_LIGHTNING_EFFECT_THUNDER          = 0;

     //moved public const float PRE_STORM_LIGHTNING_MULT_IMPACT_CHAIN_1     = 0.60f;
     //moved public const float PRE_STORM_LIGHTNING_MULT_IMPACT_CHAIN_2     = 0.80f;
     //moved public const float PRE_STORM_LIGHTNING_MULT_IMPACT_CHAIN_3     = 0.60f;

     //moved public const float PRE_STORM_LIGHTNING_DURATION                = 0.75f;
     //moved public const float PRE_STORM_LIGHTNING_DELAY_IMPACT            = 0.10f;
     //moved public const float PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_1_1  = 0.04f;
     //moved public const float PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_1_2  = 0.10f;
     //moved public const float PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_2_1  = 0.05f;
     //moved public const float PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_2_2  = 0.05f;
     //moved public const float PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_3_1  = 0.03f;
     //moved public const float PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_3_2  = 0.08f;
     //moved public const float PRE_STORM_LIGHTNING_DELAY_THUNDER           = 1.7f;
     //moved public const int   PRE_STORM_LIGHTNING_DELAY_COOLDOWN_F        = 1;
     //moved public const int   PRE_STORM_LIGHTNING_DELAY_COOLDOWN_V        = 8;

     //------------------------------------------------------------------------------

     //moved public const string    PRE_WP_LIGHTNING_POINT                  = "pre100ip_lightning_point";
     //moved public const string    EngineConstants.PRE_STORM_THUNDER_1                     = "amb_ext_scthun1_sgl";
     //moved public const string    EngineConstants.PRE_STORM_THUNDER_2                     = "amb_ext_scthun2_sgl";
     //moved public const string    EngineConstants.PRE_STORM_THUNDER_3                     = "amb_ext_scthun3_sgl";
     //moved public const string    EngineConstants.PRE_STORM_THUNDER_4                     = "amb_ext_scthun4_sgl";
     //moved public const string    EngineConstants.PRE_STORM_THUNDER_5                     = "amb_ext_scthun5_sgl";
     //moved public const string    EngineConstants.PRE_STORM_THUNDER_6                     = "amb_ext_scthun6_sgl";
     //moved public const string    EngineConstants.PRE_STORM_THUNDER_7                     = "amb_ext_scthun7_sgl";
     //moved public const string    EngineConstants.PRE_STORM_THUNDER_8                     = "amb_ext_scthun8_sgl";

     //------------------------------------------------------------------------------
     // Function Implementation
     //------------------------------------------------------------------------------

     /*seems to be disabled*/
     public void PRE_Storm_PlayThunder(int nChainNumber, GameObject oImpactPoint)
     {
          /*    string sThunder;
              GameObject oThunder;
              GameObject oParty = GetMainControlled();
              if (nChainNumber == 3)
              {
                  switch (Engine_Random(3))
                  {
                      case 0:
                      {
                          sThunder = EngineConstants.PRE_STORM_THUNDER_7;
                          oThunder = GetObjectByTag(sThunder);
                          SetPosition(oThunder, GetPosition(oParty));
                          break;
                      }
                      default:
                      {
                          sThunder = EngineConstants.PRE_STORM_THUNDER_4;
                          oThunder = GetObjectByTag(sThunder);
                          //SetPosition(oThunder, GetPosition(oImpactPoint));
                      }
                  }
              }
              else
              {
                  switch (Engine_Random(5))
                  {
                      case 0:     sThunder = EngineConstants.PRE_STORM_THUNDER_1;     break;
                      case 1:     sThunder = EngineConstants.PRE_STORM_THUNDER_2;     break;
                      case 2:     sThunder = EngineConstants.PRE_STORM_THUNDER_3;     break;
                      case 3:     sThunder = EngineConstants.PRE_STORM_THUNDER_5;     break;
                      case 4:     sThunder = EngineConstants.PRE_STORM_THUNDER_6;     break;
                      case 5:     sThunder = EngineConstants.PRE_STORM_THUNDER_7;     break;
                  }
                  //oThunder = GetObjectByTag(sThunder);
                  //SetPosition(oThunder, GetPosition(oImpactPoint));
              }
              //PlaySoundObject(oThunder);
          */
     }

     public xEvent PRE_Storm_EventLightning(int nState = EngineConstants.PRE_STORM_LIGHTNING_STATE_IMPACT, int nSeed = 0, int bLoop = EngineConstants.FALSE)
     {

          xEvent evEvent;

          //--------------------------------------------------------------------------

          evEvent = Event(EngineConstants.PRE_STORM_EVENT_LIGHTNING);
          SetEventIntegerRef(ref evEvent, 0, nState);    // Event State
          SetEventIntegerRef(ref evEvent, 1, nSeed);     // Event Seed
          SetEventIntegerRef(ref evEvent, 2, bLoop);     // Play once or loop
          SetEventIntegerRef(ref evEvent, 3, 1);         // Chain number
          SetEventIntegerRef(ref evEvent, 4, EngineConstants.FALSE);         // Chain clear?

          return evEvent;

     }

     //------------------------------------------------------------------------------

     public void PRE_Storm_HandleEventLightning(xEvent evEvent)
     {

          int nLightningState;
          int nSeed;
          int bLoop;
          int nChainNumber;
          int bChainClear;
          float fEventDelay = 0.0f;
          xEffect effStateEffect;
          GameObject oPC;
          GameObject oThis = gameObject;
          //--------------------------------------------------------------------------

          nLightningState = GetEventIntegerRef(ref evEvent, 0);
          nSeed = GetEventIntegerRef(ref evEvent, 1);
          bLoop = GetEventIntegerRef(ref evEvent, 2);
          nChainNumber = GetEventIntegerRef(ref evEvent, 3);
          bChainClear = GetEventIntegerRef(ref evEvent, 4);

          oPC = GetMainControlled();

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP,
          "pre100_lightning_h:PRE_Storm_HandleEventLightning",
           ToString(nLightningState) + ":" + ToString(nSeed) + ":" + ToString(bLoop) + ":" +
           ToString(nChainNumber) + ":" + ToString(bChainClear),
           gameObject);

          switch (nLightningState)
          {

               case EngineConstants.PRE_STORM_LIGHTNING_STATE_IMPACT:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.PRE_STORM_LIGHTNING_STATE_IMPACT
                         //------------------------------------------------------------------

                         //if (EngineConstants.PRE_STORM_LIGHTNING_EFFECT_IMPACT)//?!?DHK
                         //{
                         effStateEffect = EffectVisualEffect(EngineConstants.PRE_STORM_LIGHTNING_EFFECT_IMPACT);
                         Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY,
                                                           effStateEffect,
                                                           GetLocation(oThis),
                                                           0.75f,
                                                           oThis);
                         //}

                         //PRE_Storm_TEMPSetLightningAtmosphere(1.00f);

                         //------------------------------------------------------------------

                         evEvent = PRE_Storm_EventLightning(EngineConstants.PRE_STORM_LIGHTNING_STATE_IMPACT_CHAIN, nSeed, bLoop);
                         DelayEvent(EngineConstants.PRE_STORM_LIGHTNING_DELAY_IMPACT, oThis, evEvent);

                         break;

                    }

               case EngineConstants.PRE_STORM_LIGHTNING_STATE_IMPACT_CHAIN:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.PRE_STORM_LIGHTNING_STATE_IMPACT_CHAIN
                         //------------------------------------------------------------------

                         int bChainContinue;
                         float fChainModifier;

                         //------------------------------------------------------------------

                         bChainContinue = EngineConstants.TRUE;

                         if (bChainClear != EngineConstants.FALSE)
                         {
                              //ResetAtmosphere();
                              bChainContinue = (Engine_Random(100) > (nChainNumber * 27)) ? EngineConstants.TRUE : EngineConstants.FALSE;
                              switch (nChainNumber)
                              {
                                   case 1:
                                        fEventDelay = EngineConstants.PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_1_1;
                                        break;
                                   case 2:
                                        fEventDelay = EngineConstants.PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_2_1;
                                        break;
                                   case 3:
                                        fEventDelay = EngineConstants.PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_3_1;
                                        break;
                              }
                              if (bChainContinue == EngineConstants.FALSE)
                              {
                                   fEventDelay += 0.35f;
                                   SetFBSettings(EngineConstants.ATM_PRESET_FB_BATTLE);
                              }
                              ATM_Fade(EngineConstants.ATM_PRESET_LIGHTNING, EngineConstants.ATM_PRESET_BATTLE, fEventDelay, EngineConstants.ATM_FADE_TYPE_EASE_IN);
                         }
                         else
                         {
                              switch (nChainNumber)
                              {
                                   case 1: //fChainModifier = EngineConstants.PRE_STORM_LIGHTNING_MULT_IMPACT_CHAIN_1;
                                        fEventDelay = EngineConstants.PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_1_2;
                                        SetFBSettings(EngineConstants.ATM_PRESET_FB_LIGHTNING);
                                        break;
                                   case 2: //fChainModifier = EngineConstants.PRE_STORM_LIGHTNING_MULT_IMPACT_CHAIN_2;
                                        fEventDelay = EngineConstants.PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_2_2;
                                        break;

                                   case 3: //fChainModifier = EngineConstants.PRE_STORM_LIGHTNING_MULT_IMPACT_CHAIN_3;
                                        fEventDelay = EngineConstants.PRE_STORM_LIGHTNING_DELAY_IMPACT_CHAIN_3_2;
                                        break;
                              }
                              ATM_Fade(EngineConstants.ATM_PRESET_BATTLE, EngineConstants.ATM_PRESET_LIGHTNING, fEventDelay, EngineConstants.ATM_FADE_TYPE_NORMAL);
                              //PRE_Storm_TEMPSetLightningAtmosphere(fChainModifier);
                         }

                         //------------------------------------------------------------------

                         if (bChainContinue != EngineConstants.FALSE)
                         {
                              if (bChainClear != EngineConstants.FALSE)
                              {
                                   SetEventIntegerRef(ref evEvent, 3, (nChainNumber + 1));
                                   SetEventIntegerRef(ref evEvent, 4, EngineConstants.FALSE);
                              }
                              else
                              {
                                   SetEventIntegerRef(ref evEvent, 4, EngineConstants.TRUE);
                              }
                         }
                         else
                         {
                              fEventDelay += EngineConstants.PRE_STORM_LIGHTNING_DELAY_THUNDER;
                              evEvent = PRE_Storm_EventLightning(EngineConstants.PRE_STORM_LIGHTNING_STATE_THUNDER, nSeed, bLoop);
                         }

                         DelayEvent(fEventDelay, oThis, evEvent);

                         break;

                    }

               case EngineConstants.PRE_STORM_LIGHTNING_STATE_THUNDER:
                    {

                         //------------------------------------------------------------------
                         // PEngineConstants.PRE_STORM_LIGHTNING_STATE_THUNDER
                         //------------------------------------------------------------------

                         PRE_Storm_PlayThunder(nChainNumber, oThis);
                         //if (EngineConstants.PRE_STORM_LIGHTNING_EFFECT_THUNDER)
                         //{
                         effStateEffect = EffectVisualEffect(EngineConstants.PRE_STORM_LIGHTNING_EFFECT_THUNDER);
                         Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT,
                                                           effStateEffect,
                                                           GetLocation(oThis),
                                                           5.0f,
                                                           oThis);
                         //}

                         //------------------------------------------------------------------

                         if (bLoop != EngineConstants.FALSE)
                         {
                              evEvent = PRE_Storm_EventLightning(EngineConstants.PRE_STORM_LIGHTNING_STATE_COOLDOWN, nSeed, bLoop);
                              DelayEvent(EngineConstants.PRE_STORM_LIGHTNING_DELAY_THUNDER, oThis, evEvent);
                         }

                         break;

                    }

               case EngineConstants.PRE_STORM_LIGHTNING_STATE_COOLDOWN:
                    {

                         //------------------------------------------------------------------
                         // EngineConstants.PRE_STORM_LIGHTNING_STATE_COOLDOWN
                         //------------------------------------------------------------------

                         int nIndex;
                         int nEffectId;
                         GameObject oArea;
                         xEffect effCurrent;
                         List<xEffect> arAreaEffects;

                         //------------------------------------------------------------------

                         oArea = GetArea(oPC);
                         arAreaEffects = GetEffects(GetArea(oPC));

                         for (nIndex = 0; nIndex < GetArraySize(arAreaEffects); nIndex++)
                         {
                              effCurrent = arAreaEffects[nIndex];
                              nEffectId = GetVisualEffectID(effCurrent);
                              switch (nEffectId)
                              {
                                   case EngineConstants.PRE_STORM_LIGHTNING_EFFECT_IMPACT:
                                        //case EngineConstants.PRE_STORM_LIGHTNING_EFFECT_IMPACT_CHAIN:
                                        //case EngineConstants.PRE_STORM_LIGHTNING_EFFECT_THUNDER:
                                        {
                                             RemoveEffect(oArea, effCurrent);
                                             break;
                                        }
                              }
                         }

                         //------------------------------------------------------------------

                         if (bLoop != EngineConstants.FALSE)
                         {
                              fEventDelay = RandomF(EngineConstants.PRE_STORM_LIGHTNING_DELAY_COOLDOWN_V, EngineConstants.PRE_STORM_LIGHTNING_DELAY_COOLDOWN_F);
                              PRE_Storm_LightningStart(fEventDelay, nSeed, EngineConstants.TRUE);
                         }

                         break;

                    }
          }
     }

     public void PRE_Storm_LightningStart(float fDelay = 0.0f, int nLastSeed = -1, int bLoop = EngineConstants.TRUE)
     {

          int nIndex;
          xEvent evEvent;
          List<GameObject> arLightningPoints;
          GameObject oCurrent;
          GameObject oPC;

          //--------------------------------------------------------------------------

          oPC = GetMainControlled();

          // Grab Objects we need
          arLightningPoints = UT_GetTeam(EngineConstants.PRE_TEAM_CAMP_NIGHT_LIGHTNING_POINTS, EngineConstants.OBJECT_TYPE_PLACEABLE);

          // We do a lotto draw to see which lightning point is going to be used.
          nIndex = Engine_Random(GetArraySize(arLightningPoints));
          if (nIndex == nLastSeed)
               nIndex = Engine_Random(GetArraySize(arLightningPoints));

          evEvent = PRE_Storm_EventLightning(EngineConstants.PRE_STORM_LIGHTNING_STATE_IMPACT, nIndex, bLoop);
          DelayEvent(fDelay, arLightningPoints[nIndex], evEvent);

     }
}