using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource _seAudioSource;

    [SerializeField] List<AudioClip> SEData;

    public void PlaySE(int se)
    {
        _seAudioSource.PlayOneShot(SEData[se]);
    }
}
