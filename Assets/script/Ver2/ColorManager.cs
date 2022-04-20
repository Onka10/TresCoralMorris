using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TresCoralMorris
{
    public class ColorManager : MonoBehaviour
    {
        public GameObject[] mass = new GameObject[24]; 

        // [SerializeField] TresCoralMorris.GameDate _gamedate;

        Renderer rendererComponent;

        public void InitColor(){
            for(int i=0;i<24;i++){
                SetColor(i);
            }
        }

        public void SetColor(int id){
            Color color = Color.black;
            switch(mass[id].GetComponent<IMass>().Color.Value) {
                case MassColor.Red:
                    color = Color.red;
                    break;
                case MassColor.Green:
                    color = Color.green;
                    break;
                case MassColor.Blue:
                    color = Color.blue;
                    break;
            }

            //みための変更
            mass[id].GetComponent<MeshRenderer>().material.DOColor(color,2f);
        }

    }
}
