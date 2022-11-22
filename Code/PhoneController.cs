using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    public GameController gameController;
    private int lastCallWave = -1;
    public AudioSource audioSource;
    public AudioClip ringShort;
    public AudioClip ringLong;
    public AudioClip[] calls;
    public AudioClip barking;
    private AudioClip nextSound;

    public void Call()
    {
        if (gameController.part1)
        {
            if (lastCallWave < gameController.wave)
            {
                lastCallWave = gameController.wave;
                audioSource.clip = ringShort;
                audioSource.Play();
                nextSound = calls[lastCallWave];
                Invoke(nameof(PlayNext), ringShort.length);
            }
            else
            {
                audioSource.clip = ringLong;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.clip = ringShort;
            audioSource.Play();
            nextSound = barking;
            Invoke(nameof(PlayNext), ringShort.length);
        }
    }

    public void PlayNext()
    {
        audioSource.clip = nextSound;
        audioSource.Play();
    }
}
