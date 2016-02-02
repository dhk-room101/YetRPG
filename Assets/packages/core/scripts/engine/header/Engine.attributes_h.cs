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
     // attributes_h: Functions dealing with getting and setting player stats
     //------------------------------------------------------------------------------
     /*

         Accessor functions to creature stats and attributes.

         Many of these functions are temporary placeholders in script that should
         be moved into the engine once the system has been tested and balanced
         sufficiently.

         These functions should be the only functions in the game that write or
         read attribute values from creatures.

         The concepts I am using for storing changes to attributes is to leave the
         actual base values untouched and instead store modifiers in a seperate variable.

         This means nobody is every allowed to write the real attribute values directly,
         instead all effects and changes write into the modifier variable which is then
         added on top of the base value and returned by the Attribute_Get* function

         Example:

             Base Modifier Displayed  Comment
             10   0        10
             10   2        12           +2 effect, eg. from beneficial spell
             10   -5       9            -5 effect, eg. from curse

         Note: In case the above isn't clear, the following is NOT ALLOWED in any script

         int n = Attribute_GetStrength(oPlayer);
         n = n+10
         Attribute_SetStrength(oPlayer, n);  // <-- that's why this function doesn't exist!

         When in doubt, please contact Georg.

     */
     //------------------------------------------------------------------------------
     // Owner: Georg Zoeller
     //------------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"var_constants_h"
     //#include"log_h"
     //#include"core_h"
     //#include"config_h"
     //#include"events_h"

     //moved public const int EngineConstants.ATTRIBUTE_STR = EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH;
     //moved public const int EngineConstants.ATTRIBUTE_DEX = EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY;
     //moved public const int EngineConstants.ATTRIBUTE_INT = EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE;
     //moved public const int EngineConstants.ATTRIBUTE_CON = EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION;
     //moved public const int EngineConstants.ATTRIBUTE_MAG = EngineConstants.PROPERTY_ATTRIBUTE_MAGIC;
     //moved public const int EngineConstants.ATTRIBUTE_WIL = EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER;
     /*

     public void Attribute_ChangeAttributeModifier(GameObject oCreature,int nAttribute,int nAmount)
     {
     //    Log_Systems("+++ changing attribute " + IntToString(nAttribute) + " by " + IntToString(nAmount));
         UpdateCreatureProperty(oCreature, nAttribute, IntToFloat(nAmount), EngineConstants.PROPERTY_VALUE_MODIFIER);
     }

     public void Attribute_UpdateMaxManaStamina(GameObject oCreature, int nAmount)
     {
         float fMana = IntToFloat(nAmount);

         // to change the MAX stamina/mana (TOTAL) on a creature, we need to change the modifier
         // which, in turn, will affect both current and total.
         UpdateCreatureProperty(oCreature, EngineConstants.PROPERTY_MANA_STAMINA,fMana, EngineConstants.PROPERTY_VALUE_MODIFIER);

     }

     public int Attribute_GetSpellPower(GameObject oCreature)
     {
         float fSpellPower = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_SPELLPOWER, EngineConstants.PROPERTY_VALUE_TOTAL);
         return (FloatToInt(fSpellPower));
     }

     public float Attribute_CalcAttributeBonus(int nAmount)
     {
          return ((nAmount > 10)? nAmount-10.0f : 0.0f);
     }

     // -----------------------------------------------------------------------------
     // Attribute Accessors
     // -----------------------------------------------------------------------------

     public int Attribute_GetDexterity(GameObject oCreature)
     {
         return GetCreatureAttribute(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY);
     }

     public int Attribute_GetIntelligence(GameObject oCreature)
     {
         return GetCreatureAttribute(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE);
     }

     public int Attribute_GetMagic(GameObject oCreature)
     {
         return GetCreatureAttribute(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC);
     }

     public int Attribute_GetConstitution(GameObject oCreature)
     {
         return GetCreatureAttribute(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION);
     }

     public int Attribute_GetStrength(GameObject oCreature)
     {
         return GetCreatureAttribute(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH);
     }

     public int Attribute_GetWillpower(GameObject oCreature)
     {
         return GetCreatureAttribute(oCreature, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER);
     }

     public int Attribute_GetLevel(GameObject oCreature)
     {
       return FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_LEVEL));
     }

     // -----------------------------------------------------------------------------
     // Depleteables
     // -----------------------------------------------------------------------------

     public float Attribute_GetMaxManaStamina (GameObject oCreature)
     {
         float fMana = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_TOTAL);
         return fMana;
     }

     public float Attribute_GetManaStamina(GameObject oCreature)
     {
         float fMana = GetCreatureProperty(oCreature, EngineConstants.PROPERTY_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT);
         return fMana;
     }

     public void Attribute_UpdateManaStamina (GameObject oCreature, int nAmount)
     {
         float fMana = IntToFloat(nAmount);
         UpdateCreatureProperty(oCreature, EngineConstants.PROPERTY_MANA_STAMINA,fMana, EngineConstants.PROPERTY_VALUE_CURRENT);
     }

     */

     // -----------------------------------------------------------------------------
     // Little Funct
     // -----------------------------------------------------------------------------
     public int Attribute_LoadFrom2DA(GameObject oCreature)
     {
          int bArea = EngineConstants.FALSE;
          int i;

          string sTag = GetTag(oCreature);
          string sAreaTag = GetTag(GetArea(oCreature));
          float fValue=0.0f;

          if (GetM2DAFloat(EngineConstants.TABLE_CHARACTERS, sAreaTag + "_" + sTag, 0) >= 1.0f)
          {
               bArea = EngineConstants.TRUE;
          }
          else if (GetM2DAFloat(EngineConstants.TABLE_CHARACTERS, sTag, 0) < 1.0f)
          {
               Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "attributes_h.LoadFrom2DA", "Not loading character stats from 2da", oCreature);
               return EngineConstants.FALSE;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "attributes_h.LoadFrom2DA", "Loading character stats from 2da", oCreature);

          for (i = 1; i <= 24; i++)
          {
               sTag = GetTag(oCreature);

               if (bArea != EngineConstants.FALSE)
               {
                    sTag = GetTag(GetArea(oCreature)) + "_" + sTag;
               }
               else
               {
                    fValue = GetM2DAFloat(EngineConstants.TABLE_CHARACTERS, sTag, i);
               }

               Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "attributes_h.LoadFrom2DA", GetM2DAString(EngineConstants.TABLE_CHARACTERS, "Label", i) + " = " + ToString(fValue), oCreature);

               if (GetCreaturePropertyType(oCreature, i) == EngineConstants.PROPERTY_TYPE_DERIVED)
               {
                    UpdateCreatureProperty(oCreature, i, fValue, EngineConstants.PROPERTY_VALUE_MODIFIER);
               }
               else
               {
                    SetCreatureProperty(oCreature, i, fValue);
               }

               if (GetCreaturePropertyType(oCreature, i) == EngineConstants.PROPERTY_TYPE_DEPLETABLE)
               {
                    SetCreatureProperty(oCreature, i, fValue, EngineConstants.PROPERTY_VALUE_CURRENT);
               }

          }
          return EngineConstants.TRUE;

     }
}