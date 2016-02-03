using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class xConvTouch : MonoBehaviour {

    public int index { get; set; }
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
}
