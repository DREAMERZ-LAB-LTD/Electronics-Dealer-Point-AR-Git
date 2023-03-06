using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine.SceneManagement;
using System;
using System.Drawing;
namespace theaaa
{ 
/// <summary>
/// ToDo
/// Undo system
/// </summary>
public class MoveToFolderWindow : EditorWindow
{
    string searchString = "";

    Vector2 scrollPos;

    void OnGUI()
    {


        GUIStyle customStyle = new GUIStyle();
        customStyle.fontSize = 12;
        customStyle.fontStyle = UnityEngine.FontStyle.Italic;
        customStyle.normal.textColor = UnityEngine.Color.gray;
        customStyle.alignment = TextAnchor.MiddleLeft;


        GUIStyle buttonStyle = new GUIStyle(EditorStyles.miniButton);
        buttonStyle.alignment = TextAnchor.MiddleLeft;
        buttonStyle.fixedHeight = 20;


        EditorGUILayout.BeginHorizontal();

        searchString = EditorGUILayout.TextField("Search Folder", searchString);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);



        int point = 50 + 20;
        if (searchString.Trim().Length == 0)
        {
            GUIStyle gUI = new GUIStyle();
            gUI.normal.textColor = UnityEngine.Color.yellow;
            GUILayout.Label("For all folders please use search bar.", gUI);
            gUI.normal.textColor = UnityEngine.Color.green;
            GUILayout.Label("All root folders/", gUI);
            GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject go in rootObjects)
            {
                if (go.tag == "Folder")
                {
                    string name = go.name;
                    string path = go.name;
                    Transform pT = go.transform.parent;
                    bool hasParent = go.transform.parent != null;

                    while (hasParent)
                    {
                        path = pT.name + "/" + path;

                        hasParent = pT.parent != null;

                        pT = pT.parent;
                    }
                    point += 20;
                    EditorGUILayout.BeginVertical();
                    GUIContent content = new GUIContent(name, EditorGUIUtility.IconContent("FolderOpened On Icon").image);
                    if (GUILayout.Button(content, buttonStyle))
                    {
                        ParentTransforms(Selection.gameObjects, go.transform);
                        Debug.Log("Moved : root/" + path);
                    }
                    GUILayout.Label("       path: root/" + path, customStyle);
                    EditorGUILayout.EndVertical();
                }
            }
        }
        else
        {
            GUIStyle gUI = new GUIStyle();
            gUI.normal.textColor = UnityEngine.Color.green;
            GUILayout.Label("Search result from all folders/", gUI);
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            Array.Reverse(allObjects);
            foreach (GameObject go in allObjects)
            {

                if (go.name.ToLower().IndexOf(searchString.ToLower()) == -1) { }
                else if (go.tag == "Folder")
                {
                    string name = go.name;
                    string path = go.name;
                    Transform pT = go.transform.parent;
                    bool hasParent = go.transform.parent != null;

                    while (hasParent)
                    {
                        path = pT.name + "/" + path;

                        hasParent = pT.parent != null;

                        pT = pT.parent;
                    }
                    point += 20;
                    EditorGUILayout.BeginVertical();
                    GUIContent content = new GUIContent(name, EditorGUIUtility.IconContent("FolderOpened On Icon").image);
                    if (GUILayout.Button(content, buttonStyle))
                    {
                        ParentTransforms(Selection.gameObjects, go.transform);
                        Debug.Log("Moved : root/" + path);
                    }
                    GUILayout.Label("       path: root/" + path, customStyle);
                    EditorGUILayout.EndVertical();
                }
            }

        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();


    }
    void ParentTransforms(GameObject[] selectedTransforms, Transform parent)
    {

        foreach (GameObject tr in selectedTransforms)
        {
            tr.transform.SetParent(parent);
        }
        SceneHierarchyUtility.SetExpanded(parent.gameObject, true);
        MoveToFolderWindow window = (MoveToFolderWindow)EditorWindow.GetWindow(typeof(MoveToFolderWindow));

        EditorPrefs.SetBool("MoveWindowOn", false);
        window.Close();

    }


}
}