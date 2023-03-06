using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Get Camera
/// This script will get and store the referance active camera
/// first try to get main camera
/// if it's not enable then find active camera and store it.
/// </summary>
public static class GetCamera
{
    private static Camera _camera;

    public static Camera Get
    {
        get
        {
            if (_camera == null)
            {
                Show.LogGray("New Camra");
                _camera = Camera.main;
                if (_camera == null)
                {
                    Camera[] _cam = GameObject.FindObjectsOfType<Camera>();
                    if (_cam.Length > 0)
                    {
                        foreach (Camera c in _cam)
                        {
                            if (c.gameObject.activeSelf == true)
                            {
                                _camera = c;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                Show.LogGray("Old Camra");
            }
           
            return _camera;
        }
    }

    private static Camera FindObjectOfType(Type type)
    {
        throw new NotImplementedException();
    }
}
