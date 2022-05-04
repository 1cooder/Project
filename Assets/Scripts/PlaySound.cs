using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

 AudioSource audioSource;
 AudioSource  MusicTrack;
 
    private void Start()
    {

        MusicTrack  = GameObject.FindWithTag("MusicTrack").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        
    }




    void OnTriggerEnter2D(Collider2D trig)
    {


        if (trig.gameObject.tag == "Player" && !audioSource.isPlaying)
        {
            
            //MusicTrack.mute = true;

            MusicTrack.volume = 0;
            audioSource.Play();
        
        }
    }
    
    void OnTriggerExit2D(Collider2D trig)
    {
        audioSource.Stop();
        //MusicTrack.mute = false;
  
         MusicTrack.volume = 1;
    }


}
