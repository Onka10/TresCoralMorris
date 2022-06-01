using UniRx;

/// <summary>
/// 基本的にゲッタのみ
/// </summary>
public interface IMass
{
    public int ID{get;}
    public int Lane{get;}
    public int Point{get;}
    public IReadOnlyReactiveProperty<MassColor> Color{get;}
    public int[] MovebaleMass{get;}
    bool MobableCheck(int id);
} 