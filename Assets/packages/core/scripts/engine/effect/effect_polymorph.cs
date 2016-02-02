//ready
//double check the ` syntax
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
     // -----------------------------------------------------------------------------------------------------------------------------------------------------
     // xEffect_Shapechange_h
     // -----------------------------------------------------------------------------
     /*
         Effect: Shapechange

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------


     //#include "log_h"
     //#include "core_h"
     //#include "effect_constants_h"
     //#include "effect_death_h"
     //#include "2da_data_h"
     //#include "wrappers_h"
     //#include "effect_upkeep_h"




     public xEffect EffectShapechange(int nType, GameObject oTarget)
     {
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Shapechange create xEffect");

          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_SHAPECHANGE);


          int nShape = 0;
          int bMaster = HasAbility(oTarget, EngineConstants.ABILITY_SPELL_SHAPESHIFTER);

          if (bMaster != EngineConstants.FALSE)
          {
               nShape = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "UpgradeApr", nType);
          }
          else
          {
               nShape = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "Appearance", nType);
          }

          int bPlotShape = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "IsPlotShape", nType);

          int nAbi1 = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "Abi1", nType);
          int nAbi2 = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "Abi2", nType);
          int nAbi3 = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "Abi3", nType);
          int nAbi4 = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "PassiveTalent", nType);
          int nAbi5 = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "PassiveTalent2", nType);
          int nCrust = GetM2DAInt(EngineConstants.TABLE_SHAPECHANGE, "Crust", nType);


          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi1 = " + ToString(nAbi1));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi2 = " + ToString(nAbi2));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi3 = " + ToString(nAbi3));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi4 = " + ToString(nAbi4));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi5 = " + ToString(nAbi5));

          SetEffectIntegerRef(ref eEffect, 0, nShape);
          SetEffectIntegerRef(ref eEffect, 1, nCrust);
        SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_VFX, nCrust);
          int nAbiCount = 2;

          // -------------------------------------------------------------------------
          // Protect the player from losing abilities he already has...
          // -------------------------------------------------------------------------

          // if the shape has an ability
          if (nAbi1 > 0)
          {
               // if the player doesn't already have the ability
               if (HasAbility(oTarget, nAbi1) == EngineConstants.FALSE)
               {
                    SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi1);
               }
               else
               {
                    // negative ability ID means the character already has it (for quickslotting)
                    SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi1 * -1);
               }

               nAbiCount++;
          }

          // if the shape has an ability
          if (nAbi2 > 0)
          {
               // if the player doesn't already have the ability
               if (HasAbility(oTarget, nAbi2) == EngineConstants.FALSE)
               {
                    SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi2);
               }
               else
               {
                    // negative ability ID means the character already has it (for quickslotting)
                    SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi2 * -1);
               }

               nAbiCount++;
          }

          // if the shape has an ability
          if (nAbi3 > 0)
          {
               // ability 3 is limited to shape mastery and plot forms
               if (bMaster != EngineConstants.FALSE || bPlotShape != EngineConstants.FALSE)
               {
                    // if the player doesn't already have the ability
                    if (HasAbility(oTarget, nAbi3) == EngineConstants.FALSE)
                    {
                         SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi3);
                    }
                    else
                    {
                         // negative ability ID means the character already has it (for quickslotting)
                         SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi3 * -1);
                    }

                    nAbiCount++;
               }
          }

          // if the shape has an ability
          if (nAbi4 > 0)
          {
               // if the player doesn't already have the ability
               if (HasAbility(oTarget, nAbi4) == EngineConstants.FALSE)
               {
                    SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi4);
               }
               else
               {
                    // negative ability ID means the character already has it (for quickslotting)
                    SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi4 * -1);
               }

               nAbiCount++;
          }

          // if the shape has an ability
          if (nAbi5 > 0)
          {
               // if the player doesn't already have the ability
               if (HasAbility(oTarget, nAbi5) == EngineConstants.FALSE)
               {
                    SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi5);
               }
               else
               {
                    // negative ability ID means the character already has it (for quickslotting)
                    SetEffectIntegerRef(ref eEffect, nAbiCount, nAbi5 * -1);
               }

               nAbiCount++;
          }

          return eEffect;
     }


     public void _NotifyAreaOfShapeshift(int bShifted, int nShape)
     {
          xEvent eNotify = Event(EngineConstants.EVENT_TYPE_CREATURE_SHAPESHIFTED);
          SetEventCreatorRef(ref eNotify, gameObject);
          SetEventIntegerRef(ref eNotify, 0, bShifted);
          SetEventIntegerRef(ref eNotify, 1, nShape);
          DelayEvent(0.0f, GetArea(gameObject), eNotify);
     }


     // -----------------------------------------------------------------------------
     // This utility function handles the application of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectShapechange(xEffect eEffect)
     {
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Shapechange apply xEffect");

          int nShape = GetEffectIntegerRef(ref eEffect, 0);
          int nCrust = GetEffectIntegerRef(ref eEffect, 1);
          int nAbi1 = GetEffectIntegerRef(ref eEffect, 2);
          int nAbi2 = GetEffectIntegerRef(ref eEffect, 3);
          int nAbi3 = GetEffectIntegerRef(ref eEffect, 4);
          int nAbi4 = GetEffectIntegerRef(ref eEffect, 5);
          int nAbi5 = GetEffectIntegerRef(ref eEffect, 6);

          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi1 = " + ToString(nAbi1));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi2 = " + ToString(nAbi2));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi3 = " + ToString(nAbi3));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi4 = " + ToString(nAbi4));
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Abi5 = " + ToString(nAbi5));

          SetAppearanceType(gameObject, nShape);

          // -------------------------------------------------------------------------
          // Switch to a seperate quickbar
          // -------------------------------------------------------------------------
          SetQuickslotBar(gameObject, 1);

          // -------------------------------------------------------------------------
          // Mapping the polymorph ability onto quickbar slot 0 to allow the player
          // to actually deactivate it again...
          // -------------------------------------------------------------------------
          int nId = GetEffectAbilityIDRef(ref eEffect);
          SetQuickslot(gameObject, 0, nId);

          // reset quickbar
          SetQuickslot(gameObject, 1, 0);
          SetQuickslot(gameObject, 2, 0);
          SetQuickslot(gameObject, 3, 0);
          SetQuickslot(gameObject, 4, 0);
          SetQuickslot(gameObject, 5, 0);

          // -------------------------------------------------------------------------
          // Now map the creature abilities
          // -------------------------------------------------------------------------
          if (nAbi1 != EngineConstants.FALSE)
          {
               if (nAbi1 > 0)
               {
                    AddAbilityEx(gameObject, nAbi1, 1);
               }
               else
               {
                    if (IsFollower(gameObject) != EngineConstants.FALSE)
                    {
                         SetQuickslot(gameObject, 1, nAbi1 * -1);
                    }
               }
          }

          if (nAbi2 != EngineConstants.FALSE)
          {
               if (nAbi2 > 0)
               {
                    AddAbilityEx(gameObject, nAbi2, 2);
               }
               else
               {
                    if (IsFollower(gameObject) != EngineConstants.FALSE)
                    {
                         SetQuickslot(gameObject, 2, nAbi2 * -1);
                    }
               }
          }

          if (nAbi3 != EngineConstants.FALSE)
          {
               if (nAbi3 > 0)
               {
                    AddAbilityEx(gameObject, nAbi3, 3);
               }
               else
               {
                    if (IsFollower(gameObject) != EngineConstants.FALSE)
                    {
                         SetQuickslot(gameObject, 3, nAbi3 * -1);
                    }
               }
          }

          if (nAbi4 != EngineConstants.FALSE)
          {
               if (nAbi4 > 0)
               {
                    AddAbilityEx(gameObject, nAbi4, 4);
               }
               else
               {
                    if (IsFollower(gameObject) != EngineConstants.FALSE)
                    {
                         SetQuickslot(gameObject, 4, nAbi4 * -1);
                    }
               }
          }

          if (nAbi5 != EngineConstants.FALSE)
          {
               if (nAbi5 > 0)
               {
                    AddAbilityEx(gameObject, nAbi5, 5);
               }
               else
               {
                    if (IsFollower(gameObject) != EngineConstants.FALSE)
                    {
                         SetQuickslot(gameObject, 5, nAbi5 * -1);
                    }
               }
          }

          switch (nId)
          {
               case EngineConstants.ABILITY_SPELL_BEAR:
                    {
                         break;
                    }
               case EngineConstants.ABILITY_SPELL_SPIDER_SHAPE:
                    {
                         break;
                    }
               case EngineConstants.ABILITY_SPELL_FLYING_SWARM:
                    {
                         //Apply any xEffects that could not be handled via upkeep
                         //SetCreatureIsGhost(gameObject,  EngineConstants.TRUE);

                         break;
                    }
          }

          _NotifyAreaOfShapeshift(EngineConstants.TRUE, nShape);

          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectShapechange(xEffect eEffect)
     {

          SetAppearanceType(gameObject, -1);
          int nShape = GetEffectIntegerRef(ref eEffect, 0);
          int nAbi1 = GetEffectIntegerRef(ref eEffect, 2);
          int nAbi2 = GetEffectIntegerRef(ref eEffect, 3);
          int nAbi3 = GetEffectIntegerRef(ref eEffect, 4);
          int nAbi4 = GetEffectIntegerRef(ref eEffect, 5);
          int nAbi5 = GetEffectIntegerRef(ref eEffect, 6);



          if (nAbi1 > 0)
               RemoveAbility(gameObject, nAbi1);

          if (nAbi2 > 0)
               RemoveAbility(gameObject, nAbi2);

          if (nAbi3 > 0)
               RemoveAbility(gameObject, nAbi3);

          if (nAbi4 > 0)
               RemoveAbility(gameObject, nAbi4);

          if (nAbi5 > 0)
               RemoveAbility(gameObject, nAbi5);

          SetQuickslotBar(gameObject, 0);

          int nId = GetEffectAbilityIDRef(ref eEffect);
          switch (nId)
          {
               case EngineConstants.ABILITY_SPELL_BEAR:
                    {
                         break;
                    }
               case EngineConstants.ABILITY_SPELL_SPIDER_SHAPE:
                    {
                         break;
                    }
               case EngineConstants.ABILITY_SPELL_FLYING_SWARM:
                    {
                         //Remove any xEffects that could not be handled via upkeep
                         //SetCreatureIsGhost(gameObject,  EngineConstants.FALSE);
                         break;
                    }
          }

          _NotifyAreaOfShapeshift(EngineConstants.FALSE, nShape);


          return EngineConstants.TRUE;
     }
}