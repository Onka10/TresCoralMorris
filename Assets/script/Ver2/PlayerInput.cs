using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace TresCoralMorris
{
    public class PlayerInput : MonoBehaviour
    {
        // public IObservable<Unit> GetMass => _masssubject;
        // private readonly Subject<Unit> _masssubject = new Subject<Unit>();

        public IReadOnlyReactiveProperty<IMass> GetMass => _masssubject;
        private readonly ReactiveProperty<IMass> _masssubject = new ReactiveProperty<IMass>();
        public IReadOnlyReactiveProperty<IStone> GetStone => _stoneSubject;
        private readonly ReactiveProperty<IStone> _stoneSubject = new ReactiveProperty<IStone>();

        public IObservable<Unit> Click => click;
        private readonly Subject<Unit> click = new Subject<Unit>();

        Vector3 touchScreenPosition;
        void Update(){
            if (Input.GetMouseButtonDown(0)) {
                // touchScreenPosition = Input.mousePosition;
                // Camera  gameCamera      = Camera.main;
                // Ray     touchPointToRay = gameCamera.ScreenPointToRay( touchScreenPosition );
                // RaycastHit hitInfo = new RaycastHit();

                // if( Physics.Raycast( touchPointToRay, out hitInfo ) ){
                //     if( hitInfo.collider.gameObject.TryGetComponent<IMass>(out IMass mass) ){
                //         Debug.Log("クリック"+mass.ID);
                //         _masssubject.Value = mass;
                        
                //         click.OnNext(Unit.Default);
                //     }
                // }


                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                foreach (RaycastHit hit in Physics.RaycastAll(ray)){
                    Debug.Log("クリック");
                    if( hit.collider.gameObject.TryGetComponent<IMass>(out IMass mass) ){
                        //マスの入手
                        _masssubject.Value = mass;
                        Debug.Log(mass);
                        
                        click.OnNext(Unit.Default);
                    }else
                    if( hit.collider.gameObject.TryGetComponent<IStone>(out IStone stone) ){
                        //石の入手
                        _stoneSubject.Value = stone;
                        Debug.Log(stone);

                        click.OnNext(Unit.Default);
                    }
                    
                }
            }
        }
    }
}