using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace TresCoralMorris
{
    public class Mass : MonoBehaviour,IMass
    {
        public IReadOnlyReactiveProperty<int> ID => _id;
        private readonly ReactiveProperty<int> _id = new ReactiveProperty<int>();

        public int Lane {
            get{return _lane;}
        }
        [SerializeField] private int _lane;
        [SerializeField] private int _point;

        public IReadOnlyReactiveProperty<MassColor> Color => _color;
        private readonly ReactiveProperty<MassColor> _color = new ReactiveProperty<MassColor>();

        [SerializeField] private MassColor SerializeColor;
        
        public int[] MovebaleMass
        {
            get { return MovebaleMass; }
        }
        [SerializeField] private int[] _movebaleMass = new int[1];


        void Start(){
            _color
            .Subscribe(c => SerializeColor = c)
            .AddTo(this);
        }

        public void Init(int i){
            //idをセット
            _id.Value = i;
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

            Queue<int> queue = new Queue <int>();
            //_laneが端でないなら隣の列にいける
            if(_lane!=0)    queue.Enqueue(_id.Value-6);
            if(_lane!=3)    queue.Enqueue(_id.Value+6);
            //マスのポイントが端でないなら横に移動出来る
            if(_point!=0)   queue.Enqueue(_id.Value--);
            else            queue.Enqueue(_id.Value+5);
            if(_point!=5)   queue.Enqueue(_id.Value++);
            else            queue.Enqueue(_id.Value-5);
            _movebaleMass = queue.ToArray();
        }

        #endregion
    }

}