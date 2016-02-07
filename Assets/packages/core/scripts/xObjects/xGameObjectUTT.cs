using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class xGameObjectUTT : xGameObjectBase {
    #region valuables initialize from UTT
    public string ResRefName { get; set; }
    public bool LocalCopy { get; set; }
    public string Variable2da { get; set; }
    public Dictionary<string, string> VariableList { get; set; }
    public string Tag { get; set; }
    public int ScriptURI { get; set; }
    public int Group { get; set; }
    public string LinkedTo { get; set; }
    public string Comments { get; set; }

    #endregion

    #region variables initialized from ARE
    public Vector3 position { get; set; }
    public Vector3 orientation { get; set; }
    //public string Tag { get; set; }
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
    public int Appearance { get; set; }
    public int InitialAnimation { get; set; }
    public int ParentInitialAnimation { get; set; }
    public int PickLockLevel { get; set; }
    public int ParentPickLockLevel { get; set; }
    public int Team { get; set; }
    public int ParentTeam { get; set; }
    public bool Trackable { get; set; }
    public bool WeaponsDrawn { get; set; }
    public int RoomID1 { get; set; }
    public int RoomID2 { get; set; }
    public int Rank { get; set; }
    public int ParentRank { get; set; }
    public int TreasureCategory { get; set; }
    public int ParentTreasureCategory { get; set; }
    public float InteractionRadius { get; set; }
    public int TrapDetectionDifficulty { get; set; }
    public int ParentTrapDetectionDifficulty { get; set; }
    public int TrapDisarmDifficulty { get; set; }
    public int ParentTrapDisarmDifficulty { get; set; }
    public int Useable { get; set; }
    public int ParentUsable { get; set; }
    //unused?
    /*CloseToOpenSound
    OpenToCloseSound
    DestroyedSound
    UsedSound
    LockedSound
    HitSound*/
    //public string Variable2da { get; set; }
    public Dictionary<string, string> ParentVariableList { get; set; }//unused?
    //public Dictionary<string, string> VariableList { get; set; }
    #endregion

    //Is this needed here? Or on character UTC? Both?
    public List<xThreat> oThreats { get; set; }

    // Use this for initialization
    void Awake() {
        if (oThreats == null) oThreats = new List<xThreat>();
        if (GeometryList == null) GeometryList = new List<Vector3>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
