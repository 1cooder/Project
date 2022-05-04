using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

 AudioSource audioSource;
 AudioSource  MusicTrack;
 

    void OnTriggerEnter2D(Collider2D trig)
    {
        MusicTrack  = GameObject.FindWithTag("MusicTrack").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        if (trig.gameObject.tag == "Player" && !audioSource.isPlaying)
        {
            
            MusicTrack.mute = true;
            audioSource.Play();
        
        }
    }
    
    void OnTriggerExit2D(Collider2D trig)
    {
        audioSource.Stop();
        MusicTrack.mute = false;
    }


}
