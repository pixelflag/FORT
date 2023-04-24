using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 0.05f; 

    private SoundResource resource;

    private static SoundManager _instance;
    public static SoundManager instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        Initialize();
    }

    private const int SE_CHANNEL = 5;

    private AudioSource bgmAudioSource;
    private AudioSource[] seAudioSource;

    public float bgmVolume = 0.5f;
    public float currentBgmVolume;
    public float seVolume = 1.0f;

    private bool isPause = false;
    private BgmType nextBgm;

    private FadeState fadeState;
    private enum FadeState
    {
        None,
        FadeInOut,
        FadeIn,
        FadeOut,
    }

    public void Initialize()
    {
        currentBgmVolume = bgmVolume;

        resource = GetComponent<SoundResource>();
        resource.Initialize();

        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;

        seAudioSource = new AudioSource[SE_CHANNEL];
        for(int i=0; i < SE_CHANNEL; i++)
            seAudioSource[i] = gameObject.AddComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if(!isPause)
        {
            switch(fadeState)
            {
                case FadeState.None:
                    break;
                case FadeState.FadeInOut:
                    bgmAudioSource.volume -= fadeSpeed;
                    if(bgmAudioSource.volume <= 0)
                    {
                        BGMFadeIn(nextBgm);
                    }
                    break;
                case FadeState.FadeIn:
                    bgmAudioSource.volume += fadeSpeed;
                    if (bgmVolume < bgmAudioSource.volume)
                    {
                        bgmAudioSource.volume = bgmVolume;
                        fadeState = FadeState.None;
                    }
                    break;
                case FadeState.FadeOut:
                    bgmAudioSource.volume -= fadeSpeed;
                    if (bgmAudioSource.volume <= 0)
                    {
                        bgmAudioSource.Stop();
                        fadeState = FadeState.None;
                    }
                    break;
            }
        }
    }

    public void PlayBgm(BgmType type)
    {
        bgmAudioSource.clip = resource.GetBgm(type);
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();

        fadeState = FadeState.None;
    }

    public void BGMFadeOutAndPlayIn(BgmType type)
    {
        nextBgm = type;
        fadeState = FadeState.FadeInOut;
    }

    public void BGMFadeOutAndStop(BgmType type)
    {
        nextBgm = type;
        fadeState = FadeState.FadeInOut;
    }

    public void BGMFadeIn(BgmType type)
    {
        bgmAudioSource.clip = resource.GetBgm(type);
        bgmAudioSource.volume = 0;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();

        fadeState = FadeState.FadeIn;
    }

    public void StopBgm()
    {
        bgmAudioSource.Stop();
    }

    public void PauseBgm()
    {
        bgmAudioSource.Pause();
        isPause = true;
    }

    public void ResumeBgm()
    {
        bgmAudioSource.UnPause();
        isPause = false;
    }

    // Jingle -----

    public void PlayJingle(BgmType type, float volumeGain)
    {
        bgmAudioSource.clip = resource.GetBgm(type);
        bgmAudioSource.volume = bgmVolume * volumeGain;
        bgmAudioSource.loop = false;
        bgmAudioSource.Play();

        fadeState = FadeState.None;
    }

    // Effect -----

    public void PlayOneShot(SeType type, float volume)
    {
        seAudioSource[0].PlayOneShot(resource.GetEffect(type), volume * seVolume);
    }

    public void PlayOneShotOnChannel(int channel, SeType type, float volume)
    {
        seAudioSource[channel].Stop();
        seAudioSource[channel].PlayOneShot(resource.GetEffect(type), volume * seVolume);
    }
}
