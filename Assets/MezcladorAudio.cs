using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MezcladorAudio : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;

    public void SetVolumen()
    {
        float volumen = slider.value;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volumen)*20);
    }
}
