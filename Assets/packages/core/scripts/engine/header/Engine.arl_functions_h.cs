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
     //#include"utility_h"

     //#include"arl_constants_h"

     //Generic functions used in the Arl Eamon plot.

     public void ARL_ApplyTeamVisualEffect(int nTeam, int nVFX, int nObjectType = EngineConstants.OBJECT_TYPE_CREATURE)
     {
          List<GameObject> oTeamArray = GetTeam(nTeam, nObjectType);
          int nTeamSize = GetArraySize(oTeamArray);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               GameObject oTarget = oTeamArray[nIndex];
               ApplyEffectVisualEffect(oTarget, oTarget, nVFX, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, 0.0f);
          }
     }

     public void ARL_SetTeamGore(int nTeam, float fGoreLevel)
     {
          List<GameObject> oTeamArray = GetTeam(nTeam);
          int nTeamSize = GetArraySize(oTeamArray);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               GameObject oCreature = oTeamArray[nIndex];
               SetCreatureGoreLevel(oCreature, fGoreLevel);
          }
     }

     public void ARL_SetTeamGroup(int nTeam, int nGroup)
     {
          List<GameObject> oTeamArray = GetTeam(nTeam);
          int nTeamSize = GetArraySize(oTeamArray);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               GameObject oCreature = oTeamArray[nIndex];
               SetGroupId(oCreature, nGroup);
               Log_Trace(EngineConstants.LOG_CHANNEL_PLOT, "", "Setting group of creature to " + IntToString(GetGroupId(oCreature)) + " Name: " + GetName(oCreature) + " ObjectID: " + ObjectToString(oCreature) + " Tag: " + GetTag(oCreature));
          }
     }

     public void ARL_SetTeamPlot(int nTeam, int bPlot, int nObjectType)
     {
          List<GameObject> oTeamArray = GetTeam(nTeam, nObjectType);
          int nTeamSize = GetArraySize(oTeamArray);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               GameObject oObject = oTeamArray[nIndex];
               SetPlot(oObject, bPlot);
          }
     }

     public void ARL_SetTeamStatue(int nTeam, int bStatue)
     {
          List<GameObject> oTeamArray = GetTeam(nTeam);
          int nTeamSize = GetArraySize(oTeamArray);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               GameObject oCreature = oTeamArray[nIndex];
               SetCreatureIsStatue(oCreature, bStatue);
          }
     }

     public void ARL_PartyJump(string sWaypointTag)
     {
          int nIndex;
          int nArraySize;
          GameObject oCurrent;
          List<GameObject> arPartyList;
          //--------------------------------------------------------------------------
          arPartyList = GetPartyList(GetHero());
          nArraySize = GetArraySize(arPartyList);
          //--------------------------------------------------------------------------
          UT_LocalJump(GetMainControlled(), sWaypointTag, EngineConstants.TRUE, EngineConstants.TRUE, EngineConstants.TRUE);
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               oCurrent = arPartyList[nIndex];
               UT_LocalJump(oCurrent, sWaypointTag, EngineConstants.TRUE, EngineConstants.TRUE, EngineConstants.TRUE);
          }
     }

     public void ARL_DestroyAllItemsInIventory(GameObject oObject)
     {
          List<GameObject> arInventory = GetItemsInInventory(oObject);
          int nInventorySize = GetArraySize(arInventory);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nInventorySize; nIndex++)
          {
               GameObject oItem = arInventory[nIndex];
               Safe_Destroy_Object(oItem);
          }
     }

     public void ARL_TriggerCorpseAmbush(GameObject oArea, int nCorpseTeam)
     {
          List<GameObject> oCorpseTeamArray = GetTeam(nCorpseTeam);
          int nTeamSize = GetArraySize(oCorpseTeamArray);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nTeamSize; nIndex++)
          {
               GameObject oCorpse = oCorpseTeamArray[nIndex];
               xEvent evHostile = Event(EngineConstants.ARL_EVENT_CREATURE_GOES_HOSTILE);
               SetEventObjectRef(ref evHostile, 0, oCorpse);
               DelayEvent(RandomFloat() * 2.0f, oArea, evHostile);
          }
     }

     public void ARL_DestroyAllWithTag(string sTag)
     {
          List<GameObject> oObjectsArray = GetNearestObjectByTag(GetHero(), sTag, EngineConstants.OBJECT_TYPE_ALL, 100);

          int nArraySize = GetArraySize(oObjectsArray);
          int nIndex = 0;
          for (nIndex = 0; nIndex < nArraySize; nIndex++)
          {
               GameObject oObject = oObjectsArray[nIndex];
               Safe_Destroy_Object(oObject);
          }

     }
}