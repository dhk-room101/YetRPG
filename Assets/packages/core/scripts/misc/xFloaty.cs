using UnityEngine;
using System.Collections;

public class xFloaty : MonoBehaviour
{
    public float displayTime { get; set; }
    IEnumerator fadeAlpha { get; set; }
    IEnumerator moveUpY { get; set; }
    GUIText _floaty { get; set; }

    void Start()
    {
        _floaty = gameObject.GetComponent<GUIText>();
        SetAlpha();
        MoveUp();
    }

    void MoveUp()
    {
        if (moveUpY != null)
        {
            StopCoroutine(moveUpY);
        }
        moveUpY = MoveUpY();
        StartCoroutine(moveUpY);
    }

    void SetAlpha()
    {
        if (fadeAlpha != null)
        {
            StopCoroutine(fadeAlpha);
        }
        fadeAlpha = FadeAlpha();
        StartCoroutine(fadeAlpha);
    }

    IEnumerator MoveUpY()
    {
        Vector3 p = gameObject.transform.position;
        _floaty.transform.position = p;

        while (_floaty.font.material.color.a > 0)
        {
            p = new Vector3(p.x, p.y + (Time.deltaTime / (displayTime / 0.25f)), p.z);
            _floaty.transform.position = p;
            yield return null;
        }
        yield return null;
    }

    IEnumerator FadeAlpha()
    {
        Color resetColor = _floaty.font.material.color;
        resetColor.a = 1;
        _floaty.font.material.color = resetColor;

        yield return new WaitForSeconds(displayTime * 2 / 3);//full Alpha for two thirds

        while (_floaty.font.material.color.a > 0)
        {
            Color displayColor = _floaty.font.material.color;
            displayColor.a -= Time.deltaTime / (displayTime / 3);//start fading in the last third
            _floaty.font.material.color = displayColor;
            yield return null;
        }
        Destroy(gameObject);//Destroy object when alpha is zero
        yield return null;//Should never reach this point
    }
}
