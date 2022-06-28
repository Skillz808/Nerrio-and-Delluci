using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play1LoopOther : MonoBehaviour
{

    public GameObject musicOne;
    public GameObject musicTwo;

    void Start()
    {
        print("started");
        StartCoroutine(waitForTime());
    }

    void Update()
    {

    }

    public IEnumerator waitForTime()
    {
        yield return new WaitForSeconds(6.1f);
        musicTwo.SetActive(true);
        print("done");
    }
}

