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
            if(Turn.I.TurnColor.Value == PlayerColor.Black)         B =  (MassColor)Enum.ToObject(typeof(MassColor), color);
            else if(Turn.I.TurnColor.Value == PlayerColor.White)    W =  (MassColor)Enum.ToObject(typeof(MassColor), color);

            Turn.I.TurnChange();
        }

        private void IssueMyColors(){
            MyColorManager.I.SetMyColorForB(C(B,W));
            MyColorManager.I.SetMyColorForW(C(W,B));
            
            MyColors C(MassColor mycolor,MassColor enemycolor){
                MassColor move1 = MassColor.Neu;
                MassColor move2 = MassColor.Neu;

                if(enemycolor==MassColor.Red){
                    move1 = MassColor.Green;
                    move2 = MassColor.Blue;
                }else if(enemycolor==MassColor.Green){
                    move1 = MassColor.Red;
                    move2 = MassColor.Blue;
                }else if(enemycolor==MassColor.Blue){
                    move1 = MassColor.Red;
                    move2 = MassColor.Green;
                }

                if(move1 == MassColor.Neu)  throw new Exception("mycolor や enemycolorが正しくありません");
                MyColors myColors = new MyColors(mycolor,move1,move2);
                return myColors;
            }
        }

        private void NextPhase(){
            GameManager.I.EndPhase();

            Destroy(this.gameObject);
        }
    }
}