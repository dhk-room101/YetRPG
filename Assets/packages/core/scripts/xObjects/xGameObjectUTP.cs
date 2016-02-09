using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class xGameObjectUTP : xGameObjectBase {

    #region variables initialized from UTP
    public string ResRefName { get; set; }
    public bool LocalCopy { get; set; }
    public string Variable2da { get; set; }
    public Dictionary<string, string> VariableList { get; set; }
    public List<GameObject> InventoryList { get; set; }
    public string xname { get; set; }
    public int NameStringID { get; set; }
    public bool NameRequiresReTranslation { get; set; }
    public string PopupText { get; set; }
    public int PopupTextStringID { get; set; }
    public bool PopupTextRequiresReTranslation { get; set; }
    public string Tag { get; set; }
    public int ScriptURI { get; set; }
    public int ConversationURI { get; set; }
    public int PathfindingPatchesURI { get; set; }
    public int Group { get; set; }
    public string Comments { get; set; }
    public int Appearance { get; set; }
    public int InitialAnimation { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public bool Indestructible { get; set; }
    public int PickLockLevel { get; set; }
    public string KeyTag { get; set; }
    public bool AutoRemoveKey { get; set; }
    public int TrapDetectionDifficulty { get; set; }
    public int TrapDisarmDifficulty { get; set; }
    public bool KeyRequired { get; set; }
    public bool Useable { get; set; }//Misspelled in original Dragon age
    public bool Plot { get; set; }
    public int CharacterURI { get; set; }
    public int Rank { get; set; }
    public int TreasureCategory { get; set; }
    /*		<CloseToOpenSound/>
		<OpenToCloseSound/>
		<DestroyedSound/>
		<UsedSound/>
		<LockedSound/>
		<HitSound/>*/
    public int Team { get; set; }
    #endregion

    #region variables initialized from ARE
    public Vector3 position { get; set; }
    public Vector3 orientation { get; set; }
    //public string Tag { get; set; }//from UTP
    public List<Vector3> GeometryList { get; set; }//vertex
    public int ObjectType { get; set; }
    public Guid GUID { get; set; }
    public List<Guid> AssociatedInstanceGUIDList { get; set; }
    public int ObjectURI { get; set; }
    public string ObjectResRefName { get; set; }
    public string ObjectName { get; set; }
    public string ParentTag { get; set; }
    public bool Active { get; set; }
    public int Importance { get; set; }
    //public int Appearance { get; set; }//from UTP
    //public int InitialAnimation { get; set; }
    public int ParentInitialAnimation { get; set; }
    //public int PickLockLevel { get; set; }
    public int ParentPickLockLevel { get; set; }
    //public int Team { get; set; }//from UTP
    public int ParentTeam { get; set; }
    public bool Trackable { get; set; }
    public bool WeaponsDrawn { get; set; }
    public int RoomID1 { get; set; }
    public int RoomID2 { get; set; }
    //public int Rank { get; set; }//from UTP
    public int ParentRank { get; set; }
    //public int TreasureCategory { get; set; }//from UTP
    public int ParentTreasureCategory { get; set; }
    public float InteractionRadius { get; set; }
    //public int TrapDetectionDifficulty { get; set; }
    public int ParentTrapDetectionDifficulty { get; set; }
    //public int TrapDisarmDifficulty { get; set; }
    public int ParentTrapDisarmDifficulty { get; set; }
    //public int Useable { get; set; }
    public int ParentUseable { get; set; }
    public string CloseToOpenSound { get; set; }
    public string OpenToCloseSound { get; set; }
    public string DestroyedSound { get; set; }
    public string UsedSound { get; set; }
    public string LockedSound { get; set; }
    public string HitSound { get; set; }
    //public string Variable2da { get; set; }//from UTP
    //public Dictionary<string, string> ParentVariableList { get; set; }//unused?
    //public Dictionary<string, string> VariableList { get; set; }//from UTP
    #endregion

    #region VariableList
    public int PLC_COUNTER_1 { get; set; }
    public int PLC_COUNTER_2 { get; set; }
    public int PLC_COUNTER_3 { get; set; }
    public int PLC_DO_ONCE_A { get; set; }
    public int PLC_DO_ONCE_B { get; set; }
    public string PLC_AT_DEST_TAG { get; set; }
    public int PLC_FLIP_COVER_USE_COUNT { get; set; }
    public string PLC_AT_DEST_AREA_TAG { get; set; }
    public GameObject PLC_FLIP_COVER_CREATURE_1 { get; set; }
    public int PLC_TRAP_TYPE { get; set; }
    public string PLC_AT_WORLD_MAP_ACTIVE_1 { get; set; }
    public string PLC_AT_WORLD_MAP_ACTIVE_2 { get; set; }
    public string PLC_AT_WORLD_MAP_ACTIVE_3 { get; set; }
    public string PLC_AT_WORLD_MAP_ACTIVE_4 { get; set; }
    public int TS_OVERRIDE_CATEGORY { get; set; }
    public int TS_OVERRIDE_RANK { get; set; }
    public int TS_OVERRIDE_MONEY { get; set; }
    public float TS_OVERRIDE_ITEM { get; set; }
    public int PLC_TRAP_DETECTED { get; set; }
    public GameObject PLC_TRAP_OWNER { get; set; }
    public string PLC_TRAP_SIGNAL_TAG { get; set; }
    public float PLC_TRAP_REARM_DELAY { get; set; }
    public int PLC_CODEX_FLAG { get; set; }
    public string PLC_CODEX_PLOT { get; set; }
    public float TS_OVERRIDE_HIGH { get; set; }
    public float TS_OVERRIDE_EQUIPMENT { get; set; }
    public int TS_OVERRIDE_SCALING { get; set; }
    public int TS_OVERRIDE_STEALING { get; set; }
    public string PLC_AT_WORLD_MAP_ACTIVE_5 { get; set; }
    public int TS_TREASURE_GENERATED { get; set; }
    public int PLC_TRAP_DEACTIVATE { get; set; }
    public int PLC_TRAP_TEAM { get; set; }
    public int PLC_TRAP_AOE { get; set; }
    public float PLC_TRAP_SIGNAL_HEIGHT { get; set; }
    public int PLC_SPAWN_NON_INTERACTIVE { get; set; }
    public string PLC_TRAP_ITEM { get; set; }

    //DHK
    public int PLC_ACTION { get; set; }
    public int PLC_ACTION_RESULT { get; set; }
    #endregion

    // Use this for initialization
    void Awake() {
        //Damn strings have to be initialized
        if (PLC_AT_WORLD_MAP_ACTIVE_1 == null) PLC_AT_WORLD_MAP_ACTIVE_1 = string.Empty;
        if (PLC_AT_WORLD_MAP_ACTIVE_2 == null) PLC_AT_WORLD_MAP_ACTIVE_2 = string.Empty;
        if (PLC_AT_WORLD_MAP_ACTIVE_3 == null) PLC_AT_WORLD_MAP_ACTIVE_3 = string.Empty;
        if (PLC_AT_WORLD_MAP_ACTIVE_4 == null) PLC_AT_WORLD_MAP_ACTIVE_4 = string.Empty;
        if (PLC_AT_WORLD_MAP_ACTIVE_5 == null) PLC_AT_WORLD_MAP_ACTIVE_5 = string.Empty;
        if (PLC_CODEX_PLOT == null) PLC_CODEX_PLOT = string.Empty;

        if (VariableList == null) VariableList = new Dictionary<string, string>();
        if (InventoryList == null) InventoryList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
