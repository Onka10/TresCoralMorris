using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Turn : MonoBehaviour
{
        public IReactiveProperty<PlayerColor> TurnColor  => _turnColor;
        private readonly ReactiveProperty<PlayerColor> _turnColor = new ReactiveProperty<PlayerColor>();
}
