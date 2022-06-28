using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFallSound : MonoBehaviour
{
    public AudioSource FallSound;
    private int shouldPlay = 0; 

    // Start is called before the first frame update
    public void PlayAudio()
    {
        shouldPlay = 1;
        if (shouldPlay == 1)
        {
            FallSound.Play();
        }
    }
}
