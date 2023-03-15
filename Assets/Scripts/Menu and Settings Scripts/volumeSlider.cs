using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class volumeSlider : MonoBehaviour
{
    public Slider slider;
    //public AudioMixer audioMixer;
    FMOD.Studio.Bus Music;
    float currentVolume;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 1;
        slider.minValue = 0.0001f;
        // will need to change this based on actual bus names in FMOD
        // Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");

        //audioMixer.GetFloat("Volume", out currentVolume);
        slider.value = currentVolume;
    }

    // Update is called once per frame
    void Update()
    {
        //audioMixer.GetFloat("Volume", out currentVolume);
        slider.value = currentVolume;
    }
}
