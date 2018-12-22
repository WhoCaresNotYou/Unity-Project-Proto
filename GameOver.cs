using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameObject[] gos;

    void Update()
    {
        if (Application.loadedLevel == 1)
        {
            gos = GameObject.FindGameObjectsWithTag("Player");
            if (gos.Length == 0)
            {
                Application.LoadLevel(2);
            }
        }

        if(Application.loadedLevel == 2)
        {
            StartCoroutine(ExecuteAfterTime(2));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(0);
    }
}
