using UnityEngine;
using System.Collections;

public class xLoading : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward, -100 * Time.deltaTime);
    }
}
