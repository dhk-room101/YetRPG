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
     // -----------------------------------------------------------------------------
     // Enchantment (temporary weapon enchantments)
     // -----------------------------------------------------------------------------
     //#include"core_h"

     //moved public const int ENCHANTMENT_FIRE      = 3000;
     //moved public const int ENCHANTMENT_COLD      = 3001;
     //moved public const int ENCHANTMENT_LIGHTNING = 3002;
     //moved public const int ENCHANTMENT_STONE     = 6084;
     //moved public const int ENCHANTMENT_TELEK     = 6085;
     //moved public const int ENCHANTMENT_HOLY      = 6086;
     //moved public const int ENCHANTMENT_FIRE_COATING                   = 3003; // 2 fire damage per power
     //moved public const int ENCHANTMENT_COLD_COATING                   = 3004; // 2 cold damage per power
     //moved public const int ENCHANTMENT_LIGHTNING_COATING              = 3005; // 2 electricity damage per power
     //moved public const int ENCHANTMENT_NATURE_COATING                 = 3006; // 2 nature damage per power
     //moved public const int ENCHANTMENT_SPIRIT_COATING                 = 3007; // 2 spirit damage per power
     //moved public const int ENCHANTMENT_VENOM                          = 3008; // 1 nature damage per power, 0.1 slow per power for 5s (unresistable)
     //moved public const int ENCHANTMENT_DEATHROOT_EXTRACT              = 3009; // 1 nature damage per power, 2s stun per power (resistable)
     //moved public const int ENCHANTMENT_CROW_POISON                    = 3010; // 2 nature damage per power, 0.1 slow per power for 5s (unresistable), 2s stun per power (resistable)
     //moved public const int ENCHANTMENT_SOLDIERS_BANE                  = 3011; // 5 stamina damage per power (non-mages only)
     //moved public const int ENCHANTMENT_MAGEBANE                       = 3012; // 5 mana damage per power (mages only)
     //moved public const int ENCHANTMENT_DEMONIC_POISON                 = 3013; // 5 spirit damage per power
     //moved public const int ENCHANTMENT_QUIET_DEATH                    = 3014; // 10 nature damage per power, instant kill creatures 20% or less max health

     public int _HasItemProperty(GameObject oItem, int nProperty)
     {
          LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "Enchant._HasItemProperty:" + ToString(oItem) + " " + ToString(nProperty) + " = " + ToString(GetItemPropertyPower(oItem, nProperty, EngineConstants.FALSE)));

          return GetItemPropertyPower(oItem, nProperty, EngineConstants.FALSE) > 0 ? EngineConstants.TRUE : EngineConstants.FALSE;
     }

     public int HasEnchantments(GameObject oCreature)
     {
          return GetHasEffects(oCreature, EngineConstants.EFFECT_TYPE_ENCHANTMENT);
     }

     public void EffectEnchantment_HandleEquip(GameObject oItem, GameObject oOwner, int nType = 0, int nPower = 0)
     {
          int nItemType = GetItemType(oItem);


          if (nItemType != EngineConstants.ITEM_TYPE_WEAPON_MELEE)
          {
               return;
          }




          // -------------------------------------------------------------------------
          // coming from function handler
          // -------------------------------------------------------------------------
          if (((nType + nPower) > 0 ? EngineConstants.TRUE : EngineConstants.FALSE) != EngineConstants.FALSE)
          {
               if (_HasItemProperty(oItem, nType) == EngineConstants.FALSE)
               {

                    AddItemProperty(oItem, nType, nPower);
               }

          }
          // -------------------------------------------------------------------------
          // coming from equip event
          // -------------------------------------------------------------------------
          else
          {

               List<xEffect> eEffect = GetEffects(oOwner, EngineConstants.EFFECT_TYPE_ENCHANTMENT);

               int nSize = GetArraySize(eEffect);
               int i;

               for (i = 0; i < nSize; i++)
               {
                xEffect _effect = eEffect[i];
                    nType = GetEffectIntegerRef(ref _effect, 0);
                    nPower = GetEffectIntegerRef(ref _effect, 1);
                    if (_HasItemProperty(oItem, nType) == EngineConstants.FALSE)
                    {
                         AddItemProperty(oItem, nType, nPower);
                    }
               }

          }

     }

     public void EffectEnchantment_HandleUnEquip(GameObject oItem, GameObject oOwner, int nType = 0)
     {
          int nPower;

          LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "EffectEnchantment_HandleUnEquip:" + ToString(oItem) + " " + ToString(nType));

          if (GetItemType(oItem) != EngineConstants.ITEM_TYPE_WEAPON_MELEE)
          {
               return;
          }

          if (nType != 0)
          {
               if (_HasItemProperty(oItem, nType) != EngineConstants.FALSE)
               {
                    RemoveItemProperty(oItem, nType);
                    _HasItemProperty(oItem, nType);
               }
          }
          else
          {
               List<xEffect> eEffect = GetEffects(oOwner, EngineConstants.EFFECT_TYPE_ENCHANTMENT);
               int nSize = GetArraySize(eEffect);
               int i;

               for (i = 0; i < nSize; i++)
               {
                xEffect _effect = eEffect[i];
                    nType = GetEffectIntegerRef(ref _effect, 0);
                    nPower = GetEffectIntegerRef(ref _effect, 1);
                    if (_HasItemProperty(oItem, nType) != EngineConstants.FALSE)
                    {
                         RemoveItemProperty(oItem, nType);
                    }
               }
          }

     }

     public int Effects_HandleApplyEffectEnchantment(xEffect eEffect)
     {
          int nType = GetEffectIntegerRef(ref eEffect, 0);
          int nPower = GetEffectIntegerRef(ref eEffect, 1);

          GameObject oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, gameObject);
          if (oWeapon != null)
          {

               DEBUG_PrintToScreen(ToString(oWeapon) + " " + ToString(nType) + " " + ToString(nPower));
               EffectEnchantment_HandleEquip(oWeapon, gameObject, nType, nPower);
          }

          oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, gameObject);
          if (oWeapon != null)
          {
               EffectEnchantment_HandleEquip(oWeapon, gameObject, nType, nPower);
          }

          return EngineConstants.TRUE;
     }

     public int Effects_HandleRemoveEffectEnchantment(xEffect eEffect)
     {
          GameObject oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, gameObject);

          LogTrace(EngineConstants.LOG_CHANNEL_TEMP, "Enchant-Remove:" + ToString(oWeapon));
          int nType = GetEffectIntegerRef(ref eEffect, 0);
          if (oWeapon != null)
          {

               EffectEnchantment_HandleUnEquip(oWeapon, gameObject, nType);
          }

          oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, gameObject);
          if (oWeapon != null)
          {
               EffectEnchantment_HandleUnEquip(oWeapon, gameObject, nType);
          }

          return EngineConstants.TRUE;
     }
}