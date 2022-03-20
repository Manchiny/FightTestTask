public class UnitMoveToTargetState : UnitState
{
    public override void OnUpdate()
    {
        if (IsFinished)
            return;
        if (_unit.EnemyTarget == null || _unit.IsEnoughDistance(_unit.EnemyTarget.transform.position) == true)
        {
            Exit();
            return;
        }
        _unit.MoveTo(_unit.EnemyTarget.transform.position);
    }

}
