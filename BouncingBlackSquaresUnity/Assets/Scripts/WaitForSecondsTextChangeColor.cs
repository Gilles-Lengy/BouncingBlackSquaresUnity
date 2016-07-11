using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitForSecondsTextChangeColor : MonoBehaviour {

    public Text displayedText;

    public int waitDuration = 5;


    public byte displayedTextColorR0 = 255;
    public byte displayedTextColorG0 = 255;
    public byte displayedTextColorB0 = 255;
    public byte displayedTextColorA0 = 255;

    public byte displayedTextColorR1 = 0;
    public byte displayedTextColorG1 = 0;
    public byte displayedTextColorB1 = 0;
    public byte displayedTextColorA1 = 255;

    void Start()
    {
        displayedText.color = new Color32(displayedTextColorR0, displayedTextColorG0, displayedTextColorB0, displayedTextColorA0);
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(waitDuration);
        displayedText.color = new Color32(displayedTextColorR1, displayedTextColorG1, displayedTextColorB1, displayedTextColorA1);
        print(Time.time);
    }
}
