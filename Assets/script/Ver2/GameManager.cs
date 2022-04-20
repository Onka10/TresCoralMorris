using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using VContainer;
using TresCoralMorris;
using UnityEngine.SceneManagement;

namespace TresCoralMorris{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Fhase1Manager _f1Manager;
        [SerializeField] FhaseChangeManager _changeManager;
        [SerializeField] Fhase2Manager _f2Manager;
        [SerializeField] TresCoralMorris.GameDate _gamedate;
        [SerializeField] ColorManager _colormanager;



        void Start()
        {
            Init().Forget();
        }

        private async UniTask Init(){
            //データの初期化
            _gamedate.InitGameDate();

            //色の初期化待ち
            // await UniTask.WaitUntil(() => _colorset.END);
            _colormanager.InitColor();
            
            //操作不可を解除

            //フェーズ切り替え
            await UniTask.WaitUntil(() => _f1Manager.END);
            Debug.Log("１おわり");
            await UniTask.WaitUntil(() => _changeManager.END);
            Debug.Log("2がはじまる");
            await UniTask.WaitUntil(() => _f2Manager.END);

            //リザルト画面へ移動
            SceneManager.LoadScene("Result");
        }

    }
}
