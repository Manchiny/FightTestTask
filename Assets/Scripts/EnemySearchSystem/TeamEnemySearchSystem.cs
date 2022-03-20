using System.Collections.Generic;
using System.Linq;
public class TeamEnemySearchSystem : EnemySearchSystem
{
    private string _name = "Коммандный";
    public override string Name => _name;

    private Dictionary<int, List<Unit>> _allUnits;
    private Dictionary<int, int> _currenTarget; // id команды, id юнита, которого все атакуют;
    public TeamEnemySearchSystem(Dictionary<int, List<Unit>> allUnits)
    {
        _allUnits = allUnits;
        _currenTarget = new Dictionary<int, int>();

        foreach (var team in allUnits)

            foreach (var unit in team.Value)
            {
                if (!_currenTarget.ContainsKey(unit.TeamID))
                {
                    _currenTarget.Add(unit.TeamID, 0);
                }
            }

    }
    public override Unit GetEnemyUnit(Unit unit, List<Unit> enemyUnits)
    {
        Unit enemy = null;

        var enemies = _allUnits[enemyUnits[0].TeamID];
        var current = enemies[_currenTarget[unit.TeamID]];

        if (current != null && current.CurrentHealth > 0)
        {
            enemy = current;
        }
        else if (enemies != null && enemies.Count > 0)
        {
            enemy = enemies?.Where(e => e.CurrentHealth > 0)?.FirstOrDefault();
        }

        return enemy;
    }
}
