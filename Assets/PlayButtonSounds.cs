using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSounds : MonoBehaviour
{
    public AudioSource a1;
    public AudioSource a2;
    public AudioSource a3;
    public AudioSource a4;

    public AudioClip[] annehmSounds;
    public AudioClip[] ablehnSounds;
    public AudioClip[] klickSounds;
    public AudioClip[] klingeltonSounds;

    public void TakeCall()
    {
        a1.clip = annehmSounds[Random.Range(0, annehmSounds.Length)];
        a1.Play();
    }

    public void RejectCall()
    {
        a2.clip = ablehnSounds[Random.Range(0, ablehnSounds.Length)];
        a2.Play();
    }

    public void ActiveCall()
    {
        a3.clip = klingeltonSounds[Random.Range(0, klingeltonSounds.Length)];
        a3.Play();
    }

    public void DisableKlingelton()
    {
        a3.Stop();
    }

    void Update()
    {
        // If the left mouse button is pressed down...
        if (Input.GetMouseButtonDown(0) == true)
        {
            a4.clip = klickSounds[Random.Range(0, klickSounds.Length)];
            a4.Play();
        }
    }
}
