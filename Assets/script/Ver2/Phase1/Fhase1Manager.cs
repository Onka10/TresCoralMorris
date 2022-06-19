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

        public IReactiveProperty<PlayerColor> TurnColor  => _turnColor;
        private readonly ReactiveProperty<PlayerColor> _turnColor = new ReactiveProperty<PlayerColor>();

        public IObservable<Unit> OnReloadView => _reload;
        private readonly Subject<Unit> _reload = new Subject<Unit>();

        private readonly ReactiveProperty<int> _turn = new ReactiveProperty<int>(0);

        [SerializeField] PlayerInput _playerInput;
        [SerializeField] StoneInMass _stoneInMass;
        [SerializeField] MassManager _massManager;

        int _blackStoneNum=0;
        int _whiteStoneNum=0;

        public IReactiveCollection<int> BSetable => BPlayerSetable;
        private readonly ReactiveCollection<int> BPlayerSetable = new ReactiveCollection<int>(){ 1, 2, 2, 2};
        public IReactiveCollection<int> WSetable => WPlayerSetable;
        private readonly ReactiveCollection<int> WPlayerSetable = new ReactiveCollection<int>(){ 1, 2, 2, 2};


        void Start()
        {
            GameManager.I.Phase
            .Where(t => t == GamePhase.Phase1)
            .Subscribe(_ => Init())
            .AddTo(this);
        }

        void Init()
        {
            //終了
            _turn
            .Where(x => x == 14)
            .Subscribe(_ => NextPhase())
            .AddTo(this);

            //ターンの変更
            _turn
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => Turn.I.TurnChange())
            .AddTo(this);

            //inputをうけとる
            _playerInput.Click
            .Where(_ => !_stoneInMass.IsInStone(_playerInput.GetMass.Value.ID))
            .Subscribe(_ => ExecuteOfTurn())
            .AddTo(this);
        }

        private void ExecuteOfTurn(){
            //マスのidを入手
            int massID = _playerInput.GetMass.Value.ID;
            //マスの色を入手
            var getMassColor = _playerInput.GetMass.Value.Color.Value;

            var turn = Turn.I.TurnColor.Value;

            //ターンの判定
            if(turn==PlayerColor.Black){

                ////まだ置ける色があるか判定
                if(BPlayerSetable[(int)getMassColor]>0){
                    // Debug.Log(getmasscolor+"はまだおける");
                    BPlayerSetable[(int)getMassColor] -=1;
                }else if(BPlayerSetable[0]==1){
                    // Debug.Log("any消費");
                    BPlayerSetable[0]=0;
                }else{
                    return;
                }

                //石をおく
                _stoneInMass.SetStone(PlayerColor.Black, massID,_blackStoneNum);
                _blackStoneNum++;
                
                _turn.Value++;
            }else if(turn==PlayerColor.White){

                ////まだ置ける色があるか判定
                if(WPlayerSetable[(int)getMassColor]>0){
                    // Debug.Log(getmasscolor+"はまだおける");
                    WPlayerSetable[(int)getMassColor] -=1;
                }else if(WPlayerSetable[0]==1){
                    // Debug.Log("any消費");
                    WPlayerSetable[0]=0;
                }else{
                    return;
                }

                //石をおく
                _stoneInMass.SetStone(PlayerColor.White, massID,_whiteStoneNum);
                _whiteStoneNum++;
                _turn.Value++;
            }
        }

        private void NextPhase(){
            GameManager.I.EndPhase();

            //ストリームを終了
            Destroy(this.gameObject);
        }
    }

}