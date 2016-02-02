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
     //::///////////////////////////////////////////////
     //:: ran_constants_h
     //:: Copyright (c) 2008 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         List of constants for the Random Encounters
     */
     //:://////////////////////////////////////////////
     //:: Created By: Mark Barazzuol
     //:: Created On: May 26, 2008
     //:://////////////////////////////////////////////

     //#include"wrappers_h"
     //#include"sys_traps_h"

     public void RAN_RestartRandomArea(string[] sMonster, string[] rMonster, string[] sWPMonster)
     {

          Vector3 locWP;
          GameObject oArea = GetArea(GetHero());
          List<GameObject> arObjects = GetObjectsInArea(oArea);

          int nType;
          int nSize;
          int i, ii;                                    // Generic counter

          //===================

          //===================

          nSize = GetArraySize(arObjects);

          for (i = 0; i < nSize; i++)
          {
               // Reset subcounter
               ii = 0;

               // Destroy any corpses
               while (sMonster[ii] != "")
               {
                    if (GetTag(arObjects[i]) == sMonster[ii])
                    {
                         WR_DestroyObject(arObjects[i]);
                         break;
                    }
                    ii++; // Increment counter
               }

               // Destroy any Bodybags
               if (GetTag(arObjects[i]) == EngineConstants.BODY_BAG_TAG)
               {
                    DestroyPlaceable(arObjects[i]);
                    WR_DestroyObject(arObjects[i]);
               }

               // Rearm all the traps
               if (IsTrapTrigger(arObjects[i]) != EngineConstants.FALSE)
               {
                    Trap_ArmTrap(arObjects[i], null, 0.0f);
                    Trap_SetDetected(arObjects[i], EngineConstants.FALSE);
                    SetObjectActive(arObjects[i], EngineConstants.FALSE);
                    // One in three chance for trap to be active
                    if (Engine_Random(2) == 1)
                         SetObjectActive(arObjects[i], EngineConstants.TRUE);
               }
          }

          Log_Trace_Scripting_Error("ran_area", "About to create creature");
          // Create Monsters
          for (i = 0; i < nSize; i++)
          {
               if (GetObjectType(arObjects[i]) == EngineConstants.OBJECT_TYPE_WAYPOINT)
               {
                    // Reset Counter
                    ii = 0;
                    while (sWPMonster[ii] != "")
                    {
                         // Place monster type
                         if (GetTag(arObjects[i]) == sWPMonster[ii])
                         {
                              locWP = GetLocation(arObjects[i]);
                              Log_Trace_Scripting_Error("ran_area", "Created character on wp:");
                              Log_Trace_Scripting_Error("ran_area", "sWPMonster[ii]");
                              CreateObject(EngineConstants.OBJECT_TYPE_CREATURE, rMonster[ii], locWP);
                         }
                         ii++;
                    }  //end while
               }
          } // end if

          // Refill Plants

     }
}