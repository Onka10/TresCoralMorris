using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class stonemove1 : MonoBehaviour
{   
    Vector3 milled = new Vector3(1.0f, 0, 1.0f);

    public float DurationSeconds;
    public Ease EaseType;


    //コルーチン用のindex
    int index=0;

    float alpha_Sin;
    

    //      指定された座標に置くだけ(座標、コマのナンバー)
    public void SetStone(Vector3 mouseposition,int num){
        mouseposition.y=1.2f;
        transform.GetChild(num).gameObject.GetComponent<Transform>().position = mouseposition;
    }

    //ミル用の処理
    public void millMove(int num){
        transform.GetChild(num).gameObject.SetActive(false);
        transform.GetChild(num).gameObject.GetComponent<Transform>().position=milled;
    }

    //点滅処理
    public void stoneshine1(int n){//1P用
        //コルーチンは上手く行かなかった
        //DOTWEENはループの解除が上手く行かなかった

        //普通に色を変える
        transform.GetChild(n).gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 0f);
        // transform.GetChild(n).gameObject.GetComponent<MeshRenderer>().material.color=Color.gray;//グレーでよくね？
    }

    public void Returnstonecolor1(int n){
        transform.GetChild(n).gameObject.GetComponent<MeshRenderer>().material.color = new Color32(0, 0, 0, 1);
    }

    // IEnumerator ColorCoroutine()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForEndOfFrame();

    //         alpha_Sin = Mathf.Sin(Time.time) / 2 + 0.5f;

    //         Color _color = transform.GetChild(index).GetComponent<MeshRenderer>().material.color;

    //         _color.a = alpha_Sin;

    //         transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.color = _color;
    //     }
    // }
}
