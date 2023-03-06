using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This scritp will display frame per second rate using On GUI function so no Text needed to asign.
/// This only show in Development build or Unity Ediotr
/// User cant see in Production build
/// </summary>
public class FPSDisplay : MonoBehaviour
{
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    Rect rect = new Rect(100, 100, 200, 200);
    GUIStyle gUIStyle = new GUIStyle();
    float deltaTime;
    float fps;
    private string fpsString = "FPS";
    private void Start()
    {
      
       
        gUIStyle.fontSize = 48;
        rect = new Rect(Screen.width - 200, Screen.height -100, 200, 200);
    }
    void Update()
    {
        
        //rect = new Rect(Screen.width - 300, Screen.height - 200, 200, 200);
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
        fpsString = Mathf.Ceil(fps).ToString()+" FPS";
        
    }
    void OnGUI()
    {
        GUI.Label(rect, fpsString, gUIStyle);
    }
#endif
    

}