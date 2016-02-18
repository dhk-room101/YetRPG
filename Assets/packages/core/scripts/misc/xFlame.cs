using UnityEngine;
using System.Collections;

public class xFlame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        GameObject hf = GameObject.Find("hf");
        GameObject g = hf.transform.Find("GOD").gameObject;
        GameObject rs = g.transform.Find("RightSpawn").gameObject;
        GameObject ch2 = rs.transform.Find("CrustHook2").gameObject;
        Transform tch2 = ch2.transform;
        gameObject.transform.position = new Vector3(tch2.position.x, tch2.position.y + 0.1f, tch2.position.z);
        //gameObject.transform.parent = tch2;
        //gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x + 90, 0, 0, 1);
    }
}
