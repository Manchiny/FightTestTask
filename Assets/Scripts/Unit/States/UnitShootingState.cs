public class UnitShootingState : UnitState
{
    public override void OnUpdate()
    {
        if (IsFinished)
            return;
        if (_unit.EnemyTarget == null || _unit.EnemyTarget.CurrentHealth <=0 
                                      || _unit.IsEnoughDistance(_unit.EnemyTarget.transform.position) == false)
        {
            Exit();
            return;
        }
        _unit.Shoot();
    }
    public override void Exit()
    {
        _unit.StopShoot();
        base.Exit();
    }
}
