using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TresCoralMorris{
    /// <summary>
    /// マスに石があるか、どの石があるかしか知らない
    /// </summary>
    public class StoneInMass : MonoBehaviour
    {
        public enum StoneStats{
            Empty,
            IN
        }
        public class StoneData{

            public StoneStats INStone  => stoneStats;
            private StoneStats stoneStats;
            private IStone data;

            public StoneData(){
                stoneStats = StoneStats.Empty;
            }

            public void SetStone(IStone stone){
                stoneStats = StoneStats.IN;
                data = stone;
            }

            public void DeleteStone(){
                stoneStats = StoneStats.Empty;
                data = null;
            }
        }

        private StoneData[] _stoneInMass = new StoneData[24];
        [SerializeField] StoneManager _stoneManager;
        [SerializeField] MassManager _massManager;


        void Start(){
            for(int i =0;i<_stoneInMass.Length;i++){
                _stoneInMass[i] = new StoneData();
            }
        }

        public void SetStone(PlayerColor pc,int massID,int stoneID){
            //石を入手
            IStone stone = _stoneManager.GetStone(pc,stoneID);

            //管理
            _stoneInMass[massID].SetStone(stone);
            _stoneManager.MoveStone(pc,stoneID,massID);
        }

        /// <summary>
        /// ミル
        /// </summary>
        public void DeleteStone(PlayerColor pc,int stoneID,int massID){
            //管理
            _stoneInMass[massID].DeleteStone();
            _stoneManager.MillMoveStone(pc,stoneID);
        }

        public bool IsInStone(int massID){
            if(_stoneInMass[massID].INStone == StoneStats.IN)   return true;
            else return false;
        }

        
    }
}

