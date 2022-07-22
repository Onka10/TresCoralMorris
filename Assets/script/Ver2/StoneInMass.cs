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
            public IStone Data => data;
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

        public class MillResult{
            public bool Result => result;
            private bool result;

            public IStone[] millCandidate = new IStone[3];

            public MillResult(){
                result = false;
            }

            public void SetMill(IStone m0,IStone m1,IStone m2){
                result = true;
                millCandidate[0] = m0;
                millCandidate[1] = m1;
                millCandidate[2] = m2;
            }

            //消せるならtrue
            public bool IsDelete(IStone stone){
                for(int i=0;i<3;i++){
                    if(millCandidate[i].ID == stone.ID) return true; 
                }
                return false;
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

        /// <summary>
        /// 石を置きます
        /// </summary>
        public void SetStone(PlayerColor pc,int massID,int stoneID){
            //石を入手
            IStone stone = _stoneManager.GetStone(pc,stoneID);

            //管理
            _stoneInMass[massID].SetStone(stone);
            _stoneManager.MoveStone(pc,stoneID,massID);
        }

        /// <summary>
        /// ミル移動用
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

        /// <summary>
        /// ミル確認
        /// </summary>
        public MillResult CheckMill(IMass afterMass){
            PlayerColor turnColor = Turn.I.TurnColor.Value;
            PlayerColor stoneColor1;
            PlayerColor stoneColor2;
            PlayerColor stoneColor3;

            MillResult result = new MillResult();

            #region ミルの横判定
            //列のミルを全てチェック
            int id = afterMass.Lane * 6;

            for(int i=0;i < 3;i++){
                stoneColor1 = _stoneInMass[id + 2*i].Data.Color.Value;//0,2,4
                stoneColor2 = _stoneInMass[id+1 + 2*i].Data.Color.Value;//1,3,5
                stoneColor3 = _stoneInMass[id+2 + 2*i].Data.Color.Value;//2,4,0

                if(stoneColor1==turnColor && stoneColor2==turnColor && stoneColor3==turnColor)    MakeResult(_stoneInMass[id + 2*i].Data,_stoneInMass[id+1 + 2*i].Data,_stoneInMass[id+2 + 2*i].Data);
            }
            #endregion

            #region ミルの横判定
            //行のミルを全てチェック
            int lane = afterMass.Point * 6;

            stoneColor1 = _stoneInMass[lane].Data.Color.Value;
            stoneColor2 = _stoneInMass[lane+6].Data.Color.Value;
            stoneColor3 = _stoneInMass[lane+12].Data.Color.Value;
            if(stoneColor1==turnColor && stoneColor2==turnColor && stoneColor3==turnColor)    MakeResult(_stoneInMass[lane].Data,_stoneInMass[lane+6].Data,_stoneInMass[lane+12].Data);

            stoneColor1 = _stoneInMass[lane+6].Data.Color.Value;
            stoneColor2 = _stoneInMass[lane+12].Data.Color.Value;
            stoneColor3 = _stoneInMass[lane+18].Data.Color.Value;
            if(stoneColor1==turnColor && stoneColor2==turnColor && stoneColor3==turnColor)    MakeResult(_stoneInMass[lane+6].Data,_stoneInMass[lane+12].Data,_stoneInMass[lane+18].Data);
            
            #endregion

            return result;

            void MakeResult(IStone m0,IStone m1,IStone m2){
                result.SetMill(m0,m1,m2);
            }
        }

    }
}

