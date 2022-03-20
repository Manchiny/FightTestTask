using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int UNITS_COUNT = 3;

    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    [SerializeField] private List<Material> _teamMaterials;

    public Dictionary<int, List<Unit>> AllUnits { get; private set; }
    private Dictionary<int, List<Transform>> _allSpawnPoints;

    private void Awake()
    {
        AllUnits = new Dictionary<int, List<Unit>>();
        _allSpawnPoints = new Dictionary<int, List<Transform>>();

        foreach (var point in _spawnPoints)
        {
            if (_allSpawnPoints.TryGetValue(point.TeamId, out List<Transform> teamPoints) == false)
            {
                _allSpawnPoints.Add(point.TeamId, new List<Transform>());
            }
            _allSpawnPoints[point.TeamId].Add(point.transform);
        }
    }
    public void SpawnUnits()
    {
        for (int i = 0; i < _allSpawnPoints.Count; i++)
        {
            var points = _allSpawnPoints[i];

            for (int j = 0; j < UNITS_COUNT; j++)
            {
                Transform point = null;
                if (points[j] != null)
                    point = points[j];
                else
                    point = points[0];

                SpawnUnit(point, i, _teamMaterials[i]);
            }
        }
    }

    private void SpawnUnit(Transform point, int teamId, Material material)
    {
        var unit = Instantiate(_unitPrefab, point.position, point.rotation);
        unit.Init(teamId, material);

        if (AllUnits.TryGetValue(teamId, out List<Unit> units) == false)
        {
            AllUnits.Add(teamId, new List<Unit>());
        }
        AllUnits[teamId].Add(unit);
    }

    public void Restart()
    {
        for (int i = 0; i < AllUnits.Count; i++)
        {
            var units = AllUnits[i];
            var points = _allSpawnPoints[i];
            for (int j = 0; j < units.Count; j++)
            {
                var unit = units[j];
                var point = points[j];
                unit.transform.position = point.position;
                unit.transform.rotation = point.rotation;
                unit.Restart();
            }
        }
    }
}
