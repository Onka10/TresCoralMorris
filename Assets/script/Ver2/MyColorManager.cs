using System;

public class MyColorManager : Singleton<MyColorManager>
{
    public MyColors BPlayerMyColors => bPlayerMyColor;
    private MyColors bPlayerMyColor;

    public MyColors WPlayerMyColors => wPlayerMyColor;
    private MyColors wPlayerMyColor;

    public void SetMyColorForB(MyColors myColors){
        bPlayerMyColor = myColors;
    }

    public void SetMyColorForW(MyColors myColors){
        wPlayerMyColor = myColors;
    }

    /// <summary>
    /// 与えられたMassColorと今のMyColorのムーバブルに入ってるかを確認
    /// </summary>
    public bool CheckMovableColor(MassColor CheckMassColor){
        MyColors mycolor;
        if(Turn.I.TurnColor.Value == PlayerColor.Black) mycolor = bPlayerMyColor;
        else mycolor = wPlayerMyColor;

        //movableカラーか中立ならセーフ
        if(mycolor.MoveableColor1 == CheckMassColor || mycolor.MoveableColor2 == CheckMassColor)     return true;
        if(CheckMassColor == MassColor.Neu)         return true;

        return false;
    }

    /// <summary>
    /// 与えられたMassColorと今のMyColorが同じかを確認。同じならtrue
    /// </summary>
    public bool CheckMyColor(MassColor checkMassColor){
        MyColors mycolor;
        if(Turn.I.TurnColor.Value == PlayerColor.Black) mycolor = bPlayerMyColor;
        else mycolor = wPlayerMyColor;

        if(mycolor.MyColor == checkMassColor)   return true;
        else return false;
    }

    /// <summary>
    /// ボタンで色を変える
    /// </summary>
    public void ChangeMyColor(int c){
        MassColor mycolor =  (MassColor)Enum.ToObject(typeof(MassColor), c);

        if(Turn.I.TurnColor.Value == PlayerColor.Black)    bPlayerMyColor = new MyColors(mycolor,wPlayerMyColor.MyColor); 
        else if(Turn.I.TurnColor.Value == PlayerColor.White)    wPlayerMyColor = new MyColors(mycolor,bPlayerMyColor.MyColor);
    }
}
