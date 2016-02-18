using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class xMenuButton : MonoBehaviour {

    public void OnMouseEnter()
    {
        GameObject oButtonText = gameObject.transform.Find("ButtonMenuText").gameObject;
        oButtonText.GetComponent<Text>().color = new Color(0, 0, 0, 1);
        GameObject oButtonImage = gameObject.transform.Find("ButtonMenuImage").gameObject;
        oButtonImage.SetActive(true);
    }

    public void OnMouseExit()
    {
        GameObject oButtonText = gameObject.transform.Find("ButtonMenuText").gameObject;
        oButtonText.GetComponent<Text>().color = new Color(220, 220, 220, 1);
        GameObject oButtonImage = gameObject.transform.Find("ButtonMenuImage").gameObject;
        oButtonImage.SetActive(false);
    }

    public void OnMouseClick()
    {
        if (gameObject.name == "New Game")
        {
            xGameObjectMOD.instance.StartGame();
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
