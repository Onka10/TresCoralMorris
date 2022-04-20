using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class stonemove2 : MonoBehaviour
{   
    Vector3 milled = new Vector3(1.0f, 0, 1.0f);
    public float DurationSeconds;
    public Ease EaseType;

    //      指定された座標に置くだけ(座標、コマのナンバー)
    public void SetStone(Vector3 mouseposition,int num){
        mouseposition.y=1.2f;
        transform.GetChild(num).gameObject.GetComponent<Transform>().position = mouseposition;
    }

    public void millMove(int num){
        transform.GetChild(num).gameObject.SetActive(false);
        transform.GetChild(num).gameObject.GetComponent<Transform>().position=milled;
    }

    public void stoneshine2(int n){//2P用
        //普通に色を変える
        transform.GetChild(n).gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 0f);
        // transform.GetChild(n).gameObject.GetComponent<MeshRenderer>().material.color=Color.gray;//グレーでよくね？
    }

    public void Returnstonecolor2(int index){
        transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.color=Color.white;
    }
}

