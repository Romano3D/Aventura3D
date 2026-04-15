using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam = "MyExposedParam";

    public void ChangeValue(float f)
    {
        group.SetFloat(floatParam, f);    
    }

    public void ToggleAudio(bool isOn)
    {
        // Se isOn for true (caixa marcada), volume 0 (normal). 
        // Se false (caixa desmarcada), volume -80 (mudo).
        float volume = isOn ? 0f : -80f;
        group.SetFloat(floatParam, volume);
    }
}
