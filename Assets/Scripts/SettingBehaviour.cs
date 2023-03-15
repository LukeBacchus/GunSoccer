using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingBehaviour : MonoBehaviour
{
    public static AudioMixer audioMixer;
    static float currentVolume;
    // Start is called before the first frame update
    private void Start()
    {
        currentVolume = 0.5f;
    }

    public static void IncreaseSensitivity(int player)
    {
        Debug.Log("not implemented yet");
    }

    public static void DecreaseSensivity(int player)
    {
        Debug.Log("not implemented yet");
    }
    
    public static void IncreaseVolume()
    {
        float newVolume = currentVolume;
        if (currentVolume < 1)
        {
            // if can increase volume
            newVolume = currentVolume + 0.1f;
        }
        SetVolume(newVolume);
    }

    public static void DecreaseVolume()
    {
        float newVolume = currentVolume;
        if (currentVolume > 0)
        {
            // if can decrease volume
            newVolume = currentVolume + 0.1f;
        }
        SetVolume(newVolume);
    }

    public static void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        currentVolume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

   
    
}
