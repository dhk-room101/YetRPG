using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class xConvNode
{
    public int StringID { get; set; }
    public int LanguageID { get; set; }
    public int ConditionScriptURI { get; set; }
    public int ConditionParameter { get; set; }
    public string ConditionParameterText { get; set; }
    public int ConditionPlotURI { get; set; }
    public int ConditionPlotFlag { get; set; }
    public bool ConditionResult { get; set; }
    public int ActionScriptURI { get; set; }
    public int ActionParameter { get; set; }
    public string ActionParameterText { get; set; }
    public int ActionPlotURI { get; set; }
    public int ActionPlotFlag { get; set; }
    public bool ActionResult { get; set; }
    public string text { get; set; }
    public bool TextRequiresReTranslation { get; set; }
    public bool TextRequiresReRecording { get; set; }
    public string Speaker { get; set; }
    public string PreviousSpeaker { get; set; }
    public string Listener { get; set; }
    public int icon { get; set; }
    public string Comment { get; set; }
    public int FastPath { get; set; }
    public string SlideShowTexture { get; set; }
    public string VoiceOverTag { get; set; }
    public string VoiceOverComment { get; set; }
    public string EditorComment { get; set; }
    public int LineVisibility { get; set; }
    public bool Ambient { get; set; }
    public bool SkipLine { get; set; }
    public int StageURI { get; set; }
    public string StageTag { get; set; }
    public bool StageAtCurrentLocation { get; set; }
    public string CameraTag { get; set; }
    public bool CameraLocked { get; set; }
    public string SecondaryCameratag { get; set; }
    public float SecondaryCameraDelay { get; set; }
    public int Emotion { get; set; }
    public int CustomCutsceneURI { get; set; }
    public string SpeakerAnimation { get; set; }
    public bool RevertAnimation { get; set; }
    public bool LockAnimations { get; set; }
    public bool PlaySoundEvents { get; set; }
    public uint RoboBradSeed { get; set; }
    public bool RoboBradSeedOverride { get; set; }
    public bool RoboBradLocked { get; set; }
    public int PreviewAreaURI { get; set; }
    public bool PreviewStageUseFirstMatch { get; set; }
    public Vector3 PreviewStagePosition { get; set; }
    public Vector3 PreviewStageOrientation { get; set; }
    public bool UseAnimationDuration { get; set; }
    public bool NoVOInGame { get; set; }
    public bool Narration { get; set; }
    public bool PreCacheVO { get; set; }
    public List<Transition> TransitionList { get; set; }
    //TO DO
    //CinematicsInfoList
    //AnimationListList - oh, the creativity :-)
    //CustomCutsceneParameterList
    //PreviewTagMappingList
}

public class Transition
{
    public bool IsLink { get; set; }
    public int LineIndex { get; set; }
}