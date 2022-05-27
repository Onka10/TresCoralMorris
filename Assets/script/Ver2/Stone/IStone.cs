using UniRx;
public interface IStone
{
    public IReadOnlyReactiveProperty<int> ID{get;}
    public IReadOnlyReactiveProperty<PlayerColor> Color{get;}
} 