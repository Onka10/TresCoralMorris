using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Color MycolorB;

    public Color MycolorW;

    //汎用性上げるためにint
    //内訳　0=N;1=B;2=W;
    int[,] massdataWArray = new int[4, 6];

    //多重じゃない版
    public int[] massdataArray = new int[24];

    //マスの色データ
    public Color[] masscolorarray = new Color[24];

    //移動可能配列
    public List<int> movable = new List<int>(5);//上下左右で4方向。予備で1こ

    //PL1のsetableカラー
    public int BR=2;
    public int BG=2;
    public int BB=2;
    public int BA=1;

    public int WR=2;
    public int WG=2;
    public int WB=2;
    public int WA=1;

}
