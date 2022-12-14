using UnityEngine.Audio;
using System;
using System.Linq;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
// This is the Audio manager; this script is assigned to an empty game object witch has the task to be the audio manager
{

    [SerializeField] private AudioMixer _mixer; // The audio mixer
    [SerializeField] private GeneralSoundsConfiguration _sounds; // A list of sounds as a scriptableObject witch contains the other sounds as a scriptableObject each one
    [SerializeField] private AudioMixerGroup _audioMixerGroup; // There are 2 main groups of the audio mixer: BackgroundMusic and SoundEffects

    // Keys used to save user preferences with user pref
    public const string BACKGROUNDMUSIC_KEY = "backgroundMusicVolume";
    public const string SOUNDEFFECTS_KEY = "soundEffectsVolume";
    
    public static AudioManager instance; // There is only one audio manager

    private bool _preSceneMenu;

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
            s.source.outputAudioMixerGroup = _audioMixerGroup.audioMixer.FindMatchingGroups(outputMixer)[0]; // Assignment of the sound to an audio mixer group
        }

        //CheckMenu();
    }

    private void Update()
    {
        //CheckMenu();
    }

    #region Play
    public void Play(string name) // Method to play a music or a sound effect by its name
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        if (s.source == null)
        {
            return;
        }
        s.source.Play();
    }

    public void PlayIntro()
	{
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == "Intro");
        if (s == null) {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        if (s.source == null) {
            return;
        }
        s.source.Play();
    }

    public void PlayBackgroundMusic()
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == "BackgroungMusic");
        if (s == null) {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        if (s.source == null) {
            return;
        }
        s.source.Play();
    }

    public void PlayClickMenuSFX()
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == "ClickMenuSFX");
        if (s == null) {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        if (s.source == null) {
            return;
        }
        s.source.Play();
    }    
    
    public void PlayNewPlatfromSFX()
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == "NewPlatformSFX");
        if (s == null) {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        if (s.source == null) {
            return;
        }
        s.source.Play();
    }

    public void PlayPlacePlatfromSFX()
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == "PlacementSFX");
        if (s == null) {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        if (s.source == null) {
            return;
        }
        s.source.Play();
    }

    public void PlayActionSFX()
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == "ChangeActionSFX");
        if (s == null) {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        if (s.source == null) {
            return;
        }
        s.source.Play();
    }
    #endregion

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

    public void PauseAllBackgroundMusics() // Method to pause all backgrounds musics
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (var el in audioSources)
        {
            if (el.outputAudioMixerGroup.name == "BackgroundMusic")
            {
                el.Pause();
            }
        }
    }
    
    public void UnPauseAllBackgroundMusics() // Method to unpause all backgrounds musics
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (var el in audioSources)
        {
            if (el.outputAudioMixerGroup.name == "BackgroundMusic")
            {
                el.UnPause();
            }
        }
    }

    public void StopAllBackgroundMusics()
	{
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (var el in audioSources) {
            if (el.outputAudioMixerGroup.name == "BackgroundMusic") {
                el.Stop();
            }
        }
    }

    public void StopAllAudioSources()
	{
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (var el in audioSources) {
            el.Stop();
        }
    }
    
    public float GetVolumeScriptableObject(string name) // Method to obtain witch is the volume of a specific sound in its scriptable object
    {
        Sound s = Array.Find(_sounds.sounds.Select(el => el.sound).ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return 0.0f;
        }
        return s.volume;
    }
    
    public void SetVolumeScriptableObject(string name, float volume) // Method to set the volume of a specific sound in its scriptable object
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
    // Method to load volume setting from user pref and set them to the audio mixer
    {
        float backgroundMusicVolume = PlayerPrefs.GetFloat(BACKGROUNDMUSIC_KEY, 1f);
        float soundEffectsVolume = PlayerPrefs.GetFloat(SOUNDEFFECTS_KEY, 1f);
        _mixer.SetFloat(VolumeSetting.MIXER_BACKGROUNDMUSIC, Mathf.Log10(backgroundMusicVolume) * 20);
        _mixer.SetFloat(VolumeSetting.MIXER_SOUNDEFFECTS, Mathf.Log10(soundEffectsVolume) * 20);
    }
    
    private void CheckMenu()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if(sceneName.Equals("Credits") 
            || sceneName.Equals("LevelSelectionMenu")
            || sceneName.Equals("LogoScene")
            || sceneName.Equals("MainMenu")
            || sceneName.Equals("SendFeedbackMenu")
            || sceneName.Equals("SettingMenu")
            || sceneName.Equals("YouLoseWon"))
        {
            if(!_preSceneMenu)
                PlayIntro();

            _preSceneMenu = true;
        } else
        {
            _preSceneMenu = false;
        }
    }

    public AudioMixer GetAudioMixer()
	{
        return _mixer;
	}
}
