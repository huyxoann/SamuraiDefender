using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource sfxSource;
    public AudioSource musicSource;
    
    
    public AudioClip mainTheme;
    public AudioClip battleTheme;
    public AudioClip swordSwing;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip audioClip){
        sfxSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip audioClip){
        musicSource.Stop();
        musicSource.clip = audioClip;
        musicSource.Play();
    }

    public void PlaySwordSwing()
    {
        sfxSource.PlayOneShot(swordSwing);
    }
}
