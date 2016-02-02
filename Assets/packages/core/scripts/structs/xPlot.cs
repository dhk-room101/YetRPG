using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class xPlot
{

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
    public List<xPlotElement> StatusList { get; set; }

    //DHK
    public int ResRefID { get; set; }
    #endregion

    // Use this for initialization
    public xPlot()
    {
        if (StatusList == null) StatusList = new List<xPlotElement>();
    }


}

public class xPlotElement
{
    public xPlotNode pNode { get; set; }
    public int pValue { get; set; }

    public xPlotElement(xPlotNode pNode, int pValue)
    {
        this.pNode = pNode;
        this.pValue = pValue;
    }
}

public class xPlotNode
{
    public int Flag { get; set; }
    public string xname { get; set; }
    public bool Final { get; set; }
    public bool Repeatable { get; set; }
    public string JournalText { get; set; }
    public int JournalTextStringID { get; set; }
    public bool JournalTextRequiresReTranslation { get; set; }
    public int RewardID { get; set; }
    public string Comment { get; set; }
    public int DefaultValue { get; set; }
    public string AreaLocationTag { get; set; }
    public int OfferID { get; set; }
    public Dictionary<string, string> PlotAssistInfoList { get; set; }

    public xPlotNode()
    {
        if (PlotAssistInfoList == null) PlotAssistInfoList = new Dictionary<string, string>();
    }
}
