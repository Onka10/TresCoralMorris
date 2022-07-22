using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : Singleton<SEManager>
{
    [SerializeField] AudioSource _audioSource;

    //うちわけ1:put、2:Click、3:cancel
    public List<AudioClip> SE = new List<AudioClip>(5);

    public void Put(){
        _audioSource.PlayOneShot(SE[0]);
    } 

    public void Click(){
        _audioSource.PlayOneShot(SE[1]);
    } 

    public void Cancel(){
        _audioSource.PlayOneShot(SE[2]);
    } 
}
