using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Collections.Generic;
using TresCoralMorris.MassData;

namespace TresCoralMorris{
    /// <summary>
    /// マス全体に何かする時、データの入力処理と
    /// </summary>
    public class MassManager : MonoBehaviour
    {
        public GameObject massParent;

        private Mass[] mass = new Mass[24];
        GrayMassList _grayMassList;

        private void Start(){
            //キャッシュ
            for(int i=0 ;i<mass.Length;i++){
                mass[i] = massParent.transform.GetChild(i).gameObject.GetComponent<Mass>();
            }

            GameManager.I.Phase
            .Where(t => t==GamePhase.Ready)
            .Subscribe(_ => Init())
            .AddTo(this);
        }

        public void Init(){
            //データの初期化
            for(int i=0;i<mass.Length;i++){
                mass[i].Init(i);
            }

            //colorの初期化
            int[] counter = new int[3];

            for(int index=0;index<mass.Length;index++){
                int colorNum = Random.Range(0,3);
                while(counter[colorNum]== 8){
                    colorNum=(colorNum+1)%3;
                }
                counter[colorNum]++;

                //+1した値がMassColorに相当
                //変更
                MassColor massColor = (MassColor)colorNum+1;
                mass[index].SetColor(massColor);
            }
        }

        /// <summary>
        /// 指定したidのマスを中立化
        /// </summary>
        public void NeutralizationMassColor(){
            int id = (int)UnityEngine.Random.Range(0f, _grayMassList.NotGrayArray.Count);

            mass[id].SetColor(MassColor.Neu);
        }

        /// <summary>
        /// グレイマスのチェック結果を返すtrueなら全部グレー
        /// </summary>
        public bool CheckGrayMass(){
            _grayMassList = new GrayMassList();

            for(int i=0 ;i<mass.Length;i++){
                if(mass[i].Color == MassColor.Neu)    _grayMassList.AddGray(i);
                else                                  _grayMassList.Add(i);
            }

            return _grayMassList.IsALLGray;
        }
    }
}
