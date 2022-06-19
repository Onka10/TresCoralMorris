using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using TresCoralMorris;
using DG.Tweening;

namespace TresCoralMorris.Phase1.UI{
    public class Phase1View : MonoBehaviour
    {
        [SerializeField] Fhase1Manager _fhase1Manager;


        public Text[] _playerBCosts = new Text[4];
        public Text[] _playerWCosts = new Text[4];

        void Start(){
            _fhase1Manager.BSetable.ObserveReplace()
            .Subscribe(s =>OnReplace(s))
            .AddTo(this);

            _fhase1Manager.WSetable.ObserveReplace()
            .Subscribe(s =>OnReplace(s))
            .AddTo(this);


            // GameManager.I.Phase
            // .Where(p => p ==GamePhase.MidPhase)
            // // .Subscribe(_ => Destroy(this.gameObject))
            // .Subscribe(_ => Debug.Log("test"))
            // .AddTo(this);
        }

        private void OnReplace(CollectionReplaceEvent<int> replaceEvent) {
            // Debug.Log($"{replaceEvent.Index}番目の値が{replaceEvent.OldValue}→{replaceEvent.NewValue}に変更");

            if(Turn.I.TurnColor.Value==PlayerColor.Black){
                _playerBCosts[replaceEvent.Index].text = replaceEvent.NewValue.ToString();
            }else if(Turn.I.TurnColor.Value==PlayerColor.White){
                _playerWCosts[replaceEvent.Index].text = replaceEvent.NewValue.ToString();
            }

        }
}
}

