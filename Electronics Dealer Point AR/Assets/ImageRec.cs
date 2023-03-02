using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System;

public class ImageRec : MonoBehaviour
{
    [SerializeField] Text text;
    private ARTrackedImageManager aRTrackedImageManager;

    private void Awake()
    {
        Debug.Log("awake");
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }
    private void OnEnable()
    {

        Debug.Log("enable");
        aRTrackedImageManager.trackedImagesChanged += OnImageChange;
    }
    private void OnDisable()
    {

        Debug.Log("disable");
        aRTrackedImageManager.trackedImagesChanged -= OnImageChange;
    }



    public void OnImageChange(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log("Image Change");
        Debug.Log("-----------");
        Debug.Log(eventArgs);
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event
            Debug.Log("New Image");
            Debug.Log(newImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
            Debug.Log("update Image");
            Debug.Log(updatedImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
            Debug.Log("remove Image");
            Debug.Log(removedImage);
            
            
        }
       
    }

}
