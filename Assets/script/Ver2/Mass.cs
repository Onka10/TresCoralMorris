using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace TresCoralMorris
{
    public class Mass : MonoBehaviour,IMass
    {
        public int ID{
            get
            {
                return _id;
            }
        }

        [SerializeField] private int _id;

        public int Lane {
            get{return _lane;}
        }
        [SerializeField] private int _lane;

        public int Point {
            get{return _point;}
        }
        [SerializeField] private int _point;

        public IReadOnlyReactiveProperty<MassColor> Color => _color;
        private readonly ReactiveProperty<MassColor> _color = new ReactiveProperty<MassColor>();

        [SerializeField] private MassColor SerializeColor;
        
        public int[] MovebaleMass
        {
            get { return _movebaleMass; }
        }
        [SerializeField] private int[] _movebaleMass = new int[1];


        void Start(){
            _color
            .Subscribe(c => SerializeColor = c)
            .AddTo(this);
        }

        public void Init(int i){
            //idをセット
            _id = i;
            // Debug.Log(_id);
            //レーンをセット
            _lane = i/6;
            //ポイントをセット
            _point = i % 6;
            //移動可能マスを確認
            CheckMovablemass();
        }

        public void SetColor(MassColor color){
            _color.Value = color;
        }


        #region  privateのメソッド
        private void CheckMovablemass(){
            //数バグってます

            int id=_id;
            Queue<int> queue = new Queue <int>();
            //_laneが端でないなら隣の列にいける
            if(_lane!=0)    queue.Enqueue(id-6);
            if(_lane!=3)    queue.Enqueue(id+6);
            //マスのポイントが端でないなら横に移動出来る
            if(_point!=0)   queue.Enqueue(id--);
            else            queue.Enqueue(id+5);
            if(_point!=5)   queue.Enqueue(id++);
            else            queue.Enqueue(id-5);
            _movebaleMass = queue.ToArray();
        }

        #endregion
    }

}