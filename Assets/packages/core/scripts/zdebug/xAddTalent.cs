using UnityEngine;
using System.Collections;

public class xAddTalent : MonoBehaviour {

    Engine engine { get; set; }
	// Use this for initialization
	void Awake() {
        engine = gameObject.GetComponent<Engine>();
        //TO DO complete script
        Debug.Log("arguments: " + engine.GetLocalString(xGameObjectMOD.instance.gameObject, "RUNSCRIPT_VAR"));

          //do stuff

          //When done, reset value and destroy itself
          engine.SetLocalString(gameObject, "RUNSCRIPT_VAR", "");
          DestroyObject(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
