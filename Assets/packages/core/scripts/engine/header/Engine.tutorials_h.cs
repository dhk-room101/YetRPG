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
     ////////////////////////////////////////////////////////////////////////////////
     //  Tutorials Include file
     //  Copyright � 2007 Bioware Corp.
     ////////////////////////////////////////////////////////////////////////////////
     /*
         Handles defines for any general tutorial functions
     */
     ////////////////////////////////////////////////////////////////////////////////

     // Unique Identifiers for the various Teach and Test Tutorials.
     //moved public const int TRAINING_SESSION_INVALID                   = 0;
     //moved public const int TRAINING_SESSION_TACTICAL_VIEW             = 1;
     //moved public const int TRAINING_SESSION_MOVE_COMMANDS             = 2;
     //moved public const int TRAINING_SESSION_FOLLOWERS_AND_TACTICS     = 3;
     //moved public const int TRAINING_SESSION_LEVEL_UP                  = 4;
     //moved public const int TRAINING_SESSION_EQUIPMENT                 = 5;
     //moved public const int TRAINING_SESSION_HOTBAR                    = 6;

     public void BeginTrainingMode(int a_nTrainingSessionId)
     {
          //This will fire a: EngineConstants.EVENT_TYPE_TRAINING_BEGIN event.
          SetTrainingMode(a_nTrainingSessionId);
     }

     public void EndTrainingMode()
     {
          SetTrainingMode(EngineConstants.TRAINING_SESSION_INVALID);
     }
}