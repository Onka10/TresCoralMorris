using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using TresCoralMorris.MassData;

namespace TresCoralMorris{
    /// <summary>
    /// マス全体に何かする時、データの入力処理と
    /// </summary>
    public class MassManager : MonoBehaviour
    {
        public GameObject massParent;

        private Mass[] mass = new Mass[24];


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
                // SetColorMesh(index);
            }
        }

        /// <summary>
        /// 指定したidのマスを中立化
        /// </summary>
        public void NeutralizationMassColor(int id){
            mass[id].SetColor(MassColor.Neu);
            // SetColorMesh(id);
        }

        /// <summary>
        /// グレイのチェック結果を返す
        /// </summary>
        public CheckGrayMass GetGrayMass(){
            //grayの数を確認
            List<int> grays = new List<int>();

            for(int i=0 ;i<mass.Length;i++){
                if(mass[i].Color == MassColor.Neu)    grays.Add(i);
            }

            //TODOCountが0で良いの？nullじゃ無くて？そこの確認がまだ
            //checkクラスを生成して返す
            if(grays.Count==0)  return new CheckGrayMass();
            else return new CheckGrayMass(grays.ToArray());
        }
    }
}
