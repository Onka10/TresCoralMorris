using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TresCoralMorris{
    public class StoneManager : MonoBehaviour
    {
        public GameObject PlayerBStoneParent;
        public GameObject PlayerWStoneParent;

        private IStone[] _BStones = new IStone[7];
        private IStone[] _WStones = new IStone[7];


        private void Start(){
            //キャッシュ
            for(int i=0 ;i<_BStones.Length;i++){
                _BStones[i] = PlayerBStoneParent.transform.GetChild(i).gameObject.GetComponent<IStone>();
                _BStones[i] = PlayerBStoneParent.transform.GetChild(i).gameObject.GetComponent<IStone>();
            }
        }

    }
}
