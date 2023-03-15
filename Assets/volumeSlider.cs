using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class volumeSlider : MonoBehaviour
{
    public Slider slider;
    AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        float currentVolume;
        slider.maxValue = 1;
        slider.minValue = 0.0001f;
        audioMixer.GetFloat("Volume", out currentVolume);
        slider.value = currentVolume;
    }

    // Update is called once per frame
    void Update()
    {
        float currentVolume;
        audioMixer.GetFloat("Volume", out currentVolume);
        slider.value = currentVolume;
    }
}
