using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Stone : MonoBehaviour,IStone
{
        public IReadOnlyReactiveProperty<int> ID => _id;
        private readonly ReactiveProperty<int> _id = new ReactiveProperty<int>();

        public IReadOnlyReactiveProperty<PlayerColor> Color => _color;
        private readonly ReactiveProperty<PlayerColor> _color = new ReactiveProperty<PlayerColor>();
}