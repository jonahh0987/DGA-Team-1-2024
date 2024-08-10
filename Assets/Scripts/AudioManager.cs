using UnityEngine.Audio;
using Unity.VisualScripting;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        // Ensure that there is only one Audio manager that persists across scenes
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }


        DontDestroyOnLoad(this);    
        foreach(Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            
            s.source.clip = s.clip;
        }
    }

    public void Play (string name, float volume = 1f)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.volume = volume;
            s.source.Play();
        }
        
    }


}
