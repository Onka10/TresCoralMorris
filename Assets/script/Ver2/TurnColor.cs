using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TresCoralMorris;
using DG.Tweening;

namespace TresCoralMorris.Phase1.UI{
    public class TurnColor : MonoBehaviour
    {
        public Image _turnColor;

        // Start is called before the first frame update
        void Start()
        {
            Turn.I.TurnColor
            .Subscribe(c => ChangeTurnColor(c))
            .AddTo(this);
        }

        private void ChangeTurnColor(PlayerColor c){
            Color color= new Color(0.2f, 0.2f, 0.2f);
            if(c==PlayerColor.Black)        color = new Color(0.2f, 0.2f, 0.2f);//黒
            else if(c==PlayerColor.White)    color = new Color(0.7f, 0.7f, 0.7f);//白
            
            _turnColor.DOColor(color,1f);
        }


    }
}
