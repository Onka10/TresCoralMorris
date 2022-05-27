using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace TresCoralMorris{
    public class MassManager : MonoBehaviour
    {
        public GameObject massParent;

        private IMass[] mass = new IMass[24];
        private MeshRenderer[] massRenderer = new MeshRenderer[24];

        private void Start(){
            //キャッシュ
            for(int i=0 ;i<mass.Length;i++){
                mass[i] = massParent.transform.GetChild(i).gameObject.GetComponent<IMass>();
                massRenderer[i] = massParent.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
            }
        }

        public void InitColor(){
            int[] counter = new int[3];

            for(int index=0;index<massRenderer.Length;index++){
                int colorNum = Random.Range(0,3);
                while(counter[colorNum]== 8){
                    colorNum=(colorNum+1)%3;
                }
                counter[colorNum]++;

                //+1した値がMassColorに相当
                //変更
                MassColor massColor = (MassColor)colorNum+1;
                mass[index].SetColor(massColor);
                SetColorMesh(index);
            }
        }

        public void InitData(){
            for(int i=0;i<mass.Length;i++){
                mass[i].Init(i);
            }
        }

        /// <summary>
        /// 指定したidのマスのMassColorを入手
        /// </summary>
        public MassColor GetMassColor(int id){
            return mass[id].Color.Value;
        }
        
        /// <summary>
        /// 指定したidのマスを中立化
        /// </summary>
        public void NeutralizationMassColor(int id){
            mass[id].SetColor(MassColor.Neu);
            SetColorMesh(id);
        }

        /// <summary>
        /// グレイのチェック結果を返す
        /// </summary>
        public CheckGrayMass GetGrayMass(){
            //grayの数を確認
            List<int> grays = new List<int>();

            for(int i=0 ;i<mass.Length;i++){
                if(mass[i].Color.Value == MassColor.Neu)    grays.Add(i);
            }

            //TODOCountが0で良いの？nullじゃ無くて？そこの確認がまだ
            //checkクラスを生成して返す
            if(grays.Count==0)  return new CheckGrayMass();
            else return new CheckGrayMass(grays.ToArray());
        }

        /// <summary>
        /// MassのView
        /// </summary>
        private void SetColorMesh(int id){
            var massColor = mass[id].Color.Value;
            var color = ColorChanger.MassColorToColor(massColor);

            //みための変更
            massRenderer[id].material.DOColor(color,2f);
        }
    }
}
