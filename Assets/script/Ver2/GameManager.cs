using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

namespace TresCoralMorris{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] Fhase1Manager _f1Manager;
        [SerializeField] FhaseChangeManager _changeManager;
        [SerializeField] Fhase2Manager _f2Manager;
        [SerializeField] TresCoralMorris.GameDate _gamedate;
        [SerializeField] MassManager _massManager;


        public IReactiveProperty<GamePhase> Phase  => _phase;
        private readonly ReactiveProperty<GamePhase> _phase = new ReactiveProperty<GamePhase>();

        void Start()
        {
            Inita().Forget();
        }

        private async UniTask Inita(){
            //データの初期化
            _gamedate.InitGameDate();
            _massManager.InitData();

            //色の初期化待ち
            _massManager.InitColor();
            
            //操作不可を解除

            //フェーズ切り替え
            _phase.Value = GamePhase.Phase1;
            await UniTask.WaitUntil(() => _f1Manager.END);
            Debug.Log("１おわり");
            _phase.Value = GamePhase.MidPhase;
            await UniTask.WaitUntil(() => _changeManager.END);
            Debug.Log("2がはじまる");
            _phase.Value = GamePhase.Phase2;
            await UniTask.WaitUntil(() => _f2Manager.END);

            //リザルト画面へ移動
            SceneManager.LoadScene("Result");
        }

    }
}
