using System;
using System.Collections.Generic;
public class GrayMassList
{   
    public bool IsALLGray => isAllGray;
    private bool isAllGray;
    public List<int> GrayArray = new List<int>();
    public List<int> NotGrayArray = new List<int>();

    public GrayMassList(){
        isAllGray = false;
    }

    public void AddGray(int i){
        GrayArray.Add(i);
        if(GrayArray.Count==24) isAllGray = true;
    }

    public void Add(int i){
        NotGrayArray.Add(i);
    }
}
