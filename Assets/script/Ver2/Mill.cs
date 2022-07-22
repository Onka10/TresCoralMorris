using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : MonoBehaviour
{
    //FIXME ガバガバ
    public PlayerColor[] massinstone = new PlayerColor[24];
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
}
