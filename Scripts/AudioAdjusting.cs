using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioAdjusting : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;


    public Slider musicSlider;
    public Slider soundSlider;
    public Slider otherMusicSlider;
    public Slider otherSoundSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        otherMusicSlider.value = musicSlider.value;
    }
    public void SetSoundVolume(float sliderValue)
    {
        soundMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        otherSoundSlider.value = soundSlider.value;
    }
}
