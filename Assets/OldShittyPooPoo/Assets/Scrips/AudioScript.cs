using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour

    
{
    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        Invoke("playAudio", 3.5f);
    }

    void playAudio()
        {
        myAudio.Play();
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
