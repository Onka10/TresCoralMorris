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

        // public IReactiveProperty<PlayerColor> TurnColor  => _turnColor;
        private readonly ReactiveProperty<PlayerColor> _turnColor = new ReactiveProperty<PlayerColor>();

        private readonly ReactiveProperty<int> turn = new ReactiveProperty<int>();


        //UIが購読する
        public IReactiveProperty<int> BScore  => _bPlayerScore;
        private readonly ReactiveProperty<int> _bPlayerScore = new ReactiveProperty<int>();
        public IReactiveProperty<int> WScore  => _wPlayerScore;
        private readonly ReactiveProperty<int> _wPlayerScore = new ReactiveProperty<int>();

        //ターンとまとめるかも
        private int _phase=1;
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
            .ThrottleFirst(TimeSpan.FromSeconds(3))
            .Subscribe(_ => ExecuteOfTurn())
            .AddTo(this);

            //ターンの変更
            turn
            .Subscribe(_ => Change_turnColor())
            .AddTo(this);

            _reload.OnNext(Unit.Default);
        }

        private void Change_turnColor(){
            _turnColor.Value = turn.Value % 2==0 ? PlayerColor.Black:PlayerColor.White;
            // Debug.Log("今は"+_turnColor);
        }

        private void ExecuteOfTurn(){
            if(_phase==1)        Phase21();
            else if(_phase==2)   Phase22();
            else if(_phase==3)   Phase23();
            else if(_phase==36)  PhaseMill();
            else if(_phase==4)   Phase24();

            debug_p2log.I.DebugLogPhase(_phase);
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
            // Debug.Log("フェーズ2");
            //石を入手
            var stone = _playerInput.GetStone.Value;
            // Debug.Log(stone);
            
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
            _phase++;


            #region 確認用ローカル変数
            //クリックした石がプレイヤー色か確認
            bool CheckClick(){
                if(stone.Color.Value == _turnColor.Value)   return true;
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
            //石を入手
            var stone = _playerInput.GetStone.Value;
            
            //マスを入手
            var mass = _playerInput.GetMass.Value;

            //準備
            if(!CheckClick()) return;
            if(!CheckClick2())return;
            Debug.Log("チェックを抜けました2");

            _afterMass = mass;
            _phase++;

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
            //移動
            _stoneInMass.SetStone(Turn.I.TurnColor.Value, _afterMass.ID,_selectedStone.ID.Value);

            //FIXMEミルならミルフェーズへ、無いならフェーズ4へ
            millResult = _stoneInMass.CheckMill(_afterMass);

            if(millResult.Result)   _phase= 36;
            else    _phase=4;
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
            _stoneInMass.DeleteStone(_turnColor.Value,stone.ID.Value,mass);
            
            //スコア加算
            if(_turnColor.Value==PlayerColor.Black) _bPlayerScore.Value++;
            else if(_turnColor.Value==PlayerColor.White) _wPlayerScore.Value++;

            _phase=4;

        }

        private void Phase24(){
            //白のターン終わりのみ発動
            if(_turnColor.Value==PlayerColor.White){
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
            _phase = 1;
        }

        public void Cancel(){
            if(GameManager.I.Phase.Value != GamePhase.Phase2) return;
            SEManager.I.Cancel();
            _phase = 1;
            Debug.Log("もどしました");
        }
    }
}
