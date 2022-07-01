using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

namespace TresCoralMorris{
    public class GameManager : Singleton<GameManager>
    {
        public IReactiveProperty<GamePhase> Phase  => _phase;
        private readonly ReactiveProperty<GamePhase> _phase = new ReactiveProperty<GamePhase>(GamePhase.Ready);

        void Start()
        {
            InitGame().Forget();
        }

        private async UniTask InitGame(){
            //全部待つ
            // await UniTask.WhenAll(() => Redy())

            //かり
            await UniTask.Delay(1000);
            Debug.Log("初期化");


            //フェーズ1
            Debug.Log("フェーズ1");
            _phase.Value = GamePhase.Phase1;
            await UniTask.WaitUntil(() => _phase.Value == GamePhase.MidPhase);
            
            //フェーズ1.5
            Debug.Log("1.5スタート");
            await UniTask.WaitUntil(() => _phase.Value == GamePhase.Phase2);
            
            //フェーズ２
            Debug.Log("2スタート");
        }

        public void EndPhase(){
            _phase.Value++;
        }

        public void EndGame(){
            //リザルト画面へ移動
            SceneManager.LoadScene("Result");

        }
    }
}
