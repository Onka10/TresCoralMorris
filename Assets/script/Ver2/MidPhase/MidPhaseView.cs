using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace TresCoralMorris{
    public class MidPhaseView : MonoBehaviour
    {
        [SerializeField] Image _b;
        [SerializeField] Image _w;

        [SerializeField] MidPhaseManager _midPhaseManager;
        [SerializeField] GameObject UI;

        // Start is called before the first frame update
        void Start()
        {
            GameManager.I.Phase
            .Where(p => p ==GamePhase.MidPhase)
            .Subscribe(_ => Init())
            .AddTo(this);

            GameManager.I.Phase
            .Where(p => p ==GamePhase.Phase2)
            .Subscribe(_ => Destroy(this.gameObject))
            .AddTo(this);
        }

        private void Init(){
            UI.SetActive(true);

            _midPhaseManager.OnReloadView
            .Subscribe(_ => hyouji())
            .AddTo(this);
        }

        private void hyouji(){
            _b.color = ColorChanger.MassColorToColor(_midPhaseManager.B);
            _w.color = ColorChanger.MassColorToColor(_midPhaseManager.W);
        }
    }
}

