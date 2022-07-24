using System.Collections;
using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace TresCoralMorris
{
    public class Fhase2Manager : MonoBehaviour
    {
        [SerializeField] PlayerInput _playerInput;
        [SerializeField] StoneInMass _stoneInMass;
        [SerializeField] MassManager _massManager;

        public IObservable<Unit> OnReloadView => _reload;
        private readonly Subject<Unit> _reload = new Subject<Unit>();

        private readonly ReactiveProperty<int> turn = new ReactiveProperty<int>();


        //UIが購読する
        public IReactiveProperty<int> BScore  => _bPlayerScore;
        private readonly ReactiveProperty<int> _bPlayerScore = new ReactiveProperty<int>();
        public IReactiveProperty<int> WScore  => _wPlayerScore;
        private readonly ReactiveProperty<int> _wPlayerScore = new ReactiveProperty<int>();

        enum Sequence{
            Ready=0,
            SelectStone=1,
            SelectMass=2,
            Move=3,
            End=4,
            Mill=36
        }
        //ターンとまとめるかも
        private Sequence _sequence=Sequence.SelectStone;

        StoneInMass.MillResult millResult;

        private IMass _beforeMass;
        private IStone _selectedStone;
        private IMass _afterMass;

        void Start()
        {
            GameManager.I.Phase
            .Where(t => t == GamePhase.Phase2)
            .Subscribe(_ => Init())
            .AddTo(this);
        }

        private void Init(){
            //各種購読を開始
            //inputをうけとる
            _playerInput.Click
            .ThrottleFirst(TimeSpan.FromSeconds(2))
            .Subscribe(_ => ExecuteOfTurn())
            .AddTo(this);

            //ターンの変更
            turn
            .Skip(1)
            .Subscribe(_ => Turn.I.TurnChange())
            .AddTo(this);

            _reload.OnNext(Unit.Default);
        }

        private void ExecuteOfTurn(){
            if(_sequence==Sequence.SelectStone)        Phase21();
            else if(_sequence==Sequence.SelectMass)   Phase22();
            else if(_sequence==Sequence.Mill)  PhaseMill();


            debug_p2log.I.DebugLogPhase(_sequence.ToString());
        }

        private void Phase20(){
            //マイカラーのマスに自分の色の石が載っているか確認
            if(!CheckMovable())return;
            
            //まだ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            //先へ進めるかどうか
            bool CheckMovable(){
                // //movableを確認
                // if(_gameDate.CheckMovable(mass))    return true;
                // //もし、駄目ならCCボタンを光らせる
                return false;
            }

        }

        //動かす石を決める
        private void Phase21(){
            //エラーチェック
            if(_playerInput.GetStone.Value==null &&  _playerInput.GetMass.Value==null) return;

            //石を入手
            var stone = _playerInput.GetStone.Value;
            //マスを入手
            var mass = _playerInput.GetMass.Value;

            //駄目な選択のチェック
            if(!CheckClick()) return;
            if(!CheckClick2())return;

            //チェックが大丈夫なら次のフェーズへ
            Debug.Log("チェックを抜けました1");
            //TODOコマ選択中エフェクト再生
            _beforeMass = mass;
            _selectedStone = stone;
            // EffectManager.I.PlayEffect();
            SEManager.I.Click();
            debug_p2log.I.DebugLogObj<IStone>(stone);
            _sequence++;


            #region 確認用ローカル変数
            //クリックした石がプレイヤー色か確認
            bool CheckClick(){
                if(stone.Color.Value == Turn.I.TurnColor.Value)   return true;
                SEManager.I.Cancel();
                Debug.Log("クリックした石がプレイヤー色ではありません");
                return false;
            }

            //クリックしたマスがマイカラーと同じ&&マスがグレーならセーフ(true)
            bool CheckClick2(){
                if(mass.Color == MassColor.Neu)      return true;
                if(MyColorManager.I.CheckMyColor(mass.Color))    return true;

                Debug.Log("クリックしたマスがマイカラーかグレーではありません");
                SEManager.I.Cancel();
                return false;
            }
            #endregion

        }

        //移動先のマスを探す
        private void Phase22(){
            //エラーチェック
            if(_playerInput.GetMass.Value==null) return;
            
            //マスを入手
            var mass = _playerInput.GetMass.Value;

            //準備
            if(!CheckClick()) return;
            if(!CheckClick2())return;
            Debug.Log("チェックを抜けました2");

            _afterMass = mass;
            Phase23();

            #region  確認用ローカル関数
            //クリックしたマスが移動可能なマスか確認
            //もし選んだマスのMovableの中に今選んだマスのidがあれば移動可能
            bool CheckClick(){
                if(_beforeMass.MovableCheck(mass.ID)) return true;
                SEManager.I.Cancel();
                Debug.Log("Movableなますではありません");
                return false;
            }

            //クリックしたマスが移動可能な色か確認
            bool CheckClick2(){
                if(MyColorManager.I.CheckMovableColor(mass.Color)) return true;
                SEManager.I.Cancel();
                Debug.Log("MovableColorなますではありません");
                return false;
            }
            #endregion
        }



        //石を移動して、ミルチェックを行う
        private void Phase23(){
            Debug.Log("移動処理します");
            _sequence=Sequence.Move;
            //移動
            _stoneInMass.SetStone(Turn.I.TurnColor.Value, _afterMass.ID,_selectedStone.ID.Value);

            //FIXMEミルならミルフェーズへ、無いならフェーズ4へ
            Debug.Log(_afterMass);
            millResult = _stoneInMass.CheckMill(_afterMass);

            if(millResult.Result)   _sequence= Sequence.Mill;
            else    Phase24();
        }


        //ミルのときのしょり
        private void PhaseMill(){
            Debug.Log("ミル処理します");
            //石を入手
            var stone = _playerInput.GetStone.Value;
            //マスを入手
            var mass = _playerInput.GetMass.Value.ID;

            //クリックされた石がmillされた石にあるかを探索する。
            if (!millResult.IsDelete(stone))   return;
            _stoneInMass.DeleteStone(stone.ID.Value,mass);
            
            //スコア加算
            if(Turn.I.TurnColor.Value==PlayerColor.Black) _bPlayerScore.Value++;
            else if(Turn.I.TurnColor.Value==PlayerColor.White) _wPlayerScore.Value++;

            _sequence=Sequence.End;

        }

        private void Phase24(){
            Debug.Log("ターン終了処理します");
            _sequence=Sequence.End;
            //白のターン終わりのみ発動
            if(Turn.I.TurnColor.Value==PlayerColor.White){
                //チェック
                var checkGrayMass  = _massManager.GetGrayMass();

                //trueなら処理実行
                if(checkGrayMass.IsGrayMass)    _massManager.NeutralizationMassColor(checkGrayMass.GetRandomGrayMass());
                else{
                    //色完全消去ならゲーム終了
                    GameManager.I.EndPhase();
                }
            } 

            //ターン変更
            turn.Value++;
            _sequence = Sequence.SelectStone;
            Debug.Log("ターン終了");
        }

        public void Cancel(){
            if(GameManager.I.Phase.Value != GamePhase.Phase2) return;
            SEManager.I.Cancel();
            _sequence = Sequence.SelectStone;
            Debug.Log("もどしました");
        }

        public void ColorChange(){
            Phase24();
            _reload.OnNext(Unit.Default);
        }
    }
}
