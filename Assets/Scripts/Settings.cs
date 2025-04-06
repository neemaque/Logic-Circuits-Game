using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    private bool fullScreen = false;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volumeSlider;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            fullScreenToggle.isOn = true;
            fullScreen = true;
            Screen.fullScreen = true;
        }
        else
        {
            fullScreen = false;
            Screen.fullScreen = false;
        }
        float volume = PlayerPrefs.GetFloat("SoundFX");
        volumeSlider.value = Mathf.Pow(10, volume / 20f);
    }
    public void ToggleFullScreen()
    {
        if(fullScreen)
        {
            fullScreen = false;
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else
        {
            fullScreen = true;
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        PlayerPrefs.Save();
    }
    public void SetSoundFxVolume(float sliderValue)
    {
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        mixer.SetFloat("SoundFX", dB);
        PlayerPrefs.SetFloat("SoundFX", dB);
        PlayerPrefs.Save();
    }
}
