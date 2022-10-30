using UnityEngine.Audio;
using System;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private GeneralSoundsConfiguration _sounds; // A list of sounds as a scriptableObject
    [SerializeField] private AudioMixerGroup _audioMixerGroup; // There are 2 main groups of the audio mixer: BackgroundMusic and SoundEffects

    public const string BACKGROUNDMUSIC_KEY = "backgroundMusicVolume";
    public const string SOUNDEFFECTS_KEY = "soundEffectsVolume";
    
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
        
        LoadVolume();
        
        // This foreach adds AudioSource components: one for each sound
        foreach (SoundConfiguration ss in _sounds.sounds)
        {
            Sound s = ss.sound;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            string outputMixer = "BackgroundMusic";
            if (s.loop == false)
            {
                outputMixer = "SoundEffects";
            }
            s.source.outputAudioMixerGroup = _audioMixerGroup.audioMixer.FindMatchingGroups(outputMixer)[0];
        }
    }

    public void Play(string name) // Method to play a music or a sound effect by its name
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    
    public void Stop(string name) // Method to stop a (looped) music by its name
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public float GetVolume(string name) // Method to obtain witch is the volume of a specific sound
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return 0.0f;
        }
        return s.volume;
    }
    
    public void SetVolume(string name, float volume) // Method to set the volume of a specific sound
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == name);
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

    private void LoadVolume() // Volume is saved in VolumeSetting.cs
    {
        float backgroundMusicVolume = PlayerPrefs.GetFloat(BACKGROUNDMUSIC_KEY, 1f);
        float soundEffectsVolume = PlayerPrefs.GetFloat(SOUNDEFFECTS_KEY, 1f);
        _mixer.SetFloat(VolumeSetting.MIXER_BACKGROUNDMUSIC, Mathf.Log10(backgroundMusicVolume) * 20);
        _mixer.SetFloat(VolumeSetting.MIXER_SOUNDEFFECTS, Mathf.Log10(soundEffectsVolume) * 20);
    }
    
}
