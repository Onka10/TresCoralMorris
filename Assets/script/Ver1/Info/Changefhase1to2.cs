using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changefhase1to2 : MonoBehaviour
{
    [SerializeField]GameObject phase1Prefab;

    [SerializeField]GameObject phase1to2Prefab;


    //------

    // [SerializeField] colorchangemove CCM1;
    // [SerializeField] colorchangemove CCM2;

    [SerializeField]GameData GData;
    [SerializeField] PlayerLogic2 Next;

    [SerializeField] GameObject InputB;//Bの入力欄
    [SerializeField] GameObject InputW;//Wの入力欄

    public bool f1to2 = false;

    
    // startのフラグ
    bool start=true;

    //ターンカウント
    public int turncount=1;


    void Update(){
        if(start)   Changef1to2Info();
        if(turncount>=3||Input.GetKeyDown(KeyCode.A)){
            Next.GetComponent<PlayerLogic2>().enabled = true;
            this.GetComponent<InfomationLogic2>().enabled = true;
            this.GetComponent<Changefhase1to2>().enabled = false;
        }
    }

    public void Changef1to2Info(){
        //UIの変更
        //phase1を消して、phase2を出す。
        phase1Prefab.SetActive(false);
        phase1to2Prefab.SetActive(true);
        
        //疑似スタート関数をOFF
        start=false;
        
    }


    //カラー選択用

    public void FirstColorSelectR(){
        if(turncount==1){
            GData.MycolorB = Color.red;//データの変更
            InputB.SetActive(false);
            InputW.SetActive(true);
            // CCM1.ccm(Color.red);//見た目の変更
        }else if(turncount==2){
            GData.MycolorW = Color.red;//データの変更
            // CCM2.ccm(Color.red);//見た目の変更
        }
        turncount++;
    }

    public void FirstColorSelectG(){
        if(turncount==1){
            GData.MycolorB = Color.green;//データの変更
            InputB.SetActive(false);
            InputW.SetActive(true);
            // CCM1.ccm(Color.green);//見た目の変更
        }else if(turncount==2){
            GData.MycolorW = Color.green;//データの変更
            // CCM2.ccm(Color.green);//見た目の変更
        }
        turncount++;
    }

    public void FirstColorSelectB(){
        if(turncount==1){
            GData.MycolorB = Color.blue;//データの変更
            InputB.SetActive(false);
            InputW.SetActive(true);
            // CCM1.ccm(Color.blue);//見た目の変更
        }else if(turncount==2){
            GData.MycolorW = Color.blue;//データの変更
            // CCM2.ccm(Color.blue);//見た目の変更
        }
        turncount++;
    }

}
