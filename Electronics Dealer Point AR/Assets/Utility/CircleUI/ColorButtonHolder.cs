using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This object will create from World Space Circler UI and set the referance through Asing funtion
/// </summary>
public class ColorButtonHolder : MonoBehaviour
{
    [SerializeField] GameObject asignObject;
    [SerializeField] Color color;

    /// <summary>
    /// Asigning data from parent script.
    /// </summary>
    /// <param name="obj">Object what color will change</param>
    /// <param name="clr">Color of the button</param>
    public void Asign(GameObject obj, Color clr)
    {
        asignObject = obj;
        color = clr;
        gameObject.GetComponent<Image>().color = clr;
    }

    /// <summary>
    /// Asigning color in to the object mesh
    /// This function will called from Button
    /// </summary>
    public void SetUp()
    {
        asignObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
