using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class xGameObjectUTI : xGameObjectBase
{
    #region variables initialized from UTI
    public string ResRefName { get; set; }
    public bool LocalCopy { get; set; }
    public string Variable2da { get; set; }
    public Dictionary<string, string> VariableList { get; set; }
    public string xname { get; set; }
    public int NameStringID { get; set; }
    public bool NameRequiresReTranslation { get; set; }
    public string Description { get; set; }
    public int DescriptionStringID { get; set; }
    public bool DescriptionRequiresReTranslation { get; set; }
    public int Ability { get; set; }
    public int ScriptURI { get; set; }
    public int BaseItemType { get; set; }
    public int RecipeType { get; set; }
    public int Appearance { get; set; }
    public int MaterialType { get; set; }
    public int MaterialProgression { get; set; }
    public int TintOverride { get; set; }
    public int Heraldry { get; set; }
    public string Tag { get; set; }
    public bool Plot { get; set; }
    public bool Damaged { get; set; }
    public string icon { get; set; }
    public int CostOverride { get; set; }
    public string Comments { get; set; }
    public int ModelAppearance { get; set; }
    public int ModelGender { get; set; }
    public bool Unique { get; set; }
    public int InventorySubgroup { get; set; }
    public List<xProperty> ItemPropertyList { get; set; }
    public int OnHitEffectID { get; set; }
    public int OnHitPower { get; set; }
    public int BodyTintMask { get; set; }
    public int BodyTint { get; set; }
    #endregion

    #region variables initialized from UTC
    public int ItemURI { get; set; }
    public int StackSize { get; set; }
    public bool Stealable { get; set; }
    public bool Droppable { get; set; }
    public int Slot { get; set; }
    public bool Infinite { get; set; }
    public int SetNumber { get; set; }
    #endregion

    #region VariableList
    public int APP_ITEM_MOTIVATION { get; set; }
    public int ITEM_COUNTER_1 { get; set; }
    public int ITEM_COUNTER_2 { get; set; }
    public int ITEM_SPECIALIZATION_FLAG { get; set; }
    public int ITEM_DO_ONCE_A { get; set; }
    public int ITEM_DO_ONCE_B { get; set; }
    public int ITEM_SEND_ACQUIRED_EVENT { get; set; }
    public int ITEM_CRUST { get; set; }
    public int ITEM_CODEX_FLAG { get; set; }
    public int ITEM_SET { get; set; }
    public int ITEM_SEND_LOST_EVENT { get; set; }
    public int PROJECTILE_OVERRIDE { get; set; }
    public int ITEM_RUNE_ENABLED { get; set; }
    #endregion

    // Use this for initialization
    void Awake()
    {
        if (VariableList == null) VariableList = new Dictionary<string, string>();
        if (ItemPropertyList == null) ItemPropertyList = new List<xProperty>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
