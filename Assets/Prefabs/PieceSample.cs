using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceSample : MonoBehaviour // クラス名はファイル名と同じにする
{
    public Material mt1;
    public Material mt2;
    
    public enum EmissonPattern
    {
        GENERALLY = 0,
        CHOOSEABLE = 1,
        SELECTING = 2,
    }
    public int test = 0;
    private EmissonPattern EmissionFlags = EmissonPattern.GENERALLY;

    void Start()
    {

        mt1 = this.GetComponent<Renderer>().material;
        mt2 = this.GetComponent<Renderer>().material;
        
    }


    
    void Update()
    {
        int a = (int)EmissionFlags;
        float b = (float)a;

        mt1.SetFloat("_EmissonPattern", b);
        mt2.SetFloat("_EmissonPattern", b);



        
    }

}