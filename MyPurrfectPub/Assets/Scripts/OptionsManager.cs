using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public static Action OnMusicVolChanged;
    public static Action OnFXVolChanged;

    [SerializeField] AudioMixer mixer;
    [SerializeField] float maxSoundValue = 10, minSoundValue = -40;

    private static int maxLocaleId;
    private static int currentMusicVolume;
    private static int currentFXVolume;

    private void Awake()
    {
        currentFXVolume = PlayerPrefs.GetInt("FXVolume", 50);
        currentMusicVolume = PlayerPrefs.GetInt("MusicVolume", 50);
    }

    private void Start()
    {
        SetFXVolume();
        SetMusicVolume();
    }

    public void ChangeMusicVolume(bool more)
    {
        if (more)
        {
            currentMusicVolume = currentMusicVolume + 5 > 100 ? 100 : currentMusicVolume + 5;
        }
        else
        {
            currentMusicVolume = currentMusicVolume - 5 < 0 ? 0 : currentMusicVolume - 5;
        }

        SetMusicVolume();
    }

    private void SetMusicVolume()
    {
        float soundValue = currentMusicVolume * (maxSoundValue - minSoundValue) / 100 + minSoundValue;
        mixer.SetFloat("musicVolume", soundValue);
        PlayerPrefs.SetInt("MusicVolume", currentMusicVolume);
        OnMusicVolChanged?.Invoke();
    }

    public void ChangeFXVolume(bool more)
    {
        if (more)
        {
            currentFXVolume = currentFXVolume + 5 > 100 ? 100 : currentFXVolume + 5;
        }
        else
        {
            currentFXVolume = currentFXVolume - 5 < 0 ? 0 : currentFXVolume - 5;
        }

        SetFXVolume();
    }

    private void SetFXVolume()
    {
        float soundValue = currentFXVolume * (maxSoundValue - minSoundValue) / 100 + minSoundValue;
        mixer.SetFloat("fxVolume", soundValue);
        PlayerPrefs.SetInt("FXVolume", currentFXVolume);
        OnFXVolChanged?.Invoke();
    }
}
