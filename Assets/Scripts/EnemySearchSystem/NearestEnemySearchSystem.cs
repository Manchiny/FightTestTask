using System.Collections.Generic;
public class NearestEnemySearchSystem : EnemySearchSystem
{
    private string _name = "Ближайший";
    public override string Name => _name;
    public override Unit GetEnemyUnit(Unit unit, List<Unit> enemyUnits)
    {
        Unit nearestEnemy = null;
        var unitTransform = unit.transform;

        foreach (var enemy in enemyUnits)
        {
            if (nearestEnemy == null && enemy.CurrentHealth > 0)
                nearestEnemy = enemy;
            else if (enemy.CurrentHealth > 0)
            {
                var sqrDistanceNext = (unitTransform.position - enemy.transform.position).sqrMagnitude;
                var sqrDistanceCurrent = (unitTransform.position - nearestEnemy.transform.position).sqrMagnitude;
                if (sqrDistanceNext < sqrDistanceCurrent)
                {
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
