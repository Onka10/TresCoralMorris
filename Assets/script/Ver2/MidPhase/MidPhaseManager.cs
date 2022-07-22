using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

namespace TresCoralMorris{
    public class MidPhaseManager : MonoBehaviour
    {
        public MassColor B = MassColor.Neu;
        public MassColor W = MassColor.Neu;

        public IObservable<Unit> OnReloadView => _reload;
        private readonly Subject<Unit> _reload = new Subject<Unit>();

        void Start(){
            GameManager.I.Phase
            .Where(t => t == GamePhase.MidPhase)
            .Subscribe(_ => Init())
            .AddTo(this);
        }
        async void Init(){
            // Bが変わるのを待つ
            await UniTask.WaitUntil(() => B !=MassColor.Neu);
            await UniTask.WaitUntil(() => W !=MassColor.Neu);

            //mycolor発行
            IssueMyColors();

            await UniTask.Delay(2000);
            NextPhase();
        }

        public void SelectMyColorForButton(int color){
            SEManager.I.Click();
            if(Turn.I.TurnColor.Value == PlayerColor.Black)         B =  (MassColor)Enum.ToObject(typeof(MassColor), color);
            else if(Turn.I.TurnColor.Value == PlayerColor.White)    W =  (MassColor)Enum.ToObject(typeof(MassColor), color);

            Turn.I.TurnChange();
        }

        private void IssueMyColors(){
            MyColorManager.I.SetMyColorForB(new MyColors(B,W));
            MyColorManager.I.SetMyColorForW(new MyColors(W,B));
        }

        private void NextPhase(){
            GameManager.I.EndPhase();

            Destroy(this.gameObject);
        }
    }
}