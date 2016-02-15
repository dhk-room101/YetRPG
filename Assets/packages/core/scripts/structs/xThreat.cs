using UnityEngine;
using System.Collections;

public class xThreat
{
    public GameObject oThreat { get; set; }
    public float fThreat { get; set; }

    public xThreat(GameObject oThreat, float fThreat = 0.0f)
    {
        this.oThreat = oThreat;
        this.fThreat = fThreat;
    }
}