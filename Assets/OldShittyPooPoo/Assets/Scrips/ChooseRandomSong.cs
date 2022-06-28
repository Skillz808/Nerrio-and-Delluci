using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomSong : MonoBehaviour
{

    float randomNumber;
    public GameObject imlazy;
    public GameObject imlazy2;
    public GameObject imlazy3;

    // Start is called before the first frame update
    public void Start()
    {
        randomNumber = (Random.Range(1f, 3f));
        randomNumber = (Mathf.Round(randomNumber));

        if (randomNumber == 1)
        {
            imlazy.SetActive(true);
        }

        else if (randomNumber == 2)
        {
            imlazy2.SetActive(true);
        }

        else if (randomNumber == 3)
        {
            imlazy3.SetActive(true);
        }
    }
}
