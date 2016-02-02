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
     // -----------------------------------------------------------------------------------------------------------------------------------------------------
     // effect_wbomb_h
     // -----------------------------------------------------------------------------
     /*
         Effect: Walking Bomb

     */
     // -----------------------------------------------------------------------------
     // Owner: Georg Zoeller
     // -----------------------------------------------------------------------------

     //#include"log_h"
     //#include"core_h"
     //#include"effect_constants_h"
     //#include"effect_damage_h"
     //#include"2da_data_h"
     //#include"effect_visualeffect_h"

     //moved public const int EngineConstants.WALKING_BOMB_VFX = 90169;
     //moved public const float EngineConstants.WALKING_BOMB_HEALTH_DEGENERATION = 15.0f;
     //moved public const float EngineConstants.WALKING_BOMB_DAMAGE_START = 1.5f;
     //moved public const float EngineConstants.WALKING_BOMB_DAMAGE_RADIUS = 5.0f;
     //moved public const float EngineConstants.WALKING_BOMB_INFECTION_RADIUS = 3.0f;
     //moved public const float EngineConstants.WALKING_BOMB_INFECTION_CHANCE = 0.5f;
     //moved public const float EngineConstants.WALKING_BOMB_FRIENDLY_MODIFIER = 0.5f;
     //moved public const int EngineConstants.WALKING_BOMB_INFECTION_LIMIT = 3;
     //moved public const string EngineConstants.WALKING_BOMB_SCRIPT = "spell_singletarget";
     //moved public const float EngineConstants.WALKING_BOMB_EVENT_DELAY = 0.1f;

     public xEffect EffectWalkingBomb(GameObject oOwner, int bVirulent = EngineConstants.FALSE, float fDamage = 0.0f, int nVFX = 90169)
     {
          xEffect eEffect = Effect(EngineConstants.EFFECT_TYPE_WALKING_BOMB);
          SetEffectObjectRef(ref eEffect, 0, oOwner);
          SetEffectFloatRef(ref eEffect, 0, GetCreatureSpellPower(oOwner));
          SetEffectIntegerRef(ref eEffect, 0, bVirulent);
          SetEffectFloatRef(ref eEffect, 1, fDamage);
          SetEffectIntegerRef(ref eEffect, 1, nVFX);

          return eEffect;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the application of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleApplyEffectWalkingBomb(xEffect eEffect)
     {
          return EngineConstants.TRUE;
     }

     // -----------------------------------------------------------------------------
     // This utility function handles the removal of the xEffect and
     // should never be called directly except in Effects_h
     // -----------------------------------------------------------------------------
     public int Effects_HandleRemoveEffectWalkingBomb(xEffect eEffect)
     {
          LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "Removing walking bomb.");

          // if this ability is being removed from a dying/dead creature
          if ((IsDeadOrDying(gameObject) != EngineConstants.FALSE) || (GetHasEffects(gameObject, EngineConstants.EFFECT_TYPE_DEATH) != EngineConstants.FALSE) || (GetCreatureFlag(gameObject, EngineConstants.CREATURE_RULES_FLAG_DYING) != EngineConstants.FALSE))
          {
               LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "  Creature is dead or dying.");

               float fDamage = GetEffectFloatRef(ref eEffect, 1);
               int nVFX = GetEffectIntegerRef(ref eEffect, 1);
               xEffect eVFX = EffectVisualEffect(nVFX);
               //        ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eVFX, gameObject, 0.0f, gameObject, EngineConstants.ABILITY_SPELL_WALKING_BOMB);
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(nVFX), GetLocation(gameObject), 0.0f, gameObject, EngineConstants.ABILITY_SPELL_WALKING_BOMB);

               float fSpellPower = GetEffectFloatRef(ref eEffect, 0);
               LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "    Initial Damage = " + ToString(fDamage));
               LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "    SpellPower = " + ToString(fSpellPower));
               if (fDamage == 0.0f)
               {
                    fDamage = GetAutoScaleDataFloat(GetCreatureRank(gameObject), EngineConstants.AS_RANK_SCALE_FACTOR) * (fSpellPower / 2.0f);
               }
               LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "    Final Damage = " + ToString(fDamage));
               GameObject oCaster = GetEffectObjectRef(ref eEffect, 0);
               int bViral = GetEffectIntegerRef(ref eEffect, 0);

               List<GameObject> oVictims = GetNearestObject(gameObject, EngineConstants.OBJECT_TYPE_CREATURE, 20, 1, 0, 0); //GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(gameObject), EngineConstants.WALKING_BOMB_DAMAGE_RADIUS);
               int nSize = GetArraySize(oVictims);
               LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "    Number of Victims = " + ToString(nSize));

               int i;
               GameObject oVictim;

               xEffect eViral = EffectWalkingBomb(oCaster, bViral, fDamage, nVFX);
               int nAbilityId = GetEffectAbilityIDRef(ref eEffect);
               float fDamageInst;
               int bHostile;
               float fDistance;
               int nViral = 0;

               // cycle through blast victims
               for (i = 0; i < nSize; i++)
               {
                    oVictim = oVictims[i];
                    LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "      Victim " + ToString(i) + " " + GetTag(oVictim));

                    // not self
                    if (oVictim != gameObject)
                    {
                         LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Not self.");

                         // if the victim is not also dead or dying
                         if ((IsDeadOrDying(oVictim) == EngineConstants.FALSE) && (GetHasEffects(oVictim, EngineConstants.EFFECT_TYPE_DEATH) == EngineConstants.FALSE) && (GetCreatureFlag(oVictim, EngineConstants.CREATURE_RULES_FLAG_DYING) == EngineConstants.FALSE))
                         {
                              LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Not dead or dying.");

                              // if the caster still lives and is hostile
                              if (IsDead(oCaster) == EngineConstants.FALSE)
                              {
                                   LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Caster living.");

                                   // get distance
                                   fDistance = GetDistanceBetween(gameObject, oVictim);
                                   LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Distance = " + ToString(fDistance));

                                   // if within infection range
                                   if (fDistance <= EngineConstants.WALKING_BOMB_DAMAGE_RADIUS)
                                   {
                                        LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Within range.");

                                        bHostile = IsObjectHostile(oVictim, oCaster);
                                        if ((fDistance <= EngineConstants.WALKING_BOMB_INFECTION_RADIUS) && (bHostile != EngineConstants.FALSE))
                                        {
                                             LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Within infection range and hostile.");

                                             // if the target isn't already a walking bomb
                                             if (GetHasEffects(oVictim, EngineConstants.EFFECT_TYPE_WALKING_BOMB, nAbilityId) == EngineConstants.FALSE)
                                             {
                                                  LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        No bomb effects.");

                                                  // if the current walking bomb is viral
                                                  if (bViral != EngineConstants.FALSE)
                                                  {
                                                       LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Checking Viral.");

                                                       if ((RandomFloat() < EngineConstants.WALKING_BOMB_INFECTION_CHANCE) && (nViral < EngineConstants.WALKING_BOMB_INFECTION_LIMIT))
                                                       {
                                                            LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Adding Viral " + ToString(nViral));
                                                            nViral++;

                                                            xEvent ev = Event(EngineConstants.EVENT_TYPE_SPELLSCRIPT_IMPACT);
                                                            SetEventIntegerRef(ref ev, 0, nAbilityId);
                                                            SetEventObjectRef(ref ev, 0, oCaster);
                                                            SetEventObjectRef(ref ev, 1, oVictim);

                                                            DelayEvent(EngineConstants.WALKING_BOMB_EVENT_DELAY, oVictim, ev, EngineConstants.WALKING_BOMB_SCRIPT);

                                                       }
                                                  }
                                             }
                                        }

                                        // if creature is hostile
                                        if (bHostile != EngineConstants.FALSE)
                                        {
                                             LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Hostile.");

                                             fDamageInst = fDamage;
                                        }
                                        else
                                        {
                                             fDamageInst = fDamage * EngineConstants.WALKING_BOMB_FRIENDLY_MODIFIER;
                                        }
                                        LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Instance Start Damage = " + ToString(fDamageInst));

                                        // modified by distance
                                        if (fDistance > EngineConstants.WALKING_BOMB_DAMAGE_START)
                                        {
                                             fDamageInst *= (1.0f - ((fDistance - EngineConstants.WALKING_BOMB_DAMAGE_START) / (EngineConstants.WALKING_BOMB_DAMAGE_RADIUS - EngineConstants.WALKING_BOMB_DAMAGE_START)));
                                        }
                                        LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "        Instance Final Damage = " + ToString(fDamageInst));

                                        if (fDamageInst > 0.0f)
                                        {
                                             Effects_ApplyInstantEffectDamage(oVictim, oCaster, fDamageInst, EngineConstants.DAMAGE_TYPE_PHYSICAL, EngineConstants.DAMAGE_EFFECT_FLAG_UPDATE_GORE, nAbilityId);
                                        }
                                   }
                                   else
                                   {
                                        i = nSize;
                                   }
                              }
                         }
                    }
               }
          }

          return EngineConstants.TRUE;
     }

     public void Effect_DoOnDeathExplosion(GameObject oExploder, int bDamageFriendly = EngineConstants.FALSE, float fRadius = 3.75f, int nVfx = EngineConstants.VFX_CRUST_WBOMB, int nDamageType = EngineConstants.DAMAGE_TYPE_SPIRIT, float fScale = 0.0f, float fDamageFactor = 0.075f)
     {
          List<GameObject> oVictims = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_SPHERE, GetLocation(oExploder), fRadius);
          int nSize = GetArraySize(oVictims);
          int i;
          GameObject oVictim;
          float fDamage = (GetMaxHealth(oExploder) * fDamageFactor);
          int bHostile;

          xEffect eVFX = EffectVisualEffect(nVfx);
          if (fScale > 0.0f)
          {
                SetEffectEngineFloatRef(ref eVFX, EngineConstants.EFFECT_FLOAT_SCALE, fScale);
          }
          // Apply the VFX.
          Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, eVFX, GetLocation(oExploder), 0.0f, oExploder);

          for (i = 0; i < nSize && i < 5 /*limit to 5 victims tops*/; i++)
          {
               oVictim = oVictims[i];
               bHostile = IsObjectHostile(oVictim, oExploder);

               if (bHostile != EngineConstants.FALSE || bDamageFriendly != EngineConstants.FALSE)
               {
                    if (IsDead(oVictim) == EngineConstants.FALSE && IsDying(oVictim) == EngineConstants.FALSE)
                    {
                         Effects_ApplyInstantEffectDamage(oVictim, oExploder, fDamage, nDamageType, EngineConstants.DAMAGE_EFFECT_FLAG_UPDATE_GORE, 0);
                    }
               }
          }

     }
}