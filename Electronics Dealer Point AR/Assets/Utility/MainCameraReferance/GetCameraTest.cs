using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GetCameraTest : MonoBehaviour
{
    [SerializeField] Camera cam;
    [Button]
    private void Get()
    {
        cam = GetCamera.Get;
    }
    
    
}
