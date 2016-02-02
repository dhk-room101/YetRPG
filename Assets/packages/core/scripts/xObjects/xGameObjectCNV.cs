using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class xGameObjectCNV : xGameObjectBase {
    public string ResRefName { get; set; }
    public bool LocalCopy { get; set; }
    public int EndConversationScriptURI { get; set; }
    public int EndConversationParameter { get; set; }
    public string EndConversationParameterText { get; set; }
    public int EndConversationPlotURI { get; set; }
    public int EndConversationPlotFlag { get; set; }
    public bool EndConversationResult { get; set; }
    public string Comment { get; set; }
    public string DefaultNPCSpeaker { get; set; }
    public string DefaultNPCListener { get; set; }
    public string DefaultPCListener { get; set; }
    public int StageURI { get; set; }
    public string StageTag { get; set; }
    public bool StageAtCurrentLocation { get; set; }
    public bool AllGesturesLocked { get; set; }
    public bool AllPosesLocked { get; set; }
    public bool AllRoboBradLocked { get; set; }
    public bool AllCameraLocked { get; set; }
    public bool AmbientSoundSetType { get; set; }
    public bool OwnerIsHenchman { get; set; }
    public bool VOHardTimeRestriction { get; set; }
    public int PreviewAreaURI { get; set; }
    public bool PreviewStageUseFirstMatch { get; set; }
    public Vector3 PreviewStagePosition { get; set; }
    public Vector3 PreviewStageOrientation { get; set; }
    public bool IsSoundset { get; set; }

    public List<int> StartList { get; set; }
    public List<xConvNode> NPCLineList { get; set; }
    public List<xConvNode> PlayerLineList { get; set; }
    //TO DO
    //CinematicInfoList
    //PreviewTagMappingList

    void Awake()
    {
        if (StartList == null) StartList = new List<int>();
        if (NPCLineList == null) NPCLineList = new List<xConvNode>();
        if (PlayerLineList == null) PlayerLineList = new List<xConvNode>();
    }
}
