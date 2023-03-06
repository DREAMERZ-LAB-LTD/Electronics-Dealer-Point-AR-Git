using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class DestroyChildreanTest : MonoBehaviour
{
    [SerializeField] Transform parent;

    [Button]
    public void DestoryChild()
    {
        DestroyChildrean.DestroyAllChildren(parent);
    }
}
