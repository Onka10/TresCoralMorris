
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfomationLogic2 : MonoBehaviour
{
    [SerializeField] GameData GData;

    [SerializeField] GameObject BMyColor;//UIのmycolor B用

    [SerializeField] GameObject WMyColor;//UIのmycolor W用

    //UI部分
    [SerializeField] Image Bcolor;

    [SerializeField] Image Bmove1color;
    [SerializeField] Image Bmove2color;

    [SerializeField] Image Wcolor;
    [SerializeField] Image Wmove1color;
    [SerializeField] Image Wmove2color;

    //CCパネル
    [SerializeField] GameObject BCCpanel;
    [SerializeField] GameObject BCCbutton;
    [SerializeField] GameObject WCCpanel;
    [SerializeField] GameObject WCCbutton;

    //UIのCCボタン用
    [SerializeField] Image UIBcolor;
    [SerializeField] Image UIBmove1color;
    [SerializeField] Image UIBmove2color;

    [SerializeField] Image UIWcolor;
    [SerializeField] Image UIWmove1color;
    [SerializeField] Image UIWmove2color;

    //ターン交代の見た目変更
    [SerializeField] GameObject P1Panel;
    [SerializeField] GameObject P2Panel;

    [SerializeField] Image P1TAIKI;
    [SerializeField] Image P2TAIKI;

    [SerializeField] GameObject Mill;

    //スコア
    [SerializeField] Text score1;
    [SerializeField] Text score2;


    //UIの見た目変更
    public void initUIcolor(int p){
        if(p==1)    {
            UIBcolor.color = GData.MycolorB;
            UIBmove1color.color = Wmove1color.color;//敵のmovablecolorが実質、CC可能色
            UIBmove2color.color = Wmove2color.color;
        }else if(p==2){
            UIWcolor.color = GData.MycolorW;
            UIWmove1color.color = Bmove1color.color;
            UIWmove2color.color = Bmove2color.color;
        }
    }

    //マイカラーの更新
    public void SettingMycolor(){
        //上部UIの変更
        BMyColor.GetComponent<Image>().color=GData.MycolorB;
        WMyColor.GetComponent<Image>().color=GData.MycolorW;

        //下部UIの変更
        Bcolor.color=GData.MycolorB;//mycolorの変更
        if(GData.MycolorW==Color.red){
            Bmove1color.color=Color.green;
            Bmove2color.color=Color.blue;
        }else if(GData.MycolorW==Color.green){
            Bmove1color.color=Color.red;
            Bmove2color.color=Color.blue;
        }else if(GData.MycolorW==Color.blue){
            Bmove1color.color=Color.red;
            Bmove2color.color=Color.green;
        }

        Wcolor.color=GData.MycolorW;
        if(GData.MycolorB==Color.red){
            Wmove1color.color=Color.green;
            Wmove2color.color=Color.blue;
        }else if(GData.MycolorB==Color.green){
            Wmove1color.color=Color.red;
            Wmove2color.color=Color.blue;
        }else if(GData.MycolorB==Color.blue){
            Wmove1color.color=Color.red;
            Wmove2color.color=Color.green;
        }

    }

    public void ChangeUIturn(int p){//基本的にターン終わりに使う
        if(p==1){//Bターン終わりに...
            //今のターンのUIを消す
            P1Panel.SetActive(false);
            // P2Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            //次のターンを付ける
            P2Panel.SetActive(true);
            // P2Panel.transform.DOScale(new Vector3(0, 0, 0), 0.5f);


            //消える。それっきりやけど
            // P2TAIKI.transform.DOScale(new Vector3(0, 0, 0), 1);
        }else if(p==2){//Wターンに
            //今のターンのUIを消す
            P2Panel.SetActive(false);
            //パネル消して、待機を縮ませる
            P1Panel.SetActive(true);

        }
    }

    public void CloseCC(int p){
        if(p==1){
            BCCpanel.SetActive(false);
            BCCbutton.SetActive(true);
        }else if(p==2){
            WCCpanel.SetActive(false);
            WCCbutton.SetActive(true);
        }
    }

    public void MillAnimation(bool i){
        Mill.SetActive(i);//trueは付ける
        // Mill.GetComponent<Image>()
        // .DOFade(0f,4f)//フェード
        // // .SetEase(Ease.OutQuart)//easeタイプ
        // .SetLoops(-1, LoopType.Yoyo);
    }

    public bool GetMoveColor(int player,Color mass){//適合すれば(movableカラーならtrueを返す)
        if(player==1){//黒
            if(mass==Bmove1color.color) return true;
            if(mass==Bmove2color.color) return true;
            if(mass==Color.gray)        return true;
        }else if(player==2){
            if(mass==Wmove1color.color) return true;
            if(mass==Wmove2color.color) return true;
            if(mass==Color.gray)        return true;
        }
        return false;
    }
    
    public void AddScore(int player,int score){
        if(player==1){
            score1.text=score.ToString();
        }else{
            score2.text=score.ToString();
        }
    }

}
