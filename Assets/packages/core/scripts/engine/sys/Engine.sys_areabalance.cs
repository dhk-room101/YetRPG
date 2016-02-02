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
     // sys_areabalance
     // -----------------------------------------------------------------------------
     /*
         Control of creature balancing per area


     */
     // -----------------------------------------------------------------------------
     // owner: georg zoeller, agauthier
     // -----------------------------------------------------------------------------


     //#include "core_h"
     //#include "sys_rewards_h"
     //moved const int EngineConstants.TABLE_AREA_DATA = 225;
     //moved const int EngineConstants.TABLE_AREA_SCALEGROUP = 227;



     public int _GetAreaId(GameObject oArea)
     {
          int nId = GetLocalInt(oArea, EngineConstants.AREA_ID);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "sys_areabalance::_GetAreaId", "ID: " + ToString(nId));


          return nId;
     }



     public int _GetScaleGroup(GameObject oArea)
     {
          int nId = _GetAreaId(oArea);
          int nGroup = GetM2DAInt(EngineConstants.TABLE_AREA_DATA, "ScaleGroup", nId);

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "sys_areabalance::_GetScaleGroup", "ScaleGroup: " + ToString(nGroup));

          return nGroup;
     }

     public int _GetAreaMinLevel(GameObject oArea)
     {
          //    int nSg = _GetScaleGroup(oArea);
          //    int nMinLevel = GetM2DAInt(EngineConstants.TABLE_AREA_SCALEGROUP,"MinLevel",nSg);


          int nMinLevel = GetM2DAInt(EngineConstants.TABLE_AREA_DATA, "MinLevel", _GetAreaId(oArea));

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "sys_areabalance::_GetAreaMinLevel", GetTag(oArea) + " MinLevel: " + ToString(nMinLevel));

          return nMinLevel;
     }


     public int AB_GetAreaTargetLevel(GameObject oCreature)
     {
          GameObject oArea = GetArea(oCreature);
          //    int nSg = _GetScaleGroup(oArea);
          //    int nMinLevel = GetM2DAInt(EngineConstants.TABLE_AREA_SCALEGROUP,"MinLevel",nSg);

          int nAid = _GetAreaId(oArea);

          int nMinLevel = GetM2DAInt(EngineConstants.TABLE_AREA_DATA, "MinLevel", nAid);
          int nMaxLevel = GetM2DAInt(EngineConstants.TABLE_AREA_DATA, "MaxLevel", nAid);

          int nHero = GetLevel(GetHero());
          int nTarget = Max(Max(nMinLevel, Min(nHero, nMaxLevel)), 1);


          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "sys_areabalance::_GetAreaMinLevel", "MinLevel: " + ToString(nMinLevel));

          return nTarget;
     }





     public void AB_ForceToMinLevel(GameObject oChar, GameObject oArea)
     {
          int nMinLevel = _GetAreaMinLevel(oArea);
          int nXP = RW_GetXPNeededForLevel(nMinLevel);
          int nCurrentXP = FloatToInt(GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE));

          if (nCurrentXP < nXP)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "sys_areabalance::AB_ForceToMinLevel", "Forcing MinLevel  " + ToString(nMinLevel) + " on " + ToString(oChar));
               RewardXP(oChar, nXP - nCurrentXP);
          }


     }
}