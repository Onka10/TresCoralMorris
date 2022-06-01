using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TresCoralMorris.StoneData;

namespace TresCoralMorris{
    public class StoneManager : MonoBehaviour
    {
        [SerializeField] GameObject PlayerBStoneParent;
        [SerializeField] GameObject PlayerWStoneParent;

        private Stone[] _BStones = new Stone[7];
        private Stone[] _WStones = new Stone[7];

        private IStone[] _BIStones = new IStone[7];
        private IStone[] _WIStones = new IStone[7];

        private Transform[] _BStonePos = new Transform[7];
        private Transform[] _WStonePos = new Transform[7];


        private void Start(){
            //キャッシュ
            for(int i=0 ;i<_BStones.Length;i++){
                _BStones[i] = PlayerBStoneParent.transform.GetChild(i).gameObject.GetComponent<Stone>();
                _WStones[i] = PlayerWStoneParent.transform.GetChild(i).gameObject.GetComponent<Stone>();

                _BIStones[i] = PlayerBStoneParent.transform.GetChild(i).gameObject.GetComponent<IStone>();
                _WIStones[i] = PlayerWStoneParent.transform.GetChild(i).gameObject.GetComponent<IStone>();

                _BStonePos[i] = PlayerBStoneParent.transform.GetChild(i).gameObject.GetComponent<Transform>();
                _WStonePos[i] = PlayerWStoneParent.transform.GetChild(i).gameObject.GetComponent<Transform>();
            }

            //石の初期化
            for(int i=0;i<7;i++){
                _BStones[i].Init(i,PlayerColor.Black);
                _WStones[i].Init(i,PlayerColor.White);
            }
        }

        public IStone GetStone(PlayerColor color,int id){
            if(color == PlayerColor.Black)  return _BIStones[id];
            else return  _WIStones[id];
        }

        /// <summary>
        /// StoneのView
        /// </summary>
        public void MoveStone(PlayerColor color,int stone_id,int mass_id){
            //移動先の座標
            Vector3 movedpotion = GamePositionData.GetMassPos(mass_id);

            if(color == PlayerColor.Black)  _BStonePos[stone_id].position =movedpotion;
            else  _WStonePos[stone_id].position =movedpotion;
        }


        public void MillMoveStone(PlayerColor color,int stone_id){
            //移動先の座標
            //FIXME
            Vector3 movedpotion = GamePositionData.GetMillPos();

            if(color == PlayerColor.Black)  _BStonePos[stone_id].position =movedpotion;
            else  _WStonePos[stone_id].position =movedpotion;
        }

    }
}
