using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TresCoralMorris{
    /// <summary>
    /// マスに石があるか、どの石があるかしか知らない
    /// </summary>
    public class StoneInMass : MonoBehaviour
    {
        public class StoneData{

            public bool IsInStone  => stoneStats;
            private bool stoneStats;
            public IStone Data => data;
            private IStone data;

            public StoneData(){
                stoneStats = false;
            }

            public void SetStone(IStone stone){
                stoneStats = true;
                data = stone;
            }

            public void DeleteStone(){
                stoneStats = false;
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
                // Debug.Log(_stoneInMass[i].INStone);
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
        public void DeleteStone(int stoneID,int massID){
            //管理
            _stoneInMass[massID].DeleteStone();
            _stoneManager.MillMoveStone(Turn.I.TurnColor.Value,stoneID);
        }

        public bool IsInStone(int massID){
            return _stoneInMass[massID].IsInStone;
        }

        /// <summary>
        /// ミル確認
        /// </summary>
        public MillResult CheckMill(IMass afterMass){
            PlayerColor turnColor = Turn.I.TurnColor.Value;
            PlayerColor stoneColor1;
            PlayerColor stoneColor2;
            PlayerColor stoneColor3;

            //返すやつ
            MillResult result = new MillResult();

            //もしヒットすれば代入
            Point();
            Lane();
            return result;

            #region ミルの横判定
            void Point(){
                //列のミルを全てチェック
                int point = afterMass.Lane * 6;

                for(int i=0;i < 3;i++){
                    if(!_stoneInMass[point + 2*i].IsInStone) return;
                    stoneColor1 = _stoneInMass[point + 2*i].Data.Color.Value;//0,2,4

                    if(!_stoneInMass[point+1 + 2*i].IsInStone) return;
                    stoneColor2 = _stoneInMass[point+1 + 2*i].Data.Color.Value;//1,3,5

                    if(!_stoneInMass[point+2 + 2*i].IsInStone) return;
                    stoneColor3 = _stoneInMass[point+2 + 2*i].Data.Color.Value;//2,4,0

                    if(stoneColor1==turnColor && stoneColor2==turnColor && stoneColor3==turnColor){
                        MakeResult(_stoneInMass[point + 2*i].Data,_stoneInMass[point+1 + 2*i].Data,_stoneInMass[point+2 + 2*i].Data);
                    }
                }
            }

            #endregion

            #region ミルの横判定
            void Lane(){
                //行のミルを全てチェック
                int lane = afterMass.Point;

                if(!_stoneInMass[lane].IsInStone) return;
                stoneColor1 = _stoneInMass[lane].Data.Color.Value;
                if(!_stoneInMass[lane+6].IsInStone) return;
                stoneColor2 = _stoneInMass[lane+6].Data.Color.Value;
                if(!_stoneInMass[lane+12].IsInStone) return;
                stoneColor3 = _stoneInMass[lane+12].Data.Color.Value;
                if(stoneColor1==turnColor && stoneColor2==turnColor && stoneColor3==turnColor)    MakeResult(_stoneInMass[lane].Data,_stoneInMass[lane+6].Data,_stoneInMass[lane+12].Data);

                if(!_stoneInMass[lane+6].IsInStone) return;
                stoneColor1 = _stoneInMass[lane+6].Data.Color.Value;
                if(!_stoneInMass[lane+12].IsInStone) return;
                stoneColor2 = _stoneInMass[lane+12].Data.Color.Value;
                if(!_stoneInMass[lane+18].IsInStone) return;
                stoneColor3 = _stoneInMass[lane+18].Data.Color.Value;
                if(stoneColor1==turnColor && stoneColor2==turnColor && stoneColor3==turnColor)    MakeResult(_stoneInMass[lane+6].Data,_stoneInMass[lane+12].Data,_stoneInMass[lane+18].Data);
                
            }
            #endregion

            void MakeResult(IStone m0,IStone m1,IStone m2){
                result.SetMill(m0,m1,m2);
            }
        }

    }
}

