using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TresCoralMorris;
using DG.Tweening;

namespace TresCoralMorris{
    public class UI_F1Setable : MonoBehaviour
    {
        public Text[] _playerBCosts = new Text[4];
        public Text[] _playerWCosts = new Text[4];

        public Image _turnInfomation;

        public Fhase1Manager _F1Manager;

        void Start(){
            _F1Manager.BSetable.ObserveReplace()
            .Subscribe(s =>OnReplace(s))
            .AddTo(this);

            _F1Manager.WSetable.ObserveReplace()
            .Subscribe(s =>OnReplace(s))
            .AddTo(this);

            _F1Manager.TurnColor
            .Subscribe(c => ChangePlayerInfomation(c))
            .AddTo(this);
        }

        private void OnReplace(CollectionReplaceEvent<int> replaceEvent) {
            Debug.Log($"{replaceEvent.Index}番目の値が{replaceEvent.OldValue}→{replaceEvent.NewValue}に変更");

            if(_F1Manager.TurnColor.Value==PlayerColor.Black){
                _playerBCosts[replaceEvent.Index].text = replaceEvent.NewValue.ToString();
            }else if(_F1Manager.TurnColor.Value==PlayerColor.White){
                _playerWCosts[replaceEvent.Index].text = replaceEvent.NewValue.ToString();
            }

        }

        private void ChangePlayerInfomation(PlayerColor c){
            Color color= new Color(0.2f, 0.2f, 0.2f);
            if(c==PlayerColor.Black)        color = new Color(0.2f, 0.2f, 0.2f);//黒
            else if(c==PlayerColor.White)    color = new Color(0.7f, 0.7f, 0.7f);//白
            
            _turnInfomation.DOColor(color,1f);
        }
    
    }
}
