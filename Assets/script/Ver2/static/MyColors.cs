
public class MyColors
{
    //コンストラクタ
    public MyColors(MassColor mycolor,MassColor move1,MassColor move2){
        MyColor = mycolor;
        MoveableColor1 = move1;
        MoveableColor2 = move2;
    }

    //MyColor
    public MassColor MyColor {get;}
    public MassColor MoveableColor1 {get;}
    public MassColor MoveableColor2 {get;}
}