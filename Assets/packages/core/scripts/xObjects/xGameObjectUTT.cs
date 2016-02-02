using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class xGameObjectUTT : xGameObjectBase {

    //Is this needed here? Or on character UTC? Both?
    public List<xThreat> oThreats { get; set; }

    // Use this for initialization
    void Awake() {
        if (oThreats == null) oThreats = new List<xThreat>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
