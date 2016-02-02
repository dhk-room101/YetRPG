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
using System.Linq;

public partial class Engine
{
     // -----------------------------------------------------------------------------
     // sys_audio_h
     // -----------------------------------------------------------------------------
     /*
         Plot Audio Event Handling

     */
     // -----------------------------------------------------------------------------
     // georg zoeller
     // -----------------------------------------------------------------------------

     //#include"core_h"

     //moved public const int EngineConstants.TABLE_AUDIO_PLOT_EVENTS = 1005;

     public void _ToggleSoundObjects(string[] objTags, int bEnable, int nEvent = 0)
     {
          int i = 0;
          int nSize = GetArraySize(objTags.ToList());
          for (i = 0; i < nSize; i++)
          {

               ActivateSoundByTag(objTags[i], bEnable);
               /*        GameObject objSound = GetObjectByTag(objTags[i]);

                       if (IsObjectValid(objSound))
                       {
                           if (GetObjectType(objSound) != EngineConstants.OBJECT_TYPE_SOUND)
                           {
                               Warning("Audio Event is trying to toggle a non sound GameObject (" + ToString(objSound) + "). This is a serious bug, please notify Yaron with this error message via SkyNet Bug. Script:  " + GetCurrentScriptName() + " " + ToString(i) + "/" + ToString(nSize));
                           }
                           else
                           {
                               SetObjectActive(objSound,bEnable);
                               if (bEnable)
                               {

                                   PlaySoundObject(objSound);
                               }
                               else
                               {

                                   StopSoundObject(objSound);
                               }

                           }
                       }
                       else
                       {
                        //   Warning("AudioTriggerPlotEvent(" + ToString(nEvent) +") could not find GameObject with tag '"+objTags[i]+"'! Please notify a sound designer." + " " + ToString(i) + "/" + ToString(nSize));
                       }   */

          }

     }

     public void AudioTriggerPlotEvent(int nEvent)
     {

          string sActivate = GetM2DAString(EngineConstants.TABLE_AUDIO_PLOT_EVENTS, "SoundActivate0", nEvent);
          string sDeactivate = GetM2DAString(EngineConstants.TABLE_AUDIO_PLOT_EVENTS, "SoundDeactivate0", nEvent);
          string[] activate = SplitString(sActivate, " ");
          string[] deactivate = SplitString(sDeactivate, " ");

          if (GetArraySize(deactivate.ToList()) != EngineConstants.FALSE)
          {
               _ToggleSoundObjects(deactivate, EngineConstants.FALSE, nEvent);
          }

          if (GetArraySize(activate.ToList()) != EngineConstants.FALSE)
          {
               _ToggleSoundObjects(activate, EngineConstants.TRUE, nEvent);
          }

          sActivate = GetM2DAString(EngineConstants.TABLE_AUDIO_PLOT_EVENTS, "SoundActivate1", nEvent);
          sDeactivate = GetM2DAString(EngineConstants.TABLE_AUDIO_PLOT_EVENTS, "SoundDeactivate1", nEvent);

          if (GetStringLength(sDeactivate) != EngineConstants.FALSE)
          {
               string[] deactivate1 = SplitString(sDeactivate, " ");
               if (GetArraySize(deactivate1.ToList()) != EngineConstants.FALSE)
               {
                    _ToggleSoundObjects(deactivate1, EngineConstants.FALSE, nEvent);
               }
          }

          if (GetStringLength(sActivate) != EngineConstants.FALSE)
          {
               string[] activate1 = SplitString(sActivate, " ");
               if (GetArraySize(activate1.ToList()) != EngineConstants.FALSE)
               {
                    _ToggleSoundObjects(activate1, EngineConstants.TRUE, nEvent);
               }

          }

     }
}