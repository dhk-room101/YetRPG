#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System;

public class xPerception : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameObject _parent = gameObject.transform.parent.gameObject;
        GameObject _Other = other.gameObject.transform.parent.gameObject;
        xGameObjectUTC _utc = _parent.GetComponent<xGameObjectUTC>();
        _utc.oPerception.Add(_Other);
    }

    void OnTriggerStay(Collider other)
    {
        //TO DO
    }

    void OnTriggerExit(Collider other)
    {
        GameObject _parent = gameObject.transform.parent.gameObject;
        GameObject _Other = other.gameObject.transform.parent.gameObject;
        xGameObjectUTC _utc = _parent.GetComponent<xGameObjectUTC>();
        if (_utc.oPerception.Contains(_Other))
            _utc.oPerception.Remove(_Other);//There may be commands to clean the perception list outside the trigger
    }

    // Use this for initialization
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
