using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UI_F2Manager : MonoBehaviour
{
    [SerializeField] FhaseChangeManager _fhaseChangeManager;
    [SerializeField] TresCoralMorris.GameDate _gameDate;


    //変更する対象のUI
    public Image[] BUIColor = new Image[4];
    public Image[] WUIColor = new Image[4];

    //Dictionaryを作成
    Dictionary<MassColor, Color> _colorDictionary;

    void Start()
    {

    _colorDictionary = new Dictionary<MassColor, Color> (){
        {MassColor.Red,Color.red},
        {MassColor.Blue,Color.blue},
        {MassColor.Green,Color.green},       
    };

        _fhaseChangeManager.Next
        .Subscribe(_ => Init())
        .AddTo(this);

        _gameDate.MyColorB
        .Subscribe()
        .AddTo(this);

        _gameDate.MyColorW
        .Subscribe()
        .AddTo(this);
    }

    private void Init(){
        //UIを初期化
        ChangeImageB();
        ChangeImageW();

        //各種購読を開始

    }

    private void ChangeImageB(){
        //マイカラー
        BUIColor[0].color = _colorDictionary[_gameDate.MyColorB.Value];
        //下UIのマイカラー
        BUIColor[1].color = _colorDictionary[_gameDate.MyColorB.Value];
        //初期化してないからコメントアウト
        //movableカラー
        // BUIColor[2].color = _colorDictionary[_gameDate.MovebaleColorB1.Value];
        // BUIColor[3].color = _colorDictionary[_gameDate.MovebaleColorB2.Value];

    }

    private void ChangeImageW(){
        //マイカラー
        WUIColor[0].color = _colorDictionary[_gameDate.MyColorW.Value];
        //下UIのマイカラー
        WUIColor[1].color = _colorDictionary[_gameDate.MyColorW.Value];
        //初期化してないからコメントアウト
        //movableカラー
        // WUIColor[2].color = _colorDictionary[_gameDate.MovebaleColorW1.Value];
        // WUIColor[3].color = _colorDictionary[_gameDate.MovebaleColorW2.Value];
    }
}
