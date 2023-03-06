using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This static function help to destory all child it have.
/// </summary>
public static class DestroyChildrean
{
   /// <summary>
   /// Destory all child
   /// </summary>
   /// <param name="t">Parent or rood transfrom</param>
    public static void DestroyAllChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }
}
