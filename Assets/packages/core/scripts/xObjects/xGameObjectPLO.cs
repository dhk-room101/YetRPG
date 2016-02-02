using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class xGameObjectPLO : xGameObjectBase {
    
    #region variables initialized from PLO
    public string ResRefName { get; set; }
    public bool LocalCopy { get; set; }
    public string xname { get; set; }
    public int NameStringID { get; set; }
    public bool NameRequiresReTranslation { get; set; }
    public Guid GUID { get; set; }
    public int ScriptURI { get; set; }
    public int Priority { get; set; }
    public string JournalImage { get; set; }
    public int ParentPlotURI { get; set; }
    public int EntryType { get; set; }
    public bool AllowPausing { get; set; }
    public List<xPlot> StatusList { get; set; }
    #endregion

    // Use this for initialization
    void Awake() {
        if (StatusList == null) StatusList = new List<xPlot>();
        xGameObjectMOD.instance.oPlots.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
