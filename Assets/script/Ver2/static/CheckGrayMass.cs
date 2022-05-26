using System;
public class CheckGrayMass
{
    public CheckGrayMass(int[] grayArray){
        IsGrayMass = true;
        Array.Copy(grayArray,GrayArray,grayArray.Length);
    }

    public CheckGrayMass(){
        IsGrayMass = false;
    }

    /// <summary>
    /// 中立では無いマスのidを返す
    /// </summary>
    public int GetRandomGrayMass(){
        if(!IsGrayMass) throw new ArgumentException("中立マスが無いのに呼ばれています");

        int id;
        return id = (int)UnityEngine.Random.Range(0f, GrayArray.Length);
    }

    public bool IsGrayMass {get;}
    public int[] GrayArray = new int[0];
}
