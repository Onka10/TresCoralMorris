using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UI_F2Manager : MonoBehaviour
{
    [SerializeField] FhaseChangeManager _fhaseChangeManager;
    [SerializeField] TresCoralMorris.GameDate _gameDate;


    //変更するUI
    public Image[] BUIColor = new Image[4];
    public Image[] WUIColor = new Image[4];

    void Start()
    {
        _fhaseChangeManager.Next
        .Subscribe(_ => Init())
        .AddTo(this);
    }

    private void Init(){
        //UIを初期化
        ChangeImageB(_gameDate.MyColorB.Value,0);
        ChangeImageW(_gameDate.MyColorW.Value,0);

        //各種購読を開始

    }

    private void ChangeImageB(MassColor c,int UIid){
        if(c==MassColor.Red)        BUIColor[UIid].color = Color.red;
        else if(c==MassColor.Green) BUIColor[UIid].color = Color.green;
        else if(c==MassColor.Blue)  BUIColor[UIid].color = Color.blue;
    }

    private void ChangeImageW(MassColor c,int UIid){
        if(c==MassColor.Red)        WUIColor[UIid].color = Color.red;
        else if(c==MassColor.Green) WUIColor[UIid].color = Color.green;
        else if(c==MassColor.Blue)  WUIColor[UIid].color = Color.blue;
    }
}
