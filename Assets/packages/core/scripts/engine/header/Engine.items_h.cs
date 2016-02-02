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
     //#include"2da_constants_h"
     //#include"log_h"
     //#include"core_h"

     public int Items_CheckAmmo(GameObject oAttacker, GameObject oWeapon = null, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
     {

          // -----------------------------------------------------------------
          // Ammo is in the off hand
          // -----------------------------------------------------------------
          GameObject oAmmoStack = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, oAttacker, nWeaponSet);

          if (oWeapon == null)
          {
               oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oAttacker, nWeaponSet);
          }

          int nWeaponType = GetBaseItemType(oWeapon);

          // -----------------------------------------------------------------
          // need to have a valid ammo GameObject in the offhand
          // -----------------------------------------------------------------
          if (IsObjectValid(oAmmoStack) != EngineConstants.FALSE)
          {
               // -----------------------------------------------------------------
               // Check that this is the right ammo type for this weapon
               // -----------------------------------------------------------------
               int nBaseItem = GetBaseItemType(oAmmoStack);
               if (nBaseItem == GetM2DAInt(EngineConstants.TABLE_ITEMS, "Ammo", nWeaponType))
               {
                    // -----------------------------------------------------------------
                    // Check that the stack size is > 0
                    // -----------------------------------------------------------------
                    if (GetItemStackSize(oAmmoStack) > 0)
                    {
                         return GetItemStackSize(oAmmoStack);
                    }

               }
          }

          // -----------------------------------------------------------------
          // for everything else ... there is EngineConstants.FALSE;
          // -----------------------------------------------------------------
          return EngineConstants.FALSE;
     }

     // Returns max size of ammo
     public int Items_CheckMaxAmmo(GameObject oAttacker, GameObject oWeapon = null, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
     {
          int nMaxAmmo = 0;
          GameObject oAmmoStack = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_OFFHAND, oAttacker, nWeaponSet);

          if (oWeapon == null)
          {
               oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oAttacker, nWeaponSet);
          }

          int nWeaponType = GetBaseItemType(oWeapon);

          if (IsObjectValid(oAmmoStack) != EngineConstants.FALSE)
          {
               // -----------------------------------------------------------------
               // Check that this is the right ammo type for this weapon
               // -----------------------------------------------------------------
               int nBaseItem = GetBaseItemType(oAmmoStack);
               if (nBaseItem == GetM2DAInt(EngineConstants.TABLE_ITEMS, "Ammo", nWeaponType))
               {
                    nMaxAmmo = GetM2DAInt(EngineConstants.TABLE_ITEMS, "StackSize", nBaseItem);
               }
          }
          return nMaxAmmo;
     }

     public int Items_GetRangedWeaponSet(GameObject oCreature, int bConsiderStaffRanged = EngineConstants.FALSE)
     {

          int nWeaponSet = GetActiveWeaponSet(oCreature);
          int nInactiveWeaponSet = (nWeaponSet == 1) ? 0 : 1;

          GameObject oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oCreature, nWeaponSet);
          if (IsObjectValid(oWeapon) != EngineConstants.FALSE)
          {
               if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_RANGED)
               {
                    //if (Items_CheckAmmo(oCreature, oWeapon))
                    //{
                    return nWeaponSet;
                    //}
               }
               if (bConsiderStaffRanged != EngineConstants.FALSE && GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_WAND)
                    return nWeaponSet;
          }

          oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oCreature, nInactiveWeaponSet);
          if (IsObjectValid(oWeapon) != EngineConstants.FALSE)
          {
               if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_RANGED)
               {
                    //if (Items_CheckAmmo(oCreature, oWeapon, nInactiveWeaponSet))
                    //{
                    return nInactiveWeaponSet;
                    //}  
               }
               if (bConsiderStaffRanged != EngineConstants.FALSE && GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_WAND)
                    return nInactiveWeaponSet;
          }

          return -1;

     }

     public int Items_GetMeleeWeaponSet(GameObject oCreature)
     {

          int nWeaponSet = GetActiveWeaponSet(oCreature);
          int nInactiveWeaponSet = (nWeaponSet == 1) ? 0 : 1;

          GameObject oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oCreature, nWeaponSet);
          if (IsObjectValid(oWeapon) != EngineConstants.FALSE)
          {
               if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_MELEE)
               {
                    return nWeaponSet;
               }
          }

          oWeapon = GetItemInEquipSlot(EngineConstants.INVENTORY_SLOT_MAIN, oCreature, nInactiveWeaponSet);
          if (IsObjectValid(oWeapon) != EngineConstants.FALSE)
          {
               if (GetItemType(oWeapon) == EngineConstants.ITEM_TYPE_WEAPON_MELEE)
               {
                    return nInactiveWeaponSet;
               }
          }

          return -1;

     }
}