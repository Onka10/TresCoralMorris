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
        public IReadOnlyReactiveProperty<MassColor> Color => _color;
        private readonly ReactiveProperty<MassColor> _color = new ReactiveProperty<MassColor>();
        [SerializeField] private MassColor SerializeColor;
        
        //移動可能マス
        public int[] MovebaleMass => _movebaleMass;
        [SerializeField] private int[] _movebaleMass = new int[1];
        #endregion


        void Start(){
            _color
            .Subscribe(c => SerializeColor = c)
            .AddTo(this);
        }


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
            SetMovablemass();

            void SetMovablemass(){
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

                _movebaleMass = queue.ToArray();
            }
        }

        public void SetColor(MassColor color){
            _color.Value = color;
        }

        //movableの中に指定されたマスが存在するかを確認する
        public bool MobableCheck(int id){
            for(int i=0;i< _movebaleMass.Length;i++){
                if(_movebaleMass[i] == id)   return true;
            }

            return false;
        }
    }

}