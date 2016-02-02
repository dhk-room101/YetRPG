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

         Nature of the Beast
          -> Generic Functions Script

         These are the generic functions for the NTB plot.

     */
     //------------------------------------------------------------------------------
     // Created By: Cori May
     // Created On: April 30, 2008
     //==============================================================================

     /*//#include"utility_h"

     //#include"ntb_constants_h"
     //#include"plt_cod_bks_lanaya_songs"
     //#include"plt_cod_bks_ritual_tablet"
     //#include"plt_cod_bks_hermit_book"
     //#include"plt_ntb100pt_varathorn"
     //#include"plt_ntb210pt_hermit"
     //#include"plt_ntb220pt_grand_oak"
     //#include"plt_ntb330pt_elven_ritual"*/

     ///////////////////////////////////////////////////////////////////////
     // Sent by: ntb000pt_main
     // When: the PC attacks the elves, checks possibly inactive characters
     ////////////////////////////////////////////////////////////////////////
     public void NTB_CheckElfCombatants(GameObject oCreature)
     {
          int nActive = GetObjectActive(oCreature);
          string sCreature = GetTag(oCreature);
          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "ntb_functions.h", sCreature + " active: " + IntToString(nActive));
          if (nActive == EngineConstants.FALSE)
          {
               WR_DestroyObject(oCreature);
          }
          //else equip their weapons
          else
          {
               // Equip all possible items in backpack (shield if we got it)
               List<GameObject> arInventory = GetItemsInInventory(oCreature, EngineConstants.GET_ITEMS_OPTION_BACKPACK);
               GameObject oCurrent;
               int nArraySize = GetArraySize(arInventory);
               int nIndex;
               for (nIndex = 0; nIndex < nArraySize; nIndex++)
               {
                    oCurrent = arInventory[nIndex];
                    EquipItem(oCreature, oCurrent);
               }
          }
     }

     ///////////////////////////////////////////////////////////////////////
     // Sent by: ntb210pt_campsite
     // When: the camp is no longer interactive, the shade appears
     ////////////////////////////////////////////////////////////////////////
     public void NTB_CampShadeFightStart()
     {
          GameObject oPC = GetHero();
          GameObject oShade = UT_GetNearestCreatureByTag(oPC, EngineConstants.NTB_CR_CAMP_SHADE);
          GameObject oCampTr = UT_GetNearestObjectByTag(oPC, EngineConstants.NTB_TR_CAMPSITE);

          //----------------------------------------------------------------------
          //illusion breaks down
          //shade appears and attacks
          //----------------------------------------------------------------------
          WR_DestroyObject(oCampTr);

          UT_TeamAppears(EngineConstants.NTB_TEAM_WEST_FOREST_CAMPSITE, EngineConstants.FALSE, EngineConstants.OBJECT_TYPE_PLACEABLE);
          UT_TeamAppears(EngineConstants.NTB_TEAM_WEST_FOREST_BONES, EngineConstants.TRUE, EngineConstants.OBJECT_TYPE_PLACEABLE);
          WR_SetObjectActive(oShade, EngineConstants.TRUE);
     }

     ///////////////////////////////////////////////////////////////////////
     // Sent by: ntb210pt_campsite
     // When: checks the party's willpower
     ////////////////////////////////////////////////////////////////////////
     public void NTB_ComparePartyWillpower()
     {
          GameObject oPC = GetHero();
          List<GameObject> arParty = GetPartyList(oPC);
          int nSize = GetArraySize(arParty);
          int nIndex;
          GameObject oCurrent;
          int nCurrent;
          GameObject oHighest = oPC;
          int nHighest = GetCreatureAttribute(oHighest, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER);
          xEffect eSleep = EffectFeignDeath();

          for (nIndex = 0; nIndex < nSize; nIndex++)
          {
               oCurrent = arParty[nIndex];

               //if it's not the PC object
               if (oCurrent != oPC)
               {

                    nCurrent = GetCreatureAttribute(oCurrent, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER);

                    int nCompare = CompareInt(nHighest, nCurrent);
                    //if Highest is lower than current, current is the new highest
                    if (nCompare == EngineConstants.COMPARE_RESULT_LOWER)
                    {
                         //ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT,eDeath,oHighest);
                         //ForceSleepStart(oHighest);
                         KillCreature(oHighest);
                         oHighest = oCurrent;
                         nHighest = nCurrent;
                         //
                    }
                    //else highest stays the same - knock out current
                    else
                    {
                         //ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT,eDeath,oCurrent);
                         //ForceSleepStart(oCurrent);
                         KillCreature(oCurrent);
                    }
               }

          }

          string sHighest = GetTag(oHighest);
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "ntb_functions.nss", sHighest + " survived.");

     }

     ///////////////////////////////////////////////////////////////////////
     // Sent by: ntb210cr_camp_shade
     // When: removes the sleep xEffect from the party
     ////////////////////////////////////////////////////////////////////////
     public void NTB_RemoveSleepEffects()
     {
          GameObject oPC = GetHero();
          List<GameObject> arParty = GetPartyList(oPC);
          int nSize = GetArraySize(arParty);
          int nIndex;
          GameObject oCurrent;
          List<xEffect> arEffects;
          int nEffectIndex;
          int nArraySize;
          int nEffectID;

          for (nIndex = 0; nIndex < nSize; nIndex++)
          {
               oCurrent = arParty[nIndex];
               ForceSleepEnd(oCurrent);
          }
          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "ntb_functions.nss", "Party not sleeping.");
     }
     ///////////////////////////////////////////////////////////////////////
     // Sent by: ntb330tr_arcane_horror
     // When: sets off the special effects depending on which trigger
     ////////////////////////////////////////////////////////////////////////

     public void NTB_JumpArcaneHorror(string sWP, int nNextPosition, string sNext, int nPosition)
     {
          GameObject oPC = GetHero();
          GameObject oHorror = UT_GetNearestCreatureByTag(oPC, EngineConstants.NTB_CR_ARCANE_HORROR);
          GameObject oModule = GetModule();
          xEffect eGlow = EffectVisualEffect(EngineConstants.NTB_MAGICAL_APPARATUS_GLOW);
          GameObject oWP = UT_GetNearestObjectByTag(oPC, sWP);
          Vector3 lWP = GetLocation(oWP);
          string sJumpTo = "ntb330wp_arcane_horror_" + sNext;

          //This sets up a glow that shows where the Horror is going next
          Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eGlow, lWP, 6.0f);
          //store the new position of the horror
          SetLocalInt(oModule, EngineConstants.NTB_ARCANE_HORROR_POSITION, nNextPosition);

          //Apply teleport xEffect to the horror
          ApplyEffectVisualEffect(oHorror, oHorror, 1005, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);

          //Clear current commands and jump
          WR_ClearAllCommands(oHorror, EngineConstants.TRUE);
          UT_LocalJump(oHorror, sJumpTo, EngineConstants.TRUE);

          LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "ARCANE HORROR - trying to jump to: " + sJumpTo);

          //Fireball cycle in the other areas
          NTB_FireballCycle(nPosition, EngineConstants.DAMAGE_TYPE_ELECTRICITY);
     }

     ///////////////////////////////////////////////////////////////////////
     // Sent by: ntb330tr_arcane_horror
     // When: makes the fireballs go off
     ////////////////////////////////////////////////////////////////////////

     public void NTB_FireballCycle(int nPosition, int nVFX)
     {
          int nCheck;
          int nCheck2;
          GameObject oPC = GetHero();
          for (nCheck = 1; nCheck < 5; nCheck++)
          {
               string sWP = EngineConstants.NTB_WP_ARCANE_HORROR_;
               if (nCheck == nPosition)
               {
                    sWP = sWP + IntToString(nCheck);
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, "ntb_functions_h.nss", "CNM: WP string: " + sWP);
                    GameObject oWP = UT_GetNearestObjectByTag(oPC, sWP);
                    Vector3 lWP = GetLocation(oWP);
                    ExplosionAtLocation(lWP, 1089, 5.0f, 10.0f, EngineConstants.DAMAGE_TYPE_SPIRIT, 5.0f);

               }
          }
     }

     ///////////////////////////////////////////////////////////////////////
     // Sent by: ntb000pt_main and all the area scripts
     // When: removes all werewolves from the area once cured
     ////////////////////////////////////////////////////////////////////////

     public void NTB_RemoveCuredWerewolves(GameObject oPC)
     {
          List<GameObject> arWerewolves = GetObjectsInArea(GetArea(oPC));
          int nObjects = GetArraySize(arWerewolves);
          int nCount;
          GameObject oCurrent;
          string sArea = GetTag(GetArea(oPC));

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, sArea + ".NTB_RemoveCuredWerewolves", "Objects running: " + IntToString(nObjects));
          for (nCount = 0; nCount < nObjects; nCount++)
          {
               oCurrent = arWerewolves[nCount];
               string sTag = GetTag(oCurrent);
               if (GetObjectType(oCurrent) == EngineConstants.OBJECT_TYPE_CREATURE)
               {
                    int nApp = GetAppearanceType(oCurrent);
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, sArea + ".NTB_RemoveCuredWerewolves", sTag + "." + IntToString(nApp));
                    if (nApp == EngineConstants.APR_TYPE_WEREWOLF_A)
                    {
                         if (IsDead(oCurrent) == EngineConstants.FALSE)
                         {
                              if (GetObjectActive(oCurrent) != EngineConstants.FALSE)
                              {
                                   Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, sArea + ".NTB_RemoveCuredWerewolves.nss", sTag + " deactivated.");
                                   WR_SetObjectActive(oCurrent, EngineConstants.FALSE);
                              }
                         }
                    }
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, sArea + ".NTB_RemoveCuredWerewolves", sTag + ": not a creature");
               }
          }
     }

     ///////////////////////////////////////////////////////////////////////
     // Sent by: ntb000pt_main and all the area scripts
     // When: removes all werewolf triggers from the area once cured
     ////////////////////////////////////////////////////////////////////////

     public void NTB_RemoveWolfTriggers(GameObject oPC)
     {
          List<GameObject> arWolfTriggers = GetObjectsInArea(GetArea(oPC));
          int nObjects = GetArraySize(arWolfTriggers);
          int nCount;
          GameObject oCurrent;
          string sArea = GetTag(GetArea(oPC));

          Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, sArea + ".NTB_RemoveCuredWerewolves", "function running");
          for (nCount = 0; nCount < nObjects; nCount++)
          {
               oCurrent = arWolfTriggers[nCount];
               string sTag = GetTag(oCurrent);
               if (GetObjectType(oCurrent) == EngineConstants.OBJECT_TYPE_TRIGGER)
               {
                    string sResRef = GetResRef(oCurrent);
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, sArea + ".NTB_RemoveWolfTriggers", sTag + "." + sResRef);
                    if (sResRef == EngineConstants.NTB_TR_WOLF)
                    {
                         if (GetObjectActive(oCurrent) != EngineConstants.FALSE)
                         {
                              Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, sArea + ".NTB_RemoveWolfTriggers", sTag + " deactivated.");
                              WR_SetObjectActive(oCurrent, EngineConstants.FALSE);
                         }
                    }
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_SYSTEMS, sArea + ".NTB_RemoveWolfTriggers", sTag + ": not a trigger");
               }
          }
     }
}