using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class xGameObjectPTY : MonoBehaviour
{
    Engine engine { get; set; }
    
    //Active party is extrapolated based on FOLLOWER_STATE_ACTIVE
    public List<GameObject> oPartyPool { get; set; }

    public List<xPlot> oPlots { get; set; }

    // Use this for initialization
    void Start()
    {
        engine = gameObject.GetComponent<Engine>();
        if (oPartyPool == null) oPartyPool = new List<GameObject>();
        if (oPlots == null) oPlots = new List<xPlot>();
        //gameObject.AddComponent(Type.GetType(Script));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
