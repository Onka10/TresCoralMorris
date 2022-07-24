using System.Collections;
using System;
using UnityEngine;

namespace TresCoralMorris.UI{
    public class CCButton : MonoBehaviour
    {
        [SerializeField] MyColorManager _myColorManger;
        [SerializeField] Fhase2Manager _Phase2Mana;
        public void ColorChange(int c){
            MassColor mycolor =  (MassColor)Enum.ToObject(typeof(MassColor), c);
            _myColorManger.ChangeMyColor(mycolor);
            _Phase2Mana.ColorChange();
        }
    }

}
