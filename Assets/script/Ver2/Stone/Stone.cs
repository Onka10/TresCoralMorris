using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace TresCoralMorris.StoneData
{
    public class Stone : MonoBehaviour,IStone
    {
            public IReadOnlyReactiveProperty<int> ID => _id;
            private readonly ReactiveProperty<int> _id = new ReactiveProperty<int>();

            public IReadOnlyReactiveProperty<PlayerColor> Color => _color;
            private readonly ReactiveProperty<PlayerColor> _color = new ReactiveProperty<PlayerColor>();

            public void Init(int id,PlayerColor playerColor){
                //idをセット
                _id.Value = id;

                //色をセット
                _color.Value = playerColor;
            }
    }
}
