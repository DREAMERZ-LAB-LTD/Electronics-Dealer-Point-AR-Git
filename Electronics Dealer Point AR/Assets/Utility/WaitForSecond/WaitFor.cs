using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This system needed because New WaitForSeceonds create garbadge every time we call them.
/// So We are listing it and if we found then using it.
/// It will use when Memory boost needed more then CPU performance
/// </summary>
public class WaitFor : MonoBehaviour
{
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>(); 
    public static WaitForSeconds Sec(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait))
        {
            //Show.LogGray(time +" already have In WaitforSec");
            return wait;
        }

       // Show.LogGray(time + " Create in WaitforSec");
        WaitDictionary[time] = new WaitForSeconds(time); 
        return WaitDictionary[time];
    }
}
