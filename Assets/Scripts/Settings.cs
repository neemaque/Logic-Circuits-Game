using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private bool fullScreen = false;
    [SerializeField] private Toggle fullScreenToggle;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            fullScreenToggle.isOn = true;
            ToggleFullScreen();
        }
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
    }
}
