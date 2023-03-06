using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This script calculate the pointer/Touch is over any UI or Canves Elemets
/// </summary>
public static class IsOverUI
{
    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;

    public static bool isIt => IsItOver();  // Public variable what get the result from IsItOver

    /// <summary>
    /// This function will calculate and return is the pointer/touch over UI
    /// </summary>
    /// <returns>true menas IsOver / false means Is Not Over</returns>
    private static bool IsItOver()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.GetTouch(0).position };
        _results = new List<RaycastResult>();
        if(EventSystem.current != null)EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }


}
