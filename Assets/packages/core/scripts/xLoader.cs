using UnityEngine;
using System.Collections;

public class xLoader : MonoBehaviour {

    public xGameObjectMOD gameModule;

	void Awake () {
        if (xGameObjectMOD.instance == null) Instantiate(gameModule);
        
        //Add an empty invalid object as a placeholder for certain checks
        GameObject oObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/invalidPrefab"));
        oObject.name = "Invalid";
    }
}
