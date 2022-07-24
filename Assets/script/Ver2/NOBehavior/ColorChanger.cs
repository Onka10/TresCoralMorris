using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アイコンのデータ（読み取り専用）

public class ColorChanger
{
    static private Dictionary<MassColor, Color> MassColorDictionary = new Dictionary<MassColor, Color>(){
            {MassColor.Red,Color.red},
            {MassColor.Blue,Color.blue},
            {MassColor.Green,Color.green},   
    };

    static private Dictionary<PlayerColor, Color> PlayerColorDictionary = new Dictionary<PlayerColor, Color>(){
            {PlayerColor.Black ,Color.black},
            {PlayerColor.White ,Color.white},
    };

    /// <summary>
    /// MassColorをColorに変換
    /// </summary>
    public static Color MassColorToColor(MassColor c)
    {
        return MassColorDictionary[c];
    }

    /// <summary>
    /// MassColorをColorに変換
    /// </summary>
    public static Color PlayerColorToColor(PlayerColor c)
    {
        return PlayerColorDictionary[c];
    }
}