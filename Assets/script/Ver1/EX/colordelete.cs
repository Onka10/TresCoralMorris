using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class colordelete : MonoBehaviour
{
    [SerializeField] GameData GData;
    int index;

    Color Dcolor = new Color(0f, 0f, 0f, 0.7f);
    
    public void DeleteColor(){
        int errorcounter=0;
        while(true){//無限ループの可能性アリ
            index = Random.Range (0, 23);
            if(GData.masscolorarray[index]!=Color.gray) break;
            if(errorcounter==9999999){
                Debug.Log("エラー残っとるぞ");
                break;
            }
            errorcounter++;
        }

            //みための変更
            transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.DOColor(Dcolor,1f);

            //データの変更。データ上はグレーとなっている
            GData.masscolorarray[index] = Color.gray;
        
    }
}
