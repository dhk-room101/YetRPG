#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System;

public class xTrigger : MonoBehaviour
{
    Engine engine { get; set; }

    void OnTriggerEnter(Collider other)
    {
        GameObject _parent = gameObject.transform.parent.gameObject;
        if (engine == null) engine = _parent.GetComponent<Engine>();
        GameObject _Other = other.gameObject.transform.parent.gameObject;
        xGameObjectUTT _Trigger = _parent.GetComponent<xGameObjectUTT>();

        xEvent ev = engine.Event(EngineConstants.EVENT_TYPE_ENTER);
        engine.SetEventCreatorRef(ref ev, _Other);
        engine.SignalEvent(_parent, ev);
    }

    void OnTriggerStay(Collider other)
    {
        //TO DO
    }

    void OnTriggerExit(Collider other)
    {

    }

    // Use this for initialization
    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
