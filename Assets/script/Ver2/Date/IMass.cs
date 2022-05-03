using UniRx;
public interface IMass
{
    // public IReadOnlyReactiveProperty<int> ID{get;}
    public int ID{get;}
    public int Lane{get;}
    public int Point{get;}
    public IReadOnlyReactiveProperty<MassColor> Color{get;}
    public int[] MovebaleMass{get;}

    void Init(int i);
    void SetColor(MassColor color);
    bool MobableCheck(int id);
} 