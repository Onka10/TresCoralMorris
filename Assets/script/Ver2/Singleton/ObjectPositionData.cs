using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPositionData : MonoBehaviour
{
        private static Vector3[] massPos = new Vector3[24];
        [SerializeField] GameObject massParent;

        private void Start(){
            //キャッシュ
            for(int i=0 ;i<massPos.Length;i++){
                massPos[i] = massParent.transform.GetChild(i).position;
            }
        }

        public static Vector3 GetMassPos(int id){
            if(massPos[id] == null) throw new  Exception("id見ろ");

            Vector3 pos = massPos[id];
            pos.y = 0.5f;

            return pos;
        }

        public static Vector3 GetMillPos(){
            Vector3 pos = new Vector3(0f,0f,0f);

            return pos;
        }
}
