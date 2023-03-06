using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DitectionChange : MonoBehaviour
{
    [SerializeField] AnchorCreator anchorCreator;
    [SerializeField] GameObject withStand;
    [SerializeField] GameObject withOutStand;
    public ARPlaneManager plainManager;
    public ARSession aRSession;
    public PlaneDetectionMode horizontal, vertical;
    
    

    public void ChageDetictionModeToHorizontal()
    {
        plainManager.requestedDetectionMode = horizontal;        
        aRSession.Reset();
        anchorCreator.enabled = true;

        anchorCreator.AnchorPrefab = withStand;
    }

    public void ChageDetictionModeToVirtical()
    {
        plainManager.requestedDetectionMode = vertical;
        aRSession.Reset();
        anchorCreator.enabled = true;

        anchorCreator.AnchorPrefab = withOutStand;
    }
}
