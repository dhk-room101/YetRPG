using UnityEngine;
using System.Collections;

public class xThreat
{
    public GameObject oTarget { get; set; }
    public float fThreat { get; set; }

    public xThreat(GameObject oTarget, float fThreat = 0.0f)
    {
        this.oTarget = oTarget;
        this.fThreat = fThreat;
    }
}