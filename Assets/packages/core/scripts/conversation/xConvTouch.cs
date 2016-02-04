using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class xConvTouch : MonoBehaviour {
    public int lineIndex { get; set; }//Keeps track of position in the Conversation branch
    public int lineLocation { get; set; }
    public string iconID { get; set; }
    Transform _mood { get; set; }

    public void OnMouseEnter()
    {
        _mood = gameObject.transform.parent.Find("moodCircle");
        string path = "conversation/" + iconID;
        _mood.gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load(path, typeof(Sprite));
        _mood.gameObject.SetActive(true);
        gameObject.GetComponent<Text>().color = new Color(0.85f, 0.85f, 0.85f);
    }

    public void OnMouseExit()
    {
        if (_mood != null && _mood.gameObject != null) _mood.gameObject.SetActive(false);
        gameObject.GetComponent<Text>().color = new Color(0.588f, 0.588f, 0.588f);
    }

    public void OnMouseClick()
    {
        //Get the current line index and pass it to flow to the next conversation node
        int l = lineIndex;
        var cInstance = gameObject.transform.parent.GetComponent<xConvInstance>();
        cInstance.NextLine(l);
    }
}
