using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TresCoralMorris;
using System;

namespace TresCoralMorris
{
    public class Fhase1Manager : MonoBehaviour
    {
        // UIがまだ


        public bool END=false;

        public IReactiveProperty<PlayerColor> TurnColor  => _turnColor;
        private readonly ReactiveProperty<PlayerColor> _turnColor = new ReactiveProperty<PlayerColor>();

        public IObservable<Unit> Next => _next;
        private Subject<Unit> _next = new Subject<Unit>();

        private readonly ReactiveProperty<int> turn = new ReactiveProperty<int>();

        [SerializeField] PlayerInput _playerInput;
        [SerializeField] TresCoralMorris.GameDate _gameDate;

        int _blackStoneNum=0;
        int _whiteStoneNum=0;

        public IReactiveCollection<int> BSetable => BPlayerSetable;
        private readonly ReactiveCollection<int> BPlayerSetable = new ReactiveCollection<int>(){ 1, 2, 2, 2};
        public IReactiveCollection<int> WSetable => WPlayerSetable;
        private readonly ReactiveCollection<int> WPlayerSetable = new ReactiveCollection<int>(){ 1, 2, 2, 2};

        void Start()
        {
            //終了
            turn
            .Where(x => x == 14)
            .Subscribe(_ => NextFhase())
            .AddTo(this);

            //ターンの変更
            turn
            .Subscribe(_ => Change_turnColor())
            .AddTo(this);

            //inputをうけとる
            _playerInput.Click
            .Where(_ => _gameDate.massinstone[_playerInput.GetMass.Value.ID] == PlayerColor.Empty)
            .Subscribe(_ => ExsecuteOfTurn())
            .AddTo(this);
        }

        private void ExsecuteOfTurn(){
            //マスのidを入手
            int massID = _playerInput.GetMass.Value.ID;
            //マスの色を入手
            var getmasscolor = _gameDate.mass[massID].GetComponent<IMass>().Color.Value;


            //ターンの判定
            if(_turnColor.Value==PlayerColor.Black){

                ////まだ置ける色があるか判定
                if(BPlayerSetable[(int)getmasscolor]>0){
                    // Debug.Log(getmasscolor+"はまだおける");
                    BPlayerSetable[(int)getmasscolor] -=1;
                }else if(BPlayerSetable[0]==1){
                    // Debug.Log("any消費");
                    BPlayerSetable[0]=0;
                }else{
                    return;
                }

                //石をおく
                _gameDate.SetStone(PlayerColor.Black, massID,_blackStoneNum);
                _blackStoneNum++;
                turn.Value++;
            }else if(_turnColor.Value==PlayerColor.White){

                ////まだ置ける色があるか判定
                if(WPlayerSetable[(int)getmasscolor]>0){
                    // Debug.Log(getmasscolor+"はまだおける");
                    WPlayerSetable[(int)getmasscolor] -=1;
                }else if(WPlayerSetable[0]==1){
                    // Debug.Log("any消費");
                    WPlayerSetable[0]=0;
                }else{
                    return;
                }

                //石をおく
                _gameDate.SetStone(PlayerColor.White, massID,_whiteStoneNum);
                _whiteStoneNum++;
                turn.Value++;
            }
        }

        private void Change_turnColor(){
            _turnColor.Value = turn.Value % 2==0 ? PlayerColor.Black:PlayerColor.White;
            // Debug.Log("今は"+_turnColor);
        }

        private void NextFhase(){
            _next.OnNext(Unit.Default);

            END = true;
            //ストリームを終了
            Destroy(this.gameObject);
        }
    }

}