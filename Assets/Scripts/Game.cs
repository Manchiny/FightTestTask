using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private UIManager _uiManager;
    public float _gameTimer { get; private set; }
    private float _timerStep = 0.01f;

    private List<EnemySearchSystem> _enemySearchSystems;
    private EnemySearchSystem _defaultSearchSystem = new RandomEnemySearchSystem();
    private EnemySearchSystem _currentsSearchEnemySystem;

    private bool _isGameOver;

    public static Game Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(this);
    }

    private void Start()
    {
        Init();
        StartGame();
    }
    private void Init()
    {
        _spawner.SpawnUnits();

        _enemySearchSystems = new List<EnemySearchSystem>();
        _enemySearchSystems.Add(new RandomEnemySearchSystem());
        _enemySearchSystems.Add(new NearestEnemySearchSystem());
        _enemySearchSystems.Add(new TeamEnemySearchSystem(_spawner.AllUnits));

        _currentsSearchEnemySystem = _defaultSearchSystem;
    }
    private void StartGame()
    {
        _gameTimer = 0;

        StartCoroutine(StartGameTimer());
    }

    public void Restart()
    {
        _gameTimer = 0;
        _spawner.Restart();
        _isGameOver = false;
        StartCoroutine(StartGameTimer());
    }

    private void GameOver()
    {
        if (_isGameOver)
            return;

        StopAllCoroutines();
        _isGameOver = true;
        _uiManager.ShowGameOverWindow((float)Math.Round(_gameTimer, 2), _enemySearchSystems, _currentsSearchEnemySystem.GetType(), Restart);
    }

    public Unit FindEnemyForUnit(Unit unit)
    {
        if (_isGameOver)
            return null;

        List<Unit> enemies = new List<Unit>();

        foreach (var item in _spawner.AllUnits)
        {
            if (item.Key != unit.TeamID)
            {
                enemies = item.Value;
                continue;
            }
        }

        var enemy = _currentsSearchEnemySystem.GetEnemyUnit(unit, enemies);
        if (enemy == null)
            GameOver();

        return enemy;
    }

    private IEnumerator StartGameTimer()
    {
        yield return new WaitForSeconds(_timerStep);
        _gameTimer += _timerStep;
        StartCoroutine(StartGameTimer());
    }

    public void SetEnemySearchSystem(Type type)
    {
        foreach (var system in _enemySearchSystems)
        {
            if(type == system.GetType())
            {
                _currentsSearchEnemySystem = system;
                return;
            }
        }
    }
}
