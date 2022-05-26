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


        // public GameObject massParent;
        // private MeshRenderer[] mass = new MeshRenderer[24];
        // [SerializeField] MassManager _massManager;

        // private void Start(){
        //     for(int i=0 ;i<mass.Length;i++){
        //         mass[i] = massParent.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
        //     }

        //     _massManager.Mass
        //     .ObserveReplace()
        //     .Subscribe(_ => Debug.Log("koi"))
        //     .AddTo(this);
            
        // }

        // public void InitColor(){
        //     for(int i=0;i<24;i++){
        //         SetColorMesh(i);
        //     }
        // }

        // private void SetColorMesh(int id){
        //     var massColor = _massManager.GetMassColor(id);
        //     var color = ColorChanger.MassColorToColor(massColor);

        //     //みための変更
        //     mass[id].material.DOColor(color,2f);
        // }

    }
}
