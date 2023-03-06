using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

/// <summary>
/// This script create Circular Color Button UI what can change a object color.
/// </summary>
public class WorldSpaceCircleUI : MonoBehaviour
{
    [SerializeField] GameObject objectHolder; //base mesh referance
    [SerializeField] Color[] colors; // colors for buttons
    [Range(0, 10)]
    [SerializeField] private int numberOfObect = 50; // number of buttons you want to create
    [Range(1, 5)]
    [SerializeField] private float Radius = 30; // radius of the circle
    private int raysToShoot = 0;
    private int radius = 0;
    [SerializeField] Button buttonPrefab; // Button Prefab for spown
    [SerializeField] Canvas canvas; // Canvas referance
    [SerializeField] List<GameObject> list; // contain Button list after Instantiate 
   

    /// <summary>
    /// Createing Buttons
    /// </summary>
    [Button("Create")]
    private void CreateUI()
    {
        RemoveList();
        List<Vector3> vec = Get360Points.Angle360(canvas.transform.position, Radius, numberOfObect);
        for (int i=0; i< vec.Count; i++) CreateObject(vec[i], i);
    }
    
    /// <summary>
    /// Removing Buttons
    /// </summary>
    [Button]
    private void RemoveList()
    {
        foreach (GameObject ob in list) Destroy(ob);
        list.Clear();
    }

    /// <summary>
    /// Create each button
    /// </summary>
    /// <param name="vec">point from 360Points</param>
    /// <param name="index">index of the button</param>
    private void CreateObject(Vector3 vec, int index)
    {

        //  GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject cube = Instantiate(buttonPrefab.gameObject, canvas.transform);
        list.Add(cube);
        cube.transform.position = vec;
        cube.GetComponent<ColorButtonHolder>().Asign(objectHolder, colors[index]);
       // cube.transform.LookAt(transform.position);
       // cube.transform.SetParent(transform);
        //cube.transform.SetParent(canvas.transform);
    }
}
