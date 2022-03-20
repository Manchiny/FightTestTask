public abstract class UnitState 
{
    protected Unit _unit;

    public bool IsFinished { get; protected set; }
    public virtual void Init(Unit unit)
    {
        _unit = unit;
        IsFinished = false;
    }
    public abstract void OnUpdate();

    public virtual void Exit()
    {
        IsFinished = true;
    }
}
