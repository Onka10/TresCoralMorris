using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TresCoralMorris{
    public class EffectManager : Singleton<EffectManager>
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


    
    public void  PlayEffect()
    {
        int a = (int)EmissionFlags;
        float b = (float)a;

        mt1.SetFloat("_EmissonPattern", b);
        mt2.SetFloat("_EmissonPattern", b);
    }
}
}

