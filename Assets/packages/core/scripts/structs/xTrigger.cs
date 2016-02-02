using UnityEngine;
using System.Collections;

public class xTrigger : MonoBehaviour {
    Engine engine { get; set; }

     void OnTriggerEnter(Collider other)
     {
          //check if already exists by design/accident
          if (!gameObject.GetComponent<xGameObjectUTT>().oThreats
               .Contains(gameObject.GetComponent<xGameObjectUTT>().oThreats
                    .Find(threat => threat.oTarget = other.gameObject)))
          {
               //TO DO threat defined by distance between vector positions
               gameObject.GetComponent<xGameObjectUTT>().oThreats.
                    Add(new xThreat(other.gameObject,
                         engine.GetDistanceBetween(gameObject, other.gameObject)));
          }
     }

     void OnTriggerStay(Collider other)
     {
          //TO DO
     }

     void OnTriggerExit(Collider other)
     {
          gameObject.GetComponent<xGameObjectUTT>().oThreats
               .Remove(gameObject.GetComponent<xGameObjectUTT>().oThreats
                    .Find(threat => threat.oTarget = other.gameObject));
     }

     // Use this for initialization
     void Awake() {
        engine = gameObject.GetComponent<Engine>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
