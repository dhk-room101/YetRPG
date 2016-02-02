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
     //------------------------------------------------------------------------------
     // ability_summon_h
     //------------------------------------------------------------------------------
     /*
         header file containing common function for summoning pets and raising deads,

         Georg:
             This system is very complex and breaks easily if modified.
             Please consult with me before making any changes, regardless how
             minor they are.

             The Summoning System has 3 relevant files:
                 - ability_summon_h (this file)
                 - effect_summon_h  (xEffect definition)
                 - spell_modal      (spellscript)

             Within the system, the order of certain engine instructions is
             absolutely vital. Modifying it will cause malfunction and numerous
             critical engine problems (e.g. adding party members inactive, etc.)

     */
     //------------------------------------------------------------------------------
     // georg zoeller / emmanuel lusinchi
     //------------------------------------------------------------------------------

     //#include"log_h"
     //#include"ability_h"
     //#include"combat_h"
     //#include"2da_data_h"
     //#include"utility_h"
     //#include"sys_autoscale_h"
     //#include"talent_constants_h"
     //#include"spell_constants_h"
     //#include"sys_autolevelup_h"

     /*/ -----------------------------------------------------------------------------
     // @brief Returns if a corpse GameObject (dead creature) is suitable for the
     //        AnimateDead Spell
     // -----------------------------------------------------------------------------*/
     public int _IsAnimatableCorpse(GameObject oCorpse)
     {
          int bIsSuitable = EngineConstants.FALSE;

#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Animate Dead on " + GetTag(oCorpse));
#endif

          if (HasDeathEffect(oCorpse, EngineConstants.TRUE) != EngineConstants.FALSE)
          {
#if DEBUG
               LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Is dead");
#endif

               // is it not a party member
               if (GetFollowerState(oCorpse) == EngineConstants.FOLLOWER_STATE_INVALID)
               {
#if DEBUG
                    LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Not a follower");
#endif

                    // is it not a plot character
                    if (IsPlot(oCorpse) == EngineConstants.FALSE)
                    {
#if DEBUG
                         LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Not plot");
#endif

                         if (GetCanDiePermanently(oCorpse) != EngineConstants.FALSE)
                         {
#if DEBUG
                              LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Can die permanently");
#endif


                              if (IsHumanoid(oCorpse) != EngineConstants.FALSE)
                              {
#if DEBUG
                                   LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "  Humanoid");
#endif

                                   // ignore "spawn_dead" creatures
                                   if (GetLocalInt(oCorpse, EngineConstants.CREATURE_SPAWN_DEAD) != 1)
                                   {
                                        int nAppearance = GetAppearanceType(oCorpse);

                                        // -----------------------------------------------------
                                        // Only allow animating of creature models that have
                                        // the proper bonepile.
                                        // -----------------------------------------------------
                                        string sCorpseModel = GetM2DAString(EngineConstants.TABLE_APPEARANCE, "CorpseModel", nAppearance);
                                        if (StringLowerCase(sCorpseModel) == EngineConstants.ANIMATABLE_BONEPILE)
                                        {
                                             bIsSuitable = EngineConstants.TRUE;
                                        }
                                   }
                              }
                         }
                    }
               }
          }

          return bIsSuitable;
     }

     //moved public const int EngineConstants.SUMMON_TYPE_WOLF = 1;
     //moved public const int EngineConstants.SUMMON_TYPE_BEAR = 2;
     //moved public const int EngineConstants.SUMMON_TYPE_SPIDER = 3;
     //moved public const int EngineConstants.SUMMON_TYPE_ANIMATED_DEAD = 4;

     public int IsCreatureWeapon(GameObject oItem)
     {
          int nItemType = GetBaseItemType(oItem);
          if ((nItemType == 23) || (nItemType == 56) || (nItemType == 57))
          {
               LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "returning EngineConstants.TRUE");
               return EngineConstants.TRUE;
          }
          else
          {
               LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "returning EngineConstants.FALSE");
               return EngineConstants.FALSE;
          }
     }

     // -----------------------------------------------------------------------------
     // @brief Helper to move equipment from bodybag to the new summon
     // -----------------------------------------------------------------------------
     public void _MoveEquipment(GameObject oSummon, GameObject oCorpse)
     {

          // -------------------------------------------------------------------------
          // Since armor does not work on pre-baked appearances, we only move
          // Main, Off and Ammo slots on the new creature.
          //
          // We do never move lootable or stealable items off the bodybag.
          // -------------------------------------------------------------------------

          GameObject oRightHandItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oCorpse);
          GameObject oLeftHandItem = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, oCorpse);
          GameObject oAmmunition = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_RANGEDAMMO, oCorpse);

          // -------------------------------------------------------------------------
          // If a creature weapon is present on a creature, it will always return as
          // the main weapon, regardless of what is actually equipped.
          //
          // Hence, we destroy anything in that slot on the template if we have a
          // weapon we can move over. If not, we leave it in place
          // -------------------------------------------------------------------------

          if (IsObjectValid(oRightHandItem) != EngineConstants.FALSE)
          {
               GameObject oMain = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oSummon);
               DestroyObject(oMain);
          }

          if (IsObjectValid(oRightHandItem) != EngineConstants.FALSE && IsItemDroppable(oRightHandItem) == EngineConstants.FALSE && IsItemStealable(oRightHandItem) == EngineConstants.FALSE && IsCreatureWeapon(oRightHandItem) == EngineConstants.FALSE)
          {
               MoveItem(oCorpse, oSummon, oRightHandItem);
               EquipItem(oSummon, oRightHandItem, EngineConstants.INVENTORY_SLOT_MAIN);
          }

          if (IsObjectValid(oLeftHandItem) != EngineConstants.FALSE && IsItemDroppable(oLeftHandItem) == EngineConstants.FALSE && IsItemStealable(oLeftHandItem) == EngineConstants.FALSE && IsCreatureWeapon(oLeftHandItem) == EngineConstants.FALSE)
          {
               MoveItem(oCorpse, oSummon, oLeftHandItem);
               EquipItem(oSummon, oLeftHandItem, EngineConstants.INVENTORY_SLOT_OFFHAND);
          }

          if (IsObjectValid(oAmmunition) != EngineConstants.FALSE && IsItemDroppable(oAmmunition) == EngineConstants.FALSE && IsItemStealable(oAmmunition) == EngineConstants.FALSE && IsCreatureWeapon(oAmmunition) == EngineConstants.FALSE)
          {
               MoveItem(oCorpse, oSummon, oAmmunition);
               EquipItem(oSummon, oAmmunition, EngineConstants.INVENTORY_SLOT_RANGEDAMMO);
          }
     }

     public void _SetupAnimatedDead(GameObject oCorpse, GameObject oCaster, GameObject oSummon, int nAbility)
     {

          xEffect eTransparent = Effect(EngineConstants.EFFECT_TYPE_ALPHA);
          SetEffectEngineFloatRef(ref eTransparent, EngineConstants.EFFECT_FLOAT_POTENCY, 0.0f);
          Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eTransparent, oSummon, 0.75f, oCaster, nAbility);

          // Play VFX to show the corpse transforming into an animated dead
          Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(EngineConstants.ANIMATE_DEAD_CONSUME_CORPSE_VFX), oCorpse, 3.0f, oCaster, nAbility);
          Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(EngineConstants.ANIMATE_DEAD_NEW_PET_GLOW_VFX), oCorpse, 1.5f, oCaster, nAbility);
          Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(EngineConstants.ANIMATE_DEAD_NEW_PET_GLOW_VFX), oSummon, 1.5f, oCaster, nAbility);

          // -------------------------------------------------------------------------
          // face the summoned creature correctly and give it a "get up" animation
          // -------------------------------------------------------------------------
          float fFacing = GetFacing(oCorpse);
          SetFacing(oSummon, fFacing);

          xCommand cGetUp = CommandPlayAnimation(EngineConstants.ANIMATE_DEAD_GET_UP_ANIMATION, 0, 0, 0);
          AddCommand(oSummon, cGetUp);

          // -------------------------------------------------------------------------
          // Invoke the helper that deals with equipment transfer
          // -------------------------------------------------------------------------
          _MoveEquipment(oSummon, oCorpse);

          GameObject oBag = GetCreatureBodyBag(oCorpse);
#if DEBUG
          LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "    Old Bag = " + GetTag(oBag));
#endif

          // -------------------------------------------------------------------------
          // To guard against race conditions, we need to handle both the case
          // where there there is a bodybag already and the case where there is none.
          // -------------------------------------------------------------------------
          if (IsObjectValid(oBag) != EngineConstants.FALSE)
          {
               // ---------------------------------------------------------------------
               // Force immediate degradation of the bodybag
               // ---------------------------------------------------------------------
               SetBodybagDecayDelay(oBag, 0);
          }
          else
          {

               // ---------------------------------------------------------------------
               // No bodybag exists, so we force once where we can drop in existing
               // loot.
               // ---------------------------------------------------------------------
               SpawnBodyBag(oCorpse, EngineConstants.TRUE);
               oBag = GetCreatureBodyBag(oCorpse);

#if DEBUG
               LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "    New Bag = " + GetTag(oBag));
#endif

               // ---------------------------------------------------------------------
               // If we successfully spawned a bodybag we decay it
               // ---------------------------------------------------------------------
               if (IsObjectValid(oBag) != EngineConstants.FALSE)
               {
                    SetBodybagDecayDelay(oBag, 0);
                    SetObjectInteractive(oCorpse, EngineConstants.FALSE);
                    SetObjectInteractive(oBag, EngineConstants.FALSE);
                    DestroyObject(oBag, 10000);
               }
          }

          /*
          // decay the corpse
          SpawnBodyBag(oCorpse); // Required in case the creature does not have a body bag (i.e. if the creature has no loot)
          GameObject oBodyBag = GetBodyBag(oCorpse);
          SetBodybagDecayDelay(oBodyBag, 5);
          */
     }

     // -----------------------------------------------------------------------------
     // @brief Adjusts Armor, Dex and Armor Penetration on the summon to
     //        compensate for missing armor and often weapons.
     // -----------------------------------------------------------------------------
     public void _AdjustCombatValues(GameObject oSummon, int nSummon, int nLevelToScale)
     {
          float fArmorBase = GetM2DAFloat(EngineConstants.TABLE_SUMMONS, "ArmorBase", nSummon);
          float fArmorBonus = GetM2DAFloat(EngineConstants.TABLE_SUMMONS, "ArmorBonus", nSummon);
          SetCreatureProperty(oSummon, EngineConstants.PROPERTY_ATTRIBUTE_ARMOR, fArmorBase + (fArmorBonus * nLevelToScale));

          if (nSummon != EngineConstants.SUMMON_TYPE_ANIMATED_DEAD)
          {
               float fDexBonus = GetM2DAFloat(EngineConstants.TABLE_SUMMONS, "DexBonus", nSummon) * nLevelToScale;
               UpdateCreatureProperty(oSummon, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, fDexBonus, EngineConstants.PROPERTY_VALUE_MODIFIER);
          }

          float fAPBonus = 1.0f + GetM2DAFloat(EngineConstants.TABLE_SUMMONS, "APBonus", nSummon) * nLevelToScale;
          UpdateCreatureProperty(oSummon, EngineConstants.PROPERTY_ATTRIBUTE_AP, fAPBonus, EngineConstants.PROPERTY_VALUE_MODIFIER);
     }

     // -----------------------------------------------------------------------------
     // @brief Determine which class an animated creature will have based on the
     //        equipment it is wearing
     // -----------------------------------------------------------------------------
     public int _DetermineAnimatedClass(GameObject oSummon)
     {
          // -------------------------------------------------------------
          // By Default, all skeletons are warriors
          // -------------------------------------------------------------
          int nClass = EngineConstants.CLASS_WARRIOR;

          // -------------------------------------------------------------
          // Enemies using staves become mage skeletons
          // -------------------------------------------------------------
          GameObject oMain = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oSummon);

          if (GetBaseItemType(oMain) == EngineConstants.BASE_ITEM_TYPE_STAFF)
          {
               nClass = EngineConstants.CLASS_WIZARD;
          }
          else
          {
               // -------------------------------------------------------------
               // Enemies dual weapons (valid offhand item, no shield)
               // become rogues
               // -------------------------------------------------------------
               GameObject oShield = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, oSummon);

               if (IsObjectValid(oShield) != EngineConstants.FALSE && IsUsingShield(oSummon) == EngineConstants.FALSE)
               {
                    nClass = EngineConstants.CLASS_ROGUE;
               }
          }
          return nClass;
     }

     // -----------------------------------------------------------------------------
     // @brief Animated dead come in several different configurations, this helper
     //        generates their stats and abilities
     // -----------------------------------------------------------------------------
     public void _GenerateAnimatedStatsAndAbilities(GameObject oSummon, int nClass, int nLevelToScale, int bMaster)
     {
          // ------------------------------------------------------------------------
          // First: Wipe all abilities of the creature.
          // ------------------------------------------------------------------------
          CharGen_ClearAbilityList(oSummon, EngineConstants.ABILITY_TYPE_TALENT);
          CharGen_ClearAbilityList(oSummon, EngineConstants.ABILITY_TYPE_SPELL);
          CharGen_ClearAbilityList(oSummon, EngineConstants.ABILITY_TYPE_SKILL);

          // ------------------------------------------------------------------------
          // To limit ability use by summons, they cast at 100% cost markup!
          // ------------------------------------------------------------------------
          UpdateCreatureProperty(oSummon, 41, 100.0f, EngineConstants.PROPERTY_VALUE_MODIFIER);

          // ------------------------------------------------------------------------
          // Warrior:
          //     - Damage Bonus of level /4
          //     - +5 Attack and Defense
          //     - +3 Armor
          // Talents:
          //     - Powerful
          //     - Threaten
          //     - Shield Bash    (if weapon and shield)
          //     - Shield Defense (if weapon and shield)
          //     - RapidShot      (if archer)
          //     - ShatteringSht  (if archer)
          //     - Pommel         (if 2h)
          //     - Sunder Arms    (if 2h)
          // ------------------------------------------------------------------------
          if (nClass == EngineConstants.CLASS_WARRIOR)
          {
               UpdateCreatureProperty(oSummon, EngineConstants.PROPERTY_ATTRIBUTE_ARMOR, 2.5f, EngineConstants.PROPERTY_VALUE_MODIFIER);

               AddAbility(oSummon, EngineConstants.ABILITY_TALENT_HIDDEN_WARRIOR);
               AddAbility(oSummon, EngineConstants.ABILITY_TALENT_POWERFUL);
               AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_THREATEN, 0);

               if (IsUsingShield(oSummon) != EngineConstants.FALSE)
               {
                    AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_SHIELD_BASH, 1);
                    AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_SHIELD_DEFENSE, 2);

                    if (bMaster != EngineConstants.FALSE)
                    {
                         AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_SHIELD_PUMMEL, 2);
                         GameObject oMain = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oSummon);
                         if (IsObjectValid(oMain) != EngineConstants.FALSE)
                         {
                              AddItemProperty(oMain, 6083, 2);
                         }
                    }

               }
               else if (IsUsingRangedWeapon(oSummon) != EngineConstants.FALSE)
               {
                    if (Engine_Random(2) == 1)
                    {
                         AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_RAPIDSHOT, 1);
                         AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_SHATTERING_SHOT, 2);
                    }
                    else
                    {
                         AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_AIM, 1);
                         AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_CRITICAL_SHOT, 2);
                    }

                    if (bMaster != EngineConstants.FALSE)
                    {
                         AddAbilityEx(oSummon, EngineConstants.ABILITY_TALENT_PINNING_SHOT, 3);
                    }

               }
               else if (IsMeleeWeapon2Handed(GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oSummon)) != EngineConstants.FALSE)
               {
                    AddAbilityEx(oSummon, 3025, 1);
                    AddAbilityEx(oSummon, 3024, 2);

                    if (bMaster != EngineConstants.FALSE)
                    {
                         AddAbilityEx(oSummon, 3028, 2);
                         GameObject oMain = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oSummon);
                         if (IsObjectValid(oMain) != EngineConstants.FALSE)
                         {
                              AddItemProperty(oMain, 6083, 3);
                         }
                    }

               }
               else
               {
                    AddAbilityEx(oSummon, 19, 1);
               }

          }
          else if (nClass == EngineConstants.CLASS_WIZARD)
          {

               AddAbility(oSummon, EngineConstants.ABILITY_SPELL_HIDDEN_WIZARD);
               switch (Engine_Random(8))
               {
                    case 0: AddAbilityEx(oSummon, 200254, 0); AddAbilityEx(oSummon, 200255, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 200256, 2); break;
                    case 1: AddAbilityEx(oSummon, 11002, 0); AddAbilityEx(oSummon, 12004, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 10001, 2); break;
                    case 2: AddAbilityEx(oSummon, 14001, 0); AddAbilityEx(oSummon, 14000, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 11002, 2); break;
                    case 3: AddAbilityEx(oSummon, 11112, 0); AddAbilityEx(oSummon, 11115, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 11001, 2); break;
                    case 4: AddAbilityEx(oSummon, 11106, 0); AddAbilityEx(oSummon, 15002, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 11107, 2); break;
                    case 5: AddAbilityEx(oSummon, 10206, 0); AddAbilityEx(oSummon, 10104, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 10200, 2); break;
                    case 6: AddAbilityEx(oSummon, 200254, 0); AddAbilityEx(oSummon, 10704, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 12005, 2); break;
                    case 7: AddAbilityEx(oSummon, 13001, 0); AddAbilityEx(oSummon, 11006, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 11007, 2); break;
                    case 8: AddAbilityEx(oSummon, 10001, 0); AddAbilityEx(oSummon, 11112, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 11111, 2); break;
               }
          }
          else if (nClass == EngineConstants.CLASS_ROGUE)
          {

               UpdateCreatureProperty(oSummon, EngineConstants.PROPERTY_ATTRIBUTE_ARMOR, 2.0f, EngineConstants.PROPERTY_VALUE_MODIFIER);

               AddAbility(oSummon, EngineConstants.ABILITY_TALENT_HIDDEN_ROGUE);
               AddAbility(oSummon, EngineConstants.ABILITY_TALENT_COMBAT_MOVEMENT);
               switch (Engine_Random(5))
               {
                    case 0: AddAbilityEx(oSummon, 3026, 0); AddAbilityEx(oSummon, 708, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 9, 2); break;
                    case 1: AddAbilityEx(oSummon, 603, 0); AddAbilityEx(oSummon, 3044, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 3035, 2); break;
                    case 2: AddAbilityEx(oSummon, 3044, 0); AddAbilityEx(oSummon, 3035, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 6, 2); break;
                    case 3: AddAbilityEx(oSummon, 3026, 0); AddAbilityEx(oSummon, 9, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 10, 2); break;
                    case 4: AddAbilityEx(oSummon, 603, 0); AddAbilityEx(oSummon, 3044, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 5, 2); break;
                    case 5: AddAbilityEx(oSummon, 3026, 0); AddAbilityEx(oSummon, 708, 1); if (bMaster != EngineConstants.FALSE) AddAbilityEx(oSummon, 3060, 2); break;

               }
          }
     }

     public GameObject _GetClosestCorpseToAnimate(GameObject oCaster)
     {
          // -----------------------------------------------------------------
          // Cycle through the corpse objects to determine if there
          // is a suitable string for the spell. If so, find the closest
          // corpse to animate.
          // -----------------------------------------------------------------
          List<GameObject> oTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(oCaster), EngineConstants.ANIMATE_DEAD_AOE_RADIUS);
          int nCount = 0;
          int nMax = GetArraySize(oTargets);
          GameObject oRet = null;

          float fMinDistance = 999.99f;
          for (nCount = 0; nCount < nMax; nCount++)
          {
               GameObject oTarget = oTargets[nCount];

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "CREATURE TYPE", ToString(oTarget) + " TYPE: " + ToString(GetCreatureRacialType(oTarget)));
#endif

               int bSuitableCorpse = _IsAnimatableCorpse(oTarget);
               if (bSuitableCorpse != EngineConstants.FALSE)
               {
                    // use the nearest suitable corpse
                    float fDistance = GetDistanceBetween(oCaster, oTarget);
                    if (fDistance < fMinDistance)
                    {
                         fMinDistance = fDistance;
                         oRet = oTarget;
                    }
               }
          }
          return oRet;
     }

     // Only creatures that have this bodybag model can be raised with animate dead
     //moved public const string ANIMATABLE_BONEPILE = "PLC_BonePile_01_0";

     // This should be called from _ActivateModalAbility. It will summon the right creature
     // based on nAbility and return that creature.
     public GameObject Summon_ActivateModalAbility(GameObject oCaster, int nAbility)
     {

#if DEBUG
          Log_Trace_Spell("Summon_ActivateModalAbility", "activate.", nAbility, null);
#endif

          GameObject oSummon = null;
          int nSummon = 0;
          int nVFX = 0;

          Vector3 lSummonSpot = GetLocation(oCaster);
          GameObject oCorpse = null;

          if (GetAreaFlag(GetArea(oCaster), EngineConstants.AREA_FLAG_IS_FADE) != EngineConstants.FALSE)
          {
               UI_DisplayMessage(oCaster, EngineConstants.UI_MESSAGE_NOT_AT_THIS_LOCATION);
               return null;
          }

          switch (nAbility)
          {
               case EngineConstants.ABILITY_TALENT_NATURE_I_COURAGE_OF_THE_PACK:
                    {
                         nSummon = EngineConstants.SUMMON_TYPE_WOLF;
                         nVFX = Ability_GetImpactLocationVfxId(nAbility);

                         break;
                    }

               case EngineConstants.ABILITY_TALENT_NATURE_II_HARDINESS_OF_THE_BEAR:
                    {
                         nSummon = EngineConstants.SUMMON_TYPE_BEAR;
                         nVFX = Ability_GetImpactLocationVfxId(nAbility);

                         break;
                    }

               case EngineConstants.ABILITY_TALENT_SUMMON_SPIDER:
                    {
                         nSummon = EngineConstants.SUMMON_TYPE_SPIDER;
                         nVFX = 0;

                         break;
                    }

               case EngineConstants.ABILITY_SPELL_ANIMATE_DEAD:
                    {

                         oCorpse = _GetClosestCorpseToAnimate(oCaster);
                         if (IsObjectValid(oCorpse) != EngineConstants.FALSE)
                         {
                              lSummonSpot = GetLocation(oCorpse);
                              nSummon = EngineConstants.SUMMON_TYPE_ANIMATED_DEAD;
                              nVFX = 0;
                         }

                         break;
                    }
          }

          // -------------------------------------------------------------------------
          // If we've found a valid summon, play VFX and summon it
          // -------------------------------------------------------------------------
          if (nSummon > 0)
          {

               int bMaster = EngineConstants.FALSE;
               // ---------------------------------------------------------------------
               // Master Summoning is defined by either having the Master Ranger talent
               // or, for animate dead, running 'spell might' and being a high level blood mage
               // ---------------------------------------------------------------------
               if (nSummon == EngineConstants.SUMMON_TYPE_ANIMATED_DEAD)
               {
                    int nLvl = GetLevel(oCaster);

                    if (IsModalAbilityActive(oCaster, EngineConstants.ABILITY_SPELL_SPELL_MIGHT) != EngineConstants.FALSE)
                    {
                         bMaster = EngineConstants.TRUE;
                    }
               }
               else
               {
                    bMaster = HasAbility(oCaster, EngineConstants.ABILITY_TALENT_MASTER_RANGER);
               }

               string sCol = (bMaster != EngineConstants.FALSE) ? "masterTemplate" : "template";
               string rTemplate = GetM2DAResource(EngineConstants.TABLE_SUMMONS, sCol, nSummon);

#if DEBUG
               Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Summon", ToString(EngineConstants.TABLE_SUMMONS) + " : " + EngineConstants.SUMMON_DATA_TEMPLATE + " : " + ToString(nSummon) + " : " + ResourceToString(rTemplate));
#endif

               // ---------------------------------------------------------------------
               // Read the Summon Abilities from the 2da
               // ---------------------------------------------------------------------
               int nAbility0 = GetM2DAInt(EngineConstants.TABLE_SUMMONS, EngineConstants.SUMMON_DATA_ABILITY_0, nSummon);
               int nAbility1 = GetM2DAInt(EngineConstants.TABLE_SUMMONS, EngineConstants.SUMMON_DATA_ABILITY_1, nSummon);
               int nPassiveAbi0 = GetM2DAInt(EngineConstants.TABLE_SUMMONS, "PassiveAbi0", nSummon);
               int nPassiveAbi1 = GetM2DAInt(EngineConstants.TABLE_SUMMONS, "PassiveAbi1", nSummon);

               // ---------------------------------------------------------------------
               // Here we create the actual object, inactive.
               // ---------------------------------------------------------------------
               oSummon = CreateObject(EngineConstants.OBJECT_TYPE_CREATURE, rTemplate, lSummonSpot, "", EngineConstants.FALSE);
               SetLocalInt(oSummon, EngineConstants.IS_SUMMONED_CREATURE, EngineConstants.TRUE);

               if (IsObjectValid(oSummon) != EngineConstants.FALSE)
               {
                    // -----------------------------------------------------------------
                    // Teleport all summons except animated dead to their new location
                    // within the party formation.
                    //
                    // Since they are still inactive, this happens unbeknownst to the
                    // user.
                    // -----------------------------------------------------------------
                    if (nSummon != EngineConstants.SUMMON_TYPE_ANIMATED_DEAD)
                    {
                         Vector3 lLoc = GetFollowerWouldBeLocation(oSummon);
                         SetLocation(oSummon, lLoc);
                         if (nVFX != EngineConstants.FALSE)
                         {
                              Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(nVFX), lLoc, 0.0f, oCaster, nAbility);
                         }
                    }

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Summon", "summoned creature = " + ToString(oSummon));
#endif

                    // -----------------------------------------------------------------
                    // Animate Dead specific Handling such as equipment management
                    // -----------------------------------------------------------------
                    if (nSummon == EngineConstants.SUMMON_TYPE_ANIMATED_DEAD)
                    {
                         // -------------------------------------------------------------
                         // Deal with moving equipment over from the corpse to the new
                         // summon.
                         // -------------------------------------------------------------
                         _SetupAnimatedDead(oCorpse, oCaster, oSummon, nAbility);

                    }

                    // ================================================================
                    // Important: The order of instructions is of absolute importance
                    // in the following section.
                    // -----------------------------------------------------------------

                    // -----------------------------------------------------------------
                    // Calculate the level to scale to
                    // -----------------------------------------------------------------
                    float fScale = bMaster != EngineConstants.FALSE ? 0.9f : 0.75f;
                    int nLevel = FloatToInt(GetLevel(oCaster) * fScale);
                    int nLevelToScale = Max(1, nLevel);       //AS_GetCreatureLevelToScale(oSummon,Max(1,GetLevel(oCaster) -1)) + (bMaster?2:0);
                    int nClass = 0;

                    // -----------------------------------------------------------------
                    // Now, we scale the creature.
                    //   1. Force EngineConstants.XP to 0, which triggers a stat reset in AS_Init.
                    //   2. For animate dead, force a class to be set on the target.
                    // -----------------------------------------------------------------
                    SetCreatureProperty(oSummon, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE, 0.0f);

                    if (nSummon == EngineConstants.SUMMON_TYPE_ANIMATED_DEAD)
                    {
                         nClass = _DetermineAnimatedClass(oSummon);
                         SetCreatureProperty(oSummon, EngineConstants.PROPERTY_SIMPLE_CURRENT_CLASS, IntToFloat(nClass));
                    }

                    // -----------------------------------------------------------------
                    // Force summon to 'Normal' rank and initialize them with an
                    // override class
                    // -----------------------------------------------------------------
                    SetCreatureRank(oSummon, EngineConstants.CREATURE_RANK_NORMAL);
                    AS_InitCreature(oSummon, nLevelToScale, EngineConstants.FALSE, nClass);

                    // -----------------------------------------------------------------
                    // Activate the summon and add it to the active party.
                    // This has to happen AFTER scaling to avoid it being
                    // rescaled and messed with by Yaron's follower catchup code.
                    // -----------------------------------------------------------------
                    WR_SetObjectActive(oSummon, EngineConstants.TRUE);
                    WR_SetFollowerState(oSummon, EngineConstants.FOLLOWER_STATE_ACTIVE);

                    // -----------------------------------------------------------------
                    // PrxEvent it from accessing the levelup UI and gaining EngineConstants.XP.
                    // -----------------------------------------------------------------
                    SetCanLevelUp(oSummon, EngineConstants.FALSE);
                    SetLocalInt(oSummon, EngineConstants.CREATURE_REWARD_FLAGS, 1);

                    // -----------------------------------------------------------------
                    // Scale up combat relevant values on top of the already existing
                    // creature stats
                    // -----------------------------------------------------------------
                    _AdjustCombatValues(oSummon, nSummon, nLevelToScale);

                    // -----------------------------------------------------------------
                    // For animated skeletons, generate a random talent configuration
                    // -----------------------------------------------------------------
                    if (nSummon == EngineConstants.SUMMON_TYPE_ANIMATED_DEAD && nClass > 0)
                    {
                         _GenerateAnimatedStatsAndAbilities(oSummon, nClass, nLevelToScale, bMaster);
                    }
                    // -----------------------------------------------------------------
                    // For normal summons, consult the 2da
                    // -----------------------------------------------------------------
                    else
                    {
                         if (nAbility0 != EngineConstants.FALSE)
                         {
                              AddAbilityEx(oSummon, nAbility0, 0);
                         }

                         // -------------------------------------------------------------
                         // 2nd 2da ability is only for masters
                         // -------------------------------------------------------------
                         if (nAbility1 != EngineConstants.FALSE && bMaster != EngineConstants.FALSE)
                         {
                              AddAbilityEx(oSummon, nAbility1, 1);
                         }
                    }

                    if (nPassiveAbi0 != EngineConstants.FALSE)
                    {
                         AddAbility(oSummon, nPassiveAbi0);
                    }

                    if (nPassiveAbi1 != EngineConstants.FALSE)
                    {
                         AddAbility(oSummon, nPassiveAbi1);
                    }

                    // -----------------------------------------------------------------
                    // add locked equipment and quickbar for summoned creature
                    // to prevent inventory or quickbar modification
                    // -----------------------------------------------------------------
                    xEffect eSummon = Effect(EngineConstants.EFFECT_TYPE_LOCK_CHARACTER);
                    ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eSummon, oSummon, 0.0f, oCaster, 0);

                    // -----------------------------------------------------------------
                    // natural regeneration bonus for ranger's pets
                    // -----------------------------------------------------------------
                    if (bMaster != EngineConstants.FALSE && (nSummon != EngineConstants.SUMMON_TYPE_ANIMATED_DEAD))
                    {
                         xEffect eEffect = EffectModifyProperty(EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_HEALTH_COMBAT, EngineConstants.NATURAL_REGENERATION_HEALTH_REGENERATION_BONUS);
                         SetEffectEngineIntegerRef(ref eEffect, EngineConstants.EFFECT_INTEGER_VFX, Ability_GetImpactObjectVfxId(EngineConstants.ABILITY_TALENT_MASTER_RANGER));
                         ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, eEffect, oSummon, 0.0f, oCaster, nAbility);
                    }
               }
               else
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Summon", "Invalid summoned creature");
               }
          }
          return oSummon;
     }

     // Play effects during spell cast
     public void Summon_EventSpellscriptCast(GameObject oCaster, int nAbility)
     {
          switch (nAbility)
          {
               case EngineConstants.ABILITY_SPELL_ANIMATE_DEAD:
                    {

                         Log_Trace_Spell("Summon_EventSpellscriptCast", "generate AOE xEffect for.", nAbility, null);

                         // cycle through potential corpses objects and display VFX on them
                         List<GameObject> oTargets = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(oCaster), EngineConstants.ANIMATE_DEAD_AOE_RADIUS);
                         int nCount = 0;
                         int nMax = GetArraySize(oTargets);
                         for (nCount = 0; nCount < nMax; nCount++)
                         {

                              GameObject oTarget = oTargets[nCount];
                              int bSuitableCorpse = _IsAnimatableCorpse(oTarget);
                              if (bSuitableCorpse != EngineConstants.FALSE)
                              {
                                   // good corpse type. Give some visual feedback that the creature corpse was "suitable"
                                   Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(EngineConstants.ANIMATE_RIGHT_CORPSE_GLOW_VFX), oTarget, 3.0f, oCaster, nAbility);
                              }
                              else
                              {
                                   // wrong corpse type. Give some visual feedback that the creature corpse was not suitable (e.g. Henchman's corpse)
                                   Engine_ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(EngineConstants.ANIMATE_WRONG_CORPSE_GLOW_VFX), oTarget, 3.0f, oCaster, nAbility);
                              }
                         }
                         break;
                    }

          }

     }
}