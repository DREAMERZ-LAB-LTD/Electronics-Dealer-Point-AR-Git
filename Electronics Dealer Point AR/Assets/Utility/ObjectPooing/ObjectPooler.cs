using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object pooling system
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    public delegate void PoolCreatedCallback();
    public event PoolCreatedCallback poolCreatedCallback;
    Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    public void CreatePool(GameObject prefab, int poolSize)
    {
        StartCoroutine(CreatePoolCoroutine(prefab, poolSize));
    }

    /// <summary>
    /// Create Pool able Object
    /// </summary>
    /// <param name="prefab">Preafab Referance</param>
    /// <param name="poolSize">number of resuable object </param>
    /// <returns></returns>
    private IEnumerator CreatePoolCoroutine(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());

            GameObject poolHolder = new GameObject(prefab.name + " pool");
            //poolHolder.transform.parent = transform;

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab) as GameObject;
                newObject.transform.SetParent(poolHolder.transform);
                newObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObject);
                yield return new WaitForEndOfFrame();
               // Show.LogMagenta("Spawn: "+transform.name);
            }
            if(poolCreatedCallback!=null)poolCreatedCallback.Invoke();
        }
    }

    /// <summary>
    /// getting referance
    /// </summary>
    /// <param name="prefab">the object need to pool</param>
    /// <returns>get object from pool</returns>
    public GameObject ReuseObject(GameObject prefab)
    {
        int poolKey = prefab.GetInstanceID();
        //Debug.Log(poolKey);
        if (poolDictionary.ContainsKey(poolKey))
        {
            GameObject objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);

            objectToReuse.SetActive(true);

            return objectToReuse;
        }
        else { return null; }
    }



}
