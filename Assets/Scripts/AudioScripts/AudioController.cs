using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource;
    
    [SerializeField]
    private AudioSource _fishSoundSource;
    
    [SerializeField]
    private AudioSource _rodSoundSource;

    private readonly string _musicKey = "Music";

    private readonly string _soundKey = "Sound";

    public static AudioController Instance;

    public int Music;

    public int Sound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //Debug.Log("Instance create! " + Instance);
        
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        if (!PlayerPrefs.HasKey(_musicKey))
            PlayerPrefs.SetInt(_musicKey, 0);

        if (!PlayerPrefs.HasKey(_soundKey))
            PlayerPrefs.SetInt(_soundKey, 0);

        Music = PlayerPrefs.GetInt(_musicKey);
        Sound = PlayerPrefs.GetInt(_soundKey);

        if (_musicSource != null)
            _musicSource.volume = Music;

        if (_fishSoundSource != null)
            _fishSoundSource.volume = Sound;

        if (_rodSoundSource != null)
            _rodSoundSource.volume = Sound;
    }
    public void ChangeMusic()
    {
        Music = Music == 0 ? 1 : 0;
        PlayerPrefs.SetInt(_musicKey, Music);
        if (_musicSource != null)
            _musicSource.volume = Music;
    }

    public void ChangeSound()
    {
        Sound = Sound == 0 ? 1 : 0;
        PlayerPrefs.SetInt(_soundKey, Sound);
        
        if (_fishSoundSource != null)
            _fishSoundSource.volume = Sound;

        if (_rodSoundSource != null)
            _rodSoundSource.volume = Sound;
    }

    public void PlayFishSound()
    {
        if (Sound == 1 && _fishSoundSource != null)
            _fishSoundSource.Play();
    }

    public void PlayRodSound()
    {
        if (Sound == 1 && _rodSoundSource != null)
            _rodSoundSource.Play();
    }
}
