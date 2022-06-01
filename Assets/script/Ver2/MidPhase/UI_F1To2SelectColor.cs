using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UI_F1To2SelectColor : MonoBehaviour
{
    [SerializeField] Image _bPlayerMycolor;
    [SerializeField] Image _wPlayerMycolor;

    [SerializeField] GameObject _panelB;
    [SerializeField] GameObject _panelW;

    [SerializeField] TresCoralMorris.GameDate _gamedata;
    [SerializeField] FhaseChangeManager _fcManager;

    void Start()
    {
        _gamedata.MyColorB
        .Subscribe(c=>  ChangeImageB(c))
        .AddTo(this);

        _gamedata.MyColorW
        .Subscribe(c=>  ChangeImageW(c))
        .AddTo(this);
        
        _fcManager.Turn
        .Where(t => t==2)
        .Subscribe(_ => ChangePanel())
        .AddTo(this);
    }

    private void ChangePanel(){
        _panelB.SetActive(false);
        _panelW.SetActive(true);
    }

    private void ChangeImageB(MassColor c){
        if(c==MassColor.Red)        _bPlayerMycolor.color = Color.red;
        else if(c==MassColor.Green) _bPlayerMycolor.color = Color.green;
        else if(c==MassColor.Blue)  _bPlayerMycolor.color = Color.blue;
    }

    private void ChangeImageW(MassColor c){
        if(c==MassColor.Red)        _wPlayerMycolor.color = Color.red;
        else if(c==MassColor.Green) _wPlayerMycolor.color = Color.green;
        else if(c==MassColor.Blue)  _wPlayerMycolor.color = Color.blue;
    }

}
