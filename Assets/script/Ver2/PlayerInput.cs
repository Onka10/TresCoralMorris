using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace TresCoralMorris
{
    public class PlayerInput : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<IMass> GetMass => _massSubject;
        private readonly ReactiveProperty<IMass> _massSubject = new ReactiveProperty<IMass>();
        public IReadOnlyReactiveProperty<IStone> GetStone => _stoneSubject;
        private readonly ReactiveProperty<IStone> _stoneSubject = new ReactiveProperty<IStone>();

        public IObservable<Unit> Click => click;
        private readonly Subject<Unit> click = new Subject<Unit>();

        Vector3 touchScreenPosition;
        void Update(){
            if (Input.GetMouseButtonDown(0)) {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay (ray.origin, ray.direction * 100, Color.red, 5, false);

                //FIXMEすごく汚い
                foreach (RaycastHit hit in Physics.RaycastAll(ray)){
                    //フェーズ1
                    if(GameManager.I.Phase.Value == GamePhase.Phase1){
                        if( hit.collider.gameObject.TryGetComponent<IMass>(out IMass mass) ){
                            //マスの入手
                            _massSubject.Value = mass;

                            click.OnNext(Unit.Default);
                            //初期化
                            _massSubject.Value = null;
                        }
                    }


                    if(GameManager.I.Phase.Value == GamePhase.Phase2){
                        // Debug.Log("開始");
                        if( hit.collider.gameObject.TryGetComponent<IMass>(out IMass mass) ){
                            //マスの入手
                            // Debug.Log("mass入手");
                            _massSubject.Value = mass;
                        }
                        if( hit.collider.gameObject.TryGetComponent<IStone>(out IStone stone) ){
                            //石の入手
                            // Debug.Log("stone入手");
                            _stoneSubject.Value = stone;
                        }
                        Debug.Log("<color=red>Hit!</color>"+hit.collider.gameObject);
                    }
                }

                //forReachで抜けてから
                if(GameManager.I.Phase.Value == GamePhase.Phase2){
                    // Debug.Log("<color=blue>mass</color>"+_massSubject.Value);
                    // Debug.Log("<color=blue>stone!</color>"+_stoneSubject.Value);
                    click.OnNext(Unit.Default);
                    //Warning!!送ったあとに初期化
                    _massSubject.Value=null;
                    _stoneSubject.Value=null;
                }
            }
        }
    }


}