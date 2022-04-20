using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class shine : MonoBehaviour
{
    public float DurationSeconds;
    public Ease EaseType;
    public void massshine1(int index){//1P用
        //例
        // transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.DOColor(color,2f);
        // this.canvasGroup.DOFade(0.0f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo);
        transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.DOFade(0.0f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo);
    }

    public void stonehine2(int index){//2P用
        //例
        // transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.DOColor(color,2f);
        // this.canvasGroup.DOFade(0.0f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo);
        transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.DOFade(0.0f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo);
    }
}
