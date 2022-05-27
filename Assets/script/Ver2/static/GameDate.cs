using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace TresCoralMorris{
    public class GameDate : MonoBehaviour
    {
        public GameObject[] mass = new GameObject[24];
        public Mass[] imass = new Mass[24];
        public GameObject[] Bstone = new GameObject[7];
        public GameObject[] Wstone = new GameObject[7];

        public PlayerColor[] massinstone = new PlayerColor[24];

        private Vector3 _millPosition;

        //フェーズ2以降
        public IReactiveProperty<MassColor> MyColorB  => _myColorB;
        private readonly ReactiveProperty<MassColor> _myColorB = new ReactiveProperty<MassColor>();
        public IReactiveProperty<MassColor> MovebaleColorB1  => _moveB1;
        private readonly ReactiveProperty<MassColor> _moveB1 = new ReactiveProperty<MassColor>();
        public IReactiveProperty<MassColor> MovebaleColorB2  => _moveB2;
        private readonly ReactiveProperty<MassColor> _moveB2 = new ReactiveProperty<MassColor>();

        public IReactiveProperty<MassColor> MyColorW  => _myColorW;
        private readonly ReactiveProperty<MassColor> _myColorW = new ReactiveProperty<MassColor>();
        public IReactiveProperty<MassColor> MovebaleColorW1  => _moveW1;
        private readonly ReactiveProperty<MassColor> _moveW1 = new ReactiveProperty<MassColor>();
        public IReactiveProperty<MassColor> MovebaleColorW2  => _moveW2;
        private readonly ReactiveProperty<MassColor> _moveW2 = new ReactiveProperty<MassColor>();


        public void InitGameDate(){

            for(int i=0;i<7;i++){
                //石の初期化
                Bstone[i].GetComponent<Stone>().Init(i,PlayerColor.Black);
                Wstone[i].GetComponent<Stone>().Init(i,PlayerColor.White);
            }

            //色を決定
            // InitColor();

            //絶対よくないけどとりあえず初期化
            _millPosition = new Vector3(0,0,0);
        }


        // private void InitColor(){

        //     //色の初期化の処理で使う
        //     int[] counter = new int[3];

        //     for(int index=0;index<24;index++){
        //         int colorNum = Random.Range(0,3);
        //         while(counter[colorNum]== 8){
        //             colorNum=(colorNum+1)%3;
        //         }
        //         counter[colorNum]++;


        //         switch(colorNum) {
        //             case 0:
        //                 mass[index].GetComponent<IMass>().SetColor(MassColor.Red);
        //                 break;
        //             case 1:
        //                 mass[index].GetComponent<IMass>().SetColor(MassColor.Green);
        //                 break;
        //             case 2:
        //                 mass[index].GetComponent<IMass>().SetColor(MassColor.Blue);
        //                 break;
        //         }
        //     }
        // }

        public void SetStone(PlayerColor playerColor,int massid, int stoneid){
            //データ
            massinstone[massid] = playerColor;

            //見た目
            Vector3 movedpotion = mass[massid].GetComponent<Transform>().position;
            movedpotion.y = 1.4f;

            if(playerColor == PlayerColor.Black){
                Bstone[stoneid].GetComponent<Transform>().position = movedpotion;
            }else if(playerColor == PlayerColor.White){
                Wstone[stoneid].GetComponent<Transform>().position = movedpotion;
            }
        }

        //フェーズ2の交換がある場合のオーバーロード
        public void SetStone(PlayerColor playerColor,int beforeMassid,int afterMassid, int stoneid){
            //データ
            massinstone[beforeMassid] = PlayerColor.Empty;
            massinstone[afterMassid] = playerColor;

            //見た目
            Vector3 movedpotion = mass[afterMassid].GetComponent<Transform>().position;
            movedpotion.y = 1.4f;

            if(playerColor == PlayerColor.Black){
                Bstone[stoneid].GetComponent<Transform>().position = movedpotion;
            }else if(playerColor == PlayerColor.White){
                Wstone[stoneid].GetComponent<Transform>().position = movedpotion;
            }
        }

        //コマを消す
        public void DeleteStone(PlayerColor playerColor,int stoneid){
            if(playerColor == PlayerColor.Black){
                Bstone[stoneid].GetComponent<Transform>().position = _millPosition;
            }else if(playerColor == PlayerColor.White){
                Wstone[stoneid].GetComponent<Transform>().position = _millPosition;
            }
        }

        public bool CheckMovable(IMass mass){
            //
            return true;
        }

        //ミルチェック
        public bool MillCheck(PlayerColor turnColor,IMass aftermass){

            #region ミルの横判定
            //配列の実際の値に変換
            int point= 6 * aftermass.Lane;

            //ミルの横の判定はmassのpointが０１２・２３４・４５０の時のみ
            if(massinstone[point]==turnColor && massinstone[point+1]==turnColor && massinstone[point+2]==turnColor)    return true;
            if(massinstone[point+2]==turnColor && massinstone[point+3]==turnColor && massinstone[point+4]==turnColor)    return true;
            if(massinstone[point+4]==turnColor && massinstone[point+5]==turnColor && massinstone[point]==turnColor)    return true;
            #endregion

            #region ミルの縦判定
            int lane= 6 * aftermass.Point;

            //ミルの縦の判定はmassのLaneが012,123の時のみ
            if(massinstone[lane]==turnColor && massinstone[lane+6]==turnColor && massinstone[lane+12]==turnColor)    return true;
            if(massinstone[lane+6]==turnColor && massinstone[lane+12]==turnColor && massinstone[lane+18]==turnColor)    return true;
            #endregion

            return false;
        }

        //そのマスが、指定されたプレイヤーのmovableカラーに含まれているかを確認する
        public bool CheckMobableColor(PlayerColor playerColor,IMass checkmass){

            //movableカラーか中立ならセーフ
            if(playerColor==PlayerColor.Black){
                if(checkmass.Color.Value== MovebaleColorB1.Value || checkmass.Color.Value== MovebaleColorB2.Value)     return true;
                if(checkmass.Color.Value==MassColor.Neu)         return true;
            }else if(playerColor==PlayerColor.White){
                if(checkmass.Color.Value== MovebaleColorW1.Value || checkmass.Color.Value== MovebaleColorW2.Value)     return true;
                if(checkmass.Color.Value==MassColor.Neu)         return true;
            }
            return false;
        }

        public bool StoneCanMove(PlayerColor turncolor,IMass mass){
            MassColor mycolor;

            if(turncolor==PlayerColor.Black)  mycolor = MyColorB.Value;
            else    mycolor= MyColorW.Value;

            if(mass.Color.Value == mycolor || mass.Color.Value == MassColor.Neu)      return true;
            return false;
        }

        public void CollateMovableColor(){
            // if()
        }
    }
}
