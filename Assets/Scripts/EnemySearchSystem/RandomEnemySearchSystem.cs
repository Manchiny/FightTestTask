using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomEnemySearchSystem : EnemySearchSystem
{
    private string _name = "Случайный";
    public override string Name => _name;
    public override Unit GetEnemyUnit(Unit unit, List<Unit> enemyUnits)
    {
        List<Unit> aliveUnits = enemyUnits.Where(e => e.CurrentHealth > 0).ToList();
        Unit randomEnemy = null;

        if (aliveUnits.Count > 0)
        {
            int random = Random.Range(0, aliveUnits.Count);
            randomEnemy = aliveUnits[random];
        }

        return randomEnemy;
    }
}
