using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Class for waypoint placeable UTW
public class xGameObjectUTW : xGameObjectBase {

    public Vector3 position { get; set; }
    public Vector3 orientation { get; set; }
    public string Tag { get; set; }
    public List<Vector3> GeometryList { get; set; }//vertex?
    public int ObjectType { get; set; }
    public Guid GUID { get; set; }
    public List<Guid> AssociatedInstanceGUIDList { get; set; }
    public int Group { get; set; }
    public int Color { get; set; }//Colour
    public bool MapNoteEnabled { get; set; }
    public int MapNoteType { get; set; }
    public string Comments { get; set; }
    public string WaypointName { get; set; }
    public string MapNote { get; set; }
    public int MapNoteStringID { get; set; }
    public bool MapNoteRequiresReTranslation { get; set; }

    // Use this for initialization
    void Awake() {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
