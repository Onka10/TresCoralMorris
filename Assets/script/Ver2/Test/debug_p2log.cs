using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class debug_p2log : Singleton<debug_p2log>
{
    [SerializeField] Text objectText;
    [SerializeField] Text TurnText;
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
        TurnText.text = Turn.I.TurnColor.Value.ToString();
    }

    public void DebugLogPhase(int phase){
        PhaseText.text = phase.ToString();
    }
}
