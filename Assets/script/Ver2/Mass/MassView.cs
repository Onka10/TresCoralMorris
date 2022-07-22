using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

namespace TresCoralMorris
{
    //そんなにマスは頻繁に変わらないのと、Reactiveな設計が難しいのでMangerに統合
    public class MassView : MonoBehaviour
    {
        MeshRenderer meshRenderer;

        public void Init(){
            meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        }

        /// <summary>
        /// MassのMesh変更
        /// </summary>
        public void SetColorMesh(MassColor massColor){
            var color = ColorChanger.MassColorToColor(massColor);

            //みための変更
            meshRenderer.material.DOColor(color,2f);
        }
    }
}
