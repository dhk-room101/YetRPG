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
     // Resistance System
     //------------------------------------------------------------------------------
     /*
         Low level script for resistance checks, included in Effect_Damage
         and ability_h

     */
     //------------------------------------------------------------------------------
     // georg zoeller
     //------------------------------------------------------------------------------

     //#include"core_h"
     //#include"ui_h"

     //moved public const int EngineConstants.RESISTANCE_MENTAL     = EngineConstants.PROPERTY_ATTRIBUTE_RESISTANCE_MENTAL;
     //moved public const int EngineConstants.RESISTANCE_PHYSICAL   = EngineConstants.PROPERTY_ATTRIBUTE_RESISTANCE_PHYSICAL;
     //moved public const int EngineConstants.RESISTANCE_INVALID = 0;

     //moved public const int EngineConstants.DAMAGE_RESISTANCE_FIRE        = EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_FIRE;
     //moved public const int EngineConstants.DAMAGE_RESISTANCE_COLD        = EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_COLD;
     //moved public const int EngineConstants.DAMAGE_RESISTANCE_ELECTRICITY = EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_ELEC;
     //moved public const int EngineConstants.DAMAGE_RESISTANCE_NATURE      = EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_NATURE;
     //moved public const int EngineConstants.DAMAGE_RESISTANCE_SPIRIT      = EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_RESISTANCE_SPIRIT;

     //moved public const int EngineConstants.PROPERTY_ATTRIBUTE_SPELLRESISTANCE = 52;

     //moved public const int EngineConstants.CLASS_MONSTER_SPELLCASTER = 25;
     // -----------------------------------------------------------------------------
     // Spell resistance is 'ignore hostile magic'
     // -----------------------------------------------------------------------------
     public int CheckSpellResistance(GameObject oTarget, GameObject oCaster, int nAbility)
     {

          if (IsObjectValid(oCaster) == EngineConstants.FALSE)
          {
               return EngineConstants.FALSE;
          }

          int nType = GetAbilityType(nAbility);

          // Only resist spells or talents originating from monster spellcasters.
          if (nType != EngineConstants.ABILITY_TYPE_SPELL)
          {
               int nClass = GetCreatureCoreClass(oCaster);
               if (nClass != EngineConstants.CLASS_MONSTER_SPELLCASTER && nClass != EngineConstants.CLASS_WIZARD)
               {
                    return EngineConstants.FALSE;
               }
               else
               {
                    if (nType != EngineConstants.ABILITY_TYPE_TALENT)
                    {
                         return EngineConstants.FALSE;
                    }
               }

          }

          // Only resist spells comnig from hostile targets.
          if (IsObjectHostile(oCaster, oTarget) != EngineConstants.FALSE)
          {
               float fResist = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_ATTRIBUTE_SPELLRESISTANCE);

               // -----------------------------------------------------------------
               // Game difficulty scales Spell resistance too.
               // -----------------------------------------------------------------
               fResist += Diff_GetSRMod(oTarget);

               float fCheck = RandomFloat() * 100.0f;

               // -----------------------------------------------------------------
               // actual resistance check
               // -----------------------------------------------------------------
               if (fCheck < fResist)
               {
                    // -------------------------------------------------------------
                    // SpellShield:
                    // Since spellshield increases magic resistance, we just need
                    // to check here if it active...
                    // -------------------------------------------------------------
                    if (IsModalAbilityActive(oTarget, EngineConstants.ABILITY_SPELL_SPELL_SHIELD) != EngineConstants.FALSE)
                    {
                         // ---------------------------------------------------------
                         // ... and if it contributed to resisting the spell,...
                         // ---------------------------------------------------------
                         if (fCheck < 75.0f /* spellshield effectiveness */)
                         {
                              // ----------------------------------------------------
                              // we determine the cost of absorbing it to the users
                              // mana supply (10+ 2da cost) and...
                              // ----------------------------------------------------
                              int nAbilityType = Ability_GetAbilityType(nAbility);
                              float fCost = (GetM2DAFloat(EngineConstants.TABLE_ABILITIES_SPELLS, "Cost", nAbility) + 10.0f) * -1;
                              float fCurrentMana = GetCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT);

                              // ----------------------------------------------------
                              // ... subtract that amount of mana from the users mana...
                              // ----------------------------------------------------
                              UpdateCreatureProperty(oTarget, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fCost, EngineConstants.PROPERTY_VALUE_CURRENT);

                              // ----------------------------------------------------
                              // ... if the cost exceeded the available mana, the
                              // spell is not resisted.
                              //
                              // Note: Mana reaching 0 causes the oom xEvent to fire
                              // on the creature, which will shut down spellshield
                              // as it has flag 0x40 - end on oom set.
                              // ----------------------------------------------------
                              if (fCost > fCurrentMana)
                              {
                                   return EngineConstants.FALSE;
                              }
                         }
                    }
                    return EngineConstants.TRUE;
               }
          }

          return EngineConstants.FALSE;
     }

     public float GetResistanceCutoff()
     {
          return EngineConstants.RESISTANCE_CUTOFF;

     }

     public int DamageIsImmuneToType(GameObject oTarget, int nDamageType)
     {

          int nAbility = GetM2DAInt(EngineConstants.TABLE_DAMAGETYPES, "Immunity", nDamageType);
          if (nAbility != EngineConstants.FALSE)
               return HasAbility(oTarget, nAbility);

          return EngineConstants.FALSE;
     }

     public float ResistDamage(GameObject oAttacker, GameObject oTarget, int nAbility, float fDamageRoll, int nDamageType)
     {

          int nResistance = 0;

          // -------------------------------------------------------------------------
          // Damage below 1.0f is always reduced to 0.
          // -------------------------------------------------------------------------
          if (fDamageRoll < 1.0f)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistDamage", ToString(oTarget) + " DAMAGE ZEROED" + ToString(fDamageRoll) + " dmg by " + ToString(oAttacker) + " type " + ToString(nDamageType) + " because damage was less than 1.0");

               return 0.0f;
          }

          if (GetObjectType(oTarget) != EngineConstants.OBJECT_TYPE_CREATURE)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistDamage", ToString(oTarget) + " NOT RESISTING " + ToString(fDamageRoll) + " dmg by " + ToString(oAttacker) + " type " + ToString(nDamageType) + " because we are not a creature");

               return fDamageRoll;
          }

          // -------------------------------------------------------------------------
          // Plot damage is unresistable.
          // -------------------------------------------------------------------------
          if (nDamageType == EngineConstants.DAMAGE_TYPE_PLOT || nDamageType == EngineConstants.DAMAGE_TYPE_DOT)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistDamage", ToString(oTarget) + " NOT RESISTING " + ToString(fDamageRoll) + " dmg by " + ToString(oAttacker) + " type PLOT/DOT");
               return fDamageRoll;
          }
          else if (nDamageType == EngineConstants.DAMAGE_TYPE_PHYSICAL)
          {

               return fDamageRoll;
          }
          else if (nDamageType == EngineConstants.DAMAGE_TYPE_TBD)
          {

               return fDamageRoll;
          }
          else
          {

               nResistance = GetM2DAInt(EngineConstants.TABLE_DAMAGETYPES, "Resistance", nDamageType);

               if (nResistance != EngineConstants.FALSE)
               {
                    float fResistance = GetCreatureProperty(oTarget, nResistance);

                    // -----------------------------------------------------------------
                    // Include game difficulty based runtime modifiers.
                    // -----------------------------------------------------------------
                    float fDiffModifier = Diff_GetDRMod(oTarget);
                    fResistance = MinF(fResistance + fDiffModifier, 75.0f) / 100.0f;
                    float fModifier = 1.0f - fResistance;

                    float fDamage = MaxF(fDamageRoll * fModifier, 1.0f);

#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistDamage", ToString(oTarget) + " resisting " + ToString(fDamageRoll) + " dmg by " + ToString(oAttacker) + " type " + ToString(nDamageType) + " Resistance score:" + ToString(fResistance) + ": " + ToString(fDamage));
#endif
                    return fDamage;
               }
               else
               {
                    Warning("Damage applied with invalid type. Please keep the game as it is and call georg over to look at the callstack");
               }

          }

          return fDamageRoll * 1.0f;

     }

     // LEGACY
     public float AbiScaleEffect(GameObject oCaster, GameObject oTarget, int nResistance, float fMaxValue, int nAttackingStat = EngineConstants.PROPERTY_ATTRIBUTE_SPELLPOWER, int bShowUIFeeback = EngineConstants.TRUE, float fResistanceAttributeValue = -1.0f, float fAttackingAttributeValue = -1.0f)
     {
          // check override first
          float fResistance = fResistanceAttributeValue;

          // if override is not used
          if (fResistance < 0.0f)
          {
               // are resistances used?
               if (nResistance == EngineConstants.RESISTANCE_INVALID)
               {
                    Log_Trace(EngineConstants.LOG_CHANNEL_DESIGN_SCRIPTERROR, "sys_resistances_h.AbiScaleEffect", "Resistance check with EngineConstants.RESISTANCE_INVALID resistance attribute, might be unintended." + GetCurrentScriptName());
                    return fMaxValue;
               }

               // get creature resistance property
               fResistance = GetCreatureProperty(oTarget, nResistance);
          }

          float F_CUTOFF = GetResistanceCutoff();

          float fSpellPower = 0.0f;

          // add level to resistance
          fResistance += IntToFloat(GetLevel(oTarget));

          // Placeables don't resist... ever.
          if (GetObjectType(oTarget) != EngineConstants.OBJECT_TYPE_CREATURE)
          {
               return fMaxValue;
          }

          // For traps have an owner - use their stats for the check
          if (GetObjectType(oCaster) == EngineConstants.OBJECT_TYPE_PLACEABLE)
          {
               if (GetLocalInt(oCaster, EngineConstants.PLC_TRAP_TYPE) > 0)
               {
                    GameObject oOwner = GetLocalObject(oCaster, EngineConstants.PLC_TRAP_OWNER);
                    if (GetObjectType(oOwner) == EngineConstants.OBJECT_TYPE_CREATURE)
                    {
                         oCaster = oOwner;
                    }
                    fSpellPower = 10.0f + GetLevel(oCaster) * 3.0f;
               }
          }

          if (GetObjectType(oCaster) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               {
                    // if value is overidden
                    if (fAttackingAttributeValue >= 0.0f)
                    {
                         fSpellPower = fAttackingAttributeValue;
                    }
                    else // else use creature's property
                    {
                         fSpellPower = GetCreatureProperty(oCaster, nAttackingStat);
                    }
                    fSpellPower = (fSpellPower * 0.8f) + (RandomFloat() * (fSpellPower * 0.1f * 2f)) + IntToFloat((GetLevel(oCaster)));
               }
          }

          // Attack is 80% spellpower + (0-40% spellpower);
          // (fSpellPower * 0.8) + (RandomFloat() * (fSpellPower * 0.2 * 2));

          float fScaledValue = fMaxValue;

          LogTrace(EngineConstants.LOG_CHANNEL_RESISTANCES, ToString(oTarget) + "[" + ToString(GetLevel(oTarget)) + "] resisting " + ToString(oCaster) + ":");

          if (fSpellPower > 0.0f)
          {

               fScaledValue = (fSpellPower - fResistance) / fSpellPower;
               fScaledValue = (fScaledValue >= F_CUTOFF) ? fScaledValue : EngineConstants.EFFECTIVENESS_RESISTED;
               fScaledValue = ((fScaledValue >= EngineConstants.RESISTANCE_CEILING) ? fMaxValue : (fScaledValue * fMaxValue));

               LogTrace(EngineConstants.LOG_CHANNEL_RESISTANCES, ToString(fResistance) + " resisting " + ToString(fSpellPower) + " - MaxVal: " + ToString(fMaxValue) + " Result: " + ToString(fScaledValue));

               if (bShowUIFeeback != EngineConstants.FALSE)
               {
                    if (fScaledValue == EngineConstants.EFFECTIVENESS_RESISTED)
                    {
                         UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_RESISTED);
                    }
                    else if (fScaledValue == fMaxValue)
                    {
                         // max value.
                    }
               }

               return fScaledValue;

          }
          else
          {
               if (bShowUIFeeback != EngineConstants.FALSE)
               {
                    UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_RESISTED);
               }

               return EngineConstants.EFFECTIVENESS_RESISTED;
          }

     }

     //moved public const float EngineConstants.RESISTANCE_LEVEL_MODIFIER = 5.0f;

     // returns EngineConstants.TRUE if target resists
     public int ResistanceCheck(GameObject oAttacker, GameObject oDefender, int nAttackingAttribute, int nDefendingResistance)
     {
          if (nDefendingResistance == EngineConstants.RESISTANCE_PHYSICAL)
          {
               if (GetHasEffects(oDefender, EngineConstants.EFFECT_TYPE_DAMAGE_WARD) != EngineConstants.FALSE)
               {
                    UI_DisplayMessage(oDefender, EngineConstants.UI_MESSAGE_IMMUNE);

                    return EngineConstants.TRUE;
               }
          }

          // get base resistance value
          float fResistance = GetCreatureProperty(oDefender, nDefendingResistance);
          Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistanceCheck", "Base resistance (" + ToString(nDefendingResistance) + ") = " + ToString(fResistance));

          // subtract attacking attribute
          fResistance -= GetAttributeModifier(oAttacker, nAttackingAttribute);
          Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistanceCheck", "  Modified by attacking attribute (" + ToString(nAttackingAttribute) + ") = " + ToString(fResistance));

          // level modifier
          int nLevelDifference = GetLevel(oAttacker) - GetLevel(oDefender);
          fResistance -= EngineConstants.RESISTANCE_LEVEL_MODIFIER * nLevelDifference;
          Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistanceCheck", "  Modified by level difference (" + ToString(nLevelDifference) + ") = " + ToString(fResistance));

          // rank modifier
          int nDefenderRank = GetCreatureRank(oDefender);
          float fRankModifier = GetM2DAFloat(Diff_GetAutoScaleTable(), "fResistanceModifier", nDefenderRank);
          fResistance += fRankModifier;
          Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistanceCheck", "  Modified by rank (" + ToString(nDefenderRank) + ", " + ToString(fRankModifier) + ") = " + ToString(fResistance));

          // rank maximum
          float fRankMaximum = GetM2DAFloat(Diff_GetAutoScaleTable(), "fResistanceMaximum", nDefenderRank);
          float fFinalResistance = MinF(fResistance, fRankMaximum);
          Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistanceCheck", "  Final resistance (" + ToString(fRankMaximum) + ") = " + ToString(fFinalResistance));

          float fResistanceRoll = RandomFloat() * 100.0f;
          Log_Trace(EngineConstants.LOG_CHANNEL_RESISTANCES, "sys_resistances_h.ResistanceCheck", "Resistance roll " + ToString(fResistanceRoll) + " vs " + ToString(fFinalResistance));

          if (fResistanceRoll < fFinalResistance)
          {
               UI_DisplayMessage(oDefender, EngineConstants.UI_MESSAGE_RESISTED);

               return EngineConstants.TRUE;
          }
          else
          {
               return EngineConstants.FALSE;
          }
     }

     // @brief Returns an xEffect duration scaled by rank of oCreature and game difficulty
     // @author Georg
     public float GetRankAdjustedEffectDuration(GameObject oCreature, float fDur)
     {

          // -------------------------------------------------------------------------
          // Georg: This deserves some explanation:
          //
          //        One of the core issues when designing the combat system was always
          //        the fact that the story called for sequences during the game in which
          //        your main character acts solo.
          //
          //        Characters reliant on special abilities, such as rogues, would require
          //        them to be very effective in order to beat enemies that a standard tank
          //        can plow through. However, if these abilities would maintain the same
          //        effectiveness in a full party, most encounters would turn into a stunfest
          //
          //        The solution is to add 1 second flat to each detrimental xEffect duration
          //        lasting at least 1 second for each unoccupied slot in the party - before applying
          //        rank and difficulty modifiers.
          //
          // -------------------------------------------------------------------------
          if (IsPartyMember(oCreature) == EngineConstants.FALSE)
          {
               float fPartySizeModifier = IntToFloat(Max(0, 4 - GetArraySize(GetPartyList())));

               if (fDur >= 1.0f)
               {
                    fDur += fPartySizeModifier;
               }
          }

          float fRankDurMod = Diff_GetDurationModifier(oCreature);
          if (fRankDurMod > 0.0f)
          {
               return (fDur * fRankDurMod);
          }
          else
          {
               return fDur;
          }
     }
}