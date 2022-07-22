using UniRx;

/// <summary>
/// 基本的にゲッタのみ
/// </summary>
public interface IMass
{
    public int ID{get;}
    public int Lane{get;}
    public int Point{get;}
    public MassColor Color{get;}
    public int[] MovableMass{get;}
    bool MovableCheck(int id);
} 