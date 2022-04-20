using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class colorset : MonoBehaviour
{
    [SerializeField] GameData GData;
    Renderer rendererComponent;
    void Start(){
        int[] counter = new int[3];

        int index = 0;
        GameObject[] cellArray = new GameObject[24];
        while(index != 24) {
            int colorNum = Random.Range(0,3);
            while(counter[colorNum]== 8){
                colorNum=(colorNum+1)%3;
            }

            counter[colorNum]++;

            Color color = Color.black;
            switch(colorNum) {
                case 0:
                    color = Color.red;
                    break;
                case 1:
                    color = Color.green;
                    break;
                case 2:
                    color = Color.blue;
                    break;
            }


            // Debug.Log(colorNum);
            // transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.color = color;

            //DOtween版こっちのほうがおしゃれ
            //みための変更
            transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.DOColor(color,2f);

            //データの変更
            GData.masscolorarray[index] = color;

            index++;

        }
    }
}
