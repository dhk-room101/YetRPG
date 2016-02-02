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

     //#include"plt_arl100pt_equip_militia"
     //#include"plt_arl130pt_recruit_dwyn"
     //#include"plt_arl150pt_tavern_drinks"
     //#include"plt_arl100pt_siege_prep"
     //#include"plt_arl100pt_activate_shale"
     //#include"plt_arl100pt_holy_amulet"

     //Returns the current value of the morale of the militia in the Arl Eamon plot.
     //Morale is determined by a number of actions of the player in preparing for the
     //seige.
     //Morale can range from -4 to +4. -2 or less is considered low, +2 or more is considered high.
     public int Arl_GetMilitiaMorale()
     {
          //Positive factors
          int bOwenWorking = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_EQUIP_MILITIA, EngineConstants.ARL_EQUIP_MILITIA_OWEN_MAKING_WEAPONS);
          int bDwynHelping = WR_GetPlotFlag(EngineConstants.PLT_ARL130PT_RECRUIT_DWYN, EngineConstants.ARL_RECRUIT_DWYN_DWYN_HELPING);
          int bConvincedVictory = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_MURDOCK_CONVINCED_THEY_COULD_WIN);
          int bFreeDrinks = WR_GetPlotFlag(EngineConstants.PLT_ARL150PT_TAVERN_DRINKS, EngineConstants.ARL_TAVERN_DRINKS_MILITIA_DRINKS_FREE);

          //negative factors
          int bConvincedDefeat = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_SIEGE_PREP, EngineConstants.ARL_SIEGE_PREP_MURDOCK_DISCOURAGE);
          int bOwenDead = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_EQUIP_MILITIA, EngineConstants.ARL_EQUIP_MILITIA_MURDOCK_KNOWS_OWEN_DEAD);
          int bDwynDead = WR_GetPlotFlag(EngineConstants.PLT_ARL130PT_RECRUIT_DWYN, EngineConstants.ARL_RECRUIT_DWYN_MURDOCK_KNOWS_DWYN_DEAD);
          int bStashDenied = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_EQUIP_MILITIA, EngineConstants.ARL_EQUIP_MILITIA_MURDOCK_DENIED_STASH);

          int nPositive = bOwenWorking + bDwynHelping + bConvincedVictory + bFreeDrinks;
          int nNegative = bConvincedDefeat + bOwenDead + bDwynDead + bStashDenied;

          int nMorale = nPositive - nNegative;
          return nMorale;
     }

     //Returns the current value of the morale of the knights in the Arl Eamon plot.
     //Morale is determined by a number of actions of the player in preparing for the
     //seige.
     //Morale can range from -1 (low) to +1 (high)
     public int Arl_GetKnightsMorale()
     {
          //positive factors
          int bGaveAmulets = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_HOLY_AMULET, EngineConstants.ARL_HOLY_AMULET_PERTH_HAS_AMULETS);
          int bSentShale = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_ACTIVATE_SHALE, EngineConstants.ARL_ACTIVATE_SHALE_SHALE_HELPING_PERTH);

          int nMorale = bGaveAmulets + bSentShale;

          return 0;
     }

     public void ARL_SiegeGiveItemAndEquip(string rItem, GameObject oCreature, int nEquipmentSlot, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET, int nStackSize = 1)
     {
          if (rItem != EngineConstants.INVALID_RESOURCE)
          {
               GameObject oItem = UT_AddItemToInventory(rItem, nStackSize, oCreature);
               EquipItem(oCreature, oItem, nEquipmentSlot, nWeaponSet);
          }
     }

     public void ARL_SiegeEquipMilitiaMember(GameObject oCreature)
     {
          int bOwenWorking = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_EQUIP_MILITIA, EngineConstants.ARL_EQUIP_MILITIA_OWEN_MAKING_WEAPONS);
          int bStashGiven = WR_GetPlotFlag(EngineConstants.PLT_ARL100PT_EQUIP_MILITIA, EngineConstants.ARL_EQUIP_MILITIA_MURDOCK_GIVEN_STASH);

          string rMilitiaBoots = EngineConstants.INVALID_RESOURCE;
          string rMilitiaArmor = EngineConstants.INVALID_RESOURCE;
          string rMilitiaGloves = EngineConstants.INVALID_RESOURCE;
          string rMilitiaHelmet = EngineConstants.INVALID_RESOURCE;
          string rMiltiaWeapon = EngineConstants.INVALID_RESOURCE;
          string rMiltiaBow = EngineConstants.INVALID_RESOURCE;

          if (bOwenWorking != EngineConstants.FALSE)
          {
               rMilitiaBoots = EngineConstants.ARL_R_IT_MILITIA_BOOTS_GOOD;
               rMilitiaArmor = EngineConstants.ARL_R_IT_MILITIA_ARMOR_GOOD;
               rMilitiaGloves = EngineConstants.ARL_R_IT_MILITIA_GLOVES_GOOD;
               rMilitiaHelmet = EngineConstants.ARL_R_IT_MILITIA_HELMET_GOOD;
               rMiltiaWeapon = EngineConstants.ARL_R_IT_MILITIA_WEAPON_GOOD;
               rMiltiaBow = EngineConstants.ARL_R_IT_MILITIA_BOW_GOOD;
          }
          else if (bStashGiven != EngineConstants.FALSE)
          {
               rMilitiaBoots = EngineConstants.ARL_R_IT_MILITIA_BOOTS_STANDARD;
               rMilitiaArmor = EngineConstants.ARL_R_IT_MILITIA_ARMOR_STANDARD;
               rMilitiaGloves = EngineConstants.ARL_R_IT_MILITIA_GLOVES_STANDARD;
               rMilitiaHelmet = EngineConstants.ARL_R_IT_MILITIA_HELMET_STANDARD;
               rMiltiaWeapon = EngineConstants.ARL_R_IT_MILITIA_WEAPON_STANDARD;
               rMiltiaBow = EngineConstants.ARL_R_IT_MILITIA_BOW_STANDARD;
          }

          ARL_SiegeGiveItemAndEquip(rMilitiaBoots, oCreature, EngineConstants.INVENTORY_SLOT_BOOTS);
          ARL_SiegeGiveItemAndEquip(rMilitiaArmor, oCreature, EngineConstants.INVENTORY_SLOT_CHEST);
          ARL_SiegeGiveItemAndEquip(rMilitiaGloves, oCreature, EngineConstants.INVENTORY_SLOT_GLOVES);
          ARL_SiegeGiveItemAndEquip(rMilitiaHelmet, oCreature, EngineConstants.INVENTORY_SLOT_HEAD);
          ARL_SiegeGiveItemAndEquip(rMiltiaWeapon, oCreature, EngineConstants.INVENTORY_SLOT_MAIN, 0);
          ARL_SiegeGiveItemAndEquip(rMiltiaBow, oCreature, EngineConstants.INVENTORY_SLOT_MAIN, 1);
          SwitchWeaponSet(oCreature, 1);
     }
}