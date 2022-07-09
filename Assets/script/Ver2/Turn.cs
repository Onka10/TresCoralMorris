using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Turn :Singleton<Turn>
{
        public IReactiveProperty<PlayerColor> TurnColor  => _turnColor;
        private readonly ReactiveProperty<PlayerColor> _turnColor = new ReactiveProperty<PlayerColor>();
        

        void Start(){
                _turnColor.Value = PlayerColor.Black;
        }


        public void TurnChange(){
                _turnColor.Value = _turnColor.Value == PlayerColor.White ? PlayerColor.Black:PlayerColor.White;
                Debug.Log(_turnColor.Value);
        }

        public void FirstCheck(){
                if(_turnColor.Value != PlayerColor.Black)       throw new System.Exception("ターンが不正です");
        }
}
