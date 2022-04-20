using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerLogic : MonoBehaviour
{
    //シーケンス番号1

    [SerializeField] stonemove1 CM1;
    [SerializeField] stonemove2 CM2;

    [SerializeField] GameData GData;

    [SerializeField] Changefhase1to2 Next;

    //なんかraycastで使う
    Vector3 touchScreenPosition;

    //マスの座標
    Vector3 massposition;

    //1Pのコマカウント
    public int Bcounter =0;

    //2Pの置いた石の数
    public int Wcounter =0;

    [SerializeField] Text BR;
    [SerializeField] Text BG;
    [SerializeField] Text BB;
    [SerializeField] Text BA;

    [SerializeField] Text WR;
    [SerializeField] Text WG;
    [SerializeField] Text WB;
    [SerializeField] Text WA;

    [SerializeField] Image upper;
    [SerializeField] Image downer;
    Color co;




    //public組
    //====================================
    //フェーズ1終了通知
    public bool f1=true;
    //====================================

    void Update(){
        fhase1();
    }

    //クリックから情報をおくる。
    public void fhase1 (){
        if (Input.GetMouseButtonDown(0)) {
            touchScreenPosition = Input.mousePosition;

            //枠からはみ出したのを無効にしてる。けど結局タブで探偵するから無駄
            // touchScreenPosition.x   = Mathf.Clamp( touchScreenPosition.x, 0.0f, Screen.width );
            // touchScreenPosition.y   = Mathf.Clamp( touchScreenPosition.y, 0.0f, Screen.height );

            Camera  gameCamera      = Camera.main;
            Ray     touchPointToRay = gameCamera.ScreenPointToRay( touchScreenPosition );
            RaycastHit hitInfo = new RaycastHit();

            if( Physics.Raycast( touchPointToRay, out hitInfo ) ){//何かにあたった時
                if(hitInfo.collider.gameObject.CompareTag("mass")){//あたったのがマスなら処理開始
                    massposition = hitInfo.collider.gameObject.transform.position;


                    if(Bcounter==Wcounter){//1Pのターン
                        if(CheckSetable(int.Parse(hitInfo.collider.name.Remove(0,5)))){//色が大丈夫かの判定
                            CM1.SetStone(massposition,Bcounter);//移動
                            //UIの変更
                            Bcounter++;//カウント
                            GData.massdataArray[int.Parse(hitInfo.collider.name.Remove(0,5))] = 1;//データに入れる
                        }
                    }else{
                        if(CheckSetable(int.Parse(hitInfo.collider.name.Remove(0,5)))){//色が大丈夫かの判定
                            CM2.SetStone(massposition,Wcounter);//移動
                            Wcounter++; //カウント
                            GData.massdataArray[int.Parse(hitInfo.collider.name.Remove(0,5))] = 2;//データに入れる
                        }
                    }

                    //フェーズ1終了判定
                    if(Bcounter==7&&Wcounter==7) {
                        f1=false;
                        Next.GetComponent<Changefhase1to2>().enabled = true;
                        this.GetComponent<PlayerLogic>().enabled = false;
                    }

                    StartCoroutine("ChangeTurn");

                }
            }
        }
    }

    IEnumerator ChangeTurn()
    {
        if(Bcounter==Wcounter)          co = new Color(0.2f, 0.2f, 0.2f);//2Pのターンの終わりにBにする
        else                            co = new Color(0.7f, 0.7f, 0.7f);//1Pのターンの終わりにWにする
        
        upper.DOColor(co,1f);

        //停止
        yield return new WaitForSeconds(1);
    }

    bool CheckSetable(int mass){

        if(Bcounter==Wcounter){//1Pのターン
            if(GData.masscolorarray[mass]==Color.red){//赤のとき
                if(GData.BR>0){
                    GData.BR--;
                    BR.text = GData.BR.ToString();
                    return true;
                }else if(GData.BA>0){
                    GData.BA--;
                    BA.text = GData.BA.ToString();
                    return true;
                }
            }else if(GData.masscolorarray[mass]==Color.green){//緑のとき
                if(GData.BG>0){
                    GData.BG--;
                    BG.text = GData.BG.ToString();
                    return true;
                }else if(GData.BA>0){
                    GData.BA--;
                    BA.text = GData.BA.ToString();
                    return true;
                }
            }else if(GData.masscolorarray[mass]==Color.blue){//青のとき
                if(GData.BB>0){
                        GData.BB--;
                        BB.text = GData.BB.ToString();
                        return true;
                }else if(GData.BA>0){
                    GData.BA--;
                    BA.text = GData.BA.ToString();
                    return true;
                }
            }
        }else{//2Pのターン
            if(GData.masscolorarray[mass]==Color.red){//赤のとき
                if(GData.WR>0){
                    GData.WR--;
                    WR.text = GData.WR.ToString();
                    return true;
                }else if(GData.WA>0){
                    GData.WA--;
                    WA.text = GData.WA.ToString();
                    return true;
                }
            }else if(GData.masscolorarray[mass]==Color.green){//緑のとき
                if(GData.WG>0){
                    GData.WG--;
                    WG.text = GData.WG.ToString();
                    return true;
                }else if(GData.WA>0){
                    GData.WA--;
                    WA.text = GData.WA.ToString();
                    return true;
                }
            }else if(GData.masscolorarray[mass]==Color.blue){//青のとき
                    if(GData.WB>0){
                        GData.WB--;
                        WB.text = GData.WB.ToString();
                        return true;
                }else if(GData.WA>0){
                    GData.WA--;
                    WA.text = GData.WA.ToString();
                    return true;
                }
            }
        }

        return false;
    }

    void phase1TurnUI(int i){
        // upper
    }

}
