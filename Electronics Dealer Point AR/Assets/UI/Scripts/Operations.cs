using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Operations : MonoBehaviour
{
    /// <summary>
    /// Load 3d scene
    /// </summary>
    /// <param name="index"></param>
    public void Open3DViewScene(int index)
    {
        // async load just for precotions
        SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
    }


}
