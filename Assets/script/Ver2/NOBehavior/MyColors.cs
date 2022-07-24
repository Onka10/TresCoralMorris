using System;
public class MyColors
{
    //コンストラクタ
    public MyColors(MassColor myColor,MassColor enemyColor){
        MyColor = myColor;
        

        MassColor move1=MassColor.Neu;
        MassColor move2=MassColor.Neu;

        if(enemyColor==MassColor.Red){
            move1 = MassColor.Green;
            move2 = MassColor.Blue;
        }else if(enemyColor==MassColor.Green){
            move1 = MassColor.Red;
            move2 = MassColor.Blue;
        }else if(enemyColor==MassColor.Blue){
            move1 = MassColor.Red;
            move2 = MassColor.Green;
        }

        if(move1 == MassColor.Neu || move2 == MassColor.Neu)  throw new Exception("MyColor や EnemyColorが正しくありません");
        MoveableColor1 = move1;
        MoveableColor2 = move2;
    }

    //MyColor
    public MassColor MyColor {get;}
    public MassColor MoveableColor1 {get;}
    public MassColor MoveableColor2 {get;}
}