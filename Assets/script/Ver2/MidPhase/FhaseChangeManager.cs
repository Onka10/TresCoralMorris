using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using TresCoralMorris;

public class FhaseChangeManager : MonoBehaviour
{
    public IReactiveProperty<int> Turn  => _turnsubject;
    private readonly ReactiveProperty<int> _turnsubject = new ReactiveProperty<int>(1);

    [SerializeField]GameObject _phase1Prefab;
    [SerializeField]GameObject _phase1to2Prefab;
    [SerializeField]GameObject _phase2Prefab;

    [SerializeField]TresCoralMorris.GameDate _gameDate;
    [SerializeField]Fhase1Manager _f1Manager;

    void Start()
    {
        GameManager.I.Phase
        .Where(t => t == GamePhase.MidPhase)
        .Subscribe(_ => Init())
        .AddTo(this);
    }
    void Init(){

        _turnsubject
        .Where(t => t==3)
        .Subscribe(_ => NextFhase())
        .AddTo(this);
    }

    // private void ChangeInfomation1TiMid(){
    //     _phase1Prefab.SetActive(false);
    //     _phase1to2Prefab.SetActive(true);
    // }

    // private void ChangeInfomationMidTo2(){
    //     _phase1to2Prefab.SetActive(false);
    //     _phase2Prefab.SetActive(true);
    // }

    public void SelectMyColor(int color){
        if(_turnsubject.Value==1)         _gameDate.MyColorB.Value =  (MassColor)Enum.ToObject(typeof(MassColor), color);
        else if(_turnsubject.Value==2)   _gameDate.MyColorW.Value =  (MassColor)Enum.ToObject(typeof(MassColor), color);

        _turnsubject.Value++;
    }

    private void NextFhase(){
        GameManager.I.EndPhase();

        Destroy(this.gameObject);
    }
}
