using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class xPlot
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

    public xPlot(string xname)
    {
        this.xname = xname;
        if (PlotAssistInfoList == null) PlotAssistInfoList = new Dictionary<string, string>();
    }
}