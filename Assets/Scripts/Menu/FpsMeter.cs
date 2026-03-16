using UnityEngine;

public class FpsMeter : MonoBehaviour
{
    GUIStyle style;
    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 40;
        style.normal.textColor = Color.white;
    }

    void OnGUI()
    {
        float fps = 1f / Time.deltaTime;
        GUI.Label(new Rect(10, 10, 100, 20), "FPS: " + Mathf.Round(fps));
    }
}
