using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
