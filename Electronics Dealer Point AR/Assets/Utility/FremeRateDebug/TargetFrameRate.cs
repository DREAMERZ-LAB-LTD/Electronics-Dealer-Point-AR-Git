using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script for PC because target frame rate dont work on mobile devices
/// </summary>
public class TargetFrameRate : MonoBehaviour
{
    [SerializeField] int targetFrame = 30;
    private void Start()
    {
        Application.targetFrameRate = targetFrame;
    }
}
