using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アイコンのデータ（読み取り専用）

public class ColorChanger
{
    static private Dictionary<MassColor, Color> ColorDictionary = new Dictionary<MassColor, Color>(){
            {MassColor.Red,Color.red},
            {MassColor.Blue,Color.blue},
            {MassColor.Green,Color.green},   
    };

    /// <summary>
    /// MassColorをColorに変換
    /// </summary>
    public static Color MassColorToColor(MassColor c)
    {
        return ColorDictionary[c];
    }
}