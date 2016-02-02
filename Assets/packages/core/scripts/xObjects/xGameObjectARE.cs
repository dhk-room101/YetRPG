using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class xGameObjectARE : xGameObjectBase {

    #region variables initialize from ARE
    public string ResRefName { get; set; }
    public bool LocalCopy { get; set; }
    public string Variable2da { get; set; }
    public Dictionary<string, string> VariableList { get; set; }
    public string AssociatedResourceList { get; set; }
    public string xname { get; set; }
    public int NameStringID { get; set; }
    public bool NameRequiresReTranslation { get; set; }
    public int ScriptURI { get; set; }
    public int AreaListURI { get; set; }
    public string Comments { get; set; }
    public string ReverbPreset { get; set; }
    public string MusicPreset { get; set; }
    public bool NoTeleport { get; set; }
    public string AreaMap { get; set; }
    public string arealayout { get; set; }
    public int AreaMapNorth { get; set; }
    public List<GameObject> ObjectList { get; set; }
    public List<GameObject> WaypointList { get; set; }
    public List<GameObject> ReverbVolumeList { get; set; }
    public List<GameObject> AudioVolumeList { get; set; }
    public List<GameObject> MusicVolumeList { get; set; }
    public List<GameObject> SoundList { get; set; }

    #endregion

    #region VariableList
    public int AREA_COUNTER_1 { get; set; }
    public int AREA_COUNTER_2 { get; set; }
    public int AREA_COUNTER_3 { get; set; }
    public int AREA_DO_ONCE_A { get; set; }
    public int AREA_DO_ONCE_B { get; set; }
    public int ENTERED_FOR_THE_FIRST_TIME { get; set; }
    public string CODEX_PLOT_NAME_1 { get; set; }
    public int CODEX_PLOT_FLAG_1 { get; set; }
    public string CODEX_PLOT_NAME_2 { get; set; }
    public int CODEX_PLOT_FLAG_2 { get; set; }
    public string CODEX_PLOT_NAME_3 { get; set; }
    public int CODEX_PLOT_FLAG_3 { get; set; }
    public string CODEX_PLOT_NAME_4 { get; set; }
    public int CODEX_PLOT_FLAG_4 { get; set; }
    public string CODEX_PLOT_NAME_5 { get; set; }
    public int CODEX_PLOT_FLAG_5 { get; set; }
    public string CODEX_PLOT_NAME_6 { get; set; }
    public int CODEX_PLOT_FLAG_6 { get; set; }
    public string CODEX_PLOT_NAME_7 { get; set; }
    public int CODEX_PLOT_FLAG_7 { get; set; }
    public string CODEX_PLOT_NAME_8 { get; set; }
    public int CODEX_PLOT_FLAG_8 { get; set; }
    public string CODEX_PLOT_NAME_9 { get; set; }
    public int CODEX_PLOT_FLAG_9 { get; set; }
    public string CODEX_PLOT_NAME_10 { get; set; }
    public int CODEX_PLOT_FLAG_10 { get; set; }
    public int AREA_WORLD_MAP_ENABLED { get; set; }
    public int AREA_DEBUG { get; set; }
    public int AREA_PARTY_CAMP { get; set; }
    public int PARTY_PICKER_ENABLED { get; set; }
    public int AREA_GAME_MODE_OVERRIDE { get; set; }
    public int AREA_COUNTER_4 { get; set; }
    public int AREA_COUNTER_5 { get; set; }
    public int AREA_COUNTER_6 { get; set; }
    public int AREA_COUNTER_7 { get; set; }
    public int AREA_COUNTER_8 { get; set; }
    public int INJURY_FLAG { get; set; }
    public int AREA_ID { get; set; }
    public int AREA_COUNTER_9 { get; set; }
    public int AREA_COUNTER_10 { get; set; }
    public int AREA_COUNTER_11 { get; set; }
    public int AREA_COUNTER_12 { get; set; }
    public string WORLD_MAP_NOTE_TAG { get; set; }
    public string WORLD_MAP_TAG { get; set; }
    public int AREA_NOTIFICATION_SHOWN { get; set; }
    #endregion

    // Use this for initialization
    void Awake () {
        if (VariableList == null) VariableList = new Dictionary<string, string>();
        if (ObjectList == null) ObjectList = new List<GameObject>();
        if (WaypointList == null) WaypointList = new List<GameObject>();
        if (ReverbVolumeList == null) ReverbVolumeList = new List<GameObject>();
        if (AudioVolumeList == null) AudioVolumeList = new List<GameObject>();
        if (MusicVolumeList == null) MusicVolumeList = new List<GameObject>();
        if (SoundList == null) SoundList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
