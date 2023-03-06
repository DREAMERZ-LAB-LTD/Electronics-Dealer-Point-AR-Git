using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPositionFromUITest : MonoBehaviour
{

    [SerializeField] RectTransform rectTransform;

    void Update()
    {
        transform.position = WorldPositionFromUI.GetWorldPositionofCanvasElement(rectTransform, Camera.main);
    }
}
