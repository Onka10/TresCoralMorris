using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class debug_p2log : Singleton<debug_p2log>
{
    [SerializeField] Text objectText;
    [SerializeField] Image TurnIma;
    [SerializeField] Text PhaseText;

    private void start(){
        Turn.I.TurnColor
        .Subscribe(_ => DebugLogTurn())
        .AddTo(this);
    }

    public void DebugLogObj<T>(T s){
        objectText.text = s.ToString();
    }


    public void DebugLogTurn(){
        TurnIma.color = ColorChanger.PlayerColorToColor(Turn.I.TurnColor.Value);
    }

    public void DebugLogPhase(string phase){
        PhaseText.text = phase;
    }
}
