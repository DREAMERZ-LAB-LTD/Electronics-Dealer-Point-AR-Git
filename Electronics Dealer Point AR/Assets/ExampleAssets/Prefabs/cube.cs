using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class cube : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] LineRenderer lr;
    private void OnEnable()
    {
        text = GameObject.Find("a").GetComponent<Text>();
        //  lr = gameObject.AddComponent<LineRenderer>();
        //  lr = gameObject.GetComponent<LineRenderer>();
     //   Invoke(nameof(DisableImageManeger), 3f);
        
    }
    void DisableImageManeger()
    {
        AnchorCreator anchor = GameObject.Find("AR Session Origin").GetComponent<AnchorCreator>();
        anchor.m_AnchorPoints.Add(gameObject.GetComponent<ARAnchor>());
        ARTrackedImageManager arImage = GameObject.Find("AR Session Origin").GetComponent<ARTrackedImageManager>();
        arImage.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.name);
        text.text = gameObject.transform.position.ToString();
     //  lr.SetPosition(0, gameObject.transform.position);
     //  lr.SetPosition(1, GameObject.Find("AR Session Origin").transform.position);
    }
}
