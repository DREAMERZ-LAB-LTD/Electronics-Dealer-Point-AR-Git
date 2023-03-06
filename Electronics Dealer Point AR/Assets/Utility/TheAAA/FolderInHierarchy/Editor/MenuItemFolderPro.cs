using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace theaaa
{ 
public class MenuItemFolderPro
{

    [MenuItem("GameObject/Folder/Create Parent Folder")]
    private static void CreateEmptyFolderParent(UnityEditor.MenuCommand menuCommand)
    {

        if (menuCommand.context != Selection.objects[0])
        {
            return;
        }

        //Debug.Log("Create folder");
        Object[] selectedObjects = Selection.objects;
        //foreach(Object o in selectedObjects)Debug.Log(o as GameObject);
        GameObject selectedGameObject = selectedObjects[0] as GameObject;

        GameObject obj = new GameObject();
        Transform parent = selectedGameObject.transform.parent;

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            GameObject o = selectedObjects[i] as GameObject;
            o.transform.SetParent(obj.transform);
        }
        Selection.activeGameObject = obj;
        //selectedGameObject.transform.SetParent(obj.transform);
        if (parent)
        {
            obj.transform.SetParent(parent);

            SceneHierarchyUtility.SetExpanded(obj, true);
        }
        else
        {
            SceneHierarchyUtility.SetExpanded(parent.gameObject, true);

        }
        EditorUtility.SetDirty(Selection.activeGameObject);
        obj.name = "New Folder";

        MenuItemFolder.AddTag(obj, "Folder");

    } [MenuItem("GameObject/Folder/Create Parent Folder", true, -2)]
    private static bool ValidateCreateEmptyFolderParent()
    {
        //Debug.Log("Create Empty Parent "+ Selection.activeGameObject.name);
        // Return true to enable the menu item, false to disable it
        return Selection.objects.Length != 0 && Selection.activeGameObject.transform.parent != null && !MenuItemFolder.CheckSelectObjectParentInList(Selection.objects);
    }

    [MenuItem("GameObject/Folder/Create Root Folder")]
    private static void CreateEmptyRootFolderParent(UnityEditor.MenuCommand menuCommand)
    {
        if (menuCommand.context != Selection.objects[0])
        {
            return;
        }

        Object[] selectedObjects = Selection.objects;
        GameObject selectedGameObject = selectedObjects[0] as GameObject;

        GameObject obj = new GameObject();
        // Transform parent = selectedGameObject.transform.parent;
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            GameObject o = selectedObjects[i] as GameObject;
            o.transform.SetParent(obj.transform);
        }
        SceneHierarchyUtility.SetExpanded(obj, true);
        //if (parent) obj.transform.SetParent(parent);

        //EditorUtility.SetDirty(Selection.activeGameObject);
        obj.name = "New Folder";
        Selection.activeGameObject = obj;
        MenuItemFolder.AddTag(obj, "Folder");
    } [MenuItem("GameObject/Folder/Create Root Folder", true, -2)]
    private static bool ValidateCreateEmptyRootFolderParent()
    {
        // Return true to enable the menu item, false to disable it
        return Selection.activeGameObject != null && !MenuItemFolder.CheckSelectObjectParentInList(Selection.objects);
    }


    // Add menu named "My Window" to the Window menu
    [MenuItem("GameObject/Folder/Move To Folder")]
    static void Init()
    {
        EditorPrefs.SetBool("MoveWindowOn", true);
        // Get existing open window or if none, make a new one:
        MoveToFolderWindow window = (MoveToFolderWindow)EditorWindow.GetWindow(typeof(MoveToFolderWindow));
        window.title = "Move To Folder";
        window.Show();
    }
    [MenuItem("GameObject/Folder/Move To Folder", true, -2)]
    private static bool MoveToFolderCheck()
    {

        // Return true to enable the menu item, false to disable it
        return Selection.objects.Length != 0 && !MenuItemFolder.CheckSelectObjectParentInList(Selection.objects);
    }



}
}