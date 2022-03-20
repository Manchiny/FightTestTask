using System.Collections.Generic;

public abstract class EnemySearchSystem
{
    public virtual string Name { get; protected set; }
    public abstract Unit GetEnemyUnit(Unit unit, List<Unit> enemyUnits);
}
