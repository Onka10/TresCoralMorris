using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace TresCoralMorris.UI{

public class Phase2View : MonoBehaviour
{
    [SerializeField] Fhase2Manager _p2Manager;
    [SerializeField] GameObject UI;

    //変更する対象のUI
    public Image[] BUIColor = new Image[4];
    public Image[] WUIColor = new Image[4];

    public Text BScore;
    public Text WScore;


    void Start()
    {
        GameManager.I.Phase
        .Where(p => p ==GamePhase.Phase2)
        .Subscribe(_ => Init())
        .AddTo(this);

    }

    private void Init(){
        _p2Manager.OnReloadView
        .Subscribe(_ =>{
            ChangeMycolorB(MyColorManager.I.BPlayerMyColors);
            ChangeMycolorW(MyColorManager.I.WPlayerMyColors);
        })
        .AddTo(this);

        //UIを表示
        UI.SetActive(true);

        //TODOUIを初期化と購読
        // ChangeMycolorB();
        // ChangeMycolorW();

        //各種購読を開始
        _p2Manager.BScore
        .Subscribe(s => BScore.text = s.ToString())
        .AddTo(this);

        _p2Manager.WScore
        .Subscribe(s => WScore.text = s.ToString())
        .AddTo(this);
    }

    private void ChangeMycolorB(MyColors myColors){
        //マイカラー
        BUIColor[0].color = ColorChanger.MassColorToColor(myColors.MyColor);
        //下UIのマイカラー
        BUIColor[1].color = ColorChanger.MassColorToColor(myColors.MyColor);
        //movableカラー
        BUIColor[2].color = ColorChanger.MassColorToColor(myColors.MoveableColor1);
        BUIColor[3].color = ColorChanger.MassColorToColor(myColors.MoveableColor2);
    }

    private void ChangeMycolorW(MyColors myColors){
        //マイカラー
        WUIColor[0].color = ColorChanger.MassColorToColor(myColors.MyColor);
        //下UIのマイカラー
        WUIColor[1].color = ColorChanger.MassColorToColor(myColors.MyColor);
        //movableカラー
        WUIColor[2].color = ColorChanger.MassColorToColor(myColors.MoveableColor1);
        WUIColor[3].color = ColorChanger.MassColorToColor(myColors.MoveableColor2);
    }
}

}
