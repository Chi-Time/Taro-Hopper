using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;

    private void Update ()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI ()
    {
        if(Time.timeScale > 0.0f)
        {
            DisplayFPSCounter ();
        }
    }

    private void DisplayFPSCounter ()
    {
        int width = Screen.width, height = Screen.height;

        var style = new GUIStyle ();

        var rect = new Rect (0, 0, width, height * 2 / 100);

        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = height * 2 / 100;
        style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);

        var msec = deltaTime * 1000.0f;
        var fps = 1.0f / deltaTime;
        var text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);

        GUI.Label (rect, text, style);
    }
}
