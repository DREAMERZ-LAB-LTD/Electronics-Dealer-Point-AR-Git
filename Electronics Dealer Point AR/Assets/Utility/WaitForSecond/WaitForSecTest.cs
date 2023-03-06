using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecTest : MonoBehaviour
{
    private void Start()
    {
        Show.Log("Start");
        StartCoroutine(TestSec());
    }

    IEnumerator TestSec()
    {

        // for test enable Log in Static class
        for (int i = 0; i < 10; i++)
        {
            Show.LogRed("Wait start");

            yield return WaitFor.Sec(1f);

        }
    }
}
