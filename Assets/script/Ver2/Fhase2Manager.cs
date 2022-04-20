using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace TresCoralMorris
{
    public class Fhase2Manager : MonoBehaviour
    {
        public bool END=false;

        [SerializeField] PlayerInput _playerInput;
        [SerializeField] FhaseChangeManager _fhaseChangeManager;
        [SerializeField] TresCoralMorris.GameDate _gameDate;

        // public IReactiveProperty<PlayerColor> TurnColor  => _turnColor;
        private readonly ReactiveProperty<PlayerColor> _turnColor = new ReactiveProperty<PlayerColor>();

        private readonly ReactiveProperty<int> turn = new ReactiveProperty<int>();

        private int Phase=1;

        private IMass _selectedMass;
        private IStone _selectedStone;
        private IMass _destinationMass;

        void Start()
        {
            _fhaseChangeManager.Next
            .Subscribe(_ => Init())
            .AddTo(this);
        }

        private void Init(){
            //各種購読を開始
            //inputをうけとる
            _playerInput.Click
            .Subscribe(_ => ExecuteOfTurn())
            .AddTo(this);


            //ターンの変更
            turn
            .Subscribe(_ => Change_turnColor())
            .AddTo(this);
        }

        private void Change_turnColor(){
            _turnColor.Value = turn.Value % 2==0 ? PlayerColor.Black:PlayerColor.White;
            // Debug.Log("今は"+_turnColor);
        }

        private void ExecuteOfTurn(){
            if(Phase==1)        Phase21();
            else if(Phase==2)   Phase22();
            else if(Phase==3)   Phase23();
        }

        //動かす石を決める
        private void Phase21(){
            //石を入手
            var stone = _playerInput.GetStone.Value;
            
            //マスを入手
            var mass = _playerInput.GetMass.Value;

            //そのターンのマイカラー
            MassColor mycolor = MassColor.Neu;

            //ターンの判定
            if(_turnColor.Value==PlayerColor.Black){
                //準備
                mycolor = _gameDate.MyColorB.Value;
                if(!CheckClick()) return;
                if(!CheckClick2())return;
                if(!CheckClick3())return;

                _selectedMass = mass;
                _selectedStone = stone;
                Phase++;
            }else if(_turnColor.Value==PlayerColor.White){
                mycolor = _gameDate.MyColorW.Value;
            }
            


            //ローカル関数
            bool CheckClick(){
            //クリックした石がプレイヤー色か確認
            if(stone.Color.Value == _turnColor.Value)   return true;
            else    return false;
            }


            bool CheckClick2(){
            //クリックしたマスがマイカラーと同じ&&マスがグレー
            if(mass.Color.Value == mycolor || mass.Color.Value == MassColor.Neu)      return true;
            else     return false;
            }

            bool CheckClick3(){
                //今回のマスid
                // int massID = _gameDate.massinstone[mass.ID.Value];
                return true;
                // if(_gameDate.massinstone[massID] ==)
            }

        }

        //移動先のマスを探す
        private void Phase22(){
            var stone = _playerInput.GetStone.Value;
            
            //マスを入手
            var mass = _playerInput.GetMass.Value;

            //ターンの判定
            if(_turnColor.Value==PlayerColor.Black){
                //準備
                // mycolor = _gameDate.MyColorB.Value;
                if(!CheckClick()) return;
                if(!CheckClick2())return;

                _destinationMass = mass;

                Phase++;
                
            }else if(_turnColor.Value==PlayerColor.White){
                // mycolor = _gameDate.MyColorW.Value;
            }

            //ローカル関数

            bool CheckClick(){
            //クリックしたマスが移動可能なマスか確認
                for(int i=0;i< _selectedMass.MovebaleMass.Length;i++){
                    //もし選んだマスのMovebaleの中に今選んだマスのidがあれば移動可能
                    if(_selectedMass.MovebaleMass[i] == mass.ID.Value)   return true;
                }

                return false;
            }


            bool CheckClick2(){
                //クリックしたマスが移動可能な色か確認
                if(_turnColor.Value==PlayerColor.Black){
                    if(mass.Color.Value== _gameDate.MovebaleColorB1.Value)     return true;
                    if(mass.Color.Value== _gameDate.MovebaleColorB2.Value)     return true;
                    if(mass.Color.Value==MassColor.Neu)         return true;
                }else if(_turnColor.Value==PlayerColor.White){
                    if(mass.Color.Value== _gameDate.MovebaleColorW1.Value)     return true;
                    if(mass.Color.Value== _gameDate.MovebaleColorW2.Value)     return true;
                    if(mass.Color.Value==MassColor.Neu)         return true;
                }
                return false;
            }
        }

        //石を移動して、ミルチェックを行う
        private void Phase23(){
            
        }
    }
}
