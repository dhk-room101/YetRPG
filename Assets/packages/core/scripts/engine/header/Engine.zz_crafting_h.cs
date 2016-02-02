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
     //::///////////////////////////////////////////////
     //:: crafting_h
     //:: Copyright (c) 2006 Bioware Corp.
     //:://////////////////////////////////////////////
     /*
         Crafting constants and functions
     */
     //:://////////////////////////////////////////////
     //:: Created By: Rick Burton
     //:://////////////////////////////////////////////

     ////#include"wrappers_h"
     ////#include"utility_h"
     ////#include"plot_h"
     ////#include"events_h"

     /* @brief checks for complete set of reagents in player's inventory
     *
     * Returns whether or not the reagents exist in the player's inventory
     *
     * @param nTrapType - Constant for type of trap attempting to be crafted
     * @author Rick Burton
     **/
     //int CanCraftTrap(int nTrapType);

     /* @brief
     *
     *   1) checks for complete set of reagents in player's inventory
     *   2) removes reagents from player's inventory
     *   3) adds trap to player's inventory
     *
     *   Returns whether or not the creation was successful
     *
     * @param nTrapType - Constant for type of trap attempting to be crafted
     * @author Rick Burton
     **/
     //public void CraftTrap(int nTrapType);

     /* @brief checks for complete set of reagents in player's inventory
     *
     * Returns whether or not the reagents exist in the player's inventory
     *
     * @param nPoisonType - Constant for type of trap attempting to be crafted
     * @author Rick Burton
     **/
     //int CanCraftPoison(int nPoisonType);

     /* @brief
     *
     *   1) checks for complete set of reagents in player's inventory
     *   2) removes reagents from player's inventory
     *   3) adds trap to player's inventory
     *
     *   Returns whether or not the creation was successful
     *
     * @param nPoisonType - Constant for type of trap attempting to be crafted
     * @author Rick Burton
     **/
     //public void CraftPoison(int nPoisonType);    

     //public void main() {}

     //*****************************
     //      Reagent Constants
     //*****************************

     //moved public const string EngineConstants.GEN_IM_CRAFT_REAG_WOODEN_PLANK = "gen_im_cft_reag_woodplank.uti";
     //moved public const string EngineConstants.GEN_IM_CRAFT_REAG_SPRING = "gen_im_cft_reag_spring.uti";
     //moved public const string EngineConstants.GEN_IM_CRAFT_REAG_SMALL_POISON_GLAND = "gen_im_cft_reag_spoigland.uti";
     //moved public const string EngineConstants.GEN_IM_CRAFT_REAG_SMALL_JAR = "gen_im_cft_reag_smalljar.uti";
     //moved public const string EngineConstants.GEN_IM_CRAFT_REAG_GLASS_VIAL = "gen_im_cft_reag_glassvial.uti";
     //moved public const string EngineConstants.GEN_IM_CRAFT_REAG_CRAGROOT = "gen_im_cft_reag_cragroot.uti";

     //*****************************
     //      Poison Constants
     //*****************************

     //moved public const int POISON_RAT = 1;

     //moved public const string EngineConstants.GEN_IM_CRAFT_POISON_RAT = "gen_im_cft_poison_rat.uti";

     //*****************************
     //      Trap Constants
     //*****************************

     //moved public const int EngineConstants.TRAP_WOODEN_CLAW = 1;

     //moved public const string EngineConstants.GEN_IM_CRAFT_TRAP_WOODEN_CLAW = "gen_im_cft_trap_woodclaw.uti";

     //*****************************
     //      Herbalism Constants
     //*****************************

     //moved public const int HERBALISM_POULTICE_OF_HEALTH = 1;

     //moved public const string EngineConstants.GEN_IM_CRAFT_HERB_POULTICE_OF_HEALTH = "gen_im_cft_herb_poulthealth.uti";

     //*****************************
     //      Functions
     //*****************************

     
    public int CanCraftTrap(int nTrapType)
    {
        int nResult = EngineConstants.TRUE;
        int nTemp;

        switch (nTrapType)
        {
            case EngineConstants.TRAP_WOODEN_CLAW:
            {
                nTemp = UT_CountItemInInventory(EngineConstants.GEN_IM_CRAFT_REAG_WOODEN_PLANK);
                if (nTemp < 1)
                    nResult = EngineConstants.FALSE;

                nTemp = UT_CountItemInInventory(EngineConstants.GEN_IM_CRAFT_REAG_SPRING);
                if (nTemp < 1)
                    nResult = EngineConstants.FALSE;
                break;
            }
        }
        return nResult;
    }

    public int CraftTrap(int nTrapType)
    {
        int nResult = CanCraftTrap(nTrapType);

        if (nResult != EngineConstants.FALSE)
            switch (nTrapType)
            {
                case EngineConstants.TRAP_WOODEN_CLAW:
                {
                    UT_RemoveItemFromInventory(EngineConstants.GEN_IM_CRAFT_REAG_WOODEN_PLANK,1);
                    UT_RemoveItemFromInventory(EngineConstants.GEN_IM_CRAFT_REAG_SPRING,1);

                    UT_AddItemToInventory(EngineConstants.GEN_IM_CRAFT_TRAP_WOODEN_CLAW);
                    break;
                }
            }
        return nResult;
    }

    public int CanCraftPoison(int nPoisonType)
    {
        int nResult = EngineConstants.TRUE;
        int nTemp;

        switch (nPoisonType)
        {
            case EngineConstants.POISON_RAT:
            {
                nTemp = UT_CountItemInInventory(EngineConstants.GEN_IM_CRAFT_REAG_GLASS_VIAL);
                if (nTemp < 1)
                    nResult = EngineConstants.FALSE;

                nTemp = UT_CountItemInInventory(EngineConstants.GEN_IM_CRAFT_REAG_SMALL_POISON_GLAND);
                if (nTemp < 1)
                    nResult = EngineConstants.FALSE;
                break;
            }
        }
        return nResult;
    }

    public int CraftPoison(int nPoisonType)
    {
        int nResult = CanCraftPoison(nPoisonType);

        if (nResult != EngineConstants.FALSE)
            switch (nPoisonType)
            {
                case EngineConstants.TRAP_WOODEN_CLAW:
                {
                    UT_RemoveItemFromInventory(EngineConstants.GEN_IM_CRAFT_REAG_GLASS_VIAL,1);
                    UT_RemoveItemFromInventory(EngineConstants.GEN_IM_CRAFT_REAG_SMALL_POISON_GLAND,1);

                    UT_AddItemToInventory(EngineConstants.GEN_IM_CRAFT_POISON_RAT);
                    break;
                }
            }
        return nResult;
    }     
}