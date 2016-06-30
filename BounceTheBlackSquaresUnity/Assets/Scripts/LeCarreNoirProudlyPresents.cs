using UnityEngine;
using System.Collections;

public class LeCarreNoirProudlyPresents : MonoBehaviour {

    public string[] str; //Your list of strings to show
    public int minSpacer = 50; //The minimum gap between labels
    public int wrapSpeed = 10; //How quickly the text should move in pixels per second
    public Font f;
    int spacer = 0;
    int height = 0;
    int totalWidth = 0;
    int wrapPoint = 0;
    bool shouldUpdate = true;

    void Start()
    {
        //If you change any values used by UpdateLabelSize() you should set shouldUpdate to true to recalculate
        shouldUpdate = true;
    }

    void Update()
    {
        //Increase wrapPoint over time
        wrapPoint = (int)(Time.time * wrapSpeed);
    }

    void OnGUI()
    {
        //Recalculate our values if shouldUpdate is true
        if (shouldUpdate)
        {
            GUI.skin.label.alignment = TextAnchor.LowerCenter;
            UpdateLabelSize(str);
            shouldUpdate = false;
        }

        //Begin a group at the bottom of our screen
        GUI.BeginGroup(new Rect(0, Screen.height - height, Screen.width, height));

        //Run a for loop for our strings to create a label for each
        for (int i = 0; i < str.Length; i++)
        {
            //Find the wrapped position of a label due to wrapPoint
            int xPos = (spacer * (i + 1) - wrapPoint) % (totalWidth + spacer);
            //Make sure we get the positive modulo rather than the negative side
            int xPosWrap = xPos < 0 ? totalWidth + xPos : xPos - spacer;
            //Create a label at our wrapped position
            GUI.Label(new Rect(xPosWrap, 0, spacer, height), str[i]);
            GUI.skin.font = f;

        }

        //End group
        GUI.EndGroup();
    }

    void UpdateLabelSize(string[] strings)
    {
        //Reset values
        totalWidth = 0;
        spacer = 0;

        //Find the widest string in our list
        for (int i = 0; i < strings.Length; i++)
        {
            //Set our temporary width to the width of the current string
            int width = (int)GUI.skin.GetStyle("Label").CalcSize(new GUIContent(strings[i])).x;
            height = (int)GUI.skin.GetStyle("Label").CalcSize(new GUIContent(strings[i])).y;

            if (width > spacer)
            {
                //At the end of the for loop, spacer will be the largest value of width we received
                spacer = width;
            }
        }

        //Add our space value to create a minimum gap between neighbouring labels
        spacer += minSpacer;
        //Increase the spacing if we haven't covered our entire screen width
        spacer += (int)Mathf.Max(0, (float)(Screen.width - (spacer * (strings.Length - 1))) / (float)strings.Length);
        //Make sure the totalWidth is not 0 as this breaks the modulo function in OnGUI()
        totalWidth = Mathf.Max(spacer * (strings.Length - 1), 1);
    }
}
