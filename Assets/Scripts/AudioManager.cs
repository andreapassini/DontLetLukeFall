using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public SoundsConfiguration sounds; // A list of sounds as a scriptableObject

    public static AudioManager instance;

    public void Awake()
    {

        // This check of the instance is to avoid music restart playing when changing scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        // This foreach adds AudioSource components: one for each sound
        foreach (Sound s in sounds.sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name) // Method to play a music or a sound effect by its name
    {
        Sound s = Array.Find(sounds.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    
    public void Stop(string name) // Method to stop a (looped) music by its name
    {
        Sound s = Array.Find(sounds.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public float getVolume(string name) // Method to obtain witch is the volume of a specific sound
    {
        Sound s = Array.Find(sounds.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return 0.0f;
        }
        return s.volume;
    }
    
    public void setVolume(string name, float volume) // Method to set the volume of a specific sound
    {
        Sound s = Array.Find(sounds.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        s.source.Pause();
        s.volume = volume; //Persist last saved volume (scriptableObject)
        s.source.volume = volume;
        s.source.UnPause();
    }
    
}
