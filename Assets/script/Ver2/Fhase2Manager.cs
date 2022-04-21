using System.Collections;
using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

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

        private readonly ReactiveProperty<int> BScore = new ReactiveProperty<int>();
        private readonly ReactiveProperty<int> WScore = new ReactiveProperty<int>();

        private int Phase=1;

        private int[] _milledStone = new int[3];

        private IMass _beforeMass;
        private IStone _selectedStone;
        private IMass _afterMass;

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
            else if(Phase==36)  Phasemill();
            else if(Phase==4)   Phase24();
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

                _beforeMass = mass;
                _selectedStone = stone;
                Phase++;
            }else if(_turnColor.Value==PlayerColor.White){
                mycolor = _gameDate.MyColorW.Value;
                if(!CheckClick()) return;
                if(!CheckClick2())return;
                if(!CheckClick3())return;

                _beforeMass = mass;
                _selectedStone = stone;
                Phase++;
            }
            


            //クリックした石がプレイヤー色か確認
            bool CheckClick(){
                if(stone.Color.Value == _turnColor.Value)   return true;
                else    return false;
            }


            //クリックしたマスがマイカラーと同じ&&マスがグレー
            bool CheckClick2(){
                if(mass.Color.Value == mycolor || mass.Color.Value == MassColor.Neu)      return true;
                else     return false;
            }

            //まだ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            //先へ進めるかどうか
            bool CheckClick3(){
                //movableを確認
                //もし、駄目ならCCボタンを光らせる
                return true;
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
                if(!CheckClick()) return;
                if(!CheckClick2())return;

                _afterMass = mass;

                Phase++;
                
            }else if(_turnColor.Value==PlayerColor.White){

            }

            //ローカル関数

            //クリックしたマスが移動可能なマスか確認
            //もし選んだマスのMovebaleの中に今選んだマスのidがあれば移動可能
            bool CheckClick(){
                for(int i=0;i< _beforeMass.MovebaleMass.Length;i++){
                    if(_beforeMass.MovebaleMass[i] == mass.ID)   return true;
                }
                return false;
            }

            //クリックしたマスが移動可能な色か確認
            bool CheckClick2(){
                if(_turnColor.Value==PlayerColor.Black){
                    if(mass.Color.Value== _gameDate.MovebaleColorB1.Value || mass.Color.Value== _gameDate.MovebaleColorB2.Value)     return true;
                    if(mass.Color.Value==MassColor.Neu)         return true;
                }else if(_turnColor.Value==PlayerColor.White){
                    if(mass.Color.Value== _gameDate.MovebaleColorW1.Value || mass.Color.Value== _gameDate.MovebaleColorW2.Value)     return true;
                    if(mass.Color.Value==MassColor.Neu)         return true;
                }
                return false;
            }
        }



        //石を移動して、ミルチェックを行う
        private void Phase23(){
            //移動
            _gameDate.SetStone(_turnColor.Value,_afterMass.ID, _selectedStone.ID.Value);

            //ミルチェック
            if(MillCheck()){
                Debug.Log("ミル");
                Phase = 36;
            }else{
                Phase=4;
            }

            bool MillCheck(){
                return true;
            }
            
        }


        //ミルのときのしょり
        private void Phasemill(){
            //石を入手
            var stone = _playerInput.GetStone.Value;

            //ミルの対象ならコマを消す
            if (0 <= Array.IndexOf(_milledStone, stone.ID.Value)){
                _gameDate.DeleteStone(_turnColor.Value,stone.ID.Value);
                //スコア加算
                if(_turnColor.Value==PlayerColor.Black) BScore.Value++;
                else if(_turnColor.Value==PlayerColor.White) WScore.Value++;

                Phase=4;
            }
        }

        private void Phase24(){
            //色消滅処理

            //色完全消去ならゲーム終了

            //ターン変更
            turn.Value++;
            Phase = 1;
        }
    }
}
