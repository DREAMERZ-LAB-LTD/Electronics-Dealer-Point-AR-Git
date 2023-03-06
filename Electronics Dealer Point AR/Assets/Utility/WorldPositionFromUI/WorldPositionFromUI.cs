using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Read UI Object position and calculate the 3D world spacae position a object need to be behind the UI object
/// </summary>
public static class WorldPositionFromUI
{
    /// <summary>
    /// Calculating Canvas Space to world space object position
    /// </summary>
    /// <param name="element">UI object rect transform</param>
    /// <param name="camera"> active camera referance</param>
    /// <returns></returns>
    public static Vector2 GetWorldPositionofCanvasElement(RectTransform element, Camera camera)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, camera, out var result);
        return result;
    }
}
