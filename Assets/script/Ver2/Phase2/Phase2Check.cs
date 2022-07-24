using UnityEngine;
using UniRx;
using System;

public class Phase2Check
{
    /// <summary>
    /// trueならセーフ
    /// </summary>
    public static bool Check21A(PlayerColor stoneColor){
        //クリックした石がプレイヤー色か確認
        if(stoneColor == Turn.I.TurnColor.Value)   return true;
        SEManager.I.Cancel();
        Debug.Log("クリックした石がプレイヤー色ではありません");

        return false;
    }

    /// <summary>
    /// trueならセーフ。クリックしたマスがマイカラーと同じ&&マスがグレーならセーフ
    /// </summary>
    public static bool Check21B(MassColor massColor){
        if(MyColorManager.I.CheckMyColor(massColor) && massColor == MassColor.Neu)    return true;
        SEManager.I.Cancel();        
        Debug.Log("クリックしたマスがマイカラーかグレーではありません");

        return false;
    }

    /// <summary>
    /// trueならセーフ。もし選んだマスのMovableの中に今選んだマスのidがあれば移動可能
    /// </summary>
    public static bool Check22A(IMass before,int massID){
        if(before.MovableCheck(massID)) return true;
        SEManager.I.Cancel();
        Debug.Log("Movableなマスではありません");
        return false;
    }

    /// <summary>
    /// trueならセーフ。クリックしたマスが移動可能な色&&グレーか確認
    /// </summary>
    public static bool Check22B(MassColor massColor){
        if(MyColorManager.I.CheckMovableColor(massColor) && massColor == MassColor.Neu) return true;
        SEManager.I.Cancel();
        Debug.Log("MovableColorなマスではありません");
        return false;
    }
}
