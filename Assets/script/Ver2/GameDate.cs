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

            for(int i=0;i<24;i++){
                //マスの初期化
                    imass[i].Init(i);
                    //おいている石を空に
                    massinstone[i] = PlayerColor.Empty;
            }

            //色を決定
            InitColor();
        }


        private void InitColor(){

            //色の初期化の処理で使う
            int[] counter = new int[3];

            for(int index=0;index<24;index++){
                int colorNum = Random.Range(0,3);
                while(counter[colorNum]== 8){
                    colorNum=(colorNum+1)%3;
                }
                counter[colorNum]++;


                switch(colorNum) {
                    case 0:
                        mass[index].GetComponent<IMass>().SetColor(MassColor.Red);
                        break;
                    case 1:
                        mass[index].GetComponent<IMass>().SetColor(MassColor.Green);
                        break;
                    case 2:
                        mass[index].GetComponent<IMass>().SetColor(MassColor.Blue);
                        break;
                }
            }
        }

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
            
        }
    }
}
