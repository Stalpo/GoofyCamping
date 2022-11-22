using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Animator ma;
    public Animator oa;
    public AudioMixer mixer1;
    public AudioMixer mixer2;
    public AudioMixer mixer3;
    public Slider s;

    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            mixer1.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
            mixer2.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
            mixer3.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
            s.SetValueWithoutNotify(PlayerPrefs.GetFloat("v"));
        }
        else
        {
            SetVolume(1);
            s.SetValueWithoutNotify(1);
        }
    }

    public void BackButton()
    {
        oa.SetTrigger("Leave");
        oa.ResetTrigger("Here");
        ma.SetTrigger("Here");
        ma.ResetTrigger("Leave");
    }

    public void SetVolume(float v)
    {
        float volume = Mathf.Log10(v) * 20;
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("v", v);
        mixer1.SetFloat("volume", volume);
        mixer2.SetFloat("volume", volume);
        mixer3.SetFloat("volume", volume);
    }
}
