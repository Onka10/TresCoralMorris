using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace TresCoralMorris.MassData
{
    public class Mass : MonoBehaviour,IMass
    {
        #region プロパティ
        //ID
        public int ID => _id;
        [SerializeField] private int _id;

        //レーン
        public int Lane => _lane;
        [SerializeField] private int _lane;

        //ポイント
        public int Point => _point;
        [SerializeField] private int _point;

        //色
        public MassColor Color => _color;
        [SerializeField]private MassColor _color;

        
        //移動可能マス
        public int[] MovableMass => _movableMass;
        [SerializeField] private int[] _movableMass = new int[1];
        #endregion

        private MassView _massView;


        //初期化変数
        public void Init(int i){
            //idをセット
            _id = i;
            // Debug.Log(_id);
            //レーンをセット
            _lane = i/6;
            //ポイントをセット
            _point = i % 6;


            //移動可能マスを確認
            SetMovableMass();

            _massView = this.gameObject.GetComponent<MassView>();
            _massView.Init();

            void SetMovableMass(){
                int id=_id;
                Queue<int> queue = new Queue <int>();

                //_laneが端でないなら隣の列にいける
                if(_lane!=0)    queue.Enqueue(id-6);
                if(_lane!=3)    queue.Enqueue(id+6);

                //マスのポイントが端でないなら横に移動出来る
                if(_point!=0)   queue.Enqueue(id-1);
                else            queue.Enqueue(id+5);

                if(_point!=5)   queue.Enqueue(id+1);
                else            queue.Enqueue(id-5);

                _movableMass = queue.ToArray();
            }
        }

        public void SetColor(MassColor color){
            _color = color;
            _massView.SetColorMesh(color);
        }

        //movableの中に指定されたマスが存在するかを確認する
        public bool MovableCheck(int id){
            for(int i=0;i< _movableMass.Length;i++){
                // Debug.Log(_movableMass[i] +"b:a"+ id);
                if(_movableMass[i] == id)   return true;
            }

            return false;
        }
    }

}