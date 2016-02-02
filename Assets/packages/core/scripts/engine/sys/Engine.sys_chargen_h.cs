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
    // chargen_h
    // -----------------------------------------------------------------------------
    /*
        Character Generation Header
    */
    // -----------------------------------------------------------------------------
    // owner: georg zoeller
    // -----------------------------------------------------------------------------

    //#include"plt_gen00pt_backgrounds"
    //#include"wrappers_h"
    //#include"core_h"
    //#include"sys_chargen_engine"
    //#include"ai_constants_h"

    //moved public const int EngineConstants.ABILITY_TALENT_TRAIT_HUMANOID = 4002;
    //moved public const float CHARGEN_BASE_ATTRIBUTE_VALUE = 10.0f;

    // -----------------------------------------------------------------------------
    /*  @brief Add ability wrapper  */
    public void _AddAbility(GameObject oChar, int nAbility, int bRemove = EngineConstants.FALSE)
    {
        if (nAbility != 0)
        {
            if (bRemove == EngineConstants.FALSE)
            {
                AddAbility(oChar, nAbility);
                SetQuickslot(oChar, -1, nAbility);
            }
            else
            {
                RemoveAbility(oChar, (-1) * nAbility);
            }
        }
    }

    // -----------------------------------------------------------------------------
    /*
     *  @brief Modifiers a creature property base value by a given number
     *
     *  @param oChar        The character
     *  @param nProperty    The property to modifiy
     *  @param fModifyBy    The amount to modify by
     *
     *  @author Georg Zoeller
    */
    public void Chargen_ModifyCreaturePropertyBase(GameObject oChar, int nProperty, float fModifyBy)
    {
        if (fModifyBy != 0.0f)
        {
            float fCur = GetCreatureProperty(oChar, nProperty, EngineConstants.PROPERTY_VALUE_BASE);

#if DEBUG
            if (IsPartyMember(oChar) != EngineConstants.FALSE)
            {
                Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_chargen_tools_h.ModPropBase", "Modifying prop " + ToString(nProperty) + " by " + ToString(fModifyBy) + " = " + ToString(fCur + fModifyBy));
            }
#endif

            fCur += fModifyBy;
            SetCreatureProperty(oChar, nProperty, fCur, EngineConstants.PROPERTY_VALUE_BASE);

            if (IsUsingEP1Resources() != EngineConstants.FALSE)
            {
                // modify explore regeneration rates if applicable
                if (nProperty == EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION) // health regeneration
                {
                    float fRegenCur = GetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_HEALTH, EngineConstants.PROPERTY_VALUE_BASE);
                    float fRegen = fModifyBy * 0.5f;
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_chargen_tools_h.ModPropBase", "Modifying Health Regeneration rate from " + ToString(fRegenCur) + " by " + ToString(fRegen) + " to " + ToString(fRegenCur + fRegen));
#endif
                    fRegenCur += fRegen;
                    SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_HEALTH, fRegenCur, EngineConstants.PROPERTY_VALUE_BASE);
                }
                else if (nProperty == EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER) // mana/stamina regeneration
                {
                    float fRegenCur = GetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA, EngineConstants.PROPERTY_VALUE_BASE);
                    float fRegen = fModifyBy * 0.875f;
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_chargen_tools_h.ModPropBase", "Modifying Stamina/Mana Regeneration rate from " + ToString(fRegenCur) + " by " + ToString(fRegen) + " to " + ToString(fRegenCur + fRegen));
#endif
                    fRegenCur += fRegen;
                    SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA, fRegenCur, EngineConstants.PROPERTY_VALUE_BASE);
                }
            }

            // ---------------------------------------------------------------------
            // If the property is of a depletable type, synch the total to the max.
            // This causes the 'heal on levelup' effect
            // ---------------------------------------------------------------------
            if (GetCreaturePropertyType(oChar, nProperty) == EngineConstants.PROPERTY_TYPE_DEPLETABLE)
            {
                fCur = GetCreatureProperty(oChar, nProperty, EngineConstants.PROPERTY_VALUE_TOTAL);

                if (IsDead(oChar) == EngineConstants.FALSE)
                    SetCreatureProperty(oChar, nProperty, fCur, EngineConstants.PROPERTY_VALUE_CURRENT);
            }
        }
        else
        {
#if DEBUG
            Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_chargen_tools_h.ModPropBase", "Not modifying prop due to zero value " + ToString(nProperty));
#endif
        }

    }

    // -----------------------------------------------------------------------------
    /*
     *  @brief Applies the starting attribute modifiers from selecting a race
     *
     *  Reads the attribute modifiers for a race from the 2da and applies it
     *
     *  @param oChar    The character
     *  @param nRace    The race (index into race_base.xls)
     *  @param bUnApply Whether to apply (default) or unapply the modifier
     *
     *  @author Georg Zoeller
    */
    public void Chargen_ApplyRaceAttributeModifiers(GameObject oChar, int nRace, int bUnApply = EngineConstants.FALSE)
    {

#if DEBUG
        Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER,
            "sys_charge_h.ApplyRaceAttributeModifiers", "Setting Racial Modifiers for Race " + ToString(nRace) + "bUnApply:" + ToString(bUnApply != EngineConstants.FALSE));
#endif

        int nUnApply = (bUnApply != EngineConstants.FALSE) ? -1 : 1;

        // -------------------------------------------------------------------------
        // 1. Read the modifier values in
        //    - Clean this 2da mess up, there's a better structure than this.
        // -------------------------------------------------------------------------
        float fStrMod = GetM2DAFloat(EngineConstants.TABLE_RULES_RACES, "StrAdjust", nRace) * nUnApply;
        float fDexMod = GetM2DAFloat(EngineConstants.TABLE_RULES_RACES, "DexAdjust", nRace) * nUnApply;
        float fIntMod = GetM2DAFloat(EngineConstants.TABLE_RULES_RACES, "IntAdjust", nRace) * nUnApply;
        float fWillMod = GetM2DAFloat(EngineConstants.TABLE_RULES_RACES, "WillAdjust", nRace) * nUnApply;
        float fConMod = GetM2DAFloat(EngineConstants.TABLE_RULES_RACES, "ConAdjust", nRace) * nUnApply;
        float fMagMod = GetM2DAFloat(EngineConstants.TABLE_RULES_RACES, "MagAdjust", nRace) * nUnApply;

        // -------------------------------------------------------------------------
        // 2. Modify the creature properties based on the values
        // -------------------------------------------------------------------------
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, fStrMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, fDexMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE, fIntMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER, fWillMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION, fConMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC, fMagMod);

    }

    // -----------------------------------------------------------------------------
    /*
     *  @brief Applies the starting attribute modifiers from selecting a class
     *
     *  Reads the attribute modifiers for a class from the 2da and applies it
     *
     *  @param oChar    The character
     *  @param nRace    The class (index into cla_base.xls)
     *  @param bUnApply Whether to apply (default) or unapply the modifier
     *
     *  @author Georg Zoeller
    */
    public void Chargen_ApplyClassAttributeModifiers(GameObject oChar, int nClass, int bUnApply = EngineConstants.FALSE)
    {

        int nModifier = (bUnApply != EngineConstants.FALSE) ? -1 : 1;

        // -------------------------------------------------------------------------
        // 1. Read the modifier values in
        //    - Clean this 2da mess up, there's a better structure than this.
        // -------------------------------------------------------------------------
        float fStrMod = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "StrAdjust", nClass) * nModifier;
        float fDexMod = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "DexAdjust", nClass) * nModifier;
        float fIntMod = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "IntAdjust", nClass) * nModifier;
        float fWillMod = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "WillAdjust", nClass) * nModifier;
        float fConMod = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "ConAdjust", nClass) * nModifier;
        float fMagMod = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "MagAdjust", nClass) * nModifier;

        // -------------------------------------------------------------------------
        // 2. Modify the creature properties based on the values
        // -------------------------------------------------------------------------

#if DEBUG
        if (EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
        {
            Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "sys_chargen_h", "STR +" + ToString(fStrMod));
            Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "sys_chargen_h", "DEX +" + ToString(fDexMod));
            Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "sys_chargen_h", "CON +" + ToString(fConMod));
            Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "sys_chargen_h", "WIL +" + ToString(fWillMod));
            Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "sys_chargen_h", "MAG +" + ToString(fMagMod));
            Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "sys_chargen_h", "INT+" + ToString(fIntMod));
        }
#endif

        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, fStrMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, fDexMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE, fIntMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER, fWillMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION, fConMod);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC, fMagMod);

    }

    // -----------------------------------------------------------------------------
    /*
     *  @brief Applies the starting stat modifiers from selecting a class
     *
     *  Reads the statmodifiers for a class from the 2da and applies it
     *
     *  @param oChar    The character
     *  @param nRace    The class (index into cla_base.xls)
     *  @param bUnApply Whether to apply (default) or unapply the modifier
     *
     *  @author Georg Zoeller
    */
    public void Chargen_ApplyClassStatModifiers(GameObject oChar, int nClass, int bUnApply = EngineConstants.FALSE, float fRankModifier = 1.0f)
    {

        int nModifier = (bUnApply != EngineConstants.FALSE) ? -1 : 1;

        Log_Chargen("Chargen_ApplyClassStatModifiers", "nModifier: " + ToString(nModifier) + " nRankModifier" + ToString(fRankModifier) + " Player Rank:" + ToString(GetCreatureRank(oChar)));

        // -------------------------------------------------------------------------
        // 1. Read the modifier values in
        //    - Clean this 2da mess up, there's a better structure than this.
        // -------------------------------------------------------------------------

        float fBaseAttack = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "BaseAttack", nClass) * nModifier;
        float fBaseDefense = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "BaseDefense", nClass) * nModifier;

        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_ATTACK, fBaseAttack);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE, fBaseDefense);

        float fDepletableMod = GetClassDataFloat(EngineConstants.CLASS_DATA_BASE_MANA_STAMINA, nClass) * fRankModifier * nModifier;

        float fRankModifierHealth = GetAutoScaleDataFloat(GetCreatureRank(oChar), EngineConstants.AS_RANK_HEALTH_SCALE_FACTOR);

        //bugfix:
        if (fRankModifierHealth == 0.0f)
        {
#if DEBUG
            Warning("Creature with no rank (0) trying to apply class, causing 0 health. Please contact georg. Details: " + ToString(oChar) + " " + GetCurrentScriptName());
#endif
            fRankModifierHealth = 1.0f;
        }

        // we subtract 1 as the stat is initialized at 1 to prevent the character from dying

        float fHealthMod = (((GetClassDataFloat(EngineConstants.CLASS_DATA_BASE_HEALTH, nClass)) * fRankModifierHealth) * nModifier) - 1.0f;
        float fDamageMod = GetClassDataFloat(EngineConstants.CLASS_DATA_DAMAGE_BONUS, nClass);

        // -------------------------------------------------------------------------
        // 2. Modify the creature properties based on the values
        // -------------------------------------------------------------------------
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, fDepletableMod * nModifier);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, fHealthMod * nModifier);
        Chargen_ModifyCreaturePropertyBase(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_BONUS, fDamageMod * nModifier);

    }

    // -----------------------------------------------------------------------------
    /*
     *  @brief Applies the abilities granted to a character because of picking a class
     *
     *  Reads the class from cla_base.xls and applies it
     *
     *  @param oChar    The character
     *  @param nClass
     *  @param bUnApply Whether to apply (default) or unapply the modifier
     *
     *  @author Georg Zoeller
    */

    public void Chargen_ApplyClassAbilities(GameObject oChar, int nClass, int bUnApply = EngineConstants.FALSE)
    {
        // -------------------------------------------------------------------------
        // 1. Ability #1
        // -------------------------------------------------------------------------
        int nAbility = GetM2DAInt(EngineConstants.TABLE_RULES_CLASSES, "StartingAbility1", nClass);
        _AddAbility(oChar, nAbility, bUnApply);

        // -------------------------------------------------------------------------
        // 2. Ability #2
        // -------------------------------------------------------------------------
        nAbility = GetM2DAInt(EngineConstants.TABLE_RULES_CLASSES, "StartingAbility2", nClass);
        _AddAbility(oChar, nAbility, bUnApply);

        // -------------------------------------------------------------------------
        // Starting Skills
        // -------------------------------------------------------------------------
        if (nClass == EngineConstants.CLASS_WARRIOR)
        {
            _AddAbility(oChar, 100100);
        }
        else if (nClass == EngineConstants.CLASS_ROGUE)
        {
            _AddAbility(oChar, EngineConstants.ABILITY_SKILL_POISON_1);
        }
        else if (nClass == EngineConstants.CLASS_WIZARD)
        {
            _AddAbility(oChar, EngineConstants.ABILITY_SKILL_HERBALISM_1);
        }
    }

    public int ChargenGetBackgroundSkill(int nRace, int nBackground)
    {
        // the skill to add is found in the column postfixed with the race id of the character.
        string sCol = "Skill_" + ToString(nRace);
        int nSkill = GetM2DAInt(EngineConstants.TABLE_RULES_BACKGROUNDS, sCol, nBackground);
        return nSkill;

    }

    /*
     *  @brief
     *
     *      Selects the creature's gender.
     *
     *  @param oChar The character
     *
     *  @author Georg Zoeller
    */
    public void Chargen_SelectGender(GameObject oChar, int nGender)
    {
        // -------------------------------------------------------------------------
        // 1. Set the Gender
        //    - Write engine function to set gender
        //    - Obviously we don't want this to happen to existing creatures...
        // -------------------------------------------------------------------------
        SetCreatureGender(oChar, nGender);
    }

    /*
     *  @brief Generate a unique ID for each class_race_background combination
     *         to allow one step lookup into the 2da.
     *
     *  @author Georg Zoeller
    */
    public int Chargen_GetEquipIndex(int nRace, int nClass, int nBackground)
    {
        return nRace * 1000 + nClass * 100 + nBackground;
    }

    public void Chargen_GetItemForSlot(GameObject oCreature, int nSlot, int nWeaponSet = 1, int nClass = 0, int nIdx = 0)
    {
        /*
        int EngineConstants.INVENTORY_SLOT_MAIN                  =  0;
        int EngineConstants.INVENTORY_SLOT_OFFHAND               =  1;
        int EngineConstants.INVENTORY_SLOT_RANGEDAMMO            =  2;
        int EngineConstants.INVENTORY_SLOT_CHEST                 =  4;
        int EngineConstants.INVENTORY_SLOT_HEAD                  =  5;
        int EngineConstants.INVENTORY_SLOT_BOOTS                 =  6;
        int EngineConstants.INVENTORY_SLOT_GLOVES                =  7;
        */

        string r = String.Empty;
        if (nIdx != 0)
        {
            r = GetM2DAResource(EngineConstants.TABLE_STARTING_EQUIPMENT, "Slot" + IntToString(nSlot), nIdx);
        }

        if (r != EngineConstants.INVALID_RESOURCE)
        {
            GameObject oExists = GetItemInEquipSlot(nSlot, oCreature, nWeaponSet);
            DestroyObject(oExists);
            GameObject oItem = CreateItemOnObject(r, oCreature, 1, "", EngineConstants.TRUE, EngineConstants.FALSE);
            if (IsObjectValid(oItem) != EngineConstants.FALSE)
            {
                EquipItem(oCreature, oItem, nSlot, nWeaponSet);
            }
        }

    }

    public void Chargen_ClearInventory(GameObject oCreature)
    {
        List<GameObject> items = GetItemsInInventory(oCreature, EngineConstants.GET_ITEMS_OPTION_EQUIPPED);
        int nSize = GetArraySize(items);
        int i;

        for (i = 0; i < nSize; i++)
        {
            DestroyObject(items[i], 0);
        }
    }

    public void Chargen_InitInventory(GameObject oCreature, int nClass = 0, int nIdx = 0)
    {

#if DEBUG
        Log_Chargen("Chargen_InitInventory", "-- Initializing Inventory", oCreature);
#endif

        if (IsPartyMember(oCreature) != EngineConstants.FALSE)
        {
            Chargen_ClearInventory(oCreature);
        }

        // ---------------------------------------------------------------------
        // If we have an index, then a proper class/race/background combination was
        // passed.
        // ---------------------------------------------------------------------
        if (nIdx != 0)
        {

            // ---------------------------------------------------------------------
            // Load the starting equipment of the memory cached strings defined
            // in the 2da
            // ---------------------------------------------------------------------
            string sTemplate = GetM2DAString(EngineConstants.TABLE_STARTING_EQUIPMENT, "Template", nIdx);
            LoadItemsFromTemplate(oCreature, sTemplate, EngineConstants.TRUE);

            // ---------------------------------------------------------------------
            // Load the starting ability for the background
            // ---------------------------------------------------------------------
            int nAbility = GetM2DAInt(EngineConstants.TABLE_STARTING_EQUIPMENT, "Ability", nIdx);
            _AddAbility(oCreature, nAbility);

        }
        else
        {
            string sTemplate = "default_player.utc";
            switch (nClass)
            {
                case EngineConstants.CLASS_WARRIOR:
                    sTemplate = "default_warrior.utc";
                    break;
                case EngineConstants.CLASS_ROGUE:
                    sTemplate = "default_rogue.utc";
                    break;
                case EngineConstants.CLASS_WIZARD:
                    sTemplate = "default_wizard.utc";
                    break;
            }

            LoadItemsFromTemplate(oCreature, sTemplate, EngineConstants.TRUE);
        }
    }

    /*
     *  @brief Initializes a blank character at level 0
     *
     *  Sets up a blank character with level 0, initializes the default attribute
     *  values and removes any preexisting abilities, effects, etc.
     *
     *  @param oChar The character
     *
     *  @author Georg Zoeller
    */
    public void Chargen_InitializeCharacter(GameObject oChar, int bWipeAbilities /*TRUE*/)
    {

#if DEBUG
        Log_Chargen("Chargen_InitializeCharacter", " --------- INITIALIZE CHARACTER --------- ", oChar);
#endif

        // -------------------------------------------------------------------------
        // 1.  Clear all creature properties, resulting in an absolute empty creature
        //     - Engine_ClearInventory(oChar);
        //     - Engine_ClearAllAbilities(oChar);
        //     - Engine_ClearAllEffects(oChar);
        // -------------------------------------------------------------------------

        // -------------------------------------------------------------------------
        // 2. Set Creature to Level and Experience 0
        // -------------------------------------------------------------------------
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_LEVEL, 1.0f, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_EXPERIENCE, 0.0f, EngineConstants.PROPERTY_VALUE_BASE);

        // -------------------------------------------------------------------------
        // 3. Initialize all creature attributes to default value
        // -------------------------------------------------------------------------
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, EngineConstants.CHARGEN_BASE_ATTRIBUTE_VALUE, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY, EngineConstants.CHARGEN_BASE_ATTRIBUTE_VALUE, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_CONSTITUTION, EngineConstants.CHARGEN_BASE_ATTRIBUTE_VALUE, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_WILLPOWER, EngineConstants.CHARGEN_BASE_ATTRIBUTE_VALUE, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_INTELLIGENCE, EngineConstants.CHARGEN_BASE_ATTRIBUTE_VALUE, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC, EngineConstants.CHARGEN_BASE_ATTRIBUTE_VALUE, EngineConstants.PROPERTY_VALUE_BASE);

        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_ATTACK_SPEED_MODIFIER, 1.0f);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_SCALE, 1.0f);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_RESISTANCE_MENTAL, 0.0f);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_RESISTANCE_PHYSICAL, 0.0f);

        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_HEALING_BONUS, 1.0f);

        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_HEALTH_COMBAT, EngineConstants.REGENERATION_HEALTH_COMBAT_DEFAULT);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA_COMBAT, EngineConstants.REGENERATION_STAMINA_COMBAT_DEFAULT);

        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_HEALTH, EngineConstants.REGENERATION_HEALTH_EXPLORE_DEFAULT, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_REGENERATION_STAMINA, EngineConstants.REGENERATION_STAMINA_EXPLORE_DEFAULT, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_MISSILE_SHIELD, 0.0f);

        SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_SPEC_POINTS, 0.0f);

        // -------------------------------------------------------------------------
        // 4. Initialize all stats
        // -------------------------------------------------------------------------
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, 1.0f, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, 0.0f, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DEFENSE, 0.0f, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_ATTACK, 0.0f, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_DAMAGE_BONUS, 0.0f, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_ATTRIBUTE_FLANKING_ANGLE, 60.0f, EngineConstants.PROPERTY_VALUE_BASE);

        // -------------------------------------------------------------------------
        // 6. Set up points available to distribute (skills only on humanoids)
        // -------------------------------------------------------------------------
        if (IsHumanoid(oChar) != EngineConstants.FALSE)
        {
            SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_SKILL_POINTS, 1.0f, EngineConstants.PROPERTY_VALUE_BASE);
        }
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_ATTRIBUTE_POINTS, 5.0f, EngineConstants.PROPERTY_VALUE_BASE);
        SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_TALENT_POINTS, 2.0f, EngineConstants.PROPERTY_VALUE_BASE);

        // -------------------------------------------------------------------------
        // Undo Class and Race selection
        // -------------------------------------------------------------------------
        int nClass = GetCreatureCoreClass(oChar);
        if (bWipeAbilities != EngineConstants.FALSE)
        {

#if DEBUG
            //Something wants to wipe the abilities, double check
            Log_Chargen("Chargen_InitializeCharacter", "* Clearing Ability List", oChar);
#endif

            //DHK Comment-disable block
            /*// We don't wipe the list if any hidden traits are present and
            // the target is not a party member to avoid losing presets made in the
            // toolset
            //if (GetAbilityCount(oChar, EngineConstants.ABILITY_TYPE_TALENT, 18) == 0 || 
            if (IsPartyMember(oChar) != EngineConstants.FALSE)
            {
                // Note: This wipes the ability list clear
                CharGen_ClearAbilityList(oChar, EngineConstants.ABILITY_TYPE_TALENT);
                CharGen_ClearAbilityList(oChar, EngineConstants.ABILITY_TYPE_SPELL);
                CharGen_ClearAbilityList(oChar, EngineConstants.ABILITY_TYPE_SKILL);
            }
            else
            {
#if DEBUG
                Log_Chargen("Chargen_InitializeCharacter", "* FAILED - Creature has talents of type 18", oChar);
#endif
            }*/
            //end comment
        }

        // -------------------------------------------------------------------------
        // add player hidden talent (used to show some special abilities)
        // -------------------------------------------------------------------------
        if (IsHero(oChar) != EngineConstants.FALSE)
        {
#if DEBUG
            Log_Chargen("Chargen_InitializeCharacter", "* Adding hidden player talent.", oChar);
#endif
            AddAbility(oChar, 4001, EngineConstants.FALSE);
        }

        if (IsPartyMember(oChar) != EngineConstants.FALSE)
        {
            SetCreatureRank(oChar, EngineConstants.CREATURE_RANK_PLAYER /*RANK_PLAYER*/);
        }

    }

    /*
     *  @brief applies the race modifiers based on a character's chosen race
     *
     *  @param oChar The character
     *
     *  @author Georg Zoeller
    */
    public void Chargen_ApplyRaceModifiers(GameObject oChar, int bUndo = EngineConstants.FALSE)
    {

        int nRace = GetCreatureRacialType(oChar);

        Log_Chargen("Chargen_ApplyRaceModifiers", (bUndo != EngineConstants.FALSE ? "Un" : "") + "Applying Race Modifiers", oChar);
        // -------------------------------------------------------------------------
        // 1. Setup the Racial Attribute Modifiers
        // -------------------------------------------------------------------------
        Chargen_ApplyRaceAttributeModifiers(oChar, nRace, bUndo);

        // -------------------------------------------------------------------------
        // 2. Ability #1
        // -------------------------------------------------------------------------
        int nAbility = GetM2DAInt(EngineConstants.TABLE_RULES_RACES, "Ability1", nRace);
        if (bUndo != EngineConstants.FALSE)
        {
            RemoveAbility(oChar, nAbility);
        }
        else
        {
            _AddAbility(oChar, nAbility, EngineConstants.FALSE);
        }

        // -------------------------------------------------------------------------
        // 3. Ability #2
        // -------------------------------------------------------------------------
        nAbility = GetM2DAInt(EngineConstants.TABLE_RULES_RACES, "Ability2", nRace);
        if (bUndo != EngineConstants.FALSE)
        {
            RemoveAbility(oChar, nAbility);
        }
        else
        {
            _AddAbility(oChar, nAbility, EngineConstants.FALSE);

        }
    }

    /*
     *  @brief Selects the character's race.
     *
     *      Selects the character's race including the corresponding attribute modifiers and abilities.
     *
     *  @param oChar The character
     *
     *  @author Georg Zoeller
    */
    public void Chargen_SelectRace(GameObject oChar, int nRace, int bUndo = EngineConstants.FALSE)
    {

        Log_Chargen("Chargen_SelectRace", "-- " + (bUndo != EngineConstants.FALSE ? "Un" : "") + "Selecting Race: " + ToString(nRace), oChar);

        if (bUndo == EngineConstants.FALSE && nRace != GetCreatureRacialType(oChar))
        {
            // -------------------------------------------------------------------------
            // 1. SetAppearance
            //    - Retrieve the appearance for the race from races.xls
            //    - Set Appearance
            //    - Set Racial Type
            // -------------------------------------------------------------------------

            if (nRace == EngineConstants.RACE_HUMAN || nRace == EngineConstants.RACE_DWARF || nRace == EngineConstants.RACE_ELF)
            {
                int nApp = GetM2DAInt(EngineConstants.TABLE_RULES_RACES, "Appearance", nRace);
                if (GetCreatureRacialType(oChar) != nRace)
                {
                    SetAppearanceType(oChar, nApp, EngineConstants.TRUE);
                    SetCreatureRacialType(oChar, nRace);
                }

            }
        }

        // Humanoids gain the 'humanoid' trait (prereq for skills)
        if (bUndo != EngineConstants.FALSE)
        {
            RemoveAbility(oChar, EngineConstants.ABILITY_TALENT_TRAIT_HUMANOID);
        }
        else if (IsHumanoid(oChar) != EngineConstants.FALSE)
        {
            AddAbility(oChar, EngineConstants.ABILITY_TALENT_TRAIT_HUMANOID, EngineConstants.FALSE);
        }

        // -------------------------------------------------------------------------
        // Dwaves gain dwarven resistance free.
        // -------------------------------------------------------------------------
        if (nRace == EngineConstants.RACE_DWARF)
        {
            if (bUndo == EngineConstants.FALSE)
            {
                AddAbility(oChar, EngineConstants.ABILITY_SKILL_DWARVEN_RESISTANCE, EngineConstants.FALSE);
            }
            else
            {
                RemoveAbility(oChar, EngineConstants.ABILITY_SKILL_DWARVEN_RESISTANCE);
            }
        }

        Chargen_ApplyRaceModifiers(oChar, bUndo);
    }

    public void Chargen_SelectBackground(GameObject oChar, int nBackground, int bUnApply = EngineConstants.FALSE)
    {

        Log_Chargen("Chargen_SelectBackground", "-- " + (bUnApply != EngineConstants.FALSE ? "Un" : "") + "Selecting BG: " + ToString(nBackground), oChar);

        // -------------------------------------------------------------------------
        // 1. Set the background variable
        //          - Create creature property (or check what we used so far
        //          - We don't set backgrounds on non player generated chars..
        // -------------------------------------------------------------------------

        if (bUnApply != EngineConstants.FALSE)
        {
            SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_BACKGROUND, 0.0f, EngineConstants.PROPERTY_VALUE_BASE);
        }
        else
        {
            SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_BACKGROUND, IntToFloat(nBackground), EngineConstants.PROPERTY_VALUE_BASE);
        }

        // -------------------------------------------------------------------------
        // 2. Give one skill
        //    - retrieve the skill that is granted by the background from backgrounds.xls
        //    - give it to the player.
        // -------------------------------------------------------------------------

        int nAbility = 0;
        int nAbility1 = 0;

        switch (nBackground)
        {
            case EngineConstants.BACKGROUND_CITY: nAbility = EngineConstants.ABILITY_SKILL_PERSUADE_1; break;
            case EngineConstants.BACKGROUND_COMMONER: nAbility = EngineConstants.ABILITY_SKILL_STEALING_1; break;
            case EngineConstants.BACKGROUND_DALISH: nAbility = EngineConstants.ABILITY_SKILL_SURVIVAL_1; break;
            case EngineConstants.BACKGROUND_MAGI: nAbility = EngineConstants.ABILITY_SKILL_COMBAT_TACTICS_1; break;
            case EngineConstants.BACKGROUND_NOBLE: nAbility = (HasAbility(oChar, EngineConstants.ABILITY_SKILL_COMBAT_TRAINING_1) == EngineConstants.FALSE) ? EngineConstants.ABILITY_SKILL_COMBAT_TRAINING_1 : EngineConstants.ABILITY_SKILL_COMBAT_TRAINING_2; break;
        }

        if (nAbility != EngineConstants.FALSE)
        {
            _AddAbility(oChar, nAbility, bUnApply);
        }

    }

    /*
     *  @brief Sets the character's core clas
     *
     *  Sets the character's core class, including attributes, abilities and stats
     *
     *  @param oChar The character
     *  @param nClas The id of the core class (EngineConstants.CLASS_*)
     *
     *  @author Georg Zoeller
    */
    public void Chargen_SelectCoreClass(GameObject oChar, int nClass, int bUnApply = EngineConstants.FALSE)
    {

        Log_Chargen("Chargen_SelectCoreClass", "-- " + (bUnApply != EngineConstants.FALSE ? "Un" : "") + "Selecting Class: " + ToString(nClass), oChar);

        // -------------------------------------------------------------------------
        // 1. Set current class
        //        - Check engine implications
        //        - Get function to reset class array
        // -------------------------------------------------------------------------
        if (bUnApply != EngineConstants.FALSE)
        {
            SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_CURRENT_CLASS, 0.0f, EngineConstants.PROPERTY_VALUE_BASE);
        }
        else
        {
            SetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_CURRENT_CLASS, IntToFloat(nClass), EngineConstants.PROPERTY_VALUE_BASE);
        }

        // -------------------------------------------------------------------------
        // 2. Apply Class Specific Attribute Modifiers
        // -------------------------------------------------------------------------
        Chargen_ApplyClassAttributeModifiers(oChar, nClass, bUnApply);

        // -------------------------------------------------------------------------
        // 3. Set the initial character stats based on Class (Health, Mana, etc..)
        // -------------------------------------------------------------------------
        Chargen_ApplyClassStatModifiers(oChar, nClass, bUnApply);

        // -------------------------------------------------------------------------
        // 4. Grant class abilities  (hidden class talent)
        // -------------------------------------------------------------------------
        Chargen_ApplyClassAbilities(oChar, nClass, bUnApply);

    }

    /*
     *  @brief
     *
     *  <Description>
     *
     *  @param oChar The character
     *
     *  @author Georg Zoeller
    */
    public int Chargen_SpendAttributePoints(GameObject oChar, int nAttribute, int nPoints, int bLevelup = EngineConstants.TRUE)
    {

        Log_Chargen("Chargen_SpendAttributePoints", "-- Spending " + ToString(nPoints) + " on " + ToString(nAttribute), oChar);

        // -------------------------------------------------------------------------
        // 1. Distribute Attribute Points
        // -------------------------------------------------------------------------
        Chargen_ModifyCreaturePropertyBase(oChar, nAttribute, IntToFloat(nPoints));

        return EngineConstants.TRUE;

    }

    public void Chargen_SetupPlotFlags(GameObject oChar)
    {
        int nRace = GetCreatureRacialType(oChar);
        int nBackground = FloatToInt(GetCreatureProperty(oChar, EngineConstants.PROPERTY_SIMPLE_BACKGROUND, EngineConstants.PROPERTY_VALUE_BASE));

        Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "sys_chargen_h", "Setting plot flags, race: " + IntToString(nRace) + ", background: " + IntToString(nBackground));

        // First, init all flags (debug setup)
        WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_HUMAN_COMMONER, EngineConstants.FALSE);
        WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_HUMAN_NOBLE, EngineConstants.FALSE);
        WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_CIRCLE, EngineConstants.FALSE);
        WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_COMMONER, EngineConstants.FALSE);
        WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_NOBLE, EngineConstants.FALSE);
        WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_ELF_CITY, EngineConstants.FALSE);
        WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_ELF_DALISH, EngineConstants.FALSE);
        WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_CIRCLE, EngineConstants.FALSE);
        switch (nRace)
        {
            case EngineConstants.RACE_HUMAN:
                {
                    switch (nBackground)
                    {
                        case EngineConstants.BACKGROUND_COMMONER: WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_HUMAN_COMMONER, EngineConstants.TRUE); break;
                        case EngineConstants.BACKGROUND_NOBLE: WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_HUMAN_NOBLE, EngineConstants.TRUE); break;
                        case EngineConstants.BACKGROUND_MAGI: WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_CIRCLE, EngineConstants.TRUE); break;
                    }
                    break;
                }
            case EngineConstants.RACE_DWARF:
                {
                    switch (nBackground)
                    {
                        case EngineConstants.BACKGROUND_COMMONER: WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_COMMONER, EngineConstants.TRUE); break;
                        case EngineConstants.BACKGROUND_NOBLE: WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_DWARF_NOBLE, EngineConstants.TRUE); break;
                    }
                    break;
                }
            case EngineConstants.RACE_ELF:
                {
                    switch (nBackground)
                    {
                        case EngineConstants.BACKGROUND_CITY: WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_ELF_CITY, EngineConstants.TRUE); break;
                        case EngineConstants.BACKGROUND_DALISH: WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_ELF_DALISH, EngineConstants.TRUE); break;
                        case EngineConstants.BACKGROUND_MAGI: WR_SetPlotFlag(EngineConstants.PLT_GEN00PT_BACKGROUNDS, EngineConstants.GEN_BACK_CIRCLE, EngineConstants.TRUE); break;
                    }
                    break;
                }
        }
    }

    // enabling proper tactics presets based on class (might change at levelup)
    public void Chargen_EnableTacticsPresets(GameObject oCreature)
    {
        int nCoreClass = GetCreatureCoreClass(oCreature);
        int nTotalPresetsNum = GetM2DARows(EngineConstants.TABLE_TACTICS_USER_PRESETS);
        int j;
        int nForClass1, nForClass2, nForClass3;
        int nPresetTable;
        int nCurrentRow;
        for (j = 0; j < nTotalPresetsNum; j++)
        {
            nCurrentRow = GetM2DARowIdFromRowIndex(EngineConstants.TABLE_TACTICS_USER_PRESETS, j);
            nPresetTable = GetM2DAInt(EngineConstants.TABLE_TACTICS_USER_PRESETS, "TacticsTable", nCurrentRow);
            if (nPresetTable > 0)
            {
                nForClass1 = GetM2DAInt(EngineConstants.TABLE_TACTICS_USER_PRESETS, "ValidForClass1", nCurrentRow);
                nForClass2 = GetM2DAInt(EngineConstants.TABLE_TACTICS_USER_PRESETS, "ValidForClass2", nCurrentRow);
                nForClass3 = GetM2DAInt(EngineConstants.TABLE_TACTICS_USER_PRESETS, "ValidForClass3", nCurrentRow);
                if (nCoreClass == nForClass1 || nCoreClass == nForClass2 || nCoreClass == nForClass3)
                    AddTacticPresetID(oCreature, nCurrentRow);
            }
        }

    }

    public void Chargen_LoadPresetsTable(GameObject oCreature, int nPresetID)
    {
        int nTableID = GetM2DAInt(EngineConstants.TABLE_TACTICS_USER_PRESETS, "TacticsTable", nPresetID);
        if (nTableID != 0)
        {
            int nRows = GetM2DARows(nTableID);
            int nMaxTactics = GetNumTactics(oCreature);

            int nTacticsEntry = 1;
            int i;
            int nCurrentRow;
            for (i = 0; i < nRows && nTacticsEntry <= nMaxTactics; ++i)
            {
                int bAddEntry = EngineConstants.FALSE;
                nCurrentRow = GetM2DARowIdFromRowIndex(nTableID, i);
                Log_Trace(EngineConstants.LOG_CHANNEL_CHARACTER, "Chargen_LoadPresetsTable", "setting presets, index: " + IntToString(i) + ", row: " + IntToString(nCurrentRow));
                int nTargetType = GetM2DAInt(nTableID, "TargetType", nCurrentRow);
                int nCondition = GetM2DAInt(nTableID, "Condition", nCurrentRow);
                int nCommandType = GetM2DAInt(nTableID, "Command", nCurrentRow);
                int nCommandParam = GetM2DAInt(nTableID, "SubCommand", nCurrentRow);

                int nUseType = GetM2DAInt(EngineConstants.TABLE_COMMAND_TYPES, "UseType", nCommandType);
                if (nUseType == 0)
                {
                    bAddEntry = EngineConstants.TRUE;
                }
                else
                {
                    bAddEntry = HasAbility(oCreature, nCommandParam);
                }

                if (bAddEntry != EngineConstants.FALSE)
                {
                    SetTacticEntry(oCreature, nTacticsEntry, EngineConstants.TRUE, nTargetType, nCondition, nCommandType, nCommandParam);
                    ++nTacticsEntry;
                }
            }
        }
    }

    // Will re-populate the creature's tactic table if the creature still has a pre-defined preset
    // it will do nothing if the creature changed the perset or has nothing selected
    public void Chargen_PopulateTacticsForPreset(GameObject oCreature)
    {
        int nCurrentPreset = GetTacticPresetID(oCreature);
        if (nCurrentPreset > 0) // ==> still has a valid preset - re-populate list
        {
            Chargen_LoadPresetsTable(oCreature, nCurrentPreset);
        }

    }
}