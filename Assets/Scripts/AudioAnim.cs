using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnim : MonoBehaviour
{
    public void PlayAudio()
    {
        GetComponent<AudioSource>()?.Play();
        //Debug.Log("Audio");
    }
}
